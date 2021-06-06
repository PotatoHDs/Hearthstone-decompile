using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000669 RID: 1641
public class ChestRewardDisplay : MonoBehaviour
{
	// Token: 0x06005C3D RID: 23613 RVA: 0x001DFB0C File Offset: 0x001DDD0C
	public bool ShowRewards_TavernBrawl(int wins, List<RewardData> rewards, Transform rewardBone, bool fromNotice = false, long noticeID = -1L)
	{
		if (rewards == null || rewards.Count < 1)
		{
			Debug.LogErrorFormat("rewards is null!", Array.Empty<object>());
			return false;
		}
		this.m_wins = wins;
		this.m_rewards = rewards;
		this.m_fromNotice = fromNotice;
		this.m_noticeID = noticeID;
		this.m_descText.SetActive(fromNotice);
		this.ShowRewardChest_TavernBrawl();
		return true;
	}

	// Token: 0x06005C3E RID: 23614 RVA: 0x001DFB68 File Offset: 0x001DDD68
	public bool ShowRewards_LeaguePromotion(int leagueId, List<RewardData> rewards, Transform rewardBone, bool fromNotice = false, long noticeID = -1L)
	{
		if (rewards == null || rewards.Count < 1)
		{
			Debug.LogErrorFormat("rewards is null!", Array.Empty<object>());
			return false;
		}
		this.m_leagueId = leagueId;
		this.m_rewards = rewards;
		this.m_fromNotice = fromNotice;
		this.m_noticeID = noticeID;
		this.ShowRewardChest_LeaguePromotion();
		return true;
	}

	// Token: 0x06005C3F RID: 23615 RVA: 0x001DFBB8 File Offset: 0x001DDDB8
	public bool ShowRewards_Quest(List<RewardData> rewards, Transform rewardBone, string title, string desc, bool fromNotice, int noticeId)
	{
		if (rewards == null || rewards.Count < 1)
		{
			Debug.LogErrorFormat("rewards is null!", Array.Empty<object>());
			return false;
		}
		this.m_rewards = rewards;
		this.m_fromNotice = fromNotice;
		this.m_noticeID = (long)noticeId;
		this.m_bannerUberText.Text = title;
		this.m_descText.SetActive(true);
		this.m_descText.GetComponent<UberText>().Text = desc;
		this.ShowRewardChest();
		return true;
	}

	// Token: 0x06005C40 RID: 23616 RVA: 0x001DFC2B File Offset: 0x001DDE2B
	public void RegisterDoneCallback(Action action)
	{
		this.m_doneCallbacks.Add(action);
	}

