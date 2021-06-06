using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008DF RID: 2271
public class HeroXPBar : PegUIElement
{
	// Token: 0x06007DC4 RID: 32196 RVA: 0x0028CB07 File Offset: 0x0028AD07
	protected override void Awake()
	{
		base.Awake();
		this.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnProgressBarOver));
		this.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnProgressBarOut));
	}

	// Token: 0x06007DC5 RID: 32197 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void EmptyLevelUpFunction()
	{
	}

	// Token: 0x06007DC6 RID: 32198 RVA: 0x0028CB38 File Offset: 0x0028AD38
	public void UpdateDisplay(NetCache.HeroLevel heroLevel, int totalLevel)
	{
		if (heroLevel == null)
		{
			base.gameObject.SetActive(false);
			return;
		}
		this.m_heroLevel = heroLevel;
		this.m_totalLevel = totalLevel;
		RewardUtils.GetNextHeroLevelRewardText(this.m_heroLevel.Class, this.m_heroLevel.CurrentLevel.Level, this.m_totalLevel, out this.m_rewardTitle, out this.m_rewardDesc);
		base.gameObject.SetActive(true);
		if (this.m_isOnDeck)
		{
			this.m_simpleFrame.SetActive(true);
			this.m_heroFrame.SetActive(false);
		}
		else
		{
			this.m_simpleFrame.SetActive(false);
			this.m_heroFrame.SetActive(true);
		}
		this.SetBarText("");
		if (this.m_isAnimated)
		{
			this.m_heroLevelText.Text = this.m_heroLevel.PrevLevel.Level.ToString();
			if (this.m_heroLevel.PrevLevel.IsMaxLevel())
			{
				this.SetBarValue(1f);
				return;
			}
			this.SetBarValue((float)this.m_heroLevel.PrevLevel.XP / (float)this.m_heroLevel.PrevLevel.MaxXP);
			base.StartCoroutine(this.DelayBarAnimation(this.m_heroLevel.PrevLevel, this.m_heroLevel.CurrentLevel));
			return;
		}
		else
		{
			this.m_heroLevelText.Text = this.m_heroLevel.CurrentLevel.Level.ToString();
			if (this.m_heroLevel.CurrentLevel.IsMaxLevel())
			{
				this.SetBarValue(1f);
				return;
			}
			this.SetBarValue((float)this.m_heroLevel.CurrentLevel.XP / (float)this.m_heroLevel.CurrentLevel.MaxXP);
			return;
		}
	}

	// Token: 0x06007DC7 RID: 32199 RVA: 0x0028CCEC File Offset: 0x0028AEEC
	public void AnimateBar(NetCache.HeroLevel.LevelInfo previousLevelInfo, NetCache.HeroLevel.LevelInfo currentLevelInfo)
	{
		this.m_heroLevelText.Text = previousLevelInfo.Level.ToString();
		if (previousLevelInfo.Level < currentLevelInfo.Level)
		{
			float prevVal = (float)previousLevelInfo.XP / (float)previousLevelInfo.MaxXP;
			float currVal = 1f;
			this.m_progressBar.AnimateProgress(prevVal, currVal, iTween.EaseType.easeOutQuad);
			float animationTime = this.m_progressBar.GetAnimationTime();
			base.StartCoroutine(this.AnimatePostLevelUpXp(animationTime, currentLevelInfo));
			return;
		}
		float prevVal2 = (float)previousLevelInfo.XP / (float)previousLevelInfo.MaxXP;
		float currVal2 = (float)currentLevelInfo.XP / (float)currentLevelInfo.MaxXP;
		if (currentLevelInfo.IsMaxLevel())
		{
			currVal2 = 1f;
		}
		this.m_progressBar.AnimateProgress(prevVal2, currVal2, iTween.EaseType.easeOutQuad);
	}

	// Token: 0x06007DC8 RID: 32200 RVA: 0x0028CDA1 File Offset: 0x0028AFA1
	public void SetBarValue(float barValue)
	{
		this.m_progressBar.SetProgressBar(barValue);
	}

	// Token: 0x06007DC9 RID: 32201 RVA: 0x0028CDAF File Offset: 0x0028AFAF
	public void SetBarText(string barText)
	{
		if (this.m_barText != null)
		{
			this.m_barText.Text = barText;
		}
	}

	// Token: 0x06007DCA RID: 32202 RVA: 0x0028CDCB File Offset: 0x0028AFCB
	private IEnumerator AnimatePostLevelUpXp(float delayTime, NetCache.HeroLevel.LevelInfo currentLevelInfo)
	{
		yield return new WaitForSeconds(delayTime);
		if (currentLevelInfo.Level == 3 && !Options.Get().GetBool(Option.HAS_SEEN_LEVEL_3, false) && UserAttentionManager.CanShowAttentionGrabber("HeroXPBar.AnimatePostLevelUpXp:" + Option.HAS_SEEN_LEVEL_3))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_LEVEL3_TIP"), "VO_INNKEEPER_LEVEL3_TIP.prefab:0f82ce6c91fccf249b6abcc9f153ff1e", 0f, null, false);
			Options.Get().SetBool(Option.HAS_SEEN_LEVEL_3, true);
		}
		this.m_heroLevelText.Text = currentLevelInfo.Level.ToString();
		float currVal = (float)currentLevelInfo.XP / (float)currentLevelInfo.MaxXP;
		this.m_progressBar.AnimateProgress(0f, currVal, iTween.EaseType.easeOutQuad);
		if (this.m_levelUpCallback != null)
		{
			this.m_levelUpCallback();
		}
		yield break;
	}

	// Token: 0x06007DCB RID: 32203 RVA: 0x0028CDE8 File Offset: 0x0028AFE8
	private IEnumerator DelayBarAnimation(NetCache.HeroLevel.LevelInfo prevInfo, NetCache.HeroLevel.LevelInfo currInfo)
	{
		yield return new WaitForSeconds(this.m_delay);
		this.AnimateBar(prevInfo, currInfo);
		yield break;
	}

	// Token: 0x06007DCC RID: 32204 RVA: 0x0028CE08 File Offset: 0x0028B008
	private void ShowTooltip()
	{
		if (string.IsNullOrEmpty(this.m_rewardTitle))
		{
			return;
		}
		TooltipZone component = base.gameObject.GetComponent<TooltipZone>();
		float num;
		if (SceneMgr.Get().IsInGame())
		{
			num = TooltipPanel.MULLIGAN_SCALE;
		}
		else if (UniversalInputManager.UsePhoneUI)
		{
			num = TooltipPanel.BOX_SCALE;
		}
		else
		{
			num = TooltipPanel.COLLECTION_MANAGER_SCALE;
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			num *= 1.1f;
		}
		component.ShowTooltip(this.m_rewardTitle, this.m_rewardDesc, num, 0);
	}

	// Token: 0x06007DCD RID: 32205 RVA: 0x0028CE93 File Offset: 0x0028B093
	private void OnProgressBarOver(UIEvent e)
	{
		this.ShowTooltip();
	}

	// Token: 0x06007DCE RID: 32206 RVA: 0x0028CE9B File Offset: 0x0028B09B
	private void OnProgressBarOut(UIEvent e)
	{
		base.gameObject.GetComponent<TooltipZone>().HideTooltip();
	}

	// Token: 0x040065E0 RID: 26080
	public ProgressBar m_progressBar;

	// Token: 0x040065E1 RID: 26081
	public UberText m_heroLevelText;

	// Token: 0x040065E2 RID: 26082
	public UberText m_barText;

	// Token: 0x040065E3 RID: 26083
	public GameObject m_simpleFrame;

	// Token: 0x040065E4 RID: 26084
	public GameObject m_heroFrame;

	// Token: 0x040065E5 RID: 26085
	public bool m_isAnimated;

	// Token: 0x040065E6 RID: 26086
	public float m_delay;

	// Token: 0x040065E7 RID: 26087
	public bool m_isOnDeck;

	// Token: 0x040065E8 RID: 26088
	public int m_soloLevelLimit;

	// Token: 0x040065E9 RID: 26089
	public HeroXPBar.PlayLevelUpEffectCallback m_levelUpCallback;

	// Token: 0x040065EA RID: 26090
	private NetCache.HeroLevel m_heroLevel;

	// Token: 0x040065EB RID: 26091
	private int m_totalLevel;

	// Token: 0x040065EC RID: 26092
	private string m_rewardTitle;

	// Token: 0x040065ED RID: 26093
	private string m_rewardDesc;

	// Token: 0x02002571 RID: 9585
	// (Invoke) Token: 0x06013319 RID: 78617
	public delegate void PlayLevelUpEffectCallback();
}
