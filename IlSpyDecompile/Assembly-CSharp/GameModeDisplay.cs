using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class GameModeDisplay : MonoBehaviour
{
	private enum LockReason
	{
		INVALID,
		FEATURE_LOCKED,
		ACHIEVE_LOCKED
	}

	public AsyncReference m_DisplayReference;

	public AsyncReference m_PlayButtonReference;

	public AsyncReference m_BackButtonReference;

	public AsyncReference m_BackButtonMobileReference;

	public SlidingTray m_slidingTray;

	public GameObject m_clickBlocker;

	public GameObject m_nameText;

	public UberText m_lockedNameText;

	public GameObject m_lockedPlateMesh;

	public VisualController m_gameModeButtonController;

	private PlayButton m_playButton;

	private UIBButton m_backButton;

	private bool m_playButtonFinishedLoading;

	private bool m_backButtonFinishedLoading;

	private Action m_onSceneTransitionCompleteCallback;

	private List<GameModeDbfRecord> m_activeGameModeRecords = new List<GameModeDbfRecord>();

	private GameModeButtonDataModel m_selectedGameModeButtonDataModel;

	private List<long> m_seenGameModes = new List<long>();

	private static GameModeDisplay m_instance;

	private const string GAME_MODE_LOCKED_EVENT_NAME = "GAME_MODE_LOCKED";

	private const string GAME_MODE_ACTIVE_EVENT_NAME = "GAME_MODE_ACTIVE";

	public bool IsFinishedLoading
	{
		get
		{
			if (m_playButtonFinishedLoading)
			{
				return m_backButtonFinishedLoading;
			}
			return false;
		}
	}

	public static GameModeDisplay Get()
	{
		return m_instance;
	}

	private void Awake()
	{
		m_instance = this;
	}

	private void Start()
	{
		m_DisplayReference.RegisterReadyListener<Widget>(OnDisplayReady);
		m_PlayButtonReference.RegisterReadyListener<PlayButton>(OnPlayButtonReady);
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_BackButtonMobileReference.RegisterReadyListener<UIBButton>(OnBackButtonReady);
		}
		else
		{
			m_BackButtonReference.RegisterReadyListener<UIBButton>(OnBackButtonReady);
		}
		InitializeGameModeSceneData();
		m_slidingTray.OnTransitionComplete += OnSlidingTrayAnimationComplete;
		InitializeSlidingTray();
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Tournament);
	}

	private void Update()
	{
	}

	public void RegisterOnHideTrayListener(Action action)
	{
		if (m_slidingTray != null)
		{
			m_slidingTray.OnTransitionComplete += action;
		}
	}

	public void UnRegisterOnHideTrayListener(Action action)
	{
		if (m_slidingTray != null)
		{
			m_slidingTray.OnTransitionComplete -= action;
		}
	}

	private void GameModeDisplayEventListener(string eventName)
	{
		switch (eventName)
		{
		case "CHOOSE":
			NavigateToSelectedMode();
			break;
		case "BACK":
			GoToHub();
			break;
		case "GAME_MODE_CLICKED":
			OnGameModeSelected();
			break;
		}
	}

	private void OnDisplayReady(Widget widget)
	{
		if (widget == null)
		{
			Error.AddDevWarning("UI Error!", "DisplayReference could not be found!");
		}
		else
		{
			widget.RegisterEventListener(GameModeDisplayEventListener);
		}
	}

	public void OnPlayButtonReady(PlayButton playButton)
	{
		m_playButtonFinishedLoading = true;
		if (playButton == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButton could not be found! You will not be able to click 'Play'!");
			return;
		}
		m_playButton = playButton;
		m_playButton.AddEventListener(UIEventType.RELEASE, PlayButtonRelease);
		m_playButton.Disable();
	}

	public void OnBackButtonReady(UIBButton backButton)
	{
		m_backButtonFinishedLoading = true;
		if (backButton == null)
		{
			Error.AddDevWarning("UI Error!", "BackButton could not be found! You will not be able to click 'Back'!");
			return;
		}
		m_backButton = backButton;
		m_backButton.AddEventListener(UIEventType.RELEASE, BackButtonRelease);
	}

	public GameModeSceneDataModel GetGameModeSceneDataModel()
	{
		VisualController component = GetComponent<VisualController>();
		if (component == null)
		{
			return null;
		}
		Widget owner = component.Owner;
		if (!owner.GetDataModel(173, out var model))
		{
			model = new GameModeSceneDataModel();
			owner.BindDataModel(model);
		}
		return model as GameModeSceneDataModel;
	}

	public EventDataModel GetEventDataModel()
	{
		VisualController component = GetComponent<VisualController>();
		if (component == null)
		{
			return null;
		}
		return component.Owner.GetDataModel<EventDataModel>();
	}

	private void InitializeGameModeSceneData()
	{
		GameModeSceneDataModel gameModeSceneDataModel = GetGameModeSceneDataModel();
		if (gameModeSceneDataModel == null)
		{
			return;
		}
		m_activeGameModeRecords = (from r in GameDbf.GameMode.GetRecords((GameModeDbfRecord r) => SpecialEventManager.Get().IsEventActive(r.Event, activeIfDoesNotExist: false))
			orderby r.SortOrder
			select r).ToList();
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.GAME_MODE_SCENE, GameSaveKeySubkeyId.GAME_MODE_SCENE_LAST_SELECTED_GAME_MODE, out long value);
		gameModeSceneDataModel.LastSelectedGameModeRecordId = (int)value;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.GAME_MODE_SCENE, GameSaveKeySubkeyId.GAME_MODE_SCENE_SEEN_GAME_MODES, out m_seenGameModes);
		if (m_seenGameModes == null)
		{
			m_seenGameModes = new List<long>();
		}
		gameModeSceneDataModel.GameModeButtons = new DataModelList<GameModeButtonDataModel>();
		foreach (GameModeDbfRecord activeGameModeRecord in m_activeGameModeRecords)
		{
			bool isNew = SpecialEventManager.Get().IsEventActive(activeGameModeRecord.ShowAsNewEvent, activeIfDoesNotExist: false) && !m_seenGameModes.Contains(activeGameModeRecord.ID);
			bool isEarlyAccess = SpecialEventManager.Get().IsEventActive(activeGameModeRecord.ShowAsEarlyAccessEvent, activeIfDoesNotExist: false);
			bool isBeta = SpecialEventManager.Get().IsEventActive(activeGameModeRecord.ShowAsBetaEvent, activeIfDoesNotExist: false);
			gameModeSceneDataModel.GameModeButtons.Add(new GameModeButtonDataModel
			{
				GameModeRecordId = activeGameModeRecord.ID,
				Name = activeGameModeRecord.Name,
				Description = activeGameModeRecord.Description,
				ButtonState = activeGameModeRecord.GameModeButtonState,
				IsNew = isNew,
				IsEarlyAccess = isEarlyAccess,
				IsBeta = isBeta
			});
		}
	}

	private bool CanEnterMode(out LockReason reason)
	{
		reason = LockReason.INVALID;
		GameModeDbfRecord gameModeDbfRecord = null;
		foreach (GameModeDbfRecord activeGameModeRecord in m_activeGameModeRecords)
		{
			if (activeGameModeRecord.ID == m_selectedGameModeButtonDataModel.GameModeRecordId)
			{
				gameModeDbfRecord = activeGameModeRecord;
				break;
			}
		}
		if (gameModeDbfRecord != null)
		{
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			bool featureFlag = netObject.Games.GetFeatureFlag((NetCache.NetCacheFeatures.CacheGames.FeatureFlags)gameModeDbfRecord.FeatureUnlockId);
			bool flag = gameModeDbfRecord.FeatureUnlockId2 == 0 || netObject.Games.GetFeatureFlag((NetCache.NetCacheFeatures.CacheGames.FeatureFlags)gameModeDbfRecord.FeatureUnlockId2);
			if (!featureFlag && !flag)
			{
				reason = LockReason.FEATURE_LOCKED;
				return false;
			}
			SceneMgr.Mode mode = EnumUtils.Parse<SceneMgr.Mode>(gameModeDbfRecord.LinkedScene);
			if ((mode == SceneMgr.Mode.DRAFT || mode == SceneMgr.Mode.PVP_DUNGEON_RUN) && !AchieveManager.Get().HasUnlockedVanillaHeroes())
			{
				reason = LockReason.ACHIEVE_LOCKED;
				return false;
			}
			return true;
		}
		return false;
	}

	private void InitializeSlidingTray()
	{
		bool show = SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.HUB;
		m_slidingTray.ToggleTraySlider(show, null, animate: false);
	}

	private void PlayButtonRelease(UIEvent e)
	{
		NavigateToSelectedMode();
	}

	private void BackButtonRelease(UIEvent e)
	{
		GoToHub();
	}

	private void GoToHub()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
	}

	private void NavigateToSelectedMode()
	{
		m_playButton.Disable();
		if (m_selectedGameModeButtonDataModel == null)
		{
			Log.All.PrintError("No game mode selected!");
			return;
		}
		if (!CanEnterMode(out var reason))
		{
			ShowDisabledPopupForCurrentMode(reason);
			return;
		}
		GameModeDbfRecord record = GameDbf.GameMode.GetRecord(m_selectedGameModeButtonDataModel.GameModeRecordId);
		if (record == null)
		{
			Log.All.PrintError($"Game mode with invalid id {m_selectedGameModeButtonDataModel.GameModeRecordId} selected!");
			return;
		}
		if (!m_seenGameModes.Contains(record.ID))
		{
			m_seenGameModes.Add(record.ID);
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.GAME_MODE_SCENE, GameSaveKeySubkeyId.GAME_MODE_SCENE_SEEN_GAME_MODES, m_seenGameModes.ToArray()));
		}
		m_clickBlocker.SetActive(value: true);
		SceneMgr.Mode mode = EnumUtils.Parse<SceneMgr.Mode>(record.LinkedScene);
		SceneMgr.Get().SetNextMode(mode, SceneMgr.TransitionHandlerType.CURRENT_SCENE, OnSceneLoadCompleteHandleTransition);
		if (mode == SceneMgr.Mode.DRAFT)
		{
			AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_ARENA);
		}
	}

	private void OnSceneLoadCompleteHandleTransition(Action onTransitionComplete)
	{
		m_onSceneTransitionCompleteCallback = onTransitionComplete;
		m_slidingTray.HideTray();
	}

	public void ShowSlidingTrayAfterSceneLoad(Action onCompleteCallback)
	{
		m_clickBlocker.SetActive(value: true);
		m_onSceneTransitionCompleteCallback = onCompleteCallback;
		m_slidingTray.ShowTray();
	}

	private void OnSlidingTrayAnimationComplete()
	{
		m_clickBlocker.SetActive(value: false);
		if (m_onSceneTransitionCompleteCallback != null)
		{
			m_onSceneTransitionCompleteCallback();
			m_onSceneTransitionCompleteCallback = null;
		}
	}

	private void OnGameModeSelected()
	{
		EventDataModel eventDataModel = GetEventDataModel();
		if (eventDataModel == null)
		{
			Log.All.PrintError("No event data model attached to the GameModeDisplay.");
			return;
		}
		m_selectedGameModeButtonDataModel = (GameModeButtonDataModel)eventDataModel.Payload;
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.GAME_MODE_SCENE, GameSaveKeySubkeyId.GAME_MODE_SCENE_LAST_SELECTED_GAME_MODE, m_selectedGameModeButtonDataModel.GameModeRecordId));
		GameModeSceneDataModel gameModeSceneDataModel = GetGameModeSceneDataModel();
		if (gameModeSceneDataModel != null)
		{
			gameModeSceneDataModel.LastSelectedGameModeRecordId = m_selectedGameModeButtonDataModel.GameModeRecordId;
		}
		if (!CanEnterMode(out var _))
		{
			m_playButton.Disable(keepLabelTextVisible: true);
			m_lockedNameText.Text = GameStrings.Format("GLUE_GAME_MODE_LOCKED_HEADER", m_selectedGameModeButtonDataModel.Name);
			m_gameModeButtonController.SetState("GAME_MODE_LOCKED");
		}
		else
		{
			GameStrings.Get(m_selectedGameModeButtonDataModel.Name);
			m_playButton.Enable();
			m_gameModeButtonController.SetState("GAME_MODE_ACTIVE");
		}
	}

	private void ShowDisabledPopupForCurrentMode(LockReason reason)
	{
		if (reason != 0)
		{
			_ = m_selectedGameModeButtonDataModel.GameModeRecordId;
			string header = GameStrings.Get(m_selectedGameModeButtonDataModel.Name);
			string description = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
			switch (reason)
			{
			case LockReason.ACHIEVE_LOCKED:
				description = GameStrings.Format("GLUE_TOOLTIP_BUTTON_FORGE_NOT_UNLOCKED", 20);
				break;
			case LockReason.FEATURE_LOCKED:
				description = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
				break;
			}
			ShowDisabledPopup(header, description);
		}
	}

	private void ShowDisabledPopup(string header, string description)
	{
		if (string.IsNullOrEmpty(description))
		{
			description = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
		}
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = header,
			m_text = description,
			m_showAlertIcon = true,
			m_responseDisplay = AlertPopup.ResponseDisplay.OK
		};
		DialogManager.Get().ShowPopup(info);
	}
}
