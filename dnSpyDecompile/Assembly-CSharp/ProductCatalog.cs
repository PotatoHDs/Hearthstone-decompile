using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.DataModels;

// Token: 0x020006AD RID: 1709
public class ProductCatalog
{
	// Token: 0x170005AB RID: 1451
	// (get) Token: 0x06005F34 RID: 24372 RVA: 0x001EF3B4 File Offset: 0x001ED5B4
	public List<ProductTierDataModel> Tiers
	{
		get
		{
			return this.m_tiers;
		}
	}

	// Token: 0x170005AC RID: 1452
	// (get) Token: 0x06005F35 RID: 24373 RVA: 0x001EF3BC File Offset: 0x001ED5BC
	public List<ProductDataModel> Products
	{
		get
		{
			return this.m_products;
		}
	}

	// Token: 0x170005AD RID: 1453
	// (get) Token: 0x06005F36 RID: 24374 RVA: 0x001EF3C4 File Offset: 0x001ED5C4
	public long TiersChangeCount
	{
		get
		{
			return this.m_tiersChangeCount;
		}
	}

	// Token: 0x170005AE RID: 1454
	// (get) Token: 0x06005F37 RID: 24375 RVA: 0x001EF3CC File Offset: 0x001ED5CC
	public ProductDataModel VirtualCurrencyProductItem
	{
		get
		{
			return this.m_virtualCurrencyProduct;
		}
	}

	// Token: 0x170005AF RID: 1455
	// (get) Token: 0x06005F38 RID: 24376 RVA: 0x001EF3D4 File Offset: 0x001ED5D4
	public ProductDataModel BoosterCurrencyProductItem
	{
		get
		{
			return this.m_boosterCurrencyProduct;
		}
	}

	// Token: 0x170005B0 RID: 1456
	// (get) Token: 0x06005F39 RID: 24377 RVA: 0x001EF3DC File Offset: 0x001ED5DC
	// (set) Token: 0x06005F3A RID: 24378 RVA: 0x001EF3E4 File Offset: 0x001ED5E4
	public bool HasTestData { get; private set; }

	// Token: 0x170005B1 RID: 1457
	// (get) Token: 0x06005F3B RID: 24379 RVA: 0x001EF3ED File Offset: 0x001ED5ED
	// (set) Token: 0x06005F3C RID: 24380 RVA: 0x001EF3F5 File Offset: 0x001ED5F5
	public bool HasNetData { get; private set; }

	// Token: 0x170005B2 RID: 1458
	// (get) Token: 0x06005F3D RID: 24381 RVA: 0x001EF3FE File Offset: 0x001ED5FE
	public bool HasData
	{
		get
		{
			return this.HasTestData || this.HasNetData;
		}
	}

	// Token: 0x170005B3 RID: 1459
	// (get) Token: 0x06005F3E RID: 24382 RVA: 0x001EF410 File Offset: 0x001ED610
	public ProductCatalog.TestDataMode CurrentTestDataMode
	{
		get
		{
			return this.m_testDataMode;
		}
	}

	// Token: 0x06005F3F RID: 24383 RVA: 0x001EF418 File Offset: 0x001ED618
	public void SetTestDataMode(ProductCatalog.TestDataMode mode)
	{
		if (this.m_testDataMode == mode)
		{
			return;
		}
		if (mode != ProductCatalog.TestDataMode.NO_TEST_DATA)
		{
			if (mode == ProductCatalog.TestDataMode.ONLY_TEST_DATA)
			{
				this.ClearNonTestData();
			}
		}
		else
		{
			this.ClearTestData();
		}
		this.m_testDataMode = mode;
	}

	// Token: 0x06005F40 RID: 24384 RVA: 0x001EF444 File Offset: 0x001ED644
	public ProductCatalog(StoreManager storeManager)
	{
		storeManager.RegisterStatusChangedListener(new Action<bool>(this.OnStoreStatusChanged));
	}

	// Token: 0x06005F41 RID: 24385 RVA: 0x001EF4A0 File Offset: 0x001ED6A0
	public Network.ShopSection GetNetworkSection(ProductTierDataModel tier)
	{
		Network.ShopSection result;
		this.m_tierSectionMapping.TryGetValue(tier, out result);
		return result;
	}

	// Token: 0x06005F42 RID: 24386 RVA: 0x001EF4C0 File Offset: 0x001ED6C0
	public void PopulateWithNetData(List<Network.Bundle> netBundles, List<Network.GoldCostBooster> netGoldBoosters, List<Network.ShopSection> netSections)
	{
		if (this.CurrentTestDataMode == ProductCatalog.TestDataMode.ONLY_TEST_DATA)
		{
			return;
		}
		this.ClearNonTestData();
		this.HasNetData = true;
		this.m_products.Capacity = Math.Max(this.m_products.Capacity, netBundles.Count<Network.Bundle>() + netGoldBoosters.Count<Network.GoldCostBooster>());
		this.AddNetGoldBoosterProducts(netGoldBoosters);
		this.AddNetBundleProducts(netBundles);
		this.UpdateProductStatus();
		this.PopulateTiers(netSections);
		this.PopulateProductVariants();
	}

