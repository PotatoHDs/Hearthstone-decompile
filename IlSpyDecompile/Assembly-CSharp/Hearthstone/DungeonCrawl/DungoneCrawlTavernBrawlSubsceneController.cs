using System;
using Assets;
using UnityEngine;

namespace Hearthstone.DungeonCrawl
{
	public class DungoneCrawlTavernBrawlSubsceneController : ISubsceneController
	{
		public event EventHandler TransitionComplete;

		public void ChangeSubScene(AdventureData.Adventuresubscene subscene, bool pushToBackStack = true)
		{
			if (subscene == AdventureData.Adventuresubscene.ADVENTURER_PICKER)
			{
				bool flag = false;
				if (!(GuestHeroPickerDisplay.Get() != null) && !(HeroPickerDisplay.Get() != null))
				{
					flag = AssetLoader.Get().InstantiatePrefab("GuestHeroPicker.prefab:3ecbc18da1de3ef4fa30532f90b20e59", delegate(AssetReference name, GameObject go, object data)
					{
						if (!(go == null))
						{
							PresenceMgr.Get().SetStatus(Global.PresenceStatus.TAVERN_BRAWL_DECKEDITOR);
						}
					});
				}
				else
				{
					Log.All.PrintWarning("Tavern Brawl Dungeon Run hero picker was already loaded when trying to change subscenes.");
				}
				if (!flag)
				{
					Debug.LogWarning("Failed to load hero picker.");
				}
			}
			else
			{
				Debug.LogWarningFormat("Tavern Brawl Dungeon Run tried to load unsupported subscene {0}.", subscene);
			}
		}

		public void SubSceneGoBack(bool fireEvent = true)
		{
			StoreManager.Get().HideStore(ShopType.TAVERN_BRAWL_STORE);
			SceneMgr.Mode mode = ((FiresideGatheringManager.Get().CurrentFiresideGatheringMode == FiresideGatheringManager.FiresideGatheringMode.NONE) ? SceneMgr.Mode.HUB : SceneMgr.Mode.FIRESIDE_GATHERING);
			SceneMgr.Get().SetNextMode(mode);
		}

		public void RemoveSubSceneIfOnTopOfStack(AdventureData.Adventuresubscene subscene)
		{
		}

		public void RemoveSubScenesFromStackUntilTargetReached(AdventureData.Adventuresubscene targetSubscene)
		{
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
