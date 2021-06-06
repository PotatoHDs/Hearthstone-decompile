using System;
using Blizzard.Telemetry.CRM;
using HearthstoneTelemetry;
using MiniJSON;

namespace Hearthstone.CRM
{
	// Token: 0x020010E3 RID: 4323
	public class BlizzardCRMManager
	{
		// Token: 0x0600BDEE RID: 48622 RVA: 0x0039E479 File Offset: 0x0039C679
		private BlizzardCRMManager()
		{
			this.m_telemetryClient = TelemetryManager.Client();
		}

		// Token: 0x0600BDEF RID: 48623 RVA: 0x0039E48C File Offset: 0x0039C68C
		public static BlizzardCRMManager Get()
		{
			if (BlizzardCRMManager.s_instance == null)
			{
				BlizzardCRMManager.s_instance = new BlizzardCRMManager();
			}
			return BlizzardCRMManager.s_instance;
		}

		// Token: 0x0600BDF0 RID: 48624 RVA: 0x0039E4A4 File Offset: 0x0039C6A4
		public void SendEvent_PushRegistration(string token)
		{
			this.SendEvent(new PushRegistration
			{
				PushId = token,
				UtcOffset = (int)(DateTime.Now - DateTime.UtcNow).TotalSeconds,
				Timezone = TimeZoneInfo.Local.StandardName,
				ApplicationId = TelemetryManager.GetApplicationId(),
				Language = Localization.GetLocaleName(),
				Os = PlatformSettings.OS.ToString()
			});
		}

		// Token: 0x0600BDF1 RID: 48625 RVA: 0x0039E520 File Offset: 0x0039C720
		public void SendEvent_SessionStart(JsonNode payload)
		{
			this.SendEvent(new SessionStart
			{
				EventPayload = Json.Serialize(payload),
				ApplicationId = TelemetryManager.GetApplicationId()
			});
		}

		// Token: 0x0600BDF2 RID: 48626 RVA: 0x0039E544 File Offset: 0x0039C744
		public void SendEvent_SessionEnd()
		{
			this.SendEvent(new SessionEnd
			{
				ApplicationId = TelemetryManager.GetApplicationId()
			});
		}

		// Token: 0x0600BDF3 RID: 48627 RVA: 0x0039E55C File Offset: 0x0039C75C
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
			this.SendEvent(crmEvent);
		}

		// Token: 0x0600BDF4 RID: 48628 RVA: 0x0039E597 File Offset: 0x0039C797
		public void SendEvent_PushEvent(string campaignId, JsonNode payload)
		{
			this.SendEvent(new PushEvent
			{
				CampaignId = campaignId,
				EventPayload = Json.Serialize(payload),
				ApplicationId = TelemetryManager.GetApplicationId()
			});
		}

		// Token: 0x0600BDF5 RID: 48629 RVA: 0x0039E5C4 File Offset: 0x0039C7C4
		public void SendEvent_RealMoneyTransaction(string productId, string transactionId, int itemQuantity, string localCurrency, float itemCost)
		{
			this.SendEvent(new RealMoneyTransaction
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

		// Token: 0x0600BDF6 RID: 48630 RVA: 0x0039E624 File Offset: 0x0039C824
		public void SendEvent_VirtualCurrencyTransaction(string itemId, int itemCost, int itemQuantity, string currency, JsonNode payload)
		{
			this.SendEvent(new VirtualCurrencyTransaction
			{
				ApplicationId = TelemetryManager.GetApplicationId(),
				ItemId = itemId,
				ItemCost = itemCost.ToString(),
				ItemQuantity = itemQuantity.ToString(),
				Currency = currency,
				Payload = Json.Serialize(payload)
			});
		}

		// Token: 0x0600BDF7 RID: 48631 RVA: 0x0039E680 File Offset: 0x0039C880
		private void SendEvent(IProtoBuf eventMessage)
		{
			if (PlatformSettings.OS == OSCategory.PC || PlatformSettings.OS == OSCategory.Mac)
			{
				return;
			}
			if (HearthstoneApplication.IsInternal())
			{
				Log.CRM.Print("Sending event {0}: {1}", new object[]
				{
					eventMessage.GetType().Name,
					eventMessage.ToHumanReadableString().Replace("\n", "")
				});
			}
			this.m_telemetryClient.EnqueueMessage(eventMessage, null);
		}

		// Token: 0x0600BDF8 RID: 48632 RVA: 0x0039E6F0 File Offset: 0x0039C8F0
		public void SendAllEventsForTest()
		{
			this.SendEvent_CRMEvent("TestCRMEvent", null);
			this.SendEvent_PushEvent("TestPushEvent", null);
			this.SendEvent_PushRegistration("TestPushRegistration");
			this.SendEvent_RealMoneyTransaction("TestProduct", "TestTransaction", 1, "USD", 1337f);
			this.SendEvent_SessionStart(null);
			this.SendEvent_VirtualCurrencyTransaction("TestItem", 1337, 1, "gold", null);
		}

		// Token: 0x04009AAB RID: 39595
		private static BlizzardCRMManager s_instance;

		// Token: 0x04009AAC RID: 39596
		private readonly ITelemetryClient m_telemetryClient;
	}
}
