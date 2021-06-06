using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x020006C0 RID: 1728
public static class ShopUtils
{
	// Token: 0x170005D8 RID: 1496
	// (get) Token: 0x060060E6 RID: 24806 RVA: 0x001F979C File Offset: 0x001F799C
	public static Dbf<BoosterDbfRecord> Boosters
	{
		get
		{
			if (GameDbf.Booster != null && Application.isPlaying)
			{
				return GameDbf.Booster;
			}
			Dbf<BoosterDbfRecord> result;
			if ((result = ShopUtils.s_boosters) == null)
			{
				result = (ShopUtils.s_boosters = Dbf<BoosterDbfRecord>.Load("BOOSTER", DbfFormat.XML));
			}
			return result;
		}
	}

	// Token: 0x170005D9 RID: 1497
	// (get) Token: 0x060060E7 RID: 24807 RVA: 0x001F97CC File Offset: 0x001F79CC
	public static Dbf<AdventureDbfRecord> Adventures
	{
		get
		{
			if (GameDbf.Adventure != null && Application.isPlaying)
			{
				return GameDbf.Adventure;
			}
			Dbf<AdventureDbfRecord> result;
			if ((result = ShopUtils.s_adventures) == null)
			{
				result = (ShopUtils.s_adventures = Dbf<AdventureDbfRecord>.Load("ADVENTURE", DbfFormat.XML));
			}
			return result;
		}
	}

	// Token: 0x060060E8 RID: 24808 RVA: 0x001F97FC File Offset: 0x001F79FC
	public static HashSet<string> ParseProductTags(string tagList)
	{
		HashSet<string> hashSet = new HashSet<string>();
		if (tagList != null)
		{
			foreach (string text in tagList.Split(new char[]
			{
				','
			}))
			{
				hashSet.Add(text.ToLower().Trim());
			}
		}
		return hashSet;
	}

	// Token: 0x060060E9 RID: 24809 RVA: 0x001F9849 File Offset: 0x001F7A49
	public static bool IsProductTagInSet(string tag, HashSet<string> tagSet)
	{
		return tag != null && tagSet.Contains(tag.ToLower());
	}

	// Token: 0x060060EA RID: 24810 RVA: 0x001F985C File Offset: 0x001F7A5C
	public static bool AreProductOrVariantsPurchasable(ProductDataModel product)
	{
		if (product.Availability != ProductAvailability.CAN_PURCHASE)
		{
			return product.Variants.Any((ProductDataModel p) => p.Availability == ProductAvailability.CAN_PURCHASE);
		}
		return true;
	}

	// Token: 0x060060EB RID: 24811 RVA: 0x001F9894 File Offset: 0x001F7A94
	public static AssetHandle<GameObject> LoadStorePackPrefab(BoosterDbId boosterId)
	{
		BoosterDbfRecord record = ShopUtils.Boosters.GetRecord((int)boosterId);
		if (record == null || string.IsNullOrEmpty(record.StorePrefab))
		{
			return null;
		}
		return AssetLoader.Get().LoadAsset<GameObject>(record.StorePrefab, AssetLoadingOptions.None);
	}

	// Token: 0x060060EC RID: 24812 RVA: 0x001F98D8 File Offset: 0x001F7AD8
	public static AssetHandle<GameObject> LoadStoreAdventurePrefab(AdventureDbId adventureId)
	{
		AdventureDbfRecord record = ShopUtils.Adventures.GetRecord((int)adventureId);
		if (record == null || string.IsNullOrEmpty(record.StorePrefab))
		{
			return null;
		}
		return AssetLoader.Get().LoadAsset<GameObject>(record.StorePrefab, AssetLoadingOptions.None);
	}

