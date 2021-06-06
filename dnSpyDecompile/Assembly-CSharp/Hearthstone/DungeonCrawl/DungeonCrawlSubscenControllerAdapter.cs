using System;
using Assets;

namespace Hearthstone.DungeonCrawl
{
	// Token: 0x0200116B RID: 4459
	public class DungeonCrawlSubscenControllerAdapter : ISubsceneController
	{
		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x0600C34A RID: 49994 RVA: 0x003B0D0C File Offset: 0x003AEF0C
		// (remove) Token: 0x0600C34B RID: 49995 RVA: 0x003B0D44 File Offset: 0x003AEF44
		public event EventHandler TransitionComplete;

		// Token: 0x0600C34C RID: 49996 RVA: 0x003B0D7C File Offset: 0x003AEF7C
		public void ChangeSubScene(AdventureData.Adventuresubscene subscene, bool pushToBackStack = true)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return;
			}
			adventureConfig.ChangeSubScene(subscene, pushToBackStack);
		}

		// Token: 0x0600C34D RID: 49997 RVA: 0x003B0DA4 File Offset: 0x003AEFA4
		public void SubSceneGoBack(bool fireEvent = true)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return;
			}
			adventureConfig.SubSceneGoBack(fireEvent);
		}

		// Token: 0x0600C34E RID: 49998 RVA: 0x003B0DC8 File Offset: 0x003AEFC8
		public void RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene subscene)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return;
			}
			adventureConfig.RemoveSubSceneIfOnTopOfStack(subscene);
		}

		// Token: 0x0600C34F RID: 49999 RVA: 0x003B0DEC File Offset: 0x003AEFEC
		public void RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene targetSubscene)
		{
			AdventureConfig adventureConfig = AdventureConfig.Get();
			if (adventureConfig == null)
			{
				return;
			}
			adventureConfig.RemoveSubScenesFromStackUntilTargetReached(targetSubscene);
		}

		// Token: 0x0600C350 RID: 50000 RVA: 0x003B0E10 File Offset: 0x003AF010
		public void OnTransitionComplete()
		{
			if (this.TransitionComplete != null)
			{
				this.TransitionComplete(this, EventArgs.Empty);
			}
		}
	}
}
