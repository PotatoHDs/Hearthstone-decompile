using System;
using System.Collections.Generic;
using Blizzard.Telemetry;
using Blizzard.Telemetry.Standard.Network;
using Blizzard.Telemetry.WTCG.Client;

namespace HearthstoneTelemetry
{
	// Token: 0x02000B49 RID: 2889
	public interface ITelemetryClient
	{
		// Token: 0x0600990A RID: 39178
		void SendAccountHealUpResult(AccountHealUpResult.HealUpResult result, int errorCode);

		// Token: 0x0600990B RID: 39179
		void SendAppInitialized(string testType, float duration, string clientChangelist);

		// Token: 0x0600990C RID: 39180
		void SendAppPaused(bool pauseStatus, float pauseTime);

		// Token: 0x0600990D RID: 39181
		void SendAppStart(string testType, float duration, string clientChangelist);

		// Token: 0x0600990E RID: 39182
		void SendAssetNotFound(string assetType, string assetGuid, string filePath, string legacyName);

		// Token: 0x0600990F RID: 39183
		void SendAssetOrphaned(string filePath, string handleOwner, string handleType);

		// Token: 0x06009910 RID: 39184
		void SendAttackInputMethod(long totalNumAttacks, long totalClickAttacks, int percentClickAttacks, long totalDragAttacks, int percentDragAttacks);

		// Token: 0x06009911 RID: 39185
		void SendAttributionContentUnlocked(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string contentId);

		// Token: 0x06009912 RID: 39186
		void SendAttributionFirstLogin(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier);

		// Token: 0x06009913 RID: 39187
		void SendAttributionGameRoundEnd(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string gameMode, string result, FormatType formatType);

		// Token: 0x06009914 RID: 39188
		void SendAttributionGameRoundStart(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string gameMode, FormatType formatType);

		// Token: 0x06009915 RID: 39189
		void SendAttributionHeadlessAccountCreated(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier);

		// Token: 0x06009916 RID: 39190
		void SendAttributionHeadlessAccountHealedUp(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string temporaryGameAccountId, IdentifierInfo identifier);

		// Token: 0x06009917 RID: 39191
		void SendAttributionItemTransaction(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string itemId, int quantity);

		// Token: 0x06009918 RID: 39192
		void SendAttributionLogin(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier);

		// Token: 0x06009919 RID: 39193
		void SendAttributionPurchase(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string purchaseType, string transactionId, int quantity, List<AttributionPurchase.PaymentInfo> payments, float amount, string currency);

		// Token: 0x0600991A RID: 39194
		void SendAttributionRegistration(string applicationId, string deviceType, ulong firstInstallDate, string bundleId);

		// Token: 0x0600991B RID: 39195
		void SendAttributionScenarioResult(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, int scenarioId, string result, int bossId, IdentifierInfo identifier);

		// Token: 0x0600991C RID: 39196
		void SendAttributionVirtualCurrencyTransaction(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, float amount, string currency);

		// Token: 0x0600991D RID: 39197
		void SendBlizzardCheckoutGeneric(string messageKey, string messageValue);

		// Token: 0x0600991E RID: 39198
		void SendBlizzardCheckoutInitializationResult(bool success, string failureReason, string failureDetails);

		// Token: 0x0600991F RID: 39199
		void SendBlizzardCheckoutIsReady(double secondsShown, bool isReady);

		// Token: 0x06009920 RID: 39200
		void SendBlizzardCheckoutPurchaseCancel();

		// Token: 0x06009921 RID: 39201
		void SendBlizzardCheckoutPurchaseCompletedFailure(string transactionId, string productId, string currency, List<string> errorCodes);

		// Token: 0x06009922 RID: 39202
		void SendBlizzardCheckoutPurchaseCompletedSuccess(string transactionId, string productId, string currency);

		// Token: 0x06009923 RID: 39203
		void SendBlizzardCheckoutPurchaseStart(string transactionId, string productId, string currency);

		// Token: 0x06009924 RID: 39204
		void SendBoxInteractable(string testType, float duration, string clientChangelist);

		// Token: 0x06009925 RID: 39205
		void SendButtonPressed(string buttonName);

		// Token: 0x06009926 RID: 39206
		void SendChangePackQuantity(int boosterId);

		// Token: 0x06009927 RID: 39207
		void SendCinematic(bool begin, float duration);

		// Token: 0x06009928 RID: 39208
		void SendClickRecruitAFriend();

