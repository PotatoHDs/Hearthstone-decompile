using UnityEngine;

public sealed class OutlineBoldTextMaterialQuery : UberTextMaterialQuery
{
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	private FloatMaterialProperty m_lightingBlendProperty = new FloatMaterialProperty("_LightingBlend");

	private ColorMaterialProperty m_outlineColorProperty = new ColorMaterialProperty("_OutlineColor");

	private FloatMaterialProperty m_outlineOffsetX = new FloatMaterialProperty("_OutlineOffsetX");

	private FloatMaterialProperty m_outlineOffsetY = new FloatMaterialProperty("_OutlineOffsetY");

	private FloatMaterialProperty m_boldOffsetX = new FloatMaterialProperty("_BoldOffsetX");

	private FloatMaterialProperty m_boldOffsetY = new FloatMaterialProperty("_BoldOffsetY");

	public OutlineBoldTextMaterialQuery()
		: base(8)
	{
		base.Type = UberTextMaterialManager.MaterialType.BOLD_OUTLINE;
		m_materialProperties.Add(m_textureProperty);
		m_materialProperties.Add(m_colorProperty);
		m_materialProperties.Add(m_lightingBlendProperty);
		m_materialProperties.Add(m_outlineColorProperty);
		m_materialProperties.Add(m_outlineOffsetX);
		m_materialProperties.Add(m_outlineOffsetY);
		m_materialProperties.Add(m_boldOffsetX);
		m_materialProperties.Add(m_boldOffsetY);
	}

	public override UberTextMaterialQuery Clone()
	{
		OutlineBoldTextMaterialQuery outlineBoldTextMaterialQuery = new OutlineBoldTextMaterialQuery();
		CopyPropertiesTo(outlineBoldTextMaterialQuery);
		return outlineBoldTextMaterialQuery;
	}

	public OutlineBoldTextMaterialQuery WithTexture(Texture texture)
	{
		m_textureProperty.SetValue(texture);
		return this;
	}

	public OutlineBoldTextMaterialQuery WithColor(Color color)
	{
		m_colorProperty.SetValue(color);
		return this;
	}

	public OutlineBoldTextMaterialQuery WithLightingBlend(float blend)
	{
		m_lightingBlendProperty.SetValue(blend);
		return this;
	}

	public OutlineBoldTextMaterialQuery WithOutlineColor(Color color)
	{
		m_outlineColorProperty.SetValue(color);
		return this;
	}

	public OutlineBoldTextMaterialQuery WithOutlineOffsetX(float x)
	{
		m_outlineOffsetX.SetValue(x);
		return this;
	}

	public OutlineBoldTextMaterialQuery WithOutlineOffsetY(float y)
	{
		m_outlineOffsetY.SetValue(y);
		return this;
	}

	public OutlineBoldTextMaterialQuery WithBoldOffsetX(float x)
	{
		m_boldOffsetX.SetValue(x);
		return this;
	}

	public OutlineBoldTextMaterialQuery WithBoldOffsetY(float y)
	{
		m_boldOffsetY.SetValue(y);
		return this;
	}
}
