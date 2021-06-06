using UnityEngine;

public sealed class ShadowMaterialQuery : UberTextMaterialQuery
{
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	private FloatMaterialProperty m_offsetX = new FloatMaterialProperty("_OffsetX");

	private FloatMaterialProperty m_offsetY = new FloatMaterialProperty("_OffsetY");

	public ShadowMaterialQuery()
		: base(4)
	{
		base.Type = UberTextMaterialManager.MaterialType.SHADOW;
		m_materialProperties.Add(m_textureProperty);
		m_materialProperties.Add(m_colorProperty);
		m_materialProperties.Add(m_offsetX);
		m_materialProperties.Add(m_offsetY);
	}

	public override UberTextMaterialQuery Clone()
	{
		ShadowMaterialQuery shadowMaterialQuery = new ShadowMaterialQuery();
		CopyPropertiesTo(shadowMaterialQuery);
		return shadowMaterialQuery;
	}

	public ShadowMaterialQuery WithTexture(Texture texture)
	{
		m_textureProperty.SetValue(texture);
		return this;
	}

	public ShadowMaterialQuery WithColor(Color color)
	{
		m_colorProperty.SetValue(color);
		return this;
	}

	public ShadowMaterialQuery WithOffsetX(float x)
	{
		m_offsetX.SetValue(x);
		return this;
	}

	public ShadowMaterialQuery WithOffsetY(float y)
	{
		m_offsetY.SetValue(y);
		return this;
	}
}
