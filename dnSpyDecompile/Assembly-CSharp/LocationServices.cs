using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020007AD RID: 1965
public class LocationServices : IService
{
	// Token: 0x06006CFB RID: 27899 RVA: 0x00233236 File Offset: 0x00231436
	public static void StartGeoSearch()
	{
		WindowsLocationAPI.StartGeoSearch();
	}

	// Token: 0x06006CFC RID: 27900 RVA: 0x0023323D File Offset: 0x0023143D
	public static double GetLatitude()
	{
		return WindowsLocationAPI.GetLatitude();
	}

	// Token: 0x06006CFD RID: 27901 RVA: 0x00233244 File Offset: 0x00231444
	public static double GetLongitude()
	{
		return WindowsLocationAPI.GetLongitude();
	}

	// Token: 0x06006CFE RID: 27902 RVA: 0x0023324B File Offset: 0x0023144B
	public static double GetVerticalAccuracy()
	{
		return WindowsLocationAPI.GetVerticalAccuracy();
	}

	// Token: 0x06006CFF RID: 27903 RVA: 0x00233252 File Offset: 0x00231452
	public static double GetHorizontalAccuracy()
	{
		return WindowsLocationAPI.GetHorizontalAccuracy();
	}

	// Token: 0x06006D00 RID: 27904 RVA: 0x00233259 File Offset: 0x00231459
	public static bool GetEnabled()
	{
		return WindowsLocationAPI.GetEnabled();
	}

	// Token: 0x06006D01 RID: 27905 RVA: 0x00233260 File Offset: 0x00231460
	public static bool GetReady()
	{
		return WindowsLocationAPI.GetReady();
	}

	// Token: 0x06006D02 RID: 27906 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public static void StartSearching()
	{
	}

	// Token: 0x06006D03 RID: 27907 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public static void StopSearching()
	{
	}

	// Token: 0x06006D04 RID: 27908 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public static int HasPermission()
	{
		return 0;
	}

	// Token: 0x06006D05 RID: 27909 RVA: 0x00090064 File Offset: 0x0008E264
	public static string GetLocationData()
	{
		return null;
	}

	// Token: 0x06006D06 RID: 27910 RVA: 0x00233267 File Offset: 0x00231467
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	// Token: 0x06006D07 RID: 27911 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x06006D08 RID: 27912 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x17000670 RID: 1648
	// (get) Token: 0x06006D09 RID: 27913 RVA: 0x0023326F File Offset: 0x0023146F
	public bool IsAvailable
	{
		get
		{
			return Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.OSXPlayer && Application.platform != RuntimePlatform.OSXEditor;
		}
	}

	// Token: 0x17000671 RID: 1649
	// (get) Token: 0x06006D0A RID: 27914 RVA: 0x00233294 File Offset: 0x00231494
	public bool IsReady
	{
		get
		{
			bool result;
			try
			{
				result = LocationServices.GetReady();
			}
			catch (DllNotFoundException)
			{
				Log.FiresideGatherings.Print("Location API DLL not available", Array.Empty<object>());
				result = true;
			}
			catch (Exception ex)
			{
				Log.FiresideGatherings.PrintWarning("Couldn't check for device location services readiness.\n" + ex.Message, Array.Empty<object>());
				result = true;
			}
			return result;
		}
	}

	// Token: 0x17000672 RID: 1650
	// (get) Token: 0x06006D0B RID: 27915 RVA: 0x00233304 File Offset: 0x00231504
	public bool IsEnabled
	{
		get
		{
			bool result;
			try
			{
				if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
				{
					result = LocationServices.GetEnabled();
				}
				else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
				{
					result = (LocationServices.HasPermission() == 1);
				}
				else
				{
					result = MobilePermissionsManager.Get().CheckPermission(MobilePermission.FINE_LOCATION);
				}
			}
			catch (DllNotFoundException)
			{
				Log.FiresideGatherings.Print("Location API DLL not available", Array.Empty<object>());
				result = false;
			}
			catch (Exception ex)
			{
				Log.FiresideGatherings.PrintWarning("Couldn't check for device location services availability.\n" + ex.Message, Array.Empty<object>());
				result = false;
			}
			return result;
		}
	}

