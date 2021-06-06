using UnityEngine;

public class CollectionSetFilterDropdownToggle : PegUIElement
{
	public MeshRenderer m_currentIconQuad;

	public MeshRenderer m_buttonMesh;

	public MeshRenderer m_buttonMeshBackground;

	public Material m_normalBackgroundMaterial;

	public Material m_tavernBrawlBackgroundMaterial;

	public Material m_duelsBackgroundMaterial;

	public void SetToggleIcon(Texture texture, Vector2 materialOffset)
	{
		Material material = m_currentIconQuad.GetMaterial();
		material.SetTexture("_MainTex", texture);
		material.SetTextureOffset("_MainTex", materialOffset);
	}

	public void SetEnabledVisual(bool enabled)
	{
		if (!(m_buttonMesh == null))
		{
			m_buttonMesh.GetMaterial().SetFloat("_Desaturate", enabled ? 0f : 1f);
		}
	}

	public void SetButtonBackgroundMaterial()
	{
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			m_buttonMeshBackground.SetMaterial(m_tavernBrawlBackgroundMaterial);
		}
		else if (SceneMgr.Get().IsInDuelsMode())
		{
			m_buttonMeshBackground.SetMaterial(m_duelsBackgroundMaterial);
		}
		else
		{
			m_buttonMeshBackground.SetMaterial(m_normalBackgroundMaterial);
		}
	}
}
