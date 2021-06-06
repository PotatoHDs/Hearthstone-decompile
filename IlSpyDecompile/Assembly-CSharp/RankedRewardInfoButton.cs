using System.Collections;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
[CustomEditClass]
public class RankedRewardInfoButton : MonoBehaviour
{
	public Clickable m_buttonClickable;

	public UberText m_buttonText;

	[CustomEditField(Sections = "Reward List")]
	public Vector3_MobileOverride m_rewardListPos;

	[CustomEditField(Sections = "Reward List")]
	public Float_MobileOverride m_rewardListDeviceScale = new Float_MobileOverride(1f);

	[CustomEditField(Sections = "Reward List")]
	public float m_rewardListScaleSmall;

	[CustomEditField(Sections = "Reward List")]
	public float m_rewardListScaleWide;

	[CustomEditField(Sections = "Reward List")]
	public float m_rewardListScaleExtraWide;

	private Widget m_widget;

	private WidgetInstance m_rankedRewardListWidget;

	private RankedRewardList m_rankedRewardList;

	private MedalInfoTranslator m_medalInfo;

	private TranslatedMedalInfo m_currentMedal;

	private long m_lastRewardsVersionSeen;

	private bool m_isShowingRewardsList;

	private TooltipZone m_tooltipZone;

	private bool IsReady
	{
		get
		{
			if (m_widget != null && m_widget.IsReady)
			{
				return !m_widget.IsChangingStates;
			}
			return false;
		}
	}

	private void Awake()
	{
		m_widget = GetComponent<WidgetTemplate>();
		m_widget.RegisterEventListener(WidgetEventListener);
		m_tooltipZone = GetComponent<TooltipZone>();
	}

	private void OnDestroy()
	{
		DestroyRankedRewardsList();
	}

