using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class BasicPopup : DialogBase
{
	public enum Response
	{
		CANCEL,
		CUSTOM_RESPONSE
	}

	public delegate void ResponseCallback(Response response, object userData);

	public class PopupInfo
	{
		public readonly List<string> m_prefabAssetRefs = new List<string>();

		public ResponseCallback m_responseCallback;

		public object m_responseUserData;

		public string m_headerText;

		public string m_bodyText;

		public bool m_disableBnetBar;

		public bool m_blurWhenShown;
	}

	public UIBButton m_cancelButton;

	public UIBButton m_customButton;

	public UberText m_headerText;

	public UberText m_bodyText;

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_showAnimationSound = "Expand_Up.prefab:775d97ea42498c044897f396362b9db3";

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_hideAnimationSound = "Shrink_Down_Quicker.prefab:2fe963b171811ca4b8d544fa53e3330c";

	protected PopupInfo m_popupInfo;

	protected override void Awake()
	{
		base.Awake();
		if (m_cancelButton != null)
		{
			m_cancelButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				ButtonPress(Response.CANCEL);
			});
		}
		if (m_customButton != null)
		{
			m_customButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				ButtonPress(Response.CUSTOM_RESPONSE);
			});
		}
	}

	public override bool HandleKeyboardInput()
	{
		if (InputCollection.GetKeyUp(KeyCode.Escape))
		{
			GoBack();
			return true;
		}
		return false;
	}

	public override void GoBack()
	{
		ButtonPress(Response.CANCEL);
	}

	public void SetInfo(PopupInfo info)
	{
		m_popupInfo = info;
	}

	public override void Show()
	{
		base.Show();
		InitInfo();
		if (m_popupInfo.m_disableBnetBar)
		{
			BnetBar.Get().DisableButtonsByDialog(this);
		}
		if (m_popupInfo.m_blurWhenShown)
		{
			DialogBase.DoBlur();
		}
		DoShowAnimation();
		if (!string.IsNullOrEmpty(m_showAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(m_showAnimationSound);
		}
	}

	public override void Hide()
	{
		base.Hide();
		if (m_popupInfo.m_blurWhenShown)
		{
			DialogBase.EndBlur();
		}
	}

	protected override void OnHideAnimFinished()
	{
		UniversalInputManager.Get().SetSystemDialogActive(active: false);
		base.OnHideAnimFinished();
		if (!string.IsNullOrEmpty(m_hideAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(m_hideAnimationSound);
		}
	}

	private void InitInfo()
	{
		if (m_popupInfo == null)
		{
			m_popupInfo = new PopupInfo();
		}
		if (m_headerText != null && m_popupInfo.m_headerText != null)
		{
			m_headerText.Text = m_popupInfo.m_headerText;
		}
		if (m_bodyText != null && m_popupInfo.m_bodyText != null)
		{
			m_bodyText.Text = m_popupInfo.m_bodyText;
		}
	}

	private void ButtonPress(Response response)
	{
		if (m_popupInfo.m_responseCallback != null)
		{
			m_popupInfo.m_responseCallback(response, m_popupInfo.m_responseUserData);
		}
		Hide();
	}
}
