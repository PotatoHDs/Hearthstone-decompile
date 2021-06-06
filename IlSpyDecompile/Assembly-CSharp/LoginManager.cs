using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.InGameMessage;
using Hearthstone.Login;
using Hearthstone.Progression;
using Hearthstone.Streaming;
using PegasusFSG;
using PegasusUtil;
using UnityEngine;

public class LoginManager : IService
{
	private WaitForCallback UpdateLoginCompleteDependency;

	private WaitForCallback SetProgressDependency;

	private static SortedList<StartupSceneSource, DetermineStartupSceneCallback> s_determinePostLoginCallbacks = new SortedList<StartupSceneSource, DetermineStartupSceneCallback>();

	private JobDefinition WaitForLogin;

	public WaitForCallback LoggedInDependency;

	public WaitForCallback ReadyToGoToNextModeDependency;

	public WaitForCallback ReadyToReconnectOrChangeModeDependency;

	public WaitForCallback InitialClientStateReceivedDependency;

	public WaitForCallback LoginScreenNetCacheReceivedDependency;

	public Network.QueueInfo CurrentQueueInfo { get; private set; }

	public bool IsFullLoginFlowComplete { get; private set; }

	public event Action OnAchievesLoaded;

	public event Action OnInitialClientStateReceived;

	public event Action OnFullLoginFlowComplete;

