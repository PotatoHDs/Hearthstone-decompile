using System.Collections.Generic;

public class HemetJungleHunterSpell : Spell
{
	private int m_cardsDestroyed;

	public override bool AddPowerTargets()
	{
		if (!CanAddPowerTargets())
		{
			return false;
		}
		int num = 0;
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistShowEntity histShowEntity = taskList[i].GetPower() as Network.HistShowEntity;
			if (histShowEntity == null)
			{
				continue;
			}
			foreach (Network.Entity.Tag tag in histShowEntity.Entity.Tags)
			{
				if (tag.Name == 49 && tag.Value == 6)
				{
					num++;
					break;
				}
			}
		}
		m_cardsDestroyed = num;
		return true;
	}

	protected override void OnAttachPowerTaskList()
	{
		base.OnAttachPowerTaskList();
		PlayMakerFSM component = GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.FsmVariables.GetFsmInt("CardsDestroyed").Value = m_cardsDestroyed;
		}
	}
}
