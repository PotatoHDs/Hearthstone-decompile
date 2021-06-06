using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002BB RID: 699
public class DraftPhoneDeckTray : BasePhoneDeckTray
{
	// Token: 0x060024C2 RID: 9410 RVA: 0x000B925C File Offset: 0x000B745C
	protected override void Awake()
	{
		base.Awake();
		DraftPhoneDeckTray.s_instance = this;
		DraftManager.Get().RegisterDraftDeckSetListener(new DraftManager.DraftDeckSet(this.OnDraftDeckInitialized));
		this.m_cardsContent.RegisterCardTileHeldListener(new DeckTrayCardListContent.CardTileHeld(this.OnCardTileHeld));
		this.m_cardsContent.RegisterCardTileReleaseListener(new DeckTrayCardListContent.CardTileRelease(this.OnCardTileRelease));
		this.m_cardsContent.RegisterCardCountUpdated(new DeckTrayCardListContent.CardCountChanged(this.OnCardCountUpdated));
		CollectionInputMgr collectionInputMgr = CollectionInputMgr.Get();
		if (collectionInputMgr != null)
		{
			collectionInputMgr.SetScrollbar(this.m_scrollbar);
		}
	}

	// Token: 0x060024C3 RID: 9411 RVA: 0x000B92EC File Offset: 0x000B74EC
	private void OnDestroy()
	{
		DraftManager draftManager = DraftManager.Get();
		if (draftManager != null)
		{
			draftManager.RemoveDraftDeckSetListener(new DraftManager.DraftDeckSet(this.OnDraftDeckInitialized));
		}
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager != null)
		{
			collectionManager.ClearEditedDeck();
		}
		DraftPhoneDeckTray.s_instance = null;
	}

	// Token: 0x060024C4 RID: 9412 RVA: 0x000B9320 File Offset: 0x000B7520
	public static DraftPhoneDeckTray Get()
	{
		return DraftPhoneDeckTray.s_instance;
	}

	// Token: 0x060024C5 RID: 9413 RVA: 0x000B9328 File Offset: 0x000B7528
	public void Initialize()
	{
		CollectionDeck draftDeck = DraftManager.Get().GetDraftDeck();
		if (draftDeck != null)
		{
			this.OnDraftDeckInitialized(draftDeck);
		}
	}

	// Token: 0x060024C6 RID: 9414 RVA: 0x000B934A File Offset: 0x000B754A
	private void OnDraftDeckInitialized(CollectionDeck draftDeck)
	{
		if (draftDeck == null)
		{
			Debug.LogError("Draft deck is null.");
			return;
		}
		CollectionManager.Get().SetEditedDeck(draftDeck, null);
		this.OnCardCountUpdated(draftDeck.GetTotalCardCount());
		this.m_cardsContent.UpdateCardList();
	}

	// Token: 0x060024C7 RID: 9415 RVA: 0x000B937D File Offset: 0x000B757D
	private IEnumerator ShowBigCard(DeckTrayDeckTileVisual cardTile, float delay)
	{
		base.ShowDeckBigCard(cardTile, delay);
		yield return new WaitForSeconds(delay);
		this.m_showDisablePremiumPrompt = false;
		yield break;
	}

	// Token: 0x060024C8 RID: 9416 RVA: 0x000B939A File Offset: 0x000B759A
	protected override void OnCardTilePress(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.m_showDisablePremiumPrompt = true;
			base.StartCoroutine(this.ShowBigCard(cardTile, 0.2f));
			return;
		}
		if (CollectionInputMgr.Get() != null)
		{
			base.HideDeckBigCard(cardTile, false);
		}
	}

	// Token: 0x060024C9 RID: 9417 RVA: 0x000B93D8 File Offset: 0x000B75D8
	private void OnCardTileHeld(DeckTrayDeckTileVisual cardTile)
	{
		if (CollectionInputMgr.Get() != null && cardTile.GetActor().GetPremium() != TAG_PREMIUM.NORMAL)
		{
			CollectionInputMgr.Get().GrabCard(cardTile, new CollectionInputMgr.OnCardDroppedCallback(this.OnDeckTileDropped), false);
		}
	}

	// Token: 0x060024CA RID: 9418 RVA: 0x000B9410 File Offset: 0x000B7610
	private void OnCardTileRelease(DeckTrayDeckTileVisual cardTile)
	{
		if (this.m_isScrolling)
		{
			return;
		}
		base.StopCoroutine("ShowBigCard");
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.DRAFT && cardTile.GetActor().GetPremium() != TAG_PREMIUM.NORMAL && this.m_showDisablePremiumPrompt)
		{
			DraftManager.Get().PromptToDisablePremium();
		}
	}

	// Token: 0x060024CB RID: 9419 RVA: 0x000B945E File Offset: 0x000B765E
	private void OnDeckTileDropped()
	{
		if (this.m_isScrolling)
		{
			return;
		}
		DraftManager.Get().PromptToDisablePremium();
	}

	// Token: 0x04001482 RID: 5250
	private static DraftPhoneDeckTray s_instance;

	// Token: 0x04001483 RID: 5251
	private bool m_showDisablePremiumPrompt = true;
}
