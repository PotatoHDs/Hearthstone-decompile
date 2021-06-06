using System;
using System.Collections;
using UnityEngine;

public class FullScreenFXMgrCallbacks : MonoBehaviour
{
	private FullScreenEffects m_screenEffect;

	public Action VignetteComplete;

	public Action BlurComplete;

	public Action DesatComplete;

	public Action BlendToColorComplete;

	public FullScreenEffects ScreenEffect
	{
		get
		{
			if (m_screenEffect == null && FullScreenFXMgr.Get() != null)
			{
				return FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
			}
			return m_screenEffect;
		}
	}

	public void SetEffectsComponent(FullScreenEffects screenEffect)
	{
		m_screenEffect = screenEffect;
	}

	public void BeginEffect(string name, string onUpdate, string onComplete, float start, float end, float time, iTween.EaseType easeType)
	{
		Log.FullScreenFX.Print("BeginEffect " + name + " " + start + " => " + end);
		Hashtable hashtable = new Hashtable();
		hashtable["name"] = name;
		hashtable["onupdate"] = onUpdate;
		hashtable["onupdatetarget"] = base.gameObject;
		hashtable["from"] = start;
		if (!string.IsNullOrEmpty(onComplete))
		{
			hashtable["oncomplete"] = onComplete;
			hashtable["oncompletetarget"] = base.gameObject;
		}
		hashtable["to"] = end;
		hashtable["time"] = time;
		hashtable["easetype"] = easeType;
		iTween.StopByName(base.gameObject, name);
		iTween.ValueTo(base.gameObject, hashtable);
	}

	public void OnVignette(float val)
	{
		if (!(ScreenEffect == null))
		{
			ScreenEffect.VignettingIntensity = val;
		}
	}

	public void OnVignetteComplete()
	{
		VignetteComplete?.Invoke();
	}

	public void OnVignetteClear()
	{
		if (!(ScreenEffect == null))
		{
			ScreenEffect.VignettingEnable = false;
			OnVignetteComplete();
		}
	}

	public void OnDesat(float val)
	{
		if (!(ScreenEffect == null))
		{
			ScreenEffect.Desaturation = val;
		}
	}

	public void OnDesatComplete()
	{
		DesatComplete?.Invoke();
	}

	public void OnDesatClear()
	{
		if (!(ScreenEffect == null))
		{
			ScreenEffect.DesaturationEnabled = false;
			OnDesatComplete();
		}
	}

	public void OnBlur(float val)
	{
		if (!(ScreenEffect == null))
		{
			ScreenEffect.BlurBlend = val;
		}
	}

	public void OnBlurComplete()
	{
		BlurComplete?.Invoke();
	}

	public void OnBlurClear()
	{
		if (!(ScreenEffect == null))
		{
			ScreenEffect.BlurEnabled = false;
			OnBlurComplete();
		}
	}

	public void OnBlendToColor(float val)
	{
		if (!(ScreenEffect == null))
		{
			ScreenEffect.BlendToColorAmount = val;
		}
	}

	public void OnBlendToColorComplete()
	{
		BlendToColorComplete?.Invoke();
	}

	public void OnBlendToColorClear()
	{
		if (!(ScreenEffect == null))
		{
			ScreenEffect.BlendToColorEnable = false;
			OnBlendToColorComplete();
		}
	}

	private void OnDestroy()
	{
		m_screenEffect = null;
	}
}
