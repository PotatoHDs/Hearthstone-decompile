using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("INTERNAL USE ONLY. Do not put this on your FSMs.")]
	public abstract class SpellAction : FsmStateAction
	{
		public enum Which
		{
			SOURCE,
			TARGET,
			SOURCE_HERO,
			CHOSEN_TARGET,
			SOURCE_PLAYER,
			SPELL_PARENT
		}

		protected Spell m_spell;

		public Spell GetSpell()
		{
			if (m_spell == null)
			{
				GameObject spellOwner = GetSpellOwner();
				if (spellOwner != null)
				{
					m_spell = SceneUtils.FindComponentInThisOrParents<Spell>(spellOwner);
				}
			}
			return m_spell;
		}

		public Card GetCard(Which which)
		{
			Spell spell = GetSpell();
			if (spell == null)
			{
				return null;
			}
			if (which == Which.TARGET)
			{
				return spell.GetTargetCard();
			}
			Card sourceCard = spell.GetSourceCard();
			if (which == Which.SOURCE_HERO && sourceCard != null)
			{
				return sourceCard.GetHeroCard();
			}
			if (which == Which.CHOSEN_TARGET)
			{
				Card powerTargetCard = spell.GetPowerTargetCard();
				if (powerTargetCard != null)
				{
					return powerTargetCard;
				}
			}
			if (which == Which.SOURCE_PLAYER)
			{
				global::Log.All.PrintError("{0} cannot get card for source player: players are not cards. Did you mean to choose SOURCE_HERO?", this);
				if (sourceCard != null)
				{
					return sourceCard.GetHeroCard();
				}
			}
			if (which == Which.SPELL_PARENT && spell.gameObject != null && spell.gameObject.transform.parent != null)
			{
				Actor component = spell.gameObject.transform.parent.gameObject.GetComponent<Actor>();
				if (component != null)
				{
					return component.GetCard();
				}
			}
			return sourceCard;
		}

		public Entity GetEntity(Which which)
		{
			Spell spell = GetSpell();
			if (spell == null)
			{
				return null;
			}
			if (which == Which.TARGET)
			{
				return spell.GetTargetCard().GetEntity();
			}
			Card sourceCard = spell.GetSourceCard();
			if (which == Which.SOURCE_HERO && sourceCard != null)
			{
				return sourceCard.GetHero();
			}
			if (which == Which.SOURCE_PLAYER && sourceCard != null)
			{
				return sourceCard.GetController();
			}
			if (which == Which.CHOSEN_TARGET)
			{
				Card powerTargetCard = spell.GetPowerTargetCard();
				if (powerTargetCard != null)
				{
					return powerTargetCard.GetEntity();
				}
			}
			if (which == Which.SPELL_PARENT && spell.gameObject != null && spell.gameObject.transform.parent != null)
			{
				Actor component = spell.gameObject.transform.parent.gameObject.GetComponent<Actor>();
				if (component != null)
				{
					return component.GetEntity();
				}
			}
			return sourceCard.GetEntity();
		}

		public Actor GetActor(Which which)
		{
			Card card = GetCard(which);
			if (card == null)
			{
				return null;
			}
			return card.GetActor();
		}

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

		protected abstract GameObject GetSpellOwner();

		public override void OnEnter()
		{
			GetSpell();
			if (m_spell == null)
			{
				Debug.LogError($"{this}.OnEnter() - FAILED to find Spell component on Owner \"{base.Owner}\"");
			}
		}
	}
}