	// Token: 0x06005F43 RID: 24387 RVA: 0x001EF530 File Offset: 0x001ED730
	public ProductDataModel GetProductByPmtId(long pmtId)
	{
		if (pmtId == 0L)
		{
			return null;
		}
		return this.m_products.FirstOrDefault((ProductDataModel p) => p.PmtId == pmtId);
	}

	// Token: 0x06005F44 RID: 24388 RVA: 0x001EF56C File Offset: 0x001ED76C
	public void PopulateWithTestData(ShopProductData testData)
	{
		bool flag = false;
		bool flag2 = false;
		switch (this.CurrentTestDataMode)
		{
		case ProductCatalog.TestDataMode.ONLY_TEST_DATA:
			flag = true;
			flag2 = true;
			break;
		case ProductCatalog.TestDataMode.ADD_PRODUCT_TEST_DATA:
			flag = true;
			break;
		case ProductCatalog.TestDataMode.TIER_TEST_DATA:
			flag2 = true;
			break;
		}
		if (!flag && !flag2)
		{
			return;
		}
		Log.Store.Print("=== Begin populate ProductCatalog with test data ===", Array.Empty<object>());
		this.ClearTiers();
		this.ClearTestData();
		this.HasTestData = true;
		if (flag && testData.productCatalog != null)
		{
			Dictionary<long, RewardItemDataModel> dictionary = new Dictionary<long, RewardItemDataModel>();
			if (testData.productItemCatalog != null)
			{
				foreach (ShopProductData.ProductItemData productItemData in testData.productItemCatalog)
				{
					RewardItemDataModel rewardItemDataModel = RewardFactory.CreateShopProductRewardItemDataModel(productItemData);
					RewardUtils.InitializeRewardItemDataModelForShop(rewardItemDataModel, null, string.Format("Failure initializing item license {0} from test file: ", productItemData.licenseId));
					if (dictionary.ContainsKey(rewardItemDataModel.PmtLicenseId))
					{
						Log.Store.PrintWarning(string.Format("[ProductCatalog.PopulateWithTestData] duplicate ProductItem ID {0}", rewardItemDataModel.PmtLicenseId), Array.Empty<object>());
					}
					dictionary[rewardItemDataModel.PmtLicenseId] = rewardItemDataModel;
				}
			}
			bool flag3 = StoreManager.Get().IsOpen(true);
			foreach (ShopProductData.ProductData productData in testData.productCatalog)
			{
				ProductDataModel productDataModel = ProductFactory.CreateProductDataModel(productData);
				List<RewardItemDataModel> list = new List<RewardItemDataModel>();
				if (productData.licenseIds != null)
				{
					foreach (long num in productData.licenseIds)
					{
						RewardItemDataModel item;
						if (!dictionary.TryGetValue(num, out item))
						{
							Log.Store.PrintWarning(string.Format("[ProductCatalog.PopulateWithTestData] Product {0} referencing license {1} with no ProductItem", productDataModel.PmtId, num), Array.Empty<object>());
						}
						else
						{
							list.Add(item);
						}
					}
				}
				foreach (ShopProductData.PriceData priceData in productData.prices)
				{
					PriceDataModel item2 = new PriceDataModel
					{
						Currency = priceData.currencyType,
						Amount = (float)priceData.amount
					};
					productDataModel.Prices.Add(item2);
				}
				if (flag3)
				{
					productDataModel.FormatProductPrices(null);
				}
				productDataModel.Items.AddRange(list);
				productDataModel.RewardList = new RewardListDataModel();
				productDataModel.RewardList.Items.AddRange(list);
				productDataModel.SetupProductStrings();
				if (productDataModel.PmtId != 0L)
				{
					ProductDataModel productByPmtId = this.GetProductByPmtId(productDataModel.PmtId);
					if (productByPmtId != null)
					{
						Log.Store.Print(string.Format("[ProductCatalog.PopulateWithTestData] Replacing existing product with conflicting Product PMT = ID {0}", productData.productId), Array.Empty<object>());
						this.RemoveProduct(productByPmtId);
					}
				}
				this.m_products.Add(productDataModel);
				this.m_productsFromTestData.Add(productDataModel);
			}
			this.SortPrices();
			this.UpdateProductStatus();
		}
		if (flag2 && testData.productTierCatalog != null)
		{
			foreach (ShopProductData.ProductTierData productTierData in testData.productTierCatalog)
			{
				ProductTierDataModel productTierDataModel = new ProductTierDataModel
				{
					Style = productTierData.tierId,
					Header = productTierData.header
				};
				productTierDataModel.Tags.AddRange(CatalogUtils.ParseTagsString(productTierData.tags));
				foreach (long pmtId in productTierData.productIds)
				{
					ProductDataModel productByPmtId2 = this.GetProductByPmtId(pmtId);
					if (productByPmtId2 != null)
					{
						ShopBrowserButtonDataModel item3 = productByPmtId2.ToButton(false);
						productTierDataModel.BrowserButtons.Add(item3);
					}
				}
				if (productTierDataModel.BrowserButtons.Count > 0)
				{
					this.m_tiers.Add(productTierDataModel);
					this.m_tiersFromTestData.Add(productTierDataModel);
				}
			}
			this.m_tiersChangeCount += 1L;
		}
		else
		{
			this.PopulateTiers(null);
		}
		this.PopulateProductVariants();
		Log.Store.Print("=== End populate ProductCatalog with test data ===", Array.Empty<object>());
	}

