using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CustomEditClass]
public class RewardBoxesDisplay : MonoBehaviour
{
	[Serializable]
	public class RewardPackageData
	{
		public Transform m_StartBone;

		public Transform m_TargetBone;

		public float m_StartDelay;
	}

	[Serializable]
	public class RewardSet
	{
		public GameObject m_RewardPackage;

		public float m_AnimationTime = 1f;

		public GameObject m_RewardCard;

		public GameObject m_RewardCardBack;

		public GameObject m_RewardGold;

		public GameObject m_RewardDust;

		public int m_MaxPackagesPerPage;

		public List<BoxRewardData> m_RewardData;
	}

	[Serializable]
	public class BoxRewardData
	{
		public List<RewardPackageData> m_PackageData;
	}

	public class RewardBoxData
	{
		public GameObject m_GameObject;

		public RewardPackage m_RewardPackage;

		public PlayMakerFSM m_FSM;

		public int m_Index;
	}

	public class RewardCardLoadData
	{
		public EntityDef m_EntityDef;

		public Transform m_ParentTransform;

		public CardRewardData m_CardRewardData;
	}

	private enum Events
	{
		GVG_PROMOTION
	}

	public bool m_playBoxFlyoutSound = true;

	public const string DEFAULT_PREFAB = "RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267";

	public GameObject m_Root;

	public GameObject m_ClickCatcher;

	[CustomEditField(Sections = "Reward Panel")]
	public NormalButton m_DoneButton;

	public RewardSet m_RewardSet;

	private List<Action> m_doneCallbacks;

	private List<GameObject> m_InstancedObjects;

	private GameObject[] m_RewardObjects;

	private List<RewardPackageData> m_RewardPackages;

	private GameLayer m_layer = GameLayer.IgnoreFullScreenEffects;

	private bool m_useDarkeningClickCatcher;

	private bool m_doneButtonFinishedShown;

	private bool m_destroyed;

	private List<RewardData> m_rewards;

	private int m_currentPageNum;

	private int m_lastPageNum;

	private List<GameObject> m_rewardPackageInstances = new List<GameObject>();

	private bool m_hasFadedFullScreenEffectsOut;

	private static RewardBoxesDisplay s_Instance;

	private bool IsOnLastPage => m_currentPageNum >= m_lastPageNum;

	private List<RewardData> CurrentPageRewards => m_rewards.Skip(m_RewardSet.m_MaxPackagesPerPage * m_currentPageNum).Take(m_RewardSet.m_MaxPackagesPerPage).ToList();

	public bool IsClosing { get; private set; }

	private void Awake()
	{
		s_Instance = this;
		m_InstancedObjects = new List<GameObject>();
		m_doneCallbacks = new List<Action>();
		RenderUtils.SetAlpha(m_ClickCatcher, 0f);
	}

	private void Start()
	{
		if (m_RewardSet.m_RewardPackage != null)
		{
			m_RewardSet.m_RewardPackage.SetActive(value: false);
		}
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
		CleanUp();
		m_destroyed = true;
	}

	private void OnEnable()
	{
	}

	public static RewardBoxesDisplay Get()
	{
		return s_Instance;
	}

	public void SetRewards(List<RewardData> rewards)
	{
		m_rewards = rewards;
		int result;
		int num = Math.DivRem(m_rewards.Count, m_RewardSet.m_MaxPackagesPerPage, out result);
		if (result > 0)
		{
			num++;
		}
		m_lastPageNum = num - 1;
	}

	public void UseDarkeningClickCatcher(bool value)
	{
		m_useDarkeningClickCatcher = value;
	}

	public void RegisterDoneCallback(Action action)
	{
		m_doneCallbacks.Add(action);
	}

	public List<RewardPackageData> GetPackageData(int rewardCount)
	{
		for (int i = 0; i < m_RewardSet.m_RewardData.Count; i++)
		{
			if (m_RewardSet.m_RewardData[i].m_PackageData.Count == rewardCount)
			{
				return m_RewardSet.m_RewardData[i].m_PackageData;
			}
		}
		Debug.LogError("RewardBoxesDisplay: GetPackageData - no package data found with a reward count of " + rewardCount);
		return null;
	}

	public void SetLayer(GameLayer layer)
	{
		m_layer = layer;
		SceneUtils.SetLayer(base.gameObject, m_layer);
	}

