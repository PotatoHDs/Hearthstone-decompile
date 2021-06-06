using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

public abstract class AbsDeckPickerTrayDisplay : MonoBehaviour
{
	public delegate void DeckTrayLoaded();

	public delegate void FormatTypePickerClosed();

	protected class HeroFullDefLoadedCallbackData
	{
		public HeroPickerButton HeroPickerButton { get; private set; }

		public TAG_PREMIUM Premium { get; private set; }

		public HeroFullDefLoadedCallbackData(HeroPickerButton button, TAG_PREMIUM premium)
		{
			HeroPickerButton = button;
			Premium = premium;
		}
	}

	protected static readonly PlatformDependentValue<bool> HIGHLIGHT_SELECTED_DECK = new PlatformDependentValue<bool>(PlatformCategory.Screen)
	{
		Phone = false,
		Tablet = true,
		PC = true
	};

	private const string PLAYBUTTON_FIRESIDE_LATERN_EVENT = "LANTERN";

	public GameObject m_randomDeckPickerTray;

	public Transform m_Hero_Bone;

	public Transform m_Hero_BoneDown;

	public Transform m_HeroName_Bone;

	public Transform m_Ranked_Hero_Bone;

	public Transform m_Ranked_Hero_BoneDown;

	public Transform m_Ranked_HeroName_Bone;

	public Transform m_HeroPower_Bone;

	public Transform m_HeroPower_BoneDown;

	public AsyncReference m_playButtonWidgetReference;

	public UberText m_heroName;

	[CustomEditField(Sections = "Hero Button Placement")]
	public List<GameObject> m_heroPickerButtonBonesByHeroCount;

	public float m_heroPickerButtonHorizontalSpacing;

	public float m_heroPickerButtonVerticalSpacing;

	public GameObject m_hierarchyDetails;

	public GameObject m_basicDeckPageContainer;

	public GameObject m_tooltipPrefab;

	public Transform m_tooltipBone;

	public UberText m_modeName;

	public UIBButton m_backButton;

	public UIBButton m_collectionButton;

	public GameObject m_basicDeckPage;

	public GameObject m_trayFrame;

	public GameObject m_randomDecksShownBone;

	public GameObject m_heroPowerContainer;

	public GameObject m_heroPowerShadowQuad;

	[CustomEditField(Sections = "Prefab References", T = EditType.GAME_OBJECT)]
	public string m_heroButtonWidgetPrefab;

	[CustomEditField(Sections = "Prefab References", T = EditType.GAME_OBJECT)]
	public string m_heroPickerCrownPrefab;

	protected FormatType m_PreviousFormatType;

	protected bool m_PreviousInRankedPlayMode;

	protected bool m_isMouseOverHeroPower;

	private bool m_playButtonEnabled;

	private bool m_heroRaised = true;

	protected int m_heroDefsLoading = int.MaxValue;

	protected int m_HeroPickerButtonCount;

	protected List<HeroPickerButton> m_heroButtons = new List<HeroPickerButton>();

	protected Map<string, DefLoader.DisposableFullDef> m_heroPowerDefs = new Map<string, DefLoader.DisposableFullDef>();

	protected List<DeckTrayLoaded> m_DeckTrayLoadedListeners = new List<DeckTrayLoaded>();

	protected List<FormatTypePickerClosed> m_FormatTypePickerClosedListeners = new List<FormatTypePickerClosed>();

	protected string m_selectedHeroName;

	protected bool m_Loaded;

	protected TooltipPanel m_heroLockedTooltip;

	protected DefLoader.DisposableFullDef m_selectedHeroPowerFullDef;

	protected HeroPickerButton m_selectedHeroButton;

	protected SlidingTray m_slidingTray;

	protected PlayButton m_playButton;

	private AudioSource m_lastPickLine;

	protected PegUIElement m_heroPower;

	protected PegUIElement m_goldenHeroPower;

	protected Actor m_heroActor;

	protected Actor m_heroPowerActor;

	protected Actor m_goldenHeroPowerActor;

	protected Actor m_heroPowerBigCard;

	protected Actor m_goldenHeroPowerBigCard;

	protected List<Transform> m_heroBones;

	protected List<TAG_CLASS> m_validClasses = new List<TAG_CLASS>();

	public Transform empty;

	public virtual void Awake()
	{
		m_randomDeckPickerTray.transform.localPosition = m_randomDecksShownBone.transform.localPosition;
		DeckPickerTray.Get().SetDeckPickerTrayDisplayReference(this);
		DeckPickerTray.Get().RegisterHandlers();
		if (m_backButton != null)
		{
			m_backButton.SetText(GameStrings.Get("GLOBAL_BACK"));
			m_backButton.AddEventListener(UIEventType.RELEASE, OnBackButtonReleased);
		}
		m_playButtonWidgetReference.RegisterReadyListener<VisualController>(OnPlayButtonWidgetReady);
		if (m_heroPowerShadowQuad != null)
		{
			m_heroPowerShadowQuad.SetActive(value: false);
		}
		if (m_heroName != null)
		{
			m_heroName.RichText = false;
			m_heroName.Text = "";
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_slidingTray = base.gameObject.GetComponentInChildren<SlidingTray>();
			m_slidingTray.RegisterTrayToggleListener(OnSlidingTrayToggled);
		}
		PopupDisplayManager.Get().AddPopupShownListener(OnPopupShown);
	}

	protected virtual void OnDestroy()
	{
		PopupDisplayManager.Get()?.RemovePopupShownListener(OnPopupShown);
		if (DeckPickerTray.IsInitialized())
		{
			DeckPickerTray.Get().UnregisterHandlers();
		}
		m_heroPowerDefs.DisposeValuesAndClear();
		m_selectedHeroPowerFullDef?.Dispose();
		m_selectedHeroPowerFullDef = null;
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (GameMgr.Get().IsFindingGame())
		{
			GameMgr.Get().CancelFindGame();
		}
	}

	public virtual void HandleGameStartupFailure()
	{
		SetPlayButtonEnabled(enable: true);
		SetBackButtonEnabled(enable: true);
		SetHeroButtonsEnabled(enable: true);
		SetHeroRaised(raised: true);
	}

	public virtual void OnServerGameStarted()
	{
		FriendChallengeMgr.Get().RemoveChangedListener(OnFriendChallengeChanged);
	}

