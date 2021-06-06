using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000861 RID: 2145
public abstract class BasePhoneDeckTray : MonoBehaviour
{
	// Token: 0x170006DC RID: 1756
	// (get) Token: 0x060073C4 RID: 29636 RVA: 0x00252713 File Offset: 0x00250913
	// (set) Token: 0x060073C5 RID: 29637 RVA: 0x0025271B File Offset: 0x0025091B
	public Dictionary<long, long> CardIdToCreatorMap { get; set; }

	// Token: 0x060073C6 RID: 29638 RVA: 0x00252724 File Offset: 0x00250924
	protected virtual void Awake()
	{
		if (this.m_scrollbar != null)
		{
			this.m_scrollbar.Enable(false);
			this.m_scrollbar.AddTouchScrollStartedListener(new UIBScrollable.OnTouchScrollStarted(this.OnTouchScrollStarted));
			this.m_scrollbar.AddTouchScrollEndedListener(new UIBScrollable.OnTouchScrollEnded(this.OnTouchScrollEnded));
		}
		this.m_cardsContent.SetInArena(true);
		this.m_cardsContent.RegisterCardTilePressListener(new DeckTrayCardListContent.CardTilePress(this.OnCardTilePress));
		this.m_cardsContent.RegisterCardTileOverListener(new DeckTrayCardListContent.CardTileOver(this.OnCardTileOver));
		this.m_cardsContent.RegisterCardTileOutListener(new DeckTrayCardListContent.CardTileOut(this.OnCardTileOut));
		this.m_cardsContent.RegisterCardTileReleaseListener(new DeckTrayCardListContent.CardTileRelease(this.OnCardTileRelease));
		this.m_cardsContent.RegisterCardCountUpdated(new DeckTrayCardListContent.CardCountChanged(this.OnCardCountUpdated));
	}

	// Token: 0x060073C7 RID: 29639 RVA: 0x002527FA File Offset: 0x002509FA
	public bool MouseIsOver()
	{
		return UniversalInputManager.Get().InputIsOver(base.gameObject) || this.m_cardsContent.MouseIsOverDeckHelperButton() || this.m_cardsContent.MouseIsOverDeckCardTile();
	}

	// Token: 0x060073C8 RID: 29640 RVA: 0x00252828 File Offset: 0x00250A28
	public virtual void AddCard(string cardID, Actor animateFromActor = null, Action onCompleteCallback = null)
	{
		this.m_cardsContent.UpdateCardList(cardID, true, animateFromActor, onCompleteCallback);
	}

	// Token: 0x060073C9 RID: 29641 RVA: 0x00252839 File Offset: 0x00250A39
	public DeckTrayCardListContent GetCardsContent()
	{
		return this.m_cardsContent;
	}

	// Token: 0x060073CA RID: 29642 RVA: 0x00252841 File Offset: 0x00250A41
	public TooltipZone GetTooltipZone()
	{
		return this.m_deckHeaderTooltip;
	}

