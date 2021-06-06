using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone.Core;
using UnityEngine;
using UnityEngine.Video;

public class Cinematic : IService, IHasUpdate
{
	private static readonly string Hearthstone_Tavern_Abridged = "Hearthstone_Tavern_Abridged";

	private static readonly string Hearthstone_Tavern_Abridged_Logo = "Hearthstone_Tavern_Abridged_Logo";

	private static readonly AssetReference Hearthstone_Tavern_Abridged_Audio = new AssetReference("Hearthstone_Tavern_Abridged_Audio.wav:f89a884079f1645598bb19565f5915ef");

	private static readonly string Mobile_Assets_Path = "UnimportedAssets/MobileAssets/Android/";

	private AssetHandle<AudioClip> m_movieAudio;

	private AudioSource m_audioSource;

	private Camera m_camera;

	private SoundDucker m_soundDucker;

	private bool m_started;

	private bool m_canceled;

	private int m_previousTargetFrameRate;

	private int m_previousSleepTimeout;

	private Action m_callback;

	private VideoPlayer m_mainPlayer;

	private VideoPlayer m_logoPlayer;

	private GameObject m_sceneObject;

	private float m_playBeginTime;

	private float m_playTime;

	private GameObject SceneObject
	{
		get
		{
			if (m_sceneObject == null)
			{
				m_sceneObject = new GameObject("CinematicSceneObject", typeof(HSDontDestroyOnLoad));
			}
			return m_sceneObject;
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		m_soundDucker = SceneObject.AddComponent<SoundDucker>();
		m_soundDucker.m_GlobalDuckDef = new SoundDuckedCategoryDef();
		m_soundDucker.m_GlobalDuckDef.m_Volume = 0f;
		m_soundDucker.m_GlobalDuckDef.m_RestoreSec = 1.5f;
		m_soundDucker.m_GlobalDuckDef.m_BeginSec = 1.5f;
		yield break;
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
		AssetHandle.SafeDispose(ref m_movieAudio);
	}

	public void Play(Action callback)
	{
		m_callback = callback;
		Options.Get().SetBool(Option.HAS_SEEN_NEW_CINEMATIC, val: true);
		m_canceled = false;
		m_started = true;
		Processor.RunCoroutine(AwaitReadinessThenPlay());
	}

	private VideoPlayer CreatePlayer()
	{
		VideoPlayer videoPlayer = SceneObject.AddComponent<VideoPlayer>();
		videoPlayer.isLooping = false;
		videoPlayer.playOnAwake = false;
		videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
		return videoPlayer;
	}

	private void OnPlayBegin()
	{
		TelemetryManager.Client().SendCinematic(begin: true, -1f);
		m_playBeginTime = Time.realtimeSinceStartup;
		m_previousTargetFrameRate = Application.targetFrameRate;
		Application.targetFrameRate = 0;
		if (PlatformSettings.IsMobile())
		{
			m_previousSleepTimeout = Screen.sleepTimeout;
			Screen.sleepTimeout = -1;
		}
		m_mainPlayer = CreatePlayer();
		m_logoPlayer = CreatePlayer();
		m_mainPlayer.renderMode = VideoRenderMode.CameraNearPlane;
		m_logoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
		m_mainPlayer.loopPointReached += OnMainVideoComplete;
		m_logoPlayer.loopPointReached += OnLogoVideoComplete;
		BnetBar.Get().gameObject.SetActive(value: false);
		PegCursor.Get().Hide();
		CreateCamera();
	}

	private void OnPlayEnd(bool canceled)
	{
		Application.targetFrameRate = m_previousTargetFrameRate;
		if (PlatformSettings.IsMobile())
		{
			Screen.sleepTimeout = m_previousSleepTimeout;
		}
		if (BnetBar.Get() != null)
		{
			BnetBar.Get().gameObject.SetActive(value: true);
			BnetBar.Get().UpdateLayout();
		}
		if (PegCursor.Get() != null)
		{
			PegCursor.Get().Show();
		}
		if (SocialToastMgr.Get() != null)
		{
			SocialToastMgr.Get().Reset();
		}
		if (m_camera != null)
		{
			UnityEngine.Object.Destroy(m_camera.gameObject);
		}
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().Stop(m_audioSource);
		}
		if (m_soundDucker != null)
		{
			m_soundDucker.StopDucking();
		}
		AssetHandle.SafeDispose(ref m_movieAudio);
		if (m_audioSource != null)
		{
			m_audioSource.Stop();
		}
		m_canceled = true;
		m_started = false;
		if (m_callback != null)
		{
			m_callback();
			m_callback = null;
		}
		float duration = -1f;
		if (canceled)
		{
			duration = Time.realtimeSinceStartup - m_playBeginTime;
		}
		TelemetryManager.Client().SendCinematic(begin: false, duration);
		Processor.RunCoroutine(WaitOneFrameThenTeardownPlayer());
	}

	private IEnumerator WaitOneFrameThenTeardownPlayer()
	{
		yield return null;
		UnityEngine.Object.Destroy(m_mainPlayer);
		UnityEngine.Object.Destroy(m_logoPlayer);
	}

	private IEnumerator AwaitReadinessThenPlay()
	{
		OnPlayBegin();
		AssetLoader.Get().LoadAsset<AudioClip>(Hearthstone_Tavern_Abridged_Audio, AudioLoaded);
		LoadMovie();
		LoadLogo();
		if (PlatformSettings.IsMobile())
		{
			while (m_movieAudio == null && !m_canceled)
			{
				yield return null;
			}
		}
		else
		{
			while ((!m_mainPlayer.isPrepared || m_movieAudio == null || !m_logoPlayer.isPrepared) && !m_canceled)
			{
				yield return null;
			}
		}
		if (!m_canceled)
		{
			m_mainPlayer.Play();
			while (!m_canceled && m_mainPlayer.frame < 1)
			{
				yield return null;
			}
		}
		if (!m_canceled)
		{
			m_mainPlayer.targetCamera = m_camera;
			m_logoPlayer.targetCamera = m_camera;
			PlaySound();
		}
	}

