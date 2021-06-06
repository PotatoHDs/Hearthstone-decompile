using System;
using System.Collections;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x0200064C RID: 1612
[RequireComponent(typeof(WidgetTemplate))]
[CustomEditClass]
public class RankedRewardInfoButton : MonoBehaviour
{
	// Token: 0x06005AE2 RID: 23266 RVA: 0x001DA3EF File Offset: 0x001D85EF
	private void Awake()
	{
		this.m_widget = base.GetComponent<WidgetTemplate>();
		this.m_widget.RegisterEventListener(new Widget.EventListenerDelegate(this.WidgetEventListener));
		this.m_tooltipZone = base.GetComponent<TooltipZone>();
	}

	// Token: 0x06005AE3 RID: 23267 RVA: 0x001DA420 File Offset: 0x001D8620
	private void OnDestroy()
	{
		this.DestroyRankedRewardsList();
	}

	// Token: 0x06005AE4 RID: 23268 RVA: 0x001DA428 File Offset: 0x001D8628
	public void Initialize(MedalInfoTranslator mit)
	{
		if (mit == null)
		{
			return;
		}
		this.m_medalInfo = mit;
		this.m_currentMedal = this.m_medalInfo.GetCurrentMedal(this.m_medalInfo.GetBestCurrentRankFormatType());
		bool isTooltipEnabled = false;
		bool hasEarnedCardBack = this.m_medalInfo.GetSeasonCardBackWinsRemaining() == 0;
		this.m_currentMedal.starLevel = this.m_currentMedal.bestStarLevel;
		RankedPlayDataModel rankedPlayDataModel = this.m_currentMedal.CreateDataModel(RankedMedal.DisplayMode.Chest, isTooltipEnabled, hasEarnedCardBack, null);
		this.m_widget.BindDataModel(rankedPlayDataModel, false);
		this.InitButtonText(rankedPlayDataModel);
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_REWARDS_VERSION_SEEN, out this.m_lastRewardsVersionSeen);
		this.UpdateRankedRewardList();
	}

	// Token: 0x06005AE5 RID: 23269 RVA: 0x001DA4C8 File Offset: 0x001D86C8
	public void Show()
	{
		base.StartCoroutine(this.ShowWhenReady());
	}

	// Token: 0x06005AE6 RID: 23270 RVA: 0x001DA420 File Offset: 0x001D8620
	public void OnClose()
	{
		this.DestroyRankedRewardsList();
	}

	// Token: 0x17000551 RID: 1361
	// (get) Token: 0x06005AE7 RID: 23271 RVA: 0x001DA4D7 File Offset: 0x001D86D7
	private bool IsReady
	{
		get
		{
			return this.m_widget != null && this.m_widget.IsReady && !this.m_widget.IsChangingStates;
		}
	}

	// Token: 0x06005AE8 RID: 23272 RVA: 0x001DA504 File Offset: 0x001D8704
	private IEnumerator ShowWhenReady()
	{
		while (!this.IsReady)
		{
			yield return null;
		}
		this.m_widget.Show();
		if (!this.HasSeenLatestRewardsVersion())
		{
			this.m_widget.TriggerEvent("SHOW_GLOW", default(Widget.TriggerEventParameters));
		}
		yield break;
	}

	// Token: 0x06005AE9 RID: 23273 RVA: 0x001DA513 File Offset: 0x001D8713
	private bool HasSeenLatestRewardsVersion()
	{
		return this.m_lastRewardsVersionSeen >= (long)this.m_currentMedal.LeagueConfig.RewardsVersion;
	}

	// Token: 0x06005AEA RID: 23274 RVA: 0x001DA534 File Offset: 0x001D8734
	private void WidgetEventListener(string eventName)
	{
		if (eventName.Equals("OnClickRewardQuestLogButton"))
		{
			base.StartCoroutine(this.ShowRankedRewardList());
			return;
		}
		if (eventName.Equals("RollOver"))
		{
			this.OnRollOver();
			return;
		}
		if (eventName.Equals("RollOut"))
		{
			this.OnRollOut();
		}
	}

	// Token: 0x06005AEB RID: 23275 RVA: 0x001DA583 File Offset: 0x001D8783
	private void WidgetEventListener_RewardsList(string eventName)
	{
		if (eventName.Equals("HIDE"))
		{
			this.HideRankedRewardsList();
		}
	}

	// Token: 0x06005AEC RID: 23276 RVA: 0x001DA598 File Offset: 0x001D8798
	private void HideRankedRewardsList()
	{
		UIContext.GetRoot().UnregisterPopup(this.m_rankedRewardListWidget.gameObject);
		this.m_isShowingRewardsList = false;
		this.m_buttonClickable.Active = true;
		if (QuestLog.Get() != null)
		{
			QuestLog.Get().SetCloseButtonActive(true);
		}
	}

	// Token: 0x06005AED RID: 23277 RVA: 0x001DA5E5 File Offset: 0x001D87E5
	private IEnumerator ShowRankedRewardList()
	{
		if (this.m_isShowingRewardsList)
		{
			yield break;
		}
		this.m_isShowingRewardsList = true;
		this.m_buttonClickable.Active = false;
		if (this.m_rankedRewardListWidget == null)
		{
			this.m_rankedRewardListWidget = WidgetInstance.Create(RankMgr.RANKED_REWARD_LIST_POPUP, false);
			this.m_rankedRewardListWidget.RegisterReadyListener(delegate(object _)
			{
				this.OnRankedRewardListPopupWidgetReady();
			}, null, true);
			this.m_rankedRewardListWidget.WillLoadSynchronously = true;
			this.m_rankedRewardListWidget.Initialize();
		}
		while (this.m_rankedRewardList == null || this.m_rankedRewardListWidget.IsChangingStates)
		{
			yield return null;
		}
		UIContext.GetRoot().RegisterPopup(this.m_rankedRewardListWidget.gameObject, UIContext.RenderCameraType.OrthographicUI, UIContext.BlurType.Standard);
		this.m_rankedRewardListWidget.Show();
		this.m_rankedRewardListWidget.TriggerEvent("SHOW", default(Widget.TriggerEventParameters));
		if (!this.HasSeenLatestRewardsVersion())
		{
			this.m_widget.TriggerEvent("HIDE_GLOW", default(Widget.TriggerEventParameters));
			this.m_lastRewardsVersionSeen = (long)this.m_currentMedal.LeagueConfig.RewardsVersion;
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_REWARDS_VERSION_SEEN, new long[]
			{
				this.m_lastRewardsVersionSeen
			}), null);
		}
		yield return new WaitForSeconds(0.25f);
		if (QuestLog.Get() != null)
		{
			QuestLog.Get().SetCloseButtonActive(false);
		}
		yield break;
	}

	// Token: 0x06005AEE RID: 23278 RVA: 0x001DA5F4 File Offset: 0x001D87F4
	private void OnRankedRewardListPopupWidgetReady()
	{
		OverlayUI.Get().AddGameObject(this.m_rankedRewardListWidget.gameObject, CanvasAnchor.CENTER, false, CanvasScaleMode.HEIGHT);
		this.m_rankedRewardListWidget.transform.localPosition = this.m_rewardListPos;
		float aspectRatioDependentValue = TransformUtil.GetAspectRatioDependentValue(this.m_rewardListScaleSmall, this.m_rewardListScaleWide, this.m_rewardListScaleExtraWide);
		this.m_rankedRewardListWidget.transform.localScale = Vector3.one * aspectRatioDependentValue * this.m_rewardListDeviceScale;
		this.m_rankedRewardListWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.WidgetEventListener_RewardsList));
		this.m_rankedRewardList = this.m_rankedRewardListWidget.GetComponentInChildren<RankedRewardList>();
		this.m_rankedRewardListWidget.Hide();
		this.UpdateRankedRewardList();
	}

	// Token: 0x06005AEF RID: 23279 RVA: 0x001DA6B0 File Offset: 0x001D88B0
	private void UpdateRankedRewardList()
	{
		if (this.m_rankedRewardList != null && this.m_medalInfo != null)
		{
			this.m_rankedRewardList.Initialize(this.m_medalInfo);
		}
	}

	// Token: 0x06005AF0 RID: 23280 RVA: 0x001DA6D9 File Offset: 0x001D88D9
	private void DestroyRankedRewardsList()
	{
		if (this.m_rankedRewardListWidget != null)
		{
			UnityEngine.Object.Destroy(this.m_rankedRewardListWidget.gameObject);
		}
		this.m_isShowingRewardsList = false;
	}

	// Token: 0x06005AF1 RID: 23281 RVA: 0x001DA700 File Offset: 0x001D8900
	private void InitButtonText(RankedPlayDataModel rankedPlayDataModel)
	{
		if (!rankedPlayDataModel.HasEarnedCardBack)
		{
			this.m_buttonText.Text = GameStrings.Format("GLUE_RANKED_REWARD_QUEST_LOG_CARDBACK_PROGRESS", new object[]
			{
				this.m_medalInfo.GetSeasonCardBackWinsRemaining()
			});
			return;
		}
		if (rankedPlayDataModel.IsNewPlayer)
		{
			int leagueId = this.m_currentMedal.leagueId;
			int num = rankedPlayDataModel.StarLevel - 1;
			num -= num % 5;
			num += 5;
			num++;
			if (num < RankMgr.Get().GetMaxStarLevel(leagueId))
			{
				LeagueRankDbfRecord leagueRankRecord = RankMgr.Get().GetLeagueRankRecord(leagueId, num);
				this.m_buttonText.Text = GameStrings.Format("GLUE_RANKED_REWARD_QUEST_LOG_LABEL_RANK_REWARD", new object[]
				{
					leagueRankRecord.MedalText.GetString(true)
				});
				return;
			}
			this.m_buttonText.Text = "";
			return;
		}
		else
		{
			int leagueId2 = this.m_currentMedal.leagueId;
			int maxStarLevel = RankMgr.Get().GetMaxStarLevel(leagueId2);
			int i = 1;
			LeagueRankDbfRecord leagueRankDbfRecord = null;
			bool flag = false;
			while (i < maxStarLevel)
			{
				LeagueRankDbfRecord leagueRankRecord2 = RankMgr.Get().GetLeagueRankRecord(leagueId2, i);
				if (leagueRankRecord2.RewardBagId != 0)
				{
					if (rankedPlayDataModel.StarLevel >= i)
					{
						flag = true;
						break;
					}
					leagueRankDbfRecord = leagueRankRecord2;
					break;
				}
				else
				{
					i++;
				}
			}
			if (!flag && leagueRankDbfRecord != null)
			{
				this.m_buttonText.Text = GameStrings.Format("GLUE_RANKED_REWARD_QUEST_LOG_LABEL_RANK_REQUIRED", new object[]
				{
					leagueRankDbfRecord.RankName.GetString(true)
				});
				return;
			}
			this.m_buttonText.Text = "";
			return;
		}
	}

	// Token: 0x06005AF2 RID: 23282 RVA: 0x001DA867 File Offset: 0x001D8A67
	private void OnRollOver()
	{
		this.m_tooltipZone.ShowLayerTooltip(GameStrings.Get("GLOBAL_PROGRESSION_RANKED_REWARDS_TOOLTIP_TITLE"), GameStrings.Get("GLOBAL_PROGRESSION_RANKED_REWARDS_TOOLTIP"), 0);
	}

	// Token: 0x06005AF3 RID: 23283 RVA: 0x001DA88A File Offset: 0x001D8A8A
	private void OnRollOut()
	{
		this.m_tooltipZone.HideTooltip();
	}

	// Token: 0x04004DA9 RID: 19881
	public Clickable m_buttonClickable;

	// Token: 0x04004DAA RID: 19882
	public UberText m_buttonText;

	// Token: 0x04004DAB RID: 19883
	[CustomEditField(Sections = "Reward List")]
	public Vector3_MobileOverride m_rewardListPos;

	// Token: 0x04004DAC RID: 19884
	[CustomEditField(Sections = "Reward List")]
	public Float_MobileOverride m_rewardListDeviceScale = new Float_MobileOverride(1f);

	// Token: 0x04004DAD RID: 19885
	[CustomEditField(Sections = "Reward List")]
	public float m_rewardListScaleSmall;

	// Token: 0x04004DAE RID: 19886
	[CustomEditField(Sections = "Reward List")]
	public float m_rewardListScaleWide;

	// Token: 0x04004DAF RID: 19887
	[CustomEditField(Sections = "Reward List")]
	public float m_rewardListScaleExtraWide;

	// Token: 0x04004DB0 RID: 19888
	private Widget m_widget;

	// Token: 0x04004DB1 RID: 19889
	private WidgetInstance m_rankedRewardListWidget;

	// Token: 0x04004DB2 RID: 19890
	private RankedRewardList m_rankedRewardList;

	// Token: 0x04004DB3 RID: 19891
	private MedalInfoTranslator m_medalInfo;

	// Token: 0x04004DB4 RID: 19892
	private TranslatedMedalInfo m_currentMedal;

	// Token: 0x04004DB5 RID: 19893
	private long m_lastRewardsVersionSeen;

	// Token: 0x04004DB6 RID: 19894
	private bool m_isShowingRewardsList;

	// Token: 0x04004DB7 RID: 19895
	private TooltipZone m_tooltipZone;
}
