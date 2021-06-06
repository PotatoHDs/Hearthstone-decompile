using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on the player side of a Spell.")]
	public class SpellPlayerSideEventAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		public Which m_WhichCard;

		public FsmEvent m_FriendlyEvent;

		public FsmEvent m_OpponentEvent;

		public FsmEvent m_NeutralEvent;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void Reset()
		{
			m_SpellObject = null;
			m_WhichCard = Which.SOURCE;
			m_FriendlyEvent = null;
			m_OpponentEvent = null;
			m_NeutralEvent = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			Card card = GetCard(m_WhichCard);
			if (card == null)
			{
				Error.AddDevFatal("SpellPlayerSideEventAction.OnEnter() - Card not found for spell {0}", GetSpell());
				Finish();
				return;
			}
			switch (card.GetEntity().GetControllerSide())
			{
			case Player.Side.FRIENDLY:
				base.Fsm.Event(m_FriendlyEvent);
				break;
			case Player.Side.OPPOSING:
				base.Fsm.Event(m_OpponentEvent);
				break;
			default:
				base.Fsm.Event(m_NeutralEvent);
				break;
			}
			Finish();
		}
	}
}
