using UnityEngine;

public class DisableMesh_ColorAlpha : MonoBehaviour
{
	private Material m_material;

	private Renderer m_renderer;

	private void Start()
	{
		m_renderer = GetComponent<Renderer>();
		m_material = m_renderer.GetMaterial();
		if (m_material == null)
		{
			base.enabled = false;
		}
		if (!m_material.HasProperty("_Color"))
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		if (m_material.color.a == 0f)
		{
			m_renderer.enabled = false;
		}
		else
		{
			m_renderer.enabled = true;
		}
	}
}
