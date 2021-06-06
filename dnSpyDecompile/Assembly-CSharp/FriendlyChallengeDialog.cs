using System;
using System.Collections;
using bgs;
using Hearthstone.Core;
using Hearthstone.DataModels;
using Hearthstone.Progression;
using Hearthstone.UI;
using PegasusShared;
using SpectatorProto;
using UnityEngine;

// Token: 0x020002F6 RID: 758
public class FriendlyChallengeDialog : DialogBase
{
	// Token: 0x0600283E RID: 10302 RVA: 0x000CA3AE File Offset: 0x000C85AE
	private void Start()
	{
		this.m_acceptButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ConfirmButtonPress));
		this.m_denyButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.CancelButtonPress));
	}

	// Token: 0x0600283F RID: 10303 RVA: 0x000CA3E4 File Offset: 0x000C85E4
	public override void Show()
	{
		base.Show();
		BnetBar.Get().DisableButtonsByDialog(this);
		if (UniversalInputManager.UsePhoneUI && this.m_nearbyPlayerNote.gameObject.activeSelf)
		{
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y + 50f, base.transform.localPosition.z);
		}
		this.DoShowAnimation();
		UniversalInputManager.Get().SetSystemDialogActive(true);
		SoundManager.Get().LoadAndPlay("friendly_challenge.prefab:649e070117bcd0d45bac691a03bf2dec");
		if (this.m_partyQuestInfo != null)
		{
			Processor.ScheduleCallback(this.m_friendQuestSliderSoundDelay, false, delegate(object u)
			{
				SoundManager.Get().LoadAndPlay(this.m_friendQuestSliderSound);
			}, null);
			Processor.ScheduleCallback(this.m_friendQuestSliderSoundDelay2, false, delegate(object u)
			{
				SoundManager.Get().LoadAndPlay(this.m_friendQuestSliderSound2);
			}, null);
		}
	}

	// Token: 0x06002840 RID: 10304 RVA: 0x000CA4C8 File Offset: 0x000C86C8
	public override void Hide()
	{
		base.Hide();
		SoundManager.Get().LoadAndPlay("banner_shrink.prefab:d9de7386a7f2017429d126e972232123");
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			0f,
			"time",
			1f
		});
		iTween.FadeTo(this.m_dropShadow, args);
	}

	// Token: 0x06002841 RID: 10305 RVA: 0x000CA531 File Offset: 0x000C8731
	public override bool HandleKeyboardInput()
	{
		if (InputCollection.GetKeyUp(KeyCode.Escape))
		{
			this.CancelButtonPress(null);
			return true;
		}
		return false;
	}

	// Token: 0x06002842 RID: 10306 RVA: 0x000CA548 File Offset: 0x000C8748
	public void SetInfo(FriendlyChallengeDialog.Info info)
	{
		string key = "GLOBAL_FRIEND_CHALLENGE_BODY1";
		if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			if (FriendChallengeMgr.Get().GetChallengeBrawlType() == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
			{
				key = "GLOBAL_FRIEND_CHALLENGE_FIRESIDE_BRAWL_BODY1";
			}
			else
			{
				key = "GLOBAL_FRIEND_CHALLENGE_TAVERN_BRAWL_BODY1";
			}
		}
		else if (FriendChallengeMgr.Get().IsChallengeBacon() || info.m_partyType == PartyType.BATTLEGROUNDS_PARTY)
		{
			key = "GLOBAL_FRIEND_CHALLENGE_BODY_BACON";
		}
		else if (CollectionManager.Get().ShouldAccountSeeStandardWild())
		{
			if (info.m_formatType == FormatType.FT_STANDARD)
			{
				key = "GLOBAL_FRIEND_CHALLENGE_BODY1_STANDARD";
			}
			else if (info.m_formatType == FormatType.FT_WILD)
			{
				key = "GLOBAL_FRIEND_CHALLENGE_BODY1_WILD";
			}
		}
		this.m_challengeText.Text = GameStrings.Get(key);
		this.m_challengerName.Text = FriendUtils.GetUniqueName(info.m_challenger);
		this.m_responseCallback = info.m_callback;
		bool active = BnetNearbyPlayerMgr.Get().IsNearbyStranger(info.m_challenger);
		this.m_nearbyPlayerNote.gameObject.SetActive(active);
	}

	// Token: 0x06002843 RID: 10307 RVA: 0x000CA622 File Offset: 0x000C8822
	public global::Achievement GetQuest()
	{
		return this.m_quest;
	}

	// Token: 0x06002844 RID: 10308 RVA: 0x000CA62C File Offset: 0x000C882C
	public void SetQuestInfo(PartyQuestInfo info)
	{
		if (this.m_friendQuestContainer == null)
		{
			return;
		}
		this.m_partyQuestInfo = info;
		if (info == null || info.QuestIds.Count == 0)
		{
			this.m_friendQuestContainer.gameObject.SetActive(false);
			return;
		}
		bool flag = false;
		foreach (int id in info.QuestIds)
		{
			AchieveDbfRecord record = GameDbf.Achieve.GetRecord(id);
			if (record != null && record.SharedAchieveId != 0)
			{
				global::Achievement achievement = AchieveManager.Get().GetAchievement(record.SharedAchieveId);
				if (achievement != null)
				{
					AchieveRegionDataDbfRecord currentRegionData = achievement.GetCurrentRegionData();
					if (currentRegionData != null && currentRegionData.RewardableLimit > 0 && achievement.IntervalRewardStartDate > 0L)
					{
						DateTime d = DateTime.FromFileTimeUtc(achievement.IntervalRewardStartDate);
						if ((DateTime.UtcNow - d).TotalDays < currentRegionData.RewardableInterval && achievement.IntervalRewardCount >= currentRegionData.RewardableLimit)
						{
							flag = true;
						}
					}
				}
			}
		}
		if (flag)
		{
			if (this.m_friendlyQuestFrame != null)
			{
				this.m_friendlyQuestFrame.m_noGoldRewardText.Text = GameStrings.Get("GLOBAL_FRIENDLYCHALLENGE_QUEST_REWARD_AT_LIMIT");
				this.m_friendlyQuestFrame.m_questName.Hide();
				this.m_friendlyQuestFrame.m_questDesc.Hide();
				this.m_friendlyQuestFrame.m_nameLine.gameObject.SetActive(false);
				this.m_friendlyQuestFrame.m_rewardMesh.gameObject.SetActive(false);
				this.m_friendlyQuestFrame.m_rewardAmountLabel.Hide();
			}
			this.m_questTileWidget.Hide();
			this.m_friendQuestContainer.gameObject.SetActive(true);
			SlidingTray component = this.m_friendQuestContainer.GetComponent<SlidingTray>();
			if (component != null)
			{
				component.ShowTray();
			}
			return;
		}
		if (!QuestManager.Get().IsSystemEnabled)
		{
			this.m_questTileWidget.Hide();
			this.m_friendQuestContainer.gameObject.SetActive(true);
			SlidingTray component2 = this.m_friendQuestContainer.GetComponent<SlidingTray>();
			if (component2 != null)
			{
				component2.ShowTray();
			}
			Hashtable args = iTween.Hash(new object[]
			{
				"amount",
				1f,
				"time",
				1f
			});
			iTween.FadeTo(this.m_dropShadow, args);
			int achieveID = info.QuestIds[0];
			this.m_quest = AchieveManager.Get().GetAchievement(achieveID);
			if (this.m_quest != null)
			{
				this.m_friendlyQuestFrame.m_questName.Text = this.m_quest.Name;
				this.m_friendlyQuestFrame.m_questDesc.Text = this.m_quest.Description;
				RewardData rewardData = (this.m_quest.Rewards.Count == 0) ? null : this.m_quest.Rewards[0];
				if (rewardData != null && this.m_friendlyQuestFrame.m_rewardBone != null)
				{
					rewardData.LoadRewardObject(new Reward.DelOnRewardLoaded(this.SetQuestInfo_OnLoadRewardObject));
				}
			}
			RewardUtils.SetQuestTileNameLinePosition(this.m_friendlyQuestFrame.m_nameLine, this.m_friendlyQuestFrame.m_questName, 0.01f);
			return;
		}
		this.m_friendlyQuestFrame = ((this.m_friendQuestContainer != null) ? this.m_friendQuestContainer.GetComponentInChildren<FriendlyChallengeQuestFrame>() : null);
		if (this.m_friendlyQuestFrame != null && this.m_friendlyQuestFrame.m_questTileBone != null)
		{
			this.m_questTileWidget = WidgetInstance.Create(Hearthstone.Progression.QuestTile.QUEST_TILE_WIDGET_ASSET, false);
			GameUtils.SetParent(this.m_questTileWidget, this.m_friendlyQuestFrame.m_questTileBone, false);
			this.m_questTileWidget.SetLayerOverride((GameLayer)base.gameObject.layer);
			base.StartCoroutine(this.ShowWhenReady(info));
			return;
		}
		Debug.LogError("FriendlyChallegeDialog.Start - QuestTileWidget is not set!");
	}

	// Token: 0x06002845 RID: 10309 RVA: 0x000CAA08 File Offset: 0x000C8C08
	private void SetQuestInfo_OnLoadRewardObject(Reward reward, object callbackData)
	{
		if (this.m_friendlyQuestFrame.m_rewardBone == null)
		{
			return;
		}
		reward.transform.SetParent(this.m_friendlyQuestFrame.m_rewardBone.transform);
		reward.transform.localPosition = Vector3.zero;
		bool doubleGold = this.m_quest != null && this.m_quest.IsAffectedByDoubleGold && SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_GOLD_DOUBLED, false);
		float d;
		RewardUtils.SetupRewardIcon(this.m_quest.Rewards[0], this.m_friendlyQuestFrame.m_rewardMesh, this.m_friendlyQuestFrame.m_rewardAmountLabel, out d, doubleGold);
		this.m_friendlyQuestFrame.m_rewardMesh.transform.localScale *= d;
		this.m_friendlyQuestFrame.m_rewardAmountLabel.RenderQueue = this.m_friendlyQuestFrame.m_rewardMesh.GetMaterial().renderQueue;
	}

	// Token: 0x06002846 RID: 10310 RVA: 0x000CAAF2 File Offset: 0x000C8CF2
	private void ConfirmButtonPress(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		if (this.m_responseCallback != null)
		{
			this.m_responseCallback(true);
		}
		this.Hide();
	}

	// Token: 0x06002847 RID: 10311 RVA: 0x000CAB22 File Offset: 0x000C8D22
	private void CancelButtonPress(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681");
		if (this.m_responseCallback != null)
		{
			this.m_responseCallback(false);
		}
		this.Hide();
	}

	// Token: 0x06002848 RID: 10312 RVA: 0x000CAB52 File Offset: 0x000C8D52
	private IEnumerator ShowWhenReady(PartyQuestInfo info)
	{
		int questId = 114;
		QuestDataModel questDataModel = QuestManager.Get().CreateQuestDataModelById(questId);
		questDataModel.RerollCount = 0;
		if (this.m_questTileWidget != null)
		{
			this.m_questTileWidget.BindDataModel(questDataModel, false);
			this.m_questTileWidget.TriggerEvent("DISABLE_INTERACTION", default(Widget.TriggerEventParameters));
		}
		this.m_friendQuestContainer.gameObject.SetActive(true);
		while (this.m_questTileWidget != null && (!this.m_questTileWidget.IsReady || this.m_questTileWidget.IsChangingStates))
		{
			yield return null;
		}
		SlidingTray component = this.m_friendQuestContainer.GetComponent<SlidingTray>();
		if (component != null)
		{
			component.ShowTray();
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			1f,
			"time",
			1f
		});
		iTween.FadeTo(this.m_dropShadow, args);
		if (this.m_friendlyQuestFrame != null)
		{
			this.m_friendlyQuestFrame.m_nameLine.SetActive(false);
			this.m_friendlyQuestFrame.m_questDesc.Hide();
			this.m_friendlyQuestFrame.m_rewardAmountLabel.Hide();
			this.m_friendlyQuestFrame.m_rewardMesh.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x040016D6 RID: 5846
	public UberText m_challengeText;

	// Token: 0x040016D7 RID: 5847
	public UberText m_challengerName;

	// Token: 0x040016D8 RID: 5848
	public UIBButton m_acceptButton;

	// Token: 0x040016D9 RID: 5849
	public UIBButton m_denyButton;

	// Token: 0x040016DA RID: 5850
	public UberText m_nearbyPlayerNote;

	// Token: 0x040016DB RID: 5851
	public float m_friendQuestSliderSoundDelay;

	// Token: 0x040016DC RID: 5852
	public string m_friendQuestSliderSound;

	// Token: 0x040016DD RID: 5853
	public float m_friendQuestSliderSoundDelay2;

	// Token: 0x040016DE RID: 5854
	public string m_friendQuestSliderSound2;

	// Token: 0x040016DF RID: 5855
	public GameObject m_friendQuestContainer;

	// Token: 0x040016E0 RID: 5856
	public GameObject m_dropShadow;

	// Token: 0x040016E1 RID: 5857
	private FriendlyChallengeDialog.ResponseCallback m_responseCallback;

	// Token: 0x040016E2 RID: 5858
	private global::Achievement m_quest;

	// Token: 0x040016E3 RID: 5859
	private FriendlyChallengeQuestFrame m_friendlyQuestFrame;

	// Token: 0x040016E4 RID: 5860
	private PartyQuestInfo m_partyQuestInfo;

	// Token: 0x040016E5 RID: 5861
	private Widget m_questTileWidget;

	// Token: 0x040016E6 RID: 5862
	private const float NAME_LINE_PADDING = 0.01f;

	// Token: 0x02001623 RID: 5667
	// (Invoke) Token: 0x0600E301 RID: 58113
	public delegate void ResponseCallback(bool accept);

	// Token: 0x02001624 RID: 5668
	public class Info
	{
		// Token: 0x0400AFE9 RID: 45033
		public FormatType m_formatType;

		// Token: 0x0400AFEA RID: 45034
		public BnetPlayer m_challenger;

		// Token: 0x0400AFEB RID: 45035
		public PartyType m_partyType;

		// Token: 0x0400AFEC RID: 45036
		public FriendlyChallengeDialog.ResponseCallback m_callback;
	}
}
