using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;

public static class ProductFactory
{
	public static ProductDataModel CreateEmptyProductDataModel()
	{
		return new ProductDataModel();
	}

	public static ProductDataModel CreateProductDataModel(ShopProductData.ProductData productData)
	{
		return new ProductDataModel
		{
			PmtId = productData.productId,
			Name = productData.name,
			Tags = CatalogUtils.ParseTagsString(productData.tags).ToDataModelList()
		};
	}

	public static ProductDataModel CreateProductDataModel(Network.Bundle netBundle)
	{
		if (!netBundle.PMTProductID.HasValue || netBundle.PMTProductID.Value == 0L)
		{
			Log.Store.PrintError("A product Network.Bundle has no PMTProductID");
			return null;
		}
		string text = ((netBundle.DisplayName != null) ? netBundle.DisplayName.GetString() : "");
		string description = ((netBundle.DisplayDescription != null) ? netBundle.DisplayDescription.GetString() : "");
		ProductDataModel productDataModel = new ProductDataModel
		{
			PmtId = netBundle.PMTProductID.Value,
			Name = text,
			Description = description
		};
		if (netBundle.Attributes != null && netBundle.Attributes.TryGetValue("tags", out var value))
		{
			productDataModel.Tags.AddRange(CatalogUtils.ParseTagsString(value));
		}
		if (!netBundle.Cost.HasValue && !netBundle.GtappGoldCost.HasValue && !netBundle.VirtualCurrencyCost.HasValue && !productDataModel.IsFree())
		{
			Log.Store.PrintWarning($"Product {netBundle.PMTProductID} [{text}] has no price values and is not free");
			return null;
		}
		if (netBundle.Items.Count == 0)
		{
			Log.Store.PrintWarning($"Product {netBundle.PMTProductID} [{text}] has no items");
			return null;
		}
		if (PlatformSettings.IsMobile() && productDataModel.Tags.Contains("hide_on_mobile"))
		{
			Log.Store.PrintInfo($"Product {netBundle.PMTProductID} [{text}] is tagged to be hidden from mobile");
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
		else if (netBundle.GtappGoldCost.HasValue)
		{
			PriceDataModel item2 = new PriceDataModel
			{
				Currency = CurrencyType.GOLD,
				Amount = netBundle.GtappGoldCost.Value
			};
			productDataModel.Prices.Add(item2);
		}
		if (netBundle.VirtualCurrencyCost.HasValue)
		{
			CurrencyType currencyTypeFromCode = ShopUtils.GetCurrencyTypeFromCode(netBundle.VirtualCurrencyCode);
			if (!ShopUtils.IsCurrencyVirtual(currencyTypeFromCode))
			{
				Log.Store.PrintWarning($"Product {netBundle.PMTProductID} [{text}] has virtual currency price with unrecognized currency code {netBundle.VirtualCurrencyCode}");
			}
			else
			{
				PriceDataModel item3 = new PriceDataModel
				{
					Currency = currencyTypeFromCode,
					Amount = netBundle.VirtualCurrencyCost.Value
				};
				productDataModel.Prices.Add(item3);
			}
		}
		if (netBundle.Cost.HasValue)
		{
			PriceDataModel item4 = new PriceDataModel
			{
				Currency = CurrencyType.REAL_MONEY,
				Amount = (netBundle.CostDisplay.HasValue ? ((float)netBundle.CostDisplay.Value) : 0f)
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
		foreach (Network.BundleItem item5 in netBundle.Items)
		{
			bool isValidItem;
			RewardItemDataModel rewardItemDataModel = RewardFactory.CreateShopRewardItemDataModel(netBundle, item5, out isValidItem);
			if (!isValidItem)
			{
				flag = true;
				Log.Store.PrintWarning($"Invalid item {item5.ItemType}:{item5.ProductData} found in product {productDataModel.PmtId} ({productDataModel.Name})");
			}
			if (rewardItemDataModel != null)
			{
				list.Add(rewardItemDataModel);
			}
		}
		if (flag)
		{
			Log.Store.PrintError($"Product {netBundle.PMTProductID} [{text}] has invalid items. It will not be added.");
			return null;
		}
		if (list.Count == 0)
		{
			Log.Store.PrintWarning($"Product {netBundle.PMTProductID} [{text}] has no items. It will be ignored.");
			return null;
		}
		list.Sort(RewardUtils.CompareItemsForSort);
		productDataModel.Items.AddRange(list);
		if (!productDataModel.AddAutomaticTagsAndItems(netBundle))
		{
			return null;
		}
		productDataModel.GenerateRewardList();
		productDataModel.SetupProductStrings();
		return productDataModel;
	}

	public static ProductTierDataModel CreateEmptyProductTier()
	{
		return new ProductTierDataModel();
	}
}
