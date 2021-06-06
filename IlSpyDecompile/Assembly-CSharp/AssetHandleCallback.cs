using Blizzard.T5.AssetManager;
using UnityEngine;

public delegate void AssetHandleCallback<T>(AssetReference assetRef, AssetHandle<T> asset, object callbackData) where T : Object;
