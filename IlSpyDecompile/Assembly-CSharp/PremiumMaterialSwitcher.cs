using System.Collections.Generic;
using UnityEngine;

public class PremiumMaterialSwitcher : MonoBehaviour
{
	public Material[] m_PremiumMaterials;

	private List<Material> OrgMaterials;

	private Renderer m_renderer;

	private void Start()
	{
		m_renderer = GetComponent<Renderer>();
	}

	public void SetToPremium(int premium)
	{
		if (premium < 1)
		{
			List<Material> materials = m_renderer.GetMaterials();
			if (materials == null || OrgMaterials == null)
			{
				return;
			}
			for (int i = 0; i < m_PremiumMaterials.Length && i < materials.Count; i++)
			{
				if (!(m_PremiumMaterials[i] == null))
				{
					materials[i] = OrgMaterials[i];
				}
			}
			m_renderer.SetMaterials(materials);
			OrgMaterials = null;
		}
		else
		{
			if (m_PremiumMaterials.Length < 1)
			{
				return;
			}
			if (OrgMaterials == null)
			{
				OrgMaterials = m_renderer.GetMaterials();
			}
			List<Material> materials2 = m_renderer.GetMaterials();
			for (int j = 0; j < m_PremiumMaterials.Length && j < materials2.Count; j++)
			{
				if (!(m_PremiumMaterials[j] == null))
				{
					materials2[j] = m_PremiumMaterials[j];
				}
			}
			m_renderer.SetMaterials(materials2);
		}
	}
}
