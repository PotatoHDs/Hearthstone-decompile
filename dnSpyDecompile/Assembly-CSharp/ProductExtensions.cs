using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

// Token: 0x020006AE RID: 1710
public static class ProductExtensions
{
	// Token: 0x06005F60 RID: 24416 RVA: 0x001F0DA0 File Offset: 0x001EEFA0
	public static int GetMaxBulkPurchaseCount(this ProductDataModel product)
	{
		if (!product.ProductSupportsQuantitySelect())
		{
			return 1;
		}
		if (!product.Tags.Contains("arcane_orbs"))
		{
			return 50;
		}
		long cachedBalance = ShopUtils.GetCachedBalance(CurrencyType.ARCANE_ORBS);
		if (cachedBalance >= 9999L)
		{
			return 0;
		}
		float amountOfCurrencyInProduct = ShopUtils.GetAmountOfCurrencyInProduct(product, CurrencyType.ARCANE_ORBS);
		return Mathf.Min(488, Mathf.FloorToInt((float)(9999L - cachedBalance) / amountOfCurrencyInProduct));
	}

	// Token: 0x06005F61 RID: 24417 RVA: 0x001F0E00 File Offset: 0x001EF000
	public static bool ProductSupportsQuantitySelect(this ProductDataModel product)
	{
		return product.Prices.Count == 1 && (product.Tags.Contains("arcane_orbs") || product.GetBuyProductArgs(product.Prices.First<PriceDataModel>(), 1) is BuyNoGTAPPEventArgs);
	}

	// Token: 0x06005F62 RID: 24418 RVA: 0x001F0E40 File Offset: 0x001EF040
	public static BuyProductEventArgs GetBuyProductArgs(this ProductDataModel product, PriceDataModel price, int quantity)
	{
		if (product.PmtId == 0L && price.Currency == CurrencyType.GOLD)
		{
			if (product.Items.Count != 1)
			{
				Log.Store.PrintError(string.Format("Cannot buy product for gold where item count != 1. Name = {0}, Item Count = {1}", product.Name, product.Items.Count), Array.Empty<object>());
				return null;
			}
			RewardItemDataModel rewardItemDataModel = product.Items.First<RewardItemDataModel>();
			ProductType productType = ProductExtensions.RewardItemTypeToNetProductType(rewardItemDataModel.ItemType);
			if (productType == ProductType.PRODUCT_TYPE_UNKNOWN)
			{
				Log.Store.PrintError(string.Format("Cannot buy gold product with unsupported item type {0}. Name = {1}", rewardItemDataModel.ItemType, product.Name), Array.Empty<object>());
				return null;
			}
			return new BuyNoGTAPPEventArgs(new NoGTAPPTransactionData
			{
				Product = productType,
				ProductData = rewardItemDataModel.ItemId,
				Quantity = quantity
			});
		}
		else
		{
			Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(product.PmtId);
			if (bundleFromPmtProductId == null)
			{
				Log.Store.PrintError(string.Format("Cannot buy product with no matching Network.Bundle PMT ID = {0}, Name = {1}", product.PmtId, product.Name), Array.Empty<object>());
				return null;
			}
			return new BuyPmtProductEventArgs(bundleFromPmtProductId, price.Currency, quantity);
		}
	}

	// Token: 0x06005F63 RID: 24419 RVA: 0x001F0F5C File Offset: 0x001EF15C
	public static void BuildItemBundleFromVariantGroup(this ProductDataModel product)
	{
		DataModelList<RewardItemDataModel> dataModelList = new DataModelList<RewardItemDataModel>();
		foreach (ProductDataModel productDataModel in product.Variants)
		{
			if (productDataModel.Items.Count > 0 && CatalogUtils.ShouldItemTypeBeGrouped(productDataModel.Items.First<RewardItemDataModel>().ItemType))
			{
				dataModelList.Add(productDataModel.Items.First<RewardItemDataModel>());
			}
		}
		product.Items = dataModelList;
		product.GenerateRewardList();
		product.Tags.Add("bundle");
	}

	// Token: 0x06005F64 RID: 24420 RVA: 0x001F0FFC File Offset: 0x001EF1FC
	public static void GenerateRewardList(this ProductDataModel product)
	{
		product.RewardList = new RewardListDataModel();
		product.RewardList.Items.AddRange(product.Items);
	}

