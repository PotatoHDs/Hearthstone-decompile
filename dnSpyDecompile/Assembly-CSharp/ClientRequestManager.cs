using System;
using System.Collections.Generic;
using System.Diagnostics;
using bgs;
using Blizzard.T5.Core;
using Networking;
using PegasusUtil;
using UnityEngine;

// Token: 0x020005FF RID: 1535
public class ClientRequestManager : IClientRequestManager
{
	// Token: 0x060053C0 RID: 21440 RVA: 0x001B58D0 File Offset: 0x001B3AD0
	public bool SendClientRequest(int type, IProtoBuf body, ClientRequestManager.ClientRequestConfig clientRequestConfig, RequestPhase requestPhase = RequestPhase.RUNNING, int subID = 0)
	{
		return this.SendClientRequestImpl(type, body, clientRequestConfig, requestPhase, subID);
	}

	// Token: 0x060053C1 RID: 21441 RVA: 0x001B58DF File Offset: 0x001B3ADF
	public void EnsureSubscribedTo(UtilSystemId system)
	{
		this.EnsureSubscribedToImpl(system);
	}

	// Token: 0x060053C2 RID: 21442 RVA: 0x001B58E8 File Offset: 0x001B3AE8
	public void NotifyResponseReceived(PegasusPacket packet)
	{
		this.NotifyResponseReceivedImpl(packet);
	}

	// Token: 0x060053C3 RID: 21443 RVA: 0x001B58F1 File Offset: 0x001B3AF1
	public void NotifyStartupSequenceComplete()
	{
		this.NotifyStartupSequenceCompleteImpl();
	}

	// Token: 0x060053C4 RID: 21444 RVA: 0x001B58F9 File Offset: 0x001B3AF9
	public bool HasPendingDeliveryPackets()
	{
		return this.HasPendingDeliveryPacketsImpl();
	}

	// Token: 0x060053C5 RID: 21445 RVA: 0x001B5901 File Offset: 0x001B3B01
	public int PeekNetClientRequestType()
	{
		return this.PeekNetClientRequestTypeImpl();
	}

	// Token: 0x060053C6 RID: 21446 RVA: 0x001B5909 File Offset: 0x001B3B09
	public ResponseWithRequest GetNextClientRequest()
	{
		return this.GetNextClientRequestImpl();
	}

	// Token: 0x060053C7 RID: 21447 RVA: 0x001B5911 File Offset: 0x001B3B11
	public void DropNextClientRequest()
	{
		this.DropNextClientRequestImpl();
	}

	// Token: 0x060053C8 RID: 21448 RVA: 0x001B5919 File Offset: 0x001B3B19
	public void NotifyLoginSequenceCompleted()
	{
		this.NotifyLoginSequenceCompletedImpl();
	}

	// Token: 0x060053C9 RID: 21449 RVA: 0x001B5921 File Offset: 0x001B3B21
	public bool ShouldIgnoreError(BnetErrorInfo errorInfo)
	{
		return this.ShouldIgnoreErrorImpl(errorInfo);
	}

	// Token: 0x060053CA RID: 21450 RVA: 0x001B592C File Offset: 0x001B3B2C
	public void ScheduleResubscribe()
	{
		foreach (KeyValuePair<UtilSystemId, ClientRequestManager.SystemChannel> keyValuePair in this.m_state.m_systems.Systems)
		{
			this.ScheduleResubscribeWithNewRoute(keyValuePair.Value);
		}
	}

	// Token: 0x060053CB RID: 21451 RVA: 0x001B5990 File Offset: 0x001B3B90
	public void Terminate()
	{
		this.TerminateImpl();
	}

	// Token: 0x060053CC RID: 21452 RVA: 0x001B5998 File Offset: 0x001B3B98
	public void SetDisconnectedFromBattleNet()
	{
		this.m_state = new ClientRequestManager.InternalState();
	}

	// Token: 0x060053CD RID: 21453 RVA: 0x001B59A5 File Offset: 0x001B3BA5
	public void Update()
	{
		this.UpdateImpl();
	}

	// Token: 0x060053CE RID: 21454 RVA: 0x001B59AD File Offset: 0x001B3BAD
	public bool HasErrors()
	{
		return this.HasErrorsImpl();
	}

	// Token: 0x060053CF RID: 21455 RVA: 0x001B59B8 File Offset: 0x001B3BB8
	private bool ShouldIgnoreErrorImpl(BnetErrorInfo errorInfo)
	{
		uint context = (uint)errorInfo.GetContext();
		if (context == 0U)
		{
			return false;
		}
		ClientRequestManager.ClientRequestType clientRequest = this.GetClientRequest(context, "should_ignore_error", true);
		if (clientRequest == null)
		{
			return this.GetDroppedRequest(context, "should_ignore", true) || this.GetPendingSendRequest(context, "should_ignore", true) != null;
		}
		BattleNetErrors error = errorInfo.GetError();
		if (clientRequest.IsSubscribeRequest)
		{
			return (ulong)clientRequest.System.SubscribeAttempt < clientRequest.System.MaxResubscribeAttempts || !clientRequest.ShouldRetryOnError;
		}
		if (error == BattleNetErrors.ERROR_INTERNAL || error == BattleNetErrors.ERROR_RPC_REQUEST_TIMED_OUT)
		{
			return !clientRequest.ShouldRetryOnError || (clientRequest.System.PendingResponseTimeout != 0UL && this.RescheduleSubscriptionAndRetryRequest(clientRequest, "received_error_util_lost"));
		}
		if (error == BattleNetErrors.ERROR_GAME_UTILITY_SERVER_NO_SERVER)
		{
			clientRequest.RequestNotHandledCount += 1U;
			return !clientRequest.ShouldRetryOnUnhandled || this.RescheduleSubscriptionAndRetryRequest(clientRequest, "received_error_util_server_no_server");
		}
		return false;
	}

	// Token: 0x060053D0 RID: 21456 RVA: 0x001B5A9A File Offset: 0x001B3C9A
	private bool RescheduleSubscriptionAndRetryRequest(ClientRequestManager.ClientRequestType clientRequest, string errorReason)
	{
		if (clientRequest.RouteDispatchedTo == clientRequest.System.Route)
		{
			this.ScheduleResubscribeWithNewRoute(clientRequest.System);
		}
		this.AddRequestToPendingSendQueue(clientRequest, "resubscribe_and_retry_request");
		return true;
	}

