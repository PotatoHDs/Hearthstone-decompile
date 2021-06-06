using UnityEngine;

public class CollectionDraggableCardVisual : MonoBehaviour
{
	public DragRotatorInfo m_CardDragRotatorInfo = new DragRotatorInfo
	{
		m_PitchInfo = new DragRotatorAxisInfo
		{
			m_ForceMultiplier = 3f,
			m_MinDegrees = -55f,
			m_MaxDegrees = 55f,
			m_RestSeconds = 2f
		},
		m_RollInfo = new DragRotatorAxisInfo
		{
			m_ForceMultiplier = 4.5f,
			m_MinDegrees = -60f,
			m_MaxDegrees = 60f,
			m_RestSeconds = 2f
		}
	};

	private static Vector3 DECK_TILE_LOCAL_SCALE;

	private static Vector3 CARD_ACTOR_LOCAL_SCALE;

	private static Vector3 HERO_SKIN_ACTOR_LOCAL_SCALE;

	private static bool s_scaleIsSetup;

	private CollectionDeckSlot m_slot;

	private DeckTrayDeckTileVisual m_deckTileToRemove;

	private Actor m_cardBackActor;

	private CardBack m_currentCardBack;

	private EntityDef m_entityDef;

	private TAG_PREMIUM m_premium;

	private DefLoader.DisposableCardDef m_cardDef;

	private Actor m_activeActor;

	private CollectionDeckTileActor m_deckTile;

	private Actor m_cardActor;

	private HandActorCache m_actorCache = new HandActorCache();

	private bool m_actorCacheInit;

	private CollectionUtils.ViewMode m_visualType;

	private int m_cardBackId;

