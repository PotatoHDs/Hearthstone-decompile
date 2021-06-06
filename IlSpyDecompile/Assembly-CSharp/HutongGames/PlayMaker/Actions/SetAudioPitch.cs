using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Sets the Pitch of the Audio Clip played by the AudioSource component on a Game Object.")]
	public class SetAudioPitch : ComponentAction<AudioSource>
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		public FsmOwnerDefault gameObject;

		public FsmFloat pitch;

		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			pitch = 1f;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetAudioPitch();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetAudioPitch();
		}

		private void DoSetAudioPitch()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget) && !pitch.IsNone)
			{
				base.audio.pitch = pitch.Value;
			}
		}
	}
}
