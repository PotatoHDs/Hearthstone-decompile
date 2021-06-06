using System;
using System.Collections.Generic;
using System.Linq;
using bgs.types;
using bnet.protocol;
using bnet.protocol.account.v1;
using bnet.protocol.v2;

namespace bgs
{
	// Token: 0x02000242 RID: 578
	public static class BnetParty
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600240A RID: 9226 RVA: 0x0007EF40 File Offset: 0x0007D140
		// (remove) Token: 0x0600240B RID: 9227 RVA: 0x0007EF74 File Offset: 0x0007D174
		public static event BnetParty.PartyErrorHandler OnError;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600240C RID: 9228 RVA: 0x0007EFA8 File Offset: 0x0007D1A8
		// (remove) Token: 0x0600240D RID: 9229 RVA: 0x0007EFDC File Offset: 0x0007D1DC
		public static event BnetParty.JoinedHandler OnJoined;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600240E RID: 9230 RVA: 0x0007F010 File Offset: 0x0007D210
		// (remove) Token: 0x0600240F RID: 9231 RVA: 0x0007F044 File Offset: 0x0007D244
		public static event BnetParty.PrivacyLevelChangedHandler OnPrivacyLevelChanged;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06002410 RID: 9232 RVA: 0x0007F078 File Offset: 0x0007D278
		// (remove) Token: 0x06002411 RID: 9233 RVA: 0x0007F0AC File Offset: 0x0007D2AC
		public static event BnetParty.MemberEventHandler OnMemberEvent;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06002412 RID: 9234 RVA: 0x0007F0E0 File Offset: 0x0007D2E0
		// (remove) Token: 0x06002413 RID: 9235 RVA: 0x0007F114 File Offset: 0x0007D314
		public static event BnetParty.ReceivedInviteHandler OnReceivedInvite;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06002414 RID: 9236 RVA: 0x0007F148 File Offset: 0x0007D348
		// (remove) Token: 0x06002415 RID: 9237 RVA: 0x0007F17C File Offset: 0x0007D37C
		public static event BnetParty.SentInviteHandler OnSentInvite;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06002416 RID: 9238 RVA: 0x0007F1B0 File Offset: 0x0007D3B0
		// (remove) Token: 0x06002417 RID: 9239 RVA: 0x0007F1E4 File Offset: 0x0007D3E4
		public static event BnetParty.ReceivedInviteRequestHandler OnReceivedInviteRequest;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06002418 RID: 9240 RVA: 0x0007F218 File Offset: 0x0007D418
		// (remove) Token: 0x06002419 RID: 9241 RVA: 0x0007F24C File Offset: 0x0007D44C
		public static event BnetParty.ChatMessageHandler OnChatMessage;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600241A RID: 9242 RVA: 0x0007F280 File Offset: 0x0007D480
		// (remove) Token: 0x0600241B RID: 9243 RVA: 0x0007F2B4 File Offset: 0x0007D4B4
		public static event BnetParty.PartyAttributeChangedHandler OnPartyAttributeChanged;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600241C RID: 9244 RVA: 0x0007F2E8 File Offset: 0x0007D4E8
		// (remove) Token: 0x0600241D RID: 9245 RVA: 0x0007F31C File Offset: 0x0007D51C
		public static event BnetParty.MemberAttributeChangedHandler OnMemberAttributeChanged;

