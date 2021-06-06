using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C09 RID: 3081
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of a Game Object Variable in the PlayMaker Log Window.")]
	public class DebugGameObject : BaseLogAction
	{
		// Token: 0x06009DC0 RID: 40384 RVA: 0x00329C24 File Offset: 0x00327E24
		public override void Reset()
		{
			this.logLevel = LogLevel.Info;
			this.gameObject = null;
			base.Reset();
		}

		// Token: 0x06009DC1 RID: 40385 RVA: 0x00329C3C File Offset: 0x00327E3C
		public override void OnEnter()
		{
			string text = "None";
			if (!this.gameObject.IsNone)
			{
				text = this.gameObject.Name + ": " + this.gameObject;
			}
			ActionHelpers.DebugLog(base.Fsm, this.logLevel, text, this.sendToUnityLog);
			base.Finish();
		}

		// Token: 0x04008328 RID: 33576
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		// Token: 0x04008329 RID: 33577
		[UIHint(UIHint.Variable)]
		[Tooltip("The GameObject variable to debug.")]
		public FsmGameObject gameObject;
	}
}
