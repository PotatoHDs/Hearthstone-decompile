using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using bgs.types;
using bnet.protocol.account.v1;
using bnet.protocol.matchmaking.v1;
using bnet.protocol.v2;

namespace bgs
{
	// Token: 0x02000212 RID: 530
	public class BattleNet
	{
		// Token: 0x060020B3 RID: 8371 RVA: 0x000763E3 File Offset: 0x000745E3
		public static IBattleNet Get()
		{
			return BattleNet.s_impl;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000763EA File Offset: 0x000745EA
		public static ClientInterface Client()
		{
			return BattleNet.s_impl.Client();
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000763F6 File Offset: 0x000745F6
		public static bool IsInitialized()
		{
			return BattleNet.s_impl != null && BattleNet.s_impl.IsInitialized();
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0007640B File Offset: 0x0007460B
		public static void SetImpl(IBattleNet battleNet)
		{
			BattleNet.s_impl = battleNet;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00076413 File Offset: 0x00074613
		public static bool Init(IBattleNet battleNet, bool internalMode, string userEmailAddress, string targetServer, uint port, SslParameters sslParams)
		{
			if (BattleNet.s_impl == null)
			{
				BattleNet.s_impl = battleNet;
			}
			return BattleNet.s_impl.Init(internalMode, userEmailAddress, targetServer, port, sslParams);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00076433 File Offset: 0x00074633
		public static bool Reset(IBattleNet battleNet, bool internalMode, string userEmailAddress, string targetServer, uint port, SslParameters sslParams)
		{
			BattleNet.RequestCloseAurora();
			BattleNet.s_impl = null;
			return BattleNet.Init(battleNet, internalMode, userEmailAddress, targetServer, port, sslParams);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0007644D File Offset: 0x0007464D
		public static void AppQuit()
		{
			BattleNet.s_impl.AppQuit();
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00076459 File Offset: 0x00074659
		public static bool IsConnected()
		{
			return BattleNet.s_impl != null && BattleNet.s_impl.IsConnected();
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0007646E File Offset: 0x0007466E
		public static void ProcessAurora()
		{
			BattleNet.s_impl.ProcessAurora();
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0007647A File Offset: 0x0007467A
		public static void QueryAurora()
		{
			BattleNet.s_impl.QueryAurora();
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00076486 File Offset: 0x00074686
		public static void CloseAurora()
		{
			BattleNet.s_impl.CloseAurora();
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x00076492 File Offset: 0x00074692
		public static void RequestCloseAurora()
		{
			BattleNet.s_impl.RequestCloseAurora();
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0007649E File Offset: 0x0007469E
		public static int BattleNetStatus()
		{
			return BattleNet.s_impl.BattleNetStatus();
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x000764AA File Offset: 0x000746AA
		public static void SendUtilPacket(int packetId, UtilSystemId systemId, byte[] bytes, int size, int subID, int context, ulong route)
		{
			BattleNet.s_impl.SendUtilPacket(packetId, systemId, bytes, size, subID, context, route);
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000764C0 File Offset: 0x000746C0
		public static GamesAPI.UtilResponse NextUtilPacket()
		{
			return BattleNet.s_impl.NextUtilPacket();
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000764CC File Offset: 0x000746CC
		public static int GetErrorsCount()
		{
			return BattleNet.s_impl.GetErrorsCount();
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000764D8 File Offset: 0x000746D8
		public static void GetErrors([Out] BnetErrorInfo[] errors)
		{
			BattleNet.s_impl.GetErrors(errors);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000764E5 File Offset: 0x000746E5
		public static void ClearErrors()
		{
			BattleNet.s_impl.ClearErrors();
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000764F1 File Offset: 0x000746F1
		public static EntityId GetMyGameAccountId()
		{
			return BattleNet.s_impl.GetMyGameAccountId();
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000764FD File Offset: 0x000746FD
		public static EntityId GetMyAccoundId()
		{
			return BattleNet.s_impl.GetMyAccountId();
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x00076509 File Offset: 0x00074709
		public static void GetQueueInfo(ref QueueInfo queueInfo)
		{
			BattleNet.s_impl.GetQueueInfo(ref queueInfo);
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x00076516 File Offset: 0x00074716
		public static BattleNetLogSource Log
		{
			get
			{
				return BattleNet.s_impl.GetLogSource();
			}
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x00076522 File Offset: 0x00074722
		public static string GetVersion()
		{
			return BattleNet.s_impl.GetVersion();
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0007652E File Offset: 0x0007472E
		public static int GetDataVersion()
		{
			return BattleNet.s_impl.GetDataVersion();
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x0007653A File Offset: 0x0007473A
		public static string GetEnvironment()
		{
			return BattleNet.s_impl.GetEnvironment();
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x00076546 File Offset: 0x00074746
		public static uint GetPort()
		{
			return BattleNet.s_impl.GetPort();
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00076552 File Offset: 0x00074752
		public static string GetUserEmailAddress()
		{
			return BattleNet.s_impl.GetUserEmailAddress();
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0007655E File Offset: 0x0007475E
		public static double GetRealTimeSinceStartup()
		{
			return BattleNet.s_impl.GetRealTimeSinceStartup();
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x0007656A File Offset: 0x0007476A
		public static AuthenticationAPI.MemModuleLoadRequest NextMemModuleRequest()
		{
			return BattleNet.s_impl.NextMemModuleRequest();
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00076576 File Offset: 0x00074776
		public static void SendMemModuleResponse(AuthenticationAPI.MemModuleLoadRequest request, byte[] memModuleResponseBytes)
		{
			BattleNet.s_impl.SendMemModuleResponse(request, memModuleResponseBytes);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x00076584 File Offset: 0x00074784
		public static string GetLaunchOption(string key, bool encrypted)
		{
			return LaunchOptionHelper.GetLaunchOption(key, encrypted, null);
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x0007658E File Offset: 0x0007478E
		public static string GetConnectionString()
		{
			return BattleNet.s_impl.GetConnectionString();
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x0007659A File Offset: 0x0007479A
		public static constants.BnetRegion GetAccountRegion()
		{
			return BattleNet.s_impl.GetAccountRegion();
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000765A6 File Offset: 0x000747A6
		public static string GetAccountCountry()
		{
			return BattleNet.s_impl.GetAccountCountry();
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x000765B2 File Offset: 0x000747B2
		public static bool IsHeadlessAccount()
		{
			return BattleNet.s_impl.IsHeadlessAccount();
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000765BE File Offset: 0x000747BE
		public static constants.BnetRegion GetCurrentRegion()
		{
			if (BattleNet.s_impl != null)
			{
				return BattleNet.s_impl.GetCurrentRegion();
			}
			return constants.BnetRegion.REGION_UNINITIALIZED;
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000765D3 File Offset: 0x000747D3
		public static void GetPlayRestrictions(ref Lockouts restrictions, bool reload)
		{
			BattleNet.s_impl.GetPlayRestrictions(ref restrictions, reload);
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000765E1 File Offset: 0x000747E1
		public static void GetAccountState(BnetAccountId bnetAccount)
		{
			BattleNet.s_impl.GetAccountState(bnetAccount);
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000765EE File Offset: 0x000747EE
		public static int GetShutdownMinutes()
		{
			return BattleNet.s_impl.GetShutdownMinutes();
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000765FA File Offset: 0x000747FA
		public static int GetBnetEventsSize()
		{
			return BattleNet.s_impl.GetBnetEventsSize();
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x00076606 File Offset: 0x00074806
		public static void ClearBnetEvents()
		{
			BattleNet.s_impl.ClearBnetEvents();
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x00076612 File Offset: 0x00074812
		public static void GetBnetEvents([Out] BattleNet.BnetEvent[] events)
		{
			BattleNet.s_impl.GetBnetEvents(events);
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0007661F File Offset: 0x0007481F
		public static void QueueMatchmaking(List<bnet.protocol.v2.Attribute> matchmakerAttributesFilter, List<bnet.protocol.v2.Attribute> gameAttributes, params Player[] players)
		{
			BattleNet.s_impl.QueueMatchmaking(matchmakerAttributesFilter, gameAttributes, players);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x0007662E File Offset: 0x0007482E
		public static void CancelMatchmaking()
		{
			BattleNet.s_impl.CancelMatchmaking();
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x0007663A File Offset: 0x0007483A
		public static QueueEvent GetQueueEvent()
		{
			return BattleNet.s_impl.GetQueueEvent();
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00076646 File Offset: 0x00074846
		public static int PresenceSize()
		{
			return BattleNet.s_impl.PresenceSize();
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00076652 File Offset: 0x00074852
		public static void ClearPresence()
		{
			BattleNet.s_impl.ClearPresence();
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x0007665E File Offset: 0x0007485E
		public static void GetPresence([Out] PresenceUpdate[] updates)
		{
			BattleNet.s_impl.GetPresence(updates);
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x0007666B File Offset: 0x0007486B
		public static void SetPresenceBool(uint field, bool val)
		{
			BattleNet.s_impl.SetPresenceBool(field, val);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00076679 File Offset: 0x00074879
		public static void SetAccountLevelPresenceBool(uint field, bool val)
		{
			BattleNet.s_impl.SetAccountLevelPresenceBool(field, val);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x00076687 File Offset: 0x00074887
		public static void SetPresenceInt(uint field, long val)
		{
			BattleNet.s_impl.SetPresenceInt(field, val);
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x00076695 File Offset: 0x00074895
		public static void SetPresenceString(uint field, string val)
		{
			BattleNet.s_impl.SetPresenceString(field, val);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000766A3 File Offset: 0x000748A3
		public static void SetPresenceBlob(uint field, byte[] val)
		{
			BattleNet.s_impl.SetPresenceBlob(field, val);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000766B1 File Offset: 0x000748B1
		public static void SetPresenceEntityId(uint field, EntityId val)
		{
			BattleNet.s_impl.SetPresenceEntityId(field, val);
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000766BF File Offset: 0x000748BF
		public static void SetRichPresence([In] RichPresenceUpdate[] updates)
		{
			BattleNet.s_impl.SetRichPresence(updates);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000766CC File Offset: 0x000748CC
		public static void RequestPresenceFields(bool isGameAccountEntityId, [In] EntityId entityId, [In] PresenceFieldKey[] fieldList)
		{
			BattleNet.s_impl.RequestPresenceFields(isGameAccountEntityId, entityId, fieldList);
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000766DB File Offset: 0x000748DB
		public static void PresenceSubscribe(EntityId entityId)
		{
			BattleNet.s_impl.PresenceSubscribe(entityId);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000766E8 File Offset: 0x000748E8
		public static void PresenceUnsubscribe(EntityId entityId)
		{
			BattleNet.s_impl.PresenceUnsubscribe(entityId);
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x000766F5 File Offset: 0x000748F5
		public static bool IsSubscribedToEntity(EntityId entityId)
		{
			return BattleNet.s_impl.IsSubscribedToEntity(entityId);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x00076702 File Offset: 0x00074902
		public static void CreateParty(string partyType, int privacyLevel, params bnet.protocol.v2.Attribute[] partyAttributes)
		{
			BattleNet.s_impl.CreateParty(partyType, privacyLevel, partyAttributes);
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x00076711 File Offset: 0x00074911
		public static void JoinParty(EntityId partyId, string partyType)
		{
			BattleNet.s_impl.JoinParty(partyId, partyType);
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0007671F File Offset: 0x0007491F
		public static void LeaveParty(EntityId partyId)
		{
			BattleNet.s_impl.LeaveParty(partyId);
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0007672C File Offset: 0x0007492C
		public static void DissolveParty(EntityId partyId)
		{
			BattleNet.s_impl.DissolveParty(partyId);
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x00076739 File Offset: 0x00074939
		public static void SetPartyPrivacy(EntityId partyId, int privacyLevel)
		{
			BattleNet.s_impl.SetPartyPrivacy(partyId, privacyLevel);
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x00076747 File Offset: 0x00074947
		public static void AssignPartyRole(EntityId partyId, EntityId memberId, uint roleId)
		{
			BattleNet.s_impl.AssignPartyRole(partyId, memberId, roleId);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00076756 File Offset: 0x00074956
		public static void SendPartyInvite(EntityId partyId, EntityId inviteeId, bool isReservation)
		{
			BattleNet.s_impl.SendPartyInvite(partyId, inviteeId, isReservation);
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x00076765 File Offset: 0x00074965
		public static void AcceptPartyInvite(ulong invitationId)
		{
			BattleNet.s_impl.AcceptPartyInvite(invitationId);
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x00076772 File Offset: 0x00074972
		public static void DeclinePartyInvite(ulong invitationId)
		{
			BattleNet.s_impl.DeclinePartyInvite(invitationId);
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0007677F File Offset: 0x0007497F
		public static void RevokePartyInvite(EntityId partyId, ulong invitationId)
		{
			BattleNet.s_impl.RevokePartyInvite(partyId, invitationId);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0007678D File Offset: 0x0007498D
		public static void RequestPartyInvite(EntityId partyId, EntityId whomToAskForApproval, EntityId whomToInvite, string szPartyType)
		{
			BattleNet.s_impl.RequestPartyInvite(partyId, whomToAskForApproval, whomToInvite, szPartyType);
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0007679D File Offset: 0x0007499D
		public static void IgnoreInviteRequest(EntityId partyId, EntityId requestedTargetId)
		{
			BattleNet.s_impl.IgnoreInviteRequest(partyId, requestedTargetId);
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x000767AB File Offset: 0x000749AB
		public static void KickPartyMember(EntityId partyId, EntityId memberId)
		{
			BattleNet.s_impl.KickPartyMember(partyId, memberId);
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x000767B9 File Offset: 0x000749B9
		public static void SendPartyChatMessage(EntityId partyId, string message)
		{
			BattleNet.s_impl.SendPartyChatMessage(partyId, message);
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000767C7 File Offset: 0x000749C7
		public static void ClearPartyAttribute(EntityId partyId, string attributeKey)
		{
			BattleNet.s_impl.ClearPartyAttribute(partyId, attributeKey);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000767D5 File Offset: 0x000749D5
		public static void SetPartyAttributeLong(EntityId partyId, string attributeKey, [In] long value)
		{
			BattleNet.s_impl.SetPartyAttributeLong(partyId, attributeKey, value);
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x000767E4 File Offset: 0x000749E4
		public static void SetPartyAttributeString(EntityId partyId, string attributeKey, [In] string value)
		{
			BattleNet.s_impl.SetPartyAttributeString(partyId, attributeKey, value);
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000767F3 File Offset: 0x000749F3
		public static void SetPartyAttributeBlob(EntityId partyId, string attributeKey, [In] byte[] value)
		{
			BattleNet.s_impl.SetPartyAttributeBlob(partyId, attributeKey, value);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00076802 File Offset: 0x00074A02
		public static void SetPartyAttributes(EntityId partyId, params bnet.protocol.v2.Attribute[] attrs)
		{
			BattleNet.s_impl.SetPartyAttributes(partyId, attrs);
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00076810 File Offset: 0x00074A10
		public static void ClearMemberAttribute(EntityId partyId, GameAccountHandle partyMember, string attributeKey)
		{
			BattleNet.s_impl.ClearMemberAttribute(partyId, partyMember, attributeKey);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0007681F File Offset: 0x00074A1F
		public static void SetMemberAttributeLong(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] long value)
		{
			BattleNet.s_impl.SetMemberAttributeLong(partyId, partyMember, attributeKey, value);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0007682F File Offset: 0x00074A2F
		public static void SetMemberAttributeString(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] string value)
		{
			BattleNet.s_impl.SetMemberAttributeString(partyId, partyMember, attributeKey, value);
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0007683F File Offset: 0x00074A3F
		public static void SetMemberAttributeBlob(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] byte[] value)
		{
			BattleNet.s_impl.SetMemberAttributeBlob(partyId, partyMember, attributeKey, value);
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0007684F File Offset: 0x00074A4F
		public static void SetMemberAttributes(EntityId partyId, GameAccountHandle partyMember, params bnet.protocol.v2.Attribute[] attrs)
		{
			BattleNet.s_impl.SetMemberAttributes(partyId, partyMember, attrs);
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0007685E File Offset: 0x00074A5E
		public static int GetPartyPrivacy(EntityId partyId)
		{
			return BattleNet.s_impl.GetPartyPrivacy(partyId);
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x0007686B File Offset: 0x00074A6B
		public static int GetCountPartyMembers(EntityId partyId)
		{
			return BattleNet.s_impl.GetCountPartyMembers(partyId);
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00076878 File Offset: 0x00074A78
		public static int GetMaxPartyMembers(EntityId partyId)
		{
			return BattleNet.s_impl.GetMaxPartyMembers(partyId);
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x00076885 File Offset: 0x00074A85
		public static void GetPartyMembers(EntityId partyId, out PartyMember[] members)
		{
			BattleNet.s_impl.GetPartyMembers(partyId, out members);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00076893 File Offset: 0x00074A93
		public static void GetReceivedPartyInvites(out PartyInvite[] invites)
		{
			BattleNet.s_impl.GetReceivedPartyInvites(out invites);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000768A0 File Offset: 0x00074AA0
		public static void GetPartySentInvites(EntityId partyId, out PartyInvite[] invites)
		{
			BattleNet.s_impl.GetPartySentInvites(partyId, out invites);
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000768AE File Offset: 0x00074AAE
		public static void GetPartyInviteRequests(EntityId partyId, out InviteRequest[] requests)
		{
			BattleNet.s_impl.GetPartyInviteRequests(partyId, out requests);
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000768BC File Offset: 0x00074ABC
		public static void GetAllPartyAttributes(EntityId partyId, out string[] allKeys)
		{
			BattleNet.s_impl.GetAllPartyAttributes(partyId, out allKeys);
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000768CA File Offset: 0x00074ACA
		public static bool GetPartyAttributeLong(EntityId partyId, string attributeKey, out long value)
		{
			return BattleNet.s_impl.GetPartyAttributeLong(partyId, attributeKey, out value);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000768D9 File Offset: 0x00074AD9
		public static void GetPartyAttributeString(EntityId partyId, string attributeKey, out string value)
		{
			BattleNet.s_impl.GetPartyAttributeString(partyId, attributeKey, out value);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000768E8 File Offset: 0x00074AE8
		public static void GetPartyAttributeBlob(EntityId partyId, string attributeKey, out byte[] value)
		{
			BattleNet.s_impl.GetPartyAttributeBlob(partyId, attributeKey, out value);
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000768F7 File Offset: 0x00074AF7
		public static void GetMemberAttributeString(EntityId partyId, GameAccountHandle partyMember, string attributeKey, out string value)
		{
			BattleNet.s_impl.GetMemberAttributeString(partyId, partyMember, attributeKey, out value);
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x00076907 File Offset: 0x00074B07
		public static void GetMemberAttributeBlob(EntityId partyId, GameAccountHandle partyMember, string attributeKey, out byte[] value)
		{
			BattleNet.s_impl.GetMemberAttributeBlob(partyId, partyMember, attributeKey, out value);
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x00076917 File Offset: 0x00074B17
		public static void GetPartyListenerEvents(out PartyListenerEvent[] events)
		{
			BattleNet.s_impl.GetPartyListenerEvents(out events);
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x00076924 File Offset: 0x00074B24
		public static void ClearPartyListenerEvents()
		{
			BattleNet.s_impl.ClearPartyListenerEvents();
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x00076930 File Offset: 0x00074B30
		public static void GetFriendsInfo(ref FriendsInfo info)
		{
			BattleNet.s_impl.GetFriendsInfo(ref info);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0007693D File Offset: 0x00074B3D
		public static void ClearFriendsUpdates()
		{
			BattleNet.s_impl.ClearFriendsUpdates();
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x00076949 File Offset: 0x00074B49
		public static void GetFriendsUpdates([Out] FriendsUpdate[] updates)
		{
			BattleNet.s_impl.GetFriendsUpdates(updates);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x00076956 File Offset: 0x00074B56
		public static void SendFriendInvite(string inviter, string invitee, bool byEmail)
		{
			BattleNet.s_impl.SendFriendInvite(inviter, invitee, byEmail);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x00076965 File Offset: 0x00074B65
		public static void ManageFriendInvite(int action, ulong inviteId)
		{
			BattleNet.s_impl.ManageFriendInvite(action, inviteId);
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x00076973 File Offset: 0x00074B73
		public static void RemoveFriend(BnetAccountId account)
		{
			BattleNet.s_impl.RemoveFriend(account);
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x00076980 File Offset: 0x00074B80
		public static void SendWhisper(BnetGameAccountId gameAccount, string message)
		{
			BattleNet.s_impl.SendWhisper(gameAccount, message);
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0007698E File Offset: 0x00074B8E
		public static void GetWhisperInfo(ref WhisperInfo info)
		{
			BattleNet.s_impl.GetWhisperInfo(ref info);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0007699B File Offset: 0x00074B9B
		public static void GetWhispers([Out] BnetWhisper[] whispers)
		{
			BattleNet.s_impl.GetWhispers(whispers);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000769A8 File Offset: 0x00074BA8
		public static void ClearWhispers()
		{
			BattleNet.s_impl.ClearWhispers();
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000769B4 File Offset: 0x00074BB4
		public static int GetNotificationCount()
		{
			return BattleNet.s_impl.GetNotificationCount();
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000769C0 File Offset: 0x00074BC0
		public static void GetNotifications([Out] BnetNotification[] notifications)
		{
			BattleNet.s_impl.GetNotifications(notifications);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x000769CD File Offset: 0x00074BCD
		public static void ClearNotifications()
		{
			BattleNet.s_impl.ClearNotifications();
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000769D9 File Offset: 0x00074BD9
		public static bool CheckWebAuth(out string url)
		{
			return BattleNet.s_impl.CheckWebAuth(out url);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000769E6 File Offset: 0x00074BE6
		public static bool HasExternalChallenge()
		{
			return BattleNet.s_impl.HasExternalChallenge();
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000769F2 File Offset: 0x00074BF2
		public static void ProvideWebAuthToken(string token, RPCContextDelegate callback = null)
		{
			BattleNet.s_impl.ProvideWebAuthToken(token, callback);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00076A00 File Offset: 0x00074C00
		public static void GenerateSSOToken(Action<bool, string> callback)
		{
			BattleNet.s_impl.GenerateSSOToken(callback);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00076A0D File Offset: 0x00074C0D
		public static void GenerateAppWebCredentials(Action<bool, string> callback)
		{
			BattleNet.s_impl.GenerateAppWebCredentials(callback);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x00076A1A File Offset: 0x00074C1A
		public static void GenerateWtcgWebCredentials(Action<bool, string> callback)
		{
			BattleNet.s_impl.GenerateWtcgWebCredentials(callback);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x00076A27 File Offset: 0x00074C27
		public static string FilterProfanity(string unfiltered)
		{
			return BattleNet.s_impl.FilterProfanity(unfiltered);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00076A34 File Offset: 0x00074C34
		public static void ApplicationWasPaused()
		{
			if (BattleNet.s_impl != null)
			{
				BattleNet.s_impl.ApplicationWasPaused();
			}
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00076A47 File Offset: 0x00074C47
		public static void ApplicationWasUnpaused()
		{
			if (BattleNet.s_impl != null)
			{
				BattleNet.s_impl.ApplicationWasUnpaused();
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00076A5C File Offset: 0x00074C5C
		public static string GetEnumAsString<T>(T enumVal)
		{
			string text = enumVal.ToString();
			DescriptionAttribute[] array = (DescriptionAttribute[])enumVal.GetType().GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (array.Length != 0)
			{
				return array[0].Description;
			}
			return text;
		}

		// Token: 0x04000C08 RID: 3080
		private static IBattleNet s_impl;

		// Token: 0x04000C09 RID: 3081
		public const string COUNTRY_KOREA = "KOR";

		// Token: 0x04000C0A RID: 3082
		public const string COUNTRY_CHINA = "CHN";

		// Token: 0x020006B8 RID: 1720
		public enum BnetEvent
		{
			// Token: 0x04002206 RID: 8710
			Disconnected
		}

		// Token: 0x020006B9 RID: 1721
		public class GameServerInfo
		{
			// Token: 0x1700128B RID: 4747
			// (get) Token: 0x06006254 RID: 25172 RVA: 0x001287AC File Offset: 0x001269AC
			// (set) Token: 0x06006255 RID: 25173 RVA: 0x001287B4 File Offset: 0x001269B4
			public string Address { get; set; }

			// Token: 0x1700128C RID: 4748
			// (get) Token: 0x06006256 RID: 25174 RVA: 0x001287BD File Offset: 0x001269BD
			// (set) Token: 0x06006257 RID: 25175 RVA: 0x001287C5 File Offset: 0x001269C5
			public int Port { get; set; }

			// Token: 0x1700128D RID: 4749
			// (get) Token: 0x06006258 RID: 25176 RVA: 0x001287CE File Offset: 0x001269CE
			// (set) Token: 0x06006259 RID: 25177 RVA: 0x001287D6 File Offset: 0x001269D6
			public int GameHandle { get; set; }

			// Token: 0x1700128E RID: 4750
			// (get) Token: 0x0600625A RID: 25178 RVA: 0x001287DF File Offset: 0x001269DF
			// (set) Token: 0x0600625B RID: 25179 RVA: 0x001287E7 File Offset: 0x001269E7
			public long ClientHandle { get; set; }

			// Token: 0x1700128F RID: 4751
			// (get) Token: 0x0600625C RID: 25180 RVA: 0x001287F0 File Offset: 0x001269F0
			// (set) Token: 0x0600625D RID: 25181 RVA: 0x001287F8 File Offset: 0x001269F8
			public string AuroraPassword { get; set; }

			// Token: 0x17001290 RID: 4752
			// (get) Token: 0x0600625E RID: 25182 RVA: 0x00128801 File Offset: 0x00126A01
			// (set) Token: 0x0600625F RID: 25183 RVA: 0x00128809 File Offset: 0x00126A09
			public string Version { get; set; }

			// Token: 0x17001291 RID: 4753
			// (get) Token: 0x06006260 RID: 25184 RVA: 0x00128812 File Offset: 0x00126A12
			// (set) Token: 0x06006261 RID: 25185 RVA: 0x0012881A File Offset: 0x00126A1A
			public int Mission { get; set; }

			// Token: 0x17001292 RID: 4754
			// (get) Token: 0x06006262 RID: 25186 RVA: 0x00128823 File Offset: 0x00126A23
			// (set) Token: 0x06006263 RID: 25187 RVA: 0x0012882B File Offset: 0x00126A2B
			public bool Resumable { get; set; }

			// Token: 0x17001293 RID: 4755
			// (get) Token: 0x06006264 RID: 25188 RVA: 0x00128834 File Offset: 0x00126A34
			// (set) Token: 0x06006265 RID: 25189 RVA: 0x0012883C File Offset: 0x00126A3C
			public string SpectatorPassword { get; set; }

			// Token: 0x17001294 RID: 4756
			// (get) Token: 0x06006266 RID: 25190 RVA: 0x00128845 File Offset: 0x00126A45
			// (set) Token: 0x06006267 RID: 25191 RVA: 0x0012884D File Offset: 0x00126A4D
			public bool SpectatorMode { get; set; }
		}
	}
}
