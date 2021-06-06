using System;
using Assets;

namespace Hearthstone.DungeonCrawl
{
	public class DungoneCrawlPvPSubsceneController : ISubsceneController
	{
		public event EventHandler TransitionComplete;

		public void ChangeSubScene(AdventureData.Adventuresubscene subscene, bool pushToBackStack = true)
		{
			if (subscene == AdventureData.Adventuresubscene.ADVENTURER_PICKER)
			{
				bool flag = false;
				if (!(GuestHeroPickerDisplay.Get() != null) && !(HeroPickerDisplay.Get() != null))
				{
					flag = AssetLoader.Get().InstantiatePrefab("GuestHeroPicker.prefab:3ecbc18da1de3ef4fa30532f90b20e59", delegate
					{
					});
				}
				if (!flag)
				{
					Log.All.PrintWarning("Failed to load hero picker.");
				}
			}
		}

		public void SubSceneGoBack(bool fireEvent = true)
		{
			SceneMgr.Mode mode = SceneMgr.Mode.HUB;
			SceneMgr.Get().SetNextMode(mode);
		}

		public void OnTransitionComplete()
		{
			if (this.TransitionComplete != null)
			{
				this.TransitionComplete(this, EventArgs.Empty);
			}
		}

		public void RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene targetSubscene)
		{
		}

		public void RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene subscene)
		{
		}
	}
}
