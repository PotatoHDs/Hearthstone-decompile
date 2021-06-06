using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class ProductPage : MonoBehaviour
{
	protected Widget m_widget;

	protected ProductPageContainer m_container;

	protected Shop m_parentShop;

	private MusicPlaylistType m_musicOverride;

	protected ProductDataModel m_productImmutable;

	protected ProductDataModel m_productMutable;

	protected Dictionary<int, int> m_variantQuantities = new Dictionary<int, int>();

	protected ProductSelectionDataModel m_productSelection = new ProductSelectionDataModel();

	protected AlertPopup.PopupInfo m_preBuyPopupInfo;

	public Widget WidgetComponent => m_widget;

	public ProductDataModel Product => m_productMutable ?? m_productImmutable;

	public ProductSelectionDataModel Selection => m_productSelection;

	[Overridable]
	public string MusicOverride
	{
		get
		{
			return m_musicOverride.ToString();
		}
		set
		{
			MusicPlaylistType musicOverride = MusicPlaylistType.Invalid;
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					object obj = Enum.Parse(typeof(MusicPlaylistType), value, ignoreCase: true);
					if (obj != null)
					{
						musicOverride = (MusicPlaylistType)obj;
					}
				}
				catch (Exception)
				{
					Debug.LogErrorFormat("Invalid playlist name '{0}'", value);
				}
			}
			SetMusicOverride(musicOverride);
		}
	}

	public bool IsOpen { get; private set; }

	public bool IsAnimating { get; set; }

	public event Action OnOpened;

	public event Action OnClosed;

	public event Action OnProductVariantSet;

	protected virtual void Awake()
	{
		m_parentShop = SceneUtils.FindComponentInParents<Shop>(base.gameObject);
		m_container = SceneUtils.FindComponentInParents<ProductPageContainer>(base.gameObject);
		if (m_container != null)
		{
			m_container.RegisterProductPage(this);
		}
	}

	protected virtual void Start()
	{
		m_widget = GetComponent<Widget>();
		m_widget.RegisterEventListener(OnWidgetEvent);
	}

	protected virtual void OnDestroy()
	{
	}

	public void SetMusicOverride(MusicPlaylistType playlist)
	{
		if (m_musicOverride == playlist)
		{
			return;
		}
		m_musicOverride = playlist;
		if (IsOpen && m_container != null)
		{
			if (m_musicOverride == MusicPlaylistType.Invalid)
			{
				m_container.StopMusicOverride();
			}
			else
			{
				m_container.OverrideMusic(m_musicOverride);
			}
		}
	}

	public virtual void SelectVariant(ProductDataModel product)
	{
		product = product ?? ProductFactory.CreateEmptyProductDataModel();
		Log.Store.PrintDebug("Selecting Product PMT ID = {0}, Name = {1}", product.PmtId, product.Name);
		if (m_productSelection.Variant != product)
		{
			m_productSelection.Variant = product;
			m_productSelection.VariantIndex = m_productImmutable.Variants.IndexOf(GetImmutableVariant(product));
			m_productSelection.Quantity = GetVariantQuantityByIndex(m_productSelection.VariantIndex);
		}
		if (m_container != null)
		{
			m_container.Variant = product;
		}
		m_productSelection.MaxQuantity = product.GetMaxBulkPurchaseCount();
		if (m_widget.GetDataModel<ProductSelectionDataModel>() != m_productSelection)
		{
			m_widget.BindDataModel(m_productSelection);
		}
		if (this.OnProductVariantSet != null)
		{
			this.OnProductVariantSet();
		}
	}

	public ProductDataModel GetSelectedVariant()
	{
		return m_productSelection.Variant;
	}

	public ProductDataModel GetVariantByIndex(int index)
	{
		return Product?.Variants.ElementAtOrDefault(index);
	}

	public void SelectVariantByIndex(int index)
	{
		ProductDataModel variantByIndex = GetVariantByIndex(index);
		if (variantByIndex != null)
		{
			SelectVariant(variantByIndex);
			return;
		}
		Log.Store.PrintWarning("SelectVariantByIndex failed. Product missing variant index {0}", index);
	}

	public int GetVariantQuantityByIndex(int index)
	{
		if (m_variantQuantities.TryGetValue(index, out var value))
		{
			return value;
		}
		return 1;
	}

	public bool ShowQuantityPromptForVariant(int variantIndex)
	{
		ProductDataModel variant = GetVariantByIndex(variantIndex);
		if (variant == null)
		{
			Log.Store.PrintError("ShowQuantityPromptForVariant failed. No variant at index {0}.", variantIndex);
			return false;
		}
		if (!variant.ProductSupportsQuantitySelect())
		{
			Log.Store.Print("ShowQuantityPromptForVariant failed. Product {0} [{1}] does not support quantity select.", variant.PmtId, variant.Name);
			return false;
		}
		if (m_parentShop.QuantityPrompt == null)
		{
			Log.Store.PrintError("ShowQuantityPromptForVariant failed. Shop.QuantityPrompt is null.");
			return false;
		}
		m_parentShop.BlockInterface(blocked: true);
		m_parentShop.QuantityPrompt.Show(m_productSelection.MaxQuantity, delegate(int quantity)
		{
			SetVariantQuantityAndUpdateDataModel(variant, quantity);
			m_parentShop.BlockInterface(blocked: false);
		}, delegate
		{
			m_parentShop.BlockInterface(blocked: false);
		});
		return true;
	}

	protected virtual void OnProductSet()
	{
	}

	protected void SetProduct(ProductDataModel product, ProductDataModel variant)
	{
		m_productImmutable = product ?? ProductFactory.CreateEmptyProductDataModel();
		m_productMutable = null;
		m_variantQuantities.Clear();
		BindProductDataModel();
		OnProductSet();
		SelectVariant(variant ?? m_productImmutable);
	}

	protected bool OnNavigateBack()
	{
		if (IsAnimating)
		{
			return false;
		}
		Close();
		return true;
	}

	protected void OnWidgetEvent(string eventName)
	{
		switch (eventName)
		{
		case "SHOP_BUY_WITH_FIRST_CURRENCY":
			TryBuy(0);
			break;
		case "SHOP_BUY_WITH_ALT_CURRENCY":
			TryBuy(1);
			break;
		case "SHOP_SKU_CLICKED_0":
			SelectVariantByIndex(0);
			break;
		case "SHOP_SKU_CLICKED_1":
			SelectVariantByIndex(1);
			break;
		case "SHOP_SKU_CLICKED_2":
			SelectVariantByIndex(2);
			break;
		case "SHOP_SKU_CLICKED_3":
			SelectVariantByIndex(3);
			break;
		case "SHOP_SKU_CLICKED_4":
			SelectVariantByIndex(4);
			break;
		case "SHOP_SKU_CLICKED_5":
			SelectVariantByIndex(5);
			break;
		case "SHOP_SKU_DOUBLE_CLICKED_0":
			ShowQuantityPromptForVariant(0);
			break;
		}
	}

	public virtual void Open()
	{
		if (IsOpen)
		{
			return;
		}
		IsOpen = true;
		if (m_container != null)
		{
			SetProduct(m_container.Product, m_container.Variant);
			if (m_musicOverride != 0)
			{
				m_container.OverrideMusic(m_musicOverride);
			}
		}
		Navigation.Push(OnNavigateBack);
		StartCoroutine(OpenWhenReadyRoutine());
	}

	public virtual void Close()
	{
		if (IsOpen)
		{
			if (m_parentShop != null && m_parentShop.QuantityPrompt.IsShown())
			{
				m_parentShop.QuantityPrompt.Cancel();
			}
			IsOpen = false;
			Navigation.RemoveHandler(OnNavigateBack);
			m_widget.TriggerEvent("CLOSED");
			StopCoroutine(OpenWhenReadyRoutine());
			if (this.OnClosed != null)
			{
				this.OnClosed();
			}
		}
	}

	protected ProductDataModel GetImmutableVariant(ProductDataModel variant)
	{
		if (Product == variant)
		{
			return m_productImmutable;
		}
		if (m_productImmutable.Variants.Contains(variant))
		{
			return variant;
		}
		if (m_productMutable != null)
		{
			int num = m_productMutable.Variants.IndexOf(variant);
			if (num >= 0)
			{
				return m_productImmutable.Variants.ElementAtOrDefault(num);
			}
		}
		return null;
	}

	protected virtual void TryBuy(int priceOption)
	{
		if (Product == null)
		{
			Log.Store.PrintError("TryBuy failed where no Product is bound to ProductPage");
			return;
		}
		ProductDataModel selectedVariant = GetSelectedVariant();
		if (selectedVariant == null)
		{
			Log.Store.PrintError("Attempted to purchase, but no selected variant found.");
			return;
		}
		if (!ValidateMutableProduct())
		{
			Log.Store.PrintError("Attempted to purchase, but mutable product mismatches immutable product on ProductPage. PMT ID = {0}, Name = {1}", selectedVariant.PmtId, selectedVariant.Name);
			return;
		}
		ProductDataModel immuatableSelectedProduct = GetImmutableVariant(selectedVariant);
		if (immuatableSelectedProduct == null)
		{
			Log.Store.PrintError("Attempted to purchase but failed to get immutable version of product. PMT ID = {0}, Name = {1}", selectedVariant.PmtId, selectedVariant.Name);
			return;
		}
		int quantity = 1;
		if (immuatableSelectedProduct != selectedVariant)
		{
			int num = m_productImmutable.Variants.IndexOf(immuatableSelectedProduct);
			if (num < 0)
			{
				Log.Store.PrintError("Attempted to purchase but failed to get index of product. PMT ID = {0}, Name = {1}", selectedVariant.PmtId, selectedVariant.Name);
				return;
			}
			quantity = GetVariantQuantityByIndex(num);
			if (quantity < 1)
			{
				Log.Store.PrintError("Attempted to purchase, but selected product quantity is invalid. PMT ID = {0}, Name = {1}, Quantity = {2}", selectedVariant.PmtId, selectedVariant.Name, quantity);
				return;
			}
		}
		if (immuatableSelectedProduct.Items.Count == 0)
		{
			Log.Store.PrintError("Attempted to purchase, but product is empty. PMT ID = {0}, Name = {1}", immuatableSelectedProduct.PmtId, immuatableSelectedProduct.Name);
			return;
		}
		if (priceOption < 0 || priceOption >= immuatableSelectedProduct.Prices.Count)
		{
			Log.Store.PrintError("Attempted to purchase, but price index {0} is out of bounds. Num Prices = {1}, PMT ID = {2}, Name = {3}", priceOption, immuatableSelectedProduct.Prices.Count, immuatableSelectedProduct.PmtId, immuatableSelectedProduct.Name);
			return;
		}
		PriceDataModel price = immuatableSelectedProduct.Prices[priceOption];
		if (price == null)
		{
			Log.Store.PrintError("Attempted to purchase, but PriceDataModel is null at index {0}. PMT ID = {1}, Name = {2}", priceOption, immuatableSelectedProduct.PmtId, immuatableSelectedProduct.Name);
			return;
		}
		if (m_preBuyPopupInfo == null)
		{
			Shop.Get().AttemptToPurchaseProduct(immuatableSelectedProduct, price, quantity);
			return;
		}
		m_preBuyPopupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM || response == AlertPopup.Response.OK)
			{
				Shop.Get().AttemptToPurchaseProduct(immuatableSelectedProduct, price, quantity);
			}
		};
		DialogManager.Get().ShowPopup(m_preBuyPopupInfo);
	}

	private void CreateMutableProduct()
	{
		m_productMutable = m_productImmutable.CloneDataModel();
		m_productMutable.Variants = new DataModelList<ProductDataModel>();
		m_productMutable.Variants.AddRange(m_productImmutable.Variants.Select((ProductDataModel v) => CreateMutableVariant(v)));
		m_variantQuantities.Clear();
	}

	private ProductDataModel CreateMutableVariant(ProductDataModel immutableVariant)
	{
		ProductDataModel productDataModel = immutableVariant.CloneDataModel();
		productDataModel.Items = new DataModelList<RewardItemDataModel>();
		productDataModel.Items.AddRange(immutableVariant.Items.Select((RewardItemDataModel i) => i.CloneDataModel()));
		productDataModel.Prices = new DataModelList<PriceDataModel>();
		productDataModel.Prices.AddRange(immutableVariant.Prices.Select((PriceDataModel p) => p.CloneDataModel()));
		return productDataModel;
	}

	private bool ValidateMutableProduct()
	{
		if (m_productMutable != null)
		{
			if (m_productImmutable == null)
			{
				Log.Store.PrintError("ProductPage has a m_productMutable but no m_productImmutable. Mutable Product PMT ID = {0}, Name = {1}", m_productMutable.PmtId, m_productMutable.Name);
				return false;
			}
			if (m_productMutable.PmtId != m_productImmutable.PmtId)
			{
				Log.Store.PrintError("ProductPage Mutable and Immutable products have mismatching PMT id's. Mutable Product PMT ID = {0}, Name = {1}", m_productMutable.PmtId, m_productMutable.Name);
				return false;
			}
			if (m_productMutable.Variants.Count != m_productImmutable.Variants.Count)
			{
				Log.Store.PrintError("ProductPage Mutable and Immutable products have mismatching variant counts. Mutable Product PMT ID = {0}, Name = {1}", m_productMutable.PmtId, m_productMutable.Name);
				return false;
			}
			for (int i = 0; i < m_productMutable.Variants.Count; i++)
			{
				if (m_productMutable.Variants.ElementAt(i).PmtId != m_productImmutable.Variants.ElementAt(i).PmtId)
				{
					Log.Store.PrintError("ProductPage Mutable and Immutable products have mismatching variant. Mutable Product PMT ID = {0}, Name = {1}", m_productMutable.PmtId, m_productMutable.Name);
					return false;
				}
			}
		}
		return true;
	}

	protected void SetVariantQuantityAndUpdateDataModel(ProductDataModel variant, int quantity)
	{
		if (variant == null)
		{
			Log.Store.PrintError("Cannot set product quantity. variant is null.");
			return;
		}
		if (!ValidateMutableProduct())
		{
			Log.Store.PrintError("Cannot set product quantity. ProductPage has an invalid mutable product.");
			return;
		}
		ProductDataModel immutableVariant = GetImmutableVariant(variant);
		if (immutableVariant == null)
		{
			Log.Store.PrintError("Cannot set product quantity. No matching immutable variant found. PMT ID = {0}, Name = {1}.", variant.PmtId, variant.Name);
			return;
		}
		if (quantity < 1 || quantity > m_productSelection.MaxQuantity)
		{
			Log.Store.PrintError("Cannot set product quantity. Invalid input {0}", quantity);
			return;
		}
		int num = m_productImmutable.Variants.IndexOf(immutableVariant);
		if (num < 0)
		{
			Log.Store.PrintError("Cannot set product quantity. Variant not found in product. PMT ID = {0}, Name = {1}.", variant.PmtId, variant.Name);
		}
		else if (GetVariantQuantityByIndex(num) == quantity)
		{
			Log.Store.Print("SetVariantQuantityAndUpdateDataModel value matches current quantity. Quantity = {0}, ", quantity);
		}
		else if (!immutableVariant.ProductSupportsQuantitySelect())
		{
			Log.Store.PrintError("Cannot set product quantity. Product does not support variable quantity. PMT ID = {0}, Name = {1}", immutableVariant.PmtId, immutableVariant.Name);
		}
		else
		{
			if (m_productMutable == null && quantity == 1)
			{
				return;
			}
			if (m_productMutable == null)
			{
				CreateMutableProduct();
			}
			m_variantQuantities[num] = quantity;
			m_productSelection.Quantity = quantity;
			ProductDataModel productDataModel = m_productMutable.Variants.ElementAt(num);
			for (int i = 0; i < productDataModel.Items.Count; i++)
			{
				RewardItemDataModel rewardItemDataModel = immutableVariant.Items.ElementAtOrDefault(i);
				RewardItemDataModel rewardItemDataModel2 = productDataModel.Items.ElementAtOrDefault(i);
				if (rewardItemDataModel != null && rewardItemDataModel2 != null)
				{
					rewardItemDataModel2.Quantity = rewardItemDataModel.Quantity * quantity;
					RewardUtils.InitializeRewardItemDataModelForShop(rewardItemDataModel2);
					continue;
				}
				Log.Store.PrintError("Error modifying product item {0}, where immutable product = [{1}], mutable product = [{2}]", i, immutableVariant.Name, productDataModel.Name);
			}
			for (int j = 0; j < productDataModel.Prices.Count; j++)
			{
				PriceDataModel priceDataModel = immutableVariant.Prices.ElementAtOrDefault(j);
				PriceDataModel priceDataModel2 = productDataModel.Prices.ElementAtOrDefault(j);
				if (priceDataModel != null && priceDataModel2 != null && priceDataModel.Currency == priceDataModel2.Currency)
				{
					priceDataModel2.Amount = priceDataModel.Amount * (float)quantity;
					continue;
				}
				Log.Store.PrintError("Error modifying product price {0}, where immutable product = [{1}], mutable product = [{2}]", j, immutableVariant.Name, productDataModel.Name);
			}
			productDataModel.FormatProductPrices();
			productDataModel.SetupProductStrings();
			BindProductDataModel();
			SelectVariant(productDataModel);
		}
	}

	protected void BindProductDataModel()
	{
		ProductDataModel product = Product;
		m_widget.BindDataModel(product);
	}

	private IEnumerator OpenWhenReadyRoutine()
	{
		while (m_widget.IsChangingStates)
		{
			yield return null;
		}
		m_widget.TriggerEvent("OPEN");
		if (this.OnOpened != null)
		{
			this.OnOpened();
		}
	}
}
