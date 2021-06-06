using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using PegasusUtil;
using UnityEngine;

// Token: 0x020006BD RID: 1725
public class ShopProductDataConverter : MonoBehaviour
{
	// Token: 0x060060A5 RID: 24741 RVA: 0x001F7BB0 File Offset: 0x001F5DB0
	private void Update()
	{
		if (this.m_captureCurrentCatalog && StoreManager.Get().Catalog.HasData)
		{
			this.m_captureCurrentCatalog = false;
			this.SnapshotCurrentCatalog();
		}
		if (this.m_generateTestData)
		{
			this.m_generateTestData = false;
			this.BuildTestData();
		}
		if (this.m_flushData)
		{
			this.m_flushData = false;
			this.FlushData();
		}
	}

	// Token: 0x060060A6 RID: 24742 RVA: 0x001F7C10 File Offset: 0x001F5E10
	private void SnapshotCurrentCatalog()
	{
		ProductCatalog catalog = StoreManager.Get().Catalog;
		this.m_itemCatalog = new List<ShopProductData.ProductItemData>();
		this.m_productCatalog = new List<ShopProductData.ProductData>();
		this.m_fakeLicenseId = 404000L;
		List<ShopProductData.ProductTierData> list = new List<ShopProductData.ProductTierData>();
		foreach (ProductTierDataModel productTierDataModel in catalog.Tiers)
		{
			List<long> list2 = new List<long>();
			foreach (ShopBrowserButtonDataModel shopBrowserButtonDataModel in productTierDataModel.BrowserButtons)
			{
				list2.Add(shopBrowserButtonDataModel.DisplayProduct.PmtId);
			}
			ShopProductData.ProductTierData item = new ShopProductData.ProductTierData
			{
				tierId = productTierDataModel.Style,
				tags = string.Join(",", productTierDataModel.Tags.ToArray<string>()),
				header = productTierDataModel.Header,
				productIds = list2.ToArray()
			};
			list.Add(item);
		}
		foreach (ProductDataModel productDataModel in catalog.Products)
		{
			ShopProductData.ProductData item2 = new ShopProductData.ProductData
			{
				name = productDataModel.Name,
				description = productDataModel.Description,
				productId = productDataModel.PmtId,
				tags = string.Join(",", productDataModel.Tags.ToArray<string>())
			};
			List<ShopProductData.PriceData> list3 = new List<ShopProductData.PriceData>();
			foreach (PriceDataModel priceDataModel in productDataModel.Prices)
			{
				ShopProductData.PriceData item3 = new ShopProductData.PriceData
				{
					amount = (double)priceDataModel.Amount,
					currencyType = priceDataModel.Currency
				};
				list3.Add(item3);
			}
			item2.prices = list3.ToArray();
			List<long> list4 = new List<long>();
			foreach (RewardItemDataModel rewardItemDataModel in productDataModel.Items)
			{
				ShopProductData.ProductItemData productItemData = new ShopProductData.ProductItemData
				{
					itemId = rewardItemDataModel.ItemId,
					itemType = rewardItemDataModel.ItemType,
					licenseId = ((rewardItemDataModel.PmtLicenseId == 0L) ? this.GetUniqueFakeId() : rewardItemDataModel.PmtLicenseId),
					quantity = rewardItemDataModel.Quantity
				};
				this.FillInDebugItemName(ref productItemData);
				this.m_itemCatalog.Add(productItemData);
				list4.Add(productItemData.licenseId);
			}
			item2.licenseIds = list4.ToArray();
			this.m_productCatalog.Add(item2);
		}
		this.m_data.productTierCatalog = list.ToArray();
		this.m_data.productCatalog = this.m_productCatalog.ToArray();
		this.m_data.productItemCatalog = this.m_itemCatalog.ToArray();
	}

