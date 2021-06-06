using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;

namespace Hearthstone.InGameMessage.UI
{
	internal static class MessageDataModelFactory
	{
		public static List<IDataModel> CreateDataModel(MessageUIData data)
		{
			switch (data.ContentType)
			{
			case MessageContentType.TEXT:
				return CreateTextContentDataModel(data);
			case MessageContentType.DEBUG:
				return CreateDebugContentDataModel(data);
			case MessageContentType.SHOP:
				return CreateShopContentDataModel(data);
			default:
				Log.InGameMessage.PrintWarning($"Unsupported IGM Content type {data.ContentType}. Cannot create data model");
				return null;
			}
		}

		private static List<IDataModel> CreateDebugContentDataModel(MessageUIData data)
		{
			MessageDebugContentDataModel messageDebugContentDataModel = new MessageDebugContentDataModel();
			messageDebugContentDataModel.TestString = (data.MessageData as TestDebugMessageUIData)?.TestString;
			return new List<IDataModel> { messageDebugContentDataModel };
		}

		private static List<IDataModel> CreateTextContentDataModel(MessageUIData data)
		{
			TextMessageContentDataModel textMessageContentDataModel = new TextMessageContentDataModel();
			TextMessageContent textMessageContent = data.MessageData as TextMessageContent;
			textMessageContentDataModel.BodyText = textMessageContent.TextBody;
			textMessageContentDataModel.IconType = textMessageContent.ImageType;
			textMessageContentDataModel.Title = textMessageContent.Title;
			return new List<IDataModel> { textMessageContentDataModel };
		}

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
			list.Add(GetShopProductDataModel(shopMessageContent.ProductID));
			return list;
		}

		private static IDataModel GetShopProductDataModel(long productID)
		{
			ProductDataModel productByPmtId = StoreManager.Get().Catalog.GetProductByPmtId(productID);
			if (productByPmtId == null)
			{
				Log.InGameMessage.PrintError("Unexpected null product data model");
				return null;
			}
			RewardListDataModel rewardList = productByPmtId.RewardList;
			if (rewardList == null)
			{
				Log.InGameMessage.PrintError("Unexpected missing rewards list");
				return null;
			}
			if (IsMiniSetReward(rewardList))
			{
				int itemId = rewardList.Items[0].ItemId;
				MiniSetDbfRecord record = GameDbf.MiniSet.GetRecord(itemId);
				try
				{
					return GetMiniSetRewardListDataModel(record.DeckRecord);
				}
				catch (Exception ex)
				{
					Log.InGameMessage.PrintError("Error attempting to create miniset data models: {0}", ex.Message);
					return null;
				}
			}
			return productByPmtId;
		}

		private static bool IsMiniSetReward(RewardListDataModel rewards)
		{
			if (rewards.Items.Count == 1)
			{
				return rewards.Items[0].ItemType == RewardItemType.MINI_SET;
			}
			return false;
		}

		private static IDataModel GetMiniSetRewardListDataModel(DeckDbfRecord deck)
		{
			DefLoader loader = DefLoader.Get();
			return new RewardListDataModel
			{
				Items = (from c in deck.Cards
					select loader.GetEntityDef(c.CardId) into ed
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
				}).ToDataModelList()
			};
		}
	}
}
