using System;
using System.Collections.Generic;
using Blizzard.Telemetry;
using Blizzard.Telemetry.Standard.Network;
using Blizzard.Telemetry.WTCG.Client;

namespace HearthstoneTelemetry
{
	public interface ITelemetryClient
	{
		void SendAccountHealUpResult(AccountHealUpResult.HealUpResult result, int errorCode);

		void SendAppInitialized(string testType, float duration, string clientChangelist);

		void SendAppPaused(bool pauseStatus, float pauseTime);

		void SendAppStart(string testType, float duration, string clientChangelist);

		void SendAssetNotFound(string assetType, string assetGuid, string filePath, string legacyName);

		void SendAssetOrphaned(string filePath, string handleOwner, string handleType);

		void SendAttackInputMethod(long totalNumAttacks, long totalClickAttacks, int percentClickAttacks, long totalDragAttacks, int percentDragAttacks);

		void SendAttributionContentUnlocked(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string contentId);

		void SendAttributionFirstLogin(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier);

		void SendAttributionGameRoundEnd(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string gameMode, string result, FormatType formatType);

		void SendAttributionGameRoundStart(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string gameMode, FormatType formatType);

		void SendAttributionHeadlessAccountCreated(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier);

		void SendAttributionHeadlessAccountHealedUp(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string temporaryGameAccountId, IdentifierInfo identifier);

		void SendAttributionItemTransaction(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string itemId, int quantity);

		void SendAttributionLogin(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier);

		void SendAttributionPurchase(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string purchaseType, string transactionId, int quantity, List<AttributionPurchase.PaymentInfo> payments, float amount, string currency);

		void SendAttributionRegistration(string applicationId, string deviceType, ulong firstInstallDate, string bundleId);

		void SendAttributionScenarioResult(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, int scenarioId, string result, int bossId, IdentifierInfo identifier);

		void SendAttributionVirtualCurrencyTransaction(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, float amount, string currency);

		void SendBlizzardCheckoutGeneric(string messageKey, string messageValue);

		void SendBlizzardCheckoutInitializationResult(bool success, string failureReason, string failureDetails);

		void SendBlizzardCheckoutIsReady(double secondsShown, bool isReady);

		void SendBlizzardCheckoutPurchaseCancel();

		void SendBlizzardCheckoutPurchaseCompletedFailure(string transactionId, string productId, string currency, List<string> errorCodes);

		void SendBlizzardCheckoutPurchaseCompletedSuccess(string transactionId, string productId, string currency);

		void SendBlizzardCheckoutPurchaseStart(string transactionId, string productId, string currency);

		void SendBoxInteractable(string testType, float duration, string clientChangelist);

		void SendButtonPressed(string buttonName);

		void SendChangePackQuantity(int boosterId);

		void SendCinematic(bool begin, float duration);

		void SendClickRecruitAFriend();

		void SendClientReset(bool forceLogin, bool forceNoAccountTutorial);

		void SendCollectionLeftRightClick(CollectionLeftRightClick.Target target_);

		void SendConnectToGameServer(uint resultBnetCode, string resultBnetCodeString, long timeSpentMilliseconds, GameSessionInfo gameSessionInfo);

		void SendContentConnectFailedToConnect(string url, int httpErrorcode, int serverErrorcode);

		void SendCrmEvent(string eventName, string eventPayload, string applicationId);

		void SendDataUpdateFailed(float duration, long realDownloadBytes, long expectedDownloadBytes, int errorCode);

		void SendDataUpdateFinished(float duration, long realDownloadBytes, long expectedDownloadBytes);

		void SendDataUpdateProgress(float duration, long realDownloadBytes, long expectedDownloadBytes);

		void SendDataUpdateStarted();

		void SendDeckCopied(long deckId, string deckHash);

		void SendDeckPickerToCollection(DeckPickerToCollection.Path path_);

		void SendDeckUpdateResponseInfo(float duration);

		void SendDeepLinkExecuted(string deepLink, string source, bool completed);

		void SendDeviceInfo(DeviceInfo.OSCategory os, string osVersion, string model, DeviceInfo.ScreenCategory screen, DeviceInfo.ConnectionType connectionType_, string droidTextureCompression);

		void SendDeviceMuteChanged(bool muted);

		void SendDeviceVolumeChanged(float oldVolume, float newVolume);

		void SendDragDropCancelPlayCard(long scenarioId, string cardType);

		void SendEndGameScreenInit(float elapsedTime, int medalInfoRetryCount, bool medalInfoRetriesTimedOut, bool showRankedReward, bool showCardBackProgress, int otherRewardCount);

		void SendFatalBattleNetError(int errorCode, string description);

		void SendFatalError(string reason);

		void SendFindGameResult(uint resultCode, string resultCodeString, long timeSpentMilliseconds, GameSessionInfo gameSessionInfo);

