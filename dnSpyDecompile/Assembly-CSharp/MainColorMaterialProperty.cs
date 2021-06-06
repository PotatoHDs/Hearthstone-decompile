using System;
using UnityEngine;

// Token: 0x02000AB1 RID: 2737
public sealed class MainColorMaterialProperty : UberTextMaterialProperty
{
	// Token: 0x0600925C RID: 37468 RVA: 0x002F8996 File Offset: 0x002F6B96
	public MainColorMaterialProperty() : base("MainColor")
	{
	}

	// Token: 0x0600925D RID: 37469 RVA: 0x002F89A4 File Offset: 0x002F6BA4
	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (this.m_enabled)
		{
			MainColorMaterialProperty mainColorMaterialProperty = dest as MainColorMaterialProperty;
			if (mainColorMaterialProperty != null)
			{
				mainColorMaterialProperty.SetValue(this.m_value);
			}
		}
	}

	// Token: 0x0600925E RID: 37470 RVA: 0x002F89CF File Offset: 0x002F6BCF
	public void SetValue(Color value)
	{
		this.m_value = value;
		this.m_enabled = true;
	}

	// Token: 0x0600925F RID: 37471 RVA: 0x002F89DF File Offset: 0x002F6BDF
	public override void DoApplyToMaterial(Material material)
	{
		material.color = this.m_value;
	}

	// Token: 0x06009260 RID: 37472 RVA: 0x002F89F0 File Offset: 0x002F6BF0
	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		MainColorMaterialProperty mainColorMaterialProperty = materialProperty as MainColorMaterialProperty;
		return mainColorMaterialProperty != null && this.m_value == mainColorMaterialProperty.m_value;
	}

	// Token: 0x04007AD3 RID: 31443
	private Color m_value;
}
