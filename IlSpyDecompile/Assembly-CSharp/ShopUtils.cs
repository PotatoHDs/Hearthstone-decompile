using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

public static class ShopUtils
{
	private static Dbf<BoosterDbfRecord> s_boosters;

	private static Dbf<AdventureDbfRecord> s_adventures;

	public static Dbf<BoosterDbfRecord> Boosters
	{
		get
		{
			if (GameDbf.Booster != null && Application.isPlaying)
			{
				return GameDbf.Booster;
			}
			return s_boosters ?? (s_boosters = Dbf<BoosterDbfRecord>.Load("BOOSTER", DbfFormat.XML));
		}
	}

	public static Dbf<AdventureDbfRecord> Adventures
	{
		get
		{
			if (GameDbf.Adventure != null && Application.isPlaying)
			{
				return GameDbf.Adventure;
			}
			return s_adventures ?? (s_adventures = Dbf<AdventureDbfRecord>.Load("ADVENTURE", DbfFormat.XML));
		}
	}

	public static HashSet<string> ParseProductTags(string tagList)
	{
		HashSet<string> hashSet = new HashSet<string>();
		if (tagList != null)
		{
			string[] array = tagList.Split(',');
			foreach (string text in array)
			{
				hashSet.Add(text.ToLower().Trim());
			}
		}
		return hashSet;
	}

	public static bool IsProductTagInSet(string tag, HashSet<string> tagSet)
	{
		if (tag != null)
		{
			return tagSet.Contains(tag.ToLower());
		}
		return false;
	}

	public static bool AreProductOrVariantsPurchasable(ProductDataModel product)
	{
		if (product.Availability != ProductAvailability.CAN_PURCHASE)
		{
			return product.Variants.Any((ProductDataModel p) => p.Availability == ProductAvailability.CAN_PURCHASE);
		}
		return true;
	}

	public static AssetHandle<GameObject> LoadStorePackPrefab(BoosterDbId boosterId)
	{
		BoosterDbfRecord record = Boosters.GetRecord((int)boosterId);
		if (record == null || string.IsNullOrEmpty(record.StorePrefab))
		{
			return null;
		}
		return AssetLoader.Get().LoadAsset<GameObject>(record.StorePrefab);
	}

	public static AssetHandle<GameObject> LoadStoreAdventurePrefab(AdventureDbId adventureId)
	{
		AdventureDbfRecord record = Adventures.GetRecord((int)adventureId);
		if (record == null || string.IsNullOrEmpty(record.StorePrefab))
		{
			return null;
		}
		return AssetLoader.Get().LoadAsset<GameObject>(record.StorePrefab);
	}

	public static long GetCachedBalance(CurrencyType currencyType)
	{
		switch (currencyType)
		{
		case CurrencyType.DUST:
			if (NetCache.Get() == null)
			{
				return 0L;
			}
			return NetCache.Get().GetArcaneDustBalance();
		case CurrencyType.GOLD:
			if (NetCache.Get() == null)
			{
				return 0L;
			}
			return NetCache.Get().GetGoldBalance();
		case CurrencyType.RUNESTONES:
		case CurrencyType.ARCANE_ORBS:
		{
			IDataModel model = null;
			GlobalDataContext.Get().GetDataModel(24, out model);
			ShopDataModel shopDataModel = model as ShopDataModel;
			if (shopDataModel == null)
			{
				return 0L;
			}
			return (long)(((currencyType == CurrencyType.RUNESTONES) ? shopDataModel.VirtualCurrencyBalance : shopDataModel.BoosterCurrencyBalance)?.Amount ?? 0f);
		}
		default:
			Log.Store.PrintWarning("Unsupported currency type: {0}", currencyType.ToString());
			return 0L;
		}
	}

