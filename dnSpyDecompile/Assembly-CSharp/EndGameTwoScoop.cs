using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class EndGameTwoScoop : MonoBehaviour
{
	// Token: 0x060025A6 RID: 9638 RVA: 0x000BD886 File Offset: 0x000BBA86
	public virtual void Awake()
	{
		base.gameObject.SetActive(false);
		AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", new PrefabCallback<GameObject>(this.OnHeroActorLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x060025A7 RID: 9639 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnDestroy()
	{
	}

	// Token: 0x060025A8 RID: 9640 RVA: 0x000BD8B7 File Offset: 0x000BBAB7
	private void Start()
	{
		SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		this.ResetPositions();
	}

	// Token: 0x060025A9 RID: 9641 RVA: 0x000BD8CC File Offset: 0x000BBACC
	public bool IsShown()
	{
		return this.m_isShown;
	}

	// Token: 0x060025AA RID: 9642 RVA: 0x000BD8D4 File Offset: 0x000BBAD4
	public void Show(bool showXPBar = true)
	{
		this.m_isShown = true;
		base.gameObject.SetActive(true);
		this.ShowImpl();
		if (showXPBar && !GameMgr.Get().IsTutorial() && !GameMgr.Get().IsSpectator())
		{
			NetCache.HeroLevel heroLevel = GameUtils.GetHeroLevel(GameState.Get().GetFriendlySidePlayer().GetStartingHero().GetClass());
			int totalLevel = GameUtils.GetTotalHeroLevel() ?? 0;
			if (heroLevel == null)
			{
				this.HideXpBar();
				return;
			}
			if (this.m_xpBarPrefab != null)
			{
				this.m_xpBar = UnityEngine.Object.Instantiate<HeroXPBar>(this.m_xpBarPrefab);
				this.m_xpBar.transform.parent = this.m_heroActor.transform;
				this.m_xpBar.transform.localScale = new Vector3(0.9064f, 0.9064f, 0.9064f);
				this.m_xpBar.transform.localPosition = new Vector3(-0.166f, 0.224f, -0.738f);
				NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
				this.m_xpBar.m_soloLevelLimit = ((netObject == null) ? 60 : netObject.XPSoloLimit);
				this.m_xpBar.m_isAnimated = true;
				this.m_xpBar.m_delay = EndGameTwoScoop.BAR_ANIMATION_DELAY;
				this.m_xpBar.m_levelUpCallback = new HeroXPBar.PlayLevelUpEffectCallback(this.PlayLevelUpEffect);
				this.m_xpBar.UpdateDisplay(heroLevel, totalLevel);
			}
		}
	}

	// Token: 0x060025AB RID: 9643 RVA: 0x000BDA44 File Offset: 0x000BBC44
	public void Hide()
	{
		this.HideAll();
	}

	// Token: 0x060025AC RID: 9644 RVA: 0x000BDA4C File Offset: 0x000BBC4C
	public bool IsLoaded()
	{
		return this.m_heroActorLoaded;
	}

	// Token: 0x060025AD RID: 9645 RVA: 0x000BDA54 File Offset: 0x000BBC54
	public void HideXpBar()
	{
		if (this.m_xpBar != null)
		{
			this.m_xpBar.gameObject.SetActive(false);
		}
	}

	// Token: 0x060025AE RID: 9646 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void StopAnimating()
	{
	}

	// Token: 0x060025AF RID: 9647 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void ShowImpl()
	{
	}

	// Token: 0x060025B0 RID: 9648 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void ResetPositions()
	{
	}

	// Token: 0x060025B1 RID: 9649 RVA: 0x000BDA75 File Offset: 0x000BBC75
	protected void SetBannerLabel(string label)
	{
		this.m_bannerLabel.Text = label;
	}

	// Token: 0x060025B2 RID: 9650 RVA: 0x000BDA83 File Offset: 0x000BBC83
	protected void EnableBannerLabel(bool enable)
	{
		this.m_bannerLabel.gameObject.SetActive(enable);
	}

	// Token: 0x060025B3 RID: 9651 RVA: 0x000BDA96 File Offset: 0x000BBC96
	protected void PunchEndGameTwoScoop()
	{
		if (EndGameScreen.Get() != null)
		{
			EndGameScreen.Get().SetPlayingBlockingAnim(false);
		}
		iTween.ScaleTo(base.gameObject, new Vector3(EndGameTwoScoop.AFTER_PUNCH_SCALE_VAL, EndGameTwoScoop.AFTER_PUNCH_SCALE_VAL, EndGameTwoScoop.AFTER_PUNCH_SCALE_VAL), 0.15f);
	}

	// Token: 0x060025B4 RID: 9652 RVA: 0x000BDAD4 File Offset: 0x000BBCD4
	private void HideAll()
	{
		ScreenEffectsMgr.Get().SetActive(false);
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(EndGameTwoScoop.START_SCALE_VAL, EndGameTwoScoop.START_SCALE_VAL, EndGameTwoScoop.START_SCALE_VAL),
			"time",
			0.25f,
			"oncomplete",
			"OnAllHidden",
			"oncompletetarget",
			base.gameObject
		});
		iTween.FadeTo(base.gameObject, 0f, 0.25f);
		iTween.ScaleTo(base.gameObject, args);
		this.m_isShown = false;
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x000BDB7A File Offset: 0x000BBD7A
	private void OnAllHidden()
	{
		iTween.FadeTo(base.gameObject, 0f, 0f);
		base.gameObject.SetActive(false);
		this.ResetPositions();
	}

	// Token: 0x060025B6 RID: 9654 RVA: 0x000BDBA4 File Offset: 0x000BBDA4
	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.transform.parent = base.transform;
		go.transform.localPosition = this.m_heroBone.transform.localPosition;
		go.transform.localScale = this.m_heroBone.transform.localScale;
		this.m_heroActor = go.GetComponent<Actor>();
		this.m_heroActor.TurnOffCollider();
		this.m_heroActor.m_healthObject.SetActive(false);
		this.m_heroActorLoaded = true;
		this.m_heroActor.SetPremium(GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetPremium());
		this.m_heroActor.UpdateAllComponents();
	}

	// Token: 0x060025B7 RID: 9655 RVA: 0x000BDC54 File Offset: 0x000BBE54
	protected void PlayLevelUpEffect()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_levelUpTier1);
		if (gameObject)
		{
			gameObject.transform.parent = base.transform;
			gameObject.GetComponent<PlayMakerFSM>().SendEvent("Birth");
		}
	}

	// Token: 0x0400150D RID: 5389
	public UberText m_bannerLabel;

	// Token: 0x0400150E RID: 5390
	public GameObject m_heroBone;

	// Token: 0x0400150F RID: 5391
	public Actor m_heroActor;

	// Token: 0x04001510 RID: 5392
	public HeroXPBar m_xpBarPrefab;

	// Token: 0x04001511 RID: 5393
	public GameObject m_levelUpTier1;

	// Token: 0x04001512 RID: 5394
	public GameObject m_levelUpTier2;

	// Token: 0x04001513 RID: 5395
	public GameObject m_levelUpTier3;

	// Token: 0x04001514 RID: 5396
	protected bool m_heroActorLoaded;

	// Token: 0x04001515 RID: 5397
	protected HeroXPBar m_xpBar;

	// Token: 0x04001516 RID: 5398
	private bool m_isShown;

	// Token: 0x04001517 RID: 5399
	private static readonly float AFTER_PUNCH_SCALE_VAL = 2.3f;

	// Token: 0x04001518 RID: 5400
	protected static readonly float START_SCALE_VAL = 0.01f;

	// Token: 0x04001519 RID: 5401
	protected static readonly float END_SCALE_VAL = 2.5f;

	// Token: 0x0400151A RID: 5402
	protected static readonly Vector3 START_POSITION = new Vector3(-7.8f, 8.2f, -5f);

	// Token: 0x0400151B RID: 5403
	protected static readonly float BAR_ANIMATION_DELAY = 1f;
}
