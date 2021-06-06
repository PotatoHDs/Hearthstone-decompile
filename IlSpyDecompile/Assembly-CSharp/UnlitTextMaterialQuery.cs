using UnityEngine;

public sealed class UnlitTextMaterialQuery : UberTextMaterialQuery
{
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	private FloatMaterialProperty m_lightingBlendProperty = new FloatMaterialProperty("_LightingBlend");

	public UnlitTextMaterialQuery()
		: base(3)
	{
		base.Type = UberTextMaterialManager.MaterialType.TEXT;
		m_materialProperties.Add(m_textureProperty);
		m_materialProperties.Add(m_colorProperty);
		m_materialProperties.Add(m_lightingBlendProperty);
	}

	public override UberTextMaterialQuery Clone()
	{
		UnlitTextMaterialQuery unlitTextMaterialQuery = new UnlitTextMaterialQuery();
		CopyPropertiesTo(unlitTextMaterialQuery);
		return unlitTextMaterialQuery;
	}

	public UnlitTextMaterialQuery WithTexture(Texture texture)
	{
		m_textureProperty.SetValue(texture);
		return this;
	}

	public UnlitTextMaterialQuery WithColor(Color color)
	{
		m_colorProperty.SetValue(color);
		return this;
	}

	public UnlitTextMaterialQuery WithLightingBlend(float blend)
	{
		m_lightingBlendProperty.SetValue(blend);
		return this;
	}
}
