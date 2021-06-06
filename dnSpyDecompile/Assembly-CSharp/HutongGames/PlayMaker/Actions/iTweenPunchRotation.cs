using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CD9 RID: 3289
	[ActionCategory("iTween")]
	[Tooltip("Applies a jolt of force to a GameObject's rotation and wobbles it back to its initial rotation. NOTE: Due to the way iTween utilizes the Transform.Rotate method, PunchRotation works best with single axis usage rather than punching with a Vector3.")]
	public class iTweenPunchRotation : iTweenFsmAction
	{
		// Token: 0x0600A128 RID: 41256 RVA: 0x0033532C File Offset: 0x0033352C
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
			this.space = Space.World;
		}

		// Token: 0x0600A129 RID: 41257 RVA: 0x00335391 File Offset: 0x00333591
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A12A RID: 41258 RVA: 0x003353B4 File Offset: 0x003335B4
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A12B RID: 41259 RVA: 0x003353C4 File Offset: 0x003335C4
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
			this.itweenType = "punch";
			iTween.PunchRotation(ownerDefaultTarget, iTween.Hash(new object[]
			{
				"amount",
				vector,
				"name",
				this.id.IsNone ? "" : this.id.Value,
				"time",
				this.time.IsNone ? 1f : this.time.Value,
				"delay",
				this.delay.IsNone ? 0f : this.delay.Value,
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

		// Token: 0x040086FA RID: 34554
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040086FB RID: 34555
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x040086FC RID: 34556
		[RequiredField]
		[Tooltip("A vector punch range.")]
		public FsmVector3 vector;

		// Token: 0x040086FD RID: 34557
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x040086FE RID: 34558
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x040086FF RID: 34559
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x04008700 RID: 34560
		public Space space;
	}
}
