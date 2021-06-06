using Blizzard.T5.Jobs;

public class WaitForPlayerCollection : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		if (CollectionManager.Get() != null)
		{
			return CollectionManager.Get().IsFullyLoaded();
		}
		return false;
	}
}