	// Token: 0x06005F65 RID: 24421 RVA: 0x001F1020 File Offset: 0x001EF220
	public static void FormatProductPrices(this ProductDataModel product, Network.Bundle netBundle = null)
	{
		foreach (PriceDataModel priceDataModel in product.Prices)
		{
			CurrencyType currency = priceDataModel.Currency;
			if (currency == CurrencyType.REAL_MONEY)
			{
				if (netBundle == null)
				{
					netBundle = StoreManager.Get().GetBundleFromPmtProductId(product.PmtId);
				}
				if (netBundle != null)
				{
					priceDataModel.DisplayText = StoreManager.Get().FormatCostBundle(netBundle);
				}
				else
				{
					Log.Store.PrintWarning("Failed to find bundle for formatting cost. May appear wrong on third party store.", Array.Empty<object>());
					priceDataModel.DisplayText = StoreManager.Get().FormatCost(new double?((double)priceDataModel.Amount));
				}
			}
			else
			{
				priceDataModel.DisplayText = Mathf.RoundToInt(priceDataModel.Amount).ToString();
			}
		}
	}

	// Token: 0x06005F66 RID: 24422 RVA: 0x001F10F0 File Offset: 0x001EF2F0
	public static void SetupProductStrings(this ProductDataModel product)
	{
		product.DescriptionHeader = null;
		product.VariantName = null;
		string primaryProductTag = product.GetPrimaryProductTag();
		if (primaryProductTag == "booster")
		{
			BoosterDbId productBoosterId = product.GetProductBoosterId();
			if (productBoosterId != BoosterDbId.INVALID)
			{
				BoosterDbfRecord record = GameDbf.Booster.GetRecord((int)productBoosterId);
				if (record != null && !product.Tags.Contains("has_description"))
				{
					string text = record.Name;
					product.Name = text;
					product.DescriptionHeader = GameStrings.Get("GLUE_STORE_PRODUCT_DETAILS_HEADLINE_PACK");
					product.Description = GameStrings.Format("GLUE_STORE_PRODUCT_DETAILS_PACK", new object[]
					{
						text
					});
					if (GameUtils.IsBoosterWild(record))
					{
						product.Description = product.Description + "\n" + GameStrings.Get("GLUE_SHOP_WILD_CARDS_DISCLAIMER");
					}
				}
			}
			RewardItemDataModel rewardItemDataModel = product.Items.FirstOrDefault((RewardItemDataModel item) => item.ItemType == RewardItemType.BOOSTER);
			if (rewardItemDataModel != null)
			{
				product.VariantName = GameStrings.Format("GLUE_SHOP_BOOSTER_SKU_BUTTON", new object[]
				{
					rewardItemDataModel.Quantity
				});
			}
		}
		else if (primaryProductTag == "runestones")
		{
			RewardItemDataModel rewardItemDataModel2 = product.Items.FirstOrDefault((RewardItemDataModel item) => item.ItemType == RewardItemType.RUNESTONES);
			if (rewardItemDataModel2 != null)
			{
				product.VariantName = GameStrings.Format("GLUE_SHOP_RUNESTONE_SKU_BUTTON", new object[]
				{
					rewardItemDataModel2.Quantity
				});
			}
			product.DescriptionHeader = GameStrings.Get("GLUE_SHOP_RUNESTONES_DETAILS_HEADER");
		}
		else if (primaryProductTag == "mini_set")
		{
			RewardItemDataModel rewardItemDataModel3 = product.Items.FirstOrDefault((RewardItemDataModel item) => item.ItemType == RewardItemType.MINI_SET);
			if (rewardItemDataModel3 != null)
			{
				MiniSetDbfRecord record2 = GameDbf.MiniSet.GetRecord(rewardItemDataModel3.ItemId);
				product.FlavorText = string.Format(GameStrings.Get("GLUE_STORE_MINI_SET_CARD_COUNT"), record2.DeckRecord.Cards.Count);
			}
		}
		else if (primaryProductTag == "sellable_deck")
		{
			RewardItemDataModel rewardItemDataModel4 = product.Items.FirstOrDefault((RewardItemDataModel item) => item.ItemType == RewardItemType.SELLABLE_DECK);
			if (rewardItemDataModel4 != null)
			{
				SellableDeckDbfRecord record3 = GameDbf.SellableDeck.GetRecord(rewardItemDataModel4.ItemId);
				DeckTemplateDbfRecord deckTemplateDbfRecord = (record3 != null) ? record3.DeckTemplateRecord : null;
				if (deckTemplateDbfRecord != null)
				{
					product.FlavorText = string.Format(GameStrings.Get("GLUE_STORE_SELLABLEDECKS_FLAVOR"), GameStrings.GetClassName((TAG_CLASS)deckTemplateDbfRecord.ClassId));
				}
			}
		}
		string descriptionHeader;
		if ((descriptionHeader = product.DescriptionHeader) == null)
		{
			descriptionHeader = GameStrings.Format("GLUE_SHOP_DESCRIPTION_HEADER", new object[]
			{
				product.Name
			});
		}
		product.DescriptionHeader = descriptionHeader;
		product.VariantName = (product.VariantName ?? product.Name);
		string productLegalDisclaimer = product.GetProductLegalDisclaimer();
		if (!string.IsNullOrEmpty(productLegalDisclaimer))
		{
			product.Description = product.Description + "\n" + productLegalDisclaimer;
		}
	}

