using Blizzard.T5.Jobs;

public class WaitForTooltipPanelManager : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		return TooltipPanelManager.Get() != null;
	}
}
