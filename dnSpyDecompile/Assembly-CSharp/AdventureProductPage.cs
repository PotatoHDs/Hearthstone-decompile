using System;
using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using UnityEngine;

// Token: 0x020006A1 RID: 1697
public class AdventureProductPage : ProductPage
{
	// Token: 0x06005EBD RID: 24253 RVA: 0x001ECB16 File Offset: 0x001EAD16
	public override void Open()
	{
		base.SetMusicOverride(MusicPlaylistType.Invalid);
		base.Open();
	}

	// Token: 0x06005EBE RID: 24254 RVA: 0x001ECB28 File Offset: 0x001EAD28
	protected override void OnProductSet()
	{
		base.OnProductSet();
		RewardItemDataModel rewardItemDataModel = base.Product.Items.FirstOrDefault((RewardItemDataModel item) => item.ItemType == RewardItemType.ADVENTURE);
		if (rewardItemDataModel == null)
		{
			Log.Store.PrintError("No Adventures in Product \"{0}\"", new object[]
			{
				base.Product.Name
			});
			return;
		}
		using (AssetHandle<GameObject> assetHandle = ShopUtils.LoadStoreAdventurePrefab((AdventureDbId)rewardItemDataModel.ItemId))
		{
			StoreAdventureDef storeAdventureDef = assetHandle ? assetHandle.Asset.GetComponent<StoreAdventureDef>() : null;
			base.SetMusicOverride(storeAdventureDef ? storeAdventureDef.GetPlaylist() : MusicPlaylistType.Invalid);
		}
	}
}
