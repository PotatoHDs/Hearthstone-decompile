using Blizzard.T5.AssetManager;
using Blizzard.T5.Services;
using UnityEngine;

public interface IAssetLoader : IService
{
	AssetHandle<T> LoadAsset<T>(AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None) where T : Object;

	bool LoadAsset<T>(ref AssetHandle<T> assetHandle, AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None) where T : Object;

	bool LoadAsset<T>(AssetReference assetRef, AssetHandleCallback<T> callback, object callbackData = null, AssetLoadingOptions options = AssetLoadingOptions.None) where T : Object;

	GameObject InstantiatePrefab(AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None);

	bool InstantiatePrefab(AssetReference assetRef, PrefabCallback<GameObject> callback, object callbackData = null, AssetLoadingOptions options = AssetLoadingOptions.None);

	AssetHandle<GameObject> GetOrInstantiateSharedPrefab(AssetReference assetRef, AssetLoadingOptions options = AssetLoadingOptions.None);

	bool GetOrInstantiateSharedPrefab(AssetReference assetRef, PrefabCallback<AssetHandle<GameObject>> callback, object callbackData = null, AssetLoadingOptions options = AssetLoadingOptions.None);

	bool IsAssetAvailable(AssetReference assetRef);

	bool IsAppropriateVariantAvailable(AssetReference assetRef, AssetLoadingOptions options);

	void PopulateDebugStats(AssetManagerDebugStats stats, AssetManagerDebugStats.DataFields requestedFields);

	bool IsWaitingOnObject(GameObject go);

	bool IsSharedPrefabInstance(GameObject go);

	bool LoadMaterial(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false);

	Material LoadMaterial(AssetReference assetRef, bool persistent = false, bool disableLocalization = false);

	bool LoadTexture(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false);

	Texture LoadTexture(AssetReference assetRef, bool persistent = false, bool disableLocalization = false);

	bool LoadMesh(AssetReference assetRef, AssetLoader.ObjectCallback callback, object callbackData = null, bool persistent = false, bool disableLocalization = false);

	Mesh LoadMesh(AssetReference assetRef, bool persistent = false, bool disableLocalization = false);

	bool LoadGameObject(AssetReference assetRef, AssetLoader.GameObjectCallback callback, object callbackData = null, bool persistent = false, bool autoInstantiateOnLoad = true, bool usePrefabPosition = true);

	UberShaderAnimation LoadUberAnimation(AssetReference assetRef, bool usePrefabPosition = true, bool persistent = false);
}
