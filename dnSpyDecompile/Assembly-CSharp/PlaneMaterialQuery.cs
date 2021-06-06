using System;
using UnityEngine;

// Token: 0x02000ABB RID: 2747
public sealed class PlaneMaterialQuery : UberTextMaterialQuery
{
	// Token: 0x060092AA RID: 37546 RVA: 0x002F9489 File Offset: 0x002F7689
	public PlaneMaterialQuery() : base(1)
	{
		base.Type = UberTextMaterialManager.MaterialType.PLANE;
		this.m_materialProperties.Add(this.m_textureProperty);
	}

	// Token: 0x060092AB RID: 37547 RVA: 0x002F94B8 File Offset: 0x002F76B8
	public override UberTextMaterialQuery Clone()
	{
		PlaneMaterialQuery planeMaterialQuery = new PlaneMaterialQuery();
		base.CopyPropertiesTo(planeMaterialQuery);
		return planeMaterialQuery;
	}

	// Token: 0x060092AC RID: 37548 RVA: 0x002F94D3 File Offset: 0x002F76D3
	public PlaneMaterialQuery WithTexture(Texture texture)
	{
		this.m_textureProperty.SetValue(texture);
		return this;
	}

	// Token: 0x04007AFC RID: 31484
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();
}
