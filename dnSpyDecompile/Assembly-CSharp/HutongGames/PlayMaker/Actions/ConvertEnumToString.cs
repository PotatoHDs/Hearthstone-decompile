using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BF8 RID: 3064
	[ActionCategory(ActionCategory.Convert)]
	[Tooltip("Converts an Enum value to a String value.")]
	public class ConvertEnumToString : FsmStateAction
	{
		// Token: 0x06009D78 RID: 40312 RVA: 0x003290E5 File Offset: 0x003272E5
		public override void Reset()
		{
			this.enumVariable = null;
			this.stringVariable = null;
			this.everyFrame = false;
		}

		// Token: 0x06009D79 RID: 40313 RVA: 0x003290FC File Offset: 0x003272FC
		public override void OnEnter()
		{
			this.DoConvertEnumToString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D7A RID: 40314 RVA: 0x00329112 File Offset: 0x00327312
		public override void OnUpdate()
		{
			this.DoConvertEnumToString();
		}

		// Token: 0x06009D7B RID: 40315 RVA: 0x0032911A File Offset: 0x0032731A
		private void DoConvertEnumToString()
		{
			this.stringVariable.Value = ((this.enumVariable.Value != null) ? this.enumVariable.Value.ToString() : "");
		}

		// Token: 0x040082EB RID: 33515
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Enum variable to convert.")]
		public FsmEnum enumVariable;

		// Token: 0x040082EC RID: 33516
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The String variable to store the converted value.")]
		public FsmString stringVariable;

		// Token: 0x040082ED RID: 33517
		[Tooltip("Repeat every frame. Useful if the Enum variable is changing.")]
		public bool everyFrame;
	}
}
