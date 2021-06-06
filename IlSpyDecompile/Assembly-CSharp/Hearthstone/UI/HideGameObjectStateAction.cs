namespace Hearthstone.UI
{
	public class HideGameObjectStateAction : StateActionImplementation
	{
		public override void Run(bool loadSynchronously = false)
		{
			GetOverride(0).RegisterReadyListener(HandleReady);
		}

		private void HandleReady(object unused)
		{
			GetOverride(0).RemoveReadyOrInactiveListener(HandleReady);
			if (!GetOverride(0).Resolve(out var gameObject))
			{
				Complete(success: false);
				return;
			}
			gameObject.SetActive(value: false);
			Complete(success: true);
		}
	}
}
