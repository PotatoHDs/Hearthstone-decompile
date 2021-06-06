using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006B0 RID: 1712
public class ProductPage : MonoBehaviour
{
	// Token: 0x06005F7F RID: 24447 RVA: 0x001F209F File Offset: 0x001F029F
	protected virtual void Awake()
	{
		this.m_parentShop = SceneUtils.FindComponentInParents<Shop>(base.gameObject);
		this.m_container = SceneUtils.FindComponentInParents<ProductPageContainer>(base.gameObject);
		if (this.m_container != null)
		{
			this.m_container.RegisterProductPage(this);
		}
	}

	// Token: 0x06005F80 RID: 24448 RVA: 0x001F20DD File Offset: 0x001F02DD
	protected virtual void Start()
	{
		this.m_widget = base.GetComponent<Widget>();
		this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.OnWidgetEvent));
	}

	// Token: 0x06005F81 RID: 24449 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnDestroy()
	{
	}

	// Token: 0x1400003C RID: 60
	// (add) Token: 0x06005F82 RID: 24450 RVA: 0x001F2104 File Offset: 0x001F0304
	// (remove) Token: 0x06005F83 RID: 24451 RVA: 0x001F213C File Offset: 0x001F033C
	public event Action OnOpened;

	// Token: 0x1400003D RID: 61
	// (add) Token: 0x06005F84 RID: 24452 RVA: 0x001F2174 File Offset: 0x001F0374
	// (remove) Token: 0x06005F85 RID: 24453 RVA: 0x001F21AC File Offset: 0x001F03AC
	public event Action OnClosed;

	// Token: 0x1400003E RID: 62
	// (add) Token: 0x06005F86 RID: 24454 RVA: 0x001F21E4 File Offset: 0x001F03E4
	// (remove) Token: 0x06005F87 RID: 24455 RVA: 0x001F221C File Offset: 0x001F041C
	public event Action OnProductVariantSet;

	// Token: 0x170005B4 RID: 1460
	// (get) Token: 0x06005F88 RID: 24456 RVA: 0x001F2251 File Offset: 0x001F0451
	public Widget WidgetComponent
	{
		get
		{
			return this.m_widget;
		}
	}

	// Token: 0x170005B5 RID: 1461
	// (get) Token: 0x06005F89 RID: 24457 RVA: 0x001F2259 File Offset: 0x001F0459
	public ProductDataModel Product
	{
		get
		{
			return this.m_productMutable ?? this.m_productImmutable;
		}
	}

	// Token: 0x170005B6 RID: 1462
	// (get) Token: 0x06005F8A RID: 24458 RVA: 0x001F226B File Offset: 0x001F046B
	public ProductSelectionDataModel Selection
	{
		get
		{
			return this.m_productSelection;
		}
	}

	// Token: 0x170005B7 RID: 1463
	// (get) Token: 0x06005F8B RID: 24459 RVA: 0x001F2273 File Offset: 0x001F0473
	// (set) Token: 0x06005F8C RID: 24460 RVA: 0x001F2288 File Offset: 0x001F0488
	[Overridable]
	public string MusicOverride
	{
		get
		{
			return this.m_musicOverride.ToString();
		}
		set
		{
			MusicPlaylistType musicOverride = MusicPlaylistType.Invalid;
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					object obj = Enum.Parse(typeof(MusicPlaylistType), value, true);
					if (obj != null)
					{
						musicOverride = (MusicPlaylistType)obj;
					}
				}
				catch (Exception)
				{
					Debug.LogErrorFormat("Invalid playlist name '{0}'", new object[]
					{
						value
					});
				}
			}
			this.SetMusicOverride(musicOverride);
		}
	}

	// Token: 0x06005F8D RID: 24461 RVA: 0x001F22EC File Offset: 0x001F04EC
	public void SetMusicOverride(MusicPlaylistType playlist)
	{
		if (this.m_musicOverride == playlist)
		{
			return;
		}
		this.m_musicOverride = playlist;
		if (this.IsOpen && this.m_container != null)
		{
			if (this.m_musicOverride == MusicPlaylistType.Invalid)
			{
				this.m_container.StopMusicOverride();
				return;
			}
			this.m_container.OverrideMusic(this.m_musicOverride);
		}
	}

	// Token: 0x06005F8E RID: 24462 RVA: 0x001F2348 File Offset: 0x001F0548
	public virtual void SelectVariant(ProductDataModel product)
	{
		product = (product ?? ProductFactory.CreateEmptyProductDataModel());
		Log.Store.PrintDebug("Selecting Product PMT ID = {0}, Name = {1}", new object[]
		{
			product.PmtId,
			product.Name
		});
		if (this.m_productSelection.Variant != product)
		{
			this.m_productSelection.Variant = product;
			this.m_productSelection.VariantIndex = this.m_productImmutable.Variants.IndexOf(this.GetImmutableVariant(product));
			this.m_productSelection.Quantity = this.GetVariantQuantityByIndex(this.m_productSelection.VariantIndex);
		}
		if (this.m_container != null)
		{
			this.m_container.Variant = product;
		}
		this.m_productSelection.MaxQuantity = product.GetMaxBulkPurchaseCount();
		if (this.m_widget.GetDataModel<ProductSelectionDataModel>() != this.m_productSelection)
		{
			this.m_widget.BindDataModel(this.m_productSelection, false);
		}
		if (this.OnProductVariantSet != null)
		{
			this.OnProductVariantSet();
		}
	}

	// Token: 0x06005F8F RID: 24463 RVA: 0x001F2448 File Offset: 0x001F0648
	public ProductDataModel GetSelectedVariant()
	{
		return this.m_productSelection.Variant;
	}

	// Token: 0x06005F90 RID: 24464 RVA: 0x001F2458 File Offset: 0x001F0658
	public ProductDataModel GetVariantByIndex(int index)
	{
		ProductDataModel product = this.Product;
		if (product == null)
		{
			return null;
		}
		return product.Variants.ElementAtOrDefault(index);
	}

	// Token: 0x06005F91 RID: 24465 RVA: 0x001F2480 File Offset: 0x001F0680
	public void SelectVariantByIndex(int index)
	{
		ProductDataModel variantByIndex = this.GetVariantByIndex(index);
		if (variantByIndex != null)
		{
			this.SelectVariant(variantByIndex);
			return;
		}
		Log.Store.PrintWarning("SelectVariantByIndex failed. Product missing variant index {0}", new object[]
		{
			index
		});
	}

	// Token: 0x06005F92 RID: 24466 RVA: 0x001F24C0 File Offset: 0x001F06C0
	public int GetVariantQuantityByIndex(int index)
	{
		int result;
		if (this.m_variantQuantities.TryGetValue(index, out result))
		{
			return result;
		}
		return 1;
	}

	// Token: 0x06005F93 RID: 24467 RVA: 0x001F24E0 File Offset: 0x001F06E0
	public bool ShowQuantityPromptForVariant(int variantIndex)
	{
		ProductDataModel variant = this.GetVariantByIndex(variantIndex);
		if (variant == null)
		{
			Log.Store.PrintError("ShowQuantityPromptForVariant failed. No variant at index {0}.", new object[]
			{
				variantIndex
			});
			return false;
		}
		if (!variant.ProductSupportsQuantitySelect())
		{
			Log.Store.Print("ShowQuantityPromptForVariant failed. Product {0} [{1}] does not support quantity select.", new object[]
			{
				variant.PmtId,
				variant.Name
			});
			return false;
		}
		if (this.m_parentShop.QuantityPrompt == null)
		{
			Log.Store.PrintError("ShowQuantityPromptForVariant failed. Shop.QuantityPrompt is null.", Array.Empty<object>());
			return false;
		}
		this.m_parentShop.BlockInterface(true);
		this.m_parentShop.QuantityPrompt.Show(this.m_productSelection.MaxQuantity, delegate(int quantity)
		{
			this.SetVariantQuantityAndUpdateDataModel(variant, quantity);
			this.m_parentShop.BlockInterface(false);
		}, delegate()
		{
			this.m_parentShop.BlockInterface(false);
		});
		return true;
	}

	// Token: 0x06005F94 RID: 24468 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void OnProductSet()
	{
	}

	// Token: 0x06005F95 RID: 24469 RVA: 0x001F25E0 File Offset: 0x001F07E0
	protected void SetProduct(ProductDataModel product, ProductDataModel variant)
	{
		this.m_productImmutable = (product ?? ProductFactory.CreateEmptyProductDataModel());
		this.m_productMutable = null;
		this.m_variantQuantities.Clear();
		this.BindProductDataModel();
		this.OnProductSet();
		this.SelectVariant(variant ?? this.m_productImmutable);
	}

	// Token: 0x06005F96 RID: 24470 RVA: 0x001F262C File Offset: 0x001F082C
	protected bool OnNavigateBack()
	{
		if (this.IsAnimating)
		{
			return false;
		}
		this.Close();
		return true;
	}

	// Token: 0x06005F97 RID: 24471 RVA: 0x001F2640 File Offset: 0x001F0840
	protected void OnWidgetEvent(string eventName)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(eventName);
		if (num <= 2770982715U)
		{
			if (num <= 1318030528U)
			{
				if (num != 968010480U)
				{
					if (num != 1318030528U)
					{
						return;
					}
					if (!(eventName == "SHOP_BUY_WITH_FIRST_CURRENCY"))
					{
						return;
					}
					this.TryBuy(0);
					return;
				}
				else
				{
					if (!(eventName == "SHOP_SKU_DOUBLE_CLICKED_0"))
					{
						return;
					}
					this.ShowQuantityPromptForVariant(0);
					return;
				}
			}
			else if (num != 2754205096U)
			{
				if (num != 2770982715U)
				{
					return;
				}
				if (!(eventName == "SHOP_SKU_CLICKED_3"))
				{
					return;
				}
				this.SelectVariantByIndex(3);
				return;
			}
			else
			{
				if (!(eventName == "SHOP_SKU_CLICKED_2"))
				{
					return;
				}
				this.SelectVariantByIndex(2);
				return;
			}
		}
		else if (num <= 2804537953U)
		{
			if (num != 2787760334U)
			{
				if (num != 2804537953U)
				{
					return;
				}
				if (!(eventName == "SHOP_SKU_CLICKED_1"))
				{
					return;
				}
				this.SelectVariantByIndex(1);
				return;
			}
			else
			{
				if (!(eventName == "SHOP_SKU_CLICKED_0"))
				{
					return;
				}
				this.SelectVariantByIndex(0);
				return;
			}
		}
		else if (num != 2854870810U)
		{
			if (num != 2871648429U)
			{
				if (num != 3078142591U)
				{
					return;
				}
				if (!(eventName == "SHOP_BUY_WITH_ALT_CURRENCY"))
				{
					return;
				}
				this.TryBuy(1);
				return;
			}
			else
			{
				if (!(eventName == "SHOP_SKU_CLICKED_5"))
				{
					return;
				}
				this.SelectVariantByIndex(5);
				return;
			}
		}
		else
		{
			if (!(eventName == "SHOP_SKU_CLICKED_4"))
			{
				return;
			}
			this.SelectVariantByIndex(4);
			return;
		}
	}

	// Token: 0x170005B8 RID: 1464
	// (get) Token: 0x06005F98 RID: 24472 RVA: 0x001F2781 File Offset: 0x001F0981
	// (set) Token: 0x06005F99 RID: 24473 RVA: 0x001F2789 File Offset: 0x001F0989
	public bool IsOpen { get; private set; }

	// Token: 0x170005B9 RID: 1465
	// (get) Token: 0x06005F9A RID: 24474 RVA: 0x001F2792 File Offset: 0x001F0992
	// (set) Token: 0x06005F9B RID: 24475 RVA: 0x001F279A File Offset: 0x001F099A
	public bool IsAnimating { get; set; }

	// Token: 0x06005F9C RID: 24476 RVA: 0x001F27A4 File Offset: 0x001F09A4
	public virtual void Open()
	{
		if (this.IsOpen)
		{
			return;
		}
		this.IsOpen = true;
		if (this.m_container != null)
		{
			this.SetProduct(this.m_container.Product, this.m_container.Variant);
			if (this.m_musicOverride != MusicPlaylistType.Invalid)
			{
				this.m_container.OverrideMusic(this.m_musicOverride);
			}
		}
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		base.StartCoroutine(this.OpenWhenReadyRoutine());
	}

	// Token: 0x06005F9D RID: 24477 RVA: 0x001F2824 File Offset: 0x001F0A24
	public virtual void Close()
	{
		if (!this.IsOpen)
		{
			return;
		}
		if (this.m_parentShop != null && this.m_parentShop.QuantityPrompt.IsShown())
		{
			this.m_parentShop.QuantityPrompt.Cancel();
		}
		this.IsOpen = false;
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		this.m_widget.TriggerEvent("CLOSED", default(Widget.TriggerEventParameters));
		base.StopCoroutine(this.OpenWhenReadyRoutine());
		if (this.OnClosed != null)
		{
			this.OnClosed();
		}
	}

	// Token: 0x06005F9E RID: 24478 RVA: 0x001F28BC File Offset: 0x001F0ABC
	protected ProductDataModel GetImmutableVariant(ProductDataModel variant)
	{
		if (this.Product == variant)
		{
			return this.m_productImmutable;
		}
		if (this.m_productImmutable.Variants.Contains(variant))
		{
			return variant;
		}
		if (this.m_productMutable != null)
		{
			int num = this.m_productMutable.Variants.IndexOf(variant);
			if (num >= 0)
			{
				return this.m_productImmutable.Variants.ElementAtOrDefault(num);
			}
		}
		return null;
	}

	// Token: 0x06005F9F RID: 24479 RVA: 0x001F2920 File Offset: 0x001F0B20
	protected virtual void TryBuy(int priceOption)
	{
		if (this.Product == null)
		{
			Log.Store.PrintError("TryBuy failed where no Product is bound to ProductPage", Array.Empty<object>());
			return;
		}
		ProductDataModel selectedVariant = this.GetSelectedVariant();
		if (selectedVariant == null)
		{
			Log.Store.PrintError("Attempted to purchase, but no selected variant found.", Array.Empty<object>());
			return;
		}
		if (!this.ValidateMutableProduct())
		{
			Log.Store.PrintError("Attempted to purchase, but mutable product mismatches immutable product on ProductPage. PMT ID = {0}, Name = {1}", new object[]
			{
				selectedVariant.PmtId,
				selectedVariant.Name
			});
			return;
		}
		ProductDataModel immuatableSelectedProduct = this.GetImmutableVariant(selectedVariant);
		if (immuatableSelectedProduct == null)
		{
			Log.Store.PrintError("Attempted to purchase but failed to get immutable version of product. PMT ID = {0}, Name = {1}", new object[]
			{
				selectedVariant.PmtId,
				selectedVariant.Name
			});
			return;
		}
		int quantity = 1;
		if (immuatableSelectedProduct != selectedVariant)
		{
			int num = this.m_productImmutable.Variants.IndexOf(immuatableSelectedProduct);
			if (num < 0)
			{
				Log.Store.PrintError("Attempted to purchase but failed to get index of product. PMT ID = {0}, Name = {1}", new object[]
				{
					selectedVariant.PmtId,
					selectedVariant.Name
				});
				return;
			}
			quantity = this.GetVariantQuantityByIndex(num);
			if (quantity < 1)
			{
				Log.Store.PrintError("Attempted to purchase, but selected product quantity is invalid. PMT ID = {0}, Name = {1}, Quantity = {2}", new object[]
				{
					selectedVariant.PmtId,
					selectedVariant.Name,
					quantity
				});
				return;
			}
		}
		if (immuatableSelectedProduct.Items.Count == 0)
		{
			Log.Store.PrintError("Attempted to purchase, but product is empty. PMT ID = {0}, Name = {1}", new object[]
			{
				immuatableSelectedProduct.PmtId,
				immuatableSelectedProduct.Name
			});
			return;
		}
		if (priceOption < 0 || priceOption >= immuatableSelectedProduct.Prices.Count)
		{
			Log.Store.PrintError("Attempted to purchase, but price index {0} is out of bounds. Num Prices = {1}, PMT ID = {2}, Name = {3}", new object[]
			{
				priceOption,
				immuatableSelectedProduct.Prices.Count,
				immuatableSelectedProduct.PmtId,
				immuatableSelectedProduct.Name
			});
			return;
		}
		PriceDataModel price = immuatableSelectedProduct.Prices[priceOption];
		if (price == null)
		{
			Log.Store.PrintError("Attempted to purchase, but PriceDataModel is null at index {0}. PMT ID = {1}, Name = {2}", new object[]
			{
				priceOption,
				immuatableSelectedProduct.PmtId,
				immuatableSelectedProduct.Name
			});
			return;
		}
		if (this.m_preBuyPopupInfo == null)
		{
			Shop.Get().AttemptToPurchaseProduct(immuatableSelectedProduct, price, quantity);
			return;
		}
		this.m_preBuyPopupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM || response == AlertPopup.Response.OK)
			{
				Shop.Get().AttemptToPurchaseProduct(immuatableSelectedProduct, price, quantity);
			}
		};
		DialogManager.Get().ShowPopup(this.m_preBuyPopupInfo);
	}

	// Token: 0x06005FA0 RID: 24480 RVA: 0x001F2BF8 File Offset: 0x001F0DF8
	private void CreateMutableProduct()
	{
		this.m_productMutable = this.m_productImmutable.CloneDataModel<ProductDataModel>();
		this.m_productMutable.Variants = new DataModelList<ProductDataModel>();
		this.m_productMutable.Variants.AddRange(from v in this.m_productImmutable.Variants
		select this.CreateMutableVariant(v));
		this.m_variantQuantities.Clear();
	}

	// Token: 0x06005FA1 RID: 24481 RVA: 0x001F2C60 File Offset: 0x001F0E60
	private ProductDataModel CreateMutableVariant(ProductDataModel immutableVariant)
	{
		ProductDataModel productDataModel = immutableVariant.CloneDataModel<ProductDataModel>();
		productDataModel.Items = new DataModelList<RewardItemDataModel>();
		productDataModel.Items.AddRange(from i in immutableVariant.Items
		select i.CloneDataModel<RewardItemDataModel>());
		productDataModel.Prices = new DataModelList<PriceDataModel>();
		productDataModel.Prices.AddRange(from p in immutableVariant.Prices
		select p.CloneDataModel<PriceDataModel>());
		return productDataModel;
	}

	// Token: 0x06005FA2 RID: 24482 RVA: 0x001F2CF4 File Offset: 0x001F0EF4
	private bool ValidateMutableProduct()
	{
		if (this.m_productMutable != null)
		{
			if (this.m_productImmutable == null)
			{
				Log.Store.PrintError("ProductPage has a m_productMutable but no m_productImmutable. Mutable Product PMT ID = {0}, Name = {1}", new object[]
				{
					this.m_productMutable.PmtId,
					this.m_productMutable.Name
				});
				return false;
			}
			if (this.m_productMutable.PmtId != this.m_productImmutable.PmtId)
			{
				Log.Store.PrintError("ProductPage Mutable and Immutable products have mismatching PMT id's. Mutable Product PMT ID = {0}, Name = {1}", new object[]
				{
					this.m_productMutable.PmtId,
					this.m_productMutable.Name
				});
				return false;
			}
			if (this.m_productMutable.Variants.Count != this.m_productImmutable.Variants.Count)
			{
				Log.Store.PrintError("ProductPage Mutable and Immutable products have mismatching variant counts. Mutable Product PMT ID = {0}, Name = {1}", new object[]
				{
					this.m_productMutable.PmtId,
					this.m_productMutable.Name
				});
				return false;
			}
			for (int i = 0; i < this.m_productMutable.Variants.Count; i++)
			{
				if (this.m_productMutable.Variants.ElementAt(i).PmtId != this.m_productImmutable.Variants.ElementAt(i).PmtId)
				{
					Log.Store.PrintError("ProductPage Mutable and Immutable products have mismatching variant. Mutable Product PMT ID = {0}, Name = {1}", new object[]
					{
						this.m_productMutable.PmtId,
						this.m_productMutable.Name
					});
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06005FA3 RID: 24483 RVA: 0x001F2E78 File Offset: 0x001F1078
	protected void SetVariantQuantityAndUpdateDataModel(ProductDataModel variant, int quantity)
	{
		if (variant == null)
		{
			Log.Store.PrintError("Cannot set product quantity. variant is null.", Array.Empty<object>());
			return;
		}
		if (!this.ValidateMutableProduct())
		{
			Log.Store.PrintError("Cannot set product quantity. ProductPage has an invalid mutable product.", Array.Empty<object>());
			return;
		}
		ProductDataModel immutableVariant = this.GetImmutableVariant(variant);
		if (immutableVariant == null)
		{
			Log.Store.PrintError("Cannot set product quantity. No matching immutable variant found. PMT ID = {0}, Name = {1}.", new object[]
			{
				variant.PmtId,
				variant.Name
			});
			return;
		}
		if (quantity < 1 || quantity > this.m_productSelection.MaxQuantity)
		{
			Log.Store.PrintError("Cannot set product quantity. Invalid input {0}", new object[]
			{
				quantity
			});
			return;
		}
		int num = this.m_productImmutable.Variants.IndexOf(immutableVariant);
		if (num < 0)
		{
			Log.Store.PrintError("Cannot set product quantity. Variant not found in product. PMT ID = {0}, Name = {1}.", new object[]
			{
				variant.PmtId,
				variant.Name
			});
			return;
		}
		if (this.GetVariantQuantityByIndex(num) == quantity)
		{
			Log.Store.Print("SetVariantQuantityAndUpdateDataModel value matches current quantity. Quantity = {0}, ", new object[]
			{
				quantity
			});
			return;
		}
		if (!immutableVariant.ProductSupportsQuantitySelect())
		{
			Log.Store.PrintError("Cannot set product quantity. Product does not support variable quantity. PMT ID = {0}, Name = {1}", new object[]
			{
				immutableVariant.PmtId,
				immutableVariant.Name
			});
			return;
		}
		if (this.m_productMutable == null && quantity == 1)
		{
			return;
		}
		if (this.m_productMutable == null)
		{
			this.CreateMutableProduct();
		}
		this.m_variantQuantities[num] = quantity;
		this.m_productSelection.Quantity = quantity;
		ProductDataModel productDataModel = this.m_productMutable.Variants.ElementAt(num);
		for (int i = 0; i < productDataModel.Items.Count; i++)
		{
			RewardItemDataModel rewardItemDataModel = immutableVariant.Items.ElementAtOrDefault(i);
			RewardItemDataModel rewardItemDataModel2 = productDataModel.Items.ElementAtOrDefault(i);
			if (rewardItemDataModel != null && rewardItemDataModel2 != null)
			{
				rewardItemDataModel2.Quantity = rewardItemDataModel.Quantity * quantity;
				RewardUtils.InitializeRewardItemDataModelForShop(rewardItemDataModel2, null, null);
			}
			else
			{
				Log.Store.PrintError("Error modifying product item {0}, where immutable product = [{1}], mutable product = [{2}]", new object[]
				{
					i,
					immutableVariant.Name,
					productDataModel.Name
				});
			}
		}
		for (int j = 0; j < productDataModel.Prices.Count; j++)
		{
			PriceDataModel priceDataModel = immutableVariant.Prices.ElementAtOrDefault(j);
			PriceDataModel priceDataModel2 = productDataModel.Prices.ElementAtOrDefault(j);
			if (priceDataModel != null && priceDataModel2 != null && priceDataModel.Currency == priceDataModel2.Currency)
			{
				priceDataModel2.Amount = priceDataModel.Amount * (float)quantity;
			}
			else
			{
				Log.Store.PrintError("Error modifying product price {0}, where immutable product = [{1}], mutable product = [{2}]", new object[]
				{
					j,
					immutableVariant.Name,
					productDataModel.Name
				});
			}
		}
		productDataModel.FormatProductPrices(null);
		productDataModel.SetupProductStrings();
		this.BindProductDataModel();
		this.SelectVariant(productDataModel);
	}

	// Token: 0x06005FA4 RID: 24484 RVA: 0x001F3148 File Offset: 0x001F1348
	protected void BindProductDataModel()
	{
		ProductDataModel product = this.Product;
		this.m_widget.BindDataModel(product, false);
	}

	// Token: 0x06005FA5 RID: 24485 RVA: 0x001F3169 File Offset: 0x001F1369
	private IEnumerator OpenWhenReadyRoutine()
	{
		while (this.m_widget.IsChangingStates)
		{
			yield return null;
		}
		this.m_widget.TriggerEvent("OPEN", default(Widget.TriggerEventParameters));
		if (this.OnOpened != null)
		{
			this.OnOpened();
		}
		yield break;
	}

	// Token: 0x0400503F RID: 20543
	protected Widget m_widget;

	// Token: 0x04005040 RID: 20544
	protected ProductPageContainer m_container;

	// Token: 0x04005041 RID: 20545
	protected Shop m_parentShop;

	// Token: 0x04005042 RID: 20546
	private MusicPlaylistType m_musicOverride;

	// Token: 0x04005043 RID: 20547
	protected ProductDataModel m_productImmutable;

	// Token: 0x04005044 RID: 20548
	protected ProductDataModel m_productMutable;

	// Token: 0x04005045 RID: 20549
	protected Dictionary<int, int> m_variantQuantities = new Dictionary<int, int>();

	// Token: 0x04005046 RID: 20550
	protected ProductSelectionDataModel m_productSelection = new ProductSelectionDataModel();

	// Token: 0x04005047 RID: 20551
	protected AlertPopup.PopupInfo m_preBuyPopupInfo;
}
