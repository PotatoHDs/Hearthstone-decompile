using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using PegasusShared;
using UnityEngine;

// Token: 0x0200002F RID: 47
[CustomEditClass]
public class AdventureClassChallenge : MonoBehaviour
{
	// Token: 0x06000197 RID: 407 RVA: 0x00009818 File Offset: 0x00007A18
	private void Awake()
	{
		base.transform.position = new Vector3(-500f, 0f, 0f);
		this.m_BackButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.BackButton();
		});
		this.m_PlayButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.Play();
		});
		this.m_EmptyChallengeButtonSlot.SetActive(false);
		AssetLoader.Get().InstantiatePrefab(this.m_VersusTextPrefab, new PrefabCallback<GameObject>(this.OnVersusLettersLoaded), null, AssetLoadingOptions.None);
	}

	// Token: 0x06000198 RID: 408 RVA: 0x000098A8 File Offset: 0x00007AA8
	private void Start()
	{
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		this.InitModeName();
		this.InitAdventureChallenges();
		Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		Navigation.PushUnique(new Navigation.NavigateBackHandler(AdventureClassChallenge.OnNavigateBack));
		base.StartCoroutine(this.CreateChallengeButtons());
	}

	// Token: 0x06000199 RID: 409 RVA: 0x0000990B File Offset: 0x00007B0B
	private void OnDestroy()
	{
		GameMgr.Get().UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00009924 File Offset: 0x00007B24
	private void InitModeName()
	{
		int selectedAdventure = (int)AdventureConfig.Get().GetSelectedAdventure();
		int selectedMode = (int)AdventureConfig.Get().GetSelectedMode();
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord(selectedAdventure, selectedMode);
		string text = UniversalInputManager.UsePhoneUI ? adventureDataRecord.ShortName : adventureDataRecord.Name;
		this.m_ModeName.Text = text;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000997C File Offset: 0x00007B7C
	private void InitAdventureChallenges()
	{
		List<ScenarioDbfRecord> records = GameDbf.Scenario.GetRecords();
		records.Sort(delegate(ScenarioDbfRecord a, ScenarioDbfRecord b)
		{
			int sortOrder = a.SortOrder;
			int sortOrder2 = b.SortOrder;
			return sortOrder - sortOrder2;
		});
		foreach (ScenarioDbfRecord scenarioDbfRecord in records)
		{
			if (scenarioDbfRecord.AdventureId == (int)AdventureConfig.Get().GetSelectedAdventure() && scenarioDbfRecord.ModeId == 4)
			{
				int player1HeroCardId = scenarioDbfRecord.Player1HeroCardId;
				int num = scenarioDbfRecord.ClientPlayer2HeroCardId;
				if (num == 0)
				{
					num = scenarioDbfRecord.Player2HeroCardId;
				}
				AdventureClassChallenge.ClassChallengeData classChallengeData = new AdventureClassChallenge.ClassChallengeData();
				classChallengeData.scenarioRecord = scenarioDbfRecord;
				classChallengeData.heroID0 = GameUtils.TranslateDbIdToCardId(player1HeroCardId, false);
				classChallengeData.heroID1 = GameUtils.TranslateDbIdToCardId(num, false);
				classChallengeData.unlocked = AdventureProgressMgr.Get().CanPlayScenario(scenarioDbfRecord.ID, true);
				if (AdventureProgressMgr.Get().HasDefeatedScenario(scenarioDbfRecord.ID))
				{
					classChallengeData.defeated = true;
				}
				else
				{
					classChallengeData.defeated = false;
				}
				classChallengeData.name = scenarioDbfRecord.ShortName;
				classChallengeData.title = scenarioDbfRecord.Name;
				classChallengeData.description = ((UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(scenarioDbfRecord.ShortDescription)) ? scenarioDbfRecord.ShortDescription : scenarioDbfRecord.Description);
				classChallengeData.completedDescription = scenarioDbfRecord.CompletedDescription;
				classChallengeData.opponentName = scenarioDbfRecord.OpponentName;
				this.m_ScenarioChallengeLookup.Add(scenarioDbfRecord.ID, this.m_ClassChallenges.Count);
				this.m_ClassChallenges.Add(classChallengeData);
			}
		}
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00009B4C File Offset: 0x00007D4C
	private int BossCreateParamsSortComparison(AdventureClassChallenge.ClassChallengeData data1, AdventureClassChallenge.ClassChallengeData data2)
	{
		return GameUtils.MissionSortComparison(data1.scenarioRecord, data2.scenarioRecord);
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00009B5F File Offset: 0x00007D5F
	private IEnumerator CreateChallengeButtons()
	{
		int num = 0;
		int lastSelectedMission = (int)AdventureConfig.Get().GetLastSelectedMission();
		for (int i = 0; i < this.m_ClassChallenges.Count; i++)
		{
			AdventureClassChallenge.ClassChallengeData classChallengeData = this.m_ClassChallenges[i];
			if (classChallengeData.unlocked)
			{
				GameObject gameObject = (GameObject)GameUtils.Instantiate(this.m_ClassChallengeButtonPrefab, this.m_ChallengeButtonContainer, false);
				gameObject.transform.localPosition = this.m_ClassChallengeButtonSpacing * (float)num;
				AdventureClassChallengeButton component = gameObject.GetComponent<AdventureClassChallengeButton>();
				component.m_Text.Text = classChallengeData.name;
				component.m_ScenarioID = classChallengeData.scenarioRecord.ID;
				bool flag = AdventureProgressMgr.Get().ScenarioHasRewardData(component.m_ScenarioID);
				component.m_Chest.SetActive(!classChallengeData.defeated && flag);
				component.m_Checkmark.SetActive(classChallengeData.defeated);
				component.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.ButtonPressed));
				this.LoadButtonPortrait(component, classChallengeData.heroID1);
				if (lastSelectedMission == component.m_ScenarioID || !this.m_SelectedButton)
				{
					this.m_SelectedButton = component;
				}
				num++;
			}
		}
		int num2 = 10 - num;
		if (num2 <= 0)
		{
			Debug.LogError(string.Format("Adventure Class Challenge tray UI doesn't support scrolling yet. More than {0} buttons where added.", 10));
			yield break;
		}
		for (int j = 0; j < num2; j++)
		{
			GameObject gameObject2 = (GameObject)GameUtils.Instantiate(this.m_EmptyChallengeButtonSlot, this.m_ChallengeButtonContainer, false);
			gameObject2.transform.localPosition = this.m_ClassChallengeButtonSpacing * (float)(num + j);
			gameObject2.transform.localRotation = Quaternion.identity;
			gameObject2.SetActive(true);
			gameObject2.GetComponentInChildren<Renderer>().GetMaterial().mainTextureOffset = new UnityEngine.Vector2(0f, this.EMPTY_SLOT_UV_OFFSET[this.m_UVoffset]);
			this.m_UVoffset++;
			if (this.m_UVoffset > 5)
			{
				this.m_UVoffset = 0;
			}
		}
		yield return null;
		if (this.m_SelectedButton == null)
		{
			Debug.LogError("AdventureClassChallenge.m_SelectedButton is null!\nThis it's likely that this means there are no valid class challenges available but we still tried to load the screen.");
			Navigation.RemoveHandler(new Navigation.NavigateBackHandler(AdventureClassChallenge.OnNavigateBack));
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			yield break;
		}
		this.SetSelectedButton(this.m_SelectedButton);
		this.m_SelectedButton.Select(false);
		this.GetRewardCardForSelectedScenario();
		this.m_PlayButton.Enable();
		if (this.m_ChallengeButtonScroller != null)
		{
			this.m_ChallengeButtonScroller.SetScrollHeightCallback(() => this.m_ChallengeButtonHeight * (float)this.m_ClassChallenges.Count, false, false);
		}
		base.GetComponent<AdventureSubScene>().SetIsLoaded(true);
		yield break;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00009B70 File Offset: 0x00007D70
	private void ButtonPressed(UIEvent e)
	{
		if (this.m_ChallengeButtonScroller != null && this.m_ChallengeButtonScroller.IsTouchDragging())
		{
			return;
		}
		AdventureClassChallengeButton adventureClassChallengeButton = (AdventureClassChallengeButton)e.GetElement();
		this.m_SelectedButton.Deselect();
		this.SetSelectedButton(adventureClassChallengeButton);
		adventureClassChallengeButton.Select(true);
		this.m_SelectedScenario = adventureClassChallengeButton.m_ScenarioID;
		this.m_SelectedButton = adventureClassChallengeButton;
		this.GetRewardCardForSelectedScenario();
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00009BD8 File Offset: 0x00007DD8
	private void SetSelectedButton(AdventureClassChallengeButton button)
	{
		int scenarioID = button.m_ScenarioID;
		AdventureConfig.Get().SetMission((ScenarioDbId)scenarioID, true);
		this.SetScenario(scenarioID);
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00009BFF File Offset: 0x00007DFF
	private void LoadButtonPortrait(AdventureClassChallengeButton button, string heroID)
	{
		DefLoader.Get().LoadCardDef(heroID, new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnButtonCardDefLoaded), button, null);
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00009C1C File Offset: 0x00007E1C
	private void OnButtonCardDefLoaded(string cardId, DefLoader.DisposableCardDef disposableCardDef, object userData)
	{
		AdventureClassChallengeButton adventureClassChallengeButton = (AdventureClassChallengeButton)userData;
		DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
		if (disposablesCleaner != null)
		{
			disposablesCleaner.Attach(adventureClassChallengeButton.gameObject, disposableCardDef);
		}
		Material practiceAIPortrait = disposableCardDef.CardDef.GetPracticeAIPortrait();
		if (practiceAIPortrait != null)
		{
			practiceAIPortrait.mainTexture = disposableCardDef.CardDef.GetPortraitTexture();
			adventureClassChallengeButton.SetPortraitMaterial(practiceAIPortrait);
		}
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00009C74 File Offset: 0x00007E74
	private void SetScenario(int scenarioID)
	{
		this.m_SelectedScenario = scenarioID;
		AdventureClassChallenge.ClassChallengeData classChallengeData = this.m_ClassChallenges[this.m_ScenarioChallengeLookup[scenarioID]];
		this.LoadHero(0, classChallengeData.heroID0);
		this.LoadHero(1, classChallengeData.heroID1);
		this.m_RightHeroName.Text = classChallengeData.opponentName;
		this.m_ChallengeTitle.Text = classChallengeData.title;
		if (classChallengeData.defeated)
		{
			this.m_ChallengeDescription.Text = classChallengeData.completedDescription;
		}
		else
		{
			this.m_ChallengeDescription.Text = classChallengeData.description;
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			bool flag = AdventureProgressMgr.Get().ScenarioHasRewardData(scenarioID);
			if (this.m_ClassChallenges[this.m_ScenarioChallengeLookup[scenarioID]].defeated || !flag)
			{
				this.m_ChestButton.gameObject.SetActive(false);
				this.m_ChestButtonCover.SetActive(true);
				return;
			}
			this.m_ChestButton.gameObject.SetActive(true);
			this.m_ChestButtonCover.SetActive(false);
		}
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00009D7C File Offset: 0x00007F7C
	private void LoadHero(int heroNum, string heroID)
	{
		AdventureClassChallenge.HeroLoadData heroLoadData = new AdventureClassChallenge.HeroLoadData();
		heroLoadData.heroNum = heroNum;
		heroLoadData.heroID = heroID;
		DefLoader.Get().LoadFullDef(heroID, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroFullDefLoaded), heroLoadData, null);
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00009DB8 File Offset: 0x00007FB8
	private void OnHeroFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		if (fullDef == null)
		{
			Debug.LogWarning(string.Format("AdventureClassChallenge.OnHeroFullDefLoaded() - FAILED to load \"{0}\"", cardId));
			return;
		}
		AdventureClassChallenge.HeroLoadData heroLoadData = (AdventureClassChallenge.HeroLoadData)userData;
		heroLoadData.fulldef = fullDef;
		AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", new PrefabCallback<GameObject>(this.OnActorLoaded), heroLoadData, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00009E0C File Offset: 0x0000800C
	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		AdventureClassChallenge.HeroLoadData heroLoadData = (AdventureClassChallenge.HeroLoadData)callbackData;
		using (heroLoadData.fulldef)
		{
			if (go == null)
			{
				Debug.LogWarning(string.Format("AdventureClassChallenge.OnActorLoaded() - FAILED to load actor \"{0}\"", assetRef));
			}
			else
			{
				Actor component = go.GetComponent<Actor>();
				if (component == null)
				{
					Debug.LogWarning(string.Format("AdventureClassChallenge.OnActorLoaded() - ERROR actor \"{0}\" has no Actor component", base.name));
				}
				else
				{
					component.TurnOffCollider();
					component.SetUnlit();
					UnityEngine.Object.Destroy(component.m_healthObject);
					UnityEngine.Object.Destroy(component.m_attackObject);
					component.SetEntityDef(heroLoadData.fulldef.EntityDef);
					component.SetCardDef(heroLoadData.fulldef.DisposableCardDef);
					component.SetPremium(TAG_PREMIUM.NORMAL);
					component.UpdateAllComponents();
					GameObject parent = this.m_LeftHeroContainer;
					if (heroLoadData.heroNum == 0)
					{
						UnityEngine.Object.Destroy(this.m_LeftHero);
						this.m_LeftHero = go;
						this.m_LeftHeroName.Text = heroLoadData.fulldef.EntityDef.GetName();
					}
					else
					{
						UnityEngine.Object.Destroy(this.m_RightHero);
						this.m_RightHero = go;
						parent = this.m_RightHeroContainer;
					}
					GameUtils.SetParent(component, parent, false);
					component.transform.localRotation = Quaternion.identity;
					component.transform.localScale = Vector3.one;
					component.GetAttackObject().Hide();
					component.Show();
				}
			}
		}
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00009F78 File Offset: 0x00008178
	private void OnVersusLettersLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError(string.Format("AdventureClassChallenge.OnVersusLettersLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		GameUtils.SetParent(go, this.m_VersusTextContainer, false);
		go.GetComponentInChildren<VS>().ActivateShadow(true);
		go.transform.localRotation = Quaternion.identity;
		go.transform.Rotate(new Vector3(0f, 180f, 0f));
		go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		Component[] componentsInChildren = go.GetComponentsInChildren(typeof(Renderer));
		for (int i = 0; i < componentsInChildren.Length - 1; i++)
		{
			((Renderer)componentsInChildren[i]).GetMaterial().SetColor("_Color", this.m_VersusTextColor);
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00004EA7 File Offset: 0x000030A7
	private static bool OnNavigateBack()
	{
		AdventureConfig.Get().SubSceneGoBack(true);
		return true;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void BackButton()
	{
		Navigation.GoBack();
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000A044 File Offset: 0x00008244
	private void Play()
	{
		this.m_PlayButton.Disable(false);
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, this.m_SelectedScenario, 0, 0L, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000A080 File Offset: 0x00008280
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (eventData.m_state == FindGameState.INVALID)
		{
			this.m_PlayButton.Enable();
		}
		return false;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000A0A4 File Offset: 0x000082A4
	private void GetRewardCardForSelectedScenario()
	{
		if (this.m_RewardBone == null)
		{
			return;
		}
		this.m_ChestButton.m_IsRewardLoading = true;
		List<RewardData> immediateRewardsForDefeatingScenario = AdventureProgressMgr.Get().GetImmediateRewardsForDefeatingScenario(this.m_SelectedScenario);
		if (immediateRewardsForDefeatingScenario != null && immediateRewardsForDefeatingScenario.Count > 0)
		{
			immediateRewardsForDefeatingScenario[0].LoadRewardObject(new Reward.DelOnRewardLoaded(this.RewardCardLoaded));
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000A104 File Offset: 0x00008304
	private void RewardCardLoaded(Reward reward, object callbackData)
	{
		if (reward == null)
		{
			Debug.LogWarning(string.Format("AdventureClassChallenge.RewardCardLoaded() - FAILED to load reward \"{0}\"", base.name));
			return;
		}
		if (reward.gameObject == null)
		{
			Debug.LogWarning(string.Format("AdventureClassChallenge.RewardCardLoaded() - Reward GameObject is null \"{0}\"", base.name));
			return;
		}
		reward.gameObject.transform.parent = this.m_ChestButton.transform;
		CardReward component = reward.GetComponent<CardReward>();
		if (this.m_ChestButton.m_RewardCard != null)
		{
			UnityEngine.Object.Destroy(this.m_ChestButton.m_RewardCard);
		}
		this.m_ChestButton.m_RewardCard = component.m_nonHeroCardsRoot;
		GameUtils.SetParent(component.m_nonHeroCardsRoot, this.m_RewardBone, false);
		component.m_nonHeroCardsRoot.SetActive(false);
		UnityEngine.Object.Destroy(component.gameObject);
		this.m_ChestButton.m_IsRewardLoading = false;
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000A1DF File Offset: 0x000083DF
	private void OnBoxTransitionFinished(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
	}

	// Token: 0x0400010B RID: 267
	private readonly float[] EMPTY_SLOT_UV_OFFSET = new float[]
	{
		0f,
		0.223f,
		0.377f,
		0.535f,
		0.69f,
		0.85f
	};

	// Token: 0x0400010C RID: 268
	private const float CHALLENGE_BUTTON_OFFSET = 4.3f;

	// Token: 0x0400010D RID: 269
	private const int VISIBLE_SLOT_COUNT = 10;

	// Token: 0x0400010E RID: 270
	[CustomEditField(Sections = "DBF Stuff")]
	public UberText m_ModeName;

	// Token: 0x0400010F RID: 271
	[CustomEditField(Sections = "Class Challenge Buttons")]
	public GameObject m_ClassChallengeButtonPrefab;

	// Token: 0x04000110 RID: 272
	[CustomEditField(Sections = "Class Challenge Buttons")]
	public Vector3 m_ClassChallengeButtonSpacing;

	// Token: 0x04000111 RID: 273
	[CustomEditField(Sections = "Class Challenge Buttons")]
	public GameObject m_ChallengeButtonContainer;

	// Token: 0x04000112 RID: 274
	[CustomEditField(Sections = "Class Challenge Buttons")]
	public GameObject m_EmptyChallengeButtonSlot;

	// Token: 0x04000113 RID: 275
	[CustomEditField(Sections = "Class Challenge Buttons")]
	public float m_ChallengeButtonHeight;

	// Token: 0x04000114 RID: 276
	[CustomEditField(Sections = "Class Challenge Buttons")]
	public UIBScrollable m_ChallengeButtonScroller;

	// Token: 0x04000115 RID: 277
	[CustomEditField(Sections = "Hero Portraits")]
	public GameObject m_LeftHeroContainer;

	// Token: 0x04000116 RID: 278
	[CustomEditField(Sections = "Hero Portraits")]
	public GameObject m_RightHeroContainer;

	// Token: 0x04000117 RID: 279
	[CustomEditField(Sections = "Hero Portraits")]
	public UberText m_LeftHeroName;

	// Token: 0x04000118 RID: 280
	[CustomEditField(Sections = "Hero Portraits")]
	public UberText m_RightHeroName;

	// Token: 0x04000119 RID: 281
	[CustomEditField(Sections = "Versus Text", T = EditType.GAME_OBJECT)]
	public string m_VersusTextPrefab;

	// Token: 0x0400011A RID: 282
	[CustomEditField(Sections = "Versus Text")]
	public GameObject m_VersusTextContainer;

	// Token: 0x0400011B RID: 283
	[CustomEditField(Sections = "Versus Text")]
	public Color m_VersusTextColor;

	// Token: 0x0400011C RID: 284
	[CustomEditField(Sections = "Text")]
	public UberText m_ChallengeTitle;

	// Token: 0x0400011D RID: 285
	[CustomEditField(Sections = "Text")]
	public UberText m_ChallengeDescription;

	// Token: 0x0400011E RID: 286
	[CustomEditField(Sections = "Basic UI")]
	public PlayButton m_PlayButton;

	// Token: 0x0400011F RID: 287
	[CustomEditField(Sections = "Basic UI")]
	public UIBButton m_BackButton;

	// Token: 0x04000120 RID: 288
	[CustomEditField(Sections = "Reward UI")]
	public AdventureClassChallengeChestButton m_ChestButton;

	// Token: 0x04000121 RID: 289
	[CustomEditField(Sections = "Reward UI")]
	public GameObject m_ChestButtonCover;

	// Token: 0x04000122 RID: 290
	[CustomEditField(Sections = "Reward UI")]
	public Transform m_RewardBone;

	// Token: 0x04000123 RID: 291
	private List<AdventureClassChallenge.ClassChallengeData> m_ClassChallenges = new List<AdventureClassChallenge.ClassChallengeData>();

	// Token: 0x04000124 RID: 292
	private Map<int, int> m_ScenarioChallengeLookup = new Map<int, int>();

	// Token: 0x04000125 RID: 293
	private int m_UVoffset;

	// Token: 0x04000126 RID: 294
	private AdventureClassChallengeButton m_SelectedButton;

	// Token: 0x04000127 RID: 295
	private GameObject m_LeftHero;

	// Token: 0x04000128 RID: 296
	private GameObject m_RightHero;

	// Token: 0x04000129 RID: 297
	private int m_SelectedScenario;

	// Token: 0x0400012A RID: 298
	private bool m_gameDenied;

	// Token: 0x02001297 RID: 4759
	private class ClassChallengeData
	{
		// Token: 0x0400A3EA RID: 41962
		public ScenarioDbfRecord scenarioRecord;

		// Token: 0x0400A3EB RID: 41963
		public bool unlocked;

		// Token: 0x0400A3EC RID: 41964
		public bool defeated;

		// Token: 0x0400A3ED RID: 41965
		public string heroID0;

		// Token: 0x0400A3EE RID: 41966
		public string heroID1;

		// Token: 0x0400A3EF RID: 41967
		public string name;

		// Token: 0x0400A3F0 RID: 41968
		public string title;

		// Token: 0x0400A3F1 RID: 41969
		public string description;

		// Token: 0x0400A3F2 RID: 41970
		public string completedDescription;

		// Token: 0x0400A3F3 RID: 41971
		public string opponentName;
	}

	// Token: 0x02001298 RID: 4760
	private class HeroLoadData
	{
		// Token: 0x0400A3F4 RID: 41972
		public int heroNum;

		// Token: 0x0400A3F5 RID: 41973
		public string heroID;

		// Token: 0x0400A3F6 RID: 41974
		public DefLoader.DisposableFullDef fulldef;
	}
}