	// Token: 0x060060ED RID: 24813 RVA: 0x001F991C File Offset: 0x001F7B1C
	public static long GetCachedBalance(global::CurrencyType currencyType)
	{
		switch (currencyType)
		{
		case global::CurrencyType.GOLD:
			if (NetCache.Get() == null)
			{
				return 0L;
			}
			return NetCache.Get().GetGoldBalance();
		case global::CurrencyType.DUST:
			if (NetCache.Get() == null)
			{
				return 0L;
			}
			return NetCache.Get().GetArcaneDustBalance();
		case global::CurrencyType.RUNESTONES:
		case global::CurrencyType.ARCANE_ORBS:
		{
			IDataModel dataModel = null;
			GlobalDataContext.Get().GetDataModel(24, out dataModel);
			ShopDataModel shopDataModel = dataModel as ShopDataModel;
			if (shopDataModel == null)
			{
				return 0L;
			}
			PriceDataModel priceDataModel = (currencyType == global::CurrencyType.RUNESTONES) ? shopDataModel.VirtualCurrencyBalance : shopDataModel.BoosterCurrencyBalance;
			return (long)((priceDataModel != null) ? priceDataModel.Amount : 0f);
		}
		}
		Log.Store.PrintWarning("Unsupported currency type: {0}", new object[]
		{
			currencyType.ToString()
		});
		return 0L;
	}

	// Token: 0x060060EE RID: 24814 RVA: 0x001F99DC File Offset: 0x001F7BDC
	public static long GetDeficit(PriceDataModel price)
	{
		global::CurrencyType currency = price.Currency;
		if (currency == global::CurrencyType.REAL_MONEY)
		{
			return 0L;
		}
		long cachedBalance = ShopUtils.GetCachedBalance(price.Currency);
		long num = (long)price.Amount;
		if (num > cachedBalance)
		{
			return num - cachedBalance;
		}
		return 0L;
	}

	// Token: 0x060060EF RID: 24815 RVA: 0x001F9A18 File Offset: 0x001F7C18
	public static ProductDataModel FindCurrencyProduct(global::CurrencyType currencyType, float requiredAmount = 0f)
	{
		ProductCatalog catalog = StoreManager.Get().Catalog;
		ProductDataModel productDataModel = null;
		if (currencyType == global::CurrencyType.RUNESTONES)
		{
			productDataModel = catalog.VirtualCurrencyProductItem;
		}
		else if (currencyType == global::CurrencyType.ARCANE_ORBS)
		{
			productDataModel = catalog.BoosterCurrencyProductItem;
		}
		if (productDataModel == null)
		{
			Log.Store.PrintError("Couldn't find product for currency {0}", new object[]
			{
				currencyType.ToString()
			});
			return null;
		}
		ProductDataModel result = productDataModel;
		float num = float.MinValue;
		foreach (ProductDataModel productDataModel2 in productDataModel.Variants)
		{
			float num2 = ShopUtils.GetAmountOfCurrencyInProduct(productDataModel2, currencyType) - requiredAmount;
			if (num2 >= 0f && num < 0f)
			{
				num = num2;
				result = productDataModel2;
			}
			else if (Math.Abs(num2) < Math.Abs(num))
			{
				num = num2;
				result = productDataModel2;
			}
		}
		return result;
	}

	// Token: 0x060060F0 RID: 24816 RVA: 0x001F9AF8 File Offset: 0x001F7CF8
	public static float GetAmountOfCurrencyInProduct(ProductDataModel product, global::CurrencyType currencyType)
	{
		RewardItemDataModel rewardItemDataModel = product.Items.FirstOrDefault((RewardItemDataModel i) => i.Currency != null && i.Currency.Currency == currencyType);
		if (rewardItemDataModel != null)
		{
			return rewardItemDataModel.Currency.Amount;
		}
		return 0f;
	}

	// Token: 0x060060F1 RID: 24817 RVA: 0x001F9B3E File Offset: 0x001F7D3E
	public static bool IsCurrencyVirtual(global::CurrencyType currency)
	{
		return currency == global::CurrencyType.RUNESTONES || currency == global::CurrencyType.ARCANE_ORBS;
	}

	// Token: 0x060060F2 RID: 24818 RVA: 0x001F9B4C File Offset: 0x001F7D4C
	public static string GetCurrencyCode(global::CurrencyType currency)
	{
		switch (currency)
		{
		case global::CurrencyType.GOLD:
			return global::Currency.GTAPP.Code;
		case global::CurrencyType.RUNESTONES:
			return "XSA";
		case global::CurrencyType.REAL_MONEY:
			return StoreManager.Get().GetCurrencyCode();
		case global::CurrencyType.ARCANE_ORBS:
			return "XSB";
		}
		return "invalid";
	}

