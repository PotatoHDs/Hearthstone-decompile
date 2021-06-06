using UnityEngine;

public class SpotlightConstraint : MonoBehaviour
{
	public GameObject SpotlightSource;

	public GameObject SpotlightTarget;

	public GameObject GroundCircle;

	private float maxLeftPosition = 14f;

	private float maxLeftRotationTarget = 50f;

	private float maxLeftCircleScale = 0.25f;

	private float targetMultiplier;

	private float circleMultiplier;

	private Vector3 eulerRotation;

	private Vector3 originalTargetPosition;

	private void OnEnable()
	{
		if ((bool)SpotlightSource && (bool)SpotlightTarget && (bool)GroundCircle)
		{
			targetMultiplier = maxLeftRotationTarget / maxLeftPosition * -1f;
			circleMultiplier = maxLeftCircleScale / maxLeftPosition * -1f;
		}
	}

	private void Update()
	{
		originalTargetPosition = SpotlightTarget.transform.position;
		SpotlightTarget.transform.position = originalTargetPosition;
		eulerRotation = new Vector3(0f, targetMultiplier * SpotlightTarget.transform.position.x, 0f);
		SpotlightTarget.transform.localRotation = Quaternion.Euler(eulerRotation);
		eulerRotation = new Vector3(0f, 0f, 0f);
		GroundCircle.transform.rotation = Quaternion.Euler(eulerRotation);
		GroundCircle.transform.localScale = new Vector3(circleMultiplier * SpotlightTarget.transform.position.x + 1f, 1f, 1f);
	}
}
