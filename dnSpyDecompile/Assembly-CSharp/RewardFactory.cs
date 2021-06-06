using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets;
using Hearthstone.DataModels;
using PegasusShared;
using PegasusUtil;

// Token: 0x0200067D RID: 1661
public class RewardFactory
{
	// Token: 0x06005CEE RID: 23790 RVA: 0x001E184C File Offset: 0x001DFA4C
	public static List<RewardItemDataModel> CreateRewardItemDataModel(RewardItemDbfRecord itemRecord, int? rewardItemOutputData = null)
	{
		List<RewardItemDataModel> list = new List<RewardItemDataModel>();
		switch (itemRecord.RewardType)
		{
		case RewardItem.RewardType.GOLD:
		case RewardItem.RewardType.TAVERN_TICKET:
		case RewardItem.RewardType.REWARD_TRACK_XP_BOOST:
			list.Add(RewardFactory.CreateSimpleRewardItemDataModel(itemRecord));
			return list;
		case RewardItem.RewardType.DUST:
		case RewardItem.RewardType.ARCANE_ORBS:
			list.Add(RewardFactory.CreateCurrencyRewardItemDataModel(itemRecord));
			return list;
		case RewardItem.RewardType.BOOSTER:
			list.Add(RewardFactory.CreateBoosterRewardItemDataModel(itemRecord));
			return list;
		case RewardItem.RewardType.CARD:
			list.Add(RewardFactory.CreateCardRewardItemDataModel(itemRecord));
			return list;
		case RewardItem.RewardType.RANDOM_CARD:
			list.Add(RewardFactory.CreateRandomCardRewardItemDataModel(itemRecord, rewardItemOutputData));
			return list;
		case RewardItem.RewardType.CARD_BACK:
			list.Add(RewardFactory.CreateCardBackRewardItemDataModel(itemRecord));
			return list;
		case RewardItem.RewardType.HERO_SKIN:
			list.Add(RewardFactory.CreateHeroSkinRewardItemDataModel(itemRecord));
			return list;
		case RewardItem.RewardType.CUSTOM_COIN:
			list.Add(RewardFactory.CreateCustomCoinRewardItemDataModel(itemRecord));
			return list;
		case RewardItem.RewardType.CARD_SUBSET:
			list.AddRange(RewardFactory.CreateCardSubsetRewardItemDataModel(itemRecord));
			return list;
		}
		Log.All.PrintWarning(string.Format("RewardItem has unsupported item type [{0}]", itemRecord.RewardType), Array.Empty<object>());
		return null;
	}

	// Token: 0x06005CEF RID: 23791 RVA: 0x001E1954 File Offset: 0x001DFB54
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

