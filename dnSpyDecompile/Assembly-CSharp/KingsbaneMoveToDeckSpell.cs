using System;

// Token: 0x02000800 RID: 2048
public class KingsbaneMoveToDeckSpell : SpawnToDeckSpell
{
	// Token: 0x06006F2A RID: 28458 RVA: 0x0023CAEC File Offset: 0x0023ACEC
	protected override void OnActorLoaded(Actor actor)
	{
		actor.SetEntityDef(null);
		actor.SetEntity(base.GetSourceCard().GetEntity());
	}
}
