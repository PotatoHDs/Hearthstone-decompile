using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C07 RID: 3079
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of a Float Variable in the PlayMaker Log Window.")]
	public class DebugFloat : BaseLogAction
	{
		// Token: 0x06009DBA RID: 40378 RVA: 0x00329B67 File Offset: 0x00327D67
		public override void Reset()
		{
			this.logLevel = LogLevel.Info;
			this.floatVariable = null;
			base.Reset();
		}

		// Token: 0x06009DBB RID: 40379 RVA: 0x00329B80 File Offset: 0x00327D80
		public override void OnEnter()
		{
			string text = "None";
			if (!this.floatVariable.IsNone)
			{
				text = this.floatVariable.Name + ": " + this.floatVariable.Value;
			}
			ActionHelpers.DebugLog(base.Fsm, this.logLevel, text, this.sendToUnityLog);
			base.Finish();
		}

		// Token: 0x04008324 RID: 33572
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		// Token: 0x04008325 RID: 33573
		[UIHint(UIHint.Variable)]
		[Tooltip("The Float variable to debug.")]
		public FsmFloat floatVariable;
	}
}
