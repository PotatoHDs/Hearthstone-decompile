using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CED RID: 3309
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Sends Events based on the comparison of 2 Colors.")]
	public class ColorCompare : FsmStateAction
	{
		// Token: 0x0600A186 RID: 41350 RVA: 0x00337E80 File Offset: 0x00336080
		public override void Reset()
		{
			this.color1 = Color.white;
			this.color2 = Color.white;
			this.tolerance = 0f;
			this.equal = null;
			this.notEqual = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A187 RID: 41351 RVA: 0x00337ED2 File Offset: 0x003360D2
		public override void OnEnter()
		{
			this.DoCompare();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A188 RID: 41352 RVA: 0x00337EE8 File Offset: 0x003360E8
		public override void OnUpdate()
		{
			this.DoCompare();
		}

		// Token: 0x0600A189 RID: 41353 RVA: 0x00337EF0 File Offset: 0x003360F0
		private void DoCompare()
		{
			if (Mathf.Abs(this.color1.Value.r - this.color2.Value.r) > this.tolerance.Value || Mathf.Abs(this.color1.Value.g - this.color2.Value.g) > this.tolerance.Value || Mathf.Abs(this.color1.Value.b - this.color2.Value.b) > this.tolerance.Value || Mathf.Abs(this.color1.Value.a - this.color2.Value.a) > this.tolerance.Value)
			{
				base.Fsm.Event(this.notEqual);
				return;
			}
			base.Fsm.Event(this.equal);
		}

		// Token: 0x0600A18A RID: 41354 RVA: 0x00337FEF File Offset: 0x003361EF
		public override string ErrorCheck()
		{
			if (FsmEvent.IsNullOrEmpty(this.equal) && FsmEvent.IsNullOrEmpty(this.notEqual))
			{
				return "Action sends no events!";
			}
			return "";
		}

		// Token: 0x04008786 RID: 34694
		[RequiredField]
		[Tooltip("The first Color.")]
		public FsmColor color1;

		// Token: 0x04008787 RID: 34695
		[RequiredField]
		[Tooltip("The second Color.")]
		public FsmColor color2;

		// Token: 0x04008788 RID: 34696
		[RequiredField]
		[Tooltip("Tolerance of test, to test for 'almost equals' or to ignore small floating point rounding differences.")]
		public FsmFloat tolerance;

		// Token: 0x04008789 RID: 34697
		[Tooltip("Event sent if Color 1 equals Color 2 (within Tolerance)")]
		public FsmEvent equal;

		// Token: 0x0400878A RID: 34698
		[Tooltip("Event sent if Color 1 does not equal Color 2 (within Tolerance)")]
		public FsmEvent notEqual;

		// Token: 0x0400878B RID: 34699
		[Tooltip("Repeat every frame. Useful if the variables are changing and you're waiting for a particular result.")]
		public bool everyFrame;
	}
}
