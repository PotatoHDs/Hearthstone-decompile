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

// Token: 0x02000A31 RID: 2609
public class FullScreenFXMgr : IService
{
	// Token: 0x170007E9 RID: 2025
	// (get) Token: 0x06008C4B RID: 35915 RVA: 0x002CE750 File Offset: 0x002CC950
	public FullScreenEffects ActiveCameraFullScreenEffects
	{
		get
		{
			Camera camera = CameraUtils.FindFullScreenEffectsCamera(false);
			if (camera == null)
			{
				Log.FullScreenFX.PrintError("FullScreenEffects could not be found. FullScreenEffects will fail.", Array.Empty<object>());
				return null;
			}
			return camera.GetComponent<FullScreenEffects>();
		}
	}

	// Token: 0x06008C4C RID: 35916 RVA: 0x002CE789 File Offset: 0x002CC989
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		this.m_globalFXCallbacks = new GameObject("FullScreenFXMgrCallbacks", new Type[]
		{
			typeof(HSDontDestroyOnLoad)
		}).AddComponent<FullScreenFXMgrCallbacks>();
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset += this.OnHSReset;
		}
		yield return new ServiceSoftDependency(typeof(SceneMgr), serviceLocator);
		SceneMgr sceneMgr;
		if (serviceLocator.TryGetService<SceneMgr>(out sceneMgr))
		{
			sceneMgr.RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnScenePreLoad));
		}
		yield break;
	}

	// Token: 0x06008C4D RID: 35917 RVA: 0x00090064 File Offset: 0x0008E264
	public Type[] GetDependencies()
	{
		return null;
	}

	// Token: 0x06008C4E RID: 35918 RVA: 0x002CE7A0 File Offset: 0x002CC9A0
	public void Shutdown()
	{
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset -= this.OnHSReset;
		}
		SceneMgr sceneMgr;
		if (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			sceneMgr.UnregisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnScenePreLoad));
		}
	}

	// Token: 0x06008C4F RID: 35919 RVA: 0x002CE7EC File Offset: 0x002CC9EC
	public static FullScreenFXMgr Get()
	{
		return HearthstoneServices.Get<FullScreenFXMgr>();
	}

	// Token: 0x06008C50 RID: 35920 RVA: 0x002CE7F4 File Offset: 0x002CC9F4
	public void WillReset()
	{
		this.m_ActiveEffectsCount = 0;
		this.m_StdBlurVignetteCount = 0;
		FullScreenEffects activeCameraFullScreenEffects = this.ActiveCameraFullScreenEffects;
		if (activeCameraFullScreenEffects == null)
		{
			return;
		}
		activeCameraFullScreenEffects.BlurEnabled = false;
		activeCameraFullScreenEffects.VignettingEnable = false;
		activeCameraFullScreenEffects.DesaturationEnabled = false;
		activeCameraFullScreenEffects.BlendToColorEnable = false;
	}

	// Token: 0x06008C51 RID: 35921 RVA: 0x002CE83C File Offset: 0x002CCA3C
	private void OnHSReset()
	{
		for (int i = this.m_layeredScreenEffectsByDepth.Count - 1; i >= 0; i--)
		{
			FullScreenFXMgr.ScreenEffectsInstance screenEffectsInstance = this.m_layeredScreenEffectsByDepth[i];
			screenEffectsInstance.EffectsComponent.BlurEnabled = false;
			screenEffectsInstance.EffectsComponent.VignettingEnable = false;
			screenEffectsInstance.EffectsComponent.DesaturationEnabled = false;
			screenEffectsInstance.EffectsComponent.BlendToColorEnable = false;
			UnityEngine.Object.Destroy(screenEffectsInstance.EffectsComponent.gameObject);
			this.m_layeredScreenEffectsByDepth.RemoveAt(i);
		}
		this.WillReset();
	}

	// Token: 0x06008C52 RID: 35922 RVA: 0x002CE8BE File Offset: 0x002CCABE
	public void ResetCount()
	{
		this.m_ActiveEffectsCount = 0;
	}

	// Token: 0x06008C53 RID: 35923 RVA: 0x002CE8C7 File Offset: 0x002CCAC7
	private void OnScenePreLoad(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
	{
		if (prevMode == SceneMgr.Mode.GAMEPLAY && nextMode != SceneMgr.Mode.HUB)
		{
			this.StopAllEffects(0f);
		}
	}

	// Token: 0x06008C54 RID: 35924 RVA: 0x002CE8DC File Offset: 0x002CCADC
	public bool isFullScreenEffectActive()
	{
		FullScreenEffects activeCameraFullScreenEffects = this.ActiveCameraFullScreenEffects;
		return activeCameraFullScreenEffects != null && activeCameraFullScreenEffects.IsActive;
	}

	// Token: 0x06008C55 RID: 35925 RVA: 0x002CE904 File Offset: 0x002CCB04
	public void StopAllEffects(float delay = 0f)
	{
		FullScreenEffects activeCameraFullScreenEffects = this.ActiveCameraFullScreenEffects;
		if (activeCameraFullScreenEffects == null || !activeCameraFullScreenEffects.IsActive)
		{
			return;
		}
		Log.FullScreenFX.Print("StopAllEffects", Array.Empty<object>());
		Processor.RunCoroutine(this.StopAllEffectsCoroutine(activeCameraFullScreenEffects, delay), null);
	}

	// Token: 0x06008C56 RID: 35926 RVA: 0x002CE94D File Offset: 0x002CCB4D
	private IEnumerator StopAllEffectsCoroutine(FullScreenEffects effects, float delay)
	{
		float stopEffectsTime = 0.25f;
		yield return new WaitForSeconds(delay);
		Log.FullScreenFX.Print("StopAllEffectsCoroutine stopping effects now", Array.Empty<object>());
		if (effects == null)
		{
			yield break;
		}
		if (effects.BlurEnabled)
		{
			this.StopBlur(stopEffectsTime, iTween.EaseType.linear, null, true);
		}
		if (effects.VignettingEnable)
		{
			this.StopVignette(stopEffectsTime, iTween.EaseType.linear, null, null);
		}
		if (effects.BlendToColorEnable)
		{
			this.StopBlendToColor(stopEffectsTime, iTween.EaseType.linear, null, null);
		}
		if (effects.DesaturationEnabled)
		{
			this.StopDesaturate(stopEffectsTime, iTween.EaseType.linear, null, null);
		}
		this.m_StdBlurVignetteCount = 0;
		yield return new WaitForSeconds(stopEffectsTime);
		if (effects == null)
		{
			yield break;
		}
		effects.Disable();
		yield break;
	}

	// Token: 0x06008C57 RID: 35927 RVA: 0x002CE96A File Offset: 0x002CCB6A
	public void Vignette()
	{
		this.Vignette(0.4f, 0.4f, iTween.EaseType.easeOutCirc, null, null);
	}

	// Token: 0x06008C58 RID: 35928 RVA: 0x002CE980 File Offset: 0x002CCB80
	public void Vignette(float endVal, float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		if (customTargetFX == null && !this.CanChangeMainBlurVignette())
		{
			return;
		}
		FullScreenEffects fullScreenEffects;
		FullScreenFXMgrCallbacks fullScreenFXMgrCallbacks;
		if (!this.EffectsComponentsReady(customTargetFX, out fullScreenEffects, out fullScreenFXMgrCallbacks))
		{
			return;
		}
		fullScreenEffects.VignettingEnable = true;
		fullScreenFXMgrCallbacks.VignetteComplete = listener;
		fullScreenFXMgrCallbacks.BeginEffect("vignette", "OnVignette", "OnVignetteComplete", 0f, endVal, time, easeType);
	}

	// Token: 0x06008C59 RID: 35929 RVA: 0x002CE9DB File Offset: 0x002CCBDB
	public void StopVignette()
	{
		this.StopVignette(0.2f, iTween.EaseType.easeOutCirc, null, null);
	}

	// Token: 0x06008C5A RID: 35930 RVA: 0x002CE9EC File Offset: 0x002CCBEC
	public void StopVignette(float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		if (customTargetFX == null && !this.CanChangeMainBlurVignette())
		{
			return;
		}
		FullScreenEffects fullScreenEffects;
		FullScreenFXMgrCallbacks fullScreenFXMgrCallbacks;
		if (!this.EffectsComponentsReady(customTargetFX, out fullScreenEffects, out fullScreenFXMgrCallbacks))
		{
			return;
		}
		fullScreenEffects.VignettingEnable = true;
		fullScreenFXMgrCallbacks.VignetteComplete = listener;
		fullScreenFXMgrCallbacks.BeginEffect("vignette", "OnVignette", "OnVignetteClear", fullScreenEffects.VignettingIntensity, 0f, time, easeType);
	}

	// Token: 0x06008C5B RID: 35931 RVA: 0x002CEA4C File Offset: 0x002CCC4C
	public void Desaturate(float endVal, float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		FullScreenEffects fullScreenEffects;
		FullScreenFXMgrCallbacks fullScreenFXMgrCallbacks;
		if (!this.EffectsComponentsReady(customTargetFX, out fullScreenEffects, out fullScreenFXMgrCallbacks))
		{
			return;
		}
		fullScreenEffects.DesaturationEnabled = true;
		fullScreenFXMgrCallbacks.DesatComplete = listener;
		fullScreenFXMgrCallbacks.BeginEffect("desat", "OnDesat", "OnDesatComplete", fullScreenEffects.Desaturation, endVal, time, easeType);
	}

	// Token: 0x06008C5C RID: 35932 RVA: 0x002CEA98 File Offset: 0x002CCC98
	public void StopDesaturate(float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		FullScreenEffects fullScreenEffects;
		FullScreenFXMgrCallbacks fullScreenFXMgrCallbacks;
		if (!this.EffectsComponentsReady(customTargetFX, out fullScreenEffects, out fullScreenFXMgrCallbacks))
		{
			return;
		}
		fullScreenEffects.DesaturationEnabled = false;
		fullScreenFXMgrCallbacks.DesatComplete = listener;
		fullScreenFXMgrCallbacks.BeginEffect("desat", "OnDesat", "OnDesatClear", fullScreenEffects.Desaturation, 0f, time, easeType);
	}

	// Token: 0x06008C5D RID: 35933 RVA: 0x002CEAE4 File Offset: 0x002CCCE4
	public void ClearDesaturateListener()
	{
		this.m_globalFXCallbacks.DesatComplete = null;
	}

	// Token: 0x06008C5E RID: 35934 RVA: 0x002CEAF4 File Offset: 0x002CCCF4
	public void SetBlurAmount(float val)
	{
		if (!this.CanChangeMainBlurVignette())
		{
			return;
		}
		FullScreenEffects activeCameraFullScreenEffects = this.ActiveCameraFullScreenEffects;
		if (activeCameraFullScreenEffects == null)
		{
			return;
		}
		activeCameraFullScreenEffects.BlurAmount = val;
	}

	// Token: 0x06008C5F RID: 35935 RVA: 0x002CEB24 File Offset: 0x002CCD24
	public void SetBlurBrightness(float val)
	{
		if (!this.CanChangeMainBlurVignette())
		{
			return;
		}
		FullScreenEffects activeCameraFullScreenEffects = this.ActiveCameraFullScreenEffects;
		if (activeCameraFullScreenEffects == null)
		{
			return;
		}
		activeCameraFullScreenEffects.BlurBrightness = val;
	}

	// Token: 0x06008C60 RID: 35936 RVA: 0x002CEB54 File Offset: 0x002CCD54
	public void SetBlurDesaturation(float val)
	{
		if (!this.CanChangeMainBlurVignette())
		{
			return;
		}
		FullScreenEffects activeCameraFullScreenEffects = this.ActiveCameraFullScreenEffects;
		if (activeCameraFullScreenEffects == null)
		{
			return;
		}
		activeCameraFullScreenEffects.BlurDesaturation = val;
	}

	// Token: 0x06008C61 RID: 35937 RVA: 0x002CEB82 File Offset: 0x002CCD82
	public void Blur(float blurVal, float time, iTween.EaseType easeType, Action listener = null)
	{
		this.m_ActiveEffectsCount++;
		this.Blur(blurVal, time, easeType, listener, null);
	}

	// Token: 0x06008C62 RID: 35938 RVA: 0x002CEBA0 File Offset: 0x002CCDA0
	public void Blur(float blurVal, float time, iTween.EaseType easeType, Action listener, FullScreenEffects customTargetFX)
	{
		if (customTargetFX == null && !this.CanChangeMainBlurVignette())
		{
			return;
		}
		FullScreenEffects fullScreenEffects;
		FullScreenFXMgrCallbacks fullScreenFXMgrCallbacks;
		if (!this.EffectsComponentsReady(customTargetFX, out fullScreenEffects, out fullScreenFXMgrCallbacks))
		{
			return;
		}
		fullScreenEffects.BlurEnabled = true;
		fullScreenFXMgrCallbacks.BlurComplete = listener;
		fullScreenFXMgrCallbacks.BeginEffect("blur", "OnBlur", "OnBlurComplete", fullScreenEffects.BlurBlend, blurVal, time, easeType);
	}

	// Token: 0x06008C63 RID: 35939 RVA: 0x002CEBFC File Offset: 0x002CCDFC
	public void StopBlur()
	{
		this.StopBlur(0.2f, iTween.EaseType.easeOutCirc, null, false);
	}

	// Token: 0x06008C64 RID: 35940 RVA: 0x002CEC0D File Offset: 0x002CCE0D
	public void StopBlur(float time, iTween.EaseType easeType, Action listener = null, bool stopAll = false)
	{
		if (stopAll)
		{
			this.m_ActiveEffectsCount = 0;
		}
		if (this.m_ActiveEffectsCount > 0)
		{
			this.m_ActiveEffectsCount--;
		}
		if (this.m_ActiveEffectsCount > 0)
		{
			return;
		}
		this.StopBlur(time, easeType, listener, null);
	}

	// Token: 0x06008C65 RID: 35941 RVA: 0x002CEC48 File Offset: 0x002CCE48
	public void StopBlur(float time, iTween.EaseType easeType, Action listener, FullScreenEffects customTargetFX)
	{
		if (customTargetFX == null && !this.CanChangeMainBlurVignette())
		{
			return;
		}
		FullScreenEffects fullScreenEffects;
		FullScreenFXMgrCallbacks fullScreenFXMgrCallbacks;
		if (!this.EffectsComponentsReady(customTargetFX, out fullScreenEffects, out fullScreenFXMgrCallbacks))
		{
			return;
		}
		fullScreenEffects.BlurEnabled = true;
		fullScreenFXMgrCallbacks.BlurComplete = listener;
		fullScreenFXMgrCallbacks.BeginEffect("blur", "OnBlur", "OnBlurClear", fullScreenEffects.BlurBlend, 0f, time, easeType);
	}

	// Token: 0x06008C66 RID: 35942 RVA: 0x002CECA8 File Offset: 0x002CCEA8
	public void DisableBlur()
	{
		FullScreenEffects activeCameraFullScreenEffects = this.ActiveCameraFullScreenEffects;
		if (activeCameraFullScreenEffects == null)
		{
			return;
		}
		activeCameraFullScreenEffects.BlurEnabled = false;
		activeCameraFullScreenEffects.BlurBlend = 0f;
		activeCameraFullScreenEffects.BlurAmount = 0f;
	}

	// Token: 0x06008C67 RID: 35943 RVA: 0x002CECE4 File Offset: 0x002CCEE4
	public void BlendToColor(Color blendColor, float endVal, float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		FullScreenEffects fullScreenEffects;
		FullScreenFXMgrCallbacks fullScreenFXMgrCallbacks;
		if (!this.EffectsComponentsReady(customTargetFX, out fullScreenEffects, out fullScreenFXMgrCallbacks))
		{
			return;
		}
		fullScreenEffects.enabled = true;
		fullScreenEffects.BlendToColorEnable = true;
		fullScreenEffects.BlendColor = blendColor;
		fullScreenFXMgrCallbacks.BlendToColorComplete = listener;
		fullScreenFXMgrCallbacks.BeginEffect("blendtocolor", "OnBlendToColor", "OnBlendToColorComplete", 0f, endVal, time, easeType);
	}

	// Token: 0x06008C68 RID: 35944 RVA: 0x002CED3C File Offset: 0x002CCF3C
	public void StopBlendToColor(float time, iTween.EaseType easeType, Action listener = null, FullScreenEffects customTargetFX = null)
	{
		FullScreenEffects fullScreenEffects;
		FullScreenFXMgrCallbacks fullScreenFXMgrCallbacks;
		if (!this.EffectsComponentsReady(customTargetFX, out fullScreenEffects, out fullScreenFXMgrCallbacks))
		{
			return;
		}
		fullScreenEffects.BlendToColorEnable = true;
		fullScreenFXMgrCallbacks.BlendToColorComplete = listener;
		fullScreenFXMgrCallbacks.BeginEffect("blendtocolor", "OnBlendToColor", "OnBlendToColorClear", fullScreenEffects.BlendToColorAmount, 0f, time, easeType);
	}

	// Token: 0x06008C69 RID: 35945 RVA: 0x002CED88 File Offset: 0x002CCF88
	public void StartStandardBlurVignette(float time)
	{
		if (this.CanChangeMainBlurVignette() && this.m_StdBlurVignetteCount == 0 && !this.m_ShouldIgnoreStandardBlurVignette)
		{
			this.SetBlurBrightness(1f);
			this.SetBlurDesaturation(0f);
			this.Vignette(0.4f, time, iTween.EaseType.easeOutCirc, null, null);
			this.Blur(1f, time, iTween.EaseType.easeOutCirc, null);
		}
		this.m_StdBlurVignetteCount++;
	}

	// Token: 0x06008C6A RID: 35946 RVA: 0x002CEDF0 File Offset: 0x002CCFF0
	public void EndStandardBlurVignette(float time, Action listener = null)
	{
		if (!this.CanChangeMainBlurVignette() || this.m_StdBlurVignetteCount == 0)
		{
			return;
		}
		this.m_StdBlurVignetteCount--;
		if (this.m_StdBlurVignetteCount == 0 && !this.m_ShouldIgnoreStandardBlurVignette)
		{
			this.StopBlur(time, iTween.EaseType.easeOutCirc, null, false);
			this.StopVignette(time, iTween.EaseType.easeOutCirc, listener, null);
		}
	}

	// Token: 0x06008C6B RID: 35947 RVA: 0x002CEE44 File Offset: 0x002CD044
	public void AddStandardBlurVignette(UIContext.PopupRecord popupRecord, float time)
	{
		FullScreenFXMgr.ScreenEffectsInstance screenEffectsInstance = new FullScreenFXMgr.ScreenEffectsInstance(popupRecord.PopupInstance, this.SetupScreenEffects(popupRecord));
		popupRecord.ScreenEffectsInstance = screenEffectsInstance;
		time = (this.CanEaseLayeredFX() ? time : 0f);
		if (this.CanCompositeMultipleEffects() || this.m_layeredScreenEffectsByDepth.Count == 0)
		{
			screenEffectsInstance.EffectsComponent.BlurBrightness = 1f;
			screenEffectsInstance.EffectsComponent.BlurDesaturation = 0f;
			this.Blur(1f, time, iTween.EaseType.easeOutCirc, null, screenEffectsInstance.EffectsComponent);
			this.Vignette(0.4f, time, iTween.EaseType.easeOutCirc, null, screenEffectsInstance.EffectsComponent);
		}
		this.RegisterLayeredInstance(screenEffectsInstance);
		this.UpdateInputManager();
	}

	// Token: 0x06008C6C RID: 35948 RVA: 0x002CEEEC File Offset: 0x002CD0EC
	public void RemoveStandardBlurVignette(UIContext.PopupRecord popupRecord, float time, Action listener = null)
	{
		if (this.m_layeredScreenEffectsByDepth.Count == 0)
		{
			return;
		}
		FullScreenFXMgr.ScreenEffectsInstance fxInstance = this.RemoveLayeredInstance(popupRecord.PopupInstance);
		if (fxInstance != null)
		{
			time = (this.CanEaseLayeredFX() ? time : 0f);
			if (this.CanCompositeMultipleEffects())
			{
				this.StopBlur(time, iTween.EaseType.easeOutCirc, null, fxInstance.EffectsComponent);
				this.StopVignette(time, iTween.EaseType.easeOutCirc, delegate()
				{
					Action listener2 = listener;
					if (listener2 != null)
					{
						listener2();
					}
					if (fxInstance.EffectsComponent != null)
					{
						UnityEngine.Object.Destroy(fxInstance.EffectsComponent.gameObject);
					}
				}, fxInstance.EffectsComponent);
			}
			else if (this.m_layeredScreenEffectsByDepth.Count == 0)
			{
				this.StopBlur(time, iTween.EaseType.easeOutCirc, null, fxInstance.EffectsComponent);
				this.StopVignette(time, iTween.EaseType.easeOutCirc, delegate()
				{
					Action listener2 = listener;
					if (listener2 != null)
					{
						listener2();
					}
					this.UpdateMainEffectsDepth();
				}, fxInstance.EffectsComponent);
			}
			else
			{
				this.UpdateMainEffectsDepth();
			}
		}
		this.UpdateInputManager();
	}

	// Token: 0x06008C6D RID: 35949 RVA: 0x002CEFD9 File Offset: 0x002CD1D9
	private bool CanCompositeMultipleEffects()
	{
		return !PlatformSettings.IsMobile();
	}

	// Token: 0x06008C6E RID: 35950 RVA: 0x002CEFE4 File Offset: 0x002CD1E4
	private bool CanEaseLayeredFX()
	{
		if (!this.CanCompositeMultipleEffects())
		{
			if (this.ActiveCameraFullScreenEffects != null && (this.ActiveCameraFullScreenEffects.BlurEnabled || this.ActiveCameraFullScreenEffects.VignettingEnable))
			{
				return false;
			}
			if (this.m_layeredScreenEffectsByDepth.Count > 0)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06008C6F RID: 35951 RVA: 0x002CF034 File Offset: 0x002CD234
	private bool CanChangeMainBlurVignette()
	{
		return this.CanCompositeMultipleEffects() || this.m_layeredScreenEffectsByDepth.Count == 0;
	}

	// Token: 0x06008C70 RID: 35952 RVA: 0x002CF050 File Offset: 0x002CD250
	private void UpdateMainEffectsDepth()
	{
		UIContext.PopupRecord popupRecord = UIContext.GetRoot().GetPopupsDescendingOrder().FirstOrDefault((UIContext.PopupRecord p) => p.BlurType == UIContext.BlurType.Standard);
		if (popupRecord != null)
		{
			this.ActiveCameraFullScreenEffects.Camera.depth = popupRecord.RenderCamera.depth - 0.001f;
			return;
		}
		this.ActiveCameraFullScreenEffects.Camera.depth = ((Camera.main != null) ? (Camera.main.depth + 0.001f) : -0.999f);
	}

	// Token: 0x06008C71 RID: 35953 RVA: 0x002CF0E8 File Offset: 0x002CD2E8
	private void UpdateInputManager()
	{
		if (UniversalInputManager.Get() == null)
		{
			return;
		}
		FullScreenEffects highestActiveEffect = this.GetHighestActiveEffect();
		UniversalInputManager.Get().SetCurrentFullScreenEffect(highestActiveEffect);
	}

	// Token: 0x06008C72 RID: 35954 RVA: 0x002CF110 File Offset: 0x002CD310
	private FullScreenEffects GetHighestActiveEffect()
	{
		if (this.m_layeredScreenEffectsByDepth.Count > 0)
		{
			for (int i = 0; i < this.m_layeredScreenEffectsByDepth.Count; i++)
			{
				FullScreenFXMgr.ScreenEffectsInstance screenEffectsInstance = this.m_layeredScreenEffectsByDepth[i];
				if (!(screenEffectsInstance.EffectsComponent == null) && screenEffectsInstance.EffectsComponent.HasActiveEffects)
				{
					return screenEffectsInstance.EffectsComponent;
				}
			}
		}
		if (this.ActiveCameraFullScreenEffects != null && this.ActiveCameraFullScreenEffects.HasActiveEffects)
		{
			return this.ActiveCameraFullScreenEffects;
		}
		return null;
	}

	// Token: 0x06008C73 RID: 35955 RVA: 0x002CF194 File Offset: 0x002CD394
	private FullScreenEffects SetupScreenEffects(UIContext.PopupRecord popupRecord)
	{
		float num = popupRecord.RenderCamera.depth - 0.001f;
		if (this.CanCompositeMultipleEffects())
		{
			Log.FullScreenFX.Print(string.Format("Creating FullScreenFX camera at depth {0}", this.ActiveCameraFullScreenEffects.Camera.depth), Array.Empty<object>());
			GameObject gameObject = new GameObject(string.Format("FullScreenFXCamera (owner: {0}, depth: {1})", popupRecord.PopupInstance.name, num));
			Camera camera = gameObject.AddComponent<Camera>();
			if (this.ActiveCameraFullScreenEffects != null)
			{
				camera.CopyFrom(this.ActiveCameraFullScreenEffects.Camera);
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
		if (this.ActiveCameraFullScreenEffects != null)
		{
			Log.FullScreenFX.Print(string.Format("Placing FullScreenFX camera at depth {0}", this.ActiveCameraFullScreenEffects.Camera.depth), Array.Empty<object>());
			this.ActiveCameraFullScreenEffects.Camera.depth = num;
			return this.ActiveCameraFullScreenEffects;
		}
		Log.FullScreenFX.PrintError("SetupScreenEffectsInstance failed to find main FullscreenFX", Array.Empty<object>());
		return null;
	}

	// Token: 0x06008C74 RID: 35956 RVA: 0x002CF2D0 File Offset: 0x002CD4D0
	private void RegisterLayeredInstance(FullScreenFXMgr.ScreenEffectsInstance fxInstance)
	{
		this.m_layeredScreenEffectsByDepth.Add(fxInstance);
		this.CleanupUnusedInstances();
		this.m_layeredScreenEffectsByDepth.Sort();
	}

	// Token: 0x06008C75 RID: 35957 RVA: 0x002CF2F0 File Offset: 0x002CD4F0
	private FullScreenFXMgr.ScreenEffectsInstance RemoveLayeredInstance(UnityEngine.Object owner)
	{
		FullScreenFXMgr.ScreenEffectsInstance result = null;
		for (int i = this.m_layeredScreenEffectsByDepth.Count - 1; i >= 0; i--)
		{
			FullScreenFXMgr.ScreenEffectsInstance screenEffectsInstance = this.m_layeredScreenEffectsByDepth[i];
			if (screenEffectsInstance.Owner == owner)
			{
				this.m_layeredScreenEffectsByDepth.RemoveAt(i);
				result = screenEffectsInstance;
			}
		}
		this.CleanupUnusedInstances();
		return result;
	}

	// Token: 0x06008C76 RID: 35958 RVA: 0x002CF348 File Offset: 0x002CD548
	private void CleanupUnusedInstances()
	{
		for (int i = this.m_layeredScreenEffectsByDepth.Count - 1; i >= 0; i--)
		{
			if (this.m_layeredScreenEffectsByDepth[i].Owner == null || this.m_layeredScreenEffectsByDepth[i].EffectsComponent == null)
			{
				FullScreenFXMgr.ScreenEffectsInstance screenEffectsInstance = this.m_layeredScreenEffectsByDepth[i];
				this.m_layeredScreenEffectsByDepth.RemoveAt(i);
				if (screenEffectsInstance.EffectsComponent != null)
				{
					UnityEngine.Object.Destroy(screenEffectsInstance.EffectsComponent.gameObject);
				}
			}
		}
	}

	// Token: 0x06008C77 RID: 35959 RVA: 0x002CF3D8 File Offset: 0x002CD5D8
	private bool EffectsComponentsReady(FullScreenEffects customTargetFX, out FullScreenEffects targetFX, out FullScreenFXMgrCallbacks fxCallbacks)
	{
		if (customTargetFX == null)
		{
			targetFX = this.ActiveCameraFullScreenEffects;
			fxCallbacks = this.m_globalFXCallbacks;
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

	// Token: 0x06008C78 RID: 35960 RVA: 0x002CF443 File Offset: 0x002CD643
	public void SetIgnoreStandardBlurVignette(bool shouldIgnore)
	{
		this.m_ShouldIgnoreStandardBlurVignette = shouldIgnore;
	}

	// Token: 0x04007536 RID: 30006
	private List<FullScreenFXMgr.ScreenEffectsInstance> m_layeredScreenEffectsByDepth = new List<FullScreenFXMgr.ScreenEffectsInstance>();

	// Token: 0x04007537 RID: 30007
	private int m_ActiveEffectsCount;

	// Token: 0x04007538 RID: 30008
	private int m_StdBlurVignetteCount;

	// Token: 0x04007539 RID: 30009
	private bool m_ShouldIgnoreStandardBlurVignette;

	// Token: 0x0400753A RID: 30010
	private FullScreenFXMgrCallbacks m_globalFXCallbacks;

	// Token: 0x0200269D RID: 9885
	public class ScreenEffectsInstance : IComparable
	{
		// Token: 0x17002C87 RID: 11399
		// (get) Token: 0x060137D4 RID: 79828 RVA: 0x00535B09 File Offset: 0x00533D09
		public Camera Camera
		{
			get
			{
				if (!(this.EffectsComponent != null))
				{
					return null;
				}
				return this.EffectsComponent.Camera;
			}
		}

		// Token: 0x060137D5 RID: 79829 RVA: 0x00535B26 File Offset: 0x00533D26
		public ScreenEffectsInstance(UnityEngine.Object owner, FullScreenEffects screenEffects)
		{
			this.Owner = owner;
			this.EffectsComponent = screenEffects;
		}

		// Token: 0x060137D6 RID: 79830 RVA: 0x00535B3C File Offset: 0x00533D3C
		public int CompareTo(object obj)
		{
			FullScreenFXMgr.ScreenEffectsInstance screenEffectsInstance = obj as FullScreenFXMgr.ScreenEffectsInstance;
			if (screenEffectsInstance == null)
			{
				return -1;
			}
			if (this.Camera == null && screenEffectsInstance.Camera == null)
			{
				return 0;
			}
			if (this.Camera != null && screenEffectsInstance.Camera == null)
			{
				return -1;
			}
			if (this.Camera == null && screenEffectsInstance.Camera != null)
			{
				return 1;
			}
			if (this.Camera.depth > screenEffectsInstance.Camera.depth)
			{
				return -1;
			}
			if (this.Camera.depth < screenEffectsInstance.Camera.depth)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0400F141 RID: 61761
		public readonly UnityEngine.Object Owner;

		// Token: 0x0400F142 RID: 61762
		public readonly FullScreenEffects EffectsComponent;
	}
}
