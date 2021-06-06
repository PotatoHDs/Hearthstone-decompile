namespace Hearthstone.UI
{
	public class WaitSecondsStateAction : StateActionImplementation
	{
		public override void Run(bool runSynchronously = false)
		{
		}

		public override void Update()
		{
			if (base.SecondsSinceRun >= GetOverride(0).ValueDouble)
			{
				Complete(AsyncOperationResult.Success);
			}
			else
			{
				Complete(AsyncOperationResult.Wait);
			}
		}
	}
}
