using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002B5 RID: 693
public class DeckTemplatePhoneTray : MonoBehaviour
{
	// Token: 0x060023EE RID: 9198 RVA: 0x000B37B0 File Offset: 0x000B19B0
	private void Awake()
	{
		DeckTemplatePhoneTray.s_instance = this;
		if (this.m_scrollbar != null)
		{
			this.m_scrollbar.Enable(false);
			this.m_scrollbar.AddTouchScrollStartedListener(new UIBScrollable.OnTouchScrollStarted(this.OnTouchScrollStarted));
		}
		if (this.m_deckBigCard != null)
		{
			this.m_deckBigCard.SetHideBigHeroPower(true);
		}
		this.m_cardsContent.RegisterCardTilePressListener(new DeckTrayCardListContent.CardTilePress(this.OnCardTilePress));
		this.m_cardsContent.RegisterCardTileOverListener(new DeckTrayCardListContent.CardTileOver(this.OnCardTileOver));
		this.m_cardsContent.RegisterCardTileOutListener(new DeckTrayCardListContent.CardTileOut(this.OnCardTileOut));
		this.m_cardsContent.RegisterCardTileReleaseListener(new DeckTrayCardListContent.CardTileRelease(this.OnCardTileRelease));
		this.m_cardsContent.ShowFakeDeck(true);
	}

	// Token: 0x060023EF RID: 9199 RVA: 0x000B3876 File Offset: 0x000B1A76
	private void OnDestroy()
	{
		DeckTemplatePhoneTray.s_instance = null;
	}

	// Token: 0x060023F0 RID: 9200 RVA: 0x000B387E File Offset: 0x000B1A7E
	public static DeckTemplatePhoneTray Get()
	{
		return DeckTemplatePhoneTray.s_instance;
	}

	// Token: 0x060023F1 RID: 9201 RVA: 0x000B3885 File Offset: 0x000B1A85
	public bool MouseIsOver()
	{
		return UniversalInputManager.Get().InputIsOver(base.gameObject);
	}

	// Token: 0x060023F2 RID: 9202 RVA: 0x000B3897 File Offset: 0x000B1A97
	public DeckTrayCardListContent GetCardsContent()
	{
		return this.m_cardsContent;
	}

	// Token: 0x060023F3 RID: 9203 RVA: 0x000B389F File Offset: 0x000B1A9F
	public TooltipZone GetTooltipZone()
	{
		return this.m_deckHeaderTooltip;
	}

	// Token: 0x060023F4 RID: 9204 RVA: 0x000B38A8 File Offset: 0x000B1AA8
	private void OnCardCountUpdated(int cardCount)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		if (cardCount > 0)
		{
			if (this.m_headerLabel != null)
			{
				this.m_headerLabel.SetActive(true);
			}
			if (cardCount < CollectionManager.Get().GetDeckSize())
			{
				text = GameStrings.Get("GLUE_DECK_TRAY_CARD_COUNT_LABEL");
				text2 = GameStrings.Format("GLUE_DECK_TRAY_COUNT", new object[]
				{
					cardCount,
					CollectionManager.Get().GetDeckSize()
				});
			}
		}
		this.m_countLabelText.Text = text;
		if (UniversalInputManager.UsePhoneUI)
		{
			base.StartCoroutine(this.DelayCardCountUpdate(text2));
			return;
		}
		this.m_countText.Text = text2;
	}

	// Token: 0x060023F5 RID: 9205 RVA: 0x000B3956 File Offset: 0x000B1B56
	private IEnumerator DelayCardCountUpdate(string count)
	{
		yield return new WaitForSeconds(0.5f);
		this.m_countText.Text = count;
		yield break;
	}

	// Token: 0x060023F6 RID: 9206 RVA: 0x000B396C File Offset: 0x000B1B6C
	private void ShowDeckBigCard(DeckTrayDeckTileVisual cardTile, float delay = 0f)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (this.m_deckBigCard == null)
		{
			return;
		}
		EntityDef entityDef = actor.GetEntityDef();
		using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(entityDef.GetCardId(), new CardPortraitQuality(3, actor.GetPremium())))
		{
			GhostCard.Type ghostTypeFromSlot = GhostCard.GetGhostTypeFromSlot(this.m_cardsContent.GetEditingDeck(), cardTile.GetSlot());
			this.m_deckBigCard.Show(entityDef, actor.GetPremium(), cardDef, actor.gameObject.transform.position, ghostTypeFromSlot, delay);
			if (UniversalInputManager.Get().IsTouchMode())
			{
				cardTile.SetHighlight(true);
			}
		}
	}

	// Token: 0x060023F7 RID: 9207 RVA: 0x000B3A20 File Offset: 0x000B1C20
	private void HideDeckBigCard(DeckTrayDeckTileVisual cardTile, bool force = false)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (this.m_deckBigCard != null)
		{
			if (force)
			{
				this.m_deckBigCard.ForceHide();
			}
			else
			{
				this.m_deckBigCard.Hide(actor.GetEntityDef(), actor.GetPremium());
			}
			if (UniversalInputManager.Get().IsTouchMode())
			{
				cardTile.SetHighlight(false);
			}
		}
	}

	// Token: 0x060023F8 RID: 9208 RVA: 0x000B3A7C File Offset: 0x000B1C7C
	private void OnTouchScrollStarted()
	{
		if (this.m_deckBigCard != null)
		{
			this.m_deckBigCard.ForceHide();
		}
	}

	// Token: 0x060023F9 RID: 9209 RVA: 0x000B3A97 File Offset: 0x000B1C97
	private void OnCardTilePress(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.ShowDeckBigCard(cardTile, 0.2f);
			return;
		}
		if (CollectionInputMgr.Get() != null)
		{
			this.HideDeckBigCard(cardTile, false);
		}
	}

	// Token: 0x060023FA RID: 9210 RVA: 0x000B3AC7 File Offset: 0x000B1CC7
	private void OnCardTileOver(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		this.ShowDeckBigCard(cardTile, 0f);
	}

	// Token: 0x060023FB RID: 9211 RVA: 0x000B3AE2 File Offset: 0x000B1CE2
	private void OnCardTileOut(DeckTrayDeckTileVisual cardTile)
	{
		this.HideDeckBigCard(cardTile, false);
	}

	// Token: 0x060023FC RID: 9212 RVA: 0x000B3AEC File Offset: 0x000B1CEC
	private void OnCardTileRelease(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.HideDeckBigCard(cardTile, false);
		}
	}

	// Token: 0x060023FD RID: 9213 RVA: 0x000B3B02 File Offset: 0x000B1D02
	public void FlashDeckTemplateHighlight()
	{
		if (this.m_deckTemplateChosenGlow != null)
		{
			this.m_deckTemplateChosenGlow.SendEvent("Flash");
		}
	}

	// Token: 0x04001411 RID: 5137
	public DeckTrayCardListContent m_cardsContent;

	// Token: 0x04001412 RID: 5138
	public UIBScrollable m_scrollbar;

	// Token: 0x04001413 RID: 5139
	public TooltipZone m_deckHeaderTooltip;

	// Token: 0x04001414 RID: 5140
	public DeckBigCard m_deckBigCard;

	// Token: 0x04001415 RID: 5141
	public UberText m_countLabelText;

	// Token: 0x04001416 RID: 5142
	public UberText m_countText;

	// Token: 0x04001417 RID: 5143
	public GameObject m_headerLabel;

	// Token: 0x04001418 RID: 5144
	public PlayMakerFSM m_deckTemplateChosenGlow;

	// Token: 0x04001419 RID: 5145
	private static DeckTemplatePhoneTray s_instance;
}
