using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bgs;
using Blizzard.Proto;
using Blizzard.Telemetry;
using Blizzard.Telemetry.CRM;
using Blizzard.Telemetry.Standard.Network;
using Blizzard.Telemetry.WTCG.Client;
using Blizzard.Telemetry.WTCG.NGDP;
using UnityEngine;

namespace HearthstoneTelemetry
{
	public class TelemetryClient : ITelemetryClient
	{
		private class DeferredMessage
		{
			public Blizzard.Proto.IProtoBuf ProtoMessage { get; set; }

			public string PackageName { get; set; }

			public string MessageName { get; set; }

			public Type MessageType { get; set; }

			public byte[] Data { get; set; }

			public MessageOptions Options { get; set; }
		}

		private readonly List<DeferredMessage> m_deferredMessages = new List<DeferredMessage>();

		private readonly object m_deferredMessageLock = new object();

		private Service m_telemetryService;

		private bool m_disabled;

		private bool m_initialized;

		private Action m_updateHandler;

		private Blizzard.Telemetry.WTCG.Client.DeviceInfo m_deviceInfo;

		public void SendAccountHealUpResult(AccountHealUpResult.HealUpResult result, int errorCode)
		{
			AccountHealUpResult accountHealUpResult = new AccountHealUpResult();
			accountHealUpResult.Player = GetPlayer();
			accountHealUpResult.DeviceInfo = GetDeviceInfo();
			accountHealUpResult.Result = result;
			accountHealUpResult.ErrorCode = errorCode;
			EnqueueMessage(accountHealUpResult);
		}

		public void SendAppInitialized(string testType, float duration, string clientChangelist)
		{
			AppInitialized appInitialized = new AppInitialized();
			appInitialized.TestType = testType;
			appInitialized.Duration = duration;
			appInitialized.ClientChangelist = clientChangelist;
			EnqueueMessage(appInitialized);
		}

		public void SendAppPaused(bool pauseStatus, float pauseTime)
		{
			AppPaused appPaused = new AppPaused();
			appPaused.PauseStatus = pauseStatus;
			appPaused.PauseTime = pauseTime;
			EnqueueMessage(appPaused);
		}

		public void SendAppStart(string testType, float duration, string clientChangelist)
		{
			AppStart appStart = new AppStart();
			appStart.TestType = testType;
			appStart.Duration = duration;
			appStart.ClientChangelist = clientChangelist;
			EnqueueMessage(appStart);
		}

		public void SendAssetNotFound(string assetType, string assetGuid, string filePath, string legacyName)
		{
			AssetNotFound assetNotFound = new AssetNotFound();
			assetNotFound.DeviceInfo = GetDeviceInfo();
			assetNotFound.AssetType = assetType;
			assetNotFound.AssetGuid = assetGuid;
			assetNotFound.FilePath = filePath;
			assetNotFound.LegacyName = legacyName;
			EnqueueMessage(assetNotFound);
		}

		public void SendAssetOrphaned(string filePath, string handleOwner, string handleType)
		{
			AssetOrphaned assetOrphaned = new AssetOrphaned();
			assetOrphaned.DeviceInfo = GetDeviceInfo();
			assetOrphaned.FilePath = filePath;
			assetOrphaned.HandleOwner = handleOwner;
			assetOrphaned.HandleType = handleType;
			EnqueueMessage(assetOrphaned);
		}

		public void SendAttackInputMethod(long totalNumAttacks, long totalClickAttacks, int percentClickAttacks, long totalDragAttacks, int percentDragAttacks)
		{
			AttackInputMethod attackInputMethod = new AttackInputMethod();
			attackInputMethod.DeviceInfo = GetDeviceInfo();
			attackInputMethod.TotalNumAttacks = totalNumAttacks;
			attackInputMethod.TotalClickAttacks = totalClickAttacks;
			attackInputMethod.PercentClickAttacks = percentClickAttacks;
			attackInputMethod.TotalDragAttacks = totalDragAttacks;
			attackInputMethod.PercentDragAttacks = percentDragAttacks;
			EnqueueMessage(attackInputMethod);
		}

		public void SendAttributionContentUnlocked(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string contentId)
		{
			AttributionContentUnlocked attributionContentUnlocked = new AttributionContentUnlocked();
			attributionContentUnlocked.ApplicationId = applicationId;
			attributionContentUnlocked.DeviceType = deviceType;
			attributionContentUnlocked.FirstInstallDate = firstInstallDate;
			attributionContentUnlocked.BundleId = bundleId;
			attributionContentUnlocked.ContentId = contentId;
			EnqueueMessage(attributionContentUnlocked);
		}

		public void SendAttributionFirstLogin(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier)
		{
			AttributionFirstLogin attributionFirstLogin = new AttributionFirstLogin();
			attributionFirstLogin.ApplicationId = applicationId;
			attributionFirstLogin.DeviceType = deviceType;
			attributionFirstLogin.FirstInstallDate = firstInstallDate;
			attributionFirstLogin.BundleId = bundleId;
			attributionFirstLogin.Identifier = identifier;
			EnqueueMessage(attributionFirstLogin);
		}

		public void SendAttributionGameRoundEnd(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string gameMode, string result, FormatType formatType)
		{
			AttributionGameRoundEnd attributionGameRoundEnd = new AttributionGameRoundEnd();
			attributionGameRoundEnd.ApplicationId = applicationId;
			attributionGameRoundEnd.DeviceType = deviceType;
			attributionGameRoundEnd.FirstInstallDate = firstInstallDate;
			attributionGameRoundEnd.BundleId = bundleId;
			attributionGameRoundEnd.GameMode = gameMode;
			attributionGameRoundEnd.Result = result;
			attributionGameRoundEnd.FormatType = formatType;
			EnqueueMessage(attributionGameRoundEnd);
		}

		public void SendAttributionGameRoundStart(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string gameMode, FormatType formatType)
		{
			AttributionGameRoundStart attributionGameRoundStart = new AttributionGameRoundStart();
			attributionGameRoundStart.ApplicationId = applicationId;
			attributionGameRoundStart.DeviceType = deviceType;
			attributionGameRoundStart.FirstInstallDate = firstInstallDate;
			attributionGameRoundStart.BundleId = bundleId;
			attributionGameRoundStart.GameMode = gameMode;
			attributionGameRoundStart.FormatType = formatType;
			EnqueueMessage(attributionGameRoundStart);
		}

		public void SendAttributionHeadlessAccountCreated(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier)
		{
			AttributionHeadlessAccountCreated attributionHeadlessAccountCreated = new AttributionHeadlessAccountCreated();
			attributionHeadlessAccountCreated.ApplicationId = applicationId;
			attributionHeadlessAccountCreated.DeviceType = deviceType;
			attributionHeadlessAccountCreated.FirstInstallDate = firstInstallDate;
			attributionHeadlessAccountCreated.BundleId = bundleId;
			attributionHeadlessAccountCreated.Identifier = identifier;
			EnqueueMessage(attributionHeadlessAccountCreated);
		}

		public void SendAttributionHeadlessAccountHealedUp(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string temporaryGameAccountId, IdentifierInfo identifier)
		{
			AttributionHeadlessAccountHealedUp attributionHeadlessAccountHealedUp = new AttributionHeadlessAccountHealedUp();
			attributionHeadlessAccountHealedUp.ApplicationId = applicationId;
			attributionHeadlessAccountHealedUp.DeviceType = deviceType;
			attributionHeadlessAccountHealedUp.FirstInstallDate = firstInstallDate;
			attributionHeadlessAccountHealedUp.BundleId = bundleId;
			attributionHeadlessAccountHealedUp.TemporaryGameAccountId = temporaryGameAccountId;
			attributionHeadlessAccountHealedUp.Identifier = identifier;
			EnqueueMessage(attributionHeadlessAccountHealedUp);
		}

		public void SendAttributionItemTransaction(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string itemId, int quantity)
		{
			AttributionItemTransaction attributionItemTransaction = new AttributionItemTransaction();
			attributionItemTransaction.ApplicationId = applicationId;
			attributionItemTransaction.DeviceType = deviceType;
			attributionItemTransaction.FirstInstallDate = firstInstallDate;
			attributionItemTransaction.BundleId = bundleId;
			attributionItemTransaction.ItemId = itemId;
			attributionItemTransaction.Quantity = quantity;
			EnqueueMessage(attributionItemTransaction);
		}

		public void SendAttributionLogin(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier)
		{
			AttributionLogin attributionLogin = new AttributionLogin();
			attributionLogin.ApplicationId = applicationId;
			attributionLogin.DeviceType = deviceType;
			attributionLogin.FirstInstallDate = firstInstallDate;
			attributionLogin.BundleId = bundleId;
			attributionLogin.Identifier = identifier;
			EnqueueMessage(attributionLogin);
		}

		public void SendAttributionPurchase(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string purchaseType, string transactionId, int quantity, List<AttributionPurchase.PaymentInfo> payments, float amount, string currency)
		{
			AttributionPurchase attributionPurchase = new AttributionPurchase();
			attributionPurchase.ApplicationId = applicationId;
			attributionPurchase.DeviceType = deviceType;
			attributionPurchase.FirstInstallDate = firstInstallDate;
			attributionPurchase.BundleId = bundleId;
			attributionPurchase.PurchaseType = purchaseType;
			attributionPurchase.TransactionId = transactionId;
			attributionPurchase.Quantity = quantity;
			attributionPurchase.Payments = payments;
			attributionPurchase.Amount = amount;
			attributionPurchase.Currency = currency;
			EnqueueMessage(attributionPurchase);
		}

		public void SendAttributionRegistration(string applicationId, string deviceType, ulong firstInstallDate, string bundleId)
		{
			AttributionRegistration attributionRegistration = new AttributionRegistration();
			attributionRegistration.ApplicationId = applicationId;
			attributionRegistration.DeviceType = deviceType;
			attributionRegistration.FirstInstallDate = firstInstallDate;
			attributionRegistration.BundleId = bundleId;
			EnqueueMessage(attributionRegistration);
		}

