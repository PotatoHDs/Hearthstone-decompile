using UnityEngine;

public class RotateAnim : MonoBehaviour
{
	private Quaternion targetRotation;

	private bool gogogo;

	private float timeValue;

	private float timePassed;

	private float startingAngle;

	private void Start()
	{
	}

	private void Update()
	{
		if (gogogo)
		{
			timePassed += Time.deltaTime;
			float num = timePassed;
			float num2 = startingAngle;
			float num3 = num2 - Quaternion.Angle(base.transform.rotation, targetRotation);
			float num4 = timeValue;
			float num5 = num3 * (0f - Mathf.Pow(2f, -10f * num / num4) + 1f) + num2;
			base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, targetRotation, num5 * Time.deltaTime);
			if (Quaternion.Angle(base.transform.rotation, targetRotation) <= Mathf.Epsilon)
			{
				gogogo = false;
				Object.Destroy(this);
			}
		}
	}

	public void SetTargetRotation(Vector3 target, float timeValueInput)
	{
		targetRotation = Quaternion.Euler(target);
		gogogo = true;
		timeValue = timeValueInput;
		startingAngle = Quaternion.Angle(base.transform.rotation, targetRotation);
	}
}
