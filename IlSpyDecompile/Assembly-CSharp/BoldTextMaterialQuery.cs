using UnityEngine;

public sealed class BoldTextMaterialQuery : UberTextMaterialQuery
{
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	private FloatMaterialProperty m_lightingBlendProperty = new FloatMaterialProperty("_LightingBlend");

	private FloatMaterialProperty m_boldOffsetX = new FloatMaterialProperty("_BoldOffsetX");

	private FloatMaterialProperty m_boldOffsetY = new FloatMaterialProperty("_BoldOffsetY");

	public BoldTextMaterialQuery()
		: base(5)
	{
		base.Type = UberTextMaterialManager.MaterialType.BOLD;
		m_materialProperties.Add(m_textureProperty);
		m_materialProperties.Add(m_colorProperty);
		m_materialProperties.Add(m_lightingBlendProperty);
		m_materialProperties.Add(m_boldOffsetX);
		m_materialProperties.Add(m_boldOffsetY);
	}

	public override UberTextMaterialQuery Clone()
	{
		BoldTextMaterialQuery boldTextMaterialQuery = new BoldTextMaterialQuery();
		CopyPropertiesTo(boldTextMaterialQuery);
		return boldTextMaterialQuery;
	}

	public BoldTextMaterialQuery WithTexture(Texture texture)
	{
		m_textureProperty.SetValue(texture);
		return this;
	}

	public BoldTextMaterialQuery WithColor(Color color)
	{
		m_colorProperty.SetValue(color);
		return this;
	}

	public BoldTextMaterialQuery WithLightingBlend(float blend)
	{
		m_lightingBlendProperty.SetValue(blend);
		return this;
	}

	public BoldTextMaterialQuery WithBoldOffsetX(float x)
	{
		m_boldOffsetX.SetValue(x);
		return this;
	}

	public BoldTextMaterialQuery WithBoldOffsetY(float y)
	{
		m_boldOffsetY.SetValue(y);
		return this;
	}
}
