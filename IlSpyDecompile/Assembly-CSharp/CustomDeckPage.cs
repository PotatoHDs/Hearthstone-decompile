using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

public class CustomDeckPage : MonoBehaviour
{
	public delegate void DeckButtonCallback(CollectionDeckBoxVisual deckbox);

	public Vector3 m_customDeckStart;

	public Vector3 m_customDeckScale;

	public float m_customDeckHorizontalSpacing;

	public float m_customDeckVerticalSpacing;

	public CollectionDeckBoxVisual m_deckboxPrefab;

	public Vector3 m_deckCoverOffset;

	public GameObject m_deckboxCoverPrefab;

	public PlayMakerFSM m_vineGlowBurst;

	public GameObject[] m_customVineGlowToggle;

	public int m_maxCustomDecksToDisplay = 9;

	public Material m_multipleDeckSelectionHighlightMaterial;

	protected List<GameObject> m_deckCovers = new List<GameObject>();

	protected List<CollectionDeck> m_collectionDecks;

	protected int m_numCustomDecks;

	protected List<CollectionDeckBoxVisual> m_customDecks = new List<CollectionDeckBoxVisual>();

	protected DeckButtonCallback m_deckButtonCallback;

	private Texture m_customTrayMainTexture;

	private Texture m_customTrayTransitionToTexture;

	public const int DEFAULT_MAX_CUSTOM_DECKS_TO_DISPLAY = 9;

	public void Show()
	{
		GetComponent<Renderer>().enabled = true;
		for (int i = 0; i < m_numCustomDecks; i++)
		{
			if (i < m_customDecks.Count)
			{
				m_customDecks[i].Show();
			}
		}
	}

	public void Hide()
	{
		GetComponent<Renderer>().enabled = false;
		for (int i = 0; i < m_numCustomDecks; i++)
		{
			if (i < m_customDecks.Count)
			{
				m_customDecks[i].Hide();
			}
		}
	}

	public virtual bool PageReady()
	{
		if (m_customTrayMainTexture != null)
		{
			return AreAllCustomDecksReady();
		}
		return false;
	}

	public CollectionDeckBoxVisual GetDeckboxWithDeckID(long deckID)
	{
		if (deckID <= 0)
		{
			return null;
		}
		foreach (CollectionDeckBoxVisual customDeck in m_customDecks)
		{
			if (customDeck.GetDeckID() == deckID)
			{
				return customDeck;
			}
		}
		return null;
	}

	public void UpdateTrayTransitionValue(float transitionValue)
	{
		GetComponent<Renderer>().GetMaterial().SetFloat("_Transistion", transitionValue);
		foreach (GameObject deckCover in m_deckCovers)
		{
			Renderer componentInChildren = deckCover.GetComponentInChildren<Renderer>();
			if (componentInChildren != null)
			{
				componentInChildren.GetMaterial().SetFloat("_Transistion", transitionValue);
			}
		}
	}

	public void PlayVineGlowBurst(bool useFX, bool hasValidStandardDeck)
	{
		if (m_vineGlowBurst != null)
		{
			string text = ((!useFX) ? (hasValidStandardDeck ? "GlowVinesNoFX" : "GlowVinesCustomNoFX") : (hasValidStandardDeck ? "GlowVines" : "GlowVinesCustom"));
			if (!string.IsNullOrEmpty(text))
			{
				m_vineGlowBurst.SendEvent(text);
			}
		}
	}

	public void SetTrayTextures(Texture transitionFromTexture, Texture targetTexture)
	{
		Material material = GetComponent<Renderer>().GetMaterial();
		material.mainTexture = transitionFromTexture;
		material.SetTexture("_MainTex2", targetTexture);
		material.SetFloat("_Transistion", 0f);
		m_customTrayMainTexture = transitionFromTexture;
		m_customTrayTransitionToTexture = targetTexture;
		UpdateDeckVisuals();
	}

	public void SetDecks(List<CollectionDeck> decks)
	{
		m_collectionDecks = decks;
	}

	public void SetDeckButtonCallback(DeckButtonCallback callback)
	{
		m_deckButtonCallback = callback;
	}

	public void EnableDeckButtons(bool enable)
	{
		foreach (CollectionDeckBoxVisual customDeck in m_customDecks)
		{
			customDeck.SetEnabled(enable);
			if (!enable)
			{
				customDeck.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			}
		}
	}

	public CollectionDeckBoxVisual FindDeckVisual(CollectionDeck deck)
	{
		int num = 0;
		foreach (CollectionDeck collectionDeck in m_collectionDecks)
		{
			if (collectionDeck == deck)
			{
				return m_customDecks[num];
			}
			num++;
		}
		return null;
	}

	public void TransitionWildDecks()
	{
		int num = 0;
		foreach (CollectionDeck collectionDeck in m_collectionDecks)
		{
			if (collectionDeck.Type == DeckType.NORMAL_DECK)
			{
				CollectionDeckBoxVisual collectionDeckBoxVisual = m_customDecks[num];
				if (collectionDeck.FormatType == FormatType.FT_WILD)
				{
					collectionDeckBoxVisual.PlayGlowAnim(setFormatToStandard: true);
				}
				collectionDeckBoxVisual.UpdateMissingCardsIndicator();
				num++;
			}
		}
	}