	public void Update()
	{
		if (InputCollection.GetAnyKey())
		{
			if (m_audioSource != null && m_audioSource.isPlaying)
			{
				m_audioSource.Stop();
			}
			if (m_mainPlayer != null && m_mainPlayer.isPlaying)
			{
				m_mainPlayer.Stop();
			}
			if (m_logoPlayer != null && m_logoPlayer.isPlaying)
			{
				m_logoPlayer.Stop();
			}
			if (m_started)
			{
				OnPlayEnd(canceled: true);
			}
		}
	}

	private void PlaySound()
	{
		SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
		soundPlayClipArgs.m_forcedAudioClip = m_movieAudio;
		soundPlayClipArgs.m_volume = 1f;
		soundPlayClipArgs.m_pitch = 1f;
		soundPlayClipArgs.m_category = Global.SoundCategory.FX;
		soundPlayClipArgs.m_parentObject = SceneObject;
		m_audioSource = SoundManager.Get().PlayClip(soundPlayClipArgs);
		SoundManager.Get().Set3d(m_audioSource, enable: false);
		SoundManager.Get().SetIgnoreDucking(m_audioSource, enable: true);
		m_soundDucker.StartDucking();
	}

	private void OnMainVideoComplete(VideoPlayer _)
	{
		m_mainPlayer.renderMode = VideoRenderMode.CameraFarPlane;
		m_logoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
		m_logoPlayer.Play();
	}

	private void OnLogoVideoComplete(VideoPlayer _)
	{
		OnPlayEnd(canceled: false);
	}

	private void CreateCamera()
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.position = new Vector3(-9997.9f, -9998.9f, -9999.9f);
		m_camera = gameObject.AddComponent<Camera>();
		m_camera.name = "Cinematic Background Camera";
		m_camera.clearFlags = CameraClearFlags.Color;
		m_camera.backgroundColor = Color.black;
		m_camera.depth = 1000f;
		m_camera.nearClipPlane = 0.01f;
		m_camera.farClipPlane = 0.02f;
		m_camera.allowHDR = false;
	}

	private void LoadMovie()
	{
		string text = "Movies/" + Hearthstone_Tavern_Abridged;
		if (PlatformSettings.IsMobile())
		{
			string text2 = ".mp4";
			if (Application.isEditor)
			{
				m_logoPlayer.url = Mobile_Assets_Path + text + text2;
				return;
			}
			if (PlatformSettings.OS == OSCategory.Android)
			{
				text2 = ".mkv";
			}
			m_mainPlayer.url = FileUtils.GetAssetPath(text + text2);
		}
		else
		{
			ResourceRequest request = Resources.LoadAsync<VideoClip>(text);
			Processor.RunCoroutine(AwaitRequestThenCallback(request, MovieLoaded));
		}
	}

	private void LoadLogo()
	{
		string text = Hearthstone_Tavern_Abridged_Logo;
		Locale locale = Localization.GetLocale();
		if (locale == Locale.jaJP || locale == Locale.thTH || locale == Locale.zhCN || locale == Locale.zhTW)
		{
			text = string.Concat(locale, "/", text);
		}
		text = "Movies/" + text;
		if (PlatformSettings.IsMobile())
		{
			string text2 = ".mp4";
			if (Application.isEditor)
			{
				m_logoPlayer.url = Mobile_Assets_Path + text + text2;
				return;
			}
			if (PlatformSettings.OS == OSCategory.Android)
			{
				text2 = ".mkv";
			}
			m_logoPlayer.url = FileUtils.GetAssetPath(text + text2);
		}
		else
		{
			ResourceRequest request = Resources.LoadAsync<VideoClip>(text);
			Processor.RunCoroutine(AwaitRequestThenCallback(request, LogoLoaded));
		}
	}

	private IEnumerator AwaitRequestThenCallback(ResourceRequest request, AssetLoader.ObjectCallback callback)
	{
		while (!request.isDone)
		{
			yield return null;
		}
		callback(null, request.asset, null);
	}

	private void AudioLoaded(AssetReference assetRef, AssetHandle<AudioClip> asset, object callbackData)
	{
		using (asset)
		{
			if (asset == null)
			{
				Error.AddDevFatal("Failed to load Cinematic Audio Track!");
			}
			else if (!m_canceled)
			{
				AssetHandle.Set(ref m_movieAudio, asset);
			}
		}
	}

	private void MovieLoaded(AssetReference assetRef, UnityEngine.Object asset, object callbackData)
	{
		if (asset == null)
		{
			Error.AddDevFatal("Failed to load Cinematic movie!");
			m_canceled = true;
		}
		else if (!m_canceled)
		{
			m_mainPlayer.clip = asset as VideoClip;
			m_mainPlayer.Prepare();
		}
	}

	private void LogoLoaded(AssetReference assetRef, UnityEngine.Object asset, object callbackData)
	{
		if (asset == null)
		{
			Error.AddDevFatal("Failed to load Cinematic logo!");
			m_canceled = true;
		}
		else if (!m_canceled)
		{
			m_logoPlayer.clip = asset as VideoClip;
			m_logoPlayer.Prepare();
		}
	}
}
