using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using agent;
using AOT;
using UnityEngine;

public class AgentEmbeddedAPI
{
	public delegate void TelemetryDelegate(TelemetryMessage msg);

	private delegate void TelemetryDelegateInternal([MarshalAs(UnmanagedType.LPStr)] string msgName, [MarshalAs(UnmanagedType.LPStr)] string pkgName, IntPtr buffer, IntPtr length, [MarshalAs(UnmanagedType.LPStr)] string component);

	private delegate void OverrideUrlChangedDelegateInternal([MarshalAs(UnmanagedType.LPStr)] string product, [MarshalAs(UnmanagedType.LPStr)] string overrideUrl);

	internal static List<ICallbackHandler> s_listeners;

	public const string IMPORT = "agent";

	private static TelemetryDelegate s_telemetryDelegate;

	private static TelemetryDelegateInternal s_internalDelegate;

	private static List<ICallbackHandler> GetListeners()
	{
		lock (s_listeners)
		{
			List<ICallbackHandler> list = new List<ICallbackHandler>();
			foreach (ICallbackHandler s_listener in s_listeners)
			{
				list.Add(s_listener);
			}
			return list;
		}
	}

	public static IDisposable Subscribe(ICallbackHandler listener)
	{
		lock (s_listeners)
		{
			if (!s_listeners.Contains(listener))
			{
				Debug.Log("Subscribing a new AgentEmbeddedAPI listener");
				s_listeners.Add(listener);
			}
		}
		return new Unsubscriber<ICallbackHandler>(listener);
	}

	public static bool Initialize(string installDir, string logDir)
	{
		return Initialize(installDir, logDir, string.Empty, string.Empty);
	}

	static AgentEmbeddedAPI()
	{
		s_telemetryDelegate = null;
		s_internalDelegate = _TelemetryDelegate;
		s_listeners = new List<ICallbackHandler>();
		GlobalInitialize();
	}

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool GlobalInitialize();

	[DllImport("agent", CharSet = CharSet.Ansi)]
	public static extern void GlobalShutdown();

	private static void AdjustEnvironmentPath()
	{
		string environmentVariable = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
		string text = Application.dataPath + Path.DirectorySeparatorChar + "Plugins";
		if (environmentVariable != null && !environmentVariable.Contains(text))
		{
			Environment.SetEnvironmentVariable("PATH", environmentVariable + Path.PathSeparator + text, EnvironmentVariableTarget.Process);
		}
	}

	public static ProductStatus GetStatus()
	{
		return (ProductStatus)Marshal.PtrToStructure(GetStatusPtr(), typeof(ProductStatus));
	}

	public static bool Initialize(string installDir, string logDir, string versionToken, string region)
	{
		AdjustEnvironmentPath();
		byte[] bytes = Encoding.UTF8.GetBytes(installDir);
		GCHandle gCHandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
		byte[] bytes2 = Encoding.UTF8.GetBytes(logDir);
		GCHandle gCHandle2 = GCHandle.Alloc(bytes2, GCHandleType.Pinned);
		byte[] bytes3 = Encoding.UTF8.GetBytes(region);
		GCHandle gCHandle3 = GCHandle.Alloc(bytes3, GCHandleType.Pinned);
		byte[] bytes4 = Encoding.UTF8.GetBytes(versionToken);
		bool result = UpdaterInitialize(token: GCHandle.Alloc(bytes4, GCHandleType.Pinned).AddrOfPinnedObject(), installDir: gCHandle.AddrOfPinnedObject(), size: bytes.Length, logDir: gCHandle2.AddrOfPinnedObject(), logDirSize: bytes2.Length, region: gCHandle3.AddrOfPinnedObject(), regionSize: bytes3.Length, tokenSize: bytes4.Length);
		gCHandle.Free();
		return result;
	}

