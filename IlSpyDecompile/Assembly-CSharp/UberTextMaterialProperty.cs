using UnityEngine;

public abstract class UberTextMaterialProperty
{
	public readonly string Name;

	protected bool m_enabled;

	public UberTextMaterialProperty(string name)
	{
		Name = name;
		m_enabled = false;
	}

	public void ApplyToMaterial(Material material)
	{
		if ((bool)material && m_enabled)
		{
			DoApplyToMaterial(material);
		}
	}

	public bool IsEnabled()
	{
		return m_enabled;
	}

	public virtual void DoApplyToMaterial(Material material)
	{
	}

	public abstract bool Equals(UberTextMaterialProperty materialProperty);

	public abstract void CopyTo(UberTextMaterialProperty dest);
}
