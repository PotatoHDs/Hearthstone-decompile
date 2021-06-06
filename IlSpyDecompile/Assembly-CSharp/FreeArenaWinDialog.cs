using System.Collections;
using UnityEngine;

public class FreeArenaWinDialog : DialogBase
{
	public class Info
	{
		public HideCallback m_callbackOnHide;

		public int m_winCount;
	}

	[CustomEditField(Sections = "Object Links")]
	public UIBButton m_okayButton;

	public UberText m_okayButtonText;

	public UberText m_winCount;

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_showAnimationSound = "Expand_Up.prefab:775d97ea42498c044897f396362b9db3";

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_hideAnimationSound = "Shrink_Down_Quicker.prefab:2fe963b171811ca4b8d544fa53e3330c";

	private Info m_info;

	protected override void Awake()
	{
		base.Awake();
		m_okayButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			PressOk();
		});
	}

	public void SetInfo(Info info)
	{
		m_info = info;
		if (m_info.m_callbackOnHide != null)
		{
			AddHideListener(m_info.m_callbackOnHide);
		}
	}

	public override void Show()
	{
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		EnableFullScreenEffects(enable: true);
		base.Show();
		m_winCount.Text = m_info.m_winCount.ToString();
		if (!string.IsNullOrEmpty(m_showAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(m_showAnimationSound);
		}
		Hashtable args = iTween.Hash("scale", localScale, "time", 0.3f, "easetype", iTween.EaseType.easeOutBack);
		iTween.ScaleTo(base.gameObject, args);
		UniversalInputManager.Get().SetSystemDialogActive(active: true);
	}

	protected void EnableFullScreenEffects(bool enable)
	{
		if (FullScreenFXMgr.Get() != null)
		{
			if (enable)
			{
				FullScreenFXMgr.Get().StartStandardBlurVignette(1f);
			}
			else
			{
				FullScreenFXMgr.Get().EndStandardBlurVignette(1f);
			}
		}
	}

	protected override void DoHideAnimation()
	{
		if (!string.IsNullOrEmpty(m_hideAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(m_hideAnimationSound);
		}
		base.DoHideAnimation();
	}

	private void PressOk()
	{
		EnableFullScreenEffects(enable: false);
		Hide();
	}
}
