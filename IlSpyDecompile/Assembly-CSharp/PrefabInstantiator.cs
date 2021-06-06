using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Core;
using Hearthstone.Core;
using UnityEngine;

public class PrefabInstantiator : IPrefabInstantiator
{
	private class PendingRequest
	{
		public object callerData;

		public PrefabInstantiatorCB<AssetHandle<GameObject>> callerCallback;
	}

	private readonly Vector3 SPAWN_POS_CAMERA_OFFSET = new Vector3(0f, 0f, -5000f);

	private readonly List<GameObject> m_waitingOnObjects = new List<GameObject>();

	private readonly AssetHandleCollection m_sharedPrefabHandles;

	private readonly Dictionary<string, GameObject> m_sharedPrefabInstances = new Dictionary<string, GameObject>();

	private readonly Dictionary<string, List<PendingRequest>> m_pendingSharedInstanceRequests = new Dictionary<string, List<PendingRequest>>();

	private readonly Blizzard.T5.Core.ILogger m_logger;

	public event Action<string, string> OnSharedPrefabHandleOrphaned
	{
		add
		{
			m_sharedPrefabHandles.OnOrphanedHandleDetected += value;
		}
		remove
		{
			m_sharedPrefabHandles.OnOrphanedHandleDetected -= value;
		}
	}

	public PrefabInstantiator(Blizzard.T5.Core.ILogger logger)
	{
		m_logger = logger;
		m_sharedPrefabHandles = new AssetHandleCollection(logger);
		m_sharedPrefabHandles.OnLastHandleReleased += OnSharedPrefabReleased;
	}

