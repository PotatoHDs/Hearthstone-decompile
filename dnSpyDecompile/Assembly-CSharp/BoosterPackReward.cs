using System;
using System.Collections;
using Assets;
using UnityEngine;

// Token: 0x02000662 RID: 1634
public class BoosterPackReward : Reward
{
	// Token: 0x06005BF7 RID: 23543 RVA: 0x001DE422 File Offset: 0x001DC622
	protected override void InitData()
	{
		base.SetData(new BoosterPackRewardData(), false);
	}

	// Token: 0x06005BF8 RID: 23544 RVA: 0x001DE430 File Offset: 0x001DC630
	protected override void ShowReward(bool updateCacheValues)
	{
		this.m_root.SetActive(true);
		SceneUtils.SetLayer(this.m_root, this.m_Layer);
		Vector3 localScale = this.m_unopenedPack.transform.localScale;
		this.m_unopenedPack.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(this.m_unopenedPack.gameObject, iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.5f,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
		if (this.m_RotateIn)
		{
			this.PlayRotateInAnimation();
		}
	}

	// Token: 0x06005BF9 RID: 23545 RVA: 0x001DDEDB File Offset: 0x001DC0DB
	protected override void HideReward()
	{
		base.HideReward();
		this.m_root.SetActive(false);
	}

	// Token: 0x06005BFA RID: 23546 RVA: 0x001DE4F0 File Offset: 0x001DC6F0
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		this.m_BoosterPackBone.gameObject.SetActive(false);
		BoosterPackRewardData boosterRewardData = base.Data as BoosterPackRewardData;
		string headline = string.Empty;
		string empty = string.Empty;
		string source = string.Empty;
		if (base.Data.Origin == NetCache.ProfileNotice.NoticeOrigin.OUT_OF_BAND_LICENSE)
		{
			BoosterDbfRecord record = GameDbf.Booster.GetRecord(boosterRewardData.Id);
			if (record == null)
			{
				return;
			}
			if (boosterRewardData.Count <= 1)
			{
				headline = GameStrings.Get("GLOBAL_REWARD_BOOSTER_HEADLINE_OUT_OF_BAND");
			}
			else
			{
				headline = GameStrings.Get("GLOBAL_REWARD_BOOSTER_HEADLINE_OUT_OF_BAND_MULTI");
			}
			SpecialEventManager specialEventManager = SpecialEventManager.Get();
			SpecialEventType eventType = SpecialEventManager.GetEventType(record.BuyWithGoldEvent);
			if (!specialEventManager.IsEventActive(eventType, false) && specialEventManager.GetEventStartTimeUtc(eventType) != null && !specialEventManager.HasEventStarted(eventType))
			{
				source = GameStrings.Format("GLOBAL_REWARD_BOOSTER_DETAILS_PRESALE_OUT_OF_BAND", new object[]
				{
					boosterRewardData.Count
				});
			}
			else
			{
				source = GameStrings.Format("GLOBAL_REWARD_BOOSTER_DETAILS_OUT_OF_BAND", new object[]
				{
					boosterRewardData.Count
				});
			}
		}
		else if (boosterRewardData.Count <= 1)
		{
			headline = GameStrings.Get("GLOBAL_REWARD_BOOSTER_HEADLINE_GENERIC");
		}
		else
		{
			headline = GameStrings.Format("GLOBAL_REWARD_BOOSTER_HEADLINE_MULTIPLE", new object[]
			{
				boosterRewardData.Count
			});
		}
		base.SetRewardText(headline, empty, source);
		BoosterDbfRecord record2 = GameDbf.Booster.GetRecord(boosterRewardData.Id);
		if (record2 == null)
		{
			RewardBagDbfRecord record3 = GameDbf.RewardBag.GetRecord((RewardBagDbfRecord r) => r.BagId == boosterRewardData.RewardChestBagNum.Value);
			RewardBag.Reward reward = record3.Reward;
			if (reward != RewardBag.Reward.LATEST_PACK)
			{
				if (reward == RewardBag.Reward.PACK_OFFSET_FROM_LATEST)
				{
					record2 = GameDbf.Booster.GetRecord((int)GameUtils.GetRewardableBoosterOffsetFromLatest(record3.RewardData));
				}
			}
			else
			{
				record2 = GameDbf.Booster.GetRecord((int)GameUtils.GetLatestRewardableBooster());
			}
		}
		if (record2 != null)
		{
			base.SetReady(false);
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(record2.PackOpeningPrefab, AssetLoadingOptions.IgnorePrefabPosition);
			gameObject.transform.parent = this.m_BoosterPackBone.transform.parent;
			gameObject.transform.position = this.m_BoosterPackBone.transform.position;
			gameObject.transform.rotation = this.m_BoosterPackBone.transform.rotation;
			gameObject.transform.localScale = this.m_BoosterPackBone.transform.localScale;
			this.m_unopenedPack = gameObject.GetComponent<UnopenedPack>();
			this.m_MeshRoot = gameObject;
			if (this.m_unopenedPack.m_SingleStack.m_MeshRenderer != null)
			{
				Texture mainTexture = this.m_unopenedPack.m_SingleStack.m_MeshRenderer.GetSharedMaterial().mainTexture;
				this.m_unopenedPack.m_SingleStack.m_MeshRenderer.SetMaterial(this.m_PackGlowMaterial);
				this.m_unopenedPack.m_SingleStack.m_MeshRenderer.GetMaterial().mainTexture = mainTexture;
				if (this.m_unopenedPack.m_SingleStack.m_Shadow != null)
				{
					this.m_unopenedPack.m_SingleStack.m_Shadow.SetActive(false);
				}
			}
			if (this.m_unopenedPack.m_MultipleStack.m_MeshRenderer != null)
			{
				Texture mainTexture2 = this.m_unopenedPack.m_MultipleStack.m_MeshRenderer.GetSharedMaterial().mainTexture;
				this.m_unopenedPack.m_MultipleStack.m_MeshRenderer.SetMaterial(this.m_PackGlowMaterial);
				this.m_unopenedPack.m_MultipleStack.m_MeshRenderer.GetMaterial().mainTexture = mainTexture2;
				if (this.m_unopenedPack.m_MultipleStack.m_Shadow != null)
				{
					this.m_unopenedPack.m_MultipleStack.m_Shadow.SetActive(false);
				}
			}
			this.UpdatePackStacks();
			base.SetReady(true);
		}
	}

