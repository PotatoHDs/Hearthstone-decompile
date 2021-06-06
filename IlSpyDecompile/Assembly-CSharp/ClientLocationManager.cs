using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using UnityEngine;

public class ClientLocationManager : IService
{
	private bool m_requestingGPSData;

	private bool m_requestingWifiData;

	private IEnumerator m_requestGPSData;

	private IEnumerator m_requestWifiData;

	private float m_scanTimeout = 15f;

	private bool m_GPSCheatOn;

	private bool m_GPSCheatGPSEnabled;

	private bool m_WifiCheatOn;

	private bool m_WifiCheatWifiEnabled;

	private const string WIFI_CHEAT_NETWORK_NAME = "FAKE NETWORK";

	public bool GPSServicesReady
	{
		get
		{
			if (m_GPSCheatOn)
			{
				return true;
			}
			return HearthstoneServices.Get<LocationServices>().IsReady;
		}
	}

	public bool GPSServicesEnabled
	{
		get
		{
			if (m_GPSCheatOn)
			{
				return m_GPSCheatGPSEnabled;
			}
			return HearthstoneServices.Get<LocationServices>().IsEnabled;
		}
	}

	public bool WifiEnabled
	{
		get
		{
			if (m_WifiCheatOn)
			{
				return m_WifiCheatWifiEnabled;
			}
			if (!string.IsNullOrEmpty(GetWifiSSID))
			{
				return true;
			}
			return MobilePermissionsManager.Get().CheckPermission(MobilePermission.WIFI);
		}
	}

	public string GetWifiSSID
	{
		get
		{
			if (m_WifiCheatOn)
			{
				if (!m_WifiCheatWifiEnabled)
				{
					return null;
				}
				return "FAKE NETWORK";
			}
			return HearthstoneServices.Get<WifiInfo>().GetConnectedSSIDString();
		}
	}

	public bool GPSAvailable => HearthstoneServices.Get<LocationServices>().IsAvailable;

	public bool GPSOrWifiServicesAvailable
	{
		get
		{
			if (!HearthstoneServices.Get<WifiInfo>().IsAvailable)
			{
				return HearthstoneServices.Get<LocationServices>().IsAvailable;
			}
			return true;
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(LocationServices),
			typeof(WifiInfo)
		};
	}

	public void Shutdown()
	{
	}

	public static ClientLocationManager Get()
	{
		return HearthstoneServices.Get<ClientLocationManager>();
	}

	public void RequestGPSAndWifiData(Action<ClientLocationData> updateGPSCallback, Action<ClientLocationData> updateWIFICallback, Action completeCallback = null)
	{
		Processor.RunCoroutine(RequestGPSAndWifiDataCoroutine(updateGPSCallback, updateWIFICallback, completeCallback));
	}

	public void RequestGPSData(Action<ClientLocationData> updateCallback, Action completeCallback = null)
	{
		m_requestGPSData = RequestGPSDataCoroutine(updateCallback, completeCallback);
		Processor.RunCoroutine(m_requestGPSData);
	}

	public void RequestWifiData(Action<ClientLocationData> updateCallback, Action completeCallback = null)
	{
		m_requestWifiData = RequestWifiDataCoroutine(updateCallback, completeCallback);
		Processor.RunCoroutine(m_requestWifiData);
	}

	public void StopRequestingData()
	{
		StopRequestingGPSData();
		stopRequestingWifiData();
	}

	public void StopRequestingGPSData()
	{
		if (m_requestGPSData != null)
		{
			Processor.RunCoroutine(m_requestGPSData);
		}
		m_requestGPSData = null;
	}

	public void stopRequestingWifiData()
	{
		if (m_requestWifiData != null)
		{
			Processor.RunCoroutine(m_requestWifiData);
		}
		m_requestWifiData = null;
	}

	public ClientLocationData GetBestLocationData()
	{
		GpsCoordinate gpsCoordinate = HearthstoneServices.Get<LocationServices>().GetBestLocation();
		if (m_GPSCheatOn)
		{
			gpsCoordinate = null;
			if (m_GPSCheatGPSEnabled)
			{
				gpsCoordinate = new GpsCoordinate();
				gpsCoordinate.Accuracy = 30.0;
				gpsCoordinate.Timestamp = TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds;
			}
		}
		return new ClientLocationData
		{
			location = gpsCoordinate,
			accessPointSamples = HearthstoneServices.Get<WifiInfo>().GetLastKnownAccessPoints()
		};
	}