		// Token: 0x0600241E RID: 9246 RVA: 0x0007F350 File Offset: 0x0007D550
		public static void RegisterAttributeChangedHandler(string attributeKey, BnetParty.PartyAttributeChangedHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (BnetParty.s_attributeChangedSubscribers == null)
			{
				BnetParty.s_attributeChangedSubscribers = new Map<string, List<BnetParty.PartyAttributeChangedHandler>>();
			}
			List<BnetParty.PartyAttributeChangedHandler> list;
			if (!BnetParty.s_attributeChangedSubscribers.TryGetValue(attributeKey, out list))
			{
				list = new List<BnetParty.PartyAttributeChangedHandler>();
				BnetParty.s_attributeChangedSubscribers[attributeKey] = list;
			}
			if (!list.Contains(handler))
			{
				list.Add(handler);
			}
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x0007F3B0 File Offset: 0x0007D5B0
		public static bool UnregisterAttributeChangedHandler(string attributeKey, BnetParty.PartyAttributeChangedHandler handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (BnetParty.s_attributeChangedSubscribers == null)
			{
				return false;
			}
			List<BnetParty.PartyAttributeChangedHandler> list = null;
			return BnetParty.s_attributeChangedSubscribers.TryGetValue(attributeKey, out list) && list.Remove(handler);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x0007F3EE File Offset: 0x0007D5EE
		public static bool IsInParty(PartyId partyId)
		{
			return !(partyId == null) && BnetParty.s_joinedParties.ContainsKey(partyId);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x0007F406 File Offset: 0x0007D606
		public static PartyId[] GetJoinedPartyIds()
		{
			return BnetParty.s_joinedParties.Keys.ToArray<PartyId>();
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x0007F417 File Offset: 0x0007D617
		public static PartyInfo[] GetJoinedParties()
		{
			return (from kv in BnetParty.s_joinedParties
			select new PartyInfo(kv.Key, kv.Value)).ToArray<PartyInfo>();
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x0007F448 File Offset: 0x0007D648
		public static PartyInfo GetJoinedParty(PartyId partyId)
		{
			if (partyId == null)
			{
				return null;
			}
			PartyType type = PartyType.DEFAULT;
			if (BnetParty.s_joinedParties.TryGetValue(partyId, out type))
			{
				return new PartyInfo(partyId, type);
			}
			return null;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x0007F47C File Offset: 0x0007D67C
		public static PartyType GetPartyType(PartyId partyId)
		{
			PartyType result = PartyType.DEFAULT;
			if (partyId == null)
			{
				return result;
			}
			BnetParty.s_joinedParties.TryGetValue(partyId, out result);
			return result;
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x0007F4A8 File Offset: 0x0007D6A8
		public static bool IsLeader(PartyId partyId)
		{
			if (partyId == null)
			{
				return false;
			}
			PartyMember myselfMember = BnetParty.GetMyselfMember(partyId);
			if (myselfMember != null)
			{
				PartyType partyType = BnetParty.GetPartyType(partyId);
				if (myselfMember.IsLeader(partyType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x0007F4DD File Offset: 0x0007D6DD
		public static bool IsMember(PartyId partyId, BnetGameAccountId memberId)
		{
			return !(partyId == null) && BnetParty.GetMember(partyId, memberId) != null;
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x0007F4F4 File Offset: 0x0007D6F4
		public static bool IsPartyFull(PartyId partyId, bool includeInvites = true)
		{
			if (partyId == null)
			{
				return false;
			}
			int num = BnetParty.CountMembers(partyId);
			int num2 = includeInvites ? BnetParty.GetSentInvites(partyId).Length : 0;
			int maxPartyMembers = BattleNet.GetMaxPartyMembers(partyId.ToEntityId());
			return num + num2 >= maxPartyMembers;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x0007F535 File Offset: 0x0007D735
		public static int CountMembers(PartyId partyId)
		{
			if (partyId == null)
			{
				return 0;
			}
			return BattleNet.GetCountPartyMembers(partyId.ToEntityId());
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x0007F54D File Offset: 0x0007D74D
		public static PrivacyLevel GetPrivacyLevel(PartyId partyId)
		{
			if (partyId == null)
			{
				return PrivacyLevel.CLOSED;
			}
			return (PrivacyLevel)BattleNet.GetPartyPrivacy(partyId.ToEntityId());
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x0007F568 File Offset: 0x0007D768
		public static PartyMember GetMember(PartyId partyId, BnetGameAccountId memberId)
		{
			foreach (PartyMember partyMember in BnetParty.GetMembers(partyId))
			{
				if (partyMember.GameAccountId == memberId)
				{
					return partyMember;
				}
			}
			return null;
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x0007F5A0 File Offset: 0x0007D7A0
		public static PartyMember GetLeader(PartyId partyId)
		{
			if (partyId == null)
			{
				return null;
			}
			PartyMember[] members = BnetParty.GetMembers(partyId);
			PartyType partyType = BnetParty.GetPartyType(partyId);
			foreach (PartyMember partyMember in members)
			{
				if (partyMember.IsLeader(partyType))
				{
					return partyMember;
				}
			}
			return null;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x0007F5E4 File Offset: 0x0007D7E4
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
			return BnetParty.GetMember(partyId, bnetGameAccountId);
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x0007F61C File Offset: 0x0007D81C
		public static PartyMember[] GetMembers(PartyId partyId)
		{
			if (partyId == null)
			{
				return new PartyMember[0];
			}
			PartyMember[] array;
			BattleNet.GetPartyMembers(partyId.ToEntityId(), out array);
			PartyMember[] array2 = new PartyMember[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				PartyMember partyMember = array[i];
				array2[i] = new PartyMember
				{
					GameAccountId = BnetGameAccountId.CreateFromEntityId(partyMember.memberGameAccountId),
					RoleIds = new uint[]
					{
						partyMember.firstMemberRole
					},
					BattleTag = partyMember.battleTag,
					VoiceId = partyMember.voiceId
				};
			}
			return array2;
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x0007F6B4 File Offset: 0x0007D8B4
		public static PartyInvite GetReceivedInvite(ulong inviteId)
		{
			return BnetParty.GetReceivedInvites().FirstOrDefault((PartyInvite i) => i.InviteId == inviteId);
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0007F6E4 File Offset: 0x0007D8E4
		public static PartyInvite GetReceivedInviteFrom(BnetGameAccountId inviterId, PartyType partyType)
		{
			return BnetParty.GetReceivedInvites().FirstOrDefault((PartyInvite i) => i.InviterId == inviterId && i.PartyType == partyType);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x0007F71C File Offset: 0x0007D91C
		public static PartyInvite[] GetReceivedInvites()
		{
			PartyInvite[] result;
			BattleNet.GetReceivedPartyInvites(out result);
			return result;
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x0007F734 File Offset: 0x0007D934
		public static PartyInvite GetSentInvite(PartyId partyId, ulong inviteId)
		{
			if (partyId == null)
			{
				return null;
			}
			return BnetParty.GetSentInvites(partyId).FirstOrDefault((PartyInvite i) => i.InviteId == inviteId);
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0007F770 File Offset: 0x0007D970
		public static PartyInvite[] GetSentInvites(PartyId partyId)
		{
			if (partyId == null)
			{
				return new PartyInvite[0];
			}
			PartyInvite[] result;
			BattleNet.GetPartySentInvites(partyId.ToEntityId(), out result);
			return result;
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x0007F79C File Offset: 0x0007D99C
		public static InviteRequest[] GetInviteRequests(PartyId partyId)
		{
			if (partyId == null)
			{
				return new InviteRequest[0];
			}
			InviteRequest[] result;
			BattleNet.GetPartyInviteRequests(partyId.ToEntityId(), out result);
			return result;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x0007F7C8 File Offset: 0x0007D9C8
		public static KeyValuePair<string, object>[] GetAllPartyAttributes(PartyId partyId)
		{
			if (partyId == null)
			{
				return new KeyValuePair<string, object>[0];
			}
			string[] array;
			BattleNet.GetAllPartyAttributes(partyId.ToEntityId(), out array);
			KeyValuePair<string, object>[] array2 = new KeyValuePair<string, object>[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array[i];
				object value = null;
				long? partyAttributeLong = BnetParty.GetPartyAttributeLong(partyId, text);
				if (partyAttributeLong != null)
				{
					value = partyAttributeLong;
				}
				string partyAttributeString = BnetParty.GetPartyAttributeString(partyId, text);
				if (partyAttributeString != null)
				{
					value = partyAttributeString;
				}
				byte[] partyAttributeBlob = BnetParty.GetPartyAttributeBlob(partyId, text);
				if (partyAttributeBlob != null)
				{
					value = partyAttributeBlob;
				}
				array2[i] = new KeyValuePair<string, object>(text, value);
			}
			return array2;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x0007F860 File Offset: 0x0007DA60
		public static bnet.protocol.Attribute[] GetAllPartyAttributesVariant(PartyId partyId)
		{
			if (partyId == null)
			{
				return new bnet.protocol.Attribute[0];
			}
			string[] array;
			BattleNet.GetAllPartyAttributes(partyId.ToEntityId(), out array);
			bnet.protocol.Attribute[] array2 = new bnet.protocol.Attribute[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array[i];
				bnet.protocol.Variant partyAttributeVariant = BnetParty.GetPartyAttributeVariant(partyId, text);
				bnet.protocol.Attribute attribute = new bnet.protocol.Attribute();
				attribute.SetName(text);
				attribute.SetValue(partyAttributeVariant);
				array2[i] = attribute;
			}
			return array2;
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x0007F8CC File Offset: 0x0007DACC
		public static bnet.protocol.Variant GetPartyAttributeVariant(PartyId partyId, string attributeKey)
		{
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			bgs.types.EntityId partyId2 = partyId.ToEntityId();
			long intValue;
			if (BattleNet.GetPartyAttributeLong(partyId2, attributeKey, out intValue))
			{
				variant.IntValue = intValue;
			}
			else
			{
				string text;
				BattleNet.GetPartyAttributeString(partyId2, attributeKey, out text);
				if (text != null)
				{
					variant.StringValue = text;
				}
				else
				{
					byte[] array;
					BattleNet.GetPartyAttributeBlob(partyId2, attributeKey, out array);
					if (array != null)
					{
						variant.BlobValue = array;
					}
				}
			}
			return variant;
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x0007F928 File Offset: 0x0007DB28
		public static long? GetPartyAttributeLong(PartyId partyId, string attributeKey)
		{
			if (partyId == null)
			{
				return null;
			}
			long value;
			if (BattleNet.GetPartyAttributeLong(partyId.ToEntityId(), attributeKey, out value))
			{
				return new long?(value);
			}
			return null;
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x0007F968 File Offset: 0x0007DB68
		public static string GetPartyAttributeString(PartyId partyId, string attributeKey)
		{
			if (partyId == null)
			{
				return null;
			}
			string result;
			BattleNet.GetPartyAttributeString(partyId.ToEntityId(), attributeKey, out result);
			return result;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x0007F990 File Offset: 0x0007DB90
		public static byte[] GetPartyAttributeBlob(PartyId partyId, string attributeKey)
		{
			if (partyId == null)
			{
				return null;
			}
			byte[] result;
			BattleNet.GetPartyAttributeBlob(partyId.ToEntityId(), attributeKey, out result);
			return result;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x0007F9B8 File Offset: 0x0007DBB8
		public static string GetMemberAttributeString(PartyId partyId, GameAccountHandle partyMember, string attributeKey)
		{
			if (partyId == null)
			{
				return null;
			}
			string result;
			BattleNet.GetMemberAttributeString(partyId.ToEntityId(), partyMember, attributeKey, out result);
			return result;
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x0007F9E0 File Offset: 0x0007DBE0
		public static byte[] GetMemberAttributeBlob(PartyId partyId, GameAccountHandle partyMember, string attributeKey)
		{
			if (partyId == null)
			{
				return null;
			}
			byte[] result;
			BattleNet.GetMemberAttributeBlob(partyId.ToEntityId(), partyMember, attributeKey, out result);
			return result;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x0007FA08 File Offset: 0x0007DC08
		public static void SetDisconnectedFromBattleNet()
		{
			BnetParty.s_joinedParties.Clear();
			if (BnetParty.s_pendingPartyCreates != null)
			{
				BnetParty.s_pendingPartyCreates.Clear();
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x0007FA28 File Offset: 0x0007DC28
		public static void CreateParty(PartyType partyType, PrivacyLevel privacyLevel, byte[] creatorBlob, BnetParty.CreateSuccessCallback successCallback)
		{
			string @string = EnumUtils.GetString<PartyType>(partyType);
			if (BnetParty.s_pendingPartyCreates != null && BnetParty.s_pendingPartyCreates.ContainsKey(partyType))
			{
				BnetParty.RaisePartyError(true, @string, BnetFeatureEvent.Party_Create_Callback, "CreateParty: Already creating party of type {0}", new object[]
				{
					partyType
				});
				return;
			}
			if (BnetParty.s_pendingPartyCreates == null)
			{
				BnetParty.s_pendingPartyCreates = new Map<PartyType, BnetParty.CreateSuccessCallback>();
			}
			BnetParty.s_pendingPartyCreates[partyType] = successCallback;
			bnet.protocol.v2.Attribute attribute = ProtocolHelper.CreateAttributeV2("WTCG.Party.Creator", creatorBlob);
			BattleNet.CreateParty(@string, (int)privacyLevel, new bnet.protocol.v2.Attribute[]
			{
				attribute
			});
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x0007FAA8 File Offset: 0x0007DCA8
		public static void CreateParty(PartyType partyType, PrivacyLevel privacyLevel, BnetParty.CreateSuccessCallback successCallback, params bnet.protocol.v2.Attribute[] partyAttributes)
		{
			string @string = EnumUtils.GetString<PartyType>(partyType);
			if (BnetParty.s_pendingPartyCreates != null && BnetParty.s_pendingPartyCreates.ContainsKey(partyType))
			{
				BnetParty.RaisePartyError(true, @string, BnetFeatureEvent.Party_Create_Callback, "CreateParty: Already creating party of type {0}", new object[]
				{
					partyType
				});
				return;
			}
			if (BnetParty.s_pendingPartyCreates == null)
			{
				BnetParty.s_pendingPartyCreates = new Map<PartyType, BnetParty.CreateSuccessCallback>();
			}
			BnetParty.s_pendingPartyCreates[partyType] = successCallback;
			BattleNet.CreateParty(@string, (int)privacyLevel, partyAttributes);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x0007FB13 File Offset: 0x0007DD13
		public static void JoinParty(PartyId partyId, PartyType partyType)
		{
			BattleNet.JoinParty(partyId.ToEntityId(), EnumUtils.GetString<PartyType>(partyType));
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x0007FB26 File Offset: 0x0007DD26
		public static void Leave(PartyId partyId)
		{
			if (!BnetParty.IsInParty(partyId))
			{
				return;
			}
			BattleNet.LeaveParty(partyId.ToEntityId());
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x0007FB3C File Offset: 0x0007DD3C
		public static void DissolveParty(PartyId partyId)
		{
			if (!BnetParty.IsInParty(partyId))
			{
				return;
			}
			BattleNet.DissolveParty(partyId.ToEntityId());
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x0007FB52 File Offset: 0x0007DD52
		public static void SetPrivacy(PartyId partyId, PrivacyLevel privacyLevel)
		{
			if (!BnetParty.IsInParty(partyId))
			{
				return;
			}
			BattleNet.SetPartyPrivacy(partyId.ToEntityId(), (int)privacyLevel);
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x0007FB6C File Offset: 0x0007DD6C
		public static void SetLeader(PartyId partyId, BnetGameAccountId memberId)
		{
			if (!BnetParty.IsInParty(partyId))
			{
				return;
			}
			bgs.types.EntityId partyId2 = partyId.ToEntityId();
			bgs.types.EntityId memberId2 = BnetEntityId.CreateEntityId(memberId);
			uint leaderRoleId = PartyMember.GetLeaderRoleId(BnetParty.GetPartyType(partyId));
			BattleNet.AssignPartyRole(partyId2, memberId2, leaderRoleId);
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x0007FBA4 File Offset: 0x0007DDA4
		public static void SendInvite(PartyId toWhichPartyId, BnetGameAccountId recipientId, bool isReservation)
		{
			if (!BnetParty.IsInParty(toWhichPartyId))
			{
				return;
			}
			bgs.types.EntityId partyId = toWhichPartyId.ToEntityId();
			bgs.types.EntityId inviteeId = BnetEntityId.CreateEntityId(recipientId);
			BattleNet.SendPartyInvite(partyId, inviteeId, isReservation);
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x0007FBCE File Offset: 0x0007DDCE
		public static void AcceptReceivedInvite(ulong inviteId)
		{
			BattleNet.AcceptPartyInvite(inviteId);
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x0007FBD6 File Offset: 0x0007DDD6
		public static void DeclineReceivedInvite(ulong inviteId)
		{
			BattleNet.DeclinePartyInvite(inviteId);
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x0007FBDE File Offset: 0x0007DDDE
		public static void RevokeSentInvite(PartyId partyId, ulong inviteId)
		{
			if (!BnetParty.IsInParty(partyId))
			{
				return;
			}
			BattleNet.RevokePartyInvite(partyId.ToEntityId(), inviteId);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x0007FBF8 File Offset: 0x0007DDF8
		public static void RequestInvite(PartyId partyId, BnetGameAccountId whomToAskForApproval, BnetGameAccountId whomToInvite, PartyType partyType)
		{
			if (BnetParty.IsLeader(partyId))
			{
				PartyError error = default(PartyError);
				error.IsOperationCallback = true;
				error.DebugContext = "RequestInvite";
				error.ErrorCode = BattleNetErrors.ERROR_INVALID_TARGET_ID;
				error.Feature = BnetFeature.Party;
				error.FeatureEvent = BnetFeatureEvent.Party_RequestPartyInvite_Callback;
				error.PartyId = partyId;
				error.szPartyType = EnumUtils.GetString<PartyType>(partyType);
				error.StringData = "leaders cannot RequestInvite - use SendInvite instead.";
				BnetParty.OnError(error);
				return;
			}
			bgs.types.EntityId partyId2 = partyId.ToEntityId();
			bgs.types.EntityId whomToAskForApproval2 = BnetEntityId.CreateEntityId(whomToAskForApproval);
			bgs.types.EntityId whomToInvite2 = BnetEntityId.CreateEntityId(whomToInvite);
			string @string = EnumUtils.GetString<PartyType>(partyType);
			BattleNet.RequestPartyInvite(partyId2, whomToAskForApproval2, whomToInvite2, @string);
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x0007FC98 File Offset: 0x0007DE98
		public static void AcceptInviteRequest(PartyId partyId, BnetGameAccountId requestedTargetId, bool isReservation)
		{
			BnetParty.SendInvite(partyId, requestedTargetId, isReservation);
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x0007FCA4 File Offset: 0x0007DEA4
		public static void IgnoreInviteRequest(PartyId partyId, BnetGameAccountId requestedTargetId)
		{
			bgs.types.EntityId partyId2 = partyId.ToEntityId();
			bgs.types.EntityId requestedTargetId2 = BnetEntityId.CreateEntityId(requestedTargetId);
			BattleNet.IgnoreInviteRequest(partyId2, requestedTargetId2);
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x0007FCC4 File Offset: 0x0007DEC4
		public static void KickMember(PartyId partyId, BnetGameAccountId memberId)
		{
			if (!BnetParty.IsInParty(partyId))
			{
				return;
			}
			bgs.types.EntityId partyId2 = partyId.ToEntityId();
			bgs.types.EntityId memberId2 = BnetEntityId.CreateEntityId(memberId);
			BattleNet.KickPartyMember(partyId2, memberId2);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x0007FCED File Offset: 0x0007DEED
		public static void SendChatMessage(PartyId partyId, string chatMessage)
		{
			if (!BnetParty.IsInParty(partyId))
			{
				return;
			}
			BattleNet.SendPartyChatMessage(partyId.ToEntityId(), chatMessage);
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x0007FD04 File Offset: 0x0007DF04
		public static void ClearPartyAttribute(PartyId partyId, string attributeKey)
		{
			BattleNet.ClearPartyAttribute(partyId.ToEntityId(), attributeKey);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x0007FD12 File Offset: 0x0007DF12
		public static void SetPartyAttributeLong(PartyId partyId, string attributeKey, long value)
		{
			BattleNet.SetPartyAttributeLong(partyId.ToEntityId(), attributeKey, value);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x0007FD21 File Offset: 0x0007DF21
		public static void SetPartyAttributeString(PartyId partyId, string attributeKey, string value)
		{
			BattleNet.SetPartyAttributeString(partyId.ToEntityId(), attributeKey, value);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x0007FD30 File Offset: 0x0007DF30
		public static void SetPartyAttributeBlob(PartyId partyId, string attributeKey, byte[] value)
		{
			BattleNet.SetPartyAttributeBlob(partyId.ToEntityId(), attributeKey, value);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x0007FD3F File Offset: 0x0007DF3F
		public static void SetPartyAttributes(PartyId partyId, params bnet.protocol.v2.Attribute[] attrs)
		{
			BattleNet.SetPartyAttributes(partyId.ToEntityId(), attrs);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x0007FD4D File Offset: 0x0007DF4D
		public static void ClearMemberAttribute(PartyId partyId, GameAccountHandle partyMember, string attributeKey)
		{
			BattleNet.ClearMemberAttribute(partyId.ToEntityId(), partyMember, attributeKey);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x0007FD5C File Offset: 0x0007DF5C
		public static void SetMemberAttributeLong(PartyId partyId, GameAccountHandle partyMember, string attributeKey, long value)
		{
			BattleNet.SetMemberAttributeLong(partyId.ToEntityId(), partyMember, attributeKey, value);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x0007FD6C File Offset: 0x0007DF6C
		public static void SetMemberAttributeString(PartyId partyId, GameAccountHandle partyMember, string attributeKey, string value)
		{
			BattleNet.SetMemberAttributeString(partyId.ToEntityId(), partyMember, attributeKey, value);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0007FD7C File Offset: 0x0007DF7C
		public static void SetMemberAttributeBlob(PartyId partyId, GameAccountHandle partyMember, string attributeKey, byte[] value)
		{
			BattleNet.SetMemberAttributeBlob(partyId.ToEntityId(), partyMember, attributeKey, value);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0007FD8C File Offset: 0x0007DF8C
		public static void SetMemberAttributes(PartyId partyId, GameAccountHandle partyMember, params bnet.protocol.v2.Attribute[] attrs)
		{
			BattleNet.SetMemberAttributes(partyId.ToEntityId(), partyMember, attrs);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0007FD9C File Offset: 0x0007DF9C
		public static void RemoveFromAllEventHandlers(object targetObject)
		{
			Type right = (targetObject == null) ? null : targetObject.GetType();
			if (BnetParty.OnError != null)
			{
				foreach (object obj in (BnetParty.OnError.GetInvocationList().Clone() as Array))
				{
					Delegate @delegate = (Delegate)obj;
					if (@delegate.Target == targetObject || (@delegate.Target == null && @delegate.Method.DeclaringType == right))
					{
						BnetParty.OnError -= (BnetParty.PartyErrorHandler)@delegate;
					}
				}
			}
			if (BnetParty.OnJoined != null)
			{
				foreach (object obj2 in (BnetParty.OnJoined.GetInvocationList().Clone() as Array))
				{
					Delegate delegate2 = (Delegate)obj2;
					if (delegate2.Target == targetObject || (delegate2.Target == null && delegate2.Method.DeclaringType == right))
					{
						BnetParty.OnJoined -= (BnetParty.JoinedHandler)delegate2;
					}
				}
			}
			if (BnetParty.OnPrivacyLevelChanged != null)
			{
				foreach (object obj3 in (BnetParty.OnPrivacyLevelChanged.GetInvocationList().Clone() as Array))
				{
					Delegate delegate3 = (Delegate)obj3;
					if (delegate3.Target == targetObject || (delegate3.Target == null && delegate3.Method.DeclaringType == right))
					{
						BnetParty.OnPrivacyLevelChanged -= (BnetParty.PrivacyLevelChangedHandler)delegate3;
					}
				}
			}
			if (BnetParty.OnMemberEvent != null)
			{
				foreach (object obj4 in (BnetParty.OnMemberEvent.GetInvocationList().Clone() as Array))
				{
					Delegate delegate4 = (Delegate)obj4;
					if (delegate4.Target == targetObject || (delegate4.Target == null && delegate4.Method.DeclaringType == right))
					{
						BnetParty.OnMemberEvent -= (BnetParty.MemberEventHandler)delegate4;
					}
				}
			}
			if (BnetParty.OnReceivedInvite != null)
			{
				foreach (object obj5 in (BnetParty.OnReceivedInvite.GetInvocationList().Clone() as Array))
				{
					Delegate delegate5 = (Delegate)obj5;
					if (delegate5.Target == targetObject || (delegate5.Target == null && delegate5.Method.DeclaringType == right))
					{
						BnetParty.OnReceivedInvite -= (BnetParty.ReceivedInviteHandler)delegate5;
					}
				}
			}
			if (BnetParty.OnSentInvite != null)
			{
				foreach (object obj6 in (BnetParty.OnSentInvite.GetInvocationList().Clone() as Array))
				{
					Delegate delegate6 = (Delegate)obj6;
					if (delegate6.Target == targetObject || (delegate6.Target == null && delegate6.Method.DeclaringType == right))
					{
						BnetParty.OnSentInvite -= (BnetParty.SentInviteHandler)delegate6;
					}
				}
			}
			if (BnetParty.OnReceivedInviteRequest != null)
			{
				foreach (object obj7 in (BnetParty.OnReceivedInviteRequest.GetInvocationList().Clone() as Array))
				{
					Delegate delegate7 = (Delegate)obj7;
					if (delegate7.Target == targetObject || (delegate7.Target == null && delegate7.Method.DeclaringType == right))
					{
						BnetParty.OnReceivedInviteRequest -= (BnetParty.ReceivedInviteRequestHandler)delegate7;
					}
				}
			}
			if (BnetParty.OnChatMessage != null)
			{
				foreach (object obj8 in (BnetParty.OnChatMessage.GetInvocationList().Clone() as Array))
				{
					Delegate delegate8 = (Delegate)obj8;
					if (delegate8.Target == targetObject || (delegate8.Target == null && delegate8.Method.DeclaringType == right))
					{
						BnetParty.OnChatMessage -= (BnetParty.ChatMessageHandler)delegate8;
					}
				}
			}
			if (BnetParty.OnPartyAttributeChanged != null)
			{
				foreach (object obj9 in (BnetParty.OnPartyAttributeChanged.GetInvocationList().Clone() as Array))
				{
					Delegate delegate9 = (Delegate)obj9;
					if (delegate9.Target == targetObject || (delegate9.Target == null && delegate9.Method.DeclaringType == right))
					{
						BnetParty.OnPartyAttributeChanged -= (BnetParty.PartyAttributeChangedHandler)delegate9;
					}
				}
			}
			if (BnetParty.OnMemberAttributeChanged != null)
			{
				foreach (object obj10 in (BnetParty.OnMemberAttributeChanged.GetInvocationList().Clone() as Array))
				{
					Delegate delegate10 = (Delegate)obj10;
					if (delegate10.Target == targetObject || (delegate10.Target == null && delegate10.Method.DeclaringType == right))
					{
						BnetParty.OnMemberAttributeChanged -= (BnetParty.MemberAttributeChangedHandler)delegate10;
					}
				}
			}
			if (BnetParty.s_attributeChangedSubscribers != null)
			{
				foreach (KeyValuePair<string, List<BnetParty.PartyAttributeChangedHandler>> keyValuePair in BnetParty.s_attributeChangedSubscribers)
				{
					var array = keyValuePair.Value.Select((BnetParty.PartyAttributeChangedHandler h, int idx) => new
					{
						Handler = h,
						Index = idx
					}).ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						var <>f__AnonymousType = array[i];
						if (<>f__AnonymousType.Handler.Target == targetObject || <>f__AnonymousType.Handler.Method.DeclaringType == right)
						{
							keyValuePair.Value.RemoveAt(<>f__AnonymousType.Index);
						}
					}
				}
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x000803EC File Offset: 0x0007E5EC
		private static bool IsIgnorableError(BnetFeatureEvent feature, BattleNetErrors code)
		{
			HashSet<BattleNetErrors> hashSet;
			return BnetParty.s_ignorableErrorCodes.TryGetValue(feature, out hashSet) && hashSet.Contains(code);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00080414 File Offset: 0x0007E614
		public static void Process()
		{
			PartyListenerEvent[] array;
			BattleNet.GetPartyListenerEvents(out array);
			BattleNet.ClearPartyListenerEvents();
			int i = 0;
			while (i < array.Length)
			{
				PartyListenerEvent partyListenerEvent = array[i];
				PartyId partyId = partyListenerEvent.PartyId;
				switch (partyListenerEvent.Type)
				{
				case PartyListenerEventType.ERROR_RAISED:
				case PartyListenerEventType.OPERATION_CALLBACK:
				{
					PartyError partyError = partyListenerEvent.ToPartyError();
					if (partyError.ErrorCode != BattleNetErrors.ERROR_OK)
					{
						if (BnetParty.IsIgnorableError(partyError.FeatureEvent, partyError.ErrorCode.EnumVal))
						{
							partyError.ErrorCode = BattleNetErrors.ERROR_OK;
							if (partyError.FeatureEvent == BnetFeatureEvent.Party_Leave_Callback)
							{
								if (!BnetParty.s_joinedParties.ContainsKey(partyId))
								{
									BnetParty.s_joinedParties[partyId] = PartyType.SPECTATOR_PARTY;
									goto IL_204;
								}
								goto IL_204;
							}
						}
						if (partyError.IsOperationCallback && partyError.FeatureEvent == BnetFeatureEvent.Party_Create_Callback)
						{
							PartyType partyType = partyError.PartyType;
							if (BnetParty.s_pendingPartyCreates.ContainsKey(partyType))
							{
								BnetParty.s_pendingPartyCreates.Remove(partyType);
							}
						}
					}
					if (partyError.ErrorCode != BattleNetErrors.ERROR_OK)
					{
						BnetParty.RaisePartyError(partyError);
					}
					break;
				}
				case PartyListenerEventType.JOINED_PARTY:
				{
					string stringData = partyListenerEvent.StringData;
					PartyType partyTypeFromString = BnetParty.GetPartyTypeFromString(stringData);
					BnetParty.s_joinedParties[partyId] = partyTypeFromString;
					if (BnetParty.s_pendingPartyCreates != null)
					{
						BnetParty.CreateSuccessCallback createSuccessCallback = null;
						if (BnetParty.s_pendingPartyCreates.ContainsKey(partyTypeFromString))
						{
							createSuccessCallback = BnetParty.s_pendingPartyCreates[partyTypeFromString];
							BnetParty.s_pendingPartyCreates.Remove(partyTypeFromString);
						}
						else if (stringData == "default" && BnetParty.s_pendingPartyCreates.Count == 0)
						{
							createSuccessCallback = BnetParty.s_pendingPartyCreates.First<KeyValuePair<PartyType, BnetParty.CreateSuccessCallback>>().Value;
							BnetParty.s_pendingPartyCreates.Clear();
						}
						if (createSuccessCallback != null)
						{
							createSuccessCallback(partyTypeFromString, partyId);
						}
					}
					if (BnetParty.OnJoined != null)
					{
						BnetParty.OnJoined(OnlineEventType.ADDED, new PartyInfo(partyId, partyTypeFromString), null);
					}
					break;
				}
				case PartyListenerEventType.LEFT_PARTY:
					goto IL_204;
				case PartyListenerEventType.PRIVACY_CHANGED:
					if (BnetParty.OnPrivacyLevelChanged != null)
					{
						BnetParty.OnPrivacyLevelChanged(BnetParty.GetJoinedParty(partyId), (PrivacyLevel)partyListenerEvent.UintData);
					}
					break;
				case PartyListenerEventType.MEMBER_JOINED:
				case PartyListenerEventType.MEMBER_LEFT:
					if (BnetParty.OnMemberEvent != null)
					{
						OnlineEventType evt = (partyListenerEvent.Type == PartyListenerEventType.MEMBER_JOINED) ? OnlineEventType.ADDED : OnlineEventType.REMOVED;
						LeaveReason? reason = null;
						if (partyListenerEvent.Type == PartyListenerEventType.MEMBER_LEFT)
						{
							reason = new LeaveReason?((LeaveReason)partyListenerEvent.UintData);
						}
						BnetParty.OnMemberEvent(evt, BnetParty.GetJoinedParty(partyId), partyListenerEvent.SubjectMemberId, false, reason);
					}
					break;
				case PartyListenerEventType.MEMBER_ROLE_CHANGED:
					if (BnetParty.OnMemberEvent != null)
					{
						BnetParty.OnMemberEvent(OnlineEventType.UPDATED, BnetParty.GetJoinedParty(partyId), partyListenerEvent.SubjectMemberId, true, null);
					}
					break;
				case PartyListenerEventType.RECEIVED_INVITE_ADDED:
				case PartyListenerEventType.RECEIVED_INVITE_REMOVED:
					if (BnetParty.OnReceivedInvite != null)
					{
						OnlineEventType evt2 = (partyListenerEvent.Type == PartyListenerEventType.RECEIVED_INVITE_ADDED) ? OnlineEventType.ADDED : OnlineEventType.REMOVED;
						PartyType type = PartyType.DEFAULT;
						if (partyListenerEvent.StringData != null)
						{
							EnumUtils.TryGetEnum<PartyType>(partyListenerEvent.StringData, out type);
						}
						PartyInfo party = new PartyInfo(partyId, type);
						InviteRemoveReason? reason2 = null;
						if (partyListenerEvent.Type == PartyListenerEventType.RECEIVED_INVITE_REMOVED)
						{
							reason2 = new InviteRemoveReason?((InviteRemoveReason)partyListenerEvent.UintData);
						}
						BnetParty.OnReceivedInvite(evt2, party, partyListenerEvent.UlongData, partyListenerEvent.SubjectMemberId, partyListenerEvent.TargetMemberId, reason2);
					}
					break;
				case PartyListenerEventType.PARTY_INVITE_SENT:
				case PartyListenerEventType.PARTY_INVITE_REMOVED:
					if (BnetParty.OnSentInvite != null)
					{
						bool senderIsMyself = partyListenerEvent.SubjectMemberId == BnetGameAccountId.CreateFromEntityId(BattleNet.GetMyGameAccountId());
						OnlineEventType evt3 = (partyListenerEvent.Type == PartyListenerEventType.PARTY_INVITE_SENT) ? OnlineEventType.ADDED : OnlineEventType.REMOVED;
						PartyType type2 = PartyType.DEFAULT;
						if (partyListenerEvent.StringData != null)
						{
							EnumUtils.TryGetEnum<PartyType>(partyListenerEvent.StringData, out type2);
						}
						PartyInfo party2 = new PartyInfo(partyId, type2);
						InviteRemoveReason? reason3 = null;
						if (partyListenerEvent.Type == PartyListenerEventType.PARTY_INVITE_REMOVED)
						{
							reason3 = new InviteRemoveReason?((InviteRemoveReason)partyListenerEvent.UintData);
						}
						BnetParty.OnSentInvite(evt3, party2, partyListenerEvent.UlongData, partyListenerEvent.SubjectMemberId, partyListenerEvent.TargetMemberId, senderIsMyself, reason3);
					}
					break;
				case PartyListenerEventType.INVITE_REQUEST_ADDED:
				case PartyListenerEventType.INVITE_REQUEST_REMOVED:
					if (BnetParty.OnReceivedInviteRequest != null)
					{
						OnlineEventType evt4 = (partyListenerEvent.Type == PartyListenerEventType.INVITE_REQUEST_ADDED) ? OnlineEventType.ADDED : OnlineEventType.REMOVED;
						PartyInfo joinedParty = BnetParty.GetJoinedParty(partyId);
						InviteRequestRemovedReason? reason4 = null;
						if (partyListenerEvent.Type == PartyListenerEventType.INVITE_REQUEST_REMOVED)
						{
							reason4 = new InviteRequestRemovedReason?((InviteRequestRemovedReason)partyListenerEvent.UintData);
						}
						InviteRequest inviteRequest = new InviteRequest();
						inviteRequest.TargetId = partyListenerEvent.TargetMemberId;
						inviteRequest.TargetName = partyListenerEvent.StringData2;
						inviteRequest.RequesterId = partyListenerEvent.SubjectMemberId;
						inviteRequest.RequesterName = partyListenerEvent.StringData;
						BnetParty.OnReceivedInviteRequest(evt4, joinedParty, inviteRequest, reason4);
					}
					break;
				case PartyListenerEventType.CHAT_MESSAGE_RECEIVED:
					if (BnetParty.OnChatMessage != null)
					{
						BnetParty.OnChatMessage(BnetParty.GetJoinedParty(partyId), partyListenerEvent.SubjectMemberId, partyListenerEvent.StringData);
					}
					break;
				case PartyListenerEventType.PARTY_ATTRIBUTE_CHANGED:
				{
					PartyInfo joinedParty2 = BnetParty.GetJoinedParty(partyId);
					string stringData2 = partyListenerEvent.StringData;
					if (stringData2 == "WTCG.Party.Type")
					{
						PartyType partyTypeFromString2 = BnetParty.GetPartyTypeFromString(BnetParty.GetPartyAttributeString(partyId, "WTCG.Party.Type"));
						if (partyTypeFromString2 != PartyType.DEFAULT)
						{
							BnetParty.s_joinedParties[partyId] = partyTypeFromString2;
						}
					}
					bnet.protocol.Variant variant = new bnet.protocol.Variant();
					switch (partyListenerEvent.UintData)
					{
					case 1U:
						variant = new bnet.protocol.Variant();
						variant.IntValue = (long)partyListenerEvent.UlongData;
						break;
					case 2U:
						variant = new bnet.protocol.Variant();
						variant.StringValue = partyListenerEvent.StringData2;
						break;
					case 3U:
						variant = new bnet.protocol.Variant();
						variant.BlobValue = partyListenerEvent.BlobData;
						break;
					}
					if (BnetParty.OnPartyAttributeChanged != null)
					{
						BnetParty.OnPartyAttributeChanged(joinedParty2, stringData2, variant);
					}
					List<BnetParty.PartyAttributeChangedHandler> list;
					if (BnetParty.s_attributeChangedSubscribers != null && BnetParty.s_attributeChangedSubscribers.TryGetValue(stringData2, out list))
					{
						BnetParty.PartyAttributeChangedHandler[] array2 = list.ToArray();
						for (int j = 0; j < array2.Length; j++)
						{
							array2[j](joinedParty2, stringData2, variant);
						}
					}
					break;
				}
				case PartyListenerEventType.MEMBER_ATTRIBUTE_CHANGED:
				{
					PartyInfo joinedParty3 = BnetParty.GetJoinedParty(partyId);
					string stringData3 = partyListenerEvent.StringData;
					bnet.protocol.Variant variant2 = new bnet.protocol.Variant();
					switch (partyListenerEvent.UintData)
					{
					case 1U:
						variant2 = new bnet.protocol.Variant();
						variant2.IntValue = (long)partyListenerEvent.UlongData;
						break;
					case 2U:
						variant2 = new bnet.protocol.Variant();
						variant2.StringValue = partyListenerEvent.StringData2;
						break;
					case 3U:
						variant2 = new bnet.protocol.Variant();
						variant2.BlobValue = partyListenerEvent.BlobData;
						break;
					}
					if (BnetParty.OnMemberAttributeChanged != null)
					{
						BnetParty.OnMemberAttributeChanged(joinedParty3, partyListenerEvent.SubjectMemberId, stringData3, variant2);
					}
					break;
				}
				}
				IL_686:
				i++;
				continue;
				IL_204:
				PartyType partyTypeFromString3;
				if (BnetParty.s_joinedParties.TryGetValue(partyId, out partyTypeFromString3))
				{
					BnetParty.s_joinedParties.Remove(partyId);
				}
				else
				{
					partyTypeFromString3 = BnetParty.GetPartyTypeFromString(partyListenerEvent.StringData);
				}
				if (BnetParty.OnJoined != null)
				{
					BnetParty.OnJoined(OnlineEventType.REMOVED, new PartyInfo(partyId, partyTypeFromString3), new LeaveReason?((LeaveReason)partyListenerEvent.UintData));
					goto IL_686;
				}
				goto IL_686;
			}
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00080AB4 File Offset: 0x0007ECB4
		private static void RaisePartyError(bool isOperationCallback, string szPartyType, BnetFeatureEvent featureEvent, string errorMessageFormat, params object[] args)
		{
			string debugContext = string.Format(errorMessageFormat, args);
			BnetParty.RaisePartyError(new PartyError
			{
				IsOperationCallback = isOperationCallback,
				DebugContext = debugContext,
				ErrorCode = BattleNetErrors.ERROR_OK,
				Feature = BnetFeature.Party,
				FeatureEvent = featureEvent,
				szPartyType = szPartyType
			});
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00080B10 File Offset: 0x0007ED10
		private static void RaisePartyError(bool isOperationCallback, string debugContext, BattleNetErrors errorCode, BnetFeature feature, BnetFeatureEvent featureEvent, PartyId partyId, string szPartyType, string stringData, string errorMessageFormat, params object[] args)
		{
			if (BnetParty.OnError == null)
			{
				return;
			}
			BnetParty.RaisePartyError(new PartyError
			{
				IsOperationCallback = isOperationCallback,
				DebugContext = debugContext,
				ErrorCode = errorCode,
				Feature = feature,
				FeatureEvent = featureEvent,
				PartyId = partyId,
				szPartyType = szPartyType,
				StringData = stringData
			});
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00080B7C File Offset: 0x0007ED7C
		private static void RaisePartyError(PartyError error)
		{
			string message = string.Format("BnetParty: event={0} feature={1} code={2} partyId={3} type={4} strData={5}", new object[]
			{
				error.FeatureEvent.ToString(),
				(int)error.FeatureEvent,
				error.ErrorCode,
				error.PartyId,
				error.szPartyType,
				error.StringData
			});
			LogLevel level = (error.ErrorCode == BattleNetErrors.ERROR_OK) ? LogLevel.Info : LogLevel.Error;
			Log.Party.Print(level, message);
			if (BnetParty.OnError != null)
			{
				BnetParty.OnError(error);
			}
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00080C18 File Offset: 0x0007EE18
		public static PartyType GetPartyTypeFromString(string partyType)
		{
			PartyType result = PartyType.DEFAULT;
			if (partyType != null)
			{
				EnumUtils.TryGetEnum<PartyType>(partyType, out result);
			}
			return result;
		}

		// Token: 0x04000EC8 RID: 3784
		public const string ATTRIBUTE_PARTY_TYPE = "WTCG.Party.Type";

		// Token: 0x04000EC9 RID: 3785
		public const string ATTRIBUTE_PARTY_CREATOR = "WTCG.Party.Creator";

		// Token: 0x04000ECA RID: 3786
		public const string ATTRIBUTE_SCENARIO_ID = "WTCG.Game.ScenarioId";

		// Token: 0x04000ECB RID: 3787
		public const string ATTRIBUTE_FRIENDLY_DECLINE_REASON = "WTCG.Friendly.DeclineReason";

		// Token: 0x04000ECC RID: 3788
		public const string ATTRIBUTE_FORMAT_TYPE = "WTCG.Format.Type";

		// Token: 0x04000ECD RID: 3789
		public const string ATTRIBUTE_BRAWL_TYPE = "WTCG.Brawl.Type";

		// Token: 0x04000ECE RID: 3790
		public const string ATTRIBUTE_SEASON_ID = "WTCG.Season.Id";

		// Token: 0x04000ECF RID: 3791
		public const string ATTRIBUTE_BRAWL_LIBRARY_ITEM_ID = "WTCG.Brawl.LibraryItemId";

		// Token: 0x04000ED0 RID: 3792
		public const string ATTRIBUTE_PARTY_SERVER_INFO = "WTCG.Party.ServerInfo";

		// Token: 0x04000EDB RID: 3803
		private static Map<BnetFeatureEvent, HashSet<BattleNetErrors>> s_ignorableErrorCodes = new Map<BnetFeatureEvent, HashSet<BattleNetErrors>>
		{
			{
				BnetFeatureEvent.Party_KickMember_Callback,
				new HashSet<BattleNetErrors>
				{
					BattleNetErrors.ERROR_CHANNEL_NO_SUCH_MEMBER
				}
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

		// Token: 0x04000EDC RID: 3804
		private static Map<PartyId, PartyType> s_joinedParties = new Map<PartyId, PartyType>();

		// Token: 0x04000EDD RID: 3805
		private static Map<PartyType, BnetParty.CreateSuccessCallback> s_pendingPartyCreates = null;

		// Token: 0x04000EDE RID: 3806
		private static Map<string, List<BnetParty.PartyAttributeChangedHandler>> s_attributeChangedSubscribers = null;

		// Token: 0x020006CA RID: 1738
		// (Invoke) Token: 0x060062D2 RID: 25298
		public delegate void PartyErrorHandler(PartyError error);

		// Token: 0x020006CB RID: 1739
		// (Invoke) Token: 0x060062D6 RID: 25302
		public delegate void JoinedHandler(OnlineEventType evt, PartyInfo party, LeaveReason? reason);

		// Token: 0x020006CC RID: 1740
		// (Invoke) Token: 0x060062DA RID: 25306
		public delegate void PrivacyLevelChangedHandler(PartyInfo party, PrivacyLevel newPrivacyLevel);

		// Token: 0x020006CD RID: 1741
		// (Invoke) Token: 0x060062DE RID: 25310
		public delegate void MemberEventHandler(OnlineEventType evt, PartyInfo party, BnetGameAccountId memberId, bool isRolesUpdate, LeaveReason? reason);

		// Token: 0x020006CE RID: 1742
		// (Invoke) Token: 0x060062E2 RID: 25314
		public delegate void ReceivedInviteHandler(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, InviteRemoveReason? reason);

		// Token: 0x020006CF RID: 1743
		// (Invoke) Token: 0x060062E6 RID: 25318
		public delegate void SentInviteHandler(OnlineEventType evt, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, bool senderIsMyself, InviteRemoveReason? reason);

		// Token: 0x020006D0 RID: 1744
		// (Invoke) Token: 0x060062EA RID: 25322
		public delegate void ReceivedInviteRequestHandler(OnlineEventType evt, PartyInfo party, InviteRequest request, InviteRequestRemovedReason? reason);

		// Token: 0x020006D1 RID: 1745
		// (Invoke) Token: 0x060062EE RID: 25326
		public delegate void ChatMessageHandler(PartyInfo party, BnetGameAccountId speakerId, string chatMessage);

		// Token: 0x020006D2 RID: 1746
		// (Invoke) Token: 0x060062F2 RID: 25330
		public delegate void PartyAttributeChangedHandler(PartyInfo party, string attributeKey, bnet.protocol.Variant attributeValue);

		// Token: 0x020006D3 RID: 1747
		// (Invoke) Token: 0x060062F6 RID: 25334
		public delegate void MemberAttributeChangedHandler(PartyInfo party, BnetGameAccountId partyMember, string attributeKey, bnet.protocol.Variant attributeValue);

		// Token: 0x020006D4 RID: 1748
		// (Invoke) Token: 0x060062FA RID: 25338
		public delegate void CreateSuccessCallback(PartyType type, PartyId newlyCreatedPartyId);

		// Token: 0x020006D5 RID: 1749
		public enum FriendlyGameRoleSet
		{
			// Token: 0x04002249 RID: 8777
			Inviter = 1,
			// Token: 0x0400224A RID: 8778
			Invitee
		}

		// Token: 0x020006D6 RID: 1750
		public enum SpectatorPartyRoleSet
		{
			// Token: 0x0400224C RID: 8780
			Member = 1,
			// Token: 0x0400224D RID: 8781
			Leader
		}
	}
}
