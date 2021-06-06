using System;
using System.Collections.Generic;

// Token: 0x0200082D RID: 2093
public class TheDarknessProgressSpell : Spell
{
	// Token: 0x06007044 RID: 28740 RVA: 0x00243654 File Offset: 0x00241854
	public override bool AddPowerTargets()
	{
		if (!base.AddPowerTargets())
		{
			return false;
		}
		int num = 0;
		if (!this.GetCurrentProgress(ref num))
		{
			return false;
		}
		int tag = base.GetSourceCard().GetEntity().GetTag(GAME_TAG.SCORE_VALUE_1);
		this.m_ProgressText.Text = string.Format("{0}/{1}", num, tag);
		return true;
	}

	// Token: 0x06007045 RID: 28741 RVA: 0x002436B4 File Offset: 0x002418B4
	private bool GetCurrentProgress(ref int currnt)
	{
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (histTagChange.Entity == base.GetSourceCard().GetEntity().GetEntityId() && histTagChange.Tag == 453)
				{
					currnt = histTagChange.Value;
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04005A2E RID: 23086
	public UberText m_ProgressText;
}
