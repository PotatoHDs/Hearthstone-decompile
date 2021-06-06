using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200066A RID: 1642
[CustomEditClass]
public class ClassChallengeUnlock : Reward
{
	// Token: 0x06005C4C RID: 23628 RVA: 0x001DE1F3 File Offset: 0x001DC3F3
	protected override void Awake()
	{
		base.Awake();
		if (!UniversalInputManager.UsePhoneUI)
		{
			this.m_rewardBanner.transform.localScale = this.m_rewardBanner.transform.localScale * 8f;
		}
	}

	// Token: 0x06005C4D RID: 23629 RVA: 0x001DFF90 File Offset: 0x001DE190
	public static List<AdventureMissionDbfRecord> AdventureMissionsUnlockedByWingId(int wingId)
	{
		List<AdventureMissionDbfRecord> list = new List<AdventureMissionDbfRecord>();
		foreach (AdventureMissionDbfRecord adventureMissionDbfRecord in GameDbf.AdventureMission.GetRecords())
		{
			if (adventureMissionDbfRecord.ReqWingId == wingId)
			{
				int scenarioId = adventureMissionDbfRecord.ScenarioId;
				ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
				if (record == null)
				{
					Debug.LogError(string.Format("Unable to find Scenario record with ID: {0}", scenarioId));
				}
				else if (record.ModeId == 4)
				{
					list.Add(adventureMissionDbfRecord);
				}
			}
		}
		return list;
	}

	// Token: 0x06005C4E RID: 23630 RVA: 0x001E0030 File Offset: 0x001DE230
	protected override void InitData()
	{
		base.SetData(new ClassChallengeUnlockData(), false);
	}

	// Token: 0x06005C4F RID: 23631 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected override void PlayShowSounds()
	{
	}

	// Token: 0x06005C50 RID: 23632 RVA: 0x001E0040 File Offset: 0x001DE240
	protected override void ShowReward(bool updateCacheValues)
	{
		this.m_root.SetActive(true);
		this.m_classFrameContainer.UpdatePositions();
		foreach (GameObject gameObject in this.m_classFrames)
		{
			gameObject.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
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
			iTween.RotateAdd(gameObject, args);
		}
		FullScreenFXMgr.Get().StartStandardBlurVignette(1f);
	}

	// Token: 0x06005C51 RID: 23633 RVA: 0x001E0148 File Offset: 0x001DE348
	protected override void HideReward()
	{
		base.HideReward();
		FullScreenFXMgr.Get().EndStandardBlurVignette(1f, new Action(this.DestroyClassChallengeUnlock));
		this.m_root.SetActive(false);
	}

	// Token: 0x06005C52 RID: 23634 RVA: 0x001E0178 File Offset: 0x001DE378
	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		ClassChallengeUnlockData classChallengeUnlockData = base.Data as ClassChallengeUnlockData;
		if (classChallengeUnlockData == null)
		{
			Debug.LogWarning(string.Format("ClassChallengeUnlock.OnDataSet() - Data {0} is not ClassChallengeUnlockData", base.Data));
			return;
		}
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (AdventureMissionDbfRecord adventureMissionDbfRecord in ClassChallengeUnlock.AdventureMissionsUnlockedByWingId(classChallengeUnlockData.WingID))
		{
			int scenarioId = adventureMissionDbfRecord.ScenarioId;
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
			if (record == null)
			{
				Debug.LogError(string.Format("Unable to find Scenario record with ID: {0}", scenarioId));
			}
			else if (!string.IsNullOrEmpty(adventureMissionDbfRecord.ClassChallengePrefabPopup))
			{
				DbfLocValue shortName = record.ShortName;
				list.Add(adventureMissionDbfRecord.ClassChallengePrefabPopup);
				list2.Add(shortName);
			}
			else
			{
				Debug.LogWarning(string.Format("CLASS_CHALLENGE_PREFAB_POPUP not define for AdventureMission SCENARIO_ID: {0}", scenarioId));
			}
		}
		if (list.Count == 0)
		{
			Debug.LogError(string.Format("Unable to find AdventureMission record with REQ_WING_ID: {0}.", classChallengeUnlockData.WingID));
			return;
		}
		GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[]
		{
			new GameStrings.PluralNumber
			{
				m_index = 0,
				m_number = list.Count
			}
		};
		this.m_headerText.Text = GameStrings.FormatPlurals("GLOBAL_REWARD_CLASS_CHALLENGE_HEADLINE", pluralNumbers, Array.Empty<object>());
		string headline;
		if (list.Count > 0)
		{
			headline = string.Join(", ", list2.ToArray());
		}
		else
		{
			headline = "";
		}
		string source = GameDbf.Wing.GetRecord(classChallengeUnlockData.WingID).ClassChallengeRewardSource;
		base.SetRewardText(headline, string.Empty, source);
		foreach (string input in list)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.None);
			if (!(gameObject == null))
			{
				GameUtils.SetParent(gameObject, this.m_classFrameContainer, false);
				gameObject.transform.localRotation = Quaternion.identity;
				this.m_classFrameContainer.AddObject(gameObject, true);
				this.m_classFrames.Add(gameObject);
			}
		}
		this.m_classFrameContainer.UpdatePositions();
		base.SetReady(true);
		base.EnableClickCatcher(true);
		base.RegisterClickListener(new Reward.OnClickedCallback(this.OnClicked));
	}

	// Token: 0x06005C53 RID: 23635 RVA: 0x001DD921 File Offset: 0x001DBB21
	private void OnClicked(Reward reward, object userData)
	{
		this.HideReward();
	}

	// Token: 0x06005C54 RID: 23636 RVA: 0x000ECB8B File Offset: 0x000EAD8B
	private void DestroyClassChallengeUnlock()
	{
		UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x04004E83 RID: 20099
	[CustomEditField(Sections = "Container")]
	public UIBObjectSpacing m_classFrameContainer;

	// Token: 0x04004E84 RID: 20100
	[CustomEditField(Sections = "Text Settings")]
	public UberText m_headerText;

	// Token: 0x04004E85 RID: 20101
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_appearSound;

	// Token: 0x04004E86 RID: 20102
	private List<GameObject> m_classFrames = new List<GameObject>();
}
