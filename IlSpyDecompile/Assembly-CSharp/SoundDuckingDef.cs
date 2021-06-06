using System;
using System.Collections.Generic;
using Assets;

[Serializable]
public class SoundDuckingDef
{
	public Global.SoundCategory m_TriggerCategory;

	public List<SoundDuckedCategoryDef> m_DuckedCategoryDefs;

	public override string ToString()
	{
		return $"[SoundDuckingDef: {m_TriggerCategory}]";
	}
}
