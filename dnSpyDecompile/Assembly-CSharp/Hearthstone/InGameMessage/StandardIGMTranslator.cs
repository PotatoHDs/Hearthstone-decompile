using System;
using Hearthstone.InGameMessage.UI;

namespace Hearthstone.InGameMessage
{
	// Token: 0x0200115C RID: 4444
	public class StandardIGMTranslator : IDataTranslator
	{
		// Token: 0x0600C2C3 RID: 49859 RVA: 0x003AFE94 File Offset: 0x003AE094
		public MessageUIData CreateData(GameMessage message)
		{
			if (string.IsNullOrEmpty(message.LayoutType))
			{
				Log.InGameMessage.PrintInfo("Could not translate IGM data, missing layout type", Array.Empty<object>());
				return null;
			}
			Log.InGameMessage.PrintDebug("Translating message type {0}", new object[]
			{
				message.LayoutType
			});
			if (message.LayoutType.Equals("Simple Text", StringComparison.OrdinalIgnoreCase))
			{
				return this.TranslateTextMessage(message);
			}
			if (message.LayoutType.Equals("Shop", StringComparison.OrdinalIgnoreCase))
			{
				return this.TranslateShopMessage(message);
			}
			Log.InGameMessage.PrintInfo("Could not find data translator for IGM layout {0}", new object[]
			{
				message.LayoutType
			});
			return null;
		}

		// Token: 0x0600C2C4 RID: 49860 RVA: 0x003AFF38 File Offset: 0x003AE138
		private MessageUIData TranslateTextMessage(GameMessage message)
		{
			if (string.IsNullOrEmpty(message.Title))
			{
				Log.InGameMessage.PrintInfo("IGM Text message was missing a title and is not valid", Array.Empty<object>());
				return null;
			}
			if (string.IsNullOrEmpty(message.DisplayImageType))
			{
				Log.InGameMessage.PrintInfo("IGM Text message was missing a display image type and is not valid", Array.Empty<object>());
				return null;
			}
			if (string.IsNullOrEmpty(message.TextBody))
			{
				Log.InGameMessage.PrintInfo("IGM Text message was missing the body text and is not valid", Array.Empty<object>());
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

		// Token: 0x0600C2C5 RID: 49861 RVA: 0x003AFFF8 File Offset: 0x003AE1F8
		private MessageUIData TranslateShopMessage(GameMessage message)
		{
			if (string.IsNullOrEmpty(message.Title))
			{
				Log.InGameMessage.PrintInfo("IGM Shop message was missing a title and is not valid", Array.Empty<object>());
				return null;
			}
			if (string.IsNullOrEmpty(message.TextBody))
			{
				Log.InGameMessage.PrintInfo("IGM Shop message was missing the body text and is not valid", Array.Empty<object>());
				return null;
			}
			if (message.ProductID == 0L)
			{
				Log.InGameMessage.PrintInfo("IGM Shop message was missing a product id and is not valid", Array.Empty<object>());
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

		// Token: 0x04009CBD RID: 40125
		private const string TEXT_LAYOUT_NAME = "Simple Text";

		// Token: 0x04009CBE RID: 40126
		private const string SHOP_LAYOUT_NAME = "Shop";
	}
}
