using System;
using System.Linq;

// Token: 0x020000FD RID: 253
public class WhizbangCardEventHandler : CollectionCardEventHandler
{
	// Token: 0x06000E81 RID: 3713 RVA: 0x00051644 File Offset: 0x0004F844
	public override void OnCardAdded(CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
		string cardId = cardEntityDef.GetCardId();
		if (!GameDbf.GetIndex().HasCardPlayerDeckOverride(cardId))
		{
			Logger collectionManager = Log.CollectionManager;
			string format = "{0}.OnCardAdded(): Added card's ID is {1} and not one of the valid cardIds ({2})!";
			object[] array = new object[3];
			array[0] = this;
			array[1] = cardId;
			array[2] = string.Join(", ", (from r in GameDbf.GetIndex().GetAllCardPlayerDeckOverrides()
			select GameUtils.TranslateDbIdToCardId(r.CardId, false)).ToArray<string>());
			collectionManager.PrintError(format, array);
			return;
		}
		CardPlayerDeckOverrideDbfRecord playerDeckOverride = GameDbf.GetIndex().GetCardPlayerDeckOverride(cardId);
		if (deck.GetTotalCardCount() <= 1)
		{
			this.AddWhizbang(playerDeckOverride, collectionDeckTray, deck, cardEntityDef, premium, animateActor);
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
					for (int i = deck.GetSlots().Count - 1; i >= 0; i--)
					{
						CollectionDeckSlot collectionDeckSlot = deck.GetSlots()[i];
						if (collectionDeckSlot.CardID != cardId)
						{
							collectionDeckTray.RemoveAllCopiesOfCard(collectionDeckSlot.CardID);
						}
					}
					this.AddWhizbang(playerDeckOverride, collectionDeckTray, deck, cardEntityDef, premium, animateActor);
					return;
				}
				collectionDeckTray.RemoveAllCopiesOfCard(cardId);
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x000517DC File Offset: 0x0004F9DC
	public override void OnCardRemoved(CollectionDeckTray collectionDeckTray, CollectionDeck deck)
	{
		int uiHeroOverrideCardDbId = string.IsNullOrEmpty(deck.UIHeroOverrideCardID) ? 0 : GameUtils.TranslateCardIdToDbId(deck.UIHeroOverrideCardID, false);
		if (!GameDbf.GetIndex().GetAllCardPlayerDeckOverrides().Any((CardPlayerDeckOverrideDbfRecord r) => r.HeroCardId != 0 && r.HeroCardId == uiHeroOverrideCardDbId))
		{
			return;
		}
		if (this.m_cardRemovedGlow != null)
		{
			this.m_cardRemovedGlow.SendEvent("DoAnim");
		}
		collectionDeckTray.GetDecksContent().UpdateEditingDeckBoxVisual(deck.HeroCardID, null);
		deck.UIHeroOverrideCardID = string.Empty;
		deck.UIHeroOverridePremium = TAG_PREMIUM.NORMAL;
		deck.Name = GameStrings.Format("GLOBAL_BASIC_DECK_NAME", new object[]
		{
			GameStrings.GetClassName(deck.GetClass())
		});
		collectionDeckTray.GetEditingDeckBox().SetDeckName(deck.Name);
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.UpdateVisibleTabs();
		CollectionManager.Get().OnUIHeroOverrideCardRemoved();
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldUpdateVisuals()
	{
		return false;
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x000518CC File Offset: 0x0004FACC
	private void AddWhizbang(CardPlayerDeckOverrideDbfRecord playerDeckOverride, CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
		if (this.m_cardAddedGlow != null)
		{
			this.m_cardAddedGlow.SendEvent("DoAnim");
		}
		string text = GameUtils.TranslateDbIdToCardId(playerDeckOverride.HeroCardId, false);
		collectionDeckTray.GetDecksContent().UpdateEditingDeckBoxVisual(text, new TAG_PREMIUM?(premium));
		deck.UIHeroOverrideCardID = text;
		deck.UIHeroOverridePremium = premium;
		deck.Name = playerDeckOverride.DeckName;
		collectionDeckTray.GetEditingDeckBox().SetDeckName(deck.Name);
		collectionDeckTray.GetCardsContent().UpdateCardList(cardEntityDef, true, animateActor, null);
		CollectionManager.Get().GetCollectibleDisplay().UpdateCurrentPageCardLocks(true);
		CollectionManager.Get().GetCollectibleDisplay().m_pageManager.UpdateVisibleTabs();
	}

	// Token: 0x04000A03 RID: 2563
	public PlayMakerFSM m_cardAddedGlow;

	// Token: 0x04000A04 RID: 2564
	public PlayMakerFSM m_cardRemovedGlow;
}
