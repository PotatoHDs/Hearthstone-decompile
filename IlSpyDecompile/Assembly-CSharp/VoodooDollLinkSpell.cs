using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoodooDollLinkSpell : Spell
{
	public Spell m_VooDooFX;

	private List<Spell> m_voodooSpells;

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		StartCoroutine(SetupTargetsAndPlay());
	}

	protected override void OnNone(SpellStateType prevStateType)
	{
		if (m_voodooSpells != null)
		{
			foreach (Spell voodooSpell in m_voodooSpells)
			{
				voodooSpell.Deactivate();
				Object.Destroy(voodooSpell.gameObject);
			}
			m_voodooSpells.Clear();
			m_voodooSpells = null;
		}
		base.OnNone(prevStateType);
	}

	private IEnumerator SetupTargetsAndPlay()
	{
		SetupTargets();
		if (m_targets.Count == 0)
		{
			OnSpellFinished();
		}
		else
		{
			PlaySpells();
		}
		yield break;
	}

	private void SetupTargets()
	{
		m_targets.Clear();
		Card sourceCard = GetSourceCard();
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
				m_targets.Add(sourceCard.gameObject);
				m_targets.Add(entity2.GetCard().gameObject);
			}
			return;
		}
		foreach (Entity attachment in entity.GetAttachments())
		{
			if (attachment.HasTag(GAME_TAG.VOODOO_LINK))
			{
				Entity entity3 = GameState.Get().GetEntity(attachment.GetTag(GAME_TAG.VOODOO_LINK));
				if (entity3 != null && entity3.GetCard() != null)
				{
					m_targets.Add(entity3.GetCard().gameObject);
				}
			}
		}
		if (m_targets.Count > 0)
		{
			m_targets.Add(sourceCard.gameObject);
		}
	}

	private void PlaySpells()
	{
		m_voodooSpells = new List<Spell>();
		foreach (GameObject target in m_targets)
		{
			Card component = target.GetComponent<Card>();
			Spell spell = Object.Instantiate(m_VooDooFX);
			SpellUtils.SetCustomSpellParent(spell, component.GetActor());
			spell.SetSource(component.gameObject);
			spell.Activate();
			m_voodooSpells.Add(spell);
		}
	}
}