	// Token: 0x06005F45 RID: 24389 RVA: 0x001EF964 File Offset: 0x001EDB64
	public void UpdateProductStatus()
	{
		Log.Store.PrintDebug(string.Format("Updating Product Status at {0:g}", DateTime.Now), Array.Empty<object>());
		string format;
		if (!CatalogUtils.CanUpdateProductStatus(out format))
		{
			Log.Store.PrintWarning(format, Array.Empty<object>());
			return;
		}
		this.m_hasUpdatedProductStatusOnce = true;
		this.m_latestBoosterId = GameUtils.GetLatestRewardableBooster();
		this.m_latestAdventureId = GameUtils.GetLatestActiveAdventure();
		bool shouldSeeWild = CollectionManager.Get() != null && CollectionManager.Get().ShouldAccountSeeStandardWild();
		this.UpdateWarningThreshold(StoreManager.Get().AllSections);
		foreach (ProductDataModel productDataModel in this.m_products)
		{
			if (productDataModel.PmtId == 0L)
			{
				productDataModel.Availability = ProductAvailability.UNDEFINED;
				BuyNoGTAPPEventArgs buyNoGTAPPEventArgs;
				long num;
				if (productDataModel.Prices.Count == 1 && (buyNoGTAPPEventArgs = (productDataModel.GetBuyProductArgs(productDataModel.Prices[0], 1) as BuyNoGTAPPEventArgs)) != null && StoreManager.Get().GetGoldCostNoGTAPP(buyNoGTAPPEventArgs.transactionData, out num))
				{
					productDataModel.Availability = ProductAvailability.CAN_PURCHASE;
				}
			}
			else
			{
				Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(productDataModel.PmtId);
				productDataModel.Availability = StoreManager.Get().GetNetworkBundleProductAvailability(bundleFromPmtProductId, shouldSeeWild, true);
				if (productDataModel.Availability == ProductAvailability.CAN_PURCHASE && !StoreManager.Get().IgnoreProductTiming && productDataModel.GetPrimaryProductTag() == "booster")
				{
					BoosterDbId productBoosterId = productDataModel.GetProductBoosterId();
					if (productBoosterId != BoosterDbId.INVALID)
					{
						BoosterDbfRecord record = GameDbf.Booster.GetRecord((int)productBoosterId);
						if (record != null && !string.IsNullOrEmpty(record.BuyWithGoldEvent) && !SpecialEventManager.Get().IsEventActive(record.BuyWithGoldEvent, false))
						{
							productDataModel.Availability = ProductAvailability.SALE_NOT_ACTIVE;
						}
					}
				}
			}
			this.UpdateProductFreshness(productDataModel);
		}
		if (this.HasTestData)
		{
			foreach (ProductDataModel productDataModel2 in from p in this.m_productsFromTestData
			where p.Availability == ProductAvailability.UNDEFINED
			select p)
			{
				productDataModel2.Availability = ProductAvailability.CAN_PURCHASE;
			}
		}
		BnetBar.Get().RefreshCurrency();
		this.UpdateNextCatalogChangeTime();
	}

	// Token: 0x06005F46 RID: 24390 RVA: 0x001EFBC8 File Offset: 0x001EDDC8
	public bool TryRefreshStaleProductAvailability()
	{
		string text;
		if (!CatalogUtils.CanUpdateProductStatus(out text))
		{
			return false;
		}
		if (this.IsProductAvailabilityStale())
		{
			this.m_nextCatalogChangeTimeUtc = null;
			this.UpdateProductStatus();
			this.PopulateTiers(null);
			this.PopulateProductVariants();
			return true;
		}
		return false;
	}

	// Token: 0x06005F47 RID: 24391 RVA: 0x001EFC0C File Offset: 0x001EDE0C
	public string DebugFillShopWithProduct(long pmtProductId)
	{
		if (pmtProductId == 0L)
		{
			return "0 is not a valid PMT product ID";
		}
		ProductDataModel productByPmtId = this.GetProductByPmtId(pmtProductId);
		if (productByPmtId != null)
		{
			this.PopulateVariantsForProduct(productByPmtId);
			this.ClearTiers();
			this.SetTestDataMode(ProductCatalog.TestDataMode.TIER_TEST_DATA);
			this.HasTestData = true;
			foreach (string text in new string[]
			{
				"S",
				"BigSmall",
				"Standard",
				"Mammoth"
			})
			{
				ProductTierDataModel productTierDataModel = new ProductTierDataModel
				{
					Header = "Test Tier: " + text,
					Style = text
				};
				for (int j = 0; j < 4; j++)
				{
					ShopBrowserButtonDataModel item = productByPmtId.ToButton(false);
					productTierDataModel.BrowserButtons.Add(item);
				}
				this.m_tiers.Add(productTierDataModel);
				this.m_tiersFromTestData.Add(productTierDataModel);
			}
			this.m_tiersChangeCount += 1L;
			return null;
		}
		if (StoreManager.Get().GetBundleFromPmtProductId(pmtProductId) == null)
		{
			return string.Format("Product {0} not received from server.", pmtProductId);
		}
		return string.Format("Product {0} failed client validation. See Store log.", pmtProductId);
	}

