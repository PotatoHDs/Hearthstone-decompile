using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.Core;
using Hearthstone.InGameMessage.UI;

namespace Hearthstone.InGameMessage
{
	// Token: 0x0200115A RID: 4442
	public class MessageFeedRegistry
	{
		// Token: 0x0600C2AF RID: 49839 RVA: 0x003AFB4D File Offset: 0x003ADD4D
		public static MessageFeedRegistry Get()
		{
			return MessageFeedRegistry.s_instance;
		}

		// Token: 0x0600C2B0 RID: 49840 RVA: 0x003AFB54 File Offset: 0x003ADD54
		public MessageFeedRegistry()
		{
			MessageFeedRegistry.s_instance = this;
		}

		// Token: 0x0600C2B1 RID: 49841 RVA: 0x003AFB78 File Offset: 0x003ADD78
		public void RegisterDefaultFeeds()
		{
			MessageContentFeed feed = new MessageContentFeed
			{
				ContentId = "in_game_message_box_hub",
				DataTranslator = new StandardIGMTranslator()
			};
			this.RegisterFeed(feed);
		}

		// Token: 0x0600C2B2 RID: 49842 RVA: 0x003AFBA8 File Offset: 0x003ADDA8
		public void RegisterFeed(MessageContentFeed feed)
		{
			if (feed == null || string.IsNullOrEmpty(feed.ContentId))
			{
				Log.InGameMessage.PrintWarning("Attempted to register a feed with invalid/missing arguments", Array.Empty<object>());
				return;
			}
			if (this.m_feeds.Exists((MessageContentFeed x) => x.ContentId.Equals(feed.ContentId)))
			{
				Log.InGameMessage.PrintWarning("Attempted to re-register an existing feed", Array.Empty<object>());
				return;
			}
			this.m_feeds.Add(feed);
			this.RegisterFeedListener(feed);
		}

		// Token: 0x0600C2B3 RID: 49843 RVA: 0x003AFC3C File Offset: 0x003ADE3C
		private void RegisterFeedListener(MessageContentFeed feed)
		{
			InGameMessageScheduler inGameMessageScheduler;
			if (HearthstoneServices.TryGet<InGameMessageScheduler>(out inGameMessageScheduler))
			{
				inGameMessageScheduler.RegisterMessage(feed.ContentId, 0, new UpdateMessageHandler(this.OnFeedUpdated));
				return;
			}
			Log.InGameMessage.PrintWarning("Failed to register feed listener, scheduler is not ready or missing.", Array.Empty<object>());
		}

		// Token: 0x0600C2B4 RID: 49844 RVA: 0x003AFC80 File Offset: 0x003ADE80
		private void OnFeedUpdated(GameMessage[] messages)
		{
			for (int i = 0; i < messages.Length; i++)
			{
				GameMessage message = messages[i];
				MessageContentFeed messageContentFeed = this.FindFeedForMessage(message);
				MessageUIData messageUIData = messageContentFeed.DataTranslator.CreateData(message);
				if (messageUIData == null)
				{
					Log.InGameMessage.PrintWarning("Could not translate data for message {0} in feed {1}. Message will be ignored", new object[]
					{
						message,
						messageContentFeed.ContentId
					});
					return;
				}
				UIMessageCallbacks callbacks = messageUIData.Callbacks;
				callbacks.OnClosed = (Action)Delegate.Combine(callbacks.OnClosed, new Action(delegate()
				{
					InGameMessageScheduler inGameMessageScheduler;
					if (HearthstoneServices.TryGet<InGameMessageScheduler>(out inGameMessageScheduler))
					{
						inGameMessageScheduler.IncreaseViewCount(message);
						inGameMessageScheduler.SendTelemetryMessageAction(message, InGameMessageAction.ActionType.CLOSE);
					}
				}));
				UIMessageCallbacks callbacks2 = messageUIData.Callbacks;
				callbacks2.OnStoreOpened = (Action)Delegate.Combine(callbacks2.OnStoreOpened, new Action(delegate()
				{
					InGameMessageScheduler inGameMessageScheduler;
					if (HearthstoneServices.TryGet<InGameMessageScheduler>(out inGameMessageScheduler))
					{
						inGameMessageScheduler.SendTelemetryMessageAction(message, InGameMessageAction.ActionType.OPENED_SHOP);
					}
				}));
				if (messageUIData.ContentType == MessageContentType.SHOP && !this.CanDisplayShopMessages())
				{
					this.DisplayMessageWhenShopIsReady(messageUIData);
					Log.InGameMessage.PrintDebug("Shop data was not ready, queuing message to be displayed when available.", Array.Empty<object>());
					return;
				}
				MessageFeedRegistry.QueueMessageForDisplay(messageUIData);
			}
		}

		// Token: 0x0600C2B5 RID: 49845 RVA: 0x003AFD84 File Offset: 0x003ADF84
		private static void QueueMessageForDisplay(MessageUIData messageData)
		{
			MessagePopupDisplay messagePopupDisplay;
			if (HearthstoneServices.TryGet<MessagePopupDisplay>(out messagePopupDisplay))
			{
				messagePopupDisplay.QueueMessage(messageData);
				return;
			}
			Log.InGameMessage.PrintWarning("Could not get MessagePopupDisplay service to display in game message", Array.Empty<object>());
		}

		// Token: 0x0600C2B6 RID: 49846 RVA: 0x003AFDB8 File Offset: 0x003ADFB8
		private MessageContentFeed FindFeedForMessage(GameMessage message)
		{
			return this.m_feeds.Find((MessageContentFeed x) => x.ContentId.Equals(message.ContentType));
		}

		// Token: 0x0600C2B7 RID: 49847 RVA: 0x003AFDEC File Offset: 0x003ADFEC
		private bool CanDisplayShopMessages()
		{
			StoreManager storeManager = StoreManager.Get();
			return storeManager.IsOpen(true) && storeManager.Catalog != null && storeManager.Catalog.HasData;
		}

		// Token: 0x0600C2B8 RID: 49848 RVA: 0x003AFE1D File Offset: 0x003AE01D
		private void DisplayMessageWhenShopIsReady(MessageUIData messageUIData)
		{
			this.m_shopMessageQueue.Enqueue(messageUIData);
			Processor.QueueJobIfNotExist("Job_IGMWaitForStore", this.Job_DisplayStoreMessagesWhenAble(), Array.Empty<IJobDependency>());
		}

		// Token: 0x0600C2B9 RID: 49849 RVA: 0x003AFE41 File Offset: 0x003AE041
		private IEnumerator<IAsyncJobResult> Job_DisplayStoreMessagesWhenAble()
		{
			while (!this.CanDisplayShopMessages())
			{
				yield return null;
			}
			while (this.m_shopMessageQueue.Count > 0)
			{
				MessageFeedRegistry.QueueMessageForDisplay(this.m_shopMessageQueue.Dequeue());
			}
			yield break;
		}

		// Token: 0x04009CB6 RID: 40118
		private static MessageFeedRegistry s_instance;

		// Token: 0x04009CB7 RID: 40119
		private List<MessageContentFeed> m_feeds = new List<MessageContentFeed>();

		// Token: 0x04009CB8 RID: 40120
		private Queue<MessageUIData> m_shopMessageQueue = new Queue<MessageUIData>();
	}
}
