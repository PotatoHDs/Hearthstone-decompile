using System;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Hearthstone.Core;
using Hearthstone.DataModels;

public class ProductCatalog
{
	public enum TestDataMode
	{
		NO_TEST_DATA,
		ONLY_TEST_DATA,
		ADD_PRODUCT_TEST_DATA,
		TIER_TEST_DATA
	}

	private enum CatalogSectionAttributeId
	{
		Style = 1,
		SortOrder
	}

	private enum CatalogSectionItemType
	{
		Container = 1,
		Product
	}

	private enum CatalogStorefrontId
	{
		Hearthstone = 6
	}

	public const int MaxArcaneOrbsBalance = 9999;

	public const int ProductQuantityPromptLimit = 50;

	private const int FillerProductRefOrderId = 100;

	private readonly List<ProductDataModel> m_products = new List<ProductDataModel>();

	private readonly List<ProductTierDataModel> m_tiers = new List<ProductTierDataModel>();

	private readonly Dictionary<ProductTierDataModel, Network.ShopSection> m_tierSectionMapping = new Dictionary<ProductTierDataModel, Network.ShopSection>();

	private ProductDataModel m_virtualCurrencyProduct;

	private ProductDataModel m_boosterCurrencyProduct;

	private readonly HashSet<ProductDataModel> m_productsFromTestData = new HashSet<ProductDataModel>();

	private readonly HashSet<ProductTierDataModel> m_tiersFromTestData = new HashSet<ProductTierDataModel>();

	private BoosterDbId m_latestBoosterId;

	private AdventureDbId m_latestAdventureId;

	private int m_rotationWarningThreshold;

	private DateTime? m_nextCatalogChangeTimeUtc;

	private long m_tiersChangeCount;

	private bool m_hasUpdatedProductStatusOnce;

	private TestDataMode m_testDataMode;

	public List<ProductTierDataModel> Tiers => m_tiers;

	public List<ProductDataModel> Products => m_products;

	public long TiersChangeCount => m_tiersChangeCount;

	public ProductDataModel VirtualCurrencyProductItem => m_virtualCurrencyProduct;

	public ProductDataModel BoosterCurrencyProductItem => m_boosterCurrencyProduct;

	public bool HasTestData { get; private set; }

	public bool HasNetData { get; private set; }

	public bool HasData
	{
		get
		{
			if (!HasTestData)
			{
				return HasNetData;
			}
			return true;
		}
	}

	public TestDataMode CurrentTestDataMode => m_testDataMode;

	public void SetTestDataMode(TestDataMode mode)
	{
		if (m_testDataMode != mode)
		{
			switch (mode)
			{
			case TestDataMode.NO_TEST_DATA:
				ClearTestData();
				break;
			case TestDataMode.ONLY_TEST_DATA:
				ClearNonTestData();
				break;
			}
			m_testDataMode = mode;
		}
	}

	public ProductCatalog(StoreManager storeManager)
	{
		storeManager.RegisterStatusChangedListener(OnStoreStatusChanged);
	}

	public Network.ShopSection GetNetworkSection(ProductTierDataModel tier)
	{
		m_tierSectionMapping.TryGetValue(tier, out var value);
		return value;
	}

	public void PopulateWithNetData(List<Network.Bundle> netBundles, List<Network.GoldCostBooster> netGoldBoosters, List<Network.ShopSection> netSections)
	{
		if (CurrentTestDataMode != TestDataMode.ONLY_TEST_DATA)
		{
			ClearNonTestData();
			HasNetData = true;
			m_products.Capacity = Math.Max(m_products.Capacity, netBundles.Count() + netGoldBoosters.Count());
			AddNetGoldBoosterProducts(netGoldBoosters);
			AddNetBundleProducts(netBundles);
			UpdateProductStatus();
			PopulateTiers(netSections);
			PopulateProductVariants();
		}
	}

	public ProductDataModel GetProductByPmtId(long pmtId)
	{
		if (pmtId == 0L)
		{
			return null;
		}
		return m_products.FirstOrDefault((ProductDataModel p) => p.PmtId == pmtId);
	}

