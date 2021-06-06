using System.Collections.Generic;

public class DarkBargainSpell : OverrideCustomDeathSpell
{
	public override bool AddPowerTargets()
	{
		List<PowerTask> taskList = m_taskList.GetTaskList();
		AddMultiplePowerTargets_FromMetaData(taskList);
		return true;
	}
}
