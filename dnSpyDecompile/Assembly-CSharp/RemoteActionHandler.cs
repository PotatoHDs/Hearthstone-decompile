using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000342 RID: 834
public class RemoteActionHandler : MonoBehaviour
{
	// Token: 0x0600306B RID: 12395 RVA: 0x000F8C98 File Offset: 0x000F6E98
	private void Awake()
	{
		RemoteActionHandler.s_instance = this;
		this.m_lastSendTime = Time.realtimeSinceStartup;
		if (GameState.Get() == null)
		{
			Debug.LogError(string.Format("RemoteActionHandler.Awake() - GameState already Shutdown before RemoteActionHandler was loaded.", Array.Empty<object>()));
			return;
		}
		GameState.Get().RegisterTurnChangedListener(new GameState.TurnChangedCallback(this.OnTurnChanged));
	}

	// Token: 0x0600306C RID: 12396 RVA: 0x000F8CE9 File Offset: 0x000F6EE9
	private void OnDestroy()
	{
		RemoteActionHandler.s_instance = null;
		base.StopAllCoroutines();
	}

	// Token: 0x0600306D RID: 12397 RVA: 0x000F8CF8 File Offset: 0x000F6EF8
	private void Update()
	{
		if (TargetReticleManager.Get() != null)
		{
			TargetReticleManager.Get().UpdateArrowPosition();
		}
		if (this.myCurrentUI.SameAs(this.myLastUI))
		{
			return;
		}
		if (!this.CanSendUI())
		{
			if (!this.myCurrentUI.IsSourceOrTargetNull())
			{
				this.myLastUnsentUI.CopyFrom(this.myCurrentUI);
			}
			return;
		}
		if (!this.myCurrentUI.SameAs(this.myLastUnsentUI) && this.myCurrentUI.IsSourceOrTargetNull() && !this.myLastUnsentUI.IsSourceOrTargetNull())
		{
			Network.Get().SendUserUI(this.myLastUnsentUI.over.ID, this.myLastUnsentUI.held.ID, this.myLastUnsentUI.origin.ID, 0, 0);
		}
		else
		{
			Network.Get().SendUserUI(this.myCurrentUI.over.ID, this.myCurrentUI.held.ID, this.myCurrentUI.origin.ID, 0, 0);
		}
		this.myLastUI.CopyFrom(this.myCurrentUI);
		this.myLastUnsentUI.Clear();
	}

	// Token: 0x0600306E RID: 12398 RVA: 0x000F8E1A File Offset: 0x000F701A
	public static RemoteActionHandler Get()
	{
		return RemoteActionHandler.s_instance;
	}

	// Token: 0x0600306F RID: 12399 RVA: 0x000F8E21 File Offset: 0x000F7021
	public Card GetOpponentHeldCard()
	{
		return this.enemyActualUI.held.card;
	}

	// Token: 0x06003070 RID: 12400 RVA: 0x000F8E33 File Offset: 0x000F7033
	public Card GetFriendlyHoverCard()
	{
		return this.friendlyActualUI.over.card;
	}

	// Token: 0x06003071 RID: 12401 RVA: 0x000F8E45 File Offset: 0x000F7045
	public Card GetFriendlyHeldCard()
	{
		return this.friendlyActualUI.held.card;
	}

	// Token: 0x06003072 RID: 12402 RVA: 0x000F8E57 File Offset: 0x000F7057
	public void NotifyOpponentOfMouseOverEntity(Card card)
	{
		this.myCurrentUI.over.card = card;
	}

	// Token: 0x06003073 RID: 12403 RVA: 0x000F8E6A File Offset: 0x000F706A
	public void NotifyOpponentOfMouseOut()
	{
		this.myCurrentUI.over.card = null;
	}

	// Token: 0x06003074 RID: 12404 RVA: 0x000F8E7D File Offset: 0x000F707D
	public void NotifyOpponentOfTargetModeBegin(Card card)
	{
		this.myCurrentUI.origin.card = card;
	}

	// Token: 0x06003075 RID: 12405 RVA: 0x000F8E90 File Offset: 0x000F7090
	public void NotifyOpponentOfTargetEnd()
	{
		this.myCurrentUI.origin.card = null;
	}

