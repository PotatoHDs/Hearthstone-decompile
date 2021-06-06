using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.UI;
using UnityEngine;

public class FullScreenFXMgr : IService
{
	public class ScreenEffectsInstance : IComparable
	{
		public readonly UnityEngine.Object Owner;

		public readonly FullScreenEffects EffectsComponent;

		public Camera Camera
		{
			get
			{
				if (!(EffectsComponent != null))
				{
					return null;
				}
				return EffectsComponent.Camera;
			}
		}

		public ScreenEffectsInstance(UnityEngine.Object owner, FullScreenEffects screenEffects)
		{
			Owner = owner;
			EffectsComponent = screenEffects;
		}

		public int CompareTo(object obj)
		{
			ScreenEffectsInstance screenEffectsInstance = obj as ScreenEffectsInstance;
			if (screenEffectsInstance == null)
			{
				return -1;
			}
			if (Camera == null && screenEffectsInstance.Camera == null)
			{
				return 0;
			}
			if (Camera != null && screenEffectsInstance.Camera == null)
			{
				return -1;
			}
			if (Camera == null && screenEffectsInstance.Camera != null)
			{
				return 1;
			}
			if (Camera.depth > screenEffectsInstance.Camera.depth)
			{
				return -1;
			}
			if (Camera.depth < screenEffectsInstance.Camera.depth)
			{
				return 1;
			}
			return 0;
		}
	}

	private List<ScreenEffectsInstance> m_layeredScreenEffectsByDepth = new List<ScreenEffectsInstance>();

	private int m_ActiveEffectsCount;

	private int m_StdBlurVignetteCount;

	private bool m_ShouldIgnoreStandardBlurVignette;

	private FullScreenFXMgrCallbacks m_globalFXCallbacks;

	public FullScreenEffects ActiveCameraFullScreenEffects
	{
		get
		{
			Camera camera = CameraUtils.FindFullScreenEffectsCamera(activeOnly: false);
			if (camera == null)
			{
				Log.FullScreenFX.PrintError("FullScreenEffects could not be found. FullScreenEffects will fail.");
				return null;
			}
			return camera.GetComponent<FullScreenEffects>();
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		m_globalFXCallbacks = new GameObject("FullScreenFXMgrCallbacks", typeof(HSDontDestroyOnLoad)).AddComponent<FullScreenFXMgrCallbacks>();
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset += OnHSReset;
		}
		yield return new ServiceSoftDependency(typeof(SceneMgr), serviceLocator);
		if (serviceLocator.TryGetService<SceneMgr>(out var service))
		{
			service.RegisterScenePreLoadEvent(OnScenePreLoad);
		}
	}

	public Type[] GetDependencies()
	{
		return null;
	}

