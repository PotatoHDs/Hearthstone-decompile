using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using bgs.types;
using bnet.protocol.account.v1;
using bnet.protocol.matchmaking.v1;
using bnet.protocol.v2;

namespace bgs
{
	// Token: 0x02000213 RID: 531
	public interface IBattleNet
	{
		// Token: 0x0600212E RID: 8494
		bool IsInitialized();

		// Token: 0x0600212F RID: 8495
		bool Init(bool fromEditor, string username, string targetServer, uint port, SslParameters sslParams);

		// Token: 0x06002130 RID: 8496
		ClientInterface Client();

		// Token: 0x06002131 RID: 8497
		void AppQuit();

		// Token: 0x06002132 RID: 8498
		bool IsConnected();

		// Token: 0x06002133 RID: 8499
		void ProcessAurora();

		// Token: 0x06002134 RID: 8500
		void QueryAurora();

		// Token: 0x06002135 RID: 8501
		void CloseAurora();

		// Token: 0x06002136 RID: 8502
		void RequestCloseAurora();

		// Token: 0x06002137 RID: 8503
		int BattleNetStatus();

		// Token: 0x06002138 RID: 8504
		void SendUtilPacket(int packetId, UtilSystemId systemId, byte[] bytes, int size, int subID, int context, ulong route);

		// Token: 0x06002139 RID: 8505
		GamesAPI.UtilResponse NextUtilPacket();

		// Token: 0x0600213A RID: 8506
		int GetErrorsCount();

		// Token: 0x0600213B RID: 8507
		void GetErrors([Out] BnetErrorInfo[] errors);

		// Token: 0x0600213C RID: 8508
		void ClearErrors();

		// Token: 0x0600213D RID: 8509
		EntityId GetMyGameAccountId();

		// Token: 0x0600213E RID: 8510
		EntityId GetMyAccountId();

		// Token: 0x0600213F RID: 8511
		void GetQueueInfo(ref QueueInfo queueInfo);

		// Token: 0x06002140 RID: 8512
		string GetVersion();

		// Token: 0x06002141 RID: 8513
		int GetDataVersion();

		// Token: 0x06002142 RID: 8514
		string GetEnvironment();

		// Token: 0x06002143 RID: 8515
		uint GetPort();

		// Token: 0x06002144 RID: 8516
		string GetUserEmailAddress();

		// Token: 0x06002145 RID: 8517
		BattleNetLogSource GetLogSource();

		// Token: 0x06002146 RID: 8518
		double GetRealTimeSinceStartup();

		// Token: 0x06002147 RID: 8519
		AuthenticationAPI.MemModuleLoadRequest NextMemModuleRequest();

		// Token: 0x06002148 RID: 8520
		void SendMemModuleResponse(AuthenticationAPI.MemModuleLoadRequest request, byte[] memModuleResponseBytes);

		// Token: 0x06002149 RID: 8521
		string GetLaunchOption(string key, bool encrypted);

		// Token: 0x0600214A RID: 8522
		string GetConnectionString();

		// Token: 0x0600214B RID: 8523
		constants.BnetRegion GetAccountRegion();

		// Token: 0x0600214C RID: 8524
		string GetAccountCountry();

		// Token: 0x0600214D RID: 8525
		bool IsHeadlessAccount();

		// Token: 0x0600214E RID: 8526
		constants.BnetRegion GetCurrentRegion();

		// Token: 0x0600214F RID: 8527
		void GetPlayRestrictions(ref Lockouts restrictions, bool reload);

		// Token: 0x06002150 RID: 8528
		void GetAccountState(BnetAccountId bnetAccount);

		// Token: 0x06002151 RID: 8529
		int GetShutdownMinutes();

		// Token: 0x06002152 RID: 8530
		int GetBnetEventsSize();

		// Token: 0x06002153 RID: 8531
		void ClearBnetEvents();

		// Token: 0x06002154 RID: 8532
		void GetBnetEvents([Out] BattleNet.BnetEvent[] events);

		// Token: 0x06002155 RID: 8533
		void QueueMatchmaking(List<bnet.protocol.v2.Attribute> matchmakerAttributesFilter, List<bnet.protocol.v2.Attribute> gameAttributes, params Player[] players);

		// Token: 0x06002156 RID: 8534
		void CancelMatchmaking();

		// Token: 0x06002157 RID: 8535
		QueueEvent GetQueueEvent();

