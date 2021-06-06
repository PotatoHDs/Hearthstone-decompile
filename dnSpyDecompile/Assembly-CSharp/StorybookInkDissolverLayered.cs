using System;
using UnityEngine;

// Token: 0x02000828 RID: 2088
public class StorybookInkDissolverLayered : MonoBehaviour
{
	// Token: 0x06007031 RID: 28721 RVA: 0x00243012 File Offset: 0x00241212
	private void Start()
	{
		this.DoChecks();
	}

	// Token: 0x06007032 RID: 28722 RVA: 0x00243012 File Offset: 0x00241212
	private void Reset()
	{
		this.DoChecks();
	}

	// Token: 0x06007033 RID: 28723 RVA: 0x0024301C File Offset: 0x0024121C
	private void Update()
	{
		this.matDissolve = this.DissolveAnimation.Evaluate((Time.time - this.matDissolveStartTime) * this.DissolveAnimationSpeed);
		this.matDissolve2 = this.Dissolve2Animation.Evaluate((Time.time - this.matDissolve2StartTime) * this.Dissolve2AnimationSpeed);
		this.matDissolve3 = this.Dissolve3Animation.Evaluate((Time.time - this.matDissolve3StartTime) * this.Dissolve3AnimationSpeed);
		this.TargetMaterial.SetFloat("_Dissolve", this.matDissolve);
		this.TargetMaterial.SetFloat("_Dissolve2", this.matDissolve2);
		this.TargetMaterial.SetFloat("_Dissolve3", this.matDissolve3);
		if (this.RandomOffsetCycle)
		{
			float num = this.dissolveOffsetTime / this.DissolveAnimationSpeed;
			float num2 = this.dissolve2OffsetTime / this.Dissolve2AnimationSpeed;
			float num3 = this.dissolve3OffsetTime / this.Dissolve3AnimationSpeed;
			if (Time.time >= this.matDissolveStartTime + num)
			{
				this.matDissolveST[2] = UnityEngine.Random.Range(0f, 1f);
				this.matDissolveST[3] = UnityEngine.Random.Range(0f, 1f);
				this.TargetMaterial.SetVector("_DissolveTex_ST", this.matDissolveST);
				this.dissolveOffsetTime += 1f;
			}
			if (Time.time >= this.matDissolve2StartTime + num2)
			{
				this.matDissolve2ST[2] = UnityEngine.Random.Range(0f, 1f);
				this.matDissolve2ST[3] = UnityEngine.Random.Range(0f, 1f);
				this.TargetMaterial.SetVector("_Dissolve2Mod", this.matDissolve2ST);
				this.dissolve2OffsetTime += 1f;
			}
			if (Time.time >= this.matDissolve3StartTime + num3)
			{
				this.matDissolve3ST[2] = UnityEngine.Random.Range(0f, 1f);
				this.matDissolve3ST[3] = UnityEngine.Random.Range(0f, 1f);
				this.TargetMaterial.SetVector("_Dissolve3Mod", this.matDissolve3ST);
				this.dissolve3OffsetTime += 1f;
			}
		}
		if (this.AnimateIllustrationIntensity && this.TargetMaterialIllustration)
		{
			this.illusIntensity = this.IntensityAnimation.Evaluate(Time.time * this.IntensityAnimationSpeed) * this.IntensityAnimationValueScale + this.IntensityAnimationValueOffset;
			this.TargetMaterialIllustration.SetFloat("_MainTexIntensity", this.illusIntensity);
		}
	}

	// Token: 0x06007034 RID: 28724 RVA: 0x002432A9 File Offset: 0x002414A9
	private AnimationCurve MakeNewDefault()
	{
		AnimationCurve animationCurve = AnimationCurve.Linear(0f, 1f, 1f, -1f);
		animationCurve.preWrapMode = WrapMode.Loop;
		animationCurve.postWrapMode = WrapMode.Loop;
		return animationCurve;
	}

