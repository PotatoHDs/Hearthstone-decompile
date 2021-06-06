using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Hearthstone.DataModels;
using PegasusShared;
using PegasusUtil;

public class RewardFactory
{
	public static List<RewardItemDataModel> CreateRewardItemDataModel(RewardItemDbfRecord itemRecord, int? rewardItemOutputData = null)
	{
		List<RewardItemDataModel> list = new List<RewardItemDataModel>();
		switch (itemRecord.RewardType)
		{
		case RewardItem.RewardType.GOLD:
		case RewardItem.RewardType.TAVERN_TICKET:
		case RewardItem.RewardType.REWARD_TRACK_XP_BOOST:
			list.Add(CreateSimpleRewardItemDataModel(itemRecord));
			break;
		case RewardItem.RewardType.DUST:
		case RewardItem.RewardType.ARCANE_ORBS:
			list.Add(CreateCurrencyRewardItemDataModel(itemRecord));
			break;
		case RewardItem.RewardType.BOOSTER:
			list.Add(CreateBoosterRewardItemDataModel(itemRecord));
			break;
		case RewardItem.RewardType.CARD:
			list.Add(CreateCardRewardItemDataModel(itemRecord));
			break;
		case RewardItem.RewardType.RANDOM_CARD:
			list.Add(CreateRandomCardRewardItemDataModel(itemRecord, rewardItemOutputData));
			break;
		case RewardItem.RewardType.CARD_BACK:
			list.Add(CreateCardBackRewardItemDataModel(itemRecord));
			break;
		case RewardItem.RewardType.HERO_SKIN:
			list.Add(CreateHeroSkinRewardItemDataModel(itemRecord));
			break;
		case RewardItem.RewardType.CUSTOM_COIN:
			list.Add(CreateCustomCoinRewardItemDataModel(itemRecord));
			break;
		case RewardItem.RewardType.CARD_SUBSET:
			list.AddRange(CreateCardSubsetRewardItemDataModel(itemRecord));
			break;
		default:
			Log.All.PrintWarning($"RewardItem has unsupported item type [{itemRecord.RewardType}]");
			return null;
		}
		return list;
	}

	private static RewardItemDataModel CreateBoosterRewardItemDataModel(RewardItemDbfRecord record)
	{
		int num = record.Booster;
		if (num == 0)
		{
			num = (int)GameUtils.GetRewardableBoosterFromSelector(record.BoosterSelector);
		}
		return new RewardItemDataModel
		{
			AssetId = record.ID,
			ItemType = record.RewardType.ToRewardItemType(),
			Quantity = record.Quantity,
			ItemId = num,
			Booster = new PackDataModel
			{
				Type = (BoosterDbId)num,
				Quantity = record.Quantity
			}
		};
	}

	private static RewardItemDataModel CreateCardRewardItemDataModel(RewardItemDbfRecord record)
	{
		CardDbfRecord record2 = GameDbf.Card.GetRecord(record.Card);
		if (record2 == null)
		{
			Log.All.PrintWarning($"Card Item has unknown card id [{record.Card}]");
			return null;
		}
		return new RewardItemDataModel
		{
			AssetId = record.ID,
			ItemType = record.RewardType.ToRewardItemType(),
			Quantity = record.Quantity,
			ItemId = record.Card,
			Card = new CardDataModel
			{
				CardId = record2.NoteMiniGuid,
				Premium = record.CardPremiumLevel.GetPremium()
			}
		};
	}

	private static List<RewardItemDataModel> CreateCardSubsetRewardItemDataModel(RewardItemDbfRecord record)
	{
		List<RewardItemDataModel> list = new List<RewardItemDataModel>();
		foreach (string item in GameDbf.GetIndex().GetSubsetById(record.SubsetId))
		{
			CardDbfRecord record2 = GameDbf.Card.GetRecord(GameUtils.TranslateCardIdToDbId(item));
			list.Add(new RewardItemDataModel
			{
				AssetId = record.ID,
				ItemType = RewardItemType.CARD,
				Quantity = record.Quantity,
				ItemId = record2.ID,
				Card = new CardDataModel
				{
					CardId = record2.NoteMiniGuid,
					Premium = record.CardPremiumLevel.GetPremium()
				}
			});
		}
		return list;
	}

	private static RewardItemDataModel CreateCardBackRewardItemDataModel(RewardItemDbfRecord record)
	{
		if (!GameDbf.CardBack.HasRecord(record.CardBack))
		{
			Log.All.PrintWarning($"Card Back Item has unrecognized card back id [{record.CardBack}]");
			return null;
		}
		return new RewardItemDataModel
		{
			AssetId = record.ID,
			ItemType = record.RewardType.ToRewardItemType(),
			Quantity = record.Quantity,
			ItemId = record.CardBack,
			CardBack = new CardBackDataModel
			{
				CardBackId = record.CardBack
			}
		};
	}

