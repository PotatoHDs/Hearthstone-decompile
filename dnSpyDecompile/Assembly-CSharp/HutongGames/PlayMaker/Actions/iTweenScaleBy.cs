using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CE2 RID: 3298
	[ActionCategory("iTween")]
	[Tooltip("Multiplies a GameObject's scale over time.")]
	public class iTweenScaleBy : iTweenFsmAction
	{
		// Token: 0x0600A155 RID: 41301 RVA: 0x0033697C File Offset: 0x00334B7C
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

		// Token: 0x0600A156 RID: 41302 RVA: 0x003369EC File Offset: 0x00334BEC
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A157 RID: 41303 RVA: 0x00336A0F File Offset: 0x00334C0F
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A158 RID: 41304 RVA: 0x00336A20 File Offset: 0x00334C20
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
			iTween.ScaleBy(ownerDefaultTarget, iTween.Hash(new object[]
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

		// Token: 0x04008740 RID: 34624
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008741 RID: 34625
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x04008742 RID: 34626
		[RequiredField]
		[Tooltip("A vector that will multiply current GameObjects scale.")]
		public FsmVector3 vector;

		// Token: 0x04008743 RID: 34627
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x04008744 RID: 34628
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x04008745 RID: 34629
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x04008746 RID: 34630
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x04008747 RID: 34631
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;
	}
}
