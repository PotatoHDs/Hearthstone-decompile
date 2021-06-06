using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

public static class ProductExtensions
{
	public static int GetMaxBulkPurchaseCount(this ProductDataModel product)
	{
		if (!product.ProductSupportsQuantitySelect())
		{
			return 1;
		}
		if (product.Tags.Contains("arcane_orbs"))
		{
			long cachedBalance = ShopUtils.GetCachedBalance(CurrencyType.ARCANE_ORBS);
			if (cachedBalance >= 9999)
			{
				return 0;
			}
			float amountOfCurrencyInProduct = ShopUtils.GetAmountOfCurrencyInProduct(product, CurrencyType.ARCANE_ORBS);
			return Mathf.Min(488, Mathf.FloorToInt((float)(9999 - cachedBalance) / amountOfCurrencyInProduct));
		}
		return 50;
	}

	public static bool ProductSupportsQuantitySelect(this ProductDataModel product)
	{
		if (product.Prices.Count == 1)
		{
			if (!product.Tags.Contains("arcane_orbs"))
			{
				return product.GetBuyProductArgs(product.Prices.First(), 1) is BuyNoGTAPPEventArgs;
			}
			return true;
		}
		return false;
	}

	public static BuyProductEventArgs GetBuyProductArgs(this ProductDataModel product, PriceDataModel price, int quantity)
	{
		if (product.PmtId == 0L && price.Currency == CurrencyType.GOLD)
		{
			if (product.Items.Count != 1)
			{
				Log.Store.PrintError($"Cannot buy product for gold where item count != 1. Name = {product.Name}, Item Count = {product.Items.Count}");
				return null;
			}
			RewardItemDataModel rewardItemDataModel = product.Items.First();
			ProductType productType = RewardItemTypeToNetProductType(rewardItemDataModel.ItemType);
			if (productType == ProductType.PRODUCT_TYPE_UNKNOWN)
			{
				Log.Store.PrintError($"Cannot buy gold product with unsupported item type {rewardItemDataModel.ItemType}. Name = {product.Name}");
				return null;
			}
			return new BuyNoGTAPPEventArgs(new NoGTAPPTransactionData
			{
				Product = productType,
				ProductData = rewardItemDataModel.ItemId,
				Quantity = quantity
			});
		}
		Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(product.PmtId);
		if (bundleFromPmtProductId == null)
		{
			Log.Store.PrintError($"Cannot buy product with no matching Network.Bundle PMT ID = {product.PmtId}, Name = {product.Name}");
			return null;
		}
		return new BuyPmtProductEventArgs(bundleFromPmtProductId, price.Currency, quantity);
	}

	public static void BuildItemBundleFromVariantGroup(this ProductDataModel product)
	{
		DataModelList<RewardItemDataModel> dataModelList = new DataModelList<RewardItemDataModel>();
		foreach (ProductDataModel variant in product.Variants)
		{
			if (variant.Items.Count > 0 && CatalogUtils.ShouldItemTypeBeGrouped(variant.Items.First().ItemType))
			{
				dataModelList.Add(variant.Items.First());
			}
		}
		product.Items = dataModelList;
		product.GenerateRewardList();
		product.Tags.Add("bundle");
	}

	public static void GenerateRewardList(this ProductDataModel product)
	{
		product.RewardList = new RewardListDataModel();
		product.RewardList.Items.AddRange(product.Items);
	}

	public static void FormatProductPrices(this ProductDataModel product, Network.Bundle netBundle = null)
	{
		foreach (PriceDataModel price in product.Prices)
		{
			CurrencyType currency = price.Currency;
			if (currency == CurrencyType.REAL_MONEY)
			{
				if (netBundle == null)
				{
					netBundle = StoreManager.Get().GetBundleFromPmtProductId(product.PmtId);
				}
				if (netBundle != null)
				{
					price.DisplayText = StoreManager.Get().FormatCostBundle(netBundle);
					continue;
				}
				Log.Store.PrintWarning("Failed to find bundle for formatting cost. May appear wrong on third party store.");
				price.DisplayText = StoreManager.Get().FormatCost(price.Amount);
			}
			else
			{
				price.DisplayText = Mathf.RoundToInt(price.Amount).ToString();
			}
		}
	}

