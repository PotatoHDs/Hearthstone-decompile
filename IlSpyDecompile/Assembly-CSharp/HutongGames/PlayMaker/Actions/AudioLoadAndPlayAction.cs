using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Loads and Plays a Sound Prefab.")]
	public class AudioLoadAndPlayAction : FsmStateAction
	{
		[Tooltip("Optional. If specified, the generated Audio Source will be attached to this object.")]
		public FsmOwnerDefault m_ParentObject;

		[RequiredField]
		public FsmString m_PrefabName;

		[Tooltip("Optional. Scales the volume of the loaded sound.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_VolumeScale;

		public override void Reset()
		{
			m_ParentObject = null;
			m_PrefabName = null;
			m_VolumeScale = new FsmFloat
			{
				UseVariable = true
			};
		}

		public override void OnEnter()
		{
			if (m_PrefabName == null)
			{
				Finish();
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(m_ParentObject);
			if (m_VolumeScale.IsNone)
			{
				SoundManager.Get().LoadAndPlay(m_PrefabName.Value, ownerDefaultTarget);
			}
			else
			{
				SoundManager.Get().LoadAndPlay(m_PrefabName.Value, ownerDefaultTarget, m_VolumeScale.Value);
			}
			Finish();
		}
	}
}