	public void Initialize(MedalInfoTranslator mit)
	{
		if (mit != null)
		{
			m_medalInfo = mit;
			m_currentMedal = m_medalInfo.GetCurrentMedal(m_medalInfo.GetBestCurrentRankFormatType());
			bool isTooltipEnabled = false;
			bool hasEarnedCardBack = m_medalInfo.GetSeasonCardBackWinsRemaining() == 0;
			m_currentMedal.starLevel = m_currentMedal.bestStarLevel;
			RankedPlayDataModel rankedPlayDataModel = m_currentMedal.CreateDataModel(RankedMedal.DisplayMode.Chest, isTooltipEnabled, hasEarnedCardBack);
			m_widget.BindDataModel(rankedPlayDataModel);
			InitButtonText(rankedPlayDataModel);
			GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_REWARDS_VERSION_SEEN, out m_lastRewardsVersionSeen);
			UpdateRankedRewardList();
		}
	}

	public void Show()
	{
		StartCoroutine(ShowWhenReady());
	}

	public void OnClose()
	{
		DestroyRankedRewardsList();
	}

	private IEnumerator ShowWhenReady()
	{
		while (!IsReady)
		{
			yield return null;
		}
		m_widget.Show();
		if (!HasSeenLatestRewardsVersion())
		{
			m_widget.TriggerEvent("SHOW_GLOW");
		}
	}

	private bool HasSeenLatestRewardsVersion()
	{
		return m_lastRewardsVersionSeen >= m_currentMedal.LeagueConfig.RewardsVersion;
	}

	private void WidgetEventListener(string eventName)
	{
		if (eventName.Equals("OnClickRewardQuestLogButton"))
		{
			StartCoroutine(ShowRankedRewardList());
		}
		else if (eventName.Equals("RollOver"))
		{
			OnRollOver();
		}
		else if (eventName.Equals("RollOut"))
		{
			OnRollOut();
		}
	}

	private void WidgetEventListener_RewardsList(string eventName)
	{
		if (eventName.Equals("HIDE"))
		{
			HideRankedRewardsList();
		}
	}

	private void HideRankedRewardsList()
	{
		UIContext.GetRoot().UnregisterPopup(m_rankedRewardListWidget.gameObject);
		m_isShowingRewardsList = false;
		m_buttonClickable.Active = true;
		if (QuestLog.Get() != null)
		{
			QuestLog.Get().SetCloseButtonActive(active: true);
		}
	}

	private IEnumerator ShowRankedRewardList()
	{
		if (m_isShowingRewardsList)
		{
			yield break;
		}
		m_isShowingRewardsList = true;
		m_buttonClickable.Active = false;
		if (m_rankedRewardListWidget == null)
		{
			m_rankedRewardListWidget = WidgetInstance.Create(RankMgr.RANKED_REWARD_LIST_POPUP);
			m_rankedRewardListWidget.RegisterReadyListener(delegate
			{
				OnRankedRewardListPopupWidgetReady();
			});
			m_rankedRewardListWidget.WillLoadSynchronously = true;
			m_rankedRewardListWidget.Initialize();
		}
		while (m_rankedRewardList == null || m_rankedRewardListWidget.IsChangingStates)
		{
			yield return null;
		}
		UIContext.GetRoot().RegisterPopup(m_rankedRewardListWidget.gameObject, UIContext.RenderCameraType.OrthographicUI);
		m_rankedRewardListWidget.Show();
		m_rankedRewardListWidget.TriggerEvent("SHOW");
		if (!HasSeenLatestRewardsVersion())
		{
			m_widget.TriggerEvent("HIDE_GLOW");
			m_lastRewardsVersionSeen = m_currentMedal.LeagueConfig.RewardsVersion;
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_REWARDS_VERSION_SEEN, m_lastRewardsVersionSeen));
		}
		yield return new WaitForSeconds(0.25f);
		if (QuestLog.Get() != null)
		{
			QuestLog.Get().SetCloseButtonActive(active: false);
		}
	}

	private void OnRankedRewardListPopupWidgetReady()
	{
		OverlayUI.Get().AddGameObject(m_rankedRewardListWidget.gameObject);
		m_rankedRewardListWidget.transform.localPosition = m_rewardListPos;
		float aspectRatioDependentValue = TransformUtil.GetAspectRatioDependentValue(m_rewardListScaleSmall, m_rewardListScaleWide, m_rewardListScaleExtraWide);
		m_rankedRewardListWidget.transform.localScale = Vector3.one * aspectRatioDependentValue * m_rewardListDeviceScale;
		m_rankedRewardListWidget.RegisterEventListener(WidgetEventListener_RewardsList);
		m_rankedRewardList = m_rankedRewardListWidget.GetComponentInChildren<RankedRewardList>();
		m_rankedRewardListWidget.Hide();
		UpdateRankedRewardList();
	}

	private void UpdateRankedRewardList()
	{
		if (m_rankedRewardList != null && m_medalInfo != null)
		{
			m_rankedRewardList.Initialize(m_medalInfo);
		}
	}

	private void DestroyRankedRewardsList()
	{
		if (m_rankedRewardListWidget != null)
		{
			Object.Destroy(m_rankedRewardListWidget.gameObject);
		}
		m_isShowingRewardsList = false;
	}

	private void InitButtonText(RankedPlayDataModel rankedPlayDataModel)
	{
		if (!rankedPlayDataModel.HasEarnedCardBack)
		{
			m_buttonText.Text = GameStrings.Format("GLUE_RANKED_REWARD_QUEST_LOG_CARDBACK_PROGRESS", m_medalInfo.GetSeasonCardBackWinsRemaining());
			return;
		}
		if (rankedPlayDataModel.IsNewPlayer)
		{
			int leagueId = m_currentMedal.leagueId;
			int num = rankedPlayDataModel.StarLevel - 1;
			num -= num % 5;
			num += 5;
			num++;
			if (num < RankMgr.Get().GetMaxStarLevel(leagueId))
			{
				LeagueRankDbfRecord leagueRankRecord = RankMgr.Get().GetLeagueRankRecord(leagueId, num);
				m_buttonText.Text = GameStrings.Format("GLUE_RANKED_REWARD_QUEST_LOG_LABEL_RANK_REWARD", leagueRankRecord.MedalText.GetString());
			}
			else
			{
				m_buttonText.Text = "";
			}
			return;
		}
		int leagueId2 = m_currentMedal.leagueId;
		int maxStarLevel = RankMgr.Get().GetMaxStarLevel(leagueId2);
		int i = 1;
		LeagueRankDbfRecord leagueRankDbfRecord = null;
		bool flag = false;
		for (; i < maxStarLevel; i++)
		{
			LeagueRankDbfRecord leagueRankRecord2 = RankMgr.Get().GetLeagueRankRecord(leagueId2, i);
			if (leagueRankRecord2.RewardBagId != 0)
			{
				if (rankedPlayDataModel.StarLevel >= i)
				{
					flag = true;
				}
				else
				{
					leagueRankDbfRecord = leagueRankRecord2;
				}
				break;
			}
		}
		if (!flag && leagueRankDbfRecord != null)
		{
			m_buttonText.Text = GameStrings.Format("GLUE_RANKED_REWARD_QUEST_LOG_LABEL_RANK_REQUIRED", leagueRankDbfRecord.RankName.GetString());
		}
		else
		{
			m_buttonText.Text = "";
		}
	}

	private void OnRollOver()
	{
		m_tooltipZone.ShowLayerTooltip(GameStrings.Get("GLOBAL_PROGRESSION_RANKED_REWARDS_TOOLTIP_TITLE"), GameStrings.Get("GLOBAL_PROGRESSION_RANKED_REWARDS_TOOLTIP"));
	}

	private void OnRollOut()
	{
		m_tooltipZone.HideTooltip();
	}
}
