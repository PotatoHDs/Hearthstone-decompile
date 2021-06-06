using System;
using System.Collections.Generic;

// Token: 0x020007CC RID: 1996
public class AmbushSpell : OverrideCustomSpawnSpell
{
	// Token: 0x06006DFA RID: 28154 RVA: 0x002372FC File Offset: 0x002354FC
	public override bool AddPowerTargets()
	{
		if (!this.m_taskList.IsStartOfBlock())
		{
			return false;
		}
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		base.AddMultiplePowerTargets_FromMetaData(taskList);
		return true;
	}
}
