using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000807 RID: 2055
public abstract class MouseOverLinkSpell : Spell
{
	// Token: 0x06006F65 RID: 28517 RVA: 0x0023EC8C File Offset: 0x0023CE8C
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine("SetupTargetsAndPlay");
	}

	// Token: 0x06006F66 RID: 28518 RVA: 0x0023ECA4 File Offset: 0x0023CEA4
	protected override void OnNone(SpellStateType prevStateType)
	{
		if (this.m_spells != null)
		{
			foreach (Spell spell in this.m_spells)
			{
				spell.Deactivate();
				UnityEngine.Object.Destroy(spell.gameObject);
			}
			this.m_spells.Clear();
			this.m_spells = null;
		}
		base.OnNone(prevStateType);
	}

	// Token: 0x06006F67 RID: 28519
	protected abstract void GetAllTargets(Entity source, List<GameObject> targets);

	// Token: 0x06006F68 RID: 28520 RVA: 0x0023ED20 File Offset: 0x0023CF20
	private void SetupTargetsAndPlay()
	{
		this.SetupTargets();
		if (this.m_targets.Count == 0)
		{
			this.OnSpellFinished();
			return;
		}
		this.PlaySpells();
	}

	// Token: 0x06006F69 RID: 28521 RVA: 0x0023ED44 File Offset: 0x0023CF44
	private void SetupTargets()
	{
		this.m_targets.Clear();
		Card sourceCard = base.GetSourceCard();
		if (sourceCard == null)
		{
			return;
		}
		Entity entity = sourceCard.GetEntity();
		if (entity == null)
		{
			return;
		}
		this.GetAllTargets(entity, this.m_targets);
	}

	// Token: 0x06006F6A RID: 28522 RVA: 0x0023ED88 File Offset: 0x0023CF88
	private void PlaySpells()
	{
		this.m_spells = new List<Spell>();
		foreach (GameObject gameObject in this.m_targets)
		{
			Card component = gameObject.GetComponent<Card>();
			Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_targetSpellFX);
			SpellUtils.SetCustomSpellParent(spell, component.GetActor());
			spell.SetSource(component.gameObject);
			spell.Activate();
			this.m_spells.Add(spell);
		}
	}

	// Token: 0x04005956 RID: 22870
	public Spell m_targetSpellFX;

	// Token: 0x04005957 RID: 22871
	private List<Spell> m_spells;
}
