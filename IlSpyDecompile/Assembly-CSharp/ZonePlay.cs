using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePlay : Zone
{
	public int m_MaxSlots = 7;

	public MagneticBeamSpell m_MagneticBeamSpell;

	private const float DEFAULT_TRANSITION_TIME = 1f;

	private const float PHONE_CARD_SCALE = 1.15f;

	private float[] PHONE_WIDTH_MODIFIERS = new float[8] { 0.25f, 0.25f, 0.25f, 0.25f, 0.22f, 0.19f, 0.15f, 0.1f };

	private int m_slotMousedOver = -1;

	private float m_slotWidth;

	private float m_transitionTime = 1f;

	private float m_baseTransitionTime = 1f;

	private MagneticBeamSpell m_magneticBeamSpellInstance;

	private Card m_previousHeldCard;

	private void Awake()
	{
		m_slotWidth = GetComponent<Collider>().bounds.size.x / (float)m_MaxSlots;
	}

	public float GetTransitionTime()
	{
		return m_transitionTime;
	}

	public void SetTransitionTime(float transitionTime)
	{
		m_transitionTime = transitionTime;
	}

	public void ResetTransitionTime()
	{
		m_transitionTime = m_baseTransitionTime;
	}

	public void OverrideBaseTransitionTime(float newTransitionTime)
	{
		m_baseTransitionTime = newTransitionTime;
	}

	public void SortWithSpotForHeldCard(int slot)
	{
		m_slotMousedOver = slot;
		UpdateLayout();
	}

	public MagneticBeamSpell GetMagneticBeamSpell()
	{
		return m_MagneticBeamSpell;
	}

	public void OnMagneticHeld(Card heldCard)
	{
		if (m_slotMousedOver < 0 || !heldCard.GetEntity().HasTag(GAME_TAG.MODULAR) || heldCard.GetZone() is ZonePlay)
		{
			return;
		}
		if (m_magneticBeamSpellInstance == null)
		{
			m_magneticBeamSpellInstance = Object.Instantiate(m_MagneticBeamSpell);
		}
		Card card = null;
		List<Card> list = new List<Card>();
		int pos = m_slotMousedOver + 1;
		int num = ZoneMgr.Get().PredictZonePosition(this, pos);
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card2 = m_cards[i];
			Entity entity = card2.GetEntity();
			if (entity.HasTag(GAME_TAG.UNTOUCHABLE) || !entity.HasRace(TAG_RACE.MECHANICAL) || entity.GetRealTimeZone() != TAG_ZONE.PLAY)
			{
				SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED));
				continue;
			}
			list.Add(card2);
			if (entity.GetRealTimeZonePosition() == num)
			{
				card = card2;
			}
		}
		heldCard.GetActor().ToggleForceIdle(bOn: true);
		heldCard.UpdateActorState();
		foreach (Card item in list)
		{
			item.GetActor().ToggleForceIdle(bOn: true);
			item.UpdateActorState();
			if (card == item)
			{
				SpellUtils.ActivateBirthIfNecessary(item.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(item.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(item.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED));
			}
			else if (card == null)
			{
				SpellUtils.ActivateBirthIfNecessary(item.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(item.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(item.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED));
			}
			else
			{
				SpellUtils.ActivateBirthIfNecessary(item.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED));
				SpellUtils.ActivateDeathIfNecessary(item.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(item.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
			}
		}
		if (list.Count > 0)
		{
			if (card != null)
			{
				m_magneticBeamSpellInstance.SetSource(heldCard.gameObject);
				if (m_magneticBeamSpellInstance.GetTarget() != card.gameObject)
				{
					m_magneticBeamSpellInstance.RemoveAllTargets();
					m_magneticBeamSpellInstance.AddTarget(card.gameObject);
				}
				SpellUtils.ActivateBirthIfNecessary(m_magneticBeamSpellInstance);
				SpellUtils.ActivateBirthIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT));
				SpellUtils.ActivateDeathIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED));
			}
			else
			{
				SpellUtils.ActivateDeathIfNecessary(m_magneticBeamSpellInstance);
				SpellUtils.ActivateBirthIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED));
				SpellUtils.ActivateDeathIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT));
			}
		}
		else
		{
			SpellUtils.ActivateDeathIfNecessary(m_magneticBeamSpellInstance);
			SpellUtils.ActivateDeathIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT));
			SpellUtils.ActivateDeathIfNecessary(heldCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED));
		}
	}

	public void OnMagneticPlay(Card playedCard, int zonePos)
	{
		if (!playedCard.GetEntity().HasTag(GAME_TAG.MODULAR))
		{
			if (m_magneticBeamSpellInstance != null)
			{
				SpellUtils.ActivateDeathIfNecessary(m_magneticBeamSpellInstance);
			}
			return;
		}
		Card card = null;
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card2 = m_cards[i];
			Entity entity = card2.GetEntity();
			if (!entity.HasTag(GAME_TAG.UNTOUCHABLE) && entity.HasRace(TAG_RACE.MECHANICAL) && entity.GetRealTimeZone() == TAG_ZONE.PLAY)
			{
				if (card2.GetEntity().GetRealTimeZonePosition() == zonePos)
				{
					card = card2;
					continue;
				}
				card2.GetActor().ToggleForceIdle(bOn: false);
				card2.UpdateActorState();
				SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(card2.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED));
			}
		}
		if (card != null)
		{
			if (m_magneticBeamSpellInstance == null)
			{
				m_magneticBeamSpellInstance = Object.Instantiate(m_MagneticBeamSpell);
			}
			m_magneticBeamSpellInstance.SetSource(playedCard.gameObject);
			m_magneticBeamSpellInstance.RemoveAllTargets();
			m_magneticBeamSpellInstance.AddTarget(card.gameObject);
			MagneticPlayData magneticPlayData = new MagneticPlayData();
			magneticPlayData.m_playedCard = playedCard;
			magneticPlayData.m_targetMech = card;
			magneticPlayData.m_beamSpell = m_magneticBeamSpellInstance;
			playedCard.SetMagneticPlayData(magneticPlayData);
			playedCard.GetActor().ToggleForceIdle(bOn: true);
			playedCard.UpdateActorState();
			card.GetActor().ToggleForceIdle(bOn: true);
			card.UpdateActorState();
			m_magneticBeamSpellInstance = null;
			SpellUtils.ActivateBirthIfNecessary(playedCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT));
			SpellUtils.ActivateBirthIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
			SpellUtils.ActivateDeathIfNecessary(playedCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED));
			SpellUtils.ActivateDeathIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT));
			SpellUtils.ActivateDeathIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED));
		}
		else
		{
			playedCard.GetActor().ToggleForceIdle(bOn: false);
			playedCard.UpdateActorState();
			SpellUtils.ActivateDeathIfNecessary(m_magneticBeamSpellInstance);
			SpellUtils.ActivateDeathIfNecessary(playedCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT));
			SpellUtils.ActivateDeathIfNecessary(playedCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED));
		}
	}

	public void OnMagneticDropped(Card droppedCard)
	{
		if (!droppedCard.GetEntity().HasTag(GAME_TAG.MODULAR))
		{
			return;
		}
		SpellUtils.ActivateDeathIfNecessary(m_magneticBeamSpellInstance);
		SpellUtils.ActivateDeathIfNecessary(droppedCard.GetActorSpell(SpellType.MAGNETIC_HAND_LINKED_RIGHT));
		SpellUtils.ActivateDeathIfNecessary(droppedCard.GetActorSpell(SpellType.MAGNETIC_HAND_UNLINKED));
		droppedCard.GetActor().ToggleForceIdle(bOn: false);
		droppedCard.UpdateActorState();
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			Entity entity = card.GetEntity();
			if (!entity.HasTag(GAME_TAG.UNTOUCHABLE) && entity.HasRace(TAG_RACE.MECHANICAL))
			{
				card.GetActor().ToggleForceIdle(bOn: false);
				card.UpdateActorState();
				SpellUtils.ActivateDeathIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_LINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT));
				SpellUtils.ActivateDeathIfNecessary(card.GetActorSpell(SpellType.MAGNETIC_PLAY_UNLINKED_LEFT_DIMMED));
			}
		}
	}

	public int GetSlotMousedOver()
	{
		return m_slotMousedOver;
	}

	public float GetSlotWidth()
	{
		m_slotWidth = GetComponent<Collider>().bounds.size.x / (float)m_MaxSlots;
		int num = m_cards.Count;
		if (m_slotMousedOver >= 0)
		{
			num++;
		}
		num = Mathf.Clamp(num, 0, m_MaxSlots);
		float num2 = 1f;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			num2 += PHONE_WIDTH_MODIFIERS[num];
		}
		return m_slotWidth * num2;
	}

	public void UnhideCardZzzEffects()
	{
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			if (card.GetEntity().IsAsleep())
			{
				SpellUtils.ActivateBirthIfNecessary(card.GetActorSpell(SpellType.Zzz));
			}
		}
	}

	public void HideCardZzzEffects()
	{
		for (int i = 0; i < m_cards.Count; i++)
		{
			SpellUtils.ActivateDeathIfNecessary(m_cards[i].GetActorSpell(SpellType.Zzz));
		}
	}

	public Vector3 GetCardPosition(Card card)
	{
		int index = m_cards.FindIndex((Card currCard) => currCard == card);
		return GetCardPosition(index);
	}

	public Vector3 GetCardPosition(int index)
	{
		if (index < 0 || index >= m_cards.Count)
		{
			return base.transform.position;
		}
		int num = m_cards.Count;
		if (m_slotMousedOver >= 0)
		{
			num++;
		}
		Vector3 center = GetComponent<Collider>().bounds.center;
		float num2 = 0.5f * GetSlotWidth();
		float num3 = (float)num * num2;
		float num4 = center.x - num3 + num2;
		int num5 = ((m_slotMousedOver >= 0 && index >= m_slotMousedOver) ? 1 : 0);
		for (int i = 0; i < index; i++)
		{
			Card card = m_cards[i];
			if (CanAnimateCard(card))
			{
				num5++;
			}
		}
		return new Vector3(num4 + (float)num5 * GetSlotWidth(), center.y, center.z);
	}

	public override bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		if (!base.CanAcceptTags(controllerId, zoneTag, cardType, entity))
		{
			return false;
		}
		if (cardType == TAG_CARDTYPE.MINION)
		{
			return true;
		}
		return false;
	}

	public override void UpdateLayout()
	{
		m_updatingLayout++;
		if (IsBlockingLayout())
		{
			UpdateLayoutFinished();
			return;
		}
		if (InputManager.Get() != null && InputManager.Get().GetHeldCard() == null)
		{
			m_slotMousedOver = -1;
		}
		int num = 0;
		m_cards.Sort(Zone.CardSortComparison);
		float num2 = 0f;
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			if (!(card == null) && CanAnimateCard(card))
			{
				string tweenName = ZoneMgr.Get().GetTweenName<ZonePlay>();
				if (m_Side == Player.Side.OPPOSING)
				{
					iTween.StopOthersByName(card.gameObject, tweenName);
				}
				Vector3 localScale = base.transform.localScale;
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					localScale *= 1.15f;
				}
				Vector3 cardPosition = GetCardPosition(i);
				float transitionDelay = card.GetTransitionDelay();
				card.SetTransitionDelay(0f);
				ZoneTransitionStyle transitionStyle = card.GetTransitionStyle();
				card.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
				if (transitionStyle == ZoneTransitionStyle.INSTANT)
				{
					card.EnableTransitioningZones(enable: false);
					card.transform.position = cardPosition;
					card.transform.rotation = base.transform.rotation;
					card.transform.localScale = localScale;
					continue;
				}
				card.EnableTransitioningZones(enable: true);
				num++;
				Hashtable args = iTween.Hash("scale", localScale, "delay", transitionDelay, "time", m_transitionTime, "name", tweenName);
				iTween.ScaleTo(card.gameObject, args);
				Hashtable args2 = iTween.Hash("rotation", base.transform.eulerAngles, "delay", transitionDelay, "time", m_transitionTime, "name", tweenName);
				iTween.RotateTo(card.gameObject, args2);
				Hashtable args3 = iTween.Hash("position", cardPosition, "delay", transitionDelay, "time", m_transitionTime, "name", tweenName);
				iTween.MoveTo(card.gameObject, args3);
				num2 = Mathf.Max(num2, transitionDelay + m_transitionTime);
			}
		}
		if (num > 0)
		{
			StartFinishLayoutTimer(num2);
		}
		else
		{
			UpdateLayoutFinished();
		}
	}

	protected bool CanAnimateCard(Card card)
	{
		if (card.IsDoNotSort())
		{
			return false;
		}
		return true;
	}
}
