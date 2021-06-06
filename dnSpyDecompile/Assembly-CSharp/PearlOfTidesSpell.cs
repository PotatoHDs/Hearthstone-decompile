using System;

// Token: 0x02000814 RID: 2068
public class PearlOfTidesSpell : SuperSpell
{
	// Token: 0x06006F9E RID: 28574 RVA: 0x0023FEFC File Offset: 0x0023E0FC
	protected override void OnAction(SpellStateType prevStateType)
	{
		foreach (PowerTask powerTask in this.m_taskList.GetTaskList())
		{
			Network.HistFullEntity histFullEntity = powerTask.GetPower() as Network.HistFullEntity;
			if (histFullEntity != null)
			{
				GameState.Get().GetEntity(histFullEntity.Entity.ID).GetCard().SuppressPlaySounds(true);
			}
		}
		base.OnAction(prevStateType);
	}
}
