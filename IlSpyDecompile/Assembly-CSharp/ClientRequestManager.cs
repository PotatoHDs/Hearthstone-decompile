using System;
using System.Collections.Generic;
using System.Diagnostics;
using bgs;
using Blizzard.T5.Core;
using Networking;
using PegasusUtil;
using UnityEngine;

public class ClientRequestManager : IClientRequestManager
{
	public class ClientRequestConfig
	{
		public bool ShouldRetryOnError { get; set; }

		public bool ShouldRetryOnUnhandled { get; set; }

		public UtilSystemId RequestedSystem { get; set; }
	}

	private class ClientRequestType
	{
		public int Type;

		public int SubID;

		public byte[] Body;

		public uint Context;

		public RequestPhase Phase;

		public uint SendCount;

		public uint RequestNotHandledCount;

		public float SendTime;

		public uint RequestId;

		public bool IsSubscribeRequest;

		public SystemChannel System;

		public bool ShouldRetryOnError;

		public bool ShouldRetryOnUnhandled;

		public ulong RouteDispatchedTo;

		public ClientRequestType(SystemChannel system)
		{
			System = system;
		}

		public byte[] GetUtilPacketBytes()
		{
			RpcHeader rpcHeader = new RpcHeader();
			rpcHeader.Type = (ulong)Type;
			if (SendCount != 0)
			{
				rpcHeader.RetryCount = SendCount;
			}
			if (RequestNotHandledCount != 0)
			{
				rpcHeader.RequestNotHandledCount = RequestNotHandledCount;
			}
			RpcMessage rpcMessage = new RpcMessage();
			rpcMessage.RpcHeader = rpcHeader;
			if (Body != null && Body.Length != 0)
			{
				rpcMessage.MessageBody = Body;
			}
			return ProtobufUtil.ToByteArray(rpcMessage);
		}
	}

	private class SubscriptionStatusType
	{
		public enum State
		{
			PENDING_SEND,
			PENDING_RESPONSE,
			SUBSCRIBED
		}

		public State CurrentState;

		public DateTime LastSend = DateTime.MinValue;

		public float SubscribedTime;

		public uint ContexId;
	}

	private class PendingMapType
	{
		public Queue<ClientRequestType> PendingSend = new Queue<ClientRequestType>();
	}

	private class PhaseMapType
	{
		public PendingMapType StartUp = new PendingMapType();

		public PendingMapType Running = new PendingMapType();
	}

	private class SystemChannel
	{
		public PhaseMapType Phases = new PhaseMapType();

		public SubscriptionStatusType SubscriptionStatus = new SubscriptionStatusType();

		public ulong Route;

		public RequestPhase CurrentPhase;

		public ulong KeepAliveSecs;

		public ulong MaxResubscribeAttempts;

		public ulong PendingResponseTimeout;

		public ulong PendingSubscribeTimeout = 15uL;

		public uint SubscribeAttempt;

		public bool WasEverInRunningPhase;

		public UtilSystemId SystemId;

		public uint m_subscribePacketsReceived;
	}

	private class SystemMap
	{
		public Map<UtilSystemId, SystemChannel> Systems = new Map<UtilSystemId, SystemChannel>();
	}

	private class InternalState
	{
		public Queue<ResponseWithRequest> m_responsesPendingDelivery = new Queue<ResponseWithRequest>();

		public SystemMap m_systems = new SystemMap();

		public uint m_subscribePacketsSent;

		public bool m_loginCompleteNotificationReceived;

		public Map<uint, ClientRequestType> m_activePendingResponseMap = new Map<uint, ClientRequestType>();

		public HashSet<uint> m_ignorePendingResponseMap = new HashSet<uint>();

		public bool m_runningPhaseEnabled;

		public bool m_receivedErrorSignal;
	}

	private static Map<int, string> s_typeToStringMap = new Map<int, string>();

	private readonly ClientRequestConfig m_defaultConfig = new ClientRequestConfig
	{
		ShouldRetryOnError = true,
		ShouldRetryOnUnhandled = true,
		RequestedSystem = UtilSystemId.CLIENT
	};

	public uint m_nextContexId;

	public uint m_nextRequestId;

	private InternalState m_state = new InternalState();

	private Subscribe m_subscribePacket = new Subscribe();

	private bool m_hasSubscribedToUtilClient;

