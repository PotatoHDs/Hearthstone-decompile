using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Randomly sets the volume of an AudioSource on a Game Object.")]
	public class AudioSetRandomVolumeAction : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault m_GameObject;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_MinVolume;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_MaxVolume;

		public bool m_EveryFrame;

		private float m_volume;

		public override void Reset()
		{
			m_GameObject = null;
			m_MinVolume = 1f;
			m_MaxVolume = 1f;
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

		private void ChooseVolume()
		{
			float min = (m_MinVolume.IsNone ? 1f : m_MinVolume.Value);
			float max = (m_MaxVolume.IsNone ? 1f : m_MaxVolume.Value);
			m_volume = Random.Range(min, max);
		}

		private void UpdateVolume()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_GameObject);
			if (!(ownerDefaultTarget == null))
			{
				AudioSource component = ownerDefaultTarget.GetComponent<AudioSource>();
				if (!(component == null))
				{
					SoundManager.Get().SetVolume(component, m_volume);
				}
			}
		}
	}
}
