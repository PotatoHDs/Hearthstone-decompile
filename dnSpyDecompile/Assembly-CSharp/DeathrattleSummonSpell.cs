using System;
using UnityEngine;

// Token: 0x020006DA RID: 1754
public class DeathrattleSummonSpell : Spell
{
	// Token: 0x0600621A RID: 25114 RVA: 0x00200558 File Offset: 0x001FE758
	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type != Network.PowerType.FULL_ENTITY)
		{
			return null;
		}
		Network.Entity entity = ((Network.HistFullEntity)power).Entity;
		Entity entity2 = GameState.Get().GetEntity(entity.ID);
		if (entity2 == null)
		{
			Debug.LogWarning(string.Format("{0}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {1} but there is no entity with that id", this, entity.ID));
			return null;
		}
		return entity2.GetCard();
	}

	// Token: 0x0600621B RID: 25115 RVA: 0x002005BC File Offset: 0x001FE7BC
	protected override void OnAction(SpellStateType prevStateType)
	{
		Card sourceCard = base.GetSourceCard();
		foreach (GameObject gameObject in this.m_targets)
		{
			Card component = gameObject.GetComponent<Card>();
			component.transform.position = sourceCard.transform.position;
			float num = 0.2f;
			component.transform.localScale = new Vector3(num, num, num);
			component.SetTransitionStyle(ZoneTransitionStyle.VERY_SLOW);
			component.SetDoNotWarpToNewZone(true);
		}
		base.OnBirth(prevStateType);
		this.OnSpellFinished();
	}
}
