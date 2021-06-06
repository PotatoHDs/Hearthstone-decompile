using Blizzard.T5.Jobs;

public class WaitForBaseUI : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		return BaseUI.Get() != null;
	}
}
