using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using PegasusUtil;

// Token: 0x020006A6 RID: 1702
public static class CatalogUtils
{
	// Token: 0x06005EE1 RID: 24289 RVA: 0x001EDC28 File Offset: 0x001EBE28
	public static IEnumerable<string> ParseTagsString(string tagsString)
	{
		if (string.IsNullOrEmpty(tagsString))
		{
			return new string[0];
		}
		return new HashSet<string>(from tagString in tagsString.Split(new char[]
		{
			','
		})
		select tagString.Trim().ToLowerInvariant());
	}

	// Token: 0x06005EE2 RID: 24290 RVA: 0x001EDC80 File Offset: 0x001EBE80
	public static bool CanUpdateProductStatus(out string reason)
	{
		if (!StoreManager.Get().AllSections.Any<Network.ShopSection>())
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
		AccountLicenseMgr.LicenseUpdateState licenseUpdateState = (AccountLicenseMgr.Get() != null) ? AccountLicenseMgr.Get().FixedLicensesState : AccountLicenseMgr.LicenseUpdateState.UNKNOWN;
		if (licenseUpdateState != AccountLicenseMgr.LicenseUpdateState.SUCCESS)
		{
			reason = string.Format("Cannot update product status when AccountLicenseMgr FixedLicensesState is {0}.", licenseUpdateState);
			return false;
		}
		reason = null;
		return true;
	}

	// Token: 0x06005EE3 RID: 24291 RVA: 0x001EDD4C File Offset: 0x001EBF4C
	public static int CompareProductsForSortByQuantity(ProductDataModel xProduct, ProductDataModel yProduct)
	{
		int num = (xProduct.Items.Count > 0) ? xProduct.Items[0].Quantity : 0;
		int num2 = (yProduct.Items.Count > 0) ? yProduct.Items[0].Quantity : 0;
		return num - num2;
	}

	// Token: 0x06005EE4 RID: 24292 RVA: 0x001EDDA0 File Offset: 0x001EBFA0
	public static int CompareProductsByItemSortOrder(ProductDataModel xProduct, ProductDataModel yProduct)
	{
		int sortOrderFromItems = RewardUtils.GetSortOrderFromItems(xProduct.Items);
		int sortOrderFromItems2 = RewardUtils.GetSortOrderFromItems(yProduct.Items);
		return sortOrderFromItems - sortOrderFromItems2;
	}

	// Token: 0x06005EE5 RID: 24293 RVA: 0x001EDDC8 File Offset: 0x001EBFC8
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

	// Token: 0x06005EE6 RID: 24294 RVA: 0x001EDE28 File Offset: 0x001EC028
	public static ProductDataModel NetGoldCostBoosterToProduct(Network.GoldCostBooster goldCostBooster)
	{
		if (goldCostBooster.Cost == null)
		{
			Log.Store.PrintError("GoldCostBooster has no cost value. Booster ID = {0}", new object[]
			{
				goldCostBooster.ID
			});
			return null;
		}
		if (goldCostBooster.Cost.Value < 0L)
		{
			Log.Store.PrintError("GoldCostBooster has invalid cost value {0}. Booster ID = {1}", new object[]
			{
				goldCostBooster.Cost.Value,
				goldCostBooster.ID
			});
			return null;
		}
		BoosterDbfRecord record = GameDbf.Booster.GetRecord(goldCostBooster.ID);
		if (record == null)
		{
			Log.Store.PrintError("GoldCostBooster has unknown booster ID {0}", new object[]
			{
				goldCostBooster.ID
			});
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
			Amount = (float)goldCostBooster.Cost.Value,
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

	// Token: 0x06005EE7 RID: 24295 RVA: 0x001EDFD8 File Offset: 0x001EC1D8
	public static bool IsPrimaryProductTag(string tag)
	{
		return tag == "bundle" || tag == "adventure" || (tag != "undefined" && Enum.GetNames(typeof(RewardItemType)).Contains(tag.ToUpperInvariant()));
	}

	// Token: 0x06005EE8 RID: 24296 RVA: 0x001EE02A File Offset: 0x001EC22A
	public static bool ShouldItemTypeBeGrouped(ProductType productType)
	{
		return productType == ProductType.PRODUCT_TYPE_SELLABLE_DECK;
	}

	// Token: 0x06005EE9 RID: 24297 RVA: 0x001EE034 File Offset: 0x001EC234
	public static bool ShouldItemTypeBeGrouped(RewardItemType productType)
	{
		return productType == RewardItemType.SELLABLE_DECK;
	}
}
