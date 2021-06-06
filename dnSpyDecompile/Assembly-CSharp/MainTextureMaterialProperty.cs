using System;
using UnityEngine;

// Token: 0x02000AB3 RID: 2739
public sealed class MainTextureMaterialProperty : UberTextMaterialProperty
{
	// Token: 0x06009266 RID: 37478 RVA: 0x002F8A96 File Offset: 0x002F6C96
	public MainTextureMaterialProperty() : base("MainTexture")
	{
	}

	// Token: 0x06009267 RID: 37479 RVA: 0x002F8AA4 File Offset: 0x002F6CA4
	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (this.m_enabled)
		{
			MainTextureMaterialProperty mainTextureMaterialProperty = dest as MainTextureMaterialProperty;
			if (mainTextureMaterialProperty != null)
			{
				mainTextureMaterialProperty.SetValue(this.m_value);
			}
		}
	}

	// Token: 0x06009268 RID: 37480 RVA: 0x002F8ACF File Offset: 0x002F6CCF
	public void SetValue(Texture value)
	{
		this.m_value = value;
		this.m_enabled = true;
	}

	// Token: 0x06009269 RID: 37481 RVA: 0x002F8ADF File Offset: 0x002F6CDF
	public override void DoApplyToMaterial(Material material)
	{
		material.mainTexture = this.m_value;
	}

	// Token: 0x0600926A RID: 37482 RVA: 0x002F8AF0 File Offset: 0x002F6CF0
	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		MainTextureMaterialProperty mainTextureMaterialProperty = materialProperty as MainTextureMaterialProperty;
		return mainTextureMaterialProperty != null && this.m_value == mainTextureMaterialProperty.m_value;
	}

	// Token: 0x04007AD5 RID: 31445
	private Texture m_value;
}
