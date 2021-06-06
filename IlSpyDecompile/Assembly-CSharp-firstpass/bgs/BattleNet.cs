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
	public class BattleNet
	{
		public enum BnetEvent
		{
			Disconnected
		}

		public class GameServerInfo
		{
			public string Address { get; set; }

			public int Port { get; set; }

			public int GameHandle { get; set; }

			public long ClientHandle { get; set; }

			public string AuroraPassword { get; set; }

			public string Version { get; set; }

			public int Mission { get; set; }

			public bool Resumable { get; set; }

			public string SpectatorPassword { get; set; }

			public bool SpectatorMode { get; set; }
		}

		private static IBattleNet s_impl;

		public const string COUNTRY_KOREA = "KOR";

		public const string COUNTRY_CHINA = "CHN";

		public static BattleNetLogSource Log => s_impl.GetLogSource();

		public static IBattleNet Get()
		{
			return s_impl;
		}

		public static ClientInterface Client()
		{
			return s_impl.Client();
		}

		public static bool IsInitialized()
		{
			if (s_impl != null)
			{
				return s_impl.IsInitialized();
			}
			return false;
		}

		public static void SetImpl(IBattleNet battleNet)
		{
			s_impl = battleNet;
		}

		public static bool Init(IBattleNet battleNet, bool internalMode, string userEmailAddress, string targetServer, uint port, SslParameters sslParams)
		{
			if (s_impl == null)
			{
				s_impl = battleNet;
			}
			return s_impl.Init(internalMode, userEmailAddress, targetServer, port, sslParams);
		}

		public static bool Reset(IBattleNet battleNet, bool internalMode, string userEmailAddress, string targetServer, uint port, SslParameters sslParams)
		{
			RequestCloseAurora();
			s_impl = null;
			return Init(battleNet, internalMode, userEmailAddress, targetServer, port, sslParams);
		}

		public static void AppQuit()
		{
			s_impl.AppQuit();
		}

		public static bool IsConnected()
		{
			if (s_impl != null)
			{
				return s_impl.IsConnected();
			}
			return false;
		}

		public static void ProcessAurora()
		{
			s_impl.ProcessAurora();
		}

		public static void QueryAurora()
		{
			s_impl.QueryAurora();
		}

		public static void CloseAurora()
		{
			s_impl.CloseAurora();
		}

		public static void RequestCloseAurora()
		{
			s_impl.RequestCloseAurora();
		}

		public static int BattleNetStatus()
		{
			return s_impl.BattleNetStatus();
		}

		public static void SendUtilPacket(int packetId, UtilSystemId systemId, byte[] bytes, int size, int subID, int context, ulong route)
		{
			s_impl.SendUtilPacket(packetId, systemId, bytes, size, subID, context, route);
		}

		public static GamesAPI.UtilResponse NextUtilPacket()
		{
			return s_impl.NextUtilPacket();
		}

		public static int GetErrorsCount()
		{
			return s_impl.GetErrorsCount();
		}

		public static void GetErrors([Out] BnetErrorInfo[] errors)
		{
			s_impl.GetErrors(errors);
		}

		public static void ClearErrors()
		{
			s_impl.ClearErrors();
		}

		public static EntityId GetMyGameAccountId()
		{
			return s_impl.GetMyGameAccountId();
		}

		public static EntityId GetMyAccoundId()
		{
			return s_impl.GetMyAccountId();
		}

		public static void GetQueueInfo(ref QueueInfo queueInfo)
		{
			s_impl.GetQueueInfo(ref queueInfo);
		}

		public static string GetVersion()
		{
			return s_impl.GetVersion();
		}

		public static int GetDataVersion()
		{
			return s_impl.GetDataVersion();
		}

		public static string GetEnvironment()
		{
			return s_impl.GetEnvironment();
		}

		public static uint GetPort()
		{
			return s_impl.GetPort();
		}

		public static string GetUserEmailAddress()
		{
			return s_impl.GetUserEmailAddress();
		}

		public static double GetRealTimeSinceStartup()
		{
			return s_impl.GetRealTimeSinceStartup();
		}

		public static AuthenticationAPI.MemModuleLoadRequest NextMemModuleRequest()
		{
			return s_impl.NextMemModuleRequest();
		}

		public static void SendMemModuleResponse(AuthenticationAPI.MemModuleLoadRequest request, byte[] memModuleResponseBytes)
		{
			s_impl.SendMemModuleResponse(request, memModuleResponseBytes);
		}

		public static string GetLaunchOption(string key, bool encrypted)
		{
			return LaunchOptionHelper.GetLaunchOption(key, encrypted);
		}

		public static string GetConnectionString()
		{
			return s_impl.GetConnectionString();
		}

		public static constants.BnetRegion GetAccountRegion()
		{
			return s_impl.GetAccountRegion();
		}

		public static string GetAccountCountry()
		{
			return s_impl.GetAccountCountry();
		}

		public static bool IsHeadlessAccount()
		{
			return s_impl.IsHeadlessAccount();
		}

		public static constants.BnetRegion GetCurrentRegion()
		{
			if (s_impl != null)
			{
				return s_impl.GetCurrentRegion();
			}
			return constants.BnetRegion.REGION_UNINITIALIZED;
		}

		public static void GetPlayRestrictions(ref Lockouts restrictions, bool reload)
		{
			s_impl.GetPlayRestrictions(ref restrictions, reload);
		}

		public static void GetAccountState(BnetAccountId bnetAccount)
		{
			s_impl.GetAccountState(bnetAccount);
		}

		public static int GetShutdownMinutes()
		{
			return s_impl.GetShutdownMinutes();
		}

		public static int GetBnetEventsSize()
		{
			return s_impl.GetBnetEventsSize();
		}

		public static void ClearBnetEvents()
		{
			s_impl.ClearBnetEvents();
		}

		public static void GetBnetEvents([Out] BnetEvent[] events)
		{
			s_impl.GetBnetEvents(events);
		}

		public static void QueueMatchmaking(List<bnet.protocol.v2.Attribute> matchmakerAttributesFilter, List<bnet.protocol.v2.Attribute> gameAttributes, params Player[] players)
		{
			s_impl.QueueMatchmaking(matchmakerAttributesFilter, gameAttributes, players);
		}

		public static void CancelMatchmaking()
		{
			s_impl.CancelMatchmaking();
		}

		public static QueueEvent GetQueueEvent()
		{
			return s_impl.GetQueueEvent();
		}

		public static int PresenceSize()
		{
			return s_impl.PresenceSize();
		}

		public static void ClearPresence()
		{
			s_impl.ClearPresence();
		}

		public static void GetPresence([Out] PresenceUpdate[] updates)
		{
			s_impl.GetPresence(updates);
		}

		public static void SetPresenceBool(uint field, bool val)
		{
			s_impl.SetPresenceBool(field, val);
		}

		public static void SetAccountLevelPresenceBool(uint field, bool val)
		{
			s_impl.SetAccountLevelPresenceBool(field, val);
		}

		public static void SetPresenceInt(uint field, long val)
		{
			s_impl.SetPresenceInt(field, val);
		}

		public static void SetPresenceString(uint field, string val)
		{
			s_impl.SetPresenceString(field, val);
		}

		public static void SetPresenceBlob(uint field, byte[] val)
		{
			s_impl.SetPresenceBlob(field, val);
		}

		public static void SetPresenceEntityId(uint field, EntityId val)
		{
			s_impl.SetPresenceEntityId(field, val);
		}

		public static void SetRichPresence([In] RichPresenceUpdate[] updates)
		{
			s_impl.SetRichPresence(updates);
		}

		public static void RequestPresenceFields(bool isGameAccountEntityId, [In] EntityId entityId, [In] PresenceFieldKey[] fieldList)
		{
			s_impl.RequestPresenceFields(isGameAccountEntityId, entityId, fieldList);
		}

		public static void PresenceSubscribe(EntityId entityId)
		{
			s_impl.PresenceSubscribe(entityId);
		}

		public static void PresenceUnsubscribe(EntityId entityId)
		{
			s_impl.PresenceUnsubscribe(entityId);
		}

		public static bool IsSubscribedToEntity(EntityId entityId)
		{
			return s_impl.IsSubscribedToEntity(entityId);
		}

		public static void CreateParty(string partyType, int privacyLevel, params bnet.protocol.v2.Attribute[] partyAttributes)
		{
			s_impl.CreateParty(partyType, privacyLevel, partyAttributes);
		}

		public static void JoinParty(EntityId partyId, string partyType)
		{
			s_impl.JoinParty(partyId, partyType);
		}

		public static void LeaveParty(EntityId partyId)
		{
			s_impl.LeaveParty(partyId);
		}

		public static void DissolveParty(EntityId partyId)
		{
			s_impl.DissolveParty(partyId);
		}

		public static void SetPartyPrivacy(EntityId partyId, int privacyLevel)
		{
			s_impl.SetPartyPrivacy(partyId, privacyLevel);
		}

		public static void AssignPartyRole(EntityId partyId, EntityId memberId, uint roleId)
		{
			s_impl.AssignPartyRole(partyId, memberId, roleId);
		}

		public static void SendPartyInvite(EntityId partyId, EntityId inviteeId, bool isReservation)
		{
			s_impl.SendPartyInvite(partyId, inviteeId, isReservation);
		}

		public static void AcceptPartyInvite(ulong invitationId)
		{
			s_impl.AcceptPartyInvite(invitationId);
		}

		public static void DeclinePartyInvite(ulong invitationId)
		{
			s_impl.DeclinePartyInvite(invitationId);
		}

		public static void RevokePartyInvite(EntityId partyId, ulong invitationId)
		{
			s_impl.RevokePartyInvite(partyId, invitationId);
		}

		public static void RequestPartyInvite(EntityId partyId, EntityId whomToAskForApproval, EntityId whomToInvite, string szPartyType)
		{
			s_impl.RequestPartyInvite(partyId, whomToAskForApproval, whomToInvite, szPartyType);
		}

		public static void IgnoreInviteRequest(EntityId partyId, EntityId requestedTargetId)
		{
			s_impl.IgnoreInviteRequest(partyId, requestedTargetId);
		}

		public static void KickPartyMember(EntityId partyId, EntityId memberId)
		{
			s_impl.KickPartyMember(partyId, memberId);
		}

		public static void SendPartyChatMessage(EntityId partyId, string message)
		{
			s_impl.SendPartyChatMessage(partyId, message);
		}

		public static void ClearPartyAttribute(EntityId partyId, string attributeKey)
		{
			s_impl.ClearPartyAttribute(partyId, attributeKey);
		}

		public static void SetPartyAttributeLong(EntityId partyId, string attributeKey, [In] long value)
		{
			s_impl.SetPartyAttributeLong(partyId, attributeKey, value);
		}

		public static void SetPartyAttributeString(EntityId partyId, string attributeKey, [In] string value)
		{
			s_impl.SetPartyAttributeString(partyId, attributeKey, value);
		}

		public static void SetPartyAttributeBlob(EntityId partyId, string attributeKey, [In] byte[] value)
		{
			s_impl.SetPartyAttributeBlob(partyId, attributeKey, value);
		}

		public static void SetPartyAttributes(EntityId partyId, params bnet.protocol.v2.Attribute[] attrs)
		{
			s_impl.SetPartyAttributes(partyId, attrs);
		}

		public static void ClearMemberAttribute(EntityId partyId, GameAccountHandle partyMember, string attributeKey)
		{
			s_impl.ClearMemberAttribute(partyId, partyMember, attributeKey);
		}

		public static void SetMemberAttributeLong(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] long value)
		{
			s_impl.SetMemberAttributeLong(partyId, partyMember, attributeKey, value);
		}

		public static void SetMemberAttributeString(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] string value)
		{
			s_impl.SetMemberAttributeString(partyId, partyMember, attributeKey, value);
		}

		public static void SetMemberAttributeBlob(EntityId partyId, GameAccountHandle partyMember, string attributeKey, [In] byte[] value)
		{
			s_impl.SetMemberAttributeBlob(partyId, partyMember, attributeKey, value);
		}

		public static void SetMemberAttributes(EntityId partyId, GameAccountHandle partyMember, params bnet.protocol.v2.Attribute[] attrs)
		{
			s_impl.SetMemberAttributes(partyId, partyMember, attrs);
		}

		public static int GetPartyPrivacy(EntityId partyId)
		{
			return s_impl.GetPartyPrivacy(partyId);
		}

		public static int GetCountPartyMembers(EntityId partyId)
		{
			return s_impl.GetCountPartyMembers(partyId);
		}

		public static int GetMaxPartyMembers(EntityId partyId)
		{
			return s_impl.GetMaxPartyMembers(partyId);
		}

		public static void GetPartyMembers(EntityId partyId, out bgs.types.PartyMember[] members)
		{
			s_impl.GetPartyMembers(partyId, out members);
		}

		public static void GetReceivedPartyInvites(out PartyInvite[] invites)
		{
			s_impl.GetReceivedPartyInvites(out invites);
		}

		public static void GetPartySentInvites(EntityId partyId, out PartyInvite[] invites)
		{
			s_impl.GetPartySentInvites(partyId, out invites);
		}

		public static void GetPartyInviteRequests(EntityId partyId, out InviteRequest[] requests)
		{
			s_impl.GetPartyInviteRequests(partyId, out requests);
		}

		public static void GetAllPartyAttributes(EntityId partyId, out string[] allKeys)
		{
			s_impl.GetAllPartyAttributes(partyId, out allKeys);
		}

		public static bool GetPartyAttributeLong(EntityId partyId, string attributeKey, out long value)
		{
			return s_impl.GetPartyAttributeLong(partyId, attributeKey, out value);
		}

		public static void GetPartyAttributeString(EntityId partyId, string attributeKey, out string value)
		{
			s_impl.GetPartyAttributeString(partyId, attributeKey, out value);
		}

		public static void GetPartyAttributeBlob(EntityId partyId, string attributeKey, out byte[] value)
		{
			s_impl.GetPartyAttributeBlob(partyId, attributeKey, out value);
		}

		public static void GetMemberAttributeString(EntityId partyId, GameAccountHandle partyMember, string attributeKey, out string value)
		{
			s_impl.GetMemberAttributeString(partyId, partyMember, attributeKey, out value);
		}

		public static void GetMemberAttributeBlob(EntityId partyId, GameAccountHandle partyMember, string attributeKey, out byte[] value)
		{
			s_impl.GetMemberAttributeBlob(partyId, partyMember, attributeKey, out value);
		}

		public static void GetPartyListenerEvents(out PartyListenerEvent[] events)
		{
			s_impl.GetPartyListenerEvents(out events);
		}

		public static void ClearPartyListenerEvents()
		{
			s_impl.ClearPartyListenerEvents();
		}

		public static void GetFriendsInfo(ref FriendsInfo info)
		{
			s_impl.GetFriendsInfo(ref info);
		}

		public static void ClearFriendsUpdates()
		{
			s_impl.ClearFriendsUpdates();
		}

		public static void GetFriendsUpdates([Out] FriendsUpdate[] updates)
		{
			s_impl.GetFriendsUpdates(updates);
		}

		public static void SendFriendInvite(string inviter, string invitee, bool byEmail)
		{
			s_impl.SendFriendInvite(inviter, invitee, byEmail);
		}

		public static void ManageFriendInvite(int action, ulong inviteId)
		{
			s_impl.ManageFriendInvite(action, inviteId);
		}

		public static void RemoveFriend(BnetAccountId account)
		{
			s_impl.RemoveFriend(account);
		}

		public static void SendWhisper(BnetGameAccountId gameAccount, string message)
		{
			s_impl.SendWhisper(gameAccount, message);
		}

		public static void GetWhisperInfo(ref WhisperInfo info)
		{
			s_impl.GetWhisperInfo(ref info);
		}

		public static void GetWhispers([Out] BnetWhisper[] whispers)
		{
			s_impl.GetWhispers(whispers);
		}

		public static void ClearWhispers()
		{
			s_impl.ClearWhispers();
		}

		public static int GetNotificationCount()
		{
			return s_impl.GetNotificationCount();
		}

		public static void GetNotifications([Out] BnetNotification[] notifications)
		{
			s_impl.GetNotifications(notifications);
		}

		public static void ClearNotifications()
		{
			s_impl.ClearNotifications();
		}

		public static bool CheckWebAuth(out string url)
		{
			return s_impl.CheckWebAuth(out url);
		}

		public static bool HasExternalChallenge()
		{
			return s_impl.HasExternalChallenge();
		}

		public static void ProvideWebAuthToken(string token, RPCContextDelegate callback = null)
		{
			s_impl.ProvideWebAuthToken(token, callback);
		}

		public static void GenerateSSOToken(Action<bool, string> callback)
		{
			s_impl.GenerateSSOToken(callback);
		}

		public static void GenerateAppWebCredentials(Action<bool, string> callback)
		{
			s_impl.GenerateAppWebCredentials(callback);
		}

		public static void GenerateWtcgWebCredentials(Action<bool, string> callback)
		{
			s_impl.GenerateWtcgWebCredentials(callback);
		}

		public static string FilterProfanity(string unfiltered)
		{
			return s_impl.FilterProfanity(unfiltered);
		}

		public static void ApplicationWasPaused()
		{
			if (s_impl != null)
			{
				s_impl.ApplicationWasPaused();
			}
		}

		public static void ApplicationWasUnpaused()
		{
			if (s_impl != null)
			{
				s_impl.ApplicationWasUnpaused();
			}
		}

		public static string GetEnumAsString<T>(T enumVal)
		{
			string text = enumVal.ToString();
			DescriptionAttribute[] array = (DescriptionAttribute[])enumVal.GetType().GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
			if (array.Length != 0)
			{
				return array[0].Description;
			}
			return text;
		}
	}
}
