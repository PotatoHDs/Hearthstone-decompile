using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Login;
using HutongGames.PlayMaker;
using PegasusShared;
using UnityEngine;

public class Login : PegasusScene
{
	private int m_nextMissionId;

	private ExistingAccountPopup m_existingAccountPopup;

	private static Login s_instance;

	private bool m_blockingBnetBar;

	protected override void Awake()
	{
		s_instance = this;
		base.Awake();
		Processor.QueueJob("Login.GoToNextMode", GoToNextMode(), LoginManager.Get().ReadyToGoToNextModeDependency);
		JobDefinition sceneTransitionJob = new JobDefinition("Login.ReconnectOrChangeMode", ReconnectOrChangeMode());
		JobDefinition jobDefinition = Processor.QueueJob("Splashscreen.ShowLoginQueue", SplashScreen.Get().Job_ShowLoginQueue());
		Processor.QueueJob("SplashScreen.Hide", SplashScreen.Get().Hide(sceneTransitionJob), LoginManager.Get().ReadyToReconnectOrChangeModeDependency, jobDefinition.CreateDependency());
	}

	private void Start()
	{
		SceneMgr.Get().NotifySceneLoaded();
	}

	private void OnDestroy()
	{
		if (s_instance == this)
		{
			s_instance = null;
		}
	}

	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	public static Login Get()
	{
		return s_instance;
	}

	public override void Unload()
	{
		GameMgr.Get().UnregisterFindGameEvent(OnFindGameEvent);
		SetBlockingBnetBar(blocked: false);
	}

	private IEnumerator<IAsyncJobResult> ReconnectOrChangeMode()
	{
		HearthstoneApplication.SendStartupTimeTelemetry("Login.ReconnectOrChangeMode");
		if (BaseUI.Get() != null)
		{
			BaseUI.Get().OnLoggedIn();
		}
		if (Cheats.Get().IsLaunchingQuickGame() || !LoginManager.Get().AttemptToReconnectToGame(OnReconnectTimeout))
		{
			ChangeMode();
		}
		yield break;
	}

