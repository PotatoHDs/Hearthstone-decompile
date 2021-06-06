using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;

// Token: 0x020006AF RID: 1711
public static class ProductFactory
{
	// Token: 0x06005F7B RID: 24443 RVA: 0x001F1BF3 File Offset: 0x001EFDF3
	public static ProductDataModel CreateEmptyProductDataModel()
	{
		return new ProductDataModel();
	}

	// Token: 0x06005F7C RID: 24444 RVA: 0x001F1BFA File Offset: 0x001EFDFA
	public static ProductDataModel CreateProductDataModel(ShopProductData.ProductData productData)
	{
		return new ProductDataModel
		{
			PmtId = productData.productId,
			Name = productData.name,
			Tags = CatalogUtils.ParseTagsString(productData.tags).ToDataModelList<string>()
		};
	}

	// Token: 0x06005F7D RID: 24445 RVA: 0x001F1C30 File Offset: 0x001EFE30
	public static ProductDataModel CreateProductDataModel(Network.Bundle netBundle)
	{
		if (netBundle.PMTProductID == null || netBundle.PMTProductID.Value == 0L)
		{
			Log.Store.PrintError("A product Network.Bundle has no PMTProductID", Array.Empty<object>());
			return null;
		}
		string text = (netBundle.DisplayName != null) ? netBundle.DisplayName.GetString(true) : "";
		string description = (netBundle.DisplayDescription != null) ? netBundle.DisplayDescription.GetString(true) : "";
		ProductDataModel productDataModel = new ProductDataModel
		{
			PmtId = netBundle.PMTProductID.Value,
			Name = text,
			Description = description
		};
		string tagsString;
		if (netBundle.Attributes != null && netBundle.Attributes.TryGetValue("tags", out tagsString))
		{
			productDataModel.Tags.AddRange(CatalogUtils.ParseTagsString(tagsString));
		}
		if (netBundle.Cost == null && netBundle.GtappGoldCost == null && netBundle.VirtualCurrencyCost == null && !productDataModel.IsFree())
		{
			Log.Store.PrintWarning(string.Format("Product {0} [{1}] has no price values and is not free", netBundle.PMTProductID, text), Array.Empty<object>());
			return null;
		}
		if (netBundle.Items.Count == 0)
		{
			Log.Store.PrintWarning(string.Format("Product {0} [{1}] has no items", netBundle.PMTProductID, text), Array.Empty<object>());
			return null;
		}
		if (PlatformSettings.IsMobile() && productDataModel.Tags.Contains("hide_on_mobile"))
		{
			Log.Store.PrintInfo(string.Format("Product {0} [{1}] is tagged to be hidden from mobile", netBundle.PMTProductID, text), Array.Empty<object>());
			return null;
		}
		if (productDataModel.IsFree())
		{
			PriceDataModel item = new PriceDataModel
			{
				Currency = CurrencyType.GOLD,
				Amount = 0f
			};
			productDataModel.Prices.Add(item);
		}
		else if (netBundle.GtappGoldCost != null)
		{
			PriceDataModel item2 = new PriceDataModel
			{
				Currency = CurrencyType.GOLD,
				Amount = (float)netBundle.GtappGoldCost.Value
			};
			productDataModel.Prices.Add(item2);
		}
		if (netBundle.VirtualCurrencyCost != null)
		{
			CurrencyType currencyTypeFromCode = ShopUtils.GetCurrencyTypeFromCode(netBundle.VirtualCurrencyCode);
			if (!ShopUtils.IsCurrencyVirtual(currencyTypeFromCode))
			{
				Log.Store.PrintWarning(string.Format("Product {0} [{1}] has virtual currency price with unrecognized currency code {2}", netBundle.PMTProductID, text, netBundle.VirtualCurrencyCode), Array.Empty<object>());
			}
			else
			{
				PriceDataModel item3 = new PriceDataModel
				{
					Currency = currencyTypeFromCode,
					Amount = (float)netBundle.VirtualCurrencyCost.Value
				};
				productDataModel.Prices.Add(item3);
			}
		}
		if (netBundle.Cost != null)
		{
			PriceDataModel item4 = new PriceDataModel
			{
				Currency = CurrencyType.REAL_MONEY,
				Amount = ((netBundle.CostDisplay != null) ? ((float)netBundle.CostDisplay.Value) : 0f)
			};
			productDataModel.Prices.Add(item4);
		}
		if (productDataModel.Prices.Count == 0)
		{
			return null;
		}
		productDataModel.FormatProductPrices(netBundle);
		bool flag = false;
		List<RewardItemDataModel> list = new List<RewardItemDataModel>();
		foreach (Network.BundleItem bundleItem in netBundle.Items)
		{
			bool flag2;
			RewardItemDataModel rewardItemDataModel = RewardFactory.CreateShopRewardItemDataModel(netBundle, bundleItem, out flag2);
			if (!flag2)
			{
				flag = true;
				Log.Store.PrintWarning(string.Format("Invalid item {0}:{1} found in product {2} ({3})", new object[]
				{
					bundleItem.ItemType,
					bundleItem.ProductData,
					productDataModel.PmtId,
					productDataModel.Name
				}), Array.Empty<object>());
			}
			if (rewardItemDataModel != null)
			{
				list.Add(rewardItemDataModel);
			}
		}
		if (flag)
		{
			Log.Store.PrintError(string.Format("Product {0} [{1}] has invalid items. It will not be added.", netBundle.PMTProductID, text), Array.Empty<object>());
			return null;
		}
		if (list.Count == 0)
		{
			Log.Store.PrintWarning(string.Format("Product {0} [{1}] has no items. It will be ignored.", netBundle.PMTProductID, text), Array.Empty<object>());
			return null;
		}
		list.Sort(new Comparison<RewardItemDataModel>(RewardUtils.CompareItemsForSort));
		productDataModel.Items.AddRange(list);
		if (!productDataModel.AddAutomaticTagsAndItems(netBundle))
		{
			return null;
		}
		productDataModel.GenerateRewardList();
		productDataModel.SetupProductStrings();
		return productDataModel;
	}

	// Token: 0x06005F7E RID: 24446 RVA: 0x001F2098 File Offset: 0x001F0298
	public static ProductTierDataModel CreateEmptyProductTier()
	{
		return new ProductTierDataModel();
	}
}
