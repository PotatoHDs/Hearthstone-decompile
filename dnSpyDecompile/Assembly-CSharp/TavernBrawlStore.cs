using System;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000732 RID: 1842
public class TavernBrawlStore : Store
{
	// Token: 0x06006769 RID: 26473 RVA: 0x0021B3D8 File Offset: 0x002195D8
	protected override void Start()
	{
		base.Start();
		this.m_ContinueButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnContinuePressed));
		this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackPressed));
	}

	// Token: 0x0600676A RID: 26474 RVA: 0x0021B412 File Offset: 0x00219612
	protected override void Awake()
	{
		TavernBrawlStore.s_instance = this;
		this.m_destroyOnSceneLoad = false;
		base.Awake();
		this.m_backButton.SetText(GameStrings.Get("GLOBAL_BACK"));
		this.m_infoButton.GetComponent<BoxCollider>().enabled = false;
	}

	// Token: 0x0600676B RID: 26475 RVA: 0x0021B44D File Offset: 0x0021964D
	protected override void OnDestroy()
	{
		TavernBrawlStore.s_instance = null;
	}

	// Token: 0x0600676C RID: 26476 RVA: 0x0021B455 File Offset: 0x00219655
	public static TavernBrawlStore Get()
	{
		return TavernBrawlStore.s_instance;
	}

	// Token: 0x0600676D RID: 26477 RVA: 0x0021B45C File Offset: 0x0021965C
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

	// Token: 0x0600676E RID: 26478 RVA: 0x0021B4B9 File Offset: 0x002196B9
	public override void OnMoneySpent()
	{
		this.UpdateMoneyButtonState();
	}

	// Token: 0x0600676F RID: 26479 RVA: 0x0021B4C1 File Offset: 0x002196C1
	public override void OnGoldBalanceChanged(NetCache.NetCacheGoldBalance balance)
	{
		this.UpdateGoldButtonState(balance);
	}

	// Token: 0x06006770 RID: 26480 RVA: 0x0021B4CC File Offset: 0x002196CC
	protected override void ShowImpl(bool isTotallyFake)
	{
		this.m_shown = true;
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		StoreManager.Get().RegisterAuthorizationExitListener(new Action(this.OnAuthExit));
		base.EnableFullScreenEffects(true);
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(TavernBrawlManager.Get().CurrentMission().missionId);
		this.m_ChalkboardTitleText.Text = record.Name;
		this.m_ChalkboardDescriptionText.Text = ((UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(record.ShortDescription)) ? record.ShortDescription : record.Description);
		string endingTimeText = TavernBrawlManager.Get().EndingTimeText;
		this.m_EndsInTextPaper.Text = endingTimeText;
		this.m_EndsInTextChalk.Text = endingTimeText;
		MeshRenderer chalkboardMesh = this.m_ChalkboardMesh;
		Material material = (chalkboardMesh != null) ? chalkboardMesh.GetSharedMaterial() : null;
		if (material != null)
		{
			material.SetTexture("_MainTex", TavernBrawlDisplay.Get().m_chalkboardTexture);
		}
		this.BindTavernBrawlData();
		this.BindTicketProduct();
		this.SetUpBuyButtons();
		ShownUIMgr.Get().SetShownUI(ShownUIMgr.UI_WINDOW.TAVERN_BRAWL_STORE);
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

	// Token: 0x06006771 RID: 26481 RVA: 0x0021B61B File Offset: 0x0021981B
	protected override void BuyWithGold(UIEvent e)
	{
		if (this.m_bundle == null)
		{
			Log.Store.PrintError("TavernBrawlStore.BuyWithGold failed. Brawl ticket product not found", Array.Empty<object>());
			return;
		}
		base.FireBuyWithGoldEventGTAPP(this.m_bundle, 1);
	}

	// Token: 0x06006772 RID: 26482 RVA: 0x0021B647 File Offset: 0x00219847
	protected override void BuyWithMoney(UIEvent e)
	{
		if (this.m_bundle == null)
		{
			Log.Store.PrintError("TavernBrawlStore.BuyWithMoney failed. Brawl ticket product not found", Array.Empty<object>());
			return;
		}
		base.FireBuyWithMoneyEvent(this.m_bundle, 1);
	}

	// Token: 0x06006773 RID: 26483 RVA: 0x0021B673 File Offset: 0x00219873
	protected override void BuyWithVirtualCurrency(UIEvent e)
	{
		if (this.m_bundle == null)
		{
			Log.Store.PrintError("TavernBrawlStore.BuyWithVirtualCurrency failed. Brawl ticket product not found", Array.Empty<object>());
			return;
		}
		base.FireBuyWithVirtualCurrencyEvent(this.m_bundle, global::CurrencyType.RUNESTONES, 1);
	}

	// Token: 0x06006774 RID: 26484 RVA: 0x0021B6A0 File Offset: 0x002198A0
	private void OnAuthExit()
	{
		Navigation.Pop();
		this.ExitTavernBrawlStore(true);
	}

	// Token: 0x06006775 RID: 26485 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnBackPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x06006776 RID: 26486 RVA: 0x0021B6B0 File Offset: 0x002198B0
	private void OnContinuePressed(UIEvent e)
	{
		this.m_ButtonFlipper.SendEvent("Flip");
		this.m_PaperEffect.SendEvent("BurnAway");
		this.m_infoButton.GetComponent<BoxCollider>().enabled = true;
		uint sessionCount = TavernBrawlManager.Get().CurrentSession.SessionCount;
		uint freeSessions = TavernBrawlManager.Get().CurrentMission().FreeSessions;
		if (TavernBrawlManager.Get().IsEligibleForFreeTicket())
		{
			base.SetMoneyButtonState(Store.BuyButtonState.DISABLED);
			base.SetGoldButtonState(Store.BuyButtonState.DISABLED);
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_BRAWLISEUM");
			popupInfo.m_text = GameStrings.Get("GLUE_BRAWLISEUM_FREE_TICKET_BODY");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnFreePopupClosed);
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	// Token: 0x06006777 RID: 26487 RVA: 0x0021B779 File Offset: 0x00219979
	private void OnFreePopupClosed(AlertPopup.Response response, object userData)
	{
		TavernBrawlManager.Get().RequestSessionBegin();
	}

	// Token: 0x06006778 RID: 26488 RVA: 0x0021B785 File Offset: 0x00219985
	private bool OnNavigateBack()
	{
		this.ExitTavernBrawlStore(false);
		return true;
	}

	// Token: 0x06006779 RID: 26489 RVA: 0x0021B78F File Offset: 0x0021998F
	private void ExitTavernBrawlStore(bool authorizationBackButtonPressed)
	{
		base.BlockInterface(false);
		SceneUtils.SetLayer(base.gameObject, GameLayer.Default);
		base.EnableFullScreenEffects(false);
		StoreManager.Get().RemoveAuthorizationExitListener(new Action(this.OnAuthExit));
		base.FireExitEvent(authorizationBackButtonPressed);
	}

	// Token: 0x0600677A RID: 26490 RVA: 0x0021B7C8 File Offset: 0x002199C8
	private void UpdateMoneyButtonState()
	{
		Store.BuyButtonState buyButtonState = Store.BuyButtonState.ENABLED;
		if (this.m_bundle == null || !StoreManager.Get().IsOpen(true))
		{
			buyButtonState = Store.BuyButtonState.DISABLED;
			this.m_storeClosed.SetActive(true);
		}
		else if (!StoreManager.Get().IsBattlePayFeatureEnabled())
		{
			buyButtonState = Store.BuyButtonState.DISABLED_FEATURE;
		}
		else if (StoreManager.Get().IsPromptShowing)
		{
			buyButtonState = Store.BuyButtonState.DISABLED;
			base.SetGoldButtonState(buyButtonState);
		}
		else
		{
			this.m_storeClosed.SetActive(false);
		}
		base.SetMoneyButtonState(buyButtonState);
	}

	// Token: 0x0600677B RID: 26491 RVA: 0x0021B838 File Offset: 0x00219A38
	private void UpdateGoldButtonState(NetCache.NetCacheGoldBalance balance)
	{
		Store.BuyButtonState buyButtonState = Store.BuyButtonState.ENABLED;
		if (StoreManager.Get().IsPromptShowing)
		{
			buyButtonState = Store.BuyButtonState.DISABLED;
			base.SetMoneyButtonState(buyButtonState);
		}
		else if (this.m_bundle == null)
		{
			buyButtonState = Store.BuyButtonState.DISABLED;
		}
		else if (!StoreManager.Get().IsOpen(true))
		{
			buyButtonState = Store.BuyButtonState.DISABLED;
		}
		else if (!StoreManager.Get().IsBuyWithGoldFeatureEnabled())
		{
			buyButtonState = Store.BuyButtonState.DISABLED_FEATURE;
		}
		else if (!ShopUtils.BundleHasPrice(this.m_bundle, global::CurrencyType.GOLD) || !StoreManager.Get().IsBundleAvailableNow(this.m_bundle))
		{
			buyButtonState = Store.BuyButtonState.DISABLED_NO_TOOLTIP;
		}
		else if (balance == null)
		{
			buyButtonState = Store.BuyButtonState.DISABLED;
		}
		else if (balance.GetTotal() < this.m_bundle.GtappGoldCost.Value)
		{
			buyButtonState = Store.BuyButtonState.DISABLED_NOT_ENOUGH_GOLD;
		}
		base.SetGoldButtonState(buyButtonState);
	}

	// Token: 0x0600677C RID: 26492 RVA: 0x0021B8DC File Offset: 0x00219ADC
	private void BindTavernBrawlData()
	{
		WidgetTemplate componentOnSelfOrParent = SceneUtils.GetComponentOnSelfOrParent<WidgetTemplate>(base.transform);
		if (componentOnSelfOrParent != null)
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(TavernBrawlManager.Get().CurrentMission().missionId);
			TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
			TavernBrawlDetailsDataModel dataModel = new TavernBrawlDetailsDataModel
			{
				BrawlType = mission.BrawlType,
				BrawlMode = mission.brawlMode,
				FormatType = mission.formatType,
				TicketType = mission.ticketType,
				MaxWins = mission.maxWins,
				MaxLosses = mission.maxLosses,
				PopupType = mission.tavernBrawlSpec.StorePopupType,
				Title = record.Name,
				RulesDesc = ((UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(record.ShortDescription)) ? record.ShortDescription : record.Description),
				RewardDesc = mission.tavernBrawlSpec.RewardDesc,
				MinRewardDesc = mission.tavernBrawlSpec.MinRewardDesc,
				MaxRewardDesc = mission.tavernBrawlSpec.MaxRewardDesc,
				EndConditionDesc = mission.tavernBrawlSpec.EndConditionDesc
			};
			componentOnSelfOrParent.BindDataModel(dataModel, false);
		}
	}

	// Token: 0x0600677D RID: 26493 RVA: 0x0021BA1C File Offset: 0x00219C1C
	private void BindTicketProduct()
	{
		int ticketType = TavernBrawlManager.Get().CurrentMission().ticketType;
		List<Network.Bundle> availableBundlesForProduct = StoreManager.Get().GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET, true, ticketType, TavernBrawlStore.NUM_BUNDLE_ITEMS_REQUIRED);
		if (availableBundlesForProduct.Count == 0)
		{
			this.m_bundle = null;
			return;
		}
		this.m_bundle = availableBundlesForProduct[0];
		base.BindProductDataModel(this.m_bundle);
	}

	// Token: 0x0600677E RID: 26494 RVA: 0x0021BA76 File Offset: 0x00219C76
	private void SetUpBuyButtons()
	{
		this.SetUpBuyWithGoldButton();
		this.SetUpBuyWithMoneyButton();
	}

	// Token: 0x0600677F RID: 26495 RVA: 0x0021BA84 File Offset: 0x00219C84
	private void SetUpBuyWithGoldButton()
	{
		if (this.m_bundle != null && this.m_bundle.GtappGoldCost != null)
		{
			NetCache.NetCacheGoldBalance netObject = NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>();
			this.UpdateGoldButtonState(netObject);
			return;
		}
		Debug.LogWarningFormat("TavernBrawlStore.SetUpBuyWithGoldButton(): no gold cost (bundle={0} hasGoldCost={1})", new object[]
		{
			(this.m_bundle == null) ? "<null>" : "<not null>",
			(this.m_bundle == null || this.m_bundle.GtappGoldCost == null) ? "<no value>" : this.m_bundle.GtappGoldCost.Value.ToString()
		});
		base.SetGoldButtonState(Store.BuyButtonState.DISABLED);
	}

	// Token: 0x06006780 RID: 26496 RVA: 0x0021BB31 File Offset: 0x00219D31
	private void SetUpBuyWithMoneyButton()
	{
		if (this.m_bundle != null)
		{
			this.UpdateMoneyButtonState();
			return;
		}
		Debug.LogWarning("TavernBrawlStore.SetUpBuyWithMoneyButton(): m_bundle is null");
		base.SetMoneyButtonState(Store.BuyButtonState.DISABLED);
	}

	// Token: 0x0400552D RID: 21805
	public UIBButton m_ContinueButton;

	// Token: 0x0400552E RID: 21806
	public UIBButton m_backButton;

	// Token: 0x0400552F RID: 21807
	public GameObject m_storeClosed;

	// Token: 0x04005530 RID: 21808
	public PlayMakerFSM m_ButtonFlipper;

	// Token: 0x04005531 RID: 21809
	public PlayMakerFSM m_PaperEffect;

	// Token: 0x04005532 RID: 21810
	public UberText m_EndsInTextPaper;

	// Token: 0x04005533 RID: 21811
	public UberText m_EndsInTextChalk;

	// Token: 0x04005534 RID: 21812
	public UberText m_ChalkboardTitleText;

	// Token: 0x04005535 RID: 21813
	public UberText m_ChalkboardDescriptionText;

	// Token: 0x04005536 RID: 21814
	public MeshRenderer m_ChalkboardMesh;

	// Token: 0x04005537 RID: 21815
	private static readonly int NUM_BUNDLE_ITEMS_REQUIRED = 1;

	// Token: 0x04005538 RID: 21816
	private Network.Bundle m_bundle;

	// Token: 0x04005539 RID: 21817
	private static TavernBrawlStore s_instance;
}
