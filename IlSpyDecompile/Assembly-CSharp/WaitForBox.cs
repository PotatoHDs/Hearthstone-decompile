using Blizzard.T5.Jobs;

public class WaitForBox : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		return Box.Get() != null;
	}
}
