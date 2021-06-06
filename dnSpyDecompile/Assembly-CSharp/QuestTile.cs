using System;
using System.Collections.Generic;
using Blizzard.T5.Core;
using UnityEngine;

// Token: 0x02000637 RID: 1591
[CustomEditClass]
public class QuestTile : MonoBehaviour
{
	// Token: 0x0600598B RID: 22923 RVA: 0x001D303A File Offset: 0x001D123A
	private void Awake()
	{
		this.SetCanShowCancelButton(false);
		this.m_cancelButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCancelButtonReleased));
	}

	// Token: 0x0600598C RID: 22924 RVA: 0x001D305C File Offset: 0x001D125C
	public Achievement GetQuest()
	{
		return this.m_quest;
	}

	// Token: 0x0600598D RID: 22925 RVA: 0x001D3064 File Offset: 0x001D1264
	public void SetupTile(Achievement quest, QuestTile.FsmEvent fsmEventToPlay = QuestTile.FsmEvent.None)
	{
		quest.AckCurrentProgressAndRewardNotices(true);
		this.m_quest = quest;
		this.m_rewards = this.m_quest.Rewards;
		if (this.m_quest.MaxProgress > 1)
		{
			this.m_progressText.Text = this.m_quest.Progress + "/" + this.m_quest.MaxProgress;
			this.m_progress.SetActive(true);
		}
		else
		{
			this.m_progressText.Text = "";
			this.m_progress.SetActive(false);
		}
		if (quest.IsLegendary)
		{
			this.m_tileRenderer.SetMaterial(this.m_tileLegendaryMaterial);
			this.m_legendaryFX.SetActive(true);
		}
		else
		{
			this.m_tileRenderer.SetMaterial(this.m_tileNormalMaterial);
			this.m_legendaryFX.SetActive(false);
		}
		this.m_questName.Text = quest.Name;
		RewardUtils.SetQuestTileNameLinePosition(this.m_nameLine, this.m_questName, 0.22f);
		this.m_requirement.Text = quest.Description;
		this.SetupRewardIcons();
		this.SetVisible(false);
		this.LoadFsmAndPlayFX(fsmEventToPlay);
	}

	// Token: 0x0600598E RID: 22926 RVA: 0x001D318D File Offset: 0x001D138D
	public void OnDeathFinishedPlaying()
	{
		this.m_fsmHasDeathFxFinishedPlaying = true;
		if (this.m_fsmHasPendingQuestRerolledEvent)
		{
			this.m_fsmHasPendingQuestRerolledEvent = false;
			this.SendFsmEvent(QuestTile.FsmEvent.QuestRerolled);
		}
	}

	// Token: 0x0600598F RID: 22927 RVA: 0x001D31AC File Offset: 0x001D13AC
	public void SetCanShowCancelButton(bool canShowCancel)
	{
		this.m_canShowCancelButton = canShowCancel;
		this.UpdateCancelButtonVisibility();
	}

	// Token: 0x06005990 RID: 22928 RVA: 0x001D31BC File Offset: 0x001D13BC
	public void UpdateCancelButtonVisibility()
	{
		bool active = false;
		if (this.m_canShowCancelButton && this.m_quest != null)
		{
			active = AchieveManager.Get().CanCancelQuest(this.m_quest.ID);
		}
		this.m_cancelButtonRoot.gameObject.SetActive(active);
	}

	// Token: 0x06005991 RID: 22929 RVA: 0x001D3202 File Offset: 0x001D1402
	public int GetQuestID()
	{
		if (this.m_quest == null)
		{
			return 0;
		}
		return this.m_quest.ID;
	}

	// Token: 0x06005992 RID: 22930 RVA: 0x001D321C File Offset: 0x001D141C
	public void OnClose()
	{
		foreach (QuestTileRewardIcon questTileRewardIcon in this.m_rewardIcons)
		{
			questTileRewardIcon.OnClose();
		}
		this.SendFsmEvent(QuestTile.FsmEvent.QuestHidden);
	}

	// Token: 0x06005993 RID: 22931 RVA: 0x001D3274 File Offset: 0x001D1474
	public void CompleteAndAutoDestroyQuest()
	{
		if (this.m_quest == null || !this.m_quest.AutoDestroy || this.m_fsmForAutoDestroyQuest == null)
		{
			return;
		}
		this.m_fsmForAutoDestroyQuest.SendEvent("Death");
		AchieveManager.Get().CompleteAutoDestroyAchieve(this.m_quest.ID);
	}

	// Token: 0x06005994 RID: 22932 RVA: 0x001D32CC File Offset: 0x001D14CC
	private void ReplaceAutoDestroyQuest()
	{
		if (this.m_quest == null || !this.m_quest.AutoDestroy || this.m_fsmForAutoDestroyQuest == null)
		{
			return;
		}
		int linkToId = this.m_quest.LinkToId;
		if (linkToId == 0)
		{
			return;
		}
		this.OnClose();
		Achievement achievement = AchieveManager.Get().GetAchievement(linkToId);
		this.SetupTile(achievement, QuestTile.FsmEvent.None);
		this.m_fsmForAutoDestroyQuest.SendEvent("Birth");
	}

	// Token: 0x06005995 RID: 22933 RVA: 0x001D3338 File Offset: 0x001D1538
	private void SetVisible(bool visible)
	{
		Renderer[] componentsInChildren = base.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Renderer[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = visible;
			}
		}
		UberText[] componentsInChildren2 = base.GetComponentsInChildren<UberText>();
		if (componentsInChildren2 != null)
		{
			foreach (UberText uberText in componentsInChildren2)
			{
				if (visible)
				{
					uberText.Show();
				}
				else
				{
					uberText.Hide();
				}
			}
		}
	}

	// Token: 0x06005996 RID: 22934 RVA: 0x001D33A0 File Offset: 0x001D15A0
	private void OnCancelButtonReleased(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			DialogManager.Get().ShowReconnectHelperDialog(delegate
			{
				this.OnCancelButtonReleased(e);
			}, null);
			return;
		}
		if (this.m_quest.IsLegendary)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_LEGENDARY_QUEST_REROLL_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_LEGENDARY_QUEST_REROLL_BODY");
			popupInfo.m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES");
			popupInfo.m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = new AlertPopup.ResponseCallback(this.OnQuestRerolled);
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		this.OnQuestRerolled(AlertPopup.Response.CONFIRM, null);
	}

	// Token: 0x06005997 RID: 22935 RVA: 0x001D3460 File Offset: 0x001D1660
	private void OnQuestRerolled(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CONFIRM)
		{
			return;
		}
		if (this.m_quest == null)
		{
			return;
		}
		AchieveManager.Get().CancelQuest(this.m_quest.ID);
		foreach (QuestTileRewardIcon questTileRewardIcon in this.m_rewardIcons)
		{
			questTileRewardIcon.OnQuestRerolled();
		}
		this.SendFsmEvent(QuestTile.FsmEvent.Death);
	}

	// Token: 0x06005998 RID: 22936 RVA: 0x001D34DC File Offset: 0x001D16DC
	private void SendFsmEvent(QuestTile.FsmEvent fsmEvent)
	{
		if (fsmEvent != QuestTile.FsmEvent.None && this.m_fsm != null)
		{
			this.m_fsm.SendEvent(fsmEvent.ToString());
			if (fsmEvent == QuestTile.FsmEvent.QuestHidden || fsmEvent == QuestTile.FsmEvent.Death)
			{
				this.m_fsmHasBeenSentTerminalEvent = true;
				if (fsmEvent == QuestTile.FsmEvent.Death)
				{
					this.m_fsmHasDeathFxFinishedPlaying = false;
				}
			}
		}
	}

	// Token: 0x06005999 RID: 22937 RVA: 0x001D352C File Offset: 0x001D172C
	[ContextMenu("Reset Quest Seen")]
	private void ResetQuestSeen()
	{
		AchieveManager.Get().ResetQuestSeenByPlayerThisSession(this.m_quest);
	}

	// Token: 0x0600599A RID: 22938 RVA: 0x001D3540 File Offset: 0x001D1740
	private void LoadFsmAndPlayFX(QuestTile.FsmEvent fsmEventToPlay)
	{
		string input = this.m_fxPrefabDefault;
		AchieveRegionDataDbfRecord currentRegionData = this.m_quest.GetCurrentRegionData();
		if (currentRegionData != null && !string.IsNullOrEmpty(currentRegionData.ActivateEvent) && currentRegionData.ActivateEvent != EnumUtils.GetString<SpecialEventType>(SpecialEventType.UNKNOWN))
		{
			foreach (QuestTile.SpecialEventFxEntry specialEventFxEntry in this.m_specialEventFx)
			{
				if (!string.IsNullOrEmpty(specialEventFxEntry.m_fxPrefab) && specialEventFxEntry.m_questActivatedBySpecialEventType != SpecialEventType.UNKNOWN && !(EnumUtils.GetString<SpecialEventType>(specialEventFxEntry.m_questActivatedBySpecialEventType) != currentRegionData.ActivateEvent))
				{
					input = specialEventFxEntry.m_fxPrefab;
					break;
				}
			}
		}
		AssetLoader.Get().InstantiatePrefab(input, new PrefabCallback<GameObject>(this.OnFxPrefabLoaded), fsmEventToPlay, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x0600599B RID: 22939 RVA: 0x001D3620 File Offset: 0x001D1820
	private void OnFxPrefabLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			return;
		}
		if (base.gameObject == null)
		{
			UnityEngine.Object.Destroy(go);
			return;
		}
		GameUtils.SetParent(go, base.gameObject, false);
		SceneUtils.SetLayer(go, base.gameObject.layer, null);
		if (this.m_fsm != null && !this.m_fsmHasBeenSentTerminalEvent)
		{
			Debug.LogWarning("QuestTile FSM OnFxPrefabLoaded, but existing FSM has not been sent death event!");
			return;
		}
		this.m_fsmHasBeenSentTerminalEvent = false;
		this.m_fsm = go.GetComponent<PlayMakerFSM>();
		if (this.m_fsm == null)
		{
			return;
		}
		this.SendFsmEvent(QuestTile.FsmEvent.Birth);
		bool value = AchieveManager.Get().MarkQuestAsSeenByPlayerThisSession(this.m_quest);
		this.m_fsm.FsmVariables.GetFsmBool("IsFirstTimeShown").Value = value;
		QuestTile.FsmEvent fsmEvent;
		if (EnumUtils.TryCast<QuestTile.FsmEvent>(callbackData, out fsmEvent))
		{
			if (fsmEvent == QuestTile.FsmEvent.QuestRerolled && !this.m_fsmHasDeathFxFinishedPlaying)
			{
				this.m_fsmHasPendingQuestRerolledEvent = true;
				return;
			}
			this.SendFsmEvent(fsmEvent);
		}
	}

	// Token: 0x0600599C RID: 22940 RVA: 0x001D3710 File Offset: 0x001D1910
	private void SetupRewardIcons()
	{
		foreach (QuestTileRewardIcon questTileRewardIcon in this.m_rewardIcons)
		{
			UnityEngine.Object.Destroy(questTileRewardIcon.gameObject);
		}
		this.m_rewardIcons.Clear();
		RewardChestContentsDbfRecord rewardChestContentsDbfRecord = null;
		if (this.m_quest.DbfRecord.Reward == "generic_reward_chest")
		{
			int rewardChestAssetId = (int)this.m_quest.DbfRecord.RewardData1;
			int rewardLevel = (int)this.m_quest.DbfRecord.RewardData2;
			rewardChestContentsDbfRecord = RewardUtils.GetRewardChestContents(rewardChestAssetId, rewardLevel);
			this.m_rewards = RewardUtils.GetRewardDataFromRewardChestAsset(rewardChestAssetId, rewardLevel);
		}
		if (rewardChestContentsDbfRecord != null && !string.IsNullOrEmpty(rewardChestContentsDbfRecord.IconTexture))
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_questTileRewardIconPrefab, this.m_rewardIconZone.transform);
			SceneUtils.SetLayer(gameObject, this.m_rewardIconZone.gameObject.layer, null);
			QuestTileRewardIcon component = gameObject.GetComponent<QuestTileRewardIcon>();
			AssetReference iconTextureAssetRef = new AssetReference(rewardChestContentsDbfRecord.IconTexture);
			Vector2 iconTextureSourceOffset = new Vector2((float)rewardChestContentsDbfRecord.IconOffsetX, (float)rewardChestContentsDbfRecord.IconOffsetY);
			int renderQueue = 3000;
			component.InitWithIconParams(renderQueue, iconTextureAssetRef, iconTextureSourceOffset, null);
			this.m_rewardIcons.Add(component);
			return;
		}
		this.UnravelPackStacks();
		this.CreateRewardIconsPerReward();
	}

	// Token: 0x0600599D RID: 22941 RVA: 0x001D386C File Offset: 0x001D1A6C
	private void UnravelPackStacks()
	{
		bool flag = false;
		bool flag2 = true;
		List<RewardData> list = new List<RewardData>();
		for (int i = 0; i < this.m_rewards.Count; i++)
		{
			if (this.m_rewards[i].RewardType == Reward.Type.BOOSTER_PACK)
			{
				flag = true;
				BoosterPackRewardData boosterPackRewardData = this.m_rewards[i] as BoosterPackRewardData;
				for (int j = 0; j < boosterPackRewardData.Count; j++)
				{
					list.Add(this.m_rewards[i]);
				}
			}
			else
			{
				flag2 = false;
			}
		}
		if (!flag2)
		{
			Log.Achievements.PrintWarning("Attempted to display a mixture of packs and other rewards without using a specific Reward Chest icon.", Array.Empty<object>());
		}
		if (flag && flag2)
		{
			this.m_rewards = list;
			this.m_rewardIconPadding = this.m_rewardIconPaddingPacksOnly;
			this.m_rewardIconScaleReductionForEachAdditional = 0f;
			this.m_rewardIconShrinkToFitEnabled = false;
		}
	}

	// Token: 0x0600599E RID: 22942 RVA: 0x001D3930 File Offset: 0x001D1B30
	private void CreateRewardIconsPerReward()
	{
		bool isDoubleGoldEnabled = this.m_quest.IsAffectedByDoubleGold && SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_GOLD_DOUBLED, false);
		float num = this.m_rewardIconZone.GetComponent<BoxCollider>().size.x / (float)this.m_rewards.Count;
		int num2 = 3000 + this.m_rewards.Count - 1;
		float num3 = 0f;
		float num4 = GeneralUtils.IsEven(this.m_rewards.Count) ? -1f : 1f;
		for (int i = 0; i < this.m_rewards.Count; i++)
		{
			RewardData rewardData = this.m_rewards[i];
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_questTileRewardIconPrefab, this.m_rewardIconZone.transform);
			SceneUtils.SetLayer(gameObject, this.m_rewardIconZone.gameObject.layer, null);
			QuestTileRewardIcon component = gameObject.GetComponent<QuestTileRewardIcon>();
			component.InitWithRewardData(rewardData, isDoubleGoldEnabled, num2);
			this.m_rewardIcons.Add(component);
			if ((GeneralUtils.IsOdd(this.m_rewards.Count) && GeneralUtils.IsOdd(i)) || (GeneralUtils.IsEven(this.m_rewards.Count) && GeneralUtils.IsEven(i)))
			{
				num3 += num / 2f;
				num3 += this.m_rewardIconPadding;
			}
			num2--;
			num4 *= -1f;
			float x = num3 * num4;
			gameObject.transform.localPosition = new Vector3(x, 0f, 0f);
			if (i == 0 && this.m_rewards.Count > 1 && GeneralUtils.IsOdd(this.m_rewards.Count))
			{
				num3 += num / 2f;
			}
			if (this.m_rewardIconShrinkToFitEnabled)
			{
				float x2 = gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x;
				float d = Math.Min(1f, num / x2);
				gameObject.transform.localScale *= d;
			}
			if (this.m_rewardIconScaleReductionForEachAdditional > 0f)
			{
				float d2 = 1f - (float)Math.Max(0, this.m_rewards.Count - 1) * this.m_rewardIconScaleReductionForEachAdditional;
				gameObject.transform.localScale *= d2;
			}
		}
	}

	// Token: 0x04004C91 RID: 19601
	public UberText m_requirement;

	// Token: 0x04004C92 RID: 19602
	public UberText m_questName;

	// Token: 0x04004C93 RID: 19603
	public GameObject m_nameLine;

	// Token: 0x04004C94 RID: 19604
	public GameObject m_progress;

	// Token: 0x04004C95 RID: 19605
	public UberText m_progressText;

	// Token: 0x04004C96 RID: 19606
	public NormalButton m_cancelButton;

	// Token: 0x04004C97 RID: 19607
	public GameObject m_cancelButtonRoot;

	// Token: 0x04004C98 RID: 19608
	public PlayMakerFSM m_fsmForAutoDestroyQuest;

	// Token: 0x04004C99 RID: 19609
	public GameObject m_legendaryFX;

	// Token: 0x04004C9A RID: 19610
	public MeshRenderer m_tileRenderer;

	// Token: 0x04004C9B RID: 19611
	public Material m_tileNormalMaterial;

	// Token: 0x04004C9C RID: 19612
	public Material m_tileLegendaryMaterial;

	// Token: 0x04004C9D RID: 19613
	public GameObject m_rewardIconZone;

	// Token: 0x04004C9E RID: 19614
	public GameObject m_questTileRewardIconPrefab;

	// Token: 0x04004C9F RID: 19615
	[CustomEditField(Sections = "Reward Icons")]
	public bool m_rewardIconShrinkToFitEnabled;

	// Token: 0x04004CA0 RID: 19616
	[CustomEditField(Sections = "Reward Icons")]
	public float m_rewardIconPadding;

	// Token: 0x04004CA1 RID: 19617
	[CustomEditField(Sections = "Reward Icons")]
	public float m_rewardIconPaddingPacksOnly = -0.25f;

	// Token: 0x04004CA2 RID: 19618
	[CustomEditField(Sections = "Reward Icons")]
	public float m_rewardIconScaleReductionForEachAdditional;

	// Token: 0x04004CA3 RID: 19619
	[CustomEditField(Sections = "Special Event FX", T = EditType.GAME_OBJECT)]
	public string m_fxPrefabDefault;

	// Token: 0x04004CA4 RID: 19620
	[CustomEditField(Sections = "Special Event FX")]
	public List<QuestTile.SpecialEventFxEntry> m_specialEventFx = new List<QuestTile.SpecialEventFxEntry>();

	// Token: 0x04004CA5 RID: 19621
	private Achievement m_quest;

	// Token: 0x04004CA6 RID: 19622
	private bool m_canShowCancelButton;

	// Token: 0x04004CA7 RID: 19623
	private List<QuestTileRewardIcon> m_rewardIcons = new List<QuestTileRewardIcon>();

	// Token: 0x04004CA8 RID: 19624
	private List<RewardData> m_rewards = new List<RewardData>();

	// Token: 0x04004CA9 RID: 19625
	private PlayMakerFSM m_fsm;

	// Token: 0x04004CAA RID: 19626
	private bool m_fsmHasBeenSentTerminalEvent;

	// Token: 0x04004CAB RID: 19627
	private bool m_fsmHasDeathFxFinishedPlaying;

	// Token: 0x04004CAC RID: 19628
	private bool m_fsmHasPendingQuestRerolledEvent;

	// Token: 0x04004CAD RID: 19629
	private const float NAME_LINE_PADDING = 0.22f;

	// Token: 0x0200213A RID: 8506
	public enum FsmEvent
	{
		// Token: 0x0400DF9F RID: 57247
		None,
		// Token: 0x0400DFA0 RID: 57248
		Birth,
		// Token: 0x0400DFA1 RID: 57249
		Death,
		// Token: 0x0400DFA2 RID: 57250
		QuestGranted,
		// Token: 0x0400DFA3 RID: 57251
		QuestRerolled,
		// Token: 0x0400DFA4 RID: 57252
		QuestShownInQuestAlert,
		// Token: 0x0400DFA5 RID: 57253
		QuestShownInQuestLog,
		// Token: 0x0400DFA6 RID: 57254
		QuestHidden
	}

	// Token: 0x0200213B RID: 8507
	[Serializable]
	public class SpecialEventFxEntry
	{
		// Token: 0x0400DFA7 RID: 57255
		public SpecialEventType m_questActivatedBySpecialEventType;

		// Token: 0x0400DFA8 RID: 57256
		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_fxPrefab;
	}
}
