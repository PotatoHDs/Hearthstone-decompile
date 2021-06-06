using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone;
using UnityEngine;

// Token: 0x0200099C RID: 2460
public class LogoAnimation : MonoBehaviour
{
	// Token: 0x06008665 RID: 34405 RVA: 0x002B6954 File Offset: 0x002B4B54
	private void Awake()
	{
		LogoAnimation.s_instance = this;
		this.m_logo = AssetLoader.Get().InstantiatePrefab(this.m_LogoAssetRef, AssetLoadingOptions.None);
		this.m_logo.SetActive(true);
		GameUtils.SetParent(this.m_logo, this.m_logoContainer, true);
		this.m_logoContainer.SetActive(false);
		if (Localization.GetLocale() == Locale.zhCN)
		{
			this.m_logoCopyright.gameObject.SetActive(true);
			RenderUtils.SetAlpha(this.m_logoCopyright.gameObject, 1f);
		}
		HearthstoneApplication.Get().WillReset += this.OnWillReset;
	}

	// Token: 0x06008666 RID: 34406 RVA: 0x002B69ED File Offset: 0x002B4BED
	public static LogoAnimation Get()
	{
		return LogoAnimation.s_instance;
	}

	// Token: 0x06008667 RID: 34407 RVA: 0x002B69F4 File Offset: 0x002B4BF4
	private void OnDestroy()
	{
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset -= this.OnWillReset;
		}
		LogoAnimation.s_instance = null;
	}

	// Token: 0x06008668 RID: 34408 RVA: 0x002B6A1F File Offset: 0x002B4C1F
	public void HideLogo()
	{
		if (this.m_logoContainer != null)
		{
			this.m_logoContainer.SetActive(false);
		}
	}

	// Token: 0x06008669 RID: 34409 RVA: 0x002B6A3B File Offset: 0x002B4C3B
	public IEnumerator<IAsyncJobResult> Job_FadeLogoIn()
	{
		float num = 0.5f;
		this.m_logoContainer.SetActive(true);
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			1f,
			"time",
			num,
			"includechildren",
			true,
			"easeType",
			iTween.EaseType.easeInCubic
		});
		iTween.FadeTo(this.m_logo, args);
		yield return new WaitForDuration(num);
		yield break;
	}

	// Token: 0x0600866A RID: 34410 RVA: 0x002B6A4A File Offset: 0x002B4C4A
	public IEnumerator<IAsyncJobResult> Job_FadeLogoOut()
	{
		float num = 0.5f;
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"delay",
			0f,
			"time",
			num,
			"easeType",
			iTween.EaseType.linear
		});
		iTween.FadeTo(this.m_logo, args);
		yield return new WaitForDuration(num);
		this.DestroyLogoAnimation();
		yield break;
	}

	// Token: 0x0600866B RID: 34411 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	private void DestroyLogoAnimation()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x0600866C RID: 34412 RVA: 0x002B6A59 File Offset: 0x002B4C59
	public void ShowLogo()
	{
		if (this.m_logoContainer != null)
		{
			this.m_logoContainer.SetActive(true);
		}
	}

	// Token: 0x0600866D RID: 34413 RVA: 0x0003DCF6 File Offset: 0x0003BEF6
	private void OnWillReset()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x040071F3 RID: 29171
	public GameObject m_logoContainer;

	// Token: 0x040071F4 RID: 29172
	private static LogoAnimation s_instance;

	// Token: 0x040071F5 RID: 29173
	private GameObject m_logo;

	// Token: 0x040071F6 RID: 29174
	public UberText m_logoCopyright;

	// Token: 0x040071F7 RID: 29175
	private AssetReference m_LogoAssetRef = "LogoImage.prefab:c7bbbc47f4498224491bb952df4c6bcb";
}
