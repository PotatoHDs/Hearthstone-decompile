using System;
using UnityEngine;

// Token: 0x02000AB8 RID: 2744
public sealed class OutlineBoldTextMaterialQuery : UberTextMaterialQuery
{
	// Token: 0x06009291 RID: 37521 RVA: 0x002F906C File Offset: 0x002F726C
	public OutlineBoldTextMaterialQuery() : base(8)
	{
		base.Type = UberTextMaterialManager.MaterialType.BOLD_OUTLINE;
		this.m_materialProperties.Add(this.m_textureProperty);
		this.m_materialProperties.Add(this.m_colorProperty);
		this.m_materialProperties.Add(this.m_lightingBlendProperty);
		this.m_materialProperties.Add(this.m_outlineColorProperty);
		this.m_materialProperties.Add(this.m_outlineOffsetX);
		this.m_materialProperties.Add(this.m_outlineOffsetY);
		this.m_materialProperties.Add(this.m_boldOffsetX);
		this.m_materialProperties.Add(this.m_boldOffsetY);
	}

	// Token: 0x06009292 RID: 37522 RVA: 0x002F9188 File Offset: 0x002F7388
	public override UberTextMaterialQuery Clone()
	{
		OutlineBoldTextMaterialQuery outlineBoldTextMaterialQuery = new OutlineBoldTextMaterialQuery();
		base.CopyPropertiesTo(outlineBoldTextMaterialQuery);
		return outlineBoldTextMaterialQuery;
	}

	// Token: 0x06009293 RID: 37523 RVA: 0x002F91A3 File Offset: 0x002F73A3
	public OutlineBoldTextMaterialQuery WithTexture(Texture texture)
	{
		this.m_textureProperty.SetValue(texture);
		return this;
	}

	// Token: 0x06009294 RID: 37524 RVA: 0x002F91B2 File Offset: 0x002F73B2
	public OutlineBoldTextMaterialQuery WithColor(Color color)
	{
		this.m_colorProperty.SetValue(color);
		return this;
	}

	// Token: 0x06009295 RID: 37525 RVA: 0x002F91C1 File Offset: 0x002F73C1
	public OutlineBoldTextMaterialQuery WithLightingBlend(float blend)
	{
		this.m_lightingBlendProperty.SetValue(blend);
		return this;
	}

	// Token: 0x06009296 RID: 37526 RVA: 0x002F91D0 File Offset: 0x002F73D0
	public OutlineBoldTextMaterialQuery WithOutlineColor(Color color)
	{
		this.m_outlineColorProperty.SetValue(color);
		return this;
	}

	// Token: 0x06009297 RID: 37527 RVA: 0x002F91DF File Offset: 0x002F73DF
	public OutlineBoldTextMaterialQuery WithOutlineOffsetX(float x)
	{
		this.m_outlineOffsetX.SetValue(x);
		return this;
	}

	// Token: 0x06009298 RID: 37528 RVA: 0x002F91EE File Offset: 0x002F73EE
	public OutlineBoldTextMaterialQuery WithOutlineOffsetY(float y)
	{
		this.m_outlineOffsetY.SetValue(y);
		return this;
	}

	// Token: 0x06009299 RID: 37529 RVA: 0x002F91FD File Offset: 0x002F73FD
	public OutlineBoldTextMaterialQuery WithBoldOffsetX(float x)
	{
		this.m_boldOffsetX.SetValue(x);
		return this;
	}

	// Token: 0x0600929A RID: 37530 RVA: 0x002F920C File Offset: 0x002F740C
	public OutlineBoldTextMaterialQuery WithBoldOffsetY(float y)
	{
		this.m_boldOffsetY.SetValue(y);
		return this;
	}

	// Token: 0x04007AE9 RID: 31465
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	// Token: 0x04007AEA RID: 31466
	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	// Token: 0x04007AEB RID: 31467
	private FloatMaterialProperty m_lightingBlendProperty = new FloatMaterialProperty("_LightingBlend");

	// Token: 0x04007AEC RID: 31468
	private ColorMaterialProperty m_outlineColorProperty = new ColorMaterialProperty("_OutlineColor");

	// Token: 0x04007AED RID: 31469
	private FloatMaterialProperty m_outlineOffsetX = new FloatMaterialProperty("_OutlineOffsetX");

	// Token: 0x04007AEE RID: 31470
	private FloatMaterialProperty m_outlineOffsetY = new FloatMaterialProperty("_OutlineOffsetY");

	// Token: 0x04007AEF RID: 31471
	private FloatMaterialProperty m_boldOffsetX = new FloatMaterialProperty("_BoldOffsetX");

	// Token: 0x04007AF0 RID: 31472
	private FloatMaterialProperty m_boldOffsetY = new FloatMaterialProperty("_BoldOffsetY");
}
