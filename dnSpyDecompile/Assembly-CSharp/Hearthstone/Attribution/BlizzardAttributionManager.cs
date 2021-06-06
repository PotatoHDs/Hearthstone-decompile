using System;
using System.Collections.Generic;
using System.Globalization;
using Blizzard.Telemetry;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.Core;
using HearthstoneTelemetry;
using PegasusShared;
using UnityEngine;

namespace Hearthstone.Attribution
{
	// Token: 0x020010E4 RID: 4324
	public class BlizzardAttributionManager
	{
		// Token: 0x0600BDF9 RID: 48633 RVA: 0x0039E759 File Offset: 0x0039C959
		private BlizzardAttributionManager()
		{
			this.m_telemetryClient = TelemetryManager.Client();
		}

		// Token: 0x0600BDFA RID: 48634 RVA: 0x0039E76C File Offset: 0x0039C96C
		public static BlizzardAttributionManager Get()
		{
			if (BlizzardAttributionManager.s_instance == null)
			{
				BlizzardAttributionManager.s_instance = new BlizzardAttributionManager();
			}
			return BlizzardAttributionManager.s_instance;
		}

		// Token: 0x0600BDFB RID: 48635 RVA: 0x0039E784 File Offset: 0x0039C984
		public void SendEvent_Install()
		{
			if (PlatformSettings.RuntimeOS == OSCategory.iOS)
			{
				Processor.RunCoroutine(AppleSearchAds.RequestAsync(new AppleSearchAds.AttributionCallback(this.OnIOSInstallCompleted)), null);
				return;
			}
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				Processor.RunCoroutine(InstallReferrer.RequestCoroutine(new InstallReferrer.ReferrerReceivedCallback(this.OnAndroidInstallCompleted)), null);
				return;
			}
			this.SendEvent(new AttributionInstall
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			});
		}

		// Token: 0x0600BDFC RID: 48636 RVA: 0x0039E80C File Offset: 0x0039CA0C
		public void SendEvent_Launch()
		{
			this.SendEvent(new AttributionLaunch
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				Counter = AppLaunchTracker.LaunchCount
			});
		}

		// Token: 0x0600BDFD RID: 48637 RVA: 0x0039E85C File Offset: 0x0039CA5C
		public void SendEvent_Login()
		{
			this.SendEvent(new AttributionLogin
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			});
			this.SendAppsFlyerEvent("login", new Dictionary<string, string>());
		}

		// Token: 0x0600BDFE RID: 48638 RVA: 0x0039E8B0 File Offset: 0x0039CAB0
		public void SendEvent_FirstLogin()
		{
			this.SendEvent(new AttributionFirstLogin
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			});
			this.SendAppsFlyerEvent("first_login", new Dictionary<string, string>());
		}

		// Token: 0x0600BDFF RID: 48639 RVA: 0x0039E904 File Offset: 0x0039CB04
		public void SendEvent_Registration()
		{
			this.SendEvent(new AttributionRegistration
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			});
			this.SendAppsFlyerEvent("registration", new Dictionary<string, string>());
		}

		// Token: 0x0600BE00 RID: 48640 RVA: 0x0039E958 File Offset: 0x0039CB58
		public void SendEvent_HeadlessAccountCreated()
		{
			this.SendEvent(new AttributionHeadlessAccountCreated
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			});
			this.SendAppsFlyerEvent("headless_account_created", new Dictionary<string, string>());
		}

		// Token: 0x0600BE01 RID: 48641 RVA: 0x0039E9AC File Offset: 0x0039CBAC
		public void SendEvent_HeadlessAccountHealedUp(string temporaryGameAccountId)
		{
			this.SendEvent(new AttributionHeadlessAccountHealedUp
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				TemporaryGameAccountId = temporaryGameAccountId
			});
			this.SendAppsFlyerEvent("headless_account_healed_up", new Dictionary<string, string>
			{
				{
					"temporary_game_account_id",
					temporaryGameAccountId
				}
			});
		}

		// Token: 0x0600BE02 RID: 48642 RVA: 0x0039EA14 File Offset: 0x0039CC14
		public void SendEvent_ContentUnlocked(string contentId)
		{
			this.SendEvent(new AttributionContentUnlocked
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				ContentId = contentId
			});
			this.SendAppsFlyerEvent("content_unlocked", new Dictionary<string, string>
			{
				{
					"content_id",
					contentId
				}
			});
		}

		// Token: 0x0600BE03 RID: 48643 RVA: 0x0039EA7C File Offset: 0x0039CC7C
		public void SendEvent_ScenarioResult(int scenarioId, string result, int bossId)
		{
			this.SendEvent(new AttributionScenarioResult
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				ScenarioId = scenarioId,
				Result = result,
				BossId = bossId
			});
			this.SendAppsFlyerEvent("scenario_result", new Dictionary<string, string>
			{
				{
					"scenario_id",
					scenarioId.ToString(CultureInfo.InvariantCulture)
				},
				{
					"result",
					result
				},
				{
					"boss_id",
					bossId.ToString(CultureInfo.InvariantCulture)
				}
			});
		}

		// Token: 0x0600BE04 RID: 48644 RVA: 0x0039EB1F File Offset: 0x0039CD1F
		public void SendEvent_TutorialComplete(string tutorialType)
		{
			this.SendAppsFlyerEvent("tutorial_complete", new Dictionary<string, string>
			{
				{
					"tutorial_type",
					tutorialType
				}
			});
		}

		// Token: 0x0600BE05 RID: 48645 RVA: 0x0039EB40 File Offset: 0x0039CD40
		public void SendEvent_GameRoundEnd(string gameMode, string result, PegasusShared.FormatType formatType)
		{
			this.SendEvent(new AttributionGameRoundEnd
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				GameMode = gameMode,
				Result = result,
				FormatType = (Blizzard.Telemetry.WTCG.Client.FormatType)formatType
			});
		}

		// Token: 0x0600BE06 RID: 48646 RVA: 0x0039EB9C File Offset: 0x0039CD9C
		public void SendEvent_GameRoundStart(string gameMode, PegasusShared.FormatType formatType)
		{
			this.SendEvent(new AttributionGameRoundStart
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				GameMode = gameMode,
				FormatType = (Blizzard.Telemetry.WTCG.Client.FormatType)formatType
			});
		}

		// Token: 0x0600BE07 RID: 48647 RVA: 0x0039EBF0 File Offset: 0x0039CDF0
		public void SendEvent_Purchase(string productId, string transactionId, int quantity, string currencyCode, bool isVirtualCurrency, float amount)
		{
			AttributionPurchase.PaymentInfo item = new AttributionPurchase.PaymentInfo
			{
				CurrencyCode = currencyCode,
				IsVirtualCurrency = isVirtualCurrency,
				Amount = amount
			};
			List<AttributionPurchase.PaymentInfo> payments = new List<AttributionPurchase.PaymentInfo>
			{
				item
			};
			this.SendEvent(new AttributionPurchase
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				PurchaseType = productId,
				TransactionId = transactionId,
				Quantity = quantity,
				Payments = payments
			});
			this.SendAppsFlyerEvent("af_purchase", new Dictionary<string, string>
			{
				{
					"af_content_type",
					productId
				},
				{
					"af_receipt_id",
					transactionId
				},
				{
					"af_quantity",
					quantity.ToString(CultureInfo.InvariantCulture)
				},
				{
					"af_currency",
					currencyCode
				},
				{
					"af_revenue",
					amount.ToString(CultureInfo.InvariantCulture)
				}
			});
		}

		// Token: 0x0600BE08 RID: 48648 RVA: 0x0039ECE0 File Offset: 0x0039CEE0
		public void SendEvent_VirtualCurrencyTransaction(int amount, string currencyName)
		{
			this.SendEvent(new AttributionVirtualCurrencyTransaction
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				Amount = (float)amount,
				Currency = currencyName
			});
		}

		// Token: 0x0600BE09 RID: 48649 RVA: 0x0039ED34 File Offset: 0x0039CF34
		public void OnIOSInstallCompleted(bool success, string jsonString, int errorCode, string errorMessage)
		{
			AttributionInstall attributionInstall = new AttributionInstall
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			};
			if (success)
			{
				attributionInstall.AppleSearchAdsJson = jsonString;
			}
			else
			{
				attributionInstall.AppleSearchAdsErrorCode = errorCode;
			}
			this.SendEvent(attributionInstall);
		}

		// Token: 0x0600BE0A RID: 48650 RVA: 0x0039ED90 File Offset: 0x0039CF90
		public void OnAndroidInstallCompleted(int responseCode, string referrerUrl)
		{
			AttributionInstall attributionInstall = new AttributionInstall
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			};
			if (responseCode < 0)
			{
				global::Log.Telemetry.Print("Error while requesting the referrer URL.", Array.Empty<object>());
			}
			else
			{
				attributionInstall.Referrer = referrerUrl;
			}
			this.SendEvent(attributionInstall);
		}

		// Token: 0x0600BE0B RID: 48651 RVA: 0x0039EDF7 File Offset: 0x0039CFF7
		private void OnInstallTelemetryMessageReceivedByIngest(long messageId)
		{
			if (messageId == 0L)
			{
				global::Log.Telemetry.PrintError("There was an error sending the 'Attirbution_Install' telemetry event to ingest. Received ({0}) identifier back from telemetry SDK.", new object[]
				{
					messageId
				});
				return;
			}
			AppLaunchTracker.IsInstallReported = true;
		}

		// Token: 0x0600BE0C RID: 48652 RVA: 0x0039EE24 File Offset: 0x0039D024
		private void SendEvent(IProtoBuf eventMessage)
		{
			if (PlatformSettings.OS == OSCategory.PC || PlatformSettings.OS == OSCategory.Mac)
			{
				if (eventMessage.GetType().Name.Equals("AttributionInstall"))
				{
					AppLaunchTracker.IsInstallReported = true;
				}
				return;
			}
			new MessageOptions().IdentifierInfoExtensionEnabled = true;
			long messageId = this.m_telemetryClient.EnqueueMessage(eventMessage, new MessageOptions
			{
				IdentifierInfoExtensionEnabled = true
			});
			if (eventMessage.GetType().Name.Equals("AttributionInstall"))
			{
				TelemetryManager.RegisterMessageSentCallback(messageId, new Action<long>(this.OnInstallTelemetryMessageReceivedByIngest));
				TelemetryManager.Flush();
			}
		}

		// Token: 0x0600BE0D RID: 48653 RVA: 0x0039EEB3 File Offset: 0x0039D0B3
		private void SendAppsFlyerEvent(string eventName, Dictionary<string, string> eventParams)
		{
			HsAppsFlyer.SendEvent(eventName, eventParams);
		}

		// Token: 0x0600BE0E RID: 48654 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void SendAllEventsForTest()
		{
		}

		// Token: 0x04009AAD RID: 39597
		private const string AF_EVENT_CONTENT_UNLOCKED = "content_unlocked";

		// Token: 0x04009AAE RID: 39598
		private const string AF_EVENT_FIRST_LOGIN = "first_login";

		// Token: 0x04009AAF RID: 39599
		private const string AF_EVENT_HEADLESS_ACCT_CREATE = "headless_account_created";

		// Token: 0x04009AB0 RID: 39600
		private const string AF_EVENT_HEADLESS_ACCT_HEALUP = "headless_account_healed_up";

		// Token: 0x04009AB1 RID: 39601
		private const string AF_EVENT_LOGIN = "login";

		// Token: 0x04009AB2 RID: 39602
		private const string AF_EVENT_PURCHASE = "af_purchase";

		// Token: 0x04009AB3 RID: 39603
		private const string AF_EVENT_REGISTRATION = "registration";

		// Token: 0x04009AB4 RID: 39604
		private const string AF_EVENT_SCENARIO_RESULT = "scenario_result";

		// Token: 0x04009AB5 RID: 39605
		private const string AF_EVENT_TUTORIAL_COMPLETE = "tutorial_complete";

		// Token: 0x04009AB6 RID: 39606
		private static BlizzardAttributionManager s_instance;

		// Token: 0x04009AB7 RID: 39607
		private readonly ITelemetryClient m_telemetryClient;
	}
}
