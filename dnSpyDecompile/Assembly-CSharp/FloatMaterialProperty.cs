using System;
using UnityEngine;

// Token: 0x02000AAF RID: 2735
public sealed class FloatMaterialProperty : UberTextMaterialProperty
{
	// Token: 0x06009252 RID: 37458 RVA: 0x002F8897 File Offset: 0x002F6A97
	public FloatMaterialProperty(string name) : base(name)
	{
	}

	// Token: 0x06009253 RID: 37459 RVA: 0x002F88A0 File Offset: 0x002F6AA0
	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (this.m_enabled)
		{
			FloatMaterialProperty floatMaterialProperty = dest as FloatMaterialProperty;
			if (floatMaterialProperty != null)
			{
				floatMaterialProperty.SetValue(this.m_value);
			}
		}
	}

	// Token: 0x06009254 RID: 37460 RVA: 0x002F88CB File Offset: 0x002F6ACB
	public void SetValue(float value)
	{
		this.m_value = value;
		this.m_enabled = true;
	}

	// Token: 0x06009255 RID: 37461 RVA: 0x002F88DB File Offset: 0x002F6ADB
	public override void DoApplyToMaterial(Material material)
	{
		material.SetFloat(this.Name, this.m_value);
	}

	// Token: 0x06009256 RID: 37462 RVA: 0x002F88F0 File Offset: 0x002F6AF0
	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		FloatMaterialProperty floatMaterialProperty = materialProperty as FloatMaterialProperty;
		return floatMaterialProperty != null && Mathf.Approximately(this.m_value, floatMaterialProperty.m_value);
	}

	// Token: 0x04007AD1 RID: 31441
	private float m_value;
}
