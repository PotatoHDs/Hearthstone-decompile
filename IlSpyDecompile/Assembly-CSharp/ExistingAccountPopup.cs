using UnityEngine;

public class ExistingAccountPopup : DialogBase
{
	public delegate void ResponseCallback(bool hasAccount);

	public class Info
	{
		public ResponseCallback m_callback;
	}

	public PegUIElement m_haveAccountButton;

	public PegUIElement m_noAccountButton;

	public GameObject m_bubble;

	public ExistingAccoundSound m_sound;

	private Vector3 m_buttonOffset = new Vector3(0.2f, 0f, 0.6f);

	private bool m_haveAccount;

	private ResponseCallback m_responseCallback;

	private void Start()
	{
		base.transform.position = new Vector3(base.transform.position.x, -525f, 800f);
		m_haveAccountButton.AddEventListener(UIEventType.RELEASEALL, HaveAccountButtonReleaseAll);
		m_noAccountButton.AddEventListener(UIEventType.RELEASEALL, NoAccountButtonReleaseAll);
		m_haveAccountButton.AddEventListener(UIEventType.PRESS, HaveAccountButtonPress);
		m_noAccountButton.AddEventListener(UIEventType.PRESS, NoAccountButtonPress);
		FadeEffectsIn();
	}

	public override void Show()
	{
		base.Show();
		BaseUI.Get().m_BnetBar.DisableButtonsByDialog(this);
		BaseUI.Get().m_BnetBar.HideGameMenu();
		BaseUI.Get().m_BnetBar.HideOptionsMenu();
		m_bubble.SetActive(value: true);
		iTween.FadeTo(m_bubble, iTween.Hash("time", 0f, "amount", 1f, "oncomplete", "ShowBubble", "oncompletetarget", base.gameObject));
		m_showAnimState = ShowAnimState.IN_PROGRESS;
		UniversalInputManager.Get().SetSystemDialogActive(active: true);
		SoundManager.Get().LoadAndPlay(m_sound.m_popupShow);
		SoundManager.Get().LoadAndPlay(m_sound.m_innkeeperWelcome);
	}

	public void SetInfo(Info info)
	{
		m_responseCallback = info.m_callback;
	}

	protected void FadeBubble()
	{
		iTween.FadeTo(m_bubble, iTween.Hash("delay", 6f, "time", 1f, "amount", 0f));
	}

	protected void ShowBubble()
	{
		iTween.FadeFrom(m_bubble, iTween.Hash("delay", 1f, "time", 0.5f, "amount", 0f, "oncomplete", "FadeBubble", "oncompletetarget", base.gameObject));
	}

	protected void DownScale()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", new Vector3(0f, 0f, 0f), "delay", 0.1, "easetype", iTween.EaseType.easeInOutCubic, "oncomplete", "OnHideAnimFinished", "time", 0.2f));
	}

	protected override void OnHideAnimFinished()
	{
		base.OnHideAnimFinished();
		m_shown = false;
		SoundManager.Get().LoadAndPlay(m_sound.m_popupHide);
		m_responseCallback(m_haveAccount);
	}

	private void HaveAccountButtonReleaseAll(UIEvent e)
	{
		m_haveAccountButton.transform.localPosition -= m_buttonOffset;
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			TelemetryManager.Client().SendButtonPressed("HaveAccount");
			m_haveAccount = true;
			ScaleAway();
		}
	}

	private void NoAccountButtonReleaseAll(UIEvent e)
	{
		m_noAccountButton.transform.localPosition -= m_buttonOffset;
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			TelemetryManager.Client().SendButtonPressed("NoAccount");
			m_haveAccount = false;
			FadeEffectsOut();
		}
	}

	private void HaveAccountButtonPress(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay(m_sound.m_buttonClick);
		m_haveAccountButton.transform.localPosition += m_buttonOffset;
	}

	private void NoAccountButtonPress(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay(m_sound.m_buttonClick);
		m_noAccountButton.transform.localPosition += m_buttonOffset;
	}

	private void ScaleAway()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.Scale(PUNCH_SCALE, base.gameObject.transform.localScale), "easetype", iTween.EaseType.easeInOutCubic, "oncomplete", "DownScale", "time", 0.1f));
	}

	private void FadeEffectsIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr != null)
		{
			fullScreenFXMgr.SetBlurBrightness(1f);
			fullScreenFXMgr.SetBlurDesaturation(0f);
			fullScreenFXMgr.Vignette();
			fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc);
		}
	}

	private void FadeEffectsOut()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr != null)
		{
			fullScreenFXMgr.StopVignette();
			fullScreenFXMgr.StopBlur();
		}
		ScaleAway();
	}
}
