using System;
using System.Collections.Generic;
using Blizzard.T5.Core;
using UnityEngine;

[CustomEditClass]
public class QuestTile : MonoBehaviour
{
	public enum FsmEvent
	{
		None,
		Birth,
		Death,
		QuestGranted,
		QuestRerolled,
		QuestShownInQuestAlert,
		QuestShownInQuestLog,
		QuestHidden
	}

	[Serializable]
	public class SpecialEventFxEntry
	{
		public SpecialEventType m_questActivatedBySpecialEventType;

		[CustomEditField(T = EditType.GAME_OBJECT)]
		public string m_fxPrefab;
	}

	public UberText m_requirement;

	public UberText m_questName;

	public GameObject m_nameLine;

	public GameObject m_progress;

	public UberText m_progressText;

	public NormalButton m_cancelButton;

	public GameObject m_cancelButtonRoot;

	public PlayMakerFSM m_fsmForAutoDestroyQuest;

	public GameObject m_legendaryFX;

	public MeshRenderer m_tileRenderer;

	public Material m_tileNormalMaterial;

	public Material m_tileLegendaryMaterial;

	public GameObject m_rewardIconZone;

	public GameObject m_questTileRewardIconPrefab;

	[CustomEditField(Sections = "Reward Icons")]
	public bool m_rewardIconShrinkToFitEnabled;

	[CustomEditField(Sections = "Reward Icons")]
	public float m_rewardIconPadding;

	[CustomEditField(Sections = "Reward Icons")]
	public float m_rewardIconPaddingPacksOnly = -0.25f;

	[CustomEditField(Sections = "Reward Icons")]
	public float m_rewardIconScaleReductionForEachAdditional;

	[CustomEditField(Sections = "Special Event FX", T = EditType.GAME_OBJECT)]
	public string m_fxPrefabDefault;

	[CustomEditField(Sections = "Special Event FX")]
	public List<SpecialEventFxEntry> m_specialEventFx = new List<SpecialEventFxEntry>();

	private Achievement m_quest;

	private bool m_canShowCancelButton;

	private List<QuestTileRewardIcon> m_rewardIcons = new List<QuestTileRewardIcon>();

	private List<RewardData> m_rewards = new List<RewardData>();

	private PlayMakerFSM m_fsm;

	private bool m_fsmHasBeenSentTerminalEvent;

	private bool m_fsmHasDeathFxFinishedPlaying;

	private bool m_fsmHasPendingQuestRerolledEvent;

	private const float NAME_LINE_PADDING = 0.22f;

	private void Awake()
	{
		SetCanShowCancelButton(canShowCancel: false);
		m_cancelButton.AddEventListener(UIEventType.RELEASE, OnCancelButtonReleased);
	}

	public Achievement GetQuest()
	{
		return m_quest;
	}

	public void SetupTile(Achievement quest, FsmEvent fsmEventToPlay = FsmEvent.None)
	{
		quest.AckCurrentProgressAndRewardNotices(ackIntermediateProgress: true);
		m_quest = quest;
		m_rewards = m_quest.Rewards;
		if (m_quest.MaxProgress > 1)
		{
			m_progressText.Text = m_quest.Progress + "/" + m_quest.MaxProgress;
			m_progress.SetActive(value: true);
		}
		else
		{
			m_progressText.Text = "";
			m_progress.SetActive(value: false);
		}
		if (quest.IsLegendary)
		{
			m_tileRenderer.SetMaterial(m_tileLegendaryMaterial);
			m_legendaryFX.SetActive(value: true);
		}
		else
		{
			m_tileRenderer.SetMaterial(m_tileNormalMaterial);
			m_legendaryFX.SetActive(value: false);
		}
		m_questName.Text = quest.Name;
		RewardUtils.SetQuestTileNameLinePosition(m_nameLine, m_questName, 0.22f);
		m_requirement.Text = quest.Description;
		SetupRewardIcons();
		SetVisible(visible: false);
		LoadFsmAndPlayFX(fsmEventToPlay);
	}

