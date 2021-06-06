using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Tells the Highlight system when the state is finished.")]
	public class HighlightFinishAction : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		protected HighlightState m_HighlightState;

		public HighlightState CacheHighlightState()
		{
			if (m_HighlightState == null)
			{
				GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
				if (ownerDefaultTarget != null)
				{
					m_HighlightState = SceneUtils.FindComponentInThisOrParents<HighlightState>(ownerDefaultTarget);
				}
			}
			return m_HighlightState;
		}

		public override void Reset()
		{
			m_GameObject = null;
		}

		public override void OnEnter()
		{
			CacheHighlightState();
			if (m_HighlightState == null)
			{
				Debug.LogError($"{this}.OnEnter() - FAILED to find {typeof(HighlightState)} in hierarchy");
				Finish();
			}
			else
			{
				m_HighlightState.OnActionFinished();
				Finish();
			}
		}
	}
}
