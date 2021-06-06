using System;
using UnityEngine;

// Token: 0x02000ABA RID: 2746
public sealed class AntiAlisaingMaterialQuery : UberTextMaterialQuery
{
	// Token: 0x060092A2 RID: 37538 RVA: 0x002F933C File Offset: 0x002F753C
	public AntiAlisaingMaterialQuery() : base(5)
	{
		base.Type = UberTextMaterialManager.MaterialType.TEXT_ANTIALIASING;
		this.m_materialProperties.Add(this.m_textureProperty);
		this.m_materialProperties.Add(this.m_colorProperty);
		this.m_materialProperties.Add(this.m_lightingBlendProperty);
		this.m_materialProperties.Add(this.m_offsetX);
		this.m_materialProperties.Add(this.m_offsetY);
		this.m_materialProperties.Add(this.m_edge);
	}

	// Token: 0x060092A3 RID: 37539 RVA: 0x002F9414 File Offset: 0x002F7614
	public override UberTextMaterialQuery Clone()
	{
		AntiAlisaingMaterialQuery antiAlisaingMaterialQuery = new AntiAlisaingMaterialQuery();
		base.CopyPropertiesTo(antiAlisaingMaterialQuery);
		return antiAlisaingMaterialQuery;
	}

	// Token: 0x060092A4 RID: 37540 RVA: 0x002F942F File Offset: 0x002F762F
	public AntiAlisaingMaterialQuery WithTexture(Texture texture)
	{
		this.m_textureProperty.SetValue(texture);
		return this;
	}

	// Token: 0x060092A5 RID: 37541 RVA: 0x002F943E File Offset: 0x002F763E
	public AntiAlisaingMaterialQuery WithColor(Color color)
	{
		this.m_colorProperty.SetValue(color);
		return this;
	}

	// Token: 0x060092A6 RID: 37542 RVA: 0x002F944D File Offset: 0x002F764D
	public AntiAlisaingMaterialQuery WithLightingBlend(float blend)
	{
		this.m_lightingBlendProperty.SetValue(blend);
		return this;
	}

	// Token: 0x060092A7 RID: 37543 RVA: 0x002F945C File Offset: 0x002F765C
	public AntiAlisaingMaterialQuery WithOffsetX(float x)
	{
		this.m_offsetX.SetValue(x);
		return this;
	}

	// Token: 0x060092A8 RID: 37544 RVA: 0x002F946B File Offset: 0x002F766B
	public AntiAlisaingMaterialQuery WithOffsetY(float y)
	{
		this.m_offsetY.SetValue(y);
		return this;
	}

	// Token: 0x060092A9 RID: 37545 RVA: 0x002F947A File Offset: 0x002F767A
	public AntiAlisaingMaterialQuery WithEdge(float y)
	{
		this.m_edge.SetValue(y);
		return this;
	}

	// Token: 0x04007AF6 RID: 31478
	private MainTextureMaterialProperty m_textureProperty = new MainTextureMaterialProperty();

	// Token: 0x04007AF7 RID: 31479
	private MainColorMaterialProperty m_colorProperty = new MainColorMaterialProperty();

	// Token: 0x04007AF8 RID: 31480
	private FloatMaterialProperty m_lightingBlendProperty = new FloatMaterialProperty("_LightingBlend");

	// Token: 0x04007AF9 RID: 31481
	private FloatMaterialProperty m_offsetX = new FloatMaterialProperty("_OffsetX");

	// Token: 0x04007AFA RID: 31482
	private FloatMaterialProperty m_offsetY = new FloatMaterialProperty("_OffsetY");

	// Token: 0x04007AFB RID: 31483
	private FloatMaterialProperty m_edge = new FloatMaterialProperty("_Edge");
}