		// Token: 0x06002158 RID: 8536
		int PresenceSize();

		// Token: 0x06002159 RID: 8537
		void ClearPresence();

		// Token: 0x0600215A RID: 8538
		void GetPresence([Out] PresenceUpdate[] updates);

		// Token: 0x0600215B RID: 8539
		void SetPresenceBool(uint field, bool val);

		// Token: 0x0600215C RID: 8540
		void SetAccountLevelPresenceBool(uint field, bool val);

		// Token: 0x0600215D RID: 8541
		void SetPresenceInt(uint field, long val);

		// Token: 0x0600215E RID: 8542
		void SetPresenceString(uint field, string val);

		// Token: 0x0600215F RID: 8543
		void SetPresenceBlob(uint field, byte[] val);

		// Token: 0x06002160 RID: 8544
		void SetPresenceEntityId(uint field, EntityId val);

		// Token: 0x06002161 RID: 8545
		void SetRichPresence([In] RichPresenceUpdate[] updates);

		// Token: 0x06002162 RID: 8546
		void RequestPresenceFields(bool isGameAccountEntityId, [In] EntityId entityId, [In] PresenceFieldKey[] fieldList);

		// Token: 0x06002163 RID: 8547
		void PresenceSubscribe(EntityId entityId);

		// Token: 0x06002164 RID: 8548
		void PresenceUnsubscribe(EntityId entityId);

		// Token: 0x06002165 RID: 8549
		bool IsSubscribedToEntity(EntityId entityId);

		// Token: 0x06002166 RID: 8550
		void ChannelMembershipSubscribe();

		// Token: 0x06002167 RID: 8551
		void CreateParty(string partyType, int privacyLevel, params bnet.protocol.v2.Attribute[] partyAttributes);

		// Token: 0x06002168 RID: 8552
		void JoinParty(EntityId partyId, string partyType);

		// Token: 0x06002169 RID: 8553
		void LeaveParty(EntityId partyId);

		// Token: 0x0600216A RID: 8554
		void DissolveParty(EntityId partyId);

		// Token: 0x0600216B RID: 8555
		void SetPartyPrivacy(EntityId partyId, int privacyLevel);

		// Token: 0x0600216C RID: 8556
		void AssignPartyRole(EntityId partyId, EntityId memberId, uint roleId);

		// Token: 0x0600216D RID: 8557
		void SendPartyInvite(EntityId partyId, EntityId inviteeId, bool isReservation);

		// Token: 0x0600216E RID: 8558
		void AcceptPartyInvite(ulong invitationId);

		// Token: 0x0600216F RID: 8559
		void DeclinePartyInvite(ulong invitationId);

		// Token: 0x06002170 RID: 8560
		void RevokePartyInvite(EntityId partyId, ulong invitationId);

		// Token: 0x06002171 RID: 8561
		void RequestPartyInvite(EntityId partyId, EntityId whomToAskForApproval, EntityId whomToInvite, string szPartyType);

		// Token: 0x06002172 RID: 8562
		void IgnoreInviteRequest(EntityId partyId, EntityId requestedTargetId);

		// Token: 0x06002173 RID: 8563
		void KickPartyMember(EntityId partyId, EntityId memberId);

		// Token: 0x06002174 RID: 8564
		void SendPartyChatMessage(EntityId partyId, string message);

		// Token: 0x06002175 RID: 8565
		void ClearPartyAttribute(EntityId partyId, string attributeKey);

		// Token: 0x06002176 RID: 8566
		void SetPartyAttributeLong(EntityId partyId, string attributeKey, [In] long value);

		// Token: 0x06002177 RID: 8567
		void SetPartyAttributeString(EntityId partyId, string attributeKey, [In] string value);

		// Token: 0x06002178 RID: 8568
		void SetPartyAttributeBlob(EntityId partyId, string attributeKey, [In] byte[] value);

		// Token: 0x06002179 RID: 8569
		void SetPartyAttributes(EntityId partyId, params bnet.protocol.v2.Attribute[] attrs);

		// Token: 0x0600217A RID: 8570
		void ClearMemberAttribute(EntityId partyId, GameAccountHandle partyMember, string attributeKey);

		// Token: 0x0600217B RID: 8571
		void SetMemberAttributeLong(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] long value);

