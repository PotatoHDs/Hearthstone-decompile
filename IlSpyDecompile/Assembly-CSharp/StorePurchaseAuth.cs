using System.Collections.Generic;
using System.Linq;
using PegasusShared;
using PegasusUtil;

[CustomEditClass]
public class StorePurchaseAuth : UIBPopup
{
	public delegate void AckPurchaseResultListener(bool success, MoneyOrGTAPPTransaction moneyOrGTAPPTransaction);

	public delegate void ExitListener();

	public delegate void PurchaseLockedDialogCallback(bool showHelp);

	private const string s_OkButtonText = "GLOBAL_OKAY";

	private const string s_BackButtonText = "GLOBAL_BACK";

	[CustomEditField(Sections = "Base UI")]
	public MultiSliceElement m_root;

	[CustomEditField(Sections = "Swirly Animation")]
	public Spell m_spell;

	[CustomEditField(Sections = "Base UI")]
	public UIBButton m_okButton;

	[CustomEditField(Sections = "Text")]
	public UberText m_waitingForAuthText;

	[CustomEditField(Sections = "Text")]
	public UberText m_successHeadlineText;

	[CustomEditField(Sections = "Text")]
	public UberText m_failHeadlineText;

	[CustomEditField(Sections = "Text")]
	public UberText m_failDetailsText;

	[CustomEditField(Sections = "Base UI")]
	public StoreMiniSummary m_miniSummary;

	private bool m_isBackButton;

	private bool m_showingSuccess;

	private MoneyOrGTAPPTransaction m_moneyOrGTAPPTransaction;

	private List<AckPurchaseResultListener> m_ackPurchaseResultListeners = new List<AckPurchaseResultListener>();

	private List<ExitListener> m_exitListeners = new List<ExitListener>();

	protected override void Awake()
	{
		base.Awake();
		m_miniSummary.gameObject.SetActive(value: false);
		m_okButton.gameObject.SetActive(value: false);
		m_okButton.AddEventListener(UIEventType.RELEASE, OnOkayButtonPressed);
	}

	public void Show(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, bool enableBackButton, bool isZeroCostLicense)
	{
		if (!m_shown)
		{
			m_shown = true;
			StartNewTransaction(moneyOrGTAPPTransaction, enableBackButton, isZeroCostLicense);
			m_spell.ActivateState(SpellStateType.BIRTH);
			if (m_moneyOrGTAPPTransaction != null && m_moneyOrGTAPPTransaction.ShouldShowMiniSummary())
			{
				ShowMiniSummary();
			}
			else
			{
				m_root.UpdateSlices();
			}
			Navigation.PushBlockBackingOut();
			DoShowAnimation();
		}
	}

	public void StartNewTransaction(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, bool enableBackButton, bool isZeroCostLicense)
	{
		m_moneyOrGTAPPTransaction = moneyOrGTAPPTransaction;
		m_showingSuccess = false;
		if (isZeroCostLicense)
		{
			m_waitingForAuthText.Text = GameStrings.Get("GLUE_STORE_AUTH_ZERO_COST_WAITING");
			m_successHeadlineText.Text = GameStrings.Get("GLUE_STORE_AUTH_ZERO_COST_SUCCESS_HEADLINE");
			m_failHeadlineText.Text = GameStrings.Get("GLUE_STORE_AUTH_ZERO_COST_FAIL_HEADLINE");
		}
		else
		{
			m_waitingForAuthText.Text = GameStrings.Get("GLUE_STORE_AUTH_WAITING");
			m_successHeadlineText.Text = GameStrings.Get("GLUE_STORE_AUTH_SUCCESS_HEADLINE");
			m_failHeadlineText.Text = GameStrings.Get("GLUE_STORE_AUTH_FAIL_HEADLINE");
		}
		m_isBackButton = enableBackButton;
		m_okButton.gameObject.SetActive(enableBackButton);
		m_okButton.SetText(enableBackButton ? "GLOBAL_BACK" : "GLOBAL_OKAY");
		m_waitingForAuthText.gameObject.SetActive(value: true);
		m_successHeadlineText.gameObject.SetActive(value: false);
		m_failHeadlineText.gameObject.SetActive(value: false);
		m_failDetailsText.gameObject.SetActive(value: false);
		if (m_moneyOrGTAPPTransaction != null && (bool)m_miniSummary && m_miniSummary.gameObject.activeSelf)
		{
			m_miniSummary.SetDetails(m_moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault(), 1);
		}
	}

