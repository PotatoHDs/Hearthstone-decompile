using UnityEngine;

public class RandomMatFloatOverTime : MonoBehaviour
{
	public float minIntensity = 0.25f;

	public float maxIntensity = 0.5f;

	public float m_timeScale = 1f;

	public string m_property;

	public int m_matIndex;

	public bool m_sync;

	public float m_syncSeed;

	private float random;

	private Renderer m_renderer;

	private void Start()
	{
		if (m_sync)
		{
			random = m_syncSeed;
		}
		else
		{
			random = Random.Range(0f, 65535f);
		}
		m_renderer = GetComponent<Renderer>();
	}

	private void Update()
	{
		float t = Mathf.PerlinNoise(random, Time.time * m_timeScale);
		m_renderer.GetMaterial(m_matIndex).SetFloat(m_property, Mathf.Lerp(minIntensity, maxIntensity, t));
	}
}
