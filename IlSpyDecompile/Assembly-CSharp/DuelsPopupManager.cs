using System;
using System.Collections.Generic;
using Assets;
using bgs;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

public class DuelsPopupManager : MonoBehaviour, IStore
{
	private const string HEROIC_STORE_OPENED_EVENT = "HEROIC_STORE_OPENED";

	private const string OPEN_EVENT = "OPEN";

	private const string HIDE_EVENT = "HIDE";

	private const string BLOCK_SCREEN = "BLOCK_SCREEN";

	private const string UNBLOCK_SCREEN = "UNBLOCK_SCREEN";

	private const string OPEN_NOTICE = "OPEN_NOTICE";

	private const string PURCHASE_ACKNOWLEDGED = "PURCHASE_ACKNOWLEDGED";

	private const string SHOW_COIN_COUNTER_EVENT = "SHOW_COIN_COUNTER";

	private const string HIDE_COIN_COUNTER_EVENT = "HIDE_COIN_COUNTER";

	public AsyncReference m_buywithCurrencyReference;

	public AsyncReference m_buyWithGoldReference;

	public AsyncReference m_buyWithVCReference;

	public AsyncReference m_buyWithTicketReference;

	public AsyncReference m_normalButtonReference;

	public AsyncReference m_visualControllerReference;

	public AsyncReference m_fullScreenBlockerWidgetReference;

	public AsyncReference m_noticePopupConfirmReference;

	public AsyncReference m_infoButtonReference;

	public Material m_disabledButtonMaterial;

	private VisualController m_visualController;

	private Widget m_fullScreenBlockerWidget;

	private Action m_normalButtonPressedDelegate;

	private Action m_purchaseSuccessfulDelegate;

	private bool m_isStoreOpen;

	private bool m_isArenaTicketTransactionActive;

	private bool m_isVCPurchaseTransactionActive;

	private bool m_VCPurchaseSucceeded;

	private bool m_shouldShowCurrency;

	private Network.Bundle m_arenaTicketBundle;

	private Action m_noticeConfirmPressedDelegate;

	private Widget m_popupManagerWidget;

	private PVPDRLobbyDataModel m_dataModel;

	private bool IsAnyDuelsTransactionActive
	{
		get
		{
			if (!m_isArenaTicketTransactionActive)
			{
				return m_isVCPurchaseTransactionActive;
			}
			return true;
		}
	}

	public event Action OnOpened;

	public event Action<StoreClosedArgs> OnClosed;

	public event Action OnReady;

	public event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	public void Start()
	{
		m_buywithCurrencyReference.RegisterReadyListener<UIBButton>(OnBuyWithCurrencyButtonReady);
		m_buyWithGoldReference.RegisterReadyListener<UIBButton>(OnBuyWithGoldButtonReady);
		m_buyWithVCReference.RegisterReadyListener<UIBButton>(OnBuyWithVCButtonReady);
		m_buyWithTicketReference.RegisterReadyListener<UIBButton>(OnBuyWithTicketButtonReady);
		m_normalButtonReference.RegisterReadyListener<Clickable>(OnNormalButtonReady);
		m_visualControllerReference.RegisterReadyListener<VisualController>(OnVisualControllerReady);
		m_fullScreenBlockerWidgetReference.RegisterReadyListener<Widget>(OnFullScreenBlockerWidgetReady);
		m_noticePopupConfirmReference.RegisterReadyListener<UIBButton>(OnNoticePopupButtonReady);
		m_infoButtonReference.RegisterReadyListener<UIBButton>(OnInfoButtonReady);
		m_arenaTicketBundle = ArenaStore.GetDraftTicketProduct();
		m_popupManagerWidget = GetComponentInParent<Widget>();
		m_popupManagerWidget.RegisterEventListener(OnWidgetEvent);
		BindProductDataModel();
	}

	private void BindProductDataModel()
	{
		ProductDataModel productDataModel = null;
		if (m_arenaTicketBundle != null && m_arenaTicketBundle.PMTProductID.HasValue)
		{
			productDataModel = StoreManager.Get().Catalog.GetProductByPmtId(m_arenaTicketBundle.PMTProductID.Value);
		}
		productDataModel = productDataModel ?? ProductFactory.CreateEmptyProductDataModel();
		m_popupManagerWidget.BindDataModel(productDataModel, overrideChildren: true);
	}

