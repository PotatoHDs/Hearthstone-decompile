using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C0A RID: 3082
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of an Integer Variable in the PlayMaker Log Window.")]
	public class DebugInt : BaseLogAction
	{
		// Token: 0x06009DC3 RID: 40387 RVA: 0x00329C96 File Offset: 0x00327E96
		public override void Reset()
		{
			this.logLevel = LogLevel.Info;
			this.intVariable = null;
		}

		// Token: 0x06009DC4 RID: 40388 RVA: 0x00329CA8 File Offset: 0x00327EA8
		public override void OnEnter()
		{
			string text = "None";
			if (!this.intVariable.IsNone)
			{
				text = this.intVariable.Name + ": " + this.intVariable.Value;
			}
			ActionHelpers.DebugLog(base.Fsm, this.logLevel, text, this.sendToUnityLog);
			base.Finish();
		}

		// Token: 0x0400832A RID: 33578
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		// Token: 0x0400832B RID: 33579
		[UIHint(UIHint.Variable)]
		[Tooltip("The Int variable to debug.")]
		public FsmInt intVariable;
	}
}
