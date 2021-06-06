using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000B1D RID: 2845
public class ManaCostBar : MonoBehaviour
{
	// Token: 0x06009724 RID: 38692 RVA: 0x0030D860 File Offset: 0x0030BA60
	private void Start()
	{
		if (this.m_manaCostBarObject == null)
		{
			base.enabled = false;
		}
		if (this.m_ParticleStart != null)
		{
			this.m_particleStartPoint = this.m_ParticleStart.transform.localPosition;
		}
		if (this.m_ParticleEnd != null)
		{
			this.m_particleEndPoint = this.m_ParticleEnd.transform.localPosition;
		}
		this.m_barMaterial = this.m_manaCostBarObject.GetComponent<Renderer>().GetMaterial();
		this.m_barMaterial.SetFloat("_Seed", UnityEngine.Random.Range(0f, 1f));
	}

	// Token: 0x06009725 RID: 38693 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x06009726 RID: 38694 RVA: 0x0030D8FF File Offset: 0x0030BAFF
	public void SetBar(float newValue)
	{
		this.m_currentVal = newValue / this.m_maxValue;
		this.SetBarValue(this.m_currentVal);
		this.m_previousVal = this.m_currentVal;
	}

	// Token: 0x06009727 RID: 38695 RVA: 0x0030D928 File Offset: 0x0030BB28
	public void AnimateBar(float newValue)
	{
		if (newValue == 0f)
		{
			this.SetBarValue(0f);
			return;
		}
		this.m_currentVal = newValue / this.m_maxValue;
		if (this.m_manaCostBarObject == null)
		{
			return;
		}
		if (this.m_currentVal == this.m_previousVal)
		{
			return;
		}
		if (this.m_currentVal > this.m_previousVal)
		{
			this.m_factor = this.m_currentVal - this.m_previousVal;
		}
		else
		{
			this.m_factor = this.m_previousVal - this.m_currentVal;
		}
		this.m_factor = Mathf.Abs(this.m_factor);
		if (this.m_currentVal > this.m_previousVal)
		{
			this.IncreaseBar(this.m_currentVal, this.m_previousVal);
		}
		else
		{
			this.DecreaseBar(this.m_currentVal, this.m_previousVal);
		}
		this.m_previousVal = this.m_currentVal;
	}

	// Token: 0x06009728 RID: 38696 RVA: 0x0030D9FC File Offset: 0x0030BBFC
	private void SetBarValue(float val)
	{
		this.m_currentVal = val / this.m_maxValue;
		if (this.m_manaCostBarObject == null)
		{
			return;
		}
		if (this.m_currentVal == this.m_previousVal)
		{
			return;
		}
		this.BarPercent_OnUpdate(val);
		this.ParticlePosition_OnUpdate(val);
		if (val == 0f)
		{
			this.PlayParticles(false);
		}
		this.m_previousVal = this.m_currentVal;
	}

