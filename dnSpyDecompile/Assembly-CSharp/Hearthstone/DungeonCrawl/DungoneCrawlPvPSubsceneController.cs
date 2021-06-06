using System;
using Assets;
using UnityEngine;

namespace Hearthstone.DungeonCrawl
{
	// Token: 0x0200116D RID: 4461
	public class DungoneCrawlPvPSubsceneController : ISubsceneController
	{
		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x0600C381 RID: 50049 RVA: 0x003B13DC File Offset: 0x003AF5DC
		// (remove) Token: 0x0600C382 RID: 50050 RVA: 0x003B1414 File Offset: 0x003AF614
		public event EventHandler TransitionComplete;

		// Token: 0x0600C383 RID: 50051 RVA: 0x003B144C File Offset: 0x003AF64C
		public void ChangeSubScene(AdventureData.Adventuresubscene subscene, bool pushToBackStack = true)
		{
			if (subscene == AdventureData.Adventuresubscene.ADVENTURER_PICKER)
			{
				bool flag = false;
				if (!(GuestHeroPickerDisplay.Get() != null) && !(HeroPickerDisplay.Get() != null))
				{
					flag = AssetLoader.Get().InstantiatePrefab("GuestHeroPicker.prefab:3ecbc18da1de3ef4fa30532f90b20e59", delegate(AssetReference name, GameObject go, object data)
					{
					}, null, AssetLoadingOptions.None);
				}
				if (!flag)
				{
					Log.All.PrintWarning("Failed to load hero picker.", Array.Empty<object>());
				}
			}
		}

		// Token: 0x0600C384 RID: 50052 RVA: 0x003B14CC File Offset: 0x003AF6CC
		public void SubSceneGoBack(bool fireEvent = true)
		{
			SceneMgr.Mode mode = SceneMgr.Mode.HUB;
			SceneMgr.Get().SetNextMode(mode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}

		// Token: 0x0600C385 RID: 50053 RVA: 0x003B14E8 File Offset: 0x003AF6E8
		public void OnTransitionComplete()
		{
			if (this.TransitionComplete != null)
			{
				this.TransitionComplete(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600C386 RID: 50054 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene targetSubscene)
		{
		}

		// Token: 0x0600C387 RID: 50055 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene subscene)
		{
		}
	}
}
