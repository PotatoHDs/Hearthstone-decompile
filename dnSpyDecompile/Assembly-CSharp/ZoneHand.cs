using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200035B RID: 859
public class ZoneHand : Zone
{
	// Token: 0x1700050D RID: 1293
	// (get) Token: 0x060031DD RID: 12765 RVA: 0x000FEEEC File Offset: 0x000FD0EC
	public CardStandIn CurrentStandIn
	{
		get
		{
			if (this.m_lastMousedOverCard == null)
			{
				return null;
			}
			return this.GetStandIn(this.m_lastMousedOverCard);
		}
	}

	// Token: 0x060031DE RID: 12766 RVA: 0x000FEF0C File Offset: 0x000FD10C
	private void Awake()
	{
		this.enemyHand = (this.m_Side == Player.Side.OPPOSING);
		this.m_startingPosition = base.gameObject.transform.localPosition;
		this.m_startingScale = base.gameObject.transform.localScale;
		this.UpdateCenterAndWidth();
	}

	// Token: 0x060031DF RID: 12767 RVA: 0x000FEF5A File Offset: 0x000FD15A
	private void Start()
	{
		GameState.Get().RegisterCantPlayListener(new GameState.CantPlayCallback(this.OnCantPlay), null);
	}

	// Token: 0x060031E0 RID: 12768 RVA: 0x000FEF74 File Offset: 0x000FD174
	public Card GetLastMousedOverCard()
	{
		return this.m_lastMousedOverCard;
	}

	// Token: 0x060031E1 RID: 12769 RVA: 0x000FEF7C File Offset: 0x000FD17C
	public bool IsHandScrunched()
	{
		int num = this.GetVisualCardCount();
		if (this.m_handEnlarged && num > 3)
		{
			return true;
		}
		float defaultCardSpacing = this.GetDefaultCardSpacing();
		if (!this.enemyHand)
		{
			num -= TurnStartManager.Get().GetNumCardsToDraw();
		}
		return defaultCardSpacing * (float)num > this.MaxHandWidth();
	}

	// Token: 0x060031E2 RID: 12770 RVA: 0x000FEFC7 File Offset: 0x000FD1C7
	public void SetDoNotUpdateLayout(bool enable)
	{
		this.m_doNotUpdateLayout = enable;
	}

	// Token: 0x060031E3 RID: 12771 RVA: 0x000FEFD0 File Offset: 0x000FD1D0
	public bool IsDoNotUpdateLayout()
	{
		return this.m_doNotUpdateLayout;
	}

	// Token: 0x060031E4 RID: 12772 RVA: 0x000FEFD8 File Offset: 0x000FD1D8
	public override void OnSpellPowerEntityEnteredPlay(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		foreach (Card card in this.m_cards)
		{
			if (card.CanPlaySpellPowerHint(spellSchool))
			{
				Spell actorSpell = card.GetActorSpell(SpellType.SPELL_POWER_HINT_BURST, true);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
			}
		}
	}

	// Token: 0x060031E5 RID: 12773 RVA: 0x000FF048 File Offset: 0x000FD248
	public override void OnSpellPowerEntityMousedOver(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		if (TargetReticleManager.Get().IsActive())
		{
			return;
		}
		foreach (Card card in this.m_cards)
		{
			if (card.CanPlaySpellPowerHint(spellSchool))
			{
				Spell actorSpell = card.GetActorSpell(SpellType.SPELL_POWER_HINT_BURST, true);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
				Spell actorSpell2 = card.GetActorSpell(SpellType.SPELL_POWER_HINT_IDLE, true);
				if (actorSpell2 != null)
				{
					actorSpell2.ActivateState(SpellStateType.BIRTH);
				}
			}
		}
	}

	// Token: 0x060031E6 RID: 12774 RVA: 0x000FF0E0 File Offset: 0x000FD2E0
	public override void OnSpellPowerEntityMousedOut(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
		foreach (Card card in this.m_cards)
		{
			Spell actorSpell = card.GetActorSpell(SpellType.SPELL_POWER_HINT_IDLE, true);
			if (!(actorSpell == null) && actorSpell.IsActive())
			{
				actorSpell.ActivateState(SpellStateType.DEATH);
			}
		}
	}

	// Token: 0x060031E7 RID: 12775 RVA: 0x000FF14C File Offset: 0x000FD34C
	public float GetDefaultCardSpacing()
	{
		if (UniversalInputManager.UsePhoneUI && this.m_handEnlarged)
		{
			return this.m_enlargedHandDefaultCardSpacing;
		}
		return 1.270804f;
	}

	// Token: 0x060031E8 RID: 12776 RVA: 0x000FF16E File Offset: 0x000FD36E
	public int GetVisualCardCount()
	{
		if (this.m_reservedSlot == -1)
		{
			return this.m_cards.Count;
		}
		return this.m_cards.Count + 1;
	}

	// Token: 0x060031E9 RID: 12777 RVA: 0x000FF192 File Offset: 0x000FD392
	public void ReserveCardSlot(int slot)
	{
		this.m_reservedSlot = slot;
	}

	// Token: 0x060031EA RID: 12778 RVA: 0x000FF19B File Offset: 0x000FD39B
	public void SortWithSpotForReservedCard(int slot)
	{
		this.m_reservedSlot = slot;
		this.UpdateLayout();
	}

	// Token: 0x060031EB RID: 12779 RVA: 0x000FF1AA File Offset: 0x000FD3AA
	public void ClearReservedCard()
	{
		this.SortWithSpotForReservedCard(-1);
	}