	public void ShowPurchaseLocked(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, bool enableBackButton, bool isZeroCostLicense, PurchaseLockedDialogCallback purchaseLockedCallback)
	{
		Show(moneyOrGTAPPTransaction, enableBackButton, isZeroCostLicense);
		string text = string.Empty;
		if (moneyOrGTAPPTransaction.Provider.HasValue)
		{
			text = moneyOrGTAPPTransaction.Provider.Value switch
			{
				BattlePayProvider.BP_PROVIDER_APPLE => GameStrings.Get("GLOBAL_STORE_MOBILE_NAME_APPLE"), 
				BattlePayProvider.BP_PROVIDER_GOOGLE_PLAY => GameStrings.Get("GLOBAL_STORE_MOBILE_NAME_GOOGLE"), 
				BattlePayProvider.BP_PROVIDER_AMAZON => GameStrings.Get("GLOBAL_STORE_MOBILE_NAME_AMAZON"), 
				_ => GameStrings.Get("GLOBAL_STORE_MOBILE_NAME_DEFAULT"), 
			};
		}
		string text2 = GameStrings.Format("GLUE_STORE_PURCHASE_LOCK_DESCRIPTION", text);
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

	public override void Hide()
	{
		if (m_shown)
		{
			m_shown = false;
			Navigation.PopBlockBackingOut();
			DoHideAnimation(delegate
			{
				m_okButton.gameObject.SetActive(value: false);
				m_miniSummary.gameObject.SetActive(value: false);
				m_spell.ActivateState(SpellStateType.NONE);
			});
		}
	}

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
		ShowPurchaseSuccess(moneyOrGTAPPTransaction, showMiniSummary);
		return true;
	}

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
		ShowPurchaseFailure(moneyOrGTAPPTransaction, failDetails, showMiniSummary, error);
		return true;
	}

	public void ShowPreviousPurchaseSuccess(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, bool enableBackButton)
	{
		Show(moneyOrGTAPPTransaction, enableBackButton, isZeroCostLicense: false);
		ShowPurchaseSuccess(moneyOrGTAPPTransaction, showMiniSummary: true);
	}

	public void ShowPreviousPurchaseFailure(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string failDetails, bool enableBackButton, Network.PurchaseErrorInfo.ErrorType error)
	{
		Show(moneyOrGTAPPTransaction, enableBackButton, isZeroCostLicense: false);
		ShowPurchaseFailure(moneyOrGTAPPTransaction, failDetails, showMiniSummary: true, error);
	}

	public void ShowPurchaseMethodFailure(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string failDetails, bool enableBackButton, Network.PurchaseErrorInfo.ErrorType error)
	{
		Show(moneyOrGTAPPTransaction, enableBackButton, isZeroCostLicense: false);
		ShowPurchaseFailure(moneyOrGTAPPTransaction, failDetails, showMiniSummary: false, error);
	}

	public void RegisterAckPurchaseResultListener(AckPurchaseResultListener listener)
	{
		if (!m_ackPurchaseResultListeners.Contains(listener))
		{
			m_ackPurchaseResultListeners.Add(listener);
		}
	}

	public void RemoveAckPurchaseResultListener(AckPurchaseResultListener listener)
	{
		m_ackPurchaseResultListeners.Remove(listener);
	}

	public void RegisterExitListener(ExitListener listener)
	{
		if (!m_exitListeners.Contains(listener))
		{
			m_exitListeners.Add(listener);
		}
	}

	public void RemoveExitListener(ExitListener listener)
	{
		m_exitListeners.Remove(listener);
	}

	private void OnOkayButtonPressed(UIEvent e)
	{
		if (m_showingSuccess)
		{
			string text = null;
			Network.Bundle bundle = ((m_moneyOrGTAPPTransaction == null) ? null : StoreManager.Get().GetBundleFromPmtProductId(m_moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault()));
			if (bundle != null && bundle.Items != null)
			{
				Network.BundleItem bundleItem = bundle.Items.FirstOrDefault((Network.BundleItem i) => i.ItemType == ProductType.PRODUCT_TYPE_HERO);
				if (bundleItem != null)
				{
					string boughtHeroCardId = GameUtils.TranslateDbIdToCardId(bundleItem.ProductData);
					CardHeroDbfRecord record = GameDbf.CardHero.GetRecord((CardHeroDbfRecord dbf) => GameUtils.TranslateDbIdToCardId(dbf.CardId) == boughtHeroCardId);
					if (record != null)
					{
						text = record.PurchaseCompleteMsg;
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				Hide();
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = GameStrings.Get("GLUE_STORE_AUTH_SUCCESS_HEADLINE");
				popupInfo.m_text = text;
				popupInfo.m_showAlertIcon = false;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				popupInfo.m_responseCallback = delegate
				{
					OnOkayButtonPressed_Finish();
				};
				DialogManager.Get().ShowPopup(popupInfo);
				return;
			}
		}
		OnOkayButtonPressed_Finish();
	}

	private void OnOkayButtonPressed_Finish()
	{
		if (m_isBackButton)
		{
			ExitListener[] array = m_exitListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
			return;
		}
		Hide();
		AckPurchaseResultListener[] array2 = m_ackPurchaseResultListeners.ToArray();
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i](m_showingSuccess, m_moneyOrGTAPPTransaction);
		}
	}

	private void ShowPurchaseSuccess(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, bool showMiniSummary)
	{
		m_showingSuccess = true;
		m_moneyOrGTAPPTransaction = moneyOrGTAPPTransaction;
		m_isBackButton = false;
		m_okButton.gameObject.SetActive(value: true);
		m_okButton.SetText("GLOBAL_OKAY");
		if (showMiniSummary)
		{
			ShowMiniSummary();
		}
		m_waitingForAuthText.gameObject.SetActive(value: false);
		m_successHeadlineText.gameObject.SetActive(value: true);
		m_failHeadlineText.gameObject.SetActive(value: false);
		m_failDetailsText.gameObject.SetActive(value: false);
		m_spell.ActivateState(SpellStateType.ACTION);
	}

	private void ShowPurchaseFailure(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, string failDetails, bool showMiniSummary, Network.PurchaseErrorInfo.ErrorType error)
	{
		m_showingSuccess = false;
		m_moneyOrGTAPPTransaction = moneyOrGTAPPTransaction;
		m_isBackButton = false;
		if (error == Network.PurchaseErrorInfo.ErrorType.PRODUCT_EVENT_HAS_ENDED && (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.TAVERN_BRAWL) || SceneMgr.Get().IsModeRequested(SceneMgr.Mode.DRAFT)))
		{
			m_isBackButton = true;
		}
		m_okButton.gameObject.SetActive(value: true);
		m_okButton.SetText("GLOBAL_OKAY");
		if (showMiniSummary)
		{
			ShowMiniSummary();
		}
		m_failDetailsText.Text = failDetails;
		m_waitingForAuthText.gameObject.SetActive(value: false);
		m_successHeadlineText.gameObject.SetActive(value: false);
		m_failHeadlineText.gameObject.SetActive(value: true);
		m_failDetailsText.gameObject.SetActive(value: true);
		m_spell.ActivateState(SpellStateType.DEATH);
	}

	private void ShowMiniSummary()
	{
		if (m_moneyOrGTAPPTransaction != null)
		{
			m_miniSummary.SetDetails(m_moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault(), 1);
			m_miniSummary.gameObject.SetActive(value: true);
			m_root.UpdateSlices();
		}
	}
}
