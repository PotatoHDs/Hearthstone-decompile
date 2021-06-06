using UnityEngine;

public sealed class IncrementRenderQueueMaterialProperty : UberTextMaterialProperty
{
	private int m_value;

	private int m_originalMaterialRenderQueue = -9999;

	public IncrementRenderQueueMaterialProperty()
		: base("RenderQueue")
	{
	}

	public override void CopyTo(UberTextMaterialProperty dest)
	{
		if (m_enabled)
		{
			(dest as IncrementRenderQueueMaterialProperty)?.SetValue(m_value);
		}
	}

	public void SetValue(int value)
	{
		m_value = value;
		m_enabled = true;
	}

	public override void DoApplyToMaterial(Material material)
	{
		if (m_originalMaterialRenderQueue == -9999)
		{
			m_originalMaterialRenderQueue = material.renderQueue;
		}
		material.renderQueue = m_originalMaterialRenderQueue + m_value;
	}

	public override bool Equals(UberTextMaterialProperty materialProperty)
	{
		IncrementRenderQueueMaterialProperty incrementRenderQueueMaterialProperty = materialProperty as IncrementRenderQueueMaterialProperty;
		if (incrementRenderQueueMaterialProperty == null)
		{
			return false;
		}
		return m_value == incrementRenderQueueMaterialProperty.m_value;
	}
}
