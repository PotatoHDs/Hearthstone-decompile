using System;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x020002A5 RID: 677
public class CustomDeckPage : MonoBehaviour
{
	// Token: 0x0600226D RID: 8813 RVA: 0x000AA370 File Offset: 0x000A8570
	public void Show()
	{
		base.GetComponent<Renderer>().enabled = true;
		for (int i = 0; i < this.m_numCustomDecks; i++)
		{
			if (i < this.m_customDecks.Count)
			{
				this.m_customDecks[i].Show();
			}
		}
	}

	// Token: 0x0600226E RID: 8814 RVA: 0x000AA3BC File Offset: 0x000A85BC
	public void Hide()
	{
		base.GetComponent<Renderer>().enabled = false;
		for (int i = 0; i < this.m_numCustomDecks; i++)
		{
			if (i < this.m_customDecks.Count)
			{
				this.m_customDecks[i].Hide();
			}
		}
	}

	// Token: 0x0600226F RID: 8815 RVA: 0x000AA405 File Offset: 0x000A8605
	public virtual bool PageReady()
	{
		return this.m_customTrayMainTexture != null && this.AreAllCustomDecksReady();
	}

	// Token: 0x06002270 RID: 8816 RVA: 0x000AA420 File Offset: 0x000A8620
	public CollectionDeckBoxVisual GetDeckboxWithDeckID(long deckID)
	{
		if (deckID <= 0L)
		{
			return null;
		}
		foreach (CollectionDeckBoxVisual collectionDeckBoxVisual in this.m_customDecks)
		{
			if (collectionDeckBoxVisual.GetDeckID() == deckID)
			{
				return collectionDeckBoxVisual;
			}
		}
		return null;
	}

	// Token: 0x06002271 RID: 8817 RVA: 0x000AA484 File Offset: 0x000A8684
	public void UpdateTrayTransitionValue(float transitionValue)
	{
		base.GetComponent<Renderer>().GetMaterial().SetFloat("_Transistion", transitionValue);
		foreach (GameObject gameObject in this.m_deckCovers)
		{
			Renderer componentInChildren = gameObject.GetComponentInChildren<Renderer>();
			if (componentInChildren != null)
			{
				componentInChildren.GetMaterial().SetFloat("_Transistion", transitionValue);
			}
		}
	}

	// Token: 0x06002272 RID: 8818 RVA: 0x000AA508 File Offset: 0x000A8708
	public void PlayVineGlowBurst(bool useFX, bool hasValidStandardDeck)
	{
		if (this.m_vineGlowBurst != null)
		{
			string text;
			if (useFX)
			{
				text = (hasValidStandardDeck ? "GlowVines" : "GlowVinesCustom");
			}
			else
			{
				text = (hasValidStandardDeck ? "GlowVinesNoFX" : "GlowVinesCustomNoFX");
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.m_vineGlowBurst.SendEvent(text);
			}
		}
	}

	// Token: 0x06002273 RID: 8819 RVA: 0x000AA55C File Offset: 0x000A875C
	public void SetTrayTextures(Texture transitionFromTexture, Texture targetTexture)
	{
		Material material = base.GetComponent<Renderer>().GetMaterial();
		material.mainTexture = transitionFromTexture;
		material.SetTexture("_MainTex2", targetTexture);
		material.SetFloat("_Transistion", 0f);
		this.m_customTrayMainTexture = transitionFromTexture;
		this.m_customTrayTransitionToTexture = targetTexture;
		this.UpdateDeckVisuals();
	}

	// Token: 0x06002274 RID: 8820 RVA: 0x000AA5AA File Offset: 0x000A87AA
	public void SetDecks(List<CollectionDeck> decks)
	{
		this.m_collectionDecks = decks;
	}

	// Token: 0x06002275 RID: 8821 RVA: 0x000AA5B3 File Offset: 0x000A87B3
	public void SetDeckButtonCallback(CustomDeckPage.DeckButtonCallback callback)
	{
		this.m_deckButtonCallback = callback;
	}

	// Token: 0x06002276 RID: 8822 RVA: 0x000AA5BC File Offset: 0x000A87BC
	public void EnableDeckButtons(bool enable)
	{
		foreach (CollectionDeckBoxVisual collectionDeckBoxVisual in this.m_customDecks)
		{
			collectionDeckBoxVisual.SetEnabled(enable, false);
			if (!enable)
			{
				collectionDeckBoxVisual.SetHighlightState(ActorStateType.HIGHLIGHT_OFF);
			}
		}
	}

