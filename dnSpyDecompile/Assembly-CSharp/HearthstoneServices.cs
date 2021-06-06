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

// Token: 0x0200069A RID: 1690
public static class HearthstoneServices
{
	// Token: 0x06005E57 RID: 24151 RVA: 0x001EA8E8 File Offset: 0x001E8AE8
	private static void RegisterDefaultRuntimeServices()
	{
		if (HearthstoneServices.s_runtimeServices == null)
		{
			return;
		}
		HearthstoneServices.s_runtimeServices.RegisterService<GameDownloadManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<Network>();
		HearthstoneServices.s_runtimeServices.RegisterService<DownloadableDbfCache>();
		HearthstoneServices.s_runtimeServices.RegisterService<SpecialEventManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<GameMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<DraftManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<ChangedCardMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<AdventureProgressMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<AchieveManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<AchievementManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<QuestManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<RewardTrackManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<GenericRewardChestNoticeManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<AccountLicenseMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<FixedRewardsMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<ReturningPlayerMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<DemoMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<NetCache>();
		HearthstoneServices.s_runtimeServices.RegisterService<GameDbf>();
		HearthstoneServices.s_runtimeServices.RegisterService<DebugConsole>();
		HearthstoneServices.s_runtimeServices.RegisterService<TavernBrawlManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<IAssetLoader>(new AssetLoader());
		HearthstoneServices.s_runtimeServices.RegisterService<IAliasedAssetResolver>(new AliasedAssetResolver());
		HearthstoneServices.s_runtimeServices.RegisterService<LoginManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<CardBackManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<CheatMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<Cheats>();
		HearthstoneServices.s_runtimeServices.RegisterService<ReconnectMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<DisconnectMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<HealthyGamingMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<InGameBrowserManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<SoundManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<MusicManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<RAFManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<InactivePlayerKicker>();
		HearthstoneServices.s_runtimeServices.RegisterService<ClientLocationManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<AdTrackingManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<SpellCache>();
		HearthstoneServices.s_runtimeServices.RegisterService<LocationServices>();
		HearthstoneServices.s_runtimeServices.RegisterService<WifiInfo>();
		HearthstoneServices.s_runtimeServices.RegisterService<FiresideGatheringManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<GameplayErrorManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<FontTable>();
		HearthstoneServices.s_runtimeServices.RegisterService<UniversalInputManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<ScreenEffectsMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<ShownUIMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<PerformanceAnalytics>();
		HearthstoneServices.s_runtimeServices.RegisterService<PopupDisplayManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<GraphicsManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<MobilePermissionsManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<ShaderTime>();
		HearthstoneServices.s_runtimeServices.RegisterService<MobileCallbackManager>(HearthstoneServices.CreateMonobehaviourService<MobileCallbackManager>());
		HearthstoneServices.s_runtimeServices.RegisterService<FullScreenFXMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<SceneMgr>();
		HearthstoneServices.s_runtimeServices.RegisterService<SetRotationManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<Cinematic>();
		HearthstoneServices.s_runtimeServices.RegisterService<WidgetRunner>();
		HearthstoneServices.s_runtimeServices.RegisterService<HearthstoneCheckout>();
		HearthstoneServices.s_runtimeServices.RegisterService<NetworkReachabilityManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<VersionConfigurationService>();
		HearthstoneServices.s_runtimeServices.RegisterService<DeeplinkService>();
		HearthstoneServices.s_runtimeServices.RegisterService<ILoginService>(HearthstoneServices.CreateLoginService());
		HearthstoneServices.s_runtimeServices.RegisterService<PartyManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<PlayerMigrationManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<CoinManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<QuestToastManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<RewardXpNotificationManager>();
		HearthstoneServices.s_runtimeServices.RegisterService<MaterialManagerService>();
		HearthstoneServices.s_runtimeServices.RegisterService<InGameMessageScheduler>();
		HearthstoneServices.s_runtimeServices.RegisterService<DisposablesCleaner>();
		HearthstoneServices.s_runtimeServices.RegisterService<ExternalUrlService>();
		HearthstoneServices.s_runtimeServices.RegisterService<DiamondRenderToTextureService>();
		HearthstoneServices.s_runtimeServices.RegisterService<MessagePopupDisplay>();
		HearthstoneServices.s_runtimeServices.RegisterService<ITouchScreenService>(new W8Touch());
	}