	public bool SendClientRequest(int type, IProtoBuf body, ClientRequestConfig clientRequestConfig, RequestPhase requestPhase = RequestPhase.RUNNING, int subID = 0)
	{
		return SendClientRequestImpl(type, body, clientRequestConfig, requestPhase, subID);
	}

	public void EnsureSubscribedTo(UtilSystemId system)
	{
		EnsureSubscribedToImpl(system);
	}

	public void NotifyResponseReceived(PegasusPacket packet)
	{
		NotifyResponseReceivedImpl(packet);
	}

	public void NotifyStartupSequenceComplete()
	{
		NotifyStartupSequenceCompleteImpl();
	}

	public bool HasPendingDeliveryPackets()
	{
		return HasPendingDeliveryPacketsImpl();
	}

	public int PeekNetClientRequestType()
	{
		return PeekNetClientRequestTypeImpl();
	}

	public ResponseWithRequest GetNextClientRequest()
	{
		return GetNextClientRequestImpl();
	}

	public void DropNextClientRequest()
	{
		DropNextClientRequestImpl();
	}

	public void NotifyLoginSequenceCompleted()
	{
		NotifyLoginSequenceCompletedImpl();
	}

	public bool ShouldIgnoreError(BnetErrorInfo errorInfo)
	{
		return ShouldIgnoreErrorImpl(errorInfo);
	}

	public void ScheduleResubscribe()
	{
		foreach (KeyValuePair<UtilSystemId, SystemChannel> system in m_state.m_systems.Systems)
		{
			ScheduleResubscribeWithNewRoute(system.Value);
		}
	}

	public void Terminate()
	{
		TerminateImpl();
	}

	public void SetDisconnectedFromBattleNet()
	{
		m_state = new InternalState();
	}

	public void Update()
	{
		UpdateImpl();
	}

	public bool HasErrors()
	{
		return HasErrorsImpl();
	}

	private bool ShouldIgnoreErrorImpl(BnetErrorInfo errorInfo)
	{
		uint context = (uint)errorInfo.GetContext();
		if (context == 0)
		{
			return false;
		}
		ClientRequestType clientRequest = GetClientRequest(context, "should_ignore_error");
		if (clientRequest == null)
		{
			if (GetDroppedRequest(context, "should_ignore"))
			{
				return true;
			}
			if (GetPendingSendRequest(context, "should_ignore") != null)
			{
				return true;
			}
			return false;
		}
		BattleNetErrors error = errorInfo.GetError();
		if (clientRequest.IsSubscribeRequest)
		{
			if (clientRequest.System.SubscribeAttempt >= clientRequest.System.MaxResubscribeAttempts)
			{
				return !clientRequest.ShouldRetryOnError;
			}
			return true;
		}
		switch (error)
		{
		case BattleNetErrors.ERROR_GAME_UTILITY_SERVER_NO_SERVER:
			clientRequest.RequestNotHandledCount++;
			if (!clientRequest.ShouldRetryOnUnhandled)
			{
				return true;
			}
			return RescheduleSubscriptionAndRetryRequest(clientRequest, "received_error_util_server_no_server");
		case BattleNetErrors.ERROR_INTERNAL:
		case BattleNetErrors.ERROR_RPC_REQUEST_TIMED_OUT:
			if (!clientRequest.ShouldRetryOnError)
			{
				return true;
			}
			if (clientRequest.System.PendingResponseTimeout == 0L)
			{
				return false;
			}
			return RescheduleSubscriptionAndRetryRequest(clientRequest, "received_error_util_lost");
		default:
			return false;
		}
	}

	private bool RescheduleSubscriptionAndRetryRequest(ClientRequestType clientRequest, string errorReason)
	{
		if (clientRequest.RouteDispatchedTo == clientRequest.System.Route)
		{
			ScheduleResubscribeWithNewRoute(clientRequest.System);
		}
		AddRequestToPendingSendQueue(clientRequest, "resubscribe_and_retry_request");
		return true;
	}

	private void ProcessServiceUnavailable(ClientRequestResponse response, ClientRequestType clientRequest)
	{
		clientRequest.RequestNotHandledCount++;
		RescheduleSubscriptionAndRetryRequest(clientRequest, "received_CRRF_SERVICE_UNAVAILABLE");
	}

