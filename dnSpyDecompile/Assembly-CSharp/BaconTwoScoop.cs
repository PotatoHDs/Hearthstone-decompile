using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002C4 RID: 708
public class BaconTwoScoop : VictoryTwoScoop
{
	// Token: 0x0600252F RID: 9519 RVA: 0x000BB049 File Offset: 0x000B9249
	protected override void ShowImpl()
	{
		base.StartCoroutine(this.ShowWhenReady());
	}

	// Token: 0x06002530 RID: 9520 RVA: 0x000BB058 File Offset: 0x000B9258
	private IEnumerator ShowWhenReady()
	{
		this.m_Root.SetActive(false);
		this.m_heroActor.gameObject.SetActive(false);
		while (GameState.Get() == null || GameState.Get().GetGameEntity() == null)
		{
			yield return null;
		}
		TB_BaconShop baconGameEntity = null;
		if (GameState.Get().GetGameEntity() is TB_BaconShop)
		{
			baconGameEntity = (TB_BaconShop)GameState.Get().GetGameEntity();
		}
		bool ratingChangeDisabled = GameMgr.Get().IsFriendlyBattlegrounds();
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.WAIT_FOR_RATING_INFO))
		{
			while (baconGameEntity != null && baconGameEntity.RatingChangeData == null && !ratingChangeDisabled && this.m_waitForRatingTimeoutTimer < 5f)
			{
				this.m_waitForRatingTimeoutTimer += Time.unscaledDeltaTime;
				yield return null;
			}
		}
		this.m_Root.SetActive(true);
		this.m_heroActor.gameObject.SetActive(true);
		base.SetupHeroActor();
		base.SetupBannerText();
		this.SetupTwoScoopForPlace();
		if (GameMgr.Get().IsSpectator() || baconGameEntity == null || baconGameEntity.RatingChangeData == null)
		{
			this.m_RatingBanner.SetActive(false);
		}
		else
		{
			this.m_newRating = baconGameEntity.RatingChangeData.NewRating;
			this.m_ratingChange = baconGameEntity.RatingChangeData.RatingChange;
			this.m_RatingBanner.SetActive(true);
			yield return this.PlayRatingChangeAnimation();
		}
		yield break;
	}

	// Token: 0x06002531 RID: 9521 RVA: 0x000BB068 File Offset: 0x000B9268
	private void SetupTwoScoopForPlace()
	{
		this.m_Top1Visual.SetActive(false);
		this.m_Top4Visual.SetActive(false);
		this.m_Bottom4Visual.SetActive(false);
		int realTimePlayerLeaderboardPlace = GameState.Get().GetFriendlySidePlayer().GetHero().GetRealTimePlayerLeaderboardPlace();
		if (realTimePlayerLeaderboardPlace <= 1)
		{
			this.m_Top1Visual.SetActive(true);
			SoundManager.Get().Play(this.m_Top1Sound, null, null, null);
			return;
		}
		if (realTimePlayerLeaderboardPlace <= 4)
		{
			this.m_Top4Visual.SetActive(true);
			SoundManager.Get().Play(this.m_Top4Sound, null, null, null);
			return;
		}
		this.m_Bottom4Visual.SetActive(true);
		SoundManager.Get().Play(this.m_Bottom4Sound, null, null, null);
	}

	// Token: 0x06002532 RID: 9522 RVA: 0x000BB118 File Offset: 0x000B9318
	private IEnumerator PlayRatingChangeAnimation()
	{
		int oldRating = this.m_newRating - this.m_ratingChange;
		this.m_RatingChangeText.Text = "";
		this.m_RatingText.Text = oldRating.ToString();
		this.m_RatingChangeText.GetComponent<Animator>().enabled = false;
		this.m_RatingText.GetComponent<Animator>().enabled = false;
		yield return new WaitForSeconds(this.m_DelayBeforeRatingChangeSeconds);
		string text = ((this.m_ratingChange >= 0) ? "+" : "") + this.m_ratingChange.ToString();
		this.m_RatingChangeText.Text = text;
		this.m_RatingChangeText.TextColor = ((this.m_ratingChange >= 0) ? this.m_RatingChangeTextColorPositive : this.m_RatingChangeTextColorNegative);
		this.m_RatingChangeText.GetComponent<Animator>().enabled = true;
		this.m_RatingText.GetComponent<Animator>().enabled = true;
		float timer = 0f;
		while (timer < this.m_RatingTextUpdateTimeSeconds)
		{
			float t = Mathf.Clamp01(timer / this.m_RatingTextUpdateTimeSeconds);
			int num = Mathf.FloorToInt(Mathf.Lerp((float)oldRating, (float)this.m_newRating, t));
			this.m_RatingText.Text = num.ToString();
			timer += Time.deltaTime;
			yield return null;
		}
		this.m_RatingText.Text = this.m_newRating.ToString();
		yield break;
	}

	// Token: 0x040014C1 RID: 5313
	public GameObject m_Root;

	// Token: 0x040014C2 RID: 5314
	public GameObject m_RatingBanner;

	// Token: 0x040014C3 RID: 5315
	public GameObject m_Top1Visual;

	// Token: 0x040014C4 RID: 5316
	public GameObject m_Top4Visual;

	// Token: 0x040014C5 RID: 5317
	public GameObject m_Bottom4Visual;

	// Token: 0x040014C6 RID: 5318
	public AudioSource m_Top1Sound;

	// Token: 0x040014C7 RID: 5319
	public AudioSource m_Top4Sound;

	// Token: 0x040014C8 RID: 5320
	public AudioSource m_Bottom4Sound;

	// Token: 0x040014C9 RID: 5321
	public UberText m_RatingText;

	// Token: 0x040014CA RID: 5322
	public UberText m_RatingChangeText;

	// Token: 0x040014CB RID: 5323
	public Color m_RatingChangeTextColorPositive;

	// Token: 0x040014CC RID: 5324
	public Color m_RatingChangeTextColorNegative;

	// Token: 0x040014CD RID: 5325
	public float m_RatingTextUpdateTimeSeconds = 0.5f;

	// Token: 0x040014CE RID: 5326
	public float m_DelayBeforeRatingChangeSeconds = 0.5f;

	// Token: 0x040014CF RID: 5327
	private const float WAIT_FOR_RATING_TIMEOUT_SECONDS = 5f;

	// Token: 0x040014D0 RID: 5328
	private float m_waitForRatingTimeoutTimer;

	// Token: 0x040014D1 RID: 5329
	private int m_newRating;

	// Token: 0x040014D2 RID: 5330
	private int m_ratingChange;
}
