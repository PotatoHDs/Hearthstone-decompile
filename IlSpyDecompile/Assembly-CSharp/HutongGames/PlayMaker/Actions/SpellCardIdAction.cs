using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on a Spell's Card's ID.")]
	public class SpellCardIdAction : SpellAction
	{
		public FsmOwnerDefault m_SpellObject;

		[Tooltip("Which Card to check on the Spell.")]
		public Which m_WhichCard;

		[RequiredField]
		[CompoundArray("Events", "Event", "Card Id")]
		public FsmEvent[] m_Events;

		public string[] m_CardIds;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void Reset()
		{
			m_SpellObject = null;
			m_WhichCard = Which.SOURCE;
			m_Events = new FsmEvent[2];
			m_CardIds = new string[2];
		}

		public override void OnEnter()
		{
			base.OnEnter();
			Card card = GetCard(m_WhichCard);
			if (card == null)
			{
				Error.AddDevFatal("SpellCardIdAction.OnEnter() - Card not found!");
				Finish();
				return;
			}
			string cardId = card.GetEntity().GetCardId();
			int indexMatchingCardId = GetIndexMatchingCardId(cardId, m_CardIds);
			if (indexMatchingCardId >= 0 && m_Events[indexMatchingCardId] != null)
			{
				base.Fsm.Event(m_Events[indexMatchingCardId]);
			}
			Finish();
		}
	}
}
