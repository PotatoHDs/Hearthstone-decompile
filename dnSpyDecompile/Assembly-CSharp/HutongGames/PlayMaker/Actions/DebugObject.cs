using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C0C RID: 3084
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of an Object Variable in the PlayMaker Log Window.")]
	public class DebugObject : BaseLogAction
	{
		// Token: 0x06009DC9 RID: 40393 RVA: 0x00329D67 File Offset: 0x00327F67
		public override void Reset()
		{
			this.logLevel = LogLevel.Info;
			this.fsmObject = null;
			base.Reset();
		}

		// Token: 0x06009DCA RID: 40394 RVA: 0x00329D80 File Offset: 0x00327F80
		public override void OnEnter()
		{
			string text = "None";
			if (!this.fsmObject.IsNone)
			{
				text = this.fsmObject.Name + ": " + this.fsmObject;
			}
			ActionHelpers.DebugLog(base.Fsm, this.logLevel, text, this.sendToUnityLog);
			base.Finish();
		}

		// Token: 0x0400832E RID: 33582
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		// Token: 0x0400832F RID: 33583
		[UIHint(UIHint.Variable)]
		[Tooltip("The Object variable to debug.")]
		public FsmObject fsmObject;
	}
}
