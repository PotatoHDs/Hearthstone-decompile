using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class AdventureDungeonCrawlDeckTray : BasePhoneDeckTray
{
	// Token: 0x06000204 RID: 516 RVA: 0x0000B7B5 File Offset: 0x000099B5
	protected override void Awake()
	{
		base.Awake();
		if (SceneMgr.Get().IsInDuelsMode() && this.m_headerLabel != null)
		{
			this.m_headerLabel.GetComponent<UberText>().Text = GameStrings.Get("GLUE_PVPDR_DECK_TRAY_HEADER");
		}
	}

	// Token: 0x06000205 RID: 517 RVA: 0x0000B7F1 File Offset: 0x000099F1
	private void OnDestroy()
	{
		this.ClearEditingDeck();
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000B7FC File Offset: 0x000099FC
	public void SetDungeonCrawlDeck(CollectionDeck deck, bool playGlowAnimation)
	{
		if (deck == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDeckTray.SetDungeonCrawlDeck() - deck passed in is null!", Array.Empty<object>());
			return;
		}
		this.m_deck = deck;
		base.gameObject.SetActive(true);
		this.TagDeckForEditing();
		this.OnCardCountUpdated(deck.GetTotalCardCount());
		this.m_cardsContent.UpdateCardList();
		if (playGlowAnimation && this.DeckTrayGlow != null)
		{
			this.DeckTrayGlow.SendEvent("Flash");
		}
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000B872 File Offset: 0x00009A72
	public void OffsetDeckBigCardByVector(Vector3 offset)
	{
		this.m_deckBigCard.OffsetByVector(offset);
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000B880 File Offset: 0x00009A80
	public override void AddCard(string cardId, Actor animateFromActor, Action onCompleteCallback)
	{
		if (this.m_deck == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDeckTray.AddCard() - no deck set!", Array.Empty<object>());
			return;
		}
		this.TagDeckForEditing();
		this.m_deck.AddCard(cardId, TAG_PREMIUM.NORMAL, false);
		base.AddCard(cardId, animateFromActor, onCompleteCallback);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000B8BD File Offset: 0x00009ABD
	private void TagDeckForEditing()
	{
		if (this.m_deck == null)
		{
			Log.Adventures.PrintError("AdventureDungeonCrawlDeckTray.TagForEdit() - no deck set!", Array.Empty<object>());
			return;
		}
		CollectionManager.Get().SetEditedDeck(this.m_deck, null);
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000B8F0 File Offset: 0x00009AF0
	private void ClearEditingDeck()
	{
		if (CollectionManager.Get() != null)
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			if (editedDeck != null && editedDeck == this.m_deck)
			{
				CollectionManager.Get().ClearEditedDeck();
			}
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000B928 File Offset: 0x00009B28
	protected override void OnCardCountUpdated(int cardCount)
	{
		if (cardCount <= 0)
		{
			return;
		}
		if (this.m_countLabelText != null)
		{
			this.m_countLabelText.Text = GameStrings.Get("GLUE_DECK_TRAY_CARD_COUNT_LABEL");
		}
		if (this.m_countText != null)
		{
			string text = string.Format("{0}", cardCount);
			if (UniversalInputManager.UsePhoneUI)
			{
				base.StartCoroutine(base.DelayCardCountUpdate(text));
				return;
			}
			this.m_countText.Text = text;
		}
	}

	// Token: 0x04000174 RID: 372
	public PlayMakerFSM DeckTrayGlow;

	// Token: 0x04000175 RID: 373
	private CollectionDeck m_deck;
}
