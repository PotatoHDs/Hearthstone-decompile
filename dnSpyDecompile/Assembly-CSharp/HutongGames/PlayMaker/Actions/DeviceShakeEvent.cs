using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C14 RID: 3092
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends an Event when the mobile device is shaken.")]
	public class DeviceShakeEvent : FsmStateAction
	{
		// Token: 0x06009DE6 RID: 40422 RVA: 0x0032A109 File Offset: 0x00328309
		public override void Reset()
		{
			this.shakeThreshold = 3f;
			this.sendEvent = null;
		}

		// Token: 0x06009DE7 RID: 40423 RVA: 0x0032A124 File Offset: 0x00328324
		public override void OnUpdate()
		{
			if (Input.acceleration.sqrMagnitude > this.shakeThreshold.Value * this.shakeThreshold.Value)
			{
				base.Fsm.Event(this.sendEvent);
			}
		}

		// Token: 0x04008340 RID: 33600
		[RequiredField]
		[Tooltip("Amount of acceleration required to trigger the event. Higher numbers require a harder shake.")]
		public FsmFloat shakeThreshold;

		// Token: 0x04008341 RID: 33601
		[RequiredField]
		[Tooltip("Event to send when Shake Threshold is exceeded.")]
		public FsmEvent sendEvent;
	}
}
