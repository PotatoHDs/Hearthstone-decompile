using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200072F RID: 1839
[CustomEditClass]
public class StoreSendToBAM : UIBPopup
{
	// Token: 0x0600672B RID: 26411 RVA: 0x00219DA8 File Offset: 0x00217FA8
	protected override void Awake()
	{
		base.Awake();
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_scaleMode = CanvasScaleMode.WIDTH;
		}
		if (this.m_offClickCatcher != null)
		{
			this.m_offClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelPressed));
		}
		StoreSendToBAM.s_bamTextMap = new Map<StoreSendToBAM.BAMReason, StoreSendToBAM.SendToBAMText>
		{
			{
				StoreSendToBAM.BAMReason.PAYMENT_INFO,
				new StoreSendToBAM.SendToBAMText("GLUE_STORE_PAYMENT_INFO_HEADLINE", StoreSendToBAM.GLUE_STORE_PAYMENT_INFO_DETAILS, StoreSendToBAM.GLUE_STORE_PAYMENT_INFO_URL_DETAILS, ExternalUrlService.Get().GetPaymentInfoLink())
			},
			{
				StoreSendToBAM.BAMReason.NEED_PASSWORD_RESET,
				new StoreSendToBAM.SendToBAMText("GLUE_STORE_FORGOT_PWD_HEADLINE", "GLUE_STORE_FORGOT_PWD_DETAILS", "GLUE_STORE_FORGOT_PWD_URL_DETAILS", ExternalUrlService.Get().GetResetPasswordLink())
			},
			{
				StoreSendToBAM.BAMReason.NO_VALID_PAYMENT_METHOD,
				new StoreSendToBAM.SendToBAMText("GLUE_STORE_NO_PAYMENT_HEADLINE", "GLUE_STORE_NO_PAYMENT_DETAILS", "GLUE_STORE_NO_PAYMENT_URL_DETAILS", ExternalUrlService.Get().GetGenericPurchaseErrorLink())
			},
			{
				StoreSendToBAM.BAMReason.CREDIT_CARD_EXPIRED,
				new StoreSendToBAM.SendToBAMText("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE", "GLUE_STORE_CC_EXPIRY_DETAILS", "GLUE_STORE_GENERIC_BP_FAIL_URL_DETAILS", ExternalUrlService.Get().GetAddPaymentLink())
			},
			{
				StoreSendToBAM.BAMReason.GENERIC_PAYMENT_FAIL,
				new StoreSendToBAM.SendToBAMText("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE", "GLUE_STORE_GENERIC_BP_FAIL_DETAILS", "GLUE_STORE_GENERIC_BP_FAIL_URL_DETAILS", ExternalUrlService.Get().GetGenericPurchaseErrorLink())
			},
			{
				StoreSendToBAM.BAMReason.EULA_AND_TOS,
				new StoreSendToBAM.SendToBAMText("GLUE_STORE_EULA_AND_TOS_HEADLINE", "GLUE_STORE_EULA_AND_TOS_DETAILS", "GLUE_STORE_EULA_AND_TOS_URL_DETAILS", ExternalUrlService.Get().GetTermsOfSaleLink())
			},
			{
				StoreSendToBAM.BAMReason.PRODUCT_UNIQUENESS_VIOLATED,
				new StoreSendToBAM.SendToBAMText("GLUE_STORE_PURCHASE_LOCK_HEADER", "GLUE_STORE_FAIL_PRODUCT_UNIQUENESS_VIOLATED", "GLUE_STORE_FAIL_PRODUCT_UNIQUENESS_VIOLATED_URL", ExternalUrlService.Get().GetDuplicatePurchaseErrorLink())
			},
			{
				StoreSendToBAM.BAMReason.GENERIC_PURCHASE_FAIL_RETRY_CONTACT_CS_IF_PERSISTS,
				new StoreSendToBAM.SendToBAMText("GLUE_STORE_GENERIC_BP_FAIL_HEADLINE", "GLUE_STORE_GENERIC_BP_FAIL_RETRY_CONTACT_CS_IF_PERSISTS_DETAILS", "GLUE_STORE_GENERIC_BP_FAIL_RETRY_CONTACT_CS_IF_PERSISTS_URL_DETAILS", ExternalUrlService.Get().GetGenericPurchaseErrorLink())
			}
		};
		this.m_okayButton.SetText(GameStrings.Get("GLOBAL_MORE"));
		this.m_cancelButton.SetText(GameStrings.Get("GLOBAL_CANCEL"));
		this.m_okayButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnOkayPressed));
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelPressed));
	}

	// Token: 0x0600672C RID: 26412 RVA: 0x00219F90 File Offset: 0x00218190
	public void Show(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, StoreSendToBAM.BAMReason reason, string errorCode, bool fromPreviousPurchase)
	{
		this.m_moneyOrGTAPPTransaction = moneyOrGTAPPTransaction;
		this.m_sendToBAMReason = reason;
		this.m_errorCode = errorCode;
		this.UpdateText();
		if (moneyOrGTAPPTransaction != null && (fromPreviousPurchase || moneyOrGTAPPTransaction.ShouldShowMiniSummary()))
		{
			this.m_sendToBAMRoot.transform.position = this.m_sendToBAMRootWithSummaryBone.position;
			this.m_miniSummary.SetDetails(this.m_moneyOrGTAPPTransaction.PMTProductID.GetValueOrDefault(), 1);
			this.m_miniSummary.gameObject.SetActive(true);
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_originalShowScale = this.m_showScale;
				this.m_showScale = StoreSendToBAM.SHOW_MINI_SUMMARY_SCALE_PHONE;
			}
		}
		else
		{
			this.m_sendToBAMRoot.transform.localPosition = Vector3.zero;
			this.m_miniSummary.gameObject.SetActive(false);
			if (UniversalInputManager.UsePhoneUI && this.m_originalShowScale != Vector3.zero)
			{
				this.m_showScale = this.m_originalShowScale;
				this.m_originalShowScale = Vector3.zero;
			}
		}
		if (this.m_shown)
		{
			return;
		}
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnCancel));
		this.m_shown = true;
		this.m_headlineText.UpdateNow(false);
		this.LayoutMessageText();
		base.DoShowAnimation(null);
	}

	// Token: 0x0600672D RID: 26413 RVA: 0x0021A0D2 File Offset: 0x002182D2
	public void RegisterOkayListener(StoreSendToBAM.DelOKListener listener)
	{
		if (this.m_okayListeners.Contains(listener))
		{
			return;
		}
		this.m_okayListeners.Add(listener);
	}

	// Token: 0x0600672E RID: 26414 RVA: 0x0021A0EF File Offset: 0x002182EF
	public void RemoveOkayListener(StoreSendToBAM.DelOKListener listener)
	{
		this.m_okayListeners.Remove(listener);
	}

	// Token: 0x0600672F RID: 26415 RVA: 0x0021A0FE File Offset: 0x002182FE
	public void RegisterCancelListener(StoreSendToBAM.DelCancelListener listener)
	{
		if (this.m_cancelListeners.Contains(listener))
		{
			return;
		}
		this.m_cancelListeners.Add(listener);
	}

	// Token: 0x06006730 RID: 26416 RVA: 0x0021A11B File Offset: 0x0021831B
	public void RemoveCancelListener(StoreSendToBAM.DelCancelListener listener)
	{
		this.m_cancelListeners.Remove(listener);
	}

	// Token: 0x06006731 RID: 26417 RVA: 0x0021A12A File Offset: 0x0021832A
	protected override void OnHidden()
	{
		this.m_okayButton.SetEnabled(true, false);
		this.m_okayButton.TriggerOut();
	}

	// Token: 0x06006732 RID: 26418 RVA: 0x0021A144 File Offset: 0x00218344
	private void OnOkayPressed(UIEvent e)
	{
		base.StopCoroutine(StoreSendToBAM.SEND_TO_BAM_THEN_HIDE_COROUTINE);
		base.StartCoroutine(StoreSendToBAM.SEND_TO_BAM_THEN_HIDE_COROUTINE);
	}

	// Token: 0x06006733 RID: 26419 RVA: 0x0021A15D File Offset: 0x0021835D
	private IEnumerator SendToBAMThenHide()
	{
		this.m_okayButton.SetEnabled(false, false);
		string text = "";
		StoreSendToBAM.SendToBAMText sendToBAMText = StoreSendToBAM.s_bamTextMap[this.m_sendToBAMReason];
		if (sendToBAMText == null)
		{
			Debug.LogError(string.Format("StoreSendToBAM.SendToBAMThenHide(): can't get URL for BAM reason {0}", this.m_sendToBAMReason));
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
		this.Hide(true);
		StoreSendToBAM.DelOKListener[] array = this.m_okayListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.m_moneyOrGTAPPTransaction, this.m_sendToBAMReason);
		}
		yield break;
	}

	// Token: 0x06006734 RID: 26420 RVA: 0x0021A16C File Offset: 0x0021836C
	private bool OnCancel()
	{
		base.StopCoroutine(StoreSendToBAM.SEND_TO_BAM_THEN_HIDE_COROUTINE);
		this.Hide(true);
		StoreSendToBAM.DelCancelListener[] array = this.m_cancelListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](this.m_moneyOrGTAPPTransaction);
		}
		return true;
	}

	// Token: 0x06006735 RID: 26421 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnCancelPressed(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x06006736 RID: 26422 RVA: 0x0021A1B4 File Offset: 0x002183B4
	private void UpdateText()
	{
		StoreSendToBAM.SendToBAMText sendToBAMText = StoreSendToBAM.s_bamTextMap[this.m_sendToBAMReason];
		if (sendToBAMText == null)
		{
			Debug.LogError(string.Format("StoreSendToBAM.UpdateText(): don't know how to update text for BAM reason {0}", this.m_sendToBAMReason));
			this.m_headlineText.Text = "";
			this.m_messageText.Text = "";
			return;
		}
		string text = sendToBAMText.GetDetails();
		if (!string.IsNullOrEmpty(this.m_errorCode))
		{
			text = text + " " + GameStrings.Format("GLUE_STORE_FAIL_DETAILS_ERROR_CODE", new object[]
			{
				this.m_errorCode
			});
		}
		text += "\n\n";
		text += sendToBAMText.GetGoToURLDetails(this.m_okayButton.m_ButtonText.Text);
		this.m_headlineText.Text = sendToBAMText.GetHeadline();
		this.m_messageText.Text = text;
	}

	// Token: 0x06006737 RID: 26423 RVA: 0x0021A290 File Offset: 0x00218490
	private void LayoutMessageText()
	{
		this.m_messageText.UpdateNow(false);
		TransformUtil.SetLocalScaleZ(this.m_midSection, 1f);
		float num = TransformUtil.ComputeOrientedWorldBounds(this.m_midSection, true).Extents[2].magnitude * 2f;
		Bounds textWorldSpaceBounds = this.m_messageText.GetTextWorldSpaceBounds();
		TransformUtil.SetLocalScaleZ(this.m_midSection, textWorldSpaceBounds.size.z / num);
		this.m_allSections.UpdateSlices();
	}

	// Token: 0x040054EB RID: 21739
	public UIBButton m_okayButton;

	// Token: 0x040054EC RID: 21740
	public UIBButton m_cancelButton;

	// Token: 0x040054ED RID: 21741
	public UberText m_headlineText;

	// Token: 0x040054EE RID: 21742
	public UberText m_messageText;

	// Token: 0x040054EF RID: 21743
	public MultiSliceElement m_allSections;

	// Token: 0x040054F0 RID: 21744
	public GameObject m_midSection;

	// Token: 0x040054F1 RID: 21745
	public GameObject m_sendToBAMRoot;

	// Token: 0x040054F2 RID: 21746
	public Transform m_sendToBAMRootWithSummaryBone;

	// Token: 0x040054F3 RID: 21747
	public StoreMiniSummary m_miniSummary;

	// Token: 0x040054F4 RID: 21748
	public PegUIElement m_offClickCatcher;

	// Token: 0x040054F5 RID: 21749
	private static readonly string SEND_TO_BAM_THEN_HIDE_COROUTINE = "SendToBAMThenHide";

	// Token: 0x040054F6 RID: 21750
	private static readonly PlatformDependentValue<string> GLUE_STORE_PAYMENT_INFO_DETAILS = new PlatformDependentValue<string>(PlatformCategory.OS)
	{
		PC = "GLUE_STORE_PAYMENT_INFO_DETAILS",
		iOS = "GLUE_MOBILE_STORE_PAYMENT_INFO_DETAILS_APPLE",
		Android = "GLUE_MOBILE_STORE_PAYMENT_INFO_DETAILS_ANDROID"
	};

	// Token: 0x040054F7 RID: 21751
	private static readonly PlatformDependentValue<string> GLUE_STORE_PAYMENT_INFO_URL_DETAILS = new PlatformDependentValue<string>(PlatformCategory.OS)
	{
		PC = "GLUE_STORE_PAYMENT_INFO_URL_DETAILS",
		iOS = "GLUE_MOBILE_STORE_PAYMENT_INFO_URL_DETAILS",
		Android = "GLUE_MOBILE_STORE_PAYMENT_INFO_URL_DETAILS"
	};

	// Token: 0x040054F8 RID: 21752
	private static readonly Vector3 SHOW_MINI_SUMMARY_SCALE_PHONE = new Vector3(80f, 80f, 80f);

	// Token: 0x040054F9 RID: 21753
	private Vector3 m_originalShowScale = Vector3.zero;

	// Token: 0x040054FA RID: 21754
	private List<StoreSendToBAM.DelOKListener> m_okayListeners = new List<StoreSendToBAM.DelOKListener>();

	// Token: 0x040054FB RID: 21755
	private List<StoreSendToBAM.DelCancelListener> m_cancelListeners = new List<StoreSendToBAM.DelCancelListener>();

	// Token: 0x040054FC RID: 21756
	private StoreSendToBAM.BAMReason m_sendToBAMReason;

	// Token: 0x040054FD RID: 21757
	private MoneyOrGTAPPTransaction m_moneyOrGTAPPTransaction;

	// Token: 0x040054FE RID: 21758
	private string m_errorCode = "";

	// Token: 0x040054FF RID: 21759
	private static Map<StoreSendToBAM.BAMReason, StoreSendToBAM.SendToBAMText> s_bamTextMap;

	// Token: 0x020022DA RID: 8922
	public enum BAMReason
	{
		// Token: 0x0400E4FF RID: 58623
		PAYMENT_INFO,
		// Token: 0x0400E500 RID: 58624
		NEED_PASSWORD_RESET,
		// Token: 0x0400E501 RID: 58625
		NO_VALID_PAYMENT_METHOD,
		// Token: 0x0400E502 RID: 58626
		CREDIT_CARD_EXPIRED,
		// Token: 0x0400E503 RID: 58627
		GENERIC_PAYMENT_FAIL,
		// Token: 0x0400E504 RID: 58628
		EULA_AND_TOS,
		// Token: 0x0400E505 RID: 58629
		PRODUCT_UNIQUENESS_VIOLATED,
		// Token: 0x0400E506 RID: 58630
		GENERIC_PURCHASE_FAIL_RETRY_CONTACT_CS_IF_PERSISTS
	}

	// Token: 0x020022DB RID: 8923
	private class SendToBAMText
	{
		// Token: 0x060128D2 RID: 75986 RVA: 0x0050E8B7 File Offset: 0x0050CAB7
		public SendToBAMText(string headlineKey, string detailsKey, string goToURLKey, string url)
		{
			this.m_headlineKey = headlineKey;
			this.m_detailsKey = detailsKey;
			this.m_goToURLKey = goToURLKey;
			this.m_url = url;
		}

		// Token: 0x060128D3 RID: 75987 RVA: 0x0050E8DC File Offset: 0x0050CADC
		public string GetHeadline()
		{
			return GameStrings.Get(this.m_headlineKey);
		}

		// Token: 0x060128D4 RID: 75988 RVA: 0x0050E8E9 File Offset: 0x0050CAE9
		public string GetDetails()
		{
			return GameStrings.Get(this.m_detailsKey);
		}

		// Token: 0x060128D5 RID: 75989 RVA: 0x0050E8F6 File Offset: 0x0050CAF6
		public string GetGoToURLDetails(string buttonName)
		{
			return GameStrings.Format(this.m_goToURLKey, new object[]
			{
				buttonName
			});
		}

		// Token: 0x060128D6 RID: 75990 RVA: 0x0050E90D File Offset: 0x0050CB0D
		public string GetUrl()
		{
			return this.m_url;
		}

		// Token: 0x0400E507 RID: 58631
		private string m_headlineKey;

		// Token: 0x0400E508 RID: 58632
		private string m_detailsKey;

		// Token: 0x0400E509 RID: 58633
		private string m_goToURLKey;

		// Token: 0x0400E50A RID: 58634
		private string m_url;
	}

	// Token: 0x020022DC RID: 8924
	// (Invoke) Token: 0x060128D8 RID: 75992
	public delegate void DelOKListener(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction, StoreSendToBAM.BAMReason reason);

	// Token: 0x020022DD RID: 8925
	// (Invoke) Token: 0x060128DC RID: 75996
	public delegate void DelCancelListener(MoneyOrGTAPPTransaction moneyOrGTAPPTransaction);
}