	private void Awake()
	{
		EnsureScaleConstantsExist();
		base.gameObject.SetActive(value: false);
		LoadDeckTile();
		LoadCardBack();
		if (base.gameObject.GetComponent<AudioSource>() == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
	}

	private void OnDestroy()
	{
		m_cardDef?.Dispose();
		m_cardDef = null;
	}

	private void Update()
	{
		if (m_deckTileToRemove != null)
		{
			m_deckTileToRemove.SetHighlight(highlight: false);
		}
		m_deckTileToRemove = null;
		if (!(m_activeActor != m_deckTile) && CollectionManager.Get().GetEditedDeck() != null && UniversalInputManager.Get().GetInputHitInfo(DeckTrayDeckTileVisual.LAYER.LayerBit(), out var hitInfo))
		{
			DeckTrayDeckTileVisual component = hitInfo.collider.gameObject.GetComponent<DeckTrayDeckTileVisual>();
			if (!(component == null) && !(component == m_deckTileToRemove))
			{
				m_deckTileToRemove = component;
			}
		}
	}

	public void SetCardBackId(int cardBackId)
	{
		m_cardBackId = cardBackId;
	}

	public int GetCardBackId()
	{
		return m_cardBackId;
	}

	public string GetCardID()
	{
		return m_entityDef.GetCardId();
	}

	public TAG_PREMIUM GetPremium()
	{
		return m_premium;
	}

	public EntityDef GetEntityDef()
	{
		return m_entityDef;
	}

	public CollectionDeckSlot GetSlot()
	{
		return m_slot;
	}

	public void SetSlot(CollectionDeckSlot slot)
	{
		m_slot = slot;
	}

	public CollectionUtils.ViewMode GetVisualType()
	{
		return m_visualType;
	}

	public bool ChangeActor(Actor actor, CollectionUtils.ViewMode vtype, TAG_PREMIUM premium)
	{
		if (!m_actorCacheInit)
		{
			m_actorCacheInit = true;
			m_actorCache.AddActorLoadedListener(OnCardActorLoaded);
			m_actorCache.Initialize();
		}
		if (m_actorCache.IsInitializing())
		{
			return false;
		}
		m_visualType = vtype;
		if (m_visualType != CollectionUtils.ViewMode.CARD_BACKS)
		{
			EntityDef entityDef = actor.GetEntityDef();
			bool flag = entityDef != m_entityDef;
			bool flag2 = premium != m_premium;
			if (!flag && !flag2)
			{
				return true;
			}
			m_entityDef = entityDef;
			m_premium = premium;
			m_cardActor = m_actorCache.GetActor(entityDef, premium);
			if (m_cardActor == null)
			{
				return false;
			}
			if (flag || flag2)
			{
				DefLoader.Get().LoadCardDef(m_entityDef.GetCardId(), OnCardDefLoaded, new CardPortraitQuality(1, m_premium));
			}
			else
			{
				InitDeckTileActor();
				InitCardActor();
			}
			return true;
		}
		if (actor != null)
		{
			m_entityDef = null;
			m_premium = TAG_PREMIUM.NORMAL;
			m_currentCardBack = actor.GetComponentInChildren<CardBack>();
			m_cardActor = m_cardBackActor;
			m_cardBackActor.SetCardbackUpdateIgnore(ignoreUpdate: true);
			return true;
		}
		return false;
	}

	public void UpdateVisual(bool isOverDeck)
	{
		Actor activeActor = m_activeActor;
		SpellType spellType = SpellType.NONE;
		if (m_visualType == CollectionUtils.ViewMode.CARDS)
		{
			if (isOverDeck && m_entityDef != null && !m_entityDef.IsHeroSkin())
			{
				m_activeActor = m_deckTile;
				spellType = SpellType.SUMMON_IN;
			}
			else
			{
				m_activeActor = m_cardActor;
				spellType = SpellType.DEATHREVERSE;
			}
		}
		else
		{
			m_activeActor = m_cardActor;
			spellType = SpellType.DEATHREVERSE;
			if (m_deckTileToRemove != null)
			{
				m_deckTileToRemove.SetHighlight(highlight: false);
			}
		}
		if (activeActor == m_activeActor)
		{
			return;
		}
		if (activeActor != null)
		{
			activeActor.Hide();
			activeActor.gameObject.SetActive(value: false);
		}
		if (m_activeActor == null)
		{
			return;
		}
		m_activeActor.gameObject.SetActive(value: true);
		m_activeActor.Show();
		if (m_visualType == CollectionUtils.ViewMode.CARD_BACKS && m_currentCardBack != null)
		{
			CardBackManager.Get().UpdateCardBack(m_activeActor, m_currentCardBack);
		}
		Spell spell = m_activeActor.GetSpell(spellType);
		if (spell != null)
		{
			spell.ActivateState(SpellStateType.BIRTH);
		}
		if (m_entityDef == null || !m_entityDef.IsHeroSkin())
		{
			return;
		}
		CollectionHeroSkin component = m_activeActor.gameObject.GetComponent<CollectionHeroSkin>();
		if (component != null)
		{
			component.SetClass(m_entityDef.GetClass());
			component.ShowSocketFX();
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				component.ShowName = false;
			}
		}
	}

	public bool IsShown()
	{
		if (base.gameObject == null)
		{
			return false;
		}
		return base.gameObject.activeSelf;
	}

	public void Show(bool isOverDeck)
	{
		base.gameObject.SetActive(value: true);
		UpdateVisual(isOverDeck);
		if (m_deckTile != null && m_entityDef != null)
		{
			m_deckTile.UpdateDeckCardProperties(m_entityDef.IsElite(), isMultiCard: false, 1, useSliderAnimations: false);
		}
	}

	public void Hide()
	{
		if (m_activeActor != null && m_entityDef != null && m_entityDef.IsHeroSkin())
		{
			CollectionHeroSkin component = m_activeActor.gameObject.GetComponent<CollectionHeroSkin>();
			if (component != null)
			{
				component.HideSocketFX();
			}
		}
		base.gameObject.SetActive(value: false);
		if (m_activeActor != null)
		{
			m_activeActor.Hide();
			m_activeActor.gameObject.SetActive(value: false);
			m_activeActor = null;
		}
	}

	public DeckTrayDeckTileVisual GetDeckTileToRemove()
	{
		return m_deckTileToRemove;
	}

