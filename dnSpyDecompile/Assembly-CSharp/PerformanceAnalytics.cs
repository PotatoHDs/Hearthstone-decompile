using System;
using System.Collections.Generic;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;
using UnityEngine.Analytics;

// Token: 0x02000848 RID: 2120
public class PerformanceAnalytics : IService
{
	// Token: 0x06007307 RID: 29447 RVA: 0x002505F8 File Offset: 0x0024E7F8
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.BeginStartupTimer();
		if (BattleNet.IsInitialized() && BattleNet.IsConnected())
		{
			this.m_location = BattleNet.GetAccountCountry();
		}
		this.SendDisconnectAndTimeoutEvents();
		yield break;
	}

	// Token: 0x06007308 RID: 29448 RVA: 0x000D7BEA File Offset: 0x000D5DEA
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(SceneMgr)
		};
	}

	// Token: 0x06007309 RID: 29449 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x0600730A RID: 29450 RVA: 0x00250607 File Offset: 0x0024E807
	public static PerformanceAnalytics Get()
	{
		return HearthstoneServices.Get<PerformanceAnalytics>();
	}

	// Token: 0x0600730B RID: 29451 RVA: 0x0025060E File Offset: 0x0024E80E
	private void OnDestroy()
	{
		if (this.m_isReconnecting)
		{
			this.ReconnectEnd(false);
		}
	}

	// Token: 0x0600730C RID: 29452 RVA: 0x0025061F File Offset: 0x0024E81F
	public void BeginStartupTimer()
	{
		this.m_initStartTime = Time.realtimeSinceStartup;
	}

	// Token: 0x0600730D RID: 29453 RVA: 0x0025062C File Offset: 0x0024E82C
	public void EndStartupTimer()
	{
		float num = (Time.realtimeSinceStartup - this.m_initStartTime) * 1000f;
		global::Log.Performance.Print("Startup time: {0}", new object[]
		{
			num
		});
		Analytics.CustomEvent("perfGenericTimer", new Dictionary<string, object>
		{
			{
				"build_version",
				"20.4"
			},
			{
				"context",
				"StartupTime"
			},
			{
				"time_elapsed",
				num
			},
			{
				"plugin_version",
				"v1.0.1"
			}
		});
	}

	// Token: 0x0600730E RID: 29454 RVA: 0x002506BD File Offset: 0x0024E8BD
	public void ReconnectStart(string reconnectType)
	{
		if (this.m_isReconnecting)
		{
			return;
		}
		this.m_isReconnecting = true;
		this.m_reconnectType = reconnectType;
		this.m_reconnectStartTime = Time.realtimeSinceStartup;
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.ReconnectSceneLoaded));
		this.SendDisconnectAndTimeoutEvents();
	}

	// Token: 0x0600730F RID: 29455 RVA: 0x002506FD File Offset: 0x0024E8FD
	public void ReconnectSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (this.m_isReconnecting && mode == SceneMgr.Mode.GAMEPLAY)
		{
			this.ReconnectEnd(true);
			SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.ReconnectSceneLoaded));
		}
	}

	// Token: 0x06007310 RID: 29456 RVA: 0x0025072C File Offset: 0x0024E92C
	public void DisconnectEvent(string mode)
	{
		this.m_disconnectTime = Time.realtimeSinceStartup;
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.DisconnectTimeReset));
		PlayerPrefs.SetInt("DisconnectEvent", 1);
		PlayerPrefs.SetString("DisconnectEvent_Mode", mode);
		PlayerPrefs.SetString("DisconnectEvent_Location", this.GetCountry());
		PlayerPrefs.SetString("DisconnectEvent_Connection", this.GetConnectionType());
		PlayerPrefs.SetString("DisconnectEvent_OS", PlatformSettings.OS.ToString());
	}

	// Token: 0x06007311 RID: 29457 RVA: 0x002507B0 File Offset: 0x0024E9B0
	public void SendDisconnectAndTimeoutEvents()
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			return;
		}
		if (PlayerPrefs.GetInt("DisconnectEvent") == 1)
		{
			PlayerPrefs.SetInt("DisconnectEvent", 0);
			global::Log.Performance.Print("Sent Disconnect Event", Array.Empty<object>());
		}
		if (PlayerPrefs.GetInt("ReconnectTimeOut") == 1)
		{
			PlayerPrefs.SetInt("ReconnectTimeOut", 0);
			TelemetryManager.Client().SendReconnectTimeout(PlayerPrefs.GetString("ReconnectTimeOut_Type"));
			global::Log.Performance.Print("Sent Reconnect Timout Event", Array.Empty<object>());
		}
	}

	// Token: 0x06007312 RID: 29458 RVA: 0x00250831 File Offset: 0x0024EA31
	public void DisconnectTimeReset(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (!this.m_isReconnecting && (mode == SceneMgr.Mode.GAMEPLAY || mode == SceneMgr.Mode.HUB))
		{
			this.m_disconnectTime = 0f;
		}
	}

	// Token: 0x06007313 RID: 29459 RVA: 0x00250850 File Offset: 0x0024EA50
	public void ReconnectEnd(bool success)
	{
		if (!this.m_isReconnecting)
		{
			return;
		}
		this.SendDisconnectAndTimeoutEvents();
		this.m_isReconnecting = false;
		float reconnectDuration = Time.realtimeSinceStartup - this.m_reconnectStartTime;
		float disconnectDuration = Time.realtimeSinceStartup - this.m_disconnectTime;
		if (success)
		{
			TelemetryManager.Client().SendReconnectSuccess(disconnectDuration, reconnectDuration, this.m_reconnectType);
			this.m_disconnectTime = 0f;
			global::Log.Performance.Print("Sent Reconnect Success Event", Array.Empty<object>());
			return;
		}
		PlayerPrefs.SetInt("ReconnectTimeOut", 1);
		PlayerPrefs.SetString("ReconnectTimeOut_Type", this.m_reconnectType);
		PlayerPrefs.SetString("ReconnectTimeOut_Location", this.GetCountry());
		PlayerPrefs.SetString("ReconnectTimeOut_Connection", this.GetConnectionType());
		PlayerPrefs.SetString("ReconnectTimeOut_OS", PlatformSettings.OS.ToString());
		this.m_disconnectTime = 0f;
		global::Log.Performance.Print("Recorded Reconnect Timout", Array.Empty<object>());
	}

	// Token: 0x06007314 RID: 29460 RVA: 0x00250939 File Offset: 0x0024EB39
	private string GetCountry()
	{
		if (string.IsNullOrEmpty(this.m_location) && BattleNet.IsConnected())
		{
			this.m_location = BattleNet.GetAccountCountry();
		}
		if (string.IsNullOrEmpty(this.m_location))
		{
			this.m_location = "Unknown";
		}
		return this.m_location;
	}

	// Token: 0x06007315 RID: 29461 RVA: 0x00250978 File Offset: 0x0024EB78
	private string GetConnectionType()
	{
		if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
		{
			return "Cellular";
		}
		if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
		{
			return "LAN";
		}
		return "None";
	}

	// Token: 0x04005BA5 RID: 23461
	private float m_initStartTime;

	// Token: 0x04005BA6 RID: 23462
	private bool m_isReconnecting;

	// Token: 0x04005BA7 RID: 23463
	private string m_reconnectType = "INVALID";

	// Token: 0x04005BA8 RID: 23464
	private float m_reconnectStartTime;

	// Token: 0x04005BA9 RID: 23465
	private float m_disconnectTime;

	// Token: 0x04005BAA RID: 23466
	private string m_location = string.Empty;
}