	public GameObject InstantiatePrefab(AssetHandle<GameObject> prefabAsset, AssetLoadingOptions options)
	{
		if (!prefabAsset)
		{
			return null;
		}
		GameObject gameObject = (options.HasFlag(AssetLoadingOptions.IgnorePrefabPosition) ? ((GameObject)UnityEngine.Object.Instantiate((UnityEngine.Object)(GameObject)prefabAsset, NewGameObjectSpawnPosition(), prefabAsset.Asset.transform.rotation)) : ((GameObject)UnityEngine.Object.Instantiate((UnityEngine.Object)(GameObject)prefabAsset)));
		AssetHandle disposable = prefabAsset.Share();
		HearthstoneServices.Get<DisposablesCleaner>()?.Attach(gameObject, disposable);
		return gameObject;
	}

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
		Processor.RunCoroutine(InstantiateAndWaitThenCallGameObjectCallback(prefabAsset.Share(), options, callback, callbackData));
		return true;
	}

	public AssetHandle<GameObject> GetOrInstantiateSharedPrefab(AssetHandle<GameObject> prefabAsset, AssetLoadingOptions options)
	{
		if (!prefabAsset)
		{
			return null;
		}
		if (!TryGetSharedInstance(prefabAsset.AssetAddress, out var instance))
		{
			instance = InstantiatePrefab(prefabAsset, options);
			if (instance != null)
			{
				m_sharedPrefabInstances.Add(prefabAsset.AssetAddress, instance);
			}
		}
		if (instance == null)
		{
			return null;
		}
		AssetHandle<GameObject> assetHandle = new AssetHandle<GameObject>(prefabAsset.AssetAddress, instance);
		m_sharedPrefabHandles.StartTrackingHandle(assetHandle);
		return assetHandle;
	}

	public bool GetOrInstantiateSharedPrefab(AssetHandle<GameObject> prefabAsset, PrefabInstantiatorCB<AssetHandle<GameObject>> callback, object callbackData, AssetLoadingOptions options)
	{
		if (!prefabAsset)
		{
			return false;
		}
		if (TryGetSharedInstance(prefabAsset.AssetAddress, out var instance))
		{
			AssetHandle<GameObject> assetHandle = new AssetHandle<GameObject>(prefabAsset.AssetAddress, instance);
			m_sharedPrefabHandles.StartTrackingHandle(assetHandle);
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(prefabAsset.AssetAddress, assetHandle, callbackData);
			}
			return true;
		}
		PendingRequest item = new PendingRequest
		{
			callerCallback = callback,
			callerData = callbackData
		};
		if (m_pendingSharedInstanceRequests.TryGetValue(prefabAsset.AssetAddress, out var value))
		{
			value.Add(item);
			return true;
		}
		value = new List<PendingRequest> { item };
		m_pendingSharedInstanceRequests.Add(prefabAsset.AssetAddress, value);
		return InstantiatePrefab(prefabAsset, OnSharedPrefabInstantiated, null, options);
	}

	public bool IsWaitingOnObject(GameObject go)
	{
		return m_waitingOnObjects.Contains(go);
	}

	public bool IsSharedPrefabInstance(GameObject go)
	{
		if (!go)
		{
			return false;
		}
		foreach (GameObject value in m_sharedPrefabInstances.Values)
		{
			if (go == value)
			{
				return true;
			}
		}
		return false;
	}

	public void ReleaseUnreferencedAssets()
	{
		m_sharedPrefabHandles.ReleaseUnreferencedAssets();
	}

	public void CheckForDeadHandles()
	{
		m_sharedPrefabHandles.CheckForDeadHandles();
	}

	private IEnumerator InstantiateAndWaitThenCallGameObjectCallback(AssetHandle<GameObject> prefabHandle, AssetLoadingOptions options, PrefabInstantiatorCB<GameObject> callback, object callbackData)
	{
		using (prefabHandle)
		{
			GameObject instance = (options.HasFlag(AssetLoadingOptions.IgnorePrefabPosition) ? UnityEngine.Object.Instantiate(prefabHandle.Asset, NewGameObjectSpawnPosition(), prefabHandle.Asset.transform.rotation) : UnityEngine.Object.Instantiate(prefabHandle.Asset));
			HearthstoneServices.Get<DisposablesCleaner>()?.Attach(instance, prefabHandle.Share());
			m_waitingOnObjects.Add(instance);
			yield return new WaitForEndOfFrame();
			m_waitingOnObjects.Remove(instance);
			if (GeneralUtils.IsCallbackValid(callback))
			{
				callback(prefabHandle.AssetAddress, instance, callbackData);
			}
		}
	}

	private void OnSharedPrefabInstantiated(string prefabAddress, GameObject instance, object callbackData)
	{
		if (TryGetSharedInstance(prefabAddress, out var instance2))
		{
			UnityEngine.Object.Destroy(instance);
			instance = instance2;
		}
		else
		{
			m_sharedPrefabInstances.Add(prefabAddress, instance);
		}
		if (!m_pendingSharedInstanceRequests.TryGetValue(prefabAddress, out var _))
		{
			return;
		}
		foreach (PendingRequest item in m_pendingSharedInstanceRequests[prefabAddress])
		{
			AssetHandle<GameObject> assetHandle = new AssetHandle<GameObject>(prefabAddress, instance);
			m_sharedPrefabHandles.StartTrackingHandle(assetHandle);
			if (GeneralUtils.IsCallbackValid(item.callerCallback))
			{
				item.callerCallback(prefabAddress, assetHandle, item.callerData);
			}
		}
		m_pendingSharedInstanceRequests.Remove(prefabAddress);
	}

	private void OnSharedPrefabReleased(string prefabAddress)
	{
		if (TryGetSharedInstance(prefabAddress, out var instance))
		{
			UnityEngine.Object.Destroy(instance);
			m_sharedPrefabInstances.Remove(prefabAddress);
		}
	}

	private bool TryGetSharedInstance(string prefabAddress, out GameObject instance)
	{
		if (m_sharedPrefabInstances.TryGetValue(prefabAddress, out instance))
		{
			if (!instance)
			{
				instance = null;
				m_logger.Log(LogLevel.Warning, "PrefabInstantiator found destroyed shared instance. This is unexpected.", prefabAddress);
				m_sharedPrefabInstances.Remove(prefabAddress);
				return false;
			}
			return true;
		}
		return false;
	}

	private Vector3 NewGameObjectSpawnPosition()
	{
		if (Camera.main == null)
		{
			return Vector3.zero;
		}
		return Camera.main.transform.position + SPAWN_POS_CAMERA_OFFSET;
	}
}
