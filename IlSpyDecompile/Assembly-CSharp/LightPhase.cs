using UnityEngine;

public class LightPhase : MonoBehaviour
{
	public float duration = 1f;

	public float minPower = 3f;

	public float maxPower = 8f;

	public float speed = 0.01f;

	private float targetIntensity;

	private float lastTargetTimestamp;

	private float timeToWaitForNewTarget = 1f;

	public void Update()
	{
		float time = Time.time;
		if (time - lastTargetTimestamp > timeToWaitForNewTarget)
		{
			targetIntensity = Random.Range(minPower, maxPower);
			lastTargetTimestamp = time;
		}
		float num = targetIntensity - GetComponent<Light>().intensity;
		float num2 = num / Mathf.Abs(num);
		if (GetComponent<Light>().intensity != targetIntensity)
		{
			GetComponent<Light>().intensity = GetComponent<Light>().intensity + num2 * speed;
		}
	}
}
