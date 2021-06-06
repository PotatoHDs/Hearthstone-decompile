using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on an Actor's Card's Golden state.")]
	public class ActorGoldEventAction : ActorAction
	{
		public FsmOwnerDefault m_ActorObject;

		public FsmEvent m_GoldenCardEvent;

		public FsmEvent m_StandardCardEvent;

		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_ActorObject);
		}

		public override void Reset()
		{
			m_ActorObject = null;
			m_GoldenCardEvent = null;
			m_StandardCardEvent = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_actor == null)
			{
				Finish();
				return;
			}
			if (m_actor.GetPremium() == TAG_PREMIUM.GOLDEN)
			{
				base.Fsm.Event(m_GoldenCardEvent);
			}
			else
			{
				base.Fsm.Event(m_StandardCardEvent);
			}
			Finish();
		}
	}
}