	// Token: 0x060060A7 RID: 24743 RVA: 0x001F7FB0 File Offset: 0x001F61B0
	private void BuildTestData()
	{
		this.m_itemCatalog = new List<ShopProductData.ProductItemData>();
		this.m_productCatalog = new List<ShopProductData.ProductData>();
		this.m_fakeLicenseId = 404000L;
		foreach (ModularBundleDbfRecord modularBundleDbfRecord in GameDbf.ModularBundle.GetRecords())
		{
			if (!(modularBundleDbfRecord.Name == ""))
			{
				StorePackId storePackId = new StorePackId
				{
					Type = StorePackType.MODULAR_BUNDLE,
					Id = modularBundleDbfRecord.ID
				};
				if (!this.AddItemsAndProductsFromStorePack(storePackId))
				{
					Log.Store.PrintWarning("Could not add test data from Network.Bundles for bundle '{0}' (storePackId: {1})", new object[]
					{
						modularBundleDbfRecord.Name,
						storePackId
					});
					string[] tagsOverride = new string[]
					{
						"bundle"
					};
					this.AddDummyItemAndProduct(RewardItemType.BOOSTER, 1, modularBundleDbfRecord.Name, tagsOverride);
				}
			}
		}
		foreach (BoosterDbfRecord boosterDbfRecord in GameDbf.Booster.GetRecords())
		{
			if (!(boosterDbfRecord.Name == "") && boosterDbfRecord.StorePrefab != null)
			{
				StorePackId storePackId2 = new StorePackId
				{
					Type = StorePackType.BOOSTER,
					Id = boosterDbfRecord.ID
				};
				if (!this.AddItemsAndProductsFromStorePack(storePackId2))
				{
					Log.Store.PrintWarning("Could not add test data from Network.Bundles for booster '{0}' (storePackId: {1})", new object[]
					{
						boosterDbfRecord.Name,
						storePackId2
					});
					this.AddDummyItemAndProduct(RewardItemType.BOOSTER, boosterDbfRecord.ID, boosterDbfRecord.Name, null);
				}
			}
		}
		int num = 10;
		foreach (CardBackDbfRecord cardBackDbfRecord in GameDbf.CardBack.GetRecords())
		{
			if (!(cardBackDbfRecord.Name == ""))
			{
				this.AddDummyItemAndProduct(RewardItemType.CARD_BACK, cardBackDbfRecord.ID, cardBackDbfRecord.Name, null);
				if (--num <= 0)
				{
					break;
				}
			}
		}
		foreach (CardHeroDbfRecord cardHeroDbfRecord in GameDbf.CardHero.GetRecords())
		{
			CardDbfRecord record = GameDbf.Card.GetRecord(cardHeroDbfRecord.CardId);
			if (record != null && !(record.Name == ""))
			{
				Network.Bundle bundle = null;
				StoreManager.Get().GetHeroBundleByCardDbId(cardHeroDbfRecord.CardId, out bundle);
				if (bundle == null || !this.AddItemsAndProductsFromNetBundle(bundle, record.Name, null))
				{
					Log.Store.PrintWarning("Could not add test data from Network.Bundles for card '{0}' (hero CardId: {1})", new object[]
					{
						record.Name,
						cardHeroDbfRecord.CardId
					});
					this.AddDummyItemAndProduct(RewardItemType.HERO_SKIN, record.ID, record.Name, null);
				}
			}
		}
		this.m_data.productItemCatalog = this.m_itemCatalog.ToArray();
		this.m_data.productCatalog = this.m_productCatalog.ToArray();
	}

	// Token: 0x060060A8 RID: 24744 RVA: 0x001F8328 File Offset: 0x001F6528
	private bool AddItemsAndProductsFromStorePack(StorePackId storePackId)
	{
		ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(storePackId);
		int productDataCountFromStorePackId = GameUtils.GetProductDataCountFromStorePackId(storePackId);
		List<Network.Bundle> list = new List<Network.Bundle>();
		for (int i = 0; i < productDataCountFromStorePackId; i++)
		{
			List<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(productTypeFromStorePackType, false, GameUtils.GetProductDataFromStorePackId(storePackId, i), 0, false);
			list = list.Concat(allBundlesForProduct).ToList<Network.Bundle>();
		}
		int num = 0;
		foreach (Network.Bundle bundle in list)
		{
			string debugName = string.Format("(DEBUG) Type: {0}-{1}; productId:{2}", storePackId.Type, storePackId.Id, (bundle.PMTProductID != null) ? bundle.PMTProductID.Value : -1L);
			List<string> list2 = null;
			if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
			{
				list2 = new List<string>();
				list2.Add("bundle");
				if (bundle.IsPrePurchase)
				{
					list2.Add("prepurchase");
				}
			}
			if (this.AddItemsAndProductsFromNetBundle(bundle, debugName, list2))
			{
				num++;
			}
		}
		return num > 0;
	}

