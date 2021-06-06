using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using PegasusShared;
using UnityEngine;

// Token: 0x0200001A RID: 26
[CustomEditClass]
public class AdventureBonusChallengeDisplay : MonoBehaviour
{
	// Token: 0x060000D2 RID: 210 RVA: 0x00004CEC File Offset: 0x00002EEC
	private void Awake()
	{
		Navigation.PushUnique(new Navigation.NavigateBackHandler(AdventureBonusChallengeDisplay.OnNavigateBack));
		this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnBackButton));
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnPlayButton));
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((ScenarioDbfRecord r) => r.AdventureId == (int)AdventureConfig.Get().GetSelectedAdventure() && r.ModeId == (int)AdventureConfig.Get().GetSelectedMode());
		if (record != null)
		{
			AdventureConfig.Get().SetMission((ScenarioDbId)record.ID, true);
			this.m_headerString = record.Name;
			this.m_footerString = ((UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(record.ShortDescription)) ? record.ShortDescription : record.Description);
			this.m_wingId = (WingDbId)record.WingId;
		}
		this.SetUpUberText();
		this.InitializeRewardDisplay();
		AdventureSubScene component = base.GetComponent<AdventureSubScene>();
		if (component != null)
		{
			component.AddSubSceneTransitionFinishedListener(new AdventureSubScene.SubSceneTransitionFinished(this.OnSubSceneTransitionComplete));
			component.SetIsLoaded(true);
		}
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00004E04 File Offset: 0x00003004
	private void SetUpUberText()
	{
		if (this.m_bonusChallengeLabel != null)
		{
			this.m_bonusChallengeLabel.Text = GameStrings.Get("GLUE_ADVENTURE_BONUS_CHALLENGE_LABEL");
		}
		if (this.m_headerText != null)
		{
			this.m_headerText.Text = this.m_headerString;
		}
		if (this.m_footerText != null)
		{
			this.m_footerText.Text = this.m_footerString;
		}
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00004E74 File Offset: 0x00003074
	private void OnPlayButton(UIEvent e)
	{
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, (int)AdventureConfig.Get().GetMission(), 0, 0L, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00004EA7 File Offset: 0x000030A7
	private static bool OnNavigateBack()
	{
		AdventureConfig.Get().SubSceneGoBack(true);
		return true;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnBackButton(UIEvent e)
	{
		Navigation.GoBack();
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnSubSceneTransitionComplete()
	{
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00004EC0 File Offset: 0x000030C0
	private void InitializeRewardDisplay()
	{
		int mission = (int)AdventureConfig.Get().GetMission();
		if (this.GetFirstRewardFromScenario(mission) == null)
		{
			return;
		}
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && AchieveManager.Get().GetNewCompletedAchievesToShow().Count > 0)
		{
			this.m_playButton.SetEnabled(false, false);
			LoadingScreen.Get().RegisterFinishedTransitionListener(new LoadingScreen.FinishedTransitionCallback(this.OnTransitionFromGameplayFinished));
		}
		if (AdventureProgressMgr.Get().HasDefeatedScenario(mission))
		{
			this.m_rewardChest.GetComponent<Renderer>().SetMaterial(this.m_chestOpenMaterial);
			this.m_rewardChest.SetEnabled(false, false);
			return;
		}
		base.StartCoroutine(this.PlayEntryQuoteWithTiming());
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)AdventureConfig.Get().GetSelectedAdventure(), (int)AdventureConfig.Get().GetSelectedMode());
		if (adventureDataRecord != null)
		{
			this.m_rewardsText.Text = adventureDataRecord.RewardsDescription;
		}
		if (this.m_rewardOffClickCatcher != null)
		{
			this.m_rewardChest.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.ShowNonSessionRewardPreview));
			this.m_rewardOffClickCatcher.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.HideNonSessionRewardPreview));
		}
		else
		{
			this.m_rewardChest.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.ShowNonSessionRewardPreview));
			this.m_rewardChest.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.HideNonSessionRewardPreview));
		}
		this.m_rewardsScale = this.m_rewardsPreview.transform.localScale;
		this.m_rewardsPreview.transform.localScale = Vector3.one * 0.01f;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00005040 File Offset: 0x00003240
	private void OnTransitionFromGameplayFinished(bool cutoff, object userData)
	{
		PopupDisplayManager.Get().ShowAnyOutstandingPopups(delegate()
		{
			Navigation.GoBack();
		});
		int mission = (int)AdventureConfig.Get().GetMission();
		if (AdventureProgressMgr.Get().HasDefeatedScenario(mission))
		{
			base.StartCoroutine(this.PlayCompleteQuoteWithTiming());
		}
		LoadingScreen.Get().UnregisterFinishedTransitionListener(new LoadingScreen.FinishedTransitionCallback(this.OnTransitionFromGameplayFinished));
	}

	// Token: 0x060000DA RID: 218 RVA: 0x000050B4 File Offset: 0x000032B4
	private RewardData GetFirstRewardFromScenario(int scenarioDbId)
	{
		HashSet<Achieve.RewardTiming> rewardTimings = new HashSet<Achieve.RewardTiming>
		{
			Achieve.RewardTiming.ADVENTURE_CHEST
		};
		List<RewardData> rewardsForDefeatingScenario = AdventureProgressMgr.Get().GetRewardsForDefeatingScenario((int)AdventureConfig.Get().GetMission(), rewardTimings);
		if (rewardsForDefeatingScenario == null || rewardsForDefeatingScenario.Count == 0)
		{
			return null;
		}
		return rewardsForDefeatingScenario[0];
	}

	// Token: 0x060000DB RID: 219 RVA: 0x000050FC File Offset: 0x000032FC
	private void ShowNonSessionRewardPreview(UIEvent e)
	{
		if (AdventureConfig.Get().GetMission() == ScenarioDbId.INVALID)
		{
			return;
		}
		RewardData firstRewardFromScenario = this.GetFirstRewardFromScenario((int)AdventureConfig.Get().GetMission());
		if (firstRewardFromScenario == null)
		{
			return;
		}
		Reward.Type rewardType = firstRewardFromScenario.RewardType;
		if (rewardType == Reward.Type.CARD_BACK)
		{
			if (this.m_rewardObject == null)
			{
				int cardBackID = (firstRewardFromScenario as CardBackRewardData).CardBackID;
				CardBackManager.LoadCardBackData loadCardBackData = CardBackManager.Get().LoadCardBackByIndex(cardBackID, false, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", true);
				if (loadCardBackData == null)
				{
					Debug.LogErrorFormat("AdventureBonusChallengeDisplay.ShowReward() - Could not load cardback ID {0}!", new object[]
					{
						cardBackID
					});
					return;
				}
				this.m_rewardObject = loadCardBackData.m_GameObject;
				GameUtils.SetParent(this.m_rewardObject, this.m_rewardContainer, false);
			}
			this.m_rewardsPreview.SetActive(true);
			iTween.Stop(this.m_rewardsPreview);
			iTween.ScaleTo(this.m_rewardsPreview, iTween.Hash(new object[]
			{
				"scale",
				this.m_rewardsScale,
				"time",
				0.15f
			}));
			return;
		}
		Debug.LogErrorFormat("Adventure Bonus Challenge reward type currently not supported! Add type {0} to AdventureBonusChallengeDisplay.ShowReward().", new object[]
		{
			firstRewardFromScenario.RewardType
		});
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00005218 File Offset: 0x00003418
	private void HideNonSessionRewardPreview(UIEvent e)
	{
		iTween.Stop(this.m_rewardsPreview);
		iTween.ScaleTo(this.m_rewardsPreview, iTween.Hash(new object[]
		{
			"scale",
			Vector3.one * 0.01f,
			"time",
			0.15f,
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.m_rewardsPreview.SetActive(false);
			})
		}));
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00005291 File Offset: 0x00003491
	private IEnumerator PlayEntryQuoteWithTiming()
	{
		yield return new WaitForSeconds(this.m_delayBeforeEntryVO);
		AdventureWingDef wingDef = AdventureScene.Get().GetWingDef(this.m_wingId);
		if (AdventureUtils.CanPlayWingOpenQuote(wingDef))
		{
			string legacyAssetName = new AssetReference(wingDef.m_OpenQuoteVOLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(wingDef.m_OpenQuotePrefab, NotificationManager.CHARACTER_POS_ABOVE_QUEST_TOAST, GameStrings.Get(legacyAssetName), wingDef.m_OpenQuoteVOLine, false, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x000052A0 File Offset: 0x000034A0
	private IEnumerator PlayCompleteQuoteWithTiming()
	{
		yield return new WaitForSeconds(this.m_delayBeforeCompleteVO);
		AdventureWingDef wingDef = AdventureScene.Get().GetWingDef(this.m_wingId);
		if (AdventureUtils.CanPlayWingCompleteQuote(wingDef))
		{
			string legacyAssetName = new AssetReference(wingDef.m_CompleteQuoteVOLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(wingDef.m_CompleteQuotePrefab, NotificationManager.CHARACTER_POS_ABOVE_QUEST_TOAST, GameStrings.Get(legacyAssetName), wingDef.m_CompleteQuoteVOLine, false, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04000069 RID: 105
	[CustomEditField(Sections = "Buttons")]
	public PlayButton m_playButton;

	// Token: 0x0400006A RID: 106
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_backButton;

	// Token: 0x0400006B RID: 107
	[CustomEditField(Sections = "Buttons")]
	public PegUIElement m_rewardChest;

	// Token: 0x0400006C RID: 108
	[CustomEditField(Sections = "Text")]
	public UberText m_bonusChallengeLabel;

	// Token: 0x0400006D RID: 109
	[CustomEditField(Sections = "Text")]
	public UberText m_headerText;

	// Token: 0x0400006E RID: 110
	[CustomEditField(Sections = "Text")]
	public UberText m_footerText;

	// Token: 0x0400006F RID: 111
	[CustomEditField(Sections = "Rewards")]
	public GameObject m_rewardsPreview;

	// Token: 0x04000070 RID: 112
	[CustomEditField(Sections = "Rewards")]
	public GameObject m_rewardContainer;

	// Token: 0x04000071 RID: 113
	[CustomEditField(Sections = "Rewards")]
	public UberText m_rewardsText;

	// Token: 0x04000072 RID: 114
	[CustomEditField(Sections = "Rewards")]
	public Material m_chestOpenMaterial;

	// Token: 0x04000073 RID: 115
	[CustomEditField(Sections = "VO")]
	public float m_delayBeforeEntryVO;

	// Token: 0x04000074 RID: 116
	[CustomEditField(Sections = "VO")]
	public float m_delayBeforeCompleteVO;

	// Token: 0x04000075 RID: 117
	[CustomEditField(Sections = "Phone")]
	public PegUIElement m_rewardOffClickCatcher;

	// Token: 0x04000076 RID: 118
	private string m_headerString;

	// Token: 0x04000077 RID: 119
	private string m_footerString;

	// Token: 0x04000078 RID: 120
	private Vector3 m_rewardsScale;

	// Token: 0x04000079 RID: 121
	private GameObject m_rewardObject;

	// Token: 0x0400007A RID: 122
	private WingDbId m_wingId;
}
