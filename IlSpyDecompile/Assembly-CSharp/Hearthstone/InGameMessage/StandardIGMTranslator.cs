using System;
using Hearthstone.InGameMessage.UI;

namespace Hearthstone.InGameMessage
{
	public class StandardIGMTranslator : IDataTranslator
	{
		private const string TEXT_LAYOUT_NAME = "Simple Text";

		private const string SHOP_LAYOUT_NAME = "Shop";

		public MessageUIData CreateData(GameMessage message)
		{
			if (string.IsNullOrEmpty(message.LayoutType))
			{
				Log.InGameMessage.PrintInfo("Could not translate IGM data, missing layout type");
				return null;
			}
			Log.InGameMessage.PrintDebug("Translating message type {0}", message.LayoutType);
			if (message.LayoutType.Equals("Simple Text", StringComparison.OrdinalIgnoreCase))
			{
				return TranslateTextMessage(message);
			}
			if (message.LayoutType.Equals("Shop", StringComparison.OrdinalIgnoreCase))
			{
				return TranslateShopMessage(message);
			}
			Log.InGameMessage.PrintInfo("Could not find data translator for IGM layout {0}", message.LayoutType);
			return null;
		}

		private MessageUIData TranslateTextMessage(GameMessage message)
		{
			if (string.IsNullOrEmpty(message.Title))
			{
				Log.InGameMessage.PrintInfo("IGM Text message was missing a title and is not valid");
				return null;
			}
			if (string.IsNullOrEmpty(message.DisplayImageType))
			{
				Log.InGameMessage.PrintInfo("IGM Text message was missing a display image type and is not valid");
				return null;
			}
			if (string.IsNullOrEmpty(message.TextBody))
			{
				Log.InGameMessage.PrintInfo("IGM Text message was missing the body text and is not valid");
				return null;
			}
			return new MessageUIData
			{
				UID = message.UID,
				ContentType = MessageContentType.TEXT,
				MessageData = new TextMessageContent
				{
					Title = message.Title,
					ImageType = message.DisplayImageType,
					TextBody = message.TextBody
				}
			};
		}

		private MessageUIData TranslateShopMessage(GameMessage message)
		{
			if (string.IsNullOrEmpty(message.Title))
			{
				Log.InGameMessage.PrintInfo("IGM Shop message was missing a title and is not valid");
				return null;
			}
			if (string.IsNullOrEmpty(message.TextBody))
			{
				Log.InGameMessage.PrintInfo("IGM Shop message was missing the body text and is not valid");
				return null;
			}
			if (message.ProductID == 0L)
			{
				Log.InGameMessage.PrintInfo("IGM Shop message was missing a product id and is not valid");
				return null;
			}
			return new MessageUIData
			{
				UID = message.UID,
				ContentType = MessageContentType.SHOP,
				MessageData = new ShopMessageContent
				{
					Title = message.Title,
					TextBody = message.TextBody,
					ProductID = message.ProductID,
					OpenFullShop = message.OpenFullShop
				}
			};
		}
	}
}
