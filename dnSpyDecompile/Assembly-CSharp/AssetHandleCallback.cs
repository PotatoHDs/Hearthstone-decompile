using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x02000854 RID: 2132
// (Invoke) Token: 0x06007368 RID: 29544
public delegate void AssetHandleCallback<T>(AssetReference assetRef, AssetHandle<T> asset, object callbackData) where T : UnityEngine.Object;
