using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D79 RID: 3449
	[ActionCategory(ActionCategory.Math)]
	[Tooltip("Gets the value of a curve at a given time and stores it in a Float Variable. NOTE: This can be used for more than just animation! It's a general way to transform an input number into an output number using a curve (e.g., linear input -> bell curve).")]
	public class SampleCurve : FsmStateAction
	{
		// Token: 0x0600A46F RID: 42095 RVA: 0x00342F3E File Offset: 0x0034113E
		public override void Reset()
		{
			this.curve = null;
			this.sampleAt = null;
			this.storeValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A470 RID: 42096 RVA: 0x00342F5C File Offset: 0x0034115C
		public override void OnEnter()
		{
			this.DoSampleCurve();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A471 RID: 42097 RVA: 0x00342F72 File Offset: 0x00341172
		public override void OnUpdate()
		{
			this.DoSampleCurve();
		}

		// Token: 0x0600A472 RID: 42098 RVA: 0x00342F7C File Offset: 0x0034117C
		private void DoSampleCurve()
		{
			if (this.curve == null || this.curve.curve == null || this.storeValue == null)
			{
				return;
			}
			this.storeValue.Value = this.curve.curve.Evaluate(this.sampleAt.Value);
		}

		// Token: 0x04008AC8 RID: 35528
		[RequiredField]
		public FsmAnimationCurve curve;

		// Token: 0x04008AC9 RID: 35529
		[RequiredField]
		public FsmFloat sampleAt;

		// Token: 0x04008ACA RID: 35530
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeValue;

		// Token: 0x04008ACB RID: 35531
		public bool everyFrame;
	}
}
