using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200004A RID: 74
[CustomEditClass]
public class AdventureRewardsDisplayArea : MonoBehaviour
{
	// Token: 0x06000414 RID: 1044 RVA: 0x000188D8 File Offset: 0x00016AD8
	private void Awake()
	{
		if (this.m_FullscreenModeOffClicker != null)
		{
			this.m_FullscreenModeOffClicker.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
			{
				this.HideRewards();
			});
		}
		if (this.m_FullscreenDisableScrollBar != null)
		{
			this.m_FullscreenDisableScrollBar.AddTouchScrollStartedListener(new UIBScrollable.OnTouchScrollStarted(this.HideRewards));
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00018931 File Offset: 0x00016B31
	private void OnDestroy()
	{
		this.DisableFullscreen();
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00018939 File Offset: 0x00016B39
	public bool IsShowing()
	{
		return this.m_Showing;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00018941 File Offset: 0x00016B41
	public void ShowRewardsNoFullscreen(List<RewardData> rewards, Vector3 finalPosition, Vector3? origin = null)
	{
		this.DoShowRewards(rewards, new Vector3?(finalPosition), origin, true);
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x00018954 File Offset: 0x00016B54
	public void ShowRewards(List<RewardData> rewards, Vector3 finalPosition, Vector3? origin = null)
	{
		if (this.m_Showing)
		{
			return;
		}
		this.m_Showing = true;
		if (this.m_EnableFullscreenMode)
		{
			this.DoShowRewards(rewards, null, origin, false);
			return;
		}
		this.DoShowRewards(rewards, new Vector3?(finalPosition), origin, false);
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0001899C File Offset: 0x00016B9C
	public void HideRewards()
	{
		this.m_Showing = false;
		foreach (GameObject gameObject in this.m_CurrentRewards)
		{
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		this.m_CurrentRewards.Clear();
		this.DisableFullscreen();
		this.FireRewardsHiddenEvent();
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00018A18 File Offset: 0x00016C18
	public void AddRewardsHiddenListener(AdventureRewardsDisplayArea.RewardsHidden dlg)
	{
		this.m_RewardsHiddenListeners.Add(dlg);
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00018A26 File Offset: 0x00016C26
	public void RemoveRewardsHiddenListener(AdventureRewardsDisplayArea.RewardsHidden dlg)
	{
		this.m_RewardsHiddenListeners.Remove(dlg);
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00018A35 File Offset: 0x00016C35
	public List<GameObject> GetCurrentShownRewards()
	{
		if (!this.m_Showing)
		{
			return null;
		}
		return this.m_CurrentRewards;
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00018A48 File Offset: 0x00016C48
	private void FireRewardsHiddenEvent()
	{
		AdventureRewardsDisplayArea.RewardsHidden[] array = this.m_RewardsHiddenListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x00018A78 File Offset: 0x00016C78
	private void DoShowRewards(ICollection<RewardData> rewards, Vector3? finalPosition, Vector3? origin, bool disableFullscreen)
	{
		int num = 0;
		int count = rewards.Count;
		Vector3 positionOffset = this.m_RewardsDefaultOffset;
		Vector3 scale = Vector3.one;
		foreach (RewardData rewardData in rewards)
		{
			GameObject gameObject = null;
			Reward.Type rewardType = rewardData.RewardType;
			switch (rewardType)
			{
			case Reward.Type.BOOSTER_PACK:
			{
				BoosterDbfRecord record = GameDbf.Booster.GetRecord(((BoosterPackRewardData)rewardData).Id);
				if (record == null)
				{
					continue;
				}
				gameObject = AssetLoader.Get().InstantiatePrefab(record.PackOpeningPrefab, AssetLoadingOptions.IgnorePrefabPosition);
				scale = this.m_RewardsBoosterScale;
				UnopenedPack component = gameObject.GetComponent<UnopenedPack>();
				component.SetBoosterStack(new NetCache.BoosterStack
				{
					Id = ((BoosterPackRewardData)rewardData).Id,
					Count = ((BoosterPackRewardData)rewardData).Count
				});
				component.GetComponent<Collider>().enabled = false;
				break;
			}
			case Reward.Type.CARD:
			{
				string cardID = ((CardRewardData)rewardData).CardID;
				using (DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(cardID, null))
				{
					bool flag = fullDef.EntityDef.IsHeroSkin();
					string input = flag ? ActorNames.GetHeroSkinOrHandActor(fullDef.EntityDef.GetCardType(), TAG_PREMIUM.NORMAL) : ActorNames.GetHandActor(fullDef.EntityDef, TAG_PREMIUM.NORMAL);
					gameObject = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.IgnorePrefabPosition);
					gameObject.GetComponentInChildren<Collider>().enabled = false;
					Actor component2 = gameObject.GetComponent<Actor>();
					component2.SetFullDef(fullDef);
					component2.CreateBannedRibbon();
					if (flag)
					{
						gameObject.GetComponent<CollectionHeroSkin>().SetClass(fullDef.EntityDef.GetClass());
						scale = this.m_RewardsHeroSkinScale;
						positionOffset = this.m_RewardsHeroSkinOffset;
					}
					else
					{
						scale = this.m_RewardsCardScale;
						positionOffset = this.m_RewardsDefaultOffset;
					}
					if (component2.m_cardMesh != null)
					{
						BoxCollider component3 = component2.m_cardMesh.GetComponent<BoxCollider>();
						if (component3 != null)
						{
							component3.enabled = false;
						}
					}
					break;
				}
				goto IL_15C;
			}
			case Reward.Type.CARD_BACK:
				goto IL_15C;
			default:
				if (rewardType == Reward.Type.RANDOM_CARD)
				{
					gameObject = AssetLoader.Get().InstantiatePrefab("Card_Random_Reward.prefab:403211800142ebf4593a290b92655167", AssetLoadingOptions.IgnorePrefabPosition);
					scale = this.m_RewardsCardScale;
				}
				break;
			}
			IL_232:
			if (!(gameObject == null))
			{
				this.m_CurrentRewards.Add(gameObject);
				GameUtils.SetParent(gameObject, this.m_RewardsCardArea, false);
				this.ShowRewardsObject(gameObject, finalPosition, origin, positionOffset, scale, num, count);
				num++;
				continue;
			}
			continue;
			IL_15C:
			CardBackManager.LoadCardBackData loadCardBackData = CardBackManager.Get().LoadCardBackByIndex(((CardBackRewardData)rewardData).CardBackID, false, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", false);
			scale = this.m_RewardsCardBackScale;
			gameObject = loadCardBackData.m_GameObject;
			goto IL_232;
		}
		this.EnableFullscreen(disableFullscreen);
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00018D44 File Offset: 0x00016F44
	private void ShowRewardsObject(GameObject obj, Vector3? finalPosition, Vector3? origin, Vector3 positionOffset, Vector3 scale, int index, int totalCount)
	{
		Vector3 vector;
		if (finalPosition != null)
		{
			Vector3 min = this.m_RewardsCardArea.GetComponent<Collider>().bounds.min;
			Vector3 max = this.m_RewardsCardArea.GetComponent<Collider>().bounds.max;
			vector = finalPosition.Value + positionOffset;
			float num = (float)index * this.m_RewardsDefaultSpacing;
			vector.z = Mathf.Clamp(vector.z, min.z, max.z);
			if (vector.x + this.m_RewardsCardMouseOffset > max.x)
			{
				vector.x -= this.m_RewardsCardMouseOffset + num;
			}
			else
			{
				vector.x += this.m_RewardsCardMouseOffset + num;
			}
		}
		else
		{
			vector = this.m_RewardsCardArea.transform.position + positionOffset;
			float num2 = (float)index * this.m_RewardsDefaultSpacing;
			vector.x += num2;
			vector.x -= (float)(totalCount - 1) * this.m_RewardsDefaultSpacing * 0.5f;
		}
		obj.transform.localScale = scale;
		obj.transform.position = vector;
		obj.SetActive(true);
		if (this.m_EnableFullscreenMode)
		{
			SceneUtils.SetLayer(obj, GameLayer.IgnoreFullScreenEffects);
			if (this.m_FullscreenModeOffClicker != null)
			{
				SceneUtils.SetLayer(this.m_FullscreenModeOffClicker, GameLayer.IgnoreFullScreenEffects);
			}
		}
		iTween.StopByName(obj, "REWARD_SCALE_UP");
		iTween.ScaleFrom(obj, iTween.Hash(new object[]
		{
			"scale",
			Vector3.one * 0.05f,
			"time",
			0.15f,
			"easeType",
			iTween.EaseType.easeOutQuart,
			"name",
			"REWARD_SCALE_UP"
		}));
		if (origin != null)
		{
			iTween.StopByName(obj, "REWARD_MOVE_FROM_ORIGIN");
			iTween.MoveFrom(obj, iTween.Hash(new object[]
			{
				"position",
				origin.Value,
				"time",
				0.15f,
				"easeType",
				iTween.EaseType.easeOutQuart,
				"name",
				"REWARD_MOVE_FROM_ORIGIN",
				"oncomplete",
				new Action<object>(delegate(object o)
				{
					if (this.m_RewardsCardDriftAmount != Vector3.zero)
					{
						AnimationUtil.DriftObject(obj, this.m_RewardsCardDriftAmount);
					}
				})
			}));
		}
		else if (this.m_RewardsCardDriftAmount != Vector3.zero)
		{
			AnimationUtil.DriftObject(obj, this.m_RewardsCardDriftAmount);
		}
		if (!string.IsNullOrEmpty(this.m_CardPreviewAppearSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_CardPreviewAppearSound);
		}
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00019028 File Offset: 0x00017228
	private void EnableFullscreen(bool disableFullscreen)
	{
		if (this.m_EnableFullscreenMode && !disableFullscreen)
		{
			FullScreenFXMgr.Get().StartStandardBlurVignette(0.25f);
			if (this.m_FullscreenModeOffClicker != null)
			{
				this.m_FullscreenModeOffClicker.gameObject.SetActive(true);
			}
			this.m_FullscreenEnabled = true;
		}
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00019078 File Offset: 0x00017278
	private void DisableFullscreen()
	{
		if (!this.m_FullscreenEnabled)
		{
			return;
		}
		if (FullScreenFXMgr.Get() != null)
		{
			FullScreenFXMgr.Get().EndStandardBlurVignette(0.25f, null);
		}
		if (this.m_FullscreenModeOffClicker != null)
		{
			this.m_FullscreenModeOffClicker.gameObject.SetActive(false);
		}
		this.m_FullscreenEnabled = false;
	}

	// Token: 0x040002D1 RID: 721
	[CustomEditField(Sections = "UI")]
	public GameObject m_RewardsCardArea;

	// Token: 0x040002D2 RID: 722
	[CustomEditField(Sections = "UI")]
	public Vector3 m_RewardsDefaultOffset;

	// Token: 0x040002D3 RID: 723
	[CustomEditField(Sections = "UI")]
	public Vector3 m_RewardsHeroSkinOffset;

	// Token: 0x040002D4 RID: 724
	[CustomEditField(Sections = "UI")]
	public float m_RewardsCardMouseOffset;

	// Token: 0x040002D5 RID: 725
	[CustomEditField(Sections = "UI")]
	public Vector3 m_RewardsCardScale;

	// Token: 0x040002D6 RID: 726
	[CustomEditField(Sections = "UI")]
	public Vector3 m_RewardsHeroSkinScale;

	// Token: 0x040002D7 RID: 727
	[CustomEditField(Sections = "UI")]
	public Vector3 m_RewardsCardBackScale;

	// Token: 0x040002D8 RID: 728
	[CustomEditField(Sections = "UI")]
	public Vector3 m_RewardsBoosterScale;

	// Token: 0x040002D9 RID: 729
	[CustomEditField(Sections = "UI")]
	public float m_RewardsDefaultSpacing = 10f;

	// Token: 0x040002DA RID: 730
	[CustomEditField(Sections = "UI")]
	public Vector3 m_RewardsCardDriftAmount;

	// Token: 0x040002DB RID: 731
	[CustomEditField(Sections = "UI")]
	public bool m_EnableFullscreenMode;

	// Token: 0x040002DC RID: 732
	[CustomEditField(Sections = "UI", Parent = "m_EnableFullscreenMode")]
	public PegUIElement m_FullscreenModeOffClicker;

	// Token: 0x040002DD RID: 733
	[CustomEditField(Sections = "UI", Parent = "m_EnableFullscreenMode")]
	public UIBScrollable m_FullscreenDisableScrollBar;

	// Token: 0x040002DE RID: 734
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_CardPreviewAppearSound;

	// Token: 0x040002DF RID: 735
	private List<GameObject> m_CurrentRewards = new List<GameObject>();

	// Token: 0x040002E0 RID: 736
	private bool m_FullscreenEnabled;

	// Token: 0x040002E1 RID: 737
	private bool m_Showing;

	// Token: 0x040002E2 RID: 738
	private List<AdventureRewardsDisplayArea.RewardsHidden> m_RewardsHiddenListeners = new List<AdventureRewardsDisplayArea.RewardsHidden>();

	// Token: 0x02001312 RID: 4882
	// (Invoke) Token: 0x0600D66B RID: 54891
	public delegate void RewardsHidden();
}