		void SendFlowPerformance(string uniqueId, Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType flowType_, float averageFps, float duration, float fpsWarningsThreshold, int fpsWarningsTotalOccurences, float fpsWarningsTotalTime, float fpsWarningsAverageTime, float fpsWarningsMaxTime);

		void SendFlowPerformanceBattlegrounds(string flowId, string gameUuid, int totalRounds);

		void SendFlowPerformanceGame(string flowId, string uuid, GameType gameType, FormatType formatType, int boardId, int scenarioId);

		void SendFlowPerformanceShop(string flowId, Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType shopType_);

		void SendFriendsListView(string currentScene);

		void SendGameRoundStartAudioSettings(bool deviceMuted, float deviceVolume, float masterVolume, float musicVolume);

		void SendIgnorableBattleNetError(int errorCode, string description);

		void SendIKSClicked(string iksCampaignName, string iksMediaUrl);

		void SendIKSIgnored(string iksCampaignName, string iksMediaUrl);

		void SendInGameMessageAction(string messageType, string title, InGameMessageAction.ActionType action, int viewCounts, string uid);

		void SendInGameMessageHandlerCalled(string messageType, string title, string uid);

		void SendInitialClientStateOutOfOrder(int countNotificationsAchieve, int countNotificationsNotice, int countNotificationsCollection, int countNotificationsCurrency, int countNotificationsBooster, int countNotificationsHeroxp, int countNotificationsPlayerRecord, int countNotificationsArenaSession, int countNotificationsCardBack);

		void SendJobBegin(string jobId, string testType);

		void SendJobExceededLimit(string jobId, long jobDuration, string testType);

		void SendJobFinishFailure(string jobId, string jobFailureReason, string testType, string clientChangelist, float duration);

		void SendJobFinishSuccess(string jobId, string testType, string clientChangelist, float duration);

		void SendJobStepExceededLimit(string jobId, long jobDuration, string testType);

		void SendLanguageChanged(string previousLanguage, string nextLanguage);

		void SendLargeThirdPartyReceiptFound(long receiptSize);

		void SendLiveIssue(string category, string details);

		void SendLocaleDataUpdateFailed(float duration, long realDownloadBytes, long expectedDownloadBytes, int errorCode);

		void SendLocaleDataUpdateFinished(float duration, long realDownloadBytes, long expectedDownloadBytes);

		void SendLocaleDataUpdateStarted(string locale);

		void SendLoginTokenFetchResult(LoginTokenFetchResult.TokenFetchResult result);

		void SendManaFilterToggleOff();

		void SendMASDKAuthResult(MASDKAuthResult.AuthResult result, int errorCode, string source);

		void SendMASDKGuestCreationFailure(int errorCode);

		void SendMASDKImportResult(MASDKImportResult.ImportResult result, MASDKImportResult.ImportType importType_, int errorCode);

		void SendMasterVolumeChanged(float oldVolume, float newVolume);

		void SendMissingAssetError(string missingAssetPath, string assetContext);

		void SendMusicVolumeChanged(float oldVolume, float newVolume);

		void SendNetworkError(NetworkError.ErrorType errorType_, string description, int errorCode);

		void SendNetworkUnreachableRecovered(int outageSeconds);

		void SendPackOpenToStore(PackOpenToStore.Path path_);

		void SendPresenceChanged(PresenceStatus newPresenceStatus, PresenceStatus prevPresenceStatus, long millisecondsSincePrev);

		void SendPreviousInstanceStatus(int totalCrashCount, int totalExceptionCount, int lowMemoryWarningCount, int crashInARowCount, int sameExceptionCount, bool crashed, string exceptionHash);

		void SendPurchaseCancelClicked(long pmtProductId);

		void SendPurchasePayNowClicked(long pmtProductId);

		void SendPushEvent(string campaignId, string eventPayload, string applicationId);

		void SendPushRegistration(string pushId, int utcOffset, string timezone, string applicationId, string language, string os, string osVersion, string deviceHeight, string deviceWidth, string deviceDpi);

		void SendRealMoneyTransaction(string applicationId, string appStore, string receipt, string receiptSignature, string productId, string itemCost, string itemQuantity, string localCurrency, string transactionId);

		void SendReconnectSuccess(float disconnectDuration, float reconnectDuration, string reconnectType);

		void SendReconnectTimeout(string reconnectType);

		void SendRepairPrestep(int doubletapFingers, int locales);

		void SendRestartDueToPlayerMigration();

		void SendReturningPlayerDeckNotCreated(uint aBGroup);

		void SendRuntimeUpdate(float duration, RuntimeUpdate.Intention intention_);

		void SendSessionEnd(string applicationId);

		void SendSessionStart(string eventPayload, string applicationId);

		void SendShopBalanceAvailable(List<Balance> balances);

		void SendShopCardClick(ShopCard shopcard);

