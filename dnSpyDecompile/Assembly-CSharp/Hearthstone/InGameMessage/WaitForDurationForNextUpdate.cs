using System;
using Blizzard.T5.Jobs;
using UnityEngine;

namespace Hearthstone.InGameMessage
{
	// Token: 0x02001154 RID: 4436
	public class WaitForDurationForNextUpdate : IJobDependency, IAsyncJobResult
	{
		// Token: 0x0600C273 RID: 49779 RVA: 0x003AE6E8 File Offset: 0x003AC8E8
		public WaitForDurationForNextUpdate(float seconds)
		{
			this.m_targetTime = Time.realtimeSinceStartup + seconds;
		}

		// Token: 0x0600C274 RID: 49780 RVA: 0x003AE700 File Offset: 0x003AC900
		public bool IsReady()
		{
			InGameMessageScheduler inGameMessageScheduler;
			return HearthstoneServices.TryGet<InGameMessageScheduler>(out inGameMessageScheduler) && (inGameMessageScheduler.IsTerminated || Time.realtimeSinceStartup >= this.m_targetTime || (InGameMessageScheduler.Get() != null && InGameMessageScheduler.Get().HasNewRegisteredType));
		}

		// Token: 0x04009CA1 RID: 40097
		private float m_targetTime;
	}
}
