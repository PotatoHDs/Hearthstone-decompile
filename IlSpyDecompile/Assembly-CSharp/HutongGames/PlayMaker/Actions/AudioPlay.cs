using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[ActionTarget(typeof(AudioSource), "gameObject", false)]
	[ActionTarget(typeof(AudioClip), "oneShotClip", false)]
	[Tooltip("Plays the Audio Clip set with Set Audio Clip or in the Audio Source inspector on a Game Object. Optionally plays a one shot Audio Clip.")]
	public class AudioPlay : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with an AudioSource component.")]
		public FsmOwnerDefault gameObject;

		[HasFloatSlider(0f, 1f)]
		[Tooltip("Set the volume.")]
		public FsmFloat volume;

		[ObjectType(typeof(AudioClip))]
		[Tooltip("Optionally play a 'one shot' AudioClip. NOTE: Volume cannot be adjusted while playing a 'one shot' AudioClip.")]
		public FsmObject oneShotClip;

		[Tooltip("Wait until the end of the clip to send the Finish Event. Set to false to send the finish event immediately.")]
		public FsmBool WaitForEndOfClip;

		[Tooltip("Event to send when the action finishes.")]
		public FsmEvent finishedEvent;

		private AudioSource audio;

		public override void Reset()
		{
			gameObject = null;
			volume = 1f;
			oneShotClip = null;
			finishedEvent = null;
			WaitForEndOfClip = true;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget != null)
			{
				audio = ownerDefaultTarget.GetComponent<AudioSource>();
				if (audio != null)
				{
					AudioClip audioClip = oneShotClip.Value as AudioClip;
					if (audioClip == null)
					{
						audio.Play();
						if (!volume.IsNone)
						{
							audio.volume = volume.Value;
						}
						return;
					}
					if (!volume.IsNone)
					{
						audio.PlayOneShot(audioClip, volume.Value);
					}
					else
					{
						audio.PlayOneShot(audioClip);
					}
					if (!WaitForEndOfClip.Value)
					{
						base.Fsm.Event(finishedEvent);
						Finish();
					}
					return;
				}
			}
			Finish();
		}

		public override void OnUpdate()
		{
			if (audio == null)
			{
				Finish();
			}
			else if (!audio.isPlaying)
			{
				base.Fsm.Event(finishedEvent);
				Finish();
			}
			else if (!volume.IsNone && volume.Value != audio.volume)
			{
				audio.volume = volume.Value;
			}
		}
	}
}
