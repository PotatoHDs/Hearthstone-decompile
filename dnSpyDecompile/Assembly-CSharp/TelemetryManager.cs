using System;
using System.Collections;
using System.Collections.Generic;
using bgs;
using Blizzard.Telemetry;
using Hearthstone.Core;
using Hearthstone.Telemetry;
using HearthstoneTelemetry;
using UnityEngine;

// Token: 0x0200092F RID: 2351
public class TelemetryManager
{
	// Token: 0x1700075E RID: 1886
	// (get) Token: 0x060081C2 RID: 33218 RVA: 0x002A374A File Offset: 0x002A194A
	// (set) Token: 0x060081C3 RID: 33219 RVA: 0x002A3751 File Offset: 0x002A1951
	public static TelemetryManagerComponentNetwork NetworkComponent { get; private set; } = new TelemetryManagerComponentNetwork();

	// Token: 0x1700075F RID: 1887
	// (get) Token: 0x060081C4 RID: 33220 RVA: 0x002A375C File Offset: 0x002A195C
	public static string ProgramId
	{
		get
		{
			if (TelemetryManager.s_instance.m_telemetryService != null && TelemetryManager.s_instance.m_telemetryService.Context != null && TelemetryManager.s_instance.m_telemetryService.Context.Program != null && TelemetryManager.s_instance.m_telemetryService.Context.Program.Id != null)
			{
				return TelemetryManager.s_instance.m_telemetryService.Context.Program.Id;
			}
			return string.Empty;
		}
	}

	// Token: 0x17000760 RID: 1888
	// (get) Token: 0x060081C5 RID: 33221 RVA: 0x002A37D8 File Offset: 0x002A19D8
	public static string ProgramName
	{
		get
		{
			if (TelemetryManager.s_instance.m_telemetryService != null && TelemetryManager.s_instance.m_telemetryService.Context != null && TelemetryManager.s_instance.m_telemetryService.Context.Program != null && TelemetryManager.s_instance.m_telemetryService.Context.Program.Name != null)
			{
				return TelemetryManager.s_instance.m_telemetryService.Context.Program.Name;
			}
			return string.Empty;
		}
	}

	// Token: 0x17000761 RID: 1889
	// (get) Token: 0x060081C6 RID: 33222 RVA: 0x002A3854 File Offset: 0x002A1A54
	public static string ProgramVersion
	{
		get
		{
			if (TelemetryManager.s_instance.m_telemetryService != null && TelemetryManager.s_instance.m_telemetryService.Context != null && TelemetryManager.s_instance.m_telemetryService.Context.Program != null && TelemetryManager.s_instance.m_telemetryService.Context.Program.Version != null)
			{
				return TelemetryManager.s_instance.m_telemetryService.Context.Program.Version;
			}
			return string.Empty;
		}
	}

	// Token: 0x17000762 RID: 1890
	// (get) Token: 0x060081C7 RID: 33223 RVA: 0x002A38D0 File Offset: 0x002A1AD0
	public static string SessionId
	{
		get
		{
			if (TelemetryManager.s_instance.m_telemetryService != null && TelemetryManager.s_instance.m_telemetryService.Context != null && TelemetryManager.s_instance.m_telemetryService.Context.SessionId != null)
			{
				return TelemetryManager.s_instance.m_telemetryService.Context.SessionId;
			}
			return string.Empty;
		}
	}

	// Token: 0x060081C9 RID: 33225 RVA: 0x002A396A File Offset: 0x002A1B6A
	private TelemetryManager()
	{
	}

	// Token: 0x060081CA RID: 33226 RVA: 0x002A399E File Offset: 0x002A1B9E
	public static ITelemetryClient Client()
	{
		return TelemetryManager.s_instance.m_telemetryClient;
	}

	// Token: 0x060081CB RID: 33227 RVA: 0x002A39AC File Offset: 0x002A1BAC
	public static void RegisterMessageSentCallback(long messageId, Action<long> sentMessageCallback)
	{
		if (TelemetryManager.s_instance.m_messagesWaitingForCallback.ContainsKey(messageId))
		{
			if (TelemetryManager.s_instance.m_messagesWaitingForCallback[messageId] == null)
			{
				TelemetryManager.s_instance.m_messagesWaitingForCallback[messageId] = new List<Action<long>>();
			}
			TelemetryManager.s_instance.m_messagesWaitingForCallback[messageId].Add(sentMessageCallback);
			return;
		}
		TelemetryManager.s_instance.m_messagesWaitingForCallback.Add(messageId, new List<Action<long>>
		{
			sentMessageCallback
		});
	}

