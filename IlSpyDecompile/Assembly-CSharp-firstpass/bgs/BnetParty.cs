using System;
using System.Collections.Generic;
using System.Linq;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bgs
{
	public static class BnetParty
	{
		public delegate void PartyErrorHandler(PartyError error);

		public delegate void JoinedHandler(OnlineEventType evt, PartyInfo party, LeaveReason? reason);

		public delegate void PrivacyLevelChangedHandler(PartyInfo party, PrivacyLevel newPrivacyLevel);

		public delegate void MemberEventHandler(OnlineEventType evt, PartyInfo party, BnetGameAccountId memberId, bool isRolesUpdate, LeaveReason? reason);

		public delegate void ReceivedInviteHandler(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, InviteRemoveReason? reason);

		public delegate void SentInviteHandler(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, bool senderIsMyself, InviteRemoveReason? reason);

		public delegate void ReceivedInviteRequestHandler(OnlineEventType evt, PartyInfo party, InviteRequest request, InviteRequestRemovedReason? reason);

		public delegate void ChatMessageHandler(PartyInfo party, BnetGameAccountId speakerId, string chatMessage);

		public delegate void PartyAttributeChangedHandler(PartyInfo party, string attributeKey, bnet.protocol.Variant attributeValue);

		public delegate void MemberAttributeChangedHandler(PartyInfo party, BnetGameAccountId partyMember, string attributeKey, bnet.protocol.Variant attributeValue);

		public delegate void CreateSuccessCallback(PartyType type, PartyId newlyCreatedPartyId);

		public enum FriendlyGameRoleSet
		{
			Inviter = 1,
			Invitee
		}

		public enum SpectatorPartyRoleSet
		{
			Member = 1,
			Leader
		}

		public const string ATTRIBUTE_PARTY_TYPE = "WTCG.Party.Type";

		public const string ATTRIBUTE_PARTY_CREATOR = "WTCG.Party.Creator";

		public const string ATTRIBUTE_SCENARIO_ID = "WTCG.Game.ScenarioId";

		public const string ATTRIBUTE_FRIENDLY_DECLINE_REASON = "WTCG.Friendly.DeclineReason";

		public const string ATTRIBUTE_FORMAT_TYPE = "WTCG.Format.Type";

		public const string ATTRIBUTE_BRAWL_TYPE = "WTCG.Brawl.Type";

		public const string ATTRIBUTE_SEASON_ID = "WTCG.Season.Id";

		public const string ATTRIBUTE_BRAWL_LIBRARY_ITEM_ID = "WTCG.Brawl.LibraryItemId";

		public const string ATTRIBUTE_PARTY_SERVER_INFO = "WTCG.Party.ServerInfo";

		private static Map<BnetFeatureEvent, HashSet<BattleNetErrors>> s_ignorableErrorCodes = new Map<BnetFeatureEvent, HashSet<BattleNetErrors>>
		{
			{
				BnetFeatureEvent.Party_KickMember_Callback,
				new HashSet<BattleNetErrors> { BattleNetErrors.ERROR_CHANNEL_NO_SUCH_MEMBER }
			},
			{
				BnetFeatureEvent.Party_Leave_Callback,
				new HashSet<BattleNetErrors>
				{
					BattleNetErrors.ERROR_CHANNEL_NOT_MEMBER,
					BattleNetErrors.ERROR_CHANNEL_NO_CHANNEL,
					BattleNetErrors.ERROR_RPC_INVALID_OBJECT
				}
			},
			{
				BnetFeatureEvent.Party_Dissolve_Callback,
				new HashSet<BattleNetErrors>
				{
					BattleNetErrors.ERROR_CHANNEL_NOT_MEMBER,
					BattleNetErrors.ERROR_CHANNEL_NO_CHANNEL,
					BattleNetErrors.ERROR_RPC_INVALID_OBJECT
				}
			}
		};

		private static Map<PartyId, PartyType> s_joinedParties = new Map<PartyId, PartyType>();

		private static Map<PartyType, CreateSuccessCallback> s_pendingPartyCreates = null;

		private static Map<string, List<PartyAttributeChangedHandler>> s_attributeChangedSubscribers = null;

		public static event PartyErrorHandler OnError;

		public static event JoinedHandler OnJoined;

		public static event PrivacyLevelChangedHandler OnPrivacyLevelChanged;

		public static event MemberEventHandler OnMemberEvent;

		public static event ReceivedInviteHandler OnReceivedInvite;

		public static event SentInviteHandler OnSentInvite;

		public static event ReceivedInviteRequestHandler OnReceivedInviteRequest;

		public static event ChatMessageHandler OnChatMessage;

		public static event PartyAttributeChangedHandler OnPartyAttributeChanged;

		public static event MemberAttributeChangedHandler OnMemberAttributeChanged;

		public static void RegisterAttributeChangedHandler(string attributeKey, PartyAttributeChangedHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (s_attributeChangedSubscribers == null)
			{
				s_attributeChangedSubscribers = new Map<string, List<PartyAttributeChangedHandler>>();
			}
			if (!s_attributeChangedSubscribers.TryGetValue(attributeKey, out var value))
			{
				value = new List<PartyAttributeChangedHandler>();
				s_attributeChangedSubscribers[attributeKey] = value;
			}
			if (!value.Contains(handler))
			{
				value.Add(handler);
			}
		}

		public static bool UnregisterAttributeChangedHandler(string attributeKey, PartyAttributeChangedHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (s_attributeChangedSubscribers == null)
			{
				return false;
			}
			List<PartyAttributeChangedHandler> value = null;
			if (s_attributeChangedSubscribers.TryGetValue(attributeKey, out value))
			{
				return value.Remove(handler);
			}
			return false;
		}

		public static bool IsInParty(PartyId partyId)
		{
			if (partyId == null)
			{
				return false;
			}
			return s_joinedParties.ContainsKey(partyId);
		}

		public static PartyId[] GetJoinedPartyIds()
		{
			return s_joinedParties.Keys.ToArray();
		}

		public static PartyInfo[] GetJoinedParties()
		{
			return s_joinedParties.Select((KeyValuePair<PartyId, PartyType> kv) => new PartyInfo(kv.Key, kv.Value)).ToArray();
		}

		public static PartyInfo GetJoinedParty(PartyId partyId)
		{
			if (partyId == null)
			{
				return null;
			}
			PartyType value = PartyType.DEFAULT;
			if (s_joinedParties.TryGetValue(partyId, out value))
			{
				return new PartyInfo(partyId, value);
			}
			return null;
		}

		public static PartyType GetPartyType(PartyId partyId)
		{
			PartyType value = PartyType.DEFAULT;
			if (partyId == null)
			{
				return value;
			}
			s_joinedParties.TryGetValue(partyId, out value);
			return value;
		}

		public static bool IsLeader(PartyId partyId)
		{
			if (partyId == null)
			{
				return false;
			}
			PartyMember myselfMember = GetMyselfMember(partyId);
			if (myselfMember != null)
			{
				PartyType partyType = GetPartyType(partyId);
				if (myselfMember.IsLeader(partyType))
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsMember(PartyId partyId, BnetGameAccountId memberId)
		{
			if (partyId == null)
			{
				return false;
			}
			return GetMember(partyId, memberId) != null;
		}

		public static bool IsPartyFull(PartyId partyId, bool includeInvites = true)
		{
			if (partyId == null)
			{
				return false;
			}
			int num = CountMembers(partyId);
			int num2 = (includeInvites ? GetSentInvites(partyId).Length : 0);
			int maxPartyMembers = BattleNet.GetMaxPartyMembers(partyId.ToEntityId());
			return num + num2 >= maxPartyMembers;
		}

		public static int CountMembers(PartyId partyId)
		{
			if (partyId == null)
			{
				return 0;
			}
			return BattleNet.GetCountPartyMembers(partyId.ToEntityId());
		}

		public static PrivacyLevel GetPrivacyLevel(PartyId partyId)
		{
			if (partyId == null)
			{
				return PrivacyLevel.CLOSED;
			}
			return (PrivacyLevel)BattleNet.GetPartyPrivacy(partyId.ToEntityId());
		}

		public static PartyMember GetMember(PartyId partyId, BnetGameAccountId memberId)
		{
			PartyMember[] members = GetMembers(partyId);
			foreach (PartyMember partyMember in members)
			{
				if (partyMember.GameAccountId == memberId)
				{
					return partyMember;
				}
			}
			return null;
		}

		public static PartyMember GetLeader(PartyId partyId)
		{
			if (partyId == null)
			{
				return null;
			}
			PartyMember[] members = GetMembers(partyId);
			PartyType partyType = GetPartyType(partyId);
			foreach (PartyMember partyMember in members)
			{
				if (partyMember.IsLeader(partyType))
				{
					return partyMember;
				}
			}
			return null;
		}

		public static PartyMember GetMyselfMember(PartyId partyId)
		{
			if (partyId == null)
			{
				return null;
			}
			BnetGameAccountId bnetGameAccountId = BnetGameAccountId.CreateFromEntityId(BattleNet.GetMyGameAccountId());
			if (bnetGameAccountId == null)
			{
				return null;
			}
			return GetMember(partyId, bnetGameAccountId);
		}

		public static PartyMember[] GetMembers(PartyId partyId)
		{
			if (partyId == null)
			{
				return new PartyMember[0];
			}
			BattleNet.GetPartyMembers(partyId.ToEntityId(), out var members);
			PartyMember[] array = new PartyMember[members.Length];
			for (int i = 0; i < array.Length; i++)
			{
				bgs.types.PartyMember partyMember = members[i];
				PartyMember partyMember2 = new PartyMember();
				partyMember2.GameAccountId = BnetGameAccountId.CreateFromEntityId(partyMember.memberGameAccountId);
				partyMember2.RoleIds = new uint[1] { partyMember.firstMemberRole };
				partyMember2.BattleTag = partyMember.battleTag;
				partyMember2.VoiceId = partyMember.voiceId;
				array[i] = partyMember2;
			}
			return array;
		}

		public static PartyInvite GetReceivedInvite(ulong inviteId)
		{
			return GetReceivedInvites().FirstOrDefault((PartyInvite i) => i.InviteId == inviteId);
		}

		public static PartyInvite GetReceivedInviteFrom(BnetGameAccountId inviterId, PartyType partyType)
		{
			return GetReceivedInvites().FirstOrDefault((PartyInvite i) => i.InviterId == inviterId && i.PartyType == partyType);
		}

		public static PartyInvite[] GetReceivedInvites()
		{
			BattleNet.GetReceivedPartyInvites(out var invites);
			return invites;
		}

		public static PartyInvite GetSentInvite(PartyId partyId, ulong inviteId)
		{
			if (partyId == null)
			{
				return null;
			}
			return GetSentInvites(partyId).FirstOrDefault((PartyInvite i) => i.InviteId == inviteId);
		}

		public static PartyInvite[] GetSentInvites(PartyId partyId)
		{
			if (partyId == null)
			{
				return new PartyInvite[0];
			}
			BattleNet.GetPartySentInvites(partyId.ToEntityId(), out var invites);
			return invites;
		}

		public static InviteRequest[] GetInviteRequests(PartyId partyId)
		{
			if (partyId == null)
			{
				return new InviteRequest[0];
			}
			BattleNet.GetPartyInviteRequests(partyId.ToEntityId(), out var requests);
			return requests;
		}

		public static KeyValuePair<string, object>[] GetAllPartyAttributes(PartyId partyId)
		{
			if (partyId == null)
			{
				return new KeyValuePair<string, object>[0];
			}
			BattleNet.GetAllPartyAttributes(partyId.ToEntityId(), out var allKeys);
			KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[allKeys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string text = allKeys[i];
				object value = null;
				long? partyAttributeLong = GetPartyAttributeLong(partyId, text);
				if (partyAttributeLong.HasValue)
				{
					value = partyAttributeLong;
				}
				string partyAttributeString = GetPartyAttributeString(partyId, text);
				if (partyAttributeString != null)
				{
					value = partyAttributeString;
				}
				byte[] partyAttributeBlob = GetPartyAttributeBlob(partyId, text);
				if (partyAttributeBlob != null)
				{
					value = partyAttributeBlob;
				}
				array[i] = new KeyValuePair<string, object>(text, value);
			}
			return array;
		}

		public static bnet.protocol.Attribute[] GetAllPartyAttributesVariant(PartyId partyId)
		{
			if (partyId == null)
			{
				return new bnet.protocol.Attribute[0];
			}
			BattleNet.GetAllPartyAttributes(partyId.ToEntityId(), out var allKeys);
			bnet.protocol.Attribute[] array = new bnet.protocol.Attribute[allKeys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string text = allKeys[i];
				bnet.protocol.Variant partyAttributeVariant = GetPartyAttributeVariant(partyId, text);
				bnet.protocol.Attribute attribute2 = new bnet.protocol.Attribute();
				attribute2.SetName(text);
				attribute2.SetValue(partyAttributeVariant);
				array[i] = attribute2;
			}
			return array;
		}

		public static bnet.protocol.Variant GetPartyAttributeVariant(PartyId partyId, string attributeKey)
		{
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			bgs.types.EntityId partyId2 = partyId.ToEntityId();
			if (BattleNet.GetPartyAttributeLong(partyId2, attributeKey, out var value))
			{
				variant.IntValue = value;
			}
			else
			{
				BattleNet.GetPartyAttributeString(partyId2, attributeKey, out var value2);
				if (value2 != null)
				{
					variant.StringValue = value2;
				}
				else
				{
					BattleNet.GetPartyAttributeBlob(partyId2, attributeKey, out var value3);
					if (value3 != null)
					{
						variant.BlobValue = value3;
					}
				}
			}
			return variant;
		}

		public static long? GetPartyAttributeLong(PartyId partyId, string attributeKey)
		{
			if (partyId == null)
			{
				return null;
			}
			if (BattleNet.GetPartyAttributeLong(partyId.ToEntityId(), attributeKey, out var value))
			{
				return value;
			}
			return null;
		}

		public static string GetPartyAttributeString(PartyId partyId, string attributeKey)
		{
			if (partyId == null)
			{
				return null;
			}
			BattleNet.GetPartyAttributeString(partyId.ToEntityId(), attributeKey, out var value);
			return value;
		}

		public static byte[] GetPartyAttributeBlob(PartyId partyId, string attributeKey)
		{
			if (partyId == null)
			{
				return null;
			}
			BattleNet.GetPartyAttributeBlob(partyId.ToEntityId(), attributeKey, out var value);
			return value;
		}

		public static string GetMemberAttributeString(PartyId partyId, GameAccountHandle partyMember, string attributeKey)
		{
			if (partyId == null)
			{
				return null;
			}
			BattleNet.GetMemberAttributeString(partyId.ToEntityId(), partyMember, attributeKey, out var value);
			return value;
		}

		public static byte[] GetMemberAttributeBlob(PartyId partyId, GameAccountHandle partyMember, string attributeKey)
		{
			if (partyId == null)
			{
				return null;
			}
			BattleNet.GetMemberAttributeBlob(partyId.ToEntityId(), partyMember, attributeKey, out var value);
			return value;
		}

		public static void SetDisconnectedFromBattleNet()
		{
			s_joinedParties.Clear();
			if (s_pendingPartyCreates != null)
			{
				s_pendingPartyCreates.Clear();
			}
		}

		public static void CreateParty(PartyType partyType, PrivacyLevel privacyLevel, byte[] creatorBlob, CreateSuccessCallback successCallback)
		{
			string @string = EnumUtils.GetString(partyType);
			if (s_pendingPartyCreates != null && s_pendingPartyCreates.ContainsKey(partyType))
			{
				RaisePartyError(true, @string, BnetFeatureEvent.Party_Create_Callback, "CreateParty: Already creating party of type {0}", partyType);
				return;
			}
			if (s_pendingPartyCreates == null)
			{
				s_pendingPartyCreates = new Map<PartyType, CreateSuccessCallback>();
			}
			s_pendingPartyCreates[partyType] = successCallback;
			bnet.protocol.v2.Attribute attribute2 = ProtocolHelper.CreateAttributeV2("WTCG.Party.Creator", creatorBlob);
			BattleNet.CreateParty(@string, (int)privacyLevel, attribute2);
		}

		public static void CreateParty(PartyType partyType, PrivacyLevel privacyLevel, CreateSuccessCallback successCallback, params bnet.protocol.v2.Attribute[] partyAttributes)
		{
			string @string = EnumUtils.GetString(partyType);
			if (s_pendingPartyCreates != null && s_pendingPartyCreates.ContainsKey(partyType))
			{
				RaisePartyError(true, @string, BnetFeatureEvent.Party_Create_Callback, "CreateParty: Already creating party of type {0}", partyType);
				return;
			}
			if (s_pendingPartyCreates == null)
			{
				s_pendingPartyCreates = new Map<PartyType, CreateSuccessCallback>();
			}
			s_pendingPartyCreates[partyType] = successCallback;
			BattleNet.CreateParty(@string, (int)privacyLevel, partyAttributes);
		}

		public static void JoinParty(PartyId partyId, PartyType partyType)
		{
			BattleNet.JoinParty(partyId.ToEntityId(), EnumUtils.GetString(partyType));
		}

		public static void Leave(PartyId partyId)
		{
			if (IsInParty(partyId))
			{
				BattleNet.LeaveParty(partyId.ToEntityId());
			}
		}

		public static void DissolveParty(PartyId partyId)
		{
			if (IsInParty(partyId))
			{
				BattleNet.DissolveParty(partyId.ToEntityId());
			}
		}

		public static void SetPrivacy(PartyId partyId, PrivacyLevel privacyLevel)
		{
			if (IsInParty(partyId))
			{
				BattleNet.SetPartyPrivacy(partyId.ToEntityId(), (int)privacyLevel);
			}
		}

		public static void SetLeader(PartyId partyId, BnetGameAccountId memberId)
		{
			if (IsInParty(partyId))
			{
				bgs.types.EntityId partyId2 = partyId.ToEntityId();
				bgs.types.EntityId memberId2 = BnetEntityId.CreateEntityId(memberId);
				uint leaderRoleId = PartyMember.GetLeaderRoleId(GetPartyType(partyId));
				BattleNet.AssignPartyRole(partyId2, memberId2, leaderRoleId);
			}
		}

		public static void SendInvite(PartyId toWhichPartyId, BnetGameAccountId recipientId, bool isReservation)
		{
			if (IsInParty(toWhichPartyId))
			{
				bgs.types.EntityId partyId = toWhichPartyId.ToEntityId();
				bgs.types.EntityId inviteeId = BnetEntityId.CreateEntityId(recipientId);
				BattleNet.SendPartyInvite(partyId, inviteeId, isReservation);
			}
		}

		public static void AcceptReceivedInvite(ulong inviteId)
		{
			BattleNet.AcceptPartyInvite(inviteId);
		}

		public static void DeclineReceivedInvite(ulong inviteId)
		{
			BattleNet.DeclinePartyInvite(inviteId);
		}

		public static void RevokeSentInvite(PartyId partyId, ulong inviteId)
		{
			if (IsInParty(partyId))
			{
				BattleNet.RevokePartyInvite(partyId.ToEntityId(), inviteId);
			}
		}

		public static void RequestInvite(PartyId partyId, BnetGameAccountId whomToAskForApproval, BnetGameAccountId whomToInvite, PartyType partyType)
		{
			if (IsLeader(partyId))
			{
				PartyError error = default(PartyError);
				error.IsOperationCallback = true;
				error.DebugContext = "RequestInvite";
				error.ErrorCode = BattleNetErrors.ERROR_INVALID_TARGET_ID;
				error.Feature = BnetFeature.Party;
				error.FeatureEvent = BnetFeatureEvent.Party_RequestPartyInvite_Callback;
				error.PartyId = partyId;
				error.szPartyType = EnumUtils.GetString(partyType);
				error.StringData = "leaders cannot RequestInvite - use SendInvite instead.";
				BnetParty.OnError(error);
			}
			else
			{
				bgs.types.EntityId partyId2 = partyId.ToEntityId();
				bgs.types.EntityId whomToAskForApproval2 = BnetEntityId.CreateEntityId(whomToAskForApproval);
				bgs.types.EntityId whomToInvite2 = BnetEntityId.CreateEntityId(whomToInvite);
				string @string = EnumUtils.GetString(partyType);
				BattleNet.RequestPartyInvite(partyId2, whomToAskForApproval2, whomToInvite2, @string);
			}
		}

		public static void AcceptInviteRequest(PartyId partyId, BnetGameAccountId requestedTargetId, bool isReservation)
		{
			SendInvite(partyId, requestedTargetId, isReservation);
		}

		public static void IgnoreInviteRequest(PartyId partyId, BnetGameAccountId requestedTargetId)
		{
			bgs.types.EntityId partyId2 = partyId.ToEntityId();
			bgs.types.EntityId requestedTargetId2 = BnetEntityId.CreateEntityId(requestedTargetId);
			BattleNet.IgnoreInviteRequest(partyId2, requestedTargetId2);
		}

		public static void KickMember(PartyId partyId, BnetGameAccountId memberId)
		{
			if (IsInParty(partyId))
			{
				bgs.types.EntityId partyId2 = partyId.ToEntityId();
				bgs.types.EntityId memberId2 = BnetEntityId.CreateEntityId(memberId);
				BattleNet.KickPartyMember(partyId2, memberId2);
			}
		}

		public static void SendChatMessage(PartyId partyId, string chatMessage)
		{
			if (IsInParty(partyId))
			{
				BattleNet.SendPartyChatMessage(partyId.ToEntityId(), chatMessage);
			}
		}

		public static void ClearPartyAttribute(PartyId partyId, string attributeKey)
		{
			BattleNet.ClearPartyAttribute(partyId.ToEntityId(), attributeKey);
		}

		public static void SetPartyAttributeLong(PartyId partyId, string attributeKey, long value)
		{
			BattleNet.SetPartyAttributeLong(partyId.ToEntityId(), attributeKey, value);
		}

		public static void SetPartyAttributeString(PartyId partyId, string attributeKey, string value)
		{
			BattleNet.SetPartyAttributeString(partyId.ToEntityId(), attributeKey, value);
		}

		public static void SetPartyAttributeBlob(PartyId partyId, string attributeKey, byte[] value)
		{
			BattleNet.SetPartyAttributeBlob(partyId.ToEntityId(), attributeKey, value);
		}

		public static void SetPartyAttributes(PartyId partyId, params bnet.protocol.v2.Attribute[] attrs)
		{
			BattleNet.SetPartyAttributes(partyId.ToEntityId(), attrs);
		}

		public static void ClearMemberAttribute(PartyId partyId, GameAccountHandle partyMember, string attributeKey)
		{
			BattleNet.ClearMemberAttribute(partyId.ToEntityId(), partyMember, attributeKey);
		}

		public static void SetMemberAttributeLong(PartyId partyId, GameAccountHandle partyMember, string attributeKey, long value)
		{
			BattleNet.SetMemberAttributeLong(partyId.ToEntityId(), partyMember, attributeKey, value);
		}

		public static void SetMemberAttributeString(PartyId partyId, GameAccountHandle partyMember, string attributeKey, string value)
		{
			BattleNet.SetMemberAttributeString(partyId.ToEntityId(), partyMember, attributeKey, value);
		}

		public static void SetMemberAttributeBlob(PartyId partyId, GameAccountHandle partyMember, string attributeKey, byte[] value)
		{
			BattleNet.SetMemberAttributeBlob(partyId.ToEntityId(), partyMember, attributeKey, value);
		}

		public static void SetMemberAttributes(PartyId partyId, GameAccountHandle partyMember, params bnet.protocol.v2.Attribute[] attrs)
		{
			BattleNet.SetMemberAttributes(partyId.ToEntityId(), partyMember, attrs);
		}

		public static void RemoveFromAllEventHandlers(object targetObject)
		{
			Type type = targetObject?.GetType();
			if (BnetParty.OnError != null)
			{
				foreach (Delegate item in BnetParty.OnError.GetInvocationList().Clone() as Array)
				{
					if (item.Target == targetObject || (item.Target == null && item.Method.DeclaringType == type))
					{
						OnError -= (PartyErrorHandler)item;
					}
				}
			}
			if (BnetParty.OnJoined != null)
			{
				foreach (Delegate item2 in BnetParty.OnJoined.GetInvocationList().Clone() as Array)
				{
					if (item2.Target == targetObject || (item2.Target == null && item2.Method.DeclaringType == type))
					{
						OnJoined -= (JoinedHandler)item2;
					}
				}
			}
			if (BnetParty.OnPrivacyLevelChanged != null)
			{
				foreach (Delegate item3 in BnetParty.OnPrivacyLevelChanged.GetInvocationList().Clone() as Array)
				{
					if (item3.Target == targetObject || (item3.Target == null && item3.Method.DeclaringType == type))
					{
						OnPrivacyLevelChanged -= (PrivacyLevelChangedHandler)item3;
					}
				}
			}
			if (BnetParty.OnMemberEvent != null)
			{
				foreach (Delegate item4 in BnetParty.OnMemberEvent.GetInvocationList().Clone() as Array)
				{
					if (item4.Target == targetObject || (item4.Target == null && item4.Method.DeclaringType == type))
					{
						OnMemberEvent -= (MemberEventHandler)item4;
					}
				}
			}
			if (BnetParty.OnReceivedInvite != null)
			{
				foreach (Delegate item5 in BnetParty.OnReceivedInvite.GetInvocationList().Clone() as Array)
				{
					if (item5.Target == targetObject || (item5.Target == null && item5.Method.DeclaringType == type))
					{
						OnReceivedInvite -= (ReceivedInviteHandler)item5;
					}
				}
			}
			if (BnetParty.OnSentInvite != null)
			{
				foreach (Delegate item6 in BnetParty.OnSentInvite.GetInvocationList().Clone() as Array)
				{
					if (item6.Target == targetObject || (item6.Target == null && item6.Method.DeclaringType == type))
					{
						OnSentInvite -= (SentInviteHandler)item6;
					}
				}
			}
			if (BnetParty.OnReceivedInviteRequest != null)
			{
				foreach (Delegate item7 in BnetParty.OnReceivedInviteRequest.GetInvocationList().Clone() as Array)
				{
					if (item7.Target == targetObject || (item7.Target == null && item7.Method.DeclaringType == type))
					{
						OnReceivedInviteRequest -= (ReceivedInviteRequestHandler)item7;
					}
				}
			}
			if (BnetParty.OnChatMessage != null)
			{
				foreach (Delegate item8 in BnetParty.OnChatMessage.GetInvocationList().Clone() as Array)
				{
					if (item8.Target == targetObject || (item8.Target == null && item8.Method.DeclaringType == type))
					{
						OnChatMessage -= (ChatMessageHandler)item8;
					}
				}
			}
			if (BnetParty.OnPartyAttributeChanged != null)
			{
				foreach (Delegate item9 in BnetParty.OnPartyAttributeChanged.GetInvocationList().Clone() as Array)
				{
					if (item9.Target == targetObject || (item9.Target == null && item9.Method.DeclaringType == type))
					{
						OnPartyAttributeChanged -= (PartyAttributeChangedHandler)item9;
					}
				}
			}
			if (BnetParty.OnMemberAttributeChanged != null)
			{
				foreach (Delegate item10 in BnetParty.OnMemberAttributeChanged.GetInvocationList().Clone() as Array)
				{
					if (item10.Target == targetObject || (item10.Target == null && item10.Method.DeclaringType == type))
					{
						OnMemberAttributeChanged -= (MemberAttributeChangedHandler)item10;
					}
				}
			}
			if (s_attributeChangedSubscribers == null)
			{
				return;
			}
			foreach (KeyValuePair<string, List<PartyAttributeChangedHandler>> s_attributeChangedSubscriber in s_attributeChangedSubscribers)
			{
				var array = s_attributeChangedSubscriber.Value.Select((PartyAttributeChangedHandler h, int idx) => new
				{
					Handler = h,
					Index = idx
				}).ToArray();
				foreach (var anon in array)
				{
					if (anon.Handler.Target == targetObject || anon.Handler.Method.DeclaringType == type)
					{
						s_attributeChangedSubscriber.Value.RemoveAt(anon.Index);
					}
				}
			}
		}

		private static bool IsIgnorableError(BnetFeatureEvent feature, BattleNetErrors code)
		{
			if (s_ignorableErrorCodes.TryGetValue(feature, out var value))
			{
				return value.Contains(code);
			}
			return false;
		}

		public static void Process()
		{
			BattleNet.GetPartyListenerEvents(out var events);
			BattleNet.ClearPartyListenerEvents();
			for (int i = 0; i < events.Length; i++)
			{
				PartyListenerEvent partyListenerEvent = events[i];
				PartyId partyId = partyListenerEvent.PartyId;
				switch (partyListenerEvent.Type)
				{
				case PartyListenerEventType.ERROR_RAISED:
				case PartyListenerEventType.OPERATION_CALLBACK:
				{
					PartyError error = partyListenerEvent.ToPartyError();
					if (error.ErrorCode != BattleNetErrors.ERROR_OK)
					{
						if (IsIgnorableError(error.FeatureEvent, error.ErrorCode.EnumVal))
						{
							error.ErrorCode = BattleNetErrors.ERROR_OK;
							if (error.FeatureEvent == BnetFeatureEvent.Party_Leave_Callback)
							{
								if (!s_joinedParties.ContainsKey(partyId))
								{
									s_joinedParties[partyId] = PartyType.SPECTATOR_PARTY;
								}
								goto case PartyListenerEventType.LEFT_PARTY;
							}
						}
						if (error.IsOperationCallback && error.FeatureEvent == BnetFeatureEvent.Party_Create_Callback)
						{
							PartyType partyType = error.PartyType;
							if (s_pendingPartyCreates.ContainsKey(partyType))
							{
								s_pendingPartyCreates.Remove(partyType);
							}
						}
					}
					if (error.ErrorCode != BattleNetErrors.ERROR_OK)
					{
						RaisePartyError(error);
					}
					break;
				}
				case PartyListenerEventType.JOINED_PARTY:
				{
					string stringData3 = partyListenerEvent.StringData;
					PartyType partyTypeFromString2 = GetPartyTypeFromString(stringData3);
					s_joinedParties[partyId] = partyTypeFromString2;
					if (s_pendingPartyCreates != null)
					{
						CreateSuccessCallback createSuccessCallback = null;
						if (s_pendingPartyCreates.ContainsKey(partyTypeFromString2))
						{
							createSuccessCallback = s_pendingPartyCreates[partyTypeFromString2];
							s_pendingPartyCreates.Remove(partyTypeFromString2);
						}
						else if (stringData3 == "default" && s_pendingPartyCreates.Count == 0)
						{
							createSuccessCallback = s_pendingPartyCreates.First().Value;
							s_pendingPartyCreates.Clear();
						}
						createSuccessCallback?.Invoke(partyTypeFromString2, partyId);
					}
					if (BnetParty.OnJoined != null)
					{
						BnetParty.OnJoined(OnlineEventType.ADDED, new PartyInfo(partyId, partyTypeFromString2), null);
					}
					break;
				}
				case PartyListenerEventType.LEFT_PARTY:
				{
					if (s_joinedParties.TryGetValue(partyId, out var value2))
					{
						s_joinedParties.Remove(partyId);
					}
					else
					{
						value2 = GetPartyTypeFromString(partyListenerEvent.StringData);
					}
					if (BnetParty.OnJoined != null)
					{
						BnetParty.OnJoined(OnlineEventType.REMOVED, new PartyInfo(partyId, value2), (LeaveReason)partyListenerEvent.UintData);
					}
					break;
				}
				case PartyListenerEventType.PRIVACY_CHANGED:
					if (BnetParty.OnPrivacyLevelChanged != null)
					{
						BnetParty.OnPrivacyLevelChanged(GetJoinedParty(partyId), (PrivacyLevel)partyListenerEvent.UintData);
					}
					break;
				case PartyListenerEventType.MEMBER_JOINED:
				case PartyListenerEventType.MEMBER_LEFT:
					if (BnetParty.OnMemberEvent != null)
					{
						OnlineEventType evt4 = ((partyListenerEvent.Type != PartyListenerEventType.MEMBER_JOINED) ? OnlineEventType.REMOVED : OnlineEventType.ADDED);
						LeaveReason? reason4 = null;
						if (partyListenerEvent.Type == PartyListenerEventType.MEMBER_LEFT)
						{
							reason4 = (LeaveReason)partyListenerEvent.UintData;
						}
						BnetParty.OnMemberEvent(evt4, GetJoinedParty(partyId), partyListenerEvent.SubjectMemberId, isRolesUpdate: false, reason4);
					}
					break;
				case PartyListenerEventType.MEMBER_ROLE_CHANGED:
					if (BnetParty.OnMemberEvent != null)
					{
						BnetParty.OnMemberEvent(OnlineEventType.UPDATED, GetJoinedParty(partyId), partyListenerEvent.SubjectMemberId, isRolesUpdate: true, null);
					}
					break;
				case PartyListenerEventType.RECEIVED_INVITE_ADDED:
				case PartyListenerEventType.RECEIVED_INVITE_REMOVED:
					if (BnetParty.OnReceivedInvite != null)
					{
						OnlineEventType evt3 = ((partyListenerEvent.Type != PartyListenerEventType.RECEIVED_INVITE_ADDED) ? OnlineEventType.REMOVED : OnlineEventType.ADDED);
						PartyType outVal2 = PartyType.DEFAULT;
						if (partyListenerEvent.StringData != null)
						{
							EnumUtils.TryGetEnum<PartyType>(partyListenerEvent.StringData, out outVal2);
						}
						PartyInfo party2 = new PartyInfo(partyId, outVal2);
						InviteRemoveReason? reason3 = null;
						if (partyListenerEvent.Type == PartyListenerEventType.RECEIVED_INVITE_REMOVED)
						{
							reason3 = (InviteRemoveReason)partyListenerEvent.UintData;
						}
						BnetParty.OnReceivedInvite(evt3, party2, partyListenerEvent.UlongData, partyListenerEvent.SubjectMemberId, partyListenerEvent.TargetMemberId, reason3);
					}
					break;
				case PartyListenerEventType.PARTY_INVITE_SENT:
				case PartyListenerEventType.PARTY_INVITE_REMOVED:
					if (BnetParty.OnSentInvite != null)
					{
						bool senderIsMyself = partyListenerEvent.SubjectMemberId == BnetGameAccountId.CreateFromEntityId(BattleNet.GetMyGameAccountId());
						OnlineEventType evt = ((partyListenerEvent.Type != PartyListenerEventType.PARTY_INVITE_SENT) ? OnlineEventType.REMOVED : OnlineEventType.ADDED);
						PartyType outVal = PartyType.DEFAULT;
						if (partyListenerEvent.StringData != null)
						{
							EnumUtils.TryGetEnum<PartyType>(partyListenerEvent.StringData, out outVal);
						}
						PartyInfo party = new PartyInfo(partyId, outVal);
						InviteRemoveReason? reason = null;
						if (partyListenerEvent.Type == PartyListenerEventType.PARTY_INVITE_REMOVED)
						{
							reason = (InviteRemoveReason)partyListenerEvent.UintData;
						}
						BnetParty.OnSentInvite(evt, party, partyListenerEvent.UlongData, partyListenerEvent.SubjectMemberId, partyListenerEvent.TargetMemberId, senderIsMyself, reason);
					}
					break;
				case PartyListenerEventType.INVITE_REQUEST_ADDED:
				case PartyListenerEventType.INVITE_REQUEST_REMOVED:
					if (BnetParty.OnReceivedInviteRequest != null)
					{
						OnlineEventType evt2 = ((partyListenerEvent.Type != PartyListenerEventType.INVITE_REQUEST_ADDED) ? OnlineEventType.REMOVED : OnlineEventType.ADDED);
						PartyInfo joinedParty3 = GetJoinedParty(partyId);
						InviteRequestRemovedReason? reason2 = null;
						if (partyListenerEvent.Type == PartyListenerEventType.INVITE_REQUEST_REMOVED)
						{
							reason2 = (InviteRequestRemovedReason)partyListenerEvent.UintData;
						}
						InviteRequest inviteRequest = new InviteRequest();
						inviteRequest.TargetId = partyListenerEvent.TargetMemberId;
						inviteRequest.TargetName = partyListenerEvent.StringData2;
						inviteRequest.RequesterId = partyListenerEvent.SubjectMemberId;
						inviteRequest.RequesterName = partyListenerEvent.StringData;
						BnetParty.OnReceivedInviteRequest(evt2, joinedParty3, inviteRequest, reason2);
					}
					break;
				case PartyListenerEventType.CHAT_MESSAGE_RECEIVED:
					if (BnetParty.OnChatMessage != null)
					{
						BnetParty.OnChatMessage(GetJoinedParty(partyId), partyListenerEvent.SubjectMemberId, partyListenerEvent.StringData);
					}
					break;
				case PartyListenerEventType.PARTY_ATTRIBUTE_CHANGED:
				{
					PartyInfo joinedParty2 = GetJoinedParty(partyId);
					string stringData2 = partyListenerEvent.StringData;
					if (stringData2 == "WTCG.Party.Type")
					{
						PartyType partyTypeFromString = GetPartyTypeFromString(GetPartyAttributeString(partyId, "WTCG.Party.Type"));
						if (partyTypeFromString != 0)
						{
							s_joinedParties[partyId] = partyTypeFromString;
						}
					}
					bnet.protocol.Variant variant2 = new bnet.protocol.Variant();
					switch (partyListenerEvent.UintData)
					{
					case 1u:
						variant2 = new bnet.protocol.Variant();
						variant2.IntValue = (long)partyListenerEvent.UlongData;
						break;
					case 2u:
						variant2 = new bnet.protocol.Variant();
						variant2.StringValue = partyListenerEvent.StringData2;
						break;
					case 3u:
						variant2 = new bnet.protocol.Variant();
						variant2.BlobValue = partyListenerEvent.BlobData;
						break;
					}
					if (BnetParty.OnPartyAttributeChanged != null)
					{
						BnetParty.OnPartyAttributeChanged(joinedParty2, stringData2, variant2);
					}
					if (s_attributeChangedSubscribers != null && s_attributeChangedSubscribers.TryGetValue(stringData2, out var value))
					{
						PartyAttributeChangedHandler[] array = value.ToArray();
						for (int j = 0; j < array.Length; j++)
						{
							array[j](joinedParty2, stringData2, variant2);
						}
					}
					break;
				}
				case PartyListenerEventType.MEMBER_ATTRIBUTE_CHANGED:
				{
					PartyInfo joinedParty = GetJoinedParty(partyId);
					string stringData = partyListenerEvent.StringData;
					bnet.protocol.Variant variant = new bnet.protocol.Variant();
					switch (partyListenerEvent.UintData)
					{
					case 1u:
						variant = new bnet.protocol.Variant();
						variant.IntValue = (long)partyListenerEvent.UlongData;
						break;
					case 2u:
						variant = new bnet.protocol.Variant();
						variant.StringValue = partyListenerEvent.StringData2;
						break;
					case 3u:
						variant = new bnet.protocol.Variant();
						variant.BlobValue = partyListenerEvent.BlobData;
						break;
					}
					if (BnetParty.OnMemberAttributeChanged != null)
					{
						BnetParty.OnMemberAttributeChanged(joinedParty, partyListenerEvent.SubjectMemberId, stringData, variant);
					}
					break;
				}
				}
			}
		}

		private static void RaisePartyError(bool isOperationCallback, string szPartyType, BnetFeatureEvent featureEvent, string errorMessageFormat, params object[] args)
		{
			string debugContext = string.Format(errorMessageFormat, args);
			PartyError error = default(PartyError);
			error.IsOperationCallback = isOperationCallback;
			error.DebugContext = debugContext;
			error.ErrorCode = BattleNetErrors.ERROR_OK;
			error.Feature = BnetFeature.Party;
			error.FeatureEvent = featureEvent;
			error.szPartyType = szPartyType;
			RaisePartyError(error);
		}

		private static void RaisePartyError(bool isOperationCallback, string debugContext, BattleNetErrors errorCode, BnetFeature feature, BnetFeatureEvent featureEvent, PartyId partyId, string szPartyType, string stringData, string errorMessageFormat, params object[] args)
		{
			if (BnetParty.OnError != null)
			{
				PartyError error = default(PartyError);
				error.IsOperationCallback = isOperationCallback;
				error.DebugContext = debugContext;
				error.ErrorCode = errorCode;
				error.Feature = feature;
				error.FeatureEvent = featureEvent;
				error.PartyId = partyId;
				error.szPartyType = szPartyType;
				error.StringData = stringData;
				RaisePartyError(error);
			}
		}

		private static void RaisePartyError(PartyError error)
		{
			string message = $"BnetParty: event={error.FeatureEvent.ToString()} feature={(int)error.FeatureEvent} code={error.ErrorCode} partyId={error.PartyId} type={error.szPartyType} strData={error.StringData}";
			LogLevel level = ((error.ErrorCode == BattleNetErrors.ERROR_OK) ? LogLevel.Info : LogLevel.Error);
			Log.Party.Print(level, message);
			if (BnetParty.OnError != null)
			{
				BnetParty.OnError(error);
			}
		}

		public static PartyType GetPartyTypeFromString(string partyType)
		{
			PartyType outVal = PartyType.DEFAULT;
			if (partyType != null)
			{
				EnumUtils.TryGetEnum<PartyType>(partyType, out outVal);
			}
			return outVal;
		}
	}
}
