using System;
using UnityEngine;

// Token: 0x02000ABD RID: 2749
public sealed class InlineImageMaterialQuery : UberTextMaterialQuery
{
	// Token: 0x060092B3 RID: 37555 RVA: 0x002F95D3 File Offset: 0x002F77D3
	public InlineImageMaterialQuery() : base(1)
	{
		base.Type = UberTextMaterialManager.MaterialType.INLINE_IMAGE;
		this.m_materialProperties.Add(this.m_textureProperty);
	}

	// Token: 0x060092B4 RID: 37556 RVA: 0x002F9600 File Offset: 0x002F7800
	public override UberTextMaterialQuery Clone()
	{
		InlineImageMaterialQuery inlineImageMaterialQuery = new InlineImageMaterialQuery();
		base.CopyPropertiesTo(inlineImageMaterialQuery);
		return inlineImageMaterialQuery;
	}

	// Token: 0x060092B5 RID: 37557 RVA: 0x002F961B File Offset: 0x002F781B
	public InlineImageMaterialQuery WithTexture(Texture texture)
	{
		this.m_textureProperty.SetValue(texture);
		return this;
	}

	// Token: 0x04007B01 RID: 31489
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();
}