	// Token: 0x06003076 RID: 12406 RVA: 0x000F8EA3 File Offset: 0x000F70A3
	public void NotifyOpponentOfCardPickedUp(Card card)
	{
		this.myCurrentUI.held.card = card;
	}

	// Token: 0x06003077 RID: 12407 RVA: 0x000F8EB6 File Offset: 0x000F70B6
	public void NotifyOpponentOfCardDropped()
	{
		this.myCurrentUI.held.card = null;
	}

	// Token: 0x06003078 RID: 12408 RVA: 0x000F8ECC File Offset: 0x000F70CC
	public void HandleAction(Network.UserUI newData)
	{
		bool flag = false;
		if (newData.playerId != null)
		{
			Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
			flag = (friendlySidePlayer != null && friendlySidePlayer.GetPlayerId() == newData.playerId.Value);
		}
		if (newData.mouseInfo != null)
		{
			if (flag)
			{
				this.friendlyWantedUI.held.ID = newData.mouseInfo.HeldCardID;
				this.friendlyWantedUI.over.ID = newData.mouseInfo.OverCardID;
				this.friendlyWantedUI.origin.ID = newData.mouseInfo.ArrowOriginID;
			}
			else
			{
				this.enemyWantedUI.held.ID = newData.mouseInfo.HeldCardID;
				this.enemyWantedUI.over.ID = newData.mouseInfo.OverCardID;
				this.enemyWantedUI.origin.ID = newData.mouseInfo.ArrowOriginID;
			}
			this.UpdateCardOver();
			this.UpdateCardHeld();
			this.MaybeDestroyArrow();
			this.MaybeCreateArrow();
			this.UpdateTargetArrow();
			return;
		}
		if (newData.emoteInfo != null)
		{
			EmoteType emote = (EmoteType)newData.emoteInfo.Emote;
			if (flag)
			{
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.HAS_ALTERNATE_ENEMY_EMOTE_ACTOR))
				{
					GameState.Get().GetGameEntity().PlayAlternateEnemyEmote(newData.playerId.Value, emote);
					return;
				}
				GameState.Get().GetFriendlySidePlayer().GetHeroCard().PlayEmote(emote);
				return;
			}
			else if (this.CanReceiveEnemyEmote(emote, newData.playerId.Value))
			{
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.HAS_ALTERNATE_ENEMY_EMOTE_ACTOR))
				{
					GameState.Get().GetGameEntity().PlayAlternateEnemyEmote(newData.playerId.Value, emote);
					return;
				}
				GameState.Get().GetOpposingSidePlayer().GetHeroCard().PlayEmote(emote);
			}
		}
	}

	// Token: 0x06003079 RID: 12409 RVA: 0x000F9090 File Offset: 0x000F7290
	private bool CanSendUI()
	{
		if (GameMgr.Get() == null)
		{
			return false;
		}
		if (!InputManager.Get().PermitDecisionMakingInput())
		{
			return false;
		}
		if (GameMgr.Get().IsAI() && !SpectatorManager.Get().MyGameHasSpectators())
		{
			return false;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - this.m_lastSendTime;
		if (this.IsSendingTargetingArrow() && num > 0.25f)
		{
			this.m_lastSendTime = realtimeSinceStartup;
			return true;
		}
		if (num < 0.35f)
		{
			return false;
		}
		this.m_lastSendTime = realtimeSinceStartup;
		return true;
	}

	// Token: 0x0600307A RID: 12410 RVA: 0x000F9108 File Offset: 0x000F7308
	private bool IsSendingTargetingArrow()
	{
		return !(this.myCurrentUI.origin.card == null) && !(this.myCurrentUI.over.card == null) && !(this.myCurrentUI.over.card == this.myCurrentUI.origin.card) && (this.myCurrentUI.origin.card != this.myLastUI.origin.card || this.myCurrentUI.over.card != this.myLastUI.over.card);
	}

	// Token: 0x0600307B RID: 12411 RVA: 0x000F91C8 File Offset: 0x000F73C8
	private void UpdateCardOver()
	{
		Card card = this.enemyActualUI.over.card;
		Card card2 = this.enemyWantedUI.over.card;
		if (card != card2)
		{
			this.enemyActualUI.over.card = card2;
			if (!GameState.Get().GetGameEntity().HasTag(GAME_TAG.REVEAL_CHOICES))
			{
				if (card != null)
				{
					card.NotifyOpponentMousedOffThisCard();
				}
				if (card2 != null)
				{
					card2.NotifyOpponentMousedOverThisCard();
				}
			}
			ZoneMgr.Get().FindZoneOfType<ZoneHand>(Player.Side.OPPOSING).UpdateLayout(card2);
		}
		if (!GameMgr.Get().IsSpectator())
		{
			return;
		}
		Card card3 = this.friendlyActualUI.over.card;
		Card card4 = this.friendlyWantedUI.over.card;
		if (card3 != card4)
		{
			this.friendlyActualUI.over.card = card4;
			if (card3 != null)
			{
				ZoneHand zoneHand = card3.GetZone() as ZoneHand;
				if (zoneHand != null)
				{
					if (zoneHand.CurrentStandIn == null)
					{
						zoneHand.UpdateLayout(null);
					}
				}
				else
				{
					card3.NotifyMousedOut();
				}
			}
			if (card4 != null)
			{
				ZoneHand zoneHand2 = card4.GetZone() as ZoneHand;
				if (zoneHand2 != null)
				{
					if (zoneHand2.CurrentStandIn == null)
					{
						zoneHand2.UpdateLayout(card4);
						return;
					}
				}
				else
				{
					card4.NotifyMousedOver();
				}
			}
		}
	}

	// Token: 0x0600307C RID: 12412 RVA: 0x000F9320 File Offset: 0x000F7520
	private void UpdateCardHeld()
	{
		Card card = this.enemyActualUI.held.card;
		Card card2 = this.enemyWantedUI.held.card;
		if (card != card2)
		{
			this.enemyActualUI.held.card = card2;
			if (card != null)
			{
				card.MarkAsGrabbedByEnemyActionHandler(false);
			}
			if (this.IsCardInHand(card))
			{
				card.GetZone().UpdateLayout();
			}
			if (this.CanAnimateHeldCard(card2))
			{
				card2.MarkAsGrabbedByEnemyActionHandler(true);
				if (SpectatorManager.Get().IsSpectatingOpposingSide())
				{
					this.StandUpright(false);
				}
				Hashtable args = iTween.Hash(new object[]
				{
					"name",
					"RemoteActionHandler",
					"position",
					Board.Get().FindBone("OpponentCardPlayingSpot").position,
					"time",
					1f,
					"oncomplete",
					new Action<object>(delegate(object o)
					{
						this.StartDrift(false);
					}),
					"oncompletetarget",
					base.gameObject
				});
				iTween.MoveTo(card2.gameObject, args);
			}
		}
		if (!GameMgr.Get().IsSpectator())
		{
			return;
		}
		Card card3 = this.friendlyActualUI.held.card;
		Card card4 = this.friendlyWantedUI.held.card;
		if (card3 != card4)
		{
			this.friendlyActualUI.held.card = card4;
			if (card3 != null)
			{
				card3.MarkAsGrabbedByEnemyActionHandler(false);
			}
			if (this.IsCardInHand(card3))
			{
				card3.GetZone().UpdateLayout();
			}
			if (this.CanAnimateHeldCard(card4))
			{
				card4.MarkAsGrabbedByEnemyActionHandler(true);
				ZoneHand zoneHand = card4.GetZone() as ZoneHand;
				Hashtable args2;
				if (zoneHand != null)
				{
					if (zoneHand.CurrentStandIn == null || zoneHand.CurrentStandIn.linkedCard == card4)
					{
						card4.NotifyMousedOut();
					}
					Vector3 cardScale = zoneHand.GetCardScale();
					args2 = iTween.Hash(new object[]
					{
						"scale",
						cardScale,
						"time",
						0.15f,
						"easeType",
						iTween.EaseType.easeOutExpo,
						"name",
						"RemoteActionHandler"
					});
					iTween.ScaleTo(card4.gameObject, args2);
				}
				args2 = iTween.Hash(new object[]
				{
					"name",
					"RemoteActionHandler",
					"position",
					Board.Get().FindBone("FriendlyCardPlayingSpot").position,
					"time",
					1f,
					"oncomplete",
					new Action<object>(delegate(object o)
					{
						this.StartDrift(true);
					}),
					"oncompletetarget",
					base.gameObject
				});
				iTween.MoveTo(card4.gameObject, args2);
				SceneUtils.SetLayer(card4, GameLayer.Default);
			}
		}
	}

	// Token: 0x0600307D RID: 12413 RVA: 0x000F9609 File Offset: 0x000F7809
	private void StartDrift(bool isFriendlySide)
	{
		if (isFriendlySide || !GameState.Get().GetOpposingSidePlayer().IsRevealed())
		{
			this.StandUpright(isFriendlySide);
		}
		this.DriftLeftAndRight(isFriendlySide);
	}

	// Token: 0x0600307E RID: 12414 RVA: 0x000F9630 File Offset: 0x000F7830
	private void DriftLeftAndRight(bool isFriendlySide)
	{
		Card card = isFriendlySide ? this.friendlyActualUI.held.card : this.enemyActualUI.held.card;
		if (!this.CanAnimateHeldCard(card))
		{
			return;
		}
		Vector3[] array;
		if (isFriendlySide)
		{
			iTweenPath iTweenPath;
			if (!iTweenPath.paths.TryGetValue(iTweenPath.FixupPathName("driftPath1_friendly"), out iTweenPath))
			{
				Transform transform = Board.Get().FindBone("OpponentCardPlayingSpot");
				Transform transform2 = Board.Get().FindBone("FriendlyCardPlayingSpot");
				Vector3 b = transform2.position - transform.position;
				iTweenPath iTweenPath2 = iTweenPath.paths[iTweenPath.FixupPathName("driftPath1")];
				iTweenPath = transform2.gameObject.AddComponent<iTweenPath>();
				iTweenPath.pathVisible = true;
				iTweenPath.pathName = "driftPath1_friendly";
				iTweenPath.pathColor = iTweenPath2.pathColor;
				iTweenPath.nodes = new List<Vector3>(iTweenPath2.nodes);
				for (int i = 0; i < iTweenPath.nodes.Count; i++)
				{
					iTweenPath.nodes[i] = iTweenPath2.nodes[i] + b;
				}
				iTweenPath.enabled = false;
				iTweenPath.enabled = true;
			}
			array = iTweenPath.nodes.ToArray();
		}
		else
		{
			array = iTweenPath.GetPath("driftPath1");
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"name",
			"RemoteActionHandler",
			"path",
			array,
			"time",
			10f,
			"easetype",
			iTween.EaseType.linear,
			"looptype",
			iTween.LoopType.pingPong
		});
		iTween.MoveTo(card.gameObject, args);
	}

	// Token: 0x0600307F RID: 12415 RVA: 0x000F97E8 File Offset: 0x000F79E8
	private void StandUpright(bool isFriendlySide)
	{
		Card card = isFriendlySide ? this.friendlyActualUI.held.card : this.enemyActualUI.held.card;
		if (!this.CanAnimateHeldCard(card))
		{
			return;
		}
		float num = 5f;
		if (!isFriendlySide && GameState.Get().GetOpposingSidePlayer().IsRevealed())
		{
			num = 0.3f;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"name",
			"RemoteActionHandler",
			"rotation",
			Vector3.zero,
			"time",
			num,
			"easetype",
			iTween.EaseType.easeInOutSine
		});
		iTween.RotateTo(card.gameObject, args);
	}

	// Token: 0x06003080 RID: 12416 RVA: 0x000F98A8 File Offset: 0x000F7AA8
	private void MaybeDestroyArrow()
	{
		if (TargetReticleManager.Get() == null || !TargetReticleManager.Get().IsActive())
		{
			return;
		}
		bool flag = GameState.Get() != null && GameState.Get().IsFriendlySidePlayerTurn();
		RemoteActionHandler.UserUI userUI = flag ? this.friendlyWantedUI : this.enemyWantedUI;
		RemoteActionHandler.UserUI userUI2 = flag ? this.friendlyActualUI : this.enemyActualUI;
		if (userUI.origin.card == userUI2.origin.card)
		{
			return;
		}
		if (userUI2.origin.card != null && userUI2.origin.card.GetActor() != null && !userUI2.origin.card.ShouldShowImmuneVisuals())
		{
			userUI2.origin.card.GetActor().ActivateSpellDeathState(SpellType.IMMUNE);
		}
		userUI2.origin.card = null;
		if (flag)
		{
			TargetReticleManager.Get().DestroyFriendlyTargetArrow(false);
			return;
		}
		this.m_destroyEnemyTargetArrowCoroutine = this.DestroyEnemyTargetArrow();
		base.StartCoroutine(this.m_destroyEnemyTargetArrowCoroutine);
	}

	// Token: 0x06003081 RID: 12417 RVA: 0x000F99B0 File Offset: 0x000F7BB0
	private void MaybeCreateArrow()
	{
		if (TargetReticleManager.Get() == null || TargetReticleManager.Get().IsActive())
		{
			return;
		}
		bool flag = GameState.Get() != null && GameState.Get().IsFriendlySidePlayerTurn();
		RemoteActionHandler.UserUI userUI = flag ? this.friendlyWantedUI : this.enemyWantedUI;
		RemoteActionHandler.UserUI userUI2 = flag ? this.friendlyActualUI : this.enemyActualUI;
		if (userUI.origin.card == null)
		{
			return;
		}
		if (userUI2.over.card == null)
		{
			return;
		}
		if (userUI2.over.card.GetActor() == null)
		{
			return;
		}
		if (!userUI2.over.card.GetActor().IsShown())
		{
			return;
		}
		if (userUI2.over.card == userUI.origin.card)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer == null || currentPlayer.IsLocalUser())
		{
			return;
		}
		userUI2.origin.card = userUI.origin.card;
		if (flag)
		{
			TargetReticleManager.Get().CreateFriendlyTargetArrow(userUI2.origin.entity, userUI2.origin.entity, false, true, null, false);
		}
		else
		{
			if (this.m_destroyEnemyTargetArrowCoroutine != null)
			{
				base.StopCoroutine(this.m_destroyEnemyTargetArrowCoroutine);
			}
			TargetReticleManager.Get().CreateEnemyTargetArrow(userUI2.origin.entity);
		}
		if (userUI2.origin.entity.GetRealTimeIsImmuneWhileAttacking())
		{
			userUI2.origin.card.ActivateActorSpell(SpellType.IMMUNE);
		}
		this.SetArrowTarget();
	}

	// Token: 0x06003082 RID: 12418 RVA: 0x000F9B31 File Offset: 0x000F7D31
	private IEnumerator DestroyEnemyTargetArrow()
	{
		yield return new WaitForSeconds(0.25f);
		TargetReticleManager.Get().DestroyEnemyTargetArrow();
		yield break;
	}

	// Token: 0x06003083 RID: 12419 RVA: 0x000F9B39 File Offset: 0x000F7D39
	private void UpdateTargetArrow()
	{
		if (TargetReticleManager.Get() == null || !TargetReticleManager.Get().IsActive())
		{
			return;
		}
		this.SetArrowTarget();
	}

	// Token: 0x06003084 RID: 12420 RVA: 0x000F9B5C File Offset: 0x000F7D5C
	private void SetArrowTarget()
	{
		bool flag = GameState.Get() != null && GameState.Get().IsFriendlySidePlayerTurn();
		RemoteActionHandler.UserUI userUI = flag ? this.friendlyWantedUI : this.enemyWantedUI;
		RemoteActionHandler.UserUI userUI2 = flag ? this.friendlyActualUI : this.enemyActualUI;
		if (userUI2.over.card == null)
		{
			return;
		}
		if (userUI2.origin.card == null)
		{
			return;
		}
		if (userUI2.over.card.GetActor() == null)
		{
			return;
		}
		if (!userUI2.over.card.GetActor().IsShown())
		{
			return;
		}
		if (userUI2.over.card == userUI.origin.card)
		{
			return;
		}
		Vector3 position = Camera.main.transform.position;
		Vector3 position2 = userUI2.over.card.transform.position;
		RaycastHit raycastHit;
		if (!Physics.Raycast(new Ray(position, position2 - position), out raycastHit, Camera.main.farClipPlane, GameLayer.DragPlane.LayerBit()))
		{
			return;
		}
		TargetReticleManager.Get().SetRemotePlayerArrowPosition(raycastHit.point);
	}

	// Token: 0x06003085 RID: 12421 RVA: 0x000F9C76 File Offset: 0x000F7E76
	private bool IsCardInHand(Card card)
	{
		return !(card == null) && card.GetZone() is ZoneHand && card.GetEntity().GetZone() == TAG_ZONE.HAND;
	}

	// Token: 0x06003086 RID: 12422 RVA: 0x000F9CA4 File Offset: 0x000F7EA4
	private bool CanAnimateHeldCard(Card card)
	{
		if (!this.IsCardInHand(card))
		{
			return false;
		}
		string tweenName = ZoneMgr.Get().GetTweenName<ZoneHand>();
		return !iTween.HasNameNotInList(card.gameObject, new string[]
		{
			"RemoteActionHandler",
			tweenName
		});
	}

	// Token: 0x06003087 RID: 12423 RVA: 0x000F9CEC File Offset: 0x000F7EEC
	private void OnTurnChanged(int oldTurn, int newTurn, object userData)
	{
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer != null && !currentPlayer.IsLocalUser() && !GameMgr.Get().IsSpectator())
		{
			return;
		}
		if (TargetReticleManager.Get() == null)
		{
			return;
		}
		RemoteActionHandler.UserUI userUI;
		if (currentPlayer.IsFriendlySide())
		{
			userUI = this.friendlyActualUI;
			if (TargetReticleManager.Get().IsEnemyArrowActive())
			{
				TargetReticleManager.Get().DestroyEnemyTargetArrow();
			}
		}
		else
		{
			userUI = this.enemyActualUI;
			if (TargetReticleManager.Get().IsLocalArrowActive())
			{
				TargetReticleManager.Get().DestroyFriendlyTargetArrow(false);
			}
		}
		if (userUI.origin != null && userUI.origin.entity != null && userUI.origin.card != null && !userUI.origin.card.ShouldShowImmuneVisuals())
		{
			userUI.origin.card.GetActor().ActivateSpellDeathState(SpellType.IMMUNE);
		}
	}

	// Token: 0x06003088 RID: 12424 RVA: 0x000F9DC0 File Offset: 0x000F7FC0
	private bool CanReceiveEnemyEmote(EmoteType emoteType, int playerId)
	{
		return (!(EnemyEmoteHandler.Get() == null) || GameState.Get().GetBooleanGameOption(GameEntityOption.USES_PREMIUM_EMOTES)) && (!(EnemyEmoteHandler.Get() != null) || !EnemyEmoteHandler.Get().IsSquelched(playerId)) && !(EmoteHandler.Get() == null) && EmoteHandler.Get().IsValidEmoteTypeForOpponent(emoteType);
	}

	// Token: 0x04001AE8 RID: 6888
	public const string TWEEN_NAME = "RemoteActionHandler";

	// Token: 0x04001AE9 RID: 6889
	private const float DRIFT_TIME = 10f;

	// Token: 0x04001AEA RID: 6890
	private const float LOW_FREQ_SEND_TIME = 0.35f;

	// Token: 0x04001AEB RID: 6891
	private const float HIGH_FREQ_SEND_TIME = 0.25f;

	// Token: 0x04001AEC RID: 6892
	private const float ENEMY_TARGET_ARROW_DESTROY_DELAY = 0.25f;

	// Token: 0x04001AED RID: 6893
	private static RemoteActionHandler s_instance;

	// Token: 0x04001AEE RID: 6894
	private RemoteActionHandler.UserUI myCurrentUI = new RemoteActionHandler.UserUI();

	// Token: 0x04001AEF RID: 6895
	private RemoteActionHandler.UserUI myLastUI = new RemoteActionHandler.UserUI();

	// Token: 0x04001AF0 RID: 6896
	private RemoteActionHandler.UserUI myLastUnsentUI = new RemoteActionHandler.UserUI();

	// Token: 0x04001AF1 RID: 6897
	private RemoteActionHandler.UserUI enemyWantedUI = new RemoteActionHandler.UserUI();

	// Token: 0x04001AF2 RID: 6898
	private RemoteActionHandler.UserUI enemyActualUI = new RemoteActionHandler.UserUI();

	// Token: 0x04001AF3 RID: 6899
	private RemoteActionHandler.UserUI friendlyWantedUI = new RemoteActionHandler.UserUI();

	// Token: 0x04001AF4 RID: 6900
	private RemoteActionHandler.UserUI friendlyActualUI = new RemoteActionHandler.UserUI();

	// Token: 0x04001AF5 RID: 6901
	private float m_lastSendTime;

	// Token: 0x04001AF6 RID: 6902
	private IEnumerator m_destroyEnemyTargetArrowCoroutine;

	// Token: 0x020016E7 RID: 5863
	private class CardAndID
	{
		// Token: 0x1700149F RID: 5279
		// (get) Token: 0x0600E5FF RID: 58879 RVA: 0x0040F8EA File Offset: 0x0040DAEA
		// (set) Token: 0x0600E600 RID: 58880 RVA: 0x0040F8F4 File Offset: 0x0040DAF4
		public Card card
		{
			get
			{
				return this.m_card;
			}
			set
			{
				if (value == this.m_card)
				{
					return;
				}
				if (value == null)
				{
					this.Clear();
					return;
				}
				this.m_card = value;
				this.m_entity = value.GetEntity();
				if (this.m_entity == null)
				{
					Debug.LogWarning("RemoteActionHandler--card has no entity");
					this.Clear();
					return;
				}
				this.m_ID = this.m_entity.GetEntityId();
				if (this.m_ID < 1)
				{
					Debug.LogWarning("RemoteActionHandler--invalid entity ID");
					this.Clear();
				}
			}
		}

		// Token: 0x170014A0 RID: 5280
		// (get) Token: 0x0600E601 RID: 58881 RVA: 0x0040F976 File Offset: 0x0040DB76
		// (set) Token: 0x0600E602 RID: 58882 RVA: 0x0040F980 File Offset: 0x0040DB80
		public int ID
		{
			get
			{
				return this.m_ID;
			}
			set
			{
				if (value == this.m_ID)
				{
					return;
				}
				if (value == 0)
				{
					this.Clear();
					return;
				}
				this.m_ID = value;
				this.m_entity = GameState.Get().GetEntity(value);
				if (this.m_entity == null)
				{
					Debug.LogWarning("RemoteActionHandler--no entity found for ID");
					this.Clear();
					return;
				}
				this.m_card = this.m_entity.GetCard();
				if (this.m_card == null)
				{
					Debug.LogWarning("RemoteActionHandler--entity has no card");
					this.Clear();
				}
			}
		}

		// Token: 0x170014A1 RID: 5281
		// (get) Token: 0x0600E603 RID: 58883 RVA: 0x0040FA01 File Offset: 0x0040DC01
		public Entity entity
		{
			get
			{
				return this.m_entity;
			}
		}

		// Token: 0x0600E604 RID: 58884 RVA: 0x0040FA09 File Offset: 0x0040DC09
		private void Clear()
		{
			this.m_ID = 0;
			this.m_entity = null;
			this.m_card = null;
		}

		// Token: 0x0400B2BD RID: 45757
		private int m_ID;

		// Token: 0x0400B2BE RID: 45758
		private Entity m_entity;

		// Token: 0x0400B2BF RID: 45759
		private Card m_card;
	}

	// Token: 0x020016E8 RID: 5864
	private class UserUI
	{
		// Token: 0x0600E606 RID: 58886 RVA: 0x0040FA20 File Offset: 0x0040DC20
		public bool SameAs(RemoteActionHandler.UserUI compare)
		{
			return !(this.held.card != compare.held.card) && !(this.over.card != compare.over.card) && !(this.origin.card != compare.origin.card);
		}

		// Token: 0x0600E607 RID: 58887 RVA: 0x0040FA8C File Offset: 0x0040DC8C
		public void CopyFrom(RemoteActionHandler.UserUI source)
		{
			this.held.ID = source.held.ID;
			this.over.ID = source.over.ID;
			this.origin.ID = source.origin.ID;
		}

		// Token: 0x0600E608 RID: 58888 RVA: 0x0040FADB File Offset: 0x0040DCDB
		public bool IsSourceOrTargetNull()
		{
			return this.over.card == null || this.origin.card == null;
		}

		// Token: 0x0600E609 RID: 58889 RVA: 0x0040FB03 File Offset: 0x0040DD03
		public void Clear()
		{
			this.held.card = null;
			this.over.card = null;
			this.origin.card = null;
		}

		// Token: 0x0400B2C0 RID: 45760
		public RemoteActionHandler.CardAndID over = new RemoteActionHandler.CardAndID();

		// Token: 0x0400B2C1 RID: 45761
		public RemoteActionHandler.CardAndID held = new RemoteActionHandler.CardAndID();

		// Token: 0x0400B2C2 RID: 45762
		public RemoteActionHandler.CardAndID origin = new RemoteActionHandler.CardAndID();
	}
}