		public void SendAttributionScenarioResult(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, int scenarioId, string result, int bossId, IdentifierInfo identifier)
		{
			AttributionScenarioResult attributionScenarioResult = new AttributionScenarioResult();
			attributionScenarioResult.ApplicationId = applicationId;
			attributionScenarioResult.DeviceType = deviceType;
			attributionScenarioResult.FirstInstallDate = firstInstallDate;
			attributionScenarioResult.BundleId = bundleId;
			attributionScenarioResult.ScenarioId = scenarioId;
			attributionScenarioResult.Result = result;
			attributionScenarioResult.BossId = bossId;
			attributionScenarioResult.Identifier = identifier;
			EnqueueMessage(attributionScenarioResult);
		}

		public void SendAttributionVirtualCurrencyTransaction(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, float amount, string currency)
		{
			AttributionVirtualCurrencyTransaction attributionVirtualCurrencyTransaction = new AttributionVirtualCurrencyTransaction();
			attributionVirtualCurrencyTransaction.ApplicationId = applicationId;
			attributionVirtualCurrencyTransaction.DeviceType = deviceType;
			attributionVirtualCurrencyTransaction.FirstInstallDate = firstInstallDate;
			attributionVirtualCurrencyTransaction.BundleId = bundleId;
			attributionVirtualCurrencyTransaction.Amount = amount;
			attributionVirtualCurrencyTransaction.Currency = currency;
			EnqueueMessage(attributionVirtualCurrencyTransaction);
		}

		public void SendBlizzardCheckoutGeneric(string messageKey, string messageValue)
		{
			BlizzardCheckoutGeneric blizzardCheckoutGeneric = new BlizzardCheckoutGeneric();
			blizzardCheckoutGeneric.Player = GetPlayer();
			blizzardCheckoutGeneric.DeviceInfo = GetDeviceInfo();
			blizzardCheckoutGeneric.MessageKey = messageKey;
			blizzardCheckoutGeneric.MessageValue = messageValue;
			EnqueueMessage(blizzardCheckoutGeneric);
		}

		public void SendBlizzardCheckoutInitializationResult(bool success, string failureReason, string failureDetails)
		{
			BlizzardCheckoutInitializationResult blizzardCheckoutInitializationResult = new BlizzardCheckoutInitializationResult();
			blizzardCheckoutInitializationResult.Player = GetPlayer();
			blizzardCheckoutInitializationResult.DeviceInfo = GetDeviceInfo();
			blizzardCheckoutInitializationResult.Success = success;
			blizzardCheckoutInitializationResult.FailureReason = failureReason;
			blizzardCheckoutInitializationResult.FailureDetails = failureDetails;
			EnqueueMessage(blizzardCheckoutInitializationResult);
		}

		public void SendBlizzardCheckoutIsReady(double secondsShown, bool isReady)
		{
			BlizzardCheckoutIsReady blizzardCheckoutIsReady = new BlizzardCheckoutIsReady();
			blizzardCheckoutIsReady.Player = GetPlayer();
			blizzardCheckoutIsReady.DeviceInfo = GetDeviceInfo();
			blizzardCheckoutIsReady.SecondsShown = secondsShown;
			blizzardCheckoutIsReady.IsReady = isReady;
			EnqueueMessage(blizzardCheckoutIsReady);
		}

		public void SendBlizzardCheckoutPurchaseCancel()
		{
			BlizzardCheckoutPurchaseCancel blizzardCheckoutPurchaseCancel = new BlizzardCheckoutPurchaseCancel();
			blizzardCheckoutPurchaseCancel.Player = GetPlayer();
			blizzardCheckoutPurchaseCancel.DeviceInfo = GetDeviceInfo();
			EnqueueMessage(blizzardCheckoutPurchaseCancel);
		}

		public void SendBlizzardCheckoutPurchaseCompletedFailure(string transactionId, string productId, string currency, List<string> errorCodes)
		{
			BlizzardCheckoutPurchaseCompletedFailure blizzardCheckoutPurchaseCompletedFailure = new BlizzardCheckoutPurchaseCompletedFailure();
			blizzardCheckoutPurchaseCompletedFailure.Player = GetPlayer();
			blizzardCheckoutPurchaseCompletedFailure.DeviceInfo = GetDeviceInfo();
			blizzardCheckoutPurchaseCompletedFailure.TransactionId = transactionId;
			blizzardCheckoutPurchaseCompletedFailure.ProductId = productId;
			blizzardCheckoutPurchaseCompletedFailure.Currency = currency;
			blizzardCheckoutPurchaseCompletedFailure.ErrorCodes = errorCodes;
			EnqueueMessage(blizzardCheckoutPurchaseCompletedFailure);
		}

		public void SendBlizzardCheckoutPurchaseCompletedSuccess(string transactionId, string productId, string currency)
		{
			BlizzardCheckoutPurchaseCompletedSuccess blizzardCheckoutPurchaseCompletedSuccess = new BlizzardCheckoutPurchaseCompletedSuccess();
			blizzardCheckoutPurchaseCompletedSuccess.Player = GetPlayer();
			blizzardCheckoutPurchaseCompletedSuccess.DeviceInfo = GetDeviceInfo();
			blizzardCheckoutPurchaseCompletedSuccess.TransactionId = transactionId;
			blizzardCheckoutPurchaseCompletedSuccess.ProductId = productId;
			blizzardCheckoutPurchaseCompletedSuccess.Currency = currency;
			EnqueueMessage(blizzardCheckoutPurchaseCompletedSuccess);
		}

		public void SendBlizzardCheckoutPurchaseStart(string transactionId, string productId, string currency)
		{
			BlizzardCheckoutPurchaseStart blizzardCheckoutPurchaseStart = new BlizzardCheckoutPurchaseStart();
			blizzardCheckoutPurchaseStart.Player = GetPlayer();
			blizzardCheckoutPurchaseStart.DeviceInfo = GetDeviceInfo();
			blizzardCheckoutPurchaseStart.TransactionId = transactionId;
			blizzardCheckoutPurchaseStart.ProductId = productId;
			blizzardCheckoutPurchaseStart.Currency = currency;
			EnqueueMessage(blizzardCheckoutPurchaseStart);
		}

		public void SendBoxInteractable(string testType, float duration, string clientChangelist)
		{
			BoxInteractable boxInteractable = new BoxInteractable();
			boxInteractable.TestType = testType;
			boxInteractable.Duration = duration;
			boxInteractable.ClientChangelist = clientChangelist;
			EnqueueMessage(boxInteractable);
		}

		public void SendButtonPressed(string buttonName)
		{
			ButtonPressed buttonPressed = new ButtonPressed();
			buttonPressed.ButtonName = buttonName;
			EnqueueMessage(buttonPressed);
		}

		public void SendChangePackQuantity(int boosterId)
		{
			ChangePackQuantity changePackQuantity = new ChangePackQuantity();
			changePackQuantity.BoosterId = boosterId;
			EnqueueMessage(changePackQuantity);
		}

		public void SendCinematic(bool begin, float duration)
		{
			Blizzard.Telemetry.WTCG.Client.Cinematic cinematic = new Blizzard.Telemetry.WTCG.Client.Cinematic();
			cinematic.DeviceInfo = GetDeviceInfo();
			cinematic.Begin = begin;
			cinematic.Duration = duration;
			EnqueueMessage(cinematic);
		}

		public void SendClickRecruitAFriend()
		{
			ClickRecruitAFriend clickRecruitAFriend = new ClickRecruitAFriend();
			clickRecruitAFriend.DeviceInfo = GetDeviceInfo();
			EnqueueMessage(clickRecruitAFriend);
		}

		public void SendClientReset(bool forceLogin, bool forceNoAccountTutorial)
		{
			ClientReset clientReset = new ClientReset();
			clientReset.DeviceInfo = GetDeviceInfo();
			clientReset.ForceLogin = forceLogin;
			clientReset.ForceNoAccountTutorial = forceNoAccountTutorial;
			EnqueueMessage(clientReset);
		}

		public void SendCollectionLeftRightClick(CollectionLeftRightClick.Target target_)
		{
			CollectionLeftRightClick collectionLeftRightClick = new CollectionLeftRightClick();
			collectionLeftRightClick.Target_ = target_;
			EnqueueMessage(collectionLeftRightClick);
		}

		public void SendConnectToGameServer(uint resultBnetCode, string resultBnetCodeString, long timeSpentMilliseconds, GameSessionInfo gameSessionInfo)
		{
			ConnectToGameServer connectToGameServer = new ConnectToGameServer();
			connectToGameServer.DeviceInfo = GetDeviceInfo();
			connectToGameServer.Player = GetPlayer();
			connectToGameServer.ResultBnetCode = resultBnetCode;
			connectToGameServer.ResultBnetCodeString = resultBnetCodeString;
			connectToGameServer.TimeSpentMilliseconds = timeSpentMilliseconds;
			connectToGameServer.GameSessionInfo = gameSessionInfo;
			EnqueueMessage(connectToGameServer);
		}

		public void SendContentConnectFailedToConnect(string url, int httpErrorcode, int serverErrorcode)
		{
			ContentConnectFailedToConnect contentConnectFailedToConnect = new ContentConnectFailedToConnect();
			contentConnectFailedToConnect.DeviceInfo = GetDeviceInfo();
			contentConnectFailedToConnect.Url = url;
			contentConnectFailedToConnect.HttpErrorcode = httpErrorcode;
			contentConnectFailedToConnect.ServerErrorcode = serverErrorcode;
			EnqueueMessage(contentConnectFailedToConnect);
		}

		public void SendCrmEvent(string eventName, string eventPayload, string applicationId)
		{
			CrmEvent crmEvent = new CrmEvent();
			crmEvent.EventName = eventName;
			crmEvent.EventPayload = eventPayload;
			crmEvent.ApplicationId = applicationId;
			EnqueueMessage(crmEvent);
		}

		public void SendDataUpdateFailed(float duration, long realDownloadBytes, long expectedDownloadBytes, int errorCode)
		{
			DataUpdateFailed dataUpdateFailed = new DataUpdateFailed();
			dataUpdateFailed.DeviceInfo = GetDeviceInfo();
			dataUpdateFailed.Duration = duration;
			dataUpdateFailed.RealDownloadBytes = realDownloadBytes;
			dataUpdateFailed.ExpectedDownloadBytes = expectedDownloadBytes;
			dataUpdateFailed.ErrorCode = errorCode;
			EnqueueMessage(dataUpdateFailed);
		}

