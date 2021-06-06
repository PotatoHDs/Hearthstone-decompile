using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BFB RID: 3067
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts an Integer value to a Float value.")]
	public class ConvertIntToFloat : FsmStateAction
	{
		// Token: 0x06009D87 RID: 40327 RVA: 0x003292B4 File Offset: 0x003274B4
		public override void Reset()
		{
			this.intVariable = null;
			this.floatVariable = null;
			this.everyFrame = false;
		}

		// Token: 0x06009D88 RID: 40328 RVA: 0x003292CB File Offset: 0x003274CB
		public override void OnEnter()
		{
			this.DoConvertIntToFloat();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D89 RID: 40329 RVA: 0x003292E1 File Offset: 0x003274E1
		public override void OnUpdate()
		{
			this.DoConvertIntToFloat();
		}

		// Token: 0x06009D8A RID: 40330 RVA: 0x003292E9 File Offset: 0x003274E9
		private void DoConvertIntToFloat()
		{
			this.floatVariable.Value = (float)this.intVariable.Value;
		}

		// Token: 0x040082F6 RID: 33526
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Integer variable to convert to a float.")]
		public FsmInt intVariable;

		// Token: 0x040082F7 RID: 33527
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Float variable.")]
		public FsmFloat floatVariable;

		// Token: 0x040082F8 RID: 33528
		[Tooltip("Repeat every frame. Useful if the Integer variable is changing.")]
		public bool everyFrame;
	}
}
