using System;
using Assets;

namespace Hearthstone.DungeonCrawl
{
	// Token: 0x02001169 RID: 4457
	public interface ISubsceneController
	{
		// Token: 0x0600C317 RID: 49943
		void ChangeSubScene(AdventureData.Adventuresubscene subscene, bool pushToBackStack = true);

		// Token: 0x0600C318 RID: 49944
		void SubSceneGoBack(bool fireEvent = true);

		// Token: 0x0600C319 RID: 49945
		void RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene targetSubscene);

		// Token: 0x0600C31A RID: 49946
		void RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene subscene);

		// Token: 0x0600C31B RID: 49947
		void OnTransitionComplete();

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x0600C31C RID: 49948
		// (remove) Token: 0x0600C31D RID: 49949
		event EventHandler TransitionComplete;
	}
}
