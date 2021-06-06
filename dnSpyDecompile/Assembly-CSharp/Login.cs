using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Login;
using HutongGames.PlayMaker;
using PegasusShared;
using UnityEngine;

// Token: 0x0200036D RID: 877
public class Login : PegasusScene
{
	// Token: 0x0600337F RID: 13183 RVA: 0x00108B00 File Offset: 0x00106D00
	protected override void Awake()
	{
		Login.s_instance = this;
		base.Awake();
		Processor.QueueJob("Login.GoToNextMode", this.GoToNextMode(), new IJobDependency[]
		{
			LoginManager.Get().ReadyToGoToNextModeDependency
		});
		JobDefinition sceneTransitionJob = new JobDefinition("Login.ReconnectOrChangeMode", this.ReconnectOrChangeMode(), Array.Empty<IJobDependency>());
		JobDefinition jobDefinition = Processor.QueueJob("Splashscreen.ShowLoginQueue", SplashScreen.Get().Job_ShowLoginQueue(), Array.Empty<IJobDependency>());
		Processor.QueueJob("SplashScreen.Hide", SplashScreen.Get().Hide(sceneTransitionJob), new IJobDependency[]
		{
			LoginManager.Get().ReadyToReconnectOrChangeModeDependency,
			jobDefinition.CreateDependency()
		});
	}

	// Token: 0x06003380 RID: 13184 RVA: 0x000C10A3 File Offset: 0x000BF2A3
	private void Start()
	{
		SceneMgr.Get().NotifySceneLoaded();
	}

	// Token: 0x06003381 RID: 13185 RVA: 0x00108B9F File Offset: 0x00106D9F
	private void OnDestroy()
	{
		if (Login.s_instance == this)
		{
			Login.s_instance = null;
		}
	}

	// Token: 0x06003382 RID: 13186 RVA: 0x00019DD3 File Offset: 0x00017FD3
	private void Update()
	{
		Network.Get().ProcessNetwork();
	}

	// Token: 0x06003383 RID: 13187 RVA: 0x00108BB4 File Offset: 0x00106DB4
	public static Login Get()
	{
		return Login.s_instance;
	}

	// Token: 0x06003384 RID: 13188 RVA: 0x00108BBB File Offset: 0x00106DBB
	public override void Unload()
	{
		GameMgr.Get().UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		this.SetBlockingBnetBar(false);
	}

	// Token: 0x06003385 RID: 13189 RVA: 0x00108BDB File Offset: 0x00106DDB
	private IEnumerator<IAsyncJobResult> ReconnectOrChangeMode()
	{
		HearthstoneApplication.SendStartupTimeTelemetry("Login.ReconnectOrChangeMode");
		if (BaseUI.Get() != null)
		{
			BaseUI.Get().OnLoggedIn();
		}
		if (!Cheats.Get().IsLaunchingQuickGame() && LoginManager.Get().AttemptToReconnectToGame(new ReconnectMgr.TimeoutCallback(this.OnReconnectTimeout)))
		{
			yield break;
		}
		this.ChangeMode();
		yield break;
	}