		void SendShopPurchaseEvent(Product product, int quantity, string currency, double amount, bool isGift, string storefront, bool purchaseComplete, string storeType);

		void SendShopStatus(string error, double timeInHubSec);

		void SendShopVisit(List<ShopCard> cards);

		void SendSmartDeckCompleteFailed(int requestMessageSize);

		void SendStartupAudioSettings(bool deviceMuted, float deviceVolume, float masterVolume, float musicVolume);

		void SendTempAccountStoredInCloud(bool stored);

		void SendThirdPartyPurchaseCompletedFail(string transactionId, string productId, string bpayProvider, string errorInfo);

		void SendThirdPartyPurchaseCompletedSuccess(string transactionId, string productId, string bpayProvider);

		void SendThirdPartyPurchaseDanglingReceiptFail(string transactionId, string productId, string provider, ThirdPartyPurchaseDanglingReceiptFail.FailureReason reason, string invalidData);

		void SendThirdPartyPurchaseDeferred(string transactionId, string productId);

		void SendThirdPartyPurchaseMalformedData(string transactionId);

		void SendThirdPartyPurchaseReceiptFound(string transactionId, string productId);

		void SendThirdPartyPurchaseReceiptNotFound(string transactionId, string productId);

		void SendThirdPartyPurchaseReceiptReceived(string transactionId, string productId);

		void SendThirdPartyPurchaseReceiptRequest(string transactionId, string productId);

		void SendThirdPartyPurchaseReceiptSubmitFail(string transactionId, string productId, string provider, ThirdPartyPurchaseReceiptSubmitFail.FailureReason reason, string invalidData);

		void SendThirdPartyPurchaseStart(string transactionId, string productId, string bpayProvider);

		void SendThirdPartyPurchaseSubmitResponseDeviceNotification(string transactionId, bool success);

		void SendThirdPartyReceiptConsumed(string transactionId);

		void SendThirdPartyUserIdUpdated(bool validChange);

		void SendVirtualCurrencyTransaction(string applicationId, string itemId, string itemCost, string itemQuantity, string currency, string payload);

		void SendWebLoginError();

		void SendWelcomeQuestsAcknowledged(float questAckDuration);

		void SendAttributionInstall(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string referrer, string appleSearchAdsJson, int appleSearchAdsErrorCode, IdentifierInfo identifier);

		void SendAttributionLaunch(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, int counter);

		void SendApkInstallFailure(string updatedVersion, string reason);

		void SendApkInstallSuccess(string updatedVersion, float availableSpaceMB, float elapsedSeconds);

		void SendApkUpdate(int installedVersion, int assetVersion, int agentVersion);

		void SendCertificateRejected();

		void SendDeviceInfo(string androidId, string androidModel, uint androidSdkVersion, bool isConnectedToWifi, string gpuTextureFormat, string locale, string bnetRegion);

		void SendNotEnoughSpaceError(ulong availableSpace, ulong expectedOrgBytes, string filesDir);

		void SendNoWifi(string updatedVersion, float availableSpaceMB, float elapsedSeconds);

		void SendOpeningAppStore(string updatedVersion, float availableSpaceMB, float elapsedSeconds);

		void SendUncaughtException(string stackTrace, string androidModel, uint androidSdkVersion);

		void SendUpdateError(uint errorCode, float elapsedSeconds);

		void SendUpdateFinished(string updatedVersion, float availableSpaceMB, float elapsedSeconds);

		void SendUpdateProgress(float duration, long realDownloadBytes, long expectedDownloadBytes);

		void SendUpdateStarted(string installedVersion, string textureFormat, string dataPath, float availableSpaceMB);

		void SendUsingCellularData(string updatedVersion, float availableSpaceMB, float elapsedSeconds);

		void SendVersionError(uint errorCode, uint agentState, string languages, string region, string branch, string additionalTags);

		void SendVersionFinished(string currentVersion, string liveVersion);

		void SendVersionStarted(int dummy);

		void Initialize(Service telemetryService);

		bool IsInitialized();

		void Disable();

		void OnUpdate();

		void Shutdown();

		void RegisterUpdateHandler(Action updateHandler);

		long EnqueueMessage(IProtoBuf message, MessageOptions options = null);

		long EnqueueMessage(string packageName, string messageName, byte[] data, MessageOptions options = null);

		void SendConnectSuccess(string name, string host = null, uint? port = null);

		void SendConnectFail(string name, string reason, string host = null, uint? port = null);

		void SendDisconnect(string name, Disconnect.Reason reason, string description = null, string host = null, uint? port = null);

		void SendFindGameResult(FindGameResult message);

		void SendConnectToGameServer(ConnectToGameServer message);

		void SendTcpQualitySample(string address4, uint port, float sampleTimeMs, uint bytesSent, uint bytesReceived, uint messagesSent, uint messagesReceived);

		void SendDevLogTelemetry(string category, string details);
	}
}