	private static RewardItemDataModel CreateCurrencyRewardItemDataModel(RewardItemDbfRecord record)
	{
		RewardItemType itemType = record.RewardType.ToRewardItemType();
		CurrencyType currency = RewardUtils.RewardItemTypeToCurrencyType(itemType);
		if (ShopUtils.IsCurrencyVirtual(currency) && !ShopUtils.IsVirtualCurrencyEnabled())
		{
			return null;
		}
		return new RewardItemDataModel
		{
			AssetId = record.ID,
			ItemType = itemType,
			Quantity = record.Quantity,
			Currency = new PriceDataModel
			{
				Currency = currency,
				Amount = record.Quantity,
				DisplayText = record.Quantity.ToString()
			}
		};
	}

	private static RewardItemDataModel CreateCustomCoinRewardItemDataModel(RewardItemDbfRecord record)
	{
		CoinDbfRecord record2 = GameDbf.Coin.GetRecord(record.CustomCoin);
		if (record2 == null)
		{
			Log.All.PrintWarning($"Custom Coin Item has unknown id [{record.CustomCoin}]");
			return null;
		}
		CardDbfRecord record3 = GameDbf.Card.GetRecord(record2.CardId);
		if (record3 == null)
		{
			Log.All.PrintWarning($"Custom Coin Item has unknown card id [{record2.CardId}]");
			return null;
		}
		return new RewardItemDataModel
		{
			AssetId = record.ID,
			ItemType = record.RewardType.ToRewardItemType(),
			Quantity = record.Quantity,
			ItemId = record.CustomCoin,
			Card = new CardDataModel
			{
				CardId = record3.NoteMiniGuid,
				Premium = record.CardPremiumLevel.GetPremium()
			}
		};
	}

	private static RewardItemDataModel CreateHeroSkinRewardItemDataModel(RewardItemDbfRecord record)
	{
		CardDbfRecord record2 = GameDbf.Card.GetRecord(record.Card);
		if (record2?.CardHero == null)
		{
			Log.All.PrintWarning($"Hero Skin Item has invalid card id [{record.Card}] where card dbf record has" + " no CARD_HERO subtable. NoteMiniGuid = " + record2?.NoteMiniGuid);
			return null;
		}
		return new RewardItemDataModel
		{
			AssetId = record.ID,
			ItemType = record.RewardType.ToRewardItemType(),
			Quantity = record.Quantity,
			ItemId = record.Card,
			Card = new CardDataModel
			{
				CardId = record2.NoteMiniGuid,
				Name = record2.Name,
				FlavorText = record2.FlavorText,
				Premium = record.CardPremiumLevel.GetPremium(),
				Owned = CollectionManager.Get().IsCardInCollection(record2.NoteMiniGuid, record.CardPremiumLevel.GetPremium())
			}
		};
	}

	private static RewardItemDataModel CreateRandomCardRewardItemDataModel(RewardItemDbfRecord record, int? rewardItemOutputData = null)
	{
		RewardItemDataModel rewardItemDataModel = new RewardItemDataModel
		{
			AssetId = record.ID,
			Quantity = record.Quantity
		};
		if (rewardItemOutputData.HasValue)
		{
			int value = rewardItemOutputData.Value;
			CardDbfRecord record2 = GameDbf.Card.GetRecord(value);
			if (record2 == null)
			{
				Log.All.PrintWarning($"Random Card Item has unknown output card id [{value}]");
				return null;
			}
			rewardItemDataModel.ItemType = RewardItemType.CARD;
			rewardItemDataModel.ItemId = value;
			rewardItemDataModel.Card = new CardDataModel
			{
				CardId = record2.NoteMiniGuid,
				Premium = record.CardPremiumLevel.GetPremium()
			};
		}
		else
		{
			TAG_RARITY rarityForRandomCardReward = RewardUtils.GetRarityForRandomCardReward(record.RandomCardBoosterCardSet);
			if (rarityForRandomCardReward == TAG_RARITY.INVALID)
			{
				return null;
			}
			rewardItemDataModel.ItemType = record.RewardType.ToRewardItemType();
			rewardItemDataModel.RandomCard = new RandomCardDataModel
			{
				Premium = record.CardPremiumLevel.GetPremium(),
				Rarity = rarityForRandomCardReward,
				Count = record.Quantity
			};
		}
		return rewardItemDataModel;
	}

	private static RewardItemDataModel CreateSimpleRewardItemDataModel(RewardItemDbfRecord record)
	{
		return new RewardItemDataModel
		{
			AssetId = record.ID,
			Quantity = record.Quantity,
			ItemType = record.RewardType.ToRewardItemType()
		};
	}

