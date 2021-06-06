using System;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using Hearthstone.Core.Deeplinking;
using Hearthstone.Core.Streaming;
using Hearthstone.Extensions;
using Hearthstone.InGameMessage;
using Hearthstone.InGameMessage.UI;
using Hearthstone.Login;
using Hearthstone.Progression;
using Hearthstone.Streaming;
using Hearthstone.UI;
using UnityEngine;

public static class HearthstoneServices
{
	private static ServiceLocator s_runtimeServices;

	private static DynamicServiceLocator s_dynamicServices;

	private static DynamicServiceLocator s_testServices;

	private static List<GameObject> s_serviceObjects;

	private static void RegisterDefaultRuntimeServices()
	{
		if (s_runtimeServices != null)
		{
			s_runtimeServices.RegisterService<GameDownloadManager>();
			s_runtimeServices.RegisterService<Network>();
			s_runtimeServices.RegisterService<DownloadableDbfCache>();
			s_runtimeServices.RegisterService<SpecialEventManager>();
			s_runtimeServices.RegisterService<GameMgr>();
			s_runtimeServices.RegisterService<DraftManager>();
			s_runtimeServices.RegisterService<ChangedCardMgr>();
			s_runtimeServices.RegisterService<AdventureProgressMgr>();
			s_runtimeServices.RegisterService<AchieveManager>();
			s_runtimeServices.RegisterService<AchievementManager>();
			s_runtimeServices.RegisterService<QuestManager>();
			s_runtimeServices.RegisterService<RewardTrackManager>();
			s_runtimeServices.RegisterService<GenericRewardChestNoticeManager>();
			s_runtimeServices.RegisterService<AccountLicenseMgr>();
			s_runtimeServices.RegisterService<FixedRewardsMgr>();
			s_runtimeServices.RegisterService<ReturningPlayerMgr>();
			s_runtimeServices.RegisterService<DemoMgr>();
			s_runtimeServices.RegisterService<NetCache>();
			s_runtimeServices.RegisterService<GameDbf>();
			s_runtimeServices.RegisterService<DebugConsole>();
			s_runtimeServices.RegisterService<TavernBrawlManager>();
			s_runtimeServices.RegisterService<IAssetLoader>(new AssetLoader());
			s_runtimeServices.RegisterService<IAliasedAssetResolver>(new AliasedAssetResolver());
			s_runtimeServices.RegisterService<LoginManager>();
			s_runtimeServices.RegisterService<CardBackManager>();
			s_runtimeServices.RegisterService<CheatMgr>();
			s_runtimeServices.RegisterService<Cheats>();
			s_runtimeServices.RegisterService<ReconnectMgr>();
			s_runtimeServices.RegisterService<DisconnectMgr>();
			s_runtimeServices.RegisterService<HealthyGamingMgr>();
			s_runtimeServices.RegisterService<InGameBrowserManager>();
			s_runtimeServices.RegisterService<SoundManager>();
			s_runtimeServices.RegisterService<MusicManager>();
			s_runtimeServices.RegisterService<RAFManager>();
			s_runtimeServices.RegisterService<InactivePlayerKicker>();
			s_runtimeServices.RegisterService<ClientLocationManager>();
			s_runtimeServices.RegisterService<AdTrackingManager>();
			s_runtimeServices.RegisterService<SpellCache>();
			s_runtimeServices.RegisterService<LocationServices>();
			s_runtimeServices.RegisterService<WifiInfo>();
			s_runtimeServices.RegisterService<FiresideGatheringManager>();
			s_runtimeServices.RegisterService<GameplayErrorManager>();
			s_runtimeServices.RegisterService<FontTable>();
			s_runtimeServices.RegisterService<UniversalInputManager>();
			s_runtimeServices.RegisterService<ScreenEffectsMgr>();
			s_runtimeServices.RegisterService<ShownUIMgr>();
			s_runtimeServices.RegisterService<PerformanceAnalytics>();
			s_runtimeServices.RegisterService<PopupDisplayManager>();
			s_runtimeServices.RegisterService<GraphicsManager>();
			s_runtimeServices.RegisterService<MobilePermissionsManager>();
			s_runtimeServices.RegisterService<ShaderTime>();
			s_runtimeServices.RegisterService<MobileCallbackManager>(CreateMonobehaviourService<MobileCallbackManager>());
			s_runtimeServices.RegisterService<FullScreenFXMgr>();
			s_runtimeServices.RegisterService<SceneMgr>();
			s_runtimeServices.RegisterService<SetRotationManager>();
			s_runtimeServices.RegisterService<Cinematic>();
			s_runtimeServices.RegisterService<WidgetRunner>();
			s_runtimeServices.RegisterService<HearthstoneCheckout>();
			s_runtimeServices.RegisterService<NetworkReachabilityManager>();
			s_runtimeServices.RegisterService<VersionConfigurationService>();
			s_runtimeServices.RegisterService<DeeplinkService>();
			s_runtimeServices.RegisterService<ILoginService>(CreateLoginService());
			s_runtimeServices.RegisterService<PartyManager>();
			s_runtimeServices.RegisterService<PlayerMigrationManager>();
			s_runtimeServices.RegisterService<CoinManager>();
			s_runtimeServices.RegisterService<QuestToastManager>();
			s_runtimeServices.RegisterService<RewardXpNotificationManager>();
			s_runtimeServices.RegisterService<MaterialManagerService>();
			s_runtimeServices.RegisterService<InGameMessageScheduler>();
			s_runtimeServices.RegisterService<DisposablesCleaner>();
			s_runtimeServices.RegisterService<ExternalUrlService>();
			s_runtimeServices.RegisterService<DiamondRenderToTextureService>();
			s_runtimeServices.RegisterService<MessagePopupDisplay>();
			s_runtimeServices.RegisterService<ITouchScreenService>(new W8Touch());
		}
	}

