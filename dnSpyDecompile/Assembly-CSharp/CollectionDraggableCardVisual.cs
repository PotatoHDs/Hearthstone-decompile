using System;
using UnityEngine;

// Token: 0x0200010C RID: 268
public class CollectionDraggableCardVisual : MonoBehaviour
{
	// Token: 0x06001045 RID: 4165 RVA: 0x0005AD88 File Offset: 0x00058F88
	private void Awake()
	{
		this.EnsureScaleConstantsExist();
		base.gameObject.SetActive(false);
		this.LoadDeckTile();
		this.LoadCardBack();
		if (base.gameObject.GetComponent<AudioSource>() == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x0005ADC7 File Offset: 0x00058FC7
	private void OnDestroy()
	{
		DefLoader.DisposableCardDef cardDef = this.m_cardDef;
		if (cardDef != null)
		{
			cardDef.Dispose();
		}
		this.m_cardDef = null;
	}

	// Token: 0x06001047 RID: 4167 RVA: 0x0005ADE4 File Offset: 0x00058FE4
	private void Update()
	{
		if (this.m_deckTileToRemove != null)
		{
			this.m_deckTileToRemove.SetHighlight(false);
		}
		this.m_deckTileToRemove = null;
		if (this.m_activeActor != this.m_deckTile)
		{
			return;
		}
		if (CollectionManager.Get().GetEditedDeck() == null)
		{
			return;
		}
		RaycastHit raycastHit;
		if (!UniversalInputManager.Get().GetInputHitInfo(DeckTrayDeckTileVisual.LAYER.LayerBit(), out raycastHit))
		{
			return;
		}
		DeckTrayDeckTileVisual component = raycastHit.collider.gameObject.GetComponent<DeckTrayDeckTileVisual>();
		if (component == null)
		{
			return;
		}
		if (component == this.m_deckTileToRemove)
		{
			return;
		}
		this.m_deckTileToRemove = component;
	}

	// Token: 0x06001048 RID: 4168 RVA: 0x0005AE83 File Offset: 0x00059083
	public void SetCardBackId(int cardBackId)
	{
		this.m_cardBackId = cardBackId;
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x0005AE8C File Offset: 0x0005908C
	public int GetCardBackId()
	{
		return this.m_cardBackId;
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x0005AE94 File Offset: 0x00059094
	public string GetCardID()
	{
		return this.m_entityDef.GetCardId();
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x0005AEA1 File Offset: 0x000590A1
	public TAG_PREMIUM GetPremium()
	{
		return this.m_premium;
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x0005AEA9 File Offset: 0x000590A9
	public EntityDef GetEntityDef()
	{
		return this.m_entityDef;
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x0005AEB1 File Offset: 0x000590B1
	public CollectionDeckSlot GetSlot()
	{
		return this.m_slot;
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x0005AEB9 File Offset: 0x000590B9
	public void SetSlot(CollectionDeckSlot slot)
	{
		this.m_slot = slot;
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x0005AEC2 File Offset: 0x000590C2
	public CollectionUtils.ViewMode GetVisualType()
	{
		return this.m_visualType;
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x0005AECC File Offset: 0x000590CC
	public bool ChangeActor(Actor actor, CollectionUtils.ViewMode vtype, TAG_PREMIUM premium)
	{
		if (!this.m_actorCacheInit)
		{
			this.m_actorCacheInit = true;
			this.m_actorCache.AddActorLoadedListener(new HandActorCache.ActorLoadedCallback(this.OnCardActorLoaded));
			this.m_actorCache.Initialize();
		}
		if (this.m_actorCache.IsInitializing())
		{
			return false;
		}
		this.m_visualType = vtype;
		if (this.m_visualType != CollectionUtils.ViewMode.CARD_BACKS)
		{
			EntityDef entityDef = actor.GetEntityDef();
			bool flag = entityDef != this.m_entityDef;
			bool flag2 = premium != this.m_premium;
			if (!flag && !flag2)
			{
				return true;
			}
			this.m_entityDef = entityDef;
			this.m_premium = premium;
			this.m_cardActor = this.m_actorCache.GetActor(entityDef, premium);
			if (this.m_cardActor == null)
			{
				return false;
			}
			if (flag || flag2)
			{
				DefLoader.Get().LoadCardDef(this.m_entityDef.GetCardId(), new DefLoader.LoadDefCallback<DefLoader.DisposableCardDef>(this.OnCardDefLoaded), new CardPortraitQuality(1, this.m_premium), null);
			}
			else
			{
				this.InitDeckTileActor();
				this.InitCardActor();
			}
			return true;
		}
		else
		{
			if (actor != null)
			{
				this.m_entityDef = null;
				this.m_premium = TAG_PREMIUM.NORMAL;
				this.m_currentCardBack = actor.GetComponentInChildren<CardBack>();
				this.m_cardActor = this.m_cardBackActor;
				this.m_cardBackActor.SetCardbackUpdateIgnore(true);
				return true;
			}
			return false;
		}
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x0005B008 File Offset: 0x00059208
	public void UpdateVisual(bool isOverDeck)
	{
		Actor activeActor = this.m_activeActor;
		SpellType spellType;
		if (this.m_visualType == CollectionUtils.ViewMode.CARDS)
		{
			if (isOverDeck && this.m_entityDef != null && !this.m_entityDef.IsHeroSkin())
			{
				this.m_activeActor = this.m_deckTile;
				spellType = SpellType.SUMMON_IN;
			}
			else
			{
				this.m_activeActor = this.m_cardActor;
				spellType = SpellType.DEATHREVERSE;
			}
		}
		else
		{
			this.m_activeActor = this.m_cardActor;
			spellType = SpellType.DEATHREVERSE;
			if (this.m_deckTileToRemove != null)
			{
				this.m_deckTileToRemove.SetHighlight(false);
			}
		}
		if (activeActor == this.m_activeActor)
		{
			return;
		}
		if (activeActor != null)
		{
			activeActor.Hide();
			activeActor.gameObject.SetActive(false);
		}
		if (this.m_activeActor == null)
		{
			return;
		}
		this.m_activeActor.gameObject.SetActive(true);
		this.m_activeActor.Show();
		if (this.m_visualType == CollectionUtils.ViewMode.CARD_BACKS && this.m_currentCardBack != null)
		{
			CardBackManager.Get().UpdateCardBack(this.m_activeActor, this.m_currentCardBack);
		}
		Spell spell = this.m_activeActor.GetSpell(spellType);
		if (spell != null)
		{
			spell.ActivateState(SpellStateType.BIRTH);
		}
		if (this.m_entityDef != null && this.m_entityDef.IsHeroSkin())
		{
			CollectionHeroSkin component = this.m_activeActor.gameObject.GetComponent<CollectionHeroSkin>();
			if (component != null)
			{
				component.SetClass(this.m_entityDef.GetClass());
				component.ShowSocketFX();
				if (UniversalInputManager.UsePhoneUI)
				{
					component.ShowName = false;
				}
			}
		}
	}

	// Token: 0x06001052 RID: 4178 RVA: 0x0005B183 File Offset: 0x00059383
	public bool IsShown()
	{
		return !(base.gameObject == null) && base.gameObject.activeSelf;
	}

	// Token: 0x06001053 RID: 4179 RVA: 0x0005B1A0 File Offset: 0x000593A0
	public void Show(bool isOverDeck)
	{
		base.gameObject.SetActive(true);
		this.UpdateVisual(isOverDeck);
		if (this.m_deckTile != null && this.m_entityDef != null)
		{
			this.m_deckTile.UpdateDeckCardProperties(this.m_entityDef.IsElite(), false, 1, false);
		}
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x0005B1F0 File Offset: 0x000593F0
	public void Hide()
	{
		if (this.m_activeActor != null && this.m_entityDef != null && this.m_entityDef.IsHeroSkin())
		{
			CollectionHeroSkin component = this.m_activeActor.gameObject.GetComponent<CollectionHeroSkin>();
			if (component != null)
			{
				component.HideSocketFX();
			}
		}
		base.gameObject.SetActive(false);
		if (this.m_activeActor != null)
		{
			this.m_activeActor.Hide();
			this.m_activeActor.gameObject.SetActive(false);
			this.m_activeActor = null;
		}
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0005B27D File Offset: 0x0005947D
	public DeckTrayDeckTileVisual GetDeckTileToRemove()
	{
		return this.m_deckTileToRemove;
	}

	// Token: 0x06001056 RID: 4182 RVA: 0x0005B288 File Offset: 0x00059488
	private void LoadDeckTile()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291", AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarning(string.Format("CollectionDraggableCardVisual.OnDeckTileActorLoaded() - FAILED to load actor \"{0}\"", "DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291"));
			return;
		}
		this.m_deckTile = gameObject.GetComponent<CollectionDeckTileActor>();
		if (this.m_deckTile == null)
		{
			Debug.LogWarning(string.Format("CollectionDraggableCardVisual.OnDeckTileActorLoaded() - ERROR game object \"{0}\" has no CollectionDeckTileActor component", "DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291"));
			return;
		}
		this.m_deckTile.Hide();
		this.m_deckTile.transform.parent = base.transform;
		this.m_deckTile.transform.localPosition = new Vector3(2.194931f, 0f, 0f);
		this.m_deckTile.transform.localScale = CollectionDraggableCardVisual.DECK_TILE_LOCAL_SCALE;
		this.m_deckTile.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x0005B378 File Offset: 0x00059578
	private void LoadCardBack()
	{
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", AssetLoadingOptions.IgnorePrefabPosition);
		GameUtils.SetParent(gameObject, this, false);
		this.m_cardBackActor = gameObject.GetComponent<Actor>();
		this.m_cardBackActor.transform.localScale = CollectionDraggableCardVisual.CARD_ACTOR_LOCAL_SCALE;
		this.m_cardBackActor.TurnOffCollider();
		this.m_cardBackActor.Hide();
		gameObject.AddComponent<DragRotator>().SetInfo(this.m_CardDragRotatorInfo);
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x0005B3EC File Offset: 0x000595EC
	private void OnCardActorLoaded(string assetName, Actor actor, object callbackData)
	{
		if (actor == null)
		{
			Debug.LogWarning(string.Format("CollectionDraggableCardVisual.OnCardActorLoaded() - FAILED to load {0}", assetName));
			return;
		}
		actor.GetType();
		actor.TurnOffCollider();
		actor.Hide();
		if (base.name == "Card_Hero_Skin")
		{
			actor.transform.localScale = CollectionDraggableCardVisual.HERO_SKIN_ACTOR_LOCAL_SCALE;
		}
		else
		{
			actor.transform.localScale = CollectionDraggableCardVisual.CARD_ACTOR_LOCAL_SCALE;
		}
		actor.transform.parent = base.transform;
		actor.transform.localPosition = Vector3.zero;
		actor.gameObject.AddComponent<DragRotator>().SetInfo(this.m_CardDragRotatorInfo);
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x0005B494 File Offset: 0x00059694
	private void OnCardDefLoaded(string cardID, DefLoader.DisposableCardDef cardDef, object callbackData)
	{
		if (this.m_entityDef == null || this.m_entityDef.GetCardId() != cardID)
		{
			if (cardDef != null)
			{
				cardDef.Dispose();
			}
			return;
		}
		DefLoader.DisposableCardDef cardDef2 = this.m_cardDef;
		if (cardDef2 != null)
		{
			cardDef2.Dispose();
		}
		this.m_cardDef = cardDef;
		this.InitDeckTileActor();
		this.InitCardActor();
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x0005B4EC File Offset: 0x000596EC
	private void InitDeckTileActor()
	{
		this.InitActor(this.m_deckTile);
		this.m_deckTile.SetSlot(null);
		this.m_deckTile.SetCardDef(this.m_cardDef);
		this.m_deckTile.UpdateAllComponents();
		this.m_deckTile.UpdateMaterial(this.m_cardDef.CardDef.GetDeckCardBarPortrait());
		this.m_deckTile.UpdateDeckCardProperties(this.m_entityDef.IsElite(), false, 1, false);
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x0005B561 File Offset: 0x00059761
	private void InitCardActor()
	{
		this.InitActor(this.m_cardActor);
		this.m_cardActor.transform.localRotation = Quaternion.identity;
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x0005B584 File Offset: 0x00059784
	private void InitActor(Actor actor)
	{
		actor.SetEntityDef(this.m_entityDef);
		actor.SetCardDef(this.m_cardDef);
		actor.SetPremium(this.m_premium);
		actor.UpdateAllComponents();
	}

	// Token: 0x0600105D RID: 4189 RVA: 0x0005B5B0 File Offset: 0x000597B0
	private void EnsureScaleConstantsExist()
	{
		if (!CollectionDraggableCardVisual.s_scaleIsSetup)
		{
			CollectionDraggableCardVisual.s_scaleIsSetup = true;
			CollectionDraggableCardVisual.DECK_TILE_LOCAL_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(0.6f, 0.6f, 0.6f),
				Phone = new Vector3(0.9f, 0.9f, 0.9f)
			};
			CollectionDraggableCardVisual.CARD_ACTOR_LOCAL_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(6f, 6f, 6f),
				Phone = new Vector3(6.9f, 6.9f, 6.9f)
			};
			CollectionDraggableCardVisual.HERO_SKIN_ACTOR_LOCAL_SCALE = new PlatformDependentValue<Vector3>(PlatformCategory.Screen)
			{
				PC = new Vector3(7.5f, 7.5f, 7.5f),
				Phone = new Vector3(8.2f, 8.2f, 8.2f)
			};
		}
	}

	// Token: 0x04000ADC RID: 2780
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

	// Token: 0x04000ADD RID: 2781
	private static Vector3 DECK_TILE_LOCAL_SCALE;

	// Token: 0x04000ADE RID: 2782
	private static Vector3 CARD_ACTOR_LOCAL_SCALE;

	// Token: 0x04000ADF RID: 2783
	private static Vector3 HERO_SKIN_ACTOR_LOCAL_SCALE;

	// Token: 0x04000AE0 RID: 2784
	private static bool s_scaleIsSetup;

	// Token: 0x04000AE1 RID: 2785
	private CollectionDeckSlot m_slot;

	// Token: 0x04000AE2 RID: 2786
	private DeckTrayDeckTileVisual m_deckTileToRemove;

	// Token: 0x04000AE3 RID: 2787
	private Actor m_cardBackActor;

	// Token: 0x04000AE4 RID: 2788
	private CardBack m_currentCardBack;

	// Token: 0x04000AE5 RID: 2789
	private EntityDef m_entityDef;

	// Token: 0x04000AE6 RID: 2790
	private TAG_PREMIUM m_premium;

	// Token: 0x04000AE7 RID: 2791
	private DefLoader.DisposableCardDef m_cardDef;

	// Token: 0x04000AE8 RID: 2792
	private Actor m_activeActor;

	// Token: 0x04000AE9 RID: 2793
	private CollectionDeckTileActor m_deckTile;

	// Token: 0x04000AEA RID: 2794
	private Actor m_cardActor;

	// Token: 0x04000AEB RID: 2795
	private HandActorCache m_actorCache = new HandActorCache();

	// Token: 0x04000AEC RID: 2796
	private bool m_actorCacheInit;

	// Token: 0x04000AED RID: 2797
	private CollectionUtils.ViewMode m_visualType;

	// Token: 0x04000AEE RID: 2798
	private int m_cardBackId;
}
