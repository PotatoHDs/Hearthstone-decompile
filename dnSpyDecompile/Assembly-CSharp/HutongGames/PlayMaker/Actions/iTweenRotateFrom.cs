using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CDE RID: 3294
	[ActionCategory("iTween")]
	[Tooltip("Instantly changes a GameObject's Euler angles in degrees then returns it to it's starting rotation over time.")]
	public class iTweenRotateFrom : iTweenFsmAction
	{
		// Token: 0x0600A140 RID: 41280 RVA: 0x00335DA8 File Offset: 0x00333FA8
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

		// Token: 0x0600A141 RID: 41281 RVA: 0x00335E31 File Offset: 0x00334031
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A142 RID: 41282 RVA: 0x00335E54 File Offset: 0x00334054
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A143 RID: 41283 RVA: 0x00335E64 File Offset: 0x00334064
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
			iTween.RotateFrom(ownerDefaultTarget, iTween.Hash(new object[]
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

		// Token: 0x0400871D RID: 34589
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x0400871E RID: 34590
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x0400871F RID: 34591
		[Tooltip("A rotation from a GameObject.")]
		public FsmGameObject transformRotation;

		// Token: 0x04008720 RID: 34592
		[Tooltip("A rotation vector the GameObject will animate from.")]
		public FsmVector3 vectorRotation;

		// Token: 0x04008721 RID: 34593
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x04008722 RID: 34594
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x04008723 RID: 34595
		[Tooltip("Can be used instead of time to allow animation based on speed. When you define speed the time variable is ignored.")]
		public FsmFloat speed;

		// Token: 0x04008724 RID: 34596
		[Tooltip("The shape of the easing curve applied to the animation.")]
		public iTween.EaseType easeType = iTween.EaseType.linear;

		// Token: 0x04008725 RID: 34597
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x04008726 RID: 34598
		[Tooltip("Whether to animate in local or world space.")]
		public Space space;
	}
}
