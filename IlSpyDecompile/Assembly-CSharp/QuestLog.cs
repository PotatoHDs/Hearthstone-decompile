using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class QuestLog : UIBPopup
{
	public const int QUEST_LOG_MAX_COUNT = 3;

	public GameObject m_root;

	public UberText m_winsCountText;

	public UberText m_forgeRecordCountText;

	public UberText m_totalLevelsText;

	public Transform m_arenaMedalBone;

	public ArenaMedal m_arenaMedalPrefab;

	public PegUIElement m_offClickCatcher;

	public List<ClassProgressBar> m_classProgressBars;

	public List<ClassProgressInfo> m_classProgressInfos;

	public AsyncReference m_rankedMedalWidgetReference;

	public AsyncReference m_rankedRewardInfoButtonWidgetReference;

	public GameObject m_questTilePrefab;

	public List<Transform> m_questBones;

	public UberText m_noQuestText;

	public UIBButton m_closeButton;

	[CustomEditField(Sections = "Aspect Ratio Positioning")]
	public float m_extraWideScale = 150f;

	private List<QuestTile> m_currentQuests;

	private static QuestLog s_instance;

	private int m_justCanceledQuestID;

	private Widget m_rankedMedalWidget;

	private RankedMedal m_rankedMedal;

	private bool m_rankedPlayDataModelPending;

	private Widget m_rankedRewardInfoButtonWidget;

	private RankedRewardInfoButton m_rankedRewardInfoButton;

	private ArenaMedal m_arenaMedal;

	private Enum[] m_presencePrevStatus;

	protected override void Awake()
	{
		base.Awake();
		s_instance = this;
		AchieveManager.Get().RegisterAchievesUpdatedListener(OnAchievesUpdated);
		if (m_closeButton != null)
		{
			m_closeButton.AddEventListener(UIEventType.RELEASE, OnCloseButtonReleased);
		}
	}

	protected override void Start()
	{
		base.Start();
		m_rankedMedalWidgetReference.RegisterReadyListener<Widget>(OnRankedMedalWidgetReady);
		m_rankedRewardInfoButtonWidgetReference.RegisterReadyListener<Widget>(OnRankedRewardInfoButtonWidgetReady);
		for (int i = 0; i < m_classProgressInfos.Count; i++)
		{
			ClassProgressInfo classProgressInfo = m_classProgressInfos[i];
			TAG_CLASS @class = classProgressInfo.m_class;
			ClassProgressBar frame = classProgressInfo.m_frame;
			SceneUtils.SetLayer(frame, classProgressInfo.m_frame.gameObject.layer);
			frame.m_class = @class;
			m_classProgressBars.Add(frame);
		}
		m_offClickCatcher.AddEventListener(UIEventType.RELEASE, OnQuestLogCloseEvent);
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.OnMenuOpened += OnMenuOpened;
		}
	}

	private void OnDestroy()
	{
		if (ShownUIMgr.Get() != null)
		{
			ShownUIMgr.Get().ClearShownUI();
		}
		if (AchieveManager.Get() != null)
		{
			AchieveManager.Get().RemoveAchievesUpdatedListener(OnAchievesUpdated);
			AchieveManager.Get().RemoveQuestCanceledListener(OnQuestCanceled);
		}
		if (FullScreenFXMgr.Get() != null)
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(0.1f);
		}
		Navigation.RemoveHandler(OnNavigateBack);
		Hide(animate: false);
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar != null)
		{
			bnetBar.OnMenuOpened -= OnMenuOpened;
		}
		s_instance = null;
	}

	public static QuestLog Get()
	{
		return s_instance;
	}

	public void StartHidden()
	{
		DoHideAnimation(disableAnimation: true);
	}

	public void SetCloseButtonActive(bool active)
	{
		if (m_closeButton != null)
		{
			m_closeButton.gameObject.SetActive(active);
		}
	}

	public override void Show()
	{
		if (this == null)
		{
			Debug.Log("QuestLog: Attempting to Show after the QuestLog component has already been destroyed.");
			return;
		}
		m_presencePrevStatus = PresenceMgr.Get().GetStatus();
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.QUESTLOG);
		AchieveManager.Get().RegisterQuestCanceledListener(OnQuestCanceled);
		FullScreenFXMgr.Get().StartStandardBlurVignette(0.1f);
		Navigation.Push(OnNavigateBack);
		if ((bool)UniversalInputManager.UsePhoneUI && m_root != null)
		{
			float num = 1f;
			m_scaleMode = CanvasScaleMode.WIDTH;
			if (TransformUtil.IsExtraWideAspectRatio())
			{
				num = m_extraWideScale;
				m_scaleMode = CanvasScaleMode.HEIGHT;
			}
			OverlayUI.Get().AddGameObject(base.gameObject, CanvasAnchor.CENTER, destroyOnSceneLoad: false, m_scaleMode);
			m_root.transform.localScale = Vector3.one * num;
		}
		StartCoroutine(ShowWhenReady());
	}

	private IEnumerator ShowWhenReady()
	{
		while (m_rankedMedal == null || m_rankedRewardInfoButtonWidget == null)
		{
			yield return null;
		}
		UpdateData();
		while (m_rankedPlayDataModelPending || !m_rankedMedal.IsReady || m_rankedRewardInfoButtonWidget.IsChangingStates)
		{
			yield return null;
		}
		base.Show();
	}

	protected override void Hide(bool animate)
	{
		if (this == null)
		{
			Debug.Log("QuestLog: Attempting to Hide after the QuestLog component has already been destroyed.");
			return;
		}
		if (m_presencePrevStatus == null)
		{
			m_presencePrevStatus = new Enum[1] { Global.PresenceStatus.HUB };
		}
		PresenceMgr.Get().SetStatus(m_presencePrevStatus);
		if (ShownUIMgr.Get() != null)
		{
			ShownUIMgr.Get().ClearShownUI();
		}
		foreach (QuestTile currentQuest in m_currentQuests)
		{
			if (currentQuest != null)
			{
				currentQuest.OnClose();
			}
		}
		DoHideAnimation(!animate, delegate
		{
			if (AchieveManager.Get() != null)
			{
				AchieveManager.Get().RemoveQuestCanceledListener(OnQuestCanceled);
			}
			DeleteQuests();
			if (FullScreenFXMgr.Get() != null)
			{
				FullScreenFXMgr.Get().EndStandardBlurVignette(0.1f);
			}
			m_shown = false;
		});
	}

	private void DeleteQuests()
	{
		if (m_currentQuests == null || m_currentQuests.Count == 0)
		{
			return;
		}
		foreach (QuestTile currentQuest in m_currentQuests)
		{
			if (currentQuest != null)
			{
				UnityEngine.Object.Destroy(currentQuest.gameObject);
			}
		}
	}

	private void OnQuestLogCloseEvent(UIEvent e)
	{
		Navigation.GoBack();
	}

	private bool OnNavigateBack()
	{
		Hide(animate: true);
		if (m_rankedRewardInfoButton != null)
		{
			m_rankedRewardInfoButton.OnClose();
		}
		return true;
	}

	private void UpdateData()
	{
		UpdateClassProgress();
		UpdateActiveQuests();
		UpdateRankedMedal();
		UpdateRankedRewardInfo();
		UpdateBestArenaMedal();
		UpdateTotalWins();
	}

	private void UpdateTotalWins()
	{
		int num = 0;
		int num2 = 0;
		foreach (NetCache.PlayerRecord record in NetCache.Get().GetNetObject<NetCache.NetCachePlayerRecords>().Records)
		{
			if (record.Data == 0)
			{
				switch (record.RecordType)
				{
				case GameType.GT_ARENA:
					num2 += record.Wins;
					break;
				case GameType.GT_RANKED:
				case GameType.GT_CASUAL:
				case GameType.GT_TAVERNBRAWL:
					num += record.Wins;
					break;
				}
			}
		}
		m_winsCountText.Text = num.ToString();
		m_forgeRecordCountText.Text = num2.ToString();
	}

	private void UpdateBestArenaMedal()
	{
		NetCache.NetCacheProfileProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileProgress>();
		if (m_arenaMedal == null)
		{
			m_arenaMedal = (ArenaMedal)GameUtils.Instantiate(m_arenaMedalPrefab, m_arenaMedalBone.gameObject, withRotation: true);
			SceneUtils.SetLayer(m_arenaMedal, m_arenaMedalBone.gameObject.layer);
			m_arenaMedal.transform.localScale = Vector3.one;
		}
		if (netObject.LastForgeDate != 0L)
		{
			m_arenaMedal.gameObject.SetActive(value: true);
			m_arenaMedal.SetMedal(netObject.BestForgeWins);
		}
		else
		{
			m_arenaMedal.gameObject.SetActive(value: false);
		}
	}

	private void OnRankedMedalWidgetReady(Widget widget)
	{
		m_rankedMedalWidget = widget;
		m_rankedMedal = m_rankedMedalWidget.GetComponentInChildren<RankedMedal>();
	}

	private void OnRankedRewardInfoButtonWidgetReady(Widget widget)
	{
		m_rankedRewardInfoButtonWidget = widget;
		m_rankedRewardInfoButtonWidget.Hide();
		m_rankedRewardInfoButton = m_rankedRewardInfoButtonWidget.GetComponentInChildren<RankedRewardInfoButton>();
	}

	private void UpdateRankedMedal()
	{
		if (!(m_rankedMedalWidget == null) && !(m_rankedMedal == null))
		{
			m_rankedPlayDataModelPending = true;
			MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
			localPlayerMedalInfo.CreateDataModel(localPlayerMedalInfo.GetBestCurrentRankFormatType(), RankedMedal.DisplayMode.Default, isTooltipEnabled: true, hasEarnedCardBack: false, delegate(RankedPlayDataModel dm)
			{
				m_rankedMedal.BindRankedPlayDataModel(dm);
				m_rankedPlayDataModelPending = false;
			});
		}
	}

	private void UpdateRankedRewardInfo()
	{
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		if (m_rankedRewardInfoButton != null)
		{
			m_rankedRewardInfoButton.Initialize(localPlayerMedalInfo);
			m_rankedRewardInfoButton.Show();
		}
	}

	private void UpdateClassProgress()
	{
		if (m_classProgressBars.Count == 0)
		{
			return;
		}
		int num = 0;
		List<Achievement> achievesInGroup = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO, isComplete: true);
		List<Achievement> achievesInGroup2 = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.GOLDHERO, isComplete: true);
		NetCache.NetCacheHeroLevels netObject = NetCache.Get().GetNetObject<NetCache.NetCacheHeroLevels>();
		foreach (ClassProgressBar classProgress in m_classProgressBars)
		{
			Achievement achievement = achievesInGroup.Find((Achievement obj) => obj.ClassReward.HasValue && obj.ClassReward.Value == classProgress.m_class);
			Achievement achievement2 = achievesInGroup2.Find((Achievement obj) => obj.MyHeroClassRequirement.HasValue && obj.MyHeroClassRequirement.Value == classProgress.m_class);
			classProgress.SetPremium(achievement2 != null);
			if (achievement != null)
			{
				classProgress.m_classLockedGO.SetActive(value: false);
				NetCache.HeroLevel heroLevel = netObject.Levels.Find((NetCache.HeroLevel obj) => obj.Class == classProgress.m_class);
				classProgress.m_levelText.Text = heroLevel.CurrentLevel.Level.ToString();
				int nextRewardLevel = 0;
				RewardData nextHeroLevelReward = FixedRewardsMgr.Get().GetNextHeroLevelReward(heroLevel.Class, heroLevel.CurrentLevel.Level, out nextRewardLevel);
				if (nextHeroLevelReward != null)
				{
					classProgress.SetTooltipText(GameStrings.Format("GLOBAL_HERO_LEVEL_NEXT_REWARD_TITLE", nextRewardLevel), RewardUtils.GetRewardText(nextHeroLevelReward), heroLevel.CurrentLevel.Level.ToString());
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
		if (m_totalLevelsText != null)
		{
			m_totalLevelsText.Text = string.Format(GameStrings.Get("GLUE_QUEST_LOG_TOTAL_LEVELS"), num);
		}
	}

	private void UpdateActiveQuests()
	{
		List<Achievement> activeQuests = AchieveManager.Get().GetActiveQuests();
		m_currentQuests = new List<QuestTile>();
		for (int i = 0; i < activeQuests.Count; i++)
		{
			if (i < 3)
			{
				AddCurrentQuestTile(activeQuests[i], i);
			}
		}
		if (m_currentQuests.Count == 0)
		{
			m_noQuestText.gameObject.SetActive(value: true);
			if (AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.DAILY))
			{
				m_noQuestText.Text = GameStrings.Get("GLUE_QUEST_LOG_NO_QUESTS_DAILIES_UNLOCKED");
				if (!Options.Get().GetBool(Option.HAS_RUN_OUT_OF_QUESTS, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("QuestLog.UpdateActiveQuests:" + Option.HAS_RUN_OUT_OF_QUESTS))
				{
					NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(155.3f, 0f, 34.5f), GameStrings.Get("VO_INNKEEPER_OUT_OF_QUESTS"), "VO_INNKEEPER_OUT_OF_QUESTS.prefab:b0073c56bf38c664dab532ad92f3baf9");
					Options.Get().SetBool(Option.HAS_RUN_OUT_OF_QUESTS, val: true);
				}
			}
			else
			{
				m_noQuestText.Text = GameStrings.Get("GLUE_QUEST_LOG_NO_QUESTS");
			}
		}
		else
		{
			m_noQuestText.gameObject.SetActive(value: false);
		}
	}

	private void AddCurrentQuestTile(Achievement achieveQuest, int slot)
	{
		if (m_questTilePrefab == null || m_questBones == null || m_questBones[slot] == null || m_currentQuests == null)
		{
			Debug.Log("QuestLog: AddCurrentQuestTile failed, because a required object is null.");
			return;
		}
		GameObject obj = (GameObject)GameUtils.Instantiate(m_questTilePrefab, m_questBones[slot].gameObject, withRotation: true);
		SceneUtils.SetLayer(obj, m_questBones[slot].gameObject.layer);
		obj.transform.localScale = Vector3.one;
		QuestTile component = obj.GetComponent<QuestTile>();
		component.SetupTile(achieveQuest, QuestTile.FsmEvent.QuestShownInQuestLog);
		component.SetCanShowCancelButton(canShowCancel: true);
		m_currentQuests.Add(component);
	}

	private void OnQuestCanceled(int achieveID, bool canceled, object userData)
	{
		if (canceled)
		{
			m_justCanceledQuestID = achieveID;
		}
	}

	private void OnAchievesUpdated(List<Achievement> updatedAchieves, List<Achievement> completedAchieves, object userData)
	{
		if (m_justCanceledQuestID == 0)
		{
			return;
		}
		List<Achievement> activeQuests = AchieveManager.Get().GetActiveQuests(onlyNewlyActive: true);
		if (activeQuests.Count <= 0)
		{
			return;
		}
		if (activeQuests.Count > 1 && !Vars.Key("Quests.CanCancelManyTimes").GetBool(def: false) && !Vars.Key("Quests.CancelGivesManyNewQuests").GetBool(def: false))
		{
			Debug.LogError($"QuestLog.OnActiveAchievesUpdated(): expecting ONE new active quest after a quest cancel but received {activeQuests.Count}");
			Hide();
			return;
		}
		int justCanceledQuest = m_justCanceledQuestID;
		m_justCanceledQuestID = 0;
		QuestTile questTile = m_currentQuests.Find((QuestTile obj) => obj.GetQuestID() == justCanceledQuest);
		if (questTile == null)
		{
			Debug.LogError($"QuestLog.OnActiveAchievesUpdated(): could not find tile for just canceled quest (quest ID {justCanceledQuest})");
			Hide();
			return;
		}
		Log.Achievements.Print("Adding QuestLog tile for: {0}", activeQuests[0]);
		questTile.SetupTile(activeQuests[0], QuestTile.FsmEvent.QuestRerolled);
		for (int i = 1; i < activeQuests.Count; i++)
		{
			int count = m_currentQuests.Count;
			if (count >= m_questBones.Count)
			{
				break;
			}
			AddCurrentQuestTile(activeQuests[i], count);
		}
		foreach (QuestTile currentQuest in m_currentQuests)
		{
			currentQuest.UpdateCancelButtonVisibility();
		}
	}

	private void OnCloseButtonReleased(UIEvent e)
	{
		OnNavigateBack();
	}

	private void OnMenuOpened()
	{
		if (m_shown)
		{
			Hide(animate: false);
		}
	}
}
