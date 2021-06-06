using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C13 RID: 3091
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends an Event based on the Orientation of the mobile device.")]
	public class DeviceOrientationEvent : FsmStateAction
	{
		// Token: 0x06009DE1 RID: 40417 RVA: 0x0032A0B4 File Offset: 0x003282B4
		public override void Reset()
		{
			this.orientation = DeviceOrientation.Portrait;
			this.sendEvent = null;
			this.everyFrame = false;
		}

		// Token: 0x06009DE2 RID: 40418 RVA: 0x0032A0CB File Offset: 0x003282CB
		public override void OnEnter()
		{
			this.DoDetectDeviceOrientation();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009DE3 RID: 40419 RVA: 0x0032A0E1 File Offset: 0x003282E1
		public override void OnUpdate()
		{
			this.DoDetectDeviceOrientation();
		}

		// Token: 0x06009DE4 RID: 40420 RVA: 0x0032A0E9 File Offset: 0x003282E9
		private void DoDetectDeviceOrientation()
		{
			if (Input.deviceOrientation == this.orientation)
			{
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x0400833D RID: 33597
		[Tooltip("Note: If device is physically situated between discrete positions, as when (for example) rotated diagonally, system will report Unknown orientation.")]
		public DeviceOrientation orientation;

		// Token: 0x0400833E RID: 33598
		[Tooltip("The event to send if the device orientation matches Orientation.")]
		public FsmEvent sendEvent;

		// Token: 0x0400833F RID: 33599
		[Tooltip("Repeat every frame. Useful if you want to wait for the orientation to be true.")]
		public bool everyFrame;
	}
}
