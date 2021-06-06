using System;
using System.Collections.Generic;
using bgs;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;
using UnityEngine.Analytics;

public class PerformanceAnalytics : IService
{
	private float m_initStartTime;

	private bool m_isReconnecting;

	private string m_reconnectType = "INVALID";

	private float m_reconnectStartTime;

	private float m_disconnectTime;

	private string m_location = string.Empty;

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		BeginStartupTimer();
		if (BattleNet.IsInitialized() && BattleNet.IsConnected())
		{
			m_location = BattleNet.GetAccountCountry();
		}
		SendDisconnectAndTimeoutEvents();
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(SceneMgr) };
	}

	public void Shutdown()
	{
	}

	public static PerformanceAnalytics Get()
	{
		return HearthstoneServices.Get<PerformanceAnalytics>();
	}

	private void OnDestroy()
	{
		if (m_isReconnecting)
		{
			ReconnectEnd(success: false);
		}
	}

	public void BeginStartupTimer()
	{
		m_initStartTime = Time.realtimeSinceStartup;
	}

	public void EndStartupTimer()
	{
		float num = (Time.realtimeSinceStartup - m_initStartTime) * 1000f;
		Log.Performance.Print("Startup time: {0}", num);
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		dictionary.Add("build_version", "20.4");
		dictionary.Add("context", "StartupTime");
		dictionary.Add("time_elapsed", num);
		dictionary.Add("plugin_version", "v1.0.1");
		Analytics.CustomEvent("perfGenericTimer", dictionary);
	}

	public void ReconnectStart(string reconnectType)
	{
		if (!m_isReconnecting)
		{
			m_isReconnecting = true;
			m_reconnectType = reconnectType;
			m_reconnectStartTime = Time.realtimeSinceStartup;
			SceneMgr.Get().RegisterSceneLoadedEvent(ReconnectSceneLoaded);
			SendDisconnectAndTimeoutEvents();
		}
	}

	public void ReconnectSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (m_isReconnecting && mode == SceneMgr.Mode.GAMEPLAY)
		{
			ReconnectEnd(success: true);
			SceneMgr.Get().UnregisterSceneLoadedEvent(ReconnectSceneLoaded);
		}
	}

	public void DisconnectEvent(string mode)
	{
		m_disconnectTime = Time.realtimeSinceStartup;
		SceneMgr.Get().RegisterSceneLoadedEvent(DisconnectTimeReset);
		PlayerPrefs.SetInt("DisconnectEvent", 1);
		PlayerPrefs.SetString("DisconnectEvent_Mode", mode);
		PlayerPrefs.SetString("DisconnectEvent_Location", GetCountry());
		PlayerPrefs.SetString("DisconnectEvent_Connection", GetConnectionType());
		PlayerPrefs.SetString("DisconnectEvent_OS", PlatformSettings.OS.ToString());
	}

	public void SendDisconnectAndTimeoutEvents()
	{
		if (Application.internetReachability != 0)
		{
			if (PlayerPrefs.GetInt("DisconnectEvent") == 1)
			{
				PlayerPrefs.SetInt("DisconnectEvent", 0);
				Log.Performance.Print("Sent Disconnect Event");
			}
			if (PlayerPrefs.GetInt("ReconnectTimeOut") == 1)
			{
				PlayerPrefs.SetInt("ReconnectTimeOut", 0);
				TelemetryManager.Client().SendReconnectTimeout(PlayerPrefs.GetString("ReconnectTimeOut_Type"));
				Log.Performance.Print("Sent Reconnect Timout Event");
			}
		}
	}

	public void DisconnectTimeReset(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (!m_isReconnecting && (mode == SceneMgr.Mode.GAMEPLAY || mode == SceneMgr.Mode.HUB))
		{
			m_disconnectTime = 0f;
		}
	}

	public void ReconnectEnd(bool success)
	{
		if (m_isReconnecting)
		{
			SendDisconnectAndTimeoutEvents();
			m_isReconnecting = false;
			float reconnectDuration = Time.realtimeSinceStartup - m_reconnectStartTime;
			float disconnectDuration = Time.realtimeSinceStartup - m_disconnectTime;
			if (success)
			{
				TelemetryManager.Client().SendReconnectSuccess(disconnectDuration, reconnectDuration, m_reconnectType);
				m_disconnectTime = 0f;
				Log.Performance.Print("Sent Reconnect Success Event");
				return;
			}
			PlayerPrefs.SetInt("ReconnectTimeOut", 1);
			PlayerPrefs.SetString("ReconnectTimeOut_Type", m_reconnectType);
			PlayerPrefs.SetString("ReconnectTimeOut_Location", GetCountry());
			PlayerPrefs.SetString("ReconnectTimeOut_Connection", GetConnectionType());
			PlayerPrefs.SetString("ReconnectTimeOut_OS", PlatformSettings.OS.ToString());
			m_disconnectTime = 0f;
			Log.Performance.Print("Recorded Reconnect Timout");
		}
	}

	private string GetCountry()
	{
		if (string.IsNullOrEmpty(m_location) && BattleNet.IsConnected())
		{
			m_location = BattleNet.GetAccountCountry();
		}
		if (string.IsNullOrEmpty(m_location))
		{
			m_location = "Unknown";
		}
		return m_location;
	}

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
}
