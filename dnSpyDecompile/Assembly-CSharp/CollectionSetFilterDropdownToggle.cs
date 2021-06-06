using System;
using UnityEngine;

// Token: 0x02000119 RID: 281
public class CollectionSetFilterDropdownToggle : PegUIElement
{
	// Token: 0x06001290 RID: 4752 RVA: 0x00069BE3 File Offset: 0x00067DE3
	public void SetToggleIcon(Texture texture, Vector2 materialOffset)
	{
		Material material = this.m_currentIconQuad.GetMaterial();
		material.SetTexture("_MainTex", texture);
		material.SetTextureOffset("_MainTex", materialOffset);
	}

	// Token: 0x06001291 RID: 4753 RVA: 0x00069C07 File Offset: 0x00067E07
	public void SetEnabledVisual(bool enabled)
	{
		if (this.m_buttonMesh == null)
		{
			return;
		}
		this.m_buttonMesh.GetMaterial().SetFloat("_Desaturate", enabled ? 0f : 1f);
	}

	// Token: 0x06001292 RID: 4754 RVA: 0x00069C3C File Offset: 0x00067E3C
	public void SetButtonBackgroundMaterial()
	{
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			this.m_buttonMeshBackground.SetMaterial(this.m_tavernBrawlBackgroundMaterial);
			return;
		}
		if (SceneMgr.Get().IsInDuelsMode())
		{
			this.m_buttonMeshBackground.SetMaterial(this.m_duelsBackgroundMaterial);
			return;
		}
		this.m_buttonMeshBackground.SetMaterial(this.m_normalBackgroundMaterial);
	}

	// Token: 0x04000BDA RID: 3034
	public MeshRenderer m_currentIconQuad;

	// Token: 0x04000BDB RID: 3035
	public MeshRenderer m_buttonMesh;

	// Token: 0x04000BDC RID: 3036
	public MeshRenderer m_buttonMeshBackground;

	// Token: 0x04000BDD RID: 3037
	public Material m_normalBackgroundMaterial;

	// Token: 0x04000BDE RID: 3038
	public Material m_tavernBrawlBackgroundMaterial;

	// Token: 0x04000BDF RID: 3039
	public Material m_duelsBackgroundMaterial;
}