	private void OnBuyWithCurrencyButtonReady(UIBButton button)
	{
		button.AddEventListener(UIEventType.PRESS, OnBuyWithCurrencyButtonPressed);
		bool flag = StoreManager.Get().IsOpen(printStatus: false);
		button.SetEnabled(flag);
		if (!flag)
		{
			button.m_RootObject.GetComponent<MeshRenderer>().SetMaterial(m_disabledButtonMaterial);
		}
	}

	private void OnBuyWithCurrencyButtonPressed(UIEvent buttonEvent)
	{
		if (m_arenaTicketBundle == null)
		{
			Debug.LogError("Failed to perform Heroic Duel transaction because the arena ticket bundle was null");
			return;
		}
		BuyProductEventArgs purchaseEventArgs = new BuyPmtProductEventArgs(m_arenaTicketBundle, CurrencyType.REAL_MONEY, 1);
		StartArenaTicketTransaction(purchaseEventArgs);
	}

	private void OnBuyWithVCButtonReady(UIBButton button)
	{
		button.AddEventListener(UIEventType.PRESS, OnBuyWithVCButtonPressed);
		bool flag = StoreManager.Get().IsOpen(printStatus: false);
		button.SetEnabled(flag);
		if (!flag)
		{
			button.m_RootObject.GetComponent<MeshRenderer>().SetMaterial(m_disabledButtonMaterial);
		}
	}

	private void OnBuyWithVCButtonPressed(UIEvent buttonEvent)
	{
		if (m_arenaTicketBundle == null)
		{
			Debug.LogError("Failed to perform Heroic Duel transaction because the arena ticket bundle was null");
			return;
		}
		CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(m_arenaTicketBundle);
		long deficitForVCPurchase = GetDeficitForVCPurchase(bundleVirtualCurrencyPriceType);
		if (deficitForVCPurchase > 0)
		{
			ShowVCPurchaseConfirmationPrompt(bundleVirtualCurrencyPriceType, deficitForVCPurchase);
			return;
		}
		BuyProductEventArgs purchaseEventArgs = new BuyPmtProductEventArgs(m_arenaTicketBundle, bundleVirtualCurrencyPriceType, 1);
		StartArenaTicketTransaction(purchaseEventArgs);
	}

	private void OnBuyWithGoldButtonReady(UIBButton button)
	{
		button.AddEventListener(UIEventType.PRESS, OnBuyWithGoldButtonPressed);
		bool flag = NetCache.Get().GetGoldBalance() >= DuelsConfig.PAID_GOLD_COST && StoreManager.Get().IsOpen(printStatus: false);
		button.SetEnabled(flag);
		if (!flag)
		{
			button.m_RootObject.GetComponent<MeshRenderer>().SetMaterial(m_disabledButtonMaterial);
		}
	}

	private void OnBuyWithGoldButtonPressed(UIEvent buttonEvent)
	{
		BuyProductEventArgs purchaseEventArgs = new BuyNoGTAPPEventArgs(new NoGTAPPTransactionData
		{
			Product = ProductType.PRODUCT_TYPE_DRAFT,
			ProductData = 0,
			Quantity = 1
		});
		StartArenaTicketTransaction(purchaseEventArgs);
	}

	private void OnBuyWithTicketButtonReady(UIBButton button)
	{
		button.AddEventListener(UIEventType.PRESS, OnBuyWithTicketButtonPressed);
	}

	private void OnBuyWithTicketButtonPressed(UIEvent buttonEvent)
	{
		m_visualController.SetState("PURCHASE_ACKNOWLEDGED");
		m_purchaseSuccessfulDelegate();
	}

	private void OnNormalButtonReady(Clickable button)
	{
		button.AddEventListener(UIEventType.PRESS, OnNormalButtonPressed);
	}

	private void OnNormalButtonPressed(UIEvent buttonEvent)
	{
		if (PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsEarlyAccess && !DuelsConfig.HasEarlyAccess())
		{
			OpenDuelsShop();
			Hide();
		}
		else
		{
			m_normalButtonPressedDelegate();
		}
	}

	private void OnVisualControllerReady(VisualController visualController)
	{
		m_visualController = visualController;
		m_visualController.RegisterStateChangedListener(OnStateChanged);
		if (this.OnReady != null)
		{
			this.OnReady();
		}
	}

	private void OnFullScreenBlockerWidgetReady(Widget fullScreenBlockerWidget)
	{
		m_fullScreenBlockerWidget = fullScreenBlockerWidget;
	}

