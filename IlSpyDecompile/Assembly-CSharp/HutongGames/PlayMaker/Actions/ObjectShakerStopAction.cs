using Hearthstone.FX;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus")]
	[Tooltip("Cancels an ObjectShakerAction.")]
	public class ObjectShakerStopAction : FsmStateAction
	{
		[RequiredField]
		public FsmOwnerDefault m_GameObject;

		public override void Reset()
		{
			m_GameObject = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget != null)
			{
				ObjectShaker.Cancel(ownerDefaultTarget);
			}
			Finish();
		}
	}
}