	public static T Get<T>() where T : class, IService
	{
		ServiceLocator currentServiceLocator = GetCurrentServiceLocator();
		if (currentServiceLocator == null)
		{
			return null;
		}
		return currentServiceLocator.Get<T>();
	}

	public static bool IsAvailable<T>() where T : class, IService
	{
		ServiceLocator currentServiceLocator = GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			return currentServiceLocator.GetServiceState<T>() == ServiceLocator.ServiceState.Ready;
		}
		return false;
	}

	public static bool TryGet<T>(out T service) where T : class, IService
	{
		ServiceLocator currentServiceLocator = GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			return currentServiceLocator.TryGetService<T>(out service);
		}
		service = null;
		return false;
	}

	public static IJobDependency CreateServiceDependency(Type serviceType)
	{
		ServiceLocator currentServiceLocator = GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			return new ServiceDependency(serviceType, currentServiceLocator);
		}
		return null;
	}

	public static IJobDependency CreateServiceSoftDependency(Type serviceType)
	{
		ServiceLocator currentServiceLocator = GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			return new ServiceSoftDependency(serviceType, currentServiceLocator);
		}
		return null;
	}

	public static IJobDependency[] CreateServiceDependencies(params Type[] serviceTypes)
	{
		ServiceLocator currentServiceLocator = GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			IJobDependency[] array = new IJobDependency[serviceTypes.Length];
			for (int i = 0; i < serviceTypes.Length; i++)
			{
				array[i] = new ServiceDependency(serviceTypes[i], currentServiceLocator);
			}
			return array;
		}
		return null;
	}

	public static IJobDependency[] CreateServiceSoftDependencies(params Type[] serviceTypes)
	{
		ServiceLocator currentServiceLocator = GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			IJobDependency[] array = new IJobDependency[serviceTypes.Length];
			for (int i = 0; i < serviceTypes.Length; i++)
			{
				array[i] = new ServiceSoftDependency(serviceTypes[i], currentServiceLocator);
			}
			return array;
		}
		return null;
	}

	public static void InitializeRuntimeServices(Type[] serviceTypes = null)
	{
		if (s_runtimeServices == null)
		{
			InstantiateServiceLocator();
			if (serviceTypes == null)
			{
				RegisterDefaultRuntimeServices();
			}
			else
			{
				RegisterRuntimeServices(serviceTypes);
			}
			s_runtimeServices.SetupInitializationJobs();
			Processor.RegisterUpdateDelegate(s_runtimeServices.UpdateServices);
		}
		else
		{
			Log.Services.PrintWarning("[HearthstoneServices.InitializeRuntimeServices] Runtime services are already initialized!");
		}
	}

	public static void Shutdown()
	{
		if (s_serviceObjects != null)
		{
			for (int i = 0; i < s_serviceObjects.Count; i++)
			{
				if (s_serviceObjects[i] != null)
				{
					UnityEngine.Object.Destroy(s_serviceObjects[i]);
				}
			}
			s_serviceObjects.Clear();
		}
		ShutdownRuntimeServices();
		ShutdownDynamicServices();
	}

	public static ServiceLocator.ServiceState GetServiceState<T>() where T : class, IService
	{
		return GetCurrentServiceLocator()?.GetServiceState<T>() ?? ServiceLocator.ServiceState.Invalid;
	}

	public static ServiceLocator.ServiceState GetServiceState(Type serviceType)
	{
		return GetCurrentServiceLocator()?.GetServiceState(serviceType) ?? ServiceLocator.ServiceState.Invalid;
	}

	public static void ShutdownRuntimeServices()
	{
		if (s_runtimeServices != null)
		{
			s_runtimeServices.Shutdown();
			Processor.UnregisterUpdateDelegate(s_runtimeServices.UpdateServices);
			s_runtimeServices = null;
		}
	}

	public static bool InitializeDynamicServicesIfNeeded(params Type[] serviceTypes)
	{
		if (serviceTypes == null || serviceTypes.Length == 0)
		{
			return false;
		}
		bool flag = false;
		for (int i = 0; i < serviceTypes.Length; i++)
		{
			if (GetServiceState(serviceTypes[i]) == ServiceLocator.ServiceState.Invalid)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			return InitializeDynamicServices(ref s_dynamicServices, "DynamicServices", null, serviceTypes);
		}
		return false;
	}

	public static bool InitializeDynamicServicesIfNeeded(out IJobDependency[] serviceDependencies, params Type[] serviceTypes)
	{
		if (serviceTypes == null || serviceTypes.Length == 0)
		{
			serviceDependencies = null;
			return false;
		}
		bool result = InitializeDynamicServicesIfNeeded(serviceTypes);
		serviceDependencies = new IJobDependency[serviceTypes.Length];
		for (int i = 0; i < serviceTypes.Length; i++)
		{
			serviceDependencies[i] = CreateServiceDependency(serviceTypes[i]);
		}
		return result;
	}

	public static bool InitializeDynamicServicesIfEditor(out IJobDependency[] serviceDependencies, params Type[] serviceTypes)
	{
		serviceDependencies = CreateServiceDependencies(serviceTypes);
		return false;
	}

	public static void ShutdownDynamicServices()
	{
		ShutdownDynamicServiceLocator(ref s_dynamicServices);
	}

	public static bool InitializeTestServices(params Type[] serviceTypes)
	{
		return InitializeTestServices(null, serviceTypes);
	}

	public static bool InitializeTestServices(List<(Type, IService)> serviceOverrides, params Type[] serviceTypes)
	{
		if (serviceTypes == null || serviceTypes.Length == 0)
		{
			return false;
		}
		ShutdownTestServices();
		return InitializeDynamicServices(ref s_testServices, "TestServices", serviceOverrides, serviceTypes);
	}

	public static bool InitializeTestServices(out IJobDependency[] serviceDependencies, List<(Type, IService)> serviceOverrides = null, params Type[] serviceTypes)
	{
		if (serviceTypes == null || serviceTypes.Length == 0)
		{
			serviceDependencies = null;
			return false;
		}
		bool result = InitializeTestServices(serviceOverrides, serviceTypes);
		serviceDependencies = new IJobDependency[serviceTypes.Length];
		for (int i = 0; i < serviceTypes.Length; i++)
		{
			serviceDependencies[i] = CreateServiceDependency(serviceTypes[i]);
		}
		return result;
	}

	public static void ShutdownTestServices()
	{
		ShutdownDynamicServiceLocator(ref s_testServices);
	}

	private static void RegisterRuntimeServices(Type[] servicesTypes)
	{
		if (s_runtimeServices == null)
		{
			return;
		}
		IServiceFactory serviceFactory = HearthstoneServiceFactory.CreateServiceFactory();
		foreach (Type type in servicesTypes)
		{
			if (!serviceFactory.TryCreateService(type, out var service))
			{
				Log.Services.PrintError("[HearthstoneServices.RegisterRuntimeServices] Failed to create service of type ({0}).", type);
			}
			s_runtimeServices.RegisterService(type, service);
		}
	}

	private static void InstantiateServiceLocator()
	{
		if (s_runtimeServices == null)
		{
			s_runtimeServices = new ServiceLocator("Hearthstone Services", Processor.JobQueue);
			s_runtimeServices.SetLogger(Log.Services);
			s_runtimeServices.AddServiceLocatorStateChangedListener(OnServiceLocatorStateChanged);
		}
		else
		{
			Log.Services.PrintWarning("Service Locator already instantiated!");
		}
	}

	private static void OnServiceLocatorStateChanged(ServiceLocator.ServiceState state)
	{
		if (state == ServiceLocator.ServiceState.Ready)
		{
			HearthstonePerformance.Get()?.CaptureAppInitializedTime();
			s_runtimeServices.RemoveServiceLocatorStateChangedListener(OnServiceLocatorStateChanged);
		}
	}

	private static T CreateMonobehaviourService<T>() where T : MonoBehaviour, IService
	{
		if (s_serviceObjects == null)
		{
			s_serviceObjects = new List<GameObject>();
		}
		GameObject gameObject = new GameObject(typeof(T).Name, typeof(HSDontDestroyOnLoad));
		s_serviceObjects.Add(gameObject);
		return gameObject.AddComponent<T>();
	}

	private static ILoginService CreateLoginService()
	{
		return new LoginService(Log.Login);
	}

	private static ServiceLocator GetCurrentServiceLocator()
	{
		if (s_runtimeServices != null)
		{
			return s_runtimeServices;
		}
		return s_dynamicServices?.Services;
	}

	private static bool InitializeDynamicServices(ref DynamicServiceLocator dynamicServiceLocator, string id, List<(Type, IService)> serviceOverrides, params Type[] serviceTypes)
	{
		if (serviceTypes == null || serviceTypes.Length == 0)
		{
			return false;
		}
		if (dynamicServiceLocator == null)
		{
			dynamicServiceLocator = DynamicServiceLocator.Create(id, HearthstoneServiceFactory.CreateServiceFactory(), Log.Services, Processor.JobQueue);
			Processor.RegisterUpdateDelegate(dynamicServiceLocator.Services.UpdateServices);
		}
		return dynamicServiceLocator.InitializeServices(new DynamicServiceLocator.InitializationArgs
		{
			ServiceTypes = serviceTypes,
			Overrides = serviceOverrides
		});
	}

	private static bool InitializeDynamicServices(ref DynamicServiceLocator dynamicServiceLocator, string id, out IJobDependency[] serviceDependencies, List<(Type, IService)> serviceOverrides, params Type[] serviceTypes)
	{
		if (InitializeDynamicServices(ref dynamicServiceLocator, id, serviceOverrides, serviceTypes))
		{
			serviceDependencies = new IJobDependency[serviceTypes.Length];
			for (int i = 0; i < serviceTypes.Length; i++)
			{
				serviceDependencies[i] = new ServiceDependency(serviceTypes[i], dynamicServiceLocator.Services);
			}
			return true;
		}
		serviceDependencies = null;
		return false;
	}

	private static void ShutdownDynamicServiceLocator(ref DynamicServiceLocator dynamicServiceLocator)
	{
		if (dynamicServiceLocator != null)
		{
			Processor.UnregisterUpdateDelegate(dynamicServiceLocator.Services.UpdateServices);
			dynamicServiceLocator.Shutdown();
			dynamicServiceLocator = null;
		}
	}
}
