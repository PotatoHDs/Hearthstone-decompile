using System;
using UnityEngine;

// Token: 0x02000ABC RID: 2748
public sealed class ShadowMaterialQuery : UberTextMaterialQuery
{
	// Token: 0x060092AD RID: 37549 RVA: 0x002F94E4 File Offset: 0x002F76E4
	public ShadowMaterialQuery() : base(4)
	{
		base.Type = UberTextMaterialManager.MaterialType.SHADOW;
		this.m_materialProperties.Add(this.m_textureProperty);
		this.m_materialProperties.Add(this.m_colorProperty);
		this.m_materialProperties.Add(this.m_offsetX);
		this.m_materialProperties.Add(this.m_offsetY);
	}

	// Token: 0x060092AE RID: 37550 RVA: 0x002F957C File Offset: 0x002F777C
	public override UberTextMaterialQuery Clone()
	{
		ShadowMaterialQuery shadowMaterialQuery = new ShadowMaterialQuery();
		base.CopyPropertiesTo(shadowMaterialQuery);
		return shadowMaterialQuery;
	}

	// Token: 0x060092AF RID: 37551 RVA: 0x002F9597 File Offset: 0x002F7797
	public ShadowMaterialQuery WithTexture(Texture texture)
	{
		this.m_textureProperty.SetValue(texture);
		return this;
	}

	// Token: 0x060092B0 RID: 37552 RVA: 0x002F95A6 File Offset: 0x002F77A6
	public ShadowMaterialQuery WithColor(Color color)
	{
		this.m_colorProperty.SetValue(color);
		return this;
	}

	// Token: 0x060092B1 RID: 37553 RVA: 0x002F95B5 File Offset: 0x002F77B5
	public ShadowMaterialQuery WithOffsetX(float x)
	{
		this.m_offsetX.SetValue(x);
		return this;
	}

	// Token: 0x060092B2 RID: 37554 RVA: 0x002F95C4 File Offset: 0x002F77C4
	public ShadowMaterialQuery WithOffsetY(float y)
	{
		this.m_offsetY.SetValue(y);
		return this;
	}

	// Token: 0x04007AFD RID: 31485
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	// Token: 0x04007AFE RID: 31486
	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	// Token: 0x04007AFF RID: 31487
	private FloatMaterialProperty m_offsetX = new FloatMaterialProperty("_OffsetX");

	// Token: 0x04007B00 RID: 31488
	private FloatMaterialProperty m_offsetY = new FloatMaterialProperty("_OffsetY");
}