		// Token: 0x06009929 RID: 39209
		void SendClientReset(bool forceLogin, bool forceNoAccountTutorial);

		// Token: 0x0600992A RID: 39210
		void SendCollectionLeftRightClick(CollectionLeftRightClick.Target target_);

		// Token: 0x0600992B RID: 39211
		void SendConnectToGameServer(uint resultBnetCode, string resultBnetCodeString, long timeSpentMilliseconds, GameSessionInfo gameSessionInfo);

		// Token: 0x0600992C RID: 39212
		void SendContentConnectFailedToConnect(string url, int httpErrorcode, int serverErrorcode);

		// Token: 0x0600992D RID: 39213
		void SendCrmEvent(string eventName, string eventPayload, string applicationId);

		// Token: 0x0600992E RID: 39214
		void SendDataUpdateFailed(float duration, long realDownloadBytes, long expectedDownloadBytes, int errorCode);

		// Token: 0x0600992F RID: 39215
		void SendDataUpdateFinished(float duration, long realDownloadBytes, long expectedDownloadBytes);

		// Token: 0x06009930 RID: 39216
		void SendDataUpdateProgress(float duration, long realDownloadBytes, long expectedDownloadBytes);

		// Token: 0x06009931 RID: 39217
		void SendDataUpdateStarted();

		// Token: 0x06009932 RID: 39218
		void SendDeckCopied(long deckId, string deckHash);

		// Token: 0x06009933 RID: 39219
		void SendDeckPickerToCollection(DeckPickerToCollection.Path path_);

		// Token: 0x06009934 RID: 39220
		void SendDeckUpdateResponseInfo(float duration);

		// Token: 0x06009935 RID: 39221
		void SendDeepLinkExecuted(string deepLink, string source, bool completed);

		// Token: 0x06009936 RID: 39222
		void SendDeviceInfo(DeviceInfo.OSCategory os, string osVersion, string model, DeviceInfo.ScreenCategory screen, DeviceInfo.ConnectionType connectionType_, string droidTextureCompression);

		// Token: 0x06009937 RID: 39223
		void SendDeviceMuteChanged(bool muted);

		// Token: 0x06009938 RID: 39224
		void SendDeviceVolumeChanged(float oldVolume, float newVolume);

		// Token: 0x06009939 RID: 39225
		void SendDragDropCancelPlayCard(long scenarioId, string cardType);

		// Token: 0x0600993A RID: 39226
		void SendEndGameScreenInit(float elapsedTime, int medalInfoRetryCount, bool medalInfoRetriesTimedOut, bool showRankedReward, bool showCardBackProgress, int otherRewardCount);

		// Token: 0x0600993B RID: 39227
		void SendFatalBattleNetError(int errorCode, string description);

		// Token: 0x0600993C RID: 39228
		void SendFatalError(string reason);

		// Token: 0x0600993D RID: 39229
		void SendFindGameResult(uint resultCode, string resultCodeString, long timeSpentMilliseconds, GameSessionInfo gameSessionInfo);

		// Token: 0x0600993E RID: 39230
		void SendFlowPerformance(string uniqueId, Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType flowType_, float averageFps, float duration, float fpsWarningsThreshold, int fpsWarningsTotalOccurences, float fpsWarningsTotalTime, float fpsWarningsAverageTime, float fpsWarningsMaxTime);

		// Token: 0x0600993F RID: 39231
		void SendFlowPerformanceBattlegrounds(string flowId, string gameUuid, int totalRounds);

		// Token: 0x06009940 RID: 39232
		void SendFlowPerformanceGame(string flowId, string uuid, GameType gameType, FormatType formatType, int boardId, int scenarioId);

		// Token: 0x06009941 RID: 39233
		void SendFlowPerformanceShop(string flowId, Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType shopType_);

		// Token: 0x06009942 RID: 39234
		void SendFriendsListView(string currentScene);

		// Token: 0x06009943 RID: 39235
		void SendGameRoundStartAudioSettings(bool deviceMuted, float deviceVolume, float masterVolume, float musicVolume);

		// Token: 0x06009944 RID: 39236
		void SendIgnorableBattleNetError(int errorCode, string description);

		// Token: 0x06009945 RID: 39237
		void SendIKSClicked(string iksCampaignName, string iksMediaUrl);

		// Token: 0x06009946 RID: 39238
		void SendIKSIgnored(string iksCampaignName, string iksMediaUrl);

		// Token: 0x06009947 RID: 39239
		void SendInGameMessageAction(string messageType, string title, InGameMessageAction.ActionType action, int viewCounts, string uid);

