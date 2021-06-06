using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CE6 RID: 3302
	[ActionCategory("iTween")]
	[Tooltip("Randomly shakes a GameObject's position by a diminishing amount over time.")]
	public class iTweenShakePosition : iTweenFsmAction
	{
		// Token: 0x0600A16A RID: 41322 RVA: 0x0033740C File Offset: 0x0033560C
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
			this.axis = iTweenFsmAction.AxisRestriction.none;
		}

		// Token: 0x0600A16B RID: 41323 RVA: 0x00337478 File Offset: 0x00335678
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A16C RID: 41324 RVA: 0x0033749B File Offset: 0x0033569B
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A16D RID: 41325 RVA: 0x003374AC File Offset: 0x003356AC
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
			iTween.ShakePosition(ownerDefaultTarget, iTween.Hash(new object[]
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
				this.space,
				"isLocal",
				true,
				"axis",
				(this.axis == iTweenFsmAction.AxisRestriction.none) ? "" : Enum.GetName(typeof(iTweenFsmAction.AxisRestriction), this.axis)
			}));
		}

		// Token: 0x04008760 RID: 34656
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008761 RID: 34657
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x04008762 RID: 34658
		[RequiredField]
		[Tooltip("A vector shake range.")]
		public FsmVector3 vector;

		// Token: 0x04008763 RID: 34659
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x04008764 RID: 34660
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x04008765 RID: 34661
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x04008766 RID: 34662
		public Space space;

		// Token: 0x04008767 RID: 34663
		[Tooltip("Restricts rotation to the supplied axis only. Just put there strinc like 'x' or 'xz'")]
		public iTweenFsmAction.AxisRestriction axis;
	}
}
