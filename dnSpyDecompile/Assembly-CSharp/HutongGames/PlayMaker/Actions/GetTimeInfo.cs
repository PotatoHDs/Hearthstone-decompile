using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C8F RID: 3215
	[ActionCategory(ActionCategory.Time)]
	[Tooltip("Gets various useful Time measurements.")]
	public class GetTimeInfo : FsmStateAction
	{
		// Token: 0x0600A004 RID: 40964 RVA: 0x0032FB80 File Offset: 0x0032DD80
		public override void Reset()
		{
			this.getInfo = GetTimeInfo.TimeInfo.TimeSinceLevelLoad;
			this.storeValue = null;
			this.everyFrame = false;
		}

		// Token: 0x0600A005 RID: 40965 RVA: 0x0032FB97 File Offset: 0x0032DD97
		public override void OnEnter()
		{
			this.DoGetTimeInfo();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A006 RID: 40966 RVA: 0x0032FBAD File Offset: 0x0032DDAD
		public override void OnUpdate()
		{
			this.DoGetTimeInfo();
		}

		// Token: 0x0600A007 RID: 40967 RVA: 0x0032FBB8 File Offset: 0x0032DDB8
		private void DoGetTimeInfo()
		{
			switch (this.getInfo)
			{
			case GetTimeInfo.TimeInfo.DeltaTime:
				this.storeValue.Value = Time.deltaTime;
				return;
			case GetTimeInfo.TimeInfo.TimeScale:
				this.storeValue.Value = Time.timeScale;
				return;
			case GetTimeInfo.TimeInfo.SmoothDeltaTime:
				this.storeValue.Value = Time.smoothDeltaTime;
				return;
			case GetTimeInfo.TimeInfo.TimeInCurrentState:
				this.storeValue.Value = base.State.StateTime;
				return;
			case GetTimeInfo.TimeInfo.TimeSinceStartup:
				this.storeValue.Value = Time.time;
				return;
			case GetTimeInfo.TimeInfo.TimeSinceLevelLoad:
				this.storeValue.Value = Time.timeSinceLevelLoad;
				return;
			case GetTimeInfo.TimeInfo.RealTimeSinceStartup:
				this.storeValue.Value = FsmTime.RealtimeSinceStartup;
				return;
			case GetTimeInfo.TimeInfo.RealTimeInCurrentState:
				this.storeValue.Value = FsmTime.RealtimeSinceStartup - base.State.RealStartTime;
				return;
			default:
				this.storeValue.Value = 0f;
				return;
			}
		}

		// Token: 0x04008583 RID: 34179
		public GetTimeInfo.TimeInfo getInfo;

		// Token: 0x04008584 RID: 34180
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmFloat storeValue;

		// Token: 0x04008585 RID: 34181
		public bool everyFrame;

		// Token: 0x0200279A RID: 10138
		public enum TimeInfo
		{
			// Token: 0x0400F4CA RID: 62666
			DeltaTime,
			// Token: 0x0400F4CB RID: 62667
			TimeScale,
			// Token: 0x0400F4CC RID: 62668
			SmoothDeltaTime,
			// Token: 0x0400F4CD RID: 62669
			TimeInCurrentState,
			// Token: 0x0400F4CE RID: 62670
			TimeSinceStartup,
			// Token: 0x0400F4CF RID: 62671
			TimeSinceLevelLoad,
			// Token: 0x0400F4D0 RID: 62672
			RealTimeSinceStartup,
			// Token: 0x0400F4D1 RID: 62673
			RealTimeInCurrentState
		}
	}
}
