using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

// Token: 0x0200085B RID: 2139
public static class SoundLoader
{
	// Token: 0x060073A2 RID: 29602 RVA: 0x00251E3C File Offset: 0x0025003C
	public static bool LoadSound(AssetReference assetRef, PrefabCallback<GameObject> callback, object callbackData = null, GameObject fallback = null)
	{
		SoundLoader.LoadSoundCallbackData callbackData2 = new SoundLoader.LoadSoundCallbackData
		{
			callback = callback,
			callbackData = callbackData,
			fallback = fallback
		};
		return AssetLoader.Get().InstantiatePrefab(assetRef, new PrefabCallback<GameObject>(SoundLoader.LoadSoundCallback), callbackData2, AssetLoadingOptions.None);
	}

	// Token: 0x060073A3 RID: 29603 RVA: 0x00251E80 File Offset: 0x00250080
	public static GameObject LoadSound(AssetReference assetRef)
	{
		if (assetRef == null)
		{
			Error.AddDevFatal("SoundLoader.LoadSound() - An asset request was made but no file name was given.", Array.Empty<object>());
			return null;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(assetRef, AssetLoadingOptions.None);
		if (!SoundLoader.LocalizeSoundPrefab(gameObject))
		{
			UnityEngine.Object.Destroy(gameObject);
			return null;
		}
		return gameObject;
	}

	// Token: 0x060073A4 RID: 29604 RVA: 0x00251EC0 File Offset: 0x002500C0
	private static bool LocalizeSoundPrefab(GameObject go)
	{
		if (go == null)
		{
			return false;
		}
		SoundDef component = go.GetComponent<SoundDef>();
		if (component == null)
		{
			Log.Asset.PrintInfo("LocalizeSoundPrefab: trying to load sound prefab with no SoundDef components: \"{0}\"", new object[]
			{
				go.name
			});
			return false;
		}
		if (string.IsNullOrEmpty(component.m_AudioClip))
		{
			Log.Asset.PrintInfo("LocalizeSoundPrefab: trying to load sound prefab with an SoundDef that contains no AudoClip: \"{0}\"", new object[]
			{
				go.name
			});
			return false;
		}
		AudioSource component2 = go.GetComponent<AudioSource>();
		AssetHandle<AudioClip> assetHandle = null;
		SoundLoader.LoadAudioClipWithFallback(ref assetHandle, component2, component.m_AudioClip);
		if (assetHandle != null)
		{
			DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
			if (disposablesCleaner != null)
			{
				disposablesCleaner.Attach(go, assetHandle);
			}
			component2.clip = assetHandle;
			return true;
		}
		return false;
	}

	// Token: 0x060073A5 RID: 29605 RVA: 0x00251F78 File Offset: 0x00250178
	public static void LoadAudioClipWithFallback(ref AssetHandle<AudioClip> clip, AudioSource source, AssetReference clipAsset)
	{
		AssetLoader.Get().LoadAsset<AudioClip>(ref clip, clipAsset, AssetLoadingOptions.None);
		if (clip == null)
		{
			source.volume = 0f;
			Log.Sound.PrintWarning("LoadAudioClipWithFallback failed to load {0}. Falling back to muted enUS asset", new object[]
			{
				(clipAsset != null) ? clipAsset.ToString() : null
			});
			AssetLoader.Get().LoadAsset<AudioClip>(ref clip, clipAsset, AssetLoadingOptions.DisableLocalization);
		}
		if (clip == null)
		{
			Log.Sound.PrintWarning("LoadAudioClipWithFallback failed to load enUS variant of {0}. Falling back to general fallback sound", new object[]
			{
				(clipAsset != null) ? clipAsset.ToString() : null
			});
			AssetLoader.Get().LoadAsset<AudioClip>(ref clip, SoundManager.FallbackSound, AssetLoadingOptions.None);
		}
	}

	// Token: 0x060073A6 RID: 29606 RVA: 0x00252010 File Offset: 0x00250210
	private static void LoadSoundCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		SoundLoader.LoadSoundCallbackData loadSoundCallbackData = (SoundLoader.LoadSoundCallbackData)callbackData;
		try
		{
			if (go == null && loadSoundCallbackData.fallback != null)
			{
				go = UnityEngine.Object.Instantiate<GameObject>(loadSoundCallbackData.fallback);
			}
			if (go != null && !SoundLoader.LocalizeSoundPrefab(go))
			{
				UnityEngine.Object.Destroy(go);
				go = null;
			}
			loadSoundCallbackData.callback(assetRef, go, loadSoundCallbackData.callbackData);
		}
		catch (Exception ex)
		{
			Error.AddDevFatal("LoadSoundCallback failed - assetRef={0}: {1}", new object[]
			{
				(assetRef != null) ? assetRef.ToString() : null,
				ex
			});
			loadSoundCallbackData.callback(assetRef, null, loadSoundCallbackData.callbackData);
		}
	}

	// Token: 0x02002458 RID: 9304
	private class LoadSoundCallbackData
	{
		// Token: 0x0400E9E4 RID: 59876
		public PrefabCallback<GameObject> callback;

		// Token: 0x0400E9E5 RID: 59877
		public object callbackData;

		// Token: 0x0400E9E6 RID: 59878
		public GameObject fallback;
	}
}
