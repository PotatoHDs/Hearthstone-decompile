using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneHand : Zone
{
	public GameObject m_iPhoneCardPosition;

	public GameObject m_leftArrow;

	public GameObject m_rightArrow;

	public GameObject m_manaGemPosition;

	public ManaCrystalMgr m_manaGemMgr;

	public GameObject m_playCardButton;

	public GameObject m_iPhonePreviewBone;

	public Float_MobileOverride m_SelectCardOffsetZ;

	public Float_MobileOverride m_SelectCardScale;

	public Float_MobileOverride m_TouchDragResistanceFactorY;

	public TwinspellHoldSpell m_TwinspellHoldSpell;

	public Vector3 m_enlargedHandPosition;

	public Vector3 m_enlargedHandScale;

	public Vector3 m_enlargedHandCardScale;

	public float m_enlargedHandDefaultCardSpacing;

	public float m_enlargedHandCardMinX;

	public float m_enlargedHandCardMaxX;

	public float m_heroWidthInHand;

	public float m_handHidingDistance;

	public GameObject m_heroHitbox;

	public const float MOUSE_OVER_SCALE = 1.5f;

	public const float HAND_SCALE = 0.62f;

	public const float HAND_SCALE_Y = 0.225f;

	public const float HAND_SCALE_OPPONENT = 0.682f;

	public const float HAND_SCALE_OPPONENT_Y = 0.225f;

	private const float ANGLE_OF_CARDS = 40f;

	private const float DEFAULT_ANIMATE_TIME = 0.35f;

	private const float DRIFT_AMOUNT = 0.08f;

	private const float Z_ROTATION_ON_LEFT = 354.5f;

	private const float Z_ROTATION_ON_RIGHT = 3f;

	private const float RESISTANCE_BASE = 10f;

	private Card m_lastMousedOverCard;

	private float m_maxWidth;

	private bool m_doNotUpdateLayout = true;

	private Vector3 centerOfHand;

	private bool enemyHand;

	private bool m_handEnlarged;

	private Vector3 m_startingPosition;

	private Vector3 m_startingScale;

	private bool m_handMoving;

	private bool m_targetingMode;

	private int m_touchedSlot;

	private CardStandIn m_hiddenStandIn;

	private TwinspellHoldSpell m_twinspellHoldSpellInstance;

	private int m_playingTwinspellEntityId = -1;

	private int m_reservedSlot = -1;

	private List<CardStandIn> standIns;

	private bool m_flipHandCards;

	public CardStandIn CurrentStandIn
	{
		get
		{
			if (m_lastMousedOverCard == null)
			{
				return null;
			}
			return GetStandIn(m_lastMousedOverCard);
		}
	}

	private void Awake()
	{
		enemyHand = m_Side == Player.Side.OPPOSING;
		m_startingPosition = base.gameObject.transform.localPosition;
		m_startingScale = base.gameObject.transform.localScale;
		UpdateCenterAndWidth();
	}

	private void Start()
	{
		GameState.Get().RegisterCantPlayListener(OnCantPlay);
	}

	public Card GetLastMousedOverCard()
	{
		return m_lastMousedOverCard;
	}

	public bool IsHandScrunched()
	{
		int num = GetVisualCardCount();
		if (m_handEnlarged && num > 3)
		{
			return true;
		}
		float defaultCardSpacing = GetDefaultCardSpacing();
		if (!enemyHand)
		{
			num -= TurnStartManager.Get().GetNumCardsToDraw();
		}
		if (defaultCardSpacing * (float)num > MaxHandWidth())
		{
			return true;
		}
		return false;
	}

	public void SetDoNotUpdateLayout(bool enable)
	{
		m_doNotUpdateLayout = enable;
	}

	public bool IsDoNotUpdateLayout()
	{
		return m_doNotUpdateLayout;
	}

	public override void OnSpellPowerEntityEnteredPlay(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		foreach (Card card in m_cards)
		{
			if (card.CanPlaySpellPowerHint(spellSchool))
			{
				Spell actorSpell = card.GetActorSpell(SpellType.SPELL_POWER_HINT_BURST);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
			}
		}
	}

	public override void OnSpellPowerEntityMousedOver(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		if (TargetReticleManager.Get().IsActive())
		{
			return;
		}
		foreach (Card card in m_cards)
		{
			if (card.CanPlaySpellPowerHint(spellSchool))
			{
				Spell actorSpell = card.GetActorSpell(SpellType.SPELL_POWER_HINT_BURST);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
				Spell actorSpell2 = card.GetActorSpell(SpellType.SPELL_POWER_HINT_IDLE);
				if (actorSpell2 != null)
				{
					actorSpell2.ActivateState(SpellStateType.BIRTH);
				}
			}
		}
	}

	public override void OnSpellPowerEntityMousedOut(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		foreach (Card card in m_cards)
		{
			Spell actorSpell = card.GetActorSpell(SpellType.SPELL_POWER_HINT_IDLE);
			if (!(actorSpell == null) && actorSpell.IsActive())
			{
				actorSpell.ActivateState(SpellStateType.DEATH);
			}
		}
	}

	public float GetDefaultCardSpacing()
	{
		if ((bool)UniversalInputManager.UsePhoneUI && m_handEnlarged)
		{
			return m_enlargedHandDefaultCardSpacing;
		}
		return 1.270804f;
	}

	public int GetVisualCardCount()
	{
		if (m_reservedSlot == -1)
		{
			return m_cards.Count;
		}
		return m_cards.Count + 1;
	}

	public void ReserveCardSlot(int slot)
	{
		m_reservedSlot = slot;
	}

	public void SortWithSpotForReservedCard(int slot)
	{
		m_reservedSlot = slot;
		UpdateLayout();
	}

	public void ClearReservedCard()
	{
		SortWithSpotForReservedCard(-1);
	}

	public override void UpdateLayout()
	{
		if (!GameState.Get().IsMulliganManagerActive() && !enemyHand)
		{
			BlowUpOldStandins();
			for (int i = 0; i < m_cards.Count; i++)
			{
				Card card = m_cards[i];
				CreateCardStandIn(card);
			}
		}
		UpdateLayout(null, forced: true, -1);
	}

	public void ForceStandInUpdate()
	{
		BlowUpOldStandins();
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			CreateCardStandIn(card);
		}
	}

	public void UpdateLayout(Card cardMousedOver)
	{
		UpdateLayout(cardMousedOver, forced: false, -1);
	}

	public void UpdateLayout(Card cardMousedOver, bool forced)
	{
		UpdateLayout(cardMousedOver, forced, -1);
	}

	public void UpdateLayout(Card cardMousedOver, bool forced, int overrideCardCount)
	{
		m_updatingLayout++;
		if (IsBlockingLayout())
		{
			UpdateLayoutFinished();
			return;
		}
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			if (!card.IsDoNotSort() && card.GetTransitionStyle() != ZoneTransitionStyle.VERY_SLOW && !IsCardNotInEnemyHandAnymore(card) && !card.HasBeenGrabbedByEnemyActionHandler())
			{
				Spell bestSummonSpell = card.GetBestSummonSpell();
				if (!(bestSummonSpell != null) || !bestSummonSpell.IsActive())
				{
					card.ShowCard();
				}
			}
		}
		if (m_doNotUpdateLayout)
		{
			UpdateLayoutFinished();
			return;
		}
		if (cardMousedOver != null && GetCardSlot(cardMousedOver) < 0)
		{
			cardMousedOver = null;
		}
		if (!forced && cardMousedOver == m_lastMousedOverCard)
		{
			m_updatingLayout--;
			UpdateKeywordPanelsPosition(cardMousedOver);
		}
		else
		{
			m_cards.Sort(Zone.CardSortComparison);
			UpdateLayoutImpl(cardMousedOver, overrideCardCount);
		}
	}

	public void HideCards()
	{
		foreach (Card card in m_cards)
		{
			card.GetActor().gameObject.SetActive(value: false);
		}
	}

	public void ShowCards()
	{
		foreach (Card card in m_cards)
		{
			card.GetActor().gameObject.SetActive(value: true);
		}
	}

	public float GetCardWidth()
	{
		float cardSpacing = GetCardSpacing();
		Vector3 position = centerOfHand;
		position.x -= cardSpacing / 2f;
		Vector3 position2 = centerOfHand;
		position2.x += cardSpacing / 2f;
		Vector3 vector = Camera.main.WorldToScreenPoint(position);
		return Camera.main.WorldToScreenPoint(position2).x - vector.x;
	}

	public bool TouchReceived()
	{
		if (!UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast.LayerBit(), out var hitInfo))
		{
			m_touchedSlot = -1;
		}
		CardStandIn cardStandIn = SceneUtils.FindComponentInParents<CardStandIn>(hitInfo.transform);
		if (cardStandIn != null)
		{
			m_touchedSlot = GetCardSlot(cardStandIn.linkedCard);
			return true;
		}
		m_touchedSlot = -1;
		return false;
	}

	public void HandleInput()
	{
		Card card = null;
		if (RemoteActionHandler.Get() != null && RemoteActionHandler.Get().GetFriendlyHoverCard() != null)
		{
			Card friendlyHoverCard = RemoteActionHandler.Get().GetFriendlyHoverCard();
			if (friendlyHoverCard.GetController().IsFriendlySide() && friendlyHoverCard.GetZone() is ZoneHand)
			{
				card = friendlyHoverCard;
			}
		}
		if (UniversalInputManager.Get().IsTouchMode())
		{
			if (!InputManager.Get().LeftMouseButtonDown || m_touchedSlot < 0)
			{
				m_touchedSlot = -1;
				UpdateLayout(card);
				return;
			}
			float num = UniversalInputManager.Get().GetMousePosition().x - InputManager.Get().LastMouseDownPosition.x;
			float num2 = Mathf.Max(0f, UniversalInputManager.Get().GetMousePosition().y - InputManager.Get().LastMouseDownPosition.y);
			int cardSlot = GetCardSlot(m_lastMousedOverCard);
			float cardWidth = GetCardWidth();
			float num3 = (float)(cardSlot - m_touchedSlot) * cardWidth;
			float num4 = 10f + num2 * (float)m_TouchDragResistanceFactorY;
			num = ((!(num < num3)) ? Mathf.Max(num3, num - num4) : Mathf.Min(num3, num + num4));
			int num5 = m_touchedSlot + (int)Math.Truncate(num / cardWidth);
			Card cardMousedOver = null;
			if (num5 >= 0 && num5 < m_cards.Count)
			{
				cardMousedOver = m_cards[num5];
			}
			UpdateLayout(cardMousedOver);
			return;
		}
		CardStandIn cardStandIn = null;
		Card card2 = null;
		if (!UniversalInputManager.Get().InputHitAnyObject(Camera.main, GameLayer.InvisibleHitBox1) || !UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.CardRaycast, out var hitInfo))
		{
			if (card == null)
			{
				UpdateLayout(null);
				return;
			}
		}
		else
		{
			cardStandIn = SceneUtils.FindComponentInParents<CardStandIn>(hitInfo.transform);
		}
		if (cardStandIn == null)
		{
			if (card == null)
			{
				UpdateLayout(null);
				return;
			}
		}
		else
		{
			card2 = cardStandIn.linkedCard;
		}
		if (card2 == m_lastMousedOverCard)
		{
			UpdateKeywordPanelsPosition(card2);
		}
		else if (card2 == null && card != null)
		{
			UpdateLayout(card);
		}
		else
		{
			UpdateLayout(card2);
		}
	}

	private void UpdateKeywordPanelsPosition(Card cardMousedOver)
	{
		if (!(cardMousedOver == null) && (cardMousedOver.GetEntity() == null || !cardMousedOver.GetEntity().IsHero()))
		{
			bool showOnRight = ShouldShowCardTooltipOnRight(cardMousedOver);
			TooltipPanelManager.Get().UpdateKeywordPanelsPosition(cardMousedOver, showOnRight);
		}
	}

	public bool ShouldShowCardTooltipOnRight(Card card)
	{
		if (GameState.Get().IsMulliganManagerActive())
		{
			int num = (int)Mathf.Ceil((float)card.GetZone().GetCardCount() / 2f);
			return card.GetZonePosition() <= num;
		}
		if (card.GetActor() == null || card.GetActor().GetMeshRenderer() == null)
		{
			return false;
		}
		return card.GetActor().GetMeshRenderer().bounds.center.x < GetComponent<BoxCollider>().bounds.center.x;
	}

	public void ShowManaGems()
	{
		Vector3 position = m_manaGemPosition.transform.position;
		position.x += -0.5f * m_manaGemMgr.GetWidth();
		m_manaGemMgr.gameObject.transform.position = position;
		m_manaGemMgr.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
	}

	public void HideManaGems()
	{
		m_manaGemMgr.transform.position = new Vector3(0f, 0f, 0f);
	}

	public void SetHandEnlarged(bool enlarged)
	{
		m_handEnlarged = enlarged;
		if (enlarged)
		{
			base.gameObject.transform.localPosition = m_enlargedHandPosition;
			base.gameObject.transform.localScale = m_enlargedHandScale;
			ManaCrystalMgr.Get().ShowPhoneManaTray();
		}
		else
		{
			base.gameObject.transform.localPosition = m_startingPosition;
			base.gameObject.transform.localScale = m_startingScale;
			ManaCrystalMgr.Get().HidePhoneManaTray();
		}
		UpdateCenterAndWidth();
		m_handMoving = true;
		UpdateLayout(null, forced: true);
		m_handMoving = false;
	}

	public bool HandEnlarged()
	{
		return m_handEnlarged;
	}

	public void SetFriendlyHeroTargetingMode(bool enable)
	{
		if (!enable && m_hiddenStandIn != null)
		{
			m_hiddenStandIn.gameObject.SetActive(value: true);
		}
		if (m_targetingMode == enable)
		{
			return;
		}
		m_targetingMode = enable;
		m_heroHitbox.SetActive(enable);
		if (!m_handEnlarged)
		{
			return;
		}
		if (enable)
		{
			m_hiddenStandIn = CurrentStandIn;
			if (m_hiddenStandIn != null)
			{
				m_hiddenStandIn.gameObject.SetActive(value: false);
			}
			Vector3 enlargedHandPosition = m_enlargedHandPosition;
			enlargedHandPosition.z -= m_handHidingDistance;
			base.gameObject.transform.localPosition = enlargedHandPosition;
		}
		else
		{
			base.gameObject.transform.localPosition = m_enlargedHandPosition;
		}
		UpdateCenterAndWidth();
	}

	private void UpdateLayoutImpl(Card cardMousedOver, int overrideCardCount)
	{
		int num = 0;
		if (m_lastMousedOverCard != cardMousedOver && m_lastMousedOverCard != null)
		{
			if (CanAnimateCard(m_lastMousedOverCard) && GetCardSlot(m_lastMousedOverCard) >= 0)
			{
				iTween.Stop(m_lastMousedOverCard.gameObject);
				if (!enemyHand)
				{
					Vector3 mouseOverCardPosition = GetMouseOverCardPosition(m_lastMousedOverCard);
					Vector3 cardPosition = GetCardPosition(m_lastMousedOverCard, overrideCardCount);
					m_lastMousedOverCard.transform.position = new Vector3(mouseOverCardPosition.x, centerOfHand.y, cardPosition.z + 0.5f);
					m_lastMousedOverCard.transform.localScale = GetCardScale();
					m_lastMousedOverCard.transform.localEulerAngles = GetCardRotation(m_lastMousedOverCard);
				}
				GameLayer layer = GameLayer.Default;
				if (m_Side == Player.Side.OPPOSING && m_controller.IsRevealed())
				{
					layer = GameLayer.CardRaycast;
				}
				SceneUtils.SetLayer(m_lastMousedOverCard.gameObject, layer);
			}
			m_lastMousedOverCard.NotifyMousedOut();
		}
		float num2 = 0f;
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			if (!CanAnimateCard(card))
			{
				continue;
			}
			num++;
			float z = (m_flipHandCards ? 534.5f : 354.5f);
			card.transform.rotation = Quaternion.Euler(new Vector3(card.transform.localEulerAngles.x, card.transform.localEulerAngles.y, z));
			float num3 = 0.5f;
			if (m_handMoving)
			{
				num3 = 0.25f;
			}
			if (enemyHand)
			{
				num3 = 1.5f;
			}
			float num4 = 0.25f;
			iTween.EaseType easeType = iTween.EaseType.easeOutExpo;
			float transitionDelay = card.GetTransitionDelay();
			card.SetTransitionDelay(0f);
			ZoneTransitionStyle transitionStyle = card.GetTransitionStyle();
			card.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
			if (transitionStyle != 0)
			{
				switch (transitionStyle)
				{
				case ZoneTransitionStyle.SLOW:
					easeType = iTween.EaseType.easeInExpo;
					num4 = num3;
					break;
				case ZoneTransitionStyle.VERY_SLOW:
					easeType = iTween.EaseType.easeInOutCubic;
					num4 = 1f;
					num3 = 1f;
					break;
				}
				card.GetActor().TurnOnCollider();
			}
			Vector3 vector = GetCardPosition(card, overrideCardCount);
			Vector3 vector2 = GetCardRotation(card, overrideCardCount);
			Vector3 vector3 = GetCardScale();
			if (card == cardMousedOver)
			{
				easeType = iTween.EaseType.easeOutExpo;
				if (enemyHand)
				{
					num4 = 0.15f;
					float num5 = 0.3f;
					vector = new Vector3(vector.x, vector.y, vector.z - num5);
				}
				else
				{
					_ = 0.5f * (float)i;
					int visualCardCount = GetVisualCardCount();
					_ = 0.5f * (float)visualCardCount / 2f;
					float x = m_SelectCardScale;
					float z2 = m_SelectCardScale;
					vector2 = new Vector3(0f, 0f, 0f);
					vector3 = new Vector3(x, vector3.y, z2);
					card.transform.localScale = vector3;
					float num6 = 0.1f;
					vector = GetMouseOverCardPosition(card);
					float x2 = vector.x;
					if (m_handEnlarged)
					{
						vector.x = Mathf.Max(vector.x, m_enlargedHandCardMinX);
						vector.x = Mathf.Min(vector.x, m_enlargedHandCardMaxX);
					}
					card.transform.position = new Vector3((x2 != vector.x) ? vector.x : card.transform.position.x, vector.y, vector.z - num6);
					card.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
					iTween.Stop(card.gameObject);
					easeType = iTween.EaseType.easeOutExpo;
					if ((bool)CardTypeBanner.Get())
					{
						CardTypeBanner.Get().Show(card);
					}
					InputManager.Get().SetMousedOverCard(card);
					if (card.GetEntity() == null || !card.GetEntity().IsHero())
					{
						bool showOnRight = ShouldShowCardTooltipOnRight(card);
						TooltipPanelManager.Get().UpdateKeywordHelp(card, card.GetActor(), showOnRight);
					}
					SceneUtils.SetLayer(card.gameObject, GameLayer.Tooltip);
				}
			}
			else if (GetStandIn(card) != null)
			{
				CardStandIn standIn = GetStandIn(card);
				iTween.Stop(standIn.gameObject);
				standIn.transform.position = vector;
				standIn.transform.localEulerAngles = vector2;
				standIn.transform.localScale = vector3;
				if (!card.CardStandInIsInteractive())
				{
					standIn.DisableStandIn();
				}
				else
				{
					standIn.EnableStandIn();
				}
			}
			if (transitionStyle == ZoneTransitionStyle.INSTANT)
			{
				card.EnableTransitioningZones(enable: false);
				card.transform.position = vector;
				card.transform.localEulerAngles = vector2;
				card.transform.localScale = vector3;
				continue;
			}
			card.EnableTransitioningZones(enable: true);
			string tweenName = ZoneMgr.Get().GetTweenName<ZoneHand>();
			Hashtable args = iTween.Hash("scale", vector3, "delay", transitionDelay, "time", num4, "easeType", easeType, "name", tweenName);
			iTween.ScaleTo(card.gameObject, args);
			Hashtable args2 = iTween.Hash("rotation", vector2, "delay", transitionDelay, "time", num4, "easeType", easeType, "name", tweenName);
			iTween.RotateTo(card.gameObject, args2);
			Hashtable args3 = iTween.Hash("position", vector, "delay", transitionDelay, "time", num3, "easeType", easeType, "name", tweenName);
			iTween.MoveTo(card.gameObject, args3);
			num2 = Mathf.Max(num2, transitionDelay + num3, transitionDelay + num4);
		}
		m_lastMousedOverCard = cardMousedOver;
		if (num > 0)
		{
			StartFinishLayoutTimer(num2);
		}
		else
		{
			UpdateLayoutFinished();
		}
	}

	private void CreateCardStandIn(Card card)
	{
		Actor actor = card.GetActor();
		if (actor != null && actor.GetMeshRenderer() != null)
		{
			actor.GetMeshRenderer().gameObject.layer = 0;
		}
		GameObject obj = AssetLoader.Get().InstantiatePrefab("Card_Collider_Standin.prefab:06f88b48f6884bf4cafbd6696a28ede4", AssetLoadingOptions.IgnorePrefabPosition);
		obj.transform.localEulerAngles = GetCardRotation(card);
		obj.transform.position = GetCardPosition(card);
		obj.transform.localScale = GetCardScale();
		CardStandIn component = obj.GetComponent<CardStandIn>();
		component.linkedCard = card;
		standIns.Add(component);
		if (!component.linkedCard.CardStandInIsInteractive())
		{
			component.DisableStandIn();
		}
	}

	private CardStandIn GetStandIn(Card card)
	{
		if (standIns == null)
		{
			return null;
		}
		foreach (CardStandIn standIn in standIns)
		{
			if (!(standIn == null) && standIn.linkedCard == card)
			{
				return standIn;
			}
		}
		return null;
	}

	public void MakeStandInInteractive(Card card)
	{
		if (!(GetStandIn(card) == null))
		{
			GetStandIn(card).EnableStandIn();
		}
	}

	private void BlowUpOldStandins()
	{
		if (standIns == null)
		{
			standIns = new List<CardStandIn>();
			return;
		}
		foreach (CardStandIn standIn in standIns)
		{
			if (!(standIn == null))
			{
				UnityEngine.Object.Destroy(standIn.gameObject);
			}
		}
		standIns = new List<CardStandIn>();
	}

	public int GetCardSlot(Card card)
	{
		int num = m_cards.IndexOf(card);
		if (m_reservedSlot != -1 && num >= m_reservedSlot)
		{
			num++;
		}
		return num;
	}

	public Vector3 GetCardPosition(Card card)
	{
		return GetCardPosition(card, -1);
	}

	public Vector3 GetCardPosition(Card card, int overrideCardCount)
	{
		int cardSlot = GetCardSlot(card);
		return GetCardPosition(cardSlot, overrideCardCount);
	}

	public Vector3 GetCardPosition(int slot, int overrideCardCount)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		int num4 = GetVisualCardCount();
		if (overrideCardCount >= 0 && overrideCardCount < m_cards.Count)
		{
			num4 = overrideCardCount;
		}
		if (!enemyHand)
		{
			num4 -= TurnStartManager.Get().GetNumCardsToDraw();
		}
		if (IsHandScrunched() && num4 > 1)
		{
			num3 = 1f;
			float num5 = 40f;
			if (!enemyHand)
			{
				num5 += (float)num4;
			}
			num = num5 / (float)(num4 - 1);
			num2 = (0f - num5) / 2f;
		}
		float num6 = 0f;
		num6 = ((!enemyHand) ? (num6 + (num * (float)slot + num2)) : (0f - num * (float)slot - num2));
		float num7 = 0f;
		if ((enemyHand && num6 < 0f) || (!enemyHand && num6 > 0f))
		{
			num7 = Mathf.Sin(Mathf.Abs(num6) * (float)Math.PI / 180f) * GetCardSpacing() / 2f;
		}
		float num8 = centerOfHand.x - GetCardSpacing() / 2f * (float)(num4 - 1 - slot * 2);
		if (m_handEnlarged && m_targetingMode)
		{
			if (num4 % 2 <= 0)
			{
				num8 = ((slot >= num4 / 2) ? (num8 + m_heroWidthInHand / 2f) : (num8 - m_heroWidthInHand / 2f));
			}
			else if (slot < (num4 + 1) / 2)
			{
				num8 -= m_heroWidthInHand;
			}
		}
		float y = centerOfHand.y;
		float num9 = centerOfHand.z;
		if (num4 > 1)
		{
			num9 = ((!enemyHand) ? (centerOfHand.z - Mathf.Pow(Mathf.Abs((float)slot + 0.5f - (float)(num4 / 2)), 2f) / (float)(6 * num4) * num3 - num7) : (num9 + (Mathf.Pow(Mathf.Abs((float)slot + 0.5f - (float)(num4 / 2)), 2f) / (float)(6 * num4) * num3 + num7)));
		}
		if (enemyHand && m_controller.IsRevealed())
		{
			num9 -= 0.2f;
		}
		return new Vector3(num8, y, num9);
	}

	public Vector3 GetCardRotation(Card card)
	{
		return GetCardRotation(card, -1);
	}

	public Vector3 GetCardRotation(Card card, int overrideCardCount)
	{
		return GetCardRotation(GetCardSlot(card), overrideCardCount);
	}

	public Vector3 GetCardRotation(int slot, int overrideCardCount)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = GetVisualCardCount();
		if (overrideCardCount >= 0 && overrideCardCount < m_cards.Count)
		{
			num3 = overrideCardCount;
		}
		if (!enemyHand)
		{
			num3 -= TurnStartManager.Get().GetNumCardsToDraw();
		}
		if (IsHandScrunched() && num3 > 1)
		{
			float num4 = 40f;
			if (!enemyHand)
			{
				num4 += (float)(num3 * 2);
			}
			num = num4 / (float)(num3 - 1);
			num2 = (0f - num4) / 2f;
		}
		float num5 = 0f;
		num5 = ((!enemyHand) ? (num5 + (num * (float)slot + num2)) : (0f - num * (float)slot - num2));
		if (enemyHand && m_controller.IsRevealed())
		{
			num5 += 180f;
		}
		float z = (m_flipHandCards ? 534.5f : 354.5f);
		return new Vector3(0f, num5, z);
	}

	public Vector3 GetCardScale()
	{
		if (enemyHand)
		{
			return new Vector3(0.682f, 0.225f, 0.682f);
		}
		if ((bool)UniversalInputManager.UsePhoneUI && m_handEnlarged)
		{
			return m_enlargedHandCardScale;
		}
		return new Vector3(0.62f, 0.225f, 0.62f);
	}

	private Vector3 GetMouseOverCardPosition(Card card)
	{
		return new Vector3(GetCardPosition(card).x, centerOfHand.y + 1f, base.transform.Find("MouseOverCardHeight").position.z + (float)m_SelectCardOffsetZ);
	}

	private float GetCardSpacing()
	{
		float num = GetDefaultCardSpacing();
		int num2 = GetVisualCardCount();
		if (!enemyHand)
		{
			num2 -= TurnStartManager.Get().GetNumCardsToDraw();
		}
		float num3 = num * (float)num2;
		float num4 = MaxHandWidth();
		if (num3 > num4)
		{
			num = num4 / (float)num2;
		}
		return num;
	}

	private float MaxHandWidth()
	{
		float num = m_maxWidth;
		if (m_handEnlarged && m_targetingMode)
		{
			num -= m_heroWidthInHand;
		}
		return num;
	}

	protected bool CanAnimateCard(Card card)
	{
		bool flag = enemyHand && card.GetPrevZone() is ZonePlay;
		if (card.IsDoNotSort())
		{
			if (flag)
			{
				Log.FaceDownCard.Print("ZoneHand.CanAnimateCard() - card={0} FAILED card.IsDoNotSort()", card);
			}
			return false;
		}
		if (!card.IsActorReady())
		{
			if (flag)
			{
				Log.FaceDownCard.Print("ZoneHand.CanAnimateCard() - card={0} FAILED !card.IsActorReady()", card);
			}
			return false;
		}
		if (m_controller.IsFriendlySide() && (bool)TurnStartManager.Get() && TurnStartManager.Get().IsCardDrawHandled(card))
		{
			return false;
		}
		if (IsCardNotInEnemyHandAnymore(card))
		{
			if (flag)
			{
				Log.FaceDownCard.Print("ZoneHand.CanAnimateCard() - card={0} FAILED IsCardNotInEnemyHandAnymore()", card);
			}
			return false;
		}
		if (card.HasBeenGrabbedByEnemyActionHandler())
		{
			if (flag)
			{
				Log.FaceDownCard.Print("ZoneHand.CanAnimateCard() - card={0} FAILED card.HasBeenGrabbedByEnemyActionHandler()", card);
			}
			return false;
		}
		return true;
	}

	private bool IsCardNotInEnemyHandAnymore(Card card)
	{
		if (card.GetEntity().GetZone() != TAG_ZONE.HAND)
		{
			return enemyHand;
		}
		return false;
	}

	private void UpdateCenterAndWidth()
	{
		centerOfHand = GetComponent<Collider>().bounds.center;
		m_maxWidth = GetComponent<Collider>().bounds.size.x;
	}

	public void OnCardGrabbed(Card card)
	{
		Entity entity = card.GetEntity();
		if (entity == null)
		{
			return;
		}
		Player controller = entity.GetController();
		if (controller != null && ((controller.HasTag(GAME_TAG.HEALING_DOES_DAMAGE) && card.CanPlayHealingDoesDamageHint()) || (controller.HasTag(GAME_TAG.LIFESTEAL_DAMAGES_OPPOSING_HERO) && card.CanPlayLifestealDoesDamageHint())))
		{
			Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST);
			if (actorSpell != null)
			{
				actorSpell.Reactivate();
			}
		}
	}

	public void OnCardHeld(Card heldCard)
	{
		if (!(heldCard == null) && heldCard.GetEntity() != null && heldCard.GetEntity().HasTag(GAME_TAG.TWINSPELL))
		{
			OnTwinspellHeld(heldCard);
		}
	}

	private void OnTwinspellHeld(Card heldCard)
	{
		if (m_twinspellHoldSpellInstance == null)
		{
			m_twinspellHoldSpellInstance = UnityEngine.Object.Instantiate(m_TwinspellHoldSpell);
			m_twinspellHoldSpellInstance.Initialize(heldCard.GetEntity().GetEntityId(), heldCard.GetZonePosition());
		}
		else if (m_twinspellHoldSpellInstance.GetOriginalSpellEntityId() != heldCard.GetEntity().GetEntityId() || m_twinspellHoldSpellInstance.GetFakeTwinspellZonePosition() != heldCard.GetEntity().GetZonePosition())
		{
			m_twinspellHoldSpellInstance.Initialize(heldCard.GetEntity().GetEntityId(), heldCard.GetZonePosition());
		}
		heldCard.GetActor().ToggleForceIdle(bOn: true);
		heldCard.UpdateActorState();
		SpellUtils.ActivateBirthIfNecessary(m_twinspellHoldSpellInstance);
	}

	public void OnTwinspellPlayed(Card playedCard)
	{
		if (playedCard.GetEntity().HasTag(GAME_TAG.TWINSPELL))
		{
			playedCard.GetActor().ToggleForceIdle(bOn: false);
			playedCard.UpdateActorState();
			ReserveCardSlot(GetCardSlot(playedCard));
			m_playingTwinspellEntityId = playedCard.GetEntity().GetEntityId();
			if (m_twinspellHoldSpellInstance != null)
			{
				m_twinspellHoldSpellInstance.ActivateState(SpellStateType.ACTION);
			}
		}
	}

	public void OnTwinspellDropped(Card droppedCard)
	{
		if (droppedCard.GetEntity().HasTag(GAME_TAG.TWINSPELL))
		{
			ActivateTwinspellSpellDeath();
			droppedCard.GetActor().ToggleForceIdle(bOn: false);
			droppedCard.UpdateActorState();
		}
	}

	public void ActivateTwinspellSpellDeath()
	{
		if (m_twinspellHoldSpellInstance != null)
		{
			SpellUtils.ActivateDeathIfNecessary(m_twinspellHoldSpellInstance);
		}
		m_playingTwinspellEntityId = -1;
	}

	public bool IsTwinspellBeingPlayed(Entity twinspellEntity)
	{
		if (twinspellEntity == null)
		{
			return false;
		}
		return twinspellEntity.GetEntityId() == m_playingTwinspellEntityId;
	}

	private void OnCantPlay(Entity entity, object userData)
	{
		if (entity.IsControlledByFriendlySidePlayer() && entity.IsTwinspell())
		{
			ActivateTwinspellSpellDeath();
			ClearReservedCard();
		}
	}

	public override bool AddCard(Card card)
	{
		return base.AddCard(card);
	}
}
