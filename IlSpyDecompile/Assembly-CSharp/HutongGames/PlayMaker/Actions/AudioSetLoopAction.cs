using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Sets looping on the AudioSource on a Game Object.")]
	public class AudioSetLoopAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		public FsmBool m_Loop;

		public override void Reset()
		{
			m_GameObject = null;
			m_Loop = false;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (ownerDefaultTarget != null)
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (component != null)
				{
					component.loop = m_Loop.Value;
				}
			}
			Finish();
		}
	}
}
