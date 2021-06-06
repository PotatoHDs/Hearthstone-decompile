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

// Token: 0x02000370 RID: 880
public class LoginManager : IService
{
	// Token: 0x14000029 RID: 41
	// (add) Token: 0x060033A3 RID: 13219 RVA: 0x00109234 File Offset: 0x00107434
	// (remove) Token: 0x060033A4 RID: 13220 RVA: 0x0010926C File Offset: 0x0010746C
	public event Action OnAchievesLoaded;

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x060033A5 RID: 13221 RVA: 0x001092A4 File Offset: 0x001074A4
	// (remove) Token: 0x060033A6 RID: 13222 RVA: 0x001092DC File Offset: 0x001074DC
	public event Action OnInitialClientStateReceived;

	// Token: 0x1400002B RID: 43
	// (add) Token: 0x060033A7 RID: 13223 RVA: 0x00109314 File Offset: 0x00107514
	// (remove) Token: 0x060033A8 RID: 13224 RVA: 0x0010934C File Offset: 0x0010754C
	public event Action OnFullLoginFlowComplete;

	// Token: 0x1400002C RID: 44
	// (add) Token: 0x060033A9 RID: 13225 RVA: 0x00109384 File Offset: 0x00107584
	// (remove) Token: 0x060033AA RID: 13226 RVA: 0x001093BC File Offset: 0x001075BC
	public event Action<Network.QueueInfo> OnQueueModifiedEvent;

	// Token: 0x1700050E RID: 1294
	// (get) Token: 0x060033AB RID: 13227 RVA: 0x001093F1 File Offset: 0x001075F1
	// (set) Token: 0x060033AC RID: 13228 RVA: 0x001093F9 File Offset: 0x001075F9
	public Network.QueueInfo CurrentQueueInfo { get; private set; }

	// Token: 0x1700050F RID: 1295
	// (get) Token: 0x060033AD RID: 13229 RVA: 0x00109402 File Offset: 0x00107602
	// (set) Token: 0x060033AE RID: 13230 RVA: 0x0010940A File Offset: 0x0010760A
	public bool IsFullLoginFlowComplete { get; private set; }

