using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x020002AA RID: 682
[CustomEditClass]
public class GuestHeroPickerTrayDisplay : AbsDeckPickerTrayDisplay
{
	// Token: 0x06002351 RID: 9041 RVA: 0x000B029C File Offset: 0x000AE49C
	public override void Awake()
	{
		base.Awake();
		GuestHeroPickerTrayDisplay.s_instance = this;
		HeroPickerDataModel heroPickerDataModel = base.GetHeroPickerDataModel();
		if (heroPickerDataModel != null)
		{
			heroPickerDataModel.HasGuestHeroes = true;
		}
		VisualController visualController = base.GetComponent<VisualController>();
		if (visualController != null)
		{
			visualController.Owner.RegisterReadyListener(delegate(object _)
			{
				this.OnHeroPickerWidgetReady(visualController.Owner);
			}, null, true);
			return;
		}
		Debug.LogError("AbsDeckPickerTrayDisplay.Awake - could not find visual controller. Ensure that this component is on the same object as the visual controller.");
	}

	// Token: 0x06002352 RID: 9042 RVA: 0x000B0316 File Offset: 0x000AE516
	private void Start()
	{
		Navigation.PushIfNotOnTop(new Navigation.NavigateBackHandler(GuestHeroPickerTrayDisplay.OnNavigateBack));
	}

	// Token: 0x06002353 RID: 9043 RVA: 0x000B0329 File Offset: 0x000AE529
	private void Update()
	{
		if (AdventureScene.Get() == null || !AdventureScene.Get().IsDevMode)
		{
			return;
		}
		if (InputCollection.GetKeyDown(KeyCode.Z))
		{
			this.Cheat_LockAllButtons();
		}
	}

