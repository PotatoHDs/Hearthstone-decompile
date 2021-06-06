using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F10 RID: 3856
	[ActionCategory("Pegasus")]
	[Tooltip("Plays an Animation on a Game Object and waits for the animation to finish.")]
	public class AnimationPlayAction : FsmStateAction
	{
		// Token: 0x0600ABBD RID: 43965 RVA: 0x00359B80 File Offset: 0x00357D80
		public override void Reset()
		{
			this.m_GameObject = null;
			this.m_AnimName = null;
			this.m_PhoneAnimName = null;
			this.m_PlayMode = PlayMode.StopAll;
			this.m_CrossFadeSec = 0.3f;
			this.m_FinishEvent = null;
			this.m_LoopEvent = null;
			this.m_StopOnExit = false;
		}

		// Token: 0x0600ABBE RID: 43966 RVA: 0x00359BCE File Offset: 0x00357DCE
		public override void OnEnter()
		{
			if (!this.CacheAnim())
			{
				base.Finish();
				return;
			}
			this.StartAnimation();
		}

		// Token: 0x0600ABBF RID: 43967 RVA: 0x00359BE8 File Offset: 0x00357DE8
		public override void OnUpdate()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				base.Finish();
				return;
			}
			if (ownerDefaultTarget.GetComponent<Animation>() == null)
			{
				Debug.LogWarning(string.Format("AnimationPlayAction.OnUpdate() - GameObject {0} is missing an animation component", ownerDefaultTarget));
				base.Finish();
				return;
			}
			if (!this.m_animState.enabled || (this.m_animState.wrapMode == WrapMode.ClampForever && this.m_animState.time > this.m_animState.length))
			{
				base.Fsm.Event(this.m_FinishEvent);
				base.Finish();
			}
			if (this.m_animState.wrapMode != WrapMode.ClampForever && this.m_animState.time > this.m_animState.length && this.m_prevAnimTime < this.m_animState.length)
			{
				base.Fsm.Event(this.m_LoopEvent);
			}
		}

		// Token: 0x0600ABC0 RID: 43968 RVA: 0x00359CD0 File Offset: 0x00357ED0
		public override void OnExit()
		{
			if (this.m_StopOnExit)
			{
				this.StopAnimation();
			}
		}

		// Token: 0x0600ABC1 RID: 43969 RVA: 0x00359CE0 File Offset: 0x00357EE0
		private bool CacheAnim()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			this.m_animName = this.m_AnimName.Value;
			if (UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(this.m_PhoneAnimName.Value))
			{
				this.m_animName = this.m_PhoneAnimName.Value;
			}
			this.m_animState = ownerDefaultTarget.GetComponent<Animation>()[this.m_animName];
			return true;
		}

		// Token: 0x0600ABC2 RID: 43970 RVA: 0x00359D58 File Offset: 0x00357F58
		private void StartAnimation()
		{
			if (base.Fsm != null)
			{
				GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
				if (ownerDefaultTarget == null)
				{
					return;
				}
				Animation component = ownerDefaultTarget.GetComponent<Animation>();
				if (component == null)
				{
					return;
				}
				float num = (this.m_CrossFadeSec == null) ? 0f : this.m_CrossFadeSec.Value;
				if (num <= Mathf.Epsilon)
				{
					component.Play(this.m_animName, this.m_PlayMode);
				}
				else
				{
					component.CrossFade(this.m_animName, num, this.m_PlayMode);
				}
				this.m_prevAnimTime = ((this.m_animState == null) ? 0f : this.m_animState.time);
			}
		}

		// Token: 0x0600ABC3 RID: 43971 RVA: 0x00359E10 File Offset: 0x00358010
		private void StopAnimation()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.m_GameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			if (ownerDefaultTarget.GetComponent<Animation>() == null)
			{
				return;
			}
			ownerDefaultTarget.GetComponent<Animation>().Stop(this.m_animName);
		}

		// Token: 0x0400926F RID: 37487
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault m_GameObject;

		// Token: 0x04009270 RID: 37488
		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation to play.")]
		public FsmString m_AnimName;

		// Token: 0x04009271 RID: 37489
		public FsmString m_PhoneAnimName;

		// Token: 0x04009272 RID: 37490
		[Tooltip("How to treat previously playing animations.")]
		public PlayMode m_PlayMode;

		// Token: 0x04009273 RID: 37491
		[HasFloatSlider(0f, 5f)]
		[Tooltip("Time taken to cross fade to this animation.")]
		public FsmFloat m_CrossFadeSec;

		// Token: 0x04009274 RID: 37492
		[Tooltip("Event to send when the animation is finished playing. NOTE: Not sent with Loop or PingPong wrap modes!")]
		public FsmEvent m_FinishEvent;

		// Token: 0x04009275 RID: 37493
		[Tooltip("Event to send when the animation loops. If you want to send this event to another FSM use Set Event Target. NOTE: This event is only sent with Loop and PingPong wrap modes.")]
		public FsmEvent m_LoopEvent;

		// Token: 0x04009276 RID: 37494
		[Tooltip("Stop playing the animation when this state is exited.")]
		public bool m_StopOnExit;

		// Token: 0x04009277 RID: 37495
		private string m_animName;

		// Token: 0x04009278 RID: 37496
		private AnimationState m_animState;

		// Token: 0x04009279 RID: 37497
		private float m_prevAnimTime;
	}
}