		public void SendDataUpdateFinished(float duration, long realDownloadBytes, long expectedDownloadBytes)
		{
			DataUpdateFinished dataUpdateFinished = new DataUpdateFinished();
			dataUpdateFinished.DeviceInfo = GetDeviceInfo();
			dataUpdateFinished.Duration = duration;
			dataUpdateFinished.RealDownloadBytes = realDownloadBytes;
			dataUpdateFinished.ExpectedDownloadBytes = expectedDownloadBytes;
			EnqueueMessage(dataUpdateFinished);
		}

		public void SendDataUpdateProgress(float duration, long realDownloadBytes, long expectedDownloadBytes)
		{
			DataUpdateProgress dataUpdateProgress = new DataUpdateProgress();
			dataUpdateProgress.DeviceInfo = GetDeviceInfo();
			dataUpdateProgress.Duration = duration;
			dataUpdateProgress.RealDownloadBytes = realDownloadBytes;
			dataUpdateProgress.ExpectedDownloadBytes = expectedDownloadBytes;
			EnqueueMessage(dataUpdateProgress);
		}

		public void SendDataUpdateStarted()
		{
			DataUpdateStarted dataUpdateStarted = new DataUpdateStarted();
			dataUpdateStarted.DeviceInfo = GetDeviceInfo();
			EnqueueMessage(dataUpdateStarted);
		}

		public void SendDeckCopied(long deckId, string deckHash)
		{
			DeckCopied deckCopied = new DeckCopied();
			deckCopied.Player = GetPlayer();
			deckCopied.DeviceInfo = GetDeviceInfo();
			deckCopied.DeckId = deckId;
			deckCopied.DeckHash = deckHash;
			EnqueueMessage(deckCopied);
		}

		public void SendDeckPickerToCollection(DeckPickerToCollection.Path path_)
		{
			DeckPickerToCollection deckPickerToCollection = new DeckPickerToCollection();
			deckPickerToCollection.DeviceInfo = GetDeviceInfo();
			deckPickerToCollection.Path_ = path_;
			EnqueueMessage(deckPickerToCollection);
		}

		public void SendDeckUpdateResponseInfo(float duration)
		{
			DeckUpdateResponseInfo deckUpdateResponseInfo = new DeckUpdateResponseInfo();
			deckUpdateResponseInfo.DeviceInfo = GetDeviceInfo();
			deckUpdateResponseInfo.Duration = duration;
			EnqueueMessage(deckUpdateResponseInfo);
		}

		public void SendDeepLinkExecuted(string deepLink, string source, bool completed)
		{
			DeepLinkExecuted deepLinkExecuted = new DeepLinkExecuted();
			deepLinkExecuted.Player = GetPlayer();
			deepLinkExecuted.DeviceInfo = GetDeviceInfo();
			deepLinkExecuted.DeepLink = deepLink;
			deepLinkExecuted.Source = source;
			deepLinkExecuted.Completed = completed;
			EnqueueMessage(deepLinkExecuted);
		}

		public void SendDeviceInfo(Blizzard.Telemetry.WTCG.Client.DeviceInfo.OSCategory os, string osVersion, string model, Blizzard.Telemetry.WTCG.Client.DeviceInfo.ScreenCategory screen, Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType connectionType_, string droidTextureCompression)
		{
			Blizzard.Telemetry.WTCG.Client.DeviceInfo deviceInfo = new Blizzard.Telemetry.WTCG.Client.DeviceInfo();
			deviceInfo.Os = os;
			deviceInfo.OsVersion = osVersion;
			deviceInfo.Model = model;
			deviceInfo.Screen = screen;
			deviceInfo.ConnectionType_ = connectionType_;
			deviceInfo.DroidTextureCompression = droidTextureCompression;
			EnqueueMessage(deviceInfo);
		}

		public void SendDeviceMuteChanged(bool muted)
		{
			DeviceMuteChanged deviceMuteChanged = new DeviceMuteChanged();
			deviceMuteChanged.Player = GetPlayer();
			deviceMuteChanged.DeviceInfo = GetDeviceInfo();
			deviceMuteChanged.Muted = muted;
			EnqueueMessage(deviceMuteChanged);
		}

		public void SendDeviceVolumeChanged(float oldVolume, float newVolume)
		{
			DeviceVolumeChanged deviceVolumeChanged = new DeviceVolumeChanged();
			deviceVolumeChanged.Player = GetPlayer();
			deviceVolumeChanged.DeviceInfo = GetDeviceInfo();
			deviceVolumeChanged.OldVolume = oldVolume;
			deviceVolumeChanged.NewVolume = newVolume;
			EnqueueMessage(deviceVolumeChanged);
		}

		public void SendDragDropCancelPlayCard(long scenarioId, string cardType)
		{
			DragDropCancelPlayCard dragDropCancelPlayCard = new DragDropCancelPlayCard();
			dragDropCancelPlayCard.DeviceInfo = GetDeviceInfo();
			dragDropCancelPlayCard.ScenarioId = scenarioId;
			dragDropCancelPlayCard.CardType = cardType;
			EnqueueMessage(dragDropCancelPlayCard);
		}

		public void SendEndGameScreenInit(float elapsedTime, int medalInfoRetryCount, bool medalInfoRetriesTimedOut, bool showRankedReward, bool showCardBackProgress, int otherRewardCount)
		{
			EndGameScreenInit endGameScreenInit = new EndGameScreenInit();
			endGameScreenInit.Player = GetPlayer();
			endGameScreenInit.DeviceInfo = GetDeviceInfo();
			endGameScreenInit.ElapsedTime = elapsedTime;
			endGameScreenInit.MedalInfoRetryCount = medalInfoRetryCount;
			endGameScreenInit.MedalInfoRetriesTimedOut = medalInfoRetriesTimedOut;
			endGameScreenInit.ShowRankedReward = showRankedReward;
			endGameScreenInit.ShowCardBackProgress = showCardBackProgress;
			endGameScreenInit.OtherRewardCount = otherRewardCount;
			EnqueueMessage(endGameScreenInit);
		}

		public void SendFatalBattleNetError(int errorCode, string description)
		{
			FatalBattleNetError fatalBattleNetError = new FatalBattleNetError();
			fatalBattleNetError.DeviceInfo = GetDeviceInfo();
			fatalBattleNetError.ErrorCode = errorCode;
			fatalBattleNetError.Description = description;
			EnqueueMessage(fatalBattleNetError);
		}

		public void SendFatalError(string reason)
		{
			FatalError fatalError = new FatalError();
			fatalError.DeviceInfo = GetDeviceInfo();
			fatalError.Reason = reason;
			EnqueueMessage(fatalError);
		}

		public void SendFindGameResult(uint resultCode, string resultCodeString, long timeSpentMilliseconds, GameSessionInfo gameSessionInfo)
		{
			FindGameResult findGameResult = new FindGameResult();
			findGameResult.DeviceInfo = GetDeviceInfo();
			findGameResult.Player = GetPlayer();
			findGameResult.ResultCode = resultCode;
			findGameResult.ResultCodeString = resultCodeString;
			findGameResult.TimeSpentMilliseconds = timeSpentMilliseconds;
			findGameResult.GameSessionInfo = gameSessionInfo;
			EnqueueMessage(findGameResult);
		}

		public void SendFlowPerformance(string uniqueId, Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType flowType_, float averageFps, float duration, float fpsWarningsThreshold, int fpsWarningsTotalOccurences, float fpsWarningsTotalTime, float fpsWarningsAverageTime, float fpsWarningsMaxTime)
		{
			Blizzard.Telemetry.WTCG.Client.FlowPerformance flowPerformance = new Blizzard.Telemetry.WTCG.Client.FlowPerformance();
			flowPerformance.UniqueId = uniqueId;
			flowPerformance.DeviceInfo = GetDeviceInfo();
			flowPerformance.Player = GetPlayer();
			flowPerformance.FlowType_ = flowType_;
			flowPerformance.AverageFps = averageFps;
			flowPerformance.Duration = duration;
			flowPerformance.FpsWarningsThreshold = fpsWarningsThreshold;
			flowPerformance.FpsWarningsTotalOccurences = fpsWarningsTotalOccurences;
			flowPerformance.FpsWarningsTotalTime = fpsWarningsTotalTime;
			flowPerformance.FpsWarningsAverageTime = fpsWarningsAverageTime;
			flowPerformance.FpsWarningsMaxTime = fpsWarningsMaxTime;
			EnqueueMessage(flowPerformance);
		}

		public void SendFlowPerformanceBattlegrounds(string flowId, string gameUuid, int totalRounds)
		{
			Blizzard.Telemetry.WTCG.Client.FlowPerformanceBattlegrounds flowPerformanceBattlegrounds = new Blizzard.Telemetry.WTCG.Client.FlowPerformanceBattlegrounds();
			flowPerformanceBattlegrounds.FlowId = flowId;
			flowPerformanceBattlegrounds.GameUuid = gameUuid;
			flowPerformanceBattlegrounds.TotalRounds = totalRounds;
			EnqueueMessage(flowPerformanceBattlegrounds);
		}

		public void SendFlowPerformanceGame(string flowId, string uuid, GameType gameType, FormatType formatType, int boardId, int scenarioId)
		{
			Blizzard.Telemetry.WTCG.Client.FlowPerformanceGame flowPerformanceGame = new Blizzard.Telemetry.WTCG.Client.FlowPerformanceGame();
			flowPerformanceGame.FlowId = flowId;
			flowPerformanceGame.DeviceInfo = GetDeviceInfo();
			flowPerformanceGame.Player = GetPlayer();
			flowPerformanceGame.Uuid = uuid;
			flowPerformanceGame.GameType = gameType;
			flowPerformanceGame.FormatType = formatType;
			flowPerformanceGame.BoardId = boardId;
			flowPerformanceGame.ScenarioId = scenarioId;
			EnqueueMessage(flowPerformanceGame);
		}

		public void SendFlowPerformanceShop(string flowId, Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType shopType_)
		{
			Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop flowPerformanceShop = new Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop();
			flowPerformanceShop.FlowId = flowId;
			flowPerformanceShop.DeviceInfo = GetDeviceInfo();
			flowPerformanceShop.Player = GetPlayer();
			flowPerformanceShop.ShopType_ = shopType_;
			EnqueueMessage(flowPerformanceShop);
		}

