using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Used to control the state of the Pegasus Highlight system")]
	public class HighlightStateAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject to send highlight states to")]
		public FsmOwnerDefault m_gameObj;

		[RequiredField]
		[Tooltip("State to send")]
		public ActorStateType m_state = ActorStateType.HIGHLIGHT_OFF;

		private DelayedEvent delayedEvent;

		public override void Reset()
		{
			m_gameObj = null;
			m_state = ActorStateType.HIGHLIGHT_OFF;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_gameObj);
			if (ownerDefaultTarget == null)
			{
				Finish();
				return;
			}
			HighlightState[] componentsInChildren = ownerDefaultTarget.GetComponentsInChildren<HighlightState>();
			if (componentsInChildren == null)
			{
				Finish();
				return;
			}
			HighlightState[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ChangeState(m_state);
			}
			Finish();
		}
	}
}
