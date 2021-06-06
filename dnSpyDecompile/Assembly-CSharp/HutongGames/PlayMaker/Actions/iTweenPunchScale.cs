using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CDA RID: 3290
	[ActionCategory("iTween")]
	[Tooltip("Applies a jolt of force to a GameObject's scale and wobbles it back to its initial scale.")]
	public class iTweenPunchScale : iTweenFsmAction
	{
		// Token: 0x0600A12D RID: 41261 RVA: 0x00335574 File Offset: 0x00333774
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
		}

		// Token: 0x0600A12E RID: 41262 RVA: 0x003355D2 File Offset: 0x003337D2
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A12F RID: 41263 RVA: 0x003355F5 File Offset: 0x003337F5
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A130 RID: 41264 RVA: 0x00335604 File Offset: 0x00333804
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
			iTween.PunchScale(ownerDefaultTarget, iTween.Hash(new object[]
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
				!this.realTime.IsNone && this.realTime.Value
			}));
		}

		// Token: 0x04008701 RID: 34561
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008702 RID: 34562
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x04008703 RID: 34563
		[RequiredField]
		[Tooltip("A vector punch range.")]
		public FsmVector3 vector;

		// Token: 0x04008704 RID: 34564
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x04008705 RID: 34565
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x04008706 RID: 34566
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;
	}
}
