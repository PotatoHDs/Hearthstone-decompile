using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x02000940 RID: 2368
public class MobilePermissionsManager : IService
{
	// Token: 0x060082F2 RID: 33522 RVA: 0x002A7B27 File Offset: 0x002A5D27
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.InitAndroidPermissionStrings();
		bool isEditor = Application.isEditor;
		yield break;
	}

	// Token: 0x060082F3 RID: 33523 RVA: 0x002334B2 File Offset: 0x002316B2
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(MobileCallbackManager)
		};
	}

	// Token: 0x060082F4 RID: 33524 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void Shutdown()
	{
	}

	// Token: 0x060082F5 RID: 33525 RVA: 0x002A7B36 File Offset: 0x002A5D36
	public static MobilePermissionsManager Get()
	{
		return HearthstoneServices.Get<MobilePermissionsManager>();
	}

	// Token: 0x060082F6 RID: 33526 RVA: 0x002A7B3D File Offset: 0x002A5D3D
	public void RequestPermission(MobilePermission permission, MobilePermissionsManager.PermissionResultCallback callback)
	{
		callback(permission, false);
	}

	// Token: 0x060082F7 RID: 33527 RVA: 0x002A7B47 File Offset: 0x002A5D47
	private IEnumerator RequestLocationPermissionIOS(MobilePermissionsManager.PermissionResultCallback callback)
	{
		float elapsed = 0f;
		float interval = 0.1f;
		float timeout = 12f;
		bool granted = false;
		Input.location.Stop();
		Input.location.Start(0.1f, 0.1f);
		while (elapsed < timeout)
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
			elapsed += interval;
		}
		Input.location.Stop();
		callback(MobilePermission.FINE_LOCATION, granted);
		yield break;
	}

	// Token: 0x060082F8 RID: 33528 RVA: 0x002A7B56 File Offset: 0x002A5D56
	public bool CheckPermission(MobilePermission permission)
	{
		return this.CheckPermissionWindows(permission);
	}

	// Token: 0x060082F9 RID: 33529 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool WifiRequiresLocationPermission()
	{
		return false;
	}

	// Token: 0x060082FA RID: 33530 RVA: 0x000052EC File Offset: 0x000034EC
	public bool CheckPermissionWindows(MobilePermission permission)
	{
		return true;
	}

	// Token: 0x060082FB RID: 33531 RVA: 0x002A7B60 File Offset: 0x002A5D60
	public bool CheckPermissionAndroid(MobilePermission permission)
	{
		List<string> list;
		if (this.m_androidPermissionMap.TryGetValue(permission, out list))
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (!this.CheckPermissionAndroid(list[i]))
				{
					return false;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x060082FC RID: 33532 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool CheckPermissionAndroid(string permission)
	{
		return false;
	}

	// Token: 0x060082FD RID: 33533 RVA: 0x002A7BA4 File Offset: 0x002A5DA4
	public void OnPermissionResult(string result)
	{
		Debug.LogFormat("OnPermissionResult result={0}", new object[]
		{
			result
		});
		string[] array = result.Trim().Trim(new char[]
		{
			';'
		}).Split(new char[]
		{
			';'
		});
		if (array.Length < 2)
		{
			Debug.LogErrorFormat("OnPermissionResult, incorrectly formatted permission result: {0}", new object[]
			{
				result
			});
			return;
		}
		int num = 0;
		bool flag = int.TryParse(array[0], out num);
		MobilePermission mobilePermission = (MobilePermission)num;
		if (!flag || mobilePermission == MobilePermission.INVALID)
		{
			Debug.LogErrorFormat("OnPermissionResult, incorrectly formatted permission result: {0}", new object[]
			{
				result
			});
			return;
		}
		bool granted = true;
		for (int i = 1; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(new char[]
			{
				':'
			});
			if (array2.Length != 2)
			{
				Debug.LogErrorFormat("OnPermissionResult, incorrectly formatted permission token: {0}", new object[]
				{
					array[i]
				});
				granted = false;
				break;
			}
			int num2 = 0;
			if (!int.TryParse(array2[1], out num2) || num2 == 0)
			{
				granted = false;
			}
		}
		if (this.m_pendingRequests.ContainsKey(mobilePermission))
		{
			foreach (MobilePermissionsManager.PermissionResultCallback permissionResultCallback in this.m_pendingRequests[mobilePermission])
			{
				permissionResultCallback(mobilePermission, granted);
			}
			this.m_pendingRequests.Remove(mobilePermission);
		}
	}

	// Token: 0x060082FE RID: 33534 RVA: 0x002A7CF8 File Offset: 0x002A5EF8
	private void InitAndroidPermissionStrings()
	{
		this.m_androidPermissionMap[MobilePermission.FINE_LOCATION] = new List<string>
		{
			"android.permission.ACCESS_FINE_LOCATION"
		};
		this.m_androidPermissionMap[MobilePermission.COARSE_LOCATION] = new List<string>
		{
			"android.permission.ACCESS_COARSE_LOCATION"
		};
		this.m_androidPermissionMap[MobilePermission.BEACON] = new List<string>
		{
			"android.permission.ACCESS_COARSE_LOCATION"
		};
		this.m_androidPermissionMap[MobilePermission.WIFI] = new List<string>
		{
			"android.permission.ACCESS_NETWORK_STATE",
			"android.permission.ACCESS_WIFI_STATE"
		};
		this.m_androidPermissionMap[MobilePermission.BLUETOOTH] = new List<string>
		{
			"android.permission.BLUETOOTH",
			"android.permission.BLUETOOTH_ADMIN"
		};
		this.m_androidPermissionMap[MobilePermission.CAMERA] = new List<string>
		{
			"android.permission.CAMERA"
		};
		this.m_androidPermissionMap[MobilePermission.MICROPHONE] = new List<string>
		{
			"android.permission.RECORD_AUDIO"
		};
		this.m_androidPermissionMap[MobilePermission.GOOGLE_PUSH_NOTIFICATIONS] = new List<string>
		{
			"com.google.android.c2dm.permission.RECEIVE",
			"com.blizzard.wtcg.hearthstone.permission.C2D_MESSAGE"
		};
		this.m_androidPermissionMap[MobilePermission.AMAZON_PUSH_NOTIFICATIONS] = new List<string>
		{
			"com.blizzard.wtcg.hearthstone.permission.RECEIVE_ADM_MESSAGE",
			"com.amazon.device.messaging.permission.RECEIVE"
		};
	}

	// Token: 0x04006DA4 RID: 28068
	private Map<MobilePermission, List<string>> m_androidPermissionMap = new Map<MobilePermission, List<string>>();

	// Token: 0x04006DA5 RID: 28069
	private Map<MobilePermission, List<MobilePermissionsManager.PermissionResultCallback>> m_pendingRequests = new Map<MobilePermission, List<MobilePermissionsManager.PermissionResultCallback>>();

	// Token: 0x0200260B RID: 9739
	// (Invoke) Token: 0x06013570 RID: 79216
	public delegate void PermissionResultCallback(MobilePermission permission, bool granted);
}
