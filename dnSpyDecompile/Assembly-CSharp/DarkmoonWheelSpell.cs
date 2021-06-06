using System;
using PegasusGame;

// Token: 0x020007E1 RID: 2017
public class DarkmoonWheelSpell : SuperSpell
{
	// Token: 0x06006E5A RID: 28250 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldReconnectIfStuck()
	{
		return false;
	}

	// Token: 0x06006E5B RID: 28251 RVA: 0x002397F0 File Offset: 0x002379F0
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		bool flag = base.AttachPowerTaskList(taskList);
		this.m_metadataChoice = this.GetSpinResultMetadata();
		return this.m_metadataChoice != -1 && flag;
	}

	// Token: 0x06006E5C RID: 28252 RVA: 0x00239820 File Offset: 0x00237A20
	private int GetSpinResultMetadata()
	{
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
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

	// Token: 0x06006E5D RID: 28253 RVA: 0x0023989C File Offset: 0x00237A9C
	protected override void DoActionNow()
	{
		this.m_startSpell.GetComponent<PlayMakerFSM>().FsmVariables.GetFsmInt("YoggWheelOutcome").Value = this.m_metadataChoice;
		base.DoActionNow();
	}

	// Token: 0x04005894 RID: 22676
	private int m_metadataChoice = -1;
}
