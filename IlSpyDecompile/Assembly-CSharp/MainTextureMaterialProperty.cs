using UnityEngine;

public sealed class MainTextureMaterialProperty : UberTextMaterialProperty
{
	private Texture m_value;

	public MainTextureMaterialProperty()
		: base("MainTexture")
	{
	}

	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (m_enabled)
		{
			(dest as MainTextureMaterialProperty)?.SetValue(m_value);
		}
	}

	public void SetValue(Texture value)
	{
		m_value = value;
		m_enabled = true;
	}

	public override void DoApplyToMaterial(Material material)
	{
		material.mainTexture = m_value;
	}

	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		MainTextureMaterialProperty mainTextureMaterialProperty = materialProperty as MainTextureMaterialProperty;
		if (mainTextureMaterialProperty == null)
		{
			return false;
		}
		return m_value == mainTextureMaterialProperty.m_value;
	}
}