	// Token: 0x060060A9 RID: 24745 RVA: 0x001F845C File Offset: 0x001F665C
	private bool AddItemsAndProductsFromNetBundle(Network.Bundle netBundle, string debugName, List<string> overrideTags = null)
	{
		long productId = (netBundle.PMTProductID != null) ? netBundle.PMTProductID.Value : this.GetUniqueFakeId();
		if (this.m_productCatalog.Exists((ShopProductData.ProductData p) => p.productId == productId))
		{
			return false;
		}
		ShopProductData.ProductData item = default(ShopProductData.ProductData);
		item.name = StoreManager.Get().GetProductName(netBundle);
		item.description = debugName;
		List<string> tags = new List<string>();
		List<long> list = new List<long>();
		foreach (Network.BundleItem bundleItem in netBundle.Items)
		{
			ShopProductData.ProductItemData itemData = this.GenerateProductItemData(bundleItem);
			if (itemData.itemType != RewardItemType.UNDEFINED)
			{
				if (!this.m_itemCatalog.Exists((ShopProductData.ProductItemData i) => i.licenseId == itemData.licenseId))
				{
					this.m_itemCatalog.Add(itemData);
				}
				list.Add(itemData.licenseId);
				this.GetTags(itemData, ref tags);
			}
		}
		item.licenseIds = list.ToArray();
		if (overrideTags != null)
		{
			tags = overrideTags;
		}
		item.tags = this.SerializeTags(tags);
		item.productId = productId;
		this.AddPricesFromNetBundle(ref item, netBundle);
		this.m_productCatalog.Add(item);
		return true;
	}

	// Token: 0x060060AA RID: 24746 RVA: 0x001F85E0 File Offset: 0x001F67E0
	private ShopProductData.ProductItemData GenerateProductItemData(Network.BundleItem bundleItem)
	{
		ShopProductData.ProductItemData result = default(ShopProductData.ProductItemData);
		ProductType itemType = bundleItem.ItemType;
		if (itemType != ProductType.PRODUCT_TYPE_BOOSTER)
		{
			if (itemType != ProductType.PRODUCT_TYPE_CARD_BACK)
			{
				if (itemType != ProductType.PRODUCT_TYPE_HERO)
				{
					result.itemType = RewardItemType.UNDEFINED;
				}
				else
				{
					result.itemType = RewardItemType.HERO_SKIN;
				}
			}
			else
			{
				result.itemType = RewardItemType.CARD_BACK;
			}
		}
		else
		{
			result.itemType = RewardItemType.BOOSTER;
		}
		result.itemId = bundleItem.ProductData;
		result.quantity = bundleItem.Quantity;
		result.licenseId = this.GetUniqueFakeId();
		this.FillInDebugItemName(ref result);
		return result;
	}

	// Token: 0x060060AB RID: 24747 RVA: 0x001F8660 File Offset: 0x001F6860
	private void FillInDebugItemName(ref ShopProductData.ProductItemData itemData)
	{
		RewardItemType itemType = itemData.itemType;
		string arg;
		switch (itemType)
		{
		case RewardItemType.BOOSTER:
			arg = GameDbf.Booster.GetRecord(itemData.itemId).Name;
			goto IL_9E;
		case RewardItemType.DUST:
			goto IL_7D;
		case RewardItemType.HERO_SKIN:
			break;
		case RewardItemType.CARD_BACK:
			arg = GameDbf.CardBack.GetRecord(itemData.itemId).Name;
			goto IL_9E;
		default:
			if (itemType != RewardItemType.CARD)
			{
				goto IL_7D;
			}
			break;
		}
		arg = GameDbf.Card.GetRecord(itemData.itemId).Name;
		goto IL_9E;
		IL_7D:
		arg = string.Format("{0}-{1}", itemData.itemType, itemData.itemId);
		IL_9E:
		if (itemData.quantity == 1)
		{
			itemData.debugName = string.Format("{0} ({1})", arg, itemData.itemType);
			return;
		}
		itemData.debugName = string.Format("{0} x{1}", arg, itemData.quantity);
	}

	// Token: 0x060060AC RID: 24748 RVA: 0x001F8750 File Offset: 0x001F6950
	private void AddDummyItemAndProduct(RewardItemType itemType, int itemId, string debugName, string[] tagsOverride = null)
	{
		ShopProductData.ProductItemData productItemData = default(ShopProductData.ProductItemData);
		productItemData.itemType = itemType;
		productItemData.itemId = itemId;
		productItemData.debugName = "[PH] " + debugName;
		productItemData.licenseId = this.GetUniqueFakeId();
		productItemData.quantity = 1;
		this.m_itemCatalog.Add(productItemData);
		ShopProductData.ProductData item = default(ShopProductData.ProductData);
		item.name = productItemData.debugName;
		item.description = productItemData.ToString();
		item.licenseIds = new long[]
		{
			productItemData.licenseId
		};
		item.productId = this.GetUniqueFakeId();
		item.prices = new ShopProductData.PriceData[]
		{
			new ShopProductData.PriceData
			{
				currencyType = CurrencyType.GOLD,
				amount = 404.0
			}
		};
		List<string> tags = new List<string>();
		if (tagsOverride != null)
		{
			tags = tagsOverride.ToList<string>();
		}
		else
		{
			this.GetTags(productItemData, ref tags);
		}
		item.tags = this.SerializeTags(tags);
		this.m_productCatalog.Add(item);
	}

