using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

public interface IPrefabInstantiator
{
	event Action<string, string> OnSharedPrefabHandleOrphaned;

	GameObject InstantiatePrefab(AssetHandle<GameObject> prefabAsset, AssetLoadingOptions options);

	bool InstantiatePrefab(AssetHandle<GameObject> prefabAsset, PrefabInstantiatorCB<GameObject> callback, object callbackData, AssetLoadingOptions options);

	AssetHandle<GameObject> GetOrInstantiateSharedPrefab(AssetHandle<GameObject> prefabAsset, AssetLoadingOptions options);

	bool GetOrInstantiateSharedPrefab(AssetHandle<GameObject> prefabAsset, PrefabInstantiatorCB<AssetHandle<GameObject>> callback, object callbackData, AssetLoadingOptions options);

	bool IsWaitingOnObject(GameObject go);

	bool IsSharedPrefabInstance(GameObject go);

	void ReleaseUnreferencedAssets();

	void CheckForDeadHandles();
}
