using Assets;
using PegasusShared;

public class CollectionDeckTrayDeckListContent : EditableDeckTrayDeckListContent
{
	private static FormatType s_PreHeroPickerFormat = FormatType.FT_STANDARD;

	public static FormatType s_HeroPickerFormat = FormatType.FT_STANDARD;

	protected override void OnDeckBoxVisualRelease(TraySection traySection)
	{
		CollectionDeckBoxVisual deckBox = traySection.m_deckBox;
		if (deckBox.IsLocked())
		{
			return;
		}
		if (!GameUtils.IsCardGameplayEventActive(deckBox.GetHeroCardID()))
		{
			DialogManager.Get().ShowClassUpcomingPopup();
			return;
		}
		deckBox.enabled = true;
		if (base.IsTouchDragging || m_deckTray.IsUpdatingTrayMode())
		{
			return;
		}
		long deckID = deckBox.GetDeckID();
		CollectionDeck deck = CollectionManager.Get().GetDeck(deckID);
		if (deck != null)
		{
			if (deck.IsBeingDeleted())
			{
				Log.DeckTray.Print($"CollectionDeckTrayDeckListContent.OnDeckBoxVisualRelease(): cannot edit deck {deck}; it is being deleted");
				return;
			}
			if (deck.IsSavingChanges())
			{
				Log.DeckTray.PrintWarning("CollectionDeckTrayDeckListContent.OnDeckBoxVisualRelease(): cannot edit deck {0}; waiting for changes to be saved", deck);
				return;
			}
		}
		if (IsEditingCards())
		{
			if (!UniversalInputManager.Get().IsTouchMode())
			{
				RenameCurrentlyEditingDeck();
			}
			else if (m_deckInfoTooltip != null && !m_deckInfoTooltip.IsShown())
			{
				ShowDeckInfo();
			}
		}
		else if (IsModeActive())
		{
			m_editingTraySection = traySection;
			m_centeringDeckList = m_editingTraySection.m_deckBox.GetPositionIndex();
			m_newDeckButton.SetEnabled(enabled: false);
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (collectionManagerDisplay != null)
			{
				collectionManagerDisplay.RequestContentsToShowDeck(deckID);
				collectionManagerDisplay.HideDeckHelpPopup();
				collectionManagerDisplay.HideSetFilterTutorial();
			}
			Options.Get().SetBool(Option.HAS_STARTED_A_DECK, val: true);
		}
	}

	protected override void StartCreateNewDeck()
	{
		PresenceMgr.Get().SetStatus(Global.PresenceStatus.DECKEDITOR);
		ShowNewDeckButton(newDeckButtonActive: false);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			s_PreHeroPickerFormat = Options.GetFormatType();
			if (Options.GetInRankedPlayMode())
			{
				s_HeroPickerFormat = Options.GetFormatType();
			}
			else
			{
				s_HeroPickerFormat = Options.Get().GetEnum<FormatType>(Option.FORMAT_TYPE_LAST_PLAYED);
			}
			collectionManagerDisplay.EnterSelectNewDeckHeroMode();
		}
	}

	protected override void EndCreateNewDeck(bool newDeck)
	{
		Options.SetFormatType(s_PreHeroPickerFormat);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.ExitSelectNewDeckHeroMode();
		}
		ShowNewDeckButton(newDeckButtonActive: true, delegate
		{
			if (newDeck)
			{
				UpdateAllTrays(immediate: true);
			}
		});
	}
}
