using System.Collections;
using Assets;
using UnityEngine;

public class BoosterPackReward : Reward
{
	public bool m_RotateIn = true;

	public GameObject m_BoosterPackBone;

	public GameLayer m_Layer = GameLayer.IgnoreFullScreenEffects;

	public Material m_PackGlowMaterial;

	public AnimationCurve m_RotationCurve;

	private UnopenedPack m_unopenedPack;

	protected override void InitData()
	{
		SetData(new BoosterPackRewardData(), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		m_root.SetActive(value: true);
		SceneUtils.SetLayer(m_root, m_Layer);
		Vector3 localScale = m_unopenedPack.transform.localScale;
		m_unopenedPack.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(m_unopenedPack.gameObject, iTween.Hash("scale", localScale, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
		if (m_RotateIn)
		{
			PlayRotateInAnimation();
		}
	}

	protected override void HideReward()
	{
		base.HideReward();
		m_root.SetActive(value: false);
	}

	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		m_BoosterPackBone.gameObject.SetActive(value: false);
		BoosterPackRewardData boosterRewardData = base.Data as BoosterPackRewardData;
		string empty = string.Empty;
		string empty2 = string.Empty;
		string source = string.Empty;
		if (base.Data.Origin != NetCache.ProfileNotice.NoticeOrigin.OUT_OF_BAND_LICENSE)
		{
			empty = ((boosterRewardData.Count > 1) ? GameStrings.Format("GLOBAL_REWARD_BOOSTER_HEADLINE_MULTIPLE", boosterRewardData.Count) : GameStrings.Get("GLOBAL_REWARD_BOOSTER_HEADLINE_GENERIC"));
		}
		else
		{
			BoosterDbfRecord record = GameDbf.Booster.GetRecord(boosterRewardData.Id);
			if (record == null)
			{
				return;
			}
			empty = ((boosterRewardData.Count > 1) ? GameStrings.Get("GLOBAL_REWARD_BOOSTER_HEADLINE_OUT_OF_BAND_MULTI") : GameStrings.Get("GLOBAL_REWARD_BOOSTER_HEADLINE_OUT_OF_BAND"));
			SpecialEventManager specialEventManager = SpecialEventManager.Get();
			SpecialEventType eventType = SpecialEventManager.GetEventType(record.BuyWithGoldEvent);
			source = ((specialEventManager.IsEventActive(eventType, activeIfDoesNotExist: false) || !specialEventManager.GetEventStartTimeUtc(eventType).HasValue || specialEventManager.HasEventStarted(eventType)) ? GameStrings.Format("GLOBAL_REWARD_BOOSTER_DETAILS_OUT_OF_BAND", boosterRewardData.Count) : GameStrings.Format("GLOBAL_REWARD_BOOSTER_DETAILS_PRESALE_OUT_OF_BAND", boosterRewardData.Count));
		}
		SetRewardText(empty, empty2, source);
		BoosterDbfRecord record2 = GameDbf.Booster.GetRecord(boosterRewardData.Id);
		if (record2 == null)
		{
			RewardBagDbfRecord record3 = GameDbf.RewardBag.GetRecord((RewardBagDbfRecord r) => r.BagId == boosterRewardData.RewardChestBagNum.Value);
			switch (record3.Reward)
			{
			case RewardBag.Reward.LATEST_PACK:
				record2 = GameDbf.Booster.GetRecord((int)GameUtils.GetLatestRewardableBooster());
				break;
			case RewardBag.Reward.PACK_OFFSET_FROM_LATEST:
				record2 = GameDbf.Booster.GetRecord((int)GameUtils.GetRewardableBoosterOffsetFromLatest(record3.RewardData));
				break;
			}
		}
		if (record2 == null)
		{
			return;
		}
		SetReady(ready: false);
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(record2.PackOpeningPrefab, AssetLoadingOptions.IgnorePrefabPosition);
		gameObject.transform.parent = m_BoosterPackBone.transform.parent;
		gameObject.transform.position = m_BoosterPackBone.transform.position;
		gameObject.transform.rotation = m_BoosterPackBone.transform.rotation;
		gameObject.transform.localScale = m_BoosterPackBone.transform.localScale;
		m_unopenedPack = gameObject.GetComponent<UnopenedPack>();
		m_MeshRoot = gameObject;
		if (m_unopenedPack.m_SingleStack.m_MeshRenderer != null)
		{
			Texture mainTexture = m_unopenedPack.m_SingleStack.m_MeshRenderer.GetSharedMaterial().mainTexture;
			m_unopenedPack.m_SingleStack.m_MeshRenderer.SetMaterial(m_PackGlowMaterial);
			m_unopenedPack.m_SingleStack.m_MeshRenderer.GetMaterial().mainTexture = mainTexture;
			if (m_unopenedPack.m_SingleStack.m_Shadow != null)
			{
				m_unopenedPack.m_SingleStack.m_Shadow.SetActive(value: false);
			}
		}
		if (m_unopenedPack.m_MultipleStack.m_MeshRenderer != null)
		{
			Texture mainTexture2 = m_unopenedPack.m_MultipleStack.m_MeshRenderer.GetSharedMaterial().mainTexture;
			m_unopenedPack.m_MultipleStack.m_MeshRenderer.SetMaterial(m_PackGlowMaterial);
			m_unopenedPack.m_MultipleStack.m_MeshRenderer.GetMaterial().mainTexture = mainTexture2;
			if (m_unopenedPack.m_MultipleStack.m_Shadow != null)
			{
				m_unopenedPack.m_MultipleStack.m_Shadow.SetActive(value: false);
			}
		}
		UpdatePackStacks();
		SetReady(ready: true);
	}

	[ContextMenu("Play Rotate In Animation")]
	public void PlayRotateInAnimation()
	{
		StartCoroutine(RotateAnimation());
	}

	private void UpdatePackStacks()
	{
		BoosterPackRewardData boosterPackRewardData = base.Data as BoosterPackRewardData;
		if (boosterPackRewardData == null)
		{
			Debug.LogWarning($"BoosterPackReward.UpdatePackStacks() - Data {base.Data} is not CardRewardData");
			return;
		}
		NetCache.BoosterStack boosterStack = new NetCache.BoosterStack();
		boosterStack.Id = boosterPackRewardData.Id;
		boosterStack.Count = boosterPackRewardData.Count;
		m_unopenedPack.SetBoosterStack(boosterStack);
		bool flag = m_unopenedPack.CanOpenPack();
		bool flag2 = boosterPackRewardData.Count > 1;
		m_unopenedPack.m_SingleStack.m_RootObject.SetActive(!flag2 || !flag);
		m_unopenedPack.m_MultipleStack.m_RootObject.SetActive(flag2 && flag);
		m_unopenedPack.m_AmountBanner.SetActive(flag2);
		m_unopenedPack.m_AmountText.enabled = flag2;
		if (flag2)
		{
			m_unopenedPack.m_AmountText.Text = boosterPackRewardData.Count.ToString();
		}
	}

	private IEnumerator RotateAnimation()
	{
		float startTime = Time.timeSinceLevelLoad;
		while (Time.timeSinceLevelLoad - startTime < (float)m_RotationCurve.length)
		{
			float time = Time.timeSinceLevelLoad - startTime;
			m_unopenedPack.transform.localEulerAngles = new Vector3(m_unopenedPack.transform.localEulerAngles.x, m_unopenedPack.transform.localEulerAngles.y, m_RotationCurve.Evaluate(time));
			yield return null;
		}
	}
}
