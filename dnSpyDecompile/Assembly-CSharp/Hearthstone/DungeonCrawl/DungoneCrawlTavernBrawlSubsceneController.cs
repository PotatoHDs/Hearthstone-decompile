using System;
using Assets;
using UnityEngine;

namespace Hearthstone.DungeonCrawl
{
	// Token: 0x0200116F RID: 4463
	public class DungoneCrawlTavernBrawlSubsceneController : ISubsceneController
	{
		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x0600C3B9 RID: 50105 RVA: 0x003B17F0 File Offset: 0x003AF9F0
		// (remove) Token: 0x0600C3BA RID: 50106 RVA: 0x003B1828 File Offset: 0x003AFA28
		public event EventHandler TransitionComplete;

		// Token: 0x0600C3BB RID: 50107 RVA: 0x003B1860 File Offset: 0x003AFA60
		public void ChangeSubScene(AdventureData.Adventuresubscene subscene, bool pushToBackStack = true)
		{
			if (subscene == AdventureData.Adventuresubscene.ADVENTURER_PICKER)
			{
				bool flag = false;
				if (!(GuestHeroPickerDisplay.Get() != null) && !(HeroPickerDisplay.Get() != null))
				{
					flag = AssetLoader.Get().InstantiatePrefab("GuestHeroPicker.prefab:3ecbc18da1de3ef4fa30532f90b20e59", delegate(AssetReference name, GameObject go, object data)
					{
						if (go == null)
						{
							return;
						}
						PresenceMgr.Get().SetStatus(new Enum[]
						{
							Global.PresenceStatus.TAVERN_BRAWL_DECKEDITOR
						});
					}, null, AssetLoadingOptions.None);
				}
				else
				{
					Log.All.PrintWarning("Tavern Brawl Dungeon Run hero picker was already loaded when trying to change subscenes.", Array.Empty<object>());
				}
				if (!flag)
				{
					Debug.LogWarning("Failed to load hero picker.");
					return;
				}
			}
			else
			{
				Debug.LogWarningFormat("Tavern Brawl Dungeon Run tried to load unsupported subscene {0}.", new object[]
				{
					subscene
				});
			}
		}

		// Token: 0x0600C3BC RID: 50108 RVA: 0x003B1904 File Offset: 0x003AFB04
		public void SubSceneGoBack(bool fireEvent = true)
		{
			StoreManager.Get().HideStore(ShopType.TAVERN_BRAWL_STORE);
			SceneMgr.Mode mode = (FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.NONE) ? SceneMgr.Mode.HUB : SceneMgr.Mode.FIRESIDE_GATHERING;
			SceneMgr.Get().SetNextMode(mode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		}

		// Token: 0x0600C3BD RID: 50109 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene subscene)
		{
		}

		// Token: 0x0600C3BE RID: 50110 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene targetSubscene)
		{
		}

		// Token: 0x0600C3BF RID: 50111 RVA: 0x003B193B File Offset: 0x003AFB3B
		public void OnTransitionComplete()
		{
			if (this.TransitionComplete != null)
			{
				this.TransitionComplete(this, EventArgs.Empty);
			}
		}
	}
}