	public event Action<Network.QueueInfo> OnQueueModifiedEvent;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		LoggedInDependency = new WaitForCallback();
		ReadyToGoToNextModeDependency = new WaitForCallback();
		ReadyToReconnectOrChangeModeDependency = new WaitForCallback();
		InitialClientStateReceivedDependency = new WaitForCallback();
		LoginScreenNetCacheReceivedDependency = new WaitForCallback();
		UpdateLoginCompleteDependency = new WaitForCallback();
		SetProgressDependency = new WaitForCallback();
		Network.Get().RegisterNetHandler(AssetsVersionResponse.PacketID.ID, OnAssetsVersionResponse);
		Network.Get().RegisterNetHandler(InitialClientState.PacketID.ID, OnInitialClientStateResponse);
		Network.Get().RegisterNetHandler(SetProgressResponse.PacketID.ID, OnSetProgressResponse);
		Network.Get().RegisterNetHandler(UpdateLoginComplete.PacketID.ID, UpdateLoginCompleteDependency.Callback.Invoke);
		OnInitialClientStateReceived += InitializeManagers;
		HearthstoneApplication.Get().Resetting += OnReset;
		CurrentQueueInfo = null;
		if (!Vars.Key("Aurora.ClientCheck").GetBool(def: true) || !BattleNetClient.needsToRun)
		{
			Network.Get().RegisterQueueInfoHandler(QueueInfoHandler);
		}
		BeginLoginProcess();
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[6]
		{
			typeof(Network),
			typeof(GameDownloadManager),
			typeof(NetCache),
			typeof(ILoginService),
			typeof(SceneMgr),
			typeof(AchieveManager)
		};
	}

	public void Shutdown()
	{
		if (HearthstoneServices.TryGet<NetCache>(out var service))
		{
			service.UnregisterNetCacheHandler(OnNetCacheReady);
		}
		if (HearthstoneServices.TryGet<Network>(out var service2))
		{
			service2.RemoveNetHandler(AssetsVersionResponse.PacketID.ID, OnAssetsVersionResponse);
			service2.RemoveNetHandler(InitialClientState.PacketID.ID, OnInitialClientStateResponse);
			service2.RemoveNetHandler(SetProgressResponse.PacketID.ID, OnSetProgressResponse);
			service2.RemoveNetHandler(UpdateLoginComplete.PacketID.ID, UpdateLoginCompleteDependency.Callback.Invoke);
		}
	}

	private void OnReset()
	{
		WaitForLogin = null;
		BeginLoginProcess();
	}

	public static LoginManager Get()
	{
		return HearthstoneServices.Get<LoginManager>();
	}

	public static void RegisterDeterminePostLoginSceneCallback(StartupSceneSource source, DetermineStartupSceneCallback cb)
	{
		if (s_determinePostLoginCallbacks.TryGetValue(source, out var value))
		{
			Log.All.PrintError("RegisterDetermineStartupSceneCallback error: source={0} already registered to {1} - will overwrite with {2}.", source, value, cb);
		}
		s_determinePostLoginCallbacks[source] = cb;
	}

	public static SortedList<StartupSceneSource, DetermineStartupSceneCallback> GetPostLoginCallbacks()
	{
		return s_determinePostLoginCallbacks;
	}

	public void BeginLoginProcess()
	{
		InitializeForNewLogin();
		if (!Network.ShouldBeConnectedToAurora())
		{
			Log.Login.Print("Entering No Account flow.");
			DefLoader.Get().Initialize();
			ReadyToReconnectOrChangeModeDependency.Callback();
		}
		else if (WaitForLogin == null)
		{
			Log.Login.Print("Entering Login flow.");
			HearthstoneServices.Get<ILoginService>().StartLogin();
			Network.Get().OnLoginStarted();
			WaitForLogin = new JobDefinition("LoginManager.WaitForLogin", Job_WaitForLogin());
			Processor.QueueJob(WaitForLogin);
			HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.BeginLoginProcess");
		}
	}

	public void InitializeForNewLogin()
	{
		UpdateLoginCompleteDependency.Reset();
		SetProgressDependency.Reset();
		ReadyToGoToNextModeDependency.Reset();
		ReadyToReconnectOrChangeModeDependency.Reset();
		LoggedInDependency.Reset();
		InitialClientStateReceivedDependency.Reset();
		LoginScreenNetCacheReceivedDependency.Reset();
		IsFullLoginFlowComplete = false;
	}

	private IEnumerator<IAsyncJobResult> Job_WaitForLogin()
	{
		while (true)
		{
			Network.BnetLoginState bnetLoginState = Network.BattleNetStatus();
			if (bnetLoginState == Network.BnetLoginState.BATTLE_NET_LOGGED_IN && BattleNet.GetAccountCountry() != null && BattleNet.GetAccountRegion() != constants.BnetRegion.REGION_UNINITIALIZED)
			{
				WaitForLogin = null;
				Log.TemporaryAccount.Print("Is Temporary Account: " + (BattleNet.IsHeadlessAccount() ? "Yes" : "No"));
				OnLoginComplete();
				yield break;
			}
			if (bnetLoginState == Network.BnetLoginState.BATTLE_NET_LOGIN_FAILED || bnetLoginState == Network.BnetLoginState.BATTLE_NET_TIMEOUT)
			{
				break;
			}
			yield return null;
		}
		WaitForLogin = null;
		Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_LOGIN_FAILURE");
	}

	private IEnumerator<IAsyncJobResult> Job_WaitForStartupPacketSequenceComplete()
	{
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.WaitForStartupPacketSequenceComplete");
		Network.Get().OnStartupPacketSequenceComplete();
		NetCache.Get().RegisterScreenLogin(OnNetCacheReady);
		yield break;
	}

	private void InitializeManagers()
	{
		if (Network.IsLoggedIn())
		{
			TelemetryManager.RebuildContext();
			BnetPresenceMgr.Get().Initialize();
			BnetFriendMgr.Get().Initialize();
			BnetWhisperMgr.Get().Initialize();
			BnetNearbyPlayerMgr.Get().Initialize();
			FriendChallengeMgr.Get().OnLoggedIn();
			SpectatorManager.Get().InitializeConnectedToBnet();
			NarrativeManager.Get().Initialize();
			if (!Options.Get().GetBool(Option.CONNECT_TO_AURORA))
			{
				Options.Get().SetBool(Option.CONNECT_TO_AURORA, val: true);
			}
			if (PlatformSettings.IsMobile() && Options.Get().GetInt(Option.PREFERRED_REGION) != (int)MobileDeviceLocale.GetCurrentRegionId())
			{
				Options.Get().SetInt(Option.PREFERRED_REGION, (int)MobileDeviceLocale.GetCurrentRegionId());
			}
			if (Options.Get().GetBool(Option.CREATED_ACCOUNT))
			{
				AdTrackingManager.Get().TrackAccountCreated();
				if (PlatformSettings.IsMobile())
				{
					AchieveManager.Get().NotifyOfAccountCreation();
				}
				Options.Get().DeleteOption(Option.CREATED_ACCOUNT);
			}
		}
		RAFManager.Get().InitializeRequests();
		Tournament.Init();
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.LOGIN);
		TemporaryAccountManager.Get().Initialize();
		TemporaryAccountManager.Get().UpdateTemporaryAccountData();
		if (PlatformSettings.IsMobile())
		{
			AdTrackingManager.Get().TrackLogin();
			MobileCallbackManager.SetPushRegistrationInfo(BattleNet.GetMyAccoundId().lo, BattleNet.GetCurrentRegion(), Localization.GetLocaleName());
			if (TemporaryAccountManager.IsTemporaryAccount())
			{
				CloudStorageManager.Get().StartInitialize(OnCloudStorageInitialized, GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_BODY_01"));
			}
			else
			{
				if (PushNotificationManager.Get().AllowRegisterPushAtLogin())
				{
					MobileCallbackManager.RegisterPushNotifications();
				}
				MobileCallbackManager.SendPushAcknowledgement();
			}
		}
		SceneMgr.Get().LoadShaderPreCompiler();
	}

	private void OnProfileProgressResponse()
	{
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.OnProfileProgressResponse");
		if (!Options.Get().GetBool(Option.HAS_SEEN_NEW_CINEMATIC, defaultVal: false) && PlatformSettings.OS == OSCategory.PC)
		{
			HearthstoneServices.Get<Cinematic>().Play(delegate
			{
				ReadyToReconnectOrChangeModeDependency.Callback();
			});
		}
		else
		{
			ReadyToReconnectOrChangeModeDependency.Callback();
		}
	}

	private void OnNetCacheReady()
	{
		Log.Login.Print("LoginManager: Net Cache Ready");
		NetCache.Get().UnregisterNetCacheHandler(OnNetCacheReady);
		LoginScreenNetCacheReceivedDependency.Callback();
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.OnNetCacheReady");
		Processor.QueueJob("LoginManager.WaitForAchievesThenInit", Job_WaitForAchievesThenInit(), HearthstoneServices.CreateServiceDependency(typeof(SceneMgr)), new WaitForBox());
	}

	private void OnAssetsVersionResponse()
	{
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.OnAssetsVersionResponse");
		AssetsVersionResponse assetsVersion = Network.Get().GetAssetsVersion();
		if (assetsVersion != null && assetsVersion.HasReturningPlayerInfo)
		{
			ReturningPlayerMgr.Get().SetReturningPlayerInfo(assetsVersion.ReturningPlayerInfo);
		}
	}

	private void OnInitialClientStateResponse()
	{
		Log.Login.Print("LoginManager: Assets Version Check Completed");
		InitialClientStateReceivedDependency.Callback();
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.OnInitialClientStateResponse");
		if (this.OnInitialClientStateReceived != null)
		{
			this.OnInitialClientStateReceived();
		}
		Box.Get().OnLoggedIn();
		BaseUI.Get().OnLoggedIn();
		InactivePlayerKicker.Get().OnLoggedIn();
		HealthyGamingMgr.Get().OnLoggedIn();
		GameMgr.Get().OnLoggedIn();
		DraftManager.Get().OnLoggedIn();
		AccountLicenseMgr.Get().InitRequests();
		AdventureProgressMgr.InitRequests();
		Network network = Network.Get();
		if (Network.IsLoggedIn())
		{
			TutorialProgress @enum = Options.Get().GetEnum<TutorialProgress>(Option.LOCAL_TUTORIAL_PROGRESS);
			if (@enum > TutorialProgress.NOTHING_COMPLETE)
			{
				network.SetProgress((long)@enum);
			}
			else
			{
				SetProgressDependency.Callback();
			}
		}
		network.ResetConnectionFailureCount();
		network.DoLoginUpdate();
		Processor.QueueJob("LoginManager.WaitForStartupPacketSequenceComplete", Job_WaitForStartupPacketSequenceComplete(), SetProgressDependency, UpdateLoginCompleteDependency);
	}

	private void OnSetProgressResponse()
	{
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.OnSetProgressResponse");
		SetProgressResponse setProgressResponse = Network.Get().GetSetProgressResponse();
		SetProgressResponse.Result result_ = setProgressResponse.Result_;
		if (result_ == SetProgressResponse.Result.SUCCESS || result_ == SetProgressResponse.Result.ALREADY_DONE)
		{
			Options.Get().DeleteOption(Option.LOCAL_TUTORIAL_PROGRESS);
		}
		else
		{
			Debug.LogWarning($"LoginManager.OnSetProgressResponse(): received unexpected result {setProgressResponse.Result_}");
		}
		SetProgressDependency.Callback();
	}

	private void OnCloudStorageInitialized()
	{
		if (PushNotificationManager.Get().AllowRegisterPushAtLogin())
		{
			if (TemporaryAccountManager.Get().IsSelectedTemporaryAccountMinor())
			{
				MobileCallbackManager.LogoutPushNotifications();
			}
			else
			{
				MobileCallbackManager.RegisterPushNotifications();
			}
		}
	}

	private void OnLoginComplete()
	{
		LoggedInDependency.Callback();
		Log.Login.Print("LoginManager: OnLoginComplete");
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.OnLoginComplete");
		MobileCallbackManager.SetPushRegistrationInfo(BattleNet.GetMyAccoundId().lo, BattleNet.GetCurrentRegion(), Localization.GetLocaleName());
		HsAppsFlyer.SetCustomerUserId(string.Format(CultureInfo.InvariantCulture, "{0:D}", BattleNet.GetMyAccoundId().lo));
		DefLoader.Get().Initialize();
		CollectionManager.Init();
		InnKeepersSpecial.Get().InitializeURLAndUpdate();
		InGameMessageScheduler.Get()?.OnLoginCompleted();
		StoreManager.Get().Init();
		Network network = Network.Get();
		network.LoginOk();
		network.UpdateCachedBnetValues();
		Log.Login.Print("LoginManager: Requesting assets version and initial client state.");
		network.RequestAssetsVersion();
		NetCache.Get().RegisterScreenStartup(OnProfileProgressResponse);
	}

	private IEnumerator<IAsyncJobResult> Job_WaitForAchievesThenInit()
	{
		while (DownloadableDbfCache.Get().IsRequiredClientStaticAssetsStillPending)
		{
			yield return null;
		}
		while (!AdventureProgressMgr.Get().IsReady)
		{
			yield return null;
		}
		FixedRewardsMgr.Get().InitStartupFixedRewards();
		bool flag = false;
		float startTime = Time.realtimeSinceStartup;
		while (!flag)
		{
			yield return null;
			if (Time.realtimeSinceStartup - startTime > 10f)
			{
				break;
			}
			bool num = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>()?.FSGEnabled ?? false;
			FSGFeatureConfig netObject = NetCache.Get().GetNetObject<FSGFeatureConfig>();
			flag = !num || netObject != null;
		}
		if (this.OnAchievesLoaded != null)
		{
			this.OnAchievesLoaded();
		}
		Log.Login.Print("LoginManager: Achieves Loaded");
		Log.Downloader.Print("LOADING PROCESS COMPLETE at " + Time.realtimeSinceStartup);
	}

	public IEnumerator<IAsyncJobResult> ShowIntroPopups()
	{
		Log.Login.Print("LoginManager: Showing Intro Popups");
		yield return new JobDefinition("DialogManager.WaitForSeasonEndPopup", DialogManager.Get().Job_WaitForSeasonEndPopup());
		yield return new JobDefinition("PopupDisplayManager.WaitForAllPopups", PopupDisplayManager.Get().Job_WaitForAllPopups());
		yield return new JobDefinition("NarrativeManager.WaitForOutstandingCharacterDialog", NarrativeManager.Get().Job_WaitForOutstandingCharacterDialog());
		yield return new JobDefinition("LoginManager.ShowWelcomeQuests", Job_ShowWelcomeQuests());
		yield return new JobDefinition("LoginManager.ShowBreakingNews", Job_ShowBreakingNews());
		yield return new JobDefinition("LoginManager.ShowAlertDialogs", Job_ShowAlertDialogs());
	}

	public bool AttemptToReconnectToGame(ReconnectMgr.TimeoutCallback timeoutCallback)
	{
		if (GameMgr.Get().ConnectToGameIfHaveDeferredConnectionPacket())
		{
			return true;
		}
		if (ReconnectMgr.Get().ReconnectToGameFromLogin())
		{
			ReconnectMgr.Get().AddTimeoutListener(timeoutCallback);
			return true;
		}
		return false;
	}

	private IEnumerator<IAsyncJobResult> Job_ShowWelcomeQuests()
	{
		while (InnKeepersSpecial.Get().ProcessingResponse)
		{
			yield return new WaitForDuration(0.5f);
		}
		WaitForCallback waitForCallback = new WaitForCallback();
		bool flag = AchieveManager.Get().GetActiveQuests().Any((Achievement a) => a.IsNewlyActive());
		if (!QuestManager.Get().IsSystemEnabled && SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN && flag)
		{
			WelcomeQuests.Show(UserAttentionBlocker.NONE, fromLogin: true, waitForCallback.Callback.Invoke, keepRichPresence: true);
			yield return waitForCallback;
		}
	}

	private IEnumerator<IAsyncJobResult> Job_ShowBreakingNews()
	{
		if (BreakingNews.ShouldShowForCurrentPlatform)
		{
			WaitForCallback waitForCallback = new WaitForCallback();
			ShowBreakingNews(waitForCallback.Callback);
			yield return waitForCallback;
		}
	}

	private IEnumerator<IAsyncJobResult> Job_ShowAlertDialogs()
	{
		WaitForCallback waitForCallback = new WaitForCallback();
		ShowGoldCapAlert(waitForCallback.Callback);
		yield return waitForCallback;
	}

	public IEnumerator<IAsyncJobResult> CompleteLoginFlow()
	{
		IsFullLoginFlowComplete = true;
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.LOGIN))
		{
			if (this.OnFullLoginFlowComplete != null)
			{
				this.OnFullLoginFlowComplete();
			}
			Log.Login.Print("LoginManager: Complete Login Flow");
			ReadyToGoToNextModeDependency.Callback();
		}
		yield break;
	}

	private void ShowGoldCapAlert(Action callback)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber("Login.ShowGoldCapAlert"))
		{
			callback();
			return;
		}
		NetCache.NetCacheGoldBalance netObject = NetCache.Get().GetNetObject<NetCache.NetCacheGoldBalance>();
		long cap = netObject.Cap;
		if (netObject.GetTotal() < cap || cap == 0L)
		{
			callback();
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_attentionCategory = UserAttentionBlocker.NONE;
		popupInfo.m_headerText = GameStrings.Format("GLUE_GOLD_CAP_HEADER", cap.ToString());
		popupInfo.m_text = GameStrings.Format("GLUE_GOLD_CAP_BODY", cap.ToString());
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseCallback = delegate
		{
			callback();
		};
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void ShowBreakingNews(Action callback)
	{
		if (BreakingNews.Get().GetStatus() == BreakingNews.Status.Available || Cheats.ShowFakeBreakingNews)
		{
			string text = BreakingNews.Get().GetText();
			if (string.IsNullOrEmpty(text) && Cheats.ShowFakeBreakingNews)
			{
				text = "FAKE BREAKING NEWS ARE BREAKING NOW";
				UIStatus.Get().AddInfo("SHOWING FAKE BREAKING NEWS!\nTo disable this, remove ShowFakeBreakingNews from client.config", 5f);
			}
			if (!string.IsNullOrEmpty(text) && !MobileCallbackManager.Get().m_wasBreakingNewsShown)
			{
				AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
				popupInfo.m_headerText = GameStrings.Get("GLUE_MOBILE_SPLASH_SCREEN_BREAKING_NEWS");
				popupInfo.m_text = text;
				popupInfo.m_showAlertIcon = true;
				popupInfo.m_richTextEnabled = false;
				popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				popupInfo.m_responseCallback = delegate
				{
					callback();
				};
				DialogManager.Get().ShowPopup(popupInfo);
			}
			else
			{
				callback();
			}
		}
		else
		{
			if (!Application.isEditor)
			{
				Debug.LogWarning("Breaking News response is taking too long!");
			}
			callback();
		}
	}

	private void QueueInfoHandler(Network.QueueInfo queueInfo)
	{
		CurrentQueueInfo = queueInfo;
		if (this.OnQueueModifiedEvent != null)
		{
			this.OnQueueModifiedEvent(queueInfo);
		}
	}

	public void RegisterQueueModifiedListener(Action<Network.QueueInfo> listener)
	{
		OnQueueModifiedEvent -= listener;
		OnQueueModifiedEvent += listener;
	}

	public void RemoveQueueModifiedListener(Action<Network.QueueInfo> listener)
	{
		OnQueueModifiedEvent -= listener;
	}
}
