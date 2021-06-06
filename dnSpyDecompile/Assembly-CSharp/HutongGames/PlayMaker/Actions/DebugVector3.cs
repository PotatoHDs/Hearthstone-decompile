using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C0D RID: 3085
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Logs the value of a Vector3 Variable in the PlayMaker Log Window.")]
	public class DebugVector3 : BaseLogAction
	{
		// Token: 0x06009DCC RID: 40396 RVA: 0x00329DDA File Offset: 0x00327FDA
		public override void Reset()
		{
			this.logLevel = LogLevel.Info;
			this.vector3Variable = null;
			base.Reset();
		}

		// Token: 0x06009DCD RID: 40397 RVA: 0x00329DF0 File Offset: 0x00327FF0
		public override void OnEnter()
		{
			string text = "None";
			if (!this.vector3Variable.IsNone)
			{
				text = this.vector3Variable.Name + ": " + this.vector3Variable.Value;
			}
			ActionHelpers.DebugLog(base.Fsm, this.logLevel, text, this.sendToUnityLog);
			base.Finish();
		}

		// Token: 0x04008330 RID: 33584
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		// Token: 0x04008331 RID: 33585
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector3 variable to debug.")]
		public FsmVector3 vector3Variable;
	}
}
