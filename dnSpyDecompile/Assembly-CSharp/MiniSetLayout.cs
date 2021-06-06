using System;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x020006AA RID: 1706
public class MiniSetLayout : MonoBehaviour
{
	// Token: 0x06005F11 RID: 24337 RVA: 0x001EE9D4 File Offset: 0x001ECBD4
	public static MiniSetDbfRecord GetDbfRecord(ProductDataModel product)
	{
		int itemId = product.Items.First<RewardItemDataModel>().ItemId;
		return GameDbf.MiniSet.GetRecord(itemId);
	}

	// Token: 0x06005F12 RID: 24338 RVA: 0x001EEA00 File Offset: 0x001ECC00
	private void Start()
	{
		DeckDbfRecord deckRecord = MiniSetLayout.GetDbfRecord(this.m_widget.GetDataModel<ProductDataModel>()).DeckRecord;
		DefLoader loader = DefLoader.Get();
		RewardListDataModel rewardListDataModel = new RewardListDataModel();
		rewardListDataModel.Items = (from c in deckRecord.Cards
		select loader.GetEntityDef(c.CardId, true) into ed
		where ed.GetRarity() == TAG_RARITY.LEGENDARY
		orderby ed.GetCost()
		select new RewardItemDataModel
		{
			ItemType = RewardItemType.CARD,
			Card = new CardDataModel
			{
				CardId = ed.GetCardId()
			}
		}).Append(new RewardItemDataModel
		{
			ItemType = RewardItemType.MINI_SET
		}).ToDataModelList<RewardItemDataModel>();
		RewardListDataModel dataModel = rewardListDataModel;
		this.m_widget.BindDataModel(dataModel, false);
	}

	// Token: 0x04005025 RID: 20517
	public Widget m_widget;
}
