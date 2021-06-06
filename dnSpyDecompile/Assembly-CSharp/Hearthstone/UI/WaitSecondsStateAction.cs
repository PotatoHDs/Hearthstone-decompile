using System;

namespace Hearthstone.UI
{
	// Token: 0x0200102A RID: 4138
	public class WaitSecondsStateAction : StateActionImplementation
	{
		// Token: 0x0600B35E RID: 45918 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Run(bool runSynchronously = false)
		{
		}

		// Token: 0x0600B35F RID: 45919 RVA: 0x00373B03 File Offset: 0x00371D03
		public override void Update()
		{
			if (base.SecondsSinceRun >= base.GetOverride(0).ValueDouble)
			{
				base.Complete(AsyncOperationResult.Success);
				return;
			}
			base.Complete(AsyncOperationResult.Wait);
		}
	}
}
