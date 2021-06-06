using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BD9 RID: 3033
	public abstract class BaseLogAction : FsmStateAction
	{
		// Token: 0x06009CC8 RID: 40136 RVA: 0x00326912 File Offset: 0x00324B12
		public override void Reset()
		{
			this.sendToUnityLog = false;
		}

		// Token: 0x04008243 RID: 33347
		public bool sendToUnityLog;
	}
}
