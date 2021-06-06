using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
[CustomEditClass]
public class KARChemistry : MonoBehaviour
{
	// Token: 0x06000AD9 RID: 2777 RVA: 0x00040374 File Offset: 0x0003E574
	private void Start()
	{
		this.m_HeatGlowMat.SetFloat("_Intensity", 0f);
		this.SetEmissionRate(this.m_BubbleFX, 0f);
		this.m_CurlTubeMat.SetFloat("_UVOffsetSecondX", -0.8f);
		this.m_CurlTubeMat.SetFloat("_Intensity", 0f);
		this.m_SmallGlobeMat.SetFloat("_Transistion", 0f);
		this.m_BeakerMat.SetFloat("_Transistion", 0f);
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x000403FB File Offset: 0x0003E5FB
	private void Update()
	{
		this.HandleHits();
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x00040404 File Offset: 0x0003E604
	private void HandleHits()
	{
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && this.IsOver(this.m_Lever.gameObject))
		{
			if (!this.m_isLeverOn)
			{
				this.LeverOnAnimations();
			}
			else
			{
				this.LeverOffAnimations(true);
			}
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0) && this.IsOver(this.m_Knob.gameObject))
		{
			if (!this.m_isKnobOn)
			{
				this.KnobOnAnimations();
				return;
			}
			this.KnobOffAnimations();
		}
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x00040480 File Offset: 0x0003E680
	private void KnobOnAnimations()
	{
		if (!this.m_Knob.FsmVariables.GetFsmBool("knobAnimating").Value)
		{
			this.m_Knob.SendEvent("KnobTurnedOn");
			this.m_isKnobOn = true;
			iTween.Stop(base.gameObject);
			float @float = this.m_HeatGlowMat.GetFloat("_Intensity");
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"from",
				@float,
				"to",
				2f,
				"time",
				2f * ((2f - @float) / 2f),
				"easetype",
				iTween.EaseType.easeInOutCubic,
				"onupdate",
				new Action<object>(delegate(object newVal)
				{
					this.MaterialValueTo(this.m_HeatGlowMat, "_Intensity", (float)newVal);
				})
			}));
			float emissionRate = this.GetEmissionRate(this.m_BubbleFX);
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"from",
				emissionRate,
				"to",
				50f,
				"time",
				3f * ((50f - emissionRate) / 50f),
				"easetype",
				iTween.EaseType.easeInOutCubic,
				"onupdate",
				new Action<object>(delegate(object newVal)
				{
					this.BubbleRate((float)newVal);
				})
			}));
		}
	}

	// Token: 0x06000ADD RID: 2781 RVA: 0x00040600 File Offset: 0x0003E800
	private void KnobOffAnimations()
	{
		if (!this.m_Knob.FsmVariables.GetFsmBool("knobAnimating").Value)
		{
			this.m_Knob.SendEvent("KnobTurnedOff");
			this.m_isKnobOn = false;
			iTween.Stop(base.gameObject);
			float @float = this.m_HeatGlowMat.GetFloat("_Intensity");
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"from",
				@float,
				"to",
				0f,
				"time",
				2f * (@float / 2f),
				"easetype",
				iTween.EaseType.easeInOutCubic,
				"onupdate",
				new Action<object>(delegate(object newVal)
				{
					this.MaterialValueTo(this.m_HeatGlowMat, "_Intensity", (float)newVal);
				})
			}));
			float emissionRate = this.GetEmissionRate(this.m_BubbleFX);
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"from",
				emissionRate,
				"to",
				0f,
				"time",
				1f * (emissionRate / 50f),
				"easetype",
				iTween.EaseType.easeInOutCubic,
				"onupdate",
				new Action<object>(delegate(object newVal)
				{
					this.BubbleRate((float)newVal);
				})
			}));
		}
	}

	// Token: 0x06000ADE RID: 2782 RVA: 0x00040774 File Offset: 0x0003E974
	private void LeverOnAnimations()
	{
		if (!this.m_Lever.FsmVariables.GetFsmBool("leverAnimating").Value)
		{
			this.m_Lever.SendEvent("LeverTurnedOn");
			this.m_isLeverOn = true;
			iTween.Stop(base.gameObject);
			float @float = this.m_CurlTubeMat.GetFloat("_Intensity");
			float float2 = this.m_CurlTubeMat.GetFloat("_UVOffsetSecondX");
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"from",
				@float,
				"to",
				8f,
				"time",
				1f * ((8f - @float) / 8f),
				"easetype",
				iTween.EaseType.easeInOutCubic,
				"onupdate",
				new Action<object>(delegate(object newVal)
				{
					this.MaterialValueTo(this.m_CurlTubeMat, "_Intensity", (float)newVal);
				})
			}));
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"from",
				float2,
				"to",
				0.8f,
				"time",
				1f,
				"easetype",
				iTween.EaseType.linear,
				"onupdate",
				new Action<object>(delegate(object newVal)
				{
					this.MaterialValueTo(this.m_CurlTubeMat, "_UVOffsetSecondX", (float)newVal);
				})
			}));
			float float3 = this.m_SmallGlobeMat.GetFloat("_Transistion");
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"from",
				float3,
				"to",
				1f,
				"time",
				3f * ((1f - float3) / 1f),
				"delay",
				1f,
				"easetype",
				iTween.EaseType.easeInOutCubic,
				"onupdate",
				new Action<object>(delegate(object newVal)
				{
					this.MaterialValueTo(this.m_SmallGlobeMat, "_Transistion", (float)newVal);
				})
			}));
			float float4 = this.m_BeakerMat.GetFloat("_Transistion");
			iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
			{
				"from",
				float4,
				"to",
				1f,
				"time",
				3f * ((1f - float4) / 1f),
				"delay",
				1f,
				"easetype",
				iTween.EaseType.easeInOutCubic,
				"onupdate",
				new Action<object>(delegate(object newVal)
				{
					this.MaterialValueTo(this.m_BeakerMat, "_Transistion", (float)newVal);
				})
			}));
		}
	}

	// Token: 0x06000ADF RID: 2783 RVA: 0x00040A4C File Offset: 0x0003EC4C
	public float GetEmissionRate(ParticleSystem particleSystem)
	{
		return particleSystem.emission.rateOverTime.constantMax;
	}

	// Token: 0x06000AE0 RID: 2784 RVA: 0x00040A70 File Offset: 0x0003EC70
	public void SetEmissionRate(ParticleSystem particleSystem, float emissionRate)
	{
		ParticleSystem.EmissionModule emission = particleSystem.emission;
		ParticleSystem.MinMaxCurve rateOverTime = emission.rateOverTime;
		rateOverTime.constantMax = emissionRate;
		emission.rateOverTime = rateOverTime;
	}

	// Token: 0x06000AE1 RID: 2785 RVA: 0x00040A9C File Offset: 0x0003EC9C
	private void LeverOffAnimations(bool hasScience = true)
	{
		if (!this.m_Lever.FsmVariables.GetFsmBool("leverAnimating").Value)
		{
			if (this.m_isKnobOn && this.m_isLeverOn && this.m_BeakerMat.GetFloat("_Transistion") == 1f && hasScience)
			{
				this.BlindMeWithScience();
			}
			else
			{
				this.m_Lever.SendEvent("LeverTurnedOff");
				iTween.Stop(base.gameObject);
				float @float = this.m_CurlTubeMat.GetFloat("_Intensity");
				float float2 = this.m_CurlTubeMat.GetFloat("_UVOffsetSecondX");
				iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
				{
					"from",
					@float,
					"to",
					0f,
					"time",
					1f * (@float / 8f),
					"easetype",
					iTween.EaseType.easeInOutCubic,
					"onupdate",
					new Action<object>(delegate(object newVal)
					{
						this.MaterialValueTo(this.m_CurlTubeMat, "_Intensity", (float)newVal);
					})
				}));
				iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
				{
					"from",
					float2,
					"to",
					-0.8f,
					"time",
					1f,
					"easetype",
					iTween.EaseType.linear,
					"onupdate",
					new Action<object>(delegate(object newVal)
					{
						this.MaterialValueTo(this.m_CurlTubeMat, "_UVOffsetSecondX", (float)newVal);
					})
				}));
				float float3 = this.m_SmallGlobeMat.GetFloat("_Transistion");
				iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
				{
					"from",
					float3,
					"to",
					0f,
					"time",
					4f * (float3 / 1f),
					"easetype",
					iTween.EaseType.easeInOutCubic,
					"onupdate",
					new Action<object>(delegate(object newVal)
					{
						this.MaterialValueTo(this.m_SmallGlobeMat, "_Transistion", (float)newVal);
					})
				}));
				float float4 = this.m_BeakerMat.GetFloat("_Transistion");
				iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
				{
					"from",
					float4,
					"to",
					0f,
					"time",
					4f * (float4 / 1f),
					"easetype",
					iTween.EaseType.easeInOutCubic,
					"onupdate",
					new Action<object>(delegate(object newVal)
					{
						this.MaterialValueTo(this.m_BeakerMat, "_Transistion", (float)newVal);
					})
				}));
			}
			this.m_isLeverOn = false;
		}
	}

	// Token: 0x06000AE2 RID: 2786 RVA: 0x00040D6C File Offset: 0x0003EF6C
	private void BlindMeWithScience()
	{
		this.m_Lever.FsmVariables.GetFsmBool("doPoof").Value = true;
		this.LeverOffAnimations(false);
	}

	// Token: 0x06000AE3 RID: 2787 RVA: 0x00040D90 File Offset: 0x0003EF90
	private void MaterialValueTo(Material mat, string property, float newVal)
	{
		mat.SetFloat(property, newVal);
	}

	// Token: 0x06000AE4 RID: 2788 RVA: 0x00040D9A File Offset: 0x0003EF9A
	private void BubbleRate(float newVal)
	{
		this.SetEmissionRate(this.m_BubbleFX, newVal);
	}

	// Token: 0x06000AE5 RID: 2789 RVA: 0x000402EE File Offset: 0x0003E4EE
	private bool IsOver(GameObject go)
	{
		return go && InputUtil.IsPlayMakerMouseInputAllowed(go) && UniversalInputManager.Get().InputIsOver(go);
	}

	// Token: 0x040006EA RID: 1770
	public PlayMakerFSM m_Lever;

	// Token: 0x040006EB RID: 1771
	public PlayMakerFSM m_Knob;

	// Token: 0x040006EC RID: 1772
	public Material m_BeakerMat;

	// Token: 0x040006ED RID: 1773
	public Material m_CurlTubeMat;

	// Token: 0x040006EE RID: 1774
	public Material m_SmallGlobeMat;

	// Token: 0x040006EF RID: 1775
	public Material m_HeatGlowMat;

	// Token: 0x040006F0 RID: 1776
	public ParticleSystem m_BubbleFX;

	// Token: 0x040006F1 RID: 1777
	private bool m_isLeverOn;

	// Token: 0x040006F2 RID: 1778
	private bool m_isKnobOn;
}
