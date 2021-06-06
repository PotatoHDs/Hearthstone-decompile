using UnityEngine;

public sealed class ColorMaterialProperty : UberTextMaterialProperty
{
	private Color m_value;

	public ColorMaterialProperty(string name)
		: base(name)
	{
	}

	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (m_enabled)
		{
			(dest as ColorMaterialProperty)?.SetValue(m_value);
		}
	}

	public void SetValue(Color value)
	{
		m_value = value;
		m_enabled = true;
	}

	public override void DoApplyToMaterial(Material material)
	{
		material.SetColor(Name, m_value);
	}

	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		ColorMaterialProperty colorMaterialProperty = materialProperty as ColorMaterialProperty;
		if (colorMaterialProperty == null)
		{
			return false;
		}
		return m_value == colorMaterialProperty.m_value;
	}
}