	// Token: 0x06005F48 RID: 24392 RVA: 0x001EFD21 File Offset: 0x001EDF21
	private void Clear()
	{
		this.HasTestData = false;
		this.HasNetData = false;
		this.ClearTiers();
		this.m_products.Clear();
		this.m_productsFromTestData.Clear();
		this.m_virtualCurrencyProduct = null;
		this.m_boosterCurrencyProduct = null;
	}

	// Token: 0x06005F49 RID: 24393 RVA: 0x001EFD5B File Offset: 0x001EDF5B
	private void ClearTiers()
	{
		if (this.m_tiers.Count > 0)
		{
			this.m_tiersChangeCount += 1L;
			this.m_tiers.Clear();
		}
		this.m_tiersFromTestData.Clear();
		this.m_tierSectionMapping.Clear();
	}

	// Token: 0x06005F4A RID: 24394 RVA: 0x001EFD9C File Offset: 0x001EDF9C
	private void ClearTestData()
	{
		if (!this.HasTestData)
		{
			return;
		}
		if (!this.HasNetData)
		{
			this.Clear();
			return;
		}
		foreach (ProductDataModel product in this.m_productsFromTestData.ToArray<ProductDataModel>())
		{
			this.RemoveProduct(product);
		}
		this.HasTestData = false;
	}

	// Token: 0x06005F4B RID: 24395 RVA: 0x001EFDF0 File Offset: 0x001EDFF0
	private void ClearNonTestData()
	{
		if (!this.HasNetData)
		{
			return;
		}
		if (!this.HasTestData)
		{
			this.Clear();
			return;
		}
		ProductDataModel[] array = this.m_productsFromTestData.ToArray<ProductDataModel>();
		ProductTierDataModel[] array2 = this.m_tiersFromTestData.ToArray<ProductTierDataModel>();
		this.Clear();
		this.m_products.AddRange(array);
		this.m_tiers.AddRange(array2);
		array.ForEach(delegate(ProductDataModel x)
		{
			this.m_productsFromTestData.Add(x);
		});
		array2.ForEach(delegate(ProductTierDataModel x)
		{
			this.m_tiersFromTestData.Add(x);
		});
		this.HasTestData = true;
		this.PopulateProductVariants();
	}

