using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;

namespace Hearthstone.InGameMessage.UI
{
	// Token: 0x02001161 RID: 4449
	internal static class MessageDataModelFactory
	{
		// Token: 0x0600C2D3 RID: 49875 RVA: 0x003B0128 File Offset: 0x003AE328
		public static List<IDataModel> CreateDataModel(MessageUIData data)
		{
			MessageContentType contentType = data.ContentType;
			if (contentType == MessageContentType.TEXT)
			{
				return MessageDataModelFactory.CreateTextContentDataModel(data);
			}
			if (contentType == MessageContentType.SHOP)
			{
				return MessageDataModelFactory.CreateShopContentDataModel(data);
			}
			if (contentType != MessageContentType.DEBUG)
			{
				Log.InGameMessage.PrintWarning(string.Format("Unsupported IGM Content type {0}. Cannot create data model", data.ContentType), Array.Empty<object>());
				return null;
			}
			return MessageDataModelFactory.CreateDebugContentDataModel(data);
		}

		// Token: 0x0600C2D4 RID: 49876 RVA: 0x003B0188 File Offset: 0x003AE388
		private static List<IDataModel> CreateDebugContentDataModel(MessageUIData data)
		{
			MessageDebugContentDataModel messageDebugContentDataModel = new MessageDebugContentDataModel();
			TestDebugMessageUIData testDebugMessageUIData = data.MessageData as TestDebugMessageUIData;
			messageDebugContentDataModel.TestString = ((testDebugMessageUIData != null) ? testDebugMessageUIData.TestString : null);
			return new List<IDataModel>
			{
				messageDebugContentDataModel
			};
		}

		// Token: 0x0600C2D5 RID: 49877 RVA: 0x003B01C8 File Offset: 0x003AE3C8
		private static List<IDataModel> CreateTextContentDataModel(MessageUIData data)
		{
			TextMessageContentDataModel textMessageContentDataModel = new TextMessageContentDataModel();
			TextMessageContent textMessageContent = data.MessageData as TextMessageContent;
			textMessageContentDataModel.BodyText = textMessageContent.TextBody;
			textMessageContentDataModel.IconType = textMessageContent.ImageType;
			textMessageContentDataModel.Title = textMessageContent.Title;
			return new List<IDataModel>
			{
				textMessageContentDataModel
			};
		}

		// Token: 0x0600C2D6 RID: 49878 RVA: 0x003B0218 File Offset: 0x003AE418
		private static List<IDataModel> CreateShopContentDataModel(MessageUIData data)
		{
			List<IDataModel> list = new List<IDataModel>();
			ShopMessageContent shopMessageContent = data.MessageData as ShopMessageContent;
			ShopMessageContentDataModel item = new ShopMessageContentDataModel
			{
				Title = shopMessageContent.Title,
				BodyText = shopMessageContent.TextBody
			};
			list.Add(item);
			list.Add(MessageDataModelFactory.GetShopProductDataModel(shopMessageContent.ProductID));
			return list;
		}

		// Token: 0x0600C2D7 RID: 49879 RVA: 0x003B026C File Offset: 0x003AE46C
		private static IDataModel GetShopProductDataModel(long productID)
		{
			ProductDataModel productByPmtId = StoreManager.Get().Catalog.GetProductByPmtId(productID);
			if (productByPmtId == null)
			{
				Log.InGameMessage.PrintError("Unexpected null product data model", Array.Empty<object>());
				return null;
			}
			RewardListDataModel rewardList = productByPmtId.RewardList;
			if (rewardList == null)
			{
				Log.InGameMessage.PrintError("Unexpected missing rewards list", Array.Empty<object>());
				return null;
			}
			if (MessageDataModelFactory.IsMiniSetReward(rewardList))
			{
				int itemId = rewardList.Items[0].ItemId;
				MiniSetDbfRecord record = GameDbf.MiniSet.GetRecord(itemId);
				try
				{
					return MessageDataModelFactory.GetMiniSetRewardListDataModel(record.DeckRecord);
				}
				catch (Exception ex)
				{
					Log.InGameMessage.PrintError("Error attempting to create miniset data models: {0}", new object[]
					{
						ex.Message
					});
					return null;
				}
				return productByPmtId;
			}
			return productByPmtId;
		}

		// Token: 0x0600C2D8 RID: 49880 RVA: 0x003B0334 File Offset: 0x003AE534
		private static bool IsMiniSetReward(RewardListDataModel rewards)
		{
			return rewards.Items.Count == 1 && rewards.Items[0].ItemType == RewardItemType.MINI_SET;
		}

		// Token: 0x0600C2D9 RID: 49881 RVA: 0x003B035C File Offset: 0x003AE55C
		private static IDataModel GetMiniSetRewardListDataModel(DeckDbfRecord deck)
		{
			DefLoader loader = DefLoader.Get();
			RewardListDataModel rewardListDataModel = new RewardListDataModel();
			rewardListDataModel.Items = (from c in deck.Cards
			select loader.GetEntityDef(c.CardId, true) into ed
			where ed.GetRarity() == TAG_RARITY.LEGENDARY
			orderby ed.GetCost()
			select new RewardItemDataModel
			{
				ItemType = RewardItemType.CARD,
				Card = new CardDataModel
				{
					CardId = ed.GetCardId()
				}
			}).Append(new RewardItemDataModel
			{
				ItemType = RewardItemType.MINI_SET
			}).ToDataModelList<RewardItemDataModel>();
			return rewardListDataModel;
		}
	}
}
