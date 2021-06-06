using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200071E RID: 1822
[CustomEditClass]
public abstract class Store : UIBPopup, IStore
{
	// Token: 0x0600656D RID: 25965 RVA: 0x00210980 File Offset: 0x0020EB80
	protected override void Awake()
	{
		this.m_goldButtonInternal = new Store.BuyButtonInternal
		{
			m_button = this.m_buyWithGoldButton,
			m_tooltipZone = this.m_buyWithGoldTooltip,
			m_tooltipTrigger = this.m_buyWithGoldTooltipTrigger,
			m_meshes = this.m_goldButtonMeshes,
			m_enabledMaterial = this.m_enabledGoldButtonMaterial,
			m_disabledMaterial = this.m_disabledGoldButtonMaterial,
			m_toolTipHeadlineStringId = "GLUE_STORE_GOLD_BUTTON_TOOLTIP_HEADLINE",
			m_buyHandler = new UIEvent.Handler(this.BuyWithGold),
			m_getOwnedTooltipStringHandler = new Func<string>(this.GetOwnedTooltipString)
		};
		this.m_buyButtons.Add(this.m_goldButtonInternal);
		this.m_moneyButtonInternal = new Store.BuyButtonInternal
		{
			m_button = this.m_buyWithMoneyButton,
			m_tooltipZone = this.m_buyWithMoneyTooltip,
			m_tooltipTrigger = this.m_buyWithMoneyTooltipTrigger,
			m_meshes = this.m_moneyButtonMeshes,
			m_enabledMaterial = this.m_enabledMoneyButtonMaterial,
			m_disabledMaterial = this.m_disabledMoneyButtonMaterial,
			m_toolTipHeadlineStringId = "GLUE_STORE_MONEY_BUTTON_TOOLTIP_HEADLINE",
			m_buyHandler = new UIEvent.Handler(this.BuyWithMoney),
			m_getOwnedTooltipStringHandler = new Func<string>(this.GetOwnedTooltipString)
		};
		this.m_buyButtons.Add(this.m_moneyButtonInternal);
		this.m_vcButtonInternal = new Store.BuyButtonInternal
		{
			m_button = this.m_buyWithVCButton,
			m_tooltipZone = this.m_buyWithVCTooltip,
			m_tooltipTrigger = this.m_buyWithVCTooltipTrigger,
			m_meshes = this.m_vcButtonMeshes,
			m_enabledMaterial = this.m_enabledVCButtonMaterial,
			m_disabledMaterial = this.m_disabledVCButtonMaterial,
			m_toolTipHeadlineStringId = "GLUE_STORE_VC_BUTTON_TOOLTIP_HEADLINE",
			m_buyHandler = new UIEvent.Handler(this.BuyWithVirtualCurrency),
			m_getOwnedTooltipStringHandler = new Func<string>(this.GetOwnedTooltipString)
		};
		this.m_buyButtons.Add(this.m_vcButtonInternal);
		base.Awake();
		this.m_infoButton.SetText(GameStrings.Get("GLUE_STORE_INFO_BUTTON_TEXT"));
	}

