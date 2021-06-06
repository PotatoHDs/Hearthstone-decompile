using Blizzard.T5.Jobs;

public class WaitForNetCacheObject<T> : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		if (HearthstoneServices.TryGet<NetCache>(out var service))
		{
			return service.GetNetObject<T>() != null;
		}
		return false;
	}
}
