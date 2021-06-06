using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardedCardReturnToHandSpell : Spell
{
	[SerializeField]
	private Spell m_TargetSpell;

	private Entity m_entityDiscarded;

	private List<Spell> m_activeTargetSpells = new List<Spell>();

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_entityDiscarded = m_taskList.GetSourceEntity(warnIfNull: false);
		base.OnAction(prevStateType);
		StartCoroutine(DoActionWithTiming());
	}

	private IEnumerator DoActionWithTiming()
	{
		ProcessShowEntityForTargets();
		yield return StartCoroutine(WaitAssetLoad());
		yield return StartCoroutine(PlayTargetSpells());
	}

	private void ProcessShowEntityForTargets()
	{
		foreach (PowerTask task in GetPowerTaskList().GetTaskList())
		{
			Network.PowerHistory power = task.GetPower();
			if (power.Type != Network.PowerType.SHOW_ENTITY)
			{
				continue;
			}
			Network.Entity entity = (power as Network.HistShowEntity).Entity;
			Entity entity2 = FindTargetEntity(entity.ID);
			if (entity2 == null)
			{
				continue;
			}
			foreach (Network.Entity.Tag tag in entity.Tags)
			{
				entity2.SetTag(tag.Name, tag.Value);
			}
		}
	}

	private Entity FindTargetEntity(int entityID)
	{
		foreach (GameObject target in m_targets)
		{
			Card component = target.GetComponent<Card>();
			if (!(component == null))
			{
				Entity entity = component.GetEntity();
				if (entity != null && entity.GetEntityId() == entityID)
				{
					return entity;
				}
			}
		}
		return null;
	}

	private IEnumerator WaitAssetLoad()
	{
		foreach (GameObject target in m_targets)
		{
			Card card = target.GetComponent<Card>();
			if (!(card == null))
			{
				string cardId = m_entityDiscarded.GetCardId();
				EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
				card.GetEntity().LoadCard(cardId);
				card.UpdateActor(forceIfNullZone: true, ActorNames.GetHandActor(entityDef, m_entityDiscarded.GetPremiumType()));
				while (card.IsActorLoading())
				{
					yield return null;
				}
				TransformUtil.CopyWorld(card, m_entityDiscarded.GetCard().transform);
				card.HideCard();
			}
		}
	}

	private IEnumerator PlayTargetSpells()
	{
		if (m_TargetSpell == null)
		{
			yield break;
		}
		foreach (GameObject target in m_targets)
		{
			Spell spell = Object.Instantiate(m_TargetSpell);
			if (!(spell == null))
			{
				m_activeTargetSpells.Add(spell);
				TransformUtil.AttachAndPreserveLocalTransform(spell.transform, target.transform);
				spell.SetSource(target);
				spell.AddFinishedCallback(OnSelectedSpellFinished);
				spell.AddStateFinishedCallback(OnSelectedSpellStateFinished);
				spell.Activate();
			}
		}
	}

	private void OnSelectedSpellFinished(Spell spell, object userData)
	{
		if (m_activeTargetSpells.Count == 0)
		{
			return;
		}
		foreach (Spell activeTargetSpell in m_activeTargetSpells)
		{
			if (!activeTargetSpell.IsFinished())
			{
				return;
			}
		}
		OnSpellFinished();
	}

	private void OnSelectedSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (m_activeTargetSpells.Count == 0)
		{
			return;
		}
		foreach (Spell activeTargetSpell in m_activeTargetSpells)
		{
			_ = activeTargetSpell;
			if (spell.GetActiveState() != 0)
			{
				return;
			}
		}
		foreach (Spell activeTargetSpell2 in m_activeTargetSpells)
		{
			Object.Destroy(activeTargetSpell2);
		}
		m_activeTargetSpells.Clear();
		OnStateFinished();
	}
}
