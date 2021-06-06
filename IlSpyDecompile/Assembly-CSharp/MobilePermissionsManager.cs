using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

public class MobilePermissionsManager : IService
{
	public delegate void PermissionResultCallback(MobilePermission permission, bool granted);

	private Map<MobilePermission, List<string>> m_androidPermissionMap = new Map<MobilePermission, List<string>>();

	private Map<MobilePermission, List<PermissionResultCallback>> m_pendingRequests = new Map<MobilePermission, List<PermissionResultCallback>>();

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		InitAndroidPermissionStrings();
		_ = Application.isEditor;
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(MobileCallbackManager) };
	}

	public void Shutdown()
	{
	}

	public static MobilePermissionsManager Get()
	{
		return HearthstoneServices.Get<MobilePermissionsManager>();
	}

	public void RequestPermission(MobilePermission permission, PermissionResultCallback callback)
	{
		callback(permission, granted: false);
	}

	private IEnumerator RequestLocationPermissionIOS(PermissionResultCallback callback)
	{
		float elapsed = 0f;
		float interval = 0.1f;
		float timeout = 12f;
		bool granted = false;
		Input.location.Stop();
		Input.location.Start(0.1f, 0.1f);
		for (; elapsed < timeout; elapsed += interval)
		{
			yield return new WaitForSeconds(interval);
			Debug.Log("RequestingLocationServices! " + Input.location.status);
			if (Input.location.status == LocationServiceStatus.Failed)
			{
				Debug.Log("location services failed..");
				granted = false;
				break;
			}
			if (Input.location.status == LocationServiceStatus.Running)
			{
				Debug.Log("location services running..");
				granted = true;
				break;
			}
		}
		Input.location.Stop();
		callback(MobilePermission.FINE_LOCATION, granted);
	}

	public bool CheckPermission(MobilePermission permission)
	{
		return CheckPermissionWindows(permission);
	}

	public bool WifiRequiresLocationPermission()
	{
		return false;
	}

	public bool CheckPermissionWindows(MobilePermission permission)
	{
		return true;
	}

	public bool CheckPermissionAndroid(MobilePermission permission)
	{
		if (m_androidPermissionMap.TryGetValue(permission, out var value))
		{
			for (int i = 0; i < value.Count; i++)
			{
				if (!CheckPermissionAndroid(value[i]))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	public bool CheckPermissionAndroid(string permission)
	{
		return false;
	}

	public void OnPermissionResult(string result)
	{
		Debug.LogFormat("OnPermissionResult result={0}", result);
		string[] array = result.Trim().Trim(';').Split(';');
		if (array.Length < 2)
		{
			Debug.LogErrorFormat("OnPermissionResult, incorrectly formatted permission result: {0}", result);
			return;
		}
		int result2 = 0;
		bool num = int.TryParse(array[0], out result2);
		MobilePermission mobilePermission = (MobilePermission)result2;
		if (!num || mobilePermission == MobilePermission.INVALID)
		{
			Debug.LogErrorFormat("OnPermissionResult, incorrectly formatted permission result: {0}", result);
			return;
		}
		bool granted = true;
		for (int i = 1; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(':');
			if (array2.Length != 2)
			{
				Debug.LogErrorFormat("OnPermissionResult, incorrectly formatted permission token: {0}", array[i]);
				granted = false;
				break;
			}
			int result3 = 0;
			if (!int.TryParse(array2[1], out result3) || result3 == 0)
			{
				granted = false;
			}
		}
		if (!m_pendingRequests.ContainsKey(mobilePermission))
		{
			return;
		}
		foreach (PermissionResultCallback item in m_pendingRequests[mobilePermission])
		{
			item(mobilePermission, granted);
		}
		m_pendingRequests.Remove(mobilePermission);
	}

	private void InitAndroidPermissionStrings()
	{
		m_androidPermissionMap[MobilePermission.FINE_LOCATION] = new List<string> { "android.permission.ACCESS_FINE_LOCATION" };
		m_androidPermissionMap[MobilePermission.COARSE_LOCATION] = new List<string> { "android.permission.ACCESS_COARSE_LOCATION" };
		m_androidPermissionMap[MobilePermission.BEACON] = new List<string> { "android.permission.ACCESS_COARSE_LOCATION" };
		m_androidPermissionMap[MobilePermission.WIFI] = new List<string> { "android.permission.ACCESS_NETWORK_STATE", "android.permission.ACCESS_WIFI_STATE" };
		m_androidPermissionMap[MobilePermission.BLUETOOTH] = new List<string> { "android.permission.BLUETOOTH", "android.permission.BLUETOOTH_ADMIN" };
		m_androidPermissionMap[MobilePermission.CAMERA] = new List<string> { "android.permission.CAMERA" };
		m_androidPermissionMap[MobilePermission.MICROPHONE] = new List<string> { "android.permission.RECORD_AUDIO" };
		m_androidPermissionMap[MobilePermission.GOOGLE_PUSH_NOTIFICATIONS] = new List<string> { "com.google.android.c2dm.permission.RECEIVE", "com.blizzard.wtcg.hearthstone.permission.C2D_MESSAGE" };
		m_androidPermissionMap[MobilePermission.AMAZON_PUSH_NOTIFICATIONS] = new List<string> { "com.blizzard.wtcg.hearthstone.permission.RECEIVE_ADM_MESSAGE", "com.amazon.device.messaging.permission.RECEIVE" };
	}
}
