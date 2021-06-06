using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000660 RID: 1632
[CustomEditClass]
public class BonusChallengeUnlock : Reward
{
	// Token: 0x06005BE8 RID: 23528 RVA: 0x001DE1F3 File Offset: 0x001DC3F3
	protected override void Awake()
	{
		base.Awake();
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_rewardBanner.transform.localScale = this.m_rewardBanner.transform.localScale * 8f;
		}
	}

	// Token: 0x06005BE9 RID: 23529 RVA: 0x001DE231 File Offset: 0x001DC431
	protected override void InitData()
	{
		base.SetData(new BonusChallengeUnlockData(), false);
	}

	// Token: 0x06005BEA RID: 23530 RVA: 0x001DE240 File Offset: 0x001DC440
	protected override void ShowReward(bool updateCacheValues)
	{
		this.m_root.SetActive(true);
		this.m_cardContainer.UpdatePositions();
		this.m_cardContainer.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		Hashtable args = iTween.Hash(new object[]
		{
			"amount",
			new Vector3(0f, 0f, 540f),
			"time",
			1.5f,
			"easeType",
			iTween.EaseType.easeOutElastic,
			"space",
			Space.Self
		});
		iTween.RotateAdd(this.m_cardContainer.gameObject, args);
		FullScreenFXMgr.Get().StartStandardBlurVignette(1f);
	}

	// Token: 0x06005BEB RID: 23531 RVA: 0x001DE310 File Offset: 0x001DC510
	protected override void HideReward()
	{
		base.HideReward();
		FullScreenFXMgr.Get().EndStandardBlurVignette(1f, new Action(this.DestroyBonusChallengeUnlock));
		this.m_root.SetActive(false);
	}

	// Token: 0x06005BEC RID: 23532 RVA: 0x001DE340 File Offset: 0x001DC540
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		BonusChallengeUnlockData bonusChallengeUnlockData = base.Data as BonusChallengeUnlockData;
		if (bonusChallengeUnlockData == null)
		{
			Debug.LogWarning(string.Format("BonusChallengeUnlock.OnDataSet() - Data {0} is not BonusChallengeUnlockData", base.Data));
			return;
		}
		BannerManager.Get().ShowBanner(bonusChallengeUnlockData.PrefabToDisplay, null, GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_INTRO_BANNER_BUTTON"), new BannerManager.DelOnCloseBanner(this.HideReward), null);
		base.EnableClickCatcher(true);
	}

	// Token: 0x06005BED RID: 23533 RVA: 0x000ECB8B File Offset: 0x000EAD8B
	private void DestroyBonusChallengeUnlock()
	{
		UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x04004E53 RID: 20051
	[CustomEditField(Sections = "Container")]
	public UIBObjectSpacing m_cardContainer;

	// Token: 0x04004E54 RID: 20052
	[CustomEditField(Sections = "Text Settings")]
	public UberText m_headerText;

	// Token: 0x04004E55 RID: 20053
	private Actor m_bonusChallengeBossActor;
}