	public void PopulateWithTestData(ShopProductData testData)
	{
		bool flag = false;
		bool flag2 = false;
		switch (CurrentTestDataMode)
		{
		case TestDataMode.ADD_PRODUCT_TEST_DATA:
			flag = true;
			break;
		case TestDataMode.ONLY_TEST_DATA:
			flag = true;
			flag2 = true;
			break;
		case TestDataMode.TIER_TEST_DATA:
			flag2 = true;
			break;
		}
		if (!(flag || flag2))
		{
			return;
		}
		Log.Store.Print("=== Begin populate ProductCatalog with test data ===");
		ClearTiers();
		ClearTestData();
		HasTestData = true;
		if (flag && testData.productCatalog != null)
		{
			Dictionary<long, RewardItemDataModel> dictionary = new Dictionary<long, RewardItemDataModel>();
			if (testData.productItemCatalog != null)
			{
				ShopProductData.ProductItemData[] productItemCatalog = testData.productItemCatalog;
				for (int i = 0; i < productItemCatalog.Length; i++)
				{
					ShopProductData.ProductItemData productItemData = productItemCatalog[i];
					RewardItemDataModel rewardItemDataModel = RewardFactory.CreateShopProductRewardItemDataModel(productItemData);
					RewardUtils.InitializeRewardItemDataModelForShop(rewardItemDataModel, null, $"Failure initializing item license {productItemData.licenseId} from test file: ");
					if (dictionary.ContainsKey(rewardItemDataModel.PmtLicenseId))
					{
						Log.Store.PrintWarning($"[ProductCatalog.PopulateWithTestData] duplicate ProductItem ID {rewardItemDataModel.PmtLicenseId}");
					}
					dictionary[rewardItemDataModel.PmtLicenseId] = rewardItemDataModel;
				}
			}
			bool flag3 = StoreManager.Get().IsOpen();
			ShopProductData.ProductData[] productCatalog = testData.productCatalog;
			for (int i = 0; i < productCatalog.Length; i++)
			{
				ShopProductData.ProductData productData = productCatalog[i];
				ProductDataModel productDataModel = ProductFactory.CreateProductDataModel(productData);
				List<RewardItemDataModel> list = new List<RewardItemDataModel>();
				if (productData.licenseIds != null)
				{
					long[] licenseIds = productData.licenseIds;
					foreach (long num in licenseIds)
					{
						if (!dictionary.TryGetValue(num, out var value))
						{
							Log.Store.PrintWarning($"[ProductCatalog.PopulateWithTestData] Product {productDataModel.PmtId} referencing license {num} with no ProductItem");
						}
						else
						{
							list.Add(value);
						}
					}
				}
				ShopProductData.PriceData[] prices = productData.prices;
				for (int j = 0; j < prices.Length; j++)
				{
					ShopProductData.PriceData priceData = prices[j];
					PriceDataModel item = new PriceDataModel
					{
						Currency = priceData.currencyType,
						Amount = (float)priceData.amount
					};
					productDataModel.Prices.Add(item);
				}
				if (flag3)
				{
					productDataModel.FormatProductPrices();
				}
				productDataModel.Items.AddRange(list);
				productDataModel.RewardList = new RewardListDataModel();
				productDataModel.RewardList.Items.AddRange(list);
				productDataModel.SetupProductStrings();
				if (productDataModel.PmtId != 0L)
				{
					ProductDataModel productByPmtId = GetProductByPmtId(productDataModel.PmtId);
					if (productByPmtId != null)
					{
						Log.Store.Print($"[ProductCatalog.PopulateWithTestData] Replacing existing product with conflicting Product PMT = ID {productData.productId}");
						RemoveProduct(productByPmtId);
					}
				}
				m_products.Add(productDataModel);
				m_productsFromTestData.Add(productDataModel);
			}
			SortPrices();
			UpdateProductStatus();
		}
		if (flag2 && testData.productTierCatalog != null)
		{
			ShopProductData.ProductTierData[] productTierCatalog = testData.productTierCatalog;
			for (int i = 0; i < productTierCatalog.Length; i++)
			{
				ShopProductData.ProductTierData productTierData = productTierCatalog[i];
				ProductTierDataModel productTierDataModel = new ProductTierDataModel
				{
					Style = productTierData.tierId,
					Header = productTierData.header
				};
				productTierDataModel.Tags.AddRange(CatalogUtils.ParseTagsString(productTierData.tags));
				long[] licenseIds = productTierData.productIds;
				foreach (long pmtId in licenseIds)
				{
					ProductDataModel productByPmtId2 = GetProductByPmtId(pmtId);
					if (productByPmtId2 != null)
					{
						ShopBrowserButtonDataModel item2 = productByPmtId2.ToButton();
						productTierDataModel.BrowserButtons.Add(item2);
					}
				}
				if (productTierDataModel.BrowserButtons.Count > 0)
				{
					m_tiers.Add(productTierDataModel);
					m_tiersFromTestData.Add(productTierDataModel);
				}
			}
			m_tiersChangeCount++;
		}
		else
		{
			PopulateTiers();
		}
		PopulateProductVariants();
		Log.Store.Print("=== End populate ProductCatalog with test data ===");
	}

