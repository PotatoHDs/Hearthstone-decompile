using UnityEngine;

[RequireComponent(typeof(Material))]
public class MaterialSoftFlicker : MonoBehaviour
{
	public float minIntensity = 0.25f;

	public float maxIntensity = 0.5f;

	public float m_timeScale = 1f;

	public Color m_color = new Color(1f, 1f, 1f, 1f);

	private float random;

	private Renderer m_renderer;

	private void Start()
	{
		random = Random.Range(0f, 65535f);
		m_renderer = base.gameObject.GetComponent<Renderer>();
	}

	private void Update()
	{
		float t = Mathf.PerlinNoise(random, Time.time * m_timeScale);
		m_renderer.GetMaterial().SetColor("_TintColor", new Color(m_color.r, m_color.g, m_color.b, Mathf.Lerp(minIntensity, maxIntensity, t)));
	}
}
