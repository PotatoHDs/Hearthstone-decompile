using Blizzard.T5.Jobs;

public class WaitForLogoAnimation : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		return LogoAnimation.Get() != null;
	}
}
