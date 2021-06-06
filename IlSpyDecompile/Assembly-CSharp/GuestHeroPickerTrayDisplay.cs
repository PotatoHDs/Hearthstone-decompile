using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class GuestHeroPickerTrayDisplay : AbsDeckPickerTrayDisplay
{
	public delegate void GuestHeroSelectedCallback(TAG_CLASS classId, GuestHeroDbfRecord record);

	private struct GuestHeroRecordContainer
	{
		public GuestHeroDbfRecord GuestHeroRecord;

		public AdventureGuestHeroesDbfRecord AdventureGuestHeroRecord;

		public ScenarioGuestHeroesDbfRecord ScenarioGuestHeroRecord;
	}

	public UberText m_heroDescription;

	public UberText m_chooseHeroLabel;

	[CustomEditField(Sections = "Hero Divot Textures")]
	public Texture m_divotTextureDefault;

	[CustomEditField(Sections = "Hero Divot Textures")]
	public Texture m_divotTextureDalaran;

	[CustomEditField(Sections = "Hero Divot Textures")]
	public Texture m_divotTextureUldum;

	[CustomEditField(Sections = "Hero Divot Textures")]
	public Texture m_divotTexturePVPDR;

	private static GuestHeroPickerTrayDisplay s_instance;

	public override void Awake()
	{
		base.Awake();
		s_instance = this;
		HeroPickerDataModel heroPickerDataModel = GetHeroPickerDataModel();
		if (heroPickerDataModel != null)
		{
			heroPickerDataModel.HasGuestHeroes = true;
		}
		VisualController visualController = GetComponent<VisualController>();
		if (visualController != null)
		{
			visualController.Owner.RegisterReadyListener(delegate
			{
				OnHeroPickerWidgetReady(visualController.Owner);
			});
		}
		else
		{
			Debug.LogError("AbsDeckPickerTrayDisplay.Awake - could not find visual controller. Ensure that this component is on the same object as the visual controller.");
		}
	}

	private void Start()
	{
		Navigation.PushIfNotOnTop(OnNavigateBack);
	}

	private void Update()
	{
		if (!(AdventureScene.Get() == null) && AdventureScene.Get().IsDevMode && InputCollection.GetKeyDown(KeyCode.Z))
		{
			Cheat_LockAllButtons();
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (Get() == this)
		{
			s_instance = null;
		}
	}

	public static bool OnNavigateBack()
	{
		if (Get() != null)
		{
			return Get().OnNavigateBackImplementation();
		}
		Debug.LogError("GuestHeroPickerTrayDisplay: tried to navigate back but had null instance!");
		return false;
	}

	protected override IEnumerator InitModeWhenReady()
	{
		yield return StartCoroutine(base.InitModeWhenReady());
		if (SceneMgr.Get().IsInDuelsMode())
		{
			SetBackButtonEnabled(enable: false);
		}
		ShowFirstPage();
	}

	protected override void InitForMode(SceneMgr.Mode mode)
	{
		GetComponent<VisualController>();
		switch (mode)
		{
		case SceneMgr.Mode.ADVENTURE:
		case SceneMgr.Mode.PVP_DUNGEON_RUN:
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			AdventureDbId selectedAdventure = adventureConfig.GetSelectedAdventure();
			AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode);
			SetHeaderText(adventureDataRecord.Name);
			AdventureSubScene component = GuestHeroPickerDisplay.Get().GetComponent<AdventureSubScene>();
			if (component != null)
			{
				component.SetIsLoaded(loaded: true);
			}
			if (mode == SceneMgr.Mode.PVP_DUNGEON_RUN && GuestHeroPickerDisplay.Get() != null)
			{
				GuestHeroPickerDisplay.Get().ShowTray();
			}
			break;
		}
		case SceneMgr.Mode.FRIENDLY:
		case SceneMgr.Mode.TAVERN_BRAWL:
		case SceneMgr.Mode.FIRESIDE_GATHERING:
		{
			string key = ((TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? "GLOBAL_HEROIC_BRAWL" : "GLOBAL_TAVERN_BRAWL");
			TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(tavernBrawlMission.missionId);
			SetHeaderText(GameStrings.Get(key));
			if (record.ChooseHeroText != null)
			{
				SetChooseHeroText(record.ChooseHeroText);
			}
			if (GuestHeroPickerDisplay.Get() != null && mode != SceneMgr.Mode.FRIENDLY)
			{
				GuestHeroPickerDisplay.Get().ShowTray();
			}
			break;
		}
		}
		SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
		base.InitForMode(mode);
	}

	protected override void InitHeroPickerButtons()
	{
		base.InitHeroPickerButtons();
		List<GuestHeroRecordContainer> guestHeroes = GetGuestHeroes();
		if (guestHeroes == null)
		{
			Debug.LogError("InitHeroPickerButtons: Unable to get guest heroes to display.");
			return;
		}
		if (!GlobalDataContext.Get().GetDataModel(7, out var model))
		{
			model = new AdventureDataModel();
			GlobalDataContext.Get().BindDataModel(model);
		}
		AdventureDataModel obj = model as AdventureDataModel;
		if (obj == null)
		{
			Log.Adventures.PrintWarning("AdventureDataModel is null!");
		}
		Texture divotTexture = obj.SelectedAdventure switch
		{
			AdventureDbId.DALARAN => m_divotTextureDalaran, 
			AdventureDbId.ULDUM => m_divotTextureUldum, 
			_ => SceneMgr.Get().IsInDuelsMode() ? m_divotTexturePVPDR : m_divotTextureDefault, 
		};
		m_heroDefsLoading = guestHeroes.Count;
		for (int i = 0; i < guestHeroes.Count; i++)
		{
			if (i >= m_heroButtons.Count || m_heroButtons[i] == null)
			{
				Debug.LogWarning("InitHeroPickerButtons: not enough buttons for total guest heroes.");
				break;
			}
			GuestHeroPickerButton guestHeroPickerButton = m_heroButtons[i] as GuestHeroPickerButton;
			if (guestHeroPickerButton == null)
			{
				Debug.LogWarning("InitHeroPickerButtons: attempted to display null button.");
				m_heroDefsLoading--;
				continue;
			}
			GuestHeroDbfRecord guestHeroRecord = guestHeroes[i].GuestHeroRecord;
			if (guestHeroRecord == null)
			{
				guestHeroPickerButton.Lock();
				guestHeroPickerButton.Activate(enable: false);
			}
			else
			{
				guestHeroPickerButton.Unlock();
				guestHeroPickerButton.Activate(enable: true);
			}
			long preconDeckID = 0L;
			TAG_CLASS tAG_CLASS = ((guestHeroRecord != null) ? GameUtils.GetTagClassFromCardDbId(guestHeroRecord.CardId) : TAG_CLASS.INVALID);
			if (tAG_CLASS != 0)
			{
				CollectionManager.PreconDeck preconDeck = CollectionManager.Get().GetPreconDeck(tAG_CLASS);
				if (preconDeck != null)
				{
					preconDeckID = preconDeck.ID;
				}
			}
			guestHeroPickerButton.SetPreconDeckID(preconDeckID);
			guestHeroPickerButton.SetGuestHero(guestHeroRecord);
			AdventureGuestHeroesDbfRecord adventureGuestHeroRecord = guestHeroes[i].AdventureGuestHeroRecord;
			if (adventureGuestHeroRecord != null && !SceneMgr.Get().IsInDuelsMode())
			{
				HandleAdventureGuestHeroUnlockData(adventureGuestHeroRecord, guestHeroPickerButton);
			}
			if (guestHeroRecord == null)
			{
				m_heroDefsLoading--;
				guestHeroPickerButton.UpdateDisplay(null, TAG_PREMIUM.NORMAL);
			}
			else
			{
				string cardId = GameUtils.TranslateDbIdToCardId(guestHeroRecord.CardId);
				HeroFullDefLoadedCallbackData userData = new HeroFullDefLoadedCallbackData(guestHeroPickerButton, TAG_PREMIUM.NORMAL);
				DefLoader.Get().LoadFullDef(cardId, OnHeroFullDefLoaded, userData);
			}
			guestHeroPickerButton.SetDivotTexture(divotTexture);
		}
		if (IsChoosingHeroForDungeonCrawlAdventure())
		{
			SetUpHeroCrowns();
		}
	}

	protected override int ValidateHeroCount()
	{
		return GetGuestHeroes().Count;
	}

	protected override bool OnNavigateBackImplementation()
	{
		if (!base.OnNavigateBackImplementation())
		{
			return false;
		}
		switch ((SceneMgr.Get() != null) ? SceneMgr.Get().GetMode() : SceneMgr.Mode.INVALID)
		{
		case SceneMgr.Mode.ADVENTURE:
			if (AdventureConfig.Get() != null)
			{
				AdventureConfig.Get().SubSceneGoBack();
			}
			break;
		case SceneMgr.Mode.FRIENDLY:
		case SceneMgr.Mode.TAVERN_BRAWL:
		case SceneMgr.Mode.FIRESIDE_GATHERING:
			if (GuestHeroPickerDisplay.Get() != null)
			{
				GuestHeroPickerDisplay.Get().HideTray();
			}
			break;
		case SceneMgr.Mode.PVP_DUNGEON_RUN:
			if (PvPDungeonRunDisplay.Get() != null)
			{
				PvPDungeonRunScene.Get().TransitionBackFromGuestHeroPicker();
			}
			break;
		}
		return true;
	}

	public override void PreUnload()
	{
		if (m_randomDeckPickerTray.activeSelf)
		{
			m_randomDeckPickerTray.SetActive(value: false);
		}
	}

	protected override void UpdateHeroInfo(HeroPickerButton button)
	{
		using DefLoader.DisposableFullDef disposableFullDef = button.ShareFullDef();
		string heroName = disposableFullDef.EntityDef.GetName();
		string heroDescription = string.Empty;
		GuestHeroDbfRecord guestHero = button.GetGuestHero();
		if (guestHero != null)
		{
			heroDescription = guestHero.FlavorText;
		}
		UpdateHeroInfo(disposableFullDef, heroName, heroDescription, TAG_PREMIUM.NORMAL);
	}

	protected override void PlayGame()
	{
		base.PlayGame();
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.PVP_DUNGEON_RUN:
			PvPDungeonRunScene.Get().OnGuestHeroSelected(m_selectedHeroButton.m_heroClass, m_selectedHeroButton.GetGuestHero());
			EnableBackButton(enabled: false);
			break;
		case SceneMgr.Mode.ADVENTURE:
		{
			int selectedHeroID = GetSelectedHeroID();
			AdventureConfig ac = AdventureConfig.Get();
			if (OnPlayButtonPressed_SaveHeroAndAdvanceToDungeonRunIfNecessary())
			{
				GuestHeroDbfRecord guestHero = m_selectedHeroButton.GetGuestHero();
				if (guestHero != null)
				{
					AdventureGuestHeroesDbfRecord record = GameDbf.AdventureGuestHeroes.GetRecord((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)ac.GetSelectedAdventure() && r.GuestHeroId == guestHero.ID);
					if (record != null && record.CustomScenario != 0)
					{
						ac.SetMission((ScenarioDbId)record.CustomScenario);
					}
				}
			}
			else
			{
				ac.SubSceneGoBack(fireevent: false);
				ScenarioDbId missionToPlay = ac.GetMissionToPlay();
				GameMgr.Get().FindGameWithHero(GameType.GT_VS_AI, FormatType.FT_WILD, (int)missionToPlay, 0, selectedHeroID, 0L);
			}
			break;
		}
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_slidingTray.ToggleTraySlider(show: false);
		}
	}

	protected override void ShowHero()
	{
		UpdateHeroInfo(m_selectedHeroButton);
		base.ShowHero();
	}

	protected override void SelectHero(HeroPickerButton button, bool showTrayForPhone = true)
	{
		base.SelectHero(button, showTrayForPhone);
		UpdateSelectedHeroClasses(button);
		if (SceneMgr.Get() == null || SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE)
		{
			return;
		}
		HeroPickerOptionDataModel dataModel = button.GetDataModel();
		bool num = dataModel?.IsTimelocked ?? false;
		bool flag = dataModel?.IsUnowned ?? false;
		if (num)
		{
			string description = $"{dataModel.ComingSoonText} ({dataModel.UnlockCriteriaText})";
			AddHeroLockedTooltip(GameStrings.Get("GLOBAL_NOT_AVAILABLE"), description);
		}
		else if (flag)
		{
			AddHeroLockedTooltip(GameStrings.Get("GLUE_HERO_LOCKED_NAME"), dataModel.UnlockCriteriaText);
		}
		else if (!button.IsLocked())
		{
			AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
			List<long> values = null;
			if (!GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_UNLOCKED_HEROES, out values))
			{
				values = new List<long>();
			}
			if (!values.Contains(button.GetGuestHero().CardId))
			{
				values.Add(m_selectedHeroButton.GetGuestHero().CardId);
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_UNLOCKED_HEROES, values.ToArray()));
				button.GetDataModel().IsNewlyUnlocked = false;
			}
		}
	}

	protected override bool IsHeroPlayable(HeroPickerButton button)
	{
		HeroPickerOptionDataModel dataModel = button.GetDataModel();
		if (!button.IsLocked())
		{
			if (dataModel != null)
			{
				return !dataModel.IsUnowned;
			}
			return true;
		}
		return false;
	}

	protected override bool ShouldShowHeroPower()
	{
		return true;
	}

	protected override void GoBackUntilOnNavigateBackCalled()
	{
		Navigation.GoBackUntilOnNavigateBackCalled(OnNavigateBack);
	}

	public void EnableBackButton(bool enabled)
	{
		if (m_backButton != null)
		{
			m_backButton.SetEnabled(enabled);
			m_backButton.Flip(enabled);
		}
	}

	private void OnHeroPickerWidgetReady(WidgetTemplate widget)
	{
		if (m_collectionButton != null)
		{
			SetCollectionButtonEnabled(enable: false);
		}
	}

	private void HandleAdventureGuestHeroUnlockData(AdventureGuestHeroesDbfRecord adventureGuestHeroRecord, HeroPickerButton button)
	{
		if (adventureGuestHeroRecord == null)
		{
			Debug.LogError("HandleGuestHeroUnlockEvents: No adventure guest hero passed in.");
			return;
		}
		WingDbfRecord record = GameDbf.Wing.GetRecord(adventureGuestHeroRecord.WingId);
		bool flag = AdventureProgressMgr.IsWingEventActive(adventureGuestHeroRecord.WingId);
		string buttonLockedReasonText = GetButtonLockedReasonText(record);
		button.SetLockReasonText(buttonLockedReasonText);
		List<long> values = null;
		AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_UNLOCKED_HEROES, out values);
		HeroPickerOptionDataModel dataModel = button.GetDataModel();
		dataModel.IsTimelocked = !flag;
		dataModel.TimeLockInfoText = buttonLockedReasonText;
		dataModel.ComingSoonText = adventureGuestHeroRecord.ComingSoonText;
		dataModel.UnlockCriteriaText = adventureGuestHeroRecord.UnlockCriteriaText;
		dataModel.IsUnowned = !AdventureProgressMgr.Get().OwnsWing(adventureGuestHeroRecord.WingId);
		dataModel.IsNewlyUnlocked = AdventureUtils.DoesAdventureShowNewlyUnlockedGuestHeroTreatment((AdventureDbId)selectedAdventureDataRecord.AdventureId) && flag && !dataModel.IsUnowned && (values == null || (button.GetGuestHero() != null && !values.Contains(button.GetGuestHero().CardId)));
		if (!flag)
		{
			button.Lock();
			button.Activate(enable: false);
		}
	}

	private List<GuestHeroRecordContainer> GetGuestHeroes()
	{
		List<GuestHeroRecordContainer> result = null;
		List<AdventureGuestHeroesDbfRecord> list = null;
		List<ScenarioGuestHeroesDbfRecord> list2 = null;
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.PVP_DUNGEON_RUN:
			list = GetDuelsDraftHeroes();
			result = GetGuestHeroes(list);
			break;
		case SceneMgr.Mode.ADVENTURE:
			list = GetSortedAdventureGuestHeroes();
			result = GetGuestHeroes(list);
			break;
		case SceneMgr.Mode.FRIENDLY:
		case SceneMgr.Mode.TAVERN_BRAWL:
		case SceneMgr.Mode.FIRESIDE_GATHERING:
			list2 = GetScenarioGuestHeroes();
			result = GetGuestHeroes(list2);
			break;
		}
		return result;
	}

	private List<GuestHeroRecordContainer> GetGuestHeroes(List<AdventureGuestHeroesDbfRecord> adventureGuestHeroes)
	{
		List<GuestHeroRecordContainer> list = new List<GuestHeroRecordContainer>();
		foreach (AdventureGuestHeroesDbfRecord adventureGuestHero in adventureGuestHeroes)
		{
			GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord(adventureGuestHero.GuestHeroId);
			list.Add(new GuestHeroRecordContainer
			{
				GuestHeroRecord = record,
				AdventureGuestHeroRecord = adventureGuestHero
			});
		}
		return list;
	}

	private List<GuestHeroRecordContainer> GetGuestHeroes(List<ScenarioGuestHeroesDbfRecord> scenarioGuestHeroes)
	{
		List<GuestHeroRecordContainer> list = new List<GuestHeroRecordContainer>();
		foreach (ScenarioGuestHeroesDbfRecord scenarioGuestHero in scenarioGuestHeroes)
		{
			GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord(scenarioGuestHero.GuestHeroId);
			list.Add(new GuestHeroRecordContainer
			{
				GuestHeroRecord = record,
				ScenarioGuestHeroRecord = scenarioGuestHero
			});
		}
		return list;
	}

	private List<AdventureGuestHeroesDbfRecord> GetSortedAdventureGuestHeroes()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId currentAdventure = adventureConfig.GetSelectedAdventure();
		List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)currentAdventure);
		records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	private List<AdventureGuestHeroesDbfRecord> GetDuelsDraftHeroes()
	{
		List<long> draftedHeroes = DuelsConfig.GetDraftHeroesFromGSD();
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId currentAdventure = adventureConfig.GetSelectedAdventure();
		List<AdventureGuestHeroesDbfRecord> records;
		if (draftedHeroes != null && draftedHeroes.Count > 0)
		{
			records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)currentAdventure && draftedHeroes.Contains(r.GuestHeroId));
		}
		else
		{
			PvpdrSeasonDbfRecord seasonDBFRecord = DuelsConfig.GetSeasonDBFRecord();
			int limit = 3;
			if (seasonDBFRecord != null)
			{
				limit = seasonDBFRecord.MaxHeroesDrafted;
			}
			records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)currentAdventure, limit);
		}
		records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	private List<ScenarioGuestHeroesDbfRecord> GetScenarioGuestHeroes()
	{
		TavernBrawlMission currentMission = TavernBrawlManager.Get().CurrentMission();
		List<ScenarioGuestHeroesDbfRecord> records = GameDbf.ScenarioGuestHeroes.GetRecords((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == currentMission.missionId);
		records.Sort((ScenarioGuestHeroesDbfRecord a, ScenarioGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	private List<ScenarioGuestHeroesDbfRecord> GetPvPDungeonRunGuestHeroes()
	{
		int missionId = (int)AdventureConfig.Get().GetMission();
		List<ScenarioGuestHeroesDbfRecord> records = GameDbf.ScenarioGuestHeroes.GetRecords((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == missionId);
		records.Sort((ScenarioGuestHeroesDbfRecord a, ScenarioGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	private string GetButtonLockedReasonText(WingDbfRecord wingRecord)
	{
		if (wingRecord.UseUnlockCountdown)
		{
			SpecialEventType wingEventTiming = AdventureProgressMgr.GetWingEventTiming(wingRecord.ID);
			TimeSpan? timeSpan = SpecialEventManager.Get().GetEventStartTimeUtc(wingEventTiming) - DateTime.UtcNow;
			if (!timeSpan.HasValue)
			{
				return GameStrings.Get("GLOBAL_DATETIME_COMING_SOON");
			}
			TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
			{
				m_weeks = "GLOBAL_DATETIME_UNLOCKS_SOON_WEEKS"
			};
			return TimeUtils.GetElapsedTimeString((long)timeSpan.Value.TotalSeconds, stringSet, roundUp: true);
		}
		return wingRecord.ComingSoonLabel;
	}

	private void ShowFirstPage()
	{
		if (iTween.Count(m_randomDeckPickerTray) <= 0)
		{
			m_randomDeckPickerTray.SetActive(value: true);
			ShowPreconHighlights();
		}
	}

	private void ShowPreconHighlights()
	{
		if (!AbsDeckPickerTrayDisplay.HIGHLIGHT_SELECTED_DECK)
		{
			return;
		}
		foreach (HeroPickerButton heroButton in m_heroButtons)
		{
			if (heroButton == m_selectedHeroButton)
			{
				heroButton.SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
		}
	}

	private void UpdateHeroInfo(DefLoader.DisposableFullDef fullDef, string heroName, string heroDescription, TAG_PREMIUM premium)
	{
		m_heroName.Text = heroName;
		if (m_heroDescription != null)
		{
			m_heroDescription.Text = heroDescription;
		}
		m_selectedHeroName = fullDef.EntityDef.GetName();
		m_heroActor.SetPremium(premium);
		m_heroActor.SetEntityDef(fullDef.EntityDef);
		m_heroActor.SetCardDef(fullDef.DisposableCardDef);
		m_heroActor.UpdateAllComponents();
		m_heroActor.SetUnlit();
		string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(fullDef.EntityDef.GetCardId());
		if (!string.IsNullOrEmpty(heroPowerCardIdFromHero))
		{
			UpdateHeroPowerInfo(m_heroPowerDefs[heroPowerCardIdFromHero], premium);
			return;
		}
		SetHeroPowerActorColliderEnabled(enable: false);
		HideHeroPowerActor();
	}

	private void Cheat_LockAllButtons()
	{
		foreach (HeroPickerButton heroButton in m_heroButtons)
		{
			heroButton.Lock();
			heroButton.Activate(enable: false);
		}
	}

	public static GuestHeroPickerTrayDisplay Get()
	{
		return s_instance;
	}

	public void SetChooseHeroText(string text)
	{
		if (m_chooseHeroLabel != null)
		{
			m_chooseHeroLabel.Text = text;
		}
	}

	private void UpdateSelectedHeroClasses(HeroPickerButton button)
	{
		VisualController component = GetComponent<VisualController>();
		if (component == null)
		{
			return;
		}
		HeroClassIconsDataModel heroClassIconsDataModel = new HeroClassIconsDataModel();
		using DefLoader.DisposableFullDef disposableFullDef = button.ShareFullDef();
		EntityDef entityDef = disposableFullDef?.EntityDef;
		if (entityDef == null)
		{
			Debug.LogWarning("GuestHeroPickerTrayDisplay.UpdateSelectedHeroClasses - button did not contain an entity def!");
			return;
		}
		heroClassIconsDataModel.Classes.Clear();
		foreach (TAG_CLASS @class in entityDef.GetClasses())
		{
			heroClassIconsDataModel.Classes.Add(@class);
		}
		heroClassIconsDataModel.Classes.Sort((TAG_CLASS a, TAG_CLASS b) => (a == TAG_CLASS.NEUTRAL) ? 1 : (-1));
		component.Owner.BindDataModel(heroClassIconsDataModel);
	}
}
