using UnityEngine;

public sealed class InlineImageMaterialQuery : UberTextMaterialQuery
{
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	public InlineImageMaterialQuery()
		: base(1)
	{
		base.Type = UberTextMaterialManager.MaterialType.INLINE_IMAGE;
		m_materialProperties.Add(m_textureProperty);
	}

	public override UberTextMaterialQuery Clone()
	{
		InlineImageMaterialQuery inlineImageMaterialQuery = new InlineImageMaterialQuery();
		CopyPropertiesTo(inlineImageMaterialQuery);
		return inlineImageMaterialQuery;
	}

	public InlineImageMaterialQuery WithTexture(Texture texture)
	{
		m_textureProperty.SetValue(texture);
		return this;
	}
}
