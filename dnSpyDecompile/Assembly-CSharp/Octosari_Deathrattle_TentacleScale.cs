using System;
using UnityEngine;

// Token: 0x02000A56 RID: 2646
public class Octosari_Deathrattle_TentacleScale : MonoBehaviour
{
	// Token: 0x06008E9C RID: 36508 RVA: 0x002E06AC File Offset: 0x002DE8AC
	private void Start()
	{
		string name = this.animComponent.clip.name;
		this.animState = this.animComponent[name];
		this.bonesCount = this.bones.Length;
		this.boneWeightSampler = (float)((this.bonesCount < 2) ? 1 : (this.bonesCount - 1));
	}

	// Token: 0x06008E9D RID: 36509 RVA: 0x002E0708 File Offset: 0x002DE908
	private void LateUpdate()
	{
		if (this.animState == null || !this.animComponent.isPlaying || this.animState.time == 0f || this.stretchingByDistanceMul == 0f)
		{
			return;
		}
		float normalizedTime = this.animState.normalizedTime;
		float num = this.deformAnimation.Evaluate(normalizedTime);
		int num2 = 0;
		foreach (GameObject gameObject in this.bones)
		{
			float num3 = this.boneStretchingWeights.Evaluate((float)num2 / this.boneWeightSampler);
			float num4 = this.boneAimingWeights.Evaluate((float)num2 / this.boneWeightSampler);
			Transform transform = gameObject.transform;
			Vector3 vector = transform.localPosition;
			vector += Vector3.Scale(vector, num3 * num * this.boneStretchingMul * this.stretchingByDistanceMul);
			gameObject.transform.localPosition = vector;
			Vector3 up = transform.up;
			Vector3 toDirection = this.tentacleTarget.position - transform.position;
			Quaternion rotation = transform.rotation;
			rotation.SetFromToRotation(up, toDirection);
			gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotation * gameObject.transform.rotation, num * num4);
			num2++;
		}
	}

	// Token: 0x06008E9E RID: 36510 RVA: 0x002E0870 File Offset: 0x002DEA70
	public void Setup()
	{
		float time = Vector3.Distance(this.tentacleTarget.position, base.transform.position);
		this.stretchingByDistanceMul = this.stretchingByTargetDistance.Evaluate(time);
	}

	// Token: 0x040076DE RID: 30430
	public GameObject[] bones;

	// Token: 0x040076DF RID: 30431
	public AnimationCurve boneAimingWeights;

	// Token: 0x040076E0 RID: 30432
	public Vector3 boneStretchingMul = Vector3.up;

	// Token: 0x040076E1 RID: 30433
	public AnimationCurve boneStretchingWeights;

	// Token: 0x040076E2 RID: 30434
	public AnimationCurve stretchingByTargetDistance;

	// Token: 0x040076E3 RID: 30435
	public AnimationCurve deformAnimation;

	// Token: 0x040076E4 RID: 30436
	public Animation animComponent;

	// Token: 0x040076E5 RID: 30437
	public Transform tentacleTarget;

	// Token: 0x040076E6 RID: 30438
	private AnimationState animState;

	// Token: 0x040076E7 RID: 30439
	private int bonesCount;

	// Token: 0x040076E8 RID: 30440
	private float boneWeightSampler;

	// Token: 0x040076E9 RID: 30441
	private float stretchingByDistanceMul;
}
