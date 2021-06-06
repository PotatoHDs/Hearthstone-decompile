using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class MiniSetLayout : MonoBehaviour
{
	public Widget m_widget;

	public static MiniSetDbfRecord GetDbfRecord(ProductDataModel product)
	{
		int itemId = product.Items.First().ItemId;
		return GameDbf.MiniSet.GetRecord(itemId);
	}

	private void Start()
	{
		DeckDbfRecord deckRecord = GetDbfRecord(m_widget.GetDataModel<ProductDataModel>()).DeckRecord;
		DefLoader loader = DefLoader.Get();
		RewardListDataModel dataModel = new RewardListDataModel
		{
			Items = (from c in deckRecord.Cards
				select loader.GetEntityDef(c.CardId) into ed
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
			}).ToDataModelList()
		};
		m_widget.BindDataModel(dataModel);
	}
}
