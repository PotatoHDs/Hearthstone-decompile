using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on a Spell's Card's Type.")]
	public class SpellCardTypeAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		public Which m_WhichCard;

		public FsmEvent m_MinionEvent;

		public FsmEvent m_HeroEvent;

		public FsmEvent m_HeroPowerEvent;

		public FsmEvent m_WeaponEvent;

		public FsmEvent m_SpellEvent;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void Reset()
		{
			m_SpellObject = null;
			m_WhichCard = Which.SOURCE;
			m_MinionEvent = null;
			m_HeroEvent = null;
			m_HeroPowerEvent = null;
			m_WeaponEvent = null;
			m_SpellEvent = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			Card card = GetCard(m_WhichCard);
			if (card == null)
			{
				Error.AddDevFatal("SpellCardTypeAction.OnEnter() - Card not found!");
				Finish();
				return;
			}
			TAG_CARDTYPE cardType = card.GetEntity().GetCardType();
			switch (cardType)
			{
			case TAG_CARDTYPE.MINION:
				base.Fsm.Event(m_MinionEvent);
				break;
			case TAG_CARDTYPE.HERO:
				base.Fsm.Event(m_HeroEvent);
				break;
			case TAG_CARDTYPE.HERO_POWER:
				base.Fsm.Event(m_HeroPowerEvent);
				break;
			case TAG_CARDTYPE.WEAPON:
				base.Fsm.Event(m_WeaponEvent);
				break;
			case TAG_CARDTYPE.SPELL:
				base.Fsm.Event(m_SpellEvent);
				break;
			default:
				Error.AddDevFatal("SpellCardTypeAction.OnEnter() - unknown type {0} on {1}", cardType, card);
				break;
			}
			Finish();
		}
	}
}
