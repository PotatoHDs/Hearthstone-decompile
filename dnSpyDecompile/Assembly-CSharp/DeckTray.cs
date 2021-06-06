using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200088C RID: 2188
public abstract class DeckTray : MonoBehaviour
{
	// Token: 0x0600777D RID: 30589 RVA: 0x002707C8 File Offset: 0x0026E9C8
	protected virtual void Start()
	{
		SoundManager.Get().Load("panel_slide_off_deck_creation_screen.prefab:b0d25fc984ec05d4fbea7480b611e5ad");
	}

	// Token: 0x0600777E RID: 30590 RVA: 0x002707DF File Offset: 0x0026E9DF
	public void Initialize()
	{
		this.SetTrayMode(DeckTray.DeckContentTypes.Decks);
	}

	// Token: 0x0600777F RID: 30591 RVA: 0x002707E8 File Offset: 0x0026E9E8
	public DeckTrayCardListContent GetCardsContent()
	{
		return this.m_cardsContent;
	}

	// Token: 0x06007780 RID: 30592 RVA: 0x002707F0 File Offset: 0x0026E9F0
	public DeckTrayContent GetCurrentContent()
	{
		DeckTrayContent result;
		this.m_contents.TryGetValue(this.m_currentContent, out result);
		return result;
	}

	// Token: 0x06007781 RID: 30593 RVA: 0x00270812 File Offset: 0x0026EA12
	public DeckTray.DeckContentTypes GetCurrentContentType()
	{
		return this.m_currentContent;
	}

	// Token: 0x06007782 RID: 30594 RVA: 0x0027081A File Offset: 0x0026EA1A
	public DeckBigCard GetDeckBigCard()
	{
		return this.m_deckBigCard;
	}

	// Token: 0x06007783 RID: 30595 RVA: 0x00270822 File Offset: 0x0026EA22
	public void SetTrayMode(DeckTray.DeckContentTypes contentType)
	{
		this.m_contentToSet = contentType;
		if (this.m_settingNewMode || this.m_currentContent == contentType)
		{
			return;
		}
		base.StartCoroutine(this.UpdateTrayMode());
	}

	// Token: 0x06007784 RID: 30596
	protected abstract IEnumerator UpdateTrayMode();

	// Token: 0x06007785 RID: 30597 RVA: 0x0027084A File Offset: 0x0026EA4A
	public bool IsUpdatingTrayMode()
	{
		return this.m_updatingTrayMode;
	}

	// Token: 0x06007786 RID: 30598 RVA: 0x00270854 File Offset: 0x0026EA54
	public void TryEnableScrollbar()
	{
		if (this.m_scrollbar == null)
		{
			return;
		}
		if (this.GetCurrentContent() == null)
		{
			return;
		}
		DeckTray.DeckContentScroll deckContentScroll = this.m_scrollables.Find((DeckTray.DeckContentScroll type) => this.GetCurrentContentType() == type.m_contentType);
		if (deckContentScroll == null || deckContentScroll.m_scrollObject == null)
		{
			Debug.LogWarning("No scrollable object defined.");
			return;
		}
		this.m_scrollbar.m_ScrollObject = deckContentScroll.m_scrollObject;
		this.m_scrollbar.ResetScrollStartPosition(deckContentScroll.GetStartPosition());
		if (deckContentScroll.m_saveScrollPosition)
		{
			this.m_scrollbar.SetScrollSnap(deckContentScroll.GetCurrentScroll(), true);
		}
		this.m_scrollbar.EnableIfNeeded();
	}

	// Token: 0x06007787 RID: 30599 RVA: 0x002708FC File Offset: 0x0026EAFC
	public void SaveScrollbarPosition(DeckTray.DeckContentTypes contentType)
	{
		DeckTray.DeckContentScroll deckContentScroll = this.m_scrollables.Find((DeckTray.DeckContentScroll type) => contentType == type.m_contentType);
		if (deckContentScroll != null && deckContentScroll.m_saveScrollPosition)
		{
			deckContentScroll.SaveCurrentScroll(this.m_scrollbar.GetScroll());
		}
	}

	// Token: 0x06007788 RID: 30600 RVA: 0x0027094C File Offset: 0x0026EB4C
	public void ResetDeckTrayScroll()
	{
		if (this.m_scrollbar == null)
		{
			return;
		}
		this.m_scrollbar.SetScrollSnap(0f, true);
		foreach (DeckTray.DeckContentScroll deckContentScroll in this.m_scrollables)
		{
			deckContentScroll.SaveCurrentScroll(0f);
		}
	}

