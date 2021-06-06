using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using UnityEngine;

public class LocationServices : IService
{
	public const int LOCATION_SCAN_TIMEOUT = 15;

	private float m_initializationTimeout = 20f;

	private float m_stoppingAccuracy = 80f;

	private GpsCoordinate m_lastKnownLocation;

	private GpsCoordinate m_bestLocation;

	private bool m_isQueryingLocation;

	public bool IsAvailable
	{
		get
		{
			if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
			{
				return false;
			}
			return true;
		}
	}

	public bool IsReady
	{
		get
		{
			try
			{
				return GetReady();
			}
			catch (DllNotFoundException)
			{
				Log.FiresideGatherings.Print("Location API DLL not available");
				return true;
			}
			catch (Exception ex2)
			{
				Log.FiresideGatherings.PrintWarning("Couldn't check for device location services readiness.\n" + ex2.Message);
				return true;
			}
		}
	}

	public bool IsEnabled
	{
		get
		{
			try
			{
				if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
				{
					return GetEnabled();
				}
				if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
				{
					return HasPermission() == 1;
				}
				return MobilePermissionsManager.Get().CheckPermission(MobilePermission.FINE_LOCATION);
			}
			catch (DllNotFoundException)
			{
				Log.FiresideGatherings.Print("Location API DLL not available");
				return false;
			}
			catch (Exception ex2)
			{
				Log.FiresideGatherings.PrintWarning("Couldn't check for device location services availability.\n" + ex2.Message);
				return false;
			}
		}
	}

	public static void StartGeoSearch()
	{
		WindowsLocationAPI.StartGeoSearch();
	}

	public static double GetLatitude()
	{
		return WindowsLocationAPI.GetLatitude();
	}

	public static double GetLongitude()
	{
		return WindowsLocationAPI.GetLongitude();
	}

	public static double GetVerticalAccuracy()
	{
		return WindowsLocationAPI.GetVerticalAccuracy();
	}

	public static double GetHorizontalAccuracy()
	{
		return WindowsLocationAPI.GetHorizontalAccuracy();
	}

	public static bool GetEnabled()
	{
		return WindowsLocationAPI.GetEnabled();
	}

	public static bool GetReady()
	{
		return WindowsLocationAPI.GetReady();
	}

	public static void StartSearching()
	{
	}

	public static void StopSearching()
	{
	}

	public static int HasPermission()
	{
		return 0;
	}

	public static string GetLocationData()
	{
		return null;
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		yield break;
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
	}

	public GpsCoordinate GetLastKnownLocation()
	{
		return m_lastKnownLocation;
	}

	public GpsCoordinate GetBestLocation()
	{
		return m_bestLocation;
	}

	public bool IsQueryingLocation()
	{
		return m_isQueryingLocation;
	}

	public IEnumerator UpdateLocation(int maxTime = 15)
	{
		m_lastKnownLocation = new GpsCoordinate();
		m_bestLocation = new GpsCoordinate();
		m_isQueryingLocation = true;
		if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
		{
			yield return Processor.RunCoroutine(UpdateLocationWindows(maxTime));
		}
		else if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
		{
			yield return Processor.RunCoroutine(UpdateLocationOSX(maxTime));
		}
		else
		{
			yield return Processor.RunCoroutine(UpdateLocationMobile(maxTime));
		}
	}

	public IEnumerator UpdateLocationWindows(int maxTime)
	{
		int timeSpent = 0;
		while (timeSpent < maxTime)
		{
			StartGeoSearch();
			GpsCoordinate gpsCoordinate = new GpsCoordinate();
			gpsCoordinate.Latitude = GetLatitude();
			gpsCoordinate.Longitude = GetLongitude();
			gpsCoordinate.Accuracy = GetHorizontalAccuracy();
			gpsCoordinate.Timestamp = TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds;
			if (gpsCoordinate.Accuracy < m_bestLocation.Accuracy)
			{
				m_bestLocation = gpsCoordinate;
			}
			m_lastKnownLocation = gpsCoordinate;
			if (m_bestLocation.Accuracy <= (double)m_stoppingAccuracy)
			{
				break;
			}
			yield return new WaitForSeconds(1f);
			int num = timeSpent + 1;
			timeSpent = num;
		}
		m_isQueryingLocation = false;
	}

