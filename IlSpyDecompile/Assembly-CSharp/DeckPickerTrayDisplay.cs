using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.Core;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class DeckPickerTrayDisplay : AbsDeckPickerTrayDisplay
{
	[Serializable]
	public class ModeTextures
	{
		[SerializeField]
		public Texture customStandardTex;

		[SerializeField]
		public Texture customWildTex;

		[SerializeField]
		public Texture customClassicTex;

		[SerializeField]
		public Texture customCasualTex;

		[SerializeField]
		public Texture standardTex;

		[SerializeField]
		public Texture wildTex;

		[SerializeField]
		public Texture classicTex;

		[SerializeField]
		public Texture casualTex;

		[SerializeField]
		public Texture classDivotTex;

		[SerializeField]
		public Texture guestHeroDivotTex;

		public Texture GetTextureForFormat(VisualsFormatType visualsFormatType)
		{
			switch (visualsFormatType)
			{
			case VisualsFormatType.VFT_STANDARD:
				return standardTex;
			case VisualsFormatType.VFT_WILD:
				return wildTex;
			case VisualsFormatType.VFT_CLASSIC:
				return classicTex;
			case VisualsFormatType.VFT_CASUAL:
				return casualTex;
			default:
				Debug.LogError("ModeTextures.GetTextureForFormat does not support " + visualsFormatType);
				return null;
			}
		}

		public Texture GetCustomTextureForFormat(VisualsFormatType visualsFormatType)
		{
			switch (visualsFormatType)
			{
			case VisualsFormatType.VFT_STANDARD:
				return customStandardTex;
			case VisualsFormatType.VFT_WILD:
				return customWildTex;
			case VisualsFormatType.VFT_CLASSIC:
				return customClassicTex;
			case VisualsFormatType.VFT_CASUAL:
				return customCasualTex;
			default:
				Debug.LogError("ModeTextures.GetTextureForFormat does not support " + visualsFormatType);
				return null;
			}
		}
	}

	private enum SetRotationTutorialState
	{
		INACTIVE,
		PREPARING,
		READY,
		SHOW_TUTORIAL_POPUPS,
		SWITCH_MODE_WALKTHROUGH,
		SHOW_QUEST_LOG
	}

	public Transform m_rankedPlayDisplayWidgetBone;

	public Texture m_emptyHeroTexture;

	public NestedPrefab m_leftArrowNestedPrefab;

	public NestedPrefab m_rightArrowNestedPrefab;

	public GameObject m_modeLabelBg;

	public GameObject m_randomDecksHiddenBone;

	public GameObject m_suckedInRandomDecksBone;

	public HeroXPBar m_xpBarPrefab;

	public GameObject m_rankedWinsPlate;

	public UberText m_rankedWins;

	public BoxCollider m_clickBlocker;

	public Animator m_premadeDeckGlowAnimator;

	public GameObject m_hierarchyDeckTray;

	[CustomEditField(Sections = "Deck Pages")]
	public GameObject m_customDeckPagesRoot;

	[CustomEditField(Sections = "Deck Pages")]
	public GameObject m_customDeckPageUpperBone;

	[CustomEditField(Sections = "Deck Pages")]
	public GameObject m_customDeckPageLowerBone;

	[CustomEditField(Sections = "Deck Pages")]
	public GameObject m_customDeckPageHideBone;

	public Widget m_casualPlayDisplayWidget;

	public GameObject m_missingClassicDeck;

	public HighlightState m_collectionButtonGlow;

	public GameObject m_labelDecoration;

	public List<PlayMakerFSM> formatChangeGlowFSMs;

	public List<PlayMakerFSM> newDeckFormatChangeGlowFSMs;

	public List<GameObject> m_premadeDeckGlowBurstObjects;

	public NestedPrefab m_switchFormatButtonContainer;

	private SwitchFormatButton m_switchFormatButton;

	public GameObject m_TheClockButtonBone;

	public string m_leavingWildGlowEvent;

	public string m_leavingClassicGlowEvent;

	public string m_leavingCasualGlowEvent;

	public string m_enteringWildGlowEvent;

	public string m_enteringClassicGlowEvent;

	public string m_enteringCasualGlowEvent;

	public string m_newDeckLeavingClassicGlowEvent;

	public string m_newDeckEnteringClassicGlowEvent;

	public string m_newDeckLeavingWildGlowEvent;

	public string m_newDeckEnteringWildGlowEvent;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_standardTransitionSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_wildTransitionSound;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_classicTransitionSound;

	[CustomEditField(Sections = "Deck Sharing")]
	public UIBButton m_DeckShareRequestButton;

	[CustomEditField(Sections = "Deck Sharing")]
	public GameObject m_DeckShareGlowOutQuad;

	[CustomEditField(Sections = "Deck Sharing")]
	public float m_DeckShareGlowOutIntensity;

	[CustomEditField(Sections = "Deck Sharing")]
	public ParticleSystem m_DeckShareParticles;

	[CustomEditField(Sections = "Deck Sharing")]
	public float m_DeckShareTransitionTime = 1f;

	[CustomEditField(Sections = "Phone Only")]
	public SlidingTray m_rankedDetailsTray;

	[CustomEditField(Sections = "Phone Only")]
	public GameObject m_detailsTrayFrame;

	[CustomEditField(Sections = "Phone Only")]
	public Transform m_medalBone_phone;

	[CustomEditField(Sections = "Phone Only")]
	public Mesh m_alternateDetailsTrayMesh;

	[CustomEditField(Sections = "Phone Only")]
	public Material m_arrowButtonShadowMaterial;

	[CustomEditField(Sections = "Mode Background Textures")]
	public ModeTextures m_adventureTextures;

	[CustomEditField(Sections = "Mode Background Textures")]
	public ModeTextures m_collectionTextures;

	[CustomEditField(Sections = "Mode Background Textures")]
	public ModeTextures m_tavernBrawlTextures;

	[CustomEditField(Sections = "Mode Background Textures")]
	public ModeTextures m_tournamentTextures;

	[CustomEditField(Sections = "Mode Background Textures")]
	public ModeTextures m_friendlyTextures;

	public float m_rankedPlayDisplayShowDelay;

	public float m_rankedPlayDisplayHideDelay;

	private const float TRAY_SLIDE_TIME = 0.25f;

	private const float TRAY_SINK_TIME = 0f;

	private static readonly Vector3 INNKEEPER_QUOTE_POS = new Vector3(103f, NotificationManager.DEPTH, 42f);

	private static readonly string STANDARD_COMING_SOON_POPUP_NAME = "RotationPopUp_ComingSoon.prefab:afff670e4001e11429c04d2e0c27dd76";

	private static readonly AssetReference CUSTOM_DECK_PAGE = new AssetReference("CustomDeckPage_Top.prefab:650072e121717c04f89ac014eb3dc290");

	private static readonly AssetReference FORMAT_TYPE_PICKER_POPUP_PREFAB = new AssetReference("FormatTypePickerPopup.prefab:aa88133d144782b40b3fd8818084006c");

	private const string CREATE_WILD_DECK_STRING_FORMAT = "GLUE_CREATE_WILD_DECK";

	private const string CREATE_STANDARD_DECK_STRING_FORMAT = "GLUE_CREATE_STANDARD_DECK";

	private const string CREATE_CLASSIC_DECK_STRING_FORMAT = "GLUE_CREATE_CLASSIC_DECK";

	private const string WILD_CLICKED_EVENT_NAME = "WILD_BUTTON_CLICKED";

	private const string STANDARD_CLICKED_EVENT_NAME = "STANDARD_BUTTON_CLICKED";

	private const string CLASSIC_CLICKED_EVENT_NAME = "CLASSIC_BUTTON_CLICKED";

	private const string CASUAL_CLICKED_EVENT_NAME = "CASUAL_BUTTON_CLICKED";

	private const string OPEN_WITH_CASUAL_EVENT = "OPEN_WITH_CASUAL";

	private const string OPEN_WITHOUT_CASUAL_EVENT = "OPEN_WITHOUT_CASUAL";

	private const string SETROTATION_OPEN_WITH_CASUAL_EVENT = "SETROTATION_OPEN_WITH_CASUAL";

	private const string HIDE = "HIDE";

	private const string HIDE_WITH_CASUAL_EVENT = "HIDE_WITH_CASUAL";

	private const string HIDE_WITHOUT_CASUAL_EVENT = "HIDE_WITHOUT_CASUAL";

	private const string FORMAT_PICKER_4_BUTTONS = "4BUTTONS";

	private const string FORMAT_PICKER_3_BUTTONS = "3BUTTONS";

	private const string FORMAT_PICKER_2_BUTTONS = "2BUTTONS";

	private UIBButton m_leftArrow;

	private UIBButton m_rightArrow;

	private HeroXPBar m_xpBar;

	private CollectionDeckBoxVisual m_selectedCustomDeckBox;

	private ModeTextures m_currentModeTextures;

	private bool m_heroChosen;

	private static Coroutine s_selectHeroCoroutine = null;

	private DeckPickerMode m_deckPickerMode;

	private int m_currentPageIndex;

	private static DeckPickerTrayDisplay s_instance;

	private RankedPlayDisplay m_rankedPlayDisplay;

	private int m_numDecks;

	private int m_numPagesToShow = 1;

	private List<CustomDeckPage> m_customPages = new List<CustomDeckPage>();

	private bool m_delayButtonAnims;

	private Notification m_expoThankQuote;

	private Notification m_expoIntroQuote;

	private Notification m_switchFormatPopup;

	private Notification m_innkeeperQuote;

	private bool m_showStandardComingSoonNotice;

	private GameLayer m_defaultDetailsLayer;

	private bool m_usingSharedDecks;

	private bool m_doingDeckShareTransition;

	private bool m_isDeckShareRequestButtonHovered;

	private long m_lastSeasonBonusStarPopUpSeen;

	private long m_bonusStarsPopUpSeenCount;

	private TranslatedMedalInfo m_currentMedalInfo;

	private bool m_inHeroPicker;

	private Widget m_formatTypePickerWidget;

	private Widget m_rankedPlayDisplayWidget;

	private bool m_HasSeenPlayStandardToWildVO;

	private bool m_HasSeenPlayStandardToClassicVO;

	private Coroutine m_showLeftArrowCoroutine;

	private Coroutine m_showRightArrowCoroutine;

	[CustomEditField(Sections = "Set Rotation Tutorial")]
	public GameObject m_formatTutorialPopUpPrefab;

	[CustomEditField(Sections = "Set Rotation Tutorial")]
	public Transform m_formatTutorialPopUpBone;

	[CustomEditField(Sections = "Set Rotation Tutorial")]
	public Transform m_Switch_Format_Notification_Bone;

	[CustomEditField(Sections = "Set Rotation Tutorial")]
	public Animator m_dimQuad;

	[CustomEditField(Sections = "Set Rotation Tutorial")]
	public PegUIElement m_clickCatcher;

	[CustomEditField(Sections = "Set Rotation Tutorial", T = EditType.SOUND_PREFAB)]
	public string m_wildDeckTransitionSound;

	private SetRotationTutorialState m_setRotationTutorialState;

	private float m_showQuestPause = 1f;

	private float m_playVOPause = 1f;

	private bool m_shouldContinue;

	private List<long> m_noticeIdsToAck = new List<long>();

	public override void Awake()
	{
		base.Awake();
		SoundManager.Get().Load("hero_panel_slide_on.prefab:236147a924d7cb442872b46dddd56132");
		SoundManager.Get().Load("hero_panel_slide_off.prefab:ed410a050e783564384ca51e701ede4d");
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY)
		{
			m_delayButtonAnims = true;
			LoadingScreen.Get().RegisterFinishedTransitionListener(OnTransitionFromGameplayFinished);
		}
		SceneMgr.Get().RegisterScenePreUnloadEvent(OnScenePreUnload);
		s_instance = this;
		if (m_collectionButton != null)
		{
			if (IsDeckSharingActive())
			{
				m_collectionButton.gameObject.SetActive(value: false);
			}
			else
			{
				m_collectionButton.gameObject.SetActive(value: true);
				SetCollectionButtonEnabled(ShouldShowCollectionButton());
				if (m_collectionButton.IsEnabled())
				{
					TelemetryWatcher.WatchFor(TelemetryWatcherWatchType.CollectionManagerFromDeckPicker);
					m_collectionButton.SetText(GameStrings.Get("GLUE_MY_COLLECTION"));
					m_collectionButton.AddEventListener(UIEventType.RELEASE, CollectionButtonPress);
				}
			}
		}
		if (m_DeckShareRequestButton != null)
		{
			if (IsDeckSharingActive())
			{
				m_DeckShareRequestButton.gameObject.SetActive(value: true);
				EnableRequestDeckShareButton(enable: true);
				m_DeckShareRequestButton.SetText(GameStrings.Get("GLUE_DECK_SHARE_BUTTON_BORROW_DECKS"));
				m_DeckShareRequestButton.AddEventListener(UIEventType.RELEASE, RequestDeckShareButtonPress);
				m_DeckShareRequestButton.AddEventListener(UIEventType.ROLLOVER, RequestDeckShareButtonOver);
				m_DeckShareRequestButton.AddEventListener(UIEventType.ROLLOUT, RequestDeckShareButtonOut);
			}
			else
			{
				m_DeckShareRequestButton.gameObject.SetActive(value: false);
			}
		}
		if (m_DeckShareGlowOutQuad != null)
		{
			m_DeckShareGlowOutQuad.SetActive(value: false);
		}
		m_xpBar = UnityEngine.Object.Instantiate(m_xpBarPrefab);
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		m_xpBar.m_soloLevelLimit = netObject?.XPSoloLimit ?? 60;
	}

	private void Start()
	{
		Navigation.PushIfNotOnTop(OnNavigateBack);
		GameObject gameObject = m_leftArrowNestedPrefab.PrefabGameObject();
		m_leftArrow = gameObject.GetComponent<UIBButton>();
		m_leftArrow.AddEventListener(UIEventType.RELEASE, OnShowPreviousPage);
		gameObject = m_rightArrowNestedPrefab.PrefabGameObject();
		m_rightArrow = gameObject.GetComponent<UIBButton>();
		m_rightArrow.AddEventListener(UIEventType.RELEASE, OnShowNextPage);
		UpdatePageArrows();
		m_currentMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo().GetCurrentMedalForCurrentFormatType();
		m_formatTypePickerWidget = WidgetInstance.Create(FORMAT_TYPE_PICKER_POPUP_PREFAB);
		m_formatTypePickerWidget.Hide();
		m_formatTypePickerWidget.RegisterReadyListener(delegate
		{
			OnFormatTypePickerPopupReady();
		});
		m_formatTypePickerWidget.RegisterEventListener(OnFormatTypePickerEvent);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		HideDemoQuotes();
		if (TournamentDisplay.Get() != null)
		{
			TournamentDisplay.Get().RemoveMedalChangedListener(OnMedalChanged);
		}
		if (FriendChallengeMgr.Get() != null && Get() != null)
		{
			FriendChallengeMgr.Get().RemoveChangedListener(Get().OnFriendChallengeChanged);
		}
		if (SceneMgr.Get() != null && SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.FRIENDLY && SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			FriendChallengeMgr.Get().CancelChallenge();
		}
		s_instance = null;
	}

	public static DeckPickerTrayDisplay Get()
	{
		return s_instance;
	}

	public void SetInHeroPicker()
	{
		m_inHeroPicker = true;
	}

	public void OverridePlayButtonCallback(UIEvent.Handler callback)
	{
		if (m_playButton != null)
		{
			m_playButton.RemoveEventListener(UIEventType.RELEASE, OnPlayGameButtonReleased);
			m_playButton.AddEventListener(UIEventType.RELEASE, callback);
		}
	}

	public bool IsShowingCustomDecks()
	{
		return m_deckPickerMode == DeckPickerMode.CUSTOM;
	}

	public void SuckInFinished()
	{
		HideRandomDeckPickerTray();
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
	}

	private void OnShowNextPage(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("hero_panel_slide_off.prefab:ed410a050e783564384ca51e701ede4d");
		ShowNextPage();
	}

	public override void ResetCurrentMode()
	{
		if (m_selectedCustomDeckBox != null)
		{
			SetPlayButtonEnabled(enable: true);
			SetHeroRaised(raised: true);
		}
		else if (m_selectedHeroButton != null)
		{
			SetHeroRaised(raised: true);
			SetPlayButtonEnabled(!m_selectedHeroButton.IsLocked());
		}
		SetHeroButtonsEnabled(enable: true);
	}

	public int GetSelectedHeroLevel()
	{
		if (m_selectedHeroButton == null)
		{
			return 0;
		}
		return GameUtils.GetHeroLevel(m_selectedHeroButton.GetEntityDef().GetClass()).CurrentLevel.Level;
	}

	public void ToggleRankedDetailsTray(bool shown)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_rankedDetailsTray.ToggleTraySlider(shown);
		}
	}

	public override long GetSelectedDeckID()
	{
		if (null != m_selectedCustomDeckBox)
		{
			return m_selectedCustomDeckBox.GetDeckID();
		}
		return base.GetSelectedDeckID();
	}

	public CollectionDeck GetSelectedCollectionDeck()
	{
		if (!(m_selectedCustomDeckBox == null))
		{
			return m_selectedCustomDeckBox.GetCollectionDeck();
		}
		return null;
	}

	public void UpdateCreateDeckText()
	{
		string key;
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			key = ((TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? "GLOBAL_HEROIC_BRAWL" : "GLOBAL_TAVERN_BRAWL");
		}
		else
		{
			PegasusShared.FormatType formatType = Options.GetFormatType();
			switch (formatType)
			{
			case PegasusShared.FormatType.FT_WILD:
				key = "GLUE_CREATE_WILD_DECK";
				break;
			case PegasusShared.FormatType.FT_STANDARD:
				key = "GLUE_CREATE_STANDARD_DECK";
				break;
			case PegasusShared.FormatType.FT_CLASSIC:
				key = "GLUE_CREATE_CLASSIC_DECK";
				break;
			default:
				Debug.LogError("DeckPickerTrayDisplay.UpdateCreateDeckText called in unsupported format type: " + formatType);
				SetHeaderText("UNSUPPORTED DECK TEXT " + formatType);
				return;
			}
		}
		SetHeaderText(GameStrings.Get(key));
	}

	public bool UpdateRankedClassWinsPlate()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && m_heroActor != null && m_heroActor.GetEntityDef() != null && Options.GetInRankedPlayMode())
		{
			string heroCardID = m_heroActor.GetEntityDef().GetCardId();
			if (m_heroActor.GetEntityDef().GetCardSet() == TAG_CARD_SET.HERO_SKINS)
			{
				heroCardID = CollectionManager.GetHeroCardId(m_heroActor.GetEntityDef().GetClass(), CardHero.HeroType.VANILLA);
			}
			RankedWinsPlate component = m_rankedWinsPlate.GetComponent<RankedWinsPlate>();
			component.TooltipString = GameStrings.Get("GLUE_TOOLTIP_GOLDEN_WINS_DESC");
			Achievement unlockGoldenHeroAchievement = AchieveManager.Get().GetUnlockGoldenHeroAchievement(heroCardID, TAG_PREMIUM.GOLDEN);
			Achievement unlockPremiumHeroAchievement = AchieveManager.Get().GetUnlockPremiumHeroAchievement(m_heroActor.GetEntityDef().GetClass());
			int num = unlockGoldenHeroAchievement?.Progress ?? 0;
			int num2 = unlockGoldenHeroAchievement?.MaxProgress ?? 0;
			if (unlockGoldenHeroAchievement != null && unlockGoldenHeroAchievement.IsCompleted())
			{
				num = unlockPremiumHeroAchievement?.Progress ?? num;
				num2 = unlockPremiumHeroAchievement?.MaxProgress ?? num2;
				component.TooltipString = GameStrings.Format("GLUE_TOOLTIP_ALTERNATE_WINS_DESC", num2);
			}
			if (num == 0)
			{
				m_rankedWinsPlate.SetActive(value: false);
				return false;
			}
			if (num >= num2)
			{
				m_rankedWins.Text = GameStrings.Format(UniversalInputManager.UsePhoneUI ? "GLOBAL_HERO_WINS_PAST_MAX_PHONE" : "GLOBAL_HERO_WINS_PAST_MAX", num);
				component.TooltipEnabled = false;
			}
			else
			{
				m_rankedWins.Text = GameStrings.Format(UniversalInputManager.UsePhoneUI ? "GLOBAL_HERO_WINS_PHONE" : "GLOBAL_HERO_WINS", num, num2);
				component.TooltipEnabled = true;
			}
			m_rankedWinsPlate.SetActive(value: true);
			return true;
		}
		m_rankedWinsPlate.SetActive(value: false);
		return false;
	}

	public override void OnServerGameStarted()
	{
		base.OnServerGameStarted();
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.ADVENTURE)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			AdventureData.Adventuresubscene currentSubScene = adventureConfig.CurrentSubScene;
			if (currentSubScene == AdventureData.Adventuresubscene.MISSION_DECK_PICKER && DemoMgr.Get().GetMode() != DemoMode.BLIZZCON_2015)
			{
				adventureConfig.SubSceneGoBack(fireevent: false);
			}
		}
	}

	public override void HandleGameStartupFailure()
	{
		base.HandleGameStartupFailure();
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.ADVENTURE:
			if (AdventureConfig.Get().CurrentSubScene == AdventureData.Adventuresubscene.PRACTICE)
			{
				PracticePickerTrayDisplay.Get().OnGameDenied();
			}
			break;
		case SceneMgr.Mode.TOURNAMENT:
			if (PresenceMgr.Get().CurrentStatus == Global.PresenceStatus.PLAY_QUEUE)
			{
				PresenceMgr.Get().SetPrevStatus();
			}
			break;
		}
	}

	public void SetHeroDetailsTrayToIgnoreFullScreenEffects(bool ignoreEffects)
	{
		if (!(m_hierarchyDetails == null))
		{
			if (ignoreEffects)
			{
				SceneUtils.ReplaceLayer(m_hierarchyDetails, GameLayer.IgnoreFullScreenEffects, m_defaultDetailsLayer);
			}
			else
			{
				SceneUtils.ReplaceLayer(m_hierarchyDetails, m_defaultDetailsLayer, GameLayer.IgnoreFullScreenEffects);
			}
		}
	}

	public void ShowClickedStandardDeckInClassicPopup()
	{
		if ((SceneMgr.Get().GetMode() != SceneMgr.Mode.TOURNAMENT && SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING) || !(m_switchFormatPopup == null) || !(m_innkeeperQuote == null))
		{
			return;
		}
		if (!m_switchFormatButton.IsCovered())
		{
			Action<int> b = delegate
			{
				m_switchFormatPopup = null;
			};
			m_switchFormatPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_INTRO, m_Switch_Format_Notification_Bone.position, m_Switch_Format_Notification_Bone.localScale, GameStrings.Get("GLUE_TOURNAMENT_SWITCH_TO_STANDARD"));
			if (m_switchFormatPopup != null)
			{
				Notification.PopUpArrowDirection direction = (UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.RightUp : Notification.PopUpArrowDirection.Up);
				m_switchFormatPopup.ShowPopUpArrow(direction);
				Notification switchFormatPopup = m_switchFormatPopup;
				switchFormatPopup.OnFinishDeathState = (Action<int>)Delegate.Combine(switchFormatPopup.OnFinishDeathState, b);
			}
		}
		Action<int> finishCallback = delegate
		{
			if (m_switchFormatButton != null)
			{
				NotificationManager.Get().DestroyNotification(m_switchFormatPopup, 0f);
			}
			m_innkeeperQuote = null;
		};
		m_innkeeperQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_INTRO, INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_STANDARD_DECK_WARNING"), "", 0f, finishCallback);
	}

	public void ShowClickedWildDeckInClassicPopup()
	{
		ShowClickedWildDeckInStandardPopup();
	}

	public void ShowClickedWildDeckInStandardPopup()
	{
		if ((SceneMgr.Get().GetMode() != SceneMgr.Mode.TOURNAMENT && SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING) || !(m_switchFormatPopup == null) || !(m_innkeeperQuote == null))
		{
			return;
		}
		if (!m_switchFormatButton.IsCovered())
		{
			StopCoroutine("ShowSwitchToWildTutorialAfterTransitionsComplete");
			Action<int> b = delegate
			{
				m_switchFormatPopup = null;
			};
			m_switchFormatPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_INTRO, m_Switch_Format_Notification_Bone.position, m_Switch_Format_Notification_Bone.localScale, GameStrings.Get("GLUE_TOURNAMENT_SWITCH_TO_WILD"));
			if (m_switchFormatPopup != null)
			{
				Notification.PopUpArrowDirection direction = (UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.RightUp : Notification.PopUpArrowDirection.Up);
				m_switchFormatPopup.ShowPopUpArrow(direction);
				Notification switchFormatPopup = m_switchFormatPopup;
				switchFormatPopup.OnFinishDeathState = (Action<int>)Delegate.Combine(switchFormatPopup.OnFinishDeathState, b);
			}
		}
		Action<int> finishCallback = delegate
		{
			if (m_switchFormatButton != null)
			{
				NotificationManager.Get().DestroyNotification(m_switchFormatPopup, 0f);
			}
			m_innkeeperQuote = null;
		};
		m_innkeeperQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_INTRO, INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_WILD_DECK_WARNING"), "VO_INNKEEPER_Male_Dwarf_SetRotation_32.prefab:3377790e79f276a4484ed43edde342c4", 0f, finishCallback);
	}

	public void ShowClickedClassicDeckInNonClassicPopup()
	{
		if ((SceneMgr.Get().GetMode() != SceneMgr.Mode.TOURNAMENT && SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING) || !(m_switchFormatPopup == null) || !(m_innkeeperQuote == null))
		{
			return;
		}
		Action<int> finishCallback = delegate
		{
			if (m_switchFormatButton != null)
			{
				NotificationManager.Get().DestroyNotification(m_switchFormatPopup, 0f);
			}
			m_innkeeperQuote = null;
		};
		m_innkeeperQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_INTRO, INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_CLASSIC_DECK_WARNING"), "VO_Innkeeper_Male_Dwarf_ClassicMode_02.prefab:8cf46784be9929d4d84c40dc428df680", 0f, finishCallback);
	}

	public void ShowSwitchToWildTutorialIfNecessary()
	{
		if (!(m_switchFormatPopup != null) && UserAttentionManager.CanShowAttentionGrabber(UserAttentionBlocker.SET_ROTATION_INTRO, "DeckPickerTrayDisplay.ShowSwitchToWildTutorialIfNecessary"))
		{
			if (Options.GetFormatType() == PegasusShared.FormatType.FT_WILD)
			{
				Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_CREATE_DECK, val: false);
				Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_PLAY_SCREEN, val: false);
			}
			bool flag = false;
			SceneMgr.Mode mode = SceneMgr.Get().GetMode();
			if (Options.Get().GetBool(Option.SHOW_SWITCH_TO_WILD_ON_CREATE_DECK) && mode == SceneMgr.Mode.COLLECTIONMANAGER)
			{
				flag = true;
				Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_CREATE_DECK, val: false);
			}
			if (Options.Get().GetBool(Option.SHOW_SWITCH_TO_WILD_ON_PLAY_SCREEN) && mode == SceneMgr.Mode.TOURNAMENT)
			{
				flag = true;
				Options.Get().SetBool(Option.SHOW_SWITCH_TO_WILD_ON_PLAY_SCREEN, val: false);
			}
			if (flag)
			{
				StartCoroutine("ShowSwitchToWildTutorialAfterTransitionsComplete");
			}
		}
	}

	private IEnumerator ShowSwitchToWildTutorialAfterTransitionsComplete()
	{
		yield return new WaitForSeconds(1f);
		m_switchFormatPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_INTRO, m_Switch_Format_Notification_Bone, GameStrings.Get("GLUE_TOURNAMENT_SWITCH_TO_WILD"));
		Notification.PopUpArrowDirection direction = (UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.RightUp : Notification.PopUpArrowDirection.Up);
		m_switchFormatPopup.ShowPopUpArrow(direction);
		m_switchFormatPopup.PulseReminderEveryXSeconds(3f);
		NotificationManager.Get().DestroyNotification(m_switchFormatPopup, 6f);
	}

	public void SkipHeroSelectionAndCloseTray()
	{
		if (m_playButton != null)
		{
			m_backButton.RemoveEventListener(UIEventType.RELEASE, OnBackButtonReleased);
			m_playButton.RemoveEventListener(UIEventType.RELEASE, OnPlayGameButtonReleased);
		}
		SetPlayButtonEnabled(enable: false);
		Navigation.RemoveHandler(OnNavigateBack);
		if (m_slidingTray != null)
		{
			m_slidingTray.ToggleTraySlider(show: false);
		}
		if (HeroPickerDisplay.Get() != null)
		{
			HeroPickerDisplay.Get().HideTray(UniversalInputManager.UsePhoneUI ? 0.25f : 0f);
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null && !collectionManagerDisplay.GetHeroPickerDisplay().IsShown())
		{
			CollectionManager.Get().GetCollectibleDisplay().EnableInput(enable: false);
		}
		CollectionDeckTray.Get().RegisterModeSwitchedListener(OnModeSwitchedAfterSkippingHeroSelection);
	}

	public void ShowBonusStarsPopup()
	{
		OnPopupShown();
		DialogManager.Get().ShowBonusStarsPopup(GetBonusStarsPopupDataModel(), PlayEnterModeDialogues);
	}

	private bool ShouldShowBonusStarsPopUp()
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.LOGIN && (SceneMgr.Get().GetMode() != SceneMgr.Mode.TOURNAMENT || SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY))
		{
			return false;
		}
		if (m_currentMedalInfo.starsPerWin < 2)
		{
			return false;
		}
		int seasonId = m_currentMedalInfo.seasonId;
		int rankedIntroSeenRequirement = m_currentMedalInfo.LeagueConfig.RankedIntroSeenRequirement;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_SEASON_BONUS_STARS_POPUP_SEEN, out m_lastSeasonBonusStarPopUpSeen);
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_BONUS_STARS_POPUP_SEEN_COUNT, out m_bonusStarsPopUpSeenCount);
		if (m_lastSeasonBonusStarPopUpSeen < seasonId && m_bonusStarsPopUpSeenCount < rankedIntroSeenRequirement)
		{
			return true;
		}
		return false;
	}

	private void OnModeSwitchedAfterSkippingHeroSelection()
	{
		CollectionDeckTray.Get().UnregisterModeSwitchedListener(OnModeSwitchedAfterSkippingHeroSelection);
		CollectionManager.Get().GetCollectibleDisplay().EnableInput(enable: true);
	}

	protected override IEnumerator InitDeckDependentElements()
	{
		Log.PlayModeInvestigation.PrintInfo("DeckPickerTrayDisplay.InitDeckDependentElements() called");
		bool flag = IsChoosingHero();
		DeckPickerMode defaultDeckPickerMode = (m_deckPickerMode = DeckPickerMode.CUSTOM);
		m_numPagesToShow = 1;
		m_basicDeckPageContainer.gameObject.SetActive(flag);
		if (!flag)
		{
			while (!NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheDecks>())
			{
				yield return null;
			}
			CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded();
			while (!CollectionManager.Get().AreAllDeckContentsReady())
			{
				yield return null;
			}
			m_usingSharedDecks = FriendChallengeMgr.Get().ShouldUseSharedDecks();
			m_deckPickerMode = (m_usingSharedDecks ? DeckPickerMode.CUSTOM : defaultDeckPickerMode);
			UpdateDeckShareRequestButton();
			List<CollectionDeck> list = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK);
			if (FriendChallengeMgr.Get().IsChallengeFriendlyDuel)
			{
				list = ((!m_usingSharedDecks) ? list.FindAll((CollectionDeck deck) => deck.IsValidForFormat(FriendChallengeMgr.Get().GetFormatType())) : FriendChallengeMgr.Get().GetSharedDecks());
			}
			SetupDeckPages(list);
		}
		if (m_rankedPlayDisplay != null)
		{
			VisualsFormatType currentVisualsFormatType = VisualsFormatTypeExtensions.GetCurrentVisualsFormatType();
			UpdateRankedPlayDisplay(currentVisualsFormatType);
		}
		InitSwitchFormatButton();
		yield return StartCoroutine(base.InitDeckDependentElements());
	}

	private void SetupDeckPages(List<CollectionDeck> decks)
	{
		m_numPagesToShow = Mathf.CeilToInt((float)decks.Count / 9f);
		m_numPagesToShow = Mathf.Max(m_numPagesToShow, 1);
		Log.PlayModeInvestigation.PrintInfo($"DeckPickerTrayDisplay.SetupDeckPages() called. m_numPagesToShow={m_numPagesToShow}, decks.Count={decks.Count}");
		InitDeckPages();
		SetPageDecks(decks);
		UpdateDeckVisuals();
	}

	private void UpdateDeckVisuals()
	{
		for (int i = 0; i < m_customPages.Count; i++)
		{
			m_customPages[i].UpdateDeckVisuals();
		}
	}

	protected override void InitForMode(SceneMgr.Mode mode)
	{
		m_missingClassicDeck.SetActive(value: false);
		switch (mode)
		{
		case SceneMgr.Mode.TOURNAMENT:
			m_rankedPlayDisplayWidget = WidgetInstance.Create(UniversalInputManager.UsePhoneUI ? "RankedPlayDisplay_phone.prefab:22b0793a4bc044e47a1948619c2aa896" : "RankedPlayDisplay.prefab:1f884a817dbbdd84b9f8713dc21759f1");
			m_rankedPlayDisplayWidget.RegisterReadyListener(delegate
			{
				OnRankedPlayDisplayWidgetReady();
			});
			SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
			ChangePlayButtonTextAlpha();
			UpdateRankedClassWinsPlate();
			UpdatePageArrows();
			if (Options.GetFormatType() == PegasusShared.FormatType.FT_CLASSIC && CollectionManager.Get().GetNumberOfClassicDecks() == 0)
			{
				m_missingClassicDeck.SetActive(value: true);
			}
			break;
		case SceneMgr.Mode.TAVERN_BRAWL:
			SetHeaderForTavernBrawl();
			break;
		}
		UnityEngine.Vector2 keyholeTextureOffsets = new UnityEngine.Vector2(0f, 0f);
		m_currentModeTextures = m_collectionTextures;
		switch (mode)
		{
		case SceneMgr.Mode.ADVENTURE:
			m_currentModeTextures = m_adventureTextures;
			keyholeTextureOffsets.x = 0.5f;
			break;
		case SceneMgr.Mode.COLLECTIONMANAGER:
			m_currentModeTextures = m_collectionTextures;
			break;
		case SceneMgr.Mode.TAVERN_BRAWL:
			m_currentModeTextures = m_tavernBrawlTextures;
			keyholeTextureOffsets.x = 0.5f;
			keyholeTextureOffsets.y = 0.61f;
			break;
		case SceneMgr.Mode.TOURNAMENT:
			m_currentModeTextures = m_tournamentTextures;
			break;
		case SceneMgr.Mode.FIRESIDE_GATHERING:
			if (!FiresideGatheringManager.Get().InBrawlMode())
			{
				goto case SceneMgr.Mode.FRIENDLY;
			}
			goto IL_017a;
		case SceneMgr.Mode.FRIENDLY:
			{
				if (!FriendChallengeMgr.Get().IsChallengeTavernBrawl())
				{
					m_currentModeTextures = m_friendlyTextures;
					keyholeTextureOffsets.y = 0.61f;
					break;
				}
				goto IL_017a;
			}
			IL_017a:
			m_currentModeTextures = m_tavernBrawlTextures;
			keyholeTextureOffsets.x = 0.5f;
			keyholeTextureOffsets.y = 0.61f;
			break;
		}
		VisualsFormatType currentVisualsFormatType = VisualsFormatTypeExtensions.GetCurrentVisualsFormatType();
		Texture textureForFormat = m_currentModeTextures.GetTextureForFormat(currentVisualsFormatType);
		Texture customTextureForFormat = m_currentModeTextures.GetCustomTextureForFormat(currentVisualsFormatType);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (SceneMgr.Mode.TOURNAMENT != mode)
			{
				m_detailsTrayFrame.GetComponent<MeshFilter>().mesh = m_alternateDetailsTrayMesh;
			}
			SetPhoneDetailsTrayTextures(textureForFormat, textureForFormat);
		}
		else
		{
			SetTrayFrameAndBasicDeckPageTextures(textureForFormat, textureForFormat);
		}
		SetCustomDeckPageTextures(customTextureForFormat, customTextureForFormat);
		SetKeyholeTextureOffsets(keyholeTextureOffsets);
		UpdateDeckVisuals();
		base.InitForMode(mode);
	}

	private PegasusShared.GameType GetGameTypeForNewPlayModeGame()
	{
		if (!Options.GetInRankedPlayMode())
		{
			return PegasusShared.GameType.GT_CASUAL;
		}
		return PegasusShared.GameType.GT_RANKED;
	}

	private PegasusShared.FormatType GetFormatTypeForNewPlayModeGame()
	{
		if (GetGameTypeForNewPlayModeGame() == PegasusShared.GameType.GT_CASUAL)
		{
			return GetSelectedCollectionDeck()?.FormatType ?? PegasusShared.FormatType.FT_STANDARD;
		}
		return Options.GetFormatType();
	}

	private void UpdateFormat_Tournament(VisualsFormatType newVisualsFormatType)
	{
		Options.GetFormatType();
		bool num = CollectionManager.Get().ShouldAccountSeeStandardWild();
		SetPlayButtonText(GameStrings.Get("GLOBAL_PLAY"));
		if (num)
		{
			m_switchFormatButton.SetVisualsFormatType(newVisualsFormatType);
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && SetRotationManager.HasSeenStandardModeTutorial())
			{
				if (newVisualsFormatType == VisualsFormatType.VFT_WILD && !Options.Get().GetBool(Option.HAS_SEEN_WILD_MODE_VO) && UserAttentionManager.CanShowAttentionGrabber("DeckPickerTrayDisplay.UpdateFormat_Tournament:" + Option.HAS_SEEN_WILD_MODE_VO))
				{
					HideSetRotationNotifications();
					NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_WILD_GAME"), "VO_INNKEEPER_Male_Dwarf_SetRotation_35.prefab:db2f6e3818fa49b4d8423121eba762f6");
					Options.Get().SetBool(Option.HAS_SEEN_WILD_MODE_VO, val: true);
				}
				if (newVisualsFormatType == VisualsFormatType.VFT_CLASSIC && !Options.Get().GetBool(Option.HAS_SEEN_CLASSIC_MODE_VO) && UserAttentionManager.CanShowAttentionGrabber("DeckPickerTrayDisplay.UpdateFormat_Tournament:" + Option.HAS_SEEN_CLASSIC_MODE_VO))
				{
					HideSetRotationNotifications();
					NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_CLASSIC_TAKES_YOU_BACK_ORIGINAL_HEARTHSTONE"), "VO_Innkeeper_Male_Dwarf_ClassicMode_06.prefab:f91da6f7e66fd754fb4e568d15d49116");
					Options.Get().SetBool(Option.HAS_SEEN_CLASSIC_MODE_VO, val: true);
				}
			}
			if (m_selectedCustomDeckBox != null && !m_selectedCustomDeckBox.IsValidForCurrentMode())
			{
				Deselect();
			}
			UpdateCustomTournamentBackgroundAndDecks();
		}
		ChangePlayButtonTextAlpha();
		UpdateRankedClassWinsPlate();
		UpdateRankedPlayDisplay(newVisualsFormatType);
	}

	private void ChangePlayButtonTextAlpha()
	{
		if (m_playButton != null)
		{
			if (m_playButton.IsEnabled())
			{
				m_playButton.m_newPlayButtonText.TextAlpha = 1f;
			}
			else
			{
				m_playButton.m_newPlayButtonText.TextAlpha = 0f;
			}
		}
	}

	private void UpdateRankedPlayDisplay(VisualsFormatType newVisualsFormatType)
	{
		if (!newVisualsFormatType.IsRanked())
		{
			m_casualPlayDisplayWidget.Show();
			m_rankedPlayDisplay.Hide();
			return;
		}
		m_casualPlayDisplayWidget.Hide();
		m_rankedPlayDisplay.Show();
		m_rankedPlayDisplay.UpdateMode(newVisualsFormatType);
		RankedRewardInfoButton componentInChildren = m_rankedPlayDisplay.GetComponentInChildren<RankedRewardInfoButton>();
		if (!(componentInChildren != null))
		{
			return;
		}
		TournamentDisplay tournamentDisplay = TournamentDisplay.Get();
		if (!(tournamentDisplay == null))
		{
			NetCache.NetCacheMedalInfo currentMedalInfo = tournamentDisplay.GetCurrentMedalInfo();
			if (currentMedalInfo != null)
			{
				MedalInfoTranslator mit = new MedalInfoTranslator(currentMedalInfo);
				componentInChildren.Initialize(mit);
			}
		}
	}

	private void UpdateFormat_CollectionManager()
	{
		PegasusShared.FormatType formatType = Options.GetFormatType();
		bool inRankedPlayMode = Options.GetInRankedPlayMode();
		if (formatType == PegasusShared.FormatType.FT_WILD && !m_HasSeenPlayStandardToWildVO)
		{
			m_HasSeenPlayStandardToWildVO = true;
			m_HasSeenPlayStandardToClassicVO = false;
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get("VO_INNKEEPER_PLAY_STANDARD_TO_WILD"), "VO_INNKEEPER_Male_Dwarf_SetRotation_43.prefab:4b4ce858139927946905ec0d40d5b3c1");
		}
		else if (formatType == PegasusShared.FormatType.FT_CLASSIC && !m_HasSeenPlayStandardToClassicVO)
		{
			m_HasSeenPlayStandardToClassicVO = true;
			m_HasSeenPlayStandardToWildVO = false;
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get("VO_INNKEEPER_CLASSIC_PLAY_CLASSIC_MODE_ONLY"), "VO_Innkeeper_Male_Dwarf_ClassicMode_01.prefab:5ac6a7a19130d8c4795330b7a8693513");
		}
		else if (formatType == PegasusShared.FormatType.FT_STANDARD)
		{
			m_HasSeenPlayStandardToClassicVO = false;
			m_HasSeenPlayStandardToWildVO = false;
		}
		StartCoroutine(InitModeWhenReady());
		m_switchFormatButton.SetVisualsFormatType(VisualsFormatTypeExtensions.ToVisualsFormatType(formatType, inRankedPlayMode));
		TransitionToFormatType(formatType, inRankedPlayMode);
	}

	private void UpdateCustomTournamentBackgroundAndDecks()
	{
		TransitionToFormatType(Options.GetFormatType(), Options.GetInRankedPlayMode());
		foreach (CustomDeckPage customPage in m_customPages)
		{
			customPage.UpdateDeckVisuals();
		}
	}

	private IEnumerator InitButtonAchievements()
	{
		List<Achievement> unlockHeroAchieves = AchieveManager.Get().GetAchievesInGroup(Achieve.Type.HERO);
		UpdateCollectionButtonGlow();
		foreach (Achievement achievement2 in unlockHeroAchieves)
		{
			HeroPickerButton heroPickerButton = m_heroButtons.Find((HeroPickerButton obj) => obj.GetEntityDef().GetClass() == achievement2.ClassReward.Value);
			if (heroPickerButton == null)
			{
				if (m_validClasses.Contains(achievement2.ClassReward.Value))
				{
					Debug.LogWarning($"DeckPickerTrayDisplay.InitButtonAchievements() - could not find hero picker button matching UnlockHeroAchievement with class {achievement2.ClassReward.Value}");
				}
				continue;
			}
			if (achievement2.ClassReward.Value == TAG_CLASS.MAGE)
			{
				achievement2.AckCurrentProgressAndRewardNotices();
			}
			heroPickerButton.SetProgress(achievement2.AcknowledgedProgress, achievement2.Progress, achievement2.MaxProgress, shouldAnimate: false);
			if (IsChoosingHero())
			{
				CollectionManager.PreconDeck preconDeck = CollectionManager.Get().GetPreconDeck(achievement2.ClassReward.Value);
				long num = 0L;
				if (preconDeck != null)
				{
					num = preconDeck.ID;
				}
				heroPickerButton.SetPreconDeckID(num);
				if (achievement2.IsCompleted() && num == 0L)
				{
					Debug.LogError($"DeckPickerTrayDisplay.InitButtonAchievements() - preconDeckID = 0 for achievement {achievement2}");
				}
				SceneMgr.Mode mode = SceneMgr.Get().GetMode();
				if (mode == SceneMgr.Mode.TAVERN_BRAWL || (mode == SceneMgr.Mode.FRIENDLY && FriendChallengeMgr.Get().IsChallengeTavernBrawl()) || (mode == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().InBrawlMode()))
				{
					heroPickerButton.Unlock();
				}
			}
		}
		if (DemoMgr.Get().IsDemo())
		{
			foreach (HeroPickerButton heroButton in m_heroButtons)
			{
				if (!DemoMgr.Get().IsHeroClassPlayable(heroButton.m_heroClass))
				{
					Collider[] componentsInChildren = heroButton.GetComponentsInChildren<Collider>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].enabled = false;
					}
					heroButton.Lock();
					heroButton.Activate(enable: false);
				}
			}
		}
		while (m_delayButtonAnims)
		{
			yield return null;
		}
		foreach (Achievement achievement in unlockHeroAchieves)
		{
			HeroPickerButton heroPickerButton2 = m_heroButtons.Find((HeroPickerButton obj) => obj.GetEntityDef().GetClass() == achievement.ClassReward.Value);
			if (heroPickerButton2 == null)
			{
				if (m_validClasses.Contains(achievement.ClassReward.Value))
				{
					Debug.LogWarning($"DeckPickerTrayDisplay.InitButtonAchievements() - could not find hero picker button matching UnlockHeroAchievement with class {achievement.ClassReward.Value}");
				}
				continue;
			}
			if (!IsChoosingHero() && heroPickerButton2.GetPreconDeckID() != 0L)
			{
				heroPickerButton2.SetProgress(achievement.AcknowledgedProgress, achievement.Progress, achievement.MaxProgress);
			}
			achievement.AckCurrentProgressAndRewardNotices();
		}
	}

	protected override void SetHeaderForTavernBrawl()
	{
		if (m_labelDecoration != null)
		{
			m_labelDecoration.SetActive(value: false);
		}
		base.SetHeaderForTavernBrawl();
	}

	protected override void InitHeroPickerButtons()
	{
		base.InitHeroPickerButtons();
		CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK);
		m_heroDefsLoading = m_validClasses.Count;
		for (int i = 0; i < m_validClasses.Count; i++)
		{
			if (i >= m_heroButtons.Count || m_heroButtons[i] == null)
			{
				Debug.LogWarning("InitHeroPickerButtons: not enough buttons for total guest heroes.");
				break;
			}
			HeroPickerButton heroPickerButton = m_heroButtons[i];
			heroPickerButton.Lock();
			heroPickerButton.SetProgress(0, 0, 1);
			TAG_CLASS tAG_CLASS = m_validClasses[i];
			NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(tAG_CLASS);
			if (favoriteHero == null)
			{
				if (tAG_CLASS != TAG_CLASS.WHIZBANG)
				{
					Debug.LogWarning(string.Concat("Couldn't find Favorite Hero for hero class: ", tAG_CLASS, " defaulting to Normal Vanilla Hero!"));
				}
				string heroCardId = CollectionManager.GetHeroCardId(tAG_CLASS, CardHero.HeroType.VANILLA);
				HeroFullDefLoadedCallbackData userData = new HeroFullDefLoadedCallbackData(heroPickerButton, TAG_PREMIUM.NORMAL);
				DefLoader.Get().LoadFullDef(heroCardId, OnHeroFullDefLoaded, userData);
			}
			else
			{
				HeroFullDefLoadedCallbackData userData2 = new HeroFullDefLoadedCallbackData(heroPickerButton, favoriteHero.Premium);
				DefLoader.Get().LoadFullDef(favoriteHero.Name, OnHeroFullDefLoaded, userData2);
			}
			if (IsChoosingHero())
			{
				heroPickerButton.SetDivotTexture(m_currentModeTextures.classDivotTex);
			}
			else
			{
				heroPickerButton.SetDivotTexture(m_currentModeTextures.guestHeroDivotTex);
			}
		}
		if (IsChoosingHeroForDungeonCrawlAdventure())
		{
			SetUpHeroCrowns();
		}
	}

	private void InitDeckPages()
	{
		Log.PlayModeInvestigation.PrintInfo($"DeckPickerTrayDisplay.InitDeckPages() called. m_numPagesToShow={m_numPagesToShow}, m_customPages.Count={m_customPages.Count}");
		if (m_numPagesToShow <= 0)
		{
			Debug.LogWarning("DeckPickerTrayDisplay.InitDeckPages() called with invalid amount of pages");
			return;
		}
		while (m_numPagesToShow > m_customPages.Count)
		{
			GameObject obj = AssetLoader.Get().InstantiatePrefab(CUSTOM_DECK_PAGE);
			obj.transform.SetParent(m_customDeckPagesRoot.transform, worldPositionStays: false);
			obj.transform.localPosition = ((m_customPages.Count == 0) ? m_customDeckPageUpperBone.transform.localPosition : m_customDeckPageLowerBone.transform.localPosition);
			CustomDeckPage component = obj.GetComponent<CustomDeckPage>();
			component.SetDeckButtonCallback(OnCustomDeckPressed);
			component.SetDecks(new List<CollectionDeck>());
			m_customPages.Add(component);
			Log.PlayModeInvestigation.PrintInfo($"DeckPickerTrayDisplay.InitDeckPages() -- Deck page added. New total: {m_customPages.Count}");
		}
		while (m_numPagesToShow < m_customPages.Count - 1)
		{
			UnityEngine.Object.Destroy(m_customPages[m_customPages.Count - 1]);
			m_customPages.Remove(m_customPages[m_customPages.Count - 1]);
			Log.PlayModeInvestigation.PrintInfo($"DeckPickerTrayDisplay.InitDeckPages() -- Deck page removed. New total: {m_customPages.Count}");
		}
	}

	private void OpponentDecksSlidingTrayToggledListener(bool shown)
	{
		if (shown)
		{
			m_collectionButtonGlow.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		else
		{
			m_collectionButtonGlow.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
	}

	private void SetPageDecks(List<CollectionDeck> decks)
	{
		if (m_customPages == null)
		{
			Debug.LogError("{0}.UpdateCustomPages(): m_customPages is null. Make sure you call InitCustomPages() first!", this);
		}
		foreach (CustomDeckPage customPage in m_customPages)
		{
			int count = Mathf.Min(decks.Count, customPage.m_maxCustomDecksToDisplay);
			List<CollectionDeck> range = decks.GetRange(0, count);
			customPage.SetDecks(range);
			customPage.InitCustomDecks();
			foreach (CollectionDeck item in range)
			{
				string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(item.HeroCardID);
				if (string.IsNullOrEmpty(heroPowerCardIdFromHero))
				{
					Debug.LogErrorFormat("No hero power set up for hero {0}", item.HeroCardID);
				}
				else
				{
					LoadHeroPowerDef(heroPowerCardIdFromHero);
				}
			}
			decks.RemoveRange(0, count);
			if (decks.Count <= 0)
			{
				break;
			}
		}
		if (decks.Count > 0)
		{
			Debug.LogWarningFormat("DeckPickerTrayDisplay - {0} more decks than we can display!", decks.Count);
		}
	}

	private void InitMode()
	{
		if (IsChoosingHero())
		{
			ShowFirstPage(skipTraySlidingAnimation: true);
		}
		else
		{
			SetSelectionAndPageFromOptions();
		}
		InitExpoDemoMode();
		ShowSwitchToWildTutorialIfNecessary();
	}

	private void InitExpoDemoMode()
	{
		if (DemoMgr.Get().IsExpoDemo())
		{
			UpdatePageArrows();
			SetBackButtonEnabled(enable: false);
			StartCoroutine("ShowDemoQuotes");
		}
	}

	private IEnumerator ShowDemoQuotes()
	{
		string str = Vars.Key("Demo.ThankQuote").GetStr("");
		int @int = Vars.Key("Demo.ThankQuoteMsTime").GetInt(0);
		str = str.Replace("\\n", "\n");
		if (!string.IsNullOrEmpty(str) && @int > 0)
		{
			if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2015)
			{
				m_expoThankQuote = NotificationManager.Get().CreateCharacterQuote("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", new Vector3(0f, NotificationManager.DEPTH, 0f), str, "", allowRepeatDuringSession: true, (float)@int / 1000f, null, CanvasAnchor.CENTER);
			}
			else
			{
				m_expoThankQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(158.1f, NotificationManager.DEPTH, 80.2f), str, "", (float)@int / 1000f);
			}
			EnableClickBlocker(enable: true);
			yield return new WaitForSeconds((float)@int / 1000f);
			EnableClickBlocker(enable: false);
		}
		ShowIntroQuote();
	}

	private void ShowIntroQuote()
	{
		HideIntroQuote();
		string str = Vars.Key("Demo.IntroQuote").GetStr("");
		str = str.Replace("\\n", "\n");
		if (!string.IsNullOrEmpty(str))
		{
			if (DemoMgr.Get().GetMode() == DemoMode.BLIZZCON_2015)
			{
				m_expoIntroQuote = NotificationManager.Get().CreateCharacterQuote("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", new Vector3(0f, NotificationManager.DEPTH, -54.22f), str, "", allowRepeatDuringSession: true, 0f, null, CanvasAnchor.CENTER);
			}
			else
			{
				m_expoIntroQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(147.6f, NotificationManager.DEPTH, 23.1f), str, "");
			}
		}
	}

	private void EnableClickBlocker(bool enable)
	{
		if (!(m_clickBlocker == null))
		{
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			if (enable)
			{
				fullScreenFXMgr.SetBlurBrightness(1f);
				fullScreenFXMgr.SetBlurDesaturation(0f);
				fullScreenFXMgr.Vignette();
				fullScreenFXMgr.Blur(1f, 0.4f, iTween.EaseType.easeOutCirc);
			}
			else
			{
				fullScreenFXMgr.StopVignette();
				fullScreenFXMgr.StopBlur();
			}
			m_clickBlocker.gameObject.SetActive(enable);
		}
	}

	private void HideDemoQuotes()
	{
		DemoMgr demoMgr = DemoMgr.Get();
		if (demoMgr != null && !demoMgr.IsExpoDemo())
		{
			return;
		}
		StopCoroutine("ShowDemoQuotes");
		if (m_expoThankQuote != null)
		{
			NotificationManager notificationManager = NotificationManager.Get();
			if (notificationManager != null)
			{
				notificationManager.DestroyNotification(m_expoThankQuote, 0f);
			}
			m_expoThankQuote = null;
			FullScreenFXMgr fullScreenFXMgr = FullScreenFXMgr.Get();
			if (fullScreenFXMgr != null)
			{
				fullScreenFXMgr.StopVignette();
				fullScreenFXMgr.StopBlur();
			}
		}
		HideIntroQuote();
	}

	private void HideIntroQuote()
	{
		if (m_expoIntroQuote != null)
		{
			NotificationManager.Get().DestroyNotification(m_expoIntroQuote, 0f);
			m_expoIntroQuote = null;
		}
	}

	private void HideSetRotationNotifications()
	{
		if (m_innkeeperQuote != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_innkeeperQuote);
			m_innkeeperQuote = null;
		}
		if (m_switchFormatPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_switchFormatPopup);
			m_switchFormatPopup = null;
		}
	}

	private void OnTransitionFromGameplayFinished(bool cutoff, object userData)
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY && !FriendChallengeMgr.Get().HasChallenge())
		{
			GoBackUntilOnNavigateBackCalled();
		}
		LoadingScreen.Get().UnregisterFinishedTransitionListener(OnTransitionFromGameplayFinished);
		m_delayButtonAnims = false;
	}

	private void CollectionButtonPress(UIEvent e)
	{
		if (ShouldGlowCollectionButton())
		{
			if (!Options.Get().GetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_DECK) && HaveDecksThatNeedNames())
			{
				Options.Get().SetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_DECK, val: true);
			}
			else if (!Options.Get().GetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_CARD) && HaveUnseenCards())
			{
				Options.Get().SetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_CARD, val: true);
			}
			if (Options.Get().GetBool(Option.GLOW_COLLECTION_BUTTON_AFTER_SET_ROTATION) && SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
			{
				Options.Get().SetBool(Option.GLOW_COLLECTION_BUTTON_AFTER_SET_ROTATION, val: false);
			}
		}
		if (PracticePickerTrayDisplay.Get() != null && PracticePickerTrayDisplay.Get().IsShown())
		{
			Navigation.GoBack();
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			Navigation.Clear();
		}
		TelemetryWatcher.StopWatchingFor(TelemetryWatcherWatchType.CollectionManagerFromDeckPicker);
		TelemetryManager.Client().SendDeckPickerToCollection(DeckPickerToCollection.Path.DECK_PICKER_BUTTON);
		CollectionManager.Get().NotifyOfBoxTransitionStart();
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.COLLECTIONMANAGER);
	}

	private void RequestDeckShareButtonPress(UIEvent e)
	{
		if (m_doingDeckShareTransition)
		{
			return;
		}
		if (m_usingSharedDecks)
		{
			FriendChallengeMgr.Get().EndDeckShare();
		}
		else
		{
			if (!FriendChallengeMgr.Get().HasOpponentSharedDecks())
			{
				EnableRequestDeckShareButton(enable: false);
			}
			FriendChallengeMgr.Get().RequestDeckShare();
		}
		UpdateDeckShareTooltip();
	}

	private void RequestDeckShareButtonOver(UIEvent e)
	{
		m_isDeckShareRequestButtonHovered = true;
		UpdateDeckShareTooltip();
	}

	private void RequestDeckShareButtonOut(UIEvent e)
	{
		m_isDeckShareRequestButtonHovered = false;
		UpdateDeckShareTooltip();
	}

	private void EnableRequestDeckShareButton(bool enable)
	{
		if (m_DeckShareRequestButton.IsEnabled() != enable)
		{
			if (!enable)
			{
				m_DeckShareRequestButton.TriggerOut();
			}
			m_DeckShareRequestButton.SetEnabled(enable);
			m_DeckShareRequestButton.Flip(enable);
		}
		UpdateDeckShareRequestButton();
	}

	private void UpdateDeckShareRequestButton()
	{
		if (!(m_DeckShareRequestButton == null) && IsDeckSharingActive())
		{
			if (!FriendChallengeMgr.Get().HasOpponentSharedDecks())
			{
				m_DeckShareRequestButton.SetText(GameStrings.Get("GLUE_DECK_SHARE_BUTTON_BORROW_DECKS"));
			}
			else if (m_usingSharedDecks)
			{
				m_DeckShareRequestButton.SetText(GameStrings.Get("GLUE_DECK_SHARE_BUTTON_SHOW_MY_DECKS"));
			}
			else
			{
				m_DeckShareRequestButton.SetText(GameStrings.Format("GLUE_DECK_SHARE_BUTTON_SHOW_OPPONENT_DECKS"));
			}
			UpdateDeckShareTooltip();
		}
	}

	private void UpdateDeckShareTooltip()
	{
		if (m_DeckShareRequestButton == null)
		{
			return;
		}
		TooltipZone componentInChildren = m_DeckShareRequestButton.GetComponentInChildren<TooltipZone>();
		if (componentInChildren == null)
		{
			return;
		}
		if (!FriendChallengeMgr.Get().HasOpponentSharedDecks())
		{
			if (m_isDeckShareRequestButtonHovered && !componentInChildren.IsShowingTooltip())
			{
				string bestName = string.Empty;
				BnetPlayer myOpponent = FriendChallengeMgr.Get().GetMyOpponent();
				if (myOpponent != null)
				{
					bestName = myOpponent.GetBestName();
				}
				componentInChildren.ShowTooltip(GameStrings.Get("GLUE_DECK_SHARE_TOOLTIP_HEADER"), GameStrings.Format("GLUE_DECK_SHARE_TOOLTIP_BODY_REQUEST", bestName), 5f);
			}
			else if (!m_isDeckShareRequestButtonHovered && componentInChildren.IsShowingTooltip())
			{
				componentInChildren.HideTooltip();
			}
		}
		else if (componentInChildren.IsShowingTooltip())
		{
			componentInChildren.HideTooltip();
		}
	}

	private void OnDeckShareRequestCancelDeclineOrError()
	{
		StopCoroutine("WaitThanEnableRequestDeckShareButton");
		StartCoroutine("WaitThanEnableRequestDeckShareButton");
	}

	private IEnumerator WaitThanEnableRequestDeckShareButton()
	{
		yield return new WaitForSeconds(1f);
		EnableRequestDeckShareButton(enable: true);
	}

	public void UseSharedDecks(List<CollectionDeck> decks)
	{
		StartCoroutine(UseSharedDecksImpl(decks));
	}

	private IEnumerator UseSharedDecksImpl(List<CollectionDeck> decks)
	{
		if (m_usingSharedDecks || decks == null)
		{
			yield break;
		}
		m_doingDeckShareTransition = true;
		m_clickBlocker.gameObject.SetActive(value: true);
		m_usingSharedDecks = true;
		UpdateDeckShareRequestButton();
		Deselect();
		m_deckPickerMode = DeckPickerMode.CUSTOM;
		if (!string.IsNullOrEmpty(m_wildDeckTransitionSound))
		{
			SoundManager.Get().LoadAndPlay(m_wildDeckTransitionSound);
		}
		if (m_DeckShareGlowOutQuad != null)
		{
			m_DeckShareGlowOutQuad.SetActive(value: true);
			yield return StartCoroutine(FadeDeckShareGlowOutQuad(0f, m_DeckShareGlowOutIntensity, m_DeckShareTransitionTime * 0.5f));
		}
		if (m_DeckShareParticles != null)
		{
			m_DeckShareParticles.Stop();
			m_DeckShareParticles.Play();
		}
		SetupDeckPages(decks);
		m_basicDeckPageContainer.gameObject.SetActive(value: false);
		foreach (CollectionDeck deck in decks)
		{
			deck.Locked = false;
		}
		ShowFirstPage();
		if (m_DeckShareGlowOutQuad != null)
		{
			yield return StartCoroutine(FadeDeckShareGlowOutQuad(m_DeckShareGlowOutIntensity, 0f, m_DeckShareTransitionTime * 0.5f));
			m_DeckShareGlowOutQuad.SetActive(value: false);
		}
		EnableRequestDeckShareButton(enable: true);
		m_clickBlocker.gameObject.SetActive(value: false);
		m_doingDeckShareTransition = false;
	}

	public void StopUsingSharedDecks()
	{
		StartCoroutine(StopUsingSharedDecksImpl());
	}

	private IEnumerator StopUsingSharedDecksImpl()
	{
		if (m_usingSharedDecks)
		{
			m_clickBlocker.gameObject.SetActive(value: true);
			m_doingDeckShareTransition = true;
			m_usingSharedDecks = false;
			UpdateDeckShareRequestButton();
			Deselect();
			if (!string.IsNullOrEmpty(m_wildDeckTransitionSound))
			{
				SoundManager.Get().LoadAndPlay(m_wildDeckTransitionSound);
			}
			if (m_DeckShareGlowOutQuad != null)
			{
				m_DeckShareGlowOutQuad.SetActive(value: true);
				yield return StartCoroutine(FadeDeckShareGlowOutQuad(0f, m_DeckShareGlowOutIntensity, m_DeckShareTransitionTime * 0.5f));
			}
			if (m_DeckShareParticles != null)
			{
				m_DeckShareParticles.Stop();
				m_DeckShareParticles.Play();
			}
			List<CollectionDeck> decks = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).FindAll((CollectionDeck deck) => deck.IsValidForFormat(FriendChallengeMgr.Get().GetFormatType()));
			SetupDeckPages(decks);
			ShowFirstPage();
			if (m_DeckShareGlowOutQuad != null)
			{
				yield return StartCoroutine(FadeDeckShareGlowOutQuad(m_DeckShareGlowOutIntensity, 0f, m_DeckShareTransitionTime * 0.5f));
				m_DeckShareGlowOutQuad.SetActive(value: false);
			}
			EnableRequestDeckShareButton(enable: true);
			m_doingDeckShareTransition = false;
			m_clickBlocker.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator FadeDeckShareGlowOutQuad(float startingIntensity, float finalIntensity, float fadeTime)
	{
		if (!(m_DeckShareGlowOutQuad == null))
		{
			int propertyID = Shader.PropertyToID("_Intensity");
			float currentIntensity = startingIntensity;
			Material mat = m_DeckShareGlowOutQuad.GetComponentInChildren<MeshRenderer>(includeInactive: true).GetMaterial();
			mat.SetFloat(propertyID, currentIntensity);
			float transitionSpeed = Mathf.Abs(finalIntensity - startingIntensity) / fadeTime;
			while (currentIntensity != finalIntensity)
			{
				currentIntensity = Mathf.MoveTowards(currentIntensity, finalIntensity, transitionSpeed * Time.deltaTime);
				mat.SetFloat(propertyID, currentIntensity);
				yield return null;
			}
		}
	}

	private void SwitchFormatButtonPress(UIEvent e)
	{
		m_switchFormatButton.Disable();
		m_switchFormatButton.gameObject.SetActive(value: false);
		ShowFormatTypePickerPopup();
		TransitionToFormatType(PegasusShared.FormatType.FT_STANDARD, inRankedPlayMode: true);
	}

	public void ShowFormatTypePickerPopup()
	{
		if (m_showStandardComingSoonNotice)
		{
			BasicPopup.PopupInfo popupInfo = new BasicPopup.PopupInfo();
			popupInfo.m_responseCallback = OnStandardComingSoonResponse;
			popupInfo.m_prefabAssetRefs.Add(STANDARD_COMING_SOON_POPUP_NAME);
			DialogManager.Get().ShowStandardComingSoonPopup(UserAttentionBlocker.ALL_EXCEPT_FATAL_ERROR_SCENE, popupInfo);
			m_dimQuad.GetComponent<Renderer>().enabled = true;
			m_dimQuad.enabled = true;
			m_dimQuad.StopPlayback();
			m_dimQuad.Play("DimQuad_FadeIn", -1, 0.5f);
		}
		else
		{
			m_formatTypePickerWidget.transform.position = Vector3.zero;
			m_formatTypePickerWidget.Show();
			m_formatTypePickerWidget.TriggerEvent(m_inHeroPicker ? "OPEN_WITHOUT_CASUAL" : "OPEN_WITH_CASUAL", new Widget.TriggerEventParameters
			{
				Payload = (int)VisualsFormatTypeExtensions.GetCurrentVisualsFormatType()
			});
		}
	}

	public void ShowPopupDuringSetRotation(VisualsFormatType visualsFormatType)
	{
		m_formatTypePickerWidget.transform.position = Vector3.zero;
		m_formatTypePickerWidget.Show();
		m_formatTypePickerWidget.TriggerEvent("SETROTATION_OPEN_WITH_CASUAL", new Widget.TriggerEventParameters
		{
			Payload = (int)visualsFormatType
		});
	}

	private void OnFormatTypePickerEvent(string eventName)
	{
		switch (eventName)
		{
		case "HIDE":
		case "HIDE_WITH_CASUAL":
		case "HIDE_WITHOUT_CASUAL":
			FireFormatTypePickerClosedEvent();
			break;
		}
	}

	private void SwitchFormatTypeAndRankedPlayMode(VisualsFormatType newVisualsFormatType)
	{
		if (VisualsFormatTypeExtensions.ToVisualsFormatType(Options.GetFormatType(), Options.GetInRankedPlayMode()) != newVisualsFormatType)
		{
			Options.SetFormatType(newVisualsFormatType.ToFormatType());
			Options.SetInRankedPlayMode(newVisualsFormatType.IsRanked());
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
		{
			UpdateFormat_Tournament(newVisualsFormatType);
			TournamentDisplay.Get().UpdateHeaderText();
			m_rankedPlayDisplay.OnSwitchFormat(newVisualsFormatType);
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			UpdateCreateDeckText();
			UpdateFormat_CollectionManager();
		}
		m_missingClassicDeck.SetActive(value: false);
		if (newVisualsFormatType == VisualsFormatType.VFT_CLASSIC && SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && CollectionManager.Get().GetNumberOfClassicDecks() == 0)
		{
			m_missingClassicDeck.SetActive(value: true);
		}
		UpdatePageArrows();
		m_formatTypePickerWidget.TriggerEvent(m_inHeroPicker ? "HIDE_WITHOUT_CASUAL" : "HIDE_WITH_CASUAL", new Widget.TriggerEventParameters
		{
			Payload = (int)newVisualsFormatType
		});
		StartCoroutine(m_switchFormatButton.EnableWithDelay(0.8f));
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
		{
			if (ShouldShowRotatedBoosterPopup(newVisualsFormatType))
			{
				StartCoroutine(ShowRotatedBoostersPopup());
			}
			else if (ShouldShowStandardDeckVO(newVisualsFormatType))
			{
				StartCoroutine(ShowStandardDeckVO());
			}
		}
	}

	private void OnFormatTypeSwitchCancelled()
	{
		TransitionToFormatType(m_PreviousFormatType, m_PreviousInRankedPlayMode);
	}

	private void OnStandardComingSoonResponse(BasicPopup.Response response, object userData)
	{
		if (response == BasicPopup.Response.CUSTOM_RESPONSE)
		{
			string setRotationInfoLink = ExternalUrlService.Get().GetSetRotationInfoLink();
			Log.DeckTray.Print("Set Rotation web page URL: {0}", setRotationInfoLink);
			if (!string.IsNullOrEmpty(setRotationInfoLink))
			{
				Application.OpenURL(setRotationInfoLink);
			}
		}
		m_dimQuad.StopPlayback();
		m_dimQuad.Play("DimQuad_FadeOut");
	}

	public static bool OnNavigateBack()
	{
		if (Get() != null)
		{
			return Get().OnNavigateBackImplementation();
		}
		Debug.LogError("HeroPickerTrayDisplay: tried to navigate back but had null instance!");
		return false;
	}

	protected override bool OnNavigateBackImplementation()
	{
		if (!m_backButton.IsEnabled())
		{
			return false;
		}
		switch ((SceneMgr.Get() != null) ? SceneMgr.Get().GetMode() : SceneMgr.Mode.INVALID)
		{
		case SceneMgr.Mode.COLLECTIONMANAGER:
		case SceneMgr.Mode.TAVERN_BRAWL:
		case SceneMgr.Mode.FIRESIDE_GATHERING:
		{
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (CollectionDeckTray.Get() != null)
			{
				CollectionDeckTray.Get().GetDecksContent().CreateNewDeckCancelled();
			}
			if (Get() != null && !Get().m_heroChosen && collectionManagerDisplay != null)
			{
				collectionManagerDisplay.CancelSelectNewDeckHeroMode();
			}
			if (HeroPickerDisplay.Get() != null)
			{
				HeroPickerDisplay.Get().HideTray();
			}
			PresenceMgr.Get().SetPrevStatus();
			if (SceneMgr.Get().IsInTavernBrawlMode())
			{
				TavernBrawlDisplay.Get().EnablePlayButton();
			}
			if (collectionManagerDisplay != null)
			{
				DeckTemplatePicker deckTemplatePicker = (UniversalInputManager.UsePhoneUI ? collectionManagerDisplay.GetPhoneDeckTemplateTray() : collectionManagerDisplay.m_pageManager.GetDeckTemplatePicker());
				if (deckTemplatePicker != null)
				{
					Navigation.RemoveHandler(deckTemplatePicker.OnNavigateBack);
				}
			}
			break;
		}
		case SceneMgr.Mode.TOURNAMENT:
			BackOutToHub();
			GameMgr.Get().CancelFindGame();
			break;
		case SceneMgr.Mode.ADVENTURE:
			AdventureConfig.Get().SubSceneGoBack();
			if (AdventureConfig.Get().CurrentSubScene == AdventureData.Adventuresubscene.PRACTICE)
			{
				PracticePickerTrayDisplay.Get().gameObject.SetActive(value: false);
			}
			GameMgr.Get().CancelFindGame();
			break;
		}
		return base.OnNavigateBackImplementation();
	}

	protected override void GoBackUntilOnNavigateBackCalled()
	{
		Navigation.GoBackUntilOnNavigateBackCalled(OnNavigateBack);
	}

	protected override void BackOutToHub()
	{
		base.BackOutToHub();
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB) && Get() != null && !IsShowingFirstPage())
		{
			Get().SuckInPreconDecks();
		}
	}

	public override void PreUnload()
	{
		if (!IsShowingFirstPage() && m_randomDeckPickerTray.activeSelf)
		{
			HideRandomDeckPickerTray();
		}
	}

	private void ShowNextPage(bool skipTraySlidingAnimation = false)
	{
		ShowPage(m_currentPageIndex + 1, skipTraySlidingAnimation);
	}

	private void ShowPreviousPage(bool skipTraySlidingAnimation = false)
	{
		ShowPage(m_currentPageIndex - 1, skipTraySlidingAnimation);
	}

	private void ShowPage(int pageNum, bool skipTraySlidingAnimation = false)
	{
		if (iTween.Count(m_randomDeckPickerTray) > 0 || pageNum < 0 || pageNum >= m_customPages.Count)
		{
			return;
		}
		for (int i = 0; i < m_customPages.Count; i++)
		{
			m_customPages[i].gameObject.SetActive((i == m_currentPageIndex && !skipTraySlidingAnimation) || i == pageNum);
			if (skipTraySlidingAnimation)
			{
				Vector3 localPosition = m_customDeckPageUpperBone.transform.localPosition;
				if (i < pageNum)
				{
					localPosition = m_customDeckPageHideBone.transform.localPosition;
				}
				else if (i > pageNum)
				{
					localPosition = m_customDeckPageLowerBone.transform.localPosition;
				}
				m_customPages[i].gameObject.transform.localPosition = localPosition;
			}
		}
		if (m_currentPageIndex != pageNum && !skipTraySlidingAnimation)
		{
			GameObject currentPage = m_customPages[m_currentPageIndex].gameObject;
			GameObject target = m_customPages[pageNum].gameObject;
			if (pageNum > m_currentPageIndex)
			{
				iTween.MoveTo(currentPage, iTween.Hash("time", 0.25f, "position", m_customDeckPageHideBone.transform.localPosition, "isLocal", true, "easetype", iTween.EaseType.easeOutCubic, "oncomplete", (Action<object>)delegate
				{
					currentPage.SetActive(value: false);
				}, "oncompletetarget", base.gameObject));
				iTween.MoveTo(target, iTween.Hash("time", 0.25f, "delay", 0.25f, "easetype", iTween.EaseType.easeOutCubic, "position", m_customDeckPageUpperBone.transform.localPosition, "isLocal", true));
			}
			else
			{
				iTween.MoveTo(currentPage, iTween.Hash("time", 0.25f, "easetype", iTween.EaseType.easeOutCubic, "position", m_customDeckPageLowerBone.transform.localPosition, "isLocal", true));
				iTween.MoveTo(target, iTween.Hash("time", 0.25f, "delay", 0.25f, "easetype", iTween.EaseType.easeOutCubic, "position", m_customDeckPageUpperBone.transform.localPosition, "isLocal", true));
			}
		}
		m_currentPageIndex = pageNum;
		HideAllPreconHighlights();
		LowerHeroButtons();
		if (ShouldHandleBoxTransition() || skipTraySlidingAnimation)
		{
			Box.Get().AddTransitionFinishedListener(OnBoxTransitionFinished);
			HideRandomDeckPickerTray();
		}
		else
		{
			iTween.MoveTo(m_randomDeckPickerTray, iTween.Hash("time", 0.25f, "position", m_randomDecksHiddenBone.transform.localPosition, "oncomplete", (Action<object>)delegate
			{
				HideRandomDeckPickerTray();
			}, "oncompletetarget", base.gameObject, "isLocal", true, "delay", 0f));
		}
		UpdatePageArrows();
		Options.Get().SetBool(Option.HAS_SEEN_CUSTOM_DECK_PICKER, val: true);
	}

	private IEnumerator ArrowDelayedActivate(UIBButton arrow, float delay)
	{
		yield return new WaitForSeconds(delay);
		arrow.gameObject.SetActive(value: true);
	}

	private bool ShouldHandleBoxTransition()
	{
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY)
		{
			return false;
		}
		if (Box.Get().IsBusy() || Box.Get().GetState() == Box.State.LOADING || Box.Get().GetState() == Box.State.LOADING_HUB)
		{
			return true;
		}
		return false;
	}

	private void OnBoxTransitionFinished(object userData)
	{
		if (m_randomDeckPickerTray != null && IsShowingFirstPage())
		{
			ShowBasicDeckPickerTray();
		}
		Box.Get().RemoveTransitionFinishedListener(OnBoxTransitionFinished);
	}

	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		HideSetRotationNotifications();
		SceneMgr.Get().UnregisterScenePreUnloadEvent(OnScenePreUnload);
	}

	private void LowerHeroButtons()
	{
		foreach (HeroPickerButton heroButton in m_heroButtons)
		{
			if (heroButton.gameObject.activeSelf)
			{
				heroButton.Lower();
			}
		}
	}

	private void RaiseHeroButtons()
	{
		foreach (HeroPickerButton heroButton in m_heroButtons)
		{
			if (heroButton.gameObject.activeSelf)
			{
				heroButton.Raise();
			}
		}
	}

	protected void SetKeyholeTextureOffsets(UnityEngine.Vector2 offset)
	{
		if (IsChoosingHero())
		{
			return;
		}
		int materialIndex = (UniversalInputManager.UsePhoneUI ? 1 : 0);
		foreach (HeroPickerButton heroButton in m_heroButtons)
		{
			Renderer component = heroButton.m_buttonFrame.GetComponent<Renderer>();
			if (component == null)
			{
				Debug.LogWarning("Couldn't set keyhole texture offset on invalid renderer");
			}
			else
			{
				component.GetMaterial(materialIndex).mainTextureOffset = offset;
			}
		}
	}

	private void HideRandomDeckPickerTray()
	{
		m_randomDeckPickerTray.transform.localPosition = new Vector3(-5000f, -5000f, -5000f);
	}

	private void ShowBasicDeckPickerTray()
	{
		m_randomDeckPickerTray.transform.localPosition = m_randomDecksHiddenBone.transform.localPosition;
	}

	private void OnShowPreviousPage(UIEvent e)
	{
		SoundManager.Get().LoadAndPlay("hero_panel_slide_on.prefab:236147a924d7cb442872b46dddd56132");
		ShowPreviousPage();
	}

	private void ShowFirstPage(bool skipTraySlidingAnimation = false)
	{
		if (iTween.Count(m_randomDeckPickerTray) > 0)
		{
			return;
		}
		ShowBasicDeckPickerTray();
		m_currentPageIndex = 0;
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (IsChoosingHero())
		{
			if (m_modeLabelBg != null)
			{
				m_modeLabelBg.transform.localEulerAngles = new Vector3(180f, 0f, 0f);
			}
			if (skipTraySlidingAnimation)
			{
				m_randomDeckPickerTray.transform.localPosition = m_randomDecksShownBone.transform.localPosition;
				RaiseHeroButtons();
			}
			else
			{
				iTween.MoveTo(m_randomDeckPickerTray, iTween.Hash("time", 0.25f, "position", m_randomDecksShownBone.transform.localPosition, "isLocal", true, "oncomplete", "RaiseHeroButtons", "oncompletetarget", base.gameObject));
			}
		}
		else if (mode == SceneMgr.Mode.ADVENTURE || mode == SceneMgr.Mode.TOURNAMENT || mode == SceneMgr.Mode.FRIENDLY || mode == SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			for (int i = 0; i < m_customPages.Count; i++)
			{
				m_customPages[i].gameObject.SetActive(i == 0);
			}
			if (skipTraySlidingAnimation)
			{
				m_randomDeckPickerTray.transform.localPosition = m_randomDecksShownBone.transform.localPosition;
				RaiseHeroButtons();
			}
			else
			{
				iTween.MoveTo(m_randomDeckPickerTray, iTween.Hash("time", 0.25f, "position", m_randomDecksShownBone.transform.localPosition, "isLocal", true, "oncomplete", "RaiseHeroButtons", "oncompletetarget", base.gameObject));
			}
		}
		UpdatePageArrows();
	}

	private void SuckInPreconDecks()
	{
		ShowBasicDeckPickerTray();
		iTween.MoveTo(m_randomDeckPickerTray, iTween.Hash("time", 0.25f, "position", m_suckedInRandomDecksBone.transform.localPosition, "isLocal", true, "oncomplete", "SuckInFinished", "oncompletetarget", base.gameObject));
	}

	private void OnCustomDeckPressed(CollectionDeckBoxVisual deckbox)
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && Options.GetInRankedPlayMode())
		{
			CollectionDeck collectionDeck = deckbox.GetCollectionDeck();
			if (collectionDeck == null)
			{
				return;
			}
			if (collectionDeck.FormatType == PegasusShared.FormatType.FT_CLASSIC && Options.GetFormatType() != PegasusShared.FormatType.FT_CLASSIC)
			{
				ShowClickedClassicDeckInNonClassicPopup();
				return;
			}
			if (collectionDeck.FormatType == PegasusShared.FormatType.FT_STANDARD && Options.GetFormatType() == PegasusShared.FormatType.FT_CLASSIC)
			{
				ShowClickedStandardDeckInClassicPopup();
				return;
			}
			if (collectionDeck.FormatType == PegasusShared.FormatType.FT_WILD && Options.GetFormatType() == PegasusShared.FormatType.FT_CLASSIC)
			{
				ShowClickedWildDeckInClassicPopup();
				return;
			}
			if (!deckbox.IsDeckSelectableForCurrentMode() || HandleClickToFixDeck(deckbox.GetCollectionDeck(), null, deckbox.CanClickToConvertToStandard(), deckbox.IsMissingCards))
			{
				return;
			}
		}
		else if (!deckbox.IsDeckSelectableForCurrentMode() || HandleClickToFixDeck(deckbox.GetCollectionDeck(), null, isClickToConvertCase: false, deckbox.IsMissingCards))
		{
			return;
		}
		SelectCustomDeck(deckbox);
	}

	private void SelectCustomDeck(CollectionDeckBoxVisual deckbox)
	{
		HideDemoQuotes();
		if (!m_validClasses.Contains(deckbox.GetClass()))
		{
			deckbox.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			ShowInvalidClassPopup();
			return;
		}
		SetPlayButtonEnabled(enable: true);
		RemoveHeroLockedTooltip();
		Options.Get().SetLong(Option.LAST_CUSTOM_DECK_CHOSEN, deckbox.GetDeckID());
		deckbox.SetIsSelected(isSelected: true);
		if ((bool)AbsDeckPickerTrayDisplay.HIGHLIGHT_SELECTED_DECK)
		{
			deckbox.SetHighlightState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		else
		{
			deckbox.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		}
		if (m_selectedCustomDeckBox != null && m_selectedCustomDeckBox != deckbox)
		{
			m_selectedCustomDeckBox.SetIsSelected(isSelected: false);
			m_selectedCustomDeckBox.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
		}
		m_selectedCustomDeckBox = deckbox;
		UpdateHeroInfo(deckbox);
		ShowPreconHero(show: true);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_slidingTray.ToggleTraySlider(show: true);
		}
	}

	private bool HandleClickToFixDeck(CollectionDeck deck, HeroPickerButton button, bool isClickToConvertCase, bool IsMissingCards)
	{
		if (deck == null)
		{
			return false;
		}
		DeckRuleset ruleset = deck.GetRuleset();
		if (ruleset != null && ruleset.EntityInDeckIgnoresRuleset(deck))
		{
			return false;
		}
		bool flag = false;
		foreach (CollectionDeckSlot slot in deck.GetSlots())
		{
			if (GameDbf.GetIndex().HasCardPlayerDeckOverride(slot.CardID))
			{
				flag = true;
				break;
			}
		}
		if (isClickToConvertCase && flag)
		{
			ShowClickedWildDeckInStandardPopup();
			return true;
		}
		if (IsMissingCards || isClickToConvertCase)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_showAlertIcon = false;
			popupInfo.m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES");
			popupInfo.m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			if (isClickToConvertCase)
			{
				popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_WILD_POPUP_HEADER");
				popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_DECK_WILD_POPUP_TEXT");
				popupInfo.m_confirmText = GameStrings.Get("GLUE_COLLECTION_DECK_WILD_POPUP_CONFIRM");
				popupInfo.m_cancelText = GameStrings.Get("GLUE_COLLECTION_DECK_WILD_POPUP_CANCEL");
			}
			else
			{
				int num = deck.GetMaxCardCount() - deck.GetTotalValidCardCount();
				popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_INCOMPLETE_POPUP_HEADER");
				if (CollectionManager.Get().ShouldAccountSeeStandardWild())
				{
					popupInfo.m_text = GameStrings.Format("GLUE_COLLECTION_DECK_INCOMPLETE_POPUP_TEXT", num);
				}
				else
				{
					popupInfo.m_text = GameStrings.Format("GLUE_COLLECTION_DECK_INCOMPLETE_POPUP_TEXT_NPR_NEW", num);
				}
			}
			popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					if (isClickToConvertCase)
					{
						deck.FormatType = PegasusShared.FormatType.FT_STANDARD;
					}
					deck.RemoveInvalidCards();
					if (isClickToConvertCase)
					{
						CollectionManager.Get().AutoFillDeck(deck, allowSmartDeckCompletion: true, OnAutoFillDeckConvertedToStandard);
					}
					else
					{
						CollectionManager.Get().AutoFillDeck(deck, allowSmartDeckCompletion: true, OnAutoFillDeckFilledMissingCards);
					}
					if (button != null)
					{
						button.SetIsDeckValid(isValid: true);
					}
				}
				else if (isClickToConvertCase)
				{
					ShowClickedWildDeckInStandardPopup();
				}
			};
			DialogManager.Get().ShowPopup(popupInfo);
			return true;
		}
		return false;
	}

	private void OnAutoFillDeckConvertedToStandard(CollectionDeck deck, IEnumerable<DeckMaker.DeckFill> fillCards)
	{
		OnAutoFillDeckComplete(deck, fillCards, setFormatToStandard: true);
	}

	private void OnAutoFillDeckFilledMissingCards(CollectionDeck deck, IEnumerable<DeckMaker.DeckFill> fillCards)
	{
		OnAutoFillDeckComplete(deck, fillCards, setFormatToStandard: false);
	}

	private void OnAutoFillDeckComplete(CollectionDeck deck, IEnumerable<DeckMaker.DeckFill> fillCards, bool setFormatToStandard)
	{
		if (deck == null)
		{
			return;
		}
		deck.FillFromCardList(fillCards);
		CustomDeckPage currentCustomPage = GetCurrentCustomPage();
		if (!(currentCustomPage != null))
		{
			return;
		}
		currentCustomPage.UpdateDeckVisuals();
		CollectionDeckBoxVisual collectionDeckBoxVisual = currentCustomPage.FindDeckVisual(deck);
		if (collectionDeckBoxVisual != null)
		{
			SelectCustomDeck(collectionDeckBoxVisual);
			if (!UniversalInputManager.UsePhoneUI)
			{
				collectionDeckBoxVisual.PlayGlowAnim(setFormatToStandard);
			}
		}
	}

	protected override void OnHeroButtonReleased(UIEvent e)
	{
		base.OnHeroButtonReleased(e);
		HideDemoQuotes();
	}

	protected override void SelectHero(HeroPickerButton button, bool showTrayForPhone = true)
	{
		if ((!(button == m_selectedHeroButton) || (bool)UniversalInputManager.UsePhoneUI) && (IsChoosingHero() || button.IsDeckValid()))
		{
			base.SelectHero(button, showTrayForPhone);
			Options.Get().SetInt(Option.LAST_PRECON_HERO_CHOSEN, (int)button.m_heroClass);
			if (button.IsLocked())
			{
				string shortName = button.GetEntityDef().GetShortName();
				string className = GameStrings.GetClassName(button.m_heroClass);
				AddHeroLockedTooltip(GameStrings.Get("GLUE_HERO_LOCKED_NAME"), GameStrings.Format("GLUE_HERO_LOCKED_DESC", shortName, className));
			}
		}
	}

	private void Deselect()
	{
		if (m_selectedHeroButton == null && m_selectedCustomDeckBox == null)
		{
			return;
		}
		SetPlayButtonEnabled(enable: false);
		if (m_heroLockedTooltip != null)
		{
			UnityEngine.Object.DestroyImmediate(m_heroLockedTooltip.gameObject);
		}
		if (m_selectedCustomDeckBox != null)
		{
			m_selectedCustomDeckBox.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			m_selectedCustomDeckBox.SetEnabled(enabled: true);
			m_selectedCustomDeckBox.SetIsSelected(isSelected: false);
			m_selectedCustomDeckBox = null;
		}
		m_heroActor.SetEntityDef(null);
		m_heroActor.SetCardDef(null);
		m_heroActor.Hide();
		if (m_selectedHeroButton != null)
		{
			m_selectedHeroButton.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			m_selectedHeroButton.SetSelected(isSelected: false);
			m_selectedHeroButton = null;
		}
		if (ShouldShowHeroPower())
		{
			m_heroPowerActor.SetCardDef(null);
			m_heroPowerActor.SetEntityDef(null);
			m_heroPowerActor.Hide();
			m_goldenHeroPowerActor.SetCardDef(null);
			m_goldenHeroPowerActor.SetEntityDef(null);
			m_goldenHeroPowerActor.Hide();
			m_heroPower.GetComponent<Collider>().enabled = false;
			m_goldenHeroPower.GetComponent<Collider>().enabled = false;
			if (m_heroPowerShadowQuad != null)
			{
				m_heroPowerShadowQuad.SetActive(value: false);
			}
		}
		m_selectedHeroPowerFullDef = null;
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
		m_selectedHeroName = null;
		m_heroName.Text = "";
	}

	private void UpdateHeroInfo(CollectionDeckBoxVisual deckBox)
	{
		using DefLoader.DisposableFullDef fullDef = deckBox.SharedDisposableFullDef();
		UpdateHeroInfo(fullDef, deckBox.GetDeckNameText().Text, deckBox.GetHeroCardPremium());
	}

	protected override void UpdateHeroInfo(HeroPickerButton button)
	{
		using DefLoader.DisposableFullDef disposableFullDef = button.ShareFullDef();
		string heroName = disposableFullDef.EntityDef.GetName();
		string cardID = disposableFullDef.EntityDef.GetCardId();
		TAG_CARD_SET cardSet = disposableFullDef.EntityDef.GetCardSet();
		if (TAG_CARD_SET.HERO_SKINS == cardSet)
		{
			cardID = CollectionManager.GetHeroCardId(disposableFullDef.EntityDef.GetClass(), CardHero.HeroType.VANILLA);
		}
		UpdateHeroInfo(disposableFullDef, heroName, CollectionManager.Get().GetBestCardPremium(cardID), button.IsLocked());
	}

	private void UpdateHeroInfo(DefLoader.DisposableFullDef fullDef, string heroName, TAG_PREMIUM premium, bool locked = false)
	{
		m_heroName.Text = heroName;
		m_selectedHeroName = fullDef.EntityDef.GetName();
		m_heroActor.SetPremium(premium);
		m_heroActor.SetFullDef(fullDef);
		m_heroActor.UpdateAllComponents();
		m_heroActor.SetUnlit();
		NetCache.HeroLevel heroLevel = ((!locked) ? GameUtils.GetHeroLevel(fullDef.EntityDef.GetClass()) : null);
		int totalLevel = GameUtils.GetTotalHeroLevel() ?? 0;
		m_xpBar.UpdateDisplay(heroLevel, totalLevel);
		string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(fullDef.EntityDef.GetCardId());
		if (!locked && ShouldShowHeroPower() && !string.IsNullOrEmpty(heroPowerCardIdFromHero))
		{
			m_heroPowerContainer.SetActive(value: true);
			UpdateHeroPowerInfo(m_heroPowerDefs[heroPowerCardIdFromHero], premium);
		}
		else
		{
			SetHeroPowerActorColliderEnabled(enable: false);
			HideHeroPowerActor();
			m_heroPowerContainer.SetActive(value: false);
		}
		UpdateRankedClassWinsPlate();
	}

	protected override void TransitionToFormatType(PegasusShared.FormatType formatType, bool inRankedPlayMode, float transitionSpeed = 2f)
	{
		VisualsFormatType visualsFormatType = VisualsFormatTypeExtensions.ToVisualsFormatType(m_PreviousFormatType, m_PreviousInRankedPlayMode);
		VisualsFormatType visualsFormatType2 = VisualsFormatTypeExtensions.ToVisualsFormatType(formatType, inRankedPlayMode);
		m_PreviousFormatType = formatType;
		m_PreviousInRankedPlayMode = inRankedPlayMode;
		base.TransitionToFormatType(formatType, inRankedPlayMode, transitionSpeed);
		UpdateTrayBackgroundTransitionValues(visualsFormatType, visualsFormatType2, transitionSpeed);
		if (!UniversalInputManager.UsePhoneUI)
		{
			if (!inRankedPlayMode)
			{
				m_casualPlayDisplayWidget.Show();
				m_rankedPlayDisplay.Hide(m_rankedPlayDisplayHideDelay);
			}
			else
			{
				m_casualPlayDisplayWidget.Hide();
				if (m_rankedPlayDisplay != null)
				{
					m_rankedPlayDisplay.Show(m_rankedPlayDisplayShowDelay);
					m_rankedPlayDisplay.OnSwitchFormat(visualsFormatType2);
				}
			}
		}
		UpdateValidHeroClasses();
		if (m_inHeroPicker && ((visualsFormatType == VisualsFormatType.VFT_CLASSIC && visualsFormatType2 != VisualsFormatType.VFT_CLASSIC) || (visualsFormatType != VisualsFormatType.VFT_CLASSIC && visualsFormatType2 == VisualsFormatType.VFT_CLASSIC)))
		{
			Deselect();
			StartCoroutine(LoadHeroButtons());
		}
		PlayTrayTransitionSound(visualsFormatType2);
		PlayTrayTransitionGlowBursts(visualsFormatType, visualsFormatType2);
	}

	private void UpdateTrayBackgroundTransitionValues(VisualsFormatType oldVisualsFormatType, VisualsFormatType visualsFormatType, float transitionSpeed = 2f)
	{
		float targetValue = 1f;
		Texture textureForFormat = m_currentModeTextures.GetTextureForFormat(oldVisualsFormatType);
		Texture customTextureForFormat = m_currentModeTextures.GetCustomTextureForFormat(oldVisualsFormatType);
		Texture textureForFormat2 = m_currentModeTextures.GetTextureForFormat(visualsFormatType);
		Texture customTextureForFormat2 = m_currentModeTextures.GetCustomTextureForFormat(visualsFormatType);
		if ((bool)UniversalInputManager.UsePhoneUI && m_slidingTray.IsShown())
		{
			m_detailsTrayFrame.GetComponentInChildren<Renderer>().GetMaterial().mainTexture = textureForFormat2;
		}
		SetCustomDeckPageTextures(customTextureForFormat, customTextureForFormat2);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			SetPhoneDetailsTrayTextures(textureForFormat, textureForFormat2);
		}
		else
		{
			SetTrayFrameAndBasicDeckPageTextures(textureForFormat, textureForFormat2);
		}
		StopCoroutine("TransitionTrayMaterial");
		StartCoroutine(TransitionTrayMaterial(targetValue, transitionSpeed));
	}

	private void PlayTrayTransitionGlowBursts(VisualsFormatType oldVisualsFormatType, VisualsFormatType visualsFormatType)
	{
		if (oldVisualsFormatType == visualsFormatType)
		{
			return;
		}
		if (m_customPages != null && (oldVisualsFormatType == VisualsFormatType.VFT_WILD || visualsFormatType == VisualsFormatType.VFT_WILD))
		{
			bool useFX = oldVisualsFormatType == VisualsFormatType.VFT_WILD;
			bool flag = GetNumValidStandardDecks() != 0;
			if (m_customPages.Count > 1 && !IsShowingFirstPage())
			{
				m_customPages[1].PlayVineGlowBurst(useFX, flag);
			}
			else if (m_customPages.Count > 0)
			{
				if (flag)
				{
					GameObject[] customVineGlowToggle = m_customPages[0].m_customVineGlowToggle;
					for (int i = 0; i < customVineGlowToggle.Length; i++)
					{
						customVineGlowToggle[i].SetActive(value: true);
					}
				}
				m_customPages[0].PlayVineGlowBurst(useFX, flag);
			}
		}
		if (m_inHeroPicker)
		{
			PlayTransitionGlowBurstsForNewDeckFSMs(oldVisualsFormatType, visualsFormatType);
		}
		else
		{
			PlayTransitionGlowBurstsForNonNewDeckFSMs(oldVisualsFormatType, visualsFormatType);
		}
	}

	private void PlayTransitionGlowBurstsForNonNewDeckFSMs(VisualsFormatType oldVisualsFormatType, VisualsFormatType visualsFormatType)
	{
		string text = oldVisualsFormatType switch
		{
			VisualsFormatType.VFT_WILD => m_leavingWildGlowEvent, 
			VisualsFormatType.VFT_CLASSIC => m_leavingClassicGlowEvent, 
			VisualsFormatType.VFT_CASUAL => m_leavingCasualGlowEvent, 
			_ => null, 
		};
		if (!string.IsNullOrEmpty(text))
		{
			foreach (PlayMakerFSM formatChangeGlowFSM in formatChangeGlowFSMs)
			{
				if (formatChangeGlowFSM != null)
				{
					formatChangeGlowFSM.SendEvent(text);
				}
			}
			if (m_rankedPlayDisplay != null)
			{
				m_rankedPlayDisplay.PlayTransitionGlowBurstsForNonNewDeckFSMs(text);
			}
		}
		text = visualsFormatType switch
		{
			VisualsFormatType.VFT_WILD => m_enteringWildGlowEvent, 
			VisualsFormatType.VFT_CLASSIC => m_enteringClassicGlowEvent, 
			VisualsFormatType.VFT_CASUAL => m_enteringCasualGlowEvent, 
			_ => null, 
		};
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		foreach (PlayMakerFSM formatChangeGlowFSM2 in formatChangeGlowFSMs)
		{
			if (formatChangeGlowFSM2 != null)
			{
				formatChangeGlowFSM2.SendEvent(text);
			}
		}
		if (m_rankedPlayDisplay != null)
		{
			m_rankedPlayDisplay.PlayTransitionGlowBurstsForNonNewDeckFSMs(text);
		}
	}

	private void PlayTransitionGlowBurstsForNewDeckFSMs(VisualsFormatType oldVisualsFormatType, VisualsFormatType visualsFormatType)
	{
		string text = null;
		if (oldVisualsFormatType == VisualsFormatType.VFT_CLASSIC && visualsFormatType != VisualsFormatType.VFT_CLASSIC)
		{
			text = m_newDeckLeavingClassicGlowEvent;
		}
		else if (oldVisualsFormatType != VisualsFormatType.VFT_CLASSIC && visualsFormatType == VisualsFormatType.VFT_CLASSIC)
		{
			text = m_newDeckEnteringClassicGlowEvent;
		}
		else if (oldVisualsFormatType == VisualsFormatType.VFT_WILD && visualsFormatType != VisualsFormatType.VFT_WILD)
		{
			text = m_newDeckLeavingWildGlowEvent;
		}
		else if (oldVisualsFormatType != VisualsFormatType.VFT_WILD && visualsFormatType == VisualsFormatType.VFT_WILD)
		{
			text = m_newDeckEnteringWildGlowEvent;
		}
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		foreach (PlayMakerFSM newDeckFormatChangeGlowFSM in newDeckFormatChangeGlowFSMs)
		{
			if (newDeckFormatChangeGlowFSM != null)
			{
				newDeckFormatChangeGlowFSM.SendEvent(text);
			}
		}
		if (m_rankedPlayDisplay != null)
		{
			m_rankedPlayDisplay.PlayTransitionGlowBurstsForNewDeckFSMs(text);
		}
	}

	private void PlayTrayTransitionSound(VisualsFormatType visualsFormatType)
	{
		switch (Box.Get().GetState())
		{
		case Box.State.SET_ROTATION_OPEN:
			if (m_setRotationTutorialState == SetRotationTutorialState.PREPARING)
			{
				return;
			}
			break;
		case Box.State.LOADING:
			return;
		}
		string text;
		switch (visualsFormatType)
		{
		case VisualsFormatType.VFT_STANDARD:
			text = m_standardTransitionSound;
			break;
		case VisualsFormatType.VFT_WILD:
		case VisualsFormatType.VFT_CASUAL:
			text = m_wildTransitionSound;
			break;
		case VisualsFormatType.VFT_CLASSIC:
			text = m_classicTransitionSound;
			break;
		default:
			Debug.LogError("No transition sound for format " + visualsFormatType);
			text = "";
			break;
		}
		if (!string.IsNullOrEmpty(text))
		{
			SoundManager.Get().LoadAndPlay(text);
		}
	}

	private IEnumerator TransitionTrayMaterial(float targetValue, float speed)
	{
		Material randomTrayMat = null;
		Material trayMat;
		float currentValue;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			trayMat = null;
			randomTrayMat = m_basicDeckPage.GetComponent<Renderer>().GetMaterial();
			currentValue = randomTrayMat.GetFloat("_Transistion");
		}
		else
		{
			trayMat = m_trayFrame.GetComponentInChildren<Renderer>().GetMaterial();
			currentValue = trayMat.GetFloat("_Transistion");
			Renderer componentInChildren = m_basicDeckPage.GetComponentInChildren<Renderer>();
			if (componentInChildren != null)
			{
				randomTrayMat = componentInChildren.GetMaterial();
			}
		}
		do
		{
			currentValue = Mathf.MoveTowards(currentValue, targetValue, speed * Time.deltaTime);
			if (trayMat != null)
			{
				trayMat.SetFloat("_Transistion", currentValue);
			}
			if (randomTrayMat != null)
			{
				randomTrayMat.SetFloat("_Transistion", currentValue);
			}
			if (m_customPages != null)
			{
				foreach (CustomDeckPage customPage in m_customPages)
				{
					customPage.UpdateTrayTransitionValue(currentValue);
				}
			}
			yield return null;
		}
		while (currentValue != targetValue);
	}

	private void SetTrayFrameAndBasicDeckPageTextures(Texture mainTexture, Texture transitionToTexture)
	{
		Material material = m_trayFrame.GetComponentInChildren<Renderer>().GetMaterial();
		material.mainTexture = mainTexture;
		material.SetTexture("_MainTex2", transitionToTexture);
		material.SetFloat("_Transistion", 0f);
		Renderer componentInChildren = m_basicDeckPage.GetComponentInChildren<Renderer>();
		if (componentInChildren != null)
		{
			Material material2 = componentInChildren.GetMaterial();
			material2.mainTexture = mainTexture;
			material2.SetTexture("_MainTex2", transitionToTexture);
			material2.SetFloat("_Transistion", 0f);
		}
	}

	private void SetCustomDeckPageTextures(Texture transitionFromTexture, Texture targetTexture)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			Material material = m_basicDeckPage.GetComponent<Renderer>().GetMaterial();
			material.mainTexture = transitionFromTexture;
			material.SetTexture("_MainTex2", targetTexture);
			material.SetFloat("_Transistion", 0f);
		}
		if (m_customPages == null)
		{
			return;
		}
		foreach (CustomDeckPage customPage in m_customPages)
		{
			customPage.SetTrayTextures(transitionFromTexture, targetTexture);
		}
	}

	private void SetPhoneDetailsTrayTextures(Texture transitionFromTexture, Texture targetTexture)
	{
		Material sharedMaterial = m_detailsTrayFrame.GetComponent<Renderer>().GetSharedMaterial();
		sharedMaterial.mainTexture = transitionFromTexture;
		sharedMaterial.SetTexture("_MainTex2", targetTexture);
		sharedMaterial.SetFloat("_Transistion", 0f);
	}

	private void OnRankedPlayDisplayWidgetReady()
	{
		m_rankedPlayDisplayWidget.transform.SetParent(m_rankedPlayDisplayWidgetBone, worldPositionStays: false);
		m_rankedPlayDisplay = m_rankedPlayDisplayWidget.GetComponentInChildren<RankedPlayDisplay>();
		VisualsFormatType currentVisualsFormatType = VisualsFormatTypeExtensions.GetCurrentVisualsFormatType();
		UpdateRankedPlayDisplay(currentVisualsFormatType);
		StartCoroutine(SetRankedMedalWhenReady());
	}

	private void OnFormatTypePickerPopupReady()
	{
		m_formatTypePickerWidget.transform.SetParent(base.gameObject.transform);
		m_formatTypePickerWidget.RegisterEventListener(OnFormatTypePickerPopupEvent);
		bool num = CollectionManager.Get().ShouldAccountSeeStandardWild();
		bool inHeroPicker = m_inHeroPicker;
		if (num)
		{
			m_formatTypePickerWidget.TriggerEvent(inHeroPicker ? "3BUTTONS" : "4BUTTONS");
		}
		else
		{
			m_formatTypePickerWidget.TriggerEvent("2BUTTONS");
		}
	}

	private void OnFormatTypePickerPopupEvent(string eventName)
	{
		switch (eventName)
		{
		case "WILD_BUTTON_CLICKED":
			SwitchFormatTypeAndRankedPlayMode(VisualsFormatType.VFT_WILD);
			break;
		case "STANDARD_BUTTON_CLICKED":
			SwitchFormatTypeAndRankedPlayMode(VisualsFormatType.VFT_STANDARD);
			break;
		case "CLASSIC_BUTTON_CLICKED":
			SwitchFormatTypeAndRankedPlayMode(VisualsFormatType.VFT_CLASSIC);
			break;
		case "CASUAL_BUTTON_CLICKED":
			SwitchFormatTypeAndRankedPlayMode(VisualsFormatType.VFT_CASUAL);
			break;
		case "HIDE":
			OnFormatTypeSwitchCancelled();
			break;
		}
	}

	private IEnumerator SetRankedMedalWhenReady()
	{
		while (TournamentDisplay.Get().GetCurrentMedalInfo() == null)
		{
			yield return null;
		}
		OnMedalChanged(TournamentDisplay.Get().GetCurrentMedalInfo());
		TournamentDisplay.Get().RegisterMedalChangedListener(OnMedalChanged);
	}

	private void OnMedalChanged(NetCache.NetCacheMedalInfo medalInfo)
	{
		m_rankedPlayDisplay.OnMedalChanged(medalInfo);
	}

	protected override void OnPlayGameButtonReleased(UIEvent e)
	{
		if (!SetRotationManager.Get().CheckForSetRotationRollover() && (PlayerMigrationManager.Get() == null || !PlayerMigrationManager.Get().CheckForPlayerMigrationRequired()))
		{
			HideDemoQuotes();
			HideSetRotationNotifications();
			m_heroChosen = true;
			base.OnPlayGameButtonReleased(e);
		}
	}

	protected override void SetCollectionButtonEnabled(bool enable)
	{
		base.SetCollectionButtonEnabled(enable);
		UpdateCollectionButtonGlow();
	}

	private void UpdateCollectionButtonGlow()
	{
		if (ShouldGlowCollectionButton())
		{
			m_collectionButtonGlow.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		else
		{
			m_collectionButtonGlow.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
	}

	private void InitSwitchFormatButton()
	{
		if (!(m_switchFormatButtonContainer != null) || !(m_switchFormatButtonContainer.PrefabGameObject() != null))
		{
			return;
		}
		m_switchFormatButton = m_switchFormatButtonContainer.PrefabGameObject().GetComponent<SwitchFormatButton>();
		bool flag = false;
		if ((SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT || SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER) && CollectionManager.Get().ShouldAccountSeeStandardWild())
		{
			flag = true;
		}
		else if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && CollectionManager.Get().ShouldAccountSeeStandardComingSoon())
		{
			flag = true;
			m_showStandardComingSoonNotice = true;
		}
		if (flag)
		{
			PegasusShared.FormatType formatType;
			bool inRankedPlayMode;
			if (m_showStandardComingSoonNotice)
			{
				formatType = PegasusShared.FormatType.FT_STANDARD;
				inRankedPlayMode = true;
			}
			else if (m_inHeroPicker)
			{
				formatType = Options.GetFormatType();
				inRankedPlayMode = true;
			}
			else
			{
				formatType = Options.GetFormatType();
				inRankedPlayMode = Options.GetInRankedPlayMode();
			}
			m_switchFormatButton.Uncover();
			m_switchFormatButton.SetVisualsFormatType(VisualsFormatTypeExtensions.ToVisualsFormatType(formatType, inRankedPlayMode));
			m_switchFormatButton.AddEventListener(UIEventType.RELEASE, SwitchFormatButtonPress);
		}
		else
		{
			m_switchFormatButton.Cover();
			m_switchFormatButton.Disable();
		}
	}

	protected override void ShowHero()
	{
		if (m_selectedHeroButton != null)
		{
			UpdateHeroInfo(m_selectedHeroButton);
		}
		else
		{
			if (!(m_selectedCustomDeckBox != null))
			{
				Log.All.PrintError("DeckPickerTrayDisplay.ShowHero with no button or deck box selected!");
				return;
			}
			UpdateHeroInfo(m_selectedCustomDeckBox);
		}
		base.ShowHero();
		SetLockedPortraitMaterial(m_selectedHeroButton);
	}

	protected override void SetHeroRaised(bool raised)
	{
		m_xpBar.SetEnabled(raised);
		base.SetHeroRaised(raised);
	}

	private void HideAllPreconHighlights()
	{
		foreach (HeroPickerButton heroButton in m_heroButtons)
		{
			heroButton.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
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

	protected override void PlayGame()
	{
		base.PlayGame();
		switch (SceneMgr.Get().GetMode())
		{
		case SceneMgr.Mode.TOURNAMENT:
		{
			if (BlockOnInvalidDeckHero())
			{
				return;
			}
			long selectedDeckID2 = GetSelectedDeckID();
			if (selectedDeckID2 == 0L)
			{
				Debug.LogError("Trying to play game with deck ID 0!");
				return;
			}
			SetBackButtonEnabled(enable: false);
			PegasusShared.GameType gameTypeForNewPlayModeGame = GetGameTypeForNewPlayModeGame();
			PegasusShared.FormatType formatTypeForNewPlayModeGame = GetFormatTypeForNewPlayModeGame();
			Options.Get().SetEnum(Option.FORMAT_TYPE_LAST_PLAYED, formatTypeForNewPlayModeGame);
			GameMgr.Get().FindGame(gameTypeForNewPlayModeGame, formatTypeForNewPlayModeGame, 2, 0, selectedDeckID2);
			bool flag = true;
			if (gameTypeForNewPlayModeGame == PegasusShared.GameType.GT_RANKED && RankMgr.Get().IsLegendRank(formatTypeForNewPlayModeGame))
			{
				flag = false;
			}
			if (flag)
			{
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.PLAY_QUEUE);
			}
			break;
		}
		case SceneMgr.Mode.FRIENDLY:
			if (!FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				if (BlockOnInvalidDeckHero())
				{
					return;
				}
				long selectedDeckID3 = GetSelectedDeckID();
				if (selectedDeckID3 == 0L)
				{
					Debug.LogError("Trying to play friendly game with deck ID 0!");
					return;
				}
				FriendChallengeMgr.Get().SelectDeck(selectedDeckID3);
				FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_OPPONENT_WAITING_DECK", OnFriendChallengeWaitingForOpponentDialogResponse);
				break;
			}
			goto case SceneMgr.Mode.TAVERN_BRAWL;
		case SceneMgr.Mode.ADVENTURE:
		{
			long selectedDeckID4 = GetSelectedDeckID();
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig.GetSelectedAdventure() == AdventureDbId.NAXXRAMAS && !Options.Get().GetBool(Option.HAS_PLAYED_NAXX))
			{
				AdTrackingManager.Get().TrackAdventureProgress(Option.HAS_PLAYED_NAXX.ToString());
				Options.Get().SetBool(Option.HAS_PLAYED_NAXX, val: true);
			}
			switch (adventureConfig.CurrentSubScene)
			{
			case AdventureData.Adventuresubscene.PRACTICE:
				if (BlockOnInvalidDeckHero())
				{
					return;
				}
				PracticePickerTrayDisplay.Get().Show();
				SetHeroRaised(raised: false);
				break;
			case AdventureData.Adventuresubscene.MISSION_DECK_PICKER:
				if (!OnPlayButtonPressed_SaveHeroAndAdvanceToDungeonRunIfNecessary())
				{
					int heroCardDbId = 0;
					if (m_selectedHeroButton != null && m_selectedHeroButton.m_heroClass != 0)
					{
						heroCardDbId = GameUtils.GetFavoriteHeroCardDBIdFromClass(m_selectedHeroButton.m_heroClass);
					}
					ScenarioDbId missionToPlay = adventureConfig.GetMissionToPlay();
					if (GameDbf.Scenario.GetRecord((int)missionToPlay).RuleType == Scenario.RuleType.CHOOSE_HERO)
					{
						GameMgr.Get().FindGameWithHero(PegasusShared.GameType.GT_VS_AI, PegasusShared.FormatType.FT_WILD, (int)missionToPlay, 0, heroCardDbId, 0L);
					}
					else
					{
						GameMgr.Get().FindGame(PegasusShared.GameType.GT_VS_AI, PegasusShared.FormatType.FT_WILD, (int)missionToPlay, 0, selectedDeckID4);
					}
				}
				break;
			}
			break;
		}
		case SceneMgr.Mode.TAVERN_BRAWL:
			if (!TavernBrawlManager.Get().SelectHeroBeforeMission())
			{
				SelectHeroForCollectionManager();
			}
			break;
		case SceneMgr.Mode.COLLECTIONMANAGER:
			SelectHeroForCollectionManager();
			break;
		case SceneMgr.Mode.FIRESIDE_GATHERING:
			if (FiresideGatheringManager.Get().InBrawlMode() && !TavernBrawlManager.Get().SelectHeroBeforeMission())
			{
				SelectHeroForCollectionManager();
			}
			else if (FiresideGatheringManager.Get().InBrawlMode() && GameUtils.IsAIMission(TavernBrawlManager.Get().CurrentMission().missionId))
			{
				if (!TavernBrawlManager.Get().SelectHeroBeforeMission())
				{
					TavernBrawlManager.Get().StartGame(0L);
				}
			}
			else if (!FiresideGatheringManager.Get().InBrawlMode() || !TavernBrawlManager.Get().SelectHeroBeforeMission())
			{
				long selectedDeckID = GetSelectedDeckID();
				FriendChallengeMgr.Get().SelectDeckBeforeSendingChallenge(selectedDeckID);
			}
			break;
		}
		if ((bool)UniversalInputManager.UsePhoneUI && (SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE || AdventureConfig.Get().CurrentSubScene != AdventureData.Adventuresubscene.PRACTICE))
		{
			m_slidingTray.ToggleTraySlider(show: false);
		}
	}

	private bool BlockOnInvalidDeckHero()
	{
		if (!GameUtils.IsCardGameplayEventActive(m_selectedCustomDeckBox.GetHeroCardID()))
		{
			DialogManager.Get().ShowClassUpcomingPopup();
			return true;
		}
		return false;
	}

	private void SelectHeroForCollectionManager()
	{
		if (m_selectedHeroButton == null)
		{
			Debug.LogError("DeckPickerTrayDisplay.SelectHeroForCollectionManager called when m_selectedHeroButton was null");
			return;
		}
		m_backButton.RemoveEventListener(UIEventType.RELEASE, OnBackButtonReleased);
		Navigation.RemoveHandler(OnNavigateBack);
		if (s_selectHeroCoroutine != null)
		{
			Processor.CancelCoroutine(s_selectHeroCoroutine);
		}
		s_selectHeroCoroutine = Processor.RunCoroutine(SelectHeroForCollectionManagerImpl(m_selectedHeroButton.GetEntityDef()));
	}

	private static IEnumerator SelectHeroForCollectionManagerImpl(EntityDef heroDef)
	{
		CollectionDeckTrayDeckListContent.s_HeroPickerFormat = Options.GetFormatType();
		if (HeroPickerDisplay.Get() != null)
		{
			HeroPickerDisplay.Get().HideTray(UniversalInputManager.UsePhoneUI ? 0.25f : 0f);
		}
		CollectionDeckTray deckTray = CollectionDeckTray.Get();
		CollectionDeckTrayDeckListContent decksContent = deckTray.GetDecksContent();
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			decksContent.CreateNewDeckFromUserSelection(heroDef.GetClass(), heroDef.GetCardId());
			CollectionManager.Get().GetCollectibleDisplay().EnableInput(enable: true);
			yield break;
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		DeckTemplatePicker deckTemplatePicker = null;
		if (CollectionManager.Get().GetNonStarterTemplateDecks(Options.GetFormatType(), heroDef.GetClass()).Count > 0)
		{
			deckTemplatePicker = (UniversalInputManager.UsePhoneUI ? collectionManagerDisplay.GetPhoneDeckTemplateTray() : collectionManagerDisplay.m_pageManager.GetDeckTemplatePicker());
		}
		if (deckTemplatePicker != null && (bool)UniversalInputManager.UsePhoneUI)
		{
			deckTemplatePicker.m_phoneBackButton.SetEnabled(enabled: false);
		}
		deckTray.m_doneButton.SetEnabled(enabled: false);
		while (deckTray.IsUpdatingTrayMode() || decksContent.NumDecksToDelete() > 0 || deckTray.IsWaitingToDeleteDeck())
		{
			yield return null;
		}
		decksContent.CreateNewDeckFromUserSelection(heroDef.GetClass(), heroDef.GetCardId());
		while (deckTemplatePicker != null && !deckTemplatePicker.IsShowingPacks())
		{
			yield return null;
		}
		if (CollectionManager.Get().GetCollectibleDisplay() != null)
		{
			CollectionManager.Get().GetCollectibleDisplay().EnableInput(enable: true);
		}
		while (deckTray != null && deckTray.IsUpdatingTrayMode())
		{
			yield return null;
		}
		if (deckTemplatePicker != null && (bool)UniversalInputManager.UsePhoneUI)
		{
			deckTemplatePicker.m_phoneBackButton.SetEnabled(enabled: true);
		}
		if (deckTray != null)
		{
			deckTray.m_doneButton.SetEnabled(enabled: true);
		}
	}

	protected override void OnSlidingTrayToggled(bool isShowing)
	{
		base.OnSlidingTrayToggled(isShowing);
		if (isShowing)
		{
			TransitionToFormatType(Options.GetFormatType(), Options.GetInRankedPlayMode());
		}
	}

	protected override IEnumerator InitModeWhenReady()
	{
		while ((ShouldLoadCustomDecks() && !CustomPagesReady()) || (SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT && m_rankedPlayDisplay == null))
		{
			if (!SceneMgr.Get().DoesCurrentSceneSupportOfflineActivity() && !Network.IsLoggedIn())
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
				yield break;
			}
			yield return null;
		}
		if (!IsChoosingHero())
		{
			while (!NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheDecks>())
			{
				yield return null;
			}
		}
		yield return StartCoroutine(base.InitModeWhenReady());
		InitMode();
		while (LoadingScreen.Get().IsTransitioning())
		{
			yield return null;
		}
		if (ShouldShowBonusStarsPopUp())
		{
			ShowBonusStarsPopup();
		}
		else
		{
			PlayEnterModeDialogues();
		}
	}

	private bool CustomPagesReady()
	{
		if (m_customPages == null)
		{
			return false;
		}
		foreach (CustomDeckPage customPage in m_customPages)
		{
			if (customPage == null || !customPage.PageReady())
			{
				return false;
			}
		}
		return true;
	}

	private CustomDeckPage GetCurrentCustomPage()
	{
		if (m_currentPageIndex < m_customPages.Count)
		{
			return m_customPages[m_currentPageIndex];
		}
		return null;
	}

	protected override void InitRichPresence(Global.PresenceStatus? newStatus = null)
	{
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.TOURNAMENT)
		{
			newStatus = Global.PresenceStatus.PLAY_DECKPICKER;
		}
		base.InitRichPresence(newStatus);
	}

	private void SetSelectionAndPageFromOptions()
	{
		bool num = (bool)UniversalInputManager.UsePhoneUI && SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY;
		int pageNum = 0;
		long deckId = 0L;
		if (HasNewRewardedDeck(out deckId))
		{
			RewardUtils.MarkNewestRewardedDeckAsSeen();
		}
		else
		{
			deckId = GetLastChosenDeckId();
		}
		CollectionDeckBoxVisual deckboxWithDeckID = GetDeckboxWithDeckID(deckId, out pageNum);
		ShowPage(pageNum, skipTraySlidingAnimation: true);
		if (!num && deckboxWithDeckID != null && deckboxWithDeckID.IsValidForCurrentMode())
		{
			SelectCustomDeck(deckboxWithDeckID);
		}
	}

	private bool HasNewRewardedDeck(out long deckId)
	{
		bool flag = RewardUtils.HasNewRewardedDeck(out deckId);
		if (flag && !HasValidDeckboxWithId(deckId))
		{
			Log.DeckTray.PrintWarning("HasNewRewardedDeckId - Newest rewarded deck ID option was set to an invalid deck ID: {0}", deckId);
			return false;
		}
		return flag;
	}

	private bool HasValidDeckboxWithId(long deckId)
	{
		return GetDeckboxWithDeckID(deckId) != null;
	}

	private long GetLastChosenDeckId()
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY)
		{
			return Options.Get().GetLong(Option.LAST_CUSTOM_DECK_CHOSEN);
		}
		return 0L;
	}

	private CollectionDeckBoxVisual GetDeckboxWithDeckID(long deckID)
	{
		int pageNum;
		return GetDeckboxWithDeckID(deckID, out pageNum);
	}

	private CollectionDeckBoxVisual GetDeckboxWithDeckID(long deckID, out int pageNum)
	{
		CollectionDeckBoxVisual collectionDeckBoxVisual = null;
		for (pageNum = 0; pageNum < m_customPages.Count; pageNum++)
		{
			collectionDeckBoxVisual = m_customPages[pageNum].GetDeckboxWithDeckID(deckID);
			if (collectionDeckBoxVisual != null)
			{
				return collectionDeckBoxVisual;
			}
		}
		pageNum = 0;
		return null;
	}

	protected override void OnFriendChallengeWaitingForOpponentDialogResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL && !FriendChallengeMgr.Get().AmIInGameState())
		{
			Deselect();
			base.OnFriendChallengeWaitingForOpponentDialogResponse(response, userData);
		}
	}

	protected override void OnFriendChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		base.OnFriendChallengeChanged(challengeEvent, player, challengeData, userData);
		switch (challengeEvent)
		{
		case FriendChallengeEvent.I_RECEIVED_SHARED_DECKS:
			UseSharedDecks(FriendChallengeMgr.Get().GetSharedDecks());
			break;
		case FriendChallengeEvent.I_ENDED_DECK_SHARE:
			StopUsingSharedDecks();
			break;
		case FriendChallengeEvent.I_CANCELED_DECK_SHARE_REQUEST:
			OnDeckShareRequestCancelDeclineOrError();
			break;
		case FriendChallengeEvent.OPPONENT_DECLINED_DECK_SHARE_REQUEST:
			OnDeckShareRequestCancelDeclineOrError();
			break;
		case FriendChallengeEvent.DECK_SHARE_ERROR_OCCURED:
			OnDeckShareRequestCancelDeclineOrError();
			break;
		case FriendChallengeEvent.I_ACCEPTED_DECK_SHARE_REQUEST:
		case FriendChallengeEvent.I_DECLINED_DECK_SHARE_REQUEST:
			if (FriendChallengeMgr.Get().DidISelectDeckOrHero())
			{
				FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_OPPONENT_WAITING_DECK", OnFriendChallengeWaitingForOpponentDialogResponse);
			}
			break;
		}
	}

	protected override void OnHeroFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		base.OnHeroFullDefLoaded(cardId, fullDef, userData);
		if (IsChoosingHero() && m_heroDefsLoading <= 0)
		{
			StartCoroutine(InitButtonAchievements());
		}
	}

	protected override void OnHeroActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		base.OnHeroActorLoaded(assetRef, go, callbackData);
		if (!(m_heroActor == null))
		{
			m_xpBar.transform.parent = m_heroActor.GetRootObject().transform;
			m_xpBar.transform.localScale = new Vector3(0.89f, 0.89f, 0.89f);
			m_xpBar.transform.localPosition = new Vector3(-0.1776525f, 0.2245596f, -0.7309282f);
			m_xpBar.m_isOnDeck = false;
		}
	}

	protected override bool ShouldShowHeroPower()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			return IsChoosingHero();
		}
		return true;
	}

	private bool IsDeckSharingActive()
	{
		if (m_DeckShareRequestButton == null)
		{
			return false;
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.FRIENDLY)
		{
			return false;
		}
		if (!FriendChallengeMgr.Get().IsDeckShareEnabled())
		{
			return false;
		}
		if (IsChoosingHero())
		{
			return false;
		}
		return true;
	}

	private bool ShouldShowCollectionButton()
	{
		if (IsDeckSharingActive())
		{
			return false;
		}
		if (IsChoosingHero())
		{
			return false;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.FRIENDLY)
		{
			return false;
		}
		return true;
	}

	private bool ShouldGlowCollectionButton()
	{
		if (!ShouldShowCollectionButton())
		{
			return false;
		}
		if (!m_collectionButton.IsEnabled())
		{
			return false;
		}
		if (!Options.Get().GetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_DECK) && HaveDecksThatNeedNames())
		{
			return true;
		}
		if (!Options.Get().GetBool(Option.HAS_CLICKED_COLLECTION_BUTTON_FOR_NEW_CARD) && HaveUnseenCards())
		{
			return true;
		}
		if (Options.Get().GetBool(Option.GLOW_COLLECTION_BUTTON_AFTER_SET_ROTATION) && SceneMgr.Get().GetMode() == SceneMgr.Mode.TOURNAMENT)
		{
			return true;
		}
		return false;
	}

	private bool HaveDecksThatNeedNames()
	{
		foreach (CollectionDeck deck in CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK))
		{
			if (deck.NeedsName)
			{
				return true;
			}
		}
		return false;
	}

	private uint GetNumValidStandardDecks()
	{
		uint num = 0u;
		foreach (CollectionDeck deck in CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK))
		{
			if (deck.IsValidForFormat(PegasusShared.FormatType.FT_STANDARD) && deck.IsValidForRuleset)
			{
				num++;
			}
		}
		return num;
	}

	private bool HaveUnseenCards()
	{
		return CollectionManager.Get().FindCards(null, null, null, null, null, null, null, null, minOwned: 1, notSeen: true, isHero: false, isCraftable: null, craftableFilterPremiums: null, priorityFilters: null, deckRuleset: null, returnAfterFirstResult: true).m_cards.Count > 0;
	}

	private void PlayEnterModeDialogues()
	{
		if (!ShowInnkeeperQuoteIfNeeded())
		{
			ShowWhizbangPopupIfNeeded();
		}
	}

	private bool ShowInnkeeperQuoteIfNeeded()
	{
		bool result = false;
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.COLLECTIONMANAGER && Options.Get().GetBool(Option.SHOW_WILD_DISCLAIMER_POPUP_ON_CREATE_DECK) && Options.GetFormatType() == PegasusShared.FormatType.FT_WILD && UserAttentionManager.CanShowAttentionGrabber("DeckPickTrayDisplay.ShowInnkeeperQuoteIfNeeded:" + Option.SHOW_WILD_DISCLAIMER_POPUP_ON_CREATE_DECK))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get("VO_INNKEEPER_PLAY_STANDARD_TO_WILD"), "VO_INNKEEPER_Male_Dwarf_SetRotation_43.prefab:4b4ce858139927946905ec0d40d5b3c1");
			Options.Get().SetBool(Option.SHOW_WILD_DISCLAIMER_POPUP_ON_CREATE_DECK, val: false);
			result = true;
		}
		return result;
	}

	private bool ShowWhizbangPopupIfNeeded()
	{
		if (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY)
		{
			return false;
		}
		LastGameData lastGameData = GameMgr.Get().LastGameData;
		if (lastGameData.GameResult != TAG_PLAYSTATE.WON || !lastGameData.HasWhizbangDeckID())
		{
			return false;
		}
		int num = Options.Get().GetInt(Option.WHIZBANG_POPUP_COUNTER);
		if (num >= 7)
		{
			return false;
		}
		CollectionManager.TemplateDeck templateDeck = CollectionManager.Get().GetTemplateDeck(lastGameData.WhizbangDeckID);
		if (templateDeck == null || (!string.IsNullOrEmpty(templateDeck.m_event) && !SpecialEventManager.Get().IsEventActive(templateDeck.m_event, activeIfDoesNotExist: false)))
		{
			return false;
		}
		bool result = false;
		if (num == 0 || num == 2 || num == 6)
		{
			if (UserAttentionManager.CanShowAttentionGrabber("DeckPickerTrayDisplay.ShowWhizbangPopupIfNeeded()"))
			{
				StartCoroutine(ShowWhizbangPopup(templateDeck));
				num++;
				result = true;
			}
		}
		else
		{
			num++;
		}
		Options.Get().SetInt(Option.WHIZBANG_POPUP_COUNTER, num);
		return result;
	}

	private IEnumerator ShowWhizbangPopup(CollectionManager.TemplateDeck whizbangDeck)
	{
		if (whizbangDeck != null)
		{
			yield return new WaitForSeconds(1f);
			BasicPopup.PopupInfo popupInfo = new BasicPopup.PopupInfo();
			popupInfo.m_prefabAssetRefs.Add("WhizbangDialog_notification.prefab:89912cf72b2d5cf47820d2328de40f3f");
			popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_MANAGER_WHIZBANG_POPUP_HEADER");
			popupInfo.m_bodyText = GameStrings.Format("GLUE_COLLECTION_MANAGER_WHIZBANG_POPUP_BODY", GameStrings.GetClassName(whizbangDeck.m_class), whizbangDeck.m_title);
			popupInfo.m_disableBnetBar = true;
			DialogManager.Get().ShowBasicPopup(UserAttentionBlocker.NONE, popupInfo);
		}
	}

	private void SetLockedPortraitMaterial(HeroPickerButton button)
	{
		if (!(button != null) || !button.IsLocked())
		{
			return;
		}
		SceneMgr.Mode mode = SceneMgr.Get().GetMode();
		if (mode == SceneMgr.Mode.TAVERN_BRAWL || (mode == SceneMgr.Mode.FRIENDLY && FriendChallengeMgr.Get().IsChallengeTavernBrawl()) || (mode == SceneMgr.Mode.FIRESIDE_GATHERING && FiresideGatheringManager.Get().InBrawlMode()))
		{
			return;
		}
		using DefLoader.DisposableFullDef disposableFullDef = button.ShareFullDef();
		if (!(disposableFullDef.CardDef.m_LockedClassPortrait == null))
		{
			m_heroActor.SetPortraitMaterial(disposableFullDef.CardDef.m_LockedClassPortrait);
		}
	}

	private bool ShouldLoadCustomDecks()
	{
		if (m_deckPickerMode == DeckPickerMode.INVALID)
		{
			Debug.LogWarning("DeckPickerTrayDisplay.ShouldLoadCustomDecks() - querying m_deckPickerMode when it hasn't been set yet!");
		}
		if (IsDeckSharingActive())
		{
			return true;
		}
		return m_deckPickerMode == DeckPickerMode.CUSTOM;
	}

	private RankedPlayDataModel GetBonusStarsPopupDataModel()
	{
		TournamentDisplay tournamentDisplay = TournamentDisplay.Get();
		if (tournamentDisplay == null)
		{
			return null;
		}
		NetCache.NetCacheMedalInfo currentMedalInfo = tournamentDisplay.GetCurrentMedalInfo();
		if (currentMedalInfo == null)
		{
			return null;
		}
		return new MedalInfoTranslator(currentMedalInfo).CreateDataModel(Options.GetFormatType(), RankedMedal.DisplayMode.Default);
	}

	private void ShowInvalidClassPopup()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_CLASS_INVALID_DECK_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_CLASS_INVALID_DECK_DESC");
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.DialogProcessCallback callback = delegate
		{
			SetPlayButtonEnabled(enable: true);
			return true;
		};
		DialogManager.Get().ShowPopup(popupInfo, callback);
	}

	private void ShowClassLockedPopup(TAG_CLASS tagClass)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_headerText = GameStrings.Get("GLUE_CLASS_INVALID_DECK_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_CLASS_INVALID_DECK_DESC");
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void UpdatePageArrows()
	{
		bool flag = true;
		bool flag2 = true;
		if (m_numPagesToShow <= 1 || (Options.GetFormatType() == PegasusShared.FormatType.FT_CLASSIC && CollectionManager.Get().GetNumberOfClassicDecks() == 0) || DemoMgr.Get().IsExpoDemo() || IsChoosingHero())
		{
			flag = false;
			flag2 = false;
		}
		else
		{
			if (IsShowingFirstPage())
			{
				flag = false;
			}
			if (IsShowingLastPage())
			{
				flag2 = false;
			}
		}
		if (flag)
		{
			if (!m_leftArrow.gameObject.activeInHierarchy)
			{
				m_showLeftArrowCoroutine = StartCoroutine(ArrowDelayedActivate(m_leftArrow, 0.25f));
			}
		}
		else
		{
			if (m_showLeftArrowCoroutine != null)
			{
				StopCoroutine(m_showLeftArrowCoroutine);
			}
			m_leftArrow.gameObject.SetActive(value: false);
		}
		if (flag2)
		{
			if (!m_rightArrow.gameObject.activeInHierarchy)
			{
				m_showRightArrowCoroutine = StartCoroutine(ArrowDelayedActivate(m_rightArrow, 0.25f));
			}
			return;
		}
		if (m_showRightArrowCoroutine != null)
		{
			StopCoroutine(m_showRightArrowCoroutine);
		}
		m_rightArrow.gameObject.SetActive(value: false);
	}

	private bool IsShowingFirstPage()
	{
		return m_currentPageIndex == 0;
	}

	private bool IsShowingLastPage()
	{
		return m_currentPageIndex == m_customPages.Count - 1;
	}

	public void InitSetRotationTutorial(bool veteranFlow)
	{
		if (m_setRotationTutorialState != 0)
		{
			Debug.LogError("Tried to call DeckPickerTrayDisplay.InitTutorial() when m_setRotationTutorialState was " + m_setRotationTutorialState);
			return;
		}
		m_setRotationTutorialState = SetRotationTutorialState.PREPARING;
		m_switchFormatButton.Disable();
		m_switchFormatButton.gameObject.SetActive(value: false);
		TransitionToFormatType(PegasusShared.FormatType.FT_STANDARD, inRankedPlayMode: true);
		Options.SetFormatType(PegasusShared.FormatType.FT_STANDARD);
		Options.SetInRankedPlayMode(inRankedPlayMode: true);
		Deselect();
		ShowFirstPage();
		m_rankedPlayDisplay.StartSetRotationTutorial();
		SetPlayButtonEnabled(enable: false);
		SetBackButtonEnabled(enable: false);
		SetCollectionButtonEnabled(enable: false);
		m_rightArrow.gameObject.SetActive(value: false);
		m_leftArrow.gameObject.SetActive(value: false);
		m_rightArrow.SetEnabled(enabled: false);
		m_leftArrow.SetEnabled(enabled: false);
		SetHeaderText(GameStrings.Get("GLUE_TOURNAMENT"));
		if (m_heroPower != null)
		{
			m_heroPower.GetComponent<Collider>().enabled = false;
		}
		if (m_goldenHeroPower != null)
		{
			m_goldenHeroPower.GetComponent<Collider>().enabled = false;
		}
		foreach (CustomDeckPage customPage in m_customPages)
		{
			customPage.EnableDeckButtons(enable: false);
		}
		m_setRotationTutorialState = SetRotationTutorialState.READY;
	}

	public void StartSetRotationTutorial(SetRotationClock.DisableTheClockCallback callback)
	{
		if (m_setRotationTutorialState == SetRotationTutorialState.READY)
		{
			StartCoroutine(ShowSetRotationTutorialPopups(callback));
			return;
		}
		Debug.LogError("Tried to start Play Screen Set Rotation Tutorial without calling DeckPickerTrayDisplay.InitTutorial()");
		callback();
	}

	private IEnumerator ShowSetRotationTutorialPopups(SetRotationClock.DisableTheClockCallback disableClockCallback)
	{
		bool veteranFlow = SetRotationManager.HasSeenStandardModeTutorial();
		m_setRotationTutorialState = SetRotationTutorialState.SHOW_TUTORIAL_POPUPS;
		m_dimQuad.GetComponent<Renderer>().enabled = true;
		m_dimQuad.enabled = true;
		m_dimQuad.StopPlayback();
		m_dimQuad.Play("DimQuad_FadeIn");
		GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
		if (gameSaveDataManager != null)
		{
			long value = -1L;
			long value2 = -1L;
			gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, out value);
			gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, out value2);
			bool flag = false;
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			if (value == 0L)
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, 1L));
				flag = true;
			}
			if (value2 == 0L)
			{
				list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, 1L));
				flag = true;
			}
			if (flag)
			{
				gameSaveDataManager.SaveSubkeys(list);
			}
		}
		m_shouldContinue = false;
		Get().AddFormatTypePickerClosedListener(ContinueTutorial);
		Get().ShowPopupDuringSetRotation(VisualsFormatType.VFT_STANDARD);
		disableClockCallback();
		while (!m_shouldContinue)
		{
			yield return null;
		}
		Get().RemoveFormatTypePickerClosedListener(ContinueTutorial);
		if (veteranFlow)
		{
			StartCoroutine(ShowWelcomeQuests());
		}
		else
		{
			StartSwitchModeWalkthrough();
		}
	}

	private void ContinueTutorial(DialogBase dialog, object userData)
	{
		m_shouldContinue = true;
	}

	private void ContinueTutorial()
	{
		m_shouldContinue = true;
	}

	private bool ShouldShowRotatedBoosterPopup(VisualsFormatType newVisualsFormatType)
	{
		if (newVisualsFormatType == VisualsFormatType.VFT_STANDARD || (newVisualsFormatType == VisualsFormatType.VFT_WILD && newVisualsFormatType.IsRanked()))
		{
			GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
			if (gameSaveDataManager != null)
			{
				long value = -1L;
				gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, out value);
				if (value == 1)
				{
					return true;
				}
			}
		}
		return false;
	}

	private IEnumerator ShowRotatedBoostersPopup(Action callbackOnHide = null)
	{
		yield return new WaitForSeconds(1f);
		if (UserAttentionManager.CanShowAttentionGrabber(UserAttentionBlocker.SET_ROTATION_INTRO, "ShowSetRotationTutorialDialog"))
		{
			SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo setRotationRotatedBoostersPopupInfo = new SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo();
			setRotationRotatedBoostersPopupInfo.m_onHiddenCallback = callbackOnHide;
			DialogManager.Get().ShowSetRotationTutorialPopup(UserAttentionBlocker.SET_ROTATION_INTRO, setRotationRotatedBoostersPopupInfo);
			GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
			if (gameSaveDataManager != null)
			{
				List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
				list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.ROTATED_BOOSTER_POPUP_PROGRESS, 2L));
				gameSaveDataManager.SaveSubkeys(list);
			}
		}
	}

	private void StartSwitchModeWalkthrough()
	{
		m_setRotationTutorialState = SetRotationTutorialState.SWITCH_MODE_WALKTHROUGH;
		StartCoroutine(TutorialSwitchToStandard());
	}

	private IEnumerator TutorialSwitchToStandard()
	{
		m_switchFormatPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_INTRO, m_Switch_Format_Notification_Bone.position, m_Switch_Format_Notification_Bone.localScale, GameStrings.Get("GLUE_TOURNAMENT_SWITCH_MODE"));
		if (m_switchFormatPopup != null)
		{
			Notification.PopUpArrowDirection direction = (UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.RightUp : Notification.PopUpArrowDirection.Up);
			m_switchFormatPopup.ShowPopUpArrow(direction);
		}
		m_switchFormatButton.EnableHighlight(enabled: true);
		m_switchFormatButton.AddEventListener(UIEventType.RELEASE, OnSwitchFormatReleased);
		m_switchFormatButton.Enable();
		yield break;
	}

	private void OnSwitchFormatReleased(UIEvent e)
	{
		if (m_setRotationTutorialState == SetRotationTutorialState.SWITCH_MODE_WALKTHROUGH)
		{
			m_switchFormatButton.Disable();
			m_switchFormatButton.RemoveEventListener(UIEventType.RELEASE, OnSwitchFormatReleased);
			Processor.QueueJob("LoginManager.CompleteLoginFlow", LoginManager.Get().CompleteLoginFlow());
			StartCoroutine(ShowWelcomeQuests());
		}
		else
		{
			Debug.Log("OnSwitchFormatReleased called when not in SWITCH_MODE_WALKTHROUGH Set Rotation Tutorial state");
		}
	}

	private void PlayTransitionSounds()
	{
		if (m_customPages[m_currentPageIndex].HasWildDeck() && !string.IsNullOrEmpty(m_wildDeckTransitionSound))
		{
			SoundManager.Get().LoadAndPlay(m_wildDeckTransitionSound);
		}
	}

	private void MarkSetRotationComplete()
	{
		m_setRotationTutorialState = SetRotationTutorialState.INACTIVE;
		Options.Get().SetBool(Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, val: true);
		Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS, 6);
		foreach (long item in m_noticeIdsToAck)
		{
			Network.Get().AckNotice(item);
		}
	}

	private IEnumerator ShowWelcomeQuests()
	{
		MarkSetRotationComplete();
		m_switchFormatButton.EnableHighlight(enabled: false);
		NotificationManager.Get().DestroyNotification(m_switchFormatPopup, 0f);
		m_switchFormatPopup = null;
		m_dimQuad.StopPlayback();
		m_dimQuad.Play("DimQuad_FadeOut");
		yield return new WaitForEndOfFrame();
		float length = m_dimQuad.GetCurrentAnimatorStateInfo(0).length;
		yield return new WaitForSeconds(length);
		m_dimQuad.GetComponent<Renderer>().enabled = false;
		m_dimQuad.enabled = false;
		yield return new WaitForSeconds(m_showQuestPause);
		OnWelcomeQuestDismiss();
	}

	private void OnWelcomeQuestDismiss()
	{
		StartCoroutine(EndTutorial());
	}

	private IEnumerator EndTutorial()
	{
		yield return new WaitForSeconds(m_playVOPause);
		if (m_heroPower != null)
		{
			m_heroPower.GetComponent<Collider>().enabled = true;
		}
		if (m_goldenHeroPower != null)
		{
			m_goldenHeroPower.GetComponent<Collider>().enabled = true;
		}
		SetBackButtonEnabled(enable: true);
		SetCollectionButtonEnabled(enable: true);
		m_rightArrow.SetEnabled(enabled: true);
		m_leftArrow.SetEnabled(enabled: true);
		m_leftArrow.gameObject.SetActive(!IsShowingFirstPage());
		m_rightArrow.gameObject.SetActive(!IsShowingLastPage());
		foreach (CustomDeckPage customPage in m_customPages)
		{
			customPage.EnableDeckButtons(enable: true);
		}
		Options.Get().SetBool(Option.GLOW_COLLECTION_BUTTON_AFTER_SET_ROTATION, val: true);
		UpdateCollectionButtonGlow();
		if (m_switchFormatButton != null)
		{
			m_switchFormatButton.Enable();
		}
		UserAttentionManager.StopBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
	}

	private bool ShouldShowStandardDeckVO(VisualsFormatType newVisualsFormatType)
	{
		if (newVisualsFormatType == VisualsFormatType.VFT_STANDARD)
		{
			GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
			if (gameSaveDataManager != null)
			{
				long value = -1L;
				gameSaveDataManager.GetSubkeyValue(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, out value);
				if (value == 1)
				{
					return true;
				}
			}
		}
		return false;
	}

	private IEnumerator ShowStandardDeckVO()
	{
		yield return new WaitForSeconds(1f);
		switch (GetNumValidStandardDecks())
		{
		case 1u:
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_INTRO, INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_HAVE_ONE_STANDARD_DECK"), "VO_INNKEEPER_Male_Dwarf_HAVE_STANDARD_DECK_07.prefab:282cd0db8b3737d4bb55d71f915074e4");
			break;
		default:
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_INTRO, INNKEEPER_QUOTE_POS, GameStrings.Get("VO_INNKEEPER_HAVE_STANDARD_DECKS"), "VO_INNKEEPER_Male_Dwarf_HAVE_STANDARD_DECKS_08.prefab:0c1c2ab2c4ead094abc69ec278aa4878");
			break;
		case 0u:
			break;
		}
		GameSaveDataManager gameSaveDataManager = GameSaveDataManager.Get();
		if (gameSaveDataManager != null)
		{
			List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
			list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.SET_ROTATION, GameSaveKeySubkeyId.INNKEEPER_STANDARD_DECKS_VO_PROGRESS, 2L));
			gameSaveDataManager.SaveSubkeys(list);
		}
	}
}
