using System.Collections.Generic;
using UnityEngine;

public abstract class UberTextMaterialQuery
{
	protected IncrementRenderQueueMaterialProperty m_IncrementRenderQueueMaterialProperty = new IncrementRenderQueueMaterialProperty();

	protected List<UberTextMaterialProperty> m_materialProperties;

	protected int m_minimalRenderQueueValue = -9999;

	public UberTextMaterialManager.MaterialType Type { get; protected set; }

	public UberTextMaterialQuery(int propertiesSize)
	{
		m_materialProperties = new List<UberTextMaterialProperty>(propertiesSize + 1);
		m_materialProperties.Add(m_IncrementRenderQueueMaterialProperty);
	}

	public abstract UberTextMaterialQuery Clone();

	public virtual bool Equals(UberTextMaterialQuery query)
	{
		if (query.Type != Type)
		{
			return false;
		}
		int count = query.m_materialProperties.Count;
		int count2 = m_materialProperties.Count;
		for (int i = 0; i < count2; i++)
		{
			if (i >= count || !m_materialProperties[i].Equals(query.m_materialProperties[i]))
			{
				return false;
			}
		}
		return true;
	}

	public void ApplyToMaterial(Material material)
	{
		foreach (UberTextMaterialProperty materialProperty in m_materialProperties)
		{
			materialProperty.ApplyToMaterial(material);
		}
	}

	public UberTextMaterialQuery WithIncrementRenderQueue(int rq)
	{
		if (rq > -1)
		{
			m_IncrementRenderQueueMaterialProperty.SetValue(rq);
		}
		return this;
	}

	public UberTextMaterialQuery WithMinimalRenderQueueValue(int rq)
	{
		if (rq > -1)
		{
			m_minimalRenderQueueValue = rq;
		}
		return this;
	}

	public int GetMinimalRenderQueueValue()
	{
		return m_minimalRenderQueueValue;
	}

	public bool HasRenderQueue()
	{
		return m_IncrementRenderQueueMaterialProperty.IsEnabled();
	}

	protected void CopyPropertiesTo(UberTextMaterialQuery newQuery)
	{
		int count = newQuery.m_materialProperties.Count;
		for (int i = 0; i <= m_materialProperties.Count && i < count; i++)
		{
			UberTextMaterialProperty uberTextMaterialProperty = m_materialProperties[i];
			UberTextMaterialProperty uberTextMaterialProperty2 = newQuery.m_materialProperties[i];
			if (uberTextMaterialProperty.GetType() == uberTextMaterialProperty2.GetType())
			{
				uberTextMaterialProperty.CopyTo(uberTextMaterialProperty2);
			}
		}
	}
}
