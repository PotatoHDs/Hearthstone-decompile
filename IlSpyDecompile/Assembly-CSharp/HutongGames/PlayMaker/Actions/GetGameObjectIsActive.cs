using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Sends an Event based on the active state of a GameObject.")]
	public class GetGameObjectIsActive : FsmStateAction
	{
		public enum ActiveType
		{
			Self,
			Hierarchy
		}

		[RequiredField]
		public FsmOwnerDefault gameObject;

		[Tooltip("Do we only look at the active state of the GameObject itself, or its parent as well?")]
		public ActiveType activeType;

		[Space]
		[RequiredField]
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Active state TRUE event.")]
		public FsmEvent stateIsTrueEvent;

		[RequiredField]
		[UIHint(UIHint.FsmEvent)]
		[Tooltip("Active state FALSE event.")]
		public FsmEvent stateIsFalseEvent;

		[Tooltip("Delay before sending Event.")]
		public FsmFloat delay;

		private DelayedEvent m_delayedEvent;

		public override void Reset()
		{
			gameObject = null;
			activeType = ActiveType.Self;
			stateIsTrueEvent = null;
			stateIsFalseEvent = null;
			delay = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			bool flag = ((activeType == ActiveType.Self) ? ownerDefaultTarget.activeSelf : ownerDefaultTarget.activeInHierarchy);
			HandleEvent(flag ? stateIsTrueEvent : stateIsFalseEvent);
			Finish();
		}

		private void HandleEvent(FsmEvent fsmEvent)
		{
			if (delay.Value < 0.001f)
			{
				base.Fsm.Event(fsmEvent);
				Finish();
			}
			else
			{
				m_delayedEvent = base.Fsm.DelayedEvent(fsmEvent, delay.Value);
			}
		}

		public override void OnUpdate()
		{
			if (DelayedEvent.WasSent(m_delayedEvent))
			{
				Finish();
			}
		}
	}
}
