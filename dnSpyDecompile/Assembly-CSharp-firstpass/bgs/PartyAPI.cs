using System;
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
	// Token: 0x02000209 RID: 521
	public class PartyAPI : BattleNetAPI
	{
		// Token: 0x06002008 RID: 8200 RVA: 0x00071A1B File Offset: 0x0006FC1B
		public PartyAPI(BattleNetCSharp battlenet) : base(battlenet, "Party")
		{
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x0006D1CC File Offset: 0x0006B3CC
		public override void InitRPCListeners(IRpcConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x0006C9FD File Offset: 0x0006ABFD
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x00071A4A File Offset: 0x0006FC4A
		public override void OnDisconnected()
		{
			base.OnDisconnected();
			this.m_partyEvents.Clear();
			this.m_partyListenerEvents.Clear();
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00071A68 File Offset: 0x0006FC68
		private bool PartyExists(ChannelId channelId)
		{
			return this.m_activeParties.ContainsKey(channelId.Id);
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x00071A7C File Offset: 0x0006FC7C
		public string GetPartyType(ChannelId channelId)
		{
			string text = "";
			ChannelDescription channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription == null || !channelDescription.HasType || !channelDescription.Type.HasChannelType)
			{
				return text;
			}
			text = channelDescription.Type.ChannelType;
			if (text == PartyAPI.PARTY_TYPE_DEFAULT)
			{
				string text2;
				this.GetPartyAttributeString(channelId, "WTCG.Party.Type", out text2);
				if (text2 != null)
				{
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00071AE8 File Offset: 0x0006FCE8
		private Channel GetPartyData(ChannelId channelId)
		{
			Channel result;
			if (this.m_activeParties.TryGetValue(channelId.Id, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00071B10 File Offset: 0x0006FD10
		private void GenericPartyRequestCallback(RPCContext context, string message, BnetFeatureEvent featureEvent, ChannelId channelId, string szPartyType)
		{
			BattleNetErrors error = (BattleNetErrors)((context == null || context.Header == null) ? 3008U : context.Header.Status);
			this.GenericPartyRequestCallback_Internal(error, message, featureEvent, channelId, szPartyType);
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00071B48 File Offset: 0x0006FD48
		private void UpdateMemberRole(ChannelId channelId, RoleAssignment assignment)
		{
			Channel partyData = this.GetPartyData(channelId);
			if (partyData == null || partyData.MemberList == null || assignment == null || !assignment.HasMemberId)
			{
				return;
			}
			Member member = partyData.MemberList.FirstOrDefault((Member i) => i.HasId && i.Id.Equals(assignment.MemberId));
			if (member != null)
			{
				member.SetRole(assignment.Role);
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00071BB8 File Offset: 0x0006FDB8
		private void UpdateMemberAttribute(ChannelId channelId, AttributeAssignment assignment)
		{
			Channel partyData = this.GetPartyData(channelId);
			if (partyData == null || assignment == null || !assignment.HasMemberId)
			{
				return;
			}
			Member member = partyData.MemberList.FirstOrDefault((Member i) => i.HasId && i.Id.Equals(assignment.MemberId));
			if (member != null)
			{
				member.SetAttribute(assignment.Attribute);
			}
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00071C20 File Offset: 0x0006FE20
		private void GenericPartyRequestCallback_Internal(BattleNetErrors error, string message, BnetFeatureEvent featureEvent, ChannelId channelId, string szPartyType)
		{
			this.m_battleNet.Party.PushPartyErrorEvent(PartyListenerEventType.OPERATION_CALLBACK, message, error, featureEvent, channelId, szPartyType);
			if (error != BattleNetErrors.ERROR_OK)
			{
				if (channelId != null)
				{
					message = string.Format("PartyRequestError: {0} {1} {2} partyId={3} type={4}", new object[]
					{
						(int)error,
						error.ToString(),
						message,
						channelId.Id,
						szPartyType
					});
				}
				else
				{
					message = string.Format("PartyRequestError: {0} {1} {2} type={3}", new object[]
					{
						(int)error,
						error.ToString(),
						message,
						szPartyType
					});
				}
				this.m_battleNet.Party.ApiLog.LogError(message);
				return;
			}
			if (channelId != null)
			{
				message = string.Format("PartyRequest {0} status={1} partyId={2} type={3}", new object[]
				{
					message,
					error.ToString(),
					channelId.Id,
					szPartyType
				});
			}
			else
			{
				message = string.Format("PartyRequest {0} status={1} type={2}", message, error.ToString(), szPartyType);
			}
			this.m_battleNet.Party.ApiLog.LogDebug(message);
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00071D58 File Offset: 0x0006FF58
		private void AddPartyFromChannel(Channel channel)
		{
			this.m_activeParties.Add(channel.Id.Id, channel);
			string channelType = channel.Type.ChannelType;
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.JOINED_PARTY;
			item.PartyId = PartyId.FromChannelId(channel.Id);
			item.StringData = channelType;
			this.m_partyListenerEvents.Add(item);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00071DBE File Offset: 0x0006FFBE
		public void AddActiveChannel(ChannelId channelId, Channel channel, RPCContextDelegate callback)
		{
			if (channel != null)
			{
				this.AddPartyFromChannel(channel);
				return;
			}
			this.m_battleNet.Channel.GetChannel(channelId, callback, new ChannelAPI.ChannelInformation[]
			{
				ChannelAPI.ChannelInformation.MEMBERS,
				ChannelAPI.ChannelInformation.ROLES,
				ChannelAPI.ChannelInformation.INVITATIONS,
				ChannelAPI.ChannelInformation.ATTRIBUTES
			});
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00071DEE File Offset: 0x0006FFEE
		public void RemoveActiveChannel(ulong channelId)
		{
			if (this.m_activeParties.ContainsKey((uint)channelId))
			{
				this.m_activeParties.Remove((uint)channelId);
			}
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00071E0D File Offset: 0x0007000D
		public void GetPartyListenerEvents(out PartyListenerEvent[] updates)
		{
			updates = new PartyListenerEvent[this.m_partyListenerEvents.Count];
			this.m_partyListenerEvents.CopyTo(updates);
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x00071E2E File Offset: 0x0007002E
		public void ClearPartyListenerEvents()
		{
			this.m_partyListenerEvents.Clear();
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00071E3C File Offset: 0x0007003C
		private void PushPartyErrorEvent(PartyListenerEventType evtType, string szDebugContext, Error error, BnetFeatureEvent featureEvent, ChannelId channelId = null, string szStringData = null)
		{
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = evtType;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.UintData = error.Code;
			item.UlongData = (17179869184UL | (ulong)featureEvent);
			item.StringData = ((szDebugContext == null) ? "" : szDebugContext);
			item.StringData2 = ((szStringData == null) ? "" : szStringData);
			this.m_partyListenerEvents.Add(item);
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00071EBC File Offset: 0x000700BC
		public void CreateParty(string szPartyType, int privacyLevel, params bnet.protocol.v2.Attribute[] partyAttributes)
		{
			CreateChannelOptions createChannelOptions = new CreateChannelOptions();
			UniqueChannelType uniqueChannelType = new UniqueChannelType();
			List<bnet.protocol.v2.Attribute> list = new List<bnet.protocol.v2.Attribute>();
			uniqueChannelType.SetChannelType(szPartyType);
			uniqueChannelType.SetProgram(BnetProgramId.HEARTHSTONE.GetValue());
			createChannelOptions.SetType(uniqueChannelType);
			createChannelOptions.SetPrivacyLevel((PrivacyLevel)privacyLevel);
			createChannelOptions.SetAttribute(list);
			list.Add(ProtocolHelper.CreateAttributeV2("WTCG.Party.Type", szPartyType));
			foreach (bnet.protocol.v2.Attribute attribute in partyAttributes)
			{
				if (attribute != null)
				{
					list.Add(attribute);
				}
			}
			this.m_battleNet.Channel.CreateChannel(createChannelOptions);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00071F48 File Offset: 0x00070148
		public void JoinParty(ChannelId channelId, string szPartyType)
		{
			if (this.m_battleNet.Channel.GetChannelDescription(channelId) != null)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_PARTY_ALREADY_IN_PARTY, "JoinParty already in party", BnetFeatureEvent.Party_Join_Callback, channelId, szPartyType);
				return;
			}
			this.m_battleNet.Channel.Join(channelId);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00071F84 File Offset: 0x00070184
		public void LeaveParty(ChannelId channelId)
		{
			bool channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId) != null;
			string partyType = this.GetPartyType(channelId);
			if (!channelDescription)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "LeaveParty no PartyData", BnetFeatureEvent.Party_Leave_Callback, channelId, partyType);
				return;
			}
			this.m_battleNet.Channel.Leave(channelId);
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00071FD4 File Offset: 0x000701D4
		public void DissolveParty(ChannelId channelId)
		{
			bool channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId) != null;
			string partyType = this.GetPartyType(channelId);
			if (!channelDescription)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "DissolveParty no PartyData", BnetFeatureEvent.Party_Dissolve_Callback, channelId, partyType);
				return;
			}
			this.m_battleNet.Channel.DissolveChannel(channelId);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00072024 File Offset: 0x00070224
		public void SetPartyPrivacy(ChannelId channelId, int privacyLevel)
		{
			bool channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId) != null;
			string partyType = this.GetPartyType(channelId);
			if (!channelDescription)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "SetPartyPrivacy no PartyData", BnetFeatureEvent.Party_SetPrivacy_Callback, channelId, partyType);
				return;
			}
			this.m_battleNet.Channel.SetPrivacyLevel(channelId, (PrivacyLevel)privacyLevel);
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x00072074 File Offset: 0x00070274
		public void AssignPartyRole(ChannelId channelId, GameAccountHandle memberId, uint roleId)
		{
			bool channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId) != null;
			string partyType = this.GetPartyType(channelId);
			if (!channelDescription)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "AssignPartyRole no PartyData", BnetFeatureEvent.Party_AssignRole_Callback, channelId, partyType);
				return;
			}
			this.m_battleNet.Channel.AssignRole(channelId, memberId, new uint[]
			{
				roleId
			}.ToList<uint>());
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x000720D4 File Offset: 0x000702D4
		public void SendPartyInvite(ChannelId channelId, GameAccountHandle inviteeId, bool isReservation)
		{
			bool channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId) != null;
			string partyType = this.GetPartyType(channelId);
			if (!channelDescription)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "SendPartyInvite no PartyData", BnetFeatureEvent.Party_SendInvite_Callback, channelId, partyType);
				return;
			}
			this.m_battleNet.Channel.SendInvitation(channelId, inviteeId, null);
			this.m_battleNet.Channel.RemoveInviteRequestsFor(channelId, inviteeId, 0U);
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x00072138 File Offset: 0x00070338
		public void AcceptPartyInvite(ulong invitationId)
		{
			ChannelInvitation receivedInvite = this.m_battleNet.Channel.GetReceivedInvite(invitationId);
			if (receivedInvite == null)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "AcceptPartyInvite, no corresponding invitation.", BnetFeatureEvent.Party_AcceptInvite_Callback, null, null);
				return;
			}
			this.m_battleNet.Channel.AcceptInvitation(receivedInvite.Channel.Id, invitationId);
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0007218C File Offset: 0x0007038C
		public void DeclinePartyInvite(ulong invitationId)
		{
			ChannelInvitation receivedInvite = this.m_battleNet.Channel.GetReceivedInvite(invitationId);
			if (receivedInvite == null)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "DeclinePartyInvite, no corresponding invitation.", BnetFeatureEvent.Party_DeclineInvite_Callback, null, null);
				return;
			}
			this.m_battleNet.Channel.DeclineInvitation(receivedInvite.Channel.Id, invitationId);
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000721E0 File Offset: 0x000703E0
		public void RevokePartyInvite(ChannelId channelId, ulong invitationId)
		{
			bool channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId) != null;
			string partyType = this.GetPartyType(channelId);
			if (!channelDescription)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "RevokePartyInvite no PartyData", BnetFeatureEvent.Party_RevokeInvite_Callback, channelId, partyType);
				return;
			}
			this.m_battleNet.Channel.RevokeInvitation(channelId, invitationId);
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0007222F File Offset: 0x0007042F
		public void RequestPartyInvite(ChannelId channelId, GameAccountHandle whomToAskForApproval, GameAccountHandle whomToInvite)
		{
			this.m_battleNet.Channel.SendSuggestion(channelId, whomToAskForApproval, whomToInvite);
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00072244 File Offset: 0x00070444
		public void IgnoreInviteRequest(ChannelId channelId, GameAccountHandle requestedTargetId)
		{
			this.m_battleNet.Channel.RemoveInviteRequestsFor(channelId, requestedTargetId, 1U);
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x0007225C File Offset: 0x0007045C
		public void KickPartyMember(ChannelId channelId, GameAccountHandle memberId)
		{
			bool channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId) != null;
			string partyType = this.GetPartyType(channelId);
			if (!channelDescription)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "KickPartyMember no PartyData", BnetFeatureEvent.Party_KickMember_Callback, channelId, partyType);
				return;
			}
			this.m_battleNet.Channel.Kick(channelId, memberId);
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000722AC File Offset: 0x000704AC
		public void SendPartyChatMessage(ChannelId channelId, string message)
		{
			bool channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId) != null;
			string partyType = this.GetPartyType(channelId);
			if (!channelDescription)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "SendPartyChatMessage no PartyData", BnetFeatureEvent.Party_SendChatMessage_Callback, channelId, partyType);
				return;
			}
			this.m_battleNet.Channel.SendMessage(channelId, message, null);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000722FC File Offset: 0x000704FC
		public void ClearPartyAttribute(ChannelId channelId, string attributeKey)
		{
			bnet.protocol.v2.Attribute attribute = new bnet.protocol.v2.Attribute();
			Variant value = new Variant();
			attribute.SetName(attributeKey);
			attribute.SetValue(value);
			this.SetPartyAttribute_Internal("ClearPartyAttribute key=" + attributeKey, BnetFeatureEvent.Party_ClearAttribute_Callback, channelId, new bnet.protocol.v2.Attribute[]
			{
				attribute
			});
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00072344 File Offset: 0x00070544
		public void SetPartyAttributeLong(ChannelId channelId, string attributeKey, long value)
		{
			this.SetPartyAttribute_Internal(string.Concat(new object[]
			{
				"SetPartyAttributeLong key=",
				attributeKey,
				" val=",
				value
			}), BnetFeatureEvent.Party_SetAttribute_Callback, channelId, new bnet.protocol.v2.Attribute[]
			{
				ProtocolHelper.CreateAttributeV2(attributeKey, value)
			});
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00072394 File Offset: 0x00070594
		public void SetPartyAttributeString(ChannelId channelId, string attributeKey, string value)
		{
			this.SetPartyAttribute_Internal("SetPartyAttributeString key=" + attributeKey + " val=" + value, BnetFeatureEvent.Party_SetAttribute_Callback, channelId, new bnet.protocol.v2.Attribute[]
			{
				ProtocolHelper.CreateAttributeV2(attributeKey, value)
			});
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000723CC File Offset: 0x000705CC
		public void SetPartyAttributeBlob(ChannelId channelId, string attributeKey, byte[] value)
		{
			this.SetPartyAttribute_Internal("SetPartyAttributeBlob key=" + attributeKey + " val=" + ((value == null) ? "null" : (value.Length + " bytes")), BnetFeatureEvent.Party_SetAttribute_Callback, channelId, new bnet.protocol.v2.Attribute[]
			{
				ProtocolHelper.CreateAttributeV2(attributeKey, value)
			});
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00072420 File Offset: 0x00070620
		public void SetPartyAttributes(ChannelId channelId, params bnet.protocol.v2.Attribute[] attrs)
		{
			if (attrs == null || attrs.Length == 0)
			{
				return;
			}
			this.SetPartyAttribute_Internal("SetPartyAttributes key=" + attrs.First<bnet.protocol.v2.Attribute>().Name + " val=" + ((attrs.First<bnet.protocol.v2.Attribute>().Value == null) ? "null" : (attrs.First<bnet.protocol.v2.Attribute>().Value.IsNone() ? "none" : attrs.First<bnet.protocol.v2.Attribute>().Value.ToString())), BnetFeatureEvent.Party_SetAttribute_Callback, channelId, attrs);
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00072496 File Offset: 0x00070696
		private void SetPartyAttribute_Internal(string debugMessage, BnetFeatureEvent featureEvent, ChannelId channelId, params bnet.protocol.v2.Attribute[] attrs)
		{
			this.UpdatePartyState_Internal(debugMessage, featureEvent, channelId, attrs.ToList<bnet.protocol.v2.Attribute>());
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000724A8 File Offset: 0x000706A8
		private void UpdatePartyState_Internal(string debugMessage, BnetFeatureEvent featureEvent, ChannelId channelId, List<bnet.protocol.v2.Attribute> attributes)
		{
			bool channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId) != null;
			string partyType = this.GetPartyType(channelId);
			if (!channelDescription)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "{0} no PartyData", featureEvent, channelId, partyType);
				return;
			}
			this.m_battleNet.Channel.SetChannelAttributes(channelId, attributes);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000724F8 File Offset: 0x000706F8
		public void ClearMemberAttribute(ChannelId channelId, GameAccountHandle member, string attributeKey)
		{
			bnet.protocol.v2.Attribute attribute = new bnet.protocol.v2.Attribute();
			Variant value = new Variant();
			attribute.SetName(attributeKey);
			attribute.SetValue(value);
			this.SetMemberAttribute_Internal("ClearMemberAttribute key=" + attributeKey, BnetFeatureEvent.Party_ClearAttribute_Callback, channelId, member, new bnet.protocol.v2.Attribute[]
			{
				attribute
			});
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00072540 File Offset: 0x00070740
		public void SetMemberAttributeLong(ChannelId channelId, GameAccountHandle member, string attributeKey, long value)
		{
			this.SetMemberAttribute_Internal(string.Concat(new object[]
			{
				"SetMemberAttributeLong key=",
				attributeKey,
				" val=",
				value
			}), BnetFeatureEvent.Party_SetAttribute_Callback, channelId, member, new bnet.protocol.v2.Attribute[]
			{
				ProtocolHelper.CreateAttributeV2(attributeKey, value)
			});
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00072594 File Offset: 0x00070794
		public void SetMemberAttributeString(ChannelId channelId, GameAccountHandle member, string attributeKey, string value)
		{
			this.SetMemberAttribute_Internal("SetMemberAttributeString key=" + attributeKey + " val=" + value, BnetFeatureEvent.Party_SetAttribute_Callback, channelId, member, new bnet.protocol.v2.Attribute[]
			{
				ProtocolHelper.CreateAttributeV2(attributeKey, value)
			});
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x000725D0 File Offset: 0x000707D0
		public void SetMemberAttributeBlob(ChannelId channelId, GameAccountHandle member, string attributeKey, byte[] value)
		{
			this.SetMemberAttribute_Internal("SetMemberAttributeBlob key=" + attributeKey + " val=" + ((value == null) ? "null" : (value.Length + " bytes")), BnetFeatureEvent.Party_SetAttribute_Callback, channelId, member, new bnet.protocol.v2.Attribute[]
			{
				ProtocolHelper.CreateAttributeV2(attributeKey, value)
			});
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00072628 File Offset: 0x00070828
		public void SetMemberAttributes(ChannelId channelId, GameAccountHandle member, params bnet.protocol.v2.Attribute[] attrs)
		{
			if (attrs == null || attrs.Length == 0)
			{
				return;
			}
			this.SetMemberAttribute_Internal("SetMemberAttributes key=" + attrs.First<bnet.protocol.v2.Attribute>().Name + " val=" + ((attrs.First<bnet.protocol.v2.Attribute>().Value == null) ? "null" : (attrs.First<bnet.protocol.v2.Attribute>().Value.IsNone() ? "none" : attrs.First<bnet.protocol.v2.Attribute>().Value.ToString())), BnetFeatureEvent.Party_SetAttribute_Callback, channelId, member, attrs);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x0007269F File Offset: 0x0007089F
		private void SetMemberAttribute_Internal(string debugMessage, BnetFeatureEvent featureEvent, ChannelId channelId, GameAccountHandle member, params bnet.protocol.v2.Attribute[] attrs)
		{
			this.UpdateMemberState_Internal(debugMessage, featureEvent, channelId, member, attrs.ToList<bnet.protocol.v2.Attribute>());
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000726B4 File Offset: 0x000708B4
		private void UpdateMemberState_Internal(string debugMessage, BnetFeatureEvent featureEvent, ChannelId channelId, GameAccountHandle member, List<bnet.protocol.v2.Attribute> attributes)
		{
			bool channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId) != null;
			string partyType = this.GetPartyType(channelId);
			if (!channelDescription)
			{
				this.GenericPartyRequestCallback_Internal(BattleNetErrors.ERROR_RPC_INVALID_OBJECT, "{0} no PartyData", featureEvent, channelId, partyType);
				return;
			}
			AttributeAssignment attributeAssignment = new AttributeAssignment();
			attributeAssignment.SetMemberId(member);
			attributeAssignment.SetAttribute(attributes);
			this.m_battleNet.Channel.SetMemberAttribute(channelId, attributeAssignment);
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00072718 File Offset: 0x00070918
		public int GetPartyPrivacy(ChannelId channelId)
		{
			int result = 4;
			ChannelDescription channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId);
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

		// Token: 0x06002036 RID: 8246 RVA: 0x00072750 File Offset: 0x00070950
		public uint GetCountPartyMembers(ChannelId channelId)
		{
			ChannelDescription channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription != null && channelDescription.HasMemberCount)
			{
				return channelDescription.MemberCount;
			}
			return 0U;
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00072782 File Offset: 0x00070982
		public uint GetMaxPartyMembers(ChannelId channelId)
		{
			return 100U;
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00072788 File Offset: 0x00070988
		public void GetPartyMembers(ChannelId channelId, out PartyMember[] members)
		{
			members = null;
			Channel partyData = this.GetPartyData(channelId);
			if (partyData != null && partyData.MemberCount > 0)
			{
				members = new PartyMember[partyData.MemberCount];
				int num = 0;
				foreach (Member member in partyData.MemberList)
				{
					PartyMember partyMember = default(PartyMember);
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
				members = new PartyMember[0];
			}
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00072870 File Offset: 0x00070A70
		public string GetReceivedInvitationPartyType(ulong invitationId)
		{
			string result = "";
			ChannelInvitation receivedInvite = this.m_battleNet.Channel.GetReceivedInvite(invitationId);
			if (receivedInvite != null && receivedInvite.HasChannel && receivedInvite.Channel.HasType)
			{
				result = receivedInvite.Channel.Type.ChannelType;
			}
			return result;
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000728C0 File Offset: 0x00070AC0
		public bnet.protocol.v2.Attribute GetReceivedInvitationAttribute(ChannelId channelId, string attributeKey)
		{
			ChannelInvitation channelInvitation = this.m_battleNet.Channel.GetAllReceivedInvites().FirstOrDefault((ChannelInvitation i) => i.HasChannel && i.Channel.HasId && i.Channel.Id.Equals(channelId));
			if (channelInvitation != null && channelInvitation.HasChannel)
			{
				ChannelDescription channel = channelInvitation.Channel;
				for (int j = 0; j < channel.AttributeList.Count; j++)
				{
					bnet.protocol.v2.Attribute attribute = channel.AttributeList[j];
					if (attribute.Name == attributeKey)
					{
						return attribute;
					}
				}
			}
			return null;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00072948 File Offset: 0x00070B48
		public void GetReceivedPartyInvites(out PartyInvite[] invites)
		{
			ChannelInvitation[] allReceivedInvites = this.m_battleNet.Channel.GetAllReceivedInvites();
			invites = new PartyInvite[allReceivedInvites.Length];
			for (int i = 0; i < invites.Length; i++)
			{
				ChannelInvitation channelInvitation = allReceivedInvites[i];
				PartyType partyTypeFromString = BnetParty.GetPartyTypeFromString(this.GetReceivedInvitationPartyType(channelInvitation.Id));
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

		// Token: 0x0600203C RID: 8252 RVA: 0x00072A28 File Offset: 0x00070C28
		private Channel GetPartyState(ChannelId channelId)
		{
			Channel result;
			this.m_activeParties.TryGetValue(channelId.Id, out result);
			return result;
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00072A4C File Offset: 0x00070C4C
		public void GetPartySentChannelInvitations(ChannelId channelId, out ChannelInvitation[] invites)
		{
			invites = null;
			Channel partyState = this.GetPartyState(channelId);
			if (partyState != null)
			{
				invites = partyState.InvitationList.ToArray();
			}
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00072A74 File Offset: 0x00070C74
		public void GetPartySentInvites(ChannelId channelId, out PartyInvite[] invites)
		{
			invites = null;
			Channel partyState = this.GetPartyState(channelId);
			if (partyState != null)
			{
				invites = new PartyInvite[partyState.InvitationCount];
				PartyType partyTypeFromString = BnetParty.GetPartyTypeFromString(this.GetPartyType(channelId));
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

		// Token: 0x0600203F RID: 8255 RVA: 0x00072B5C File Offset: 0x00070D5C
		public void GetPartyInviteRequests(ChannelId channelId, out InviteRequest[] requests)
		{
			Suggestion[] receivedInviteRequestsForChannel = this.m_battleNet.Channel.GetReceivedInviteRequestsForChannel(channelId);
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

		// Token: 0x06002040 RID: 8256 RVA: 0x00072BF4 File Offset: 0x00070DF4
		public void GetAllPartyAttributes(ChannelId channelId, out string[] allKeys)
		{
			ChannelInvitation channelInvitation = this.m_battleNet.Channel.GetAllReceivedInvites().FirstOrDefault((ChannelInvitation i) => i.HasChannel && i.Channel.HasId && i.Channel.Id.Equals(channelId));
			ChannelDescription channelDescription;
			if (channelInvitation != null && channelInvitation.HasChannel)
			{
				channelDescription = channelInvitation.Channel;
			}
			else
			{
				channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId);
			}
			if (channelDescription == null)
			{
				allKeys = new string[0];
				return;
			}
			allKeys = new string[channelDescription.AttributeList.Count];
			for (int j = 0; j < channelDescription.AttributeList.Count; j++)
			{
				bnet.protocol.v2.Attribute attribute = channelDescription.AttributeList[j];
				allKeys[j] = attribute.Name;
			}
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x00072CA8 File Offset: 0x00070EA8
		public bool GetPartyAttributeLong(ChannelId channelId, string attributeKey, out long value)
		{
			value = 0L;
			bnet.protocol.v2.Attribute attribute = this.GetReceivedInvitationAttribute(channelId, attributeKey);
			if (attribute != null && attribute.Value.HasIntValue)
			{
				value = attribute.Value.IntValue;
				return true;
			}
			ChannelDescription channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription != null)
			{
				for (int i = 0; i < channelDescription.AttributeList.Count; i++)
				{
					attribute = channelDescription.AttributeList[i];
					if (attribute.Name == attributeKey && attribute.Value.HasIntValue)
					{
						value = attribute.Value.IntValue;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00072D44 File Offset: 0x00070F44
		public void GetPartyAttributeString(ChannelId channelId, string attributeKey, out string value)
		{
			value = null;
			bnet.protocol.v2.Attribute attribute = this.GetReceivedInvitationAttribute(channelId, attributeKey);
			if (attribute != null && attribute.Value.HasStringValue)
			{
				value = attribute.Value.StringValue;
				return;
			}
			ChannelDescription channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription != null)
			{
				for (int i = 0; i < channelDescription.AttributeList.Count; i++)
				{
					attribute = channelDescription.AttributeList[i];
					if (attribute.Name == attributeKey && attribute.Value.HasStringValue)
					{
						value = attribute.Value.StringValue;
						return;
					}
				}
			}
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x00072DDC File Offset: 0x00070FDC
		public void GetPartyAttributeBlob(ChannelId channelId, string attributeKey, out byte[] value)
		{
			value = null;
			bnet.protocol.v2.Attribute attribute = this.GetReceivedInvitationAttribute(channelId, attributeKey);
			if (attribute != null && attribute.Value.HasBlobValue)
			{
				value = attribute.Value.BlobValue;
				return;
			}
			ChannelDescription channelDescription = this.m_battleNet.Channel.GetChannelDescription(channelId);
			if (channelDescription != null)
			{
				for (int i = 0; i < channelDescription.AttributeList.Count; i++)
				{
					attribute = channelDescription.AttributeList[i];
					if (attribute.Name == attributeKey && attribute.Value.HasBlobValue)
					{
						value = attribute.Value.BlobValue;
						return;
					}
				}
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00072E74 File Offset: 0x00071074
		public void GetMemberAttributeString(ChannelId channelId, GameAccountHandle partyMember, string attributeKey, out string value)
		{
			value = null;
			Channel partyData = this.GetPartyData(channelId);
			if (partyData == null)
			{
				return;
			}
			Member member = partyData.MemberList.FirstOrDefault((Member i) => i.HasId && i.Id.Equals(partyMember));
			if (member != null)
			{
				bnet.protocol.v2.Attribute attribute = member.AttributeList.FirstOrDefault((bnet.protocol.v2.Attribute i) => i.HasName && i.Name.Equals(attributeKey));
				if (attribute != null && attribute.HasValue && attribute.Value.HasStringValue)
				{
					value = attribute.Value.StringValue;
				}
			}
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x00072F00 File Offset: 0x00071100
		public void GetMemberAttributeBlob(ChannelId channelId, GameAccountHandle partyMember, string attributeKey, out byte[] value)
		{
			value = null;
			Channel partyData = this.GetPartyData(channelId);
			if (partyData == null)
			{
				return;
			}
			Member member = partyData.MemberList.FirstOrDefault((Member i) => i.HasId && i.Id.Equals(partyMember));
			if (member != null)
			{
				bnet.protocol.v2.Attribute attribute = member.AttributeList.FirstOrDefault((bnet.protocol.v2.Attribute i) => i.HasName && i.Name.Equals(attributeKey));
				if (attribute != null && attribute.HasValue && attribute.Value.HasBlobValue)
				{
					value = attribute.Value.BlobValue;
				}
			}
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00072F8C File Offset: 0x0007118C
		public void PartyCreated(RPCContext context)
		{
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			CreateChannelRequest createChannelRequest = (CreateChannelRequest)context.Request;
			this.m_battleNet.Party.PushPartyErrorEvent(PartyListenerEventType.OPERATION_CALLBACK, "CreateParty", status, BnetFeatureEvent.Party_Create_Callback, null, createChannelRequest.Options.Type.ChannelType + createChannelRequest.Options.Type.Program);
			if (status != BattleNetErrors.ERROR_OK)
			{
				this.m_battleNet.Party.ApiLog.LogError("CreateChannelCallback: code=" + new Error(status));
				return;
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00073029 File Offset: 0x00071229
		public void PartyJoined(ChannelDescription channelDescription, bnet.protocol.channel.v2.SubscribeResponse subscribeResponse)
		{
			this.m_battleNet.Party.AddActiveChannel(channelDescription.Id, subscribeResponse.Channel, delegate(RPCContext ctx)
			{
				GetChannelResponse getChannelResponse = GetChannelResponse.ParseFrom(ctx.Payload);
				this.AddPartyFromChannel(getChannelResponse.Channel);
			});
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00073054 File Offset: 0x00071254
		public void PartyLeft(ChannelId channelId, ChannelRemovedNotification notification)
		{
			string partyType = this.GetPartyType(channelId);
			if (this.GetPartyData(channelId) != null)
			{
				this.m_activeParties.Remove(channelId.Id);
			}
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.LEFT_PARTY;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.StringData = partyType;
			item.UintData = (uint)(notification.HasReason ? notification.Reason : ChannelRemovedReason.CHANNEL_REMOVED_REASON_MEMBER_LEFT);
			this.m_partyListenerEvents.Add(item);
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000730D0 File Offset: 0x000712D0
		public void PartyMemberJoined(ChannelId channelId, MemberAddedNotification notification)
		{
			Channel partyData = this.GetPartyData(channelId);
			if (partyData != null)
			{
				partyData.AddMember(notification.Member);
			}
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.MEMBER_JOINED;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.Member.Id);
			this.m_partyListenerEvents.Add(item);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00073134 File Offset: 0x00071334
		public void MemberRolesChanged(ChannelId channelId, MemberRoleChangedNotification notification)
		{
			List<GameAccountHandle> list = new List<GameAccountHandle>();
			foreach (RoleAssignment roleAssignment in notification.AssignmentList)
			{
				this.UpdateMemberRole(channelId, roleAssignment);
				list.Add(roleAssignment.MemberId);
			}
			string partyType = this.GetPartyType(channelId);
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.MEMBER_ROLE_CHANGED;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.StringData = partyType;
			foreach (GameAccountHandle handle in list)
			{
				item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(handle);
				this.m_partyListenerEvents.Add(item);
			}
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0007321C File Offset: 0x0007141C
		public void MemberAttributesChanged(ChannelId channelId, MemberAttributeChangedNotification notification)
		{
			if (!notification.HasAssignment || !notification.Assignment.HasMemberId)
			{
				return;
			}
			this.UpdateMemberAttribute(channelId, notification.Assignment);
			foreach (bnet.protocol.v2.Attribute attribute in notification.Assignment.AttributeList)
			{
				PartyListenerEvent item = default(PartyListenerEvent);
				item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.Assignment.MemberId);
				item.Type = PartyListenerEventType.MEMBER_ATTRIBUTE_CHANGED;
				item.PartyId = PartyId.FromChannelId(channelId);
				item.StringData = attribute.Name;
				if (attribute.Value.IsNone())
				{
					item.UintData = 0U;
				}
				else if (attribute.Value.HasIntValue)
				{
					item.UintData = 1U;
					item.UlongData = (ulong)attribute.Value.IntValue;
				}
				else if (attribute.Value.HasStringValue)
				{
					item.UintData = 2U;
					item.StringData2 = attribute.Value.StringValue;
				}
				else if (attribute.Value.HasBlobValue)
				{
					item.UintData = 3U;
					item.BlobData = attribute.Value.BlobValue;
				}
				else
				{
					item.UintData = 0U;
				}
				this.m_partyListenerEvents.Add(item);
			}
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00073390 File Offset: 0x00071590
		public void PartyMemberLeft(ChannelId channelId, MemberRemovedNotification notification)
		{
			Channel partyData = this.GetPartyData(channelId);
			if (partyData != null)
			{
				Member item = partyData.MemberList.FirstOrDefault((Member i) => i.HasId && i.Id.Equals(notification.MemberId));
				partyData.Member.Remove(item);
			}
			PartyListenerEvent item2 = default(PartyListenerEvent);
			item2.Type = PartyListenerEventType.MEMBER_LEFT;
			item2.PartyId = PartyId.FromChannelId(channelId);
			item2.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.MemberId);
			item2.UintData = (notification.HasReason ? notification.Reason : 0U);
			this.m_partyListenerEvents.Add(item2);
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0007343C File Offset: 0x0007163C
		public void ReceivedInvitationAdded(ReceivedInvitationAddedNotification notification, ChannelInvitation channelInvitation)
		{
			string receivedInvitationPartyType = this.GetReceivedInvitationPartyType(notification.Invitation.Id);
			PartyListenerEvent partyListenerEvent = default(PartyListenerEvent);
			partyListenerEvent.Type = PartyListenerEventType.RECEIVED_INVITE_ADDED;
			partyListenerEvent.PartyId = PartyId.FromChannelId(channelInvitation.Channel.Id);
			partyListenerEvent.UlongData = notification.Invitation.Id;
			partyListenerEvent.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.Invitation.Inviter.Id);
			partyListenerEvent.TargetMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.Invitation.Invitee.Id);
			partyListenerEvent.StringData = receivedInvitationPartyType;
			partyListenerEvent.StringData2 = notification.Invitation.Inviter.BattleTag;
			partyListenerEvent.UintData = 0U;
			if (channelInvitation.HasSlot && channelInvitation.Slot.HasReserved && channelInvitation.Slot.Reserved)
			{
				partyListenerEvent.UintData |= 1U;
			}
			if (channelInvitation.HasSlot && channelInvitation.Slot.HasRejoin && channelInvitation.Slot.Rejoin)
			{
				partyListenerEvent.UintData |= 1U;
			}
			this.m_partyListenerEvents.Add(partyListenerEvent);
			this.m_battleNet.Party.ApiLog.LogInfo("PartyAPI.ReceivedInvitationAdded: id={0} party={1}:{2} isRejoin={2} isReservation={3} inviter={4} invitee={5}", new object[]
			{
				notification.Invitation.Id,
				receivedInvitationPartyType,
				partyListenerEvent.PartyId.Lo,
				channelInvitation.HasSlot && channelInvitation.Slot.HasRejoin && channelInvitation.Slot.Rejoin,
				channelInvitation.HasSlot && channelInvitation.Slot.HasReserved && channelInvitation.Slot.Reserved,
				partyListenerEvent.SubjectMemberId.GetLo(),
				partyListenerEvent.TargetMemberId.GetLo()
			});
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00073624 File Offset: 0x00071824
		public void ReceivedInvitationRemoved(ReceivedInvitationRemovedNotification notification, ChannelInvitation channelInvitation)
		{
			string receivedInvitationPartyType = this.GetReceivedInvitationPartyType(notification.InvitationId);
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
			this.m_partyListenerEvents.Add(item);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000736DC File Offset: 0x000718DC
		public void PartyInvitationDelta(ChannelId channelId, ChannelInvitation invite, uint? removeReason)
		{
			Channel partyState = this.GetPartyState(channelId);
			if (partyState == null)
			{
				return;
			}
			string partyType = this.GetPartyType(channelId);
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = ((removeReason != null) ? PartyListenerEventType.PARTY_INVITE_REMOVED : PartyListenerEventType.PARTY_INVITE_SENT);
			item.PartyId = PartyId.FromChannelId(channelId);
			item.UlongData = invite.Id;
			item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(invite.Inviter.Id);
			item.TargetMemberId = BnetGameAccountId.CreateFromGameAccountHandle(invite.Invitee.Id);
			item.StringData = partyType;
			item.StringData2 = invite.Inviter.BattleTag;
			if (removeReason != null)
			{
				item.UintData = removeReason.Value;
				List<ChannelInvitation> invitationList = partyState.InvitationList;
				invitationList.Remove(invite);
				partyState.SetInvitation(invitationList);
			}
			else
			{
				item.UintData = 0U;
				if (invite.HasSlot && invite.Slot.HasReserved && invite.Slot.Reserved)
				{
					item.UintData |= 1U;
				}
				if (invite.HasSlot && invite.Slot.HasReserved && invite.Slot.Rejoin)
				{
					item.UintData |= 1U;
				}
				if (!partyState.InvitationList.Contains(invite))
				{
					partyState.AddInvitation(invite);
				}
			}
			this.m_partyListenerEvents.Add(item);
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00073834 File Offset: 0x00071A34
		public void ReceivedInviteRequestDelta(ChannelId channelId, Suggestion suggestion, uint? removeReason)
		{
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = ((removeReason != null) ? PartyListenerEventType.INVITE_REQUEST_REMOVED : PartyListenerEventType.INVITE_REQUEST_ADDED);
			item.PartyId = PartyId.FromChannelId(channelId);
			item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(suggestion.Suggester.Id);
			item.TargetMemberId = BnetGameAccountId.CreateFromGameAccountHandle(suggestion.Suggestee.Id);
			item.StringData = suggestion.Suggester.BattleTag;
			item.StringData2 = suggestion.Suggestee.BattleTag;
			if (removeReason != null)
			{
				item.UintData = removeReason.Value;
			}
			this.m_partyListenerEvents.Add(item);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000738E4 File Offset: 0x00071AE4
		public void PartyMessageReceived(ChannelId channelId, SendMessageNotification notification)
		{
			string text = null;
			for (int i = 0; i < notification.Message.AttributeCount; i++)
			{
				bnet.protocol.v2.Attribute attribute = notification.Message.AttributeList[i];
				if (attribute.TEXT_ATTRIBUTE.Equals(attribute.Name) && attribute.Value.HasStringValue)
				{
					text = attribute.Value.StringValue;
					break;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.CHAT_MESSAGE_RECEIVED;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.SubjectMemberId = BnetGameAccountId.CreateFromGameAccountHandle(notification.AgentId);
			item.StringData = text;
			this.m_partyListenerEvents.Add(item);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00003FD0 File Offset: 0x000021D0
		public void PartyTypingIndicatorUpdateReceived(ChannelId channelId, TypingIndicatorNotification notification)
		{
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00073998 File Offset: 0x00071B98
		public void PartyPrivacyChanged(ChannelId channelId, PrivacyLevel newPrivacyLevel)
		{
			string partyType = this.GetPartyType(channelId);
			PartyListenerEvent item = default(PartyListenerEvent);
			item.Type = PartyListenerEventType.PRIVACY_CHANGED;
			item.PartyId = PartyId.FromChannelId(channelId);
			item.UintData = (uint)newPrivacyLevel;
			item.StringData = partyType;
			this.m_partyListenerEvents.Add(item);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000739E8 File Offset: 0x00071BE8
		public void PartyAttributeChanged(ChannelId channelId, bnet.protocol.v2.Attribute[] attributes)
		{
			foreach (bnet.protocol.v2.Attribute attribute in attributes)
			{
				PartyListenerEvent item = default(PartyListenerEvent);
				item.Type = PartyListenerEventType.PARTY_ATTRIBUTE_CHANGED;
				item.PartyId = PartyId.FromChannelId(channelId);
				item.StringData = attribute.Name;
				if (attribute.Value.IsNone())
				{
					item.UintData = 0U;
				}
				else if (attribute.Value.HasIntValue)
				{
					item.UintData = 1U;
					item.UlongData = (ulong)attribute.Value.IntValue;
				}
				else if (attribute.Value.HasStringValue)
				{
					item.UintData = 2U;
					item.StringData2 = attribute.Value.StringValue;
				}
				else if (attribute.Value.HasBlobValue)
				{
					item.UintData = 3U;
					item.BlobData = attribute.Value.BlobValue;
				}
				else
				{
					item.UintData = 0U;
				}
				this.m_partyListenerEvents.Add(item);
			}
		}

		// Token: 0x04000B98 RID: 2968
		public static string PARTY_TYPE_DEFAULT = "default";

		// Token: 0x04000B99 RID: 2969
		private List<PartyEvent> m_partyEvents = new List<PartyEvent>();

		// Token: 0x04000B9A RID: 2970
		private List<PartyListenerEvent> m_partyListenerEvents = new List<PartyListenerEvent>();

		// Token: 0x04000B9B RID: 2971
		private Map<uint, Channel> m_activeParties = new Map<uint, Channel>();

		// Token: 0x020006A5 RID: 1701
		public struct PartyCreateOptions
		{
			// Token: 0x040021EB RID: 8683
			public string m_name;

			// Token: 0x040021EC RID: 8684
			public PrivacyLevel m_privacyLevel;
		}
	}
}
