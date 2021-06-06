using System;
using System.Collections.Generic;
using Assets;
using bgs;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200062B RID: 1579
public class DuelsPopupManager : MonoBehaviour, IStore
{
	// Token: 0x1700052A RID: 1322
	// (get) Token: 0x06005861 RID: 22625 RVA: 0x001CDB7B File Offset: 0x001CBD7B
	private bool IsAnyDuelsTransactionActive
	{
		get
		{
			return this.m_isArenaTicketTransactionActive || this.m_isVCPurchaseTransactionActive;
		}
	}

	// Token: 0x14000035 RID: 53
	// (add) Token: 0x06005862 RID: 22626 RVA: 0x001CDB90 File Offset: 0x001CBD90
	// (remove) Token: 0x06005863 RID: 22627 RVA: 0x001CDBC8 File Offset: 0x001CBDC8
	public event Action OnOpened;

	// Token: 0x14000036 RID: 54
	// (add) Token: 0x06005864 RID: 22628 RVA: 0x001CDC00 File Offset: 0x001CBE00
	// (remove) Token: 0x06005865 RID: 22629 RVA: 0x001CDC38 File Offset: 0x001CBE38
	public event Action<StoreClosedArgs> OnClosed;

	// Token: 0x14000037 RID: 55
	// (add) Token: 0x06005866 RID: 22630 RVA: 0x001CDC70 File Offset: 0x001CBE70
	// (remove) Token: 0x06005867 RID: 22631 RVA: 0x001CDCA8 File Offset: 0x001CBEA8
	public event Action OnReady;

	// Token: 0x14000038 RID: 56
	// (add) Token: 0x06005868 RID: 22632 RVA: 0x001CDCE0 File Offset: 0x001CBEE0
	// (remove) Token: 0x06005869 RID: 22633 RVA: 0x001CDD18 File Offset: 0x001CBF18
	public event Action<BuyProductEventArgs> OnProductPurchaseAttempt;

