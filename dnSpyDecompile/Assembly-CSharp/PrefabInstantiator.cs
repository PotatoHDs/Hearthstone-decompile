using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Core;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x02000859 RID: 2137
public class PrefabInstantiator : IPrefabInstantiator
{
	// Token: 0x06007391 RID: 29585 RVA: 0x0025196C File Offset: 0x0024FB6C
	public PrefabInstantiator(Blizzard.T5.Core.ILogger logger)
	{
		this.m_logger = logger;
		this.m_sharedPrefabHandles = new AssetHandleCollection(logger);
		this.m_sharedPrefabHandles.OnLastHandleReleased += this.OnSharedPrefabReleased;
	}

	// Token: 0x14000079 RID: 121
	// (add) Token: 0x06007392 RID: 29586 RVA: 0x002519E4 File Offset: 0x0024FBE4
	// (remove) Token: 0x06007393 RID: 29587 RVA: 0x002519F2 File Offset: 0x0024FBF2
	public event Action<string, string> OnSharedPrefabHandleOrphaned
	{
		add
		{
			this.m_sharedPrefabHandles.OnOrphanedHandleDetected += value;
		}
		remove
		{
			this.m_sharedPrefabHandles.OnOrphanedHandleDetected -= value;
		}
	}

	// Token: 0x06007394 RID: 29588 RVA: 0x00251A00 File Offset: 0x0024FC00
	public GameObject InstantiatePrefab(AssetHandle<GameObject> prefabAsset, AssetLoadingOptions options)
	{
		if (!prefabAsset)
		{
			return null;
		}
		GameObject gameObject = options.HasFlag(AssetLoadingOptions.IgnorePrefabPosition) ? ((GameObject)UnityEngine.Object.Instantiate(prefabAsset, this.NewGameObjectSpawnPosition(), prefabAsset.Asset.transform.rotation)) : ((GameObject)UnityEngine.Object.Instantiate(prefabAsset));
		AssetHandle disposable = prefabAsset.Share();
		DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
		if (disposablesCleaner != null)
		{
			disposablesCleaner.Attach(gameObject, disposable);
		}
		return gameObject;
	}