	// Token: 0x060033AF RID: 13231 RVA: 0x00109413 File Offset: 0x00107613
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.LoggedInDependency = new WaitForCallback();
		this.ReadyToGoToNextModeDependency = new WaitForCallback();
		this.ReadyToReconnectOrChangeModeDependency = new WaitForCallback();
		this.InitialClientStateReceivedDependency = new WaitForCallback();
		this.LoginScreenNetCacheReceivedDependency = new WaitForCallback();
		this.UpdateLoginCompleteDependency = new WaitForCallback();
		this.SetProgressDependency = new WaitForCallback();
		Network.Get().RegisterNetHandler(AssetsVersionResponse.PacketID.ID, new Network.NetHandler(this.OnAssetsVersionResponse), null);
		Network.Get().RegisterNetHandler(InitialClientState.PacketID.ID, new Network.NetHandler(this.OnInitialClientStateResponse), null);
		Network.Get().RegisterNetHandler(SetProgressResponse.PacketID.ID, new Network.NetHandler(this.OnSetProgressResponse), null);
		Network.Get().RegisterNetHandler(UpdateLoginComplete.PacketID.ID, new Network.NetHandler(this.UpdateLoginCompleteDependency.Callback.Invoke), null);
		this.OnInitialClientStateReceived += this.InitializeManagers;
		HearthstoneApplication.Get().Resetting += this.OnReset;
		this.CurrentQueueInfo = null;
		if (!Vars.Key("Aurora.ClientCheck").GetBool(true) || !BattleNetClient.needsToRun)
		{
			Network.Get().RegisterQueueInfoHandler(new Network.QueueInfoHandler(this.QueueInfoHandler));
		}
		this.BeginLoginProcess();
		yield break;
	}

	// Token: 0x060033B0 RID: 13232 RVA: 0x00109424 File Offset: 0x00107624
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(Network),
			typeof(GameDownloadManager),
			typeof(NetCache),
			typeof(ILoginService),
			typeof(SceneMgr),
			typeof(AchieveManager)
		};
	}

	// Token: 0x060033B1 RID: 13233 RVA: 0x00109488 File Offset: 0x00107688
	public void Shutdown()
	{
		NetCache netCache;
		if (HearthstoneServices.TryGet<NetCache>(out netCache))
		{
			netCache.UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		}
		Network network;
		if (HearthstoneServices.TryGet<Network>(out network))
		{
			network.RemoveNetHandler(AssetsVersionResponse.PacketID.ID, new Network.NetHandler(this.OnAssetsVersionResponse));
			network.RemoveNetHandler(InitialClientState.PacketID.ID, new Network.NetHandler(this.OnInitialClientStateResponse));
			network.RemoveNetHandler(SetProgressResponse.PacketID.ID, new Network.NetHandler(this.OnSetProgressResponse));
			network.RemoveNetHandler(UpdateLoginComplete.PacketID.ID, new Network.NetHandler(this.UpdateLoginCompleteDependency.Callback.Invoke));
		}
	}

	// Token: 0x060033B2 RID: 13234 RVA: 0x00109537 File Offset: 0x00107737
	private void OnReset()
	{
		this.WaitForLogin = null;
		this.BeginLoginProcess();
	}

	// Token: 0x060033B3 RID: 13235 RVA: 0x00109546 File Offset: 0x00107746
	public static LoginManager Get()
	{
		return HearthstoneServices.Get<LoginManager>();
	}

	// Token: 0x060033B4 RID: 13236 RVA: 0x00109550 File Offset: 0x00107750
	public static void RegisterDeterminePostLoginSceneCallback(StartupSceneSource source, DetermineStartupSceneCallback cb)
	{
		DetermineStartupSceneCallback determineStartupSceneCallback;
		if (LoginManager.s_determinePostLoginCallbacks.TryGetValue(source, out determineStartupSceneCallback))
		{
			global::Log.All.PrintError("RegisterDetermineStartupSceneCallback error: source={0} already registered to {1} - will overwrite with {2}.", new object[]
			{
				source,
				determineStartupSceneCallback,
				cb
			});
		}
		LoginManager.s_determinePostLoginCallbacks[source] = cb;
	}

	// Token: 0x060033B5 RID: 13237 RVA: 0x0010959E File Offset: 0x0010779E
	public static SortedList<StartupSceneSource, DetermineStartupSceneCallback> GetPostLoginCallbacks()
	{
		return LoginManager.s_determinePostLoginCallbacks;
	}

	// Token: 0x060033B6 RID: 13238 RVA: 0x001095A8 File Offset: 0x001077A8
	public void BeginLoginProcess()
	{
		this.InitializeForNewLogin();
		if (!Network.ShouldBeConnectedToAurora())
		{
			global::Log.Login.Print("Entering No Account flow.", Array.Empty<object>());
			DefLoader.Get().Initialize();
			this.ReadyToReconnectOrChangeModeDependency.Callback();
			return;
		}
		if (this.WaitForLogin == null)
		{
			global::Log.Login.Print("Entering Login flow.", Array.Empty<object>());
			HearthstoneServices.Get<ILoginService>().StartLogin();
			Network.Get().OnLoginStarted();
			this.WaitForLogin = new JobDefinition("LoginManager.WaitForLogin", this.Job_WaitForLogin(), Array.Empty<IJobDependency>());
			Processor.QueueJob(this.WaitForLogin);
			HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.BeginLoginProcess");
		}
	}

	// Token: 0x060033B7 RID: 13239 RVA: 0x00109654 File Offset: 0x00107854
	public void InitializeForNewLogin()
	{
		this.UpdateLoginCompleteDependency.Reset();
		this.SetProgressDependency.Reset();
		this.ReadyToGoToNextModeDependency.Reset();
		this.ReadyToReconnectOrChangeModeDependency.Reset();
		this.LoggedInDependency.Reset();
		this.InitialClientStateReceivedDependency.Reset();
		this.LoginScreenNetCacheReceivedDependency.Reset();
		this.IsFullLoginFlowComplete = false;
	}

	// Token: 0x060033B8 RID: 13240 RVA: 0x001096B5 File Offset: 0x001078B5
	private IEnumerator<IAsyncJobResult> Job_WaitForLogin()
	{
		for (;;)
		{
			Network.BnetLoginState bnetLoginState = Network.BattleNetStatus();
			if (bnetLoginState == Network.BnetLoginState.BATTLE_NET_LOGGED_IN && BattleNet.GetAccountCountry() != null && BattleNet.GetAccountRegion() != constants.BnetRegion.REGION_UNINITIALIZED)
			{
				break;
			}
			if (bnetLoginState == Network.BnetLoginState.BATTLE_NET_LOGIN_FAILED || bnetLoginState == Network.BnetLoginState.BATTLE_NET_TIMEOUT)
			{
				goto IL_7D;
			}
			yield return null;
		}
		this.WaitForLogin = null;
		global::Log.TemporaryAccount.Print("Is Temporary Account: " + (BattleNet.IsHeadlessAccount() ? "Yes" : "No"), Array.Empty<object>());
		this.OnLoginComplete();
		yield break;
		IL_7D:
		this.WaitForLogin = null;
		Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_LOGIN_FAILURE");
		yield break;
		yield break;
	}

	// Token: 0x060033B9 RID: 13241 RVA: 0x001096C4 File Offset: 0x001078C4
	private IEnumerator<IAsyncJobResult> Job_WaitForStartupPacketSequenceComplete()
	{
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.WaitForStartupPacketSequenceComplete");
		Network.Get().OnStartupPacketSequenceComplete();
		NetCache.Get().RegisterScreenLogin(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		yield break;
	}

	// Token: 0x060033BA RID: 13242 RVA: 0x001096D4 File Offset: 0x001078D4
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
				Options.Get().SetBool(Option.CONNECT_TO_AURORA, true);
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
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.LOGIN
		});
		TemporaryAccountManager.Get().Initialize();
		TemporaryAccountManager.Get().UpdateTemporaryAccountData();
		if (PlatformSettings.IsMobile())
		{
			AdTrackingManager.Get().TrackLogin();
			MobileCallbackManager.SetPushRegistrationInfo(BattleNet.GetMyAccoundId().lo, BattleNet.GetCurrentRegion(), Localization.GetLocaleName());
			if (TemporaryAccountManager.IsTemporaryAccount())
			{
				CloudStorageManager.Get().StartInitialize(new CloudStorageManager.OnInitializedFinished(this.OnCloudStorageInitialized), GameStrings.Get("GLUE_CLOUD_STORAGE_CONTEXT_BODY_01"));
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

	// Token: 0x060033BB RID: 13243 RVA: 0x00109864 File Offset: 0x00107A64
	private void OnProfileProgressResponse()
	{
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.OnProfileProgressResponse");
		if (!Options.Get().GetBool(Option.HAS_SEEN_NEW_CINEMATIC, false) && PlatformSettings.OS == OSCategory.PC)
		{
			HearthstoneServices.Get<Cinematic>().Play(delegate
			{
				this.ReadyToReconnectOrChangeModeDependency.Callback();
			});
			return;
		}
		this.ReadyToReconnectOrChangeModeDependency.Callback();
	}

	// Token: 0x060033BC RID: 13244 RVA: 0x001098BC File Offset: 0x00107ABC
	private void OnNetCacheReady()
	{
		global::Log.Login.Print("LoginManager: Net Cache Ready", Array.Empty<object>());
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		this.LoginScreenNetCacheReceivedDependency.Callback();
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.OnNetCacheReady");
		Processor.QueueJob("LoginManager.WaitForAchievesThenInit", this.Job_WaitForAchievesThenInit(), new IJobDependency[]
		{
			HearthstoneServices.CreateServiceDependency(typeof(SceneMgr)),
			new WaitForBox()
		});
	}

	// Token: 0x060033BD RID: 13245 RVA: 0x00109940 File Offset: 0x00107B40
	private void OnAssetsVersionResponse()
	{
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.OnAssetsVersionResponse");
		AssetsVersionResponse assetsVersion = Network.Get().GetAssetsVersion();
		if (assetsVersion != null && assetsVersion.HasReturningPlayerInfo)
		{
			ReturningPlayerMgr.Get().SetReturningPlayerInfo(assetsVersion.ReturningPlayerInfo);
		}
	}

	// Token: 0x060033BE RID: 13246 RVA: 0x00109980 File Offset: 0x00107B80
	private void OnInitialClientStateResponse()
	{
		global::Log.Login.Print("LoginManager: Assets Version Check Completed", Array.Empty<object>());
		this.InitialClientStateReceivedDependency.Callback();
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
				this.SetProgressDependency.Callback();
			}
		}
		network.ResetConnectionFailureCount();
		network.DoLoginUpdate();
		Processor.QueueJob("LoginManager.WaitForStartupPacketSequenceComplete", this.Job_WaitForStartupPacketSequenceComplete(), new IJobDependency[]
		{
			this.SetProgressDependency,
			this.UpdateLoginCompleteDependency
		});
	}

	// Token: 0x060033BF RID: 13247 RVA: 0x00109A88 File Offset: 0x00107C88
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
			Debug.LogWarning(string.Format("LoginManager.OnSetProgressResponse(): received unexpected result {0}", setProgressResponse.Result_));
		}
		this.SetProgressDependency.Callback();
	}

	// Token: 0x060033C0 RID: 13248 RVA: 0x00109AF1 File Offset: 0x00107CF1
	private void OnCloudStorageInitialized()
	{
		if (PushNotificationManager.Get().AllowRegisterPushAtLogin())
		{
			if (TemporaryAccountManager.Get().IsSelectedTemporaryAccountMinor())
			{
				MobileCallbackManager.LogoutPushNotifications();
				return;
			}
			MobileCallbackManager.RegisterPushNotifications();
		}
	}

	// Token: 0x060033C1 RID: 13249 RVA: 0x00109B18 File Offset: 0x00107D18
	private void OnLoginComplete()
	{
		this.LoggedInDependency.Callback();
		global::Log.Login.Print("LoginManager: OnLoginComplete", Array.Empty<object>());
		HearthstoneApplication.SendStartupTimeTelemetry("LoginManager.OnLoginComplete");
		MobileCallbackManager.SetPushRegistrationInfo(BattleNet.GetMyAccoundId().lo, BattleNet.GetCurrentRegion(), Localization.GetLocaleName());
		HsAppsFlyer.SetCustomerUserId(string.Format(CultureInfo.InvariantCulture, "{0:D}", BattleNet.GetMyAccoundId().lo));
		DefLoader.Get().Initialize();
		CollectionManager.Init();
		InnKeepersSpecial.Get().InitializeURLAndUpdate();
		InGameMessageScheduler inGameMessageScheduler = InGameMessageScheduler.Get();
		if (inGameMessageScheduler != null)
		{
			inGameMessageScheduler.OnLoginCompleted();
		}
		StoreManager.Get().Init();
		Network network = Network.Get();
		network.LoginOk();
		network.UpdateCachedBnetValues();
		global::Log.Login.Print("LoginManager: Requesting assets version and initial client state.", Array.Empty<object>());
		network.RequestAssetsVersion();
		NetCache.Get().RegisterScreenStartup(new NetCache.NetCacheCallback(this.OnProfileProgressResponse));
	}

	// Token: 0x060033C2 RID: 13250 RVA: 0x00109C02 File Offset: 0x00107E02
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
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			bool flag2 = netObject != null && netObject.FSGEnabled;
			FSGFeatureConfig netObject2 = NetCache.Get().GetNetObject<FSGFeatureConfig>();
			flag = (!flag2 || netObject2 != null);
		}
		if (this.OnAchievesLoaded != null)
		{
			this.OnAchievesLoaded();
		}
		global::Log.Login.Print("LoginManager: Achieves Loaded", Array.Empty<object>());
		global::Log.Downloader.Print("LOADING PROCESS COMPLETE at " + Time.realtimeSinceStartup, Array.Empty<object>());
		yield break;
	}

	// Token: 0x060033C3 RID: 13251 RVA: 0x00109C11 File Offset: 0x00107E11
	public IEnumerator<IAsyncJobResult> ShowIntroPopups()
	{
		global::Log.Login.Print("LoginManager: Showing Intro Popups", Array.Empty<object>());
		yield return new JobDefinition("DialogManager.WaitForSeasonEndPopup", DialogManager.Get().Job_WaitForSeasonEndPopup(), Array.Empty<IJobDependency>());
		yield return new JobDefinition("PopupDisplayManager.WaitForAllPopups", PopupDisplayManager.Get().Job_WaitForAllPopups(), Array.Empty<IJobDependency>());
		yield return new JobDefinition("NarrativeManager.WaitForOutstandingCharacterDialog", NarrativeManager.Get().Job_WaitForOutstandingCharacterDialog(), Array.Empty<IJobDependency>());
		yield return new JobDefinition("LoginManager.ShowWelcomeQuests", this.Job_ShowWelcomeQuests(), Array.Empty<IJobDependency>());
		yield return new JobDefinition("LoginManager.ShowBreakingNews", this.Job_ShowBreakingNews(), Array.Empty<IJobDependency>());
		yield return new JobDefinition("LoginManager.ShowAlertDialogs", this.Job_ShowAlertDialogs(), Array.Empty<IJobDependency>());
		yield break;
	}

	// Token: 0x060033C4 RID: 13252 RVA: 0x00109C20 File Offset: 0x00107E20
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

	// Token: 0x060033C5 RID: 13253 RVA: 0x00109C4B File Offset: 0x00107E4B
	private IEnumerator<IAsyncJobResult> Job_ShowWelcomeQuests()
	{
		while (InnKeepersSpecial.Get().ProcessingResponse)
		{
			yield return new WaitForDuration(0.5f);
		}
		WaitForCallback waitForCallback = new WaitForCallback();
		bool flag = AchieveManager.Get().GetActiveQuests(false).Any((global::Achievement a) => a.IsNewlyActive());
		if (!QuestManager.Get().IsSystemEnabled && SceneMgr.Get().GetMode() == SceneMgr.Mode.LOGIN && flag)
		{
			WelcomeQuests.Show(UserAttentionBlocker.NONE, true, new WelcomeQuests.DelOnWelcomeQuestsClosed(waitForCallback.Callback.Invoke), true);
			yield return waitForCallback;
		}
		yield break;
	}

	// Token: 0x060033C6 RID: 13254 RVA: 0x00109C53 File Offset: 0x00107E53
	private IEnumerator<IAsyncJobResult> Job_ShowBreakingNews()
	{
		if (BreakingNews.ShouldShowForCurrentPlatform)
		{
			WaitForCallback waitForCallback = new WaitForCallback();
			this.ShowBreakingNews(waitForCallback.Callback);
			yield return waitForCallback;
		}
		yield break;
	}

	// Token: 0x060033C7 RID: 13255 RVA: 0x00109C62 File Offset: 0x00107E62
	private IEnumerator<IAsyncJobResult> Job_ShowAlertDialogs()
	{
		WaitForCallback waitForCallback = new WaitForCallback();
		this.ShowGoldCapAlert(waitForCallback.Callback);
		yield return waitForCallback;
		yield break;
	}

	// Token: 0x060033C8 RID: 13256 RVA: 0x00109C71 File Offset: 0x00107E71
	public IEnumerator<IAsyncJobResult> CompleteLoginFlow()
	{
		this.IsFullLoginFlowComplete = true;
		if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.LOGIN))
		{
			yield break;
		}
		if (this.OnFullLoginFlowComplete != null)
		{
			this.OnFullLoginFlowComplete();
		}
		global::Log.Login.Print("LoginManager: Complete Login Flow", Array.Empty<object>());
		this.ReadyToGoToNextModeDependency.Callback();
		yield break;
	}

	// Token: 0x060033C9 RID: 13257 RVA: 0x00109C80 File Offset: 0x00107E80
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
		popupInfo.m_headerText = GameStrings.Format("GLUE_GOLD_CAP_HEADER", new object[]
		{
			cap.ToString()
		});
		popupInfo.m_text = GameStrings.Format("GLUE_GOLD_CAP_BODY", new object[]
		{
			cap.ToString()
		});
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response <p0>, object <p1>)
		{
			callback();
		};
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x060033CA RID: 13258 RVA: 0x00109D54 File Offset: 0x00107F54
	private void ShowBreakingNews(Action callback)
	{
		if (BreakingNews.Get().GetStatus() != BreakingNews.Status.Available && !Cheats.ShowFakeBreakingNews)
		{
			if (!Application.isEditor)
			{
				Debug.LogWarning("Breaking News response is taking too long!");
			}
			callback();
			return;
		}
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
			popupInfo.m_responseCallback = delegate(AlertPopup.Response <p0>, object <p1>)
			{
				callback();
			};
			DialogManager.Get().ShowPopup(popupInfo);
			return;
		}
		callback();
	}

	// Token: 0x060033CB RID: 13259 RVA: 0x00109E45 File Offset: 0x00108045
	private void QueueInfoHandler(Network.QueueInfo queueInfo)
	{
		this.CurrentQueueInfo = queueInfo;
		if (this.OnQueueModifiedEvent != null)
		{
			this.OnQueueModifiedEvent(queueInfo);
		}
	}

	// Token: 0x060033CC RID: 13260 RVA: 0x00109E62 File Offset: 0x00108062
	public void RegisterQueueModifiedListener(Action<Network.QueueInfo> listener)
	{
		this.OnQueueModifiedEvent -= listener;
		this.OnQueueModifiedEvent += listener;
	}

	// Token: 0x060033CD RID: 13261 RVA: 0x00109E72 File Offset: 0x00108072
	public void RemoveQueueModifiedListener(Action<Network.QueueInfo> listener)
	{
		this.OnQueueModifiedEvent -= listener;
	}

	// Token: 0x04001C52 RID: 7250
	private WaitForCallback UpdateLoginCompleteDependency;

	// Token: 0x04001C53 RID: 7251
	private WaitForCallback SetProgressDependency;

	// Token: 0x04001C54 RID: 7252
	private static SortedList<StartupSceneSource, DetermineStartupSceneCallback> s_determinePostLoginCallbacks = new SortedList<StartupSceneSource, DetermineStartupSceneCallback>();

	// Token: 0x04001C55 RID: 7253
	private JobDefinition WaitForLogin;

	// Token: 0x04001C56 RID: 7254
	public WaitForCallback LoggedInDependency;

	// Token: 0x04001C57 RID: 7255
	public WaitForCallback ReadyToGoToNextModeDependency;

	// Token: 0x04001C58 RID: 7256
	public WaitForCallback ReadyToReconnectOrChangeModeDependency;

	// Token: 0x04001C59 RID: 7257
	public WaitForCallback InitialClientStateReceivedDependency;

	// Token: 0x04001C5A RID: 7258
	public WaitForCallback LoginScreenNetCacheReceivedDependency;
}
