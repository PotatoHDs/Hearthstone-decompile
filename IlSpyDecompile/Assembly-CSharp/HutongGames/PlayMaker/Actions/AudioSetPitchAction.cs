using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Sets the pitch of an AudioSource on a Game Object.")]
	public class AudioSetPitchAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_Pitch;

		public bool m_EveryFrame;

		public override void Reset()
		{
			m_GameObject = null;
			m_Pitch = 1f;
			m_EveryFrame = false;
		}

		public override void OnEnter()
		{
			UpdatePitch();
			if (!m_EveryFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			UpdatePitch();
		}

		private void UpdatePitch()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (!(ownerDefaultTarget == null))
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (!(component == null) && !m_Pitch.IsNone)
				{
					SoundManager.Get().SetPitch(component, m_Pitch.Value);
				}
			}
		}
	}
}
