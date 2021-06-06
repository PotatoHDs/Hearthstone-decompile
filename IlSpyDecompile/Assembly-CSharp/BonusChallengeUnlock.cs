using System.Collections;
using UnityEngine;

[CustomEditClass]
public class BonusChallengeUnlock : Reward
{
	[CustomEditField(Sections = "Container")]
	public UIBObjectSpacing m_cardContainer;

	[CustomEditField(Sections = "Text Settings")]
	public UberText m_headerText;

	private Actor m_bonusChallengeBossActor;

	protected override void Awake()
	{
		base.Awake();
		if (!UniversalInputManager.UsePhoneUI)
		{
			m_rewardBanner.transform.localScale = m_rewardBanner.transform.localScale * 8f;
		}
	}

	protected override void InitData()
	{
		SetData(new BonusChallengeUnlockData(), updateVisuals: false);
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		m_root.SetActive(value: true);
		m_cardContainer.UpdatePositions();
		m_cardContainer.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
		Hashtable args = iTween.Hash("amount", new Vector3(0f, 0f, 540f), "time", 1.5f, "easeType", iTween.EaseType.easeOutElastic, "space", Space.Self);
		iTween.RotateAdd(m_cardContainer.gameObject, args);
		FullScreenFXMgr.Get().StartStandardBlurVignette(1f);
	}

	protected override void HideReward()
	{
		base.HideReward();
		FullScreenFXMgr.Get().EndStandardBlurVignette(1f, DestroyBonusChallengeUnlock);
		m_root.SetActive(value: false);
	}

	protected override void OnDataSet(bool updateVisuals)
	{
		if (updateVisuals)
		{
			BonusChallengeUnlockData bonusChallengeUnlockData = base.Data as BonusChallengeUnlockData;
			if (bonusChallengeUnlockData == null)
			{
				Debug.LogWarning($"BonusChallengeUnlock.OnDataSet() - Data {base.Data} is not BonusChallengeUnlockData");
				return;
			}
			BannerManager.Get().ShowBanner(bonusChallengeUnlockData.PrefabToDisplay, null, GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_INTRO_BANNER_BUTTON"), HideReward);
			EnableClickCatcher(enabled: true);
		}
	}

	private void DestroyBonusChallengeUnlock()
	{
		Object.DestroyImmediate(base.gameObject);
	}
}
