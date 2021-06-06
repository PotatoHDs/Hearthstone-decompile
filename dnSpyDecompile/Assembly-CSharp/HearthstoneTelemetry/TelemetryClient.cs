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
	// Token: 0x02000B4A RID: 2890
	public class TelemetryClient : ITelemetryClient
	{
		// Token: 0x060099AA RID: 39338 RVA: 0x003181B0 File Offset: 0x003163B0
		public void SendAccountHealUpResult(AccountHealUpResult.HealUpResult result, int errorCode)
		{
			this.EnqueueMessage(new AccountHealUpResult
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				Result = result,
				ErrorCode = errorCode
			}, null);
		}

		// Token: 0x060099AB RID: 39339 RVA: 0x003181F4 File Offset: 0x003163F4
		public void SendAppInitialized(string testType, float duration, string clientChangelist)
		{
			this.EnqueueMessage(new AppInitialized
			{
				TestType = testType,
				Duration = duration,
				ClientChangelist = clientChangelist
			}, null);
		}

		// Token: 0x060099AC RID: 39340 RVA: 0x00318228 File Offset: 0x00316428
		public void SendAppPaused(bool pauseStatus, float pauseTime)
		{
			this.EnqueueMessage(new AppPaused
			{
				PauseStatus = pauseStatus,
				PauseTime = pauseTime
			}, null);
		}

		// Token: 0x060099AD RID: 39341 RVA: 0x00318254 File Offset: 0x00316454
		public void SendAppStart(string testType, float duration, string clientChangelist)
		{
			this.EnqueueMessage(new AppStart
			{
				TestType = testType,
				Duration = duration,
				ClientChangelist = clientChangelist
			}, null);
		}

		// Token: 0x060099AE RID: 39342 RVA: 0x00318288 File Offset: 0x00316488
		public void SendAssetNotFound(string assetType, string assetGuid, string filePath, string legacyName)
		{
			this.EnqueueMessage(new AssetNotFound
			{
				DeviceInfo = this.GetDeviceInfo(),
				AssetType = assetType,
				AssetGuid = assetGuid,
				FilePath = filePath,
				LegacyName = legacyName
			}, null);
		}

		// Token: 0x060099AF RID: 39343 RVA: 0x003182D0 File Offset: 0x003164D0
		public void SendAssetOrphaned(string filePath, string handleOwner, string handleType)
		{
			this.EnqueueMessage(new AssetOrphaned
			{
				DeviceInfo = this.GetDeviceInfo(),
				FilePath = filePath,
				HandleOwner = handleOwner,
				HandleType = handleType
			}, null);
		}

		// Token: 0x060099B0 RID: 39344 RVA: 0x00318310 File Offset: 0x00316510
		public void SendAttackInputMethod(long totalNumAttacks, long totalClickAttacks, int percentClickAttacks, long totalDragAttacks, int percentDragAttacks)
		{
			this.EnqueueMessage(new AttackInputMethod
			{
				DeviceInfo = this.GetDeviceInfo(),
				TotalNumAttacks = totalNumAttacks,
				TotalClickAttacks = totalClickAttacks,
				PercentClickAttacks = percentClickAttacks,
				TotalDragAttacks = totalDragAttacks,
				PercentDragAttacks = percentDragAttacks
			}, null);
		}

		// Token: 0x060099B1 RID: 39345 RVA: 0x00318360 File Offset: 0x00316560
		public void SendAttributionContentUnlocked(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string contentId)
		{
			this.EnqueueMessage(new AttributionContentUnlocked
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				ContentId = contentId
			}, null);
		}

		// Token: 0x060099B2 RID: 39346 RVA: 0x003183A4 File Offset: 0x003165A4
		public void SendAttributionFirstLogin(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier)
		{
			this.EnqueueMessage(new AttributionFirstLogin
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				Identifier = identifier
			}, null);
		}

		// Token: 0x060099B3 RID: 39347 RVA: 0x003183E8 File Offset: 0x003165E8
		public void SendAttributionGameRoundEnd(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string gameMode, string result, FormatType formatType)
		{
			this.EnqueueMessage(new AttributionGameRoundEnd
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				GameMode = gameMode,
				Result = result,
				FormatType = formatType
			}, null);
		}

		// Token: 0x060099B4 RID: 39348 RVA: 0x0031843C File Offset: 0x0031663C
		public void SendAttributionGameRoundStart(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string gameMode, FormatType formatType)
		{
			this.EnqueueMessage(new AttributionGameRoundStart
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				GameMode = gameMode,
				FormatType = formatType
			}, null);
		}

		// Token: 0x060099B5 RID: 39349 RVA: 0x00318488 File Offset: 0x00316688
		public void SendAttributionHeadlessAccountCreated(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier)
		{
			this.EnqueueMessage(new AttributionHeadlessAccountCreated
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				Identifier = identifier
			}, null);
		}

		// Token: 0x060099B6 RID: 39350 RVA: 0x003184CC File Offset: 0x003166CC
		public void SendAttributionHeadlessAccountHealedUp(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string temporaryGameAccountId, IdentifierInfo identifier)
		{
			this.EnqueueMessage(new AttributionHeadlessAccountHealedUp
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				TemporaryGameAccountId = temporaryGameAccountId,
				Identifier = identifier
			}, null);
		}

		// Token: 0x060099B7 RID: 39351 RVA: 0x00318518 File Offset: 0x00316718
		public void SendAttributionItemTransaction(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string itemId, int quantity)
		{
			this.EnqueueMessage(new AttributionItemTransaction
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				ItemId = itemId,
				Quantity = quantity
			}, null);
		}

		// Token: 0x060099B8 RID: 39352 RVA: 0x00318564 File Offset: 0x00316764
		public void SendAttributionLogin(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, IdentifierInfo identifier)
		{
			this.EnqueueMessage(new AttributionLogin
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				Identifier = identifier
			}, null);
		}

		// Token: 0x060099B9 RID: 39353 RVA: 0x003185A8 File Offset: 0x003167A8
		public void SendAttributionPurchase(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string purchaseType, string transactionId, int quantity, List<AttributionPurchase.PaymentInfo> payments, float amount, string currency)
		{
			this.EnqueueMessage(new AttributionPurchase
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				PurchaseType = purchaseType,
				TransactionId = transactionId,
				Quantity = quantity,
				Payments = payments,
				Amount = amount,
				Currency = currency
			}, null);
		}

		// Token: 0x060099BA RID: 39354 RVA: 0x00318614 File Offset: 0x00316814
		public void SendAttributionRegistration(string applicationId, string deviceType, ulong firstInstallDate, string bundleId)
		{
			this.EnqueueMessage(new AttributionRegistration
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId
			}, null);
		}

		// Token: 0x060099BB RID: 39355 RVA: 0x00318650 File Offset: 0x00316850
		public void SendAttributionScenarioResult(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, int scenarioId, string result, int bossId, IdentifierInfo identifier)
		{
			this.EnqueueMessage(new AttributionScenarioResult
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				ScenarioId = scenarioId,
				Result = result,
				BossId = bossId,
				Identifier = identifier
			}, null);
		}

		// Token: 0x060099BC RID: 39356 RVA: 0x003186AC File Offset: 0x003168AC
		public void SendAttributionVirtualCurrencyTransaction(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, float amount, string currency)
		{
			this.EnqueueMessage(new AttributionVirtualCurrencyTransaction
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				Amount = amount,
				Currency = currency
			}, null);
		}

		// Token: 0x060099BD RID: 39357 RVA: 0x003186F8 File Offset: 0x003168F8
		public void SendBlizzardCheckoutGeneric(string messageKey, string messageValue)
		{
			this.EnqueueMessage(new BlizzardCheckoutGeneric
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				MessageKey = messageKey,
				MessageValue = messageValue
			}, null);
		}

		// Token: 0x060099BE RID: 39358 RVA: 0x0031873C File Offset: 0x0031693C
		public void SendBlizzardCheckoutInitializationResult(bool success, string failureReason, string failureDetails)
		{
			this.EnqueueMessage(new BlizzardCheckoutInitializationResult
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				Success = success,
				FailureReason = failureReason,
				FailureDetails = failureDetails
			}, null);
		}

		// Token: 0x060099BF RID: 39359 RVA: 0x00318788 File Offset: 0x00316988
		public void SendBlizzardCheckoutIsReady(double secondsShown, bool isReady)
		{
			this.EnqueueMessage(new BlizzardCheckoutIsReady
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				SecondsShown = secondsShown,
				IsReady = isReady
			}, null);
		}

		// Token: 0x060099C0 RID: 39360 RVA: 0x003187CC File Offset: 0x003169CC
		public void SendBlizzardCheckoutPurchaseCancel()
		{
			this.EnqueueMessage(new BlizzardCheckoutPurchaseCancel
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo()
			}, null);
		}

		// Token: 0x060099C1 RID: 39361 RVA: 0x00318800 File Offset: 0x00316A00
		public void SendBlizzardCheckoutPurchaseCompletedFailure(string transactionId, string productId, string currency, List<string> errorCodes)
		{
			this.EnqueueMessage(new BlizzardCheckoutPurchaseCompletedFailure
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId,
				Currency = currency,
				ErrorCodes = errorCodes
			}, null);
		}

		// Token: 0x060099C2 RID: 39362 RVA: 0x00318854 File Offset: 0x00316A54
		public void SendBlizzardCheckoutPurchaseCompletedSuccess(string transactionId, string productId, string currency)
		{
			this.EnqueueMessage(new BlizzardCheckoutPurchaseCompletedSuccess
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId,
				Currency = currency
			}, null);
		}

		// Token: 0x060099C3 RID: 39363 RVA: 0x003188A0 File Offset: 0x00316AA0
		public void SendBlizzardCheckoutPurchaseStart(string transactionId, string productId, string currency)
		{
			this.EnqueueMessage(new BlizzardCheckoutPurchaseStart
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId,
				Currency = currency
			}, null);
		}

		// Token: 0x060099C4 RID: 39364 RVA: 0x003188EC File Offset: 0x00316AEC
		public void SendBoxInteractable(string testType, float duration, string clientChangelist)
		{
			this.EnqueueMessage(new BoxInteractable
			{
				TestType = testType,
				Duration = duration,
				ClientChangelist = clientChangelist
			}, null);
		}

		// Token: 0x060099C5 RID: 39365 RVA: 0x00318920 File Offset: 0x00316B20
		public void SendButtonPressed(string buttonName)
		{
			this.EnqueueMessage(new ButtonPressed
			{
				ButtonName = buttonName
			}, null);
		}

		// Token: 0x060099C6 RID: 39366 RVA: 0x00318944 File Offset: 0x00316B44
		public void SendChangePackQuantity(int boosterId)
		{
			this.EnqueueMessage(new ChangePackQuantity
			{
				BoosterId = boosterId
			}, null);
		}

		// Token: 0x060099C7 RID: 39367 RVA: 0x00318968 File Offset: 0x00316B68
		public void SendCinematic(bool begin, float duration)
		{
			this.EnqueueMessage(new Blizzard.Telemetry.WTCG.Client.Cinematic
			{
				DeviceInfo = this.GetDeviceInfo(),
				Begin = begin,
				Duration = duration
			}, null);
		}

		// Token: 0x060099C8 RID: 39368 RVA: 0x003189A0 File Offset: 0x00316BA0
		public void SendClickRecruitAFriend()
		{
			this.EnqueueMessage(new ClickRecruitAFriend
			{
				DeviceInfo = this.GetDeviceInfo()
			}, null);
		}

		// Token: 0x060099C9 RID: 39369 RVA: 0x003189C8 File Offset: 0x00316BC8
		public void SendClientReset(bool forceLogin, bool forceNoAccountTutorial)
		{
			this.EnqueueMessage(new ClientReset
			{
				DeviceInfo = this.GetDeviceInfo(),
				ForceLogin = forceLogin,
				ForceNoAccountTutorial = forceNoAccountTutorial
			}, null);
		}

		// Token: 0x060099CA RID: 39370 RVA: 0x00318A00 File Offset: 0x00316C00
		public void SendCollectionLeftRightClick(CollectionLeftRightClick.Target target_)
		{
			this.EnqueueMessage(new CollectionLeftRightClick
			{
				Target_ = target_
			}, null);
		}

		// Token: 0x060099CB RID: 39371 RVA: 0x00318A24 File Offset: 0x00316C24
		public void SendConnectToGameServer(uint resultBnetCode, string resultBnetCodeString, long timeSpentMilliseconds, GameSessionInfo gameSessionInfo)
		{
			this.EnqueueMessage(new ConnectToGameServer
			{
				DeviceInfo = this.GetDeviceInfo(),
				Player = this.GetPlayer(),
				ResultBnetCode = resultBnetCode,
				ResultBnetCodeString = resultBnetCodeString,
				TimeSpentMilliseconds = timeSpentMilliseconds,
				GameSessionInfo = gameSessionInfo
			}, null);
		}

		// Token: 0x060099CC RID: 39372 RVA: 0x00318A78 File Offset: 0x00316C78
		public void SendContentConnectFailedToConnect(string url, int httpErrorcode, int serverErrorcode)
		{
			this.EnqueueMessage(new ContentConnectFailedToConnect
			{
				DeviceInfo = this.GetDeviceInfo(),
				Url = url,
				HttpErrorcode = httpErrorcode,
				ServerErrorcode = serverErrorcode
			}, null);
		}

		// Token: 0x060099CD RID: 39373 RVA: 0x00318AB8 File Offset: 0x00316CB8
		public void SendCrmEvent(string eventName, string eventPayload, string applicationId)
		{
			this.EnqueueMessage(new CrmEvent
			{
				EventName = eventName,
				EventPayload = eventPayload,
				ApplicationId = applicationId
			}, null);
		}

		// Token: 0x060099CE RID: 39374 RVA: 0x00318AEC File Offset: 0x00316CEC
		public void SendDataUpdateFailed(float duration, long realDownloadBytes, long expectedDownloadBytes, int errorCode)
		{
			this.EnqueueMessage(new DataUpdateFailed
			{
				DeviceInfo = this.GetDeviceInfo(),
				Duration = duration,
				RealDownloadBytes = realDownloadBytes,
				ExpectedDownloadBytes = expectedDownloadBytes,
				ErrorCode = errorCode
			}, null);
		}

		// Token: 0x060099CF RID: 39375 RVA: 0x00318B34 File Offset: 0x00316D34
		public void SendDataUpdateFinished(float duration, long realDownloadBytes, long expectedDownloadBytes)
		{
			this.EnqueueMessage(new DataUpdateFinished
			{
				DeviceInfo = this.GetDeviceInfo(),
				Duration = duration,
				RealDownloadBytes = realDownloadBytes,
				ExpectedDownloadBytes = expectedDownloadBytes
			}, null);
		}

		// Token: 0x060099D0 RID: 39376 RVA: 0x00318B74 File Offset: 0x00316D74
		public void SendDataUpdateProgress(float duration, long realDownloadBytes, long expectedDownloadBytes)
		{
			this.EnqueueMessage(new DataUpdateProgress
			{
				DeviceInfo = this.GetDeviceInfo(),
				Duration = duration,
				RealDownloadBytes = realDownloadBytes,
				ExpectedDownloadBytes = expectedDownloadBytes
			}, null);
		}

		// Token: 0x060099D1 RID: 39377 RVA: 0x00318BB4 File Offset: 0x00316DB4
		public void SendDataUpdateStarted()
		{
			this.EnqueueMessage(new DataUpdateStarted
			{
				DeviceInfo = this.GetDeviceInfo()
			}, null);
		}

		// Token: 0x060099D2 RID: 39378 RVA: 0x00318BDC File Offset: 0x00316DDC
		public void SendDeckCopied(long deckId, string deckHash)
		{
			this.EnqueueMessage(new DeckCopied
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				DeckId = deckId,
				DeckHash = deckHash
			}, null);
		}

		// Token: 0x060099D3 RID: 39379 RVA: 0x00318C20 File Offset: 0x00316E20
		public void SendDeckPickerToCollection(DeckPickerToCollection.Path path_)
		{
			this.EnqueueMessage(new DeckPickerToCollection
			{
				DeviceInfo = this.GetDeviceInfo(),
				Path_ = path_
			}, null);
		}

		// Token: 0x060099D4 RID: 39380 RVA: 0x00318C50 File Offset: 0x00316E50
		public void SendDeckUpdateResponseInfo(float duration)
		{
			this.EnqueueMessage(new DeckUpdateResponseInfo
			{
				DeviceInfo = this.GetDeviceInfo(),
				Duration = duration
			}, null);
		}

		// Token: 0x060099D5 RID: 39381 RVA: 0x00318C80 File Offset: 0x00316E80
		public void SendDeepLinkExecuted(string deepLink, string source, bool completed)
		{
			this.EnqueueMessage(new DeepLinkExecuted
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				DeepLink = deepLink,
				Source = source,
				Completed = completed
			}, null);
		}

		// Token: 0x060099D6 RID: 39382 RVA: 0x00318CCC File Offset: 0x00316ECC
		public void SendDeviceInfo(Blizzard.Telemetry.WTCG.Client.DeviceInfo.OSCategory os, string osVersion, string model, Blizzard.Telemetry.WTCG.Client.DeviceInfo.ScreenCategory screen, Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType connectionType_, string droidTextureCompression)
		{
			this.EnqueueMessage(new Blizzard.Telemetry.WTCG.Client.DeviceInfo
			{
				Os = os,
				OsVersion = osVersion,
				Model = model,
				Screen = screen,
				ConnectionType_ = connectionType_,
				DroidTextureCompression = droidTextureCompression
			}, null);
		}

		// Token: 0x060099D7 RID: 39383 RVA: 0x00318D18 File Offset: 0x00316F18
		public void SendDeviceMuteChanged(bool muted)
		{
			this.EnqueueMessage(new DeviceMuteChanged
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				Muted = muted
			}, null);
		}

		// Token: 0x060099D8 RID: 39384 RVA: 0x00318D54 File Offset: 0x00316F54
		public void SendDeviceVolumeChanged(float oldVolume, float newVolume)
		{
			this.EnqueueMessage(new DeviceVolumeChanged
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				OldVolume = oldVolume,
				NewVolume = newVolume
			}, null);
		}

		// Token: 0x060099D9 RID: 39385 RVA: 0x00318D98 File Offset: 0x00316F98
		public void SendDragDropCancelPlayCard(long scenarioId, string cardType)
		{
			this.EnqueueMessage(new DragDropCancelPlayCard
			{
				DeviceInfo = this.GetDeviceInfo(),
				ScenarioId = scenarioId,
				CardType = cardType
			}, null);
		}

		// Token: 0x060099DA RID: 39386 RVA: 0x00318DD0 File Offset: 0x00316FD0
		public void SendEndGameScreenInit(float elapsedTime, int medalInfoRetryCount, bool medalInfoRetriesTimedOut, bool showRankedReward, bool showCardBackProgress, int otherRewardCount)
		{
			this.EnqueueMessage(new EndGameScreenInit
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				ElapsedTime = elapsedTime,
				MedalInfoRetryCount = medalInfoRetryCount,
				MedalInfoRetriesTimedOut = medalInfoRetriesTimedOut,
				ShowRankedReward = showRankedReward,
				ShowCardBackProgress = showCardBackProgress,
				OtherRewardCount = otherRewardCount
			}, null);
		}

		// Token: 0x060099DB RID: 39387 RVA: 0x00318E34 File Offset: 0x00317034
		public void SendFatalBattleNetError(int errorCode, string description)
		{
			this.EnqueueMessage(new FatalBattleNetError
			{
				DeviceInfo = this.GetDeviceInfo(),
				ErrorCode = errorCode,
				Description = description
			}, null);
		}

		// Token: 0x060099DC RID: 39388 RVA: 0x00318E6C File Offset: 0x0031706C
		public void SendFatalError(string reason)
		{
			this.EnqueueMessage(new FatalError
			{
				DeviceInfo = this.GetDeviceInfo(),
				Reason = reason
			}, null);
		}

		// Token: 0x060099DD RID: 39389 RVA: 0x00318E9C File Offset: 0x0031709C
		public void SendFindGameResult(uint resultCode, string resultCodeString, long timeSpentMilliseconds, GameSessionInfo gameSessionInfo)
		{
			this.EnqueueMessage(new FindGameResult
			{
				DeviceInfo = this.GetDeviceInfo(),
				Player = this.GetPlayer(),
				ResultCode = resultCode,
				ResultCodeString = resultCodeString,
				TimeSpentMilliseconds = timeSpentMilliseconds,
				GameSessionInfo = gameSessionInfo
			}, null);
		}

		// Token: 0x060099DE RID: 39390 RVA: 0x00318EF0 File Offset: 0x003170F0
		public void SendFlowPerformance(string uniqueId, Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType flowType_, float averageFps, float duration, float fpsWarningsThreshold, int fpsWarningsTotalOccurences, float fpsWarningsTotalTime, float fpsWarningsAverageTime, float fpsWarningsMaxTime)
		{
			this.EnqueueMessage(new Blizzard.Telemetry.WTCG.Client.FlowPerformance
			{
				UniqueId = uniqueId,
				DeviceInfo = this.GetDeviceInfo(),
				Player = this.GetPlayer(),
				FlowType_ = flowType_,
				AverageFps = averageFps,
				Duration = duration,
				FpsWarningsThreshold = fpsWarningsThreshold,
				FpsWarningsTotalOccurences = fpsWarningsTotalOccurences,
				FpsWarningsTotalTime = fpsWarningsTotalTime,
				FpsWarningsAverageTime = fpsWarningsAverageTime,
				FpsWarningsMaxTime = fpsWarningsMaxTime
			}, null);
		}

		// Token: 0x060099DF RID: 39391 RVA: 0x00318F6C File Offset: 0x0031716C
		public void SendFlowPerformanceBattlegrounds(string flowId, string gameUuid, int totalRounds)
		{
			this.EnqueueMessage(new Blizzard.Telemetry.WTCG.Client.FlowPerformanceBattlegrounds
			{
				FlowId = flowId,
				GameUuid = gameUuid,
				TotalRounds = totalRounds
			}, null);
		}

		// Token: 0x060099E0 RID: 39392 RVA: 0x00318FA0 File Offset: 0x003171A0
		public void SendFlowPerformanceGame(string flowId, string uuid, GameType gameType, FormatType formatType, int boardId, int scenarioId)
		{
			this.EnqueueMessage(new Blizzard.Telemetry.WTCG.Client.FlowPerformanceGame
			{
				FlowId = flowId,
				DeviceInfo = this.GetDeviceInfo(),
				Player = this.GetPlayer(),
				Uuid = uuid,
				GameType = gameType,
				FormatType = formatType,
				BoardId = boardId,
				ScenarioId = scenarioId
			}, null);
		}

		// Token: 0x060099E1 RID: 39393 RVA: 0x00319004 File Offset: 0x00317204
		public void SendFlowPerformanceShop(string flowId, Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop.ShopType shopType_)
		{
			this.EnqueueMessage(new Blizzard.Telemetry.WTCG.Client.FlowPerformanceShop
			{
				FlowId = flowId,
				DeviceInfo = this.GetDeviceInfo(),
				Player = this.GetPlayer(),
				ShopType_ = shopType_
			}, null);
		}

		// Token: 0x060099E2 RID: 39394 RVA: 0x00319048 File Offset: 0x00317248
		public void SendFriendsListView(string currentScene)
		{
			this.EnqueueMessage(new FriendsListView
			{
				DeviceInfo = this.GetDeviceInfo(),
				CurrentScene = currentScene
			}, null);
		}

		// Token: 0x060099E3 RID: 39395 RVA: 0x00319078 File Offset: 0x00317278
		public void SendGameRoundStartAudioSettings(bool deviceMuted, float deviceVolume, float masterVolume, float musicVolume)
		{
			this.EnqueueMessage(new GameRoundStartAudioSettings
			{
				DeviceInfo = this.GetDeviceInfo(),
				Player = this.GetPlayer(),
				DeviceMuted = deviceMuted,
				DeviceVolume = deviceVolume,
				MasterVolume = masterVolume,
				MusicVolume = musicVolume
			}, null);
		}

		// Token: 0x060099E4 RID: 39396 RVA: 0x003190CC File Offset: 0x003172CC
		public void SendIgnorableBattleNetError(int errorCode, string description)
		{
			this.EnqueueMessage(new IgnorableBattleNetError
			{
				DeviceInfo = this.GetDeviceInfo(),
				ErrorCode = errorCode,
				Description = description
			}, null);
		}

		// Token: 0x060099E5 RID: 39397 RVA: 0x00319104 File Offset: 0x00317304
		public void SendIKSClicked(string iksCampaignName, string iksMediaUrl)
		{
			this.EnqueueMessage(new IKSClicked
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				IksCampaignName = iksCampaignName,
				IksMediaUrl = iksMediaUrl
			}, null);
		}

		// Token: 0x060099E6 RID: 39398 RVA: 0x00319148 File Offset: 0x00317348
		public void SendIKSIgnored(string iksCampaignName, string iksMediaUrl)
		{
			this.EnqueueMessage(new IKSIgnored
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				IksCampaignName = iksCampaignName,
				IksMediaUrl = iksMediaUrl
			}, null);
		}

		// Token: 0x060099E7 RID: 39399 RVA: 0x0031918C File Offset: 0x0031738C
		public void SendInGameMessageAction(string messageType, string title, InGameMessageAction.ActionType action, int viewCounts, string uid)
		{
			this.EnqueueMessage(new InGameMessageAction
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				MessageType = messageType,
				Title = title,
				Action = action,
				ViewCounts = viewCounts,
				Uid = uid
			}, null);
		}

		// Token: 0x060099E8 RID: 39400 RVA: 0x003191E8 File Offset: 0x003173E8
		public void SendInGameMessageHandlerCalled(string messageType, string title, string uid)
		{
			this.EnqueueMessage(new InGameMessageHandlerCalled
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				MessageType = messageType,
				Title = title,
				Uid = uid
			}, null);
		}

		// Token: 0x060099E9 RID: 39401 RVA: 0x00319234 File Offset: 0x00317434
		public void SendInitialClientStateOutOfOrder(int countNotificationsAchieve, int countNotificationsNotice, int countNotificationsCollection, int countNotificationsCurrency, int countNotificationsBooster, int countNotificationsHeroxp, int countNotificationsPlayerRecord, int countNotificationsArenaSession, int countNotificationsCardBack)
		{
			this.EnqueueMessage(new InitialClientStateOutOfOrder
			{
				CountNotificationsAchieve = countNotificationsAchieve,
				CountNotificationsNotice = countNotificationsNotice,
				CountNotificationsCollection = countNotificationsCollection,
				CountNotificationsCurrency = countNotificationsCurrency,
				CountNotificationsBooster = countNotificationsBooster,
				CountNotificationsHeroxp = countNotificationsHeroxp,
				CountNotificationsPlayerRecord = countNotificationsPlayerRecord,
				CountNotificationsArenaSession = countNotificationsArenaSession,
				CountNotificationsCardBack = countNotificationsCardBack,
				DeviceInfo = this.GetDeviceInfo()
			}, null);
		}

		// Token: 0x060099EA RID: 39402 RVA: 0x003192A4 File Offset: 0x003174A4
		public void SendJobBegin(string jobId, string testType)
		{
			this.EnqueueMessage(new JobBegin
			{
				JobId = jobId,
				TestType = testType
			}, null);
		}

		// Token: 0x060099EB RID: 39403 RVA: 0x003192D0 File Offset: 0x003174D0
		public void SendJobExceededLimit(string jobId, long jobDuration, string testType)
		{
			this.EnqueueMessage(new JobExceededLimit
			{
				JobId = jobId,
				JobDuration = jobDuration,
				TestType = testType
			}, null);
		}

		// Token: 0x060099EC RID: 39404 RVA: 0x00319304 File Offset: 0x00317504
		public void SendJobFinishFailure(string jobId, string jobFailureReason, string testType, string clientChangelist, float duration)
		{
			this.EnqueueMessage(new JobFinishFailure
			{
				JobId = jobId,
				JobFailureReason = jobFailureReason,
				TestType = testType,
				ClientChangelist = clientChangelist,
				Duration = duration
			}, null);
		}

		// Token: 0x060099ED RID: 39405 RVA: 0x00319348 File Offset: 0x00317548
		public void SendJobFinishSuccess(string jobId, string testType, string clientChangelist, float duration)
		{
			this.EnqueueMessage(new JobFinishSuccess
			{
				JobId = jobId,
				TestType = testType,
				ClientChangelist = clientChangelist,
				Duration = duration
			}, null);
		}

		// Token: 0x060099EE RID: 39406 RVA: 0x00319384 File Offset: 0x00317584
		public void SendJobStepExceededLimit(string jobId, long jobDuration, string testType)
		{
			this.EnqueueMessage(new JobStepExceededLimit
			{
				JobId = jobId,
				JobDuration = jobDuration,
				TestType = testType
			}, null);
		}

		// Token: 0x060099EF RID: 39407 RVA: 0x003193B8 File Offset: 0x003175B8
		public void SendLanguageChanged(string previousLanguage, string nextLanguage)
		{
			this.EnqueueMessage(new LanguageChanged
			{
				DeviceInfo = this.GetDeviceInfo(),
				PreviousLanguage = previousLanguage,
				NextLanguage = nextLanguage
			}, null);
		}

		// Token: 0x060099F0 RID: 39408 RVA: 0x003193F0 File Offset: 0x003175F0
		public void SendLargeThirdPartyReceiptFound(long receiptSize)
		{
			this.EnqueueMessage(new LargeThirdPartyReceiptFound
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				ReceiptSize = receiptSize
			}, null);
		}

		// Token: 0x060099F1 RID: 39409 RVA: 0x0031942C File Offset: 0x0031762C
		public void SendLiveIssue(string category, string details)
		{
			this.EnqueueMessage(new LiveIssue
			{
				Category = category,
				Details = details
			}, null);
		}

		// Token: 0x060099F2 RID: 39410 RVA: 0x00319458 File Offset: 0x00317658
		public void SendLocaleDataUpdateFailed(float duration, long realDownloadBytes, long expectedDownloadBytes, int errorCode)
		{
			this.EnqueueMessage(new LocaleDataUpdateFailed
			{
				DeviceInfo = this.GetDeviceInfo(),
				Duration = duration,
				RealDownloadBytes = realDownloadBytes,
				ExpectedDownloadBytes = expectedDownloadBytes,
				ErrorCode = errorCode
			}, null);
		}

		// Token: 0x060099F3 RID: 39411 RVA: 0x003194A0 File Offset: 0x003176A0
		public void SendLocaleDataUpdateFinished(float duration, long realDownloadBytes, long expectedDownloadBytes)
		{
			this.EnqueueMessage(new LocaleDataUpdateFinished
			{
				DeviceInfo = this.GetDeviceInfo(),
				Duration = duration,
				RealDownloadBytes = realDownloadBytes,
				ExpectedDownloadBytes = expectedDownloadBytes
			}, null);
		}

		// Token: 0x060099F4 RID: 39412 RVA: 0x003194E0 File Offset: 0x003176E0
		public void SendLocaleDataUpdateStarted(string locale)
		{
			this.EnqueueMessage(new LocaleDataUpdateStarted
			{
				DeviceInfo = this.GetDeviceInfo(),
				Locale = locale
			}, null);
		}

		// Token: 0x060099F5 RID: 39413 RVA: 0x00319510 File Offset: 0x00317710
		public void SendLoginTokenFetchResult(LoginTokenFetchResult.TokenFetchResult result)
		{
			this.EnqueueMessage(new LoginTokenFetchResult
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				Result = result
			}, null);
		}

		// Token: 0x060099F6 RID: 39414 RVA: 0x0031954C File Offset: 0x0031774C
		public void SendManaFilterToggleOff()
		{
			this.EnqueueMessage(new ManaFilterToggleOff
			{
				DeviceInfo = this.GetDeviceInfo()
			}, null);
		}

		// Token: 0x060099F7 RID: 39415 RVA: 0x00319574 File Offset: 0x00317774
		public void SendMASDKAuthResult(MASDKAuthResult.AuthResult result, int errorCode, string source)
		{
			this.EnqueueMessage(new MASDKAuthResult
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				Result = result,
				ErrorCode = errorCode,
				Source = source
			}, null);
		}

		// Token: 0x060099F8 RID: 39416 RVA: 0x003195C0 File Offset: 0x003177C0
		public void SendMASDKGuestCreationFailure(int errorCode)
		{
			this.EnqueueMessage(new MASDKGuestCreationFailure
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				ErrorCode = errorCode
			}, null);
		}

		// Token: 0x060099F9 RID: 39417 RVA: 0x003195FC File Offset: 0x003177FC
		public void SendMASDKImportResult(MASDKImportResult.ImportResult result, MASDKImportResult.ImportType importType_, int errorCode)
		{
			this.EnqueueMessage(new MASDKImportResult
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				Result = result,
				ImportType_ = importType_,
				ErrorCode = errorCode
			}, null);
		}

		// Token: 0x060099FA RID: 39418 RVA: 0x00319648 File Offset: 0x00317848
		public void SendMasterVolumeChanged(float oldVolume, float newVolume)
		{
			this.EnqueueMessage(new MasterVolumeChanged
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				OldVolume = oldVolume,
				NewVolume = newVolume
			}, null);
		}

		// Token: 0x060099FB RID: 39419 RVA: 0x0031968C File Offset: 0x0031788C
		public void SendMissingAssetError(string missingAssetPath, string assetContext)
		{
			this.EnqueueMessage(new MissingAssetError
			{
				DeviceInfo = this.GetDeviceInfo(),
				MissingAssetPath = missingAssetPath,
				AssetContext = assetContext
			}, null);
		}

		// Token: 0x060099FC RID: 39420 RVA: 0x003196C4 File Offset: 0x003178C4
		public void SendMusicVolumeChanged(float oldVolume, float newVolume)
		{
			this.EnqueueMessage(new MusicVolumeChanged
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				OldVolume = oldVolume,
				NewVolume = newVolume
			}, null);
		}

		// Token: 0x060099FD RID: 39421 RVA: 0x00319708 File Offset: 0x00317908
		public void SendNetworkError(NetworkError.ErrorType errorType_, string description, int errorCode)
		{
			this.EnqueueMessage(new NetworkError
			{
				DeviceInfo = this.GetDeviceInfo(),
				ErrorType_ = errorType_,
				Description = description,
				ErrorCode = errorCode
			}, null);
		}

		// Token: 0x060099FE RID: 39422 RVA: 0x00319748 File Offset: 0x00317948
		public void SendNetworkUnreachableRecovered(int outageSeconds)
		{
			this.EnqueueMessage(new NetworkUnreachableRecovered
			{
				DeviceInfo = this.GetDeviceInfo(),
				OutageSeconds = outageSeconds
			}, null);
		}

		// Token: 0x060099FF RID: 39423 RVA: 0x00319778 File Offset: 0x00317978
		public void SendPackOpenToStore(PackOpenToStore.Path path_)
		{
			this.EnqueueMessage(new PackOpenToStore
			{
				DeviceInfo = this.GetDeviceInfo(),
				Path_ = path_
			}, null);
		}

		// Token: 0x06009A00 RID: 39424 RVA: 0x003197A8 File Offset: 0x003179A8
		public void SendPresenceChanged(PresenceStatus newPresenceStatus, PresenceStatus prevPresenceStatus, long millisecondsSincePrev)
		{
			this.EnqueueMessage(new PresenceChanged
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				NewPresenceStatus = newPresenceStatus,
				PrevPresenceStatus = prevPresenceStatus,
				MillisecondsSincePrev = millisecondsSincePrev
			}, null);
		}

		// Token: 0x06009A01 RID: 39425 RVA: 0x003197F4 File Offset: 0x003179F4
		public void SendPreviousInstanceStatus(int totalCrashCount, int totalExceptionCount, int lowMemoryWarningCount, int crashInARowCount, int sameExceptionCount, bool crashed, string exceptionHash)
		{
			this.EnqueueMessage(new Blizzard.Telemetry.WTCG.Client.PreviousInstanceStatus
			{
				DeviceInfo = this.GetDeviceInfo(),
				TotalCrashCount = totalCrashCount,
				TotalExceptionCount = totalExceptionCount,
				LowMemoryWarningCount = lowMemoryWarningCount,
				CrashInARowCount = crashInARowCount,
				SameExceptionCount = sameExceptionCount,
				Crashed = crashed,
				ExceptionHash = exceptionHash
			}, null);
		}

		// Token: 0x06009A02 RID: 39426 RVA: 0x00319854 File Offset: 0x00317A54
		public void SendPurchaseCancelClicked(long pmtProductId)
		{
			this.EnqueueMessage(new PurchaseCancelClicked
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				PmtProductId = pmtProductId
			}, null);
		}

		// Token: 0x06009A03 RID: 39427 RVA: 0x00319890 File Offset: 0x00317A90
		public void SendPurchasePayNowClicked(long pmtProductId)
		{
			this.EnqueueMessage(new PurchasePayNowClicked
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				PmtProductId = pmtProductId
			}, null);
		}

		// Token: 0x06009A04 RID: 39428 RVA: 0x003198CC File Offset: 0x00317ACC
		public void SendPushEvent(string campaignId, string eventPayload, string applicationId)
		{
			this.EnqueueMessage(new PushEvent
			{
				CampaignId = campaignId,
				EventPayload = eventPayload,
				ApplicationId = applicationId
			}, null);
		}

		// Token: 0x06009A05 RID: 39429 RVA: 0x00319900 File Offset: 0x00317B00
		public void SendPushRegistration(string pushId, int utcOffset, string timezone, string applicationId, string language, string os, string osVersion, string deviceHeight, string deviceWidth, string deviceDpi)
		{
			this.EnqueueMessage(new PushRegistration
			{
				PushId = pushId,
				UtcOffset = utcOffset,
				Timezone = timezone,
				ApplicationId = applicationId,
				Language = language,
				Os = os,
				OsVersion = osVersion,
				DeviceHeight = deviceHeight,
				DeviceWidth = deviceWidth,
				DeviceDpi = deviceDpi
			}, null);
		}

		// Token: 0x06009A06 RID: 39430 RVA: 0x0031996C File Offset: 0x00317B6C
		public void SendRealMoneyTransaction(string applicationId, string appStore, string receipt, string receiptSignature, string productId, string itemCost, string itemQuantity, string localCurrency, string transactionId)
		{
			this.EnqueueMessage(new RealMoneyTransaction
			{
				ApplicationId = applicationId,
				AppStore = appStore,
				Receipt = receipt,
				ReceiptSignature = receiptSignature,
				ProductId = productId,
				ItemCost = itemCost,
				ItemQuantity = itemQuantity,
				LocalCurrency = localCurrency,
				TransactionId = transactionId
			}, null);
		}

		// Token: 0x06009A07 RID: 39431 RVA: 0x003199D0 File Offset: 0x00317BD0
		public void SendReconnectSuccess(float disconnectDuration, float reconnectDuration, string reconnectType)
		{
			this.EnqueueMessage(new ReconnectSuccess
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				DisconnectDuration = disconnectDuration,
				ReconnectDuration = reconnectDuration,
				ReconnectType = reconnectType
			}, null);
		}

		// Token: 0x06009A08 RID: 39432 RVA: 0x00319A1C File Offset: 0x00317C1C
		public void SendReconnectTimeout(string reconnectType)
		{
			this.EnqueueMessage(new ReconnectTimeout
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				ReconnectType = reconnectType
			}, null);
		}

		// Token: 0x06009A09 RID: 39433 RVA: 0x00319A58 File Offset: 0x00317C58
		public void SendRepairPrestep(int doubletapFingers, int locales)
		{
			this.EnqueueMessage(new RepairPrestep
			{
				DoubletapFingers = doubletapFingers,
				Locales = locales
			}, null);
		}

		// Token: 0x06009A0A RID: 39434 RVA: 0x00319A84 File Offset: 0x00317C84
		public void SendRestartDueToPlayerMigration()
		{
			this.EnqueueMessage(new RestartDueToPlayerMigration
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo()
			}, null);
		}

		// Token: 0x06009A0B RID: 39435 RVA: 0x00319AB8 File Offset: 0x00317CB8
		public void SendReturningPlayerDeckNotCreated(uint aBGroup)
		{
			this.EnqueueMessage(new ReturningPlayerDeckNotCreated
			{
				Player = this.GetPlayer(),
				ABGroup = aBGroup
			}, null);
		}

		// Token: 0x06009A0C RID: 39436 RVA: 0x00319AE8 File Offset: 0x00317CE8
		public void SendRuntimeUpdate(float duration, RuntimeUpdate.Intention intention_)
		{
			this.EnqueueMessage(new RuntimeUpdate
			{
				DeviceInfo = this.GetDeviceInfo(),
				Duration = duration,
				Intention_ = intention_
			}, null);
		}

		// Token: 0x06009A0D RID: 39437 RVA: 0x00319B20 File Offset: 0x00317D20
		public void SendSessionEnd(string applicationId)
		{
			this.EnqueueMessage(new SessionEnd
			{
				ApplicationId = applicationId
			}, null);
		}

		// Token: 0x06009A0E RID: 39438 RVA: 0x00319B44 File Offset: 0x00317D44
		public void SendSessionStart(string eventPayload, string applicationId)
		{
			this.EnqueueMessage(new SessionStart
			{
				EventPayload = eventPayload,
				ApplicationId = applicationId
			}, null);
		}

		// Token: 0x06009A0F RID: 39439 RVA: 0x00319B70 File Offset: 0x00317D70
		public void SendShopBalanceAvailable(List<Balance> balances)
		{
			this.EnqueueMessage(new ShopBalanceAvailable
			{
				Player = this.GetPlayer(),
				Balances = balances
			}, null);
		}

		// Token: 0x06009A10 RID: 39440 RVA: 0x00319BA0 File Offset: 0x00317DA0
		public void SendShopCardClick(ShopCard shopcard)
		{
			this.EnqueueMessage(new ShopCardClick
			{
				Player = this.GetPlayer(),
				Shopcard = shopcard
			}, null);
		}

		// Token: 0x06009A11 RID: 39441 RVA: 0x00319BD0 File Offset: 0x00317DD0
		public void SendShopPurchaseEvent(Product product, int quantity, string currency, double amount, bool isGift, string storefront, bool purchaseComplete, string storeType)
		{
			this.EnqueueMessage(new ShopPurchaseEvent
			{
				Player = this.GetPlayer(),
				Product = product,
				Quantity = quantity,
				Currency = currency,
				Amount = amount,
				IsGift = isGift,
				Storefront = storefront,
				PurchaseComplete = purchaseComplete,
				StoreType = storeType
			}, null);
		}

		// Token: 0x06009A12 RID: 39442 RVA: 0x00319C38 File Offset: 0x00317E38
		public void SendShopStatus(string error, double timeInHubSec)
		{
			this.EnqueueMessage(new ShopStatus
			{
				Player = this.GetPlayer(),
				Error = error,
				TimeInHubSec = timeInHubSec
			}, null);
		}

		// Token: 0x06009A13 RID: 39443 RVA: 0x00319C70 File Offset: 0x00317E70
		public void SendShopVisit(List<ShopCard> cards)
		{
			this.EnqueueMessage(new ShopVisit
			{
				Player = this.GetPlayer(),
				Cards = cards
			}, null);
		}

		// Token: 0x06009A14 RID: 39444 RVA: 0x00319CA0 File Offset: 0x00317EA0
		public void SendSmartDeckCompleteFailed(int requestMessageSize)
		{
			this.EnqueueMessage(new SmartDeckCompleteFailed
			{
				Player = this.GetPlayer(),
				RequestMessageSize = requestMessageSize
			}, null);
		}

		// Token: 0x06009A15 RID: 39445 RVA: 0x00319CD0 File Offset: 0x00317ED0
		public void SendStartupAudioSettings(bool deviceMuted, float deviceVolume, float masterVolume, float musicVolume)
		{
			this.EnqueueMessage(new StartupAudioSettings
			{
				DeviceInfo = this.GetDeviceInfo(),
				DeviceMuted = deviceMuted,
				DeviceVolume = deviceVolume,
				MasterVolume = masterVolume,
				MusicVolume = musicVolume
			}, null);
		}

		// Token: 0x06009A16 RID: 39446 RVA: 0x00319D18 File Offset: 0x00317F18
		public void SendTempAccountStoredInCloud(bool stored)
		{
			this.EnqueueMessage(new TempAccountStoredInCloud
			{
				DeviceInfo = this.GetDeviceInfo(),
				Stored = stored
			}, null);
		}

		// Token: 0x06009A17 RID: 39447 RVA: 0x00319D48 File Offset: 0x00317F48
		public void SendThirdPartyPurchaseCompletedFail(string transactionId, string productId, string bpayProvider, string errorInfo)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseCompletedFail
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId,
				BpayProvider = bpayProvider,
				ErrorInfo = errorInfo
			}, null);
		}

		// Token: 0x06009A18 RID: 39448 RVA: 0x00319D9C File Offset: 0x00317F9C
		public void SendThirdPartyPurchaseCompletedSuccess(string transactionId, string productId, string bpayProvider)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseCompletedSuccess
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId,
				BpayProvider = bpayProvider
			}, null);
		}

		// Token: 0x06009A19 RID: 39449 RVA: 0x00319DE8 File Offset: 0x00317FE8
		public void SendThirdPartyPurchaseDanglingReceiptFail(string transactionId, string productId, string provider, ThirdPartyPurchaseDanglingReceiptFail.FailureReason reason, string invalidData)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseDanglingReceiptFail
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId,
				Provider = provider,
				Reason = reason,
				InvalidData = invalidData
			}, null);
		}

		// Token: 0x06009A1A RID: 39450 RVA: 0x00319E44 File Offset: 0x00318044
		public void SendThirdPartyPurchaseDeferred(string transactionId, string productId)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseDeferred
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId
			}, null);
		}

		// Token: 0x06009A1B RID: 39451 RVA: 0x00319E88 File Offset: 0x00318088
		public void SendThirdPartyPurchaseMalformedData(string transactionId)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseMalformedData
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId
			}, null);
		}

		// Token: 0x06009A1C RID: 39452 RVA: 0x00319EC4 File Offset: 0x003180C4
		public void SendThirdPartyPurchaseReceiptFound(string transactionId, string productId)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseReceiptFound
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId
			}, null);
		}

		// Token: 0x06009A1D RID: 39453 RVA: 0x00319F08 File Offset: 0x00318108
		public void SendThirdPartyPurchaseReceiptNotFound(string transactionId, string productId)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseReceiptNotFound
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId
			}, null);
		}

		// Token: 0x06009A1E RID: 39454 RVA: 0x00319F4C File Offset: 0x0031814C
		public void SendThirdPartyPurchaseReceiptReceived(string transactionId, string productId)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseReceiptReceived
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId
			}, null);
		}

		// Token: 0x06009A1F RID: 39455 RVA: 0x00319F90 File Offset: 0x00318190
		public void SendThirdPartyPurchaseReceiptRequest(string transactionId, string productId)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseReceiptRequest
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId
			}, null);
		}

		// Token: 0x06009A20 RID: 39456 RVA: 0x00319FD4 File Offset: 0x003181D4
		public void SendThirdPartyPurchaseReceiptSubmitFail(string transactionId, string productId, string provider, ThirdPartyPurchaseReceiptSubmitFail.FailureReason reason, string invalidData)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseReceiptSubmitFail
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId,
				Provider = provider,
				Reason = reason,
				InvalidData = invalidData
			}, null);
		}

		// Token: 0x06009A21 RID: 39457 RVA: 0x0031A030 File Offset: 0x00318230
		public void SendThirdPartyPurchaseStart(string transactionId, string productId, string bpayProvider)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseStart
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				ProductId = productId,
				BpayProvider = bpayProvider
			}, null);
		}

		// Token: 0x06009A22 RID: 39458 RVA: 0x0031A07C File Offset: 0x0031827C
		public void SendThirdPartyPurchaseSubmitResponseDeviceNotification(string transactionId, bool success)
		{
			this.EnqueueMessage(new ThirdPartyPurchaseSubmitResponseDeviceNotification
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId,
				Success = success
			}, null);
		}

		// Token: 0x06009A23 RID: 39459 RVA: 0x0031A0C0 File Offset: 0x003182C0
		public void SendThirdPartyReceiptConsumed(string transactionId)
		{
			this.EnqueueMessage(new ThirdPartyReceiptConsumed
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				TransactionId = transactionId
			}, null);
		}

		// Token: 0x06009A24 RID: 39460 RVA: 0x0031A0FC File Offset: 0x003182FC
		public void SendThirdPartyUserIdUpdated(bool validChange)
		{
			this.EnqueueMessage(new ThirdPartyUserIdUpdated
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				ValidChange = validChange
			}, null);
		}

		// Token: 0x06009A25 RID: 39461 RVA: 0x0031A138 File Offset: 0x00318338
		public void SendVirtualCurrencyTransaction(string applicationId, string itemId, string itemCost, string itemQuantity, string currency, string payload)
		{
			this.EnqueueMessage(new VirtualCurrencyTransaction
			{
				ApplicationId = applicationId,
				ItemId = itemId,
				ItemCost = itemCost,
				ItemQuantity = itemQuantity,
				Currency = currency,
				Payload = payload
			}, null);
		}

		// Token: 0x06009A26 RID: 39462 RVA: 0x0031A184 File Offset: 0x00318384
		public void SendWebLoginError()
		{
			this.EnqueueMessage(new WebLoginError
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo()
			}, null);
		}

		// Token: 0x06009A27 RID: 39463 RVA: 0x0031A1B8 File Offset: 0x003183B8
		public void SendWelcomeQuestsAcknowledged(float questAckDuration)
		{
			this.EnqueueMessage(new WelcomeQuestsAcknowledged
			{
				Player = this.GetPlayer(),
				DeviceInfo = this.GetDeviceInfo(),
				QuestAckDuration = questAckDuration
			}, null);
		}

		// Token: 0x06009A28 RID: 39464 RVA: 0x0031A1F4 File Offset: 0x003183F4
		public void SendAttributionInstall(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, string referrer, string appleSearchAdsJson, int appleSearchAdsErrorCode, IdentifierInfo identifier)
		{
			this.EnqueueMessage(new AttributionInstall
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				Referrer = referrer,
				AppleSearchAdsJson = appleSearchAdsJson,
				AppleSearchAdsErrorCode = appleSearchAdsErrorCode,
				Identifier = identifier
			}, null);
		}

		// Token: 0x06009A29 RID: 39465 RVA: 0x0031A250 File Offset: 0x00318450
		public void SendAttributionLaunch(string applicationId, string deviceType, ulong firstInstallDate, string bundleId, int counter)
		{
			this.EnqueueMessage(new AttributionLaunch
			{
				ApplicationId = applicationId,
				DeviceType = deviceType,
				FirstInstallDate = firstInstallDate,
				BundleId = bundleId,
				Counter = counter
			}, null);
		}

		// Token: 0x06009A2A RID: 39466 RVA: 0x0031A294 File Offset: 0x00318494
		public void SendApkInstallFailure(string updatedVersion, string reason)
		{
			this.EnqueueMessage(new ApkInstallFailure
			{
				UpdatedVersion = updatedVersion,
				Reason = reason
			}, null);
		}

		// Token: 0x06009A2B RID: 39467 RVA: 0x0031A2C0 File Offset: 0x003184C0
		public void SendApkInstallSuccess(string updatedVersion, float availableSpaceMB, float elapsedSeconds)
		{
			this.EnqueueMessage(new ApkInstallSuccess
			{
				UpdatedVersion = updatedVersion,
				AvailableSpaceMB = availableSpaceMB,
				ElapsedSeconds = elapsedSeconds
			}, null);
		}

		// Token: 0x06009A2C RID: 39468 RVA: 0x0031A2F4 File Offset: 0x003184F4
		public void SendApkUpdate(int installedVersion, int assetVersion, int agentVersion)
		{
			this.EnqueueMessage(new ApkUpdate
			{
				InstalledVersion = installedVersion,
				AssetVersion = assetVersion,
				AgentVersion = agentVersion
			}, null);
		}

		// Token: 0x06009A2D RID: 39469 RVA: 0x0031A328 File Offset: 0x00318528
		public void SendCertificateRejected()
		{
			CertificateRejected message = new CertificateRejected();
			this.EnqueueMessage(message, null);
		}

		// Token: 0x06009A2E RID: 39470 RVA: 0x0031A344 File Offset: 0x00318544
		public void SendDeviceInfo(string androidId, string androidModel, uint androidSdkVersion, bool isConnectedToWifi, string gpuTextureFormat, string locale, string bnetRegion)
		{
			this.EnqueueMessage(new Blizzard.Telemetry.WTCG.NGDP.DeviceInfo
			{
				AndroidId = androidId,
				AndroidModel = androidModel,
				AndroidSdkVersion = androidSdkVersion,
				IsConnectedToWifi = isConnectedToWifi,
				GpuTextureFormat = gpuTextureFormat,
				Locale = locale,
				BnetRegion = bnetRegion
			}, null);
		}

		// Token: 0x06009A2F RID: 39471 RVA: 0x0031A398 File Offset: 0x00318598
		public void SendNotEnoughSpaceError(ulong availableSpace, ulong expectedOrgBytes, string filesDir)
		{
			this.EnqueueMessage(new NotEnoughSpaceError
			{
				AvailableSpace = availableSpace,
				ExpectedOrgBytes = expectedOrgBytes,
				FilesDir = filesDir
			}, null);
		}

		// Token: 0x06009A30 RID: 39472 RVA: 0x0031A3CC File Offset: 0x003185CC
		public void SendNoWifi(string updatedVersion, float availableSpaceMB, float elapsedSeconds)
		{
			this.EnqueueMessage(new NoWifi
			{
				UpdatedVersion = updatedVersion,
				AvailableSpaceMB = availableSpaceMB,
				ElapsedSeconds = elapsedSeconds
			}, null);
		}

		// Token: 0x06009A31 RID: 39473 RVA: 0x0031A400 File Offset: 0x00318600
		public void SendOpeningAppStore(string updatedVersion, float availableSpaceMB, float elapsedSeconds)
		{
			this.EnqueueMessage(new OpeningAppStore
			{
				UpdatedVersion = updatedVersion,
				AvailableSpaceMB = availableSpaceMB,
				ElapsedSeconds = elapsedSeconds
			}, null);
		}

		// Token: 0x06009A32 RID: 39474 RVA: 0x0031A434 File Offset: 0x00318634
		public void SendUncaughtException(string stackTrace, string androidModel, uint androidSdkVersion)
		{
			this.EnqueueMessage(new UncaughtException
			{
				StackTrace = stackTrace,
				AndroidModel = androidModel,
				AndroidSdkVersion = androidSdkVersion
			}, null);
		}

		// Token: 0x06009A33 RID: 39475 RVA: 0x0031A468 File Offset: 0x00318668
		public void SendUpdateError(uint errorCode, float elapsedSeconds)
		{
			this.EnqueueMessage(new UpdateError
			{
				ErrorCode = errorCode,
				ElapsedSeconds = elapsedSeconds
			}, null);
		}

		// Token: 0x06009A34 RID: 39476 RVA: 0x0031A494 File Offset: 0x00318694
		public void SendUpdateFinished(string updatedVersion, float availableSpaceMB, float elapsedSeconds)
		{
			this.EnqueueMessage(new UpdateFinished
			{
				UpdatedVersion = updatedVersion,
				AvailableSpaceMB = availableSpaceMB,
				ElapsedSeconds = elapsedSeconds
			}, null);
		}

		// Token: 0x06009A35 RID: 39477 RVA: 0x0031A4C8 File Offset: 0x003186C8
		public void SendUpdateProgress(float duration, long realDownloadBytes, long expectedDownloadBytes)
		{
			this.EnqueueMessage(new UpdateProgress
			{
				DeviceInfo = this.GetDeviceInfo(),
				Duration = duration,
				RealDownloadBytes = realDownloadBytes,
				ExpectedDownloadBytes = expectedDownloadBytes
			}, null);
		}

		// Token: 0x06009A36 RID: 39478 RVA: 0x0031A508 File Offset: 0x00318708
		public void SendUpdateStarted(string installedVersion, string textureFormat, string dataPath, float availableSpaceMB)
		{
			this.EnqueueMessage(new UpdateStarted
			{
				InstalledVersion = installedVersion,
				TextureFormat = textureFormat,
				DataPath = dataPath,
				AvailableSpaceMB = availableSpaceMB
			}, null);
		}

		// Token: 0x06009A37 RID: 39479 RVA: 0x0031A544 File Offset: 0x00318744
		public void SendUsingCellularData(string updatedVersion, float availableSpaceMB, float elapsedSeconds)
		{
			this.EnqueueMessage(new UsingCellularData
			{
				UpdatedVersion = updatedVersion,
				AvailableSpaceMB = availableSpaceMB,
				ElapsedSeconds = elapsedSeconds
			}, null);
		}

		// Token: 0x06009A38 RID: 39480 RVA: 0x0031A578 File Offset: 0x00318778
		public void SendVersionError(uint errorCode, uint agentState, string languages, string region, string branch, string additionalTags)
		{
			this.EnqueueMessage(new VersionError
			{
				ErrorCode = errorCode,
				AgentState = agentState,
				Languages = languages,
				Region = region,
				Branch = branch,
				AdditionalTags = additionalTags
			}, null);
		}

		// Token: 0x06009A39 RID: 39481 RVA: 0x0031A5C4 File Offset: 0x003187C4
		public void SendVersionFinished(string currentVersion, string liveVersion)
		{
			this.EnqueueMessage(new VersionFinished
			{
				CurrentVersion = currentVersion,
				LiveVersion = liveVersion
			}, null);
		}

		// Token: 0x06009A3A RID: 39482 RVA: 0x0031A5F0 File Offset: 0x003187F0
		public void SendVersionStarted(int dummy)
		{
			this.EnqueueMessage(new VersionStarted
			{
				Dummy = dummy
			}, null);
		}

		// Token: 0x06009A3B RID: 39483 RVA: 0x0031A614 File Offset: 0x00318814
		public void Initialize(Service telemetryService)
		{
			if (telemetryService == null)
			{
				throw new ArgumentNullException("telemetryService");
			}
			this.m_telemetryService = telemetryService;
			this.m_initialized = true;
			if (!this.m_deferredMessages.Any<TelemetryClient.DeferredMessage>())
			{
				return;
			}
			object deferredMessageLock = this.m_deferredMessageLock;
			TelemetryClient.DeferredMessage[] array;
			lock (deferredMessageLock)
			{
				array = this.m_deferredMessages.ToArray();
				this.m_deferredMessages.Clear();
			}
			foreach (TelemetryClient.DeferredMessage message in array)
			{
				this.EnqueueMessage(message);
			}
		}

		// Token: 0x06009A3C RID: 39484 RVA: 0x0031A6B4 File Offset: 0x003188B4
		public bool IsInitialized()
		{
			return this.m_initialized;
		}

		// Token: 0x06009A3D RID: 39485 RVA: 0x0031A6BC File Offset: 0x003188BC
		public void Disable()
		{
			this.m_disabled = true;
		}

		// Token: 0x06009A3E RID: 39486 RVA: 0x0031A6C5 File Offset: 0x003188C5
		public void OnUpdate()
		{
			if (!this.m_initialized || this.m_disabled || this.m_updateHandler == null)
			{
				return;
			}
			this.m_updateHandler();
		}

		// Token: 0x06009A3F RID: 39487 RVA: 0x0031A6EB File Offset: 0x003188EB
		public void RegisterUpdateHandler(Action updateHandler)
		{
			if (updateHandler == null)
			{
				throw new ArgumentNullException("updateHandler");
			}
			this.m_updateHandler = updateHandler;
		}

		// Token: 0x06009A40 RID: 39488 RVA: 0x0031A704 File Offset: 0x00318904
		public void Shutdown()
		{
			this.m_initialized = false;
			IDisposable disposable = this.m_telemetryService as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			this.m_telemetryService = null;
		}

		// Token: 0x06009A41 RID: 39489 RVA: 0x0031A734 File Offset: 0x00318934
		public long EnqueueMessage(string packageName, string messageName, byte[] data, MessageOptions options = null)
		{
			if (this.m_disabled || data.Length == 0)
			{
				return 0L;
			}
			if (!this.m_initialized)
			{
				object deferredMessageLock = this.m_deferredMessageLock;
				lock (deferredMessageLock)
				{
					this.m_deferredMessages.Add(new TelemetryClient.DeferredMessage
					{
						PackageName = packageName,
						MessageName = messageName,
						Data = data,
						Options = options
					});
				}
				return 0L;
			}
			return this.m_telemetryService.Enqueue(packageName, messageName, data, options);
		}

		// Token: 0x06009A42 RID: 39490 RVA: 0x0031A7C8 File Offset: 0x003189C8
		public void SendConnectSuccess(string name, string host = null, uint? port = null)
		{
			this.EnqueueMessage(new ConnectSuccess
			{
				Name = name,
				Host = host,
				Port = port
			}, null);
		}

		// Token: 0x06009A43 RID: 39491 RVA: 0x0031A7FC File Offset: 0x003189FC
		public void SendConnectFail(string name, string reason, string host = null, uint? port = null)
		{
			this.EnqueueMessage(new ConnectFail
			{
				Name = name,
				Reason = reason,
				Host = host,
				Port = port
			}, null);
		}

		// Token: 0x06009A44 RID: 39492 RVA: 0x0031A838 File Offset: 0x00318A38
		public void SendDisconnect(string name, Disconnect.Reason reason, string description = null, string host = null, uint? port = null)
		{
			this.EnqueueMessage(new Disconnect
			{
				Reason_ = new Disconnect.Reason?(reason),
				Name = name,
				Description = description,
				Host = host,
				Port = port
			}, null);
		}

		// Token: 0x06009A45 RID: 39493 RVA: 0x0031A87E File Offset: 0x00318A7E
		public void SendFindGameResult(FindGameResult message)
		{
			message.DeviceInfo = this.GetDeviceInfo();
			message.Player = this.GetPlayer();
			this.EnqueueMessage(message, null);
		}

		// Token: 0x06009A46 RID: 39494 RVA: 0x0031A8A1 File Offset: 0x00318AA1
		public void SendConnectToGameServer(ConnectToGameServer message)
		{
			message.DeviceInfo = this.GetDeviceInfo();
			message.Player = this.GetPlayer();
			this.EnqueueMessage(message, null);
		}

		// Token: 0x06009A47 RID: 39495 RVA: 0x0031A8C4 File Offset: 0x00318AC4
		public void SendTcpQualitySample(string address4, uint port, float sampleTimeMs, uint bytesSent, uint bytesReceived, uint messagesSent, uint messagesReceived)
		{
			this.EnqueueMessage(new TcpQualitySample
			{
				Address4 = address4,
				Port = new uint?(port),
				SampleTimeMs = new float?(sampleTimeMs),
				BytesSent = new ulong?((ulong)bytesSent),
				BytesReceived = new ulong?((ulong)bytesReceived),
				MessagesSent = new uint?(messagesSent),
				MessagesReceived = new uint?(messagesReceived)
			}, null);
		}

		// Token: 0x06009A48 RID: 39496 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void SendDevLogTelemetry(string category, string details)
		{
		}

		// Token: 0x06009A49 RID: 39497 RVA: 0x0031A938 File Offset: 0x00318B38
		public long EnqueueMessage(global::IProtoBuf message, MessageOptions options = null)
		{
			if (this.m_disabled || message == null)
			{
				return 0L;
			}
			MemoryStream memoryStream = new MemoryStream();
			message.Serialize(memoryStream);
			return this.EnqueueMessage(message.GetType(), memoryStream.ToArray(), options);
		}

		// Token: 0x06009A4A RID: 39498 RVA: 0x0031A974 File Offset: 0x00318B74
		private long EnqueueMessage(Blizzard.Proto.IProtoBuf message, MessageOptions options = null)
		{
			if (this.m_disabled || message == null)
			{
				return 0L;
			}
			if (!this.m_initialized)
			{
				object deferredMessageLock = this.m_deferredMessageLock;
				lock (deferredMessageLock)
				{
					this.m_deferredMessages.Add(new TelemetryClient.DeferredMessage
					{
						ProtoMessage = message,
						Options = options
					});
				}
				return 0L;
			}
			return this.m_telemetryService.Enqueue(message, options);
		}

		// Token: 0x06009A4B RID: 39499 RVA: 0x0031A9F4 File Offset: 0x00318BF4
		private long EnqueueMessage(Type messageType, byte[] data, MessageOptions options = null)
		{
			if (this.m_disabled || data.Length == 0)
			{
				return 0L;
			}
			if (!this.m_initialized)
			{
				object deferredMessageLock = this.m_deferredMessageLock;
				lock (deferredMessageLock)
				{
					this.m_deferredMessages.Add(new TelemetryClient.DeferredMessage
					{
						MessageType = messageType,
						Data = data,
						Options = options
					});
				}
				return 0L;
			}
			int num = messageType.FullName.LastIndexOf('.');
			return this.m_telemetryService.Enqueue(messageType.FullName.Substring(0, num), messageType.FullName.Substring(num + 1), data, options);
		}

		// Token: 0x06009A4C RID: 39500 RVA: 0x0031AAA4 File Offset: 0x00318CA4
		private void EnqueueMessage(TelemetryClient.DeferredMessage message)
		{
			if (message.ProtoMessage != null)
			{
				this.EnqueueMessage(message.ProtoMessage, message.Options);
				return;
			}
			if (string.IsNullOrEmpty(message.PackageName) || string.IsNullOrEmpty(message.MessageName))
			{
				this.EnqueueMessage(message.MessageType, message.Data, message.Options);
				return;
			}
			this.EnqueueMessage(message.PackageName, message.MessageName, message.Data, message.Options);
		}

		// Token: 0x06009A4D RID: 39501 RVA: 0x0031AB20 File Offset: 0x00318D20
		private Blizzard.Telemetry.WTCG.Client.DeviceInfo GetDeviceInfo()
		{
			if (this.m_deviceInfo == null)
			{
				int os = (int)PlatformSettings.OS;
				int screen = (int)PlatformSettings.Screen;
				this.m_deviceInfo = new Blizzard.Telemetry.WTCG.Client.DeviceInfo
				{
					Os = (Blizzard.Telemetry.WTCG.Client.DeviceInfo.OSCategory)os,
					OsVersion = SystemInfo.operatingSystem,
					Model = PlatformSettings.DeviceModel,
					Screen = (Blizzard.Telemetry.WTCG.Client.DeviceInfo.ScreenCategory)screen,
					DroidTextureCompression = ((PlatformSettings.OS == OSCategory.Android) ? AndroidDeviceSettings.Get().InstalledTexture : null)
				};
			}
			this.m_deviceInfo.ConnectionType_ = TelemetryClient.GetConnectionType();
			return this.m_deviceInfo;
		}

		// Token: 0x06009A4E RID: 39502 RVA: 0x0031ABA4 File Offset: 0x00318DA4
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

		// Token: 0x06009A4F RID: 39503 RVA: 0x0031AC0C File Offset: 0x00318E0C
		private static Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType GetConnectionType()
		{
			NetworkReachability internetReachability = Application.internetReachability;
			if (internetReachability == NetworkReachability.NotReachable)
			{
				return Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType.UNKNOWN;
			}
			if (internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
			{
				return Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType.CELLULAR;
			}
			if (PlatformSettings.OS != OSCategory.Android && PlatformSettings.OS != OSCategory.iOS)
			{
				return Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType.WIRED;
			}
			return Blizzard.Telemetry.WTCG.Client.DeviceInfo.ConnectionType.WIFI;
		}

		// Token: 0x04007FFE RID: 32766
		private readonly List<TelemetryClient.DeferredMessage> m_deferredMessages = new List<TelemetryClient.DeferredMessage>();

		// Token: 0x04007FFF RID: 32767
		private readonly object m_deferredMessageLock = new object();

		// Token: 0x04008000 RID: 32768
		private Service m_telemetryService;

		// Token: 0x04008001 RID: 32769
		private bool m_disabled;

		// Token: 0x04008002 RID: 32770
		private bool m_initialized;

		// Token: 0x04008003 RID: 32771
		private Action m_updateHandler;

		// Token: 0x04008004 RID: 32772
		private Blizzard.Telemetry.WTCG.Client.DeviceInfo m_deviceInfo;

		// Token: 0x02002785 RID: 10117
		private class DeferredMessage
		{
			// Token: 0x17002CE7 RID: 11495
			// (get) Token: 0x06013A3C RID: 80444 RVA: 0x00539234 File Offset: 0x00537434
			// (set) Token: 0x06013A3D RID: 80445 RVA: 0x0053923C File Offset: 0x0053743C
			public Blizzard.Proto.IProtoBuf ProtoMessage { get; set; }

			// Token: 0x17002CE8 RID: 11496
			// (get) Token: 0x06013A3E RID: 80446 RVA: 0x00539245 File Offset: 0x00537445
			// (set) Token: 0x06013A3F RID: 80447 RVA: 0x0053924D File Offset: 0x0053744D
			public string PackageName { get; set; }

			// Token: 0x17002CE9 RID: 11497
			// (get) Token: 0x06013A40 RID: 80448 RVA: 0x00539256 File Offset: 0x00537456
			// (set) Token: 0x06013A41 RID: 80449 RVA: 0x0053925E File Offset: 0x0053745E
			public string MessageName { get; set; }

			// Token: 0x17002CEA RID: 11498
			// (get) Token: 0x06013A42 RID: 80450 RVA: 0x00539267 File Offset: 0x00537467
			// (set) Token: 0x06013A43 RID: 80451 RVA: 0x0053926F File Offset: 0x0053746F
			public Type MessageType { get; set; }

			// Token: 0x17002CEB RID: 11499
			// (get) Token: 0x06013A44 RID: 80452 RVA: 0x00539278 File Offset: 0x00537478
			// (set) Token: 0x06013A45 RID: 80453 RVA: 0x00539280 File Offset: 0x00537480
			public byte[] Data { get; set; }

			// Token: 0x17002CEC RID: 11500
			// (get) Token: 0x06013A46 RID: 80454 RVA: 0x00539289 File Offset: 0x00537489
			// (set) Token: 0x06013A47 RID: 80455 RVA: 0x00539291 File Offset: 0x00537491
			public MessageOptions Options { get; set; }
		}
	}
}
