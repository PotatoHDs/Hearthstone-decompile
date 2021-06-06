using System;
using UnityEngine;

public class AdventureDungeonCrawlDeckTray : BasePhoneDeckTray
{
	public PlayMakerFSM DeckTrayGlow;

	private CollectionDeck m_deck;

	protected override void Awake()
	{
		base.Awake();
		if (SceneMgr.Get().IsInDuelsMode() && m_headerLabel != null)
		{
			m_headerLabel.GetComponent<UberText>().Text = GameStrings.Get("GLUE_PVPDR_DECK_TRAY_HEADER");
		}
	}

	private void OnDestroy()
	{
		ClearEditingDeck();
	}

	public void SetDungeonCrawlDeck(CollectionDeck deck, bool playGlowAnimation)
	{
		if (deck == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDeckTray.SetDungeonCrawlDeck() - deck passed in is null!");
			return;
		}
		m_deck = deck;
		base.gameObject.SetActive(value: true);
		TagDeckForEditing();
		OnCardCountUpdated(deck.GetTotalCardCount());
		m_cardsContent.UpdateCardList();
		if (playGlowAnimation && DeckTrayGlow != null)
		{
			DeckTrayGlow.SendEvent("Flash");
		}
	}

	public void OffsetDeckBigCardByVector(Vector3 offset)
	{
		m_deckBigCard.OffsetByVector(offset);
	}

	public override void AddCard(string cardId, Actor animateFromActor, Action onCompleteCallback)
	{
		if (m_deck == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDeckTray.AddCard() - no deck set!");
			return;
		}
		TagDeckForEditing();
		m_deck.AddCard(cardId, TAG_PREMIUM.NORMAL);
		base.AddCard(cardId, animateFromActor, onCompleteCallback);
	}

	private void TagDeckForEditing()
	{
		if (m_deck == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDeckTray.TagForEdit() - no deck set!");
		}
		else
		{
			CollectionManager.Get().SetEditedDeck(m_deck);
		}
	}

	private void ClearEditingDeck()
	{
		if (CollectionManager.Get() != null)
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			if (editedDeck != null && editedDeck == m_deck)
			{
				CollectionManager.Get().ClearEditedDeck();
			}
		}
	}

	protected override void OnCardCountUpdated(int cardCount)
	{
		if (cardCount <= 0)
		{
			return;
		}
		if (m_countLabelText != null)
		{
			m_countLabelText.Text = GameStrings.Get("GLUE_DECK_TRAY_CARD_COUNT_LABEL");
		}
		if (m_countText != null)
		{
			string text = $"{cardCount}";
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				StartCoroutine(DelayCardCountUpdate(text));
			}
			else
			{
				m_countText.Text = text;
			}
		}
	}
}
