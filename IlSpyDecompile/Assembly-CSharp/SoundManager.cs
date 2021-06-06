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

public class SoundManager : IService, IHasFixedUpdate, IHasUpdate
{
	public delegate void LoadedCallback(AudioSource source, object userData);

	public class SoundOptions
	{
		public bool InstanceLimited { get; set; }

		public float InstanceTimeLimit { get; set; }

		public int MaxInstancesOfThisSound { get; set; } = 1;

	}

	private class ExtensionMapping
	{
		public AudioSource Source;

		public SourceExtension Extension;
	}

	private class SoundLoadContext
	{
		public GameObject m_parent;

		public float m_volume;

		public SceneMgr.Mode m_sceneMode;

		public bool m_haveCallback;

		public LoadedCallback m_callback;

		public object m_userData;

		public void Init(GameObject parent, float volume, LoadedCallback callback, object userData)
		{
			m_parent = parent;
			m_volume = volume;
			Init(callback, userData);
		}

		public void Init(LoadedCallback callback, object userData)
		{
			m_sceneMode = (HearthstoneServices.TryGet<SceneMgr>(out var service) ? service.GetMode() : SceneMgr.Mode.INVALID);
			m_haveCallback = callback != null;
			m_callback = callback;
			m_userData = userData;
		}
	}

	private class SourceExtension
	{
		public int m_id;

		public float m_codeVolume = 1f;

		public float m_sourceVolume = 1f;

		public float m_defVolume = 1f;

		public float m_codePitch = 1f;

		public float m_sourcePitch = 1f;

		public float m_defPitch = 1f;

		public AudioClip m_sourceClip;

		public bool m_paused;

		public bool m_ducking;

		public string m_bundleName;
	}

	private class BundleInfo
	{
		private AssetReference m_assetRef;

		private List<AudioSource> m_refs = new List<AudioSource>();

		private bool m_garbageCollect;

		public string GetAssetRef()
		{
			return m_assetRef;
		}

		public void SetAssetRef(AssetReference assetRef)
		{
			m_assetRef = assetRef;
		}

		public int GetRefCount()
		{
			return m_refs.Count;
		}

		public List<AudioSource> GetRefs()
		{
			return m_refs;
		}

		public void AddRef(AudioSource instance)
		{
			m_garbageCollect = false;
			m_refs.Add(instance);
		}

		public bool RemoveRef(AudioSource instance)
		{
			return m_refs.Remove(instance);
		}

		public bool CanGarbageCollect()
		{
			if (!m_garbageCollect)
			{
				return false;
			}
			if (m_refs.Count > 0)
			{
				return false;
			}
			return true;
		}

		public bool IsGarbageCollectEnabled()
		{
			return m_garbageCollect;
		}

		public void EnableGarbageCollect(bool enable)
		{
			m_garbageCollect = enable;
		}
	}

	private enum DuckMode
	{
		IDLE,
		BEGINNING,
		HOLD,
		RESTORING
	}

	private class DuckState
	{
		private object m_trigger;

		private Global.SoundCategory m_triggerCategory;

		private SoundDuckedCategoryDef m_duckedDef;

		private DuckMode m_mode;

		private string m_tweenName;

		private float m_volume = 1f;

		public object GetTrigger()
		{
			return m_trigger;
		}

		public void SetTrigger(object trigger)
		{
			m_trigger = trigger;
			AudioSource audioSource = trigger as AudioSource;
			if (audioSource != null)
			{
				m_triggerCategory = Get().GetCategory(audioSource);
			}
		}

		public bool IsTrigger(object trigger)
		{
			return m_trigger == trigger;
		}

		public bool IsTriggerAlive()
		{
			return GeneralUtils.IsObjectAlive(m_trigger);
		}

		public Global.SoundCategory GetTriggerCategory()
		{
			return m_triggerCategory;
		}

		public SoundDuckedCategoryDef GetDuckedDef()
		{
			return m_duckedDef;
		}

		public void SetDuckedDef(SoundDuckedCategoryDef def)
		{
			m_duckedDef = def;
		}

		public DuckMode GetMode()
		{
			return m_mode;
		}

		public void SetMode(DuckMode mode)
		{
			m_mode = mode;
		}

		public string GetTweenName()
		{
			return m_tweenName;
		}

		public void SetTweenName(string name)
		{
			m_tweenName = name;
		}

		public float GetVolume()
		{
			return m_volume;
		}

		public void SetVolume(float volume)
		{
			m_volume = volume;
		}
	}

	public const float VOLUME_SCALE_FACTOR = 1.75f;

	private static SoundManager s_instance;

	private SoundConfig m_config;

	private List<AudioSource> m_generatedSources = new List<AudioSource>();

	private List<ExtensionMapping> m_extensionMappings = new List<ExtensionMapping>();

	private Map<Global.SoundCategory, List<AudioSource>> m_sourcesByCategory = new Map<Global.SoundCategory, List<AudioSource>>();

	private Map<string, List<AudioSource>> m_sourcesByClipName = new Map<string, List<AudioSource>>();

	private Map<string, BundleInfo> m_bundleInfos = new Map<string, BundleInfo>();

	private Map<Global.SoundCategory, List<DuckState>> m_duckStates = new Map<Global.SoundCategory, List<DuckState>>();

	private uint m_nextDuckStateTweenId;

	private List<AudioSource> m_inactiveSources = new List<AudioSource>();

	private List<string> m_bundleInfosToRemove = new List<string>();

	private GameObject m_sceneObject;

	private Map<string, int> activeLimitedSounds = new Map<string, int>();

	private List<MusicTrack> m_musicTracks = new List<MusicTrack>();

	private List<MusicTrack> m_ambienceTracks = new List<MusicTrack>();

	private bool m_musicIsAboutToPlay;

	private bool m_ambienceIsAboutToPlay;

	private AudioSource m_currentMusicTrack;

	private AudioSource m_currentAmbienceTrack;

	private List<AudioSource> m_fadingTracks = new List<AudioSource>();

	private float m_musicTrackStartTime;

	private int m_musicTrackIndex;

	private int m_nextMusicTrackIndex;

	private int m_ambienceTrackIndex;

	private bool m_isMasterEnabled;

	private bool m_isMusicEnabled;

	private bool m_mute;

	private int m_nextSourceId = 1;

	private uint m_frame;

	private List<Coroutine> m_fadingTracksIn = new List<Coroutine>();

	public static readonly AssetReference FallbackSound = new AssetReference("tavern_crowd_play_reaction_very_positive_2.wav:07343a9a2cec38942b8fdbbafa9165d7");

	public Action OnMusicStarted;

	private GameObject SceneObject
	{
		get
		{
			if (m_sceneObject == null)
			{
				m_sceneObject = new GameObject("SoundManagerSceneObject", typeof(HSDontDestroyOnLoad));
			}
			return m_sceneObject;
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		InstantiatePrefab instantiateSoundConfigPrefab = new InstantiatePrefab("SoundConfig.prefab:cd41c731c777d4f468b79ffa365a9f94");
		yield return instantiateSoundConfigPrefab;
		SetConfig(instantiateSoundConfigPrefab.InstantiatedPrefab.GetComponent<SoundConfig>());
		Options.Get().RegisterChangedListener(Option.SOUND, OnMasterEnabledOptionChanged);
		Options.Get().RegisterChangedListener(Option.SOUND_VOLUME, OnMasterVolumeOptionChanged);
		Options.Get().RegisterChangedListener(Option.MUSIC, OnEnabledOptionChanged);
		Options.Get().RegisterChangedListener(Option.MUSIC_VOLUME, OnVolumeOptionChanged);
		Options.Get().RegisterChangedListener(Option.BACKGROUND_SOUND, OnBackgroundSoundOptionChanged);
		m_isMasterEnabled = Options.Get().GetBool(Option.SOUND);
		m_isMusicEnabled = Options.Get().GetBool(Option.MUSIC);
		SetMasterVolumeExponential();
		UpdateAppMute();
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.AddFocusChangedListener(OnAppFocusChanged);
		}
		yield return new ServiceSoftDependency(typeof(SceneMgr), serviceLocator);
		if (serviceLocator.TryGetService<SceneMgr>(out var service))
		{
			service.RegisterSceneLoadedEvent(OnSceneLoaded);
		}
	}

	public Type[] GetDependencies()
	{
		return new Type[1] { typeof(IAssetLoader) };
	}

	public void Shutdown()
	{
		s_instance = null;
	}

	public void Update()
	{
		m_frame = (m_frame + 1) & 0xFFFFFFFFu;
		UpdateMusicAndAmbience();
	}