	public static void SetupProductStrings(this ProductDataModel product)
	{
		product.DescriptionHeader = null;
		product.VariantName = null;
		switch (product.GetPrimaryProductTag())
		{
		case "booster":
		{
			BoosterDbId productBoosterId = product.GetProductBoosterId();
			if (productBoosterId != 0)
			{
				BoosterDbfRecord record2 = GameDbf.Booster.GetRecord((int)productBoosterId);
				if (record2 != null && !product.Tags.Contains("has_description"))
				{
					string text2 = (product.Name = record2.Name);
					product.DescriptionHeader = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_PACK");
					product.Description = GameStrings.Format("GLUE_STORE_PRODUCT_DETAILS_PACK", text2);
					if (GameUtils.IsBoosterWild(record2))
					{
						product.Description = product.Description + "\n" + GameStrings.Get("GLUE_SHOP_WILD_CARDS_DISCLAIMER");
					}
				}
			}
			RewardItemDataModel rewardItemDataModel4 = product.Items.FirstOrDefault((RewardItemDataModel item) => item.ItemType == RewardItemType.BOOSTER);
			if (rewardItemDataModel4 != null)
			{
				product.VariantName = GameStrings.Format("GLUE_SHOP_BOOSTER_SKU_BUTTON", rewardItemDataModel4.Quantity);
			}
			break;
		}
		case "runestones":
		{
			RewardItemDataModel rewardItemDataModel2 = product.Items.FirstOrDefault((RewardItemDataModel item) => item.ItemType == RewardItemType.RUNESTONES);
			if (rewardItemDataModel2 != null)
			{
				product.VariantName = GameStrings.Format("GLUE_SHOP_RUNESTONE_SKU_BUTTON", rewardItemDataModel2.Quantity);
			}
			product.DescriptionHeader = GameStrings.Get("GLUE_SHOP_RUNESTONES_DETAILS_HEADER");
			break;
		}
		case "mini_set":
		{
			RewardItemDataModel rewardItemDataModel3 = product.Items.FirstOrDefault((RewardItemDataModel item) => item.ItemType == RewardItemType.MINI_SET);
			if (rewardItemDataModel3 != null)
			{
				MiniSetDbfRecord record = GameDbf.MiniSet.GetRecord(rewardItemDataModel3.ItemId);
				product.FlavorText = string.Format(GameStrings.Get("GLUE_STORE_MINI_SET_CARD_COUNT"), record.DeckRecord.Cards.Count);
			}
			break;
		}
		case "sellable_deck":
		{
			RewardItemDataModel rewardItemDataModel = product.Items.FirstOrDefault((RewardItemDataModel item) => item.ItemType == RewardItemType.SELLABLE_DECK);
			if (rewardItemDataModel != null)
			{
				DeckTemplateDbfRecord deckTemplateDbfRecord = GameDbf.SellableDeck.GetRecord(rewardItemDataModel.ItemId)?.DeckTemplateRecord;
				if (deckTemplateDbfRecord != null)
				{
					product.FlavorText = string.Format(GameStrings.Get("GLUE_STORE_SELLABLEDECKS_FLAVOR"), GameStrings.GetClassName((TAG_CLASS)deckTemplateDbfRecord.ClassId));
				}
			}
			break;
		}
		}
		product.DescriptionHeader = product.DescriptionHeader ?? GameStrings.Format("GLUE_SHOP_DESCRIPTION_HEADER", product.Name);
		product.VariantName = product.VariantName ?? product.Name;
		string productLegalDisclaimer = product.GetProductLegalDisclaimer();
		if (!string.IsNullOrEmpty(productLegalDisclaimer))
		{
			product.Description = product.Description + "\n" + productLegalDisclaimer;
		}
	}

	public static BoosterDbId GetProductBoosterId(this ProductDataModel product)
	{
		if (!product.Tags.Contains("booster"))
		{
			return BoosterDbId.INVALID;
		}
		BoosterDbId boosterDbId = BoosterDbId.INVALID;
		foreach (RewardItemDataModel item in product.Items)
		{
			if (item.ItemType != RewardItemType.DUST)
			{
				if (item.Booster == null)
				{
					return BoosterDbId.INVALID;
				}
				if (boosterDbId != 0 && item.Booster.Type != boosterDbId)
				{
					return BoosterDbId.INVALID;
				}
				boosterDbId = item.Booster.Type;
			}
		}
		return boosterDbId;
	}

