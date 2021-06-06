using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

// Token: 0x020008E3 RID: 2275
public class LoadingScreen : MonoBehaviour
{
	// Token: 0x06007E1F RID: 32287 RVA: 0x0028D889 File Offset: 0x0028BA89
	private void Awake()
	{
		this.InitializeFxCamera();
		HearthstoneApplication.Get().WillReset += this.WillReset;
	}

	// Token: 0x06007E20 RID: 32288 RVA: 0x0028D8A8 File Offset: 0x0028BAA8
	private void OnDestroy()
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.WillReset -= this.WillReset;
		}
	}

	// Token: 0x06007E21 RID: 32289 RVA: 0x0028D8D6 File Offset: 0x0028BAD6
	private void Start()
	{
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x06007E22 RID: 32290 RVA: 0x0028D8F0 File Offset: 0x0028BAF0
	public static LoadingScreen Get()
	{
		SceneMgr sceneMgr;
		if (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			return sceneMgr.LoadingScreen;
		}
		return null;
	}

	// Token: 0x06007E23 RID: 32291 RVA: 0x0028D90E File Offset: 0x0028BB0E
	public Camera GetFxCamera()
	{
		return this.m_fxCamera;
	}

	// Token: 0x06007E24 RID: 32292 RVA: 0x0028D916 File Offset: 0x0028BB16
	public CameraFade GetCameraFade()
	{
		return base.GetComponent<CameraFade>();
	}

	// Token: 0x06007E25 RID: 32293 RVA: 0x0028D91E File Offset: 0x0028BB1E
	public void RegisterSceneListeners(SceneMgr sceneMgr)
	{
		sceneMgr.RegisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		sceneMgr.RegisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(this.OnSceneUnloaded));
		sceneMgr.RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
	}

	// Token: 0x06007E26 RID: 32294 RVA: 0x0028D956 File Offset: 0x0028BB56
	public void UnregisterSceneListeners(SceneMgr sceneMgr)
	{
		sceneMgr.UnregisterScenePreUnloadEvent(new SceneMgr.ScenePreUnloadCallback(this.OnScenePreUnload));
		sceneMgr.UnregisterSceneUnloadedEvent(new SceneMgr.SceneUnloadedCallback(this.OnSceneUnloaded));
		sceneMgr.UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
	}

	// Token: 0x06007E27 RID: 32295 RVA: 0x0028D991 File Offset: 0x0028BB91
	public static bool DoesShowLoadingScreen(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode)
	{
		return prevMode == SceneMgr.Mode.GAMEPLAY || nextMode == SceneMgr.Mode.GAMEPLAY;
	}

	// Token: 0x06007E28 RID: 32296 RVA: 0x0028D8D6 File Offset: 0x0028BAD6
	private void WillReset()
	{
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x06007E29 RID: 32297 RVA: 0x0028D9A0 File Offset: 0x0028BBA0
	public LoadingScreen.Phase GetPhase()
	{
		return this.m_phase;
	}

	// Token: 0x06007E2A RID: 32298 RVA: 0x0028D9A8 File Offset: 0x0028BBA8
	public bool IsTransitioning()
	{
		return this.m_phase > LoadingScreen.Phase.INVALID;
	}

	// Token: 0x06007E2B RID: 32299 RVA: 0x0028D9B4 File Offset: 0x0028BBB4
	public bool IsWaiting()
	{
		LoadingScreen.Phase phase = this.m_phase;
		return phase - LoadingScreen.Phase.WAITING_FOR_SCENE_UNLOAD <= 2;
	}

	// Token: 0x06007E2C RID: 32300 RVA: 0x0028D9D1 File Offset: 0x0028BBD1
	public bool IsFadingOut()
	{
		return this.m_phase == LoadingScreen.Phase.FADING_OUT;
	}

	// Token: 0x06007E2D RID: 32301 RVA: 0x0028D9DC File Offset: 0x0028BBDC
	public bool IsFadingIn()
	{
		return this.m_phase == LoadingScreen.Phase.FADING_IN;
	}

	// Token: 0x06007E2E RID: 32302 RVA: 0x0028D9E7 File Offset: 0x0028BBE7
	public bool IsFading()
	{
		return this.IsFadingOut() || this.IsFadingIn();
	}

	// Token: 0x06007E2F RID: 32303 RVA: 0x0028D9FE File Offset: 0x0028BBFE
	public bool IsPreviousSceneActive()
	{
		return this.m_previousSceneActive;
	}

	// Token: 0x06007E30 RID: 32304 RVA: 0x0028DA06 File Offset: 0x0028BC06
	public bool IsTransitionEnabled()
	{
		return this.m_transitionParams.IsEnabled();
	}

	// Token: 0x06007E31 RID: 32305 RVA: 0x0028DA13 File Offset: 0x0028BC13
	public void EnableTransition(bool enable)
	{
		this.m_transitionParams.Enable(enable);
	}

	// Token: 0x06007E32 RID: 32306 RVA: 0x0028DA21 File Offset: 0x0028BC21
	public void AddTransitionObject(GameObject go)
	{
		this.m_transitionParams.AddObject(go);
	}

	// Token: 0x06007E33 RID: 32307 RVA: 0x0028DA2F File Offset: 0x0028BC2F
	public void AddTransitionObject(Component c)
	{
		this.m_transitionParams.AddObject(c.gameObject);
	}

	// Token: 0x06007E34 RID: 32308 RVA: 0x0028DA42 File Offset: 0x0028BC42
	public void AddTransitionBlocker()
	{
		this.m_transitionParams.AddBlocker();
	}

	// Token: 0x06007E35 RID: 32309 RVA: 0x0028DA4F File Offset: 0x0028BC4F
	public void AddTransitionBlocker(int count)
	{
		this.m_transitionParams.AddBlocker(count);
	}

	// Token: 0x06007E36 RID: 32310 RVA: 0x0028DA5D File Offset: 0x0028BC5D
	public Camera GetFreezeFrameCamera()
	{
		return this.m_transitionParams.GetFreezeFrameCamera();
	}

	// Token: 0x06007E37 RID: 32311 RVA: 0x0028DA6A File Offset: 0x0028BC6A
	public void SetFreezeFrameCamera(Camera camera)
	{
		this.m_transitionParams.SetFreezeFrameCamera(camera);
	}

	// Token: 0x06007E38 RID: 32312 RVA: 0x0028DA78 File Offset: 0x0028BC78
	public AudioListener GetTransitionAudioListener()
	{
		return this.m_transitionParams.GetAudioListener();
	}

	// Token: 0x06007E39 RID: 32313 RVA: 0x0028DA85 File Offset: 0x0028BC85
	public void SetTransitionAudioListener(AudioListener listener)
	{
		Log.LoadingScreen.Print("LoadingScreen.SetTransitionAudioListener() - {0}", new object[]
		{
			listener
		});
		this.m_transitionParams.SetAudioListener(listener);
	}

	// Token: 0x06007E3A RID: 32314 RVA: 0x0028DAAC File Offset: 0x0028BCAC
	public void EnableFadeOut(bool enable)
	{
		this.m_transitionParams.EnableFadeOut(enable);
	}

	// Token: 0x06007E3B RID: 32315 RVA: 0x0028DABA File Offset: 0x0028BCBA
	public void EnableFadeIn(bool enable)
	{
		this.m_transitionParams.EnableFadeIn(enable);
	}

	// Token: 0x06007E3C RID: 32316 RVA: 0x0028DAC8 File Offset: 0x0028BCC8
	public Color GetFadeColor()
	{
		return this.m_transitionParams.GetFadeColor();
	}

	// Token: 0x06007E3D RID: 32317 RVA: 0x0028DAD5 File Offset: 0x0028BCD5
	public void SetFadeColor(Color color)
	{
		this.m_transitionParams.SetFadeColor(color);
	}

	// Token: 0x06007E3E RID: 32318 RVA: 0x0028DAE3 File Offset: 0x0028BCE3
	public void NotifyTransitionBlockerComplete()
	{
		if (this.m_prevTransitionParams == null)
		{
			return;
		}
		this.m_prevTransitionParams.RemoveBlocker();
		this.TransitionIfPossible();
	}

	// Token: 0x06007E3F RID: 32319 RVA: 0x0028DB00 File Offset: 0x0028BD00
	public void NotifyTransitionBlockerComplete(int count)
	{
		if (this.m_prevTransitionParams == null)
		{
			return;
		}
		this.m_prevTransitionParams.RemoveBlocker(count);
		this.TransitionIfPossible();
	}

	// Token: 0x06007E40 RID: 32320 RVA: 0x0028DB1E File Offset: 0x0028BD1E
	public void NotifyMainSceneObjectAwoke(GameObject mainObject)
	{
		if (!this.IsPreviousSceneActive())
		{
			return;
		}
		this.DisableTransitionUnfriendlyStuff(mainObject);
	}

	// Token: 0x06007E41 RID: 32321 RVA: 0x0028DB30 File Offset: 0x0028BD30
	public long GetAssetLoadStartTimestamp()
	{
		return this.m_assetLoadStartTimestamp;
	}

	// Token: 0x06007E42 RID: 32322 RVA: 0x0028DB38 File Offset: 0x0028BD38
	public void SetAssetLoadStartTimestamp(long timestamp)
	{
		this.m_assetLoadStartTimestamp = Math.Min(this.m_assetLoadStartTimestamp, timestamp);
		Log.LoadingScreen.Print("LoadingScreen.SetAssetLoadStartTimestamp() - m_assetLoadStartTimestamp={0}", new object[]
		{
			this.m_assetLoadStartTimestamp
		});
	}

	// Token: 0x06007E43 RID: 32323 RVA: 0x0028DB6F File Offset: 0x0028BD6F
	public bool RegisterPreviousSceneDestroyedListener(LoadingScreen.PreviousSceneDestroyedCallback callback)
	{
		return this.RegisterPreviousSceneDestroyedListener(callback, null);
	}

	// Token: 0x06007E44 RID: 32324 RVA: 0x0028DB7C File Offset: 0x0028BD7C
	public bool RegisterPreviousSceneDestroyedListener(LoadingScreen.PreviousSceneDestroyedCallback callback, object userData)
	{
		LoadingScreen.PreviousSceneDestroyedListener previousSceneDestroyedListener = new LoadingScreen.PreviousSceneDestroyedListener();
		previousSceneDestroyedListener.SetCallback(callback);
		previousSceneDestroyedListener.SetUserData(userData);
		if (this.m_prevSceneDestroyedListeners.Contains(previousSceneDestroyedListener))
		{
			return false;
		}
		this.m_prevSceneDestroyedListeners.Add(previousSceneDestroyedListener);
		return true;
	}

	// Token: 0x06007E45 RID: 32325 RVA: 0x0028DBBA File Offset: 0x0028BDBA
	public bool UnregisterPreviousSceneDestroyedListener(LoadingScreen.PreviousSceneDestroyedCallback callback)
	{
		return this.UnregisterPreviousSceneDestroyedListener(callback, null);
	}

	// Token: 0x06007E46 RID: 32326 RVA: 0x0028DBC4 File Offset: 0x0028BDC4
	public bool UnregisterPreviousSceneDestroyedListener(LoadingScreen.PreviousSceneDestroyedCallback callback, object userData)
	{
		LoadingScreen.PreviousSceneDestroyedListener previousSceneDestroyedListener = new LoadingScreen.PreviousSceneDestroyedListener();
		previousSceneDestroyedListener.SetCallback(callback);
		previousSceneDestroyedListener.SetUserData(userData);
		return this.m_prevSceneDestroyedListeners.Remove(previousSceneDestroyedListener);
	}

	// Token: 0x06007E47 RID: 32327 RVA: 0x0028DBF1 File Offset: 0x0028BDF1
	public bool RegisterFinishedTransitionListener(LoadingScreen.FinishedTransitionCallback callback)
	{
		return this.RegisterFinishedTransitionListener(callback, null);
	}

	// Token: 0x06007E48 RID: 32328 RVA: 0x0028DBFC File Offset: 0x0028BDFC
	public bool RegisterFinishedTransitionListener(LoadingScreen.FinishedTransitionCallback callback, object userData)
	{
		LoadingScreen.FinishedTransitionListener finishedTransitionListener = new LoadingScreen.FinishedTransitionListener();
		finishedTransitionListener.SetCallback(callback);
		finishedTransitionListener.SetUserData(userData);
		if (this.m_finishedTransitionListeners.Contains(finishedTransitionListener))
		{
			return false;
		}
		this.m_finishedTransitionListeners.Add(finishedTransitionListener);
		return true;
	}

	// Token: 0x06007E49 RID: 32329 RVA: 0x0028DC3A File Offset: 0x0028BE3A
	public bool UnregisterFinishedTransitionListener(LoadingScreen.FinishedTransitionCallback callback)
	{
		return this.UnregisterFinishedTransitionListener(callback, null);
	}

	// Token: 0x06007E4A RID: 32330 RVA: 0x0028DC44 File Offset: 0x0028BE44
	public bool UnregisterFinishedTransitionListener(LoadingScreen.FinishedTransitionCallback callback, object userData)
	{
		LoadingScreen.FinishedTransitionListener finishedTransitionListener = new LoadingScreen.FinishedTransitionListener();
		finishedTransitionListener.SetCallback(callback);
		finishedTransitionListener.SetUserData(userData);
		return this.m_finishedTransitionListeners.Remove(finishedTransitionListener);
	}

	// Token: 0x06007E4B RID: 32331 RVA: 0x0028DC71 File Offset: 0x0028BE71
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		this.EnableTransition(false);
	}

	// Token: 0x06007E4C RID: 32332 RVA: 0x0028DC94 File Offset: 0x0028BE94
	private void OnScenePreUnload(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		Log.LoadingScreen.Print("LoadingScreen.OnScenePreUnload() - prevMode={0} nextMode={1} m_phase={2}", new object[]
		{
			prevMode,
			SceneMgr.Get().GetMode(),
			this.m_phase
		});
		if (!LoadingScreen.DoesShowLoadingScreen(prevMode, SceneMgr.Get().GetMode()))
		{
			this.CutoffTransition();
			return;
		}
		if (!this.m_transitionParams.IsEnabled())
		{
			this.CutoffTransition();
			return;
		}
		if (this.IsTransitioning())
		{
			this.DoInterruptionCleanUp();
		}
		this.m_assetLoadNextStartTimestamp = TimeUtils.BinaryStamp();
		if (this.IsTransitioning())
		{
			this.FireFinishedTransitionListeners(true);
			if (this.IsPreviousSceneActive())
			{
				return;
			}
		}
		this.m_phase = LoadingScreen.Phase.WAITING_FOR_SCENE_UNLOAD;
		this.m_previousSceneActive = true;
		this.ShowFreezeFrame(this.m_transitionParams.GetFreezeFrameCamera());
	}

	// Token: 0x06007E4D RID: 32333 RVA: 0x0028DD5C File Offset: 0x0028BF5C
	private void OnSceneUnloaded(SceneMgr.Mode prevMode, PegasusScene prevScene, object userData)
	{
		Log.LoadingScreen.Print("LoadingScreen.OnSceneUnloaded() - prevMode={0} nextMode={1} m_phase={2}", new object[]
		{
			prevMode,
			SceneMgr.Get().GetMode(),
			this.m_phase
		});
		if (this.m_phase != LoadingScreen.Phase.WAITING_FOR_SCENE_UNLOAD)
		{
			return;
		}
		this.m_assetLoadEndTimestamp = this.m_assetLoadNextStartTimestamp;
		Log.LoadingScreen.Print("LoadingScreen.OnSceneUnloaded() - m_assetLoadEndTimestamp={0}", new object[]
		{
			this.m_assetLoadEndTimestamp
		});
		this.m_phase = LoadingScreen.Phase.WAITING_FOR_SCENE_LOAD;
		this.m_prevTransitionParams = this.m_transitionParams;
		this.m_transitionParams = new LoadingScreen.TransitionParams();
		this.m_transitionParams.ClearPreviousAssets = (prevMode != SceneMgr.Get().GetMode());
		this.m_prevTransitionParams.AutoAddObjects();
		if (this.m_fxCamera != null)
		{
			float highestCameraDepth = this.m_prevTransitionParams.GetHighestCameraDepth();
			if (highestCameraDepth >= this.m_fxCamera.depth - 1f)
			{
				Debug.LogWarning("FX Camera's depth was less than or equal to previous transition's camera stack. Moving to a higher depth");
				this.m_fxCamera.depth = highestCameraDepth + 2f;
			}
			this.m_prevTransitionParams.FixCameraTagsAndDepths(this.m_fxCamera.depth);
		}
		this.m_prevTransitionParams.PreserveObjects(base.transform);
		this.m_originalPosX = base.transform.position.x;
		TransformUtil.SetPosX(base.gameObject, 5000f);
	}

	// Token: 0x06007E4E RID: 32334 RVA: 0x0028DEBC File Offset: 0x0028C0BC
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		Log.LoadingScreen.Print("LoadingScreen.OnSceneLoaded() - prevMode={0} currMode={1}", new object[]
		{
			SceneMgr.Get().GetPrevMode(),
			mode
		});
		if (mode == SceneMgr.Mode.FATAL_ERROR)
		{
			Log.LoadingScreen.Print("LoadingScreen.OnSceneLoaded() - calling CutoffTransition()", new object[]
			{
				mode
			});
			this.CutoffTransition();
			return;
		}
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.STARTUP)
		{
			this.m_assetLoadStartTimestamp = TimeUtils.BinaryStamp();
			Log.LoadingScreen.Print("LoadingScreen.OnSceneLoaded() - m_assetLoadStartTimestamp={0}", new object[]
			{
				this.m_assetLoadStartTimestamp
			});
		}
		if (this.m_phase != LoadingScreen.Phase.WAITING_FOR_SCENE_LOAD)
		{
			Log.LoadingScreen.Print("LoadingScreen.OnSceneLoaded() - END - {0} != Phase.WAITING_FOR_SCENE_LOAD", new object[]
			{
				this.m_phase
			});
			return;
		}
		this.m_phase = LoadingScreen.Phase.WAITING_FOR_BLOCKERS;
		this.TransitionIfPossible();
	}

	// Token: 0x06007E4F RID: 32335 RVA: 0x0028DF99 File Offset: 0x0028C199
	private bool TransitionIfPossible()
	{
		if (this.m_prevTransitionParams.GetBlockerCount() > 0)
		{
			return false;
		}
		base.StartCoroutine("HackWaitThenStartTransitionEffects");
		return true;
	}

	// Token: 0x06007E50 RID: 32336 RVA: 0x0028DFB8 File Offset: 0x0028C1B8
	private IEnumerator HackWaitThenStartTransitionEffects()
	{
		Log.LoadingScreen.Print("LoadingScreen.HackWaitThenStartTransitionEffects() - START", Array.Empty<object>());
		yield return new WaitForEndOfFrame();
		if (this.m_phase != LoadingScreen.Phase.WAITING_FOR_BLOCKERS)
		{
			Log.LoadingScreen.Print("LoadingScreen.HackWaitThenStartTransitionEffects() - END - {0} != Phase.WAITING_FOR_BLOCKERS", new object[]
			{
				this.m_phase
			});
			yield break;
		}
		this.FadeOut();
		yield break;
	}

	// Token: 0x06007E51 RID: 32337 RVA: 0x0028DFC8 File Offset: 0x0028C1C8
	private void FirePreviousSceneDestroyedListeners()
	{
		LoadingScreen.PreviousSceneDestroyedListener[] array = this.m_prevSceneDestroyedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire();
		}
	}

	// Token: 0x06007E52 RID: 32338 RVA: 0x0028DFF8 File Offset: 0x0028C1F8
	private void FireFinishedTransitionListeners(bool cutoff)
	{
		LoadingScreen.FinishedTransitionListener[] array = this.m_finishedTransitionListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(cutoff);
		}
	}

	// Token: 0x06007E53 RID: 32339 RVA: 0x0028E028 File Offset: 0x0028C228
	private void FadeOut()
	{
		Log.LoadingScreen.Print("LoadingScreen.FadeOut()", Array.Empty<object>());
		this.m_phase = LoadingScreen.Phase.FADING_OUT;
		if (!this.m_prevTransitionParams.IsFadeOutEnabled())
		{
			this.OnFadeOutComplete();
			return;
		}
		CameraFade cameraFade = base.GetComponent<CameraFade>();
		if (cameraFade == null)
		{
			Debug.LogError("LoadingScreen FadeOut(): Failed to find CameraFade component");
			return;
		}
		cameraFade.m_Color = this.m_prevTransitionParams.GetFadeColor();
		Action<object> action = delegate(object amount)
		{
			cameraFade.m_Fade = (float)amount;
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_FadeOutSec,
			"from",
			cameraFade.m_Fade,
			"to",
			1f,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject,
			"oncomplete",
			"OnFadeOutComplete",
			"oncompletetarget",
			base.gameObject,
			"name",
			"Fade"
		});
		iTween.ValueTo(base.gameObject, args);
	}

	// Token: 0x06007E54 RID: 32340 RVA: 0x0028E166 File Offset: 0x0028C366
	private void OnFadeOutComplete()
	{
		Log.LoadingScreen.Print("LoadingScreen.OnFadeOutComplete()", Array.Empty<object>());
		this.FinishPreviousScene();
		this.FadeIn();
	}

	// Token: 0x06007E55 RID: 32341 RVA: 0x0028E188 File Offset: 0x0028C388
	private void FadeIn()
	{
		Log.LoadingScreen.Print("LoadingScreen.FadeIn()", Array.Empty<object>());
		this.m_phase = LoadingScreen.Phase.FADING_IN;
		if (!this.m_prevTransitionParams.IsFadeInEnabled())
		{
			this.OnFadeInComplete();
			return;
		}
		CameraFade cameraFade = base.GetComponent<CameraFade>();
		if (cameraFade == null)
		{
			Debug.LogError("LoadingScreen FadeIn(): Failed to find CameraFade component");
			return;
		}
		cameraFade.m_Color = this.m_prevTransitionParams.GetFadeColor();
		Action<object> action = delegate(object amount)
		{
			cameraFade.m_Fade = (float)amount;
		};
		action(1f);
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			this.m_FadeInSec,
			"from",
			1f,
			"to",
			0f,
			"onupdate",
			action,
			"onupdatetarget",
			base.gameObject,
			"oncomplete",
			"OnFadeInComplete",
			"oncompletetarget",
			base.gameObject,
			"name",
			"Fade"
		});
		iTween.ValueTo(base.gameObject, args);
	}

	// Token: 0x06007E56 RID: 32342 RVA: 0x0028E2D0 File Offset: 0x0028C4D0
	private void OnFadeInComplete()
	{
		Log.LoadingScreen.Print("LoadingScreen.OnFadeInComplete()", Array.Empty<object>());
		this.FinishFxCamera();
		this.m_prevTransitionParams = null;
		this.m_phase = LoadingScreen.Phase.INVALID;
		this.FireFinishedTransitionListeners(false);
	}

	// Token: 0x06007E57 RID: 32343 RVA: 0x0028E301 File Offset: 0x0028C501
	private void InitializeFxCamera()
	{
		this.m_fxCamera = base.GetComponent<Camera>();
	}

	// Token: 0x06007E58 RID: 32344 RVA: 0x0028E310 File Offset: 0x0028C510
	private void FinishFxCamera()
	{
		CameraFade cameraFade = base.GetComponent<CameraFade>();
		if (cameraFade == null)
		{
			Debug.LogError("LoadingScreen.FinishFxCamera(): Failed to find CameraFade component");
			return;
		}
		if (cameraFade.m_Fade > 0f)
		{
			Action<object> action = delegate(object amount)
			{
				cameraFade.m_Fade = (float)amount;
			};
			Hashtable args = iTween.Hash(new object[]
			{
				"time",
				0.3f,
				"from",
				cameraFade.m_Fade,
				"to",
				0f,
				"onupdate",
				action,
				"onupdatetarget",
				base.gameObject,
				"oncompletetarget",
				base.gameObject,
				"delay",
				0.5f,
				"name",
				"Fade"
			});
			iTween.ValueTo(base.gameObject, args);
		}
	}

	// Token: 0x06007E59 RID: 32345 RVA: 0x0028E424 File Offset: 0x0028C624
	private FreezeFrame GetFreezeFrameEffect(Camera camera)
	{
		FreezeFrame component = camera.GetComponent<FreezeFrame>();
		if (component != null)
		{
			return component;
		}
		return camera.gameObject.AddComponent<FreezeFrame>();
	}

	// Token: 0x06007E5A RID: 32346 RVA: 0x0028E44E File Offset: 0x0028C64E
	private void ShowFreezeFrame(Camera camera)
	{
		if (camera == null)
		{
			return;
		}
		FreezeFrame freezeFrameEffect = this.GetFreezeFrameEffect(camera);
		freezeFrameEffect.enabled = true;
		freezeFrameEffect.Freeze();
	}

	// Token: 0x06007E5B RID: 32347 RVA: 0x0028E470 File Offset: 0x0028C670
	private void CutoffTransition()
	{
		if (!this.IsTransitioning())
		{
			this.m_transitionParams = new LoadingScreen.TransitionParams();
			return;
		}
		this.StopFading();
		this.FinishPreviousScene();
		this.FinishFxCamera();
		this.m_prevTransitionParams = null;
		this.m_transitionParams = new LoadingScreen.TransitionParams();
		this.m_phase = LoadingScreen.Phase.INVALID;
		this.FireFinishedTransitionListeners(true);
	}

	// Token: 0x06007E5C RID: 32348 RVA: 0x0028E4C3 File Offset: 0x0028C6C3
	private void StopFading()
	{
		iTween.Stop(base.gameObject);
	}

	// Token: 0x06007E5D RID: 32349 RVA: 0x0028E4D0 File Offset: 0x0028C6D0
	private void DoInterruptionCleanUp()
	{
		bool flag = this.IsPreviousSceneActive();
		Log.LoadingScreen.Print("LoadingScreen.DoInterruptionCleanUp() - m_phase={0} previousSceneActive={1}", new object[]
		{
			this.m_phase,
			flag
		});
		if (this.m_phase == LoadingScreen.Phase.WAITING_FOR_BLOCKERS)
		{
			base.StopCoroutine("HackWaitThenStartTransitionEffects");
		}
		if (this.IsFading())
		{
			this.StopFading();
			if (this.IsFadingIn())
			{
				this.m_prevTransitionParams = null;
			}
		}
		if (flag)
		{
			long assetLoadNextStartTimestamp = this.m_assetLoadNextStartTimestamp;
			long endTimestamp = TimeUtils.BinaryStamp();
			this.ClearAssets(assetLoadNextStartTimestamp, endTimestamp);
			this.m_transitionUnfriendlyData.Clear();
			this.m_transitionParams = new LoadingScreen.TransitionParams();
			this.m_phase = LoadingScreen.Phase.WAITING_FOR_SCENE_LOAD;
		}
	}

	// Token: 0x06007E5E RID: 32350 RVA: 0x0028E578 File Offset: 0x0028C778
	private void FinishPreviousScene()
	{
		Log.LoadingScreen.Print("LoadingScreen.FinishPreviousScene()", Array.Empty<object>());
		if (this.m_prevTransitionParams != null)
		{
			this.m_prevTransitionParams.DestroyObjects();
			TransformUtil.SetPosX(base.gameObject, this.m_originalPosX);
		}
		if (this.m_transitionParams.ClearPreviousAssets)
		{
			this.ClearPreviousSceneAssets();
		}
		this.m_transitionUnfriendlyData.Restore();
		this.m_transitionUnfriendlyData.Clear();
		this.m_previousSceneActive = false;
		this.FirePreviousSceneDestroyedListeners();
	}

	// Token: 0x06007E5F RID: 32351 RVA: 0x0028E5F4 File Offset: 0x0028C7F4
	private void ClearPreviousSceneAssets()
	{
		Log.LoadingScreen.Print("LoadingScreen.ClearPreviousSceneAssets() - START m_assetLoadStartTimestamp={0} m_assetLoadEndTimestamp={1}", new object[]
		{
			this.m_assetLoadStartTimestamp,
			this.m_assetLoadEndTimestamp
		});
		this.ClearAssets(this.m_assetLoadStartTimestamp, this.m_assetLoadEndTimestamp);
		this.m_assetLoadStartTimestamp = this.m_assetLoadNextStartTimestamp;
		this.m_assetLoadEndTimestamp = 0L;
		this.m_assetLoadNextStartTimestamp = 0L;
		Log.LoadingScreen.Print("LoadingScreen.ClearPreviousSceneAssets() - END m_assetLoadStartTimestamp={0} m_assetLoadEndTimestamp={1}", new object[]
		{
			this.m_assetLoadStartTimestamp,
			this.m_assetLoadEndTimestamp
		});
	}

	// Token: 0x06007E60 RID: 32352 RVA: 0x0028E694 File Offset: 0x0028C894
	private void ClearAssets(long startTimestamp, long endTimestamp)
	{
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.UnloadUnusedAssets();
		}
	}

	// Token: 0x06007E61 RID: 32353 RVA: 0x0028E6B8 File Offset: 0x0028C8B8
	private void DisableTransitionUnfriendlyStuff(GameObject mainObject)
	{
		Log.LoadingScreen.Print("LoadingScreen.DisableTransitionUnfriendlyStuff() - {0}", new object[]
		{
			mainObject
		});
		AudioListener[] componentsInChildren = base.GetComponentsInChildren<AudioListener>();
		bool flag = false;
		foreach (AudioListener audioListener in componentsInChildren)
		{
			flag |= (audioListener != null && audioListener.enabled);
		}
		if (flag)
		{
			AudioListener componentInChildren = mainObject.GetComponentInChildren<AudioListener>();
			this.m_transitionUnfriendlyData.SetAudioListener(componentInChildren);
		}
		Light[] componentsInChildren2 = mainObject.GetComponentsInChildren<Light>();
		this.m_transitionUnfriendlyData.AddLights(componentsInChildren2);
	}

	// Token: 0x040065FC RID: 26108
	public float m_FadeOutSec = 1f;

	// Token: 0x040065FD RID: 26109
	public iTween.EaseType m_FadeOutEaseType = iTween.EaseType.linear;

	// Token: 0x040065FE RID: 26110
	public float m_FadeInSec = 1f;

	// Token: 0x040065FF RID: 26111
	public iTween.EaseType m_FadeInEaseType = iTween.EaseType.linear;

	// Token: 0x04006600 RID: 26112
	private const float MIDDLE_OF_NOWHERE_X = 5000f;

	// Token: 0x04006601 RID: 26113
	private LoadingScreen.Phase m_phase;

	// Token: 0x04006602 RID: 26114
	private bool m_previousSceneActive;

	// Token: 0x04006603 RID: 26115
	private LoadingScreen.TransitionParams m_prevTransitionParams;

	// Token: 0x04006604 RID: 26116
	private LoadingScreen.TransitionParams m_transitionParams = new LoadingScreen.TransitionParams();

	// Token: 0x04006605 RID: 26117
	private LoadingScreen.TransitionUnfriendlyData m_transitionUnfriendlyData = new LoadingScreen.TransitionUnfriendlyData();

	// Token: 0x04006606 RID: 26118
	private Camera m_fxCamera;

	// Token: 0x04006607 RID: 26119
	private List<LoadingScreen.PreviousSceneDestroyedListener> m_prevSceneDestroyedListeners = new List<LoadingScreen.PreviousSceneDestroyedListener>();

	// Token: 0x04006608 RID: 26120
	private List<LoadingScreen.FinishedTransitionListener> m_finishedTransitionListeners = new List<LoadingScreen.FinishedTransitionListener>();

	// Token: 0x04006609 RID: 26121
	private float m_originalPosX;

	// Token: 0x0400660A RID: 26122
	private long m_assetLoadStartTimestamp;

	// Token: 0x0400660B RID: 26123
	private long m_assetLoadEndTimestamp;

	// Token: 0x0400660C RID: 26124
	private long m_assetLoadNextStartTimestamp;

	// Token: 0x02002587 RID: 9607
	// (Invoke) Token: 0x0601339B RID: 78747
	public delegate void PreviousSceneDestroyedCallback(object userData);

	// Token: 0x02002588 RID: 9608
	// (Invoke) Token: 0x0601339F RID: 78751
	public delegate void FinishedTransitionCallback(bool cutoff, object userData);

	// Token: 0x02002589 RID: 9609
	public enum Phase
	{
		// Token: 0x0400EDDE RID: 60894
		INVALID,
		// Token: 0x0400EDDF RID: 60895
		WAITING_FOR_SCENE_UNLOAD,
		// Token: 0x0400EDE0 RID: 60896
		WAITING_FOR_SCENE_LOAD,
		// Token: 0x0400EDE1 RID: 60897
		WAITING_FOR_BLOCKERS,
		// Token: 0x0400EDE2 RID: 60898
		FADING_OUT,
		// Token: 0x0400EDE3 RID: 60899
		FADING_IN
	}

	// Token: 0x0200258A RID: 9610
	private class PreviousSceneDestroyedListener : EventListener<LoadingScreen.PreviousSceneDestroyedCallback>
	{
		// Token: 0x060133A2 RID: 78754 RVA: 0x0052E2B9 File Offset: 0x0052C4B9
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}

	// Token: 0x0200258B RID: 9611
	private class FinishedTransitionListener : EventListener<LoadingScreen.FinishedTransitionCallback>
	{
		// Token: 0x060133A4 RID: 78756 RVA: 0x0052E2D4 File Offset: 0x0052C4D4
		public void Fire(bool cutoff)
		{
			this.m_callback(cutoff, this.m_userData);
		}
	}

	// Token: 0x0200258C RID: 9612
	private class TransitionParams
	{
		// Token: 0x060133A6 RID: 78758 RVA: 0x0052E2F0 File Offset: 0x0052C4F0
		public bool IsEnabled()
		{
			return this.m_enabled;
		}

		// Token: 0x060133A7 RID: 78759 RVA: 0x0052E2F8 File Offset: 0x0052C4F8
		public void Enable(bool enable)
		{
			this.m_enabled = enable;
		}

		// Token: 0x060133A8 RID: 78760 RVA: 0x0052E301 File Offset: 0x0052C501
		public void AddObject(Component c)
		{
			if (c == null)
			{
				return;
			}
			this.AddObject(c.gameObject);
		}

		// Token: 0x060133A9 RID: 78761 RVA: 0x0052E31C File Offset: 0x0052C51C
		public void AddObject(GameObject go)
		{
			if (go == null)
			{
				return;
			}
			Transform transform = go.transform;
			while (transform != null)
			{
				if (this.m_objects.Contains(transform.gameObject))
				{
					return;
				}
				transform = transform.parent;
			}
			foreach (Camera item in go.GetComponentsInChildren<Camera>())
			{
				if (!this.m_cameras.Contains(item))
				{
					this.m_cameras.Add(item);
				}
			}
			this.m_objects.Add(go);
		}

		// Token: 0x060133AA RID: 78762 RVA: 0x0052E39F File Offset: 0x0052C59F
		public void AddBlocker()
		{
			this.m_blockerCount++;
		}

		// Token: 0x060133AB RID: 78763 RVA: 0x0052E3AF File Offset: 0x0052C5AF
		public void AddBlocker(int count)
		{
			this.m_blockerCount += count;
		}

		// Token: 0x060133AC RID: 78764 RVA: 0x0052E3BF File Offset: 0x0052C5BF
		public void RemoveBlocker()
		{
			this.m_blockerCount--;
		}

		// Token: 0x060133AD RID: 78765 RVA: 0x0052E3CF File Offset: 0x0052C5CF
		public void RemoveBlocker(int count)
		{
			this.m_blockerCount -= count;
		}

		// Token: 0x060133AE RID: 78766 RVA: 0x0052E3DF File Offset: 0x0052C5DF
		public int GetBlockerCount()
		{
			return this.m_blockerCount;
		}

		// Token: 0x060133AF RID: 78767 RVA: 0x0052E3E7 File Offset: 0x0052C5E7
		public void SetFreezeFrameCamera(Camera camera)
		{
			if (camera == null)
			{
				return;
			}
			this.m_freezeFrameCamera = camera;
			this.AddObject(camera.gameObject);
		}

		// Token: 0x060133B0 RID: 78768 RVA: 0x0052E406 File Offset: 0x0052C606
		public Camera GetFreezeFrameCamera()
		{
			return this.m_freezeFrameCamera;
		}

		// Token: 0x060133B1 RID: 78769 RVA: 0x0052E40E File Offset: 0x0052C60E
		public AudioListener GetAudioListener()
		{
			return this.m_audioListener;
		}

		// Token: 0x060133B2 RID: 78770 RVA: 0x0052E416 File Offset: 0x0052C616
		public void SetAudioListener(AudioListener listener)
		{
			if (listener == null)
			{
				return;
			}
			this.m_audioListener = listener;
			this.AddObject(listener);
		}

		// Token: 0x060133B3 RID: 78771 RVA: 0x0052E430 File Offset: 0x0052C630
		public void EnableFadeOut(bool enable)
		{
			this.m_fadeOut = enable;
		}

		// Token: 0x060133B4 RID: 78772 RVA: 0x0052E439 File Offset: 0x0052C639
		public bool IsFadeOutEnabled()
		{
			return this.m_fadeOut;
		}

		// Token: 0x060133B5 RID: 78773 RVA: 0x0052E441 File Offset: 0x0052C641
		public void EnableFadeIn(bool enable)
		{
			this.m_fadeIn = enable;
		}

		// Token: 0x060133B6 RID: 78774 RVA: 0x0052E44A File Offset: 0x0052C64A
		public bool IsFadeInEnabled()
		{
			return this.m_fadeIn;
		}

		// Token: 0x060133B7 RID: 78775 RVA: 0x0052E452 File Offset: 0x0052C652
		public void SetFadeColor(Color color)
		{
			this.m_fadeColor = color;
		}

		// Token: 0x060133B8 RID: 78776 RVA: 0x0052E45B File Offset: 0x0052C65B
		public Color GetFadeColor()
		{
			return this.m_fadeColor;
		}

		// Token: 0x060133B9 RID: 78777 RVA: 0x0052E463 File Offset: 0x0052C663
		public List<Camera> GetCameras()
		{
			return this.m_cameras;
		}

		// Token: 0x060133BA RID: 78778 RVA: 0x0052E46B File Offset: 0x0052C66B
		public List<Light> GetLights()
		{
			return this.m_lights;
		}

		// Token: 0x17002BC9 RID: 11209
		// (get) Token: 0x060133BB RID: 78779 RVA: 0x0052E473 File Offset: 0x0052C673
		// (set) Token: 0x060133BC RID: 78780 RVA: 0x0052E47B File Offset: 0x0052C67B
		public bool ClearPreviousAssets
		{
			get
			{
				return this.m_clearPreviousAssets;
			}
			set
			{
				this.m_clearPreviousAssets = value;
			}
		}

		// Token: 0x060133BD RID: 78781 RVA: 0x0052E484 File Offset: 0x0052C684
		public void FixCameraTagsAndDepths(float fxCameraDepth)
		{
			if (this.m_cameras.Count == 0)
			{
				return;
			}
			this.UntagCameras();
			this.BoostCamerasToJustBelowDepth(fxCameraDepth);
		}

		// Token: 0x060133BE RID: 78782 RVA: 0x0052E4A4 File Offset: 0x0052C6A4
		private void UntagCameras()
		{
			foreach (Camera camera in this.m_cameras)
			{
				camera.tag = "Untagged";
			}
		}

		// Token: 0x060133BF RID: 78783 RVA: 0x0052E4FC File Offset: 0x0052C6FC
		private void BoostCamerasToJustBelowDepth(float targetDepth)
		{
			float highestCameraDepth = this.GetHighestCameraDepth();
			float num = targetDepth - 1f - highestCameraDepth;
			for (int i = 0; i < this.m_cameras.Count; i++)
			{
				this.m_cameras[i].depth += num;
			}
		}

		// Token: 0x060133C0 RID: 78784 RVA: 0x0052E54C File Offset: 0x0052C74C
		public float GetHighestCameraDepth()
		{
			if (this.m_cameras.Count == 0)
			{
				return 0f;
			}
			float num = this.m_cameras[0].depth;
			for (int i = 1; i < this.m_cameras.Count; i++)
			{
				float depth = this.m_cameras[i].depth;
				if (num < depth)
				{
					num = depth;
				}
			}
			return num;
		}

		// Token: 0x060133C1 RID: 78785 RVA: 0x0052E5B0 File Offset: 0x0052C7B0
		public void AutoAddObjects()
		{
			foreach (Light light in (Light[])UnityEngine.Object.FindObjectsOfType(typeof(Light)))
			{
				this.AddObject(light.gameObject);
				this.m_lights.Add(light);
			}
		}

		// Token: 0x060133C2 RID: 78786 RVA: 0x0052E5FC File Offset: 0x0052C7FC
		public void PreserveObjects(Transform parent)
		{
			foreach (GameObject gameObject in this.m_objects)
			{
				if (!(gameObject == null))
				{
					gameObject.transform.parent = parent;
				}
			}
		}

		// Token: 0x060133C3 RID: 78787 RVA: 0x0052E660 File Offset: 0x0052C860
		public void DestroyObjects()
		{
			foreach (GameObject obj in this.m_objects)
			{
				UnityEngine.Object.DestroyImmediate(obj);
			}
		}

		// Token: 0x0400EDE4 RID: 60900
		private bool m_enabled = true;

		// Token: 0x0400EDE5 RID: 60901
		private List<GameObject> m_objects = new List<GameObject>();

		// Token: 0x0400EDE6 RID: 60902
		private List<Camera> m_cameras = new List<Camera>();

		// Token: 0x0400EDE7 RID: 60903
		private List<Light> m_lights = new List<Light>();

		// Token: 0x0400EDE8 RID: 60904
		private Camera m_freezeFrameCamera;

		// Token: 0x0400EDE9 RID: 60905
		private AudioListener m_audioListener;

		// Token: 0x0400EDEA RID: 60906
		private int m_blockerCount;

		// Token: 0x0400EDEB RID: 60907
		private bool m_fadeOut = true;

		// Token: 0x0400EDEC RID: 60908
		private bool m_fadeIn = true;

		// Token: 0x0400EDED RID: 60909
		private Color m_fadeColor = Color.black;

		// Token: 0x0400EDEE RID: 60910
		private bool m_clearPreviousAssets = true;
	}

	// Token: 0x0200258D RID: 9613
	private class TransitionUnfriendlyData
	{
		// Token: 0x060133C5 RID: 78789 RVA: 0x0052E70B File Offset: 0x0052C90B
		public void Clear()
		{
			this.m_audioListener = null;
			this.m_lights.Clear();
		}

		// Token: 0x060133C6 RID: 78790 RVA: 0x0052E71F File Offset: 0x0052C91F
		public AudioListener GetAudioListener()
		{
			return this.m_audioListener;
		}

		// Token: 0x060133C7 RID: 78791 RVA: 0x0052E727 File Offset: 0x0052C927
		public void SetAudioListener(AudioListener listener)
		{
			if (listener == null)
			{
				return;
			}
			if (!listener.enabled)
			{
				return;
			}
			this.m_audioListener = listener;
			this.m_audioListener.enabled = false;
		}

		// Token: 0x060133C8 RID: 78792 RVA: 0x0052E74F File Offset: 0x0052C94F
		public List<Light> GetLights()
		{
			return this.m_lights;
		}

		// Token: 0x060133C9 RID: 78793 RVA: 0x0052E758 File Offset: 0x0052C958
		public void AddLights(Light[] lights)
		{
			foreach (Light light in lights)
			{
				if (light.enabled)
				{
					light.enabled = false;
					Transform transform = light.transform;
					while (transform.parent != null)
					{
						transform = transform.parent;
					}
					this.m_lights.Add(light);
				}
			}
		}

		// Token: 0x060133CA RID: 78794 RVA: 0x0052E7B4 File Offset: 0x0052C9B4
		public void Restore()
		{
			for (int i = 0; i < this.m_lights.Count; i++)
			{
				Light light = this.m_lights[i];
				if (light == null)
				{
					Debug.LogError(string.Format("TransitionUnfriendlyData.Restore() - light {0} is null!", i));
				}
				else
				{
					Transform transform = light.transform;
					while (transform.parent != null)
					{
						transform = transform.parent;
					}
					light.enabled = true;
				}
			}
			if (this.m_audioListener != null)
			{
				this.m_audioListener.enabled = true;
			}
		}

		// Token: 0x0400EDEF RID: 60911
		private AudioListener m_audioListener;

		// Token: 0x0400EDF0 RID: 60912
		private List<Light> m_lights = new List<Light>();
	}
}