	public void UpdateProductStatus()
	{
		Log.Store.PrintDebug($"Updating Product Status at {DateTime.Now:g}");
		if (!CatalogUtils.CanUpdateProductStatus(out var reason))
		{
			Log.Store.PrintWarning(reason);
			return;
		}
		m_hasUpdatedProductStatusOnce = true;
		m_latestBoosterId = GameUtils.GetLatestRewardableBooster();
		m_latestAdventureId = GameUtils.GetLatestActiveAdventure();
		bool shouldSeeWild = CollectionManager.Get() != null && CollectionManager.Get().ShouldAccountSeeStandardWild();
		UpdateWarningThreshold(StoreManager.Get().AllSections);
		foreach (ProductDataModel product in m_products)
		{
			if (product.PmtId == 0L)
			{
				product.Availability = ProductAvailability.UNDEFINED;
				BuyNoGTAPPEventArgs buyNoGTAPPEventArgs;
				if (product.Prices.Count == 1 && (buyNoGTAPPEventArgs = product.GetBuyProductArgs(product.Prices[0], 1) as BuyNoGTAPPEventArgs) != null && StoreManager.Get().GetGoldCostNoGTAPP(buyNoGTAPPEventArgs.transactionData, out var _))
				{
					product.Availability = ProductAvailability.CAN_PURCHASE;
				}
			}
			else
			{
				Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(product.PmtId);
				product.Availability = StoreManager.Get().GetNetworkBundleProductAvailability(bundleFromPmtProductId, shouldSeeWild);
				if (product.Availability == ProductAvailability.CAN_PURCHASE && !StoreManager.Get().IgnoreProductTiming && product.GetPrimaryProductTag() == "booster")
				{
					BoosterDbId productBoosterId = product.GetProductBoosterId();
					if (productBoosterId != 0)
					{
						BoosterDbfRecord record = GameDbf.Booster.GetRecord((int)productBoosterId);
						if (record != null && !string.IsNullOrEmpty(record.BuyWithGoldEvent) && !SpecialEventManager.Get().IsEventActive(record.BuyWithGoldEvent, activeIfDoesNotExist: false))
						{
							product.Availability = ProductAvailability.SALE_NOT_ACTIVE;
						}
					}
				}
			}
			UpdateProductFreshness(product);
		}
		if (HasTestData)
		{
			foreach (ProductDataModel item in m_productsFromTestData.Where((ProductDataModel p) => p.Availability == ProductAvailability.UNDEFINED))
			{
				item.Availability = ProductAvailability.CAN_PURCHASE;
			}
		}
		BnetBar.Get().RefreshCurrency();
		UpdateNextCatalogChangeTime();
	}

