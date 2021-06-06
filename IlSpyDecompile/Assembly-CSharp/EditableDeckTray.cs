using System.Collections;
using UnityEngine;

public abstract class EditableDeckTray : DeckTray
{
	public UIBButton m_doneButton;

	public GameObject m_backArrow;

	public UberText m_myDecksLabel;

	public UberText m_countLabelText;

	public UberText m_countText;

	public TooltipZone m_deckHeaderTooltip;

	protected override IEnumerator UpdateTrayMode()
	{
		DeckContentTypes oldContentType = m_currentContent;
		DeckContentTypes newContentType = m_contentToSet;
		if (m_settingNewMode || m_currentContent == m_contentToSet || m_contentToSet == DeckContentTypes.INVALID)
		{
			m_updatingTrayMode = false;
			yield break;
		}
		AllowInput(allowed: false);
		m_contentToSet = DeckContentTypes.INVALID;
		m_currentContent = DeckContentTypes.INVALID;
		m_settingNewMode = true;
		m_updatingTrayMode = true;
		DeckTrayContent oldContent = null;
		DeckTrayContent newContent = null;
		m_contents.TryGetValue(oldContentType, out oldContent);
		m_contents.TryGetValue(newContentType, out newContent);
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
		SaveScrollbarPosition(oldContentType);
		TryDisableScrollbar();
		if (oldContent != null)
		{
			oldContent.SetModeActive(active: false);
			while (!oldContent.AnimateContentExitStart())
			{
				yield return null;
			}
			Log.DeckTray.Print("OLD: {0} AnimateContentExitStart - finished", oldContentType);
			while (!oldContent.AnimateContentExitEnd())
			{
				yield return null;
			}
			Log.DeckTray.Print("OLD: {0} AnimateContentExitEnd - finished", oldContentType);
		}
		m_currentContent = newContentType;
		if (newContent != null)
		{
			newContent.SetModeTrying(trying: true);
			while (!newContent.AnimateContentEntranceStart())
			{
				yield return null;
			}
			Log.DeckTray.Print("NEW: {0} AnimateContentEntranceStart - finished", newContentType);
			while (!newContent.AnimateContentEntranceEnd())
			{
				yield return null;
			}
			Log.DeckTray.Print("NEW: {0} AnimateContentEntranceEnd - finished", newContentType);
			newContent.SetModeActive(active: true);
			newContent.SetModeTrying(trying: false);
		}
		TryEnableScrollbar();
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
		if (m_currentContent != 0)
		{
			m_cardsContent.TriggerCardCountUpdate();
		}
		m_settingNewMode = false;
		FireModeSwitchedEvent();
		UpdateDoneButtonText();
		if (m_contentToSet != DeckContentTypes.INVALID)
		{
			StartCoroutine(UpdateTrayMode());
			yield break;
		}
		m_updatingTrayMode = false;
		AllowInput(allowed: true);
	}

	public abstract void UpdateDoneButtonText();

	protected override void HideUnseenDeckTrays()
	{
		_ = m_currentContent;
	}

	protected override void OnCardTileOut(DeckTrayDeckTileVisual cardTile)
	{
		HideDeckBigCard(cardTile);
	}
}