	public void OnDeathFinishedPlaying()
	{
		m_fsmHasDeathFxFinishedPlaying = true;
		if (m_fsmHasPendingQuestRerolledEvent)
		{
			m_fsmHasPendingQuestRerolledEvent = false;
			SendFsmEvent(FsmEvent.QuestRerolled);
		}
	}

	public void SetCanShowCancelButton(bool canShowCancel)
	{
		m_canShowCancelButton = canShowCancel;
		UpdateCancelButtonVisibility();
	}

	public void UpdateCancelButtonVisibility()
	{
		bool active = false;
		if (m_canShowCancelButton && m_quest != null)
		{
			active = AchieveManager.Get().CanCancelQuest(m_quest.ID);
		}
		m_cancelButtonRoot.gameObject.SetActive(active);
	}

	public int GetQuestID()
	{
		if (m_quest == null)
		{
			return 0;
		}
		return m_quest.ID;
	}

	public void OnClose()
	{
		foreach (QuestTileRewardIcon rewardIcon in m_rewardIcons)
		{
			rewardIcon.OnClose();
		}
		SendFsmEvent(FsmEvent.QuestHidden);
	}

	public void CompleteAndAutoDestroyQuest()
	{
		if (m_quest != null && m_quest.AutoDestroy && !(m_fsmForAutoDestroyQuest == null))
		{
			m_fsmForAutoDestroyQuest.SendEvent("Death");
			AchieveManager.Get().CompleteAutoDestroyAchieve(m_quest.ID);
		}
	}

	private void ReplaceAutoDestroyQuest()
	{
		if (m_quest != null && m_quest.AutoDestroy && !(m_fsmForAutoDestroyQuest == null))
		{
			int linkToId = m_quest.LinkToId;
			if (linkToId != 0)
			{
				OnClose();
				Achievement achievement = AchieveManager.Get().GetAchievement(linkToId);
				SetupTile(achievement);
				m_fsmForAutoDestroyQuest.SendEvent("Birth");
			}
		}
	}

	private void SetVisible(bool visible)
	{
		Renderer[] componentsInChildren = GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Renderer[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = visible;
			}
		}
		UberText[] componentsInChildren2 = GetComponentsInChildren<UberText>();
		if (componentsInChildren2 == null)
		{
			return;
		}
		UberText[] array2 = componentsInChildren2;
		foreach (UberText uberText in array2)
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

