using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E29 RID: 3625
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the Arc Tangent 2 as in atan2(y,x). You can get the result in degrees, simply check on the RadToDeg conversion")]
	public class GetAtan2 : FsmStateAction
	{
		// Token: 0x0600A787 RID: 42887 RVA: 0x0034CF6F File Offset: 0x0034B16F
		public override void Reset()
		{
			this.xValue = null;
			this.yValue = null;
			this.RadToDeg = true;
			this.everyFrame = false;
			this.angle = null;
		}

		// Token: 0x0600A788 RID: 42888 RVA: 0x0034CF99 File Offset: 0x0034B199
		public override void OnEnter()
		{
			this.DoATan();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A789 RID: 42889 RVA: 0x0034CFAF File Offset: 0x0034B1AF
		public override void OnUpdate()
		{
			this.DoATan();
		}

		// Token: 0x0600A78A RID: 42890 RVA: 0x0034CFB8 File Offset: 0x0034B1B8
		private void DoATan()
		{
			float num = Mathf.Atan2(this.yValue.Value, this.xValue.Value);
			if (this.RadToDeg.Value)
			{
				num *= 57.29578f;
			}
			this.angle.Value = num;
		}

		// Token: 0x04008E17 RID: 36375
		[RequiredField]
		[Tooltip("The x value of the tan")]
		public FsmFloat xValue;

		// Token: 0x04008E18 RID: 36376
		[RequiredField]
		[Tooltip("The y value of the tan")]
		public FsmFloat yValue;

		// Token: 0x04008E19 RID: 36377
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting angle. Note:If you want degrees, simply check RadToDeg")]
		public FsmFloat angle;

		// Token: 0x04008E1A RID: 36378
		[Tooltip("Check on if you want the angle expressed in degrees.")]
		public FsmBool RadToDeg;

		// Token: 0x04008E1B RID: 36379
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
