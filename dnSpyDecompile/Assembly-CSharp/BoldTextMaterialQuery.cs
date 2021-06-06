using System;
using UnityEngine;

// Token: 0x02000AB9 RID: 2745
public sealed class BoldTextMaterialQuery : UberTextMaterialQuery
{
	// Token: 0x0600929B RID: 37531 RVA: 0x002F921C File Offset: 0x002F741C
	public BoldTextMaterialQuery() : base(5)
	{
		base.Type = UberTextMaterialManager.MaterialType.BOLD;
		this.m_materialProperties.Add(this.m_textureProperty);
		this.m_materialProperties.Add(this.m_colorProperty);
		this.m_materialProperties.Add(this.m_lightingBlendProperty);
		this.m_materialProperties.Add(this.m_boldOffsetX);
		this.m_materialProperties.Add(this.m_boldOffsetY);
	}

	// Token: 0x0600929C RID: 37532 RVA: 0x002F92D4 File Offset: 0x002F74D4
	public override UberTextMaterialQuery Clone()
	{
		BoldTextMaterialQuery boldTextMaterialQuery = new BoldTextMaterialQuery();
		base.CopyPropertiesTo(boldTextMaterialQuery);
		return boldTextMaterialQuery;
	}

	// Token: 0x0600929D RID: 37533 RVA: 0x002F92EF File Offset: 0x002F74EF
	public BoldTextMaterialQuery WithTexture(Texture texture)
	{
		this.m_textureProperty.SetValue(texture);
		return this;
	}

	// Token: 0x0600929E RID: 37534 RVA: 0x002F92FE File Offset: 0x002F74FE
	public BoldTextMaterialQuery WithColor(Color color)
	{
		this.m_colorProperty.SetValue(color);
		return this;
	}

	// Token: 0x0600929F RID: 37535 RVA: 0x002F930D File Offset: 0x002F750D
	public BoldTextMaterialQuery WithLightingBlend(float blend)
	{
		this.m_lightingBlendProperty.SetValue(blend);
		return this;
	}

	// Token: 0x060092A0 RID: 37536 RVA: 0x002F931C File Offset: 0x002F751C
	public BoldTextMaterialQuery WithBoldOffsetX(float x)
	{
		this.m_boldOffsetX.SetValue(x);
		return this;
	}

	// Token: 0x060092A1 RID: 37537 RVA: 0x002F932B File Offset: 0x002F752B
	public BoldTextMaterialQuery WithBoldOffsetY(float y)
	{
		this.m_boldOffsetY.SetValue(y);
		return this;
	}

	// Token: 0x04007AF1 RID: 31473
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	// Token: 0x04007AF2 RID: 31474
	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	// Token: 0x04007AF3 RID: 31475
	private FloatMaterialProperty m_lightingBlendProperty = new FloatMaterialProperty("_LightingBlend");

	// Token: 0x04007AF4 RID: 31476
	private FloatMaterialProperty m_boldOffsetX = new FloatMaterialProperty("_BoldOffsetX");

	// Token: 0x04007AF5 RID: 31477
	private FloatMaterialProperty m_boldOffsetY = new FloatMaterialProperty("_BoldOffsetY");
}
