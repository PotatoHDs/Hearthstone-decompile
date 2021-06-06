using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

public static class SoundLoader
{
	private class LoadSoundCallbackData
	{
		public PrefabCallback<GameObject> callback;

		public object callbackData;

		public GameObject fallback;
	}

	public static bool LoadSound(AssetReference assetRef, PrefabCallback<GameObject> callback, object callbackData = null, GameObject fallback = null)
	{
		LoadSoundCallbackData callbackData2 = new LoadSoundCallbackData
		{
			callback = callback,
			callbackData = callbackData,
			fallback = fallback
		};
		return AssetLoader.Get().InstantiatePrefab(assetRef, LoadSoundCallback, callbackData2);
	}

	public static GameObject LoadSound(AssetReference assetRef)
	{
		if (assetRef == null)
		{
			Error.AddDevFatal("SoundLoader.LoadSound() - An asset request was made but no file name was given.");
			return null;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(assetRef);
		if (!LocalizeSoundPrefab(gameObject))
		{
			UnityEngine.Object.Destroy(gameObject);
			return null;
		}
		return gameObject;
	}

	private static bool LocalizeSoundPrefab(GameObject go)
	{
		if (go == null)
		{
			return false;
		}
		SoundDef component = go.GetComponent<SoundDef>();
		if (component == null)
		{
			Log.Asset.PrintInfo("LocalizeSoundPrefab: trying to load sound prefab with no SoundDef components: \"{0}\"", go.name);
			return false;
		}
		if (string.IsNullOrEmpty(component.m_AudioClip))
		{
			Log.Asset.PrintInfo("LocalizeSoundPrefab: trying to load sound prefab with an SoundDef that contains no AudoClip: \"{0}\"", go.name);
			return false;
		}
		AudioSource component2 = go.GetComponent<AudioSource>();
		AssetHandle<AudioClip> clip = null;
		LoadAudioClipWithFallback(ref clip, component2, component.m_AudioClip);
		if (clip != null)
		{
			HearthstoneServices.Get<DisposablesCleaner>()?.Attach(go, clip);
			component2.clip = clip;
			return true;
		}
		return false;
	}

	public static void LoadAudioClipWithFallback(ref AssetHandle<AudioClip> clip, AudioSource source, AssetReference clipAsset)
	{
		AssetLoader.Get().LoadAsset(ref clip, clipAsset);
		if (clip == null)
		{
			source.volume = 0f;
			Log.Sound.PrintWarning("LoadAudioClipWithFallback failed to load {0}. Falling back to muted enUS asset", clipAsset?.ToString());
			AssetLoader.Get().LoadAsset(ref clip, clipAsset, AssetLoadingOptions.DisableLocalization);
		}
		if (clip == null)
		{
			Log.Sound.PrintWarning("LoadAudioClipWithFallback failed to load enUS variant of {0}. Falling back to general fallback sound", clipAsset?.ToString());
			AssetLoader.Get().LoadAsset(ref clip, SoundManager.FallbackSound);
		}
	}

	private static void LoadSoundCallback(AssetReference assetRef, GameObject go, object callbackData)
	{
		LoadSoundCallbackData loadSoundCallbackData = (LoadSoundCallbackData)callbackData;
		try
		{
			if (go == null && loadSoundCallbackData.fallback != null)
			{
				go = UnityEngine.Object.Instantiate(loadSoundCallbackData.fallback);
			}
			if (go != null && !LocalizeSoundPrefab(go))
			{
				UnityEngine.Object.Destroy(go);
				go = null;
			}
			loadSoundCallbackData.callback(assetRef, go, loadSoundCallbackData.callbackData);
		}
		catch (Exception ex)
		{
			Error.AddDevFatal("LoadSoundCallback failed - assetRef={0}: {1}", assetRef?.ToString(), ex);
			loadSoundCallbackData.callback(assetRef, null, loadSoundCallbackData.callbackData);
		}
	}
}
