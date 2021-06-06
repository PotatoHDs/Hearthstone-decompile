using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A63 RID: 2659
public class PremiumMaterialSwitcher : MonoBehaviour
{
	// Token: 0x06008ED0 RID: 36560 RVA: 0x002E19CC File Offset: 0x002DFBCC
	private void Start()
	{
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06008ED1 RID: 36561 RVA: 0x002E19DC File Offset: 0x002DFBDC
	public void SetToPremium(int premium)
	{
		if (premium < 1)
		{
			List<Material> materials = this.m_renderer.GetMaterials();
			if (materials == null || this.OrgMaterials == null)
			{
				return;
			}
			int num = 0;
			while (num < this.m_PremiumMaterials.Length && num < materials.Count)
			{
				if (!(this.m_PremiumMaterials[num] == null))
				{
					materials[num] = this.OrgMaterials[num];
				}
				num++;
			}
			this.m_renderer.SetMaterials(materials);
			this.OrgMaterials = null;
			return;
		}
		else
		{
			if (this.m_PremiumMaterials.Length < 1)
			{
				return;
			}
			if (this.OrgMaterials == null)
			{
				this.OrgMaterials = this.m_renderer.GetMaterials();
			}
			List<Material> materials2 = this.m_renderer.GetMaterials();
			int num2 = 0;
			while (num2 < this.m_PremiumMaterials.Length && num2 < materials2.Count)
			{
				if (!(this.m_PremiumMaterials[num2] == null))
				{
					materials2[num2] = this.m_PremiumMaterials[num2];
				}
				num2++;
			}
			this.m_renderer.SetMaterials(materials2);
			return;
		}
	}

	// Token: 0x04007728 RID: 30504
	public Material[] m_PremiumMaterials;

	// Token: 0x04007729 RID: 30505
	private List<Material> OrgMaterials;

	// Token: 0x0400772A RID: 30506
	private Renderer m_renderer;
}