	private void OnCancelButtonReleased(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			DialogManager.Get().ShowReconnectHelperDialog(delegate
			{
				OnCancelButtonReleased(e);
			});
		}
		else if (m_quest.IsLegendary)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_LEGENDARY_QUEST_REROLL_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_LEGENDARY_QUEST_REROLL_BODY");
			popupInfo.m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES");
			popupInfo.m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = OnQuestRerolled;
			DialogManager.Get().ShowPopup(popupInfo);
		}
		else
		{
			OnQuestRerolled(AlertPopup.Response.CONFIRM, null);
		}
	}

	private void OnQuestRerolled(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CONFIRM || m_quest == null)
		{
			return;
		}
		AchieveManager.Get().CancelQuest(m_quest.ID);
		foreach (QuestTileRewardIcon rewardIcon in m_rewardIcons)
		{
			rewardIcon.OnQuestRerolled();
		}
		SendFsmEvent(FsmEvent.Death);
	}

	private void SendFsmEvent(FsmEvent fsmEvent)
	{
		if (fsmEvent == FsmEvent.None || !(m_fsm != null))
		{
			return;
		}
		m_fsm.SendEvent(fsmEvent.ToString());
		if (fsmEvent == FsmEvent.QuestHidden || fsmEvent == FsmEvent.Death)
		{
			m_fsmHasBeenSentTerminalEvent = true;
			if (fsmEvent == FsmEvent.Death)
			{
				m_fsmHasDeathFxFinishedPlaying = false;
			}
		}
	}

	[ContextMenu("Reset Quest Seen")]
	private void ResetQuestSeen()
	{
		AchieveManager.Get().ResetQuestSeenByPlayerThisSession(m_quest);
	}

	private void LoadFsmAndPlayFX(FsmEvent fsmEventToPlay)
	{
		string text = m_fxPrefabDefault;
		AchieveRegionDataDbfRecord currentRegionData = m_quest.GetCurrentRegionData();
		if (currentRegionData != null && !string.IsNullOrEmpty(currentRegionData.ActivateEvent) && currentRegionData.ActivateEvent != EnumUtils.GetString(SpecialEventType.UNKNOWN))
		{
			foreach (SpecialEventFxEntry item in m_specialEventFx)
			{
				if (!string.IsNullOrEmpty(item.m_fxPrefab) && item.m_questActivatedBySpecialEventType != SpecialEventType.UNKNOWN && !(EnumUtils.GetString(item.m_questActivatedBySpecialEventType) != currentRegionData.ActivateEvent))
				{
					text = item.m_fxPrefab;
					break;
				}
			}
		}
		AssetLoader.Get().InstantiatePrefab(text, OnFxPrefabLoaded, fsmEventToPlay, AssetLoadingOptions.IgnorePrefabPosition);
	}

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
		GameUtils.SetParent(go, base.gameObject);
		SceneUtils.SetLayer(go, base.gameObject.layer);
		if (m_fsm != null && !m_fsmHasBeenSentTerminalEvent)
		{
			Debug.LogWarning("QuestTile FSM OnFxPrefabLoaded, but existing FSM has not been sent death event!");
			return;
		}
		m_fsmHasBeenSentTerminalEvent = false;
		m_fsm = go.GetComponent<PlayMakerFSM>();
		if (m_fsm == null)
		{
			return;
		}
		SendFsmEvent(FsmEvent.Birth);
		bool value = AchieveManager.Get().MarkQuestAsSeenByPlayerThisSession(m_quest);
		m_fsm.FsmVariables.GetFsmBool("IsFirstTimeShown").Value = value;
		if (EnumUtils.TryCast<FsmEvent>(callbackData, out var outVal))
		{
			if (outVal == FsmEvent.QuestRerolled && !m_fsmHasDeathFxFinishedPlaying)
			{
				m_fsmHasPendingQuestRerolledEvent = true;
			}
			else
			{
				SendFsmEvent(outVal);
			}
		}
	}

	private void SetupRewardIcons()
	{
		foreach (QuestTileRewardIcon rewardIcon in m_rewardIcons)
		{
			UnityEngine.Object.Destroy(rewardIcon.gameObject);
		}
		m_rewardIcons.Clear();
		RewardChestContentsDbfRecord rewardChestContentsDbfRecord = null;
		if (m_quest.DbfRecord.Reward == "generic_reward_chest")
		{
			int rewardChestAssetId = (int)m_quest.DbfRecord.RewardData1;
			int rewardLevel = (int)m_quest.DbfRecord.RewardData2;
			rewardChestContentsDbfRecord = RewardUtils.GetRewardChestContents(rewardChestAssetId, rewardLevel);
			m_rewards = RewardUtils.GetRewardDataFromRewardChestAsset(rewardChestAssetId, rewardLevel);
		}
		if (rewardChestContentsDbfRecord != null && !string.IsNullOrEmpty(rewardChestContentsDbfRecord.IconTexture))
		{
			GameObject obj = UnityEngine.Object.Instantiate(m_questTileRewardIconPrefab, m_rewardIconZone.transform);
			SceneUtils.SetLayer(obj, m_rewardIconZone.gameObject.layer);
			QuestTileRewardIcon component = obj.GetComponent<QuestTileRewardIcon>();
			AssetReference iconTextureAssetRef = new AssetReference(rewardChestContentsDbfRecord.IconTexture);
			Vector2 iconTextureSourceOffset = new Vector2((float)rewardChestContentsDbfRecord.IconOffsetX, (float)rewardChestContentsDbfRecord.IconOffsetY);
			int renderQueue = 3000;
			component.InitWithIconParams(renderQueue, iconTextureAssetRef, iconTextureSourceOffset, null);
			m_rewardIcons.Add(component);
		}
		else
		{
			UnravelPackStacks();
			CreateRewardIconsPerReward();
		}
	}

	private void UnravelPackStacks()
	{
		bool flag = false;
		bool flag2 = true;
		List<RewardData> list = new List<RewardData>();
		for (int i = 0; i < m_rewards.Count; i++)
		{
			if (m_rewards[i].RewardType == Reward.Type.BOOSTER_PACK)
			{
				flag = true;
				BoosterPackRewardData boosterPackRewardData = m_rewards[i] as BoosterPackRewardData;
				for (int j = 0; j < boosterPackRewardData.Count; j++)
				{
					list.Add(m_rewards[i]);
				}
			}
			else
			{
				flag2 = false;
			}
		}
		if (!flag2)
		{
			Log.Achievements.PrintWarning("Attempted to display a mixture of packs and other rewards without using a specific Reward Chest icon.");
		}
		if (flag && flag2)
		{
			m_rewards = list;
			m_rewardIconPadding = m_rewardIconPaddingPacksOnly;
			m_rewardIconScaleReductionForEachAdditional = 0f;
			m_rewardIconShrinkToFitEnabled = false;
		}
	}

	private void CreateRewardIconsPerReward()
	{
		bool isDoubleGoldEnabled = m_quest.IsAffectedByDoubleGold && SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_GOLD_DOUBLED, activeIfDoesNotExist: false);
		float num = m_rewardIconZone.GetComponent<BoxCollider>().size.x / (float)m_rewards.Count;
		int num2 = 3000 + m_rewards.Count - 1;
		float num3 = 0f;
		float num4 = (GeneralUtils.IsEven(m_rewards.Count) ? (-1f) : 1f);
		for (int i = 0; i < m_rewards.Count; i++)
		{
			RewardData rewardData = m_rewards[i];
			GameObject gameObject = UnityEngine.Object.Instantiate(m_questTileRewardIconPrefab, m_rewardIconZone.transform);
			SceneUtils.SetLayer(gameObject, m_rewardIconZone.gameObject.layer);
			QuestTileRewardIcon component = gameObject.GetComponent<QuestTileRewardIcon>();
			component.InitWithRewardData(rewardData, isDoubleGoldEnabled, num2);
			m_rewardIcons.Add(component);
			if ((GeneralUtils.IsOdd(m_rewards.Count) && GeneralUtils.IsOdd(i)) || (GeneralUtils.IsEven(m_rewards.Count) && GeneralUtils.IsEven(i)))
			{
				num3 += num / 2f;
				num3 += m_rewardIconPadding;
			}
			num2--;
			num4 *= -1f;
			float x = num3 * num4;
			gameObject.transform.localPosition = new Vector3(x, 0f, 0f);
			if (i == 0 && m_rewards.Count > 1 && GeneralUtils.IsOdd(m_rewards.Count))
			{
				num3 += num / 2f;
			}
			if (m_rewardIconShrinkToFitEnabled)
			{
				float x2 = gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x;
				float num5 = Math.Min(1f, num / x2);
				gameObject.transform.localScale *= num5;
			}
			if (m_rewardIconScaleReductionForEachAdditional > 0f)
			{
				float num6 = 1f - (float)Math.Max(0, m_rewards.Count - 1) * m_rewardIconScaleReductionForEachAdditional;
				gameObject.transform.localScale *= num6;
			}
		}
	}
}
