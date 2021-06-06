using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using PegasusUtil;
using UnityEngine;

public class ShopProductDataConverter : MonoBehaviour
{
	[SerializeField]
	private ShopProductData m_data;

	[SerializeField]
	private bool m_captureCurrentCatalog;

	[SerializeField]
	private bool m_generateTestData;

	[SerializeField]
	private bool m_flushData;

	private List<ShopProductData.ProductItemData> m_itemCatalog;

	private List<ShopProductData.ProductData> m_productCatalog;

	private long m_fakeLicenseId;

	private void Update()
	{
		if (m_captureCurrentCatalog && StoreManager.Get().Catalog.HasData)
		{
			m_captureCurrentCatalog = false;
			SnapshotCurrentCatalog();
		}
		if (m_generateTestData)
		{
			m_generateTestData = false;
			BuildTestData();
		}
		if (m_flushData)
		{
			m_flushData = false;
			FlushData();
		}
	}

	private void SnapshotCurrentCatalog()
	{
		ProductCatalog catalog = StoreManager.Get().Catalog;
		m_itemCatalog = new List<ShopProductData.ProductItemData>();
		m_productCatalog = new List<ShopProductData.ProductData>();
		m_fakeLicenseId = 404000L;
		List<ShopProductData.ProductTierData> list = new List<ShopProductData.ProductTierData>();
		foreach (ProductTierDataModel tier in catalog.Tiers)
		{
			List<long> list2 = new List<long>();
			foreach (ShopBrowserButtonDataModel browserButton in tier.BrowserButtons)
			{
				list2.Add(browserButton.DisplayProduct.PmtId);
			}
			ShopProductData.ProductTierData productTierData = default(ShopProductData.ProductTierData);
			productTierData.tierId = tier.Style;
			productTierData.tags = string.Join(",", tier.Tags.ToArray());
			productTierData.header = tier.Header;
			productTierData.productIds = list2.ToArray();
			ShopProductData.ProductTierData item = productTierData;
			list.Add(item);
		}
		foreach (ProductDataModel product in catalog.Products)
		{
			ShopProductData.ProductData productData = default(ShopProductData.ProductData);
			productData.name = product.Name;
			productData.description = product.Description;
			productData.productId = product.PmtId;
			productData.tags = string.Join(",", product.Tags.ToArray());
			ShopProductData.ProductData item2 = productData;
			List<ShopProductData.PriceData> list3 = new List<ShopProductData.PriceData>();
			foreach (PriceDataModel price in product.Prices)
			{
				ShopProductData.PriceData priceData = default(ShopProductData.PriceData);
				priceData.amount = price.Amount;
				priceData.currencyType = price.Currency;
				ShopProductData.PriceData item3 = priceData;
				list3.Add(item3);
			}
			item2.prices = list3.ToArray();
			List<long> list4 = new List<long>();
			foreach (RewardItemDataModel item4 in product.Items)
			{
				ShopProductData.ProductItemData productItemData = default(ShopProductData.ProductItemData);
				productItemData.itemId = item4.ItemId;
				productItemData.itemType = item4.ItemType;
				productItemData.licenseId = ((item4.PmtLicenseId == 0L) ? GetUniqueFakeId() : item4.PmtLicenseId);
				productItemData.quantity = item4.Quantity;
				ShopProductData.ProductItemData itemData = productItemData;
				FillInDebugItemName(ref itemData);
				m_itemCatalog.Add(itemData);
				list4.Add(itemData.licenseId);
			}
			item2.licenseIds = list4.ToArray();
			m_productCatalog.Add(item2);
		}
		m_data.productTierCatalog = list.ToArray();
		m_data.productCatalog = m_productCatalog.ToArray();
		m_data.productItemCatalog = m_itemCatalog.ToArray();
	}

