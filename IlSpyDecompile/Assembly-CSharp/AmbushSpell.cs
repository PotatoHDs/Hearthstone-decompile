using System.Collections.Generic;

public class AmbushSpell : OverrideCustomSpawnSpell
{
	public override bool AddPowerTargets()
	{
		if (!m_taskList.IsStartOfBlock())
		{
			return false;
		}
		List<PowerTask> taskList = m_taskList.GetTaskList();
		AddMultiplePowerTargets_FromMetaData(taskList);
		return true;
	}
}