	// Token: 0x06003386 RID: 13190 RVA: 0x00108BEC File Offset: 0x00106DEC
	private void ChangeMode()
	{
		this.m_nextMissionId = GameUtils.GetNextTutorial();
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_MainTitle);
		if (this.m_nextMissionId != 0)
		{
			this.ChangeMode_Tutorial();
			return;
		}
		if (CreateSkipHelper.ShouldShowCreateSkip() && CreateSkipHelper.ShowCreateSkipDialog(new Action(this.ChangeToAppropriateHubMode)))
		{
			return;
		}
		this.ChangeToAppropriateHubMode();
	}

	// Token: 0x06003387 RID: 13191 RVA: 0x00108C41 File Offset: 0x00106E41
	private void ChangeToAppropriateHubMode()
	{
		Log.Login.PrintInfo("Changing mode", Array.Empty<object>());
		if (SetRotationManager.ShouldShowSetRotationIntro())
		{
			this.ChangeMode_SetRotation();
			return;
		}
		this.ChangeMode_Hub();
	}

	// Token: 0x06003388 RID: 13192 RVA: 0x00108C6B File Offset: 0x00106E6B
	private bool OnReconnectTimeout(object userData)
	{
		this.ChangeMode();
		return true;
	}

	// Token: 0x06003389 RID: 13193 RVA: 0x00108C74 File Offset: 0x00106E74
	private void ChangeMode_Hub()
	{
		this.SetBlockingBnetBar(true);
		HearthstoneServices.Get<LoginManager>().OnFullLoginFlowComplete += delegate()
		{
			this.SetBlockingBnetBar(false);
		};
		if (Options.Get().GetBool(Option.HAS_SEEN_HUB, false))
		{
			this.PlayInnkeeperIntroVO();
		}
		Spell eventSpell = Box.Get().GetEventSpell(BoxEventType.STARTUP_HUB);
		eventSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnStartupHubSpellFinished));
		eventSpell.Activate();
	}

	// Token: 0x0600338A RID: 13194 RVA: 0x00108CD8 File Offset: 0x00106ED8
	private void SetBlockingBnetBar(bool blocked)
	{
		if (blocked == this.m_blockingBnetBar)
		{
			return;
		}
		this.m_blockingBnetBar = blocked;
		if (blocked)
		{
			BaseUI.Get().m_BnetBar.RequestDisableButtons();
			return;
		}
		BaseUI.Get().m_BnetBar.CancelRequestToDisableButtons();
	}

	// Token: 0x0600338B RID: 13195 RVA: 0x00108D0D File Offset: 0x00106F0D
	private void PlayInnkeeperIntroVO()
	{
		if (!ReturningPlayerMgr.Get().PlayReturningPlayerInnkeeperGreetingIfNecessary())
		{
			SoundManager.Get().LoadAndPlay("VO_INNKEEPER_INTRO_01.prefab:16aa6b57075ce5743a80fcfa6e4f30c1");
		}
	}

	// Token: 0x0600338C RID: 13196 RVA: 0x00108D2F File Offset: 0x00106F2F
	private IEnumerator<IAsyncJobResult> GoToNextMode()
	{
		if (this.m_nextMissionId != 0)
		{
			yield break;
		}
		SceneMgr.Mode mode = SceneMgr.Mode.INVALID;
		if (!this.DeterminePostLoginScene(ref mode))
		{
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}
		yield break;
	}

	// Token: 0x0600338D RID: 13197 RVA: 0x00108D40 File Offset: 0x00106F40
	private bool DeterminePostLoginScene(ref SceneMgr.Mode nextScene)
	{
		foreach (KeyValuePair<StartupSceneSource, DetermineStartupSceneCallback> keyValuePair in new SortedList<StartupSceneSource, DetermineStartupSceneCallback>(LoginManager.GetPostLoginCallbacks()))
		{
			if (keyValuePair.Key == StartupSceneSource.DEFAULT_NORMAL_STARTUP)
			{
				break;
			}
			DetermineStartupSceneCallback value = keyValuePair.Value;
			nextScene = SceneMgr.Mode.INVALID;
			if (value(ref nextScene))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600338E RID: 13198 RVA: 0x00108DB0 File Offset: 0x00106FB0
	private void ChangeMode_Tutorial()
	{
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.TUTORIAL_PREGAME
		});
		Box.Get().SetLightState(BoxLightStateType.TUTORIAL);
		Spell eventSpell = Box.Get().GetEventSpell(BoxEventType.STARTUP_TUTORIAL);
		eventSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnStartupTutorialSpellFinished));
		eventSpell.Activate();
	}

	// Token: 0x0600338F RID: 13199 RVA: 0x00108E04 File Offset: 0x00107004
	private void OnStartupTutorialSpellFinished(Spell spell, object userData)
	{
		Box.Get().AddButtonPressListener(new Box.ButtonPressCallback(this.OnStartButtonPressed));
		Box.Get().ChangeState(Box.State.PRESS_START);
	}

	// Token: 0x06003390 RID: 13200 RVA: 0x00108E28 File Offset: 0x00107028
	private void OnStartButtonPressed(Box.ButtonType buttonType, object userData)
	{
		if (buttonType != Box.ButtonType.START)
		{
			return;
		}
		TelemetryManager.Client().SendButtonPressed("PressToStart");
		if (this.m_nextMissionId == 3)
		{
			AdTrackingManager.Get().TrackTutorialProgress(TutorialProgress.NOTHING_COMPLETE, true);
		}
		Box.Get().RemoveButtonPressListener(new Box.ButtonPressCallback(this.OnStartButtonPressed));
		if (this.m_nextMissionId != 3)
		{
			this.ShowTutorialProgressScreen();
			return;
		}
		if (DemoMgr.Get().GetMode() == DemoMode.BLIZZ_MUSEUM || Network.ShouldBeConnectedToAurora())
		{
			this.StartTutorial();
			return;
		}
		Box.Get().m_StartButton.ChangeState(BoxStartButton.State.HIDDEN);
		DialogManager.Get().ShowExistingAccountPopup(new ExistingAccountPopup.ResponseCallback(this.OnExistingAccountPopupResponse), new DialogManager.DialogProcessCallback(this.OnExistingAccountLoadedCallback));
	}

	// Token: 0x06003391 RID: 13201 RVA: 0x00108ED2 File Offset: 0x001070D2
	private void ShowTutorialProgressScreen()
	{
		Box.Get().m_StartButton.ChangeState(BoxStartButton.State.HIDDEN);
		AssetLoader.Get().InstantiatePrefab("TutorialProgressScreen.prefab:a78bac9caa971494ea8fac23dc1a9bd8", new PrefabCallback<GameObject>(this.OnTutorialProgressScreenCallback), null, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06003392 RID: 13202 RVA: 0x00108F08 File Offset: 0x00107108
	private void OnTutorialProgressScreenCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		TutorialProgressScreen component = go.GetComponent<TutorialProgressScreen>();
		component.SetCoinPressCallback(new HeroCoin.CoinPressCallback(this.StartTutorial));
		component.StartTutorialProgress();
	}

	// Token: 0x06003393 RID: 13203 RVA: 0x00108F27 File Offset: 0x00107127
	private void OnExistingAccountPopupResponse(bool hasAccount)
	{
		this.m_existingAccountPopup.gameObject.SetActive(false);
		if (hasAccount)
		{
			HearthstoneApplication.Get().ResetAndForceLogin();
			return;
		}
		this.StartTutorial();
	}

	// Token: 0x06003394 RID: 13204 RVA: 0x00108F50 File Offset: 0x00107150
	private void StartTutorial()
	{
		MusicManager.Get().StopPlaylist();
		Box.Get().ChangeState(Box.State.CLOSED);
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		GameMgr.Get().FindGame(GameType.GT_TUTORIAL, FormatType.FT_WILD, this.m_nextMissionId, 0, 0L, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x06003395 RID: 13205 RVA: 0x00108FAC File Offset: 0x001071AC
	private bool OnExistingAccountLoadedCallback(DialogBase dialog, object userData)
	{
		this.m_existingAccountPopup = (ExistingAccountPopup)dialog;
		this.m_existingAccountPopup.gameObject.SetActive(true);
		return true;
	}

	// Token: 0x06003396 RID: 13206 RVA: 0x00108FCC File Offset: 0x001071CC
	private void ChangeMode_SetRotation()
	{
		UserAttentionManager.StartBlocking(UserAttentionBlocker.SET_ROTATION_INTRO);
		Spell eventSpell = Box.Get().GetEventSpell(BoxEventType.STARTUP_SET_ROTATION);
		Box.Get().m_StoreButton.gameObject.SetActive(false);
		Box.Get().m_QuestLogButton.gameObject.SetActive(false);
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
		eventSpell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSetRotationSpellFinished));
		eventSpell.Activate();
	}

	// Token: 0x06003397 RID: 13207 RVA: 0x0010908C File Offset: 0x0010728C
	private void OnSetRotationSpellFinished(Spell spell, object userData)
	{
		Processor.QueueJob("Login.GoToNextMode", this.GoToNextMode(), Array.Empty<IJobDependency>());
	}

	// Token: 0x06003398 RID: 13208 RVA: 0x001090A4 File Offset: 0x001072A4
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if (state == FindGameState.SERVER_GAME_STARTED && !GameMgr.Get().IsNextReconnect())
		{
			Spell eventSpell = Box.Get().GetEventSpell(BoxEventType.TUTORIAL_PLAY);
			eventSpell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnTutorialPlaySpellStateFinished));
			eventSpell.ActivateState(SpellStateType.BIRTH);
			return true;
		}
		return false;
	}

	// Token: 0x06003399 RID: 13209 RVA: 0x001090F0 File Offset: 0x001072F0
	private void OnTutorialPlaySpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		SpellStateType activeState = spell.GetActiveState();
		if (prevStateType == SpellStateType.BIRTH)
		{
			LoadingScreen.Get().SetFadeColor(Color.white);
			LoadingScreen.Get().EnableFadeOut(false);
			LoadingScreen.Get().AddTransitionObject(Box.Get().gameObject);
			LoadingScreen.Get().AddTransitionBlocker();
			SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnMissionSceneLoaded));
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.GAMEPLAY, SceneMgr.TransitionHandlerType.SCENEMGR, null);
			return;
		}
		if (activeState == SpellStateType.NONE)
		{
			LoadingScreen.Get().NotifyTransitionBlockerComplete();
			return;
		}
	}

	// Token: 0x0600339A RID: 13210 RVA: 0x00109172 File Offset: 0x00107372
	private void OnMissionSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnMissionSceneLoaded));
		Box.Get().GetEventSpell(BoxEventType.TUTORIAL_PLAY).ActivateState(SpellStateType.ACTION);
	}

	// Token: 0x0600339B RID: 13211 RVA: 0x0010919C File Offset: 0x0010739C
	private void OnStartupHubSpellFinished(Spell spell, object userData)
	{
		HearthstoneApplication.SendStartupTimeTelemetry("Login.OnStartupHubSpellFinished");
		if (Network.ShouldBeConnectedToAurora() && this.m_nextMissionId == 0)
		{
			JobDefinition jobDefinition = Processor.QueueJob("LoginManager.ShowIntroPopups", LoginManager.Get().ShowIntroPopups(), Array.Empty<IJobDependency>());
			IJobDependency[] dependencies = new IJobDependency[]
			{
				jobDefinition.CreateDependency()
			};
			Processor.QueueJob("LoginManager.CompleteLoginFlow", LoginManager.Get().CompleteLoginFlow(), dependencies);
		}
	}

	// Token: 0x04001C4E RID: 7246
	private int m_nextMissionId;

	// Token: 0x04001C4F RID: 7247
	private ExistingAccountPopup m_existingAccountPopup;

	// Token: 0x04001C50 RID: 7248
	private static Login s_instance;

	// Token: 0x04001C51 RID: 7249
	private bool m_blockingBnetBar;
}
