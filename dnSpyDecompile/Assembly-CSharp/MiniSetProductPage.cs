using System;
using System.Linq;
using Blizzard.T5.AssetManager;
using Hearthstone.DataModels;
using UnityEngine;

// Token: 0x020006AB RID: 1707
public class MiniSetProductPage : ProductPage
{
	// Token: 0x06005F14 RID: 24340 RVA: 0x001EEAE7 File Offset: 0x001ECCE7
	public override void Open()
	{
		base.SetMusicOverride(MusicPlaylistType.Invalid);
		this.m_cardList = new ShopCardList(this.m_widget, this.m_scrollbar);
		base.Open();
		base.OnOpened += this.InitInput;
	}

	// Token: 0x06005F15 RID: 24341 RVA: 0x001EEB1F File Offset: 0x001ECD1F
	public override void Close()
	{
		base.Close();
		this.m_cardList.RemoveListeners();
	}

	// Token: 0x06005F16 RID: 24342 RVA: 0x001EEB32 File Offset: 0x001ECD32
	public void InitInput()
	{
		base.OnOpened -= this.InitInput;
		this.m_cardList.InitInput();
	}

	// Token: 0x06005F17 RID: 24343 RVA: 0x001EEB54 File Offset: 0x001ECD54
	protected override void OnProductSet()
	{
		base.OnProductSet();
		int itemId = base.Product.Items.First<RewardItemDataModel>().ItemId;
		MiniSetDbfRecord record = GameDbf.MiniSet.GetRecord(itemId);
		DeckDbfRecord deckRecord = record.DeckRecord;
		BoosterDbId id = (BoosterDbId)record.BoosterRecord.ID;
		this.m_cardList.SetData(deckRecord, id);
		using (AssetHandle<GameObject> assetHandle = ShopUtils.LoadStorePackPrefab(id))
		{
			base.SetMusicOverride(assetHandle.Asset.GetComponent<StorePackDef>().GetMiniSetPlaylist());
		}
	}

	// Token: 0x04005026 RID: 20518
	public UIBScrollable m_scrollbar;

	// Token: 0x04005027 RID: 20519
	private ShopCardList m_cardList;
}
