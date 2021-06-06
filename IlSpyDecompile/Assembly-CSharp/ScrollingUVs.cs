using UnityEngine;

public class ScrollingUVs : MonoBehaviour
{
	public int materialIndex;

	public Vector2 uvAnimationRate = new Vector2(1f, 1f);

	private Material m_material;

	private Vector2 m_offset = Vector2.zero;

	private Renderer m_renderer;

	private void Start()
	{
		m_renderer = GetComponent<Renderer>();
		m_material = m_renderer.GetMaterial(materialIndex);
	}

	private void LateUpdate()
	{
		if (m_renderer.enabled)
		{
			if (m_material == null)
			{
				m_material = m_renderer.GetMaterial(materialIndex);
			}
			m_offset += uvAnimationRate * Time.deltaTime;
			m_material.SetTextureOffset("_MainTex", m_offset);
		}
	}
}
