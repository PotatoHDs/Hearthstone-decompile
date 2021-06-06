using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
[CustomEditClass]
public class AdventureWingOpenBanner : MonoBehaviour
{
	// Token: 0x0600056E RID: 1390 RVA: 0x0001F704 File Offset: 0x0001D904
	private void Awake()
	{
		if (this.m_clickCatcher != null)
		{
			this.m_clickCatcher.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
			{
				this.HideBanner();
			});
		}
		if (this.m_root != null)
		{
			this.m_root.SetActive(false);
		}
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, true, CanvasScaleMode.HEIGHT);
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0001F768 File Offset: 0x0001D968
	public void ShowBanner(AdventureWingOpenBanner.OnBannerHidden onBannerHiddenCallback = null)
	{
		if (this.m_root == null)
		{
			Debug.LogError("m_root not defined in banner!");
			return;
		}
		this.m_bannerHiddenCallback = onBannerHiddenCallback;
		this.m_originalScale = this.m_root.transform.localScale;
		this.m_root.SetActive(true);
		iTween.ScaleFrom(this.m_root, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0.01f, 0.01f, 0.01f),
			"time",
			this.m_showTime,
			"easetype",
			this.m_showEase
		}));
		if (!string.IsNullOrEmpty(this.m_showSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_showSound);
		}
		if (!string.IsNullOrEmpty(this.m_VOQuotePrefab) && !string.IsNullOrEmpty(this.m_VOQuoteLine))
		{
			string legacyAssetName = new AssetReference(this.m_VOQuoteLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(this.m_VOQuotePrefab, this.m_VOQuotePosition, GameStrings.Get(legacyAssetName), this.m_VOQuoteLine, true, 0f, null, CanvasAnchor.CENTER, false);
		}
		FullScreenFXMgr.Get().StartStandardBlurVignette(this.m_showTime);
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0001F8A8 File Offset: 0x0001DAA8
	public void HideBanner()
	{
		if (this.m_root == null)
		{
			Debug.LogError("m_root not defined in banner!");
			return;
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(this.m_hideTime, null);
		this.m_root.transform.localScale = this.m_originalScale;
		iTween.ScaleTo(this.m_root, iTween.Hash(new object[]
		{
			"scale",
			new Vector3(0.01f, 0.01f, 0.01f),
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				if (this.m_bannerHiddenCallback != null)
				{
					this.m_bannerHiddenCallback();
				}
			}),
			"time",
			this.m_hideTime
		}));
		if (!string.IsNullOrEmpty(this.m_hideSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_hideSound);
		}
		if (!string.IsNullOrEmpty(this.m_BannerDismissedQuotePrefab) && !string.IsNullOrEmpty(this.m_BannerDismissedQuoteLine))
		{
			string legacyAssetName = new AssetReference(this.m_BannerDismissedQuoteLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(this.m_BannerDismissedQuotePrefab, this.m_BannerDismissedQuotePosition, GameStrings.Get(legacyAssetName), this.m_BannerDismissedQuoteLine, true, 0f, null, CanvasAnchor.CENTER, false);
		}
	}

	// Token: 0x040003BC RID: 956
	public PegUIElement m_clickCatcher;

	// Token: 0x040003BD RID: 957
	public GameObject m_root;

	// Token: 0x040003BE RID: 958
	public iTween.EaseType m_showEase = iTween.EaseType.easeOutElastic;

	// Token: 0x040003BF RID: 959
	public float m_showTime = 0.5f;

	// Token: 0x040003C0 RID: 960
	public float m_hideTime = 0.5f;

	// Token: 0x040003C1 RID: 961
	[CustomEditField(Sections = "Shown Quote")]
	public string m_VOQuotePrefab;

	// Token: 0x040003C2 RID: 962
	[CustomEditField(Sections = "Shown Quote")]
	public string m_VOQuoteLine;

	// Token: 0x040003C3 RID: 963
	[CustomEditField(Sections = "Shown Quote")]
	public Vector3 m_VOQuotePosition = new Vector3(0f, 0f, -55f);

	// Token: 0x040003C4 RID: 964
	[CustomEditField(Sections = "Dismissed Quote", T = EditType.GAME_OBJECT)]
	public string m_BannerDismissedQuotePrefab;

	// Token: 0x040003C5 RID: 965
	[CustomEditField(Sections = "Dismissed Quote")]
	public string m_BannerDismissedQuoteLine;

	// Token: 0x040003C6 RID: 966
	[CustomEditField(Sections = "Dismissed Quote")]
	public Vector3 m_BannerDismissedQuotePosition = new Vector3(0f, 0f, -55f);

	// Token: 0x040003C7 RID: 967
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_showSound;

	// Token: 0x040003C8 RID: 968
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_hideSound;

	// Token: 0x040003C9 RID: 969
	private Vector3 m_originalScale;

	// Token: 0x040003CA RID: 970
	private AdventureWingOpenBanner.OnBannerHidden m_bannerHiddenCallback;

	// Token: 0x02001351 RID: 4945
	// (Invoke) Token: 0x0600D726 RID: 55078
	public delegate void OnBannerHidden();
}
