using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BFF RID: 3071
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts an String value to an Int value.")]
	public class ConvertStringToInt : FsmStateAction
	{
		// Token: 0x06009D9B RID: 40347 RVA: 0x0032954B File Offset: 0x0032774B
		public override void Reset()
		{
			this.intVariable = null;
			this.stringVariable = null;
			this.everyFrame = false;
		}

		// Token: 0x06009D9C RID: 40348 RVA: 0x00329562 File Offset: 0x00327762
		public override void OnEnter()
		{
			this.DoConvertStringToInt();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D9D RID: 40349 RVA: 0x00329578 File Offset: 0x00327778
		public override void OnUpdate()
		{
			this.DoConvertStringToInt();
		}

		// Token: 0x06009D9E RID: 40350 RVA: 0x00329580 File Offset: 0x00327780
		private void DoConvertStringToInt()
		{
			this.intVariable.Value = int.Parse(this.stringVariable.Value);
		}

		// Token: 0x04008304 RID: 33540
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The String variable to convert to an integer.")]
		public FsmString stringVariable;

		// Token: 0x04008305 RID: 33541
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in an Int variable.")]
		public FsmInt intVariable;

		// Token: 0x04008306 RID: 33542
		[Tooltip("Repeat every frame. Useful if the String variable is changing.")]
		public bool everyFrame;
	}
}
