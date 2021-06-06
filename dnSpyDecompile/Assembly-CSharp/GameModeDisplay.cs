using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class GameModeDisplay : MonoBehaviour
{
	// Token: 0x06002873 RID: 10355 RVA: 0x000CB3DD File Offset: 0x000C95DD
	public static GameModeDisplay Get()
	{
		return GameModeDisplay.m_instance;
	}

	// Token: 0x170004FE RID: 1278
	// (get) Token: 0x06002874 RID: 10356 RVA: 0x000CB3E4 File Offset: 0x000C95E4
	public bool IsFinishedLoading
	{
		get
		{
			return this.m_playButtonFinishedLoading && this.m_backButtonFinishedLoading;
		}
	}

	// Token: 0x06002875 RID: 10357 RVA: 0x000CB3F6 File Offset: 0x000C95F6
	private void Awake()
	{
		GameModeDisplay.m_instance = this;
	}

	// Token: 0x06002876 RID: 10358 RVA: 0x000CB400 File Offset: 0x000C9600
	private void Start()
	{
		this.m_DisplayReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnDisplayReady));
		this.m_PlayButtonReference.RegisterReadyListener<PlayButton>(new Action<PlayButton>(this.OnPlayButtonReady));
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_BackButtonMobileReference.RegisterReadyListener<UIBButton>(new Action<UIBButton>(this.OnBackButtonReady));
		}
		else
		{
			this.m_BackButtonReference.RegisterReadyListener<UIBButton>(new Action<UIBButton>(this.OnBackButtonReady));
		}
		this.InitializeGameModeSceneData();
		this.m_slidingTray.OnTransitionComplete += this.OnSlidingTrayAnimationComplete;
		this.InitializeSlidingTray();
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_Tournament);
	}

	// Token: 0x06002877 RID: 10359 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x06002878 RID: 10360 RVA: 0x000CB4A7 File Offset: 0x000C96A7
	public void RegisterOnHideTrayListener(Action action)
	{
		if (this.m_slidingTray != null)
		{
			this.m_slidingTray.OnTransitionComplete += action;
		}
	}

	// Token: 0x06002879 RID: 10361 RVA: 0x000CB4C3 File Offset: 0x000C96C3
	public void UnRegisterOnHideTrayListener(Action action)
	{
		if (this.m_slidingTray != null)
		{
			this.m_slidingTray.OnTransitionComplete -= action;
		}
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x000CB4DF File Offset: 0x000C96DF
	private void GameModeDisplayEventListener(string eventName)
	{
		if (eventName == "CHOOSE")
		{
			this.NavigateToSelectedMode();
			return;
		}
		if (eventName == "BACK")
		{
			this.GoToHub();
			return;
		}
		if (!(eventName == "GAME_MODE_CLICKED"))
		{
			return;
		}
		this.OnGameModeSelected();
	}

	// Token: 0x0600287B RID: 10363 RVA: 0x000CB51D File Offset: 0x000C971D
	private void OnDisplayReady(Widget widget)
	{
		if (widget == null)
		{
			Error.AddDevWarning("UI Error!", "DisplayReference could not be found!", Array.Empty<object>());
			return;
		}
		widget.RegisterEventListener(new Widget.EventListenerDelegate(this.GameModeDisplayEventListener));
	}

	// Token: 0x0600287C RID: 10364 RVA: 0x000CB550 File Offset: 0x000C9750
	public void OnPlayButtonReady(PlayButton playButton)
	{
		this.m_playButtonFinishedLoading = true;
		if (playButton == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButton could not be found! You will not be able to click 'Play'!", Array.Empty<object>());
			return;
		}
		this.m_playButton = playButton;
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.PlayButtonRelease));
		this.m_playButton.Disable(false);
	}

	// Token: 0x0600287D RID: 10365 RVA: 0x000CB5B0 File Offset: 0x000C97B0
	public void OnBackButtonReady(UIBButton backButton)
	{
		this.m_backButtonFinishedLoading = true;
		if (backButton == null)
		{
			Error.AddDevWarning("UI Error!", "BackButton could not be found! You will not be able to click 'Back'!", Array.Empty<object>());
			return;
		}
		this.m_backButton = backButton;
		this.m_backButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.BackButtonRelease));
	}

	// Token: 0x0600287E RID: 10366 RVA: 0x000CB604 File Offset: 0x000C9804
	public GameModeSceneDataModel GetGameModeSceneDataModel()
	{
		VisualController component = base.GetComponent<VisualController>();
		if (component == null)
		{
			return null;
		}
		Widget owner = component.Owner;
		IDataModel dataModel;
		if (!owner.GetDataModel(173, out dataModel))
		{
			dataModel = new GameModeSceneDataModel();
			owner.BindDataModel(dataModel, false);
		}
		return dataModel as GameModeSceneDataModel;
	}

	// Token: 0x0600287F RID: 10367 RVA: 0x000CB650 File Offset: 0x000C9850
	public EventDataModel GetEventDataModel()
	{
		VisualController component = base.GetComponent<VisualController>();
		if (component == null)
		{
			return null;
		}
		return component.Owner.GetDataModel<EventDataModel>();
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x000CB67C File Offset: 0x000C987C
	private void InitializeGameModeSceneData()
	{
		GameModeSceneDataModel gameModeSceneDataModel = this.GetGameModeSceneDataModel();
		if (gameModeSceneDataModel == null)
		{
			return;
		}
		this.m_activeGameModeRecords = (from r in GameDbf.GameMode.GetRecords((GameModeDbfRecord r) => SpecialEventManager.Get().IsEventActive(r.Event, false), -1)
		orderby r.SortOrder
		select r).ToList<GameModeDbfRecord>();
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.GAME_MODE_SCENE, GameSaveKeySubkeyId.GAME_MODE_SCENE_LAST_SELECTED_GAME_MODE, out num);
		gameModeSceneDataModel.LastSelectedGameModeRecordId = (int)num;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.GAME_MODE_SCENE, GameSaveKeySubkeyId.GAME_MODE_SCENE_SEEN_GAME_MODES, out this.m_seenGameModes);
		if (this.m_seenGameModes == null)
		{
			this.m_seenGameModes = new List<long>();
		}
		gameModeSceneDataModel.GameModeButtons = new DataModelList<GameModeButtonDataModel>();
		foreach (GameModeDbfRecord gameModeDbfRecord in this.m_activeGameModeRecords)
		{
			bool isNew = SpecialEventManager.Get().IsEventActive(gameModeDbfRecord.ShowAsNewEvent, false) && !this.m_seenGameModes.Contains((long)gameModeDbfRecord.ID);
			bool isEarlyAccess = SpecialEventManager.Get().IsEventActive(gameModeDbfRecord.ShowAsEarlyAccessEvent, false);
			bool isBeta = SpecialEventManager.Get().IsEventActive(gameModeDbfRecord.ShowAsBetaEvent, false);
			gameModeSceneDataModel.GameModeButtons.Add(new GameModeButtonDataModel
			{
				GameModeRecordId = gameModeDbfRecord.ID,
				Name = gameModeDbfRecord.Name,
				Description = gameModeDbfRecord.Description,
				ButtonState = gameModeDbfRecord.GameModeButtonState,
				IsNew = isNew,
				IsEarlyAccess = isEarlyAccess,
				IsBeta = isBeta
			});
		}
	}

	// Token: 0x06002881 RID: 10369 RVA: 0x000CB840 File Offset: 0x000C9A40
	private bool CanEnterMode(out GameModeDisplay.LockReason reason)
	{
		reason = GameModeDisplay.LockReason.INVALID;
		GameModeDbfRecord gameModeDbfRecord = null;
		foreach (GameModeDbfRecord gameModeDbfRecord2 in this.m_activeGameModeRecords)
		{
			if (gameModeDbfRecord2.ID == this.m_selectedGameModeButtonDataModel.GameModeRecordId)
			{
				gameModeDbfRecord = gameModeDbfRecord2;
				break;
			}
		}
		if (gameModeDbfRecord == null)
		{
			return false;
		}
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		bool featureFlag = netObject.Games.GetFeatureFlag((NetCache.NetCacheFeatures.CacheGames.FeatureFlags)gameModeDbfRecord.FeatureUnlockId);
		bool flag = gameModeDbfRecord.FeatureUnlockId2 == 0 || netObject.Games.GetFeatureFlag((NetCache.NetCacheFeatures.CacheGames.FeatureFlags)gameModeDbfRecord.FeatureUnlockId2);
		if (!featureFlag && !flag)
		{
			reason = GameModeDisplay.LockReason.FEATURE_LOCKED;
			return false;
		}
		SceneMgr.Mode mode = EnumUtils.Parse<SceneMgr.Mode>(gameModeDbfRecord.LinkedScene);
		if ((mode == SceneMgr.Mode.DRAFT || mode == SceneMgr.Mode.PVP_DUNGEON_RUN) && !AchieveManager.Get().HasUnlockedVanillaHeroes())
		{
			reason = GameModeDisplay.LockReason.ACHIEVE_LOCKED;
			return false;
		}
		return true;
	}

	// Token: 0x06002882 RID: 10370 RVA: 0x000CB920 File Offset: 0x000C9B20
	private void InitializeSlidingTray()
	{
		bool show = SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.HUB;
		this.m_slidingTray.ToggleTraySlider(show, null, false);
	}

	// Token: 0x06002883 RID: 10371 RVA: 0x000CB949 File Offset: 0x000C9B49
	private void PlayButtonRelease(UIEvent e)
	{
		this.NavigateToSelectedMode();
	}

	// Token: 0x06002884 RID: 10372 RVA: 0x000CB951 File Offset: 0x000C9B51
	private void BackButtonRelease(UIEvent e)
	{
		this.GoToHub();
	}

	// Token: 0x06002885 RID: 10373 RVA: 0x00008CCD File Offset: 0x00006ECD
	private void GoToHub()
	{
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x06002886 RID: 10374 RVA: 0x000CB95C File Offset: 0x000C9B5C
	private void NavigateToSelectedMode()
	{
		this.m_playButton.Disable(false);
		if (this.m_selectedGameModeButtonDataModel == null)
		{
			Log.All.PrintError("No game mode selected!", Array.Empty<object>());
			return;
		}
		GameModeDisplay.LockReason reason;
		if (!this.CanEnterMode(out reason))
		{
			this.ShowDisabledPopupForCurrentMode(reason);
			return;
		}
		GameModeDbfRecord record = GameDbf.GameMode.GetRecord(this.m_selectedGameModeButtonDataModel.GameModeRecordId);
		if (record == null)
		{
			Log.All.PrintError(string.Format("Game mode with invalid id {0} selected!", this.m_selectedGameModeButtonDataModel.GameModeRecordId), Array.Empty<object>());
			return;
		}
		if (!this.m_seenGameModes.Contains((long)record.ID))
		{
			this.m_seenGameModes.Add((long)record.ID);
			GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.GAME_MODE_SCENE, GameSaveKeySubkeyId.GAME_MODE_SCENE_SEEN_GAME_MODES, this.m_seenGameModes.ToArray()), null);
		}
		this.m_clickBlocker.SetActive(true);
		SceneMgr.Mode mode = EnumUtils.Parse<SceneMgr.Mode>(record.LinkedScene);
		SceneMgr.Get().SetNextMode(mode, SceneMgr.TransitionHandlerType.CURRENT_SCENE, new SceneMgr.OnSceneLoadCompleteForSceneDrivenTransition(this.OnSceneLoadCompleteHandleTransition));
		if (mode == SceneMgr.Mode.DRAFT)
		{
			AchieveManager.Get().NotifyOfClick(Achievement.ClickTriggerType.BUTTON_ARENA);
		}
	}

	// Token: 0x06002887 RID: 10375 RVA: 0x000CBA73 File Offset: 0x000C9C73
	private void OnSceneLoadCompleteHandleTransition(Action onTransitionComplete)
	{
		this.m_onSceneTransitionCompleteCallback = onTransitionComplete;
		this.m_slidingTray.HideTray();
	}

	// Token: 0x06002888 RID: 10376 RVA: 0x000CBA87 File Offset: 0x000C9C87
	public void ShowSlidingTrayAfterSceneLoad(Action onCompleteCallback)
	{
		this.m_clickBlocker.SetActive(true);
		this.m_onSceneTransitionCompleteCallback = onCompleteCallback;
		this.m_slidingTray.ShowTray();
	}

	// Token: 0x06002889 RID: 10377 RVA: 0x000CBAA7 File Offset: 0x000C9CA7
	private void OnSlidingTrayAnimationComplete()
	{
		this.m_clickBlocker.SetActive(false);
		if (this.m_onSceneTransitionCompleteCallback != null)
		{
			this.m_onSceneTransitionCompleteCallback();
			this.m_onSceneTransitionCompleteCallback = null;
		}
	}

	// Token: 0x0600288A RID: 10378 RVA: 0x000CBAD0 File Offset: 0x000C9CD0
	private void OnGameModeSelected()
	{
		EventDataModel eventDataModel = this.GetEventDataModel();
		if (eventDataModel == null)
		{
			Log.All.PrintError("No event data model attached to the GameModeDisplay.", Array.Empty<object>());
			return;
		}
		this.m_selectedGameModeButtonDataModel = (GameModeButtonDataModel)eventDataModel.Payload;
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.GAME_MODE_SCENE, GameSaveKeySubkeyId.GAME_MODE_SCENE_LAST_SELECTED_GAME_MODE, new long[]
		{
			(long)this.m_selectedGameModeButtonDataModel.GameModeRecordId
		}), null);
		GameModeSceneDataModel gameModeSceneDataModel = this.GetGameModeSceneDataModel();
		if (gameModeSceneDataModel != null)
		{
			gameModeSceneDataModel.LastSelectedGameModeRecordId = this.m_selectedGameModeButtonDataModel.GameModeRecordId;
		}
		GameModeDisplay.LockReason lockReason;
		if (!this.CanEnterMode(out lockReason))
		{
			this.m_playButton.Disable(true);
			this.m_lockedNameText.Text = GameStrings.Format("GLUE_GAME_MODE_LOCKED_HEADER", new object[]
			{
				this.m_selectedGameModeButtonDataModel.Name
			});
			this.m_gameModeButtonController.SetState("GAME_MODE_LOCKED");
			return;
		}
		GameStrings.Get(this.m_selectedGameModeButtonDataModel.Name);
		this.m_playButton.Enable();
		this.m_gameModeButtonController.SetState("GAME_MODE_ACTIVE");
	}

	// Token: 0x0600288B RID: 10379 RVA: 0x000CBBD8 File Offset: 0x000C9DD8
	private void ShowDisabledPopupForCurrentMode(GameModeDisplay.LockReason reason)
	{
		if (reason == GameModeDisplay.LockReason.INVALID)
		{
			return;
		}
		int gameModeRecordId = this.m_selectedGameModeButtonDataModel.GameModeRecordId;
		string header = GameStrings.Get(this.m_selectedGameModeButtonDataModel.Name);
		string description = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
		if (reason != GameModeDisplay.LockReason.FEATURE_LOCKED)
		{
			if (reason == GameModeDisplay.LockReason.ACHIEVE_LOCKED)
			{
				description = GameStrings.Format("GLUE_TOOLTIP_BUTTON_FORGE_NOT_UNLOCKED", new object[]
				{
					20
				});
			}
		}
		else
		{
			description = GameStrings.Get("GLUE_TOOLTIP_BUTTON_DISABLED_DESC");
		}
		this.ShowDisabledPopup(header, description);
	}

	// Token: 0x0600288C RID: 10380 RVA: 0x000CBC4C File Offset: 0x000C9E4C
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

	// Token: 0x040016FD RID: 5885
	public AsyncReference m_DisplayReference;

	// Token: 0x040016FE RID: 5886
	public AsyncReference m_PlayButtonReference;

	// Token: 0x040016FF RID: 5887
	public AsyncReference m_BackButtonReference;

	// Token: 0x04001700 RID: 5888
	public AsyncReference m_BackButtonMobileReference;

	// Token: 0x04001701 RID: 5889
	public SlidingTray m_slidingTray;

	// Token: 0x04001702 RID: 5890
	public GameObject m_clickBlocker;

	// Token: 0x04001703 RID: 5891
	public GameObject m_nameText;

	// Token: 0x04001704 RID: 5892
	public UberText m_lockedNameText;

	// Token: 0x04001705 RID: 5893
	public GameObject m_lockedPlateMesh;

	// Token: 0x04001706 RID: 5894
	public VisualController m_gameModeButtonController;

	// Token: 0x04001707 RID: 5895
	private PlayButton m_playButton;

	// Token: 0x04001708 RID: 5896
	private UIBButton m_backButton;

	// Token: 0x04001709 RID: 5897
	private bool m_playButtonFinishedLoading;

	// Token: 0x0400170A RID: 5898
	private bool m_backButtonFinishedLoading;

	// Token: 0x0400170B RID: 5899
	private Action m_onSceneTransitionCompleteCallback;

	// Token: 0x0400170C RID: 5900
	private List<GameModeDbfRecord> m_activeGameModeRecords = new List<GameModeDbfRecord>();

	// Token: 0x0400170D RID: 5901
	private GameModeButtonDataModel m_selectedGameModeButtonDataModel;

	// Token: 0x0400170E RID: 5902
	private List<long> m_seenGameModes = new List<long>();

	// Token: 0x0400170F RID: 5903
	private static GameModeDisplay m_instance;

	// Token: 0x04001710 RID: 5904
	private const string GAME_MODE_LOCKED_EVENT_NAME = "GAME_MODE_LOCKED";

	// Token: 0x04001711 RID: 5905
	private const string GAME_MODE_ACTIVE_EVENT_NAME = "GAME_MODE_ACTIVE";

	// Token: 0x02001626 RID: 5670
	private enum LockReason
	{
		// Token: 0x0400AFF1 RID: 45041
		INVALID,
		// Token: 0x0400AFF2 RID: 45042
		FEATURE_LOCKED,
		// Token: 0x0400AFF3 RID: 45043
		ACHIEVE_LOCKED
	}
}