		// Token: 0x06009948 RID: 39240
		void SendInGameMessageHandlerCalled(string messageType, string title, string uid);

		// Token: 0x06009949 RID: 39241
		void SendInitialClientStateOutOfOrder(int countNotificationsAchieve, int countNotificationsNotice, int countNotificationsCollection, int countNotificationsCurrency, int countNotificationsBooster, int countNotificationsHeroxp, int countNotificationsPlayerRecord, int countNotificationsArenaSession, int countNotificationsCardBack);

		// Token: 0x0600994A RID: 39242
		void SendJobBegin(string jobId, string testType);

		// Token: 0x0600994B RID: 39243
		void SendJobExceededLimit(string jobId, long jobDuration, string testType);

		// Token: 0x0600994C RID: 39244
		void SendJobFinishFailure(string jobId, string jobFailureReason, string testType, string clientChangelist, float duration);

		// Token: 0x0600994D RID: 39245
		void SendJobFinishSuccess(string jobId, string testType, string clientChangelist, float duration);

		// Token: 0x0600994E RID: 39246
		void SendJobStepExceededLimit(string jobId, long jobDuration, string testType);

		// Token: 0x0600994F RID: 39247
		void SendLanguageChanged(string previousLanguage, string nextLanguage);

		// Token: 0x06009950 RID: 39248
		void SendLargeThirdPartyReceiptFound(long receiptSize);

		// Token: 0x06009951 RID: 39249
		void SendLiveIssue(string category, string details);

		// Token: 0x06009952 RID: 39250
		void SendLocaleDataUpdateFailed(float duration, long realDownloadBytes, long expectedDownloadBytes, int errorCode);

		// Token: 0x06009953 RID: 39251
		void SendLocaleDataUpdateFinished(float duration, long realDownloadBytes, long expectedDownloadBytes);

		// Token: 0x06009954 RID: 39252
		void SendLocaleDataUpdateStarted(string locale);

		// Token: 0x06009955 RID: 39253
		void SendLoginTokenFetchResult(LoginTokenFetchResult.TokenFetchResult result);

		// Token: 0x06009956 RID: 39254
		void SendManaFilterToggleOff();

		// Token: 0x06009957 RID: 39255
		void SendMASDKAuthResult(MASDKAuthResult.AuthResult result, int errorCode, string source);

		// Token: 0x06009958 RID: 39256
		void SendMASDKGuestCreationFailure(int errorCode);

		// Token: 0x06009959 RID: 39257
		void SendMASDKImportResult(MASDKImportResult.ImportResult result, MASDKImportResult.ImportType importType_, int errorCode);

		// Token: 0x0600995A RID: 39258
		void SendMasterVolumeChanged(float oldVolume, float newVolume);

		// Token: 0x0600995B RID: 39259
		void SendMissingAssetError(string missingAssetPath, string assetContext);

		// Token: 0x0600995C RID: 39260
		void SendMusicVolumeChanged(float oldVolume, float newVolume);

		// Token: 0x0600995D RID: 39261
		void SendNetworkError(NetworkError.ErrorType errorType_, string description, int errorCode);

		// Token: 0x0600995E RID: 39262
		void SendNetworkUnreachableRecovered(int outageSeconds);

		// Token: 0x0600995F RID: 39263
		void SendPackOpenToStore(PackOpenToStore.Path path_);

		// Token: 0x06009960 RID: 39264
		void SendPresenceChanged(PresenceStatus newPresenceStatus, PresenceStatus prevPresenceStatus, long millisecondsSincePrev);

		// Token: 0x06009961 RID: 39265
		void SendPreviousInstanceStatus(int totalCrashCount, int totalExceptionCount, int lowMemoryWarningCount, int crashInARowCount, int sameExceptionCount, bool crashed, string exceptionHash);

		// Token: 0x06009962 RID: 39266
		void SendPurchaseCancelClicked(long pmtProductId);

		// Token: 0x06009963 RID: 39267
		void SendPurchasePayNowClicked(long pmtProductId);

		// Token: 0x06009964 RID: 39268
		void SendPushEvent(string campaignId, string eventPayload, string applicationId);

		// Token: 0x06009965 RID: 39269
		void SendPushRegistration(string pushId, int utcOffset, string timezone, string applicationId, string language, string os, string osVersion, string deviceHeight, string deviceWidth, string deviceDpi);