	// Token: 0x060060F3 RID: 24819 RVA: 0x001F9BA0 File Offset: 0x001F7DA0
	public static global::CurrencyType GetCurrencyTypeFromCode(string code)
	{
		if (code == null || (code != null && code.Length == 0))
		{
			return global::CurrencyType.NONE;
		}
		if (code == "XSA")
		{
			return global::CurrencyType.RUNESTONES;
		}
		if (code == "XSB")
		{
			return global::CurrencyType.ARCANE_ORBS;
		}
		if (!(code == "XSG"))
		{
			return global::CurrencyType.REAL_MONEY;
		}
		return global::CurrencyType.GOLD;
	}

	// Token: 0x060060F4 RID: 24820 RVA: 0x001F9BF0 File Offset: 0x001F7DF0
	public static bool IsVirtualCurrencyEnabled()
	{
		NetCache netCache = NetCache.Get();
		if (netCache == null)
		{
			return false;
		}
		NetCache.NetCacheFeatures netObject = netCache.GetNetObject<NetCache.NetCacheFeatures>();
		return netObject != null && netObject.Store.VirtualCurrencyEnabled;
	}

	// Token: 0x060060F5 RID: 24821 RVA: 0x001F9C20 File Offset: 0x001F7E20
	public static bool BundleHasPrice(Network.Bundle bundle, global::CurrencyType currencyType)
	{
		switch (currencyType)
		{
		case global::CurrencyType.GOLD:
			return bundle.GtappGoldCost != null;
		case global::CurrencyType.RUNESTONES:
			return bundle.VirtualCurrencyCode == "XSA";
		case global::CurrencyType.REAL_MONEY:
			return bundle.Cost != null;
		case global::CurrencyType.ARCANE_ORBS:
			return bundle.VirtualCurrencyCode == "XSB";
		}
		return false;
	}

	// Token: 0x060060F6 RID: 24822 RVA: 0x001F9C8C File Offset: 0x001F7E8C
	public static bool BundleHasNonGoldPrice(Network.Bundle bundle)
	{
		return bundle.Cost != null || bundle.VirtualCurrencyCode == "XSA" || bundle.VirtualCurrencyCode == "XSB";
	}

	// Token: 0x060060F7 RID: 24823 RVA: 0x001F9CD0 File Offset: 0x001F7ED0
	public static bool BundleHasNonRealMoneyPrice(Network.Bundle bundle)
	{
		return bundle.GtappGoldCost != null || bundle.VirtualCurrencyCode == "XSA" || bundle.VirtualCurrencyCode == "XSB";
	}