	private void ChangeMode()
	{
		m_nextMissionId = GameUtils.GetNextTutorial();
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_MainTitle);
		if (m_nextMissionId != 0)
		{
			ChangeMode_Tutorial();
		}
		else if (!CreateSkipHelper.ShouldShowCreateSkip() || !CreateSkipHelper.ShowCreateSkipDialog(ChangeToAppropriateHubMode))
		{
			ChangeToAppropriateHubMode();
		}
	}

	private void ChangeToAppropriateHubMode()
	{
		Log.Login.PrintInfo("Changing mode");
		if (SetRotationManager.ShouldShowSetRotationIntro())
		{
			ChangeMode_SetRotation();
		}
		else
		{
			ChangeMode_Hub();
		}
	}

	private bool OnReconnectTimeout(object userData)
	{
		ChangeMode();
		return true;
	}

	private void ChangeMode_Hub()
	{
		SetBlockingBnetBar(blocked: true);
		HearthstoneServices.Get<LoginManager>().OnFullLoginFlowComplete += delegate
		{
			SetBlockingBnetBar(blocked: false);
		};
		if (Options.Get().GetBool(Option.HAS_SEEN_HUB, defaultVal: false))
		{
			PlayInnkeeperIntroVO();
		}
		Spell eventSpell = Box.Get().GetEventSpell(BoxEventType.STARTUP_HUB);
		eventSpell.AddFinishedCallback(OnStartupHubSpellFinished);
		eventSpell.Activate();
	}

	private void SetBlockingBnetBar(bool blocked)
	{
		if (blocked != m_blockingBnetBar)
		{
			m_blockingBnetBar = blocked;
			if (blocked)
			{
				BaseUI.Get().m_BnetBar.RequestDisableButtons();
			}
			else
			{
				BaseUI.Get().m_BnetBar.CancelRequestToDisableButtons();
			}
		}
	}

	private void PlayInnkeeperIntroVO()
	{
		if (!ReturningPlayerMgr.Get().PlayReturningPlayerInnkeeperGreetingIfNecessary())
		{
			SoundManager.Get().LoadAndPlay("VO_INNKEEPER_INTRO_01.prefab:16aa6b57075ce5743a80fcfa6e4f30c1");
		}
	}

	private IEnumerator<IAsyncJobResult> GoToNextMode()
	{
		if (m_nextMissionId == 0)
		{
			SceneMgr.Mode nextScene = SceneMgr.Mode.INVALID;
			if (!DeterminePostLoginScene(ref nextScene))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			}
		}
		yield break;
	}

	private bool DeterminePostLoginScene(ref SceneMgr.Mode nextScene)
	{
		foreach (KeyValuePair<StartupSceneSource, DetermineStartupSceneCallback> item in new SortedList<StartupSceneSource, DetermineStartupSceneCallback>(LoginManager.GetPostLoginCallbacks()))
		{
			if (item.Key == StartupSceneSource.DEFAULT_NORMAL_STARTUP)
			{
				break;
			}
			DetermineStartupSceneCallback value = item.Value;
			nextScene = SceneMgr.Mode.INVALID;
			if (value(ref nextScene))
			{
				return true;
			}
		}
		return false;
	}

	private void ChangeMode_Tutorial()
	{
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.TUTORIAL_PREGAME);
		Box.Get().SetLightState(BoxLightStateType.TUTORIAL);
		Spell eventSpell = Box.Get().GetEventSpell(BoxEventType.STARTUP_TUTORIAL);
		eventSpell.AddFinishedCallback(OnStartupTutorialSpellFinished);
		eventSpell.Activate();
	}

	private void OnStartupTutorialSpellFinished(Spell spell, object userData)
	{
		Box.Get().AddButtonPressListener(OnStartButtonPressed);
		Box.Get().ChangeState(Box.State.PRESS_START);
	}

	private void OnStartButtonPressed(Box.ButtonType buttonType, object userData)
	{
		if (buttonType != 0)
		{
			return;
		}
		TelemetryManager.Client().SendButtonPressed("PressToStart");
		if (m_nextMissionId == 3)
		{
			AdTrackingManager.Get().TrackTutorialProgress(TutorialProgress.NOTHING_COMPLETE);
		}
		Box.Get().RemoveButtonPressListener(OnStartButtonPressed);
		if (m_nextMissionId == 3)
		{
			if (DemoMgr.Get().GetMode() == DemoMode.BLIZZ_MUSEUM || Network.ShouldBeConnectedToAurora())
			{
				StartTutorial();
				return;
			}
			Box.Get().m_StartButton.ChangeState(BoxStartButton.State.HIDDEN);
			DialogManager.Get().ShowExistingAccountPopup(OnExistingAccountPopupResponse, OnExistingAccountLoadedCallback);
		}
		else
		{
			ShowTutorialProgressScreen();
		}
	}

	private void ShowTutorialProgressScreen()
	{
		Box.Get().m_StartButton.ChangeState(BoxStartButton.State.HIDDEN);
		AssetLoader.Get().InstantiatePrefab("TutorialProgressScreen.prefab:a78bac9caa971494ea8fac23dc1a9bd8", OnTutorialProgressScreenCallback, null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	private void OnTutorialProgressScreenCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		TutorialProgressScreen component = go.GetComponent<TutorialProgressScreen>();
		component.SetCoinPressCallback(StartTutorial);
		component.StartTutorialProgress();
	}

	private void OnExistingAccountPopupResponse(bool hasAccount)
	{
		m_existingAccountPopup.gameObject.SetActive(value: false);
		if (hasAccount)
		{
			HearthstoneApplication.Get().ResetAndForceLogin();
		}
		else
		{
			StartTutorial();
		}
	}

	private void StartTutorial()
	{
		MusicManager.Get().StopPlaylist();
		Box.Get().ChangeState(Box.State.CLOSED);
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		GameMgr.Get().FindGame(GameType.GT_TUTORIAL, FormatType.FT_WILD, m_nextMissionId, 0, 0L);
	}

	private bool OnExistingAccountLoadedCallback(DialogBase dialog, object userData)
	{
		m_existingAccountPopup = (ExistingAccountPopup)dialog;
		m_existingAccountPopup.gameObject.SetActive(value: true);
		return true;
	}

	private void ChangeMode_SetRotation()
	{
		UserAttentionManager.StartBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
		Spell eventSpell = Box.Get().GetEventSpell(BoxEventType.STARTUP_SET_ROTATION);
		Box.Get().m_StoreButton.gameObject.SetActive(value: false);
		Box.Get().m_QuestLogButton.gameObject.SetActive(value: false);
		if (PlatformSettings.IsMobile())
		{
			PlayMakerFSM component = eventSpell.gameObject.GetComponent<PlayMakerFSM>();
			if (component == null)
			{
				Debug.LogError("Missing FSM on Startup_Hub");
			}
			else
			{
				FsmFloat fsmFloat = component.FsmVariables.GetFsmFloat("PanDuration");
				FsmFloat fsmFloat2 = component.FsmVariables.GetFsmFloat("PanStartTime");
				fsmFloat.Value = 3f;
				fsmFloat2.Value = 2f;
			}
		}
		eventSpell.AddFinishedCallback(OnSetRotationSpellFinished);
		eventSpell.Activate();
	}

	private void OnSetRotationSpellFinished(Spell spell, object userData)
	{
		Processor.QueueJob("Login.GoToNextMode", GoToNextMode());
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if (state == FindGameState.SERVER_GAME_STARTED && !GameMgr.Get().IsNextReconnect())
		{
			Spell eventSpell = Box.Get().GetEventSpell(BoxEventType.TUTORIAL_PLAY);
			eventSpell.AddStateFinishedCallback(OnTutorialPlaySpellStateFinished);
			eventSpell.ActivateState(SpellStateType.BIRTH);
			return true;
		}
		return false;
	}

	private void OnTutorialPlaySpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		SpellStateType activeState = spell.GetActiveState();
		if (prevStateType == SpellStateType.BIRTH)
		{
			LoadingScreen.Get().SetFadeColor(Color.white);
			LoadingScreen.Get().EnableFadeOut(enable: false);
			LoadingScreen.Get().AddTransitionObject(Box.Get().gameObject);
			LoadingScreen.Get().AddTransitionBlocker();
			SceneMgr.Get().RegisterSceneLoadedEvent(OnMissionSceneLoaded);
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAMEPLAY);
		}
		else if (activeState == SpellStateType.NONE)
		{
			LoadingScreen.Get().NotifyTransitionBlockerComplete();
		}
	}

	private void OnMissionSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		SceneMgr.Get().UnregisterSceneLoadedEvent(OnMissionSceneLoaded);
		Box.Get().GetEventSpell(BoxEventType.TUTORIAL_PLAY).ActivateState(SpellStateType.ACTION);
	}

	private void OnStartupHubSpellFinished(Spell spell, object userData)
	{
		HearthstoneApplication.SendStartupTimeTelemetry("Login.OnStartupHubSpellFinished");
		IJobDependency[] array = null;
		if (Network.ShouldBeConnectedToAurora() && m_nextMissionId == 0)
		{
			JobDefinition jobDefinition = Processor.QueueJob("LoginManager.ShowIntroPopups", LoginManager.Get().ShowIntroPopups());
			array = new IJobDependency[1] { jobDefinition.CreateDependency() };
			Processor.QueueJob("LoginManager.CompleteLoginFlow", LoginManager.Get().CompleteLoginFlow(), array);
		}
	}
}