	public static AdventureDbId GetProductAdventureId(this ProductDataModel product)
	{
		RewardItemDataModel rewardItemDataModel = product.Items.FirstOrDefault((RewardItemDataModel i) => i.ItemType == RewardItemType.ADVENTURE_WING);
		if (rewardItemDataModel != null)
		{
			return GameUtils.GetAdventureIdByWingId(rewardItemDataModel.ItemId);
		}
		return AdventureDbId.INVALID;
	}

	public static string GetPrimaryProductTag(this ProductDataModel product)
	{
		return product.Tags.FirstOrDefault(CatalogUtils.IsPrimaryProductTag);
	}

	public static bool IsFree(this ProductDataModel product)
	{
		return product.Tags.Contains("free");
	}

	public static ShopBrowserButtonDataModel ToButton(this ProductDataModel product, bool isFiller = false)
	{
		return new ShopBrowserButtonDataModel
		{
			DisplayProduct = product,
			DisplayText = product.Name,
			IsFiller = isFiller,
			Hovered = false
		};
	}

	public static void SetProductTagPresence(this ProductDataModel product, string tag, bool shouldHave)
	{
		bool flag = product.Tags.Contains(tag);
		if (!flag && shouldHave)
		{
			product.Tags.Add(tag);
		}
		else if (flag && !shouldHave)
		{
			product.Tags.Remove(tag);
		}
	}

	public static bool AddAutomaticTagsAndItems(this ProductDataModel product, Network.Bundle netBundle)
	{
		if (product.Tags.Contains("collapse_wings"))
		{
			List<RewardItemDataModel> list = product.Items.ToList();
			while (true)
			{
				RewardItemDataModel rewardItemDataModel = list.FirstOrDefault((RewardItemDataModel i) => i.ItemType == RewardItemType.ADVENTURE_WING);
				if (rewardItemDataModel == null)
				{
					break;
				}
				AdventureDbId adventureId2 = GameUtils.GetAdventureIdByWingId(rewardItemDataModel.ItemId);
				list.RemoveAll((RewardItemDataModel i) => i.ItemType == RewardItemType.ADVENTURE_WING && GameUtils.GetAdventureIdByWingId(i.ItemId) == adventureId2);
				if (adventureId2 != 0)
				{
					list.Add(new RewardItemDataModel
					{
						ItemType = RewardItemType.ADVENTURE,
						ItemId = (int)adventureId2,
						Quantity = 1
					});
				}
			}
			list.Sort(RewardUtils.CompareItemsForSort);
			product.Items.Clear();
			product.Items.AddRange(list);
		}
		else if (product.Items.Count > 1)
		{
			AdventureDbId adventureId = product.GetProductAdventureId();
			if (adventureId != 0 && product.Items.All((RewardItemDataModel item) => item.ItemType == RewardItemType.ADVENTURE_WING && GameUtils.GetAdventureIdByWingId(item.ItemId) == adventureId))
			{
				product.Items.Insert(0, new RewardItemDataModel
				{
					ItemType = RewardItemType.ADVENTURE,
					ItemId = (int)adventureId,
					Quantity = 1
				});
			}
		}
		string primaryProductTag = product.GetPrimaryProductTag();
		if (primaryProductTag == null)
		{
			primaryProductTag = product.DetermineProductPrimaryTagFromItems(netBundle);
			if (primaryProductTag == null)
			{
				Log.Store.PrintError($"Product {product.PmtId} [{product.Name}] could not determine a primary tag");
				return false;
			}
			product.Tags.Add(primaryProductTag);
		}
		if (netBundle.IsPrePurchase && !product.Tags.Contains("prepurchase"))
		{
			product.Tags.Add("prepurchase");
		}
		return true;
	}