	private void ProcessClientRequestResponse(PegasusPacket packet, ClientRequestType clientRequest)
	{
		if (packet.Body is ClientRequestResponse)
		{
			ClientRequestResponse clientRequestResponse = (ClientRequestResponse)packet.Body;
			ClientRequestResponse.ClientRequestResponseFlags clientRequestResponseFlags = ClientRequestResponse.ClientRequestResponseFlags.CRRF_SERVICE_UNAVAILABLE;
			if ((clientRequestResponse.ResponseFlags & clientRequestResponseFlags) != 0)
			{
				ProcessServiceUnavailable(clientRequestResponse, clientRequest);
			}
			ClientRequestResponse.ClientRequestResponseFlags clientRequestResponseFlags2 = ClientRequestResponse.ClientRequestResponseFlags.CRRF_SERVICE_UNKNOWN_ERROR;
			if ((clientRequestResponse.ResponseFlags & clientRequestResponseFlags2) != 0)
			{
				m_state.m_receivedErrorSignal = true;
			}
		}
	}

	[Conditional("CLIENTREQUESTMANAGER_LOGGING")]
	private void PopulateStringMap()
	{
		s_typeToStringMap.Add(201, "GetAccountInfo");
		s_typeToStringMap.Add(202, "DeckList");
		s_typeToStringMap.Add(203, "UtilHandshake");
		s_typeToStringMap.Add(204, "UtilAuth");
		s_typeToStringMap.Add(205, "UpdateLogin");
		s_typeToStringMap.Add(206, "DebugAuth");
		s_typeToStringMap.Add(207, "InitialClientState");
		s_typeToStringMap.Add(208, "GamesInfo");
		s_typeToStringMap.Add(209, "CreateDeck");
		s_typeToStringMap.Add(210, "DeleteDeck");
		s_typeToStringMap.Add(211, "RenameDeck");
		s_typeToStringMap.Add(213, "AckNotice");
		s_typeToStringMap.Add(214, "GetDeck");
		s_typeToStringMap.Add(215, "DeckContents");
		s_typeToStringMap.Add(216, "DBAction");
		s_typeToStringMap.Add(217, "DeckCreated");
		s_typeToStringMap.Add(218, "DeckDeleted");
		s_typeToStringMap.Add(219, "DeckRenamed");
		s_typeToStringMap.Add(220, "DeckGainedCard");
		s_typeToStringMap.Add(221, "DeckLostCard");
		s_typeToStringMap.Add(222, "DeckSetData");
		s_typeToStringMap.Add(223, "AckCardSeen");
		s_typeToStringMap.Add(225, "OpenBooster");
		s_typeToStringMap.Add(226, "BoosterContent");
		s_typeToStringMap.Add(227, "ProfileLastLogin");
		s_typeToStringMap.Add(228, "ClientTracking");
		s_typeToStringMap.Add(229, "unused");
		s_typeToStringMap.Add(230, "SetProgress");
		s_typeToStringMap.Add(231, "ProfileDeckLimit");
		s_typeToStringMap.Add(232, "MedalInfo");
		s_typeToStringMap.Add(233, "ProfileProgress");
		s_typeToStringMap.Add(234, "MedalHistory");
		s_typeToStringMap.Add(235, "DraftBegin");
		s_typeToStringMap.Add(236, "CardBacks");
		s_typeToStringMap.Add(237, "GetBattlePayConfig");
		s_typeToStringMap.Add(238, "BattlePayConfigResponse");
		s_typeToStringMap.Add(239, "SetOptions");
		s_typeToStringMap.Add(240, "GetOptions");
		s_typeToStringMap.Add(241, "ClientOptions");
		s_typeToStringMap.Add(242, "DraftRetire");
		s_typeToStringMap.Add(243, "AckAchieveProgress");
		s_typeToStringMap.Add(244, "DraftGetChoicesAndContents");
		s_typeToStringMap.Add(245, "DraftMakePick");
		s_typeToStringMap.Add(246, "DraftBeginning");
		s_typeToStringMap.Add(247, "DraftRetired");
		s_typeToStringMap.Add(248, "DraftChoicesAndContents");
		s_typeToStringMap.Add(249, "DraftChosen");
		s_typeToStringMap.Add(250, "GetPurchaseMethod");
		s_typeToStringMap.Add(251, "DraftError");
		s_typeToStringMap.Add(254, "NOP");
		s_typeToStringMap.Add(255, "GetBattlePayStatus");
		s_typeToStringMap.Add(256, "PurchaseResponse");
		s_typeToStringMap.Add(257, "BuySellCard");
		s_typeToStringMap.Add(258, "BoughtSoldCard");
		s_typeToStringMap.Add(259, "DevBnetIdentify");
		s_typeToStringMap.Add(260, "CardValues");
		s_typeToStringMap.Add(261, "GuardianTrack");
		s_typeToStringMap.Add(262, "ArcaneDustBalance");
		s_typeToStringMap.Add(263, "CloseCardMarket");
		s_typeToStringMap.Add(264, "GuardianVars");
		s_typeToStringMap.Add(265, "BattlePayStatusResponse");
		s_typeToStringMap.Add(266, "Error37 (deprecated)");
		s_typeToStringMap.Add(267, "CheckAccountLicenses");
		s_typeToStringMap.Add(268, "MassDisenchant");
		s_typeToStringMap.Add(269, "MassDisenchantResponse");
		s_typeToStringMap.Add(270, "PlayerRecords");
		s_typeToStringMap.Add(271, "RewardProgress");
		s_typeToStringMap.Add(272, "PurchaseMethod");
		s_typeToStringMap.Add(273, "DoPurchase");
		s_typeToStringMap.Add(274, "CancelPurchase");
		s_typeToStringMap.Add(275, "CancelPurchaseResponse");
		s_typeToStringMap.Add(276, "CheckGameLicenses");
		s_typeToStringMap.Add(277, "CheckLicensesResponse");
		s_typeToStringMap.Add(278, "GoldBalance");
		s_typeToStringMap.Add(279, "PurchaseWithGold");
		s_typeToStringMap.Add(280, "PurchaseWithGoldResponse");
		s_typeToStringMap.Add(281, "CancelQuest");
		s_typeToStringMap.Add(282, "CancelQuestResponse");
		s_typeToStringMap.Add(283, "HeroXP");
		s_typeToStringMap.Add(284, "ValidateAchieve");
		s_typeToStringMap.Add(285, "ValidateAchieveResponse");
		s_typeToStringMap.Add(286, "PlayQueue");
		s_typeToStringMap.Add(287, "DraftAckRewards");
		s_typeToStringMap.Add(288, "DraftRewardsAcked");
		s_typeToStringMap.Add(289, "Disconnected");
		s_typeToStringMap.Add(290, "Deadend");
		s_typeToStringMap.Add(291, "SetCardBack");
		s_typeToStringMap.Add(292, "SetCardBackResponse");
		s_typeToStringMap.Add(293, "SubmitThirdPartyReceipt");
		s_typeToStringMap.Add(294, "GetThirdPartyPurchaseStatus");
		s_typeToStringMap.Add(295, "ThirdPartyPurchaseStatusResponse");
		s_typeToStringMap.Add(296, "SetProgressResponse");
		s_typeToStringMap.Add(297, "CheckAccountLicenseAchieve");
		s_typeToStringMap.Add(298, "TriggerPlayedNearbyPlayerOnSubnet");
		s_typeToStringMap.Add(299, "EventResponse");
		s_typeToStringMap.Add(300, "MassiveLoginReply");
		s_typeToStringMap.Add(301, "(used in Console.proto)");
		s_typeToStringMap.Add(302, "(used in Console.proto)");
		s_typeToStringMap.Add(303, "GetAssetsVersion");
		s_typeToStringMap.Add(304, "AssetsVersionResponse");
		s_typeToStringMap.Add(305, "GetAdventureProgress");
		s_typeToStringMap.Add(306, "AdventureProgressResponse");
		s_typeToStringMap.Add(307, "UpdateLoginComplete");
		s_typeToStringMap.Add(308, "AckWingProgress");
		s_typeToStringMap.Add(309, "SetPlayerAdventureProgress");
		s_typeToStringMap.Add(310, "SetAdventureOptions");
		s_typeToStringMap.Add(311, "AccountLicenseAchieveResponse");
		s_typeToStringMap.Add(312, "StartThirdPartyPurchase");
		s_typeToStringMap.Add(314, "Subscribe");
		s_typeToStringMap.Add(315, "SubscribeResponse");
		s_typeToStringMap.Add(316, "TavernBrawlInfo");
		s_typeToStringMap.Add(317, "TavernBrawlPlayerRecordResponse");
		s_typeToStringMap.Add(318, "FavoriteHeroesResponse");
		s_typeToStringMap.Add(319, "SetFavoriteHero");
		s_typeToStringMap.Add(320, "SetFavoriteHeroResponse");
		s_typeToStringMap.Add(321, "GetAssetRequest");
		s_typeToStringMap.Add(322, "GetAssetResponse");
		s_typeToStringMap.Add(323, "DebugCommandRequest");
		s_typeToStringMap.Add(324, "DebugCommandResponse");
		s_typeToStringMap.Add(325, "AccountLicensesInfoResponse");
		s_typeToStringMap.Add(326, "GenericResponse");
		s_typeToStringMap.Add(327, "GenericRequestList");
		s_typeToStringMap.Add(328, "ClientRequestResponse");
		s_typeToStringMap.Add(381, "Coins");
		s_typeToStringMap.Add(382, "SetFavoriteCoin");
		s_typeToStringMap.Add(383, "SetFavoriteCoinResponse");
		s_typeToStringMap.Add(384, "CoinUpdate");
	}

