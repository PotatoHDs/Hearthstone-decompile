using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Send an event based on an Actor's Card's premium.")]
	public class ActorPremiumEventAction : ActorAction
	{
		public FsmOwnerDefault m_ActorObject;

		public FsmEvent m_NormalEvent;

		public FsmEvent m_GoldenEvent;

		public FsmEvent m_DiamondEvent;

		protected override GameObject GetActorOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_ActorObject);
		}

		public override void Reset()
		{
			m_ActorObject = null;
			m_NormalEvent = null;
			m_GoldenEvent = null;
			m_DiamondEvent = null;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_actor == null)
			{
				Finish();
				return;
			}
			TAG_PREMIUM premium = m_actor.GetPremium();
			switch (premium)
			{
			case TAG_PREMIUM.NORMAL:
				base.Fsm.Event(m_NormalEvent);
				break;
			case TAG_PREMIUM.GOLDEN:
				base.Fsm.Event(m_GoldenEvent);
				break;
			case TAG_PREMIUM.DIAMOND:
				base.Fsm.Event(m_DiamondEvent);
				break;
			default:
				Debug.LogError($"ActorPremiumEventAction.OnEnter() - unknown premium {premium} on actor {m_actor}");
				break;
			}
			Finish();
		}
	}
}