	// Token: 0x06002354 RID: 9044 RVA: 0x000B0354 File Offset: 0x000AE554
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (GuestHeroPickerTrayDisplay.Get() == this)
		{
			GuestHeroPickerTrayDisplay.s_instance = null;
		}
	}

	// Token: 0x06002355 RID: 9045 RVA: 0x000B036F File Offset: 0x000AE56F
	public static bool OnNavigateBack()
	{
		if (GuestHeroPickerTrayDisplay.Get() != null)
		{
			return GuestHeroPickerTrayDisplay.Get().OnNavigateBackImplementation();
		}
		Debug.LogError("GuestHeroPickerTrayDisplay: tried to navigate back but had null instance!");
		return false;
	}

	// Token: 0x06002356 RID: 9046 RVA: 0x000B0394 File Offset: 0x000AE594
	protected override IEnumerator InitModeWhenReady()
	{
		yield return base.StartCoroutine(base.InitModeWhenReady());
		if (SceneMgr.Get().IsInDuelsMode())
		{
			base.SetBackButtonEnabled(false);
		}
		this.ShowFirstPage();
		yield break;
	}

	// Token: 0x06002357 RID: 9047 RVA: 0x000B03A4 File Offset: 0x000AE5A4
	protected override void InitForMode(SceneMgr.Mode mode)
	{
		base.GetComponent<VisualController>();
		if (mode != SceneMgr.Mode.FRIENDLY)
		{
			switch (mode)
			{
			case SceneMgr.Mode.ADVENTURE:
			case SceneMgr.Mode.PVP_DUNGEON_RUN:
			{
				AdventureConfig adventureConfig = AdventureConfig.Get();
				AdventureDbId selectedAdventure = adventureConfig.GetSelectedAdventure();
				AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
				AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode);
				base.SetHeaderText(adventureDataRecord.Name);
				AdventureSubScene component = GuestHeroPickerDisplay.Get().GetComponent<AdventureSubScene>();
				if (component != null)
				{
					component.SetIsLoaded(true);
				}
				if (mode == SceneMgr.Mode.PVP_DUNGEON_RUN && GuestHeroPickerDisplay.Get() != null)
				{
					GuestHeroPickerDisplay.Get().ShowTray();
					goto IL_11B;
				}
				goto IL_11B;
			}
			case SceneMgr.Mode.TAVERN_BRAWL:
			case SceneMgr.Mode.FIRESIDE_GATHERING:
				break;
			case SceneMgr.Mode.BACON:
			case SceneMgr.Mode.GAME_MODE:
				goto IL_11B;
			default:
				goto IL_11B;
			}
		}
		string key = (TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? "GLOBAL_HEROIC_BRAWL" : "GLOBAL_TAVERN_BRAWL";
		TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(tavernBrawlMission.missionId);
		base.SetHeaderText(GameStrings.Get(key));
		if (record.ChooseHeroText != null)
		{
			this.SetChooseHeroText(record.ChooseHeroText);
		}
		if (GuestHeroPickerDisplay.Get() != null && mode != SceneMgr.Mode.FRIENDLY)
		{
			GuestHeroPickerDisplay.Get().ShowTray();
		}
		IL_11B:
		base.SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
		base.InitForMode(mode);
	}

	// Token: 0x06002358 RID: 9048 RVA: 0x000B04E4 File Offset: 0x000AE6E4
	protected override void InitHeroPickerButtons()
	{
		base.InitHeroPickerButtons();
		List<GuestHeroPickerTrayDisplay.GuestHeroRecordContainer> guestHeroes = this.GetGuestHeroes();
		if (guestHeroes == null)
		{
			Debug.LogError("InitHeroPickerButtons: Unable to get guest heroes to display.");
			return;
		}
		IDataModel dataModel;
		if (!GlobalDataContext.Get().GetDataModel(7, out dataModel))
		{
			dataModel = new AdventureDataModel();
			GlobalDataContext.Get().BindDataModel(dataModel);
		}
		AdventureDataModel adventureDataModel = dataModel as AdventureDataModel;
		if (adventureDataModel == null)
		{
			Log.Adventures.PrintWarning("AdventureDataModel is null!", Array.Empty<object>());
		}
		AdventureDbId selectedAdventure = adventureDataModel.SelectedAdventure;
		Texture divotTexture;
		if (selectedAdventure != AdventureDbId.DALARAN)
		{
			if (selectedAdventure != AdventureDbId.ULDUM)
			{
				divotTexture = (SceneMgr.Get().IsInDuelsMode() ? this.m_divotTexturePVPDR : this.m_divotTextureDefault);
			}
			else
			{
				divotTexture = this.m_divotTextureUldum;
			}
		}
		else
		{
			divotTexture = this.m_divotTextureDalaran;
		}
		this.m_heroDefsLoading = guestHeroes.Count;
		for (int i = 0; i < guestHeroes.Count; i++)
		{
			if (i >= this.m_heroButtons.Count || this.m_heroButtons[i] == null)
			{
				Debug.LogWarning("InitHeroPickerButtons: not enough buttons for total guest heroes.");
				break;
			}
			GuestHeroPickerButton guestHeroPickerButton = this.m_heroButtons[i] as GuestHeroPickerButton;
			if (guestHeroPickerButton == null)
			{
				Debug.LogWarning("InitHeroPickerButtons: attempted to display null button.");
				this.m_heroDefsLoading--;
			}
			else
			{
				GuestHeroDbfRecord guestHeroRecord = guestHeroes[i].GuestHeroRecord;
				if (guestHeroRecord == null)
				{
					guestHeroPickerButton.Lock();
					guestHeroPickerButton.Activate(false);
				}
				else
				{
					guestHeroPickerButton.Unlock();
					guestHeroPickerButton.Activate(true);
				}
				long preconDeckID = 0L;
				TAG_CLASS tag_CLASS = (guestHeroRecord == null) ? TAG_CLASS.INVALID : GameUtils.GetTagClassFromCardDbId(guestHeroRecord.CardId);
				if (tag_CLASS != TAG_CLASS.INVALID)
				{
					CollectionManager.PreconDeck preconDeck = CollectionManager.Get().GetPreconDeck(tag_CLASS);
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
					this.HandleAdventureGuestHeroUnlockData(adventureGuestHeroRecord, guestHeroPickerButton);
				}
				if (guestHeroRecord == null)
				{
					this.m_heroDefsLoading--;
					guestHeroPickerButton.UpdateDisplay(null, TAG_PREMIUM.NORMAL);
				}
				else
				{
					string cardId = GameUtils.TranslateDbIdToCardId(guestHeroRecord.CardId, false);
					AbsDeckPickerTrayDisplay.HeroFullDefLoadedCallbackData userData = new AbsDeckPickerTrayDisplay.HeroFullDefLoadedCallbackData(guestHeroPickerButton, TAG_PREMIUM.NORMAL);
					DefLoader.Get().LoadFullDef(cardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnHeroFullDefLoaded), userData, null);
				}
				guestHeroPickerButton.SetDivotTexture(divotTexture);
			}
		}
		if (base.IsChoosingHeroForDungeonCrawlAdventure())
		{
			base.SetUpHeroCrowns();
		}
	}

	// Token: 0x06002359 RID: 9049 RVA: 0x000B0731 File Offset: 0x000AE931
	protected override int ValidateHeroCount()
	{
		return this.GetGuestHeroes().Count;
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x000B0740 File Offset: 0x000AE940
	protected override bool OnNavigateBackImplementation()
	{
		if (!base.OnNavigateBackImplementation())
		{
			return false;
		}
		SceneMgr.Mode mode = (SceneMgr.Get() != null) ? SceneMgr.Get().GetMode() : SceneMgr.Mode.INVALID;
		if (mode != SceneMgr.Mode.FRIENDLY)
		{
			switch (mode)
			{
			case SceneMgr.Mode.ADVENTURE:
				if (AdventureConfig.Get() != null)
				{
					AdventureConfig.Get().SubSceneGoBack(true);
					return true;
				}
				return true;
			case SceneMgr.Mode.TAVERN_BRAWL:
			case SceneMgr.Mode.FIRESIDE_GATHERING:
				break;
			case SceneMgr.Mode.BACON:
			case SceneMgr.Mode.GAME_MODE:
				return true;
			case SceneMgr.Mode.PVP_DUNGEON_RUN:
				if (PvPDungeonRunDisplay.Get() != null)
				{
					PvPDungeonRunScene.Get().TransitionBackFromGuestHeroPicker();
					return true;
				}
				return true;
			default:
				return true;
			}
		}
		if (GuestHeroPickerDisplay.Get() != null)
		{
			GuestHeroPickerDisplay.Get().HideTray(0f);
		}
		return true;
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x000B07E3 File Offset: 0x000AE9E3
	public override void PreUnload()
	{
		if (this.m_randomDeckPickerTray.activeSelf)
		{
			this.m_randomDeckPickerTray.SetActive(false);
		}
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x000B0800 File Offset: 0x000AEA00
	protected override void UpdateHeroInfo(HeroPickerButton button)
	{
		using (DefLoader.DisposableFullDef disposableFullDef = button.ShareFullDef())
		{
			string name = disposableFullDef.EntityDef.GetName();
			string heroDescription = string.Empty;
			GuestHeroDbfRecord guestHero = button.GetGuestHero();
			if (guestHero != null)
			{
				heroDescription = guestHero.FlavorText;
			}
			this.UpdateHeroInfo(disposableFullDef, name, heroDescription, TAG_PREMIUM.NORMAL);
		}
	}

	// Token: 0x0600235D RID: 9053 RVA: 0x000B0864 File Offset: 0x000AEA64
	protected override void PlayGame()
	{
		base.PlayGame();
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode != SceneMgr.Mode.ADVENTURE)
		{
			if (mode == SceneMgr.Mode.PVP_DUNGEON_RUN)
			{
				PvPDungeonRunScene.Get().OnGuestHeroSelected(this.m_selectedHeroButton.m_heroClass, this.m_selectedHeroButton.GetGuestHero());
				this.EnableBackButton(false);
			}
		}
		else
		{
			int selectedHeroID = this.GetSelectedHeroID();
			AdventureConfig ac = AdventureConfig.Get();
			if (base.OnPlayButtonPressed_SaveHeroAndAdvanceToDungeonRunIfNecessary())
			{
				GuestHeroDbfRecord guestHero = this.m_selectedHeroButton.GetGuestHero();
				if (guestHero != null)
				{
					AdventureGuestHeroesDbfRecord record = GameDbf.AdventureGuestHeroes.GetRecord((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)ac.GetSelectedAdventure() && r.GuestHeroId == guestHero.ID);
					if (record != null && record.CustomScenario != 0)
					{
						ac.SetMission((ScenarioDbId)record.CustomScenario, true);
					}
				}
			}
			else
			{
				ac.SubSceneGoBack(false);
				ScenarioDbId missionToPlay = ac.GetMissionToPlay();
				GameMgr.Get().FindGameWithHero(GameType.GT_VS_AI, FormatType.FT_WILD, (int)missionToPlay, 0, selectedHeroID, 0L);
			}
		}
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_slidingTray.ToggleTraySlider(false, null, true);
		}
	}

	// Token: 0x0600235E RID: 9054 RVA: 0x000B0989 File Offset: 0x000AEB89
	protected override void ShowHero()
	{
		this.UpdateHeroInfo(this.m_selectedHeroButton);
		base.ShowHero();
	}

	// Token: 0x0600235F RID: 9055 RVA: 0x000B09A0 File Offset: 0x000AEBA0
	protected override void SelectHero(HeroPickerButton button, bool showTrayForPhone = true)
	{
		base.SelectHero(button, showTrayForPhone);
		this.UpdateSelectedHeroClasses(button);
		if (SceneMgr.Get() == null || SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE)
		{
			return;
		}
		HeroPickerOptionDataModel dataModel = button.GetDataModel();
		bool flag = dataModel != null && dataModel.IsTimelocked;
		bool flag2 = dataModel != null && dataModel.IsUnowned;
		if (flag)
		{
			string description = string.Format("{0} ({1})", dataModel.ComingSoonText, dataModel.UnlockCriteriaText);
			base.AddHeroLockedTooltip(GameStrings.Get("GLOBAL_NOT_AVAILABLE"), description);
			return;
		}
		if (flag2)
		{
			base.AddHeroLockedTooltip(GameStrings.Get("GLUE_HERO_LOCKED_NAME"), dataModel.UnlockCriteriaText);
			return;
		}
		if (button.IsLocked())
		{
			return;
		}
		AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		List<long> list = null;
		if (!GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_UNLOCKED_HEROES, out list))
		{
			list = new List<long>();
		}
		if (!list.Contains((long)button.GetGuestHero().CardId))
		{
			list.Add((long)this.m_selectedHeroButton.GetGuestHero().CardId);
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_UNLOCKED_HEROES, list.ToArray()), null);
			button.GetDataModel().IsNewlyUnlocked = false;
		}
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x000B0AC8 File Offset: 0x000AECC8
	protected override bool IsHeroPlayable(HeroPickerButton button)
	{
		HeroPickerOptionDataModel dataModel = button.GetDataModel();
		return !button.IsLocked() && (dataModel == null || !dataModel.IsUnowned);
	}

	// Token: 0x06002361 RID: 9057 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool ShouldShowHeroPower()
	{
		return true;
	}

	// Token: 0x06002362 RID: 9058 RVA: 0x000B0AF4 File Offset: 0x000AECF4
	protected override void GoBackUntilOnNavigateBackCalled()
	{
		Navigation.GoBackUntilOnNavigateBackCalled(new Navigation.NavigateBackHandler(GuestHeroPickerTrayDisplay.OnNavigateBack));
	}

	// Token: 0x06002363 RID: 9059 RVA: 0x000B0B07 File Offset: 0x000AED07
	public void EnableBackButton(bool enabled)
	{
		if (this.m_backButton != null)
		{
			this.m_backButton.SetEnabled(enabled, false);
			this.m_backButton.Flip(enabled, false);
		}
	}

	// Token: 0x06002364 RID: 9060 RVA: 0x000B0B31 File Offset: 0x000AED31
	private void OnHeroPickerWidgetReady(WidgetTemplate widget)
	{
		if (this.m_collectionButton != null)
		{
			this.SetCollectionButtonEnabled(false);
		}
	}

	// Token: 0x06002365 RID: 9061 RVA: 0x000B0B48 File Offset: 0x000AED48
	private void HandleAdventureGuestHeroUnlockData(AdventureGuestHeroesDbfRecord adventureGuestHeroRecord, HeroPickerButton button)
	{
		if (adventureGuestHeroRecord == null)
		{
			Debug.LogError("HandleGuestHeroUnlockEvents: No adventure guest hero passed in.");
			return;
		}
		WingDbfRecord record = GameDbf.Wing.GetRecord(adventureGuestHeroRecord.WingId);
		bool flag = AdventureProgressMgr.IsWingEventActive(adventureGuestHeroRecord.WingId);
		string buttonLockedReasonText = this.GetButtonLockedReasonText(record);
		button.SetLockReasonText(buttonLockedReasonText);
		List<long> list = null;
		AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_UNLOCKED_HEROES, out list);
		HeroPickerOptionDataModel dataModel = button.GetDataModel();
		dataModel.IsTimelocked = !flag;
		dataModel.TimeLockInfoText = buttonLockedReasonText;
		dataModel.ComingSoonText = adventureGuestHeroRecord.ComingSoonText;
		dataModel.UnlockCriteriaText = adventureGuestHeroRecord.UnlockCriteriaText;
		dataModel.IsUnowned = !AdventureProgressMgr.Get().OwnsWing(adventureGuestHeroRecord.WingId);
		dataModel.IsNewlyUnlocked = (AdventureUtils.DoesAdventureShowNewlyUnlockedGuestHeroTreatment((AdventureDbId)selectedAdventureDataRecord.AdventureId) && flag && !dataModel.IsUnowned && (list == null || (button.GetGuestHero() != null && !list.Contains((long)button.GetGuestHero().CardId))));
		if (!flag)
		{
			button.Lock();
			button.Activate(false);
		}
	}

	// Token: 0x06002366 RID: 9062 RVA: 0x000B0C68 File Offset: 0x000AEE68
	private List<GuestHeroPickerTrayDisplay.GuestHeroRecordContainer> GetGuestHeroes()
	{
		List<GuestHeroPickerTrayDisplay.GuestHeroRecordContainer> result = null;
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode != SceneMgr.Mode.FRIENDLY)
		{
			switch (mode)
			{
			case SceneMgr.Mode.ADVENTURE:
			{
				List<AdventureGuestHeroesDbfRecord> adventureGuestHeroes = this.GetSortedAdventureGuestHeroes();
				return this.GetGuestHeroes(adventureGuestHeroes);
			}
			case SceneMgr.Mode.TAVERN_BRAWL:
			case SceneMgr.Mode.FIRESIDE_GATHERING:
				break;
			case SceneMgr.Mode.BACON:
			case SceneMgr.Mode.GAME_MODE:
				return result;
			case SceneMgr.Mode.PVP_DUNGEON_RUN:
			{
				List<AdventureGuestHeroesDbfRecord> adventureGuestHeroes = this.GetDuelsDraftHeroes();
				return this.GetGuestHeroes(adventureGuestHeroes);
			}
			default:
				return result;
			}
		}
		List<ScenarioGuestHeroesDbfRecord> scenarioGuestHeroes = this.GetScenarioGuestHeroes();
		result = this.GetGuestHeroes(scenarioGuestHeroes);
		return result;
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x000B0CE0 File Offset: 0x000AEEE0
	private List<GuestHeroPickerTrayDisplay.GuestHeroRecordContainer> GetGuestHeroes(List<AdventureGuestHeroesDbfRecord> adventureGuestHeroes)
	{
		List<GuestHeroPickerTrayDisplay.GuestHeroRecordContainer> list = new List<GuestHeroPickerTrayDisplay.GuestHeroRecordContainer>();
		foreach (AdventureGuestHeroesDbfRecord adventureGuestHeroesDbfRecord in adventureGuestHeroes)
		{
			GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord(adventureGuestHeroesDbfRecord.GuestHeroId);
			list.Add(new GuestHeroPickerTrayDisplay.GuestHeroRecordContainer
			{
				GuestHeroRecord = record,
				AdventureGuestHeroRecord = adventureGuestHeroesDbfRecord
			});
		}
		return list;
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x000B0D60 File Offset: 0x000AEF60
	private List<GuestHeroPickerTrayDisplay.GuestHeroRecordContainer> GetGuestHeroes(List<ScenarioGuestHeroesDbfRecord> scenarioGuestHeroes)
	{
		List<GuestHeroPickerTrayDisplay.GuestHeroRecordContainer> list = new List<GuestHeroPickerTrayDisplay.GuestHeroRecordContainer>();
		foreach (ScenarioGuestHeroesDbfRecord scenarioGuestHeroesDbfRecord in scenarioGuestHeroes)
		{
			GuestHeroDbfRecord record = GameDbf.GuestHero.GetRecord(scenarioGuestHeroesDbfRecord.GuestHeroId);
			list.Add(new GuestHeroPickerTrayDisplay.GuestHeroRecordContainer
			{
				GuestHeroRecord = record,
				ScenarioGuestHeroRecord = scenarioGuestHeroesDbfRecord
			});
		}
		return list;
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x000B0DE0 File Offset: 0x000AEFE0
	private List<AdventureGuestHeroesDbfRecord> GetSortedAdventureGuestHeroes()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId currentAdventure = adventureConfig.GetSelectedAdventure();
		List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)currentAdventure, -1);
		records.Sort((AdventureGuestHeroesDbfRecord a, AdventureGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x000B0E44 File Offset: 0x000AF044
	private List<AdventureGuestHeroesDbfRecord> GetDuelsDraftHeroes()
	{
		List<long> draftedHeroes = DuelsConfig.GetDraftHeroesFromGSD();
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId currentAdventure = adventureConfig.GetSelectedAdventure();
		List<AdventureGuestHeroesDbfRecord> records;
		if (draftedHeroes != null && draftedHeroes.Count > 0)
		{
			records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)currentAdventure && draftedHeroes.Contains((long)r.GuestHeroId), -1);
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

	// Token: 0x0600236B RID: 9067 RVA: 0x000B0EF8 File Offset: 0x000AF0F8
	private List<ScenarioGuestHeroesDbfRecord> GetScenarioGuestHeroes()
	{
		TavernBrawlMission currentMission = TavernBrawlManager.Get().CurrentMission();
		List<ScenarioGuestHeroesDbfRecord> records = GameDbf.ScenarioGuestHeroes.GetRecords((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == currentMission.missionId, -1);
		records.Sort((ScenarioGuestHeroesDbfRecord a, ScenarioGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x000B0F58 File Offset: 0x000AF158
	private List<ScenarioGuestHeroesDbfRecord> GetPvPDungeonRunGuestHeroes()
	{
		int missionId = (int)AdventureConfig.Get().GetMission();
		List<ScenarioGuestHeroesDbfRecord> records = GameDbf.ScenarioGuestHeroes.GetRecords((ScenarioGuestHeroesDbfRecord r) => r.ScenarioId == missionId, -1);
		records.Sort((ScenarioGuestHeroesDbfRecord a, ScenarioGuestHeroesDbfRecord b) => a.SortOrder.CompareTo(b.SortOrder));
		return records;
	}

	// Token: 0x0600236D RID: 9069 RVA: 0x000B0FB8 File Offset: 0x000AF1B8
	private string GetButtonLockedReasonText(WingDbfRecord wingRecord)
	{
		if (!wingRecord.UseUnlockCountdown)
		{
			return wingRecord.ComingSoonLabel;
		}
		SpecialEventType wingEventTiming = AdventureProgressMgr.GetWingEventTiming(wingRecord.ID);
		TimeSpan? timeSpan = SpecialEventManager.Get().GetEventStartTimeUtc(wingEventTiming) - DateTime.UtcNow;
		if (timeSpan == null)
		{
			return GameStrings.Get("GLOBAL_DATETIME_COMING_SOON");
		}
		TimeUtils.ElapsedStringSet stringSet = new TimeUtils.ElapsedStringSet
		{
			m_weeks = "GLOBAL_DATETIME_UNLOCKS_SOON_WEEKS"
		};
		return TimeUtils.GetElapsedTimeString((long)timeSpan.Value.TotalSeconds, stringSet, true);
	}

	// Token: 0x0600236E RID: 9070 RVA: 0x000B1061 File Offset: 0x000AF261
	private void ShowFirstPage()
	{
		if (iTween.Count(this.m_randomDeckPickerTray) > 0)
		{
			return;
		}
		this.m_randomDeckPickerTray.SetActive(true);
		this.ShowPreconHighlights();
	}

	// Token: 0x0600236F RID: 9071 RVA: 0x000B1084 File Offset: 0x000AF284
	private void ShowPreconHighlights()
	{
		if (!AbsDeckPickerTrayDisplay.HIGHLIGHT_SELECTED_DECK)
		{
			return;
		}
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			if (heroPickerButton == this.m_selectedHeroButton)
			{
				heroPickerButton.SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
		}
	}

	// Token: 0x06002370 RID: 9072 RVA: 0x000B10F4 File Offset: 0x000AF2F4
	private void UpdateHeroInfo(DefLoader.DisposableFullDef fullDef, string heroName, string heroDescription, TAG_PREMIUM premium)
	{
		this.m_heroName.Text = heroName;
		if (this.m_heroDescription != null)
		{
			this.m_heroDescription.Text = heroDescription;
		}
		this.m_selectedHeroName = fullDef.EntityDef.GetName();
		this.m_heroActor.SetPremium(premium);
		this.m_heroActor.SetEntityDef(fullDef.EntityDef);
		this.m_heroActor.SetCardDef(fullDef.DisposableCardDef);
		this.m_heroActor.UpdateAllComponents();
		this.m_heroActor.SetUnlit();
		string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(fullDef.EntityDef.GetCardId());
		if (!string.IsNullOrEmpty(heroPowerCardIdFromHero))
		{
			base.UpdateHeroPowerInfo(this.m_heroPowerDefs[heroPowerCardIdFromHero], premium);
			return;
		}
		base.SetHeroPowerActorColliderEnabled(false);
		base.HideHeroPowerActor();
	}

	// Token: 0x06002371 RID: 9073 RVA: 0x000B11B8 File Offset: 0x000AF3B8
	private void Cheat_LockAllButtons()
	{
		foreach (HeroPickerButton heroPickerButton in this.m_heroButtons)
		{
			heroPickerButton.Lock();
			heroPickerButton.Activate(false);
		}
	}

	// Token: 0x06002372 RID: 9074 RVA: 0x000B1210 File Offset: 0x000AF410
	public static GuestHeroPickerTrayDisplay Get()
	{
		return GuestHeroPickerTrayDisplay.s_instance;
	}

	// Token: 0x06002373 RID: 9075 RVA: 0x000B1217 File Offset: 0x000AF417
	public void SetChooseHeroText(string text)
	{
		if (this.m_chooseHeroLabel != null)
		{
			this.m_chooseHeroLabel.Text = text;
		}
	}

	// Token: 0x06002374 RID: 9076 RVA: 0x000B1234 File Offset: 0x000AF434
	private void UpdateSelectedHeroClasses(HeroPickerButton button)
	{
		VisualController component = base.GetComponent<VisualController>();
		if (component == null)
		{
			return;
		}
		HeroClassIconsDataModel heroClassIconsDataModel = new HeroClassIconsDataModel();
		using (DefLoader.DisposableFullDef disposableFullDef = button.ShareFullDef())
		{
			EntityDef entityDef = (disposableFullDef != null) ? disposableFullDef.EntityDef : null;
			if (entityDef == null)
			{
				Debug.LogWarning("GuestHeroPickerTrayDisplay.UpdateSelectedHeroClasses - button did not contain an entity def!");
			}
			else
			{
				heroClassIconsDataModel.Classes.Clear();
				foreach (TAG_CLASS item in entityDef.GetClasses(null))
				{
					heroClassIconsDataModel.Classes.Add(item);
				}
				heroClassIconsDataModel.Classes.Sort(delegate(TAG_CLASS a, TAG_CLASS b)
				{
					if (a != TAG_CLASS.NEUTRAL)
					{
						return -1;
					}
					return 1;
				});
				component.Owner.BindDataModel(heroClassIconsDataModel, false);
			}
		}
	}

	// Token: 0x0400139E RID: 5022
	public UberText m_heroDescription;

	// Token: 0x0400139F RID: 5023
	public UberText m_chooseHeroLabel;

	// Token: 0x040013A0 RID: 5024
	[CustomEditField(Sections = "Hero Divot Textures")]
	public Texture m_divotTextureDefault;

	// Token: 0x040013A1 RID: 5025
	[CustomEditField(Sections = "Hero Divot Textures")]
	public Texture m_divotTextureDalaran;

	// Token: 0x040013A2 RID: 5026
	[CustomEditField(Sections = "Hero Divot Textures")]
	public Texture m_divotTextureUldum;

	// Token: 0x040013A3 RID: 5027
	[CustomEditField(Sections = "Hero Divot Textures")]
	public Texture m_divotTexturePVPDR;

	// Token: 0x040013A4 RID: 5028
	private static GuestHeroPickerTrayDisplay s_instance;

	// Token: 0x0200159A RID: 5530
	// (Invoke) Token: 0x0600E101 RID: 57601
	public delegate void GuestHeroSelectedCallback(TAG_CLASS classId, GuestHeroDbfRecord record);

	// Token: 0x0200159B RID: 5531
	private struct GuestHeroRecordContainer
	{
		// Token: 0x0400AE6F RID: 44655
		public GuestHeroDbfRecord GuestHeroRecord;

		// Token: 0x0400AE70 RID: 44656
		public AdventureGuestHeroesDbfRecord AdventureGuestHeroRecord;

		// Token: 0x0400AE71 RID: 44657
		public ScenarioGuestHeroesDbfRecord ScenarioGuestHeroRecord;
	}
}
