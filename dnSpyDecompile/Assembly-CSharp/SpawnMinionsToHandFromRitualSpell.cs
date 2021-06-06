using System;
using UnityEngine;

// Token: 0x02000822 RID: 2082
public class SpawnMinionsToHandFromRitualSpell : SpawnToHandSpell
{
	// Token: 0x06006FF3 RID: 28659 RVA: 0x00241BE8 File Offset: 0x0023FDE8
	protected override Vector3 GetOriginForTarget(int targetIndex = 0)
	{
		Entity entity = base.GetSourceCard().GetEntity();
		Player controller = entity.GetController();
		if (controller.GetTag(GAME_TAG.MAIN_GALAKROND) == controller.GetHero().GetEntityId())
		{
			return base.GetOriginForTarget(targetIndex);
		}
		string name = (entity.GetControllerSide() == Player.Side.FRIENDLY) ? this.m_friendlyInvokeBoneName : this.m_opponentInvokeBoneName;
		return Board.Get().FindBone(name).position;
	}

	// Token: 0x040059C5 RID: 22981
	public string m_friendlyInvokeBoneName = "FriendlyRitual";

	// Token: 0x040059C6 RID: 22982
	public string m_opponentInvokeBoneName = "OpponentRitual";
}
