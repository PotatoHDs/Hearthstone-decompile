using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C4E RID: 3150
	[ActionCategory(ActionCategory.Color)]
	[Tooltip("Get the RGBA channels of a Color Variable and store them in Float Variables.")]
	public class GetColorRGBA : FsmStateAction
	{
		// Token: 0x06009EE0 RID: 40672 RVA: 0x0032CB48 File Offset: 0x0032AD48
		public override void Reset()
		{
			this.color = null;
			this.storeRed = null;
			this.storeGreen = null;
			this.storeBlue = null;
			this.storeAlpha = null;
			this.everyFrame = false;
		}

		// Token: 0x06009EE1 RID: 40673 RVA: 0x0032CB74 File Offset: 0x0032AD74
		public override void OnEnter()
		{
			this.DoGetColorRGBA();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009EE2 RID: 40674 RVA: 0x0032CB8A File Offset: 0x0032AD8A
		public override void OnUpdate()
		{
			this.DoGetColorRGBA();
		}

		// Token: 0x06009EE3 RID: 40675 RVA: 0x0032CB94 File Offset: 0x0032AD94
		private void DoGetColorRGBA()
		{
			if (this.color.IsNone)
			{
				return;
			}
			this.storeRed.Value = this.color.Value.r;
			this.storeGreen.Value = this.color.Value.g;
			this.storeBlue.Value = this.color.Value.b;
			this.storeAlpha.Value = this.color.Value.a;
		}

		// Token: 0x0400843C RID: 33852
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Color variable.")]
		public FsmColor color;

		// Token: 0x0400843D RID: 33853
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the red channel in a float variable.")]
		public FsmFloat storeRed;

		// Token: 0x0400843E RID: 33854
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the green channel in a float variable.")]
		public FsmFloat storeGreen;

		// Token: 0x0400843F RID: 33855
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the blue channel in a float variable.")]
		public FsmFloat storeBlue;

		// Token: 0x04008440 RID: 33856
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the alpha channel in a float variable.")]
		public FsmFloat storeAlpha;

		// Token: 0x04008441 RID: 33857
		[Tooltip("Repeat every frame. Useful if the color variable is changing.")]
		public bool everyFrame;
	}
}
