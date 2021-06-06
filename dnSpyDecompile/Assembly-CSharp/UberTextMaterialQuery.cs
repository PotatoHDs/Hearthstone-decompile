using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AB5 RID: 2741
public abstract class UberTextMaterialQuery
{
	// Token: 0x1700086A RID: 2154
	// (get) Token: 0x06009270 RID: 37488 RVA: 0x002F8BC7 File Offset: 0x002F6DC7
	// (set) Token: 0x06009271 RID: 37489 RVA: 0x002F8BCF File Offset: 0x002F6DCF
	public UberTextMaterialManager.MaterialType Type { get; protected set; }

	// Token: 0x06009272 RID: 37490 RVA: 0x002F8BD8 File Offset: 0x002F6DD8
	public UberTextMaterialQuery(int propertiesSize)
	{
		this.m_materialProperties = new List<UberTextMaterialProperty>(propertiesSize + 1);
		this.m_materialProperties.Add(this.m_IncrementRenderQueueMaterialProperty);
	}

	// Token: 0x06009273 RID: 37491
	public abstract UberTextMaterialQuery Clone();

	// Token: 0x06009274 RID: 37492 RVA: 0x002F8C18 File Offset: 0x002F6E18
	public virtual bool Equals(UberTextMaterialQuery query)
	{
		if (query.Type != this.Type)
		{
			return false;
		}
		int count = query.m_materialProperties.Count;
		int count2 = this.m_materialProperties.Count;
		for (int i = 0; i < count2; i++)
		{
			if (i >= count || !this.m_materialProperties[i].Equals(query.m_materialProperties[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06009275 RID: 37493 RVA: 0x002F8C80 File Offset: 0x002F6E80
	public void ApplyToMaterial(Material material)
	{
		foreach (UberTextMaterialProperty uberTextMaterialProperty in this.m_materialProperties)
		{
			uberTextMaterialProperty.ApplyToMaterial(material);
		}
	}

	// Token: 0x06009276 RID: 37494 RVA: 0x002F8CD4 File Offset: 0x002F6ED4
	public UberTextMaterialQuery WithIncrementRenderQueue(int rq)
	{
		if (rq > -1)
		{
			this.m_IncrementRenderQueueMaterialProperty.SetValue(rq);
		}
		return this;
	}

	// Token: 0x06009277 RID: 37495 RVA: 0x002F8CE7 File Offset: 0x002F6EE7
	public UberTextMaterialQuery WithMinimalRenderQueueValue(int rq)
	{
		if (rq > -1)
		{
			this.m_minimalRenderQueueValue = rq;
		}
		return this;
	}

	// Token: 0x06009278 RID: 37496 RVA: 0x002F8CF5 File Offset: 0x002F6EF5
	public int GetMinimalRenderQueueValue()
	{
		return this.m_minimalRenderQueueValue;
	}

	// Token: 0x06009279 RID: 37497 RVA: 0x002F8CFD File Offset: 0x002F6EFD
	public bool HasRenderQueue()
	{
		return this.m_IncrementRenderQueueMaterialProperty.IsEnabled();
	}

	// Token: 0x0600927A RID: 37498 RVA: 0x002F8D0C File Offset: 0x002F6F0C
	protected void CopyPropertiesTo(UberTextMaterialQuery newQuery)
	{
		int count = newQuery.m_materialProperties.Count;
		int num = 0;
		while (num <= this.m_materialProperties.Count && num < count)
		{
			UberTextMaterialProperty uberTextMaterialProperty = this.m_materialProperties[num];
			UberTextMaterialProperty uberTextMaterialProperty2 = newQuery.m_materialProperties[num];
			if (uberTextMaterialProperty.GetType() == uberTextMaterialProperty2.GetType())
			{
				uberTextMaterialProperty.CopyTo(uberTextMaterialProperty2);
			}
			num++;
		}
	}

	// Token: 0x04007AD9 RID: 31449
	protected IncrementRenderQueueMaterialProperty m_IncrementRenderQueueMaterialProperty = new IncrementRenderQueueMaterialProperty();

	// Token: 0x04007ADA RID: 31450
	protected List<UberTextMaterialProperty> m_materialProperties;

	// Token: 0x04007ADB RID: 31451
	protected int m_minimalRenderQueueValue = -9999;
}