	// Token: 0x060031EC RID: 12780 RVA: 0x000FF1B4 File Offset: 0x000FD3B4
	public override void UpdateLayout()
	{
		if (!GameState.Get().IsMulliganManagerActive() && !this.enemyHand)
		{
			this.BlowUpOldStandins();
			for (int i = 0; i < this.m_cards.Count; i++)
			{
				Card card = this.m_cards[i];
				this.CreateCardStandIn(card);
			}
		}
		this.UpdateLayout(null, true, -1);
	}

	// Token: 0x060031ED RID: 12781 RVA: 0x000FF210 File Offset: 0x000FD410
	public void ForceStandInUpdate()
	{
		this.BlowUpOldStandins();
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			this.CreateCardStandIn(card);
		}
	}

	// Token: 0x060031EE RID: 12782 RVA: 0x000FF24D File Offset: 0x000FD44D
	public void UpdateLayout(Card cardMousedOver)
	{
		this.UpdateLayout(cardMousedOver, false, -1);
	}

	// Token: 0x060031EF RID: 12783 RVA: 0x000FF258 File Offset: 0x000FD458
	public void UpdateLayout(Card cardMousedOver, bool forced)
	{
		this.UpdateLayout(cardMousedOver, forced, -1);
	}

	// Token: 0x060031F0 RID: 12784 RVA: 0x000FF264 File Offset: 0x000FD464
	public void UpdateLayout(Card cardMousedOver, bool forced, int overrideCardCount)
	{
		this.m_updatingLayout++;
		if (base.IsBlockingLayout())
		{
			base.UpdateLayoutFinished();
			return;
		}
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			if (!card.IsDoNotSort() && card.GetTransitionStyle() != ZoneTransitionStyle.VERY_SLOW && !this.IsCardNotInEnemyHandAnymore(card) && !card.HasBeenGrabbedByEnemyActionHandler())
			{
				Spell bestSummonSpell = card.GetBestSummonSpell();
				if (!(bestSummonSpell != null) || !bestSummonSpell.IsActive())
				{
					card.ShowCard();
				}
			}
		}
		if (this.m_doNotUpdateLayout)
		{
			base.UpdateLayoutFinished();
			return;
		}
		if (cardMousedOver != null && this.GetCardSlot(cardMousedOver) < 0)
		{
			cardMousedOver = null;
		}
		if (!forced && cardMousedOver == this.m_lastMousedOverCard)
		{
			this.m_updatingLayout--;
			this.UpdateKeywordPanelsPosition(cardMousedOver);
			return;
		}
		this.m_cards.Sort(new Comparison<Card>(Zone.CardSortComparison));
		this.UpdateLayoutImpl(cardMousedOver, overrideCardCount);
	}

	// Token: 0x060031F1 RID: 12785 RVA: 0x000FF35C File Offset: 0x000FD55C
	public void HideCards()
	{
		foreach (Card card in this.m_cards)
		{
			card.GetActor().gameObject.SetActive(false);
		}
	}

	// Token: 0x060031F2 RID: 12786 RVA: 0x000FF3B8 File Offset: 0x000FD5B8
	public void ShowCards()
	{
		foreach (Card card in this.m_cards)
		{
			card.GetActor().gameObject.SetActive(true);
		}
	}

	// Token: 0x060031F3 RID: 12787 RVA: 0x000FF414 File Offset: 0x000FD614
	public float GetCardWidth()
	{
		float cardSpacing = this.GetCardSpacing();
		Vector3 position = this.centerOfHand;
		position.x -= cardSpacing / 2f;
		Vector3 position2 = this.centerOfHand;
		position2.x += cardSpacing / 2f;
		Vector3 vector = Camera.main.WorldToScreenPoint(position);
		return Camera.main.WorldToScreenPoint(position2).x - vector.x;
	}

	// Token: 0x060031F4 RID: 12788 RVA: 0x000FF480 File Offset: 0x000FD680
	public bool TouchReceived()
	{
		RaycastHit raycastHit;
		if (!UniversalInputManager.Get().GetInputHitInfo(GameLayer.CardRaycast.LayerBit(), out raycastHit))
		{
			this.m_touchedSlot = -1;
		}
		CardStandIn cardStandIn = SceneUtils.FindComponentInParents<CardStandIn>(raycastHit.transform);
		if (cardStandIn != null)
		{
			this.m_touchedSlot = this.GetCardSlot(cardStandIn.linkedCard);
			return true;
		}
		this.m_touchedSlot = -1;
		return false;
	}

	// Token: 0x060031F5 RID: 12789 RVA: 0x000FF4E0 File Offset: 0x000FD6E0
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
			if (!InputManager.Get().LeftMouseButtonDown || this.m_touchedSlot < 0)
			{
				this.m_touchedSlot = -1;
				this.UpdateLayout(card);
				return;
			}
			float num = UniversalInputManager.Get().GetMousePosition().x - InputManager.Get().LastMouseDownPosition.x;
			float num2 = Mathf.Max(0f, UniversalInputManager.Get().GetMousePosition().y - InputManager.Get().LastMouseDownPosition.y);
			float cardSlot = (float)this.GetCardSlot(this.m_lastMousedOverCard);
			float cardWidth = this.GetCardWidth();
			float num3 = (cardSlot - (float)this.m_touchedSlot) * cardWidth;
			float num4 = 10f + num2 * this.m_TouchDragResistanceFactorY;
			if (num < num3)
			{
				num = Mathf.Min(num3, num + num4);
			}
			else
			{
				num = Mathf.Max(num3, num - num4);
			}
			int num5 = this.m_touchedSlot + (int)Math.Truncate((double)(num / cardWidth));
			Card cardMousedOver = null;
			if (num5 >= 0 && num5 < this.m_cards.Count)
			{
				cardMousedOver = this.m_cards[num5];
			}
			this.UpdateLayout(cardMousedOver);
			return;
		}
		else
		{
			CardStandIn cardStandIn = null;
			Card card2 = null;
			RaycastHit raycastHit;
			if (!UniversalInputManager.Get().InputHitAnyObject(Camera.main, GameLayer.InvisibleHitBox1) || !UniversalInputManager.Get().GetInputHitInfo(Camera.main, GameLayer.CardRaycast, out raycastHit))
			{
				if (card == null)
				{
					this.UpdateLayout(null);
					return;
				}
			}
			else
			{
				cardStandIn = SceneUtils.FindComponentInParents<CardStandIn>(raycastHit.transform);
			}
			if (cardStandIn == null)
			{
				if (card == null)
				{
					this.UpdateLayout(null);
					return;
				}
			}
			else
			{
				card2 = cardStandIn.linkedCard;
			}
			if (card2 == this.m_lastMousedOverCard)
			{
				this.UpdateKeywordPanelsPosition(card2);
				return;
			}
			if (card2 == null && card != null)
			{
				this.UpdateLayout(card);
				return;
			}
			this.UpdateLayout(card2);
			return;
		}
	}

	// Token: 0x060031F6 RID: 12790 RVA: 0x000FF6F8 File Offset: 0x000FD8F8
	private void UpdateKeywordPanelsPosition(Card cardMousedOver)
	{
		if (cardMousedOver == null)
		{
			return;
		}
		if (cardMousedOver.GetEntity() == null || !cardMousedOver.GetEntity().IsHero())
		{
			bool showOnRight = this.ShouldShowCardTooltipOnRight(cardMousedOver);
			TooltipPanelManager.Get().UpdateKeywordPanelsPosition(cardMousedOver, showOnRight);
		}
	}

	// Token: 0x060031F7 RID: 12791 RVA: 0x000FF738 File Offset: 0x000FD938
	public bool ShouldShowCardTooltipOnRight(Card card)
	{
		if (GameState.Get().IsMulliganManagerActive())
		{
			int num = (int)Mathf.Ceil((float)card.GetZone().GetCardCount() / 2f);
			return card.GetZonePosition() <= num;
		}
		return !(card.GetActor() == null) && !(card.GetActor().GetMeshRenderer(false) == null) && card.GetActor().GetMeshRenderer(false).bounds.center.x < base.GetComponent<BoxCollider>().bounds.center.x;
	}

	// Token: 0x060031F8 RID: 12792 RVA: 0x000FF7D4 File Offset: 0x000FD9D4
	public void ShowManaGems()
	{
		Vector3 position = this.m_manaGemPosition.transform.position;
		position.x += -0.5f * this.m_manaGemMgr.GetWidth();
		this.m_manaGemMgr.gameObject.transform.position = position;
		this.m_manaGemMgr.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
	}

	// Token: 0x060031F9 RID: 12793 RVA: 0x000FF848 File Offset: 0x000FDA48
	public void HideManaGems()
	{
		this.m_manaGemMgr.transform.position = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x060031FA RID: 12794 RVA: 0x000FF870 File Offset: 0x000FDA70
	public void SetHandEnlarged(bool enlarged)
	{
		this.m_handEnlarged = enlarged;
		if (enlarged)
		{
			base.gameObject.transform.localPosition = this.m_enlargedHandPosition;
			base.gameObject.transform.localScale = this.m_enlargedHandScale;
			ManaCrystalMgr.Get().ShowPhoneManaTray();
		}
		else
		{
			base.gameObject.transform.localPosition = this.m_startingPosition;
			base.gameObject.transform.localScale = this.m_startingScale;
			ManaCrystalMgr.Get().HidePhoneManaTray();
		}
		this.UpdateCenterAndWidth();
		this.m_handMoving = true;
		this.UpdateLayout(null, true);
		this.m_handMoving = false;
	}

	// Token: 0x060031FB RID: 12795 RVA: 0x000FF911 File Offset: 0x000FDB11
	public bool HandEnlarged()
	{
		return this.m_handEnlarged;
	}

	// Token: 0x060031FC RID: 12796 RVA: 0x000FF91C File Offset: 0x000FDB1C
	public void SetFriendlyHeroTargetingMode(bool enable)
	{
		if (!enable && this.m_hiddenStandIn != null)
		{
			this.m_hiddenStandIn.gameObject.SetActive(true);
		}
		if (this.m_targetingMode == enable)
		{
			return;
		}
		this.m_targetingMode = enable;
		this.m_heroHitbox.SetActive(enable);
		if (!this.m_handEnlarged)
		{
			return;
		}
		if (enable)
		{
			this.m_hiddenStandIn = this.CurrentStandIn;
			if (this.m_hiddenStandIn != null)
			{
				this.m_hiddenStandIn.gameObject.SetActive(false);
			}
			Vector3 enlargedHandPosition = this.m_enlargedHandPosition;
			enlargedHandPosition.z -= this.m_handHidingDistance;
			base.gameObject.transform.localPosition = enlargedHandPosition;
		}
		else
		{
			base.gameObject.transform.localPosition = this.m_enlargedHandPosition;
		}
		this.UpdateCenterAndWidth();
	}

	// Token: 0x060031FD RID: 12797 RVA: 0x000FF9E8 File Offset: 0x000FDBE8
	private void UpdateLayoutImpl(Card cardMousedOver, int overrideCardCount)
	{
		int num = 0;
		if (this.m_lastMousedOverCard != cardMousedOver && this.m_lastMousedOverCard != null)
		{
			if (this.CanAnimateCard(this.m_lastMousedOverCard) && this.GetCardSlot(this.m_lastMousedOverCard) >= 0)
			{
				iTween.Stop(this.m_lastMousedOverCard.gameObject);
				if (!this.enemyHand)
				{
					Vector3 mouseOverCardPosition = this.GetMouseOverCardPosition(this.m_lastMousedOverCard);
					Vector3 cardPosition = this.GetCardPosition(this.m_lastMousedOverCard, overrideCardCount);
					this.m_lastMousedOverCard.transform.position = new Vector3(mouseOverCardPosition.x, this.centerOfHand.y, cardPosition.z + 0.5f);
					this.m_lastMousedOverCard.transform.localScale = this.GetCardScale();
					this.m_lastMousedOverCard.transform.localEulerAngles = this.GetCardRotation(this.m_lastMousedOverCard);
				}
				GameLayer layer = GameLayer.Default;
				if (this.m_Side == Player.Side.OPPOSING && this.m_controller.IsRevealed())
				{
					layer = GameLayer.CardRaycast;
				}
				SceneUtils.SetLayer(this.m_lastMousedOverCard.gameObject, layer);
			}
			this.m_lastMousedOverCard.NotifyMousedOut();
		}
		float num2 = 0f;
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			if (this.CanAnimateCard(card))
			{
				num++;
				float z = this.m_flipHandCards ? 534.5f : 354.5f;
				card.transform.rotation = Quaternion.Euler(new Vector3(card.transform.localEulerAngles.x, card.transform.localEulerAngles.y, z));
				float num3 = 0.5f;
				if (this.m_handMoving)
				{
					num3 = 0.25f;
				}
				if (this.enemyHand)
				{
					num3 = 1.5f;
				}
				float num4 = 0.25f;
				iTween.EaseType easeType = iTween.EaseType.easeOutExpo;
				float transitionDelay = card.GetTransitionDelay();
				card.SetTransitionDelay(0f);
				ZoneTransitionStyle transitionStyle = card.GetTransitionStyle();
				card.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
				if (transitionStyle != ZoneTransitionStyle.NORMAL)
				{
					if (transitionStyle != ZoneTransitionStyle.SLOW)
					{
						if (transitionStyle == ZoneTransitionStyle.VERY_SLOW)
						{
							easeType = iTween.EaseType.easeInOutCubic;
							num4 = 1f;
							num3 = 1f;
						}
					}
					else
					{
						easeType = iTween.EaseType.easeInExpo;
						num4 = num3;
					}
					card.GetActor().TurnOnCollider();
				}
				Vector3 vector = this.GetCardPosition(card, overrideCardCount);
				Vector3 cardRotation = this.GetCardRotation(card, overrideCardCount);
				Vector3 cardScale = this.GetCardScale();
				if (card == cardMousedOver)
				{
					easeType = iTween.EaseType.easeOutExpo;
					if (this.enemyHand)
					{
						num4 = 0.15f;
						float num5 = 0.3f;
						vector = new Vector3(vector.x, vector.y, vector.z - num5);
					}
					else
					{
						float num6 = 0.5f * (float)i;
						int visualCardCount = this.GetVisualCardCount();
						float num7 = 0.5f * (float)visualCardCount / 2f;
						float x = this.m_SelectCardScale;
						float z2 = this.m_SelectCardScale;
						cardRotation = new Vector3(0f, 0f, 0f);
						cardScale = new Vector3(x, cardScale.y, z2);
						card.transform.localScale = cardScale;
						float num8 = 0.1f;
						vector = this.GetMouseOverCardPosition(card);
						float x2 = vector.x;
						if (this.m_handEnlarged)
						{
							vector.x = Mathf.Max(vector.x, this.m_enlargedHandCardMinX);
							vector.x = Mathf.Min(vector.x, this.m_enlargedHandCardMaxX);
						}
						card.transform.position = new Vector3((x2 != vector.x) ? vector.x : card.transform.position.x, vector.y, vector.z - num8);
						card.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
						iTween.Stop(card.gameObject);
						easeType = iTween.EaseType.easeOutExpo;
						if (CardTypeBanner.Get())
						{
							CardTypeBanner.Get().Show(card);
						}
						InputManager.Get().SetMousedOverCard(card);
						if (card.GetEntity() == null || !card.GetEntity().IsHero())
						{
							bool showOnRight = this.ShouldShowCardTooltipOnRight(card);
							TooltipPanelManager.Get().UpdateKeywordHelp(card, card.GetActor(), showOnRight, null, null);
						}
						SceneUtils.SetLayer(card.gameObject, GameLayer.Tooltip);
					}
				}
				else if (this.GetStandIn(card) != null)
				{
					CardStandIn standIn = this.GetStandIn(card);
					iTween.Stop(standIn.gameObject);
					standIn.transform.position = vector;
					standIn.transform.localEulerAngles = cardRotation;
					standIn.transform.localScale = cardScale;
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
					card.EnableTransitioningZones(false);
					card.transform.position = vector;
					card.transform.localEulerAngles = cardRotation;
					card.transform.localScale = cardScale;
				}
				else
				{
					card.EnableTransitioningZones(true);
					string tweenName = ZoneMgr.Get().GetTweenName<ZoneHand>();
					Hashtable args = iTween.Hash(new object[]
					{
						"scale",
						cardScale,
						"delay",
						transitionDelay,
						"time",
						num4,
						"easeType",
						easeType,
						"name",
						tweenName
					});
					iTween.ScaleTo(card.gameObject, args);
					Hashtable args2 = iTween.Hash(new object[]
					{
						"rotation",
						cardRotation,
						"delay",
						transitionDelay,
						"time",
						num4,
						"easeType",
						easeType,
						"name",
						tweenName
					});
					iTween.RotateTo(card.gameObject, args2);
					Hashtable args3 = iTween.Hash(new object[]
					{
						"position",
						vector,
						"delay",
						transitionDelay,
						"time",
						num3,
						"easeType",
						easeType,
						"name",
						tweenName
					});
					iTween.MoveTo(card.gameObject, args3);
					num2 = Mathf.Max(new float[]
					{
						num2,
						transitionDelay + num3,
						transitionDelay + num4
					});
				}
			}
		}
		this.m_lastMousedOverCard = cardMousedOver;
		if (num > 0)
		{
			base.StartFinishLayoutTimer(num2);
			return;
		}
		base.UpdateLayoutFinished();
	}

	// Token: 0x060031FE RID: 12798 RVA: 0x001000A0 File Offset: 0x000FE2A0
	private void CreateCardStandIn(Card card)
	{
		Actor actor = card.GetActor();
		if (actor != null && actor.GetMeshRenderer(false) != null)
		{
			actor.GetMeshRenderer(false).gameObject.layer = 0;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Card_Collider_Standin.prefab:06f88b48f6884bf4cafbd6696a28ede4", AssetLoadingOptions.IgnorePrefabPosition);
		gameObject.transform.localEulerAngles = this.GetCardRotation(card);
		gameObject.transform.position = this.GetCardPosition(card);
		gameObject.transform.localScale = this.GetCardScale();
		CardStandIn component = gameObject.GetComponent<CardStandIn>();
		component.linkedCard = card;
		this.standIns.Add(component);
		if (!component.linkedCard.CardStandInIsInteractive())
		{
			component.DisableStandIn();
		}
	}

	// Token: 0x060031FF RID: 12799 RVA: 0x00100154 File Offset: 0x000FE354
	private CardStandIn GetStandIn(Card card)
	{
		if (this.standIns == null)
		{
			return null;
		}
		foreach (CardStandIn cardStandIn in this.standIns)
		{
			if (!(cardStandIn == null) && cardStandIn.linkedCard == card)
			{
				return cardStandIn;
			}
		}
		return null;
	}

	// Token: 0x06003200 RID: 12800 RVA: 0x001001C8 File Offset: 0x000FE3C8
	public void MakeStandInInteractive(Card card)
	{
		if (this.GetStandIn(card) == null)
		{
			return;
		}
		this.GetStandIn(card).EnableStandIn();
	}

	// Token: 0x06003201 RID: 12801 RVA: 0x001001E8 File Offset: 0x000FE3E8
	private void BlowUpOldStandins()
	{
		if (this.standIns == null)
		{
			this.standIns = new List<CardStandIn>();
			return;
		}
		foreach (CardStandIn cardStandIn in this.standIns)
		{
			if (!(cardStandIn == null))
			{
				UnityEngine.Object.Destroy(cardStandIn.gameObject);
			}
		}
		this.standIns = new List<CardStandIn>();
	}

	// Token: 0x06003202 RID: 12802 RVA: 0x00100268 File Offset: 0x000FE468
	public int GetCardSlot(Card card)
	{
		int num = this.m_cards.IndexOf(card);
		if (this.m_reservedSlot != -1 && num >= this.m_reservedSlot)
		{
			num++;
		}
		return num;
	}

	// Token: 0x06003203 RID: 12803 RVA: 0x00100299 File Offset: 0x000FE499
	public Vector3 GetCardPosition(Card card)
	{
		return this.GetCardPosition(card, -1);
	}

	// Token: 0x06003204 RID: 12804 RVA: 0x001002A4 File Offset: 0x000FE4A4
	public Vector3 GetCardPosition(Card card, int overrideCardCount)
	{
		int cardSlot = this.GetCardSlot(card);
		return this.GetCardPosition(cardSlot, overrideCardCount);
	}

	// Token: 0x06003205 RID: 12805 RVA: 0x001002C4 File Offset: 0x000FE4C4
	public Vector3 GetCardPosition(int slot, int overrideCardCount)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		int num4 = this.GetVisualCardCount();
		if (overrideCardCount >= 0 && overrideCardCount < this.m_cards.Count)
		{
			num4 = overrideCardCount;
		}
		if (!this.enemyHand)
		{
			num4 -= TurnStartManager.Get().GetNumCardsToDraw();
		}
		if (this.IsHandScrunched() && num4 > 1)
		{
			num3 = 1f;
			float num5 = 40f;
			if (!this.enemyHand)
			{
				num5 += (float)num4;
			}
			num = num5 / (float)(num4 - 1);
			num2 = -num5 / 2f;
		}
		float num6 = 0f;
		if (this.enemyHand)
		{
			num6 = 0f - num * (float)slot - num2;
		}
		else
		{
			num6 += num * (float)slot + num2;
		}
		float num7 = 0f;
		if ((this.enemyHand && num6 < 0f) || (!this.enemyHand && num6 > 0f))
		{
			num7 = Mathf.Sin(Mathf.Abs(num6) * 3.1415927f / 180f) * this.GetCardSpacing() / 2f;
		}
		float num8 = this.centerOfHand.x - this.GetCardSpacing() / 2f * (float)(num4 - 1 - slot * 2);
		if (this.m_handEnlarged && this.m_targetingMode)
		{
			if (num4 % 2 > 0)
			{
				if (slot < (num4 + 1) / 2)
				{
					num8 -= this.m_heroWidthInHand;
				}
			}
			else if (slot < num4 / 2)
			{
				num8 -= this.m_heroWidthInHand / 2f;
			}
			else
			{
				num8 += this.m_heroWidthInHand / 2f;
			}
		}
		float y = this.centerOfHand.y;
		float num9 = this.centerOfHand.z;
		if (num4 > 1)
		{
			if (this.enemyHand)
			{
				num9 += Mathf.Pow(Mathf.Abs((float)slot + 0.5f - (float)(num4 / 2)), 2f) / (float)(6 * num4) * num3 + num7;
			}
			else
			{
				num9 = this.centerOfHand.z - Mathf.Pow(Mathf.Abs((float)slot + 0.5f - (float)(num4 / 2)), 2f) / (float)(6 * num4) * num3 - num7;
			}
		}
		if (this.enemyHand && this.m_controller.IsRevealed())
		{
			num9 -= 0.2f;
		}
		return new Vector3(num8, y, num9);
	}

	// Token: 0x06003206 RID: 12806 RVA: 0x001004F2 File Offset: 0x000FE6F2
	public Vector3 GetCardRotation(Card card)
	{
		return this.GetCardRotation(card, -1);
	}

	// Token: 0x06003207 RID: 12807 RVA: 0x001004FC File Offset: 0x000FE6FC
	public Vector3 GetCardRotation(Card card, int overrideCardCount)
	{
		return this.GetCardRotation(this.GetCardSlot(card), overrideCardCount);
	}

	// Token: 0x06003208 RID: 12808 RVA: 0x0010050C File Offset: 0x000FE70C
	public Vector3 GetCardRotation(int slot, int overrideCardCount)
	{
		float num = 0f;
		float num2 = 0f;
		int num3 = this.GetVisualCardCount();
		if (overrideCardCount >= 0 && overrideCardCount < this.m_cards.Count)
		{
			num3 = overrideCardCount;
		}
		if (!this.enemyHand)
		{
			num3 -= TurnStartManager.Get().GetNumCardsToDraw();
		}
		if (this.IsHandScrunched() && num3 > 1)
		{
			float num4 = 40f;
			if (!this.enemyHand)
			{
				num4 += (float)(num3 * 2);
			}
			num = num4 / (float)(num3 - 1);
			num2 = -num4 / 2f;
		}
		float num5 = 0f;
		if (this.enemyHand)
		{
			num5 = 0f - num * (float)slot - num2;
		}
		else
		{
			num5 += num * (float)slot + num2;
		}
		if (this.enemyHand && this.m_controller.IsRevealed())
		{
			num5 += 180f;
		}
		float z = this.m_flipHandCards ? 534.5f : 354.5f;
		return new Vector3(0f, num5, z);
	}

	// Token: 0x06003209 RID: 12809 RVA: 0x001005F4 File Offset: 0x000FE7F4
	public Vector3 GetCardScale()
	{
		if (this.enemyHand)
		{
			return new Vector3(0.682f, 0.225f, 0.682f);
		}
		if (UniversalInputManager.UsePhoneUI && this.m_handEnlarged)
		{
			return this.m_enlargedHandCardScale;
		}
		return new Vector3(0.62f, 0.225f, 0.62f);
	}

	// Token: 0x0600320A RID: 12810 RVA: 0x00100650 File Offset: 0x000FE850
	private Vector3 GetMouseOverCardPosition(Card card)
	{
		return new Vector3(this.GetCardPosition(card).x, this.centerOfHand.y + 1f, base.transform.Find("MouseOverCardHeight").position.z + this.m_SelectCardOffsetZ);
	}

	// Token: 0x0600320B RID: 12811 RVA: 0x001006A8 File Offset: 0x000FE8A8
	private float GetCardSpacing()
	{
		float num = this.GetDefaultCardSpacing();
		int num2 = this.GetVisualCardCount();
		if (!this.enemyHand)
		{
			num2 -= TurnStartManager.Get().GetNumCardsToDraw();
		}
		float num3 = num * (float)num2;
		float num4 = this.MaxHandWidth();
		if (num3 > num4)
		{
			num = num4 / (float)num2;
		}
		return num;
	}

	// Token: 0x0600320C RID: 12812 RVA: 0x001006EC File Offset: 0x000FE8EC
	private float MaxHandWidth()
	{
		float num = this.m_maxWidth;
		if (this.m_handEnlarged && this.m_targetingMode)
		{
			num -= this.m_heroWidthInHand;
		}
		return num;
	}

	// Token: 0x0600320D RID: 12813 RVA: 0x0010071C File Offset: 0x000FE91C
	protected bool CanAnimateCard(Card card)
	{
		bool flag = this.enemyHand && card.GetPrevZone() is ZonePlay;
		if (card.IsDoNotSort())
		{
			if (flag)
			{
				Log.FaceDownCard.Print("ZoneHand.CanAnimateCard() - card={0} FAILED card.IsDoNotSort()", new object[]
				{
					card
				});
			}
			return false;
		}
		if (!card.IsActorReady())
		{
			if (flag)
			{
				Log.FaceDownCard.Print("ZoneHand.CanAnimateCard() - card={0} FAILED !card.IsActorReady()", new object[]
				{
					card
				});
			}
			return false;
		}
		if (this.m_controller.IsFriendlySide() && TurnStartManager.Get() && TurnStartManager.Get().IsCardDrawHandled(card))
		{
			return false;
		}
		if (this.IsCardNotInEnemyHandAnymore(card))
		{
			if (flag)
			{
				Log.FaceDownCard.Print("ZoneHand.CanAnimateCard() - card={0} FAILED IsCardNotInEnemyHandAnymore()", new object[]
				{
					card
				});
			}
			return false;
		}
		if (card.HasBeenGrabbedByEnemyActionHandler())
		{
			if (flag)
			{
				Log.FaceDownCard.Print("ZoneHand.CanAnimateCard() - card={0} FAILED card.HasBeenGrabbedByEnemyActionHandler()", new object[]
				{
					card
				});
			}
			return false;
		}
		return true;
	}

	// Token: 0x0600320E RID: 12814 RVA: 0x00100805 File Offset: 0x000FEA05
	private bool IsCardNotInEnemyHandAnymore(Card card)
	{
		return card.GetEntity().GetZone() != TAG_ZONE.HAND && this.enemyHand;
	}

	// Token: 0x0600320F RID: 12815 RVA: 0x00100820 File Offset: 0x000FEA20
	private void UpdateCenterAndWidth()
	{
		this.centerOfHand = base.GetComponent<Collider>().bounds.center;
		this.m_maxWidth = base.GetComponent<Collider>().bounds.size.x;
	}

	// Token: 0x06003210 RID: 12816 RVA: 0x00100864 File Offset: 0x000FEA64
	public void OnCardGrabbed(Card card)
	{
		Entity entity = card.GetEntity();
		if (entity == null)
		{
			return;
		}
		Player controller = entity.GetController();
		if (controller == null)
		{
			return;
		}
		if ((controller.HasTag(GAME_TAG.HEALING_DOES_DAMAGE) && card.CanPlayHealingDoesDamageHint()) || (controller.HasTag(GAME_TAG.LIFESTEAL_DAMAGES_OPPOSING_HERO) && card.CanPlayLifestealDoesDamageHint()))
		{
			Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST, true);
			if (actorSpell != null)
			{
				actorSpell.Reactivate();
			}
		}
	}

	// Token: 0x06003211 RID: 12817 RVA: 0x001008CD File Offset: 0x000FEACD
	public void OnCardHeld(Card heldCard)
	{
		if (heldCard == null || heldCard.GetEntity() == null)
		{
			return;
		}
		if (heldCard.GetEntity().HasTag(GAME_TAG.TWINSPELL))
		{
			this.OnTwinspellHeld(heldCard);
		}
	}

	// Token: 0x06003212 RID: 12818 RVA: 0x001008FC File Offset: 0x000FEAFC
	private void OnTwinspellHeld(Card heldCard)
	{
		if (this.m_twinspellHoldSpellInstance == null)
		{
			this.m_twinspellHoldSpellInstance = UnityEngine.Object.Instantiate<TwinspellHoldSpell>(this.m_TwinspellHoldSpell);
			this.m_twinspellHoldSpellInstance.Initialize(heldCard.GetEntity().GetEntityId(), heldCard.GetZonePosition());
		}
		else if (this.m_twinspellHoldSpellInstance.GetOriginalSpellEntityId() != heldCard.GetEntity().GetEntityId() || this.m_twinspellHoldSpellInstance.GetFakeTwinspellZonePosition() != heldCard.GetEntity().GetZonePosition())
		{
			this.m_twinspellHoldSpellInstance.Initialize(heldCard.GetEntity().GetEntityId(), heldCard.GetZonePosition());
		}
		heldCard.GetActor().ToggleForceIdle(true);
		heldCard.UpdateActorState(false);
		SpellUtils.ActivateBirthIfNecessary(this.m_twinspellHoldSpellInstance);
	}

	// Token: 0x06003213 RID: 12819 RVA: 0x001009B4 File Offset: 0x000FEBB4
	public void OnTwinspellPlayed(Card playedCard)
	{
		if (!playedCard.GetEntity().HasTag(GAME_TAG.TWINSPELL))
		{
			return;
		}
		playedCard.GetActor().ToggleForceIdle(false);
		playedCard.UpdateActorState(false);
		this.ReserveCardSlot(this.GetCardSlot(playedCard));
		this.m_playingTwinspellEntityId = playedCard.GetEntity().GetEntityId();
		if (this.m_twinspellHoldSpellInstance != null)
		{
			this.m_twinspellHoldSpellInstance.ActivateState(SpellStateType.ACTION);
		}
	}

	// Token: 0x06003214 RID: 12820 RVA: 0x00100A1F File Offset: 0x000FEC1F
	public void OnTwinspellDropped(Card droppedCard)
	{
		if (!droppedCard.GetEntity().HasTag(GAME_TAG.TWINSPELL))
		{
			return;
		}
		this.ActivateTwinspellSpellDeath();
		droppedCard.GetActor().ToggleForceIdle(false);
		droppedCard.UpdateActorState(false);
	}

	// Token: 0x06003215 RID: 12821 RVA: 0x00100A4D File Offset: 0x000FEC4D
	public void ActivateTwinspellSpellDeath()
	{
		if (this.m_twinspellHoldSpellInstance != null)
		{
			SpellUtils.ActivateDeathIfNecessary(this.m_twinspellHoldSpellInstance);
		}
		this.m_playingTwinspellEntityId = -1;
	}

	// Token: 0x06003216 RID: 12822 RVA: 0x00100A70 File Offset: 0x000FEC70
	public bool IsTwinspellBeingPlayed(Entity twinspellEntity)
	{
		return twinspellEntity != null && twinspellEntity.GetEntityId() == this.m_playingTwinspellEntityId;
	}

	// Token: 0x06003217 RID: 12823 RVA: 0x00100A85 File Offset: 0x000FEC85
	private void OnCantPlay(Entity entity, object userData)
	{
		if (!entity.IsControlledByFriendlySidePlayer())
		{
			return;
		}
		if (entity.IsTwinspell())
		{
			this.ActivateTwinspellSpellDeath();
			this.ClearReservedCard();
		}
	}

	// Token: 0x06003218 RID: 12824 RVA: 0x00100AA4 File Offset: 0x000FECA4
	public override bool AddCard(Card card)
	{
		return base.AddCard(card);
	}

	// Token: 0x04001BA6 RID: 7078
	public GameObject m_iPhoneCardPosition;

	// Token: 0x04001BA7 RID: 7079
	public GameObject m_leftArrow;

	// Token: 0x04001BA8 RID: 7080
	public GameObject m_rightArrow;

	// Token: 0x04001BA9 RID: 7081
	public GameObject m_manaGemPosition;

	// Token: 0x04001BAA RID: 7082
	public ManaCrystalMgr m_manaGemMgr;

	// Token: 0x04001BAB RID: 7083
	public GameObject m_playCardButton;

	// Token: 0x04001BAC RID: 7084
	public GameObject m_iPhonePreviewBone;

	// Token: 0x04001BAD RID: 7085
	public Float_MobileOverride m_SelectCardOffsetZ;

	// Token: 0x04001BAE RID: 7086
	public Float_MobileOverride m_SelectCardScale;

	// Token: 0x04001BAF RID: 7087
	public Float_MobileOverride m_TouchDragResistanceFactorY;

	// Token: 0x04001BB0 RID: 7088
	public TwinspellHoldSpell m_TwinspellHoldSpell;

	// Token: 0x04001BB1 RID: 7089
	public Vector3 m_enlargedHandPosition;

	// Token: 0x04001BB2 RID: 7090
	public Vector3 m_enlargedHandScale;

	// Token: 0x04001BB3 RID: 7091
	public Vector3 m_enlargedHandCardScale;

	// Token: 0x04001BB4 RID: 7092
	public float m_enlargedHandDefaultCardSpacing;

	// Token: 0x04001BB5 RID: 7093
	public float m_enlargedHandCardMinX;

	// Token: 0x04001BB6 RID: 7094
	public float m_enlargedHandCardMaxX;

	// Token: 0x04001BB7 RID: 7095
	public float m_heroWidthInHand;

	// Token: 0x04001BB8 RID: 7096
	public float m_handHidingDistance;

	// Token: 0x04001BB9 RID: 7097
	public GameObject m_heroHitbox;

	// Token: 0x04001BBA RID: 7098
	public const float MOUSE_OVER_SCALE = 1.5f;

	// Token: 0x04001BBB RID: 7099
	public const float HAND_SCALE = 0.62f;

	// Token: 0x04001BBC RID: 7100
	public const float HAND_SCALE_Y = 0.225f;

	// Token: 0x04001BBD RID: 7101
	public const float HAND_SCALE_OPPONENT = 0.682f;

	// Token: 0x04001BBE RID: 7102
	public const float HAND_SCALE_OPPONENT_Y = 0.225f;

	// Token: 0x04001BBF RID: 7103
	private const float ANGLE_OF_CARDS = 40f;

	// Token: 0x04001BC0 RID: 7104
	private const float DEFAULT_ANIMATE_TIME = 0.35f;

	// Token: 0x04001BC1 RID: 7105
	private const float DRIFT_AMOUNT = 0.08f;

	// Token: 0x04001BC2 RID: 7106
	private const float Z_ROTATION_ON_LEFT = 354.5f;

	// Token: 0x04001BC3 RID: 7107
	private const float Z_ROTATION_ON_RIGHT = 3f;

	// Token: 0x04001BC4 RID: 7108
	private const float RESISTANCE_BASE = 10f;

	// Token: 0x04001BC5 RID: 7109
	private Card m_lastMousedOverCard;

	// Token: 0x04001BC6 RID: 7110
	private float m_maxWidth;

	// Token: 0x04001BC7 RID: 7111
	private bool m_doNotUpdateLayout = true;

	// Token: 0x04001BC8 RID: 7112
	private Vector3 centerOfHand;

	// Token: 0x04001BC9 RID: 7113
	private bool enemyHand;

	// Token: 0x04001BCA RID: 7114
	private bool m_handEnlarged;

	// Token: 0x04001BCB RID: 7115
	private Vector3 m_startingPosition;

	// Token: 0x04001BCC RID: 7116
	private Vector3 m_startingScale;

	// Token: 0x04001BCD RID: 7117
	private bool m_handMoving;

	// Token: 0x04001BCE RID: 7118
	private bool m_targetingMode;

	// Token: 0x04001BCF RID: 7119
	private int m_touchedSlot;

	// Token: 0x04001BD0 RID: 7120
	private CardStandIn m_hiddenStandIn;

	// Token: 0x04001BD1 RID: 7121
	private TwinspellHoldSpell m_twinspellHoldSpellInstance;

	// Token: 0x04001BD2 RID: 7122
	private int m_playingTwinspellEntityId = -1;

	// Token: 0x04001BD3 RID: 7123
	private int m_reservedSlot = -1;

	// Token: 0x04001BD4 RID: 7124
	private List<CardStandIn> standIns;

	// Token: 0x04001BD5 RID: 7125
	private bool m_flipHandCards;
}
