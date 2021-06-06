public class GalakrondCollectionCardEventHandler : CollectionCardEventHandler
{
	public override void OnCardAdded(CollectionDeckTray collectionDeckTray, CollectionDeck deck, EntityDef cardEntityDef, TAG_PREMIUM premium, Actor animateActor)
	{
		if (deck.CreatedFromShareableDeck != null || deck.IsCreatedWithDeckComplete)
		{
			return;
		}
		string galakrondCardIdForClass = GameUtils.GetGalakrondCardIdByClass(deck.GetClass());
		if (string.IsNullOrEmpty(galakrondCardIdForClass) || deck.GetCardIdCount(galakrondCardIdForClass) != 0)
		{
			return;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_DECK_TRAY_GALAKROND_PROMPT_TITLE");
		popupInfo.m_text = GameStrings.Get("GLUE_DECK_TRAY_GALAKROND_PROMPT_DESC");
		popupInfo.m_iconSet = AlertPopup.PopupInfo.IconSet.Default;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
		popupInfo.m_confirmText = GameStrings.Get("GLUE_COLLECTION_DECK_COMPLETE_POPUP_CONFIRM");
		popupInfo.m_cancelText = GameStrings.Get("GLUE_COLLECTION_DECK_COMPLETE_POPUP_CANCEL");
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM)
			{
				TAG_PREMIUM preferredPremiumThatCanBeAddedToDeck = collectionDeckTray.GetPreferredPremiumThatCanBeAddedToDeck(deck, galakrondCardIdForClass);
				collectionDeckTray.AddCard(DefLoader.Get().GetEntityDef(galakrondCardIdForClass), preferredPremiumThatCanBeAddedToDeck, null, playSound: true, null, allowInvalid: true);
			}
		};
		DialogManager.Get().ShowPopup(popupInfo);
	}
}