	public static RewardItemDataModel CreateShopProductRewardItemDataModel(ShopProductData.ProductItemData productItemData)
	{
		return new RewardItemDataModel
		{
			PmtLicenseId = productItemData.licenseId,
			ItemType = productItemData.itemType,
			ItemId = productItemData.itemId,
			Quantity = productItemData.quantity
		};
	}

	public static RewardItemDataModel CreateShopRewardItemDataModel(Network.Bundle netBundle, Network.BundleItem netBundleItem, out bool isValidItem)
	{
		string arg = ((netBundle.DisplayName != null) ? netBundle.DisplayName.GetString() : "");
		RewardItemDataModel rewardItemDataModel = null;
		switch (netBundleItem.ItemType)
		{
		case ProductType.PRODUCT_TYPE_BOOSTER:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.BOOSTER,
				ItemId = netBundleItem.ProductData,
				Quantity = netBundleItem.Quantity
			};
			break;
		case ProductType.PRODUCT_TYPE_RANDOM_CARD:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.RANDOM_CARD,
				ItemId = netBundleItem.ProductData,
				Quantity = netBundleItem.Quantity
			};
			break;
		case ProductType.PRODUCT_TYPE_CURRENCY:
		{
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.UNDEFINED,
				Quantity = netBundleItem.Quantity
			};
			PegasusShared.CurrencyType productData = (PegasusShared.CurrencyType)netBundleItem.ProductData;
			switch (productData)
			{
			case PegasusShared.CurrencyType.CURRENCY_TYPE_DUST:
				rewardItemDataModel.ItemType = RewardItemType.DUST;
				break;
			case PegasusShared.CurrencyType.CURRENCY_TYPE_RUNESTONES:
				rewardItemDataModel.ItemType = RewardItemType.RUNESTONES;
				break;
			case PegasusShared.CurrencyType.CURRENCY_TYPE_ARCANE_ORBS:
				rewardItemDataModel.ItemType = RewardItemType.ARCANE_ORBS;
				break;
			default:
				Log.Store.PrintWarning($"Product Error [PMT ID = {netBundle.PMTProductID}, Name = {arg}]: Has unsupported currency type {productData}");
				isValidItem = false;
				return null;
			}
			break;
		}
		case ProductType.PRODUCT_TYPE_DRAFT:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.ARENA_TICKET,
				Quantity = netBundleItem.Quantity
			};
			break;
		case ProductType.PRODUCT_TYPE_CARD_BACK:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.CARD_BACK,
				ItemId = netBundleItem.ProductData,
				Quantity = 1
			};
			break;
		case ProductType.PRODUCT_TYPE_HERO:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.HERO_SKIN,
				ItemId = netBundleItem.ProductData,
				Quantity = 1
			};
			break;
		case ProductType.PRODUCT_TYPE_BATTLEGROUNDS_BONUS:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.BATTLEGROUNDS_BONUS,
				ItemId = netBundleItem.ProductData,
				Quantity = 1
			};
			break;
		case ProductType.PRODUCT_TYPE_PROGRESSION_BONUS:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.PROGRESSION_BONUS,
				ItemId = netBundleItem.ProductData,
				Quantity = 1
			};
			break;
		case ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.TAVERN_BRAWL_TICKET,
				ItemId = netBundleItem.ProductData,
				Quantity = 1
			};
			break;
		case ProductType.PRODUCT_TYPE_NAXX:
		case ProductType.PRODUCT_TYPE_BRM:
		case ProductType.PRODUCT_TYPE_LOE:
		case ProductType.PRODUCT_TYPE_WING:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.ADVENTURE_WING,
				ItemId = netBundleItem.ProductData,
				Quantity = 1
			};
			break;
		case ProductType.PRODUCT_TYPE_HIDDEN_LICENSE:
		case ProductType.PRODUCT_TYPE_FIXED_LICENSE:
			isValidItem = true;
			return null;
		case ProductType.PRODUCT_TYPE_MINI_SET:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.MINI_SET,
				ItemId = netBundleItem.ProductData,
				Quantity = 1
			};
			break;
		case ProductType.PRODUCT_TYPE_SELLABLE_DECK:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.SELLABLE_DECK,
				ItemId = netBundleItem.ProductData,
				Quantity = 1
			};
			break;
		default:
			Log.Store.PrintWarning($"Product {netBundle.PMTProductID} [{arg}] contains unrecognized item type [{netBundleItem.ItemType}]");
			isValidItem = false;
			return null;
		}
		string warningPrefix = $"Product Error [PMT ID = {netBundle.PMTProductID}, Name = {arg}]: ";
		isValidItem = RewardUtils.InitializeRewardItemDataModelForShop(rewardItemDataModel, netBundleItem, warningPrefix);
		return rewardItemDataModel;
	}

	public static IEnumerable<RewardItemDataModel> ConsolidateGroup(IGrouping<RewardItemType, RewardItemDataModel> group)
	{
		switch (group.Key)
		{
		case RewardItemType.ARENA_TICKET:
		case RewardItemType.GOLD:
			return ConsolidateSimpleRewardItems(group);
		case RewardItemType.REWARD_TRACK_XP_BOOST:
			return ConsolidateRewardTrackXpBoostItems(group);
		case RewardItemType.DUST:
		case RewardItemType.ARCANE_ORBS:
			return ConsolidateCurrencyRewardItems(group);
		case RewardItemType.BOOSTER:
			return ConsolidateBoosterRewardItems(group);
		case RewardItemType.HERO_SKIN:
		case RewardItemType.CARD:
		case RewardItemType.CUSTOM_COIN:
			return ConsolidateCardRewardItems(group);
		case RewardItemType.RANDOM_CARD:
			return ConsolidateRandomCardRewardItems(group);
		case RewardItemType.CARD_BACK:
			return ConsolidateCardBackRewardItems(group);
		default:
			Log.All.PrintWarning("RewardItem has unsupported item type [{0}]", group.Key);
			return Enumerable.Empty<RewardItemDataModel>();
		}
	}

	private static IEnumerable<RewardItemDataModel> ConsolidateBoosterRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return from element in rewards
			group element by element.ItemId into @group
			select @group.Aggregate(new RewardItemDataModel
			{
				ItemType = RewardItemType.BOOSTER,
				Quantity = 0,
				ItemId = @group.Key,
				Booster = new PackDataModel
				{
					Type = (BoosterDbId)@group.Key,
					Quantity = 0
				}
			}, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				acc.Booster.Quantity += element.Booster.Quantity;
				return acc;
			});
	}

	private static IEnumerable<RewardItemDataModel> ConsolidateCardRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return from element in rewards
			group element by (element.ItemType, element.ItemId, element.Card.Premium) into @group
			select @group.Aggregate(new RewardItemDataModel
			{
				ItemType = @group.Key.ToTuple().Item1,
				Quantity = 0,
				ItemId = @group.Key.ToTuple().Item2,
				Card = new CardDataModel
				{
					Premium = @group.Key.ToTuple().Item3
				}
			}, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				acc.Card.CardId = element.Card.CardId;
				return acc;
			});
	}

	private static IEnumerable<RewardItemDataModel> ConsolidateCardBackRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return from element in rewards
			group element by element.ItemId into @group
			select @group.Aggregate(new RewardItemDataModel
			{
				ItemType = RewardItemType.CARD_BACK,
				Quantity = 0,
				ItemId = @group.Key,
				CardBack = new CardBackDataModel
				{
					CardBackId = @group.Key
				}
			}, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				return acc;
			});
	}

	private static IEnumerable<RewardItemDataModel> ConsolidateCurrencyRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return from element in rewards
			group element by element.ItemType into @group
			select @group.Aggregate(new RewardItemDataModel
			{
				ItemType = @group.Key,
				Quantity = 0,
				Currency = new PriceDataModel
				{
					Currency = RewardUtils.RewardItemTypeToCurrencyType(@group.Key),
					Amount = 0f
				}
			}, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				acc.Currency.Amount += element.Currency.Amount;
				acc.Currency.DisplayText = acc.Quantity.ToString();
				return acc;
			});
	}

	private static IEnumerable<RewardItemDataModel> ConsolidateRandomCardRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return from element in rewards
			group element by (element.ItemId, element.RandomCard.Premium, element.RandomCard.Rarity) into @group
			select @group.Aggregate(new RewardItemDataModel
			{
				ItemType = RewardItemType.RANDOM_CARD,
				Quantity = 0,
				ItemId = @group.Key.ToTuple().Item1,
				RandomCard = new RandomCardDataModel
				{
					Premium = @group.Key.ToTuple().Item2,
					Rarity = @group.Key.ToTuple().Item3,
					Count = 0
				}
			}, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				acc.Card.CardId = element.Card.CardId;
				return acc;
			});
	}

	private static IEnumerable<RewardItemDataModel> ConsolidateRewardTrackXpBoostItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return from element in rewards
			group element by element.ItemType into @group
			select @group.Aggregate(new RewardItemDataModel
			{
				ItemType = @group.Key,
				Quantity = 0
			}, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity = Math.Max(acc.Quantity, element.Quantity);
				return acc;
			});
	}

	private static IEnumerable<RewardItemDataModel> ConsolidateSimpleRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return from element in rewards
			group element by element.ItemType into @group
			select @group.Aggregate(new RewardItemDataModel
			{
				ItemType = @group.Key,
				Quantity = 0
			}, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				return acc;
			});
	}
}