		// Token: 0x0600217C RID: 8572
		void SetMemberAttributeString(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] string value);

		// Token: 0x0600217D RID: 8573
		void SetMemberAttributeBlob(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] byte[] value);

		// Token: 0x0600217E RID: 8574
		void SetMemberAttributes(EntityId partyId, GameAccountHandle partyMember, params bnet.protocol.v2.Attribute[] attrs);

		// Token: 0x0600217F RID: 8575
		int GetPartyPrivacy(EntityId partyId);

		// Token: 0x06002180 RID: 8576
		int GetCountPartyMembers(EntityId partyId);

		// Token: 0x06002181 RID: 8577
		int GetMaxPartyMembers(EntityId partyId);

		// Token: 0x06002182 RID: 8578
		void GetPartyMembers(EntityId partyId, out PartyMember[] members);

		// Token: 0x06002183 RID: 8579
		void GetReceivedPartyInvites(out PartyInvite[] invites);

		// Token: 0x06002184 RID: 8580
		void GetPartySentInvites(EntityId partyId, out PartyInvite[] invites);

		// Token: 0x06002185 RID: 8581
		void GetPartyInviteRequests(EntityId partyId, out InviteRequest[] requests);

		// Token: 0x06002186 RID: 8582
		void GetAllPartyAttributes(EntityId partyId, out string[] allKeys);

		// Token: 0x06002187 RID: 8583
		bool GetPartyAttributeLong(EntityId partyId, string attributeKey, out long value);

		// Token: 0x06002188 RID: 8584
		void GetPartyAttributeString(EntityId partyId, string attributeKey, out string value);

		// Token: 0x06002189 RID: 8585
		void GetPartyAttributeBlob(EntityId partyId, string attributeKey, out byte[] value);

		// Token: 0x0600218A RID: 8586
		void GetMemberAttributeString(EntityId partyId, GameAccountHandle partyMember, string attributeKey, out string value);

		// Token: 0x0600218B RID: 8587
		void GetMemberAttributeBlob(EntityId partyId, GameAccountHandle partyMember, string attributeKey, out byte[] value);

		// Token: 0x0600218C RID: 8588
		void GetPartyListenerEvents(out PartyListenerEvent[] events);

		// Token: 0x0600218D RID: 8589
		void ClearPartyListenerEvents();

		// Token: 0x0600218E RID: 8590
		void GetFriendsInfo(ref FriendsInfo info);

		// Token: 0x0600218F RID: 8591
		void ClearFriendsUpdates();

		// Token: 0x06002190 RID: 8592
		void GetFriendsUpdates([Out] FriendsUpdate[] updates);

		// Token: 0x06002191 RID: 8593
		void SendFriendInvite(string inviter, string invitee, bool byEmail);

		// Token: 0x06002192 RID: 8594
		void ManageFriendInvite(int action, ulong inviteId);

		// Token: 0x06002193 RID: 8595
		void RemoveFriend(BnetAccountId account);

		// Token: 0x06002194 RID: 8596
		void SendWhisper(BnetGameAccountId gameAccount, string message);

		// Token: 0x06002195 RID: 8597
		void GetWhisperInfo(ref WhisperInfo info);

		// Token: 0x06002196 RID: 8598
		void GetWhispers([Out] BnetWhisper[] whispers);

		// Token: 0x06002197 RID: 8599
		void ClearWhispers();

		// Token: 0x06002198 RID: 8600
		int GetNotificationCount();

		// Token: 0x06002199 RID: 8601
		void GetNotifications([Out] BnetNotification[] notifications);

		// Token: 0x0600219A RID: 8602
		void ClearNotifications();

		// Token: 0x0600219B RID: 8603
		void ApplicationWasPaused();

		// Token: 0x0600219C RID: 8604
		void ApplicationWasUnpaused();

		// Token: 0x0600219D RID: 8605
		bool CheckWebAuth(out string url);

		// Token: 0x0600219E RID: 8606
		bool HasExternalChallenge();

		// Token: 0x0600219F RID: 8607
		void ProvideWebAuthToken(string token, RPCContextDelegate callback);

		// Token: 0x060021A0 RID: 8608
		void GenerateSSOToken(Action<bool, string> callback);

		// Token: 0x060021A1 RID: 8609
		void GenerateAppWebCredentials(Action<bool, string> callback);

		// Token: 0x060021A2 RID: 8610
		void GenerateWtcgWebCredentials(Action<bool, string> callback);

		// Token: 0x060021A3 RID: 8611
		string FilterProfanity(string unfiltered);
	}
}