	// Token: 0x06005CF0 RID: 23792 RVA: 0x001E19CC File Offset: 0x001DFBCC
	private static RewardItemDataModel CreateCardRewardItemDataModel(RewardItemDbfRecord record)
	{
		CardDbfRecord record2 = GameDbf.Card.GetRecord(record.Card);
		if (record2 == null)
		{
			Log.All.PrintWarning(string.Format("Card Item has unknown card id [{0}]", record.Card), Array.Empty<object>());
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

	// Token: 0x06005CF1 RID: 23793 RVA: 0x001E1A78 File Offset: 0x001DFC78
	private static List<RewardItemDataModel> CreateCardSubsetRewardItemDataModel(RewardItemDbfRecord record)
	{
		List<RewardItemDataModel> list = new List<RewardItemDataModel>();
		foreach (string cardId in GameDbf.GetIndex().GetSubsetById(record.SubsetId))
		{
			CardDbfRecord record2 = GameDbf.Card.GetRecord(GameUtils.TranslateCardIdToDbId(cardId, false));
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

	// Token: 0x06005CF2 RID: 23794 RVA: 0x001E1B4C File Offset: 0x001DFD4C
	private static RewardItemDataModel CreateCardBackRewardItemDataModel(RewardItemDbfRecord record)
	{
		if (!GameDbf.CardBack.HasRecord(record.CardBack))
		{
			Log.All.PrintWarning(string.Format("Card Back Item has unrecognized card back id [{0}]", record.CardBack), Array.Empty<object>());
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

	// Token: 0x06005CF3 RID: 23795 RVA: 0x001E1BE4 File Offset: 0x001DFDE4
	private static RewardItemDataModel CreateCurrencyRewardItemDataModel(RewardItemDbfRecord record)
	{
		RewardItemType itemType = record.RewardType.ToRewardItemType();
		global::CurrencyType currency = RewardUtils.RewardItemTypeToCurrencyType(itemType);
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
				Amount = (float)record.Quantity,
				DisplayText = record.Quantity.ToString()
			}
		};
	}

	// Token: 0x06005CF4 RID: 23796 RVA: 0x001E1C6C File Offset: 0x001DFE6C
	private static RewardItemDataModel CreateCustomCoinRewardItemDataModel(RewardItemDbfRecord record)
	{
		CoinDbfRecord record2 = GameDbf.Coin.GetRecord(record.CustomCoin);
		if (record2 == null)
		{
			Log.All.PrintWarning(string.Format("Custom Coin Item has unknown id [{0}]", record.CustomCoin), Array.Empty<object>());
			return null;
		}
		CardDbfRecord record3 = GameDbf.Card.GetRecord(record2.CardId);
		if (record3 == null)
		{
			Log.All.PrintWarning(string.Format("Custom Coin Item has unknown card id [{0}]", record2.CardId), Array.Empty<object>());
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

	// Token: 0x06005CF5 RID: 23797 RVA: 0x001E1D50 File Offset: 0x001DFF50
	private static RewardItemDataModel CreateHeroSkinRewardItemDataModel(RewardItemDbfRecord record)
	{
		CardDbfRecord record2 = GameDbf.Card.GetRecord(record.Card);
		if (((record2 != null) ? record2.CardHero : null) == null)
		{
			Log.All.PrintWarning(string.Format("Hero Skin Item has invalid card id [{0}] where card dbf record has", record.Card) + " no CARD_HERO subtable. NoteMiniGuid = " + ((record2 != null) ? record2.NoteMiniGuid : null), Array.Empty<object>());
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

	// Token: 0x06005CF6 RID: 23798 RVA: 0x001E1E60 File Offset: 0x001E0060
	private static RewardItemDataModel CreateRandomCardRewardItemDataModel(RewardItemDbfRecord record, int? rewardItemOutputData = null)
	{
		RewardItemDataModel rewardItemDataModel = new RewardItemDataModel
		{
			AssetId = record.ID,
			Quantity = record.Quantity
		};
		if (rewardItemOutputData != null)
		{
			int value = rewardItemOutputData.Value;
			CardDbfRecord record2 = GameDbf.Card.GetRecord(value);
			if (record2 == null)
			{
				Log.All.PrintWarning(string.Format("Random Card Item has unknown output card id [{0}]", value), Array.Empty<object>());
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

	// Token: 0x06005CF7 RID: 23799 RVA: 0x001E1F57 File Offset: 0x001E0157
	private static RewardItemDataModel CreateSimpleRewardItemDataModel(RewardItemDbfRecord record)
	{
		return new RewardItemDataModel
		{
			AssetId = record.ID,
			Quantity = record.Quantity,
			ItemType = record.RewardType.ToRewardItemType()
		};
	}

	// Token: 0x06005CF8 RID: 23800 RVA: 0x001E1F87 File Offset: 0x001E0187
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

	// Token: 0x06005CF9 RID: 23801 RVA: 0x001E1FC0 File Offset: 0x001E01C0
	public static RewardItemDataModel CreateShopRewardItemDataModel(Network.Bundle netBundle, Network.BundleItem netBundleItem, out bool isValidItem)
	{
		string arg = (netBundle.DisplayName != null) ? netBundle.DisplayName.GetString(true) : "";
		RewardItemDataModel rewardItemDataModel;
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
		case ProductType.PRODUCT_TYPE_DRAFT:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.ARENA_TICKET,
				Quantity = netBundleItem.Quantity
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
		case ProductType.PRODUCT_TYPE_RANDOM_CARD:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.RANDOM_CARD,
				ItemId = netBundleItem.ProductData,
				Quantity = netBundleItem.Quantity
			};
			break;
		case ProductType.PRODUCT_TYPE_HIDDEN_LICENSE:
		case ProductType.PRODUCT_TYPE_FIXED_LICENSE:
			isValidItem = true;
			return null;
		case ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET:
			rewardItemDataModel = new RewardItemDataModel
			{
				ItemType = RewardItemType.TAVERN_BRAWL_TICKET,
				ItemId = netBundleItem.ProductData,
				Quantity = 1
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
				Log.Store.PrintWarning(string.Format("Product Error [PMT ID = {0}, Name = {1}]: Has unsupported currency type {2}", netBundle.PMTProductID, arg, productData), Array.Empty<object>());
				isValidItem = false;
				return null;
			}
			break;
		}
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
			Log.Store.PrintWarning(string.Format("Product {0} [{1}] contains unrecognized item type [{2}]", netBundle.PMTProductID, arg, netBundleItem.ItemType), Array.Empty<object>());
			isValidItem = false;
			return null;
		}
		string warningPrefix = string.Format("Product Error [PMT ID = {0}, Name = {1}]: ", netBundle.PMTProductID, arg);
		isValidItem = RewardUtils.InitializeRewardItemDataModelForShop(rewardItemDataModel, netBundleItem, warningPrefix);
		return rewardItemDataModel;
	}

	// Token: 0x06005CFA RID: 23802 RVA: 0x001E22C8 File Offset: 0x001E04C8
	public static IEnumerable<RewardItemDataModel> ConsolidateGroup(IGrouping<RewardItemType, RewardItemDataModel> group)
	{
		switch (group.Key)
		{
		case RewardItemType.BOOSTER:
			return RewardFactory.ConsolidateBoosterRewardItems(group);
		case RewardItemType.DUST:
		case RewardItemType.ARCANE_ORBS:
			return RewardFactory.ConsolidateCurrencyRewardItems(group);
		case RewardItemType.HERO_SKIN:
		case RewardItemType.CARD:
		case RewardItemType.CUSTOM_COIN:
			return RewardFactory.ConsolidateCardRewardItems(group);
		case RewardItemType.CARD_BACK:
			return RewardFactory.ConsolidateCardBackRewardItems(group);
		case RewardItemType.ARENA_TICKET:
		case RewardItemType.GOLD:
			return RewardFactory.ConsolidateSimpleRewardItems(group);
		case RewardItemType.RANDOM_CARD:
			return RewardFactory.ConsolidateRandomCardRewardItems(group);
		case RewardItemType.REWARD_TRACK_XP_BOOST:
			return RewardFactory.ConsolidateRewardTrackXpBoostItems(group);
		}
		Log.All.PrintWarning("RewardItem has unsupported item type [{0}]", new object[]
		{
			group.Key
		});
		return Enumerable.Empty<RewardItemDataModel>();
	}

	// Token: 0x06005CFB RID: 23803 RVA: 0x001E2384 File Offset: 0x001E0584
	private static IEnumerable<RewardItemDataModel> ConsolidateBoosterRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return (from element in rewards
		group element by element.ItemId).Select(delegate(IGrouping<int, RewardItemDataModel> group)
		{
			RewardItemDataModel rewardItemDataModel = new RewardItemDataModel();
			rewardItemDataModel.ItemType = RewardItemType.BOOSTER;
			rewardItemDataModel.Quantity = 0;
			rewardItemDataModel.ItemId = group.Key;
			rewardItemDataModel.Booster = new PackDataModel
			{
				Type = (BoosterDbId)group.Key,
				Quantity = 0
			};
			return group.Aggregate(rewardItemDataModel, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				acc.Booster.Quantity += element.Booster.Quantity;
				return acc;
			});
		});
	}

	// Token: 0x06005CFC RID: 23804 RVA: 0x001E23DC File Offset: 0x001E05DC
	private static IEnumerable<RewardItemDataModel> ConsolidateCardRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return (from element in rewards
		group element by new ValueTuple<RewardItemType, int, TAG_PREMIUM>(element.ItemType, element.ItemId, element.Card.Premium)).Select(delegate([TupleElementNames(new string[]
		{
			"ItemType",
			"ItemId",
			"Premium"
		})] IGrouping<ValueTuple<RewardItemType, int, TAG_PREMIUM>, RewardItemDataModel> group)
		{
			RewardItemDataModel rewardItemDataModel = new RewardItemDataModel();
			rewardItemDataModel.ItemType = group.Key.ToTuple<RewardItemType, int, TAG_PREMIUM>().Item1;
			rewardItemDataModel.Quantity = 0;
			rewardItemDataModel.ItemId = group.Key.ToTuple<RewardItemType, int, TAG_PREMIUM>().Item2;
			rewardItemDataModel.Card = new CardDataModel
			{
				Premium = group.Key.ToTuple<RewardItemType, int, TAG_PREMIUM>().Item3
			};
			return group.Aggregate(rewardItemDataModel, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				acc.Card.CardId = element.Card.CardId;
				return acc;
			});
		});
	}

	// Token: 0x06005CFD RID: 23805 RVA: 0x001E2434 File Offset: 0x001E0634
	private static IEnumerable<RewardItemDataModel> ConsolidateCardBackRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return (from element in rewards
		group element by element.ItemId).Select(delegate(IGrouping<int, RewardItemDataModel> group)
		{
			RewardItemDataModel rewardItemDataModel = new RewardItemDataModel();
			rewardItemDataModel.ItemType = RewardItemType.CARD_BACK;
			rewardItemDataModel.Quantity = 0;
			rewardItemDataModel.ItemId = group.Key;
			rewardItemDataModel.CardBack = new CardBackDataModel
			{
				CardBackId = group.Key
			};
			return group.Aggregate(rewardItemDataModel, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				return acc;
			});
		});
	}

	// Token: 0x06005CFE RID: 23806 RVA: 0x001E248C File Offset: 0x001E068C
	private static IEnumerable<RewardItemDataModel> ConsolidateCurrencyRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return (from element in rewards
		group element by element.ItemType).Select(delegate(IGrouping<RewardItemType, RewardItemDataModel> group)
		{
			RewardItemDataModel rewardItemDataModel = new RewardItemDataModel();
			rewardItemDataModel.ItemType = group.Key;
			rewardItemDataModel.Quantity = 0;
			rewardItemDataModel.Currency = new PriceDataModel
			{
				Currency = RewardUtils.RewardItemTypeToCurrencyType(group.Key),
				Amount = 0f
			};
			return group.Aggregate(rewardItemDataModel, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				acc.Currency.Amount += element.Currency.Amount;
				acc.Currency.DisplayText = acc.Quantity.ToString();
				return acc;
			});
		});
	}

