using System;
using Blizzard.Telemetry.WTCG.Client;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020000FF RID: 255
[CustomEditClass]
public class CollectionCardVisual : PegUIElement
{
	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00051BB4 File Offset: 0x0004FDB4
	public string CardId
	{
		get
		{
			if (this.m_actors == null)
			{
				return string.Empty;
			}
			Actor preferredActor = this.m_actors.GetPreferredActor();
			if (preferredActor == null)
			{
				return string.Empty;
			}
			EntityDef entityDef = preferredActor.GetEntityDef();
			if (entityDef == null)
			{
				return string.Empty;
			}
			return entityDef.GetCardId();
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00051C00 File Offset: 0x0004FE00
	public TAG_PREMIUM Premium
	{
		get
		{
			if (this.m_actors == null)
			{
				return TAG_PREMIUM.NORMAL;
			}
			Actor preferredActor = this.m_actors.GetPreferredActor();
			if (preferredActor == null)
			{
				return TAG_PREMIUM.NORMAL;
			}
			return preferredActor.GetPremium();
		}
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x00051C34 File Offset: 0x0004FE34
	protected override void Awake()
	{
		base.Awake();
		if (base.gameObject.GetComponent<AudioSource>() == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		base.SetDragTolerance(5f);
		SoundManager.Get().Load("collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef");
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x00051C86 File Offset: 0x0004FE86
	public bool IsShown()
	{
		return this.m_shown;
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x00051C8E File Offset: 0x0004FE8E
	public void ShowLock(CollectionCardVisual.LockType type)
	{
		this.ShowLock(type, null, false);
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x00051C9C File Offset: 0x0004FE9C
	public void ShowLock(CollectionCardVisual.LockType lockType, string reason, bool playSound)
	{
		CollectionCardVisual.LockType lockType2 = this.m_lockType;
		this.m_lockType = lockType;
		this.UpdateCardCountVisibility();
		if (this.m_actors == null)
		{
			return;
		}
		Actor preferredActor = this.m_actors.GetPreferredActor();
		if (this.m_cardLock != null)
		{
			if (this.m_actors != null)
			{
				this.m_cardLock.UpdateLockVisual(preferredActor.GetEntityDef(), lockType, reason);
			}
			else
			{
				this.m_cardLock.UpdateLockVisual(null, CollectionCardVisual.LockType.NONE, null);
			}
		}
		if (playSound)
		{
			if (this.m_lockType == CollectionCardVisual.LockType.NONE && lockType2 != CollectionCardVisual.LockType.NONE)
			{
				SoundManager.Get().LoadAndPlay("card_limit_unlock.prefab:83ffc974654bdd84f84ecbbaf7ba8e5e");
			}
			if (this.m_lockType != CollectionCardVisual.LockType.NONE && lockType2 == CollectionCardVisual.LockType.NONE)
			{
				SoundManager.Get().LoadAndPlay("card_limit_lock.prefab:68e3525ae3fa8634ab19fde893d7e15b");
			}
		}
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x00051D4C File Offset: 0x0004FF4C
	public void OnDoneCrafting()
	{
		this.UpdateCardCount();
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x00051D54 File Offset: 0x0004FF54
	private void HidePreferredActorIfNecessary(CollectionCardActors actors)
	{
		if (actors != null)
		{
			Actor preferredActor = actors.GetPreferredActor();
			if (preferredActor != null && preferredActor.transform.parent == base.transform)
			{
				if (preferredActor.GetEntityDef() != null)
				{
					preferredActor.ReleaseCardDef();
				}
				preferredActor.Hide();
			}
		}
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x00051DA0 File Offset: 0x0004FFA0
	public void SetActors(CollectionCardActors actors, CollectionUtils.ViewMode type = CollectionUtils.ViewMode.CARDS)
	{
		this.HidePreferredActorIfNecessary(this.m_actors);
		this.m_actors = actors;
		this.UpdateCardCount();
		this.m_visualType = type;
		if (actors == null)
		{
			return;
		}
		Actor preferredActor = this.m_actors.GetPreferredActor();
		this.HidePreferredActorIfNecessary(this.m_actors);
		if (preferredActor == null)
		{
			return;
		}
		GameUtils.SetParent(preferredActor, this, false);
		ActorStateType activeStateType = preferredActor.GetActorStateMgr().GetActiveStateType();
		this.ShowNewCardCallout(activeStateType == ActorStateType.CARD_RECENTLY_ACQUIRED);
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x00051E12 File Offset: 0x00050012
	public Actor GetActor()
	{
		if (this.m_actors == null)
		{
			return null;
		}
		return this.m_actors.GetPreferredActor();
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x00051E29 File Offset: 0x00050029
	public CollectionCardActors GetCollectionCardActors()
	{
		return this.m_actors;
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x00051E31 File Offset: 0x00050031
	public CollectionUtils.ViewMode GetVisualType()
	{
		return this.m_visualType;
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x00051E39 File Offset: 0x00050039
	public void SetCMRow(int rowNum)
	{
		this.m_cmRow = rowNum;
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x00051E42 File Offset: 0x00050042
	public int GetCMRow()
	{
		return this.m_cmRow;
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x00051E4C File Offset: 0x0005004C
	public static void ShowActorShadow(Actor actor, bool show)
	{
		string tag = "FakeShadow";
		string tag2 = "FakeShadowUnique";
		GameObject gameObject = SceneUtils.FindChildByTag(actor.gameObject, tag, false);
		GameObject gameObject2 = SceneUtils.FindChildByTag(actor.gameObject, tag2, false);
		EntityDef entityDef = actor.GetEntityDef();
		if (entityDef != null && show)
		{
			if (entityDef.IsElite())
			{
				if (gameObject != null)
				{
					gameObject.GetComponent<Renderer>().enabled = false;
				}
				if (gameObject2 != null)
				{
					gameObject2.GetComponent<Renderer>().enabled = true;
					return;
				}
			}
			else
			{
				if (gameObject != null)
				{
					gameObject.GetComponent<Renderer>().enabled = true;
				}
				if (gameObject2 != null)
				{
					gameObject2.GetComponent<Renderer>().enabled = false;
					return;
				}
			}
		}
		else
		{
			if (gameObject != null)
			{
				gameObject.GetComponent<Renderer>().enabled = false;
			}
			if (gameObject2 != null)
			{
				gameObject2.GetComponent<Renderer>().enabled = false;
			}
		}
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x00051F1C File Offset: 0x0005011C
	public void Show()
	{
		this.m_shown = true;
		this.SetEnabled(true, false);
		base.GetComponent<Collider>().enabled = true;
		if (this.m_actors == null)
		{
			return;
		}
		Actor preferredActor = this.m_actors.GetPreferredActor();
		if (preferredActor == null || preferredActor.GetEntityDef() == null)
		{
			return;
		}
		bool flag = false;
		if (this.m_visualType == CollectionUtils.ViewMode.CARDS)
		{
			string cardId = preferredActor.GetEntityDef().GetCardId();
			TAG_PREMIUM premium = preferredActor.GetPremium();
			flag = CollectionManager.Get().GetCollectibleDisplay().ShouldShowNewCardGlow(cardId, premium);
			if (premium == TAG_PREMIUM.DIAMOND)
			{
				this.SetDiamondCardTransform();
				this.m_isDiamond = true;
			}
		}
		this.ShowNewCardCallout(flag);
		preferredActor.Show();
		ActorStateType actorState = flag ? ActorStateType.CARD_RECENTLY_ACQUIRED : ActorStateType.CARD_IDLE;
		preferredActor.SetActorState(actorState);
		Renderer[] componentsInChildren = preferredActor.gameObject.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Renderer[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].shadowCastingMode = ShadowCastingMode.Off;
			}
		}
		EntityDef entityDef = preferredActor.GetEntityDef();
		bool show = CollectionManager.Get().IsCardInCollection(entityDef.GetCardId(), preferredActor.GetPremium());
		CollectionCardVisual.ShowActorShadow(preferredActor, show);
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x00052028 File Offset: 0x00050228
	public void Hide()
	{
		this.m_shown = false;
		if (this.m_isDiamond)
		{
			this.SetOriginalCardTransform();
			this.m_isDiamond = false;
		}
		this.SetEnabled(false, false);
		base.GetComponent<Collider>().enabled = false;
		this.ShowLock(CollectionCardVisual.LockType.NONE);
		this.ShowNewCardCallout(false);
		if (this.m_count != null)
		{
			this.m_count.Hide();
		}
		if (this.m_actors == null)
		{
			return;
		}
		Actor preferredActor = this.m_actors.GetPreferredActor();
		if (preferredActor != null)
		{
			preferredActor.Hide();
		}
		UberText componentInChildren = base.GetComponentInChildren<UberText>();
		if (componentInChildren != null)
		{
			componentInChildren.Hide();
		}
		PegUI.Get().RemoveAsMouseDownElement(this);
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x000520D1 File Offset: 0x000502D1
	public void SetHeroSkinBoxCollider()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		component.center = this.m_heroSkinBoxColliderCenter;
		component.size = this.m_heroSkinBoxColliderSize;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x000520F0 File Offset: 0x000502F0
	public void SetDefaultBoxCollider()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		component.center = this.m_boxColliderCenter;
		component.size = this.m_boxColliderSize;
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x00052110 File Offset: 0x00050310
	private void SetDiamondCardTransform()
	{
		this.m_originalScale = base.gameObject.transform.localScale;
		base.gameObject.transform.localScale = this.m_diamondScale;
		base.gameObject.transform.localPosition -= this.m_diamondPositionOffset;
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x00052174 File Offset: 0x00050374
	private void SetOriginalCardTransform()
	{
		base.gameObject.transform.localPosition += this.m_diamondPositionOffset;
		base.gameObject.transform.localScale = this.m_originalScale;
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x000521B4 File Offset: 0x000503B4
	private bool CheckCardSeen()
	{
		if (this.m_actors == null)
		{
			return false;
		}
		Actor preferredActor = this.m_actors.GetPreferredActor();
		bool flag = preferredActor.GetActorStateMgr().GetActiveStateType() == ActorStateType.CARD_RECENTLY_ACQUIRED;
		if (flag)
		{
			EntityDef entityDef = preferredActor.GetEntityDef();
			if (entityDef != null)
			{
				CollectionManager.Get().MarkAllInstancesAsSeen(entityDef.GetCardId(), preferredActor.GetPremium());
			}
		}
		return flag;
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x0005220C File Offset: 0x0005040C
	protected override void OnOver(PegUIElement.InteractionState oldState)
	{
		if (this.ShouldIgnoreAllInput())
		{
			return;
		}
		if (this.m_actors == null)
		{
			return;
		}
		Actor preferredActor = this.m_actors.GetPreferredActor();
		EntityDef entityDef = preferredActor.GetEntityDef();
		if (entityDef != null)
		{
			TooltipPanelManager.Orientation orientation = (this.m_cmRow > 0) ? TooltipPanelManager.Orientation.RightBottom : TooltipPanelManager.Orientation.RightTop;
			TooltipPanelManager.Get().UpdateKeywordHelpForCollectionManager(entityDef, preferredActor, orientation);
		}
		SoundManager.Get().LoadAndPlay("collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c", base.gameObject);
		if (!this.IsInCollection(preferredActor.GetPremium()))
		{
			return;
		}
		ActorStateType actorState = ActorStateType.CARD_MOUSE_OVER;
		if (this.CheckCardSeen())
		{
			actorState = ActorStateType.CARD_RECENTLY_ACQUIRED_MOUSE_OVER;
		}
		preferredActor.SetActorState(actorState);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x0005229C File Offset: 0x0005049C
	protected override void OnOut(PegUIElement.InteractionState oldState)
	{
		TooltipPanelManager.Get().HideKeywordHelp();
		if (this.ShouldIgnoreAllInput())
		{
			return;
		}
		if (this.m_actors == null)
		{
			return;
		}
		Actor preferredActor = this.m_actors.GetPreferredActor();
		if (!this.IsInCollection(preferredActor.GetPremium()))
		{
			return;
		}
		this.CheckCardSeen();
		preferredActor.SetActorState(ActorStateType.CARD_IDLE);
		this.ShowNewCardCallout(false);
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x000522F5 File Offset: 0x000504F5
	protected override void OnDrag()
	{
		if (!this.CanPickUpCard())
		{
			return;
		}
		CollectionInputMgr.Get().GrabCard(this);
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x0005230C File Offset: 0x0005050C
	protected override void OnRelease()
	{
		if (this.IsTransactionPendingOnThisCard())
		{
			return;
		}
		if (CollectionInputMgr.Get().HasHeldCard())
		{
			return;
		}
		Actor preferredActor = this.m_actors.GetPreferredActor();
		if (UniversalInputManager.Get().IsTouchMode() || (CraftingTray.Get() != null && CraftingTray.Get().IsShown()))
		{
			this.CheckCardSeen();
			this.ShowNewCardCallout(false);
			preferredActor.SetActorState(ActorStateType.CARD_IDLE);
			this.EnterCraftingMode();
			return;
		}
		Spell spell = preferredActor.GetSpell(SpellType.DEATHREVERSE);
		if (spell != null)
		{
			spell.gameObject.GetComponentInChildren<ParticleSystem>().main.simulationSpace = ParticleSystemSimulationSpace.Local;
		}
		if (!this.CanPickUpCard())
		{
			this.m_lastClickLeft = true;
			SoundManager.Get().LoadAndPlay("collection_manager_card_move_invalid_or_click.prefab:777caa6f44f027747a03f3d85bcc897c");
			if (spell != null)
			{
				spell.ActivateState(SpellStateType.BIRTH);
			}
			CollectionManager.Get().GetCollectibleDisplay().ShowInnkeeperLClickHelp(preferredActor.GetEntityDef());
			return;
		}
		if (this.m_visualType == CollectionUtils.ViewMode.CARDS)
		{
			EntityDef entityDef = preferredActor.GetEntityDef();
			if (entityDef == null)
			{
				return;
			}
			if (spell != null)
			{
				spell.ActivateState(SpellStateType.BIRTH);
			}
			if (CollectionDeckTray.Get().AddCard(entityDef, preferredActor.GetPremium(), null, false, preferredActor, false))
			{
				CollectionDeckTray.Get().OnCardManuallyAddedByUser_CheckSuggestions(entityDef);
				return;
			}
		}
		else
		{
			if (this.m_visualType == CollectionUtils.ViewMode.CARD_BACKS)
			{
				CollectionDeckTray.Get().SetCardBack(preferredActor);
				return;
			}
			if (this.m_visualType == CollectionUtils.ViewMode.HERO_SKINS)
			{
				CollectionDeckTray.Get().SetHeroSkin(preferredActor);
			}
		}
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x00052460 File Offset: 0x00050660
	protected override void OnRightClick()
	{
		if (this.IsTransactionPendingOnThisCard())
		{
			return;
		}
		if (!Options.Get().GetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, false))
		{
			Options.Get().SetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, true);
		}
		Actor preferredActor = this.m_actors.GetPreferredActor();
		if (this.m_lastClickLeft)
		{
			this.m_lastClickLeft = false;
			this.SendLeftRightClickTelemetry(preferredActor);
		}
		this.ShowNewCardCallout(false);
		preferredActor.SetActorState(ActorStateType.CARD_IDLE);
		this.m_clickedActorTransform = preferredActor.transform;
		this.EnterCraftingMode();
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x000524DC File Offset: 0x000506DC
	private void EnterCraftingMode()
	{
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		if (this.m_visualType != viewMode)
		{
			return;
		}
		switch (viewMode)
		{
		case CollectionUtils.ViewMode.CARDS:
			if (CraftingManager.Get() != null)
			{
				CraftingManager.Get().EnterCraftMode(this.GetActor(), null);
			}
			break;
		case CollectionUtils.ViewMode.HERO_SKINS:
			HeroSkinInfoManager.EnterPreviewWhenReady(this);
			break;
		case CollectionUtils.ViewMode.CARD_BACKS:
			CardBackInfoManager.EnterPreviewWhenReady(this);
			break;
		case CollectionUtils.ViewMode.COINS:
		{
			CoinManager coinManager = CoinManager.Get();
			if (coinManager != null)
			{
				coinManager.ShowCoinPreview(this.CardId, this.m_clickedActorTransform);
			}
			break;
		}
		}
		CollectionDeckTray.Get().CancelRenamingDeck();
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x0005257C File Offset: 0x0005077C
	private bool IsTransactionPendingOnThisCard()
	{
		if (this.m_actors == null)
		{
			return false;
		}
		Actor preferredActor = this.m_actors.GetPreferredActor();
		CraftingManager craftingManager = CraftingManager.Get();
		if (craftingManager == null)
		{
			return false;
		}
		PendingTransaction pendingServerTransaction = craftingManager.GetPendingServerTransaction();
		if (pendingServerTransaction == null)
		{
			return false;
		}
		EntityDef entityDef = preferredActor.GetEntityDef();
		return entityDef != null && !(pendingServerTransaction.CardID != entityDef.GetCardId()) && pendingServerTransaction.Premium == preferredActor.GetPremium();
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x000525F0 File Offset: 0x000507F0
	private bool ShouldIgnoreAllInput()
	{
		return !this.m_shown || (CollectionInputMgr.Get() != null && CollectionInputMgr.Get().IsDraggingScrollbar()) || (CraftingManager.Get() != null && CraftingManager.Get().IsCardShowing()) || CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ArePagesTurning();
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x00052658 File Offset: 0x00050858
	private bool IsInCollection(TAG_PREMIUM premium)
	{
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.COINS)
		{
			return CoinManager.Get().CoinCardOwned(this.CardId);
		}
		if (this.m_actors != null)
		{
			CollectionCardBack component = this.m_actors.GetPreferredActor().GetComponent<CollectionCardBack>();
			if (component != null && CardBackManager.Get().IsCardBackOwned(component.GetCardBackId()))
			{
				return true;
			}
		}
		int num = 0;
		if (this.m_count != null)
		{
			num = this.m_count.GetCount(premium);
		}
		return num > 0;
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x000526E0 File Offset: 0x000508E0
	private bool IsUnlocked()
	{
		Actor preferredActor = this.m_actors.GetPreferredActor();
		return !RankMgr.Get().IsCardLockedInCurrentLeague(preferredActor.GetEntityDef()) && this.m_lockType == CollectionCardVisual.LockType.NONE;
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x00052718 File Offset: 0x00050918
	private bool CanPickUpCard()
	{
		if (this.ShouldIgnoreAllInput())
		{
			return false;
		}
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != this.m_visualType)
		{
			return false;
		}
		if (!CollectionDeckTray.Get().CanPickupCard())
		{
			return false;
		}
		CollectionUtils.ViewMode visualType = this.m_visualType;
		if (visualType != CollectionUtils.ViewMode.CARDS)
		{
			if (visualType == CollectionUtils.ViewMode.HERO_SKINS)
			{
				if (HeroSkinInfoManager.IsLoadedAndShowingPreview())
				{
					return false;
				}
			}
		}
		else
		{
			if (this.m_actors == null)
			{
				return false;
			}
			Actor preferredActor = this.m_actors.GetPreferredActor();
			if (preferredActor && !this.IsInCollection(preferredActor.GetPremium()))
			{
				return false;
			}
			if (!this.IsUnlocked())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x000527A8 File Offset: 0x000509A8
	public void ShowNewCardCallout(bool show)
	{
		if (this.m_newCardCallout == null)
		{
			return;
		}
		this.m_newCardCallout.SetActive(show);
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x000527C8 File Offset: 0x000509C8
	private void UpdateCardCount()
	{
		int normalCount = 0;
		int goldenCount = 0;
		int diamondCount = 0;
		TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
		if (this.m_actors != null)
		{
			Actor preferredActor = this.m_actors.GetPreferredActor();
			EntityDef entityDef = preferredActor.GetEntityDef();
			if (entityDef != null)
			{
				premium = preferredActor.GetPremium();
				CollectibleCard card = CollectionManager.Get().GetCard(entityDef.GetCardId(), TAG_PREMIUM.NORMAL);
				if (card != null)
				{
					normalCount = card.OwnedCount;
				}
				CollectibleCard card2 = CollectionManager.Get().GetCard(entityDef.GetCardId(), TAG_PREMIUM.GOLDEN);
				if (card2 != null)
				{
					goldenCount = card2.OwnedCount;
				}
				CollectibleCard card3 = CollectionManager.Get().GetCard(entityDef.GetCardId(), TAG_PREMIUM.DIAMOND);
				if (card3 != null)
				{
					diamondCount = card3.OwnedCount;
				}
			}
		}
		if (this.m_count != null)
		{
			this.m_count.SetCount(normalCount, goldenCount, diamondCount, premium);
		}
		this.UpdateCardCountVisibility();
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x00052890 File Offset: 0x00050A90
	private void UpdateCardCountVisibility()
	{
		if (this.m_count != null)
		{
			if ((this.m_lockType == CollectionCardVisual.LockType.NONE || this.m_lockType == CollectionCardVisual.LockType.BANNED) && (this.m_visualType == CollectionUtils.ViewMode.CARDS || this.m_visualType == CollectionUtils.ViewMode.COINS) && this.Premium != TAG_PREMIUM.DIAMOND)
			{
				this.m_count.Show();
				return;
			}
			this.m_count.Hide();
		}
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x000528F0 File Offset: 0x00050AF0
	private void SendLeftRightClickTelemetry(Actor actor)
	{
		CollectionLeftRightClick.Target target_ = CollectionLeftRightClick.Target.CARD;
		EntityDef entityDef = actor.GetEntityDef();
		if (entityDef == null)
		{
			target_ = CollectionLeftRightClick.Target.CARD_BACK;
		}
		else if (entityDef.IsHeroSkin())
		{
			target_ = CollectionLeftRightClick.Target.HERO_SKIN;
		}
		TelemetryManager.Client().SendCollectionLeftRightClick(target_);
	}

	// Token: 0x04000A0E RID: 2574
	public CollectionCardCount m_count;

	// Token: 0x04000A0F RID: 2575
	public CollectionCardLock m_cardLock;

	// Token: 0x04000A10 RID: 2576
	public GameObject m_newCardCallout;

	// Token: 0x04000A11 RID: 2577
	public Vector3 m_boxColliderCenter = new Vector3(0f, 0.14f, 0f);

	// Token: 0x04000A12 RID: 2578
	public Vector3 m_boxColliderSize = new Vector3(2f, 0.21f, 2.7f);

	// Token: 0x04000A13 RID: 2579
	public Vector3 m_heroSkinBoxColliderCenter = new Vector3(0f, 0.14f, -0.58f);

	// Token: 0x04000A14 RID: 2580
	public Vector3 m_heroSkinBoxColliderSize = new Vector3(2f, 0.21f, 2f);

	// Token: 0x04000A15 RID: 2581
	[CustomEditField(Sections = "Diamond")]
	public Vector3_MobileOverride m_diamondScale;

	// Token: 0x04000A16 RID: 2582
	[CustomEditField(Sections = "Diamond")]
	public Vector3_MobileOverride m_diamondPositionOffset;

	// Token: 0x04000A17 RID: 2583
	private const string ADD_CARD_TO_DECK_SOUND = "collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef";

	// Token: 0x04000A18 RID: 2584
	private const string CARD_LIMIT_UNLOCK_SOUND = "card_limit_unlock.prefab:83ffc974654bdd84f84ecbbaf7ba8e5e";

	// Token: 0x04000A19 RID: 2585
	private const string CARD_LIMIT_LOCK_SOUND = "card_limit_lock.prefab:68e3525ae3fa8634ab19fde893d7e15b";

	// Token: 0x04000A1A RID: 2586
	private const string CARD_MOUSE_OVER_SOUND = "collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c";

	// Token: 0x04000A1B RID: 2587
	private const string CARD_MOVE_INVALID_OR_CLICK_SOUND = "collection_manager_card_move_invalid_or_click.prefab:777caa6f44f027747a03f3d85bcc897c";

	// Token: 0x04000A1C RID: 2588
	private CollectionCardActors m_actors;

	// Token: 0x04000A1D RID: 2589
	private CollectionCardVisual.LockType m_lockType;

	// Token: 0x04000A1E RID: 2590
	private bool m_shown;

	// Token: 0x04000A1F RID: 2591
	private CollectionUtils.ViewMode m_visualType;

	// Token: 0x04000A20 RID: 2592
	private int m_cmRow;

	// Token: 0x04000A21 RID: 2593
	private bool m_lastClickLeft;

	// Token: 0x04000A22 RID: 2594
	private Transform m_clickedActorTransform;

	// Token: 0x04000A23 RID: 2595
	private Vector3 m_originalScale;

	// Token: 0x04000A24 RID: 2596
	private bool m_isDiamond;

	// Token: 0x0200141C RID: 5148
	public enum LockType
	{
		// Token: 0x0400A8FE RID: 43262
		NONE,
		// Token: 0x0400A8FF RID: 43263
		MAX_COPIES_IN_DECK,
		// Token: 0x0400A900 RID: 43264
		NO_MORE_INSTANCES,
		// Token: 0x0400A901 RID: 43265
		NOT_PLAYABLE,
		// Token: 0x0400A902 RID: 43266
		BANNED
	}
}
