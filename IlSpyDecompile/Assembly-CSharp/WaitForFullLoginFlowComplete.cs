using Blizzard.T5.Jobs;

public class WaitForFullLoginFlowComplete : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		if (HearthstoneServices.TryGet<LoginManager>(out var service))
		{
			return service.IsFullLoginFlowComplete;
		}
		return false;
	}
}
