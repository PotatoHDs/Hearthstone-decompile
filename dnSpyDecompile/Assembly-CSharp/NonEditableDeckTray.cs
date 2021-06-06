using System;
using System.Collections;
using UnityEngine;

// Token: 0x020008EE RID: 2286
public class NonEditableDeckTray : DeckTray
{
	// Token: 0x06007EAD RID: 32429 RVA: 0x00290DB1 File Offset: 0x0028EFB1
	public override bool OnBackOutOfDeckContents()
	{
		if (base.GetCurrentContentType() != DeckTray.DeckContentTypes.INVALID && !base.GetCurrentContent().IsModeActive())
		{
			return false;
		}
		if (!base.IsShowingDeckContents())
		{
			return false;
		}
		Log.DeckTray.Print("NonEditableDeckTray: Backing out of deck contents", Array.Empty<object>());
		this.EnterDeckListMode();
		return true;
	}

	// Token: 0x06007EAE RID: 32430 RVA: 0x002707DF File Offset: 0x0026E9DF
	private void EnterDeckListMode()
	{
		base.SetTrayMode(DeckTray.DeckContentTypes.Decks);
	}

	// Token: 0x06007EAF RID: 32431 RVA: 0x00290DF0 File Offset: 0x0028EFF0
	protected override IEnumerator UpdateTrayMode()
	{
		DeckTray.DeckContentTypes oldContentType = this.m_currentContent;
		DeckTray.DeckContentTypes newContentType = this.m_contentToSet;
		if (this.m_settingNewMode || this.m_currentContent == this.m_contentToSet || this.m_contentToSet == DeckTray.DeckContentTypes.INVALID)
		{
			this.m_updatingTrayMode = false;
			yield break;
		}
		base.AllowInput(false);
		this.m_contentToSet = DeckTray.DeckContentTypes.INVALID;
		this.m_currentContent = DeckTray.DeckContentTypes.INVALID;
		this.m_settingNewMode = true;
		this.m_updatingTrayMode = true;
		DeckTrayContent oldContent = null;
		DeckTrayContent newContent = null;
		this.m_contents.TryGetValue(oldContentType, out oldContent);
		this.m_contents.TryGetValue(newContentType, out newContent);
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
		base.SaveScrollbarPosition(oldContentType);
		base.TryDisableScrollbar();
		if (oldContent != null)
		{
			oldContent.SetModeActive(false);
			while (!oldContent.AnimateContentExitStart())
			{
				yield return null;
			}
			Log.DeckTray.Print("OLD: {0} AnimateContentExitStart - finished", new object[]
			{
				oldContentType
			});
			while (!oldContent.AnimateContentExitEnd())
			{
				yield return null;
			}
			Log.DeckTray.Print("OLD: {0} AnimateContentExitEnd - finished", new object[]
			{
				oldContentType
			});
		}
		this.m_currentContent = newContentType;
		if (newContent != null)
		{
			newContent.SetModeTrying(true);
			while (!newContent.AnimateContentEntranceStart())
			{
				yield return null;
			}
			Log.DeckTray.Print("NEW: {0} AnimateContentEntranceStart - finished", new object[]
			{
				newContentType
			});
			while (!newContent.AnimateContentEntranceEnd())
			{
				yield return null;
			}
			Log.DeckTray.Print("NEW: {0} AnimateContentEntranceEnd - finished", new object[]
			{
				newContentType
			});
			newContent.SetModeActive(true);
			newContent.SetModeTrying(false);
		}
		base.TryEnableScrollbar();
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
		if (this.m_currentContent != DeckTray.DeckContentTypes.Decks)
		{
			this.m_cardsContent.TriggerCardCountUpdate();
		}
		this.m_settingNewMode = false;
		base.FireModeSwitchedEvent();
		if (this.m_contentToSet != DeckTray.DeckContentTypes.INVALID)
		{
			base.StartCoroutine(this.UpdateTrayMode());
			yield break;
		}
		this.m_updatingTrayMode = false;
		base.AllowInput(true);
		yield break;
	}

	// Token: 0x06007EB0 RID: 32432 RVA: 0x00279162 File Offset: 0x00277362
	protected override void HideUnseenDeckTrays()
	{
		DeckTray.DeckContentTypes currentContent = this.m_currentContent;
	}

	// Token: 0x06007EB1 RID: 32433 RVA: 0x00290E00 File Offset: 0x0028F000
	protected override void ShowDeckBigCard(DeckTrayDeckTileVisual cardTile, float delay = 0f)
	{
		CollectionDeckTileActor actor = cardTile.GetActor();
		if (this.m_deckBigCard == null)
		{
			return;
		}
		EntityDef entityDef = actor.GetEntityDef();
		using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(entityDef.GetCardId(), null))
		{
			this.m_deckBigCard.Show(entityDef, actor.GetPremium(), cardDef, actor.gameObject.transform.position, GhostCard.Type.NONE, delay);
			if (UniversalInputManager.Get().IsTouchMode())
			{
				cardTile.SetHighlight(true);
			}
		}
	}

	// Token: 0x06007EB2 RID: 32434 RVA: 0x00290E90 File Offset: 0x0028F090
	protected override void HideDeckBigCard(DeckTrayDeckTileVisual cardTile, bool force = false)
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

	// Token: 0x06007EB3 RID: 32435 RVA: 0x00290EEC File Offset: 0x0028F0EC
	protected override void OnCardTilePress(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.ShowDeckBigCard(cardTile, 0.2f);
		}
	}

	// Token: 0x06007EB4 RID: 32436 RVA: 0x00290F06 File Offset: 0x0028F106
	protected override void OnCardTileOver(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			return;
		}
		this.ShowDeckBigCard(cardTile, 0f);
	}

	// Token: 0x06007EB5 RID: 32437 RVA: 0x0027916B File Offset: 0x0027736B
	protected override void OnCardTileOut(DeckTrayDeckTileVisual cardTile)
	{
		this.HideDeckBigCard(cardTile, false);
	}

	// Token: 0x06007EB6 RID: 32438 RVA: 0x00290F21 File Offset: 0x0028F121
	protected override void OnCardTileRelease(DeckTrayDeckTileVisual cardTile)
	{
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.HideDeckBigCard(cardTile, false);
		}
	}

	// Token: 0x04006645 RID: 26181
	public GameObject m_headerLabel;
}
