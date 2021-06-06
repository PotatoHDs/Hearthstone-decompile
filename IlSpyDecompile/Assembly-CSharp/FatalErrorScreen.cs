using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

public class FatalErrorScreen : MonoBehaviour
{
	public UberText m_closedSignText;

	public UberText m_closedSignTitle;

	public UberText m_reconnectTip;

	public UberText m_errorCodeText;

	private Camera m_camera;

	private PegUIElement m_inputBlocker;

	private bool m_allowClick;

	private bool m_redirectToStore;

	public float m_delayBeforeNextReset;

	private bool m_isUnrecoverable;

	private void Awake()
	{
		LogoAnimation logoAnimation = LogoAnimation.Get();
		if (logoAnimation != null)
		{
			logoAnimation.HideLogo();
		}
		m_closedSignTitle.Text = GameStrings.Get("GLOBAL_SPLASH_CLOSED_SIGN_TITLE");
		List<FatalErrorMessage> messages = FatalErrorMgr.Get().GetMessages();
		if (messages.Count > 0)
		{
			m_closedSignText.Text = messages[0].m_text;
			m_allowClick = messages[0].m_allowClick;
			m_redirectToStore = messages[0].m_redirectToStore;
			m_delayBeforeNextReset = messages[0].m_delayBeforeNextReset;
		}
		else if (Application.isEditor)
		{
			m_closedSignText.Text = "Please make it sure FatalError scene is NOT in your Hierarchy window.";
		}
		m_isUnrecoverable = FatalErrorMgr.Get().IsUnrecoverable;
	}

	private void Start()
	{
		if ((bool)HearthstoneApplication.AllowResetFromFatalError)
		{
			if (m_isUnrecoverable)
			{
				m_allowClick = false;
				m_reconnectTip.SetGameStringText("GLOBAL_MOBILE_RESTART_APPLICATION");
				m_reconnectTip.gameObject.SetActive(value: true);
			}
			else if (m_allowClick)
			{
				if (m_redirectToStore)
				{
					m_reconnectTip.SetGameStringText("GLOBAL_MOBILE_TAP_TO_UPDATE");
				}
				else
				{
					m_reconnectTip.SetGameStringText("GLOBAL_MOBILE_TAP_TO_RECONNECT");
				}
				m_reconnectTip.gameObject.SetActive(value: true);
			}
		}
		StartCoroutine(WaitForUIThenFinishSetup());
	}

	private void Update()
	{
		if (m_reconnectTip.gameObject.activeSelf)
		{
			m_reconnectTip.TextAlpha = (Mathf.Sin(Time.time * (float)Math.PI / 1f) + 1f) / 2f;
		}
	}

	private void OnDestroy()
	{
		if (PegUI.Get() != null)
		{
			PegUI.Get().RemoveInputCamera(m_camera);
		}
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		Hashtable args = iTween.Hash("amount", 1f, "time", 0.25f, "easeType", iTween.EaseType.easeOutCubic);
		iTween.FadeTo(base.gameObject, args);
	}

	private IEnumerator WaitForUIThenFinishSetup()
	{
		while (PegUI.Get() == null || OverlayUI.Get() == null)
		{
			yield return null;
		}
		OverlayUI.Get().AddGameObject(base.gameObject);
		Show();
		m_camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		PegUI.Get().AddInputCamera(m_camera);
		GameObject gameObject = CameraUtils.CreateInputBlocker(m_camera, "ClosedSignInputBlocker", this);
		SceneUtils.SetLayer(gameObject, base.gameObject.layer);
		m_inputBlocker = gameObject.AddComponent<PegUIElement>();
		if (m_allowClick)
		{
			m_inputBlocker.AddEventListener(UIEventType.RELEASE, OnClick);
		}
		if (FatalErrorMgr.Get().GetFormattedErrorCode() != null)
		{
			m_errorCodeText.gameObject.SetActive(value: true);
			m_errorCodeText.Text = FatalErrorMgr.Get().GetFormattedErrorCode();
			OverlayUI.Get().AddGameObject(m_errorCodeText.gameObject, CanvasAnchor.TOP_RIGHT);
		}
		if (m_isUnrecoverable)
		{
			Processor.TerminateAllProcessing();
		}
	}

	private void OnClick(UIEvent e)
	{
		if ((bool)HearthstoneApplication.AllowResetFromFatalError)
		{
			if (m_redirectToStore)
			{
				UpdateUtils.OpenAppStore();
				return;
			}
			float num = HearthstoneApplication.Get().LastResetTime() + m_delayBeforeNextReset - Time.realtimeSinceStartup;
			if (num > 0f)
			{
				m_inputBlocker.RemoveEventListener(UIEventType.RELEASE, OnClick);
				m_closedSignText.Text = GameStrings.Get("GLOBAL_SPLASH_CLOSED_RECONNECTING");
				m_allowClick = false;
				m_reconnectTip.gameObject.SetActive(value: false);
				StartCoroutine(WaitBeforeReconnecting(num));
			}
			else
			{
				Debug.Log("resetting!");
				m_inputBlocker.RemoveEventListener(UIEventType.RELEASE, OnClick);
				HearthstoneApplication.Get().Reset();
			}
		}
		else
		{
			m_inputBlocker.RemoveEventListener(UIEventType.RELEASE, OnClick);
			HearthstoneApplication.Get().Exit();
		}
	}

	private IEnumerator WaitBeforeReconnecting(float waitDuration)
	{
		yield return new WaitForSeconds(waitDuration);
		HearthstoneApplication.Get().Reset();
	}
}
