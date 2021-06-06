using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestRewardDisplay : MonoBehaviour
{
	public const string DEFAULT_PREFAB = "RewardChest_Lock.prefab:06ffa33e82036694e8cacb96aa7b48e8";

	public PegUIElement m_rewardChest;

	public PlayMakerFSM m_FSM;

	public Transform m_parent;

	public GameObject m_descText;

	public UberText m_bannerUberText;

	public Transform m_rewardBoxBone;

	public Transform m_rewardBoxBonePackOpening;

	private List<RewardData> m_rewards = new List<RewardData>();

	private List<Action> m_doneCallbacks = new List<Action>();

	private bool m_fromNotice;

	private long m_noticeID = -1L;

	private int m_wins;

	private int m_leagueId;

	public bool ShowRewards_TavernBrawl(int wins, List<RewardData> rewards, Transform rewardBone, bool fromNotice = false, long noticeID = -1L)
	{
		if (rewards == null || rewards.Count < 1)
		{
			Debug.LogErrorFormat("rewards is null!");
			return false;
		}
		m_wins = wins;
		m_rewards = rewards;
		m_fromNotice = fromNotice;
		m_noticeID = noticeID;
		m_descText.SetActive(fromNotice);
		ShowRewardChest_TavernBrawl();
		return true;
	}

	public bool ShowRewards_LeaguePromotion(int leagueId, List<RewardData> rewards, Transform rewardBone, bool fromNotice = false, long noticeID = -1L)
	{
		if (rewards == null || rewards.Count < 1)
		{
			Debug.LogErrorFormat("rewards is null!");
			return false;
		}
		m_leagueId = leagueId;
		m_rewards = rewards;
		m_fromNotice = fromNotice;
		m_noticeID = noticeID;
		ShowRewardChest_LeaguePromotion();
		return true;
	}

	public bool ShowRewards_Quest(List<RewardData> rewards, Transform rewardBone, string title, string desc, bool fromNotice, int noticeId)
	{
		if (rewards == null || rewards.Count < 1)
		{
			Debug.LogErrorFormat("rewards is null!");
			return false;
		}
		m_rewards = rewards;
		m_fromNotice = fromNotice;
		m_noticeID = noticeId;
		m_bannerUberText.Text = title;
		m_descText.SetActive(value: true);
		m_descText.GetComponent<UberText>().Text = desc;
		ShowRewardChest();
		return true;
	}

	public void RegisterDoneCallback(Action action)
	{
		m_doneCallbacks.Add(action);
	}

	private void ShowRewardChest()
	{
		if (FullScreenFXMgr.Get() != null)
		{
			FullScreenFXMgr.Get().StartStandardBlurVignette(0.5f);
		}
		m_FSM.SendEvent("SummonIn");
		SceneUtils.SetLayer(m_rewardChest.gameObject, GameLayer.IgnoreFullScreenEffects);
		m_rewardChest.AddEventListener(UIEventType.RELEASE, ShowRewardBags);
	}

	private void ShowRewardChest_TavernBrawl()
	{
		ShowRewardChest();
		string text = ((m_wins != 0) ? GameStrings.Format("GLUE_BRAWLISEUM_REWARDS_WIN_BANNER_TEXT", m_wins, m_wins) : GameStrings.Get("GLUE_BRAWLISEUM_NO_WINS_REWARD_PACK_TEXT"));
		m_bannerUberText.Text = text;
	}

	private void ShowRewardChest_LeaguePromotion()
	{
		ShowRewardChest();
		LeagueRankDbfRecord record = GameDbf.LeagueRank.GetRecord((LeagueRankDbfRecord r) => r.LeagueId == m_leagueId && r.StarLevel == 1);
		m_bannerUberText.Text = record.RankName.GetString();
		m_descText.GetComponent<UberText>().Text = GameStrings.Get("GLUE_NEW_PLAYER_PROMOTION_CHEST_DESC");
	}

	private void ShowRewardBags(UIEvent e)
	{
		m_rewardChest.RemoveEventListener(UIEventType.RELEASE, ShowRewardBags);
		m_FSM.SendEvent("StartAnim");
	}

	private void OpenRewards()
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			if (SoundManager.Get() != null)
			{
				SoundManager.Get().LoadAndPlay("card_turn_over_legendary.prefab:a8140f686bff601459e954bc23de35e0");
			}
			RewardBoxesDisplay component = go.GetComponent<RewardBoxesDisplay>();
			component.SetRewards(m_rewards);
			component.m_playBoxFlyoutSound = false;
			component.SetLayer(GameLayer.IgnoreFullScreenEffects);
			component.UseDarkeningClickCatcher(value: true);
			component.RegisterDoneCallback(OnRewardBoxesDone);
			if (!UniversalInputManager.UsePhoneUI)
			{
				SceneUtils.SetLayer(m_rewardChest.gameObject, GameLayer.Default);
			}
			Transform rewardBoxBoneForScene = GetRewardBoxBoneForScene();
			component.transform.position = rewardBoxBoneForScene.position;
			component.transform.localRotation = rewardBoxBoneForScene.localRotation;
			component.transform.localScale = rewardBoxBoneForScene.localScale;
			component.AnimateRewards();
		};
		AssetLoader.Get().InstantiatePrefab("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback);
	}

	private void OnRewardBoxesDone()
	{
		FullScreenFXMgr.Get().DisableBlur();
		m_FSM.SendEvent("SummonOut");
		m_descText.SetActive(value: false);
		if (m_fromNotice)
		{
			Network.Get().AckNotice(m_noticeID);
		}
	}

	public void OnSummonOutAnimationDone()
	{
		foreach (Action doneCallback in m_doneCallbacks)
		{
			doneCallback?.Invoke();
		}
		UnityEngine.Object.Destroy(m_parent.gameObject);
	}

	private Transform GetRewardBoxBoneForScene()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.PACKOPENING)
		{
			return m_rewardBoxBonePackOpening;
		}
		return m_rewardBoxBone;
	}
}
