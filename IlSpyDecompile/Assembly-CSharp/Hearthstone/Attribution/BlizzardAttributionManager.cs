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
	public class BlizzardAttributionManager
	{
		private const string AF_EVENT_CONTENT_UNLOCKED = "content_unlocked";

		private const string AF_EVENT_FIRST_LOGIN = "first_login";

		private const string AF_EVENT_HEADLESS_ACCT_CREATE = "headless_account_created";

		private const string AF_EVENT_HEADLESS_ACCT_HEALUP = "headless_account_healed_up";

		private const string AF_EVENT_LOGIN = "login";

		private const string AF_EVENT_PURCHASE = "af_purchase";

		private const string AF_EVENT_REGISTRATION = "registration";

		private const string AF_EVENT_SCENARIO_RESULT = "scenario_result";

		private const string AF_EVENT_TUTORIAL_COMPLETE = "tutorial_complete";

		private static BlizzardAttributionManager s_instance;

		private readonly ITelemetryClient m_telemetryClient;

		private BlizzardAttributionManager()
		{
			m_telemetryClient = TelemetryManager.Client();
		}

		public static BlizzardAttributionManager Get()
		{
			if (s_instance == null)
			{
				s_instance = new BlizzardAttributionManager();
			}
			return s_instance;
		}

		public void SendEvent_Install()
		{
			if (PlatformSettings.RuntimeOS == OSCategory.iOS)
			{
				Processor.RunCoroutine(AppleSearchAds.RequestAsync(OnIOSInstallCompleted));
				return;
			}
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				Processor.RunCoroutine(InstallReferrer.RequestCoroutine(OnAndroidInstallCompleted));
				return;
			}
			SendEvent(new AttributionInstall
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			});
		}

		public void SendEvent_Launch()
		{
			SendEvent(new AttributionLaunch
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				Counter = AppLaunchTracker.LaunchCount
			});
		}

		public void SendEvent_Login()
		{
			SendEvent(new AttributionLogin
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			});
			SendAppsFlyerEvent("login", new Dictionary<string, string>());
		}

		public void SendEvent_FirstLogin()
		{
			SendEvent(new AttributionFirstLogin
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			});
			SendAppsFlyerEvent("first_login", new Dictionary<string, string>());
		}

		public void SendEvent_Registration()
		{
			SendEvent(new AttributionRegistration
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			});
			SendAppsFlyerEvent("registration", new Dictionary<string, string>());
		}

		public void SendEvent_HeadlessAccountCreated()
		{
			SendEvent(new AttributionHeadlessAccountCreated
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel
			});
			SendAppsFlyerEvent("headless_account_created", new Dictionary<string, string>());
		}

		public void SendEvent_HeadlessAccountHealedUp(string temporaryGameAccountId)
		{
			SendEvent(new AttributionHeadlessAccountHealedUp
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				TemporaryGameAccountId = temporaryGameAccountId
			});
			SendAppsFlyerEvent("headless_account_healed_up", new Dictionary<string, string> { { "temporary_game_account_id", temporaryGameAccountId } });
		}

		public void SendEvent_ContentUnlocked(string contentId)
		{
			SendEvent(new AttributionContentUnlocked
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				ContentId = contentId
			});
			SendAppsFlyerEvent("content_unlocked", new Dictionary<string, string> { { "content_id", contentId } });
		}

		public void SendEvent_ScenarioResult(int scenarioId, string result, int bossId)
		{
			SendEvent(new AttributionScenarioResult
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				ScenarioId = scenarioId,
				Result = result,
				BossId = bossId
			});
			SendAppsFlyerEvent("scenario_result", new Dictionary<string, string>
			{
				{
					"scenario_id",
					scenarioId.ToString(CultureInfo.InvariantCulture)
				},
				{ "result", result },
				{
					"boss_id",
					bossId.ToString(CultureInfo.InvariantCulture)
				}
			});
		}

		public void SendEvent_TutorialComplete(string tutorialType)
		{
			SendAppsFlyerEvent("tutorial_complete", new Dictionary<string, string> { { "tutorial_type", tutorialType } });
		}

		public void SendEvent_GameRoundEnd(string gameMode, string result, PegasusShared.FormatType formatType)
		{
			SendEvent(new AttributionGameRoundEnd
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

		public void SendEvent_GameRoundStart(string gameMode, PegasusShared.FormatType formatType)
		{
			SendEvent(new AttributionGameRoundStart
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				GameMode = gameMode,
				FormatType = (Blizzard.Telemetry.WTCG.Client.FormatType)formatType
			});
		}

		public void SendEvent_Purchase(string productId, string transactionId, int quantity, string currencyCode, bool isVirtualCurrency, float amount)
		{
			AttributionPurchase.PaymentInfo item = new AttributionPurchase.PaymentInfo
			{
				CurrencyCode = currencyCode,
				IsVirtualCurrency = isVirtualCurrency,
				Amount = amount
			};
			List<AttributionPurchase.PaymentInfo> payments = new List<AttributionPurchase.PaymentInfo> { item };
			SendEvent(new AttributionPurchase
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
			SendAppsFlyerEvent("af_purchase", new Dictionary<string, string>
			{
				{ "af_content_type", productId },
				{ "af_receipt_id", transactionId },
				{
					"af_quantity",
					quantity.ToString(CultureInfo.InvariantCulture)
				},
				{ "af_currency", currencyCode },
				{
					"af_revenue",
					amount.ToString(CultureInfo.InvariantCulture)
				}
			});
		}

		public void SendEvent_VirtualCurrencyTransaction(int amount, string currencyName)
		{
			SendEvent(new AttributionVirtualCurrencyTransaction
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				BundleId = Application.identifier,
				FirstInstallDate = AppLaunchTracker.FirstInstallTimeMilliseconds,
				DeviceType = SystemInfo.deviceModel,
				Amount = amount,
				Currency = currencyName
			});
		}

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
			SendEvent(attributionInstall);
		}

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
				Log.Telemetry.Print("Error while requesting the referrer URL.");
			}
			else
			{
				attributionInstall.Referrer = referrerUrl;
			}
			SendEvent(attributionInstall);
		}

		private void OnInstallTelemetryMessageReceivedByIngest(long messageId)
		{
			if (messageId == 0L)
			{
				Log.Telemetry.PrintError("There was an error sending the 'Attirbution_Install' telemetry event to ingest. Received ({0}) identifier back from telemetry SDK.", messageId);
			}
			else
			{
				AppLaunchTracker.IsInstallReported = true;
			}
		}

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
			long messageId = m_telemetryClient.EnqueueMessage(eventMessage, new MessageOptions
			{
				IdentifierInfoExtensionEnabled = true
			});
			if (eventMessage.GetType().Name.Equals("AttributionInstall"))
			{
				TelemetryManager.RegisterMessageSentCallback(messageId, OnInstallTelemetryMessageReceivedByIngest);
				TelemetryManager.Flush();
			}
		}

		private void SendAppsFlyerEvent(string eventName, Dictionary<string, string> eventParams)
		{
			HsAppsFlyer.SendEvent(eventName, eventParams);
		}

		public void SendAllEventsForTest()
		{
		}
	}
}
