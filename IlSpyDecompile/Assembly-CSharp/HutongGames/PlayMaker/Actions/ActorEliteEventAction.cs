using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on an Actor's Card's elite flag.")]
	public class ActorEliteEventAction : ActorAction
	{
		public FsmOwnerDefault m_ActorObject;

		public FsmEvent m_EliteEvent;

		public FsmEvent m_NonEliteEvent;

		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_ActorObject);
		}

		public override void Reset()
		{
			m_ActorObject = null;
			m_EliteEvent = null;
			m_NonEliteEvent = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_actor == null)
			{
				Finish();
				return;
			}
			if (m_actor.IsElite())
			{
				base.Fsm.Event(m_EliteEvent);
			}
			else
			{
				base.Fsm.Event(m_NonEliteEvent);
			}
			Finish();
		}
	}
}
