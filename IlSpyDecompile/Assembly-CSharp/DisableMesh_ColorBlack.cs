using UnityEngine;

public class DisableMesh_ColorBlack : MonoBehaviour
{
	private Material m_material;

	private bool m_tintColor;

	private Color m_color = Color.black;

	private Renderer m_renderer;

	private void Start()
	{
		m_renderer = GetComponent<Renderer>();
		m_material = m_renderer.GetMaterial();
		if (m_material == null)
		{
			base.enabled = false;
		}
		if (!m_material.HasProperty("_Color") && !m_material.HasProperty("_TintColor"))
		{
			base.enabled = false;
		}
		if (m_material.HasProperty("_TintColor"))
		{
			m_tintColor = true;
		}
	}

	private void Update()
	{
		if (m_tintColor)
		{
			m_color = m_material.GetColor("_TintColor");
		}
		else
		{
			m_color = m_material.color;
		}
		if (m_color.r < 0.01f && m_color.g < 0.01f && m_color.b < 0.01f)
		{
			m_renderer.enabled = false;
		}
		else
		{
			m_renderer.enabled = true;
		}
	}
}
