using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CE8 RID: 3304
	[ActionCategory("iTween")]
	[Tooltip("Randomly shakes a GameObject's scale by a diminishing amount over time.")]
	public class iTweenShakeScale : iTweenFsmAction
	{
		// Token: 0x0600A174 RID: 41332 RVA: 0x003378EC File Offset: 0x00335AEC
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

		// Token: 0x0600A175 RID: 41333 RVA: 0x0033794A File Offset: 0x00335B4A
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A176 RID: 41334 RVA: 0x0033796D File Offset: 0x00335B6D
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A177 RID: 41335 RVA: 0x0033797C File Offset: 0x00335B7C
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
			iTween.ShakeScale(ownerDefaultTarget, iTween.Hash(new object[]
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

		// Token: 0x0400876F RID: 34671
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008770 RID: 34672
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x04008771 RID: 34673
		[RequiredField]
		[Tooltip("A vector shake range.")]
		public FsmVector3 vector;

		// Token: 0x04008772 RID: 34674
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x04008773 RID: 34675
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x04008774 RID: 34676
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;
	}
}
