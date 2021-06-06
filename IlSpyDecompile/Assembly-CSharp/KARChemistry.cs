using System;
using UnityEngine;

[CustomEditClass]
public class KARChemistry : MonoBehaviour
{
	public PlayMakerFSM m_Lever;

	public PlayMakerFSM m_Knob;

	public Material m_BeakerMat;

	public Material m_CurlTubeMat;

	public Material m_SmallGlobeMat;

	public Material m_HeatGlowMat;

	public ParticleSystem m_BubbleFX;

	private bool m_isLeverOn;

	private bool m_isKnobOn;

	private void Start()
	{
		m_HeatGlowMat.SetFloat("_Intensity", 0f);
		SetEmissionRate(m_BubbleFX, 0f);
		m_CurlTubeMat.SetFloat("_UVOffsetSecondX", -0.8f);
		m_CurlTubeMat.SetFloat("_Intensity", 0f);
		m_SmallGlobeMat.SetFloat("_Transistion", 0f);
		m_BeakerMat.SetFloat("_Transistion", 0f);
	}

	private void Update()
	{
		HandleHits();
	}

	private void HandleHits()
	{
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && IsOver(m_Lever.gameObject))
		{
			if (!m_isLeverOn)
			{
				LeverOnAnimations();
			}
			else
			{
				LeverOffAnimations();
			}
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && IsOver(m_Knob.gameObject))
		{
			if (!m_isKnobOn)
			{
				KnobOnAnimations();
			}
			else
			{
				KnobOffAnimations();
			}
		}
	}

	private void KnobOnAnimations()
	{
		if (!m_Knob.FsmVariables.GetFsmBool("knobAnimating").Value)
		{
			m_Knob.SendEvent("KnobTurnedOn");
			m_isKnobOn = true;
			iTween.Stop(base.gameObject);
			float @float = m_HeatGlowMat.GetFloat("_Intensity");
			iTween.ValueTo(base.gameObject, iTween.Hash("from", @float, "to", 2f, "time", 2f * ((2f - @float) / 2f), "easetype", iTween.EaseType.easeInOutCubic, "onupdate", (Action<object>)delegate(object newVal)
			{
				MaterialValueTo(m_HeatGlowMat, "_Intensity", (float)newVal);
			}));
			float emissionRate = GetEmissionRate(m_BubbleFX);
			iTween.ValueTo(base.gameObject, iTween.Hash("from", emissionRate, "to", 50f, "time", 3f * ((50f - emissionRate) / 50f), "easetype", iTween.EaseType.easeInOutCubic, "onupdate", (Action<object>)delegate(object newVal)
			{
				BubbleRate((float)newVal);
			}));
		}
	}

	private void KnobOffAnimations()
	{
		if (!m_Knob.FsmVariables.GetFsmBool("knobAnimating").Value)
		{
			m_Knob.SendEvent("KnobTurnedOff");
			m_isKnobOn = false;
			iTween.Stop(base.gameObject);
			float @float = m_HeatGlowMat.GetFloat("_Intensity");
			iTween.ValueTo(base.gameObject, iTween.Hash("from", @float, "to", 0f, "time", 2f * (@float / 2f), "easetype", iTween.EaseType.easeInOutCubic, "onupdate", (Action<object>)delegate(object newVal)
			{
				MaterialValueTo(m_HeatGlowMat, "_Intensity", (float)newVal);
			}));
			float emissionRate = GetEmissionRate(m_BubbleFX);
			iTween.ValueTo(base.gameObject, iTween.Hash("from", emissionRate, "to", 0f, "time", 1f * (emissionRate / 50f), "easetype", iTween.EaseType.easeInOutCubic, "onupdate", (Action<object>)delegate(object newVal)
			{
				BubbleRate((float)newVal);
			}));
		}
	}

	private void LeverOnAnimations()
	{
		if (!m_Lever.FsmVariables.GetFsmBool("leverAnimating").Value)
		{
			m_Lever.SendEvent("LeverTurnedOn");
			m_isLeverOn = true;
			iTween.Stop(base.gameObject);
			float @float = m_CurlTubeMat.GetFloat("_Intensity");
			float float2 = m_CurlTubeMat.GetFloat("_UVOffsetSecondX");
			iTween.ValueTo(base.gameObject, iTween.Hash("from", @float, "to", 8f, "time", 1f * ((8f - @float) / 8f), "easetype", iTween.EaseType.easeInOutCubic, "onupdate", (Action<object>)delegate(object newVal)
			{
				MaterialValueTo(m_CurlTubeMat, "_Intensity", (float)newVal);
			}));
			iTween.ValueTo(base.gameObject, iTween.Hash("from", float2, "to", 0.8f, "time", 1f, "easetype", iTween.EaseType.linear, "onupdate", (Action<object>)delegate(object newVal)
			{
				MaterialValueTo(m_CurlTubeMat, "_UVOffsetSecondX", (float)newVal);
			}));
			float float3 = m_SmallGlobeMat.GetFloat("_Transistion");
			iTween.ValueTo(base.gameObject, iTween.Hash("from", float3, "to", 1f, "time", 3f * ((1f - float3) / 1f), "delay", 1f, "easetype", iTween.EaseType.easeInOutCubic, "onupdate", (Action<object>)delegate(object newVal)
			{
				MaterialValueTo(m_SmallGlobeMat, "_Transistion", (float)newVal);
			}));
			float float4 = m_BeakerMat.GetFloat("_Transistion");
			iTween.ValueTo(base.gameObject, iTween.Hash("from", float4, "to", 1f, "time", 3f * ((1f - float4) / 1f), "delay", 1f, "easetype", iTween.EaseType.easeInOutCubic, "onupdate", (Action<object>)delegate(object newVal)
			{
				MaterialValueTo(m_BeakerMat, "_Transistion", (float)newVal);
			}));
		}
	}

	public float GetEmissionRate(ParticleSystem particleSystem)
	{
		return particleSystem.emission.rateOverTime.constantMax;
	}

	public void SetEmissionRate(ParticleSystem particleSystem, float emissionRate)
	{
		ParticleSystem.EmissionModule emission = particleSystem.emission;
		ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;
		rateOverTime.constantMax = emissionRate;
		emission.rateOverTime = rateOverTime;
	}

	private void LeverOffAnimations(bool hasScience = true)
	{
		if (m_Lever.FsmVariables.GetFsmBool("leverAnimating").Value)
		{
			return;
		}
		if (m_isKnobOn && m_isLeverOn && m_BeakerMat.GetFloat("_Transistion") == 1f && hasScience)
		{
			BlindMeWithScience();
		}
		else
		{
			m_Lever.SendEvent("LeverTurnedOff");
			iTween.Stop(base.gameObject);
			float @float = m_CurlTubeMat.GetFloat("_Intensity");
			float float2 = m_CurlTubeMat.GetFloat("_UVOffsetSecondX");
			iTween.ValueTo(base.gameObject, iTween.Hash("from", @float, "to", 0f, "time", 1f * (@float / 8f), "easetype", iTween.EaseType.easeInOutCubic, "onupdate", (Action<object>)delegate(object newVal)
			{
				MaterialValueTo(m_CurlTubeMat, "_Intensity", (float)newVal);
			}));
			iTween.ValueTo(base.gameObject, iTween.Hash("from", float2, "to", -0.8f, "time", 1f, "easetype", iTween.EaseType.linear, "onupdate", (Action<object>)delegate(object newVal)
			{
				MaterialValueTo(m_CurlTubeMat, "_UVOffsetSecondX", (float)newVal);
			}));
			float float3 = m_SmallGlobeMat.GetFloat("_Transistion");
			iTween.ValueTo(base.gameObject, iTween.Hash("from", float3, "to", 0f, "time", 4f * (float3 / 1f), "easetype", iTween.EaseType.easeInOutCubic, "onupdate", (Action<object>)delegate(object newVal)
			{
				MaterialValueTo(m_SmallGlobeMat, "_Transistion", (float)newVal);
			}));
			float float4 = m_BeakerMat.GetFloat("_Transistion");
			iTween.ValueTo(base.gameObject, iTween.Hash("from", float4, "to", 0f, "time", 4f * (float4 / 1f), "easetype", iTween.EaseType.easeInOutCubic, "onupdate", (Action<object>)delegate(object newVal)
			{
				MaterialValueTo(m_BeakerMat, "_Transistion", (float)newVal);
			}));
		}
		m_isLeverOn = false;
	}

	private void BlindMeWithScience()
	{
		m_Lever.FsmVariables.GetFsmBool("doPoof").Value = true;
		LeverOffAnimations(hasScience: false);
	}

	private void MaterialValueTo(Material mat, string property, float newVal)
	{
		mat.SetFloat(property, newVal);
	}

	private void BubbleRate(float newVal)
	{
		SetEmissionRate(m_BubbleFX, newVal);
	}

	private bool IsOver(GameObject go)
	{
		if (!go)
		{
			return false;
		}
		if (!InputUtil.IsPlayMakerMouseInputAllowed(go))
		{
			return false;
		}
		if (!UniversalInputManager.Get().InputIsOver(go))
		{
			return false;
		}
		return true;
	}
}