	// Token: 0x060081CC RID: 33228 RVA: 0x002A3A28 File Offset: 0x002A1C28
	public static void RegisterShutdownListener(Action handler)
	{
		object listenerLock = TelemetryManager.s_instance.m_listenerLock;
		lock (listenerLock)
		{
			if (!TelemetryManager.s_instance.m_shutdownListeners.Contains(handler))
			{
				TelemetryManager.s_instance.m_shutdownListeners.Add(handler);
			}
		}
	}

	// Token: 0x060081CD RID: 33229 RVA: 0x002A3A8C File Offset: 0x002A1C8C
	public static void UnregisterShutdownListener(Action handler)
	{
		object listenerLock = TelemetryManager.s_instance.m_listenerLock;
		lock (listenerLock)
		{
			TelemetryManager.s_instance.m_shutdownListeners.Remove(handler);
		}
	}

	// Token: 0x060081CE RID: 33230 RVA: 0x002A3ADC File Offset: 0x002A1CDC
	public static string GetApplicationId()
	{
		if (TelemetryManager.s_instance.m_contextData != null)
		{
			return TelemetryManager.s_instance.m_contextData.ApplicationID;
		}
		return string.Empty;
	}

	// Token: 0x060081CF RID: 33231 RVA: 0x002A3AFF File Offset: 0x002A1CFF
	public static void RebuildContext()
	{
		TelemetryManager.s_instance.SetServiceContext();
	}

	// Token: 0x060081D0 RID: 33232 RVA: 0x002A3B0B File Offset: 0x002A1D0B
	public static void Flush()
	{
		TelemetryManager.s_instance.m_telemetryService.Flush();
	}

	// Token: 0x060081D1 RID: 33233 RVA: 0x002A3B1C File Offset: 0x002A1D1C
	public static void Reset()
	{
		if (!TelemetryManager.s_instance.m_telemetryClient.IsInitialized())
		{
			return;
		}
		TelemetryManager.s_instance.m_auroraConnected = false;
		PresenceMgr.Get().ResetTelemetry();
		TelemetryManager.s_instance.m_telemetryService.Stop(new TimeSpan(0, 0, 0, 0, 3000), null);
		TelemetryManager.s_instance.SetTelemetryServiceData();
		Processor.RunCoroutine(TelemetryManager.s_instance.m_telemetryService.Run(null), null);
		Processor.RunCoroutine(TelemetryManager.s_instance.SetPushSdkTelemetryInfo(), null);
	}

	// Token: 0x060081D2 RID: 33234 RVA: 0x002A3BA9 File Offset: 0x002A1DA9
	private IEnumerator SetPushSdkTelemetryInfo()
	{
		yield return new WaitUntil(() => TelemetryManager.s_instance.m_telemetryService.Running);
		MobileCallbackManager.SetTelemetryInfo(TelemetryManager.ProgramId, TelemetryManager.ProgramName, TelemetryManager.ProgramVersion, TelemetryManager.SessionId);
		yield break;
	}

	// Token: 0x060081D3 RID: 33235 RVA: 0x002A3BB4 File Offset: 0x002A1DB4
	public static void OnBattleNetConnect(string host, int port, BattleNetErrors error)
	{
		if (error != BattleNetErrors.ERROR_OK)
		{
			TelemetryManager.s_instance.m_telemetryClient.SendConnectFail("AURORA", error.ToString(), host, new uint?((uint)port));
			return;
		}
		TelemetryManager.s_instance.m_auroraConnected = true;
		TelemetryManager.s_instance.m_telemetryClient.SendConnectSuccess("AURORA", host, new uint?((uint)port));
		TelemetryManager.RegisterShutdownListener(delegate
		{
			TelemetryManager.OnBattleNetDisconnect(host, port, BattleNetErrors.ERROR_OK);
		});
	}

	// Token: 0x060081D4 RID: 33236 RVA: 0x002A3C4C File Offset: 0x002A1E4C
	public static void OnBattleNetDisconnect(string host, int port, BattleNetErrors error)
	{
		if (!TelemetryManager.s_instance.m_auroraConnected)
		{
			return;
		}
		TelemetryManager.s_instance.m_auroraConnected = false;
		TelemetryManager.s_instance.m_telemetryClient.SendDisconnect("AURORA", TelemetryUtil.GetReasonFromBnetError(error), (error == BattleNetErrors.ERROR_OK) ? null : error.ToString(), null, null);
	}

