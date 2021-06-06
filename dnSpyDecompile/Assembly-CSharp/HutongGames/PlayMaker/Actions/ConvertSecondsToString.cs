using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BFE RID: 3070
	[ActionCategory(ActionCategory.Convert)]
	[HelpUrl("http://hutonggames.com/playmakerforum/index.php?topic=1711.0")]
	[Tooltip("Converts Seconds to a String value representing the time.")]
	public class ConvertSecondsToString : FsmStateAction
	{
		// Token: 0x06009D96 RID: 40342 RVA: 0x00329401 File Offset: 0x00327601
		public override void Reset()
		{
			this.secondsVariable = null;
			this.stringVariable = null;
			this.everyFrame = false;
			this.format = "{1:D2}h:{2:D2}m:{3:D2}s:{10}ms";
		}

		// Token: 0x06009D97 RID: 40343 RVA: 0x00329428 File Offset: 0x00327628
		public override void OnEnter()
		{
			this.DoConvertSecondsToString();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D98 RID: 40344 RVA: 0x0032943E File Offset: 0x0032763E
		public override void OnUpdate()
		{
			this.DoConvertSecondsToString();
		}

		// Token: 0x06009D99 RID: 40345 RVA: 0x00329448 File Offset: 0x00327648
		private void DoConvertSecondsToString()
		{
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)this.secondsVariable.Value);
			string text = timeSpan.Milliseconds.ToString("D3").PadLeft(2, '0');
			text = text.Substring(0, 2);
			this.stringVariable.Value = string.Format(this.format.Value, new object[]
			{
				timeSpan.Days,
				timeSpan.Hours,
				timeSpan.Minutes,
				timeSpan.Seconds,
				timeSpan.Milliseconds,
				timeSpan.TotalDays,
				timeSpan.TotalHours,
				timeSpan.TotalMinutes,
				timeSpan.TotalSeconds,
				timeSpan.TotalMilliseconds,
				text
			});
		}

		// Token: 0x04008300 RID: 33536
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The seconds variable to convert.")]
		public FsmFloat secondsVariable;

		// Token: 0x04008301 RID: 33537
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("A string variable to store the time value.")]
		public FsmString stringVariable;

		// Token: 0x04008302 RID: 33538
		[RequiredField]
		[Tooltip("Format. 0 for days, 1 is for hours, 2 for minutes, 3 for seconds and 4 for milliseconds. 5 for total days, 6 for total hours, 7 for total minutes, 8 for total seconds, 9 for total milliseconds, 10 for two digits milliseconds. so {2:D2} would just show the seconds of the current time, NOT the grand total number of seconds, the grand total of seconds would be {8:F0}")]
		public FsmString format;

		// Token: 0x04008303 RID: 33539
		[Tooltip("Repeat every frame. Useful if the seconds variable is changing.")]
		public bool everyFrame;
	}
}
