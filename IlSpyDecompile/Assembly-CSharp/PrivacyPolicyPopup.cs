using Hearthstone;
using Hearthstone.UI;
using UnityEngine;

public class PrivacyPolicyPopup : DialogBase
{
	public delegate void ResponseCallback(bool confirmedPrivacyPolicy);

	public class Info
	{
		public ResponseCallback m_callback;
	}

	private const string PrivacyPolicyUrl = "https://cn.blizzard.com/zh-cn/legal/1c6ada91-4be1-4e5d-afb3-c44ace09a8d6/%E9%9A%90%E7%A7%81%E6%94%BF%E7%AD%96";

	private const string EulaUrl = "https://cn.blizzard.com/zh-cn/legal/4ac4d7fb-f007-4c62-92ff-886c6a4c127b/%E6%9A%B4%E9%9B%AA%E6%88%98%E7%BD%91%E6%9C%80%E7%BB%88%E7%94%A8%E6%88%B7%E8%AE%B8%E5%8F%AF%E5%8D%8F%E8%AE%AE";

	public PegUIElement m_confirmButton;

	public PegUIElement m_rejectButton;

	public PegUIElement m_privacyPolicyButton;

	public PegUIElement m_eulaButton;

	private Vector3 m_buttonOffset = new Vector3(0.2f, 0f, 0.6f);

	private bool m_confirmedPrivacyPolicy;

	private Camera referenceCamera;

	private ResponseCallback m_responseCallback;

	protected override void Awake()
	{
		base.Awake();
		referenceCamera = CameraUtils.FindFirstByLayer(GameLayer.UI);
		base.transform.position = referenceCamera.transform.TransformPoint(0f, 0f, 200f);
	}

	private void Start()
	{
		GetComponent<WidgetTemplate>().InitializeWidgetBehaviors();
		m_confirmButton.AddEventListener(UIEventType.RELEASEALL, ConfirmButtonReleaseAll);
		m_rejectButton.AddEventListener(UIEventType.RELEASEALL, RejectButtonReleaseAll);
		m_privacyPolicyButton.AddEventListener(UIEventType.RELEASEALL, PrivacyPolicyButtonReleaseAll);
		m_eulaButton.AddEventListener(UIEventType.RELEASEALL, EULAButtonReleaseAll);
		m_privacyPolicyButton.AddEventListener(UIEventType.PRESS, PrivacyPolicyButtonPress);
		m_eulaButton.AddEventListener(UIEventType.PRESS, EULAButtonPress);
	}

	public override void Show()
	{
		base.Show();
		m_showAnimState = ShowAnimState.IN_PROGRESS;
		UniversalInputManager.Get().SetSystemDialogActive(active: true);
	}

	public void SetInfo(Info info)
	{
		m_responseCallback = info.m_callback;
	}

	protected void DownScale()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", new Vector3(0f, 0f, 0f), "delay", 0.1, "easetype", iTween.EaseType.easeInOutCubic, "oncomplete", "OnHideAnimFinished", "time", 0.2f));
	}

	protected override void OnHideAnimFinished()
	{
		base.OnHideAnimFinished();
		m_shown = false;
		OnPrivacyPolicyPopupResponse(m_confirmedPrivacyPolicy);
	}

	private void ConfirmButtonReleaseAll(UIEvent e)
	{
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			m_confirmedPrivacyPolicy = true;
			ScaleAway();
		}
	}

	private void RejectButtonReleaseAll(UIEvent e)
	{
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			m_confirmedPrivacyPolicy = false;
			ScaleAway();
		}
	}

	private void PrivacyPolicyButtonReleaseAll(UIEvent e)
	{
		m_privacyPolicyButton.transform.localPosition -= m_buttonOffset;
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			Application.OpenURL("https://cn.blizzard.com/zh-cn/legal/1c6ada91-4be1-4e5d-afb3-c44ace09a8d6/%E9%9A%90%E7%A7%81%E6%94%BF%E7%AD%96");
		}
	}

	private void EULAButtonReleaseAll(UIEvent e)
	{
		m_eulaButton.transform.localPosition -= m_buttonOffset;
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			Application.OpenURL("https://cn.blizzard.com/zh-cn/legal/4ac4d7fb-f007-4c62-92ff-886c6a4c127b/%E6%9A%B4%E9%9B%AA%E6%88%98%E7%BD%91%E6%9C%80%E7%BB%88%E7%94%A8%E6%88%B7%E8%AE%B8%E5%8F%AF%E5%8D%8F%E8%AE%AE");
		}
	}

	private void PrivacyPolicyButtonPress(UIEvent e)
	{
		m_privacyPolicyButton.transform.localPosition += m_buttonOffset;
	}

	private void EULAButtonPress(UIEvent e)
	{
		m_eulaButton.transform.localPosition += m_buttonOffset;
	}

	private void ScaleAway()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.Scale(PUNCH_SCALE, base.gameObject.transform.localScale), "easetype", iTween.EaseType.easeInOutCubic, "oncomplete", "DownScale", "time", 0.1f));
	}

	private void OnPrivacyPolicyPopupResponse(bool confirmedPrivacyPolicy)
	{
		if (confirmedPrivacyPolicy)
		{
			Options.Get().SetBool(Option.HAS_ACCEPTED_PRIVACY_POLICY_AND_EULA, val: true);
			HearthstoneApplication.Get().DataTransferDependency.Callback();
			return;
		}
		GameObject gameObject = Object.Instantiate(Resources.Load("Prefabs/EmbeddedAlertPopup")) as GameObject;
		if (referenceCamera != null)
		{
			gameObject.transform.position = referenceCamera.transform.TransformPoint(0f, 0f, 200f);
		}
		AlertPopup component = gameObject.GetComponent<AlertPopup>();
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_PRIVACY_POLICY_EULA_TITLE"),
			m_text = GameStrings.Get("GLUE_PRIVACY_POLICY_EULA_CONFIRMATION_TEXT"),
			m_confirmText = GameStrings.Get("GLUE_PRIVACY_POLICY_EULA_CONFIRMATION_ACCEPT"),
			m_cancelText = GameStrings.Get("GLUE_PRIVACY_POLICY_EULA_CONFIRMATION_REJECT"),
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					Options.Get().SetBool(Option.HAS_ACCEPTED_PRIVACY_POLICY_AND_EULA, val: true);
					HearthstoneApplication.Get().DataTransferDependency.Callback();
				}
				else
				{
					HearthstoneApplication.Get().Exit();
				}
			}
		};
		component.UpdateInfo(info);
	}
}