	// Token: 0x060073CB RID: 29643 RVA: 0x0025284C File Offset: 0x00250A4C
	protected virtual void OnCardCountUpdated(int cardCount)
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
		if (this.m_countLabelText != null)
		{
			this.m_countLabelText.Text = text;
		}
		if (this.m_countText != null)
		{
			if (UniversalInputManager.UsePhoneUI)
			{
				base.StartCoroutine(this.DelayCardCountUpdate(text2));
				return;
			}
			this.m_countText.Text = text2;
		}
	}

	// Token: 0x060073CC RID: 29644 RVA: 0x00252916 File Offset: 0x00250B16
	protected IEnumerator DelayCardCountUpdate(string count)
	{
		yield return new WaitForSeconds(0.5f);
		if (this.m_countText == null)
		{
			yield break;
		}
		this.m_countText.Text = count;
		yield break;
	}

	// Token: 0x060073CD RID: 29645 RVA: 0x0025292C File Offset: 0x00250B2C
	protected void ShowDeckBigCard(DeckTrayDeckTileVisual cardTile, float delay = 0f)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (this.m_deckBigCard == null)
		{
			return;
		}
		EntityDef entityDef = actor.GetEntityDef();
		using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(entityDef.GetCardId(), null))
		{
			long cardId = (long)GameUtils.TranslateCardIdToDbId(entityDef.GetCardId(), false);
			this.m_deckBigCard.SetCreatorName(this.GetCreatorNameFromChildCardDbId(cardId));
			this.m_deckBigCard.Show(entityDef, actor.GetPremium(), cardDef, actor.gameObject.transform.position, GhostCard.Type.NONE, delay);
			if (UniversalInputManager.Get().IsTouchMode())
			{
				cardTile.SetHighlight(true);
			}
		}
	}

	// Token: 0x060073CE RID: 29646 RVA: 0x002529DC File Offset: 0x00250BDC
	protected void HideDeckBigCard(DeckTrayDeckTileVisual cardTile, bool force = false)
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

	// Token: 0x060073CF RID: 29647 RVA: 0x00252A38 File Offset: 0x00250C38
	private void OnTouchScrollStarted()
	{
		this.m_isScrolling = true;
		if (this.m_deckBigCard != null)
		{
			this.m_deckBigCard.ForceHide();
		}
	}

	// Token: 0x060073D0 RID: 29648 RVA: 0x00252A5A File Offset: 0x00250C5A
	private void OnTouchScrollEnded()
	{
		this.m_isScrolling = false;
	}

	// Token: 0x060073D1 RID: 29649 RVA: 0x00252A64 File Offset: 0x00250C64
	protected virtual void OnCardTilePress(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.ShowDeckBigCard(cardTile, 0.2f);
			return;
		}
		if (CollectionInputMgr.Get() != null)
		{
			if (SceneMgr.Get().IsInDuelsMode() && !PvPDungeonRunScene.IsEditingDeck())
			{
				return;
			}
			this.HideDeckBigCard(cardTile, false);
		}
	}

	// Token: 0x060073D2 RID: 29650 RVA: 0x00252AB3 File Offset: 0x00250CB3
	private void OnCardTileOver(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		this.ShowDeckBigCard(cardTile, 0f);
	}

	// Token: 0x060073D3 RID: 29651 RVA: 0x00252ACE File Offset: 0x00250CCE
	private void OnCardTileOut(DeckTrayDeckTileVisual cardTile)
	{
		this.HideDeckBigCard(cardTile, false);
	}

	// Token: 0x060073D4 RID: 29652 RVA: 0x00252AD8 File Offset: 0x00250CD8
	private void OnCardTileRelease(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.HideDeckBigCard(cardTile, false);
		}
	}

	// Token: 0x060073D5 RID: 29653 RVA: 0x00252AF0 File Offset: 0x00250CF0
	private string GetCreatorNameFromChildCardDbId(long cardId)
	{
		if (this.CardIdToCreatorMap == null)
		{
			return string.Empty;
		}
		long num;
		if (!this.CardIdToCreatorMap.TryGetValue(cardId, out num))
		{
			return string.Empty;
		}
		CardDbfRecord record = GameDbf.Card.GetRecord((int)num);
		if (record == null)
		{
			return string.Empty;
		}
		return record.Name;
	}

	// Token: 0x04005BFB RID: 23547
	public DeckTrayCardListContent m_cardsContent;

	// Token: 0x04005BFC RID: 23548
	public UIBScrollable m_scrollbar;

	// Token: 0x04005BFD RID: 23549
	public TooltipZone m_deckHeaderTooltip;

	// Token: 0x04005BFE RID: 23550
	public DeckBigCard m_deckBigCard;

	// Token: 0x04005BFF RID: 23551
	public UberText m_countLabelText;

	// Token: 0x04005C00 RID: 23552
	public UberText m_countText;

	// Token: 0x04005C01 RID: 23553
	public GameObject m_headerLabel;

	// Token: 0x04005C03 RID: 23555
	protected bool m_isScrolling;
}
