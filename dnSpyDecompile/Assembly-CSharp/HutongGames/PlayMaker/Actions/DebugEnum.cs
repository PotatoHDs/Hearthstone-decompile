using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C06 RID: 3078
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of an Enum Variable in the PlayMaker Log Window.")]
	public class DebugEnum : BaseLogAction
	{
		// Token: 0x06009DB7 RID: 40375 RVA: 0x00329AEF File Offset: 0x00327CEF
		public override void Reset()
		{
			this.logLevel = LogLevel.Info;
			this.enumVariable = null;
			base.Reset();
		}

		// Token: 0x06009DB8 RID: 40376 RVA: 0x00329B08 File Offset: 0x00327D08
		public override void OnEnter()
		{
			string text = "None";
			if (!this.enumVariable.IsNone)
			{
				text = this.enumVariable.Name + ": " + this.enumVariable.Value;
			}
			ActionHelpers.DebugLog(base.Fsm, this.logLevel, text, this.sendToUnityLog);
			base.Finish();
		}

		// Token: 0x04008322 RID: 33570
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		// Token: 0x04008323 RID: 33571
		[UIHint(UIHint.Variable)]
		[Tooltip("The Enum Variable to debug.")]
		public FsmEnum enumVariable;
	}
}
