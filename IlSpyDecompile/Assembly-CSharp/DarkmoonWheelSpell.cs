using PegasusGame;

public class DarkmoonWheelSpell : SuperSpell
{
	private int m_metadataChoice = -1;

	public override bool ShouldReconnectIfStuck()
	{
		return false;
	}

	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		bool result = base.AttachPowerTaskList(taskList);
		m_metadataChoice = GetSpinResultMetadata();
		if (m_metadataChoice == -1)
		{
			return false;
		}
		return result;
	}

	private int GetSpinResultMetadata()
	{
		foreach (PowerTask task in m_taskList.GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type == Network.PowerType.META_DATA)
			{
				Network.HistMetaData histMetaData = power as Network.HistMetaData;
				if (histMetaData.MetaType == HistoryMeta.Type.EFFECT_SELECTION)
				{
					return histMetaData.Data;
				}
			}
		}
		return -1;
	}

	protected override void DoActionNow()
	{
		m_startSpell.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("YoggWheelOutcome").Value = m_metadataChoice;
		base.DoActionNow();
	}
}