	public virtual void UpdateDeckVisuals()
	{
		foreach (GameObject deckCover in m_deckCovers)
		{
			Material material = deckCover.GetComponentInChildren<Renderer>().GetMaterial();
			material.mainTexture = m_customTrayMainTexture;
			material.SetTexture("_MainTex2", m_customTrayTransitionToTexture);
			material.SetFloat("_Transistion", 0f);
		}
		int i = 0;
		m_numCustomDecks = 0;
		foreach (CollectionDeck collectionDeck in m_collectionDecks)
		{
			if (collectionDeck.Type == DeckType.NORMAL_DECK)
			{
				if (collectionDeck.FormatType == FormatType.FT_UNKNOWN && !collectionDeck.Locked)
				{
					Debug.LogError("A deck with an unknown format type was detected. Details: " + collectionDeck.ToString());
				}
				m_numCustomDecks++;
				CollectionDeckBoxVisual collectionDeckBoxVisual = m_customDecks[i];
				collectionDeckBoxVisual.SetDeckName(collectionDeck.Name);
				collectionDeckBoxVisual.SetDeckID(collectionDeck.ID);
				collectionDeckBoxVisual.SetHeroCardPremiumOverride(collectionDeck.GetDisplayHeroPremiumOverride());
				collectionDeckBoxVisual.SetHeroCardID(collectionDeck.GetDisplayHeroCardID());
				collectionDeckBoxVisual.SetFormatType(collectionDeck.FormatType);
				collectionDeckBoxVisual.SetIsShared(collectionDeck.IsShared);
				collectionDeckBoxVisual.UpdateMissingCardsIndicator();
				collectionDeckBoxVisual.Show();
				if (i < m_deckCovers.Count)
				{
					m_deckCovers[i].SetActive(value: false);
				}
				i++;
				if (i >= m_customDecks.Count)
				{
					break;
				}
			}
		}
		for (; i < m_customDecks.Count; i++)
		{
			m_customDecks[i].Hide();
			if (i < m_deckCovers.Count)
			{
				m_deckCovers[i].SetActive(value: true);
			}
		}
	}

	public bool HasWildDeck()
	{
		foreach (CollectionDeck collectionDeck in m_collectionDecks)
		{
			if (collectionDeck.FormatType == FormatType.FT_WILD)
			{
				return true;
			}
		}
		return false;
	}

	private bool AreAllCustomDecksReady()
	{
		foreach (CollectionDeckBoxVisual customDeck in m_customDecks)
		{
			if (!customDeck.HasFullDef() && customDeck.GetDeckID() > 0)
			{
				return false;
			}
		}
		return true;
	}

	public void InitCustomDecks()
	{
		int i = 0;
		Vector3 customDeckStart = m_customDeckStart;
		float customDeckHorizontalSpacing = m_customDeckHorizontalSpacing;
		float customDeckVerticalSpacing = m_customDeckVerticalSpacing;
		for (; i < m_maxCustomDecksToDisplay; i++)
		{
			GameObject gameObject = new GameObject();
			gameObject.name = "DeckParent" + i;
			gameObject.transform.parent = base.gameObject.transform;
			if (i == 0)
			{
				gameObject.transform.localPosition = customDeckStart;
			}
			else
			{
				float x = customDeckStart.x - (float)(i % 3) * customDeckHorizontalSpacing;
				float z = (float)Mathf.CeilToInt(i / 3) * customDeckVerticalSpacing + customDeckStart.z;
				gameObject.transform.localPosition = new Vector3(x, customDeckStart.y, z);
			}
			CollectionDeckBoxVisual deckBox = Object.Instantiate(m_deckboxPrefab);
			CollectionDeckBoxVisual collectionDeckBoxVisual = deckBox;
			collectionDeckBoxVisual.name = collectionDeckBoxVisual.name + " - " + i;
			deckBox.transform.parent = gameObject.transform;
			deckBox.transform.localPosition = Vector3.zero;
			deckBox.StoreOriginalButtonPositionAndRotation();
			gameObject.transform.localScale = m_customDeckScale;
			if (m_deckButtonCallback == null)
			{
				Debug.LogError("SetDeckButtonCallback() not called in CustomDeckPage!");
			}
			else
			{
				deckBox.AddEventListener(UIEventType.RELEASE, delegate
				{
					OnSelectCustomDeck(deckBox);
				});
			}
			deckBox.SetEnabled(enabled: true);
			m_customDecks.Add(deckBox);
			if (m_deckboxCoverPrefab != null)
			{
				GameObject gameObject2 = Object.Instantiate(m_deckboxCoverPrefab);
				gameObject2.transform.parent = base.gameObject.transform;
				gameObject2.transform.localScale = m_customDeckScale;
				gameObject2.transform.position = gameObject.transform.position + m_deckCoverOffset;
				m_deckCovers.Add(gameObject2);
			}
		}
		if (m_collectionDecks == null)
		{
			Debug.LogErrorFormat("m_collectionDecks not set in CustomDeckPage! Ensure that SetDecks() is called before this method!");
		}
		else
		{
			UpdateDeckVisuals();
		}
	}

	private void OnSelectCustomDeck(CollectionDeckBoxVisual deckbox)
	{
		m_deckButtonCallback(deckbox);
	}
}