	// Token: 0x060053D1 RID: 21457 RVA: 0x001B5AC8 File Offset: 0x001B3CC8
	private void ProcessServiceUnavailable(ClientRequestResponse response, ClientRequestManager.ClientRequestType clientRequest)
	{
		clientRequest.RequestNotHandledCount += 1U;
		this.RescheduleSubscriptionAndRetryRequest(clientRequest, "received_CRRF_SERVICE_UNAVAILABLE");
	}

	// Token: 0x060053D2 RID: 21458 RVA: 0x001B5AE8 File Offset: 0x001B3CE8
	private void ProcessClientRequestResponse(PegasusPacket packet, ClientRequestManager.ClientRequestType clientRequest)
	{
		if (!(packet.Body is ClientRequestResponse))
		{
			return;
		}
		ClientRequestResponse clientRequestResponse = (ClientRequestResponse)packet.Body;
		ClientRequestResponse.ClientRequestResponseFlags clientRequestResponseFlags = ClientRequestResponse.ClientRequestResponseFlags.CRRF_SERVICE_UNAVAILABLE;
		if ((clientRequestResponse.ResponseFlags & clientRequestResponseFlags) != ClientRequestResponse.ClientRequestResponseFlags.CRRF_SERVICE_NONE)
		{
			this.ProcessServiceUnavailable(clientRequestResponse, clientRequest);
		}
		ClientRequestResponse.ClientRequestResponseFlags clientRequestResponseFlags2 = ClientRequestResponse.ClientRequestResponseFlags.CRRF_SERVICE_UNKNOWN_ERROR;
		if ((clientRequestResponse.ResponseFlags & clientRequestResponseFlags2) != ClientRequestResponse.ClientRequestResponseFlags.CRRF_SERVICE_NONE)
		{
			this.m_state.m_receivedErrorSignal = true;
		}
	}

