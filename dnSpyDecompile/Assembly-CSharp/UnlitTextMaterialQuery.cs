using System;
using UnityEngine;

// Token: 0x02000AB6 RID: 2742
public sealed class UnlitTextMaterialQuery : UberTextMaterialQuery
{
	// Token: 0x0600927B RID: 37499 RVA: 0x002F8D74 File Offset: 0x002F6F74
	public UnlitTextMaterialQuery() : base(3)
	{
		base.Type = UberTextMaterialManager.MaterialType.TEXT;
		this.m_materialProperties.Add(this.m_textureProperty);
		this.m_materialProperties.Add(this.m_colorProperty);
		this.m_materialProperties.Add(this.m_lightingBlendProperty);
	}

	// Token: 0x0600927C RID: 37500 RVA: 0x002F8DE8 File Offset: 0x002F6FE8
	public override UberTextMaterialQuery Clone()
	{
		UnlitTextMaterialQuery unlitTextMaterialQuery = new UnlitTextMaterialQuery();
		base.CopyPropertiesTo(unlitTextMaterialQuery);
		return unlitTextMaterialQuery;
	}

	// Token: 0x0600927D RID: 37501 RVA: 0x002F8E03 File Offset: 0x002F7003
	public UnlitTextMaterialQuery WithTexture(Texture texture)
	{
		this.m_textureProperty.SetValue(texture);
		return this;
	}

	// Token: 0x0600927E RID: 37502 RVA: 0x002F8E12 File Offset: 0x002F7012
	public UnlitTextMaterialQuery WithColor(Color color)
	{
		this.m_colorProperty.SetValue(color);
		return this;
	}

	// Token: 0x0600927F RID: 37503 RVA: 0x002F8E21 File Offset: 0x002F7021
	public UnlitTextMaterialQuery WithLightingBlend(float blend)
	{
		this.m_lightingBlendProperty.SetValue(blend);
		return this;
	}

	// Token: 0x04007ADC RID: 31452
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	// Token: 0x04007ADD RID: 31453
	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	// Token: 0x04007ADE RID: 31454
	private FloatMaterialProperty m_lightingBlendProperty = new FloatMaterialProperty("_LightingBlend");
}
