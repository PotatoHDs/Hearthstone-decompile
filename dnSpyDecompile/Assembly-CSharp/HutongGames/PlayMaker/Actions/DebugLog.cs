using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C0B RID: 3083
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Sends a log message to the PlayMaker Log Window.")]
	public class DebugLog : BaseLogAction
	{
		// Token: 0x06009DC6 RID: 40390 RVA: 0x00329D0C File Offset: 0x00327F0C
		public override void Reset()
		{
			this.logLevel = LogLevel.Info;
			this.text = "";
			base.Reset();
		}

		// Token: 0x06009DC7 RID: 40391 RVA: 0x00329D2B File Offset: 0x00327F2B
		public override void OnEnter()
		{
			if (!string.IsNullOrEmpty(this.text.Value))
			{
				ActionHelpers.DebugLog(base.Fsm, this.logLevel, this.text.Value, this.sendToUnityLog);
			}
			base.Finish();
		}

		// Token: 0x0400832C RID: 33580
		[Tooltip("Info, Warning, or Error.")]
		public LogLevel logLevel;

		// Token: 0x0400832D RID: 33581
		[Tooltip("Text to send to the log.")]
		public FsmString text;
	}
}