	// Token: 0x0600656E RID: 25966 RVA: 0x00210B68 File Offset: 0x0020ED68
	protected override void Start()
	{
		base.Start();
		foreach (Store.BuyButtonInternal buyButtonInternal in this.m_buyButtons)
		{
			buyButtonInternal.Init();
		}
		this.m_infoButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInfoPressed));
		NetCache.Get().RegisterGoldBalanceListener(new NetCache.DelGoldBalanceListener(this.OnStoreGoldBalanceChanged));
		StoreManager.Get().RegisterSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchase));
		base.StartCoroutine(this.NotifyListenersWhenReady());
		if (!this.m_shown)
		{
			this.Hide();
		}
		if (Shop.Get() != null)
		{
			Shop.Get().CurrencyBalanceChanged += this.OnCurrencyBalanceChangedInternal;
			Shop.Get().OnProductPageChanged += this.OnShopProductPageChanged;
		}
	}

	// Token: 0x0600656F RID: 25967 RVA: 0x00210C58 File Offset: 0x0020EE58
	private void OnStoreGoldBalanceChanged(NetCache.NetCacheGoldBalance balance)
	{
		if (this.IsOpen())
		{
			this.OnGoldBalanceChanged(balance);
		}
	}

	// Token: 0x06006570 RID: 25968 RVA: 0x00210C69 File Offset: 0x0020EE69
	private void OnCurrencyBalanceChangedInternal(CurrencyBalanceChangedEventArgs args)
	{
		if (this.IsOpen())
		{
			this.OnCurrencyBalanceChanged(args);
		}
	}

	// Token: 0x06006571 RID: 25969 RVA: 0x00210C7A File Offset: 0x0020EE7A
	private void OnSuccessfulPurchase(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		if (paymentMethod == PaymentMethod.MONEY && this.IsOpen())
		{
			this.OnMoneySpent();
		}
	}

	// Token: 0x06006572 RID: 25970 RVA: 0x00210C90 File Offset: 0x0020EE90
	protected virtual void OnDestroy()
	{
		if (NetCache.Get() != null)
		{
			NetCache.Get().RemoveGoldBalanceListener(new NetCache.DelGoldBalanceListener(this.OnStoreGoldBalanceChanged));
		}
		if (Shop.Get() != null)
		{
			Shop.Get().CurrencyBalanceChanged -= this.OnCurrencyBalanceChangedInternal;
			Shop.Get().OnProductPageChanged -= this.OnShopProductPageChanged;
		}
		foreach (Store.BuyButtonInternal buyButtonInternal in this.m_buyButtons)
		{
			buyButtonInternal.OnDestroy();
		}
		this.m_buyButtons.Clear();
		this.m_enabledGoldButtonMaterial = null;
		this.m_disabledGoldButtonMaterial = null;
		this.m_enabledMoneyButtonMaterial = null;
		this.m_disabledMoneyButtonMaterial = null;
		this.m_enabledVCButtonMaterial = null;
		this.m_disabledVCButtonMaterial = null;
		if (FullScreenFXMgr.Get() != null && this.m_hasRequestedFullscreenFX)
		{
			this.EnableFullScreenEffects(false);
		}
	}

	// Token: 0x06006573 RID: 25971 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnGoldBalanceChanged(NetCache.NetCacheGoldBalance balance)
	{
	}

	// Token: 0x06006574 RID: 25972 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnMoneySpent()
	{
	}

	// Token: 0x06006575 RID: 25973 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnCurrencyBalanceChanged(CurrencyBalanceChangedEventArgs args)
	{
	}

	// Token: 0x06006576 RID: 25974 RVA: 0x00210D84 File Offset: 0x0020EF84
	public void Show(bool isTotallyFake, bool useOverlayUI, IDataModel dataModel)
	{
		if (dataModel != null)
		{
			WidgetTemplate componentInParent = base.GetComponentInParent<WidgetTemplate>();
			if (componentInParent != null)
			{
				componentInParent.BindDataModel(dataModel, base.gameObject, true, false);
			}
		}
		this.m_useOverlayUI = useOverlayUI;
		base.StartCoroutine(this.ShowWhenReady(isTotallyFake));
	}

	// Token: 0x06006577 RID: 25975 RVA: 0x00210DC9 File Offset: 0x0020EFC9
	public void Open()
	{
		base.StartCoroutine(this.ShowWhenReady(false));
	}

	// Token: 0x06006578 RID: 25976 RVA: 0x00210DD9 File Offset: 0x0020EFD9
	public bool IsOpen()
	{
		return this.IsShown();
	}

	// Token: 0x06006579 RID: 25977 RVA: 0x00210DE1 File Offset: 0x0020EFE1
	public virtual void Close()
	{
		this.Hide();
	}

	// Token: 0x0600657A RID: 25978 RVA: 0x000052EC File Offset: 0x000034EC
	public virtual bool IsReady()
	{
		return true;
	}

	// Token: 0x0600657B RID: 25979 RVA: 0x00210DE9 File Offset: 0x0020EFE9
	public bool IsCovered()
	{
		return this.m_cover.activeSelf;
	}

	// Token: 0x0600657C RID: 25980 RVA: 0x00210DF6 File Offset: 0x0020EFF6
	public void BlockInterface(bool blocked)
	{
		if (this.m_cover != null)
		{
			this.m_cover.SetActive(blocked);
		}
		this.ForceDisableBuyButtons(blocked);
	}

	// Token: 0x0600657D RID: 25981 RVA: 0x00210E19 File Offset: 0x0020F019
	public void EnableClickCatcher(bool enabled)
	{
		this.m_offClicker.gameObject.SetActive(enabled);
	}

	// Token: 0x1400005F RID: 95
	// (add) Token: 0x0600657E RID: 25982 RVA: 0x00210E2C File Offset: 0x0020F02C
	// (remove) Token: 0x0600657F RID: 25983 RVA: 0x00210E64 File Offset: 0x0020F064
	public event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	// Token: 0x14000060 RID: 96
	// (add) Token: 0x06006580 RID: 25984 RVA: 0x00210E9C File Offset: 0x0020F09C
	// (remove) Token: 0x06006581 RID: 25985 RVA: 0x00210ED4 File Offset: 0x0020F0D4
	public event Action OnOpened;

	// Token: 0x14000061 RID: 97
	// (add) Token: 0x06006582 RID: 25986 RVA: 0x00210F0C File Offset: 0x0020F10C
	// (remove) Token: 0x06006583 RID: 25987 RVA: 0x00210F44 File Offset: 0x0020F144
	public event Action<StoreClosedArgs> OnClosed;

	// Token: 0x14000062 RID: 98
	// (add) Token: 0x06006584 RID: 25988 RVA: 0x00210F7C File Offset: 0x0020F17C
	// (remove) Token: 0x06006585 RID: 25989 RVA: 0x00210FB4 File Offset: 0x0020F1B4
	public event Action OnReady;

	// Token: 0x06006586 RID: 25990 RVA: 0x00210FE9 File Offset: 0x0020F1E9
	public bool RegisterInfoListener(Store.InfoCallback callback)
	{
		return this.RegisterInfoListener(callback, null);
	}

	// Token: 0x06006587 RID: 25991 RVA: 0x00210FF4 File Offset: 0x0020F1F4
	public bool RegisterInfoListener(Store.InfoCallback callback, object userData)
	{
		Store.InfoListener infoListener = new Store.InfoListener();
		infoListener.SetCallback(callback);
		infoListener.SetUserData(userData);
		if (this.m_infoListeners.Contains(infoListener))
		{
			return false;
		}
		this.m_infoListeners.Add(infoListener);
		return true;
	}

	// Token: 0x06006588 RID: 25992 RVA: 0x00211032 File Offset: 0x0020F232
	public bool RemoveInfoListener(Store.InfoCallback callback)
	{
		return this.RemoveInfoListener(callback, null);
	}

	// Token: 0x06006589 RID: 25993 RVA: 0x0021103C File Offset: 0x0020F23C
	public bool RemoveInfoListener(Store.InfoCallback callback, object userData)
	{
		Store.InfoListener infoListener = new Store.InfoListener();
		infoListener.SetCallback(callback);
		infoListener.SetUserData(userData);
		return this.m_infoListeners.Remove(infoListener);
	}

	// Token: 0x0600658A RID: 25994 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	public void Unload()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600658B RID: 25995 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void BuyWithGold(UIEvent e)
	{
	}

	// Token: 0x0600658C RID: 25996 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void BuyWithMoney(UIEvent e)
	{
	}

	// Token: 0x0600658D RID: 25997 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void BuyWithVirtualCurrency(UIEvent e)
	{
	}

	// Token: 0x0600658E RID: 25998
	protected abstract void ShowImpl(bool isTotallyFake);

	// Token: 0x0600658F RID: 25999 RVA: 0x00211069 File Offset: 0x0020F269
	protected void FireOpenedEvent()
	{
		if (this.OnOpened != null)
		{
			this.OnOpened();
		}
	}

	// Token: 0x06006590 RID: 26000 RVA: 0x0021107E File Offset: 0x0020F27E
	public void FireExitEvent(bool authorizationBackButtonPressed)
	{
		if (this.OnClosed != null)
		{
			this.OnClosed(new StoreClosedArgs(authorizationBackButtonPressed));
		}
	}

	// Token: 0x06006591 RID: 26001 RVA: 0x00211099 File Offset: 0x0020F299
	protected void EnableFullScreenEffects(bool enable)
	{
		if (FullScreenFXMgr.Get() == null)
		{
			return;
		}
		if (enable)
		{
			this.m_hasRequestedFullscreenFX = true;
			FullScreenFXMgr.Get().StartStandardBlurVignette(1f);
			return;
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(1f, null);
	}

	// Token: 0x06006592 RID: 26002 RVA: 0x002110CD File Offset: 0x0020F2CD
	protected void FireBuyWithMoneyEvent(Network.Bundle bundle, int quantity)
	{
		if (this.OnProductPurchaseAttempt != null)
		{
			this.OnProductPurchaseAttempt(new BuyPmtProductEventArgs(bundle, CurrencyType.REAL_MONEY, quantity));
		}
	}

	// Token: 0x06006593 RID: 26003 RVA: 0x002110EA File Offset: 0x0020F2EA
	protected void FireBuyWithGoldEventGTAPP(Network.Bundle bundle, int quantity)
	{
		if (this.OnProductPurchaseAttempt != null)
		{
			this.OnProductPurchaseAttempt(new BuyPmtProductEventArgs(bundle, CurrencyType.GOLD, quantity));
		}
	}

	// Token: 0x06006594 RID: 26004 RVA: 0x00211107 File Offset: 0x0020F307
	protected void FireBuyWithGoldEventNoGTAPP(NoGTAPPTransactionData noGTAPPTransactionData)
	{
		if (this.OnProductPurchaseAttempt != null)
		{
			this.OnProductPurchaseAttempt(new BuyNoGTAPPEventArgs(noGTAPPTransactionData));
		}
	}

	// Token: 0x06006595 RID: 26005 RVA: 0x00211124 File Offset: 0x0020F324
	protected void FireBuyWithVirtualCurrencyEvent(Network.Bundle bundle, CurrencyType currencyType, int quantity = 1)
	{
		if (this.OnProductPurchaseAttempt == null)
		{
			return;
		}
		if (!ShopUtils.IsVirtualCurrencyEnabled())
		{
			Log.Store.PrintError("FireBuyWithVirtualCurrencyEvent error: Virtual Currency is not enabled.", Array.Empty<object>());
			return;
		}
		if (bundle == null)
		{
			Log.Store.PrintError("FireBuyWithVirtualCurrencyEvent error: bundle is null", Array.Empty<object>());
			return;
		}
		if (!ShopUtils.IsCurrencyVirtual(currencyType))
		{
			Log.Store.PrintError("FireBuyWithVirtualCurrencyEvent error: Currency Type is not virtual: {0}", new object[]
			{
				currencyType
			});
			return;
		}
		ProductDataModel productByPmtId = StoreManager.Get().Catalog.GetProductByPmtId(bundle.PMTProductID.Value);
		PriceDataModel price = productByPmtId.Prices.FirstOrDefault((PriceDataModel p) => p.Currency == currencyType);
		Shop.Get().AttemptToPurchaseProduct(productByPmtId, price, quantity);
	}

	// Token: 0x06006596 RID: 26006 RVA: 0x002111EF File Offset: 0x0020F3EF
	protected void SetGoldButtonState(Store.BuyButtonState state)
	{
		this.m_goldButtonInternal.State = state;
	}

	// Token: 0x06006597 RID: 26007 RVA: 0x002111FD File Offset: 0x0020F3FD
	protected Store.BuyButtonState GetGoldButtonState()
	{
		return this.m_goldButtonInternal.State;
	}

	// Token: 0x06006598 RID: 26008 RVA: 0x0021120A File Offset: 0x0020F40A
	protected void SetMoneyButtonState(Store.BuyButtonState state)
	{
		this.m_moneyButtonInternal.State = state;
	}

	// Token: 0x06006599 RID: 26009 RVA: 0x00211218 File Offset: 0x0020F418
	protected Store.BuyButtonState GetMoneyButtonState()
	{
		return this.m_moneyButtonInternal.State;
	}

	// Token: 0x0600659A RID: 26010 RVA: 0x00211225 File Offset: 0x0020F425
	protected void SetVCButtonState(Store.BuyButtonState state)
	{
		this.m_vcButtonInternal.State = state;
	}

	// Token: 0x0600659B RID: 26011 RVA: 0x00211233 File Offset: 0x0020F433
	protected Store.BuyButtonState GetVCButtonState()
	{
		return this.m_vcButtonInternal.State;
	}

	// Token: 0x0600659C RID: 26012 RVA: 0x00211240 File Offset: 0x0020F440
	private IEnumerator ShowWhenReady(bool isTotallyFake)
	{
		VisualController visualController = base.GetComponent<VisualController>();
		while (visualController != null && visualController.IsChangingStates)
		{
			yield return null;
		}
		this.ShowImpl(isTotallyFake);
		yield break;
	}

	// Token: 0x0600659D RID: 26013 RVA: 0x00211258 File Offset: 0x0020F458
	private void ForceDisableBuyButtons(bool forceDisable)
	{
		foreach (Store.BuyButtonInternal buyButtonInternal in this.m_buyButtons)
		{
			buyButtonInternal.ForceDisabled = forceDisable;
		}
	}

	// Token: 0x0600659E RID: 26014 RVA: 0x002112AC File Offset: 0x0020F4AC
	private IEnumerator NotifyListenersWhenReady()
	{
		while (!this.IsReady())
		{
			yield return null;
		}
		if (this.OnReady != null)
		{
			this.OnReady();
		}
		yield break;
	}

	// Token: 0x0600659F RID: 26015 RVA: 0x002112BC File Offset: 0x0020F4BC
	protected void OnInfoPressed(UIEvent e)
	{
		Store.InfoListener[] array = this.m_infoListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x060065A0 RID: 26016 RVA: 0x002112EB File Offset: 0x0020F4EB
	protected virtual string GetOwnedTooltipString()
	{
		return GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_TEXT_PURCHASED");
	}

	// Token: 0x060065A1 RID: 26017 RVA: 0x002112F8 File Offset: 0x0020F4F8
	protected void SilenceBuyButtons()
	{
		foreach (Store.BuyButtonInternal buyButtonInternal in this.m_buyButtons)
		{
			buyButtonInternal.m_silenceReleaseHandler = true;
		}
	}

	// Token: 0x060065A2 RID: 26018 RVA: 0x0021134C File Offset: 0x0020F54C
	protected virtual void RefreshBuyButtonStates(Network.Bundle bundle, NoGTAPPTransactionData transaction)
	{
		Store.BuyButtonState moneyButtonState = Store.DetermineBuyButtonState(bundle, transaction, CurrencyType.REAL_MONEY);
		this.SetMoneyButtonState(moneyButtonState);
		Store.BuyButtonState goldButtonState = Store.DetermineBuyButtonState(bundle, transaction, CurrencyType.GOLD);
		this.SetGoldButtonState(goldButtonState);
		CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(bundle);
		Store.BuyButtonState vcbuttonState = Store.DetermineBuyButtonState(bundle, transaction, bundleVirtualCurrencyPriceType);
		this.SetVCButtonState(vcbuttonState);
	}

	// Token: 0x060065A3 RID: 26019 RVA: 0x00211390 File Offset: 0x0020F590
	protected static Store.BuyButtonState DetermineBuyButtonState(Network.Bundle bundle, NoGTAPPTransactionData transaction, CurrencyType currencyType)
	{
		if (currencyType == CurrencyType.NONE)
		{
			return Store.BuyButtonState.DISABLED;
		}
		if (!StoreManager.Get().IsOpen(true))
		{
			return Store.BuyButtonState.DISABLED;
		}
		if (currencyType == CurrencyType.REAL_MONEY && !StoreManager.Get().IsBattlePayFeatureEnabled())
		{
			return Store.BuyButtonState.DISABLED_FEATURE;
		}
		if (currencyType == CurrencyType.GOLD && !StoreManager.Get().IsBuyWithGoldFeatureEnabled())
		{
			return Store.BuyButtonState.DISABLED_FEATURE;
		}
		if (ShopUtils.IsCurrencyVirtual(currencyType) && !ShopUtils.IsVirtualCurrencyEnabled())
		{
			return Store.BuyButtonState.DISABLED_FEATURE;
		}
		long num;
		if (!ShopUtils.TryGetPriceFromBundleOrTransaction(bundle, transaction, currencyType, out num))
		{
			return Store.BuyButtonState.DISABLED_NO_TOOLTIP;
		}
		if (StoreManager.Get().IsProductAlreadyOwned(bundle))
		{
			return Store.BuyButtonState.DISABLED_OWNED;
		}
		if (!StoreManager.Get().IsBundleAvailableNow(bundle))
		{
			return Store.BuyButtonState.DISABLED_NO_TOOLTIP;
		}
		if (currencyType != CurrencyType.REAL_MONEY && ShopUtils.GetCachedBalance(currencyType) < num)
		{
			switch (currencyType)
			{
			case CurrencyType.GOLD:
				return Store.BuyButtonState.DISABLED_NOT_ENOUGH_GOLD;
			case CurrencyType.RUNESTONES:
			{
				ProductDataModel virtualCurrencyProductItem = StoreManager.Get().Catalog.VirtualCurrencyProductItem;
				if (virtualCurrencyProductItem != null && virtualCurrencyProductItem.Availability == ProductAvailability.CAN_PURCHASE)
				{
					return Store.BuyButtonState.ENABLED;
				}
				return Store.BuyButtonState.DISABLED_NOT_ENOUGH_RUNESTONES;
			}
			case CurrencyType.ARCANE_ORBS:
			{
				ProductDataModel boosterCurrencyProductItem = StoreManager.Get().Catalog.BoosterCurrencyProductItem;
				if (boosterCurrencyProductItem != null && boosterCurrencyProductItem.Availability == ProductAvailability.CAN_PURCHASE)
				{
					return Store.BuyButtonState.ENABLED;
				}
				return Store.BuyButtonState.DISABLED_NOT_ENOUGH_ARCANE_ORBS;
			}
			}
			return Store.BuyButtonState.DISABLED;
		}
		return Store.BuyButtonState.ENABLED;
	}

	// Token: 0x060065A4 RID: 26020 RVA: 0x00211484 File Offset: 0x0020F684
	protected void BindProductDataModel(Network.Bundle bundle)
	{
		WidgetTemplate componentOnSelfOrParent = SceneUtils.GetComponentOnSelfOrParent<WidgetTemplate>(base.transform);
		if (componentOnSelfOrParent != null)
		{
			ProductDataModel productDataModel = null;
			if (bundle != null && bundle.PMTProductID != null)
			{
				productDataModel = StoreManager.Get().Catalog.GetProductByPmtId(bundle.PMTProductID.Value);
			}
			productDataModel = (productDataModel ?? ProductFactory.CreateEmptyProductDataModel());
			componentOnSelfOrParent.BindDataModel(productDataModel, base.gameObject, true, true);
		}
	}

	// Token: 0x060065A5 RID: 26021 RVA: 0x002114F4 File Offset: 0x0020F6F4
	private void OnShopProductPageChanged(ProductPage page)
	{
		bool flag = page != null;
		if (flag && this.IsShown())
		{
			this.m_restoreWhenShopHides = true;
			this.Hide();
			return;
		}
		if (!flag && this.m_restoreWhenShopHides)
		{
			this.m_restoreWhenShopHides = false;
			base.StartCoroutine(this.ShowWhenReady(false));
		}
	}

	// Token: 0x060065A6 RID: 26022 RVA: 0x001CE9C5 File Offset: 0x001CCBC5
	public IEnumerable<CurrencyType> GetVisibleCurrencies()
	{
		if (ShopUtils.IsVirtualCurrencyEnabled())
		{
			return new CurrencyType[]
			{
				CurrencyType.GOLD,
				CurrencyType.RUNESTONES
			};
		}
		return new CurrencyType[]
		{
			CurrencyType.GOLD
		};
	}

	// Token: 0x0400542A RID: 21546
	[CustomEditField(Sections = "Store/UI")]
	public GameObject m_root;

	// Token: 0x0400542B RID: 21547
	[CustomEditField(Sections = "Store/UI")]
	public GameObject m_cover;

	// Token: 0x0400542C RID: 21548
	[CustomEditField(Sections = "Store/UI")]
	public UIBButton m_buyWithMoneyButton;

	// Token: 0x0400542D RID: 21549
	[CustomEditField(Sections = "Store/UI")]
	public TooltipZone m_buyWithMoneyTooltip;

	// Token: 0x0400542E RID: 21550
	[CustomEditField(Sections = "Store/UI")]
	public PegUIElement m_buyWithMoneyTooltipTrigger;

	// Token: 0x0400542F RID: 21551
	[CustomEditField(Sections = "Store/UI")]
	public UIBButton m_buyWithGoldButton;

	// Token: 0x04005430 RID: 21552
	[CustomEditField(Sections = "Store/UI")]
	public TooltipZone m_buyWithGoldTooltip;

	// Token: 0x04005431 RID: 21553
	[CustomEditField(Sections = "Store/UI")]
	public PegUIElement m_buyWithGoldTooltipTrigger;

	// Token: 0x04005432 RID: 21554
	[CustomEditField(Sections = "Store/UI")]
	public UIBButton m_buyWithVCButton;

	// Token: 0x04005433 RID: 21555
	[CustomEditField(Sections = "Store/UI")]
	public TooltipZone m_buyWithVCTooltip;

	// Token: 0x04005434 RID: 21556
	[CustomEditField(Sections = "Store/UI")]
	public PegUIElement m_buyWithVCTooltipTrigger;

	// Token: 0x04005435 RID: 21557
	[CustomEditField(Sections = "Store/UI")]
	public UIBButton m_infoButton;

	// Token: 0x04005436 RID: 21558
	[CustomEditField(Sections = "Store/Materials")]
	public List<MeshRenderer> m_goldButtonMeshes = new List<MeshRenderer>();

	// Token: 0x04005437 RID: 21559
	[CustomEditField(Sections = "Store/Materials")]
	public Material m_enabledGoldButtonMaterial;

	// Token: 0x04005438 RID: 21560
	[CustomEditField(Sections = "Store/Materials")]
	public Material m_disabledGoldButtonMaterial;

	// Token: 0x04005439 RID: 21561
	[CustomEditField(Sections = "Store/Materials")]
	public List<MeshRenderer> m_moneyButtonMeshes = new List<MeshRenderer>();

	// Token: 0x0400543A RID: 21562
	[CustomEditField(Sections = "Store/Materials")]
	public Material m_enabledMoneyButtonMaterial;

	// Token: 0x0400543B RID: 21563
	[CustomEditField(Sections = "Store/Materials")]
	public Material m_disabledMoneyButtonMaterial;

	// Token: 0x0400543C RID: 21564
	[CustomEditField(Sections = "Store/Materials")]
	public List<MeshRenderer> m_vcButtonMeshes = new List<MeshRenderer>();

	// Token: 0x0400543D RID: 21565
	[CustomEditField(Sections = "Store/Materials")]
	public Material m_enabledVCButtonMaterial;

	// Token: 0x0400543E RID: 21566
	[CustomEditField(Sections = "Store/Materials")]
	public Material m_disabledVCButtonMaterial;

	// Token: 0x0400543F RID: 21567
	[CustomEditField(Sections = "Store/UI")]
	public PegUIElement m_offClicker;

	// Token: 0x04005440 RID: 21568
	protected bool m_hasRequestedFullscreenFX;

	// Token: 0x04005441 RID: 21569
	protected bool m_restoreWhenShopHides;

	// Token: 0x04005442 RID: 21570
	private Store.BuyButtonInternal m_goldButtonInternal;

	// Token: 0x04005443 RID: 21571
	private Store.BuyButtonInternal m_moneyButtonInternal;

	// Token: 0x04005444 RID: 21572
	private Store.BuyButtonInternal m_vcButtonInternal;

	// Token: 0x04005445 RID: 21573
	private List<Store.BuyButtonInternal> m_buyButtons = new List<Store.BuyButtonInternal>();

	// Token: 0x04005446 RID: 21574
	private List<Store.InfoListener> m_infoListeners = new List<Store.InfoListener>();

	// Token: 0x020022AA RID: 8874
	protected enum BuyButtonState
	{
		// Token: 0x0400E44E RID: 58446
		ENABLED,
		// Token: 0x0400E44F RID: 58447
		DISABLED_NOT_ENOUGH_GOLD,
		// Token: 0x0400E450 RID: 58448
		DISABLED_NOT_ENOUGH_RUNESTONES,
		// Token: 0x0400E451 RID: 58449
		DISABLED_NOT_ENOUGH_ARCANE_ORBS,
		// Token: 0x0400E452 RID: 58450
		DISABLED_FEATURE,
		// Token: 0x0400E453 RID: 58451
		DISABLED,
		// Token: 0x0400E454 RID: 58452
		DISABLED_OWNED,
		// Token: 0x0400E455 RID: 58453
		DISABLED_NO_TOOLTIP
	}

	// Token: 0x020022AB RID: 8875
	private class BuyButtonInternal
	{
		// Token: 0x0601280F RID: 75791 RVA: 0x0050D378 File Offset: 0x0050B578
		public void Init()
		{
			if (this.m_button != null)
			{
				this.m_button.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnReleased));
			}
			if (this.m_tooltipTrigger != null)
			{
				this.m_tooltipTrigger.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnShowTooltip));
				this.m_tooltipTrigger.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnHideTooltip));
			}
		}

		// Token: 0x06012810 RID: 75792 RVA: 0x0050D3EC File Offset: 0x0050B5EC
		public void OnDestroy()
		{
			if (this.m_button != null)
			{
				this.m_button.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnReleased));
			}
			if (this.m_tooltipTrigger != null)
			{
				this.m_tooltipTrigger.RemoveEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnShowTooltip));
				this.m_tooltipTrigger.RemoveEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnHideTooltip));
			}
			this.m_enabledMaterial = null;
			this.m_disabledMaterial = null;
		}

		// Token: 0x17002982 RID: 10626
		// (get) Token: 0x06012811 RID: 75793 RVA: 0x0050D46E File Offset: 0x0050B66E
		// (set) Token: 0x06012812 RID: 75794 RVA: 0x0050D476 File Offset: 0x0050B676
		public Store.BuyButtonState State
		{
			get
			{
				return this.m_state;
			}
			set
			{
				this.m_state = value;
				this.Refresh();
			}
		}

		// Token: 0x17002983 RID: 10627
		// (get) Token: 0x06012813 RID: 75795 RVA: 0x0050D485 File Offset: 0x0050B685
		// (set) Token: 0x06012814 RID: 75796 RVA: 0x0050D48D File Offset: 0x0050B68D
		public bool ForceDisabled
		{
			get
			{
				return this.m_forceDisabled;
			}
			set
			{
				this.m_forceDisabled = value;
				this.Refresh();
			}
		}

		// Token: 0x06012815 RID: 75797 RVA: 0x0050D49C File Offset: 0x0050B69C
		private void Refresh()
		{
			bool flag = !this.m_forceDisabled && this.m_state == Store.BuyButtonState.ENABLED;
			Material material = flag ? this.m_enabledMaterial : this.m_disabledMaterial;
			foreach (MeshRenderer meshRenderer in this.m_meshes)
			{
				if (meshRenderer != null)
				{
					meshRenderer.SetSharedMaterial(material);
				}
			}
			if (this.m_button != null)
			{
				Collider component = this.m_button.GetComponent<Collider>();
				if (component != null)
				{
					component.enabled = flag;
				}
			}
			if (this.m_tooltipTrigger != null)
			{
				this.m_tooltipTrigger.gameObject.SetActive(!flag && this.m_state != Store.BuyButtonState.DISABLED_NO_TOOLTIP);
			}
		}

		// Token: 0x06012816 RID: 75798 RVA: 0x0050D57C File Offset: 0x0050B77C
		private void OnReleased(UIEvent e)
		{
			if (!this.m_forceDisabled && this.m_state == Store.BuyButtonState.ENABLED && !this.m_silenceReleaseHandler)
			{
				this.m_buyHandler(e);
			}
		}

		// Token: 0x06012817 RID: 75799 RVA: 0x0050D5A2 File Offset: 0x0050B7A2
		private void OnShowTooltip(UIEvent e)
		{
			if (this.m_state != Store.BuyButtonState.ENABLED)
			{
				this.m_tooltipZone.ShowLayerTooltip(GameStrings.Get(this.m_toolTipHeadlineStringId), this.GetBuyButtonTooltipMessage(this.m_state), 0);
			}
		}

		// Token: 0x06012818 RID: 75800 RVA: 0x0050D5D0 File Offset: 0x0050B7D0
		private void OnHideTooltip(UIEvent e)
		{
			this.m_tooltipZone.HideTooltip();
		}

		// Token: 0x06012819 RID: 75801 RVA: 0x0050D5E0 File Offset: 0x0050B7E0
		private string GetBuyButtonTooltipMessage(Store.BuyButtonState state)
		{
			switch (state)
			{
			case Store.BuyButtonState.DISABLED_NOT_ENOUGH_GOLD:
				return GameStrings.Get("GLUE_STORE_FAIL_NOT_ENOUGH_GOLD");
			case Store.BuyButtonState.DISABLED_NOT_ENOUGH_RUNESTONES:
				return GameStrings.Get("GLUE_STORE_FAIL_NOT_ENOUGH_RUNESTONES");
			case Store.BuyButtonState.DISABLED_NOT_ENOUGH_ARCANE_ORBS:
				return GameStrings.Get("GLUE_STORE_FAIL_NOT_ENOUGH_ARCANE_ORBS");
			case Store.BuyButtonState.DISABLED_FEATURE:
				return GameStrings.Get("GLUE_STORE_DISABLED");
			case Store.BuyButtonState.DISABLED_OWNED:
				return this.m_getOwnedTooltipStringHandler();
			case Store.BuyButtonState.DISABLED_NO_TOOLTIP:
				return string.Empty;
			}
			return GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
		}

		// Token: 0x0400E456 RID: 58454
		public UIBButton m_button;

		// Token: 0x0400E457 RID: 58455
		public TooltipZone m_tooltipZone;

		// Token: 0x0400E458 RID: 58456
		public PegUIElement m_tooltipTrigger;

		// Token: 0x0400E459 RID: 58457
		public List<MeshRenderer> m_meshes;

		// Token: 0x0400E45A RID: 58458
		public Material m_enabledMaterial;

		// Token: 0x0400E45B RID: 58459
		public Material m_disabledMaterial;

		// Token: 0x0400E45C RID: 58460
		public string m_toolTipHeadlineStringId;

		// Token: 0x0400E45D RID: 58461
		public UIEvent.Handler m_buyHandler;

		// Token: 0x0400E45E RID: 58462
		public Func<string> m_getOwnedTooltipStringHandler;

		// Token: 0x0400E45F RID: 58463
		public bool m_silenceReleaseHandler;

		// Token: 0x0400E460 RID: 58464
		private Store.BuyButtonState m_state;

		// Token: 0x0400E461 RID: 58465
		private bool m_forceDisabled;
	}

	// Token: 0x020022AC RID: 8876
	// (Invoke) Token: 0x0601281C RID: 75804
	public delegate void ExitCallback(bool authorizationBackButtonPressed, object userData);

	// Token: 0x020022AD RID: 8877
	// (Invoke) Token: 0x06012820 RID: 75808
	public delegate void InfoCallback(object userData);

	// Token: 0x020022AE RID: 8878
	private class InfoListener : EventListener<Store.InfoCallback>
	{
		// Token: 0x06012823 RID: 75811 RVA: 0x0050D65B File Offset: 0x0050B85B
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}
}
