using System;
using Hearthstone.DataModels;

namespace Hearthstone.InGameMessage.UI
{
	// Token: 0x02001166 RID: 4454
	public class MessageValidator
	{
		// Token: 0x0600C309 RID: 49929 RVA: 0x003B0AF0 File Offset: 0x003AECF0
		public static bool IsMessageValid(MessageUIData messageData)
		{
			if (messageData == null)
			{
				return false;
			}
			MessageContentType contentType = messageData.ContentType;
			if (contentType != MessageContentType.TEXT)
			{
				return contentType == MessageContentType.SHOP && MessageValidator.IsShopMessageValid(messageData.MessageData as ShopMessageContent);
			}
			return MessageValidator.IsTextMessageValid(messageData.MessageData as TextMessageContent);
		}

		// Token: 0x0600C30A RID: 49930 RVA: 0x003B0B36 File Offset: 0x003AED36
		private static bool IsTextMessageValid(TextMessageContent content)
		{
			return MessageValidator.IsRequiredStringValid(content.ImageType, "Image Type") && MessageValidator.IsRequiredStringValid(content.TextBody, "Text Body") && MessageValidator.IsRequiredStringValid(content.TextBody, "Title");
		}

		// Token: 0x0600C30B RID: 49931 RVA: 0x003B0B6E File Offset: 0x003AED6E
		private static bool IsShopMessageValid(ShopMessageContent content)
		{
			return MessageValidator.IsRequiredStringValid(content.TextBody, "Text Body") && MessageValidator.IsRequiredStringValid(content.Title, "Title") && MessageValidator.IsShopProductValid(content.ProductID);
		}

		// Token: 0x0600C30C RID: 49932 RVA: 0x003B0BA1 File Offset: 0x003AEDA1
		private static bool IsRequiredStringValid(string stringField, string fieldName)
		{
			if (string.IsNullOrEmpty(stringField))
			{
				Log.InGameMessage.PrintInfo("Message was missing " + fieldName + " and is invalid", Array.Empty<object>());
				return false;
			}
			return true;
		}

		// Token: 0x0600C30D RID: 49933 RVA: 0x003B0BD0 File Offset: 0x003AEDD0
		private static bool IsShopProductValid(long pmtId)
		{
			StoreManager storeManager = StoreManager.Get();
			if (storeManager == null || !storeManager.IsOpen(true) || storeManager.Catalog == null || !storeManager.Catalog.HasData)
			{
				Log.InGameMessage.PrintInfo("Shop product is considered invalid as shop is not ready", Array.Empty<object>());
				return false;
			}
			ProductDataModel productByPmtId = storeManager.Catalog.GetProductByPmtId(pmtId);
			if (productByPmtId == null)
			{
				Log.InGameMessage.PrintInfo(string.Format("Shop product {0} is invalid as shop data was missing", pmtId), Array.Empty<object>());
				return false;
			}
			if (MessageValidator.AllowOnlyPurchaseableProduct() && productByPmtId.Availability != ProductAvailability.CAN_PURCHASE)
			{
				Log.InGameMessage.PrintInfo(string.Format("Shop product {0} cannot be purchased. Considering message invalid", pmtId), Array.Empty<object>());
				return false;
			}
			if (productByPmtId.RewardList == null || productByPmtId.RewardList.Items == null || productByPmtId.RewardList.Items.Count < 1)
			{
				Log.InGameBrowser.PrintInfo(string.Format("Product {0} has no reward items to display. Considering message invalid", pmtId), Array.Empty<object>());
				return false;
			}
			return true;
		}

		// Token: 0x0600C30E RID: 49934 RVA: 0x003B0CC6 File Offset: 0x003AEEC6
		private static bool AllowOnlyPurchaseableProduct()
		{
			return Vars.Key("IGM.RequirePurchasable").GetBool(true);
		}

		// Token: 0x04009CDF RID: 40159
		private const bool REQUIRE_PRODUCTS_PURCHASABLE_DEFAULT = true;
	}
}
