using System.Linq;

public class WhizbangCardEventHandler : CollectionCardEventHandler
{
	public PlayMakerFSM m_cardAddedGlow;

	public PlayMakerFSM m_cardRemovedGlow;

	public override void OnCardAdded(CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
		string cardId = cardEntityDef.GetCardId();
		if (!GameDbf.GetIndex().HasCardPlayerDeckOverride(cardId))
		{
			Log.CollectionManager.PrintError("{0}.OnCardAdded(): Added card's ID is {1} and not one of the valid cardIds ({2})!", this, cardId, string.Join(", ", (from r in GameDbf.GetIndex().GetAllCardPlayerDeckOverrides()
				select GameUtils.TranslateDbIdToCardId(r.CardId)).ToArray()));
			return;
		}
		CardPlayerDeckOverrideDbfRecord playerDeckOverride = GameDbf.GetIndex().GetCardPlayerDeckOverride(cardId);
		if (deck.GetTotalCardCount() <= 1)
		{
			AddWhizbang(playerDeckOverride, collectionDeckTray, deck, cardEntityDef, premium, animateActor);
			return;
		}
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = playerDeckOverride.AddToDeckWarningHeader,
			m_text = playerDeckOverride.AddToDeckWarningBody,
			m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES"),
			m_cancelText = GameStrings.Get("GLOBAL_BUTTON_NO"),
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CONFIRM)
				{
					for (int num = deck.GetSlots().Count - 1; num >= 0; num--)
					{
						CollectionDeckSlot collectionDeckSlot = deck.GetSlots()[num];
						if (collectionDeckSlot.CardID != cardId)
						{
							collectionDeckTray.RemoveAllCopiesOfCard(collectionDeckSlot.CardID);
						}
					}
					AddWhizbang(playerDeckOverride, collectionDeckTray, deck, cardEntityDef, premium, animateActor);
				}
				else
				{
					collectionDeckTray.RemoveAllCopiesOfCard(cardId);
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	public override void OnCardRemoved(CollectionDeckTray collectionDeckTray, CollectionDeck deck)
	{
		int uiHeroOverrideCardDbId = ((!string.IsNullOrEmpty(deck.UIHeroOverrideCardID)) ? GameUtils.TranslateCardIdToDbId(deck.UIHeroOverrideCardID) : 0);
		if (GameDbf.GetIndex().GetAllCardPlayerDeckOverrides().Any((CardPlayerDeckOverrideDbfRecord r) => r.HeroCardId != 0 && r.HeroCardId == uiHeroOverrideCardDbId))
		{
			if (m_cardRemovedGlow != null)
			{
				m_cardRemovedGlow.SendEvent("DoAnim");
			}
			collectionDeckTray.GetDecksContent().UpdateEditingDeckBoxVisual(deck.HeroCardID);
			deck.UIHeroOverrideCardID = string.Empty;
			deck.UIHeroOverridePremium = TAG_PREMIUM.NORMAL;
			deck.Name = GameStrings.Format("GLOBAL_BASIC_DECK_NAME", GameStrings.GetClassName(deck.GetClass()));
			collectionDeckTray.GetEditingDeckBox().SetDeckName(deck.Name);
			CollectionManager.Get().GetCollectibleDisplay().m_pageManager.UpdateVisibleTabs();
			CollectionManager.Get().OnUIHeroOverrideCardRemoved();
		}
	}

	public override bool ShouldUpdateVisuals()
	{
		return false;
	}

	private void AddWhizbang(CardPlayerDeckOverrideDbfRecord playerDeckOverride, CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
		if (m_cardAddedGlow != null)
		{
			m_cardAddedGlow.SendEvent("DoAnim");
		}
		string text = GameUtils.TranslateDbIdToCardId(playerDeckOverride.HeroCardId);
		collectionDeckTray.GetDecksContent().UpdateEditingDeckBoxVisual(text, premium);
		deck.UIHeroOverrideCardID = text;
		deck.UIHeroOverridePremium = premium;
		deck.Name = playerDeckOverride.DeckName;
		collectionDeckTray.GetEditingDeckBox().SetDeckName(deck.Name);
		collectionDeckTray.GetCardsContent().UpdateCardList(cardEntityDef, updateHighlight: true, animateActor);
		CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(playSound: true);
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.UpdateVisibleTabs();
	}
}
