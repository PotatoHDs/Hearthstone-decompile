using System;
using System.Collections;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000B28 RID: 2856
public class ProgressBar : MonoBehaviour
{
	// Token: 0x17000897 RID: 2199
	// (get) Token: 0x0600977A RID: 38778 RVA: 0x0030F478 File Offset: 0x0030D678
	// (set) Token: 0x0600977B RID: 38779 RVA: 0x0030F480 File Offset: 0x0030D680
	[Overridable]
	public float Progress
	{
		get
		{
			return this.m_progress;
		}
		set
		{
			this.SetProgressBar(value);
		}
	}

	// Token: 0x0600977C RID: 38780 RVA: 0x0030F489 File Offset: 0x0030D689
	public void Awake()
	{
		this.m_barMaterial = base.GetComponent<Renderer>().GetMaterial();
	}

	// Token: 0x0600977D RID: 38781 RVA: 0x0030F49C File Offset: 0x0030D69C
	public void OnDestroy()
	{
		UnityEngine.Object.Destroy(this.m_barMaterial);
	}

	// Token: 0x0600977E RID: 38782 RVA: 0x0030F4A9 File Offset: 0x0030D6A9
	public void SetMaterial(Material material)
	{
		this.m_barMaterial = material;
	}

	// Token: 0x0600977F RID: 38783 RVA: 0x0030F4B4 File Offset: 0x0030D6B4
	public void AnimateProgress(float prevVal, float currVal, iTween.EaseType easeType = iTween.EaseType.easeOutQuad)
	{
		this.m_prevVal = prevVal;
		this.m_currVal = currVal;
		if (this.m_currVal > this.m_prevVal)
		{
			this.m_factor = this.m_currVal - this.m_prevVal;
		}
		else
		{
			this.m_factor = this.m_prevVal - this.m_currVal;
		}
		this.m_factor = Mathf.Abs(this.m_factor);
		if (this.m_currVal > this.m_prevVal)
		{
			this.IncreaseProgress(this.m_currVal, this.m_prevVal, easeType);
			return;
		}
		this.DecreaseProgress(this.m_currVal, this.m_prevVal);
	}

	// Token: 0x06009780 RID: 38784 RVA: 0x0030F54C File Offset: 0x0030D74C
	public void SetProgressBar(float progress)
	{
		this.m_progress = progress;
		if (this.m_barMaterial == null)
		{
			this.m_barMaterial = base.GetComponent<Renderer>().GetMaterial();
		}
		this.m_barMaterial.SetFloat("_Intensity", this.m_barIntensity);
		this.m_barMaterial.SetFloat("_Percent", progress);
	}

	// Token: 0x06009781 RID: 38785 RVA: 0x0030F5A6 File Offset: 0x0030D7A6
	public float GetAnimationTime()
	{
		return this.m_animationTime;
	}

	// Token: 0x06009782 RID: 38786 RVA: 0x0030F5AE File Offset: 0x0030D7AE
	public void SetLabel(string text)
	{
		if (this.m_uberLabel != null)
		{
			this.m_uberLabel.Text = text;
		}
		if (this.m_label != null)
		{
			this.m_label.text = text;
		}
	}

	// Token: 0x06009783 RID: 38787 RVA: 0x0030F5E4 File Offset: 0x0030D7E4
	public void SetBarTexture(Texture texture)
	{
		if (this.m_barMaterial == null)
		{
			this.m_barMaterial = base.GetComponent<Renderer>().GetMaterial();
		}
		this.m_barMaterial.SetTexture("_NoiseTex", texture);
	}

