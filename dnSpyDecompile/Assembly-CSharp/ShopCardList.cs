using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.DataModels;
using Hearthstone.UI;

// Token: 0x020006B9 RID: 1721
public class ShopCardList
{
	// Token: 0x0600608A RID: 24714 RVA: 0x001F7237 File Offset: 0x001F5437
	public ShopCardList(Widget parentWidget, UIBScrollable scrollbar)
	{
		this.m_parentWidget = parentWidget;
		this.m_scrollbar = scrollbar;
	}

	// Token: 0x0600608B RID: 24715 RVA: 0x001F7263 File Offset: 0x001F5463
	public void InitInput()
	{
		this.m_parentWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.HandleMouseEvents));
		this.m_scrollbar.SetScrollImmediate(0f);
		this.m_scrollbar.AddTouchScrollStartedListener(new UIBScrollable.OnTouchScrollStarted(this.BindNoCard));
	}

	// Token: 0x0600608C RID: 24716 RVA: 0x001F72A3 File Offset: 0x001F54A3
	public void RemoveListeners()
	{
		this.m_parentWidget.RemoveEventListener(new Widget.EventListenerDelegate(this.HandleMouseEvents));
		this.m_scrollbar.RemoveTouchScrollStartedListener(new UIBScrollable.OnTouchScrollStarted(this.BindNoCard));
	}

	// Token: 0x0600608D RID: 24717 RVA: 0x001F72D4 File Offset: 0x001F54D4
	public void SetData(DeckDbfRecord deck, BoosterDbId boosterId)
	{
		DefLoader loader = DefLoader.Get();
		IEnumerable<CardTileDataModel> collection = from cr in deck.Cards
		group cr by cr.CardId into g
		select new
		{
			def = loader.GetEntityDef(g.Key, true),
			count = g.Count<DeckCardDbfRecord>()
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
		this.m_dataModel = new MiniSetDetailsDataModel
		{
			CardTiles = dataModelList,
			Pack = new PackDataModel
			{
				Type = boosterId
			},
			SelectedCard = this.m_selectedCard
		};
		this.BindNoCard();
		this.m_parentWidget.BindDataModel(this.m_dataModel, false);
	}

	// Token: 0x0600608E RID: 24718 RVA: 0x001F73EF File Offset: 0x001F55EF
	private void BindNoCard()
	{
		this.m_selectedCard.CardId = "";
		if (this.m_clickedTile != null)
		{
			this.m_clickedTile.Selected = false;
			this.m_clickedTile = null;
		}
	}

	// Token: 0x0600608F RID: 24719 RVA: 0x001F741C File Offset: 0x001F561C
	private CardTileDataModel GetEventPayload()
	{
		return this.m_parentWidget.GetDataModel<EventDataModel>().Payload as CardTileDataModel;
	}

	// Token: 0x06006090 RID: 24720 RVA: 0x001F7434 File Offset: 0x001F5634
	private void HandleMouseEvents(string eventName)
	{
		if (this.m_scrollbar.IsTouchDragging())
		{
			this.BindNoCard();
			return;
		}
		if (eventName == "TILE_OVER_code")
		{
			this.m_selectedCard.CardId = this.GetEventPayload().CardId;
			return;
		}
		if (eventName == "TILE_OUT_code")
		{
			this.BindNoCard();
			return;
		}
		if (eventName == "TILE_CLICKED_code")
		{
			CardTileDataModel eventPayload = this.GetEventPayload();
			eventPayload.Selected = true;
			this.m_clickedTile = eventPayload;
			return;
		}
		if (!(eventName == "TILE_RELEASED_code"))
		{
			return;
		}
		this.GetEventPayload().Selected = false;
		this.m_clickedTile = null;
	}

	// Token: 0x040050C7 RID: 20679
	private UIBScrollable m_scrollbar;

	// Token: 0x040050C8 RID: 20680
	private MiniSetDetailsDataModel m_dataModel;

	// Token: 0x040050C9 RID: 20681
	private CardTileDataModel m_clickedTile;

	// Token: 0x040050CA RID: 20682
	private CardDataModel m_selectedCard = new CardDataModel
	{
		CardId = ""
	};

	// Token: 0x040050CB RID: 20683
	private Widget m_parentWidget;
}