		public void SendFriendsListView(string currentScene)
		{
			FriendsListView friendsListView = new FriendsListView();
			friendsListView.DeviceInfo = GetDeviceInfo();
			friendsListView.CurrentScene = currentScene;
			EnqueueMessage(friendsListView);
		}

		public void SendGameRoundStartAudioSettings(bool deviceMuted, float deviceVolume, float masterVolume, float musicVolume)
		{
			GameRoundStartAudioSettings gameRoundStartAudioSettings = new GameRoundStartAudioSettings();
			gameRoundStartAudioSettings.DeviceInfo = GetDeviceInfo();
			gameRoundStartAudioSettings.Player = GetPlayer();
			gameRoundStartAudioSettings.DeviceMuted = deviceMuted;
			gameRoundStartAudioSettings.DeviceVolume = deviceVolume;
			gameRoundStartAudioSettings.MasterVolume = masterVolume;
			gameRoundStartAudioSettings.MusicVolume = musicVolume;
			EnqueueMessage(gameRoundStartAudioSettings);
		}

		public void SendIgnorableBattleNetError(int errorCode, string description)
		{
			IgnorableBattleNetError ignorableBattleNetError = new IgnorableBattleNetError();
			ignorableBattleNetError.DeviceInfo = GetDeviceInfo();
			ignorableBattleNetError.ErrorCode = errorCode;
			ignorableBattleNetError.Description = description;
			EnqueueMessage(ignorableBattleNetError);
		}

		public void SendIKSClicked(string iksCampaignName, string iksMediaUrl)
		{
			IKSClicked iKSClicked = new IKSClicked();
			iKSClicked.Player = GetPlayer();
			iKSClicked.DeviceInfo = GetDeviceInfo();
			iKSClicked.IksCampaignName = iksCampaignName;
			iKSClicked.IksMediaUrl = iksMediaUrl;
			EnqueueMessage(iKSClicked);
		}

		public void SendIKSIgnored(string iksCampaignName, string iksMediaUrl)
		{
			IKSIgnored iKSIgnored = new IKSIgnored();
			iKSIgnored.Player = GetPlayer();
			iKSIgnored.DeviceInfo = GetDeviceInfo();
			iKSIgnored.IksCampaignName = iksCampaignName;
			iKSIgnored.IksMediaUrl = iksMediaUrl;
			EnqueueMessage(iKSIgnored);
		}

		public void SendInGameMessageAction(string messageType, string title, InGameMessageAction.ActionType action, int viewCounts, string uid)
		{
			InGameMessageAction inGameMessageAction = new InGameMessageAction();
			inGameMessageAction.Player = GetPlayer();
			inGameMessageAction.DeviceInfo = GetDeviceInfo();
			inGameMessageAction.MessageType = messageType;
			inGameMessageAction.Title = title;
			inGameMessageAction.Action = action;
			inGameMessageAction.ViewCounts = viewCounts;
			inGameMessageAction.Uid = uid;
			EnqueueMessage(inGameMessageAction);
		}

		public void SendInGameMessageHandlerCalled(string messageType, string title, string uid)
		{
			InGameMessageHandlerCalled inGameMessageHandlerCalled = new InGameMessageHandlerCalled();
			inGameMessageHandlerCalled.Player = GetPlayer();
			inGameMessageHandlerCalled.DeviceInfo = GetDeviceInfo();
			inGameMessageHandlerCalled.MessageType = messageType;
			inGameMessageHandlerCalled.Title = title;
			inGameMessageHandlerCalled.Uid = uid;
			EnqueueMessage(inGameMessageHandlerCalled);
		}

		public void SendInitialClientStateOutOfOrder(int countNotificationsAchieve, int countNotificationsNotice, int countNotificationsCollection, int countNotificationsCurrency, int countNotificationsBooster, int countNotificationsHeroxp, int countNotificationsPlayerRecord, int countNotificationsArenaSession, int countNotificationsCardBack)
		{
			InitialClientStateOutOfOrder initialClientStateOutOfOrder = new InitialClientStateOutOfOrder();
			initialClientStateOutOfOrder.CountNotificationsAchieve = countNotificationsAchieve;
			initialClientStateOutOfOrder.CountNotificationsNotice = countNotificationsNotice;
			initialClientStateOutOfOrder.CountNotificationsCollection = countNotificationsCollection;
			initialClientStateOutOfOrder.CountNotificationsCurrency = countNotificationsCurrency;
			initialClientStateOutOfOrder.CountNotificationsBooster = countNotificationsBooster;
			initialClientStateOutOfOrder.CountNotificationsHeroxp = countNotificationsHeroxp;
			initialClientStateOutOfOrder.CountNotificationsPlayerRecord = countNotificationsPlayerRecord;
			initialClientStateOutOfOrder.CountNotificationsArenaSession = countNotificationsArenaSession;
			initialClientStateOutOfOrder.CountNotificationsCardBack = countNotificationsCardBack;
			initialClientStateOutOfOrder.DeviceInfo = GetDeviceInfo();
			EnqueueMessage(initialClientStateOutOfOrder);
		}

		public void SendJobBegin(string jobId, string testType)
		{
			JobBegin jobBegin = new JobBegin();
			jobBegin.JobId = jobId;
			jobBegin.TestType = testType;
			EnqueueMessage(jobBegin);
		}

		public void SendJobExceededLimit(string jobId, long jobDuration, string testType)
		{
			JobExceededLimit jobExceededLimit = new JobExceededLimit();
			jobExceededLimit.JobId = jobId;
			jobExceededLimit.JobDuration = jobDuration;
			jobExceededLimit.TestType = testType;
			EnqueueMessage(jobExceededLimit);
		}

		public void SendJobFinishFailure(string jobId, string jobFailureReason, string testType, string clientChangelist, float duration)
		{
			JobFinishFailure jobFinishFailure = new JobFinishFailure();
			jobFinishFailure.JobId = jobId;
			jobFinishFailure.JobFailureReason = jobFailureReason;
			jobFinishFailure.TestType = testType;
			jobFinishFailure.ClientChangelist = clientChangelist;
			jobFinishFailure.Duration = duration;
			EnqueueMessage(jobFinishFailure);
		}

		public void SendJobFinishSuccess(string jobId, string testType, string clientChangelist, float duration)
		{
			JobFinishSuccess jobFinishSuccess = new JobFinishSuccess();
			jobFinishSuccess.JobId = jobId;
			jobFinishSuccess.TestType = testType;
			jobFinishSuccess.ClientChangelist = clientChangelist;
			jobFinishSuccess.Duration = duration;
			EnqueueMessage(jobFinishSuccess);
		}

		public void SendJobStepExceededLimit(string jobId, long jobDuration, string testType)
		{
			JobStepExceededLimit jobStepExceededLimit = new JobStepExceededLimit();
			jobStepExceededLimit.JobId = jobId;
			jobStepExceededLimit.JobDuration = jobDuration;
			jobStepExceededLimit.TestType = testType;
			EnqueueMessage(jobStepExceededLimit);
		}

		public void SendLanguageChanged(string previousLanguage, string nextLanguage)
		{
			LanguageChanged languageChanged = new LanguageChanged();
			languageChanged.DeviceInfo = GetDeviceInfo();
			languageChanged.PreviousLanguage = previousLanguage;
			languageChanged.NextLanguage = nextLanguage;
			EnqueueMessage(languageChanged);
		}

		public void SendLargeThirdPartyReceiptFound(long receiptSize)
		{
			LargeThirdPartyReceiptFound largeThirdPartyReceiptFound = new LargeThirdPartyReceiptFound();
			largeThirdPartyReceiptFound.Player = GetPlayer();
			largeThirdPartyReceiptFound.DeviceInfo = GetDeviceInfo();
			largeThirdPartyReceiptFound.ReceiptSize = receiptSize;
			EnqueueMessage(largeThirdPartyReceiptFound);
		}

		public void SendLiveIssue(string category, string details)
		{
			LiveIssue liveIssue = new LiveIssue();
			liveIssue.Category = category;
			liveIssue.Details = details;
			EnqueueMessage(liveIssue);
		}

		public void SendLocaleDataUpdateFailed(float duration, long realDownloadBytes, long expectedDownloadBytes, int errorCode)
		{
			LocaleDataUpdateFailed localeDataUpdateFailed = new LocaleDataUpdateFailed();
			localeDataUpdateFailed.DeviceInfo = GetDeviceInfo();
			localeDataUpdateFailed.Duration = duration;
			localeDataUpdateFailed.RealDownloadBytes = realDownloadBytes;
			localeDataUpdateFailed.ExpectedDownloadBytes = expectedDownloadBytes;
			localeDataUpdateFailed.ErrorCode = errorCode;
			EnqueueMessage(localeDataUpdateFailed);
		}

		public void SendLocaleDataUpdateFinished(float duration, long realDownloadBytes, long expectedDownloadBytes)
		{
			LocaleDataUpdateFinished localeDataUpdateFinished = new LocaleDataUpdateFinished();
			localeDataUpdateFinished.DeviceInfo = GetDeviceInfo();
			localeDataUpdateFinished.Duration = duration;
			localeDataUpdateFinished.RealDownloadBytes = realDownloadBytes;
			localeDataUpdateFinished.ExpectedDownloadBytes = expectedDownloadBytes;
			EnqueueMessage(localeDataUpdateFinished);
		}

		public void SendLocaleDataUpdateStarted(string locale)
		{
			LocaleDataUpdateStarted localeDataUpdateStarted = new LocaleDataUpdateStarted();
			localeDataUpdateStarted.DeviceInfo = GetDeviceInfo();
			localeDataUpdateStarted.Locale = locale;
			EnqueueMessage(localeDataUpdateStarted);
		}

		public void SendLoginTokenFetchResult(LoginTokenFetchResult.TokenFetchResult result)
		{
			LoginTokenFetchResult loginTokenFetchResult = new LoginTokenFetchResult();
			loginTokenFetchResult.Player = GetPlayer();
			loginTokenFetchResult.DeviceInfo = GetDeviceInfo();
			loginTokenFetchResult.Result = result;
			EnqueueMessage(loginTokenFetchResult);
		}