	public static long GetDeficit(PriceDataModel price)
	{
		long num = 0L;
		CurrencyType currency = price.Currency;
		if (currency == CurrencyType.REAL_MONEY)
		{
			return 0L;
		}
		num = GetCachedBalance(price.Currency);
		long num2 = (long)price.Amount;
		if (num2 > num)
		{
			return num2 - num;
		}
		return 0L;
	}

	public static ProductDataModel FindCurrencyProduct(CurrencyType currencyType, float requiredAmount = 0f)
	{
		ProductCatalog catalog = StoreManager.Get().Catalog;
		ProductDataModel productDataModel = null;
		switch (currencyType)
		{
		case CurrencyType.RUNESTONES:
			productDataModel = catalog.VirtualCurrencyProductItem;
			break;
		case CurrencyType.ARCANE_ORBS:
			productDataModel = catalog.BoosterCurrencyProductItem;
			break;
		}
		if (productDataModel == null)
		{
			Log.Store.PrintError("Couldn't find product for currency {0}", currencyType.ToString());
			return null;
		}
		ProductDataModel result = productDataModel;
		float num = float.MinValue;
		foreach (ProductDataModel variant in productDataModel.Variants)
		{
			float num2 = GetAmountOfCurrencyInProduct(variant, currencyType) - requiredAmount;
			if (num2 >= 0f && num < 0f)
			{
				num = num2;
				result = variant;
			}
			else if (Math.Abs(num2) < Math.Abs(num))
			{
				num = num2;
				result = variant;
			}
		}
		return result;
	}

	public static float GetAmountOfCurrencyInProduct(ProductDataModel product, CurrencyType currencyType)
	{
		return product.Items.FirstOrDefault((RewardItemDataModel i) => i.Currency != null && i.Currency.Currency == currencyType)?.Currency.Amount ?? 0f;
	}

	public static bool IsCurrencyVirtual(CurrencyType currency)
	{
		if (currency == CurrencyType.RUNESTONES || currency == CurrencyType.ARCANE_ORBS)
		{
			return true;
		}
		return false;
	}

	public static string GetCurrencyCode(CurrencyType currency)
	{
		return currency switch
		{
			CurrencyType.RUNESTONES => "XSA", 
			CurrencyType.ARCANE_ORBS => "XSB", 
			CurrencyType.GOLD => Currency.GTAPP.Code, 
			CurrencyType.REAL_MONEY => StoreManager.Get().GetCurrencyCode(), 
			_ => "invalid", 
		};
	}

	public static CurrencyType GetCurrencyTypeFromCode(string code)
	{
		if (code != null && (code == null || code.Length != 0))
		{
			return code switch
			{
				"XSA" => CurrencyType.RUNESTONES, 
				"XSB" => CurrencyType.ARCANE_ORBS, 
				"XSG" => CurrencyType.GOLD, 
				_ => CurrencyType.REAL_MONEY, 
			};
		}
		return CurrencyType.NONE;
	}

	public static bool IsVirtualCurrencyEnabled()
	{
		NetCache netCache = NetCache.Get();
		if (netCache == null)
		{
			return false;
		}
		return netCache.GetNetObject<NetCache.NetCacheFeatures>()?.Store.VirtualCurrencyEnabled ?? false;
	}

	public static bool BundleHasPrice(Network.Bundle bundle, CurrencyType currencyType)
	{
		return currencyType switch
		{
			CurrencyType.REAL_MONEY => bundle.Cost.HasValue, 
			CurrencyType.GOLD => bundle.GtappGoldCost.HasValue, 
			CurrencyType.RUNESTONES => bundle.VirtualCurrencyCode == "XSA", 
			CurrencyType.ARCANE_ORBS => bundle.VirtualCurrencyCode == "XSB", 
			_ => false, 
		};
	}

	public static bool BundleHasNonGoldPrice(Network.Bundle bundle)
	{
		if (!bundle.Cost.HasValue && !(bundle.VirtualCurrencyCode == "XSA"))
		{
			return bundle.VirtualCurrencyCode == "XSB";
		}
		return true;
	}