	// Token: 0x06006D0C RID: 27916 RVA: 0x002333AC File Offset: 0x002315AC
	public GpsCoordinate GetLastKnownLocation()
	{
		return this.m_lastKnownLocation;
	}

	// Token: 0x06006D0D RID: 27917 RVA: 0x002333B4 File Offset: 0x002315B4
	public GpsCoordinate GetBestLocation()
	{
		return this.m_bestLocation;
	}

	// Token: 0x06006D0E RID: 27918 RVA: 0x002333BC File Offset: 0x002315BC
	public bool IsQueryingLocation()
	{
		return this.m_isQueryingLocation;
	}

	// Token: 0x06006D0F RID: 27919 RVA: 0x002333C4 File Offset: 0x002315C4
	public IEnumerator UpdateLocation(int maxTime = 15)
	{
		this.m_lastKnownLocation = new GpsCoordinate();
		this.m_bestLocation = new GpsCoordinate();
		this.m_isQueryingLocation = true;
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
		{
			yield return Processor.RunCoroutine(this.UpdateLocationWindows(maxTime), null);
		}
		else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
		{
			yield return Processor.RunCoroutine(this.UpdateLocationOSX(maxTime), null);
		}
		else
		{
			yield return Processor.RunCoroutine(this.UpdateLocationMobile(maxTime), null);
		}
		yield break;
	}

	// Token: 0x06006D10 RID: 27920 RVA: 0x002333DA File Offset: 0x002315DA
	public IEnumerator UpdateLocationWindows(int maxTime)
	{
		int num;
		for (int timeSpent = 0; timeSpent < maxTime; timeSpent = num)
		{
			LocationServices.StartGeoSearch();
			GpsCoordinate gpsCoordinate = new GpsCoordinate();
			gpsCoordinate.Latitude = LocationServices.GetLatitude();
			gpsCoordinate.Longitude = LocationServices.GetLongitude();
			gpsCoordinate.Accuracy = LocationServices.GetHorizontalAccuracy();
			gpsCoordinate.Timestamp = TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
			if (gpsCoordinate.Accuracy < this.m_bestLocation.Accuracy)
			{
				this.m_bestLocation = gpsCoordinate;
			}
			this.m_lastKnownLocation = gpsCoordinate;
			if (this.m_bestLocation.Accuracy <= (double)this.m_stoppingAccuracy)
			{
				break;
			}
			yield return new WaitForSeconds(1f);
			num = timeSpent + 1;
		}
		this.m_isQueryingLocation = false;
		yield break;
	}

	// Token: 0x06006D11 RID: 27921 RVA: 0x002333F0 File Offset: 0x002315F0
	public IEnumerator UpdateLocationOSX(int maxTime)
	{
		int timeSpent = 0;
		LocationServices.StartSearching();
		while (timeSpent < maxTime)
		{
			GpsCoordinate gpsCoordinate = new GpsCoordinate();
			string locationData = LocationServices.GetLocationData();
			if (string.IsNullOrEmpty(locationData))
			{
				yield return new WaitForSeconds(1f);
			}
			else
			{
				string[] array = locationData.Split(new char[]
				{
					';'
				});
				if (array.Length != 3)
				{
					Log.FiresideGatherings.PrintWarning("Invalid OSX location data string: \"{0}\"", new object[]
					{
						locationData
					});
					yield return new WaitForSeconds(1f);
				}
				else
				{
					double latitude = 0.0;
					double longitude = 0.0;
					double maxValue = double.MaxValue;
					if (!double.TryParse(array[0], out latitude) || !double.TryParse(array[1], out longitude) || !double.TryParse(array[2], out maxValue))
					{
						Log.FiresideGatherings.PrintWarning("Invalid OSX location data string: \"{0}\"", new object[]
						{
							locationData
						});
						yield return new WaitForSeconds(1f);
					}
					else
					{
						gpsCoordinate.Latitude = latitude;
						gpsCoordinate.Longitude = longitude;
						gpsCoordinate.Accuracy = (double)((float)maxValue);
						gpsCoordinate.Timestamp = TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
						if (gpsCoordinate.Accuracy < this.m_bestLocation.Accuracy)
						{
							this.m_bestLocation = gpsCoordinate;
						}
						this.m_lastKnownLocation = gpsCoordinate;
						if (this.m_bestLocation.Accuracy <= (double)this.m_stoppingAccuracy)
						{
							break;
						}
						yield return new WaitForSeconds(1f);
						int num = timeSpent + 1;
						timeSpent = num;
					}
				}
			}
		}
		LocationServices.StopSearching();
		this.m_isQueryingLocation = false;
		yield break;
	}

