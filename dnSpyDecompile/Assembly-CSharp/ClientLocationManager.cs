using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020007AB RID: 1963
public class ClientLocationManager : IService
{
	// Token: 0x06006CDA RID: 27866 RVA: 0x00232D32 File Offset: 0x00230F32
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	// Token: 0x06006CDB RID: 27867 RVA: 0x00232D3A File Offset: 0x00230F3A
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(LocationServices),
			typeof(WifiInfo)
		};
	}

	// Token: 0x06006CDC RID: 27868 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x06006CDD RID: 27869 RVA: 0x00232D5C File Offset: 0x00230F5C
	public static ClientLocationManager Get()
	{
		return HearthstoneServices.Get<ClientLocationManager>();
	}

	// Token: 0x06006CDE RID: 27870 RVA: 0x00232D63 File Offset: 0x00230F63
	public void RequestGPSAndWifiData(Action<ClientLocationData> updateGPSCallback, Action<ClientLocationData> updateWIFICallback, Action completeCallback = null)
	{
		Processor.RunCoroutine(this.RequestGPSAndWifiDataCoroutine(updateGPSCallback, updateWIFICallback, completeCallback), null);
	}

	// Token: 0x06006CDF RID: 27871 RVA: 0x00232D75 File Offset: 0x00230F75
	public void RequestGPSData(Action<ClientLocationData> updateCallback, Action completeCallback = null)
	{
		this.m_requestGPSData = this.RequestGPSDataCoroutine(updateCallback, completeCallback);
		Processor.RunCoroutine(this.m_requestGPSData, null);
	}

	// Token: 0x06006CE0 RID: 27872 RVA: 0x00232D92 File Offset: 0x00230F92
	public void RequestWifiData(Action<ClientLocationData> updateCallback, Action completeCallback = null)
	{
		this.m_requestWifiData = this.RequestWifiDataCoroutine(updateCallback, completeCallback);
		Processor.RunCoroutine(this.m_requestWifiData, null);
	}

	// Token: 0x06006CE1 RID: 27873 RVA: 0x00232DAF File Offset: 0x00230FAF
	public void StopRequestingData()
	{
		this.StopRequestingGPSData();
		this.stopRequestingWifiData();
	}

	// Token: 0x06006CE2 RID: 27874 RVA: 0x00232DBD File Offset: 0x00230FBD
	public void StopRequestingGPSData()
	{
		if (this.m_requestGPSData != null)
		{
			Processor.RunCoroutine(this.m_requestGPSData, null);
		}
		this.m_requestGPSData = null;
	}

	// Token: 0x06006CE3 RID: 27875 RVA: 0x00232DDB File Offset: 0x00230FDB
	public void stopRequestingWifiData()
	{
		if (this.m_requestWifiData != null)
		{
			Processor.RunCoroutine(this.m_requestWifiData, null);
		}
		this.m_requestWifiData = null;
	}

	// Token: 0x06006CE4 RID: 27876 RVA: 0x00232DFC File Offset: 0x00230FFC
	public ClientLocationData GetBestLocationData()
	{
		GpsCoordinate gpsCoordinate = HearthstoneServices.Get<LocationServices>().GetBestLocation();
		if (this.m_GPSCheatOn)
		{
			gpsCoordinate = null;
			if (this.m_GPSCheatGPSEnabled)
			{
				gpsCoordinate = new GpsCoordinate();
				gpsCoordinate.Accuracy = 30.0;
				gpsCoordinate.Timestamp = TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
			}
		}
		return new ClientLocationData
		{
			location = gpsCoordinate,
			accessPointSamples = HearthstoneServices.Get<WifiInfo>().GetLastKnownAccessPoints()
		};
	}

	// Token: 0x06006CE5 RID: 27877 RVA: 0x00232E73 File Offset: 0x00231073
	public bool IsRequestingGPSData()
	{
		return this.m_requestingGPSData;
	}

	// Token: 0x06006CE6 RID: 27878 RVA: 0x00232E7B File Offset: 0x0023107B
	public bool IsRequestingWifiData()
	{
		return this.m_requestingWifiData;
	}

	// Token: 0x1700066A RID: 1642
	// (get) Token: 0x06006CE7 RID: 27879 RVA: 0x00232E83 File Offset: 0x00231083
	public bool GPSServicesReady
	{
		get
		{
			return this.m_GPSCheatOn || HearthstoneServices.Get<LocationServices>().IsReady;
		}
	}

	// Token: 0x1700066B RID: 1643
	// (get) Token: 0x06006CE8 RID: 27880 RVA: 0x00232E99 File Offset: 0x00231099
	public bool GPSServicesEnabled
	{
		get
		{
			if (this.m_GPSCheatOn)
			{
				return this.m_GPSCheatGPSEnabled;
			}
			return HearthstoneServices.Get<LocationServices>().IsEnabled;
		}
	}

	// Token: 0x1700066C RID: 1644
	// (get) Token: 0x06006CE9 RID: 27881 RVA: 0x00232EB4 File Offset: 0x002310B4
	public bool WifiEnabled
	{
		get
		{
			if (this.m_WifiCheatOn)
			{
				return this.m_WifiCheatWifiEnabled;
			}
			return !string.IsNullOrEmpty(this.GetWifiSSID) || MobilePermissionsManager.Get().CheckPermission(MobilePermission.WIFI);
		}
	}

	// Token: 0x1700066D RID: 1645
	// (get) Token: 0x06006CEA RID: 27882 RVA: 0x00232EDF File Offset: 0x002310DF
	public string GetWifiSSID
	{
		get
		{
			if (!this.m_WifiCheatOn)
			{
				return HearthstoneServices.Get<WifiInfo>().GetConnectedSSIDString();
			}
			if (!this.m_WifiCheatWifiEnabled)
			{
				return null;
			}
			return "FAKE NETWORK";
		}
	}

	// Token: 0x1700066E RID: 1646
	// (get) Token: 0x06006CEB RID: 27883 RVA: 0x00232F03 File Offset: 0x00231103
	public bool GPSAvailable
	{
		get
		{
			return HearthstoneServices.Get<LocationServices>().IsAvailable;
		}
	}

	// Token: 0x1700066F RID: 1647
	// (get) Token: 0x06006CEC RID: 27884 RVA: 0x00232F0F File Offset: 0x0023110F
	public bool GPSOrWifiServicesAvailable
	{
		get
		{
			return HearthstoneServices.Get<WifiInfo>().IsAvailable || HearthstoneServices.Get<LocationServices>().IsAvailable;
		}
	}

	// Token: 0x06006CED RID: 27885 RVA: 0x00232F29 File Offset: 0x00231129
	private IEnumerator RequestGPSAndWifiDataCoroutine(Action<ClientLocationData> updateGPSCallback, Action<ClientLocationData> updateWIFICallback, Action completeCallback)
	{
		Log.FiresideGatherings.Print("ClientLocationManager.RequestGPSAndWIFIDataCoroutine", Array.Empty<object>());
		if (this.GPSServicesEnabled && this.GPSAvailable)
		{
			Processor.RunCoroutine(this.RequestGPSDataCoroutine(updateGPSCallback, null), null);
		}
		if (this.WifiEnabled)
		{
			Processor.RunCoroutine(this.RequestWifiDataCoroutine(updateWIFICallback, null), null);
		}
		float timer = 0f;
		while (timer < this.m_scanTimeout && (this.m_requestingGPSData || this.m_requestingWifiData))
		{
			timer += Time.deltaTime;
			yield return new WaitForSeconds(0.25f);
		}
		Log.FiresideGatherings.Print("ClientLocationManager.RequestGPSAndWIFIDataCoroutine Finished", Array.Empty<object>());
		if (completeCallback != null)
		{
			completeCallback();
		}
		yield break;
	}

	// Token: 0x06006CEE RID: 27886 RVA: 0x00232F4D File Offset: 0x0023114D
	private IEnumerator RequestGPSDataCoroutine(Action<ClientLocationData> updateCallback, Action completeCallback)
	{
		Log.FiresideGatherings.Print("ClientLocationManager.RequestGPSDataCoroutine", Array.Empty<object>());
		if (!this.m_requestingGPSData && this.GPSServicesEnabled && this.GPSAvailable)
		{
			Processor.RunCoroutine(HearthstoneServices.Get<LocationServices>().UpdateLocation(15), null);
		}
		this.m_requestingGPSData = true;
		ClientLocationData bestData = this.GetBestLocationData();
		float timer = 0f;
		bool hasUpdated = false;
		while (timer < this.m_scanTimeout)
		{
			ClientLocationData bestLocationData = this.GetBestLocationData();
			if (bestLocationData.location != null && (!hasUpdated || !bestLocationData.location.Equals(bestData.location)))
			{
				hasUpdated = true;
				if (updateCallback != null)
				{
					updateCallback(bestLocationData);
				}
			}
			bestData = bestLocationData;
			if (!HearthstoneServices.Get<LocationServices>().IsQueryingLocation())
			{
				break;
			}
			timer += Time.deltaTime;
			yield return new WaitForSeconds(0.25f);
		}
		this.m_requestingGPSData = false;
		Log.FiresideGatherings.Print("ClientLocationManager.RequestGPSDataCoroutine Finished", Array.Empty<object>());
		if (completeCallback != null)
		{
			completeCallback();
		}
		yield break;
	}

	// Token: 0x06006CEF RID: 27887 RVA: 0x00232F6A File Offset: 0x0023116A
	private IEnumerator RequestWifiDataCoroutine(Action<ClientLocationData> updateCallback, Action completeCallback)
	{
		Log.FiresideGatherings.Print("ClientLocationManager.RequestWIFIDataCoroutine", Array.Empty<object>());
		while (!HearthstoneServices.IsAvailable<WifiInfo>())
		{
			yield return null;
		}
		if (!this.m_requestingWifiData && this.WifiEnabled)
		{
			Processor.RunCoroutine(HearthstoneServices.Get<WifiInfo>().RequestVisibleAccessPoints(), null);
		}
		this.m_requestingWifiData = true;
		ClientLocationData bestData = this.GetBestLocationData();
		bool hasUpdated = false;
		float timer = 0f;
		while (timer < this.m_scanTimeout)
		{
			ClientLocationData bestLocationData = this.GetBestLocationData();
			if (!bestLocationData.accessPointSamples.Equals(bestData.accessPointSamples) || !hasUpdated)
			{
				hasUpdated = true;
				if (updateCallback != null)
				{
					updateCallback(bestLocationData);
				}
			}
			bestData = bestLocationData;
			if (!HearthstoneServices.Get<WifiInfo>().IsScanningWifi())
			{
				break;
			}
			timer += Time.deltaTime;
			yield return new WaitForSeconds(0.25f);
		}
		Log.FiresideGatherings.Print("ClientLocationManager.RequestWIFIDataCoroutine Finished", Array.Empty<object>());
		this.m_requestingWifiData = false;
		yield return null;
		if (completeCallback != null)
		{
			completeCallback();
		}
		yield break;
	}

	// Token: 0x06006CF0 RID: 27888 RVA: 0x00232F87 File Offset: 0x00231187
	public void Cheat_SetGPSEnabled(bool on)
	{
		this.m_GPSCheatOn = true;
		this.m_GPSCheatGPSEnabled = on;
	}

	// Token: 0x06006CF1 RID: 27889 RVA: 0x00232F97 File Offset: 0x00231197
	public void Cheat_SetWifiEnabled(bool on)
	{
		this.m_WifiCheatOn = true;
		this.m_WifiCheatWifiEnabled = on;
	}

	// Token: 0x040057C5 RID: 22469
	private bool m_requestingGPSData;

	// Token: 0x040057C6 RID: 22470
	private bool m_requestingWifiData;

	// Token: 0x040057C7 RID: 22471
	private IEnumerator m_requestGPSData;

	// Token: 0x040057C8 RID: 22472
	private IEnumerator m_requestWifiData;

	// Token: 0x040057C9 RID: 22473
	private float m_scanTimeout = 15f;

	// Token: 0x040057CA RID: 22474
	private bool m_GPSCheatOn;

	// Token: 0x040057CB RID: 22475
	private bool m_GPSCheatGPSEnabled;

	// Token: 0x040057CC RID: 22476
	private bool m_WifiCheatOn;

	// Token: 0x040057CD RID: 22477
	private bool m_WifiCheatWifiEnabled;

	// Token: 0x040057CE RID: 22478
	private const string WIFI_CHEAT_NETWORK_NAME = "FAKE NETWORK";
}
