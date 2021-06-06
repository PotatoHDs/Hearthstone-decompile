using UnityEngine;

public sealed class TextureMaterialProperty : UberTextMaterialProperty
{
	private Texture m_value;

	public TextureMaterialProperty(string name)
		: base(name)
	{
	}

	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (m_enabled)
		{
			(dest as TextureMaterialProperty)?.SetValue(m_value);
		}
	}

	public void SetValue(Texture value)
	{
		m_value = value;
		m_enabled = true;
	}

	public override void DoApplyToMaterial(Material material)
	{
		material.SetTexture(Name, m_value);
	}

	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		TextureMaterialProperty textureMaterialProperty = materialProperty as TextureMaterialProperty;
		if (textureMaterialProperty == null)
		{
			return false;
		}
		return m_value == textureMaterialProperty.m_value;
	}
}
