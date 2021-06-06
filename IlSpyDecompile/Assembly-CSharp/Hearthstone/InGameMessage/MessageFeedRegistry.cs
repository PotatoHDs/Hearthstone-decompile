using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.Core;
using Hearthstone.InGameMessage.UI;

namespace Hearthstone.InGameMessage
{
	public class MessageFeedRegistry
	{
		private static MessageFeedRegistry s_instance;

		private List<MessageContentFeed> m_feeds = new List<MessageContentFeed>();

		private Queue<MessageUIData> m_shopMessageQueue = new Queue<MessageUIData>();

		public static MessageFeedRegistry Get()
		{
			return s_instance;
		}

		public MessageFeedRegistry()
		{
			s_instance = this;
		}

		public void RegisterDefaultFeeds()
		{
			MessageContentFeed feed = new MessageContentFeed
			{
				ContentId = "in_game_message_box_hub",
				DataTranslator = new StandardIGMTranslator()
			};
			RegisterFeed(feed);
		}

		public void RegisterFeed(MessageContentFeed feed)
		{
			if (feed == null || string.IsNullOrEmpty(feed.ContentId))
			{
				Log.InGameMessage.PrintWarning("Attempted to register a feed with invalid/missing arguments");
				return;
			}
			if (m_feeds.Exists((MessageContentFeed x) => x.ContentId.Equals(feed.ContentId)))
			{
				Log.InGameMessage.PrintWarning("Attempted to re-register an existing feed");
				return;
			}
			m_feeds.Add(feed);
			RegisterFeedListener(feed);
		}

		private void RegisterFeedListener(MessageContentFeed feed)
		{
			if (HearthstoneServices.TryGet<InGameMessageScheduler>(out var service))
			{
				service.RegisterMessage(feed.ContentId, 0, OnFeedUpdated);
			}
			else
			{
				Log.InGameMessage.PrintWarning("Failed to register feed listener, scheduler is not ready or missing.");
			}
		}

		private void OnFeedUpdated(GameMessage[] messages)
		{
			foreach (GameMessage message in messages)
			{
				MessageContentFeed messageContentFeed = FindFeedForMessage(message);
				MessageUIData messageUIData = messageContentFeed.DataTranslator.CreateData(message);
				if (messageUIData == null)
				{
					Log.InGameMessage.PrintWarning("Could not translate data for message {0} in feed {1}. Message will be ignored", message, messageContentFeed.ContentId);
					break;
				}
				UIMessageCallbacks callbacks = messageUIData.Callbacks;
				callbacks.OnClosed = (Action)Delegate.Combine(callbacks.OnClosed, (Action)delegate
				{
					if (HearthstoneServices.TryGet<InGameMessageScheduler>(out var service2))
					{
						service2.IncreaseViewCount(message);
						service2.SendTelemetryMessageAction(message, InGameMessageAction.ActionType.CLOSE);
					}
				});
				UIMessageCallbacks callbacks2 = messageUIData.Callbacks;
				callbacks2.OnStoreOpened = (Action)Delegate.Combine(callbacks2.OnStoreOpened, (Action)delegate
				{
					if (HearthstoneServices.TryGet<InGameMessageScheduler>(out var service))
					{
						service.SendTelemetryMessageAction(message, InGameMessageAction.ActionType.OPENED_SHOP);
					}
				});
				if (messageUIData.ContentType == MessageContentType.SHOP && !CanDisplayShopMessages())
				{
					DisplayMessageWhenShopIsReady(messageUIData);
					Log.InGameMessage.PrintDebug("Shop data was not ready, queuing message to be displayed when available.");
					break;
				}
				QueueMessageForDisplay(messageUIData);
			}
		}

		private static void QueueMessageForDisplay(MessageUIData messageData)
		{
			if (HearthstoneServices.TryGet<MessagePopupDisplay>(out var service))
			{
				service.QueueMessage(messageData);
			}
			else
			{
				Log.InGameMessage.PrintWarning("Could not get MessagePopupDisplay service to display in game message");
			}
		}

		private MessageContentFeed FindFeedForMessage(GameMessage message)
		{
			return m_feeds.Find((MessageContentFeed x) => x.ContentId.Equals(message.ContentType));
		}

		private bool CanDisplayShopMessages()
		{
			StoreManager storeManager = StoreManager.Get();
			if (storeManager.IsOpen() && storeManager.Catalog != null)
			{
				return storeManager.Catalog.HasData;
			}
			return false;
		}

		private void DisplayMessageWhenShopIsReady(MessageUIData messageUIData)
		{
			m_shopMessageQueue.Enqueue(messageUIData);
			Processor.QueueJobIfNotExist("Job_IGMWaitForStore", Job_DisplayStoreMessagesWhenAble());
		}

		private IEnumerator<IAsyncJobResult> Job_DisplayStoreMessagesWhenAble()
		{
			while (!CanDisplayShopMessages())
			{
				yield return null;
			}
			while (m_shopMessageQueue.Count > 0)
			{
				QueueMessageForDisplay(m_shopMessageQueue.Dequeue());
			}
		}
	}
}