	public bool TryRefreshStaleProductAvailability()
	{
		if (!CatalogUtils.CanUpdateProductStatus(out var _))
		{
			return false;
		}
		if (IsProductAvailabilityStale())
		{
			m_nextCatalogChangeTimeUtc = null;
			UpdateProductStatus();
			PopulateTiers();
			PopulateProductVariants();
			return true;
		}
		return false;
	}

	public string DebugFillShopWithProduct(long pmtProductId)
	{
		if (pmtProductId == 0L)
		{
			return "0 is not a valid PMT product ID";
		}
		ProductDataModel productByPmtId = GetProductByPmtId(pmtProductId);
		if (productByPmtId == null)
		{
			if (StoreManager.Get().GetBundleFromPmtProductId(pmtProductId) == null)
			{
				return $"Product {pmtProductId} not received from server.";
			}
			return $"Product {pmtProductId} failed client validation. See Store log.";
		}
		PopulateVariantsForProduct(productByPmtId);
		ClearTiers();
		SetTestDataMode(TestDataMode.TIER_TEST_DATA);
		HasTestData = true;
		string[] array = new string[4] { "S", "BigSmall", "Standard", "Mammoth" };
		foreach (string text in array)
		{
			ProductTierDataModel productTierDataModel = new ProductTierDataModel
			{
				Header = "Test Tier: " + text,
				Style = text
			};
			for (int j = 0; j < 4; j++)
			{
				ShopBrowserButtonDataModel item = productByPmtId.ToButton();
				productTierDataModel.BrowserButtons.Add(item);
			}
			m_tiers.Add(productTierDataModel);
			m_tiersFromTestData.Add(productTierDataModel);
		}
		m_tiersChangeCount++;
		return null;
	}

	private void Clear()
	{
		HasTestData = false;
		HasNetData = false;
		ClearTiers();
		m_products.Clear();
		m_productsFromTestData.Clear();
		m_virtualCurrencyProduct = null;
		m_boosterCurrencyProduct = null;
	}

	private void ClearTiers()
	{
		if (m_tiers.Count > 0)
		{
			m_tiersChangeCount++;
			m_tiers.Clear();
		}
		m_tiersFromTestData.Clear();
		m_tierSectionMapping.Clear();
	}

	private void ClearTestData()
	{
		if (!HasTestData)
		{
			return;
		}
		if (!HasNetData)
		{
			Clear();
			return;
		}
		ProductDataModel[] array = m_productsFromTestData.ToArray();
		foreach (ProductDataModel product in array)
		{
			RemoveProduct(product);
		}
		HasTestData = false;
	}

	private void ClearNonTestData()
	{
		if (!HasNetData)
		{
			return;
		}
		if (!HasTestData)
		{
			Clear();
			return;
		}
		ProductDataModel[] array = m_productsFromTestData.ToArray();
		ProductTierDataModel[] array2 = m_tiersFromTestData.ToArray();
		Clear();
		m_products.AddRange(array);
		m_tiers.AddRange(array2);
		array.ForEach(delegate(ProductDataModel x)
		{
			m_productsFromTestData.Add(x);
		});
		array2.ForEach(delegate(ProductTierDataModel x)
		{
			m_tiersFromTestData.Add(x);
		});
		HasTestData = true;
		PopulateProductVariants();
	}

	private void RemoveProduct(ProductDataModel product)
	{
		m_products.Remove(product);
		if (HasTestData)
		{
			m_productsFromTestData.Remove(product);
		}
		foreach (ProductTierDataModel tier in m_tiers)
		{
			foreach (ShopBrowserButtonDataModel item in tier.BrowserButtons.Where((ShopBrowserButtonDataModel b) => b.DisplayProduct == product))
			{
				tier.BrowserButtons.Remove(item);
			}
			foreach (ShopBrowserButtonDataModel browserButton in tier.BrowserButtons)
			{
				browserButton.DisplayProduct.Variants.Remove(product);
				m_tiersChangeCount++;
			}
		}
		if (m_virtualCurrencyProduct != null)
		{
			if (m_virtualCurrencyProduct == product)
			{
				m_virtualCurrencyProduct = null;
			}
			else
			{
				m_virtualCurrencyProduct.Variants.Remove(product);
			}
		}
		if (m_boosterCurrencyProduct != null)
		{
			if (m_boosterCurrencyProduct == product)
			{
				m_boosterCurrencyProduct = null;
			}
			else
			{
				m_boosterCurrencyProduct.Variants.Remove(product);
			}
		}
	}