		public void SendManaFilterToggleOff()
		{
			ManaFilterToggleOff manaFilterToggleOff = new ManaFilterToggleOff();
			manaFilterToggleOff.DeviceInfo = GetDeviceInfo();
			EnqueueMessage(manaFilterToggleOff);
		}

		public void SendMASDKAuthResult(MASDKAuthResult.AuthResult result, int errorCode, string source)
		{
			MASDKAuthResult mASDKAuthResult = new MASDKAuthResult();
			mASDKAuthResult.Player = GetPlayer();
			mASDKAuthResult.DeviceInfo = GetDeviceInfo();
			mASDKAuthResult.Result = result;
			mASDKAuthResult.ErrorCode = errorCode;
			mASDKAuthResult.Source = source;
			EnqueueMessage(mASDKAuthResult);
		}

		public void SendMASDKGuestCreationFailure(int errorCode)
		{
			MASDKGuestCreationFailure mASDKGuestCreationFailure = new MASDKGuestCreationFailure();
			mASDKGuestCreationFailure.Player = GetPlayer();
			mASDKGuestCreationFailure.DeviceInfo = GetDeviceInfo();
			mASDKGuestCreationFailure.ErrorCode = errorCode;
			EnqueueMessage(mASDKGuestCreationFailure);
		}

		public void SendMASDKImportResult(MASDKImportResult.ImportResult result, MASDKImportResult.ImportType importType_, int errorCode)
		{
			MASDKImportResult mASDKImportResult = new MASDKImportResult();
			mASDKImportResult.Player = GetPlayer();
			mASDKImportResult.DeviceInfo = GetDeviceInfo();
			mASDKImportResult.Result = result;
			mASDKImportResult.ImportType_ = importType_;
			mASDKImportResult.ErrorCode = errorCode;
			EnqueueMessage(mASDKImportResult);
		}

		public void SendMasterVolumeChanged(float oldVolume, float newVolume)
		{
			MasterVolumeChanged masterVolumeChanged = new MasterVolumeChanged();
			masterVolumeChanged.Player = GetPlayer();
			masterVolumeChanged.DeviceInfo = GetDeviceInfo();
			masterVolumeChanged.OldVolume = oldVolume;
			masterVolumeChanged.NewVolume = newVolume;
			EnqueueMessage(masterVolumeChanged);
		}

		public void SendMissingAssetError(string missingAssetPath, string assetContext)
		{
			MissingAssetError missingAssetError = new MissingAssetError();
			missingAssetError.DeviceInfo = GetDeviceInfo();
			missingAssetError.MissingAssetPath = missingAssetPath;
			missingAssetError.AssetContext = assetContext;
			EnqueueMessage(missingAssetError);
		}

		public void SendMusicVolumeChanged(float oldVolume, float newVolume)
		{
			MusicVolumeChanged musicVolumeChanged = new MusicVolumeChanged();
			musicVolumeChanged.Player = GetPlayer();
			musicVolumeChanged.DeviceInfo = GetDeviceInfo();
			musicVolumeChanged.OldVolume = oldVolume;
			musicVolumeChanged.NewVolume = newVolume;
			EnqueueMessage(musicVolumeChanged);
		}

		public void SendNetworkError(NetworkError.ErrorType errorType_, string description, int errorCode)
		{
			NetworkError networkError = new NetworkError();
			networkError.DeviceInfo = GetDeviceInfo();
			networkError.ErrorType_ = errorType_;
			networkError.Description = description;
			networkError.ErrorCode = errorCode;
			EnqueueMessage(networkError);
		}

		public void SendNetworkUnreachableRecovered(int outageSeconds)
		{
			NetworkUnreachableRecovered networkUnreachableRecovered = new NetworkUnreachableRecovered();
			networkUnreachableRecovered.DeviceInfo = GetDeviceInfo();
			networkUnreachableRecovered.OutageSeconds = outageSeconds;
			EnqueueMessage(networkUnreachableRecovered);
		}

		public void SendPackOpenToStore(PackOpenToStore.Path path_)
		{
			PackOpenToStore packOpenToStore = new PackOpenToStore();
			packOpenToStore.DeviceInfo = GetDeviceInfo();
			packOpenToStore.Path_ = path_;
			EnqueueMessage(packOpenToStore);
		}

		public void SendPresenceChanged(PresenceStatus newPresenceStatus, PresenceStatus prevPresenceStatus, long millisecondsSincePrev)
		{
			PresenceChanged presenceChanged = new PresenceChanged();
			presenceChanged.Player = GetPlayer();
			presenceChanged.DeviceInfo = GetDeviceInfo();
			presenceChanged.NewPresenceStatus = newPresenceStatus;
			presenceChanged.PrevPresenceStatus = prevPresenceStatus;
			presenceChanged.MillisecondsSincePrev = millisecondsSincePrev;
			EnqueueMessage(presenceChanged);
		}

		public void SendPreviousInstanceStatus(int totalCrashCount, int totalExceptionCount, int lowMemoryWarningCount, int crashInARowCount, int sameExceptionCount, bool crashed, string exceptionHash)
		{
			Blizzard.Telemetry.WTCG.Client.PreviousInstanceStatus previousInstanceStatus = new Blizzard.Telemetry.WTCG.Client.PreviousInstanceStatus();
			previousInstanceStatus.DeviceInfo = GetDeviceInfo();
			previousInstanceStatus.TotalCrashCount = totalCrashCount;
			previousInstanceStatus.TotalExceptionCount = totalExceptionCount;
			previousInstanceStatus.LowMemoryWarningCount = lowMemoryWarningCount;
			previousInstanceStatus.CrashInARowCount = crashInARowCount;
			previousInstanceStatus.SameExceptionCount = sameExceptionCount;
			previousInstanceStatus.Crashed = crashed;
			previousInstanceStatus.ExceptionHash = exceptionHash;
			EnqueueMessage(previousInstanceStatus);
		}

		public void SendPurchaseCancelClicked(long pmtProductId)
		{
			PurchaseCancelClicked purchaseCancelClicked = new PurchaseCancelClicked();
			purchaseCancelClicked.Player = GetPlayer();
			purchaseCancelClicked.DeviceInfo = GetDeviceInfo();
			purchaseCancelClicked.PmtProductId = pmtProductId;
			EnqueueMessage(purchaseCancelClicked);
		}

		public void SendPurchasePayNowClicked(long pmtProductId)
		{
			PurchasePayNowClicked purchasePayNowClicked = new PurchasePayNowClicked();
			purchasePayNowClicked.Player = GetPlayer();
			purchasePayNowClicked.DeviceInfo = GetDeviceInfo();
			purchasePayNowClicked.PmtProductId = pmtProductId;
			EnqueueMessage(purchasePayNowClicked);
		}

		public void SendPushEvent(string campaignId, string eventPayload, string applicationId)
		{
			PushEvent pushEvent = new PushEvent();
			pushEvent.CampaignId = campaignId;
			pushEvent.EventPayload = eventPayload;
			pushEvent.ApplicationId = applicationId;
			EnqueueMessage(pushEvent);
		}

		public void SendPushRegistration(string pushId, int utcOffset, string timezone, string applicationId, string language, string os, string osVersion, string deviceHeight, string deviceWidth, string deviceDpi)
		{
			PushRegistration pushRegistration = new PushRegistration();
			pushRegistration.PushId = pushId;
			pushRegistration.UtcOffset = utcOffset;
			pushRegistration.Timezone = timezone;
			pushRegistration.ApplicationId = applicationId;
			pushRegistration.Language = language;
			pushRegistration.Os = os;
			pushRegistration.OsVersion = osVersion;
			pushRegistration.DeviceHeight = deviceHeight;
			pushRegistration.DeviceWidth = deviceWidth;
			pushRegistration.DeviceDpi = deviceDpi;
			EnqueueMessage(pushRegistration);
		}

		public void SendRealMoneyTransaction(string applicationId, string appStore, string receipt, string receiptSignature, string productId, string itemCost, string itemQuantity, string localCurrency, string transactionId)
		{
			RealMoneyTransaction realMoneyTransaction = new RealMoneyTransaction();
			realMoneyTransaction.ApplicationId = applicationId;
			realMoneyTransaction.AppStore = appStore;
			realMoneyTransaction.Receipt = receipt;
			realMoneyTransaction.ReceiptSignature = receiptSignature;
			realMoneyTransaction.ProductId = productId;
			realMoneyTransaction.ItemCost = itemCost;
			realMoneyTransaction.ItemQuantity = itemQuantity;
			realMoneyTransaction.LocalCurrency = localCurrency;
			realMoneyTransaction.TransactionId = transactionId;
			EnqueueMessage(realMoneyTransaction);
		}

		public void SendReconnectSuccess(float disconnectDuration, float reconnectDuration, string reconnectType)
		{
			ReconnectSuccess reconnectSuccess = new ReconnectSuccess();
			reconnectSuccess.Player = GetPlayer();
			reconnectSuccess.DeviceInfo = GetDeviceInfo();
			reconnectSuccess.DisconnectDuration = disconnectDuration;
			reconnectSuccess.ReconnectDuration = reconnectDuration;
			reconnectSuccess.ReconnectType = reconnectType;
			EnqueueMessage(reconnectSuccess);
		}

		public void SendReconnectTimeout(string reconnectType)
		{
			ReconnectTimeout reconnectTimeout = new ReconnectTimeout();
			reconnectTimeout.Player = GetPlayer();
			reconnectTimeout.DeviceInfo = GetDeviceInfo();
			reconnectTimeout.ReconnectType = reconnectType;
			EnqueueMessage(reconnectTimeout);
		}

		public void SendRepairPrestep(int doubletapFingers, int locales)
		{
			RepairPrestep repairPrestep = new RepairPrestep();
			repairPrestep.DoubletapFingers = doubletapFingers;
			repairPrestep.Locales = locales;
			EnqueueMessage(repairPrestep);
		}

		public void SendRestartDueToPlayerMigration()
		{
			RestartDueToPlayerMigration restartDueToPlayerMigration = new RestartDueToPlayerMigration();
			restartDueToPlayerMigration.Player = GetPlayer();
			restartDueToPlayerMigration.DeviceInfo = GetDeviceInfo();
			EnqueueMessage(restartDueToPlayerMigration);
		}

