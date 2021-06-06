using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C2E RID: 3118
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the comparison of 2 Floats.")]
	public class FloatCompare : FsmStateAction
	{
		// Token: 0x06009E52 RID: 40530 RVA: 0x0032B314 File Offset: 0x00329514
		public override void Reset()
		{
			this.float1 = 0f;
			this.float2 = 0f;
			this.tolerance = 0f;
			this.equal = null;
			this.lessThan = null;
			this.greaterThan = null;
			this.everyFrame = false;
		}

		// Token: 0x06009E53 RID: 40531 RVA: 0x0032B36D File Offset: 0x0032956D
		public override void OnEnter()
		{
			this.DoCompare();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009E54 RID: 40532 RVA: 0x0032B383 File Offset: 0x00329583
		public override void OnUpdate()
		{
			this.DoCompare();
		}

		// Token: 0x06009E55 RID: 40533 RVA: 0x0032B38C File Offset: 0x0032958C
		private void DoCompare()
		{
			if (Mathf.Abs(this.float1.Value - this.float2.Value) <= this.tolerance.Value)
			{
				base.Fsm.Event(this.equal);
				return;
			}
			if (this.float1.Value < this.float2.Value)
			{
				base.Fsm.Event(this.lessThan);
				return;
			}
			if (this.float1.Value > this.float2.Value)
			{
				base.Fsm.Event(this.greaterThan);
			}
		}

		// Token: 0x06009E56 RID: 40534 RVA: 0x0032B427 File Offset: 0x00329627
		public override string ErrorCheck()
		{
			if (FsmEvent.IsNullOrEmpty(this.equal) && FsmEvent.IsNullOrEmpty(this.lessThan) && FsmEvent.IsNullOrEmpty(this.greaterThan))
			{
				return "Action sends no events!";
			}
			return "";
		}

		// Token: 0x040083A4 RID: 33700
		[RequiredField]
		[Tooltip("The first float variable.")]
		public FsmFloat float1;

		// Token: 0x040083A5 RID: 33701
		[RequiredField]
		[Tooltip("The second float variable.")]
		public FsmFloat float2;

		// Token: 0x040083A6 RID: 33702
		[RequiredField]
		[Tooltip("Tolerance for the Equal test (almost equal).\nNOTE: Floats that look the same are often not exactly the same, so you often need to use a small tolerance.")]
		public FsmFloat tolerance;

		// Token: 0x040083A7 RID: 33703
		[Tooltip("Event sent if Float 1 equals Float 2 (within Tolerance)")]
		public FsmEvent equal;

		// Token: 0x040083A8 RID: 33704
		[Tooltip("Event sent if Float 1 is less than Float 2")]
		public FsmEvent lessThan;

		// Token: 0x040083A9 RID: 33705
		[Tooltip("Event sent if Float 1 is greater than Float 2")]
		public FsmEvent greaterThan;

		// Token: 0x040083AA RID: 33706
		[Tooltip("Repeat every frame. Useful if the variables are changing and you're waiting for a particular result.")]
		public bool everyFrame;
	}
}
