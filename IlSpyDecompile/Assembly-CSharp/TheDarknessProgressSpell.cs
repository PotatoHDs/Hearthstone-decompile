using System.Collections.Generic;

public class TheDarknessProgressSpell : Spell
{
	public UberText m_ProgressText;

	public override bool AddPowerTargets()
	{
		if (!base.AddPowerTargets())
		{
			return false;
		}
		int currnt = 0;
		if (!GetCurrentProgress(ref currnt))
		{
			return false;
		}
		int num = GetSourceCard().GetEntity().GetTag(GAME_TAG.SCORE_VALUE_1);
		m_ProgressText.Text = $"{currnt}/{num}";
		return true;
	}

	private bool GetCurrentProgress(ref int currnt)
	{
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = (Network.HistTagChange)power;
				if (histTagChange.Entity == GetSourceCard().GetEntity().GetEntityId() && histTagChange.Tag == 453)
				{
					currnt = histTagChange.Value;
					return true;
				}
			}
		}
		return false;
	}
}