	private string GetTypeName(int type)
	{
		string text = type.ToString();
		if (s_typeToStringMap.Count > 0 && s_typeToStringMap.TryGetValue(type, out var value))
		{
			return value + ":" + text;
		}
		return text;
	}

	[Conditional("CLIENTREQUESTMANAGER_LOGGING")]
	private void LOG_DEBUG(string format, params object[] args)
	{
		string text = GeneralUtils.SafeFormat(format, args);
		Log.ClientRequestManager.Print("D " + text);
	}

	[Conditional("CLIENTREQUESTMANAGER_LOGGING")]
	private void LOG_WARN(string format, params object[] args)
	{
		string text = GeneralUtils.SafeFormat(format, args);
		Log.ClientRequestManager.Print("W " + text);
	}

	[Conditional("CLIENTREQUESTMANAGER_LOGGING")]
	private void LOG_ERROR(string format, params object[] args)
	{
		string text = GeneralUtils.SafeFormat(format, args);
		Log.ClientRequestManager.Print("E " + text);
	}

	private bool HasPendingDeliveryPacketsImpl()
	{
		return m_state.m_responsesPendingDelivery.Count > 0;
	}

	private int PeekNetClientRequestTypeImpl()
	{
		if (m_state.m_responsesPendingDelivery.Count == 0)
		{
			return 0;
		}
		return m_state.m_responsesPendingDelivery.Peek().Response.Type;
	}

