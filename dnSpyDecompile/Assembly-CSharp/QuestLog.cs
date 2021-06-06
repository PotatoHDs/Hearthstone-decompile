using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x02000634 RID: 1588
[CustomEditClass]
public class QuestLog : UIBPopup
{
	// Token: 0x06005966 RID: 22886 RVA: 0x001D2144 File Offset: 0x001D0344
	protected override void Awake()
	{
		base.Awake();
		QuestLog.s_instance = this;
		AchieveManager.Get().RegisterAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(this.OnAchievesUpdated), null);
		if (this.m_closeButton != null)
		{
			this.m_closeButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCloseButtonReleased));
		}
	}

	// Token: 0x06005967 RID: 22887 RVA: 0x001D219C File Offset: 0x001D039C
	protected override void Start()
	{
		base.Start();
		this.m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRankedMedalWidgetReady));
		this.m_rankedRewardInfoButtonWidgetReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnRankedRewardInfoButtonWidgetReady));
		for (int i = 0; i < this.m_classProgressInfos.Count; i++)
		{
			ClassProgressInfo classProgressInfo = this.m_classProgressInfos[i];
			TAG_CLASS @class = classProgressInfo.m_class;
			ClassProgressBar frame = classProgressInfo.m_frame;
			SceneUtils.SetLayer(frame, classProgressInfo.m_frame.gameObject.layer);
			frame.m_class = @class;
			this.m_classProgressBars.Add(frame);
		}
		this.m_offClickCatcher.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnQuestLogCloseEvent));
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.OnMenuOpened += this.OnMenuOpened;
		}
	}

	// Token: 0x06005968 RID: 22888 RVA: 0x001D2278 File Offset: 0x001D0478
	private void OnDestroy()
	{
		if (ShownUIMgr.Get() != null)
		{
			ShownUIMgr.Get().ClearShownUI();
		}
		if (AchieveManager.Get() != null)
		{
			AchieveManager.Get().RemoveAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(this.OnAchievesUpdated));
			AchieveManager.Get().RemoveQuestCanceledListener(new AchieveManager.AchieveCanceledCallback(this.OnQuestCanceled));
		}
		if (FullScreenFXMgr.Get() != null)
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(0.1f, null);
		}
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		this.Hide(false);
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.OnMenuOpened -= this.OnMenuOpened;
		}
		QuestLog.s_instance = null;
	}

	// Token: 0x06005969 RID: 22889 RVA: 0x001D2322 File Offset: 0x001D0522
	public static QuestLog Get()
	{
		return QuestLog.s_instance;
	}

	// Token: 0x0600596A RID: 22890 RVA: 0x001D2329 File Offset: 0x001D0529
	public void StartHidden()
	{
		base.DoHideAnimation(true, null);
	}

	// Token: 0x0600596B RID: 22891 RVA: 0x001D2333 File Offset: 0x001D0533
	public void SetCloseButtonActive(bool active)
	{
		if (this.m_closeButton != null)
		{
			this.m_closeButton.gameObject.SetActive(active);
		}
	}

	// Token: 0x0600596C RID: 22892 RVA: 0x001D2354 File Offset: 0x001D0554
	public override void Show()
	{
		if (this == null)
		{
			Debug.Log("QuestLog: Attempting to Show after the QuestLog component has already been destroyed.");
			return;
		}
		this.m_presencePrevStatus = PresenceMgr.Get().GetStatus();
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.QUESTLOG
		});
		AchieveManager.Get().RegisterQuestCanceledListener(new AchieveManager.AchieveCanceledCallback(this.OnQuestCanceled));
		FullScreenFXMgr.Get().StartStandardBlurVignette(0.1f);
		Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		if (UniversalInputManager.UsePhoneUI && this.m_root != null)
		{
			float d = 1f;
			this.m_scaleMode = CanvasScaleMode.WIDTH;
			if (TransformUtil.IsExtraWideAspectRatio())
			{
				d = this.m_extraWideScale;
				this.m_scaleMode = CanvasScaleMode.HEIGHT;
			}
			OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, false, this.m_scaleMode);
			this.m_root.transform.localScale = Vector3.one * d;
		}
		base.StartCoroutine(this.ShowWhenReady());
	}

	// Token: 0x0600596D RID: 22893 RVA: 0x001D2452 File Offset: 0x001D0652
	private IEnumerator ShowWhenReady()
	{
		while (this.m_rankedMedal == null || this.m_rankedRewardInfoButtonWidget == null)
		{
			yield return null;
		}
		this.UpdateData();
		while (this.m_rankedPlayDataModelPending || !this.m_rankedMedal.IsReady || this.m_rankedRewardInfoButtonWidget.IsChangingStates)
		{
			yield return null;
		}
		base.Show();
		yield break;
	}

	// Token: 0x0600596E RID: 22894 RVA: 0x001D2464 File Offset: 0x001D0664
	protected override void Hide(bool animate)
	{
		if (this == null)
		{
			Debug.Log("QuestLog: Attempting to Hide after the QuestLog component has already been destroyed.");
			return;
		}
		if (this.m_presencePrevStatus == null)
		{
			this.m_presencePrevStatus = new Enum[]
			{
				Global.PresenceStatus.HUB
			};
		}
		PresenceMgr.Get().SetStatus(this.m_presencePrevStatus);
		if (ShownUIMgr.Get() != null)
		{
			ShownUIMgr.Get().ClearShownUI();
		}
		foreach (QuestTile questTile in this.m_currentQuests)
		{
			if (questTile != null)
			{
				questTile.OnClose();
			}
		}
		base.DoHideAnimation(!animate, delegate()
		{
			if (AchieveManager.Get() != null)
			{
				AchieveManager.Get().RemoveQuestCanceledListener(new AchieveManager.AchieveCanceledCallback(this.OnQuestCanceled));
			}
			this.DeleteQuests();
			if (FullScreenFXMgr.Get() != null)
			{
				FullScreenFXMgr.Get().EndStandardBlurVignette(0.1f, null);
			}
			this.m_shown = false;
		});
	}

	// Token: 0x0600596F RID: 22895 RVA: 0x001D2528 File Offset: 0x001D0728
	private void DeleteQuests()
	{
		if (this.m_currentQuests == null || this.m_currentQuests.Count == 0)
		{
			return;
		}
		foreach (QuestTile questTile in this.m_currentQuests)
		{
			if (questTile != null)
			{
				UnityEngine.Object.Destroy(questTile.gameObject);
			}
		}
	}

	// Token: 0x06005970 RID: 22896 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnQuestLogCloseEvent(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x06005971 RID: 22897 RVA: 0x001D25A0 File Offset: 0x001D07A0
	private bool OnNavigateBack()
	{
		this.Hide(true);
		if (this.m_rankedRewardInfoButton != null)
		{
			this.m_rankedRewardInfoButton.OnClose();
		}
		return true;
	}

	// Token: 0x06005972 RID: 22898 RVA: 0x001D25C3 File Offset: 0x001D07C3
	private void UpdateData()
	{
		this.UpdateClassProgress();
		this.UpdateActiveQuests();
		this.UpdateRankedMedal();
		this.UpdateRankedRewardInfo();
		this.UpdateBestArenaMedal();
		this.UpdateTotalWins();
	}

	// Token: 0x06005973 RID: 22899 RVA: 0x001D25EC File Offset: 0x001D07EC
	private void UpdateTotalWins()
	{
		int num = 0;
		int num2 = 0;
		foreach (NetCache.PlayerRecord playerRecord in NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>().Records)
		{
			if (playerRecord.Data == 0)
			{
				GameType recordType = playerRecord.RecordType;
				if (recordType != GameType.GT_ARENA)
				{
					if (recordType - GameType.GT_RANKED <= 1 || recordType == GameType.GT_TAVERNBRAWL)
					{
						num += playerRecord.Wins;
					}
				}
				else
				{
					num2 += playerRecord.Wins;
				}
			}
		}
		this.m_winsCountText.Text = num.ToString();
		this.m_forgeRecordCountText.Text = num2.ToString();
	}

	// Token: 0x06005974 RID: 22900 RVA: 0x001D26A4 File Offset: 0x001D08A4
	private void UpdateBestArenaMedal()
	{
		NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
		if (this.m_arenaMedal == null)
		{
			this.m_arenaMedal = (ArenaMedal)GameUtils.Instantiate(this.m_arenaMedalPrefab, this.m_arenaMedalBone.gameObject, true);
			SceneUtils.SetLayer(this.m_arenaMedal, this.m_arenaMedalBone.gameObject.layer);
			this.m_arenaMedal.transform.localScale = Vector3.one;
		}
		if (netObject.LastForgeDate != 0L)
		{
			this.m_arenaMedal.gameObject.SetActive(true);
			this.m_arenaMedal.SetMedal(netObject.BestForgeWins);
			return;
		}
		this.m_arenaMedal.gameObject.SetActive(false);
	}

	// Token: 0x06005975 RID: 22901 RVA: 0x001D2758 File Offset: 0x001D0958
	private void OnRankedMedalWidgetReady(Widget widget)
	{
		this.m_rankedMedalWidget = widget;
		this.m_rankedMedal = this.m_rankedMedalWidget.GetComponentInChildren<RankedMedal>();
	}

	// Token: 0x06005976 RID: 22902 RVA: 0x001D2772 File Offset: 0x001D0972
	private void OnRankedRewardInfoButtonWidgetReady(Widget widget)
	{
		this.m_rankedRewardInfoButtonWidget = widget;
		this.m_rankedRewardInfoButtonWidget.Hide();
		this.m_rankedRewardInfoButton = this.m_rankedRewardInfoButtonWidget.GetComponentInChildren<RankedRewardInfoButton>();
	}

	// Token: 0x06005977 RID: 22903 RVA: 0x001D2798 File Offset: 0x001D0998
	private void UpdateRankedMedal()
	{
		if (this.m_rankedMedalWidget == null || this.m_rankedMedal == null)
		{
			return;
		}
		this.m_rankedPlayDataModelPending = true;
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		localPlayerMedalInfo.CreateDataModel(localPlayerMedalInfo.GetBestCurrentRankFormatType(), RankedMedal.DisplayMode.Default, true, false, delegate(RankedPlayDataModel dm)
		{
			this.m_rankedMedal.BindRankedPlayDataModel(dm);
			this.m_rankedPlayDataModelPending = false;
		});
	}

	// Token: 0x06005978 RID: 22904 RVA: 0x001D27F0 File Offset: 0x001D09F0
	private void UpdateRankedRewardInfo()
	{
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		if (this.m_rankedRewardInfoButton != null)
		{
			this.m_rankedRewardInfoButton.Initialize(localPlayerMedalInfo);
			this.m_rankedRewardInfoButton.Show();
		}
	}

	// Token: 0x06005979 RID: 22905 RVA: 0x001D2830 File Offset: 0x001D0A30
	private void UpdateClassProgress()
	{
		if (this.m_classProgressBars.Count == 0)
		{
			return;
		}
		int num = 0;
		List<global::Achievement> achievesInGroup = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO, true);
		List<global::Achievement> achievesInGroup2 = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.GOLDHERO, true);
		NetCache.NetCacheHeroLevels netObject = NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>();
		using (List<ClassProgressBar>.Enumerator enumerator = this.m_classProgressBars.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ClassProgressBar classProgress = enumerator.Current;
				bool flag = achievesInGroup.Find((global::Achievement obj) => obj.ClassReward != null && obj.ClassReward.Value == classProgress.m_class) != null;
				global::Achievement achievement = achievesInGroup2.Find((global::Achievement obj) => obj.MyHeroClassRequirement != null && obj.MyHeroClassRequirement.Value == classProgress.m_class);
				classProgress.SetPremium(achievement != null);
				if (flag)
				{
					classProgress.m_classLockedGO.SetActive(false);
					NetCache.HeroLevel heroLevel = netObject.Levels.Find((NetCache.HeroLevel obj) => obj.Class == classProgress.m_class);
					classProgress.m_levelText.Text = heroLevel.CurrentLevel.Level.ToString();
					int num2 = 0;
					RewardData nextHeroLevelReward = FixedRewardsMgr.Get().GetNextHeroLevelReward(heroLevel.Class, heroLevel.CurrentLevel.Level, out num2);
					if (nextHeroLevelReward != null)
					{
						classProgress.SetTooltipText(GameStrings.Format("GLOBAL_HERO_LEVEL_NEXT_REWARD_TITLE", new object[]
						{
							num2
						}), RewardUtils.GetRewardText(nextHeroLevelReward), heroLevel.CurrentLevel.Level.ToString());
					}
					num += heroLevel.CurrentLevel.Level;
					if (heroLevel.CurrentLevel.IsMaxLevel())
					{
						classProgress.m_progressBar.SetProgressBar(1f);
					}
					else
					{
						classProgress.m_progressBar.SetProgressBar((float)heroLevel.CurrentLevel.XP / (float)heroLevel.CurrentLevel.MaxXP);
					}
				}
				else
				{
					classProgress.m_levelText.Text = "0";
					classProgress.Lock();
				}
			}
		}
		if (this.m_totalLevelsText != null)
		{
			this.m_totalLevelsText.Text = string.Format(GameStrings.Get("GLUE_QUEST_LOG_TOTAL_LEVELS"), num);
		}
	}

	// Token: 0x0600597A RID: 22906 RVA: 0x001D2A80 File Offset: 0x001D0C80
	private void UpdateActiveQuests()
	{
		List<global::Achievement> activeQuests = AchieveManager.Get().GetActiveQuests(false);
		this.m_currentQuests = new List<QuestTile>();
		for (int i = 0; i < activeQuests.Count; i++)
		{
			if (i < 3)
			{
				this.AddCurrentQuestTile(activeQuests[i], i);
			}
		}
		if (this.m_currentQuests.Count == 0)
		{
			this.m_noQuestText.gameObject.SetActive(true);
			if (!AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.DAILY))
			{
				this.m_noQuestText.Text = GameStrings.Get("GLUE_QUEST_LOG_NO_QUESTS");
				return;
			}
			this.m_noQuestText.Text = GameStrings.Get("GLUE_QUEST_LOG_NO_QUESTS_DAILIES_UNLOCKED");
			if (!Options.Get().GetBool(Option.HAS_RUN_OUT_OF_QUESTS, false) && UserAttentionManager.CanShowAttentionGrabber("QuestLog.UpdateActiveQuests:" + Option.HAS_RUN_OUT_OF_QUESTS))
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, 0f, 34.5f), GameStrings.Get("VO_INNKEEPER_OUT_OF_QUESTS"), "VO_INNKEEPER_OUT_OF_QUESTS.prefab:b0073c56bf38c664dab532ad92f3baf9", 0f, null, false);
				Options.Get().SetBool(Option.HAS_RUN_OUT_OF_QUESTS, true);
				return;
			}
		}
		else
		{
			this.m_noQuestText.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600597B RID: 22907 RVA: 0x001D2BAC File Offset: 0x001D0DAC
	private void AddCurrentQuestTile(global::Achievement achieveQuest, int slot)
	{
		if (this.m_questTilePrefab == null || this.m_questBones == null || this.m_questBones[slot] == null || this.m_currentQuests == null)
		{
			Debug.Log("QuestLog: AddCurrentQuestTile failed, because a required object is null.");
			return;
		}
		GameObject gameObject = (GameObject)GameUtils.Instantiate(this.m_questTilePrefab, this.m_questBones[slot].gameObject, true);
		SceneUtils.SetLayer(gameObject, this.m_questBones[slot].gameObject.layer, null);
		gameObject.transform.localScale = Vector3.one;
		QuestTile component = gameObject.GetComponent<QuestTile>();
		component.SetupTile(achieveQuest, QuestTile.FsmEvent.QuestShownInQuestLog);
		component.SetCanShowCancelButton(true);
		this.m_currentQuests.Add(component);
	}

	// Token: 0x0600597C RID: 22908 RVA: 0x001D2C6E File Offset: 0x001D0E6E
	private void OnQuestCanceled(int achieveID, bool canceled, object userData)
	{
		if (!canceled)
		{
			return;
		}
		this.m_justCanceledQuestID = achieveID;
	}

	// Token: 0x0600597D RID: 22909 RVA: 0x001D2C7C File Offset: 0x001D0E7C
	private void OnAchievesUpdated(List<global::Achievement> updatedAchieves, List<global::Achievement> completedAchieves, object userData)
	{
		if (this.m_justCanceledQuestID == 0)
		{
			return;
		}
		List<global::Achievement> activeQuests = AchieveManager.Get().GetActiveQuests(true);
		if (activeQuests.Count <= 0)
		{
			return;
		}
		if (activeQuests.Count > 1 && !Vars.Key("Quests.CanCancelManyTimes").GetBool(false) && !Vars.Key("Quests.CancelGivesManyNewQuests").GetBool(false))
		{
			Debug.LogError(string.Format("QuestLog.OnActiveAchievesUpdated(): expecting ONE new active quest after a quest cancel but received {0}", activeQuests.Count));
			this.Hide();
			return;
		}
		int justCanceledQuest = this.m_justCanceledQuestID;
		this.m_justCanceledQuestID = 0;
		QuestTile questTile = this.m_currentQuests.Find((QuestTile obj) => obj.GetQuestID() == justCanceledQuest);
		if (questTile == null)
		{
			Debug.LogError(string.Format("QuestLog.OnActiveAchievesUpdated(): could not find tile for just canceled quest (quest ID {0})", justCanceledQuest));
			this.Hide();
			return;
		}
		Log.Achievements.Print("Adding QuestLog tile for: {0}", new object[]
		{
			activeQuests[0]
		});
		questTile.SetupTile(activeQuests[0], QuestTile.FsmEvent.QuestRerolled);
		for (int i = 1; i < activeQuests.Count; i++)
		{
			int count = this.m_currentQuests.Count;
			if (count >= this.m_questBones.Count)
			{
				break;
			}
			this.AddCurrentQuestTile(activeQuests[i], count);
		}
		foreach (QuestTile questTile2 in this.m_currentQuests)
		{
			questTile2.UpdateCancelButtonVisibility();
		}
	}

	// Token: 0x0600597E RID: 22910 RVA: 0x001D2E00 File Offset: 0x001D1000
	private void OnCloseButtonReleased(UIEvent e)
	{
		this.OnNavigateBack();
	}

	// Token: 0x0600597F RID: 22911 RVA: 0x001D2E09 File Offset: 0x001D1009
	private void OnMenuOpened()
	{
		if (this.m_shown)
		{
			this.Hide(false);
		}
	}

	// Token: 0x04004C70 RID: 19568
	public const int QUEST_LOG_MAX_COUNT = 3;

	// Token: 0x04004C71 RID: 19569
	public GameObject m_root;

	// Token: 0x04004C72 RID: 19570
	public UberText m_winsCountText;

	// Token: 0x04004C73 RID: 19571
	public UberText m_forgeRecordCountText;

	// Token: 0x04004C74 RID: 19572
	public UberText m_totalLevelsText;

	// Token: 0x04004C75 RID: 19573
	public Transform m_arenaMedalBone;

	// Token: 0x04004C76 RID: 19574
	public ArenaMedal m_arenaMedalPrefab;

	// Token: 0x04004C77 RID: 19575
	public PegUIElement m_offClickCatcher;

	// Token: 0x04004C78 RID: 19576
	public List<ClassProgressBar> m_classProgressBars;

	// Token: 0x04004C79 RID: 19577
	public List<ClassProgressInfo> m_classProgressInfos;

	// Token: 0x04004C7A RID: 19578
	public AsyncReference m_rankedMedalWidgetReference;

	// Token: 0x04004C7B RID: 19579
	public AsyncReference m_rankedRewardInfoButtonWidgetReference;

	// Token: 0x04004C7C RID: 19580
	public GameObject m_questTilePrefab;

	// Token: 0x04004C7D RID: 19581
	public List<Transform> m_questBones;

	// Token: 0x04004C7E RID: 19582
	public UberText m_noQuestText;

	// Token: 0x04004C7F RID: 19583
	public UIBButton m_closeButton;

	// Token: 0x04004C80 RID: 19584
	[CustomEditField(Sections = "Aspect Ratio Positioning")]
	public float m_extraWideScale = 150f;

	// Token: 0x04004C81 RID: 19585
	private List<QuestTile> m_currentQuests;

	// Token: 0x04004C82 RID: 19586
	private static QuestLog s_instance;

	// Token: 0x04004C83 RID: 19587
	private int m_justCanceledQuestID;

	// Token: 0x04004C84 RID: 19588
	private Widget m_rankedMedalWidget;

	// Token: 0x04004C85 RID: 19589
	private RankedMedal m_rankedMedal;

	// Token: 0x04004C86 RID: 19590
	private bool m_rankedPlayDataModelPending;

	// Token: 0x04004C87 RID: 19591
	private Widget m_rankedRewardInfoButtonWidget;

	// Token: 0x04004C88 RID: 19592
	private RankedRewardInfoButton m_rankedRewardInfoButton;

	// Token: 0x04004C89 RID: 19593
	private ArenaMedal m_arenaMedal;

	// Token: 0x04004C8A RID: 19594
	private Enum[] m_presencePrevStatus;
}
