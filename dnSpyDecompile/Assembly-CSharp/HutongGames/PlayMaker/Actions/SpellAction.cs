using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F7D RID: 3965
	[ActionCategory("Pegasus")]
	[Tooltip("INTERNAL USE ONLY. Do not put this on your FSMs.")]
	public abstract class SpellAction : FsmStateAction
	{
		// Token: 0x0600AD7F RID: 44415 RVA: 0x00361408 File Offset: 0x0035F608
		public Spell GetSpell()
		{
			if (this.m_spell == null)
			{
				GameObject spellOwner = this.GetSpellOwner();
				if (spellOwner != null)
				{
					this.m_spell = SceneUtils.FindComponentInThisOrParents<Spell>(spellOwner);
				}
			}
			return this.m_spell;
		}

		// Token: 0x0600AD80 RID: 44416 RVA: 0x00361448 File Offset: 0x0035F648
		public Card GetCard(SpellAction.Which which)
		{
			Spell spell = this.GetSpell();
			if (spell == null)
			{
				return null;
			}
			if (which == SpellAction.Which.TARGET)
			{
				return spell.GetTargetCard();
			}
			Card sourceCard = spell.GetSourceCard();
			if (which == SpellAction.Which.SOURCE_HERO && sourceCard != null)
			{
				return sourceCard.GetHeroCard();
			}
			if (which == SpellAction.Which.CHOSEN_TARGET)
			{
				Card powerTargetCard = spell.GetPowerTargetCard();
				if (powerTargetCard != null)
				{
					return powerTargetCard;
				}
			}
			if (which == SpellAction.Which.SOURCE_PLAYER)
			{
				global::Log.All.PrintError("{0} cannot get card for source player: players are not cards. Did you mean to choose SOURCE_HERO?", new object[]
				{
					this
				});
				if (sourceCard != null)
				{
					return sourceCard.GetHeroCard();
				}
			}
			if (which == SpellAction.Which.SPELL_PARENT && spell.gameObject != null && spell.gameObject.transform.parent != null)
			{
				Actor component = spell.gameObject.transform.parent.gameObject.GetComponent<Actor>();
				if (component != null)
				{
					return component.GetCard();
				}
			}
			return sourceCard;
		}

		// Token: 0x0600AD81 RID: 44417 RVA: 0x00361528 File Offset: 0x0035F728
		public Entity GetEntity(SpellAction.Which which)
		{
			Spell spell = this.GetSpell();
			if (spell == null)
			{
				return null;
			}
			if (which == SpellAction.Which.TARGET)
			{
				return spell.GetTargetCard().GetEntity();
			}
			Card sourceCard = spell.GetSourceCard();
			if (which == SpellAction.Which.SOURCE_HERO && sourceCard != null)
			{
				return sourceCard.GetHero();
			}
			if (which == SpellAction.Which.SOURCE_PLAYER && sourceCard != null)
			{
				return sourceCard.GetController();
			}
			if (which == SpellAction.Which.CHOSEN_TARGET)
			{
				Card powerTargetCard = spell.GetPowerTargetCard();
				if (powerTargetCard != null)
				{
					return powerTargetCard.GetEntity();
				}
			}
			if (which == SpellAction.Which.SPELL_PARENT && spell.gameObject != null && spell.gameObject.transform.parent != null)
			{
				Actor component = spell.gameObject.transform.parent.gameObject.GetComponent<Actor>();
				if (component != null)
				{
					return component.GetEntity();
				}
			}
			return sourceCard.GetEntity();
		}

		// Token: 0x0600AD82 RID: 44418 RVA: 0x003615FC File Offset: 0x0035F7FC
		public Actor GetActor(SpellAction.Which which)
		{
			Card card = this.GetCard(which);
			if (card == null)
			{
				return null;
			}
			return card.GetActor();
		}

		// Token: 0x0600AD83 RID: 44419 RVA: 0x00361624 File Offset: 0x0035F824
		public int GetIndexMatchingCardId(string cardId, string[] cardIds)
		{
			if (cardId == null || cardIds == null || cardIds.Length == 0)
			{
				return -1;
			}
			for (int i = 0; i < cardIds.Length; i++)
			{
				string value = cardIds[i].Trim();
				if (cardId.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600AD84 RID: 44420
		protected abstract GameObject GetSpellOwner();

		// Token: 0x0600AD85 RID: 44421 RVA: 0x00361661 File Offset: 0x0035F861
		public override void OnEnter()
		{
			this.GetSpell();
			if (this.m_spell == null)
			{
				Debug.LogError(string.Format("{0}.OnEnter() - FAILED to find Spell component on Owner \"{1}\"", this, base.Owner));
				return;
			}
		}

		// Token: 0x0400945D RID: 37981
		protected Spell m_spell;

		// Token: 0x020027C2 RID: 10178
		public enum Which
		{
			// Token: 0x0400F58E RID: 62862
			SOURCE,
			// Token: 0x0400F58F RID: 62863
			TARGET,
			// Token: 0x0400F590 RID: 62864
			SOURCE_HERO,
			// Token: 0x0400F591 RID: 62865
			CHOSEN_TARGET,
			// Token: 0x0400F592 RID: 62866
			SOURCE_PLAYER,
			// Token: 0x0400F593 RID: 62867
			SPELL_PARENT
		}
	}
}