	// Token: 0x060081D5 RID: 33237 RVA: 0x002A3CA8 File Offset: 0x002A1EA8
	public static void Shutdown()
	{
		if (!TelemetryManager.s_instance.m_telemetryClient.IsInitialized())
		{
			return;
		}
		global::Log.Telemetry.Print("Shutting down telemetry", Array.Empty<object>());
		foreach (ITelemetryManagerComponent telemetryManagerComponent in TelemetryManager.s_components)
		{
			telemetryManagerComponent.Shutdown();
		}
		Processor.UnregisterUpdateDelegate(new Action(TelemetryManager.s_instance.OnUpdate));
		TelemetryManager.ProcessShutdownListeners();
		TelemetryManager.s_instance.m_telemetryService.Stop(new TimeSpan(0, 0, 0, 0, 3000), null);
		TelemetryManager.s_instance.m_telemetryClient.Shutdown();
	}

	// Token: 0x060081D6 RID: 33238 RVA: 0x002A3D70 File Offset: 0x002A1F70
	public static void Initialize()
	{
		if (!Vars.Key("Telemetry.Enabled").GetBool(true))
		{
			return;
		}
		TelemetryManager.s_instance.SetTelemetryServiceData();
		global::Log.Telemetry.Print("Sending telemetry messages to TDK instance: {0}, SSL={1} IngestPort={2}", new object[]
		{
			TelemetryManager.s_instance.m_contextData.IngestUri.AbsoluteUri,
			TelemetryManager.s_instance.m_contextData.IngestUri.Scheme == "https",
			TelemetryManager.s_instance.m_contextData.IngestUri.Port
		});
		Processor.RunCoroutine(TelemetryManager.s_instance.m_telemetryService.Run(null), null);
		Processor.RunCoroutine(TelemetryManager.s_instance.SetPushSdkTelemetryInfo(), null);
		foreach (ITelemetryManagerComponent telemetryManagerComponent in TelemetryManager.s_components)
		{
			telemetryManagerComponent.Initialize();
		}
		Processor.RegisterUpdateDelegate(new Action(TelemetryManager.s_instance.OnUpdate));
	}

	// Token: 0x060081D7 RID: 33239 RVA: 0x002A3E88 File Offset: 0x002A2088
	private void SetTelemetryServiceData()
	{
		TelemetryManager.s_instance.m_contextData = new StandaloneContext();
		ServiceOptions serviceOptions = new ServiceOptions(this.m_contextData.ProgramId);
		serviceOptions.IngestBaseUrl = this.m_contextData.IngestUri.AbsoluteUri;
		serviceOptions.SendStartAndFinishMessages = true;
		serviceOptions.MaxQueueSize = 1500;
		serviceOptions.MaxBatchSize = 10;
		serviceOptions.OnMessageSent = new MessageCallback(this.TelemetryMessageSent);
		if (Vars.Key("Telemetry.LogEnabled").GetBool(false))
		{
			Blizzard.Telemetry.Log.Logger = new TelemetryLogWrapper();
			serviceOptions.LogEnqueue = true;
			serviceOptions.LogMessageRequest = true;
		}
		this.m_telemetryService = new Service(serviceOptions);
		this.SetServiceContext();
		this.m_telemetryClient.Initialize(this.m_telemetryService);
	}

	// Token: 0x060081D8 RID: 33240 RVA: 0x002A3F44 File Offset: 0x002A2144
	private AndroidContext CreateAndroidContext()
	{
		AndroidStore androidStore = AndroidDeviceSettings.Get().GetAndroidStore();
		if (androidStore == AndroidStore.HUAWEI)
		{
			return new AndroidHuaweiContext();
		}
		if (androidStore != AndroidStore.ONE_STORE)
		{
			return new AndroidContext();
		}
		return new AndroidOneStoreContext();
	}