	// Token: 0x06007395 RID: 29589 RVA: 0x00251A7D File Offset: 0x0024FC7D
	public bool InstantiatePrefab(AssetHandle<GameObject> prefabAsset, PrefabInstantiatorCB<GameObject> callback, object callbackData, AssetLoadingOptions options)
	{
		if (!prefabAsset)
		{
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(null, null, callbackData);
			}
			return false;
		}
		Processor.RunCoroutine(this.InstantiateAndWaitThenCallGameObjectCallback(prefabAsset.Share(), options, callback, callbackData), null);
		return true;
	}

	// Token: 0x06007396 RID: 29590 RVA: 0x00251AB4 File Offset: 0x0024FCB4
	public AssetHandle<GameObject> GetOrInstantiateSharedPrefab(AssetHandle<GameObject> prefabAsset, AssetLoadingOptions options)
	{
		if (!prefabAsset)
		{
			return null;
		}
		GameObject gameObject;
		if (!this.TryGetSharedInstance(prefabAsset.AssetAddress, out gameObject))
		{
			gameObject = this.InstantiatePrefab(prefabAsset, options);
			if (gameObject != null)
			{
				this.m_sharedPrefabInstances.Add(prefabAsset.AssetAddress, gameObject);
			}
		}
		if (gameObject == null)
		{
			return null;
		}
		AssetHandle<GameObject> assetHandle = new AssetHandle<GameObject>(prefabAsset.AssetAddress, gameObject);
		this.m_sharedPrefabHandles.StartTrackingHandle(assetHandle, null);
		return assetHandle;
	}

	// Token: 0x06007397 RID: 29591 RVA: 0x00251B28 File Offset: 0x0024FD28
	public bool GetOrInstantiateSharedPrefab(AssetHandle<GameObject> prefabAsset, PrefabInstantiatorCB<AssetHandle<GameObject>> callback, object callbackData, AssetLoadingOptions options)
	{
		if (!prefabAsset)
		{
			return false;
		}
		GameObject asset;
		if (this.TryGetSharedInstance(prefabAsset.AssetAddress, out asset))
		{
			AssetHandle<GameObject> assetHandle = new AssetHandle<GameObject>(prefabAsset.AssetAddress, asset);
			this.m_sharedPrefabHandles.StartTrackingHandle(assetHandle, null);
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(prefabAsset.AssetAddress, assetHandle, callbackData);
			}
			return true;
		}
		PrefabInstantiator.PendingRequest item = new PrefabInstantiator.PendingRequest
		{
			callerCallback = callback,
			callerData = callbackData
		};
		List<PrefabInstantiator.PendingRequest> list;
		if (this.m_pendingSharedInstanceRequests.TryGetValue(prefabAsset.AssetAddress, out list))
		{
			list.Add(item);
			return true;
		}
		list = new List<PrefabInstantiator.PendingRequest>
		{
			item
		};
		this.m_pendingSharedInstanceRequests.Add(prefabAsset.AssetAddress, list);
		return this.InstantiatePrefab(prefabAsset, new PrefabInstantiatorCB<GameObject>(this.OnSharedPrefabInstantiated), null, options);
	}

	// Token: 0x06007398 RID: 29592 RVA: 0x00251BE8 File Offset: 0x0024FDE8
	public bool IsWaitingOnObject(GameObject go)
	{
		return this.m_waitingOnObjects.Contains(go);
	}

	// Token: 0x06007399 RID: 29593 RVA: 0x00251BF8 File Offset: 0x0024FDF8
	public bool IsSharedPrefabInstance(GameObject go)
	{
		if (!go)
		{
			return false;
		}
		foreach (GameObject y in this.m_sharedPrefabInstances.Values)
		{
			if (go == y)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600739A RID: 29594 RVA: 0x00251C64 File Offset: 0x0024FE64
	public void ReleaseUnreferencedAssets()
	{
		this.m_sharedPrefabHandles.ReleaseUnreferencedAssets();
	}

	// Token: 0x0600739B RID: 29595 RVA: 0x00251C71 File Offset: 0x0024FE71
	public void CheckForDeadHandles()
	{
		this.m_sharedPrefabHandles.CheckForDeadHandles();
	}

	// Token: 0x0600739C RID: 29596 RVA: 0x00251C7E File Offset: 0x0024FE7E
	private IEnumerator InstantiateAndWaitThenCallGameObjectCallback(AssetHandle<GameObject> prefabHandle, AssetLoadingOptions options, PrefabInstantiatorCB<GameObject> callback, object callbackData)
	{
		using (prefabHandle)
		{
			GameObject instance = options.HasFlag(AssetLoadingOptions.IgnorePrefabPosition) ? UnityEngine.Object.Instantiate<GameObject>(prefabHandle.Asset, this.NewGameObjectSpawnPosition(), prefabHandle.Asset.transform.rotation) : UnityEngine.Object.Instantiate<GameObject>(prefabHandle.Asset);
			DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
			if (disposablesCleaner != null)
			{
				disposablesCleaner.Attach(instance, prefabHandle.Share());
			}
			this.m_waitingOnObjects.Add(instance);
			yield return new WaitForEndOfFrame();
			this.m_waitingOnObjects.Remove(instance);
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(prefabHandle.AssetAddress, instance, callbackData);
			}
			instance = null;
		}
		AssetHandle<GameObject> assetHandle = null;
		yield break;
		yield break;
	}

	// Token: 0x0600739D RID: 29597 RVA: 0x00251CAC File Offset: 0x0024FEAC
	private void OnSharedPrefabInstantiated(string prefabAddress, GameObject instance, object callbackData)
	{
		GameObject gameObject;
		if (this.TryGetSharedInstance(prefabAddress, out gameObject))
		{
			UnityEngine.Object.Destroy(instance);
			instance = gameObject;
		}
		else
		{
			this.m_sharedPrefabInstances.Add(prefabAddress, instance);
		}
		List<PrefabInstantiator.PendingRequest> list;
		if (this.m_pendingSharedInstanceRequests.TryGetValue(prefabAddress, out list))
		{
			foreach (PrefabInstantiator.PendingRequest pendingRequest in this.m_pendingSharedInstanceRequests[prefabAddress])
			{
				AssetHandle<GameObject> assetHandle = new AssetHandle<GameObject>(prefabAddress, instance);
				this.m_sharedPrefabHandles.StartTrackingHandle(assetHandle, null);
				if (GeneralUtils.IsCallbackValid(pendingRequest.callerCallback))
				{
					pendingRequest.callerCallback(prefabAddress, assetHandle, pendingRequest.callerData);
				}
			}
			this.m_pendingSharedInstanceRequests.Remove(prefabAddress);
		}
	}

	// Token: 0x0600739E RID: 29598 RVA: 0x00251D78 File Offset: 0x0024FF78
	private void OnSharedPrefabReleased(string prefabAddress)
	{
		GameObject obj;
		if (this.TryGetSharedInstance(prefabAddress, out obj))
		{
			UnityEngine.Object.Destroy(obj);
			this.m_sharedPrefabInstances.Remove(prefabAddress);
		}
	}

	// Token: 0x0600739F RID: 29599 RVA: 0x00251DA4 File Offset: 0x0024FFA4
	private bool TryGetSharedInstance(string prefabAddress, out GameObject instance)
	{
		if (!this.m_sharedPrefabInstances.TryGetValue(prefabAddress, out instance))
		{
			return false;
		}
		if (!instance)
		{
			instance = null;
			this.m_logger.Log(LogLevel.Warning, "PrefabInstantiator found destroyed shared instance. This is unexpected.", new object[]
			{
				prefabAddress
			});
			this.m_sharedPrefabInstances.Remove(prefabAddress);
			return false;
		}
		return true;
	}

	// Token: 0x060073A0 RID: 29600 RVA: 0x00251DF9 File Offset: 0x0024FFF9
	private Vector3 NewGameObjectSpawnPosition()
	{
		if (Camera.main == null)
		{
			return Vector3.zero;
		}
		return Camera.main.transform.position + this.SPAWN_POS_CAMERA_OFFSET;
	}

	// Token: 0x04005BCB RID: 23499
	private readonly Vector3 SPAWN_POS_CAMERA_OFFSET = new Vector3(0f, 0f, -5000f);

	// Token: 0x04005BCC RID: 23500
	private readonly List<GameObject> m_waitingOnObjects = new List<GameObject>();

	// Token: 0x04005BCD RID: 23501
	private readonly AssetHandleCollection m_sharedPrefabHandles;

	// Token: 0x04005BCE RID: 23502
	private readonly Dictionary<string, GameObject> m_sharedPrefabInstances = new Dictionary<string, GameObject>();

	// Token: 0x04005BCF RID: 23503
	private readonly Dictionary<string, List<PrefabInstantiator.PendingRequest>> m_pendingSharedInstanceRequests = new Dictionary<string, List<PrefabInstantiator.PendingRequest>>();

	// Token: 0x04005BD0 RID: 23504
	private readonly Blizzard.T5.Core.ILogger m_logger;

	// Token: 0x02002455 RID: 9301
	private class PendingRequest
	{
		// Token: 0x0400E9D9 RID: 59865
		public object callerData;

		// Token: 0x0400E9DA RID: 59866
		public PrefabInstantiatorCB<AssetHandle<GameObject>> callerCallback;
	}
}
