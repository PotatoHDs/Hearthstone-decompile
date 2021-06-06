using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CDF RID: 3295
	[ActionCategory("iTween")]
	[Tooltip("Rotates a GameObject to the supplied Euler angles in degrees over time.")]
	public class iTweenRotateTo : iTweenFsmAction
	{
		// Token: 0x0600A145 RID: 41285 RVA: 0x003360CC File Offset: 0x003342CC
		public override void Reset()
		{
			base.Reset();
			this.id = new FsmString
			{
				UseVariable = true
			};
			this.transformRotation = new FsmGameObject
			{
				UseVariable = true
			};
			this.vectorRotation = new FsmVector3
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
			this.space = Space.World;
		}

		// Token: 0x0600A146 RID: 41286 RVA: 0x00336155 File Offset: 0x00334355
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A147 RID: 41287 RVA: 0x00336178 File Offset: 0x00334378
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A148 RID: 41288 RVA: 0x00336188 File Offset: 0x00334388
		private void DoiTween()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Vector3 vector = this.vectorRotation.IsNone ? Vector3.zero : this.vectorRotation.Value;
			if (!this.transformRotation.IsNone && this.transformRotation.Value)
			{
				vector = ((this.space == Space.World) ? (this.transformRotation.Value.transform.eulerAngles + vector) : (this.transformRotation.Value.transform.localEulerAngles + vector));
			}
			this.itweenType = "rotate";
			iTween.RotateTo(ownerDefaultTarget, iTween.Hash(new object[]
			{
				"rotation",
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
				"islocal",
				this.space == Space.Self
			}));
		}

		// Token: 0x04008727 RID: 34599
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008728 RID: 34600
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x04008729 RID: 34601
		[Tooltip("Rotate to a transform rotation.")]
		public FsmGameObject transformRotation;

		// Token: 0x0400872A RID: 34602
		[Tooltip("A rotation the GameObject will animate from.")]
		public FsmVector3 vectorRotation;

		// Token: 0x0400872B RID: 34603
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x0400872C RID: 34604
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x0400872D RID: 34605
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x0400872E RID: 34606
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x0400872F RID: 34607
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x04008730 RID: 34608
		[Tooltip("Whether to animate in local or world space.")]
		public Space space;
	}
}
