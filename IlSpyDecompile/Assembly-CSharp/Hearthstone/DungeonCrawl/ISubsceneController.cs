using System;
using Assets;

namespace Hearthstone.DungeonCrawl
{
	public interface ISubsceneController
	{
		event EventHandler TransitionComplete;

		void ChangeSubScene(AdventureData.Adventuresubscene subscene, bool pushToBackStack = true);

		void SubSceneGoBack(bool fireEvent = true);

		void RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene targetSubscene);

		void RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene subscene);

		void OnTransitionComplete();
	}
}
