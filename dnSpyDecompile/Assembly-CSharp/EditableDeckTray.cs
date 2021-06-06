using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200089C RID: 2204
public abstract class EditableDeckTray : DeckTray
{
	// Token: 0x06007948 RID: 31048 RVA: 0x00279153 File Offset: 0x00277353
	protected override IEnumerator UpdateTrayMode()
	{
		DeckTray.DeckContentTypes oldContentType = this.m_currentContent;
		DeckTray.DeckContentTypes newContentType = this.m_contentToSet;
		if (this.m_settingNewMode || this.m_currentContent == this.m_contentToSet || this.m_contentToSet == DeckTray.DeckContentTypes.INVALID)
		{
			this.m_updatingTrayMode = false;
			yield break;
		}
		base.AllowInput(false);
		this.m_contentToSet = DeckTray.DeckContentTypes.INVALID;
		this.m_currentContent = DeckTray.DeckContentTypes.INVALID;
		this.m_settingNewMode = true;
		this.m_updatingTrayMode = true;
		DeckTrayContent oldContent = null;
		DeckTrayContent newContent = null;
		this.m_contents.TryGetValue(oldContentType, out oldContent);
		this.m_contents.TryGetValue(newContentType, out newContent);
		if (oldContent != null)
		{
			while (!oldContent.PreAnimateContentExit())
			{
				yield return null;
			}
		}
		if (newContent != null)
		{
			while (!newContent.PreAnimateContentEntrance())
			{
				yield return null;
			}
		}
		base.SaveScrollbarPosition(oldContentType);
		base.TryDisableScrollbar();
		if (oldContent != null)
		{
			oldContent.SetModeActive(false);
			while (!oldContent.AnimateContentExitStart())
			{
				yield return null;
			}
			Log.DeckTray.Print("OLD: {0} AnimateContentExitStart - finished", new object[]
			{
				oldContentType
			});
			while (!oldContent.AnimateContentExitEnd())
			{
				yield return null;
			}
			Log.DeckTray.Print("OLD: {0} AnimateContentExitEnd - finished", new object[]
			{
				oldContentType
			});
		}
		this.m_currentContent = newContentType;
		if (newContent != null)
		{
			newContent.SetModeTrying(true);
			while (!newContent.AnimateContentEntranceStart())
			{
				yield return null;
			}
			Log.DeckTray.Print("NEW: {0} AnimateContentEntranceStart - finished", new object[]
			{
				newContentType
			});
			while (!newContent.AnimateContentEntranceEnd())
			{
				yield return null;
			}
			Log.DeckTray.Print("NEW: {0} AnimateContentEntranceEnd - finished", new object[]
			{
				newContentType
			});
			newContent.SetModeActive(true);
			newContent.SetModeTrying(false);
		}
		base.TryEnableScrollbar();
		if (newContent != null)
		{
			while (!newContent.PostAnimateContentEntrance())
			{
				yield return null;
			}
		}
		if (oldContent != null)
		{
			while (!oldContent.PostAnimateContentExit())
			{
				yield return null;
			}
		}
		if (this.m_currentContent != DeckTray.DeckContentTypes.Decks)
		{
			this.m_cardsContent.TriggerCardCountUpdate();
		}
		this.m_settingNewMode = false;
		base.FireModeSwitchedEvent();
		this.UpdateDoneButtonText();
		if (this.m_contentToSet != DeckTray.DeckContentTypes.INVALID)
		{
			base.StartCoroutine(this.UpdateTrayMode());
			yield break;
		}
		this.m_updatingTrayMode = false;
		base.AllowInput(true);
		yield break;
	}

	// Token: 0x06007949 RID: 31049
	public abstract void UpdateDoneButtonText();

	// Token: 0x0600794A RID: 31050 RVA: 0x00279162 File Offset: 0x00277362
	protected override void HideUnseenDeckTrays()
	{
		DeckTray.DeckContentTypes currentContent = this.m_currentContent;
	}

	// Token: 0x0600794B RID: 31051 RVA: 0x0027916B File Offset: 0x0027736B
	protected override void OnCardTileOut(DeckTrayDeckTileVisual cardTile)
	{
		this.HideDeckBigCard(cardTile, false);
	}

	// Token: 0x04005E3F RID: 24127
	public UIBButton m_doneButton;

	// Token: 0x04005E40 RID: 24128
	public GameObject m_backArrow;

	// Token: 0x04005E41 RID: 24129
	public UberText m_myDecksLabel;

	// Token: 0x04005E42 RID: 24130
	public UberText m_countLabelText;

	// Token: 0x04005E43 RID: 24131
	public UberText m_countText;

	// Token: 0x04005E44 RID: 24132
	public TooltipZone m_deckHeaderTooltip;
}
