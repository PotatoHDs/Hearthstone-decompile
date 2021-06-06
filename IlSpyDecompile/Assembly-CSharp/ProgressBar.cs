using System.Collections;
using Hearthstone.UI;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
	public TextMesh m_label;

	public UberText m_uberLabel;

	public float m_increaseAnimTime = 2f;

	public float m_decreaseAnimTime = 1f;

	public float m_coolDownAnimTime = 1f;

	public float m_barIntensity = 1.2f;

	public float m_barIntensityIncreaseMax = 3f;

	public float m_audioFadeInOut = 0.2f;

	public float m_increasePitchStart = 1f;

	public float m_increasePitchEnd = 1.2f;

	public float m_decreasePitchStart = 1f;

	public float m_decreasePitchEnd = 0.8f;

	private Material m_barMaterial;

	private float m_prevVal;

	private float m_currVal;

	private float m_factor;

	private float m_maxIntensity;

	private float m_Uadd;

	private float m_animationTime;

	private float m_progress;

	[Overridable]
	public float Progress
	{
		get
		{
			return m_progress;
		}
		set
		{
			SetProgressBar(value);
		}
	}

	public void Awake()
	{
		m_barMaterial = GetComponent<Renderer>().GetMaterial();
	}

	public void OnDestroy()
	{
		Object.Destroy(m_barMaterial);
	}

	public void SetMaterial(Material material)
	{
		m_barMaterial = material;
	}

	public void AnimateProgress(float prevVal, float currVal, iTween.EaseType easeType = iTween.EaseType.easeOutQuad)
	{
		m_prevVal = prevVal;
		m_currVal = currVal;
		if (m_currVal > m_prevVal)
		{
			m_factor = m_currVal - m_prevVal;
		}
		else
		{
			m_factor = m_prevVal - m_currVal;
		}
		m_factor = Mathf.Abs(m_factor);
		if (m_currVal > m_prevVal)
		{
			IncreaseProgress(m_currVal, m_prevVal, easeType);
		}
		else
		{
			DecreaseProgress(m_currVal, m_prevVal);
		}
	}

	public void SetProgressBar(float progress)
	{
		m_progress = progress;
		if (m_barMaterial == null)
		{
			m_barMaterial = GetComponent<Renderer>().GetMaterial();
		}
		m_barMaterial.SetFloat("_Intensity", m_barIntensity);
		m_barMaterial.SetFloat("_Percent", progress);
	}

	public float GetAnimationTime()
	{
		return m_animationTime;
	}

	public void SetLabel(string text)
	{
		if (m_uberLabel != null)
		{
			m_uberLabel.Text = text;
		}
		if (m_label != null)
		{
			m_label.text = text;
		}
	}

	public void SetBarTexture(Texture texture)
	{
		if (m_barMaterial == null)
		{
			m_barMaterial = GetComponent<Renderer>().GetMaterial();
		}
		m_barMaterial.SetTexture("_NoiseTex", texture);
	}

	private void IncreaseProgress(float currProgress, float prevProgress, iTween.EaseType easeType)
	{
		float num = (m_animationTime = m_increaseAnimTime * m_factor);
		Hashtable args = iTween.Hash("from", prevProgress, "to", currProgress, "time", num, "easetype", easeType, "onupdate", "Progress_OnUpdate", "onupdatetarget", base.gameObject, "name", "IncreaseProgress");
		iTween.StopByName(base.gameObject, "IncreaseProgress");
		iTween.ValueTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash("from", 0f, "to", 0.005f, "time", num, "easetype", iTween.EaseType.easeOutQuad, "onupdate", "ScrollSpeed_OnUpdate", "onupdatetarget", base.gameObject, "name", "UVSpeed");
		iTween.StopByName(base.gameObject, "UVSpeed");
		iTween.ValueTo(base.gameObject, args2);
		m_maxIntensity = m_barIntensity + (m_barIntensityIncreaseMax - m_barIntensity) * m_factor;
		Hashtable args3 = iTween.Hash("from", m_barIntensity, "to", m_maxIntensity, "time", num, "easetype", easeType, "onupdate", "Intensity_OnUpdate", "onupdatetarget", base.gameObject, "name", "Intensity", "oncomplete", "Intensity_OnComplete", "oncompletetarget", base.gameObject);
		iTween.StopByName(base.gameObject, "Intensity");
		iTween.ValueTo(base.gameObject, args3);
		if ((bool)GetComponent<AudioSource>())
		{
			SoundManager.Get().SetVolume(GetComponent<AudioSource>(), 0f);
			SoundManager.Get().SetPitch(GetComponent<AudioSource>(), m_increasePitchStart);
			SoundManager.Get().Play(GetComponent<AudioSource>());
		}
		Hashtable args4 = iTween.Hash("from", 0, "to", 1, "time", num * m_audioFadeInOut, "delay", 0, "easetype", easeType, "onupdate", "AudioVolume_OnUpdate", "onupdatetarget", base.gameObject, "name", "barVolumeStart");
		iTween.StopByName(base.gameObject, "barVolumeStart");
		iTween.ValueTo(base.gameObject, args4);
		Hashtable args5 = iTween.Hash("from", 1, "to", 0, "time", num * m_audioFadeInOut, "delay", num * (1f - m_audioFadeInOut), "easetype", easeType, "onupdate", "AudioVolume_OnUpdate", "onupdatetarget", base.gameObject, "oncomplete", "AudioVolume_OnComplete", "name", "barVolumeEnd");
		iTween.StopByName(base.gameObject, "barVolumeEnd");
		iTween.ValueTo(base.gameObject, args5);
		Hashtable args6 = iTween.Hash("from", m_increasePitchStart, "to", m_increasePitchEnd, "time", num, "delay", 0, "easetype", easeType, "onupdate", "AudioPitch_OnUpdate", "onupdatetarget", base.gameObject, "name", "barPitch");
		iTween.StopByName(base.gameObject, "barPitch");
		iTween.ValueTo(base.gameObject, args6);
	}

	private void Progress_OnUpdate(float val)
	{
		if (m_barMaterial == null)
		{
			m_barMaterial = GetComponent<Renderer>().GetMaterial();
		}
		m_barMaterial.SetFloat("_Percent", val);
	}

	private void Intensity_OnComplete()
	{
		iTween.StopByName(base.gameObject, "Increase");
		iTween.StopByName(base.gameObject, "Intensity");
		iTween.StopByName(base.gameObject, "UVSpeed");
		Hashtable args = iTween.Hash("from", m_maxIntensity, "to", m_barIntensity, "time", m_coolDownAnimTime, "easetype", iTween.EaseType.easeOutQuad, "onupdate", "Intensity_OnUpdate", "onupdatetarget", base.gameObject, "name", "Intensity");
		iTween.ValueTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash("from", 0.005f, "to", 0f, "time", m_coolDownAnimTime, "easetype", iTween.EaseType.easeOutQuad, "onupdate", "ScrollSpeed_OnUpdate", "onupdatetarget", base.gameObject, "name", "UVSpeed");
		iTween.ValueTo(base.gameObject, args2);
	}

	private void Intensity_OnUpdate(float val)
	{
		if (m_barMaterial == null)
		{
			m_barMaterial = GetComponent<Renderer>().GetMaterial();
		}
		m_barMaterial.SetFloat("_Intensity", val);
	}

	private void ScrollSpeed_OnUpdate(float val)
	{
		if (m_barMaterial == null)
		{
			m_barMaterial = GetComponent<Renderer>().GetMaterial();
		}
		m_Uadd += val;
		m_barMaterial.SetFloat("_Uadd", m_Uadd);
	}

	private void AudioVolume_OnUpdate(float val)
	{
		if ((bool)GetComponent<AudioSource>())
		{
			SoundManager.Get().SetVolume(GetComponent<AudioSource>(), val);
		}
	}

	private void AudioVolume_OnComplete()
	{
		if ((bool)GetComponent<AudioSource>())
		{
			SoundManager.Get().Stop(GetComponent<AudioSource>());
		}
	}

	private void AudioPitch_OnUpdate(float val)
	{
		if ((bool)GetComponent<AudioSource>())
		{
			SoundManager.Get().SetPitch(GetComponent<AudioSource>(), val);
		}
	}

	private void DecreaseProgress(float currProgress, float prevProgress)
	{
		float num = (m_animationTime = m_decreaseAnimTime * m_factor);
		iTween.EaseType easeType = iTween.EaseType.easeInOutCubic;
		Hashtable args = iTween.Hash("from", prevProgress, "to", currProgress, "time", num, "easetype", easeType, "onupdate", "Progress_OnUpdate", "onupdatetarget", base.gameObject, "name", "Decrease");
		iTween.StopByName(base.gameObject, "Decrease");
		iTween.ValueTo(base.gameObject, args);
		if ((bool)GetComponent<AudioSource>())
		{
			SoundManager.Get().SetVolume(GetComponent<AudioSource>(), 0f);
			SoundManager.Get().SetPitch(GetComponent<AudioSource>(), m_decreasePitchStart);
			SoundManager.Get().Play(GetComponent<AudioSource>());
		}
		Hashtable args2 = iTween.Hash("from", 0, "to", 1, "time", num * m_audioFadeInOut, "delay", 0, "easetype", easeType, "onupdate", "AudioVolume_OnUpdate", "onupdatetarget", base.gameObject, "name", "barVolumeStart");
		iTween.StopByName(base.gameObject, "barVolumeStart");
		iTween.ValueTo(base.gameObject, args2);
		Hashtable args3 = iTween.Hash("from", 1, "to", 0, "time", num * m_audioFadeInOut, "delay", num * (1f - m_audioFadeInOut), "easetype", easeType, "onupdate", "AudioVolume_OnUpdate", "onupdatetarget", base.gameObject, "oncomplete", "AudioVolume_OnComplete", "name", "barVolumeEnd");
		iTween.StopByName(base.gameObject, "barVolumeEnd");
		iTween.ValueTo(base.gameObject, args3);
		Hashtable args4 = iTween.Hash("from", m_decreasePitchStart, "to", m_decreasePitchEnd, "time", num, "delay", 0, "easetype", easeType, "onupdate", "AudioPitch_OnUpdate", "onupdatetarget", base.gameObject, "name", "barPitch");
		iTween.StopByName(base.gameObject, "barPitch");
		iTween.ValueTo(base.gameObject, args4);
	}
}
