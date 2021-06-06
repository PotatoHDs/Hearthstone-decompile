using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CDC RID: 3292
	[ActionCategory("iTween")]
	[Tooltip("Adds supplied Euler angles in degrees to a GameObject's rotation over time.")]
	public class iTweenRotateAdd : iTweenFsmAction
	{
		// Token: 0x0600A136 RID: 41270 RVA: 0x00335850 File Offset: 0x00333A50
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
			this.space = Space.World;
		}

		// Token: 0x0600A137 RID: 41271 RVA: 0x003358C7 File Offset: 0x00333AC7
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A138 RID: 41272 RVA: 0x003358EA File Offset: 0x00333AEA
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A139 RID: 41273 RVA: 0x003358F8 File Offset: 0x00333AF8
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
			this.itweenType = "rotate";
			iTween.RotateAdd(ownerDefaultTarget, iTween.Hash(new object[]
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
				!this.realTime.IsNone && this.realTime.Value,
				"space",
				this.space
			}));
		}

		// Token: 0x0400870B RID: 34571
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400870C RID: 34572
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x0400870D RID: 34573
		[RequiredField]
		[Tooltip("A vector that will be added to a GameObjects rotation.")]
		public FsmVector3 vector;

		// Token: 0x0400870E RID: 34574
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x0400870F RID: 34575
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x04008710 RID: 34576
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x04008711 RID: 34577
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x04008712 RID: 34578
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x04008713 RID: 34579
		public Space space;
	}
}
