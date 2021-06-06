using System.Collections;
using UnityEngine;

public class UIStatus : MonoBehaviour
{
	public enum StatusType
	{
		GENERIC,
		SCREENSHOT
	}

	public UberText m_Text;

	public Color m_InfoColor;

	public Color m_ErrorColor;

	public float m_FadeDelaySec = 2f;

	public float m_FadeSec = 0.5f;

	public iTween.EaseType m_FadeEaseType = iTween.EaseType.linear;

	private static UIStatus s_instance;

	private StatusType m_currentStatusType;

	private void Awake()
	{
		s_instance = this;
		m_Text.gameObject.SetActive(value: false);
		if ((bool)OverlayUI.Get())
		{
			OverlayUI.Get().AddGameObject(base.gameObject);
			return;
		}
		throw new UnityException("Trying to create UIStatus before OverlayUI!");
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static UIStatus Get()
	{
		if (s_instance == null)
		{
			s_instance = AssetLoader.Get().InstantiatePrefab("UIStatus.prefab:8fe3c92addcd14427a5277cfedc2341c").GetComponent<UIStatus>();
		}
		return s_instance;
	}

	public void AddInfo(string message)
	{
		AddInfo(message, StatusType.GENERIC);
	}

	public void AddInfo(string message, float delay)
	{
		AddInfo(message, StatusType.GENERIC, delay);
	}

	public void AddInfo(string message, StatusType statusType)
	{
		AddInfo(message, statusType, -1f);
	}

	public void AddInfo(string message, StatusType statusType, float delay)
	{
		m_currentStatusType = statusType;
		m_Text.TextColor = m_InfoColor;
		ShowMessage(message, delay);
	}

	public void AddInfoNoRichText(string message, float delay = -1f)
	{
		m_Text.TextColor = m_InfoColor;
		ShowMessage(message, delay, richText: false);
	}

	public void AddError(string message, float delay = -1f)
	{
		m_Text.TextColor = m_ErrorColor;
		ShowMessage(message, delay);
	}

	public void HideIfScreenshotMessage()
	{
		if (m_currentStatusType == StatusType.SCREENSHOT)
		{
			iTween.Stop(m_Text.gameObject);
			OnFadeComplete();
		}
	}

	private void ShowMessage(string message)
	{
		ShowMessage(message, -1f);
	}

	private void ShowMessage(string message, float delay, bool richText = true)
	{
		Log.UIStatus.PrintDebug(message);
		m_Text.Text = string.Empty;
		m_Text.RichText = richText;
		if (message.Contains("\n"))
		{
			m_Text.ResizeToFit = false;
			m_Text.WordWrap = true;
			m_Text.ForceWrapLargeWords = true;
		}
		else
		{
			m_Text.ResizeToFit = true;
			m_Text.WordWrap = false;
			m_Text.ForceWrapLargeWords = false;
		}
		m_Text.Text = message;
		m_Text.gameObject.SetActive(value: true);
		m_Text.TextAlpha = 1f;
		iTween.Stop(m_Text.gameObject, includechildren: true);
		if (delay < 0f)
		{
			delay = m_FadeDelaySec;
		}
		Hashtable args = iTween.Hash("amount", 0f, "delay", delay, "time", m_FadeSec, "easeType", m_FadeEaseType, "oncomplete", "OnFadeComplete", "oncompletetarget", base.gameObject);
		iTween.FadeTo(m_Text.gameObject, args);
	}

	private void OnFadeComplete()
	{
		m_currentStatusType = StatusType.GENERIC;
		m_Text.gameObject.SetActive(value: false);
	}
}
