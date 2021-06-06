using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006A8 RID: 1704
public class CurrencyConversionPage : ProductPage
{
	// Token: 0x06005EFF RID: 24319 RVA: 0x001EE488 File Offset: 0x001EC688
	protected override void Start()
	{
		base.Start();
		this.SetupIncrementerButton(this.m_buttonIncrease, 1);
		this.SetupIncrementerButton(this.m_buttonDecrease, -1);
		if (this.m_slider != null)
		{
			this.m_slider.SetUpdateHandler(new ScrollbarControl.UpdateHandler(this.OnSliderUpdated));
			this.m_slider.SetFinishHandler(new ScrollbarControl.FinishHandler(this.OnSliderFinished));
		}
		Shop shop = Shop.Get();
		if (shop != null)
		{
			shop.CurrencyBalanceChanged += this.HandleCurrencyBalanceChanged;
		}
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.HandleSuccessfulPurchaseAck));
	}

	// Token: 0x06005F00 RID: 24320 RVA: 0x001EE528 File Offset: 0x001EC728
	protected override void OnDestroy()
	{
		base.OnDestroy();
		Shop shop = Shop.Get();
		if (shop != null)
		{
			shop.CurrencyBalanceChanged -= this.HandleCurrencyBalanceChanged;
		}
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.HandleSuccessfulPurchaseAck));
	}

	// Token: 0x06005F01 RID: 24321 RVA: 0x001EE572 File Offset: 0x001EC772
	public void OpenToSKU(float desiredAmount)
	{
		this.Open();
		if (this.m_baseQuantity > 0f)
		{
			this.m_selectedQuantity = this.ClampSelection(Mathf.CeilToInt(desiredAmount / this.m_baseQuantity));
			this.UpdateQuantity();
		}
	}

	// Token: 0x06005F02 RID: 24322 RVA: 0x001EE5A8 File Offset: 0x001EC7A8
	public override void Open()
	{
		base.Open();
		if (this.m_productImmutable == null)
		{
			ProductDataModel dataModel = this.m_widget.GetDataModel<ProductDataModel>();
			base.SetProduct(dataModel, dataModel);
		}
		RewardItemDataModel currencyItem = this.GetCurrencyItem();
		this.m_baseQuantity = ((currencyItem != null) ? currencyItem.Currency.Amount : 0f);
		this.UpdateConstraints();
	}

	// Token: 0x06005F03 RID: 24323 RVA: 0x001EE600 File Offset: 0x001EC800
	private void OnSliderUpdated(float val)
	{
		int num = Mathf.RoundToInt((float)this.m_sliderRange.start + val * (float)this.m_sliderRange.length);
		if (this.m_selectedQuantity != num)
		{
			this.m_selectedQuantity = num;
			this.UpdateModel();
		}
	}

	// Token: 0x06005F04 RID: 24324 RVA: 0x001EE644 File Offset: 0x001EC844
	private void OnSliderFinished()
	{
		this.UpdateSlider();
	}

	// Token: 0x06005F05 RID: 24325 RVA: 0x001EE64C File Offset: 0x001EC84C
	private void HandleCurrencyBalanceChanged(CurrencyBalanceChangedEventArgs args)
	{
		if (!base.IsOpen)
		{
			return;
		}
		RewardItemDataModel currencyItem = this.GetCurrencyItem();
		PriceDataModel price = this.GetPrice();
		if ((currencyItem != null && args.Currency == currencyItem.Currency.Currency) || (price != null && args.Currency == price.Currency))
		{
			this.UpdateConstraints();
		}
	}

	// Token: 0x06005F06 RID: 24326 RVA: 0x001EE69D File Offset: 0x001EC89D
	private void HandleSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		this.Close();
	}

	// Token: 0x06005F07 RID: 24327 RVA: 0x001EE6A8 File Offset: 0x001EC8A8
	private RewardItemDataModel GetCurrencyItem()
	{
		ProductDataModel productImmutable = this.m_productImmutable;
		if (productImmutable == null)
		{
			Log.Store.PrintError("No currency conversion product set", Array.Empty<object>());
			return null;
		}
		RewardItemDataModel rewardItemDataModel = productImmutable.Items.FirstOrDefault((RewardItemDataModel i) => i.Currency != null);
		if (rewardItemDataModel == null || rewardItemDataModel.Currency == null || rewardItemDataModel.Currency.Amount == 0f)
		{
			Log.Store.PrintError("No currency found on product {0}", new object[]
			{
				productImmutable.Name
			});
			return null;
		}
		return rewardItemDataModel;
	}

	// Token: 0x06005F08 RID: 24328 RVA: 0x001EE73E File Offset: 0x001EC93E
	private PriceDataModel GetPrice()
	{
		if (this.m_productImmutable == null)
		{
			return null;
		}
		return this.m_productImmutable.Prices.FirstOrDefault<PriceDataModel>();
	}

	// Token: 0x06005F09 RID: 24329 RVA: 0x001EE75C File Offset: 0x001EC95C
	private void SetupIncrementerButton(PegUIElement ui, int increment)
	{
		if (ui == null)
		{
			return;
		}
		ui.AddEventListener(UIEventType.PRESS, delegate(UIEvent _)
		{
			this.IncrementQuantity(increment);
		});
		ui.AddEventListener(UIEventType.HOLD, delegate(UIEvent _)
		{
			this.IncrementQuantity(increment);
		});
	}

	// Token: 0x06005F0A RID: 24330 RVA: 0x001EE7B0 File Offset: 0x001EC9B0
	private void IncrementQuantity(int delta)
	{
		this.m_selectedQuantity = this.ClampSelection(this.m_selectedQuantity + delta);
		this.UpdateQuantity();
	}

	// Token: 0x06005F0B RID: 24331 RVA: 0x001EE7CC File Offset: 0x001EC9CC
	private void UpdateConstraints()
	{
		ProductDataModel productImmutable = this.m_productImmutable;
		if (productImmutable == null)
		{
			Log.Store.PrintError("Unable to update VC conversion constraints; no product set", Array.Empty<object>());
			return;
		}
		PriceDataModel price = this.GetPrice();
		if (price == null || price.Amount == 0f)
		{
			Log.Store.PrintError("No price on currency product {0}", new object[]
			{
				productImmutable.Name
			});
			return;
		}
		long cachedBalance = ShopUtils.GetCachedBalance(price.Currency);
		this.m_productSelection.MaxQuantity = productImmutable.GetMaxBulkPurchaseCount();
		this.m_maxAffordable = Math.Min(Mathf.FloorToInt((float)cachedBalance / price.Amount), this.m_productSelection.MaxQuantity);
		this.m_sliderRange.start = Math.Min(1, this.m_maxAffordable);
		this.m_sliderRange.length = this.m_maxAffordable - this.m_sliderRange.start;
		this.m_selectedQuantity = this.ClampSelection(this.m_selectedQuantity);
		this.m_widget.TriggerEvent((this.m_maxAffordable > 0) ? this.m_affordableEventName : this.m_unaffordableEventName, default(Widget.TriggerEventParameters));
		this.UpdateQuantity();
	}

	// Token: 0x06005F0C RID: 24332 RVA: 0x001EE8E7 File Offset: 0x001ECAE7
	private void UpdateQuantity()
	{
		this.UpdateModel();
		this.UpdateSlider();
	}

	// Token: 0x06005F0D RID: 24333 RVA: 0x001EE8F5 File Offset: 0x001ECAF5
	private void UpdateModel()
	{
		base.SetVariantQuantityAndUpdateDataModel(this.m_productImmutable, this.m_selectedQuantity);
		base.Selection.MaxQuantity = this.m_maxAffordable;
	}

	// Token: 0x06005F0E RID: 24334 RVA: 0x001EE91C File Offset: 0x001ECB1C
	private void UpdateSlider()
	{
		if (this.m_slider != null)
		{
			if (this.m_sliderRange.length > 0)
			{
				this.m_slider.SetValue((float)(this.m_selectedQuantity - this.m_sliderRange.start) / (float)this.m_sliderRange.length);
				return;
			}
			this.m_slider.SetValue((float)((this.m_selectedQuantity == 0) ? 0 : 1));
		}
	}

	// Token: 0x06005F0F RID: 24335 RVA: 0x001EE989 File Offset: 0x001ECB89
	private int ClampSelection(int amount)
	{
		return Math.Max(1, Mathf.Clamp(amount, this.m_sliderRange.start, this.m_sliderRange.end));
	}

	// Token: 0x04005014 RID: 20500
	[SerializeField]
	private PegUIElement m_buttonIncrease;

	// Token: 0x04005015 RID: 20501
	[SerializeField]
	private PegUIElement m_buttonDecrease;

	// Token: 0x04005016 RID: 20502
	[SerializeField]
	private ScrollbarControl m_slider;

	// Token: 0x04005017 RID: 20503
	[Tooltip("Widget event when the player can afford to convert")]
	[SerializeField]
	private string m_affordableEventName = "AFFORDABLE";

	// Token: 0x04005018 RID: 20504
	[Tooltip("Widget event when the player cannot afford conversion")]
	[SerializeField]
	private string m_unaffordableEventName = "UNAFFORDABLE";

	// Token: 0x04005019 RID: 20505
	private const int MINIMUM_SELECTION = 1;

	// Token: 0x0400501A RID: 20506
	private float m_baseQuantity;

	// Token: 0x0400501B RID: 20507
	private int m_selectedQuantity = 1;

	// Token: 0x0400501C RID: 20508
	private int m_maxAffordable;

	// Token: 0x0400501D RID: 20509
	private RangeInt m_sliderRange;
}
