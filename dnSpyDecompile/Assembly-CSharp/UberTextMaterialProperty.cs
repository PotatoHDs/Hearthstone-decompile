using System;
using UnityEngine;

// Token: 0x02000AAE RID: 2734
public abstract class UberTextMaterialProperty
{
	// Token: 0x0600924C RID: 37452 RVA: 0x002F885F File Offset: 0x002F6A5F
	public UberTextMaterialProperty(string name)
	{
		this.Name = name;
		this.m_enabled = false;
	}

	// Token: 0x0600924D RID: 37453 RVA: 0x002F8875 File Offset: 0x002F6A75
	public void ApplyToMaterial(Material material)
	{
		if (!material || !this.m_enabled)
		{
			return;
		}
		this.DoApplyToMaterial(material);
	}

	// Token: 0x0600924E RID: 37454 RVA: 0x002F888F File Offset: 0x002F6A8F
	public bool IsEnabled()
	{
		return this.m_enabled;
	}

	// Token: 0x0600924F RID: 37455 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void DoApplyToMaterial(Material material)
	{
	}

	// Token: 0x06009250 RID: 37456
	public abstract bool Equals(UberTextMaterialProperty materialProperty);

	// Token: 0x06009251 RID: 37457
	public abstract void CopyTo(UberTextMaterialProperty dest);

	// Token: 0x04007ACF RID: 31439
	public readonly string Name;

	// Token: 0x04007AD0 RID: 31440
	protected bool m_enabled;
}
