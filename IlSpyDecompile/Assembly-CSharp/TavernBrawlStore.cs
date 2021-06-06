using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class TavernBrawlStore : Store
{
	public UIBButton m_ContinueButton;

	public UIBButton m_backButton;

	public GameObject m_storeClosed;

	public PlayMakerFSM m_ButtonFlipper;

	public PlayMakerFSM m_PaperEffect;

	public UberText m_EndsInTextPaper;

	public UberText m_EndsInTextChalk;

	public UberText m_ChalkboardTitleText;

	public UberText m_ChalkboardDescriptionText;

	public MeshRenderer m_ChalkboardMesh;

	private static readonly int NUM_BUNDLE_ITEMS_REQUIRED = 1;

	private Network.Bundle m_bundle;

	private static TavernBrawlStore s_instance;

	protected override void Start()
	{
		base.Start();
		m_ContinueButton.AddEventListener(UIEventType.RELEASE, OnContinuePressed);
		m_backButton.AddEventListener(UIEventType.RELEASE, OnBackPressed);
	}

	protected override void Awake()
	{
		s_instance = this;
		m_destroyOnSceneLoad = false;
		base.Awake();
		m_backButton.SetText(GameStrings.Get("GLOBAL_BACK"));
		m_infoButton.GetComponent<BoxCollider>().enabled = false;
	}

	protected override void OnDestroy()
	{
		s_instance = null;
	}

	public static TavernBrawlStore Get()
	{
		return s_instance;
	}

	public override void Hide()
	{
		if (ShownUIMgr.Get() != null)
		{
			ShownUIMgr.Get().ClearShownUI();
		}
		FriendChallengeMgr.Get().OnStoreClosed();
		StoreManager.Get().RemoveAuthorizationExitListener(OnAuthExit);
		Navigation.RemoveHandler(OnNavigateBack);
		EnableFullScreenEffects(enable: false);
		base.Hide();
	}

	public override void OnMoneySpent()
	{
		UpdateMoneyButtonState();
	}

	public override void OnGoldBalanceChanged(NetCache.NetCacheGoldBalance balance)
	{
		UpdateGoldButtonState(balance);
	}

	protected override void ShowImpl(bool isTotallyFake)
	{
		m_shown = true;
		Navigation.Push(OnNavigateBack);
		StoreManager.Get().RegisterAuthorizationExitListener(OnAuthExit);
		EnableFullScreenEffects(enable: true);
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(TavernBrawlManager.Get().CurrentMission().missionId);
		m_ChalkboardTitleText.Text = record.Name;
		m_ChalkboardDescriptionText.Text = (((bool)UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(record.ShortDescription)) ? record.ShortDescription : record.Description);
		string endingTimeText = TavernBrawlManager.Get().EndingTimeText;
		m_EndsInTextPaper.Text = endingTimeText;
		m_EndsInTextChalk.Text = endingTimeText;
		Material material = m_ChalkboardMesh?.GetSharedMaterial();
		if (material != null)
		{
			material.SetTexture("_MainTex", TavernBrawlDisplay.Get().m_chalkboardTexture);
		}
		BindTavernBrawlData();
		BindTicketProduct();
		SetUpBuyButtons();
		ShownUIMgr.Get().SetShownUI(ShownUIMgr.UI_WINDOW.TAVERN_BRAWL_STORE);
		FriendChallengeMgr.Get().OnStoreOpened();
		DoShowAnimation(delegate
		{
			if (isTotallyFake)
			{
				SilenceBuyButtons();
				m_infoButton.RemoveEventListener(UIEventType.RELEASE, base.OnInfoPressed);
			}
			FireOpenedEvent();
		});
	}

	protected override void BuyWithGold(UIEvent e)
	{
		if (m_bundle == null)
		{
			Log.Store.PrintError("TavernBrawlStore.BuyWithGold failed. Brawl ticket product not found");
		}
		else
		{
			FireBuyWithGoldEventGTAPP(m_bundle, 1);
		}
	}

	protected override void BuyWithMoney(UIEvent e)
	{
		if (m_bundle == null)
		{
			Log.Store.PrintError("TavernBrawlStore.BuyWithMoney failed. Brawl ticket product not found");
		}
		else
		{
			FireBuyWithMoneyEvent(m_bundle, 1);
		}
	}

	protected override void BuyWithVirtualCurrency(UIEvent e)
	{
		if (m_bundle == null)
		{
			Log.Store.PrintError("TavernBrawlStore.BuyWithVirtualCurrency failed. Brawl ticket product not found");
		}
		else
		{
			FireBuyWithVirtualCurrencyEvent(m_bundle, CurrencyType.RUNESTONES);
		}
	}

	private void OnAuthExit()
	{
		Navigation.Pop();
		ExitTavernBrawlStore(authorizationBackButtonPressed: true);
	}

	private void OnBackPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	private void OnContinuePressed(UIEvent e)
	{
		m_ButtonFlipper.SendEvent("Flip");
		m_PaperEffect.SendEvent("BurnAway");
		m_infoButton.GetComponent<BoxCollider>().enabled = true;
		_ = TavernBrawlManager.Get().CurrentSession.SessionCount;
		_ = TavernBrawlManager.Get().CurrentMission().FreeSessions;
		if (TavernBrawlManager.Get().IsEligibleForFreeTicket())
		{
			SetMoneyButtonState(BuyButtonState.DISABLED);
			SetGoldButtonState(BuyButtonState.DISABLED);
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLOBAL_BRAWLISEUM");
			popupInfo.m_text = GameStrings.Get("GLUE_BRAWLISEUM_FREE_TICKET_BODY");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			popupInfo.m_responseCallback = OnFreePopupClosed;
			DialogManager.Get().ShowPopup(popupInfo);
		}
	}

	private void OnFreePopupClosed(AlertPopup.Response response, object userData)
	{
		TavernBrawlManager.Get().RequestSessionBegin();
	}

	private bool OnNavigateBack()
	{
		ExitTavernBrawlStore(authorizationBackButtonPressed: false);
		return true;
	}

	private void ExitTavernBrawlStore(bool authorizationBackButtonPressed)
	{
		BlockInterface(blocked: false);
		SceneUtils.SetLayer(base.gameObject, GameLayer.Default);
		EnableFullScreenEffects(enable: false);
		StoreManager.Get().RemoveAuthorizationExitListener(OnAuthExit);
		FireExitEvent(authorizationBackButtonPressed);
	}

	private void UpdateMoneyButtonState()
	{
		BuyButtonState buyButtonState = BuyButtonState.ENABLED;
		if (m_bundle == null || !StoreManager.Get().IsOpen())
		{
			buyButtonState = BuyButtonState.DISABLED;
			m_storeClosed.SetActive(value: true);
		}
		else if (!StoreManager.Get().IsBattlePayFeatureEnabled())
		{
			buyButtonState = BuyButtonState.DISABLED_FEATURE;
		}
		else if (StoreManager.Get().IsPromptShowing)
		{
			buyButtonState = BuyButtonState.DISABLED;
			SetGoldButtonState(buyButtonState);
		}
		else
		{
			m_storeClosed.SetActive(value: false);
		}
		SetMoneyButtonState(buyButtonState);
	}

	private void UpdateGoldButtonState(NetCache.NetCacheGoldBalance balance)
	{
		BuyButtonState buyButtonState = BuyButtonState.ENABLED;
		if (StoreManager.Get().IsPromptShowing)
		{
			buyButtonState = BuyButtonState.DISABLED;
			SetMoneyButtonState(buyButtonState);
		}
		else if (m_bundle == null)
		{
			buyButtonState = BuyButtonState.DISABLED;
		}
		else if (!StoreManager.Get().IsOpen())
		{
			buyButtonState = BuyButtonState.DISABLED;
		}
		else if (!StoreManager.Get().IsBuyWithGoldFeatureEnabled())
		{
			buyButtonState = BuyButtonState.DISABLED_FEATURE;
		}
		else if (!ShopUtils.BundleHasPrice(m_bundle, CurrencyType.GOLD) || !StoreManager.Get().IsBundleAvailableNow(m_bundle))
		{
			buyButtonState = BuyButtonState.DISABLED_NO_TOOLTIP;
		}
		else if (balance == null)
		{
			buyButtonState = BuyButtonState.DISABLED;
		}
		else if (balance.GetTotal() < m_bundle.GtappGoldCost.Value)
		{
			buyButtonState = BuyButtonState.DISABLED_NOT_ENOUGH_GOLD;
		}
		SetGoldButtonState(buyButtonState);
	}

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
				RulesDesc = (((bool)UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(record.ShortDescription)) ? record.ShortDescription : record.Description),
				RewardDesc = mission.tavernBrawlSpec.RewardDesc,
				MinRewardDesc = mission.tavernBrawlSpec.MinRewardDesc,
				MaxRewardDesc = mission.tavernBrawlSpec.MaxRewardDesc,
				EndConditionDesc = mission.tavernBrawlSpec.EndConditionDesc
			};
			componentOnSelfOrParent.BindDataModel(dataModel);
		}
	}

	private void BindTicketProduct()
	{
		int ticketType = TavernBrawlManager.Get().CurrentMission().ticketType;
		List<Network.Bundle> availableBundlesForProduct = StoreManager.Get().GetAvailableBundlesForProduct(ProductType.PRODUCT_TYPE_TAVERN_BRAWL_TICKET, requireNonGoldPriceOption: true, ticketType, NUM_BUNDLE_ITEMS_REQUIRED);
		if (availableBundlesForProduct.Count == 0)
		{
			m_bundle = null;
			return;
		}
		m_bundle = availableBundlesForProduct[0];
		BindProductDataModel(m_bundle);
	}

	private void SetUpBuyButtons()
	{
		SetUpBuyWithGoldButton();
		SetUpBuyWithMoneyButton();
	}

	private void SetUpBuyWithGoldButton()
	{
		if (m_bundle != null && m_bundle.GtappGoldCost.HasValue)
		{
			NetCache.NetCacheGoldBalance netObject = NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>();
			UpdateGoldButtonState(netObject);
			return;
		}
		Debug.LogWarningFormat("TavernBrawlStore.SetUpBuyWithGoldButton(): no gold cost (bundle={0} hasGoldCost={1})", (m_bundle == null) ? "<null>" : "<not null>", (m_bundle == null || !m_bundle.GtappGoldCost.HasValue) ? "<no value>" : m_bundle.GtappGoldCost.Value.ToString());
		SetGoldButtonState(BuyButtonState.DISABLED);
	}

	private void SetUpBuyWithMoneyButton()
	{
		if (m_bundle != null)
		{
			UpdateMoneyButtonState();
			return;
		}
		Debug.LogWarning("TavernBrawlStore.SetUpBuyWithMoneyButton(): m_bundle is null");
		SetMoneyButtonState(BuyButtonState.DISABLED);
	}
}
