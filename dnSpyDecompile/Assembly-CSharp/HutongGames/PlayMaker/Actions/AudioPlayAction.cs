using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F1A RID: 3866
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays the Audio Clip on a Game Object or plays a one shot clip. Waits for the audio to finish.")]
	public class AudioPlayAction : FsmStateAction
	{
		// Token: 0x0600ABE6 RID: 44006 RVA: 0x0035A79A File Offset: 0x0035899A
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_VolumeScale = 1f;
			this.m_Delay = 0f;
			this.m_DelayTime = 0f;
			this.m_OneShotSound = null;
		}

		// Token: 0x0600ABE7 RID: 44007 RVA: 0x0035A7D0 File Offset: 0x003589D0
		public override void OnEnter()
		{
			this.m_DelayTime = this.m_Delay;
			base.StartCoroutine(this.Delay());
		}

		// Token: 0x0600ABE8 RID: 44008 RVA: 0x0035A7EC File Offset: 0x003589EC
		private AudioSource GetSource()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				return null;
			}
			return ownerDefaultTarget.GetComponent<AudioSource>();
		}

		// Token: 0x0600ABE9 RID: 44009 RVA: 0x0035A81C File Offset: 0x00358A1C
		private void Play()
		{
			AudioSource source = this.GetSource();
			if (source == null)
			{
				base.Fsm.Event(this.m_FinishedEvent);
				base.Finish();
				return;
			}
			SoundDef soundDef = this.m_OneShotSound.Value as SoundDef;
			if (soundDef == null)
			{
				if (!this.m_VolumeScale.IsNone)
				{
					SoundManager.Get().SetVolume(source, this.m_VolumeScale.Value);
				}
				SoundManager.Get().Play(source, null, null, null);
			}
			else
			{
				SoundPlayClipArgs soundPlayClipArgs = new SoundPlayClipArgs();
				soundPlayClipArgs.m_templateSource = source;
				soundPlayClipArgs.m_def = soundDef;
				if (!this.m_VolumeScale.IsNone)
				{
					soundPlayClipArgs.m_volume = new float?(this.m_VolumeScale.Value);
				}
				soundPlayClipArgs.m_parentObject = source.gameObject;
				if (SoundManager.Get() != null)
				{
					SoundManager.Get().PlayClip(soundPlayClipArgs, true, null);
				}
			}
			if (!SoundManager.Get().IsActive(source))
			{
				base.Fsm.Event(this.m_FinishedEvent);
				base.Finish();
			}
		}

		// Token: 0x0600ABEA RID: 44010 RVA: 0x0035A91B File Offset: 0x00358B1B
		private IEnumerator Delay()
		{
			while (this.m_DelayTime > 0f)
			{
				this.m_DelayTime -= Time.deltaTime;
				yield return null;
			}
			this.Play();
			AudioSource source = this.GetSource();
			while (SoundManager.Get().IsActive(source))
			{
				yield return null;
			}
			Debug.Log("Finish");
			base.Fsm.Event(this.m_FinishedEvent);
			base.Finish();
			yield break;
		}

		// Token: 0x040092A5 RID: 37541
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with the AudioSource component.")]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040092A6 RID: 37542
		[Tooltip("Scales the volume of the AudioSource just for this Play call.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_VolumeScale;

		// Token: 0x040092A7 RID: 37543
		public float m_Delay;

		// Token: 0x040092A8 RID: 37544
		[ObjectType(typeof(SoundDef))]
		[Tooltip("Optionally play a one shot AudioClip.")]
		public FsmObject m_OneShotSound;

		// Token: 0x040092A9 RID: 37545
		[Tooltip("Event to send when the AudioSource finishes playing.")]
		public FsmEvent m_FinishedEvent;

		// Token: 0x040092AA RID: 37546
		private float m_DelayTime;
	}
}
