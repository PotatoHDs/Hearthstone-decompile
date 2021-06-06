using UnityEngine;

public class StorybookInkDissolverLayered : MonoBehaviour
{
	public Material TargetMaterial;

	public bool RandomOffsetCycle = true;

	public AnimationCurve DissolveAnimation;

	public float DissolveAnimationSpeed = 0.05f;

	public float DissolveAnimationTimeOffset;

	public AnimationCurve Dissolve2Animation;

	public float Dissolve2AnimationSpeed = 0.05f;

	public float Dissolve2AnimationTimeOffset = 0.333f;

	public AnimationCurve Dissolve3Animation;

	public float Dissolve3AnimationSpeed = 0.05f;

	public float Dissolve3AnimationTimeOffset = 0.666f;

	private float matDissolve;

	private float matDissolve2;

	private float matDissolve3;

	private Vector4 matDissolveST;

	private Vector4 matDissolve2ST;

	private Vector4 matDissolve3ST;

	private float dissolveOffsetTime;

	private float dissolve2OffsetTime;

	private float dissolve3OffsetTime;

	private float matDissolveStartTime;

	private float matDissolve2StartTime;

	private float matDissolve3StartTime;

	public GameObject TargetObjectWithIllustrationMaterial;

	private Material TargetMaterialIllustration;

	public bool AnimateIllustrationIntensity = true;

	public AnimationCurve IntensityAnimation;

	public float IntensityAnimationSpeed = 0.25f;

	public float IntensityAnimationValueScale;

	public float IntensityAnimationValueOffset = 1f;

	private float illusIntensity;

	private void Start()
	{
		DoChecks();
	}

	private void Reset()
	{
		DoChecks();
	}

	private void Update()
	{
		matDissolve = DissolveAnimation.Evaluate((Time.time - matDissolveStartTime) * DissolveAnimationSpeed);
		matDissolve2 = Dissolve2Animation.Evaluate((Time.time - matDissolve2StartTime) * Dissolve2AnimationSpeed);
		matDissolve3 = Dissolve3Animation.Evaluate((Time.time - matDissolve3StartTime) * Dissolve3AnimationSpeed);
		TargetMaterial.SetFloat("_Dissolve", matDissolve);
		TargetMaterial.SetFloat("_Dissolve2", matDissolve2);
		TargetMaterial.SetFloat("_Dissolve3", matDissolve3);
		if (RandomOffsetCycle)
		{
			float num = dissolveOffsetTime / DissolveAnimationSpeed;
			float num2 = dissolve2OffsetTime / Dissolve2AnimationSpeed;
			float num3 = dissolve3OffsetTime / Dissolve3AnimationSpeed;
			if (Time.time >= matDissolveStartTime + num)
			{
				matDissolveST[2] = Random.Range(0f, 1f);
				matDissolveST[3] = Random.Range(0f, 1f);
				TargetMaterial.SetVector("_DissolveTex_ST", matDissolveST);
				dissolveOffsetTime += 1f;
			}
			if (Time.time >= matDissolve2StartTime + num2)
			{
				matDissolve2ST[2] = Random.Range(0f, 1f);
				matDissolve2ST[3] = Random.Range(0f, 1f);
				TargetMaterial.SetVector("_Dissolve2Mod", matDissolve2ST);
				dissolve2OffsetTime += 1f;
			}
			if (Time.time >= matDissolve3StartTime + num3)
			{
				matDissolve3ST[2] = Random.Range(0f, 1f);
				matDissolve3ST[3] = Random.Range(0f, 1f);
				TargetMaterial.SetVector("_Dissolve3Mod", matDissolve3ST);
				dissolve3OffsetTime += 1f;
			}
		}
		if (AnimateIllustrationIntensity && (bool)TargetMaterialIllustration)
		{
			illusIntensity = IntensityAnimation.Evaluate(Time.time * IntensityAnimationSpeed) * IntensityAnimationValueScale + IntensityAnimationValueOffset;
			TargetMaterialIllustration.SetFloat("_MainTexIntensity", illusIntensity);
		}
	}

	private AnimationCurve MakeNewDefault()
	{
		AnimationCurve animationCurve = AnimationCurve.Linear(0f, 1f, 1f, -1f);
		animationCurve.preWrapMode = WrapMode.Loop;
		animationCurve.postWrapMode = WrapMode.Loop;
		return animationCurve;
	}

	private void DoChecks()
	{
		if (TargetMaterial == null)
		{
			TargetMaterial = base.gameObject.GetComponent<Renderer>().GetMaterial();
		}
		if (TargetMaterial == null)
		{
			Debug.Log("StorybookInkDissolver: no target material");
			return;
		}
		if (TargetMaterialIllustration == null && TargetObjectWithIllustrationMaterial != null)
		{
			TargetMaterialIllustration = TargetObjectWithIllustrationMaterial.GetComponent<Renderer>().GetMaterial();
		}
		if (TargetMaterialIllustration == null)
		{
			Debug.Log("StorybookInkDissolver: no target material for intensity animation");
			return;
		}
		if (DissolveAnimation == null || DissolveAnimation.length < 1)
		{
			DissolveAnimation = MakeNewDefault();
			Dissolve2Animation = MakeNewDefault();
			Dissolve3Animation = MakeNewDefault();
		}
		if (IntensityAnimation == null || IntensityAnimation.length < 1)
		{
			IntensityAnimation = MakeNewDefault();
		}
		matDissolveST = TargetMaterial.GetVector("_DissolveTex_ST");
		matDissolve2ST = TargetMaterial.GetVector("_Dissolve2Mod");
		matDissolve3ST = TargetMaterial.GetVector("_Dissolve3Mod");
		dissolveOffsetTime = 1f;
		dissolve2OffsetTime = 1f;
		dissolve3OffsetTime = 1f;
		matDissolveStartTime = Time.time - (1f - DissolveAnimationTimeOffset) / DissolveAnimationSpeed;
		matDissolve2StartTime = Time.time - (1f - Dissolve2AnimationTimeOffset) / Dissolve2AnimationSpeed;
		matDissolve3StartTime = Time.time - (1f - Dissolve3AnimationTimeOffset) / Dissolve3AnimationSpeed;
	}
}
