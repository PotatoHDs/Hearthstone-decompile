using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F1D RID: 3869
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays the Audio Clip on a Game Object or plays a one shot clip. Does not wait for the audio to finish.")]
	public class AudioPlaythroughAction : FsmStateAction
	{
		// Token: 0x0600ABFA RID: 44026 RVA: 0x0035AE78 File Offset: 0x00359078
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_VolumeScale = 1f;
			this.m_OneShotClip = null;
			this.m_Delay = 0f;
			this.m_DelayTime = 0f;
			this.m_OneShotSound = null;
		}

		// Token: 0x0600ABFB RID: 44027 RVA: 0x0035AEB5 File Offset: 0x003590B5
		public override void OnEnter()
		{
			if (this.m_Delay > 0f)
			{
				this.m_DelayTime = this.m_Delay;
				base.StartCoroutine(this.Delay());
				return;
			}
			this.Play();
		}

		// Token: 0x0600ABFC RID: 44028 RVA: 0x0035AEE4 File Offset: 0x003590E4
		private AudioSource GetSource()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				return null;
			}
			return ownerDefaultTarget.GetComponent<AudioSource>();
		}

		// Token: 0x0600ABFD RID: 44029 RVA: 0x0035AF14 File Offset: 0x00359114
		private void Play()
		{
			AudioSource source = this.GetSource();
			if (source == null)
			{
				base.Fsm.Event(this.m_FinishedEvent);
				base.Finish();
				return;
			}
			if (this.m_IsMinionSummonVO)
			{
				Actor actor = this.GetActor();
				if (actor != null && actor.GetEntity() != null && actor.GetEntity().HasTag(GAME_TAG.SUPPRESS_ALL_SUMMON_VO))
				{
					base.Fsm.Event(this.m_FinishedEvent);
					base.Finish();
					return;
				}
			}
			SoundManager.SoundOptions options = new SoundManager.SoundOptions
			{
				InstanceLimited = this.m_InstanceLimited,
				InstanceTimeLimit = this.m_InstanceLimitedDuration,
				MaxInstancesOfThisSound = this.m_InstanceLimitMaximum
			};
			SoundDef soundDef = this.m_OneShotSound.Value as SoundDef;
			if (soundDef == null)
			{
				if (!this.m_VolumeScale.IsNone)
				{
					SoundManager.Get().SetVolume(source, this.m_VolumeScale.Value);
				}
				SoundManager.Get().Play(source, null, null, options);
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
					SoundManager.Get().PlayClip(soundPlayClipArgs, true, options);
				}
			}
			base.Fsm.Event(this.m_FinishedEvent);
			base.Finish();
		}

		// Token: 0x0600ABFE RID: 44030 RVA: 0x0035B080 File Offset: 0x00359280
		private IEnumerator Delay()
		{
			while (this.m_DelayTime > 0f)
			{
				this.m_DelayTime -= Time.deltaTime;
				yield return null;
			}
			this.Play();
			yield break;
		}

		// Token: 0x0600ABFF RID: 44031 RVA: 0x0035B090 File Offset: 0x00359290
		protected Actor GetActor()
		{
			Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.Owner);
			if (actor == null)
			{
				Card card = SceneUtils.FindComponentInThisOrParents<Card>(base.Owner);
				if (card != null)
				{
					actor = card.GetActor();
				}
			}
			return actor;
		}

		// Token: 0x040092C7 RID: 37575
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with the AudioSource component.")]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x040092C8 RID: 37576
		[Tooltip("Scales the volume of the AudioSource just for this Play call.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_VolumeScale;

		// Token: 0x040092C9 RID: 37577
		[ObjectType(typeof(AudioClip))]
		[Tooltip("Optionally play a One Shot AudioClip.")]
		public FsmObject m_OneShotClip;

		// Token: 0x040092CA RID: 37578
		public float m_Delay;

		// Token: 0x040092CB RID: 37579
		[ObjectType(typeof(SoundDef))]
		[Tooltip("Optionally play a one shot AudioClip.")]
		public FsmObject m_OneShotSound;

		// Token: 0x040092CC RID: 37580
		[Tooltip("Event to send when the AudioSource finishes playing.")]
		public FsmEvent m_FinishedEvent;

		// Token: 0x040092CD RID: 37581
		[Tooltip("If true, this audio clip will be suppressed by SUPPRESS_ALL_SUMMON_VO")]
		public bool m_IsMinionSummonVO;

		// Token: 0x040092CE RID: 37582
		[Tooltip("If true, there will be a limit to the number instances of of this sound that can play at once.")]
		public bool m_InstanceLimited;

		// Token: 0x040092CF RID: 37583
		[Tooltip("If instance limited, this defines the duration that each clip will prevent another from playing.  If zero, then it will measure the length of the audio file.")]
		public float m_InstanceLimitedDuration;

		// Token: 0x040092D0 RID: 37584
		[Tooltip("If instance limited, this defines the maximum number of instances of the sound that can be playing at once.")]
		public int m_InstanceLimitMaximum = 1;

		// Token: 0x040092D1 RID: 37585
		private float m_DelayTime;
	}
}
