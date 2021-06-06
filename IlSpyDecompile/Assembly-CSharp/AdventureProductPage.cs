using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using UnityEngine;

public class AdventureProductPage : ProductPage
{
	public override void Open()
	{
		SetMusicOverride(MusicPlaylistType.Invalid);
		base.Open();
	}

	protected override void OnProductSet()
	{
		base.OnProductSet();
		RewardItemDataModel rewardItemDataModel = base.Product.Items.FirstOrDefault((RewardItemDataModel item) => item.ItemType == RewardItemType.ADVENTURE);
		if (rewardItemDataModel == null)
		{
			Log.Store.PrintError("No Adventures in Product \"{0}\"", base.Product.Name);
			return;
		}
		using AssetHandle<GameObject> assetHandle = ShopUtils.LoadStoreAdventurePrefab((AdventureDbId)rewardItemDataModel.ItemId);
		StoreAdventureDef storeAdventureDef = (assetHandle ? assetHandle.Asset.GetComponent<StoreAdventureDef>() : null);
		SetMusicOverride(storeAdventureDef ? storeAdventureDef.GetPlaylist() : MusicPlaylistType.Invalid);
	}
}