	private void BuildTestData()
	{
		m_itemCatalog = new List<ShopProductData.ProductItemData>();
		m_productCatalog = new List<ShopProductData.ProductData>();
		m_fakeLicenseId = 404000L;
		foreach (ModularBundleDbfRecord record2 in GameDbf.ModularBundle.GetRecords())
		{
			if (!((string)record2.Name == ""))
			{
				StorePackId storePackId = default(StorePackId);
				storePackId.Type = StorePackType.MODULAR_BUNDLE;
				storePackId.Id = record2.ID;
				if (!AddItemsAndProductsFromStorePack(storePackId))
				{
					Log.Store.PrintWarning("Could not add test data from Network.Bundles for bundle '{0}' (storePackId: {1})", record2.Name, storePackId);
					string[] tagsOverride = new string[1] { "bundle" };
					AddDummyItemAndProduct(RewardItemType.BOOSTER, 1, record2.Name, tagsOverride);
				}
			}
		}
		foreach (BoosterDbfRecord record3 in GameDbf.Booster.GetRecords())
		{
			if (!((string)record3.Name == "") && record3.StorePrefab != null)
			{
				StorePackId storePackId2 = default(StorePackId);
				storePackId2.Type = StorePackType.BOOSTER;
				storePackId2.Id = record3.ID;
				if (!AddItemsAndProductsFromStorePack(storePackId2))
				{
					Log.Store.PrintWarning("Could not add test data from Network.Bundles for booster '{0}' (storePackId: {1})", record3.Name, storePackId2);
					AddDummyItemAndProduct(RewardItemType.BOOSTER, record3.ID, record3.Name);
				}
			}
		}
		int num = 10;
		foreach (CardBackDbfRecord record4 in GameDbf.CardBack.GetRecords())
		{
			if (!((string)record4.Name == ""))
			{
				AddDummyItemAndProduct(RewardItemType.CARD_BACK, record4.ID, record4.Name);
				if (--num <= 0)
				{
					break;
				}
			}
		}
		foreach (CardHeroDbfRecord record5 in GameDbf.CardHero.GetRecords())
		{
			CardDbfRecord record = GameDbf.Card.GetRecord(record5.CardId);
			if (record != null && !((string)record.Name == ""))
			{
				Network.Bundle heroBundle = null;
				StoreManager.Get().GetHeroBundleByCardDbId(record5.CardId, out heroBundle);
				if (heroBundle == null || !AddItemsAndProductsFromNetBundle(heroBundle, record.Name))
				{
					Log.Store.PrintWarning("Could not add test data from Network.Bundles for card '{0}' (hero CardId: {1})", record.Name, record5.CardId);
					AddDummyItemAndProduct(RewardItemType.HERO_SKIN, record.ID, record.Name);
				}
			}
		}
		m_data.productItemCatalog = m_itemCatalog.ToArray();
		m_data.productCatalog = m_productCatalog.ToArray();
	}

	private bool AddItemsAndProductsFromStorePack(StorePackId storePackId)
	{
		ProductType productTypeFromStorePackType = StorePackId.GetProductTypeFromStorePackType(storePackId);
		int productDataCountFromStorePackId = GameUtils.GetProductDataCountFromStorePackId(storePackId);
		List<Network.Bundle> list = new List<Network.Bundle>();
		for (int i = 0; i < productDataCountFromStorePackId; i++)
		{
			List<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(productTypeFromStorePackType, requireRealMoneyOption: false, GameUtils.GetProductDataFromStorePackId(storePackId, i), 0, checkAvailability: false);
			list = list.Concat(allBundlesForProduct).ToList();
		}
		int num = 0;
		foreach (Network.Bundle item in list)
		{
			string debugName = $"(DEBUG) Type: {storePackId.Type}-{storePackId.Id}; productId:{(item.PMTProductID.HasValue ? item.PMTProductID.Value : (-1))}";
			List<string> list2 = null;
			if (storePackId.Type == StorePackType.MODULAR_BUNDLE)
			{
				list2 = new List<string>();
				list2.Add("bundle");
				if (item.IsPrePurchase)
				{
					list2.Add("prepurchase");
				}
			}
			if (AddItemsAndProductsFromNetBundle(item, debugName, list2))
			{
				num++;
			}
		}
		return num > 0;
	}

	private bool AddItemsAndProductsFromNetBundle(Network.Bundle netBundle, string debugName, List<string> overrideTags = null)
	{
		long productId = (netBundle.PMTProductID.HasValue ? netBundle.PMTProductID.Value : GetUniqueFakeId());
		if (m_productCatalog.Exists((ShopProductData.ProductData p) => p.productId == productId))
		{
			return false;
		}
		ShopProductData.ProductData productData = default(ShopProductData.ProductData);
		productData.name = StoreManager.Get().GetProductName(netBundle);
		productData.description = debugName;
		List<string> tags = new List<string>();
		List<long> list = new List<long>();
		foreach (Network.BundleItem item in netBundle.Items)
		{
			ShopProductData.ProductItemData itemData = GenerateProductItemData(item);
			if (itemData.itemType != 0)
			{
				if (!m_itemCatalog.Exists((ShopProductData.ProductItemData i) => i.licenseId == itemData.licenseId))
				{
					m_itemCatalog.Add(itemData);
				}
				list.Add(itemData.licenseId);
				GetTags(itemData, ref tags);
			}
		}
		productData.licenseIds = list.ToArray();
		if (overrideTags != null)
		{
			tags = overrideTags;
		}
		productData.tags = SerializeTags(tags);
		productData.productId = productId;
		AddPricesFromNetBundle(ref productData, netBundle);
		m_productCatalog.Add(productData);
		return true;
	}

