using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Sets the volume of an AudioSource on a Game Object.")]
	public class AudioSetVolumeAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_Volume;

		public bool m_EveryFrame;

		public override void Reset()
		{
			m_GameObject = null;
			m_Volume = 1f;
			m_EveryFrame = false;
		}

		public override void OnEnter()
		{
			UpdateVolume();
			if (!m_EveryFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			UpdateVolume();
		}

		private void UpdateVolume()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (!(ownerDefaultTarget == null))
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (!(component == null) && !m_Volume.IsNone)
				{
					SoundManager.Get().SetVolume(component, m_Volume.Value);
				}
			}
		}
	}
}