	private void OnStoreStatusChanged(bool isStoreOpen)
	{
		if (isStoreOpen)
		{
			Processor.QueueJob(new JobDefinition("ProductCatalog.PopulateInitialNetData", Job_PopulateInitialNetData()));
		}
	}

	private bool IsProductAvailabilityStale()
	{
		if (m_hasUpdatedProductStatusOnce)
		{
			if (m_nextCatalogChangeTimeUtc.HasValue)
			{
				return m_nextCatalogChangeTimeUtc.Value < DateTime.UtcNow;
			}
			return false;
		}
		return true;
	}

	private IEnumerator<IAsyncJobResult> Job_PopulateInitialNetData()
	{
		string lastFailReason = string.Empty;
		string reason;
		while (!CatalogUtils.CanUpdateProductStatus(out reason))
		{
			if (reason != lastFailReason)
			{
				Log.Store.PrintWarning("Could not update product status: {0}", reason);
				lastFailReason = reason;
			}
			yield return new WaitForDuration(0.1f);
		}
		StoreManager storeManager = StoreManager.Get();
		PopulateWithNetData(storeManager.AllBundles.ToList(), storeManager.AllGoldCostBoosters.ToList(), storeManager.AllSections.ToList());
		foreach (ProductDataModel productsFromTestDatum in m_productsFromTestData)
		{
			productsFromTestDatum.FormatProductPrices();
		}
		Log.Store.PrintDebug("ProductCatalog initial population complete");
	}

	private void AddNetGoldBoosterProducts(IEnumerable<Network.GoldCostBooster> netGoldBoosters)
	{
		foreach (Network.GoldCostBooster netGoldBooster in netGoldBoosters)
		{
			ProductDataModel productDataModel = CatalogUtils.NetGoldCostBoosterToProduct(netGoldBooster);
			if (productDataModel != null)
			{
				m_products.Add(productDataModel);
			}
		}
	}

	private void AddNetBundleProducts(IEnumerable<Network.Bundle> netBundles)
	{
		foreach (Network.Bundle netBundle in netBundles)
		{
			ProductDataModel productDataModel = (netBundle.PMTProductID.HasValue ? GetProductByPmtId(netBundle.PMTProductID.Value) : null);
			if (productDataModel != null)
			{
				if (!m_productsFromTestData.Contains(productDataModel))
				{
					string text = ((netBundle.DisplayName != null) ? netBundle.DisplayName.GetString() : "");
					Log.Store.PrintError("Ignoring Network.Bundle with PMTProductID that is already in use. PMT ID = {0}, Exiting Product Name = {1}, Ignored Product Name = {2}", netBundle.PMTProductID.Value, productDataModel.Name, text);
				}
			}
			else
			{
				ProductDataModel productDataModel2 = ProductFactory.CreateProductDataModel(netBundle);
				if (productDataModel2 != null)
				{
					m_products.Add(productDataModel2);
				}
			}
		}
	}