	// Token: 0x06005BFB RID: 23547 RVA: 0x001DE8C9 File Offset: 0x001DCAC9
	[ContextMenu("Play Rotate In Animation")]
	public void PlayRotateInAnimation()
	{
		base.StartCoroutine(this.RotateAnimation());
	}

	// Token: 0x06005BFC RID: 23548 RVA: 0x001DE8D8 File Offset: 0x001DCAD8
	private void UpdatePackStacks()
	{
		BoosterPackRewardData boosterPackRewardData = base.Data as BoosterPackRewardData;
		if (boosterPackRewardData == null)
		{
			Debug.LogWarning(string.Format("BoosterPackReward.UpdatePackStacks() - Data {0} is not CardRewardData", base.Data));
			return;
		}
		NetCache.BoosterStack boosterStack = new NetCache.BoosterStack();
		boosterStack.Id = boosterPackRewardData.Id;
		boosterStack.Count = boosterPackRewardData.Count;
		this.m_unopenedPack.SetBoosterStack(boosterStack);
		bool flag = this.m_unopenedPack.CanOpenPack();
		bool flag2 = boosterPackRewardData.Count > 1;
		this.m_unopenedPack.m_SingleStack.m_RootObject.SetActive(!flag2 || !flag);
		this.m_unopenedPack.m_MultipleStack.m_RootObject.SetActive(flag2 && flag);
		this.m_unopenedPack.m_AmountBanner.SetActive(flag2);
		this.m_unopenedPack.m_AmountText.enabled = flag2;
		if (flag2)
		{
			this.m_unopenedPack.m_AmountText.Text = boosterPackRewardData.Count.ToString();
		}
	}

	// Token: 0x06005BFD RID: 23549 RVA: 0x001DE9C5 File Offset: 0x001DCBC5
	private IEnumerator RotateAnimation()
	{
		float startTime = Time.timeSinceLevelLoad;
		while (Time.timeSinceLevelLoad - startTime < (float)this.m_RotationCurve.length)
		{
			float time = Time.timeSinceLevelLoad - startTime;
			this.m_unopenedPack.transform.localEulerAngles = new Vector3(this.m_unopenedPack.transform.localEulerAngles.x, this.m_unopenedPack.transform.localEulerAngles.y, this.m_RotationCurve.Evaluate(time));
			yield return null;
		}
		yield break;
	}

	// Token: 0x04004E58 RID: 20056
	public bool m_RotateIn = true;

	// Token: 0x04004E59 RID: 20057
	public GameObject m_BoosterPackBone;

	// Token: 0x04004E5A RID: 20058
	public GameLayer m_Layer = GameLayer.IgnoreFullScreenEffects;

	// Token: 0x04004E5B RID: 20059
	public Material m_PackGlowMaterial;

	// Token: 0x04004E5C RID: 20060
	public AnimationCurve m_RotationCurve;

	// Token: 0x04004E5D RID: 20061
	private UnopenedPack m_unopenedPack;
}
