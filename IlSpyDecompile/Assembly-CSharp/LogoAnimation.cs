using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone;
using UnityEngine;

public class LogoAnimation : MonoBehaviour
{
	public GameObject m_logoContainer;

	private static LogoAnimation s_instance;

	private GameObject m_logo;

	public UberText m_logoCopyright;

	private AssetReference m_LogoAssetRef = "LogoImage.prefab:c7bbbc47f4498224491bb952df4c6bcb";

	private void Awake()
	{
		s_instance = this;
		m_logo = AssetLoader.Get().InstantiatePrefab(m_LogoAssetRef);
		m_logo.SetActive(value: true);
		GameUtils.SetParent(m_logo, m_logoContainer, withRotation: true);
		m_logoContainer.SetActive(value: false);
		if (Localization.GetLocale() == Locale.zhCN)
		{
			m_logoCopyright.gameObject.SetActive(value: true);
			RenderUtils.SetAlpha(m_logoCopyright.gameObject, 1f);
		}
		HearthstoneApplication.Get().WillReset += OnWillReset;
	}

	public static LogoAnimation Get()
	{
		return s_instance;
	}

	private void OnDestroy()
	{
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset -= OnWillReset;
		}
		s_instance = null;
	}

	public void HideLogo()
	{
		if (m_logoContainer != null)
		{
			m_logoContainer.SetActive(value: false);
		}
	}

	public IEnumerator<IAsyncJobResult> Job_FadeLogoIn()
	{
		float num = 0.5f;
		m_logoContainer.SetActive(value: true);
		Hashtable args = iTween.Hash("amount", 1f, "time", num, "includechildren", true, "easeType", iTween.EaseType.easeInCubic);
		iTween.FadeTo(m_logo, args);
		yield return new WaitForDuration(num);
	}

	public IEnumerator<IAsyncJobResult> Job_FadeLogoOut()
	{
		float num = 0.5f;
		Hashtable args = iTween.Hash("amount", 0f, "delay", 0f, "time", num, "easeType", iTween.EaseType.linear);
		iTween.FadeTo(m_logo, args);
		yield return new WaitForDuration(num);
		DestroyLogoAnimation();
	}

	private void DestroyLogoAnimation()
	{
		Object.Destroy(base.gameObject);
	}

	public void ShowLogo()
	{
		if (m_logoContainer != null)
		{
			m_logoContainer.SetActive(value: true);
		}
	}

	private void OnWillReset()
	{
		Object.Destroy(base.gameObject);
	}
}
