using Blizzard.T5.Jobs;

public class WaitForOverlayUI : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		return OverlayUI.Get() != null;
	}
}
