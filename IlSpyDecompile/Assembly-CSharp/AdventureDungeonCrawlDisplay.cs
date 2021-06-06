using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.DungeonCrawl;
using Hearthstone.UI;
using PegasusClient;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public class AdventureDungeonCrawlDisplay : MonoBehaviour
{
	[Serializable]
	public class DungeonCrawlDisplayStyleOverride
	{
		public DungeonRunVisualStyle VisualStyle;

		public Material DungeonCrawlTrayMaterial;

		public Material PhoneDeckTrayMaterial;
	}

	public class PlayerHeroData
	{
		public delegate void DataChangedEventHandler();

		private TAG_CLASS _deckClass;

		private IDungeonCrawlData m_dungeonCrawlData;

		public TAG_CLASS DeckClass
		{
			get
			{
				return _deckClass;
			}
			set
			{
				_deckClass = value;
				if (value == TAG_CLASS.INVALID)
				{
					HeroCardId = null;
					HeroDbId = 0;
				}
				else
				{
					HeroCardId = AdventureUtils.GetHeroCardIdFromClassForDungeonCrawl(m_dungeonCrawlData, value);
					HeroDbId = GameUtils.TranslateCardIdToDbId(HeroCardId);
				}
				if (this.OnHeroDataChanged != null)
				{
					this.OnHeroDataChanged();
				}
			}
		}

		public int HeroDbId { get; private set; }

		public string HeroCardId { get; private set; }

		public event DataChangedEventHandler OnHeroDataChanged;

		public PlayerHeroData(IDungeonCrawlData dungeonCrawlData)
		{
			m_dungeonCrawlData = dungeonCrawlData;
		}
	}

	private enum DungeonRunLoadoutState
	{
		INVALID,
		HEROPOWER,
		TREASURE,
		DECKTEMPLATE,
		LOADOUTCOMPLETE
	}

	[CustomEditField(Sections = "UI")]
	public UberText m_AdventureTitle;

	[CustomEditField(Sections = "UI")]
	public UIBButton m_BackButton;

	[CustomEditField(Sections = "UI")]
	public AdventureDungeonCrawlDeckTray m_dungeonCrawlDeckTray;

	[CustomEditField(Sections = "UI")]
	public AsyncReference m_dungeonCrawlDeckSelectWidgetReference;

	[CustomEditField(Sections = "UI")]
	public AsyncReference m_dungeonCrawlPlayMatReference;

	[CustomEditField(Sections = "UI")]
	public AsyncReference m_heroClassIconsControllerReference;

	[CustomEditField(Sections = "UI")]
	public GameObject m_dungeonCrawlTray;

	[CustomEditField(Sections = "UI")]
	public DungeonCrawlBossKillCounter m_bossKillCounter;

	[CustomEditField(Sections = "UI")]
	public HighlightState m_backButtonHighlight;

	[CustomEditField(Sections = "UI")]
	public float m_RolloverTimeToHideBossHeroPowerTooltip = 0.35f;

	[CustomEditField(Sections = "UI")]
	public Material m_anomalyModeCardHighlightMaterial;

	[CustomEditField(Sections = "UI")]
	public float m_BigCardScale = 1f;

	[CustomEditField(Sections = "UI")]
	public AsyncReference m_retireButtonReference;

	[CustomEditField(Sections = "Animation")]
	public PlayMakerFSM m_HeroPowerPortraitPlayMaker;

	[CustomEditField(Sections = "Animation")]
	public string m_HeroPowerPotraitIntroStateName;

	[CustomEditField(Sections = "Bones")]
	public Transform m_socketHeroBone;

	[CustomEditField(Sections = "Bones")]
	public Transform m_heroPowerBone;

	[CustomEditField(Sections = "Bones")]
	public GameObject m_BossPowerBone;

	[CustomEditField(Sections = "Bones")]
	public GameObject m_HeroPowerBigCardBone;

	[CustomEditField(Sections = "Styles")]
	public List<DungeonCrawlDisplayStyleOverride> m_DungeonCrawlDisplayStyle;

	[CustomEditField(Sections = "Phone")]
	public UIBButton m_ShowDeckButton;

	[CustomEditField(Sections = "Phone")]
	public GameObject m_ShowDeckButtonFrame;

	[CustomEditField(Sections = "Phone")]
	public GameObject m_ShowDeckNoButtonFrame;

	[CustomEditField(Sections = "Phone")]
	public GameObject m_PhoneDeckTray;

	[CustomEditField(Sections = "Phone")]
	public GameObject m_DeckTrayRunCompleteBone;

	[CustomEditField(Sections = "Phone")]
	public GameObject m_DeckListHeaderRunCompleteBone;

	[CustomEditField(Sections = "Phone")]
	public TraySection m_DeckListHeaderPrefab;

	[CustomEditField(Sections = "Phone")]
	public GameObject m_TrayFrameDefault;

	[CustomEditField(Sections = "Phone")]
	public GameObject m_TrayFrameRunComplete;

	[CustomEditField(Sections = "Phone")]
	public GameObject m_AdventureTitleRunCompleteBone;

	[CustomEditField(Sections = "Phone")]
	public Vector3 m_DeckBigCardOffsetForRunCompleteState;

	[CustomEditField(Sections = "Phone")]
	public GameObject m_ViewDeckTrayMesh;

	public AdventureDungeonCrawlPlayMat m_playMat;

	public static bool s_shouldShowWelcomeBanner = true;

	private bool m_subsceneTransitionComplete;

	private CollectionDeck m_dungeonCrawlDeck;

	private DungeonCrawlDeckSelect m_dungeonCrawlDeckSelect;

	private Actor m_heroActor;

	private PlayerHeroData m_playerHeroData;

	private int m_numBossesDefeated;

	private List<long> m_defeatedBossIds;

	private long m_bossWhoDefeatedMeId;

	private long m_nextBossHealth;

	private string m_nextBossCardId;

	private long m_heroHealth;

	private bool m_isRunActive;

	private bool m_isRunRetired;

	private int m_selectedShrineIndex;

	private List<long> m_cardsAddedToDeckMap = new List<long>();

	private bool m_hasSeenLatestDungeonRunComplete;

	private List<long> m_shrineOptions;

	private long m_anomalyModeCardDbId;

	private long m_plotTwistCardDbId;

	private static GameSaveKeyId m_gameSaveDataServerKey;

	private static GameSaveKeyId m_gameSaveDataClientKey;

	private bool m_hasReceivedGameSaveDataServerKeyResponse;

	private bool m_hasReceivedGameSaveDataClientKeyResponse;

	private int m_numBossesInRun;

	private int m_bossCardBackId;

	private bool m_shouldSkipHeroSelect;

	private bool m_mustPickShrine;

	private bool m_mustSelectChapter;

	private Coroutine m_bossHeroPowerHideCoroutine;

	private IDungeonCrawlData m_dungeonCrawlData;

	private ISubsceneController m_subsceneController;

	private AssetLoadingHelper m_assetLoadingHelper;

	private Actor m_bossActor;

	private Actor m_bossPowerBigCard;

	private Actor m_heroPowerActor;

	private DefLoader.DisposableFullDef m_currentBossHeroPowerFullDef;

	private Actor m_heroPowerBigCard;

	private DefLoader.DisposableFullDef m_currentHeroPowerFullDef;

	private GameObject m_retireButton;

	private DungeonRunLoadoutState m_currentLoadoutState;

	private static AdventureDungeonCrawlDisplay m_instance;

	private bool m_isPVPDR;

	private CollectionDeck m_realDuelSeedDeck;

	private bool m_seedDeckCreateRequested;

	private long m_rewardNoticeId;

	public static AdventureDungeonCrawlDisplay Get()
	{
		return m_instance;
	}

	private void Awake()
	{
		m_instance = this;
	}

	private void Start()
	{
		CollectionManager.Get().RegisterDeckCreatedListener(OnDeckCreated);
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		m_isPVPDR = SceneMgr.Get().IsInDuelsMode();
	}

	public void StartRun(DungeonCrawlServices services)
	{
		m_dungeonCrawlData = services.DungeonCrawlData;
		m_subsceneController = services.SubsceneController;
		m_assetLoadingHelper = services.AssetLoadingHelper;
		services.AssetLoadingHelper.AssetLoadingComplete += OnSubSceneLoaded;
		m_subsceneController.TransitionComplete += OnSubSceneTransitionComplete;
		AdventureDbId selectedAdv = m_dungeonCrawlData.GetSelectedAdventure();
		AdventureModeDbId selectedMode = m_dungeonCrawlData.GetSelectedMode();
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdv, (int)selectedMode);
		m_playerHeroData = new PlayerHeroData(m_dungeonCrawlData);
		m_playerHeroData.OnHeroDataChanged += delegate
		{
			m_playMat.SetPlayerHeroDbId(m_playerHeroData.HeroDbId);
		};
		m_AdventureTitle.Text = adventureDataRecord.Name;
		m_gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		m_gameSaveDataClientKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey;
		if (m_gameSaveDataServerKey <= (GameSaveKeyId)0)
		{
			Debug.LogErrorFormat("Adventure {0} Mode {1} has no GameSaveDataKey set! This mode does not work without defining GAME_SAVE_DATA_SERVER_KEY in ADVENTURE.dbi!", selectedAdv, selectedMode);
		}
		if (m_gameSaveDataClientKey <= (GameSaveKeyId)0)
		{
			Debug.LogErrorFormat("Adventure {0} Mode {1} has no GameSaveDataKey set! This mode does not work without defining GAME_SAVE_DATA_CLIENT_KEY in ADVENTURE.dbi!", selectedAdv, selectedMode);
		}
		if (m_gameSaveDataClientKey == m_gameSaveDataServerKey)
		{
			Debug.LogErrorFormat("Adventure {0} Mode {1} has an equal GameSaveDataKey for Client and Server. These keys are not allowed to be equal!", selectedAdv, selectedMode);
		}
		m_bossCardBackId = adventureDataRecord.BossCardBack;
		if (m_bossCardBackId == 0)
		{
			m_bossCardBackId = 0;
		}
		List<ScenarioDbfRecord> records = GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == (int)selectedAdv && r.ModeId == (int)selectedMode);
		if (records == null || records.Count < 1)
		{
			Log.Adventures.PrintError("No Scenarios found for Adventure {0} and Mode {1}!", selectedAdv, selectedMode);
		}
		else if (records.Count == 1)
		{
			ScenarioDbfRecord scenarioDbfRecord = records[0];
			m_dungeonCrawlData.SetMission((ScenarioDbId)scenarioDbfRecord.ID, showDetails: false);
			Log.Adventures.Print("Owns wing for this Dungeon Run? {0}", AdventureProgressMgr.Get().OwnsWing(scenarioDbfRecord.WingId));
		}
		else if (m_dungeonCrawlData.GetMission() == ScenarioDbId.INVALID)
		{
			Log.Adventures.Print("No selectedScenarioId currently set - this should come with the GameSaveData.");
		}
		else
		{
			ScenarioDbfRecord scenarioDbfRecord2 = records.Find((ScenarioDbfRecord x) => x.ID == (int)m_dungeonCrawlData.GetMission());
			if (scenarioDbfRecord2 == null)
			{
				Log.Adventures.PrintError("No matching Scenario for this Adventure has been set in AdventureConfig! AdventureConfig's mission: {0}", m_dungeonCrawlData.GetMission());
			}
			else
			{
				Log.Adventures.Print("Owns wing for this Dungeon Run? {0}", AdventureProgressMgr.Get().OwnsWing(scenarioDbfRecord2.WingId));
			}
		}
		m_shouldSkipHeroSelect = adventureDataRecord.DungeonCrawlSkipHeroSelect;
		m_mustPickShrine = adventureDataRecord.DungeonCrawlMustPickShrine;
		m_mustSelectChapter = adventureDataRecord.DungeonCrawlSelectChapter;
		m_anomalyModeCardDbId = adventureDataRecord.AnomalyModeDefaultCardId;
		m_assetLoadingHelper.AddAssetToLoad();
		m_dungeonCrawlPlayMatReference.RegisterReadyListener<AdventureDungeonCrawlPlayMat>(OnPlayMatReady);
		bool retireButtonSupported = adventureDataRecord.DungeonCrawlIsRetireSupported;
		m_assetLoadingHelper.AddAssetToLoad();
		m_retireButtonReference.RegisterReadyListener(delegate(Widget w)
		{
			w.RegisterEventListener(delegate(string eventName)
			{
				if (eventName == "Button_Framed_Clicked" && retireButtonSupported)
				{
					m_retireButton.SetActive(value: false);
					AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo
					{
						m_headerText = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_RETIRE_CONFIRMATION_HEADER"),
						m_text = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_RETIRE_CONFIRMATION_BODY"),
						m_showAlertIcon = true,
						m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle,
						m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL
					};
					if (m_isPVPDR)
					{
						popupInfo.m_responseCallback = OnPVPDRRetirePopupResponse;
					}
					else
					{
						popupInfo.m_responseCallback = OnRetirePopupResponse;
					}
					DialogManager.Get().ShowPopup(popupInfo);
				}
			});
			m_retireButton = w.gameObject;
			m_retireButton.SetActive(value: false);
			w.RegisterDoneChangingStatesListener(delegate
			{
				m_assetLoadingHelper.AssetLoadCompleted();
			}, null, callImmediatelyIfSet: true, doOnce: true);
		});
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_dungeonCrawlDeckSelectWidgetReference.RegisterReadyListener<DungeonCrawlDeckSelect>(OnDungeonCrawlDeckTrayReady);
		}
		if (m_dungeonCrawlDeckTray != null && m_dungeonCrawlDeckTray.m_deckBigCard != null)
		{
			m_dungeonCrawlDeckTray.m_deckBigCard.OnBigCardShown += OnDeckTrayBigCardShown;
		}
		EnableBackButton(enabled: true);
		if (m_isPVPDR)
		{
			Navigation.PushUnique(PvPDungeonRunScene.Get().NavigateBackFromPlaymat);
		}
		else
		{
			Navigation.PushUnique(OnNavigateBack);
		}
		m_BackButton.AddEventListener(UIEventType.RELEASE, OnBackButtonPress);
		if (m_ShowDeckButton != null)
		{
			m_ShowDeckButton.AddEventListener(UIEventType.RELEASE, OnShowDeckButtonPress);
		}
		DisableBackButtonIfInDemoMode();
		RequestOrLoadCachedGameSaveData();
		SetDungeonCrawlDisplayVisualStyle();
	}

	public void EnablePlayButton()
	{
		if (m_playMat != null)
		{
			m_playMat.PlayButton.Enable();
		}
	}

	public void DisablePlayButton()
	{
		if (m_playMat != null && m_playMat.PlayButton.IsEnabled())
		{
			m_playMat.PlayButton.Disable();
		}
	}

	public void EnableBackButton(bool enabled)
	{
		if (m_BackButton != null && m_BackButton.IsEnabled() != enabled)
		{
			m_BackButton.SetEnabled(enabled);
			m_BackButton.Flip(enabled);
		}
	}

	private void OnDeckTrayBigCardShown(Actor shownActor, EntityDef entityDef)
	{
		if (shownActor == null || entityDef == null)
		{
			return;
		}
		long num = GameUtils.TranslateCardIdToDbId(entityDef.GetCardId());
		if (m_anomalyModeCardDbId == num)
		{
			HighlightRender componentInChildren = shownActor.GetComponentInChildren<HighlightRender>();
			MeshRenderer meshRenderer = ((componentInChildren != null) ? componentInChildren.GetComponent<MeshRenderer>() : null);
			if (meshRenderer != null && m_anomalyModeCardHighlightMaterial != null)
			{
				meshRenderer.SetSharedMaterial(m_anomalyModeCardHighlightMaterial);
				meshRenderer.enabled = true;
			}
		}
	}

	private void OnPlayMatPlayButtonReady(PlayButton playButton)
	{
		if (playButton == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButtonReference is null, or does not have a PlayButton component on it!");
			return;
		}
		playButton.AddEventListener(UIEventType.RELEASE, OnPlayButtonPress);
		Widget componentInParent = playButton.GetComponentInParent<WidgetTemplate>();
		if (componentInParent != null)
		{
			componentInParent.RegisterDoneChangingStatesListener(delegate
			{
				m_assetLoadingHelper.AssetLoadCompleted();
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}
		else
		{
			Error.AddDevWarning("UI Error!", "Could not find PlayMat PlayButton WidgetTemplate!");
			m_assetLoadingHelper.AssetLoadCompleted();
		}
	}

	private void OnDungeonCrawlDeckTrayReady(DungeonCrawlDeckSelect deckSelect)
	{
		m_dungeonCrawlDeckSelect = deckSelect;
		if (m_dungeonCrawlDeckSelect == null)
		{
			Error.AddDevWarning("UI Error!", "Could not find AdventureDungeonCrawlDeckTray in the AdventureDeckSelectWidget.");
			return;
		}
		if (m_dungeonCrawlDeckSelect == null)
		{
			Error.AddDevWarning("UI Error!", "Could not find SlidingTray in the AdventureDeckSelectWidget.");
			return;
		}
		deckSelect.playButton.AddEventListener(UIEventType.RELEASE, OnPlayButtonPress);
		deckSelect.heroDetails.AddHeroPowerListener(UIEventType.ROLLOVER, delegate
		{
			ShowBigCard(m_heroPowerBigCard, m_currentHeroPowerFullDef, m_HeroPowerBigCardBone);
		});
		deckSelect.heroDetails.AddHeroPowerListener(UIEventType.ROLLOUT, delegate
		{
			BigCardHelper.HideBigCard(m_heroPowerBigCard);
		});
		if (deckSelect.deckTray != null && deckSelect.deckTray.m_deckBigCard != null)
		{
			deckSelect.deckTray.m_deckBigCard.OnBigCardShown += OnDeckTrayBigCardShown;
		}
	}

	private void OnPlayMatReady(AdventureDungeonCrawlPlayMat playMat)
	{
		if (playMat == null)
		{
			Error.AddDevWarning("UI Error!", "m_dungeonCrawlPlayMatReference is null, or does not have a AdventureDungeonCrawlPlayMat component on it!");
			return;
		}
		m_playMat = playMat;
		m_playMat.SetCardBack(m_bossCardBackId);
		m_BossPowerBone = m_playMat.m_BossPowerBone;
		m_assetLoadingHelper.AddAssetToLoad();
		m_playMat.m_PlayButtonReference.RegisterReadyListener<PlayButton>(OnPlayMatPlayButtonReady);
		LoadInitialAssets();
		Widget component = playMat.GetComponent<WidgetTemplate>();
		if (component != null)
		{
			component.RegisterDoneChangingStatesListener(delegate
			{
				m_assetLoadingHelper.AssetLoadCompleted();
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}
		else
		{
			Error.AddDevWarning("UI Error!", "Could not find PlayMat WidgetTemplate!");
			m_assetLoadingHelper.AssetLoadCompleted();
		}
	}

	private void Update()
	{
		if (m_dungeonCrawlData != null && m_dungeonCrawlData.IsDevMode && InputCollection.GetKeyDown(KeyCode.Z) && !(m_playMat == null))
		{
			if (m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_BOSS_GRAVEYARD)
			{
				m_playMat.ShowNextBoss(GetPlayButtonTextForNextMission());
			}
			else if (m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_NEXT_BOSS)
			{
				ShowRunEnd(m_defeatedBossIds, m_bossWhoDefeatedMeId);
			}
		}
	}

	private void OnDestroy()
	{
		m_instance = null;
		m_currentBossHeroPowerFullDef?.Dispose();
		m_currentHeroPowerFullDef?.Dispose();
		if (m_playMat != null)
		{
			m_playMat.HideBossHeroPowerTooltip(immediate: true);
		}
		if (m_dungeonCrawlDeckTray != null && m_dungeonCrawlDeckTray.m_deckBigCard != null)
		{
			m_dungeonCrawlDeckTray.m_deckBigCard.OnBigCardShown -= OnDeckTrayBigCardShown;
		}
		if (m_dungeonCrawlDeckSelect != null && m_dungeonCrawlDeckSelect.deckTray != null && m_dungeonCrawlDeckSelect.deckTray.m_deckBigCard != null)
		{
			m_dungeonCrawlDeckSelect.deckTray.m_deckBigCard.OnBigCardShown -= OnDeckTrayBigCardShown;
		}
		GameMgr.Get()?.UnregisterFindGameEvent(OnFindGameEvent);
	}

	private void OnBossActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_bossActor = OnActorLoaded(assetRef, go, m_playMat.m_nextBossFaceBone.gameObject, withRotation: true);
		if (m_bossActor != null)
		{
			PegUIElement pegUIElement = m_bossActor.GetCollider().gameObject.AddComponent<PegUIElement>();
			pegUIElement.AddEventListener(UIEventType.ROLLOVER, delegate
			{
				m_bossActor.SetActorState(ActorStateType.CARD_MOUSE_OVER);
				ShowBigCard(m_bossPowerBigCard, m_currentBossHeroPowerFullDef, m_HeroPowerBigCardBone);
				m_bossHeroPowerHideCoroutine = StartCoroutine(HideBossHeroPowerTooltipAfterHover());
			});
			pegUIElement.AddEventListener(UIEventType.ROLLOUT, delegate
			{
				m_bossActor.SetActorState(ActorStateType.CARD_IDLE);
				BigCardHelper.HideBigCard(m_bossPowerBigCard);
				if (m_bossHeroPowerHideCoroutine != null)
				{
					StopCoroutine(m_bossHeroPowerHideCoroutine);
				}
			});
		}
		m_playMat.SetBossActor(m_bossActor);
		m_assetLoadingHelper.AssetLoadCompleted();
	}

	private void LoadInitialAssets()
	{
		AdventureDbId selectedAdventure = m_dungeonCrawlData.GetSelectedAdventure();
		AdventureModeDbId selectedMode = m_dungeonCrawlData.GetSelectedMode();
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode);
		if (adventureDataRecord == null)
		{
			Log.Adventures.PrintError("Tried to load assets but data record not found!");
			return;
		}
		IAssetLoader assetLoader = AssetLoader.Get();
		m_assetLoadingHelper.AddAssetToLoad();
		assetLoader.InstantiatePrefab(adventureDataRecord.DungeonCrawlBossCardPrefab, OnBossActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		m_assetLoadingHelper.AddAssetToLoad();
		assetLoader.InstantiatePrefab("History_HeroPower_Opponent.prefab:a99d23d6e8630f94b96a8e096fffb16f", OnBossPowerBigCardLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		m_assetLoadingHelper.AddAssetToLoad();
		assetLoader.InstantiatePrefab("Card_Dungeon_Play_Hero.prefab:183cb9cc59697844e911776ec349fe5e", OnHeroActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		m_assetLoadingHelper.AddAssetToLoad();
		assetLoader.InstantiatePrefab("History_HeroPower.prefab:e73edf8ccea2b11429093f7a448eef53", OnHeroPowerBigCardLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		m_assetLoadingHelper.AddAssetToLoad();
		assetLoader.InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", OnHeroPowerActorLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private IEnumerator HideBossHeroPowerTooltipAfterHover()
	{
		float timer = 0f;
		while (timer < m_RolloverTimeToHideBossHeroPowerTooltip)
		{
			timer += Time.unscaledDeltaTime;
			yield return new WaitForEndOfFrame();
		}
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS, out long value);
		if (value == 1)
		{
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS, 2L));
		}
		m_playMat.HideBossHeroPowerTooltip();
	}

	private void OnBossPowerBigCardLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_bossPowerBigCard = OnActorLoaded(assetRef, go, m_BossPowerBone);
		if (m_bossPowerBigCard != null)
		{
			m_bossPowerBigCard.TurnOffCollider();
		}
		m_assetLoadingHelper.AssetLoadCompleted();
	}

	private void OnHeroPowerBigCardLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_heroPowerBigCard = OnActorLoaded(assetRef, go, m_HeroPowerBigCardBone);
		if (m_heroPowerBigCard != null)
		{
			m_heroPowerBigCard.TurnOffCollider();
		}
		m_assetLoadingHelper.AssetLoadCompleted();
	}

	private void RequestOrLoadCachedGameSaveData()
	{
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY)
		{
			GameSaveDataManager.Get().ClearLocalData(m_gameSaveDataServerKey);
		}
		StartCoroutine(InitializeFromGameSaveDataWhenReady());
		if (!GameSaveDataManager.Get().IsDataReady(m_gameSaveDataServerKey))
		{
			GameSaveDataManager.Get().Request(m_gameSaveDataServerKey, OnRequestGameSaveDataServerResponse);
		}
		else
		{
			m_hasReceivedGameSaveDataServerKeyResponse = true;
		}
		if (!GameSaveDataManager.Get().IsDataReady(m_gameSaveDataClientKey))
		{
			GameSaveDataManager.Get().Request(m_gameSaveDataClientKey, OnRequestGameSaveDataClientResponse);
		}
		else
		{
			m_hasReceivedGameSaveDataClientKeyResponse = true;
		}
	}

	private void OnRequestGameSaveDataServerResponse(bool success)
	{
		if (!success)
		{
			Debug.LogError("OnRequestGameSaveDataResponse: Error requesting game save data for current adventure.");
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		}
		else
		{
			m_hasReceivedGameSaveDataServerKeyResponse = true;
		}
	}

	private void OnRequestGameSaveDataClientResponse(bool success)
	{
		if (!success)
		{
			Debug.LogError("OnRequestGameSaveDataResponse: Error requesting game save data for current adventure.");
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
		}
		else
		{
			m_hasReceivedGameSaveDataClientKeyResponse = true;
		}
	}

	private IEnumerator InitializeFromGameSaveDataWhenReady()
	{
		while (m_playMat == null || !m_playMat.IsReady())
		{
			Log.Adventures.Print("Waiting for Play Mat to be initialized before handling new Game Save Data.");
			yield return null;
		}
		while (m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			Log.Adventures.Print("Waiting for Play Mat to be done transitioning before handling new Game Save Data.");
			yield return null;
		}
		while (!m_hasReceivedGameSaveDataClientKeyResponse || !m_hasReceivedGameSaveDataServerKeyResponse)
		{
			yield return null;
		}
		DungeonCrawlUtil.MigrateDungeonCrawlSubkeys(m_gameSaveDataClientKey, m_gameSaveDataServerKey);
		while (m_heroActor == null || m_heroPowerActor == null || m_heroPowerBigCard == null)
		{
			yield return null;
		}
		InitializeFromGameSaveData();
	}

	private bool IsScenarioValidForAdventureAndMode(ScenarioDbId selectedScenario)
	{
		if (!AdventureUtils.IsMissionValidForAdventureMode(m_dungeonCrawlData.GetSelectedAdventure(), m_dungeonCrawlData.GetSelectedMode(), selectedScenario))
		{
			Debug.LogErrorFormat("Scenario {0} is not a part of Adventure {1} and mode {2}! Something is probably wrong.", selectedScenario, m_dungeonCrawlData.GetSelectedAdventure(), m_dungeonCrawlData.GetSelectedMode());
			return false;
		}
		return true;
	}

	private void InitializeFromGameSaveData()
	{
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)m_dungeonCrawlData.GetSelectedAdventure(), (int)m_dungeonCrawlData.GetSelectedMode());
		m_playerHeroData.DeckClass = TAG_CLASS.INVALID;
		List<long> values = null;
		List<CardWithPremiumStatus> deckCardListPremium = null;
		List<long> values2 = null;
		List<long> values3 = null;
		List<long> values4 = null;
		List<long> values5 = null;
		List<long> values6 = null;
		long value = 0L;
		long value2 = 0L;
		if (GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, out m_defeatedBossIds))
		{
			m_numBossesDefeated = m_defeatedBossIds.Count;
		}
		List<long> values7 = null;
		List<long> values8 = null;
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, out values);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CLASS, out long value3);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_LOST_TO, out m_bossWhoDefeatedMeId);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_A, out values3);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_B, out values4);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_C, out values5);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_OPTION, out values2);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SHRINE_OPTIONS, out m_shrineOptions);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_FIGHT, out values6);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT, out value);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_TREASURE, out value2);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_HEALTH, out m_nextBossHealth);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_HEALTH, out long value4);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_CARDS_ADDED_TO_DECK_MAP, out m_cardsAddedToDeckMap);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_SHRINE, out long value5);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_SCENARIO_ID, out long value6);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, out long value7);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_OVERRIDE, out long value8);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, out long value9);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, out long value10);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER, out long value11);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_ANOMALY_MODE, out long value12);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_ANOMALY_MODE, out long value13);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_DECK, out long value14);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_ENCHANTMENT_INDICES, out values7);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_ENCHANTMENTS, out values8);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_CURRENT_ANOMALY_MODE_CARD, out long value15);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_ANOMALY_MODE_CARD_PREVIEW, out long value16);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, out long value17);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, out long value18);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, out long value19);
		m_playerHeroData.DeckClass = (TAG_CLASS)value3;
		m_selectedShrineIndex = (int)value5;
		if (values != null)
		{
			deckCardListPremium = CardWithPremiumStatus.ConvertList(values);
		}
		m_isRunRetired = value19 > 0;
		m_isRunActive = DungeonCrawlUtil.IsDungeonRunActive(m_gameSaveDataServerKey);
		m_hasSeenLatestDungeonRunComplete = value17 > 0;
		bool flag = m_isRunActive || ShouldShowRunCompletedScreen();
		if (m_isPVPDR)
		{
			CollectionDeck duelsDeck = CollectionManager.Get().GetDuelsDeck();
			if (duelsDeck != null)
			{
				if (m_isRunActive || (values != null && values.Count > duelsDeck.GetTotalCardCount()))
				{
					List<CardWithPremiumStatus> cardsWithPremiumStatus = duelsDeck.GetCardsWithPremiumStatus();
					int i;
					for (i = 0; i < deckCardListPremium.Count; i++)
					{
						int num = cardsWithPremiumStatus.FindIndex((CardWithPremiumStatus r) => r.cardId == deckCardListPremium[i].cardId);
						if (num >= 0)
						{
							deckCardListPremium[i].premium = cardsWithPremiumStatus[num].premium;
							cardsWithPremiumStatus.RemoveAt(num);
						}
					}
				}
				else if (!m_seedDeckCreateRequested)
				{
					m_realDuelSeedDeck = duelsDeck;
					deckCardListPremium = duelsDeck.GetCardsWithPremiumStatus();
					TAG_CLASS tAG_CLASS2 = (m_dungeonCrawlData.SelectedHeroClass = (m_playerHeroData.DeckClass = duelsDeck.GetClass()));
				}
			}
		}
		m_dungeonCrawlData.SelectedLoadoutTreasureDbId = value9;
		m_dungeonCrawlData.SelectedHeroPowerDbId = value10;
		m_dungeonCrawlData.SelectedDeckId = value14;
		if ((int)value18 != 0)
		{
			m_dungeonCrawlData.SelectedHeroClass = (TAG_CLASS)value18;
		}
		ScenarioDbId scenarioDbId = (ScenarioDbId)value8;
		if (scenarioDbId != 0 && !IsScenarioValidForAdventureAndMode(scenarioDbId))
		{
			scenarioDbId = ScenarioDbId.INVALID;
		}
		Log.Adventures.Print("Scenario Override set to {0}!", scenarioDbId);
		m_dungeonCrawlData.SetMissionOverride(scenarioDbId);
		ScenarioDbId scenarioDbId2 = (ScenarioDbId)(flag ? value7 : value6);
		if (scenarioDbId2 != 0 && IsScenarioValidForAdventureAndMode(scenarioDbId2))
		{
			m_dungeonCrawlData.SetMission(scenarioDbId2);
		}
		bool flag2 = false;
		if (!flag)
		{
			flag2 = m_dungeonCrawlData.HasValidLoadoutForSelectedAdventure();
			if (!flag2)
			{
				ResetDungeonCrawlSelections(m_dungeonCrawlData);
			}
		}
		m_playMat.m_paperControllerReference.RegisterReadyListener<VisualController>(OnPlayMatPaperControllerReady);
		m_playMat.m_paperControllerReference_phone.RegisterReadyListener<VisualController>(OnPlayMatPaperControllerReady);
		if (flag)
		{
			m_dungeonCrawlData.AnomalyModeActivated = value13 > 0;
		}
		else if (flag2)
		{
			m_dungeonCrawlData.AnomalyModeActivated = value12 > 0;
		}
		if (flag)
		{
			m_heroHealth = value4;
		}
		else
		{
			m_heroHealth = 0L;
		}
		if (HandleDemoModeReset())
		{
			return;
		}
		long num2 = (flag ? value15 : value16);
		if (num2 > 0)
		{
			m_anomalyModeCardDbId = num2;
		}
		if (m_isRunActive && deckCardListPremium != null)
		{
			if (value != 0L)
			{
				List<long>[] array = new List<long>[3] { values3, values4, values5 };
				int num3 = (int)value - 1;
				if (num3 >= array.Length || array[num3] == null)
				{
					Log.Adventures.PrintError("Attempting to add Loot choice {0} to the deck list, but there is not corresponding list of Loot!", num3);
				}
				else
				{
					List<long> list = array[num3];
					for (int j = 1; j < list.Count; j++)
					{
						deckCardListPremium.Add(new CardWithPremiumStatus(list[j], TAG_PREMIUM.NORMAL));
					}
				}
			}
			if (value2 != 0L && values2 != null)
			{
				int num4 = (int)value2 - 1;
				if (values2.Count <= num4)
				{
					Log.Adventures.PrintError("Attempting to add Treasure choice {0} to the deck list, but treasureLootOptions only has {1} options!", num4, values2.Count);
				}
				else
				{
					deckCardListPremium.Add(new CardWithPremiumStatus(values2[num4], TAG_PREMIUM.NORMAL));
				}
			}
		}
		ScenarioDbId mission = m_dungeonCrawlData.GetMission();
		int num5 = 0;
		WingDbfRecord wingRecordFromMissionId = GameUtils.GetWingRecordFromMissionId((int)mission);
		m_numBossesInRun = m_dungeonCrawlData.GetAdventureBossesInRun(wingRecordFromMissionId);
		if (wingRecordFromMissionId != null)
		{
			num5 = Mathf.Max(0, GameUtils.GetSortedWingUnlockIndex(wingRecordFromMissionId));
			m_plotTwistCardDbId = wingRecordFromMissionId.PlotTwistCardId;
		}
		int num6 = 0;
		if (values6 != null && values6.Count > num5 && !m_isRunRetired)
		{
			num6 = (int)values6[num5];
		}
		if (num6 != 0)
		{
			m_nextBossCardId = GameUtils.TranslateDbIdToCardId(num6);
		}
		else
		{
			m_nextBossCardId = GameUtils.GetMissionHeroCardId((int)mission);
		}
		if (m_nextBossCardId == null)
		{
			Log.Adventures.PrintWarning("AdventureDungeonCrawlDisplay.OnGameSaveDataResponse() - No cardId for boss dbId {0}!", num6);
		}
		else
		{
			m_assetLoadingHelper.AddAssetToLoad();
			DefLoader.Get().LoadFullDef(m_nextBossCardId, OnBossFullDefLoaded);
		}
		long num7 = (flag ? value11 : m_dungeonCrawlData.SelectedHeroPowerDbId);
		if (num7 != 0L)
		{
			SetHeroPower(GameUtils.TranslateDbIdToCardId((int)num7));
		}
		if (m_isRunActive || ShouldShowRunCompletedScreen())
		{
			s_shouldShowWelcomeBanner = false;
		}
		InitializePlayMat();
		if (ShouldShowRunCompletedScreen())
		{
			if (m_isPVPDR)
			{
				ShowDuelsEndRun();
			}
			else
			{
				ShowRunEnd(m_defeatedBossIds, m_bossWhoDefeatedMeId);
			}
			SetUpBossKillCounter(m_playerHeroData.DeckClass);
			SetUpDeckList(deckCardListPremium, flag, playGlowAnimation: false, values7, values8);
			SetUpHeroPortrait(m_playerHeroData);
			if (!SceneMgr.Get().IsInDuelsMode())
			{
				SetUpPhoneRunCompleteScreen();
			}
		}
		else if (!m_isRunActive)
		{
			if (!m_dungeonCrawlData.HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
			{
				TryShowWelcomeBanner();
			}
			bool flag3 = !m_isPVPDR;
			if (m_mustPickShrine)
			{
				if (m_shrineOptions == null && m_dungeonCrawlData.GetSelectedAdventure() == AdventureDbId.TRL)
				{
					m_shrineOptions = GetDefaultStartingShrineOptions_TRL();
				}
				if (m_shrineOptions != null && m_selectedShrineIndex == 0)
				{
					SetPlaymatStateForShrineSelection(m_shrineOptions);
					SetUpHeroPortrait(m_playerHeroData);
					flag3 = false;
				}
				else if (m_shrineOptions != null)
				{
					long shrineCardId = m_shrineOptions[m_selectedShrineIndex - 1];
					SetUpDeckListFromShrine(shrineCardId, playDeckGlowAnimation: false);
					TAG_CLASS classFromShrine = GetClassFromShrine(shrineCardId);
					m_playerHeroData.DeckClass = classFromShrine;
					SetUpHeroPortrait(m_playerHeroData);
					if (m_dungeonCrawlData.SelectedHeroClass == TAG_CLASS.INVALID)
					{
						m_dungeonCrawlData.SelectedHeroClass = classFromShrine;
					}
				}
				SetUpBossKillCounter(m_dungeonCrawlData.SelectedHeroClass);
			}
			else
			{
				if (m_dungeonCrawlData.HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure() && m_dungeonCrawlData.SelectedHeroClass != 0)
				{
					m_playerHeroData.DeckClass = m_dungeonCrawlData.SelectedHeroClass;
					SetUpHeroPortrait(m_playerHeroData);
					SetUpBossKillCounter(m_playerHeroData.DeckClass);
				}
				bool num8 = m_dungeonCrawlData.SelectableLoadoutTreasuresExist();
				bool flag4 = m_dungeonCrawlData.SelectableHeroPowersAndDecksExist();
				if (num8 || flag4)
				{
					if (!m_dungeonCrawlData.HasValidLoadoutForSelectedAdventure())
					{
						m_currentLoadoutState = DungeonRunLoadoutState.INVALID;
						GoToNextLoadoutState();
						flag3 = false;
						if (m_plotTwistCardDbId != 0L || (m_anomalyModeCardDbId != 0L && m_dungeonCrawlData.AnomalyModeActivated))
						{
							SetUpDeckList(null, flag);
						}
					}
					else if (m_isPVPDR)
					{
						SetUpDeckList(deckCardListPremium, flag);
						m_playMat.ShowPVPDRActiveRun(GetPlayButtonTextForNextMission());
						m_BackButton.SetText("GLOBAL_LEAVE");
					}
					else if ((m_dungeonCrawlDeck == null || m_dungeonCrawlDeck.GetTotalCardCount() <= 0) && m_dungeonCrawlData.SelectedDeckId != 0L && m_playerHeroData.DeckClass != 0)
					{
						string deckName;
						string deckDescription;
						List<long> cards = CollectionManager.Get().LoadDeckFromDBF((int)m_dungeonCrawlData.SelectedDeckId, out deckName, out deckDescription);
						SetUpDeckList(CardWithPremiumStatus.ConvertList(cards), flag);
					}
				}
				else if (adventureDataRecord.DungeonCrawlDefaultToDeckFromUpcomingScenario)
				{
					SetUpDeckListFromScenario(m_dungeonCrawlData.GetMission(), flag);
				}
			}
			if (flag3)
			{
				m_playMat.SetUpDefeatedBosses(null, m_numBossesInRun);
				m_playMat.SetShouldShowBossHeroPowerTooltip(ShouldShowBossHeroPowerTutorial());
				m_assetLoadingHelper.AddAssetToLoad();
				m_playMat.SetUpCardBacks(m_numBossesInRun - 1, m_assetLoadingHelper.AssetLoadCompleted);
				string playButtonText = "GLUE_CHOOSE";
				if (m_shouldSkipHeroSelect || m_dungeonCrawlData.HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
				{
					playButtonText = GetPlayButtonTextForNextMission();
				}
				m_playMat.ShowNextBoss(playButtonText);
				if (m_mustSelectChapter)
				{
					m_BackButton.SetText("GLOBAL_LEAVE");
				}
			}
			SetUpPhoneNewRunScreen();
		}
		else
		{
			SetUpBossKillCounter(m_playerHeroData.DeckClass);
			if (adventureDataRecord.DungeonCrawlDefaultToDeckFromUpcomingScenario && (deckCardListPremium == null || deckCardListPremium.Count == 0))
			{
				if ((values7 != null && values7.Count > 0) || (values8 != null && values8.Count > 0))
				{
					Debug.LogWarning("AdventureDungeonCrawlDisplay.InitializeFromGameSaveData() - Setting the deck list using the deck from upcoming scenario, but you have deck card enchantments! Something is probably wrong. Enchantments being ignored.");
				}
				SetUpDeckListFromScenario(m_dungeonCrawlData.GetMission(), flag);
			}
			else
			{
				SetUpDeckList(deckCardListPremium, flag, playGlowAnimation: false, values7, values8);
			}
			SetUpHeroPortrait(m_playerHeroData);
			m_playMat.SetUpDefeatedBosses(m_defeatedBossIds, m_numBossesInRun);
			m_playMat.SetShouldShowBossHeroPowerTooltip(ShouldShowBossHeroPowerTutorial());
			m_assetLoadingHelper.AddAssetToLoad();
			int num9 = ((m_defeatedBossIds != null) ? m_defeatedBossIds.Count : 0);
			m_playMat.SetUpCardBacks(m_numBossesInRun - num9 - 1, m_assetLoadingHelper.AssetLoadCompleted);
			SetPlayMatStateFromGameSaveData();
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_dungeonCrawlDeckTray.gameObject.SetActive(value: false);
			}
			m_retireButton.SetActive(adventureDataRecord.DungeonCrawlIsRetireSupported);
		}
		m_assetLoadingHelper.AssetLoadCompleted();
	}

	private void OnPlayMatPaperControllerReady(VisualController paperController)
	{
		if (paperController == null)
		{
			Debug.LogError("paperController was null in OnPlayMatPaperControllerReady!");
			m_assetLoadingHelper.AssetLoadCompleted();
		}
		else
		{
			paperController.RegisterDoneChangingStatesListener(delegate
			{
				m_assetLoadingHelper.AssetLoadCompleted();
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}
	}

	private void InitializePlayMat()
	{
		m_assetLoadingHelper.AddAssetToLoad();
		m_playMat.Initialize(m_dungeonCrawlData);
		Widget component = m_playMat.GetComponent<WidgetTemplate>();
		if (component != null)
		{
			component.RegisterDoneChangingStatesListener(delegate
			{
				m_assetLoadingHelper.AssetLoadCompleted();
			}, null, callImmediatelyIfSet: true, doOnce: true);
		}
		else
		{
			Error.AddDevWarning("UI Error!", "Could not find PlayMat WidgetTemplate!");
			m_assetLoadingHelper.AssetLoadCompleted();
		}
	}

	private IEnumerator SetPlayMatStateFromGameSaveDataWhenReady()
	{
		while (GameSaveDataManager.Get().IsRequestPending(m_gameSaveDataServerKey) || GameSaveDataManager.Get().IsRequestPending(m_gameSaveDataClientKey) || m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			yield return null;
		}
		SetPlayMatStateFromGameSaveData();
	}

	private string GetPlayButtonTextForNextMission()
	{
		string value = "";
		if (GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.PLAY_BUTTON_TEXT_OVERRIDE, out value) && !string.IsNullOrEmpty(value))
		{
			return value;
		}
		if (m_isPVPDR && !m_isRunActive && (m_realDuelSeedDeck == null || !m_realDuelSeedDeck.IsValidForRuleset))
		{
			return "GLUE_PVPDR_BUILD_DECK";
		}
		return "GLOBAL_PLAY";
	}

	private bool IsNextMissionASpecialEncounter()
	{
		if (!m_hasReceivedGameSaveDataServerKeyResponse)
		{
			Debug.LogError("GetPlayButtonTextForNextMission() - this cannot be called before we've gotten the Game Save Data Server Key response!");
			return false;
		}
		return m_dungeonCrawlData.GetMissionOverride() != ScenarioDbId.INVALID;
	}

	private void SetPlayMatStateFromGameSaveData()
	{
		List<long> values = null;
		List<long> values2 = null;
		List<long> values3 = null;
		List<long> values4 = null;
		long value = 0L;
		long value2 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_A, out values2);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_B, out values3);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_C, out values4);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_OPTION, out values);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT, out value);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_TREASURE, out value2);
		bool flag = DungeonCrawlUtil.IsDungeonRunActive(m_gameSaveDataServerKey);
		m_playMat.IsNextMissionASpecialEncounter = IsNextMissionASpecialEncounter();
		if (m_backButtonHighlight != null)
		{
			m_backButtonHighlight.ChangeState(ActorStateType.NONE);
		}
		if (m_isPVPDR)
		{
			if (Cheats.Get().HasCheatTreasureIds() && values != null && values.Count > 0)
			{
				Cheats.Get().SaveDuelsCheatTreasures(out var addedTreasures);
				Cheats.Get().ClearCheatTreasures();
				int num = Mathf.Min(addedTreasures.Count, values.Count);
				for (int i = 0; i < num; i++)
				{
					values[i] = addedTreasures[i];
				}
			}
			if (Cheats.Get().HasCheatLootIds() && value == 0L && ((values2 != null && values2.Count > 0) || (values3 != null && values3.Count > 0) || (values4 != null && values4.Count > 0)))
			{
				Cheats.Get().SaveDuelsCheatLoot(out var addedLootA, out var addedLootB, out var addedLootC);
				Cheats.Get().ClearCheatLoot();
				for (int j = 0; j < addedLootA.Count; j++)
				{
					values2[j + 1] = addedLootA[j];
				}
				for (int k = 0; k < addedLootB.Count; k++)
				{
					values3[k + 1] = addedLootB[k];
				}
				for (int l = 0; l < addedLootC.Count; l++)
				{
					values4[l + 1] = addedLootC[l];
				}
			}
		}
		if (flag && value2 == 0L && values != null && values.Count > 0)
		{
			m_playMat.ShowTreasureOptions(values);
			return;
		}
		if (flag && value == 0L && ((values2 != null && values2.Count > 0) || (values3 != null && values3.Count > 0) || (values4 != null && values4.Count > 0)))
		{
			m_playMat.ShowLootOptions(values2, values3, values4);
			return;
		}
		if (!flag)
		{
			m_playMat.SetUpDefeatedBosses(null, m_numBossesInRun);
			m_playMat.SetShouldShowBossHeroPowerTooltip(ShouldShowBossHeroPowerTutorial());
			m_playMat.SetUpCardBacks(m_numBossesInRun - 1, null);
		}
		if (m_isPVPDR)
		{
			m_playMat.ShowPVPDRActiveRun(GetPlayButtonTextForNextMission());
		}
		else
		{
			m_playMat.ShowNextBoss(GetPlayButtonTextForNextMission());
		}
	}

	private void SetPlaymatStateForShrineSelection(List<long> shrineOptions)
	{
		if (shrineOptions == null || shrineOptions.Count == 0)
		{
			Log.Adventures.PrintError("SetPlaymatStateForShrineSelection: No shrine options found for adventure.");
			return;
		}
		SetShowDeckButtonEnabled(enabled: false);
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_DECK_SELECTION_TUTORIAL, out long value);
		if (value == 0L)
		{
			m_playMat.ShowEmptyState();
			StartCoroutine(ShowDeckSelectionTutorialPopupWhenReady(delegate
			{
				m_playMat.ShowShrineOptions(shrineOptions);
			}));
		}
		else
		{
			m_playMat.ShowShrineOptions(shrineOptions);
		}
	}

	private List<long> GetDefaultStartingShrineOptions_TRL()
	{
		return new List<long> { 52891L, 51920L, 53036L };
	}

	private IEnumerator ShowDeckSelectionTutorialPopupWhenReady(Action popupDismissedCallback)
	{
		while (!m_subsceneTransitionComplete)
		{
			yield return new WaitForEndOfFrame();
		}
		while (s_shouldShowWelcomeBanner)
		{
			yield return new WaitForEndOfFrame();
		}
		AdventureDef adventureDef = m_dungeonCrawlData.GetAdventureDef();
		if (adventureDef != null && !string.IsNullOrEmpty(adventureDef.m_AdventureDeckSelectionTutorialBannerPrefab))
		{
			BannerManager.Get().ShowBanner(adventureDef.m_AdventureDeckSelectionTutorialBannerPrefab, null, null, delegate
			{
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_DECK_SELECTION_TUTORIAL, 1L));
				popupDismissedCallback();
			});
		}
		else
		{
			popupDismissedCallback();
		}
	}

	private bool HandleDemoModeReset()
	{
		if (IsInDemoMode() && (m_numBossesDefeated >= 3 || m_bossWhoDefeatedMeId != 0L))
		{
			m_isRunActive = false;
			m_defeatedBossIds = null;
			m_bossWhoDefeatedMeId = 0L;
			m_numBossesDefeated = 0;
			StartCoroutine(ShowDemoThankQuote());
			s_shouldShowWelcomeBanner = false;
			return true;
		}
		return false;
	}

	private void TryShowWelcomeBanner()
	{
		if (!s_shouldShowWelcomeBanner)
		{
			return;
		}
		AdventureDef adventureDef = m_dungeonCrawlData.GetAdventureDef();
		if (adventureDef != null && !string.IsNullOrEmpty(adventureDef.m_AdventureIntroBannerPrefab))
		{
			BannerManager.Get().ShowBanner(adventureDef.m_AdventureIntroBannerPrefab, null, GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_INTRO_BANNER_BUTTON"), delegate
			{
				s_shouldShowWelcomeBanner = false;
			});
			WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(m_dungeonCrawlData.GetMission());
			DungeonCrawlSubDef_VOLines.PlayVOLine(m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, m_playerHeroData.HeroDbId, DungeonCrawlSubDef_VOLines.VOEventType.WELCOME_BANNER);
		}
		else
		{
			s_shouldShowWelcomeBanner = false;
		}
	}

	private bool ShouldShowBossHeroPowerTutorial()
	{
		GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS, out long value);
		if (value == 0L)
		{
			if (m_numBossesDefeated >= 2)
			{
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataClientKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS, 1L));
				return true;
			}
			return false;
		}
		return value == 1;
	}

	private void ShowRunEnd(List<long> defeatedBossIds, long bossWhoDefeatedMeId)
	{
		m_BackButton.Flip(faceUp: false, forceImmediate: true);
		m_BackButton.SetEnabled(enabled: false);
		m_assetLoadingHelper.AddAssetToLoad();
		m_playMat.ShowRunEnd(defeatedBossIds, bossWhoDefeatedMeId, m_numBossesInRun, HasCompletedAdventureWithAllClasses(), GetRunWinsForClass(m_playerHeroData.DeckClass) == 1, GetNumberOfClassesThatHaveCompletedAdventure(), m_gameSaveDataServerKey, m_gameSaveDataClientKey, m_assetLoadingHelper.AssetLoadCompleted, RunEndCompleted);
	}

	private int GetNumberOfClassesThatHaveCompletedAdventure()
	{
		int num = 0;
		foreach (TAG_CLASS item in GameSaveDataManager.GetClassesFromDungeonCrawlProgressMap())
		{
			if (GetRunWinsForClass(item) > 0)
			{
				num++;
			}
		}
		return num;
	}

	private bool HasCompletedAdventureWithAllClasses()
	{
		List<int> guestHeroesForCurrentAdventure = m_dungeonCrawlData.GetGuestHeroesForCurrentAdventure();
		if (guestHeroesForCurrentAdventure.Count > 0)
		{
			foreach (int item in guestHeroesForCurrentAdventure)
			{
				TAG_CLASS tagClassFromCardDbId = GameUtils.GetTagClassFromCardDbId(item);
				if (GameSaveDataManager.GetClassesFromDungeonCrawlProgressMap().Contains(tagClassFromCardDbId) && !HasCompletedAdventureWithClass(tagClassFromCardDbId))
				{
					return false;
				}
			}
		}
		else
		{
			foreach (TAG_CLASS item2 in GameSaveDataManager.GetClassesFromDungeonCrawlProgressMap())
			{
				if (!HasCompletedAdventureWithClass(item2))
				{
					return false;
				}
			}
		}
		return true;
	}

	private bool HasCompletedAdventureWithClass(TAG_CLASS tagClass)
	{
		return GetRunWinsForClass(tagClass) > 0;
	}

	private void RunEndCompleted()
	{
		if (!(m_BackButton == null))
		{
			m_dungeonCrawlData.SelectedHeroClass = TAG_CLASS.INVALID;
			m_BackButton.Flip(faceUp: true);
			m_BackButton.SetEnabled(enabled: true);
			if (m_backButtonHighlight != null)
			{
				m_backButtonHighlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			}
		}
	}

	private void SetUpBossKillCounter(TAG_CLASS deckClass)
	{
		bool shouldSkipHeroSelect = m_shouldSkipHeroSelect;
		long value = 0L;
		long value2 = 0L;
		m_bossKillCounter.SetDungeonRunData(m_dungeonCrawlData);
		if (deckClass != 0 && !shouldSkipHeroSelect)
		{
			m_bossKillCounter.SetHeroClass(deckClass);
			value = GetBossWinsForClass(deckClass);
			value2 = GetRunWinsForClass(deckClass);
		}
		else if (shouldSkipHeroSelect)
		{
			GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_ALL_CLASSES_TOTAL_BOSS_WINS, out value);
			GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_ALL_CLASSES_TOTAL_RUN_WINS, out value2);
		}
		m_bossKillCounter.SetBossWins(value);
		if (value2 > 0)
		{
			m_bossKillCounter.SetRunWins(value2);
		}
		m_bossKillCounter.UpdateLayout();
	}

	private long GetRunWinsForClass(TAG_CLASS tagClass)
	{
		long value = 0L;
		if (GameSaveDataManager.GetProgressSubkeyForDungeonCrawlClass(tagClass, out var progressSubkeys))
		{
			GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, progressSubkeys.runWins, out value);
		}
		return value;
	}

	public bool IsCardLoadoutTreasure(string cardID)
	{
		if (m_dungeonCrawlData != null)
		{
			int num = GameUtils.TranslateCardIdToDbId(cardID);
			foreach (AdventureLoadoutTreasuresDbfRecord item in m_dungeonCrawlData.GetLoadoutTreasuresForClass(m_dungeonCrawlData.SelectedHeroClass))
			{
				if (item.CardId == num)
				{
					return true;
				}
			}
		}
		return false;
	}

	private long GetBossWinsForClass(TAG_CLASS tagClass)
	{
		long value = 0L;
		if (GameSaveDataManager.GetProgressSubkeyForDungeonCrawlClass(tagClass, out var progressSubkeys))
		{
			GameSaveDataManager.Get().GetSubkeyValue(m_gameSaveDataServerKey, progressSubkeys.bossWins, out value);
		}
		return value;
	}

	private void SetUpDeckListFromShrine(long shrineCardId, bool playDeckGlowAnimation)
	{
		List<long> list = new List<long>();
		CardTagDbfRecord shrineDeckCardTagRecord = GameDbf.CardTag.GetRecord((CardTagDbfRecord r) => r.CardId == (int)shrineCardId && r.TagId == 1099);
		foreach (DeckCardDbfRecord record in GameDbf.DeckCard.GetRecords((DeckCardDbfRecord r) => r.DeckId == shrineDeckCardTagRecord.TagValue))
		{
			list.Add(record.CardId);
		}
		list.Add(shrineCardId);
		SetUpDeckList(CardWithPremiumStatus.ConvertList(list), useLoadoutOfActiveRun: false, playDeckGlowAnimation);
		SetShowDeckButtonEnabled(enabled: true);
	}

	private void SetUpDeckListFromScenario(ScenarioDbId scenario, bool useLoadoutOfActiveRun)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)scenario);
		if (record != null)
		{
			string deckName;
			string deckDescription;
			List<long> cards = CollectionManager.Get().LoadDeckFromDBF(record.Player1DeckId, out deckName, out deckDescription);
			SetUpDeckList(CardWithPremiumStatus.ConvertList(cards), useLoadoutOfActiveRun);
		}
	}

	private void SetUpDeckList(List<CardWithPremiumStatus> deckCardList, bool useLoadoutOfActiveRun, bool playGlowAnimation = false, List<long> deckCardIndices = null, List<long> deckCardEnchantments = null)
	{
		if (m_playerHeroData.DeckClass == TAG_CLASS.INVALID)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDisplay.SetUpDeckList() - deckClass is INVALID!");
		}
		else
		{
			if (string.IsNullOrEmpty(m_playerHeroData.HeroCardId))
			{
				return;
			}
			m_dungeonCrawlDeck = new CollectionDeck
			{
				HeroCardID = m_playerHeroData.HeroCardId
			};
			m_dungeonCrawlDeck.FormatType = FormatType.FT_WILD;
			m_dungeonCrawlDeck.Type = DeckType.CLIENT_ONLY_DECK;
			if (m_isPVPDR)
			{
				m_dungeonCrawlDeck.Name = GameStrings.Get("GLUE_COLLECTION_DUEL_DECKNAME");
				m_dungeonCrawlDeck.HeroPowerCardID = GameUtils.TranslateDbIdToCardId((int)m_dungeonCrawlData.SelectedHeroPowerDbId);
			}
			if (m_isPVPDR && !m_isRunActive)
			{
				m_dungeonCrawlDeck.Type = DeckType.PVPDR_DISPLAY_DECK;
				if (deckCardList != null && deckCardList.Count > m_dungeonCrawlDeck.GetMaxCardCount())
				{
					m_dungeonCrawlDeck.Type = DeckType.CLIENT_ONLY_DECK;
				}
			}
			if (m_anomalyModeCardDbId != 0L && m_dungeonCrawlData.AnomalyModeActivated)
			{
				string text = GameUtils.TranslateDbIdToCardId((int)m_anomalyModeCardDbId);
				if (text != null)
				{
					m_dungeonCrawlDeck.AddCard(text, TAG_PREMIUM.NORMAL);
				}
				else
				{
					Log.Adventures.PrintWarning("AdventureDungeonCrawlDisplay.SetUpDeckList() - No cardId for anomalyCardDbId {0}!", m_anomalyModeCardDbId);
				}
			}
			if (m_plotTwistCardDbId != 0L)
			{
				string text2 = GameUtils.TranslateDbIdToCardId((int)m_plotTwistCardDbId);
				if (text2 != null)
				{
					m_dungeonCrawlDeck.AddCard(text2, TAG_PREMIUM.NORMAL);
				}
				else
				{
					Log.Adventures.PrintWarning("AdventureDungeonCrawlDisplay.SetUpDeckList() - No cardId for m_plotTwistCardDbId {0}!", m_plotTwistCardDbId);
				}
			}
			if (!useLoadoutOfActiveRun && m_dungeonCrawlData.SelectedLoadoutTreasureDbId != 0L)
			{
				string text3 = GameUtils.TranslateDbIdToCardId((int)m_dungeonCrawlData.SelectedLoadoutTreasureDbId);
				if (!string.IsNullOrEmpty(text3))
				{
					CollectionDeckSlot collectionDeckSlot = m_dungeonCrawlDeck.FindFirstSlotByCardId(text3);
					if (collectionDeckSlot == null || collectionDeckSlot.Count == 0)
					{
						m_dungeonCrawlDeck.AddCard(text3, TAG_PREMIUM.NORMAL);
					}
				}
				else
				{
					Log.Adventures.PrintWarning("AdventureDungeonCrawlDisplay.SetUpDeckList() - No cardId for SelectedLoadoutTreasureDbId {0}!", m_dungeonCrawlData.SelectedLoadoutTreasureDbId);
				}
			}
			if (deckCardList != null)
			{
				Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
				if (deckCardIndices != null && deckCardEnchantments != null && deckCardIndices.Count == deckCardEnchantments.Count)
				{
					for (int i = 0; i < deckCardIndices.Count; i++)
					{
						if (!dictionary.TryGetValue((int)deckCardIndices[i], out var value))
						{
							value = new List<int>();
							dictionary.Add((int)deckCardIndices[i], value);
						}
						value.Add((int)deckCardEnchantments[i]);
					}
				}
				for (int j = 0; j < deckCardList.Count; j++)
				{
					long cardId = deckCardList[j].cardId;
					TAG_PREMIUM premium = deckCardList[j].premium;
					if (cardId != 0L)
					{
						string text4 = GameUtils.TranslateDbIdToCardId((int)cardId);
						List<int> value2;
						if (text4 == null)
						{
							Log.Adventures.PrintWarning("AdventureDungeonCrawlDisplay.SetUpDeckList() - No cardId for dbId {0}!", cardId);
						}
						else if (dictionary.TryGetValue(j + 1, out value2))
						{
							m_dungeonCrawlDeck.AddCard_DungeonCrawlBuff(text4, premium, value2);
						}
						else
						{
							m_dungeonCrawlDeck.AddCard(text4, premium);
						}
					}
				}
			}
			m_dungeonCrawlDeckTray.SetDungeonCrawlDeck(m_dungeonCrawlDeck, playGlowAnimation);
			SetUpCardsCreatedByTreasures();
			SetUpPhoneNewRunScreen();
		}
	}

	private void SetUpHeroPortrait(PlayerHeroData playerHeroData)
	{
		if (m_heroActor == null)
		{
			Log.Adventures.PrintError("Unable to change hero portrait. No hero actor has been loaded.");
		}
		else
		{
			if (string.IsNullOrEmpty(playerHeroData.HeroCardId))
			{
				return;
			}
			bool flag = IsInDefeatScreen();
			NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(playerHeroData.DeckClass);
			bool flag2 = m_dungeonCrawlData.GuestHeroesExistForCurrentAdventure();
			TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
			if (!flag && !flag2 && favoriteHero != null && !GameUtils.IsVanillaHero(favoriteHero.Name))
			{
				premium = TAG_PREMIUM.GOLDEN;
			}
			SetHero(playerHeroData.HeroCardId, premium);
			if (flag)
			{
				m_heroActor.GetComponent<Animation>().Play("AllyDefeat_Desat");
			}
			if (m_heroHealth == 0L)
			{
				CardTagDbfRecord record = GameDbf.CardTag.GetRecord((CardTagDbfRecord r) => r.CardId == playerHeroData.HeroDbId && r.TagId == 45);
				m_heroHealth = record.TagValue;
			}
			SetHeroHealthVisual(m_heroActor, !flag);
			if (m_dungeonCrawlDeckSelect != null)
			{
				SetHeroHealthVisual(m_dungeonCrawlDeckSelect.heroDetails.HeroActor, !flag);
			}
		}
	}

	private void SetHero(string cardID, TAG_PREMIUM premium)
	{
		if (m_heroActor == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDisplay.SetHero was called but m_heroActor was null");
			return;
		}
		using DefLoader.DisposableFullDef disposableFullDef = DefLoader.Get().GetFullDef(cardID);
		if (!(disposableFullDef?.CardDef == null) && disposableFullDef.EntityDef != null)
		{
			m_heroActor.SetCardDef(disposableFullDef.DisposableCardDef);
			m_heroActor.SetEntityDef(disposableFullDef.EntityDef);
			disposableFullDef.CardDef.m_AlwaysRenderPremiumPortrait = false;
			m_heroActor.SetPremium(premium);
			m_heroActor.UpdateAllComponents();
			m_heroActor.Show();
			m_heroClassIconsControllerReference.RegisterReadyListener<Widget>(OnHeroClassIconsControllerReady);
		}
	}

	private void SetHeroPower(string cardID)
	{
		if (m_heroPowerActor == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDisplay.SetHeroPower was called but m_heroPowerActor was null.");
			return;
		}
		BoxCollider component = m_heroPowerActor.GetComponent<BoxCollider>();
		if (component != null)
		{
			component.enabled = false;
		}
		if (string.IsNullOrEmpty(cardID))
		{
			m_heroPowerActor.Hide();
			return;
		}
		DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(cardID);
		if (!(fullDef?.CardDef == null) && fullDef?.EntityDef != null)
		{
			m_currentHeroPowerFullDef?.Dispose();
			m_currentHeroPowerFullDef = fullDef;
			m_heroPowerActor.SetFullDef(fullDef);
			m_heroPowerActor.UpdateAllComponents();
			m_heroPowerActor.Show();
			if (component != null)
			{
				component.enabled = true;
			}
		}
	}

	private void SetUpPhoneNewRunScreen()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_dungeonCrawlDeckTray.gameObject.SetActive(value: false);
			bool showDeckButtonEnabled = m_dungeonCrawlDeck != null && m_dungeonCrawlDeck.GetTotalCardCount() > 0;
			SetShowDeckButtonEnabled(showDeckButtonEnabled);
		}
	}

	public void SetShowDeckButtonEnabled(bool enabled)
	{
		if ((bool)UniversalInputManager.UsePhoneUI && enabled != m_ShowDeckButton.IsEnabled())
		{
			m_ShowDeckButton.SetEnabled(enabled);
			m_ShowDeckButton.Flip(enabled);
		}
	}

	private void SetUpPhoneRunCompleteScreen()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_ShowDeckButtonFrame.SetActive(value: false);
			m_ShowDeckNoButtonFrame.SetActive(value: false);
			m_TrayFrameDefault.SetActive(value: false);
			m_TrayFrameRunComplete.SetActive(value: true);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_dungeonCrawlDeckTray.gameObject.SetActive(value: false);
			}
			GameUtils.SetParent(m_AdventureTitle, m_AdventureTitleRunCompleteBone);
			m_PhoneDeckTray.SetActive(value: true);
			GameUtils.SetParent(m_PhoneDeckTray, m_DeckTrayRunCompleteBone);
			TraySection traySection = (TraySection)GameUtils.Instantiate(m_DeckListHeaderPrefab, m_DeckListHeaderRunCompleteBone, withRotation: true);
			m_dungeonCrawlDeckTray.OffsetDeckBigCardByVector(m_DeckBigCardOffsetForRunCompleteState);
			traySection.m_deckBox.m_neverUseGoldenPortraits = IsInDefeatScreen();
			traySection.m_deckBox.SetHeroCardID(m_playerHeroData.HeroCardId);
			traySection.m_deckBox.HideBanner();
			traySection.m_deckBox.SetDeckName(GetClassNameFromDeckClass(m_playerHeroData.DeckClass));
			traySection.m_deckBox.HideRenameVisuals();
			traySection.m_deckBox.SetDeckNameAsSingleLine(forceSingleLine: true);
			if (IsInDefeatScreen())
			{
				traySection.m_deckBox.PlayDesaturationAnimation();
			}
		}
	}

	private bool IsInDefeatScreen()
	{
		if (m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_BOSS_GRAVEYARD)
		{
			return m_numBossesDefeated < m_numBossesInRun;
		}
		return false;
	}

	private void SetUpCardsCreatedByTreasures()
	{
		if (m_cardsAddedToDeckMap != null && m_cardsAddedToDeckMap.Count != 0 && m_cardsAddedToDeckMap.Count % 2 != 1)
		{
			Dictionary<long, long> dictionary = new Dictionary<long, long>();
			for (int i = 0; i < m_cardsAddedToDeckMap.Count; i += 2)
			{
				dictionary[m_cardsAddedToDeckMap[i]] = m_cardsAddedToDeckMap[i + 1];
			}
			m_dungeonCrawlDeckTray.CardIdToCreatorMap = dictionary;
		}
	}

	public static bool OnNavigateBack()
	{
		if (m_instance == null)
		{
			Debug.LogError("Trying to navigate back, but AdventureDungeonCrawlDisplay has been destroyed!");
			return false;
		}
		AdventureDungeonCrawlPlayMat playMat = m_instance.m_playMat;
		if (playMat != null)
		{
			if (playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
			{
				return false;
			}
			playMat.HideBossHeroPowerTooltip(immediate: true);
		}
		if ((bool)UniversalInputManager.UsePhoneUI && m_instance.m_dungeonCrawlDeckTray != null)
		{
			m_instance.m_dungeonCrawlDeckTray.gameObject.SetActive(value: false);
		}
		if (playMat != null && playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_BOSS_GRAVEYARD && m_instance.m_mustSelectChapter)
		{
			if (m_instance.m_subsceneController != null)
			{
				m_instance.m_subsceneController.ChangeSubScene(AdventureData.Adventuresubscene.LOCATION_SELECT, pushToBackStack: false);
			}
		}
		else if (m_instance.m_subsceneController != null)
		{
			m_instance.m_subsceneController.SubSceneGoBack();
		}
		return true;
	}

	private void OnBackButtonPress(UIEvent e)
	{
		EnableBackButton(enabled: false);
		Navigation.GoBack();
	}

	private void GoToHeroSelectSubscene()
	{
		bool num = m_dungeonCrawlData.GuestHeroesExistForCurrentAdventure();
		m_playMat.PlayButton.Disable();
		AdventureData.Adventuresubscene subscene = (num ? AdventureData.Adventuresubscene.ADVENTURER_PICKER : AdventureData.Adventuresubscene.MISSION_DECK_PICKER);
		if (m_subsceneController != null)
		{
			m_subsceneController.ChangeSubScene(subscene);
		}
	}

	private void GoBackToHeroPower()
	{
		m_dungeonCrawlData.SelectedHeroPowerDbId = 0L;
		SetHeroPower(null);
		StartCoroutine(ShowHeroPowerOptionsWhenReady());
	}

	private void GoBackFromHeroPower()
	{
		m_playMat.PlayHeroPowerOptionSelected();
	}

	private void GoBackToTreasureLoadoutSelection()
	{
		SetUpDeckList(new List<CardWithPremiumStatus>(), useLoadoutOfActiveRun: false, playGlowAnimation: true);
		m_dungeonCrawlData.SelectedLoadoutTreasureDbId = 0L;
		StartCoroutine(ShowTreasureSatchelWhenReady());
	}

	private void GoBackFromTreasureLoadoutSelection()
	{
		m_playMat.PlayTreasureSatchelOptionHidden();
	}

	private void GoBackFromDeckTemplateSelection()
	{
		m_dungeonCrawlData.SelectedDeckId = 0L;
		SetUpDeckList(new List<CardWithPremiumStatus>(), useLoadoutOfActiveRun: false, playGlowAnimation: true);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (m_dungeonCrawlDeckSelect == null || !m_dungeonCrawlDeckSelect.isReady)
			{
				Error.AddDevWarning("UI Error!", "AdventureDeckSelectWidget is not setup correctly or is not ready!");
				return;
			}
			m_dungeonCrawlDeckSelect.deckTray.SetDungeonCrawlDeck(m_dungeonCrawlDeck, playGlowAnimation: false);
			m_dungeonCrawlDeckSelect.playButton.Disable();
		}
		else
		{
			m_playMat.PlayButton.Disable();
		}
		m_playMat.DeselectAllDeckOptionsWithoutId(0);
		m_playMat.PlayDeckOptionSelected();
	}

	private static bool OnNavigateBackFromCurrentLoadoutState()
	{
		AdventureDungeonCrawlPlayMat playMat = m_instance.m_playMat;
		if (playMat != null && playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			return false;
		}
		if (m_instance == null)
		{
			Debug.LogError("Trying to navigate back to previous loadout selection, but AdventureDungeonCrawlDisplay has been destroyed!");
			return false;
		}
		switch (m_instance.m_currentLoadoutState)
		{
		case DungeonRunLoadoutState.HEROPOWER:
			m_instance.GoBackFromHeroPower();
			if (m_instance.m_dungeonCrawlData.SelectableLoadoutTreasuresExist() && !m_instance.m_isPVPDR)
			{
				m_instance.GoBackToTreasureLoadoutSelection();
			}
			break;
		case DungeonRunLoadoutState.TREASURE:
			m_instance.GoBackFromTreasureLoadoutSelection();
			if (m_instance.m_isPVPDR)
			{
				m_instance.GoBackToHeroPower();
			}
			break;
		case DungeonRunLoadoutState.DECKTEMPLATE:
			m_instance.GoBackFromDeckTemplateSelection();
			m_instance.GoBackToHeroPower();
			break;
		}
		return true;
	}

	private void GoToNextLoadoutState()
	{
		switch (m_currentLoadoutState)
		{
		case DungeonRunLoadoutState.HEROPOWER:
			if (m_isPVPDR)
			{
				SetUpDeckList(null, useLoadoutOfActiveRun: false);
				StartCoroutine(ShowTreasureSatchelWhenReady());
			}
			else
			{
				StartCoroutine(ShowDeckOptionsWhenReady());
			}
			break;
		case DungeonRunLoadoutState.TREASURE:
			if (m_isPVPDR)
			{
				SetUpDeckList(null, useLoadoutOfActiveRun: false);
				LockInDuelsLoadoutSelections();
				StartCoroutine(ShowBuildDeckButtonWhenReady());
			}
			else
			{
				StartCoroutine(ShowHeroPowerOptionsWhenReady());
			}
			break;
		case DungeonRunLoadoutState.INVALID:
			if (m_dungeonCrawlData.SelectableLoadoutTreasuresExist() && !m_isPVPDR)
			{
				StartCoroutine(ShowTreasureSatchelWhenReady());
			}
			else if (m_dungeonCrawlData.SelectableHeroPowersAndDecksExist())
			{
				StartCoroutine(ShowHeroPowerOptionsWhenReady());
			}
			break;
		case DungeonRunLoadoutState.DECKTEMPLATE:
			break;
		}
	}

	private void LockInDuelsLoadoutSelections()
	{
		Navigation.RemoveHandler(OnNavigateBackFromCurrentLoadoutState);
		Navigation.RemoveHandler(GuestHeroPickerTrayDisplay.OnNavigateBack);
		if (m_dungeonCrawlData.HasValidLoadoutForSelectedAdventure())
		{
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			m_dungeonCrawlData.GetSelectedAdventureDataRecord();
			list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, (long)m_dungeonCrawlData.SelectedHeroClass));
			list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, m_dungeonCrawlData.SelectedHeroPowerDbId));
			list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, m_dungeonCrawlData.SelectedLoadoutTreasureDbId));
			GameSaveDataManager.Get().SaveSubkeys(list);
			m_BackButton.SetText("GLOBAL_LEAVE");
		}
	}

	private void LockInNewRunSelectionsAndTransition()
	{
		Navigation.RemoveHandler(OnNavigateBackFromCurrentLoadoutState);
		Navigation.RemoveHandler(GuestHeroPickerTrayDisplay.OnNavigateBack);
		Navigation.RemoveHandler(AdventureLocationSelectBook.OnNavigateBack);
		Navigation.RemoveHandler(AdventureBookPageManager.NavigateToMapPage);
		if (m_subsceneController != null)
		{
			m_subsceneController.RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene.CHOOSER);
		}
		if (m_dungeonCrawlData.HasValidLoadoutForSelectedAdventure())
		{
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			AdventureDataDbfRecord selectedAdventureDataRecord = m_dungeonCrawlData.GetSelectedAdventureDataRecord();
			if (selectedAdventureDataRecord.DungeonCrawlSelectChapter)
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_SCENARIO_ID, (long)m_dungeonCrawlData.GetMission()));
			}
			if (selectedAdventureDataRecord.DungeonCrawlPickHeroFirst)
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, (long)m_dungeonCrawlData.SelectedHeroClass));
			}
			if (m_dungeonCrawlData.SelectableHeroPowersExist())
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, m_dungeonCrawlData.SelectedHeroPowerDbId));
			}
			if (!m_isPVPDR && m_dungeonCrawlData.SelectableDecksExist())
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_DECK, m_dungeonCrawlData.SelectedDeckId));
			}
			if (m_dungeonCrawlData.SelectableLoadoutTreasuresExist())
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, m_dungeonCrawlData.SelectedLoadoutTreasureDbId));
			}
			long num = (m_dungeonCrawlData.AnomalyModeActivated ? 1 : 0);
			list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_ANOMALY_MODE, num));
			GameSaveDataManager.Get().SaveSubkeys(list);
			SetShowDeckButtonEnabled(enabled: true);
			m_BackButton.SetText("GLOBAL_LEAVE");
			StartCoroutine(SetPlayMatStateFromGameSaveDataWhenReady());
		}
		else
		{
			Navigation.GoBack();
		}
	}

	private void OnPlayButtonPress(UIEvent e)
	{
		PlayButton playButton = e.GetElement() as PlayButton;
		if (playButton != null)
		{
			playButton.Disable();
		}
		m_playMat.HideBossHeroPowerTooltip(immediate: true);
		if (m_dungeonCrawlData.DoesSelectedMissionRequireDeck() && !m_dungeonCrawlData.HeroIsSelectedBeforeDungeonCrawlScreenForSelectedAdventure() && !m_shouldSkipHeroSelect && (m_numBossesDefeated == 0 || !m_isRunActive))
		{
			GoToHeroSelectSubscene();
		}
		else if (m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS && m_playMat.GetPlayMatOptionType() == AdventureDungeonCrawlPlayMat.OptionType.DECK)
		{
			m_playMat.PlayDeckOptionSelected();
			LockInNewRunSelectionsAndTransition();
		}
		else if (m_isPVPDR && (m_realDuelSeedDeck == null || !m_realDuelSeedDeck.IsValidForRuleset) && !m_isRunActive)
		{
			if (m_realDuelSeedDeck == null)
			{
				CreateDuelsSeedDeck();
				return;
			}
			playButton.Enable();
			EditCurrentDeck();
		}
		else
		{
			QueueForGame();
		}
	}

	private void QueueForGame()
	{
		List<int> guestHeroesForCurrentAdventure = m_dungeonCrawlData.GetGuestHeroesForCurrentAdventure();
		bool num = guestHeroesForCurrentAdventure.Count > 0 && (!m_shouldSkipHeroSelect || m_mustPickShrine);
		int heroCardDbId = GameUtils.GetFavoriteHeroCardDBIdFromClass(m_playerHeroData.DeckClass);
		if (num)
		{
			TAG_CLASS tAG_CLASS = m_dungeonCrawlData.SelectedHeroClass;
			if (m_isRunActive || m_mustPickShrine)
			{
				tAG_CLASS = m_playerHeroData.DeckClass;
			}
			bool flag = false;
			foreach (int item in guestHeroesForCurrentAdventure)
			{
				if (GameUtils.GetTagClassFromCardDbId(item) == tAG_CLASS)
				{
					flag = true;
					heroCardDbId = item;
					break;
				}
			}
			if (!flag)
			{
				Debug.LogErrorFormat("Attempting to start a game with class {0}, but no guest hero found of that class!", tAG_CLASS);
				return;
			}
		}
		long deckid = 0L;
		bool flag2 = false;
		if (m_isPVPDR)
		{
			CollectionDeck duelsDeck = CollectionManager.Get().GetDuelsDeck();
			if (m_realDuelSeedDeck != null)
			{
				deckid = m_realDuelSeedDeck.ID;
			}
			else if (duelsDeck != null)
			{
				deckid = duelsDeck.ID;
			}
			else
			{
				Debug.LogError("Valid duels deck not found, canceling queue");
				flag2 = true;
			}
		}
		if (flag2)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR_DECK_ERROR_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_PVPDR_DECK_ERROR_DESC");
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		if (m_isPVPDR)
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.DUELS_QUEUE);
			PVPDRLobbyDataModel pVPDRLobbyDataModel = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel();
			if (pVPDRLobbyDataModel != null)
			{
				SessionRecord sessionRecord = new SessionRecord();
				sessionRecord.Wins = (uint)pVPDRLobbyDataModel.Wins;
				sessionRecord.Losses = (uint)pVPDRLobbyDataModel.Losses;
				sessionRecord.RunFinished = false;
				sessionRecord.SessionRecordType = SessionRecordType.DUELS;
				BnetPresenceMgr.Get().SetGameFieldBlob(22u, sessionRecord);
			}
		}
		GameMgr.Get().FindGameWithHero(m_dungeonCrawlData.GameType, FormatType.FT_WILD, (int)m_dungeonCrawlData.GetMissionToPlay(), 0, heroCardDbId, deckid);
	}

	private void OnShowDeckButtonPress(UIEvent e)
	{
		ShowMobileDeckTray(m_dungeonCrawlDeckTray.gameObject.GetComponent<SlidingTray>());
	}

	protected void OnSubSceneLoaded(object sender, EventArgs args)
	{
		m_playMat.OnSubSceneLoaded();
		m_playMat.SetRewardOptionSelectedCallback(OnRewardOptionSelected);
		m_playMat.SetTreasureSatchelOptionSelectedCallback(OnTreasureSatchelOptionSelected);
		m_playMat.SetHeroPowerOptionCallback(OnHeroPowerOptionSelected, OnHeroPowerOptionRollover, OnHeroPowerOptionRollout);
		m_playMat.SetDeckOptionSelectedCallback(OnDeckOptionSelected);
	}

	protected void OnSubSceneTransitionComplete(object sender, EventArgs args)
	{
		m_subsceneTransitionComplete = true;
		if (m_dungeonCrawlDeckTray != null)
		{
			m_dungeonCrawlDeckTray.gameObject.SetActive(value: true);
		}
		if (m_playMat != null)
		{
			m_playMat.OnSubSceneTransitionComplete();
		}
	}

	private void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"AdventureDungeonCrawlDisplay.OnHeroActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		m_heroActor = go.GetComponent<Actor>();
		if (m_heroActor == null)
		{
			Debug.LogWarning($"AdventureDungeonCrawlDisplay.OnHeroActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		m_heroActor.transform.parent = m_socketHeroBone.transform;
		m_heroActor.transform.localPosition = Vector3.zero;
		m_heroActor.transform.localScale = Vector3.one;
		m_heroActor.Hide();
	}

	private void OnHeroPowerActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Debug.LogWarning($"AdventureDungeonCrawlDisplay.OnHeroPowerActorLoaded() - FAILED to load actor \"{assetRef}\"");
			return;
		}
		m_heroPowerActor = go.GetComponent<Actor>();
		if (m_heroPowerActor == null)
		{
			Debug.LogWarning($"AdventureDungeonCrawlDisplay.OnHeroPowerActorLoaded() - ERROR actor \"{assetRef}\" has no Actor component");
			return;
		}
		m_heroPowerActor.transform.parent = m_heroPowerBone;
		m_heroPowerActor.transform.localPosition = Vector3.zero;
		m_heroPowerActor.transform.localRotation = Quaternion.identity;
		m_heroPowerActor.transform.localScale = Vector3.one;
		m_heroPowerActor.Hide();
		m_heroPowerActor.SetUnlit();
		PegUIElement pegUIElement = go.AddComponent<PegUIElement>();
		go.AddComponent<BoxCollider>().enabled = false;
		pegUIElement.AddEventListener(UIEventType.ROLLOVER, delegate
		{
			ShowBigCard(m_heroPowerBigCard, m_currentHeroPowerFullDef, m_HeroPowerBigCardBone);
		});
		pegUIElement.AddEventListener(UIEventType.ROLLOUT, delegate
		{
			BigCardHelper.HideBigCard(m_heroPowerBigCard);
		});
	}

	private void SetHeroHealthVisual(Actor actor, bool show)
	{
		if (actor == null)
		{
			Log.Adventures.PrintError("SetHeroHealthVisual: actor provided is null!");
			return;
		}
		actor.GetHealthObject().gameObject.SetActive(show);
		if (show)
		{
			actor.GetHealthText().Text = Convert.ToString(m_heroHealth);
			actor.GetHealthText().AmbientLightBlend = 0f;
		}
	}

	private IEnumerator ShowTreasureSatchelWhenReady()
	{
		TAG_CLASS heroClass = m_dungeonCrawlData.SelectedHeroClass;
		while (m_playMat == null || m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			yield return null;
		}
		List<AdventureLoadoutTreasuresDbfRecord> loadoutTreasuresForClass = m_dungeonCrawlData.GetLoadoutTreasuresForClass(heroClass);
		m_playMat.ShowTreasureSatchel(loadoutTreasuresForClass, m_gameSaveDataServerKey, m_gameSaveDataClientKey);
		m_currentLoadoutState = DungeonRunLoadoutState.TREASURE;
		EnableBackButton(enabled: true);
		if (m_isPVPDR)
		{
			Navigation.PushUnique(OnNavigateBackFromCurrentLoadoutState);
		}
	}

	private IEnumerator ShowHeroPowerOptionsWhenReady()
	{
		TAG_CLASS heroPowerClass = m_dungeonCrawlData.SelectedHeroClass;
		while (m_playMat == null || m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			yield return null;
		}
		List<AdventureHeroPowerDbfRecord> heroPowersForClass = m_dungeonCrawlData.GetHeroPowersForClass(heroPowerClass);
		m_playMat.ShowHeroPowers(heroPowersForClass, m_gameSaveDataServerKey, m_gameSaveDataClientKey);
		m_currentLoadoutState = DungeonRunLoadoutState.HEROPOWER;
		EnableBackButton(enabled: true);
		if (!m_isPVPDR && m_instance.m_dungeonCrawlData.SelectableLoadoutTreasuresExist())
		{
			Navigation.PushUnique(OnNavigateBackFromCurrentLoadoutState);
		}
	}

	private IEnumerator ShowDeckOptionsWhenReady()
	{
		while (m_playMat == null || m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			yield return null;
		}
		List<AdventureDeckDbfRecord> decksForClass = m_dungeonCrawlData.GetDecksForClass(m_playerHeroData.DeckClass);
		m_playMat.ShowDecks(decksForClass, m_gameSaveDataServerKey, m_gameSaveDataClientKey);
		m_currentLoadoutState = DungeonRunLoadoutState.DECKTEMPLATE;
		EnableBackButton(enabled: true);
		Navigation.PushUnique(OnNavigateBackFromCurrentLoadoutState);
	}

	private IEnumerator ShowBuildDeckButtonWhenReady()
	{
		while (m_playMat == null || m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE)
		{
			yield return null;
		}
		m_currentLoadoutState = DungeonRunLoadoutState.LOADOUTCOMPLETE;
		m_playMat.ShowPVPDRActiveRun(GetPlayButtonTextForNextMission());
		m_playMat.PlayButton.SetText(GetPlayButtonTextForNextMission());
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_dungeonCrawlDeckSelect.deckTray.SetDungeonCrawlDeck(m_dungeonCrawlDeck, playGlowAnimation: false);
			using (DefLoader.DisposableCardDef cardDef = m_heroActor.ShareDisposableCardDef())
			{
				m_dungeonCrawlDeckSelect.heroDetails.UpdateHeroInfo(m_heroActor.GetEntityDef(), cardDef);
			}
			using DefLoader.DisposableCardDef cardDef2 = m_heroPowerActor.ShareDisposableCardDef();
			m_dungeonCrawlDeckSelect.heroDetails.UpdateHeroPowerInfo(m_heroPowerActor.GetEntityDef(), cardDef2);
		}
	}

	private void OnBossFullDefLoaded(string cardId, DefLoader.DisposableFullDef def, object userData)
	{
		using (def)
		{
			if (def == null)
			{
				Log.Adventures.PrintError("Unable to load {0} hero def for Dungeon Crawl boss.", cardId);
				m_assetLoadingHelper.AssetLoadCompleted();
				return;
			}
			string text = null;
			string cardId2 = def.EntityDef.GetCardId();
			if (GameUtils.IsModeHeroic(m_dungeonCrawlData.GetSelectedMode()))
			{
				int cardTagValue = GameUtils.GetCardTagValue(cardId2, GAME_TAG.HEROIC_HERO_POWER);
				if (cardTagValue != 0)
				{
					text = GameUtils.TranslateDbIdToCardId(cardTagValue);
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = GameUtils.GetHeroPowerCardIdFromHero(cardId2);
			}
			if (!string.IsNullOrEmpty(text))
			{
				m_assetLoadingHelper.AddAssetToLoad();
				DefLoader.Get().LoadFullDef(text, OnBossPowerFullDefLoaded);
			}
			EntityDef entityDef = def.EntityDef;
			if (entityDef != null && m_nextBossHealth != 0L && !m_isRunRetired)
			{
				entityDef = entityDef.Clone();
				entityDef.SetTag(GAME_TAG.HEALTH, m_nextBossHealth);
			}
			if (IsNextMissionASpecialEncounter() && m_bossActor != null && m_bossActor.GetHealthObject() != null)
			{
				m_bossActor.GetHealthObject().Hide();
			}
			m_playMat.SetBossFullDef(def.DisposableCardDef, entityDef);
			m_assetLoadingHelper.AssetLoadCompleted();
		}
	}

	private void OnBossPowerFullDefLoaded(string cardId, DefLoader.DisposableFullDef def, object userData)
	{
		if (def == null)
		{
			Debug.LogError($"Unable to load {cardId} hero power def for Dungeon Crawl boss.", base.gameObject);
			m_assetLoadingHelper.AssetLoadCompleted();
		}
		else
		{
			m_currentBossHeroPowerFullDef?.Dispose();
			m_currentBossHeroPowerFullDef = def;
			m_assetLoadingHelper.AssetLoadCompleted();
		}
	}

	private void OnTreasureSatchelOptionSelected(long treasureLoadoutDbId)
	{
		m_dungeonCrawlData.SelectedLoadoutTreasureDbId = treasureLoadoutDbId;
		AdventureConfig.Get().SelectedLoadoutTreasureDbId = treasureLoadoutDbId;
		if (m_dungeonCrawlData.SelectableHeroPowersAndDecksExist())
		{
			List<AdventureLoadoutTreasuresDbfRecord> loadoutTreasuresForClass = m_dungeonCrawlData.GetLoadoutTreasuresForClass(m_dungeonCrawlData.SelectedHeroClass);
			AdventureDungeonCrawlRewardOption.OptionData optionData = new AdventureDungeonCrawlRewardOption.OptionData(AdventureDungeonCrawlPlayMat.OptionType.TREASURE_SATCHEL, new List<long> { treasureLoadoutDbId }, loadoutTreasuresForClass.FindIndex((AdventureLoadoutTreasuresDbfRecord r) => r.CardId == treasureLoadoutDbId));
			OnRewardOptionSelected(optionData);
			m_playMat.PlayTreasureSatchelOptionSelected();
			GoToNextLoadoutState();
		}
		else
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnTreasureLoadoutOptionSelected: Selecting a Treasure Loadout but no Hero Power or Deck is not supported!");
		}
	}

	private void OnHeroPowerOptionSelected(long heroPowerDbId, bool isLocked)
	{
		if (!isLocked)
		{
			m_dungeonCrawlData.SelectedHeroPowerDbId = heroPowerDbId;
			AdventureConfig.Get().SelectedHeroPowerDbId = heroPowerDbId;
			SetHeroPower(GameUtils.TranslateDbIdToCardId((int)heroPowerDbId));
			if (m_HeroPowerPortraitPlayMaker != null && !string.IsNullOrEmpty(m_HeroPowerPotraitIntroStateName))
			{
				m_HeroPowerPortraitPlayMaker.SendEvent(m_HeroPowerPotraitIntroStateName);
			}
			m_playMat.PlayHeroPowerOptionSelected();
			GoToNextLoadoutState();
		}
	}

	private void OnHeroPowerOptionRollover(long heroPowerDbId, GameObject bone)
	{
		GameUtils.SetParent(m_heroPowerBigCard, bone);
		using DefLoader.DisposableFullDef heroPowerFullDef = DefLoader.Get().GetFullDef(GameUtils.TranslateDbIdToCardId((int)heroPowerDbId));
		ShowBigCard(m_heroPowerBigCard, heroPowerFullDef, m_HeroPowerBigCardBone);
	}

	private void OnHeroPowerOptionRollout(long heroPowerDbId, GameObject bone)
	{
		BigCardHelper.HideBigCard(m_heroPowerBigCard);
		GameUtils.SetParent(m_heroPowerBigCard, m_HeroPowerBigCardBone);
	}

	private void OnDeckOptionSelected(int deckId, List<long> deckContent, bool deckIsLocked)
	{
		m_playMat.DeselectAllDeckOptionsWithoutId(deckId);
		m_dungeonCrawlData.SelectedDeckId = deckId;
		SetUpDeckList(CardWithPremiumStatus.ConvertList(deckContent), useLoadoutOfActiveRun: false, playGlowAnimation: true);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (m_dungeonCrawlDeckSelect == null || !m_dungeonCrawlDeckSelect.isReady)
			{
				Error.AddDevWarning("UI Error!", "AdventureDeckSelectWidget is not setup correctly or is not ready!");
				return;
			}
			m_dungeonCrawlDeckSelect.deckTray.SetDungeonCrawlDeck(m_dungeonCrawlDeck, playGlowAnimation: false);
			using (DefLoader.DisposableCardDef cardDef = m_heroActor.ShareDisposableCardDef())
			{
				m_dungeonCrawlDeckSelect.heroDetails.UpdateHeroInfo(m_heroActor.GetEntityDef(), cardDef);
			}
			using (DefLoader.DisposableCardDef cardDef2 = m_heroPowerActor.ShareDisposableCardDef())
			{
				m_dungeonCrawlDeckSelect.heroDetails.UpdateHeroPowerInfo(m_heroPowerActor.GetEntityDef(), cardDef2);
			}
			if (deckIsLocked)
			{
				m_dungeonCrawlDeckSelect.playButton.Disable();
			}
			else
			{
				m_dungeonCrawlDeckSelect.playButton.Enable();
			}
			ShowMobileDeckTray(m_dungeonCrawlDeckSelect.slidingTray);
		}
		else if (deckIsLocked)
		{
			m_playMat.PlayButton.Disable();
		}
		else
		{
			m_playMat.PlayButton.Enable();
		}
		GoToNextLoadoutState();
	}

	private void OnRewardOptionSelected(AdventureDungeonCrawlRewardOption.OptionData optionData)
	{
		if (!GameSaveDataManager.Get().IsDataReady(m_gameSaveDataServerKey))
		{
			Log.Adventures.PrintError("Attempting to make a selection, but no data is ready yet!");
			return;
		}
		if (m_playMat.GetPlayMatState() != AdventureDungeonCrawlPlayMat.PlayMatState.SHOWING_OPTIONS)
		{
			Log.Adventures.PrintError("Attempting to choose a reward, but the Play Mat is not currently in the 'SHOWING_OPTIONS' state!");
			return;
		}
		GameSaveKeySubkeyId gameSaveKeySubkeyId = GameSaveKeySubkeyId.INVALID;
		switch (optionData.optionType)
		{
		case AdventureDungeonCrawlPlayMat.OptionType.TREASURE:
			gameSaveKeySubkeyId = GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_TREASURE;
			break;
		case AdventureDungeonCrawlPlayMat.OptionType.LOOT:
			gameSaveKeySubkeyId = GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT;
			break;
		case AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE:
			gameSaveKeySubkeyId = GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_SHRINE;
			break;
		}
		m_dungeonCrawlDeckTray.gameObject.SetActive(value: true);
		Action onCompleteCallback = null;
		if (optionData.optionType == AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE)
		{
			if (m_shrineOptions == null || m_shrineOptions.Count == 0)
			{
				Log.Adventures.PrintError("OnRewardOptionSelected: Player selected a shrine, but there are no shrine options!");
				return;
			}
			long shrineCardId = m_shrineOptions[optionData.index];
			m_playerHeroData.DeckClass = GetClassFromShrine(shrineCardId);
			SetUpDeckList(new List<CardWithPremiumStatus>(), useLoadoutOfActiveRun: false);
			onCompleteCallback = delegate
			{
				SetUpDeckListFromShrine(shrineCardId, playDeckGlowAnimation: true);
				ChangeHeroPortrait(m_playerHeroData.HeroCardId, TAG_PREMIUM.NORMAL);
			};
		}
		for (int i = ((gameSaveKeySubkeyId == GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT) ? 1 : 0); i < optionData.options.Count; i++)
		{
			string text = GameUtils.TranslateDbIdToCardId((int)optionData.options[i], showWarning: true);
			if (!string.IsNullOrEmpty(text))
			{
				Actor animateFromActor = null;
				if (!UniversalInputManager.UsePhoneUI)
				{
					animateFromActor = m_playMat.GetActorToAnimateFrom(text, optionData.index);
				}
				m_dungeonCrawlDeckTray.AddCard(text, animateFromActor, onCompleteCallback);
			}
		}
		TooltipPanelManager.Get().HideKeywordHelp();
		if (optionData.optionType != AdventureDungeonCrawlPlayMat.OptionType.TREASURE_SATCHEL)
		{
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			list.Add(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, gameSaveKeySubkeyId, optionData.index + 1));
			GameSaveDataManager.Get().SaveSubkeys(list);
			m_playMat.PlayRewardOptionSelected(optionData);
			StartCoroutine(SetPlayMatStateFromGameSaveDataWhenReady());
		}
		PlayRewardSelectVO(optionData);
	}

	private void PlayRewardSelectVO(AdventureDungeonCrawlRewardOption.OptionData optionData)
	{
		if (optionData.optionType != AdventureDungeonCrawlPlayMat.OptionType.TREASURE && optionData.optionType != AdventureDungeonCrawlPlayMat.OptionType.SHRINE_TREASURE)
		{
			return;
		}
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(m_dungeonCrawlData.GetMission());
		if (DungeonCrawlSubDef_VOLines.GetNextValidEventType(m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, m_playerHeroData.HeroDbId, DungeonCrawlSubDef_VOLines.OFFER_LOOT_PACKS_EVENTS) == DungeonCrawlSubDef_VOLines.VOEventType.INVALID)
		{
			int treasureDatabaseID = AdventureDungeonCrawlRewardOption.GetTreasureDatabaseID(optionData);
			if (treasureDatabaseID != 47251 || Options.Get().GetBool(Option.HAS_JUST_SEEN_LOOT_NO_TAKE_CANDLE_VO))
			{
				DungeonCrawlSubDef_VOLines.PlayVOLine(m_dungeonCrawlData.GetSelectedAdventure(), wingIdFromMissionId, m_playerHeroData.HeroDbId, DungeonCrawlSubDef_VOLines.VOEventType.TAKE_TREASURE_GENERAL, treasureDatabaseID);
			}
		}
	}

	private bool ShouldShowRunCompletedScreen()
	{
		if (m_isPVPDR)
		{
			PVPDRLobbyDataModel pVPDRLobbyDataModel = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel();
			if (pVPDRLobbyDataModel.HasSession)
			{
				return !pVPDRLobbyDataModel.IsSessionActive;
			}
			return true;
		}
		if (m_defeatedBossIds == null && m_bossWhoDefeatedMeId == 0L)
		{
			return false;
		}
		if (!m_isRunActive && !m_isRunRetired)
		{
			return !m_hasSeenLatestDungeonRunComplete;
		}
		return false;
	}

	private void ShowMobileDeckTray(SlidingTray tray)
	{
		if (!UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		if (tray == null)
		{
			Debug.LogError("ToggleMobileDeckTray: Could not find SlidingTray on Dungeon Crawl Deck Tray.");
			return;
		}
		m_playMat.HideBossHeroPowerTooltip(immediate: true);
		SlidingTray.TrayToggledListener trayListener = null;
		trayListener = delegate(bool shown)
		{
			OnMobileDeckTrayToggled(tray, shown, trayListener);
		};
		tray.RegisterTrayToggleListener(trayListener);
		tray.ToggleTraySlider(show: true);
	}

	private void OnMobileDeckTrayToggled(SlidingTray tray, bool shown, SlidingTray.TrayToggledListener trayListener)
	{
		if (!shown)
		{
			tray.UnregisterTrayToggleListener(trayListener);
			m_playMat.ShowBossHeroPowerTooltip();
		}
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		switch (eventData.m_state)
		{
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_CANCELED:
			HandleGameStartupFailure();
			break;
		case FindGameState.SERVER_GAME_STARTED:
			if (m_subsceneController != null)
			{
				m_subsceneController.RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene.ADVENTURER_PICKER);
				m_subsceneController.RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene.MISSION_DECK_PICKER);
			}
			break;
		}
		return false;
	}

	private void HandleGameStartupFailure()
	{
		if (SceneMgr.Get().IsInDuelsMode())
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.DUELS_IDLE);
		}
		EnablePlayButton();
	}

	private IEnumerator ShowDemoThankQuote()
	{
		string thankQuote = Vars.Key("Demo.DungeonThankQuote").GetStr("");
		float @float = Vars.Key("Demo.DungeonThankQuoteDelaySeconds").GetFloat(1f);
		float blockSeconds = Vars.Key("Demo.DungeonThankQuoteDurationSeconds").GetFloat(5f);
		BannerPopup thankBanner = null;
		yield return new WaitForSeconds(@float);
		BannerManager.Get().ShowBanner("NewPopUp_LOOT.prefab:c1f1a158f539ad3428175ebcd948f138", null, thankQuote, delegate
		{
			s_shouldShowWelcomeBanner = true;
			TryShowWelcomeBanner();
		}, delegate(BannerPopup popup)
		{
			thankBanner = popup;
			GameObject obj = CameraUtils.CreateInputBlocker(CameraUtils.FindFirstByLayer(thankBanner.gameObject.layer), "BannerInputBlocker", thankBanner.transform);
			obj.transform.localPosition = new Vector3(0f, 100f, 0f);
			obj.layer = 17;
		});
		while (thankBanner == null)
		{
			yield return null;
		}
		yield return new WaitForSeconds(blockSeconds);
		thankBanner.Close();
	}

	private static bool IsInDemoMode()
	{
		return DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2017_ADVENTURE;
	}

	private void DisableBackButtonIfInDemoMode()
	{
		if (IsInDemoMode())
		{
			m_BackButton.SetEnabled(enabled: false);
			m_BackButton.Flip(faceUp: false);
		}
	}

	private void SetDungeonCrawlDisplayVisualStyle()
	{
		DungeonRunVisualStyle visualStyle = m_dungeonCrawlData.VisualStyle;
		foreach (DungeonCrawlDisplayStyleOverride item in m_DungeonCrawlDisplayStyle)
		{
			if (item.VisualStyle != visualStyle)
			{
				continue;
			}
			MeshRenderer component = m_dungeonCrawlTray.GetComponent<MeshRenderer>();
			if (component != null && item.DungeonCrawlTrayMaterial != null)
			{
				component.SetMaterial(item.DungeonCrawlTrayMaterial);
			}
			if ((bool)UniversalInputManager.UsePhoneUI && m_ViewDeckTrayMesh != null)
			{
				MeshRenderer component2 = m_ViewDeckTrayMesh.GetComponent<MeshRenderer>();
				if (component2 != null && item.PhoneDeckTrayMaterial != null)
				{
					component2.SetMaterial(item.PhoneDeckTrayMaterial);
				}
			}
			break;
		}
	}

	private string GetClassNameFromDeckClass(TAG_CLASS deckClass)
	{
		List<int> guestHeroesForCurrentAdventure = m_dungeonCrawlData.GetGuestHeroesForCurrentAdventure();
		if (guestHeroesForCurrentAdventure.Count == 0)
		{
			return GameStrings.GetClassName(deckClass);
		}
		foreach (int guestHeroCardDbId in guestHeroesForCurrentAdventure)
		{
			if (GameUtils.GetTagClassFromCardDbId(guestHeroCardDbId) == deckClass)
			{
				return GameDbf.GuestHero.GetRecord((GuestHeroDbfRecord r) => r.CardId == guestHeroCardDbId).Name;
			}
		}
		return string.Empty;
	}

	private TAG_CLASS GetClassFromShrine(long shrineCardId)
	{
		return GameUtils.GetTagClassFromCardDbId((int)shrineCardId);
	}

	private void ChangeHeroPortrait(string newHeroCardId, TAG_PREMIUM premium)
	{
		if (m_heroActor == null)
		{
			Log.Adventures.PrintError($"Unable to change hero portrait to cardId={newHeroCardId}. No actor has been loaded.");
			return;
		}
		DefLoader.Get().LoadFullDef(newHeroCardId, delegate(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
		{
			using (fullDef)
			{
				m_heroActor.SetFullDef(fullDef);
				m_heroActor.SetPremium(premium);
				m_heroActor.GetComponent<PlayMakerFSM>().SendEvent(fullDef.EntityDef.GetClass().ToString());
			}
		});
	}

	public static Actor OnActorLoaded(string actorName, GameObject actorObject, GameObject container, bool withRotation = false)
	{
		Actor component = actorObject.GetComponent<Actor>();
		if (component != null)
		{
			if (container != null)
			{
				GameUtils.SetParent(component, container, withRotation);
				SceneUtils.SetLayer(component, container.layer);
			}
			component.SetUnlit();
			component.Hide();
		}
		else
		{
			Debug.LogWarning($"ERROR actor \"{actorName}\" has no Actor component");
		}
		return component;
	}

	private void ShowBigCard(Actor heroPowerBigCard, DefLoader.DisposableFullDef heroPowerFullDef, GameObject bone)
	{
		Vector3? origin = null;
		if (m_heroPowerActor != null)
		{
			origin = m_heroPowerActor.gameObject.transform.position;
		}
		BigCardHelper.ShowBigCard(heroPowerBigCard, heroPowerFullDef, bone, m_BigCardScale, origin);
	}

	private void OnHeroClassIconsControllerReady(Widget widget)
	{
		if (widget == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - widget was null!");
			return;
		}
		if (m_heroActor == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - m_heroActor was null!");
			return;
		}
		HeroClassIconsDataModel heroClassIconsDataModel = new HeroClassIconsDataModel();
		EntityDef entityDef = m_heroActor.GetEntityDef();
		if (entityDef == null)
		{
			Debug.LogWarning("AdventureDungeonCrawlDisplay.OnHeroIconsControllerReady - m_heroActor did not contain an entity def!");
			return;
		}
		heroClassIconsDataModel.Classes.Clear();
		foreach (TAG_CLASS @class in entityDef.GetClasses())
		{
			heroClassIconsDataModel.Classes.Add(@class);
		}
		widget.BindDataModel(heroClassIconsDataModel);
	}

	private static void ResetDungeonCrawlSelections(IDungeonCrawlData data)
	{
		data.SelectedLoadoutTreasureDbId = 0L;
		data.SelectedHeroPowerDbId = 0L;
		data.SelectedDeckId = 0L;
	}

	private void OnRetirePopupResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			m_retireButton.SetActive(value: true);
			return;
		}
		Navigation.GoBack();
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(m_gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, 1L), delegate(bool dataWrittenSuccessfully)
		{
			HandleRetireSuccessOrFail(dataWrittenSuccessfully);
		});
	}

	private void HandleRetireSuccessOrFail(bool success)
	{
		if (!success)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_RETIRE_CONFIRMATION_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_RETIRE_FAILURE_BODY");
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			DialogManager.Get().ShowPopup(popupInfo);
			m_retireButton.SetActive(value: true);
		}
	}

	public void CreateDuelsSeedDeck()
	{
		if (!m_seedDeckCreateRequested)
		{
			CollectionDeckTray.Get().GetDecksContent().CreateNewDeckFromUserSelection(m_dungeonCrawlData.SelectedHeroClass, GetDuelsHeroCardIDFromClass(m_dungeonCrawlData.SelectedHeroClass), "PVPDR DECK");
			m_seedDeckCreateRequested = true;
		}
	}

	private void OnDeckCreated(long deckID)
	{
		if (m_seedDeckCreateRequested)
		{
			CollectionDeck deck = CollectionManager.Get().GetDeck(deckID);
			if (deck != null && deckID == deck.ID && m_isPVPDR)
			{
				m_realDuelSeedDeck = deck;
				Network.Get().SendPVPDRSessionInfoRequest();
				m_playMat.PlayButton.Enable();
				EditCurrentDeck();
				m_seedDeckCreateRequested = false;
			}
		}
	}

	public bool BackFromDeckEdit(CollectionDeck deck)
	{
		SyncDeckList();
		SaveDuelsDeckList();
		StartCoroutine(EnablePlayButtonWhenDeckChangesAreSaved(20f));
		m_BackButton.SetText("GLOBAL_LEAVE");
		m_dungeonCrawlDeckTray.m_cardsContent.UpdateCardList();
		if (m_realDuelSeedDeck.IsValidForRuleset)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR_LOCK_IN");
			popupInfo.m_text = GameStrings.Get("GLUE_PVPDR_LOCK_IN_DESC");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					LockInNewRunSelectionsAndTransition();
					Network.Get().SendPVPDRSessionInfoRequest();
					CollectionDeckTray.Get().OnConfirmBackOutOfDeckContentsDuel();
					Navigation.RemoveHandler(OnExitCollection);
					PvPDungeonRunScene.Get().ShowDungeonCrawlDisplay(delegate
					{
						if ((bool)UniversalInputManager.UsePhoneUI)
						{
							m_dungeonCrawlDeckTray.gameObject.SetActive(value: true);
						}
					});
				}
			};
			DialogManager.Get().ShowPopup(popupInfo);
			return false;
		}
		return true;
	}

	public string GetDuelsHeroCardIDFromClass(TAG_CLASS heroClass)
	{
		string result = string.Empty;
		if (m_dungeonCrawlData != null)
		{
			foreach (AdventureGuestHeroesDbfRecord sortedGuestHeroRecordsForAdventure in AdventureUtils.GetSortedGuestHeroRecordsForAdventures(m_dungeonCrawlData.GetSelectedAdventure()))
			{
				int cardIdFromGuestHeroDbId = GameUtils.GetCardIdFromGuestHeroDbId(sortedGuestHeroRecordsForAdventure.GuestHeroId);
				if (GameUtils.GetTagClassFromCardDbId(cardIdFromGuestHeroDbId) == heroClass)
				{
					result = GameUtils.TranslateDbIdToCardId(cardIdFromGuestHeroDbId);
				}
			}
			return result;
		}
		return result;
	}

	private IEnumerator EnablePlayButtonWhenDeckChangesAreSaved(float timeout)
	{
		bool didTimeout = false;
		while (m_realDuelSeedDeck.IsSavingChanges())
		{
			DisablePlayButton();
			timeout -= Time.deltaTime;
			if (timeout <= 0f)
			{
				didTimeout = true;
				break;
			}
			yield return null;
		}
		if (didTimeout)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR_DECK_ERROR_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_GENERIC_ERROR");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = delegate
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			};
			DialogManager.Get().ShowPopup(popupInfo);
		}
		else
		{
			EnablePlayButton();
		}
	}

	private void EditCurrentDeck()
	{
		Navigation.Push(OnExitCollection);
		if (m_dungeonCrawlDeck != null)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_dungeonCrawlDeckTray.gameObject.SetActive(value: false);
			}
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			collectionManagerDisplay.EnableInput(enable: true);
			CollectionDeckTray.Get().EnterDeckEditForPVPDR(m_dungeonCrawlDeck);
			PvPDungeonRunScene.Get().HideDungeonCrawlDisplay();
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.DUELS_BUILDING_DECK);
			collectionManagerDisplay.CheckClipboardAndPromptPlayerToPaste();
		}
	}

	public bool GetDuelsDeckIsValid()
	{
		if (m_realDuelSeedDeck != null)
		{
			return m_realDuelSeedDeck.IsValidForRuleset;
		}
		return false;
	}

	public void SyncDeckList()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (m_realDuelSeedDeck == null || editedDeck == null)
		{
			return;
		}
		m_realDuelSeedDeck.RemoveAllCards();
		foreach (CollectionDeckSlot slot in editedDeck.GetSlots())
		{
			m_realDuelSeedDeck.AddCard(slot.CardID, slot.PreferredPremium);
		}
		if (editedDeck.CardBackID != m_realDuelSeedDeck.CardBackID)
		{
			m_realDuelSeedDeck.CardBackID = editedDeck.CardBackID;
			m_realDuelSeedDeck.CardBackOverridden = true;
		}
	}

	public void SaveDuelsDeckList()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (m_realDuelSeedDeck != null && editedDeck != null)
		{
			CollectionManager.Get().SetEditedDeck(m_realDuelSeedDeck);
			CollectionDeckTray.SaveCurrentDeck();
			CollectionManager.Get().SetEditedDeck(editedDeck);
		}
	}

	private bool OnExitCollection()
	{
		if (CollectionDeckTray.Get().OnBackOutOfDeckContents())
		{
			(CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay).EnableInput(enable: false);
			PvPDungeonRunScene.Get().ShowDungeonCrawlDisplay(delegate
			{
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					m_dungeonCrawlDeckTray.gameObject.SetActive(value: true);
				}
			});
			return true;
		}
		return false;
	}

	private void OnPVPDRRetirePopupResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL)
		{
			m_retireButton.SetActive(value: true);
			return;
		}
		Network.Get().RegisterNetHandler(PVPDRRetireResponse.PacketID.ID, OnPVPDRRetireResponse);
		Network.Get().SendPVPDRRetireRequest();
	}

	private void OnPVPDRRetireResponse()
	{
		GameSaveDataManager.Get().Request(m_gameSaveDataServerKey);
		Network.Get().SendPVPDRSessionInfoRequest();
		Network.Get().RemoveNetHandler(PVPDRRetireResponse.PacketID.ID, OnPVPDRRetireResponse);
		PVPDRRetireResponse pVPDRRetireResponse = Network.Get().GetPVPDRRetireResponse();
		bool flag = pVPDRRetireResponse != null && pVPDRRetireResponse.ErrorCode == ErrorCode.ERROR_OK;
		HandleRetireSuccessOrFail(flag);
		if (!flag)
		{
			return;
		}
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().RecentWin = false;
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().RecentLoss = false;
		if (PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsPaidEntry)
		{
			ShowDuelsEndRun();
			return;
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			Navigation.GoBack();
		}
		EndDuelsSession(0L);
	}

	private void ShowDuelsEndRun()
	{
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.DUELS_REWARD);
		m_playMat.ShowPVPDRReward();
		m_BackButton.SetEnabled(enabled: false);
		m_BackButton.Flip(faceUp: false);
		Navigation.PushBlockBackingOut();
	}

	public void EndDuelsSession(long noticeId = 0L)
	{
		m_rewardNoticeId = noticeId;
		Network.Get().RegisterNetHandler(PVPDRSessionEndResponse.PacketID.ID, OnSessionEndResponse);
		Network.Get().SendPVPDRSessionEndRequest();
	}

	private void OnSessionEndResponse()
	{
		Network.Get().RemoveNetHandler(PVPDRSessionEndResponse.PacketID.ID, OnSessionEndResponse);
		PVPDRSessionEndResponse pVPDRSessionEndResponse = Network.Get().GetPVPDRSessionEndResponse();
		if (pVPDRSessionEndResponse.ErrorCode != 0)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_PVPDR");
			popupInfo.m_text = GameStrings.Get("GLUE_PVPDR_SESSION_END_ERROR");
			popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = delegate
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			};
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		if (m_rewardNoticeId > 0)
		{
			Network.Get().AckNotice(m_rewardNoticeId);
			m_rewardNoticeId = 0L;
		}
		DuelsConfig.Get().SetRecentEnd(value: true);
		int lastRatingChange = pVPDRSessionEndResponse.NewRating - PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().Rating;
		int lastRatingChange2 = pVPDRSessionEndResponse.NewPaidRating - PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().PaidRating;
		Network.Get().RequestPVPDRStatsInfo();
		DuelsConfig.Get().LastRunWins = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().Wins;
		if (!PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsPaidEntry)
		{
			PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().LastPlayedMode = 1;
			PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().LastRatingChange = lastRatingChange;
			StartCoroutine(ShowRatingChangePopupWhenReady(delegate
			{
				OnSessionEndComplete();
			}));
		}
		else
		{
			PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().LastPlayedMode = 2;
			PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().LastRatingChange = lastRatingChange2;
			OnSessionEndComplete();
		}
	}

	private IEnumerator ShowRatingChangePopupWhenReady(Action callback)
	{
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsRatingNotice = true;
		while (m_playMat == null || !m_playMat.IsReady() || m_playMat.GetPlayMatState() == AdventureDungeonCrawlPlayMat.PlayMatState.TRANSITIONING_FROM_PREV_STATE || !Navigation.CanGoBack)
		{
			Log.Adventures.Print("Waiting for Play Mat to be initialized before showing rating change popup");
			yield return null;
		}
		int lastRatingChange = PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().LastRatingChange;
		int lastRunWins = DuelsConfig.Get().LastRunWins;
		string key = "GLUE_PVPDR_END_OF_RUN_TIER_0_WIN";
		string text = string.Concat(lastRatingChange);
		if (lastRatingChange >= 0)
		{
			text = "+" + lastRatingChange;
		}
		if (lastRunWins >= 12)
		{
			key = "GLUE_PVPDR_END_OF_RUN_TIER_MAX_WIN";
		}
		else if (lastRunWins >= 6)
		{
			key = "GLUE_PVPDR_END_OF_RUN_TIER_2_WIN";
		}
		else if (lastRunWins >= 1)
		{
			key = "GLUE_PVPDR_END_OF_RUN_TIER_1_WIN";
		}
		PvPDungeonRunScene.ShowDuelsMessagePopup(GameStrings.Format("GLUE_PVPDR_END_OF_RUN_HEADER", lastRunWins), GameStrings.Get(key), GameStrings.Format("GLUE_PVPDR_RATING_CHANGE", text), callback);
	}

	private void OnSessionEndComplete()
	{
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().HasSession = false;
		PvPDungeonRunDisplay.Get().GetPVPDRLobbyDataModel().IsSessionActive = false;
		Navigation.PopBlockBackingOut();
		Navigation.GoBack();
	}
}
