using UnityEngine;

public sealed class MainColorMaterialProperty : UberTextMaterialProperty
{
	private Color m_value;

	public MainColorMaterialProperty()
		: base("MainColor")
	{
	}

	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (m_enabled)
		{
			(dest as MainColorMaterialProperty)?.SetValue(m_value);
		}
	}

	public void SetValue(Color value)
	{
		m_value = value;
		m_enabled = true;
	}

	public override void DoApplyToMaterial(Material material)
	{
		material.color = m_value;
	}

	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		MainColorMaterialProperty mainColorMaterialProperty = materialProperty as MainColorMaterialProperty;
		if (mainColorMaterialProperty == null)
		{
			return false;
		}
		return m_value == mainColorMaterialProperty.m_value;
	}
}
