using System.Collections.Generic;
using System.Linq;
using bgs.types;
using bnet.protocol.account.v1;
using bnet.protocol.channel.v2;
using bnet.protocol.channel.v2.membership;
using bnet.protocol.channel.v2.Types;
using bnet.protocol.v2;

namespace bgs
{
	public class PartyAPI : BattleNetAPI
	{
		public struct PartyCreateOptions
		{
			public string m_name;

			public bnet.protocol.channel.v2.Types.PrivacyLevel m_privacyLevel;
		}

		public static string PARTY_TYPE_DEFAULT = "default";

		private List<PartyEvent> m_partyEvents = new List<PartyEvent>();

		private List<PartyListenerEvent> m_partyListenerEvents = new List<PartyListenerEvent>();

		private Map<uint, Channel> m_activeParties = new Map<uint, Channel>();

		public PartyAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Party")
		{
		}

		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
			m_partyEvents.Clear();
			m_partyListenerEvents.Clear();
		}

		private bool PartyExists(ChannelId channelId)
		{
			return m_activeParties.ContainsKey(channelId.Id);
		}

		public string GetPartyType(ChannelId channelId)
		{
			string result = "";
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription == null || !channelDescription.HasType || !channelDescription.Type.HasChannelType)
			{
				return result;
			}
			result = channelDescription.Type.ChannelType;
			if (result == PARTY_TYPE_DEFAULT)
			{
				GetPartyAttributeString(channelId, "WTCG.Party.Type", out var value);
				if (value != null)
				{
					result = value;
				}
			}
			return result;
		}

		private Channel GetPartyData(ChannelId channelId)
		{
			if (m_activeParties.TryGetValue(channelId.Id, out var value))
			{
				return value;
			}
			return null;
		}

		private void GenericPartyRequestCallback(RPCContext context, string message, BnetFeatureEvent featureEvent, ChannelId channelId, string szPartyType)
		{
			BattleNetErrors error = ((context == null || context.Header == null) ? BattleNetErrors.ERROR_RPC_MALFORMED_RESPONSE : ((BattleNetErrors)context.Header.Status));
			GenericPartyRequestCallback_Internal(error, message, featureEvent, channelId, szPartyType);
		}

		private void UpdateMemberRole(ChannelId channelId, RoleAssignment assignment)
		{
			Channel partyData = GetPartyData(channelId);
			if (partyData != null && partyData.MemberList != null && assignment != null && assignment.HasMemberId)
			{
				partyData.MemberList.FirstOrDefault((Member i) => i.HasId && i.Id.Equals(assignment.MemberId))?.SetRole(assignment.Role);
			}
		}

		private void UpdateMemberAttribute(ChannelId channelId, AttributeAssignment assignment)
		{
			Channel partyData = GetPartyData(channelId);
			if (partyData != null && assignment != null && assignment.HasMemberId)
			{
				partyData.MemberList.FirstOrDefault((Member i) => i.HasId && i.Id.Equals(assignment.MemberId))?.SetAttribute(assignment.Attribute);
			}
		}

		private void GenericPartyRequestCallback_Internal(BattleNetErrors error, string message, BnetFeatureEvent featureEvent, ChannelId channelId, string szPartyType)
		{
			m_battleNet.Party.PushPartyErrorEvent(PartyListenerEventType.OPERATION_CALLBACK, message, error, featureEvent, channelId, szPartyType);
			if (error != 0)
			{
				message = ((channelId == null) ? $"PartyRequestError: {(int)error} {error.ToString()} {message} type={szPartyType}" : $"PartyRequestError: {(int)error} {error.ToString()} {message} partyId={channelId.Id} type={szPartyType}");
				m_battleNet.Party.ApiLog.LogError(message);
			}
			else
			{
				message = ((channelId == null) ? $"PartyRequest {message} status={error.ToString()} type={szPartyType}" : $"PartyRequest {message} status={error.ToString()} partyId={channelId.Id} type={szPartyType}");
				m_battleNet.Party.ApiLog.LogDebug(message);
			}
		}

		private void AddPartyFromChannel(Channel channel)
		{
			m_activeParties.Add(channel.Id.Id, channel);
			string channelType = channel.Type.ChannelType;
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.JOINED_PARTY;
			item.PartyId = PartyId.FromChannelId(channel.Id);
			item.StringData = channelType;
			m_partyListenerEvents.Add(item);
		}

		public void AddActiveChannel(ChannelId channelId, Channel channel, RPCContextDelegate callback)
		{
			if (channel != null)
			{
				AddPartyFromChannel(channel);
				return;
			}
			m_battleNet.Channel.GetChannel(channelId, callback, new ChannelAPI.ChannelInformation[4]
			{
				ChannelAPI.ChannelInformation.MEMBERS,
				ChannelAPI.ChannelInformation.ROLES,
				ChannelAPI.ChannelInformation.INVITATIONS,
				ChannelAPI.ChannelInformation.ATTRIBUTES
			});
		}

		public void RemoveActiveChannel(ulong channelId)
		{
			if (m_activeParties.ContainsKey((uint)channelId))
			{
				m_activeParties.Remove((uint)channelId);
			}
		}

		public void GetPartyListenerEvents(out PartyListenerEvent[] updates)
		{
			updates = new PartyListenerEvent[m_partyListenerEvents.Count];
			m_partyListenerEvents.CopyTo(updates);
		}

		public void ClearPartyListenerEvents()
		{
			m_partyListenerEvents.Clear();
		}

		private void PushPartyErrorEvent(PartyListenerEventType evtType, string szDebugContext, Error error, BnetFeatureEvent featureEvent, ChannelId channelId = null, string szStringData = null)
		{
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = evtType;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.UintData = error.Code;
			item.UlongData = 0x400000000uL | (uint)featureEvent;
			item.StringData = ((szDebugContext == null) ? "" : szDebugContext);
			item.StringData2 = ((szStringData == null) ? "" : szStringData);
			m_partyListenerEvents.Add(item);
		}

		public void CreateParty(string szPartyType, int privacyLevel, params Attribute[] partyAttributes)
		{
			CreateChannelOptions createChannelOptions = new CreateChannelOptions();
			UniqueChannelType uniqueChannelType = new UniqueChannelType();
			List<Attribute> list = new List<Attribute>();
			uniqueChannelType.SetChannelType(szPartyType);
			uniqueChannelType.SetProgram(BnetProgramId.HEARTHSTONE.GetValue());
			createChannelOptions.SetType(uniqueChannelType);
			createChannelOptions.SetPrivacyLevel((bnet.protocol.channel.v2.Types.PrivacyLevel)privacyLevel);
			createChannelOptions.SetAttribute(list);
			list.Add(ProtocolHelper.CreateAttributeV2("WTCG.Party.Type", szPartyType));
			foreach (Attribute attribute2 in partyAttributes)
			{
				if (attribute2 != null)
				{
					list.Add(attribute2);
				}
			}
			m_battleNet.Channel.CreateChannel(createChannelOptions);
		}

		public void JoinParty(ChannelId channelId, string szPartyType)
		{
			if (m_battleNet.Channel.GetChannelDescription(channelId) != null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_PARTY_ALREADY_IN_PARTY, "JoinParty already in party", BnetFeatureEvent.Party_Join_Callback, channelId, szPartyType);
			}
			else
			{
				m_battleNet.Channel.Join(channelId);
			}
		}

		public void LeaveParty(ChannelId channelId)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			string partyType = GetPartyType(channelId);
			if (channelDescription == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "LeaveParty no PartyData", BnetFeatureEvent.Party_Leave_Callback, channelId, partyType);
			}
			else
			{
				m_battleNet.Channel.Leave(channelId);
			}
		}

		public void DissolveParty(ChannelId channelId)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			string partyType = GetPartyType(channelId);
			if (channelDescription == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "DissolveParty no PartyData", BnetFeatureEvent.Party_Dissolve_Callback, channelId, partyType);
			}
			else
			{
				m_battleNet.Channel.DissolveChannel(channelId);
			}
		}

		public void SetPartyPrivacy(ChannelId channelId, int privacyLevel)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			string partyType = GetPartyType(channelId);
			if (channelDescription == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "SetPartyPrivacy no PartyData", BnetFeatureEvent.Party_SetPrivacy_Callback, channelId, partyType);
			}
			else
			{
				m_battleNet.Channel.SetPrivacyLevel(channelId, (bnet.protocol.channel.v2.Types.PrivacyLevel)privacyLevel);
			}
		}

		public void AssignPartyRole(ChannelId channelId, GameAccountHandle memberId, uint roleId)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			string partyType = GetPartyType(channelId);
			if (channelDescription == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "AssignPartyRole no PartyData", BnetFeatureEvent.Party_AssignRole_Callback, channelId, partyType);
				return;
			}
			m_battleNet.Channel.AssignRole(channelId, memberId, new uint[1] { roleId }.ToList());
		}

		public void SendPartyInvite(ChannelId channelId, GameAccountHandle inviteeId, bool isReservation)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			string partyType = GetPartyType(channelId);
			if (channelDescription == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "SendPartyInvite no PartyData", BnetFeatureEvent.Party_SendInvite_Callback, channelId, partyType);
				return;
			}
			m_battleNet.Channel.SendInvitation(channelId, inviteeId, null);
			m_battleNet.Channel.RemoveInviteRequestsFor(channelId, inviteeId, 0u);
		}

		public void AcceptPartyInvite(ulong invitationId)
		{
			ChannelInvitation receivedInvite = m_battleNet.Channel.GetReceivedInvite(invitationId);
			if (receivedInvite == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "AcceptPartyInvite, no corresponding invitation.", BnetFeatureEvent.Party_AcceptInvite_Callback, null, null);
			}
			else
			{
				m_battleNet.Channel.AcceptInvitation(receivedInvite.Channel.Id, invitationId);
			}
		}

		public void DeclinePartyInvite(ulong invitationId)
		{
			ChannelInvitation receivedInvite = m_battleNet.Channel.GetReceivedInvite(invitationId);
			if (receivedInvite == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "DeclinePartyInvite, no corresponding invitation.", BnetFeatureEvent.Party_DeclineInvite_Callback, null, null);
			}
			else
			{
				m_battleNet.Channel.DeclineInvitation(receivedInvite.Channel.Id, invitationId);
			}
		}

		public void RevokePartyInvite(ChannelId channelId, ulong invitationId)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			string partyType = GetPartyType(channelId);
			if (channelDescription == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "RevokePartyInvite no PartyData", BnetFeatureEvent.Party_RevokeInvite_Callback, channelId, partyType);
			}
			else
			{
				m_battleNet.Channel.RevokeInvitation(channelId, invitationId);
			}
		}

		public void RequestPartyInvite(ChannelId channelId, GameAccountHandle whomToAskForApproval, GameAccountHandle whomToInvite)
		{
			m_battleNet.Channel.SendSuggestion(channelId, whomToAskForApproval, whomToInvite);
		}

		public void IgnoreInviteRequest(ChannelId channelId, GameAccountHandle requestedTargetId)
		{
			m_battleNet.Channel.RemoveInviteRequestsFor(channelId, requestedTargetId, 1u);
		}

		public void KickPartyMember(ChannelId channelId, GameAccountHandle memberId)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			string partyType = GetPartyType(channelId);
			if (channelDescription == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "KickPartyMember no PartyData", BnetFeatureEvent.Party_KickMember_Callback, channelId, partyType);
			}
			else
			{
				m_battleNet.Channel.Kick(channelId, memberId);
			}
		}

		public void SendPartyChatMessage(ChannelId channelId, string message)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			string partyType = GetPartyType(channelId);
			if (channelDescription == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "SendPartyChatMessage no PartyData", BnetFeatureEvent.Party_SendChatMessage_Callback, channelId, partyType);
			}
			else
			{
				m_battleNet.Channel.SendMessage(channelId, message);
			}
		}

		public void ClearPartyAttribute(ChannelId channelId, string attributeKey)
		{
			Attribute attribute2 = new Attribute();
			Variant value = new Variant();
			attribute2.SetName(attributeKey);
			attribute2.SetValue(value);
			SetPartyAttribute_Internal("ClearPartyAttribute key=" + attributeKey, BnetFeatureEvent.Party_ClearAttribute_Callback, channelId, attribute2);
		}

		public void SetPartyAttributeLong(ChannelId channelId, string attributeKey, long value)
		{
			SetPartyAttribute_Internal("SetPartyAttributeLong key=" + attributeKey + " val=" + value, BnetFeatureEvent.Party_SetAttribute_Callback, channelId, ProtocolHelper.CreateAttributeV2(attributeKey, value));
		}

		public void SetPartyAttributeString(ChannelId channelId, string attributeKey, string value)
		{
			SetPartyAttribute_Internal("SetPartyAttributeString key=" + attributeKey + " val=" + value, BnetFeatureEvent.Party_SetAttribute_Callback, channelId, ProtocolHelper.CreateAttributeV2(attributeKey, value));
		}

		public void SetPartyAttributeBlob(ChannelId channelId, string attributeKey, byte[] value)
		{
			SetPartyAttribute_Internal("SetPartyAttributeBlob key=" + attributeKey + " val=" + ((value == null) ? "null" : (value.Length + " bytes")), BnetFeatureEvent.Party_SetAttribute_Callback, channelId, ProtocolHelper.CreateAttributeV2(attributeKey, value));
		}

		public void SetPartyAttributes(ChannelId channelId, params Attribute[] attrs)
		{
			if (attrs != null && attrs.Length != 0)
			{
				SetPartyAttribute_Internal("SetPartyAttributes key=" + attrs.First().Name + " val=" + ((attrs.First().Value == null) ? "null" : (attrs.First().Value.IsNone() ? "none" : attrs.First().Value.ToString())), BnetFeatureEvent.Party_SetAttribute_Callback, channelId, attrs);
			}
		}

		private void SetPartyAttribute_Internal(string debugMessage, BnetFeatureEvent featureEvent, ChannelId channelId, params Attribute[] attrs)
		{
			UpdatePartyState_Internal(debugMessage, featureEvent, channelId, attrs.ToList());
		}

		private void UpdatePartyState_Internal(string debugMessage, BnetFeatureEvent featureEvent, ChannelId channelId, List<Attribute> attributes)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			string partyType = GetPartyType(channelId);
			if (channelDescription == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "{0} no PartyData", featureEvent, channelId, partyType);
			}
			else
			{
				m_battleNet.Channel.SetChannelAttributes(channelId, attributes);
			}
		}

		public void ClearMemberAttribute(ChannelId channelId, GameAccountHandle member, string attributeKey)
		{
			Attribute attribute2 = new Attribute();
			Variant value = new Variant();
			attribute2.SetName(attributeKey);
			attribute2.SetValue(value);
			SetMemberAttribute_Internal("ClearMemberAttribute key=" + attributeKey, BnetFeatureEvent.Party_ClearAttribute_Callback, channelId, member, attribute2);
		}

		public void SetMemberAttributeLong(ChannelId channelId, GameAccountHandle member, string attributeKey, long value)
		{
			SetMemberAttribute_Internal("SetMemberAttributeLong key=" + attributeKey + " val=" + value, BnetFeatureEvent.Party_SetAttribute_Callback, channelId, member, ProtocolHelper.CreateAttributeV2(attributeKey, value));
		}

		public void SetMemberAttributeString(ChannelId channelId, GameAccountHandle member, string attributeKey, string value)
		{
			SetMemberAttribute_Internal("SetMemberAttributeString key=" + attributeKey + " val=" + value, BnetFeatureEvent.Party_SetAttribute_Callback, channelId, member, ProtocolHelper.CreateAttributeV2(attributeKey, value));
		}

		public void SetMemberAttributeBlob(ChannelId channelId, GameAccountHandle member, string attributeKey, byte[] value)
		{
			SetMemberAttribute_Internal("SetMemberAttributeBlob key=" + attributeKey + " val=" + ((value == null) ? "null" : (value.Length + " bytes")), BnetFeatureEvent.Party_SetAttribute_Callback, channelId, member, ProtocolHelper.CreateAttributeV2(attributeKey, value));
		}

		public void SetMemberAttributes(ChannelId channelId, GameAccountHandle member, params Attribute[] attrs)
		{
			if (attrs != null && attrs.Length != 0)
			{
				SetMemberAttribute_Internal("SetMemberAttributes key=" + attrs.First().Name + " val=" + ((attrs.First().Value == null) ? "null" : (attrs.First().Value.IsNone() ? "none" : attrs.First().Value.ToString())), BnetFeatureEvent.Party_SetAttribute_Callback, channelId, member, attrs);
			}
		}

		private void SetMemberAttribute_Internal(string debugMessage, BnetFeatureEvent featureEvent, ChannelId channelId, GameAccountHandle member, params Attribute[] attrs)
		{
			UpdateMemberState_Internal(debugMessage, featureEvent, channelId, member, attrs.ToList());
		}

		private void UpdateMemberState_Internal(string debugMessage, BnetFeatureEvent featureEvent, ChannelId channelId, GameAccountHandle member, List<Attribute> attributes)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			string partyType = GetPartyType(channelId);
			if (channelDescription == null)
			{
				GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "{0} no PartyData", featureEvent, channelId, partyType);
				return;
			}
			AttributeAssignment attributeAssignment = new AttributeAssignment();
			attributeAssignment.SetMemberId(member);
			attributeAssignment.SetAttribute(attributes);
			m_battleNet.Channel.SetMemberAttribute(channelId, attributeAssignment);
		}

		public int GetPartyPrivacy(ChannelId channelId)
		{
			int result = 4;
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription == null)
			{
				return result;
			}
			if (channelDescription.HasPrivacyLevel)
			{
				result = (int)channelDescription.PrivacyLevel;
			}
			return result;
		}

		public uint GetCountPartyMembers(ChannelId channelId)
		{
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription != null && channelDescription.HasMemberCount)
			{
				return channelDescription.MemberCount;
			}
			return 0u;
		}

		public uint GetMaxPartyMembers(ChannelId channelId)
		{
			return 100u;
		}

		public void GetPartyMembers(ChannelId channelId, out bgs.types.PartyMember[] members)
		{
			members = null;
			Channel partyData = GetPartyData(channelId);
			if (partyData != null && partyData.MemberCount > 0)
			{
				members = new bgs.types.PartyMember[partyData.MemberCount];
				int num = 0;
				foreach (Member member in partyData.MemberList)
				{
					bgs.types.PartyMember partyMember = default(bgs.types.PartyMember);
					partyMember.memberGameAccountId = BnetEntityId.CreateEntityId(BnetEntityId.CreateFromGameAccountHandle(member.Id));
					partyMember.battleTag = member.BattleTag;
					partyMember.voiceId = member.VoiceId;
					if (member.RoleCount > 0)
					{
						partyMember.firstMemberRole = member.RoleList[0];
					}
					members[num] = partyMember;
					num++;
				}
			}
			if (members == null)
			{
				members = new bgs.types.PartyMember[0];
			}
		}

		public string GetReceivedInvitationPartyType(ulong invitationId)
		{
			string result = "";
			ChannelInvitation receivedInvite = m_battleNet.Channel.GetReceivedInvite(invitationId);
			if (receivedInvite != null && receivedInvite.HasChannel && receivedInvite.Channel.HasType)
			{
				result = receivedInvite.Channel.Type.ChannelType;
			}
			return result;
		}

		public Attribute GetReceivedInvitationAttribute(ChannelId channelId, string attributeKey)
		{
			ChannelInvitation channelInvitation = m_battleNet.Channel.GetAllReceivedInvites().FirstOrDefault((ChannelInvitation i) => i.HasChannel && i.Channel.HasId && i.Channel.Id.Equals(channelId));
			if (channelInvitation != null && channelInvitation.HasChannel)
			{
				ChannelDescription channel = channelInvitation.Channel;
				for (int j = 0; j < channel.AttributeList.Count; j++)
				{
					Attribute attribute2 = channel.AttributeList[j];
					if (attribute2.Name == attributeKey)
					{
						return attribute2;
					}
				}
			}
			return null;
		}

		public void GetReceivedPartyInvites(out PartyInvite[] invites)
		{
			ChannelInvitation[] allReceivedInvites = m_battleNet.Channel.GetAllReceivedInvites();
			invites = new PartyInvite[allReceivedInvites.Length];
			for (int i = 0; i < invites.Length; i++)
			{
				ChannelInvitation channelInvitation = allReceivedInvites[i];
				PartyType partyTypeFromString = BnetParty.GetPartyTypeFromString(GetReceivedInvitationPartyType(channelInvitation.Id));
				PartyInvite partyInvite = new PartyInvite();
				partyInvite.InviteId = channelInvitation.Id;
				partyInvite.PartyId = PartyId.FromChannelId(channelInvitation.Channel.Id);
				partyInvite.PartyType = partyTypeFromString;
				partyInvite.InviterName = channelInvitation.Inviter.BattleTag;
				partyInvite.InviteeName = channelInvitation.Invitee.BattleTag;
				partyInvite.InviterId = BnetGameAccountId.CreateFromGameAccountHandle(channelInvitation.Inviter.Id);
				partyInvite.InviteeId = BnetGameAccountId.CreateFromGameAccountHandle(channelInvitation.Invitee.Id);
				invites[i] = partyInvite;
			}
		}

		private Channel GetPartyState(ChannelId channelId)
		{
			m_activeParties.TryGetValue(channelId.Id, out var value);
			return value;
		}

		public void GetPartySentChannelInvitations(ChannelId channelId, out ChannelInvitation[] invites)
		{
			invites = null;
			Channel partyState = GetPartyState(channelId);
			if (partyState != null)
			{
				invites = partyState.InvitationList.ToArray();
			}
		}

		public void GetPartySentInvites(ChannelId channelId, out PartyInvite[] invites)
		{
			invites = null;
			Channel partyState = GetPartyState(channelId);
			if (partyState != null)
			{
				invites = new PartyInvite[partyState.InvitationCount];
				PartyType partyTypeFromString = BnetParty.GetPartyTypeFromString(GetPartyType(channelId));
				for (int i = 0; i < invites.Length; i++)
				{
					ChannelInvitation channelInvitation = partyState.InvitationList[i];
					PartyInvite partyInvite = new PartyInvite();
					partyInvite.InviteId = channelInvitation.Id;
					partyInvite.PartyId = PartyId.FromChannelId(channelId);
					partyInvite.PartyType = partyTypeFromString;
					partyInvite.InviterName = channelInvitation.Inviter.BattleTag;
					partyInvite.InviteeName = channelInvitation.Invitee.BattleTag;
					partyInvite.InviterId = BnetGameAccountId.CreateFromGameAccountHandle(channelInvitation.Inviter.Id);
					partyInvite.InviteeId = BnetGameAccountId.CreateFromGameAccountHandle(channelInvitation.Invitee.Id);
					invites[i] = partyInvite;
				}
			}
			if (invites == null)
			{
				invites = new PartyInvite[0];
			}
		}

		public void GetPartyInviteRequests(ChannelId channelId, out InviteRequest[] requests)
		{
			Suggestion[] receivedInviteRequestsForChannel = m_battleNet.Channel.GetReceivedInviteRequestsForChannel(channelId);
			requests = new InviteRequest[receivedInviteRequestsForChannel.Length];
			for (int i = 0; i < requests.Length; i++)
			{
				Suggestion suggestion = receivedInviteRequestsForChannel[i];
				InviteRequest inviteRequest = new InviteRequest();
				inviteRequest.TargetName = suggestion.Suggestee.BattleTag;
				inviteRequest.TargetId = BnetGameAccountId.CreateFromGameAccountHandle(suggestion.Suggestee.Id);
				inviteRequest.RequesterName = suggestion.Suggester.BattleTag;
				inviteRequest.RequesterId = BnetGameAccountId.CreateFromGameAccountHandle(suggestion.Suggester.Id);
				requests[i] = inviteRequest;
			}
		}

		public void GetAllPartyAttributes(ChannelId channelId, out string[] allKeys)
		{
			ChannelInvitation channelInvitation = m_battleNet.Channel.GetAllReceivedInvites().FirstOrDefault((ChannelInvitation i) => i.HasChannel && i.Channel.HasId && i.Channel.Id.Equals(channelId));
			ChannelDescription channelDescription = ((channelInvitation == null || !channelInvitation.HasChannel) ? m_battleNet.Channel.GetChannelDescription(channelId) : channelInvitation.Channel);
			if (channelDescription == null)
			{
				allKeys = new string[0];
				return;
			}
			allKeys = new string[channelDescription.AttributeList.Count];
			for (int j = 0; j < channelDescription.AttributeList.Count; j++)
			{
				Attribute attribute2 = channelDescription.AttributeList[j];
				allKeys[j] = attribute2.Name;
			}
		}

		public bool GetPartyAttributeLong(ChannelId channelId, string attributeKey, out long value)
		{
			value = 0L;
			Attribute receivedInvitationAttribute = GetReceivedInvitationAttribute(channelId, attributeKey);
			if (receivedInvitationAttribute != null && receivedInvitationAttribute.Value.HasIntValue)
			{
				value = receivedInvitationAttribute.Value.IntValue;
				return true;
			}
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription != null)
			{
				for (int i = 0; i < channelDescription.AttributeList.Count; i++)
				{
					receivedInvitationAttribute = channelDescription.AttributeList[i];
					if (receivedInvitationAttribute.Name == attributeKey && receivedInvitationAttribute.Value.HasIntValue)
					{
						value = receivedInvitationAttribute.Value.IntValue;
						return true;
					}
				}
			}
			return false;
		}

		public void GetPartyAttributeString(ChannelId channelId, string attributeKey, out string value)
		{
			value = null;
			Attribute receivedInvitationAttribute = GetReceivedInvitationAttribute(channelId, attributeKey);
			if (receivedInvitationAttribute != null && receivedInvitationAttribute.Value.HasStringValue)
			{
				value = receivedInvitationAttribute.Value.StringValue;
				return;
			}
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription == null)
			{
				return;
			}
			for (int i = 0; i < channelDescription.AttributeList.Count; i++)
			{
				receivedInvitationAttribute = channelDescription.AttributeList[i];
				if (receivedInvitationAttribute.Name == attributeKey && receivedInvitationAttribute.Value.HasStringValue)
				{
					value = receivedInvitationAttribute.Value.StringValue;
					break;
				}
			}
		}

		public void GetPartyAttributeBlob(ChannelId channelId, string attributeKey, out byte[] value)
		{
			value = null;
			Attribute receivedInvitationAttribute = GetReceivedInvitationAttribute(channelId, attributeKey);
			if (receivedInvitationAttribute != null && receivedInvitationAttribute.Value.HasBlobValue)
			{
				value = receivedInvitationAttribute.Value.BlobValue;
				return;
			}
			ChannelDescription channelDescription = m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription == null)
			{
				return;
			}
			for (int i = 0; i < channelDescription.AttributeList.Count; i++)
			{
				receivedInvitationAttribute = channelDescription.AttributeList[i];
				if (receivedInvitationAttribute.Name == attributeKey && receivedInvitationAttribute.Value.HasBlobValue)
				{
					value = receivedInvitationAttribute.Value.BlobValue;
					break;
				}
			}
		}

		public void GetMemberAttributeString(ChannelId channelId, GameAccountHandle partyMember, string attributeKey, out string value)
		{
			value = null;
			Channel partyData = GetPartyData(channelId);
			if (partyData == null)
			{
				return;
			}
			Member member = partyData.MemberList.FirstOrDefault((Member i) => i.HasId && i.Id.Equals(partyMember));
			if (member != null)
			{
				Attribute attribute2 = member.AttributeList.FirstOrDefault((Attribute i) => i.HasName && i.Name.Equals(attributeKey));
				if (attribute2 != null && attribute2.HasValue && attribute2.Value.HasStringValue)
				{
					value = attribute2.Value.StringValue;
				}
			}
		}

		public void GetMemberAttributeBlob(ChannelId channelId, GameAccountHandle partyMember, string attributeKey, out byte[] value)
		{
			value = null;
			Channel partyData = GetPartyData(channelId);
			if (partyData == null)
			{
				return;
			}
			Member member = partyData.MemberList.FirstOrDefault((Member i) => i.HasId && i.Id.Equals(partyMember));
			if (member != null)
			{
				Attribute attribute2 = member.AttributeList.FirstOrDefault((Attribute i) => i.HasName && i.Name.Equals(attributeKey));
				if (attribute2 != null && attribute2.HasValue && attribute2.Value.HasBlobValue)
				{
					value = attribute2.Value.BlobValue;
				}
			}
		}

		public void PartyCreated(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			CreateChannelRequest createChannelRequest = (CreateChannelRequest)context.Request;
			m_battleNet.Party.PushPartyErrorEvent(PartyListenerEventType.OPERATION_CALLBACK, "CreateParty", status, BnetFeatureEvent.Party_Create_Callback, null, createChannelRequest.Options.Type.ChannelType + createChannelRequest.Options.Type.Program);
			if (status != 0)
			{
				m_battleNet.Party.ApiLog.LogError("CreateChannelCallback: code=" + new Error(status));
			}
		}

		public void PartyJoined(ChannelDescription channelDescription, bnet.protocol.channel.v2.SubscribeResponse subscribeResponse)
		{
			m_battleNet.Party.AddActiveChannel(channelDescription.Id, subscribeResponse.Channel, delegate(RPCContext ctx)
			{
				GetChannelResponse getChannelResponse = GetChannelResponse.ParseFrom(ctx.Payload);
				AddPartyFromChannel(getChannelResponse.Channel);
			});
		}

		public void PartyLeft(ChannelId channelId, ChannelRemovedNotification notification)
		{
			string partyType = GetPartyType(channelId);
			if (GetPartyData(channelId) != null)
			{
				m_activeParties.Remove(channelId.Id);
			}
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.LEFT_PARTY;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.StringData = partyType;
			item.UintData = (uint)(notification.HasReason ? notification.Reason : ChannelRemovedReason.CHANNEL_REMOVED_REASON_MEMBER_LEFT);
			m_partyListenerEvents.Add(item);
		}

		public void PartyMemberJoined(ChannelId channelId, MemberAddedNotification notification)
		{
			GetPartyData(channelId)?.AddMember(notification.Member);
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.MEMBER_JOINED;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.Member.Id);
			m_partyListenerEvents.Add(item);
		}

		public void MemberRolesChanged(ChannelId channelId, MemberRoleChangedNotification notification)
		{
			List<GameAccountHandle> list = new List<GameAccountHandle>();
			foreach (RoleAssignment assignment in notification.AssignmentList)
			{
				UpdateMemberRole(channelId, assignment);
				list.Add(assignment.MemberId);
			}
			string partyType = GetPartyType(channelId);
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.MEMBER_ROLE_CHANGED;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.StringData = partyType;
			foreach (GameAccountHandle item2 in list)
			{
				item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(item2);
				m_partyListenerEvents.Add(item);
			}
		}

		public void MemberAttributesChanged(ChannelId channelId, MemberAttributeChangedNotification notification)
		{
			if (!notification.HasAssignment || !notification.Assignment.HasMemberId)
			{
				return;
			}
			UpdateMemberAttribute(channelId, notification.Assignment);
			foreach (Attribute attribute in notification.Assignment.AttributeList)
			{
				PartyListenerEvent item = default(PartyListenerEvent);
				item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.Assignment.MemberId);
				item.Type = PartyListenerEventType.MEMBER_ATTRIBUTE_CHANGED;
				item.PartyId = PartyId.FromChannelId(channelId);
				item.StringData = attribute.Name;
				if (attribute.Value.IsNone())
				{
					item.UintData = 0u;
				}
				else if (attribute.Value.HasIntValue)
				{
					item.UintData = 1u;
					item.UlongData = (ulong)attribute.Value.IntValue;
				}
				else if (attribute.Value.HasStringValue)
				{
					item.UintData = 2u;
					item.StringData2 = attribute.Value.StringValue;
				}
				else if (attribute.Value.HasBlobValue)
				{
					item.UintData = 3u;
					item.BlobData = attribute.Value.BlobValue;
				}
				else
				{
					item.UintData = 0u;
				}
				m_partyListenerEvents.Add(item);
			}
		}

		public void PartyMemberLeft(ChannelId channelId, MemberRemovedNotification notification)
		{
			Channel partyData = GetPartyData(channelId);
			if (partyData != null)
			{
				Member item = partyData.MemberList.FirstOrDefault((Member i) => i.HasId && i.Id.Equals(notification.MemberId));
				partyData.Member.Remove(item);
			}
			PartyListenerEvent item2 = default(PartyListenerEvent);
			item2.Type = PartyListenerEventType.MEMBER_LEFT;
			item2.PartyId = PartyId.FromChannelId(channelId);
			item2.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.MemberId);
			item2.UintData = (notification.HasReason ? notification.Reason : 0u);
			m_partyListenerEvents.Add(item2);
		}

		public void ReceivedInvitationAdded(ReceivedInvitationAddedNotification notification, ChannelInvitation channelInvitation)
		{
			string receivedInvitationPartyType = GetReceivedInvitationPartyType(notification.Invitation.Id);
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.RECEIVED_INVITE_ADDED;
			item.PartyId = PartyId.FromChannelId(channelInvitation.Channel.Id);
			item.UlongData = notification.Invitation.Id;
			item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.Invitation.Inviter.Id);
			item.TargetMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.Invitation.Invitee.Id);
			item.StringData = receivedInvitationPartyType;
			item.StringData2 = notification.Invitation.Inviter.BattleTag;
			item.UintData = 0u;
			if (channelInvitation.HasSlot && channelInvitation.Slot.HasReserved && channelInvitation.Slot.Reserved)
			{
				item.UintData |= 1u;
			}
			if (channelInvitation.HasSlot && channelInvitation.Slot.HasRejoin && channelInvitation.Slot.Rejoin)
			{
				item.UintData |= 1u;
			}
			m_partyListenerEvents.Add(item);
			m_battleNet.Party.ApiLog.LogInfo("PartyAPI.ReceivedInvitationAdded: id={0} party={1}:{2} isRejoin={2} isReservation={3} inviter={4} invitee={5}", notification.Invitation.Id, receivedInvitationPartyType, item.PartyId.Lo, channelInvitation.HasSlot && channelInvitation.Slot.HasRejoin && channelInvitation.Slot.Rejoin, channelInvitation.HasSlot && channelInvitation.Slot.HasReserved && channelInvitation.Slot.Reserved, item.SubjectMemberId.GetLo(), item.TargetMemberId.GetLo());
		}

		public void ReceivedInvitationRemoved(ReceivedInvitationRemovedNotification notification, ChannelInvitation channelInvitation)
		{
			string receivedInvitationPartyType = GetReceivedInvitationPartyType(notification.InvitationId);
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.RECEIVED_INVITE_REMOVED;
			item.PartyId = PartyId.FromChannelId(channelInvitation.Channel.Id);
			item.UlongData = channelInvitation.Id;
			item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(channelInvitation.Inviter.Id);
			item.TargetMemberId = BnetGameAccountId.CreateFromGameAccountHandle(channelInvitation.Invitee.Id);
			item.StringData = receivedInvitationPartyType;
			item.StringData2 = channelInvitation.Inviter.BattleTag;
			if (notification.HasReason)
			{
				item.UintData = (uint)notification.Reason;
			}
			m_partyListenerEvents.Add(item);
		}

		public void PartyInvitationDelta(ChannelId channelId, ChannelInvitation invite, uint? removeReason)
		{
			Channel partyState = GetPartyState(channelId);
			if (partyState == null)
			{
				return;
			}
			string partyType = GetPartyType(channelId);
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = (removeReason.HasValue ? PartyListenerEventType.PARTY_INVITE_REMOVED : PartyListenerEventType.PARTY_INVITE_SENT);
			item.PartyId = PartyId.FromChannelId(channelId);
			item.UlongData = invite.Id;
			item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(invite.Inviter.Id);
			item.TargetMemberId = BnetGameAccountId.CreateFromGameAccountHandle(invite.Invitee.Id);
			item.StringData = partyType;
			item.StringData2 = invite.Inviter.BattleTag;
			if (removeReason.HasValue)
			{
				item.UintData = removeReason.Value;
				List<ChannelInvitation> invitationList = partyState.InvitationList;
				invitationList.Remove(invite);
				partyState.SetInvitation(invitationList);
			}
			else
			{
				item.UintData = 0u;
				if (invite.HasSlot && invite.Slot.HasReserved && invite.Slot.Reserved)
				{
					item.UintData |= 1u;
				}
				if (invite.HasSlot && invite.Slot.HasReserved && invite.Slot.Rejoin)
				{
					item.UintData |= 1u;
				}
				if (!partyState.InvitationList.Contains(invite))
				{
					partyState.AddInvitation(invite);
				}
			}
			m_partyListenerEvents.Add(item);
		}

		public void ReceivedInviteRequestDelta(ChannelId channelId, Suggestion suggestion, uint? removeReason)
		{
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = (removeReason.HasValue ? PartyListenerEventType.INVITE_REQUEST_REMOVED : PartyListenerEventType.INVITE_REQUEST_ADDED);
			item.PartyId = PartyId.FromChannelId(channelId);
			item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(suggestion.Suggester.Id);
			item.TargetMemberId = BnetGameAccountId.CreateFromGameAccountHandle(suggestion.Suggestee.Id);
			item.StringData = suggestion.Suggester.BattleTag;
			item.StringData2 = suggestion.Suggestee.BattleTag;
			if (removeReason.HasValue)
			{
				item.UintData = removeReason.Value;
			}
			m_partyListenerEvents.Add(item);
		}

		public void PartyMessageReceived(ChannelId channelId, SendMessageNotification notification)
		{
			string text = null;
			for (int i = 0; i < notification.Message.AttributeCount; i++)
			{
				Attribute attribute2 = notification.Message.AttributeList[i];
				if (attribute.TEXT_ATTRIBUTE.Equals(attribute2.Name) && attribute2.Value.HasStringValue)
				{
					text = attribute2.Value.StringValue;
					break;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				PartyListenerEvent item = default(PartyListenerEvent);
				item.Type = PartyListenerEventType.CHAT_MESSAGE_RECEIVED;
				item.PartyId = PartyId.FromChannelId(channelId);
				item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.AgentId);
				item.StringData = text;
				m_partyListenerEvents.Add(item);
			}
		}

		public void PartyTypingIndicatorUpdateReceived(ChannelId channelId, TypingIndicatorNotification notification)
		{
		}

		public void PartyPrivacyChanged(ChannelId channelId, bnet.protocol.channel.v2.Types.PrivacyLevel newPrivacyLevel)
		{
			string partyType = GetPartyType(channelId);
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.PRIVACY_CHANGED;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.UintData = (uint)newPrivacyLevel;
			item.StringData = partyType;
			m_partyListenerEvents.Add(item);
		}

		public void PartyAttributeChanged(ChannelId channelId, Attribute[] attributes)
		{
			foreach (Attribute attribute2 in attributes)
			{
				PartyListenerEvent item = default(PartyListenerEvent);
				item.Type = PartyListenerEventType.PARTY_ATTRIBUTE_CHANGED;
				item.PartyId = PartyId.FromChannelId(channelId);
				item.StringData = attribute2.Name;
				if (attribute2.Value.IsNone())
				{
					item.UintData = 0u;
				}
				else if (attribute2.Value.HasIntValue)
				{
					item.UintData = 1u;
					item.UlongData = (ulong)attribute2.Value.IntValue;
				}
				else if (attribute2.Value.HasStringValue)
				{
					item.UintData = 2u;
					item.StringData2 = attribute2.Value.StringValue;
				}
				else if (attribute2.Value.HasBlobValue)
				{
					item.UintData = 3u;
					item.BlobData = attribute2.Value.BlobValue;
				}
				else
				{
					item.UintData = 0u;
				}
				m_partyListenerEvents.Add(item);
			}
		}
	}
}
