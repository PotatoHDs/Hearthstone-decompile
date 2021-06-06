using System;
using System.Collections;
using Assets;
using Blizzard.T5.AssetManager;
using Hearthstone.DungeonCrawl;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class TavernBrawlDisplay : MonoBehaviour
{
	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_createDeckButton;

	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_editDeckButton;

	[CustomEditField(Sections = "Buttons")]
	public PlayButton m_playButton;

	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_backButton;

	[CustomEditField(Sections = "Buttons")]
	public PegUIElement m_rewardChest;

	[CustomEditField(Sections = "Buttons")]
	public UIBButton m_viewDeckButton;

	[CustomEditField(Sections = "Strings")]
	public UberText m_chalkboardHeader;

	[CustomEditField(Sections = "Strings")]
	public UberText m_chalkboardInfo;

	[CustomEditField(Sections = "Strings")]
	public UberText m_chalkboardEndInfo;

	[CustomEditField(Sections = "Strings")]
	public UberText m_numWins;

	[CustomEditField(Sections = "Strings")]
	public UberText m_TavernBrawlHeadline;

	[CustomEditField(Sections = "Animating Elements")]
	public SlidingTray m_tavernBrawlTray;

	[CustomEditField(Sections = "Animating Elements")]
	public SlidingTray m_cardListPanel;

	[CustomEditField(Sections = "Animating Elements")]
	public Animation m_cardCountPanelAnim;

	[CustomEditField(Sections = "Animating Elements")]
	public GameObject m_rewardsPreview;

	[CustomEditField(Sections = "Animating Elements")]
	public GameObject m_rewardContainer;

	[CustomEditField(Sections = "Animating Elements")]
	public UberText m_rewardsText;

	[CustomEditField(Sections = "Animating Elements")]
	public Animator m_LockedDeckTray;

	[CustomEditField(Sections = "Animating Elements")]
	public TavernBrawlPhoneDeckTray m_PhoneDeckTrayView;

	[CustomEditField(Sections = "Animating Elements")]
	public DraftManaCurve m_ManaCurvePhone;

	[CustomEditField(Sections = "Highlights")]
	public HighlightState m_createDeckHighlight;

	[CustomEditField(Sections = "Highlights")]
	public HighlightState m_rewardHighlight;

	[CustomEditField(Sections = "Highlights")]
	public HighlightState m_editDeckHighlight;

	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_transitionStartingOffset = 100f;

	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_transitionTime = 1f;

	[CustomEditField(Sections = "DungeonCrawl")]
	public float m_rootDropHeight = 10f;

	[CustomEditField(Sections = "DungeonCrawl", T = EditType.SOUND_PREFAB)]
	public string m_SlideInSound;

	public GameObject m_winsBanner;

	public GameObject m_panelWithCreateDeck;

	public GameObject m_fullPanel;

	public GameObject m_chalkboard;

	public Material m_chestOpenMaterial;

	public float m_wipeAnimStartDelay;

	public PegUIElement m_rewardOffClickCatcher;

	public GameObject m_editIcon;

	public GameObject m_deleteIcon;

	public UberText m_editText;

	public Color m_disabledTextColor = new Color(0.5f, 0.5f, 0.5f);

	public GameObject m_lossesRoot;

	public LossMarks m_lossMarks;

	public GameObject m_rewardBoxesBone;

	public GameObject m_normalWinLocationBone;

	public GameObject m_sessionWinLocationBone;

	public PegUIElement m_LockedDeckTooltipTrigger;

	public TooltipZone m_LockedDeckTooltipZone;

	public Transform m_SocketHeroBone;

	public BoxCollider m_clickBlocker;

	public GameObject m_chalkboardFX;

	public FiresideGatheringPlayButtonLantern m_FiresideGatheringPlayButtonLantern;

	public GameObject m_sessionRewardBoxesBone;

	public Vector3 m_firesideArrowHintPositionOffset;

	public Vector3 m_firesideArrowRotation;

	public float m_firesideArrowScale;

	public Texture m_chalkboardTexture;

	private static TavernBrawlDisplay s_instance;

	private bool m_doFirstSeenAnimations;

	private long m_deckBeingEdited;

	private GameObject m_rewardObject;

	private Vector3 m_rewardsScale;

	private readonly string CARD_COUNT_PANEL_OPEN_ANIM = "TavernBrawl_DecksNumberCoverUp_Open";

	private readonly string CARD_COUNT_PANEL_CLOSE_ANIM = "TavernBrawl_DecksNumberCoverUp_Close";

	private bool m_cardCountPanelAnimOpen;

	private Color? m_originalEditTextColor;

	private Color? m_originalEditIconColor;

	private TavernBrawlMission m_currentMission;

	private TavernBrawlStatus m_currentlyShowingMode;

	private bool m_firstTimeIntroductionPopupShowing;

	private BannerPopup m_firstTimeIntroBanner;

	private Actor m_chosenHero;

	private Notification m_expoThankQuote;

	private AssetLoadingHelper m_assetLoadingHelper;

	private AdventureDungeonCrawlDisplay m_dungeonCrawlDisplay;

	private DungeonCrawlServices m_dungeonCrawlServices;

	private AdventureDefCache m_adventureDefCache = new AdventureDefCache(preloadRecords: false);

	private AdventureWingDefCache m_adventureWingDefCache = new AdventureWingDefCache(preloadRecords: false);

	private bool m_rewardChestDeprecated;

	private bool m_tavernBrawlHasEndedDialogActive;

	private static readonly PlatformDependentValue<string> DEFAULT_CHALKBOARD_TEXTURE_NAME_NO_DECK = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "TavernBrawl_Chalkboard_Default_NoBorders.psd:556aa8938a98460498f590d2458e88b2",
		Phone = "TavernBrawl_Chalkboard_Default_phone.psd:c8421199aaf31fc4da69869c716fcf98"
	};

	private static readonly PlatformDependentValue<string> DEFAULT_CHALKBOARD_TEXTURE_NAME_WITH_DECK = new PlatformDependentValue<string>(PlatformCategory.Screen)
	{
		PC = "TavernBrawl_Chalkboard_Default_Borders.psd:e61e732d5bdd27e408e21fd873c99aa0",
		Phone = "TavernBrawl_Chalkboard_Default_phone.psd:c8421199aaf31fc4da69869c716fcf98"
	};

	private static readonly PlatformDependentValue<UnityEngine.Vector2> DEFAULT_CHALKBOARD_TEXTURE_OFFSET_NO_DECK = new PlatformDependentValue<UnityEngine.Vector2>(PlatformCategory.Screen)
	{
		PC = UnityEngine.Vector2.zero,
		Phone = UnityEngine.Vector2.zero
	};

	private static readonly PlatformDependentValue<UnityEngine.Vector2> DEFAULT_CHALKBOARD_TEXTURE_OFFSET_WITH_DECK = new PlatformDependentValue<UnityEngine.Vector2>(PlatformCategory.Screen)
	{
		PC = UnityEngine.Vector2.zero,
		Phone = new UnityEngine.Vector2(0f, -0.389f)
	};

	private static readonly AssetReference HEROIC_BRAWL_DIFFICULTY_WARNING_POPUP = new AssetReference("NewPopUp_HeroicBrawl.prefab:cac9ec2e7b497e641a02a03f65609486");

	private void Awake()
	{
		s_instance = this;
		base.transform.localScale = Vector3.one;
		m_currentMission = TavernBrawlManager.Get().CurrentMission();
		m_assetLoadingHelper = new AssetLoadingHelper();
		m_assetLoadingHelper.AssetLoadingComplete += OnAssetLoadingComplete;
		Awake_InitializeRewardDisplay();
		SetupUniversalButtons();
		RegisterListeners();
		SetUIForFriendlyChallenge(FriendChallengeMgr.Get().IsChallengeTavernBrawl() && !FiresideGatheringManager.Get().InBrawlMode());
	}

	private void Start()
	{
		m_tavernBrawlTray.ToggleTraySlider(show: true, null, animate: false);
		m_rewardChestDeprecated = false;
		if (PresenceMgr.Get().CurrentStatus != Global.PresenceStatus.TAVERN_BRAWL_FRIENDLY_WAITING)
		{
			Global.PresenceStatus presenceStatus = ((m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_TAVERN_BRAWL) ? Global.PresenceStatus.TAVERN_BRAWL_SCREEN : Global.PresenceStatus.FIRESIDE_BRAWL_SCREEN);
			PresenceMgr.Get().SetStatus(presenceStatus);
		}
		StartCoroutine(RefreshUIWhenReady());
		if (m_currentMission != null)
		{
			MusicPlaylistType type = ((m_currentMission.brawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? MusicPlaylistType.UI_HeroicBrawl : MusicPlaylistType.UI_TavernBrawl);
			MusicManager.Get().StartPlaylist(type);
			NarrativeManager.Get().OnTavernBrawlEntered();
			InitExpoDemoMode();
		}
	}

	private IEnumerator RefreshUIWhenReady()
	{
		while (TavernBrawlManager.Get() == null)
		{
			yield return null;
		}
		TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
		if (tavernBrawlMission != null && tavernBrawlMission.canCreateDeck && !UniversalInputManager.UsePhoneUI)
		{
			while (CollectionDeckTray.Get() == null)
			{
				yield return null;
			}
		}
		RefreshStateBasedUI(animate: false);
		RefreshDataBasedUI(m_wipeAnimStartDelay);
	}

	private void OnDestroy()
	{
		HideDemoQuotes();
		UnregisterListeners();
		s_instance = null;
	}

	public static TavernBrawlDisplay Get()
	{
		return s_instance;
	}

	public void Unload()
	{
		if (FriendChallengeMgr.Get().IsChallengeTavernBrawl() && !SceneMgr.Get().IsInGame() && !SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FRIENDLY) && (!FriendChallengeMgr.Get().DidReceiveChallenge() || FriendChallengeMgr.Get().DidChallengeeAccept()))
		{
			FriendChallengeMgr.Get().CancelChallenge();
		}
		if (IsInDeckEditMode())
		{
			Navigation.Pop();
		}
	}

	public void RefreshDataBasedUI(float animDelay = 0f)
	{
		if (m_currentlyShowingMode == TavernBrawlStatus.TB_STATUS_IN_REWARDS || m_tavernBrawlHasEndedDialogActive)
		{
			return;
		}
		RefreshTavernBrawlInfo(animDelay);
		if (m_currentMission == null)
		{
			return;
		}
		if (m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING && !FiresideGatheringManager.Get().IsCheckedIn)
		{
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
			return;
		}
		UpdateRecordUI();
		if (m_currentMission.brawlMode == TavernBrawlMode.TB_MODE_HEROIC && !m_firstTimeIntroductionPopupShowing && !Options.Get().GetBool(Option.HAS_SEEN_HEROIC_BRAWL, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("TavernBrawlDisplay.RefreshDataBasedUI:" + Option.HAS_SEEN_HEROIC_BRAWL))
		{
			m_firstTimeIntroductionPopupShowing = true;
			StartCoroutine(DoFirstTimeHeroicIntro());
		}
		else
		{
			if (m_firstTimeIntroductionPopupShowing)
			{
				return;
			}
			TavernBrawlStatus playerStatus = TavernBrawlManager.Get().PlayerStatus;
			if (playerStatus != TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED)
			{
				StoreManager.Get().HideStore(ShopType.TAVERN_BRAWL_STORE);
			}
			switch (playerStatus)
			{
			case TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED:
				StartCoroutine(ShowPurchaseScreen());
				return;
			case TavernBrawlStatus.TB_STATUS_IN_REWARDS:
				StartCoroutine(ShowRewardsScreen());
				return;
			default:
				if (m_currentMission != null && m_currentMission.IsSessionBased)
				{
					Debug.LogErrorFormat("TavernBrawlDisplay.UpdateDisplayState(): don't know how to handle currentStatus={0}. Kicking to HUB", playerStatus);
					if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
					{
						SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
					}
					AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
					if (m_currentMission.brawlMode == TavernBrawlMode.TB_MODE_HEROIC)
					{
						popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR_TITLE");
						popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR");
					}
					else
					{
						popupInfo.m_headerText = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR_TITLE");
						popupInfo.m_text = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR");
					}
					popupInfo.m_responseCallback = delegate
					{
						TavernBrawlManager.Get().RefreshServerData();
					};
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
					popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
					DialogManager.Get().ShowPopup(popupInfo);
					return;
				}
				break;
			case TavernBrawlStatus.TB_STATUS_ACTIVE:
				break;
			}
			ShowActiveScreen(animDelay);
		}
	}

	public bool IsInDeckEditMode()
	{
		return m_deckBeingEdited > 0;
	}

	public bool IsInRewards()
	{
		return m_currentlyShowingMode == TavernBrawlStatus.TB_STATUS_IN_REWARDS;
	}

	public bool BackFromDeckEdit(bool animate)
	{
		if (!IsInDeckEditMode())
		{
			return false;
		}
		if (animate)
		{
			PresenceMgr.Get().SetPrevStatus();
		}
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != 0)
		{
			if (TavernBrawlManager.Get().CurrentDeck() == null)
			{
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARDS);
			}
			else
			{
				CollectionManager.Get().GetCollectibleDisplay().m_pageManager.JumpToCollectionClassPage(TavernBrawlManager.Get().CurrentDeck().GetClass());
			}
		}
		m_tavernBrawlTray.ToggleTraySlider(show: true, null, animate);
		RefreshStateBasedUI(animate);
		m_deckBeingEdited = 0L;
		BnetBar.Get().RefreshCurrency();
		FriendChallengeMgr.Get().UpdateMyAvailability();
		UpdateEditOrCreate();
		if (!UniversalInputManager.UsePhoneUI)
		{
			m_editDeckButton.SetText(GameStrings.Get("GLUE_EDIT"));
			if (m_editIcon != null)
			{
				m_editIcon.SetActive(value: true);
			}
			if (m_deleteIcon != null)
			{
				m_deleteIcon.SetActive(value: false);
			}
		}
		CollectionDeckTray.Get().ExitEditDeckModeForTavernBrawl();
		return true;
	}

	public static bool IsTavernBrawlOpen()
	{
		if (!SceneMgr.Get().IsInTavernBrawlMode())
		{
			return false;
		}
		if (s_instance == null)
		{
			return false;
		}
		return true;
	}

	public static bool IsTavernBrawlEditing()
	{
		if (IsTavernBrawlOpen())
		{
			return s_instance.IsInDeckEditMode();
		}
		return false;
	}

	public static bool IsTavernBrawlViewing()
	{
		if (IsTavernBrawlOpen())
		{
			return !s_instance.IsInDeckEditMode();
		}
		return false;
	}

	public void ValidateDeck()
	{
		if (m_currentMission == null)
		{
			DisablePlayButton();
		}
		else
		{
			if (!m_currentMission.canCreateDeck)
			{
				return;
			}
			if (TavernBrawlManager.Get().HasValidDeckForCurrent())
			{
				if (TavernBrawlManager.Get().PlayerStatus == TavernBrawlStatus.TB_STATUS_ACTIVE)
				{
					if (m_playButton != null)
					{
						m_playButton.Enable();
					}
					m_FiresideGatheringPlayButtonLantern.SetLanternLit(lit: true);
				}
				if (m_editDeckHighlight != null)
				{
					m_editDeckHighlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
				}
			}
			else
			{
				DisablePlayButton();
				if (m_editDeckHighlight != null)
				{
					m_editDeckHighlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
				}
			}
		}
	}

	public void EnablePlayButton()
	{
		if (m_currentMission == null || m_currentMission.canCreateDeck)
		{
			ValidateDeck();
			return;
		}
		if (m_playButton != null)
		{
			m_playButton.Enable();
		}
		if (m_FiresideGatheringPlayButtonLantern != null)
		{
			m_FiresideGatheringPlayButtonLantern.SetLanternLit(lit: true);
		}
	}

	private void DisablePlayButton()
	{
		if (m_playButton != null)
		{
			m_playButton.Disable();
		}
		if (m_FiresideGatheringPlayButtonLantern != null)
		{
			m_FiresideGatheringPlayButtonLantern.SetLanternLit(lit: false);
		}
	}

	public void EnableBackButton(bool enable)
	{
		if (m_backButton != null)
		{
			m_backButton.SetEnabled(enable);
			m_backButton.Flip(enable);
		}
	}

	public void OnHeroPickerClosed()
	{
		if (m_dungeonCrawlDisplay != null && m_dungeonCrawlServices != null)
		{
			m_dungeonCrawlDisplay.EnablePlayButton();
			return;
		}
		EnablePlayButton();
		EnableBackButton(enable: true);
	}

	public AdventureDef GetAdventureDef(AdventureDbId id)
	{
		return m_adventureDefCache.GetDef(id);
	}

	public AdventureWingDef GetAdventureWingDef(WingDbId id)
	{
		return m_adventureWingDefCache.GetDef(id);
	}

	private void RefreshTavernBrawlInfo(float animDelay)
	{
		UpdateEditOrCreate();
		m_currentMission = TavernBrawlManager.Get().CurrentMission();
		if (m_currentMission == null || m_currentMission.missionId < 0)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_TAVERN_BRAWL_HAS_ENDED_HEADER");
			popupInfo.m_text = GameStrings.Get("GLUE_TAVERN_BRAWL_HAS_ENDED_TEXT");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_responseCallback = RefreshTavernBrawlInfo_ConfirmEnded;
			popupInfo.m_offset = new Vector3(0f, 104f, 0f);
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			DialogManager.Get().ShowPopup(popupInfo);
			m_tavernBrawlHasEndedDialogActive = true;
			return;
		}
		if (m_rewardChestDeprecated || m_currentMission.rewardType == RewardType.REWARD_CHEST || m_currentMission.rewardType == RewardType.REWARD_NONE || DemoMgr.Get().IsDemo())
		{
			m_rewardChest.gameObject.SetActive(value: false);
		}
		if (m_currentMission.IsSessionBased)
		{
			if (m_sessionWinLocationBone != null)
			{
				m_winsBanner.transform.position = m_sessionWinLocationBone.transform.position;
			}
			if (m_lossMarks != null)
			{
				m_lossesRoot.SetActive(value: true);
				m_lossMarks.Init(m_currentMission.maxLosses);
			}
			if (m_currentMission.brawlMode == TavernBrawlMode.TB_MODE_HEROIC)
			{
				m_TavernBrawlHeadline.Text = GameStrings.Get("GLOBAL_HEROIC_BRAWL");
			}
			else
			{
				m_TavernBrawlHeadline.Text = GameStrings.Get("GLOBAL_BRAWLISEUM");
			}
		}
		else
		{
			if (m_normalWinLocationBone != null)
			{
				m_winsBanner.transform.position = m_normalWinLocationBone.transform.position;
			}
			if (m_lossMarks != null)
			{
				m_lossesRoot.SetActive(value: false);
			}
			m_TavernBrawlHeadline.Text = GameStrings.Get("GLOBAL_TAVERN_BRAWL");
		}
		if (m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
		{
			m_TavernBrawlHeadline.Text = GameStrings.Get("GLOBAL_FIRESIDE_BRAWL");
		}
		if (DemoMgr.Get().IsExpoDemo())
		{
			string str = Vars.Key("Demo.Header").GetStr("");
			if (!string.IsNullOrEmpty(str))
			{
				m_TavernBrawlHeadline.Text = str;
			}
		}
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(m_currentMission.missionId);
		if (record != null)
		{
			m_chalkboardHeader.Text = record.Name;
			m_chalkboardInfo.Text = (((bool)UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(record.ShortDescription)) ? record.ShortDescription : record.Description);
		}
		LoadChalkboardTexture();
		CancelInvoke("UpdateTimeText");
		InvokeRepeating("UpdateTimeText", 0.1f, 0.1f);
		UpdateTimeText();
	}

	private void RefreshTavernBrawlInfo_ConfirmEnded(AlertPopup.Response response, object userData)
	{
		if (!(s_instance == null))
		{
			Navigation.Clear();
			ExitScene();
		}
	}

	private void SetUIForFriendlyChallenge(bool isTavernBrawlChallenge)
	{
		string key = "GLUE_BRAWL";
		bool flag = m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING;
		if (ShouldPlayButtonShowOpponentPickerTray())
		{
			key = "GLUE_CHOOSE_OPPONENT";
		}
		else if (flag)
		{
			key = "GLUE_BRAWL";
		}
		else if (TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			key = "GLUE_CHOOSE";
		}
		else if (isTavernBrawlChallenge && !DemoMgr.Get().IsExpoDemo())
		{
			key = "GLUE_BRAWL_FRIEND";
		}
		m_playButton.SetText(GameStrings.Get(key));
		bool flag2 = true;
		if (m_rewardChestDeprecated)
		{
			flag2 = false;
		}
		else
		{
			if (isTavernBrawlChallenge)
			{
				flag2 = false;
				NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
				if (netObject != null && netObject.FriendWeekAllowsTavernBrawlRecordUpdate && SpecialEventManager.Get().IsEventActive(SpecialEventType.FRIEND_WEEK, activeIfDoesNotExist: false))
				{
					flag2 = true;
				}
			}
			flag2 = flag2 && m_currentMission.rewardType != RewardType.REWARD_CHEST && m_currentMission.rewardType != RewardType.REWARD_NONE;
			if (DemoMgr.Get().IsDemo())
			{
				flag2 = false;
			}
		}
		m_rewardChest.gameObject.SetActive(flag2);
		m_winsBanner.SetActive(!isTavernBrawlChallenge && !flag);
		if (m_lossMarks != null)
		{
			m_lossMarks.gameObject.SetActive(!isTavernBrawlChallenge);
		}
		if (!(m_editDeckButton != null))
		{
			return;
		}
		if (!m_originalEditTextColor.HasValue)
		{
			m_originalEditTextColor = m_editText.TextColor;
		}
		if (isTavernBrawlChallenge)
		{
			m_editText.TextColor = m_disabledTextColor;
			m_editDeckButton.SetEnabled(enabled: false);
		}
		else
		{
			m_editText.TextColor = m_originalEditTextColor.Value;
			m_editDeckButton.SetEnabled(enabled: true);
		}
		if (m_editIcon != null)
		{
			Material material = m_editIcon.GetComponent<Renderer>().GetMaterial();
			if (!m_originalEditIconColor.HasValue)
			{
				m_originalEditIconColor = material.color;
			}
			if (isTavernBrawlChallenge)
			{
				material.color = m_disabledTextColor;
			}
			else
			{
				material.color = m_originalEditIconColor.Value;
			}
		}
	}

	private void LoadChalkboardTexture()
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(m_currentMission.missionId);
		if (!(m_chalkboard != null) || !(m_chalkboard.GetComponent<MeshRenderer>() != null) || !(m_chalkboard.GetComponent<MeshRenderer>().GetMaterial() != null))
		{
			return;
		}
		Material material = m_chalkboard.GetComponent<MeshRenderer>().GetMaterial();
		string text = null;
		UnityEngine.Vector2 value = UnityEngine.Vector2.zero;
		if (record != null)
		{
			if (!string.IsNullOrEmpty(record.ScriptObject))
			{
				AssetReference assetReference = new AssetReference(record.ScriptObject);
				using AssetHandle<ScenarioData> assetHandle = AssetLoader.Get().LoadAsset<ScenarioData>(assetReference);
				if (assetHandle == null)
				{
					Debug.LogErrorFormat("Pointing to {0} but unable to load.  Rebuilding RAD will fix.", assetReference.ToString());
				}
				else if (PlatformSettings.Screen == ScreenCategory.Phone)
				{
					text = assetHandle.Asset.m_Texture_Phone;
					value.y = assetHandle.Asset.m_Texture_Phone_offsetY;
				}
				else
				{
					text = assetHandle.Asset.m_Texture;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = record.TbTexture;
				if (PlatformSettings.Screen == ScreenCategory.Phone)
				{
					text = record.TbTexturePhone;
					value.y = (float)record.TbTexturePhoneOffsetY;
				}
			}
			m_chalkboardTexture = (string.IsNullOrEmpty(text) ? null : AssetLoader.Get().LoadTexture(text));
		}
		if (m_chalkboardTexture == null)
		{
			bool canCreateDeck = m_currentMission.canCreateDeck;
			text = (canCreateDeck ? DEFAULT_CHALKBOARD_TEXTURE_NAME_WITH_DECK : DEFAULT_CHALKBOARD_TEXTURE_NAME_NO_DECK);
			value = (canCreateDeck ? DEFAULT_CHALKBOARD_TEXTURE_OFFSET_WITH_DECK : DEFAULT_CHALKBOARD_TEXTURE_OFFSET_NO_DECK);
			m_chalkboardTexture = AssetLoader.Get().LoadTexture(text);
		}
		if (m_chalkboardTexture != null)
		{
			material.SetTexture("_TopTex", m_chalkboardTexture);
			material.SetTextureOffset("_MainTex", value);
		}
	}

	private void UpdateChalkboardVisual(float animDelay)
	{
		if (m_chalkboardTexture == null)
		{
			LoadChalkboardTexture();
		}
		StartCoroutine(WaitThenPlayWipeAnim(m_doFirstSeenAnimations ? animDelay : 0f));
	}

	private void UpdateTimeText()
	{
		if (DemoMgr.Get().IsExpoDemo())
		{
			return;
		}
		if (m_currentMission != null && m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING)
		{
			m_chalkboardEndInfo.Text = "";
			return;
		}
		string endingTimeText = TavernBrawlManager.Get().EndingTimeText;
		if (endingTimeText == null)
		{
			CancelInvoke("UpdateTimeText");
		}
		else
		{
			m_chalkboardEndInfo.Text = endingTimeText;
		}
	}

	private void UpdateRecordUI()
	{
		m_numWins.Text = TavernBrawlManager.Get().GamesWon.ToString();
		if (m_currentMission.IsSessionBased)
		{
			int gamesLost = TavernBrawlManager.Get().GamesLost;
			m_lossMarks.SetNumMarked(gamesLost);
		}
		else if (TavernBrawlManager.Get().RewardProgress >= m_currentMission.RewardTriggerQuota)
		{
			m_rewardChest.GetComponent<Renderer>().SetMaterial(m_chestOpenMaterial);
			m_rewardHighlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
			m_rewardChest.SetEnabled(enabled: false);
		}
	}

	private IEnumerator DoFirstTimeHeroicIntro()
	{
		Box.Get().SetToIgnoreFullScreenEffects(ignoreEffects: true);
		if (!UniversalInputManager.UsePhoneUI)
		{
			DisablePlayButton();
		}
		string text = GameStrings.Get("GLUE_HEROIC_BRAWL_INTRO");
		while (SceneMgr.Get().IsTransitioning())
		{
			yield return 0;
		}
		if (!BannerManager.Get().ShowBanner(HEROIC_BRAWL_DIFFICULTY_WARNING_POPUP, null, text, OnFirstTimeIntroClosed, OnFirstTimeIntroCreated))
		{
			Log.TavernBrawl.PrintWarning("TavernBrawlManager.DoFirstTimeHeroicIntro: First time popup failed to show.");
			ExitScene();
		}
	}

	private static void OnFirstTimeIntroCreated(BannerPopup popup)
	{
		TavernBrawlDisplay tavernBrawlDisplay = Get();
		if (!(tavernBrawlDisplay == null))
		{
			tavernBrawlDisplay.m_firstTimeIntroBanner = popup;
		}
	}

	private static void OnFirstTimeIntroClosed()
	{
		Box.Get().SetToIgnoreFullScreenEffects(ignoreEffects: false);
		TavernBrawlDisplay tavernBrawlDisplay = Get();
		if (!(tavernBrawlDisplay == null))
		{
			tavernBrawlDisplay.m_firstTimeIntroBanner = null;
			tavernBrawlDisplay.m_firstTimeIntroductionPopupShowing = false;
			Options.Get().SetBool(Option.HAS_SEEN_HEROIC_BRAWL, val: true);
			if (SceneMgr.Get().IsInTavernBrawlMode())
			{
				tavernBrawlDisplay.RefreshDataBasedUI();
			}
		}
	}

	private void RegisterListeners()
	{
		SceneMgr.Get().RegisterScenePreUnloadEvent(OnScenePreUnload);
		CollectionManager.Get().RegisterDeckCreatedListener(OnDeckCreated);
		CollectionManager.Get().RegisterDeckDeletedListener(OnDeckDeleted);
		CollectionManager.Get().RegisterDeckContentsListener(OnDeckContents);
		CollectionManager.Get().RegisterCollectionChangedListener(OnCollectionChanged);
		FriendChallengeMgr.Get().AddChangedListener(OnFriendChallengeChanged);
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		TavernBrawlManager.Get().OnTavernBrawlUpdated += OnTavernBrawlUpdated;
		FiresideGatheringManager.Get().OnLeaveFSG += OnLeaveFSG;
		if ((m_currentMission == null || !m_currentMission.canEditDeck) && SceneMgr.Get().GetMode() != SceneMgr.Mode.FIRESIDE_GATHERING)
		{
			Navigation.Push(OnNavigateBack);
		}
	}

	private void UnregisterListeners()
	{
		SceneMgr.UnregisterScenePreUnloadEventFromInstance(OnScenePreUnload);
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager != null)
		{
			collectionManager.RemoveDeckCreatedListener(OnDeckCreated);
			collectionManager.RemoveDeckDeletedListener(OnDeckDeleted);
			collectionManager.RemoveDeckContentsListener(OnDeckContents);
			collectionManager.RemoveCollectionChangedListener(OnCollectionChanged);
		}
		FriendChallengeMgr.RemoveChangedListenerFromInstance(OnFriendChallengeChanged);
		GameMgr.Get()?.UnregisterFindGameEvent(OnFindGameEvent);
		if (TavernBrawlManager.Get() != null)
		{
			TavernBrawlManager.Get().OnTavernBrawlUpdated -= OnTavernBrawlUpdated;
		}
		if (FiresideGatheringManager.Get() != null)
		{
			FiresideGatheringManager.Get().OnLeaveFSG -= OnLeaveFSG;
		}
	}

	private void Start_ShowAttentionGrabbers()
	{
		if (m_currentMission == null)
		{
			return;
		}
		bool flag = UserAttentionManager.CanShowAttentionGrabber("TavernBrawlDisplay.Show");
		int latestSeenTavernBrawlChalkboard = TavernBrawlManager.Get().LatestSeenTavernBrawlChalkboard;
		if (latestSeenTavernBrawlChalkboard == 0)
		{
			m_doFirstSeenAnimations = true;
			if (flag && !NotificationManager.Get().HasSoundPlayedThisSession("VO_INNKEEPER_TAVERNBRAWL_WELCOME1_27.prefab:094070b7fecad8548b0b8fdb02bde052") && m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_TAVERN_BRAWL)
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_TAVERNBRAWL_WELCOME1_27"), "VO_INNKEEPER_TAVERNBRAWL_WELCOME1_27.prefab:094070b7fecad8548b0b8fdb02bde052");
				NotificationManager.Get().ForceAddSoundToPlayedList("VO_INNKEEPER_TAVERNBRAWL_WELCOME1_27.prefab:094070b7fecad8548b0b8fdb02bde052");
			}
		}
		else if (latestSeenTavernBrawlChalkboard < m_currentMission.seasonId)
		{
			m_doFirstSeenAnimations = true;
			if (m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_TAVERN_BRAWL)
			{
				int @int = Options.Get().GetInt(Option.TIMES_SEEN_TAVERNBRAWL_CRAZY_RULES_QUOTE);
				if (flag && !NotificationManager.Get().HasSoundPlayedThisSession("VO_INNKEEPER_TAVERNBRAWL_DESC2_30.prefab:498657df8d08bc1468bfd1ad9f74ccac") && @int < 3)
				{
					NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_TAVERNBRAWL_DESC2_30"), "VO_INNKEEPER_TAVERNBRAWL_DESC2_30.prefab:498657df8d08bc1468bfd1ad9f74ccac");
					NotificationManager.Get().ForceAddSoundToPlayedList("VO_INNKEEPER_TAVERNBRAWL_DESC2_30.prefab:498657df8d08bc1468bfd1ad9f74ccac");
					@int++;
					Options.Get().SetInt(Option.TIMES_SEEN_TAVERNBRAWL_CRAZY_RULES_QUOTE, @int);
				}
			}
		}
		if (flag && latestSeenTavernBrawlChalkboard != m_currentMission.seasonId)
		{
			TavernBrawlManager.Get().LatestSeenTavernBrawlChalkboard = m_currentMission.seasonId;
		}
	}

	private void OnTavernBrawlUpdated()
	{
		m_currentMission = TavernBrawlManager.Get().CurrentMission();
		RefreshDataBasedUI();
	}

	private void OnLeaveFSG(FSGConfig fsg)
	{
		RefreshDataBasedUI();
	}

	private IEnumerator ShowPurchaseScreen()
	{
		if (TavernBrawlManager.Get().CurrentTavernBrawlSeasonNewSessionsClosedInSeconds <= 0)
		{
			Log.TavernBrawl.Print("TavernBrawlManager.ShowPurchaseScreen: New sessions in this season closed! Kicking out of TB");
			StoreManager.Get().HideStore(ShopType.TAVERN_BRAWL_STORE);
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SIGNUPS_CLOSED_TITLE");
			popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SIGNUPS_CLOSED");
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			DialogManager.Get().ShowPopup(popupInfo);
		}
		else if (m_currentlyShowingMode != TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED)
		{
			m_currentlyShowingMode = TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED;
			if (!UniversalInputManager.UsePhoneUI)
			{
				DisablePlayButton();
			}
			while (SceneMgr.Get().IsTransitioning())
			{
				yield return 0;
			}
			if (m_currentlyShowingMode == TavernBrawlStatus.TB_STATUS_TICKET_REQUIRED)
			{
				StoreManager.Get().StartTavernBrawlTransaction(OnStoreBackButtonPressed, isTotallyFake: false);
			}
		}
	}

	private void ShowActiveScreen(float animDelay)
	{
		if (m_currentlyShowingMode == TavernBrawlStatus.TB_STATUS_ACTIVE)
		{
			return;
		}
		m_currentlyShowingMode = TavernBrawlStatus.TB_STATUS_ACTIVE;
		Start_ShowAttentionGrabbers();
		UpdateChalkboardVisual(animDelay);
		UpdateDeckUI(animate: false);
		if (!m_currentMission.IsDungeonRun)
		{
			return;
		}
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(m_currentMission.missionId);
		AdventureDbId adventureId = (AdventureDbId)record.AdventureId;
		AdventureModeDbId adventureModeId = (AdventureModeDbId)record.ModeId;
		m_adventureDefCache.LoadDefForId(adventureId);
		m_adventureWingDefCache.LoadDefForId((WingDbId)record.WingId);
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY)
		{
			DungeonCrawlUtil.ClearDungeonRunServerData(adventureId, adventureModeId);
		}
		if (DungeonCrawlUtil.IsDungeonRunDataReady(adventureId, adventureModeId))
		{
			StartDungeonRunIfInProgress(adventureId, adventureModeId);
			return;
		}
		DisablePlayButton();
		DungeonCrawlUtil.LoadDungeonRunData(adventureId, adventureModeId, delegate(bool success)
		{
			if (!(base.gameObject == null))
			{
				EnablePlayButton();
				if (success)
				{
					AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventureId, (int)adventureModeId);
					if (adventureDataRecord != null)
					{
						DungeonCrawlUtil.MigrateDungeonCrawlSubkeys((GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey, (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey);
					}
					StartDungeonRunIfInProgress(adventureId, adventureModeId);
				}
			}
		});
	}

	private void StartDungeonRunIfInProgress(AdventureDbId adventureId, AdventureModeDbId modeId)
	{
		if (DungeonCrawlUtil.IsDungeonRunInProgress(adventureId, modeId))
		{
			StartDungeonRun();
		}
	}

	private IEnumerator ShowRewardsScreen()
	{
		if (m_currentlyShowingMode == TavernBrawlStatus.TB_STATUS_IN_REWARDS)
		{
			yield break;
		}
		m_currentlyShowingMode = TavernBrawlStatus.TB_STATUS_IN_REWARDS;
		if (!UniversalInputManager.UsePhoneUI)
		{
			DisablePlayButton();
		}
		if (!TavernBrawlManager.Get().CurrentSession.HasChest)
		{
			Log.TavernBrawl.PrintError("TavernBrawlManager.ShowHeroicRewardsScreen: Server said we're in rewards but no rewards were specified!");
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			if (m_currentMission != null && m_currentMission.brawlMode == TavernBrawlMode.TB_MODE_HEROIC)
			{
				popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR_TITLE");
				popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_SESSION_ERROR");
			}
			else
			{
				popupInfo.m_headerText = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR_TITLE");
				popupInfo.m_text = GameStrings.Get("GLUE_BRAWLISEUM_SESSION_ERROR");
			}
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
			popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
			DialogManager.Get().ShowPopup(popupInfo);
		}
		else
		{
			while (SceneMgr.Get().IsTransitioning())
			{
				yield return null;
			}
			if (m_PhoneDeckTrayView != null)
			{
				m_PhoneDeckTrayView.gameObject.GetComponent<SlidingTray>().HideTray();
			}
			Transform rewardBone = ((TavernBrawlManager.Get().CurrentSeasonBrawlMode == TavernBrawlMode.TB_MODE_HEROIC) ? m_rewardBoxesBone.transform : m_sessionRewardBoxesBone.transform);
			RewardUtils.ShowTavernBrawlRewards(TavernBrawlManager.Get().GamesWon, TavernBrawlManager.Get().CurrentSessionRewards, rewardBone, OnRewardsDone);
		}
	}

	private void OnRewardsDone()
	{
		if (!(this == null) && !(base.gameObject == null))
		{
			Network.Get().AckTavernBrawlSessionRewards();
			OnOpenRewardsComplete();
		}
	}

	public void OnOpenRewardsComplete()
	{
		ExitScene();
	}

	private void ExitScene()
	{
		m_tavernBrawlTray.m_animateBounce = false;
		m_tavernBrawlTray.ShowTray();
		GameMgr.Get().CancelFindGame();
		if (!UniversalInputManager.UsePhoneUI)
		{
			DisablePlayButton();
		}
		StoreManager.Get().HideStore(ShopType.TAVERN_BRAWL_STORE);
		SceneMgr.Mode mode = ((FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.NONE) ? SceneMgr.Mode.HUB : SceneMgr.Mode.FIRESIDE_GATHERING);
		SceneMgr.Get().SetNextMode(mode);
	}

	private void UpdateDeckUI(bool animate)
	{
		UpdateDeckPanels(animate);
		ValidateDeck();
	}

	private bool OnNavigateBack()
	{
		if (m_backButton != null && !m_backButton.IsEnabled())
		{
			return false;
		}
		ExitScene();
		return true;
	}

	private void OnBackButton()
	{
		if (m_backButton.IsEnabled())
		{
			Navigation.GoBack();
		}
	}

	private void OnStoreBackButtonPressed(bool authorizationBackButtonPressed, object userData)
	{
		ExitScene();
	}

	private void RefreshStateBasedUI(bool animate)
	{
		UpdateDeckUI(animate);
	}

	private void UpdateEditOrCreate()
	{
		bool flag = m_currentMission != null && m_currentMission.canCreateDeck;
		bool num = m_currentMission != null && m_currentMission.canEditDeck && !TavernBrawlManager.Get().IsDeckLocked;
		bool flag2 = TavernBrawlManager.Get().HasCreatedDeck();
		bool isDeckLocked = TavernBrawlManager.Get().IsDeckLocked;
		bool num2 = (bool)UniversalInputManager.UsePhoneUI && isDeckLocked;
		bool flag3 = flag && !flag2;
		bool flag4 = num2;
		bool active = num && flag && flag2 && !flag4;
		if (m_viewDeckButton != null)
		{
			m_viewDeckButton.gameObject.SetActive(flag4);
		}
		if (m_editDeckButton != null)
		{
			m_editDeckButton.gameObject.SetActive(active);
			if (TavernBrawlManager.Get().IsDeckLocked)
			{
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					m_PhoneDeckTrayView.Initialize();
					InitializeDeckTrayManaCurve();
					LoadAndPositionPhoneDeckTrayHeroCard();
				}
				else
				{
					CollectionDeckTray.Get().m_cardsContent.UpdateDeckCompleteHighlight();
					if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && GameMgr.Get().WasTavernBrawl() && TavernBrawlManager.Get().GamesWon + TavernBrawlManager.Get().GamesLost == 1)
					{
						StartCoroutine(DoDeckTrayLockedAnimation());
					}
					else
					{
						ShowDeckTrayLocked();
					}
				}
			}
			if (m_editIcon != null)
			{
				m_editIcon.SetActive(value: true);
			}
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			if (m_createDeckButton != null)
			{
				m_createDeckButton.gameObject.SetActive(flag3);
			}
		}
		else
		{
			if (m_panelWithCreateDeck != null)
			{
				m_panelWithCreateDeck.SetActive(flag3);
			}
			if (m_fullPanel != null)
			{
				m_fullPanel.SetActive(!flag3);
			}
		}
		if (m_createDeckHighlight != null)
		{
			if (!m_createDeckHighlight.gameObject.activeInHierarchy && flag3)
			{
				Debug.LogWarning("Attempting to activate m_createDeckHighlight, but it is inactive! This will not behave correctly!");
			}
			m_createDeckHighlight.ChangeState(flag3 ? ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE : ActorStateType.HIGHLIGHT_OFF);
		}
	}

	private void LoadAndPositionPhoneDeckTrayHeroCard()
	{
		if (!(m_chosenHero != null))
		{
			CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
			if (collectionDeck == null)
			{
				Log.TavernBrawl.PrintError("TavernBrawlManager.LoadAndPositionPhoneDeckTrayHeroCard: No deck found but trying to load the deck tray!");
				return;
			}
			string heroCardID = collectionDeck.HeroCardID;
			GameUtils.LoadAndPositionCardActor("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", heroCardID, collectionDeck.HeroPremium, OnHeroActorLoaded);
		}
	}

	private void OnHeroActorLoaded(Actor actor)
	{
		m_chosenHero = actor;
		m_chosenHero.transform.parent = m_SocketHeroBone.transform;
		m_chosenHero.transform.localPosition = Vector3.zero;
		m_chosenHero.transform.localScale = Vector3.one;
	}

	private IEnumerator DoDeckTrayLockedAnimation()
	{
		while (SceneMgr.Get().IsTransitioning())
		{
			yield return 0;
		}
		yield return new WaitForSeconds(1.5f);
		ShowDeckTrayLocked();
	}

	private void ShowDeckTrayLocked()
	{
		m_LockedDeckTray.enabled = true;
		m_LockedDeckTooltipZone.GetComponent<BoxCollider>().enabled = true;
	}

	private void InitializeDeckTrayManaCurve()
	{
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		if (collectionDeck == null)
		{
			return;
		}
		foreach (CollectionDeckSlot slot in collectionDeck.GetSlots())
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(slot.CardID);
			for (int i = 0; i < slot.Count; i++)
			{
				AddCardToManaCurve(entityDef);
			}
		}
	}

	public void AddCardToManaCurve(EntityDef entityDef)
	{
		if (m_ManaCurvePhone == null)
		{
			Debug.LogWarning($"TavernBrawlDisplay.AddCardToManaCurve({entityDef}) - m_manaCurve is null");
		}
		else
		{
			m_ManaCurvePhone.AddCardToManaCurve(entityDef);
		}
	}

	private void UpdateDeckPanels(bool animate = true)
	{
		UpdateDeckPanels(m_currentMission != null && m_currentMission.canCreateDeck && TavernBrawlManager.Get().HasCreatedDeck(), animate);
	}

	private void UpdateDeckPanels(bool hasDeck, bool animate)
	{
		if (m_cardListPanel != null)
		{
			bool flag = !hasDeck;
			if (animate && !flag)
			{
				m_createDeckButton.gameObject.SetActive(value: false);
				m_createDeckHighlight.gameObject.SetActive(value: false);
			}
			else if (flag)
			{
				m_createDeckButton.gameObject.SetActive(value: true);
				m_createDeckHighlight.gameObject.SetActive(value: true);
			}
			m_cardListPanel.ToggleTraySlider(flag, null, animate);
		}
		if (m_cardCountPanelAnim != null && m_cardCountPanelAnimOpen != hasDeck)
		{
			m_cardCountPanelAnim.Play(hasDeck ? CARD_COUNT_PANEL_OPEN_ANIM : CARD_COUNT_PANEL_CLOSE_ANIM);
			m_cardCountPanelAnimOpen = hasDeck;
		}
	}

	private void CreateDeck()
	{
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.TAVERN_BRAWL_DECKEDITOR);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.EnterSelectNewDeckHeroMode();
		}
		HideChalkboardFX();
	}

	private void EditDeckButton_OnRelease(UIEvent e)
	{
		if (IsInDeckEditMode())
		{
			OnDeleteButtonPressed();
			return;
		}
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.TAVERN_BRAWL_DECKEDITOR);
		SwitchToEditDeckMode(TavernBrawlManager.Get().CurrentDeck());
	}

	private void ViewDeckButton_OnRelease(UIEvent e)
	{
		m_PhoneDeckTrayView.gameObject.GetComponent<SlidingTray>().ShowTray();
	}

	private bool SwitchToEditDeckMode(CollectionDeck deck)
	{
		if (CollectionManager.Get().GetCollectibleDisplay() == null || deck == null)
		{
			return false;
		}
		m_tavernBrawlTray.HideTray();
		UpdateDeckPanels(hasDeck: true, animate: true);
		if (!UniversalInputManager.UsePhoneUI)
		{
			m_editDeckButton.gameObject.SetActive(m_currentMission.canEditDeck);
			m_editDeckButton.SetText(GameStrings.Get("GLUE_COLLECTION_DECK_DELETE"));
			if (m_editIcon != null)
			{
				m_editIcon.SetActive(value: false);
			}
			if (m_deleteIcon != null)
			{
				m_deleteIcon.SetActive(value: true);
			}
			m_editDeckHighlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
		m_deckBeingEdited = deck.ID;
		BnetBar.Get().RefreshCurrency();
		CollectionDeckTray.Get().EnterEditDeckModeForTavernBrawl(deck);
		FriendChallengeMgr.Get().UpdateMyAvailability();
		return true;
	}

	private void ShowNonSessionRewardPreview(UIEvent e)
	{
		if (m_currentMission == null)
		{
			return;
		}
		switch (m_currentMission.rewardType)
		{
		case RewardType.REWARD_BOOSTER_PACKS:
			if (m_rewardObject == null)
			{
				int num2 = (int)m_currentMission.RewardData2;
				BoosterDbfRecord record = GameDbf.Booster.GetRecord(num2);
				if (record == null)
				{
					Debug.LogErrorFormat("TavernBrawlDisplay.ShowReward() - no record found for booster {0}!", num2);
					return;
				}
				string packOpeningPrefab = record.PackOpeningPrefab;
				if (string.IsNullOrEmpty(packOpeningPrefab))
				{
					Debug.LogErrorFormat("TavernBrawlDisplay.ShowReward() - no prefab found for booster {0}!", num2);
					return;
				}
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(packOpeningPrefab, AssetLoadingOptions.IgnorePrefabPosition);
				if (gameObject == null)
				{
					Debug.LogError($"TavernBrawlDisplay.ShowReward() - failed to load prefab {packOpeningPrefab} for booster {num2}!");
					return;
				}
				m_rewardObject = gameObject;
				UnopenedPack component = gameObject.GetComponent<UnopenedPack>();
				if (component == null)
				{
					Debug.LogError($"TavernBrawlDisplay.ShowReward() - No UnopenedPack script found on prefab {packOpeningPrefab} for booster {num2}!");
					return;
				}
				GameUtils.SetParent(m_rewardObject, m_rewardContainer);
				NetCache.BoosterStack boosterStack = new NetCache.BoosterStack();
				boosterStack.Id = (int)m_currentMission.RewardData2;
				boosterStack.Count = (int)m_currentMission.RewardData1;
				component.SetBoosterStack(boosterStack);
			}
			break;
		case RewardType.REWARD_CARD_BACK:
			if (m_rewardObject == null)
			{
				int num = (int)m_currentMission.RewardData1;
				CardBackManager.LoadCardBackData loadCardBackData = CardBackManager.Get().LoadCardBackByIndex(num, unlit: false, "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", shadowActive: true);
				if (loadCardBackData == null)
				{
					Debug.LogErrorFormat("TavernBrawlDisplay.ShowReward() - Could not load cardback ID {0}!", num);
					return;
				}
				m_rewardObject = loadCardBackData.m_GameObject;
				GameUtils.SetParent(m_rewardObject, m_rewardContainer);
				m_rewardObject.transform.localScale = Vector3.one * 5.92f;
			}
			break;
		default:
			Debug.LogErrorFormat("Tavern Brawl reward type currently not supported! Add type {0} to TaverBrawlDisplay.ShowReward().", m_currentMission.rewardType);
			return;
		}
		m_rewardsPreview.SetActive(value: true);
		iTween.Stop(m_rewardsPreview);
		iTween.ScaleTo(m_rewardsPreview, iTween.Hash("scale", m_rewardsScale, "time", 0.15f));
	}

	private void HideNonSessionRewardPreview(UIEvent e)
	{
		iTween.Stop(m_rewardsPreview);
		iTween.ScaleTo(m_rewardsPreview, iTween.Hash("scale", Vector3.one * 0.01f, "time", 0.15f, "oncomplete", (Action<object>)delegate
		{
			m_rewardsPreview.SetActive(value: false);
		}));
	}

	private void StartDungeonRun()
	{
		DisablePlayButton();
		ScenarioDbfRecord scen = GameDbf.Scenario.GetRecord(m_currentMission.missionId);
		if (scen == null)
		{
			return;
		}
		DungeonCrawlUtil.LoadDungeonRunPrefab(delegate(GameObject go)
		{
			DungeonCrawlServices dungeonCrawlServices = DungeonCrawlUtil.CreateTavernBrawlDungeonCrawlServices((AdventureDbId)scen.AdventureId, (AdventureModeDbId)scen.ModeId, m_assetLoadingHelper);
			AdventureDungeonCrawlDisplay component = go.GetComponent<AdventureDungeonCrawlDisplay>();
			if ((bool)component)
			{
				m_dungeonCrawlServices = dungeonCrawlServices;
				m_dungeonCrawlDisplay = component;
				GameUtils.SetParent(go, base.transform);
				go.transform.position = new Vector3(-500f, 0f, 0f);
				component.StartRun(dungeonCrawlServices);
			}
		});
	}

	private void PlayButton_OnRelease(UIEvent e)
	{
		if (!Network.IsLoggedIn())
		{
			DialogManager.Get().ShowReconnectHelperDialog(delegate
			{
				PlayButton_OnRelease(e);
			});
		}
		else if (m_currentMission == null)
		{
			RefreshDataBasedUI();
		}
		else if (!SetRotationManager.Get().CheckForSetRotationRollover() && (PlayerMigrationManager.Get() == null || !PlayerMigrationManager.Get().CheckForPlayerMigrationRequired()))
		{
			if (ShouldPlayButtonShowOpponentPickerTray())
			{
				FiresideGatheringDisplay.Get().ShowOpponentPickerTray(EnablePlayButton);
				DisablePlayButton();
			}
			else if (m_currentMission.IsSessionBased && m_currentMission.canEditDeck && !TavernBrawlManager.Get().IsDeckLocked)
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = GameStrings.Get("GLUE_HEROIC_BRAWL_PLAY_CONFIRMATION_TITLE");
				popupInfo.m_text = GameStrings.Get("GLUE_HEROIC_BRAWL_PLAY_CONFIRMATION");
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
				popupInfo.m_confirmText = GameStrings.Get("GLUE_HEROIC_BRAWL_PLAY_CONFIRMATION_OK");
				popupInfo.m_cancelText = GameStrings.Get("GLUE_HEROIC_BRAWL_PLAY_CONFIRMATION_CANCEL");
				popupInfo.m_responseCallback = OnPlayButtonConfirmationResponse;
				popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
				DialogManager.Get().ShowPopup(popupInfo);
			}
			else
			{
				OnPlayButtonExecute();
			}
		}
	}

	private void OnPlayButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			OnPlayButtonExecute();
		}
	}

	private void OnPlayButtonExecute()
	{
		if (m_currentMission.IsDungeonRun)
		{
			StartDungeonRun();
		}
		else if (TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			bool flag = false;
			bool num = GuestHeroPickerDisplay.Get() != null || HeroPickerDisplay.Get() != null;
			if (!num)
			{
				flag = AssetLoader.Get().InstantiatePrefab(GetHeroPickerAssetStr(m_currentMission.missionId), delegate(AssetReference name, GameObject go, object data)
				{
					if (go == null)
					{
						Debug.LogError("Failed to load hero picker.");
					}
					else
					{
						PresenceMgr.Get().SetStatus(Global.PresenceStatus.TAVERN_BRAWL_DECKEDITOR);
						HideChalkboardFX();
					}
				});
			}
			if (!flag)
			{
				Log.All.PrintWarning("Failed to load hero picker.");
			}
			if (num)
			{
				Log.All.PrintWarning("Attempting to load HeroPickerDisplay a second time!");
				return;
			}
		}
		else if (m_currentMission.canCreateDeck)
		{
			if (!TavernBrawlManager.Get().HasValidDeckForCurrent())
			{
				Debug.LogError("Attempting to start a Tavern Brawl game without having a valid deck!");
				return;
			}
			CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
			if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
			{
				FriendChallengeMgr.Get().SelectDeck(collectionDeck.ID);
				FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_TAVERN_BRAWL_OPPONENT_WAITING_READY", OnFriendChallengeWaitingForOpponentDialogResponse);
			}
			else
			{
				TavernBrawlManager.Get().StartGame(collectionDeck.ID);
			}
		}
		else if (FriendChallengeMgr.Get().IsChallengeTavernBrawl())
		{
			FriendChallengeMgr.Get().SkipDeckSelection();
			FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_TAVERN_BRAWL_OPPONENT_WAITING_READY", OnFriendChallengeWaitingForOpponentDialogResponse);
		}
		else
		{
			TavernBrawlManager.Get().StartGame(0L);
		}
		DisablePlayButton();
		EnableBackButton(enable: false);
	}

	private string GetHeroPickerAssetStr(int scenarioId)
	{
		if (GameUtils.GetScenarioGuestHeroes(scenarioId).Count > 0)
		{
			return "GuestHeroPicker.prefab:3ecbc18da1de3ef4fa30532f90b20e59";
		}
		return "HeroPicker.prefab:59e2d2f899d09f4488a194df18967915";
	}

	private bool ShouldPlayButtonShowOpponentPickerTray()
	{
		if ((m_currentMission.BrawlType == BrawlType.BRAWL_TYPE_FIRESIDE_GATHERING || SceneMgr.Get().GetMode() == SceneMgr.Mode.FIRESIDE_GATHERING) && !GameUtils.IsAIMission(m_currentMission.missionId))
		{
			return !TavernBrawlManager.Get().SelectHeroBeforeMission();
		}
		return false;
	}

	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		if (prevMode switch
		{
			SceneMgr.Mode.FIRESIDE_GATHERING => FiresideGatheringManager.Get().InBrawlMode(), 
			SceneMgr.Mode.TAVERN_BRAWL => true, 
			_ => false, 
		} && m_firstTimeIntroBanner != null)
		{
			m_firstTimeIntroBanner.Close();
		}
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		switch (eventData.m_state)
		{
		case FindGameState.SERVER_GAME_STARTED:
			FriendChallengeMgr.Get().RemoveChangedListener(OnFriendChallengeChanged);
			break;
		case FindGameState.CLIENT_CANCELED:
		case FindGameState.CLIENT_ERROR:
		case FindGameState.BNET_QUEUE_CANCELED:
		case FindGameState.BNET_ERROR:
		case FindGameState.SERVER_GAME_CANCELED:
			HandleGameStartupFailure();
			break;
		}
		return false;
	}

	private void HandleGameStartupFailure()
	{
		if (!TavernBrawlManager.Get().SelectHeroBeforeMission())
		{
			EnablePlayButton();
			EnableBackButton(enable: true);
		}
	}

	private void OnDeleteButtonPressed()
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_HEADER");
		popupInfo.m_text = GameStrings.Get("GLUE_COLLECTION_DELETE_CONFIRM_DESC");
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_responseCallback = OnDeleteButtonConfirmationResponse;
		popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void OnDeleteButtonConfirmationResponse(AlertPopup.Response response, object userData)
	{
		if (response != AlertPopup.Response.CANCEL)
		{
			CollectionDeckTray.Get().DeleteEditingDeck();
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (collectionManagerDisplay != null)
			{
				collectionManagerDisplay.OnDoneEditingDeck();
			}
		}
	}

	private void OnDeckCreated(long deckID)
	{
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		if (collectionDeck != null && deckID == collectionDeck.ID)
		{
			SwitchToEditDeckMode(collectionDeck);
		}
	}

	private void OnDeckDeleted(CollectionDeck removedDeck)
	{
		if (removedDeck.ID == m_deckBeingEdited && IsTavernBrawlOpen())
		{
			StartCoroutine(WaitThenCreateDeck());
		}
	}

	private IEnumerator WaitThenPlayWipeAnim(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		if (m_chalkboard != null && TavernBrawlManager.Get().IsCurrentBrawlTypeActive && TavernBrawlManager.Get().IsCurrentBrawlAllDataReady)
		{
			m_chalkboard.GetComponent<PlayMakerFSM>().SendEvent(m_doFirstSeenAnimations ? "Wipe" : "QuickShow");
		}
		while (NotificationManager.Get().IsQuotePlaying)
		{
			yield return null;
		}
		if (m_doFirstSeenAnimations && m_currentMission.FirstTimeSeenCharacterDialogID > 0)
		{
			NarrativeManager.Get().PushDialogSequence(m_currentMission.FirstTimeSeenCharacterDialogSequence);
		}
		yield return new WaitForSeconds(1f);
	}

	private IEnumerator WaitThenCreateDeck()
	{
		yield return new WaitForEndOfFrame();
		CreateDeck();
		yield return new WaitForSeconds(0.4f);
		BackFromDeckEdit(animate: false);
	}

	private void OnCollectionChanged()
	{
		if (IsTavernBrawlViewing())
		{
			ValidateDeck();
		}
	}

	private void OnDeckContents(long deckID)
	{
		CollectionDeck collectionDeck = TavernBrawlManager.Get().CurrentDeck();
		if (collectionDeck != null && deckID == collectionDeck.ID && IsTavernBrawlOpen())
		{
			ValidateDeck();
		}
	}

	private void Awake_InitializeRewardDisplay()
	{
		if (m_rewardChestDeprecated)
		{
			return;
		}
		RewardType rewardType = ((m_currentMission != null) ? m_currentMission.rewardType : RewardType.REWARD_UNKNOWN);
		RewardTrigger rewardTrigger = ((m_currentMission != null) ? m_currentMission.rewardTrigger : RewardTrigger.REWARD_TRIGGER_UNKNOWN);
		string text = null;
		long num = 1L;
		switch (rewardType)
		{
		case RewardType.REWARD_CARD_BACK:
			text = ((rewardTrigger == RewardTrigger.REWARD_TRIGGER_WIN_GAME || rewardTrigger != RewardTrigger.REWARD_TRIGGER_FINISH_GAME) ? "GLUE_TAVERN_BRAWL_REWARD_DESC_CARDBACK" : "GLUE_TAVERN_BRAWL_REWARD_DESC_FINISH_CARDBACK");
			break;
		case RewardType.REWARD_BOOSTER_PACKS:
			num = m_currentMission.RewardData1;
			text = ((rewardTrigger == RewardTrigger.REWARD_TRIGGER_WIN_GAME || rewardTrigger != RewardTrigger.REWARD_TRIGGER_FINISH_GAME) ? "GLUE_TAVERN_BRAWL_REWARD_DESC" : "GLUE_TAVERN_BRAWL_REWARD_DESC_FINISH");
			break;
		}
		if (text != null)
		{
			if (m_currentMission.RewardTriggerQuota != 1)
			{
				text += "_QUOTA";
			}
			if (num != 1)
			{
				text += "_MULTIPLE_PACKS";
			}
			m_rewardsText.Text = GameStrings.Format(text, num, m_currentMission.RewardTriggerQuota);
		}
		if (m_rewardOffClickCatcher != null)
		{
			m_rewardChest.AddEventListener(UIEventType.PRESS, ShowNonSessionRewardPreview);
			m_rewardOffClickCatcher.AddEventListener(UIEventType.PRESS, HideNonSessionRewardPreview);
		}
		else
		{
			m_rewardChest.AddEventListener(UIEventType.ROLLOVER, ShowNonSessionRewardPreview);
			m_rewardChest.AddEventListener(UIEventType.ROLLOUT, HideNonSessionRewardPreview);
		}
		m_rewardsScale = m_rewardsPreview.transform.localScale;
		m_rewardsPreview.transform.localScale = Vector3.one * 0.01f;
		if (m_currentMission != null && TavernBrawlManager.Get().RewardProgress < m_currentMission.RewardTriggerQuota)
		{
			m_rewardHighlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
	}

	private void SetupUniversalButtons()
	{
		if (m_editDeckButton != null)
		{
			m_editDeckButton.AddEventListener(UIEventType.RELEASE, EditDeckButton_OnRelease);
		}
		if (m_createDeckButton != null)
		{
			m_createDeckButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				CreateDeck();
			});
		}
		if (m_backButton != null)
		{
			m_backButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				OnBackButton();
			});
		}
		if (m_LockedDeckTooltipTrigger != null && m_LockedDeckTooltipZone != null)
		{
			m_LockedDeckTooltipTrigger.AddEventListener(UIEventType.ROLLOVER, OnLockedTooltipRollover);
			m_LockedDeckTooltipTrigger.AddEventListener(UIEventType.ROLLOUT, OnLockedTooltipRollout);
		}
		if (m_viewDeckButton != null)
		{
			m_viewDeckButton.AddEventListener(UIEventType.RELEASE, ViewDeckButton_OnRelease);
		}
		m_playButton.AddEventListener(UIEventType.RELEASE, PlayButton_OnRelease);
		m_FiresideGatheringPlayButtonLantern.gameObject.SetActive(FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.FIRESIDE_BRAWL);
		m_FiresideGatheringPlayButtonLantern.SetLanternLit(lit: true);
	}

	private void OnLockedTooltipRollover(UIEvent e)
	{
		if (TavernBrawlManager.Get().IsDeckLocked)
		{
			m_LockedDeckTooltipZone.ShowLayerTooltip(GameStrings.Get("GLUE_LOCKED_DECK_TOOLTIP_TITLE"), GameStrings.Get("GLUE_LOCKED_DECK_TOOLTIP"));
		}
	}

	private void OnLockedTooltipRollout(UIEvent e)
	{
		m_LockedDeckTooltipZone.HideTooltip();
	}

	private void DoDungeonRunTransition()
	{
		Vector3 localPosition = base.transform.localPosition;
		Vector3 localPosition2 = base.transform.localPosition;
		localPosition2.x -= m_transitionStartingOffset;
		m_dungeonCrawlDisplay.gameObject.transform.localPosition = localPosition2;
		Transform transform = base.transform.Find("Root");
		GameObject rootGo = transform.gameObject;
		Hashtable args = iTween.Hash("islocal", true, "position", localPosition, "time", m_transitionTime, "easeType", "easeOutBounce", "oncomplete", (Action<object>)delegate
		{
			m_dungeonCrawlServices.SubsceneController.OnTransitionComplete();
			rootGo.SetActive(value: false);
		}, "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_dungeonCrawlDisplay.gameObject, args);
		if (!string.IsNullOrEmpty(m_SlideInSound))
		{
			SoundManager.Get().LoadAndPlay(m_SlideInSound);
		}
		Vector3 localPosition3 = transform.localPosition;
		localPosition3.y -= m_rootDropHeight;
		Hashtable args2 = iTween.Hash("islocal", true, "position", localPosition3, "time", m_transitionTime / 2f, "easeType", "easeOutBounce", "oncomplete", (Action<object>)delegate
		{
		}, "oncompletetarget", rootGo);
		iTween.MoveTo(rootGo, args2);
	}

	private void OnAssetLoadingComplete(object sender, EventArgs args)
	{
		if (m_dungeonCrawlServices != null && m_dungeonCrawlDisplay != null)
		{
			DoDungeonRunTransition();
		}
	}

	private void OnFriendChallengeWaitingForOpponentDialogResponse(AlertPopup.Response response, object userData)
	{
		if (response == AlertPopup.Response.CANCEL && !FriendChallengeMgr.Get().AmIInGameState())
		{
			FriendChallengeMgr.Get().DeselectDeckOrHero();
			FriendlyChallengeHelper.Get().StopWaitingForFriendChallenge();
			if (!TavernBrawlManager.Get().SelectHeroBeforeMission())
			{
				EnablePlayButton();
			}
		}
	}

	private void OnFriendChallengeChanged(FriendChallengeEvent challengeEvent, BnetPlayer player, FriendlyChallengeData challengeData, object userData)
	{
		switch (challengeEvent)
		{
		case FriendChallengeEvent.OPPONENT_ACCEPTED_CHALLENGE:
		case FriendChallengeEvent.I_ACCEPTED_CHALLENGE:
			SetUIForFriendlyChallenge(isTavernBrawlChallenge: true);
			break;
		case FriendChallengeEvent.SELECTED_DECK_OR_HERO:
			if (player != BnetPresenceMgr.Get().GetMyPlayer() && FriendChallengeMgr.Get().DidISelectDeckOrHero())
			{
				FriendlyChallengeHelper.Get().HideFriendChallengeWaitingForOpponentDialog();
			}
			break;
		case FriendChallengeEvent.I_RESCINDED_CHALLENGE:
		case FriendChallengeEvent.OPPONENT_DECLINED_CHALLENGE:
		case FriendChallengeEvent.OPPONENT_RESCINDED_CHALLENGE:
			SetUIForFriendlyChallenge(isTavernBrawlChallenge: false);
			break;
		case FriendChallengeEvent.OPPONENT_CANCELED_CHALLENGE:
		case FriendChallengeEvent.OPPONENT_REMOVED_FROM_FRIENDS:
		case FriendChallengeEvent.QUEUE_CANCELED:
			SetUIForFriendlyChallenge(isTavernBrawlChallenge: false);
			FriendlyChallengeHelper.Get().StopWaitingForFriendChallenge();
			break;
		case FriendChallengeEvent.DESELECTED_DECK_OR_HERO:
			if (player != BnetPresenceMgr.Get().GetMyPlayer())
			{
				if (!TavernBrawlManager.Get().SelectHeroBeforeMission())
				{
					EnablePlayButton();
				}
				else if (FriendChallengeMgr.Get().DidISelectDeckOrHero())
				{
					FriendlyChallengeHelper.Get().StartChallengeOrWaitForOpponent("GLOBAL_FRIEND_CHALLENGE_OPPONENT_WAITING_DECK", OnFriendChallengeWaitingForOpponentDialogResponse);
				}
			}
			break;
		}
	}

	private void InitExpoDemoMode()
	{
		if (DemoMgr.Get().IsExpoDemo())
		{
			if (m_backButton != null)
			{
				m_backButton.Flip(faceUp: false);
				m_backButton.SetEnabled(enabled: false);
			}
			m_chalkboardEndInfo.gameObject.SetActive(value: false);
			StartCoroutine("ShowDemoQuotes");
		}
	}

	private IEnumerator ShowDemoQuotes()
	{
		yield return new WaitForSeconds(1f);
		string str = Vars.Key("Demo.ThankQuote").GetStr("");
		int @int = Vars.Key("Demo.ThankQuoteMsTime").GetInt(0);
		str = str.Replace("\\n", "\n");
		if (!string.IsNullOrEmpty(str) && @int > 0)
		{
			m_expoThankQuote = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, new Vector3(138.3f, NotificationManager.DEPTH, 58.7f), str, "", (float)@int / 1000f);
			EnableClickBlocker(enable: true);
			yield return new WaitForSeconds((float)@int / 1000f);
			EnableClickBlocker(enable: false);
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
	}

	private void HideChalkboardFX()
	{
		m_chalkboardFX.SetActive(value: false);
	}
}
