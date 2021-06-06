using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007FE RID: 2046
public class KarazhanPortalSpell : IdleSuperSpell
{
	// Token: 0x06006F20 RID: 28448 RVA: 0x0023C8B9 File Offset: 0x0023AAB9
	public KarazhanPortalSpell()
	{
		this.m_playIdleSpellWithoutTargets = true;
	}

	// Token: 0x06006F21 RID: 28449 RVA: 0x0023C8C8 File Offset: 0x0023AAC8
	protected override void DoActionPreTasks()
	{
		this.m_willSummonAMinion = false;
		if (this.m_spawnedMinion == null)
		{
			this.m_spawnedMinion = this.GetSpawnedMinion();
			if (this.m_spawnedMinion != null)
			{
				this.m_waitForSpawnSpell = true;
				this.m_spawnSpellInstance = UnityEngine.Object.Instantiate<Spell>(this.m_customSpawnSpell);
				this.m_spawnSpellInstance.AddSpellEventCallback(new Spell.SpellEventCallback(this.OnSpawnSpellEvent));
				this.m_spawnedMinion.OverrideCustomSpawnSpell(this.m_spawnSpellInstance);
				this.m_willSummonAMinion = true;
			}
		}
	}

	// Token: 0x06006F22 RID: 28450 RVA: 0x0023C94B File Offset: 0x0023AB4B
	protected override void DoActionPostTasks()
	{
		if (this.m_willSummonAMinion)
		{
			this.SuppressDeathSoundsOnKilledTargets();
		}
	}

	// Token: 0x06006F23 RID: 28451 RVA: 0x0023C95B File Offset: 0x0023AB5B
	protected override bool HasPendingTasks()
	{
		return this.m_waitForSpawnSpell;
	}

	// Token: 0x06006F24 RID: 28452 RVA: 0x0023C964 File Offset: 0x0023AB64
	private void SuppressDeathSoundsOnKilledTargets()
	{
		List<Entity> list = new List<Entity>();
		foreach (GameObject gameObject in this.GetVisualTargets())
		{
			if (!(gameObject == null))
			{
				Card component = gameObject.GetComponent<Card>();
				list.Add(component.GetEntity());
			}
		}
		foreach (Entity entity in GameUtils.GetEntitiesKilledBySourceAmongstTargets(base.GetSourceCard().GetEntity().GetEntityId(), list))
		{
			entity.GetCard().SuppressDeathSounds(true);
		}
	}

	// Token: 0x06006F25 RID: 28453 RVA: 0x0023CA28 File Offset: 0x0023AC28
	public void OnSpawnSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName == "ClosePortal")
		{
			this.m_waitForSpawnSpell = false;
			this.OnSpellFinished();
		}
	}

	// Token: 0x06006F26 RID: 28454 RVA: 0x0023CA44 File Offset: 0x0023AC44
	private Card GetSpawnedMinion()
	{
		for (int i = 0; i < this.m_taskList.GetTaskList().Count; i++)
		{
			Network.PowerHistory power = this.m_taskList.GetTaskList()[i].GetPower();
			if (power.Type == Network.PowerType.FULL_ENTITY)
			{
				int id = (power as Network.HistFullEntity).Entity.ID;
				Entity entity = GameState.Get().GetEntity(id);
				if (entity.GetTag(GAME_TAG.ZONE) != 6 && entity != null)
				{
					Card card = entity.GetCard();
					if (!(card == null))
					{
						return card;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x04005920 RID: 22816
	public Spell m_customSpawnSpell;

	// Token: 0x04005921 RID: 22817
	private bool m_waitForSpawnSpell;

	// Token: 0x04005922 RID: 22818
	private Spell m_spawnSpellInstance;

	// Token: 0x04005923 RID: 22819
	private Card m_spawnedMinion;

	// Token: 0x04005924 RID: 22820
	private bool m_willSummonAMinion;
}
