using UnityEngine;

public class floatyObj : MonoBehaviour
{
	public float frequencyMin = 0.0001f;

	public float frequencyMax = 0.001f;

	public float magnitude = 0.0001f;

	private float m_interval;

	private void Start()
	{
		m_interval = Random.Range(frequencyMin, frequencyMax);
	}

	private void Update()
	{
		float num = Mathf.Sin(Time.time * m_interval) * magnitude;
		Vector3 vector = new Vector3(num, num, num);
		base.transform.position += vector;
		base.transform.eulerAngles += vector;
	}
}