		// Token: 0x06009966 RID: 39270
		void SendRealMoneyTransaction(string applicationId, string appStore, string receipt, string receiptSignature, string productId, string itemCost, string itemQuantity, string localCurrency, string transactionId);

		// Token: 0x06009967 RID: 39271
		void SendReconnectSuccess(float disconnectDuration, float reconnectDuration, string reconnectType);

		// Token: 0x06009968 RID: 39272
		void SendReconnectTimeout(string reconnectType);

		// Token: 0x06009969 RID: 39273
		void SendRepairPrestep(int doubletapFingers, int locales);

		// Token: 0x0600996A RID: 39274
		void SendRestartDueToPlayerMigration();

		// Token: 0x0600996B RID: 39275
		void SendReturningPlayerDeckNotCreated(uint aBGroup);

		// Token: 0x0600996C RID: 39276
		void SendRuntimeUpdate(float duration, RuntimeUpdate.Intention intention_);

		// Token: 0x0600996D RID: 39277
		void SendSessionEnd(string applicationId);

		// Token: 0x0600996E RID: 39278
		void SendSessionStart(string eventPayload, string applicationId);

		// Token: 0x0600996F RID: 39279
		void SendShopBalanceAvailable(List<Balance> balances);

		// Token: 0x06009970 RID: 39280
		void SendShopCardClick(ShopCard shopcard);

		// Token: 0x06009971 RID: 39281
		void SendShopPurchaseEvent(Product product, int quantity, string currency, double amount, bool isGift, string storefront, bool purchaseComplete, string storeType);

		// Token: 0x06009972 RID: 39282
		void SendShopStatus(string error, double timeInHubSec);

		// Token: 0x06009973 RID: 39283
		void SendShopVisit(List<ShopCard> cards);

		// Token: 0x06009974 RID: 39284
		void SendSmartDeckCompleteFailed(int requestMessageSize);

		// Token: 0x06009975 RID: 39285
		void SendStartupAudioSettings(bool deviceMuted, float deviceVolume, float masterVolume, float musicVolume);

		// Token: 0x06009976 RID: 39286
		void SendTempAccountStoredInCloud(bool stored);

		// Token: 0x06009977 RID: 39287
		void SendThirdPartyPurchaseCompletedFail(string transactionId, string productId, string bpayProvider, string errorInfo);

		// Token: 0x06009978 RID: 39288
		void SendThirdPartyPurchaseCompletedSuccess(string transactionId, string productId, string bpayProvider);

		// Token: 0x06009979 RID: 39289
		void SendThirdPartyPurchaseDanglingReceiptFail(string transactionId, string productId, string provider, ThirdPartyPurchaseDanglingReceiptFail.FailureReason reason, string invalidData);

		// Token: 0x0600997A RID: 39290
		void SendThirdPartyPurchaseDeferred(string transactionId, string productId);

		// Token: 0x0600997B RID: 39291
		void SendThirdPartyPurchaseMalformedData(string transactionId);

		// Token: 0x0600997C RID: 39292
		void SendThirdPartyPurchaseReceiptFound(string transactionId, string productId);

		// Token: 0x0600997D RID: 39293
		void SendThirdPartyPurchaseReceiptNotFound(string transactionId, string productId);

		// Token: 0x0600997E RID: 39294
		void SendThirdPartyPurchaseReceiptReceived(string transactionId, string productId);

		// Token: 0x0600997F RID: 39295
		void SendThirdPartyPurchaseReceiptRequest(string transactionId, string productId);

		// Token: 0x06009980 RID: 39296
		void SendThirdPartyPurchaseReceiptSubmitFail(string transactionId, string productId, string provider, ThirdPartyPurchaseReceiptSubmitFail.FailureReason reason, string invalidData);

		// Token: 0x06009981 RID: 39297
		void SendThirdPartyPurchaseStart(string transactionId, string productId, string bpayProvider);

		// Token: 0x06009982 RID: 39298
		void SendThirdPartyPurchaseSubmitResponseDeviceNotification(string transactionId, bool success);

		// Token: 0x06009983 RID: 39299
		void SendThirdPartyReceiptConsumed(string transactionId);

		// Token: 0x06009984 RID: 39300
		void SendThirdPartyUserIdUpdated(bool validChange);

		// Token: 0x06009985 RID: 39301
		void SendVirtualCurrencyTransaction(string applicationId, string itemId, string itemCost, string itemQuantity, string currency, string payload);

		// Token: 0x06009986 RID: 39302
		void SendWebLoginError();