	public void ShowAlreadyOpenedRewards()
	{
		List<RewardData> currentPageRewards = CurrentPageRewards;
		m_RewardPackages = GetPackageData(currentPageRewards.Count);
		m_RewardObjects = new GameObject[currentPageRewards.Count];
		FadeFullscreenEffectsIn();
		ShowOpenedRewards(currentPageRewards);
		AllDone();
	}

	public void ShowOpenedRewards(List<RewardData> rewardData)
	{
		for (int i = 0; i < m_RewardPackages.Count; i++)
		{
			RewardPackageData rewardPackageData = m_RewardPackages[i];
			if (rewardPackageData.m_TargetBone == null)
			{
				Debug.LogWarning("RewardBoxesDisplay: AnimateRewards package target bone is null!");
				break;
			}
			if (i >= m_RewardObjects.Length || i >= rewardData.Count)
			{
				Debug.LogWarning("RewardBoxesDisplay: AnimateRewards reward index exceeded!");
				break;
			}
			m_RewardObjects[i] = CreateRewardInstance(rewardData[i], i, rewardPackageData.m_TargetBone.position, activeOnStart: true);
		}
	}

	public void AnimateRewards()
	{
		List<RewardData> currentPageRewards = CurrentPageRewards;
		int count = currentPageRewards.Count;
		m_RewardPackages = GetPackageData(count);
		m_RewardObjects = new GameObject[count];
		for (int i = 0; i < m_RewardPackages.Count; i++)
		{
			RewardPackageData rewardPackageData = m_RewardPackages[i];
			if (rewardPackageData.m_TargetBone == null)
			{
				Debug.LogWarning("RewardBoxesDisplay: AnimateRewards package target bone is null!");
				return;
			}
			if (i >= m_RewardObjects.Length || i >= count)
			{
				Debug.LogWarning("RewardBoxesDisplay: AnimateRewards reward index exceeded!");
				return;
			}
			m_RewardObjects[i] = CreateRewardInstance(currentPageRewards[i], i, rewardPackageData.m_TargetBone.position, activeOnStart: false);
		}
		RewardPackageAnimation();
	}

