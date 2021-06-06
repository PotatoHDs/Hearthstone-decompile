using System.Collections;
using UnityEngine;

public class DraftPhoneDeckTray : BasePhoneDeckTray
{
	private static DraftPhoneDeckTray s_instance;

	private bool m_showDisablePremiumPrompt = true;

	protected override void Awake()
	{
		base.Awake();
		s_instance = this;
		DraftManager.Get().RegisterDraftDeckSetListener(OnDraftDeckInitialized);
		m_cardsContent.RegisterCardTileHeldListener(OnCardTileHeld);
		m_cardsContent.RegisterCardTileReleaseListener(OnCardTileRelease);
		m_cardsContent.RegisterCardCountUpdated(OnCardCountUpdated);
		CollectionInputMgr collectionInputMgr = CollectionInputMgr.Get();
		if (collectionInputMgr != null)
		{
			collectionInputMgr.SetScrollbar(m_scrollbar);
		}
	}

	private void OnDestroy()
	{
		DraftManager.Get()?.RemoveDraftDeckSetListener(OnDraftDeckInitialized);
		CollectionManager.Get()?.ClearEditedDeck();
		s_instance = null;
	}

	public static DraftPhoneDeckTray Get()
	{
		return s_instance;
	}

	public void Initialize()
	{
		CollectionDeck draftDeck = DraftManager.Get().GetDraftDeck();
		if (draftDeck != null)
		{
			OnDraftDeckInitialized(draftDeck);
		}
	}

	private void OnDraftDeckInitialized(CollectionDeck draftDeck)
	{
		if (draftDeck == null)
		{
			Debug.LogError("Draft deck is null.");
			return;
		}
		CollectionManager.Get().SetEditedDeck(draftDeck);
		OnCardCountUpdated(draftDeck.GetTotalCardCount());
		m_cardsContent.UpdateCardList();
	}

	private IEnumerator ShowBigCard(DeckTrayDeckTileVisual cardTile, float delay)
	{
		ShowDeckBigCard(cardTile, delay);
		yield return new WaitForSeconds(delay);
		m_showDisablePremiumPrompt = false;
	}

	protected override void OnCardTilePress(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			m_showDisablePremiumPrompt = true;
			StartCoroutine(ShowBigCard(cardTile, 0.2f));
		}
		else if (CollectionInputMgr.Get() != null)
		{
			HideDeckBigCard(cardTile);
		}
	}

	private void OnCardTileHeld(DeckTrayDeckTileVisual cardTile)
	{
		if (CollectionInputMgr.Get() != null && cardTile.GetActor().GetPremium() != 0)
		{
			CollectionInputMgr.Get().GrabCard(cardTile, OnDeckTileDropped, removeCard: false);
		}
	}

	private void OnCardTileRelease(DeckTrayDeckTileVisual cardTile)
	{
		if (!m_isScrolling)
		{
			StopCoroutine("ShowBigCard");
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.DRAFT && cardTile.GetActor().GetPremium() != 0 && m_showDisablePremiumPrompt)
			{
				DraftManager.Get().PromptToDisablePremium();
			}
		}
	}

	private void OnDeckTileDropped()
	{
		if (!m_isScrolling)
		{
			DraftManager.Get().PromptToDisablePremium();
		}
	}
}
