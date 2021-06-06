using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000364 RID: 868
public class ZonePlay : Zone
{
	// Token: 0x060032E8 RID: 13032 RVA: 0x00104DD8 File Offset: 0x00102FD8
	private void Awake()
	{
		this.m_slotWidth = base.GetComponent<Collider>().bounds.size.x / (float)this.m_MaxSlots;
	}

	// Token: 0x060032E9 RID: 13033 RVA: 0x00104E0B File Offset: 0x0010300B
	public float GetTransitionTime()
	{
		return this.m_transitionTime;
	}

	// Token: 0x060032EA RID: 13034 RVA: 0x00104E13 File Offset: 0x00103013
	public void SetTransitionTime(float transitionTime)
	{
		this.m_transitionTime = transitionTime;
	}

	// Token: 0x060032EB RID: 13035 RVA: 0x00104E1C File Offset: 0x0010301C
	public void ResetTransitionTime()
	{
		this.m_transitionTime = this.m_baseTransitionTime;
	}

	// Token: 0x060032EC RID: 13036 RVA: 0x00104E2A File Offset: 0x0010302A
	public void OverrideBaseTransitionTime(float newTransitionTime)
	{
		this.m_baseTransitionTime = newTransitionTime;
	}

	// Token: 0x060032ED RID: 13037 RVA: 0x00104E33 File Offset: 0x00103033
	public void SortWithSpotForHeldCard(int slot)
	{
		this.m_slotMousedOver = slot;
		this.UpdateLayout();
	}

	// Token: 0x060032EE RID: 13038 RVA: 0x00104E42 File Offset: 0x00103042
	public MagneticBeamSpell GetMagneticBeamSpell()
	{
		return this.m_MagneticBeamSpell;
	}