	public static bool BundleHasNonRealMoneyPrice(Network.Bundle bundle)
	{
		if (!bundle.GtappGoldCost.HasValue && !(bundle.VirtualCurrencyCode == "XSA"))
		{
			return bundle.VirtualCurrencyCode == "XSB";
		}
		return true;
	}

	public static CurrencyType GetBundleVirtualCurrencyPriceType(Network.Bundle bundle)
	{
		if (bundle == null)
		{
			return CurrencyType.NONE;
		}
		CurrencyType currencyTypeFromCode = GetCurrencyTypeFromCode(bundle.VirtualCurrencyCode);
		if (IsCurrencyVirtual(currencyTypeFromCode))
		{
			return currencyTypeFromCode;
		}
		return CurrencyType.NONE;
	}

	public static bool TryGetBundlePrice(Network.Bundle bundle, CurrencyType currencyType, out long amount)
	{
		amount = 0L;
		if (bundle == null)
		{
			return false;
		}
		switch (currencyType)
		{
		case CurrencyType.REAL_MONEY:
			if (!bundle.Cost.HasValue)
			{
				return false;
			}
			amount = (long)bundle.Cost.Value;
			return true;
		case CurrencyType.GOLD:
			if (!bundle.GtappGoldCost.HasValue)
			{
				return false;
			}
			amount = bundle.GtappGoldCost.Value;
			return true;
		case CurrencyType.RUNESTONES:
			if (bundle.VirtualCurrencyCode != "XSA")
			{
				return false;
			}
			amount = bundle.VirtualCurrencyCost.GetValueOrDefault();
			return true;
		case CurrencyType.ARCANE_ORBS:
			if (bundle.VirtualCurrencyCode != "XSB")
			{
				return false;
			}
			amount = bundle.VirtualCurrencyCost.GetValueOrDefault();
			return true;
		default:
			return false;
		}
	}

	public static bool TryGetPriceFromBundleOrTransaction(Network.Bundle bundle, NoGTAPPTransactionData transaction, CurrencyType currencyType, out long price)
	{
		if (transaction != null && currencyType == CurrencyType.GOLD)
		{
			return StoreManager.Get().GetGoldCostNoGTAPP(transaction, out price);
		}
		return TryGetBundlePrice(bundle, currencyType, out price);
	}

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
			currencyCode = GetCurrencyCode(buyPmtProductEventArgs.paymentCurrency);
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
			currencyCode = GetCurrencyCode(CurrencyType.GOLD);
			StoreManager.Get().GetGoldCostNoGTAPP(buyNoGTAPPEventArgs.transactionData, out totalPrice);
		}
		if (bundle != null)
		{
			pmtProductId = bundle.PMTProductID.GetValueOrDefault();
			CurrencyType currencyTypeFromCode = GetCurrencyTypeFromCode(currencyCode);
			TryGetBundlePrice(bundle, currencyTypeFromCode, out totalPrice);
			if (bundle.Items.Count == 1)
			{
				productItemType = bundle.Items[0].ItemType.ToString().ToLowerInvariant();
				productItemId = bundle.Items[0].ProductData;
			}
			totalPrice *= quantity;
		}
		return true;
	}

	public static CurrencyType GetCurrencyTypeFromProto(PegasusShared.CurrencyType protoCurrencyType)
	{
		return protoCurrencyType switch
		{
			PegasusShared.CurrencyType.CURRENCY_TYPE_DUST => CurrencyType.DUST, 
			PegasusShared.CurrencyType.CURRENCY_TYPE_RUNESTONES => CurrencyType.RUNESTONES, 
			PegasusShared.CurrencyType.CURRENCY_TYPE_ARCANE_ORBS => CurrencyType.ARCANE_ORBS, 
			PegasusShared.CurrencyType.CURRENCY_TYPE_GOLD => CurrencyType.GOLD, 
			_ => CurrencyType.NONE, 
		};
	}
}