		public void SendReturningPlayerDeckNotCreated(uint aBGroup)
		{
			ReturningPlayerDeckNotCreated returningPlayerDeckNotCreated = new ReturningPlayerDeckNotCreated();
			returningPlayerDeckNotCreated.Player = GetPlayer();
			returningPlayerDeckNotCreated.ABGroup = aBGroup;
			EnqueueMessage(returningPlayerDeckNotCreated);
		}

		public void SendRuntimeUpdate(float duration, RuntimeUpdate.Intention intention_)
		{
			RuntimeUpdate runtimeUpdate = new RuntimeUpdate();
			runtimeUpdate.DeviceInfo = GetDeviceInfo();
			runtimeUpdate.Duration = duration;
			runtimeUpdate.Intention_ = intention_;
			EnqueueMessage(runtimeUpdate);
		}

		public void SendSessionEnd(string applicationId)
		{
			SessionEnd sessionEnd = new SessionEnd();
			sessionEnd.ApplicationId = applicationId;
			EnqueueMessage(sessionEnd);
		}

		public void SendSessionStart(string eventPayload, string applicationId)
		{
			SessionStart sessionStart = new SessionStart();
			sessionStart.EventPayload = eventPayload;
			sessionStart.ApplicationId = applicationId;
			EnqueueMessage(sessionStart);
		}

		public void SendShopBalanceAvailable(List<Balance> balances)
		{
			ShopBalanceAvailable shopBalanceAvailable = new ShopBalanceAvailable();
			shopBalanceAvailable.Player = GetPlayer();
			shopBalanceAvailable.Balances = balances;
			EnqueueMessage(shopBalanceAvailable);
		}

		public void SendShopCardClick(ShopCard shopcard)
		{
			ShopCardClick shopCardClick = new ShopCardClick();
			shopCardClick.Player = GetPlayer();
			shopCardClick.Shopcard = shopcard;
			EnqueueMessage(shopCardClick);
		}

		public void SendShopPurchaseEvent(Product product, int quantity, string currency, double amount, bool isGift, string storefront, bool purchaseComplete, string storeType)
		{
			ShopPurchaseEvent shopPurchaseEvent = new ShopPurchaseEvent();
			shopPurchaseEvent.Player = GetPlayer();
			shopPurchaseEvent.Product = product;
			shopPurchaseEvent.Quantity = quantity;
			shopPurchaseEvent.Currency = currency;
			shopPurchaseEvent.Amount = amount;
			shopPurchaseEvent.IsGift = isGift;
			shopPurchaseEvent.Storefront = storefront;
			shopPurchaseEvent.PurchaseComplete = purchaseComplete;
			shopPurchaseEvent.StoreType = storeType;
			EnqueueMessage(shopPurchaseEvent);
		}

		public void SendShopStatus(string error, double timeInHubSec)
		{
			ShopStatus shopStatus = new ShopStatus();
			shopStatus.Player = GetPlayer();
			shopStatus.Error = error;
			shopStatus.TimeInHubSec = timeInHubSec;
			EnqueueMessage(shopStatus);
		}

		public void SendShopVisit(List<ShopCard> cards)
		{
			ShopVisit shopVisit = new ShopVisit();
			shopVisit.Player = GetPlayer();
			shopVisit.Cards = cards;
			EnqueueMessage(shopVisit);
		}

		public void SendSmartDeckCompleteFailed(int requestMessageSize)
		{
			SmartDeckCompleteFailed smartDeckCompleteFailed = new SmartDeckCompleteFailed();
			smartDeckCompleteFailed.Player = GetPlayer();
			smartDeckCompleteFailed.RequestMessageSize = requestMessageSize;
			EnqueueMessage(smartDeckCompleteFailed);
		}

		public void SendStartupAudioSettings(bool deviceMuted, float deviceVolume, float masterVolume, float musicVolume)
		{
			StartupAudioSettings startupAudioSettings = new StartupAudioSettings();
			startupAudioSettings.DeviceInfo = GetDeviceInfo();
			startupAudioSettings.DeviceMuted = deviceMuted;
			startupAudioSettings.DeviceVolume = deviceVolume;
			startupAudioSettings.MasterVolume = masterVolume;
			startupAudioSettings.MusicVolume = musicVolume;
			EnqueueMessage(startupAudioSettings);
		}

		public void SendTempAccountStoredInCloud(bool stored)
		{
			TempAccountStoredInCloud tempAccountStoredInCloud = new TempAccountStoredInCloud();
			tempAccountStoredInCloud.DeviceInfo = GetDeviceInfo();
			tempAccountStoredInCloud.Stored = stored;
			EnqueueMessage(tempAccountStoredInCloud);
		}

		public void SendThirdPartyPurchaseCompletedFail(string transactionId, string productId, string bpayProvider, string errorInfo)
		{
			ThirdPartyPurchaseCompletedFail thirdPartyPurchaseCompletedFail = new ThirdPartyPurchaseCompletedFail();
			thirdPartyPurchaseCompletedFail.Player = GetPlayer();
			thirdPartyPurchaseCompletedFail.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseCompletedFail.TransactionId = transactionId;
			thirdPartyPurchaseCompletedFail.ProductId = productId;
			thirdPartyPurchaseCompletedFail.BpayProvider = bpayProvider;
			thirdPartyPurchaseCompletedFail.ErrorInfo = errorInfo;
			EnqueueMessage(thirdPartyPurchaseCompletedFail);
		}

		public void SendThirdPartyPurchaseCompletedSuccess(string transactionId, string productId, string bpayProvider)
		{
			ThirdPartyPurchaseCompletedSuccess thirdPartyPurchaseCompletedSuccess = new ThirdPartyPurchaseCompletedSuccess();
			thirdPartyPurchaseCompletedSuccess.Player = GetPlayer();
			thirdPartyPurchaseCompletedSuccess.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseCompletedSuccess.TransactionId = transactionId;
			thirdPartyPurchaseCompletedSuccess.ProductId = productId;
			thirdPartyPurchaseCompletedSuccess.BpayProvider = bpayProvider;
			EnqueueMessage(thirdPartyPurchaseCompletedSuccess);
		}

		public void SendThirdPartyPurchaseDanglingReceiptFail(string transactionId, string productId, string provider, ThirdPartyPurchaseDanglingReceiptFail.FailureReason reason, string invalidData)
		{
			ThirdPartyPurchaseDanglingReceiptFail thirdPartyPurchaseDanglingReceiptFail = new ThirdPartyPurchaseDanglingReceiptFail();
			thirdPartyPurchaseDanglingReceiptFail.Player = GetPlayer();
			thirdPartyPurchaseDanglingReceiptFail.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseDanglingReceiptFail.TransactionId = transactionId;
			thirdPartyPurchaseDanglingReceiptFail.ProductId = productId;
			thirdPartyPurchaseDanglingReceiptFail.Provider = provider;
			thirdPartyPurchaseDanglingReceiptFail.Reason = reason;
			thirdPartyPurchaseDanglingReceiptFail.InvalidData = invalidData;
			EnqueueMessage(thirdPartyPurchaseDanglingReceiptFail);
		}

		public void SendThirdPartyPurchaseDeferred(string transactionId, string productId)
		{
			ThirdPartyPurchaseDeferred thirdPartyPurchaseDeferred = new ThirdPartyPurchaseDeferred();
			thirdPartyPurchaseDeferred.Player = GetPlayer();
			thirdPartyPurchaseDeferred.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseDeferred.TransactionId = transactionId;
			thirdPartyPurchaseDeferred.ProductId = productId;
			EnqueueMessage(thirdPartyPurchaseDeferred);
		}

		public void SendThirdPartyPurchaseMalformedData(string transactionId)
		{
			ThirdPartyPurchaseMalformedData thirdPartyPurchaseMalformedData = new ThirdPartyPurchaseMalformedData();
			thirdPartyPurchaseMalformedData.Player = GetPlayer();
			thirdPartyPurchaseMalformedData.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseMalformedData.TransactionId = transactionId;
			EnqueueMessage(thirdPartyPurchaseMalformedData);
		}

		public void SendThirdPartyPurchaseReceiptFound(string transactionId, string productId)
		{
			ThirdPartyPurchaseReceiptFound thirdPartyPurchaseReceiptFound = new ThirdPartyPurchaseReceiptFound();
			thirdPartyPurchaseReceiptFound.Player = GetPlayer();
			thirdPartyPurchaseReceiptFound.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseReceiptFound.TransactionId = transactionId;
			thirdPartyPurchaseReceiptFound.ProductId = productId;
			EnqueueMessage(thirdPartyPurchaseReceiptFound);
		}

		public void SendThirdPartyPurchaseReceiptNotFound(string transactionId, string productId)
		{
			ThirdPartyPurchaseReceiptNotFound thirdPartyPurchaseReceiptNotFound = new ThirdPartyPurchaseReceiptNotFound();
			thirdPartyPurchaseReceiptNotFound.Player = GetPlayer();
			thirdPartyPurchaseReceiptNotFound.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseReceiptNotFound.TransactionId = transactionId;
			thirdPartyPurchaseReceiptNotFound.ProductId = productId;
			EnqueueMessage(thirdPartyPurchaseReceiptNotFound);
		}

		public void SendThirdPartyPurchaseReceiptReceived(string transactionId, string productId)
		{
			ThirdPartyPurchaseReceiptReceived thirdPartyPurchaseReceiptReceived = new ThirdPartyPurchaseReceiptReceived();
			thirdPartyPurchaseReceiptReceived.Player = GetPlayer();
			thirdPartyPurchaseReceiptReceived.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseReceiptReceived.TransactionId = transactionId;
			thirdPartyPurchaseReceiptReceived.ProductId = productId;
			EnqueueMessage(thirdPartyPurchaseReceiptReceived);
		}

		public void SendThirdPartyPurchaseReceiptRequest(string transactionId, string productId)
		{
			ThirdPartyPurchaseReceiptRequest thirdPartyPurchaseReceiptRequest = new ThirdPartyPurchaseReceiptRequest();
			thirdPartyPurchaseReceiptRequest.Player = GetPlayer();
			thirdPartyPurchaseReceiptRequest.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseReceiptRequest.TransactionId = transactionId;
			thirdPartyPurchaseReceiptRequest.ProductId = productId;
			EnqueueMessage(thirdPartyPurchaseReceiptRequest);
		}

