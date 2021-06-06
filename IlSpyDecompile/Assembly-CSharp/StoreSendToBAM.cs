using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class StoreSendToBAM : UIBPopup
{
	public enum BAMReason
	{
		PAYMENT_INFO,
		NEED_PASSWORD_RESET,
		NO_VALID_PAYMENT_METHOD,
		CREDIT_CARD_EXPIRED,
		GENERIC_PAYMENT_FAIL,
		EULA_AND_TOS,
		PRODUCT_UNIQUENESS_VIOLATED,
		GENERIC_PURCHASE_FAIL_RETRY_CONTACT_CS_IF_PERSISTS
	}

	private class SendToBAMText
	{
		private string m_headlineKey;

		private string m_detailsKey;

		private string m_goToURLKey;

		private string m_url;

		public SendToBAMText(string headlineKey, string detailsKey, string goToURLKey, string url)
		{
			m_headlineKey = headlineKey;
			m_detailsKey = detailsKey;
			m_goToURLKey = goToURLKey;
			m_url = url;
		}

		public string GetHeadline()
		{
			return GameStrings.Get(m_headlineKey);
		}

		public string GetDetails()
		{
			return GameStrings.Get(m_detailsKey);
		}

		public string GetGoToURLDetails(string buttonName)
		{
			return GameStrings.Format(m_goToURLKey, buttonName);
		}

		public string GetUrl()
		{
			return m_url;
		}
	}

	public delegate void DelOKListener(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, BAMReason reason);

	public delegate void DelCancelListener(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction);

	public UIBButton m_okayButton;

	public UIBButton m_cancelButton;

	public UberText m_headlineText;

	public UberText m_messageText;

	public MultiSliceElement m_allSections;

	public GameObject m_midSection;

	public GameObject m_sendToBAMRoot;

	public Transform m_sendToBAMRootWithSummaryBone;

	public StoreMiniSummary m_miniSummary;

	public PegUIElement m_offClickCatcher;

	private static readonly string SEND_TO_BAM_THEN_HIDE_COROUTINE = "SendToBAMThenHide";

	private static readonly PlatformDependentValue<string> GLUE_STORE_PAYMENT_INFO_DETAILS = new PlatformDependentValue<string>(PlatformCategory.OS)
	{
		PC = "GLUE_STORE_PAYMENT_INFO_DETAILS",
		iOS = "GLUE_MOBILE_STORE_PAYMENT_INFO_DETAILS_APPLE",
		Android = "GLUE_MOBILE_STORE_PAYMENT_INFO_DETAILS_ANDROID"
	};

	private static readonly PlatformDependentValue<string> GLUE_STORE_PAYMENT_INFO_URL_DETAILS = new PlatformDependentValue<string>(PlatformCategory.OS)
	{
		PC = "GLUE_STORE_PAYMENT_INFO_URL_DETAILS",
		iOS = "GLUE_MOBILE_STORE_PAYMENT_INFO_URL_DETAILS",
		Android = "GLUE_MOBILE_STORE_PAYMENT_INFO_URL_DETAILS"
	};

	private static readonly Vector3 SHOW_MINI_SUMMARY_SCALE_PHONE = new Vector3(80f, 80f, 80f);

	private Vector3 m_originalShowScale = Vector3.zero;

	private List<DelOKListener> m_okayListeners = new List<DelOKListener>();

	private List<DelCancelListener> m_cancelListeners = new List<DelCancelListener>();

	private BAMReason m_sendToBAMReason;

	private MoneyOrGTAPPTransaction m_moneyOrGTAPPTransaction;

	private string m_errorCode = "";

	private static Map<BAMReason, SendToBAMText> s_bamTextMap;

	protected override void Awake()
	{
		base.Awake();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_scaleMode = CanvasScaleMode.WIDTH;
		}
		if (m_offClickCatcher != null)
		{
			m_offClickCatcher.AddEventListener(UIEventType.RELEASE, OnCancelPressed);
		}
		s_bamTextMap = new Map<BAMReason, SendToBAMText>
		{
			{
				BAMReason.PAYMENT_INFO,
				new SendToBAMText("GLUE_STORE_PAYMENT_INFO_HEADLINE", GLUE_STORE_PAYMENT_INFO_DETAILS, GLUE_STORE_PAYMENT_INFO_URL_DETAILS, ExternalUrlService.Get().GetPaymentInfoLink())
			},
			{
				BAMReason.NEED_PASSWORD_RESET,
				new SendToBAMText("GLUE_STORE_FORGOT_PWD_HEADLINE", "GLUE_STORE_FORGOT_PWD_DETAILS", "GLUE_STORE_FORGOT_PWD_URL_DETAILS", ExternalUrlService.Get().GetResetPasswordLink())
			},
			{
				BAMReason.NO_VALID_PAYMENT_METHOD,
				new SendToBAMText("GLUE_STORE_NO_PAYMENT_HEADLINE", "GLUE_STORE_NO_PAYMENT_DETAILS", "GLUE_STORE_NO_PAYMENT_URL_DETAILS", ExternalUrlService.Get().GetGenericPurchaseErrorLink())
			},
			{
				BAMReason.CREDIT_CARD_EXPIRED,
				new SendToBAMText("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE", "GLUE_STORE_CC_EXPIRY_DETAILS", "GLUE_STORE_GENERIC_BP_FAIL_URL_DETAILS", ExternalUrlService.Get().GetAddPaymentLink())
			},
			{
				BAMReason.GENERIC_PAYMENT_FAIL,
				new SendToBAMText("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE", "GLUE_STORE_GENERIC_BP_FAIL_DETAILS", "GLUE_STORE_GENERIC_BP_FAIL_URL_DETAILS", ExternalUrlService.Get().GetGenericPurchaseErrorLink())
			},
			{
				BAMReason.EULA_AND_TOS,
				new SendToBAMText("GLUE_STORE_EULA_AND_TOS_HEADLINE", "GLUE_STORE_EULA_AND_TOS_DETAILS", "GLUE_STORE_EULA_AND_TOS_URL_DETAILS", ExternalUrlService.Get().GetTermsOfSaleLink())
			},
			{
				BAMReason.PRODUCT_UNIQUENESS_VIOLATED,
				new SendToBAMText("GLUE_STORE_PURCHASE_LOCK_HEADER", "GLUE_STORE_FAIL_PRODUCT_UNIQUENESS_VIOLATED", "GLUE_STORE_FAIL_PRODUCT_UNIQUENESS_VIOLATED_URL", ExternalUrlService.Get().GetDuplicatePurchaseErrorLink())
			},
			{
				BAMReason.GENERIC_PURCHASE_FAIL_RETRY_CONTACT_CS_IF_PERSISTS,
				new SendToBAMText("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE", "GLUE_STORE_GENERIC_BP_FAIL_RETRY_CONTACT_CS_IF_PERSISTS_DETAILS", "GLUE_STORE_GENERIC_BP_FAIL_RETRY_CONTACT_CS_IF_PERSISTS_URL_DETAILS", ExternalUrlService.Get().GetGenericPurchaseErrorLink())
			}
		};
		m_okayButton.SetText(GameStrings.Get("GLOBAL_MORE"));
		m_cancelButton.SetText(GameStrings.Get("GLOBAL_CANCEL"));
		m_okayButton.AddEventListener(UIEventType.RELEASE, OnOkayPressed);
		m_cancelButton.AddEventListener(UIEventType.RELEASE, OnCancelPressed);
	}

	public void Show(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, BAMReason reason, string errorCode, bool fromPreviousPurchase)
	{
		m_moneyOrGTAPPTransaction = moneyOrGTAPPTransaction;
		m_sendToBAMReason = reason;
		m_errorCode = errorCode;
		UpdateText();
		if (moneyOrGTAPPTransaction != null && (fromPreviousPurchase || moneyOrGTAPPTransaction.ShouldShowMiniSummary()))
		{
			m_sendToBAMRoot.transform.position = m_sendToBAMRootWithSummaryBone.position;
			m_miniSummary.SetDetails(m_moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault(), 1);
			m_miniSummary.gameObject.SetActive(value: true);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_originalShowScale = m_showScale;
				m_showScale = SHOW_MINI_SUMMARY_SCALE_PHONE;
			}
		}
		else
		{
			m_sendToBAMRoot.transform.localPosition = Vector3.zero;
			m_miniSummary.gameObject.SetActive(value: false);
			if ((bool)UniversalInputManager.UsePhoneUI && m_originalShowScale != Vector3.zero)
			{
				m_showScale = m_originalShowScale;
				m_originalShowScale = Vector3.zero;
			}
		}
		if (!m_shown)
		{
			Navigation.Push(OnCancel);
			m_shown = true;
			m_headlineText.UpdateNow();
			LayoutMessageText();
			DoShowAnimation();
		}
	}

	public void RegisterOkayListener(DelOKListener listener)
	{
		if (!m_okayListeners.Contains(listener))
		{
			m_okayListeners.Add(listener);
		}
	}

	public void RemoveOkayListener(DelOKListener listener)
	{
		m_okayListeners.Remove(listener);
	}

	public void RegisterCancelListener(DelCancelListener listener)
	{
		if (!m_cancelListeners.Contains(listener))
		{
			m_cancelListeners.Add(listener);
		}
	}

	public void RemoveCancelListener(DelCancelListener listener)
	{
		m_cancelListeners.Remove(listener);
	}

	protected override void OnHidden()
	{
		m_okayButton.SetEnabled(enabled: true);
		m_okayButton.TriggerOut();
	}

	private void OnOkayPressed(UIEvent e)
	{
		StopCoroutine(SEND_TO_BAM_THEN_HIDE_COROUTINE);
		StartCoroutine(SEND_TO_BAM_THEN_HIDE_COROUTINE);
	}

	private IEnumerator SendToBAMThenHide()
	{
		m_okayButton.SetEnabled(enabled: false);
		string text = "";
		SendToBAMText sendToBAMText = s_bamTextMap[m_sendToBAMReason];
		if (sendToBAMText == null)
		{
			Debug.LogError($"StoreSendToBAM.SendToBAMThenHide(): can't get URL for BAM reason {m_sendToBAMReason}");
		}
		else
		{
			text = sendToBAMText.GetUrl();
		}
		if (!string.IsNullOrEmpty(text))
		{
			Application.OpenURL(text);
		}
		yield return new WaitForSeconds(2f);
		Navigation.Pop();
		Hide(animate: true);
		DelOKListener[] array = m_okayListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](m_moneyOrGTAPPTransaction, m_sendToBAMReason);
		}
	}

	private bool OnCancel()
	{
		StopCoroutine(SEND_TO_BAM_THEN_HIDE_COROUTINE);
		Hide(animate: true);
		DelCancelListener[] array = m_cancelListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](m_moneyOrGTAPPTransaction);
		}
		return true;
	}

	private void OnCancelPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	private void UpdateText()
	{
		SendToBAMText sendToBAMText = s_bamTextMap[m_sendToBAMReason];
		if (sendToBAMText == null)
		{
			Debug.LogError($"StoreSendToBAM.UpdateText(): don't know how to update text for BAM reason {m_sendToBAMReason}");
			m_headlineText.Text = "";
			m_messageText.Text = "";
			return;
		}
		string text = sendToBAMText.GetDetails();
		if (!string.IsNullOrEmpty(m_errorCode))
		{
			text = text + " " + GameStrings.Format("GLUE_STORE_FAIL_DETAILS_ERROR_CODE", m_errorCode);
		}
		text += "\n\n";
		text += sendToBAMText.GetGoToURLDetails(m_okayButton.m_ButtonText.Text);
		m_headlineText.Text = sendToBAMText.GetHeadline();
		m_messageText.Text = text;
	}

	private void LayoutMessageText()
	{
		m_messageText.UpdateNow();
		TransformUtil.SetLocalScaleZ(m_midSection, 1f);
		float num = TransformUtil.ComputeOrientedWorldBounds(m_midSection).Extents[2].magnitude * 2f;
		TransformUtil.SetLocalScaleZ(z: m_messageText.GetTextWorldSpaceBounds().size.z / num, go: m_midSection);
		m_allSections.UpdateSlices();
	}
}