	private ResponseWithRequest GetNextClientRequestImpl()
	{
		if (m_state.m_responsesPendingDelivery.Count == 0)
		{
			return null;
		}
		return m_state.m_responsesPendingDelivery.Peek();
	}

	private void DropNextClientRequestImpl()
	{
		if (m_state.m_responsesPendingDelivery.Count != 0)
		{
			m_state.m_responsesPendingDelivery.Dequeue();
		}
	}

	private bool HasErrorsImpl()
	{
		return m_state.m_receivedErrorSignal;
	}

	private void UpdateImpl()
	{
		if (!m_state.m_loginCompleteNotificationReceived || !m_hasSubscribedToUtilClient)
		{
			return;
		}
		SystemChannel system = m_state.m_systems.Systems[UtilSystemId.CLIENT];
		if (!UpdateStateSubscribeImpl(system))
		{
			return;
		}
		ProcessClientRequests(system);
		foreach (KeyValuePair<UtilSystemId, SystemChannel> system2 in m_state.m_systems.Systems)
		{
			if (system2.Key != 0 && UpdateStateSubscribeImpl(system2.Value))
			{
				ProcessClientRequests(system2.Value);
			}
		}
	}

	private bool SendClientRequestImpl(int type, IProtoBuf body, ClientRequestConfig clientRequestConfig, RequestPhase requestPhase, int subID)
	{
		if (type == 0)
		{
			return false;
		}
		if (requestPhase < RequestPhase.STARTUP || requestPhase > RequestPhase.RUNNING)
		{
			return false;
		}
		ClientRequestConfig clientRequestConfig2 = ((clientRequestConfig == null) ? m_defaultConfig : clientRequestConfig);
		UtilSystemId requestedSystem = clientRequestConfig2.RequestedSystem;
		SystemChannel orCreateSystem = GetOrCreateSystem(requestedSystem);
		if (requestPhase < orCreateSystem.CurrentPhase)
		{
			return false;
		}
		if (orCreateSystem.WasEverInRunningPhase && requestPhase < RequestPhase.RUNNING)
		{
			return false;
		}
		if (body == null)
		{
			return false;
		}
		ClientRequestType clientRequestType = new ClientRequestType(orCreateSystem);
		clientRequestType.Type = type;
		clientRequestType.ShouldRetryOnError = clientRequestConfig2.ShouldRetryOnError;
		clientRequestType.ShouldRetryOnUnhandled = clientRequestConfig2.ShouldRetryOnUnhandled;
		clientRequestType.SubID = subID;
		clientRequestType.Body = ProtobufUtil.ToByteArray(body);
		clientRequestType.Phase = requestPhase;
		clientRequestType.SendCount = 0u;
		clientRequestType.RequestNotHandledCount = 0u;
		clientRequestType.RequestId = GetNextRequestId();
		if (clientRequestType.Phase == RequestPhase.STARTUP)
		{
			orCreateSystem.Phases.StartUp.PendingSend.Enqueue(clientRequestType);
		}
		else
		{
			orCreateSystem.Phases.Running.PendingSend.Enqueue(clientRequestType);
		}
		return true;
	}