	// Token: 0x06009729 RID: 38697 RVA: 0x0030DA60 File Offset: 0x0030BC60
	private void IncreaseBar(float newVal, float prevVal)
	{
		float num = this.m_increaseAnimTime * this.m_factor;
		this.PlayParticles(true);
		iTween.EaseType easeType = iTween.EaseType.easeInQuad;
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			prevVal,
			"to",
			newVal,
			"time",
			num,
			"easetype",
			easeType,
			"onupdate",
			"BarPercent_OnUpdate",
			"oncomplete",
			"Increase_OnComplete",
			"oncompletetarget",
			base.gameObject,
			"onupdatetarget",
			base.gameObject,
			"name",
			"IncreaseBarPercent"
		});
		iTween.StopByName(this.m_manaCostBarObject.gameObject, "IncreaseBarPercent");
		iTween.ValueTo(this.m_manaCostBarObject.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"from",
			prevVal,
			"to",
			newVal,
			"time",
			num,
			"easetype",
			easeType,
			"onupdate",
			"ParticlePosition_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"ParticlePos"
		});
		iTween.StopByName(this.m_manaCostBarObject.gameObject, "ParticlePos");
		iTween.ValueTo(this.m_manaCostBarObject.gameObject, args2);
		Hashtable args3 = iTween.Hash(new object[]
		{
			"from",
			this.m_BarIntensity,
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
			"Intensity"
		});
		iTween.StopByName(this.m_manaCostBarObject.gameObject, "Intensity");
		iTween.ValueTo(this.m_manaCostBarObject.gameObject, args3);
	}

	// Token: 0x0600972A RID: 38698 RVA: 0x0030DCBC File Offset: 0x0030BEBC
	private void DecreaseBar(float newVal, float prevVal)
	{
		float num = this.m_increaseAnimTime * this.m_factor;
		this.PlayParticles(true);
		iTween.EaseType easeType = iTween.EaseType.easeOutQuad;
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			prevVal,
			"to",
			newVal,
			"time",
			num,
			"easetype",
			easeType,
			"onupdate",
			"BarPercent_OnUpdate",
			"oncomplete",
			"Decrease_OnComplete",
			"onupdatetarget",
			base.gameObject,
			"name",
			"IncreaseBarPercent"
		});
		iTween.StopByName(this.m_manaCostBarObject.gameObject, "IncreaseBarPercent");
		iTween.ValueTo(this.m_manaCostBarObject.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"from",
			prevVal,
			"to",
			newVal,
			"time",
			num,
			"easetype",
			easeType,
			"onupdate",
			"ParticlePosition_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"ParticlePos"
		});
		iTween.StopByName(this.m_manaCostBarObject.gameObject, "ParticlePos");
		iTween.ValueTo(this.m_manaCostBarObject.gameObject, args2);
	}

	// Token: 0x0600972B RID: 38699 RVA: 0x0030DE4C File Offset: 0x0030C04C
	private void BarPercent_OnUpdate(float val)
	{
		this.m_barMaterial.SetFloat("_Percent", val);
	}

	// Token: 0x0600972C RID: 38700 RVA: 0x0030DE5F File Offset: 0x0030C05F
	private void ParticlePosition_OnUpdate(float val)
	{
		this.m_ParticleObject.transform.localPosition = Vector3.Lerp(this.m_particleStartPoint, this.m_particleEndPoint, val);
	}

	// Token: 0x0600972D RID: 38701 RVA: 0x0030DE83 File Offset: 0x0030C083
	private void Intensity_OnUpdate(float val)
	{
		this.m_barMaterial.SetFloat("_Intensity", val);
	}

	// Token: 0x0600972E RID: 38702 RVA: 0x0030DE96 File Offset: 0x0030C096
	private void Increase_OnComplete()
	{
		if (this.m_ParticleImpact != null)
		{
			this.m_ParticleImpact.GetComponent<ParticleSystem>().Play();
		}
		this.CoolDown();
	}

	// Token: 0x0600972F RID: 38703 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Decrease_OnComplete()
	{
	}

	// Token: 0x06009730 RID: 38704 RVA: 0x0030DEBC File Offset: 0x0030C0BC
	private void CoolDown()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			this.m_maxIntensity,
			"to",
			this.m_BarIntensity,
			"time",
			this.m_coolDownAnimTime,
			"easetype",
			iTween.EaseType.easeOutQuad,
			"onupdate",
			"Intensity_OnUpdate",
			"onupdatetarget",
			base.gameObject,
			"name",
			"CoolDownIntensity",
			"oncomplete",
			"CoolDown_OnComplete",
			"oncompletetarget",
			base.gameObject
		});
		iTween.StopByName(this.m_manaCostBarObject.gameObject, "CoolDownIntensity");
		iTween.ValueTo(this.m_manaCostBarObject.gameObject, args);
	}

	// Token: 0x06009731 RID: 38705 RVA: 0x0030DFAA File Offset: 0x0030C1AA
	private void CoolDown_OnComplete()
	{
		iTween.StopByName(this.m_manaCostBarObject.gameObject, "CoolDownIntensity");
	}

	// Token: 0x06009732 RID: 38706 RVA: 0x0030DFC4 File Offset: 0x0030C1C4
	private void PlayParticles(bool state)
	{
		foreach (ParticleSystem particleSystem in this.m_ParticleObject.GetComponentsInChildren<ParticleSystem>())
		{
			if (state && particleSystem != this.m_ParticleImpact.GetComponent<ParticleSystem>())
			{
				particleSystem.Play();
			}
			else
			{
				particleSystem.Stop();
			}
			particleSystem.emission.enabled = state;
		}
	}

	// Token: 0x06009733 RID: 38707 RVA: 0x0030E022 File Offset: 0x0030C222
	public void TestIncrease()
	{
		this.AnimateBar(7f);
	}

	// Token: 0x06009734 RID: 38708 RVA: 0x0030E02F File Offset: 0x0030C22F
	public void TestDecrease()
	{
		this.AnimateBar(6f);
	}

	// Token: 0x06009735 RID: 38709 RVA: 0x0030E03C File Offset: 0x0030C23C
	public void TestReset()
	{
		this.SetBar(4f);
	}

	// Token: 0x04007E7B RID: 32379
	public GameObject m_manaCostBarObject;

	// Token: 0x04007E7C RID: 32380
	public GameObject m_ParticleObject;

	// Token: 0x04007E7D RID: 32381
	public GameObject m_ParticleStart;

	// Token: 0x04007E7E RID: 32382
	public GameObject m_ParticleEnd;

	// Token: 0x04007E7F RID: 32383
	public GameObject m_ParticleImpact;

	// Token: 0x04007E80 RID: 32384
	public float m_maxValue = 10f;

	// Token: 0x04007E81 RID: 32385
	public float m_BarIntensity = 1.6f;

	// Token: 0x04007E82 RID: 32386
	public float m_maxIntensity = 2f;

	// Token: 0x04007E83 RID: 32387
	public float m_increaseAnimTime = 2f;

	// Token: 0x04007E84 RID: 32388
	public float m_coolDownAnimTime = 1f;

	// Token: 0x04007E85 RID: 32389
	private float m_previousVal;

	// Token: 0x04007E86 RID: 32390
	private float m_currentVal;

	// Token: 0x04007E87 RID: 32391
	private float m_factor;

	// Token: 0x04007E88 RID: 32392
	private Vector3 m_particleStartPoint = Vector3.zero;

	// Token: 0x04007E89 RID: 32393
	private Vector3 m_particleEndPoint = Vector3.zero;

	// Token: 0x04007E8A RID: 32394
	private Material m_barMaterial;
}
