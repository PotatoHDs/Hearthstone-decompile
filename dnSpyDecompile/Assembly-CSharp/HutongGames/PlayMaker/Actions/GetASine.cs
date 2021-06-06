using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E27 RID: 3623
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the Arc sine. You can get the result in degrees, simply check on the RadToDeg conversion")]
	public class GetASine : FsmStateAction
	{
		// Token: 0x0600A77D RID: 42877 RVA: 0x0034CE6F File Offset: 0x0034B06F
		public override void Reset()
		{
			this.angle = null;
			this.RadToDeg = true;
			this.everyFrame = false;
			this.Value = null;
		}

		// Token: 0x0600A77E RID: 42878 RVA: 0x0034CE92 File Offset: 0x0034B092
		public override void OnEnter()
		{
			this.DoASine();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A77F RID: 42879 RVA: 0x0034CEA8 File Offset: 0x0034B0A8
		public override void OnUpdate()
		{
			this.DoASine();
		}

		// Token: 0x0600A780 RID: 42880 RVA: 0x0034CEB0 File Offset: 0x0034B0B0
		private void DoASine()
		{
			float num = Mathf.Asin(this.Value.Value);
			if (this.RadToDeg.Value)
			{
				num *= 57.29578f;
			}
			this.angle.Value = num;
		}

		// Token: 0x04008E0F RID: 36367
		[RequiredField]
		[Tooltip("The value of the sine")]
		public FsmFloat Value;

		// Token: 0x04008E10 RID: 36368
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting angle. Note:If you want degrees, simply check RadToDeg")]
		public FsmFloat angle;

		// Token: 0x04008E11 RID: 36369
		[Tooltip("Check on if you want the angle expressed in degrees.")]
		public FsmBool RadToDeg;

		// Token: 0x04008E12 RID: 36370
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
