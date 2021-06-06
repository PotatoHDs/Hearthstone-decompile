using Blizzard.Telemetry.WTCG.Client;
using UnityEngine;
using UnityEngine.Rendering;

[CustomEditClass]
public class CollectionCardVisual : PegUIElement
{
	public enum LockType
	{
		NONE,
		MAX_COPIES_IN_DECK,
		NO_MORE_INSTANCES,
		NOT_PLAYABLE,
		BANNED
	}

	public CollectionCardCount m_count;

	public CollectionCardLock m_cardLock;

	public GameObject m_newCardCallout;

	public Vector3 m_boxColliderCenter = new Vector3(0f, 0.14f, 0f);

	public Vector3 m_boxColliderSize = new Vector3(2f, 0.21f, 2.7f);

	public Vector3 m_heroSkinBoxColliderCenter = new Vector3(0f, 0.14f, -0.58f);

	public Vector3 m_heroSkinBoxColliderSize = new Vector3(2f, 0.21f, 2f);

	[CustomEditField(Sections = "Diamond")]
	public Vector3_MobileOverride m_diamondScale;

	[CustomEditField(Sections = "Diamond")]
	public Vector3_MobileOverride m_diamondPositionOffset;

	private const string ADD_CARD_TO_DECK_SOUND = "collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef";

	private const string CARD_LIMIT_UNLOCK_SOUND = "card_limit_unlock.prefab:83ffc974654bdd84f84ecbbaf7ba8e5e";

	private const string CARD_LIMIT_LOCK_SOUND = "card_limit_lock.prefab:68e3525ae3fa8634ab19fde893d7e15b";

	private const string CARD_MOUSE_OVER_SOUND = "collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c";

	private const string CARD_MOVE_INVALID_OR_CLICK_SOUND = "collection_manager_card_move_invalid_or_click.prefab:777caa6f44f027747a03f3d85bcc897c";

	private CollectionCardActors m_actors;

	private LockType m_lockType;

	private bool m_shown;

	private CollectionUtils.ViewMode m_visualType;

	private int m_cmRow;

	private bool m_lastClickLeft;

	private Transform m_clickedActorTransform;

	private Vector3 m_originalScale;

	private bool m_isDiamond;