	// Token: 0x060053D3 RID: 21459 RVA: 0x001B5B3C File Offset: 0x001B3D3C
	[Conditional("CLIENTREQUESTMANAGER_LOGGING")]
	private void PopulateStringMap()
	{
		ClientRequestManager.s_typeToStringMap.Add(201, "GetAccountInfo");
		ClientRequestManager.s_typeToStringMap.Add(202, "DeckList");
		ClientRequestManager.s_typeToStringMap.Add(203, "UtilHandshake");
		ClientRequestManager.s_typeToStringMap.Add(204, "UtilAuth");
		ClientRequestManager.s_typeToStringMap.Add(205, "UpdateLogin");
		ClientRequestManager.s_typeToStringMap.Add(206, "DebugAuth");
		ClientRequestManager.s_typeToStringMap.Add(207, "InitialClientState");
		ClientRequestManager.s_typeToStringMap.Add(208, "GamesInfo");
		ClientRequestManager.s_typeToStringMap.Add(209, "CreateDeck");
		ClientRequestManager.s_typeToStringMap.Add(210, "DeleteDeck");
		ClientRequestManager.s_typeToStringMap.Add(211, "RenameDeck");
		ClientRequestManager.s_typeToStringMap.Add(213, "AckNotice");
		ClientRequestManager.s_typeToStringMap.Add(214, "GetDeck");
		ClientRequestManager.s_typeToStringMap.Add(215, "DeckContents");
		ClientRequestManager.s_typeToStringMap.Add(216, "DBAction");
		ClientRequestManager.s_typeToStringMap.Add(217, "DeckCreated");
		ClientRequestManager.s_typeToStringMap.Add(218, "DeckDeleted");
		ClientRequestManager.s_typeToStringMap.Add(219, "DeckRenamed");
		ClientRequestManager.s_typeToStringMap.Add(220, "DeckGainedCard");
		ClientRequestManager.s_typeToStringMap.Add(221, "DeckLostCard");
		ClientRequestManager.s_typeToStringMap.Add(222, "DeckSetData");
		ClientRequestManager.s_typeToStringMap.Add(223, "AckCardSeen");
		ClientRequestManager.s_typeToStringMap.Add(225, "OpenBooster");
		ClientRequestManager.s_typeToStringMap.Add(226, "BoosterContent");
		ClientRequestManager.s_typeToStringMap.Add(227, "ProfileLastLogin");
		ClientRequestManager.s_typeToStringMap.Add(228, "ClientTracking");
		ClientRequestManager.s_typeToStringMap.Add(229, "unused");
		ClientRequestManager.s_typeToStringMap.Add(230, "SetProgress");
		ClientRequestManager.s_typeToStringMap.Add(231, "ProfileDeckLimit");
		ClientRequestManager.s_typeToStringMap.Add(232, "MedalInfo");
		ClientRequestManager.s_typeToStringMap.Add(233, "ProfileProgress");
		ClientRequestManager.s_typeToStringMap.Add(234, "MedalHistory");
		ClientRequestManager.s_typeToStringMap.Add(235, "DraftBegin");
		ClientRequestManager.s_typeToStringMap.Add(236, "CardBacks");
		ClientRequestManager.s_typeToStringMap.Add(237, "GetBattlePayConfig");
		ClientRequestManager.s_typeToStringMap.Add(238, "BattlePayConfigResponse");
		ClientRequestManager.s_typeToStringMap.Add(239, "SetOptions");
		ClientRequestManager.s_typeToStringMap.Add(240, "GetOptions");
		ClientRequestManager.s_typeToStringMap.Add(241, "ClientOptions");
		ClientRequestManager.s_typeToStringMap.Add(242, "DraftRetire");
		ClientRequestManager.s_typeToStringMap.Add(243, "AckAchieveProgress");
		ClientRequestManager.s_typeToStringMap.Add(244, "DraftGetChoicesAndContents");
		ClientRequestManager.s_typeToStringMap.Add(245, "DraftMakePick");
		ClientRequestManager.s_typeToStringMap.Add(246, "DraftBeginning");
		ClientRequestManager.s_typeToStringMap.Add(247, "DraftRetired");
		ClientRequestManager.s_typeToStringMap.Add(248, "DraftChoicesAndContents");
		ClientRequestManager.s_typeToStringMap.Add(249, "DraftChosen");
		ClientRequestManager.s_typeToStringMap.Add(250, "GetPurchaseMethod");
		ClientRequestManager.s_typeToStringMap.Add(251, "DraftError");
		ClientRequestManager.s_typeToStringMap.Add(254, "NOP");
		ClientRequestManager.s_typeToStringMap.Add(255, "GetBattlePayStatus");
		ClientRequestManager.s_typeToStringMap.Add(256, "PurchaseResponse");
		ClientRequestManager.s_typeToStringMap.Add(257, "BuySellCard");
		ClientRequestManager.s_typeToStringMap.Add(258, "BoughtSoldCard");
		ClientRequestManager.s_typeToStringMap.Add(259, "DevBnetIdentify");
		ClientRequestManager.s_typeToStringMap.Add(260, "CardValues");
		ClientRequestManager.s_typeToStringMap.Add(261, "GuardianTrack");
		ClientRequestManager.s_typeToStringMap.Add(262, "ArcaneDustBalance");
		ClientRequestManager.s_typeToStringMap.Add(263, "CloseCardMarket");
		ClientRequestManager.s_typeToStringMap.Add(264, "GuardianVars");
		ClientRequestManager.s_typeToStringMap.Add(265, "BattlePayStatusResponse");
		ClientRequestManager.s_typeToStringMap.Add(266, "Error37 (deprecated)");
		ClientRequestManager.s_typeToStringMap.Add(267, "CheckAccountLicenses");
		ClientRequestManager.s_typeToStringMap.Add(268, "MassDisenchant");
		ClientRequestManager.s_typeToStringMap.Add(269, "MassDisenchantResponse");
		ClientRequestManager.s_typeToStringMap.Add(270, "PlayerRecords");
		ClientRequestManager.s_typeToStringMap.Add(271, "RewardProgress");
		ClientRequestManager.s_typeToStringMap.Add(272, "PurchaseMethod");
		ClientRequestManager.s_typeToStringMap.Add(273, "DoPurchase");
		ClientRequestManager.s_typeToStringMap.Add(274, "CancelPurchase");
		ClientRequestManager.s_typeToStringMap.Add(275, "CancelPurchaseResponse");
		ClientRequestManager.s_typeToStringMap.Add(276, "CheckGameLicenses");
		ClientRequestManager.s_typeToStringMap.Add(277, "CheckLicensesResponse");
		ClientRequestManager.s_typeToStringMap.Add(278, "GoldBalance");
		ClientRequestManager.s_typeToStringMap.Add(279, "PurchaseWithGold");
		ClientRequestManager.s_typeToStringMap.Add(280, "PurchaseWithGoldResponse");
		ClientRequestManager.s_typeToStringMap.Add(281, "CancelQuest");
		ClientRequestManager.s_typeToStringMap.Add(282, "CancelQuestResponse");
		ClientRequestManager.s_typeToStringMap.Add(283, "HeroXP");
		ClientRequestManager.s_typeToStringMap.Add(284, "ValidateAchieve");
		ClientRequestManager.s_typeToStringMap.Add(285, "ValidateAchieveResponse");
		ClientRequestManager.s_typeToStringMap.Add(286, "PlayQueue");
		ClientRequestManager.s_typeToStringMap.Add(287, "DraftAckRewards");
		ClientRequestManager.s_typeToStringMap.Add(288, "DraftRewardsAcked");
		ClientRequestManager.s_typeToStringMap.Add(289, "Disconnected");
		ClientRequestManager.s_typeToStringMap.Add(290, "Deadend");
		ClientRequestManager.s_typeToStringMap.Add(291, "SetCardBack");
		ClientRequestManager.s_typeToStringMap.Add(292, "SetCardBackResponse");
		ClientRequestManager.s_typeToStringMap.Add(293, "SubmitThirdPartyReceipt");
		ClientRequestManager.s_typeToStringMap.Add(294, "GetThirdPartyPurchaseStatus");
		ClientRequestManager.s_typeToStringMap.Add(295, "ThirdPartyPurchaseStatusResponse");
		ClientRequestManager.s_typeToStringMap.Add(296, "SetProgressResponse");
		ClientRequestManager.s_typeToStringMap.Add(297, "CheckAccountLicenseAchieve");
		ClientRequestManager.s_typeToStringMap.Add(298, "TriggerPlayedNearbyPlayerOnSubnet");
		ClientRequestManager.s_typeToStringMap.Add(299, "EventResponse");
		ClientRequestManager.s_typeToStringMap.Add(300, "MassiveLoginReply");
		ClientRequestManager.s_typeToStringMap.Add(301, "(used in Console.proto)");
		ClientRequestManager.s_typeToStringMap.Add(302, "(used in Console.proto)");
		ClientRequestManager.s_typeToStringMap.Add(303, "GetAssetsVersion");
		ClientRequestManager.s_typeToStringMap.Add(304, "AssetsVersionResponse");
		ClientRequestManager.s_typeToStringMap.Add(305, "GetAdventureProgress");
		ClientRequestManager.s_typeToStringMap.Add(306, "AdventureProgressResponse");
		ClientRequestManager.s_typeToStringMap.Add(307, "UpdateLoginComplete");
		ClientRequestManager.s_typeToStringMap.Add(308, "AckWingProgress");
		ClientRequestManager.s_typeToStringMap.Add(309, "SetPlayerAdventureProgress");
		ClientRequestManager.s_typeToStringMap.Add(310, "SetAdventureOptions");
		ClientRequestManager.s_typeToStringMap.Add(311, "AccountLicenseAchieveResponse");
		ClientRequestManager.s_typeToStringMap.Add(312, "StartThirdPartyPurchase");
		ClientRequestManager.s_typeToStringMap.Add(314, "Subscribe");
		ClientRequestManager.s_typeToStringMap.Add(315, "SubscribeResponse");
		ClientRequestManager.s_typeToStringMap.Add(316, "TavernBrawlInfo");
		ClientRequestManager.s_typeToStringMap.Add(317, "TavernBrawlPlayerRecordResponse");
		ClientRequestManager.s_typeToStringMap.Add(318, "FavoriteHeroesResponse");
		ClientRequestManager.s_typeToStringMap.Add(319, "SetFavoriteHero");
		ClientRequestManager.s_typeToStringMap.Add(320, "SetFavoriteHeroResponse");
		ClientRequestManager.s_typeToStringMap.Add(321, "GetAssetRequest");
		ClientRequestManager.s_typeToStringMap.Add(322, "GetAssetResponse");
		ClientRequestManager.s_typeToStringMap.Add(323, "DebugCommandRequest");
		ClientRequestManager.s_typeToStringMap.Add(324, "DebugCommandResponse");
		ClientRequestManager.s_typeToStringMap.Add(325, "AccountLicensesInfoResponse");
		ClientRequestManager.s_typeToStringMap.Add(326, "GenericResponse");
		ClientRequestManager.s_typeToStringMap.Add(327, "GenericRequestList");
		ClientRequestManager.s_typeToStringMap.Add(328, "ClientRequestResponse");
		ClientRequestManager.s_typeToStringMap.Add(381, "Coins");
		ClientRequestManager.s_typeToStringMap.Add(382, "SetFavoriteCoin");
		ClientRequestManager.s_typeToStringMap.Add(383, "SetFavoriteCoinResponse");
		ClientRequestManager.s_typeToStringMap.Add(384, "CoinUpdate");
	}

