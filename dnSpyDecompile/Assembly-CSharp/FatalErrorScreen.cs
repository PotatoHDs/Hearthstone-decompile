using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020002DF RID: 735
public class FatalErrorScreen : MonoBehaviour
{
	// Token: 0x0600266F RID: 9839 RVA: 0x000C10D0 File Offset: 0x000BF2D0
	private void Awake()
	{
		LogoAnimation logoAnimation = LogoAnimation.Get();
		if (logoAnimation != null)
		{
			logoAnimation.HideLogo();
		}
		this.m_closedSignTitle.Text = GameStrings.Get("GLOBAL_SPLASH_CLOSED_SIGN_TITLE");
		List<FatalErrorMessage> messages = FatalErrorMgr.Get().GetMessages();
		if (messages.Count > 0)
		{
			this.m_closedSignText.Text = messages[0].m_text;
			this.m_allowClick = messages[0].m_allowClick;
			this.m_redirectToStore = messages[0].m_redirectToStore;
			this.m_delayBeforeNextReset = messages[0].m_delayBeforeNextReset;
		}
		else if (Application.isEditor)
		{
			this.m_closedSignText.Text = "Please make it sure FatalError scene is NOT in your Hierarchy window.";
		}
		this.m_isUnrecoverable = FatalErrorMgr.Get().IsUnrecoverable;
	}

	// Token: 0x06002670 RID: 9840 RVA: 0x000C1194 File Offset: 0x000BF394
	private void Start()
	{
		if (HearthstoneApplication.AllowResetFromFatalError)
		{
			if (this.m_isUnrecoverable)
			{
				this.m_allowClick = false;
				this.m_reconnectTip.SetGameStringText("GLOBAL_MOBILE_RESTART_APPLICATION");
				this.m_reconnectTip.gameObject.SetActive(true);
			}
			else if (this.m_allowClick)
			{
				if (this.m_redirectToStore)
				{
					this.m_reconnectTip.SetGameStringText("GLOBAL_MOBILE_TAP_TO_UPDATE");
				}
				else
				{
					this.m_reconnectTip.SetGameStringText("GLOBAL_MOBILE_TAP_TO_RECONNECT");
				}
				this.m_reconnectTip.gameObject.SetActive(true);
			}
		}
		base.StartCoroutine(this.WaitForUIThenFinishSetup());
	}

	// Token: 0x06002671 RID: 9841 RVA: 0x000C1230 File Offset: 0x000BF430
	private void Update()
	{
		if (this.m_reconnectTip.gameObject.activeSelf)
		{
			this.m_reconnectTip.TextAlpha = (Mathf.Sin(Time.time * 3.1415927f / 1f) + 1f) / 2f;
		}
	}

	// Token: 0x06002672 RID: 9842 RVA: 0x000C127C File Offset: 0x000BF47C
	private void OnDestroy()
	{
		if (PegUI.Get() != null)
		{
			PegUI.Get().RemoveInputCamera(this.m_camera);
		}
	}

	// Token: 0x06002673 RID: 9843 RVA: 0x000C129C File Offset: 0x000BF49C
	public void Show()
	{
		base.gameObject.SetActive(true);
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			1f,
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeOutCubic
		});
		iTween.FadeTo(base.gameObject, args);
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x000C1308 File Offset: 0x000BF508
	private IEnumerator WaitForUIThenFinishSetup()
	{
		while (PegUI.Get() == null || OverlayUI.Get() == null)
		{
			yield return null;
		}
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		this.Show();
		this.m_camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		PegUI.Get().AddInputCamera(this.m_camera);
		GameObject gameObject = CameraUtils.CreateInputBlocker(this.m_camera, "ClosedSignInputBlocker", this);
		SceneUtils.SetLayer(gameObject, base.gameObject.layer, null);
		this.m_inputBlocker = gameObject.AddComponent<PegUIElement>();
		if (this.m_allowClick)
		{
			this.m_inputBlocker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		}
		if (FatalErrorMgr.Get().GetFormattedErrorCode() != null)
		{
			this.m_errorCodeText.gameObject.SetActive(true);
			this.m_errorCodeText.Text = FatalErrorMgr.Get().GetFormattedErrorCode();
			OverlayUI.Get().AddGameObject(this.m_errorCodeText.gameObject, CanvasAnchor.TOP_RIGHT, false, CanvasScaleMode.HEIGHT);
		}
		if (this.m_isUnrecoverable)
		{
			Processor.TerminateAllProcessing();
		}
		yield break;
	}

	// Token: 0x06002675 RID: 9845 RVA: 0x000C1318 File Offset: 0x000BF518
	private void OnClick(UIEvent e)
	{
		if (!HearthstoneApplication.AllowResetFromFatalError)
		{
			this.m_inputBlocker.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
			HearthstoneApplication.Get().Exit();
			return;
		}
		if (this.m_redirectToStore)
		{
			UpdateUtils.OpenAppStore();
			return;
		}
		float num = HearthstoneApplication.Get().LastResetTime() + this.m_delayBeforeNextReset - Time.realtimeSinceStartup;
		if (num > 0f)
		{
			this.m_inputBlocker.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
			this.m_closedSignText.Text = GameStrings.Get("GLOBAL_SPLASH_CLOSED_RECONNECTING");
			this.m_allowClick = false;
			this.m_reconnectTip.gameObject.SetActive(false);
			base.StartCoroutine(this.WaitBeforeReconnecting(num));
			return;
		}
		Debug.Log("resetting!");
		this.m_inputBlocker.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClick));
		HearthstoneApplication.Get().Reset();
	}

	// Token: 0x06002676 RID: 9846 RVA: 0x000C1408 File Offset: 0x000BF608
	private IEnumerator WaitBeforeReconnecting(float waitDuration)
	{
		yield return new WaitForSeconds(waitDuration);
		HearthstoneApplication.Get().Reset();
		yield break;
	}

	// Token: 0x040015CD RID: 5581
	public UberText m_closedSignText;

	// Token: 0x040015CE RID: 5582
	public UberText m_closedSignTitle;

	// Token: 0x040015CF RID: 5583
	public UberText m_reconnectTip;

	// Token: 0x040015D0 RID: 5584
	public UberText m_errorCodeText;

	// Token: 0x040015D1 RID: 5585
	private Camera m_camera;

	// Token: 0x040015D2 RID: 5586
	private PegUIElement m_inputBlocker;

	// Token: 0x040015D3 RID: 5587
	private bool m_allowClick;

	// Token: 0x040015D4 RID: 5588
	private bool m_redirectToStore;

	// Token: 0x040015D5 RID: 5589
	public float m_delayBeforeNextReset;

	// Token: 0x040015D6 RID: 5590
	private bool m_isUnrecoverable;
}
