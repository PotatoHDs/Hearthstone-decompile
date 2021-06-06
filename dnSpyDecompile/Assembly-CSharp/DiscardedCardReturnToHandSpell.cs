using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007E8 RID: 2024
public class DiscardedCardReturnToHandSpell : Spell
{
	// Token: 0x06006E97 RID: 28311 RVA: 0x0023A995 File Offset: 0x00238B95
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_entityDiscarded = this.m_taskList.GetSourceEntity(false);
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DoActionWithTiming());
	}

	// Token: 0x06006E98 RID: 28312 RVA: 0x0023A9BD File Offset: 0x00238BBD
	private IEnumerator DoActionWithTiming()
	{
		this.ProcessShowEntityForTargets();
		yield return base.StartCoroutine(this.WaitAssetLoad());
		yield return base.StartCoroutine(this.PlayTargetSpells());
		yield break;
	}

	// Token: 0x06006E99 RID: 28313 RVA: 0x0023A9CC File Offset: 0x00238BCC
	private void ProcessShowEntityForTargets()
	{
		foreach (PowerTask powerTask in base.GetPowerTaskList().GetTaskList())
		{
			Network.PowerHistory power = powerTask.GetPower();
			if (power.Type == Network.PowerType.SHOW_ENTITY)
			{
				Network.Entity entity = (power as Network.HistShowEntity).Entity;
				Entity entity2 = this.FindTargetEntity(entity.ID);
				if (entity2 != null)
				{
					foreach (Network.Entity.Tag tag in entity.Tags)
					{
						entity2.SetTag(tag.Name, tag.Value);
					}
				}
			}
		}
	}

	// Token: 0x06006E9A RID: 28314 RVA: 0x0023AA98 File Offset: 0x00238C98
	private Entity FindTargetEntity(int entityID)
	{
		foreach (GameObject gameObject in this.m_targets)
		{
			Card component = gameObject.GetComponent<Card>();
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

	// Token: 0x06006E9B RID: 28315 RVA: 0x0023AB0C File Offset: 0x00238D0C
	private IEnumerator WaitAssetLoad()
	{
		foreach (GameObject gameObject in this.m_targets)
		{
			Card card = gameObject.GetComponent<Card>();
			if (!(card == null))
			{
				string cardId = this.m_entityDiscarded.GetCardId();
				EntityDef entityDef = DefLoader.Get().GetEntityDef(cardId);
				card.GetEntity().LoadCard(cardId, null);
				card.UpdateActor(true, ActorNames.GetHandActor(entityDef, this.m_entityDiscarded.GetPremiumType()));
				while (card.IsActorLoading())
				{
					yield return null;
				}
				TransformUtil.CopyWorld(card, this.m_entityDiscarded.GetCard().transform);
				card.HideCard();
				card = null;
			}
		}
		List<GameObject>.Enumerator enumerator = default(List<GameObject>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06006E9C RID: 28316 RVA: 0x0023AB1B File Offset: 0x00238D1B
	private IEnumerator PlayTargetSpells()
	{
		if (this.m_TargetSpell == null)
		{
			yield break;
		}
		using (List<GameObject>.Enumerator enumerator = this.m_targets.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GameObject gameObject = enumerator.Current;
				Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_TargetSpell);
				if (!(spell == null))
				{
					this.m_activeTargetSpells.Add(spell);
					TransformUtil.AttachAndPreserveLocalTransform(spell.transform, gameObject.transform);
					spell.SetSource(gameObject);
					spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnSelectedSpellFinished));
					spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnSelectedSpellStateFinished));
					spell.Activate();
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06006E9D RID: 28317 RVA: 0x0023AB2C File Offset: 0x00238D2C
	private void OnSelectedSpellFinished(Spell spell, object userData)
	{
		if (this.m_activeTargetSpells.Count == 0)
		{
			return;
		}
		using (List<Spell>.Enumerator enumerator = this.m_activeTargetSpells.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.IsFinished())
				{
					return;
				}
			}
		}
		this.OnSpellFinished();
	}

	// Token: 0x06006E9E RID: 28318 RVA: 0x0023AB94 File Offset: 0x00238D94
	private void OnSelectedSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (this.m_activeTargetSpells.Count == 0)
		{
			return;
		}
		foreach (Spell spell2 in this.m_activeTargetSpells)
		{
			if (spell.GetActiveState() != SpellStateType.NONE)
			{
				return;
			}
		}
		foreach (Spell obj in this.m_activeTargetSpells)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.m_activeTargetSpells.Clear();
		this.OnStateFinished();
	}

	// Token: 0x040058BE RID: 22718
	[SerializeField]
	private Spell m_TargetSpell;

	// Token: 0x040058BF RID: 22719
	private Entity m_entityDiscarded;

	// Token: 0x040058C0 RID: 22720
	private List<Spell> m_activeTargetSpells = new List<Spell>();
}