	private SystemChannel GetOrCreateSystem(UtilSystemId systemId)
	{
		SystemChannel value = null;
		if (m_state.m_systems.Systems.TryGetValue(systemId, out value))
		{
			return value;
		}
		value = new SystemChannel();
		value.SystemId = systemId;
		m_state.m_systems.Systems[systemId] = value;
		if (systemId == UtilSystemId.CLIENT)
		{
			m_hasSubscribedToUtilClient = true;
		}
		return value;
	}

	public void EnsureSubscribedToImpl(UtilSystemId systemId)
	{
		GetOrCreateSystem(systemId);
	}

	private uint GenerateContextId()
	{
		return ++m_nextContexId;
	}

	private void NotifyResponseReceivedImpl(PegasusPacket packet)
	{
		uint context = (uint)packet.Context;
		ClientRequestType clientRequest = GetClientRequest(context, "received_response");
		if (clientRequest == null)
		{
			if (packet.Context == 0 || !GetDroppedRequest(context, "received_response"))
			{
				m_state.m_responsesPendingDelivery.Enqueue(new ResponseWithRequest(packet));
			}
			return;
		}
		switch (packet.Type)
		{
		case 315:
			ProcessSubscribeResponse(packet, clientRequest);
			break;
		case 328:
			ProcessClientRequestResponse(packet, clientRequest);
			break;
		default:
			ProcessResponse(packet, clientRequest);
			break;
		}
	}

	private void NotifyStartupSequenceCompleteImpl()
	{
		m_state.m_runningPhaseEnabled = true;
	}

	private void NotifyLoginSequenceCompletedImpl()
	{
		m_state.m_loginCompleteNotificationReceived = true;
	}

	private uint SendToUtil(ClientRequestType request)
	{
		uint num = GenerateContextId();
		ulong route = request.System.Route;
		byte[] utilPacketBytes = request.GetUtilPacketBytes();
		BattleNet.SendUtilPacket(request.Type, request.System.SystemId, utilPacketBytes, utilPacketBytes.Length, request.SubID, (int)num, route);
		request.Context = num;
		request.SendTime = Time.realtimeSinceStartup;
		request.SendCount++;
		request.RouteDispatchedTo = route;
		AddRequestToPendingResponse(request, "send_to_util");
		if (!request.IsSubscribeRequest)
		{
			request.Phase.ToString();
		}
		return num;
	}

	private uint GetNextRequestId()
	{
		return ++m_nextRequestId;
	}

	private void SendSubscriptionRequest(SystemChannel system)
	{
		UtilSystemId systemId = system.SystemId;
		if (system.Route == 0L)
		{
			m_subscribePacket.FirstSubscribeForRoute = true;
		}
		else
		{
			m_subscribePacket.FirstSubscribeForRoute = false;
		}
		m_subscribePacket.FirstSubscribe = system.SubscriptionStatus.LastSend == DateTime.MinValue;
		m_subscribePacket.UtilSystemId = (int)systemId;
		ClientRequestType clientRequestType = new ClientRequestType(system);
		clientRequestType.Type = 314;
		clientRequestType.SubID = 0;
		clientRequestType.Body = ProtobufUtil.ToByteArray(m_subscribePacket);
		clientRequestType.RequestId = GetNextRequestId();
		clientRequestType.IsSubscribeRequest = true;
		system.SubscriptionStatus.CurrentState = SubscriptionStatusType.State.PENDING_RESPONSE;
		system.SubscriptionStatus.LastSend = DateTime.Now;
		system.SubscriptionStatus.ContexId = SendToUtil(clientRequestType);
		system.SubscribeAttempt++;
		m_state.m_subscribePacketsSent++;
	}

