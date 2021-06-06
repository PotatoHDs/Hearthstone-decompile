using UnityEngine;

public class DeckTrayDeckTileVisual : PegUIElement
{
	public static readonly GameLayer LAYER = GameLayer.CardRaycast;

	private readonly Vector3 BOX_COLLIDER_SIZE = new Vector3(25.34f, 2.14f, 3.68f);

	private readonly Vector3 BOX_COLLIDER_CENTER = new Vector3(-1.4f, 0f, 0f);

	protected const int DEFAULT_PORTRAIT_QUALITY = 1;

	protected CollectionDeck m_deck;

	protected CollectionDeckSlot m_slot;

	protected BoxCollider m_collider;

	protected CollectionDeckTileActor m_actor;

	protected bool m_isInUse;

	protected bool m_useSliderAnimations;

	protected bool m_inArena;

	public void Initialize(bool useFullScaleDeckTileActor)
	{
		string text = (useFullScaleDeckTileActor ? "DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291" : "DeckCardBar_phone.prefab:bd1c5e767f791984e851553bc5cb3b07");
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarning($"DeckTrayDeckTileVisual.OnDeckTileActorLoaded() - FAILED to load actor \"{text}\"");
			return;
		}
		m_actor = gameObject.GetComponent<CollectionDeckTileActor>();
		if (m_actor == null)
		{
			Debug.LogWarning($"DeckTrayDeckTileVisual.OnDeckTileActorLoaded() - ERROR game object \"{text}\" has no CollectionDeckTileActor component");
			return;
		}
		GameUtils.SetParent(m_actor, this);
		m_actor.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		UIBScrollableItem component = m_actor.GetComponent<UIBScrollableItem>();
		if (component != null)
		{
			component.SetCustomActiveState(IsInUse);
		}
		SetUpActor();
		if (base.gameObject.GetComponent<BoxCollider>() == null)
		{
			m_collider = base.gameObject.AddComponent<BoxCollider>();
			m_collider.size = BOX_COLLIDER_SIZE;
			m_collider.center = BOX_COLLIDER_CENTER;
		}
		Hide();
		SceneUtils.SetLayer(base.gameObject, LAYER);
		SetDragTolerance(5f);
	}

	public string GetCardID()
	{
		return m_actor.GetEntityDef().GetCardId();
	}

	public TAG_PREMIUM GetPremium()
	{
		return m_actor.GetPremium();
	}

	public CollectionDeckSlot GetSlot()
	{
		return m_slot;
	}

	public void SetSlot(CollectionDeck deck, CollectionDeckSlot s, bool useSliderAnimations)
	{
		m_deck = deck;
		m_slot = s;
		m_useSliderAnimations = useSliderAnimations;
		SetUpActor();
	}

	public CollectionDeckTileActor GetActor()
	{
		return m_actor;
	}

	public Bounds GetBounds()
	{
		return m_collider.bounds;
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
	}

	public void ShowAndSetupActor()
	{
		Show();
		SetUpActor();
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}

	public void MarkAsUsed()
	{
		m_isInUse = true;
	}

	public void MarkAsUnused()
	{
		m_isInUse = false;
		if (!(m_actor == null))
		{
			m_actor.UpdateDeckCardProperties(CollectionDeckTileActor.TileIconState.CARD_COUNT, 1, useSliderAnimations: false);
		}
	}

	public bool IsInUse()
	{
		return m_isInUse;
	}

	public void SetInArena(bool inArena)
	{
		m_inArena = inArena;
	}

	public void SetHighlight(bool highlight)
	{
		if (m_actor.m_highlight != null)
		{
			m_actor.m_highlight.SetActive(highlight);
		}
		if (m_actor.m_highlightGlow != null)
		{
			if (GetGhostedState() == CollectionDeckTileActor.GhostedState.RED)
			{
				m_actor.m_highlightGlow.SetActive(highlight);
			}
			else
			{
				m_actor.m_highlightGlow.SetActive(value: false);
			}
		}
	}

	public void UpdateGhostedState()
	{
		m_actor.SetGhosted(GetGhostedState());
		m_actor.UpdateGhostTileEffect();
	}

	private CollectionDeckTileActor.GhostedState GetGhostedState()
	{
		if (m_deck != null)
		{
			switch (m_deck.GetSlotStatus(m_slot))
			{
			case CollectionDeck.SlotStatus.MISSING:
				return CollectionDeckTileActor.GhostedState.BLUE;
			case CollectionDeck.SlotStatus.NOT_VALID:
				return CollectionDeckTileActor.GhostedState.RED;
			}
		}
		return CollectionDeckTileActor.GhostedState.NONE;
	}

	private void SetUpActor()
	{
		if (m_actor == null || m_slot == null || string.IsNullOrEmpty(m_slot.CardID))
		{
			return;
		}
		m_actor.GetEntityDef();
		EntityDef entityDef = m_slot.GetEntityDef();
		m_actor.SetSlot(m_slot);
		TAG_PREMIUM tAG_PREMIUM = m_slot.PreferredPremium;
		if (m_inArena && Options.Get().GetBool(Option.HAS_DISABLED_PREMIUMS_THIS_DRAFT))
		{
			tAG_PREMIUM = TAG_PREMIUM.NORMAL;
		}
		m_actor.SetPremium(tAG_PREMIUM);
		m_actor.SetEntityDef(entityDef);
		m_actor.SetGhosted(GetGhostedState());
		bool flag = entityDef?.IsElite() ?? false;
		if (flag && m_inArena && m_slot.Count > 1)
		{
			flag = false;
		}
		m_actor.UpdateDeckCardProperties(flag, isMultiCard: false, m_slot.Count, m_useSliderAnimations);
		DefLoader.Get().LoadCardDef(entityDef.GetCardId(), delegate(string cardID, DefLoader.DisposableCardDef cardDef, object data)
		{
			using (cardDef)
			{
				if (!(m_actor == null) && cardID.Equals(m_actor.GetEntityDef().GetCardId()))
				{
					m_actor.SetCardDef(cardDef);
					m_actor.UpdateAllComponents();
					m_actor.UpdateMaterial(cardDef.CardDef.GetDeckCardBarPortrait());
					m_actor.UpdateGhostTileEffect();
				}
			}
		}, null, new CardPortraitQuality(1, tAG_PREMIUM));
	}
}