	// Token: 0x060032EF RID: 13039 RVA: 0x00104E4C File Offset: 0x0010304C
	public void OnMagneticHeld(Card heldCard)
	{
		if (this.m_slotMousedOver < 0)
		{
			return;
		}
		if (!heldCard.GetEntity().HasTag(GAME_TAG.MODULAR))
		{
			return;
		}
		if (heldCard.GetZone() is ZonePlay)
		{
			return;
		}
		if (this.m_magneticBeamSpellInstance == null)
		{
			this.m_magneticBeamSpellInstance = UnityEngine.Object.Instantiate<MagneticBeamSpell>(this.m_MagneticBeamSpell);
		}
		Card card = null;
		List<Card> list = new List<Card>();
		int pos = this.m_slotMousedOver + 1;
		int num = ZoneMgr.Get().PredictZonePosition(this, pos);
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card2 = this.m_cards[i];
			Entity entity = card2.GetEntity();
			if (entity.HasTag(GAME_TAG.UNTOUCHABLE) || !entity.HasRace(TAG_RACE.MECHANICAL) || entity.GetRealTimeZone() != TAG_ZONE.PLAY)
			{
				SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
				SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT, true));
				SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED, true));
			}
			else
			{
				list.Add(card2);
				if (entity.GetRealTimeZonePosition() == num)
				{
					card = card2;
				}
			}
		}
		heldCard.GetActor().ToggleForceIdle(true);
		heldCard.UpdateActorState(false);
		foreach (Card card3 in list)
		{
			card3.GetActor().ToggleForceIdle(true);
			card3.UpdateActorState(false);
			if (card == card3)
			{
				SpellUtils.ActivateBirthIfNecessary(card3.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
				SpellUtils.ActivateDeathIfNecessary(card3.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT, true));
				SpellUtils.ActivateDeathIfNecessary(card3.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED, true));
			}
			else if (card == null)
			{
				SpellUtils.ActivateBirthIfNecessary(card3.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT, true));
				SpellUtils.ActivateDeathIfNecessary(card3.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
				SpellUtils.ActivateDeathIfNecessary(card3.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED, true));
			}
			else
			{
				SpellUtils.ActivateBirthIfNecessary(card3.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED, true));
				SpellUtils.ActivateDeathIfNecessary(card3.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT, true));
				SpellUtils.ActivateDeathIfNecessary(card3.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
			}
		}
		if (list.Count <= 0)
		{
			SpellUtils.ActivateDeathIfNecessary(this.m_magneticBeamSpellInstance);
			SpellUtils.ActivateDeathIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT, true));
			SpellUtils.ActivateDeathIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED, true));
			return;
		}
		if (card != null)
		{
			this.m_magneticBeamSpellInstance.SetSource(heldCard.gameObject);
			if (this.m_magneticBeamSpellInstance.GetTarget() != card.gameObject)
			{
				this.m_magneticBeamSpellInstance.RemoveAllTargets();
				this.m_magneticBeamSpellInstance.AddTarget(card.gameObject);
			}
			SpellUtils.ActivateBirthIfNecessary(this.m_magneticBeamSpellInstance);
			SpellUtils.ActivateBirthIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT, true));
			SpellUtils.ActivateDeathIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED, true));
			return;
		}
		SpellUtils.ActivateDeathIfNecessary(this.m_magneticBeamSpellInstance);
		SpellUtils.ActivateBirthIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED, true));
		SpellUtils.ActivateDeathIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT, true));
	}

	// Token: 0x060032F0 RID: 13040 RVA: 0x00105184 File Offset: 0x00103384
	public void OnMagneticPlay(Card playedCard, int zonePos)
	{
		if (!playedCard.GetEntity().HasTag(GAME_TAG.MODULAR))
		{
			if (this.m_magneticBeamSpellInstance != null)
			{
				SpellUtils.ActivateDeathIfNecessary(this.m_magneticBeamSpellInstance);
			}
			return;
		}
		Card card = null;
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card2 = this.m_cards[i];
			Entity entity = card2.GetEntity();
			if (!entity.HasTag(GAME_TAG.UNTOUCHABLE) && entity.HasRace(TAG_RACE.MECHANICAL) && entity.GetRealTimeZone() == TAG_ZONE.PLAY)
			{
				if (card2.GetEntity().GetRealTimeZonePosition() == zonePos)
				{
					card = card2;
				}
				else
				{
					card2.GetActor().ToggleForceIdle(false);
					card2.UpdateActorState(false);
					SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
					SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT, true));
					SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED, true));
				}
			}
		}
		if (card != null)
		{
			if (this.m_magneticBeamSpellInstance == null)
			{
				this.m_magneticBeamSpellInstance = UnityEngine.Object.Instantiate<MagneticBeamSpell>(this.m_MagneticBeamSpell);
			}
			this.m_magneticBeamSpellInstance.SetSource(playedCard.gameObject);
			this.m_magneticBeamSpellInstance.RemoveAllTargets();
			this.m_magneticBeamSpellInstance.AddTarget(card.gameObject);
			playedCard.SetMagneticPlayData(new MagneticPlayData
			{
				m_playedCard = playedCard,
				m_targetMech = card,
				m_beamSpell = this.m_magneticBeamSpellInstance
			});
			playedCard.GetActor().ToggleForceIdle(true);
			playedCard.UpdateActorState(false);
			card.GetActor().ToggleForceIdle(true);
			card.UpdateActorState(false);
			this.m_magneticBeamSpellInstance = null;
			SpellUtils.ActivateBirthIfNecessary(playedCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT, true));
			SpellUtils.ActivateBirthIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
			SpellUtils.ActivateDeathIfNecessary(playedCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED, true));
			SpellUtils.ActivateDeathIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT, true));
			SpellUtils.ActivateDeathIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED, true));
			return;
		}
		playedCard.GetActor().ToggleForceIdle(false);
		playedCard.UpdateActorState(false);
		SpellUtils.ActivateDeathIfNecessary(this.m_magneticBeamSpellInstance);
		SpellUtils.ActivateDeathIfNecessary(playedCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT, true));
		SpellUtils.ActivateDeathIfNecessary(playedCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED, true));
	}

	// Token: 0x060032F1 RID: 13041 RVA: 0x001053BC File Offset: 0x001035BC
	public void OnMagneticDropped(Card droppedCard)
	{
		if (!droppedCard.GetEntity().HasTag(GAME_TAG.MODULAR))
		{
			return;
		}
		SpellUtils.ActivateDeathIfNecessary(this.m_magneticBeamSpellInstance);
		SpellUtils.ActivateDeathIfNecessary(droppedCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT, true));
		SpellUtils.ActivateDeathIfNecessary(droppedCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED, true));
		droppedCard.GetActor().ToggleForceIdle(false);
		droppedCard.UpdateActorState(false);
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			Entity entity = card.GetEntity();
			if (!entity.HasTag(GAME_TAG.UNTOUCHABLE) && entity.HasRace(TAG_RACE.MECHANICAL))
			{
				card.GetActor().ToggleForceIdle(false);
				card.UpdateActorState(false);
				SpellUtils.ActivateDeathIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT, true));
				SpellUtils.ActivateDeathIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT, true));
				SpellUtils.ActivateDeathIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED, true));
			}
		}
	}

	// Token: 0x060032F2 RID: 13042 RVA: 0x001054AC File Offset: 0x001036AC
	public int GetSlotMousedOver()
	{
		return this.m_slotMousedOver;
	}

	// Token: 0x060032F3 RID: 13043 RVA: 0x001054B4 File Offset: 0x001036B4
	public float GetSlotWidth()
	{
		this.m_slotWidth = base.GetComponent<Collider>().bounds.size.x / (float)this.m_MaxSlots;
		int num = this.m_cards.Count;
		if (this.m_slotMousedOver >= 0)
		{
			num++;
		}
		num = Mathf.Clamp(num, 0, this.m_MaxSlots);
		float num2 = 1f;
		if (UniversalInputManager.UsePhoneUI)
		{
			num2 += this.PHONE_WIDTH_MODIFIERS[num];
		}
		return this.m_slotWidth * num2;
	}

	// Token: 0x060032F4 RID: 13044 RVA: 0x00105534 File Offset: 0x00103734
	public void UnhideCardZzzEffects()
	{
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			if (card.GetEntity().IsAsleep())
			{
				SpellUtils.ActivateBirthIfNecessary(card.GetActorSpell(SpellType.Zzz, true));
			}
		}
	}

	// Token: 0x060032F5 RID: 13045 RVA: 0x00105580 File Offset: 0x00103780
	public void HideCardZzzEffects()
	{
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			SpellUtils.ActivateDeathIfNecessary(this.m_cards[i].GetActorSpell(SpellType.Zzz, true));
		}
	}

	// Token: 0x060032F6 RID: 13046 RVA: 0x001055C0 File Offset: 0x001037C0
	public Vector3 GetCardPosition(Card card)
	{
		int index = this.m_cards.FindIndex((Card currCard) => currCard == card);
		return this.GetCardPosition(index);
	}

	// Token: 0x060032F7 RID: 13047 RVA: 0x001055FC File Offset: 0x001037FC
	public Vector3 GetCardPosition(int index)
	{
		if (index < 0 || index >= this.m_cards.Count)
		{
			return base.transform.position;
		}
		int num = this.m_cards.Count;
		if (this.m_slotMousedOver >= 0)
		{
			num++;
		}
		Vector3 center = base.GetComponent<Collider>().bounds.center;
		float num2 = 0.5f * this.GetSlotWidth();
		float num3 = (float)num * num2;
		float num4 = center.x - num3 + num2;
		int num5 = (this.m_slotMousedOver >= 0 && index >= this.m_slotMousedOver) ? 1 : 0;
		for (int i = 0; i < index; i++)
		{
			Card card = this.m_cards[i];
			if (this.CanAnimateCard(card))
			{
				num5++;
			}
		}
		return new Vector3(num4 + (float)num5 * this.GetSlotWidth(), center.y, center.z);
	}

	// Token: 0x060032F8 RID: 13048 RVA: 0x001056D8 File Offset: 0x001038D8
	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		return base.CanAcceptTags(controllerId, zoneTag, cardType, entity) && cardType == TAG_CARDTYPE.MINION;
	}

	// Token: 0x060032F9 RID: 13049 RVA: 0x001056F0 File Offset: 0x001038F0
	public override void UpdateLayout()
	{
		this.m_updatingLayout++;
		if (base.IsBlockingLayout())
		{
			base.UpdateLayoutFinished();
			return;
		}
		if (InputManager.Get() != null && InputManager.Get().GetHeldCard() == null)
		{
			this.m_slotMousedOver = -1;
		}
		int num = 0;
		this.m_cards.Sort(new Comparison<Card>(Zone.CardSortComparison));
		float num2 = 0f;
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			if (!(card == null) && this.CanAnimateCard(card))
			{
				string tweenName = ZoneMgr.Get().GetTweenName<ZonePlay>();
				if (this.m_Side == Player.Side.OPPOSING)
				{
					iTween.StopOthersByName(card.gameObject, tweenName, false);
				}
				Vector3 vector = base.transform.localScale;
				if (UniversalInputManager.UsePhoneUI)
				{
					vector *= 1.15f;
				}
				Vector3 cardPosition = this.GetCardPosition(i);
				float transitionDelay = card.GetTransitionDelay();
				card.SetTransitionDelay(0f);
				int transitionStyle = (int)card.GetTransitionStyle();
				card.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
				if (transitionStyle == 3)
				{
					card.EnableTransitioningZones(false);
					card.transform.position = cardPosition;
					card.transform.rotation = base.transform.rotation;
					card.transform.localScale = vector;
				}
				else
				{
					card.EnableTransitioningZones(true);
					num++;
					Hashtable args = iTween.Hash(new object[]
					{
						"scale",
						vector,
						"delay",
						transitionDelay,
						"time",
						this.m_transitionTime,
						"name",
						tweenName
					});
					iTween.ScaleTo(card.gameObject, args);
					Hashtable args2 = iTween.Hash(new object[]
					{
						"rotation",
						base.transform.eulerAngles,
						"delay",
						transitionDelay,
						"time",
						this.m_transitionTime,
						"name",
						tweenName
					});
					iTween.RotateTo(card.gameObject, args2);
					Hashtable args3 = iTween.Hash(new object[]
					{
						"position",
						cardPosition,
						"delay",
						transitionDelay,
						"time",
						this.m_transitionTime,
						"name",
						tweenName
					});
					iTween.MoveTo(card.gameObject, args3);
					num2 = Mathf.Max(num2, transitionDelay + this.m_transitionTime);
				}
			}
		}
		if (num > 0)
		{
			base.StartFinishLayoutTimer(num2);
			return;
		}
		base.UpdateLayoutFinished();
	}

	// Token: 0x060032FA RID: 13050 RVA: 0x001059AB File Offset: 0x00103BAB
	protected bool CanAnimateCard(Card card)
	{
		return !card.IsDoNotSort();
	}

	// Token: 0x04001C01 RID: 7169
	public int m_MaxSlots = 7;

	// Token: 0x04001C02 RID: 7170
	public MagneticBeamSpell m_MagneticBeamSpell;

	// Token: 0x04001C03 RID: 7171
	private const float DEFAULT_TRANSITION_TIME = 1f;

	// Token: 0x04001C04 RID: 7172
	private const float PHONE_CARD_SCALE = 1.15f;

	// Token: 0x04001C05 RID: 7173
	private float[] PHONE_WIDTH_MODIFIERS = new float[]
	{
		0.25f,
		0.25f,
		0.25f,
		0.25f,
		0.22f,
		0.19f,
		0.15f,
		0.1f
	};

	// Token: 0x04001C06 RID: 7174
	private int m_slotMousedOver = -1;

	// Token: 0x04001C07 RID: 7175
	private float m_slotWidth;

	// Token: 0x04001C08 RID: 7176
	private float m_transitionTime = 1f;

	// Token: 0x04001C09 RID: 7177
	private float m_baseTransitionTime = 1f;

	// Token: 0x04001C0A RID: 7178
	private MagneticBeamSpell m_magneticBeamSpellInstance;

	// Token: 0x04001C0B RID: 7179
	private Card m_previousHeldCard;
}
