using System.Collections;
using UnityEngine;

public class HeroXPBar : PegUIElement
{
	public delegate void PlayLevelUpEffectCallback();

	public ProgressBar m_progressBar;

	public UberText m_heroLevelText;

	public UberText m_barText;

	public GameObject m_simpleFrame;

	public GameObject m_heroFrame;

	public bool m_isAnimated;

	public float m_delay;

	public bool m_isOnDeck;

	public int m_soloLevelLimit;

	public PlayLevelUpEffectCallback m_levelUpCallback;

	private NetCache.HeroLevel m_heroLevel;

	private int m_totalLevel;

	private string m_rewardTitle;

	private string m_rewardDesc;

	protected override void Awake()
	{
		base.Awake();
		AddEventListener(UIEventType.ROLLOVER, OnProgressBarOver);
		AddEventListener(UIEventType.ROLLOUT, OnProgressBarOut);
	}

	public void EmptyLevelUpFunction()
	{
	}

	public void UpdateDisplay(NetCache.HeroLevel heroLevel, int totalLevel)
	{
		if (heroLevel == null)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		m_heroLevel = heroLevel;
		m_totalLevel = totalLevel;
		RewardUtils.GetNextHeroLevelRewardText(m_heroLevel.Class, m_heroLevel.CurrentLevel.Level, m_totalLevel, out m_rewardTitle, out m_rewardDesc);
		base.gameObject.SetActive(value: true);
		if (m_isOnDeck)
		{
			m_simpleFrame.SetActive(value: true);
			m_heroFrame.SetActive(value: false);
		}
		else
		{
			m_simpleFrame.SetActive(value: false);
			m_heroFrame.SetActive(value: true);
		}
		SetBarText("");
		if (m_isAnimated)
		{
			m_heroLevelText.Text = m_heroLevel.PrevLevel.Level.ToString();
			if (m_heroLevel.PrevLevel.IsMaxLevel())
			{
				SetBarValue(1f);
				return;
			}
			SetBarValue((float)m_heroLevel.PrevLevel.XP / (float)m_heroLevel.PrevLevel.MaxXP);
			StartCoroutine(DelayBarAnimation(m_heroLevel.PrevLevel, m_heroLevel.CurrentLevel));
		}
		else
		{
			m_heroLevelText.Text = m_heroLevel.CurrentLevel.Level.ToString();
			if (m_heroLevel.CurrentLevel.IsMaxLevel())
			{
				SetBarValue(1f);
			}
			else
			{
				SetBarValue((float)m_heroLevel.CurrentLevel.XP / (float)m_heroLevel.CurrentLevel.MaxXP);
			}
		}
	}

	public void AnimateBar(NetCache.HeroLevel.LevelInfo previousLevelInfo, NetCache.HeroLevel.LevelInfo currentLevelInfo)
	{
		m_heroLevelText.Text = previousLevelInfo.Level.ToString();
		if (previousLevelInfo.Level < currentLevelInfo.Level)
		{
			float prevVal = (float)previousLevelInfo.XP / (float)previousLevelInfo.MaxXP;
			float currVal = 1f;
			m_progressBar.AnimateProgress(prevVal, currVal);
			float animationTime = m_progressBar.GetAnimationTime();
			StartCoroutine(AnimatePostLevelUpXp(animationTime, currentLevelInfo));
			return;
		}
		float prevVal2 = (float)previousLevelInfo.XP / (float)previousLevelInfo.MaxXP;
		float currVal2 = (float)currentLevelInfo.XP / (float)currentLevelInfo.MaxXP;
		if (currentLevelInfo.IsMaxLevel())
		{
			currVal2 = 1f;
		}
		m_progressBar.AnimateProgress(prevVal2, currVal2);
	}

	public void SetBarValue(float barValue)
	{
		m_progressBar.SetProgressBar(barValue);
	}

	public void SetBarText(string barText)
	{
		if (m_barText != null)
		{
			m_barText.Text = barText;
		}
	}

	private IEnumerator AnimatePostLevelUpXp(float delayTime, NetCache.HeroLevel.LevelInfo currentLevelInfo)
	{
		yield return new WaitForSeconds(delayTime);
		if (currentLevelInfo.Level == 3 && !Options.Get().GetBool(Option.HAS_SEEN_LEVEL_3, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("HeroXPBar.AnimatePostLevelUpXp:" + Option.HAS_SEEN_LEVEL_3))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, NotificationManager.DEPTH, 34.5f), GameStrings.Get("VO_INNKEEPER_LEVEL3_TIP"), "VO_INNKEEPER_LEVEL3_TIP.prefab:0f82ce6c91fccf249b6abcc9f153ff1e");
			Options.Get().SetBool(Option.HAS_SEEN_LEVEL_3, val: true);
		}
		m_heroLevelText.Text = currentLevelInfo.Level.ToString();
		float currVal = (float)currentLevelInfo.XP / (float)currentLevelInfo.MaxXP;
		m_progressBar.AnimateProgress(0f, currVal);
		if (m_levelUpCallback != null)
		{
			m_levelUpCallback();
		}
	}

	private IEnumerator DelayBarAnimation(NetCache.HeroLevel.LevelInfo prevInfo, NetCache.HeroLevel.LevelInfo currInfo)
	{
		yield return new WaitForSeconds(m_delay);
		AnimateBar(prevInfo, currInfo);
	}

	private void ShowTooltip()
	{
		if (!string.IsNullOrEmpty(m_rewardTitle))
		{
			TooltipZone component = base.gameObject.GetComponent<TooltipZone>();
			float num = (SceneMgr.Get().IsInGame() ? ((float)TooltipPanel.MULLIGAN_SCALE) : ((!UniversalInputManager.UsePhoneUI) ? ((float)TooltipPanel.COLLECTION_MANAGER_SCALE) : ((float)TooltipPanel.BOX_SCALE)));
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				num *= 1.1f;
			}
			component.ShowTooltip(m_rewardTitle, m_rewardDesc, num);
		}
	}

	private void OnProgressBarOver(UIEvent e)
	{
		ShowTooltip();
	}

	private void OnProgressBarOut(UIEvent e)
	{
		base.gameObject.GetComponent<TooltipZone>().HideTooltip();
	}
}