		// Token: 0x06009987 RID: 39303
		void SendWelcomeQuestsAcknowledged(float questAckDuration);

		// Token: 0x06009988 RID: 39304
		void SendAttributionInstall(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string referrer, string appleSearchAdsJson, int appleSearchAdsErrorCode, IdentifierInfo identifier);

		// Token: 0x06009989 RID: 39305
		void SendAttributionLaunch(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, int counter);

		// Token: 0x0600998A RID: 39306
		void SendApkInstallFailure(string updatedVersion, string reason);

		// Token: 0x0600998B RID: 39307
		void SendApkInstallSuccess(string updatedVersion, float availableSpaceMB, float elapsedSeconds);

		// Token: 0x0600998C RID: 39308
		void SendApkUpdate(int installedVersion, int assetVersion, int agentVersion);

		// Token: 0x0600998D RID: 39309
		void SendCertificateRejected();

		// Token: 0x0600998E RID: 39310
		void SendDeviceInfo(string androidId, string androidModel, uint androidSdkVersion, bool isConnectedToWifi, string gpuTextureFormat, string locale, string bnetRegion);

		// Token: 0x0600998F RID: 39311
		void SendNotEnoughSpaceError(ulong availableSpace, ulong expectedOrgBytes, string filesDir);

		// Token: 0x06009990 RID: 39312
		void SendNoWifi(string updatedVersion, float availableSpaceMB, float elapsedSeconds);

		// Token: 0x06009991 RID: 39313
		void SendOpeningAppStore(string updatedVersion, float availableSpaceMB, float elapsedSeconds);

		// Token: 0x06009992 RID: 39314
		void SendUncaughtException(string stackTrace, string androidModel, uint androidSdkVersion);

		// Token: 0x06009993 RID: 39315
		void SendUpdateError(uint errorCode, float elapsedSeconds);

		// Token: 0x06009994 RID: 39316
		void SendUpdateFinished(string updatedVersion, float availableSpaceMB, float elapsedSeconds);

		// Token: 0x06009995 RID: 39317
		void SendUpdateProgress(float duration, long realDownloadBytes, long expectedDownloadBytes);

		// Token: 0x06009996 RID: 39318
		void SendUpdateStarted(string installedVersion, string textureFormat, string dataPath, float availableSpaceMB);

		// Token: 0x06009997 RID: 39319
		void SendUsingCellularData(string updatedVersion, float availableSpaceMB, float elapsedSeconds);

		// Token: 0x06009998 RID: 39320
		void SendVersionError(uint errorCode, uint agentState, string languages, string region, string branch, string additionalTags);

		// Token: 0x06009999 RID: 39321
		void SendVersionFinished(string currentVersion, string liveVersion);

		// Token: 0x0600999A RID: 39322
		void SendVersionStarted(int dummy);

		// Token: 0x0600999B RID: 39323
		void Initialize(Service telemetryService);

		// Token: 0x0600999C RID: 39324
		bool IsInitialized();

		// Token: 0x0600999D RID: 39325
		void Disable();

		// Token: 0x0600999E RID: 39326
		void OnUpdate();

		// Token: 0x0600999F RID: 39327
		void Shutdown();

		// Token: 0x060099A0 RID: 39328
		void RegisterUpdateHandler(Action updateHandler);

		// Token: 0x060099A1 RID: 39329
		long EnqueueMessage(IProtoBuf message, MessageOptions options = null);

		// Token: 0x060099A2 RID: 39330
		long EnqueueMessage(string packageName, string messageName, byte[] data, MessageOptions options = null);

		// Token: 0x060099A3 RID: 39331
		void SendConnectSuccess(string name, string host = null, uint? port = null);

		// Token: 0x060099A4 RID: 39332
		void SendConnectFail(string name, string reason, string host = null, uint? port = null);

		// Token: 0x060099A5 RID: 39333
		void SendDisconnect(string name, Disconnect.Reason reason, string description = null, string host = null, uint? port = null);

		// Token: 0x060099A6 RID: 39334
		void SendFindGameResult(FindGameResult message);

		// Token: 0x060099A7 RID: 39335
		void SendConnectToGameServer(ConnectToGameServer message);

		// Token: 0x060099A8 RID: 39336
		void SendTcpQualitySample(string address4, uint port, float sampleTimeMs, uint bytesSent, uint bytesReceived, uint messagesSent, uint messagesReceived);

		// Token: 0x060099A9 RID: 39337
		void SendDevLogTelemetry(string category, string details);
	}
}