		public void SendThirdPartyPurchaseReceiptSubmitFail(string transactionId, string productId, string provider, ThirdPartyPurchaseReceiptSubmitFail.FailureReason reason, string invalidData)
		{
			ThirdPartyPurchaseReceiptSubmitFail thirdPartyPurchaseReceiptSubmitFail = new ThirdPartyPurchaseReceiptSubmitFail();
			thirdPartyPurchaseReceiptSubmitFail.Player = GetPlayer();
			thirdPartyPurchaseReceiptSubmitFail.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseReceiptSubmitFail.TransactionId = transactionId;
			thirdPartyPurchaseReceiptSubmitFail.ProductId = productId;
			thirdPartyPurchaseReceiptSubmitFail.Provider = provider;
			thirdPartyPurchaseReceiptSubmitFail.Reason = reason;
			thirdPartyPurchaseReceiptSubmitFail.InvalidData = invalidData;
			EnqueueMessage(thirdPartyPurchaseReceiptSubmitFail);
		}

		public void SendThirdPartyPurchaseStart(string transactionId, string productId, string bpayProvider)
		{
			ThirdPartyPurchaseStart thirdPartyPurchaseStart = new ThirdPartyPurchaseStart();
			thirdPartyPurchaseStart.Player = GetPlayer();
			thirdPartyPurchaseStart.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseStart.TransactionId = transactionId;
			thirdPartyPurchaseStart.ProductId = productId;
			thirdPartyPurchaseStart.BpayProvider = bpayProvider;
			EnqueueMessage(thirdPartyPurchaseStart);
		}

		public void SendThirdPartyPurchaseSubmitResponseDeviceNotification(string transactionId, bool success)
		{
			ThirdPartyPurchaseSubmitResponseDeviceNotification thirdPartyPurchaseSubmitResponseDeviceNotification = new ThirdPartyPurchaseSubmitResponseDeviceNotification();
			thirdPartyPurchaseSubmitResponseDeviceNotification.Player = GetPlayer();
			thirdPartyPurchaseSubmitResponseDeviceNotification.DeviceInfo = GetDeviceInfo();
			thirdPartyPurchaseSubmitResponseDeviceNotification.TransactionId = transactionId;
			thirdPartyPurchaseSubmitResponseDeviceNotification.Success = success;
			EnqueueMessage(thirdPartyPurchaseSubmitResponseDeviceNotification);
		}

		public void SendThirdPartyReceiptConsumed(string transactionId)
		{
			ThirdPartyReceiptConsumed thirdPartyReceiptConsumed = new ThirdPartyReceiptConsumed();
			thirdPartyReceiptConsumed.Player = GetPlayer();
			thirdPartyReceiptConsumed.DeviceInfo = GetDeviceInfo();
			thirdPartyReceiptConsumed.TransactionId = transactionId;
			EnqueueMessage(thirdPartyReceiptConsumed);
		}

		public void SendThirdPartyUserIdUpdated(bool validChange)
		{
			ThirdPartyUserIdUpdated thirdPartyUserIdUpdated = new ThirdPartyUserIdUpdated();
			thirdPartyUserIdUpdated.Player = GetPlayer();
			thirdPartyUserIdUpdated.DeviceInfo = GetDeviceInfo();
			thirdPartyUserIdUpdated.ValidChange = validChange;
			EnqueueMessage(thirdPartyUserIdUpdated);
		}

		public void SendVirtualCurrencyTransaction(string applicationId, string itemId, string itemCost, string itemQuantity, string currency, string payload)
		{
			VirtualCurrencyTransaction virtualCurrencyTransaction = new VirtualCurrencyTransaction();
			virtualCurrencyTransaction.ApplicationId = applicationId;
			virtualCurrencyTransaction.ItemId = itemId;
			virtualCurrencyTransaction.ItemCost = itemCost;
			virtualCurrencyTransaction.ItemQuantity = itemQuantity;
			virtualCurrencyTransaction.Currency = currency;
			virtualCurrencyTransaction.Payload = payload;
			EnqueueMessage(virtualCurrencyTransaction);
		}

		public void SendWebLoginError()
		{
			WebLoginError webLoginError = new WebLoginError();
			webLoginError.Player = GetPlayer();
			webLoginError.DeviceInfo = GetDeviceInfo();
			EnqueueMessage(webLoginError);
		}

		public void SendWelcomeQuestsAcknowledged(float questAckDuration)
		{
			WelcomeQuestsAcknowledged welcomeQuestsAcknowledged = new WelcomeQuestsAcknowledged();
			welcomeQuestsAcknowledged.Player = GetPlayer();
			welcomeQuestsAcknowledged.DeviceInfo = GetDeviceInfo();
			welcomeQuestsAcknowledged.QuestAckDuration = questAckDuration;
			EnqueueMessage(welcomeQuestsAcknowledged);
		}

		public void SendAttributionInstall(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string referrer, string appleSearchAdsJson, int appleSearchAdsErrorCode, IdentifierInfo identifier)
		{
			AttributionInstall attributionInstall = new AttributionInstall();
			attributionInstall.ApplicationId = applicationId;
			attributionInstall.DeviceType = deviceType;
			attributionInstall.FirstInstallDate = firstInstallDate;
			attributionInstall.BundleId = bundleId;
			attributionInstall.Referrer = referrer;
			attributionInstall.AppleSearchAdsJson = appleSearchAdsJson;
			attributionInstall.AppleSearchAdsErrorCode = appleSearchAdsErrorCode;
			attributionInstall.Identifier = identifier;
			EnqueueMessage(attributionInstall);
		}

		public void SendAttributionLaunch(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, int counter)
		{
			AttributionLaunch attributionLaunch = new AttributionLaunch();
			attributionLaunch.ApplicationId = applicationId;
			attributionLaunch.DeviceType = deviceType;
			attributionLaunch.FirstInstallDate = firstInstallDate;
			attributionLaunch.BundleId = bundleId;
			attributionLaunch.Counter = counter;
			EnqueueMessage(attributionLaunch);
		}

		public void SendApkInstallFailure(string updatedVersion, string reason)
		{
			ApkInstallFailure apkInstallFailure = new ApkInstallFailure();
			apkInstallFailure.UpdatedVersion = updatedVersion;
			apkInstallFailure.Reason = reason;
			EnqueueMessage(apkInstallFailure);
		}

		public void SendApkInstallSuccess(string updatedVersion, float availableSpaceMB, float elapsedSeconds)
		{
			ApkInstallSuccess apkInstallSuccess = new ApkInstallSuccess();
			apkInstallSuccess.UpdatedVersion = updatedVersion;
			apkInstallSuccess.AvailableSpaceMB = availableSpaceMB;
			apkInstallSuccess.ElapsedSeconds = elapsedSeconds;
			EnqueueMessage(apkInstallSuccess);
		}

		public void SendApkUpdate(int installedVersion, int assetVersion, int agentVersion)
		{
			ApkUpdate apkUpdate = new ApkUpdate();
			apkUpdate.InstalledVersion = installedVersion;
			apkUpdate.AssetVersion = assetVersion;
			apkUpdate.AgentVersion = agentVersion;
			EnqueueMessage(apkUpdate);
		}

		public void SendCertificateRejected()
		{
			CertificateRejected message = new CertificateRejected();
			EnqueueMessage(message);
		}

		public void SendDeviceInfo(string androidId, string androidModel, uint androidSdkVersion, bool isConnectedToWifi, string gpuTextureFormat, string locale, string bnetRegion)
		{
			Blizzard.Telemetry.WTCG.NGDP.DeviceInfo deviceInfo = new Blizzard.Telemetry.WTCG.NGDP.DeviceInfo();
			deviceInfo.AndroidId = androidId;
			deviceInfo.AndroidModel = androidModel;
			deviceInfo.AndroidSdkVersion = androidSdkVersion;
			deviceInfo.IsConnectedToWifi = isConnectedToWifi;
			deviceInfo.GpuTextureFormat = gpuTextureFormat;
			deviceInfo.Locale = locale;
			deviceInfo.BnetRegion = bnetRegion;
			EnqueueMessage(deviceInfo);
		}

		public void SendNotEnoughSpaceError(ulong availableSpace, ulong expectedOrgBytes, string filesDir)
		{
			NotEnoughSpaceError notEnoughSpaceError = new NotEnoughSpaceError();
			notEnoughSpaceError.AvailableSpace = availableSpace;
			notEnoughSpaceError.ExpectedOrgBytes = expectedOrgBytes;
			notEnoughSpaceError.FilesDir = filesDir;
			EnqueueMessage(notEnoughSpaceError);
		}

		public void SendNoWifi(string updatedVersion, float availableSpaceMB, float elapsedSeconds)
		{
			NoWifi noWifi = new NoWifi();
			noWifi.UpdatedVersion = updatedVersion;
			noWifi.AvailableSpaceMB = availableSpaceMB;
			noWifi.ElapsedSeconds = elapsedSeconds;
			EnqueueMessage(noWifi);
		}

		public void SendOpeningAppStore(string updatedVersion, float availableSpaceMB, float elapsedSeconds)
		{
			OpeningAppStore openingAppStore = new OpeningAppStore();
			openingAppStore.UpdatedVersion = updatedVersion;
			openingAppStore.AvailableSpaceMB = availableSpaceMB;
			openingAppStore.ElapsedSeconds = elapsedSeconds;
			EnqueueMessage(openingAppStore);
		}

		public void SendUncaughtException(string stackTrace, string androidModel, uint androidSdkVersion)
		{
			UncaughtException ex = new UncaughtException();
			ex.StackTrace = stackTrace;
			ex.AndroidModel = androidModel;
			ex.AndroidSdkVersion = androidSdkVersion;
			EnqueueMessage(ex);
		}

		public void SendUpdateError(uint errorCode, float elapsedSeconds)
		{
			UpdateError updateError = new UpdateError();
			updateError.ErrorCode = errorCode;
			updateError.ElapsedSeconds = elapsedSeconds;
			EnqueueMessage(updateError);
		}

		public void SendUpdateFinished(string updatedVersion, float availableSpaceMB, float elapsedSeconds)
		{
			UpdateFinished updateFinished = new UpdateFinished();
			updateFinished.UpdatedVersion = updatedVersion;
			updateFinished.AvailableSpaceMB = availableSpaceMB;
			updateFinished.ElapsedSeconds = elapsedSeconds;
			EnqueueMessage(updateFinished);
		}