	public void FixedUpdate()
	{
		UpdateSources();
	}

	public float GetSecondsBetweenUpdates()
	{
		return 1f;
	}

	public static SoundManager Get()
	{
		if (s_instance == null)
		{
			s_instance = HearthstoneServices.Get<SoundManager>();
		}
		return s_instance;
	}

	public SoundConfig GetConfig()
	{
		return m_config;
	}

	public void SetConfig(SoundConfig config)
	{
		m_config = config;
	}

	public bool IsInitialized()
	{
		return m_config != null;
	}

	public GameObject GetPlaceholderSound()
	{
		AudioSource placeholderSource = GetPlaceholderSource();
		if (placeholderSource == null)
		{
			return null;
		}
		return placeholderSource.gameObject;
	}

	public AudioSource GetPlaceholderSource()
	{
		if (m_config == null)
		{
			return null;
		}
		if (HearthstoneApplication.IsInternal())
		{
			return m_config.m_PlaceholderSound;
		}
		return null;
	}

	public SoundDef GetSoundDef(AudioSource source)
	{
		return source.gameObject.GetComponent<SoundDef>();
	}

	private void SetMasterVolumeExponential()
	{
		AudioListener.volume = Mathf.Pow(Mathf.Clamp01(Options.Get().GetFloat(Option.SOUND_VOLUME)), 1.75f);
	}

	public bool Play(AudioSource source, SoundDef oneShotDef = null, AudioClip oneShotClip = null, SoundOptions options = null)
	{
		return PlayImpl(source, oneShotDef, oneShotClip, options);
	}

	public bool PlayOneShot(AudioSource source, SoundDef oneShotDef, float volume = 1f, SoundOptions options = null)
	{
		if (!PlayImpl(source, oneShotDef, null, options))
		{
			return false;
		}
		if (IsActive(source))
		{
			SetVolume(source, volume);
		}
		return true;
	}

	public bool IsPlaying(AudioSource source)
	{
		if (source == null)
		{
			return false;
		}
		return source.isPlaying;
	}

	public bool Pause(AudioSource source)
	{
		if (source == null)
		{
			return false;
		}
		if (IsPaused(source))
		{
			return false;
		}
		SourceExtension sourceExtension = RegisterExtension(source);
		if (sourceExtension == null)
		{
			return false;
		}
		sourceExtension.m_paused = true;
		UpdateSource(source, sourceExtension);
		source.Pause();
		return true;
	}

	public bool IsPaused(AudioSource source)
	{
		if (source == null)
		{
			return false;
		}
		return GetExtension(source)?.m_paused ?? false;
	}

	public bool Stop(AudioSource source)
	{
		if (source == null)
		{
			return false;
		}
		if (!IsActive(source))
		{
			return false;
		}
		source.Stop();
		FinishSource(source);
		return true;
	}

	public void Destroy(AudioSource source)
	{
		if (!(source == null))
		{
			FinishSource(source);
		}
	}

	public void DestroyAll(Global.SoundCategory category)
	{
		List<AudioSource> list = new List<AudioSource>();
		for (int i = 0; i < m_generatedSources.Count; i++)
		{
			AudioSource audioSource = m_generatedSources[i];
			SoundDef component = audioSource.GetComponent<SoundDef>();
			if (component.m_Category == category && !component.m_persistPastGameEnd)
			{
				list.Add(audioSource);
			}
		}
		foreach (AudioSource item in list)
		{
			Destroy(item);
		}
	}

	public bool IsActive(AudioSource source)
	{
		if (source == null)
		{
			return false;
		}
		if (IsPlaying(source))
		{
			return true;
		}
		if (IsPaused(source))
		{
			return true;
		}
		return false;
	}

	public bool IsPlaybackFinished(AudioSource source)
	{
		if (source == null || source.clip == null)
		{
			return false;
		}
		return source.timeSamples >= source.clip.samples;
	}

	public float GetVolume(AudioSource source)
	{
		if (source == null)
		{
			return 1f;
		}
		return RegisterExtension(source)?.m_codeVolume ?? 1f;
	}

	public void SetVolume(AudioSource source, float volume)
	{
		if (!(source == null))
		{
			SourceExtension sourceExtension = RegisterExtension(source);
			if (sourceExtension != null)
			{
				sourceExtension.m_codeVolume = volume;
				UpdateVolume(source, sourceExtension);
			}
		}
	}

	public float GetPitch(AudioSource source)
	{
		if (source == null)
		{
			return 1f;
		}
		return RegisterExtension(source)?.m_codePitch ?? 1f;
	}

	public void SetPitch(AudioSource source, float pitch)
	{
		if (!(source == null))
		{
			SourceExtension sourceExtension = RegisterExtension(source);
			if (sourceExtension != null)
			{
				sourceExtension.m_codePitch = pitch;
				UpdatePitch(source, sourceExtension);
			}
		}
	}