	private void TryAssignProductToSlot(List<ShopBrowserButtonDataModel> buttons, ProductDataModel product, bool isFiller)
	{
		switch (product.Availability)
		{
		case ProductAvailability.ALREADY_OWNED:
			if (product.Tags.Contains("hide_owned"))
			{
				return;
			}
			break;
		case ProductAvailability.SALE_NOT_ACTIVE:
		{
			Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(product.PmtId);
			if (bundleFromPmtProductId != null)
			{
				ProductAvailabilityRange bundleAvailabilityRange = StoreManager.Get().GetBundleAvailabilityRange(bundleFromPmtProductId);
				string arg = ((bundleAvailabilityRange != null) ? bundleAvailabilityRange.ToString() : "<unknown sale>");
				Log.Store.PrintDebug($"Product will not appear in tier because sale is not active. PMT ID = {product.PmtId}, Name = {product.Name}, range = {arg}");
			}
			return;
		}
		default:
			Log.Store.PrintDebug($"Product will not appear in Store because status is {product.Availability}. PMT ID = {product.PmtId}, Name = {product.Name}");
			return;
		case ProductAvailability.CAN_PURCHASE:
			break;
		}
		ShopBrowserButtonDataModel item = product.ToButton(isFiller);
		buttons.Add(item);
	}

	private void PopulateTiersFromNetSections(IEnumerable<Network.ShopSection> netSections)
	{
		ClearTiers();
		if (!HasData)
		{
			return;
		}
		foreach (Network.ShopSection item in netSections.OrderBy((Network.ShopSection s) => s.SortOrder))
		{
			List<ShopBrowserButtonDataModel> list = new List<ShopBrowserButtonDataModel>();
			IOrderedEnumerable<Network.ShopSection.ProductRef> orderedEnumerable = item.Products.OrderBy((Network.ShopSection.ProductRef p) => p.OrderId);
			List<string> list2 = new List<string>();
			if (!string.IsNullOrEmpty(item.FillerTags))
			{
				list2.AddRange(CatalogUtils.ParseTagsString(item.FillerTags));
			}
			foreach (Network.ShopSection.ProductRef item2 in orderedEnumerable)
			{
				ProductDataModel product = GetProductByPmtId(item2.PmtId);
				if (product == null)
				{
					Log.Store.PrintDebug($"Section [{item.InternalName}] error: Ignoring product that is not known to client: PMT ID = {item2.PmtId}");
					continue;
				}
				bool isFiller = list2.Any((string t) => product.Tags.Contains(t));
				TryAssignProductToSlot(list, product, isFiller);
			}
			if (list.Count == 0)
			{
				Log.Store.Print("Tier [" + item.InternalName + "] is hidden because it has no products");
				continue;
			}
			ProductTierDataModel productTierDataModel = new ProductTierDataModel
			{
				Header = item.Label.GetString(),
				Style = item.Style
			};
			productTierDataModel.BrowserButtons.AddRange(list);
			m_tiers.Add(productTierDataModel);
			m_tierSectionMapping.Add(productTierDataModel, item);
		}
		m_tiersChangeCount++;
	}

	private void PopulateTiers(IEnumerable<Network.ShopSection> sections = null)
	{
		if (m_testDataMode != TestDataMode.ONLY_TEST_DATA && m_testDataMode != TestDataMode.TIER_TEST_DATA && m_hasUpdatedProductStatusOnce)
		{
			PopulateTiersFromNetSections(sections ?? StoreManager.Get().AllSections);
		}
	}

	private void PopulateProductVariants()
	{
		foreach (ProductTierDataModel tier in m_tiers)
		{
			foreach (ShopBrowserButtonDataModel browserButton in tier.BrowserButtons)
			{
				PopulateVariantsForProduct(browserButton.DisplayProduct);
			}
		}
		if (ShopUtils.IsVirtualCurrencyEnabled())
		{
			m_virtualCurrencyProduct = GetPrimaryProductForItemAndPopulateVariants(RewardItemType.RUNESTONES, 0);
			if (m_virtualCurrencyProduct == null)
			{
				Log.Store.PrintError("Failed to find any Runestone products.");
			}
			m_boosterCurrencyProduct = GetPrimaryProductForItemAndPopulateVariants(RewardItemType.ARCANE_ORBS, 0);
			if (m_boosterCurrencyProduct == null)
			{
				Log.Store.PrintError("Failed to find any Arcane Orb products.");
			}
		}
		else
		{
			m_virtualCurrencyProduct = null;
			m_boosterCurrencyProduct = null;
		}
	}

