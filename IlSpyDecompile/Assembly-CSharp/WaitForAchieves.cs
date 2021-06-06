using Blizzard.T5.Jobs;

public class WaitForAchieves : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		if (HearthstoneServices.TryGet<AchieveManager>(out var service))
		{
			return service.IsReady();
		}
		return false;
	}
}