	public virtual void OnServerGameCanceled()
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY && !TavernBrawlManager.IsInTavernBrawlFriendlyChallenge())
		{
			HandleGameStartupFailure();
		}
	}

	protected virtual bool OnNavigateBackImplementation()
	{
		if (!m_backButton.IsEnabled())
		{
			return false;
		}
		SceneMgr.Mode mode = ((SceneMgr.Get() != null) ? SceneMgr.Get().GetMode() : SceneMgr.Mode.INVALID);
		if (mode == SceneMgr.Mode.FRIENDLY)
		{
			if (FiresideGatheringManager.Get() != null && FiresideGatheringManager.Get().CurrentFiresideGatheringMode != 0)
			{
				BackOutToFiresideGathering();
			}
			else
			{
				BackOutToHub();
			}
			if (FriendChallengeMgr.Get() != null)
			{
				FriendChallengeMgr.Get().CancelChallenge();
			}
		}
		SetPlayButtonEnabled(enable: false);
		SetBackButtonEnabled(enable: false);
		SetHeroButtonsEnabled(enable: false);
		GameMgr.Get().CancelFindGame();
		SoundManager.Get().Stop(m_lastPickLine);
		return true;
	}

	protected virtual void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"AbsDeckPickerTrayDisplay.OnHeroActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		m_heroActor = go.GetComponent<Actor>();
		if (m_heroActor == null)
		{
			Debug.LogWarning($"AbsDeckPickerTrayDisplay.OnHeroActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		go.transform.parent = m_hierarchyDetails.transform;
		UpdateHeroActorOrientation();
		m_heroActor.SetUnlit();
		Object.Destroy(m_heroActor.m_healthObject);
		Object.Destroy(m_heroActor.m_attackObject);
		m_heroActor.Hide();
	}

	protected virtual void OnHeroFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		using (fullDef)
		{
			EntityDef entityDef = fullDef?.EntityDef;
			if (entityDef != null)
			{
				HeroFullDefLoadedCallbackData heroFullDefLoadedCallbackData = userData as HeroFullDefLoadedCallbackData;
				TAG_PREMIUM premium = ((entityDef.GetCardSet() == TAG_CARD_SET.HERO_SKINS) ? TAG_PREMIUM.GOLDEN : CollectionManager.Get().GetBestCardPremium(cardId));
				heroFullDefLoadedCallbackData.HeroPickerButton.UpdateDisplay(fullDef, premium);
				Vector3 originalLocalPosition = ((heroFullDefLoadedCallbackData.HeroPickerButton.m_raiseAndLowerRoot != null) ? heroFullDefLoadedCallbackData.HeroPickerButton.m_raiseAndLowerRoot.transform.localPosition : heroFullDefLoadedCallbackData.HeroPickerButton.transform.localPosition);
				heroFullDefLoadedCallbackData.HeroPickerButton.SetOriginalLocalPosition(originalLocalPosition);
				if (entityDef.GetClass() != TAG_CLASS.WHIZBANG)
				{
					string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(entityDef.GetCardId());
					if (!string.IsNullOrEmpty(heroPowerCardIdFromHero))
					{
						LoadHeroPowerDef(heroPowerCardIdFromHero);
					}
					else
					{
						Debug.LogErrorFormat("No hero power set up for hero {0}", entityDef.GetCardId());
					}
				}
			}
			m_heroDefsLoading--;
		}
	}

	protected virtual void OnSlidingTrayToggled(bool isShowing)
	{
		if (!isShowing && PracticePickerTrayDisplay.Get() != null && PracticePickerTrayDisplay.Get().IsShown())
		{
			Navigation.GoBack();
		}
	}

	public virtual void ResetCurrentMode()
	{
		if (m_selectedHeroButton != null)
		{
			SetPlayButtonEnabled(enable: true);
			SetHeroRaised(raised: true);
		}
		else
		{
			SetPlayButtonEnabled(enable: false);
		}
		SetHeroButtonsEnabled(enable: true);
	}

	public virtual void PreUnload()
	{
	}

	public virtual void InitAssets()
	{
		Log.PlayModeInvestigation.PrintInfo("AbsDeckPickerTrayDisplay.InitAssets() called");
		m_PreviousFormatType = Options.GetFormatType();
		m_PreviousInRankedPlayMode = Options.GetInRankedPlayMode();
		m_HeroPickerButtonCount = ValidateHeroCount();
		SetupHeroLayout();
		LoadHero();
		if (ShouldShowHeroPower())
		{
			LoadHeroPower();
			LoadGoldenHeroPower();
		}
		StartCoroutine(LoadHeroButtons());
		StartCoroutine(InitModeWhenReady());
	}

	protected virtual IEnumerator WaitForHeroPickerButtonsLoaded()
	{
		while (m_heroButtons.Count < m_HeroPickerButtonCount)
		{
			yield return null;
		}
		foreach (HeroPickerButton button in m_heroButtons)
		{
			while (button.GetComponent<WidgetTemplate>().IsChangingStates)
			{
				yield return null;
			}
		}
	}

	protected virtual IEnumerator InitDeckDependentElements()
	{
		yield return StartCoroutine(WaitForHeroPickerButtonsLoaded());
		InitForMode(SceneMgr.Get().GetMode());
	}

	protected virtual IEnumerator InitHeroPickerElements()
	{
		yield return StartCoroutine(WaitForHeroPickerButtonsLoaded());
		InitHeroPickerButtons();
	}

	protected virtual IEnumerator InitModeWhenReady()
	{
		while (m_heroDefsLoading > 0 || m_heroActor == null || ((m_heroPowerActor == null || m_goldenHeroPowerActor == null) && ShouldShowHeroPower()))
		{
			yield return null;
		}
		m_Loaded = true;
		PlayGameScene playGameScene = SceneMgr.Get().GetScene() as PlayGameScene;
		if (playGameScene != null)
		{
			playGameScene.OnDeckPickerLoaded(this);
		}
		FireDeckTrayLoadedEvent();
		InitRichPresence();
		SetBackButtonEnabled(enable: true);
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY || TavernBrawlManager.IsInTavernBrawlFriendlyChallenge())
		{
			if (FriendChallengeMgr.Get().HasChallenge())
			{
				FriendChallengeMgr.Get().AddChangedListener(OnFriendChallengeChanged);
			}
			else
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Get().GetPrevMode());
			}
		}
	}

	protected virtual void InitForMode(SceneMgr.Mode mode)
	{
		switch (mode)
		{
		case SceneMgr.Mode.ADVENTURE:
			if (AdventureConfig.Get() != null && AdventureConfig.Get().IsHeroSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
			{
				SetPlayButtonText(GameStrings.Get("GLUE_CHOOSE"));
			}
			break;
		case SceneMgr.Mode.COLLECTIONMANAGER:
			SetPlayButtonText(GameStrings.Get("GLUE_CHOOSE"));
			break;
		case SceneMgr.Mode.FRIENDLY:
		case SceneMgr.Mode.FIRESIDE_GATHERING:
		{
			if (FiresideGatheringManager.Get().InBrawlMode())
			{
				SetHeaderForTavernBrawl();
				break;
			}
			string key = ((mode == SceneMgr.Mode.FIRESIDE_GATHERING) ? "GLUE_CHOOSE_OPPONENT" : "GLUE_CHOOSE");
			SetPlayButtonText(GameStrings.Get(key));
			break;
		}
		}
	}

	protected virtual void InitHeroPickerButtons()
	{
	}

	protected virtual void InitRichPresence(Global.PresenceStatus? newStatus = null)
	{
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.ADVENTURE:
		{
			AdventureData.Adventuresubscene currentSubScene = AdventureConfig.Get().CurrentSubScene;
			if (currentSubScene == AdventureData.Adventuresubscene.PRACTICE)
			{
				newStatus = Global.PresenceStatus.PRACTICE_DECKPICKER;
			}
			break;
		}
		case SceneMgr.Mode.FRIENDLY:
			newStatus = Global.PresenceStatus.FRIENDLY_DECKPICKER;
			if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				newStatus = Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING;
			}
			break;
		case SceneMgr.Mode.TAVERN_BRAWL:
			if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				newStatus = Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING;
			}
			break;
		}
		if (newStatus.HasValue)
		{
			PresenceMgr.Get().SetStatus(newStatus.Value);
		}
	}

	protected virtual void TransitionToFormatType(FormatType formatType, bool inRankedPlayMode, float transitionSpeed = 2f)
	{
		Options.SetFormatType(formatType);
		Options.SetInRankedPlayMode(inRankedPlayMode);
		UpdateHeroActorOrientation();
	}

	protected virtual void PlayGame()
	{
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.FRIENDLY:
		case SceneMgr.Mode.TAVERN_BRAWL:
			if (TavernBrawlManager.Get().SelectHeroBeforeMission())
			{
				if (m_selectedHeroButton == null)
				{
					Debug.LogError("Trying to play Tavern Brawl game with no m_selectedHeroButton!");
					return;
				}
				int num2 = GameUtils.TranslateCardIdToDbId(m_selectedHeroButton.GetEntityDef().GetCardId());
				if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
				{
					FriendChallengeMgr.Get().SelectHero(num2);
					FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_TAVERN_BRAWL_OPPONENT_WAITING_READY", OnFriendChallengeWaitingForOpponentDialogResponse);
				}
				else
				{
					TavernBrawlManager.Get().StartGameWithHero(num2);
				}
			}
			break;
		case SceneMgr.Mode.FIRESIDE_GATHERING:
		{
			bool flag = FiresideGatheringManager.Get().InBrawlMode();
			bool flag2 = TavernBrawlManager.Get().SelectHeroBeforeMission();
			bool flag3 = false;
			if (TavernBrawlManager.Get().CurrentMission() != null)
			{
				flag3 = GameUtils.IsAIMission(TavernBrawlManager.Get().CurrentMission().missionId);
			}
			if (flag && flag3)
			{
				if (TavernBrawlManager.Get().SelectHeroBeforeMission())
				{
					int heroCardDbId = GameUtils.TranslateCardIdToDbId(m_selectedHeroButton.GetEntityDef().GetCardId());
					TavernBrawlManager.Get().StartGameWithHero(heroCardDbId);
				}
				break;
			}
			if (flag && flag2)
			{
				int num = GameUtils.TranslateCardIdToDbId(m_selectedHeroButton.GetEntityDef().GetCardId());
				FriendChallengeMgr.Get().SelectHeroBeforeSendingChallenge(num);
			}
			if (!flag3 && ((flag && flag2) || !flag))
			{
				FiresideGatheringDisplay.Get().ShowOpponentPickerTray(delegate
				{
					SetPlayButtonEnabled(enable: true);
				});
				SetPlayButtonEnabled(enable: false);
			}
			break;
		}
		}
		SoundManager.Get().Stop(m_lastPickLine);
	}

	protected virtual void ShowHero()
	{
		m_heroActor.Show();
		string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(m_heroActor.GetEntityDef().GetCardId());
		if (ShouldShowHeroPower() && !string.IsNullOrEmpty(heroPowerCardIdFromHero))
		{
			TAG_PREMIUM premium = m_heroActor.GetPremium();
			ShowHeroPower(premium);
		}
		else
		{
			m_heroPowerShadowQuad.SetActive(value: false);
			if (m_heroPowerActor != null)
			{
				m_heroPowerActor.Hide();
			}
			if (m_goldenHeroPower != null)
			{
				m_goldenHeroPowerActor.Hide();
			}
		}
		if (m_selectedHeroName == null)
		{
			m_heroName.Text = "";
		}
	}

	protected virtual void SelectHero(HeroPickerButton button, bool showTrayForPhone = true)
	{
		if (!(button == m_selectedHeroButton) || (bool)UniversalInputManager.UsePhoneUI)
		{
			DeselectLastSelectedHero();
			if ((bool)HIGHLIGHT_SELECTED_DECK)
			{
				button.SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
			else
			{
				button.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			}
			m_selectedHeroButton = button;
			UpdateHeroInfo(button);
			button.SetSelected(isSelected: true);
			ShowPreconHero(show: true);
			RemoveHeroLockedTooltip();
			if ((bool)UniversalInputManager.UsePhoneUI && showTrayForPhone)
			{
				m_slidingTray.ToggleTraySlider(show: true);
			}
			bool flag = IsHeroPlayable(button);
			if (flag && !NotificationManager.Get().IsQuotePlaying && button.HasCardDef)
			{
				SoundManager.Get().LoadAndPlay(button.HeroPickerSelectedPrefab, button.gameObject, 1f, OnLastPickLineLoaded);
			}
			SetPlayButtonEnabled(flag);
		}
	}

	protected virtual void UpdateHeroInfo(HeroPickerButton button)
	{
	}

	protected virtual void BackOutToHub()
	{
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			if (FriendChallengeMgr.Get() != null)
			{
				FriendChallengeMgr.Get().RemoveChangedListener(OnFriendChallengeChanged);
			}
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		}
	}

	protected virtual void BackOutToFiresideGathering()
	{
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FIRESIDE_GATHERING))
		{
			if (DeckPickerTrayDisplay.Get() != null)
			{
				FriendChallengeMgr.Get().RemoveChangedListener(OnFriendChallengeChanged);
			}
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.FIRESIDE_GATHERING);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_slidingTray.ToggleTraySlider(show: false);
			}
		}
	}

	protected void UpdateValidHeroClasses()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (Options.GetFormatType() == FormatType.FT_CLASSIC && mode != SceneMgr.Mode.ADVENTURE)
		{
			m_validClasses = new List<TAG_CLASS>(GameUtils.CLASSIC_ORDERED_HERO_CLASSES);
		}
		else
		{
			m_validClasses = new List<TAG_CLASS>(GameUtils.ORDERED_HERO_CLASSES);
		}
		if (!IsChoosingHero())
		{
			m_validClasses.Add(TAG_CLASS.WHIZBANG);
		}
		ScenarioDbId? scenarioDbId = null;
		if (mode == SceneMgr.Mode.ADVENTURE)
		{
			scenarioDbId = AdventureConfig.Get().GetMission();
		}
		if (mode == SceneMgr.Mode.TAVERN_BRAWL || (mode == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().InBrawlMode()) || FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			scenarioDbId = (ScenarioDbId)TavernBrawlManager.Get().CurrentMission().missionId;
		}
		if (scenarioDbId.HasValue && scenarioDbId != ScenarioDbId.INVALID)
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)scenarioDbId.Value);
			for (int i = 0; i < record.ClassExclusions.Count; i++)
			{
				m_validClasses.Remove((TAG_CLASS)record.ClassExclusions[i].ClassId);
			}
		}
	}

	protected virtual int ValidateHeroCount()
	{
		UpdateValidHeroClasses();
		return m_validClasses.Count;
	}

	protected virtual bool ShouldShowHeroPower()
	{
		return false;
	}

	private bool DeckPickerInRankedPlayMode()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.TOURNAMENT)
		{
			return Options.GetInRankedPlayMode();
		}
		return false;
	}

	private Transform GetActiveHeroBone()
	{
		bool flag = DeckPickerInRankedPlayMode();
		if (m_heroRaised)
		{
			if (!flag)
			{
				return m_Hero_Bone;
			}
			return m_Ranked_Hero_Bone;
		}
		if (!flag)
		{
			return m_Hero_BoneDown;
		}
		return m_Ranked_Hero_BoneDown;
	}

	private Transform GetActiveHeroNameBone()
	{
		if (!DeckPickerInRankedPlayMode())
		{
			return m_HeroName_Bone;
		}
		return m_Ranked_HeroName_Bone;
	}

	private void UpdateHeroActorOrientation()
	{
		if (m_heroActor != null)
		{
			iTween.Stop(m_heroActor.gameObject);
			Transform activeHeroBone = GetActiveHeroBone();
			if (activeHeroBone != null)
			{
				m_heroActor.transform.localScale = activeHeroBone.localScale;
				m_heroActor.transform.localPosition = activeHeroBone.localPosition;
			}
		}
		if (m_heroName != null)
		{
			Transform activeHeroNameBone = GetActiveHeroNameBone();
			if (activeHeroNameBone != null)
			{
				m_heroName.transform.localScale = activeHeroNameBone.localScale;
				m_heroName.transform.localPosition = activeHeroNameBone.localPosition;
			}
		}
	}

	protected virtual void SetHeroRaised(bool raised)
	{
		m_heroRaised = raised;
		Transform activeHeroBone = GetActiveHeroBone();
		if (activeHeroBone == null || m_HeroPower_Bone == null || m_HeroPower_BoneDown == null)
		{
			Debug.LogWarning("SetHeroRaised tried using transforms that were undefined!");
			return;
		}
		Vector3 localPosition = activeHeroBone.localPosition;
		Vector3 position = (raised ? m_HeroPower_Bone.localPosition : m_HeroPower_BoneDown.localPosition);
		MoveToRaisedPosition(localPosition, m_heroActor, raised);
		if (ShouldShowHeroPower())
		{
			m_heroPowerShadowQuad.SetActive(raised);
			MoveToRaisedPosition(position, m_heroPowerActor, raised, m_heroPower);
			MoveToRaisedPosition(position, m_goldenHeroPowerActor, raised, m_goldenHeroPower);
		}
	}

	private void MoveToRaisedPosition(Vector3 position, Actor actor, bool raised, PegUIElement pegUiElement = null)
	{
		if (actor == null)
		{
			return;
		}
		Hashtable args = iTween.Hash("position", position, "time", 0.25f, "easeType", iTween.EaseType.easeOutExpo, "islocal", true);
		iTween.MoveTo(actor.gameObject, args);
		if (pegUiElement != null)
		{
			Collider component = pegUiElement.GetComponent<Collider>();
			if (component != null)
			{
				component.enabled = raised;
			}
			else
			{
				Debug.LogWarning("Could not locate Collider for " + pegUiElement.name + " when trying to SetHeroRaised");
			}
		}
	}

	protected virtual void SetPlayButtonEnabled(bool enable)
	{
		if (enable && SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY && !FriendChallengeMgr.Get().HasChallenge())
		{
			return;
		}
		m_playButtonEnabled = enable;
		if (m_playButton != null && m_playButton.IsEnabled() != enable)
		{
			if (enable)
			{
				m_playButton.Enable();
			}
			else
			{
				m_playButton.Disable();
			}
		}
	}

	protected virtual void SetCollectionButtonEnabled(bool enable)
	{
		if (m_collectionButton != null)
		{
			m_collectionButton.SetEnabled(enable);
			m_collectionButton.Flip(enable);
		}
	}

	protected virtual void SetHeroButtonsEnabled(bool enable)
	{
		foreach (HeroPickerButton heroButton in m_heroButtons)
		{
			if (!heroButton.IsLocked() || !enable)
			{
				heroButton.SetEnabled(enable);
			}
		}
	}

	protected virtual void SetHeaderForTavernBrawl()
	{
		string key = "GLUE_CHOOSE";
		if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			key = ((TavernBrawlManager.Get().CurrentBrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING) ? "GLUE_BRAWL_PATRON" : "GLUE_BRAWL_FRIEND");
		}
		else if (TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			key = "GLUE_BRAWL";
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING)
			{
				key = "GLUE_CHOOSE_OPPONENT";
				TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING);
				TavernBrawlMission mission2 = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
				bool num = FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL && mission != null && mission.GameType == GameType.GT_FSG_BRAWL_1P_VS_AI;
				bool flag = FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FRIENDLY_CHALLENGE_BRAWL && mission2 != null && mission2.GameType == GameType.GT_TB_1P_VS_AI;
				if (num || flag)
				{
					key = "GLUE_CHOOSE";
				}
			}
		}
		SetPlayButtonText(GameStrings.Get(key));
	}

	protected virtual bool IsHeroPlayable(HeroPickerButton button)
	{
		return !button.IsLocked();
	}

	public virtual int GetSelectedHeroID()
	{
		if (m_selectedHeroButton != null)
		{
			GuestHeroDbfRecord guestHero = m_selectedHeroButton.GetGuestHero();
			if (guestHero != null)
			{
				return guestHero.CardId;
			}
		}
		return 0;
	}

	public virtual long GetSelectedDeckID()
	{
		if (!(m_selectedHeroButton == null))
		{
			return m_selectedHeroButton.GetPreconDeckID();
		}
		return 0L;
	}

	protected abstract void GoBackUntilOnNavigateBackCalled();

	protected virtual void OnBackButtonReleased(UIEvent e)
	{
		Navigation.GoBack();
	}

	protected virtual void OnPlayGameButtonReleased(UIEvent e)
	{
		if (!Network.IsLoggedIn() && SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
		{
			DialogManager.Get().ShowReconnectHelperDialog();
		}
		else if (!SetRotationManager.Get().CheckForSetRotationRollover() && (PlayerMigrationManager.Get() == null || !PlayerMigrationManager.Get().CheckForPlayerMigrationRequired()))
		{
			SetPlayButtonEnabled(enable: false);
			SetHeroButtonsEnabled(enable: false);
			PlayGame();
		}
	}

	protected virtual void OnHeroButtonReleased(UIEvent e)
	{
		HeroPickerButton button = (HeroPickerButton)e.GetElement();
		SelectHero(button);
		SoundManager.Get().LoadAndPlay("tournament_screen_select_hero.prefab:2b9bdf587ac07084b8f7d5c4bce33ecf");
	}

	protected virtual void OnHeroMouseOver(UIEvent e)
	{
		if (e != null)
		{
			((HeroPickerButton)e.GetElement()).SetHighlightState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
			SoundManager.Get().LoadAndPlay("collection_manager_hero_mouse_over.prefab:653cc8000b988cd468d2210a209adce6");
		}
	}

	protected virtual void OnHeroMouseOut(UIEvent e)
	{
		HeroPickerButton heroPickerButton = (HeroPickerButton)e.GetElement();
		if (!UniversalInputManager.UsePhoneUI || !heroPickerButton.IsSelected())
		{
			heroPickerButton.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		}
	}

	protected virtual void OnHeroPowerMouseOver(UIEvent e)
	{
		m_isMouseOverHeroPower = true;
		if (m_heroActor.GetPremium() == TAG_PREMIUM.GOLDEN)
		{
			if (m_goldenHeroPowerBigCard == null)
			{
				AssetLoader.Get().InstantiatePrefab(ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.HISTORY_HERO_POWER, TAG_PREMIUM.GOLDEN), OnGoldenHeroPowerLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
			}
			else
			{
				ShowHeroPowerBigCard(isGolden: true);
			}
		}
		else if (m_heroPowerBigCard == null)
		{
			AssetLoader.Get().InstantiatePrefab("History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53", OnHeroPowerLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		}
		else
		{
			ShowHeroPowerBigCard();
		}
	}

	protected virtual void OnHeroPowerMouseOut(UIEvent e)
	{
		m_isMouseOverHeroPower = false;
		if (m_heroPowerBigCard != null)
		{
			iTween.Stop(m_heroPowerBigCard.gameObject);
			m_heroPowerBigCard.Hide();
		}
		if (m_goldenHeroPowerBigCard != null)
		{
			iTween.Stop(m_goldenHeroPowerBigCard.gameObject);
			m_goldenHeroPowerBigCard.Hide();
		}
	}

	protected void LoadHeroPowerDef(string heroPowerCardId)
	{
		DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(heroPowerCardId);
		m_heroPowerDefs.SetOrReplaceDisposable(heroPowerCardId, fullDef);
	}

	protected void OnPlayButtonWidgetReady(VisualController visualController)
	{
		if (visualController == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButton could not be found! You will not be able to click 'Play'!");
			return;
		}
		m_playButton = visualController.GetComponent<PlayButton>();
		if (!(m_playButton == null))
		{
			if (FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL)
			{
				visualController.Owner.TriggerEvent("LANTERN");
			}
			m_playButton.AddEventListener(UIEventType.RELEASE, OnPlayGameButtonReleased);
			SetPlayButtonEnabled(m_playButtonEnabled);
		}
	}

	protected void OnHeroPickerButtonWidgetReady(WidgetInstance widget)
	{
		HeroPickerButton componentInChildren = widget.GetComponentInChildren<HeroPickerButton>();
		m_heroButtons.Add(componentInChildren);
		SetUpHeroPickerButton(componentInChildren, m_heroButtons.Count - 1);
		componentInChildren.Lock();
		componentInChildren.Activate(enable: false);
		componentInChildren.AddEventListener(UIEventType.RELEASE, OnHeroButtonReleased);
		componentInChildren.AddEventListener(UIEventType.ROLLOVER, OnHeroMouseOver);
		componentInChildren.AddEventListener(UIEventType.ROLLOUT, OnHeroMouseOut);
		Vector3 originalLocalPosition = ((componentInChildren.m_raiseAndLowerRoot != null) ? componentInChildren.m_raiseAndLowerRoot.transform.localPosition : base.transform.localPosition);
		componentInChildren.SetOriginalLocalPosition(originalLocalPosition);
	}

	protected void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"AbsDeckPickerTrayDisplay.OnHeroPowerActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		m_heroPowerActor = go.GetComponent<Actor>();
		if (m_heroPowerActor == null)
		{
			Debug.LogWarning($"AbsDeckPickerTrayDisplay.OnHeroPowerActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		m_heroPower = go.AddComponent<PegUIElement>();
		go.AddComponent<BoxCollider>();
		GameUtils.SetParent(go, m_heroPowerContainer);
		go.transform.localScale = m_HeroPower_Bone.localScale;
		go.transform.localPosition = m_HeroPower_Bone.localPosition;
		m_heroPowerActor.SetUnlit();
		m_heroPower.AddEventListener(UIEventType.ROLLOVER, OnHeroPowerMouseOver);
		m_heroPower.AddEventListener(UIEventType.ROLLOUT, OnHeroPowerMouseOut);
		m_heroPowerActor.Hide();
		m_heroPower.GetComponent<Collider>().enabled = false;
		m_heroName.Text = "";
		StartCoroutine(UpdateHeroSkinHeroPower());
	}

	protected void OnGoldenHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"AbsDeckPickerTrayDisplay.OnHeroPowerActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		m_goldenHeroPowerActor = go.GetComponent<Actor>();
		if (m_goldenHeroPowerActor == null)
		{
			Debug.LogWarning($"AbsDeckPickerTrayDisplay.OnHeroPowerActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		m_goldenHeroPower = go.AddComponent<PegUIElement>();
		go.AddComponent<BoxCollider>();
		GameUtils.SetParent(go, m_heroPowerContainer);
		go.transform.localScale = m_HeroPower_Bone.localScale;
		go.transform.localPosition = m_HeroPower_Bone.localPosition;
		m_goldenHeroPowerActor.SetUnlit();
		m_goldenHeroPowerActor.SetPremium(TAG_PREMIUM.GOLDEN);
		m_goldenHeroPower.AddEventListener(UIEventType.ROLLOVER, OnHeroPowerMouseOver);
		m_goldenHeroPower.AddEventListener(UIEventType.ROLLOUT, OnHeroPowerMouseOut);
		m_goldenHeroPowerActor.Hide();
		m_goldenHeroPower.GetComponent<Collider>().enabled = false;
		m_heroName.Text = "";
	}

	protected void OnHeroPowerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"AbsDeckPickerTrayDisplay.LoadHeroPowerCallback() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning($"AbsDeckPickerTrayDisplay.LoadHeroPowerCallback() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		component.transform.parent = m_heroPower.transform;
		component.TurnOffCollider();
		SceneUtils.SetLayer(component.gameObject, m_heroPower.gameObject.layer);
		m_heroPowerBigCard = component;
		if (m_isMouseOverHeroPower)
		{
			ShowHeroPowerBigCard();
		}
	}

	protected void OnGoldenHeroPowerLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"AbsDeckPickerTrayDisplay.LoadHeroPowerCallback() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		Actor component = go.GetComponent<Actor>();
		if (component == null)
		{
			Debug.LogWarning($"AbsDeckPickerTrayDisplay.LoadHeroPowerCallback() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		component.transform.parent = m_heroPower.transform;
		component.TurnOffCollider();
		SceneUtils.SetLayer(component.gameObject, m_heroPower.gameObject.layer);
		m_goldenHeroPowerBigCard = component;
		if (m_isMouseOverHeroPower)
		{
			ShowHeroPowerBigCard(isGolden: true);
		}
	}

	protected void OnPopupShown()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_slidingTray.ToggleTraySlider(show: false, null, animate: false);
		}
	}

	private void OnLastPickLineLoaded(AudioSource source, object callbackData)
	{
		SoundManager.Get().Stop(m_lastPickLine);
		m_lastPickLine = source;
	}

	protected virtual void OnFriendChallengeWaitingForOpponentDialogResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL && !FriendChallengeMgr.Get().AmIInGameState())
		{
			ResetCurrentMode();
			FriendChallengeMgr.Get().DeselectDeckOrHero();
			FriendlyChallengeHelper.Get().StopWaitingForFriendChallenge();
		}
	}

	protected virtual void OnFriendChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		switch (challengeEvent)
		{
		case FriendChallengeEvent.SELECTED_DECK_OR_HERO:
			if (!SceneMgr.Get().IsInTavernBrawlMode() && player != BnetPresenceMgr.Get().GetMyPlayer() && FriendChallengeMgr.Get().DidISelectDeckOrHero())
			{
				FriendlyChallengeHelper.Get().HideFriendChallengeWaitingForOpponentDialog();
			}
			break;
		case FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE:
		case FriendChallengeEvent.OPPONENT_REMOVED_FROM_FRIENDS:
		case FriendChallengeEvent.QUEUE_CANCELED:
			FriendlyChallengeHelper.Get().StopWaitingForFriendChallenge();
			GoBackUntilOnNavigateBackCalled();
			break;
		case FriendChallengeEvent.DESELECTED_DECK_OR_HERO:
			if (player != BnetPresenceMgr.Get().GetMyPlayer())
			{
				if (FriendChallengeMgr.Get().DidISelectDeckOrHero())
				{
					FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_OPPONENT_WAITING_DECK", OnFriendChallengeWaitingForOpponentDialogResponse);
					break;
				}
				ResetCurrentMode();
				SetBackButtonEnabled(enable: true);
			}
			break;
		}
	}

	protected IEnumerator LoadHeroButtons(int? m_cheatOverrideHeroPickerButtonCount = null)
	{
		if (m_cheatOverrideHeroPickerButtonCount.HasValue)
		{
			m_HeroPickerButtonCount = m_cheatOverrideHeroPickerButtonCount.Value;
		}
		else
		{
			m_HeroPickerButtonCount = ValidateHeroCount();
		}
		SetupHeroLayout();
		foreach (HeroPickerButton heroButton in m_heroButtons)
		{
			Object.Destroy(heroButton.gameObject);
		}
		m_heroButtons.Clear();
		HeroPickerDataModel heroPickerDataModel = GetHeroPickerDataModel();
		for (int i = 0; i < m_HeroPickerButtonCount; i++)
		{
			WidgetInstance heroPickerButtonWidget = WidgetInstance.Create(m_heroButtonWidgetPrefab);
			if (heroPickerDataModel != null)
			{
				heroPickerButtonWidget.BindDataModel(heroPickerDataModel);
			}
			heroPickerButtonWidget.RegisterReadyListener(delegate
			{
				OnHeroPickerButtonWidgetReady(heroPickerButtonWidget);
			});
		}
		yield return StartCoroutine(InitDeckDependentElements());
		StartCoroutine(InitHeroPickerElements());
	}

	protected void SetupHeroLayout()
	{
		if (m_HeroPickerButtonCount <= 0 || m_HeroPickerButtonCount > m_heroPickerButtonBonesByHeroCount.Count || m_heroPickerButtonBonesByHeroCount[m_HeroPickerButtonCount] == null)
		{
			Log.Adventures.PrintWarning("Deck/Class Picker Instantiated with an unsupported amount of heroes: " + m_HeroPickerButtonCount);
			return;
		}
		GameObject gameObject = m_heroPickerButtonBonesByHeroCount[m_HeroPickerButtonCount];
		m_heroBones = new List<Transform>();
		m_heroBones.AddRange(gameObject.GetComponentsInChildren<Transform>());
		m_heroBones.RemoveAt(0);
		if (m_heroBones.Count != m_HeroPickerButtonCount)
		{
			Log.Adventures.PrintWarning("Layout for {0} heroes yielded an incorrect amount of transforms. This will result in errors when displaying heroes!", m_HeroPickerButtonCount);
		}
	}

	protected void LoadHero()
	{
		AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", OnHeroActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	protected void LoadHeroPower()
	{
		AssetLoader.Get().InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", OnHeroPowerActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	protected void LoadGoldenHeroPower()
	{
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetNameWithPremiumType(ActorNames.ACTOR_ASSET.PLAY_HERO_POWER, TAG_PREMIUM.GOLDEN), OnGoldenHeroPowerActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	protected IEnumerator UpdateHeroSkinHeroPower()
	{
		while (m_heroActor == null || !m_heroActor.HasCardDef)
		{
			yield return null;
		}
		HeroSkinHeroPower componentInChildren = m_heroPowerActor.gameObject.GetComponentInChildren<HeroSkinHeroPower>();
		if (!(componentInChildren == null))
		{
			componentInChildren.m_Actor.AlwaysRenderPremiumPortrait = !GameUtils.IsVanillaHero(m_heroActor.GetEntityDef().GetCardId());
			componentInChildren.m_Actor.UpdateMaterials();
			componentInChildren.m_Actor.UpdateTextures();
		}
	}

	protected void UpdateHeroPowerInfo(DefLoader.DisposableFullDef def, TAG_PREMIUM premium)
	{
		SetHeroPowerActorColliderEnabled();
		m_heroPowerActor.SetFullDef(def);
		m_selectedHeroPowerFullDef?.Dispose();
		m_selectedHeroPowerFullDef = def.Share();
		m_heroPowerActor.SetUnlit();
		def.CardDef.m_AlwaysRenderPremiumPortrait = false;
		m_heroPowerActor.UpdateAllComponents();
		m_goldenHeroPowerActor.SetFullDef(def);
		m_goldenHeroPowerActor.UpdateAllComponents();
		m_goldenHeroPowerActor.SetUnlit();
		ShowHeroPower(premium);
		if (premium != TAG_PREMIUM.GOLDEN && m_heroActor.GetEntityDef().GetCardSet() == TAG_CARD_SET.HERO_SKINS)
		{
			StartCoroutine(UpdateHeroSkinHeroPower());
		}
	}

	protected void UpdateCustomHeroPowerBigCard(GameObject heroPowerBigCard)
	{
		if (!m_heroActor.HasCardDef)
		{
			Debug.LogWarning("AbsDeckPickerTrayDisplay.UpdateCustomHeroPowerBigCard heroCardDef = null!");
			return;
		}
		Actor componentInChildren = heroPowerBigCard.GetComponentInChildren<Actor>();
		TAG_CARD_SET cardSet = m_heroActor.GetEntityDef().GetCardSet();
		componentInChildren.AlwaysRenderPremiumPortrait = cardSet == TAG_CARD_SET.HERO_SKINS;
		componentInChildren.UpdateMaterials();
	}

	protected void ShowHeroPowerBigCard(bool isGolden = false)
	{
		Actor actor = (isGolden ? m_goldenHeroPowerBigCard : m_heroPowerBigCard);
		Actor actor2 = ((!isGolden) ? m_goldenHeroPowerBigCard : m_heroPowerBigCard);
		if (!(m_selectedHeroPowerFullDef?.CardDef == null))
		{
			actor.SetCardDef(m_selectedHeroPowerFullDef.DisposableCardDef);
			actor.SetEntityDef(m_selectedHeroPowerFullDef.EntityDef);
			actor.UpdateAllComponents();
			actor.Show();
			if (actor2 != null)
			{
				actor2.Hide();
			}
			UpdateCustomHeroPowerBigCard(actor.gameObject);
			float num = 1f;
			float num2 = 1.5f;
			Vector3 vector = (UniversalInputManager.Get().IsTouchMode() ? new Vector3(0.019f, 0.54f, 3f) : new Vector3(0.019f, 0.54f, -1.12f));
			GameObject gameObject = actor.gameObject;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				gameObject.transform.localPosition = new Vector3(-11.4f, 0.6f, -0.14f);
				gameObject.transform.localScale = Vector3.one * 3.2f;
				AnimationUtil.GrowThenDrift(gameObject, m_HeroPower_Bone.transform.position, 2f);
				return;
			}
			Vector3 vector2 = (PlatformSettings.IsTablet ? new Vector3(0f, 0.1f, 0.1f) : new Vector3(0.1f, 0.1f, 0.1f));
			gameObject.transform.localPosition = vector;
			gameObject.transform.localScale = Vector3.one * num;
			iTween.ScaleTo(gameObject, Vector3.one * num2, 0.15f);
			iTween.MoveTo(gameObject, iTween.Hash("position", vector + vector2, "isLocal", true, "time", 10));
		}
	}

	protected void ShowHeroPower(TAG_PREMIUM premium)
	{
		if (m_heroPowerShadowQuad != null)
		{
			m_heroPowerShadowQuad.SetActive(value: true);
		}
		if (premium == TAG_PREMIUM.GOLDEN)
		{
			m_heroPowerActor.Hide();
			m_goldenHeroPowerActor.Show();
			m_goldenHeroPower.GetComponent<Collider>().enabled = true;
		}
		else
		{
			m_goldenHeroPowerActor.Hide();
			m_heroPowerActor.Show();
			m_heroPower.GetComponent<Collider>().enabled = true;
		}
	}

	protected void ShowPreconHero(bool show)
	{
		if (show && SceneMgr.Get().GetMode() == SceneMgr.Mode.ADVENTURE && AdventureConfig.Get().CurrentSubScene == AdventureData.Adventuresubscene.PRACTICE && PracticePickerTrayDisplay.Get() != null && PracticePickerTrayDisplay.Get().IsShown())
		{
			return;
		}
		if (show)
		{
			ShowHero();
			return;
		}
		if ((bool)m_heroActor)
		{
			m_heroActor.Hide();
		}
		if ((bool)m_heroPowerActor)
		{
			m_heroPowerActor.Hide();
		}
		if ((bool)m_goldenHeroPowerActor)
		{
			m_goldenHeroPowerActor.Hide();
		}
		if ((bool)m_heroPower)
		{
			m_heroPower.GetComponent<Collider>().enabled = false;
		}
		if ((bool)m_goldenHeroPower)
		{
			m_goldenHeroPower.GetComponent<Collider>().enabled = false;
		}
		m_heroName.Text = "";
	}

	protected void HideHeroPowerActor()
	{
		m_heroPowerShadowQuad.SetActive(value: false);
		if (m_heroPowerActor != null)
		{
			m_heroPowerActor.Hide();
		}
		if (m_goldenHeroPower != null)
		{
			m_goldenHeroPowerActor.Hide();
		}
	}

	protected void SetUpHeroPickerButton(HeroPickerButton button, int heroCount)
	{
		GameObject gameObject = button.gameObject;
		Transform parent = gameObject.transform.parent;
		gameObject.name = $"{gameObject.name}_{heroCount}";
		parent.transform.SetParent(m_heroBones[heroCount], worldPositionStays: false);
		parent.transform.localScale = Vector3.one;
		parent.transform.localPosition = Vector3.zero;
		parent.SetParent(m_basicDeckPageContainer.transform, worldPositionStays: true);
	}

	protected void AddHeroLockedTooltip(string name, string description)
	{
		RemoveHeroLockedTooltip();
		GameObject gameObject = Object.Instantiate(m_tooltipPrefab);
		SceneUtils.SetLayer(gameObject, UniversalInputManager.UsePhoneUI ? GameLayer.IgnoreFullScreenEffects : GameLayer.Default);
		m_heroLockedTooltip = gameObject.GetComponent<TooltipPanel>();
		m_heroLockedTooltip.Reset();
		m_heroLockedTooltip.Initialize(name, description);
		GameUtils.SetParent(m_heroLockedTooltip, m_tooltipBone);
	}

	protected void RemoveHeroLockedTooltip()
	{
		if (m_heroLockedTooltip != null)
		{
			Object.DestroyImmediate(m_heroLockedTooltip.gameObject);
		}
	}

	protected void DeselectLastSelectedHero()
	{
		if (!(m_selectedHeroButton == null))
		{
			m_selectedHeroButton.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			m_selectedHeroButton.SetSelected(isSelected: false);
		}
	}

	protected void FireDeckTrayLoadedEvent()
	{
		DeckTrayLoaded[] array = m_DeckTrayLoadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	protected void FireFormatTypePickerClosedEvent()
	{
		FormatTypePickerClosed[] array = m_FormatTypePickerClosedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	protected bool IsChoosingHero()
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode != SceneMgr.Mode.COLLECTIONMANAGER && mode != SceneMgr.Mode.TAVERN_BRAWL && !IsChoosingHeroForTavernBrawlChallenge() && !IsInFiresideGatheringAndInBrawlMode() && !IsChoosingHeroForDungeonCrawlAdventure())
		{
			return IsChoosingHeroForPvPDungeonRunDeck();
		}
		return true;
	}

	protected bool IsChoosingHeroForTavernBrawlChallenge()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY)
		{
			return FriendChallengeMgr.Get().IsChallengeTavernBrawl();
		}
		return false;
	}

	protected bool IsInFiresideGatheringAndInBrawlMode()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			return FiresideGatheringManager.Get().InBrawlMode();
		}
		return false;
	}

	protected bool IsChoosingHeroForDungeonCrawlAdventure()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.ADVENTURE)
		{
			return GameUtils.DoesAdventureModeUseDungeonCrawlFormat(AdventureConfig.Get().GetSelectedMode());
		}
		return false;
	}

	protected bool IsChoosingHeroForPvPDungeonRunDeck()
	{
		return SceneMgr.Get().GetMode() == SceneMgr.Mode.PVP_DUNGEON_RUN;
	}

	protected bool OnPlayButtonPressed_SaveHeroAndAdvanceToDungeonRunIfNecessary()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDataDbfRecord selectedAdventureDataRecord = adventureConfig.GetSelectedAdventureDataRecord();
		if (GameUtils.DoesAdventureModeUseDungeonCrawlFormat(AdventureConfig.Get().GetSelectedMode()) && selectedAdventureDataRecord.DungeonCrawlPickHeroFirst)
		{
			adventureConfig.SelectedHeroClass = m_selectedHeroButton.m_heroClass;
			adventureConfig.ChangeSubScene(AdventureData.Adventuresubscene.DUNGEON_CRAWL);
			return true;
		}
		return false;
	}

	protected void SetBackButtonEnabled(bool enable)
	{
		if (DemoMgr.Get().IsExpoDemo())
		{
			if (enable)
			{
				return;
			}
			enable = false;
		}
		if (m_backButton != null && m_backButton.IsEnabled() != enable)
		{
			m_backButton.SetEnabled(enable);
			m_backButton.Flip(enable);
		}
	}

	protected void SetHeroPowerActorColliderEnabled(bool enable = true)
	{
		if (m_heroPowerActor != null)
		{
			m_heroPowerActor.GetComponent<Collider>().enabled = enable;
		}
		if (m_goldenHeroPower != null)
		{
			m_goldenHeroPowerActor.GetComponent<Collider>().enabled = enable;
		}
	}

	protected void SetUpHeroCrowns()
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)AdventureConfig.Get().GetSelectedAdventure(), (int)AdventureConfig.Get().GetSelectedMode());
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		if (!GameSaveDataManager.Get().ValidateIfKeyCanBeAccessed(gameSaveDataServerKey, adventureDataRecord.Name))
		{
			return;
		}
		if (adventureDataRecord != null && adventureDataRecord.DungeonCrawlDisplayHeroWinsPerChapter)
		{
			WingDbfRecord wingRecordFromMissionId = GameUtils.GetWingRecordFromMissionId((int)AdventureConfig.Get().GetMission());
			GameSaveDataManager.AdventureDungeonCrawlWingProgressSubkeys progressSubkeys;
			List<long> values;
			if (wingRecordFromMissionId == null)
			{
				Log.Adventures.PrintError("SetUpHeroCrowns() - No WingRecord found for mission {0}, so cannot set up hero crowns.", AdventureConfig.Get().GetMission());
			}
			else if (!GameSaveDataManager.GetProgressSubkeysForDungeonCrawlWing(wingRecordFromMissionId, out progressSubkeys))
			{
				Log.Adventures.PrintError("GetProgressSubkeysForDungeonCrawlWing could not find progress subkeys for Wing {0}, so we don't know which Heroes to show crowns over.", wingRecordFromMissionId.ID);
			}
			else if (GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, progressSubkeys.heroCardWins, out values) && values != null)
			{
				ActivateCrownsForHeroCardDbIds(values);
			}
			return;
		}
		long value = 0L;
		List<TAG_CLASS> list = new List<TAG_CLASS>();
		foreach (TAG_CLASS item in GameSaveDataManager.GetClassesFromDungeonCrawlProgressMap())
		{
			if (GameSaveDataManager.GetProgressSubkeyForDungeonCrawlClass(item, out var progressSubkeys2) && GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, progressSubkeys2.runWins, out value) && value > 0)
			{
				list.Add(item);
			}
		}
		ActivateCrownsForClasses(list);
	}

	protected List<Transform> ActivateCrownsForClasses(List<TAG_CLASS> classes)
	{
		List<Transform> result = new List<Transform>();
		foreach (HeroPickerButton heroButton in m_heroButtons)
		{
			if (classes.Contains(heroButton.m_heroClass))
			{
				heroButton.m_crown.SetActive(value: true);
			}
		}
		return result;
	}

	protected void ActivateCrownsForHeroCardDbIds(List<long> cardDbIds)
	{
		foreach (HeroPickerButton heroButton in m_heroButtons)
		{
			EntityDef entityDef = heroButton.GetEntityDef();
			if (entityDef != null)
			{
				int num = GameUtils.TranslateCardIdToDbId(entityDef.GetCardId());
				if (cardDbIds.Contains(num))
				{
					heroButton.m_crown.SetActive(value: true);
				}
			}
		}
	}

	public void Unload()
	{
		DeckPickerTray.Get().UnregisterHandlers();
		if (FriendChallengeMgr.Get() != null)
		{
			FriendChallengeMgr.Get().RemoveChangedListener(OnFriendChallengeChanged);
		}
	}

	public bool IsLoaded()
	{
		return m_Loaded;
	}

	public void AddDeckTrayLoadedListener(DeckTrayLoaded dlg)
	{
		m_DeckTrayLoadedListeners.Add(dlg);
	}

	public void RemoveDeckTrayLoadedListener(DeckTrayLoaded dlg)
	{
		m_DeckTrayLoadedListeners.Remove(dlg);
	}

	public void AddFormatTypePickerClosedListener(FormatTypePickerClosed dlg)
	{
		m_FormatTypePickerClosedListeners.Add(dlg);
	}

	public void RemoveFormatTypePickerClosedListener(FormatTypePickerClosed dlg)
	{
		m_FormatTypePickerClosedListeners.Remove(dlg);
	}

	public void SetPlayButtonText(string text)
	{
		if (m_playButton != null)
		{
			m_playButton.SetText(text);
		}
	}

	public void SetPlayButtonTextAlpha(float alpha)
	{
		if (m_playButton != null)
		{
			m_playButton.m_newPlayButtonText.TextAlpha = alpha;
		}
	}

	public void AddPlayButtonListener(UIEventType type, UIEvent.Handler handler)
	{
		if (m_playButton != null)
		{
			m_playButton.AddEventListener(type, handler);
		}
	}

	public void RemovePlayButtonListener(UIEventType type, UIEvent.Handler handler)
	{
		if (m_playButton != null)
		{
			m_playButton.RemoveEventListener(type, handler);
		}
	}

	public void SetHeaderText(string text)
	{
		if (m_modeName != null)
		{
			m_modeName.Text = text;
		}
	}

	public HeroPickerDataModel GetHeroPickerDataModel()
	{
		VisualController component = GetComponent<VisualController>();
		if (component == null)
		{
			return null;
		}
		Widget owner = component.Owner;
		if (!owner.GetDataModel(13, out var model))
		{
			model = new HeroPickerDataModel();
			owner.BindDataModel(model);
		}
		return model as HeroPickerDataModel;
	}

	public void CheatLoadHeroButtons(int buttonsToDisplay)
	{
		StartCoroutine(LoadHeroButtons(buttonsToDisplay));
	}
}
