using System;
using System.Collections.Generic;
using System.Linq;
using PegasusShared;
using PegasusUtil;

// Token: 0x0200072B RID: 1835
[CustomEditClass]
public class StorePurchaseAuth : UIBPopup
{
	// Token: 0x060066FB RID: 26363 RVA: 0x00219014 File Offset: 0x00217214
	protected override void Awake()
	{
		base.Awake();
		this.m_miniSummary.gameObject.SetActive(false);
		this.m_okButton.gameObject.SetActive(false);
		this.m_okButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnOkayButtonPressed));
	}

	// Token: 0x060066FC RID: 26364 RVA: 0x00219064 File Offset: 0x00217264
	public void Show(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, bool enableBackButton, bool isZeroCostLicense)
	{
		if (this.m_shown)
		{
			return;
		}
		this.m_shown = true;
		this.StartNewTransaction(moneyOrGTAPPTransaction, enableBackButton, isZeroCostLicense);
		this.m_spell.ActivateState(SpellStateType.BIRTH);
		if (this.m_moneyOrGTAPPTransaction != null && this.m_moneyOrGTAPPTransaction.ShouldShowMiniSummary())
		{
			this.ShowMiniSummary();
		}
		else
		{
			this.m_root.UpdateSlices();
		}
		Navigation.PushBlockBackingOut();
		base.DoShowAnimation(null);
	}

	// Token: 0x060066FD RID: 26365 RVA: 0x002190CC File Offset: 0x002172CC
	public void StartNewTransaction(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, bool enableBackButton, bool isZeroCostLicense)
	{
		this.m_moneyOrGTAPPTransaction = moneyOrGTAPPTransaction;
		this.m_showingSuccess = false;
		if (isZeroCostLicense)
		{
			this.m_waitingForAuthText.Text = GameStrings.Get("GLUE_STORE_AUTH_ZERO_COST_WAITING");
			this.m_successHeadlineText.Text = GameStrings.Get("GLUE_STORE_AUTH_ZERO_COST_SUCCESS_HEADLINE");
			this.m_failHeadlineText.Text = GameStrings.Get("GLUE_STORE_AUTH_ZERO_COST_FAIL_HEADLINE");
		}
		else
		{
			this.m_waitingForAuthText.Text = GameStrings.Get("GLUE_STORE_AUTH_WAITING");
			this.m_successHeadlineText.Text = GameStrings.Get("GLUE_STORE_AUTH_SUCCESS_HEADLINE");
			this.m_failHeadlineText.Text = GameStrings.Get("GLUE_STORE_AUTH_FAIL_HEADLINE");
		}
		this.m_isBackButton = enableBackButton;
		this.m_okButton.gameObject.SetActive(enableBackButton);
		this.m_okButton.SetText(enableBackButton ? "GLOBAL_BACK" : "GLOBAL_OKAY");
		this.m_waitingForAuthText.gameObject.SetActive(true);
		this.m_successHeadlineText.gameObject.SetActive(false);
		this.m_failHeadlineText.gameObject.SetActive(false);
		this.m_failDetailsText.gameObject.SetActive(false);
		if (this.m_moneyOrGTAPPTransaction != null && this.m_miniSummary && this.m_miniSummary.gameObject.activeSelf)
		{
			this.m_miniSummary.SetDetails(this.m_moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault(), 1);
		}
	}

	// Token: 0x060066FE RID: 26366 RVA: 0x00219228 File Offset: 0x00217428
	public void ShowPurchaseLocked(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, bool enableBackButton, bool isZeroCostLicense, StorePurchaseAuth.PurchaseLockedDialogCallback purchaseLockedCallback)
	{
		this.Show(moneyOrGTAPPTransaction, enableBackButton, isZeroCostLicense);
		string text = string.Empty;
		if (moneyOrGTAPPTransaction.Provider != null)
		{
			switch (moneyOrGTAPPTransaction.Provider.Value)
			{
			case BattlePayProvider.BP_PROVIDER_APPLE:
				text = GameStrings.Get("GLOBAL_STORE_MOBILE_NAME_APPLE");
				break;
			case BattlePayProvider.BP_PROVIDER_GOOGLE_PLAY:
				text = GameStrings.Get("GLOBAL_STORE_MOBILE_NAME_GOOGLE");
				break;
			case BattlePayProvider.BP_PROVIDER_AMAZON:
				text = GameStrings.Get("GLOBAL_STORE_MOBILE_NAME_AMAZON");
				break;
			default:
				text = GameStrings.Get("GLOBAL_STORE_MOBILE_NAME_DEFAULT");
				break;
			}
		}
		string text2 = GameStrings.Format("GLUE_STORE_PURCHASE_LOCK_DESCRIPTION", new object[]
		{
			text
		});
		DialogManager.Get().ShowPopup(new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_STORE_PURCHASE_LOCK_HEADER"),
			m_confirmText = GameStrings.Get("GLOBAL_CANCEL"),
			m_cancelText = GameStrings.Get("GLOBAL_HELP"),
			m_text = text2,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_iconSet = AlertPopup.PopupInfo.IconSet.Alternate,
			m_responseCallback = delegate(AlertPopup.Response response, object data)
			{
				if (purchaseLockedCallback != null)
				{
					purchaseLockedCallback(response == AlertPopup.Response.CANCEL);
				}
			}
		});
	}

	// Token: 0x060066FF RID: 26367 RVA: 0x00219336 File Offset: 0x00217536
	public override void Hide()
	{
		if (!this.m_shown)
		{
			return;
		}
		this.m_shown = false;
		Navigation.PopBlockBackingOut();
		base.DoHideAnimation(delegate()
		{
			this.m_okButton.gameObject.SetActive(false);
			this.m_miniSummary.gameObject.SetActive(false);
			this.m_spell.ActivateState(SpellStateType.NONE);
		});
	}

	// Token: 0x06006700 RID: 26368 RVA: 0x00219360 File Offset: 0x00217560
	public bool CompletePurchaseSuccess(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return false;
		}
		bool showMiniSummary = false;
		if (moneyOrGTAPPTransaction != null)
		{
			showMiniSummary = moneyOrGTAPPTransaction.ShouldShowMiniSummary();
		}
		this.ShowPurchaseSuccess(moneyOrGTAPPTransaction, showMiniSummary);
		return true;
	}

	// Token: 0x06006701 RID: 26369 RVA: 0x00219394 File Offset: 0x00217594
	public bool CompletePurchaseFailure(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string failDetails, Network.PurchaseErrorInfo.ErrorType error)
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return false;
		}
		bool showMiniSummary = false;
		if (moneyOrGTAPPTransaction != null)
		{
			showMiniSummary = moneyOrGTAPPTransaction.ShouldShowMiniSummary();
		}
		this.ShowPurchaseFailure(moneyOrGTAPPTransaction, failDetails, showMiniSummary, error);
		return true;
	}

	// Token: 0x06006702 RID: 26370 RVA: 0x002193C7 File Offset: 0x002175C7
	public void ShowPreviousPurchaseSuccess(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, bool enableBackButton)
	{
		this.Show(moneyOrGTAPPTransaction, enableBackButton, false);
		this.ShowPurchaseSuccess(moneyOrGTAPPTransaction, true);
	}

	// Token: 0x06006703 RID: 26371 RVA: 0x002193DA File Offset: 0x002175DA
	public void ShowPreviousPurchaseFailure(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string failDetails, bool enableBackButton, Network.PurchaseErrorInfo.ErrorType error)
	{
		this.Show(moneyOrGTAPPTransaction, enableBackButton, false);
		this.ShowPurchaseFailure(moneyOrGTAPPTransaction, failDetails, true, error);
	}

	// Token: 0x06006704 RID: 26372 RVA: 0x002193F0 File Offset: 0x002175F0
	public void ShowPurchaseMethodFailure(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string failDetails, bool enableBackButton, Network.PurchaseErrorInfo.ErrorType error)
	{
		this.Show(moneyOrGTAPPTransaction, enableBackButton, false);
		this.ShowPurchaseFailure(moneyOrGTAPPTransaction, failDetails, false, error);
	}

	// Token: 0x06006705 RID: 26373 RVA: 0x00219406 File Offset: 0x00217606
	public void RegisterAckPurchaseResultListener(StorePurchaseAuth.AckPurchaseResultListener listener)
	{
		if (this.m_ackPurchaseResultListeners.Contains(listener))
		{
			return;
		}
		this.m_ackPurchaseResultListeners.Add(listener);
	}

	// Token: 0x06006706 RID: 26374 RVA: 0x00219423 File Offset: 0x00217623
	public void RemoveAckPurchaseResultListener(StorePurchaseAuth.AckPurchaseResultListener listener)
	{
		this.m_ackPurchaseResultListeners.Remove(listener);
	}

	// Token: 0x06006707 RID: 26375 RVA: 0x00219432 File Offset: 0x00217632
	public void RegisterExitListener(StorePurchaseAuth.ExitListener listener)
	{
		if (this.m_exitListeners.Contains(listener))
		{
			return;
		}
		this.m_exitListeners.Add(listener);
	}

	// Token: 0x06006708 RID: 26376 RVA: 0x0021944F File Offset: 0x0021764F
	public void RemoveExitListener(StorePurchaseAuth.ExitListener listener)
	{
		this.m_exitListeners.Remove(listener);
	}

	// Token: 0x06006709 RID: 26377 RVA: 0x00219460 File Offset: 0x00217660
	private void OnOkayButtonPressed(UIEvent e)
	{
		if (this.m_showingSuccess)
		{
			string text = null;
			Network.Bundle bundle = (this.m_moneyOrGTAPPTransaction == null) ? null : StoreManager.Get().GetBundleFromPmtProductId(this.m_moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault());
			if (bundle != null && bundle.Items != null)
			{
				Network.BundleItem bundleItem = bundle.Items.FirstOrDefault((Network.BundleItem i) => i.ItemType == ProductType.PRODUCT_TYPE_HERO);
				if (bundleItem != null)
				{
					string boughtHeroCardId = GameUtils.TranslateDbIdToCardId(bundleItem.ProductData, false);
					CardHeroDbfRecord record = GameDbf.CardHero.GetRecord((CardHeroDbfRecord dbf) => GameUtils.TranslateDbIdToCardId(dbf.CardId, false) == boughtHeroCardId);
					if (record != null)
					{
						text = record.PurchaseCompleteMsg;
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.Hide();
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = GameStrings.Get("GLUE_STORE_AUTH_SUCCESS_HEADLINE");
				popupInfo.m_text = text;
				popupInfo.m_showAlertIcon = false;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object data)
				{
					this.OnOkayButtonPressed_Finish();
				};
				DialogManager.Get().ShowPopup(popupInfo);
				return;
			}
		}
		this.OnOkayButtonPressed_Finish();
	}

	// Token: 0x0600670A RID: 26378 RVA: 0x00219584 File Offset: 0x00217784
	private void OnOkayButtonPressed_Finish()
	{
		if (this.m_isBackButton)
		{
			StorePurchaseAuth.ExitListener[] array = this.m_exitListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
			return;
		}
		this.Hide();
		StorePurchaseAuth.AckPurchaseResultListener[] array2 = this.m_ackPurchaseResultListeners.ToArray();
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i](this.m_showingSuccess, this.m_moneyOrGTAPPTransaction);
		}
	}

	// Token: 0x0600670B RID: 26379 RVA: 0x002195F0 File Offset: 0x002177F0
	private void ShowPurchaseSuccess(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, bool showMiniSummary)
	{
		this.m_showingSuccess = true;
		this.m_moneyOrGTAPPTransaction = moneyOrGTAPPTransaction;
		this.m_isBackButton = false;
		this.m_okButton.gameObject.SetActive(true);
		this.m_okButton.SetText("GLOBAL_OKAY");
		if (showMiniSummary)
		{
			this.ShowMiniSummary();
		}
		this.m_waitingForAuthText.gameObject.SetActive(false);
		this.m_successHeadlineText.gameObject.SetActive(true);
		this.m_failHeadlineText.gameObject.SetActive(false);
		this.m_failDetailsText.gameObject.SetActive(false);
		this.m_spell.ActivateState(SpellStateType.ACTION);
	}

	// Token: 0x0600670C RID: 26380 RVA: 0x0021968C File Offset: 0x0021788C
	private void ShowPurchaseFailure(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string failDetails, bool showMiniSummary, Network.PurchaseErrorInfo.ErrorType error)
	{
		this.m_showingSuccess = false;
		this.m_moneyOrGTAPPTransaction = moneyOrGTAPPTransaction;
		this.m_isBackButton = false;
		if (error == Network.PurchaseErrorInfo.ErrorType.PRODUCT_EVENT_HAS_ENDED && (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.TAVERN_BRAWL) || SceneMgr.Get().IsModeRequested(SceneMgr.Mode.DRAFT)))
		{
			this.m_isBackButton = true;
		}
		this.m_okButton.gameObject.SetActive(true);
		this.m_okButton.SetText("GLOBAL_OKAY");
		if (showMiniSummary)
		{
			this.ShowMiniSummary();
		}
		this.m_failDetailsText.Text = failDetails;
		this.m_waitingForAuthText.gameObject.SetActive(false);
		this.m_successHeadlineText.gameObject.SetActive(false);
		this.m_failHeadlineText.gameObject.SetActive(true);
		this.m_failDetailsText.gameObject.SetActive(true);
		this.m_spell.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x0600670D RID: 26381 RVA: 0x00219760 File Offset: 0x00217960
	private void ShowMiniSummary()
	{
		if (this.m_moneyOrGTAPPTransaction == null)
		{
			return;
		}
		this.m_miniSummary.SetDetails(this.m_moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault(), 1);
		this.m_miniSummary.gameObject.SetActive(true);
		this.m_root.UpdateSlices();
	}

	// Token: 0x040054C9 RID: 21705
	private const string s_OkButtonText = "GLOBAL_OKAY";

	// Token: 0x040054CA RID: 21706
	private const string s_BackButtonText = "GLOBAL_BACK";

	// Token: 0x040054CB RID: 21707
	[CustomEditField(Sections = "Base UI")]
	public MultiSliceElement m_root;

	// Token: 0x040054CC RID: 21708
	[CustomEditField(Sections = "Swirly Animation")]
	public Spell m_spell;

	// Token: 0x040054CD RID: 21709
	[CustomEditField(Sections = "Base UI")]
	public UIBButton m_okButton;

	// Token: 0x040054CE RID: 21710
	[CustomEditField(Sections = "Text")]
	public UberText m_waitingForAuthText;

	// Token: 0x040054CF RID: 21711
	[CustomEditField(Sections = "Text")]
	public UberText m_successHeadlineText;

	// Token: 0x040054D0 RID: 21712
	[CustomEditField(Sections = "Text")]
	public UberText m_failHeadlineText;

	// Token: 0x040054D1 RID: 21713
	[CustomEditField(Sections = "Text")]
	public UberText m_failDetailsText;

	// Token: 0x040054D2 RID: 21714
	[CustomEditField(Sections = "Base UI")]
	public StoreMiniSummary m_miniSummary;

	// Token: 0x040054D3 RID: 21715
	private bool m_isBackButton;

	// Token: 0x040054D4 RID: 21716
	private bool m_showingSuccess;

	// Token: 0x040054D5 RID: 21717
	private MoneyOrGTAPPTransaction m_moneyOrGTAPPTransaction;

	// Token: 0x040054D6 RID: 21718
	private List<StorePurchaseAuth.AckPurchaseResultListener> m_ackPurchaseResultListeners = new List<StorePurchaseAuth.AckPurchaseResultListener>();

	// Token: 0x040054D7 RID: 21719
	private List<StorePurchaseAuth.ExitListener> m_exitListeners = new List<StorePurchaseAuth.ExitListener>();

	// Token: 0x020022D1 RID: 8913
	// (Invoke) Token: 0x060128B7 RID: 75959
	public delegate void AckPurchaseResultListener(bool success, MoneyOrGTAPPTransaction moneyOrGTAPPTransaction);

	// Token: 0x020022D2 RID: 8914
	// (Invoke) Token: 0x060128BB RID: 75963
	public delegate void ExitListener();

	// Token: 0x020022D3 RID: 8915
	// (Invoke) Token: 0x060128BF RID: 75967
	public delegate void PurchaseLockedDialogCallback(bool showHelp);
}
