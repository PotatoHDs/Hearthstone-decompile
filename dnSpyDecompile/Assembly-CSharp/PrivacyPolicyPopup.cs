using System;
using Hearthstone;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000371 RID: 881
public class PrivacyPolicyPopup : DialogBase
{
	// Token: 0x060033D1 RID: 13265 RVA: 0x00109E99 File Offset: 0x00108099
	protected override void Awake()
	{
		base.Awake();
		this.referenceCamera = CameraUtils.FindFirstByLayer(GameLayer.UI);
		base.transform.position = this.referenceCamera.transform.TransformPoint(0f, 0f, 200f);
	}

	// Token: 0x060033D2 RID: 13266 RVA: 0x00109ED8 File Offset: 0x001080D8
	private void Start()
	{
		base.GetComponent<WidgetTemplate>().InitializeWidgetBehaviors();
		this.m_confirmButton.AddEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.ConfirmButtonReleaseAll));
		this.m_rejectButton.AddEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.RejectButtonReleaseAll));
		this.m_privacyPolicyButton.AddEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.PrivacyPolicyButtonReleaseAll));
		this.m_eulaButton.AddEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.EULAButtonReleaseAll));
		this.m_privacyPolicyButton.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.PrivacyPolicyButtonPress));
		this.m_eulaButton.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.EULAButtonPress));
	}

	// Token: 0x060033D3 RID: 13267 RVA: 0x00109F86 File Offset: 0x00108186
	public override void Show()
	{
		base.Show();
		this.m_showAnimState = DialogBase.ShowAnimState.IN_PROGRESS;
		UniversalInputManager.Get().SetSystemDialogActive(true);
	}

	// Token: 0x060033D4 RID: 13268 RVA: 0x00109FA0 File Offset: 0x001081A0
	public void SetInfo(PrivacyPolicyPopup.Info info)
	{
		this.m_responseCallback = info.m_callback;
	}

	// Token: 0x060033D5 RID: 13269 RVA: 0x00109FB0 File Offset: 0x001081B0
	protected void DownScale()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0f, 0f, 0f),
			"delay",
			0.1,
			"easetype",
			iTween.EaseType.easeInOutCubic,
			"oncomplete",
			"OnHideAnimFinished",
			"time",
			0.2f
		}));
	}

	// Token: 0x060033D6 RID: 13270 RVA: 0x0010A048 File Offset: 0x00108248
	protected override void OnHideAnimFinished()
	{
		base.OnHideAnimFinished();
		this.m_shown = false;
		this.OnPrivacyPolicyPopupResponse(this.m_confirmedPrivacyPolicy);
	}

	// Token: 0x060033D7 RID: 13271 RVA: 0x0010A063 File Offset: 0x00108263
	private void ConfirmButtonReleaseAll(UIEvent e)
	{
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			this.m_confirmedPrivacyPolicy = true;
			this.ScaleAway();
		}
	}

	// Token: 0x060033D8 RID: 13272 RVA: 0x0010A07F File Offset: 0x0010827F
	private void RejectButtonReleaseAll(UIEvent e)
	{
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			this.m_confirmedPrivacyPolicy = false;
			this.ScaleAway();
		}
	}

	// Token: 0x060033D9 RID: 13273 RVA: 0x0010A09B File Offset: 0x0010829B
	private void PrivacyPolicyButtonReleaseAll(UIEvent e)
	{
		this.m_privacyPolicyButton.transform.localPosition -= this.m_buttonOffset;
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			Application.OpenURL("https://cn.blizzard.com/zh-cn/legal/1c6ada91-4be1-4e5d-afb3-c44ace09a8d6/%E9%9A%90%E7%A7%81%E6%94%BF%E7%AD%96");
		}
	}

	// Token: 0x060033DA RID: 13274 RVA: 0x0010A0D5 File Offset: 0x001082D5
	private void EULAButtonReleaseAll(UIEvent e)
	{
		this.m_eulaButton.transform.localPosition -= this.m_buttonOffset;
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			Application.OpenURL("https://cn.blizzard.com/zh-cn/legal/4ac4d7fb-f007-4c62-92ff-886c6a4c127b/%E6%9A%B4%E9%9B%AA%E6%88%98%E7%BD%91%E6%9C%80%E7%BB%88%E7%94%A8%E6%88%B7%E8%AE%B8%E5%8F%AF%E5%8D%8F%E8%AE%AE");
		}
	}

	// Token: 0x060033DB RID: 13275 RVA: 0x0010A10F File Offset: 0x0010830F
	private void PrivacyPolicyButtonPress(UIEvent e)
	{
		this.m_privacyPolicyButton.transform.localPosition += this.m_buttonOffset;
	}

	// Token: 0x060033DC RID: 13276 RVA: 0x0010A132 File Offset: 0x00108332
	private void EULAButtonPress(UIEvent e)
	{
		this.m_eulaButton.transform.localPosition += this.m_buttonOffset;
	}

	// Token: 0x060033DD RID: 13277 RVA: 0x0010A158 File Offset: 0x00108358
	private void ScaleAway()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.Scale(this.PUNCH_SCALE, base.gameObject.transform.localScale),
			"easetype",
			iTween.EaseType.easeInOutCubic,
			"oncomplete",
			"DownScale",
			"time",
			0.1f
		}));
	}

	// Token: 0x060033DE RID: 13278 RVA: 0x0010A1DC File Offset: 0x001083DC
	private void OnPrivacyPolicyPopupResponse(bool confirmedPrivacyPolicy)
	{
		if (confirmedPrivacyPolicy)
		{
			Options.Get().SetBool(Option.HAS_ACCEPTED_PRIVACY_POLICY_AND_EULA, true);
			HearthstoneApplication.Get().DataTransferDependency.Callback();
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/EmbeddedAlertPopup")) as GameObject;
		if (this.referenceCamera != null)
		{
			gameObject.transform.position = this.referenceCamera.transform.TransformPoint(0f, 0f, 200f);
		}
		AlertPopup component = gameObject.GetComponent<AlertPopup>();
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_PRIVACY_POLICY_EULA_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_PRIVACY_POLICY_EULA_CONFIRMATION_TEXT");
		popupInfo.m_confirmText = GameStrings.Get("GLUE_PRIVACY_POLICY_EULA_CONFIRMATION_ACCEPT");
		popupInfo.m_cancelText = GameStrings.Get("GLUE_PRIVACY_POLICY_EULA_CONFIRMATION_REJECT");
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				Options.Get().SetBool(Option.HAS_ACCEPTED_PRIVACY_POLICY_AND_EULA, true);
				HearthstoneApplication.Get().DataTransferDependency.Callback();
				return;
			}
			HearthstoneApplication.Get().Exit();
		};
		AlertPopup.PopupInfo info = popupInfo;
		component.UpdateInfo(info);
	}

	// Token: 0x04001C61 RID: 7265
	private const string PrivacyPolicyUrl = "https://cn.blizzard.com/zh-cn/legal/1c6ada91-4be1-4e5d-afb3-c44ace09a8d6/%E9%9A%90%E7%A7%81%E6%94%BF%E7%AD%96";

	// Token: 0x04001C62 RID: 7266
	private const string EulaUrl = "https://cn.blizzard.com/zh-cn/legal/4ac4d7fb-f007-4c62-92ff-886c6a4c127b/%E6%9A%B4%E9%9B%AA%E6%88%98%E7%BD%91%E6%9C%80%E7%BB%88%E7%94%A8%E6%88%B7%E8%AE%B8%E5%8F%AF%E5%8D%8F%E8%AE%AE";

	// Token: 0x04001C63 RID: 7267
	public PegUIElement m_confirmButton;

	// Token: 0x04001C64 RID: 7268
	public PegUIElement m_rejectButton;

	// Token: 0x04001C65 RID: 7269
	public PegUIElement m_privacyPolicyButton;

	// Token: 0x04001C66 RID: 7270
	public PegUIElement m_eulaButton;

	// Token: 0x04001C67 RID: 7271
	private Vector3 m_buttonOffset = new Vector3(0.2f, 0f, 0.6f);

	// Token: 0x04001C68 RID: 7272
	private bool m_confirmedPrivacyPolicy;

	// Token: 0x04001C69 RID: 7273
	private Camera referenceCamera;

	// Token: 0x04001C6A RID: 7274
	private PrivacyPolicyPopup.ResponseCallback m_responseCallback;

	// Token: 0x02001725 RID: 5925
	// (Invoke) Token: 0x0600E741 RID: 59201
	public delegate void ResponseCallback(bool confirmedPrivacyPolicy);

	// Token: 0x02001726 RID: 5926
	public class Info
	{
		// Token: 0x0400B3AB RID: 45995
		public PrivacyPolicyPopup.ResponseCallback m_callback;
	}
}
