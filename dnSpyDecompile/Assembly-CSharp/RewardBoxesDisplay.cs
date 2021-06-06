using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020002BF RID: 703
[CustomEditClass]
public class RewardBoxesDisplay : MonoBehaviour
{
	// Token: 0x170004C5 RID: 1221
	// (get) Token: 0x060024E7 RID: 9447 RVA: 0x000B9A2C File Offset: 0x000B7C2C
	private bool IsOnLastPage
	{
		get
		{
			return this.m_currentPageNum >= this.m_lastPageNum;
		}
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x000B9A3F File Offset: 0x000B7C3F
	private void Awake()
	{
		RewardBoxesDisplay.s_Instance = this;
		this.m_InstancedObjects = new List<GameObject>();
		this.m_doneCallbacks = new List<Action>();
		RenderUtils.SetAlpha(this.m_ClickCatcher, 0f);
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x000B9A6D File Offset: 0x000B7C6D
	private void Start()
	{
		if (this.m_RewardSet.m_RewardPackage != null)
		{
			this.m_RewardSet.m_RewardPackage.SetActive(false);
		}
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnDisable()
	{
	}

	// Token: 0x060024EB RID: 9451 RVA: 0x000B9A93 File Offset: 0x000B7C93
	private void OnDestroy()
	{
		this.CleanUp();
		this.m_destroyed = true;
	}

	// Token: 0x060024EC RID: 9452 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnEnable()
	{
	}

	// Token: 0x060024ED RID: 9453 RVA: 0x000B9AA2 File Offset: 0x000B7CA2
	public static RewardBoxesDisplay Get()
	{
		return RewardBoxesDisplay.s_Instance;
	}

	// Token: 0x060024EE RID: 9454 RVA: 0x000B9AAC File Offset: 0x000B7CAC
	public void SetRewards(List<RewardData> rewards)
	{
		this.m_rewards = rewards;
		int num2;
		int num = Math.DivRem(this.m_rewards.Count, this.m_RewardSet.m_MaxPackagesPerPage, out num2);
		if (num2 > 0)
		{
			num++;
		}
		this.m_lastPageNum = num - 1;
	}

	// Token: 0x060024EF RID: 9455 RVA: 0x000B9AEF File Offset: 0x000B7CEF
	public void UseDarkeningClickCatcher(bool value)
	{
		this.m_useDarkeningClickCatcher = value;
	}

	// Token: 0x060024F0 RID: 9456 RVA: 0x000B9AF8 File Offset: 0x000B7CF8
	public void RegisterDoneCallback(Action action)
	{
		this.m_doneCallbacks.Add(action);
	}

	// Token: 0x060024F1 RID: 9457 RVA: 0x000B9B08 File Offset: 0x000B7D08
	public List<RewardBoxesDisplay.RewardPackageData> GetPackageData(int rewardCount)
	{
		for (int i = 0; i < this.m_RewardSet.m_RewardData.Count; i++)
		{
			if (this.m_RewardSet.m_RewardData[i].m_PackageData.Count == rewardCount)
			{
				return this.m_RewardSet.m_RewardData[i].m_PackageData;
			}
		}
		Debug.LogError("RewardBoxesDisplay: GetPackageData - no package data found with a reward count of " + rewardCount);
		return null;
	}

	// Token: 0x060024F2 RID: 9458 RVA: 0x000B9B7B File Offset: 0x000B7D7B
	public void SetLayer(GameLayer layer)
	{
		this.m_layer = layer;
		SceneUtils.SetLayer(base.gameObject, this.m_layer);
	}

	// Token: 0x060024F3 RID: 9459 RVA: 0x000B9B98 File Offset: 0x000B7D98
	public void ShowAlreadyOpenedRewards()
	{
		List<RewardData> currentPageRewards = this.CurrentPageRewards;
		this.m_RewardPackages = this.GetPackageData(currentPageRewards.Count);
		this.m_RewardObjects = new GameObject[currentPageRewards.Count];
		this.FadeFullscreenEffectsIn();
		this.ShowOpenedRewards(currentPageRewards);
		this.AllDone();
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x000B9BE4 File Offset: 0x000B7DE4
	public void ShowOpenedRewards(List<RewardData> rewardData)
	{
		for (int i = 0; i < this.m_RewardPackages.Count; i++)
		{
			RewardBoxesDisplay.RewardPackageData rewardPackageData = this.m_RewardPackages[i];
			if (rewardPackageData.m_TargetBone == null)
			{
				Debug.LogWarning("RewardBoxesDisplay: AnimateRewards package target bone is null!");
				return;
			}
			if (i >= this.m_RewardObjects.Length || i >= rewardData.Count)
			{
				Debug.LogWarning("RewardBoxesDisplay: AnimateRewards reward index exceeded!");
				return;
			}
			this.m_RewardObjects[i] = this.CreateRewardInstance(rewardData[i], i, rewardPackageData.m_TargetBone.position, true);
		}
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x000B9C70 File Offset: 0x000B7E70
	public void AnimateRewards()
	{
		List<RewardData> currentPageRewards = this.CurrentPageRewards;
		int count = currentPageRewards.Count;
		this.m_RewardPackages = this.GetPackageData(count);
		this.m_RewardObjects = new GameObject[count];
		for (int i = 0; i < this.m_RewardPackages.Count; i++)
		{
			RewardBoxesDisplay.RewardPackageData rewardPackageData = this.m_RewardPackages[i];
			if (rewardPackageData.m_TargetBone == null)
			{
				Debug.LogWarning("RewardBoxesDisplay: AnimateRewards package target bone is null!");
				return;
			}
			if (i >= this.m_RewardObjects.Length || i >= count)
			{
				Debug.LogWarning("RewardBoxesDisplay: AnimateRewards reward index exceeded!");
				return;
			}
			this.m_RewardObjects[i] = this.CreateRewardInstance(currentPageRewards[i], i, rewardPackageData.m_TargetBone.position, false);
		}
		this.RewardPackageAnimation();
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x000B9D24 File Offset: 0x000B7F24
	public void OpenReward(int rewardIndex, Vector3 rewardPos)
	{
		if (rewardIndex >= this.m_RewardObjects.Length)
		{
			Debug.LogWarning("RewardBoxesDisplay: OpenReward reward index exceeded!");
			return;
		}
		GameObject gameObject = this.m_RewardObjects[rewardIndex];
		if (gameObject == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: OpenReward object is null!");
			return;
		}
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}
		if (this.CheckAllRewardsActive())
		{
			this.AllDone();
		}
	}

	// Token: 0x170004C6 RID: 1222
	// (get) Token: 0x060024F7 RID: 9463 RVA: 0x000B9D81 File Offset: 0x000B7F81
	private List<RewardData> CurrentPageRewards
	{
		get
		{
			return this.m_rewards.Skip(this.m_RewardSet.m_MaxPackagesPerPage * this.m_currentPageNum).Take(this.m_RewardSet.m_MaxPackagesPerPage).ToList<RewardData>();
		}
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x000B9DB8 File Offset: 0x000B7FB8
	private void RewardPackageAnimation()
	{
		if (this.m_RewardSet.m_RewardPackage == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: missing Reward Package!");
			return;
		}
		if (this.m_currentPageNum == 0)
		{
			this.FadeFullscreenEffectsIn();
		}
		foreach (GameObject gameObject in this.m_rewardPackageInstances)
		{
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		this.m_rewardPackageInstances.Clear();
		for (int i = 0; i < this.m_RewardPackages.Count; i++)
		{
			RewardBoxesDisplay.RewardPackageData rewardPackageData = this.m_RewardPackages[i];
			if (rewardPackageData.m_TargetBone == null || rewardPackageData.m_StartBone == null)
			{
				Debug.LogWarning("RewardBoxesDisplay: missing reward target bone!");
			}
			else
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.m_RewardSet.m_RewardPackage);
				TransformUtil.AttachAndPreserveLocalTransform(gameObject2.transform, this.m_Root.transform);
				gameObject2.transform.position = rewardPackageData.m_StartBone.position;
				gameObject2.SetActive(true);
				this.m_InstancedObjects.Add(gameObject2);
				this.m_rewardPackageInstances.Add(gameObject2);
				Vector3 localScale = gameObject2.transform.localScale;
				gameObject2.transform.localScale = Vector3.zero;
				SceneUtils.EnableColliders(gameObject2, false);
				iTween.ScaleTo(gameObject2, iTween.Hash(new object[]
				{
					"scale",
					localScale,
					"time",
					this.m_RewardSet.m_AnimationTime,
					"delay",
					rewardPackageData.m_StartDelay,
					"easetype",
					iTween.EaseType.linear
				}));
				PlayMakerFSM component = gameObject2.GetComponent<PlayMakerFSM>();
				if (component == null)
				{
					Debug.LogWarning("RewardBoxesDisplay: missing reward Playmaker FSM!");
				}
				else
				{
					if (!this.m_playBoxFlyoutSound)
					{
						component.FsmVariables.FindFsmBool("PlayFlyoutSound").Value = false;
					}
					RewardPackage component2 = gameObject2.GetComponent<RewardPackage>();
					component2.m_RewardIndex = i;
					RewardBoxesDisplay.RewardBoxData rewardBoxData = new RewardBoxesDisplay.RewardBoxData();
					rewardBoxData.m_GameObject = gameObject2;
					rewardBoxData.m_RewardPackage = component2;
					rewardBoxData.m_FSM = component;
					rewardBoxData.m_Index = i;
					iTween.MoveTo(gameObject2, iTween.Hash(new object[]
					{
						"position",
						rewardPackageData.m_TargetBone.transform.position,
						"time",
						this.m_RewardSet.m_AnimationTime,
						"delay",
						rewardPackageData.m_StartDelay,
						"easetype",
						iTween.EaseType.linear,
						"onstarttarget",
						base.gameObject,
						"onstart",
						"RewardPackageOnStart",
						"onstartparams",
						rewardBoxData,
						"oncompletetarget",
						base.gameObject,
						"oncomplete",
						"RewardPackageOnComplete",
						"oncompleteparams",
						rewardBoxData
					}));
				}
			}
		}
	}

	// Token: 0x060024F9 RID: 9465 RVA: 0x000BA0E8 File Offset: 0x000B82E8
	private void RewardPackageOnStart(RewardBoxesDisplay.RewardBoxData boxData)
	{
		boxData.m_FSM.SendEvent("Birth");
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x000BA0FA File Offset: 0x000B82FA
	private void RewardPackageOnComplete(RewardBoxesDisplay.RewardBoxData boxData)
	{
		base.StartCoroutine(this.RewardPackageActivate(boxData));
	}

	// Token: 0x060024FB RID: 9467 RVA: 0x000BA10A File Offset: 0x000B830A
	private IEnumerator RewardPackageActivate(RewardBoxesDisplay.RewardBoxData boxData)
	{
		yield return new WaitForSeconds(0.5f);
		SceneUtils.EnableColliders(boxData.m_GameObject, true);
		boxData.m_RewardPackage.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.RewardPackagePressed));
		yield break;
	}

	// Token: 0x060024FC RID: 9468 RVA: 0x000BA120 File Offset: 0x000B8320
	private void RewardPackagePressed(UIEvent e)
	{
		Log.RewardBox.Print("box clicked!", Array.Empty<object>());
	}

	// Token: 0x060024FD RID: 9469 RVA: 0x000BA138 File Offset: 0x000B8338
	private GameObject CreateRewardInstance(RewardData reward, int rewardIndex, Vector3 rewardPos, bool activeOnStart)
	{
		GameObject gameObject = null;
		switch (reward.RewardType)
		{
		case Reward.Type.ARCANE_DUST:
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_RewardSet.m_RewardDust);
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, this.m_Root.transform);
			gameObject.transform.position = rewardPos;
			gameObject.SetActive(true);
			UberText componentInChildren = gameObject.GetComponentInChildren<UberText>();
			ArcaneDustRewardData arcaneDustRewardData = (ArcaneDustRewardData)reward;
			componentInChildren.Text = arcaneDustRewardData.Amount.ToString();
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
			Log.RewardBox.Print(string.Format("Booster DB ID: {0}", num), Array.Empty<object>());
			string arenaPrefab = GameDbf.Booster.GetRecord(num).ArenaPrefab;
			if (string.IsNullOrEmpty(arenaPrefab))
			{
				Debug.LogError(string.Format("RewardBoxesDisplay - no prefab found for booster {0}!", boosterPackRewardData.Id));
			}
			else
			{
				gameObject = AssetLoader.Get().InstantiatePrefab(arenaPrefab, AssetLoadingOptions.None);
				if (boosterPackRewardData.Count > 1)
				{
					UberText componentInChildren2 = gameObject.GetComponentInChildren<UberText>(true);
					if (componentInChildren2 == null)
					{
						Debug.LogError(string.Format("RewardBoxesDisplay - no uber text found for booster {0}!", boosterPackRewardData.Id));
						break;
					}
					componentInChildren2.transform.parent.gameObject.SetActive(true);
					componentInChildren2.Text = boosterPackRewardData.Count.ToString();
				}
				TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, this.m_Root.transform);
				gameObject.transform.position = rewardPos;
				gameObject.SetActive(activeOnStart);
			}
			break;
		}
		case Reward.Type.CARD:
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_RewardSet.m_RewardCard);
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, this.m_Root.transform);
			gameObject.transform.position = rewardPos;
			gameObject.SetActive(true);
			CardRewardData cardData = (CardRewardData)reward;
			gameObject.GetComponentInChildren<RewardCard>().LoadCard(cardData, this.m_layer);
			gameObject.SetActive(activeOnStart);
			break;
		}
		case Reward.Type.CARD_BACK:
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_RewardSet.m_RewardCardBack);
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, this.m_Root.transform);
			gameObject.transform.position = rewardPos;
			gameObject.SetActive(true);
			CardBackRewardData cardbackData = (CardBackRewardData)reward;
			gameObject.GetComponentInChildren<RewardCardBack>().LoadCardBack(cardbackData, this.m_layer);
			gameObject.SetActive(activeOnStart);
			break;
		}
		case Reward.Type.GOLD:
		{
			gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_RewardSet.m_RewardGold);
			TransformUtil.AttachAndPreserveLocalTransform(gameObject.transform, this.m_Root.transform);
			gameObject.transform.position = rewardPos;
			gameObject.SetActive(true);
			UberText componentInChildren3 = gameObject.GetComponentInChildren<UberText>();
			GoldRewardData goldRewardData = (GoldRewardData)reward;
			componentInChildren3.Text = goldRewardData.Amount.ToString();
			gameObject.SetActive(activeOnStart);
			break;
		}
		}
		if (gameObject == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: Unable to create reward, object null!");
			return null;
		}
		if (rewardIndex >= this.m_RewardObjects.Length)
		{
			Debug.LogWarning("RewardBoxesDisplay: CreateRewardInstance reward index exceeded!");
			return null;
		}
		SceneUtils.SetLayer(gameObject, this.m_layer);
		this.m_RewardObjects[rewardIndex] = gameObject;
		this.m_InstancedObjects.Add(gameObject);
		return gameObject;
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x000BA478 File Offset: 0x000B8678
	private void AllDone()
	{
		Vector3 a = Vector3.zero;
		if (this.m_RewardPackages.Count > 1)
		{
			for (int i = 0; i < this.m_RewardPackages.Count; i++)
			{
				RewardBoxesDisplay.RewardPackageData rewardPackageData = this.m_RewardPackages[i];
				a += rewardPackageData.m_TargetBone.position;
			}
			this.m_DoneButton.transform.position = a / (float)this.m_RewardPackages.Count;
		}
		this.m_DoneButton.gameObject.SetActive(true);
		if (this.IsOnLastPage)
		{
			this.m_DoneButton.SetText(GameStrings.Get("GLOBAL_DONE"));
		}
		else
		{
			this.m_DoneButton.SetText(GameStrings.Get("GLOBAL_BUTTON_NEXT"));
		}
		Spell component = this.m_DoneButton.m_button.GetComponent<Spell>();
		component.AddFinishedCallback(new Spell.FinishedCallback(this.OnDoneButtonShown));
		component.ActivateState(SpellStateType.BIRTH);
		if (this.IsOnLastPage)
		{
			NarrativeManager.Get().OnArenaRewardsShown();
		}
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x000BA570 File Offset: 0x000B8770
	private void OnDoneButtonShown(Spell spell, object userData)
	{
		this.m_doneButtonFinishedShown = true;
		SceneUtils.EnableColliders(this.m_DoneButton.gameObject, true);
		this.m_DoneButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDoneButtonPressed));
		if (this.IsOnLastPage)
		{
			Navigation.Push(new Navigation.NavigateBackHandler(this.OnNavigateBack));
		}
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x000BA5C7 File Offset: 0x000B87C7
	private void OnDoneButtonPressed(UIEvent e)
	{
		if (this.IsOnLastPage)
		{
			this.FadeFullscreenEffectsOut();
			Navigation.GoBack();
			return;
		}
		this.m_currentPageNum++;
		this.KillRewardObjects();
		this.KillDoneButton();
		base.StartCoroutine(this.AnimateRewardsWhenReady());
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x000BA605 File Offset: 0x000B8805
	private IEnumerator AnimateRewardsWhenReady()
	{
		while (this.CheckAnyRewardActive())
		{
			yield return null;
		}
		this.AnimateRewards();
		yield break;
	}

	// Token: 0x170004C7 RID: 1223
	// (get) Token: 0x06002502 RID: 9474 RVA: 0x000BA614 File Offset: 0x000B8814
	// (set) Token: 0x06002503 RID: 9475 RVA: 0x000BA61C File Offset: 0x000B881C
	public bool IsClosing { get; private set; }

	// Token: 0x06002504 RID: 9476 RVA: 0x000BA625 File Offset: 0x000B8825
	public void Close()
	{
		this.IsClosing = true;
		if (this.m_doneButtonFinishedShown)
		{
			this.OnNavigateBack();
			return;
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x000BA64C File Offset: 0x000B884C
	private void KillRewardObjects()
	{
		foreach (GameObject gameObject in this.m_RewardObjects)
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
					iTween.FadeTo(componentsInChildren[j].gameObject, iTween.Hash(new object[]
					{
						"alpha",
						0f,
						"time",
						0.8f,
						"includechildren",
						true,
						"easetype",
						iTween.EaseType.easeInOutCubic
					}));
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

	// Token: 0x06002506 RID: 9478 RVA: 0x000BA744 File Offset: 0x000B8944
	private void KillDoneButton()
	{
		SceneUtils.EnableColliders(this.m_DoneButton.gameObject, false);
		this.m_DoneButton.RemoveEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDoneButtonPressed));
		this.m_DoneButton.m_button.GetComponent<Spell>().ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x06002507 RID: 9479 RVA: 0x000BA791 File Offset: 0x000B8991
	private bool OnNavigateBack()
	{
		Debug.Log("navigating back!");
		if (!this.m_DoneButton.m_button.activeSelf)
		{
			return false;
		}
		this.KillRewardObjects();
		this.KillDoneButton();
		return true;
	}

	// Token: 0x06002508 RID: 9480 RVA: 0x000BA7C0 File Offset: 0x000B89C0
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
		fullScreenFXMgr.Vignette(0.4f, 0.5f, iTween.EaseType.easeOutCirc, null, null);
		fullScreenFXMgr.Blur(1f, 0.5f, iTween.EaseType.easeOutCirc, null);
		if (this.m_useDarkeningClickCatcher)
		{
			iTween.FadeTo(this.m_ClickCatcher, 0.75f, 0.5f);
		}
	}

	// Token: 0x06002509 RID: 9481 RVA: 0x000BA83C File Offset: 0x000B8A3C
	private void FadeFullscreenEffectsOut()
	{
		if (this.m_hasFadedFullScreenEffectsOut)
		{
			return;
		}
		this.m_hasFadedFullScreenEffectsOut = true;
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: FullScreenFXMgr.Get() returned null!");
			return;
		}
		fullScreenFXMgr.StopVignette(2f, iTween.EaseType.easeOutCirc, new Action(this.FadeFullscreenEffectsOutFinished), null);
		fullScreenFXMgr.StopBlur(2f, iTween.EaseType.easeOutCirc, null, false);
		if (this.m_useDarkeningClickCatcher)
		{
			iTween.FadeTo(this.m_ClickCatcher, 0f, 0.5f);
		}
	}

	// Token: 0x0600250A RID: 9482 RVA: 0x000BA8B4 File Offset: 0x000B8AB4
	private void FadeVignetteIn()
	{
		FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
		if (fullScreenFXMgr == null)
		{
			Debug.LogWarning("RewardBoxesDisplay: FullScreenFXMgr.Get() returned null!");
			return;
		}
		fullScreenFXMgr.DisableBlur();
		fullScreenFXMgr.Vignette(1.4f, 1.5f, iTween.EaseType.easeOutCirc, null, null);
	}

	// Token: 0x0600250B RID: 9483 RVA: 0x000BA8F0 File Offset: 0x000B8AF0
	private void FadeFullscreenEffectsOutFinished()
	{
		foreach (Action action in this.m_doneCallbacks)
		{
			if (action != null)
			{
				action();
			}
		}
		this.m_doneCallbacks.Clear();
		if (!this.m_destroyed)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0600250C RID: 9484 RVA: 0x000BA964 File Offset: 0x000B8B64
	private bool CheckAllRewardsActive()
	{
		foreach (GameObject gameObject in this.m_RewardObjects)
		{
			if (gameObject == null || !gameObject.activeSelf)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600250D RID: 9485 RVA: 0x000BA9A0 File Offset: 0x000B8BA0
	private bool CheckAnyRewardActive()
	{
		GameObject[] rewardObjects = this.m_RewardObjects;
		for (int i = 0; i < rewardObjects.Length; i++)
		{
			if (rewardObjects[i] != null)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x000BA9D0 File Offset: 0x000B8BD0
	private void CleanUp()
	{
		foreach (GameObject gameObject in this.m_InstancedObjects)
		{
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		this.FadeFullscreenEffectsOut();
		RewardBoxesDisplay.s_Instance = null;
	}

	// Token: 0x0600250F RID: 9487 RVA: 0x000BAA38 File Offset: 0x000B8C38
	public void DebugLogRewards()
	{
		Debug.Log("BOX REWARDS:");
		List<RewardData> currentPageRewards = this.CurrentPageRewards;
		for (int i = 0; i < currentPageRewards.Count; i++)
		{
			RewardData arg = currentPageRewards[i];
			Debug.Log(string.Format("  reward {0}={1}", i, arg));
		}
	}

	// Token: 0x04001499 RID: 5273
	public bool m_playBoxFlyoutSound = true;

	// Token: 0x0400149A RID: 5274
	public const string DEFAULT_PREFAB = "RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267";

	// Token: 0x0400149B RID: 5275
	public GameObject m_Root;

	// Token: 0x0400149C RID: 5276
	public GameObject m_ClickCatcher;

	// Token: 0x0400149D RID: 5277
	[CustomEditField(Sections = "Reward Panel")]
	public NormalButton m_DoneButton;

	// Token: 0x0400149E RID: 5278
	public RewardBoxesDisplay.RewardSet m_RewardSet;

	// Token: 0x0400149F RID: 5279
	private List<Action> m_doneCallbacks;

	// Token: 0x040014A0 RID: 5280
	private List<GameObject> m_InstancedObjects;

	// Token: 0x040014A1 RID: 5281
	private GameObject[] m_RewardObjects;

	// Token: 0x040014A2 RID: 5282
	private List<RewardBoxesDisplay.RewardPackageData> m_RewardPackages;

	// Token: 0x040014A3 RID: 5283
	private GameLayer m_layer = GameLayer.IgnoreFullScreenEffects;

	// Token: 0x040014A4 RID: 5284
	private bool m_useDarkeningClickCatcher;

	// Token: 0x040014A5 RID: 5285
	private bool m_doneButtonFinishedShown;

	// Token: 0x040014A6 RID: 5286
	private bool m_destroyed;

	// Token: 0x040014A7 RID: 5287
	private List<RewardData> m_rewards;

	// Token: 0x040014A8 RID: 5288
	private int m_currentPageNum;

	// Token: 0x040014A9 RID: 5289
	private int m_lastPageNum;

	// Token: 0x040014AA RID: 5290
	private List<GameObject> m_rewardPackageInstances = new List<GameObject>();

	// Token: 0x040014AB RID: 5291
	private bool m_hasFadedFullScreenEffectsOut;

	// Token: 0x040014AC RID: 5292
	private static RewardBoxesDisplay s_Instance;

	// Token: 0x020015CC RID: 5580
	[Serializable]
	public class RewardPackageData
	{
		// Token: 0x0400AF05 RID: 44805
		public Transform m_StartBone;

		// Token: 0x0400AF06 RID: 44806
		public Transform m_TargetBone;

		// Token: 0x0400AF07 RID: 44807
		public float m_StartDelay;
	}

	// Token: 0x020015CD RID: 5581
	[Serializable]
	public class RewardSet
	{
		// Token: 0x0400AF08 RID: 44808
		public GameObject m_RewardPackage;

		// Token: 0x0400AF09 RID: 44809
		public float m_AnimationTime = 1f;

		// Token: 0x0400AF0A RID: 44810
		public GameObject m_RewardCard;

		// Token: 0x0400AF0B RID: 44811
		public GameObject m_RewardCardBack;

		// Token: 0x0400AF0C RID: 44812
		public GameObject m_RewardGold;

		// Token: 0x0400AF0D RID: 44813
		public GameObject m_RewardDust;

		// Token: 0x0400AF0E RID: 44814
		public int m_MaxPackagesPerPage;

		// Token: 0x0400AF0F RID: 44815
		public List<RewardBoxesDisplay.BoxRewardData> m_RewardData;
	}

	// Token: 0x020015CE RID: 5582
	[Serializable]
	public class BoxRewardData
	{
		// Token: 0x0400AF10 RID: 44816
		public List<RewardBoxesDisplay.RewardPackageData> m_PackageData;
	}

	// Token: 0x020015CF RID: 5583
	public class RewardBoxData
	{
		// Token: 0x0400AF11 RID: 44817
		public GameObject m_GameObject;

		// Token: 0x0400AF12 RID: 44818
		public RewardPackage m_RewardPackage;

		// Token: 0x0400AF13 RID: 44819
		public PlayMakerFSM m_FSM;

		// Token: 0x0400AF14 RID: 44820
		public int m_Index;
	}

	// Token: 0x020015D0 RID: 5584
	public class RewardCardLoadData
	{
		// Token: 0x0400AF15 RID: 44821
		public EntityDef m_EntityDef;

		// Token: 0x0400AF16 RID: 44822
		public Transform m_ParentTransform;

		// Token: 0x0400AF17 RID: 44823
		public CardRewardData m_CardRewardData;
	}

	// Token: 0x020015D1 RID: 5585
	private enum Events
	{
		// Token: 0x0400AF19 RID: 44825
		GVG_PROMOTION
	}
}
