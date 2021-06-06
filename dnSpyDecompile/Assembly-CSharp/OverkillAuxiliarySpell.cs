using System;

// Token: 0x0200080D RID: 2061
public class OverkillAuxiliarySpell : Spell
{
	// Token: 0x06006F8E RID: 28558 RVA: 0x0023F9B8 File Offset: 0x0023DBB8
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		if (!base.AttachPowerTaskList(taskList))
		{
			return false;
		}
		Card sourceCard = base.GetSourceCard();
		if (sourceCard == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): No source card found.", new object[]
			{
				this
			});
			return false;
		}
		Entity entity = sourceCard.GetEntity();
		if (entity == null)
		{
			Log.Gameplay.PrintError("{0}.AttachPowerTaskList(): Current tasklist has no source entity.", new object[]
			{
				this
			});
			return false;
		}
		return !entity.IsSpell();
	}
}
