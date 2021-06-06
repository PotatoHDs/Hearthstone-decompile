using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

// Token: 0x020006E7 RID: 1767
[CustomEditClass]
public class AdventureStore : Store
{
	// Token: 0x06006268 RID: 25192 RVA: 0x00201AB0 File Offset: 0x001FFCB0
	protected override void Start()
	{
		base.Start();
		if (this.m_BuyDungeonButton != null)
		{
			this.m_BuyDungeonButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBuyDungeonButtonReleased));
		}
		if (this.m_offClicker != null)
		{
			this.m_offClicker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackButtonReleased));
		}
		if (this.m_BackButton != null)
		{
			this.m_BackButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackButtonReleased));
		}
		HearthstoneCheckout hearthstoneCheckout;
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout) && hearthstoneCheckout.OnOrderCompleteEvent != null)
		{
			hearthstoneCheckout.OnOrderCompleteEvent.AddListener(new Action<HearthstoneCheckoutTransactionData>(this.OnOrderComplete));
		}
	}

	// Token: 0x06006269 RID: 25193 RVA: 0x00201B60 File Offset: 0x001FFD60
	protected override void OnDestroy()
	{
		base.OnDestroy();
		HearthstoneCheckout hearthstoneCheckout;
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout) && hearthstoneCheckout.OnOrderCompleteEvent != null)
		{
			hearthstoneCheckout.OnOrderCompleteEvent.RemoveListener(new Action<HearthstoneCheckoutTransactionData>(this.OnOrderComplete));
		}
	}

	// Token: 0x0600626A RID: 25194 RVA: 0x00201B9C File Offset: 0x001FFD9C
	public void SetAdventureProduct(ProductType productItemType, int productData, int numItemsRequired, int pmtProductId = 0)
	{
		if (pmtProductId != 0)
		{
			this.m_bundle = null;
			Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId((long)pmtProductId);
			if (bundleFromPmtProductId == null)
			{
				Log.Store.PrintWarning("AdventureStore.SetAdventureProduct(): could not find bundle with PMT Product ID {0}", new object[]
				{
					pmtProductId
				});
			}
			else if (!StoreManager.DoesBundleContainProduct(bundleFromPmtProductId, productItemType, productData, numItemsRequired))
			{
				Log.Store.PrintWarning("AdventureStore.SetAdventureProduct(): bundle with PMT product ID {0} does not meet the expected criteria! productItemType: {1}  productData: {2}  numItemsRequired: {3}", new object[]
				{
					pmtProductId,
					productItemType,
					productData,
					numItemsRequired
				});
			}
			else if (!StoreManager.Get().IsBundleAvailableNow(bundleFromPmtProductId))
			{
				Log.Store.PrintWarning("AdventureStore.SetAdventureProduct(): bundle with PMT product ID {0} is not available now!", new object[]
				{
					pmtProductId
				});
			}
			else if (StoreManager.Get().IsProductAlreadyOwned(bundleFromPmtProductId))
			{
				Log.Store.PrintWarning("AdventureStore.SetAdventureProduct(): bundle with PMT product ID {0} contains already owned content!", new object[]
				{
					pmtProductId
				});
			}
			else
			{
				this.m_bundle = bundleFromPmtProductId;
			}
		}
		else
		{
			List<Network.Bundle> availableBundlesForProduct = StoreManager.Get().GetAvailableBundlesForProduct(productItemType, numItemsRequired > 1, productData, numItemsRequired);
			if (availableBundlesForProduct.Count == 1)
			{
				this.m_bundle = availableBundlesForProduct[0];
			}
			else
			{
				Debug.LogWarningFormat("AdventureStore.SetAdventureProduct(): expected to find 1 available bundle for productItemType {0} productData {1} numItemsRequired {2}, found {3}", new object[]
				{
					productItemType,
					productData,
					numItemsRequired,
					availableBundlesForProduct.Count
				});
				this.m_bundle = null;
			}
		}
		string productName = StoreManager.Get().GetProductName(this.m_bundle);
		if (this.m_Headline != null)
		{
			this.m_Headline.Text = productName;
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
		string text2;
		if (!string.IsNullOrEmpty(text))
		{
			text2 = text;
		}
		else
		{
			text2 = productName;
		}
		if (this.m_DetailsText1 != null)
		{
			string key = string.Format("GLUE_STORE_PRODUCT_DETAILS_{0}_PART_1", arg);
			this.m_DetailsText1.Text = GameStrings.Format(key, new object[]
			{
				text2
			});
		}
		if (this.m_DetailsText2 != null)
		{
			string key2 = string.Format("GLUE_STORE_PRODUCT_DETAILS_{0}_PART_2", arg);
			this.m_DetailsText2.Text = GameStrings.Format(key2, Array.Empty<object>());
		}
		AdventureDbId adventureIdByWingId = GameUtils.GetAdventureIdByWingId(productData);
		StoreManager.Get().GetAvailableAdventureBundle(adventureIdByWingId, GeneralStoreAdventureContent.REQUIRE_REAL_MONEY_BUNDLE_OPTION, out this.m_fullAdventureBundle);
		if (this.m_fullAdventureBundle == null)
		{
			Log.Store.PrintWarning("Full adventure bundle not available.", Array.Empty<object>());
		}
		base.BindProductDataModel(this.m_bundle);
	}

	// Token: 0x0600626B RID: 25195 RVA: 0x00201E5C File Offset: 0x0020005C
	public override void Hide()
	{
		this.m_shown = false;
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		StoreManager.Get().RemoveAuthorizationExitListener(new Action(this.OnAuthExit));
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchase));
		base.EnableFullScreenEffects(false);
		base.DoHideAnimation(null);
	}

	// Token: 0x0600626C RID: 25196 RVA: 0x00201EBC File Offset: 0x002000BC
	public override void OnMoneySpent()
	{
		this.RefreshBuyButtonStates(this.m_bundle, null);
		this.RefreshBuyFullAdventureButton();
	}

	// Token: 0x0600626D RID: 25197 RVA: 0x00201ED1 File Offset: 0x002000D1
	public override void OnGoldBalanceChanged(NetCache.NetCacheGoldBalance balance)
	{
		this.RefreshBuyButtonStates(this.m_bundle, null);
	}

	// Token: 0x0600626E RID: 25198 RVA: 0x00201EE0 File Offset: 0x002000E0
	public override void OnCurrencyBalanceChanged(CurrencyBalanceChangedEventArgs args)
	{
		if (ShopUtils.IsCurrencyVirtual(args.Currency))
		{
			this.RefreshBuyButtonStates(this.m_bundle, null);
			this.RefreshBuyFullAdventureButton();
		}
	}

	// Token: 0x0600626F RID: 25199 RVA: 0x00201F02 File Offset: 0x00200102
	public override void Close()
	{
		this.Hide();
		base.FireExitEvent(false);
	}

	// Token: 0x06006270 RID: 25200 RVA: 0x00201F14 File Offset: 0x00200114
	protected override void ShowImpl(bool isTotallyFake)
	{
		if (this.m_shown)
		{
			return;
		}
		this.m_shown = true;
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		StoreManager.Get().RegisterAuthorizationExitListener(new Action(this.OnAuthExit));
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchase));
		base.EnableFullScreenEffects(true);
		this.SetUpBuyButtons();
		this.m_animating = true;
		base.DoShowAnimation(delegate()
		{
			this.m_animating = false;
			base.FireOpenedEvent();
		});
	}

	// Token: 0x06006271 RID: 25201 RVA: 0x00201F94 File Offset: 0x00200194
	protected override void BuyWithGold(UIEvent e)
	{
		if (this.m_animating)
		{
			Log.Store.Print("AdventureStore.BuyWithGold failed: still animating", Array.Empty<object>());
			return;
		}
		if (this.m_bundle == null)
		{
			Log.Store.PrintError("AdventureStore.BuyWithGold failed: Bundle is null", Array.Empty<object>());
			return;
		}
		base.FireBuyWithGoldEventGTAPP(this.m_bundle, 1);
	}

	// Token: 0x06006272 RID: 25202 RVA: 0x00201FE8 File Offset: 0x002001E8
	protected override void BuyWithMoney(UIEvent e)
	{
		if (this.m_animating)
		{
			Log.Store.Print("AdventureStore.BuyWithMoney failed: still animating", Array.Empty<object>());
			return;
		}
		if (this.m_bundle == null)
		{
			Log.Store.PrintError("AdventureStore.BuyWithMoney failed: Bundle is null", Array.Empty<object>());
			return;
		}
		base.FireBuyWithMoneyEvent(this.m_bundle, 1);
	}

	// Token: 0x06006273 RID: 25203 RVA: 0x0020203C File Offset: 0x0020023C
	protected override void BuyWithVirtualCurrency(UIEvent e)
	{
		if (this.m_animating)
		{
			Log.Store.Print("AdventureStore.BuyWithVirtualCurrency failed: still animating", Array.Empty<object>());
			return;
		}
		if (this.m_bundle == null)
		{
			Log.Store.PrintError("AdventureStore.BuyWithVirtualCurrency failed: Bundle is null", Array.Empty<object>());
			return;
		}
		CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(this.m_bundle);
		if (bundleVirtualCurrencyPriceType == CurrencyType.NONE)
		{
			Log.Store.PrintError("BuyWithVirtualCurrency failed: Bundle not available for virtual currency", Array.Empty<object>());
			return;
		}
		base.FireBuyWithVirtualCurrencyEvent(this.m_bundle, bundleVirtualCurrencyPriceType, 1);
	}

	// Token: 0x06006274 RID: 25204 RVA: 0x002020B8 File Offset: 0x002002B8
	protected override void RefreshBuyButtonStates(Network.Bundle bundle, NoGTAPPTransactionData transaction)
	{
		base.RefreshBuyButtonStates(bundle, transaction);
		if (this.m_BuyWithMoneyButtonOpaqueCover != null)
		{
			bool flag = this.m_buyWithMoneyButton != null && this.m_buyWithMoneyButton.gameObject.activeInHierarchy;
			bool flag2 = this.m_buyWithVCButton != null && this.m_buyWithVCButton.gameObject.activeInHierarchy;
			bool active = false;
			if (flag && base.GetMoneyButtonState() == Store.BuyButtonState.DISABLED_NO_TOOLTIP)
			{
				active = true;
			}
			if (flag2 && base.GetVCButtonState() == Store.BuyButtonState.DISABLED_NO_TOOLTIP)
			{
				active = true;
			}
			this.m_BuyWithMoneyButtonOpaqueCover.SetActive(active);
		}
		if (this.m_BuyWithGoldButtonOpaqueCover != null)
		{
			bool active2 = base.GetGoldButtonState() == Store.BuyButtonState.DISABLED_NO_TOOLTIP;
			this.m_BuyWithGoldButtonOpaqueCover.SetActive(active2);
		}
		this.RefreshBuyFullAdventureButton();
	}

	// Token: 0x06006275 RID: 25205 RVA: 0x00202171 File Offset: 0x00200371
	private void OnAuthExit()
	{
		base.BlockInterface(false);
		SceneUtils.SetLayer(base.gameObject, GameLayer.Default);
		base.EnableFullScreenEffects(false);
		StoreManager.Get().RemoveAuthorizationExitListener(new Action(this.OnAuthExit));
		base.FireExitEvent(true);
		this.Hide();
	}

	// Token: 0x06006276 RID: 25206 RVA: 0x002021B0 File Offset: 0x002003B0
	private void OnSuccessfulPurchase(Network.Bundle bundle, PaymentMethod method)
	{
		base.BlockInterface(false);
		base.EnableFullScreenEffects(false);
		base.FireExitEvent(true);
		this.Hide();
	}

	// Token: 0x06006277 RID: 25207 RVA: 0x002021CD File Offset: 0x002003CD
	private void SetUpBuyButtons()
	{
		this.SetUpBuyWithGoldButton();
		this.SetUpBuyWithMoneyButton();
		this.SetUpBuyFullAdventureButton();
		this.RefreshBuyButtonStates(this.m_bundle, null);
	}

	// Token: 0x06006278 RID: 25208 RVA: 0x002021F0 File Offset: 0x002003F0
	private void SetUpBuyWithGoldButton()
	{
		string text = string.Empty;
		if (this.m_bundle != null)
		{
			text = this.m_bundle.GtappGoldCost.ToString();
		}
		else
		{
			Debug.LogWarning("AdventureStore.SetUpBuyWithGoldButton(): m_bundle is null");
			text = GameStrings.Get("GLUE_STORE_PRODUCT_PRICE_NA");
		}
		if (this.m_buyWithGoldButton != null)
		{
			this.m_buyWithGoldButton.SetText(text);
		}
	}

	// Token: 0x06006279 RID: 25209 RVA: 0x00202258 File Offset: 0x00200458
	private void SetUpBuyWithMoneyButton()
	{
		string text = string.Empty;
		if (this.m_bundle != null)
		{
			text = StoreManager.Get().FormatCostBundle(this.m_bundle);
		}
		else
		{
			Debug.LogWarning("AdventureStore.SetUpBuyWithMoneyButton(): m_bundle is null");
			text = GameStrings.Get("GLUE_STORE_PRODUCT_PRICE_NA");
		}
		this.m_buyWithMoneyButton.SetText(text);
	}

	// Token: 0x0600627A RID: 25210 RVA: 0x002022A7 File Offset: 0x002004A7
	private void SetUpBuyFullAdventureButton()
	{
		this.RefreshBuyFullAdventureButton();
	}

	// Token: 0x0600627B RID: 25211 RVA: 0x002022B0 File Offset: 0x002004B0
	private void RefreshBuyFullAdventureButton()
	{
		if (this.m_fullAdventureBundle != null && !StoreManager.Get().CanBuyBundle(this.m_fullAdventureBundle))
		{
			Log.Store.PrintWarning("CanBuyBundle is false for m_fullAdventureBundle, PMTProductID = {0}", new object[]
			{
				this.m_fullAdventureBundle.PMTProductID.GetValueOrDefault()
			});
			this.m_fullAdventureBundle = null;
		}
		string text = string.Empty;
		bool flag = false;
		string text2 = null;
		CurrencyType currencyType = CurrencyType.NONE;
		long num = 0L;
		if (this.m_fullAdventureBundle != null)
		{
			currencyType = ShopUtils.GetBundleVirtualCurrencyPriceType(this.m_fullAdventureBundle);
			if (currencyType != CurrencyType.NONE)
			{
				num = this.m_fullAdventureBundle.VirtualCurrencyCost.GetValueOrDefault();
				if (currencyType != CurrencyType.RUNESTONES)
				{
					if (currencyType == CurrencyType.ARCANE_ORBS)
					{
						text2 = GameStrings.Format("GLUE_SHOP_PRICE_ARCANE_ORBS", new object[]
						{
							num
						});
					}
				}
				else
				{
					text2 = GameStrings.Format("GLUE_SHOP_PRICE_RUNESTONES", new object[]
					{
						num
					});
				}
			}
			else if (this.m_fullAdventureBundle.Cost != null)
			{
				text2 = StoreManager.Get().FormatCostBundle(this.m_fullAdventureBundle);
			}
		}
		if (text2 != null)
		{
			string arg = GameStrings.Get("GLUE_STORE_DUNGEON_BUTTON_TEXT");
			string arg2 = GameStrings.Format("GLUE_STORE_DUNGEON_BUTTON_COST_TEXT", new object[]
			{
				this.m_fullAdventureBundle.Items.Count,
				text2
			});
			text = string.Format("{0}\n{1}", arg, arg2);
		}
		else
		{
			flag = true;
			text = string.Empty;
		}
		if (this.m_FullAdventureBundleCurrencyIcon != null)
		{
			this.m_FullAdventureBundleCurrencyIcon.BindDataModel(new PriceDataModel
			{
				Currency = currencyType,
				Amount = (float)num,
				DisplayText = num.ToString()
			}, false);
		}
		if (this.m_BuyDungeonButton != null)
		{
			this.m_BuyDungeonButton.SetText(text);
		}
		if (this.m_BuyDungeonButtonOpaqueCover != null)
		{
			this.m_BuyDungeonButtonOpaqueCover.SetActive(flag);
		}
		if (this.m_BuyDungeonButton != null)
		{
			this.m_BuyDungeonButton.SetEnabled(!flag, false);
		}
	}

	// Token: 0x0600627C RID: 25212 RVA: 0x002024A0 File Offset: 0x002006A0
	private void OnBuyDungeonButtonReleased(UIEvent e)
	{
		if (this.m_animating)
		{
			Log.Store.Print("AdventureStore.OnBuyDungeonButtonReleased failed: still animating", Array.Empty<object>());
			return;
		}
		if (this.m_fullAdventureBundle == null)
		{
			Log.Store.PrintError("AdventureStore.OnBuyDungeonButtonReleased failed: m_fullAdventureBundle is null", Array.Empty<object>());
			return;
		}
		CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(this.m_fullAdventureBundle);
		if (bundleVirtualCurrencyPriceType != CurrencyType.NONE)
		{
			base.FireBuyWithVirtualCurrencyEvent(this.m_fullAdventureBundle, bundleVirtualCurrencyPriceType, 1);
			return;
		}
		if (this.m_fullAdventureBundle.Cost != null)
		{
			base.FireBuyWithMoneyEvent(this.m_fullAdventureBundle, 1);
			return;
		}
		Log.Store.PrintError("AdventureStore.OnBuyDungeonButtonReleased failed: no valid price on m_fullAdventureBundle. PMT ID = {0}", new object[]
		{
			this.m_fullAdventureBundle.PMTProductID.GetValueOrDefault()
		});
	}

	// Token: 0x0600627D RID: 25213 RVA: 0x00202558 File Offset: 0x00200758
	private void OnBackButtonReleased(UIEvent e)
	{
		HearthstoneCheckout hearthstoneCheckout;
		if (HearthstoneServices.TryGet<HearthstoneCheckout>(out hearthstoneCheckout) && hearthstoneCheckout.CurrentState == HearthstoneCheckout.State.InProgress)
		{
			return;
		}
		this.Close();
	}

	// Token: 0x0600627E RID: 25214 RVA: 0x0020257E File Offset: 0x0020077E
	private bool OnNavigateBack()
	{
		this.Close();
		return true;
	}

	// Token: 0x0600627F RID: 25215 RVA: 0x00202587 File Offset: 0x00200787
	private void OnOrderComplete(HearthstoneCheckoutTransactionData data)
	{
		if (this != null)
		{
			this.Hide();
		}
	}

	// Token: 0x040051D3 RID: 20947
	[CustomEditField(Sections = "UI")]
	public UIBButton m_BuyDungeonButton;

	// Token: 0x040051D4 RID: 20948
	[CustomEditField(Sections = "UI")]
	public UberText m_Headline;

	// Token: 0x040051D5 RID: 20949
	[CustomEditField(Sections = "UI")]
	public UberText m_DetailsText1;

	// Token: 0x040051D6 RID: 20950
	[CustomEditField(Sections = "UI")]
	public UberText m_DetailsText2;

	// Token: 0x040051D7 RID: 20951
	[CustomEditField(Sections = "UI")]
	public GameObject m_BuyWithMoneyButtonOpaqueCover;

	// Token: 0x040051D8 RID: 20952
	[CustomEditField(Sections = "UI")]
	public GameObject m_BuyWithGoldButtonOpaqueCover;

	// Token: 0x040051D9 RID: 20953
	[CustomEditField(Sections = "UI")]
	public GameObject m_BuyDungeonButtonOpaqueCover;

	// Token: 0x040051DA RID: 20954
	[CustomEditField(Sections = "UI")]
	public UIBButton m_BackButton;

	// Token: 0x040051DB RID: 20955
	[CustomEditField(Sections = "UI")]
	public WidgetInstance m_FullAdventureBundleCurrencyIcon;

	// Token: 0x040051DC RID: 20956
	private bool m_animating;

	// Token: 0x040051DD RID: 20957
	private Network.Bundle m_bundle;

	// Token: 0x040051DE RID: 20958
	private Network.Bundle m_fullAdventureBundle;
}
