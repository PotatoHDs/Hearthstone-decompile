using System;

// Token: 0x020000FB RID: 251
public class FitnessCoachIllidanEventHandler : CollectionCardEventHandler
{
	// Token: 0x06000E7B RID: 3707 RVA: 0x00051380 File Offset: 0x0004F580
	public override void OnCardAdded(CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
		if (cardEntityDef.GetCardId() != "BT_900proto")
		{
			Log.CollectionManager.PrintError("{0}.OnCardAdded(): Added card's ID is {1} and not Fitness Coach Illidan's ({2})!", new object[]
			{
				this,
				cardEntityDef.GetCardId(),
				"BT_900proto"
			});
			return;
		}
		if (deck.GetTotalCardCount() <= 1)
		{
			this.AddIllidan(collectionDeckTray, deck, cardEntityDef, premium, animateActor);
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
					for (int i = deck.GetSlots().Count - 1; i >= 0; i--)
					{
						CollectionDeckSlot collectionDeckSlot = deck.GetSlots()[i];
						if (collectionDeckSlot.CardID != "BT_900proto")
						{
							collectionDeckTray.RemoveAllCopiesOfCard(collectionDeckSlot.CardID);
						}
					}
					this.AddIllidan(collectionDeckTray, deck, cardEntityDef, premium, animateActor);
					return;
				}
				collectionDeckTray.RemoveAllCopiesOfCard("BT_900proto");
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldUpdateVisuals()
	{
		return false;
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x000514A8 File Offset: 0x0004F6A8
	private void AddIllidan(CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
		if (this.m_cardAddedGlow != null)
		{
			this.m_cardAddedGlow.SendEvent("DoAnim");
		}
		collectionDeckTray.GetDecksContent().UpdateEditingDeckBoxVisual("HERO_10", new TAG_PREMIUM?(TAG_PREMIUM.NORMAL));
		deck.HeroCardID = "HERO_10";
		deck.HeroOverridden = true;
		collectionDeckTray.RemoveAllCopiesOfCard("BT_900proto");
		CollectionManager.Get().GetCollectibleDisplay().ResetFilters(true);
		collectionDeckTray.GetCardsContent().UpdateCardList(cardEntityDef, true, animateActor, null);
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.UpdateFiltersForDeck(deck, TAG_CLASS.DEMONHUNTER, false, null, null);
		CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(true);
	}

	// Token: 0x04000A01 RID: 2561
	public PlayMakerFSM m_cardAddedGlow;

	// Token: 0x04000A02 RID: 2562
	public PlayMakerFSM m_cardRemovedGlow;
}
