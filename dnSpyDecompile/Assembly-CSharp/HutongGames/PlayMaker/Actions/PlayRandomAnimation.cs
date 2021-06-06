using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D32 RID: 3378
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Plays a Random Animation on a Game Object. You can set the relative weight of each animation to control how often they are selected.")]
	public class PlayRandomAnimation : BaseAnimationAction
	{
		// Token: 0x0600A2EB RID: 41707 RVA: 0x0033DEF4 File Offset: 0x0033C0F4
		public override void Reset()
		{
			this.gameObject = null;
			this.animations = new FsmString[0];
			this.weights = new FsmFloat[0];
			this.playMode = PlayMode.StopAll;
			this.blendTime = 0.3f;
			this.finishEvent = null;
			this.loopEvent = null;
			this.stopOnExit = false;
		}

		// Token: 0x0600A2EC RID: 41708 RVA: 0x0033DF4C File Offset: 0x0033C14C
		public override void OnEnter()
		{
			this.DoPlayRandomAnimation();
		}

		// Token: 0x0600A2ED RID: 41709 RVA: 0x0033DF54 File Offset: 0x0033C154
		private void DoPlayRandomAnimation()
		{
			if (this.animations.Length != 0)
			{
				int randomWeightedIndex = ActionHelpers.GetRandomWeightedIndex(this.weights);
				if (randomWeightedIndex != -1)
				{
					this.DoPlayAnimation(this.animations[randomWeightedIndex].Value);
				}
			}
		}

		// Token: 0x0600A2EE RID: 41710 RVA: 0x0033DF90 File Offset: 0x0033C190
		private void DoPlayAnimation(string animName)
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				base.Finish();
				return;
			}
			if (string.IsNullOrEmpty(animName))
			{
				base.LogWarning("Missing animName!");
				base.Finish();
				return;
			}
			this.anim = base.animation[animName];
			if (this.anim == null)
			{
				base.LogWarning("Missing animation: " + animName);
				base.Finish();
				return;
			}
			float value = this.blendTime.Value;
			if (value < 0.001f)
			{
				base.animation.Play(animName, this.playMode);
			}
			else
			{
				base.animation.CrossFade(animName, value, this.playMode);
			}
			this.prevAnimtTime = this.anim.time;
		}

		// Token: 0x0600A2EF RID: 41711 RVA: 0x0033E060 File Offset: 0x0033C260
		public override void OnUpdate()
		{
			if (base.Fsm.GetOwnerDefaultTarget(this.gameObject) == null || this.anim == null)
			{
				return;
			}
			if (!this.anim.enabled || (this.anim.wrapMode == WrapMode.ClampForever && this.anim.time > this.anim.length))
			{
				base.Fsm.Event(this.finishEvent);
				base.Finish();
			}
			if (this.anim.wrapMode != WrapMode.ClampForever && this.anim.time > this.anim.length && this.prevAnimtTime < this.anim.length)
			{
				base.Fsm.Event(this.loopEvent);
			}
		}

		// Token: 0x0600A2F0 RID: 41712 RVA: 0x0033E129 File Offset: 0x0033C329
		public override void OnExit()
		{
			if (this.stopOnExit)
			{
				this.StopAnimation();
			}
		}

		// Token: 0x0600A2F1 RID: 41713 RVA: 0x0033E139 File Offset: 0x0033C339
		private void StopAnimation()
		{
			if (base.animation != null)
			{
				base.animation.Stop(this.anim.name);
			}
		}

		// Token: 0x04008944 RID: 35140
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008945 RID: 35141
		[CompoundArray("Animations", "Animation", "Weight")]
		[UIHint(UIHint.Animation)]
		public FsmString[] animations;

		// Token: 0x04008946 RID: 35142
		[HasFloatSlider(0f, 1f)]
		public FsmFloat[] weights;

		// Token: 0x04008947 RID: 35143
		[Tooltip("How to treat previously playing animations.")]
		public PlayMode playMode;

		// Token: 0x04008948 RID: 35144
		[HasFloatSlider(0f, 5f)]
		[Tooltip("Time taken to blend to this animation.")]
		public FsmFloat blendTime;

		// Token: 0x04008949 RID: 35145
		[Tooltip("Event to send when the animation is finished playing. NOTE: Not sent with Loop or PingPong wrap modes!")]
		public FsmEvent finishEvent;

		// Token: 0x0400894A RID: 35146
		[Tooltip("Event to send when the animation loops. If you want to send this event to another FSM use Set Event Target. NOTE: This event is only sent with Loop and PingPong wrap modes.")]
		public FsmEvent loopEvent;

		// Token: 0x0400894B RID: 35147
		[Tooltip("Stop playing the animation when this state is exited.")]
		public bool stopOnExit;

		// Token: 0x0400894C RID: 35148
		private AnimationState anim;

		// Token: 0x0400894D RID: 35149
		private float prevAnimtTime;
	}
}
