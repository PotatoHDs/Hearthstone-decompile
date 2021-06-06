using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using UnityEngine;

// Token: 0x02000321 RID: 801
public class InputManager : MonoBehaviour
{
	// Token: 0x06002D01 RID: 11521 RVA: 0x000E2600 File Offset: 0x000E0800
	private void Awake()
	{
		InputManager.s_instance = this;
		this.m_useHandEnlarge = UniversalInputManager.UsePhoneUI;
		this.SetDragging(this.m_dragging);
		if (GameState.Get() != null)
		{
			GameState.Get().RegisterOptionsReceivedListener(new GameState.OptionsReceivedCallback(this.OnOptionsReceived));
			GameState.Get().RegisterOptionRejectedListener(new GameState.OptionRejectedCallback(this.OnOptionRejected), null);
			GameState.Get().RegisterTurnTimerUpdateListener(new GameState.TurnTimerUpdateCallback(this.OnTurnTimerUpdate));
			GameState.Get().RegisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
		}
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x06002D02 RID: 11522 RVA: 0x000E26AC File Offset: 0x000E08AC
	private void OnDestroy()
	{
		if (GameState.Get() != null)
		{
			GameState.Get().UnregisterOptionsReceivedListener(new GameState.OptionsReceivedCallback(this.OnOptionsReceived));
			GameState.Get().UnregisterOptionRejectedListener(new GameState.OptionRejectedCallback(this.OnOptionRejected), null);
			GameState.Get().UnregisterTurnTimerUpdateListener(new GameState.TurnTimerUpdateCallback(this.OnTurnTimerUpdate));
			GameState.Get().UnregisterGameOverListener(new GameState.GameOverCallback(this.OnGameOver), null);
		}
		FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		InputManager.s_instance = null;
	}

	// Token: 0x06002D03 RID: 11523 RVA: 0x000E273B File Offset: 0x000E093B
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.DisableInput();
	}

