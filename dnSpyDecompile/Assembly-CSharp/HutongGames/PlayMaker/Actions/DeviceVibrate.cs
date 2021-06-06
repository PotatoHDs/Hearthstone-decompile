using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C15 RID: 3093
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Causes the device to vibrate for half a second.")]
	public class DeviceVibrate : FsmStateAction
	{
		// Token: 0x06009DE9 RID: 40425 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x06009DEA RID: 40426 RVA: 0x00328883 File Offset: 0x00326A83
		public override void OnEnter()
		{
			base.Finish();
		}
	}
}
