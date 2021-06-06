using System;
using UnityEngine;

[CustomEditClass]
public class AdventureWingOpenBanner : MonoBehaviour
{
	public delegate void OnBannerHidden();

	public PegUIElement m_clickCatcher;

	public GameObject m_root;

	public iTween.EaseType m_showEase = iTween.EaseType.easeOutElastic;

	public float m_showTime = 0.5f;

	public float m_hideTime = 0.5f;

	[CustomEditField(Sections = "Shown Quote")]
	public string m_VOQuotePrefab;

	[CustomEditField(Sections = "Shown Quote")]
	public string m_VOQuoteLine;

	[CustomEditField(Sections = "Shown Quote")]
	public Vector3 m_VOQuotePosition = new Vector3(0f, 0f, -55f);

	[CustomEditField(Sections = "Dismissed Quote", T = EditType.GAME_OBJECT)]
	public string m_BannerDismissedQuotePrefab;

	[CustomEditField(Sections = "Dismissed Quote")]
	public string m_BannerDismissedQuoteLine;

	[CustomEditField(Sections = "Dismissed Quote")]
	public Vector3 m_BannerDismissedQuotePosition = new Vector3(0f, 0f, -55f);

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_showSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_hideSound;

	private Vector3 m_originalScale;

	private OnBannerHidden m_bannerHiddenCallback;

	private void Awake()
	{
		if (m_clickCatcher != null)
		{
			m_clickCatcher.AddEventListener(UIEventType.RELEASE, delegate
			{
				HideBanner();
			});
		}
		if (m_root != null)
		{
			m_root.SetActive(value: false);
		}
		OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, destroyOnSceneLoad: true);
	}

	public void ShowBanner(OnBannerHidden onBannerHiddenCallback = null)
	{
		if (m_root == null)
		{
			Debug.LogError("m_root not defined in banner!");
			return;
		}
		m_bannerHiddenCallback = onBannerHiddenCallback;
		m_originalScale = m_root.transform.localScale;
		m_root.SetActive(value: true);
		iTween.ScaleFrom(m_root, iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "time", m_showTime, "easetype", m_showEase));
		if (!string.IsNullOrEmpty(m_showSound))
		{
			SoundManager.Get().LoadAndPlay(m_showSound);
		}
		if (!string.IsNullOrEmpty(m_VOQuotePrefab) && !string.IsNullOrEmpty(m_VOQuoteLine))
		{
			string legacyAssetName = new AssetReference(m_VOQuoteLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(m_VOQuotePrefab, m_VOQuotePosition, GameStrings.Get(legacyAssetName), m_VOQuoteLine, allowRepeatDuringSession: true, 0f, null, CanvasAnchor.CENTER);
		}
		FullScreenFXMgr.Get().StartStandardBlurVignette(m_showTime);
	}

	public void HideBanner()
	{
		if (m_root == null)
		{
			Debug.LogError("m_root not defined in banner!");
			return;
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(m_hideTime);
		m_root.transform.localScale = m_originalScale;
		iTween.ScaleTo(m_root, iTween.Hash("scale", new Vector3(0.01f, 0.01f, 0.01f), "oncomplete", (Action<object>)delegate
		{
			UnityEngine.Object.Destroy(base.gameObject);
			if (m_bannerHiddenCallback != null)
			{
				m_bannerHiddenCallback();
			}
		}, "time", m_hideTime));
		if (!string.IsNullOrEmpty(m_hideSound))
		{
			SoundManager.Get().LoadAndPlay(m_hideSound);
		}
		if (!string.IsNullOrEmpty(m_BannerDismissedQuotePrefab) && !string.IsNullOrEmpty(m_BannerDismissedQuoteLine))
		{
			string legacyAssetName = new AssetReference(m_BannerDismissedQuoteLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(m_BannerDismissedQuotePrefab, m_BannerDismissedQuotePosition, GameStrings.Get(legacyAssetName), m_BannerDismissedQuoteLine, allowRepeatDuringSession: true, 0f, null, CanvasAnchor.CENTER);
		}
	}
}
