using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E26 RID: 3622
	[ActionCategory(ActionCategory.Trigonometry)]
	[Tooltip("Get the Arc Cosine. You can get the result in degrees, simply check on the RadToDeg conversion")]
	public class GetACosine : FsmStateAction
	{
		// Token: 0x0600A778 RID: 42872 RVA: 0x0034CDEC File Offset: 0x0034AFEC
		public override void Reset()
		{
			this.angle = null;
			this.RadToDeg = true;
			this.everyFrame = false;
			this.Value = null;
		}

		// Token: 0x0600A779 RID: 42873 RVA: 0x0034CE0F File Offset: 0x0034B00F
		public override void OnEnter()
		{
			this.DoACosine();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A77A RID: 42874 RVA: 0x0034CE25 File Offset: 0x0034B025
		public override void OnUpdate()
		{
			this.DoACosine();
		}

		// Token: 0x0600A77B RID: 42875 RVA: 0x0034CE30 File Offset: 0x0034B030
		private void DoACosine()
		{
			float num = Mathf.Acos(this.Value.Value);
			if (this.RadToDeg.Value)
			{
				num *= 57.29578f;
			}
			this.angle.Value = num;
		}

		// Token: 0x04008E0B RID: 36363
		[RequiredField]
		[Tooltip("The value of the cosine")]
		public FsmFloat Value;

		// Token: 0x04008E0C RID: 36364
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The resulting angle. Note:If you want degrees, simply check RadToDeg")]
		public FsmFloat angle;

		// Token: 0x04008E0D RID: 36365
		[Tooltip("Check on if you want the angle expressed in degrees.")]
		public FsmBool RadToDeg;

		// Token: 0x04008E0E RID: 36366
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;
	}
}