	// Token: 0x060053D4 RID: 21460 RVA: 0x001B6538 File Offset: 0x001B4738
	private string GetTypeName(int type)
	{
		string text = type.ToString();
		string str;
		if (ClientRequestManager.s_typeToStringMap.Count > 0 && ClientRequestManager.s_typeToStringMap.TryGetValue(type, out str))
		{
			return str + ":" + text;
		}
		return text;
	}

	// Token: 0x060053D5 RID: 21461 RVA: 0x001B6578 File Offset: 0x001B4778
	[Conditional("CLIENTREQUESTMANAGER_LOGGING")]
	private void LOG_DEBUG(string format, params object[] args)
	{
		string str = GeneralUtils.SafeFormat(format, args);
		global::Log.ClientRequestManager.Print("D " + str, Array.Empty<object>());
	}

	// Token: 0x060053D6 RID: 21462 RVA: 0x001B65A8 File Offset: 0x001B47A8
	[Conditional("CLIENTREQUESTMANAGER_LOGGING")]
	private void LOG_WARN(string format, params object[] args)
	{
		string str = GeneralUtils.SafeFormat(format, args);
		global::Log.ClientRequestManager.Print("W " + str, Array.Empty<object>());
	}

	// Token: 0x060053D7 RID: 21463 RVA: 0x001B65D8 File Offset: 0x001B47D8
	[Conditional("CLIENTREQUESTMANAGER_LOGGING")]
	private void LOG_ERROR(string format, params object[] args)
	{
		string str = GeneralUtils.SafeFormat(format, args);
		global::Log.ClientRequestManager.Print("E " + str, Array.Empty<object>());
	}

	// Token: 0x060053D8 RID: 21464 RVA: 0x001B6607 File Offset: 0x001B4807
	private bool HasPendingDeliveryPacketsImpl()
	{
		return this.m_state.m_responsesPendingDelivery.Count > 0;
	}

	// Token: 0x060053D9 RID: 21465 RVA: 0x001B661C File Offset: 0x001B481C
	private int PeekNetClientRequestTypeImpl()
	{
		if (this.m_state.m_responsesPendingDelivery.Count == 0)
		{
			return 0;
		}
		return this.m_state.m_responsesPendingDelivery.Peek().Response.Type;
	}

	// Token: 0x060053DA RID: 21466 RVA: 0x001B664C File Offset: 0x001B484C
	private ResponseWithRequest GetNextClientRequestImpl()
	{
		if (this.m_state.m_responsesPendingDelivery.Count == 0)
		{
			return null;
		}
		return this.m_state.m_responsesPendingDelivery.Peek();
	}

	// Token: 0x060053DB RID: 21467 RVA: 0x001B6672 File Offset: 0x001B4872
	private void DropNextClientRequestImpl()
	{
		if (this.m_state.m_responsesPendingDelivery.Count == 0)
		{
			return;
		}
		this.m_state.m_responsesPendingDelivery.Dequeue();
	}

	// Token: 0x060053DC RID: 21468 RVA: 0x001B6698 File Offset: 0x001B4898
	private bool HasErrorsImpl()
	{
		return this.m_state.m_receivedErrorSignal;
	}

	// Token: 0x060053DE RID: 21470 RVA: 0x001B66E4 File Offset: 0x001B48E4
	private void UpdateImpl()
	{
		if (!this.m_state.m_loginCompleteNotificationReceived)
		{
			return;
		}
		if (!this.m_hasSubscribedToUtilClient)
		{
			return;
		}
		ClientRequestManager.SystemChannel system = this.m_state.m_systems.Systems[UtilSystemId.CLIENT];
		if (!this.UpdateStateSubscribeImpl(system))
		{
			return;
		}
		this.ProcessClientRequests(system);
		foreach (KeyValuePair<UtilSystemId, ClientRequestManager.SystemChannel> keyValuePair in this.m_state.m_systems.Systems)
		{
			if (keyValuePair.Key != UtilSystemId.CLIENT && this.UpdateStateSubscribeImpl(keyValuePair.Value))
			{
				this.ProcessClientRequests(keyValuePair.Value);
			}
		}
	}

