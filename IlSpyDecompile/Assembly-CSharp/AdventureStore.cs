using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class AdventureStore : Store
{
	[CustomEditField(Sections = "UI")]
	public UIBButton m_BuyDungeonButton;

	[CustomEditField(Sections = "UI")]
	public UberText m_Headline;

	[CustomEditField(Sections = "UI")]
	public UberText m_DetailsText1;

	[CustomEditField(Sections = "UI")]
	public UberText m_DetailsText2;

	[CustomEditField(Sections = "UI")]
	public GameObject m_BuyWithMoneyButtonOpaqueCover;

	[CustomEditField(Sections = "UI")]
	public GameObject m_BuyWithGoldButtonOpaqueCover;

	[CustomEditField(Sections = "UI")]
	public GameObject m_BuyDungeonButtonOpaqueCover;

	[CustomEditField(Sections = "UI")]
	public UIBButton m_BackButton;

	[CustomEditField(Sections = "UI")]
	public WidgetInstance m_FullAdventureBundleCurrencyIcon;

	private bool m_animating;

	private Network.Bundle m_bundle;

	private Network.Bundle m_fullAdventureBundle;

	protected override void Start()
	{
		base.Start();
		if (m_BuyDungeonButton != null)
		{
			m_BuyDungeonButton.AddEventListener(UIEventType.RELEASE, OnBuyDungeonButtonReleased);
		}
		if (m_offClicker != null)
		{
			m_offClicker.AddEventListener(UIEventType.RELEASE, OnBackButtonReleased);
		}
		if (m_BackButton != null)
		{
			m_BackButton.AddEventListener(UIEventType.RELEASE, OnBackButtonReleased);
		}
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service) && service.OnOrderCompleteEvent != null)
		{
			service.OnOrderCompleteEvent.AddListener(OnOrderComplete);
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out var service) && service.OnOrderCompleteEvent != null)
		{
			service.OnOrderCompleteEvent.RemoveListener(OnOrderComplete);
		}
	}

	public void SetAdventureProduct(ProductType productItemType, int productData, int numItemsRequired, int pmtProductId = 0)
	{
		if (pmtProductId != 0)
		{
			m_bundle = null;
			Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(pmtProductId);
			if (bundleFromPmtProductId == null)
			{
				Log.Store.PrintWarning("AdventureStore.SetAdventureProduct(): could not find bundle with PMT Product ID {0}", pmtProductId);
			}
			else if (!StoreManager.DoesBundleContainProduct(bundleFromPmtProductId, productItemType, productData, numItemsRequired))
			{
				Log.Store.PrintWarning("AdventureStore.SetAdventureProduct(): bundle with PMT product ID {0} does not meet the expected criteria! productItemType: {1}  productData: {2}  numItemsRequired: {3}", pmtProductId, productItemType, productData, numItemsRequired);
			}
			else if (!StoreManager.Get().IsBundleAvailableNow(bundleFromPmtProductId))
			{
				Log.Store.PrintWarning("AdventureStore.SetAdventureProduct(): bundle with PMT product ID {0} is not available now!", pmtProductId);
			}
			else if (StoreManager.Get().IsProductAlreadyOwned(bundleFromPmtProductId))
			{
				Log.Store.PrintWarning("AdventureStore.SetAdventureProduct(): bundle with PMT product ID {0} contains already owned content!", pmtProductId);
			}
			else
			{
				m_bundle = bundleFromPmtProductId;
			}
		}
		else
		{
			List<Network.Bundle> availableBundlesForProduct = StoreManager.Get().GetAvailableBundlesForProduct(productItemType, numItemsRequired > 1, productData, numItemsRequired);
			if (availableBundlesForProduct.Count == 1)
			{
				m_bundle = availableBundlesForProduct[0];
			}
			else
			{
				Debug.LogWarningFormat("AdventureStore.SetAdventureProduct(): expected to find 1 available bundle for productItemType {0} productData {1} numItemsRequired {2}, found {3}", productItemType, productData, numItemsRequired, availableBundlesForProduct.Count);
				m_bundle = null;
			}
		}
		string productName = StoreManager.Get().GetProductName(m_bundle);
		if (m_Headline != null)
		{
			m_Headline.Text = productName;
		}
		string arg = string.Empty;
		switch (productItemType)
		{
		case ProductType.PRODUCT_TYPE_NAXX:
			arg = "NAXX";
			break;
		case ProductType.PRODUCT_TYPE_BRM:
			arg = "BRM";
			break;
		case ProductType.PRODUCT_TYPE_LOE:
			arg = "LOE";
			break;
		case ProductType.PRODUCT_TYPE_WING:
			arg = GameUtils.GetAdventureProductStringKey(productData);
			break;
		}
		string text = GameDbf.Wing.GetRecord(productData).NameShort;
		string text2 = (string.IsNullOrEmpty(text) ? productName : text);
		if (m_DetailsText1 != null)
		{
			string key = $"GLUE_STORE_PRODUCT_DETAILS_{arg}_PART_1";
			m_DetailsText1.Text = GameStrings.Format(key, text2);
		}
		if (m_DetailsText2 != null)
		{
			string key2 = $"GLUE_STORE_PRODUCT_DETAILS_{arg}_PART_2";
			m_DetailsText2.Text = GameStrings.Format(key2);
		}
		AdventureDbId adventureIdByWingId = GameUtils.GetAdventureIdByWingId(productData);
		StoreManager.Get().GetAvailableAdventureBundle(adventureIdByWingId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out m_fullAdventureBundle);
		if (m_fullAdventureBundle == null)
		{
			Log.Store.PrintWarning("Full adventure bundle not available.");
		}
		BindProductDataModel(m_bundle);
	}

	public override void Hide()
	{
		m_shown = false;
		Navigation.RemoveHandler(OnNavigateBack);
		StoreManager.Get().RemoveAuthorizationExitListener(OnAuthExit);
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(OnSuccessfulPurchase);
		EnableFullScreenEffects(enable: false);
		DoHideAnimation();
	}

	public override void OnMoneySpent()
	{
		RefreshBuyButtonStates(m_bundle, null);
		RefreshBuyFullAdventureButton();
	}

	public override void OnGoldBalanceChanged(NetCache.NetCacheGoldBalance balance)
	{
		RefreshBuyButtonStates(m_bundle, null);
	}

	public override void OnCurrencyBalanceChanged(CurrencyBalanceChangedEventArgs args)
	{
		if (ShopUtils.IsCurrencyVirtual(args.Currency))
		{
			RefreshBuyButtonStates(m_bundle, null);
			RefreshBuyFullAdventureButton();
		}
	}

	public override void Close()
	{
		Hide();
		FireExitEvent(authorizationBackButtonPressed: false);
	}

	protected override void ShowImpl(bool isTotallyFake)
	{
		if (!m_shown)
		{
			m_shown = true;
			Navigation.Push(OnNavigateBack);
			StoreManager.Get().RegisterAuthorizationExitListener(OnAuthExit);
			StoreManager.Get().RegisterSuccessfulPurchaseAckListener(OnSuccessfulPurchase);
			EnableFullScreenEffects(enable: true);
			SetUpBuyButtons();
			m_animating = true;
			DoShowAnimation(delegate
			{
				m_animating = false;
				FireOpenedEvent();
			});
		}
	}

	protected override void BuyWithGold(UIEvent e)
	{
		if (m_animating)
		{
			Log.Store.Print("AdventureStore.BuyWithGold failed: still animating");
		}
		else if (m_bundle == null)
		{
			Log.Store.PrintError("AdventureStore.BuyWithGold failed: Bundle is null");
		}
		else
		{
			FireBuyWithGoldEventGTAPP(m_bundle, 1);
		}
	}

	protected override void BuyWithMoney(UIEvent e)
	{
		if (m_animating)
		{
			Log.Store.Print("AdventureStore.BuyWithMoney failed: still animating");
		}
		else if (m_bundle == null)
		{
			Log.Store.PrintError("AdventureStore.BuyWithMoney failed: Bundle is null");
		}
		else
		{
			FireBuyWithMoneyEvent(m_bundle, 1);
		}
	}

	protected override void BuyWithVirtualCurrency(UIEvent e)
	{
		if (m_animating)
		{
			Log.Store.Print("AdventureStore.BuyWithVirtualCurrency failed: still animating");
			return;
		}
		if (m_bundle == null)
		{
			Log.Store.PrintError("AdventureStore.BuyWithVirtualCurrency failed: Bundle is null");
			return;
		}
		CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(m_bundle);
		if (bundleVirtualCurrencyPriceType == CurrencyType.NONE)
		{
			Log.Store.PrintError("BuyWithVirtualCurrency failed: Bundle not available for virtual currency");
		}
		else
		{
			FireBuyWithVirtualCurrencyEvent(m_bundle, bundleVirtualCurrencyPriceType);
		}
	}

	protected override void RefreshBuyButtonStates(Network.Bundle bundle, NoGTAPPTransactionData transaction)
	{
		base.RefreshBuyButtonStates(bundle, transaction);
		if (m_BuyWithMoneyButtonOpaqueCover != null)
		{
			bool num = m_buyWithMoneyButton != null && m_buyWithMoneyButton.gameObject.activeInHierarchy;
			bool flag = m_buyWithVCButton != null && m_buyWithVCButton.gameObject.activeInHierarchy;
			bool active = false;
			if (num && GetMoneyButtonState() == BuyButtonState.DISABLED_NO_TOOLTIP)
			{
				active = true;
			}
			if (flag && GetVCButtonState() == BuyButtonState.DISABLED_NO_TOOLTIP)
			{
				active = true;
			}
			m_BuyWithMoneyButtonOpaqueCover.SetActive(active);
		}
		if (m_BuyWithGoldButtonOpaqueCover != null)
		{
			bool active2 = GetGoldButtonState() == BuyButtonState.DISABLED_NO_TOOLTIP;
			m_BuyWithGoldButtonOpaqueCover.SetActive(active2);
		}
		RefreshBuyFullAdventureButton();
	}

	private void OnAuthExit()
	{
		BlockInterface(blocked: false);
		SceneUtils.SetLayer(base.gameObject, GameLayer.Default);
		EnableFullScreenEffects(enable: false);
		StoreManager.Get().RemoveAuthorizationExitListener(OnAuthExit);
		FireExitEvent(authorizationBackButtonPressed: true);
		Hide();
	}

	private void OnSuccessfulPurchase(Network.Bundle bundle, PaymentMethod method)
	{
		BlockInterface(blocked: false);
		EnableFullScreenEffects(enable: false);
		FireExitEvent(authorizationBackButtonPressed: true);
		Hide();
	}

	private void SetUpBuyButtons()
	{
		SetUpBuyWithGoldButton();
		SetUpBuyWithMoneyButton();
		SetUpBuyFullAdventureButton();
		RefreshBuyButtonStates(m_bundle, null);
	}

	private void SetUpBuyWithGoldButton()
	{
		string empty = string.Empty;
		if (m_bundle != null)
		{
			empty = m_bundle.GtappGoldCost.ToString();
		}
		else
		{
			Debug.LogWarning("AdventureStore.SetUpBuyWithGoldButton(): m_bundle is null");
			empty = GameStrings.Get("GLUE_STORE_PRODUCT_PRICE_NA");
		}
		if (m_buyWithGoldButton != null)
		{
			m_buyWithGoldButton.SetText(empty);
		}
	}

	private void SetUpBuyWithMoneyButton()
	{
		string empty = string.Empty;
		if (m_bundle != null)
		{
			empty = StoreManager.Get().FormatCostBundle(m_bundle);
		}
		else
		{
			Debug.LogWarning("AdventureStore.SetUpBuyWithMoneyButton(): m_bundle is null");
			empty = GameStrings.Get("GLUE_STORE_PRODUCT_PRICE_NA");
		}
		m_buyWithMoneyButton.SetText(empty);
	}

	private void SetUpBuyFullAdventureButton()
	{
		RefreshBuyFullAdventureButton();
	}

	private void RefreshBuyFullAdventureButton()
	{
		if (m_fullAdventureBundle != null && !StoreManager.Get().CanBuyBundle(m_fullAdventureBundle))
		{
			Log.Store.PrintWarning("CanBuyBundle is false for m_fullAdventureBundle, PMTProductID = {0}", m_fullAdventureBundle.PMTProductID.GetValueOrDefault());
			m_fullAdventureBundle = null;
		}
		string empty = string.Empty;
		bool flag = false;
		string text = null;
		CurrencyType currencyType = CurrencyType.NONE;
		long num = 0L;
		if (m_fullAdventureBundle != null)
		{
			currencyType = ShopUtils.GetBundleVirtualCurrencyPriceType(m_fullAdventureBundle);
			if (currencyType != 0)
			{
				num = m_fullAdventureBundle.VirtualCurrencyCost.GetValueOrDefault();
				switch (currencyType)
				{
				case CurrencyType.RUNESTONES:
					text = GameStrings.Format("GLUE_SHOP_PRICE_RUNESTONES", num);
					break;
				case CurrencyType.ARCANE_ORBS:
					text = GameStrings.Format("GLUE_SHOP_PRICE_ARCANE_ORBS", num);
					break;
				}
			}
			else if (m_fullAdventureBundle.Cost.HasValue)
			{
				text = StoreManager.Get().FormatCostBundle(m_fullAdventureBundle);
			}
		}
		if (text != null)
		{
			string arg = GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_TEXT");
			string arg2 = GameStrings.Format("GLUE_STORE_DUNGEON_BUTTON_COST_TEXT", m_fullAdventureBundle.Items.Count, text);
			empty = $"{arg}\n{arg2}";
		}
		else
		{
			flag = true;
			empty = string.Empty;
		}
		if (m_FullAdventureBundleCurrencyIcon != null)
		{
			m_FullAdventureBundleCurrencyIcon.BindDataModel(new PriceDataModel
			{
				Currency = currencyType,
				Amount = num,
				DisplayText = num.ToString()
			});
		}
		if (m_BuyDungeonButton != null)
		{
			m_BuyDungeonButton.SetText(empty);
		}
		if (m_BuyDungeonButtonOpaqueCover != null)
		{
			m_BuyDungeonButtonOpaqueCover.SetActive(flag);
		}
		if (m_BuyDungeonButton != null)
		{
			m_BuyDungeonButton.SetEnabled(!flag);
		}
	}

	private void OnBuyDungeonButtonReleased(UIEvent e)
	{
		if (m_animating)
		{
			Log.Store.Print("AdventureStore.OnBuyDungeonButtonReleased failed: still animating");
			return;
		}
		if (m_fullAdventureBundle == null)
		{
			Log.Store.PrintError("AdventureStore.OnBuyDungeonButtonReleased failed: m_fullAdventureBundle is null");
			return;
		}
		CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(m_fullAdventureBundle);
		if (bundleVirtualCurrencyPriceType != 0)
		{
			FireBuyWithVirtualCurrencyEvent(m_fullAdventureBundle, bundleVirtualCurrencyPriceType);
			return;
		}
		if (m_fullAdventureBundle.Cost.HasValue)
		{
			FireBuyWithMoneyEvent(m_fullAdventureBundle, 1);
			return;
		}
		Log.Store.PrintError("AdventureStore.OnBuyDungeonButtonReleased failed: no valid price on m_fullAdventureBundle. PMT ID = {0}", m_fullAdventureBundle.PMTProductID.GetValueOrDefault());
	}

	private void OnBackButtonReleased(UIEvent e)
	{
		if (!HearthstoneServices.TryGet<HearthstoneCheckout>(out var service) || service.CurrentState != HearthstoneCheckout.State.InProgress)
		{
			Close();
		}
	}

	private bool OnNavigateBack()
	{
		Close();
		return true;
	}

	private void OnOrderComplete(HearthstoneCheckoutTransactionData data)
	{
		if (this != null)
		{
			Hide();
		}
	}
}
