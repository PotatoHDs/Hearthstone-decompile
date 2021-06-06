using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Sets the Volume of the Audio Clip played by the AudioSource component on a Game Object.")]
	public class SetAudioVolume : ComponentAction<AudioSource>
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;

		[HasFloatSlider(0f, 1f)]
		public FsmFloat volume;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			volume = 1f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetAudioVolume();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetAudioVolume();
		}

		private void DoSetAudioVolume()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget) && !volume.IsNone)
			{
				base.audio.volume = volume.Value;
			}
		}
	}
}
