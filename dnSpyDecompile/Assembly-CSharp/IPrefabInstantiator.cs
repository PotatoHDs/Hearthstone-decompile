using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000858 RID: 2136
public interface IPrefabInstantiator
{
	// Token: 0x14000078 RID: 120
	// (add) Token: 0x06007387 RID: 29575
	// (remove) Token: 0x06007388 RID: 29576
	event Action<string, string> OnSharedPrefabHandleOrphaned;

	// Token: 0x06007389 RID: 29577
	GameObject InstantiatePrefab(AssetHandle<GameObject> prefabAsset, AssetLoadingOptions options);

	// Token: 0x0600738A RID: 29578
	bool InstantiatePrefab(AssetHandle<GameObject> prefabAsset, PrefabInstantiatorCB<GameObject> callback, object callbackData, AssetLoadingOptions options);

	// Token: 0x0600738B RID: 29579
	AssetHandle<GameObject> GetOrInstantiateSharedPrefab(AssetHandle<GameObject> prefabAsset, AssetLoadingOptions options);

	// Token: 0x0600738C RID: 29580
	bool GetOrInstantiateSharedPrefab(AssetHandle<GameObject> prefabAsset, PrefabInstantiatorCB<AssetHandle<GameObject>> callback, object callbackData, AssetLoadingOptions options);

	// Token: 0x0600738D RID: 29581
	bool IsWaitingOnObject(GameObject go);

	// Token: 0x0600738E RID: 29582
	bool IsSharedPrefabInstance(GameObject go);

	// Token: 0x0600738F RID: 29583
	void ReleaseUnreferencedAssets();

	// Token: 0x06007390 RID: 29584
	void CheckForDeadHandles();
}
