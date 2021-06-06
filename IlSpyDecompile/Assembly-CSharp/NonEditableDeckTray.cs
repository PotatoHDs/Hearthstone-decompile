using System.Collections;
using UnityEngine;

public class NonEditableDeckTray : DeckTray
{
	public GameObject m_headerLabel;

	public override bool OnBackOutOfDeckContents()
	{
		if (GetCurrentContentType() != DeckContentTypes.INVALID && !GetCurrentContent().IsModeActive())
		{
			return false;
		}
		if (!IsShowingDeckContents())
		{
			return false;
		}
		Log.DeckTray.Print("NonEditableDeckTray: Backing out of deck contents");
		EnterDeckListMode();
		return true;
	}

	private void EnterDeckListMode()
	{
		SetTrayMode(DeckContentTypes.Decks);
	}

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
		if (m_contentToSet != DeckContentTypes.INVALID)
		{
			StartCoroutine(UpdateTrayMode());
			yield break;
		}
		m_updatingTrayMode = false;
		AllowInput(allowed: true);
	}

	protected override void HideUnseenDeckTrays()
	{
		_ = m_currentContent;
	}

	protected override void ShowDeckBigCard(DeckTrayDeckTileVisual cardTile, float delay = 0f)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (m_deckBigCard == null)
		{
			return;
		}
		EntityDef entityDef = actor.GetEntityDef();
		using DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(entityDef.GetCardId());
		m_deckBigCard.Show(entityDef, actor.GetPremium(), cardDef, actor.gameObject.transform.position, GhostCard.Type.NONE, delay);
		if (UniversalInputManager.Get().IsTouchMode())
		{
			cardTile.SetHighlight(highlight: true);
		}
	}

	protected override void HideDeckBigCard(DeckTrayDeckTileVisual cardTile, bool force = false)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (m_deckBigCard != null)
		{
			if (force)
			{
				m_deckBigCard.ForceHide();
			}
			else
			{
				m_deckBigCard.Hide(actor.GetEntityDef(), actor.GetPremium());
			}
			if (UniversalInputManager.Get().IsTouchMode())
			{
				cardTile.SetHighlight(highlight: false);
			}
		}
	}

	protected override void OnCardTilePress(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			ShowDeckBigCard(cardTile, 0.2f);
		}
	}

	protected override void OnCardTileOver(DeckTrayDeckTileVisual cardTile)
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			ShowDeckBigCard(cardTile);
		}
	}

	protected override void OnCardTileOut(DeckTrayDeckTileVisual cardTile)
	{
		HideDeckBigCard(cardTile);
	}

	protected override void OnCardTileRelease(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			HideDeckBigCard(cardTile);
		}
	}
}
