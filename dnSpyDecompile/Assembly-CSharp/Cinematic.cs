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

// Token: 0x02000A1B RID: 2587
public class Cinematic : IService, IHasUpdate
{
	// Token: 0x170007C6 RID: 1990
	// (get) Token: 0x06008B75 RID: 35701 RVA: 0x002C9904 File Offset: 0x002C7B04
	private GameObject SceneObject
	{
		get
		{
			if (this.m_sceneObject == null)
			{
				this.m_sceneObject = new GameObject("CinematicSceneObject", new Type[]
				{
					typeof(HSDontDestroyOnLoad)
				});
			}
			return this.m_sceneObject;
		}
	}

	// Token: 0x06008B76 RID: 35702 RVA: 0x002C993D File Offset: 0x002C7B3D
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.m_soundDucker = this.SceneObject.AddComponent<SoundDucker>();
		this.m_soundDucker.m_GlobalDuckDef = new SoundDuckedCategoryDef();
		this.m_soundDucker.m_GlobalDuckDef.m_Volume = 0f;
		this.m_soundDucker.m_GlobalDuckDef.m_RestoreSec = 1.5f;
		this.m_soundDucker.m_GlobalDuckDef.m_BeginSec = 1.5f;
		yield break;
	}

	// Token: 0x06008B77 RID: 35703 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x06008B78 RID: 35704 RVA: 0x002C994C File Offset: 0x002C7B4C
	public void Shutdown()
	{
		AssetHandle.SafeDispose<AudioClip>(ref this.m_movieAudio);
	}

	// Token: 0x06008B79 RID: 35705 RVA: 0x002C9959 File Offset: 0x002C7B59
	public void Play(Action callback)
	{
		this.m_callback = callback;
		Options.Get().SetBool(Option.HAS_SEEN_NEW_CINEMATIC, true);
		this.m_canceled = false;
		this.m_started = true;
		Processor.RunCoroutine(this.AwaitReadinessThenPlay(), null);
	}

	// Token: 0x06008B7A RID: 35706 RVA: 0x002C998A File Offset: 0x002C7B8A
	private VideoPlayer CreatePlayer()
	{
		VideoPlayer videoPlayer = this.SceneObject.AddComponent<VideoPlayer>();
		videoPlayer.isLooping = false;
		videoPlayer.playOnAwake = false;
		videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
		return videoPlayer;
	}

	// Token: 0x06008B7B RID: 35707 RVA: 0x002C99AC File Offset: 0x002C7BAC
	private void OnPlayBegin()
	{
		TelemetryManager.Client().SendCinematic(true, -1f);
		this.m_playBeginTime = Time.realtimeSinceStartup;
		this.m_previousTargetFrameRate = Application.targetFrameRate;
		Application.targetFrameRate = 0;
		if (PlatformSettings.IsMobile())
		{
			this.m_previousSleepTimeout = Screen.sleepTimeout;
			Screen.sleepTimeout = -1;
		}
		this.m_mainPlayer = this.CreatePlayer();
		this.m_logoPlayer = this.CreatePlayer();
		this.m_mainPlayer.renderMode = VideoRenderMode.CameraNearPlane;
		this.m_logoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
		this.m_mainPlayer.loopPointReached += this.OnMainVideoComplete;
		this.m_logoPlayer.loopPointReached += this.OnLogoVideoComplete;
		BnetBar.Get().gameObject.SetActive(false);
		PegCursor.Get().Hide();
		this.CreateCamera();
	}

	// Token: 0x06008B7C RID: 35708 RVA: 0x002C9A7C File Offset: 0x002C7C7C
	private void OnPlayEnd(bool canceled)
	{
		Application.targetFrameRate = this.m_previousTargetFrameRate;
		if (PlatformSettings.IsMobile())
		{
			Screen.sleepTimeout = this.m_previousSleepTimeout;
		}
		if (BnetBar.Get() != null)
		{
			BnetBar.Get().gameObject.SetActive(true);
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
		if (this.m_camera != null)
		{
			UnityEngine.Object.Destroy(this.m_camera.gameObject);
		}
		if (SoundManager.Get() != null)
		{
			SoundManager.Get().Stop(this.m_audioSource);
		}
		if (this.m_soundDucker != null)
		{
			this.m_soundDucker.StopDucking();
		}
		AssetHandle.SafeDispose<AudioClip>(ref this.m_movieAudio);
		if (this.m_audioSource != null)
		{
			this.m_audioSource.Stop();
		}
		this.m_canceled = true;
		this.m_started = false;
		if (this.m_callback != null)
		{
			this.m_callback();
			this.m_callback = null;
		}
		float duration = -1f;
		if (canceled)
		{
			duration = Time.realtimeSinceStartup - this.m_playBeginTime;
		}
		TelemetryManager.Client().SendCinematic(false, duration);
		Processor.RunCoroutine(this.WaitOneFrameThenTeardownPlayer(), null);
	}

	// Token: 0x06008B7D RID: 35709 RVA: 0x002C9BC5 File Offset: 0x002C7DC5
	private IEnumerator WaitOneFrameThenTeardownPlayer()
	{
		yield return null;
		UnityEngine.Object.Destroy(this.m_mainPlayer);
		UnityEngine.Object.Destroy(this.m_logoPlayer);
		yield break;
	}

	// Token: 0x06008B7E RID: 35710 RVA: 0x002C9BD4 File Offset: 0x002C7DD4
	private IEnumerator AwaitReadinessThenPlay()
	{
		this.OnPlayBegin();
		AssetLoader.Get().LoadAsset<AudioClip>(Cinematic.Hearthstone_Tavern_Abridged_Audio, new AssetHandleCallback<AudioClip>(this.AudioLoaded), null, AssetLoadingOptions.None);
		this.LoadMovie();
		this.LoadLogo();
		if (PlatformSettings.IsMobile())
		{
			while (this.m_movieAudio == null)
			{
				if (this.m_canceled)
				{
					break;
				}
				yield return null;
			}
		}
		else
		{
			while ((!this.m_mainPlayer.isPrepared || this.m_movieAudio == null || !this.m_logoPlayer.isPrepared) && !this.m_canceled)
			{
				yield return null;
			}
		}
		if (!this.m_canceled)
		{
			this.m_mainPlayer.Play();
			while (!this.m_canceled && this.m_mainPlayer.frame < 1L)
			{
				yield return null;
			}
		}
		if (!this.m_canceled)
		{
			this.m_mainPlayer.targetCamera = this.m_camera;
			this.m_logoPlayer.targetCamera = this.m_camera;
			this.PlaySound();
		}
		yield break;
	}

	// Token: 0x06008B7F RID: 35711 RVA: 0x002C9BE4 File Offset: 0x002C7DE4
	public void Update()
	{
		if (InputCollection.GetAnyKey())
		{
			if (this.m_audioSource != null && this.m_audioSource.isPlaying)
			{
				this.m_audioSource.Stop();
			}
			if (this.m_mainPlayer != null && this.m_mainPlayer.isPlaying)
			{
				this.m_mainPlayer.Stop();
			}
			if (this.m_logoPlayer != null && this.m_logoPlayer.isPlaying)
			{
				this.m_logoPlayer.Stop();
			}
			if (this.m_started)
			{
				this.OnPlayEnd(true);
			}
		}
	}

	// Token: 0x06008B80 RID: 35712 RVA: 0x002C9C7C File Offset: 0x002C7E7C
	private void PlaySound()
	{
		SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
		soundPlayClipArgs.m_forcedAudioClip = this.m_movieAudio;
		soundPlayClipArgs.m_volume = new float?(1f);
		soundPlayClipArgs.m_pitch = new float?(1f);
		soundPlayClipArgs.m_category = new Global.SoundCategory?(Global.SoundCategory.FX);
		soundPlayClipArgs.m_parentObject = this.SceneObject;
		this.m_audioSource = SoundManager.Get().PlayClip(soundPlayClipArgs, true, null);
		SoundManager.Get().Set3d(this.m_audioSource, false);
		SoundManager.Get().SetIgnoreDucking(this.m_audioSource, true);
		this.m_soundDucker.StartDucking();
	}

	// Token: 0x06008B81 RID: 35713 RVA: 0x002C9D18 File Offset: 0x002C7F18
	private void OnMainVideoComplete(VideoPlayer _)
	{
		this.m_mainPlayer.renderMode = VideoRenderMode.CameraFarPlane;
		this.m_logoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
		this.m_logoPlayer.Play();
	}

	// Token: 0x06008B82 RID: 35714 RVA: 0x002C9D3D File Offset: 0x002C7F3D
	private void OnLogoVideoComplete(VideoPlayer _)
	{
		this.OnPlayEnd(false);
	}

	// Token: 0x06008B83 RID: 35715 RVA: 0x002C9D48 File Offset: 0x002C7F48
	private void CreateCamera()
	{
		this.m_camera = new GameObject
		{
			transform = 
			{
				position = new Vector3(-9997.9f, -9998.9f, -9999.9f)
			}
		}.AddComponent<Camera>();
		this.m_camera.name = "Cinematic Background Camera";
		this.m_camera.clearFlags = CameraClearFlags.Color;
		this.m_camera.backgroundColor = Color.black;
		this.m_camera.depth = 1000f;
		this.m_camera.nearClipPlane = 0.01f;
		this.m_camera.farClipPlane = 0.02f;
		this.m_camera.allowHDR = false;
	}

	// Token: 0x06008B84 RID: 35716 RVA: 0x002C9DF0 File Offset: 0x002C7FF0
	private void LoadMovie()
	{
		string text = "Movies/" + Cinematic.Hearthstone_Tavern_Abridged;
		if (!PlatformSettings.IsMobile())
		{
			ResourceRequest request = Resources.LoadAsync<VideoClip>(text);
			Processor.RunCoroutine(this.AwaitRequestThenCallback(request, new AssetLoader.ObjectCallback(this.MovieLoaded)), null);
			return;
		}
		string text2 = ".mp4";
		if (Application.isEditor)
		{
			this.m_logoPlayer.url = Cinematic.Mobile_Assets_Path + text + text2;
			return;
		}
		if (PlatformSettings.OS == OSCategory.Android)
		{
			text2 = ".mkv";
		}
		this.m_mainPlayer.url = FileUtils.GetAssetPath(text + text2, true);
	}

	// Token: 0x06008B85 RID: 35717 RVA: 0x002C9E84 File Offset: 0x002C8084
	private void LoadLogo()
	{
		string text = Cinematic.Hearthstone_Tavern_Abridged_Logo;
		Locale locale = Localization.GetLocale();
		if (locale == Locale.jaJP || locale == Locale.thTH || locale == Locale.zhCN || locale == Locale.zhTW)
		{
			text = locale + "/" + text;
		}
		text = "Movies/" + text;
		if (!PlatformSettings.IsMobile())
		{
			ResourceRequest request = Resources.LoadAsync<VideoClip>(text);
			Processor.RunCoroutine(this.AwaitRequestThenCallback(request, new AssetLoader.ObjectCallback(this.LogoLoaded)), null);
			return;
		}
		string text2 = ".mp4";
		if (Application.isEditor)
		{
			this.m_logoPlayer.url = Cinematic.Mobile_Assets_Path + text + text2;
			return;
		}
		if (PlatformSettings.OS == OSCategory.Android)
		{
			text2 = ".mkv";
		}
		this.m_logoPlayer.url = FileUtils.GetAssetPath(text + text2, true);
	}

	// Token: 0x06008B86 RID: 35718 RVA: 0x002C9F42 File Offset: 0x002C8142
	private IEnumerator AwaitRequestThenCallback(ResourceRequest request, AssetLoader.ObjectCallback callback)
	{
		while (!request.isDone)
		{
			yield return null;
		}
		callback(null, request.asset, null);
		yield break;
	}

	// Token: 0x06008B87 RID: 35719 RVA: 0x002C9F58 File Offset: 0x002C8158
	private void AudioLoaded(AssetReference assetRef, AssetHandle<AudioClip> asset, object callbackData)
	{
		try
		{
			if (asset == null)
			{
				Error.AddDevFatal("Failed to load Cinematic Audio Track!", Array.Empty<object>());
			}
			else if (!this.m_canceled)
			{
				AssetHandle.Set<AudioClip>(ref this.m_movieAudio, asset);
			}
		}
		finally
		{
			if (asset != null)
			{
				((IDisposable)asset).Dispose();
			}
		}
	}

	// Token: 0x06008B88 RID: 35720 RVA: 0x002C9FAC File Offset: 0x002C81AC
	private void MovieLoaded(AssetReference assetRef, UnityEngine.Object asset, object callbackData)
	{
		if (asset == null)
		{
			Error.AddDevFatal("Failed to load Cinematic movie!", Array.Empty<object>());
			this.m_canceled = true;
			return;
		}
		if (!this.m_canceled)
		{
			this.m_mainPlayer.clip = (asset as VideoClip);
			this.m_mainPlayer.Prepare();
		}
	}

	// Token: 0x06008B89 RID: 35721 RVA: 0x002CA000 File Offset: 0x002C8200
	private void LogoLoaded(AssetReference assetRef, UnityEngine.Object asset, object callbackData)
	{
		if (asset == null)
		{
			Error.AddDevFatal("Failed to load Cinematic logo!", Array.Empty<object>());
			this.m_canceled = true;
			return;
		}
		if (!this.m_canceled)
		{
			this.m_logoPlayer.clip = (asset as VideoClip);
			this.m_logoPlayer.Prepare();
		}
	}

	// Token: 0x04007405 RID: 29701
	private static readonly string Hearthstone_Tavern_Abridged = "Hearthstone_Tavern_Abridged";

	// Token: 0x04007406 RID: 29702
	private static readonly string Hearthstone_Tavern_Abridged_Logo = "Hearthstone_Tavern_Abridged_Logo";

	// Token: 0x04007407 RID: 29703
	private static readonly AssetReference Hearthstone_Tavern_Abridged_Audio = new AssetReference("Hearthstone_Tavern_Abridged_Audio.wav:f89a884079f1645598bb19565f5915ef");

	// Token: 0x04007408 RID: 29704
	private static readonly string Mobile_Assets_Path = "UnimportedAssets/MobileAssets/Android/";

	// Token: 0x04007409 RID: 29705
	private AssetHandle<AudioClip> m_movieAudio;

	// Token: 0x0400740A RID: 29706
	private AudioSource m_audioSource;

	// Token: 0x0400740B RID: 29707
	private Camera m_camera;

	// Token: 0x0400740C RID: 29708
	private SoundDucker m_soundDucker;

	// Token: 0x0400740D RID: 29709
	private bool m_started;

	// Token: 0x0400740E RID: 29710
	private bool m_canceled;

	// Token: 0x0400740F RID: 29711
	private int m_previousTargetFrameRate;

	// Token: 0x04007410 RID: 29712
	private int m_previousSleepTimeout;

	// Token: 0x04007411 RID: 29713
	private Action m_callback;

	// Token: 0x04007412 RID: 29714
	private VideoPlayer m_mainPlayer;

	// Token: 0x04007413 RID: 29715
	private VideoPlayer m_logoPlayer;

	// Token: 0x04007414 RID: 29716
	private GameObject m_sceneObject;

	// Token: 0x04007415 RID: 29717
	private float m_playBeginTime;

	// Token: 0x04007416 RID: 29718
	private float m_playTime;
}
