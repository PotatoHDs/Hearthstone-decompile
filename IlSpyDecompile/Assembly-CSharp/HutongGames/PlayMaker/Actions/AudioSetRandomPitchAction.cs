using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Randomly sets the pitch of an AudioSource on a Game Object.")]
	public class AudioSetRandomPitchAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_MinPitch;

		[HasFloatSlider(-3f, 3f)]
		public FsmFloat m_MaxPitch;

		public bool m_EveryFrame;

		private float m_pitch;

		public override void Reset()
		{
			m_GameObject = null;
			m_MinPitch = 1f;
			m_MaxPitch = 1f;
			m_EveryFrame = false;
		}

		public override void OnEnter()
		{
			ChoosePitch();
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

		private void ChoosePitch()
		{
			float min = (m_MinPitch.IsNone ? 1f : m_MinPitch.Value);
			float max = (m_MaxPitch.IsNone ? 1f : m_MaxPitch.Value);
			m_pitch = Random.Range(min, max);
		}

		private void UpdatePitch()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (!(ownerDefaultTarget == null))
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (!(component == null))
				{
					SoundManager.Get().SetPitch(component, m_pitch);
				}
			}
		}
	}
}
