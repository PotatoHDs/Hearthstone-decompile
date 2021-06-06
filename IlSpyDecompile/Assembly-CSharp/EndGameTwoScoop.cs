using System.Collections;
using UnityEngine;

public class EndGameTwoScoop : MonoBehaviour
{
	public UberText m_bannerLabel;

	public GameObject m_heroBone;

	public Actor m_heroActor;

	public HeroXPBar m_xpBarPrefab;

	public GameObject m_levelUpTier1;

	public GameObject m_levelUpTier2;

	public GameObject m_levelUpTier3;

	protected bool m_heroActorLoaded;

	protected HeroXPBar m_xpBar;

	private bool m_isShown;

	private static readonly float AFTER_PUNCH_SCALE_VAL = 2.3f;

	protected static readonly float START_SCALE_VAL = 0.01f;

	protected static readonly float END_SCALE_VAL = 2.5f;

	protected static readonly Vector3 START_POSITION = new Vector3(-7.8f, 8.2f, -5f);

	protected static readonly float BAR_ANIMATION_DELAY = 1f;

	public virtual void Awake()
	{
		base.gameObject.SetActive(value: false);
		AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", OnHeroActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	public virtual void OnDestroy()
	{
	}

	private void Start()
	{
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		ResetPositions();
	}

	public bool IsShown()
	{
		return m_isShown;
	}

	public void Show(bool showXPBar = true)
	{
		m_isShown = true;
		base.gameObject.SetActive(value: true);
		ShowImpl();
		if (showXPBar && !GameMgr.Get().IsTutorial() && !GameMgr.Get().IsSpectator())
		{
			NetCache.HeroLevel heroLevel = GameUtils.GetHeroLevel(GameState.Get().GetFriendlySidePlayer().GetStartingHero()
				.GetClass());
			int totalLevel = GameUtils.GetTotalHeroLevel() ?? 0;
			if (heroLevel == null)
			{
				HideXpBar();
			}
			else if (m_xpBarPrefab != null)
			{
				m_xpBar = Object.Instantiate(m_xpBarPrefab);
				m_xpBar.transform.parent = m_heroActor.transform;
				m_xpBar.transform.localScale = new Vector3(0.9064f, 0.9064f, 0.9064f);
				m_xpBar.transform.localPosition = new Vector3(-0.166f, 0.224f, -0.738f);
				NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
				m_xpBar.m_soloLevelLimit = netObject?.XPSoloLimit ?? 60;
				m_xpBar.m_isAnimated = true;
				m_xpBar.m_delay = BAR_ANIMATION_DELAY;
				m_xpBar.m_levelUpCallback = PlayLevelUpEffect;
				m_xpBar.UpdateDisplay(heroLevel, totalLevel);
			}
		}
	}

	public void Hide()
	{
		HideAll();
	}

	public bool IsLoaded()
	{
		return m_heroActorLoaded;
	}

	public void HideXpBar()
	{
		if (m_xpBar != null)
		{
			m_xpBar.gameObject.SetActive(value: false);
		}
	}

	public virtual void StopAnimating()
	{
	}

	protected virtual void ShowImpl()
	{
	}

	protected virtual void ResetPositions()
	{
	}

	protected void SetBannerLabel(string label)
	{
		m_bannerLabel.Text = label;
	}

	protected void EnableBannerLabel(bool enable)
	{
		m_bannerLabel.gameObject.SetActive(enable);
	}

	protected void PunchEndGameTwoScoop()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().SetPlayingBlockingAnim(set: false);
		}
		iTween.ScaleTo(base.gameObject, new Vector3(AFTER_PUNCH_SCALE_VAL, AFTER_PUNCH_SCALE_VAL, AFTER_PUNCH_SCALE_VAL), 0.15f);
	}

	private void HideAll()
	{
		ScreenEffectsMgr.Get().SetActive(enabled: false);
		Hashtable args = iTween.Hash("scale", new Vector3(START_SCALE_VAL, START_SCALE_VAL, START_SCALE_VAL), "time", 0.25f, "oncomplete", "OnAllHidden", "oncompletetarget", base.gameObject);
		iTween.FadeTo(base.gameObject, 0f, 0.25f);
		iTween.ScaleTo(base.gameObject, args);
		m_isShown = false;
	}

	private void OnAllHidden()
	{
		iTween.FadeTo(base.gameObject, 0f, 0f);
		base.gameObject.SetActive(value: false);
		ResetPositions();
	}

	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.parent = base.transform;
		go.transform.localPosition = m_heroBone.transform.localPosition;
		go.transform.localScale = m_heroBone.transform.localScale;
		m_heroActor = go.GetComponent<Actor>();
		m_heroActor.TurnOffCollider();
		m_heroActor.m_healthObject.SetActive(value: false);
		m_heroActorLoaded = true;
		m_heroActor.SetPremium(GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetPremium());
		m_heroActor.UpdateAllComponents();
	}

	protected void PlayLevelUpEffect()
	{
		GameObject gameObject = Object.Instantiate(m_levelUpTier1);
		if ((bool)gameObject)
		{
			gameObject.transform.parent = base.transform;
			gameObject.GetComponent<PlayMakerFSM>().SendEvent("Birth");
		}
	}
}
