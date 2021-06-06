using System.Linq;
using Blizzard.T5.AssetManager;
using UnityEngine;

public class MiniSetProductPage : ProductPage
{
	public UIBScrollable m_scrollbar;

	private ShopCardList m_cardList;

	public override void Open()
	{
		SetMusicOverride(MusicPlaylistType.Invalid);
		m_cardList = new ShopCardList(m_widget, m_scrollbar);
		base.Open();
		base.OnOpened += InitInput;
	}

	public override void Close()
	{
		base.Close();
		m_cardList.RemoveListeners();
	}

	public void InitInput()
	{
		base.OnOpened -= InitInput;
		m_cardList.InitInput();
	}

	protected override void OnProductSet()
	{
		base.OnProductSet();
		int itemId = base.Product.Items.First().ItemId;
		MiniSetDbfRecord record = GameDbf.MiniSet.GetRecord(itemId);
		DeckDbfRecord deckRecord = record.DeckRecord;
		BoosterDbId iD = (BoosterDbId)record.BoosterRecord.ID;
		m_cardList.SetData(deckRecord, iD);
		using AssetHandle<GameObject> assetHandle = ShopUtils.LoadStorePackPrefab(iD);
		SetMusicOverride(assetHandle.Asset.GetComponent<StorePackDef>().GetMiniSetPlaylist());
	}
}