	public IEnumerator UpdateLocationOSX(int maxTime)
	{
		int timeSpent = 0;
		StartSearching();
		while (timeSpent < maxTime)
		{
			GpsCoordinate gpsCoordinate = new GpsCoordinate();
			string locationData = GetLocationData();
			if (string.IsNullOrEmpty(locationData))
			{
				yield return new WaitForSeconds(1f);
				continue;
			}
			string[] array = locationData.Split(';');
			if (array.Length != 3)
			{
				Log.FiresideGatherings.PrintWarning("Invalid OSX location data string: \"{0}\"", locationData);
				yield return new WaitForSeconds(1f);
				continue;
			}
			double result = 0.0;
			double result2 = 0.0;
			double result3 = double.MaxValue;
			if (!double.TryParse(array[0], out result) || !double.TryParse(array[1], out result2) || !double.TryParse(array[2], out result3))
			{
				Log.FiresideGatherings.PrintWarning("Invalid OSX location data string: \"{0}\"", locationData);
				yield return new WaitForSeconds(1f);
				continue;
			}
			gpsCoordinate.Latitude = result;
			gpsCoordinate.Longitude = result2;
			gpsCoordinate.Accuracy = (float)result3;
			gpsCoordinate.Timestamp = TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds;
			if (gpsCoordinate.Accuracy < m_bestLocation.Accuracy)
			{
				m_bestLocation = gpsCoordinate;
			}
			m_lastKnownLocation = gpsCoordinate;
			if (m_bestLocation.Accuracy <= (double)m_stoppingAccuracy)
			{
				break;
			}
			yield return new WaitForSeconds(1f);
			int num = timeSpent + 1;
			timeSpent = num;
		}
		StopSearching();
		m_isQueryingLocation = false;
	}

	public IEnumerator UpdateLocationMobile(int maxTime)
	{
		double locationStartTime = TimeUtils.GetElapsedTimeSinceEpoch().TotalSeconds;
		if (!ClientLocationManager.Get().GPSServicesEnabled)
		{
			Log.FiresideGatherings.PrintWarning("Location services not available to user!");
			m_isQueryingLocation = false;
			yield break;
		}
		Input.location.Stop();
		int timeSpent = 0;
		while (timeSpent < maxTime)
		{
			int num;
			if (Input.location.status != LocationServiceStatus.Running)
			{
				Input.location.Start(0.1f, 0.1f);
				int timeoutLeft = (int)m_initializationTimeout;
				while (Input.location.status == LocationServiceStatus.Initializing && timeoutLeft > 0)
				{
					yield return new WaitForSeconds(1f);
					num = timeoutLeft - 1;
					timeoutLeft = num;
					num = timeSpent + 1;
					timeSpent = num;
				}
				if (timeoutLeft < 1)
				{
					Log.FiresideGatherings.PrintError("LocationServices Timed out");
					m_isQueryingLocation = false;
					yield break;
				}
				if (Input.location.status == LocationServiceStatus.Failed)
				{
					Log.FiresideGatherings.PrintError("Unable to determine device location");
					m_isQueryingLocation = false;
					yield break;
				}
			}
			GpsCoordinate gpsCoordinate = Input.location.lastData;
			Input.location.Stop();
			if (gpsCoordinate.Timestamp < locationStartTime)
			{
				yield return new WaitForSeconds(1f);
				continue;
			}
			if (gpsCoordinate.Accuracy < m_bestLocation.Accuracy)
			{
				m_bestLocation = gpsCoordinate;
			}
			m_lastKnownLocation = gpsCoordinate;
			if (m_bestLocation.Accuracy <= (double)m_stoppingAccuracy)
			{
				break;
			}
			Log.FiresideGatherings.Print("Best location updated with accuracy: " + m_bestLocation.Accuracy);
			yield return new WaitForSeconds(1f);
			num = timeSpent + 1;
			timeSpent = num;
		}
		m_isQueryingLocation = false;
	}
}