	// Token: 0x06005F67 RID: 24423 RVA: 0x001F13F8 File Offset: 0x001EF5F8
	public static BoosterDbId GetProductBoosterId(this ProductDataModel product)
	{
		if (!product.Tags.Contains("booster"))
		{
			return BoosterDbId.INVALID;
		}
		BoosterDbId boosterDbId = BoosterDbId.INVALID;
		foreach (RewardItemDataModel rewardItemDataModel in product.Items)
		{
			if (rewardItemDataModel.ItemType != RewardItemType.DUST)
			{
				if (rewardItemDataModel.Booster == null)
				{
					return BoosterDbId.INVALID;
				}
				if (boosterDbId != BoosterDbId.INVALID && rewardItemDataModel.Booster.Type != boosterDbId)
				{
					return BoosterDbId.INVALID;
				}
				boosterDbId = rewardItemDataModel.Booster.Type;
			}
		}
		return boosterDbId;
	}

	// Token: 0x06005F68 RID: 24424 RVA: 0x001F1490 File Offset: 0x001EF690
	public static AdventureDbId GetProductAdventureId(this ProductDataModel product)
	{
		RewardItemDataModel rewardItemDataModel = product.Items.FirstOrDefault((RewardItemDataModel i) => i.ItemType == RewardItemType.ADVENTURE_WING);
		if (rewardItemDataModel != null)
		{
			return GameUtils.GetAdventureIdByWingId(rewardItemDataModel.ItemId);
		}
		return AdventureDbId.INVALID;
	}

	// Token: 0x06005F69 RID: 24425 RVA: 0x001F14D8 File Offset: 0x001EF6D8
	public static string GetPrimaryProductTag(this ProductDataModel product)
	{
		return product.Tags.FirstOrDefault(new Func<string, bool>(CatalogUtils.IsPrimaryProductTag));
	}

	// Token: 0x06005F6A RID: 24426 RVA: 0x001F14F1 File Offset: 0x001EF6F1
	public static bool IsFree(this ProductDataModel product)
	{
		return product.Tags.Contains("free");
	}

	// Token: 0x06005F6B RID: 24427 RVA: 0x001F1503 File Offset: 0x001EF703
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

	// Token: 0x06005F6C RID: 24428 RVA: 0x001F152C File Offset: 0x001EF72C
	public static void SetProductTagPresence(this ProductDataModel product, string tag, bool shouldHave)
	{
		bool flag = product.Tags.Contains(tag);
		if (!flag && shouldHave)
		{
			product.Tags.Add(tag);
			return;
		}
		if (flag && !shouldHave)
		{
			product.Tags.Remove(tag);
		}
	}

