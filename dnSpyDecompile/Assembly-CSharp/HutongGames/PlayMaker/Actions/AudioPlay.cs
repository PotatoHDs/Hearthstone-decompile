using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BD3 RID: 3027
	[ActionCategory(ActionCategory.Audio)]
	[ActionTarget(typeof(AudioSource), "gameObject", false)]
	[ActionTarget(typeof(AudioClip), "oneShotClip", false)]
	[Tooltip("Plays the Audio Clip set with Set Audio Clip or in the Audio Source inspector on a Game Object. Optionally plays a one shot Audio Clip.")]
	public class AudioPlay : FsmStateAction
	{
		// Token: 0x06009CB4 RID: 40116 RVA: 0x00326398 File Offset: 0x00324598
		public override void Reset()
		{
			this.gameObject = null;
			this.volume = 1f;
			this.oneShotClip = null;
			this.finishedEvent = null;
			this.WaitForEndOfClip = true;
		}

		// Token: 0x06009CB5 RID: 40117 RVA: 0x003263CC File Offset: 0x003245CC
		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget != null)
			{
				this.audio = ownerDefaultTarget.GetComponent<AudioSource>();
				if (this.audio != null)
				{
					AudioClip audioClip = this.oneShotClip.Value as AudioClip;
					if (audioClip == null)
					{
						this.audio.Play();
						if (!this.volume.IsNone)
						{
							this.audio.volume = this.volume.Value;
						}
						return;
					}
					if (!this.volume.IsNone)
					{
						this.audio.PlayOneShot(audioClip, this.volume.Value);
					}
					else
					{
						this.audio.PlayOneShot(audioClip);
					}
					if (!this.WaitForEndOfClip.Value)
					{
						base.Fsm.Event(this.finishedEvent);
						base.Finish();
					}
					return;
				}
			}
			base.Finish();
		}

		// Token: 0x06009CB6 RID: 40118 RVA: 0x003264BC File Offset: 0x003246BC
		public override void OnUpdate()
		{
			if (this.audio == null)
			{
				base.Finish();
				return;
			}
			if (!this.audio.isPlaying)
			{
				base.Fsm.Event(this.finishedEvent);
				base.Finish();
				return;
			}
			if (!this.volume.IsNone && this.volume.Value != this.audio.volume)
			{
				this.audio.volume = this.volume.Value;
			}
		}

		// Token: 0x04008229 RID: 33321
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with an AudioSource component.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400822A RID: 33322
		[HasFloatSlider(0f, 1f)]
		[Tooltip("Set the volume.")]
		public FsmFloat volume;

		// Token: 0x0400822B RID: 33323
		[ObjectType(typeof(AudioClip))]
		[Tooltip("Optionally play a 'one shot' AudioClip. NOTE: Volume cannot be adjusted while playing a 'one shot' AudioClip.")]
		public FsmObject oneShotClip;

		// Token: 0x0400822C RID: 33324
		[Tooltip("Wait until the end of the clip to send the Finish Event. Set to false to send the finish event immediately.")]
		public FsmBool WaitForEndOfClip;

		// Token: 0x0400822D RID: 33325
		[Tooltip("Event to send when the action finishes.")]
		public FsmEvent finishedEvent;

		// Token: 0x0400822E RID: 33326
		private AudioSource audio;
	}
}
