using Blizzard.T5.Jobs;
using UnityEngine;

namespace Hearthstone.InGameMessage
{
	public class WaitForDurationForNextUpdate : IJobDependency, IAsyncJobResult
	{
		private float m_targetTime;

		public WaitForDurationForNextUpdate(float seconds)
		{
			m_targetTime = Time.realtimeSinceStartup + seconds;
		}

		public bool IsReady()
		{
			if (!HearthstoneServices.TryGet<InGameMessageScheduler>(out var service))
			{
				return false;
			}
			if (!service.IsTerminated && !(Time.realtimeSinceStartup >= m_targetTime))
			{
				if (InGameMessageScheduler.Get() != null)
				{
					return InGameMessageScheduler.Get().HasNewRegisteredType;
				}
				return false;
			}
			return true;
		}
	}
}
