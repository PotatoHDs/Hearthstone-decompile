using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using agent;
using AOT;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class AgentEmbeddedAPI
{
	// Token: 0x06000009 RID: 9 RVA: 0x00002230 File Offset: 0x00000430
	private static List<ICallbackHandler> GetListeners()
	{
		List<ICallbackHandler> obj = AgentEmbeddedAPI.s_listeners;
		List<ICallbackHandler> result;
		lock (obj)
		{
			List<ICallbackHandler> list = new List<ICallbackHandler>();
			foreach (ICallbackHandler item in AgentEmbeddedAPI.s_listeners)
			{
				list.Add(item);
			}
			result = list;
		}
		return result;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000022B8 File Offset: 0x000004B8
	public static IDisposable Subscribe(ICallbackHandler listener)
	{
		List<ICallbackHandler> obj = AgentEmbeddedAPI.s_listeners;
		lock (obj)
		{
			if (!AgentEmbeddedAPI.s_listeners.Contains(listener))
			{
				Debug.Log("Subscribing a new AgentEmbeddedAPI listener");
				AgentEmbeddedAPI.s_listeners.Add(listener);
			}
		}
		return new Unsubscriber<ICallbackHandler>(listener);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000231C File Offset: 0x0000051C
	public static bool Initialize(string installDir, string logDir)
	{
		return AgentEmbeddedAPI.Initialize(installDir, logDir, string.Empty, string.Empty);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000232F File Offset: 0x0000052F
	static AgentEmbeddedAPI()
	{
		AgentEmbeddedAPI.s_listeners = new List<ICallbackHandler>();
		AgentEmbeddedAPI.GlobalInitialize();
	}

	// Token: 0x0600000D RID: 13
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool GlobalInitialize();

	// Token: 0x0600000E RID: 14
	[DllImport("agent", CharSet = CharSet.Ansi)]
	public static extern void GlobalShutdown();

	// Token: 0x0600000F RID: 15 RVA: 0x00002358 File Offset: 0x00000558
	private static void AdjustEnvironmentPath()
	{
		string environmentVariable = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
		string text = Application.dataPath + Path.DirectorySeparatorChar.ToString() + "Plugins";
		if (environmentVariable != null && !environmentVariable.Contains(text))
		{
			Environment.SetEnvironmentVariable("PATH", environmentVariable + Path.PathSeparator.ToString() + text, EnvironmentVariableTarget.Process);
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000023B9 File Offset: 0x000005B9
	public static ProductStatus GetStatus()
	{
		return (ProductStatus)Marshal.PtrToStructure(AgentEmbeddedAPI.GetStatusPtr(), typeof(ProductStatus));
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000023D4 File Offset: 0x000005D4
	public static bool Initialize(string installDir, string logDir, string versionToken, string region)
	{
		AgentEmbeddedAPI.AdjustEnvironmentPath();
		byte[] bytes = Encoding.UTF8.GetBytes(installDir);
		GCHandle gchandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
		byte[] bytes2 = Encoding.UTF8.GetBytes(logDir);
		GCHandle gchandle2 = GCHandle.Alloc(bytes2, GCHandleType.Pinned);
		byte[] bytes3 = Encoding.UTF8.GetBytes(region);
		GCHandle gchandle3 = GCHandle.Alloc(bytes3, GCHandleType.Pinned);
		byte[] bytes4 = Encoding.UTF8.GetBytes(versionToken);
		GCHandle gchandle4 = GCHandle.Alloc(bytes4, GCHandleType.Pinned);
		bool result = AgentEmbeddedAPI.UpdaterInitialize(gchandle.AddrOfPinnedObject(), bytes.Length, gchandle2.AddrOfPinnedObject(), bytes2.Length, gchandle3.AddrOfPinnedObject(), bytes3.Length, gchandle4.AddrOfPinnedObject(), bytes4.Length);
		gchandle.Free();
		return result;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002472 File Offset: 0x00000672
	public static void Shutdown()
	{
		AgentEmbeddedAPI.UpdaterShutdown();
	}

	// Token: 0x06000013 RID: 19
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int CancelAllOperations();

	// Token: 0x06000014 RID: 20
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int CreateProductInstall([MarshalAs(UnmanagedType.LPStr)] [In] string product, ref UserSettings settings);

	// Token: 0x06000015 RID: 21
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.LPStr)]
	public static extern string GetOpaqueString([MarshalAs(UnmanagedType.LPStr)] [In] string key);

	// Token: 0x06000016 RID: 22
	[DllImport("agent", CharSet = CharSet.Ansi)]
	public static extern IntPtr GetStatusPtr();

	// Token: 0x06000017 RID: 23
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int ModifyProductInstall(ref UserSettings settings);

	// Token: 0x06000018 RID: 24 RVA: 0x00002479 File Offset: 0x00000679
	public static int SetBgdlParams(string options)
	{
		return AgentEmbeddedAPI.SetBackgroundDownloadParams(options);
	}

	// Token: 0x06000019 RID: 25
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int SetBackgroundDownloadParams([MarshalAs(UnmanagedType.LPStr)] string options);

	// Token: 0x0600001A RID: 26 RVA: 0x00002481 File Offset: 0x00000681
	public static void SetPatchUrlOverride(string product, string url)
	{
		AgentEmbeddedAPI.SetUrlOverride(product, url, new AgentEmbeddedAPI.OverrideUrlChangedDelegateInternal(AgentEmbeddedAPI._PatchOverrideUrlDelegate));
	}

	// Token: 0x0600001B RID: 27
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int SetUpdateParams([MarshalAs(UnmanagedType.LPStr)] string options);

	// Token: 0x0600001C RID: 28 RVA: 0x00002498 File Offset: 0x00000698
	[MonoPInvokeCallback(typeof(AgentEmbeddedAPI.OverrideUrlChangedDelegateInternal))]
	private static void _PatchOverrideUrlDelegate([MarshalAs(UnmanagedType.LPStr)] string product, [MarshalAs(UnmanagedType.LPStr)] string overrideUrl)
	{
		OverrideUrlChangedMessage overrideUrlChangedMessage = new OverrideUrlChangedMessage();
		overrideUrlChangedMessage.m_product = product;
		overrideUrlChangedMessage.m_overrideUrl = overrideUrl;
		foreach (ICallbackHandler callbackHandler in AgentEmbeddedAPI.GetListeners())
		{
			callbackHandler.OnPatchOverrideUrlChanged(overrideUrlChangedMessage);
		}
	}

	// Token: 0x0600001D RID: 29
	[DllImport("agent", CharSet = CharSet.Ansi)]
	private static extern void SetUrlOverride([MarshalAs(UnmanagedType.LPStr)] [In] string product, [MarshalAs(UnmanagedType.LPStr)] [In] string url, AgentEmbeddedAPI.OverrideUrlChangedDelegateInternal callback);

	// Token: 0x0600001E RID: 30
	[DllImport("agent", CharSet = CharSet.Ansi)]
	private static extern void SetTelemetryDelegate(AgentEmbeddedAPI.TelemetryDelegateInternal callback);

	// Token: 0x0600001F RID: 31 RVA: 0x000024FC File Offset: 0x000006FC
	public static void SetVersionServiceUrlOverride(string product, string url, string token)
	{
		AgentEmbeddedAPI.SetVersionServiceOverride(product, url, token, new AgentEmbeddedAPI.OverrideUrlChangedDelegateInternal(AgentEmbeddedAPI._VersionServiceOverrideUrlDelegate));
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002514 File Offset: 0x00000714
	[MonoPInvokeCallback(typeof(AgentEmbeddedAPI.OverrideUrlChangedDelegateInternal))]
	private static void _VersionServiceOverrideUrlDelegate([MarshalAs(UnmanagedType.LPStr)] string product, [MarshalAs(UnmanagedType.LPStr)] string overrideUrl)
	{
		OverrideUrlChangedMessage overrideUrlChangedMessage = new OverrideUrlChangedMessage();
		overrideUrlChangedMessage.m_product = product;
		overrideUrlChangedMessage.m_overrideUrl = overrideUrl;
		foreach (ICallbackHandler callbackHandler in AgentEmbeddedAPI.GetListeners())
		{
			callbackHandler.OnVersionServiceOverrideUrlChanged(overrideUrlChangedMessage);
		}
	}

	// Token: 0x06000021 RID: 33
	[DllImport("agent", CharSet = CharSet.Ansi)]
	private static extern void SetVersionServiceOverride([MarshalAs(UnmanagedType.LPStr)] [In] string product, [MarshalAs(UnmanagedType.LPStr)] [In] string url, [MarshalAs(UnmanagedType.LPStr)] [In] string token, AgentEmbeddedAPI.OverrideUrlChangedDelegateInternal callback);

	// Token: 0x06000022 RID: 34
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int StartBackgroundDownload([MarshalAs(UnmanagedType.LPStr)] string options);

	// Token: 0x06000023 RID: 35
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int StartRepair([MarshalAs(UnmanagedType.LPStr)] string options);

	// Token: 0x06000024 RID: 36
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int StartUninstall([MarshalAs(UnmanagedType.LPStr)] string options);

	// Token: 0x06000025 RID: 37
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int StartUpdate([MarshalAs(UnmanagedType.LPStr)] string options);

	// Token: 0x06000026 RID: 38 RVA: 0x00002578 File Offset: 0x00000778
	public static int StartUpdate(string options, NotificationUpdateSettings settings)
	{
		return AgentEmbeddedAPI.StartUpdate(options);
	}

	// Token: 0x06000027 RID: 39
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int StartVersion([MarshalAs(UnmanagedType.LPStr)] string options);

	// Token: 0x06000028 RID: 40 RVA: 0x00002580 File Offset: 0x00000780
	public static void SetTelemetry(bool enable)
	{
		AgentEmbeddedAPI.SetTelemetryDelegate(enable ? AgentEmbeddedAPI.s_internalDelegate : null);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002592 File Offset: 0x00000792
	public static void SetTelemetryCallback(AgentEmbeddedAPI.TelemetryDelegate callback)
	{
		if (callback != null)
		{
			AgentEmbeddedAPI.s_telemetryDelegate = callback;
			AgentEmbeddedAPI.SetTelemetry(true);
			return;
		}
		AgentEmbeddedAPI.SetTelemetry(false);
		AgentEmbeddedAPI.s_telemetryDelegate = null;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x000025B0 File Offset: 0x000007B0
	[MonoPInvokeCallback(typeof(AgentEmbeddedAPI.TelemetryDelegateInternal))]
	private static void _TelemetryDelegate([MarshalAs(UnmanagedType.LPStr)] string msgName, [MarshalAs(UnmanagedType.LPStr)] string pkgName, IntPtr buffer, IntPtr length, [MarshalAs(UnmanagedType.LPStr)] string component)
	{
		TelemetryMessage telemetryMessage = new TelemetryMessage();
		telemetryMessage.m_messageName = msgName;
		telemetryMessage.m_packageName = pkgName;
		telemetryMessage.m_component = component;
		int num = length.ToInt32();
		if (num > 0)
		{
			telemetryMessage.m_payload = new byte[num];
			Marshal.Copy(buffer, telemetryMessage.m_payload, 0, num);
		}
		if (AgentEmbeddedAPI.s_telemetryDelegate != null)
		{
			AgentEmbeddedAPI.s_telemetryDelegate(telemetryMessage);
		}
		foreach (ICallbackHandler callbackHandler in AgentEmbeddedAPI.GetListeners())
		{
			callbackHandler.OnTelemetry(telemetryMessage);
		}
	}

	// Token: 0x0600002B RID: 43
	[DllImport("agent", CharSet = CharSet.Unicode)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool UpdaterInitialize(IntPtr installDir, int size, IntPtr logDir, int logDirSize, IntPtr region, int regionSize, IntPtr token, int tokenSize);

	// Token: 0x0600002C RID: 44
	[DllImport("agent", CharSet = CharSet.Ansi)]
	private static extern void UpdaterShutdown();

	// Token: 0x0600002D RID: 45
	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern void CreateCrash([MarshalAs(UnmanagedType.LPStr)] string options);

	// Token: 0x0400000B RID: 11
	internal static List<ICallbackHandler> s_listeners;

	// Token: 0x0400000C RID: 12
	public const string IMPORT = "agent";

	// Token: 0x0400000D RID: 13
	private static AgentEmbeddedAPI.TelemetryDelegate s_telemetryDelegate = null;

	// Token: 0x0400000E RID: 14
	private static AgentEmbeddedAPI.TelemetryDelegateInternal s_internalDelegate = new AgentEmbeddedAPI.TelemetryDelegateInternal(AgentEmbeddedAPI._TelemetryDelegate);

	// Token: 0x02000546 RID: 1350
	// (Invoke) Token: 0x06006140 RID: 24896
	public delegate void TelemetryDelegate(TelemetryMessage msg);

	// Token: 0x02000547 RID: 1351
	// (Invoke) Token: 0x06006144 RID: 24900
	private delegate void TelemetryDelegateInternal([MarshalAs(UnmanagedType.LPStr)] string msgName, [MarshalAs(UnmanagedType.LPStr)] string pkgName, IntPtr buffer, IntPtr length, [MarshalAs(UnmanagedType.LPStr)] string component);

	// Token: 0x02000548 RID: 1352
	// (Invoke) Token: 0x06006148 RID: 24904
	private delegate void OverrideUrlChangedDelegateInternal([MarshalAs(UnmanagedType.LPStr)] string product, [MarshalAs(UnmanagedType.LPStr)] string overrideUrl);
}
