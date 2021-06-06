using System;
using Blizzard.Telemetry.CRM;
using HearthstoneTelemetry;
using MiniJSON;

namespace Hearthstone.CRM
{
	public class BlizzardCRMManager
	{
		private static BlizzardCRMManager s_instance;

		private readonly ITelemetryClient m_telemetryClient;

		private BlizzardCRMManager()
		{
			m_telemetryClient = TelemetryManager.Client();
		}

		public static BlizzardCRMManager Get()
		{
			if (s_instance == null)
			{
				s_instance = new BlizzardCRMManager();
			}
			return s_instance;
		}

		public void SendEvent_PushRegistration(string token)
		{
			SendEvent(new PushRegistration
			{
				PushId = token,
				UtcOffset = (int)(DateTime.Now - DateTime.UtcNow).TotalSeconds,
				Timezone = TimeZoneInfo.Local.StandardName,
				ApplicationId = TelemetryManager.GetApplicationId(),
				Language = Localization.GetLocaleName(),
				Os = PlatformSettings.OS.ToString()
			});
		}

		public void SendEvent_SessionStart(JsonNode payload)
		{
			SendEvent(new SessionStart
			{
				EventPayload = Json.Serialize(payload),
				ApplicationId = TelemetryManager.GetApplicationId()
			});
		}

		public void SendEvent_SessionEnd()
		{
			SendEvent(new SessionEnd
			{
				ApplicationId = TelemetryManager.GetApplicationId()
			});
		}

		public void SendEvent_CRMEvent(string eventName, JsonNode payload)
		{
			CrmEvent crmEvent = new CrmEvent
			{
				EventName = eventName,
				ApplicationId = TelemetryManager.GetApplicationId()
			};
			if (payload != null)
			{
				crmEvent.EventPayload = Json.Serialize(payload);
			}
			SendEvent(crmEvent);
		}

		public void SendEvent_PushEvent(string campaignId, JsonNode payload)
		{
			SendEvent(new PushEvent
			{
				CampaignId = campaignId,
				EventPayload = Json.Serialize(payload),
				ApplicationId = TelemetryManager.GetApplicationId()
			});
		}

		public void SendEvent_RealMoneyTransaction(string productId, string transactionId, int itemQuantity, string localCurrency, float itemCost)
		{
			SendEvent(new RealMoneyTransaction
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				AppStore = "unknown_store",
				ItemCost = itemCost.ToString(),
				LocalCurrency = localCurrency,
				TransactionId = transactionId,
				ProductId = productId,
				ItemQuantity = itemQuantity.ToString()
			});
		}

		public void SendEvent_VirtualCurrencyTransaction(string itemId, int itemCost, int itemQuantity, string currency, JsonNode payload)
		{
			SendEvent(new VirtualCurrencyTransaction
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				ItemId = itemId,
				ItemCost = itemCost.ToString(),
				ItemQuantity = itemQuantity.ToString(),
				Currency = currency,
				Payload = Json.Serialize(payload)
			});
		}

		private void SendEvent(IProtoBuf eventMessage)
		{
			if (PlatformSettings.OS != OSCategory.PC && PlatformSettings.OS != OSCategory.Mac)
			{
				if (HearthstoneApplication.IsInternal())
				{
					Log.CRM.Print("Sending event {0}: {1}", eventMessage.GetType().Name, eventMessage.ToHumanReadableString().Replace("\n", ""));
				}
				m_telemetryClient.EnqueueMessage(eventMessage);
			}
		}

		public void SendAllEventsForTest()
		{
			SendEvent_CRMEvent("TestCRMEvent", null);
			SendEvent_PushEvent("TestPushEvent", null);
			SendEvent_PushRegistration("TestPushRegistration");
			SendEvent_RealMoneyTransaction("TestProduct", "TestTransaction", 1, "USD", 1337f);
			SendEvent_SessionStart(null);
			SendEvent_VirtualCurrencyTransaction("TestItem", 1337, 1, "gold", null);
		}
	}
}
