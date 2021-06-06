using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	public delegate bool TooltipContentDelegate(ref string headline, ref string description, int index);

	public class TooltipSettings
	{
		private TooltipContentDelegate m_overrideContentDelegate;

		public bool Allowed { get; private set; }

		public TooltipSettings(bool allowed)
		{
			Allowed = allowed;
			m_overrideContentDelegate = null;
		}

		public TooltipSettings(bool allowed, TooltipContentDelegate contentDelegate)
		{
			Allowed = allowed;
			m_overrideContentDelegate = contentDelegate;
		}

		public bool GetTooltipOverrideContent(ref string headline, ref string description, int index = 0)
		{
			if (m_overrideContentDelegate != null)
			{
				return m_overrideContentDelegate(ref headline, ref description, index);
			}
			return false;
		}
	}

	public class ZoneTooltipSettings
	{
		public TooltipSettings EnemyHand = new TooltipSettings(allowed: true);

		public TooltipSettings EnemyDeck = new TooltipSettings(allowed: true);

		public TooltipSettings EnemyMana = new TooltipSettings(allowed: true);

		public TooltipSettings FriendlyHand = new TooltipSettings(allowed: true);

		public TooltipSettings FriendlyDeck = new TooltipSettings(allowed: true);

		public TooltipSettings FriendlyMana = new TooltipSettings(allowed: true);
	}

	public delegate void PhoneHandShownCallback(object userData);

	private class PhoneHandShownListener : EventListener<PhoneHandShownCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	public delegate void PhoneHandHiddenCallback(object userData);

	private class PhoneHandHiddenListener : EventListener<PhoneHandHiddenCallback>
	{
		public void Fire()
		{
			m_callback(m_userData);
		}
	}

	public float m_MouseOverDelay = 0.4f;

	public DragRotatorInfo m_DragRotatorInfo = new DragRotatorInfo
	{
		m_PitchInfo = new DragRotatorAxisInfo
		{
			m_ForceMultiplier = 25f,
			m_MinDegrees = -40f,
			m_MaxDegrees = 40f,
			m_RestSeconds = 2f
		},
		m_RollInfo = new DragRotatorAxisInfo
		{
			m_ForceMultiplier = 25f,
			m_MinDegrees = -45f,
			m_MaxDegrees = 45f,
			m_RestSeconds = 2f
		}
	};

	private readonly PlatformDependentValue<float> MIN_GRAB_Y = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		Tablet = 80f,
		Phone = 80f
	};

	private const float MOBILE_TARGETTING_Y_OFFSET = 0.8f;

	private const float MOBILE_TARGETTING_XY_SCALE = 1.08f;

	private static InputManager s_instance;

	private ZoneHand m_myHandZone;

	private ZonePlay m_myPlayZone;

	private ZoneWeapon m_myWeaponZone;

	private ZoneHand m_enemyHandZone;

	private ZonePlay m_enemyPlayZone;

	private Card m_heldCard;

	private bool m_checkForInput;

	private GameObject m_lastObjectMousedDown;

	private GameObject m_lastObjectRightMousedDown;

	private Vector3 m_lastMouseDownPosition;

	private bool m_leftMouseButtonIsDown;

	private bool m_dragging;

	private bool m_lastInputDrag;

	private Card m_mousedOverCard;

	private HistoryCard m_mousedOverHistoryCard;

	private GameObject m_mousedOverObject;

	private float m_mousedOverTimer;

	private ZoneChangeList m_lastZoneChangeList;

	private Card m_battlecrySourceCard;

	private List<Card> m_cancelingBattlecryCards = new List<Card>();

	private bool m_cardWasInsideHandLastFrame;

	private bool m_isInBattleCryEffect;

	private List<Entity> m_entitiesThatPredictedMana = new List<Entity>();

	private List<Actor> m_mobileTargettingEffectActors = new List<Actor>();

	private Card m_lastPreviewedCard;

	private bool m_touchDraggingCard;

	private bool m_useHandEnlarge;

	private bool m_hideHandAfterPlayingCard;

	private bool m_targettingHeroPower;

	private bool m_touchedDownOnSmallHand;

	private bool m_enlargeHandAfterDropCard;

	private int m_telemetryNumDragAttacks;

	private int m_telemetryNumClickAttacks;

	private readonly string[] DEFAULT_BOARD_CLICK_SOUNDS = new string[5] { "board_common_dirt_poke_1.prefab:db7d81ea320f3bb4b9fa44bcd371d379", "board_common_dirt_poke_2.prefab:a078131beb0546444b4ccfc41ec5c547", "board_common_dirt_poke_3.prefab:7fbdaca211c05b94382e3142dfdbb306", "board_common_dirt_poke_4.prefab:d2713c07dcb56904da5ce08da04b5d26", "board_common_dirt_poke_5.prefab:c7234b85b15bca047b7ce32dc96bc851" };

	private List<PhoneHandShownListener> m_phoneHandShownListener = new List<PhoneHandShownListener>();

	private List<PhoneHandHiddenListener> m_phoneHandHiddenListener = new List<PhoneHandHiddenListener>();

	public bool LeftMouseButtonDown => m_leftMouseButtonIsDown;

	public Vector3 LastMouseDownPosition => m_lastMouseDownPosition;

	private void Awake()
	{
		s_instance = this;
		m_useHandEnlarge = UniversalInputManager.UsePhoneUI;
		SetDragging(m_dragging);
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterOptionsReceivedListener(OnOptionsReceived);
			GameState.Get().RegisterOptionRejectedListener(OnOptionRejected);
			GameState.Get().RegisterTurnTimerUpdateListener(OnTurnTimerUpdate);
			GameState.Get().RegisterGameOverListener(OnGameOver);
		}
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
	}

	private void OnDestroy()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterOptionsReceivedListener(OnOptionsReceived);
			GameState.Get().UnregisterOptionRejectedListener(OnOptionRejected);
			GameState.Get().UnregisterTurnTimerUpdateListener(OnTurnTimerUpdate);
			GameState.Get().UnregisterGameOverListener(OnGameOver);
		}
		FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		s_instance = null;
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		DisableInput();
	}

	private bool IsInputOverCard(Card wantedCard)
	{
		if (wantedCard == null)
		{
			return false;
		}
		Actor actor = wantedCard.GetActor();
		if (actor == null)
		{
			return false;
		}
		RaycastHit hitInfo;
		if (!actor.IsColliderEnabled())
		{
			actor.ToggleCollider(enabled: true);
			UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out hitInfo);
			actor.ToggleCollider(enabled: false);
		}
		else
		{
			UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out hitInfo);
		}
		if (hitInfo.collider != null)
		{
			Actor actor2 = SceneUtils.FindComponentInParents<Actor>(hitInfo.transform);
			if (actor2 == null)
			{
				return false;
			}
			return actor2.GetCard() == wantedCard;
		}
		return false;
	}

	private bool ShouldCancelTargeting(bool hitBattlefieldHitbox)
	{
		bool result = false;
		if (!hitBattlefieldHitbox && GetBattlecrySourceCard() == null && ChoiceCardMgr.Get().GetSubOptionParentCard() == null)
		{
			result = true;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				bool flag = UniversalInputManager.Get().InputHitAnyObject(Camera.main, GameLayer.InvisibleHitBox3);
				if (m_targettingHeroPower || GameState.Get().IsSelectedOptionFriendlyHero() || flag)
				{
					result = false;
				}
			}
			else if (GameState.Get().IsSelectedOptionFriendlyHero())
			{
				Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
				Card weaponCard = GameState.Get().GetFriendlySidePlayer().GetWeaponCard();
				if (IsInputOverCard(heroCard) || IsInputOverCard(weaponCard))
				{
					result = false;
				}
			}
			else if (GameState.Get().IsSelectedOptionFriendlyHeroPower())
			{
				Card heroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
				if (IsInputOverCard(heroPowerCard))
				{
					result = false;
				}
			}
		}
		return result;
	}

	private void Update()
	{
		if (!m_checkForInput)
		{
			return;
		}
		if (UniversalInputManager.Get().GetMouseButtonDown(0))
		{
			HandleLeftMouseDown();
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			m_touchDraggingCard = false;
			HandleLeftMouseUp();
		}
		if (UniversalInputManager.Get().GetMouseButtonDown(1))
		{
			HandleRightMouseDown();
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(1))
		{
			HandleRightMouseUp();
		}
		HandleMouseMove();
		if (m_leftMouseButtonIsDown && m_heldCard == null)
		{
			HandleUpdateWhileLeftMouseButtonIsDown();
			if (UniversalInputManager.Get().IsTouchMode() && !m_touchDraggingCard)
			{
				HandleUpdateWhileNotHoldingCard();
			}
		}
		else if (m_heldCard == null)
		{
			HandleUpdateWhileNotHoldingCard();
		}
		if (GameState.Get() == null || GameState.Get().GetFriendlySidePlayer() == null || GameState.Get().GetFriendlySidePlayer().IsLocalUser())
		{
			bool flag = UniversalInputManager.Get().InputHitAnyObject(Camera.main, GameLayer.InvisibleHitBox2);
			if ((bool)TargetReticleManager.Get() && TargetReticleManager.Get().IsActive())
			{
				if (ShouldCancelTargeting(flag))
				{
					CancelOption();
					if (m_useHandEnlarge)
					{
						m_myHandZone.SetFriendlyHeroTargetingMode(enable: false);
					}
					if (m_heldCard != null)
					{
						PositionHeldCard();
					}
				}
				else
				{
					TargetReticleManager.Get().UpdateArrowPosition();
					if (m_heldCard != null)
					{
						m_myHandZone.OnCardHeld(m_heldCard);
					}
				}
			}
			else if ((bool)m_heldCard)
			{
				HandleUpdateWhileHoldingCard(flag);
			}
		}
		if (EmoteHandler.Get() != null && EmoteHandler.Get().AreEmotesActive())
		{
			EmoteHandler.Get().HandleInput();
		}
		if (EnemyEmoteHandler.Get() != null && EnemyEmoteHandler.Get().AreEmotesActive())
		{
			EnemyEmoteHandler.Get().HandleInput();
		}
		ShowTooltipIfNecessary();
	}

	public static InputManager Get()
	{
		return s_instance;
	}

	public bool HandleKeyboardInput()
	{
		if (HandleUniversalHotkeys())
		{
			return true;
		}
		if (GameState.Get() != null && GameState.Get().IsMulliganManagerActive())
		{
			return HandleMulliganHotkeys();
		}
		return HandleGameHotkeys();
	}

	public Card GetMousedOverCard()
	{
		return m_mousedOverCard;
	}

	public void SetMousedOverCard(Card card)
	{
		if (!(m_mousedOverCard == card))
		{
			if (m_mousedOverCard != null && !(m_mousedOverCard.GetZone() is ZoneHand))
			{
				HandleMouseOffCard();
			}
			if (card.IsInputEnabled())
			{
				m_mousedOverCard = card;
				card.NotifyMousedOver();
			}
		}
	}

	public Card GetBattlecrySourceCard()
	{
		return m_battlecrySourceCard;
	}

	public void StartWatchingForInput()
	{
		if (m_checkForInput)
		{
			return;
		}
		m_checkForInput = true;
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (zone.m_Side == Player.Side.FRIENDLY)
			{
				if (zone is ZoneHand)
				{
					m_myHandZone = (ZoneHand)zone;
				}
				else if (zone is ZonePlay)
				{
					m_myPlayZone = (ZonePlay)zone;
				}
				else if (zone is ZoneWeapon)
				{
					m_myWeaponZone = (ZoneWeapon)zone;
				}
			}
			else if (zone is ZonePlay)
			{
				m_enemyPlayZone = (ZonePlay)zone;
			}
			else if (zone is ZoneHand)
			{
				m_enemyHandZone = (ZoneHand)zone;
			}
		}
	}

	public void DisableInput()
	{
		m_checkForInput = false;
		HandleMouseOff();
		if ((bool)TargetReticleManager.Get())
		{
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(isLocallyCanceled: false);
		}
	}

	public bool PermitDecisionMakingInput()
	{
		if (GameMgr.Get() != null && GameMgr.Get().IsSpectator())
		{
			return false;
		}
		if (GameState.Get() != null)
		{
			Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
			if (friendlySidePlayer != null && friendlySidePlayer.HasTag(GAME_TAG.AI_MAKES_DECISIONS_FOR_PLAYER))
			{
				return false;
			}
		}
		return true;
	}

	public Card GetHeldCard()
	{
		return m_heldCard;
	}

	public void EnableInput()
	{
		m_checkForInput = true;
	}

	public void OnMulliganEnded()
	{
		if ((bool)m_mousedOverCard)
		{
			SetShouldShowTooltip();
		}
	}

	private void SetShouldShowTooltip()
	{
		m_mousedOverTimer = 0f;
		m_mousedOverCard.SetShouldShowTooltip();
	}

	public ZoneHand GetFriendlyHand()
	{
		return m_myHandZone;
	}

	public ZoneHand GetEnemyHand()
	{
		return m_enemyHandZone;
	}

	public bool UseHandEnlarge()
	{
		return m_useHandEnlarge;
	}

	public void SetHandEnlarge(bool set)
	{
		m_useHandEnlarge = set;
	}

	public bool DoesHideHandAfterPlayingCard()
	{
		return m_hideHandAfterPlayingCard;
	}

	public void SetHideHandAfterPlayingCard(bool set)
	{
		m_hideHandAfterPlayingCard = set;
	}

	public bool DropHeldCard()
	{
		return DropHeldCard(wasCancelled: false);
	}

	private void HandleLeftMouseDown()
	{
		m_touchedDownOnSmallHand = false;
		bool flag = true;
		GameObject gameObject = null;
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out var hitInfo))
		{
			gameObject = hitInfo.collider.gameObject;
			if (gameObject.GetComponent<EndTurnButtonReminder>() != null)
			{
				return;
			}
			CardStandIn cardStandIn = SceneUtils.FindComponentInParents<CardStandIn>(hitInfo.transform);
			if (cardStandIn != null && GameState.Get() != null && !GameState.Get().IsMulliganManagerActive())
			{
				Card linkedCard = cardStandIn.linkedCard;
				if (IsCancelingBattlecryCard(linkedCard))
				{
					return;
				}
				if (m_useHandEnlarge && !m_myHandZone.HandEnlarged())
				{
					m_leftMouseButtonIsDown = true;
					m_touchedDownOnSmallHand = true;
					return;
				}
				m_lastObjectMousedDown = cardStandIn.gameObject;
				m_lastMouseDownPosition = UniversalInputManager.Get().GetMousePosition();
				m_leftMouseButtonIsDown = true;
				if (UniversalInputManager.Get().IsTouchMode())
				{
					m_touchDraggingCard = m_myHandZone.TouchReceived();
					m_lastPreviewedCard = cardStandIn.linkedCard;
				}
				if (m_heldCard == null)
				{
					m_myHandZone.HandleInput();
				}
				return;
			}
			if (gameObject.GetComponent<EndTurnButton>() != null && PermitDecisionMakingInput())
			{
				EndTurnButton.Get().PlayPushDownAnimation();
				m_lastObjectMousedDown = hitInfo.collider.gameObject;
				return;
			}
			if (gameObject.GetComponent<GameOpenPack>() != null)
			{
				m_lastObjectMousedDown = hitInfo.collider.gameObject;
				return;
			}
			Actor actor = SceneUtils.FindComponentInParents<Actor>(hitInfo.transform);
			if (actor == null)
			{
				return;
			}
			Card card = actor.GetCard();
			if (UniversalInputManager.Get().IsTouchMode() && m_battlecrySourceCard != null && card == m_battlecrySourceCard)
			{
				SetDragging(dragging: true);
				TargetReticleManager.Get().ShowArrow(show: true);
				return;
			}
			if (card != null && (IsCancelingBattlecryCard(card) || (m_useHandEnlarge && m_myHandZone.HandEnlarged() && card.GetEntity().IsHeroPower() && card.GetEntity().IsControlledByLocalUser() && m_myHandZone.GetCardCount() > 1)))
			{
				return;
			}
			if (card != null)
			{
				m_lastObjectMousedDown = card.gameObject;
			}
			else if (actor.GetHistoryCard() != null)
			{
				m_lastObjectMousedDown = actor.transform.parent.gameObject;
			}
			else
			{
				Debug.LogWarning("You clicked on something that is not being handled by InputManager.  Alert The Brode!");
			}
			m_lastMouseDownPosition = UniversalInputManager.Get().GetMousePosition();
			m_leftMouseButtonIsDown = true;
			flag = actor.GetEntity() != null && actor.GetEntity().IsGameModeButton();
		}
		if (m_useHandEnlarge && m_myHandZone.HandEnlarged() && ChoiceCardMgr.Get().GetSubOptionParentCard() == null && (gameObject == null || flag))
		{
			HidePhoneHand();
		}
		HandleMemberClick();
	}

	private void ShowPhoneHand()
	{
		if (!GameState.Get().IsMulliganPhaseNowOrPending() && !GameState.Get().IsGameOver() && m_useHandEnlarge && !m_myHandZone.HandEnlarged())
		{
			m_myHandZone.AddUpdateLayoutCompleteCallback(OnHandEnlargeComplete);
			m_myHandZone.SetHandEnlarged(enlarged: true);
			PhoneHandShownListener[] array = m_phoneHandShownListener.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire();
			}
		}
	}

	public void HidePhoneHand()
	{
		if (m_useHandEnlarge && m_myHandZone != null && m_myHandZone.HandEnlarged())
		{
			m_myHandZone.SetHandEnlarged(enlarged: false);
			PhoneHandHiddenListener[] array = m_phoneHandHiddenListener.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire();
			}
		}
	}

	private void OnHandEnlargeComplete(Zone zone, object userData)
	{
		zone.RemoveUpdateLayoutCompleteCallback(OnHandEnlargeComplete);
		if (m_leftMouseButtonIsDown && UniversalInputManager.Get().InputHitAnyObject(GameLayer.CardRaycast))
		{
			HandleLeftMouseDown();
		}
	}

	private void HidePhoneHandIfOutOfServerPlays()
	{
		if (!GameState.Get().HasHandPlays())
		{
			HidePhoneHand();
		}
	}

	private bool HasLocalHandPlays()
	{
		List<Card> cards = m_myHandZone.GetCards();
		if (cards.Count == 0)
		{
			return false;
		}
		int spendableManaCrystals = ManaCrystalMgr.Get().GetSpendableManaCrystals();
		foreach (Card item in cards)
		{
			if (item.GetEntity().GetRealTimeCost() <= spendableManaCrystals)
			{
				return true;
			}
		}
		return false;
	}

	private void HandleLeftMouseUp()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.UP);
		m_lastInputDrag = m_dragging;
		SetDragging(dragging: false);
		m_leftMouseButtonIsDown = false;
		m_targettingHeroPower = false;
		GameObject lastObjectMousedDown = m_lastObjectMousedDown;
		m_lastObjectMousedDown = null;
		if (UniversalInputManager.Get().WasTouchCanceled())
		{
			CancelOption();
			m_heldCard = null;
			return;
		}
		if (m_heldCard != null && (GameState.Get().GetResponseMode() == GameState.ResponseMode.OPTION || GameState.Get().GetResponseMode() == GameState.ResponseMode.NONE))
		{
			DropHeldCard();
			return;
		}
		bool flag = UniversalInputManager.Get().IsTouchMode() && GameState.Get().IsInTargetMode();
		bool flag2 = ChoiceCardMgr.Get().GetSubOptionParentCard() != null;
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out var hitInfo))
		{
			GameObject gameObject = hitInfo.collider.gameObject;
			if (gameObject.GetComponent<EndTurnButtonReminder>() != null)
			{
				return;
			}
			if (gameObject.GetComponent<EndTurnButton>() != null && gameObject == lastObjectMousedDown && PermitDecisionMakingInput())
			{
				EndTurnButton.Get().PlayButtonUpAnimation();
				DoEndTurnButton();
			}
			else if (gameObject.GetComponent<GameOpenPack>() != null && gameObject == lastObjectMousedDown)
			{
				gameObject.GetComponent<GameOpenPack>().HandleClick();
			}
			else
			{
				Actor actor = SceneUtils.FindComponentInParents<Actor>(hitInfo.transform);
				if (actor != null)
				{
					Card card = actor.GetCard();
					if (card != null)
					{
						if ((card.gameObject == lastObjectMousedDown || m_lastInputDrag) && !IsCancelingBattlecryCard(card))
						{
							HandleClickOnCard(actor.GetCard().gameObject, card.gameObject == lastObjectMousedDown);
						}
					}
					else if (actor.GetHistoryCard() != null)
					{
						HistoryManager.Get().HandleClickOnBigCard(actor.GetHistoryCard());
					}
					else if (GameState.Get().IsMulliganManagerActive())
					{
						MulliganManager.Get().ToggleHoldState(actor);
					}
				}
				CardStandIn cardStandIn = SceneUtils.FindComponentInParents<CardStandIn>(hitInfo.transform);
				if (cardStandIn != null)
				{
					if (m_useHandEnlarge && m_touchedDownOnSmallHand)
					{
						ShowPhoneHand();
					}
					if (lastObjectMousedDown == cardStandIn.gameObject && cardStandIn.linkedCard != null && GameState.Get() != null && !GameState.Get().IsMulliganManagerActive() && !IsCancelingBattlecryCard(cardStandIn.linkedCard))
					{
						HandleClickOnCard(cardStandIn.linkedCard.gameObject, wasMouseDownTarget: true);
					}
				}
				if (UniversalInputManager.Get().IsTouchMode() && actor != null && ChoiceCardMgr.Get().GetSubOptionParentCard() != null)
				{
					foreach (Card friendlyCard in ChoiceCardMgr.Get().GetFriendlyCards())
					{
						if (friendlyCard == actor.GetCard())
						{
							flag2 = false;
							break;
						}
					}
				}
			}
		}
		if (flag)
		{
			CancelOption();
		}
		if (UniversalInputManager.Get().IsTouchMode() && flag2 && ChoiceCardMgr.Get().GetSubOptionParentCard() != null)
		{
			CancelSubOptionMode();
		}
	}

	private void HandleRightMouseDown()
	{
		if (!UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out var hitInfo))
		{
			return;
		}
		GameObject gameObject = hitInfo.collider.gameObject;
		if (gameObject.GetComponent<EndTurnButtonReminder>() != null || gameObject.GetComponent<EndTurnButton>() != null)
		{
			return;
		}
		Actor actor = SceneUtils.FindComponentInParents<Actor>(hitInfo.transform);
		if (!(actor == null))
		{
			if (actor.GetCard() != null)
			{
				m_lastObjectRightMousedDown = actor.GetCard().gameObject;
			}
			else if (actor.GetHistoryCard() != null)
			{
				m_lastObjectRightMousedDown = actor.transform.parent.gameObject;
			}
			else
			{
				Debug.LogWarning("You clicked on something that is not being handled by InputManager.  Alert The Brode!");
			}
		}
	}

	private void HandleRightMouseUp()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.UP);
		GameObject lastObjectRightMousedDown = m_lastObjectRightMousedDown;
		m_lastObjectRightMousedDown = null;
		m_lastObjectMousedDown = null;
		m_leftMouseButtonIsDown = false;
		SetDragging(dragging: false);
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out var hitInfo))
		{
			Actor actor = SceneUtils.FindComponentInParents<Actor>(hitInfo.transform);
			if (actor == null || actor.GetCard() == null)
			{
				HandleRightClick();
			}
			else if (actor.GetCard().gameObject == lastObjectRightMousedDown)
			{
				HandleRightClickOnCard(actor.GetCard());
			}
			else
			{
				HandleRightClick();
			}
		}
		else
		{
			HandleRightClick();
		}
	}

	private void HandleRightClick()
	{
		if (!CancelOption())
		{
			if (EmoteHandler.Get() != null && EmoteHandler.Get().AreEmotesActive())
			{
				EmoteHandler.Get().HideEmotes();
			}
			if (EnemyEmoteHandler.Get() != null && EnemyEmoteHandler.Get().AreEmotesActive())
			{
				EnemyEmoteHandler.Get().HideEmotes();
			}
		}
	}

	private bool CancelOption(bool timeout = false)
	{
		bool result = false;
		GameState gameState = GameState.Get();
		if (gameState.IsInMainOptionMode())
		{
			gameState.CancelCurrentOptionMode();
		}
		if (CancelTargetMode())
		{
			result = true;
		}
		if (CancelSubOptionMode(timeout))
		{
			result = true;
		}
		if (DropHeldCard(wasCancelled: true))
		{
			result = true;
		}
		if ((bool)m_mousedOverCard)
		{
			m_mousedOverCard.UpdateProposedManaUsage();
		}
		return result;
	}

	private bool CancelTargetMode()
	{
		if (!GameState.Get().IsInTargetMode())
		{
			return false;
		}
		SoundManager.Get().LoadAndPlay("CancelAttack.prefab:9cde7207a78024e46aa5a0a657807845");
		if ((bool)m_mousedOverCard)
		{
			DisableSkullIfNeeded(m_mousedOverCard);
		}
		if ((bool)TargetReticleManager.Get())
		{
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(isLocallyCanceled: true);
		}
		ResetBattlecrySourceCard();
		CancelSubOptions();
		GameState.Get().CancelCurrentOptionMode();
		return true;
	}

	private bool CancelSubOptionMode(bool timeout = false)
	{
		if (!GameState.Get().IsInSubOptionMode())
		{
			return false;
		}
		if (ChoiceCardMgr.Get().IsWaitingToShowSubOptions())
		{
			if (timeout)
			{
				StartCoroutine(WaitAndCancelSubOptionMode());
			}
			return false;
		}
		CancelSubOptions();
		GameState.Get().CancelCurrentOptionMode();
		return true;
	}

	private IEnumerator WaitAndCancelSubOptionMode()
	{
		ChoiceCardMgr.Get().QuenePendingCancelSubOptions();
		while (ChoiceCardMgr.Get().IsWaitingToShowSubOptions())
		{
			yield return null;
		}
		if (ChoiceCardMgr.Get().HasPendingCancelSubOptions())
		{
			CancelSubOptions();
			if (GameState.Get().IsInSubOptionMode())
			{
				GameState.Get().CancelCurrentOptionMode();
			}
		}
		ChoiceCardMgr.Get().ClearPendingCancelSubOptions();
	}

	private void PositionHeldCard()
	{
		Card heldCard = m_heldCard;
		Entity entity = heldCard.GetEntity();
		ZonePlay controllersPlayZone = GetControllersPlayZone(entity);
		if (UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.InvisibleHitBox2, out var _))
		{
			if (!heldCard.IsOverPlayfield())
			{
				if (!GameState.Get().HasResponse(entity))
				{
					m_leftMouseButtonIsDown = false;
					m_lastObjectMousedDown = null;
					SetDragging(dragging: false);
					DropHeldCard();
					return;
				}
				heldCard.NotifyOverPlayfield();
			}
			if (entity.IsMinion() && GetNumberOfMinionsInPlay(controllersPlayZone) < GameState.Get().GetMaxFriendlyMinionsPerPlayer())
			{
				if ((bool)GetMoveMinionHoverTarget(heldCard))
				{
					controllersPlayZone.SortWithSpotForHeldCard(-1);
				}
				else
				{
					int num = PlayZoneSlotMousedOver(heldCard);
					if (num != controllersPlayZone.GetSlotMousedOver())
					{
						controllersPlayZone.SortWithSpotForHeldCard(num);
					}
				}
			}
		}
		else if (heldCard.IsOverPlayfield())
		{
			heldCard.NotifyLeftPlayfield();
			controllersPlayZone.SortWithSpotForHeldCard(-1);
		}
		if (UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.DragPlane, out var hitInfo2))
		{
			heldCard.transform.position = hitInfo2.point;
		}
	}

	private int GetNumberOfMinionsInPlay(ZonePlay play)
	{
		return play.GetCards().Count((Card c) => !c.IsBeingDragged);
	}

	private int PlayZoneSlotMousedOver(Card card)
	{
		ZonePlay controllersPlayZone = GetControllersPlayZone(card.GetEntity());
		int num = 0;
		if (UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.InvisibleHitBox2, out var hitInfo))
		{
			int numberOfMinionsInPlay = GetNumberOfMinionsInPlay(controllersPlayZone);
			float slotWidth = controllersPlayZone.GetSlotWidth();
			float num2 = controllersPlayZone.transform.position.x - (float)(numberOfMinionsInPlay + 1) * slotWidth / 2f;
			num = (int)Mathf.Ceil((hitInfo.point.x - num2) / slotWidth - slotWidth / 2f);
			if (num < 0 || num > numberOfMinionsInPlay)
			{
				num = ((!(card.transform.position.x < controllersPlayZone.transform.position.x)) ? numberOfMinionsInPlay : 0);
			}
		}
		return num;
	}

	private void HandleUpdateWhileLeftMouseButtonIsDown()
	{
		if (UniversalInputManager.Get().IsTouchMode() && m_heldCard == null)
		{
			if (GetBattlecrySourceCard() == null)
			{
				m_myHandZone.HandleInput();
			}
			Card card = ((m_myHandZone.CurrentStandIn != null) ? m_myHandZone.CurrentStandIn.linkedCard : null);
			if (card != m_lastPreviewedCard)
			{
				if (card != null)
				{
					m_lastMouseDownPosition.y = UniversalInputManager.Get().GetMousePosition().y;
				}
				m_lastPreviewedCard = card;
			}
		}
		if (m_dragging || m_lastObjectMousedDown == null)
		{
			return;
		}
		if ((bool)m_lastObjectMousedDown.GetComponent<HistoryCard>())
		{
			m_lastObjectMousedDown = null;
			m_leftMouseButtonIsDown = false;
			return;
		}
		float num = UniversalInputManager.Get().GetMousePosition().y - m_lastMouseDownPosition.y;
		float num2 = UniversalInputManager.Get().GetMousePosition().x - m_lastMouseDownPosition.x;
		if (num2 > -20f && num2 < 20f && num > -20f && num < 20f)
		{
			return;
		}
		bool flag = !UniversalInputManager.Get().IsTouchMode() || num > (float)MIN_GRAB_Y;
		CardStandIn cardStandIn = m_lastObjectMousedDown.GetComponent<CardStandIn>();
		if (cardStandIn != null && GameState.Get() != null && !GameState.Get().IsMulliganManagerActive())
		{
			if (UniversalInputManager.Get().IsTouchMode())
			{
				if (!flag)
				{
					return;
				}
				cardStandIn = m_myHandZone.CurrentStandIn;
				if (cardStandIn == null)
				{
					return;
				}
			}
			if (!ChoiceCardMgr.Get().IsFriendlyShown() && GetBattlecrySourceCard() == null && IsInZone(cardStandIn.linkedCard, TAG_ZONE.HAND))
			{
				SetDragging(dragging: true);
				GrabCard(cardStandIn.linkedCard.gameObject);
			}
		}
		else
		{
			if (GameState.Get().IsMulliganManagerActive() || GameState.Get().IsInTargetMode())
			{
				return;
			}
			Card component = m_lastObjectMousedDown.GetComponent<Card>();
			Entity entity = component.GetEntity();
			if (IsInZone(component, TAG_ZONE.HAND))
			{
				if (entity.IsControlledByLocalUser() && flag && (!UniversalInputManager.Get().IsTouchMode() || GameState.Get().HasResponse(entity)) && (component.GetZone().m_ServerTag == TAG_ZONE.HAND || GameState.Get().HasResponse(entity)) && !ChoiceCardMgr.Get().IsFriendlyShown() && GetBattlecrySourceCard() == null)
				{
					SetDragging(dragging: true);
					GrabCard(m_lastObjectMousedDown);
				}
			}
			else if (IsInZone(component, TAG_ZONE.PLAY) && ((!entity.IsHeroPowerOrGameModeButton() && !entity.IsMoveMinionHoverTarget()) || (entity.IsHeroPowerOrGameModeButton() && GameState.Get().EntityHasTargets(entity))))
			{
				SetDragging(dragging: true);
				HandleClickOnCardInBattlefield(entity);
			}
		}
	}

	private void HandleUpdateWhileHoldingCard(bool hitBattlefield)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.DRAG);
		Card heldCard = m_heldCard;
		if (!heldCard.IsInputEnabled())
		{
			DropHeldCard();
			return;
		}
		Entity entity = heldCard.GetEntity();
		if (hitBattlefield && (bool)TargetReticleManager.Get() && !TargetReticleManager.Get().IsActive() && GameState.Get().EntityHasTargets(entity) && entity.GetCardType() != TAG_CARDTYPE.MINION)
		{
			if (!DoNetworkResponse(entity))
			{
				PositionHeldCard();
				return;
			}
			DragCardSoundEffects component = heldCard.GetComponent<DragCardSoundEffects>();
			if ((bool)component)
			{
				component.Disable();
			}
			RemoteActionHandler.Get().NotifyOpponentOfCardPickedUp(heldCard);
			RemoteActionHandler.Get().NotifyOpponentOfTargetModeBegin(heldCard);
			Entity hero = entity.GetHero();
			TargetReticleManager.Get().CreateFriendlyTargetArrow(hero, entity, showDamageIndicatorText: true);
			ActivatePowerUpSpell(heldCard);
			ActivatePlaySpell(heldCard);
		}
		else
		{
			bool cardWasInsideHandLastFrame = m_cardWasInsideHandLastFrame;
			if (hitBattlefield && m_cardWasInsideHandLastFrame)
			{
				RemoteActionHandler.Get().NotifyOpponentOfCardPickedUp(heldCard);
				m_cardWasInsideHandLastFrame = false;
			}
			else if (!hitBattlefield)
			{
				m_cardWasInsideHandLastFrame = true;
			}
			PositionHeldCard();
			if (hitBattlefield)
			{
				m_myPlayZone.OnMagneticHeld(m_heldCard);
				m_myHandZone.OnCardHeld(m_heldCard);
			}
			else if (cardWasInsideHandLastFrame)
			{
				m_myHandZone.OnTwinspellDropped(m_heldCard);
				m_myPlayZone.OnMagneticDropped(m_heldCard);
			}
			if (GameState.Get().GetResponseMode() == GameState.ResponseMode.SUB_OPTION)
			{
				CancelSubOptionMode();
			}
		}
		if (UniversalInputManager.Get().IsTouchMode() && !hitBattlefield && m_heldCard != null && UniversalInputManager.Get().GetMousePosition().y - m_lastMouseDownPosition.y < (float)MIN_GRAB_Y && !IsInZone(m_heldCard, TAG_ZONE.PLAY))
		{
			m_myHandZone.OnTwinspellDropped(m_heldCard);
			m_myPlayZone.OnMagneticDropped(m_heldCard);
			PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
			ReturnHeldCardToHand();
		}
	}

	private MoveMinionHoverTarget GetMoveMinionHoverTarget(Card heldCard)
	{
		if (heldCard == null)
		{
			return null;
		}
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out var hitInfo))
		{
			MoveMinionHoverTarget componentInParent = hitInfo.transform.gameObject.GetComponentInParent<MoveMinionHoverTarget>();
			if (componentInParent != null)
			{
				return componentInParent;
			}
		}
		return null;
	}

	private void ActivatePowerUpSpell(Card card)
	{
		Entity entity = card.GetEntity();
		if (entity.IsSpell() || entity.IsMinion())
		{
			Spell actorSpell = card.GetActorSpell(SpellType.POWER_UP);
			if (actorSpell != null)
			{
				actorSpell.ActivateState(SpellStateType.BIRTH);
			}
		}
		card.DeactivateHandStateSpells();
	}

	private void ActivatePlaySpell(Card card)
	{
		Entity entity = card.GetEntity();
		if (!entity.HasTag(GAME_TAG.CARD_DOES_NOTHING))
		{
			Entity parentEntity = entity.GetParentEntity();
			Spell spell;
			if (parentEntity == null)
			{
				spell = card.GetPlaySpell(0);
			}
			else
			{
				Card card2 = parentEntity.GetCard();
				int subCardIndex = parentEntity.GetSubCardIndex(entity);
				spell = card2.GetSubOptionSpell(subCardIndex, 0);
			}
			if (spell != null && spell.GetActiveState() == SpellStateType.NONE)
			{
				spell.ActivateState(SpellStateType.BIRTH);
			}
		}
	}

	private void HandleMouseMove()
	{
		if (GameState.Get() != null && GameState.Get().IsInTargetMode())
		{
			HandleUpdateWhileNotHoldingCard();
		}
	}

	private void HandleUpdateWhileNotHoldingCard()
	{
		if (!UniversalInputManager.Get().IsTouchMode() || !TargetReticleManager.Get().IsLocalArrowActive())
		{
			m_myHandZone.HandleInput();
		}
		bool num = UniversalInputManager.Get().IsTouchMode() && !UniversalInputManager.Get().GetMouseButton(0);
		RaycastHit hitInfo;
		bool inputHitInfo = UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out hitInfo);
		if (!num && inputHitInfo)
		{
			CardStandIn cardStandIn = SceneUtils.FindComponentInParents<CardStandIn>(hitInfo.transform);
			Actor actor = SceneUtils.FindComponentInParents<Actor>(hitInfo.transform);
			if (actor == null && cardStandIn == null)
			{
				HandleMouseOverObjectWhileNotHoldingCard(hitInfo);
				return;
			}
			if (m_mousedOverObject != null)
			{
				HandleMouseOffLastObject();
			}
			Card card = null;
			if (actor != null)
			{
				card = actor.GetCard();
			}
			if (card == null)
			{
				if (GameState.Get() == null || GameState.Get().IsMulliganManagerActive())
				{
					if (m_mousedOverCard != null)
					{
						HandleMouseOffCard();
					}
					return;
				}
				if (cardStandIn == null)
				{
					return;
				}
				card = cardStandIn.linkedCard;
			}
			if (IsCancelingBattlecryCard(card) || (m_useHandEnlarge && m_myHandZone.HandEnlarged() && card.GetEntity().IsHeroPowerOrGameModeButton() && card.GetEntity().IsControlledByLocalUser() && m_myHandZone.GetCardCount() > 1))
			{
				return;
			}
			if (card != m_mousedOverCard && (card.GetZone() != m_myHandZone || GameState.Get().IsMulliganManagerActive()))
			{
				if (m_mousedOverCard != null)
				{
					HandleMouseOffCard();
				}
				HandleMouseOverCard(card);
			}
			PegCursor.Get().SetMode(PegCursor.Mode.OVER);
		}
		else
		{
			HandleMouseOff();
		}
	}

	private void HandleMouseOverObjectWhileNotHoldingCard(RaycastHit hitInfo)
	{
		GameObject gameObject = hitInfo.collider.gameObject;
		if (m_mousedOverCard != null)
		{
			HandleMouseOffCard();
		}
		if (UniversalInputManager.Get().IsTouchMode() && !UniversalInputManager.Get().GetMouseButton(0))
		{
			if (m_mousedOverObject != null)
			{
				HandleMouseOffLastObject();
			}
			return;
		}
		bool flag = TargetReticleManager.Get() != null && TargetReticleManager.Get().IsLocalArrowActive();
		if (!PermitDecisionMakingInput())
		{
			flag = false;
		}
		if (gameObject.GetComponent<HistoryManager>() != null && !flag)
		{
			m_mousedOverObject = gameObject;
			HistoryManager.Get().NotifyOfInput(hitInfo.point.z);
		}
		else if (gameObject.GetComponent<PlayerLeaderboardManager>() != null && !flag)
		{
			m_mousedOverObject = gameObject;
			PlayerLeaderboardManager.Get().NotifyOfInput(hitInfo.point);
		}
		else
		{
			if (m_mousedOverObject == gameObject)
			{
				return;
			}
			if (m_mousedOverObject != null)
			{
				HandleMouseOffLastObject();
			}
			if ((bool)EndTurnButton.Get() && PermitDecisionMakingInput())
			{
				if (gameObject.GetComponent<EndTurnButton>() != null)
				{
					m_mousedOverObject = gameObject;
					EndTurnButton.Get().HandleMouseOver();
				}
				else if (gameObject.GetComponent<EndTurnButtonReminder>() != null && gameObject.GetComponent<EndTurnButtonReminder>().ShowFriendlySidePlayerTurnReminder())
				{
					m_mousedOverObject = gameObject;
				}
			}
			TooltipZone component = gameObject.GetComponent<TooltipZone>();
			if (component != null)
			{
				m_mousedOverObject = gameObject;
				ShowTooltipZone(gameObject, component);
			}
			GameOpenPack component2 = gameObject.GetComponent<GameOpenPack>();
			if (component2 != null)
			{
				m_mousedOverObject = gameObject;
				component2.NotifyOfMouseOver();
			}
			_ = GetBattlecrySourceCard() != null;
		}
	}

	private void HandleMouseOff()
	{
		if ((bool)m_mousedOverCard)
		{
			Card friendlyHoverCard = RemoteActionHandler.Get().GetFriendlyHoverCard();
			if (m_mousedOverCard != friendlyHoverCard)
			{
				HandleMouseOffCard();
			}
		}
		if ((bool)m_mousedOverObject)
		{
			HandleMouseOffLastObject();
		}
	}

	private void HandleMouseOffLastObject()
	{
		if ((bool)m_mousedOverObject.GetComponent<EndTurnButton>())
		{
			m_mousedOverObject.GetComponent<EndTurnButton>().HandleMouseOut();
			m_lastObjectMousedDown = null;
		}
		else if ((bool)m_mousedOverObject.GetComponent<EndTurnButtonReminder>())
		{
			m_lastObjectMousedDown = null;
		}
		else if (m_mousedOverObject.GetComponent<TooltipZone>() != null)
		{
			m_mousedOverObject.GetComponent<TooltipZone>().HideTooltip();
			m_lastObjectMousedDown = null;
		}
		else if (m_mousedOverObject.GetComponent<HistoryManager>() != null)
		{
			HistoryManager.Get().NotifyOfMouseOff();
		}
		else if (m_mousedOverObject.GetComponent<PlayerLeaderboardManager>() != null)
		{
			PlayerLeaderboardManager.Get().NotifyOfMouseOff();
		}
		else if (m_mousedOverObject.GetComponent<GameOpenPack>() != null)
		{
			m_mousedOverObject.GetComponent<GameOpenPack>().NotifyOfMouseOff();
			m_lastObjectMousedDown = null;
		}
		m_mousedOverObject = null;
		HideBigViewCardBacks();
	}

	private void GrabCard(GameObject cardObject)
	{
		if (!PermitDecisionMakingInput())
		{
			return;
		}
		Card component = cardObject.GetComponent<Card>();
		if (!component.IsInputEnabled() || !GameState.Get().GetGameEntity().ShouldAllowCardGrab(component.GetEntity()))
		{
			return;
		}
		Zone zone = component.GetZone();
		if (!zone.IsInputEnabled())
		{
			return;
		}
		component.SetDoNotSort(on: true);
		float num = 0.7f;
		if (zone is ZoneHand)
		{
			ZoneHand zoneHand = (ZoneHand)zone;
			if (!UniversalInputManager.Get().IsTouchMode())
			{
				zoneHand.UpdateLayout(null);
			}
			zoneHand.OnCardGrabbed(component);
		}
		else if (zone is ZonePlay)
		{
			ZonePlay obj = (ZonePlay)zone;
			obj.RemoveCard(component);
			obj.UpdateLayout();
			component.HideTooltip();
			num = 0.9f;
		}
		m_heldCard = component;
		component.IsBeingDragged = true;
		SoundManager.Get().LoadAndPlay("FX_MinionSummon01_DrawFromHand_01.prefab:c8adc026a7f5d0a4cb0706627a980c58", cardObject);
		DragCardSoundEffects dragCardSoundEffects = m_heldCard.GetComponent<DragCardSoundEffects>();
		if ((bool)dragCardSoundEffects)
		{
			dragCardSoundEffects.enabled = true;
		}
		else
		{
			dragCardSoundEffects = cardObject.AddComponent<DragCardSoundEffects>();
		}
		dragCardSoundEffects.Restart();
		cardObject.AddComponent<DragRotator>().SetInfo(m_DragRotatorInfo);
		ProjectedShadow componentInChildren = component.GetActor().GetComponentInChildren<ProjectedShadow>();
		if (componentInChildren != null)
		{
			componentInChildren.EnableShadow(0.15f);
		}
		iTween.Stop(cardObject);
		iTween.ScaleTo(cardObject, new Vector3(num, num, num), 0.2f);
		TooltipPanelManager.Get().HideKeywordHelp();
		if ((bool)CardTypeBanner.Get())
		{
			CardTypeBanner.Get().Hide();
		}
		component.NotifyPickedUp();
		GameState.Get().GetGameEntity().NotifyOfCardGrabbed(component.GetEntity());
		SceneUtils.SetLayer(component, GameLayer.Default);
	}

	private void DropCanceledHeldCard(Card card)
	{
		m_heldCard = null;
		RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
		ZonePlay controllersPlayZone = GetControllersPlayZone(card.GetEntity());
		m_myHandZone.UpdateLayout(null, forced: true);
		controllersPlayZone.SortWithSpotForHeldCard(-1);
		controllersPlayZone.OnMagneticDropped(card);
		m_myHandZone.OnTwinspellDropped(card);
		SendDragDropCancelPlayTelemetry(card.GetEntity());
		card.IsBeingDragged = false;
	}

	public void ReturnHeldCardToHand()
	{
		if (!(m_heldCard == null))
		{
			Log.Hand.Print("ReturnHeldCardToHand()");
			Card heldCard = m_heldCard;
			heldCard.SetDoNotSort(on: false);
			iTween.Stop(m_heldCard.gameObject);
			Entity entity = heldCard.GetEntity();
			heldCard.NotifyLeftPlayfield();
			GameState.Get().GetGameEntity().NotifyOfCardDropped(entity);
			DragCardSoundEffects component = heldCard.GetComponent<DragCardSoundEffects>();
			if ((bool)component)
			{
				component.Disable();
			}
			Object.Destroy(m_heldCard.GetComponent<DragRotator>());
			ProjectedShadow componentInChildren = heldCard.GetActor().GetComponentInChildren<ProjectedShadow>();
			if (componentInChildren != null)
			{
				componentInChildren.DisableShadow();
			}
			RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
			if (m_useHandEnlarge)
			{
				m_myHandZone.SetFriendlyHeroTargetingMode(enable: false);
			}
			m_myHandZone.UpdateLayout(m_myHandZone.GetLastMousedOverCard(), forced: true);
			m_heldCard.IsBeingDragged = false;
			SetDragging(dragging: false);
			m_heldCard = null;
		}
	}

	private bool DropHeldCard(bool wasCancelled)
	{
		Log.Hand.Print("DropHeldCard - cancelled? " + wasCancelled);
		PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
		if (m_enlargeHandAfterDropCard)
		{
			m_enlargeHandAfterDropCard = false;
			ShowPhoneHand();
		}
		if (m_useHandEnlarge)
		{
			m_myHandZone.SetFriendlyHeroTargetingMode(enable: false);
			if (m_hideHandAfterPlayingCard)
			{
				HidePhoneHand();
			}
			else
			{
				m_myHandZone.UpdateLayout(null, forced: true);
			}
		}
		if (m_heldCard == null)
		{
			return false;
		}
		Card heldCard = m_heldCard;
		heldCard.SetDoNotSort(on: false);
		iTween.Stop(m_heldCard.gameObject);
		Entity entity = heldCard.GetEntity();
		heldCard.NotifyLeftPlayfield();
		GameState.Get().GetGameEntity().NotifyOfCardDropped(entity);
		DragCardSoundEffects component = heldCard.GetComponent<DragCardSoundEffects>();
		if ((bool)component)
		{
			component.Disable();
		}
		Object.Destroy(m_heldCard.GetComponent<DragRotator>());
		m_heldCard = null;
		ProjectedShadow componentInChildren = heldCard.GetActor().GetComponentInChildren<ProjectedShadow>();
		if (componentInChildren != null)
		{
			componentInChildren.DisableShadow();
		}
		if (IsInZone(heldCard, TAG_ZONE.PLAY))
		{
			MoveMinionHoverTarget moveMinionHoverTarget = GetMoveMinionHoverTarget(heldCard);
			if (moveMinionHoverTarget != null && !wasCancelled)
			{
				moveMinionHoverTarget.DropCardOnHoverTarget(heldCard);
			}
			else
			{
				AddHeldCardBackToPlayZone(heldCard);
			}
			GameState.Get().ExitMoveMinionMode();
		}
		SceneUtils.SetLayer(heldCard, GameLayer.CardRaycast);
		if (wasCancelled)
		{
			DropCanceledHeldCard(heldCard);
			return true;
		}
		bool notifyEnemyOfTargetArrow = false;
		if (IsInZone(heldCard, TAG_ZONE.HAND))
		{
			if (entity.IsMinion() || entity.IsWeapon())
			{
				DropHeldMinionOrWeapon(heldCard, entity, ref notifyEnemyOfTargetArrow);
				if (entity.IsMinion() && heldCard.GetActor() != null && !UniversalInputManager.Get().IsTouchMode())
				{
					heldCard.GetActor().TurnOffCollider();
				}
			}
			else if (entity.IsSpell() || entity.IsHero())
			{
				bool cancelDrop = false;
				DropHeldSpellOrHero(heldCard, entity, ref cancelDrop);
				if (cancelDrop)
				{
					DropCanceledHeldCard(entity.GetCard());
					return true;
				}
			}
			m_myHandZone.UpdateLayout(null, forced: true);
			m_myPlayZone.SortWithSpotForHeldCard(-1);
		}
		if (IsInZone(heldCard, TAG_ZONE.PLAY))
		{
			if (entity.IsMinion())
			{
				DropHeldMinionOrWeapon(heldCard, entity, ref notifyEnemyOfTargetArrow);
			}
			GetControllersPlayZone(heldCard.GetEntity()).SortWithSpotForHeldCard(-1);
		}
		if (notifyEnemyOfTargetArrow)
		{
			if ((bool)RemoteActionHandler.Get())
			{
				RemoteActionHandler.Get().NotifyOpponentOfTargetModeBegin(heldCard);
			}
		}
		else if (GameState.Get().GetResponseMode() != GameState.ResponseMode.SUB_OPTION)
		{
			RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
		}
		return true;
	}

	public ZonePlay GetControllersPlayZone(Entity entity)
	{
		if (!entity.IsControlledByFriendlySidePlayer())
		{
			return m_enemyPlayZone;
		}
		return m_myPlayZone;
	}

	public void AddHeldCardBackToPlayZone(Card card)
	{
		GetControllersPlayZone(card.GetEntity()).AddCard(card);
	}

	private void SendDragDropCancelPlayTelemetry(Entity cancelledEntity)
	{
		if (cancelledEntity != null && GameMgr.Get() != null)
		{
			TelemetryManager.Client().SendDragDropCancelPlayCard(GameMgr.Get().GetMissionId(), ((TAG_CARDTYPE)cancelledEntity.GetTag(GAME_TAG.CARDTYPE)).ToString());
		}
	}

	private void DropHeldMinionOrWeapon(Card card, Entity entity, ref bool notifyEnemyOfTargetArrow)
	{
		if (card == null || entity == null)
		{
			Debug.LogWarningFormat("DropHeldMinionOrWeapon() is called with the invalid card or entity.");
			return;
		}
		ZonePlay controllersPlayZone = GetControllersPlayZone(card.GetEntity());
		bool flag = entity.IsMinion();
		bool flag2 = entity.IsWeapon();
		if (!flag && !flag2)
		{
			Debug.LogWarningFormat("DropHeldMinionOrWeapon() is called with the card: {0}", entity.GetCardId());
			card.IsBeingDragged = false;
			return;
		}
		if (!UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.InvisibleHitBox2, out var _))
		{
			controllersPlayZone.OnMagneticDropped(card);
			SendDragDropCancelPlayTelemetry(entity);
			card.IsBeingDragged = false;
			return;
		}
		Zone zone = ((!flag2) ? ((Zone)controllersPlayZone) : ((Zone)m_myWeaponZone));
		if ((bool)zone)
		{
			GameState gameState = GameState.Get();
			int num = 0;
			int num2 = 0;
			if (flag)
			{
				num = PlayZoneSlotMousedOver(card) + 1;
				num2 = ZoneMgr.Get().PredictZonePosition(zone, num);
				gameState.SetSelectedOptionPosition(num2);
			}
			if (DoNetworkResponse(entity))
			{
				if (IsInZone(card, TAG_ZONE.HAND))
				{
					m_lastZoneChangeList = ZoneMgr.Get().AddPredictedLocalZoneChange(card, zone, num, num2);
					PredictSpentMana(entity);
					controllersPlayZone.OnMagneticPlay(card, num2);
					if (flag && gameState.EntityHasTargets(entity))
					{
						notifyEnemyOfTargetArrow = true;
						bool showArrow = !UniversalInputManager.Get().IsTouchMode();
						if ((bool)TargetReticleManager.Get())
						{
							TargetReticleManager.Get().CreateFriendlyTargetArrow(entity, entity, showDamageIndicatorText: true, showArrow);
						}
						m_battlecrySourceCard = card;
						if (UniversalInputManager.Get().IsTouchMode())
						{
							StartBattleCryEffect(entity);
						}
					}
				}
				else if (IsInZone(card, TAG_ZONE.PLAY) && card.GetZone() == zone && card.GetZonePosition() != num2)
				{
					m_lastZoneChangeList = ZoneMgr.Get().AddPredictedLocalZoneChange(card, zone, num, num2);
					card.m_minionWasMovedFromSrcToDst = new ZonePositionChange
					{
						m_sourceZonePosition = card.GetZonePosition(),
						m_destinationZonePosition = num
					};
				}
			}
			else
			{
				gameState.SetSelectedOptionPosition(0);
			}
		}
		card.IsBeingDragged = false;
	}

	private void DropHeldSpellOrHero(Card card, Entity entity, ref bool cancelDrop)
	{
		if (card == null || entity == null)
		{
			Debug.LogWarningFormat("DropHeldSpellOrHero() is called with the invalid card or entity.");
			return;
		}
		if (!entity.IsSpell() && !entity.IsHero())
		{
			Debug.LogWarningFormat("DropHeldSpellOrHero() is called with the card: {0}", entity.GetCardId());
			return;
		}
		if (GameState.Get().EntityHasTargets(entity))
		{
			cancelDrop = true;
			return;
		}
		if (!UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.InvisibleHitBox2, out var _))
		{
			m_myHandZone.OnTwinspellDropped(card);
			SendDragDropCancelPlayTelemetry(entity);
			return;
		}
		if (!GameState.Get().HasResponse(entity))
		{
			PlayErrors.DisplayPlayError(GameState.Get().GetErrorType(entity), GameState.Get().GetErrorParam(entity), entity);
			return;
		}
		m_myHandZone.OnTwinspellPlayed(card);
		DoNetworkResponse(entity);
		m_lastZoneChangeList = ZoneMgr.Get().AddLocalZoneChange(card, TAG_ZONE.PLAY);
		PredictSpentMana(entity);
		if (entity.IsSpell())
		{
			if (GameState.Get().HasSubOptions(entity))
			{
				card.DeactivateHandStateSpells();
				return;
			}
			ActivatePowerUpSpell(card);
			ActivatePlaySpell(card);
		}
	}

	private void HandleRightClickOnCard(Card card)
	{
		if (GameState.Get().IsInTargetMode() || GameState.Get().IsInSubOptionMode() || m_heldCard != null)
		{
			HandleRightClick();
		}
		else
		{
			if (!card.GetEntity().IsHero())
			{
				return;
			}
			if (card.GetEntity().IsControlledByLocalUser())
			{
				if (EmoteHandler.Get() != null)
				{
					if (EmoteHandler.Get().AreEmotesActive())
					{
						EmoteHandler.Get().HideEmotes();
					}
					else
					{
						EmoteHandler.Get().ShowEmotes();
					}
				}
				return;
			}
			bool flag = EnemyEmoteHandler.Get() != null;
			if (GameMgr.Get().IsSpectator() && card.GetEntity().GetControllerSide() != Player.Side.OPPOSING)
			{
				flag = false;
			}
			if (flag)
			{
				if (EnemyEmoteHandler.Get().AreEmotesActive())
				{
					EnemyEmoteHandler.Get().HideEmotes();
				}
				else
				{
					EnemyEmoteHandler.Get().ShowEmotes();
				}
			}
		}
	}

	private void HandleClickOnCard(GameObject upClickedCard, bool wasMouseDownTarget)
	{
		if (EmoteHandler.Get() != null)
		{
			if (EmoteHandler.Get().IsMouseOverEmoteOption())
			{
				return;
			}
			EmoteHandler.Get().HideEmotes();
		}
		if (EnemyEmoteHandler.Get() != null)
		{
			if (EnemyEmoteHandler.Get().IsMouseOverEmoteOption())
			{
				return;
			}
			EnemyEmoteHandler.Get().HideEmotes();
		}
		Card component = upClickedCard.GetComponent<Card>();
		Entity entity = component.GetEntity();
		Log.Hand.Print("HandleClickOnCard - Card zone: " + component.GetZone());
		if (UniversalInputManager.Get().IsTouchMode() && entity.IsHero() && component.GetZone() is ZoneHero && !GameState.Get().IsInTargetMode() && wasMouseDownTarget)
		{
			if (entity.IsControlledByLocalUser())
			{
				if (EmoteHandler.Get() != null)
				{
					EmoteHandler.Get().ShowEmotes();
				}
				return;
			}
			if (!GameMgr.Get().IsSpectator() && EnemyEmoteHandler.Get() != null)
			{
				EnemyEmoteHandler.Get().ShowEmotes();
				return;
			}
		}
		if (component.GetEntity().IsMoveMinionHoverTarget())
		{
			return;
		}
		if (component == ChoiceCardMgr.Get().GetSubOptionParentCard())
		{
			CancelOption();
			return;
		}
		GameState.ResponseMode responseMode = GameState.Get().GetResponseMode();
		if (IsInZone(component, TAG_ZONE.HAND))
		{
			if (GameState.Get().IsMulliganManagerActive())
			{
				if (PermitDecisionMakingInput())
				{
					MulliganManager.Get().ToggleHoldState(component);
				}
			}
			else if (!component.IsAttacking() && !GameState.Get().IsInTargetMode() && !UniversalInputManager.Get().IsTouchMode() && component.GetEntity().IsControlledByLocalUser() && !ChoiceCardMgr.Get().IsFriendlyShown() && GetBattlecrySourceCard() == null && (component.GetZone().m_ServerTag == TAG_ZONE.HAND || GameState.Get().HasResponse(entity)))
			{
				GrabCard(upClickedCard);
			}
			return;
		}
		switch (responseMode)
		{
		case GameState.ResponseMode.SUB_OPTION:
			HandleClickOnSubOption(entity);
			return;
		case GameState.ResponseMode.CHOICE:
			HandleClickOnChoice(entity);
			return;
		}
		if (IsInZone(component, TAG_ZONE.PLAY))
		{
			HandleClickOnCardInBattlefield(entity);
		}
	}

	private void HandleClickOnCardInBattlefield(Entity clickedEntity)
	{
		if (!PermitDecisionMakingInput())
		{
			return;
		}
		PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
		GameState gameState = GameState.Get();
		Card card = clickedEntity.GetCard();
		if ((UniversalInputManager.Get().IsTouchMode() && clickedEntity.IsHeroPowerOrGameModeButton() && m_mousedOverTimer > m_MouseOverDelay) || (clickedEntity.IsGameModeButton() && clickedEntity.GetCard() != null && clickedEntity.GetCard().GetPlaySpell(0) != null && clickedEntity.GetCard().GetPlaySpell(0).GetActiveState() != 0) || !gameState.GetGameEntity().NotifyOfBattlefieldCardClicked(clickedEntity, gameState.IsInTargetMode()))
		{
			return;
		}
		if (gameState.IsInTargetMode())
		{
			DisableSkullIfNeeded(card);
			Network.Options.Option.SubOption selectedNetworkSubOption = gameState.GetSelectedNetworkSubOption();
			if (selectedNetworkSubOption.ID == clickedEntity.GetEntityId())
			{
				CancelOption();
				return;
			}
			UpdateTelemetryAttackInputCounts(gameState.GetEntity(selectedNetworkSubOption.ID));
			if (DoNetworkResponse(clickedEntity) && m_heldCard != null)
			{
				Card heldCard = m_heldCard;
				m_myHandZone.OnTwinspellPlayed(heldCard);
				Object.Destroy(heldCard.GetComponent<DragRotator>());
				m_heldCard = null;
				heldCard.SetDoNotSort(on: false);
				m_lastZoneChangeList = ZoneMgr.Get().AddLocalZoneChange(heldCard, TAG_ZONE.PLAY);
			}
		}
		else if (UniversalInputManager.Get().IsTouchMode() && UniversalInputManager.Get().GetMouseButtonUp(0) && gameState.EntityHasTargets(clickedEntity))
		{
			if (!card.IsShowingTooltip() && gameState.IsFriendlySidePlayerTurn())
			{
				PlayErrors.DisplayPlayError(PlayErrors.ErrorType.REQ_DRAG_TO_PLAY, null, clickedEntity);
			}
		}
		else if (clickedEntity.IsWeapon() && clickedEntity.IsControlledByLocalUser() && !GameState.Get().IsValidOption(clickedEntity))
		{
			HandleClickOnCardInBattlefield(gameState.GetFriendlySidePlayer().GetHero());
		}
		else if (clickedEntity.IsHero() && clickedEntity.IsControlledByLocalUser() && clickedEntity.GetWeaponCard() != null && GameState.Get().IsValidOption(clickedEntity.GetWeaponCard().GetEntity()))
		{
			HandleClickOnCardInBattlefield(clickedEntity.GetWeaponCard().GetEntity());
		}
		else if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.ALLOW_MOVE_MINION) > 0 && card.GetEntity().IsMinion())
		{
			if (card.IsInputEnabled() && !card.GetEntity().HasTag(GAME_TAG.CANT_MOVE_MINION) && (!UniversalInputManager.Get().IsTouchMode() || (!(m_mousedOverTimer > m_MouseOverDelay) && !UniversalInputManager.Get().GetMouseButtonUp(0))))
			{
				if (!clickedEntity.IsControlledByFriendlySidePlayer() && !GameState.Get().HasValidHoverTargetForMovedMinion(card.GetEntity(), out var mainOptionPlayError))
				{
					PlayErrors.DisplayPlayError(mainOptionPlayError, null, clickedEntity);
					return;
				}
				GrabCard(card.gameObject);
				GameState.Get().EnterMoveMinionMode(card.GetEntity());
			}
		}
		else
		{
			if (!DoNetworkResponse(clickedEntity))
			{
				return;
			}
			if (!gameState.IsInTargetMode())
			{
				if (clickedEntity.IsHeroPowerOrGameModeButton())
				{
					if (!clickedEntity.HasSubCards())
					{
						ActivatePlaySpell(card);
					}
					clickedEntity.SetTagAndHandleChange(GAME_TAG.EXHAUSTED, 1);
					PredictSpentMana(clickedEntity);
				}
				return;
			}
			RemoteActionHandler.Get().NotifyOpponentOfTargetModeBegin(card);
			if ((bool)TargetReticleManager.Get())
			{
				TargetReticleManager.Get().CreateFriendlyTargetArrow(clickedEntity, clickedEntity, showDamageIndicatorText: false);
			}
			if (clickedEntity.IsHeroPowerOrGameModeButton())
			{
				m_targettingHeroPower = true;
				ActivatePlaySpell(card);
			}
			else
			{
				if (!clickedEntity.IsCharacter())
				{
					return;
				}
				card.ActivateCharacterAttackEffects();
				if (!clickedEntity.HasTag(GAME_TAG.IGNORE_TAUNT))
				{
					gameState.ShowEnemyTauntCharacters();
				}
				if (card.IsAttacking())
				{
					return;
				}
				Spell actorAttackSpellForInput = card.GetActorAttackSpellForInput();
				if (actorAttackSpellForInput != null)
				{
					if (clickedEntity.GetRealTimeIsImmuneWhileAttacking())
					{
						card.GetActor().ActivateSpellBirthState(SpellType.IMMUNE);
					}
					actorAttackSpellForInput.ActivateState(SpellStateType.BIRTH);
				}
			}
		}
	}

	private void UpdateTelemetryAttackInputCounts(Entity sourceEntity)
	{
		if (sourceEntity != null && !(m_battlecrySourceCard != null) && (sourceEntity.IsMinion() || sourceEntity.IsHero()))
		{
			if (m_lastInputDrag)
			{
				m_telemetryNumDragAttacks++;
			}
			else
			{
				m_telemetryNumClickAttacks++;
			}
		}
	}

	public void HandleClickOnSubOption(Entity entity, bool isSimulated = false)
	{
		if (!isSimulated && !PermitDecisionMakingInput())
		{
			return;
		}
		if (isSimulated || GameState.Get().HasResponse(entity))
		{
			bool flag = false;
			Card subOptionParentCard = ChoiceCardMgr.Get().GetSubOptionParentCard();
			if (!isSimulated)
			{
				flag = GameState.Get().SubEntityHasTargets(entity);
				if (flag)
				{
					RemoteActionHandler.Get().NotifyOpponentOfTargetModeBegin(subOptionParentCard);
					Entity hero = entity.GetHero();
					Entity entity2 = subOptionParentCard.GetEntity();
					TargetReticleManager.Get().CreateFriendlyTargetArrow(hero, entity2, showDamageIndicatorText: true, !UniversalInputManager.Get().IsTouchMode(), entity.GetCardTextInHand());
				}
			}
			Card card = entity.GetCard();
			if (!isSimulated)
			{
				DoNetworkResponse(entity);
			}
			ActivatePowerUpSpell(card);
			if (!isSimulated)
			{
				ActivatePlaySpell(card);
			}
			if (entity.IsMinion() || entity.IsHero())
			{
				card.HideCard();
			}
			ChoiceCardMgr.Get().OnSubOptionClicked(entity);
			if (isSimulated)
			{
				ChoiceCardMgr.Get().ClearSubOptions();
			}
			else if (!flag)
			{
				FinishSubOptions();
			}
			if (UniversalInputManager.Get().IsTouchMode() && !isSimulated && flag)
			{
				StartMobileTargetingEffect(GameState.Get().GetSelectedNetworkSubOption().Targets);
			}
		}
		else
		{
			PlayErrors.DisplayPlayError(GameState.Get().GetErrorType(entity), GameState.Get().GetErrorParam(entity), entity);
		}
	}

	private void HandleClickOnChoice(Entity entity)
	{
		if (PermitDecisionMakingInput())
		{
			if (DoNetworkResponse(entity))
			{
				SoundManager.Get().LoadAndPlay("HeroDropItem1.prefab:587232e6704b20942af1205d00cfc0f9");
			}
			else
			{
				PlayErrors.DisplayPlayError(GameState.Get().GetErrorType(entity), GameState.Get().GetErrorParam(entity), entity);
			}
		}
	}

	public void ResetBattlecrySourceCard()
	{
		if (!(m_battlecrySourceCard == null))
		{
			if (UniversalInputManager.Get().IsTouchMode())
			{
				string message = ((!m_battlecrySourceCard.GetEntity().HasTag(GAME_TAG.BATTLECRY)) ? GameStrings.Get("GAMEPLAY_MOBILE_TARGETING_CANCELED") : GameStrings.Get("GAMEPLAY_MOBILE_BATTLECRY_CANCELED"));
				GameplayErrorManager.Get().DisplayMessage(message);
			}
			m_cancelingBattlecryCards.Add(m_battlecrySourceCard);
			Entity entity = m_battlecrySourceCard.GetEntity();
			Spell actorSpell = m_battlecrySourceCard.GetActorSpell(SpellType.BATTLECRY);
			if ((bool)actorSpell)
			{
				actorSpell.ActivateState(SpellStateType.CANCEL);
			}
			Spell playSpell = m_battlecrySourceCard.GetPlaySpell(0);
			if ((bool)playSpell)
			{
				playSpell.ActivateState(SpellStateType.CANCEL);
			}
			Spell customSummonSpell = m_battlecrySourceCard.GetCustomSummonSpell();
			if ((bool)customSummonSpell)
			{
				customSummonSpell.ActivateState(SpellStateType.CANCEL);
			}
			ZoneMgr.ChangeCompleteCallback callback = delegate(ZoneChangeList changeList, object userData)
			{
				Card item = (Card)userData;
				m_cancelingBattlecryCards.Remove(item);
			};
			ZoneMgr.Get().CancelLocalZoneChange(m_lastZoneChangeList, callback, m_battlecrySourceCard);
			m_lastZoneChangeList = null;
			RollbackSpentMana(entity);
			ClearBattlecrySourceCard();
		}
	}

	private bool IsCancelingBattlecryCard(Card card)
	{
		return m_cancelingBattlecryCards.Contains(card);
	}

	public void DoEndTurnButton()
	{
		if (PermitDecisionMakingInput() && !GameState.Get().IsResponsePacketBlocked() && !EndTurnButton.Get().IsInputBlocked() && !EndTurnButton.Get().IsDisabled)
		{
			DoEndTurnInternal();
		}
	}

	private void DoEndTurnInternal()
	{
		GameState gameState = GameState.Get();
		switch (gameState.GetResponseMode())
		{
		case GameState.ResponseMode.CHOICE:
			gameState.SendChoices();
			break;
		case GameState.ResponseMode.OPTION:
		{
			Network.Options optionsPacket = gameState.GetOptionsPacket();
			for (int i = 0; i < optionsPacket.List.Count; i++)
			{
				Network.Options.Option option = optionsPacket.List[i];
				if (option.Type == Network.Options.Option.OptionType.END_TURN || option.Type == Network.Options.Option.OptionType.PASS)
				{
					if (gameState.GetGameEntity().NotifyOfEndTurnButtonPushed())
					{
						gameState.SetSelectedOption(i);
						gameState.SendOption();
						HidePhoneHand();
						DoEndTurnButton_Option_OnEndTurnRequested();
					}
					break;
				}
			}
			break;
		}
		}
	}

	public void DoEndTurn_Cheat()
	{
		DoEndTurnInternal();
	}

	private void DoEndTurnButton_Option_OnEndTurnRequested()
	{
		if (TurnTimer.Get() != null)
		{
			TurnTimer.Get().OnEndTurnRequested();
		}
		EndTurnButton.Get().OnEndTurnRequested();
	}

	public bool DoNetworkResponse(Entity entity, bool checkValidInput = true)
	{
		if (ThinkEmoteManager.Get() != null)
		{
			ThinkEmoteManager.Get().NotifyOfActivity();
		}
		GameState gameState = GameState.Get();
		if (checkValidInput && !gameState.IsEntityInputEnabled(entity))
		{
			return false;
		}
		GameState.ResponseMode responseMode = gameState.GetResponseMode();
		bool flag = false;
		switch (responseMode)
		{
		case GameState.ResponseMode.CHOICE:
			flag = DoNetworkChoice(entity);
			break;
		case GameState.ResponseMode.OPTION:
			flag = DoNetworkOptions(entity);
			break;
		case GameState.ResponseMode.SUB_OPTION:
			flag = DoNetworkSubOptions(entity);
			break;
		case GameState.ResponseMode.OPTION_TARGET:
			flag = DoNetworkOptionTarget(entity);
			break;
		}
		if (flag)
		{
			entity.GetCard().UpdateActorState();
		}
		return flag;
	}

	private void OnOptionsReceived(object userData)
	{
		if ((bool)m_mousedOverCard)
		{
			m_mousedOverCard.UpdateProposedManaUsage();
		}
		HidePhoneHandIfOutOfServerPlays();
	}

	private void OnCurrentPlayerChanged(Player player, object userData)
	{
		if (player.IsLocalUser())
		{
			m_entitiesThatPredictedMana.Clear();
		}
	}

	private void OnOptionRejected(Network.Options.Option option, object userData)
	{
		if (option.Type == Network.Options.Option.OptionType.POWER)
		{
			Entity entity = GameState.Get().GetEntity(option.Main.ID);
			if (entity == null)
			{
				Debug.LogError("OnOptionRejected - Null Entity");
				return;
			}
			entity.GetCard().NotifyTargetingCanceled();
			if (entity.IsHeroPowerOrGameModeButton())
			{
				entity.SetTagAndHandleChange(GAME_TAG.EXHAUSTED, 0);
			}
			RollbackSpentMana(entity);
			if (entity.IsTwinspell())
			{
				GetFriendlyHand().ActivateTwinspellSpellDeath();
				GetFriendlyHand().ClearReservedCard();
			}
		}
		string message = GameStrings.Get("GAMEPLAY_ERROR_PLAY_REJECTED");
		GameplayErrorManager.Get().DisplayMessage(message);
	}

	private void OnTurnTimerUpdate(TurnTimerUpdate update, object userData)
	{
		if (update.GetSecondsRemaining() <= Mathf.Epsilon || GameUtils.IsWaitingForOpponentReconnect())
		{
			CancelOption(timeout: true);
		}
	}

	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		HidePhoneHand();
		CancelOption();
		SendGameOverTelemetry();
	}

	private void SendGameOverTelemetry()
	{
		int num = m_telemetryNumClickAttacks + m_telemetryNumDragAttacks;
		int percentClickAttacks = ((num != 0) ? ((int)((double)m_telemetryNumClickAttacks * 100.0 / (double)num)) : 0);
		int percentDragAttacks = ((num != 0) ? ((int)((double)m_telemetryNumDragAttacks * 100.0 / (double)num)) : 0);
		TelemetryManager.Client().SendAttackInputMethod(num, m_telemetryNumClickAttacks, percentClickAttacks, m_telemetryNumDragAttacks, percentDragAttacks);
		m_telemetryNumDragAttacks = 0;
		m_telemetryNumClickAttacks = 0;
	}

	private bool DoNetworkChoice(Entity entity)
	{
		GameState gameState = GameState.Get();
		if (!gameState.IsChoosableEntity(entity))
		{
			return false;
		}
		if (gameState.RemoveChosenEntity(entity))
		{
			return true;
		}
		gameState.AddChosenEntity(entity);
		if (gameState.GetFriendlyEntityChoices().IsSingleChoice() && (!GameState.Get().GetBooleanGameOption(GameEntityOption.MULLIGAN_IS_CHOOSE_ONE) || MulliganManager.Get() == null || !MulliganManager.Get().IsMulliganActive()))
		{
			gameState.SendChoices();
		}
		return true;
	}

	private bool DoNetworkOptions(Entity entity)
	{
		int entityId = entity.GetEntityId();
		GameState gameState = GameState.Get();
		Network.Options optionsPacket = gameState.GetOptionsPacket();
		for (int i = 0; i < optionsPacket.List.Count; i++)
		{
			Network.Options.Option option = optionsPacket.List[i];
			if (option.Type != Network.Options.Option.OptionType.POWER || !option.Main.PlayErrorInfo.IsValid() || option.Main.ID != entityId)
			{
				continue;
			}
			gameState.SetSelectedOption(i);
			if (!option.HasValidSubOption())
			{
				if (option.Main.Targets == null || option.Main.Targets.Count == 0)
				{
					gameState.SendOption();
				}
				else
				{
					EnterOptionTargetMode();
				}
			}
			else
			{
				gameState.EnterSubOptionMode();
				Card card = entity.GetCard();
				ChoiceCardMgr.Get().ShowSubOptions(card);
			}
			return true;
		}
		if (!UniversalInputManager.Get().IsTouchMode() || !entity.GetCard().IsShowingTooltip())
		{
			PlayErrors.DisplayPlayError(GameState.Get().GetErrorType(entity), GameState.Get().GetErrorParam(entity), entity);
		}
		return false;
	}

	private bool DoNetworkSubOptions(Entity entity)
	{
		int entityId = entity.GetEntityId();
		GameState gameState = GameState.Get();
		Network.Options.Option selectedNetworkOption = gameState.GetSelectedNetworkOption();
		for (int i = 0; i < selectedNetworkOption.Subs.Count; i++)
		{
			Network.Options.Option.SubOption subOption = selectedNetworkOption.Subs[i];
			if (subOption.PlayErrorInfo.IsValid() && subOption.ID == entityId)
			{
				gameState.SetSelectedSubOption(i);
				if (subOption.Targets == null || subOption.Targets.Count == 0)
				{
					gameState.SendOption();
				}
				else
				{
					EnterOptionTargetMode();
				}
				return true;
			}
		}
		return false;
	}

	private bool DoNetworkOptionTarget(Entity entity)
	{
		int entityId = entity.GetEntityId();
		GameState gameState = GameState.Get();
		Network.Options.Option.SubOption selectedNetworkSubOption = gameState.GetSelectedNetworkSubOption();
		Entity entity2 = gameState.GetEntity(selectedNetworkSubOption.ID);
		if (!selectedNetworkSubOption.IsValidTarget(entityId))
		{
			Entity entity3 = gameState.GetEntity(entityId);
			PlayErrors.DisplayPlayError(selectedNetworkSubOption.GetErrorForTarget(entity3.GetEntityId()), selectedNetworkSubOption.GetErrorParamForTarget(entity3.GetEntityId()), entity2);
			return false;
		}
		if ((bool)TargetReticleManager.Get())
		{
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(isLocallyCanceled: false);
		}
		if ((bool)RemoteActionHandler.Get())
		{
			RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
		}
		FinishBattlecrySourceCard();
		FinishSubOptions();
		if (entity2.IsHeroPowerOrGameModeButton())
		{
			entity2.SetTagAndHandleChange(GAME_TAG.EXHAUSTED, 1);
			PredictSpentMana(entity2);
		}
		gameState.SetSelectedOptionTarget(entityId);
		gameState.SendOption();
		return true;
	}

	private void EnterOptionTargetMode()
	{
		GameState gameState = GameState.Get();
		gameState.EnterOptionTargetMode();
		if (m_useHandEnlarge)
		{
			m_myHandZone.SetFriendlyHeroTargetingMode(gameState.FriendlyHeroIsTargetable());
			m_enlargeHandAfterDropCard = m_myHandZone.HandEnlarged() || ChoiceCardMgr.Get().RestoreEnlargedHandAfterChoice();
			if (m_myHandZone.HandEnlarged())
			{
				HidePhoneHand();
			}
			else
			{
				m_myHandZone.UpdateLayout(null, forced: true);
			}
		}
	}

	private void FinishBattlecrySourceCard()
	{
		if (!(m_battlecrySourceCard == null))
		{
			ClearBattlecrySourceCard();
		}
	}

	private void ClearBattlecrySourceCard()
	{
		if (m_isInBattleCryEffect && m_battlecrySourceCard != null)
		{
			EndBattleCryEffect();
		}
		m_battlecrySourceCard = null;
		RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
		if (m_useHandEnlarge)
		{
			m_myHandZone.SetFriendlyHeroTargetingMode(enable: false);
			m_myHandZone.UpdateLayout(null, forced: true);
		}
	}

	private void CancelSubOptions()
	{
		Card subOptionParentCard = ChoiceCardMgr.Get().GetSubOptionParentCard();
		if (!(subOptionParentCard == null))
		{
			ChoiceCardMgr.Get().CancelSubOptions();
			if (subOptionParentCard.GetEntity().IsTwinspell())
			{
				m_myHandZone.OnTwinspellDropped(subOptionParentCard);
				m_myHandZone.ClearReservedCard();
			}
			Entity entity = subOptionParentCard.GetEntity();
			if (!entity.IsHeroPowerOrGameModeButton())
			{
				ZoneMgr.Get().CancelLocalZoneChange(m_lastZoneChangeList);
				m_lastZoneChangeList = null;
			}
			RollbackSpentMana(entity);
			DropSubOptionParentCard();
		}
	}

	private void FinishSubOptions()
	{
		if (!(ChoiceCardMgr.Get().GetSubOptionParentCard() == null))
		{
			DropSubOptionParentCard();
		}
	}

	public void DropSubOptionParentCard()
	{
		Log.Hand.Print("DropSubOptionParentCard()");
		ChoiceCardMgr.Get().ClearSubOptions();
		RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
		if (m_useHandEnlarge)
		{
			m_myHandZone.SetFriendlyHeroTargetingMode(enable: false);
			m_myHandZone.UpdateLayout(null, forced: true);
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			EndMobileTargetingEffect();
		}
	}

	private void StartMobileTargetingEffect(List<Network.Options.Option.TargetOption> targets)
	{
		if (targets == null || targets.Count == 0)
		{
			return;
		}
		m_mobileTargettingEffectActors.Clear();
		foreach (Network.Options.Option.TargetOption target in targets)
		{
			if (target.PlayErrorInfo.IsValid())
			{
				Entity entity = GameState.Get().GetEntity(target.ID);
				if (entity.GetCard() != null)
				{
					Actor actor = entity.GetCard().GetActor();
					m_mobileTargettingEffectActors.Add(actor);
					ApplyMobileTargettingEffectToActor(actor);
				}
			}
		}
		FullScreenFXMgr.Get().Desaturate(0.9f, 0.4f, iTween.EaseType.easeInOutQuad);
	}

	private void EndMobileTargetingEffect()
	{
		foreach (Actor mobileTargettingEffectActor in m_mobileTargettingEffectActors)
		{
			RemoveMobileTargettingEffectFromActor(mobileTargettingEffectActor);
		}
		FullScreenFXMgr.Get().StopDesaturate(0.4f, iTween.EaseType.easeInOutQuad);
	}

	private void StartBattleCryEffect(Entity entity)
	{
		m_isInBattleCryEffect = true;
		Network.Options.Option selectedNetworkOption = GameState.Get().GetSelectedNetworkOption();
		if (selectedNetworkOption == null)
		{
			Debug.LogError("No targets for BattleCry.");
			return;
		}
		StartMobileTargetingEffect(selectedNetworkOption.Main.Targets);
		m_battlecrySourceCard.SetBattleCrySource(source: true);
	}

	private void EndBattleCryEffect()
	{
		m_isInBattleCryEffect = false;
		EndMobileTargetingEffect();
		m_battlecrySourceCard.SetBattleCrySource(source: false);
	}

	private void ApplyMobileTargettingEffectToActor(Actor actor)
	{
		if (!(actor == null) && !(actor.gameObject == null))
		{
			SceneUtils.SetLayer(actor.gameObject, GameLayer.IgnoreFullScreenEffects);
			Hashtable args = iTween.Hash("y", 0.8f, "time", 0.4f, "easeType", iTween.EaseType.easeOutQuad, "name", "position", "isLocal", true);
			Hashtable args2 = iTween.Hash("x", 1.08f, "z", 1.08f, "time", 0.4f, "easeType", iTween.EaseType.easeOutQuad, "name", "scale");
			iTween.StopByName(actor.gameObject, "position");
			iTween.StopByName(actor.gameObject, "scale");
			iTween.MoveTo(actor.gameObject, args);
			iTween.ScaleTo(actor.gameObject, args2);
		}
	}

	private void RemoveMobileTargettingEffectFromActor(Actor actor)
	{
		if (!(actor == null) && !(actor.gameObject == null))
		{
			SceneUtils.SetLayer(actor.gameObject, GameLayer.Default);
			SceneUtils.SetLayer(actor.GetMeshRenderer().gameObject, GameLayer.CardRaycast);
			Hashtable args = iTween.Hash("x", 0f, "y", 0f, "z", 0f, "time", 0.5f, "easeType", iTween.EaseType.easeOutQuad, "name", "position", "isLocal", true);
			Hashtable args2 = iTween.Hash("x", 1f, "z", 1f, "time", 0.4f, "easeType", iTween.EaseType.easeOutQuad, "name", "scale");
			iTween.StopByName(actor.gameObject, "position");
			iTween.StopByName(actor.gameObject, "scale");
			iTween.MoveTo(actor.gameObject, args);
			iTween.ScaleTo(actor.gameObject, args2);
		}
	}

	private bool HandleMulliganHotkeys()
	{
		if (MulliganManager.Get() == null)
		{
			return false;
		}
		if (GameMgr.Get().IsBattlegrounds())
		{
			return false;
		}
		if (HearthstoneApplication.IsInternal() && InputCollection.GetKeyUp(KeyCode.Escape) && !GameMgr.Get().IsTutorial() && !PlatformSettings.IsMobile())
		{
			MulliganManager.Get().SetAllMulliganCardsToHold();
			DoEndTurnButton();
			TurnStartManager.Get().BeginListeningForTurnEvents();
			MulliganManager.Get().SkipMulliganForDev();
			return true;
		}
		return false;
	}

	private bool HandleUniversalHotkeys()
	{
		return false;
	}

	private bool HandleGameHotkeys()
	{
		if (GameState.Get() != null && GameState.Get().IsMulliganManagerActive())
		{
			return false;
		}
		if (InputCollection.GetKeyUp(KeyCode.Escape))
		{
			return CancelOption();
		}
		return false;
	}

	private void ShowBullseyeIfNeeded()
	{
		if (!(TargetReticleManager.Get() == null) && TargetReticleManager.Get().IsActive())
		{
			bool show = m_mousedOverCard != null && GameState.Get().IsValidOptionTarget(m_mousedOverCard.GetEntity(), checkInputEnabled: false);
			TargetReticleManager.Get().ShowBullseye(show);
		}
	}

	private bool EntityIsPoisonousForSkullPreview(Entity entity)
	{
		if (entity.GetRealTimeAttack() <= 0)
		{
			return false;
		}
		if (entity.GetRealTimeIsPoisonous())
		{
			return true;
		}
		if (entity.IsHero())
		{
			Card weaponCard = entity.GetWeaponCard();
			Entity entity2 = (weaponCard ? weaponCard.GetEntity() : null);
			if (entity2 != null && entity2.GetRealTimeIsPoisonous())
			{
				return true;
			}
		}
		return false;
	}

	private void ShowSkullIfNeeded()
	{
		if (GetBattlecrySourceCard() != null)
		{
			return;
		}
		Network.Options.Option.SubOption selectedNetworkSubOption = GameState.Get().GetSelectedNetworkSubOption();
		if (selectedNetworkSubOption == null)
		{
			return;
		}
		Entity entity = GameState.Get().GetEntity(selectedNetworkSubOption.ID);
		if (entity == null || (!entity.IsMinion() && !entity.IsHero()))
		{
			return;
		}
		Entity entity2 = m_mousedOverCard.GetEntity();
		if ((!entity2.IsMinion() && !entity2.IsHero()) || !GameState.Get().IsValidOptionTarget(entity2, checkInputEnabled: false) || entity2.IsObfuscated())
		{
			return;
		}
		int num = entity.GetRealTimeAttack();
		if (entity2.HasTag(GAME_TAG.HEAVILY_ARMORED))
		{
			num = Mathf.Min(num, 1);
		}
		if (entity2.CanBeDamagedRealTime() && (num >= entity2.GetRealTimeRemainingHP() || (EntityIsPoisonousForSkullPreview(entity) && entity2.IsMinion())))
		{
			if (EntityIsPoisonousForSkullPreview(entity))
			{
				DamageSplatSpell damageSplatSpell = m_mousedOverCard.ActivateActorSpell(SpellType.DAMAGE) as DamageSplatSpell;
				if (damageSplatSpell != null)
				{
					damageSplatSpell.SetPoisonous(isPoisonous: true);
					damageSplatSpell.ActivateState(SpellStateType.IDLE);
					damageSplatSpell.transform.localScale = Vector3.zero;
					iTween.ScaleTo(damageSplatSpell.gameObject, iTween.Hash("scale", Vector3.one, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
				}
			}
			else
			{
				Spell spell = m_mousedOverCard.ActivateActorSpell(SpellType.SKULL);
				if (spell != null)
				{
					spell.transform.localScale = Vector3.zero;
					iTween.ScaleTo(spell.gameObject, iTween.Hash("scale", Vector3.one, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
				}
			}
		}
		int num2 = entity2.GetRealTimeAttack();
		if (entity.HasTag(GAME_TAG.HEAVILY_ARMORED))
		{
			num2 = Mathf.Min(num2, 1);
		}
		if (!entity.CanBeDamagedRealTime() || (num2 < entity.GetRealTimeRemainingHP() && (!EntityIsPoisonousForSkullPreview(entity2) || !entity.IsMinion())))
		{
			return;
		}
		if (EntityIsPoisonousForSkullPreview(entity2))
		{
			DamageSplatSpell damageSplatSpell2 = entity.GetCard().ActivateActorSpell(SpellType.DAMAGE) as DamageSplatSpell;
			if (damageSplatSpell2 != null)
			{
				damageSplatSpell2.SetPoisonous(isPoisonous: true);
				damageSplatSpell2.ActivateState(SpellStateType.IDLE);
				damageSplatSpell2.transform.localScale = Vector3.zero;
				iTween.ScaleTo(damageSplatSpell2.gameObject, iTween.Hash("scale", Vector3.one, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
			}
		}
		else
		{
			Spell spell2 = entity.GetCard().ActivateActorSpell(SpellType.SKULL);
			if (spell2 != null)
			{
				spell2.transform.localScale = Vector3.zero;
				iTween.ScaleTo(spell2.gameObject, iTween.Hash("scale", Vector3.one, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
			}
		}
	}

	private void DisableSkullIfNeeded(Card mousedOverCard)
	{
		Spell actorSpell = mousedOverCard.GetActorSpell(SpellType.SKULL);
		if (actorSpell != null)
		{
			iTween.Stop(actorSpell.gameObject);
			actorSpell.transform.localScale = Vector3.zero;
			actorSpell.Deactivate();
		}
		Spell actorSpell2 = mousedOverCard.GetActorSpell(SpellType.DAMAGE);
		if (actorSpell2 != null && mousedOverCard.GetEntity().IsMinion())
		{
			iTween.Stop(actorSpell2.gameObject);
			actorSpell2.transform.localScale = Vector3.zero;
			actorSpell2.Deactivate();
		}
		if (GameState.Get() == null)
		{
			return;
		}
		Network.Options.Option.SubOption selectedNetworkSubOption = GameState.Get().GetSelectedNetworkSubOption();
		if (selectedNetworkSubOption == null)
		{
			return;
		}
		Entity entity = GameState.Get().GetEntity(selectedNetworkSubOption.ID);
		if (entity == null)
		{
			return;
		}
		Card card = entity.GetCard();
		if (!(card == null))
		{
			actorSpell = card.GetActorSpell(SpellType.SKULL);
			if (actorSpell != null)
			{
				iTween.Stop(actorSpell.gameObject);
				actorSpell.transform.localScale = Vector3.zero;
				actorSpell.Deactivate();
			}
			actorSpell2 = card.GetActorSpell(SpellType.DAMAGE);
			if (actorSpell2 != null)
			{
				iTween.Stop(actorSpell2.gameObject);
				actorSpell2.transform.localScale = Vector3.zero;
				actorSpell2.Deactivate();
			}
		}
	}

	private void HandleMouseOverCard(Card card)
	{
		if (card.IsInputEnabled())
		{
			GameState gameState = GameState.Get();
			m_mousedOverCard = card;
			bool flag = gameState.IsFriendlySidePlayerTurn() && (bool)TargetReticleManager.Get() && TargetReticleManager.Get().IsActive();
			if (!PermitDecisionMakingInput())
			{
				flag = false;
			}
			if (gameState.IsMainPhase() && m_heldCard == null && !ChoiceCardMgr.Get().HasSubOption() && !flag && (!UniversalInputManager.Get().IsTouchMode() || card.gameObject == m_lastObjectMousedDown))
			{
				SetShouldShowTooltip();
			}
			card.NotifyMousedOver();
			if (gameState.IsMulliganManagerActive() && card.GetEntity().IsControlledByFriendlySidePlayer() && card.GetZone() is ZoneHand && !UniversalInputManager.UsePhoneUI)
			{
				TooltipPanelManager.Get().UpdateKeywordHelpForMulliganCard(card.GetEntity(), card.GetActor());
			}
			ShowBullseyeIfNeeded();
			ShowSkullIfNeeded();
		}
	}

	public void NotifyCardDestroyed(Card destroyedCard)
	{
		if (destroyedCard == m_mousedOverCard)
		{
			HandleMouseOffCard();
		}
	}

	private void HandleMouseOffCard()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.UP);
		Card mousedOverCard = m_mousedOverCard;
		m_mousedOverCard = null;
		mousedOverCard.HideTooltip();
		mousedOverCard.NotifyMousedOut();
		ShowBullseyeIfNeeded();
		DisableSkullIfNeeded(mousedOverCard);
	}

	public void HandleMemberClick()
	{
		if (!(m_mousedOverObject == null))
		{
			return;
		}
		if (UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.PlayAreaCollision, out var hitInfo))
		{
			if (GameState.Get() == null || GameState.Get().IsMulliganManagerActive() || UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out var _))
			{
				return;
			}
			GameObject mouseClickDustEffectPrefab = Board.Get().GetMouseClickDustEffectPrefab();
			if (mouseClickDustEffectPrefab == null)
			{
				return;
			}
			GameObject gameObject = Object.Instantiate(mouseClickDustEffectPrefab);
			gameObject.transform.position = hitInfo.point;
			ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
			if (componentsInChildren == null)
			{
				return;
			}
			Vector3 euler = new Vector3(Input.GetAxis("Mouse Y") * 40f, Input.GetAxis("Mouse X") * 40f, 0f);
			ParticleSystem[] array = componentsInChildren;
			foreach (ParticleSystem particleSystem in array)
			{
				if (particleSystem.name == "Rocks")
				{
					particleSystem.transform.localRotation = Quaternion.Euler(euler);
				}
				particleSystem.Play();
			}
			string[] array2 = null;
			GameEntity gameEntity = GameState.Get().GetGameEntity();
			if (gameEntity != null)
			{
				array2 = gameEntity.GetOverrideBoardClickSounds();
			}
			if (array2 == null || array2.Length == 0)
			{
				array2 = DEFAULT_BOARD_CLICK_SOUNDS;
			}
			string text = array2[Random.Range(0, array2.Length)];
			SoundManager.Get().LoadAndPlay(text, gameObject);
		}
		else if (Gameplay.Get() != null)
		{
			SoundManager.Get().LoadAndPlay("UI_MouseClick_01.prefab:fa537702a0db1c3478c989967458788b");
		}
	}

	public bool MouseIsMoving(float tolerance)
	{
		if (Mathf.Abs(Input.GetAxis("Mouse X")) <= tolerance && Mathf.Abs(Input.GetAxis("Mouse Y")) <= tolerance)
		{
			return false;
		}
		return true;
	}

	public bool MouseIsMoving()
	{
		return MouseIsMoving(0f);
	}

	private void ShowTooltipIfNecessary()
	{
		if (m_mousedOverCard == null || !m_mousedOverCard.GetShouldShowTooltip())
		{
			return;
		}
		m_mousedOverTimer += Time.unscaledDeltaTime;
		if (m_mousedOverCard.IsActorReady())
		{
			if (GameState.Get().GetBooleanGameOption(GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN))
			{
				m_mousedOverCard.ShowTooltip();
			}
			else if (m_mousedOverCard.GetZone() is ZoneHand)
			{
				m_mousedOverCard.ShowTooltip();
			}
			else if (m_mousedOverTimer >= m_MouseOverDelay)
			{
				m_mousedOverCard.ShowTooltip();
			}
		}
	}

	private void ShowTooltipZone(GameObject hitObject, TooltipZone tooltip)
	{
		HideBigViewCardBacks();
		GameState gameState = GameState.Get();
		if (gameState.IsMulliganManagerActive())
		{
			return;
		}
		GameEntity gameEntity = gameState.GetGameEntity();
		if (gameEntity == null || gameEntity.GetGameOptions().GetBooleanOption(GameEntityOption.DISABLE_TOOLTIPS) || gameEntity.NotifyOfTooltipDisplay(tooltip))
		{
			return;
		}
		ZoneTooltipSettings zoneTooltipSettings = gameEntity.GetZoneTooltipSettings();
		ManaCrystalMgr component = tooltip.targetObject.GetComponent<ManaCrystalMgr>();
		if (component != null && zoneTooltipSettings.FriendlyMana.Allowed)
		{
			string headline = null;
			string description = null;
			if (zoneTooltipSettings.FriendlyMana.GetTooltipOverrideContent(ref headline, ref description))
			{
				ShowTooltipInZone(tooltip, headline, description);
			}
			if (component.ShouldShowTooltip(ManaCrystalType.DEFAULT))
			{
				int num = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.OVERLOAD_OWED);
				if (num > 0)
				{
					string headline2 = GameStrings.Format("GAMEPLAY_TOOLTIP_MANA_OVERLOAD_HEADLINE");
					string description2 = GameStrings.Format("GAMEPLAY_TOOLTIP_MANA_OVERLOAD_DESCRIPTION", num);
					ShowTooltipInZone(tooltip, headline2, description2);
				}
				else
				{
					ShowTooltipInZone(tooltip, GameStrings.Get("GAMEPLAY_TOOLTIP_MANA_HEADLINE"), GameStrings.Get("GAMEPLAY_TOOLTIP_MANA_DESCRIPTION"));
				}
				int num2 = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.OVERLOAD_LOCKED);
				if (num2 > 0)
				{
					string headline3 = GameStrings.Format("GAMEPLAY_TOOLTIP_MANA_LOCKED_HEADLINE");
					string description3 = GameStrings.Format("GAMEPLAY_TOOLTIP_MANA_LOCKED_DESCRIPTION", num2);
					AddTooltipInZone(tooltip, headline3, description3);
				}
			}
			else if (component.ShouldShowTooltip(ManaCrystalType.COIN))
			{
				ShowTooltipInZone(tooltip, GameStrings.Get("GAMEPLAY_TOOLTIP_MANA_COIN_HEADLINE"), GameStrings.Get("GAMEPLAY_TOOLTIP_MANA_COIN_DESCRIPTION"));
			}
			return;
		}
		ZoneDeck component2 = tooltip.targetObject.GetComponent<ZoneDeck>();
		if (component2 != null)
		{
			if (component2.m_Side == Player.Side.FRIENDLY)
			{
				if (zoneTooltipSettings.FriendlyDeck.Allowed)
				{
					Vector3 localOffset = Vector3.zero;
					string headline4 = null;
					string description4 = null;
					if (!zoneTooltipSettings.FriendlyDeck.GetTooltipOverrideContent(ref headline4, ref description4))
					{
						if (component2.IsFatigued())
						{
							if ((bool)UniversalInputManager.UsePhoneUI)
							{
								localOffset = new Vector3(0f, 0f, 0.562f);
							}
							headline4 = GameStrings.Get("GAMEPLAY_TOOLTIP_FATIGUE_DECK_HEADLINE");
							description4 = GameStrings.Get("GAMEPLAY_TOOLTIP_FATIGUE_DECK_DESCRIPTION");
						}
						else
						{
							headline4 = GameStrings.Format("GAMEPLAY_TOOLTIP_DECK_HEADLINE");
							description4 = GameStrings.Format("GAMEPLAY_TOOLTIP_DECK_DESCRIPTION", component2.GetCards().Count);
						}
					}
					ShowTooltipInZone(tooltip, headline4, description4, localOffset);
				}
				if (!(component2.m_playerHandTooltipZone != null) || !zoneTooltipSettings.FriendlyHand.Allowed)
				{
					return;
				}
				Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
				int count = friendlySidePlayer.GetHandZone().GetCards().Count;
				if (count < 5 || GameMgr.Get().IsTutorial())
				{
					return;
				}
				string headline5 = null;
				string description5 = null;
				if (!zoneTooltipSettings.FriendlyHand.GetTooltipOverrideContent(ref headline5, ref description5))
				{
					headline5 = GameStrings.Get("GAMEPLAY_TOOLTIP_HAND_HEADLINE");
					description5 = GameStrings.Format("GAMEPLAY_TOOLTIP_HAND_DESCRIPTION", count);
					if (count >= friendlySidePlayer.GetTag(GAME_TAG.MAXHANDSIZE))
					{
						headline5 = GameStrings.Get("GAMEPLAY_TOOLTIP_HAND_FULL_HEADLINE");
						description5 = GameStrings.Format("GAMEPLAY_TOOLTIP_HAND_FULL_DESCRIPTION", count);
					}
				}
				ShowTooltipInZone(component2.m_playerHandTooltipZone, headline5, description5);
			}
			else
			{
				if (component2.m_Side != Player.Side.OPPOSING)
				{
					return;
				}
				if (zoneTooltipSettings.EnemyDeck.Allowed)
				{
					string headline6 = null;
					string description6 = null;
					if (!zoneTooltipSettings.EnemyDeck.GetTooltipOverrideContent(ref headline6, ref description6))
					{
						if (component2.IsFatigued())
						{
							headline6 = GameStrings.Get("GAMEPLAY_TOOLTIP_FATIGUE_ENEMYDECK_HEADLINE");
							description6 = GameStrings.Get("GAMEPLAY_TOOLTIP_FATIGUE_ENEMYDECK_DESCRIPTION");
						}
						else
						{
							headline6 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYDECK_HEADLINE");
							description6 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYDECK_DESC", component2.GetCards().Count);
						}
					}
					ShowTooltipInZone(tooltip, headline6, description6);
					if (zoneTooltipSettings.EnemyDeck.GetTooltipOverrideContent(ref headline6, ref description6, 1))
					{
						AddTooltipInZone(tooltip, headline6, description6);
					}
				}
				if (component2.m_playerHandTooltipZone != null && zoneTooltipSettings.EnemyHand.Allowed)
				{
					int count2 = GameState.Get().GetOpposingSidePlayer().GetHandZone()
						.GetCards()
						.Count;
					if (count2 >= 5 && !GameMgr.Get().IsTutorial())
					{
						string headline7 = null;
						string description7 = null;
						if (!zoneTooltipSettings.EnemyHand.GetTooltipOverrideContent(ref headline7, ref description7))
						{
							headline7 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYHAND_HEADLINE");
							description7 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYHAND_DESC", count2);
						}
						ShowTooltipInZone(component2.m_playerHandTooltipZone, headline7, description7);
					}
				}
				int num3 = GameState.Get().GetOpposingSidePlayer().GetTag(GAME_TAG.OVERLOAD_OWED);
				if (zoneTooltipSettings.EnemyMana.Allowed && num3 > 0)
				{
					if ((bool)UniversalInputManager.UsePhoneUI && component2.m_playerHandTooltipZone != null)
					{
						string headline8 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_HEADLINE");
						string description8 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_DESC", num3);
						AddTooltipInZone(component2.m_playerHandTooltipZone, headline8, description8);
					}
					else if (!UniversalInputManager.UsePhoneUI && component2.m_playerManaTooltipZone != null)
					{
						string headline9 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_HEADLINE");
						string description9 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_DESC", num3);
						ShowTooltipInZone(component2.m_playerManaTooltipZone, headline9, description9);
					}
				}
			}
			return;
		}
		ZoneHand component3 = tooltip.targetObject.GetComponent<ZoneHand>();
		if (component3 != null && component3.m_Side == Player.Side.OPPOSING)
		{
			if (GameMgr.Get().IsTutorial())
			{
				ShowTooltipInZone(tooltip, GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYHAND_HEADLINE"), GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYHAND_DESC_TUT"));
			}
			else
			{
				if (!zoneTooltipSettings.EnemyHand.Allowed)
				{
					return;
				}
				string headline10 = null;
				string description10 = null;
				if (!zoneTooltipSettings.EnemyHand.GetTooltipOverrideContent(ref headline10, ref description10))
				{
					int cardCount = component3.GetCardCount();
					if (cardCount == 1)
					{
						headline10 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYHAND_HEADLINE");
						description10 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYHAND_DESC_SINGLE", cardCount);
					}
					else
					{
						headline10 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYHAND_HEADLINE");
						description10 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYHAND_DESC", cardCount);
					}
				}
				ShowTooltipInZone(tooltip, headline10, description10);
				if ((bool)UniversalInputManager.UsePhoneUI && zoneTooltipSettings.EnemyMana.Allowed)
				{
					int num4 = GameState.Get().GetOpposingSidePlayer().GetTag(GAME_TAG.OVERLOAD_OWED);
					if (num4 > 0)
					{
						string headline11 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_HEADLINE");
						string description11 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_DESC", num4);
						AddTooltipInZone(tooltip, headline11, description11);
					}
				}
			}
			return;
		}
		ManaCounter component4 = tooltip.targetObject.GetComponent<ManaCounter>();
		if (component4 != null && component4.m_Side == Player.Side.OPPOSING && zoneTooltipSettings.EnemyMana.Allowed)
		{
			int num5 = GameState.Get().GetOpposingSidePlayer().GetTag(GAME_TAG.OVERLOAD_OWED);
			if (num5 > 0)
			{
				string headline12 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_HEADLINE");
				string description12 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_DESC", num5);
				ShowTooltipInZone(tooltip, headline12, description12);
			}
		}
	}

	private void AddTooltipInZone(TooltipZone tooltip, string headline, string description)
	{
		for (int i = 0; i < 10; i++)
		{
			if (!tooltip.IsShowingTooltip(i))
			{
				ShowTooltipInZone(tooltip, headline, description, Vector3.zero, i);
				return;
			}
		}
		Debug.LogError("You are trying to add too many tooltips. TooltipZone = [" + tooltip.gameObject.name + "] MAX_TOOLTIPS = [" + 10 + "]");
	}

	private void ShowTooltipInZone(TooltipZone tooltip, string headline, string description, int index = 0)
	{
		ShowTooltipInZone(tooltip, headline, description, Vector3.zero, index);
	}

	private void ShowTooltipInZone(TooltipZone tooltip, string headline, string description, Vector3 localOffset, int index = 0)
	{
		GameState.Get().GetGameEntity().NotifyOfTooltipZoneMouseOver(tooltip);
		if (UniversalInputManager.Get().IsTouchMode())
		{
			tooltip.ShowGameplayTooltipLarge(headline, description, localOffset, index);
		}
		else
		{
			tooltip.ShowGameplayTooltip(headline, description, localOffset, index);
		}
	}

	private void HideBigViewCardBacks()
	{
	}

	private void PredictSpentMana(Entity entity)
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		if ((!friendlySidePlayer.GetRealTimeSpellsCostHealth() || entity.GetRealTimeCardType() != TAG_CARDTYPE.SPELL) && !entity.GetRealTimeCardCostsHealth())
		{
			int num = entity.GetRealTimeCost() - friendlySidePlayer.GetRealTimeTempMana();
			if (friendlySidePlayer.GetRealTimeTempMana() > 0)
			{
				int num2 = Mathf.Clamp(entity.GetRealTimeCost(), 0, friendlySidePlayer.GetRealTimeTempMana());
				friendlySidePlayer.NotifyOfUsedTempMana(num2);
				ManaCrystalMgr.Get().DestroyTempManaCrystals(num2);
			}
			if (num > 0 && !entity.HasTag(GAME_TAG.RED_MANA_CRYSTALS))
			{
				friendlySidePlayer.NotifyOfSpentMana(num);
				ManaCrystalMgr.Get().UpdateSpentMana(num);
			}
			friendlySidePlayer.UpdateManaCounter();
			m_entitiesThatPredictedMana.Add(entity);
		}
	}

	private void RollbackSpentMana(Entity entity)
	{
		int num = m_entitiesThatPredictedMana.IndexOf(entity);
		if (num >= 0)
		{
			m_entitiesThatPredictedMana.RemoveAt(num);
			Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
			int num2 = -entity.GetRealTimeCost() + friendlySidePlayer.GetRealTimeTempMana();
			if (friendlySidePlayer.GetRealTimeTempMana() > 0)
			{
				int num3 = Mathf.Clamp(entity.GetRealTimeCost(), 0, friendlySidePlayer.GetRealTimeTempMana());
				friendlySidePlayer.NotifyOfUsedTempMana(-num3);
				ManaCrystalMgr.Get().AddTempManaCrystals(num3);
			}
			if (num2 < 0)
			{
				friendlySidePlayer.NotifyOfSpentMana(num2);
				ManaCrystalMgr.Get().UpdateSpentMana(num2);
			}
			friendlySidePlayer.UpdateManaCounter();
		}
	}

	public void OnManaCrystalMgrManaSpent()
	{
		if ((bool)m_mousedOverCard)
		{
			m_mousedOverCard.UpdateProposedManaUsage();
		}
	}

	private bool IsInZone(Entity entity, TAG_ZONE zoneTag)
	{
		return IsInZone(entity.GetCard(), zoneTag);
	}

	private bool IsInZone(Card card, TAG_ZONE zoneTag)
	{
		if (card.GetZone() == null)
		{
			return false;
		}
		return GameUtils.GetFinalZoneForEntity(card.GetEntity()) == zoneTag;
	}

	private void SetDragging(bool dragging)
	{
		m_dragging = dragging;
		GraphicsManager.Get()?.SetDraggingFramerate(dragging);
	}

	public bool RegisterPhoneHandShownListener(PhoneHandShownCallback callback)
	{
		return RegisterPhoneHandShownListener(callback, null);
	}

	public bool RegisterPhoneHandShownListener(PhoneHandShownCallback callback, object userData)
	{
		PhoneHandShownListener phoneHandShownListener = new PhoneHandShownListener();
		phoneHandShownListener.SetCallback(callback);
		phoneHandShownListener.SetUserData(userData);
		if (m_phoneHandShownListener.Contains(phoneHandShownListener))
		{
			return false;
		}
		m_phoneHandShownListener.Add(phoneHandShownListener);
		return true;
	}

	public bool RemovePhoneHandShownListener(PhoneHandShownCallback callback)
	{
		return RemovePhoneHandShownListener(callback, null);
	}

	public bool RemovePhoneHandShownListener(PhoneHandShownCallback callback, object userData)
	{
		PhoneHandShownListener phoneHandShownListener = new PhoneHandShownListener();
		phoneHandShownListener.SetCallback(callback);
		phoneHandShownListener.SetUserData(userData);
		return m_phoneHandShownListener.Remove(phoneHandShownListener);
	}

	public bool RegisterPhoneHandHiddenListener(PhoneHandHiddenCallback callback)
	{
		return RegisterPhoneHandHiddenListener(callback, null);
	}

	public bool RegisterPhoneHandHiddenListener(PhoneHandHiddenCallback callback, object userData)
	{
		PhoneHandHiddenListener phoneHandHiddenListener = new PhoneHandHiddenListener();
		phoneHandHiddenListener.SetCallback(callback);
		phoneHandHiddenListener.SetUserData(userData);
		if (m_phoneHandHiddenListener.Contains(phoneHandHiddenListener))
		{
			return false;
		}
		m_phoneHandHiddenListener.Add(phoneHandHiddenListener);
		return true;
	}

	public bool RemovePhoneHandHiddenListener(PhoneHandHiddenCallback callback)
	{
		return RemovePhoneHandHiddenListener(callback, null);
	}

	public bool RemovePhoneHandHiddenListener(PhoneHandHiddenCallback callback, object userData)
	{
		PhoneHandHiddenListener phoneHandHiddenListener = new PhoneHandHiddenListener();
		phoneHandHiddenListener.SetCallback(callback);
		phoneHandHiddenListener.SetUserData(userData);
		return m_phoneHandHiddenListener.Remove(phoneHandHiddenListener);
	}
}
