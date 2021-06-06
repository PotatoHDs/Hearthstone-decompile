using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CD8 RID: 3288
	[ActionCategory("iTween")]
	[Tooltip("Applies a jolt of force to a GameObject's position and wobbles it back to its initial position.")]
	public class iTweenPunchPosition : iTweenFsmAction
	{
		// Token: 0x0600A123 RID: 41251 RVA: 0x003350A0 File Offset: 0x003332A0
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

		// Token: 0x0600A124 RID: 41252 RVA: 0x0033510C File Offset: 0x0033330C
		public override void OnEnter()
		{
			base.OnEnteriTween(this.gameObject);
			if (this.loopType != iTween.LoopType.none)
			{
				base.IsLoop(true);
			}
			this.DoiTween();
		}

		// Token: 0x0600A125 RID: 41253 RVA: 0x0033512F File Offset: 0x0033332F
		public override void OnExit()
		{
			base.OnExitiTween(this.gameObject);
		}

		// Token: 0x0600A126 RID: 41254 RVA: 0x00335140 File Offset: 0x00333340
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
			iTween.PunchPosition(ownerDefaultTarget, iTween.Hash(new object[]
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
				"axis",
				(this.axis == iTweenFsmAction.AxisRestriction.none) ? "" : Enum.GetName(typeof(iTweenFsmAction.AxisRestriction), this.axis)
			}));
		}

		// Token: 0x040086F2 RID: 34546
		[RequiredField]
		public FsmOwnerDefault gameObject;

		// Token: 0x040086F3 RID: 34547
		[Tooltip("iTween ID. If set you can use iTween Stop action to stop it by its id.")]
		public FsmString id;

		// Token: 0x040086F4 RID: 34548
		[RequiredField]
		[Tooltip("A vector punch range.")]
		public FsmVector3 vector;

		// Token: 0x040086F5 RID: 34549
		[Tooltip("The time in seconds the animation will take to complete.")]
		public FsmFloat time;

		// Token: 0x040086F6 RID: 34550
		[Tooltip("The time in seconds the animation will wait before beginning.")]
		public FsmFloat delay;

		// Token: 0x040086F7 RID: 34551
		[Tooltip("The type of loop to apply once the animation has completed.")]
		public iTween.LoopType loopType;

		// Token: 0x040086F8 RID: 34552
		public Space space;

		// Token: 0x040086F9 RID: 34553
		[Tooltip("Restricts rotation to the supplied axis only.")]
		public iTweenFsmAction.AxisRestriction axis;
	}
}
