using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000839 RID: 2105
public class VoodooDollLinkSpell : Spell
{
	// Token: 0x0600707C RID: 28796 RVA: 0x00244A55 File Offset: 0x00242C55
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.StartCoroutine(this.SetupTargetsAndPlay());
	}

	// Token: 0x0600707D RID: 28797 RVA: 0x00244A6C File Offset: 0x00242C6C
	protected override void OnNone(SpellStateType prevStateType)
	{
		if (this.m_voodooSpells != null)
		{
			foreach (Spell spell in this.m_voodooSpells)
			{
				spell.Deactivate();
				UnityEngine.Object.Destroy(spell.gameObject);
			}
			this.m_voodooSpells.Clear();
			this.m_voodooSpells = null;
		}
		base.OnNone(prevStateType);
	}

	// Token: 0x0600707E RID: 28798 RVA: 0x00244AE8 File Offset: 0x00242CE8
	private IEnumerator SetupTargetsAndPlay()
	{
		this.SetupTargets();
		if (this.m_targets.Count == 0)
		{
			this.OnSpellFinished();
			yield break;
		}
		this.PlaySpells();
		yield break;
	}

	// Token: 0x0600707F RID: 28799 RVA: 0x00244AF8 File Offset: 0x00242CF8
	private void SetupTargets()
	{
		this.m_targets.Clear();
		Card sourceCard = base.GetSourceCard();
		if (sourceCard == null)
		{
			return;
		}
		Entity entity = sourceCard.GetEntity();
		if (entity.HasTag(GAME_TAG.VOODOO_LINK))
		{
			Entity entity2 = GameState.Get().GetEntity(entity.GetTag(GAME_TAG.VOODOO_LINK));
			if (entity2 != null && entity2.GetCard() != null)
			{
				this.m_targets.Add(sourceCard.gameObject);
				this.m_targets.Add(entity2.GetCard().gameObject);
				return;
			}
		}
		else
		{
			foreach (Entity entity3 in entity.GetAttachments())
			{
				if (entity3.HasTag(GAME_TAG.VOODOO_LINK))
				{
					Entity entity4 = GameState.Get().GetEntity(entity3.GetTag(GAME_TAG.VOODOO_LINK));
					if (entity4 != null && entity4.GetCard() != null)
					{
						this.m_targets.Add(entity4.GetCard().gameObject);
					}
				}
			}
			if (this.m_targets.Count > 0)
			{
				this.m_targets.Add(sourceCard.gameObject);
			}
		}
	}

	// Token: 0x06007080 RID: 28800 RVA: 0x00244C3C File Offset: 0x00242E3C
	private void PlaySpells()
	{
		this.m_voodooSpells = new List<Spell>();
		foreach (GameObject gameObject in this.m_targets)
		{
			Card component = gameObject.GetComponent<Card>();
			Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_VooDooFX);
			SpellUtils.SetCustomSpellParent(spell, component.GetActor());
			spell.SetSource(component.gameObject);
			spell.Activate();
			this.m_voodooSpells.Add(spell);
		}
	}

	// Token: 0x04005A65 RID: 23141
	public Spell m_VooDooFX;

	// Token: 0x04005A66 RID: 23142
	private List<Spell> m_voodooSpells;
}