	// Token: 0x06007789 RID: 30601 RVA: 0x002709C4 File Offset: 0x0026EBC4
	protected void TryDisableScrollbar()
	{
		if (this.m_scrollbar == null || this.m_scrollbar.m_ScrollObject == null)
		{
			return;
		}
		this.m_scrollbar.Enable(false);
		this.m_scrollbar.m_ScrollObject = null;
	}

	// Token: 0x0600778A RID: 30602 RVA: 0x00270A00 File Offset: 0x0026EC00
	public void AllowInput(bool allowed)
	{
		this.m_inputBlocker.SetActive(!allowed);
	}

	// Token: 0x0600778B RID: 30603 RVA: 0x00270A11 File Offset: 0x0026EC11
	public bool MouseIsOver()
	{
		return UniversalInputManager.Get().InputIsOver(base.gameObject) || this.m_cardsContent.MouseIsOverDeckHelperButton();
	}

	// Token: 0x0600778C RID: 30604
	protected abstract void HideUnseenDeckTrays();

	// Token: 0x0600778D RID: 30605 RVA: 0x00270A32 File Offset: 0x0026EC32
	protected void OnTouchScrollStarted()
	{
		if (this.m_deckBigCard != null)
		{
			this.m_deckBigCard.ForceHide();
		}
	}

	// Token: 0x0600778E RID: 30606 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected void OnTouchScrollEnded()
	{
	}

	// Token: 0x0600778F RID: 30607 RVA: 0x00270A50 File Offset: 0x0026EC50
	public static void OnDeckTrayTileScrollVisibleAffected(GameObject obj, bool visible)
	{
		DeckTrayDeckTileVisual component = obj.GetComponent<DeckTrayDeckTileVisual>();
		if (component == null || !component.IsInUse())
		{
			return;
		}
		if (visible != component.gameObject.activeSelf)
		{
			component.gameObject.SetActive(visible);
		}
	}

	// Token: 0x06007790 RID: 30608
	protected abstract void ShowDeckBigCard(DeckTrayDeckTileVisual cardTile, float delay = 0f);

	// Token: 0x06007791 RID: 30609
	protected abstract void HideDeckBigCard(DeckTrayDeckTileVisual cardTile, bool force = false);

	// Token: 0x06007792 RID: 30610
	protected abstract void OnCardTilePress(DeckTrayDeckTileVisual cardTile);

	// Token: 0x06007793 RID: 30611
	protected abstract void OnCardTileOver(DeckTrayDeckTileVisual cardTile);

	// Token: 0x06007794 RID: 30612
	protected abstract void OnCardTileOut(DeckTrayDeckTileVisual cardTile);

	// Token: 0x06007795 RID: 30613
	protected abstract void OnCardTileRelease(DeckTrayDeckTileVisual cardTile);

	// Token: 0x06007796 RID: 30614 RVA: 0x00270A90 File Offset: 0x0026EC90
	public bool IsShowingDeckContents()
	{
		return this.GetCurrentContentType() > DeckTray.DeckContentTypes.Decks;
	}

	// Token: 0x06007797 RID: 30615 RVA: 0x00270A9B File Offset: 0x0026EC9B
	protected void OnBusyWithDeck(bool busy)
	{
		if (this.m_inputBlocker == null)
		{
			Log.All.PrintError("If this happens, please notify JMac and copy your stack trace to bug 21743!", Array.Empty<object>());
			return;
		}
		this.m_inputBlocker.SetActive(busy);
	}

