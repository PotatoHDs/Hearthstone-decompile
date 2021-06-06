using UnityEngine;

public class Octosari_Deathrattle_TentacleScale : MonoBehaviour
{
	public GameObject[] bones;

	public AnimationCurve boneAimingWeights;

	public Vector3 boneStretchingMul = Vector3.up;

	public AnimationCurve boneStretchingWeights;

	public AnimationCurve stretchingByTargetDistance;

	public AnimationCurve deformAnimation;

	public Animation animComponent;

	public Transform tentacleTarget;

	private AnimationState animState;

	private int bonesCount;

	private float boneWeightSampler;

	private float stretchingByDistanceMul;

	private void Start()
	{
		string text = animComponent.clip.name;
		animState = animComponent[text];
		bonesCount = bones.Length;
		boneWeightSampler = ((bonesCount < 2) ? 1 : (bonesCount - 1));
	}

	private void LateUpdate()
	{
		if (!(animState == null) && animComponent.isPlaying && animState.time != 0f && stretchingByDistanceMul != 0f)
		{
			float normalizedTime = animState.normalizedTime;
			float num = deformAnimation.Evaluate(normalizedTime);
			int num2 = 0;
			GameObject[] array = bones;
			foreach (GameObject gameObject in array)
			{
				float num3 = boneStretchingWeights.Evaluate((float)num2 / boneWeightSampler);
				float num4 = boneAimingWeights.Evaluate((float)num2 / boneWeightSampler);
				Transform transform = gameObject.transform;
				Vector3 localPosition = transform.localPosition;
				localPosition += Vector3.Scale(localPosition, num3 * num * boneStretchingMul * stretchingByDistanceMul);
				gameObject.transform.localPosition = localPosition;
				Vector3 up = transform.up;
				Vector3 toDirection = tentacleTarget.position - transform.position;
				Quaternion rotation = transform.rotation;
				rotation.SetFromToRotation(up, toDirection);
				gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, rotation * gameObject.transform.rotation, num * num4);
				num2++;
			}
		}
	}

	public void Setup()
	{
		float time = Vector3.Distance(tentacleTarget.position, base.transform.position);
		stretchingByDistanceMul = stretchingByTargetDistance.Evaluate(time);
	}
}
