using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using PegasusUtil;

public static class CatalogUtils
{
	public static IEnumerable<string> ParseTagsString(string tagsString)
	{
		if (string.IsNullOrEmpty(tagsString))
		{
			return new string[0];
		}
		return new HashSet<string>(from tagString in tagsString.Split(',')
			select tagString.Trim().ToLowerInvariant());
	}

	public static bool CanUpdateProductStatus(out string reason)
	{
		if (!StoreManager.Get().AllSections.Any())
		{
			reason = "Cannot update product status before populating sections";
			return false;
		}
		if (SpecialEventManager.Get() == null || !SpecialEventManager.Get().HasReceivedEventTimingsFromServer)
		{
			reason = "Cannot update product status before HasReceivedEventTimingsFromServer";
			return false;
		}
		if (NetCache.Get().GetNetObject<NetCache.NetCacheCardBacks>() == null)
		{
			reason = "Cannot update product status before NetCacheCardBacks received";
			return false;
		}
		if (NetCache.Get().GetNetObject<NetCache.NetCacheCollection>() == null)
		{
			reason = "Cannot update product status before NetCacheCollection received";
			return false;
		}
		if (CollectionManager.Get() == null)
		{
			reason = "Cannot update product status before CollectionManager initialized";
			return false;
		}
		if (FixedRewardsMgr.Get() == null || !FixedRewardsMgr.Get().IsStartupFinished())
		{
			reason = "Cannot update product status before FixedRewardsMgr initialized";
			return false;
		}
		AccountLicenseMgr.LicenseUpdateState licenseUpdateState = ((AccountLicenseMgr.Get() != null) ? AccountLicenseMgr.Get().FixedLicensesState : AccountLicenseMgr.LicenseUpdateState.UNKNOWN);
		if (licenseUpdateState != AccountLicenseMgr.LicenseUpdateState.SUCCESS)
		{
			reason = $"Cannot update product status when AccountLicenseMgr FixedLicensesState is {licenseUpdateState}.";
			return false;
		}
		reason = null;
		return true;
	}

	public static int CompareProductsForSortByQuantity(ProductDataModel xProduct, ProductDataModel yProduct)
	{
		int num = ((xProduct.Items.Count > 0) ? xProduct.Items[0].Quantity : 0);
		int num2 = ((yProduct.Items.Count > 0) ? yProduct.Items[0].Quantity : 0);
		return num - num2;
	}

	public static int CompareProductsByItemSortOrder(ProductDataModel xProduct, ProductDataModel yProduct)
	{
		int sortOrderFromItems = RewardUtils.GetSortOrderFromItems(xProduct.Items);
		int sortOrderFromItems2 = RewardUtils.GetSortOrderFromItems(yProduct.Items);
		return sortOrderFromItems - sortOrderFromItems2;
	}

	public static int ComparePricesForSort(PriceDataModel xPrice, PriceDataModel yPrice)
	{
		if (xPrice == null && yPrice == null)
		{
			return 0;
		}
		if (xPrice == null)
		{
			return 1;
		}
		if (yPrice == null)
		{
			return -1;
		}
		if (xPrice.Currency < yPrice.Currency)
		{
			return -1;
		}
		if (xPrice.Currency > yPrice.Currency)
		{
			return 1;
		}
		if (xPrice.Amount < yPrice.Amount)
		{
			return -1;
		}
		if (xPrice.Amount > yPrice.Amount)
		{
			return 1;
		}
		return 0;
	}

	public static ProductDataModel NetGoldCostBoosterToProduct(Network.GoldCostBooster goldCostBooster)
	{
		if (!goldCostBooster.Cost.HasValue)
		{
			Log.Store.PrintError("GoldCostBooster has no cost value. Booster ID = {0}", goldCostBooster.ID);
			return null;
		}
		if (goldCostBooster.Cost.Value < 0)
		{
			Log.Store.PrintError("GoldCostBooster has invalid cost value {0}. Booster ID = {1}", goldCostBooster.Cost.Value, goldCostBooster.ID);
			return null;
		}
		BoosterDbfRecord record = GameDbf.Booster.GetRecord(goldCostBooster.ID);
		if (record == null)
		{
			Log.Store.PrintError("GoldCostBooster has unknown booster ID {0}", goldCostBooster.ID);
			return null;
		}
		ProductDataModel productDataModel = new ProductDataModel
		{
			Name = record.Name,
			Availability = ProductAvailability.CAN_PURCHASE
		};
		productDataModel.Prices.Add(new PriceDataModel
		{
			Currency = CurrencyType.GOLD,
			Amount = goldCostBooster.Cost.Value,
			DisplayText = goldCostBooster.Cost.Value.ToString()
		});
		productDataModel.Items.Add(new RewardItemDataModel
		{
			ItemType = RewardItemType.BOOSTER,
			ItemId = goldCostBooster.ID,
			Quantity = 1,
			Booster = new PackDataModel
			{
				Type = (BoosterDbId)goldCostBooster.ID,
				Quantity = 1
			}
		});
		productDataModel.Tags.Add("booster");
		productDataModel.RewardList = new RewardListDataModel();
		productDataModel.RewardList.Items.AddRange(productDataModel.Items);
		productDataModel.SetupProductStrings();
		return productDataModel;
	}

	public static bool IsPrimaryProductTag(string tag)
	{
		if (!(tag == "bundle") && !(tag == "adventure"))
		{
			if (tag != "undefined")
			{
				return Enum.GetNames(typeof(RewardItemType)).Contains(tag.ToUpperInvariant());
			}
			return false;
		}
		return true;
	}

	public static bool ShouldItemTypeBeGrouped(ProductType productType)
	{
		if (productType == ProductType.PRODUCT_TYPE_SELLABLE_DECK)
		{
			return true;
		}
		return false;
	}

	public static bool ShouldItemTypeBeGrouped(RewardItemType productType)
	{
		if (productType == RewardItemType.SELLABLE_DECK)
		{
			return true;
		}
		return false;
	}
}