	private void PopulateVariantsForProduct(ProductDataModel product)
	{
		product.Variants.Clear();
		if (product.Items.Count == 1 && !product.Tags.Contains("bundle"))
		{
			RewardItemDataModel rewardItemDataModel = product.Items.First();
			product.Variants.AddRange(GetVariantsByItemType(rewardItemDataModel.ItemType, rewardItemDataModel.ItemId, product.PmtId));
			if (CatalogUtils.ShouldItemTypeBeGrouped(rewardItemDataModel.ItemType))
			{
				product.BuildItemBundleFromVariantGroup();
			}
		}
		else
		{
			product.Variants.Add(product);
		}
	}

	private ProductDataModel GetPrimaryProductForItemAndPopulateVariants(RewardItemType itemType, int itemId)
	{
		List<ProductDataModel> variantsByItemType = GetVariantsByItemType(itemType, itemId, 0L);
		ProductDataModel productDataModel = variantsByItemType.FirstOrDefault();
		if (productDataModel == null)
		{
			return null;
		}
		productDataModel.Variants.Clear();
		productDataModel.Variants.AddRange(variantsByItemType);
		return productDataModel;
	}

	private List<ProductDataModel> GetVariantsByItemType(RewardItemType itemType, int itemId, long productPMTid = 0L)
	{
		List<ProductDataModel> variants = new List<ProductDataModel>();
		foreach (ProductDataModel product in m_products)
		{
			if (CatalogUtils.ShouldItemTypeBeGrouped(itemType))
			{
				RewardItemDataModel rewardItemDataModel = product.Items.First();
				if (rewardItemDataModel != null && rewardItemDataModel.ItemType == itemType)
				{
					variants.Add(product);
				}
			}
			else if (product.Items.Count == 1 && !product.Tags.Contains("bundle"))
			{
				RewardItemDataModel rewardItemDataModel2 = product.Items.First();
				if (rewardItemDataModel2 != null && rewardItemDataModel2.ItemType == itemType && rewardItemDataModel2.ItemId == itemId)
				{
					variants.Add(product);
				}
			}
		}
		SortVariantsOfItemType(itemType, ref variants);
		return variants;
	}

	private void SortVariantsOfItemType(RewardItemType itemType, ref List<ProductDataModel> variants)
	{
		if (CatalogUtils.ShouldItemTypeBeGrouped(itemType))
		{
			variants.Sort(CatalogUtils.CompareProductsByItemSortOrder);
		}
		else
		{
			variants.Sort(CatalogUtils.CompareProductsForSortByQuantity);
		}
	}

	private void SortPrices()
	{
		foreach (ProductDataModel product in m_products)
		{
			List<PriceDataModel> list = product.Prices.ToList();
			list.Sort(CatalogUtils.ComparePricesForSort);
			product.Prices.Clear();
			product.Prices.AddRange(list);
		}
	}