	// Token: 0x060053DF RID: 21471 RVA: 0x001B67A0 File Offset: 0x001B49A0
	private bool SendClientRequestImpl(int type, IProtoBuf body, ClientRequestManager.ClientRequestConfig clientRequestConfig, RequestPhase requestPhase, int subID)
	{
		if (type == 0)
		{
			return false;
		}
		if (requestPhase < RequestPhase.STARTUP || requestPhase > RequestPhase.RUNNING)
		{
			return false;
		}
		ClientRequestManager.ClientRequestConfig clientRequestConfig2 = (clientRequestConfig == null) ? this.m_defaultConfig : clientRequestConfig;
		UtilSystemId requestedSystem = clientRequestConfig2.RequestedSystem;
		ClientRequestManager.SystemChannel orCreateSystem = this.GetOrCreateSystem(requestedSystem);
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
		ClientRequestManager.ClientRequestType clientRequestType = new ClientRequestManager.ClientRequestType(orCreateSystem);
		clientRequestType.Type = type;
		clientRequestType.ShouldRetryOnError = clientRequestConfig2.ShouldRetryOnError;
		clientRequestType.ShouldRetryOnUnhandled = clientRequestConfig2.ShouldRetryOnUnhandled;
		clientRequestType.SubID = subID;
		clientRequestType.Body = ProtobufUtil.ToByteArray(body);
		clientRequestType.Phase = requestPhase;
		clientRequestType.SendCount = 0U;
		clientRequestType.RequestNotHandledCount = 0U;
		clientRequestType.RequestId = this.GetNextRequestId();
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

	// Token: 0x060053E0 RID: 21472 RVA: 0x001B6890 File Offset: 0x001B4A90
	private ClientRequestManager.SystemChannel GetOrCreateSystem(UtilSystemId systemId)
	{
		ClientRequestManager.SystemChannel systemChannel = null;
		if (this.m_state.m_systems.Systems.TryGetValue(systemId, out systemChannel))
		{
			return systemChannel;
		}
		systemChannel = new ClientRequestManager.SystemChannel();
		systemChannel.SystemId = systemId;
		this.m_state.m_systems.Systems[systemId] = systemChannel;
		if (systemId == UtilSystemId.CLIENT)
		{
			this.m_hasSubscribedToUtilClient = true;
		}
		return systemChannel;
	}

	// Token: 0x060053E1 RID: 21473 RVA: 0x001B68EA File Offset: 0x001B4AEA
	public void EnsureSubscribedToImpl(UtilSystemId systemId)
	{
		this.GetOrCreateSystem(systemId);
	}

	// Token: 0x060053E2 RID: 21474 RVA: 0x001B68F4 File Offset: 0x001B4AF4
	private uint GenerateContextId()
	{
		uint num = this.m_nextContexId + 1U;
		this.m_nextContexId = num;
		return num;
	}

	// Token: 0x060053E3 RID: 21475 RVA: 0x001B6914 File Offset: 0x001B4B14
	private void NotifyResponseReceivedImpl(PegasusPacket packet)
	{
		uint context = (uint)packet.Context;
		ClientRequestManager.ClientRequestType clientRequest = this.GetClientRequest(context, "received_response", true);
		if (clientRequest == null)
		{
			if (packet.Context != 0 && this.GetDroppedRequest(context, "received_response", true))
			{
				return;
			}
			this.m_state.m_responsesPendingDelivery.Enqueue(new ResponseWithRequest(packet));
			return;
		}
		else
		{
			int type = packet.Type;
			if (type == 315)
			{
				this.ProcessSubscribeResponse(packet, clientRequest);
				return;
			}
			if (type != 328)
			{
				this.ProcessResponse(packet, clientRequest);
				return;
			}
			this.ProcessClientRequestResponse(packet, clientRequest);
			return;
		}
	}

	// Token: 0x060053E4 RID: 21476 RVA: 0x001B699B File Offset: 0x001B4B9B
	private void NotifyStartupSequenceCompleteImpl()
	{
		this.m_state.m_runningPhaseEnabled = true;
	}

	// Token: 0x060053E5 RID: 21477 RVA: 0x001B69A9 File Offset: 0x001B4BA9
	private void NotifyLoginSequenceCompletedImpl()
	{
		this.m_state.m_loginCompleteNotificationReceived = true;
	}

	// Token: 0x060053E6 RID: 21478 RVA: 0x001B69B8 File Offset: 0x001B4BB8
	private uint SendToUtil(ClientRequestManager.ClientRequestType request)
	{
		uint num = this.GenerateContextId();
		ulong route = request.System.Route;
		byte[] utilPacketBytes = request.GetUtilPacketBytes();
		BattleNet.SendUtilPacket(request.Type, request.System.SystemId, utilPacketBytes, utilPacketBytes.Length, request.SubID, (int)num, route);
		request.Context = num;
		request.SendTime = Time.realtimeSinceStartup;
		request.SendCount += 1U;
		request.RouteDispatchedTo = route;
		this.AddRequestToPendingResponse(request, "send_to_util");
		if (!request.IsSubscribeRequest)
		{
			request.Phase.ToString();
		}
		return num;
	}

	// Token: 0x060053E7 RID: 21479 RVA: 0x001B6A50 File Offset: 0x001B4C50
	private uint GetNextRequestId()
	{
		uint num = this.m_nextRequestId + 1U;
		this.m_nextRequestId = num;
		return num;
	}

	// Token: 0x060053E8 RID: 21480 RVA: 0x001B6A70 File Offset: 0x001B4C70
	private void SendSubscriptionRequest(ClientRequestManager.SystemChannel system)
	{
		UtilSystemId systemId = system.SystemId;
		if (system.Route == 0UL)
		{
			this.m_subscribePacket.FirstSubscribeForRoute = true;
		}
		else
		{
			this.m_subscribePacket.FirstSubscribeForRoute = false;
		}
		this.m_subscribePacket.FirstSubscribe = (system.SubscriptionStatus.LastSend == DateTime.MinValue);
		this.m_subscribePacket.UtilSystemId = (int)systemId;
		ClientRequestManager.ClientRequestType clientRequestType = new ClientRequestManager.ClientRequestType(system);
		clientRequestType.Type = 314;
		clientRequestType.SubID = 0;
		clientRequestType.Body = ProtobufUtil.ToByteArray(this.m_subscribePacket);
		clientRequestType.RequestId = this.GetNextRequestId();
		clientRequestType.IsSubscribeRequest = true;
		system.SubscriptionStatus.CurrentState = ClientRequestManager.SubscriptionStatusType.State.PENDING_RESPONSE;
		system.SubscriptionStatus.LastSend = DateTime.Now;
		system.SubscriptionStatus.ContexId = this.SendToUtil(clientRequestType);
		system.SubscribeAttempt += 1U;
		this.m_state.m_subscribePacketsSent += 1U;
	}

	// Token: 0x060053E9 RID: 21481 RVA: 0x001B6B5E File Offset: 0x001B4D5E
	private void ScheduleResubscribeWithNewRoute(ClientRequestManager.SystemChannel system)
	{
		system.Route = 0UL;
		system.SubscriptionStatus.CurrentState = ClientRequestManager.SubscriptionStatusType.State.PENDING_SEND;
	}

	// Token: 0x060053EA RID: 21482 RVA: 0x001B6B74 File Offset: 0x001B4D74
	private void TerminateImpl()
	{
		Unsubscribe packet = new Unsubscribe();
		foreach (KeyValuePair<UtilSystemId, ClientRequestManager.SystemChannel> keyValuePair in this.m_state.m_systems.Systems)
		{
			ClientRequestManager.SystemChannel value = keyValuePair.Value;
			Network network;
			if (value.SubscriptionStatus.CurrentState == ClientRequestManager.SubscriptionStatusType.State.SUBSCRIBED && value.Route != 0UL && HearthstoneServices.TryGet<Network>(out network))
			{
				network.SendUnsubcribeRequest(packet, value.SystemId);
			}
		}
	}

	// Token: 0x060053EB RID: 21483 RVA: 0x001B6C08 File Offset: 0x001B4E08
	private bool UpdateStateSubscribeImpl(ClientRequestManager.SystemChannel system)
	{
		switch (system.SubscriptionStatus.CurrentState)
		{
		case ClientRequestManager.SubscriptionStatusType.State.PENDING_SEND:
			return this.ProcessSubscribeStatePendingSend(system);
		case ClientRequestManager.SubscriptionStatusType.State.PENDING_RESPONSE:
			return this.ProcessSubscribeStatePendingResponse(system);
		case ClientRequestManager.SubscriptionStatusType.State.SUBSCRIBED:
			return this.ProcessSubscribeStateSubscribed(system);
		default:
			return system.SubscriptionStatus.CurrentState == ClientRequestManager.SubscriptionStatusType.State.SUBSCRIBED;
		}
	}

	// Token: 0x060053EC RID: 21484 RVA: 0x001B6C5C File Offset: 0x001B4E5C
	private bool ProcessSubscribeStatePendingSend(ClientRequestManager.SystemChannel system)
	{
		if ((DateTime.Now - system.SubscriptionStatus.LastSend).TotalSeconds > system.PendingSubscribeTimeout)
		{
			this.SendSubscriptionRequest(system);
		}
		return system.Route > 0UL;
	}

	// Token: 0x060053ED RID: 21485 RVA: 0x001B6CA4 File Offset: 0x001B4EA4
	private bool ProcessSubscribeStatePendingResponse(ClientRequestManager.SystemChannel system)
	{
		if ((DateTime.Now - system.SubscriptionStatus.LastSend).TotalSeconds > system.PendingSubscribeTimeout)
		{
			this.ScheduleResubscribeWithNewRoute(system);
		}
		return system.Route > 0UL;
	}

	// Token: 0x060053EE RID: 21486 RVA: 0x001B6CEC File Offset: 0x001B4EEC
	private int CountPendingResponsesForSystemId(ClientRequestManager.SystemChannel system)
	{
		int num = 0;
		foreach (KeyValuePair<uint, ClientRequestManager.ClientRequestType> keyValuePair in this.m_state.m_activePendingResponseMap)
		{
			if (keyValuePair.Value.System.SystemId == system.SystemId)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060053EF RID: 21487 RVA: 0x001B6D60 File Offset: 0x001B4F60
	private bool ProcessSubscribeStateSubscribed(ClientRequestManager.SystemChannel system)
	{
		if ((ulong)(Time.realtimeSinceStartup - system.SubscriptionStatus.SubscribedTime) < system.KeepAliveSecs)
		{
			return true;
		}
		if (this.CountPendingResponsesForSystemId(system) > 0)
		{
			return true;
		}
		if (system.KeepAliveSecs > 0UL)
		{
			system.SubscriptionStatus.CurrentState = ClientRequestManager.SubscriptionStatusType.State.PENDING_SEND;
		}
		return true;
	}

	// Token: 0x060053F0 RID: 21488 RVA: 0x001B6DAC File Offset: 0x001B4FAC
	private void ProcessSubscribeResponse(PegasusPacket packet, ClientRequestManager.ClientRequestType request)
	{
		if (!(packet.Body is SubscribeResponse))
		{
			return;
		}
		ClientRequestManager.SystemChannel system = request.System;
		UtilSystemId systemId = system.SystemId;
		SubscribeResponse subscribeResponse = (SubscribeResponse)packet.Body;
		if (subscribeResponse.Result == SubscribeResponse.ResponseResult.FAILED_UNAVAILABLE)
		{
			this.ScheduleResubscribeWithNewRoute(system);
			return;
		}
		system.SubscriptionStatus.CurrentState = ClientRequestManager.SubscriptionStatusType.State.SUBSCRIBED;
		system.SubscriptionStatus.SubscribedTime = Time.realtimeSinceStartup;
		system.Route = subscribeResponse.Route;
		system.CurrentPhase = RequestPhase.STARTUP;
		system.SubscribeAttempt = 0U;
		system.KeepAliveSecs = subscribeResponse.KeepAliveSecs;
		system.MaxResubscribeAttempts = subscribeResponse.MaxResubscribeAttempts;
		system.PendingResponseTimeout = subscribeResponse.PendingResponseTimeout;
		system.PendingSubscribeTimeout = subscribeResponse.PendingSubscribeTimeout;
		PegasusPacket request2 = new PegasusPacket(request.Type, packet.Context, request.Body);
		this.m_state.m_responsesPendingDelivery.Enqueue(new ResponseWithRequest(packet, request2));
		system.m_subscribePacketsReceived += 1U;
	}

	// Token: 0x060053F1 RID: 21489 RVA: 0x001B6E98 File Offset: 0x001B5098
	private void ProcessClientRequests(ClientRequestManager.SystemChannel system)
	{
		ClientRequestManager.PendingMapType pendingMapType = (system.CurrentPhase == RequestPhase.STARTUP) ? system.Phases.StartUp : system.Phases.Running;
		foreach (KeyValuePair<uint, ClientRequestManager.ClientRequestType> keyValuePair in this.m_state.m_activePendingResponseMap)
		{
			ClientRequestManager.ClientRequestType value = keyValuePair.Value;
			if (!value.IsSubscribeRequest && value.System != null && value.System.SystemId == system.SystemId && system.PendingResponseTimeout != 0UL && Time.realtimeSinceStartup - value.SendTime >= system.PendingResponseTimeout)
			{
				this.ScheduleResubscribeWithNewRoute(system);
				return;
			}
		}
		if (system.Route == 0UL)
		{
			return;
		}
		bool flag = pendingMapType.PendingSend.Count > 0;
		while (pendingMapType.PendingSend.Count > 0)
		{
			ClientRequestManager.ClientRequestType request = pendingMapType.PendingSend.Dequeue();
			this.SendToUtil(request);
		}
		if (flag)
		{
			return;
		}
		if (system.CurrentPhase == RequestPhase.STARTUP && this.m_state.m_runningPhaseEnabled)
		{
			system.CurrentPhase = RequestPhase.RUNNING;
		}
	}

	// Token: 0x060053F2 RID: 21490 RVA: 0x001B6FC0 File Offset: 0x001B51C0
	private void ProcessResponse(PegasusPacket packet, ClientRequestManager.ClientRequestType clientRequest)
	{
		if (packet.Type != 254)
		{
			PegasusPacket request = new PegasusPacket(clientRequest.Type, packet.Context, clientRequest.Body);
			this.m_state.m_responsesPendingDelivery.Enqueue(new ResponseWithRequest(packet, request));
		}
	}

	// Token: 0x060053F3 RID: 21491 RVA: 0x001B700C File Offset: 0x001B520C
	private ClientRequestManager.ClientRequestType GetClientRequest(uint contextId, string reason, bool removeIfFound = true)
	{
		if (contextId == 0U)
		{
			return null;
		}
		ClientRequestManager.ClientRequestType result;
		if (!this.m_state.m_activePendingResponseMap.TryGetValue(contextId, out result))
		{
			if (this.GetDroppedRequest(contextId, "get_client_request", false))
			{
				this.GetPendingSendRequest(contextId, "get_client_request", false);
			}
			return null;
		}
		if (removeIfFound)
		{
			this.m_state.m_activePendingResponseMap.Remove(contextId);
		}
		return result;
	}

	// Token: 0x060053F4 RID: 21492 RVA: 0x001B7068 File Offset: 0x001B5268
	private void AddRequestToPendingSendQueue(ClientRequestManager.ClientRequestType clientRequest, string reason)
	{
		if (clientRequest.Phase == RequestPhase.STARTUP)
		{
			clientRequest.System.Phases.StartUp.PendingSend.Enqueue(clientRequest);
			int count = clientRequest.System.Phases.StartUp.PendingSend.Count;
			return;
		}
		clientRequest.System.Phases.Running.PendingSend.Enqueue(clientRequest);
		int count2 = clientRequest.System.Phases.Running.PendingSend.Count;
	}

	// Token: 0x060053F5 RID: 21493 RVA: 0x001B70EA File Offset: 0x001B52EA
	private void AddRequestToPendingResponse(ClientRequestManager.ClientRequestType clientRequest, string reason)
	{
		if (!this.m_state.m_activePendingResponseMap.ContainsKey(clientRequest.Context))
		{
			this.m_state.m_activePendingResponseMap.Add(clientRequest.Context, clientRequest);
		}
	}

	// Token: 0x060053F6 RID: 21494 RVA: 0x001B711B File Offset: 0x001B531B
	private bool GetDroppedRequest(uint contextId, string reason, bool removeIfFound = true)
	{
		if (this.m_state.m_ignorePendingResponseMap.Contains(contextId) && removeIfFound)
		{
			this.m_state.m_ignorePendingResponseMap.Remove(contextId);
			return true;
		}
		return false;
	}

	// Token: 0x060053F7 RID: 21495 RVA: 0x001B7148 File Offset: 0x001B5348
	private ClientRequestManager.ClientRequestType GetPendingSendRequestForPhase(uint contextId, bool removeIfFound, ClientRequestManager.PendingMapType pendingMap)
	{
		ClientRequestManager.ClientRequestType clientRequestType = null;
		Queue<ClientRequestManager.ClientRequestType> queue = new Queue<ClientRequestManager.ClientRequestType>();
		foreach (ClientRequestManager.ClientRequestType clientRequestType2 in pendingMap.PendingSend)
		{
			if (clientRequestType == null && clientRequestType2.Context == contextId)
			{
				clientRequestType = clientRequestType2;
				if (!removeIfFound)
				{
					queue.Enqueue(clientRequestType2);
				}
			}
			else
			{
				queue.Enqueue(clientRequestType2);
			}
		}
		pendingMap.PendingSend = queue;
		return clientRequestType;
	}

	// Token: 0x060053F8 RID: 21496 RVA: 0x001B71C8 File Offset: 0x001B53C8
	private ClientRequestManager.ClientRequestType GetPendingSendRequest(uint contextId, string reason, bool removeIfFound = true)
	{
		ClientRequestManager.ClientRequestType clientRequestType = null;
		foreach (KeyValuePair<UtilSystemId, ClientRequestManager.SystemChannel> keyValuePair in this.m_state.m_systems.Systems)
		{
			ClientRequestManager.SystemChannel value = keyValuePair.Value;
			clientRequestType = this.GetPendingSendRequestForPhase(contextId, removeIfFound, value.Phases.Running);
			if (clientRequestType != null)
			{
				break;
			}
			clientRequestType = this.GetPendingSendRequestForPhase(contextId, removeIfFound, value.Phases.StartUp);
		}
		return clientRequestType;
	}

	// Token: 0x04004A2F RID: 18991
	private static global::Map<int, string> s_typeToStringMap = new global::Map<int, string>();

	// Token: 0x04004A30 RID: 18992
	private readonly ClientRequestManager.ClientRequestConfig m_defaultConfig = new ClientRequestManager.ClientRequestConfig
	{
		ShouldRetryOnError = true,
		ShouldRetryOnUnhandled = true,
		RequestedSystem = UtilSystemId.CLIENT
	};

	// Token: 0x04004A31 RID: 18993
	public uint m_nextContexId;

	// Token: 0x04004A32 RID: 18994
	public uint m_nextRequestId;

	// Token: 0x04004A33 RID: 18995
	private ClientRequestManager.InternalState m_state = new ClientRequestManager.InternalState();

	// Token: 0x04004A34 RID: 18996
	private Subscribe m_subscribePacket = new Subscribe();

	// Token: 0x04004A35 RID: 18997
	private bool m_hasSubscribedToUtilClient;

	// Token: 0x0200204B RID: 8267
	public class ClientRequestConfig
	{
		// Token: 0x17002688 RID: 9864
		// (get) Token: 0x06011CD1 RID: 72913 RVA: 0x004F9892 File Offset: 0x004F7A92
		// (set) Token: 0x06011CD2 RID: 72914 RVA: 0x004F989A File Offset: 0x004F7A9A
		public bool ShouldRetryOnError { get; set; }

		// Token: 0x17002689 RID: 9865
		// (get) Token: 0x06011CD3 RID: 72915 RVA: 0x004F98A3 File Offset: 0x004F7AA3
		// (set) Token: 0x06011CD4 RID: 72916 RVA: 0x004F98AB File Offset: 0x004F7AAB
		public bool ShouldRetryOnUnhandled { get; set; }

		// Token: 0x1700268A RID: 9866
		// (get) Token: 0x06011CD5 RID: 72917 RVA: 0x004F98B4 File Offset: 0x004F7AB4
		// (set) Token: 0x06011CD6 RID: 72918 RVA: 0x004F98BC File Offset: 0x004F7ABC
		public UtilSystemId RequestedSystem { get; set; }
	}

	// Token: 0x0200204C RID: 8268
	private class ClientRequestType
	{
		// Token: 0x06011CD8 RID: 72920 RVA: 0x004F98C5 File Offset: 0x004F7AC5
		public ClientRequestType(ClientRequestManager.SystemChannel system)
		{
			this.System = system;
		}

		// Token: 0x06011CD9 RID: 72921 RVA: 0x004F98D4 File Offset: 0x004F7AD4
		public byte[] GetUtilPacketBytes()
		{
			RpcHeader rpcHeader = new RpcHeader();
			rpcHeader.Type = (ulong)((long)this.Type);
			if (this.SendCount > 0U)
			{
				rpcHeader.RetryCount = (ulong)this.SendCount;
			}
			if (this.RequestNotHandledCount > 0U)
			{
				rpcHeader.RequestNotHandledCount = (ulong)this.RequestNotHandledCount;
			}
			RpcMessage rpcMessage = new RpcMessage();
			rpcMessage.RpcHeader = rpcHeader;
			if (this.Body != null && this.Body.Length != 0)
			{
				rpcMessage.MessageBody = this.Body;
			}
			return ProtobufUtil.ToByteArray(rpcMessage);
		}

		// Token: 0x0400DCA2 RID: 56482
		public int Type;

		// Token: 0x0400DCA3 RID: 56483
		public int SubID;

		// Token: 0x0400DCA4 RID: 56484
		public byte[] Body;

		// Token: 0x0400DCA5 RID: 56485
		public uint Context;

		// Token: 0x0400DCA6 RID: 56486
		public RequestPhase Phase;

		// Token: 0x0400DCA7 RID: 56487
		public uint SendCount;

		// Token: 0x0400DCA8 RID: 56488
		public uint RequestNotHandledCount;

		// Token: 0x0400DCA9 RID: 56489
		public float SendTime;

		// Token: 0x0400DCAA RID: 56490
		public uint RequestId;

		// Token: 0x0400DCAB RID: 56491
		public bool IsSubscribeRequest;

		// Token: 0x0400DCAC RID: 56492
		public ClientRequestManager.SystemChannel System;

		// Token: 0x0400DCAD RID: 56493
		public bool ShouldRetryOnError;

		// Token: 0x0400DCAE RID: 56494
		public bool ShouldRetryOnUnhandled;

		// Token: 0x0400DCAF RID: 56495
		public ulong RouteDispatchedTo;
	}

	// Token: 0x0200204D RID: 8269
	private class SubscriptionStatusType
	{
		// Token: 0x0400DCB0 RID: 56496
		public ClientRequestManager.SubscriptionStatusType.State CurrentState;

		// Token: 0x0400DCB1 RID: 56497
		public DateTime LastSend = DateTime.MinValue;

		// Token: 0x0400DCB2 RID: 56498
		public float SubscribedTime;

		// Token: 0x0400DCB3 RID: 56499
		public uint ContexId;

		// Token: 0x02002984 RID: 10628
		public enum State
		{
			// Token: 0x0400FCF3 RID: 64755
			PENDING_SEND,
			// Token: 0x0400FCF4 RID: 64756
			PENDING_RESPONSE,
			// Token: 0x0400FCF5 RID: 64757
			SUBSCRIBED
		}
	}

	// Token: 0x0200204E RID: 8270
	private class PendingMapType
	{
		// Token: 0x0400DCB4 RID: 56500
		public Queue<ClientRequestManager.ClientRequestType> PendingSend = new Queue<ClientRequestManager.ClientRequestType>();
	}

	// Token: 0x0200204F RID: 8271
	private class PhaseMapType
	{
		// Token: 0x0400DCB5 RID: 56501
		public ClientRequestManager.PendingMapType StartUp = new ClientRequestManager.PendingMapType();

		// Token: 0x0400DCB6 RID: 56502
		public ClientRequestManager.PendingMapType Running = new ClientRequestManager.PendingMapType();
	}

	// Token: 0x02002050 RID: 8272
	private class SystemChannel
	{
		// Token: 0x0400DCB7 RID: 56503
		public ClientRequestManager.PhaseMapType Phases = new ClientRequestManager.PhaseMapType();

		// Token: 0x0400DCB8 RID: 56504
		public ClientRequestManager.SubscriptionStatusType SubscriptionStatus = new ClientRequestManager.SubscriptionStatusType();

		// Token: 0x0400DCB9 RID: 56505
		public ulong Route;

		// Token: 0x0400DCBA RID: 56506
		public RequestPhase CurrentPhase;

		// Token: 0x0400DCBB RID: 56507
		public ulong KeepAliveSecs;

		// Token: 0x0400DCBC RID: 56508
		public ulong MaxResubscribeAttempts;

		// Token: 0x0400DCBD RID: 56509
		public ulong PendingResponseTimeout;

		// Token: 0x0400DCBE RID: 56510
		public ulong PendingSubscribeTimeout = 15UL;

		// Token: 0x0400DCBF RID: 56511
		public uint SubscribeAttempt;

		// Token: 0x0400DCC0 RID: 56512
		public bool WasEverInRunningPhase;

		// Token: 0x0400DCC1 RID: 56513
		public UtilSystemId SystemId;

		// Token: 0x0400DCC2 RID: 56514
		public uint m_subscribePacketsReceived;
	}

	// Token: 0x02002051 RID: 8273
	private class SystemMap
	{
		// Token: 0x0400DCC3 RID: 56515
		public global::Map<UtilSystemId, ClientRequestManager.SystemChannel> Systems = new global::Map<UtilSystemId, ClientRequestManager.SystemChannel>();
	}

	// Token: 0x02002052 RID: 8274
	private class InternalState
	{
		// Token: 0x0400DCC4 RID: 56516
		public Queue<ResponseWithRequest> m_responsesPendingDelivery = new Queue<ResponseWithRequest>();

		// Token: 0x0400DCC5 RID: 56517
		public ClientRequestManager.SystemMap m_systems = new ClientRequestManager.SystemMap();

		// Token: 0x0400DCC6 RID: 56518
		public uint m_subscribePacketsSent;

		// Token: 0x0400DCC7 RID: 56519
		public bool m_loginCompleteNotificationReceived;

		// Token: 0x0400DCC8 RID: 56520
		public global::Map<uint, ClientRequestManager.ClientRequestType> m_activePendingResponseMap = new global::Map<uint, ClientRequestManager.ClientRequestType>();

		// Token: 0x0400DCC9 RID: 56521
		public HashSet<uint> m_ignorePendingResponseMap = new HashSet<uint>();

		// Token: 0x0400DCCA RID: 56522
		public bool m_runningPhaseEnabled;

		// Token: 0x0400DCCB RID: 56523
		public bool m_receivedErrorSignal;
	}
}