	// Token: 0x06005F6D RID: 24429 RVA: 0x001F1570 File Offset: 0x001EF770
	public static bool AddAutomaticTagsAndItems(this ProductDataModel product, Network.Bundle netBundle)
	{
		if (product.Tags.Contains("collapse_wings"))
		{
			List<RewardItemDataModel> list = product.Items.ToList<RewardItemDataModel>();
			for (;;)
			{
				RewardItemDataModel rewardItemDataModel = list.FirstOrDefault((RewardItemDataModel i) => i.ItemType == RewardItemType.ADVENTURE_WING);
				if (rewardItemDataModel == null)
				{
					break;
				}
				AdventureDbId adventureId = GameUtils.GetAdventureIdByWingId(rewardItemDataModel.ItemId);
				list.RemoveAll((RewardItemDataModel i) => i.ItemType == RewardItemType.ADVENTURE_WING && GameUtils.GetAdventureIdByWingId(i.ItemId) == adventureId);
				if (adventureId != AdventureDbId.INVALID)
				{
					list.Add(new RewardItemDataModel
					{
						ItemType = RewardItemType.ADVENTURE,
						ItemId = (int)adventureId,
						Quantity = 1
					});
				}
			}
			list.Sort(new Comparison<RewardItemDataModel>(RewardUtils.CompareItemsForSort));
			product.Items.Clear();
			product.Items.AddRange(list);
		}
		else if (product.Items.Count > 1)
		{
			AdventureDbId adventureId = product.GetProductAdventureId();
			if (adventureId != AdventureDbId.INVALID && product.Items.All((RewardItemDataModel item) => item.ItemType == RewardItemType.ADVENTURE_WING && GameUtils.GetAdventureIdByWingId(item.ItemId) == adventureId))
			{
				product.Items.Insert(0, new RewardItemDataModel
				{
					ItemType = RewardItemType.ADVENTURE,
					ItemId = (int)adventureId,
					Quantity = 1
				});
			}
		}
		if (product.GetPrimaryProductTag() == null)
		{
			string text = product.DetermineProductPrimaryTagFromItems(netBundle);
			if (text == null)
			{
				Log.Store.PrintError(string.Format("Product {0} [{1}] could not determine a primary tag", product.PmtId, product.Name), Array.Empty<object>());
				return false;
			}
			product.Tags.Add(text);
		}
		if (netBundle.IsPrePurchase && !product.Tags.Contains("prepurchase"))
		{
			product.Tags.Add("prepurchase");
		}
		return true;
	}

