using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on an Actor's Card's rarity.")]
	public class ActorRarityEventAction : ActorAction
	{
		public FsmOwnerDefault m_ActorObject;

		public FsmEvent m_FreeEvent;

		public FsmEvent m_CommonEvent;

		public FsmEvent m_RareEvent;

		public FsmEvent m_EpicEvent;

		public FsmEvent m_LegendaryEvent;

		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_ActorObject);
		}

		public override void Reset()
		{
			m_ActorObject = null;
			m_FreeEvent = null;
			m_CommonEvent = null;
			m_RareEvent = null;
			m_EpicEvent = null;
			m_LegendaryEvent = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_actor == null)
			{
				Finish();
				return;
			}
			TAG_RARITY rarity = m_actor.GetRarity();
			switch (rarity)
			{
			case TAG_RARITY.FREE:
				base.Fsm.Event(m_FreeEvent);
				break;
			case TAG_RARITY.COMMON:
				base.Fsm.Event(m_CommonEvent);
				break;
			case TAG_RARITY.RARE:
				base.Fsm.Event(m_RareEvent);
				break;
			case TAG_RARITY.EPIC:
				base.Fsm.Event(m_EpicEvent);
				break;
			case TAG_RARITY.LEGENDARY:
				base.Fsm.Event(m_LegendaryEvent);
				break;
			default:
				Debug.LogError($"ActorRarityEventAction.OnEnter() - unknown rarity {rarity} on actor {m_actor}");
				break;
			}
			Finish();
		}
	}
}
