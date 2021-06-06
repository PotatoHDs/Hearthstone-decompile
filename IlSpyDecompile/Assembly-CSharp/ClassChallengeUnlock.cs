using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class ClassChallengeUnlock : Reward
{
	[CustomEditField(Sections = "Container")]
	public UIBObjectSpacing m_classFrameContainer;

	[CustomEditField(Sections = "Text Settings")]
	public UberText m_headerText;

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_appearSound;

	private List<GameObject> m_classFrames = new List<GameObject>();

	protected override void Awake()
	{
		base.Awake();
		if (!UniversalInputManager.UsePhoneUI)
		{
			m_rewardBanner.transform.localScale = m_rewardBanner.transform.localScale * 8f;
		}
	}

	public static List<AdventureMissionDbfRecord> AdventureMissionsUnlockedByWingId(int wingId)
	{
		List<AdventureMissionDbfRecord> list = new List<AdventureMissionDbfRecord>();
		foreach (AdventureMissionDbfRecord record2 in GameDbf.AdventureMission.GetRecords())
		{
			if (record2.ReqWingId == wingId)
			{
				int scenarioId = record2.ScenarioId;
				ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
				if (record == null)
				{
					Debug.LogError($"Unable to find Scenario record with ID: {scenarioId}");
				}
				else if (record.ModeId == 4)
				{
					list.Add(record2);
				}
			}
		}
		return list;
	}

	protected override void InitData()
	{
		SetData(new ClassChallengeUnlockData(), updateVisuals: false);
	}

	protected override void PlayShowSounds()
	{
	}

	protected override void ShowReward(bool updateCacheValues)
	{
		m_root.SetActive(value: true);
		m_classFrameContainer.UpdatePositions();
		foreach (GameObject classFrame in m_classFrames)
		{
			classFrame.transform.localEulerAngles = new Vector3(0f, 0f, 180f);
			Hashtable args = iTween.Hash("amount", new Vector3(0f, 0f, 540f), "time", 1.5f, "easeType", iTween.EaseType.easeOutElastic, "space", Space.Self);
			iTween.RotateAdd(classFrame, args);
		}
		FullScreenFXMgr.Get().StartStandardBlurVignette(1f);
	}

	protected override void HideReward()
	{
		base.HideReward();
		FullScreenFXMgr.Get().EndStandardBlurVignette(1f, DestroyClassChallengeUnlock);
		m_root.SetActive(value: false);
	}

	protected override void OnDataSet(bool updateVisuals)
	{
		if (!updateVisuals)
		{
			return;
		}
		ClassChallengeUnlockData classChallengeUnlockData = base.Data as ClassChallengeUnlockData;
		if (classChallengeUnlockData == null)
		{
			Debug.LogWarning($"ClassChallengeUnlock.OnDataSet() - Data {base.Data} is not ClassChallengeUnlockData");
			return;
		}
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (AdventureMissionDbfRecord item in AdventureMissionsUnlockedByWingId(classChallengeUnlockData.WingID))
		{
			int scenarioId = item.ScenarioId;
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(scenarioId);
			if (record == null)
			{
				Debug.LogError($"Unable to find Scenario record with ID: {scenarioId}");
			}
			else if (!string.IsNullOrEmpty(item.ClassChallengePrefabPopup))
			{
				DbfLocValue shortName = record.ShortName;
				list.Add(item.ClassChallengePrefabPopup);
				list2.Add(shortName);
			}
			else
			{
				Debug.LogWarning($"CLASS_CHALLENGE_PREFAB_POPUP not define for AdventureMission SCENARIO_ID: {scenarioId}");
			}
		}
		if (list.Count == 0)
		{
			Debug.LogError($"Unable to find AdventureMission record with REQ_WING_ID: {classChallengeUnlockData.WingID}.");
			return;
		}
		GameStrings.PluralNumber[] pluralNumbers = new GameStrings.PluralNumber[1]
		{
			new GameStrings.PluralNumber
			{
				m_index = 0,
				m_number = list.Count
			}
		};
		m_headerText.Text = GameStrings.FormatPlurals("GLOBAL_REWARD_CLASS_CHALLENGE_HEADLINE", pluralNumbers);
		string headline = ((list.Count <= 0) ? "" : string.Join(", ", list2.ToArray()));
		string source = GameDbf.Wing.GetRecord(classChallengeUnlockData.WingID).ClassChallengeRewardSource;
		SetRewardText(headline, string.Empty, source);
		foreach (string item2 in list)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(item2);
			if (!(gameObject == null))
			{
				GameUtils.SetParent(gameObject, m_classFrameContainer);
				gameObject.transform.localRotation = Quaternion.identity;
				m_classFrameContainer.AddObject(gameObject);
				m_classFrames.Add(gameObject);
			}
		}
		m_classFrameContainer.UpdatePositions();
		SetReady(ready: true);
		EnableClickCatcher(enabled: true);
		RegisterClickListener(OnClicked);
	}

	private void OnClicked(Reward reward, object userData)
	{
		HideReward();
	}

	private void DestroyClassChallengeUnlock()
	{
		Object.DestroyImmediate(base.gameObject);
	}
}