	private ShopProductData.ProductItemData GenerateProductItemData(Network.BundleItem bundleItem)
	{
		ShopProductData.ProductItemData itemData = default(ShopProductData.ProductItemData);
		switch (bundleItem.ItemType)
		{
		case ProductType.PRODUCT_TYPE_BOOSTER:
			itemData.itemType = RewardItemType.BOOSTER;
			break;
		case ProductType.PRODUCT_TYPE_CARD_BACK:
			itemData.itemType = RewardItemType.CARD_BACK;
			break;
		case ProductType.PRODUCT_TYPE_HERO:
			itemData.itemType = RewardItemType.HERO_SKIN;
			break;
		default:
			itemData.itemType = RewardItemType.UNDEFINED;
			break;
		}
		itemData.itemId = bundleItem.ProductData;
		itemData.quantity = bundleItem.Quantity;
		itemData.licenseId = GetUniqueFakeId();
		FillInDebugItemName(ref itemData);
		return itemData;
	}

	private void FillInDebugItemName(ref ShopProductData.ProductItemData itemData)
	{
		string arg;
		switch (itemData.itemType)
		{
		case RewardItemType.BOOSTER:
			arg = GameDbf.Booster.GetRecord(itemData.itemId).Name;
			break;
		case RewardItemType.CARD_BACK:
			arg = GameDbf.CardBack.GetRecord(itemData.itemId).Name;
			break;
		case RewardItemType.HERO_SKIN:
		case RewardItemType.CARD:
			arg = GameDbf.Card.GetRecord(itemData.itemId).Name;
			break;
		default:
			arg = $"{itemData.itemType}-{itemData.itemId}";
			break;
		}
		if (itemData.quantity == 1)
		{
			itemData.debugName = $"{arg} ({itemData.itemType})";
		}
		else
		{
			itemData.debugName = $"{arg} x{itemData.quantity}";
		}
	}

	private void AddDummyItemAndProduct(RewardItemType itemType, int itemId, string debugName, string[] tagsOverride = null)
	{
		ShopProductData.ProductItemData productItemData = default(ShopProductData.ProductItemData);
		productItemData.itemType = itemType;
		productItemData.itemId = itemId;
		productItemData.debugName = "[PH] " + debugName;
		productItemData.licenseId = GetUniqueFakeId();
		productItemData.quantity = 1;
		m_itemCatalog.Add(productItemData);
		ShopProductData.ProductData item = default(ShopProductData.ProductData);
		item.name = productItemData.debugName;
		item.description = productItemData.ToString();
		item.licenseIds = new long[1] { productItemData.licenseId };
		item.productId = GetUniqueFakeId();
		ShopProductData.PriceData priceData = default(ShopProductData.PriceData);
		priceData.currencyType = CurrencyType.GOLD;
		priceData.amount = 404.0;
		item.prices = new ShopProductData.PriceData[1] { priceData };
		List<string> tags = new List<string>();
		if (tagsOverride != null)
		{
			tags = tagsOverride.ToList();
		}
		else
		{
			GetTags(productItemData, ref tags);
		}
		item.tags = SerializeTags(tags);
		m_productCatalog.Add(item);
	}

	private void AddPricesFromNetBundle(ref ShopProductData.ProductData productData, Network.Bundle netBundle)
	{
		List<ShopProductData.PriceData> list = new List<ShopProductData.PriceData>();
		ShopProductData.PriceData item = default(ShopProductData.PriceData);
		if (netBundle.CostDisplay.HasValue)
		{
			item.currencyType = CurrencyType.REAL_MONEY;
			item.amount = netBundle.CostDisplay.Value;
			list.Add(item);
		}
		if (netBundle.GtappGoldCost.HasValue)
		{
			item.currencyType = CurrencyType.GOLD;
			item.amount = netBundle.GtappGoldCost.Value;
			list.Add(item);
		}
		productData.prices = list.ToArray();
	}

	private void FlushData()
	{
	}

	private string GetTags(ShopProductData.ProductItemData itemData)
	{
		List<string> tags = new List<string>();
		GetTags(itemData, ref tags);
		return SerializeTags(tags);
	}

	private void GetTags(ShopProductData.ProductItemData itemData, ref List<string> tags)
	{
		List<string> list = new List<string>();
		switch (itemData.itemType)
		{
		case RewardItemType.BOOSTER:
			list.Add("booster");
			break;
		case RewardItemType.CARD_BACK:
			list.Add("cardback");
			break;
		case RewardItemType.HERO_SKIN:
			list.Add("skin");
			break;
		}
		if (itemData.itemType == RewardItemType.BOOSTER)
		{
			switch (itemData.itemId)
			{
			case 1:
				list.Add("classic");
				break;
			case 9:
			case 11:
			case 19:
			case 20:
			case 21:
			case 30:
				list.Add("wild");
				break;
			case 17:
				list.Add("welcome_bundle");
				list.Add("bad_prefab");
				break;
			case 256:
				list.Add("theme_pack");
				list.Add("rogue_theme");
				break;
			}
		}
		foreach (string item in list)
		{
			if (!tags.Contains(item))
			{
				tags.Add(item);
			}
		}
	}

	private string SerializeTags(List<string> tags)
	{
		return string.Join(",", tags.ToArray());
	}

	private long GetUniqueFakeId()
	{
		return m_fakeLicenseId++;
	}
}