	private void OnNoticePopupButtonReady(UIBButton confirmButton)
	{
		confirmButton.AddEventListener(UIEventType.PRESS, OnNoticeConfirmPressed);
	}

	private void OnInfoButtonReady(UIBButton infoButton)
	{
		infoButton.AddEventListener(UIEventType.PRESS, OnInfoButtonPressed);
	}

	private void OnInfoButtonPressed(UIEvent e)
	{
		StoreManager.Get().ShowStoreInfo();
	}

	private void OnNoticeConfirmPressed(UIEvent buttonEvent)
	{
		Hide();
		m_noticeConfirmPressedDelegate();
		m_noticeConfirmPressedDelegate = null;
	}

	private void OnStateChanged(VisualController visualController)
	{
		GetDataModel();
		if (visualController.State == "OPEN")
		{
			SetupDuelsStore();
		}
		else if (visualController.State == "HIDE")
		{
			ShutdownDuelsStore();
		}
	}

	protected void OnWidgetEvent(string eventName)
	{
		if (eventName == "SHOW_COIN_COUNTER")
		{
			m_shouldShowCurrency = true;
			BnetBar.Get()?.RefreshCurrency();
		}
		else if (eventName == "HIDE_COIN_COUNTER")
		{
			m_shouldShowCurrency = false;
			BnetBar.Get()?.RefreshCurrency();
		}
	}

	public bool ShouldShowCoinCounter()
	{
		if (m_shouldShowCurrency)
		{
			return m_isStoreOpen;
		}
		return false;
	}

	public void Show()
	{
		m_visualController.SetState("OPEN");
	}

	public void ShowNotice(string header, string desc, string rating, Action callback)
	{
		AddOnNoticeConfirmButtonPressedDelegate(callback);
		SetNoticeText(header, desc, rating);
		m_visualController.SetState("OPEN_NOTICE");
	}

