using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class AdventureClassChallenge : MonoBehaviour
{
	private class ClassChallengeData
	{
		public ScenarioDbfRecord scenarioRecord;

		public bool unlocked;

		public bool defeated;

		public string heroID0;

		public string heroID1;

		public string name;

		public string title;

		public string description;

		public string completedDescription;

		public string opponentName;
	}

	private class HeroLoadData
	{
		public int heroNum;

		public string heroID;

		public DefLoader.DisposableFullDef fulldef;
	}

	private readonly float[] EMPTY_SLOT_UV_OFFSET = new float[6] { 0f, 0.223f, 0.377f, 0.535f, 0.69f, 0.85f };

	private const float CHALLENGE_BUTTON_OFFSET = 4.3f;

	private const int VISIBLE_SLOT_COUNT = 10;

	[CustomEditField(Sections = "DBF Stuff")]
	public UberText m_ModeName;

	[CustomEditField(Sections = "Class Challenge Buttons")]
	public GameObject m_ClassChallengeButtonPrefab;

	[CustomEditField(Sections = "Class Challenge Buttons")]
	public Vector3 m_ClassChallengeButtonSpacing;

	[CustomEditField(Sections = "Class Challenge Buttons")]
	public GameObject m_ChallengeButtonContainer;

	[CustomEditField(Sections = "Class Challenge Buttons")]
	public GameObject m_EmptyChallengeButtonSlot;

	[CustomEditField(Sections = "Class Challenge Buttons")]
	public float m_ChallengeButtonHeight;

	[CustomEditField(Sections = "Class Challenge Buttons")]
	public UIBScrollable m_ChallengeButtonScroller;

	[CustomEditField(Sections = "Hero Portraits")]
	public GameObject m_LeftHeroContainer;

	[CustomEditField(Sections = "Hero Portraits")]
	public GameObject m_RightHeroContainer;

	[CustomEditField(Sections = "Hero Portraits")]
	public UberText m_LeftHeroName;

	[CustomEditField(Sections = "Hero Portraits")]
	public UberText m_RightHeroName;

	[CustomEditField(Sections = "Versus Text", T = EditType.GAME_OBJECT)]
	public string m_VersusTextPrefab;

	[CustomEditField(Sections = "Versus Text")]
	public GameObject m_VersusTextContainer;

	[CustomEditField(Sections = "Versus Text")]
	public Color m_VersusTextColor;

	[CustomEditField(Sections = "Text")]
	public UberText m_ChallengeTitle;

	[CustomEditField(Sections = "Text")]
	public UberText m_ChallengeDescription;

	[CustomEditField(Sections = "Basic UI")]
	public PlayButton m_PlayButton;

	[CustomEditField(Sections = "Basic UI")]
	public UIBButton m_BackButton;

	[CustomEditField(Sections = "Reward UI")]
	public AdventureClassChallengeChestButton m_ChestButton;

	[CustomEditField(Sections = "Reward UI")]
	public GameObject m_ChestButtonCover;

	[CustomEditField(Sections = "Reward UI")]
	public Transform m_RewardBone;

	private List<ClassChallengeData> m_ClassChallenges = new List<ClassChallengeData>();

	private Map<int, int> m_ScenarioChallengeLookup = new Map<int, int>();

	private int m_UVoffset;

	private AdventureClassChallengeButton m_SelectedButton;

	private GameObject m_LeftHero;

	private GameObject m_RightHero;

	private int m_SelectedScenario;

	private bool m_gameDenied;

	private void Awake()
	{
		base.transform.position = new Vector3(-500f, 0f, 0f);
		m_BackButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			BackButton();
		});
		m_PlayButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			Play();
		});
		m_EmptyChallengeButtonSlot.SetActive(value: false);
		AssetLoader.Get().InstantiatePrefab(m_VersusTextPrefab, OnVersusLettersLoaded);
	}

	private void Start()
	{
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		InitModeName();
		InitAdventureChallenges();
		Box.Get().AddTransitionFinishedListener(OnBoxTransitionFinished);
		Navigation.PushUnique(OnNavigateBack);
		StartCoroutine(CreateChallengeButtons());
	}

	private void OnDestroy()
	{
		GameMgr.Get().UnregisterFindGameEvent(OnFindGameEvent);
	}

	private void InitModeName()
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		int selectedMode = (int)AdventureConfig.Get().GetSelectedMode();
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdventure, selectedMode);
		string text = (UniversalInputManager.UsePhoneUI ? adventureDataRecord.ShortName : adventureDataRecord.Name);
		m_ModeName.Text = text;
	}

	private void InitAdventureChallenges()
	{
		List<ScenarioDbfRecord> records = GameDbf.Scenario.GetRecords();
		records.Sort(delegate(ScenarioDbfRecord a, ScenarioDbfRecord b)
		{
			int sortOrder = a.SortOrder;
			int sortOrder2 = b.SortOrder;
			return sortOrder - sortOrder2;
		});
		foreach (ScenarioDbfRecord item in records)
		{
			if (item.AdventureId == (int)AdventureConfig.Get().GetSelectedAdventure() && item.ModeId == 4)
			{
				int player1HeroCardId = item.Player1HeroCardId;
				int num = item.ClientPlayer2HeroCardId;
				if (num == 0)
				{
					num = item.Player2HeroCardId;
				}
				ClassChallengeData classChallengeData = new ClassChallengeData();
				classChallengeData.scenarioRecord = item;
				classChallengeData.heroID0 = GameUtils.TranslateDbIdToCardId(player1HeroCardId);
				classChallengeData.heroID1 = GameUtils.TranslateDbIdToCardId(num);
				classChallengeData.unlocked = AdventureProgressMgr.Get().CanPlayScenario(item.ID);
				if (AdventureProgressMgr.Get().HasDefeatedScenario(item.ID))
				{
					classChallengeData.defeated = true;
				}
				else
				{
					classChallengeData.defeated = false;
				}
				classChallengeData.name = item.ShortName;
				classChallengeData.title = item.Name;
				classChallengeData.description = (((bool)UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(item.ShortDescription)) ? item.ShortDescription : item.Description);
				classChallengeData.completedDescription = item.CompletedDescription;
				classChallengeData.opponentName = item.OpponentName;
				m_ScenarioChallengeLookup.Add(item.ID, m_ClassChallenges.Count);
				m_ClassChallenges.Add(classChallengeData);
			}
		}
	}

	private int BossCreateParamsSortComparison(ClassChallengeData data1, ClassChallengeData data2)
	{
		return GameUtils.MissionSortComparison(data1.scenarioRecord, data2.scenarioRecord);
	}

	private IEnumerator CreateChallengeButtons()
	{
		int num = 0;
		int lastSelectedMission = (int)AdventureConfig.Get().GetLastSelectedMission();
		for (int i = 0; i < m_ClassChallenges.Count; i++)
		{
			ClassChallengeData classChallengeData = m_ClassChallenges[i];
			if (classChallengeData.unlocked)
			{
				GameObject obj = (GameObject)GameUtils.Instantiate(m_ClassChallengeButtonPrefab, m_ChallengeButtonContainer);
				obj.transform.localPosition = m_ClassChallengeButtonSpacing * num;
				AdventureClassChallengeButton component = obj.GetComponent<AdventureClassChallengeButton>();
				component.m_Text.Text = classChallengeData.name;
				component.m_ScenarioID = classChallengeData.scenarioRecord.ID;
				bool flag = AdventureProgressMgr.Get().ScenarioHasRewardData(component.m_ScenarioID);
				component.m_Chest.SetActive(!classChallengeData.defeated && flag);
				component.m_Checkmark.SetActive(classChallengeData.defeated);
				component.AddEventListener(UIEventType.RELEASE, ButtonPressed);
				LoadButtonPortrait(component, classChallengeData.heroID1);
				if (lastSelectedMission == component.m_ScenarioID || !m_SelectedButton)
				{
					m_SelectedButton = component;
				}
				num++;
			}
		}
		int num2 = 10 - num;
		if (num2 <= 0)
		{
			Debug.LogError($"Adventure Class Challenge tray UI doesn't support scrolling yet. More than {10} buttons where added.");
			yield break;
		}
		for (int j = 0; j < num2; j++)
		{
			GameObject obj2 = (GameObject)GameUtils.Instantiate(m_EmptyChallengeButtonSlot, m_ChallengeButtonContainer);
			obj2.transform.localPosition = m_ClassChallengeButtonSpacing * (num + j);
			obj2.transform.localRotation = Quaternion.identity;
			obj2.SetActive(value: true);
			obj2.GetComponentInChildren<Renderer>().GetMaterial().mainTextureOffset = new UnityEngine.Vector2(0f, EMPTY_SLOT_UV_OFFSET[m_UVoffset]);
			m_UVoffset++;
			if (m_UVoffset > 5)
			{
				m_UVoffset = 0;
			}
		}
		yield return null;
		if (m_SelectedButton == null)
		{
			Debug.LogError("AdventureClassChallenge.m_SelectedButton is null!\nThis it's likely that this means there are no valid class challenges available but we still tried to load the screen.");
			Navigation.RemoveHandler(OnNavigateBack);
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			yield break;
		}
		SetSelectedButton(m_SelectedButton);
		m_SelectedButton.Select(playSound: false);
		GetRewardCardForSelectedScenario();
		m_PlayButton.Enable();
		if (m_ChallengeButtonScroller != null)
		{
			m_ChallengeButtonScroller.SetScrollHeightCallback(() => m_ChallengeButtonHeight * (float)m_ClassChallenges.Count);
		}
		GetComponent<AdventureSubScene>().SetIsLoaded(loaded: true);
	}

	private void ButtonPressed(UIEvent e)
	{
		if (!(m_ChallengeButtonScroller != null) || !m_ChallengeButtonScroller.IsTouchDragging())
		{
			AdventureClassChallengeButton adventureClassChallengeButton = (AdventureClassChallengeButton)e.GetElement();
			m_SelectedButton.Deselect();
			SetSelectedButton(adventureClassChallengeButton);
			adventureClassChallengeButton.Select(playSound: true);
			m_SelectedScenario = adventureClassChallengeButton.m_ScenarioID;
			m_SelectedButton = adventureClassChallengeButton;
			GetRewardCardForSelectedScenario();
		}
	}

	private void SetSelectedButton(AdventureClassChallengeButton button)
	{
		int scenarioID = button.m_ScenarioID;
		AdventureConfig.Get().SetMission((ScenarioDbId)scenarioID);
		SetScenario(scenarioID);
	}

	private void LoadButtonPortrait(AdventureClassChallengeButton button, string heroID)
	{
		DefLoader.Get().LoadCardDef(heroID, OnButtonCardDefLoaded, button);
	}

	private void OnButtonCardDefLoaded(string cardId, DefLoader.DisposableCardDef disposableCardDef, object userData)
	{
		AdventureClassChallengeButton adventureClassChallengeButton = (AdventureClassChallengeButton)userData;
		HearthstoneServices.Get<DisposablesCleaner>()?.Attach(adventureClassChallengeButton.gameObject, disposableCardDef);
		Material practiceAIPortrait = disposableCardDef.CardDef.GetPracticeAIPortrait();
		if (practiceAIPortrait != null)
		{
			practiceAIPortrait.mainTexture = disposableCardDef.CardDef.GetPortraitTexture();
			adventureClassChallengeButton.SetPortraitMaterial(practiceAIPortrait);
		}
	}

	private void SetScenario(int scenarioID)
	{
		m_SelectedScenario = scenarioID;
		ClassChallengeData classChallengeData = m_ClassChallenges[m_ScenarioChallengeLookup[scenarioID]];
		LoadHero(0, classChallengeData.heroID0);
		LoadHero(1, classChallengeData.heroID1);
		m_RightHeroName.Text = classChallengeData.opponentName;
		m_ChallengeTitle.Text = classChallengeData.title;
		if (classChallengeData.defeated)
		{
			m_ChallengeDescription.Text = classChallengeData.completedDescription;
		}
		else
		{
			m_ChallengeDescription.Text = classChallengeData.description;
		}
		if (!UniversalInputManager.UsePhoneUI)
		{
			bool flag = AdventureProgressMgr.Get().ScenarioHasRewardData(scenarioID);
			if (m_ClassChallenges[m_ScenarioChallengeLookup[scenarioID]].defeated || !flag)
			{
				m_ChestButton.gameObject.SetActive(value: false);
				m_ChestButtonCover.SetActive(value: true);
			}
			else
			{
				m_ChestButton.gameObject.SetActive(value: true);
				m_ChestButtonCover.SetActive(value: false);
			}
		}
	}

	private void LoadHero(int heroNum, string heroID)
	{
		HeroLoadData heroLoadData = new HeroLoadData();
		heroLoadData.heroNum = heroNum;
		heroLoadData.heroID = heroID;
		DefLoader.Get().LoadFullDef(heroID, OnHeroFullDefLoaded, heroLoadData);
	}

	private void OnHeroFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		if (fullDef == null)
		{
			Debug.LogWarning($"AdventureClassChallenge.OnHeroFullDefLoaded() - FAILED to load \"{cardId}\"");
			return;
		}
		HeroLoadData heroLoadData = (HeroLoadData)userData;
		heroLoadData.fulldef = fullDef;
		AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", OnActorLoaded, heroLoadData, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void OnActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		HeroLoadData heroLoadData = (HeroLoadData)callbackData;
		using (heroLoadData.fulldef)
		{
			if (go == null)
			{
				Debug.LogWarning($"AdventureClassChallenge.OnActorLoaded() - FAILED to load actor \"{assetRef}\"");
				return;
			}
			Actor component = go.GetComponent<Actor>();
			if (component == null)
			{
				Debug.LogWarning($"AdventureClassChallenge.OnActorLoaded() - ERROR actor \"{base.name}\" has no Actor component");
				return;
			}
			component.TurnOffCollider();
			component.SetUnlit();
			Object.Destroy(component.m_healthObject);
			Object.Destroy(component.m_attackObject);
			component.SetEntityDef(heroLoadData.fulldef.EntityDef);
			component.SetCardDef(heroLoadData.fulldef.DisposableCardDef);
			component.SetPremium(TAG_PREMIUM.NORMAL);
			component.UpdateAllComponents();
			GameObject parent = m_LeftHeroContainer;
			if (heroLoadData.heroNum == 0)
			{
				Object.Destroy(m_LeftHero);
				m_LeftHero = go;
				m_LeftHeroName.Text = heroLoadData.fulldef.EntityDef.GetName();
			}
			else
			{
				Object.Destroy(m_RightHero);
				m_RightHero = go;
				parent = m_RightHeroContainer;
			}
			GameUtils.SetParent(component, parent);
			component.transform.localRotation = Quaternion.identity;
			component.transform.localScale = Vector3.one;
			component.GetAttackObject().Hide();
			component.Show();
		}
	}

	private void OnVersusLettersLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogError($"AdventureClassChallenge.OnVersusLettersLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		GameUtils.SetParent(go, m_VersusTextContainer);
		go.GetComponentInChildren<VS>().ActivateShadow();
		go.transform.localRotation = Quaternion.identity;
		go.transform.Rotate(new Vector3(0f, 180f, 0f));
		go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		Component[] componentsInChildren = go.GetComponentsInChildren(typeof(Renderer));
		for (int i = 0; i < componentsInChildren.Length - 1; i++)
		{
			((Renderer)componentsInChildren[i]).GetMaterial().SetColor("_Color", m_VersusTextColor);
		}
	}

	private static bool OnNavigateBack()
	{
		AdventureConfig.Get().SubSceneGoBack();
		return true;
	}

	private void BackButton()
	{
		Navigation.GoBack();
	}

	private void Play()
	{
		m_PlayButton.Disable();
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, m_SelectedScenario, 0, 0L);
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		if (eventData.m_state == FindGameState.INVALID)
		{
			m_PlayButton.Enable();
		}
		return false;
	}

	private void GetRewardCardForSelectedScenario()
	{
		if (!(m_RewardBone == null))
		{
			m_ChestButton.m_IsRewardLoading = true;
			List<RewardData> immediateRewardsForDefeatingScenario = AdventureProgressMgr.Get().GetImmediateRewardsForDefeatingScenario(m_SelectedScenario);
			if (immediateRewardsForDefeatingScenario != null && immediateRewardsForDefeatingScenario.Count > 0)
			{
				immediateRewardsForDefeatingScenario[0].LoadRewardObject(RewardCardLoaded);
			}
		}
	}

	private void RewardCardLoaded(Reward reward, object callbackData)
	{
		if (reward == null)
		{
			Debug.LogWarning($"AdventureClassChallenge.RewardCardLoaded() - FAILED to load reward \"{base.name}\"");
			return;
		}
		if (reward.gameObject == null)
		{
			Debug.LogWarning($"AdventureClassChallenge.RewardCardLoaded() - Reward GameObject is null \"{base.name}\"");
			return;
		}
		reward.gameObject.transform.parent = m_ChestButton.transform;
		CardReward component = reward.GetComponent<CardReward>();
		if (m_ChestButton.m_RewardCard != null)
		{
			Object.Destroy(m_ChestButton.m_RewardCard);
		}
		m_ChestButton.m_RewardCard = component.m_nonHeroCardsRoot;
		GameUtils.SetParent(component.m_nonHeroCardsRoot, m_RewardBone);
		component.m_nonHeroCardsRoot.SetActive(value: false);
		Object.Destroy(component.gameObject);
		m_ChestButton.m_IsRewardLoading = false;
	}

	private void OnBoxTransitionFinished(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(OnBoxTransitionFinished);
	}
}