	private void ScheduleResubscribeWithNewRoute(SystemChannel system)
	{
		system.Route = 0uL;
		system.SubscriptionStatus.CurrentState = SubscriptionStatusType.State.PENDING_SEND;
	}

	private void TerminateImpl()
	{
		Unsubscribe packet = new Unsubscribe();
		foreach (KeyValuePair<UtilSystemId, SystemChannel> system in m_state.m_systems.Systems)
		{
			SystemChannel value = system.Value;
			if (value.SubscriptionStatus.CurrentState == SubscriptionStatusType.State.SUBSCRIBED && value.Route != 0L && HearthstoneServices.TryGet<Network>(out var service))
			{
				service.SendUnsubcribeRequest(packet, value.SystemId);
			}
		}
	}

	private bool UpdateStateSubscribeImpl(SystemChannel system)
	{
		return system.SubscriptionStatus.CurrentState switch
		{
			SubscriptionStatusType.State.PENDING_SEND => ProcessSubscribeStatePendingSend(system), 
			SubscriptionStatusType.State.PENDING_RESPONSE => ProcessSubscribeStatePendingResponse(system), 
			SubscriptionStatusType.State.SUBSCRIBED => ProcessSubscribeStateSubscribed(system), 
			_ => system.SubscriptionStatus.CurrentState == SubscriptionStatusType.State.SUBSCRIBED, 
		};
	}

	private bool ProcessSubscribeStatePendingSend(SystemChannel system)
	{
		if ((DateTime.Now - system.SubscriptionStatus.LastSend).TotalSeconds > (double)system.PendingSubscribeTimeout)
		{
			SendSubscriptionRequest(system);
		}
		return system.Route != 0;
	}

	private bool ProcessSubscribeStatePendingResponse(SystemChannel system)
	{
		if ((DateTime.Now - system.SubscriptionStatus.LastSend).TotalSeconds > (double)system.PendingSubscribeTimeout)
		{
			ScheduleResubscribeWithNewRoute(system);
		}
		return system.Route != 0;
	}

	private int CountPendingResponsesForSystemId(SystemChannel system)
	{
		int num = 0;
		foreach (KeyValuePair<uint, ClientRequestType> item in m_state.m_activePendingResponseMap)
		{
			if (item.Value.System.SystemId == system.SystemId)
			{
				num++;
			}
		}
		return num;
	}

	private bool ProcessSubscribeStateSubscribed(SystemChannel system)
	{
		if ((ulong)(Time.realtimeSinceStartup - system.SubscriptionStatus.SubscribedTime) < system.KeepAliveSecs)
		{
			return true;
		}
		if (CountPendingResponsesForSystemId(system) > 0)
		{
			return true;
		}
		if (system.KeepAliveSecs != 0)
		{
			system.SubscriptionStatus.CurrentState = SubscriptionStatusType.State.PENDING_SEND;
		}
		return true;
	}

	private void ProcessSubscribeResponse(PegasusPacket packet, ClientRequestType request)
	{
		if (packet.Body is SubscribeResponse)
		{
			SystemChannel system = request.System;
			_ = system.SystemId;
			SubscribeResponse subscribeResponse = (SubscribeResponse)packet.Body;
			if (subscribeResponse.Result == SubscribeResponse.ResponseResult.FAILED_UNAVAILABLE)
			{
				ScheduleResubscribeWithNewRoute(system);
				return;
			}
			system.SubscriptionStatus.CurrentState = SubscriptionStatusType.State.SUBSCRIBED;
			system.SubscriptionStatus.SubscribedTime = Time.realtimeSinceStartup;
			system.Route = subscribeResponse.Route;
			system.CurrentPhase = RequestPhase.STARTUP;
			system.SubscribeAttempt = 0u;
			system.KeepAliveSecs = subscribeResponse.KeepAliveSecs;
			system.MaxResubscribeAttempts = subscribeResponse.MaxResubscribeAttempts;
			system.PendingResponseTimeout = subscribeResponse.PendingResponseTimeout;
			system.PendingSubscribeTimeout = subscribeResponse.PendingSubscribeTimeout;
			PegasusPacket request2 = new PegasusPacket(request.Type, packet.Context, request.Body);
			m_state.m_responsesPendingDelivery.Enqueue(new ResponseWithRequest(packet, request2));
			system.m_subscribePacketsReceived++;
		}
	}

