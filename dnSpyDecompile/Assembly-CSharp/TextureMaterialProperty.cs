using System;
using UnityEngine;

// Token: 0x02000AB2 RID: 2738
public sealed class TextureMaterialProperty : UberTextMaterialProperty
{
	// Token: 0x06009261 RID: 37473 RVA: 0x002F8897 File Offset: 0x002F6A97
	public TextureMaterialProperty(string name) : base(name)
	{
	}

	// Token: 0x06009262 RID: 37474 RVA: 0x002F8A1C File Offset: 0x002F6C1C
	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (this.m_enabled)
		{
			TextureMaterialProperty textureMaterialProperty = dest as TextureMaterialProperty;
			if (textureMaterialProperty != null)
			{
				textureMaterialProperty.SetValue(this.m_value);
			}
		}
	}

	// Token: 0x06009263 RID: 37475 RVA: 0x002F8A47 File Offset: 0x002F6C47
	public void SetValue(Texture value)
	{
		this.m_value = value;
		this.m_enabled = true;
	}

	// Token: 0x06009264 RID: 37476 RVA: 0x002F8A57 File Offset: 0x002F6C57
	public override void DoApplyToMaterial(Material material)
	{
		material.SetTexture(this.Name, this.m_value);
	}

	// Token: 0x06009265 RID: 37477 RVA: 0x002F8A6C File Offset: 0x002F6C6C
	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		TextureMaterialProperty textureMaterialProperty = materialProperty as TextureMaterialProperty;
		return textureMaterialProperty != null && this.m_value == textureMaterialProperty.m_value;
	}

	// Token: 0x04007AD4 RID: 31444
	private Texture m_value;
}
