using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CE7 RID: 3303
	[ActionCategory("iTween")]
	[Tooltip("Randomly shakes a GameObject's rotation by a diminishing amount over time.")]
	public class iTweenShakeRotation : iTweenFsmAction
	{
		// Token: 0x0600A16F RID: 41327 RVA: 0x003376A4 File Offset: 0x003358A4
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

		// Token: 0x0600A170 RID: 41328 RVA: 0x00337709 File Offset: 0x00335909
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A171 RID: 41329 RVA: 0x0033772C File Offset: 0x0033592C
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A172 RID: 41330 RVA: 0x0033773C File Offset: 0x0033593C
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
			this.itweenType = "shake";
			iTween.ShakeRotation(ownerDefaultTarget, iTween.Hash(new object[]
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

		// Token: 0x04008768 RID: 34664
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008769 RID: 34665
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x0400876A RID: 34666
		[RequiredField]
		[Tooltip("A vector shake range.")]
		public FsmVector3 vector;

		// Token: 0x0400876B RID: 34667
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x0400876C RID: 34668
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x0400876D RID: 34669
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x0400876E RID: 34670
		public Space space;
	}
}