	private void LoadDeckTile()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291", AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarning(string.Format("CollectionDraggableCardVisual.OnDeckTileActorLoaded() - FAILED to load actor \"{0}\"", "DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291"));
			return;
		}
		m_deckTile = gameObject.GetComponent<CollectionDeckTileActor>();
		if (m_deckTile == null)
		{
			Debug.LogWarning(string.Format("CollectionDraggableCardVisual.OnDeckTileActorLoaded() - ERROR game object \"{0}\" has no CollectionDeckTileActor component", "DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291"));
			return;
		}
		m_deckTile.Hide();
		m_deckTile.transform.parent = base.transform;
		m_deckTile.transform.localPosition = new Vector3(2.194931f, 0f, 0f);
		m_deckTile.transform.localScale = DECK_TILE_LOCAL_SCALE;
		m_deckTile.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
	}

	private void LoadCardBack()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition);
		GameUtils.SetParent(gameObject, this);
		m_cardBackActor = gameObject.GetComponent<Actor>();
		m_cardBackActor.transform.localScale = CARD_ACTOR_LOCAL_SCALE;
		m_cardBackActor.TurnOffCollider();
		m_cardBackActor.Hide();
		gameObject.AddComponent<DragRotator>().SetInfo(m_CardDragRotatorInfo);
	}

	private void OnCardActorLoaded(string assetName, Actor actor, object callbackData)
	{
		if (actor == null)
		{
			Debug.LogWarning($"CollectionDraggableCardVisual.OnCardActorLoaded() - FAILED to load {assetName}");
			return;
		}
		actor.GetType();
		actor.TurnOffCollider();
		actor.Hide();
		if (base.name == "Card_Hero_Skin")
		{
			actor.transform.localScale = HERO_SKIN_ACTOR_LOCAL_SCALE;
		}
		else
		{
			actor.transform.localScale = CARD_ACTOR_LOCAL_SCALE;
		}
		actor.transform.parent = base.transform;
		actor.transform.localPosition = Vector3.zero;
		actor.gameObject.AddComponent<DragRotator>().SetInfo(m_CardDragRotatorInfo);
	}

	private void OnCardDefLoaded(string cardID, DefLoader.DisposableCardDef cardDef, object callbackData)
	{
		if (m_entityDef == null || m_entityDef.GetCardId() != cardID)
		{
			cardDef?.Dispose();
			return;
		}
		m_cardDef?.Dispose();
		m_cardDef = cardDef;
		InitDeckTileActor();
		InitCardActor();
	}

	private void InitDeckTileActor()
	{
		InitActor(m_deckTile);
		m_deckTile.SetSlot(null);
		m_deckTile.SetCardDef(m_cardDef);
		m_deckTile.UpdateAllComponents();
		m_deckTile.UpdateMaterial(m_cardDef.CardDef.GetDeckCardBarPortrait());
		m_deckTile.UpdateDeckCardProperties(m_entityDef.IsElite(), isMultiCard: false, 1, useSliderAnimations: false);
	}

	private void InitCardActor()
	{
		InitActor(m_cardActor);
		m_cardActor.transform.localRotation = Quaternion.identity;
	}

	private void InitActor(Actor actor)
	{
		actor.SetEntityDef(m_entityDef);
		actor.SetCardDef(m_cardDef);
		actor.SetPremium(m_premium);
		actor.UpdateAllComponents();
	}

	private void EnsureScaleConstantsExist()
	{
		if (!s_scaleIsSetup)
		{
			s_scaleIsSetup = true;
			DECK_TILE_LOCAL_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(0.6f, 0.6f, 0.6f),
				Phone = new Vector3(0.9f, 0.9f, 0.9f)
			};
			CARD_ACTOR_LOCAL_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(6f, 6f, 6f),
				Phone = new Vector3(6.9f, 6.9f, 6.9f)
			};
			HERO_SKIN_ACTOR_LOCAL_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(7.5f, 7.5f, 7.5f),
				Phone = new Vector3(8.2f, 8.2f, 8.2f)
			};
		}
	}
}