	// Token: 0x060081D9 RID: 33241 RVA: 0x002A3F78 File Offset: 0x002A2178
	private void SetServiceContext()
	{
		List<string> list = new List<string>();
		if (this.m_contextData.ConnectionType == TelemetryConnectionType.Internal)
		{
			list.Add("dev");
			global::Log.Telemetry.Print("Sending telemetry to production endpoint. Messages will be tagged as dev", Array.Empty<object>());
		}
		else
		{
			list.Add("prod");
			global::Log.Telemetry.Print("Sending telemetry to production endpoint. Messages will be tagged as prod", Array.Empty<object>());
		}
		Service telemetryService = this.m_telemetryService;
		Context context = new Context();
		context.Account = BnetUtils.TryGetGameAccountId();
		context.BnetId = BnetUtils.TryGetBnetAccountId();
		Context.LocationInfo locationInfo = new Context.LocationInfo();
		constants.BnetRegion? bnetRegion = BnetUtils.TryGetGameRegion();
		locationInfo.BnetRegion = ((bnetRegion != null) ? new int?((int)bnetRegion.GetValueOrDefault()) : null);
		context.GameLocation = locationInfo;
		context.Program = new Context.ProgramInfo
		{
			Id = TelemetryManager.s_instance.m_contextData.ProgramId,
			Name = TelemetryManager.s_instance.m_contextData.ProgramName,
			Version = TelemetryManager.s_instance.m_contextData.ProgramVersion
		};
		context.PlayerLocation = new Context.LocationInfo
		{
			BnetRegion = TelemetryManager.s_instance.m_contextData.BattleNetRegion
		};
		context.Host = new Context.HostInfo
		{
			Tag = list
		};
		telemetryService.UserContext = context;
	}

	// Token: 0x060081DA RID: 33242 RVA: 0x002A40B4 File Offset: 0x002A22B4
	private void OnUpdate()
	{
		if (!this.m_telemetryClient.IsInitialized())
		{
			return;
		}
		foreach (ITelemetryManagerComponent telemetryManagerComponent in TelemetryManager.s_components)
		{
			telemetryManagerComponent.Update();
		}
		this.m_telemetryClient.OnUpdate();
	}

	// Token: 0x060081DB RID: 33243 RVA: 0x002A411C File Offset: 0x002A231C
	private void TelemetryMessageSent(long messageId)
	{
		if (!this.m_messagesWaitingForCallback.ContainsKey(messageId))
		{
			return;
		}
		foreach (Action<long> action in this.m_messagesWaitingForCallback[messageId])
		{
			if (action != null)
			{
				action(messageId);
			}
		}
		this.m_messagesWaitingForCallback.Remove(messageId);
	}

	// Token: 0x060081DC RID: 33244 RVA: 0x002A4198 File Offset: 0x002A2398
	private static void ProcessShutdownListeners()
	{
		if (TelemetryManager.s_instance.m_shutdownListeners.Count == 0)
		{
			return;
		}
		object listenerLock = TelemetryManager.s_instance.m_listenerLock;
		Action[] array;
		lock (listenerLock)
		{
			array = TelemetryManager.s_instance.m_shutdownListeners.ToArray();
			TelemetryManager.s_instance.m_shutdownListeners.Clear();
		}
		global::Log.Telemetry.Print("Processing {0} shutdown listeners", new object[]
		{
			array.Length
		});
		Action[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i]();
		}
	}

	// Token: 0x04006CE4 RID: 27876
	private const int FLUSH_TIMEOUT = 3000;

	// Token: 0x04006CE5 RID: 27877
	private const string InternalTag = "dev";

	// Token: 0x04006CE6 RID: 27878
	private const string ExternalTag = "prod";

	// Token: 0x04006CE7 RID: 27879
	private const string EditorTag = "editor";

	// Token: 0x04006CE8 RID: 27880
	private readonly ITelemetryClient m_telemetryClient = new TelemetryClient();

	// Token: 0x04006CE9 RID: 27881
	private static readonly TelemetryManager s_instance = new TelemetryManager();

	// Token: 0x04006CEA RID: 27882
	private Service m_telemetryService;

	// Token: 0x04006CEB RID: 27883
	private bool m_auroraConnected;

	// Token: 0x04006CEC RID: 27884
	private readonly List<Action> m_shutdownListeners = new List<Action>();

	// Token: 0x04006CED RID: 27885
	private readonly object m_listenerLock = new object();

	// Token: 0x04006CEE RID: 27886
	private BaseContextData m_contextData;

	// Token: 0x04006CF0 RID: 27888
	private static List<ITelemetryManagerComponent> s_components = new List<ITelemetryManagerComponent>
	{
		new TelemetryManagerComponentAudio(TelemetryManager.s_instance.m_telemetryClient),
		TelemetryManager.NetworkComponent
	};

	// Token: 0x04006CF1 RID: 27889
	private readonly Dictionary<long, List<Action<long>>> m_messagesWaitingForCallback = new Dictionary<long, List<Action<long>>>();
}