	public string CardId
	{
		get
		{
			if (m_actors == null)
			{
				return string.Empty;
			}
			Actor preferredActor = m_actors.GetPreferredActor();
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

	public TAG_PREMIUM Premium
	{
		get
		{
			if (m_actors == null)
			{
				return TAG_PREMIUM.NORMAL;
			}
			Actor preferredActor = m_actors.GetPreferredActor();
			if (preferredActor == null)
			{
				return TAG_PREMIUM.NORMAL;
			}
			return preferredActor.GetPremium();
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (base.gameObject.GetComponent<AudioSource>() == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		SetDragTolerance(5f);
		SoundManager.Get().Load("collection_manager_card_add_to_deck_instant.prefab:06df359c4026d7e47b06a4174f33e3ef");
	}

	public bool IsShown()
	{
		return m_shown;
	}

	public void ShowLock(LockType type)
	{
		ShowLock(type, null, playSound: false);
	}

	public void ShowLock(LockType lockType, string reason, bool playSound)
	{
		LockType lockType2 = m_lockType;
		m_lockType = lockType;
		UpdateCardCountVisibility();
		if (m_actors == null)
		{
			return;
		}
		Actor preferredActor = m_actors.GetPreferredActor();
		if (m_cardLock != null)
		{
			if (m_actors != null)
			{
				m_cardLock.UpdateLockVisual(preferredActor.GetEntityDef(), lockType, reason);
			}
			else
			{
				m_cardLock.UpdateLockVisual(null, LockType.NONE, null);
			}
		}
		if (playSound)
		{
			if (m_lockType == LockType.NONE && lockType2 != 0)
			{
				SoundManager.Get().LoadAndPlay("card_limit_unlock.prefab:83ffc974654bdd84f84ecbbaf7ba8e5e");
			}
			if (m_lockType != 0 && lockType2 == LockType.NONE)
			{
				SoundManager.Get().LoadAndPlay("card_limit_lock.prefab:68e3525ae3fa8634ab19fde893d7e15b");
			}
		}
	}

	public void OnDoneCrafting()
	{
		UpdateCardCount();
	}

	private void HidePreferredActorIfNecessary(CollectionCardActors actors)
	{
		if (actors == null)
		{
			return;
		}
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

	public void SetActors(CollectionCardActors actors, CollectionUtils.ViewMode type = CollectionUtils.ViewMode.CARDS)
	{
		HidePreferredActorIfNecessary(m_actors);
		m_actors = actors;
		UpdateCardCount();
		m_visualType = type;
		if (actors != null)
		{
			Actor preferredActor = m_actors.GetPreferredActor();
			HidePreferredActorIfNecessary(m_actors);
			if (!(preferredActor == null))
			{
				GameUtils.SetParent(preferredActor, this);
				ActorStateType activeStateType = preferredActor.GetActorStateMgr().GetActiveStateType();
				ShowNewCardCallout(activeStateType == ActorStateType.CARD_RECENTLY_ACQUIRED);
			}
		}
	}

	public Actor GetActor()
	{
		if (m_actors == null)
		{
			return null;
		}
		return m_actors.GetPreferredActor();
	}

	public CollectionCardActors GetCollectionCardActors()
	{
		return m_actors;
	}

	public CollectionUtils.ViewMode GetVisualType()
	{
		return m_visualType;
	}

	public void SetCMRow(int rowNum)
	{
		m_cmRow = rowNum;
	}

	public int GetCMRow()
	{
		return m_cmRow;
	}

	public static void ShowActorShadow(Actor actor, bool show)
	{
		string text = "FakeShadow";
		string text2 = "FakeShadowUnique";
		GameObject gameObject = SceneUtils.FindChildByTag(actor.gameObject, text);
		GameObject gameObject2 = SceneUtils.FindChildByTag(actor.gameObject, text2);
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

	public void Show()
	{
		m_shown = true;
		SetEnabled(enabled: true);
		GetComponent<Collider>().enabled = true;
		if (m_actors == null)
		{
			return;
		}
		Actor preferredActor = m_actors.GetPreferredActor();
		if (preferredActor == null || preferredActor.GetEntityDef() == null)
		{
			return;
		}
		bool flag = false;
		if (m_visualType == CollectionUtils.ViewMode.CARDS)
		{
			string cardId = preferredActor.GetEntityDef().GetCardId();
			TAG_PREMIUM premium = preferredActor.GetPremium();
			flag = CollectionManager.Get().GetCollectibleDisplay().ShouldShowNewCardGlow(cardId, premium);
			if (premium == TAG_PREMIUM.DIAMOND)
			{
				SetDiamondCardTransform();
				m_isDiamond = true;
			}
		}
		ShowNewCardCallout(flag);
		preferredActor.Show();
		ActorStateType actorState = ((!flag) ? ActorStateType.CARD_IDLE : ActorStateType.CARD_RECENTLY_ACQUIRED);
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
		ShowActorShadow(preferredActor, show);
	}

	public void Hide()
	{
		m_shown = false;
		if (m_isDiamond)
		{
			SetOriginalCardTransform();
			m_isDiamond = false;
		}
		SetEnabled(enabled: false);
		GetComponent<Collider>().enabled = false;
		ShowLock(LockType.NONE);
		ShowNewCardCallout(show: false);
		if (m_count != null)
		{
			m_count.Hide();
		}
		if (m_actors != null)
		{
			Actor preferredActor = m_actors.GetPreferredActor();
			if (preferredActor != null)
			{
				preferredActor.Hide();
			}
			UberText componentInChildren = GetComponentInChildren<UberText>();
			if (componentInChildren != null)
			{
				componentInChildren.Hide();
			}
			PegUI.Get().RemoveAsMouseDownElement(this);
		}
	}

	public void SetHeroSkinBoxCollider()
	{
		BoxCollider component = GetComponent<BoxCollider>();
		component.center = m_heroSkinBoxColliderCenter;
		component.size = m_heroSkinBoxColliderSize;
	}

	public void SetDefaultBoxCollider()
	{
		BoxCollider component = GetComponent<BoxCollider>();
		component.center = m_boxColliderCenter;
		component.size = m_boxColliderSize;
	}

	private void SetDiamondCardTransform()
	{
		m_originalScale = base.gameObject.transform.localScale;
		base.gameObject.transform.localScale = m_diamondScale;
		base.gameObject.transform.localPosition -= (Vector3)m_diamondPositionOffset;
	}

	private void SetOriginalCardTransform()
	{
		base.gameObject.transform.localPosition += (Vector3)m_diamondPositionOffset;
		base.gameObject.transform.localScale = m_originalScale;
	}

	private bool CheckCardSeen()
	{
		if (m_actors == null)
		{
			return false;
		}
		Actor preferredActor = m_actors.GetPreferredActor();
		bool num = preferredActor.GetActorStateMgr().GetActiveStateType() == ActorStateType.CARD_RECENTLY_ACQUIRED;
		if (num)
		{
			EntityDef entityDef = preferredActor.GetEntityDef();
			if (entityDef != null)
			{
				CollectionManager.Get().MarkAllInstancesAsSeen(entityDef.GetCardId(), preferredActor.GetPremium());
			}
		}
		return num;
	}

	protected override void OnOver(InteractionState oldState)
	{
		if (ShouldIgnoreAllInput() || m_actors == null)
		{
			return;
		}
		Actor preferredActor = m_actors.GetPreferredActor();
		EntityDef entityDef = preferredActor.GetEntityDef();
		if (entityDef != null)
		{
			TooltipPanelManager.Orientation orientation = ((m_cmRow > 0) ? TooltipPanelManager.Orientation.RightBottom : TooltipPanelManager.Orientation.RightTop);
			TooltipPanelManager.Get().UpdateKeywordHelpForCollectionManager(entityDef, preferredActor, orientation);
		}
		SoundManager.Get().LoadAndPlay("collection_manager_card_mouse_over.prefab:0d4e20bc78956bc48b5e2963ec39211c", base.gameObject);
		if (IsInCollection(preferredActor.GetPremium()))
		{
			ActorStateType actorState = ActorStateType.CARD_MOUSE_OVER;
			if (CheckCardSeen())
			{
				actorState = ActorStateType.CARD_RECENTLY_ACQUIRED_MOUSE_OVER;
			}
			preferredActor.SetActorState(actorState);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		TooltipPanelManager.Get().HideKeywordHelp();
		if (!ShouldIgnoreAllInput() && m_actors != null)
		{
			Actor preferredActor = m_actors.GetPreferredActor();
			if (IsInCollection(preferredActor.GetPremium()))
			{
				CheckCardSeen();
				preferredActor.SetActorState(ActorStateType.CARD_IDLE);
				ShowNewCardCallout(show: false);
			}
		}
	}

	protected override void OnDrag()
	{
		if (CanPickUpCard())
		{
			CollectionInputMgr.Get().GrabCard(this);
		}
	}

	protected override void OnRelease()
	{
		if (IsTransactionPendingOnThisCard() || CollectionInputMgr.Get().HasHeldCard())
		{
			return;
		}
		Actor preferredActor = m_actors.GetPreferredActor();
		if (UniversalInputManager.Get().IsTouchMode() || (CraftingTray.Get() != null && CraftingTray.Get().IsShown()))
		{
			CheckCardSeen();
			ShowNewCardCallout(show: false);
			preferredActor.SetActorState(ActorStateType.CARD_IDLE);
			EnterCraftingMode();
			return;
		}
		Spell spell = preferredActor.GetSpell(SpellType.DEATHREVERSE);
		if (spell != null)
		{
			ParticleSystem.MainModule main = spell.gameObject.GetComponentInChildren<ParticleSystem>().main;
			main.simulationSpace = ParticleSystemSimulationSpace.Local;
		}
		if (!CanPickUpCard())
		{
			m_lastClickLeft = true;
			SoundManager.Get().LoadAndPlay("collection_manager_card_move_invalid_or_click.prefab:777caa6f44f027747a03f3d85bcc897c");
			if (spell != null)
			{
				spell.ActivateState(SpellStateType.BIRTH);
			}
			CollectionManager.Get().GetCollectibleDisplay().ShowInnkeeperLClickHelp(preferredActor.GetEntityDef());
		}
		else if (m_visualType == CollectionUtils.ViewMode.CARDS)
		{
			EntityDef entityDef = preferredActor.GetEntityDef();
			if (entityDef != null)
			{
				if (spell != null)
				{
					spell.ActivateState(SpellStateType.BIRTH);
				}
				if (CollectionDeckTray.Get().AddCard(entityDef, preferredActor.GetPremium(), null, playSound: false, preferredActor))
				{
					CollectionDeckTray.Get().OnCardManuallyAddedByUser_CheckSuggestions(entityDef);
				}
			}
		}
		else if (m_visualType == CollectionUtils.ViewMode.CARD_BACKS)
		{
			CollectionDeckTray.Get().SetCardBack(preferredActor);
		}
		else if (m_visualType == CollectionUtils.ViewMode.HERO_SKINS)
		{
			CollectionDeckTray.Get().SetHeroSkin(preferredActor);
		}
	}

	protected override void OnRightClick()
	{
		if (!IsTransactionPendingOnThisCard())
		{
			if (!Options.Get().GetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, defaultVal: false))
			{
				Options.Get().SetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, val: true);
			}
			Actor preferredActor = m_actors.GetPreferredActor();
			if (m_lastClickLeft)
			{
				m_lastClickLeft = false;
				SendLeftRightClickTelemetry(preferredActor);
			}
			ShowNewCardCallout(show: false);
			preferredActor.SetActorState(ActorStateType.CARD_IDLE);
			m_clickedActorTransform = preferredActor.transform;
			EnterCraftingMode();
		}
	}

	private void EnterCraftingMode()
	{
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		if (m_visualType != viewMode)
		{
			return;
		}
		switch (viewMode)
		{
		case CollectionUtils.ViewMode.CARDS:
			if (CraftingManager.Get() != null)
			{
				CraftingManager.Get().EnterCraftMode(GetActor());
			}
			break;
		case CollectionUtils.ViewMode.HERO_SKINS:
			HeroSkinInfoManager.EnterPreviewWhenReady(this);
			break;
		case CollectionUtils.ViewMode.CARD_BACKS:
			CardBackInfoManager.EnterPreviewWhenReady(this);
			break;
		case CollectionUtils.ViewMode.COINS:
			CoinManager.Get()?.ShowCoinPreview(CardId, m_clickedActorTransform);
			break;
		}
		CollectionDeckTray.Get().CancelRenamingDeck();
	}

	private bool IsTransactionPendingOnThisCard()
	{
		if (m_actors == null)
		{
			return false;
		}
		Actor preferredActor = m_actors.GetPreferredActor();
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
		if (entityDef == null)
		{
			return false;
		}
		if (pendingServerTransaction.CardID != entityDef.GetCardId())
		{
			return false;
		}
		if (pendingServerTransaction.Premium != preferredActor.GetPremium())
		{
			return false;
		}
		return true;
	}

	private bool ShouldIgnoreAllInput()
	{
		if (!m_shown)
		{
			return true;
		}
		if (CollectionInputMgr.Get() != null && CollectionInputMgr.Get().IsDraggingScrollbar())
		{
			return true;
		}
		if (CraftingManager.Get() != null && CraftingManager.Get().IsCardShowing())
		{
			return true;
		}
		if (CollectionManager.Get().GetCollectibleDisplay().m_pageManager.ArePagesTurning())
		{
			return true;
		}
		return false;
	}

	private bool IsInCollection(TAG_PREMIUM premium)
	{
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.COINS)
		{
			return CoinManager.Get().CoinCardOwned(CardId);
		}
		if (m_actors != null)
		{
			CollectionCardBack component = m_actors.GetPreferredActor().GetComponent<CollectionCardBack>();
			if (component != null && CardBackManager.Get().IsCardBackOwned(component.GetCardBackId()))
			{
				return true;
			}
		}
		int num = 0;
		if (m_count != null)
		{
			num = m_count.GetCount(premium);
		}
		return num > 0;
	}

	private bool IsUnlocked()
	{
		Actor preferredActor = m_actors.GetPreferredActor();
		if (RankMgr.Get().IsCardLockedInCurrentLeague(preferredActor.GetEntityDef()))
		{
			return false;
		}
		return m_lockType == LockType.NONE;
	}

	private bool CanPickUpCard()
	{
		if (ShouldIgnoreAllInput())
		{
			return false;
		}
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != m_visualType)
		{
			return false;
		}
		if (!CollectionDeckTray.Get().CanPickupCard())
		{
			return false;
		}
		switch (m_visualType)
		{
		case CollectionUtils.ViewMode.CARDS:
		{
			if (m_actors == null)
			{
				return false;
			}
			Actor preferredActor = m_actors.GetPreferredActor();
			if ((bool)preferredActor && !IsInCollection(preferredActor.GetPremium()))
			{
				return false;
			}
			if (!IsUnlocked())
			{
				return false;
			}
			break;
		}
		case CollectionUtils.ViewMode.HERO_SKINS:
			if (HeroSkinInfoManager.IsLoadedAndShowingPreview())
			{
				return false;
			}
			break;
		}
		return true;
	}

	public void ShowNewCardCallout(bool show)
	{
		if (!(m_newCardCallout == null))
		{
			m_newCardCallout.SetActive(show);
		}
	}

	private void UpdateCardCount()
	{
		int normalCount = 0;
		int goldenCount = 0;
		int diamondCount = 0;
		TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
		if (m_actors != null)
		{
			Actor preferredActor = m_actors.GetPreferredActor();
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
		if (m_count != null)
		{
			m_count.SetCount(normalCount, goldenCount, diamondCount, premium);
		}
		UpdateCardCountVisibility();
	}

	private void UpdateCardCountVisibility()
	{
		if (m_count != null)
		{
			if ((m_lockType == LockType.NONE || m_lockType == LockType.BANNED) && (m_visualType == CollectionUtils.ViewMode.CARDS || m_visualType == CollectionUtils.ViewMode.COINS) && Premium != TAG_PREMIUM.DIAMOND)
			{
				m_count.Show();
			}
			else
			{
				m_count.Hide();
			}
		}
	}

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
}
