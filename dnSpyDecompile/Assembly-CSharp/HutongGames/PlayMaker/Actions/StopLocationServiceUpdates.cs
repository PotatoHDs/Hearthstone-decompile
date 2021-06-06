using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E12 RID: 3602
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Stops location service updates. This could be useful for saving battery life.")]
	public class StopLocationServiceUpdates : FsmStateAction
	{
		// Token: 0x0600A717 RID: 42775 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A718 RID: 42776 RVA: 0x00328883 File Offset: 0x00326A83
		public override void OnEnter()
		{
			base.Finish();
		}
	}
}