	// Token: 0x06005F4C RID: 24396 RVA: 0x001EFE7C File Offset: 0x001EE07C
	private void RemoveProduct(ProductDataModel product)
	{
		this.m_products.Remove(product);
		if (this.HasTestData)
		{
			this.m_productsFromTestData.Remove(product);
		}
		Func<ShopBrowserButtonDataModel, bool> <>9__0;
		foreach (ProductTierDataModel productTierDataModel in this.m_tiers)
		{
			IEnumerable<ShopBrowserButtonDataModel> browserButtons = productTierDataModel.BrowserButtons;
			Func<ShopBrowserButtonDataModel, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((ShopBrowserButtonDataModel b) => b.DisplayProduct == product));
			}
			foreach (ShopBrowserButtonDataModel item in browserButtons.Where(predicate))
			{
				productTierDataModel.BrowserButtons.Remove(item);
			}
			foreach (ShopBrowserButtonDataModel shopBrowserButtonDataModel in productTierDataModel.BrowserButtons)
			{
				shopBrowserButtonDataModel.DisplayProduct.Variants.Remove(product);
				this.m_tiersChangeCount += 1L;
			}
		}
		if (this.m_virtualCurrencyProduct != null)
		{
			if (this.m_virtualCurrencyProduct == product)
			{
				this.m_virtualCurrencyProduct = null;
			}
			else
			{
				this.m_virtualCurrencyProduct.Variants.Remove(product);
			}
		}
		if (this.m_boosterCurrencyProduct != null)
		{
			if (this.m_boosterCurrencyProduct == product)
			{
				this.m_boosterCurrencyProduct = null;
				return;
			}
			this.m_boosterCurrencyProduct.Variants.Remove(product);
		}
	}

	// Token: 0x06005F4D RID: 24397 RVA: 0x001F0038 File Offset: 0x001EE238
	private void OnStoreStatusChanged(bool isStoreOpen)
	{
		if (isStoreOpen)
		{
			Processor.QueueJob(new JobDefinition("ProductCatalog.PopulateInitialNetData", this.Job_PopulateInitialNetData(), Array.Empty<IJobDependency>()));
		}
	}

	// Token: 0x06005F4E RID: 24398 RVA: 0x001F0058 File Offset: 0x001EE258
	private bool IsProductAvailabilityStale()
	{
		return !this.m_hasUpdatedProductStatusOnce || (this.m_nextCatalogChangeTimeUtc != null && this.m_nextCatalogChangeTimeUtc.Value < DateTime.UtcNow);
	}

	// Token: 0x06005F4F RID: 24399 RVA: 0x001F0088 File Offset: 0x001EE288
	private IEnumerator<IAsyncJobResult> Job_PopulateInitialNetData()
	{
		string lastFailReason = string.Empty;
		string text;
		while (!CatalogUtils.CanUpdateProductStatus(out text))
		{
			if (text != lastFailReason)
			{
				Log.Store.PrintWarning("Could not update product status: {0}", new object[]
				{
					text
				});
				lastFailReason = text;
			}
			yield return new WaitForDuration(0.1f);
		}
		StoreManager storeManager = StoreManager.Get();
		this.PopulateWithNetData(storeManager.AllBundles.ToList<Network.Bundle>(), storeManager.AllGoldCostBoosters.ToList<Network.GoldCostBooster>(), storeManager.AllSections.ToList<Network.ShopSection>());
		foreach (ProductDataModel product in this.m_productsFromTestData)
		{
			product.FormatProductPrices(null);
		}
		Log.Store.PrintDebug("ProductCatalog initial population complete", Array.Empty<object>());
		yield break;
	}

	// Token: 0x06005F50 RID: 24400 RVA: 0x001F0098 File Offset: 0x001EE298
	private void AddNetGoldBoosterProducts(IEnumerable<Network.GoldCostBooster> netGoldBoosters)
	{
		foreach (Network.GoldCostBooster goldCostBooster in netGoldBoosters)
		{
			ProductDataModel productDataModel = CatalogUtils.NetGoldCostBoosterToProduct(goldCostBooster);
			if (productDataModel != null)
			{
				this.m_products.Add(productDataModel);
			}
		}
	}

	// Token: 0x06005F51 RID: 24401 RVA: 0x001F00F0 File Offset: 0x001EE2F0
	private void AddNetBundleProducts(IEnumerable<Network.Bundle> netBundles)
	{
		foreach (Network.Bundle bundle in netBundles)
		{
			ProductDataModel productDataModel = (bundle.PMTProductID != null) ? this.GetProductByPmtId(bundle.PMTProductID.Value) : null;
			if (productDataModel != null)
			{
				if (!this.m_productsFromTestData.Contains(productDataModel))
				{
					string text = (bundle.DisplayName != null) ? bundle.DisplayName.GetString(true) : "";
					Log.Store.PrintError("Ignoring Network.Bundle with PMTProductID that is already in use. PMT ID = {0}, Exiting Product Name = {1}, Ignored Product Name = {2}", new object[]
					{
						bundle.PMTProductID.Value,
						productDataModel.Name,
						text
					});
				}
			}
			else
			{
				ProductDataModel productDataModel2 = ProductFactory.CreateProductDataModel(bundle);
				if (productDataModel2 != null)
				{
					this.m_products.Add(productDataModel2);
				}
			}
		}
	}

	// Token: 0x06005F52 RID: 24402 RVA: 0x001F01E4 File Offset: 0x001EE3E4
	private void TryAssignProductToSlot(List<ShopBrowserButtonDataModel> buttons, ProductDataModel product, bool isFiller)
	{
		switch (product.Availability)
		{
		case ProductAvailability.CAN_PURCHASE:
			goto IL_CC;
		case ProductAvailability.ALREADY_OWNED:
			if (product.Tags.Contains("hide_owned"))
			{
				return;
			}
			goto IL_CC;
		case ProductAvailability.SALE_NOT_ACTIVE:
		{
			Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(product.PmtId);
			if (bundleFromPmtProductId != null)
			{
				ProductAvailabilityRange bundleAvailabilityRange = StoreManager.Get().GetBundleAvailabilityRange(bundleFromPmtProductId);
				string arg = (bundleAvailabilityRange != null) ? bundleAvailabilityRange.ToString() : "<unknown sale>";
				Log.Store.PrintDebug(string.Format("Product will not appear in tier because sale is not active. PMT ID = {0}, Name = {1}, range = {2}", product.PmtId, product.Name, arg), Array.Empty<object>());
			}
			return;
		}
		}
		Log.Store.PrintDebug(string.Format("Product will not appear in Store because status is {0}. PMT ID = {1}, Name = {2}", product.Availability, product.PmtId, product.Name), Array.Empty<object>());
		return;
		IL_CC:
		ShopBrowserButtonDataModel item = product.ToButton(isFiller);
		buttons.Add(item);
	}

	// Token: 0x06005F53 RID: 24403 RVA: 0x001F02CC File Offset: 0x001EE4CC
	private void PopulateTiersFromNetSections(IEnumerable<Network.ShopSection> netSections)
	{
		this.ClearTiers();
		if (!this.HasData)
		{
			return;
		}
		foreach (Network.ShopSection shopSection in from s in netSections
		orderby s.SortOrder
		select s)
		{
			List<ShopBrowserButtonDataModel> list = new List<ShopBrowserButtonDataModel>();
			IEnumerable<Network.ShopSection.ProductRef> enumerable = shopSection.Products.OrderBy((Network.ShopSection.ProductRef p) => p.OrderId);
			List<string> list2 = new List<string>();
			if (!string.IsNullOrEmpty(shopSection.FillerTags))
			{
				list2.AddRange(CatalogUtils.ParseTagsString(shopSection.FillerTags));
			}
			foreach (Network.ShopSection.ProductRef productRef in enumerable)
			{
				ProductDataModel product = this.GetProductByPmtId(productRef.PmtId);
				if (product == null)
				{
					Log.Store.PrintDebug(string.Format("Section [{0}] error: Ignoring product that is not known to client: PMT ID = {1}", shopSection.InternalName, productRef.PmtId), Array.Empty<object>());
				}
				else
				{
					bool isFiller = list2.Any((string t) => product.Tags.Contains(t));
					this.TryAssignProductToSlot(list, product, isFiller);
				}
			}
			if (list.Count == 0)
			{
				Log.Store.Print("Tier [" + shopSection.InternalName + "] is hidden because it has no products", Array.Empty<object>());
			}
			else
			{
				ProductTierDataModel productTierDataModel = new ProductTierDataModel
				{
					Header = shopSection.Label.GetString(true),
					Style = shopSection.Style
				};
				productTierDataModel.BrowserButtons.AddRange(list);
				this.m_tiers.Add(productTierDataModel);
				this.m_tierSectionMapping.Add(productTierDataModel, shopSection);
			}
		}
		this.m_tiersChangeCount += 1L;
	}

	// Token: 0x06005F54 RID: 24404 RVA: 0x001F04EC File Offset: 0x001EE6EC
	private void PopulateTiers(IEnumerable<Network.ShopSection> sections = null)
	{
		if (this.m_testDataMode == ProductCatalog.TestDataMode.ONLY_TEST_DATA || this.m_testDataMode == ProductCatalog.TestDataMode.TIER_TEST_DATA)
		{
			return;
		}
		if (!this.m_hasUpdatedProductStatusOnce)
		{
			return;
		}
		this.PopulateTiersFromNetSections(sections ?? StoreManager.Get().AllSections);
	}

	// Token: 0x06005F55 RID: 24405 RVA: 0x001F0520 File Offset: 0x001EE720
	private void PopulateProductVariants()
	{
		foreach (ProductTierDataModel productTierDataModel in this.m_tiers)
		{
			foreach (ShopBrowserButtonDataModel shopBrowserButtonDataModel in productTierDataModel.BrowserButtons)
			{
				this.PopulateVariantsForProduct(shopBrowserButtonDataModel.DisplayProduct);
			}
		}
		if (ShopUtils.IsVirtualCurrencyEnabled())
		{
			this.m_virtualCurrencyProduct = this.GetPrimaryProductForItemAndPopulateVariants(RewardItemType.RUNESTONES, 0);
			if (this.m_virtualCurrencyProduct == null)
			{
				Log.Store.PrintError("Failed to find any Runestone products.", Array.Empty<object>());
			}
			this.m_boosterCurrencyProduct = this.GetPrimaryProductForItemAndPopulateVariants(RewardItemType.ARCANE_ORBS, 0);
			if (this.m_boosterCurrencyProduct == null)
			{
				Log.Store.PrintError("Failed to find any Arcane Orb products.", Array.Empty<object>());
				return;
			}
		}
		else
		{
			this.m_virtualCurrencyProduct = null;
			this.m_boosterCurrencyProduct = null;
		}
	}

	// Token: 0x06005F56 RID: 24406 RVA: 0x001F0618 File Offset: 0x001EE818
	private void PopulateVariantsForProduct(ProductDataModel product)
	{
		product.Variants.Clear();
		if (product.Items.Count == 1 && !product.Tags.Contains("bundle"))
		{
			RewardItemDataModel rewardItemDataModel = product.Items.First<RewardItemDataModel>();
			product.Variants.AddRange(this.GetVariantsByItemType(rewardItemDataModel.ItemType, rewardItemDataModel.ItemId, product.PmtId));
			if (CatalogUtils.ShouldItemTypeBeGrouped(rewardItemDataModel.ItemType))
			{
				product.BuildItemBundleFromVariantGroup();
				return;
			}
		}
		else
		{
			product.Variants.Add(product);
		}
	}

	// Token: 0x06005F57 RID: 24407 RVA: 0x001F06A0 File Offset: 0x001EE8A0
	private ProductDataModel GetPrimaryProductForItemAndPopulateVariants(RewardItemType itemType, int itemId)
	{
		List<ProductDataModel> variantsByItemType = this.GetVariantsByItemType(itemType, itemId, 0L);
		ProductDataModel productDataModel = variantsByItemType.FirstOrDefault<ProductDataModel>();
		if (productDataModel == null)
		{
			return null;
		}
		productDataModel.Variants.Clear();
		productDataModel.Variants.AddRange(variantsByItemType);
		return productDataModel;
	}

	// Token: 0x06005F58 RID: 24408 RVA: 0x001F06DC File Offset: 0x001EE8DC
	private List<ProductDataModel> GetVariantsByItemType(RewardItemType itemType, int itemId, long productPMTid = 0L)
	{
		List<ProductDataModel> list = new List<ProductDataModel>();
		foreach (ProductDataModel productDataModel in this.m_products)
		{
			if (CatalogUtils.ShouldItemTypeBeGrouped(itemType))
			{
				RewardItemDataModel rewardItemDataModel = productDataModel.Items.First<RewardItemDataModel>();
				if (rewardItemDataModel != null && rewardItemDataModel.ItemType == itemType)
				{
					list.Add(productDataModel);
				}
			}
			else if (productDataModel.Items.Count == 1 && !productDataModel.Tags.Contains("bundle"))
			{
				RewardItemDataModel rewardItemDataModel2 = productDataModel.Items.First<RewardItemDataModel>();
				if (rewardItemDataModel2 != null && rewardItemDataModel2.ItemType == itemType && rewardItemDataModel2.ItemId == itemId)
				{
					list.Add(productDataModel);
				}
			}
		}
		this.SortVariantsOfItemType(itemType, ref list);
		return list;
	}

	// Token: 0x06005F59 RID: 24409 RVA: 0x001F07B0 File Offset: 0x001EE9B0
	private void SortVariantsOfItemType(RewardItemType itemType, ref List<ProductDataModel> variants)
	{
		if (CatalogUtils.ShouldItemTypeBeGrouped(itemType))
		{
			variants.Sort(new Comparison<ProductDataModel>(CatalogUtils.CompareProductsByItemSortOrder));
			return;
		}
		variants.Sort(new Comparison<ProductDataModel>(CatalogUtils.CompareProductsForSortByQuantity));
	}

	// Token: 0x06005F5A RID: 24410 RVA: 0x001F07E4 File Offset: 0x001EE9E4
	private void SortPrices()
	{
		foreach (ProductDataModel productDataModel in this.m_products)
		{
			List<PriceDataModel> list = productDataModel.Prices.ToList<PriceDataModel>();
			list.Sort(new Comparison<PriceDataModel>(CatalogUtils.ComparePricesForSort));
			productDataModel.Prices.Clear();
			productDataModel.Prices.AddRange(list);
		}
	}

	// Token: 0x06005F5B RID: 24411 RVA: 0x001F0864 File Offset: 0x001EEA64
	private void UpdateWarningThreshold(IEnumerable<Network.ShopSection> sections)
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		StoreManager storeManager = StoreManager.Get();
		if (netObject == null || storeManager == null)
		{
			return;
		}
		bool flag = false;
		foreach (Network.ShopSection shopSection in sections)
		{
			foreach (Network.ShopSection.ProductRef productRef in shopSection.Products)
			{
				ProductDataModel productByPmtId = this.GetProductByPmtId(productRef.PmtId);
				if (productByPmtId != null && productByPmtId.Tags.Contains("prepurchase"))
				{
					Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(productRef.PmtId);
					ProductAvailabilityRange productAvailabilityRange = (bundleFromPmtProductId != null) ? storeManager.GetBundleAvailabilityRange(bundleFromPmtProductId) : null;
					if (productAvailabilityRange != null && productAvailabilityRange.IsVisibleAtTime(DateTime.Now))
					{
						flag = true;
						break;
					}
				}
			}
		}
		if (flag)
		{
			this.m_rotationWarningThreshold = netObject.Store.BoosterRotatingSoonWarnDaysWithSale;
			return;
		}
		this.m_rotationWarningThreshold = netObject.Store.BoosterRotatingSoonWarnDaysWithoutSale;
	}

	// Token: 0x06005F5C RID: 24412 RVA: 0x001F0988 File Offset: 0x001EEB88
	private void UpdateProductFreshness(ProductDataModel product)
	{
		bool flag = product.Tags.Contains("latest_expansion");
		bool flag2 = product.Tags.Contains("new");
		string primaryProductTag = product.GetPrimaryProductTag();
		if (primaryProductTag == "booster")
		{
			BoosterDbId productBoosterId = product.GetProductBoosterId();
			if (productBoosterId == this.m_latestBoosterId)
			{
				flag = true;
				flag2 = true;
			}
			else if (!flag2)
			{
				BoosterDbfRecord record = GameDbf.Booster.GetRecord((int)productBoosterId);
				flag2 = (record != null && record.LatestExpansionOrder == 0);
			}
			bool flag3 = GameUtils.IsBoosterWild(productBoosterId);
			if (!product.Tags.Contains("wild") && flag3)
			{
				product.Tags.Add("wild");
			}
			else if (!product.Tags.Contains("rotating_soon") && !flag3 && GameUtils.IsBoosterRotated(productBoosterId, DateTime.UtcNow.AddDays((double)this.m_rotationWarningThreshold)))
			{
				product.Tags.Add("rotating_soon");
			}
		}
		else if (primaryProductTag == "adventure")
		{
			AdventureDbId productAdventureId = product.GetProductAdventureId();
			flag |= (productAdventureId == this.m_latestAdventureId);
			flag2 = (flag2 || flag);
			if (!product.Tags.Contains("wild") && GameUtils.IsAdventureWild(productAdventureId))
			{
				product.Tags.Add("wild");
			}
		}
		else
		{
			flag2 = true;
		}
		if (flag2 && product.Availability == ProductAvailability.ALREADY_OWNED)
		{
			flag2 = false;
		}
		if (flag2)
		{
			string value = product.PmtId.ToString();
			flag2 = (Options.Get().GetString(Option.LATEST_SEEN_SHOP_PRODUCT_LIST).IndexOf(value) < 0);
		}
		product.SetProductTagPresence("new", flag2);
		product.SetProductTagPresence("latest_expansion", flag);
	}

	// Token: 0x06005F5D RID: 24413 RVA: 0x001F0B34 File Offset: 0x001EED34
	private void UpdateNextCatalogChangeTime()
	{
		StoreManager storeManager = StoreManager.Get();
		if (storeManager == null)
		{
			return;
		}
		DateTime utcNow = DateTime.UtcNow;
		if (this.m_nextCatalogChangeTimeUtc != null && this.m_nextCatalogChangeTimeUtc.Value <= utcNow)
		{
			return;
		}
		ProductDataModel productDataModel = null;
		ProductAvailabilityRange arg = null;
		foreach (ProductDataModel productDataModel2 in this.m_products)
		{
			if (productDataModel2.PmtId != 0L)
			{
				Network.Bundle bundleFromPmtProductId = storeManager.GetBundleFromPmtProductId(productDataModel2.PmtId);
				if (bundleFromPmtProductId != null)
				{
					ProductAvailabilityRange bundleAvailabilityRange = storeManager.GetBundleAvailabilityRange(bundleFromPmtProductId);
					if (bundleAvailabilityRange != null && !bundleAvailabilityRange.IsNever)
					{
						if (bundleAvailabilityRange.StartDateTime != null && bundleAvailabilityRange.StartDateTime.Value > utcNow && (this.m_nextCatalogChangeTimeUtc == null || bundleAvailabilityRange.StartDateTime.Value < this.m_nextCatalogChangeTimeUtc.Value))
						{
							this.m_nextCatalogChangeTimeUtc = new DateTime?(bundleAvailabilityRange.StartDateTime.Value);
							productDataModel = productDataModel2;
							arg = bundleAvailabilityRange;
						}
						if (bundleAvailabilityRange.SoftEndDateTime != null && bundleAvailabilityRange.SoftEndDateTime.Value > utcNow && (this.m_nextCatalogChangeTimeUtc == null || bundleAvailabilityRange.SoftEndDateTime.Value < this.m_nextCatalogChangeTimeUtc.Value))
						{
							this.m_nextCatalogChangeTimeUtc = new DateTime?(bundleAvailabilityRange.SoftEndDateTime.Value);
							productDataModel = productDataModel2;
							arg = bundleAvailabilityRange;
						}
					}
				}
			}
		}
		if (this.m_nextCatalogChangeTimeUtc != null)
		{
			Log.Store.PrintDebug(string.Format("Next product availability change at {0:g}", this.m_nextCatalogChangeTimeUtc.Value.ToLocalTime()), Array.Empty<object>());
			if (productDataModel != null)
			{
				Log.Store.PrintDebug(string.Format("Next product to change availability is PMT ID = {0}, Name = [{1}], range = {2}", productDataModel.PmtId, productDataModel.Name, arg), Array.Empty<object>());
				return;
			}
		}
		else
		{
			Log.Store.PrintDebug("No known incoming product availability changes", Array.Empty<object>());
		}
	}

	// Token: 0x0400502C RID: 20524
	public const int MaxArcaneOrbsBalance = 9999;

	// Token: 0x0400502D RID: 20525
	public const int ProductQuantityPromptLimit = 50;

	// Token: 0x0400502E RID: 20526
	private const int FillerProductRefOrderId = 100;

	// Token: 0x0400502F RID: 20527
	private readonly List<ProductDataModel> m_products = new List<ProductDataModel>();

	// Token: 0x04005030 RID: 20528
	private readonly List<ProductTierDataModel> m_tiers = new List<ProductTierDataModel>();

	// Token: 0x04005031 RID: 20529
	private readonly Dictionary<ProductTierDataModel, Network.ShopSection> m_tierSectionMapping = new Dictionary<ProductTierDataModel, Network.ShopSection>();

	// Token: 0x04005032 RID: 20530
	private ProductDataModel m_virtualCurrencyProduct;

	// Token: 0x04005033 RID: 20531
	private ProductDataModel m_boosterCurrencyProduct;

	// Token: 0x04005034 RID: 20532
	private readonly HashSet<ProductDataModel> m_productsFromTestData = new HashSet<ProductDataModel>();

	// Token: 0x04005035 RID: 20533
	private readonly HashSet<ProductTierDataModel> m_tiersFromTestData = new HashSet<ProductTierDataModel>();

	// Token: 0x04005036 RID: 20534
	private BoosterDbId m_latestBoosterId;

	// Token: 0x04005037 RID: 20535
	private AdventureDbId m_latestAdventureId;

	// Token: 0x04005038 RID: 20536
	private int m_rotationWarningThreshold;

	// Token: 0x04005039 RID: 20537
	private DateTime? m_nextCatalogChangeTimeUtc;

	// Token: 0x0400503A RID: 20538
	private long m_tiersChangeCount;

	// Token: 0x0400503B RID: 20539
	private bool m_hasUpdatedProductStatusOnce;

	// Token: 0x0400503E RID: 20542
	private ProductCatalog.TestDataMode m_testDataMode;

	// Token: 0x020021D7 RID: 8663
	public enum TestDataMode
	{
		// Token: 0x0400E178 RID: 57720
		NO_TEST_DATA,
		// Token: 0x0400E179 RID: 57721
		ONLY_TEST_DATA,
		// Token: 0x0400E17A RID: 57722
		ADD_PRODUCT_TEST_DATA,
		// Token: 0x0400E17B RID: 57723
		TIER_TEST_DATA
	}

	// Token: 0x020021D8 RID: 8664
	private enum CatalogSectionAttributeId
	{
		// Token: 0x0400E17D RID: 57725
		Style = 1,
		// Token: 0x0400E17E RID: 57726
		SortOrder
	}

	// Token: 0x020021D9 RID: 8665
	private enum CatalogSectionItemType
	{
		// Token: 0x0400E180 RID: 57728
		Container = 1,
		// Token: 0x0400E181 RID: 57729
		Product
	}

	// Token: 0x020021DA RID: 8666
	private enum CatalogStorefrontId
	{
		// Token: 0x0400E183 RID: 57731
		Hearthstone = 6
	}
}
