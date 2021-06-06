using System;
using UnityEngine;

// Token: 0x02000AB0 RID: 2736
public sealed class ColorMaterialProperty : UberTextMaterialProperty
{
	// Token: 0x06009257 RID: 37463 RVA: 0x002F8897 File Offset: 0x002F6A97
	public ColorMaterialProperty(string name) : base(name)
	{
	}

	// Token: 0x06009258 RID: 37464 RVA: 0x002F891C File Offset: 0x002F6B1C
	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (this.m_enabled)
		{
			ColorMaterialProperty colorMaterialProperty = dest as ColorMaterialProperty;
			if (colorMaterialProperty != null)
			{
				colorMaterialProperty.SetValue(this.m_value);
			}
		}
	}

	// Token: 0x06009259 RID: 37465 RVA: 0x002F8947 File Offset: 0x002F6B47
	public void SetValue(Color value)
	{
		this.m_value = value;
		this.m_enabled = true;
	}

	// Token: 0x0600925A RID: 37466 RVA: 0x002F8957 File Offset: 0x002F6B57
	public override void DoApplyToMaterial(Material material)
	{
		material.SetColor(this.Name, this.m_value);
	}

	// Token: 0x0600925B RID: 37467 RVA: 0x002F896C File Offset: 0x002F6B6C
	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		ColorMaterialProperty colorMaterialProperty = materialProperty as ColorMaterialProperty;
		return colorMaterialProperty != null && this.m_value == colorMaterialProperty.m_value;
	}

	// Token: 0x04007AD2 RID: 31442
	private Color m_value;
}
