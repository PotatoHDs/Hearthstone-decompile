using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeckTray : MonoBehaviour
{
	public enum DeckContentTypes
	{
		Decks,
		Cards,
		HeroSkin,
		CardBack,
		Coin,
		INVALID
	}

	public delegate void ModeSwitched();

	[Serializable]
	public class DeckContentScroll
	{
		public DeckContentTypes m_contentType;

		public GameObject m_scrollObject;

		public bool m_saveScrollPosition;

		private Vector3 m_startPosition;

		private float m_currentScroll;

		public void SaveStartPosition()
		{
			if (m_scrollObject != null)
			{
				m_startPosition = m_scrollObject.transform.localPosition;
			}
		}

		public Vector3 GetStartPosition()
		{
			return m_startPosition;
		}

		public Vector3 GetCurrentPosition()
		{
			if (!(m_scrollObject != null))
			{
				return Vector3.zero;
			}
			return m_scrollObject.transform.localPosition;
		}

		public void SaveCurrentScroll(float scroll)
		{
			m_currentScroll = scroll;
		}

		public float GetCurrentScroll()
		{
			return m_currentScroll;
		}
	}

	public DeckTrayCardListContent m_cardsContent;

	public UIBScrollable m_scrollbar;

	public DeckBigCard m_deckBigCard;

	public GameObject m_inputBlocker;

	public GameObject m_topCardPositionBone;

	public List<DeckContentScroll> m_scrollables = new List<DeckContentScroll>();

	protected Map<DeckContentTypes, DeckTrayContent> m_contents = new Map<DeckContentTypes, DeckTrayContent>();

	protected DeckContentTypes m_currentContent = DeckContentTypes.INVALID;

	protected DeckContentTypes m_contentToSet = DeckContentTypes.INVALID;

	protected bool m_settingNewMode;

	protected bool m_updatingTrayMode;

	protected List<ModeSwitched> m_modeSwitchedListeners = new List<ModeSwitched>();

	protected virtual void Start()
	{
		SoundManager.Get().Load("panel_slide_off_deck_creation_screen.prefab:b0d25fc984ec05d4fbea7480b611e5ad");
	}

	public void Initialize()
	{
		SetTrayMode(DeckContentTypes.Decks);
	}

	public DeckTrayCardListContent GetCardsContent()
	{
		return m_cardsContent;
	}

	public DeckTrayContent GetCurrentContent()
	{
		m_contents.TryGetValue(m_currentContent, out var value);
		return value;
	}

	public DeckContentTypes GetCurrentContentType()
	{
		return m_currentContent;
	}

	public DeckBigCard GetDeckBigCard()
	{
		return m_deckBigCard;
	}

	public void SetTrayMode(DeckContentTypes contentType)
	{
		m_contentToSet = contentType;
		if (!m_settingNewMode && m_currentContent != contentType)
		{
			StartCoroutine(UpdateTrayMode());
		}
	}

	protected abstract IEnumerator UpdateTrayMode();

	public bool IsUpdatingTrayMode()
	{
		return m_updatingTrayMode;
	}

	public void TryEnableScrollbar()
	{
		if (m_scrollbar == null || GetCurrentContent() == null)
		{
			return;
		}
		DeckContentScroll deckContentScroll = m_scrollables.Find((DeckContentScroll type) => GetCurrentContentType() == type.m_contentType);
		if (deckContentScroll == null || deckContentScroll.m_scrollObject == null)
		{
			Debug.LogWarning("No scrollable object defined.");
			return;
		}
		m_scrollbar.m_ScrollObject = deckContentScroll.m_scrollObject;
		m_scrollbar.ResetScrollStartPosition(deckContentScroll.GetStartPosition());
		if (deckContentScroll.m_saveScrollPosition)
		{
			m_scrollbar.SetScrollSnap(deckContentScroll.GetCurrentScroll());
		}
		m_scrollbar.EnableIfNeeded();
	}

	public void SaveScrollbarPosition(DeckContentTypes contentType)
	{
		DeckContentScroll deckContentScroll = m_scrollables.Find((DeckContentScroll type) => contentType == type.m_contentType);
		if (deckContentScroll != null && deckContentScroll.m_saveScrollPosition)
		{
			deckContentScroll.SaveCurrentScroll(m_scrollbar.GetScroll());
		}
	}

	public void ResetDeckTrayScroll()
	{
		if (m_scrollbar == null)
		{
			return;
		}
		m_scrollbar.SetScrollSnap(0f);
		foreach (DeckContentScroll scrollable in m_scrollables)
		{
			scrollable.SaveCurrentScroll(0f);
		}
	}

	protected void TryDisableScrollbar()
	{
		if (!(m_scrollbar == null) && !(m_scrollbar.m_ScrollObject == null))
		{
			m_scrollbar.Enable(enable: false);
			m_scrollbar.m_ScrollObject = null;
		}
	}

	public void AllowInput(bool allowed)
	{
		m_inputBlocker.SetActive(!allowed);
	}

	public bool MouseIsOver()
	{
		if (!UniversalInputManager.Get().InputIsOver(base.gameObject))
		{
			return m_cardsContent.MouseIsOverDeckHelperButton();
		}
		return true;
	}

	protected abstract void HideUnseenDeckTrays();

	protected void OnTouchScrollStarted()
	{
		if (m_deckBigCard != null)
		{
			m_deckBigCard.ForceHide();
		}
	}

	protected void OnTouchScrollEnded()
	{
	}

	public static void OnDeckTrayTileScrollVisibleAffected(GameObject obj, bool visible)
	{
		DeckTrayDeckTileVisual component = obj.GetComponent<DeckTrayDeckTileVisual>();
		if (!(component == null) && component.IsInUse() && visible != component.gameObject.activeSelf)
		{
			component.gameObject.SetActive(visible);
		}
	}

	protected abstract void ShowDeckBigCard(DeckTrayDeckTileVisual cardTile, float delay = 0f);

	protected abstract void HideDeckBigCard(DeckTrayDeckTileVisual cardTile, bool force = false);

	protected abstract void OnCardTilePress(DeckTrayDeckTileVisual cardTile);

	protected abstract void OnCardTileOver(DeckTrayDeckTileVisual cardTile);

	protected abstract void OnCardTileOut(DeckTrayDeckTileVisual cardTile);

	protected abstract void OnCardTileRelease(DeckTrayDeckTileVisual cardTile);

	public bool IsShowingDeckContents()
	{
		return GetCurrentContentType() != DeckContentTypes.Decks;
	}

	protected void OnBusyWithDeck(bool busy)
	{
		if (m_inputBlocker == null)
		{
			Log.All.PrintError("If this happens, please notify JMac and copy your stack trace to bug 21743!");
		}
		else
		{
			m_inputBlocker.SetActive(busy);
		}
	}

	protected void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, object callbackData)
	{
		bool isNewDeck = callbackData != null && callbackData is bool && (bool)callbackData;
		foreach (KeyValuePair<DeckContentTypes, DeckTrayContent> content in m_contents)
		{
			content.Value.OnEditedDeckChanged(newDeck, oldDeck, isNewDeck);
		}
	}

	public abstract bool OnBackOutOfDeckContents();

	protected void FireModeSwitchedEvent()
	{
		ModeSwitched[] array = m_modeSwitchedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	public void RegisterModeSwitchedListener(ModeSwitched callback)
	{
		m_modeSwitchedListeners.Add(callback);
	}

	public void UnregisterModeSwitchedListener(ModeSwitched callback)
	{
		m_modeSwitchedListeners.Remove(callback);
	}
}
