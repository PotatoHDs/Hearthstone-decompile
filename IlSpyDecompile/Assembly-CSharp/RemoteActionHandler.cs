using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteActionHandler : MonoBehaviour
{
	private class CardAndID
	{
		private int m_ID;

		private Entity m_entity;

		private Card m_card;

		public Card card
		{
			get
			{
				return m_card;
			}
			set
			{
				if (value == m_card)
				{
					return;
				}
				if (value == null)
				{
					Clear();
					return;
				}
				m_card = value;
				m_entity = value.GetEntity();
				if (m_entity == null)
				{
					Debug.LogWarning("RemoteActionHandler--card has no entity");
					Clear();
					return;
				}
				m_ID = m_entity.GetEntityId();
				if (m_ID < 1)
				{
					Debug.LogWarning("RemoteActionHandler--invalid entity ID");
					Clear();
				}
			}
		}

		public int ID
		{
			get
			{
				return m_ID;
			}
			set
			{
				if (value == m_ID)
				{
					return;
				}
				if (value == 0)
				{
					Clear();
					return;
				}
				m_ID = value;
				m_entity = GameState.Get().GetEntity(value);
				if (m_entity == null)
				{
					Debug.LogWarning("RemoteActionHandler--no entity found for ID");
					Clear();
					return;
				}
				m_card = m_entity.GetCard();
				if (m_card == null)
				{
					Debug.LogWarning("RemoteActionHandler--entity has no card");
					Clear();
				}
			}
		}

		public Entity entity => m_entity;

		private void Clear()
		{
			m_ID = 0;
			m_entity = null;
			m_card = null;
		}
	}

	private class UserUI
	{
		public CardAndID over = new CardAndID();

		public CardAndID held = new CardAndID();

		public CardAndID origin = new CardAndID();

		public bool SameAs(UserUI compare)
		{
			if (held.card != compare.held.card)
			{
				return false;
			}
			if (over.card != compare.over.card)
			{
				return false;
			}
			if (origin.card != compare.origin.card)
			{
				return false;
			}
			return true;
		}

		public void CopyFrom(UserUI source)
		{
			held.ID = source.held.ID;
			over.ID = source.over.ID;
			origin.ID = source.origin.ID;
		}

		public bool IsSourceOrTargetNull()
		{
			if (!(over.card == null))
			{
				return origin.card == null;
			}
			return true;
		}

		public void Clear()
		{
			held.card = null;
			over.card = null;
			origin.card = null;
		}
	}

	public const string TWEEN_NAME = "RemoteActionHandler";

	private const float DRIFT_TIME = 10f;

	private const float LOW_FREQ_SEND_TIME = 0.35f;

	private const float HIGH_FREQ_SEND_TIME = 0.25f;

	private const float ENEMY_TARGET_ARROW_DESTROY_DELAY = 0.25f;

	private static RemoteActionHandler s_instance;

	private UserUI myCurrentUI = new UserUI();

	private UserUI myLastUI = new UserUI();

	private UserUI myLastUnsentUI = new UserUI();

	private UserUI enemyWantedUI = new UserUI();

	private UserUI enemyActualUI = new UserUI();

	private UserUI friendlyWantedUI = new UserUI();

	private UserUI friendlyActualUI = new UserUI();

	private float m_lastSendTime;

	private IEnumerator m_destroyEnemyTargetArrowCoroutine;

	private void Awake()
	{
		s_instance = this;
		m_lastSendTime = Time.realtimeSinceStartup;
		if (GameState.Get() == null)
		{
			Debug.LogError($"RemoteActionHandler.Awake() - GameState already Shutdown before RemoteActionHandler was loaded.");
		}
		else
		{
			GameState.Get().RegisterTurnChangedListener(OnTurnChanged);
		}
	}

	private void OnDestroy()
	{
		s_instance = null;
		StopAllCoroutines();
	}

	private void Update()
	{
		if (TargetReticleManager.Get() != null)
		{
			TargetReticleManager.Get().UpdateArrowPosition();
		}
		if (myCurrentUI.SameAs(myLastUI))
		{
			return;
		}
		if (!CanSendUI())
		{
			if (!myCurrentUI.IsSourceOrTargetNull())
			{
				myLastUnsentUI.CopyFrom(myCurrentUI);
			}
			return;
		}
		if (!myCurrentUI.SameAs(myLastUnsentUI) && myCurrentUI.IsSourceOrTargetNull() && !myLastUnsentUI.IsSourceOrTargetNull())
		{
			Network.Get().SendUserUI(myLastUnsentUI.over.ID, myLastUnsentUI.held.ID, myLastUnsentUI.origin.ID, 0, 0);
		}
		else
		{
			Network.Get().SendUserUI(myCurrentUI.over.ID, myCurrentUI.held.ID, myCurrentUI.origin.ID, 0, 0);
		}
		myLastUI.CopyFrom(myCurrentUI);
		myLastUnsentUI.Clear();
	}

	public static RemoteActionHandler Get()
	{
		return s_instance;
	}

	public Card GetOpponentHeldCard()
	{
		return enemyActualUI.held.card;
	}

	public Card GetFriendlyHoverCard()
	{
		return friendlyActualUI.over.card;
	}

	public Card GetFriendlyHeldCard()
	{
		return friendlyActualUI.held.card;
	}

	public void NotifyOpponentOfMouseOverEntity(Card card)
	{
		myCurrentUI.over.card = card;
	}

	public void NotifyOpponentOfMouseOut()
	{
		myCurrentUI.over.card = null;
	}

	public void NotifyOpponentOfTargetModeBegin(Card card)
	{
		myCurrentUI.origin.card = card;
	}

	public void NotifyOpponentOfTargetEnd()
	{
		myCurrentUI.origin.card = null;
	}

	public void NotifyOpponentOfCardPickedUp(Card card)
	{
		myCurrentUI.held.card = card;
	}

	public void NotifyOpponentOfCardDropped()
	{
		myCurrentUI.held.card = null;
	}

	public void HandleAction(Network.UserUI newData)
	{
		bool flag = false;
		if (newData.playerId.HasValue)
		{
			Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
			flag = friendlySidePlayer != null && friendlySidePlayer.GetPlayerId() == newData.playerId.Value;
		}
		if (newData.mouseInfo != null)
		{
			if (flag)
			{
				friendlyWantedUI.held.ID = newData.mouseInfo.HeldCardID;
				friendlyWantedUI.over.ID = newData.mouseInfo.OverCardID;
				friendlyWantedUI.origin.ID = newData.mouseInfo.ArrowOriginID;
			}
			else
			{
				enemyWantedUI.held.ID = newData.mouseInfo.HeldCardID;
				enemyWantedUI.over.ID = newData.mouseInfo.OverCardID;
				enemyWantedUI.origin.ID = newData.mouseInfo.ArrowOriginID;
			}
			UpdateCardOver();
			UpdateCardHeld();
			MaybeDestroyArrow();
			MaybeCreateArrow();
			UpdateTargetArrow();
		}
		else
		{
			if (newData.emoteInfo == null)
			{
				return;
			}
			EmoteType emote = (EmoteType)newData.emoteInfo.Emote;
			if (flag)
			{
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.HAS_ALTERNATE_ENEMY_EMOTE_ACTOR))
				{
					GameState.Get().GetGameEntity().PlayAlternateEnemyEmote(newData.playerId.Value, emote);
				}
				else
				{
					GameState.Get().GetFriendlySidePlayer().GetHeroCard()
						.PlayEmote(emote);
				}
			}
			else if (CanReceiveEnemyEmote(emote, newData.playerId.Value))
			{
				if (GameState.Get().GetBooleanGameOption(GameEntityOption.HAS_ALTERNATE_ENEMY_EMOTE_ACTOR))
				{
					GameState.Get().GetGameEntity().PlayAlternateEnemyEmote(newData.playerId.Value, emote);
				}
				else
				{
					GameState.Get().GetOpposingSidePlayer().GetHeroCard()
						.PlayEmote(emote);
				}
			}
		}
	}

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
		float num = realtimeSinceStartup - m_lastSendTime;
		if (IsSendingTargetingArrow() && num > 0.25f)
		{
			m_lastSendTime = realtimeSinceStartup;
			return true;
		}
		if (num < 0.35f)
		{
			return false;
		}
		m_lastSendTime = realtimeSinceStartup;
		return true;
	}

	private bool IsSendingTargetingArrow()
	{
		if (myCurrentUI.origin.card == null)
		{
			return false;
		}
		if (myCurrentUI.over.card == null)
		{
			return false;
		}
		if (myCurrentUI.over.card == myCurrentUI.origin.card)
		{
			return false;
		}
		if (myCurrentUI.origin.card != myLastUI.origin.card)
		{
			return true;
		}
		if (myCurrentUI.over.card != myLastUI.over.card)
		{
			return true;
		}
		return false;
	}

	private void UpdateCardOver()
	{
		Card card = enemyActualUI.over.card;
		Card card2 = enemyWantedUI.over.card;
		if (card != card2)
		{
			enemyActualUI.over.card = card2;
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
		Card card3 = friendlyActualUI.over.card;
		Card card4 = friendlyWantedUI.over.card;
		if (!(card3 != card4))
		{
			return;
		}
		friendlyActualUI.over.card = card4;
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
		if (!(card4 != null))
		{
			return;
		}
		ZoneHand zoneHand2 = card4.GetZone() as ZoneHand;
		if (zoneHand2 != null)
		{
			if (zoneHand2.CurrentStandIn == null)
			{
				zoneHand2.UpdateLayout(card4);
			}
		}
		else
		{
			card4.NotifyMousedOver();
		}
	}

	private void UpdateCardHeld()
	{
		Card card = enemyActualUI.held.card;
		Card card2 = enemyWantedUI.held.card;
		if (card != card2)
		{
			enemyActualUI.held.card = card2;
			if (card != null)
			{
				card.MarkAsGrabbedByEnemyActionHandler(enable: false);
			}
			if (IsCardInHand(card))
			{
				card.GetZone().UpdateLayout();
			}
			if (CanAnimateHeldCard(card2))
			{
				card2.MarkAsGrabbedByEnemyActionHandler(enable: true);
				if (SpectatorManager.Get().IsSpectatingOpposingSide())
				{
					StandUpright(isFriendlySide: false);
				}
				Hashtable args = iTween.Hash("name", "RemoteActionHandler", "position", Board.Get().FindBone("OpponentCardPlayingSpot").position, "time", 1f, "oncomplete", (Action<object>)delegate
				{
					StartDrift(isFriendlySide: false);
				}, "oncompletetarget", base.gameObject);
				iTween.MoveTo(card2.gameObject, args);
			}
		}
		if (!GameMgr.Get().IsSpectator())
		{
			return;
		}
		Card card3 = friendlyActualUI.held.card;
		Card card4 = friendlyWantedUI.held.card;
		if (!(card3 != card4))
		{
			return;
		}
		friendlyActualUI.held.card = card4;
		if (card3 != null)
		{
			card3.MarkAsGrabbedByEnemyActionHandler(enable: false);
		}
		if (IsCardInHand(card3))
		{
			card3.GetZone().UpdateLayout();
		}
		if (!CanAnimateHeldCard(card4))
		{
			return;
		}
		card4.MarkAsGrabbedByEnemyActionHandler(enable: true);
		ZoneHand zoneHand = card4.GetZone() as ZoneHand;
		Hashtable args2;
		if (zoneHand != null)
		{
			if (zoneHand.CurrentStandIn == null || zoneHand.CurrentStandIn.linkedCard == card4)
			{
				card4.NotifyMousedOut();
			}
			Vector3 cardScale = zoneHand.GetCardScale();
			args2 = iTween.Hash("scale", cardScale, "time", 0.15f, "easeType", iTween.EaseType.easeOutExpo, "name", "RemoteActionHandler");
			iTween.ScaleTo(card4.gameObject, args2);
		}
		args2 = iTween.Hash("name", "RemoteActionHandler", "position", Board.Get().FindBone("FriendlyCardPlayingSpot").position, "time", 1f, "oncomplete", (Action<object>)delegate
		{
			StartDrift(isFriendlySide: true);
		}, "oncompletetarget", base.gameObject);
		iTween.MoveTo(card4.gameObject, args2);
		SceneUtils.SetLayer(card4, GameLayer.Default);
	}

	private void StartDrift(bool isFriendlySide)
	{
		if (isFriendlySide || !GameState.Get().GetOpposingSidePlayer().IsRevealed())
		{
			StandUpright(isFriendlySide);
		}
		DriftLeftAndRight(isFriendlySide);
	}

	private void DriftLeftAndRight(bool isFriendlySide)
	{
		Card card = (isFriendlySide ? friendlyActualUI.held.card : enemyActualUI.held.card);
		if (!CanAnimateHeldCard(card))
		{
			return;
		}
		Vector3[] array;
		if (isFriendlySide)
		{
			if (!iTweenPath.paths.TryGetValue(iTweenPath.FixupPathName("driftPath1_friendly"), out var value))
			{
				Transform transform = Board.Get().FindBone("OpponentCardPlayingSpot");
				Transform obj = Board.Get().FindBone("FriendlyCardPlayingSpot");
				Vector3 vector = obj.position - transform.position;
				iTweenPath iTweenPath2 = iTweenPath.paths[iTweenPath.FixupPathName("driftPath1")];
				value = obj.gameObject.AddComponent<iTweenPath>();
				value.pathVisible = true;
				value.pathName = "driftPath1_friendly";
				value.pathColor = iTweenPath2.pathColor;
				value.nodes = new List<Vector3>(iTweenPath2.nodes);
				for (int i = 0; i < value.nodes.Count; i++)
				{
					value.nodes[i] = iTweenPath2.nodes[i] + vector;
				}
				value.enabled = false;
				value.enabled = true;
			}
			array = value.nodes.ToArray();
		}
		else
		{
			array = iTweenPath.GetPath("driftPath1");
		}
		Hashtable args = iTween.Hash("name", "RemoteActionHandler", "path", array, "time", 10f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong);
		iTween.MoveTo(card.gameObject, args);
	}

	private void StandUpright(bool isFriendlySide)
	{
		Card card = (isFriendlySide ? friendlyActualUI.held.card : enemyActualUI.held.card);
		if (CanAnimateHeldCard(card))
		{
			float num = 5f;
			if (!isFriendlySide && GameState.Get().GetOpposingSidePlayer().IsRevealed())
			{
				num = 0.3f;
			}
			Hashtable args = iTween.Hash("name", "RemoteActionHandler", "rotation", Vector3.zero, "time", num, "easetype", iTween.EaseType.easeInOutSine);
			iTween.RotateTo(card.gameObject, args);
		}
	}

	private void MaybeDestroyArrow()
	{
		if (TargetReticleManager.Get() == null || !TargetReticleManager.Get().IsActive())
		{
			return;
		}
		bool flag = GameState.Get() != null && GameState.Get().IsFriendlySidePlayerTurn();
		UserUI userUI = (flag ? friendlyWantedUI : enemyWantedUI);
		UserUI userUI2 = (flag ? friendlyActualUI : enemyActualUI);
		if (!(userUI.origin.card == userUI2.origin.card))
		{
			if (userUI2.origin.card != null && userUI2.origin.card.GetActor() != null && !userUI2.origin.card.ShouldShowImmuneVisuals())
			{
				userUI2.origin.card.GetActor().ActivateSpellDeathState(SpellType.IMMUNE);
			}
			userUI2.origin.card = null;
			if (flag)
			{
				TargetReticleManager.Get().DestroyFriendlyTargetArrow(isLocallyCanceled: false);
				return;
			}
			m_destroyEnemyTargetArrowCoroutine = DestroyEnemyTargetArrow();
			StartCoroutine(m_destroyEnemyTargetArrowCoroutine);
		}
	}

	private void MaybeCreateArrow()
	{
		if (TargetReticleManager.Get() == null || TargetReticleManager.Get().IsActive())
		{
			return;
		}
		bool flag = GameState.Get() != null && GameState.Get().IsFriendlySidePlayerTurn();
		UserUI userUI = (flag ? friendlyWantedUI : enemyWantedUI);
		UserUI userUI2 = (flag ? friendlyActualUI : enemyActualUI);
		if (userUI.origin.card == null || userUI2.over.card == null || userUI2.over.card.GetActor() == null || !userUI2.over.card.GetActor().IsShown() || userUI2.over.card == userUI.origin.card)
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
			TargetReticleManager.Get().CreateFriendlyTargetArrow(userUI2.origin.entity, userUI2.origin.entity, showDamageIndicatorText: false);
		}
		else
		{
			if (m_destroyEnemyTargetArrowCoroutine != null)
			{
				StopCoroutine(m_destroyEnemyTargetArrowCoroutine);
			}
			TargetReticleManager.Get().CreateEnemyTargetArrow(userUI2.origin.entity);
		}
		if (userUI2.origin.entity.GetRealTimeIsImmuneWhileAttacking())
		{
			userUI2.origin.card.ActivateActorSpell(SpellType.IMMUNE);
		}
		SetArrowTarget();
	}

	private IEnumerator DestroyEnemyTargetArrow()
	{
		yield return new WaitForSeconds(0.25f);
		TargetReticleManager.Get().DestroyEnemyTargetArrow();
	}

	private void UpdateTargetArrow()
	{
		if (!(TargetReticleManager.Get() == null) && TargetReticleManager.Get().IsActive())
		{
			SetArrowTarget();
		}
	}

	private void SetArrowTarget()
	{
		bool num = GameState.Get() != null && GameState.Get().IsFriendlySidePlayerTurn();
		UserUI userUI = (num ? friendlyWantedUI : enemyWantedUI);
		UserUI userUI2 = (num ? friendlyActualUI : enemyActualUI);
		if (!(userUI2.over.card == null) && !(userUI2.origin.card == null) && !(userUI2.over.card.GetActor() == null) && userUI2.over.card.GetActor().IsShown() && !(userUI2.over.card == userUI.origin.card))
		{
			Vector3 position = Camera.main.transform.position;
			Vector3 position2 = userUI2.over.card.transform.position;
			if (Physics.Raycast(new Ray(position, position2 - position), out var hitInfo, Camera.main.farClipPlane, GameLayer.DragPlane.LayerBit()))
			{
				TargetReticleManager.Get().SetRemotePlayerArrowPosition(hitInfo.point);
			}
		}
	}

	private bool IsCardInHand(Card card)
	{
		if (card == null)
		{
			return false;
		}
		if (!(card.GetZone() is ZoneHand))
		{
			return false;
		}
		if (card.GetEntity().GetZone() != TAG_ZONE.HAND)
		{
			return false;
		}
		return true;
	}

	private bool CanAnimateHeldCard(Card card)
	{
		if (!IsCardInHand(card))
		{
			return false;
		}
		string tweenName = ZoneMgr.Get().GetTweenName<ZoneHand>();
		if (iTween.HasNameNotInList(card.gameObject, "RemoteActionHandler", tweenName))
		{
			return false;
		}
		return true;
	}

	private void OnTurnChanged(int oldTurn, int newTurn, object userData)
	{
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if ((currentPlayer != null && !currentPlayer.IsLocalUser() && !GameMgr.Get().IsSpectator()) || TargetReticleManager.Get() == null)
		{
			return;
		}
		UserUI userUI;
		if (currentPlayer.IsFriendlySide())
		{
			userUI = friendlyActualUI;
			if (TargetReticleManager.Get().IsEnemyArrowActive())
			{
				TargetReticleManager.Get().DestroyEnemyTargetArrow();
			}
		}
		else
		{
			userUI = enemyActualUI;
			if (TargetReticleManager.Get().IsLocalArrowActive())
			{
				TargetReticleManager.Get().DestroyFriendlyTargetArrow(isLocallyCanceled: false);
			}
		}
		if (userUI.origin != null && userUI.origin.entity != null && userUI.origin.card != null && !userUI.origin.card.ShouldShowImmuneVisuals())
		{
			userUI.origin.card.GetActor().ActivateSpellDeathState(SpellType.IMMUNE);
		}
	}

	private bool CanReceiveEnemyEmote(EmoteType emoteType, int playerId)
	{
		if (EnemyEmoteHandler.Get() == null && !GameState.Get().GetBooleanGameOption(GameEntityOption.USES_PREMIUM_EMOTES))
		{
			return false;
		}
		if (EnemyEmoteHandler.Get() != null && EnemyEmoteHandler.Get().IsSquelched(playerId))
		{
			return false;
		}
		if (EmoteHandler.Get() == null)
		{
			return false;
		}
		return EmoteHandler.Get().IsValidEmoteTypeForOpponent(emoteType);
	}
}
