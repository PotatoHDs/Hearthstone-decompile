using System;
using UnityEngine;

// Token: 0x02000AB4 RID: 2740
public sealed class IncrementRenderQueueMaterialProperty : UberTextMaterialProperty
{
	// Token: 0x0600926B RID: 37483 RVA: 0x002F8B1A File Offset: 0x002F6D1A
	public IncrementRenderQueueMaterialProperty() : base("RenderQueue")
	{
	}

	// Token: 0x0600926C RID: 37484 RVA: 0x002F8B34 File Offset: 0x002F6D34
	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (this.m_enabled)
		{
			IncrementRenderQueueMaterialProperty incrementRenderQueueMaterialProperty = dest as IncrementRenderQueueMaterialProperty;
			if (incrementRenderQueueMaterialProperty != null)
			{
				incrementRenderQueueMaterialProperty.SetValue(this.m_value);
			}
		}
	}

	// Token: 0x0600926D RID: 37485 RVA: 0x002F8B5F File Offset: 0x002F6D5F
	public void SetValue(int value)
	{
		this.m_value = value;
		this.m_enabled = true;
	}

	// Token: 0x0600926E RID: 37486 RVA: 0x002F8B6F File Offset: 0x002F6D6F
	public override void DoApplyToMaterial(Material material)
	{
		if (this.m_originalMaterialRenderQueue == -9999)
		{
			this.m_originalMaterialRenderQueue = material.renderQueue;
		}
		material.renderQueue = this.m_originalMaterialRenderQueue + this.m_value;
	}

	// Token: 0x0600926F RID: 37487 RVA: 0x002F8BA0 File Offset: 0x002F6DA0
	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		IncrementRenderQueueMaterialProperty incrementRenderQueueMaterialProperty = materialProperty as IncrementRenderQueueMaterialProperty;
		return incrementRenderQueueMaterialProperty != null && this.m_value == incrementRenderQueueMaterialProperty.m_value;
	}

	// Token: 0x04007AD6 RID: 31446
	private int m_value;

	// Token: 0x04007AD7 RID: 31447
	private int m_originalMaterialRenderQueue = -9999;
}
