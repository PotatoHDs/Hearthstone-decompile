using UnityEngine;

public sealed class PlaneMaterialQuery : UberTextMaterialQuery
{
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	public PlaneMaterialQuery()
		: base(1)
	{
		base.Type = UberTextMaterialManager.MaterialType.PLANE;
		m_materialProperties.Add(m_textureProperty);
	}

	public override UberTextMaterialQuery Clone()
	{
		PlaneMaterialQuery planeMaterialQuery = new PlaneMaterialQuery();
		CopyPropertiesTo(planeMaterialQuery);
		return planeMaterialQuery;
	}

	public PlaneMaterialQuery WithTexture(Texture texture)
	{
		m_textureProperty.SetValue(texture);
		return this;
	}
}