	public void Shutdown()
	{
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset -= OnHSReset;
		}
		if (HearthstoneServices.TryGet<SceneMgr>(out var service))
		{
			service.UnregisterScenePreLoadEvent(OnScenePreLoad);
		}
	}

	public static FullScreenFXMgr Get()
	{
		return HearthstoneServices.Get<FullScreenFXMgr>();
	}

	public void WillReset()
	{
		m_ActiveEffectsCount = 0;
		m_StdBlurVignetteCount = 0;
		FullScreenEffects activeCameraFullScreenEffects = ActiveCameraFullScreenEffects;
		if (!(activeCameraFullScreenEffects == null))
		{
			activeCameraFullScreenEffects.BlurEnabled = false;
			activeCameraFullScreenEffects.VignettingEnable = false;
			activeCameraFullScreenEffects.DesaturationEnabled = false;
			activeCameraFullScreenEffects.BlendToColorEnable = false;
		}
	}

	private void OnHSReset()
	{
		for (int num = m_layeredScreenEffectsByDepth.Count - 1; num >= 0; num--)
		{
			ScreenEffectsInstance screenEffectsInstance = m_layeredScreenEffectsByDepth[num];
			screenEffectsInstance.EffectsComponent.BlurEnabled = false;
			screenEffectsInstance.EffectsComponent.VignettingEnable = false;
			screenEffectsInstance.EffectsComponent.DesaturationEnabled = false;
			screenEffectsInstance.EffectsComponent.BlendToColorEnable = false;
			UnityEngine.Object.Destroy(screenEffectsInstance.EffectsComponent.gameObject);
			m_layeredScreenEffectsByDepth.RemoveAt(num);
		}
		WillReset();
	}

	public void ResetCount()
	{
		m_ActiveEffectsCount = 0;
	}

	private void OnScenePreLoad(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
	{
		if (prevMode == SceneMgr.Mode.GAMEPLAY && nextMode != SceneMgr.Mode.HUB)
		{
			StopAllEffects();
		}
	}

	public bool isFullScreenEffectActive()
	{
		FullScreenEffects activeCameraFullScreenEffects = ActiveCameraFullScreenEffects;
		if (activeCameraFullScreenEffects != null)
		{
			return activeCameraFullScreenEffects.IsActive;
		}
		return false;
	}

	public void StopAllEffects(float delay = 0f)
	{
		FullScreenEffects activeCameraFullScreenEffects = ActiveCameraFullScreenEffects;
		if (!(activeCameraFullScreenEffects == null) && activeCameraFullScreenEffects.IsActive)
		{
			Log.FullScreenFX.Print("StopAllEffects");
			Processor.RunCoroutine(StopAllEffectsCoroutine(activeCameraFullScreenEffects, delay));
		}
	}

	private IEnumerator StopAllEffectsCoroutine(FullScreenEffects effects, float delay)
	{
		float stopEffectsTime = 0.25f;
		yield return new WaitForSeconds(delay);
		Log.FullScreenFX.Print("StopAllEffectsCoroutine stopping effects now");
		if (!(effects == null))
		{
			if (effects.BlurEnabled)
			{
				StopBlur(stopEffectsTime, iTween.EaseType.linear, null, stopAll: true);
			}
			if (effects.VignettingEnable)
			{
				StopVignette(stopEffectsTime, iTween.EaseType.linear);
			}
			if (effects.BlendToColorEnable)
			{
				StopBlendToColor(stopEffectsTime, iTween.EaseType.linear);
			}
			if (effects.DesaturationEnabled)
			{
				StopDesaturate(stopEffectsTime, iTween.EaseType.linear);
			}
			m_StdBlurVignetteCount = 0;
			yield return new WaitForSeconds(stopEffectsTime);
			if (!(effects == null))
			{
				effects.Disable();
			}
		}
	}

	public void Vignette()
	{
		Vignette(0.4f, 0.4f, iTween.EaseType.easeOutCirc);
	}

	public void Vignette(float endVal, float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		if ((!(customTargetFX == null) || CanChangeMainBlurVignette()) && EffectsComponentsReady(customTargetFX, out var targetFX, out var fxCallbacks))
		{
			targetFX.VignettingEnable = true;
			fxCallbacks.VignetteComplete = listener;
			fxCallbacks.BeginEffect("vignette", "OnVignette", "OnVignetteComplete", 0f, endVal, time, easeType);
		}
	}

	public void StopVignette()
	{
		StopVignette(0.2f, iTween.EaseType.easeOutCirc);
	}

	public void StopVignette(float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		if ((!(customTargetFX == null) || CanChangeMainBlurVignette()) && EffectsComponentsReady(customTargetFX, out var targetFX, out var fxCallbacks))
		{
			targetFX.VignettingEnable = true;
			fxCallbacks.VignetteComplete = listener;
			fxCallbacks.BeginEffect("vignette", "OnVignette", "OnVignetteClear", targetFX.VignettingIntensity, 0f, time, easeType);
		}
	}

	public void Desaturate(float endVal, float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		if (EffectsComponentsReady(customTargetFX, out var targetFX, out var fxCallbacks))
		{
			targetFX.DesaturationEnabled = true;
			fxCallbacks.DesatComplete = listener;
			fxCallbacks.BeginEffect("desat", "OnDesat", "OnDesatComplete", targetFX.Desaturation, endVal, time, easeType);
		}
	}

	public void StopDesaturate(float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		if (EffectsComponentsReady(customTargetFX, out var targetFX, out var fxCallbacks))
		{
			targetFX.DesaturationEnabled = false;
			fxCallbacks.DesatComplete = listener;
			fxCallbacks.BeginEffect("desat", "OnDesat", "OnDesatClear", targetFX.Desaturation, 0f, time, easeType);
		}
	}

	public void ClearDesaturateListener()
	{
		m_globalFXCallbacks.DesatComplete = null;
	}

	public void SetBlurAmount(float val)
	{
		if (CanChangeMainBlurVignette())
		{
			FullScreenEffects activeCameraFullScreenEffects = ActiveCameraFullScreenEffects;
			if (!(activeCameraFullScreenEffects == null))
			{
				activeCameraFullScreenEffects.BlurAmount = val;
			}
		}
	}

	public void SetBlurBrightness(float val)
	{
		if (CanChangeMainBlurVignette())
		{
			FullScreenEffects activeCameraFullScreenEffects = ActiveCameraFullScreenEffects;
			if (!(activeCameraFullScreenEffects == null))
			{
				activeCameraFullScreenEffects.BlurBrightness = val;
			}
		}
	}

	public void SetBlurDesaturation(float val)
	{
		if (CanChangeMainBlurVignette())
		{
			FullScreenEffects activeCameraFullScreenEffects = ActiveCameraFullScreenEffects;
			if (!(activeCameraFullScreenEffects == null))
			{
				activeCameraFullScreenEffects.BlurDesaturation = val;
			}
		}
	}

	public void Blur(float blurVal, float time, iTween.EaseType easeType, Action listener = null)
	{
		m_ActiveEffectsCount++;
		Blur(blurVal, time, easeType, listener, null);
	}

	public void Blur(float blurVal, float time, iTween.EaseType easeType, Action listener, FullScreenEffects customTargetFX)
	{
		if ((!(customTargetFX == null) || CanChangeMainBlurVignette()) && EffectsComponentsReady(customTargetFX, out var targetFX, out var fxCallbacks))
		{
			targetFX.BlurEnabled = true;
			fxCallbacks.BlurComplete = listener;
			fxCallbacks.BeginEffect("blur", "OnBlur", "OnBlurComplete", targetFX.BlurBlend, blurVal, time, easeType);
		}
	}

	public void StopBlur()
	{
		StopBlur(0.2f, iTween.EaseType.easeOutCirc);
	}

	public void StopBlur(float time, iTween.EaseType easeType, Action listener = null, bool stopAll = false)
	{
		if (stopAll)
		{
			m_ActiveEffectsCount = 0;
		}
		if (m_ActiveEffectsCount > 0)
		{
			m_ActiveEffectsCount--;
		}
		if (m_ActiveEffectsCount <= 0)
		{
			StopBlur(time, easeType, listener, null);
		}
	}

	public void StopBlur(float time, iTween.EaseType easeType, Action listener, FullScreenEffects customTargetFX)
	{
		if ((!(customTargetFX == null) || CanChangeMainBlurVignette()) && EffectsComponentsReady(customTargetFX, out var targetFX, out var fxCallbacks))
		{
			targetFX.BlurEnabled = true;
			fxCallbacks.BlurComplete = listener;
			fxCallbacks.BeginEffect("blur", "OnBlur", "OnBlurClear", targetFX.BlurBlend, 0f, time, easeType);
		}
	}

	public void DisableBlur()
	{
		FullScreenEffects activeCameraFullScreenEffects = ActiveCameraFullScreenEffects;
		if (!(activeCameraFullScreenEffects == null))
		{
			activeCameraFullScreenEffects.BlurEnabled = false;
			activeCameraFullScreenEffects.BlurBlend = 0f;
			activeCameraFullScreenEffects.BlurAmount = 0f;
		}
	}

	public void BlendToColor(Color blendColor, float endVal, float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		if (EffectsComponentsReady(customTargetFX, out var targetFX, out var fxCallbacks))
		{
			targetFX.enabled = true;
			targetFX.BlendToColorEnable = true;
			targetFX.BlendColor = blendColor;
			fxCallbacks.BlendToColorComplete = listener;
			fxCallbacks.BeginEffect("blendtocolor", "OnBlendToColor", "OnBlendToColorComplete", 0f, endVal, time, easeType);
		}
	}

	public void StopBlendToColor(float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		if (EffectsComponentsReady(customTargetFX, out var targetFX, out var fxCallbacks))
		{
			targetFX.BlendToColorEnable = true;
			fxCallbacks.BlendToColorComplete = listener;
			fxCallbacks.BeginEffect("blendtocolor", "OnBlendToColor", "OnBlendToColorClear", targetFX.BlendToColorAmount, 0f, time, easeType);
		}
	}

	public void StartStandardBlurVignette(float time)
	{
		if (CanChangeMainBlurVignette() && m_StdBlurVignetteCount == 0 && !m_ShouldIgnoreStandardBlurVignette)
		{
			SetBlurBrightness(1f);
			SetBlurDesaturation(0f);
			Vignette(0.4f, time, iTween.EaseType.easeOutCirc);
			Blur(1f, time, iTween.EaseType.easeOutCirc);
		}
		m_StdBlurVignetteCount++;
	}

	public void EndStandardBlurVignette(float time, Action listener = null)
	{
		if (CanChangeMainBlurVignette() && m_StdBlurVignetteCount != 0)
		{
			m_StdBlurVignetteCount--;
			if (m_StdBlurVignetteCount == 0 && !m_ShouldIgnoreStandardBlurVignette)
			{
				StopBlur(time, iTween.EaseType.easeOutCirc);
				StopVignette(time, iTween.EaseType.easeOutCirc, listener);
			}
		}
	}

	public void AddStandardBlurVignette(UIContext.PopupRecord popupRecord, float time)
	{
		ScreenEffectsInstance screenEffectsInstance2 = (popupRecord.ScreenEffectsInstance = new ScreenEffectsInstance(popupRecord.PopupInstance, SetupScreenEffects(popupRecord)));
		time = (CanEaseLayeredFX() ? time : 0f);
		if (CanCompositeMultipleEffects() || m_layeredScreenEffectsByDepth.Count == 0)
		{
			screenEffectsInstance2.EffectsComponent.BlurBrightness = 1f;
			screenEffectsInstance2.EffectsComponent.BlurDesaturation = 0f;
			Blur(1f, time, iTween.EaseType.easeOutCirc, null, screenEffectsInstance2.EffectsComponent);
			Vignette(0.4f, time, iTween.EaseType.easeOutCirc, null, screenEffectsInstance2.EffectsComponent);
		}
		RegisterLayeredInstance(screenEffectsInstance2);
		UpdateInputManager();
	}

	public void RemoveStandardBlurVignette(UIContext.PopupRecord popupRecord, float time, Action listener = null)
	{
		if (m_layeredScreenEffectsByDepth.Count == 0)
		{
			return;
		}
		ScreenEffectsInstance fxInstance = RemoveLayeredInstance(popupRecord.PopupInstance);
		if (fxInstance != null)
		{
			time = (CanEaseLayeredFX() ? time : 0f);
			if (CanCompositeMultipleEffects())
			{
				StopBlur(time, iTween.EaseType.easeOutCirc, null, fxInstance.EffectsComponent);
				StopVignette(time, iTween.EaseType.easeOutCirc, delegate
				{
					listener?.Invoke();
					if (fxInstance.EffectsComponent != null)
					{
						UnityEngine.Object.Destroy(fxInstance.EffectsComponent.gameObject);
					}
				}, fxInstance.EffectsComponent);
			}
			else if (m_layeredScreenEffectsByDepth.Count == 0)
			{
				StopBlur(time, iTween.EaseType.easeOutCirc, null, fxInstance.EffectsComponent);
				StopVignette(time, iTween.EaseType.easeOutCirc, delegate
				{
					listener?.Invoke();
					UpdateMainEffectsDepth();
				}, fxInstance.EffectsComponent);
			}
			else
			{
				UpdateMainEffectsDepth();
			}
		}
		UpdateInputManager();
	}

	private bool CanCompositeMultipleEffects()
	{
		return !PlatformSettings.IsMobile();
	}

	private bool CanEaseLayeredFX()
	{
		if (!CanCompositeMultipleEffects())
		{
			if (ActiveCameraFullScreenEffects != null && (ActiveCameraFullScreenEffects.BlurEnabled || ActiveCameraFullScreenEffects.VignettingEnable))
			{
				return false;
			}
			if (m_layeredScreenEffectsByDepth.Count > 0)
			{
				return false;
			}
		}
		return true;
	}

	private bool CanChangeMainBlurVignette()
	{
		if (!CanCompositeMultipleEffects())
		{
			return m_layeredScreenEffectsByDepth.Count == 0;
		}
		return true;
	}

	private void UpdateMainEffectsDepth()
	{
		UIContext.PopupRecord popupRecord = UIContext.GetRoot().GetPopupsDescendingOrder().FirstOrDefault((UIContext.PopupRecord p) => p.BlurType == UIContext.BlurType.Standard);
		if (popupRecord != null)
		{
			ActiveCameraFullScreenEffects.Camera.depth = popupRecord.RenderCamera.depth - 0.001f;
		}
		else
		{
			ActiveCameraFullScreenEffects.Camera.depth = ((Camera.main != null) ? (Camera.main.depth + 0.001f) : (-0.999f));
		}
	}

	private void UpdateInputManager()
	{
		if (UniversalInputManager.Get() != null)
		{
			FullScreenEffects highestActiveEffect = GetHighestActiveEffect();
			UniversalInputManager.Get().SetCurrentFullScreenEffect(highestActiveEffect);
		}
	}

	private FullScreenEffects GetHighestActiveEffect()
	{
		if (m_layeredScreenEffectsByDepth.Count > 0)
		{
			for (int i = 0; i < m_layeredScreenEffectsByDepth.Count; i++)
			{
				ScreenEffectsInstance screenEffectsInstance = m_layeredScreenEffectsByDepth[i];
				if (!(screenEffectsInstance.EffectsComponent == null) && screenEffectsInstance.EffectsComponent.HasActiveEffects)
				{
					return screenEffectsInstance.EffectsComponent;
				}
			}
		}
		if (ActiveCameraFullScreenEffects != null && ActiveCameraFullScreenEffects.HasActiveEffects)
		{
			return ActiveCameraFullScreenEffects;
		}
		return null;
	}

	private FullScreenEffects SetupScreenEffects(UIContext.PopupRecord popupRecord)
	{
		float num = popupRecord.RenderCamera.depth - 0.001f;
		if (CanCompositeMultipleEffects())
		{
			Log.FullScreenFX.Print($"Creating FullScreenFX camera at depth {ActiveCameraFullScreenEffects.Camera.depth}");
			GameObject gameObject = new GameObject($"FullScreenFXCamera (owner: {popupRecord.PopupInstance.name}, depth: {num})");
			Camera camera = gameObject.AddComponent<Camera>();
			if (ActiveCameraFullScreenEffects != null)
			{
				camera.CopyFrom(ActiveCameraFullScreenEffects.Camera);
			}
			else if (Camera.main != null)
			{
				camera.CopyFrom(Camera.main);
			}
			camera.depth = num;
			camera.cullingMask = 0;
			camera.clearFlags = CameraClearFlags.Depth;
			return gameObject.AddComponent<FullScreenEffects>();
		}
		if (ActiveCameraFullScreenEffects != null)
		{
			Log.FullScreenFX.Print($"Placing FullScreenFX camera at depth {ActiveCameraFullScreenEffects.Camera.depth}");
			ActiveCameraFullScreenEffects.Camera.depth = num;
			return ActiveCameraFullScreenEffects;
		}
		Log.FullScreenFX.PrintError("SetupScreenEffectsInstance failed to find main FullscreenFX");
		return null;
	}

	private void RegisterLayeredInstance(ScreenEffectsInstance fxInstance)
	{
		m_layeredScreenEffectsByDepth.Add(fxInstance);
		CleanupUnusedInstances();
		m_layeredScreenEffectsByDepth.Sort();
	}

	private ScreenEffectsInstance RemoveLayeredInstance(UnityEngine.Object owner)
	{
		ScreenEffectsInstance result = null;
		for (int num = m_layeredScreenEffectsByDepth.Count - 1; num >= 0; num--)
		{
			ScreenEffectsInstance screenEffectsInstance = m_layeredScreenEffectsByDepth[num];
			if (screenEffectsInstance.Owner == owner)
			{
				m_layeredScreenEffectsByDepth.RemoveAt(num);
				result = screenEffectsInstance;
			}
		}
		CleanupUnusedInstances();
		return result;
	}

	private void CleanupUnusedInstances()
	{
		for (int num = m_layeredScreenEffectsByDepth.Count - 1; num >= 0; num--)
		{
			if (m_layeredScreenEffectsByDepth[num].Owner == null || m_layeredScreenEffectsByDepth[num].EffectsComponent == null)
			{
				ScreenEffectsInstance screenEffectsInstance = m_layeredScreenEffectsByDepth[num];
				m_layeredScreenEffectsByDepth.RemoveAt(num);
				if (screenEffectsInstance.EffectsComponent != null)
				{
					UnityEngine.Object.Destroy(screenEffectsInstance.EffectsComponent.gameObject);
				}
			}
		}
	}

	private bool EffectsComponentsReady(FullScreenEffects customTargetFX, out FullScreenEffects targetFX, out FullScreenFXMgrCallbacks fxCallbacks)
	{
		if (customTargetFX == null)
		{
			targetFX = ActiveCameraFullScreenEffects;
			fxCallbacks = m_globalFXCallbacks;
			if (targetFX == null)
			{
				return false;
			}
			fxCallbacks.SetEffectsComponent(targetFX);
		}
		else
		{
			targetFX = customTargetFX;
			fxCallbacks = targetFX.GetComponent<FullScreenFXMgrCallbacks>();
			if (fxCallbacks == null)
			{
				fxCallbacks = targetFX.gameObject.AddComponent<FullScreenFXMgrCallbacks>();
			}
			fxCallbacks.SetEffectsComponent(targetFX);
		}
		return true;
	}

	public void SetIgnoreStandardBlurVignette(bool shouldIgnore)
	{
		m_ShouldIgnoreStandardBlurVignette = shouldIgnore;
	}
}
