using System.Collections;
using UnityEngine;

public class ManaCostBar : MonoBehaviour
{
	public GameObject m_manaCostBarObject;

	public GameObject m_ParticleObject;

	public GameObject m_ParticleStart;

	public GameObject m_ParticleEnd;

	public GameObject m_ParticleImpact;

	public float m_maxValue = 10f;

	public float m_BarIntensity = 1.6f;

	public float m_maxIntensity = 2f;

	public float m_increaseAnimTime = 2f;

	public float m_coolDownAnimTime = 1f;

	private float m_previousVal;

	private float m_currentVal;

	private float m_factor;

	private Vector3 m_particleStartPoint = Vector3.zero;

	private Vector3 m_particleEndPoint = Vector3.zero;

	private Material m_barMaterial;

	private void Start()
	{
		if (m_manaCostBarObject == null)
		{
			base.enabled = false;
		}
		if (m_ParticleStart != null)
		{
			m_particleStartPoint = m_ParticleStart.transform.localPosition;
		}
		if (m_ParticleEnd != null)
		{
			m_particleEndPoint = m_ParticleEnd.transform.localPosition;
		}
		m_barMaterial = m_manaCostBarObject.GetComponent<Renderer>().GetMaterial();
		m_barMaterial.SetFloat("_Seed", Random.Range(0f, 1f));
	}

	private void Update()
	{
	}

	public void SetBar(float newValue)
	{
		m_currentVal = newValue / m_maxValue;
		SetBarValue(m_currentVal);
		m_previousVal = m_currentVal;
	}

	public void AnimateBar(float newValue)
	{
		if (newValue == 0f)
		{
			SetBarValue(0f);
			return;
		}
		m_currentVal = newValue / m_maxValue;
		if (!(m_manaCostBarObject == null) && m_currentVal != m_previousVal)
		{
			if (m_currentVal > m_previousVal)
			{
				m_factor = m_currentVal - m_previousVal;
			}
			else
			{
				m_factor = m_previousVal - m_currentVal;
			}
			m_factor = Mathf.Abs(m_factor);
			if (m_currentVal > m_previousVal)
			{
				IncreaseBar(m_currentVal, m_previousVal);
			}
			else
			{
				DecreaseBar(m_currentVal, m_previousVal);
			}
			m_previousVal = m_currentVal;
		}
	}

	private void SetBarValue(float val)
	{
		m_currentVal = val / m_maxValue;
		if (!(m_manaCostBarObject == null) && m_currentVal != m_previousVal)
		{
			BarPercent_OnUpdate(val);
			ParticlePosition_OnUpdate(val);
			if (val == 0f)
			{
				PlayParticles(state: false);
			}
			m_previousVal = m_currentVal;
		}
	}

	private void IncreaseBar(float newVal, float prevVal)
	{
		float num = m_increaseAnimTime * m_factor;
		PlayParticles(state: true);
		iTween.EaseType easeType = iTween.EaseType.easeInQuad;
		Hashtable args = iTween.Hash("from", prevVal, "to", newVal, "time", num, "easetype", easeType, "onupdate", "BarPercent_OnUpdate", "oncomplete", "Increase_OnComplete", "oncompletetarget", base.gameObject, "onupdatetarget", base.gameObject, "name", "IncreaseBarPercent");
		iTween.StopByName(m_manaCostBarObject.gameObject, "IncreaseBarPercent");
		iTween.ValueTo(m_manaCostBarObject.gameObject, args);
		Hashtable args2 = iTween.Hash("from", prevVal, "to", newVal, "time", num, "easetype", easeType, "onupdate", "ParticlePosition_OnUpdate", "onupdatetarget", base.gameObject, "name", "ParticlePos");
		iTween.StopByName(m_manaCostBarObject.gameObject, "ParticlePos");
		iTween.ValueTo(m_manaCostBarObject.gameObject, args2);
		Hashtable args3 = iTween.Hash("from", m_BarIntensity, "to", m_maxIntensity, "time", num, "easetype", easeType, "onupdate", "Intensity_OnUpdate", "onupdatetarget", base.gameObject, "name", "Intensity");
		iTween.StopByName(m_manaCostBarObject.gameObject, "Intensity");
		iTween.ValueTo(m_manaCostBarObject.gameObject, args3);
	}

	private void DecreaseBar(float newVal, float prevVal)
	{
		float num = m_increaseAnimTime * m_factor;
		PlayParticles(state: true);
		iTween.EaseType easeType = iTween.EaseType.easeOutQuad;
		Hashtable args = iTween.Hash("from", prevVal, "to", newVal, "time", num, "easetype", easeType, "onupdate", "BarPercent_OnUpdate", "oncomplete", "Decrease_OnComplete", "onupdatetarget", base.gameObject, "name", "IncreaseBarPercent");
		iTween.StopByName(m_manaCostBarObject.gameObject, "IncreaseBarPercent");
		iTween.ValueTo(m_manaCostBarObject.gameObject, args);
		Hashtable args2 = iTween.Hash("from", prevVal, "to", newVal, "time", num, "easetype", easeType, "onupdate", "ParticlePosition_OnUpdate", "onupdatetarget", base.gameObject, "name", "ParticlePos");
		iTween.StopByName(m_manaCostBarObject.gameObject, "ParticlePos");
		iTween.ValueTo(m_manaCostBarObject.gameObject, args2);
	}

	private void BarPercent_OnUpdate(float val)
	{
		m_barMaterial.SetFloat("_Percent", val);
	}

	private void ParticlePosition_OnUpdate(float val)
	{
		m_ParticleObject.transform.localPosition = Vector3.Lerp(m_particleStartPoint, m_particleEndPoint, val);
	}

	private void Intensity_OnUpdate(float val)
	{
		m_barMaterial.SetFloat("_Intensity", val);
	}

	private void Increase_OnComplete()
	{
		if (m_ParticleImpact != null)
		{
			m_ParticleImpact.GetComponent<ParticleSystem>().Play();
		}
		CoolDown();
	}

	private void Decrease_OnComplete()
	{
	}

	private void CoolDown()
	{
		Hashtable args = iTween.Hash("from", m_maxIntensity, "to", m_BarIntensity, "time", m_coolDownAnimTime, "easetype", iTween.EaseType.easeOutQuad, "onupdate", "Intensity_OnUpdate", "onupdatetarget", base.gameObject, "name", "CoolDownIntensity", "oncomplete", "CoolDown_OnComplete", "oncompletetarget", base.gameObject);
		iTween.StopByName(m_manaCostBarObject.gameObject, "CoolDownIntensity");
		iTween.ValueTo(m_manaCostBarObject.gameObject, args);
	}

	private void CoolDown_OnComplete()
	{
		iTween.StopByName(m_manaCostBarObject.gameObject, "CoolDownIntensity");
	}

	private void PlayParticles(bool state)
	{
		ParticleSystem[] componentsInChildren = m_ParticleObject.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem in componentsInChildren)
		{
			if (state && particleSystem != m_ParticleImpact.GetComponent<ParticleSystem>())
			{
				particleSystem.Play();
			}
			else
			{
				particleSystem.Stop();
			}
			ParticleSystem.EmissionModule emission = particleSystem.emission;
			emission.enabled = state;
		}
	}

	public void TestIncrease()
	{
		AnimateBar(7f);
	}

	public void TestDecrease()
	{
		AnimateBar(6f);
	}

	public void TestReset()
	{
		SetBar(4f);
	}
}
