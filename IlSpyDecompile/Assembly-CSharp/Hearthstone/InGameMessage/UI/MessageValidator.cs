using Hearthstone.DataModels;

namespace Hearthstone.InGameMessage.UI
{
	public class MessageValidator
	{
		private const bool REQUIRE_PRODUCTS_PURCHASABLE_DEFAULT = true;

		public static bool IsMessageValid(MessageUIData messageData)
		{
			if (messageData == null)
			{
				return false;
			}
			return messageData.ContentType switch
			{
				MessageContentType.TEXT => IsTextMessageValid(messageData.MessageData as TextMessageContent), 
				MessageContentType.SHOP => IsShopMessageValid(messageData.MessageData as ShopMessageContent), 
				_ => false, 
			};
		}

		private static bool IsTextMessageValid(TextMessageContent content)
		{
			if (IsRequiredStringValid(content.ImageType, "Image Type") && IsRequiredStringValid(content.TextBody, "Text Body"))
			{
				return IsRequiredStringValid(content.TextBody, "Title");
			}
			return false;
		}

		private static bool IsShopMessageValid(ShopMessageContent content)
		{
			if (IsRequiredStringValid(content.TextBody, "Text Body") && IsRequiredStringValid(content.Title, "Title"))
			{
				return IsShopProductValid(content.ProductID);
			}
			return false;
		}

		private static bool IsRequiredStringValid(string stringField, string fieldName)
		{
			if (string.IsNullOrEmpty(stringField))
			{
				Log.InGameMessage.PrintInfo("Message was missing " + fieldName + " and is invalid");
				return false;
			}
			return true;
		}

		private static bool IsShopProductValid(long pmtId)
		{
			StoreManager storeManager = StoreManager.Get();
			if (storeManager == null || !storeManager.IsOpen() || storeManager.Catalog == null || !storeManager.Catalog.HasData)
			{
				Log.InGameMessage.PrintInfo("Shop product is considered invalid as shop is not ready");
				return false;
			}
			ProductDataModel productByPmtId = storeManager.Catalog.GetProductByPmtId(pmtId);
			if (productByPmtId == null)
			{
				Log.InGameMessage.PrintInfo($"Shop product {pmtId} is invalid as shop data was missing");
				return false;
			}
			if (AllowOnlyPurchaseableProduct() && productByPmtId.Availability != ProductAvailability.CAN_PURCHASE)
			{
				Log.InGameMessage.PrintInfo($"Shop product {pmtId} cannot be purchased. Considering message invalid");
				return false;
			}
			if (productByPmtId.RewardList == null || productByPmtId.RewardList.Items == null || productByPmtId.RewardList.Items.Count < 1)
			{
				Log.InGameBrowser.PrintInfo($"Product {pmtId} has no reward items to display. Considering message invalid");
				return false;
			}
			return true;
		}

		private static bool AllowOnlyPurchaseableProduct()
		{
			return Vars.Key("IGM.RequirePurchasable").GetBool(def: true);
		}
	}
}
