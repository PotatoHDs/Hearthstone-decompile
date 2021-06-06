using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;

public class ShopCardList
{
	private UIBScrollable m_scrollbar;

	private MiniSetDetailsDataModel m_dataModel;

	private CardTileDataModel m_clickedTile;

	private CardDataModel m_selectedCard = new CardDataModel
	{
		CardId = ""
	};

	private Widget m_parentWidget;

	public ShopCardList(Widget parentWidget, UIBScrollable scrollbar)
	{
		m_parentWidget = parentWidget;
		m_scrollbar = scrollbar;
	}

	public void InitInput()
	{
		m_parentWidget.RegisterEventListener(HandleMouseEvents);
		m_scrollbar.SetScrollImmediate(0f);
		m_scrollbar.AddTouchScrollStartedListener(BindNoCard);
	}

	public void RemoveListeners()
	{
		m_parentWidget.RemoveEventListener(HandleMouseEvents);
		m_scrollbar.RemoveTouchScrollStartedListener(BindNoCard);
	}

	public void SetData(DeckDbfRecord deck, BoosterDbId boosterId)
	{
		DefLoader loader = DefLoader.Get();
		IEnumerable<CardTileDataModel> collection = from cr in deck.Cards
			group cr by cr.CardId into g
			select new
			{
				def = loader.GetEntityDef(g.Key),
				count = g.Count()
			} into ed
			orderby ed.def.GetRarity() descending, ed.def.GetCost()
			select new CardTileDataModel
			{
				CardId = ed.def.GetCardId(),
				Count = ed.count,
				Premium = TAG_PREMIUM.NORMAL
			};
		DataModelList<CardTileDataModel> dataModelList = new DataModelList<CardTileDataModel>();
		dataModelList.AddRange(collection);
		m_dataModel = new MiniSetDetailsDataModel
		{
			CardTiles = dataModelList,
			Pack = new PackDataModel
			{
				Type = boosterId
			},
			SelectedCard = m_selectedCard
		};
		BindNoCard();
		m_parentWidget.BindDataModel(m_dataModel);
	}

	private void BindNoCard()
	{
		m_selectedCard.CardId = "";
		if (m_clickedTile != null)
		{
			m_clickedTile.Selected = false;
			m_clickedTile = null;
		}
	}

	private CardTileDataModel GetEventPayload()
	{
		return m_parentWidget.GetDataModel<EventDataModel>().Payload as CardTileDataModel;
	}

	private void HandleMouseEvents(string eventName)
	{
		if (m_scrollbar.IsTouchDragging())
		{
			BindNoCard();
			return;
		}
		switch (eventName)
		{
		case "TILE_OVER_code":
			m_selectedCard.CardId = GetEventPayload().CardId;
			break;
		case "TILE_OUT_code":
			BindNoCard();
			break;
		case "TILE_CLICKED_code":
		{
			CardTileDataModel eventPayload = GetEventPayload();
			eventPayload.Selected = true;
			m_clickedTile = eventPayload;
			break;
		}
		case "TILE_RELEASED_code":
			GetEventPayload().Selected = false;
			m_clickedTile = null;
			break;
		}
	}
}
