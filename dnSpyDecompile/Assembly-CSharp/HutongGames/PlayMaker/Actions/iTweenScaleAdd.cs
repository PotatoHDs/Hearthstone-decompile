using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CE1 RID: 3297
	[ActionCategory("iTween")]
	[Tooltip("Adds to a GameObject's scale over time.")]
	public class iTweenScaleAdd : iTweenFsmAction
	{
		// Token: 0x0600A150 RID: 41296 RVA: 0x003366EC File Offset: 0x003348EC
		public override void Reset()
		{
			base.Reset();
			this.id = new FsmString
			{
				UseVariable = true
			};
			this.time = 1f;
			this.delay = 0f;
			this.loopType = iTween.LoopType.none;
			this.vector = new FsmVector3
			{
				UseVariable = true
			};
			this.speed = new FsmFloat
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A151 RID: 41297 RVA: 0x0033675C File Offset: 0x0033495C
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A152 RID: 41298 RVA: 0x0033677F File Offset: 0x0033497F
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A153 RID: 41299 RVA: 0x00336790 File Offset: 0x00334990
		private void DoiTween()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector = Vector3.zero;
			if (!this.vector.IsNone)
			{
				vector = this.vector.Value;
			}
			this.itweenType = "scale";
			iTween.ScaleAdd(ownerDefaultTarget, iTween.Hash(new object[]
			{
				"amount",
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
				!this.realTime.IsNone && this.realTime.Value
			}));
		}

		// Token: 0x04008738 RID: 34616
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008739 RID: 34617
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x0400873A RID: 34618
		[RequiredField]
		[Tooltip("A vector that will be added to the GameObjects scale.")]
		public FsmVector3 vector;

		// Token: 0x0400873B RID: 34619
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x0400873C RID: 34620
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x0400873D RID: 34621
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x0400873E RID: 34622
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x0400873F RID: 34623
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;
	}
}