	// Token: 0x060060F8 RID: 24824 RVA: 0x001F9D14 File Offset: 0x001F7F14
	public static global::CurrencyType GetBundleVirtualCurrencyPriceType(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return global::CurrencyType.NONE;
		}
		global::CurrencyType currencyTypeFromCode = ShopUtils.GetCurrencyTypeFromCode(bundle.VirtualCurrencyCode);
		if (ShopUtils.IsCurrencyVirtual(currencyTypeFromCode))
		{
			return currencyTypeFromCode;
		}
		return global::CurrencyType.NONE;
	}

	// Token: 0x060060F9 RID: 24825 RVA: 0x001F9D40 File Offset: 0x001F7F40
	public static bool TryGetBundlePrice(Network.Bundle bundle, global::CurrencyType currencyType, out long amount)
	{
		amount = 0L;
		if (bundle == null)
		{
			return false;
		}
		switch (currencyType)
		{
		case global::CurrencyType.GOLD:
			if (bundle.GtappGoldCost == null)
			{
				return false;
			}
			amount = bundle.GtappGoldCost.Value;
			return true;
		case global::CurrencyType.RUNESTONES:
			if (bundle.VirtualCurrencyCode != "XSA")
			{
				return false;
			}
			amount = bundle.VirtualCurrencyCost.GetValueOrDefault();
			return true;
		case global::CurrencyType.REAL_MONEY:
			if (bundle.Cost == null)
			{
				return false;
			}
			amount = (long)bundle.Cost.Value;
			return true;
		case global::CurrencyType.ARCANE_ORBS:
			if (bundle.VirtualCurrencyCode != "XSB")
			{
				return false;
			}
			amount = bundle.VirtualCurrencyCost.GetValueOrDefault();
			return true;
		}
		return false;
	}

	// Token: 0x060060FA RID: 24826 RVA: 0x001F9E0C File Offset: 0x001F800C
	public static bool TryGetPriceFromBundleOrTransaction(Network.Bundle bundle, NoGTAPPTransactionData transaction, global::CurrencyType currencyType, out long price)
	{
		if (transaction != null && currencyType == global::CurrencyType.GOLD)
		{
			return StoreManager.Get().GetGoldCostNoGTAPP(transaction, out price);
		}
		return ShopUtils.TryGetBundlePrice(bundle, currencyType, out price);
	}

	// Token: 0x060060FB RID: 24827 RVA: 0x001F9E2C File Offset: 0x001F802C
	public static bool TryDecomposeBuyProductEventArgs(BuyProductEventArgs args, out long pmtProductId, out string currencyCode, out long totalPrice, out int quantity, out string productItemType, out int productItemId)
	{
		pmtProductId = 0L;
		currencyCode = null;
		totalPrice = 0L;
		quantity = 0;
		productItemType = "";
		productItemId = 0;
		Network.Bundle bundle = null;
		if (args == null)
		{
			return false;
		}
		quantity = args.quantity;
		if (args is BuyPmtProductEventArgs)
		{
			BuyPmtProductEventArgs buyPmtProductEventArgs = (BuyPmtProductEventArgs)args;
			pmtProductId = buyPmtProductEventArgs.pmtProductId;
			currencyCode = ShopUtils.GetCurrencyCode(buyPmtProductEventArgs.paymentCurrency);
			bundle = StoreManager.Get().GetBundleFromPmtProductId(pmtProductId);
		}
		else
		{
			if (!(args is BuyNoGTAPPEventArgs))
			{
				return false;
			}
			BuyNoGTAPPEventArgs buyNoGTAPPEventArgs = (BuyNoGTAPPEventArgs)args;
			productItemType = buyNoGTAPPEventArgs.transactionData.Product.ToString().ToLowerInvariant();
			productItemId = buyNoGTAPPEventArgs.transactionData.ProductData;
			currencyCode = ShopUtils.GetCurrencyCode(global::CurrencyType.GOLD);
			StoreManager.Get().GetGoldCostNoGTAPP(buyNoGTAPPEventArgs.transactionData, out totalPrice);
		}
		if (bundle != null)
		{
			pmtProductId = bundle.PMTProductID.GetValueOrDefault();
			global::CurrencyType currencyTypeFromCode = ShopUtils.GetCurrencyTypeFromCode(currencyCode);
			ShopUtils.TryGetBundlePrice(bundle, currencyTypeFromCode, out totalPrice);
			if (bundle.Items.Count == 1)
			{
				productItemType = bundle.Items[0].ItemType.ToString().ToLowerInvariant();
				productItemId = bundle.Items[0].ProductData;
			}
			totalPrice *= (long)quantity;
		}
		return true;
	}

	// Token: 0x060060FC RID: 24828 RVA: 0x001F9F6D File Offset: 0x001F816D
	public static global::CurrencyType GetCurrencyTypeFromProto(PegasusShared.CurrencyType protoCurrencyType)
	{
		switch (protoCurrencyType)
		{
		case PegasusShared.CurrencyType.CURRENCY_TYPE_GOLD:
			return global::CurrencyType.GOLD;
		case PegasusShared.CurrencyType.CURRENCY_TYPE_DUST:
			return global::CurrencyType.DUST;
		case PegasusShared.CurrencyType.CURRENCY_TYPE_RUNESTONES:
			return global::CurrencyType.RUNESTONES;
		case PegasusShared.CurrencyType.CURRENCY_TYPE_ARCANE_ORBS:
			return global::CurrencyType.ARCANE_ORBS;
		default:
			return global::CurrencyType.NONE;
		}
	}

	// Token: 0x040050F5 RID: 20725
	private static Dbf<BoosterDbfRecord> s_boosters;

	// Token: 0x040050F6 RID: 20726
	private static Dbf<AdventureDbfRecord> s_adventures;
}
