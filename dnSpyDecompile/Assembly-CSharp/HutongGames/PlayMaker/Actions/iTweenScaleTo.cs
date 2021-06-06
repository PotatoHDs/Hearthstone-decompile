using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CE4 RID: 3300
	[ActionCategory("iTween")]
	[Tooltip("Changes a GameObject's scale over time.")]
	public class iTweenScaleTo : iTweenFsmAction
	{
		// Token: 0x0600A15F RID: 41311 RVA: 0x00336EE8 File Offset: 0x003350E8
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

		// Token: 0x0600A160 RID: 41312 RVA: 0x00336F6A File Offset: 0x0033516A
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A161 RID: 41313 RVA: 0x00336F8D File Offset: 0x0033518D
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A162 RID: 41314 RVA: 0x00336F9C File Offset: 0x0033519C
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
			iTween.ScaleTo(ownerDefaultTarget, iTween.Hash(new object[]
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

		// Token: 0x04008751 RID: 34641
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008752 RID: 34642
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x04008753 RID: 34643
		[Tooltip("Scale To a transform scale.")]
		public FsmGameObject transformScale;

		// Token: 0x04008754 RID: 34644
		[Tooltip("A scale vector the GameObject will animate To.")]
		public FsmVector3 vectorScale;

		// Token: 0x04008755 RID: 34645
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x04008756 RID: 34646
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x04008757 RID: 34647
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x04008758 RID: 34648
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x04008759 RID: 34649
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;
	}
}
