using UnityEngine;

public class TransitionPulse : MonoBehaviour
{
	public float frequencyMin = 0.0001f;

	public float frequencyMax = 1f;

	public float magnitude = 0.0001f;

	private float m_interval;

	private void Start()
	{
		m_interval = Random.Range(frequencyMin, frequencyMax);
	}

	private void Update()
	{
		float value = Mathf.Sin(Time.time * m_interval) * magnitude;
		base.gameObject.GetComponent<Renderer>().GetMaterial().SetFloat("_Transistion", value);
	}
}
