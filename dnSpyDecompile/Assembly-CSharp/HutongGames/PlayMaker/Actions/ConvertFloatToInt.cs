using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BF9 RID: 3065
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts a Float value to an Integer value.")]
	public class ConvertFloatToInt : FsmStateAction
	{
		// Token: 0x06009D7D RID: 40317 RVA: 0x0032914B File Offset: 0x0032734B
		public override void Reset()
		{
			this.floatVariable = null;
			this.intVariable = null;
			this.rounding = ConvertFloatToInt.FloatRounding.Nearest;
			this.everyFrame = false;
		}

		// Token: 0x06009D7E RID: 40318 RVA: 0x00329169 File Offset: 0x00327369
		public override void OnEnter()
		{
			this.DoConvertFloatToInt();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D7F RID: 40319 RVA: 0x0032917F File Offset: 0x0032737F
		public override void OnUpdate()
		{
			this.DoConvertFloatToInt();
		}

		// Token: 0x06009D80 RID: 40320 RVA: 0x00329188 File Offset: 0x00327388
		private void DoConvertFloatToInt()
		{
			switch (this.rounding)
			{
			case ConvertFloatToInt.FloatRounding.RoundDown:
				this.intVariable.Value = Mathf.FloorToInt(this.floatVariable.Value);
				return;
			case ConvertFloatToInt.FloatRounding.RoundUp:
				this.intVariable.Value = Mathf.CeilToInt(this.floatVariable.Value);
				return;
			case ConvertFloatToInt.FloatRounding.Nearest:
				this.intVariable.Value = Mathf.RoundToInt(this.floatVariable.Value);
				return;
			default:
				return;
			}
		}

		// Token: 0x040082EE RID: 33518
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Float variable to convert to an integer.")]
		public FsmFloat floatVariable;

		// Token: 0x040082EF RID: 33519
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in an Integer variable.")]
		public FsmInt intVariable;

		// Token: 0x040082F0 RID: 33520
		public ConvertFloatToInt.FloatRounding rounding;

		// Token: 0x040082F1 RID: 33521
		public bool everyFrame;

		// Token: 0x02002794 RID: 10132
		public enum FloatRounding
		{
			// Token: 0x0400F4AF RID: 62639
			RoundDown,
			// Token: 0x0400F4B0 RID: 62640
			RoundUp,
			// Token: 0x0400F4B1 RID: 62641
			Nearest
		}
	}
}