	public void Hide()
	{
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.DUELS_IDLE);
		m_visualController.SetState("HIDE");
	}

	public void AddOnNormalButtonPressedDelegate(Action action)
	{
		m_normalButtonPressedDelegate = (Action)Delegate.Combine(m_normalButtonPressedDelegate, action);
	}

	public void RemoveOnNormalButtonPressedDelegate(Action action)
	{
		m_normalButtonPressedDelegate = (Action)Delegate.Remove(m_normalButtonPressedDelegate, action);
	}

	public void AddOnSuccessfulPurchaseDelegate(Action action)
	{
		m_purchaseSuccessfulDelegate = (Action)Delegate.Combine(m_purchaseSuccessfulDelegate, action);
	}

	public void RemoveOnSuccessfulPurchaseDelegate(Action action)
	{
		m_purchaseSuccessfulDelegate = (Action)Delegate.Remove(m_purchaseSuccessfulDelegate, action);
	}

	public void AddOnNoticeConfirmButtonPressedDelegate(Action action)
	{
		m_noticeConfirmPressedDelegate = (Action)Delegate.Combine(m_noticeConfirmPressedDelegate, action);
	}

	public void RemoveOnNoticeConfirmButtonPressedDelegate(Action action)
	{
		m_noticeConfirmPressedDelegate = (Action)Delegate.Remove(m_noticeConfirmPressedDelegate, action);
	}

	public void SetNoticeText(string header, string desc, string ratingText = "")
	{
		if (GetDataModel() != null)
		{
			m_dataModel.NoticeHeaderString = header;
			m_dataModel.NoticeDescString = desc;
			if (!string.IsNullOrEmpty(ratingText))
			{
				m_dataModel.NoticeRatingString = ratingText;
			}
		}
	}

	private PVPDRLobbyDataModel GetDataModel()
	{
		if (m_dataModel == null && PvPDungeonRunDisplay.Get() != null && m_popupManagerWidget != null)
		{
			m_dataModel = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel();
			m_popupManagerWidget.BindDataModel(m_dataModel);
		}
		return m_dataModel;
	}

	private void SetupDuelsStore()
	{
		StoreManager.Get().SetupDuelsStore(this);
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
		StoreManager.Get().RegisterFailedPurchaseAckListener(OnFailedPurchaseAck);
		m_isStoreOpen = true;
		this.OnOpened?.Invoke();
		BnetBar.Get()?.RefreshCurrency();
	}

	private void ShutdownDuelsStore()
	{
		CancelArenaTicketTransaction();
		CancelVCPurchaseTransaction();
		this.OnClosed?.Invoke(new StoreClosedArgs());
		StoreManager.Get().RemoveFailedPurchaseAckListener(OnFailedPurchaseAck);
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
		StoreManager.Get().ShutDownDuelsStore();
		this.OnProductPurchaseAttempt = null;
		m_isStoreOpen = false;
		BlockInputs(blocked: false);
		PvPDungeonRunDisplay.Get().EnableButtons();
		BnetBar.Get()?.RefreshCurrency();
	}

	private long GetDeficitForVCPurchase(CurrencyType currency)
	{
		long result = 0L;
		if (m_arenaTicketBundle == null)
		{
			Debug.LogError("Failed to calculate VC deficit because m_arenaTicketBundle was null");
			return result;
		}
		if (!m_arenaTicketBundle.VirtualCurrencyCost.HasValue)
		{
			Debug.LogError("Failed to calculate VC deficit because m_arenaTicketBundle.VirtualCurrencyCost was null");
			return result;
		}
		return ShopUtils.GetDeficit(new PriceDataModel
		{
			Currency = currency,
			Amount = m_arenaTicketBundle.VirtualCurrencyCost.Value
		});
	}

	private void ShowVCPurchaseConfirmationPrompt(CurrencyType currencyType, long vcAmount)
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES"),
			m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NOT_NOW"),
			m_alertTextAlignment = UberText.AlignmentOptions.Center,
			m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle,
			m_headerText = GameStrings.Get("GLUE_SHOP_GET_MORE_RUNESTONES_HEADER"),
			m_text = GameStrings.Get("GLUE_SHOP_GET_MORE_RUNESTONES"),
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					StartVCPurchaseTransaction(currencyType, vcAmount);
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	private void StartVCPurchaseTransaction(CurrencyType currencyType, long vcAmount)
	{
		if (IsAnyDuelsTransactionActive)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DUELS_HEADLINE");
			popupInfo.m_text = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			Debug.LogError("Attempted to start a virtual currency purchase transaction while an existing transaction was in progress");
			return;
		}
		m_isVCPurchaseTransactionActive = true;
		m_VCPurchaseSucceeded = false;
		switch (currencyType)
		{
		case CurrencyType.RUNESTONES:
			Shop.Get().OpenVirtualCurrencyPurchase(vcAmount, rememberLastPage: true);
			break;
		case CurrencyType.ARCANE_ORBS:
			Shop.Get().OpenBoosterCurrencyPurchase(vcAmount, rememberLastPage: true);
			break;
		}
	}

	private void EndVCPurchaseTransaction()
	{
		if (m_isVCPurchaseTransactionActive)
		{
			m_isVCPurchaseTransactionActive = false;
			if (m_VCPurchaseSucceeded)
			{
				m_VCPurchaseSucceeded = false;
				CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(m_arenaTicketBundle);
				BuyProductEventArgs purchaseEventArgs = new BuyPmtProductEventArgs(m_arenaTicketBundle, bundleVirtualCurrencyPriceType, 1);
				StartArenaTicketTransaction(purchaseEventArgs);
			}
		}
	}

	private void CancelVCPurchaseTransaction()
	{
		if (m_isVCPurchaseTransactionActive)
		{
			m_VCPurchaseSucceeded = false;
			EndVCPurchaseTransaction();
		}
	}

	private void StartArenaTicketTransaction(BuyProductEventArgs purchaseEventArgs)
	{
		if (IsAnyDuelsTransactionActive)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DUELS_HEADLINE");
			popupInfo.m_text = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			Debug.LogError("Attempted to start an arena ticket transaction while an existing transaction was in progress");
		}
		else if (this.OnProductPurchaseAttempt == null)
		{
			Debug.LogError("Attempted to start an arena ticket transaction while OnProductPurchaseAttempt was null");
		}
		else
		{
			m_isArenaTicketTransactionActive = true;
			this.OnProductPurchaseAttempt(purchaseEventArgs);
		}
	}

	private void EndArenaTicketTransaction()
	{
		if (m_isArenaTicketTransactionActive)
		{
			m_isArenaTicketTransactionActive = false;
		}
	}

	private void CancelArenaTicketTransaction()
	{
		if (m_isArenaTicketTransactionActive)
		{
			EndArenaTicketTransaction();
		}
	}

	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		if (m_isArenaTicketTransactionActive)
		{
			EndArenaTicketTransaction();
			BlockInputs(blocked: false);
			m_visualController.SetState("PURCHASE_ACKNOWLEDGED");
			m_purchaseSuccessfulDelegate();
		}
		else if (m_isVCPurchaseTransactionActive)
		{
			m_VCPurchaseSucceeded = true;
			EndVCPurchaseTransaction();
		}
	}

	private void OnFailedPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		if (m_isArenaTicketTransactionActive)
		{
			EndArenaTicketTransaction();
		}
		else if (m_isVCPurchaseTransactionActive)
		{
			EndVCPurchaseTransaction();
		}
	}

	private void OpenDuelsShop()
	{
		int earlyAccessPMTLicenseId = (int)NetCache.Get().GetDuelsEarlyAccessLicenseId();
		AccountLicenseDbfRecord record = GameDbf.AccountLicense.GetRecord((AccountLicenseDbfRecord rec) => earlyAccessPMTLicenseId == rec.LicenseId);
		if (record == null)
		{
			Debug.LogWarning("DuelsPopupManager::OpenDuelsShop() - Duels early access account license not found.");
			ShowallDuelsBundlesErrorPopup();
		}
		List<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(ProductType.PRODUCT_TYPE_FIXED_LICENSE, requireRealMoneyOption: false, record.ID);
		if (allBundlesForProduct == null || allBundlesForProduct.Count == 0)
		{
			Debug.LogWarning("DuelsPopupManager::OpenDuelsShop() - No products in the shop have Duels early access.");
			ShowallDuelsBundlesErrorPopup();
			return;
		}
		int index = (int)(BattleNet.GetMyAccoundId().lo % (ulong)allBundlesForProduct.Count);
		Network.Bundle bundle = allBundlesForProduct[index];
		if (bundle == null || !bundle.PMTProductID.HasValue)
		{
			Debug.LogWarning("DuelsPopupManager::OpenDuelsShop() - Duels product has no PMT Product Id.");
			ShowallDuelsBundlesErrorPopup();
		}
		else
		{
			Shop.OpenToProductPageWhenReady(bundle.PMTProductID.Value, suppressBox: true);
		}
	}

	private void ShowallDuelsBundlesErrorPopup()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_PVPDR"),
			m_text = GameStrings.Get("GLUE_PVPDR_EARLY_ACCESS_SHOP_ERROR_BODY"),
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		};
		DialogManager.Get().ShowPopup(info);
	}

	private void BlockInputs(bool blocked)
	{
		Widget.TriggerEventParameters parameters;
		if (m_fullScreenBlockerWidget == null)
		{
			Debug.LogError("Failed to toggle interface blocker from Duels Popup Manager");
		}
		else if (blocked)
		{
			Widget fullScreenBlockerWidget = m_fullScreenBlockerWidget;
			parameters = new Widget.TriggerEventParameters
			{
				IgnorePlaymaker = true,
				NoDownwardPropagation = false
			};
			fullScreenBlockerWidget.TriggerEvent("BLOCK_SCREEN", parameters);
		}
		else
		{
			Widget fullScreenBlockerWidget2 = m_fullScreenBlockerWidget;
			parameters = new Widget.TriggerEventParameters
			{
				IgnorePlaymaker = true,
				NoDownwardPropagation = false
			};
			fullScreenBlockerWidget2.TriggerEvent("UNBLOCK_SCREEN", parameters);
		}
	}

	void IStore.Open()
	{
		Shop.Get().RefreshDataModel();
	}

	void IStore.Close()
	{
		CancelArenaTicketTransaction();
		CancelVCPurchaseTransaction();
	}

	void IStore.BlockInterface(bool blocked)
	{
		BlockInputs(blocked);
	}

	bool IStore.IsReady()
	{
		return true;
	}

	bool IStore.IsOpen()
	{
		return m_isStoreOpen;
	}

	void IStore.Unload()
	{
	}

	IEnumerable<CurrencyType> IStore.GetVisibleCurrencies()
	{
		if (!ShopUtils.IsVirtualCurrencyEnabled())
		{
			return new CurrencyType[1] { CurrencyType.GOLD };
		}
		return new CurrencyType[2]
		{
			CurrencyType.GOLD,
			CurrencyType.RUNESTONES
		};
	}
}