	public bool IsRequestingGPSData()
	{
		return m_requestingGPSData;
	}

	public bool IsRequestingWifiData()
	{
		return m_requestingWifiData;
	}

	private IEnumerator RequestGPSAndWifiDataCoroutine(Action<ClientLocationData> updateGPSCallback, Action<ClientLocationData> updateWIFICallback, Action completeCallback)
	{
		Log.FiresideGatherings.Print("ClientLocationManager.RequestGPSAndWIFIDataCoroutine");
		if (GPSServicesEnabled && GPSAvailable)
		{
			Processor.RunCoroutine(RequestGPSDataCoroutine(updateGPSCallback, null));
		}
		if (WifiEnabled)
		{
			Processor.RunCoroutine(RequestWifiDataCoroutine(updateWIFICallback, null));
		}
		float timer = 0f;
		while (timer < m_scanTimeout && (m_requestingGPSData || m_requestingWifiData))
		{
			timer += Time.deltaTime;
			yield return new WaitForSeconds(0.25f);
		}
		Log.FiresideGatherings.Print("ClientLocationManager.RequestGPSAndWIFIDataCoroutine Finished");
		completeCallback?.Invoke();
	}

	private IEnumerator RequestGPSDataCoroutine(Action<ClientLocationData> updateCallback, Action completeCallback)
	{
		Log.FiresideGatherings.Print("ClientLocationManager.RequestGPSDataCoroutine");
		if (!m_requestingGPSData && GPSServicesEnabled && GPSAvailable)
		{
			Processor.RunCoroutine(HearthstoneServices.Get<LocationServices>().UpdateLocation());
		}
		m_requestingGPSData = true;
		ClientLocationData bestData = GetBestLocationData();
		float timer = 0f;
		bool hasUpdated = false;
		while (timer < m_scanTimeout)
		{
			ClientLocationData bestLocationData = GetBestLocationData();
			if (bestLocationData.location != null && (!hasUpdated || !bestLocationData.location.Equals(bestData.location)))
			{
				hasUpdated = true;
				updateCallback?.Invoke(bestLocationData);
			}
			bestData = bestLocationData;
			if (!HearthstoneServices.Get<LocationServices>().IsQueryingLocation())
			{
				break;
			}
			timer += Time.deltaTime;
			yield return new WaitForSeconds(0.25f);
		}
		m_requestingGPSData = false;
		Log.FiresideGatherings.Print("ClientLocationManager.RequestGPSDataCoroutine Finished");
		completeCallback?.Invoke();
	}

	private IEnumerator RequestWifiDataCoroutine(Action<ClientLocationData> updateCallback, Action completeCallback)
	{
		Log.FiresideGatherings.Print("ClientLocationManager.RequestWIFIDataCoroutine");
		while (!HearthstoneServices.IsAvailable<WifiInfo>())
		{
			yield return null;
		}
		if (!m_requestingWifiData && WifiEnabled)
		{
			Processor.RunCoroutine(HearthstoneServices.Get<WifiInfo>().RequestVisibleAccessPoints());
		}
		m_requestingWifiData = true;
		ClientLocationData bestData = GetBestLocationData();
		bool hasUpdated = false;
		float timer = 0f;
		while (timer < m_scanTimeout)
		{
			ClientLocationData bestLocationData = GetBestLocationData();
			if (!bestLocationData.accessPointSamples.Equals(bestData.accessPointSamples) || !hasUpdated)
			{
				hasUpdated = true;
				updateCallback?.Invoke(bestLocationData);
			}
			bestData = bestLocationData;
			if (!HearthstoneServices.Get<WifiInfo>().IsScanningWifi())
			{
				break;
			}
			timer += Time.deltaTime;
			yield return new WaitForSeconds(0.25f);
		}
		Log.FiresideGatherings.Print("ClientLocationManager.RequestWIFIDataCoroutine Finished");
		m_requestingWifiData = false;
		yield return null;
		completeCallback?.Invoke();
	}

	public void Cheat_SetGPSEnabled(bool on)
	{
		m_GPSCheatOn = true;
		m_GPSCheatGPSEnabled = on;
	}

	public void Cheat_SetWifiEnabled(bool on)
	{
		m_WifiCheatOn = true;
		m_WifiCheatWifiEnabled = on;
	}
}