	// Token: 0x06006D12 RID: 27922 RVA: 0x00233406 File Offset: 0x00231606
	public IEnumerator UpdateLocationMobile(int maxTime)
	{
		double locationStartTime = TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
		if (!ClientLocationManager.Get().GPSServicesEnabled)
		{
			Log.FiresideGatherings.PrintWarning("Location services not available to user!", Array.Empty<object>());
			this.m_isQueryingLocation = false;
			yield break;
		}
		Input.location.Stop();
		int timeSpent = 0;
		while (timeSpent < maxTime)
		{
			if (Input.location.status != LocationServiceStatus.Running)
			{
				Input.location.Start(0.1f, 0.1f);
				int timeoutLeft = (int)this.m_initializationTimeout;
				while (Input.location.status == LocationServiceStatus.Initializing && timeoutLeft > 0)
				{
					yield return new WaitForSeconds(1f);
					int num = timeoutLeft - 1;
					timeoutLeft = num;
					num = timeSpent + 1;
					timeSpent = num;
				}
				if (timeoutLeft < 1)
				{
					Log.FiresideGatherings.PrintError("LocationServices Timed out", Array.Empty<object>());
					this.m_isQueryingLocation = false;
					yield break;
				}
				if (Input.location.status == LocationServiceStatus.Failed)
				{
					Log.FiresideGatherings.PrintError("Unable to determine device location", Array.Empty<object>());
					this.m_isQueryingLocation = false;
					yield break;
				}
			}
			GpsCoordinate gpsCoordinate = Input.location.lastData;
			Input.location.Stop();
			if (gpsCoordinate.Timestamp < locationStartTime)
			{
				yield return new WaitForSeconds(1f);
			}
			else
			{
				if (gpsCoordinate.Accuracy < this.m_bestLocation.Accuracy)
				{
					this.m_bestLocation = gpsCoordinate;
				}
				this.m_lastKnownLocation = gpsCoordinate;
				if (this.m_bestLocation.Accuracy <= (double)this.m_stoppingAccuracy)
				{
					break;
				}
				Log.FiresideGatherings.Print("Best location updated with accuracy: " + this.m_bestLocation.Accuracy, Array.Empty<object>());
				yield return new WaitForSeconds(1f);
				int num = timeSpent + 1;
				timeSpent = num;
			}
		}
		this.m_isQueryingLocation = false;
		yield break;
	}

	// Token: 0x040057D3 RID: 22483
	public const int LOCATION_SCAN_TIMEOUT = 15;

	// Token: 0x040057D4 RID: 22484
	private float m_initializationTimeout = 20f;

	// Token: 0x040057D5 RID: 22485
	private float m_stoppingAccuracy = 80f;

	// Token: 0x040057D6 RID: 22486
	private GpsCoordinate m_lastKnownLocation;

	// Token: 0x040057D7 RID: 22487
	private GpsCoordinate m_bestLocation;

	// Token: 0x040057D8 RID: 22488
	private bool m_isQueryingLocation;
}