	// Token: 0x06009784 RID: 38788 RVA: 0x0030F618 File Offset: 0x0030D818
	private void IncreaseProgress(float currProgress, float prevProgress, iTween.EaseType easeType)
	{
		float num = this.m_increaseAnimTime * this.m_factor;
		this.m_animationTime = num;
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			prevProgress,
			"to",
			currProgress,
			"time",
			num,
			"easetype",
			easeType,
			"onupdate",
			"Progress_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"IncreaseProgress"
		});
		iTween.StopByName(base.gameObject, "IncreaseProgress");
		iTween.ValueTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"from",
			0f,
			"to",
			0.005f,
			"time",
			num,
			"easetype",
			iTween.EaseType.easeOutQuad,
			"onupdate",
			"ScrollSpeed_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"UVSpeed"
		});
		iTween.StopByName(base.gameObject, "UVSpeed");
		iTween.ValueTo(base.gameObject, args2);
		this.m_maxIntensity = this.m_barIntensity + (this.m_barIntensityIncreaseMax - this.m_barIntensity) * this.m_factor;
		Hashtable args3 = iTween.Hash(new object[]
		{
			"from",
			this.m_barIntensity,
			"to",
			this.m_maxIntensity,
			"time",
			num,
			"easetype",
			easeType,
			"onupdate",
			"Intensity_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"Intensity",
			"oncomplete",
			"Intensity_OnComplete",
			"oncompletetarget",
			base.gameObject
		});
		iTween.StopByName(base.gameObject, "Intensity");
		iTween.ValueTo(base.gameObject, args3);
		if (base.GetComponent<AudioSource>())
		{
			SoundManager.Get().SetVolume(base.GetComponent<AudioSource>(), 0f);
			SoundManager.Get().SetPitch(base.GetComponent<AudioSource>(), this.m_increasePitchStart);
			SoundManager.Get().Play(base.GetComponent<AudioSource>(), null, null, null);
		}
		Hashtable args4 = iTween.Hash(new object[]
		{
			"from",
			0,
			"to",
			1,
			"time",
			num * this.m_audioFadeInOut,
			"delay",
			0,
			"easetype",
			easeType,
			"onupdate",
			"AudioVolume_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"barVolumeStart"
		});
		iTween.StopByName(base.gameObject, "barVolumeStart");
		iTween.ValueTo(base.gameObject, args4);
		Hashtable args5 = iTween.Hash(new object[]
		{
			"from",
			1,
			"to",
			0,
			"time",
			num * this.m_audioFadeInOut,
			"delay",
			num * (1f - this.m_audioFadeInOut),
			"easetype",
			easeType,
			"onupdate",
			"AudioVolume_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"oncomplete",
			"AudioVolume_OnComplete",
			"name",
			"barVolumeEnd"
		});
		iTween.StopByName(base.gameObject, "barVolumeEnd");
		iTween.ValueTo(base.gameObject, args5);
		Hashtable args6 = iTween.Hash(new object[]
		{
			"from",
			this.m_increasePitchStart,
			"to",
			this.m_increasePitchEnd,
			"time",
			num,
			"delay",
			0,
			"easetype",
			easeType,
			"onupdate",
			"AudioPitch_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"barPitch"
		});
		iTween.StopByName(base.gameObject, "barPitch");
		iTween.ValueTo(base.gameObject, args6);
	}

	// Token: 0x06009785 RID: 38789 RVA: 0x0030FB26 File Offset: 0x0030DD26
	private void Progress_OnUpdate(float val)
	{
		if (this.m_barMaterial == null)
		{
			this.m_barMaterial = base.GetComponent<Renderer>().GetMaterial();
		}
		this.m_barMaterial.SetFloat("_Percent", val);
	}

	// Token: 0x06009786 RID: 38790 RVA: 0x0030FB58 File Offset: 0x0030DD58
	private void Intensity_OnComplete()
	{
		iTween.StopByName(base.gameObject, "Increase");
		iTween.StopByName(base.gameObject, "Intensity");
		iTween.StopByName(base.gameObject, "UVSpeed");
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			this.m_maxIntensity,
			"to",
			this.m_barIntensity,
			"time",
			this.m_coolDownAnimTime,
			"easetype",
			iTween.EaseType.easeOutQuad,
			"onupdate",
			"Intensity_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"Intensity"
		});
		iTween.ValueTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"from",
			0.005f,
			"to",
			0f,
			"time",
			this.m_coolDownAnimTime,
			"easetype",
			iTween.EaseType.easeOutQuad,
			"onupdate",
			"ScrollSpeed_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"UVSpeed"
		});
		iTween.ValueTo(base.gameObject, args2);
	}

	// Token: 0x06009787 RID: 38791 RVA: 0x0030FCD7 File Offset: 0x0030DED7
	private void Intensity_OnUpdate(float val)
	{
		if (this.m_barMaterial == null)
		{
			this.m_barMaterial = base.GetComponent<Renderer>().GetMaterial();
		}
		this.m_barMaterial.SetFloat("_Intensity", val);
	}

	// Token: 0x06009788 RID: 38792 RVA: 0x0030FD0C File Offset: 0x0030DF0C
	private void ScrollSpeed_OnUpdate(float val)
	{
		if (this.m_barMaterial == null)
		{
			this.m_barMaterial = base.GetComponent<Renderer>().GetMaterial();
		}
		this.m_Uadd += val;
		this.m_barMaterial.SetFloat("_Uadd", this.m_Uadd);
	}

	// Token: 0x06009789 RID: 38793 RVA: 0x0030FD5C File Offset: 0x0030DF5C
	private void AudioVolume_OnUpdate(float val)
	{
		if (base.GetComponent<AudioSource>())
		{
			SoundManager.Get().SetVolume(base.GetComponent<AudioSource>(), val);
		}
	}

	// Token: 0x0600978A RID: 38794 RVA: 0x0030FD7C File Offset: 0x0030DF7C
	private void AudioVolume_OnComplete()
	{
		if (base.GetComponent<AudioSource>())
		{
			SoundManager.Get().Stop(base.GetComponent<AudioSource>());
		}
	}

	// Token: 0x0600978B RID: 38795 RVA: 0x0030FD9C File Offset: 0x0030DF9C
	private void AudioPitch_OnUpdate(float val)
	{
		if (base.GetComponent<AudioSource>())
		{
			SoundManager.Get().SetPitch(base.GetComponent<AudioSource>(), val);
		}
	}

	// Token: 0x0600978C RID: 38796 RVA: 0x0030FDBC File Offset: 0x0030DFBC
	private void DecreaseProgress(float currProgress, float prevProgress)
	{
		float num = this.m_decreaseAnimTime * this.m_factor;
		this.m_animationTime = num;
		iTween.EaseType easeType = iTween.EaseType.easeInOutCubic;
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			prevProgress,
			"to",
			currProgress,
			"time",
			num,
			"easetype",
			easeType,
			"onupdate",
			"Progress_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"Decrease"
		});
		iTween.StopByName(base.gameObject, "Decrease");
		iTween.ValueTo(base.gameObject, args);
		if (base.GetComponent<AudioSource>())
		{
			SoundManager.Get().SetVolume(base.GetComponent<AudioSource>(), 0f);
			SoundManager.Get().SetPitch(base.GetComponent<AudioSource>(), this.m_decreasePitchStart);
			SoundManager.Get().Play(base.GetComponent<AudioSource>(), null, null, null);
		}
		Hashtable args2 = iTween.Hash(new object[]
		{
			"from",
			0,
			"to",
			1,
			"time",
			num * this.m_audioFadeInOut,
			"delay",
			0,
			"easetype",
			easeType,
			"onupdate",
			"AudioVolume_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"barVolumeStart"
		});
		iTween.StopByName(base.gameObject, "barVolumeStart");
		iTween.ValueTo(base.gameObject, args2);
		Hashtable args3 = iTween.Hash(new object[]
		{
			"from",
			1,
			"to",
			0,
			"time",
			num * this.m_audioFadeInOut,
			"delay",
			num * (1f - this.m_audioFadeInOut),
			"easetype",
			easeType,
			"onupdate",
			"AudioVolume_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"oncomplete",
			"AudioVolume_OnComplete",
			"name",
			"barVolumeEnd"
		});
		iTween.StopByName(base.gameObject, "barVolumeEnd");
		iTween.ValueTo(base.gameObject, args3);
		Hashtable args4 = iTween.Hash(new object[]
		{
			"from",
			this.m_decreasePitchStart,
			"to",
			this.m_decreasePitchEnd,
			"time",
			num,
			"delay",
			0,
			"easetype",
			easeType,
			"onupdate",
			"AudioPitch_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"barPitch"
		});
		iTween.StopByName(base.gameObject, "barPitch");
		iTween.ValueTo(base.gameObject, args4);
	}

	// Token: 0x04007ED6 RID: 32470
	public TextMesh m_label;

	// Token: 0x04007ED7 RID: 32471
	public UberText m_uberLabel;

	// Token: 0x04007ED8 RID: 32472
	public float m_increaseAnimTime = 2f;

	// Token: 0x04007ED9 RID: 32473
	public float m_decreaseAnimTime = 1f;

	// Token: 0x04007EDA RID: 32474
	public float m_coolDownAnimTime = 1f;

	// Token: 0x04007EDB RID: 32475
	public float m_barIntensity = 1.2f;

	// Token: 0x04007EDC RID: 32476
	public float m_barIntensityIncreaseMax = 3f;

	// Token: 0x04007EDD RID: 32477
	public float m_audioFadeInOut = 0.2f;

	// Token: 0x04007EDE RID: 32478
	public float m_increasePitchStart = 1f;

	// Token: 0x04007EDF RID: 32479
	public float m_increasePitchEnd = 1.2f;

	// Token: 0x04007EE0 RID: 32480
	public float m_decreasePitchStart = 1f;

	// Token: 0x04007EE1 RID: 32481
	public float m_decreasePitchEnd = 0.8f;

	// Token: 0x04007EE2 RID: 32482
	private Material m_barMaterial;

	// Token: 0x04007EE3 RID: 32483
	private float m_prevVal;

	// Token: 0x04007EE4 RID: 32484
	private float m_currVal;

	// Token: 0x04007EE5 RID: 32485
	private float m_factor;

	// Token: 0x04007EE6 RID: 32486
	private float m_maxIntensity;

	// Token: 0x04007EE7 RID: 32487
	private float m_Uadd;

	// Token: 0x04007EE8 RID: 32488
	private float m_animationTime;

	// Token: 0x04007EE9 RID: 32489
	private float m_progress;
}
