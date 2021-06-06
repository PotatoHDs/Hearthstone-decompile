using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E08 RID: 3592
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Starts location service updates. Last location coordinates can be retrieved with GetLocationInfo.")]
	public class StartLocationServiceUpdates : FsmStateAction
	{
		// Token: 0x0600A6F5 RID: 42741 RVA: 0x0034B2FF File Offset: 0x003494FF
		public override void Reset()
		{
			this.maxWait = 20f;
			this.desiredAccuracy = 10f;
			this.updateDistance = 10f;
			this.successEvent = null;
			this.failedEvent = null;
		}

		// Token: 0x0600A6F6 RID: 42742 RVA: 0x00328883 File Offset: 0x00326A83
		public override void OnEnter()
		{
			base.Finish();
		}

		// Token: 0x0600A6F7 RID: 42743 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void OnUpdate()
		{
		}

		// Token: 0x04008D80 RID: 36224
		[Tooltip("Maximum time to wait in seconds before failing.")]
		public FsmFloat maxWait;

		// Token: 0x04008D81 RID: 36225
		public FsmFloat desiredAccuracy;

		// Token: 0x04008D82 RID: 36226
		public FsmFloat updateDistance;

		// Token: 0x04008D83 RID: 36227
		[Tooltip("Event to send when the location services have started.")]
		public FsmEvent successEvent;

		// Token: 0x04008D84 RID: 36228
		[Tooltip("Event to send if the location services fail to start.")]
		public FsmEvent failedEvent;
	}
}
