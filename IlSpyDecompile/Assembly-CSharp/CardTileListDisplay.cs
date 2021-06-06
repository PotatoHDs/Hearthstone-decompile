using System.Collections;
using UnityEngine;

public class CardTileListDisplay : MonoBehaviour
{
	public SoundDucker m_SoundDucker;

	protected bool m_animatingVignette;

	protected bool m_animatingDesat;

	protected bool m_animatingBlur;

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
	{
	}

	protected virtual void OnDestroy()
	{
	}

	protected void AnimateVignetteIn()
	{
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		m_animatingVignette = activeCameraFullScreenEffects.VignettingEnable;
		if (m_animatingVignette)
		{
			Hashtable args = iTween.Hash("from", activeCameraFullScreenEffects.VignettingIntensity, "to", 0.6f, "time", 0.4f, "easetype", iTween.EaseType.easeInOutQuad, "onupdate", "OnUpdateVignetteVal", "onupdatetarget", base.gameObject, "name", "historyVig", "oncomplete", "OnVignetteInFinished", "oncompletetarget", base.gameObject);
			iTween.StopByName(Camera.main.gameObject, "historyVig");
			iTween.ValueTo(Camera.main.gameObject, args);
		}
		m_animatingDesat = activeCameraFullScreenEffects.DesaturationEnabled;
		if (m_animatingDesat)
		{
			Hashtable args2 = iTween.Hash("from", activeCameraFullScreenEffects.Desaturation, "to", 1f, "time", 0.4f, "easetype", iTween.EaseType.easeInOutQuad, "onupdate", "OnUpdateDesatVal", "onupdatetarget", base.gameObject, "name", "historyDesat", "oncomplete", "OnDesatInFinished", "oncompletetarget", base.gameObject);
			iTween.StopByName(Camera.main.gameObject, "historyDesat");
			iTween.ValueTo(Camera.main.gameObject, args2);
		}
	}

	protected void AnimateVignetteOut()
	{
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		m_animatingVignette = activeCameraFullScreenEffects.VignettingEnable;
		if (m_animatingVignette)
		{
			Hashtable args = iTween.Hash("from", activeCameraFullScreenEffects.VignettingIntensity, "to", 0f, "time", 0.4f, "easetype", iTween.EaseType.easeInOutQuad, "onupdate", "OnUpdateVignetteVal", "onupdatetarget", base.gameObject, "name", "historyVig", "oncomplete", "OnVignetteOutFinished", "oncompletetarget", base.gameObject);
			iTween.StopByName(Camera.main.gameObject, "historyVig");
			iTween.ValueTo(Camera.main.gameObject, args);
		}
		m_animatingDesat = activeCameraFullScreenEffects.DesaturationEnabled;
		if (m_animatingDesat)
		{
			Hashtable args2 = iTween.Hash("from", activeCameraFullScreenEffects.Desaturation, "to", 0f, "time", 0.4f, "easetype", iTween.EaseType.easeInOutQuad, "onupdate", "OnUpdateDesatVal", "onupdatetarget", base.gameObject, "name", "historyDesat", "oncomplete", "OnDesatOutFinished", "oncompletetarget", base.gameObject);
			iTween.StopByName(Camera.main.gameObject, "historyDesat");
			iTween.ValueTo(Camera.main.gameObject, args2);
		}
	}

	protected void AnimateBlurVignetteIn()
	{
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		m_animatingBlur = activeCameraFullScreenEffects.BlurEnabled;
		if (m_animatingBlur)
		{
			Hashtable args = iTween.Hash("from", activeCameraFullScreenEffects.BlurAmount, "to", 0.6f, "time", 0.4f, "easetype", iTween.EaseType.easeInOutQuad, "onupdate", "OnUpdateBlurVal", "onupdatetarget", base.gameObject, "name", "historyBlur", "oncomplete", "OnBlurInFinished", "oncompletetarget", base.gameObject);
			iTween.StopByName(Camera.main.gameObject, "historyBlur");
			iTween.ValueTo(Camera.main.gameObject, args);
		}
		m_animatingVignette = activeCameraFullScreenEffects.VignettingEnable;
		if (m_animatingVignette)
		{
			Hashtable args2 = iTween.Hash("from", activeCameraFullScreenEffects.VignettingIntensity, "to", 0.6f, "time", 0.4f, "easetype", iTween.EaseType.easeInOutQuad, "onupdate", "OnUpdateVignetteVal", "onupdatetarget", base.gameObject, "name", "historyVig", "oncomplete", "OnVignetteInFinished", "oncompletetarget", base.gameObject);
			iTween.StopByName(Camera.main.gameObject, "historyVig");
			iTween.ValueTo(Camera.main.gameObject, args2);
		}
		m_animatingDesat = activeCameraFullScreenEffects.BlurEnabled;
		if (m_animatingDesat)
		{
			Hashtable args3 = iTween.Hash("from", activeCameraFullScreenEffects.BlurDesaturation, "to", 1f, "time", 0.4f, "easetype", iTween.EaseType.easeInOutQuad, "onupdate", "OnUpdateBlurDesatVal", "onupdatetarget", base.gameObject, "name", "historyBlurDesat", "oncomplete", "OnBlurDesatInFinished", "oncompletetarget", base.gameObject);
			iTween.StopByName(Camera.main.gameObject, "historyBlurDesat");
			iTween.ValueTo(Camera.main.gameObject, args3);
		}
	}