		public void SendUpdateProgress(float duration, long realDownloadBytes, long expectedDownloadBytes)
		{
			UpdateProgress updateProgress = new UpdateProgress();
			updateProgress.DeviceInfo = GetDeviceInfo();
			updateProgress.Duration = duration;
			updateProgress.RealDownloadBytes = realDownloadBytes;
			updateProgress.ExpectedDownloadBytes = expectedDownloadBytes;
			EnqueueMessage(updateProgress);
		}

		public void SendUpdateStarted(string installedVersion, string textureFormat, string dataPath, float availableSpaceMB)
		{
			UpdateStarted updateStarted = new UpdateStarted();
			updateStarted.InstalledVersion = installedVersion;
			updateStarted.TextureFormat = textureFormat;
			updateStarted.DataPath = dataPath;
			updateStarted.AvailableSpaceMB = availableSpaceMB;
			EnqueueMessage(updateStarted);
		}

		public void SendUsingCellularData(string updatedVersion, float availableSpaceMB, float elapsedSeconds)
		{
			UsingCellularData usingCellularData = new UsingCellularData();
			usingCellularData.UpdatedVersion = updatedVersion;
			usingCellularData.AvailableSpaceMB = availableSpaceMB;
			usingCellularData.ElapsedSeconds = elapsedSeconds;
			EnqueueMessage(usingCellularData);
		}

		public void SendVersionError(uint errorCode, uint agentState, string languages, string region, string branch, string additionalTags)
		{
			VersionError versionError = new VersionError();
			versionError.ErrorCode = errorCode;
			versionError.AgentState = agentState;
			versionError.Languages = languages;
			versionError.Region = region;
			versionError.Branch = branch;
			versionError.AdditionalTags = additionalTags;
			EnqueueMessage(versionError);
		}

		public void SendVersionFinished(string currentVersion, string liveVersion)
		{
			VersionFinished versionFinished = new VersionFinished();
			versionFinished.CurrentVersion = currentVersion;
			versionFinished.LiveVersion = liveVersion;
			EnqueueMessage(versionFinished);
		}

		public void SendVersionStarted(int dummy)
		{
			VersionStarted versionStarted = new VersionStarted();
			versionStarted.Dummy = dummy;
			EnqueueMessage(versionStarted);
		}

		public void Initialize(Service telemetryService)
		{
			if (telemetryService == null)
			{
				throw new ArgumentNullException("telemetryService");
			}
			m_telemetryService = telemetryService;
			m_initialized = true;
			if (m_deferredMessages.Any())
			{
				DeferredMessage[] array;
				lock (m_deferredMessageLock)
				{
					array = m_deferredMessages.ToArray();
					m_deferredMessages.Clear();
				}
				DeferredMessage[] array2 = array;
				foreach (DeferredMessage message in array2)
				{
					EnqueueMessage(message);
				}
			}
		}

		public bool IsInitialized()
		{
			return m_initialized;
		}

		public void Disable()
		{
			m_disabled = true;
		}

		public void OnUpdate()
		{
			if (m_initialized && !m_disabled && m_updateHandler != null)
			{
				m_updateHandler();
			}
		}

		public void RegisterUpdateHandler(Action updateHandler)
		{
			if (updateHandler == null)
			{
				throw new ArgumentNullException("updateHandler");
			}
			m_updateHandler = updateHandler;
		}

		public void Shutdown()
		{
			m_initialized = false;
			(m_telemetryService as IDisposable)?.Dispose();
			m_telemetryService = null;
		}

		public long EnqueueMessage(string packageName, string messageName, byte[] data, MessageOptions options = null)
		{
			if (m_disabled || data.Length == 0)
			{
				return 0L;
			}
			if (!m_initialized)
			{
				lock (m_deferredMessageLock)
				{
					m_deferredMessages.Add(new DeferredMessage
					{
						PackageName = packageName,
						MessageName = messageName,
						Data = data,
						Options = options
					});
				}
				return 0L;
			}
			return m_telemetryService.Enqueue(packageName, messageName, data, options);
		}

		public void SendConnectSuccess(string name, string host = null, uint? port = null)
		{
			ConnectSuccess connectSuccess = new ConnectSuccess();
			connectSuccess.Name = name;
			connectSuccess.Host = host;
			connectSuccess.Port = port;
			EnqueueMessage(connectSuccess);
		}

		public void SendConnectFail(string name, string reason, string host = null, uint? port = null)
		{
			ConnectFail connectFail = new ConnectFail();
			connectFail.Name = name;
			connectFail.Reason = reason;
			connectFail.Host = host;
			connectFail.Port = port;
			EnqueueMessage(connectFail);
		}

		public void SendDisconnect(string name, Disconnect.Reason reason, string description = null, string host = null, uint? port = null)
		{
			Disconnect disconnect = new Disconnect();
			disconnect.Reason_ = reason;
			disconnect.Name = name;
			disconnect.Description = description;
			disconnect.Host = host;
			disconnect.Port = port;
			EnqueueMessage(disconnect);
		}

		public void SendFindGameResult(FindGameResult message)
		{
			message.DeviceInfo = GetDeviceInfo();
			message.Player = GetPlayer();
			EnqueueMessage(message);
		}

		public void SendConnectToGameServer(ConnectToGameServer message)
		{
			message.DeviceInfo = GetDeviceInfo();
			message.Player = GetPlayer();
			EnqueueMessage(message);
		}

		public void SendTcpQualitySample(string address4, uint port, float sampleTimeMs, uint bytesSent, uint bytesReceived, uint messagesSent, uint messagesReceived)
		{
			TcpQualitySample tcpQualitySample = new TcpQualitySample();
			tcpQualitySample.Address4 = address4;
			tcpQualitySample.Port = port;
			tcpQualitySample.SampleTimeMs = sampleTimeMs;
			tcpQualitySample.BytesSent = bytesSent;
			tcpQualitySample.BytesReceived = bytesReceived;
			tcpQualitySample.MessagesSent = messagesSent;
			tcpQualitySample.MessagesReceived = messagesReceived;
			EnqueueMessage(tcpQualitySample);
		}

		public void SendDevLogTelemetry(string category, string details)
		{
		}

		public long EnqueueMessage(IProtoBuf message, MessageOptions options = null)
		{
			if (m_disabled || message == null)
			{
				return 0L;
			}
			MemoryStream memoryStream = new MemoryStream();
			message.Serialize(memoryStream);
			return EnqueueMessage(message.GetType(), memoryStream.ToArray(), options);
		}

		private long EnqueueMessage(Blizzard.Proto.IProtoBuf message, MessageOptions options = null)
		{
			if (m_disabled || message == null)
			{
				return 0L;
			}
			if (!m_initialized)
			{
				lock (m_deferredMessageLock)
				{
					m_deferredMessages.Add(new DeferredMessage
					{
						ProtoMessage = message,
						Options = options
					});
				}
				return 0L;
			}
			return m_telemetryService.Enqueue(message, options);
		}

		private long EnqueueMessage(Type messageType, byte[] data, MessageOptions options = null)
		{
			if (m_disabled || data.Length == 0)
			{
				return 0L;
			}
			if (!m_initialized)
			{
				lock (m_deferredMessageLock)
				{
					m_deferredMessages.Add(new DeferredMessage
					{
						MessageType = messageType,
						Data = data,
						Options = options
					});
				}
				return 0L;
			}
			int num = messageType.FullName.LastIndexOf('.');
			return m_telemetryService.Enqueue(messageType.FullName.Substring(0, num), messageType.FullName.Substring(num + 1), data, options);
		}

		private void EnqueueMessage(DeferredMessage message)
		{
			if (message.ProtoMessage != null)
			{
				EnqueueMessage(message.ProtoMessage, message.Options);
			}
			else if (string.IsNullOrEmpty(message.PackageName) || string.IsNullOrEmpty(message.MessageName))
			{
				EnqueueMessage(message.MessageType, message.Data, message.Options);
			}
			else
			{
				EnqueueMessage(message.PackageName, message.MessageName, message.Data, message.Options);
			}
		}

		private Blizzard.Telemetry.WTCG.Client.DeviceInfo GetDeviceInfo()
		{
			if (m_deviceInfo == null)
			{
				int oS = (int)PlatformSettings.OS;
				int screen = (int)PlatformSettings.Screen;
				m_deviceInfo = new Blizzard.Telemetry.WTCG.Client.DeviceInfo
				{
					Os = (Blizzard.Telemetry.WTCG.Client.DeviceInfo.OSCategory)oS,
					OsVersion = SystemInfo.operatingSystem,
					Model = PlatformSettings.DeviceModel,
					Screen = (Blizzard.Telemetry.WTCG.Client.DeviceInfo.ScreenCategory)screen,
					DroidTextureCompression = ((PlatformSettings.OS == OSCategory.Android) ? AndroidDeviceSettings.Get().InstalledTexture : null)
				};
			}
			m_deviceInfo.ConnectionType_ = GetConnectionType();
			return m_deviceInfo;
		}

		private Blizzard.Telemetry.WTCG.Client.Player GetPlayer()
		{
			return new Blizzard.Telemetry.WTCG.Client.Player
			{
				BattleNetIdLo = (long)BnetUtils.TryGetBnetAccountId().GetValueOrDefault(),
				BnetRegion = BnetUtils.TryGetBnetRegion().GetValueOrDefault(constants.BnetRegion.REGION_UNINITIALIZED).ToString(),
				Locale = Localization.GetLocaleName(),
				GameAccountId = (long)BnetUtils.TryGetGameAccountId().GetValueOrDefault()
			};
		}

		private static Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType GetConnectionType()
		{
			switch (Application.internetReachability)
			{
			case NetworkReachability.ReachableViaCarrierDataNetwork:
				return Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType.CELLULAR;
			case NetworkReachability.NotReachable:
				return Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType.UNKNOWN;
			default:
				if (PlatformSettings.OS != OSCategory.Android && PlatformSettings.OS != OSCategory.iOS)
				{
					return Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType.WIRED;
				}
				return Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType.WIFI;
			}
		}
	}
}
