using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C6D RID: 3181
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Gets Location Info from a mobile device. NOTE: Use StartLocationService before trying to get location info.")]
	public class GetLocationInfo : FsmStateAction
	{
		// Token: 0x06009F72 RID: 40818 RVA: 0x0032E89F File Offset: 0x0032CA9F
		public override void Reset()
		{
			this.longitude = null;
			this.latitude = null;
			this.altitude = null;
			this.horizontalAccuracy = null;
			this.verticalAccuracy = null;
			this.errorEvent = null;
		}

		// Token: 0x06009F73 RID: 40819 RVA: 0x0032E8CB File Offset: 0x0032CACB
		public override void OnEnter()
		{
			this.DoGetLocationInfo();
			base.Finish();
		}

		// Token: 0x06009F74 RID: 40820 RVA: 0x00003BE8 File Offset: 0x00001DE8
		private void DoGetLocationInfo()
		{
		}

		// Token: 0x0400850D RID: 34061
		[UIHint(UIHint.Variable)]
		public FsmVector3 vectorPosition;

		// Token: 0x0400850E RID: 34062
		[UIHint(UIHint.Variable)]
		public FsmFloat longitude;

		// Token: 0x0400850F RID: 34063
		[UIHint(UIHint.Variable)]
		public FsmFloat latitude;

		// Token: 0x04008510 RID: 34064
		[UIHint(UIHint.Variable)]
		public FsmFloat altitude;

		// Token: 0x04008511 RID: 34065
		[UIHint(UIHint.Variable)]
		public FsmFloat horizontalAccuracy;

		// Token: 0x04008512 RID: 34066
		[UIHint(UIHint.Variable)]
		public FsmFloat verticalAccuracy;

		// Token: 0x04008513 RID: 34067
		[Tooltip("Event to send if the location cannot be queried.")]
		public FsmEvent errorEvent;
	}
}
