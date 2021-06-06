using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CD0 RID: 3280
	[ActionCategory("iTween")]
	[Tooltip("Rotates a GameObject to look at a supplied Transform or Vector3 over time.")]
	public class iTweenLookTo : iTweenFsmAction
	{
		// Token: 0x0600A0F9 RID: 41209 RVA: 0x00332F9C File Offset: 0x0033119C
		public override void Reset()
		{
			base.Reset();
			this.id = new FsmString
			{
				UseVariable = true
			};
			this.transformTarget = new FsmGameObject
			{
				UseVariable = true
			};
			this.vectorTarget = new FsmVector3
			{
				UseVariable = true
			};
			this.time = 1f;
			this.delay = 0f;
			this.loopType = iTween.LoopType.none;
			this.speed = new FsmFloat
			{
				UseVariable = true
			};
			this.axis = iTweenFsmAction.AxisRestriction.none;
		}

		// Token: 0x0600A0FA RID: 41210 RVA: 0x00333025 File Offset: 0x00331225
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A0FB RID: 41211 RVA: 0x00333048 File Offset: 0x00331248
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A0FC RID: 41212 RVA: 0x00333058 File Offset: 0x00331258
		private void DoiTween()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector = this.vectorTarget.IsNone ? Vector3.zero : this.vectorTarget.Value;
			if (!this.transformTarget.IsNone && this.transformTarget.Value)
			{
				vector = this.transformTarget.Value.transform.position + vector;
			}
			this.itweenType = "rotate";
			iTween.LookTo(ownerDefaultTarget, iTween.Hash(new object[]
			{
				"looktarget",
				vector,
				"name",
				this.id.IsNone ? "" : this.id.Value,
				this.speed.IsNone ? "time" : "speed",
				this.speed.IsNone ? (this.time.IsNone ? 1f : this.time.Value) : this.speed.Value,
				"delay",
				this.delay.IsNone ? 0f : this.delay.Value,
				"easetype",
				this.easeType,
				"looptype",
				this.loopType,
				"oncomplete",
				"iTweenOnComplete",
				"oncompleteparams",
				this.itweenID,
				"onstart",
				"iTweenOnStart",
				"onstartparams",
				this.itweenID,
				"ignoretimescale",
				!this.realTime.IsNone && this.realTime.Value,
				"axis",
				(this.axis == iTweenFsmAction.AxisRestriction.none) ? "" : Enum.GetName(typeof(iTweenFsmAction.AxisRestriction), this.axis)
			}));
		}

		// Token: 0x04008691 RID: 34449
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008692 RID: 34450
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x04008693 RID: 34451
		[Tooltip("Look at a transform position.")]
		public FsmGameObject transformTarget;

		// Token: 0x04008694 RID: 34452
		[Tooltip("A target position the GameObject will look at. If Transform Target is defined this is used as a local offset.")]
		public FsmVector3 vectorTarget;

		// Token: 0x04008695 RID: 34453
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x04008696 RID: 34454
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x04008697 RID: 34455
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x04008698 RID: 34456
		[Tooltip("For the shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x04008699 RID: 34457
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x0400869A RID: 34458
		[Tooltip("Restricts rotation to the supplied axis only. Just put there strinc like 'x' or 'xz'")]
		public iTweenFsmAction.AxisRestriction axis;
	}
}
