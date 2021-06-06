using System;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Services;
using UnityEngine;

// Token: 0x02000856 RID: 2134
public interface IAssetLoader : IService
{
	// Token: 0x0600736F RID: 29551
	AssetHandle<T> LoadAsset<T>(AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None) where T : UnityEngine.Object;

	// Token: 0x06007370 RID: 29552
	bool LoadAsset<T>(ref AssetHandle<T> assetHandle, AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None) where T : UnityEngine.Object;

	// Token: 0x06007371 RID: 29553
	bool LoadAsset<T>(AssetReference assetRef, AssetHandleCallback<T> callback, object callbackData = null, AssetLoadingOptions options = AssetLoadingOptions.None) where T : UnityEngine.Object;

	// Token: 0x06007372 RID: 29554
	GameObject InstantiatePrefab(AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None);

	// Token: 0x06007373 RID: 29555
	bool InstantiatePrefab(AssetReference assetRef, PrefabCallback<GameObject> callback, object callbackData = null, AssetLoadingOptions options = AssetLoadingOptions.None);

	// Token: 0x06007374 RID: 29556
	AssetHandle<GameObject> GetOrInstantiateSharedPrefab(AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None);

	// Token: 0x06007375 RID: 29557
	bool GetOrInstantiateSharedPrefab(AssetReference assetRef, PrefabCallback<AssetHandle<GameObject>> callback, object callbackData = null, AssetLoadingOptions options = AssetLoadingOptions.None);

	// Token: 0x06007376 RID: 29558
	bool IsAssetAvailable(AssetReference assetRef);

	// Token: 0x06007377 RID: 29559
	bool IsAppropriateVariantAvailable(AssetReference assetRef, AssetLoadingOptions options);

	// Token: 0x06007378 RID: 29560
	void PopulateDebugStats(AssetManagerDebugStats stats, AssetManagerDebugStats.DataFields requestedFields);

	// Token: 0x06007379 RID: 29561
	bool IsWaitingOnObject(GameObject go);

	// Token: 0x0600737A RID: 29562
	bool IsSharedPrefabInstance(GameObject go);

	// Token: 0x0600737B RID: 29563
	bool LoadMaterial(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false);

	// Token: 0x0600737C RID: 29564
	Material LoadMaterial(AssetReference assetRef, bool persistent = false, bool disableLocalization = false);

	// Token: 0x0600737D RID: 29565
	bool LoadTexture(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false);

	// Token: 0x0600737E RID: 29566
	Texture LoadTexture(AssetReference assetRef, bool persistent = false, bool disableLocalization = false);

	// Token: 0x0600737F RID: 29567
	bool LoadMesh(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false);

	// Token: 0x06007380 RID: 29568
	Mesh LoadMesh(AssetReference assetRef, bool persistent = false, bool disableLocalization = false);

	// Token: 0x06007381 RID: 29569
	bool LoadGameObject(AssetReference assetRef, AssetLoader.GameObjectCallback callback, object callbackData = null, bool persistent = false, bool autoInstantiateOnLoad = true, bool usePrefabPosition = true);

	// Token: 0x06007382 RID: 29570
	UberShaderAnimation LoadUberAnimation(AssetReference assetRef, bool usePrefabPosition = true, bool persistent = false);
}