	// Token: 0x06005C41 RID: 23617 RVA: 0x001DFC3C File Offset: 0x001DDE3C
	private void ShowRewardChest()
	{
		if (FullScreenFXMgr.Get() != null)
		{
			FullScreenFXMgr.Get().StartStandardBlurVignette(0.5f);
		}
		this.m_FSM.SendEvent("SummonIn");
		SceneUtils.SetLayer(this.m_rewardChest.gameObject, GameLayer.IgnoreFullScreenEffects);
		this.m_rewardChest.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ShowRewardBags));
	}

	// Token: 0x06005C42 RID: 23618 RVA: 0x001DFC9C File Offset: 0x001DDE9C
	private void ShowRewardChest_TavernBrawl()
	{
		this.ShowRewardChest();
		string text;
		if (this.m_wins == 0)
		{
			text = GameStrings.Get("GLUE_BRAWLISEUM_NO_WINS_REWARD_PACK_TEXT");
		}
		else
		{
			text = GameStrings.Format("GLUE_BRAWLISEUM_REWARDS_WIN_BANNER_TEXT", new object[]
			{
				this.m_wins,
				this.m_wins
			});
		}
		this.m_bannerUberText.Text = text;
	}

	// Token: 0x06005C43 RID: 23619 RVA: 0x001DFD00 File Offset: 0x001DDF00
	private void ShowRewardChest_LeaguePromotion()
	{
		this.ShowRewardChest();
		LeagueRankDbfRecord record = GameDbf.LeagueRank.GetRecord((LeagueRankDbfRecord r) => r.LeagueId == this.m_leagueId && r.StarLevel == 1);
		this.m_bannerUberText.Text = record.RankName.GetString(true);
		this.m_descText.GetComponent<UberText>().Text = GameStrings.Get("GLUE_NEW_PLAYER_PROMOTION_CHEST_DESC");
	}

	// Token: 0x06005C44 RID: 23620 RVA: 0x001DFD5B File Offset: 0x001DDF5B
	private void ShowRewardBags(UIEvent e)
	{
		this.m_rewardChest.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ShowRewardBags));
		this.m_FSM.SendEvent("StartAnim");
	}

	// Token: 0x06005C45 RID: 23621 RVA: 0x001DFD88 File Offset: 0x001DDF88
	private void OpenRewards()
	{
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			if (SoundManager.Get() != null)
			{
				SoundManager.Get().LoadAndPlay("card_turn_over_legendary.prefab:a8140f686bff601459e954bc23de35e0");
			}
			RewardBoxesDisplay component = go.GetComponent<RewardBoxesDisplay>();
			component.SetRewards(this.m_rewards);
			component.m_playBoxFlyoutSound = false;
			component.SetLayer(GameLayer.IgnoreFullScreenEffects);
			component.UseDarkeningClickCatcher(true);
			component.RegisterDoneCallback(new Action(this.OnRewardBoxesDone));
			if (!UniversalInputManager.UsePhoneUI)
			{
				SceneUtils.SetLayer(this.m_rewardChest.gameObject, GameLayer.Default);
			}
			Transform rewardBoxBoneForScene = this.GetRewardBoxBoneForScene();
			component.transform.position = rewardBoxBoneForScene.position;
			component.transform.localRotation = rewardBoxBoneForScene.localRotation;
			component.transform.localScale = rewardBoxBoneForScene.localScale;
			component.AnimateRewards();
		};
		AssetLoader.Get().InstantiatePrefab("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback, null, AssetLoadingOptions.None);
	}

	// Token: 0x06005C46 RID: 23622 RVA: 0x001DFDBA File Offset: 0x001DDFBA
	private void OnRewardBoxesDone()
	{
		FullScreenFXMgr.Get().DisableBlur();
		this.m_FSM.SendEvent("SummonOut");
		this.m_descText.SetActive(false);
		if (this.m_fromNotice)
		{
			Network.Get().AckNotice(this.m_noticeID);
		}
	}

	// Token: 0x06005C47 RID: 23623 RVA: 0x001DFDFC File Offset: 0x001DDFFC
	public void OnSummonOutAnimationDone()
	{
		foreach (Action action in this.m_doneCallbacks)
		{
			if (action != null)
			{
				action();
			}
		}
		UnityEngine.Object.Destroy(this.m_parent.gameObject);
	}

	// Token: 0x06005C48 RID: 23624 RVA: 0x001DFE64 File Offset: 0x001DE064
	private Transform GetRewardBoxBoneForScene()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.PACKOPENING)
		{
			return this.m_rewardBoxBonePackOpening;
		}
		return this.m_rewardBoxBone;
	}

	// Token: 0x04004E75 RID: 20085
	public const string DEFAULT_PREFAB = "RewardChest_Lock.prefab:06ffa33e82036694e8cacb96aa7b48e8";

	// Token: 0x04004E76 RID: 20086
	public PegUIElement m_rewardChest;

	// Token: 0x04004E77 RID: 20087
	public PlayMakerFSM m_FSM;

	// Token: 0x04004E78 RID: 20088
	public Transform m_parent;

	// Token: 0x04004E79 RID: 20089
	public GameObject m_descText;

	// Token: 0x04004E7A RID: 20090
	public UberText m_bannerUberText;

	// Token: 0x04004E7B RID: 20091
	public Transform m_rewardBoxBone;

	// Token: 0x04004E7C RID: 20092
	public Transform m_rewardBoxBonePackOpening;

	// Token: 0x04004E7D RID: 20093
	private List<RewardData> m_rewards = new List<RewardData>();

	// Token: 0x04004E7E RID: 20094
	private List<Action> m_doneCallbacks = new List<Action>();

	// Token: 0x04004E7F RID: 20095
	private bool m_fromNotice;

	// Token: 0x04004E80 RID: 20096
	private long m_noticeID = -1L;

	// Token: 0x04004E81 RID: 20097
	private int m_wins;

	// Token: 0x04004E82 RID: 20098
	private int m_leagueId;
}
