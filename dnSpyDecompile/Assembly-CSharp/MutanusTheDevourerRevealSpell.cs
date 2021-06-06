using System;

// Token: 0x02000808 RID: 2056
public class MutanusTheDevourerRevealSpell : Spell
{
	// Token: 0x06006F6C RID: 28524 RVA: 0x0023EE1C File Offset: 0x0023D01C
	protected override void OnBirth(SpellStateType prevStateType)
	{
		Card sourceCard = base.GetSourceCard();
		Entity entity = sourceCard.GetEntity();
		if (entity != null && entity.IsControlledByOpposingSidePlayer())
		{
			string handActor = ActorNames.GetHandActor(entity);
			sourceCard.UpdateActor(false, handActor);
		}
		base.OnBirth(prevStateType);
	}

	// Token: 0x06006F6D RID: 28525 RVA: 0x0023EE58 File Offset: 0x0023D058
	public override void OnSpellFinished()
	{
		base.OnSpellFinished();
	}
}