	public Global.SoundCategory GetCategory(AudioSource source)
	{
		if (source == null)
		{
			return Global.SoundCategory.NONE;
		}
		return GetDefFromSource(source).m_Category;
	}

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
		UpdateSource(source);
	}

	public bool Is3d(AudioSource source)
	{
		if (source == null)
		{
			return false;
		}
		return source.spatialBlend >= 1f;
	}

	public void Set3d(AudioSource source, bool enable)
	{
		if (!(source == null))
		{
			source.spatialBlend = (enable ? 1f : 0f);
		}
	}

	public AudioSource GetCurrentMusicTrack()
	{
		return m_currentMusicTrack;
	}

	public AudioSource GetCurrentAmbienceTrack()
	{
		return m_currentAmbienceTrack;
	}

	public bool Load(AssetReference assetRef)
	{
		if (!AssetLoader.Get().IsAssetAvailable(assetRef))
		{
			return false;
		}
		return SoundLoader.LoadSound(assetRef, OnLoadSoundLoaded);
	}

	public void LoadAndPlay(AssetReference assetRef)
	{
		LoadAndPlay(assetRef, null, 1f, null, null);
	}

	public void LoadAndPlay(AssetReference assetRef, float volume)
	{
		LoadAndPlay(assetRef, null, volume, null, null);
	}

	public void LoadAndPlay(AssetReference assetRef, GameObject parent)
	{
		LoadAndPlay(assetRef, parent, 1f, null, null);
	}

	public void LoadAndPlay(AssetReference assetRef, GameObject parent, float volume)
	{
		LoadAndPlay(assetRef, parent, volume, null, null);
	}

	public void LoadAndPlay(AssetReference assetRef, GameObject parent, float volume, LoadedCallback callback)
	{
		LoadAndPlay(assetRef, parent, volume, callback, null);
	}

	public void LoadAndPlay(AssetReference assetRef, GameObject parent, float volume, LoadedCallback callback, object callbackData)
	{
		if (string.IsNullOrEmpty(assetRef))
		{
			Log.Sound.PrintWarning("Missing assetref for LoadAndPlay().");
			callback?.Invoke(null, callbackData);
		}
		else
		{
			SoundLoadContext soundLoadContext = new SoundLoadContext();
			soundLoadContext.Init(parent, volume, callback, callbackData);
			SoundLoader.LoadSound(assetRef, OnLoadAndPlaySoundLoaded, soundLoadContext, GetPlaceholderSound());
		}
	}

	public void PlayPreloaded(AudioSource source)
	{
		PlayPreloaded(source, null);
	}

	public void PlayPreloaded(AudioSource source, float volume)
	{
		PlayPreloaded(source, null, volume);
	}

	public void PlayPreloaded(AudioSource source, GameObject parentObject)
	{
		PlayPreloaded(source, parentObject, 1f);
	}

	public void PlayPreloaded(AudioSource source, GameObject parentObject, float volume)
	{
		if (source == null)
		{
			UnityEngine.Debug.LogError("Preloaded audio source is null! Cannot play!");
			return;
		}
		SourceExtension sourceExtension = RegisterExtension(source);
		if (sourceExtension != null)
		{
			sourceExtension.m_codeVolume = volume;
		}
		InitSourceTransform(source, parentObject);
		m_generatedSources.Add(source);
		Play(source);
	}

	public AudioSource PlayClip(SoundPlayClipArgs args, bool createNewSource = true, SoundOptions options = null)
	{
		if (args == null || (args.m_def == null && args.m_forcedAudioClip == null))
		{
			UnityEngine.Debug.LogWarningFormat("PlayClip: using placeholder sound for audio clip: {0}", (args != null) ? args.ToString() : "");
			return PlayImpl(null, null);
		}
		AudioSource audioSource = null;
		if (createNewSource)
		{
			audioSource = GenerateAudioSource(args.m_templateSource, args.m_def);
		}
		else
		{
			audioSource = args.m_def.GetComponent<AudioSource>();
			if (audioSource != null)
			{
				m_generatedSources.Add(audioSource);
			}
			else
			{
				Log.Asset.PrintWarning("PlayClip: Loaded sound asset missing AudioSource. Generating new one...");
				audioSource = GenerateAudioSource(args.m_templateSource, args.m_def);
			}
		}
		if (args.m_forcedAudioClip != null)
		{
			audioSource.clip = args.m_forcedAudioClip;
		}
		if (args.m_volume.HasValue)
		{
			audioSource.volume = args.m_volume.Value;
		}
		if (args.m_pitch.HasValue)
		{
			audioSource.pitch = args.m_pitch.Value;
		}
		if (args.m_spatialBlend.HasValue)
		{
			audioSource.spatialBlend = args.m_spatialBlend.Value;
		}
		if (args.m_category.HasValue)
		{
			audioSource.GetComponent<SoundDef>().m_Category = args.m_category.Value;
		}
		InitSourceTransform(audioSource, args.m_parentObject);
		if (args.m_forcedAudioClip != null)
		{
			if (Play(audioSource, null, args.m_forcedAudioClip))
			{
				return audioSource;
			}
		}
		else if (Play(audioSource, args.m_def, null, options))
		{
			return audioSource;
		}
		FinishGeneratedSource(audioSource);
		return null;
	}

	public bool LoadAndPlayClip(AssetReference assetRef, SoundPlayClipArgs args)
	{
		if (string.IsNullOrEmpty(assetRef))
		{
			Log.Sound.PrintError("LoadAndPlayClip: Missing asset AssetReference!");
			return false;
		}
		if (!AssetLoader.Get().IsAssetAvailable(assetRef))
		{
			return false;
		}
		if (args == null)
		{
			Log.Sound.PrintWarning("LoadAndPlayClip: Missing SoundPlayClipArgs. Using default...");
			args = new SoundPlayClipArgs
			{
				m_category = Global.SoundCategory.FX
			};
		}
		return SoundLoader.LoadSound(assetRef, OnLoadAndPlayClipLoaded, args);
	}

	private void OnLoadSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (assetRef == null)
		{
			UnityEngine.Debug.LogErrorFormat("SoundManager.OnLoadSoundLoaded() - ERROR Tried to load null assetRef!", assetRef, go);
			return;
		}
		if (go == null)
		{
			UnityEngine.Debug.LogErrorFormat("SoundManager.OnLoadSoundLoaded() - ERROR assetRef=\"{0}\" go=\"{1}\" failed to load", assetRef, go);
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			UnityEngine.Object.DestroyImmediate(go);
			UnityEngine.Debug.LogErrorFormat("SoundManager.OnLoadSoundLoaded() - ERROR assetRef=\"{0}\" has no AudioSource", assetRef);
			return;
		}
		RegisterSourceBundle(assetRef, component);
		component.volume = 0f;
		component.Play();
		component.Stop();
		UnregisterSourceBundle(assetRef.ToString(), component);
		UnityEngine.Object.DestroyImmediate(component.gameObject);
	}

	private void OnLoadAndPlaySoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			UnityEngine.Debug.LogError($"SoundManager.OnLoadAndPlaySoundLoaded() - ERROR \"{assetRef}\" failed to load");
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			UnityEngine.Object.DestroyImmediate(go);
			UnityEngine.Debug.LogError($"SoundManager.OnLoadAndPlaySoundLoaded() - ERROR \"{assetRef}\" has no AudioSource");
			return;
		}
		SoundLoadContext soundLoadContext = (SoundLoadContext)callbackData;
		if (soundLoadContext.m_sceneMode != SceneMgr.Mode.FATAL_ERROR && SceneMgr.Get() != null && SceneMgr.Get().IsModeRequested(SceneMgr.Mode.FATAL_ERROR))
		{
			UnityEngine.Object.DestroyImmediate(go);
			return;
		}
		if (RegisterSourceBundle(assetRef, component) == null)
		{
			UnityEngine.Debug.LogWarningFormat("Failed to load and play sound name={0}, go={1} (this may be due to it not yet being downloaded)", assetRef, go.name);
			return;
		}
		if (soundLoadContext.m_haveCallback && !GeneralUtils.IsCallbackValid(soundLoadContext.m_callback))
		{
			UnityEngine.Object.DestroyImmediate(go);
			UnregisterSourceBundle(SceneObject.name, component);
			return;
		}
		m_generatedSources.Add(component);
		RegisterExtension(component).m_codeVolume = soundLoadContext.m_volume;
		InitSourceTransform(component, soundLoadContext.m_parent);
		Play(component);
		if (soundLoadContext.m_callback != null)
		{
			soundLoadContext.m_callback(component, soundLoadContext.m_userData);
		}
	}

	private void OnLoadAndPlayClipLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Log.Sound.PrintError("LoadAndPlayClip: Sound asset \"{0}\" failed to load", assetRef);
			return;
		}
		SoundDef component = go.GetComponent<SoundDef>();
		if (component == null)
		{
			Log.Sound.PrintError("LoadAndPlayClip: SoundDef missing from asset! Aborting playing \"{0}\"", assetRef);
			UnityEngine.Object.DestroyImmediate(go);
		}
		else
		{
			SoundPlayClipArgs soundPlayClipArgs = (SoundPlayClipArgs)callbackData;
			soundPlayClipArgs.m_def = component;
			PlayClip(soundPlayClipArgs, createNewSource: false);
		}
	}

	public void AddMusicTracks(List<MusicTrack> tracks)
	{
		AddTracks(tracks, m_musicTracks);
	}

	public void AddAmbienceTracks(List<MusicTrack> tracks)
	{
		AddTracks(tracks, m_ambienceTracks);
	}

	public List<MusicTrack> GetCurrentMusicTracks()
	{
		return m_musicTracks;
	}

	public List<MusicTrack> GetCurrentAmbienceTracks()
	{
		return m_ambienceTracks;
	}

	public int GetCurrentMusicTrackIndex()
	{
		return m_musicTrackIndex;
	}

	public void SetCurrentMusicTrackIndex(int idx)
	{
		if (m_musicTrackIndex != idx)
		{
			m_musicIsAboutToPlay = PlayMusicTrack(idx);
		}
	}

	public void SetCurrentMusicTrackTime(float time)
	{
		if ((bool)m_currentMusicTrack)
		{
			m_currentMusicTrack.time = time;
		}
		else
		{
			m_musicTrackStartTime = time;
		}
	}

	public void StopCurrentMusicTrack()
	{
		if (m_currentMusicTrack != null)
		{
			FadeTrackOut(m_currentMusicTrack);
			ChangeCurrentMusicTrack(null);
		}
	}

	public void StopCurrentAmbienceTrack()
	{
		if (m_currentAmbienceTrack != null)
		{
			FadeTrackOut(m_currentAmbienceTrack);
			ChangeCurrentAmbienceTrack(null);
		}
	}

	public void NukeMusicAndAmbiencePlaylists()
	{
		m_musicTracks.Clear();
		m_ambienceTracks.Clear();
		m_nextMusicTrackIndex = 0;
		m_musicTrackIndex = 0;
		m_ambienceTrackIndex = 0;
	}

	public void NukePlaylistsAndStopPlayingCurrentTracks()
	{
		NukeMusicAndAmbiencePlaylists();
		StopCurrentMusicTrack();
		StopCurrentAmbienceTrack();
	}

	public void NukeMusicAndStopPlayingCurrentTrack()
	{
		m_musicTracks.Clear();
		m_nextMusicTrackIndex = 0;
		m_musicTrackIndex = 0;
		StopCurrentMusicTrack();
	}

	public void NukeAmbienceAndStopPlayingCurrentTrack()
	{
		m_ambienceTracks.Clear();
		m_ambienceTrackIndex = 0;
		StopCurrentAmbienceTrack();
	}

	public void ImmediatelyKillMusicAndAmbience()
	{
		NukeMusicAndAmbiencePlaylists();
		AudioSource[] array = m_fadingTracks.ToArray();
		foreach (AudioSource source in array)
		{
			FinishSource(source);
		}
		if (m_currentMusicTrack != null)
		{
			FinishSource(m_currentMusicTrack);
			ChangeCurrentMusicTrack(null);
		}
		if (m_currentAmbienceTrack != null)
		{
			FinishSource(m_currentAmbienceTrack);
			ChangeCurrentAmbienceTrack(null);
		}
	}

	private void AddTracks(List<MusicTrack> sourceTracks, List<MusicTrack> destTracks)
	{
		foreach (MusicTrack sourceTrack in sourceTracks)
		{
			destTracks.Add(sourceTrack);
		}
	}

	private void OnMusicLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			UnityEngine.Debug.LogError($"SoundManager.OnMusicLoaded() - ERROR \"{assetRef}\" failed to load");
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			UnityEngine.Debug.LogError($"SoundManager.OnMusicLoaded() - ERROR \"{SceneObject.name}\" has no AudioSource");
			return;
		}
		RegisterSourceBundle(assetRef, component);
		MusicTrack musicTrack = (MusicTrack)callbackData;
		if (m_musicTrackIndex >= m_musicTracks.Count || m_musicTracks[m_musicTrackIndex] != musicTrack)
		{
			UnregisterSourceBundle(assetRef, component);
			UnityEngine.Object.DestroyImmediate(go);
		}
		else
		{
			m_generatedSources.Add(component);
			component.transform.parent = SceneObject.transform;
			component.volume *= musicTrack.m_volume;
			component.time = m_musicTrackStartTime;
			m_musicTrackStartTime = 0f;
			ChangeCurrentMusicTrack(component);
			Play(component);
			if (OnMusicStarted != null)
			{
				OnMusicStarted();
			}
		}
		m_musicIsAboutToPlay = false;
	}

	private void OnAmbienceLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			UnityEngine.Debug.LogError($"SoundManager.OnAmbienceLoaded() - ERROR \"{assetRef}\" failed to load");
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			UnityEngine.Debug.LogError($"SoundManager.OnAmbienceLoaded() - ERROR \"{SceneObject.name}\" has no AudioSource");
			return;
		}
		RegisterSourceBundle(assetRef, component);
		MusicTrack musicTrack = (MusicTrack)callbackData;
		if (!m_ambienceTracks.Contains(musicTrack))
		{
			UnregisterSourceBundle(assetRef, component);
			UnityEngine.Object.DestroyImmediate(go);
		}
		else
		{
			m_generatedSources.Add(component);
			component.transform.parent = SceneObject.transform;
			component.volume *= musicTrack.m_volume;
			ChangeCurrentAmbienceTrack(component);
			m_fadingTracksIn.Add(Processor.RunCoroutine(FadeTrackIn(component)));
			Play(component);
		}
		m_ambienceIsAboutToPlay = false;
	}

	private void ChangeCurrentMusicTrack(AudioSource source)
	{
		m_currentMusicTrack = source;
	}

	private void ChangeCurrentAmbienceTrack(AudioSource source)
	{
		m_currentAmbienceTrack = source;
	}

	private void UpdateMusicAndAmbience()
	{
		if (!IsMusicEnabled())
		{
			return;
		}
		if (!m_musicIsAboutToPlay)
		{
			if (m_currentMusicTrack != null)
			{
				if (!IsPlaying(m_currentMusicTrack))
				{
					Processor.RunCoroutine(PlayMusicInSeconds(m_config.m_SecondsBetweenMusicTracks));
				}
			}
			else
			{
				m_musicIsAboutToPlay = PlayNextMusic();
			}
		}
		if (m_ambienceIsAboutToPlay)
		{
			return;
		}
		if (m_currentAmbienceTrack != null)
		{
			if (!IsPlaying(m_currentAmbienceTrack))
			{
				Processor.RunCoroutine(PlayAmbienceInSeconds(0f));
			}
		}
		else
		{
			m_ambienceIsAboutToPlay = PlayNextAmbience();
		}
	}

	private IEnumerator PlayMusicInSeconds(float seconds)
	{
		m_musicIsAboutToPlay = true;
		yield return new WaitForSeconds(seconds);
		m_musicIsAboutToPlay = PlayNextMusic();
	}

	private bool PlayNextMusic()
	{
		if (!IsMusicEnabled())
		{
			return false;
		}
		if (m_musicTracks.Count <= 0)
		{
			return false;
		}
		return PlayMusicTrack(m_nextMusicTrackIndex);
	}

	private bool PlayMusicTrack(int index)
	{
		if (index < 0 || index >= m_musicTracks.Count)
		{
			return false;
		}
		m_musicTrackIndex = index;
		MusicTrack musicTrack = m_musicTracks[m_musicTrackIndex];
		m_nextMusicTrackIndex = (index + 1) % m_musicTracks.Count;
		if (musicTrack == null)
		{
			return false;
		}
		if (m_currentMusicTrack != null)
		{
			FadeTrackOut(m_currentMusicTrack);
			ChangeCurrentMusicTrack(null);
		}
		return SoundLoader.LoadSound(AssetLoader.Get().IsAssetAvailable(musicTrack.m_name) ? musicTrack.m_name : musicTrack.m_fallback, OnMusicLoaded, musicTrack, GetPlaceholderSound());
	}

	private bool IsMusicEnabled()
	{
		if (!SoundUtils.IsDeviceBackgroundMusicPlaying() && m_isMasterEnabled)
		{
			return m_isMusicEnabled;
		}
		return false;
	}

	private IEnumerator PlayAmbienceInSeconds(float seconds)
	{
		m_ambienceIsAboutToPlay = true;
		yield return new WaitForSeconds(seconds);
		m_ambienceIsAboutToPlay = PlayNextAmbience();
	}

	private bool PlayNextAmbience()
	{
		if (!IsMusicEnabled())
		{
			return false;
		}
		if (m_ambienceTracks.Count <= 0)
		{
			return false;
		}
		MusicTrack musicTrack = m_ambienceTracks[m_ambienceTrackIndex];
		m_ambienceTrackIndex = (m_ambienceTrackIndex + 1) % m_ambienceTracks.Count;
		if (musicTrack == null)
		{
			return false;
		}
		string text = (AssetLoader.Get().IsAssetAvailable(musicTrack.m_name) ? musicTrack.m_name : musicTrack.m_fallback);
		foreach (Coroutine item in m_fadingTracksIn)
		{
			if (item != null)
			{
				Processor.CancelCoroutine(item);
			}
		}
		m_fadingTracksIn.Clear();
		return SoundLoader.LoadSound(text, OnAmbienceLoaded, musicTrack, GetPlaceholderSound());
	}

	private void FadeTrackOut(AudioSource source)
	{
		if (!IsActive(source))
		{
			FinishSource(source);
		}
		else
		{
			Processor.RunCoroutine(FadeTrack(source, 0f));
		}
	}

	private IEnumerator FadeTrackIn(AudioSource source)
	{
		SourceExtension ext = GetExtension(source);
		if (ext == null)
		{
			Log.Sound.PrintWarning("Unable to find extension for sound {0}", source.name);
			yield break;
		}
		float targetVolume = GetVolume(source);
		float currTime = 0f;
		float targetVolumeTime = 1f;
		ext.m_codeVolume = 0f;
		UpdateVolume(source, ext);
		while (ext.m_codeVolume < targetVolume)
		{
			currTime += Time.deltaTime;
			ext.m_codeVolume = Mathf.Lerp(0f, targetVolume, Mathf.Clamp01(currTime / targetVolumeTime));
			UpdateVolume(source, ext);
			yield return null;
			if (source == null || !IsActive(source))
			{
				break;
			}
		}
	}

	private IEnumerator FadeTrack(AudioSource source, float targetVolume)
	{
		m_fadingTracks.Add(source);
		SourceExtension ext = GetExtension(source);
		while (ext.m_codeVolume > 0.0001f)
		{
			ext.m_codeVolume = Mathf.Lerp(ext.m_codeVolume, targetVolume, Time.deltaTime);
			UpdateVolume(source, ext);
			yield return null;
			if (source == null || !IsActive(source))
			{
				yield break;
			}
		}
		FinishSource(source);
	}

	private SourceExtension RegisterExtension(AudioSource source, SoundDef oneShotDef = null, AudioClip oneShotClip = null, bool aboutToPlay = false)
	{
		SoundDef soundDef = oneShotDef;
		if (soundDef == null)
		{
			soundDef = GetDefFromSource(source);
		}
		SourceExtension sourceExtension = GetExtension(source);
		if (sourceExtension == null)
		{
			AssetHandle<AudioClip> clipHandle = null;
			AudioClip audioClip = ((oneShotClip == null) ? LoadClipForPlayback(ref clipHandle, source, soundDef) : oneShotClip);
			HearthstoneServices.Get<DisposablesCleaner>()?.Attach(source, clipHandle);
			if (audioClip == null || (aboutToPlay && ProcessClipLimits(audioClip)))
			{
				return null;
			}
			sourceExtension = new SourceExtension();
			sourceExtension.m_sourceVolume = source.volume;
			sourceExtension.m_sourcePitch = source.pitch;
			sourceExtension.m_sourceClip = source.clip;
			sourceExtension.m_id = GetNextSourceId();
			AddExtensionMapping(source, sourceExtension);
			Global.SoundCategory category = GetCategory(source);
			if (category == Global.SoundCategory.NONE)
			{
				category = soundDef.m_Category;
			}
			RegisterSourceByCategory(source, category);
			InitNewClipOnSource(source, soundDef, sourceExtension, audioClip);
		}
		else if (aboutToPlay)
		{
			AudioClip audioClip2;
			if (oneShotClip == null)
			{
				AssetHandle<AudioClip> clipHandle2 = null;
				audioClip2 = LoadClipForPlayback(ref clipHandle2, source, soundDef);
				HearthstoneServices.Get<DisposablesCleaner>()?.Attach(source, clipHandle2);
			}
			else
			{
				audioClip2 = oneShotClip;
			}
			if (!CanPlayClipOnExistingSource(source, audioClip2))
			{
				if (IsActive(source))
				{
					Stop(source);
				}
				else
				{
					FinishSource(source);
				}
				return null;
			}
			if (source.clip != audioClip2)
			{
				if (source.clip != null)
				{
					UnregisterSourceByClip(source);
				}
				InitNewClipOnSource(source, soundDef, sourceExtension, audioClip2);
			}
		}
		return sourceExtension;
	}

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
						text2 = " " + DebugUtils.GetHierarchyPathAndType(source);
					}
					Error.AddDevFatal("{0} has no AudioClip. Top-level parent is {1}{2}.", source.gameObject.name, SceneUtils.FindTopParent(source), text2);
					return null;
				}
			}
		}
		if (text == null || soundDef == null)
		{
			Error.AddDevFatal("DetermineClipForPlayback: failed to GET AudioClip clipAsset={0}, gameObject={2}, soundDef={3}", text, source.gameObject.name, soundDef);
			return null;
		}
		SoundLoader.LoadAudioClipWithFallback(ref clipHandle, source, text);
		return clipHandle?.Asset;
	}

	private bool CanPlayClipOnExistingSource(AudioSource source, AudioClip clip)
	{
		if (clip == null)
		{
			return false;
		}
		if ((!IsActive(source) || source.clip != clip) && ProcessClipLimits(clip))
		{
			return false;
		}
		return true;
	}

	private void InitNewClipOnSource(AudioSource source, SoundDef def, SourceExtension ext, AudioClip clip)
	{
		ext.m_defVolume = SoundUtils.GetRandomVolumeFromDef(def);
		ext.m_defPitch = SoundUtils.GetRandomPitchFromDef(def);
		source.clip = clip;
		RegisterSourceByClip(source, clip);
	}

	private void UnregisterExtension(AudioSource source, SourceExtension ext)
	{
		source.volume = ext.m_sourceVolume;
		source.pitch = ext.m_sourcePitch;
		source.clip = ext.m_sourceClip;
		RemoveExtensionMapping(source);
	}

	private void UpdateSource(AudioSource source)
	{
		SourceExtension extension = GetExtension(source);
		UpdateSource(source, extension);
	}

	private void UpdateSource(AudioSource source, SourceExtension ext)
	{
		UpdateMute(source);
		UpdateVolume(source, ext);
		UpdatePitch(source, ext);
	}

	private void UpdateMute(AudioSource source)
	{
		bool categoryEnabled = IsCategoryEnabled(source);
		UpdateMute(source, categoryEnabled);
	}

	private void UpdateMute(AudioSource source, bool categoryEnabled)
	{
		source.mute = m_mute || !categoryEnabled;
	}

	private void UpdateCategoryMute(Global.SoundCategory cat)
	{
		if (m_sourcesByCategory.TryGetValue(cat, out var value))
		{
			bool categoryEnabled = IsCategoryEnabled(cat);
			for (int i = 0; i < value.Count; i++)
			{
				AudioSource source = value[i];
				UpdateMute(source, categoryEnabled);
			}
		}
	}

	private void UpdateAllMutes()
	{
		foreach (ExtensionMapping extensionMapping in m_extensionMappings)
		{
			UpdateMute(extensionMapping.Source);
		}
	}

	private void UpdateVolume(AudioSource source, SourceExtension ext)
	{
		float categoryVolume = GetCategoryVolume(source);
		float duckingVolume = GetDuckingVolume(source);
		UpdateVolume(source, ext, categoryVolume, duckingVolume);
	}

	private void UpdateVolume(AudioSource source, SourceExtension ext, float categoryVolume, float duckingVolume)
	{
		source.volume = ext.m_codeVolume * ext.m_sourceVolume * ext.m_defVolume * categoryVolume * duckingVolume;
	}

	public void UpdateCategoryVolume(Global.SoundCategory cat)
	{
		if (!m_sourcesByCategory.TryGetValue(cat, out var value))
		{
			return;
		}
		float categoryVolume = SoundUtils.GetCategoryVolume(cat);
		for (int i = 0; i < value.Count; i++)
		{
			AudioSource audioSource = value[i];
			if (!(audioSource == null))
			{
				SourceExtension extension = GetExtension(audioSource);
				float duckingVolume = GetDuckingVolume(audioSource);
				UpdateVolume(audioSource, extension, categoryVolume, duckingVolume);
			}
		}
	}

	private void UpdateAllCategoryVolumes()
	{
		foreach (Global.SoundCategory key in m_sourcesByCategory.Keys)
		{
			UpdateCategoryVolume(key);
		}
	}

	private void UpdatePitch(AudioSource source, SourceExtension ext)
	{
		source.pitch = ext.m_codePitch * ext.m_sourcePitch * ext.m_defPitch;
	}

	private void OnMasterEnabledOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		m_isMasterEnabled = Options.Get().GetBool(option);
		UpdateAllMutes();
	}

	private void OnMasterVolumeOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		SetMasterVolumeExponential();
	}

	private void OnEnabledOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		m_isMusicEnabled = Options.Get().GetBool(option);
		foreach (KeyValuePair<Global.SoundCategory, Option> item in SoundDataTables.s_categoryEnabledOptionMap)
		{
			Global.SoundCategory key = item.Key;
			if (item.Value == option)
			{
				UpdateCategoryMute(key);
			}
		}
	}

	private void OnVolumeOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		foreach (KeyValuePair<Global.SoundCategory, Option> item in SoundDataTables.s_categoryVolumeOptionMap)
		{
			Global.SoundCategory key = item.Key;
			if (item.Value == option)
			{
				UpdateCategoryVolume(key);
			}
		}
	}

	private void OnBackgroundSoundOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		UpdateAppMute();
	}

	private void RegisterSourceByCategory(AudioSource source, Global.SoundCategory cat)
	{
		if (!m_sourcesByCategory.TryGetValue(cat, out var value))
		{
			value = new List<AudioSource>();
			m_sourcesByCategory.Add(cat, value);
			value.Add(source);
		}
		else if (!value.Contains(source))
		{
			value.Add(source);
		}
	}

	private void UnregisterSourceByCategory(AudioSource source)
	{
		Global.SoundCategory category = GetCategory(source);
		if (!m_sourcesByCategory.TryGetValue(category, out var value))
		{
			UnityEngine.Debug.LogWarning($"SoundManager.UnregisterSourceByCategory() - {GetSourceId(source)} is untracked. category={category}");
		}
		else
		{
			value.Remove(source);
		}
	}

	private bool IsCategoryEnabled(AudioSource source)
	{
		SoundDef component = source.GetComponent<SoundDef>();
		return IsCategoryEnabled(component.m_Category);
	}

	private bool IsCategoryEnabled(Global.SoundCategory cat)
	{
		if (SoundUtils.IsMusicCategory(cat) && SoundUtils.IsDeviceBackgroundMusicPlaying())
		{
			return false;
		}
		if (!m_isMasterEnabled)
		{
			return false;
		}
		Option categoryEnabledOption = SoundUtils.GetCategoryEnabledOption(cat);
		return categoryEnabledOption switch
		{
			Option.INVALID => true, 
			Option.MUSIC => m_isMusicEnabled, 
			_ => Options.Get().GetBool(categoryEnabledOption), 
		};
	}

	private float GetCategoryVolume(AudioSource source)
	{
		return SoundUtils.GetCategoryVolume(source.GetComponent<SoundDef>().m_Category);
	}

	private bool IsCategoryAudible(Global.SoundCategory cat)
	{
		if (SoundUtils.GetCategoryVolume(cat) <= Mathf.Epsilon)
		{
			return false;
		}
		return IsCategoryEnabled(cat);
	}

	private void RegisterSourceByClip(AudioSource source, AudioClip clip)
	{
		if (!m_sourcesByClipName.TryGetValue(clip.name, out var value))
		{
			value = new List<AudioSource>();
			m_sourcesByClipName.Add(clip.name, value);
			value.Add(source);
		}
		else if (!value.Contains(source))
		{
			value.Add(source);
		}
	}

	private void UnregisterSourceByClip(AudioSource source)
	{
		AudioClip clip = source.clip;
		if (clip == null)
		{
			UnityEngine.Debug.LogWarning($"SoundManager.UnregisterSourceByClip() - id {GetSourceId(source)} (source {source}) is untracked");
			return;
		}
		if (!m_sourcesByClipName.TryGetValue(clip.name, out var value))
		{
			UnityEngine.Debug.LogError($"SoundManager.UnregisterSourceByClip() - id {GetSourceId(source)} (source {source}) is untracked. clip={clip}");
			return;
		}
		value.Remove(source);
		if (value.Count == 0)
		{
			m_sourcesByClipName.Remove(clip.name);
		}
	}

	private bool ProcessClipLimits(AudioClip clip)
	{
		if (m_config == null || m_config.m_PlaybackLimitDefs == null)
		{
			return false;
		}
		string name = clip.name;
		bool flag = false;
		AudioSource audioSource = null;
		foreach (SoundPlaybackLimitDef playbackLimitDef in m_config.m_PlaybackLimitDefs)
		{
			SoundPlaybackLimitClipDef soundPlaybackLimitClipDef = FindClipDefInPlaybackDef(name, playbackLimitDef);
			if (soundPlaybackLimitClipDef == null)
			{
				continue;
			}
			int num = soundPlaybackLimitClipDef.m_Priority;
			float num2 = 2f;
			int num3 = 0;
			foreach (SoundPlaybackLimitClipDef clipDef in playbackLimitDef.m_ClipDefs)
			{
				string legacyName = clipDef.LegacyName;
				if (!m_sourcesByClipName.TryGetValue(legacyName, out var value))
				{
					continue;
				}
				int priority = clipDef.m_Priority;
				foreach (AudioSource item in value)
				{
					if (!IsPlaying(item))
					{
						continue;
					}
					float num4 = item.time / item.clip.length;
					if (num4 <= clipDef.m_ExclusivePlaybackThreshold)
					{
						num3++;
						if (priority < num && num4 < num2)
						{
							audioSource = item;
							num = priority;
							num2 = num4;
						}
					}
				}
			}
			if (num3 >= playbackLimitDef.m_Limit)
			{
				flag = true;
				break;
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
		Stop(audioSource);
		return false;
	}

	private SoundPlaybackLimitClipDef FindClipDefInPlaybackDef(string clipName, SoundPlaybackLimitDef def)
	{
		if (def.m_ClipDefs == null)
		{
			return null;
		}
		foreach (SoundPlaybackLimitClipDef clipDef in def.m_ClipDefs)
		{
			string legacyName = clipDef.LegacyName;
			if (clipName == legacyName)
			{
				return clipDef;
			}
		}
		return null;
	}

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
		RegisterForDucking(ducker, ducker.GetDuckedCategoryDefs());
		return true;
	}

	public void StopDucking(SoundDucker ducker)
	{
		if (!(ducker == null) && ducker.m_DuckedCategoryDefs != null && ducker.m_DuckedCategoryDefs.Count != 0)
		{
			UnregisterForDucking(ducker, ducker.GetDuckedCategoryDefs());
		}
	}

	public bool IsIgnoringDucking(AudioSource source)
	{
		if (source == null)
		{
			return true;
		}
		SoundDef component = source.GetComponent<SoundDef>();
		if (component == null)
		{
			return true;
		}
		return component.m_IgnoreDucking;
	}

	public void SetIgnoreDucking(AudioSource source, bool enable)
	{
		if (!(source == null))
		{
			SoundDef component = source.GetComponent<SoundDef>();
			if (!(component == null))
			{
				component.m_IgnoreDucking = enable;
			}
		}
	}

	private void RegisterSourceForDucking(AudioSource source, SourceExtension ext)
	{
		SoundDuckingDef soundDuckingDef = FindDuckingDefForSource(source);
		if (soundDuckingDef != null)
		{
			RegisterForDucking(source, soundDuckingDef.m_DuckedCategoryDefs);
			ext.m_ducking = true;
		}
	}

	private void RegisterForDucking(object trigger, List<SoundDuckedCategoryDef> defs)
	{
		foreach (SoundDuckedCategoryDef def in defs)
		{
			DuckState state = RegisterDuckState(trigger, def);
			ChangeDuckState(state, DuckMode.BEGINNING);
		}
	}

	private DuckState RegisterDuckState(object trigger, SoundDuckedCategoryDef duckedCatDef)
	{
		Global.SoundCategory category = duckedCatDef.m_Category;
		DuckState duckState;
		if (m_duckStates.TryGetValue(category, out var value))
		{
			duckState = value.Find((DuckState currState) => currState.IsTrigger(trigger));
			if (duckState != null)
			{
				return duckState;
			}
		}
		else
		{
			value = new List<DuckState>();
			m_duckStates.Add(category, value);
		}
		duckState = new DuckState();
		value.Add(duckState);
		duckState.SetTrigger(trigger);
		duckState.SetDuckedDef(duckedCatDef);
		return duckState;
	}

	private void UnregisterSourceForDucking(AudioSource source, SourceExtension ext)
	{
		if (ext.m_ducking)
		{
			SoundDuckingDef soundDuckingDef = FindDuckingDefForSource(source);
			if (soundDuckingDef != null)
			{
				UnregisterForDucking(source, soundDuckingDef.m_DuckedCategoryDefs);
			}
		}
	}

	private void UnregisterForDucking(object trigger, List<SoundDuckedCategoryDef> defs)
	{
		foreach (SoundDuckedCategoryDef def in defs)
		{
			Global.SoundCategory category = def.m_Category;
			if (!m_duckStates.TryGetValue(category, out var value))
			{
				UnityEngine.Debug.LogError(string.Format("SoundManager.UnregisterForDucking() - {0} ducks {1}, but no DuckStates were found for {1}", trigger, category));
				continue;
			}
			DuckState duckState = value.Find((DuckState currState) => currState.IsTrigger(trigger));
			if (duckState != null)
			{
				ChangeDuckState(duckState, DuckMode.RESTORING);
			}
		}
	}

	private uint GetNextDuckStateTweenId()
	{
		m_nextDuckStateTweenId = (m_nextDuckStateTweenId + 1) & 0xFFFFFFFFu;
		return m_nextDuckStateTweenId;
	}

	private void ChangeDuckState(DuckState state, DuckMode mode)
	{
		string tweenName = state.GetTweenName();
		if (tweenName != null)
		{
			iTween.StopByName(SceneObject, tweenName);
		}
		state.SetMode(mode);
		state.SetTweenName(null);
		switch (mode)
		{
		case DuckMode.BEGINNING:
			AnimateBeginningDuckState(state);
			break;
		case DuckMode.RESTORING:
			AnimateRestoringDuckState(state);
			break;
		}
	}

	private void AnimateBeginningDuckState(DuckState state)
	{
		string text = $"DuckState Begin id={GetNextDuckStateTweenId()}";
		state.SetTweenName(text);
		SoundDuckedCategoryDef duckedDef = state.GetDuckedDef();
		Action<object> action = delegate(object amount)
		{
			float volume = (float)amount;
			state.SetVolume(volume);
			UpdateCategoryVolume(duckedDef.m_Category);
		};
		Action<object> action2 = delegate(object e)
		{
			OnDuckStateBeginningComplete(e);
		};
		Hashtable args = iTween.Hash("name", text, "time", duckedDef.m_BeginSec, "easeType", duckedDef.m_BeginEaseType, "from", state.GetVolume(), "to", duckedDef.m_Volume, "onupdate", action, "oncomplete", action2, "oncompleteparams", state);
		iTween.ValueTo(SceneObject, args);
	}

	private void OnDuckStateBeginningComplete(object arg)
	{
		DuckState duckState = arg as DuckState;
		if (duckState != null)
		{
			duckState.SetMode(DuckMode.HOLD);
			duckState.SetTweenName(null);
		}
	}

	private void AnimateRestoringDuckState(DuckState state)
	{
		string text = $"DuckState Finish id={GetNextDuckStateTweenId()}";
		state.SetTweenName(text);
		SoundDuckedCategoryDef duckedDef = state.GetDuckedDef();
		Action<object> action = delegate(object amount)
		{
			float volume = (float)amount;
			state.SetVolume(volume);
			UpdateCategoryVolume(duckedDef.m_Category);
		};
		Action<object> action2 = delegate(object e)
		{
			OnDuckStateRestoringComplete(e);
		};
		Hashtable args = iTween.Hash("name", text, "time", duckedDef.m_RestoreSec, "easeType", duckedDef.m_RestoreEaseType, "from", state.GetVolume(), "to", 1f, "onupdate", action, "oncomplete", action2, "oncompleteparams", state);
		iTween.ValueTo(SceneObject, args);
	}

	private void OnDuckStateRestoringComplete(object arg)
	{
		DuckState duckState = arg as DuckState;
		if (duckState == null)
		{
			return;
		}
		Global.SoundCategory category = duckState.GetDuckedDef().m_Category;
		List<DuckState> list = m_duckStates[category];
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == duckState)
			{
				list.RemoveAt(i);
				if (list.Count == 0)
				{
					m_duckStates.Remove(category);
				}
				break;
			}
		}
	}

	private SoundDuckingDef FindDuckingDefForSource(AudioSource source)
	{
		Global.SoundCategory category = GetCategory(source);
		return FindDuckingDefForCategory(category);
	}

	private SoundDuckingDef FindDuckingDefForCategory(Global.SoundCategory cat)
	{
		if (m_config == null || m_config.m_DuckingDefs == null)
		{
			return null;
		}
		foreach (SoundDuckingDef duckingDef in m_config.m_DuckingDefs)
		{
			if (cat == duckingDef.m_TriggerCategory)
			{
				return duckingDef;
			}
		}
		return null;
	}

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
		return GetDuckingVolume(component.m_Category);
	}

	private float GetDuckingVolume(Global.SoundCategory cat)
	{
		if (!m_duckStates.TryGetValue(cat, out var value))
		{
			return 1f;
		}
		float num = 1f;
		foreach (DuckState item in value)
		{
			Global.SoundCategory triggerCategory = item.GetTriggerCategory();
			if (triggerCategory == Global.SoundCategory.NONE || IsCategoryAudible(triggerCategory))
			{
				float volume = item.GetVolume();
				if (num > volume)
				{
					num = volume;
				}
			}
		}
		return num;
	}

	private int GetNextSourceId()
	{
		int nextSourceId = m_nextSourceId;
		m_nextSourceId = ((m_nextSourceId == int.MaxValue) ? 1 : (m_nextSourceId + 1));
		return nextSourceId;
	}

	private int GetSourceId(AudioSource source)
	{
		return GetExtension(source)?.m_id ?? 0;
	}

	private AudioSource PlayImpl(AudioSource source, SoundDef oneShotDef, AudioClip oneShotClip = null, SoundOptions additionalSettings = null)
	{
		if (source == null)
		{
			AudioSource placeholderSource = GetPlaceholderSource();
			if (placeholderSource == null)
			{
				Error.AddDevFatal("SoundManager.Play() - source is null and fallback is null");
				return null;
			}
			UnityEngine.Debug.LogWarningFormat("Using placeholder sound for source={0}, oneShotDef={1}, oneShotClip={2}", source, oneShotDef, oneShotClip);
			source = UnityEngine.Object.Instantiate(placeholderSource);
			m_generatedSources.Add(source);
		}
		bool flag = IsActive(source);
		SourceExtension sourceExtension = RegisterExtension(source, oneShotDef, oneShotClip, aboutToPlay: true);
		if (sourceExtension == null)
		{
			return null;
		}
		if (!flag)
		{
			RegisterSourceForDucking(source, sourceExtension);
		}
		UpdateSource(source, sourceExtension);
		if (additionalSettings != null && additionalSettings.InstanceLimited)
		{
			if (activeLimitedSounds.TryGetValue(source.gameObject.name, out var value))
			{
				int num = additionalSettings.MaxInstancesOfThisSound;
				if (num < 1)
				{
					num = 1;
				}
				if (value >= num)
				{
					return null;
				}
				activeLimitedSounds.Remove(source.gameObject.name);
				activeLimitedSounds.Add(source.gameObject.name, value + 1);
			}
			else
			{
				activeLimitedSounds.Add(source.gameObject.name, 1);
			}
			float num2 = additionalSettings.InstanceTimeLimit;
			if (num2 <= 0f)
			{
				num2 = source.clip.length;
			}
			HearthstoneApplication.Get().StartCoroutine(EnableInstanceLimitedSound(source.gameObject.name, num2));
		}
		source.Play();
		return source;
	}

	private IEnumerator EnableInstanceLimitedSound(string sound, float time)
	{
		if (!activeLimitedSounds.ContainsKey(sound))
		{
			yield break;
		}
		while (time > 0f)
		{
			time -= Time.deltaTime;
			yield return null;
		}
		if (activeLimitedSounds.TryGetValue(sound, out var value))
		{
			activeLimitedSounds.Remove(sound);
			value--;
			if (value > 0)
			{
				activeLimitedSounds.Add(sound, value);
			}
		}
	}

	private SoundDef GetDefFromSource(AudioSource source)
	{
		SoundDef soundDef = source.GetComponent<SoundDef>();
		if (soundDef == null)
		{
			Log.Sound.Print("SoundUtils.GetDefFromSource() - source={0} has no def. adding new def.", source);
			soundDef = source.gameObject.AddComponent<SoundDef>();
		}
		return soundDef;
	}

	private void OnAppFocusChanged(bool focus, object userData)
	{
		UpdateAppMute();
	}

	private void UpdateAppMute()
	{
		UpdateMusicAndSources();
		if (HearthstoneApplication.Get() != null)
		{
			m_mute = !HearthstoneApplication.Get().HasFocus() && !Options.Get().GetBool(Option.BACKGROUND_SOUND);
		}
		UpdateAllMutes();
	}

	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		GarbageCollectBundles();
	}

	private AudioSource GenerateAudioSource(AudioSource templateSource, SoundDef def)
	{
		string arg = ((def != null) ? Path.GetFileNameWithoutExtension(def.m_AudioClip) : "CreatedSound");
		string name = $"Audio Object - {arg}";
		AudioSource component;
		if ((bool)templateSource)
		{
			GameObject gameObject = new GameObject(name);
			SoundUtils.AddAudioSourceComponents(gameObject);
			component = gameObject.GetComponent<AudioSource>();
			SoundUtils.CopyAudioSource(templateSource, component);
		}
		else if ((bool)m_config.m_PlayClipTemplate)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate(m_config.m_PlayClipTemplate.gameObject);
			gameObject2.name = name;
			component = gameObject2.GetComponent<AudioSource>();
		}
		else
		{
			GameObject gameObject3 = new GameObject(name);
			SoundUtils.AddAudioSourceComponents(gameObject3);
			component = gameObject3.GetComponent<AudioSource>();
		}
		m_generatedSources.Add(component);
		return component;
	}

	private void InitSourceTransform(AudioSource source, GameObject parentObject)
	{
		if (!(source == null) && !(source.gameObject == null) && !(source.transform == null))
		{
			source.transform.parent = SceneObject.transform;
			if (parentObject == null || parentObject.transform == null)
			{
				source.transform.position = Vector3.zero;
			}
			else
			{
				source.transform.position = parentObject.transform.position;
			}
		}
	}

	private void FinishSource(AudioSource source)
	{
		if (m_currentMusicTrack == source)
		{
			ChangeCurrentMusicTrack(null);
		}
		else if (m_currentAmbienceTrack == source)
		{
			ChangeCurrentAmbienceTrack(null);
		}
		for (int i = 0; i < m_fadingTracks.Count; i++)
		{
			if (m_fadingTracks[i] == source)
			{
				m_fadingTracks.RemoveAt(i);
				break;
			}
		}
		UnregisterSourceByCategory(source);
		UnregisterSourceByClip(source);
		SourceExtension extension = GetExtension(source);
		if (extension != null)
		{
			UnregisterSourceForDucking(source, extension);
			UnregisterSourceBundle(source, extension);
			UnregisterExtension(source, extension);
		}
		FinishGeneratedSource(source);
	}

	private void FinishGeneratedSource(AudioSource source)
	{
		for (int i = 0; i < m_generatedSources.Count; i++)
		{
			if (m_generatedSources[i] == source)
			{
				UnityEngine.Object.Destroy(source.gameObject);
				m_generatedSources.RemoveAt(i);
				break;
			}
		}
	}

	private BundleInfo RegisterSourceBundle(AssetReference assetRef, AudioSource source)
	{
		if (!m_bundleInfos.TryGetValue(assetRef, out var value))
		{
			value = new BundleInfo();
			value.SetAssetRef(assetRef);
			m_bundleInfos.Add(assetRef, value);
		}
		if (source != null)
		{
			value.AddRef(source);
			SourceExtension sourceExtension = RegisterExtension(source);
			if (sourceExtension == null)
			{
				return null;
			}
			sourceExtension.m_bundleName = assetRef;
		}
		return value;
	}

	private void UnregisterSourceBundle(AudioSource source, SourceExtension ext)
	{
		if (ext.m_bundleName != null)
		{
			UnregisterSourceBundle(ext.m_bundleName, source);
		}
	}

	private void UnregisterSourceBundle(string name, AudioSource source)
	{
		if (m_bundleInfos.TryGetValue(name, out var value) && value.RemoveRef(source) && value.CanGarbageCollect())
		{
			m_bundleInfos.Remove(name);
			UnloadSoundBundle(name);
		}
	}

	private void UnloadSoundBundle(AssetReference assetRef)
	{
	}

	private void GarbageCollectBundles()
	{
		Map<string, BundleInfo> map = new Map<string, BundleInfo>();
		foreach (KeyValuePair<string, BundleInfo> bundleInfo in m_bundleInfos)
		{
			string key = bundleInfo.Key;
			BundleInfo value = bundleInfo.Value;
			value.EnableGarbageCollect(enable: true);
			if (value.CanGarbageCollect())
			{
				UnloadSoundBundle(key);
			}
			else
			{
				map.Add(key, value);
			}
		}
		m_bundleInfos = map;
	}

	private void UpdateMusicAndSources()
	{
		UpdateMusicAndAmbience();
		UpdateSources();
	}

	private void UpdateSources()
	{
		UpdateSourceExtensionMappings();
		UpdateSourcesByCategory();
		UpdateSourcesByClipName();
		UpdateSourceBundles();
		UpdateGeneratedSources();
		UpdateDuckStates();
	}

	private void UpdateSourceExtensionMappings()
	{
		int num = 0;
		while (num < m_extensionMappings.Count)
		{
			AudioSource source = m_extensionMappings[num].Source;
			if (source == null)
			{
				m_extensionMappings.RemoveAt(num);
				continue;
			}
			if (!IsActive(source))
			{
				m_inactiveSources.Add(source);
			}
			num++;
		}
		CleanInactiveSources();
	}

	private void CleanUpSourceList(List<AudioSource> sources)
	{
		if (sources == null)
		{
			return;
		}
		int num = 0;
		while (num < sources.Count)
		{
			if (sources[num] == null)
			{
				sources.RemoveAt(num);
			}
			else
			{
				num++;
			}
		}
	}

	private void UpdateSourcesByCategory()
	{
		foreach (List<AudioSource> value in m_sourcesByCategory.Values)
		{
			CleanUpSourceList(value);
		}
	}

	private void UpdateSourcesByClipName()
	{
		foreach (List<AudioSource> value in m_sourcesByClipName.Values)
		{
			CleanUpSourceList(value);
		}
	}

	private void UpdateSourceBundles()
	{
		m_bundleInfosToRemove.Clear();
		foreach (BundleInfo value in m_bundleInfos.Values)
		{
			List<AudioSource> refs = value.GetRefs();
			int num = 0;
			bool flag = false;
			while (num < refs.Count)
			{
				if (refs[num] == null)
				{
					flag = true;
					refs.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
			if (flag)
			{
				string assetRef = value.GetAssetRef();
				if (value.CanGarbageCollect())
				{
					m_bundleInfosToRemove.Add(assetRef);
				}
			}
		}
		for (int i = 0; i < m_bundleInfosToRemove.Count; i++)
		{
			string text = m_bundleInfosToRemove[i];
			m_bundleInfos.Remove(text);
			UnloadSoundBundle(text);
		}
	}

	private void UpdateGeneratedSources()
	{
		CleanUpSourceList(m_generatedSources);
	}

	private void FinishDeadGeneratedSource(AudioSource source)
	{
		for (int i = 0; i < m_generatedSources.Count; i++)
		{
			if (m_generatedSources[i] == source)
			{
				m_generatedSources.RemoveAt(i);
				break;
			}
		}
	}

	private void UpdateDuckStates()
	{
		foreach (List<DuckState> value in m_duckStates.Values)
		{
			foreach (DuckState item in value)
			{
				if (!item.IsTriggerAlive() && item.GetMode() != DuckMode.RESTORING)
				{
					ChangeDuckState(item, DuckMode.RESTORING);
				}
			}
		}
	}

	private void CleanInactiveSources()
	{
		foreach (AudioSource inactiveSource in m_inactiveSources)
		{
			FinishSource(inactiveSource);
		}
		m_inactiveSources.Clear();
	}

	[Conditional("SOUND_SOURCE_DEBUG")]
	private void SourcePrint(string format, params object[] args)
	{
		Log.Sound.Print(format, args);
	}

	[Conditional("SOUND_SOURCE_DEBUG")]
	private void SourceScreenPrint(string format, params object[] args)
	{
		Log.Sound.ScreenPrint(format, args);
	}

	[Conditional("SOUND_TRACK_DEBUG")]
	private void TrackPrint(string format, params object[] args)
	{
		Log.Sound.Print(format, args);
	}

	[Conditional("SOUND_TRACK_DEBUG")]
	private void TrackScreenPrint(string format, params object[] args)
	{
		Log.Sound.ScreenPrint(format, args);
	}

	[Conditional("SOUND_CATEGORY_DEBUG")]
	private void CategoryPrint(string format, params object[] args)
	{
		Log.Sound.Print(format, args);
	}

	[Conditional("SOUND_CATEGORY_DEBUG")]
	private void CategoryScreenPrint(string format, params object[] args)
	{
		Log.Sound.ScreenPrint(format, args);
	}

	[Conditional("SOUND_CATEGORY_DEBUG")]
	private void PrintAllCategorySources()
	{
		Log.Sound.Print("SoundManager.PrintAllCategorySources()");
		foreach (KeyValuePair<Global.SoundCategory, List<AudioSource>> item in m_sourcesByCategory)
		{
			Global.SoundCategory key = item.Key;
			List<AudioSource> value = item.Value;
			Log.Sound.Print("Category {0}:", key);
			for (int i = 0; i < value.Count; i++)
			{
				Log.Sound.Print("    {0} = {1}", i, value[i]);
			}
		}
	}

	[Conditional("SOUND_BUNDLE_DEBUG")]
	private void BundlePrint(string format, params object[] args)
	{
		Log.Sound.Print(format, args);
	}

	[Conditional("SOUND_BUNDLE_DEBUG")]
	private void BundleScreenPrint(string format, params object[] args)
	{
		Log.Sound.ScreenPrint(format, args);
	}

	[Conditional("SOUND_DUCKING_DEBUG")]
	private void DuckingPrint(string format, params object[] args)
	{
		Log.Sound.Print(format, args);
	}

	[Conditional("SOUND_DUCKING_DEBUG")]
	private void DuckingScreenPrint(string format, params object[] args)
	{
		Log.Sound.ScreenPrint(format, args);
	}

	private void AddExtensionMapping(AudioSource source, SourceExtension extension)
	{
		if (!(source == null) && extension != null)
		{
			ExtensionMapping extensionMapping = new ExtensionMapping();
			extensionMapping.Source = source;
			extensionMapping.Extension = extension;
			m_extensionMappings.Add(extensionMapping);
		}
	}

	private void RemoveExtensionMapping(AudioSource source)
	{
		for (int i = 0; i < m_extensionMappings.Count; i++)
		{
			if (m_extensionMappings[i].Source == source)
			{
				m_extensionMappings.RemoveAt(i);
				break;
			}
		}
	}

	private SourceExtension GetExtension(AudioSource source)
	{
		for (int i = 0; i < m_extensionMappings.Count; i++)
		{
			ExtensionMapping extensionMapping = m_extensionMappings[i];
			if (extensionMapping.Source == source)
			{
				return extensionMapping.Extension;
			}
		}
		return null;
	}

	public Dictionary<Global.SoundCategory, float> DuckingLevels()
	{
		Dictionary<Global.SoundCategory, float> dictionary = new Dictionary<Global.SoundCategory, float>();
		Global.SoundCategory[] array = (Global.SoundCategory[])Enum.GetValues(typeof(Global.SoundCategory));
		foreach (Global.SoundCategory soundCategory in array)
		{
			dictionary[soundCategory] = GetDuckingVolume(soundCategory);
		}
		return dictionary;
	}
}