	private static string DetermineProductPrimaryTagFromItems(this ProductDataModel product, Network.Bundle netBundle)
	{
		if (product.Items.Count == 0)
		{
			Log.Store.PrintError($"Product {product.PmtId} [{product.Name}] contains no items");
			return null;
		}
		if (product.Items.First().ItemType == RewardItemType.ADVENTURE && !product.Items.Any((RewardItemDataModel i) => i.ItemType != RewardItemType.ADVENTURE && i.ItemType != RewardItemType.ADVENTURE_WING))
		{
			return "adventure";
		}
		if (product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.SELLABLE_DECK))
		{
			return "sellable_deck";
		}
		if (netBundle != null && netBundle.Items.Any((Network.BundleItem i) => i.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE))
		{
			return "bundle";
		}
		if (product.Items.Count == 1)
		{
			RewardItemDataModel rewardItemDataModel = product.Items.First();
			if (rewardItemDataModel.ItemType != 0 && Enum.IsDefined(typeof(RewardItemType), rewardItemDataModel.ItemType))
			{
				return Enum.GetName(typeof(RewardItemType), rewardItemDataModel.ItemType).ToLowerInvariant();
			}
			Log.Store.PrintError($"Product {product.PmtId} [{product.Name}] has a primary item with unsupported item type {rewardItemDataModel.ItemType}");
			return null;
		}
		return "bundle";
	}

	private static string GetProductLegalDisclaimer(this ProductDataModel product)
	{
		if (StoreManager.Get().IsKoreanCustomer())
		{
			if (product.ContainsAdventureChapter())
			{
				if (product.Items.Count == 1)
				{
					return GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_ADVENTURE_SINGLE");
				}
				return GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_ADVENTURE_BUNDLE");
			}
			if (product.ContainsBoosterPack())
			{
				if (product.IsWelcomeBundle())
				{
					return GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_FIRST_PURCHASE_BUNDLE");
				}
				if (product.IsPrepurchaseBundle())
				{
					return GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_PACK_PREORDER");
				}
				return GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_EXPERT_PACK");
			}
			if (product.ContainsBattlegroundsPerk())
			{
				return GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_BATTLEGROUNDS_PERKS");
			}
			if (product.ContainsProgressionBonus())
			{
				return GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_PROGRESSION_BONUS");
			}
			if (product.ContainsArenaTicket())
			{
				return GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_FORGE_TICKET");
			}
			if (product.ContainsHeroSkin())
			{
				return GetGenericKoreanAgreementString();
			}
			if (product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.CARD_BACK))
			{
				return GetGenericKoreanAgreementString();
			}
			if (product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.MINI_SET))
			{
				return GetGenericKoreanAgreementString();
			}
			if (product.ContainsSellableDeck())
			{
				return GetGenericKoreanAgreementString();
			}
		}
		return null;
	}

	private static bool ContainsAdventureChapter(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.ADVENTURE_WING);
	}

	private static bool ContainsBoosterPack(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.BOOSTER);
	}

	private static bool IsWelcomeBundle(this ProductDataModel product)
	{
		return product.Tags.Contains("welcome_bundle");
	}

	private static bool IsPrepurchaseBundle(this ProductDataModel product)
	{
		return product.Tags.Contains("prepurchase");
	}

	private static bool ContainsBattlegroundsPerk(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.BATTLEGROUNDS_BONUS);
	}

	private static bool ContainsProgressionBonus(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.PROGRESSION_BONUS);
	}

	private static bool ContainsArenaTicket(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.ARENA_TICKET);
	}

	private static bool ContainsHeroSkin(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.HERO_SKIN);
	}

	private static bool ContainsSellableDeck(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.SELLABLE_DECK);
	}

	private static string GetGenericKoreanAgreementString()
	{
		return GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_HERO");
	}

	private static ProductType RewardItemTypeToNetProductType(RewardItemType itemType)
	{
		return itemType switch
		{
			RewardItemType.BOOSTER => ProductType.PRODUCT_TYPE_BOOSTER, 
			RewardItemType.DUST => ProductType.PRODUCT_TYPE_CURRENCY, 
			RewardItemType.HERO_SKIN => ProductType.PRODUCT_TYPE_HERO, 
			RewardItemType.CARD_BACK => ProductType.PRODUCT_TYPE_CARD_BACK, 
			RewardItemType.ADVENTURE_WING => ProductType.PRODUCT_TYPE_WING, 
			RewardItemType.ARENA_TICKET => ProductType.PRODUCT_TYPE_DRAFT, 
			RewardItemType.RANDOM_CARD => ProductType.PRODUCT_TYPE_RANDOM_CARD, 
			RewardItemType.BATTLEGROUNDS_BONUS => ProductType.PRODUCT_TYPE_BATTLEGROUNDS_BONUS, 
			RewardItemType.TAVERN_BRAWL_TICKET => ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET, 
			RewardItemType.PROGRESSION_BONUS => ProductType.PRODUCT_TYPE_PROGRESSION_BONUS, 
			RewardItemType.MINI_SET => ProductType.PRODUCT_TYPE_MINI_SET, 
			RewardItemType.SELLABLE_DECK => ProductType.PRODUCT_TYPE_SELLABLE_DECK, 
			_ => ProductType.PRODUCT_TYPE_UNKNOWN, 
		};
	}
}
