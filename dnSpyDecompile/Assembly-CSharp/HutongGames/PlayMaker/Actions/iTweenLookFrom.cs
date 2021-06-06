using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CCF RID: 3279
	[ActionCategory("iTween")]
	[Tooltip("Instantly rotates a GameObject to look at the supplied Vector3 then returns it to it's starting rotation over time.")]
	public class iTweenLookFrom : iTweenFsmAction
	{
		// Token: 0x0600A0F4 RID: 41204 RVA: 0x00332C80 File Offset: 0x00330E80
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

		// Token: 0x0600A0F5 RID: 41205 RVA: 0x00332D09 File Offset: 0x00330F09
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A0F6 RID: 41206 RVA: 0x00332D2C File Offset: 0x00330F2C
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A0F7 RID: 41207 RVA: 0x00332D3C File Offset: 0x00330F3C
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
			iTween.LookFrom(ownerDefaultTarget, iTween.Hash(new object[]
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

		// Token: 0x04008687 RID: 34439
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008688 RID: 34440
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x04008689 RID: 34441
		[Tooltip("Look from a transform position.")]
		public FsmGameObject transformTarget;

		// Token: 0x0400868A RID: 34442
		[Tooltip("A target position the GameObject will look at. If Transform Target is defined this is used as a local offset.")]
		public FsmVector3 vectorTarget;

		// Token: 0x0400868B RID: 34443
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x0400868C RID: 34444
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x0400868D RID: 34445
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x0400868E RID: 34446
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x0400868F RID: 34447
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x04008690 RID: 34448
		[Tooltip("Restricts rotation to the supplied axis only.")]
		public iTweenFsmAction.AxisRestriction axis;
	}
}
