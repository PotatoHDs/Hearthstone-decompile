using UnityEngine;

public sealed class FloatMaterialProperty : UberTextMaterialProperty
{
	private float m_value;

	public FloatMaterialProperty(string name)
		: base(name)
	{
	}

	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (m_enabled)
		{
			(dest as FloatMaterialProperty)?.SetValue(m_value);
		}
	}

	public void SetValue(float value)
	{
		m_value = value;
		m_enabled = true;
	}

	public override void DoApplyToMaterial(Material material)
	{
		material.SetFloat(Name, m_value);
	}

	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		FloatMaterialProperty floatMaterialProperty = materialProperty as FloatMaterialProperty;
		if (floatMaterialProperty == null)
		{
			return false;
		}
		return Mathf.Approximately(m_value, floatMaterialProperty.m_value);
	}
}
