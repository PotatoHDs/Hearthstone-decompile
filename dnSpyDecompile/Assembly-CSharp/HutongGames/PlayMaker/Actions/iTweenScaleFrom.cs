using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CE3 RID: 3299
	[ActionCategory("iTween")]
	[Tooltip("Instantly changes a GameObject's scale then returns it to it's starting scale over time.")]
	public class iTweenScaleFrom : iTweenFsmAction
	{
		// Token: 0x0600A15A RID: 41306 RVA: 0x00336C0C File Offset: 0x00334E0C
		public override void Reset()
		{
			base.Reset();
			this.id = new FsmString
			{
				UseVariable = true
			};
			this.transformScale = new FsmGameObject
			{
				UseVariable = true
			};
			this.vectorScale = new FsmVector3
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
		}

		// Token: 0x0600A15B RID: 41307 RVA: 0x00336C8E File Offset: 0x00334E8E
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A15C RID: 41308 RVA: 0x00336CB1 File Offset: 0x00334EB1
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A15D RID: 41309 RVA: 0x00336CC0 File Offset: 0x00334EC0
		private void DoiTween()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector = this.vectorScale.IsNone ? Vector3.zero : this.vectorScale.Value;
			if (!this.transformScale.IsNone && this.transformScale.Value)
			{
				vector = this.transformScale.Value.transform.localScale + vector;
			}
			this.itweenType = "scale";
			iTween.ScaleFrom(ownerDefaultTarget, iTween.Hash(new object[]
			{
				"scale",
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

		// Token: 0x04008748 RID: 34632
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008749 RID: 34633
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x0400874A RID: 34634
		[Tooltip("Scale From a transform scale.")]
		public FsmGameObject transformScale;

		// Token: 0x0400874B RID: 34635
		[Tooltip("A scale vector the GameObject will animate From.")]
		public FsmVector3 vectorScale;

		// Token: 0x0400874C RID: 34636
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x0400874D RID: 34637
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x0400874E RID: 34638
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x0400874F RID: 34639
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x04008750 RID: 34640
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;
	}
}