	// Token: 0x06007798 RID: 30616 RVA: 0x00270ACC File Offset: 0x0026ECCC
	protected void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, object callbackData)
	{
		bool isNewDeck = callbackData != null && callbackData is bool && (bool)callbackData;
		foreach (KeyValuePair<DeckTray.DeckContentTypes, DeckTrayContent> keyValuePair in this.m_contents)
		{
			keyValuePair.Value.OnEditedDeckChanged(newDeck, oldDeck, isNewDeck);
		}
	}

	// Token: 0x06007799 RID: 30617
	public abstract bool OnBackOutOfDeckContents();

	// Token: 0x0600779A RID: 30618 RVA: 0x00270B3C File Offset: 0x0026ED3C
	protected void FireModeSwitchedEvent()
	{
		DeckTray.ModeSwitched[] array = this.m_modeSwitchedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x0600779B RID: 30619 RVA: 0x00270B6B File Offset: 0x0026ED6B
	public void RegisterModeSwitchedListener(DeckTray.ModeSwitched callback)
	{
		this.m_modeSwitchedListeners.Add(callback);
	}

	// Token: 0x0600779C RID: 30620 RVA: 0x00270B79 File Offset: 0x0026ED79
	public void UnregisterModeSwitchedListener(DeckTray.ModeSwitched callback)
	{
		this.m_modeSwitchedListeners.Remove(callback);
	}

	// Token: 0x04005D97 RID: 23959
	public DeckTrayCardListContent m_cardsContent;

	// Token: 0x04005D98 RID: 23960
	public UIBScrollable m_scrollbar;

	// Token: 0x04005D99 RID: 23961
	public DeckBigCard m_deckBigCard;

	// Token: 0x04005D9A RID: 23962
	public GameObject m_inputBlocker;

	// Token: 0x04005D9B RID: 23963
	public GameObject m_topCardPositionBone;

	// Token: 0x04005D9C RID: 23964
	public List<DeckTray.DeckContentScroll> m_scrollables = new List<DeckTray.DeckContentScroll>();

	// Token: 0x04005D9D RID: 23965
	protected Map<DeckTray.DeckContentTypes, DeckTrayContent> m_contents = new Map<DeckTray.DeckContentTypes, DeckTrayContent>();

	// Token: 0x04005D9E RID: 23966
	protected DeckTray.DeckContentTypes m_currentContent = DeckTray.DeckContentTypes.INVALID;

	// Token: 0x04005D9F RID: 23967
	protected DeckTray.DeckContentTypes m_contentToSet = DeckTray.DeckContentTypes.INVALID;

	// Token: 0x04005DA0 RID: 23968
	protected bool m_settingNewMode;

	// Token: 0x04005DA1 RID: 23969
	protected bool m_updatingTrayMode;

	// Token: 0x04005DA2 RID: 23970
	protected List<DeckTray.ModeSwitched> m_modeSwitchedListeners = new List<DeckTray.ModeSwitched>();

	// Token: 0x020024CE RID: 9422
	public enum DeckContentTypes
	{
		// Token: 0x0400EBD0 RID: 60368
		Decks,
		// Token: 0x0400EBD1 RID: 60369
		Cards,
		// Token: 0x0400EBD2 RID: 60370
		HeroSkin,
		// Token: 0x0400EBD3 RID: 60371
		CardBack,
		// Token: 0x0400EBD4 RID: 60372
		Coin,
		// Token: 0x0400EBD5 RID: 60373
		INVALID
	}

	// Token: 0x020024CF RID: 9423
	// (Invoke) Token: 0x060130FB RID: 78075
	public delegate void ModeSwitched();

	// Token: 0x020024D0 RID: 9424
	[Serializable]
	public class DeckContentScroll
	{
		// Token: 0x060130FE RID: 78078 RVA: 0x00528506 File Offset: 0x00526706
		public void SaveStartPosition()
		{
			if (this.m_scrollObject != null)
			{
				this.m_startPosition = this.m_scrollObject.transform.localPosition;
			}
		}

		// Token: 0x060130FF RID: 78079 RVA: 0x0052852C File Offset: 0x0052672C
		public Vector3 GetStartPosition()
		{
			return this.m_startPosition;
		}

		// Token: 0x06013100 RID: 78080 RVA: 0x00528534 File Offset: 0x00526734
		public Vector3 GetCurrentPosition()
		{
			if (!(this.m_scrollObject != null))
			{
				return Vector3.zero;
			}
			return this.m_scrollObject.transform.localPosition;
		}

		// Token: 0x06013101 RID: 78081 RVA: 0x0052855A File Offset: 0x0052675A
		public void SaveCurrentScroll(float scroll)
		{
			this.m_currentScroll = scroll;
		}

		// Token: 0x06013102 RID: 78082 RVA: 0x00528563 File Offset: 0x00526763
		public float GetCurrentScroll()
		{
			return this.m_currentScroll;
		}

		// Token: 0x0400EBD6 RID: 60374
		public DeckTray.DeckContentTypes m_contentType;

		// Token: 0x0400EBD7 RID: 60375
		public GameObject m_scrollObject;

		// Token: 0x0400EBD8 RID: 60376
		public bool m_saveScrollPosition;

		// Token: 0x0400EBD9 RID: 60377
		private Vector3 m_startPosition;

		// Token: 0x0400EBDA RID: 60378
		private float m_currentScroll;
	}
}
