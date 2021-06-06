using System;
using Assets;
using PegasusShared;

// Token: 0x02000889 RID: 2185
public class CollectionDeckTrayDeckListContent : EditableDeckTrayDeckListContent
{
	// Token: 0x06007766 RID: 30566 RVA: 0x0026FCB8 File Offset: 0x0026DEB8
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
		if (base.IsTouchDragging)
		{
			return;
		}
		if (this.m_deckTray.IsUpdatingTrayMode())
		{
			return;
		}
		long deckID = deckBox.GetDeckID();
		CollectionDeck deck = CollectionManager.Get().GetDeck(deckID);
		if (deck != null)
		{
			if (deck.IsBeingDeleted())
			{
				Log.DeckTray.Print(string.Format("CollectionDeckTrayDeckListContent.OnDeckBoxVisualRelease(): cannot edit deck {0}; it is being deleted", deck), Array.Empty<object>());
				return;
			}
			if (deck.IsSavingChanges())
			{
				Log.DeckTray.PrintWarning("CollectionDeckTrayDeckListContent.OnDeckBoxVisualRelease(): cannot edit deck {0}; waiting for changes to be saved", new object[]
				{
					deck
				});
				return;
			}
		}
		if (base.IsEditingCards())
		{
			if (!UniversalInputManager.Get().IsTouchMode())
			{
				base.RenameCurrentlyEditingDeck();
				return;
			}
			if (this.m_deckInfoTooltip != null && !this.m_deckInfoTooltip.IsShown())
			{
				this.ShowDeckInfo();
				return;
			}
		}
		else if (base.IsModeActive())
		{
			this.m_editingTraySection = traySection;
			this.m_centeringDeckList = this.m_editingTraySection.m_deckBox.GetPositionIndex();
			this.m_newDeckButton.SetEnabled(false, false);
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (collectionManagerDisplay != null)
			{
				collectionManagerDisplay.RequestContentsToShowDeck(deckID);
				collectionManagerDisplay.HideDeckHelpPopup();
				collectionManagerDisplay.HideSetFilterTutorial();
			}
			Options.Get().SetBool(Option.HAS_STARTED_A_DECK, true);
		}
	}

	// Token: 0x06007767 RID: 30567 RVA: 0x0026FE14 File Offset: 0x0026E014
	protected override void StartCreateNewDeck()
	{
		PresenceMgr.Get().SetStatus(new Enum[]
		{
			Global.PresenceStatus.DECKEDITOR
		});
		base.ShowNewDeckButton(false, null);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			CollectionDeckTrayDeckListContent.s_PreHeroPickerFormat = Options.GetFormatType();
			if (Options.GetInRankedPlayMode())
			{
				CollectionDeckTrayDeckListContent.s_HeroPickerFormat = Options.GetFormatType();
			}
			else
			{
				CollectionDeckTrayDeckListContent.s_HeroPickerFormat = Options.Get().GetEnum<FormatType>(Option.FORMAT_TYPE_LAST_PLAYED);
			}
			collectionManagerDisplay.EnterSelectNewDeckHeroMode();
		}
	}

	// Token: 0x06007768 RID: 30568 RVA: 0x0026FE94 File Offset: 0x0026E094
	protected override void EndCreateNewDeck(bool newDeck)
	{
		Options.SetFormatType(CollectionDeckTrayDeckListContent.s_PreHeroPickerFormat);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.ExitSelectNewDeckHeroMode();
		}
		base.ShowNewDeckButton(true, delegate(object o)
		{
			if (newDeck)
			{
				this.UpdateAllTrays(true, true);
			}
		});
	}

	// Token: 0x04005D91 RID: 23953
	private static FormatType s_PreHeroPickerFormat = FormatType.FT_STANDARD;

	// Token: 0x04005D92 RID: 23954
	public static FormatType s_HeroPickerFormat = FormatType.FT_STANDARD;
}