	public static void Shutdown()
	{
		UpdaterShutdown();
	}

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int CancelAllOperations();

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int CreateProductInstall([In][MarshalAs(UnmanagedType.LPStr)] string product, ref UserSettings settings);

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.LPStr)]
	public static extern string GetOpaqueString([In][MarshalAs(UnmanagedType.LPStr)] string key);

	[DllImport("agent", CharSet = CharSet.Ansi)]
	public static extern IntPtr GetStatusPtr();

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int ModifyProductInstall(ref UserSettings settings);

	public static int SetBgdlParams(string options)
	{
		return SetBackgroundDownloadParams(options);
	}

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int SetBackgroundDownloadParams([MarshalAs(UnmanagedType.LPStr)] string options);

	public static void SetPatchUrlOverride(string product, string url)
	{
		SetUrlOverride(product, url, _PatchOverrideUrlDelegate);
	}

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int SetUpdateParams([MarshalAs(UnmanagedType.LPStr)] string options);

	[MonoPInvokeCallback(typeof(OverrideUrlChangedDelegateInternal))]
	private static void _PatchOverrideUrlDelegate([MarshalAs(UnmanagedType.LPStr)] string product, [MarshalAs(UnmanagedType.LPStr)] string overrideUrl)
	{
		OverrideUrlChangedMessage overrideUrlChangedMessage = new OverrideUrlChangedMessage();
		overrideUrlChangedMessage.m_product = product;
		overrideUrlChangedMessage.m_overrideUrl = overrideUrl;
		foreach (ICallbackHandler listener in GetListeners())
		{
			listener.OnPatchOverrideUrlChanged(overrideUrlChangedMessage);
		}
	}

	[DllImport("agent", CharSet = CharSet.Ansi)]
	private static extern void SetUrlOverride([In][MarshalAs(UnmanagedType.LPStr)] string product, [In][MarshalAs(UnmanagedType.LPStr)] string url, OverrideUrlChangedDelegateInternal callback);

	[DllImport("agent", CharSet = CharSet.Ansi)]
	private static extern void SetTelemetryDelegate(TelemetryDelegateInternal callback);

	public static void SetVersionServiceUrlOverride(string product, string url, string token)
	{
		SetVersionServiceOverride(product, url, token, _VersionServiceOverrideUrlDelegate);
	}

	[MonoPInvokeCallback(typeof(OverrideUrlChangedDelegateInternal))]
	private static void _VersionServiceOverrideUrlDelegate([MarshalAs(UnmanagedType.LPStr)] string product, [MarshalAs(UnmanagedType.LPStr)] string overrideUrl)
	{
		OverrideUrlChangedMessage overrideUrlChangedMessage = new OverrideUrlChangedMessage();
		overrideUrlChangedMessage.m_product = product;
		overrideUrlChangedMessage.m_overrideUrl = overrideUrl;
		foreach (ICallbackHandler listener in GetListeners())
		{
			listener.OnVersionServiceOverrideUrlChanged(overrideUrlChangedMessage);
		}
	}

	[DllImport("agent", CharSet = CharSet.Ansi)]
	private static extern void SetVersionServiceOverride([In][MarshalAs(UnmanagedType.LPStr)] string product, [In][MarshalAs(UnmanagedType.LPStr)] string url, [In][MarshalAs(UnmanagedType.LPStr)] string token, OverrideUrlChangedDelegateInternal callback);

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int StartBackgroundDownload([MarshalAs(UnmanagedType.LPStr)] string options);

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int StartRepair([MarshalAs(UnmanagedType.LPStr)] string options);

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int StartUninstall([MarshalAs(UnmanagedType.LPStr)] string options);

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int StartUpdate([MarshalAs(UnmanagedType.LPStr)] string options);

	public static int StartUpdate(string options, NotificationUpdateSettings settings)
	{
		return StartUpdate(options);
	}

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern int StartVersion([MarshalAs(UnmanagedType.LPStr)] string options);

	public static void SetTelemetry(bool enable)
	{
		SetTelemetryDelegate(enable ? s_internalDelegate : null);
	}

	public static void SetTelemetryCallback(TelemetryDelegate callback)
	{
		if (callback != null)
		{
			s_telemetryDelegate = callback;
			SetTelemetry(enable: true);
		}
		else
		{
			SetTelemetry(enable: false);
			s_telemetryDelegate = null;
		}
	}

	[MonoPInvokeCallback(typeof(TelemetryDelegateInternal))]
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
		if (s_telemetryDelegate != null)
		{
			s_telemetryDelegate(telemetryMessage);
		}
		foreach (ICallbackHandler listener in GetListeners())
		{
			listener.OnTelemetry(telemetryMessage);
		}
	}

	[DllImport("agent", CharSet = CharSet.Unicode)]
	[return: MarshalAs(UnmanagedType.U1)]
	private static extern bool UpdaterInitialize(IntPtr installDir, int size, IntPtr logDir, int logDirSize, IntPtr region, int regionSize, IntPtr token, int tokenSize);

	[DllImport("agent", CharSet = CharSet.Ansi)]
	private static extern void UpdaterShutdown();

	[DllImport("agent", CharSet = CharSet.Ansi)]
	[return: MarshalAs(UnmanagedType.I4)]
	public static extern void CreateCrash([MarshalAs(UnmanagedType.LPStr)] string options);
}
