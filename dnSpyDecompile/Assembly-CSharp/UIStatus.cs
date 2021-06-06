using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000B35 RID: 2869
public class UIStatus : MonoBehaviour
{
	// Token: 0x06009861 RID: 39009 RVA: 0x003158E8 File Offset: 0x00313AE8
	private void Awake()
	{
		UIStatus.s_instance = this;
		this.m_Text.gameObject.SetActive(false);
		if (OverlayUI.Get())
		{
			OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
			return;
		}
		throw new UnityException("Trying to create UIStatus before OverlayUI!");
	}

	// Token: 0x06009862 RID: 39010 RVA: 0x00315936 File Offset: 0x00313B36
	private void OnDestroy()
	{
		UIStatus.s_instance = null;
	}

	// Token: 0x06009863 RID: 39011 RVA: 0x0031593E File Offset: 0x00313B3E
	public static UIStatus Get()
	{
		if (UIStatus.s_instance == null)
		{
			UIStatus.s_instance = AssetLoader.Get().InstantiatePrefab("UIStatus.prefab:8fe3c92addcd14427a5277cfedc2341c", AssetLoadingOptions.None).GetComponent<UIStatus>();
		}
		return UIStatus.s_instance;
	}

	// Token: 0x06009864 RID: 39012 RVA: 0x00315971 File Offset: 0x00313B71
	public void AddInfo(string message)
	{
		this.AddInfo(message, UIStatus.StatusType.GENERIC);
	}

	// Token: 0x06009865 RID: 39013 RVA: 0x0031597B File Offset: 0x00313B7B
	public void AddInfo(string message, float delay)
	{
		this.AddInfo(message, UIStatus.StatusType.GENERIC, delay);
	}

	// Token: 0x06009866 RID: 39014 RVA: 0x00315986 File Offset: 0x00313B86
	public void AddInfo(string message, UIStatus.StatusType statusType)
	{
		this.AddInfo(message, statusType, -1f);
	}

	// Token: 0x06009867 RID: 39015 RVA: 0x00315995 File Offset: 0x00313B95
	public void AddInfo(string message, UIStatus.StatusType statusType, float delay)
	{
		this.m_currentStatusType = statusType;
		this.m_Text.TextColor = this.m_InfoColor;
		this.ShowMessage(message, delay, true);
	}

	// Token: 0x06009868 RID: 39016 RVA: 0x003159B8 File Offset: 0x00313BB8
	public void AddInfoNoRichText(string message, float delay = -1f)
	{
		this.m_Text.TextColor = this.m_InfoColor;
		this.ShowMessage(message, delay, false);
	}

	// Token: 0x06009869 RID: 39017 RVA: 0x003159D4 File Offset: 0x00313BD4
	public void AddError(string message, float delay = -1f)
	{
		this.m_Text.TextColor = this.m_ErrorColor;
		this.ShowMessage(message, delay, true);
	}

	// Token: 0x0600986A RID: 39018 RVA: 0x003159F0 File Offset: 0x00313BF0
	public void HideIfScreenshotMessage()
	{
		if (this.m_currentStatusType != UIStatus.StatusType.SCREENSHOT)
		{
			return;
		}
		iTween.Stop(this.m_Text.gameObject);
		this.OnFadeComplete();
	}

	// Token: 0x0600986B RID: 39019 RVA: 0x00315A12 File Offset: 0x00313C12
	private void ShowMessage(string message)
	{
		this.ShowMessage(message, -1f, true);
	}

	// Token: 0x0600986C RID: 39020 RVA: 0x00315A24 File Offset: 0x00313C24
	private void ShowMessage(string message, float delay, bool richText = true)
	{
		Log.UIStatus.PrintDebug(message, Array.Empty<object>());
		this.m_Text.Text = string.Empty;
		this.m_Text.RichText = richText;
		if (message.Contains("\n"))
		{
			this.m_Text.ResizeToFit = false;
			this.m_Text.WordWrap = true;
			this.m_Text.ForceWrapLargeWords = true;
		}
		else
		{
			this.m_Text.ResizeToFit = true;
			this.m_Text.WordWrap = false;
			this.m_Text.ForceWrapLargeWords = false;
		}
		this.m_Text.Text = message;
		this.m_Text.gameObject.SetActive(true);
		this.m_Text.TextAlpha = 1f;
		iTween.Stop(this.m_Text.gameObject, true);
		if (delay < 0f)
		{
			delay = this.m_FadeDelaySec;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"delay",
			delay,
			"time",
			this.m_FadeSec,
			"easeType",
			this.m_FadeEaseType,
			"oncomplete",
			"OnFadeComplete",
			"oncompletetarget",
			base.gameObject
		});
		iTween.FadeTo(this.m_Text.gameObject, args);
	}

	// Token: 0x0600986D RID: 39021 RVA: 0x00315B96 File Offset: 0x00313D96
	private void OnFadeComplete()
	{
		this.m_currentStatusType = UIStatus.StatusType.GENERIC;
		this.m_Text.gameObject.SetActive(false);
	}

	// Token: 0x04007F6B RID: 32619
	public UberText m_Text;

	// Token: 0x04007F6C RID: 32620
	public Color m_InfoColor;

	// Token: 0x04007F6D RID: 32621
	public Color m_ErrorColor;

	// Token: 0x04007F6E RID: 32622
	public float m_FadeDelaySec = 2f;

	// Token: 0x04007F6F RID: 32623
	public float m_FadeSec = 0.5f;

	// Token: 0x04007F70 RID: 32624
	public iTween.EaseType m_FadeEaseType = iTween.EaseType.linear;

	// Token: 0x04007F71 RID: 32625
	private static UIStatus s_instance;

	// Token: 0x04007F72 RID: 32626
	private UIStatus.StatusType m_currentStatusType;

	// Token: 0x0200277E RID: 10110
	public enum StatusType
	{
		// Token: 0x0400F42B RID: 62507
		GENERIC,
		// Token: 0x0400F42C RID: 62508
		SCREENSHOT
	}
}
