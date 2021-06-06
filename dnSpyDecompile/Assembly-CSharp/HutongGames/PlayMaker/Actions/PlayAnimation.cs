using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D27 RID: 3367
	[ActionCategory(ActionCategory.Animation)]
	[Tooltip("Plays an Animation on a Game Object. You can add named animation clips to the object in the Unity editor, or with the Add Animation Clip action.")]
	public class PlayAnimation : BaseAnimationAction
	{
		// Token: 0x0600A2C6 RID: 41670 RVA: 0x0033D798 File Offset: 0x0033B998
		public override void Reset()
		{
			this.gameObject = null;
			this.animName = null;
			this.playMode = PlayMode.StopAll;
			this.blendTime = 0.3f;
			this.finishEvent = null;
			this.loopEvent = null;
			this.stopOnExit = false;
		}

		// Token: 0x0600A2C7 RID: 41671 RVA: 0x0033D7D4 File Offset: 0x0033B9D4
		public override void OnEnter()
		{
			this.DoPlayAnimation();
		}

		// Token: 0x0600A2C8 RID: 41672 RVA: 0x0033D7DC File Offset: 0x0033B9DC
		private void DoPlayAnimation()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				base.Finish();
				return;
			}
			if (string.IsNullOrEmpty(this.animName.Value))
			{
				base.LogWarning("Missing animName!");
				base.Finish();
				return;
			}
			this.anim = base.animation[this.animName.Value];
			if (this.anim == null)
			{
				base.LogWarning("Missing animation: " + this.animName.Value);
				base.Finish();
				return;
			}
			float value = this.blendTime.Value;
			if (value < 0.001f)
			{
				base.animation.Play(this.animName.Value, this.playMode);
			}
			else
			{
				base.animation.CrossFade(this.animName.Value, value, this.playMode);
			}
			this.prevAnimtTime = this.anim.time;
		}

		// Token: 0x0600A2C9 RID: 41673 RVA: 0x0033D8DC File Offset: 0x0033BADC
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

		// Token: 0x0600A2CA RID: 41674 RVA: 0x0033D9A5 File Offset: 0x0033BBA5
		public override void OnExit()
		{
			if (this.stopOnExit)
			{
				this.StopAnimation();
			}
		}

		// Token: 0x0600A2CB RID: 41675 RVA: 0x0033D9B5 File Offset: 0x0033BBB5
		private void StopAnimation()
		{
			if (base.animation != null)
			{
				base.animation.Stop(this.animName.Value);
			}
		}

		// Token: 0x04008928 RID: 35112
		[RequiredField]
		[CheckForComponent(typeof(Animation))]
		[Tooltip("Game Object to play the animation on.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008929 RID: 35113
		[UIHint(UIHint.Animation)]
		[Tooltip("The name of the animation to play.")]
		public FsmString animName;

		// Token: 0x0400892A RID: 35114
		[Tooltip("How to treat previously playing animations.")]
		public PlayMode playMode;

		// Token: 0x0400892B RID: 35115
		[HasFloatSlider(0f, 5f)]
		[Tooltip("Time taken to blend to this animation.")]
		public FsmFloat blendTime;

		// Token: 0x0400892C RID: 35116
		[Tooltip("Event to send when the animation is finished playing. NOTE: Not sent with Loop or PingPong wrap modes!")]
		public FsmEvent finishEvent;

		// Token: 0x0400892D RID: 35117
		[Tooltip("Event to send when the animation loops. If you want to send this event to another FSM use Set Event Target. NOTE: This event is only sent with Loop and PingPong wrap modes.")]
		public FsmEvent loopEvent;

		// Token: 0x0400892E RID: 35118
		[Tooltip("Stop playing the animation when this state is exited.")]
		public bool stopOnExit;

		// Token: 0x0400892F RID: 35119
		private AnimationState anim;

		// Token: 0x04008930 RID: 35120
		private float prevAnimtTime;
	}
}
