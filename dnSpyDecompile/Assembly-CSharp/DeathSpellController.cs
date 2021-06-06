using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006C5 RID: 1733
public class DeathSpellController : SpellController
{
	// Token: 0x06006124 RID: 24868 RVA: 0x001FAEAD File Offset: 0x001F90AD
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		this.AddDeadCardsToTargetList(taskList);
		return this.m_targets.Count != 0;
	}

	// Token: 0x06006125 RID: 24869 RVA: 0x001FAEC8 File Offset: 0x001F90C8
	protected override void OnProcessTaskList()
	{
		int num = this.PickDeathSoundCardIndex();
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			Card card = this.m_targets[i];
			if (i != num)
			{
				card.SuppressDeathSounds(true);
			}
			card.ActivateCharacterDeathEffects();
		}
		base.OnProcessTaskList();
	}

	// Token: 0x06006126 RID: 24870 RVA: 0x001FAF18 File Offset: 0x001F9118
	private void AddDeadCardsToTargetList(PowerTaskList taskList)
	{
		List<PowerTask> taskList2 = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.PowerHistory power = taskList2[i].GetPower();
			if (power.Type == Network.PowerType.TAG_CHANGE)
			{
				Network.HistTagChange histTagChange = power as Network.HistTagChange;
				if (GameUtils.IsCharacterDeathTagChange(histTagChange))
				{
					Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
					Card card = entity.GetCard();
					if (this.CanAddTarget(entity, card))
					{
						base.AddTarget(card);
					}
				}
			}
		}
	}

	// Token: 0x06006127 RID: 24871 RVA: 0x001FAF96 File Offset: 0x001F9196
	private bool CanAddTarget(Entity entity, Card card)
	{
		return !card.WillSuppressDeathEffects();
	}

	// Token: 0x06006128 RID: 24872 RVA: 0x001FAFA4 File Offset: 0x001F91A4
	private int PickDeathSoundCardIndex()
	{
		if (this.m_targets.Count != 1)
		{
			if (this.m_targets.Count == 2)
			{
				Card card = this.m_targets[0];
				Card card2 = this.m_targets[1];
				Entity entity = card.GetEntity();
				Entity entity2 = card2.GetEntity();
				if (this.WasAttackedBy(entity, entity2))
				{
					if (this.CanPlayDeathSound(entity))
					{
						return 0;
					}
					return 1;
				}
				else if (this.WasAttackedBy(entity2, entity))
				{
					if (this.CanPlayDeathSound(entity2))
					{
						return 1;
					}
					return 0;
				}
			}
			return this.PickRandomDeathSoundCardIndex();
		}
		Entity entity3 = this.m_targets[0].GetEntity();
		if (this.CanPlayDeathSound(entity3))
		{
			return 0;
		}
		return -1;
	}

	// Token: 0x06006129 RID: 24873 RVA: 0x001FB046 File Offset: 0x001F9246
	private bool WasAttackedBy(Entity defender, Entity attacker)
	{
		return attacker.HasTag(GAME_TAG.ATTACKING) && defender.HasTag(GAME_TAG.DEFENDING) && defender.GetTag(GAME_TAG.LAST_AFFECTED_BY) == attacker.GetEntityId();
	}

	// Token: 0x0600612A RID: 24874 RVA: 0x001FB074 File Offset: 0x001F9274
	private int PickRandomDeathSoundCardIndex()
	{
		List<int> list = new List<int>();
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			Entity entity = this.m_targets[i].GetEntity();
			if (this.CanPlayDeathSound(entity))
			{
				list.Add(i);
			}
		}
		if (list.Count == 0)
		{
			return -1;
		}
		return list[UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x0600612B RID: 24875 RVA: 0x001FB0DB File Offset: 0x001F92DB
	private bool CanPlayDeathSound(Entity entity)
	{
		return !entity.HasTag(GAME_TAG.DEATHRATTLE_RETURN_ZONE) && !entity.HasTag(GAME_TAG.SUPPRESS_DEATH_SOUND);
	}
}
