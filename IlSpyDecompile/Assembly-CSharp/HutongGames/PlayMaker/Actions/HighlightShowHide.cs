using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Used to Show and Hide card Highlights")]
	public class HighlightShowHide : FsmStateAction
	{
		[RequiredField]
		[Tooltip("GameObject to send highlight states to")]
		public FsmOwnerDefault m_gameObj;

		[RequiredField]
		[Tooltip("Show or Hide")]
		public FsmBool m_Show = true;

		private DelayedEvent delayedEvent;

		public override void Reset()
		{
			m_gameObj = null;
			m_Show = true;
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
			foreach (HighlightState highlightState in array)
			{
				if (m_Show.Value)
				{
					highlightState.Show();
				}
				else
				{
					highlightState.Hide();
				}
			}
			Finish();
		}
	}
}