	// Token: 0x06005F6E RID: 24430 RVA: 0x001F173C File Offset: 0x001EF93C
	private static string DetermineProductPrimaryTagFromItems(this ProductDataModel product, Network.Bundle netBundle)
	{
		if (product.Items.Count == 0)
		{
			Log.Store.PrintError(string.Format("Product {0} [{1}] contains no items", product.PmtId, product.Name), Array.Empty<object>());
			return null;
		}
		if (product.Items.First<RewardItemDataModel>().ItemType == RewardItemType.ADVENTURE)
		{
			if (!product.Items.Any((RewardItemDataModel i) => i.ItemType != RewardItemType.ADVENTURE && i.ItemType != RewardItemType.ADVENTURE_WING))
			{
				return "adventure";
			}
		}
		if (product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.SELLABLE_DECK))
		{
			return "sellable_deck";
		}
		if (netBundle != null)
		{
			if (netBundle.Items.Any((Network.BundleItem i) => i.ItemType == ProductType.PRODUCT_TYPE_HIDDEN_LICENSE))
			{
				return "bundle";
			}
		}
		if (product.Items.Count != 1)
		{
			return "bundle";
		}
		RewardItemDataModel rewardItemDataModel = product.Items.First<RewardItemDataModel>();
		if (rewardItemDataModel.ItemType != RewardItemType.UNDEFINED && Enum.IsDefined(typeof(RewardItemType), rewardItemDataModel.ItemType))
		{
			return Enum.GetName(typeof(RewardItemType), rewardItemDataModel.ItemType).ToLowerInvariant();
		}
		Log.Store.PrintError(string.Format("Product {0} [{1}] has a primary item with unsupported item type {2}", product.PmtId, product.Name, rewardItemDataModel.ItemType), Array.Empty<object>());
		return null;
	}

	// Token: 0x06005F6F RID: 24431 RVA: 0x001F18CC File Offset: 0x001EFACC
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
			else if (product.ContainsBoosterPack())
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
			else
			{
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
					return ProductExtensions.GetGenericKoreanAgreementString();
				}
				if (product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.CARD_BACK))
				{
					return ProductExtensions.GetGenericKoreanAgreementString();
				}
				if (product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.MINI_SET))
				{
					return ProductExtensions.GetGenericKoreanAgreementString();
				}
				if (product.ContainsSellableDeck())
				{
					return ProductExtensions.GetGenericKoreanAgreementString();
				}
			}
		}
		return null;
	}

	// Token: 0x06005F70 RID: 24432 RVA: 0x001F1A07 File Offset: 0x001EFC07
	private static bool ContainsAdventureChapter(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.ADVENTURE_WING);
	}

	// Token: 0x06005F71 RID: 24433 RVA: 0x001F1A33 File Offset: 0x001EFC33
	private static bool ContainsBoosterPack(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.BOOSTER);
	}

	// Token: 0x06005F72 RID: 24434 RVA: 0x001F1A5F File Offset: 0x001EFC5F
	private static bool IsWelcomeBundle(this ProductDataModel product)
	{
		return product.Tags.Contains("welcome_bundle");
	}

	// Token: 0x06005F73 RID: 24435 RVA: 0x001F1A71 File Offset: 0x001EFC71
	private static bool IsPrepurchaseBundle(this ProductDataModel product)
	{
		return product.Tags.Contains("prepurchase");
	}

	// Token: 0x06005F74 RID: 24436 RVA: 0x001F1A83 File Offset: 0x001EFC83
	private static bool ContainsBattlegroundsPerk(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.BATTLEGROUNDS_BONUS);
	}

	// Token: 0x06005F75 RID: 24437 RVA: 0x001F1AAF File Offset: 0x001EFCAF
	private static bool ContainsProgressionBonus(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.PROGRESSION_BONUS);
	}

	// Token: 0x06005F76 RID: 24438 RVA: 0x001F1ADB File Offset: 0x001EFCDB
	private static bool ContainsArenaTicket(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.ARENA_TICKET);
	}

	// Token: 0x06005F77 RID: 24439 RVA: 0x001F1B07 File Offset: 0x001EFD07
	private static bool ContainsHeroSkin(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.HERO_SKIN);
	}

	// Token: 0x06005F78 RID: 24440 RVA: 0x001F1B33 File Offset: 0x001EFD33
	private static bool ContainsSellableDeck(this ProductDataModel product)
	{
		return product.Items.Any((RewardItemDataModel i) => i.ItemType == RewardItemType.SELLABLE_DECK);
	}

	// Token: 0x06005F79 RID: 24441 RVA: 0x001F1B5F File Offset: 0x001EFD5F
	private static string GetGenericKoreanAgreementString()
	{
		return GameStrings.Get("GLUE_STORE_SUMMARY_KOREAN_AGREEMENT_HERO");
	}

	// Token: 0x06005F7A RID: 24442 RVA: 0x001F1B6C File Offset: 0x001EFD6C
	private static ProductType RewardItemTypeToNetProductType(RewardItemType itemType)
	{
		switch (itemType)
		{
		case RewardItemType.BOOSTER:
			return ProductType.PRODUCT_TYPE_BOOSTER;
		case RewardItemType.DUST:
			return ProductType.PRODUCT_TYPE_CURRENCY;
		case RewardItemType.HERO_SKIN:
			return ProductType.PRODUCT_TYPE_HERO;
		case RewardItemType.CARD_BACK:
			return ProductType.PRODUCT_TYPE_CARD_BACK;
		case RewardItemType.ADVENTURE_WING:
			return ProductType.PRODUCT_TYPE_WING;
		case RewardItemType.ARENA_TICKET:
			return ProductType.PRODUCT_TYPE_DRAFT;
		case RewardItemType.RANDOM_CARD:
			return ProductType.PRODUCT_TYPE_RANDOM_CARD;
		case RewardItemType.BATTLEGROUNDS_BONUS:
			return ProductType.PRODUCT_TYPE_BATTLEGROUNDS_BONUS;
		case RewardItemType.TAVERN_BRAWL_TICKET:
			return ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET;
		case RewardItemType.PROGRESSION_BONUS:
			return ProductType.PRODUCT_TYPE_PROGRESSION_BONUS;
		case RewardItemType.MINI_SET:
			return ProductType.PRODUCT_TYPE_MINI_SET;
		case RewardItemType.SELLABLE_DECK:
			return ProductType.PRODUCT_TYPE_SELLABLE_DECK;
		}
		return ProductType.PRODUCT_TYPE_UNKNOWN;
	}
}
