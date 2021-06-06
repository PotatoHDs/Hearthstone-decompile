using UnityEngine;

public sealed class AntiAlisaingMaterialQuery : UberTextMaterialQuery
{
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	private FloatMaterialProperty m_lightingBlendProperty = new FloatMaterialProperty("_LightingBlend");

	private FloatMaterialProperty m_offsetX = new FloatMaterialProperty("_OffsetX");

	private FloatMaterialProperty m_offsetY = new FloatMaterialProperty("_OffsetY");

	private FloatMaterialProperty m_edge = new FloatMaterialProperty("_Edge");

	public AntiAlisaingMaterialQuery()
		: base(5)
	{
		base.Type = UberTextMaterialManager.MaterialType.TEXT_ANTIALIASING;
		m_materialProperties.Add(m_textureProperty);
		m_materialProperties.Add(m_colorProperty);
		m_materialProperties.Add(m_lightingBlendProperty);
		m_materialProperties.Add(m_offsetX);
		m_materialProperties.Add(m_offsetY);
		m_materialProperties.Add(m_edge);
	}

	public override UberTextMaterialQuery Clone()
	{
		AntiAlisaingMaterialQuery antiAlisaingMaterialQuery = new AntiAlisaingMaterialQuery();
		CopyPropertiesTo(antiAlisaingMaterialQuery);
		return antiAlisaingMaterialQuery;
	}

	public AntiAlisaingMaterialQuery WithTexture(Texture texture)
	{
		m_textureProperty.SetValue(texture);
		return this;
	}

	public AntiAlisaingMaterialQuery WithColor(Color color)
	{
		m_colorProperty.SetValue(color);
		return this;
	}

	public AntiAlisaingMaterialQuery WithLightingBlend(float blend)
	{
		m_lightingBlendProperty.SetValue(blend);
		return this;
	}

	public AntiAlisaingMaterialQuery WithOffsetX(float x)
	{
		m_offsetX.SetValue(x);
		return this;
	}

	public AntiAlisaingMaterialQuery WithOffsetY(float y)
	{
		m_offsetY.SetValue(y);
		return this;
	}

	public AntiAlisaingMaterialQuery WithEdge(float y)
	{
		m_edge.SetValue(y);
		return this;
	}
}
