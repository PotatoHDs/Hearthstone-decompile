using Blizzard.T5.Jobs;

public class WaitForSplashScreen : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		return SplashScreen.Get() != null;
	}
}