	// Token: 0x06005E58 RID: 24152 RVA: 0x001EAC40 File Offset: 0x001E8E40
	public static T Get<T>() where T : class, IService
	{
		ServiceLocator currentServiceLocator = HearthstoneServices.GetCurrentServiceLocator();
		if (currentServiceLocator == null)
		{
			return default(T);
		}
		return currentServiceLocator.Get<T>();
	}

	// Token: 0x06005E59 RID: 24153 RVA: 0x001EAC68 File Offset: 0x001E8E68
	public static bool IsAvailable<T>() where T : class, IService
	{
		ServiceLocator currentServiceLocator = HearthstoneServices.GetCurrentServiceLocator();
		return currentServiceLocator != null && currentServiceLocator.GetServiceState<T>() == ServiceLocator.ServiceState.Ready;
	}

	// Token: 0x06005E5A RID: 24154 RVA: 0x001EAC8C File Offset: 0x001E8E8C
	public static bool TryGet<T>(out T service) where T : class, IService
	{
		ServiceLocator currentServiceLocator = HearthstoneServices.GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			return currentServiceLocator.TryGetService<T>(out service);
		}
		service = default(T);
		return false;
	}

	// Token: 0x06005E5B RID: 24155 RVA: 0x001EACB4 File Offset: 0x001E8EB4
	public static IJobDependency CreateServiceDependency(Type serviceType)
	{
		ServiceLocator currentServiceLocator = HearthstoneServices.GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			return new ServiceDependency(serviceType, currentServiceLocator);
		}
		return null;
	}

	// Token: 0x06005E5C RID: 24156 RVA: 0x001EACD4 File Offset: 0x001E8ED4
	public static IJobDependency CreateServiceSoftDependency(Type serviceType)
	{
		ServiceLocator currentServiceLocator = HearthstoneServices.GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			return new ServiceSoftDependency(serviceType, currentServiceLocator);
		}
		return null;
	}

	// Token: 0x06005E5D RID: 24157 RVA: 0x001EACF4 File Offset: 0x001E8EF4
	public static IJobDependency[] CreateServiceDependencies(params Type[] serviceTypes)
	{
		ServiceLocator currentServiceLocator = HearthstoneServices.GetCurrentServiceLocator();
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

	// Token: 0x06005E5E RID: 24158 RVA: 0x001EAD30 File Offset: 0x001E8F30
	public static IJobDependency[] CreateServiceSoftDependencies(params Type[] serviceTypes)
	{
		ServiceLocator currentServiceLocator = HearthstoneServices.GetCurrentServiceLocator();
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

	// Token: 0x06005E5F RID: 24159 RVA: 0x001EAD6C File Offset: 0x001E8F6C
	public static void InitializeRuntimeServices(Type[] serviceTypes = null)
	{
		if (HearthstoneServices.s_runtimeServices == null)
		{
			HearthstoneServices.InstantiateServiceLocator();
			if (serviceTypes == null)
			{
				HearthstoneServices.RegisterDefaultRuntimeServices();
			}
			else
			{
				HearthstoneServices.RegisterRuntimeServices(serviceTypes);
			}
			HearthstoneServices.s_runtimeServices.SetupInitializationJobs();
			Processor.RegisterUpdateDelegate(new Action(HearthstoneServices.s_runtimeServices.UpdateServices));
			return;
		}
		Log.Services.PrintWarning("[HearthstoneServices.InitializeRuntimeServices] Runtime services are already initialized!", Array.Empty<object>());
	}

	// Token: 0x06005E60 RID: 24160 RVA: 0x001EADCC File Offset: 0x001E8FCC
	public static void Shutdown()
	{
		if (HearthstoneServices.s_serviceObjects != null)
		{
			for (int i = 0; i < HearthstoneServices.s_serviceObjects.Count; i++)
			{
				if (HearthstoneServices.s_serviceObjects[i] != null)
				{
					UnityEngine.Object.Destroy(HearthstoneServices.s_serviceObjects[i]);
				}
			}
			HearthstoneServices.s_serviceObjects.Clear();
		}
		HearthstoneServices.ShutdownRuntimeServices();
		HearthstoneServices.ShutdownDynamicServices();
	}

	// Token: 0x06005E61 RID: 24161 RVA: 0x001EAE2C File Offset: 0x001E902C
	public static ServiceLocator.ServiceState GetServiceState<T>() where T : class, IService
	{
		ServiceLocator currentServiceLocator = HearthstoneServices.GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			return currentServiceLocator.GetServiceState<T>();
		}
		return ServiceLocator.ServiceState.Invalid;
	}

	// Token: 0x06005E62 RID: 24162 RVA: 0x001EAE4C File Offset: 0x001E904C
	public static ServiceLocator.ServiceState GetServiceState(Type serviceType)
	{
		ServiceLocator currentServiceLocator = HearthstoneServices.GetCurrentServiceLocator();
		if (currentServiceLocator != null)
		{
			return currentServiceLocator.GetServiceState(serviceType);
		}
		return ServiceLocator.ServiceState.Invalid;
	}

	// Token: 0x06005E63 RID: 24163 RVA: 0x001EAE6B File Offset: 0x001E906B
	public static void ShutdownRuntimeServices()
	{
		if (HearthstoneServices.s_runtimeServices != null)
		{
			HearthstoneServices.s_runtimeServices.Shutdown();
			Processor.UnregisterUpdateDelegate(new Action(HearthstoneServices.s_runtimeServices.UpdateServices));
			HearthstoneServices.s_runtimeServices = null;
		}
	}

	// Token: 0x06005E64 RID: 24164 RVA: 0x001EAE9C File Offset: 0x001E909C
	public static bool InitializeDynamicServicesIfNeeded(params Type[] serviceTypes)
	{
		if (serviceTypes == null || serviceTypes.Length == 0)
		{
			return false;
		}
		bool flag = false;
		for (int i = 0; i < serviceTypes.Length; i++)
		{
			if (HearthstoneServices.GetServiceState(serviceTypes[i]) == ServiceLocator.ServiceState.Invalid)
			{
				flag = true;
				break;
			}
		}
		return flag && HearthstoneServices.InitializeDynamicServices(ref HearthstoneServices.s_dynamicServices, "DynamicServices", null, serviceTypes);
	}

	// Token: 0x06005E65 RID: 24165 RVA: 0x001EAEE8 File Offset: 0x001E90E8
	public static bool InitializeDynamicServicesIfNeeded(out IJobDependency[] serviceDependencies, params Type[] serviceTypes)
	{
		if (serviceTypes == null || serviceTypes.Length == 0)
		{
			serviceDependencies = null;
			return false;
		}
		bool result = HearthstoneServices.InitializeDynamicServicesIfNeeded(serviceTypes);
		serviceDependencies = new IJobDependency[serviceTypes.Length];
		for (int i = 0; i < serviceTypes.Length; i++)
		{
			serviceDependencies[i] = HearthstoneServices.CreateServiceDependency(serviceTypes[i]);
		}
		return result;
	}

	// Token: 0x06005E66 RID: 24166 RVA: 0x001EAF2D File Offset: 0x001E912D
	public static bool InitializeDynamicServicesIfEditor(out IJobDependency[] serviceDependencies, params Type[] serviceTypes)
	{
		serviceDependencies = HearthstoneServices.CreateServiceDependencies(serviceTypes);
		return false;
	}

	// Token: 0x06005E67 RID: 24167 RVA: 0x001EAF38 File Offset: 0x001E9138
	public static void ShutdownDynamicServices()
	{
		HearthstoneServices.ShutdownDynamicServiceLocator(ref HearthstoneServices.s_dynamicServices);
	}

	// Token: 0x06005E68 RID: 24168 RVA: 0x001EAF44 File Offset: 0x001E9144
	public static bool InitializeTestServices(params Type[] serviceTypes)
	{
		return HearthstoneServices.InitializeTestServices(null, serviceTypes);
	}

	// Token: 0x06005E69 RID: 24169 RVA: 0x001EAF4D File Offset: 0x001E914D
	public static bool InitializeTestServices(List<ValueTuple<Type, IService>> serviceOverrides, params Type[] serviceTypes)
	{
		if (serviceTypes == null || serviceTypes.Length == 0)
		{
			return false;
		}
		HearthstoneServices.ShutdownTestServices();
		return HearthstoneServices.InitializeDynamicServices(ref HearthstoneServices.s_testServices, "TestServices", serviceOverrides, serviceTypes);
	}

	// Token: 0x06005E6A RID: 24170 RVA: 0x001EAF70 File Offset: 0x001E9170
	public static bool InitializeTestServices(out IJobDependency[] serviceDependencies, List<ValueTuple<Type, IService>> serviceOverrides = null, params Type[] serviceTypes)
	{
		if (serviceTypes == null || serviceTypes.Length == 0)
		{
			serviceDependencies = null;
			return false;
		}
		bool result = HearthstoneServices.InitializeTestServices(serviceOverrides, serviceTypes);
		serviceDependencies = new IJobDependency[serviceTypes.Length];
		for (int i = 0; i < serviceTypes.Length; i++)
		{
			serviceDependencies[i] = HearthstoneServices.CreateServiceDependency(serviceTypes[i]);
		}
		return result;
	}

	// Token: 0x06005E6B RID: 24171 RVA: 0x001EAFB6 File Offset: 0x001E91B6
	public static void ShutdownTestServices()
	{
		HearthstoneServices.ShutdownDynamicServiceLocator(ref HearthstoneServices.s_testServices);
	}

	// Token: 0x06005E6C RID: 24172 RVA: 0x001EAFC4 File Offset: 0x001E91C4
	private static void RegisterRuntimeServices(Type[] servicesTypes)
	{
		if (HearthstoneServices.s_runtimeServices == null)
		{
			return;
		}
		IServiceFactory serviceFactory = HearthstoneServiceFactory.CreateServiceFactory();
		foreach (Type type in servicesTypes)
		{
			IService service;
			if (!serviceFactory.TryCreateService(type, out service))
			{
				Log.Services.PrintError("[HearthstoneServices.RegisterRuntimeServices] Failed to create service of type ({0}).", new object[]
				{
					type
				});
			}
			HearthstoneServices.s_runtimeServices.RegisterService(type, service);
		}
	}

	// Token: 0x06005E6D RID: 24173 RVA: 0x001EB024 File Offset: 0x001E9224
	private static void InstantiateServiceLocator()
	{
		if (HearthstoneServices.s_runtimeServices == null)
		{
			HearthstoneServices.s_runtimeServices = new ServiceLocator("Hearthstone Services", Processor.JobQueue);
			HearthstoneServices.s_runtimeServices.SetLogger(Log.Services);
			HearthstoneServices.s_runtimeServices.AddServiceLocatorStateChangedListener(new ServiceLocator.ServiceLocatorStateChangedListener(HearthstoneServices.OnServiceLocatorStateChanged));
			return;
		}
		Log.Services.PrintWarning("Service Locator already instantiated!", Array.Empty<object>());
	}

	// Token: 0x06005E6E RID: 24174 RVA: 0x001EB088 File Offset: 0x001E9288
	private static void OnServiceLocatorStateChanged(ServiceLocator.ServiceState state)
	{
		if (state == ServiceLocator.ServiceState.Ready)
		{
			HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
			if (hearthstonePerformance != null)
			{
				hearthstonePerformance.CaptureAppInitializedTime();
			}
			HearthstoneServices.s_runtimeServices.RemoveServiceLocatorStateChangedListener(new ServiceLocator.ServiceLocatorStateChangedListener(HearthstoneServices.OnServiceLocatorStateChanged));
		}
	}

	// Token: 0x06005E6F RID: 24175 RVA: 0x001EB0C0 File Offset: 0x001E92C0
	private static T CreateMonobehaviourService<T>() where T : MonoBehaviour, IService
	{
		if (HearthstoneServices.s_serviceObjects == null)
		{
			HearthstoneServices.s_serviceObjects = new List<GameObject>();
		}
		GameObject gameObject = new GameObject(typeof(T).Name, new Type[]
		{
			typeof(HSDontDestroyOnLoad)
		});
		HearthstoneServices.s_serviceObjects.Add(gameObject);
		return gameObject.AddComponent<T>();
	}

	// Token: 0x06005E70 RID: 24176 RVA: 0x001EB117 File Offset: 0x001E9317
	private static ILoginService CreateLoginService()
	{
		return new LoginService(Log.Login);
	}

	// Token: 0x06005E71 RID: 24177 RVA: 0x001EB123 File Offset: 0x001E9323
	private static ServiceLocator GetCurrentServiceLocator()
	{
		if (HearthstoneServices.s_runtimeServices != null)
		{
			return HearthstoneServices.s_runtimeServices;
		}
		DynamicServiceLocator dynamicServiceLocator = HearthstoneServices.s_dynamicServices;
		if (dynamicServiceLocator == null)
		{
			return null;
		}
		return dynamicServiceLocator.Services;
	}

	// Token: 0x06005E72 RID: 24178 RVA: 0x001EB144 File Offset: 0x001E9344
	private static bool InitializeDynamicServices(ref DynamicServiceLocator dynamicServiceLocator, string id, List<ValueTuple<Type, IService>> serviceOverrides, params Type[] serviceTypes)
	{
		if (serviceTypes == null || serviceTypes.Length == 0)
		{
			return false;
		}
		if (dynamicServiceLocator == null)
		{
			dynamicServiceLocator = DynamicServiceLocator.Create(id, HearthstoneServiceFactory.CreateServiceFactory(), Log.Services, Processor.JobQueue);
			Processor.RegisterUpdateDelegate(new Action(dynamicServiceLocator.Services.UpdateServices));
		}
		return dynamicServiceLocator.InitializeServices(new DynamicServiceLocator.InitializationArgs
		{
			ServiceTypes = serviceTypes,
			Overrides = serviceOverrides
		});
	}

	// Token: 0x06005E73 RID: 24179 RVA: 0x001EB1AC File Offset: 0x001E93AC
	private static bool InitializeDynamicServices(ref DynamicServiceLocator dynamicServiceLocator, string id, out IJobDependency[] serviceDependencies, List<ValueTuple<Type, IService>> serviceOverrides, params Type[] serviceTypes)
	{
		if (HearthstoneServices.InitializeDynamicServices(ref dynamicServiceLocator, id, serviceOverrides, serviceTypes))
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

	// Token: 0x06005E74 RID: 24180 RVA: 0x001EB1F9 File Offset: 0x001E93F9
	private static void ShutdownDynamicServiceLocator(ref DynamicServiceLocator dynamicServiceLocator)
	{
		if (dynamicServiceLocator != null)
		{
			Processor.UnregisterUpdateDelegate(new Action(dynamicServiceLocator.Services.UpdateServices));
			dynamicServiceLocator.Shutdown();
			dynamicServiceLocator = null;
		}
	}

	// Token: 0x04004F88 RID: 20360
	private static ServiceLocator s_runtimeServices;

	// Token: 0x04004F89 RID: 20361
	private static DynamicServiceLocator s_dynamicServices;

	// Token: 0x04004F8A RID: 20362
	private static DynamicServiceLocator s_testServices;

	// Token: 0x04004F8B RID: 20363
	private static List<GameObject> s_serviceObjects;
}
