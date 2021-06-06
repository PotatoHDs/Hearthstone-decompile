using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Used to control the state of the Pegasus Highlight system")]
	public class HighlightContinuousUpdateAction : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject to send highlight states to")]
		public FsmOwnerDefault m_gameObj;

		[RequiredField]
		[Tooltip("Amount of time to render")]
		public FsmFloat m_updateTime = 1f;

		private DelayedEvent delayedEvent;

		public override void Reset()
		{
			m_gameObj = null;
			m_updateTime = 1f;
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
				array[i].ContinuousUpdate(m_updateTime.Value);
			}
			Finish();
		}
	}
}
