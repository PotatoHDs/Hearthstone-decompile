using System;
using UnityEngine;

// Token: 0x02000AB7 RID: 2743
public sealed class OutlineTextMaterialQuery : UberTextMaterialQuery
{
	// Token: 0x1700086B RID: 2155
	// (get) Token: 0x06009280 RID: 37504 RVA: 0x002F8E30 File Offset: 0x002F7030
	// (set) Token: 0x06009281 RID: 37505 RVA: 0x002F8E38 File Offset: 0x002F7038
	public bool RichTextEnabled { get; private set; }

	// Token: 0x1700086C RID: 2156
	// (get) Token: 0x06009282 RID: 37506 RVA: 0x002F8E41 File Offset: 0x002F7041
	// (set) Token: 0x06009283 RID: 37507 RVA: 0x002F8E49 File Offset: 0x002F7049
	public Locale Locale { get; private set; }

	// Token: 0x06009284 RID: 37508 RVA: 0x002F8E54 File Offset: 0x002F7054
	public OutlineTextMaterialQuery() : base(8)
	{
		base.Type = UberTextMaterialManager.MaterialType.TEXT_OUTLINE;
		this.m_materialProperties.Add(this.m_textureProperty);
		this.m_materialProperties.Add(this.m_colorProperty);
		this.m_materialProperties.Add(this.m_lightingBlendProperty);
		this.m_materialProperties.Add(this.m_outlineColorProperty);
		this.m_materialProperties.Add(this.m_outlineOffsetX);
		this.m_materialProperties.Add(this.m_outlineOffsetY);
		this.m_materialProperties.Add(this.m_texelSizeX);
		this.m_materialProperties.Add(this.m_texelSizeY);
	}

	// Token: 0x06009285 RID: 37509 RVA: 0x002F8F70 File Offset: 0x002F7170
	public override UberTextMaterialQuery Clone()
	{
		OutlineTextMaterialQuery outlineTextMaterialQuery = new OutlineTextMaterialQuery();
		base.CopyPropertiesTo(outlineTextMaterialQuery);
		outlineTextMaterialQuery.RichTextEnabled = this.RichTextEnabled;
		outlineTextMaterialQuery.Locale = this.Locale;
		return outlineTextMaterialQuery;
	}

	// Token: 0x06009286 RID: 37510 RVA: 0x002F8FA4 File Offset: 0x002F71A4
	public override bool Equals(UberTextMaterialQuery query)
	{
		OutlineTextMaterialQuery outlineTextMaterialQuery = query as OutlineTextMaterialQuery;
		return (outlineTextMaterialQuery == null || (outlineTextMaterialQuery.RichTextEnabled == this.RichTextEnabled && outlineTextMaterialQuery.Locale == this.Locale)) && base.Equals(query);
	}

	// Token: 0x06009287 RID: 37511 RVA: 0x002F8FE0 File Offset: 0x002F71E0
	public OutlineTextMaterialQuery WithTexture(Texture texture)
	{
		this.m_textureProperty.SetValue(texture);
		return this;
	}

	// Token: 0x06009288 RID: 37512 RVA: 0x002F8FEF File Offset: 0x002F71EF
	public OutlineTextMaterialQuery WithColor(Color color)
	{
		this.m_colorProperty.SetValue(color);
		return this;
	}

	// Token: 0x06009289 RID: 37513 RVA: 0x002F8FFE File Offset: 0x002F71FE
	public OutlineTextMaterialQuery WithLightingBlend(float blend)
	{
		this.m_lightingBlendProperty.SetValue(blend);
		return this;
	}

	// Token: 0x0600928A RID: 37514 RVA: 0x002F900D File Offset: 0x002F720D
	public OutlineTextMaterialQuery WithOutlineColor(Color color)
	{
		this.m_outlineColorProperty.SetValue(color);
		return this;
	}

	// Token: 0x0600928B RID: 37515 RVA: 0x002F901C File Offset: 0x002F721C
	public OutlineTextMaterialQuery WithOutlineOffsetX(float x)
	{
		this.m_outlineOffsetX.SetValue(x);
		return this;
	}

	// Token: 0x0600928C RID: 37516 RVA: 0x002F902B File Offset: 0x002F722B
	public OutlineTextMaterialQuery WithOutlineOffsetY(float y)
	{
		this.m_outlineOffsetY.SetValue(y);
		return this;
	}

	// Token: 0x0600928D RID: 37517 RVA: 0x002F903A File Offset: 0x002F723A
	public OutlineTextMaterialQuery WithTexelSizeX(float x)
	{
		this.m_texelSizeX.SetValue(x);
		return this;
	}

	// Token: 0x0600928E RID: 37518 RVA: 0x002F9049 File Offset: 0x002F7249
	public OutlineTextMaterialQuery WithTexelSizeY(float y)
	{
		this.m_texelSizeY.SetValue(y);
		return this;
	}

	// Token: 0x0600928F RID: 37519 RVA: 0x002F9058 File Offset: 0x002F7258
	public OutlineTextMaterialQuery WithRichText(bool richText)
	{
		this.RichTextEnabled = richText;
		return this;
	}

	// Token: 0x06009290 RID: 37520 RVA: 0x002F9062 File Offset: 0x002F7262
	public OutlineTextMaterialQuery WithLocale(Locale locale)
	{
		this.Locale = locale;
		return this;
	}

	// Token: 0x04007ADF RID: 31455
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	// Token: 0x04007AE0 RID: 31456
	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	// Token: 0x04007AE1 RID: 31457
	private FloatMaterialProperty m_lightingBlendProperty = new FloatMaterialProperty("_LightingBlend");

	// Token: 0x04007AE2 RID: 31458
	private ColorMaterialProperty m_outlineColorProperty = new ColorMaterialProperty("_OutlineColor");

	// Token: 0x04007AE3 RID: 31459
	private FloatMaterialProperty m_outlineOffsetX = new FloatMaterialProperty("_OutlineOffsetX");

	// Token: 0x04007AE4 RID: 31460
	private FloatMaterialProperty m_outlineOffsetY = new FloatMaterialProperty("_OutlineOffsetY");

	// Token: 0x04007AE5 RID: 31461
	private FloatMaterialProperty m_texelSizeX = new FloatMaterialProperty("_TexelSizeX");

	// Token: 0x04007AE6 RID: 31462
	private FloatMaterialProperty m_texelSizeY = new FloatMaterialProperty("_TexelSizeY");
}