	// Token: 0x06002D04 RID: 11524 RVA: 0x000E2744 File Offset: 0x000E0944
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
		RaycastHit raycastHit;
		if (!actor.IsColliderEnabled())
		{
			actor.ToggleCollider(true);
			UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out raycastHit);
			actor.ToggleCollider(false);
		}
		else
		{
			UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out raycastHit);
		}
		if (raycastHit.collider != null)
		{
			Actor actor2 = SceneUtils.FindComponentInParents<Actor>(raycastHit.transform);
			return !(actor2 == null) && actor2.GetCard() == wantedCard;
		}
		return false;
	}

	// Token: 0x06002D05 RID: 11525 RVA: 0x000E27D8 File Offset: 0x000E09D8
	private bool ShouldCancelTargeting(bool hitBattlefieldHitbox)
	{
		bool result = false;
		if (!hitBattlefieldHitbox && this.GetBattlecrySourceCard() == null && ChoiceCardMgr.Get().GetSubOptionParentCard() == null)
		{
			result = true;
			if (UniversalInputManager.UsePhoneUI)
			{
				bool flag = UniversalInputManager.Get().InputHitAnyObject(Camera.main, GameLayer.InvisibleHitBox3);
				if (this.m_targettingHeroPower || GameState.Get().IsSelectedOptionFriendlyHero() || flag)
				{
					result = false;
				}
			}
			else if (GameState.Get().IsSelectedOptionFriendlyHero())
			{
				Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
				Card weaponCard = GameState.Get().GetFriendlySidePlayer().GetWeaponCard();
				if (this.IsInputOverCard(heroCard) || this.IsInputOverCard(weaponCard))
				{
					result = false;
				}
			}
			else if (GameState.Get().IsSelectedOptionFriendlyHeroPower())
			{
				Card heroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
				if (this.IsInputOverCard(heroPowerCard))
				{
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x06002D06 RID: 11526 RVA: 0x000E28BC File Offset: 0x000E0ABC
	private void Update()
	{
		if (!this.m_checkForInput)
		{
			return;
		}
		if (UniversalInputManager.Get().GetMouseButtonDown(0))
		{
			this.HandleLeftMouseDown();
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			this.m_touchDraggingCard = false;
			this.HandleLeftMouseUp();
		}
		if (UniversalInputManager.Get().GetMouseButtonDown(1))
		{
			this.HandleRightMouseDown();
		}
		if (UniversalInputManager.Get().GetMouseButtonUp(1))
		{
			this.HandleRightMouseUp();
		}
		this.HandleMouseMove();
		if (this.m_leftMouseButtonIsDown && this.m_heldCard == null)
		{
			this.HandleUpdateWhileLeftMouseButtonIsDown();
			if (UniversalInputManager.Get().IsTouchMode() && !this.m_touchDraggingCard)
			{
				this.HandleUpdateWhileNotHoldingCard();
			}
		}
		else if (this.m_heldCard == null)
		{
			this.HandleUpdateWhileNotHoldingCard();
		}
		if (GameState.Get() == null || GameState.Get().GetFriendlySidePlayer() == null || GameState.Get().GetFriendlySidePlayer().IsLocalUser())
		{
			bool flag = UniversalInputManager.Get().InputHitAnyObject(Camera.main, GameLayer.InvisibleHitBox2);
			if (TargetReticleManager.Get() && TargetReticleManager.Get().IsActive())
			{
				if (this.ShouldCancelTargeting(flag))
				{
					this.CancelOption(false);
					if (this.m_useHandEnlarge)
					{
						this.m_myHandZone.SetFriendlyHeroTargetingMode(false);
					}
					if (this.m_heldCard != null)
					{
						this.PositionHeldCard();
					}
				}
				else
				{
					TargetReticleManager.Get().UpdateArrowPosition();
					if (this.m_heldCard != null)
					{
						this.m_myHandZone.OnCardHeld(this.m_heldCard);
					}
				}
			}
			else if (this.m_heldCard)
			{
				this.HandleUpdateWhileHoldingCard(flag);
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
		this.ShowTooltipIfNecessary();
	}

	// Token: 0x06002D07 RID: 11527 RVA: 0x000E2A91 File Offset: 0x000E0C91
	public static InputManager Get()
	{
		return InputManager.s_instance;
	}

	// Token: 0x06002D08 RID: 11528 RVA: 0x000E2A98 File Offset: 0x000E0C98
	public bool HandleKeyboardInput()
	{
		if (this.HandleUniversalHotkeys())
		{
			return true;
		}
		if (GameState.Get() != null && GameState.Get().IsMulliganManagerActive())
		{
			return this.HandleMulliganHotkeys();
		}
		return this.HandleGameHotkeys();
	}

	// Token: 0x06002D09 RID: 11529 RVA: 0x000E2AC4 File Offset: 0x000E0CC4
	public Card GetMousedOverCard()
	{
		return this.m_mousedOverCard;
	}

	// Token: 0x06002D0A RID: 11530 RVA: 0x000E2ACC File Offset: 0x000E0CCC
	public void SetMousedOverCard(Card card)
	{
		if (this.m_mousedOverCard == card)
		{
			return;
		}
		if (this.m_mousedOverCard != null && !(this.m_mousedOverCard.GetZone() is ZoneHand))
		{
			this.HandleMouseOffCard();
		}
		if (!card.IsInputEnabled())
		{
			return;
		}
		this.m_mousedOverCard = card;
		card.NotifyMousedOver();
	}

	// Token: 0x06002D0B RID: 11531 RVA: 0x000E2B24 File Offset: 0x000E0D24
	public Card GetBattlecrySourceCard()
	{
		return this.m_battlecrySourceCard;
	}

	// Token: 0x06002D0C RID: 11532 RVA: 0x000E2B2C File Offset: 0x000E0D2C
	public void StartWatchingForInput()
	{
		if (this.m_checkForInput)
		{
			return;
		}
		this.m_checkForInput = true;
		foreach (Zone zone in ZoneMgr.Get().GetZones())
		{
			if (zone.m_Side == Player.Side.FRIENDLY)
			{
				if (zone is ZoneHand)
				{
					this.m_myHandZone = (ZoneHand)zone;
				}
				else if (zone is ZonePlay)
				{
					this.m_myPlayZone = (ZonePlay)zone;
				}
				else if (zone is ZoneWeapon)
				{
					this.m_myWeaponZone = (ZoneWeapon)zone;
				}
			}
			else if (zone is ZonePlay)
			{
				this.m_enemyPlayZone = (ZonePlay)zone;
			}
			else if (zone is ZoneHand)
			{
				this.m_enemyHandZone = (ZoneHand)zone;
			}
		}
	}

	// Token: 0x06002D0D RID: 11533 RVA: 0x000E2C04 File Offset: 0x000E0E04
	public void DisableInput()
	{
		this.m_checkForInput = false;
		this.HandleMouseOff();
		if (TargetReticleManager.Get())
		{
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(false);
		}
	}

	// Token: 0x06002D0E RID: 11534 RVA: 0x000E2C2C File Offset: 0x000E0E2C
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

	// Token: 0x06002D0F RID: 11535 RVA: 0x000E2C73 File Offset: 0x000E0E73
	public Card GetHeldCard()
	{
		return this.m_heldCard;
	}

	// Token: 0x06002D10 RID: 11536 RVA: 0x000E2C7B File Offset: 0x000E0E7B
	public void EnableInput()
	{
		this.m_checkForInput = true;
	}

	// Token: 0x06002D11 RID: 11537 RVA: 0x000E2C84 File Offset: 0x000E0E84
	public void OnMulliganEnded()
	{
		if (this.m_mousedOverCard)
		{
			this.SetShouldShowTooltip();
		}
	}

	// Token: 0x06002D12 RID: 11538 RVA: 0x000E2C99 File Offset: 0x000E0E99
	private void SetShouldShowTooltip()
	{
		this.m_mousedOverTimer = 0f;
		this.m_mousedOverCard.SetShouldShowTooltip();
	}

	// Token: 0x17000504 RID: 1284
	// (get) Token: 0x06002D13 RID: 11539 RVA: 0x000E2CB1 File Offset: 0x000E0EB1
	public bool LeftMouseButtonDown
	{
		get
		{
			return this.m_leftMouseButtonIsDown;
		}
	}

	// Token: 0x17000505 RID: 1285
	// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000E2CB9 File Offset: 0x000E0EB9
	public Vector3 LastMouseDownPosition
	{
		get
		{
			return this.m_lastMouseDownPosition;
		}
	}

	// Token: 0x06002D15 RID: 11541 RVA: 0x000E2CC1 File Offset: 0x000E0EC1
	public ZoneHand GetFriendlyHand()
	{
		return this.m_myHandZone;
	}

	// Token: 0x06002D16 RID: 11542 RVA: 0x000E2CC9 File Offset: 0x000E0EC9
	public ZoneHand GetEnemyHand()
	{
		return this.m_enemyHandZone;
	}

	// Token: 0x06002D17 RID: 11543 RVA: 0x000E2CD1 File Offset: 0x000E0ED1
	public bool UseHandEnlarge()
	{
		return this.m_useHandEnlarge;
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x000E2CD9 File Offset: 0x000E0ED9
	public void SetHandEnlarge(bool set)
	{
		this.m_useHandEnlarge = set;
	}

	// Token: 0x06002D19 RID: 11545 RVA: 0x000E2CE2 File Offset: 0x000E0EE2
	public bool DoesHideHandAfterPlayingCard()
	{
		return this.m_hideHandAfterPlayingCard;
	}

	// Token: 0x06002D1A RID: 11546 RVA: 0x000E2CEA File Offset: 0x000E0EEA
	public void SetHideHandAfterPlayingCard(bool set)
	{
		this.m_hideHandAfterPlayingCard = set;
	}

	// Token: 0x06002D1B RID: 11547 RVA: 0x000E2CF3 File Offset: 0x000E0EF3
	public bool DropHeldCard()
	{
		return this.DropHeldCard(false);
	}

	// Token: 0x06002D1C RID: 11548 RVA: 0x000E2CFC File Offset: 0x000E0EFC
	private void HandleLeftMouseDown()
	{
		this.m_touchedDownOnSmallHand = false;
		bool flag = true;
		GameObject gameObject = null;
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out raycastHit))
		{
			gameObject = raycastHit.collider.gameObject;
			if (gameObject.GetComponent<EndTurnButtonReminder>() != null)
			{
				return;
			}
			CardStandIn cardStandIn = SceneUtils.FindComponentInParents<CardStandIn>(raycastHit.transform);
			if (cardStandIn != null && GameState.Get() != null && !GameState.Get().IsMulliganManagerActive())
			{
				Card linkedCard = cardStandIn.linkedCard;
				if (this.IsCancelingBattlecryCard(linkedCard))
				{
					return;
				}
				if (this.m_useHandEnlarge && !this.m_myHandZone.HandEnlarged())
				{
					this.m_leftMouseButtonIsDown = true;
					this.m_touchedDownOnSmallHand = true;
					return;
				}
				this.m_lastObjectMousedDown = cardStandIn.gameObject;
				this.m_lastMouseDownPosition = UniversalInputManager.Get().GetMousePosition();
				this.m_leftMouseButtonIsDown = true;
				if (UniversalInputManager.Get().IsTouchMode())
				{
					this.m_touchDraggingCard = this.m_myHandZone.TouchReceived();
					this.m_lastPreviewedCard = cardStandIn.linkedCard;
				}
				if (this.m_heldCard == null)
				{
					this.m_myHandZone.HandleInput();
					return;
				}
				return;
			}
			else
			{
				if (gameObject.GetComponent<EndTurnButton>() != null && this.PermitDecisionMakingInput())
				{
					EndTurnButton.Get().PlayPushDownAnimation();
					this.m_lastObjectMousedDown = raycastHit.collider.gameObject;
					return;
				}
				if (gameObject.GetComponent<GameOpenPack>() != null)
				{
					this.m_lastObjectMousedDown = raycastHit.collider.gameObject;
					return;
				}
				Actor actor = SceneUtils.FindComponentInParents<Actor>(raycastHit.transform);
				if (actor == null)
				{
					return;
				}
				Card card = actor.GetCard();
				if (UniversalInputManager.Get().IsTouchMode() && this.m_battlecrySourceCard != null && card == this.m_battlecrySourceCard)
				{
					this.SetDragging(true);
					TargetReticleManager.Get().ShowArrow(true);
					return;
				}
				if (card != null)
				{
					if (this.IsCancelingBattlecryCard(card))
					{
						return;
					}
					if (this.m_useHandEnlarge && this.m_myHandZone.HandEnlarged() && card.GetEntity().IsHeroPower() && card.GetEntity().IsControlledByLocalUser() && this.m_myHandZone.GetCardCount() > 1)
					{
						return;
					}
				}
				if (card != null)
				{
					this.m_lastObjectMousedDown = card.gameObject;
				}
				else if (actor.GetHistoryCard() != null)
				{
					this.m_lastObjectMousedDown = actor.transform.parent.gameObject;
				}
				else
				{
					Debug.LogWarning("You clicked on something that is not being handled by InputManager.  Alert The Brode!");
				}
				this.m_lastMouseDownPosition = UniversalInputManager.Get().GetMousePosition();
				this.m_leftMouseButtonIsDown = true;
				flag = (actor.GetEntity() != null && actor.GetEntity().IsGameModeButton());
			}
		}
		if (this.m_useHandEnlarge && this.m_myHandZone.HandEnlarged() && ChoiceCardMgr.Get().GetSubOptionParentCard() == null && (gameObject == null || flag))
		{
			this.HidePhoneHand();
		}
		this.HandleMemberClick();
	}

	// Token: 0x06002D1D RID: 11549 RVA: 0x000E2FD4 File Offset: 0x000E11D4
	private void ShowPhoneHand()
	{
		if (GameState.Get().IsMulliganPhaseNowOrPending() || GameState.Get().IsGameOver())
		{
			return;
		}
		if (this.m_useHandEnlarge && !this.m_myHandZone.HandEnlarged())
		{
			this.m_myHandZone.AddUpdateLayoutCompleteCallback(new Zone.UpdateLayoutCompleteCallback(this.OnHandEnlargeComplete));
			this.m_myHandZone.SetHandEnlarged(true);
			InputManager.PhoneHandShownListener[] array = this.m_phoneHandShownListener.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire();
			}
		}
	}

	// Token: 0x06002D1E RID: 11550 RVA: 0x000E3058 File Offset: 0x000E1258
	public void HidePhoneHand()
	{
		if (this.m_useHandEnlarge && this.m_myHandZone != null && this.m_myHandZone.HandEnlarged())
		{
			this.m_myHandZone.SetHandEnlarged(false);
			InputManager.PhoneHandHiddenListener[] array = this.m_phoneHandHiddenListener.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire();
			}
		}
	}

	// Token: 0x06002D1F RID: 11551 RVA: 0x000E30B6 File Offset: 0x000E12B6
	private void OnHandEnlargeComplete(Zone zone, object userData)
	{
		zone.RemoveUpdateLayoutCompleteCallback(new Zone.UpdateLayoutCompleteCallback(this.OnHandEnlargeComplete));
		if (this.m_leftMouseButtonIsDown && UniversalInputManager.Get().InputHitAnyObject(GameLayer.CardRaycast))
		{
			this.HandleLeftMouseDown();
		}
	}

	// Token: 0x06002D20 RID: 11552 RVA: 0x000E30E6 File Offset: 0x000E12E6
	private void HidePhoneHandIfOutOfServerPlays()
	{
		if (GameState.Get().HasHandPlays())
		{
			return;
		}
		this.HidePhoneHand();
	}

	// Token: 0x06002D21 RID: 11553 RVA: 0x000E30FC File Offset: 0x000E12FC
	private bool HasLocalHandPlays()
	{
		List<Card> cards = this.m_myHandZone.GetCards();
		if (cards.Count == 0)
		{
			return false;
		}
		int spendableManaCrystals = ManaCrystalMgr.Get().GetSpendableManaCrystals();
		using (List<Card>.Enumerator enumerator = cards.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetEntity().GetRealTimeCost() <= spendableManaCrystals)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06002D22 RID: 11554 RVA: 0x000E3178 File Offset: 0x000E1378
	private void HandleLeftMouseUp()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.UP);
		this.m_lastInputDrag = this.m_dragging;
		this.SetDragging(false);
		this.m_leftMouseButtonIsDown = false;
		this.m_targettingHeroPower = false;
		GameObject lastObjectMousedDown = this.m_lastObjectMousedDown;
		this.m_lastObjectMousedDown = null;
		if (UniversalInputManager.Get().WasTouchCanceled())
		{
			this.CancelOption(false);
			this.m_heldCard = null;
			return;
		}
		if (this.m_heldCard != null && (GameState.Get().GetResponseMode() == GameState.ResponseMode.OPTION || GameState.Get().GetResponseMode() == GameState.ResponseMode.NONE))
		{
			this.DropHeldCard();
			return;
		}
		bool flag = UniversalInputManager.Get().IsTouchMode() && GameState.Get().IsInTargetMode();
		bool flag2 = ChoiceCardMgr.Get().GetSubOptionParentCard() != null;
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out raycastHit))
		{
			GameObject gameObject = raycastHit.collider.gameObject;
			if (gameObject.GetComponent<EndTurnButtonReminder>() != null)
			{
				return;
			}
			if (gameObject.GetComponent<EndTurnButton>() != null && gameObject == lastObjectMousedDown && this.PermitDecisionMakingInput())
			{
				EndTurnButton.Get().PlayButtonUpAnimation();
				this.DoEndTurnButton();
			}
			else if (gameObject.GetComponent<GameOpenPack>() != null && gameObject == lastObjectMousedDown)
			{
				gameObject.GetComponent<GameOpenPack>().HandleClick();
			}
			else
			{
				Actor actor = SceneUtils.FindComponentInParents<Actor>(raycastHit.transform);
				if (actor != null)
				{
					Card card = actor.GetCard();
					if (card != null)
					{
						if ((card.gameObject == lastObjectMousedDown || this.m_lastInputDrag) && !this.IsCancelingBattlecryCard(card))
						{
							this.HandleClickOnCard(actor.GetCard().gameObject, card.gameObject == lastObjectMousedDown);
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
				CardStandIn cardStandIn = SceneUtils.FindComponentInParents<CardStandIn>(raycastHit.transform);
				if (cardStandIn != null)
				{
					if (this.m_useHandEnlarge && this.m_touchedDownOnSmallHand)
					{
						this.ShowPhoneHand();
					}
					if (lastObjectMousedDown == cardStandIn.gameObject && cardStandIn.linkedCard != null && GameState.Get() != null && !GameState.Get().IsMulliganManagerActive() && !this.IsCancelingBattlecryCard(cardStandIn.linkedCard))
					{
						this.HandleClickOnCard(cardStandIn.linkedCard.gameObject, true);
					}
				}
				if (UniversalInputManager.Get().IsTouchMode() && actor != null && ChoiceCardMgr.Get().GetSubOptionParentCard() != null)
				{
					using (List<Card>.Enumerator enumerator = ChoiceCardMgr.Get().GetFriendlyCards().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == actor.GetCard())
							{
								flag2 = false;
								break;
							}
						}
					}
				}
			}
		}
		if (flag)
		{
			this.CancelOption(false);
		}
		if (UniversalInputManager.Get().IsTouchMode() && flag2 && ChoiceCardMgr.Get().GetSubOptionParentCard() != null)
		{
			this.CancelSubOptionMode(false);
		}
	}

	// Token: 0x06002D23 RID: 11555 RVA: 0x000E34A0 File Offset: 0x000E16A0
	private void HandleRightMouseDown()
	{
		RaycastHit raycastHit;
		if (!UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out raycastHit))
		{
			return;
		}
		GameObject gameObject = raycastHit.collider.gameObject;
		if (gameObject.GetComponent<EndTurnButtonReminder>() != null)
		{
			return;
		}
		if (gameObject.GetComponent<EndTurnButton>() != null)
		{
			return;
		}
		Actor actor = SceneUtils.FindComponentInParents<Actor>(raycastHit.transform);
		if (actor == null)
		{
			return;
		}
		if (actor.GetCard() != null)
		{
			this.m_lastObjectRightMousedDown = actor.GetCard().gameObject;
			return;
		}
		if (actor.GetHistoryCard() != null)
		{
			this.m_lastObjectRightMousedDown = actor.transform.parent.gameObject;
			return;
		}
		Debug.LogWarning("You clicked on something that is not being handled by InputManager.  Alert The Brode!");
	}

	// Token: 0x06002D24 RID: 11556 RVA: 0x000E3550 File Offset: 0x000E1750
	private void HandleRightMouseUp()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.UP);
		GameObject lastObjectRightMousedDown = this.m_lastObjectRightMousedDown;
		this.m_lastObjectRightMousedDown = null;
		this.m_lastObjectMousedDown = null;
		this.m_leftMouseButtonIsDown = false;
		this.SetDragging(false);
		RaycastHit raycastHit;
		if (!UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out raycastHit))
		{
			this.HandleRightClick();
			return;
		}
		Actor actor = SceneUtils.FindComponentInParents<Actor>(raycastHit.transform);
		if (actor == null || actor.GetCard() == null)
		{
			this.HandleRightClick();
			return;
		}
		if (actor.GetCard().gameObject == lastObjectRightMousedDown)
		{
			this.HandleRightClickOnCard(actor.GetCard());
			return;
		}
		this.HandleRightClick();
	}

	// Token: 0x06002D25 RID: 11557 RVA: 0x000E35F4 File Offset: 0x000E17F4
	private void HandleRightClick()
	{
		if (this.CancelOption(false))
		{
			return;
		}
		if (EmoteHandler.Get() != null && EmoteHandler.Get().AreEmotesActive())
		{
			EmoteHandler.Get().HideEmotes();
		}
		if (EnemyEmoteHandler.Get() != null && EnemyEmoteHandler.Get().AreEmotesActive())
		{
			EnemyEmoteHandler.Get().HideEmotes();
		}
	}

	// Token: 0x06002D26 RID: 11558 RVA: 0x000E3654 File Offset: 0x000E1854
	private bool CancelOption(bool timeout = false)
	{
		bool result = false;
		GameState gameState = GameState.Get();
		if (gameState.IsInMainOptionMode())
		{
			gameState.CancelCurrentOptionMode();
		}
		if (this.CancelTargetMode())
		{
			result = true;
		}
		if (this.CancelSubOptionMode(timeout))
		{
			result = true;
		}
		if (this.DropHeldCard(true))
		{
			result = true;
		}
		if (this.m_mousedOverCard)
		{
			this.m_mousedOverCard.UpdateProposedManaUsage();
		}
		return result;
	}

	// Token: 0x06002D27 RID: 11559 RVA: 0x000E36B0 File Offset: 0x000E18B0
	private bool CancelTargetMode()
	{
		if (!GameState.Get().IsInTargetMode())
		{
			return false;
		}
		SoundManager.Get().LoadAndPlay("CancelAttack.prefab:9cde7207a78024e46aa5a0a657807845");
		if (this.m_mousedOverCard)
		{
			this.DisableSkullIfNeeded(this.m_mousedOverCard);
		}
		if (TargetReticleManager.Get())
		{
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(true);
		}
		this.ResetBattlecrySourceCard();
		this.CancelSubOptions();
		GameState.Get().CancelCurrentOptionMode();
		return true;
	}

	// Token: 0x06002D28 RID: 11560 RVA: 0x000E3726 File Offset: 0x000E1926
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
				base.StartCoroutine(this.WaitAndCancelSubOptionMode());
			}
			return false;
		}
		this.CancelSubOptions();
		GameState.Get().CancelCurrentOptionMode();
		return true;
	}

	// Token: 0x06002D29 RID: 11561 RVA: 0x000E3765 File Offset: 0x000E1965
	private IEnumerator WaitAndCancelSubOptionMode()
	{
		ChoiceCardMgr.Get().QuenePendingCancelSubOptions();
		while (ChoiceCardMgr.Get().IsWaitingToShowSubOptions())
		{
			yield return null;
		}
		if (ChoiceCardMgr.Get().HasPendingCancelSubOptions())
		{
			this.CancelSubOptions();
			if (GameState.Get().IsInSubOptionMode())
			{
				GameState.Get().CancelCurrentOptionMode();
			}
		}
		ChoiceCardMgr.Get().ClearPendingCancelSubOptions();
		yield break;
	}

	// Token: 0x06002D2A RID: 11562 RVA: 0x000E3774 File Offset: 0x000E1974
	private void PositionHeldCard()
	{
		Card heldCard = this.m_heldCard;
		Entity entity = heldCard.GetEntity();
		ZonePlay controllersPlayZone = this.GetControllersPlayZone(entity);
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.InvisibleHitBox2, out raycastHit))
		{
			if (!heldCard.IsOverPlayfield())
			{
				if (!GameState.Get().HasResponse(entity))
				{
					this.m_leftMouseButtonIsDown = false;
					this.m_lastObjectMousedDown = null;
					this.SetDragging(false);
					this.DropHeldCard();
					return;
				}
				heldCard.NotifyOverPlayfield();
			}
			if (entity.IsMinion() && this.GetNumberOfMinionsInPlay(controllersPlayZone) < GameState.Get().GetMaxFriendlyMinionsPerPlayer())
			{
				if (this.GetMoveMinionHoverTarget(heldCard))
				{
					controllersPlayZone.SortWithSpotForHeldCard(-1);
				}
				else
				{
					int num = this.PlayZoneSlotMousedOver(heldCard);
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
		RaycastHit raycastHit2;
		if (UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.DragPlane, out raycastHit2))
		{
			heldCard.transform.position = raycastHit2.point;
		}
	}

	// Token: 0x06002D2B RID: 11563 RVA: 0x000E3874 File Offset: 0x000E1A74
	private int GetNumberOfMinionsInPlay(ZonePlay play)
	{
		return play.GetCards().Count((Card c) => !c.IsBeingDragged);
	}

	// Token: 0x06002D2C RID: 11564 RVA: 0x000E38A0 File Offset: 0x000E1AA0
	private int PlayZoneSlotMousedOver(Card card)
	{
		ZonePlay controllersPlayZone = this.GetControllersPlayZone(card.GetEntity());
		int num = 0;
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.InvisibleHitBox2, out raycastHit))
		{
			int numberOfMinionsInPlay = this.GetNumberOfMinionsInPlay(controllersPlayZone);
			float slotWidth = controllersPlayZone.GetSlotWidth();
			float num2 = controllersPlayZone.transform.position.x - (float)(numberOfMinionsInPlay + 1) * slotWidth / 2f;
			num = (int)Mathf.Ceil((raycastHit.point.x - num2) / slotWidth - slotWidth / 2f);
			if (num < 0 || num > numberOfMinionsInPlay)
			{
				if (card.transform.position.x < controllersPlayZone.transform.position.x)
				{
					num = 0;
				}
				else
				{
					num = numberOfMinionsInPlay;
				}
			}
		}
		return num;
	}

	// Token: 0x06002D2D RID: 11565 RVA: 0x000E3958 File Offset: 0x000E1B58
	private void HandleUpdateWhileLeftMouseButtonIsDown()
	{
		if (UniversalInputManager.Get().IsTouchMode() && this.m_heldCard == null)
		{
			if (this.GetBattlecrySourceCard() == null)
			{
				this.m_myHandZone.HandleInput();
			}
			Card card = (this.m_myHandZone.CurrentStandIn != null) ? this.m_myHandZone.CurrentStandIn.linkedCard : null;
			if (card != this.m_lastPreviewedCard)
			{
				if (card != null)
				{
					this.m_lastMouseDownPosition.y = UniversalInputManager.Get().GetMousePosition().y;
				}
				this.m_lastPreviewedCard = card;
			}
		}
		if (this.m_dragging)
		{
			return;
		}
		if (this.m_lastObjectMousedDown == null)
		{
			return;
		}
		if (this.m_lastObjectMousedDown.GetComponent<HistoryCard>())
		{
			this.m_lastObjectMousedDown = null;
			this.m_leftMouseButtonIsDown = false;
			return;
		}
		float num = UniversalInputManager.Get().GetMousePosition().y - this.m_lastMouseDownPosition.y;
		float num2 = UniversalInputManager.Get().GetMousePosition().x - this.m_lastMouseDownPosition.x;
		if (num2 > -20f && num2 < 20f && num > -20f && num < 20f)
		{
			return;
		}
		bool flag = !UniversalInputManager.Get().IsTouchMode() || num > this.MIN_GRAB_Y;
		CardStandIn cardStandIn = this.m_lastObjectMousedDown.GetComponent<CardStandIn>();
		if (cardStandIn != null && GameState.Get() != null && !GameState.Get().IsMulliganManagerActive())
		{
			if (UniversalInputManager.Get().IsTouchMode())
			{
				if (!flag)
				{
					return;
				}
				cardStandIn = this.m_myHandZone.CurrentStandIn;
				if (cardStandIn == null)
				{
					return;
				}
			}
			if (!ChoiceCardMgr.Get().IsFriendlyShown() && this.GetBattlecrySourceCard() == null && this.IsInZone(cardStandIn.linkedCard, TAG_ZONE.HAND))
			{
				this.SetDragging(true);
				this.GrabCard(cardStandIn.linkedCard.gameObject);
			}
			return;
		}
		if (GameState.Get().IsMulliganManagerActive())
		{
			return;
		}
		if (GameState.Get().IsInTargetMode())
		{
			return;
		}
		Card component = this.m_lastObjectMousedDown.GetComponent<Card>();
		Entity entity = component.GetEntity();
		if (this.IsInZone(component, TAG_ZONE.HAND))
		{
			if (entity.IsControlledByLocalUser())
			{
				if (!flag || (UniversalInputManager.Get().IsTouchMode() && !GameState.Get().HasResponse(entity)))
				{
					return;
				}
				if (component.GetZone().m_ServerTag != TAG_ZONE.HAND && !GameState.Get().HasResponse(entity))
				{
					return;
				}
				if (!ChoiceCardMgr.Get().IsFriendlyShown() && this.GetBattlecrySourceCard() == null)
				{
					this.SetDragging(true);
					this.GrabCard(this.m_lastObjectMousedDown);
					return;
				}
			}
		}
		else if (this.IsInZone(component, TAG_ZONE.PLAY) && ((!entity.IsHeroPowerOrGameModeButton() && !entity.IsMoveMinionHoverTarget()) || (entity.IsHeroPowerOrGameModeButton() && GameState.Get().EntityHasTargets(entity))))
		{
			this.SetDragging(true);
			this.HandleClickOnCardInBattlefield(entity);
		}
	}

	// Token: 0x06002D2E RID: 11566 RVA: 0x000E3C34 File Offset: 0x000E1E34
	private void HandleUpdateWhileHoldingCard(bool hitBattlefield)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.DRAG);
		Card heldCard = this.m_heldCard;
		if (!heldCard.IsInputEnabled())
		{
			this.DropHeldCard();
			return;
		}
		Entity entity = heldCard.GetEntity();
		if (hitBattlefield && TargetReticleManager.Get() && !TargetReticleManager.Get().IsActive() && GameState.Get().EntityHasTargets(entity) && entity.GetCardType() != TAG_CARDTYPE.MINION)
		{
			if (!this.DoNetworkResponse(entity, true))
			{
				this.PositionHeldCard();
				return;
			}
			DragCardSoundEffects component = heldCard.GetComponent<DragCardSoundEffects>();
			if (component)
			{
				component.Disable();
			}
			RemoteActionHandler.Get().NotifyOpponentOfCardPickedUp(heldCard);
			RemoteActionHandler.Get().NotifyOpponentOfTargetModeBegin(heldCard);
			Entity hero = entity.GetHero();
			TargetReticleManager.Get().CreateFriendlyTargetArrow(hero, entity, true, true, null, false);
			this.ActivatePowerUpSpell(heldCard);
			this.ActivatePlaySpell(heldCard);
		}
		else
		{
			bool cardWasInsideHandLastFrame = this.m_cardWasInsideHandLastFrame;
			if (hitBattlefield && this.m_cardWasInsideHandLastFrame)
			{
				RemoteActionHandler.Get().NotifyOpponentOfCardPickedUp(heldCard);
				this.m_cardWasInsideHandLastFrame = false;
			}
			else if (!hitBattlefield)
			{
				this.m_cardWasInsideHandLastFrame = true;
			}
			this.PositionHeldCard();
			if (hitBattlefield)
			{
				this.m_myPlayZone.OnMagneticHeld(this.m_heldCard);
				this.m_myHandZone.OnCardHeld(this.m_heldCard);
			}
			else if (cardWasInsideHandLastFrame)
			{
				this.m_myHandZone.OnTwinspellDropped(this.m_heldCard);
				this.m_myPlayZone.OnMagneticDropped(this.m_heldCard);
			}
			if (GameState.Get().GetResponseMode() == GameState.ResponseMode.SUB_OPTION)
			{
				this.CancelSubOptionMode(false);
			}
		}
		if (UniversalInputManager.Get().IsTouchMode() && !hitBattlefield && this.m_heldCard != null && UniversalInputManager.Get().GetMousePosition().y - this.m_lastMouseDownPosition.y < this.MIN_GRAB_Y && !this.IsInZone(this.m_heldCard, TAG_ZONE.PLAY))
		{
			this.m_myHandZone.OnTwinspellDropped(this.m_heldCard);
			this.m_myPlayZone.OnMagneticDropped(this.m_heldCard);
			PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
			this.ReturnHeldCardToHand();
			return;
		}
	}

	// Token: 0x06002D2F RID: 11567 RVA: 0x000E3E28 File Offset: 0x000E2028
	private MoveMinionHoverTarget GetMoveMinionHoverTarget(Card heldCard)
	{
		if (heldCard == null)
		{
			return null;
		}
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out raycastHit))
		{
			MoveMinionHoverTarget componentInParent = raycastHit.transform.gameObject.GetComponentInParent<MoveMinionHoverTarget>();
			if (componentInParent != null)
			{
				return componentInParent;
			}
		}
		return null;
	}

	// Token: 0x06002D30 RID: 11568 RVA: 0x000E3E70 File Offset: 0x000E2070
	private void ActivatePowerUpSpell(Card card)
	{
		Entity entity = card.GetEntity();
		if (entity.IsSpell() || entity.IsMinion())
		{
			Spell actorSpell = card.GetActorSpell(SpellType.POWER_UP, true);
			if (actorSpell != null)
			{
				actorSpell.ActivateState(SpellStateType.BIRTH);
			}
		}
		card.DeactivateHandStateSpells(null);
	}

	// Token: 0x06002D31 RID: 11569 RVA: 0x000E3EB8 File Offset: 0x000E20B8
	private void ActivatePlaySpell(Card card)
	{
		Entity entity = card.GetEntity();
		if (entity.HasTag(GAME_TAG.CARD_DOES_NOTHING))
		{
			return;
		}
		Entity parentEntity = entity.GetParentEntity();
		Spell spell;
		if (parentEntity == null)
		{
			spell = card.GetPlaySpell(0, true);
		}
		else
		{
			Card card2 = parentEntity.GetCard();
			int subCardIndex = parentEntity.GetSubCardIndex(entity);
			spell = card2.GetSubOptionSpell(subCardIndex, 0, true);
		}
		if (spell != null && spell.GetActiveState() == SpellStateType.NONE)
		{
			spell.ActivateState(SpellStateType.BIRTH);
			return;
		}
	}

	// Token: 0x06002D32 RID: 11570 RVA: 0x000E3F23 File Offset: 0x000E2123
	private void HandleMouseMove()
	{
		if (GameState.Get() != null && GameState.Get().IsInTargetMode())
		{
			this.HandleUpdateWhileNotHoldingCard();
		}
	}

	// Token: 0x06002D33 RID: 11571 RVA: 0x000E3F40 File Offset: 0x000E2140
	private void HandleUpdateWhileNotHoldingCard()
	{
		if (!UniversalInputManager.Get().IsTouchMode() || !TargetReticleManager.Get().IsLocalArrowActive())
		{
			this.m_myHandZone.HandleInput();
		}
		int num = (UniversalInputManager.Get().IsTouchMode() && !UniversalInputManager.Get().GetMouseButton(0)) ? 1 : 0;
		RaycastHit hitInfo;
		bool inputHitInfo = UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out hitInfo);
		if (num != 0 || !inputHitInfo)
		{
			this.HandleMouseOff();
			return;
		}
		CardStandIn cardStandIn = SceneUtils.FindComponentInParents<CardStandIn>(hitInfo.transform);
		Actor actor = SceneUtils.FindComponentInParents<Actor>(hitInfo.transform);
		if (actor == null && cardStandIn == null)
		{
			this.HandleMouseOverObjectWhileNotHoldingCard(hitInfo);
			return;
		}
		if (this.m_mousedOverObject != null)
		{
			this.HandleMouseOffLastObject();
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
				if (this.m_mousedOverCard != null)
				{
					this.HandleMouseOffCard();
				}
				return;
			}
			if (cardStandIn == null)
			{
				return;
			}
			card = cardStandIn.linkedCard;
		}
		if (this.IsCancelingBattlecryCard(card))
		{
			return;
		}
		if (this.m_useHandEnlarge && this.m_myHandZone.HandEnlarged() && card.GetEntity().IsHeroPowerOrGameModeButton() && card.GetEntity().IsControlledByLocalUser() && this.m_myHandZone.GetCardCount() > 1)
		{
			return;
		}
		if (card != this.m_mousedOverCard && (card.GetZone() != this.m_myHandZone || GameState.Get().IsMulliganManagerActive()))
		{
			if (this.m_mousedOverCard != null)
			{
				this.HandleMouseOffCard();
			}
			this.HandleMouseOverCard(card);
		}
		PegCursor.Get().SetMode(PegCursor.Mode.OVER);
	}

	// Token: 0x06002D34 RID: 11572 RVA: 0x000E40F0 File Offset: 0x000E22F0
	private void HandleMouseOverObjectWhileNotHoldingCard(RaycastHit hitInfo)
	{
		GameObject gameObject = hitInfo.collider.gameObject;
		if (this.m_mousedOverCard != null)
		{
			this.HandleMouseOffCard();
		}
		if (UniversalInputManager.Get().IsTouchMode() && !UniversalInputManager.Get().GetMouseButton(0))
		{
			if (this.m_mousedOverObject != null)
			{
				this.HandleMouseOffLastObject();
			}
			return;
		}
		bool flag = TargetReticleManager.Get() != null && TargetReticleManager.Get().IsLocalArrowActive();
		if (!this.PermitDecisionMakingInput())
		{
			flag = false;
		}
		if (gameObject.GetComponent<HistoryManager>() != null && !flag)
		{
			this.m_mousedOverObject = gameObject;
			HistoryManager.Get().NotifyOfInput(hitInfo.point.z);
			return;
		}
		if (gameObject.GetComponent<PlayerLeaderboardManager>() != null && !flag)
		{
			this.m_mousedOverObject = gameObject;
			PlayerLeaderboardManager.Get().NotifyOfInput(hitInfo.point);
			return;
		}
		if (this.m_mousedOverObject == gameObject)
		{
			return;
		}
		if (this.m_mousedOverObject != null)
		{
			this.HandleMouseOffLastObject();
		}
		if (EndTurnButton.Get() && this.PermitDecisionMakingInput())
		{
			if (gameObject.GetComponent<EndTurnButton>() != null)
			{
				this.m_mousedOverObject = gameObject;
				EndTurnButton.Get().HandleMouseOver();
			}
			else if (gameObject.GetComponent<EndTurnButtonReminder>() != null && gameObject.GetComponent<EndTurnButtonReminder>().ShowFriendlySidePlayerTurnReminder())
			{
				this.m_mousedOverObject = gameObject;
			}
		}
		TooltipZone component = gameObject.GetComponent<TooltipZone>();
		if (component != null)
		{
			this.m_mousedOverObject = gameObject;
			this.ShowTooltipZone(gameObject, component);
		}
		GameOpenPack component2 = gameObject.GetComponent<GameOpenPack>();
		if (component2 != null)
		{
			this.m_mousedOverObject = gameObject;
			component2.NotifyOfMouseOver();
		}
		this.GetBattlecrySourceCard() != null;
	}

	// Token: 0x06002D35 RID: 11573 RVA: 0x000E4290 File Offset: 0x000E2490
	private void HandleMouseOff()
	{
		if (this.m_mousedOverCard)
		{
			Card friendlyHoverCard = RemoteActionHandler.Get().GetFriendlyHoverCard();
			if (this.m_mousedOverCard != friendlyHoverCard)
			{
				this.HandleMouseOffCard();
			}
		}
		if (this.m_mousedOverObject)
		{
			this.HandleMouseOffLastObject();
		}
	}

	// Token: 0x06002D36 RID: 11574 RVA: 0x000E42DC File Offset: 0x000E24DC
	private void HandleMouseOffLastObject()
	{
		if (this.m_mousedOverObject.GetComponent<EndTurnButton>())
		{
			this.m_mousedOverObject.GetComponent<EndTurnButton>().HandleMouseOut();
			this.m_lastObjectMousedDown = null;
		}
		else if (this.m_mousedOverObject.GetComponent<EndTurnButtonReminder>())
		{
			this.m_lastObjectMousedDown = null;
		}
		else if (this.m_mousedOverObject.GetComponent<TooltipZone>() != null)
		{
			this.m_mousedOverObject.GetComponent<TooltipZone>().HideTooltip();
			this.m_lastObjectMousedDown = null;
		}
		else if (this.m_mousedOverObject.GetComponent<HistoryManager>() != null)
		{
			HistoryManager.Get().NotifyOfMouseOff();
		}
		else if (this.m_mousedOverObject.GetComponent<PlayerLeaderboardManager>() != null)
		{
			PlayerLeaderboardManager.Get().NotifyOfMouseOff();
		}
		else if (this.m_mousedOverObject.GetComponent<GameOpenPack>() != null)
		{
			this.m_mousedOverObject.GetComponent<GameOpenPack>().NotifyOfMouseOff();
			this.m_lastObjectMousedDown = null;
		}
		this.m_mousedOverObject = null;
		this.HideBigViewCardBacks();
	}

	// Token: 0x06002D37 RID: 11575 RVA: 0x000E43D8 File Offset: 0x000E25D8
	private void GrabCard(GameObject cardObject)
	{
		if (!this.PermitDecisionMakingInput())
		{
			return;
		}
		Card component = cardObject.GetComponent<Card>();
		if (!component.IsInputEnabled())
		{
			return;
		}
		if (!GameState.Get().GetGameEntity().ShouldAllowCardGrab(component.GetEntity()))
		{
			return;
		}
		Zone zone = component.GetZone();
		if (!zone.IsInputEnabled())
		{
			return;
		}
		component.SetDoNotSort(true);
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
			ZonePlay zonePlay = (ZonePlay)zone;
			zonePlay.RemoveCard(component);
			zonePlay.UpdateLayout();
			component.HideTooltip();
			num = 0.9f;
		}
		this.m_heldCard = component;
		component.IsBeingDragged = true;
		SoundManager.Get().LoadAndPlay("FX_MinionSummon01_DrawFromHand_01.prefab:c8adc026a7f5d0a4cb0706627a980c58", cardObject);
		DragCardSoundEffects dragCardSoundEffects = this.m_heldCard.GetComponent<DragCardSoundEffects>();
		if (dragCardSoundEffects)
		{
			dragCardSoundEffects.enabled = true;
		}
		else
		{
			dragCardSoundEffects = cardObject.AddComponent<DragCardSoundEffects>();
		}
		dragCardSoundEffects.Restart();
		cardObject.AddComponent<DragRotator>().SetInfo(this.m_DragRotatorInfo);
		ProjectedShadow componentInChildren = component.GetActor().GetComponentInChildren<ProjectedShadow>();
		if (componentInChildren != null)
		{
			componentInChildren.EnableShadow(0.15f);
		}
		iTween.Stop(cardObject);
		iTween.ScaleTo(cardObject, new Vector3(num, num, num), 0.2f);
		TooltipPanelManager.Get().HideKeywordHelp();
		if (CardTypeBanner.Get())
		{
			CardTypeBanner.Get().Hide();
		}
		component.NotifyPickedUp();
		GameState.Get().GetGameEntity().NotifyOfCardGrabbed(component.GetEntity());
		SceneUtils.SetLayer(component, GameLayer.Default);
	}

	// Token: 0x06002D38 RID: 11576 RVA: 0x000E4564 File Offset: 0x000E2764
	private void DropCanceledHeldCard(Card card)
	{
		this.m_heldCard = null;
		RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
		ZonePlay controllersPlayZone = this.GetControllersPlayZone(card.GetEntity());
		this.m_myHandZone.UpdateLayout(null, true);
		controllersPlayZone.SortWithSpotForHeldCard(-1);
		controllersPlayZone.OnMagneticDropped(card);
		this.m_myHandZone.OnTwinspellDropped(card);
		this.SendDragDropCancelPlayTelemetry(card.GetEntity());
		card.IsBeingDragged = false;
	}

	// Token: 0x06002D39 RID: 11577 RVA: 0x000E45C8 File Offset: 0x000E27C8
	public void ReturnHeldCardToHand()
	{
		if (this.m_heldCard == null)
		{
			return;
		}
		Log.Hand.Print("ReturnHeldCardToHand()", Array.Empty<object>());
		Card heldCard = this.m_heldCard;
		heldCard.SetDoNotSort(false);
		iTween.Stop(this.m_heldCard.gameObject);
		Entity entity = heldCard.GetEntity();
		heldCard.NotifyLeftPlayfield();
		GameState.Get().GetGameEntity().NotifyOfCardDropped(entity);
		DragCardSoundEffects component = heldCard.GetComponent<DragCardSoundEffects>();
		if (component)
		{
			component.Disable();
		}
		UnityEngine.Object.Destroy(this.m_heldCard.GetComponent<DragRotator>());
		ProjectedShadow componentInChildren = heldCard.GetActor().GetComponentInChildren<ProjectedShadow>();
		if (componentInChildren != null)
		{
			componentInChildren.DisableShadow();
		}
		RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
		if (this.m_useHandEnlarge)
		{
			this.m_myHandZone.SetFriendlyHeroTargetingMode(false);
		}
		this.m_myHandZone.UpdateLayout(this.m_myHandZone.GetLastMousedOverCard(), true);
		this.m_heldCard.IsBeingDragged = false;
		this.SetDragging(false);
		this.m_heldCard = null;
	}

	// Token: 0x06002D3A RID: 11578 RVA: 0x000E46C0 File Offset: 0x000E28C0
	private bool DropHeldCard(bool wasCancelled)
	{
		Log.Hand.Print("DropHeldCard - cancelled? " + wasCancelled.ToString(), Array.Empty<object>());
		PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
		if (this.m_enlargeHandAfterDropCard)
		{
			this.m_enlargeHandAfterDropCard = false;
			this.ShowPhoneHand();
		}
		if (this.m_useHandEnlarge)
		{
			this.m_myHandZone.SetFriendlyHeroTargetingMode(false);
			if (this.m_hideHandAfterPlayingCard)
			{
				this.HidePhoneHand();
			}
			else
			{
				this.m_myHandZone.UpdateLayout(null, true);
			}
		}
		if (this.m_heldCard == null)
		{
			return false;
		}
		Card heldCard = this.m_heldCard;
		heldCard.SetDoNotSort(false);
		iTween.Stop(this.m_heldCard.gameObject);
		Entity entity = heldCard.GetEntity();
		heldCard.NotifyLeftPlayfield();
		GameState.Get().GetGameEntity().NotifyOfCardDropped(entity);
		DragCardSoundEffects component = heldCard.GetComponent<DragCardSoundEffects>();
		if (component)
		{
			component.Disable();
		}
		UnityEngine.Object.Destroy(this.m_heldCard.GetComponent<DragRotator>());
		this.m_heldCard = null;
		ProjectedShadow componentInChildren = heldCard.GetActor().GetComponentInChildren<ProjectedShadow>();
		if (componentInChildren != null)
		{
			componentInChildren.DisableShadow();
		}
		if (this.IsInZone(heldCard, TAG_ZONE.PLAY))
		{
			MoveMinionHoverTarget moveMinionHoverTarget = this.GetMoveMinionHoverTarget(heldCard);
			if (moveMinionHoverTarget != null && !wasCancelled)
			{
				moveMinionHoverTarget.DropCardOnHoverTarget(heldCard);
			}
			else
			{
				this.AddHeldCardBackToPlayZone(heldCard);
			}
			GameState.Get().ExitMoveMinionMode();
		}
		SceneUtils.SetLayer(heldCard, GameLayer.CardRaycast);
		if (wasCancelled)
		{
			this.DropCanceledHeldCard(heldCard);
			return true;
		}
		bool flag = false;
		if (this.IsInZone(heldCard, TAG_ZONE.HAND))
		{
			if (entity.IsMinion() || entity.IsWeapon())
			{
				this.DropHeldMinionOrWeapon(heldCard, entity, ref flag);
				if (entity.IsMinion() && heldCard.GetActor() != null && !UniversalInputManager.Get().IsTouchMode())
				{
					heldCard.GetActor().TurnOffCollider();
				}
			}
			else if (entity.IsSpell() || entity.IsHero())
			{
				bool flag2 = false;
				this.DropHeldSpellOrHero(heldCard, entity, ref flag2);
				if (flag2)
				{
					this.DropCanceledHeldCard(entity.GetCard());
					return true;
				}
			}
			this.m_myHandZone.UpdateLayout(null, true);
			this.m_myPlayZone.SortWithSpotForHeldCard(-1);
		}
		if (this.IsInZone(heldCard, TAG_ZONE.PLAY))
		{
			if (entity.IsMinion())
			{
				this.DropHeldMinionOrWeapon(heldCard, entity, ref flag);
			}
			this.GetControllersPlayZone(heldCard.GetEntity()).SortWithSpotForHeldCard(-1);
		}
		if (flag)
		{
			if (RemoteActionHandler.Get())
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

	// Token: 0x06002D3B RID: 11579 RVA: 0x000E4922 File Offset: 0x000E2B22
	public ZonePlay GetControllersPlayZone(Entity entity)
	{
		if (!entity.IsControlledByFriendlySidePlayer())
		{
			return this.m_enemyPlayZone;
		}
		return this.m_myPlayZone;
	}

	// Token: 0x06002D3C RID: 11580 RVA: 0x000E4939 File Offset: 0x000E2B39
	public void AddHeldCardBackToPlayZone(Card card)
	{
		this.GetControllersPlayZone(card.GetEntity()).AddCard(card);
	}

	// Token: 0x06002D3D RID: 11581 RVA: 0x000E4950 File Offset: 0x000E2B50
	private void SendDragDropCancelPlayTelemetry(Entity cancelledEntity)
	{
		if (cancelledEntity == null || GameMgr.Get() == null)
		{
			return;
		}
		TelemetryManager.Client().SendDragDropCancelPlayCard((long)GameMgr.Get().GetMissionId(), ((TAG_CARDTYPE)cancelledEntity.GetTag(GAME_TAG.CARDTYPE)).ToString());
	}

	// Token: 0x06002D3E RID: 11582 RVA: 0x000E4998 File Offset: 0x000E2B98
	private void DropHeldMinionOrWeapon(Card card, Entity entity, ref bool notifyEnemyOfTargetArrow)
	{
		if (card == null || entity == null)
		{
			Debug.LogWarningFormat("DropHeldMinionOrWeapon() is called with the invalid card or entity.", Array.Empty<object>());
			return;
		}
		ZonePlay controllersPlayZone = this.GetControllersPlayZone(card.GetEntity());
		bool flag = entity.IsMinion();
		bool flag2 = entity.IsWeapon();
		if (!flag && !flag2)
		{
			Debug.LogWarningFormat("DropHeldMinionOrWeapon() is called with the card: {0}", new object[]
			{
				entity.GetCardId()
			});
			card.IsBeingDragged = false;
			return;
		}
		RaycastHit raycastHit;
		if (!UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.InvisibleHitBox2, out raycastHit))
		{
			controllersPlayZone.OnMagneticDropped(card);
			this.SendDragDropCancelPlayTelemetry(entity);
			card.IsBeingDragged = false;
			return;
		}
		Zone zone;
		if (flag2)
		{
			zone = this.m_myWeaponZone;
		}
		else
		{
			zone = controllersPlayZone;
		}
		if (zone)
		{
			GameState gameState = GameState.Get();
			int num = 0;
			int num2 = 0;
			if (flag)
			{
				num = this.PlayZoneSlotMousedOver(card) + 1;
				num2 = ZoneMgr.Get().PredictZonePosition(zone, num);
				gameState.SetSelectedOptionPosition(num2);
			}
			if (this.DoNetworkResponse(entity, true))
			{
				if (this.IsInZone(card, TAG_ZONE.HAND))
				{
					this.m_lastZoneChangeList = ZoneMgr.Get().AddPredictedLocalZoneChange(card, zone, num, num2);
					this.PredictSpentMana(entity);
					controllersPlayZone.OnMagneticPlay(card, num2);
					if (flag && gameState.EntityHasTargets(entity))
					{
						notifyEnemyOfTargetArrow = true;
						bool showArrow = !UniversalInputManager.Get().IsTouchMode();
						if (TargetReticleManager.Get())
						{
							TargetReticleManager.Get().CreateFriendlyTargetArrow(entity, entity, true, showArrow, null, false);
						}
						this.m_battlecrySourceCard = card;
						if (UniversalInputManager.Get().IsTouchMode())
						{
							this.StartBattleCryEffect(entity);
						}
					}
				}
				else if (this.IsInZone(card, TAG_ZONE.PLAY) && card.GetZone() == zone && card.GetZonePosition() != num2)
				{
					this.m_lastZoneChangeList = ZoneMgr.Get().AddPredictedLocalZoneChange(card, zone, num, num2);
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

	// Token: 0x06002D3F RID: 11583 RVA: 0x000E4B88 File Offset: 0x000E2D88
	private void DropHeldSpellOrHero(Card card, Entity entity, ref bool cancelDrop)
	{
		if (card == null || entity == null)
		{
			Debug.LogWarningFormat("DropHeldSpellOrHero() is called with the invalid card or entity.", Array.Empty<object>());
			return;
		}
		if (!entity.IsSpell() && !entity.IsHero())
		{
			Debug.LogWarningFormat("DropHeldSpellOrHero() is called with the card: {0}", new object[]
			{
				entity.GetCardId()
			});
			return;
		}
		if (GameState.Get().EntityHasTargets(entity))
		{
			cancelDrop = true;
			return;
		}
		RaycastHit raycastHit;
		if (!UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.InvisibleHitBox2, out raycastHit))
		{
			this.m_myHandZone.OnTwinspellDropped(card);
			this.SendDragDropCancelPlayTelemetry(entity);
			return;
		}
		if (!GameState.Get().HasResponse(entity))
		{
			PlayErrors.DisplayPlayError(GameState.Get().GetErrorType(entity), GameState.Get().GetErrorParam(entity), entity);
			return;
		}
		this.m_myHandZone.OnTwinspellPlayed(card);
		this.DoNetworkResponse(entity, true);
		this.m_lastZoneChangeList = ZoneMgr.Get().AddLocalZoneChange(card, TAG_ZONE.PLAY);
		this.PredictSpentMana(entity);
		if (entity.IsSpell())
		{
			if (GameState.Get().HasSubOptions(entity))
			{
				card.DeactivateHandStateSpells(null);
				return;
			}
			this.ActivatePowerUpSpell(card);
			this.ActivatePlaySpell(card);
		}
	}

	// Token: 0x06002D40 RID: 11584 RVA: 0x000E4C98 File Offset: 0x000E2E98
	private void HandleRightClickOnCard(Card card)
	{
		if (GameState.Get().IsInTargetMode() || GameState.Get().IsInSubOptionMode() || this.m_heldCard != null)
		{
			this.HandleRightClick();
			return;
		}
		if (card.GetEntity().IsHero())
		{
			if (card.GetEntity().IsControlledByLocalUser())
			{
				if (EmoteHandler.Get() != null)
				{
					if (EmoteHandler.Get().AreEmotesActive())
					{
						EmoteHandler.Get().HideEmotes();
						return;
					}
					EmoteHandler.Get().ShowEmotes();
					return;
				}
			}
			else
			{
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
						return;
					}
					EnemyEmoteHandler.Get().ShowEmotes();
					return;
				}
			}
		}
	}

	// Token: 0x06002D41 RID: 11585 RVA: 0x000E4D6C File Offset: 0x000E2F6C
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
		Log.Hand.Print("HandleClickOnCard - Card zone: " + component.GetZone(), Array.Empty<object>());
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
			this.CancelOption(false);
			return;
		}
		GameState.ResponseMode responseMode = GameState.Get().GetResponseMode();
		if (this.IsInZone(component, TAG_ZONE.HAND))
		{
			if (GameState.Get().IsMulliganManagerActive())
			{
				if (this.PermitDecisionMakingInput())
				{
					MulliganManager.Get().ToggleHoldState(component);
					return;
				}
			}
			else
			{
				if (component.IsAttacking())
				{
					return;
				}
				if (GameState.Get().IsInTargetMode())
				{
					return;
				}
				if (UniversalInputManager.Get().IsTouchMode())
				{
					return;
				}
				if (!component.GetEntity().IsControlledByLocalUser())
				{
					return;
				}
				if (!ChoiceCardMgr.Get().IsFriendlyShown() && this.GetBattlecrySourceCard() == null)
				{
					if (component.GetZone().m_ServerTag != TAG_ZONE.HAND && !GameState.Get().HasResponse(entity))
					{
						return;
					}
					this.GrabCard(upClickedCard);
				}
			}
			return;
		}
		if (responseMode == GameState.ResponseMode.SUB_OPTION)
		{
			this.HandleClickOnSubOption(entity, false);
			return;
		}
		if (responseMode == GameState.ResponseMode.CHOICE)
		{
			this.HandleClickOnChoice(entity);
			return;
		}
		if (this.IsInZone(component, TAG_ZONE.PLAY))
		{
			this.HandleClickOnCardInBattlefield(entity);
			return;
		}
	}

	// Token: 0x06002D42 RID: 11586 RVA: 0x000E4F64 File Offset: 0x000E3164
	private void HandleClickOnCardInBattlefield(Entity clickedEntity)
	{
		if (!this.PermitDecisionMakingInput())
		{
			return;
		}
		PegCursor.Get().SetMode(PegCursor.Mode.STOPDRAG);
		GameState gameState = GameState.Get();
		Card card = clickedEntity.GetCard();
		if (UniversalInputManager.Get().IsTouchMode() && clickedEntity.IsHeroPowerOrGameModeButton() && this.m_mousedOverTimer > this.m_MouseOverDelay)
		{
			return;
		}
		if (clickedEntity.IsGameModeButton() && clickedEntity.GetCard() != null && clickedEntity.GetCard().GetPlaySpell(0, true) != null && clickedEntity.GetCard().GetPlaySpell(0, true).GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		if (!gameState.GetGameEntity().NotifyOfBattlefieldCardClicked(clickedEntity, gameState.IsInTargetMode()))
		{
			return;
		}
		if (gameState.IsInTargetMode())
		{
			this.DisableSkullIfNeeded(card);
			Network.Options.Option.SubOption selectedNetworkSubOption = gameState.GetSelectedNetworkSubOption();
			if (selectedNetworkSubOption.ID == clickedEntity.GetEntityId())
			{
				this.CancelOption(false);
				return;
			}
			this.UpdateTelemetryAttackInputCounts(gameState.GetEntity(selectedNetworkSubOption.ID));
			if (this.DoNetworkResponse(clickedEntity, true) && this.m_heldCard != null)
			{
				Card heldCard = this.m_heldCard;
				this.m_myHandZone.OnTwinspellPlayed(heldCard);
				UnityEngine.Object.Destroy(heldCard.GetComponent<DragRotator>());
				this.m_heldCard = null;
				heldCard.SetDoNotSort(false);
				this.m_lastZoneChangeList = ZoneMgr.Get().AddLocalZoneChange(heldCard, TAG_ZONE.PLAY);
			}
			return;
		}
		else
		{
			if (UniversalInputManager.Get().IsTouchMode() && UniversalInputManager.Get().GetMouseButtonUp(0) && gameState.EntityHasTargets(clickedEntity))
			{
				if (!card.IsShowingTooltip() && gameState.IsFriendlySidePlayerTurn())
				{
					PlayErrors.DisplayPlayError(PlayErrors.ErrorType.REQ_DRAG_TO_PLAY, null, clickedEntity);
				}
				return;
			}
			if (clickedEntity.IsWeapon() && clickedEntity.IsControlledByLocalUser() && !GameState.Get().IsValidOption(clickedEntity))
			{
				this.HandleClickOnCardInBattlefield(gameState.GetFriendlySidePlayer().GetHero());
				return;
			}
			if (clickedEntity.IsHero() && clickedEntity.IsControlledByLocalUser() && clickedEntity.GetWeaponCard() != null && GameState.Get().IsValidOption(clickedEntity.GetWeaponCard().GetEntity()))
			{
				this.HandleClickOnCardInBattlefield(clickedEntity.GetWeaponCard().GetEntity());
				return;
			}
			if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.ALLOW_MOVE_MINION) > 0 && card.GetEntity().IsMinion())
			{
				if (!card.IsInputEnabled() || card.GetEntity().HasTag(GAME_TAG.CANT_MOVE_MINION))
				{
					return;
				}
				if (UniversalInputManager.Get().IsTouchMode())
				{
					if (this.m_mousedOverTimer > this.m_MouseOverDelay)
					{
						return;
					}
					if (UniversalInputManager.Get().GetMouseButtonUp(0))
					{
						return;
					}
				}
				PlayErrors.ErrorType error;
				if (!clickedEntity.IsControlledByFriendlySidePlayer() && !GameState.Get().HasValidHoverTargetForMovedMinion(card.GetEntity(), out error))
				{
					PlayErrors.DisplayPlayError(error, null, clickedEntity);
					return;
				}
				this.GrabCard(card.gameObject);
				GameState.Get().EnterMoveMinionMode(card.GetEntity(), false);
				return;
			}
			else
			{
				if (!this.DoNetworkResponse(clickedEntity, true))
				{
					return;
				}
				if (!gameState.IsInTargetMode())
				{
					if (clickedEntity.IsHeroPowerOrGameModeButton())
					{
						if (!clickedEntity.HasSubCards())
						{
							this.ActivatePlaySpell(card);
						}
						clickedEntity.SetTagAndHandleChange<int>(GAME_TAG.EXHAUSTED, 1);
						this.PredictSpentMana(clickedEntity);
					}
					return;
				}
				RemoteActionHandler.Get().NotifyOpponentOfTargetModeBegin(card);
				if (TargetReticleManager.Get())
				{
					TargetReticleManager.Get().CreateFriendlyTargetArrow(clickedEntity, clickedEntity, false, true, null, false);
				}
				if (clickedEntity.IsHeroPowerOrGameModeButton())
				{
					this.m_targettingHeroPower = true;
					this.ActivatePlaySpell(card);
					return;
				}
				if (!clickedEntity.IsCharacter())
				{
					return;
				}
				card.ActivateCharacterAttackEffects();
				if (!clickedEntity.HasTag(GAME_TAG.IGNORE_TAUNT))
				{
					gameState.ShowEnemyTauntCharacters();
				}
				if (!card.IsAttacking())
				{
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
				return;
			}
		}
	}

	// Token: 0x06002D43 RID: 11587 RVA: 0x000E52F0 File Offset: 0x000E34F0
	private void UpdateTelemetryAttackInputCounts(Entity sourceEntity)
	{
		if (sourceEntity == null || this.m_battlecrySourceCard != null)
		{
			return;
		}
		if (!sourceEntity.IsMinion() && !sourceEntity.IsHero())
		{
			return;
		}
		if (this.m_lastInputDrag)
		{
			this.m_telemetryNumDragAttacks++;
			return;
		}
		this.m_telemetryNumClickAttacks++;
	}

	// Token: 0x06002D44 RID: 11588 RVA: 0x000E5348 File Offset: 0x000E3548
	public void HandleClickOnSubOption(Entity entity, bool isSimulated = false)
	{
		if (!isSimulated && !this.PermitDecisionMakingInput())
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
					TargetReticleManager.Get().CreateFriendlyTargetArrow(hero, entity2, true, !UniversalInputManager.Get().IsTouchMode(), entity.GetCardTextInHand(), false);
				}
			}
			Card card = entity.GetCard();
			if (!isSimulated)
			{
				this.DoNetworkResponse(entity, true);
			}
			this.ActivatePowerUpSpell(card);
			if (!isSimulated)
			{
				this.ActivatePlaySpell(card);
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
				this.FinishSubOptions();
			}
			if (UniversalInputManager.Get().IsTouchMode() && !isSimulated && flag)
			{
				this.StartMobileTargetingEffect(GameState.Get().GetSelectedNetworkSubOption().Targets);
				return;
			}
		}
		else
		{
			PlayErrors.DisplayPlayError(GameState.Get().GetErrorType(entity), GameState.Get().GetErrorParam(entity), entity);
		}
	}

	// Token: 0x06002D45 RID: 11589 RVA: 0x000E5478 File Offset: 0x000E3678
	private void HandleClickOnChoice(Entity entity)
	{
		if (!this.PermitDecisionMakingInput())
		{
			return;
		}
		if (this.DoNetworkResponse(entity, true))
		{
			SoundManager.Get().LoadAndPlay("HeroDropItem1.prefab:587232e6704b20942af1205d00cfc0f9");
			return;
		}
		PlayErrors.DisplayPlayError(GameState.Get().GetErrorType(entity), GameState.Get().GetErrorParam(entity), entity);
	}

	// Token: 0x06002D46 RID: 11590 RVA: 0x000E54CC File Offset: 0x000E36CC
	public void ResetBattlecrySourceCard()
	{
		if (this.m_battlecrySourceCard == null)
		{
			return;
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			string message;
			if (this.m_battlecrySourceCard.GetEntity().HasTag(GAME_TAG.BATTLECRY))
			{
				message = GameStrings.Get("GAMEPLAY_MOBILE_BATTLECRY_CANCELED");
			}
			else
			{
				message = GameStrings.Get("GAMEPLAY_MOBILE_TARGETING_CANCELED");
			}
			GameplayErrorManager.Get().DisplayMessage(message);
		}
		this.m_cancelingBattlecryCards.Add(this.m_battlecrySourceCard);
		Entity entity = this.m_battlecrySourceCard.GetEntity();
		Spell actorSpell = this.m_battlecrySourceCard.GetActorSpell(SpellType.BATTLECRY, true);
		if (actorSpell)
		{
			actorSpell.ActivateState(SpellStateType.CANCEL);
		}
		Spell playSpell = this.m_battlecrySourceCard.GetPlaySpell(0, true);
		if (playSpell)
		{
			playSpell.ActivateState(SpellStateType.CANCEL);
		}
		Spell customSummonSpell = this.m_battlecrySourceCard.GetCustomSummonSpell();
		if (customSummonSpell)
		{
			customSummonSpell.ActivateState(SpellStateType.CANCEL);
		}
		ZoneMgr.ChangeCompleteCallback callback = delegate(ZoneChangeList changeList, object userData)
		{
			Card item = (Card)userData;
			this.m_cancelingBattlecryCards.Remove(item);
		};
		ZoneMgr.Get().CancelLocalZoneChange(this.m_lastZoneChangeList, callback, this.m_battlecrySourceCard);
		this.m_lastZoneChangeList = null;
		this.RollbackSpentMana(entity);
		this.ClearBattlecrySourceCard();
	}

	// Token: 0x06002D47 RID: 11591 RVA: 0x000E55DF File Offset: 0x000E37DF
	private bool IsCancelingBattlecryCard(Card card)
	{
		return this.m_cancelingBattlecryCards.Contains(card);
	}

	// Token: 0x06002D48 RID: 11592 RVA: 0x000E55ED File Offset: 0x000E37ED
	public void DoEndTurnButton()
	{
		if (!this.PermitDecisionMakingInput())
		{
			return;
		}
		if (GameState.Get().IsResponsePacketBlocked())
		{
			return;
		}
		if (EndTurnButton.Get().IsInputBlocked())
		{
			return;
		}
		if (EndTurnButton.Get().IsDisabled)
		{
			return;
		}
		this.DoEndTurnInternal();
	}

	// Token: 0x06002D49 RID: 11593 RVA: 0x000E5628 File Offset: 0x000E3828
	private void DoEndTurnInternal()
	{
		GameState gameState = GameState.Get();
		GameState.ResponseMode responseMode = gameState.GetResponseMode();
		if (responseMode != GameState.ResponseMode.OPTION)
		{
			if (responseMode == GameState.ResponseMode.CHOICE)
			{
				gameState.SendChoices();
				return;
			}
		}
		else
		{
			Network.Options optionsPacket = gameState.GetOptionsPacket();
			int i = 0;
			while (i < optionsPacket.List.Count)
			{
				Network.Options.Option option = optionsPacket.List[i];
				if (option.Type == Network.Options.Option.OptionType.END_TURN || option.Type == Network.Options.Option.OptionType.PASS)
				{
					if (gameState.GetGameEntity().NotifyOfEndTurnButtonPushed())
					{
						gameState.SetSelectedOption(i);
						gameState.SendOption();
						this.HidePhoneHand();
						this.DoEndTurnButton_Option_OnEndTurnRequested();
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}
	}

	// Token: 0x06002D4A RID: 11594 RVA: 0x000E56B7 File Offset: 0x000E38B7
	public void DoEndTurn_Cheat()
	{
		this.DoEndTurnInternal();
	}

	// Token: 0x06002D4B RID: 11595 RVA: 0x000E56BF File Offset: 0x000E38BF
	private void DoEndTurnButton_Option_OnEndTurnRequested()
	{
		if (TurnTimer.Get() != null)
		{
			TurnTimer.Get().OnEndTurnRequested();
		}
		EndTurnButton.Get().OnEndTurnRequested();
	}

	// Token: 0x06002D4C RID: 11596 RVA: 0x000E56E4 File Offset: 0x000E38E4
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
		case GameState.ResponseMode.OPTION:
			flag = this.DoNetworkOptions(entity);
			break;
		case GameState.ResponseMode.SUB_OPTION:
			flag = this.DoNetworkSubOptions(entity);
			break;
		case GameState.ResponseMode.OPTION_TARGET:
			flag = this.DoNetworkOptionTarget(entity);
			break;
		case GameState.ResponseMode.CHOICE:
			flag = this.DoNetworkChoice(entity);
			break;
		}
		if (flag)
		{
			entity.GetCard().UpdateActorState(false);
		}
		return flag;
	}

	// Token: 0x06002D4D RID: 11597 RVA: 0x000E5775 File Offset: 0x000E3975
	private void OnOptionsReceived(object userData)
	{
		if (this.m_mousedOverCard)
		{
			this.m_mousedOverCard.UpdateProposedManaUsage();
		}
		this.HidePhoneHandIfOutOfServerPlays();
	}

	// Token: 0x06002D4E RID: 11598 RVA: 0x000E5795 File Offset: 0x000E3995
	private void OnCurrentPlayerChanged(Player player, object userData)
	{
		if (player.IsLocalUser())
		{
			this.m_entitiesThatPredictedMana.Clear();
		}
	}

	// Token: 0x06002D4F RID: 11599 RVA: 0x000E57AC File Offset: 0x000E39AC
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
				entity.SetTagAndHandleChange<int>(GAME_TAG.EXHAUSTED, 0);
			}
			this.RollbackSpentMana(entity);
			if (entity.IsTwinspell())
			{
				this.GetFriendlyHand().ActivateTwinspellSpellDeath();
				this.GetFriendlyHand().ClearReservedCard();
			}
		}
		string message = GameStrings.Get("GAMEPLAY_ERROR_PLAY_REJECTED");
		GameplayErrorManager.Get().DisplayMessage(message);
	}

	// Token: 0x06002D50 RID: 11600 RVA: 0x000E583D File Offset: 0x000E3A3D
	private void OnTurnTimerUpdate(TurnTimerUpdate update, object userData)
	{
		if (update.GetSecondsRemaining() <= Mathf.Epsilon || GameUtils.IsWaitingForOpponentReconnect())
		{
			this.CancelOption(true);
		}
	}

	// Token: 0x06002D51 RID: 11601 RVA: 0x000E585B File Offset: 0x000E3A5B
	private void OnGameOver(TAG_PLAYSTATE playState, object userData)
	{
		this.HidePhoneHand();
		this.CancelOption(false);
		this.SendGameOverTelemetry();
	}

	// Token: 0x06002D52 RID: 11602 RVA: 0x000E5874 File Offset: 0x000E3A74
	private void SendGameOverTelemetry()
	{
		int num = this.m_telemetryNumClickAttacks + this.m_telemetryNumDragAttacks;
		int percentClickAttacks = (num == 0) ? 0 : ((int)((double)this.m_telemetryNumClickAttacks * 100.0 / (double)num));
		int percentDragAttacks = (num == 0) ? 0 : ((int)((double)this.m_telemetryNumDragAttacks * 100.0 / (double)num));
		TelemetryManager.Client().SendAttackInputMethod((long)num, (long)this.m_telemetryNumClickAttacks, percentClickAttacks, (long)this.m_telemetryNumDragAttacks, percentDragAttacks);
		this.m_telemetryNumDragAttacks = 0;
		this.m_telemetryNumClickAttacks = 0;
	}

	// Token: 0x06002D53 RID: 11603 RVA: 0x000E58F4 File Offset: 0x000E3AF4
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

	// Token: 0x06002D54 RID: 11604 RVA: 0x000E5960 File Offset: 0x000E3B60
	private bool DoNetworkOptions(Entity entity)
	{
		int entityId = entity.GetEntityId();
		GameState gameState = GameState.Get();
		Network.Options optionsPacket = gameState.GetOptionsPacket();
		for (int i = 0; i < optionsPacket.List.Count; i++)
		{
			Network.Options.Option option = optionsPacket.List[i];
			if (option.Type == Network.Options.Option.OptionType.POWER && option.Main.PlayErrorInfo.IsValid() && option.Main.ID == entityId)
			{
				gameState.SetSelectedOption(i);
				if (!option.HasValidSubOption())
				{
					if (option.Main.Targets == null || option.Main.Targets.Count == 0)
					{
						gameState.SendOption();
					}
					else
					{
						this.EnterOptionTargetMode();
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
		}
		if (!UniversalInputManager.Get().IsTouchMode() || !entity.GetCard().IsShowingTooltip())
		{
			PlayErrors.DisplayPlayError(GameState.Get().GetErrorType(entity), GameState.Get().GetErrorParam(entity), entity);
		}
		return false;
	}

	// Token: 0x06002D55 RID: 11605 RVA: 0x000E5A6C File Offset: 0x000E3C6C
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
					this.EnterOptionTargetMode();
				}
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002D56 RID: 11606 RVA: 0x000E5AF8 File Offset: 0x000E3CF8
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
		if (TargetReticleManager.Get())
		{
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(false);
		}
		if (RemoteActionHandler.Get())
		{
			RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
		}
		this.FinishBattlecrySourceCard();
		this.FinishSubOptions();
		if (entity2.IsHeroPowerOrGameModeButton())
		{
			entity2.SetTagAndHandleChange<int>(GAME_TAG.EXHAUSTED, 1);
			this.PredictSpentMana(entity2);
		}
		gameState.SetSelectedOptionTarget(entityId);
		gameState.SendOption();
		return true;
	}

	// Token: 0x06002D57 RID: 11607 RVA: 0x000E5BBC File Offset: 0x000E3DBC
	private void EnterOptionTargetMode()
	{
		GameState gameState = GameState.Get();
		gameState.EnterOptionTargetMode();
		if (this.m_useHandEnlarge)
		{
			this.m_myHandZone.SetFriendlyHeroTargetingMode(gameState.FriendlyHeroIsTargetable());
			this.m_enlargeHandAfterDropCard = (this.m_myHandZone.HandEnlarged() || ChoiceCardMgr.Get().RestoreEnlargedHandAfterChoice());
			if (this.m_myHandZone.HandEnlarged())
			{
				this.HidePhoneHand();
				return;
			}
			this.m_myHandZone.UpdateLayout(null, true);
		}
	}

	// Token: 0x06002D58 RID: 11608 RVA: 0x000E5C2F File Offset: 0x000E3E2F
	private void FinishBattlecrySourceCard()
	{
		if (this.m_battlecrySourceCard == null)
		{
			return;
		}
		this.ClearBattlecrySourceCard();
	}

	// Token: 0x06002D59 RID: 11609 RVA: 0x000E5C48 File Offset: 0x000E3E48
	private void ClearBattlecrySourceCard()
	{
		if (this.m_isInBattleCryEffect && this.m_battlecrySourceCard != null)
		{
			this.EndBattleCryEffect();
		}
		this.m_battlecrySourceCard = null;
		RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
		if (this.m_useHandEnlarge)
		{
			this.m_myHandZone.SetFriendlyHeroTargetingMode(false);
			this.m_myHandZone.UpdateLayout(null, true);
		}
	}

	// Token: 0x06002D5A RID: 11610 RVA: 0x000E5CA4 File Offset: 0x000E3EA4
	private void CancelSubOptions()
	{
		Card subOptionParentCard = ChoiceCardMgr.Get().GetSubOptionParentCard();
		if (subOptionParentCard == null)
		{
			return;
		}
		ChoiceCardMgr.Get().CancelSubOptions();
		if (subOptionParentCard.GetEntity().IsTwinspell())
		{
			this.m_myHandZone.OnTwinspellDropped(subOptionParentCard);
			this.m_myHandZone.ClearReservedCard();
		}
		Entity entity = subOptionParentCard.GetEntity();
		if (!entity.IsHeroPowerOrGameModeButton())
		{
			ZoneMgr.Get().CancelLocalZoneChange(this.m_lastZoneChangeList, null, null);
			this.m_lastZoneChangeList = null;
		}
		this.RollbackSpentMana(entity);
		this.DropSubOptionParentCard();
	}

	// Token: 0x06002D5B RID: 11611 RVA: 0x000E5D2A File Offset: 0x000E3F2A
	private void FinishSubOptions()
	{
		if (ChoiceCardMgr.Get().GetSubOptionParentCard() == null)
		{
			return;
		}
		this.DropSubOptionParentCard();
	}

	// Token: 0x06002D5C RID: 11612 RVA: 0x000E5D48 File Offset: 0x000E3F48
	public void DropSubOptionParentCard()
	{
		Log.Hand.Print("DropSubOptionParentCard()", Array.Empty<object>());
		ChoiceCardMgr.Get().ClearSubOptions();
		RemoteActionHandler.Get().NotifyOpponentOfCardDropped();
		if (this.m_useHandEnlarge)
		{
			this.m_myHandZone.SetFriendlyHeroTargetingMode(false);
			this.m_myHandZone.UpdateLayout(null, true);
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			this.EndMobileTargetingEffect();
		}
	}

	// Token: 0x06002D5D RID: 11613 RVA: 0x000E5DB0 File Offset: 0x000E3FB0
	private void StartMobileTargetingEffect(List<Network.Options.Option.TargetOption> targets)
	{
		if (targets == null || targets.Count == 0)
		{
			return;
		}
		this.m_mobileTargettingEffectActors.Clear();
		foreach (Network.Options.Option.TargetOption targetOption in targets)
		{
			if (targetOption.PlayErrorInfo.IsValid())
			{
				Entity entity = GameState.Get().GetEntity(targetOption.ID);
				if (entity.GetCard() != null)
				{
					Actor actor = entity.GetCard().GetActor();
					this.m_mobileTargettingEffectActors.Add(actor);
					this.ApplyMobileTargettingEffectToActor(actor);
				}
			}
		}
		FullScreenFXMgr.Get().Desaturate(0.9f, 0.4f, iTween.EaseType.easeInOutQuad, null, null);
	}

	// Token: 0x06002D5E RID: 11614 RVA: 0x000E5E70 File Offset: 0x000E4070
	private void EndMobileTargetingEffect()
	{
		foreach (Actor actor in this.m_mobileTargettingEffectActors)
		{
			this.RemoveMobileTargettingEffectFromActor(actor);
		}
		FullScreenFXMgr.Get().StopDesaturate(0.4f, iTween.EaseType.easeInOutQuad, null, null);
	}

	// Token: 0x06002D5F RID: 11615 RVA: 0x000E5ED8 File Offset: 0x000E40D8
	private void StartBattleCryEffect(Entity entity)
	{
		this.m_isInBattleCryEffect = true;
		Network.Options.Option selectedNetworkOption = GameState.Get().GetSelectedNetworkOption();
		if (selectedNetworkOption == null)
		{
			Debug.LogError("No targets for BattleCry.");
			return;
		}
		this.StartMobileTargetingEffect(selectedNetworkOption.Main.Targets);
		this.m_battlecrySourceCard.SetBattleCrySource(true);
	}

	// Token: 0x06002D60 RID: 11616 RVA: 0x000E5F22 File Offset: 0x000E4122
	private void EndBattleCryEffect()
	{
		this.m_isInBattleCryEffect = false;
		this.EndMobileTargetingEffect();
		this.m_battlecrySourceCard.SetBattleCrySource(false);
	}

	// Token: 0x06002D61 RID: 11617 RVA: 0x000E5F40 File Offset: 0x000E4140
	private void ApplyMobileTargettingEffectToActor(Actor actor)
	{
		if (actor == null || actor.gameObject == null)
		{
			return;
		}
		SceneUtils.SetLayer(actor.gameObject, GameLayer.IgnoreFullScreenEffects);
		Hashtable args = iTween.Hash(new object[]
		{
			"y",
			0.8f,
			"time",
			0.4f,
			"easeType",
			iTween.EaseType.easeOutQuad,
			"name",
			"position",
			"isLocal",
			true
		});
		Hashtable args2 = iTween.Hash(new object[]
		{
			"x",
			1.08f,
			"z",
			1.08f,
			"time",
			0.4f,
			"easeType",
			iTween.EaseType.easeOutQuad,
			"name",
			"scale"
		});
		iTween.StopByName(actor.gameObject, "position");
		iTween.StopByName(actor.gameObject, "scale");
		iTween.MoveTo(actor.gameObject, args);
		iTween.ScaleTo(actor.gameObject, args2);
	}

	// Token: 0x06002D62 RID: 11618 RVA: 0x000E6084 File Offset: 0x000E4284
	private void RemoveMobileTargettingEffectFromActor(Actor actor)
	{
		if (actor == null || actor.gameObject == null)
		{
			return;
		}
		SceneUtils.SetLayer(actor.gameObject, GameLayer.Default);
		SceneUtils.SetLayer(actor.GetMeshRenderer(false).gameObject, GameLayer.CardRaycast);
		Hashtable args = iTween.Hash(new object[]
		{
			"x",
			0f,
			"y",
			0f,
			"z",
			0f,
			"time",
			0.5f,
			"easeType",
			iTween.EaseType.easeOutQuad,
			"name",
			"position",
			"isLocal",
			true
		});
		Hashtable args2 = iTween.Hash(new object[]
		{
			"x",
			1f,
			"z",
			1f,
			"time",
			0.4f,
			"easeType",
			iTween.EaseType.easeOutQuad,
			"name",
			"scale"
		});
		iTween.StopByName(actor.gameObject, "position");
		iTween.StopByName(actor.gameObject, "scale");
		iTween.MoveTo(actor.gameObject, args);
		iTween.ScaleTo(actor.gameObject, args2);
	}

	// Token: 0x06002D63 RID: 11619 RVA: 0x000E6208 File Offset: 0x000E4408
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
			this.DoEndTurnButton();
			TurnStartManager.Get().BeginListeningForTurnEvents(false);
			MulliganManager.Get().SkipMulliganForDev();
			return true;
		}
		return false;
	}

	// Token: 0x06002D64 RID: 11620 RVA: 0x0001FA65 File Offset: 0x0001DC65
	private bool HandleUniversalHotkeys()
	{
		return false;
	}

	// Token: 0x06002D65 RID: 11621 RVA: 0x000E627D File Offset: 0x000E447D
	private bool HandleGameHotkeys()
	{
		return (GameState.Get() == null || !GameState.Get().IsMulliganManagerActive()) && InputCollection.GetKeyUp(KeyCode.Escape) && this.CancelOption(false);
	}

	// Token: 0x06002D66 RID: 11622 RVA: 0x000E62A8 File Offset: 0x000E44A8
	private void ShowBullseyeIfNeeded()
	{
		if (TargetReticleManager.Get() == null)
		{
			return;
		}
		if (!TargetReticleManager.Get().IsActive())
		{
			return;
		}
		bool show = this.m_mousedOverCard != null && GameState.Get().IsValidOptionTarget(this.m_mousedOverCard.GetEntity(), false);
		TargetReticleManager.Get().ShowBullseye(show);
	}

	// Token: 0x06002D67 RID: 11623 RVA: 0x000E6304 File Offset: 0x000E4504
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
			Entity entity2 = weaponCard ? weaponCard.GetEntity() : null;
			if (entity2 != null && entity2.GetRealTimeIsPoisonous())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002D68 RID: 11624 RVA: 0x000E6358 File Offset: 0x000E4558
	private void ShowSkullIfNeeded()
	{
		if (this.GetBattlecrySourceCard() != null)
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
		if (!entity.IsMinion() && !entity.IsHero())
		{
			return;
		}
		Entity entity2 = this.m_mousedOverCard.GetEntity();
		if (!entity2.IsMinion() && !entity2.IsHero())
		{
			return;
		}
		if (!GameState.Get().IsValidOptionTarget(entity2, false))
		{
			return;
		}
		if (entity2.IsObfuscated())
		{
			return;
		}
		int num = entity.GetRealTimeAttack();
		if (entity2.HasTag(GAME_TAG.HEAVILY_ARMORED))
		{
			num = Mathf.Min(num, 1);
		}
		if (entity2.CanBeDamagedRealTime() && (num >= entity2.GetRealTimeRemainingHP() || (this.EntityIsPoisonousForSkullPreview(entity) && entity2.IsMinion())))
		{
			if (this.EntityIsPoisonousForSkullPreview(entity))
			{
				DamageSplatSpell damageSplatSpell = this.m_mousedOverCard.ActivateActorSpell(SpellType.DAMAGE) as DamageSplatSpell;
				if (damageSplatSpell != null)
				{
					damageSplatSpell.SetPoisonous(true);
					damageSplatSpell.ActivateState(SpellStateType.IDLE);
					damageSplatSpell.transform.localScale = Vector3.zero;
					iTween.ScaleTo(damageSplatSpell.gameObject, iTween.Hash(new object[]
					{
						"scale",
						Vector3.one,
						"time",
						0.5f,
						"easetype",
						iTween.EaseType.easeOutElastic
					}));
				}
			}
			else
			{
				Spell spell = this.m_mousedOverCard.ActivateActorSpell(SpellType.SKULL);
				if (spell != null)
				{
					spell.transform.localScale = Vector3.zero;
					iTween.ScaleTo(spell.gameObject, iTween.Hash(new object[]
					{
						"scale",
						Vector3.one,
						"time",
						0.5f,
						"easetype",
						iTween.EaseType.easeOutElastic
					}));
				}
			}
		}
		int num2 = entity2.GetRealTimeAttack();
		if (entity.HasTag(GAME_TAG.HEAVILY_ARMORED))
		{
			num2 = Mathf.Min(num2, 1);
		}
		if (entity.CanBeDamagedRealTime() && (num2 >= entity.GetRealTimeRemainingHP() || (this.EntityIsPoisonousForSkullPreview(entity2) && entity.IsMinion())))
		{
			if (this.EntityIsPoisonousForSkullPreview(entity2))
			{
				DamageSplatSpell damageSplatSpell2 = entity.GetCard().ActivateActorSpell(SpellType.DAMAGE) as DamageSplatSpell;
				if (damageSplatSpell2 != null)
				{
					damageSplatSpell2.SetPoisonous(true);
					damageSplatSpell2.ActivateState(SpellStateType.IDLE);
					damageSplatSpell2.transform.localScale = Vector3.zero;
					iTween.ScaleTo(damageSplatSpell2.gameObject, iTween.Hash(new object[]
					{
						"scale",
						Vector3.one,
						"time",
						0.5f,
						"easetype",
						iTween.EaseType.easeOutElastic
					}));
					return;
				}
			}
			else
			{
				Spell spell2 = entity.GetCard().ActivateActorSpell(SpellType.SKULL);
				if (spell2 != null)
				{
					spell2.transform.localScale = Vector3.zero;
					iTween.ScaleTo(spell2.gameObject, iTween.Hash(new object[]
					{
						"scale",
						Vector3.one,
						"time",
						0.5f,
						"easetype",
						iTween.EaseType.easeOutElastic
					}));
				}
			}
		}
	}

	// Token: 0x06002D69 RID: 11625 RVA: 0x000E66B0 File Offset: 0x000E48B0
	private void DisableSkullIfNeeded(Card mousedOverCard)
	{
		Spell actorSpell = mousedOverCard.GetActorSpell(SpellType.SKULL, true);
		if (actorSpell != null)
		{
			iTween.Stop(actorSpell.gameObject);
			actorSpell.transform.localScale = Vector3.zero;
			actorSpell.Deactivate();
		}
		Spell actorSpell2 = mousedOverCard.GetActorSpell(SpellType.DAMAGE, true);
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
		if (card == null)
		{
			return;
		}
		actorSpell = card.GetActorSpell(SpellType.SKULL, true);
		if (actorSpell != null)
		{
			iTween.Stop(actorSpell.gameObject);
			actorSpell.transform.localScale = Vector3.zero;
			actorSpell.Deactivate();
		}
		actorSpell2 = card.GetActorSpell(SpellType.DAMAGE, true);
		if (actorSpell2 != null)
		{
			iTween.Stop(actorSpell2.gameObject);
			actorSpell2.transform.localScale = Vector3.zero;
			actorSpell2.Deactivate();
		}
	}

	// Token: 0x06002D6A RID: 11626 RVA: 0x000E67DC File Offset: 0x000E49DC
	private void HandleMouseOverCard(Card card)
	{
		if (!card.IsInputEnabled())
		{
			return;
		}
		GameState gameState = GameState.Get();
		this.m_mousedOverCard = card;
		bool flag = gameState.IsFriendlySidePlayerTurn() && TargetReticleManager.Get() && TargetReticleManager.Get().IsActive();
		if (!this.PermitDecisionMakingInput())
		{
			flag = false;
		}
		if (gameState.IsMainPhase() && this.m_heldCard == null && !ChoiceCardMgr.Get().HasSubOption() && !flag && (!UniversalInputManager.Get().IsTouchMode() || card.gameObject == this.m_lastObjectMousedDown))
		{
			this.SetShouldShowTooltip();
		}
		card.NotifyMousedOver();
		if (gameState.IsMulliganManagerActive() && card.GetEntity().IsControlledByFriendlySidePlayer() && card.GetZone() is ZoneHand && !UniversalInputManager.UsePhoneUI)
		{
			TooltipPanelManager.Get().UpdateKeywordHelpForMulliganCard(card.GetEntity(), card.GetActor());
		}
		this.ShowBullseyeIfNeeded();
		this.ShowSkullIfNeeded();
	}

	// Token: 0x06002D6B RID: 11627 RVA: 0x000E68C9 File Offset: 0x000E4AC9
	public void NotifyCardDestroyed(Card destroyedCard)
	{
		if (destroyedCard == this.m_mousedOverCard)
		{
			this.HandleMouseOffCard();
		}
	}

	// Token: 0x06002D6C RID: 11628 RVA: 0x000E68E0 File Offset: 0x000E4AE0
	private void HandleMouseOffCard()
	{
		PegCursor.Get().SetMode(PegCursor.Mode.UP);
		Card mousedOverCard = this.m_mousedOverCard;
		this.m_mousedOverCard = null;
		mousedOverCard.HideTooltip();
		mousedOverCard.NotifyMousedOut();
		this.ShowBullseyeIfNeeded();
		this.DisableSkullIfNeeded(mousedOverCard);
	}

	// Token: 0x06002D6D RID: 11629 RVA: 0x000E6920 File Offset: 0x000E4B20
	public void HandleMemberClick()
	{
		if (this.m_mousedOverObject == null)
		{
			RaycastHit raycastHit;
			if (UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.PlayAreaCollision, out raycastHit))
			{
				if (GameState.Get() == null)
				{
					return;
				}
				if (GameState.Get().IsMulliganManagerActive())
				{
					return;
				}
				RaycastHit raycastHit2;
				if (UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast, out raycastHit2))
				{
					return;
				}
				GameObject mouseClickDustEffectPrefab = Board.Get().GetMouseClickDustEffectPrefab();
				if (mouseClickDustEffectPrefab == null)
				{
					return;
				}
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(mouseClickDustEffectPrefab);
				gameObject.transform.position = raycastHit.point;
				ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
				if (componentsInChildren == null)
				{
					return;
				}
				Vector3 euler = new Vector3(Input.GetAxis("Mouse Y") * 40f, Input.GetAxis("Mouse X") * 40f, 0f);
				foreach (ParticleSystem particleSystem in componentsInChildren)
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
					array2 = this.DEFAULT_BOARD_CLICK_SOUNDS;
				}
				string input = array2[UnityEngine.Random.Range(0, array2.Length)];
				SoundManager.Get().LoadAndPlay(input, gameObject);
				return;
			}
			else if (Gameplay.Get() != null)
			{
				SoundManager.Get().LoadAndPlay("UI_MouseClick_01.prefab:fa537702a0db1c3478c989967458788b");
			}
		}
	}

	// Token: 0x06002D6E RID: 11630 RVA: 0x000E6A9D File Offset: 0x000E4C9D
	public bool MouseIsMoving(float tolerance)
	{
		return Mathf.Abs(Input.GetAxis("Mouse X")) > tolerance || Mathf.Abs(Input.GetAxis("Mouse Y")) > tolerance;
	}

	// Token: 0x06002D6F RID: 11631 RVA: 0x000E6AC6 File Offset: 0x000E4CC6
	public bool MouseIsMoving()
	{
		return this.MouseIsMoving(0f);
	}

	// Token: 0x06002D70 RID: 11632 RVA: 0x000E6AD4 File Offset: 0x000E4CD4
	private void ShowTooltipIfNecessary()
	{
		if (this.m_mousedOverCard == null)
		{
			return;
		}
		if (!this.m_mousedOverCard.GetShouldShowTooltip())
		{
			return;
		}
		this.m_mousedOverTimer += Time.unscaledDeltaTime;
		if (!this.m_mousedOverCard.IsActorReady())
		{
			return;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.MOUSEOVER_DELAY_OVERRIDDEN))
		{
			this.m_mousedOverCard.ShowTooltip();
			return;
		}
		if (this.m_mousedOverCard.GetZone() is ZoneHand)
		{
			this.m_mousedOverCard.ShowTooltip();
			return;
		}
		if (this.m_mousedOverTimer >= this.m_MouseOverDelay)
		{
			this.m_mousedOverCard.ShowTooltip();
		}
	}

	// Token: 0x06002D71 RID: 11633 RVA: 0x000E6B70 File Offset: 0x000E4D70
	private void ShowTooltipZone(GameObject hitObject, TooltipZone tooltip)
	{
		this.HideBigViewCardBacks();
		GameState gameState = GameState.Get();
		if (gameState.IsMulliganManagerActive())
		{
			return;
		}
		GameEntity gameEntity = gameState.GetGameEntity();
		if (gameEntity == null)
		{
			return;
		}
		if (gameEntity.GetGameOptions().GetBooleanOption(GameEntityOption.DISABLE_TOOLTIPS))
		{
			return;
		}
		if (gameEntity.NotifyOfTooltipDisplay(tooltip))
		{
			return;
		}
		InputManager.ZoneTooltipSettings zoneTooltipSettings = gameEntity.GetZoneTooltipSettings();
		ManaCrystalMgr component = tooltip.targetObject.GetComponent<ManaCrystalMgr>();
		if (component != null && zoneTooltipSettings.FriendlyMana.Allowed)
		{
			string headline = null;
			string description = null;
			if (zoneTooltipSettings.FriendlyMana.GetTooltipOverrideContent(ref headline, ref description, 0))
			{
				this.ShowTooltipInZone(tooltip, headline, description, 0);
			}
			if (component.ShouldShowTooltip(ManaCrystalType.DEFAULT))
			{
				int tag = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.OVERLOAD_OWED);
				if (tag > 0)
				{
					string headline2 = GameStrings.Format("GAMEPLAY_TOOLTIP_MANA_OVERLOAD_HEADLINE", Array.Empty<object>());
					string description2 = GameStrings.Format("GAMEPLAY_TOOLTIP_MANA_OVERLOAD_DESCRIPTION", new object[]
					{
						tag
					});
					this.ShowTooltipInZone(tooltip, headline2, description2, 0);
				}
				else
				{
					this.ShowTooltipInZone(tooltip, GameStrings.Get("GAMEPLAY_TOOLTIP_MANA_HEADLINE"), GameStrings.Get("GAMEPLAY_TOOLTIP_MANA_DESCRIPTION"), 0);
				}
				int tag2 = GameState.Get().GetFriendlySidePlayer().GetTag(GAME_TAG.OVERLOAD_LOCKED);
				if (tag2 > 0)
				{
					string headline3 = GameStrings.Format("GAMEPLAY_TOOLTIP_MANA_LOCKED_HEADLINE", Array.Empty<object>());
					string description3 = GameStrings.Format("GAMEPLAY_TOOLTIP_MANA_LOCKED_DESCRIPTION", new object[]
					{
						tag2
					});
					this.AddTooltipInZone(tooltip, headline3, description3);
					return;
				}
			}
			else if (component.ShouldShowTooltip(ManaCrystalType.COIN))
			{
				this.ShowTooltipInZone(tooltip, GameStrings.Get("GAMEPLAY_TOOLTIP_MANA_COIN_HEADLINE"), GameStrings.Get("GAMEPLAY_TOOLTIP_MANA_COIN_DESCRIPTION"), 0);
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
					Vector3 zero = Vector3.zero;
					string headline4 = null;
					string description4 = null;
					if (!zoneTooltipSettings.FriendlyDeck.GetTooltipOverrideContent(ref headline4, ref description4, 0))
					{
						if (component2.IsFatigued())
						{
							if (UniversalInputManager.UsePhoneUI)
							{
								zero = new Vector3(0f, 0f, 0.562f);
							}
							headline4 = GameStrings.Get("GAMEPLAY_TOOLTIP_FATIGUE_DECK_HEADLINE");
							description4 = GameStrings.Get("GAMEPLAY_TOOLTIP_FATIGUE_DECK_DESCRIPTION");
						}
						else
						{
							headline4 = GameStrings.Format("GAMEPLAY_TOOLTIP_DECK_HEADLINE", Array.Empty<object>());
							description4 = GameStrings.Format("GAMEPLAY_TOOLTIP_DECK_DESCRIPTION", new object[]
							{
								component2.GetCards().Count
							});
						}
					}
					this.ShowTooltipInZone(tooltip, headline4, description4, zero, 0);
				}
				if (component2.m_playerHandTooltipZone != null && zoneTooltipSettings.FriendlyHand.Allowed)
				{
					Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
					int count = friendlySidePlayer.GetHandZone().GetCards().Count;
					if (count >= 5 && !GameMgr.Get().IsTutorial())
					{
						string headline5 = null;
						string description5 = null;
						if (!zoneTooltipSettings.FriendlyHand.GetTooltipOverrideContent(ref headline5, ref description5, 0))
						{
							headline5 = GameStrings.Get("GAMEPLAY_TOOLTIP_HAND_HEADLINE");
							description5 = GameStrings.Format("GAMEPLAY_TOOLTIP_HAND_DESCRIPTION", new object[]
							{
								count
							});
							if (count >= friendlySidePlayer.GetTag(GAME_TAG.MAXHANDSIZE))
							{
								headline5 = GameStrings.Get("GAMEPLAY_TOOLTIP_HAND_FULL_HEADLINE");
								description5 = GameStrings.Format("GAMEPLAY_TOOLTIP_HAND_FULL_DESCRIPTION", new object[]
								{
									count
								});
							}
						}
						this.ShowTooltipInZone(component2.m_playerHandTooltipZone, headline5, description5, 0);
						return;
					}
				}
			}
			else if (component2.m_Side == Player.Side.OPPOSING)
			{
				if (zoneTooltipSettings.EnemyDeck.Allowed)
				{
					string headline6 = null;
					string description6 = null;
					if (!zoneTooltipSettings.EnemyDeck.GetTooltipOverrideContent(ref headline6, ref description6, 0))
					{
						if (component2.IsFatigued())
						{
							headline6 = GameStrings.Get("GAMEPLAY_TOOLTIP_FATIGUE_ENEMYDECK_HEADLINE");
							description6 = GameStrings.Get("GAMEPLAY_TOOLTIP_FATIGUE_ENEMYDECK_DESCRIPTION");
						}
						else
						{
							headline6 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYDECK_HEADLINE");
							description6 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYDECK_DESC", new object[]
							{
								component2.GetCards().Count
							});
						}
					}
					this.ShowTooltipInZone(tooltip, headline6, description6, 0);
					if (zoneTooltipSettings.EnemyDeck.GetTooltipOverrideContent(ref headline6, ref description6, 1))
					{
						this.AddTooltipInZone(tooltip, headline6, description6);
					}
				}
				if (component2.m_playerHandTooltipZone != null && zoneTooltipSettings.EnemyHand.Allowed)
				{
					int count2 = GameState.Get().GetOpposingSidePlayer().GetHandZone().GetCards().Count;
					if (count2 >= 5 && !GameMgr.Get().IsTutorial())
					{
						string headline7 = null;
						string description7 = null;
						if (!zoneTooltipSettings.EnemyHand.GetTooltipOverrideContent(ref headline7, ref description7, 0))
						{
							headline7 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYHAND_HEADLINE");
							description7 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYHAND_DESC", new object[]
							{
								count2
							});
						}
						this.ShowTooltipInZone(component2.m_playerHandTooltipZone, headline7, description7, 0);
					}
				}
				int tag3 = GameState.Get().GetOpposingSidePlayer().GetTag(GAME_TAG.OVERLOAD_OWED);
				if (zoneTooltipSettings.EnemyMana.Allowed && tag3 > 0)
				{
					if (UniversalInputManager.UsePhoneUI && component2.m_playerHandTooltipZone != null)
					{
						string headline8 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_HEADLINE");
						string description8 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_DESC", new object[]
						{
							tag3
						});
						this.AddTooltipInZone(component2.m_playerHandTooltipZone, headline8, description8);
						return;
					}
					if (!UniversalInputManager.UsePhoneUI && component2.m_playerManaTooltipZone != null)
					{
						string headline9 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_HEADLINE");
						string description9 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_DESC", new object[]
						{
							tag3
						});
						this.ShowTooltipInZone(component2.m_playerManaTooltipZone, headline9, description9, 0);
					}
				}
			}
			return;
		}
		ZoneHand component3 = tooltip.targetObject.GetComponent<ZoneHand>();
		if (!(component3 != null) || component3.m_Side != Player.Side.OPPOSING)
		{
			ManaCounter component4 = tooltip.targetObject.GetComponent<ManaCounter>();
			if (component4 != null && component4.m_Side == Player.Side.OPPOSING && zoneTooltipSettings.EnemyMana.Allowed)
			{
				int tag4 = GameState.Get().GetOpposingSidePlayer().GetTag(GAME_TAG.OVERLOAD_OWED);
				if (tag4 > 0)
				{
					string headline10 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_HEADLINE");
					string description10 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_DESC", new object[]
					{
						tag4
					});
					this.ShowTooltipInZone(tooltip, headline10, description10, 0);
				}
			}
			return;
		}
		if (GameMgr.Get().IsTutorial())
		{
			this.ShowTooltipInZone(tooltip, GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYHAND_HEADLINE"), GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYHAND_DESC_TUT"), 0);
			return;
		}
		if (zoneTooltipSettings.EnemyHand.Allowed)
		{
			string headline11 = null;
			string description11 = null;
			if (!zoneTooltipSettings.EnemyHand.GetTooltipOverrideContent(ref headline11, ref description11, 0))
			{
				int cardCount = component3.GetCardCount();
				if (cardCount == 1)
				{
					headline11 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYHAND_HEADLINE");
					description11 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYHAND_DESC_SINGLE", new object[]
					{
						cardCount
					});
				}
				else
				{
					headline11 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYHAND_HEADLINE");
					description11 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYHAND_DESC", new object[]
					{
						cardCount
					});
				}
			}
			this.ShowTooltipInZone(tooltip, headline11, description11, 0);
			if (UniversalInputManager.UsePhoneUI && zoneTooltipSettings.EnemyMana.Allowed)
			{
				int tag5 = GameState.Get().GetOpposingSidePlayer().GetTag(GAME_TAG.OVERLOAD_OWED);
				if (tag5 > 0)
				{
					string headline12 = GameStrings.Get("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_HEADLINE");
					string description12 = GameStrings.Format("GAMEPLAY_TOOLTIP_ENEMYOVERLOAD_DESC", new object[]
					{
						tag5
					});
					this.AddTooltipInZone(tooltip, headline12, description12);
				}
			}
		}
	}

	// Token: 0x06002D72 RID: 11634 RVA: 0x000E72B4 File Offset: 0x000E54B4
	private void AddTooltipInZone(TooltipZone tooltip, string headline, string description)
	{
		for (int i = 0; i < 10; i++)
		{
			if (!tooltip.IsShowingTooltip(i))
			{
				this.ShowTooltipInZone(tooltip, headline, description, Vector3.zero, i);
				return;
			}
		}
		Debug.LogError(string.Concat(new object[]
		{
			"You are trying to add too many tooltips. TooltipZone = [",
			tooltip.gameObject.name,
			"] MAX_TOOLTIPS = [",
			10,
			"]"
		}));
	}

	// Token: 0x06002D73 RID: 11635 RVA: 0x000E7327 File Offset: 0x000E5527
	private void ShowTooltipInZone(TooltipZone tooltip, string headline, string description, int index = 0)
	{
		this.ShowTooltipInZone(tooltip, headline, description, Vector3.zero, index);
	}

	// Token: 0x06002D74 RID: 11636 RVA: 0x000E7339 File Offset: 0x000E5539
	private void ShowTooltipInZone(TooltipZone tooltip, string headline, string description, Vector3 localOffset, int index = 0)
	{
		GameState.Get().GetGameEntity().NotifyOfTooltipZoneMouseOver(tooltip);
		if (UniversalInputManager.Get().IsTouchMode())
		{
			tooltip.ShowGameplayTooltipLarge(headline, description, localOffset, index);
			return;
		}
		tooltip.ShowGameplayTooltip(headline, description, localOffset, index);
	}

	// Token: 0x06002D75 RID: 11637 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void HideBigViewCardBacks()
	{
	}

	// Token: 0x06002D76 RID: 11638 RVA: 0x000E7370 File Offset: 0x000E5570
	private void PredictSpentMana(Entity entity)
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		if (friendlySidePlayer.GetRealTimeSpellsCostHealth() && entity.GetRealTimeCardType() == TAG_CARDTYPE.SPELL)
		{
			return;
		}
		if (entity.GetRealTimeCardCostsHealth())
		{
			return;
		}
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
		this.m_entitiesThatPredictedMana.Add(entity);
	}

	// Token: 0x06002D77 RID: 11639 RVA: 0x000E7414 File Offset: 0x000E5614
	private void RollbackSpentMana(Entity entity)
	{
		int num = this.m_entitiesThatPredictedMana.IndexOf(entity);
		if (num < 0)
		{
			return;
		}
		this.m_entitiesThatPredictedMana.RemoveAt(num);
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

	// Token: 0x06002D78 RID: 11640 RVA: 0x000E74A4 File Offset: 0x000E56A4
	public void OnManaCrystalMgrManaSpent()
	{
		if (this.m_mousedOverCard)
		{
			this.m_mousedOverCard.UpdateProposedManaUsage();
		}
	}

	// Token: 0x06002D79 RID: 11641 RVA: 0x000E74BE File Offset: 0x000E56BE
	private bool IsInZone(Entity entity, TAG_ZONE zoneTag)
	{
		return this.IsInZone(entity.GetCard(), zoneTag);
	}

	// Token: 0x06002D7A RID: 11642 RVA: 0x000E74CD File Offset: 0x000E56CD
	private bool IsInZone(Card card, TAG_ZONE zoneTag)
	{
		return !(card.GetZone() == null) && GameUtils.GetFinalZoneForEntity(card.GetEntity()) == zoneTag;
	}

	// Token: 0x06002D7B RID: 11643 RVA: 0x000E74F0 File Offset: 0x000E56F0
	private void SetDragging(bool dragging)
	{
		this.m_dragging = dragging;
		GraphicsManager graphicsManager = GraphicsManager.Get();
		if (graphicsManager != null)
		{
			graphicsManager.SetDraggingFramerate(dragging);
		}
	}

	// Token: 0x06002D7C RID: 11644 RVA: 0x000E7514 File Offset: 0x000E5714
	public bool RegisterPhoneHandShownListener(InputManager.PhoneHandShownCallback callback)
	{
		return this.RegisterPhoneHandShownListener(callback, null);
	}

	// Token: 0x06002D7D RID: 11645 RVA: 0x000E7520 File Offset: 0x000E5720
	public bool RegisterPhoneHandShownListener(InputManager.PhoneHandShownCallback callback, object userData)
	{
		InputManager.PhoneHandShownListener phoneHandShownListener = new InputManager.PhoneHandShownListener();
		phoneHandShownListener.SetCallback(callback);
		phoneHandShownListener.SetUserData(userData);
		if (this.m_phoneHandShownListener.Contains(phoneHandShownListener))
		{
			return false;
		}
		this.m_phoneHandShownListener.Add(phoneHandShownListener);
		return true;
	}

	// Token: 0x06002D7E RID: 11646 RVA: 0x000E755E File Offset: 0x000E575E
	public bool RemovePhoneHandShownListener(InputManager.PhoneHandShownCallback callback)
	{
		return this.RemovePhoneHandShownListener(callback, null);
	}

	// Token: 0x06002D7F RID: 11647 RVA: 0x000E7568 File Offset: 0x000E5768
	public bool RemovePhoneHandShownListener(InputManager.PhoneHandShownCallback callback, object userData)
	{
		InputManager.PhoneHandShownListener phoneHandShownListener = new InputManager.PhoneHandShownListener();
		phoneHandShownListener.SetCallback(callback);
		phoneHandShownListener.SetUserData(userData);
		return this.m_phoneHandShownListener.Remove(phoneHandShownListener);
	}

	// Token: 0x06002D80 RID: 11648 RVA: 0x000E7595 File Offset: 0x000E5795
	public bool RegisterPhoneHandHiddenListener(InputManager.PhoneHandHiddenCallback callback)
	{
		return this.RegisterPhoneHandHiddenListener(callback, null);
	}

	// Token: 0x06002D81 RID: 11649 RVA: 0x000E75A0 File Offset: 0x000E57A0
	public bool RegisterPhoneHandHiddenListener(InputManager.PhoneHandHiddenCallback callback, object userData)
	{
		InputManager.PhoneHandHiddenListener phoneHandHiddenListener = new InputManager.PhoneHandHiddenListener();
		phoneHandHiddenListener.SetCallback(callback);
		phoneHandHiddenListener.SetUserData(userData);
		if (this.m_phoneHandHiddenListener.Contains(phoneHandHiddenListener))
		{
			return false;
		}
		this.m_phoneHandHiddenListener.Add(phoneHandHiddenListener);
		return true;
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x000E75DE File Offset: 0x000E57DE
	public bool RemovePhoneHandHiddenListener(InputManager.PhoneHandHiddenCallback callback)
	{
		return this.RemovePhoneHandHiddenListener(callback, null);
	}

	// Token: 0x06002D83 RID: 11651 RVA: 0x000E75E8 File Offset: 0x000E57E8
	public bool RemovePhoneHandHiddenListener(InputManager.PhoneHandHiddenCallback callback, object userData)
	{
		InputManager.PhoneHandHiddenListener phoneHandHiddenListener = new InputManager.PhoneHandHiddenListener();
		phoneHandHiddenListener.SetCallback(callback);
		phoneHandHiddenListener.SetUserData(userData);
		return this.m_phoneHandHiddenListener.Remove(phoneHandHiddenListener);
	}

	// Token: 0x040018D6 RID: 6358
	public float m_MouseOverDelay = 0.4f;

	// Token: 0x040018D7 RID: 6359
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

	// Token: 0x040018D8 RID: 6360
	private readonly PlatformDependentValue<float> MIN_GRAB_Y = new PlatformDependentValue<float>(PlatformCategory.Screen)
	{
		Tablet = 80f,
		Phone = 80f
	};

	// Token: 0x040018D9 RID: 6361
	private const float MOBILE_TARGETTING_Y_OFFSET = 0.8f;

	// Token: 0x040018DA RID: 6362
	private const float MOBILE_TARGETTING_XY_SCALE = 1.08f;

	// Token: 0x040018DB RID: 6363
	private static InputManager s_instance;

	// Token: 0x040018DC RID: 6364
	private ZoneHand m_myHandZone;

	// Token: 0x040018DD RID: 6365
	private ZonePlay m_myPlayZone;

	// Token: 0x040018DE RID: 6366
	private ZoneWeapon m_myWeaponZone;

	// Token: 0x040018DF RID: 6367
	private ZoneHand m_enemyHandZone;

	// Token: 0x040018E0 RID: 6368
	private ZonePlay m_enemyPlayZone;

	// Token: 0x040018E1 RID: 6369
	private Card m_heldCard;

	// Token: 0x040018E2 RID: 6370
	private bool m_checkForInput;

	// Token: 0x040018E3 RID: 6371
	private GameObject m_lastObjectMousedDown;

	// Token: 0x040018E4 RID: 6372
	private GameObject m_lastObjectRightMousedDown;

	// Token: 0x040018E5 RID: 6373
	private Vector3 m_lastMouseDownPosition;

	// Token: 0x040018E6 RID: 6374
	private bool m_leftMouseButtonIsDown;

	// Token: 0x040018E7 RID: 6375
	private bool m_dragging;

	// Token: 0x040018E8 RID: 6376
	private bool m_lastInputDrag;

	// Token: 0x040018E9 RID: 6377
	private Card m_mousedOverCard;

	// Token: 0x040018EA RID: 6378
	private HistoryCard m_mousedOverHistoryCard;

	// Token: 0x040018EB RID: 6379
	private GameObject m_mousedOverObject;

	// Token: 0x040018EC RID: 6380
	private float m_mousedOverTimer;

	// Token: 0x040018ED RID: 6381
	private ZoneChangeList m_lastZoneChangeList;

	// Token: 0x040018EE RID: 6382
	private Card m_battlecrySourceCard;

	// Token: 0x040018EF RID: 6383
	private List<Card> m_cancelingBattlecryCards = new List<Card>();

	// Token: 0x040018F0 RID: 6384
	private bool m_cardWasInsideHandLastFrame;

	// Token: 0x040018F1 RID: 6385
	private bool m_isInBattleCryEffect;

	// Token: 0x040018F2 RID: 6386
	private List<Entity> m_entitiesThatPredictedMana = new List<Entity>();

	// Token: 0x040018F3 RID: 6387
	private List<Actor> m_mobileTargettingEffectActors = new List<Actor>();

	// Token: 0x040018F4 RID: 6388
	private Card m_lastPreviewedCard;

	// Token: 0x040018F5 RID: 6389
	private bool m_touchDraggingCard;

	// Token: 0x040018F6 RID: 6390
	private bool m_useHandEnlarge;

	// Token: 0x040018F7 RID: 6391
	private bool m_hideHandAfterPlayingCard;

	// Token: 0x040018F8 RID: 6392
	private bool m_targettingHeroPower;

	// Token: 0x040018F9 RID: 6393
	private bool m_touchedDownOnSmallHand;

	// Token: 0x040018FA RID: 6394
	private bool m_enlargeHandAfterDropCard;

	// Token: 0x040018FB RID: 6395
	private int m_telemetryNumDragAttacks;

	// Token: 0x040018FC RID: 6396
	private int m_telemetryNumClickAttacks;

	// Token: 0x040018FD RID: 6397
	private readonly string[] DEFAULT_BOARD_CLICK_SOUNDS = new string[]
	{
		"board_common_dirt_poke_1.prefab:db7d81ea320f3bb4b9fa44bcd371d379",
		"board_common_dirt_poke_2.prefab:a078131beb0546444b4ccfc41ec5c547",
		"board_common_dirt_poke_3.prefab:7fbdaca211c05b94382e3142dfdbb306",
		"board_common_dirt_poke_4.prefab:d2713c07dcb56904da5ce08da04b5d26",
		"board_common_dirt_poke_5.prefab:c7234b85b15bca047b7ce32dc96bc851"
	};

	// Token: 0x040018FE RID: 6398
	private List<InputManager.PhoneHandShownListener> m_phoneHandShownListener = new List<InputManager.PhoneHandShownListener>();

	// Token: 0x040018FF RID: 6399
	private List<InputManager.PhoneHandHiddenListener> m_phoneHandHiddenListener = new List<InputManager.PhoneHandHiddenListener>();

	// Token: 0x0200169E RID: 5790
	// (Invoke) Token: 0x0600E4D0 RID: 58576
	public delegate bool TooltipContentDelegate(ref string headline, ref string description, int index);

	// Token: 0x0200169F RID: 5791
	public class TooltipSettings
	{
		// Token: 0x0600E4D3 RID: 58579 RVA: 0x00407309 File Offset: 0x00405509
		public TooltipSettings(bool allowed)
		{
			this.Allowed = allowed;
			this.m_overrideContentDelegate = null;
		}

		// Token: 0x0600E4D4 RID: 58580 RVA: 0x0040731F File Offset: 0x0040551F
		public TooltipSettings(bool allowed, InputManager.TooltipContentDelegate contentDelegate)
		{
			this.Allowed = allowed;
			this.m_overrideContentDelegate = contentDelegate;
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x0600E4D5 RID: 58581 RVA: 0x00407335 File Offset: 0x00405535
		// (set) Token: 0x0600E4D6 RID: 58582 RVA: 0x0040733D File Offset: 0x0040553D
		public bool Allowed { get; private set; }

		// Token: 0x0600E4D7 RID: 58583 RVA: 0x00407346 File Offset: 0x00405546
		public bool GetTooltipOverrideContent(ref string headline, ref string description, int index = 0)
		{
			return this.m_overrideContentDelegate != null && this.m_overrideContentDelegate(ref headline, ref description, index);
		}

		// Token: 0x0400B132 RID: 45362
		private InputManager.TooltipContentDelegate m_overrideContentDelegate;
	}

	// Token: 0x020016A0 RID: 5792
	public class ZoneTooltipSettings
	{
		// Token: 0x0400B134 RID: 45364
		public InputManager.TooltipSettings EnemyHand = new InputManager.TooltipSettings(true);

		// Token: 0x0400B135 RID: 45365
		public InputManager.TooltipSettings EnemyDeck = new InputManager.TooltipSettings(true);

		// Token: 0x0400B136 RID: 45366
		public InputManager.TooltipSettings EnemyMana = new InputManager.TooltipSettings(true);

		// Token: 0x0400B137 RID: 45367
		public InputManager.TooltipSettings FriendlyHand = new InputManager.TooltipSettings(true);

		// Token: 0x0400B138 RID: 45368
		public InputManager.TooltipSettings FriendlyDeck = new InputManager.TooltipSettings(true);

		// Token: 0x0400B139 RID: 45369
		public InputManager.TooltipSettings FriendlyMana = new InputManager.TooltipSettings(true);
	}

	// Token: 0x020016A1 RID: 5793
	// (Invoke) Token: 0x0600E4DA RID: 58586
	public delegate void PhoneHandShownCallback(object userData);

	// Token: 0x020016A2 RID: 5794
	private class PhoneHandShownListener : EventListener<InputManager.PhoneHandShownCallback>
	{
		// Token: 0x0600E4DD RID: 58589 RVA: 0x004073BB File Offset: 0x004055BB
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}

	// Token: 0x020016A3 RID: 5795
	// (Invoke) Token: 0x0600E4E0 RID: 58592
	public delegate void PhoneHandHiddenCallback(object userData);

	// Token: 0x020016A4 RID: 5796
	private class PhoneHandHiddenListener : EventListener<InputManager.PhoneHandHiddenCallback>
	{
		// Token: 0x0600E4E3 RID: 58595 RVA: 0x004073D6 File Offset: 0x004055D6
		public void Fire()
		{
			this.m_callback(this.m_userData);
		}
	}
}
