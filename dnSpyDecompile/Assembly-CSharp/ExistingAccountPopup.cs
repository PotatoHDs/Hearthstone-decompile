using System;
using UnityEngine;

// Token: 0x0200036C RID: 876
public class ExistingAccountPopup : DialogBase
{
	// Token: 0x06003370 RID: 13168 RVA: 0x00108554 File Offset: 0x00106754
	private void Start()
	{
		base.transform.position = new Vector3(base.transform.position.x, -525f, 800f);
		this.m_haveAccountButton.AddEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.HaveAccountButtonReleaseAll));
		this.m_noAccountButton.AddEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.NoAccountButtonReleaseAll));
		this.m_haveAccountButton.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.HaveAccountButtonPress));
		this.m_noAccountButton.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.NoAccountButtonPress));
		this.FadeEffectsIn();
	}

	// Token: 0x06003371 RID: 13169 RVA: 0x001085F8 File Offset: 0x001067F8
	public override void Show()
	{
		base.Show();
		BaseUI.Get().m_BnetBar.DisableButtonsByDialog(this);
		BaseUI.Get().m_BnetBar.HideGameMenu();
		BaseUI.Get().m_BnetBar.HideOptionsMenu();
		this.m_bubble.SetActive(true);
		iTween.FadeTo(this.m_bubble, iTween.Hash(new object[]
		{
			"time",
			0f,
			"amount",
			1f,
			"oncomplete",
			"ShowBubble",
			"oncompletetarget",
			base.gameObject
		}));
		this.m_showAnimState = DialogBase.ShowAnimState.IN_PROGRESS;
		UniversalInputManager.Get().SetSystemDialogActive(true);
		SoundManager.Get().LoadAndPlay(this.m_sound.m_popupShow);
		SoundManager.Get().LoadAndPlay(this.m_sound.m_innkeeperWelcome);
	}

	// Token: 0x06003372 RID: 13170 RVA: 0x001086EC File Offset: 0x001068EC
	public void SetInfo(ExistingAccountPopup.Info info)
	{
		this.m_responseCallback = info.m_callback;
	}

	// Token: 0x06003373 RID: 13171 RVA: 0x001086FC File Offset: 0x001068FC
	protected void FadeBubble()
	{
		iTween.FadeTo(this.m_bubble, iTween.Hash(new object[]
		{
			"delay",
			6f,
			"time",
			1f,
			"amount",
			0f
		}));
	}

	// Token: 0x06003374 RID: 13172 RVA: 0x00108760 File Offset: 0x00106960
	protected void ShowBubble()
	{
		iTween.FadeFrom(this.m_bubble, iTween.Hash(new object[]
		{
			"delay",
			1f,
			"time",
			0.5f,
			"amount",
			0f,
			"oncomplete",
			"FadeBubble",
			"oncompletetarget",
			base.gameObject
		}));
	}

	// Token: 0x06003375 RID: 13173 RVA: 0x001087E8 File Offset: 0x001069E8
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

	// Token: 0x06003376 RID: 13174 RVA: 0x00108880 File Offset: 0x00106A80
	protected override void OnHideAnimFinished()
	{
		base.OnHideAnimFinished();
		this.m_shown = false;
		SoundManager.Get().LoadAndPlay(this.m_sound.m_popupHide);
		this.m_responseCallback(this.m_haveAccount);
	}

	// Token: 0x06003377 RID: 13175 RVA: 0x001088BC File Offset: 0x00106ABC
	private void HaveAccountButtonReleaseAll(UIEvent e)
	{
		this.m_haveAccountButton.transform.localPosition -= this.m_buttonOffset;
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			TelemetryManager.Client().SendButtonPressed("HaveAccount");
			this.m_haveAccount = true;
			this.ScaleAway();
		}
	}

	// Token: 0x06003378 RID: 13176 RVA: 0x00108914 File Offset: 0x00106B14
	private void NoAccountButtonReleaseAll(UIEvent e)
	{
		this.m_noAccountButton.transform.localPosition -= this.m_buttonOffset;
		if (((UIReleaseAllEvent)e).GetMouseIsOver())
		{
			TelemetryManager.Client().SendButtonPressed("NoAccount");
			this.m_haveAccount = false;
			this.FadeEffectsOut();
		}
	}

	// Token: 0x06003379 RID: 13177 RVA: 0x0010896B File Offset: 0x00106B6B
	private void HaveAccountButtonPress(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay(this.m_sound.m_buttonClick);
		this.m_haveAccountButton.transform.localPosition += this.m_buttonOffset;
	}

	// Token: 0x0600337A RID: 13178 RVA: 0x001089A8 File Offset: 0x00106BA8
	private void NoAccountButtonPress(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay(this.m_sound.m_buttonClick);
		this.m_noAccountButton.transform.localPosition += this.m_buttonOffset;
	}

	// Token: 0x0600337B RID: 13179 RVA: 0x001089E8 File Offset: 0x00106BE8
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

	// Token: 0x0600337C RID: 13180 RVA: 0x00108A6C File Offset: 0x00106C6C
	private void FadeEffectsIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr != null)
		{
			fullScreenFXMgr.SetBlurBrightness(1f);
			fullScreenFXMgr.SetBlurDesaturation(0f);
			fullScreenFXMgr.Vignette();
			fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc, null);
		}
	}

	// Token: 0x0600337D RID: 13181 RVA: 0x00108AB4 File Offset: 0x00106CB4
	private void FadeEffectsOut()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr != null)
		{
			fullScreenFXMgr.StopVignette();
			fullScreenFXMgr.StopBlur();
		}
		this.ScaleAway();
	}

	// Token: 0x04001C47 RID: 7239
	public PegUIElement m_haveAccountButton;

	// Token: 0x04001C48 RID: 7240
	public PegUIElement m_noAccountButton;

	// Token: 0x04001C49 RID: 7241
	public GameObject m_bubble;

	// Token: 0x04001C4A RID: 7242
	public ExistingAccoundSound m_sound;

	// Token: 0x04001C4B RID: 7243
	private Vector3 m_buttonOffset = new Vector3(0.2f, 0f, 0.6f);

	// Token: 0x04001C4C RID: 7244
	private bool m_haveAccount;

	// Token: 0x04001C4D RID: 7245
	private ExistingAccountPopup.ResponseCallback m_responseCallback;

	// Token: 0x02001715 RID: 5909
	// (Invoke) Token: 0x0600E6F3 RID: 59123
	public delegate void ResponseCallback(bool hasAccount);

	// Token: 0x02001716 RID: 5910
	public class Info
	{
		// Token: 0x0400B385 RID: 45957
		public ExistingAccountPopup.ResponseCallback m_callback;
	}
}