	protected void AnimateBlurVignetteOut()
	{
		FullScreenEffects activeCameraFullScreenEffects = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
		m_animatingBlur = activeCameraFullScreenEffects.BlurEnabled;
		if (m_animatingBlur)
		{
			Hashtable args = iTween.Hash("from", activeCameraFullScreenEffects.BlurAmount, "to", 0f, "time", 0.1f, "easetype", iTween.EaseType.easeInOutQuad, "onupdate", "OnUpdateBlurVal", "onupdatetarget", base.gameObject, "name", "historyBlur", "oncomplete", "OnBlurOutFinished", "oncompletetarget", base.gameObject);
			iTween.StopByName(Camera.main.gameObject, "historyBlur");
			iTween.ValueTo(Camera.main.gameObject, args);
		}
		m_animatingVignette = activeCameraFullScreenEffects.VignettingEnable;
		if (m_animatingVignette)
		{
			Hashtable args2 = iTween.Hash("from", activeCameraFullScreenEffects.VignettingIntensity, "to", 0f, "time", 0.1f, "easetype", iTween.EaseType.easeInOutQuad, "onupdate", "OnUpdateVignetteVal", "onupdatetarget", base.gameObject, "name", "historyVig", "oncomplete", "OnVignetteOutFinished", "oncompletetarget", base.gameObject);
			iTween.StopByName(Camera.main.gameObject, "historyVig");
			iTween.ValueTo(Camera.main.gameObject, args2);
		}
		m_animatingDesat = activeCameraFullScreenEffects.BlurEnabled;
		if (m_animatingDesat)
		{
			Hashtable args3 = iTween.Hash("from", activeCameraFullScreenEffects.BlurDesaturation, "to", 0f, "time", 0.1f, "easetype", iTween.EaseType.easeInOutQuad, "onupdate", "OnUpdateBlurDesatVal", "onupdatetarget", base.gameObject, "name", "historyBlurDesat", "oncomplete", "OnBlurDesatOutFinished", "oncompletetarget", base.gameObject);
			iTween.StopByName(Camera.main.gameObject, "historyBlurDesat");
			iTween.ValueTo(Camera.main.gameObject, args3);
		}
	}

	private void OnUpdateVignetteVal(float val)
	{
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.VignettingIntensity = val;
	}

	private void OnUpdateDesatVal(float val)
	{
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.Desaturation = val;
	}

	private void OnUpdateBlurDesatVal(float val)
	{
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.BlurDesaturation = val;
	}

	private void OnVignetteInFinished()
	{
		m_animatingVignette = false;
	}

	private void OnDesatInFinished()
	{
		m_animatingDesat = false;
	}

	private void OnBlurDesatInFinished()
	{
		m_animatingDesat = false;
	}

	private void OnVignetteOutFinished()
	{
		m_animatingVignette = false;
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.VignettingEnable = false;
		OnFullScreenEffectOutFinished();
	}

	private void OnDesatOutFinished()
	{
		m_animatingDesat = false;
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.DesaturationEnabled = false;
		OnFullScreenEffectOutFinished();
	}

	private void OnBlurDesatOutFinished()
	{
		m_animatingDesat = false;
		OnFullScreenEffectOutFinished();
	}

	private void OnUpdateBlurVal(float val)
	{
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.BlurAmount = val;
	}

	private void OnBlurInFinished()
	{
		m_animatingBlur = false;
	}

	private void OnBlurOutFinished()
	{
		m_animatingBlur = false;
		FullScreenFXMgr.Get().ActiveCameraFullScreenEffects.BlurEnabled = false;
		OnFullScreenEffectOutFinished();
	}

	protected virtual void OnFullScreenEffectOutFinished()
	{
	}
}