	public void OpenReward(int rewardIndex, Vector3 rewardPos)
	{
		if (rewardIndex >= m_RewardObjects.Length)
		{
			Debug.LogWarning("RewardBoxesDisplay: OpenReward reward index exceeded!");
			return;
		}
		GameObject gameObject = m_RewardObjects[rewardIndex];
		if (gameObject == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: OpenReward object is null!");
			return;
		}
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(value: true);
		}
		if (CheckAllRewardsActive())
		{
			AllDone();
		}
	}

	private void RewardPackageAnimation()
	{
		if (m_RewardSet.m_RewardPackage == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: missing Reward Package!");
			return;
		}
		if (m_currentPageNum == 0)
		{
			FadeFullscreenEffectsIn();
		}
		foreach (GameObject rewardPackageInstance in m_rewardPackageInstances)
		{
			if (rewardPackageInstance != null)
			{
				UnityEngine.Object.Destroy(rewardPackageInstance);
			}
		}
		m_rewardPackageInstances.Clear();
		for (int i = 0; i < m_RewardPackages.Count; i++)
		{
			RewardPackageData rewardPackageData = m_RewardPackages[i];
			if (rewardPackageData.m_TargetBone == null || rewardPackageData.m_StartBone == null)
			{
				Debug.LogWarning("RewardBoxesDisplay: missing reward target bone!");
				continue;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate(m_RewardSet.m_RewardPackage);
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, m_Root.transform);
			gameObject.transform.position = rewardPackageData.m_StartBone.position;
			gameObject.SetActive(value: true);
			m_InstancedObjects.Add(gameObject);
			m_rewardPackageInstances.Add(gameObject);
			Vector3 localScale = gameObject.transform.localScale;
			gameObject.transform.localScale = Vector3.zero;
			SceneUtils.EnableColliders(gameObject, enable: false);
			iTween.ScaleTo(gameObject, iTween.Hash("scale", localScale, "time", m_RewardSet.m_AnimationTime, "delay", rewardPackageData.m_StartDelay, "easetype", iTween.EaseType.linear));
			PlayMakerFSM component = gameObject.GetComponent<PlayMakerFSM>();
			if (component == null)
			{
				Debug.LogWarning("RewardBoxesDisplay: missing reward Playmaker FSM!");
				continue;
			}
			if (!m_playBoxFlyoutSound)
			{
				component.FsmVariables.FindFsmBool("PlayFlyoutSound").Value = false;
			}
			RewardPackage component2 = gameObject.GetComponent<RewardPackage>();
			component2.m_RewardIndex = i;
			RewardBoxData rewardBoxData = new RewardBoxData();
			rewardBoxData.m_GameObject = gameObject;
			rewardBoxData.m_RewardPackage = component2;
			rewardBoxData.m_FSM = component;
			rewardBoxData.m_Index = i;
			iTween.MoveTo(gameObject, iTween.Hash("position", rewardPackageData.m_TargetBone.transform.position, "time", m_RewardSet.m_AnimationTime, "delay", rewardPackageData.m_StartDelay, "easetype", iTween.EaseType.linear, "onstarttarget", base.gameObject, "onstart", "RewardPackageOnStart", "onstartparams", rewardBoxData, "oncompletetarget", base.gameObject, "oncomplete", "RewardPackageOnComplete", "oncompleteparams", rewardBoxData));
		}
	}

	private void RewardPackageOnStart(RewardBoxData boxData)
	{
		boxData.m_FSM.SendEvent("Birth");
	}

	private void RewardPackageOnComplete(RewardBoxData boxData)
	{
		StartCoroutine(RewardPackageActivate(boxData));
	}

	private IEnumerator RewardPackageActivate(RewardBoxData boxData)
	{
		yield return new WaitForSeconds(0.5f);
		SceneUtils.EnableColliders(boxData.m_GameObject, enable: true);
		boxData.m_RewardPackage.AddEventListener(UIEventType.PRESS, RewardPackagePressed);
	}

	private void RewardPackagePressed(UIEvent e)
	{
		Log.RewardBox.Print("box clicked!");
	}

	private GameObject CreateRewardInstance(RewardData reward, int rewardIndex, Vector3 rewardPos, bool activeOnStart)
	{
		GameObject gameObject = null;
		switch (reward.RewardType)
		{
		case Reward.Type.ARCANE_DUST:
		{
			gameObject = UnityEngine.Object.Instantiate(m_RewardSet.m_RewardDust);
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, m_Root.transform);
			gameObject.transform.position = rewardPos;
			gameObject.SetActive(value: true);
			UberText componentInChildren3 = gameObject.GetComponentInChildren<UberText>();
			ArcaneDustRewardData arcaneDustRewardData = (ArcaneDustRewardData)reward;
			componentInChildren3.Text = arcaneDustRewardData.Amount.ToString();
			gameObject.SetActive(activeOnStart);
			break;
		}
		case Reward.Type.BOOSTER_PACK:
		{
			BoosterPackRewardData boosterPackRewardData = reward as BoosterPackRewardData;
			int num = boosterPackRewardData.Id;
			if (num == 0)
			{
				num = 1;
				Debug.LogWarning("RewardBoxesDisplay - booster reward is not valid. ID = 0");
			}
			Log.RewardBox.Print($"Booster DB ID: {num}");
			string arenaPrefab = GameDbf.Booster.GetRecord(num).ArenaPrefab;
			if (string.IsNullOrEmpty(arenaPrefab))
			{
				Debug.LogError($"RewardBoxesDisplay - no prefab found for booster {boosterPackRewardData.Id}!");
				break;
			}
			gameObject = AssetLoader.Get().InstantiatePrefab(arenaPrefab);
			if (boosterPackRewardData.Count > 1)
			{
				UberText componentInChildren2 = gameObject.GetComponentInChildren<UberText>(includeInactive: true);
				if (componentInChildren2 == null)
				{
					Debug.LogError($"RewardBoxesDisplay - no uber text found for booster {boosterPackRewardData.Id}!");
					break;
				}
				componentInChildren2.transform.parent.gameObject.SetActive(value: true);
				componentInChildren2.Text = boosterPackRewardData.Count.ToString();
			}
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, m_Root.transform);
			gameObject.transform.position = rewardPos;
			gameObject.SetActive(activeOnStart);
			break;
		}
		case Reward.Type.GOLD:
		{
			gameObject = UnityEngine.Object.Instantiate(m_RewardSet.m_RewardGold);
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, m_Root.transform);
			gameObject.transform.position = rewardPos;
			gameObject.SetActive(value: true);
			UberText componentInChildren = gameObject.GetComponentInChildren<UberText>();
			GoldRewardData goldRewardData = (GoldRewardData)reward;
			componentInChildren.Text = goldRewardData.Amount.ToString();
			gameObject.SetActive(activeOnStart);
			break;
		}
		case Reward.Type.CARD:
		{
			gameObject = UnityEngine.Object.Instantiate(m_RewardSet.m_RewardCard);
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, m_Root.transform);
			gameObject.transform.position = rewardPos;
			gameObject.SetActive(value: true);
			CardRewardData cardData = (CardRewardData)reward;
			gameObject.GetComponentInChildren<RewardCard>().LoadCard(cardData, m_layer);
			gameObject.SetActive(activeOnStart);
			break;
		}
		case Reward.Type.CARD_BACK:
		{
			gameObject = UnityEngine.Object.Instantiate(m_RewardSet.m_RewardCardBack);
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, m_Root.transform);
			gameObject.transform.position = rewardPos;
			gameObject.SetActive(value: true);
			CardBackRewardData cardbackData = (CardBackRewardData)reward;
			gameObject.GetComponentInChildren<RewardCardBack>().LoadCardBack(cardbackData, m_layer);
			gameObject.SetActive(activeOnStart);
			break;
		}
		}
		if (gameObject == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: Unable to create reward, object null!");
			return null;
		}
		if (rewardIndex >= m_RewardObjects.Length)
		{
			Debug.LogWarning("RewardBoxesDisplay: CreateRewardInstance reward index exceeded!");
			return null;
		}
		SceneUtils.SetLayer(gameObject, m_layer);
		m_RewardObjects[rewardIndex] = gameObject;
		m_InstancedObjects.Add(gameObject);
		return gameObject;
	}

	private void AllDone()
	{
		Vector3 zero = Vector3.zero;
		if (m_RewardPackages.Count > 1)
		{
			for (int i = 0; i < m_RewardPackages.Count; i++)
			{
				RewardPackageData rewardPackageData = m_RewardPackages[i];
				zero += rewardPackageData.m_TargetBone.position;
			}
			m_DoneButton.transform.position = zero / m_RewardPackages.Count;
		}
		m_DoneButton.gameObject.SetActive(value: true);
		if (IsOnLastPage)
		{
			m_DoneButton.SetText(GameStrings.Get("GLOBAL_DONE"));
		}
		else
		{
			m_DoneButton.SetText(GameStrings.Get("GLOBAL_BUTTON_NEXT"));
		}
		Spell component = m_DoneButton.m_button.GetComponent<Spell>();
		component.AddFinishedCallback(OnDoneButtonShown);
		component.ActivateState(SpellStateType.BIRTH);
		if (IsOnLastPage)
		{
			NarrativeManager.Get().OnArenaRewardsShown();
		}
	}

	private void OnDoneButtonShown(Spell spell, object userData)
	{
		m_doneButtonFinishedShown = true;
		SceneUtils.EnableColliders(m_DoneButton.gameObject, enable: true);
		m_DoneButton.AddEventListener(UIEventType.RELEASE, OnDoneButtonPressed);
		if (IsOnLastPage)
		{
			Navigation.Push(OnNavigateBack);
		}
	}

	private void OnDoneButtonPressed(UIEvent e)
	{
		if (IsOnLastPage)
		{
			FadeFullscreenEffectsOut();
			Navigation.GoBack();
			return;
		}
		m_currentPageNum++;
		KillRewardObjects();
		KillDoneButton();
		StartCoroutine(AnimateRewardsWhenReady());
	}

	private IEnumerator AnimateRewardsWhenReady()
	{
		while (CheckAnyRewardActive())
		{
			yield return null;
		}
		AnimateRewards();
	}

	public void Close()
	{
		IsClosing = true;
		if (m_doneButtonFinishedShown)
		{
			OnNavigateBack();
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private void KillRewardObjects()
	{
		GameObject[] rewardObjects = m_RewardObjects;
		foreach (GameObject gameObject in rewardObjects)
		{
			if (!(gameObject == null))
			{
				PlayMakerFSM component = gameObject.GetComponent<PlayMakerFSM>();
				if (component != null)
				{
					component.SendEvent("Death");
				}
				UberText[] componentsInChildren = gameObject.GetComponentsInChildren<UberText>();
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					iTween.FadeTo(componentsInChildren[j].gameObject, iTween.Hash("alpha", 0f, "time", 0.8f, "includechildren", true, "easetype", iTween.EaseType.easeInOutCubic));
				}
				RewardCard componentInChildren = gameObject.GetComponentInChildren<RewardCard>();
				if (componentInChildren != null)
				{
					componentInChildren.Death();
				}
				UnityEngine.Object.Destroy(gameObject, 0.8f);
			}
		}
	}

	private void KillDoneButton()
	{
		SceneUtils.EnableColliders(m_DoneButton.gameObject, enable: false);
		m_DoneButton.RemoveEventListener(UIEventType.RELEASE, OnDoneButtonPressed);
		m_DoneButton.m_button.GetComponent<Spell>().ActivateState(SpellStateType.DEATH);
	}

	private bool OnNavigateBack()
	{
		Debug.Log("navigating back!");
		if (!m_DoneButton.m_button.activeSelf)
		{
			return false;
		}
		KillRewardObjects();
		KillDoneButton();
		return true;
	}

	private void FadeFullscreenEffectsIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: FullScreenFXMgr.Get() returned null!");
			return;
		}
		fullScreenFXMgr.SetBlurBrightness(0.85f);
		fullScreenFXMgr.SetBlurDesaturation(0f);
		fullScreenFXMgr.Vignette(0.4f, 0.5f, iTween.EaseType.easeOutCirc);
		fullScreenFXMgr.Blur(1f, 0.5f, iTween.EaseType.easeOutCirc);
		if (m_useDarkeningClickCatcher)
		{
			iTween.FadeTo(m_ClickCatcher, 0.75f, 0.5f);
		}
	}

	private void FadeFullscreenEffectsOut()
	{
		if (m_hasFadedFullScreenEffectsOut)
		{
			return;
		}
		m_hasFadedFullScreenEffectsOut = true;
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: FullScreenFXMgr.Get() returned null!");
			return;
		}
		fullScreenFXMgr.StopVignette(2f, iTween.EaseType.easeOutCirc, FadeFullscreenEffectsOutFinished);
		fullScreenFXMgr.StopBlur(2f, iTween.EaseType.easeOutCirc);
		if (m_useDarkeningClickCatcher)
		{
			iTween.FadeTo(m_ClickCatcher, 0f, 0.5f);
		}
	}

	private void FadeVignetteIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: FullScreenFXMgr.Get() returned null!");
			return;
		}
		fullScreenFXMgr.DisableBlur();
		fullScreenFXMgr.Vignette(1.4f, 1.5f, iTween.EaseType.easeOutCirc);
	}

	private void FadeFullscreenEffectsOutFinished()
	{
		foreach (Action doneCallback in m_doneCallbacks)
		{
			doneCallback?.Invoke();
		}
		m_doneCallbacks.Clear();
		if (!m_destroyed)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	private bool CheckAllRewardsActive()
	{
		GameObject[] rewardObjects = m_RewardObjects;
		foreach (GameObject gameObject in rewardObjects)
		{
			if (gameObject == null || !gameObject.activeSelf)
			{
				return false;
			}
		}
		return true;
	}

	private bool CheckAnyRewardActive()
	{
		GameObject[] rewardObjects = m_RewardObjects;
		for (int i = 0; i < rewardObjects.Length; i++)
		{
			if (rewardObjects[i] != null)
			{
				return true;
			}
		}
		return false;
	}

	private void CleanUp()
	{
		foreach (GameObject instancedObject in m_InstancedObjects)
		{
			if (instancedObject != null)
			{
				UnityEngine.Object.Destroy(instancedObject);
			}
		}
		FadeFullscreenEffectsOut();
		s_Instance = null;
	}

	public void DebugLogRewards()
	{
		Debug.Log("BOX REWARDS:");
		List<RewardData> currentPageRewards = CurrentPageRewards;
		for (int i = 0; i < currentPageRewards.Count; i++)
		{
			RewardData arg = currentPageRewards[i];
			Debug.Log($"  reward {i}={arg}");
		}
	}
}
