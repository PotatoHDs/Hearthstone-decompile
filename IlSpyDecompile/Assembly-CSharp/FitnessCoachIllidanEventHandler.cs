public class FitnessCoachIllidanEventHandler : CollectionCardEventHandler
{
	public PlayMakerFSM m_cardAddedGlow;

	public PlayMakerFSM m_cardRemovedGlow;

	public override void OnCardAdded(CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
		if (cardEntityDef.GetCardId() != "BT_900proto")
		{
			Log.CollectionManager.PrintError("{0}.OnCardAdded(): Added card's ID is {1} and not Fitness Coach Illidan's ({2})!", this, cardEntityDef.GetCardId(), "BT_900proto");
			return;
		}
		if (deck.GetTotalCardCount() <= 1)
		{
			AddIllidan(collectionDeckTray, deck, cardEntityDef, premium, animateActor);
			return;
		}
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = "Demon Hunter?",
			m_text = "Adding Fitness Coach Illidan to your deck will remove all other cards. Continue?",
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
						if (collectionDeckSlot.CardID != "BT_900proto")
						{
							collectionDeckTray.RemoveAllCopiesOfCard(collectionDeckSlot.CardID);
						}
					}
					AddIllidan(collectionDeckTray, deck, cardEntityDef, premium, animateActor);
				}
				else
				{
					collectionDeckTray.RemoveAllCopiesOfCard("BT_900proto");
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	public override bool ShouldUpdateVisuals()
	{
		return false;
	}

	private void AddIllidan(CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
		if (m_cardAddedGlow != null)
		{
			m_cardAddedGlow.SendEvent("DoAnim");
		}
		collectionDeckTray.GetDecksContent().UpdateEditingDeckBoxVisual("HERO_10", TAG_PREMIUM.NORMAL);
		deck.HeroCardID = "HERO_10";
		deck.HeroOverridden = true;
		collectionDeckTray.RemoveAllCopiesOfCard("BT_900proto");
		CollectionManager.Get().GetCollectibleDisplay().ResetFilters();
		collectionDeckTray.GetCardsContent().UpdateCardList(cardEntityDef, updateHighlight: true, animateActor);
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.UpdateFiltersForDeck(deck, TAG_CLASS.DEMONHUNTER, skipPageTurn: false);
		CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(playSound: true);
	}
}
