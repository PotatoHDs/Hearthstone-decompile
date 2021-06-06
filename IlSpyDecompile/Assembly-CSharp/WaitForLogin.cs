using Blizzard.T5.Jobs;

public class WaitForLogin : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		return Network.IsLoggedIn();
	}
}