	// Token: 0x06002277 RID: 8823 RVA: 0x000AA61C File Offset: 0x000A881C
	public CollectionDeckBoxVisual FindDeckVisual(CollectionDeck deck)
	{
		int num = 0;
		using (List<CollectionDeck>.Enumerator enumerator = this.m_collectionDecks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == deck)
				{
					return this.m_customDecks[num];
				}
				num++;
			}
		}
		return null;
	}

	// Token: 0x06002278 RID: 8824 RVA: 0x000AA684 File Offset: 0x000A8884
	public void TransitionWildDecks()
	{
		int num = 0;
		foreach (CollectionDeck collectionDeck in this.m_collectionDecks)
		{
			if (collectionDeck.Type == DeckType.NORMAL_DECK)
			{
				CollectionDeckBoxVisual collectionDeckBoxVisual = this.m_customDecks[num];
				if (collectionDeck.FormatType == FormatType.FT_WILD)
				{
					collectionDeckBoxVisual.PlayGlowAnim(true);
				}
				collectionDeckBoxVisual.UpdateMissingCardsIndicator();
				num++;
			}
		}
	}

	// Token: 0x06002279 RID: 8825 RVA: 0x000AA704 File Offset: 0x000A8904
	public virtual void UpdateDeckVisuals()
	{
		foreach (GameObject gameObject in this.m_deckCovers)
		{
			Material material = gameObject.GetComponentInChildren<Renderer>().GetMaterial();
			material.mainTexture = this.m_customTrayMainTexture;
			material.SetTexture("_MainTex2", this.m_customTrayTransitionToTexture);
			material.SetFloat("_Transistion", 0f);
		}
		int num = 0;
		this.m_numCustomDecks = 0;
		using (List<CollectionDeck>.Enumerator enumerator2 = this.m_collectionDecks.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				CollectionDeck collectionDeck = enumerator2.Current;
				if (collectionDeck.Type == DeckType.NORMAL_DECK)
				{
					if (collectionDeck.FormatType == FormatType.FT_UNKNOWN && !collectionDeck.Locked)
					{
						Debug.LogError("A deck with an unknown format type was detected. Details: " + collectionDeck.ToString());
					}
					this.m_numCustomDecks++;
					CollectionDeckBoxVisual collectionDeckBoxVisual = this.m_customDecks[num];
					collectionDeckBoxVisual.SetDeckName(collectionDeck.Name);
					collectionDeckBoxVisual.SetDeckID(collectionDeck.ID);
					collectionDeckBoxVisual.SetHeroCardPremiumOverride(collectionDeck.GetDisplayHeroPremiumOverride());
					collectionDeckBoxVisual.SetHeroCardID(collectionDeck.GetDisplayHeroCardID());
					collectionDeckBoxVisual.SetFormatType(collectionDeck.FormatType);
					collectionDeckBoxVisual.SetIsShared(collectionDeck.IsShared);
					collectionDeckBoxVisual.UpdateMissingCardsIndicator();
					collectionDeckBoxVisual.Show();
					if (num < this.m_deckCovers.Count)
					{
						this.m_deckCovers[num].SetActive(false);
					}
					num++;
					if (num >= this.m_customDecks.Count)
					{
						break;
					}
				}
			}
			goto IL_1A9;
		}
		IL_174:
		this.m_customDecks[num].Hide();
		if (num < this.m_deckCovers.Count)
		{
			this.m_deckCovers[num].SetActive(true);
		}
		num++;
		IL_1A9:
		if (num >= this.m_customDecks.Count)
		{
			return;
		}
		goto IL_174;
	}

	// Token: 0x0600227A RID: 8826 RVA: 0x000AA8E4 File Offset: 0x000A8AE4
	public bool HasWildDeck()
	{
		using (List<CollectionDeck>.Enumerator enumerator = this.m_collectionDecks.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.FormatType == FormatType.FT_WILD)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600227B RID: 8827 RVA: 0x000AA940 File Offset: 0x000A8B40
	private bool AreAllCustomDecksReady()
	{
		foreach (CollectionDeckBoxVisual collectionDeckBoxVisual in this.m_customDecks)
		{
			if (!collectionDeckBoxVisual.HasFullDef() && collectionDeckBoxVisual.GetDeckID() > 0L)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600227C RID: 8828 RVA: 0x000AA9A8 File Offset: 0x000A8BA8
	public void InitCustomDecks()
	{
		int i = 0;
		Vector3 customDeckStart = this.m_customDeckStart;
		float customDeckHorizontalSpacing = this.m_customDeckHorizontalSpacing;
		float customDeckVerticalSpacing = this.m_customDeckVerticalSpacing;
		while (i < this.m_maxCustomDecksToDisplay)
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
				float z = (float)Mathf.CeilToInt((float)(i / 3)) * customDeckVerticalSpacing + customDeckStart.z;
				gameObject.transform.localPosition = new Vector3(x, customDeckStart.y, z);
			}
			CollectionDeckBoxVisual deckBox = UnityEngine.Object.Instantiate<CollectionDeckBoxVisual>(this.m_deckboxPrefab);
			CollectionDeckBoxVisual deckBox2 = deckBox;
			deckBox2.name = deckBox2.name + " - " + i;
			deckBox.transform.parent = gameObject.transform;
			deckBox.transform.localPosition = Vector3.zero;
			deckBox.StoreOriginalButtonPositionAndRotation();
			gameObject.transform.localScale = this.m_customDeckScale;
			if (this.m_deckButtonCallback == null)
			{
				Debug.LogError("SetDeckButtonCallback() not called in CustomDeckPage!");
			}
			else
			{
				deckBox.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
				{
					this.OnSelectCustomDeck(deckBox);
				});
			}
			deckBox.SetEnabled(true, false);
			this.m_customDecks.Add(deckBox);
			if (this.m_deckboxCoverPrefab != null)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.m_deckboxCoverPrefab);
				gameObject2.transform.parent = base.gameObject.transform;
				gameObject2.transform.localScale = this.m_customDeckScale;
				gameObject2.transform.position = gameObject.transform.position + this.m_deckCoverOffset;
				this.m_deckCovers.Add(gameObject2);
			}
			i++;
		}
		if (this.m_collectionDecks == null)
		{
			Debug.LogErrorFormat("m_collectionDecks not set in CustomDeckPage! Ensure that SetDecks() is called before this method!", Array.Empty<object>());
			return;
		}
		this.UpdateDeckVisuals();
	}

	// Token: 0x0600227D RID: 8829 RVA: 0x000AABD7 File Offset: 0x000A8DD7
	private void OnSelectCustomDeck(CollectionDeckBoxVisual deckbox)
	{
		this.m_deckButtonCallback(deckbox);
	}

	// Token: 0x04001306 RID: 4870
	public Vector3 m_customDeckStart;

	// Token: 0x04001307 RID: 4871
	public Vector3 m_customDeckScale;

	// Token: 0x04001308 RID: 4872
	public float m_customDeckHorizontalSpacing;

	// Token: 0x04001309 RID: 4873
	public float m_customDeckVerticalSpacing;

	// Token: 0x0400130A RID: 4874
	public CollectionDeckBoxVisual m_deckboxPrefab;

	// Token: 0x0400130B RID: 4875
	public Vector3 m_deckCoverOffset;

	// Token: 0x0400130C RID: 4876
	public GameObject m_deckboxCoverPrefab;

	// Token: 0x0400130D RID: 4877
	public PlayMakerFSM m_vineGlowBurst;

	// Token: 0x0400130E RID: 4878
	public GameObject[] m_customVineGlowToggle;

	// Token: 0x0400130F RID: 4879
	public int m_maxCustomDecksToDisplay = 9;

	// Token: 0x04001310 RID: 4880
	public Material m_multipleDeckSelectionHighlightMaterial;

	// Token: 0x04001311 RID: 4881
	protected List<GameObject> m_deckCovers = new List<GameObject>();

	// Token: 0x04001312 RID: 4882
	protected List<CollectionDeck> m_collectionDecks;

	// Token: 0x04001313 RID: 4883
	protected int m_numCustomDecks;

	// Token: 0x04001314 RID: 4884
	protected List<CollectionDeckBoxVisual> m_customDecks = new List<CollectionDeckBoxVisual>();

	// Token: 0x04001315 RID: 4885
	protected CustomDeckPage.DeckButtonCallback m_deckButtonCallback;

	// Token: 0x04001316 RID: 4886
	private Texture m_customTrayMainTexture;

	// Token: 0x04001317 RID: 4887
	private Texture m_customTrayTransitionToTexture;

	// Token: 0x04001318 RID: 4888
	public const int DEFAULT_MAX_CUSTOM_DECKS_TO_DISPLAY = 9;

	// Token: 0x0200157D RID: 5501
	// (Invoke) Token: 0x0600E074 RID: 57460
	public delegate void DeckButtonCallback(CollectionDeckBoxVisual deckbox);
}