	// Token: 0x06007035 RID: 28725 RVA: 0x002432D4 File Offset: 0x002414D4
	private void DoChecks()
	{
		if (this.TargetMaterial == null)
		{
			this.TargetMaterial = base.gameObject.GetComponent<Renderer>().GetMaterial();
		}
		if (this.TargetMaterial == null)
		{
			Debug.Log("StorybookInkDissolver: no target material");
			return;
		}
		if (this.TargetMaterialIllustration == null && this.TargetObjectWithIllustrationMaterial != null)
		{
			this.TargetMaterialIllustration = this.TargetObjectWithIllustrationMaterial.GetComponent<Renderer>().GetMaterial();
		}
		if (this.TargetMaterialIllustration == null)
		{
			Debug.Log("StorybookInkDissolver: no target material for intensity animation");
			return;
		}
		if (this.DissolveAnimation == null || this.DissolveAnimation.length < 1)
		{
			this.DissolveAnimation = this.MakeNewDefault();
			this.Dissolve2Animation = this.MakeNewDefault();
			this.Dissolve3Animation = this.MakeNewDefault();
		}
		if (this.IntensityAnimation == null || this.IntensityAnimation.length < 1)
		{
			this.IntensityAnimation = this.MakeNewDefault();
		}
		this.matDissolveST = this.TargetMaterial.GetVector("_DissolveTex_ST");
		this.matDissolve2ST = this.TargetMaterial.GetVector("_Dissolve2Mod");
		this.matDissolve3ST = this.TargetMaterial.GetVector("_Dissolve3Mod");
		this.dissolveOffsetTime = 1f;
		this.dissolve2OffsetTime = 1f;
		this.dissolve3OffsetTime = 1f;
		this.matDissolveStartTime = Time.time - (1f - this.DissolveAnimationTimeOffset) / this.DissolveAnimationSpeed;
		this.matDissolve2StartTime = Time.time - (1f - this.Dissolve2AnimationTimeOffset) / this.Dissolve2AnimationSpeed;
		this.matDissolve3StartTime = Time.time - (1f - this.Dissolve3AnimationTimeOffset) / this.Dissolve3AnimationSpeed;
	}

	// Token: 0x040059FD RID: 23037
	public Material TargetMaterial;

	// Token: 0x040059FE RID: 23038
	public bool RandomOffsetCycle = true;

	// Token: 0x040059FF RID: 23039
	public AnimationCurve DissolveAnimation;

	// Token: 0x04005A00 RID: 23040
	public float DissolveAnimationSpeed = 0.05f;

	// Token: 0x04005A01 RID: 23041
	public float DissolveAnimationTimeOffset;

	// Token: 0x04005A02 RID: 23042
	public AnimationCurve Dissolve2Animation;

	// Token: 0x04005A03 RID: 23043
	public float Dissolve2AnimationSpeed = 0.05f;

	// Token: 0x04005A04 RID: 23044
	public float Dissolve2AnimationTimeOffset = 0.333f;

	// Token: 0x04005A05 RID: 23045
	public AnimationCurve Dissolve3Animation;

	// Token: 0x04005A06 RID: 23046
	public float Dissolve3AnimationSpeed = 0.05f;

	// Token: 0x04005A07 RID: 23047
	public float Dissolve3AnimationTimeOffset = 0.666f;

	// Token: 0x04005A08 RID: 23048
	private float matDissolve;

	// Token: 0x04005A09 RID: 23049
	private float matDissolve2;

	// Token: 0x04005A0A RID: 23050
	private float matDissolve3;

	// Token: 0x04005A0B RID: 23051
	private Vector4 matDissolveST;

	// Token: 0x04005A0C RID: 23052
	private Vector4 matDissolve2ST;

	// Token: 0x04005A0D RID: 23053
	private Vector4 matDissolve3ST;

	// Token: 0x04005A0E RID: 23054
	private float dissolveOffsetTime;

	// Token: 0x04005A0F RID: 23055
	private float dissolve2OffsetTime;

	// Token: 0x04005A10 RID: 23056
	private float dissolve3OffsetTime;

	// Token: 0x04005A11 RID: 23057
	private float matDissolveStartTime;

	// Token: 0x04005A12 RID: 23058
	private float matDissolve2StartTime;

	// Token: 0x04005A13 RID: 23059
	private float matDissolve3StartTime;

	// Token: 0x04005A14 RID: 23060
	public GameObject TargetObjectWithIllustrationMaterial;

	// Token: 0x04005A15 RID: 23061
	private Material TargetMaterialIllustration;

	// Token: 0x04005A16 RID: 23062
	public bool AnimateIllustrationIntensity = true;

	// Token: 0x04005A17 RID: 23063
	public AnimationCurve IntensityAnimation;

	// Token: 0x04005A18 RID: 23064
	public float IntensityAnimationSpeed = 0.25f;

	// Token: 0x04005A19 RID: 23065
	public float IntensityAnimationValueScale;

	// Token: 0x04005A1A RID: 23066
	public float IntensityAnimationValueOffset = 1f;

	// Token: 0x04005A1B RID: 23067
	private float illusIntensity;
}
