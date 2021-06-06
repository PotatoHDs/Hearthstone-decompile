using System;
using Assets;

namespace Hearthstone.DungeonCrawl
{
	public class DungeonCrawlSubscenControllerAdapter : ISubsceneController
	{
		public event EventHandler TransitionComplete;

		public void ChangeSubScene(AdventureData.Adventuresubscene subscene, bool pushToBackStack = true)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (!(adventureConfig == null))
			{
				adventureConfig.ChangeSubScene(subscene, pushToBackStack);
			}
		}

		public void SubSceneGoBack(bool fireEvent = true)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (!(adventureConfig == null))
			{
				adventureConfig.SubSceneGoBack(fireEvent);
			}
		}

		public void RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene subscene)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (!(adventureConfig == null))
			{
				adventureConfig.RemoveSubSceneIfOnTopOfStack(subscene);
			}
		}

		public void RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene targetSubscene)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (!(adventureConfig == null))
			{
				adventureConfig.RemoveSubScenesFromStackUntilTargetReached(targetSubscene);
			}
		}

		public void OnTransitionComplete()
		{
			if (this.TransitionComplete != null)
			{
				this.TransitionComplete(this, EventArgs.Empty);
			}
		}
	}
}
