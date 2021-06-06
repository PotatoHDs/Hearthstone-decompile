using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Assets;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x02000955 RID: 2389
public class SoundManager : IService, IHasFixedUpdate, IHasUpdate
{
	// Token: 0x1700077E RID: 1918
	// (get) Token: 0x0600833C RID: 33596 RVA: 0x002A86F3 File Offset: 0x002A68F3
	private GameObject SceneObject
	{
		get
		{
			if (this.m_sceneObject == null)
			{
				this.m_sceneObject = new GameObject("SoundManagerSceneObject", new Type[]
				{
					typeof(HSDontDestroyOnLoad)
				});
			}
			return this.m_sceneObject;
		}
	}

	// Token: 0x0600833D RID: 33597 RVA: 0x002A872C File Offset: 0x002A692C
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		InstantiatePrefab instantiateSoundConfigPrefab = new InstantiatePrefab("SoundConfig.prefab:cd41c731c777d4f468b79ffa365a9f94");
		yield return instantiateSoundConfigPrefab;
		this.SetConfig(instantiateSoundConfigPrefab.InstantiatedPrefab.GetComponent<SoundConfig>());
		Options.Get().RegisterChangedListener(Option.SOUND, new Options.ChangedCallback(this.OnMasterEnabledOptionChanged));
		Options.Get().RegisterChangedListener(Option.SOUND_VOLUME, new Options.ChangedCallback(this.OnMasterVolumeOptionChanged));
		Options.Get().RegisterChangedListener(Option.MUSIC, new Options.ChangedCallback(this.OnEnabledOptionChanged));
		Options.Get().RegisterChangedListener(Option.MUSIC_VOLUME, new Options.ChangedCallback(this.OnVolumeOptionChanged));
		Options.Get().RegisterChangedListener(Option.BACKGROUND_SOUND, new Options.ChangedCallback(this.OnBackgroundSoundOptionChanged));
		this.m_isMasterEnabled = Options.Get().GetBool(Option.SOUND);
		this.m_isMusicEnabled = Options.Get().GetBool(Option.MUSIC);
		this.SetMasterVolumeExponential();
		this.UpdateAppMute();
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.AddFocusChangedListener(new HearthstoneApplication.FocusChangedCallback(this.OnAppFocusChanged));
		}
		yield return new ServiceSoftDependency(typeof(SceneMgr), serviceLocator);
		SceneMgr sceneMgr;
		if (serviceLocator.TryGetService<SceneMgr>(out sceneMgr))
		{
			sceneMgr.RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		}
		yield break;
	}

	// Token: 0x0600833E RID: 33598 RVA: 0x002450CA File Offset: 0x002432CA
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(IAssetLoader)
		};
	}

	// Token: 0x0600833F RID: 33599 RVA: 0x002A8742 File Offset: 0x002A6942
	public void Shutdown()
	{
		SoundManager.s_instance = null;
	}

	// Token: 0x06008340 RID: 33600 RVA: 0x002A874A File Offset: 0x002A694A
	public void Update()
	{
		this.m_frame = (this.m_frame + 1U & uint.MaxValue);
		this.UpdateMusicAndAmbience();
	}

	// Token: 0x06008341 RID: 33601 RVA: 0x002A8762 File Offset: 0x002A6962
	public void FixedUpdate()
	{
		this.UpdateSources();
	}

	// Token: 0x06008342 RID: 33602 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	public float GetSecondsBetweenUpdates()
	{
		return 1f;
	}

	// Token: 0x06008343 RID: 33603 RVA: 0x002A876A File Offset: 0x002A696A
	public static SoundManager Get()
	{
		if (SoundManager.s_instance == null)
		{
			SoundManager.s_instance = HearthstoneServices.Get<SoundManager>();
		}
		return SoundManager.s_instance;
	}

	// Token: 0x06008344 RID: 33604 RVA: 0x002A8782 File Offset: 0x002A6982
	public SoundConfig GetConfig()
	{
		return this.m_config;
	}

	// Token: 0x06008345 RID: 33605 RVA: 0x002A878A File Offset: 0x002A698A
	public void SetConfig(SoundConfig config)
	{
		this.m_config = config;
	}

	// Token: 0x06008346 RID: 33606 RVA: 0x002A8793 File Offset: 0x002A6993
	public bool IsInitialized()
	{
		return this.m_config != null;
	}

	// Token: 0x06008347 RID: 33607 RVA: 0x002A87A4 File Offset: 0x002A69A4
	public GameObject GetPlaceholderSound()
	{
		AudioSource placeholderSource = this.GetPlaceholderSource();
		if (placeholderSource == null)
		{
			return null;
		}
		return placeholderSource.gameObject;
	}

	// Token: 0x06008348 RID: 33608 RVA: 0x002A87C9 File Offset: 0x002A69C9
	public AudioSource GetPlaceholderSource()
	{
		if (this.m_config == null)
		{
			return null;
		}
		if (HearthstoneApplication.IsInternal())
		{
			return this.m_config.m_PlaceholderSound;
		}
		return null;
	}

	// Token: 0x06008349 RID: 33609 RVA: 0x002A87EF File Offset: 0x002A69EF
	public SoundDef GetSoundDef(AudioSource source)
	{
		return source.gameObject.GetComponent<SoundDef>();
	}

	// Token: 0x0600834A RID: 33610 RVA: 0x002A87FC File Offset: 0x002A69FC
	private void SetMasterVolumeExponential()
	{
		AudioListener.volume = Mathf.Pow(Mathf.Clamp01(Options.Get().GetFloat(Option.SOUND_VOLUME)), 1.75f);
	}

	// Token: 0x0600834B RID: 33611 RVA: 0x002A881D File Offset: 0x002A6A1D
	public bool Play(AudioSource source, SoundDef oneShotDef = null, AudioClip oneShotClip = null, SoundManager.SoundOptions options = null)
	{
		return this.PlayImpl(source, oneShotDef, oneShotClip, options);
	}

	// Token: 0x0600834C RID: 33612 RVA: 0x002A882F File Offset: 0x002A6A2F
	public bool PlayOneShot(AudioSource source, SoundDef oneShotDef, float volume = 1f, SoundManager.SoundOptions options = null)
	{
		if (!this.PlayImpl(source, oneShotDef, null, options))
		{
			return false;
		}
		if (this.IsActive(source))
		{
			this.SetVolume(source, volume);
		}
		return true;
	}

	// Token: 0x0600834D RID: 33613 RVA: 0x002A8857 File Offset: 0x002A6A57
	public bool IsPlaying(AudioSource source)
	{
		return !(source == null) && source.isPlaying;
	}

	// Token: 0x0600834E RID: 33614 RVA: 0x002A886C File Offset: 0x002A6A6C
	public bool Pause(AudioSource source)
	{
		if (source == null)
		{
			return false;
		}
		if (this.IsPaused(source))
		{
			return false;
		}
		SoundManager.SourceExtension sourceExtension = this.RegisterExtension(source, null, null, false);
		if (sourceExtension == null)
		{
			return false;
		}
		sourceExtension.m_paused = true;
		this.UpdateSource(source, sourceExtension);
		source.Pause();
		return true;
	}

	// Token: 0x0600834F RID: 33615 RVA: 0x002A88B8 File Offset: 0x002A6AB8
	public bool IsPaused(AudioSource source)
	{
		if (source == null)
		{
			return false;
		}
		SoundManager.SourceExtension extension = this.GetExtension(source);
		return extension != null && extension.m_paused;
	}

	// Token: 0x06008350 RID: 33616 RVA: 0x002A88E3 File Offset: 0x002A6AE3
	public bool Stop(AudioSource source)
	{
		if (source == null)
		{
			return false;
		}
		if (!this.IsActive(source))
		{
			return false;
		}
		source.Stop();
		this.FinishSource(source);
		return true;
	}

	// Token: 0x06008351 RID: 33617 RVA: 0x002A8909 File Offset: 0x002A6B09
	public void Destroy(AudioSource source)
	{
		if (source == null)
		{
			return;
		}
		this.FinishSource(source);
	}

	// Token: 0x06008352 RID: 33618 RVA: 0x002A891C File Offset: 0x002A6B1C
	public void DestroyAll(Global.SoundCategory category)
	{
		List<AudioSource> list = new List<AudioSource>();
		for (int i = 0; i < this.m_generatedSources.Count; i++)
		{
			AudioSource audioSource = this.m_generatedSources[i];
			SoundDef component = audioSource.GetComponent<SoundDef>();
			if (component.m_Category == category && !component.m_persistPastGameEnd)
			{
				list.Add(audioSource);
			}
		}
		foreach (AudioSource source in list)
		{
			this.Destroy(source);
		}
	}

	// Token: 0x06008353 RID: 33619 RVA: 0x002A89B8 File Offset: 0x002A6BB8
	public bool IsActive(AudioSource source)
	{
		return !(source == null) && (this.IsPlaying(source) || this.IsPaused(source));
	}

	// Token: 0x06008354 RID: 33620 RVA: 0x002A89DC File Offset: 0x002A6BDC
	public bool IsPlaybackFinished(AudioSource source)
	{
		return !(source == null) && !(source.clip == null) && source.timeSamples >= source.clip.samples;
	}

	// Token: 0x06008355 RID: 33621 RVA: 0x002A8A10 File Offset: 0x002A6C10
	public float GetVolume(AudioSource source)
	{
		if (source == null)
		{
			return 1f;
		}
		SoundManager.SourceExtension sourceExtension = this.RegisterExtension(source, null, null, false);
		if (sourceExtension == null)
		{
			return 1f;
		}
		return sourceExtension.m_codeVolume;
	}

	// Token: 0x06008356 RID: 33622 RVA: 0x002A8A48 File Offset: 0x002A6C48
	public void SetVolume(AudioSource source, float volume)
	{
		if (source == null)
		{
			return;
		}
		SoundManager.SourceExtension sourceExtension = this.RegisterExtension(source, null, null, false);
		if (sourceExtension == null)
		{
			return;
		}
		sourceExtension.m_codeVolume = volume;
		this.UpdateVolume(source, sourceExtension);
	}

	// Token: 0x06008357 RID: 33623 RVA: 0x002A8A80 File Offset: 0x002A6C80
	public float GetPitch(AudioSource source)
	{
		if (source == null)
		{
			return 1f;
		}
		SoundManager.SourceExtension sourceExtension = this.RegisterExtension(source, null, null, false);
		if (sourceExtension == null)
		{
			return 1f;
		}
		return sourceExtension.m_codePitch;
	}

	// Token: 0x06008358 RID: 33624 RVA: 0x002A8AB8 File Offset: 0x002A6CB8
	public void SetPitch(AudioSource source, float pitch)
	{
		if (source == null)
		{
			return;
		}
		SoundManager.SourceExtension sourceExtension = this.RegisterExtension(source, null, null, false);
		if (sourceExtension == null)
		{
			return;
		}
		sourceExtension.m_codePitch = pitch;
		this.UpdatePitch(source, sourceExtension);
	}

	// Token: 0x06008359 RID: 33625 RVA: 0x002A8AED File Offset: 0x002A6CED
	public Global.SoundCategory GetCategory(AudioSource source)
	{
		if (source == null)
		{
			return Global.SoundCategory.NONE;
		}
		return this.GetDefFromSource(source).m_Category;
	}

	// Token: 0x0600835A RID: 33626 RVA: 0x002A8B08 File Offset: 0x002A6D08
	public void SetCategory(AudioSource source, Global.SoundCategory cat)
	{
		if (source == null)
		{
			return;
		}
		SoundDef soundDef = source.GetComponent<SoundDef>();
		if (soundDef != null)
		{
			if (soundDef.m_Category == cat)
			{
				return;
			}
		}
		else
		{
			soundDef = source.gameObject.AddComponent<SoundDef>();
		}
		soundDef.m_Category = cat;
		this.UpdateSource(source);
	}

	// Token: 0x0600835B RID: 33627 RVA: 0x002A8B53 File Offset: 0x002A6D53
	public bool Is3d(AudioSource source)
	{
		return !(source == null) && source.spatialBlend >= 1f;
	}

	// Token: 0x0600835C RID: 33628 RVA: 0x002A8B70 File Offset: 0x002A6D70
	public void Set3d(AudioSource source, bool enable)
	{
		if (source == null)
		{
			return;
		}
		source.spatialBlend = (enable ? 1f : 0f);
	}

	// Token: 0x0600835D RID: 33629 RVA: 0x002A8B91 File Offset: 0x002A6D91
	public AudioSource GetCurrentMusicTrack()
	{
		return this.m_currentMusicTrack;
	}

	// Token: 0x0600835E RID: 33630 RVA: 0x002A8B99 File Offset: 0x002A6D99
	public AudioSource GetCurrentAmbienceTrack()
	{
		return this.m_currentAmbienceTrack;
	}

	// Token: 0x0600835F RID: 33631 RVA: 0x002A8BA1 File Offset: 0x002A6DA1
	public bool Load(AssetReference assetRef)
	{
		return AssetLoader.Get().IsAssetAvailable(assetRef) && SoundLoader.LoadSound(assetRef, new PrefabCallback<GameObject>(this.OnLoadSoundLoaded), null, null);
	}

	// Token: 0x06008360 RID: 33632 RVA: 0x002A8BC6 File Offset: 0x002A6DC6
	public void LoadAndPlay(AssetReference assetRef)
	{
		this.LoadAndPlay(assetRef, null, 1f, null, null);
	}

	// Token: 0x06008361 RID: 33633 RVA: 0x002A8BD7 File Offset: 0x002A6DD7
	public void LoadAndPlay(AssetReference assetRef, float volume)
	{
		this.LoadAndPlay(assetRef, null, volume, null, null);
	}

	// Token: 0x06008362 RID: 33634 RVA: 0x002A8BE4 File Offset: 0x002A6DE4
	public void LoadAndPlay(AssetReference assetRef, GameObject parent)
	{
		this.LoadAndPlay(assetRef, parent, 1f, null, null);
	}

	// Token: 0x06008363 RID: 33635 RVA: 0x002A8BF5 File Offset: 0x002A6DF5
	public void LoadAndPlay(AssetReference assetRef, GameObject parent, float volume)
	{
		this.LoadAndPlay(assetRef, parent, volume, null, null);
	}

	// Token: 0x06008364 RID: 33636 RVA: 0x002A8C02 File Offset: 0x002A6E02
	public void LoadAndPlay(AssetReference assetRef, GameObject parent, float volume, SoundManager.LoadedCallback callback)
	{
		this.LoadAndPlay(assetRef, parent, volume, callback, null);
	}

	// Token: 0x06008365 RID: 33637 RVA: 0x002A8C10 File Offset: 0x002A6E10
	public void LoadAndPlay(AssetReference assetRef, GameObject parent, float volume, SoundManager.LoadedCallback callback, object callbackData)
	{
		if (string.IsNullOrEmpty(assetRef))
		{
			Log.Sound.PrintWarning("Missing assetref for LoadAndPlay().", Array.Empty<object>());
			if (callback != null)
			{
				callback(null, callbackData);
			}
			return;
		}
		SoundManager.SoundLoadContext soundLoadContext = new SoundManager.SoundLoadContext();
		soundLoadContext.Init(parent, volume, callback, callbackData);
		SoundLoader.LoadSound(assetRef, new PrefabCallback<GameObject>(this.OnLoadAndPlaySoundLoaded), soundLoadContext, this.GetPlaceholderSound());
	}

	// Token: 0x06008366 RID: 33638 RVA: 0x002A8C79 File Offset: 0x002A6E79
	public void PlayPreloaded(AudioSource source)
	{
		this.PlayPreloaded(source, null);
	}

	// Token: 0x06008367 RID: 33639 RVA: 0x002A8C83 File Offset: 0x002A6E83
	public void PlayPreloaded(AudioSource source, float volume)
	{
		this.PlayPreloaded(source, null, volume);
	}

	// Token: 0x06008368 RID: 33640 RVA: 0x002A8C8E File Offset: 0x002A6E8E
	public void PlayPreloaded(AudioSource source, GameObject parentObject)
	{
		this.PlayPreloaded(source, parentObject, 1f);
	}

	// Token: 0x06008369 RID: 33641 RVA: 0x002A8CA0 File Offset: 0x002A6EA0
	public void PlayPreloaded(AudioSource source, GameObject parentObject, float volume)
	{
		if (source == null)
		{
			UnityEngine.Debug.LogError("Preloaded audio source is null! Cannot play!");
			return;
		}
		SoundManager.SourceExtension sourceExtension = this.RegisterExtension(source, null, null, false);
		if (sourceExtension != null)
		{
			sourceExtension.m_codeVolume = volume;
		}
		this.InitSourceTransform(source, parentObject);
		this.m_generatedSources.Add(source);
		this.Play(source, null, null, null);
	}

	// Token: 0x0600836A RID: 33642 RVA: 0x002A8CF8 File Offset: 0x002A6EF8
	public AudioSource PlayClip(SoundPlayClipArgs args, bool createNewSource = true, SoundManager.SoundOptions options = null)
	{
		if (args == null || (args.m_def == null && args.m_forcedAudioClip == null))
		{
			UnityEngine.Debug.LogWarningFormat("PlayClip: using placeholder sound for audio clip: {0}", new object[]
			{
				(args != null) ? args.ToString() : ""
			});
			return this.PlayImpl(null, null, null, null);
		}
		AudioSource audioSource;
		if (createNewSource)
		{
			audioSource = this.GenerateAudioSource(args.m_templateSource, args.m_def);
		}
		else
		{
			audioSource = args.m_def.GetComponent<AudioSource>();
			if (audioSource != null)
			{
				this.m_generatedSources.Add(audioSource);
			}
			else
			{
				Log.Asset.PrintWarning("PlayClip: Loaded sound asset missing AudioSource. Generating new one...", Array.Empty<object>());
				audioSource = this.GenerateAudioSource(args.m_templateSource, args.m_def);
			}
		}
		if (args.m_forcedAudioClip != null)
		{
			audioSource.clip = args.m_forcedAudioClip;
		}
		if (args.m_volume != null)
		{
			audioSource.volume = args.m_volume.Value;
		}
		if (args.m_pitch != null)
		{
			audioSource.pitch = args.m_pitch.Value;
		}
		if (args.m_spatialBlend != null)
		{
			audioSource.spatialBlend = args.m_spatialBlend.Value;
		}
		if (args.m_category != null)
		{
			audioSource.GetComponent<SoundDef>().m_Category = args.m_category.Value;
		}
		this.InitSourceTransform(audioSource, args.m_parentObject);
		if (args.m_forcedAudioClip != null)
		{
			if (this.Play(audioSource, null, args.m_forcedAudioClip, null))
			{
				return audioSource;
			}
		}
		else if (this.Play(audioSource, args.m_def, null, options))
		{
			return audioSource;
		}
		this.FinishGeneratedSource(audioSource);
		return null;
	}

	// Token: 0x0600836B RID: 33643 RVA: 0x002A8E98 File Offset: 0x002A7098
	public bool LoadAndPlayClip(AssetReference assetRef, SoundPlayClipArgs args)
	{
		if (string.IsNullOrEmpty(assetRef))
		{
			Log.Sound.PrintError("LoadAndPlayClip: Missing asset AssetReference!", Array.Empty<object>());
			return false;
		}
		if (!AssetLoader.Get().IsAssetAvailable(assetRef))
		{
			return false;
		}
		if (args == null)
		{
			Log.Sound.PrintWarning("LoadAndPlayClip: Missing SoundPlayClipArgs. Using default...", Array.Empty<object>());
			args = new SoundPlayClipArgs
			{
				m_category = new Global.SoundCategory?(Global.SoundCategory.FX)
			};
		}
		return SoundLoader.LoadSound(assetRef, new PrefabCallback<GameObject>(this.OnLoadAndPlayClipLoaded), args, null);
	}

	// Token: 0x0600836C RID: 33644 RVA: 0x002A8F18 File Offset: 0x002A7118
	private void OnLoadSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (assetRef == null)
		{
			UnityEngine.Debug.LogErrorFormat("SoundManager.OnLoadSoundLoaded() - ERROR Tried to load null assetRef!", new object[]
			{
				assetRef,
				go
			});
			return;
		}
		if (go == null)
		{
			UnityEngine.Debug.LogErrorFormat("SoundManager.OnLoadSoundLoaded() - ERROR assetRef=\"{0}\" go=\"{1}\" failed to load", new object[]
			{
				assetRef,
				go
			});
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			UnityEngine.Object.DestroyImmediate(go);
			UnityEngine.Debug.LogErrorFormat("SoundManager.OnLoadSoundLoaded() - ERROR assetRef=\"{0}\" has no AudioSource", new object[]
			{
				assetRef
			});
			return;
		}
		this.RegisterSourceBundle(assetRef, component);
		component.volume = 0f;
		component.Play();
		component.Stop();
		this.UnregisterSourceBundle(assetRef.ToString(), component);
		UnityEngine.Object.DestroyImmediate(component.gameObject);
	}

	// Token: 0x0600836D RID: 33645 RVA: 0x002A8FC8 File Offset: 0x002A71C8
	private void OnLoadAndPlaySoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			UnityEngine.Debug.LogError(string.Format("SoundManager.OnLoadAndPlaySoundLoaded() - ERROR \"{0}\" failed to load", assetRef));
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			UnityEngine.Object.DestroyImmediate(go);
			UnityEngine.Debug.LogError(string.Format("SoundManager.OnLoadAndPlaySoundLoaded() - ERROR \"{0}\" has no AudioSource", assetRef));
			return;
		}
		SoundManager.SoundLoadContext soundLoadContext = (SoundManager.SoundLoadContext)callbackData;
		if (soundLoadContext.m_sceneMode != SceneMgr.Mode.FATAL_ERROR && SceneMgr.Get() != null && SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
		{
			UnityEngine.Object.DestroyImmediate(go);
			return;
		}
		if (this.RegisterSourceBundle(assetRef, component) == null)
		{
			UnityEngine.Debug.LogWarningFormat("Failed to load and play sound name={0}, go={1} (this may be due to it not yet being downloaded)", new object[]
			{
				assetRef,
				go.name
			});
			return;
		}
		if (soundLoadContext.m_haveCallback && !GeneralUtils.IsCallbackValid(soundLoadContext.m_callback))
		{
			UnityEngine.Object.DestroyImmediate(go);
			this.UnregisterSourceBundle(this.SceneObject.name, component);
			return;
		}
		this.m_generatedSources.Add(component);
		this.RegisterExtension(component, null, null, false).m_codeVolume = soundLoadContext.m_volume;
		this.InitSourceTransform(component, soundLoadContext.m_parent);
		this.Play(component, null, null, null);
		if (soundLoadContext.m_callback != null)
		{
			soundLoadContext.m_callback(component, soundLoadContext.m_userData);
		}
	}

	// Token: 0x0600836E RID: 33646 RVA: 0x002A90EC File Offset: 0x002A72EC
	private void OnLoadAndPlayClipLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Log.Sound.PrintError("LoadAndPlayClip: Sound asset \"{0}\" failed to load", new object[]
			{
				assetRef
			});
			return;
		}
		SoundDef component = go.GetComponent<SoundDef>();
		if (component == null)
		{
			Log.Sound.PrintError("LoadAndPlayClip: SoundDef missing from asset! Aborting playing \"{0}\"", new object[]
			{
				assetRef
			});
			UnityEngine.Object.DestroyImmediate(go);
			return;
		}
		SoundPlayClipArgs soundPlayClipArgs = (SoundPlayClipArgs)callbackData;
		soundPlayClipArgs.m_def = component;
		this.PlayClip(soundPlayClipArgs, false, null);
	}

	// Token: 0x0600836F RID: 33647 RVA: 0x002A9164 File Offset: 0x002A7364
	public void AddMusicTracks(List<MusicTrack> tracks)
	{
		this.AddTracks(tracks, this.m_musicTracks);
	}

	// Token: 0x06008370 RID: 33648 RVA: 0x002A9173 File Offset: 0x002A7373
	public void AddAmbienceTracks(List<MusicTrack> tracks)
	{
		this.AddTracks(tracks, this.m_ambienceTracks);
	}

	// Token: 0x06008371 RID: 33649 RVA: 0x002A9182 File Offset: 0x002A7382
	public List<MusicTrack> GetCurrentMusicTracks()
	{
		return this.m_musicTracks;
	}

	// Token: 0x06008372 RID: 33650 RVA: 0x002A918A File Offset: 0x002A738A
	public List<MusicTrack> GetCurrentAmbienceTracks()
	{
		return this.m_ambienceTracks;
	}

	// Token: 0x06008373 RID: 33651 RVA: 0x002A9192 File Offset: 0x002A7392
	public int GetCurrentMusicTrackIndex()
	{
		return this.m_musicTrackIndex;
	}

	// Token: 0x06008374 RID: 33652 RVA: 0x002A919A File Offset: 0x002A739A
	public void SetCurrentMusicTrackIndex(int idx)
	{
		if (this.m_musicTrackIndex != idx)
		{
			this.m_musicIsAboutToPlay = this.PlayMusicTrack(idx);
		}
	}

	// Token: 0x06008375 RID: 33653 RVA: 0x002A91B2 File Offset: 0x002A73B2
	public void SetCurrentMusicTrackTime(float time)
	{
		if (this.m_currentMusicTrack)
		{
			this.m_currentMusicTrack.time = time;
			return;
		}
		this.m_musicTrackStartTime = time;
	}

	// Token: 0x06008376 RID: 33654 RVA: 0x002A91D5 File Offset: 0x002A73D5
	public void StopCurrentMusicTrack()
	{
		if (this.m_currentMusicTrack != null)
		{
			this.FadeTrackOut(this.m_currentMusicTrack);
			this.ChangeCurrentMusicTrack(null);
		}
	}

	// Token: 0x06008377 RID: 33655 RVA: 0x002A91F8 File Offset: 0x002A73F8
	public void StopCurrentAmbienceTrack()
	{
		if (this.m_currentAmbienceTrack != null)
		{
			this.FadeTrackOut(this.m_currentAmbienceTrack);
			this.ChangeCurrentAmbienceTrack(null);
		}
	}

	// Token: 0x06008378 RID: 33656 RVA: 0x002A921B File Offset: 0x002A741B
	public void NukeMusicAndAmbiencePlaylists()
	{
		this.m_musicTracks.Clear();
		this.m_ambienceTracks.Clear();
		this.m_nextMusicTrackIndex = 0;
		this.m_musicTrackIndex = 0;
		this.m_ambienceTrackIndex = 0;
	}

	// Token: 0x06008379 RID: 33657 RVA: 0x002A9248 File Offset: 0x002A7448
	public void NukePlaylistsAndStopPlayingCurrentTracks()
	{
		this.NukeMusicAndAmbiencePlaylists();
		this.StopCurrentMusicTrack();
		this.StopCurrentAmbienceTrack();
	}

	// Token: 0x0600837A RID: 33658 RVA: 0x002A925C File Offset: 0x002A745C
	public void NukeMusicAndStopPlayingCurrentTrack()
	{
		this.m_musicTracks.Clear();
		this.m_nextMusicTrackIndex = 0;
		this.m_musicTrackIndex = 0;
		this.StopCurrentMusicTrack();
	}

	// Token: 0x0600837B RID: 33659 RVA: 0x002A927D File Offset: 0x002A747D
	public void NukeAmbienceAndStopPlayingCurrentTrack()
	{
		this.m_ambienceTracks.Clear();
		this.m_ambienceTrackIndex = 0;
		this.StopCurrentAmbienceTrack();
	}

	// Token: 0x0600837C RID: 33660 RVA: 0x002A9298 File Offset: 0x002A7498
	public void ImmediatelyKillMusicAndAmbience()
	{
		this.NukeMusicAndAmbiencePlaylists();
		foreach (AudioSource source in this.m_fadingTracks.ToArray())
		{
			this.FinishSource(source);
		}
		if (this.m_currentMusicTrack != null)
		{
			this.FinishSource(this.m_currentMusicTrack);
			this.ChangeCurrentMusicTrack(null);
		}
		if (this.m_currentAmbienceTrack != null)
		{
			this.FinishSource(this.m_currentAmbienceTrack);
			this.ChangeCurrentAmbienceTrack(null);
		}
	}

	// Token: 0x0600837D RID: 33661 RVA: 0x002A9314 File Offset: 0x002A7514
	private void AddTracks(List<MusicTrack> sourceTracks, List<MusicTrack> destTracks)
	{
		foreach (MusicTrack item in sourceTracks)
		{
			destTracks.Add(item);
		}
	}

	// Token: 0x0600837E RID: 33662 RVA: 0x002A9364 File Offset: 0x002A7564
	private void OnMusicLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			UnityEngine.Debug.LogError(string.Format("SoundManager.OnMusicLoaded() - ERROR \"{0}\" failed to load", assetRef));
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			UnityEngine.Debug.LogError(string.Format("SoundManager.OnMusicLoaded() - ERROR \"{0}\" has no AudioSource", this.SceneObject.name));
			return;
		}
		this.RegisterSourceBundle(assetRef, component);
		MusicTrack musicTrack = (MusicTrack)callbackData;
		if (this.m_musicTrackIndex >= this.m_musicTracks.Count || this.m_musicTracks[this.m_musicTrackIndex] != musicTrack)
		{
			this.UnregisterSourceBundle(assetRef, component);
			UnityEngine.Object.DestroyImmediate(go);
		}
		else
		{
			this.m_generatedSources.Add(component);
			component.transform.parent = this.SceneObject.transform;
			component.volume *= musicTrack.m_volume;
			component.time = this.m_musicTrackStartTime;
			this.m_musicTrackStartTime = 0f;
			this.ChangeCurrentMusicTrack(component);
			this.Play(component, null, null, null);
			if (this.OnMusicStarted != null)
			{
				this.OnMusicStarted();
			}
		}
		this.m_musicIsAboutToPlay = false;
	}

	// Token: 0x0600837F RID: 33663 RVA: 0x002A947C File Offset: 0x002A767C
	private void OnAmbienceLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			UnityEngine.Debug.LogError(string.Format("SoundManager.OnAmbienceLoaded() - ERROR \"{0}\" failed to load", assetRef));
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			UnityEngine.Debug.LogError(string.Format("SoundManager.OnAmbienceLoaded() - ERROR \"{0}\" has no AudioSource", this.SceneObject.name));
			return;
		}
		this.RegisterSourceBundle(assetRef, component);
		MusicTrack musicTrack = (MusicTrack)callbackData;
		if (!this.m_ambienceTracks.Contains(musicTrack))
		{
			this.UnregisterSourceBundle(assetRef, component);
			UnityEngine.Object.DestroyImmediate(go);
		}
		else
		{
			this.m_generatedSources.Add(component);
			component.transform.parent = this.SceneObject.transform;
			component.volume *= musicTrack.m_volume;
			this.ChangeCurrentAmbienceTrack(component);
			this.m_fadingTracksIn.Add(Processor.RunCoroutine(this.FadeTrackIn(component), null));
			this.Play(component, null, null, null);
		}
		this.m_ambienceIsAboutToPlay = false;
	}

	// Token: 0x06008380 RID: 33664 RVA: 0x002A9567 File Offset: 0x002A7767
	private void ChangeCurrentMusicTrack(AudioSource source)
	{
		this.m_currentMusicTrack = source;
	}

	// Token: 0x06008381 RID: 33665 RVA: 0x002A9570 File Offset: 0x002A7770
	private void ChangeCurrentAmbienceTrack(AudioSource source)
	{
		this.m_currentAmbienceTrack = source;
	}

	// Token: 0x06008382 RID: 33666 RVA: 0x002A957C File Offset: 0x002A777C
	private void UpdateMusicAndAmbience()
	{
		if (!this.IsMusicEnabled())
		{
			return;
		}
		if (!this.m_musicIsAboutToPlay)
		{
			if (this.m_currentMusicTrack != null)
			{
				if (!this.IsPlaying(this.m_currentMusicTrack))
				{
					Processor.RunCoroutine(this.PlayMusicInSeconds(this.m_config.m_SecondsBetweenMusicTracks), null);
				}
			}
			else
			{
				this.m_musicIsAboutToPlay = this.PlayNextMusic();
			}
		}
		if (!this.m_ambienceIsAboutToPlay)
		{
			if (this.m_currentAmbienceTrack != null)
			{
				if (!this.IsPlaying(this.m_currentAmbienceTrack))
				{
					Processor.RunCoroutine(this.PlayAmbienceInSeconds(0f), null);
					return;
				}
			}
			else
			{
				this.m_ambienceIsAboutToPlay = this.PlayNextAmbience();
			}
		}
	}

	// Token: 0x06008383 RID: 33667 RVA: 0x002A961F File Offset: 0x002A781F
	private IEnumerator PlayMusicInSeconds(float seconds)
	{
		this.m_musicIsAboutToPlay = true;
		yield return new WaitForSeconds(seconds);
		this.m_musicIsAboutToPlay = this.PlayNextMusic();
		yield break;
	}

	// Token: 0x06008384 RID: 33668 RVA: 0x002A9635 File Offset: 0x002A7835
	private bool PlayNextMusic()
	{
		return this.IsMusicEnabled() && this.m_musicTracks.Count > 0 && this.PlayMusicTrack(this.m_nextMusicTrackIndex);
	}

	// Token: 0x06008385 RID: 33669 RVA: 0x002A9660 File Offset: 0x002A7860
	private bool PlayMusicTrack(int index)
	{
		if (index < 0 || index >= this.m_musicTracks.Count)
		{
			return false;
		}
		this.m_musicTrackIndex = index;
		MusicTrack musicTrack = this.m_musicTracks[this.m_musicTrackIndex];
		this.m_nextMusicTrackIndex = (index + 1) % this.m_musicTracks.Count;
		if (musicTrack == null)
		{
			return false;
		}
		if (this.m_currentMusicTrack != null)
		{
			this.FadeTrackOut(this.m_currentMusicTrack);
			this.ChangeCurrentMusicTrack(null);
		}
		return SoundLoader.LoadSound(AssetLoader.Get().IsAssetAvailable(musicTrack.m_name) ? musicTrack.m_name : musicTrack.m_fallback, new PrefabCallback<GameObject>(this.OnMusicLoaded), musicTrack, this.GetPlaceholderSound());
	}

	// Token: 0x06008386 RID: 33670 RVA: 0x002A9717 File Offset: 0x002A7917
	private bool IsMusicEnabled()
	{
		return !SoundUtils.IsDeviceBackgroundMusicPlaying() && this.m_isMasterEnabled && this.m_isMusicEnabled;
	}

	// Token: 0x06008387 RID: 33671 RVA: 0x002A9730 File Offset: 0x002A7930
	private IEnumerator PlayAmbienceInSeconds(float seconds)
	{
		this.m_ambienceIsAboutToPlay = true;
		yield return new WaitForSeconds(seconds);
		this.m_ambienceIsAboutToPlay = this.PlayNextAmbience();
		yield break;
	}

	// Token: 0x06008388 RID: 33672 RVA: 0x002A9748 File Offset: 0x002A7948
	private bool PlayNextAmbience()
	{
		if (!this.IsMusicEnabled())
		{
			return false;
		}
		if (this.m_ambienceTracks.Count <= 0)
		{
			return false;
		}
		MusicTrack musicTrack = this.m_ambienceTracks[this.m_ambienceTrackIndex];
		this.m_ambienceTrackIndex = (this.m_ambienceTrackIndex + 1) % this.m_ambienceTracks.Count;
		if (musicTrack == null)
		{
			return false;
		}
		string input = AssetLoader.Get().IsAssetAvailable(musicTrack.m_name) ? musicTrack.m_name : musicTrack.m_fallback;
		foreach (Coroutine coroutine in this.m_fadingTracksIn)
		{
			if (coroutine != null)
			{
				Processor.CancelCoroutine(coroutine);
			}
		}
		this.m_fadingTracksIn.Clear();
		return SoundLoader.LoadSound(input, new PrefabCallback<GameObject>(this.OnAmbienceLoaded), musicTrack, this.GetPlaceholderSound());
	}

	// Token: 0x06008389 RID: 33673 RVA: 0x002A9838 File Offset: 0x002A7A38
	private void FadeTrackOut(AudioSource source)
	{
		if (!this.IsActive(source))
		{
			this.FinishSource(source);
			return;
		}
		Processor.RunCoroutine(this.FadeTrack(source, 0f), null);
	}

	// Token: 0x0600838A RID: 33674 RVA: 0x002A985E File Offset: 0x002A7A5E
	private IEnumerator FadeTrackIn(AudioSource source)
	{
		SoundManager.SourceExtension ext = this.GetExtension(source);
		if (ext == null)
		{
			Log.Sound.PrintWarning("Unable to find extension for sound {0}", new object[]
			{
				source.name
			});
			yield break;
		}
		float targetVolume = this.GetVolume(source);
		float currTime = 0f;
		float targetVolumeTime = 1f;
		ext.m_codeVolume = 0f;
		this.UpdateVolume(source, ext);
		while (ext.m_codeVolume < targetVolume)
		{
			currTime += Time.deltaTime;
			ext.m_codeVolume = Mathf.Lerp(0f, targetVolume, Mathf.Clamp01(currTime / targetVolumeTime));
			this.UpdateVolume(source, ext);
			yield return null;
			if (source == null)
			{
				yield break;
			}
			if (!this.IsActive(source))
			{
				yield break;
			}
		}
		yield break;
	}

	// Token: 0x0600838B RID: 33675 RVA: 0x002A9874 File Offset: 0x002A7A74
	private IEnumerator FadeTrack(AudioSource source, float targetVolume)
	{
		this.m_fadingTracks.Add(source);
		SoundManager.SourceExtension ext = this.GetExtension(source);
		while (ext.m_codeVolume > 0.0001f)
		{
			ext.m_codeVolume = Mathf.Lerp(ext.m_codeVolume, targetVolume, Time.deltaTime);
			this.UpdateVolume(source, ext);
			yield return null;
			if (source == null)
			{
				yield break;
			}
			if (!this.IsActive(source))
			{
				yield break;
			}
		}
		this.FinishSource(source);
		yield break;
	}

	// Token: 0x0600838C RID: 33676 RVA: 0x002A9894 File Offset: 0x002A7A94
	private SoundManager.SourceExtension RegisterExtension(AudioSource source, SoundDef oneShotDef = null, AudioClip oneShotClip = null, bool aboutToPlay = false)
	{
		SoundDef soundDef = oneShotDef;
		if (soundDef == null)
		{
			soundDef = this.GetDefFromSource(source);
		}
		SoundManager.SourceExtension sourceExtension = this.GetExtension(source);
		if (sourceExtension == null)
		{
			AssetHandle<AudioClip> disposable = null;
			AudioClip audioClip = (oneShotClip == null) ? this.LoadClipForPlayback(ref disposable, source, soundDef) : oneShotClip;
			DisposablesCleaner disposablesCleaner = HearthstoneServices.Get<DisposablesCleaner>();
			if (disposablesCleaner != null)
			{
				disposablesCleaner.Attach(source, disposable);
			}
			if (audioClip == null || (aboutToPlay && this.ProcessClipLimits(audioClip)))
			{
				return null;
			}
			sourceExtension = new SoundManager.SourceExtension();
			sourceExtension.m_sourceVolume = source.volume;
			sourceExtension.m_sourcePitch = source.pitch;
			sourceExtension.m_sourceClip = source.clip;
			sourceExtension.m_id = this.GetNextSourceId();
			this.AddExtensionMapping(source, sourceExtension);
			Global.SoundCategory category = this.GetCategory(source);
			if (category == Global.SoundCategory.NONE)
			{
				category = soundDef.m_Category;
			}
			this.RegisterSourceByCategory(source, category);
			this.InitNewClipOnSource(source, soundDef, sourceExtension, audioClip);
		}
		else if (aboutToPlay)
		{
			AudioClip audioClip2;
			if (oneShotClip == null)
			{
				AssetHandle<AudioClip> disposable2 = null;
				audioClip2 = this.LoadClipForPlayback(ref disposable2, source, soundDef);
				DisposablesCleaner disposablesCleaner2 = HearthstoneServices.Get<DisposablesCleaner>();
				if (disposablesCleaner2 != null)
				{
					disposablesCleaner2.Attach(source, disposable2);
				}
			}
			else
			{
				audioClip2 = oneShotClip;
			}
			if (!this.CanPlayClipOnExistingSource(source, audioClip2))
			{
				if (this.IsActive(source))
				{
					this.Stop(source);
				}
				else
				{
					this.FinishSource(source);
				}
				return null;
			}
			if (source.clip != audioClip2)
			{
				if (source.clip != null)
				{
					this.UnregisterSourceByClip(source);
				}
				this.InitNewClipOnSource(source, soundDef, sourceExtension, audioClip2);
			}
		}
		return sourceExtension;
	}

	// Token: 0x0600838D RID: 33677 RVA: 0x002A9A00 File Offset: 0x002A7C00
	public AudioClip LoadClipForPlayback(ref AssetHandle<AudioClip> clipHandle, AudioSource source, SoundDef oneShotDef)
	{
		string text = null;
		SoundDef soundDef = oneShotDef;
		if (oneShotDef == null)
		{
			soundDef = source.GetComponent<SoundDef>();
		}
		if (soundDef != null)
		{
			text = SoundUtils.GetRandomClipFromDef(soundDef);
			if (text == null)
			{
				text = soundDef.m_AudioClip;
				if (string.IsNullOrEmpty(text))
				{
					if (source.clip != null)
					{
						return source.clip;
					}
					string text2 = "";
					if (HearthstoneApplication.IsInternal())
					{
						text2 = " " + DebugUtils.GetHierarchyPathAndType(source, '.');
					}
					Error.AddDevFatal("{0} has no AudioClip. Top-level parent is {1}{2}.", new object[]
					{
						source.gameObject.name,
						SceneUtils.FindTopParent(source),
						text2
					});
					return null;
				}
			}
		}
		if (text == null || soundDef == null)
		{
			Error.AddDevFatal("DetermineClipForPlayback: failed to GET AudioClip clipAsset={0}, gameObject={2}, soundDef={3}", new object[]
			{
				text,
				source.gameObject.name,
				soundDef
			});
			return null;
		}
		SoundLoader.LoadAudioClipWithFallback(ref clipHandle, source, text);
		AssetHandle<AudioClip> assetHandle = clipHandle;
		if (assetHandle == null)
		{
			return null;
		}
		return assetHandle.Asset;
	}

	// Token: 0x0600838E RID: 33678 RVA: 0x002A9AF3 File Offset: 0x002A7CF3
	private bool CanPlayClipOnExistingSource(AudioSource source, AudioClip clip)
	{
		return !(clip == null) && ((this.IsActive(source) && !(source.clip != clip)) || !this.ProcessClipLimits(clip));
	}

	// Token: 0x0600838F RID: 33679 RVA: 0x002A9B23 File Offset: 0x002A7D23
	private void InitNewClipOnSource(AudioSource source, SoundDef def, SoundManager.SourceExtension ext, AudioClip clip)
	{
		ext.m_defVolume = SoundUtils.GetRandomVolumeFromDef(def);
		ext.m_defPitch = SoundUtils.GetRandomPitchFromDef(def);
		source.clip = clip;
		this.RegisterSourceByClip(source, clip);
	}

	// Token: 0x06008390 RID: 33680 RVA: 0x002A9B4E File Offset: 0x002A7D4E
	private void UnregisterExtension(AudioSource source, SoundManager.SourceExtension ext)
	{
		source.volume = ext.m_sourceVolume;
		source.pitch = ext.m_sourcePitch;
		source.clip = ext.m_sourceClip;
		this.RemoveExtensionMapping(source);
	}

	// Token: 0x06008391 RID: 33681 RVA: 0x002A9B7C File Offset: 0x002A7D7C
	private void UpdateSource(AudioSource source)
	{
		SoundManager.SourceExtension extension = this.GetExtension(source);
		this.UpdateSource(source, extension);
	}

	// Token: 0x06008392 RID: 33682 RVA: 0x002A9B99 File Offset: 0x002A7D99
	private void UpdateSource(AudioSource source, SoundManager.SourceExtension ext)
	{
		this.UpdateMute(source);
		this.UpdateVolume(source, ext);
		this.UpdatePitch(source, ext);
	}

	// Token: 0x06008393 RID: 33683 RVA: 0x002A9BB4 File Offset: 0x002A7DB4
	private void UpdateMute(AudioSource source)
	{
		bool categoryEnabled = this.IsCategoryEnabled(source);
		this.UpdateMute(source, categoryEnabled);
	}

	// Token: 0x06008394 RID: 33684 RVA: 0x002A9BD1 File Offset: 0x002A7DD1
	private void UpdateMute(AudioSource source, bool categoryEnabled)
	{
		source.mute = (this.m_mute || !categoryEnabled);
	}

	// Token: 0x06008395 RID: 33685 RVA: 0x002A9BE8 File Offset: 0x002A7DE8
	private void UpdateCategoryMute(Global.SoundCategory cat)
	{
		List<AudioSource> list;
		if (!this.m_sourcesByCategory.TryGetValue(cat, out list))
		{
			return;
		}
		bool categoryEnabled = this.IsCategoryEnabled(cat);
		for (int i = 0; i < list.Count; i++)
		{
			AudioSource source = list[i];
			this.UpdateMute(source, categoryEnabled);
		}
	}

	// Token: 0x06008396 RID: 33686 RVA: 0x002A9C30 File Offset: 0x002A7E30
	private void UpdateAllMutes()
	{
		foreach (SoundManager.ExtensionMapping extensionMapping in this.m_extensionMappings)
		{
			this.UpdateMute(extensionMapping.Source);
		}
	}

	// Token: 0x06008397 RID: 33687 RVA: 0x002A9C88 File Offset: 0x002A7E88
	private void UpdateVolume(AudioSource source, SoundManager.SourceExtension ext)
	{
		float categoryVolume = this.GetCategoryVolume(source);
		float duckingVolume = this.GetDuckingVolume(source);
		this.UpdateVolume(source, ext, categoryVolume, duckingVolume);
	}

	// Token: 0x06008398 RID: 33688 RVA: 0x002A9CAF File Offset: 0x002A7EAF
	private void UpdateVolume(AudioSource source, SoundManager.SourceExtension ext, float categoryVolume, float duckingVolume)
	{
		source.volume = ext.m_codeVolume * ext.m_sourceVolume * ext.m_defVolume * categoryVolume * duckingVolume;
	}

	// Token: 0x06008399 RID: 33689 RVA: 0x002A9CD0 File Offset: 0x002A7ED0
	public void UpdateCategoryVolume(Global.SoundCategory cat)
	{
		List<AudioSource> list;
		if (!this.m_sourcesByCategory.TryGetValue(cat, out list))
		{
			return;
		}
		float categoryVolume = SoundUtils.GetCategoryVolume(cat);
		for (int i = 0; i < list.Count; i++)
		{
			AudioSource audioSource = list[i];
			if (!(audioSource == null))
			{
				SoundManager.SourceExtension extension = this.GetExtension(audioSource);
				float duckingVolume = this.GetDuckingVolume(audioSource);
				this.UpdateVolume(audioSource, extension, categoryVolume, duckingVolume);
			}
		}
	}

	// Token: 0x0600839A RID: 33690 RVA: 0x002A9D38 File Offset: 0x002A7F38
	private void UpdateAllCategoryVolumes()
	{
		foreach (Global.SoundCategory cat in this.m_sourcesByCategory.Keys)
		{
			this.UpdateCategoryVolume(cat);
		}
	}

	// Token: 0x0600839B RID: 33691 RVA: 0x002A9D90 File Offset: 0x002A7F90
	private void UpdatePitch(AudioSource source, SoundManager.SourceExtension ext)
	{
		source.pitch = ext.m_codePitch * ext.m_sourcePitch * ext.m_defPitch;
	}

	// Token: 0x0600839C RID: 33692 RVA: 0x002A9DAC File Offset: 0x002A7FAC
	private void OnMasterEnabledOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		this.m_isMasterEnabled = Options.Get().GetBool(option);
		this.UpdateAllMutes();
	}

	// Token: 0x0600839D RID: 33693 RVA: 0x002A9DC5 File Offset: 0x002A7FC5
	private void OnMasterVolumeOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		this.SetMasterVolumeExponential();
	}

	// Token: 0x0600839E RID: 33694 RVA: 0x002A9DD0 File Offset: 0x002A7FD0
	private void OnEnabledOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		this.m_isMusicEnabled = Options.Get().GetBool(option);
		foreach (KeyValuePair<Global.SoundCategory, Option> keyValuePair in SoundDataTables.s_categoryEnabledOptionMap)
		{
			Global.SoundCategory key = keyValuePair.Key;
			if (keyValuePair.Value == option)
			{
				this.UpdateCategoryMute(key);
			}
		}
	}

	// Token: 0x0600839F RID: 33695 RVA: 0x002A9E48 File Offset: 0x002A8048
	private void OnVolumeOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		foreach (KeyValuePair<Global.SoundCategory, Option> keyValuePair in SoundDataTables.s_categoryVolumeOptionMap)
		{
			Global.SoundCategory key = keyValuePair.Key;
			if (keyValuePair.Value == option)
			{
				this.UpdateCategoryVolume(key);
			}
		}
	}

	// Token: 0x060083A0 RID: 33696 RVA: 0x002A9EAC File Offset: 0x002A80AC
	private void OnBackgroundSoundOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		this.UpdateAppMute();
	}

	// Token: 0x060083A1 RID: 33697 RVA: 0x002A9EB4 File Offset: 0x002A80B4
	private void RegisterSourceByCategory(AudioSource source, Global.SoundCategory cat)
	{
		List<AudioSource> list;
		if (!this.m_sourcesByCategory.TryGetValue(cat, out list))
		{
			list = new List<AudioSource>();
			this.m_sourcesByCategory.Add(cat, list);
			list.Add(source);
			return;
		}
		if (!list.Contains(source))
		{
			list.Add(source);
		}
	}

	// Token: 0x060083A2 RID: 33698 RVA: 0x002A9EFC File Offset: 0x002A80FC
	private void UnregisterSourceByCategory(AudioSource source)
	{
		Global.SoundCategory category = this.GetCategory(source);
		List<AudioSource> list;
		if (!this.m_sourcesByCategory.TryGetValue(category, out list))
		{
			UnityEngine.Debug.LogWarning(string.Format("SoundManager.UnregisterSourceByCategory() - {0} is untracked. category={1}", this.GetSourceId(source), category));
			return;
		}
		list.Remove(source);
	}

	// Token: 0x060083A3 RID: 33699 RVA: 0x002A9F4C File Offset: 0x002A814C
	private bool IsCategoryEnabled(AudioSource source)
	{
		SoundDef component = source.GetComponent<SoundDef>();
		return this.IsCategoryEnabled(component.m_Category);
	}

	// Token: 0x060083A4 RID: 33700 RVA: 0x002A9F6C File Offset: 0x002A816C
	private bool IsCategoryEnabled(Global.SoundCategory cat)
	{
		if (SoundUtils.IsMusicCategory(cat) && SoundUtils.IsDeviceBackgroundMusicPlaying())
		{
			return false;
		}
		if (!this.m_isMasterEnabled)
		{
			return false;
		}
		Option categoryEnabledOption = SoundUtils.GetCategoryEnabledOption(cat);
		if (categoryEnabledOption == Option.INVALID)
		{
			return true;
		}
		if (categoryEnabledOption == Option.MUSIC)
		{
			return this.m_isMusicEnabled;
		}
		return Options.Get().GetBool(categoryEnabledOption);
	}

	// Token: 0x060083A5 RID: 33701 RVA: 0x002A9FB6 File Offset: 0x002A81B6
	private float GetCategoryVolume(AudioSource source)
	{
		return SoundUtils.GetCategoryVolume(source.GetComponent<SoundDef>().m_Category);
	}

	// Token: 0x060083A6 RID: 33702 RVA: 0x002A9FC8 File Offset: 0x002A81C8
	private bool IsCategoryAudible(Global.SoundCategory cat)
	{
		return SoundUtils.GetCategoryVolume(cat) > Mathf.Epsilon && this.IsCategoryEnabled(cat);
	}

	// Token: 0x060083A7 RID: 33703 RVA: 0x002A9FE0 File Offset: 0x002A81E0
	private void RegisterSourceByClip(AudioSource source, AudioClip clip)
	{
		List<AudioSource> list;
		if (!this.m_sourcesByClipName.TryGetValue(clip.name, out list))
		{
			list = new List<AudioSource>();
			this.m_sourcesByClipName.Add(clip.name, list);
			list.Add(source);
			return;
		}
		if (!list.Contains(source))
		{
			list.Add(source);
		}
	}

	// Token: 0x060083A8 RID: 33704 RVA: 0x002AA034 File Offset: 0x002A8234
	private void UnregisterSourceByClip(AudioSource source)
	{
		AudioClip clip = source.clip;
		if (clip == null)
		{
			UnityEngine.Debug.LogWarning(string.Format("SoundManager.UnregisterSourceByClip() - id {0} (source {1}) is untracked", this.GetSourceId(source), source));
			return;
		}
		List<AudioSource> list;
		if (!this.m_sourcesByClipName.TryGetValue(clip.name, out list))
		{
			UnityEngine.Debug.LogError(string.Format("SoundManager.UnregisterSourceByClip() - id {0} (source {1}) is untracked. clip={2}", this.GetSourceId(source), source, clip));
			return;
		}
		list.Remove(source);
		if (list.Count == 0)
		{
			this.m_sourcesByClipName.Remove(clip.name);
		}
	}

	// Token: 0x060083A9 RID: 33705 RVA: 0x002AA0C4 File Offset: 0x002A82C4
	private bool ProcessClipLimits(AudioClip clip)
	{
		if (this.m_config == null || this.m_config.m_PlaybackLimitDefs == null)
		{
			return false;
		}
		string name = clip.name;
		bool flag = false;
		AudioSource audioSource = null;
		foreach (SoundPlaybackLimitDef soundPlaybackLimitDef in this.m_config.m_PlaybackLimitDefs)
		{
			SoundPlaybackLimitClipDef soundPlaybackLimitClipDef = this.FindClipDefInPlaybackDef(name, soundPlaybackLimitDef);
			if (soundPlaybackLimitClipDef != null)
			{
				int num = soundPlaybackLimitClipDef.m_Priority;
				float num2 = 2f;
				int num3 = 0;
				foreach (SoundPlaybackLimitClipDef soundPlaybackLimitClipDef2 in soundPlaybackLimitDef.m_ClipDefs)
				{
					string legacyName = soundPlaybackLimitClipDef2.LegacyName;
					List<AudioSource> list;
					if (this.m_sourcesByClipName.TryGetValue(legacyName, out list))
					{
						int priority = soundPlaybackLimitClipDef2.m_Priority;
						foreach (AudioSource audioSource2 in list)
						{
							if (this.IsPlaying(audioSource2))
							{
								float num4 = audioSource2.time / audioSource2.clip.length;
								if (num4 <= soundPlaybackLimitClipDef2.m_ExclusivePlaybackThreshold)
								{
									num3++;
									if (priority < num && num4 < num2)
									{
										audioSource = audioSource2;
										num = priority;
										num2 = num4;
									}
								}
							}
						}
					}
				}
				if (num3 >= soundPlaybackLimitDef.m_Limit)
				{
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			return false;
		}
		if (audioSource == null)
		{
			return true;
		}
		this.Stop(audioSource);
		return false;
	}

	// Token: 0x060083AA RID: 33706 RVA: 0x002AA2A0 File Offset: 0x002A84A0
	private SoundPlaybackLimitClipDef FindClipDefInPlaybackDef(string clipName, SoundPlaybackLimitDef def)
	{
		if (def.m_ClipDefs == null)
		{
			return null;
		}
		foreach (SoundPlaybackLimitClipDef soundPlaybackLimitClipDef in def.m_ClipDefs)
		{
			string legacyName = soundPlaybackLimitClipDef.LegacyName;
			if (clipName == legacyName)
			{
				return soundPlaybackLimitClipDef;
			}
		}
		return null;
	}

	// Token: 0x060083AB RID: 33707 RVA: 0x002AA310 File Offset: 0x002A8510
	public bool StartDucking(SoundDucker ducker)
	{
		if (ducker == null)
		{
			return false;
		}
		if (ducker.m_DuckedCategoryDefs == null)
		{
			return false;
		}
		if (ducker.m_DuckedCategoryDefs.Count == 0)
		{
			return false;
		}
		this.RegisterForDucking(ducker, ducker.GetDuckedCategoryDefs());
		return true;
	}

	// Token: 0x060083AC RID: 33708 RVA: 0x002AA344 File Offset: 0x002A8544
	public void StopDucking(SoundDucker ducker)
	{
		if (ducker == null)
		{
			return;
		}
		if (ducker.m_DuckedCategoryDefs == null)
		{
			return;
		}
		if (ducker.m_DuckedCategoryDefs.Count == 0)
		{
			return;
		}
		this.UnregisterForDucking(ducker, ducker.GetDuckedCategoryDefs());
	}

	// Token: 0x060083AD RID: 33709 RVA: 0x002AA374 File Offset: 0x002A8574
	public bool IsIgnoringDucking(AudioSource source)
	{
		if (source == null)
		{
			return true;
		}
		SoundDef component = source.GetComponent<SoundDef>();
		return component == null || component.m_IgnoreDucking;
	}

	// Token: 0x060083AE RID: 33710 RVA: 0x002AA3A4 File Offset: 0x002A85A4
	public void SetIgnoreDucking(AudioSource source, bool enable)
	{
		if (source == null)
		{
			return;
		}
		SoundDef component = source.GetComponent<SoundDef>();
		if (component == null)
		{
			return;
		}
		component.m_IgnoreDucking = enable;
	}

	// Token: 0x060083AF RID: 33711 RVA: 0x002AA3D4 File Offset: 0x002A85D4
	private void RegisterSourceForDucking(AudioSource source, SoundManager.SourceExtension ext)
	{
		SoundDuckingDef soundDuckingDef = this.FindDuckingDefForSource(source);
		if (soundDuckingDef == null)
		{
			return;
		}
		this.RegisterForDucking(source, soundDuckingDef.m_DuckedCategoryDefs);
		ext.m_ducking = true;
	}

	// Token: 0x060083B0 RID: 33712 RVA: 0x002AA404 File Offset: 0x002A8604
	private void RegisterForDucking(object trigger, List<SoundDuckedCategoryDef> defs)
	{
		foreach (SoundDuckedCategoryDef duckedCatDef in defs)
		{
			SoundManager.DuckState state = this.RegisterDuckState(trigger, duckedCatDef);
			this.ChangeDuckState(state, SoundManager.DuckMode.BEGINNING);
		}
	}

	// Token: 0x060083B1 RID: 33713 RVA: 0x002AA45C File Offset: 0x002A865C
	private SoundManager.DuckState RegisterDuckState(object trigger, SoundDuckedCategoryDef duckedCatDef)
	{
		Global.SoundCategory category = duckedCatDef.m_Category;
		List<SoundManager.DuckState> list;
		SoundManager.DuckState duckState;
		if (this.m_duckStates.TryGetValue(category, out list))
		{
			duckState = list.Find((SoundManager.DuckState currState) => currState.IsTrigger(trigger));
			if (duckState != null)
			{
				return duckState;
			}
		}
		else
		{
			list = new List<SoundManager.DuckState>();
			this.m_duckStates.Add(category, list);
		}
		duckState = new SoundManager.DuckState();
		list.Add(duckState);
		duckState.SetTrigger(trigger);
		duckState.SetDuckedDef(duckedCatDef);
		return duckState;
	}

	// Token: 0x060083B2 RID: 33714 RVA: 0x002AA4DC File Offset: 0x002A86DC
	private void UnregisterSourceForDucking(AudioSource source, SoundManager.SourceExtension ext)
	{
		if (!ext.m_ducking)
		{
			return;
		}
		SoundDuckingDef soundDuckingDef = this.FindDuckingDefForSource(source);
		if (soundDuckingDef == null)
		{
			return;
		}
		this.UnregisterForDucking(source, soundDuckingDef.m_DuckedCategoryDefs);
	}

	// Token: 0x060083B3 RID: 33715 RVA: 0x002AA50C File Offset: 0x002A870C
	private void UnregisterForDucking(object trigger, List<SoundDuckedCategoryDef> defs)
	{
		Predicate<SoundManager.DuckState> <>9__0;
		foreach (SoundDuckedCategoryDef soundDuckedCategoryDef in defs)
		{
			Global.SoundCategory category = soundDuckedCategoryDef.m_Category;
			List<SoundManager.DuckState> list;
			if (!this.m_duckStates.TryGetValue(category, out list))
			{
				UnityEngine.Debug.LogError(string.Format("SoundManager.UnregisterForDucking() - {0} ducks {1}, but no DuckStates were found for {1}", trigger, category));
			}
			else
			{
				List<SoundManager.DuckState> list2 = list;
				Predicate<SoundManager.DuckState> match;
				if ((match = <>9__0) == null)
				{
					match = (<>9__0 = ((SoundManager.DuckState currState) => currState.IsTrigger(trigger)));
				}
				SoundManager.DuckState duckState = list2.Find(match);
				if (duckState != null)
				{
					this.ChangeDuckState(duckState, SoundManager.DuckMode.RESTORING);
				}
			}
		}
	}

	// Token: 0x060083B4 RID: 33716 RVA: 0x002AA5C8 File Offset: 0x002A87C8
	private uint GetNextDuckStateTweenId()
	{
		this.m_nextDuckStateTweenId = (this.m_nextDuckStateTweenId + 1U & uint.MaxValue);
		return this.m_nextDuckStateTweenId;
	}

	// Token: 0x060083B5 RID: 33717 RVA: 0x002AA5E0 File Offset: 0x002A87E0
	private void ChangeDuckState(SoundManager.DuckState state, SoundManager.DuckMode mode)
	{
		string tweenName = state.GetTweenName();
		if (tweenName != null)
		{
			iTween.StopByName(this.SceneObject, tweenName);
		}
		state.SetMode(mode);
		state.SetTweenName(null);
		if (mode == SoundManager.DuckMode.BEGINNING)
		{
			this.AnimateBeginningDuckState(state);
			return;
		}
		if (mode != SoundManager.DuckMode.RESTORING)
		{
			return;
		}
		this.AnimateRestoringDuckState(state);
	}

	// Token: 0x060083B6 RID: 33718 RVA: 0x002AA62C File Offset: 0x002A882C
	private void AnimateBeginningDuckState(SoundManager.DuckState state)
	{
		string text = string.Format("DuckState Begin id={0}", this.GetNextDuckStateTweenId());
		state.SetTweenName(text);
		SoundDuckedCategoryDef duckedDef = state.GetDuckedDef();
		Action<object> action = delegate(object amount)
		{
			float volume = (float)amount;
			state.SetVolume(volume);
			this.UpdateCategoryVolume(duckedDef.m_Category);
		};
		Action<object> action2 = delegate(object e)
		{
			this.OnDuckStateBeginningComplete(e);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"name",
			text,
			"time",
			duckedDef.m_BeginSec,
			"easeType",
			duckedDef.m_BeginEaseType,
			"from",
			state.GetVolume(),
			"to",
			duckedDef.m_Volume,
			"onupdate",
			action,
			"oncomplete",
			action2,
			"oncompleteparams",
			state
		});
		iTween.ValueTo(this.SceneObject, args);
	}

	// Token: 0x060083B7 RID: 33719 RVA: 0x002AA760 File Offset: 0x002A8960
	private void OnDuckStateBeginningComplete(object arg)
	{
		SoundManager.DuckState duckState = arg as SoundManager.DuckState;
		if (duckState != null)
		{
			duckState.SetMode(SoundManager.DuckMode.HOLD);
			duckState.SetTweenName(null);
		}
	}

	// Token: 0x060083B8 RID: 33720 RVA: 0x002AA788 File Offset: 0x002A8988
	private void AnimateRestoringDuckState(SoundManager.DuckState state)
	{
		string text = string.Format("DuckState Finish id={0}", this.GetNextDuckStateTweenId());
		state.SetTweenName(text);
		SoundDuckedCategoryDef duckedDef = state.GetDuckedDef();
		Action<object> action = delegate(object amount)
		{
			float volume = (float)amount;
			state.SetVolume(volume);
			this.UpdateCategoryVolume(duckedDef.m_Category);
		};
		Action<object> action2 = delegate(object e)
		{
			this.OnDuckStateRestoringComplete(e);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"name",
			text,
			"time",
			duckedDef.m_RestoreSec,
			"easeType",
			duckedDef.m_RestoreEaseType,
			"from",
			state.GetVolume(),
			"to",
			1f,
			"onupdate",
			action,
			"oncomplete",
			action2,
			"oncompleteparams",
			state
		});
		iTween.ValueTo(this.SceneObject, args);
	}

	// Token: 0x060083B9 RID: 33721 RVA: 0x002AA8B4 File Offset: 0x002A8AB4
	private void OnDuckStateRestoringComplete(object arg)
	{
		SoundManager.DuckState duckState = arg as SoundManager.DuckState;
		if (duckState != null)
		{
			Global.SoundCategory category = duckState.GetDuckedDef().m_Category;
			List<SoundManager.DuckState> list = this.m_duckStates[category];
			int i = 0;
			while (i < list.Count)
			{
				if (list[i] == duckState)
				{
					list.RemoveAt(i);
					if (list.Count == 0)
					{
						this.m_duckStates.Remove(category);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}
	}

	// Token: 0x060083BA RID: 33722 RVA: 0x002AA91C File Offset: 0x002A8B1C
	private SoundDuckingDef FindDuckingDefForSource(AudioSource source)
	{
		Global.SoundCategory category = this.GetCategory(source);
		return this.FindDuckingDefForCategory(category);
	}

	// Token: 0x060083BB RID: 33723 RVA: 0x002AA938 File Offset: 0x002A8B38
	private SoundDuckingDef FindDuckingDefForCategory(Global.SoundCategory cat)
	{
		if (this.m_config == null || this.m_config.m_DuckingDefs == null)
		{
			return null;
		}
		foreach (SoundDuckingDef soundDuckingDef in this.m_config.m_DuckingDefs)
		{
			if (cat == soundDuckingDef.m_TriggerCategory)
			{
				return soundDuckingDef;
			}
		}
		return null;
	}

	// Token: 0x060083BC RID: 33724 RVA: 0x002AA9B8 File Offset: 0x002A8BB8
	private float GetDuckingVolume(AudioSource source)
	{
		if (source == null)
		{
			return 1f;
		}
		SoundDef component = source.GetComponent<SoundDef>();
		if (component.m_IgnoreDucking)
		{
			return 1f;
		}
		return this.GetDuckingVolume(component.m_Category);
	}

	// Token: 0x060083BD RID: 33725 RVA: 0x002AA9F8 File Offset: 0x002A8BF8
	private float GetDuckingVolume(Global.SoundCategory cat)
	{
		List<SoundManager.DuckState> list;
		if (!this.m_duckStates.TryGetValue(cat, out list))
		{
			return 1f;
		}
		float num = 1f;
		foreach (SoundManager.DuckState duckState in list)
		{
			Global.SoundCategory triggerCategory = duckState.GetTriggerCategory();
			if (triggerCategory == Global.SoundCategory.NONE || this.IsCategoryAudible(triggerCategory))
			{
				float volume = duckState.GetVolume();
				if (num > volume)
				{
					num = volume;
				}
			}
		}
		return num;
	}

	// Token: 0x060083BE RID: 33726 RVA: 0x002AAA84 File Offset: 0x002A8C84
	private int GetNextSourceId()
	{
		int nextSourceId = this.m_nextSourceId;
		this.m_nextSourceId = ((this.m_nextSourceId == int.MaxValue) ? 1 : (this.m_nextSourceId + 1));
		return nextSourceId;
	}

	// Token: 0x060083BF RID: 33727 RVA: 0x002AAAAC File Offset: 0x002A8CAC
	private int GetSourceId(AudioSource source)
	{
		SoundManager.SourceExtension extension = this.GetExtension(source);
		if (extension == null)
		{
			return 0;
		}
		return extension.m_id;
	}

	// Token: 0x060083C0 RID: 33728 RVA: 0x002AAACC File Offset: 0x002A8CCC
	private AudioSource PlayImpl(AudioSource source, SoundDef oneShotDef, AudioClip oneShotClip = null, SoundManager.SoundOptions additionalSettings = null)
	{
		if (source == null)
		{
			AudioSource placeholderSource = this.GetPlaceholderSource();
			if (placeholderSource == null)
			{
				Error.AddDevFatal("SoundManager.Play() - source is null and fallback is null", Array.Empty<object>());
				return null;
			}
			UnityEngine.Debug.LogWarningFormat("Using placeholder sound for source={0}, oneShotDef={1}, oneShotClip={2}", new object[]
			{
				source,
				oneShotDef,
				oneShotClip
			});
			source = UnityEngine.Object.Instantiate<AudioSource>(placeholderSource);
			this.m_generatedSources.Add(source);
		}
		bool flag = this.IsActive(source);
		SoundManager.SourceExtension sourceExtension = this.RegisterExtension(source, oneShotDef, oneShotClip, true);
		if (sourceExtension == null)
		{
			return null;
		}
		if (!flag)
		{
			this.RegisterSourceForDucking(source, sourceExtension);
		}
		this.UpdateSource(source, sourceExtension);
		if (additionalSettings != null && additionalSettings.InstanceLimited)
		{
			int num;
			if (this.activeLimitedSounds.TryGetValue(source.gameObject.name, out num))
			{
				int num2 = additionalSettings.MaxInstancesOfThisSound;
				if (num2 < 1)
				{
					num2 = 1;
				}
				if (num >= num2)
				{
					return null;
				}
				this.activeLimitedSounds.Remove(source.gameObject.name);
				this.activeLimitedSounds.Add(source.gameObject.name, num + 1);
			}
			else
			{
				this.activeLimitedSounds.Add(source.gameObject.name, 1);
			}
			float num3 = additionalSettings.InstanceTimeLimit;
			if (num3 <= 0f)
			{
				num3 = source.clip.length;
			}
			HearthstoneApplication.Get().StartCoroutine(this.EnableInstanceLimitedSound(source.gameObject.name, num3));
		}
		source.Play();
		return source;
	}

	// Token: 0x060083C1 RID: 33729 RVA: 0x002AAC30 File Offset: 0x002A8E30
	private IEnumerator EnableInstanceLimitedSound(string sound, float time)
	{
		if (!this.activeLimitedSounds.ContainsKey(sound))
		{
			yield break;
		}
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		int num;
		if (this.activeLimitedSounds.TryGetValue(sound, out num))
		{
			this.activeLimitedSounds.Remove(sound);
			num--;
			if (num > 0)
			{
				this.activeLimitedSounds.Add(sound, num);
			}
		}
		yield break;
	}

	// Token: 0x060083C2 RID: 33730 RVA: 0x002AAC50 File Offset: 0x002A8E50
	private SoundDef GetDefFromSource(AudioSource source)
	{
		SoundDef soundDef = source.GetComponent<SoundDef>();
		if (soundDef == null)
		{
			Log.Sound.Print("SoundUtils.GetDefFromSource() - source={0} has no def. adding new def.", new object[]
			{
				source
			});
			soundDef = source.gameObject.AddComponent<SoundDef>();
		}
		return soundDef;
	}

	// Token: 0x060083C3 RID: 33731 RVA: 0x002A9EAC File Offset: 0x002A80AC
	private void OnAppFocusChanged(bool focus, object userData)
	{
		this.UpdateAppMute();
	}

	// Token: 0x060083C4 RID: 33732 RVA: 0x002AAC93 File Offset: 0x002A8E93
	private void UpdateAppMute()
	{
		this.UpdateMusicAndSources();
		if (HearthstoneApplication.Get() != null)
		{
			this.m_mute = (!HearthstoneApplication.Get().HasFocus() && !Options.Get().GetBool(Option.BACKGROUND_SOUND));
		}
		this.UpdateAllMutes();
	}

	// Token: 0x060083C5 RID: 33733 RVA: 0x002AACD2 File Offset: 0x002A8ED2
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		this.GarbageCollectBundles();
	}

	// Token: 0x060083C6 RID: 33734 RVA: 0x002AACDC File Offset: 0x002A8EDC
	private AudioSource GenerateAudioSource(AudioSource templateSource, SoundDef def)
	{
		string arg = (def != null) ? Path.GetFileNameWithoutExtension(def.m_AudioClip) : "CreatedSound";
		string name = string.Format("Audio Object - {0}", arg);
		AudioSource component;
		if (templateSource)
		{
			GameObject gameObject = new GameObject(name);
			SoundUtils.AddAudioSourceComponents(gameObject);
			component = gameObject.GetComponent<AudioSource>();
			SoundUtils.CopyAudioSource(templateSource, component);
		}
		else if (this.m_config.m_PlayClipTemplate)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.m_config.m_PlayClipTemplate.gameObject);
			gameObject2.name = name;
			component = gameObject2.GetComponent<AudioSource>();
		}
		else
		{
			GameObject gameObject3 = new GameObject(name);
			SoundUtils.AddAudioSourceComponents(gameObject3);
			component = gameObject3.GetComponent<AudioSource>();
		}
		this.m_generatedSources.Add(component);
		return component;
	}

	// Token: 0x060083C7 RID: 33735 RVA: 0x002AAD8C File Offset: 0x002A8F8C
	private void InitSourceTransform(AudioSource source, GameObject parentObject)
	{
		if (source == null || source.gameObject == null || source.transform == null)
		{
			return;
		}
		source.transform.parent = this.SceneObject.transform;
		if (parentObject == null || parentObject.transform == null)
		{
			source.transform.position = Vector3.zero;
			return;
		}
		source.transform.position = parentObject.transform.position;
	}

	// Token: 0x060083C8 RID: 33736 RVA: 0x002AAE14 File Offset: 0x002A9014
	private void FinishSource(AudioSource source)
	{
		if (this.m_currentMusicTrack == source)
		{
			this.ChangeCurrentMusicTrack(null);
		}
		else if (this.m_currentAmbienceTrack == source)
		{
			this.ChangeCurrentAmbienceTrack(null);
		}
		for (int i = 0; i < this.m_fadingTracks.Count; i++)
		{
			if (this.m_fadingTracks[i] == source)
			{
				this.m_fadingTracks.RemoveAt(i);
				break;
			}
		}
		this.UnregisterSourceByCategory(source);
		this.UnregisterSourceByClip(source);
		SoundManager.SourceExtension extension = this.GetExtension(source);
		if (extension != null)
		{
			this.UnregisterSourceForDucking(source, extension);
			this.UnregisterSourceBundle(source, extension);
			this.UnregisterExtension(source, extension);
		}
		this.FinishGeneratedSource(source);
	}

	// Token: 0x060083C9 RID: 33737 RVA: 0x002AAEC0 File Offset: 0x002A90C0
	private void FinishGeneratedSource(AudioSource source)
	{
		for (int i = 0; i < this.m_generatedSources.Count; i++)
		{
			if (this.m_generatedSources[i] == source)
			{
				UnityEngine.Object.Destroy(source.gameObject);
				this.m_generatedSources.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x060083CA RID: 33738 RVA: 0x002AAF10 File Offset: 0x002A9110
	private SoundManager.BundleInfo RegisterSourceBundle(AssetReference assetRef, AudioSource source)
	{
		SoundManager.BundleInfo bundleInfo;
		if (!this.m_bundleInfos.TryGetValue(assetRef, out bundleInfo))
		{
			bundleInfo = new SoundManager.BundleInfo();
			bundleInfo.SetAssetRef(assetRef);
			this.m_bundleInfos.Add(assetRef, bundleInfo);
		}
		if (source != null)
		{
			bundleInfo.AddRef(source);
			SoundManager.SourceExtension sourceExtension = this.RegisterExtension(source, null, null, false);
			if (sourceExtension == null)
			{
				return null;
			}
			sourceExtension.m_bundleName = assetRef;
		}
		return bundleInfo;
	}

	// Token: 0x060083CB RID: 33739 RVA: 0x002AAF7E File Offset: 0x002A917E
	private void UnregisterSourceBundle(AudioSource source, SoundManager.SourceExtension ext)
	{
		if (ext.m_bundleName == null)
		{
			return;
		}
		this.UnregisterSourceBundle(ext.m_bundleName, source);
	}

	// Token: 0x060083CC RID: 33740 RVA: 0x002AAF98 File Offset: 0x002A9198
	private void UnregisterSourceBundle(string name, AudioSource source)
	{
		SoundManager.BundleInfo bundleInfo;
		if (!this.m_bundleInfos.TryGetValue(name, out bundleInfo))
		{
			return;
		}
		if (!bundleInfo.RemoveRef(source))
		{
			return;
		}
		if (bundleInfo.CanGarbageCollect())
		{
			this.m_bundleInfos.Remove(name);
			this.UnloadSoundBundle(name);
		}
	}

	// Token: 0x060083CD RID: 33741 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void UnloadSoundBundle(AssetReference assetRef)
	{
	}

	// Token: 0x060083CE RID: 33742 RVA: 0x002AAFE4 File Offset: 0x002A91E4
	private void GarbageCollectBundles()
	{
		Map<string, SoundManager.BundleInfo> map = new Map<string, SoundManager.BundleInfo>();
		foreach (KeyValuePair<string, SoundManager.BundleInfo> keyValuePair in this.m_bundleInfos)
		{
			string key = keyValuePair.Key;
			SoundManager.BundleInfo value = keyValuePair.Value;
			value.EnableGarbageCollect(true);
			if (value.CanGarbageCollect())
			{
				this.UnloadSoundBundle(key);
			}
			else
			{
				map.Add(key, value);
			}
		}
		this.m_bundleInfos = map;
	}

	// Token: 0x060083CF RID: 33743 RVA: 0x002AB078 File Offset: 0x002A9278
	private void UpdateMusicAndSources()
	{
		this.UpdateMusicAndAmbience();
		this.UpdateSources();
	}

	// Token: 0x060083D0 RID: 33744 RVA: 0x002AB086 File Offset: 0x002A9286
	private void UpdateSources()
	{
		this.UpdateSourceExtensionMappings();
		this.UpdateSourcesByCategory();
		this.UpdateSourcesByClipName();
		this.UpdateSourceBundles();
		this.UpdateGeneratedSources();
		this.UpdateDuckStates();
	}

	// Token: 0x060083D1 RID: 33745 RVA: 0x002AB0AC File Offset: 0x002A92AC
	private void UpdateSourceExtensionMappings()
	{
		int i = 0;
		while (i < this.m_extensionMappings.Count)
		{
			AudioSource source = this.m_extensionMappings[i].Source;
			if (source == null)
			{
				this.m_extensionMappings.RemoveAt(i);
			}
			else
			{
				if (!this.IsActive(source))
				{
					this.m_inactiveSources.Add(source);
				}
				i++;
			}
		}
		this.CleanInactiveSources();
	}

	// Token: 0x060083D2 RID: 33746 RVA: 0x002AB114 File Offset: 0x002A9314
	private void CleanUpSourceList(List<AudioSource> sources)
	{
		if (sources == null)
		{
			return;
		}
		int i = 0;
		while (i < sources.Count)
		{
			if (sources[i] == null)
			{
				sources.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x060083D3 RID: 33747 RVA: 0x002AB150 File Offset: 0x002A9350
	private void UpdateSourcesByCategory()
	{
		foreach (List<AudioSource> sources in this.m_sourcesByCategory.Values)
		{
			this.CleanUpSourceList(sources);
		}
	}

	// Token: 0x060083D4 RID: 33748 RVA: 0x002AB1A8 File Offset: 0x002A93A8
	private void UpdateSourcesByClipName()
	{
		foreach (List<AudioSource> sources in this.m_sourcesByClipName.Values)
		{
			this.CleanUpSourceList(sources);
		}
	}

	// Token: 0x060083D5 RID: 33749 RVA: 0x002AB200 File Offset: 0x002A9400
	private void UpdateSourceBundles()
	{
		this.m_bundleInfosToRemove.Clear();
		foreach (SoundManager.BundleInfo bundleInfo in this.m_bundleInfos.Values)
		{
			List<AudioSource> refs = bundleInfo.GetRefs();
			int i = 0;
			bool flag = false;
			while (i < refs.Count)
			{
				if (refs[i] == null)
				{
					flag = true;
					refs.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			if (flag)
			{
				string assetRef = bundleInfo.GetAssetRef();
				if (bundleInfo.CanGarbageCollect())
				{
					this.m_bundleInfosToRemove.Add(assetRef);
				}
			}
		}
		for (int j = 0; j < this.m_bundleInfosToRemove.Count; j++)
		{
			string text = this.m_bundleInfosToRemove[j];
			this.m_bundleInfos.Remove(text);
			this.UnloadSoundBundle(text);
		}
	}

	// Token: 0x060083D6 RID: 33750 RVA: 0x002AB2F8 File Offset: 0x002A94F8
	private void UpdateGeneratedSources()
	{
		this.CleanUpSourceList(this.m_generatedSources);
	}

	// Token: 0x060083D7 RID: 33751 RVA: 0x002AB308 File Offset: 0x002A9508
	private void FinishDeadGeneratedSource(AudioSource source)
	{
		for (int i = 0; i < this.m_generatedSources.Count; i++)
		{
			if (this.m_generatedSources[i] == source)
			{
				this.m_generatedSources.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x060083D8 RID: 33752 RVA: 0x002AB34C File Offset: 0x002A954C
	private void UpdateDuckStates()
	{
		foreach (List<SoundManager.DuckState> list in this.m_duckStates.Values)
		{
			foreach (SoundManager.DuckState duckState in list)
			{
				if (!duckState.IsTriggerAlive() && duckState.GetMode() != SoundManager.DuckMode.RESTORING)
				{
					this.ChangeDuckState(duckState, SoundManager.DuckMode.RESTORING);
				}
			}
		}
	}

	// Token: 0x060083D9 RID: 33753 RVA: 0x002AB3EC File Offset: 0x002A95EC
	private void CleanInactiveSources()
	{
		foreach (AudioSource source in this.m_inactiveSources)
		{
			this.FinishSource(source);
		}
		this.m_inactiveSources.Clear();
	}

	// Token: 0x060083DA RID: 33754 RVA: 0x002AB44C File Offset: 0x002A964C
	[Conditional("SOUND_SOURCE_DEBUG")]
	private void SourcePrint(string format, params object[] args)
	{
		Log.Sound.Print(format, args);
	}

	// Token: 0x060083DB RID: 33755 RVA: 0x002AB45A File Offset: 0x002A965A
	[Conditional("SOUND_SOURCE_DEBUG")]
	private void SourceScreenPrint(string format, params object[] args)
	{
		Log.Sound.ScreenPrint(format, args);
	}

	// Token: 0x060083DC RID: 33756 RVA: 0x002AB44C File Offset: 0x002A964C
	[Conditional("SOUND_TRACK_DEBUG")]
	private void TrackPrint(string format, params object[] args)
	{
		Log.Sound.Print(format, args);
	}

	// Token: 0x060083DD RID: 33757 RVA: 0x002AB45A File Offset: 0x002A965A
	[Conditional("SOUND_TRACK_DEBUG")]
	private void TrackScreenPrint(string format, params object[] args)
	{
		Log.Sound.ScreenPrint(format, args);
	}

	// Token: 0x060083DE RID: 33758 RVA: 0x002AB44C File Offset: 0x002A964C
	[Conditional("SOUND_CATEGORY_DEBUG")]
	private void CategoryPrint(string format, params object[] args)
	{
		Log.Sound.Print(format, args);
	}

	// Token: 0x060083DF RID: 33759 RVA: 0x002AB45A File Offset: 0x002A965A
	[Conditional("SOUND_CATEGORY_DEBUG")]
	private void CategoryScreenPrint(string format, params object[] args)
	{
		Log.Sound.ScreenPrint(format, args);
	}

	// Token: 0x060083E0 RID: 33760 RVA: 0x002AB468 File Offset: 0x002A9668
	[Conditional("SOUND_CATEGORY_DEBUG")]
	private void PrintAllCategorySources()
	{
		Log.Sound.Print("SoundManager.PrintAllCategorySources()", Array.Empty<object>());
		foreach (KeyValuePair<Global.SoundCategory, List<AudioSource>> keyValuePair in this.m_sourcesByCategory)
		{
			Global.SoundCategory key = keyValuePair.Key;
			List<AudioSource> value = keyValuePair.Value;
			Log.Sound.Print("Category {0}:", new object[]
			{
				key
			});
			for (int i = 0; i < value.Count; i++)
			{
				Log.Sound.Print("    {0} = {1}", new object[]
				{
					i,
					value[i]
				});
			}
		}
	}

	// Token: 0x060083E1 RID: 33761 RVA: 0x002AB44C File Offset: 0x002A964C
	[Conditional("SOUND_BUNDLE_DEBUG")]
	private void BundlePrint(string format, params object[] args)
	{
		Log.Sound.Print(format, args);
	}

	// Token: 0x060083E2 RID: 33762 RVA: 0x002AB45A File Offset: 0x002A965A
	[Conditional("SOUND_BUNDLE_DEBUG")]
	private void BundleScreenPrint(string format, params object[] args)
	{
		Log.Sound.ScreenPrint(format, args);
	}

	// Token: 0x060083E3 RID: 33763 RVA: 0x002AB44C File Offset: 0x002A964C
	[Conditional("SOUND_DUCKING_DEBUG")]
	private void DuckingPrint(string format, params object[] args)
	{
		Log.Sound.Print(format, args);
	}

	// Token: 0x060083E4 RID: 33764 RVA: 0x002AB45A File Offset: 0x002A965A
	[Conditional("SOUND_DUCKING_DEBUG")]
	private void DuckingScreenPrint(string format, params object[] args)
	{
		Log.Sound.ScreenPrint(format, args);
	}

	// Token: 0x060083E5 RID: 33765 RVA: 0x002AB538 File Offset: 0x002A9738
	private void AddExtensionMapping(AudioSource source, SoundManager.SourceExtension extension)
	{
		if (source == null || extension == null)
		{
			return;
		}
		SoundManager.ExtensionMapping extensionMapping = new SoundManager.ExtensionMapping();
		extensionMapping.Source = source;
		extensionMapping.Extension = extension;
		this.m_extensionMappings.Add(extensionMapping);
	}

	// Token: 0x060083E6 RID: 33766 RVA: 0x002AB574 File Offset: 0x002A9774
	private void RemoveExtensionMapping(AudioSource source)
	{
		for (int i = 0; i < this.m_extensionMappings.Count; i++)
		{
			if (this.m_extensionMappings[i].Source == source)
			{
				this.m_extensionMappings.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x060083E7 RID: 33767 RVA: 0x002AB5C0 File Offset: 0x002A97C0
	private SoundManager.SourceExtension GetExtension(AudioSource source)
	{
		for (int i = 0; i < this.m_extensionMappings.Count; i++)
		{
			SoundManager.ExtensionMapping extensionMapping = this.m_extensionMappings[i];
			if (extensionMapping.Source == source)
			{
				return extensionMapping.Extension;
			}
		}
		return null;
	}

	// Token: 0x060083E8 RID: 33768 RVA: 0x002AB608 File Offset: 0x002A9808
	public Dictionary<Global.SoundCategory, float> DuckingLevels()
	{
		Dictionary<Global.SoundCategory, float> dictionary = new Dictionary<Global.SoundCategory, float>();
		foreach (Global.SoundCategory soundCategory in (Global.SoundCategory[])Enum.GetValues(typeof(Global.SoundCategory)))
		{
			dictionary[soundCategory] = this.GetDuckingVolume(soundCategory);
		}
		return dictionary;
	}

	// Token: 0x04006E79 RID: 28281
	public const float VOLUME_SCALE_FACTOR = 1.75f;

	// Token: 0x04006E7A RID: 28282
	private static SoundManager s_instance;

	// Token: 0x04006E7B RID: 28283
	private SoundConfig m_config;

	// Token: 0x04006E7C RID: 28284
	private List<AudioSource> m_generatedSources = new List<AudioSource>();

	// Token: 0x04006E7D RID: 28285
	private List<SoundManager.ExtensionMapping> m_extensionMappings = new List<SoundManager.ExtensionMapping>();

	// Token: 0x04006E7E RID: 28286
	private Map<Global.SoundCategory, List<AudioSource>> m_sourcesByCategory = new Map<Global.SoundCategory, List<AudioSource>>();

	// Token: 0x04006E7F RID: 28287
	private Map<string, List<AudioSource>> m_sourcesByClipName = new Map<string, List<AudioSource>>();

	// Token: 0x04006E80 RID: 28288
	private Map<string, SoundManager.BundleInfo> m_bundleInfos = new Map<string, SoundManager.BundleInfo>();

	// Token: 0x04006E81 RID: 28289
	private Map<Global.SoundCategory, List<SoundManager.DuckState>> m_duckStates = new Map<Global.SoundCategory, List<SoundManager.DuckState>>();

	// Token: 0x04006E82 RID: 28290
	private uint m_nextDuckStateTweenId;

	// Token: 0x04006E83 RID: 28291
	private List<AudioSource> m_inactiveSources = new List<AudioSource>();

	// Token: 0x04006E84 RID: 28292
	private List<string> m_bundleInfosToRemove = new List<string>();

	// Token: 0x04006E85 RID: 28293
	private GameObject m_sceneObject;

	// Token: 0x04006E86 RID: 28294
	private Map<string, int> activeLimitedSounds = new Map<string, int>();

	// Token: 0x04006E87 RID: 28295
	private List<MusicTrack> m_musicTracks = new List<MusicTrack>();

	// Token: 0x04006E88 RID: 28296
	private List<MusicTrack> m_ambienceTracks = new List<MusicTrack>();

	// Token: 0x04006E89 RID: 28297
	private bool m_musicIsAboutToPlay;

	// Token: 0x04006E8A RID: 28298
	private bool m_ambienceIsAboutToPlay;

	// Token: 0x04006E8B RID: 28299
	private AudioSource m_currentMusicTrack;

	// Token: 0x04006E8C RID: 28300
	private AudioSource m_currentAmbienceTrack;

	// Token: 0x04006E8D RID: 28301
	private List<AudioSource> m_fadingTracks = new List<AudioSource>();

	// Token: 0x04006E8E RID: 28302
	private float m_musicTrackStartTime;

	// Token: 0x04006E8F RID: 28303
	private int m_musicTrackIndex;

	// Token: 0x04006E90 RID: 28304
	private int m_nextMusicTrackIndex;

	// Token: 0x04006E91 RID: 28305
	private int m_ambienceTrackIndex;

	// Token: 0x04006E92 RID: 28306
	private bool m_isMasterEnabled;

	// Token: 0x04006E93 RID: 28307
	private bool m_isMusicEnabled;

	// Token: 0x04006E94 RID: 28308
	private bool m_mute;

	// Token: 0x04006E95 RID: 28309
	private int m_nextSourceId = 1;

	// Token: 0x04006E96 RID: 28310
	private uint m_frame;

	// Token: 0x04006E97 RID: 28311
	private List<Coroutine> m_fadingTracksIn = new List<Coroutine>();

	// Token: 0x04006E98 RID: 28312
	public static readonly AssetReference FallbackSound = new AssetReference("tavern_crowd_play_reaction_very_positive_2.wav:07343a9a2cec38942b8fdbbafa9165d7");

	// Token: 0x04006E99 RID: 28313
	public Action OnMusicStarted;

	// Token: 0x02002613 RID: 9747
	// (Invoke) Token: 0x0601358D RID: 79245
	public delegate void LoadedCallback(AudioSource source, object userData);

	// Token: 0x02002614 RID: 9748
	public class SoundOptions
	{
		// Token: 0x17002C0C RID: 11276
		// (get) Token: 0x06013590 RID: 79248 RVA: 0x00531565 File Offset: 0x0052F765
		// (set) Token: 0x06013591 RID: 79249 RVA: 0x0053156D File Offset: 0x0052F76D
		public bool InstanceLimited { get; set; }

		// Token: 0x17002C0D RID: 11277
		// (get) Token: 0x06013592 RID: 79250 RVA: 0x00531576 File Offset: 0x0052F776
		// (set) Token: 0x06013593 RID: 79251 RVA: 0x0053157E File Offset: 0x0052F77E
		public float InstanceTimeLimit { get; set; }

		// Token: 0x17002C0E RID: 11278
		// (get) Token: 0x06013594 RID: 79252 RVA: 0x00531587 File Offset: 0x0052F787
		// (set) Token: 0x06013595 RID: 79253 RVA: 0x0053158F File Offset: 0x0052F78F
		public int MaxInstancesOfThisSound { get; set; } = 1;
	}

	// Token: 0x02002615 RID: 9749
	private class ExtensionMapping
	{
		// Token: 0x0400EF8C RID: 61324
		public AudioSource Source;

		// Token: 0x0400EF8D RID: 61325
		public SoundManager.SourceExtension Extension;
	}

	// Token: 0x02002616 RID: 9750
	private class SoundLoadContext
	{
		// Token: 0x06013598 RID: 79256 RVA: 0x005315A7 File Offset: 0x0052F7A7
		public void Init(GameObject parent, float volume, SoundManager.LoadedCallback callback, object userData)
		{
			this.m_parent = parent;
			this.m_volume = volume;
			this.Init(callback, userData);
		}

		// Token: 0x06013599 RID: 79257 RVA: 0x005315C0 File Offset: 0x0052F7C0
		public void Init(SoundManager.LoadedCallback callback, object userData)
		{
			SceneMgr sceneMgr;
			this.m_sceneMode = (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr) ? sceneMgr.GetMode() : SceneMgr.Mode.INVALID);
			this.m_haveCallback = (callback != null);
			this.m_callback = callback;
			this.m_userData = userData;
		}

		// Token: 0x0400EF8E RID: 61326
		public GameObject m_parent;

		// Token: 0x0400EF8F RID: 61327
		public float m_volume;

		// Token: 0x0400EF90 RID: 61328
		public SceneMgr.Mode m_sceneMode;

		// Token: 0x0400EF91 RID: 61329
		public bool m_haveCallback;

		// Token: 0x0400EF92 RID: 61330
		public SoundManager.LoadedCallback m_callback;

		// Token: 0x0400EF93 RID: 61331
		public object m_userData;
	}

	// Token: 0x02002617 RID: 9751
	private class SourceExtension
	{
		// Token: 0x0400EF94 RID: 61332
		public int m_id;

		// Token: 0x0400EF95 RID: 61333
		public float m_codeVolume = 1f;

		// Token: 0x0400EF96 RID: 61334
		public float m_sourceVolume = 1f;

		// Token: 0x0400EF97 RID: 61335
		public float m_defVolume = 1f;

		// Token: 0x0400EF98 RID: 61336
		public float m_codePitch = 1f;

		// Token: 0x0400EF99 RID: 61337
		public float m_sourcePitch = 1f;

		// Token: 0x0400EF9A RID: 61338
		public float m_defPitch = 1f;

		// Token: 0x0400EF9B RID: 61339
		public AudioClip m_sourceClip;

		// Token: 0x0400EF9C RID: 61340
		public bool m_paused;

		// Token: 0x0400EF9D RID: 61341
		public bool m_ducking;

		// Token: 0x0400EF9E RID: 61342
		public string m_bundleName;
	}

	// Token: 0x02002618 RID: 9752
	private class BundleInfo
	{
		// Token: 0x0601359C RID: 79260 RVA: 0x00531655 File Offset: 0x0052F855
		public string GetAssetRef()
		{
			return this.m_assetRef;
		}

		// Token: 0x0601359D RID: 79261 RVA: 0x00531662 File Offset: 0x0052F862
		public void SetAssetRef(AssetReference assetRef)
		{
			this.m_assetRef = assetRef;
		}

		// Token: 0x0601359E RID: 79262 RVA: 0x0053166B File Offset: 0x0052F86B
		public int GetRefCount()
		{
			return this.m_refs.Count;
		}

		// Token: 0x0601359F RID: 79263 RVA: 0x00531678 File Offset: 0x0052F878
		public List<AudioSource> GetRefs()
		{
			return this.m_refs;
		}

		// Token: 0x060135A0 RID: 79264 RVA: 0x00531680 File Offset: 0x0052F880
		public void AddRef(AudioSource instance)
		{
			this.m_garbageCollect = false;
			this.m_refs.Add(instance);
		}

		// Token: 0x060135A1 RID: 79265 RVA: 0x00531695 File Offset: 0x0052F895
		public bool RemoveRef(AudioSource instance)
		{
			return this.m_refs.Remove(instance);
		}

		// Token: 0x060135A2 RID: 79266 RVA: 0x005316A3 File Offset: 0x0052F8A3
		public bool CanGarbageCollect()
		{
			return this.m_garbageCollect && this.m_refs.Count <= 0;
		}

		// Token: 0x060135A3 RID: 79267 RVA: 0x005316C0 File Offset: 0x0052F8C0
		public bool IsGarbageCollectEnabled()
		{
			return this.m_garbageCollect;
		}

		// Token: 0x060135A4 RID: 79268 RVA: 0x005316C8 File Offset: 0x0052F8C8
		public void EnableGarbageCollect(bool enable)
		{
			this.m_garbageCollect = enable;
		}

		// Token: 0x0400EF9F RID: 61343
		private AssetReference m_assetRef;

		// Token: 0x0400EFA0 RID: 61344
		private List<AudioSource> m_refs = new List<AudioSource>();

		// Token: 0x0400EFA1 RID: 61345
		private bool m_garbageCollect;
	}

	// Token: 0x02002619 RID: 9753
	private enum DuckMode
	{
		// Token: 0x0400EFA3 RID: 61347
		IDLE,
		// Token: 0x0400EFA4 RID: 61348
		BEGINNING,
		// Token: 0x0400EFA5 RID: 61349
		HOLD,
		// Token: 0x0400EFA6 RID: 61350
		RESTORING
	}

	// Token: 0x0200261A RID: 9754
	private class DuckState
	{
		// Token: 0x060135A6 RID: 79270 RVA: 0x005316E4 File Offset: 0x0052F8E4
		public object GetTrigger()
		{
			return this.m_trigger;
		}

		// Token: 0x060135A7 RID: 79271 RVA: 0x005316EC File Offset: 0x0052F8EC
		public void SetTrigger(object trigger)
		{
			this.m_trigger = trigger;
			AudioSource audioSource = trigger as AudioSource;
			if (audioSource != null)
			{
				this.m_triggerCategory = SoundManager.Get().GetCategory(audioSource);
			}
		}

		// Token: 0x060135A8 RID: 79272 RVA: 0x00531721 File Offset: 0x0052F921
		public bool IsTrigger(object trigger)
		{
			return this.m_trigger == trigger;
		}

		// Token: 0x060135A9 RID: 79273 RVA: 0x0053172C File Offset: 0x0052F92C
		public bool IsTriggerAlive()
		{
			return GeneralUtils.IsObjectAlive(this.m_trigger);
		}

		// Token: 0x060135AA RID: 79274 RVA: 0x00531739 File Offset: 0x0052F939
		public Global.SoundCategory GetTriggerCategory()
		{
			return this.m_triggerCategory;
		}

		// Token: 0x060135AB RID: 79275 RVA: 0x00531741 File Offset: 0x0052F941
		public SoundDuckedCategoryDef GetDuckedDef()
		{
			return this.m_duckedDef;
		}

		// Token: 0x060135AC RID: 79276 RVA: 0x00531749 File Offset: 0x0052F949
		public void SetDuckedDef(SoundDuckedCategoryDef def)
		{
			this.m_duckedDef = def;
		}

		// Token: 0x060135AD RID: 79277 RVA: 0x00531752 File Offset: 0x0052F952
		public SoundManager.DuckMode GetMode()
		{
			return this.m_mode;
		}

		// Token: 0x060135AE RID: 79278 RVA: 0x0053175A File Offset: 0x0052F95A
		public void SetMode(SoundManager.DuckMode mode)
		{
			this.m_mode = mode;
		}

		// Token: 0x060135AF RID: 79279 RVA: 0x00531763 File Offset: 0x0052F963
		public string GetTweenName()
		{
			return this.m_tweenName;
		}

		// Token: 0x060135B0 RID: 79280 RVA: 0x0053176B File Offset: 0x0052F96B
		public void SetTweenName(string name)
		{
			this.m_tweenName = name;
		}

		// Token: 0x060135B1 RID: 79281 RVA: 0x00531774 File Offset: 0x0052F974
		public float GetVolume()
		{
			return this.m_volume;
		}

		// Token: 0x060135B2 RID: 79282 RVA: 0x0053177C File Offset: 0x0052F97C
		public void SetVolume(float volume)
		{
			this.m_volume = volume;
		}

		// Token: 0x0400EFA7 RID: 61351
		private object m_trigger;

		// Token: 0x0400EFA8 RID: 61352
		private Global.SoundCategory m_triggerCategory;

		// Token: 0x0400EFA9 RID: 61353
		private SoundDuckedCategoryDef m_duckedDef;

		// Token: 0x0400EFAA RID: 61354
		private SoundManager.DuckMode m_mode;

		// Token: 0x0400EFAB RID: 61355
		private string m_tweenName;

		// Token: 0x0400EFAC RID: 61356
		private float m_volume = 1f;
	}
}