	// Token: 0x060060AD RID: 24749 RVA: 0x001F8860 File Offset: 0x001F6A60
	private void AddPricesFromNetBundle(ref ShopProductData.ProductData productData, Network.Bundle netBundle)
	{
		List<ShopProductData.PriceData> list = new List<ShopProductData.PriceData>();
		ShopProductData.PriceData item = default(ShopProductData.PriceData);
		if (netBundle.CostDisplay != null)
		{
			item.currencyType = CurrencyType.REAL_MONEY;
			item.amount = netBundle.CostDisplay.Value;
			list.Add(item);
		}
		if (netBundle.GtappGoldCost != null)
		{
			item.currencyType = CurrencyType.GOLD;
			item.amount = (double)netBundle.GtappGoldCost.Value;
			list.Add(item);
		}
		productData.prices = list.ToArray();
	}

	// Token: 0x060060AE RID: 24750 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void FlushData()
	{
	}

	// Token: 0x060060AF RID: 24751 RVA: 0x001F88F0 File Offset: 0x001F6AF0
	private string GetTags(ShopProductData.ProductItemData itemData)
	{
		List<string> tags = new List<string>();
		this.GetTags(itemData, ref tags);
		return this.SerializeTags(tags);
	}

	// Token: 0x060060B0 RID: 24752 RVA: 0x001F8914 File Offset: 0x001F6B14
	private void GetTags(ShopProductData.ProductItemData itemData, ref List<string> tags)
	{
		List<string> list = new List<string>();
		switch (itemData.itemType)
		{
		case RewardItemType.BOOSTER:
			list.Add("booster");
			break;
		case RewardItemType.HERO_SKIN:
			list.Add("skin");
			break;
		case RewardItemType.CARD_BACK:
			list.Add("cardback");
			break;
		}
		if (itemData.itemType == RewardItemType.BOOSTER)
		{
			int itemId = itemData.itemId;
			if (itemId <= 11)
			{
				if (itemId == 1)
				{
					list.Add("classic");
					goto IL_DE;
				}
				if (itemId != 9 && itemId != 11)
				{
					goto IL_DE;
				}
			}
			else if (itemId <= 21)
			{
				if (itemId == 17)
				{
					list.Add("welcome_bundle");
					list.Add("bad_prefab");
					goto IL_DE;
				}
				if (itemId - 19 > 2)
				{
					goto IL_DE;
				}
			}
			else if (itemId != 30)
			{
				if (itemId != 256)
				{
					goto IL_DE;
				}
				list.Add("theme_pack");
				list.Add("rogue_theme");
				goto IL_DE;
			}
			list.Add("wild");
		}
		IL_DE:
		foreach (string item in list)
		{
			if (!tags.Contains(item))
			{
				tags.Add(item);
			}
		}
	}

	// Token: 0x060060B1 RID: 24753 RVA: 0x001F8A50 File Offset: 0x001F6C50
	private string SerializeTags(List<string> tags)
	{
		return string.Join(",", tags.ToArray());
	}

	// Token: 0x060060B2 RID: 24754 RVA: 0x001F8A64 File Offset: 0x001F6C64
	private long GetUniqueFakeId()
	{
		long fakeLicenseId = this.m_fakeLicenseId;
		this.m_fakeLicenseId = fakeLicenseId + 1L;
		return fakeLicenseId;
	}

	// Token: 0x040050DF RID: 20703
	[SerializeField]
	private ShopProductData m_data;

	// Token: 0x040050E0 RID: 20704
	[SerializeField]
	private bool m_captureCurrentCatalog;

	// Token: 0x040050E1 RID: 20705
	[SerializeField]
	private bool m_generateTestData;

	// Token: 0x040050E2 RID: 20706
	[SerializeField]
	private bool m_flushData;

	// Token: 0x040050E3 RID: 20707
	private List<ShopProductData.ProductItemData> m_itemCatalog;

	// Token: 0x040050E4 RID: 20708
	private List<ShopProductData.ProductData> m_productCatalog;

	// Token: 0x040050E5 RID: 20709
	private long m_fakeLicenseId;
}
