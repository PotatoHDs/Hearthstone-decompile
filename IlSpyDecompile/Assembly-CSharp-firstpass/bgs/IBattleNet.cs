using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using bgs.types;
using bnet.protocol.account.v1;
using bnet.protocol.matchmaking.v1;
using bnet.protocol.v2;

namespace bgs
{
	public interface IBattleNet
	{
		bool IsInitialized();

		bool Init(bool fromEditor, string username, string targetServer, uint port, SslParameters sslParams);

		ClientInterface Client();

		void AppQuit();

		bool IsConnected();

		void ProcessAurora();

		void QueryAurora();

		void CloseAurora();

		void RequestCloseAurora();

		int BattleNetStatus();

		void SendUtilPacket(int packetId, UtilSystemId systemId, byte[] bytes, int size, int subID, int context, ulong route);

		GamesAPI.UtilResponse NextUtilPacket();

		int GetErrorsCount();

		void GetErrors([Out] BnetErrorInfo[] errors);

		void ClearErrors();

		EntityId GetMyGameAccountId();

		EntityId GetMyAccountId();

		void GetQueueInfo(ref QueueInfo queueInfo);

		string GetVersion();

		int GetDataVersion();

		string GetEnvironment();

		uint GetPort();

		string GetUserEmailAddress();

		BattleNetLogSource GetLogSource();

		double GetRealTimeSinceStartup();

		AuthenticationAPI.MemModuleLoadRequest NextMemModuleRequest();

		void SendMemModuleResponse(AuthenticationAPI.MemModuleLoadRequest request, byte[] memModuleResponseBytes);

		string GetLaunchOption(string key, bool encrypted);

		string GetConnectionString();

		constants.BnetRegion GetAccountRegion();

		string GetAccountCountry();

		bool IsHeadlessAccount();

		constants.BnetRegion GetCurrentRegion();

		void GetPlayRestrictions(ref Lockouts restrictions, bool reload);

		void GetAccountState(BnetAccountId bnetAccount);

		int GetShutdownMinutes();

		int GetBnetEventsSize();

		void ClearBnetEvents();

		void GetBnetEvents([Out] BattleNet.BnetEvent[] events);

		void QueueMatchmaking(List<bnet.protocol.v2.Attribute> matchmakerAttributesFilter, List<bnet.protocol.v2.Attribute> gameAttributes, params Player[] players);

		void CancelMatchmaking();

		QueueEvent GetQueueEvent();

		int PresenceSize();

		void ClearPresence();

		void GetPresence([Out] PresenceUpdate[] updates);

		void SetPresenceBool(uint field, bool val);

		void SetAccountLevelPresenceBool(uint field, bool val);

		void SetPresenceInt(uint field, long val);

		void SetPresenceString(uint field, string val);

		void SetPresenceBlob(uint field, byte[] val);

		void SetPresenceEntityId(uint field, EntityId val);

		void SetRichPresence([In] RichPresenceUpdate[] updates);

		void RequestPresenceFields(bool isGameAccountEntityId, [In] EntityId entityId, [In] PresenceFieldKey[] fieldList);

		void PresenceSubscribe(EntityId entityId);

		void PresenceUnsubscribe(EntityId entityId);

		bool IsSubscribedToEntity(EntityId entityId);

		void ChannelMembershipSubscribe();

		void CreateParty(string partyType, int privacyLevel, params bnet.protocol.v2.Attribute[] partyAttributes);

		void JoinParty(EntityId partyId, string partyType);

		void LeaveParty(EntityId partyId);

		void DissolveParty(EntityId partyId);

		void SetPartyPrivacy(EntityId partyId, int privacyLevel);

		void AssignPartyRole(EntityId partyId, EntityId memberId, uint roleId);

		void SendPartyInvite(EntityId partyId, EntityId inviteeId, bool isReservation);

		void AcceptPartyInvite(ulong invitationId);

		void DeclinePartyInvite(ulong invitationId);

		void RevokePartyInvite(EntityId partyId, ulong invitationId);

		void RequestPartyInvite(EntityId partyId, EntityId whomToAskForApproval, EntityId whomToInvite, string szPartyType);

		void IgnoreInviteRequest(EntityId partyId, EntityId requestedTargetId);

		void KickPartyMember(EntityId partyId, EntityId memberId);

		void SendPartyChatMessage(EntityId partyId, string message);

		void ClearPartyAttribute(EntityId partyId, string attributeKey);

		void SetPartyAttributeLong(EntityId partyId, string attributeKey, [In] long value);

		void SetPartyAttributeString(EntityId partyId, string attributeKey, [In] string value);

		void SetPartyAttributeBlob(EntityId partyId, string attributeKey, [In] byte[] value);

		void SetPartyAttributes(EntityId partyId, params bnet.protocol.v2.Attribute[] attrs);

		void ClearMemberAttribute(EntityId partyId, GameAccountHandle partyMember, string attributeKey);

		void SetMemberAttributeLong(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] long value);

		void SetMemberAttributeString(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] string value);

		void SetMemberAttributeBlob(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] byte[] value);

		void SetMemberAttributes(EntityId partyId, GameAccountHandle partyMember, params bnet.protocol.v2.Attribute[] attrs);

		int GetPartyPrivacy(EntityId partyId);

		int GetCountPartyMembers(EntityId partyId);

		int GetMaxPartyMembers(EntityId partyId);

		void GetPartyMembers(EntityId partyId, out bgs.types.PartyMember[] members);

		void GetReceivedPartyInvites(out PartyInvite[] invites);

		void GetPartySentInvites(EntityId partyId, out PartyInvite[] invites);

		void GetPartyInviteRequests(EntityId partyId, out InviteRequest[] requests);

		void GetAllPartyAttributes(EntityId partyId, out string[] allKeys);

		bool GetPartyAttributeLong(EntityId partyId, string attributeKey, out long value);

		void GetPartyAttributeString(EntityId partyId, string attributeKey, out string value);

		void GetPartyAttributeBlob(EntityId partyId, string attributeKey, out byte[] value);

		void GetMemberAttributeString(EntityId partyId, GameAccountHandle partyMember, string attributeKey, out string value);

		void GetMemberAttributeBlob(EntityId partyId, GameAccountHandle partyMember, string attributeKey, out byte[] value);

		void GetPartyListenerEvents(out PartyListenerEvent[] events);

		void ClearPartyListenerEvents();

		void GetFriendsInfo(ref FriendsInfo info);

		void ClearFriendsUpdates();

		void GetFriendsUpdates([Out] FriendsUpdate[] updates);

		void SendFriendInvite(string inviter, string invitee, bool byEmail);

		void ManageFriendInvite(int action, ulong inviteId);

		void RemoveFriend(BnetAccountId account);

		void SendWhisper(BnetGameAccountId gameAccount, string message);

		void GetWhisperInfo(ref WhisperInfo info);

		void GetWhispers([Out] BnetWhisper[] whispers);

		void ClearWhispers();

		int GetNotificationCount();

		void GetNotifications([Out] BnetNotification[] notifications);

		void ClearNotifications();

		void ApplicationWasPaused();

		void ApplicationWasUnpaused();

		bool CheckWebAuth(out string url);

		bool HasExternalChallenge();

		void ProvideWebAuthToken(string token, RPCContextDelegate callback);

		void GenerateSSOToken(Action<bool, string> callback);

		void GenerateAppWebCredentials(Action<bool, string> callback);

		void GenerateWtcgWebCredentials(Action<bool, string> callback);

		string FilterProfanity(string unfiltered);
	}
}
