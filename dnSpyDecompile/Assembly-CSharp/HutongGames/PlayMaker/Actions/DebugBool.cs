using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C04 RID: 3076
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of a Bool Variable in the PlayMaker Log Window.")]
	public class DebugBool : BaseLogAction
	{
		// Token: 0x06009DB1 RID: 40369 RVA: 0x00329953 File Offset: 0x00327B53
		public override void Reset()
		{
			this.logLevel = LogLevel.Info;
			this.boolVariable = null;
			base.Reset();
		}

		// Token: 0x06009DB2 RID: 40370 RVA: 0x0032996C File Offset: 0x00327B6C
		public override void OnEnter()
		{
			string text = "None";
			if (!this.boolVariable.IsNone)
			{
				text = this.boolVariable.Name + ": " + this.boolVariable.Value.ToString();
			}
			ActionHelpers.DebugLog(base.Fsm, this.logLevel, text, this.sendToUnityLog);
			base.Finish();
		}

		// Token: 0x0400831B RID: 33563
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		// Token: 0x0400831C RID: 33564
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variable to debug.")]
		public FsmBool boolVariable;
	}
}