	private void ProcessClientRequests(SystemChannel system)
	{
		PendingMapType pendingMapType = ((system.CurrentPhase == RequestPhase.STARTUP) ? system.Phases.StartUp : system.Phases.Running);
		foreach (KeyValuePair<uint, ClientRequestType> item in m_state.m_activePendingResponseMap)
		{
			ClientRequestType value = item.Value;
			if (!value.IsSubscribeRequest && value.System != null && value.System.SystemId == system.SystemId && system.PendingResponseTimeout != 0L && Time.realtimeSinceStartup - value.SendTime >= (float)system.PendingResponseTimeout)
			{
				ScheduleResubscribeWithNewRoute(system);
				return;
			}
		}
		if (system.Route != 0L)
		{
			bool flag = pendingMapType.PendingSend.Count > 0;
			while (pendingMapType.PendingSend.Count > 0)
			{
				ClientRequestType request = pendingMapType.PendingSend.Dequeue();
				SendToUtil(request);
			}
			if (!flag && system.CurrentPhase == RequestPhase.STARTUP && m_state.m_runningPhaseEnabled)
			{
				system.CurrentPhase = RequestPhase.RUNNING;
			}
		}
	}

	private void ProcessResponse(PegasusPacket packet, ClientRequestType clientRequest)
	{
		if (packet.Type != 254)
		{
			PegasusPacket request = new PegasusPacket(clientRequest.Type, packet.Context, clientRequest.Body);
			m_state.m_responsesPendingDelivery.Enqueue(new ResponseWithRequest(packet, request));
		}
	}

	private ClientRequestType GetClientRequest(uint contextId, string reason, bool removeIfFound = true)
	{
		if (contextId == 0)
		{
			return null;
		}
		if (!m_state.m_activePendingResponseMap.TryGetValue(contextId, out var value))
		{
			if (GetDroppedRequest(contextId, "get_client_request", removeIfFound: false))
			{
				GetPendingSendRequest(contextId, "get_client_request", removeIfFound: false);
			}
			return null;
		}
		if (removeIfFound)
		{
			m_state.m_activePendingResponseMap.Remove(contextId);
		}
		return value;
	}

	private void AddRequestToPendingSendQueue(ClientRequestType clientRequest, string reason)
	{
		if (clientRequest.Phase == RequestPhase.STARTUP)
		{
			clientRequest.System.Phases.StartUp.PendingSend.Enqueue(clientRequest);
			_ = clientRequest.System.Phases.StartUp.PendingSend.Count;
		}
		else
		{
			clientRequest.System.Phases.Running.PendingSend.Enqueue(clientRequest);
			_ = clientRequest.System.Phases.Running.PendingSend.Count;
		}
	}

	private void AddRequestToPendingResponse(ClientRequestType clientRequest, string reason)
	{
		if (!m_state.m_activePendingResponseMap.ContainsKey(clientRequest.Context))
		{
			m_state.m_activePendingResponseMap.Add(clientRequest.Context, clientRequest);
		}
	}

	private bool GetDroppedRequest(uint contextId, string reason, bool removeIfFound = true)
	{
		if (m_state.m_ignorePendingResponseMap.Contains(contextId) && removeIfFound)
		{
			m_state.m_ignorePendingResponseMap.Remove(contextId);
			return true;
		}
		return false;
	}

	private ClientRequestType GetPendingSendRequestForPhase(uint contextId, bool removeIfFound, PendingMapType pendingMap)
	{
		ClientRequestType clientRequestType = null;
		Queue<ClientRequestType> queue = new Queue<ClientRequestType>();
		foreach (ClientRequestType item in pendingMap.PendingSend)
		{
			if (clientRequestType == null && item.Context == contextId)
			{
				clientRequestType = item;
				if (!removeIfFound)
				{
					queue.Enqueue(item);
				}
			}
			else
			{
				queue.Enqueue(item);
			}
		}
		pendingMap.PendingSend = queue;
		return clientRequestType;
	}

	private ClientRequestType GetPendingSendRequest(uint contextId, string reason, bool removeIfFound = true)
	{
		ClientRequestType result = null;
		foreach (KeyValuePair<UtilSystemId, SystemChannel> system in m_state.m_systems.Systems)
		{
			SystemChannel value = system.Value;
			result = GetPendingSendRequestForPhase(contextId, removeIfFound, value.Phases.Running);
			if (result == null)
			{
				result = GetPendingSendRequestForPhase(contextId, removeIfFound, value.Phases.StartUp);
				continue;
			}
			return result;
		}
		return result;
	}
}