	// Token: 0x06005CFF RID: 23807 RVA: 0x001E24E4 File Offset: 0x001E06E4
	private static IEnumerable<RewardItemDataModel> ConsolidateRandomCardRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return (from element in rewards
		group element by new ValueTuple<int, TAG_PREMIUM, TAG_RARITY>(element.ItemId, element.RandomCard.Premium, element.RandomCard.Rarity)).Select(delegate([TupleElementNames(new string[]
		{
			"ItemId",
			"Premium",
			"Rarity"
		})] IGrouping<ValueTuple<int, TAG_PREMIUM, TAG_RARITY>, RewardItemDataModel> group)
		{
			RewardItemDataModel rewardItemDataModel = new RewardItemDataModel();
			rewardItemDataModel.ItemType = RewardItemType.RANDOM_CARD;
			rewardItemDataModel.Quantity = 0;
			rewardItemDataModel.ItemId = group.Key.ToTuple<int, TAG_PREMIUM, TAG_RARITY>().Item1;
			rewardItemDataModel.RandomCard = new RandomCardDataModel
			{
				Premium = group.Key.ToTuple<int, TAG_PREMIUM, TAG_RARITY>().Item2,
				Rarity = group.Key.ToTuple<int, TAG_PREMIUM, TAG_RARITY>().Item3,
				Count = 0
			};
			return group.Aggregate(rewardItemDataModel, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				acc.Card.CardId = element.Card.CardId;
				return acc;
			});
		});
	}

	// Token: 0x06005D00 RID: 23808 RVA: 0x001E253C File Offset: 0x001E073C
	private static IEnumerable<RewardItemDataModel> ConsolidateRewardTrackXpBoostItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return (from element in rewards
		group element by element.ItemType).Select(delegate(IGrouping<RewardItemType, RewardItemDataModel> group)
		{
			RewardItemDataModel rewardItemDataModel = new RewardItemDataModel();
			rewardItemDataModel.ItemType = group.Key;
			rewardItemDataModel.Quantity = 0;
			return group.Aggregate(rewardItemDataModel, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity = Math.Max(acc.Quantity, element.Quantity);
				return acc;
			});
		});
	}

	// Token: 0x06005D01 RID: 23809 RVA: 0x001E2594 File Offset: 0x001E0794
	private static IEnumerable<RewardItemDataModel> ConsolidateSimpleRewardItems(IEnumerable<RewardItemDataModel> rewards)
	{
		return (from element in rewards
		group element by element.ItemType).Select(delegate(IGrouping<RewardItemType, RewardItemDataModel> group)
		{
			RewardItemDataModel rewardItemDataModel = new RewardItemDataModel();
			rewardItemDataModel.ItemType = group.Key;
			rewardItemDataModel.Quantity = 0;
			return group.Aggregate(rewardItemDataModel, delegate(RewardItemDataModel acc, RewardItemDataModel element)
			{
				acc.AssetId = element.AssetId;
				acc.Quantity += element.Quantity;
				return acc;
			});
		});
	}
}
