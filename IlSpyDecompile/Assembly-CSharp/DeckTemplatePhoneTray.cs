using System.Collections;
using UnityEngine;

public class DeckTemplatePhoneTray : MonoBehaviour
{
	public DeckTrayCardListContent m_cardsContent;

	public UIBScrollable m_scrollbar;

	public TooltipZone m_deckHeaderTooltip;

	public DeckBigCard m_deckBigCard;

	public UberText m_countLabelText;

	public UberText m_countText;

	public GameObject m_headerLabel;

	public PlayMakerFSM m_deckTemplateChosenGlow;

	private static DeckTemplatePhoneTray s_instance;

	private void Awake()
	{
		s_instance = this;
		if (m_scrollbar != null)
		{
			m_scrollbar.Enable(enable: false);
			m_scrollbar.AddTouchScrollStartedListener(OnTouchScrollStarted);
		}
		if (m_deckBigCard != null)
		{
			m_deckBigCard.SetHideBigHeroPower(hide: true);
		}
		m_cardsContent.RegisterCardTilePressListener(OnCardTilePress);
		m_cardsContent.RegisterCardTileOverListener(OnCardTileOver);
		m_cardsContent.RegisterCardTileOutListener(OnCardTileOut);
		m_cardsContent.RegisterCardTileReleaseListener(OnCardTileRelease);
		m_cardsContent.ShowFakeDeck(show: true);
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static DeckTemplatePhoneTray Get()
	{
		return s_instance;
	}

	public bool MouseIsOver()
	{
		return UniversalInputManager.Get().InputIsOver(base.gameObject);
	}

	public DeckTrayCardListContent GetCardsContent()
	{
		return m_cardsContent;
	}

	public TooltipZone GetTooltipZone()
	{
		return m_deckHeaderTooltip;
	}

	private void OnCardCountUpdated(int cardCount)
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
		m_countLabelText.Text = text;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			StartCoroutine(DelayCardCountUpdate(text2));
		}
		else
		{
			m_countText.Text = text2;
		}
	}

	private IEnumerator DelayCardCountUpdate(string count)
	{
		yield return new WaitForSeconds(0.5f);
		m_countText.Text = count;
	}

	private void ShowDeckBigCard(DeckTrayDeckTileVisual cardTile, float delay = 0f)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (m_deckBigCard == null)
		{
			return;
		}
		EntityDef entityDef = actor.GetEntityDef();
		using DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(entityDef.GetCardId(), new CardPortraitQuality(3, actor.GetPremium()));
		GhostCard.Type ghostTypeFromSlot = GhostCard.GetGhostTypeFromSlot(m_cardsContent.GetEditingDeck(), cardTile.GetSlot());
		m_deckBigCard.Show(entityDef, actor.GetPremium(), cardDef, actor.gameObject.transform.position, ghostTypeFromSlot, delay);
		if (UniversalInputManager.Get().IsTouchMode())
		{
			cardTile.SetHighlight(highlight: true);
		}
	}

	private void HideDeckBigCard(DeckTrayDeckTileVisual cardTile, bool force = false)
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
		if (m_deckBigCard != null)
		{
			m_deckBigCard.ForceHide();
		}
	}

	private void OnCardTilePress(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			ShowDeckBigCard(cardTile, 0.2f);
		}
		else if (CollectionInputMgr.Get() != null)
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

	public void FlashDeckTemplateHighlight()
	{
		if (m_deckTemplateChosenGlow != null)
		{
			m_deckTemplateChosenGlow.SendEvent("Flash");
		}
	}
}