	private void UpdateWarningThreshold(IEnumerable<Network.ShopSection> sections)
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		StoreManager storeManager = StoreManager.Get();
		if (netObject == null || storeManager == null)
		{
			return;
		}
		bool flag = false;
		foreach (Network.ShopSection section in sections)
		{
			foreach (Network.ShopSection.ProductRef product in section.Products)
			{
				ProductDataModel productByPmtId = GetProductByPmtId(product.PmtId);
				if (productByPmtId != null && productByPmtId.Tags.Contains("prepurchase"))
				{
					Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(product.PmtId);
					ProductAvailabilityRange productAvailabilityRange = ((bundleFromPmtProductId != null) ? storeManager.GetBundleAvailabilityRange(bundleFromPmtProductId) : null);
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
			m_rotationWarningThreshold = netObject.Store.BoosterRotatingSoonWarnDaysWithSale;
		}
		else
		{
			m_rotationWarningThreshold = netObject.Store.BoosterRotatingSoonWarnDaysWithoutSale;
		}
	}

	private void UpdateProductFreshness(ProductDataModel product)
	{
		bool flag = product.Tags.Contains("latest_expansion");
		bool flag2 = product.Tags.Contains("new");
		string primaryProductTag = product.GetPrimaryProductTag();
		if (primaryProductTag == "booster")
		{
			BoosterDbId productBoosterId = product.GetProductBoosterId();
			if (productBoosterId == m_latestBoosterId)
			{
				flag = true;
				flag2 = true;
			}
			else if (!flag2)
			{
				BoosterDbfRecord record = GameDbf.Booster.GetRecord((int)productBoosterId);
				flag2 = record != null && record.LatestExpansionOrder == 0;
			}
			bool flag3 = GameUtils.IsBoosterWild(productBoosterId);
			if (!product.Tags.Contains("wild") && flag3)
			{
				product.Tags.Add("wild");
			}
			else if (!product.Tags.Contains("rotating_soon") && !flag3 && GameUtils.IsBoosterRotated(productBoosterId, DateTime.UtcNow.AddDays(m_rotationWarningThreshold)))
			{
				product.Tags.Add("rotating_soon");
			}
		}
		else if (primaryProductTag == "adventure")
		{
			AdventureDbId productAdventureId = product.GetProductAdventureId();
			flag |= productAdventureId == m_latestAdventureId;
			flag2 = flag2 || flag;
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
			flag2 = Options.Get().GetString(Option.LATEST_SEEN_SHOP_PRODUCT_LIST).IndexOf(value) < 0;
		}
		product.SetProductTagPresence("new", flag2);
		product.SetProductTagPresence("latest_expansion", flag);
	}

	private void UpdateNextCatalogChangeTime()
	{
		StoreManager storeManager = StoreManager.Get();
		if (storeManager == null)
		{
			return;
		}
		DateTime utcNow = DateTime.UtcNow;
		if (m_nextCatalogChangeTimeUtc.HasValue && m_nextCatalogChangeTimeUtc.Value <= utcNow)
		{
			return;
		}
		ProductDataModel productDataModel = null;
		ProductAvailabilityRange arg = null;
		foreach (ProductDataModel product in m_products)
		{
			if (product.PmtId == 0L)
			{
				continue;
			}
			Network.Bundle bundleFromPmtProductId = storeManager.GetBundleFromPmtProductId(product.PmtId);
			if (bundleFromPmtProductId == null)
			{
				continue;
			}
			ProductAvailabilityRange bundleAvailabilityRange = storeManager.GetBundleAvailabilityRange(bundleFromPmtProductId);
			if (bundleAvailabilityRange != null && !bundleAvailabilityRange.IsNever)
			{
				if (bundleAvailabilityRange.StartDateTime.HasValue && bundleAvailabilityRange.StartDateTime.Value > utcNow && (!m_nextCatalogChangeTimeUtc.HasValue || bundleAvailabilityRange.StartDateTime.Value < m_nextCatalogChangeTimeUtc.Value))
				{
					m_nextCatalogChangeTimeUtc = bundleAvailabilityRange.StartDateTime.Value;
					productDataModel = product;
					arg = bundleAvailabilityRange;
				}
				if (bundleAvailabilityRange.SoftEndDateTime.HasValue && bundleAvailabilityRange.SoftEndDateTime.Value > utcNow && (!m_nextCatalogChangeTimeUtc.HasValue || bundleAvailabilityRange.SoftEndDateTime.Value < m_nextCatalogChangeTimeUtc.Value))
				{
					m_nextCatalogChangeTimeUtc = bundleAvailabilityRange.SoftEndDateTime.Value;
					productDataModel = product;
					arg = bundleAvailabilityRange;
				}
			}
		}
		if (m_nextCatalogChangeTimeUtc.HasValue)
		{
			Log.Store.PrintDebug($"Next product availability change at {m_nextCatalogChangeTimeUtc.Value.ToLocalTime():g}");
			if (productDataModel != null)
			{
				Log.Store.PrintDebug($"Next product to change availability is PMT ID = {productDataModel.PmtId}, Name = [{productDataModel.Name}], range = {arg}");
			}
		}
		else
		{
			Log.Store.PrintDebug("No known incoming product availability changes");
		}
	}
}
