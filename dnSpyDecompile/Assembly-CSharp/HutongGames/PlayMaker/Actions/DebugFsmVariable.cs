using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C08 RID: 3080
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Print the value of any FSM Variable in the PlayMaker Log Window.")]
	public class DebugFsmVariable : BaseLogAction
	{
		// Token: 0x06009DBD RID: 40381 RVA: 0x00329BE4 File Offset: 0x00327DE4
		public override void Reset()
		{
			this.logLevel = LogLevel.Info;
			this.variable = null;
			base.Reset();
		}

		// Token: 0x06009DBE RID: 40382 RVA: 0x00329BFA File Offset: 0x00327DFA
		public override void OnEnter()
		{
			ActionHelpers.DebugLog(base.Fsm, this.logLevel, this.variable.DebugString(), this.sendToUnityLog);
			base.Finish();
		}

		// Token: 0x04008326 RID: 33574
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		// Token: 0x04008327 RID: 33575
		[HideTypeFilter]
		[UIHint(UIHint.Variable)]
		[Tooltip("The variable to debug.")]
		public FsmVar variable;
	}
}
