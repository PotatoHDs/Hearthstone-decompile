using System;
using System.Collections.Generic;

// Token: 0x020007F4 RID: 2036
public class HemetJungleHunterSpell : Spell
{
	// Token: 0x06006ED6 RID: 28374 RVA: 0x0023B970 File Offset: 0x00239B70
	public override bool AddPowerTargets()
	{
		if (!base.CanAddPowerTargets())
		{
			return false;
		}
		int num = 0;
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistShowEntity histShowEntity = taskList[i].GetPower() as Network.HistShowEntity;
			if (histShowEntity != null)
			{
				foreach (Network.Entity.Tag tag in histShowEntity.Entity.Tags)
				{
					if (tag.Name == 49 && tag.Value == 6)
					{
						num++;
						break;
					}
				}
			}
		}
		this.m_cardsDestroyed = num;
		return true;
	}

	// Token: 0x06006ED7 RID: 28375 RVA: 0x0023BA24 File Offset: 0x00239C24
	protected override void OnAttachPowerTaskList()
	{
		base.OnAttachPowerTaskList();
		PlayMakerFSM component = base.GetComponent<PlayMakerFSM>();
		if (component != null)
		{
			component.FsmVariables.GetFsmInt("CardsDestroyed").Value = this.m_cardsDestroyed;
		}
	}

	// Token: 0x040058ED RID: 22765
	private int m_cardsDestroyed;
}