	// Token: 0x0600586A RID: 22634 RVA: 0x001CDD50 File Offset: 0x001CBF50
	public void Start()
	{
		this.m_buywithCurrencyReference.RegisterReadyListener<UIBButton>(new Action<UIBButton>(this.OnBuyWithCurrencyButtonReady));
		this.m_buyWithGoldReference.RegisterReadyListener<UIBButton>(new Action<UIBButton>(this.OnBuyWithGoldButtonReady));
		this.m_buyWithVCReference.RegisterReadyListener<UIBButton>(new Action<UIBButton>(this.OnBuyWithVCButtonReady));
		this.m_buyWithTicketReference.RegisterReadyListener<UIBButton>(new Action<UIBButton>(this.OnBuyWithTicketButtonReady));
		this.m_normalButtonReference.RegisterReadyListener<Clickable>(new Action<Clickable>(this.OnNormalButtonReady));
		this.m_visualControllerReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnVisualControllerReady));
		this.m_fullScreenBlockerWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnFullScreenBlockerWidgetReady));
		this.m_noticePopupConfirmReference.RegisterReadyListener<UIBButton>(new Action<UIBButton>(this.OnNoticePopupButtonReady));
		this.m_infoButtonReference.RegisterReadyListener<UIBButton>(new Action<UIBButton>(this.OnInfoButtonReady));
		this.m_arenaTicketBundle = ArenaStore.GetDraftTicketProduct();
		this.m_popupManagerWidget = base.GetComponentInParent<Widget>();
		this.m_popupManagerWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.OnWidgetEvent));
		this.BindProductDataModel();
	}

	// Token: 0x0600586B RID: 22635 RVA: 0x001CDE60 File Offset: 0x001CC060
	private void BindProductDataModel()
	{
		ProductDataModel productDataModel = null;
		if (this.m_arenaTicketBundle != null && this.m_arenaTicketBundle.PMTProductID != null)
		{
			productDataModel = StoreManager.Get().Catalog.GetProductByPmtId(this.m_arenaTicketBundle.PMTProductID.Value);
		}
		productDataModel = (productDataModel ?? ProductFactory.CreateEmptyProductDataModel());
		this.m_popupManagerWidget.BindDataModel(productDataModel, true);
	}

	// Token: 0x0600586C RID: 22636 RVA: 0x001CDEC8 File Offset: 0x001CC0C8
	private void OnBuyWithCurrencyButtonReady(UIBButton button)
	{
		button.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnBuyWithCurrencyButtonPressed));
		bool flag = StoreManager.Get().IsOpen(false);
		button.SetEnabled(flag, false);
		if (!flag)
		{
			button.m_RootObject.GetComponent<MeshRenderer>().SetMaterial(this.m_disabledButtonMaterial);
		}
	}

	// Token: 0x0600586D RID: 22637 RVA: 0x001CDF18 File Offset: 0x001CC118
	private void OnBuyWithCurrencyButtonPressed(UIEvent buttonEvent)
	{
		if (this.m_arenaTicketBundle == null)
		{
			Debug.LogError("Failed to perform Heroic Duel transaction because the arena ticket bundle was null");
			return;
		}
		BuyProductEventArgs purchaseEventArgs = new BuyPmtProductEventArgs(this.m_arenaTicketBundle, CurrencyType.REAL_MONEY, 1);
		this.StartArenaTicketTransaction(purchaseEventArgs);
	}

	// Token: 0x0600586E RID: 22638 RVA: 0x001CDF50 File Offset: 0x001CC150
	private void OnBuyWithVCButtonReady(UIBButton button)
	{
		button.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnBuyWithVCButtonPressed));
		bool flag = StoreManager.Get().IsOpen(false);
		button.SetEnabled(flag, false);
		if (!flag)
		{
			button.m_RootObject.GetComponent<MeshRenderer>().SetMaterial(this.m_disabledButtonMaterial);
		}
	}

	// Token: 0x0600586F RID: 22639 RVA: 0x001CDFA0 File Offset: 0x001CC1A0
	private void OnBuyWithVCButtonPressed(UIEvent buttonEvent)
	{
		if (this.m_arenaTicketBundle == null)
		{
			Debug.LogError("Failed to perform Heroic Duel transaction because the arena ticket bundle was null");
			return;
		}
		CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(this.m_arenaTicketBundle);
		long deficitForVCPurchase = this.GetDeficitForVCPurchase(bundleVirtualCurrencyPriceType);
		if (deficitForVCPurchase > 0L)
		{
			this.ShowVCPurchaseConfirmationPrompt(bundleVirtualCurrencyPriceType, deficitForVCPurchase);
			return;
		}
		BuyProductEventArgs purchaseEventArgs = new BuyPmtProductEventArgs(this.m_arenaTicketBundle, bundleVirtualCurrencyPriceType, 1);
		this.StartArenaTicketTransaction(purchaseEventArgs);
	}

	// Token: 0x06005870 RID: 22640 RVA: 0x001CDFF8 File Offset: 0x001CC1F8
	private void OnBuyWithGoldButtonReady(UIBButton button)
	{
		button.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnBuyWithGoldButtonPressed));
		bool flag = NetCache.Get().GetGoldBalance() >= (long)DuelsConfig.PAID_GOLD_COST && StoreManager.Get().IsOpen(false);
		button.SetEnabled(flag, false);
		if (!flag)
		{
			button.m_RootObject.GetComponent<MeshRenderer>().SetMaterial(this.m_disabledButtonMaterial);
		}
	}

	// Token: 0x06005871 RID: 22641 RVA: 0x001CE05C File Offset: 0x001CC25C
	private void OnBuyWithGoldButtonPressed(UIEvent buttonEvent)
	{
		BuyProductEventArgs purchaseEventArgs = new BuyNoGTAPPEventArgs(new NoGTAPPTransactionData
		{
			Product = ProductType.PRODUCT_TYPE_DRAFT,
			ProductData = 0,
			Quantity = 1
		});
		this.StartArenaTicketTransaction(purchaseEventArgs);
	}

	// Token: 0x06005872 RID: 22642 RVA: 0x001CE090 File Offset: 0x001CC290
	private void OnBuyWithTicketButtonReady(UIBButton button)
	{
		button.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnBuyWithTicketButtonPressed));
	}

	// Token: 0x06005873 RID: 22643 RVA: 0x001CE0A6 File Offset: 0x001CC2A6
	private void OnBuyWithTicketButtonPressed(UIEvent buttonEvent)
	{
		this.m_visualController.SetState("PURCHASE_ACKNOWLEDGED");
		this.m_purchaseSuccessfulDelegate();
	}

	// Token: 0x06005874 RID: 22644 RVA: 0x001CE0C4 File Offset: 0x001CC2C4
	private void OnNormalButtonReady(Clickable button)
	{
		button.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnNormalButtonPressed));
	}

	// Token: 0x06005875 RID: 22645 RVA: 0x001CE0DA File Offset: 0x001CC2DA
	private void OnNormalButtonPressed(UIEvent buttonEvent)
	{
		if (PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsEarlyAccess && !DuelsConfig.HasEarlyAccess())
		{
			this.OpenDuelsShop();
			this.Hide();
			return;
		}
		this.m_normalButtonPressedDelegate();
	}

	// Token: 0x06005876 RID: 22646 RVA: 0x001CE10C File Offset: 0x001CC30C
	private void OnVisualControllerReady(VisualController visualController)
	{
		this.m_visualController = visualController;
		this.m_visualController.RegisterStateChangedListener(new VisualController.OnStateChangedDelegate(this.OnStateChanged));
		if (this.OnReady != null)
		{
			this.OnReady();
		}
	}

	// Token: 0x06005877 RID: 22647 RVA: 0x001CE13F File Offset: 0x001CC33F
	private void OnFullScreenBlockerWidgetReady(Widget fullScreenBlockerWidget)
	{
		this.m_fullScreenBlockerWidget = fullScreenBlockerWidget;
	}

	// Token: 0x06005878 RID: 22648 RVA: 0x001CE148 File Offset: 0x001CC348
	private void OnNoticePopupButtonReady(UIBButton confirmButton)
	{
		confirmButton.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnNoticeConfirmPressed));
	}

	// Token: 0x06005879 RID: 22649 RVA: 0x001CE15E File Offset: 0x001CC35E
	private void OnInfoButtonReady(UIBButton infoButton)
	{
		infoButton.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnInfoButtonPressed));
	}

	// Token: 0x0600587A RID: 22650 RVA: 0x001CE174 File Offset: 0x001CC374
	private void OnInfoButtonPressed(UIEvent e)
	{
		StoreManager.Get().ShowStoreInfo();
	}

	// Token: 0x0600587B RID: 22651 RVA: 0x001CE180 File Offset: 0x001CC380
	private void OnNoticeConfirmPressed(UIEvent buttonEvent)
	{
		this.Hide();
		this.m_noticeConfirmPressedDelegate();
		this.m_noticeConfirmPressedDelegate = null;
	}

	// Token: 0x0600587C RID: 22652 RVA: 0x001CE19A File Offset: 0x001CC39A
	private void OnStateChanged(VisualController visualController)
	{
		this.GetDataModel();
		if (visualController.State == "OPEN")
		{
			this.SetupDuelsStore();
			return;
		}
		if (visualController.State == "HIDE")
		{
			this.ShutdownDuelsStore();
		}
	}

	// Token: 0x0600587D RID: 22653 RVA: 0x001CE1D4 File Offset: 0x001CC3D4
	protected void OnWidgetEvent(string eventName)
	{
		if (!(eventName == "SHOW_COIN_COUNTER"))
		{
			if (eventName == "HIDE_COIN_COUNTER")
			{
				this.m_shouldShowCurrency = false;
				BnetBar bnetBar = BnetBar.Get();
				if (bnetBar == null)
				{
					return;
				}
				bnetBar.RefreshCurrency();
			}
			return;
		}
		this.m_shouldShowCurrency = true;
		BnetBar bnetBar2 = BnetBar.Get();
		if (bnetBar2 == null)
		{
			return;
		}
		bnetBar2.RefreshCurrency();
	}

	// Token: 0x0600587E RID: 22654 RVA: 0x001CE228 File Offset: 0x001CC428
	public bool ShouldShowCoinCounter()
	{
		return this.m_shouldShowCurrency && this.m_isStoreOpen;
	}

	// Token: 0x0600587F RID: 22655 RVA: 0x001CE23A File Offset: 0x001CC43A
	public void Show()
	{
		this.m_visualController.SetState("OPEN");
	}

	// Token: 0x06005880 RID: 22656 RVA: 0x001CE24D File Offset: 0x001CC44D
	public void ShowNotice(string header, string desc, string rating, Action callback)
	{
		this.AddOnNoticeConfirmButtonPressedDelegate(callback);
		this.SetNoticeText(header, desc, rating);
		this.m_visualController.SetState("OPEN_NOTICE");
	}

	// Token: 0x06005881 RID: 22657 RVA: 0x001CE271 File Offset: 0x001CC471
	public void Hide()
	{
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.DUELS_IDLE
		});
		this.m_visualController.SetState("HIDE");
	}

	// Token: 0x06005882 RID: 22658 RVA: 0x001CE29F File Offset: 0x001CC49F
	public void AddOnNormalButtonPressedDelegate(Action action)
	{
		this.m_normalButtonPressedDelegate = (Action)Delegate.Combine(this.m_normalButtonPressedDelegate, action);
	}

	// Token: 0x06005883 RID: 22659 RVA: 0x001CE2B8 File Offset: 0x001CC4B8
	public void RemoveOnNormalButtonPressedDelegate(Action action)
	{
		this.m_normalButtonPressedDelegate = (Action)Delegate.Remove(this.m_normalButtonPressedDelegate, action);
	}

	// Token: 0x06005884 RID: 22660 RVA: 0x001CE2D1 File Offset: 0x001CC4D1
	public void AddOnSuccessfulPurchaseDelegate(Action action)
	{
		this.m_purchaseSuccessfulDelegate = (Action)Delegate.Combine(this.m_purchaseSuccessfulDelegate, action);
	}

	// Token: 0x06005885 RID: 22661 RVA: 0x001CE2EA File Offset: 0x001CC4EA
	public void RemoveOnSuccessfulPurchaseDelegate(Action action)
	{
		this.m_purchaseSuccessfulDelegate = (Action)Delegate.Remove(this.m_purchaseSuccessfulDelegate, action);
	}

	// Token: 0x06005886 RID: 22662 RVA: 0x001CE303 File Offset: 0x001CC503
	public void AddOnNoticeConfirmButtonPressedDelegate(Action action)
	{
		this.m_noticeConfirmPressedDelegate = (Action)Delegate.Combine(this.m_noticeConfirmPressedDelegate, action);
	}

	// Token: 0x06005887 RID: 22663 RVA: 0x001CE31C File Offset: 0x001CC51C
	public void RemoveOnNoticeConfirmButtonPressedDelegate(Action action)
	{
		this.m_noticeConfirmPressedDelegate = (Action)Delegate.Remove(this.m_noticeConfirmPressedDelegate, action);
	}

	// Token: 0x06005888 RID: 22664 RVA: 0x001CE335 File Offset: 0x001CC535
	public void SetNoticeText(string header, string desc, string ratingText = "")
	{
		if (this.GetDataModel() != null)
		{
			this.m_dataModel.NoticeHeaderString = header;
			this.m_dataModel.NoticeDescString = desc;
			if (!string.IsNullOrEmpty(ratingText))
			{
				this.m_dataModel.NoticeRatingString = ratingText;
			}
		}
	}

	// Token: 0x06005889 RID: 22665 RVA: 0x001CE36C File Offset: 0x001CC56C
	private PVPDRLobbyDataModel GetDataModel()
	{
		if (this.m_dataModel == null && PvPDungeonRunDisplay.Get() != null && this.m_popupManagerWidget != null)
		{
			this.m_dataModel = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel();
			this.m_popupManagerWidget.BindDataModel(this.m_dataModel, false);
		}
		return this.m_dataModel;
	}

	// Token: 0x0600588A RID: 22666 RVA: 0x001CE3C4 File Offset: 0x001CC5C4
	private void SetupDuelsStore()
	{
		StoreManager.Get().SetupDuelsStore(this);
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
		StoreManager.Get().RegisterFailedPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnFailedPurchaseAck));
		this.m_isStoreOpen = true;
		Action onOpened = this.OnOpened;
		if (onOpened != null)
		{
			onOpened();
		}
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar == null)
		{
			return;
		}
		bnetBar.RefreshCurrency();
	}

	// Token: 0x0600588B RID: 22667 RVA: 0x001CE430 File Offset: 0x001CC630
	private void ShutdownDuelsStore()
	{
		this.CancelArenaTicketTransaction();
		this.CancelVCPurchaseTransaction();
		Action<StoreClosedArgs> onClosed = this.OnClosed;
		if (onClosed != null)
		{
			onClosed(new StoreClosedArgs(false));
		}
		StoreManager.Get().RemoveFailedPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnFailedPurchaseAck));
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
		StoreManager.Get().ShutDownDuelsStore();
		this.OnProductPurchaseAttempt = null;
		this.m_isStoreOpen = false;
		this.BlockInputs(false);
		PvPDungeonRunDisplay.Get().EnableButtons(true);
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar == null)
		{
			return;
		}
		bnetBar.RefreshCurrency();
	}

	// Token: 0x0600588C RID: 22668 RVA: 0x001CE4C8 File Offset: 0x001CC6C8
	private long GetDeficitForVCPurchase(CurrencyType currency)
	{
		long result = 0L;
		if (this.m_arenaTicketBundle == null)
		{
			Debug.LogError("Failed to calculate VC deficit because m_arenaTicketBundle was null");
			return result;
		}
		if (this.m_arenaTicketBundle.VirtualCurrencyCost == null)
		{
			Debug.LogError("Failed to calculate VC deficit because m_arenaTicketBundle.VirtualCurrencyCost was null");
			return result;
		}
		return ShopUtils.GetDeficit(new PriceDataModel
		{
			Currency = currency,
			Amount = (float)this.m_arenaTicketBundle.VirtualCurrencyCost.Value
		});
	}

	// Token: 0x0600588D RID: 22669 RVA: 0x001CE538 File Offset: 0x001CC738
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
					this.StartVCPurchaseTransaction(currencyType, vcAmount);
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x0600588E RID: 22670 RVA: 0x001CE5E0 File Offset: 0x001CC7E0
	private void StartVCPurchaseTransaction(CurrencyType currencyType, long vcAmount)
	{
		if (this.IsAnyDuelsTransactionActive)
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
		this.m_isVCPurchaseTransactionActive = true;
		this.m_VCPurchaseSucceeded = false;
		if (currencyType == CurrencyType.RUNESTONES)
		{
			Shop.Get().OpenVirtualCurrencyPurchase((float)vcAmount, true);
			return;
		}
		if (currencyType != CurrencyType.ARCANE_ORBS)
		{
			return;
		}
		Shop.Get().OpenBoosterCurrencyPurchase((float)vcAmount, true);
	}

	// Token: 0x0600588F RID: 22671 RVA: 0x001CE674 File Offset: 0x001CC874
	private void EndVCPurchaseTransaction()
	{
		if (!this.m_isVCPurchaseTransactionActive)
		{
			return;
		}
		this.m_isVCPurchaseTransactionActive = false;
		if (this.m_VCPurchaseSucceeded)
		{
			this.m_VCPurchaseSucceeded = false;
			CurrencyType bundleVirtualCurrencyPriceType = ShopUtils.GetBundleVirtualCurrencyPriceType(this.m_arenaTicketBundle);
			BuyProductEventArgs purchaseEventArgs = new BuyPmtProductEventArgs(this.m_arenaTicketBundle, bundleVirtualCurrencyPriceType, 1);
			this.StartArenaTicketTransaction(purchaseEventArgs);
		}
	}

	// Token: 0x06005890 RID: 22672 RVA: 0x001CE6C1 File Offset: 0x001CC8C1
	private void CancelVCPurchaseTransaction()
	{
		if (this.m_isVCPurchaseTransactionActive)
		{
			this.m_VCPurchaseSucceeded = false;
			this.EndVCPurchaseTransaction();
		}
	}

	// Token: 0x06005891 RID: 22673 RVA: 0x001CE6D8 File Offset: 0x001CC8D8
	private void StartArenaTicketTransaction(BuyProductEventArgs purchaseEventArgs)
	{
		if (this.IsAnyDuelsTransactionActive)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DUELS_HEADLINE");
			popupInfo.m_text = GameStrings.Get("GLUE_CHECKOUT_ERROR_GENERIC_FAILURE");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			Debug.LogError("Attempted to start an arena ticket transaction while an existing transaction was in progress");
			return;
		}
		if (this.OnProductPurchaseAttempt == null)
		{
			Debug.LogError("Attempted to start an arena ticket transaction while OnProductPurchaseAttempt was null");
			return;
		}
		this.m_isArenaTicketTransactionActive = true;
		this.OnProductPurchaseAttempt(purchaseEventArgs);
	}

	// Token: 0x06005892 RID: 22674 RVA: 0x001CE75D File Offset: 0x001CC95D
	private void EndArenaTicketTransaction()
	{
		if (!this.m_isArenaTicketTransactionActive)
		{
			return;
		}
		this.m_isArenaTicketTransactionActive = false;
	}

	// Token: 0x06005893 RID: 22675 RVA: 0x001CE76F File Offset: 0x001CC96F
	private void CancelArenaTicketTransaction()
	{
		if (this.m_isArenaTicketTransactionActive)
		{
			this.EndArenaTicketTransaction();
		}
	}

	// Token: 0x06005894 RID: 22676 RVA: 0x001CE780 File Offset: 0x001CC980
	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		if (this.m_isArenaTicketTransactionActive)
		{
			this.EndArenaTicketTransaction();
			this.BlockInputs(false);
			this.m_visualController.SetState("PURCHASE_ACKNOWLEDGED");
			this.m_purchaseSuccessfulDelegate();
			return;
		}
		if (this.m_isVCPurchaseTransactionActive)
		{
			this.m_VCPurchaseSucceeded = true;
			this.EndVCPurchaseTransaction();
		}
	}

	// Token: 0x06005895 RID: 22677 RVA: 0x001CE7D4 File Offset: 0x001CC9D4
	private void OnFailedPurchaseAck(Network.Bundle bundle, PaymentMethod paymentMethod)
	{
		if (this.m_isArenaTicketTransactionActive)
		{
			this.EndArenaTicketTransaction();
			return;
		}
		if (this.m_isVCPurchaseTransactionActive)
		{
			this.EndVCPurchaseTransaction();
		}
	}

	// Token: 0x06005896 RID: 22678 RVA: 0x001CE7F4 File Offset: 0x001CC9F4
	private void OpenDuelsShop()
	{
		int earlyAccessPMTLicenseId = (int)NetCache.Get().GetDuelsEarlyAccessLicenseId();
		AccountLicenseDbfRecord record = GameDbf.AccountLicense.GetRecord((AccountLicenseDbfRecord rec) => (long)earlyAccessPMTLicenseId == rec.LicenseId);
		if (record == null)
		{
			Debug.LogWarning("DuelsPopupManager::OpenDuelsShop() - Duels early access account license not found.");
			this.ShowallDuelsBundlesErrorPopup();
		}
		List<Network.Bundle> allBundlesForProduct = StoreManager.Get().GetAllBundlesForProduct(ProductType.PRODUCT_TYPE_FIXED_LICENSE, false, record.ID, 0, true);
		if (allBundlesForProduct == null || allBundlesForProduct.Count == 0)
		{
			Debug.LogWarning("DuelsPopupManager::OpenDuelsShop() - No products in the shop have Duels early access.");
			this.ShowallDuelsBundlesErrorPopup();
			return;
		}
		int index = (int)(BattleNet.GetMyAccoundId().lo % (ulong)((long)allBundlesForProduct.Count));
		Network.Bundle bundle = allBundlesForProduct[index];
		if (bundle == null || bundle.PMTProductID == null)
		{
			Debug.LogWarning("DuelsPopupManager::OpenDuelsShop() - Duels product has no PMT Product Id.");
			this.ShowallDuelsBundlesErrorPopup();
			return;
		}
		Shop.OpenToProductPageWhenReady(bundle.PMTProductID.Value, true);
	}

	// Token: 0x06005897 RID: 22679 RVA: 0x001CE8D0 File Offset: 0x001CCAD0
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

	// Token: 0x06005898 RID: 22680 RVA: 0x001CE91C File Offset: 0x001CCB1C
	private void BlockInputs(bool blocked)
	{
		if (this.m_fullScreenBlockerWidget == null)
		{
			Debug.LogError("Failed to toggle interface blocker from Duels Popup Manager");
			return;
		}
		if (blocked)
		{
			this.m_fullScreenBlockerWidget.TriggerEvent("BLOCK_SCREEN", new Widget.TriggerEventParameters
			{
				IgnorePlaymaker = true,
				NoDownwardPropagation = false
			});
			return;
		}
		this.m_fullScreenBlockerWidget.TriggerEvent("UNBLOCK_SCREEN", new Widget.TriggerEventParameters
		{
			IgnorePlaymaker = true,
			NoDownwardPropagation = false
		});
	}

	// Token: 0x06005899 RID: 22681 RVA: 0x001CE99A File Offset: 0x001CCB9A
	void IStore.Open()
	{
		Shop.Get().RefreshDataModel();
	}

	// Token: 0x0600589A RID: 22682 RVA: 0x001CE9A6 File Offset: 0x001CCBA6
	void IStore.Close()
	{
		this.CancelArenaTicketTransaction();
		this.CancelVCPurchaseTransaction();
	}

	// Token: 0x0600589B RID: 22683 RVA: 0x001CE9B4 File Offset: 0x001CCBB4
	void IStore.BlockInterface(bool blocked)
	{
		this.BlockInputs(blocked);
	}

	// Token: 0x0600589C RID: 22684 RVA: 0x000052EC File Offset: 0x000034EC
	bool IStore.IsReady()
	{
		return true;
	}

	// Token: 0x0600589D RID: 22685 RVA: 0x001CE9BD File Offset: 0x001CCBBD
	bool IStore.IsOpen()
	{
		return this.m_isStoreOpen;
	}

	// Token: 0x0600589E RID: 22686 RVA: 0x00003BE8 File Offset: 0x00001DE8
	void IStore.Unload()
	{
	}

	// Token: 0x0600589F RID: 22687 RVA: 0x001CE9C5 File Offset: 0x001CCBC5
	IEnumerable<CurrencyType> IStore.GetVisibleCurrencies()
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

	// Token: 0x04004BBE RID: 19390
	private const string HEROIC_STORE_OPENED_EVENT = "HEROIC_STORE_OPENED";

	// Token: 0x04004BBF RID: 19391
	private const string OPEN_EVENT = "OPEN";

	// Token: 0x04004BC0 RID: 19392
	private const string HIDE_EVENT = "HIDE";

	// Token: 0x04004BC1 RID: 19393
	private const string BLOCK_SCREEN = "BLOCK_SCREEN";

	// Token: 0x04004BC2 RID: 19394
	private const string UNBLOCK_SCREEN = "UNBLOCK_SCREEN";

	// Token: 0x04004BC3 RID: 19395
	private const string OPEN_NOTICE = "OPEN_NOTICE";

	// Token: 0x04004BC4 RID: 19396
	private const string PURCHASE_ACKNOWLEDGED = "PURCHASE_ACKNOWLEDGED";

	// Token: 0x04004BC5 RID: 19397
	private const string SHOW_COIN_COUNTER_EVENT = "SHOW_COIN_COUNTER";

	// Token: 0x04004BC6 RID: 19398
	private const string HIDE_COIN_COUNTER_EVENT = "HIDE_COIN_COUNTER";

	// Token: 0x04004BC7 RID: 19399
	public AsyncReference m_buywithCurrencyReference;

	// Token: 0x04004BC8 RID: 19400
	public AsyncReference m_buyWithGoldReference;

	// Token: 0x04004BC9 RID: 19401
	public AsyncReference m_buyWithVCReference;

	// Token: 0x04004BCA RID: 19402
	public AsyncReference m_buyWithTicketReference;

	// Token: 0x04004BCB RID: 19403
	public AsyncReference m_normalButtonReference;

	// Token: 0x04004BCC RID: 19404
	public AsyncReference m_visualControllerReference;

	// Token: 0x04004BCD RID: 19405
	public AsyncReference m_fullScreenBlockerWidgetReference;

	// Token: 0x04004BCE RID: 19406
	public AsyncReference m_noticePopupConfirmReference;

	// Token: 0x04004BCF RID: 19407
	public AsyncReference m_infoButtonReference;

	// Token: 0x04004BD0 RID: 19408
	public Material m_disabledButtonMaterial;

	// Token: 0x04004BD1 RID: 19409
	private VisualController m_visualController;

	// Token: 0x04004BD2 RID: 19410
	private Widget m_fullScreenBlockerWidget;

	// Token: 0x04004BD3 RID: 19411
	private Action m_normalButtonPressedDelegate;

	// Token: 0x04004BD4 RID: 19412
	private Action m_purchaseSuccessfulDelegate;

	// Token: 0x04004BD5 RID: 19413
	private bool m_isStoreOpen;

	// Token: 0x04004BD6 RID: 19414
	private bool m_isArenaTicketTransactionActive;

	// Token: 0x04004BD7 RID: 19415
	private bool m_isVCPurchaseTransactionActive;

	// Token: 0x04004BD8 RID: 19416
	private bool m_VCPurchaseSucceeded;

	// Token: 0x04004BD9 RID: 19417
	private bool m_shouldShowCurrency;

	// Token: 0x04004BDA RID: 19418
	private Network.Bundle m_arenaTicketBundle;

	// Token: 0x04004BDB RID: 19419
	private Action m_noticeConfirmPressedDelegate;

	// Token: 0x04004BDC RID: 19420
	private Widget m_popupManagerWidget;

	// Token: 0x04004BDD RID: 19421
	private PVPDRLobbyDataModel m_dataModel;
}
