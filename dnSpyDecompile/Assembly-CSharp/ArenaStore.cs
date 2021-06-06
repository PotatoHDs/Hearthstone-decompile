using System;
using System.Collections.Generic;
using PegasusUtil;
using UnityEngine;

// Token: 0x020006EA RID: 1770
public class ArenaStore : Store
{
	// Token: 0x06006298 RID: 25240 RVA: 0x00202B01 File Offset: 0x00200D01
	protected override void Start()
	{
		base.Start();
		this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackPressed));
	}

	// Token: 0x06006299 RID: 25241 RVA: 0x00202B22 File Offset: 0x00200D22
	protected override void Awake()
	{
		ArenaStore.s_instance = this;
		this.m_destroyOnSceneLoad = false;
		base.Awake();
		this.m_backButton.SetText(GameStrings.Get("GLOBAL_BACK"));
	}

	// Token: 0x0600629A RID: 25242 RVA: 0x00202B4C File Offset: 0x00200D4C
	protected override void OnDestroy()
	{
		ArenaStore.s_instance = null;
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
	}

	// Token: 0x0600629B RID: 25243 RVA: 0x00202B66 File Offset: 0x00200D66
	public static ArenaStore Get()
	{
		return ArenaStore.s_instance;
	}

	// Token: 0x0600629C RID: 25244 RVA: 0x00202B70 File Offset: 0x00200D70
	public static Network.Bundle GetDraftTicketProduct()
	{
		List<Network.Bundle> availableBundlesForProduct = StoreManager.Get().GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_DRAFT, true, 0, ArenaStore.NUM_BUNDLE_ITEMS_REQUIRED);
		if (availableBundlesForProduct.Count == 1)
		{
			Log.Store.PrintDebug("Arena Ticket Product found. PMT ID = {0}, Name = {1}", new object[]
			{
				availableBundlesForProduct[0].PMTProductID.GetValueOrDefault(),
				availableBundlesForProduct[0].DisplayName.GetString(true)
			});
			return availableBundlesForProduct[0];
		}
		if (availableBundlesForProduct.Count == 0)
		{
			Log.Store.PrintError("Arena Ticket Product not found!", Array.Empty<object>());
		}
		else
		{
			Log.Store.PrintError("Multiple Arena Ticket Products found!", Array.Empty<object>());
		}
		return null;
	}

	// Token: 0x0600629D RID: 25245 RVA: 0x00202C1C File Offset: 0x00200E1C
	public override void Hide()
	{
		if (ShownUIMgr.Get() != null)
		{
			ShownUIMgr.Get().ClearShownUI();
		}
		FriendChallengeMgr.Get().OnStoreClosed();
		StoreManager.Get().RemoveAuthorizationExitListener(new Action(this.OnAuthExit));
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		base.EnableFullScreenEffects(false);
		base.Hide();
	}

	// Token: 0x0600629E RID: 25246 RVA: 0x00202C79 File Offset: 0x00200E79
	public override void OnMoneySpent()
	{
		this.RefreshBuyButtonStates(this.m_bundle, this.m_goldTransactionData);
	}

	// Token: 0x0600629F RID: 25247 RVA: 0x00202C79 File Offset: 0x00200E79
	public override void OnGoldBalanceChanged(NetCache.NetCacheGoldBalance balance)
	{
		this.RefreshBuyButtonStates(this.m_bundle, this.m_goldTransactionData);
	}

	// Token: 0x060062A0 RID: 25248 RVA: 0x00202C8D File Offset: 0x00200E8D
	public override void OnCurrencyBalanceChanged(CurrencyBalanceChangedEventArgs args)
	{
		if (ShopUtils.IsCurrencyVirtual(args.Currency))
		{
			this.RefreshBuyButtonStates(this.m_bundle, this.m_goldTransactionData);
		}
	}

	// Token: 0x060062A1 RID: 25249 RVA: 0x00202CB0 File Offset: 0x00200EB0
	protected override void ShowImpl(bool isTotallyFake)
	{
		this.m_shown = true;
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		StoreManager.Get().RegisterAuthorizationExitListener(new Action(this.OnAuthExit));
		base.EnableFullScreenEffects(true);
		this.FindTicketProduct();
		this.SetUpBuyButtons();
		ShownUIMgr.Get().SetShownUI(ShownUIMgr.UI_WINDOW.ARENA_STORE);
		FriendChallengeMgr.Get().OnStoreOpened();
		base.DoShowAnimation(delegate()
		{
			if (isTotallyFake)
			{
				this.SilenceBuyButtons();
				this.m_infoButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInfoPressed));
			}
			this.FireOpenedEvent();
		});
	}

	// Token: 0x060062A2 RID: 25250 RVA: 0x00202D39 File Offset: 0x00200F39
	protected override void BuyWithGold(UIEvent e)
	{
		if (this.m_goldTransactionData == null)
		{
			return;
		}
		base.FireBuyWithGoldEventNoGTAPP(this.m_goldTransactionData);
	}

	// Token: 0x060062A3 RID: 25251 RVA: 0x00202D50 File Offset: 0x00200F50
	protected override void BuyWithMoney(UIEvent e)
	{
		if (this.m_bundle == null)
		{
			Log.Store.PrintError("ArenaStore.BuyWithMoney failed. Arena ticket product not found", Array.Empty<object>());
			return;
		}
		base.FireBuyWithMoneyEvent(this.m_bundle, 1);
	}

	// Token: 0x060062A4 RID: 25252 RVA: 0x00202D7C File Offset: 0x00200F7C
	protected override void BuyWithVirtualCurrency(UIEvent e)
	{
		if (this.m_bundle == null)
		{
			Log.Store.PrintError("ArenaStore.BuyWithVirtualCurrency failed. Arena ticket product not found", Array.Empty<object>());
			return;
		}
		base.FireBuyWithVirtualCurrencyEvent(this.m_bundle, CurrencyType.RUNESTONES, 1);
	}

	// Token: 0x060062A5 RID: 25253 RVA: 0x00202DA9 File Offset: 0x00200FA9
	private void OnAuthExit()
	{
		Navigation.Pop();
		this.ExitForgeStore(true);
	}

	// Token: 0x060062A6 RID: 25254 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnBackPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x060062A7 RID: 25255 RVA: 0x00202DB7 File Offset: 0x00200FB7
	private bool OnNavigateBack()
	{
		this.ExitForgeStore(false);
		return true;
	}

	// Token: 0x060062A8 RID: 25256 RVA: 0x00202DC1 File Offset: 0x00200FC1
	private void ExitForgeStore(bool authorizationBackButtonPressed)
	{
		base.BlockInterface(false);
		SceneUtils.SetLayer(base.gameObject, GameLayer.Default);
		base.EnableFullScreenEffects(false);
		StoreManager.Get().RemoveAuthorizationExitListener(new Action(this.OnAuthExit));
		base.FireExitEvent(authorizationBackButtonPressed);
	}

	// Token: 0x060062A9 RID: 25257 RVA: 0x00202DFA File Offset: 0x00200FFA
	private void SetUpBuyButtons()
	{
		this.SetUpBuyWithGoldButton();
		this.SetUpBuyWithMoneyButton();
		this.RefreshBuyButtonStates(this.m_bundle, this.m_goldTransactionData);
	}

	// Token: 0x060062AA RID: 25258 RVA: 0x00202E1C File Offset: 0x0020101C
	private void SetUpBuyWithGoldButton()
	{
		NoGTAPPTransactionData noGTAPPTransactionData = new NoGTAPPTransactionData
		{
			Product = ProductType.PRODUCT_TYPE_DRAFT,
			ProductData = 0,
			Quantity = 1
		};
		long num;
		string text;
		if (StoreManager.Get().GetGoldCostNoGTAPP(noGTAPPTransactionData, out num))
		{
			this.m_goldTransactionData = noGTAPPTransactionData;
			text = num.ToString();
		}
		else
		{
			Debug.LogWarning("ForgeStore.SetUpBuyWithGoldButton(): no gold price for purchase Arena without GTAPP");
			text = GameStrings.Get("GLUE_STORE_PRODUCT_PRICE_NA");
		}
		this.m_buyWithGoldButton.SetText(text);
	}

	// Token: 0x060062AB RID: 25259 RVA: 0x00202E8B File Offset: 0x0020108B
	private void FindTicketProduct()
	{
		this.m_bundle = ArenaStore.GetDraftTicketProduct();
		if (this.m_bundle == null)
		{
			return;
		}
		base.BindProductDataModel(this.m_bundle);
	}

	// Token: 0x060062AC RID: 25260 RVA: 0x00202EB0 File Offset: 0x002010B0
	private void SetUpBuyWithMoneyButton()
	{
		string text;
		if (this.m_bundle != null)
		{
			text = StoreManager.Get().FormatCostBundle(this.m_bundle);
		}
		else
		{
			text = GameStrings.Get("GLUE_STORE_PRODUCT_PRICE_NA");
		}
		this.m_buyWithMoneyButton.SetText(text);
	}

	// Token: 0x040051F5 RID: 20981
	public UIBButton m_backButton;

	// Token: 0x040051F6 RID: 20982
	public GameObject m_storeClosed;

	// Token: 0x040051F7 RID: 20983
	private static readonly int NUM_BUNDLE_ITEMS_REQUIRED = 1;

	// Token: 0x040051F8 RID: 20984
	private NoGTAPPTransactionData m_goldTransactionData;

	// Token: 0x040051F9 RID: 20985
	private Network.Bundle m_bundle;

	// Token: 0x040051FA RID: 20986
	private static ArenaStore s_instance;
}
