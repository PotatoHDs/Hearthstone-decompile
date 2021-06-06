using System;
using System.Collections.Generic;

// Token: 0x020007E0 RID: 2016
public class DarkBargainSpell : OverrideCustomDeathSpell
{
	// Token: 0x06006E58 RID: 28248 RVA: 0x002397C4 File Offset: 0x002379C4
	public override bool AddPowerTargets()
	{
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		base.AddMultiplePowerTargets_FromMetaData(taskList);
		return true;
	}
}
