using System;
using UnityEngine;

// Token: 0x02000891 RID: 2193
public class DeckTrayDeckTileVisual : PegUIElement
{
	// Token: 0x0600784F RID: 30799 RVA: 0x002741F8 File Offset: 0x002723F8
	public void Initialize(bool useFullScaleDeckTileActor)
	{
		string text = useFullScaleDeckTileActor ? "DeckCardBar.prefab:c2bab6eea6c3a3a4d90dcd7572075291" : "DeckCardBar_phone.prefab:bd1c5e767f791984e851553bc5cb3b07";
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject == null)
		{
			Debug.LogWarning(string.Format("DeckTrayDeckTileVisual.OnDeckTileActorLoaded() - FAILED to load actor \"{0}\"", text));
			return;
		}
		this.m_actor = gameObject.GetComponent<CollectionDeckTileActor>();
		if (this.m_actor == null)
		{
			Debug.LogWarning(string.Format("DeckTrayDeckTileVisual.OnDeckTileActorLoaded() - ERROR game object \"{0}\" has no CollectionDeckTileActor component", text));
			return;
		}
		GameUtils.SetParent(this.m_actor, this, false);
		this.m_actor.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		UIBScrollableItem component = this.m_actor.GetComponent<UIBScrollableItem>();
		if (component != null)
		{
			component.SetCustomActiveState(new UIBScrollableItem.ActiveStateCallback(this.IsInUse));
		}
		this.SetUpActor();
		if (base.gameObject.GetComponent<BoxCollider>() == null)
		{
			this.m_collider = base.gameObject.AddComponent<BoxCollider>();
			this.m_collider.size = this.BOX_COLLIDER_SIZE;
			this.m_collider.center = this.BOX_COLLIDER_CENTER;
		}
		this.Hide();
		SceneUtils.SetLayer(base.gameObject, DeckTrayDeckTileVisual.LAYER);
		base.SetDragTolerance(5f);
	}

	// Token: 0x06007850 RID: 30800 RVA: 0x00274331 File Offset: 0x00272531
	public string GetCardID()
	{
		return this.m_actor.GetEntityDef().GetCardId();
	}

	// Token: 0x06007851 RID: 30801 RVA: 0x00274343 File Offset: 0x00272543
	public TAG_PREMIUM GetPremium()
	{
		return this.m_actor.GetPremium();
	}

	// Token: 0x06007852 RID: 30802 RVA: 0x00274350 File Offset: 0x00272550
	public CollectionDeckSlot GetSlot()
	{
		return this.m_slot;
	}

	// Token: 0x06007853 RID: 30803 RVA: 0x00274358 File Offset: 0x00272558
	public void SetSlot(CollectionDeck deck, CollectionDeckSlot s, bool useSliderAnimations)
	{
		this.m_deck = deck;
		this.m_slot = s;
		this.m_useSliderAnimations = useSliderAnimations;
		this.SetUpActor();
	}

	// Token: 0x06007854 RID: 30804 RVA: 0x00274375 File Offset: 0x00272575
	public CollectionDeckTileActor GetActor()
	{
		return this.m_actor;
	}

	// Token: 0x06007855 RID: 30805 RVA: 0x0027437D File Offset: 0x0027257D
	public Bounds GetBounds()
	{
		return this.m_collider.bounds;
	}

	// Token: 0x06007856 RID: 30806 RVA: 0x00028159 File Offset: 0x00026359
	public void Show()
	{
		base.gameObject.SetActive(true);
	}

	// Token: 0x06007857 RID: 30807 RVA: 0x0027438A File Offset: 0x0027258A
	public void ShowAndSetupActor()
	{
		this.Show();
		this.SetUpActor();
	}

	// Token: 0x06007858 RID: 30808 RVA: 0x00028167 File Offset: 0x00026367
	public void Hide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06007859 RID: 30809 RVA: 0x00274398 File Offset: 0x00272598
	public void MarkAsUsed()
	{
		this.m_isInUse = true;
	}

	// Token: 0x0600785A RID: 30810 RVA: 0x002743A1 File Offset: 0x002725A1
	public void MarkAsUnused()
	{
		this.m_isInUse = false;
		if (this.m_actor == null)
		{
			return;
		}
		this.m_actor.UpdateDeckCardProperties(CollectionDeckTileActor.TileIconState.CARD_COUNT, 1, false);
	}

	// Token: 0x0600785B RID: 30811 RVA: 0x002743C7 File Offset: 0x002725C7
	public bool IsInUse()
	{
		return this.m_isInUse;
	}

	// Token: 0x0600785C RID: 30812 RVA: 0x002743CF File Offset: 0x002725CF
	public void SetInArena(bool inArena)
	{
		this.m_inArena = inArena;
	}

	// Token: 0x0600785D RID: 30813 RVA: 0x002743D8 File Offset: 0x002725D8
	public void SetHighlight(bool highlight)
	{
		if (this.m_actor.m_highlight != null)
		{
			this.m_actor.m_highlight.SetActive(highlight);
		}
		if (this.m_actor.m_highlightGlow != null)
		{
			if (this.GetGhostedState() == CollectionDeckTileActor.GhostedState.RED)
			{
				this.m_actor.m_highlightGlow.SetActive(highlight);
				return;
			}
			this.m_actor.m_highlightGlow.SetActive(false);
		}
	}

	// Token: 0x0600785E RID: 30814 RVA: 0x00274448 File Offset: 0x00272648
	public void UpdateGhostedState()
	{
		this.m_actor.SetGhosted(this.GetGhostedState());
		this.m_actor.UpdateGhostTileEffect();
	}

	// Token: 0x0600785F RID: 30815 RVA: 0x00274468 File Offset: 0x00272668
	private CollectionDeckTileActor.GhostedState GetGhostedState()
	{
		if (this.m_deck != null)
		{
			CollectionDeck.SlotStatus slotStatus = this.m_deck.GetSlotStatus(this.m_slot);
			if (slotStatus == CollectionDeck.SlotStatus.NOT_VALID)
			{
				return CollectionDeckTileActor.GhostedState.RED;
			}
			if (slotStatus == CollectionDeck.SlotStatus.MISSING)
			{
				return CollectionDeckTileActor.GhostedState.BLUE;
			}
		}
		return CollectionDeckTileActor.GhostedState.NONE;
	}

	// Token: 0x06007860 RID: 30816 RVA: 0x0027449C File Offset: 0x0027269C
	private void SetUpActor()
	{
		if (this.m_actor == null || this.m_slot == null || string.IsNullOrEmpty(this.m_slot.CardID))
		{
			return;
		}
		this.m_actor.GetEntityDef();
		EntityDef entityDef = this.m_slot.GetEntityDef();
		this.m_actor.SetSlot(this.m_slot);
		TAG_PREMIUM tag_PREMIUM = this.m_slot.PreferredPremium;
		if (this.m_inArena && Options.Get().GetBool(Option.HAS_DISABLED_PREMIUMS_THIS_DRAFT))
		{
			tag_PREMIUM = TAG_PREMIUM.NORMAL;
		}
		this.m_actor.SetPremium(tag_PREMIUM);
		this.m_actor.SetEntityDef(entityDef);
		this.m_actor.SetGhosted(this.GetGhostedState());
		bool flag = entityDef != null && entityDef.IsElite();
		if (flag && this.m_inArena && this.m_slot.Count > 1)
		{
			flag = false;
		}
		this.m_actor.UpdateDeckCardProperties(flag, false, this.m_slot.Count, this.m_useSliderAnimations);
		DefLoader.Get().LoadCardDef(entityDef.GetCardId(), delegate(string cardID, DefLoader.DisposableCardDef cardDef, object data)
		{
			try
			{
				if (!(this.m_actor == null) && cardID.Equals(this.m_actor.GetEntityDef().GetCardId()))
				{
					this.m_actor.SetCardDef(cardDef);
					this.m_actor.UpdateAllComponents();
					this.m_actor.UpdateMaterial(cardDef.CardDef.GetDeckCardBarPortrait());
					this.m_actor.UpdateGhostTileEffect();
				}
			}
			finally
			{
				if (cardDef != null)
				{
					((IDisposable)cardDef).Dispose();
				}
			}
		}, null, new CardPortraitQuality(1, tag_PREMIUM));
	}

	// Token: 0x04005DF0 RID: 24048
	public static readonly GameLayer LAYER = GameLayer.CardRaycast;

	// Token: 0x04005DF1 RID: 24049
	private readonly Vector3 BOX_COLLIDER_SIZE = new Vector3(25.34f, 2.14f, 3.68f);

	// Token: 0x04005DF2 RID: 24050
	private readonly Vector3 BOX_COLLIDER_CENTER = new Vector3(-1.4f, 0f, 0f);

	// Token: 0x04005DF3 RID: 24051
	protected const int DEFAULT_PORTRAIT_QUALITY = 1;

	// Token: 0x04005DF4 RID: 24052
	protected CollectionDeck m_deck;

	// Token: 0x04005DF5 RID: 24053
	protected CollectionDeckSlot m_slot;

	// Token: 0x04005DF6 RID: 24054
	protected BoxCollider m_collider;

	// Token: 0x04005DF7 RID: 24055
	protected CollectionDeckTileActor m_actor;

	// Token: 0x04005DF8 RID: 24056
	protected bool m_isInUse;

	// Token: 0x04005DF9 RID: 24057
	protected bool m_useSliderAnimations;

	// Token: 0x04005DFA RID: 24058
	protected bool m_inArena;
}
