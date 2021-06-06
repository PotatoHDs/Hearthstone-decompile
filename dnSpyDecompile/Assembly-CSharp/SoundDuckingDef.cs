using System;
using System.Collections.Generic;
using Assets;

// Token: 0x02000959 RID: 2393
[Serializable]
public class SoundDuckingDef
{
	// Token: 0x060083F2 RID: 33778 RVA: 0x002AB7E2 File Offset: 0x002A99E2
	public override string ToString()
	{
		return string.Format("[SoundDuckingDef: {0}]", this.m_TriggerCategory);
	}

	// Token: 0x04006EA1 RID: 28321
	public Global.SoundCategory m_TriggerCategory;

	// Token: 0x04006EA2 RID: 28322
	public List<SoundDuckedCategoryDef> m_DuckedCategoryDefs;
}
