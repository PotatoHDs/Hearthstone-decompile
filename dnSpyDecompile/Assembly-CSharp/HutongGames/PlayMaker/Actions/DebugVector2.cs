using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E9F RID: 3743
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of a Vector2 Variable in the PlayMaker Log Window.")]
	public class DebugVector2 : FsmStateAction
	{
		// Token: 0x0600A9BC RID: 43452 RVA: 0x003535C4 File Offset: 0x003517C4
		public override void Reset()
		{
			this.logLevel = LogLevel.Info;
			this.vector2Variable = null;
		}

		// Token: 0x0600A9BD RID: 43453 RVA: 0x003535D4 File Offset: 0x003517D4
		public override void OnEnter()
		{
			string text = "None";
			if (!this.vector2Variable.IsNone)
			{
				text = this.vector2Variable.Name + ": " + this.vector2Variable.Value;
			}
			ActionHelpers.DebugLog(base.Fsm, this.logLevel, text, false);
			base.Finish();
		}

		// Token: 0x04009064 RID: 36964
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		// Token: 0x04009065 RID: 36965
		[UIHint(UIHint.Variable)]
		[Tooltip("Prints the value of a Vector2 variable in the PlayMaker log window.")]
		public FsmVector2 vector2Variable;
	}
}
