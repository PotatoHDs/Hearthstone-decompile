using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePhoneDeckTray : MonoBehaviour
{
	public DeckTrayCardListContent m_cardsContent;

	public UIBScrollable m_scrollbar;

	public TooltipZone m_deckHeaderTooltip;

	public DeckBigCard m_deckBigCard;

	public UberText m_countLabelText;

	public UberText m_countText;

	public GameObject m_headerLabel;

	protected bool m_isScrolling;

	public Dictionary<long, long> CardIdToCreatorMap { get; set; }

	protected virtual void Awake()
	{
		if (m_scrollbar != null)
		{
			m_scrollbar.Enable(enable: false);
			m_scrollbar.AddTouchScrollStartedListener(OnTouchScrollStarted);
			m_scrollbar.AddTouchScrollEndedListener(OnTouchScrollEnded);
		}
		m_cardsContent.SetInArena(inArena: true);
		m_cardsContent.RegisterCardTilePressListener(OnCardTilePress);
		m_cardsContent.RegisterCardTileOverListener(OnCardTileOver);
		m_cardsContent.RegisterCardTileOutListener(OnCardTileOut);
		m_cardsContent.RegisterCardTileReleaseListener(OnCardTileRelease);
		m_cardsContent.RegisterCardCountUpdated(OnCardCountUpdated);
	}

	public bool MouseIsOver()
	{
		if (!UniversalInputManager.Get().InputIsOver(base.gameObject) && !m_cardsContent.MouseIsOverDeckHelperButton())
		{
			return m_cardsContent.MouseIsOverDeckCardTile();
		}
		return true;
	}

	public virtual void AddCard(string cardID, Actor animateFromActor = null, Action onCompleteCallback = null)
	{
		m_cardsContent.UpdateCardList(cardID, updateHighlight: true, animateFromActor, onCompleteCallback);
	}

	public DeckTrayCardListContent GetCardsContent()
	{
		return m_cardsContent;
	}

	public TooltipZone GetTooltipZone()
	{
		return m_deckHeaderTooltip;
	}

	protected virtual void OnCardCountUpdated(int cardCount)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		if (cardCount > 0)
		{
			if (m_headerLabel != null)
			{
				m_headerLabel.SetActive(value: true);
			}
			if (cardCount < CollectionManager.Get().GetDeckSize())
			{
				text = GameStrings.Get("GLUE_DECK_TRAY_CARD_COUNT_LABEL");
				text2 = GameStrings.Format("GLUE_DECK_TRAY_COUNT", cardCount, CollectionManager.Get().GetDeckSize());
			}
		}
		if (m_countLabelText != null)
		{
			m_countLabelText.Text = text;
		}
		if (m_countText != null)
		{
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				StartCoroutine(DelayCardCountUpdate(text2));
			}
			else
			{
				m_countText.Text = text2;
			}
		}
	}

	protected IEnumerator DelayCardCountUpdate(string count)
	{
		yield return new WaitForSeconds(0.5f);
		if (!(m_countText == null))
		{
			m_countText.Text = count;
		}
	}

	protected void ShowDeckBigCard(DeckTrayDeckTileVisual cardTile, float delay = 0f)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (m_deckBigCard == null)
		{
			return;
		}
		EntityDef entityDef = actor.GetEntityDef();
		using DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(entityDef.GetCardId());
		long cardId = GameUtils.TranslateCardIdToDbId(entityDef.GetCardId());
		m_deckBigCard.SetCreatorName(GetCreatorNameFromChildCardDbId(cardId));
		m_deckBigCard.Show(entityDef, actor.GetPremium(), cardDef, actor.gameObject.transform.position, GhostCard.Type.NONE, delay);
		if (UniversalInputManager.Get().IsTouchMode())
		{
			cardTile.SetHighlight(highlight: true);
		}
	}

	protected void HideDeckBigCard(DeckTrayDeckTileVisual cardTile, bool force = false)
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

	private void OnTouchScrollStarted()
	{
		m_isScrolling = true;
		if (m_deckBigCard != null)
		{
			m_deckBigCard.ForceHide();
		}
	}

	private void OnTouchScrollEnded()
	{
		m_isScrolling = false;
	}

	protected virtual void OnCardTilePress(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			ShowDeckBigCard(cardTile, 0.2f);
		}
		else if (CollectionInputMgr.Get() != null && (!SceneMgr.Get().IsInDuelsMode() || PvPDungeonRunScene.IsEditingDeck()))
		{
			HideDeckBigCard(cardTile);
		}
	}

	private void OnCardTileOver(DeckTrayDeckTileVisual cardTile)
	{
		if (!UniversalInputManager.Get().IsTouchMode())
		{
			ShowDeckBigCard(cardTile);
		}
	}

	private void OnCardTileOut(DeckTrayDeckTileVisual cardTile)
	{
		HideDeckBigCard(cardTile);
	}

	private void OnCardTileRelease(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			HideDeckBigCard(cardTile);
		}
	}

	private string GetCreatorNameFromChildCardDbId(long cardId)
	{
		if (CardIdToCreatorMap == null)
		{
			return string.Empty;
		}
		if (!CardIdToCreatorMap.TryGetValue(cardId, out var value))
		{
			return string.Empty;
		}
		CardDbfRecord record = GameDbf.Card.GetRecord((int)value);
		if (record == null)
		{
			return string.Empty;
		}
		return record.Name;
	}
}
