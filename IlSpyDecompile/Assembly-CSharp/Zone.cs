using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
	public delegate void UpdateLayoutCompleteCallback(Zone zone, object userData);

	protected class UpdateLayoutCompleteListener : EventListener<UpdateLayoutCompleteCallback>
	{
		public void Fire(Zone zone)
		{
			m_callback(zone, m_userData);
		}
	}

	public TAG_ZONE m_ServerTag;

	public Player.Side m_Side;

	public const float TRANSITION_SEC = 1f;

	protected Player m_controller;

	protected List<Card> m_cards = new List<Card>();

	protected bool m_layoutDirty = true;

	protected int m_updatingLayout;

	protected List<UpdateLayoutCompleteListener> m_completeListeners = new List<UpdateLayoutCompleteListener>();

	protected int m_inputBlockerCount;

	protected int m_layoutBlockerCount;

	public override string ToString()
	{
		return string.Format("{1} {0}", m_ServerTag, m_Side);
	}

	public Player GetController()
	{
		return m_controller;
	}

	public int GetControllerId()
	{
		if (m_controller != null)
		{
			return m_controller.GetPlayerId();
		}
		return 0;
	}

	public void SetController(Player controller)
	{
		m_controller = controller;
	}

	public List<Card> GetCards()
	{
		return m_cards;
	}

	public int GetCardCount()
	{
		return m_cards.Count;
	}

	public Card GetFirstCard()
	{
		if (m_cards.Count <= 0)
		{
			return null;
		}
		return m_cards[0];
	}

	public Card GetLastCard()
	{
		if (m_cards.Count <= 0)
		{
			return null;
		}
		return m_cards[m_cards.Count - 1];
	}

	public Card GetCardAtIndex(int index)
	{
		if (index < 0)
		{
			return null;
		}
		if (index >= m_cards.Count)
		{
			return null;
		}
		return m_cards[index];
	}

	public Card GetCardAtPos(int pos)
	{
		return GetCardAtIndex(pos - 1);
	}

	public int GetLastPos()
	{
		return m_cards.Count + 1;
	}

	public int FindCardPos(Card card)
	{
		return 1 + m_cards.FindIndex((Card currCard) => currCard == card);
	}

	public bool ContainsCard(Card card)
	{
		return FindCardPos(card) > 0;
	}

	public bool IsOnlyCard(Card card)
	{
		if (m_cards.Count != 1)
		{
			return false;
		}
		return m_cards[0] == card;
	}

	public void DirtyLayout()
	{
		m_layoutDirty = true;
	}

	public bool IsLayoutDirty()
	{
		return m_layoutDirty;
	}

	public bool IsUpdatingLayout()
	{
		return m_updatingLayout > 0;
	}

	public bool IsInputEnabled()
	{
		return m_inputBlockerCount <= 0;
	}

	public int GetInputBlockerCount()
	{
		return m_inputBlockerCount;
	}

	public void AddInputBlocker()
	{
		AddInputBlocker(1);
	}

	public void RemoveInputBlocker()
	{
		AddInputBlocker(-1);
	}

	public void BlockInput(bool block)
	{
		int count = (block ? 1 : (-1));
		AddInputBlocker(count);
	}

	public void AddInputBlocker(int count)
	{
		int inputBlockerCount = m_inputBlockerCount;
		m_inputBlockerCount += count;
		if (inputBlockerCount != m_inputBlockerCount && inputBlockerCount * m_inputBlockerCount == 0)
		{
			UpdateInput();
		}
	}

	public bool IsBlockingLayout()
	{
		return m_layoutBlockerCount > 0;
	}

	public int GetLayoutBlockerCount()
	{
		return m_layoutBlockerCount;
	}

	public void AddLayoutBlocker()
	{
		m_layoutBlockerCount++;
	}

	public void RemoveLayoutBlocker()
	{
		m_layoutBlockerCount--;
	}

	public bool AddUpdateLayoutCompleteCallback(UpdateLayoutCompleteCallback callback)
	{
		return AddUpdateLayoutCompleteCallback(callback, null);
	}

	public bool AddUpdateLayoutCompleteCallback(UpdateLayoutCompleteCallback callback, object userData)
	{
		UpdateLayoutCompleteListener updateLayoutCompleteListener = new UpdateLayoutCompleteListener();
		updateLayoutCompleteListener.SetCallback(callback);
		updateLayoutCompleteListener.SetUserData(userData);
		if (m_completeListeners.Contains(updateLayoutCompleteListener))
		{
			return false;
		}
		m_completeListeners.Add(updateLayoutCompleteListener);
		return true;
	}

	public bool RemoveUpdateLayoutCompleteCallback(UpdateLayoutCompleteCallback callback)
	{
		return RemoveUpdateLayoutCompleteCallback(callback, null);
	}

	public bool RemoveUpdateLayoutCompleteCallback(UpdateLayoutCompleteCallback callback, object userData)
	{
		UpdateLayoutCompleteListener updateLayoutCompleteListener = new UpdateLayoutCompleteListener();
		updateLayoutCompleteListener.SetCallback(callback);
		updateLayoutCompleteListener.SetUserData(userData);
		return m_completeListeners.Remove(updateLayoutCompleteListener);
	}

	public virtual bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		if (m_ServerTag != zoneTag)
		{
			return false;
		}
		if (m_controller != null && m_controller.GetPlayerId() != controllerId)
		{
			return false;
		}
		if (cardType == TAG_CARDTYPE.ENCHANTMENT)
		{
			return false;
		}
		return true;
	}

	public virtual bool AddCard(Card card)
	{
		m_cards.Add(card);
		DirtyLayout();
		return true;
	}

	public virtual bool InsertCard(int index, Card card)
	{
		m_cards.Insert(index, card);
		DirtyLayout();
		return true;
	}

	public virtual int RemoveCard(Card card)
	{
		for (int i = 0; i < m_cards.Count; i++)
		{
			if (m_cards[i] == card)
			{
				m_cards.RemoveAt(i);
				DirtyLayout();
				return i;
			}
		}
		if (!GameState.Get().EntityRemovedFromGame(card.GetEntity().GetEntityId()))
		{
			Debug.LogWarning($"{this}.RemoveCard() - FAILED: {m_controller} tried to remove {card}");
		}
		return -1;
	}

	public virtual void Reset()
	{
		m_cards.Clear();
		m_inputBlockerCount = 0;
		UpdateInput();
	}

	public virtual Transform GetZoneTransformForCard(Card card)
	{
		return base.transform;
	}

	public virtual void UpdateLayout()
	{
		if (m_cards.Count == 0)
		{
			UpdateLayoutFinished();
			return;
		}
		if (GameState.Get().IsMulliganManagerActive())
		{
			UpdateLayoutFinished();
			return;
		}
		m_updatingLayout++;
		if (IsBlockingLayout())
		{
			UpdateLayoutFinished();
			return;
		}
		for (int i = 0; i < m_cards.Count; i++)
		{
			Card card = m_cards[i];
			if (!card.IsDoNotSort())
			{
				card.ShowCard();
				card.EnableTransitioningZones(enable: true);
				Transform zoneTransformForCard = GetZoneTransformForCard(card);
				iTween.MoveTo(card.gameObject, zoneTransformForCard.position, 1f);
				iTween.RotateTo(card.gameObject, zoneTransformForCard.localEulerAngles, 1f);
				iTween.ScaleTo(card.gameObject, zoneTransformForCard.localScale, 1f);
			}
		}
		StartFinishLayoutTimer(1f);
	}

	public static int CardSortComparison(Card card1, Card card2)
	{
		int zonePosition = card1.GetZonePosition();
		int zonePosition2 = card2.GetZonePosition();
		if (zonePosition != zonePosition2)
		{
			return zonePosition - zonePosition2;
		}
		zonePosition = card1.GetEntity().GetZonePosition();
		zonePosition2 = card2.GetEntity().GetZonePosition();
		return zonePosition - zonePosition2;
	}

	public virtual void OnHealingDoesDamageEntityMousedOver()
	{
		if (TargetReticleManager.Get().IsActive())
		{
			return;
		}
		foreach (Card card in m_cards)
		{
			if (card.CanPlayHealingDoesDamageHint())
			{
				Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
				Spell actorSpell2 = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_IDLE);
				if (actorSpell2 != null)
				{
					actorSpell2.ActivateState(SpellStateType.BIRTH);
				}
			}
		}
	}

	public virtual void OnHealingDoesDamageEntityMousedOut()
	{
		foreach (Card card in m_cards)
		{
			if (!card.GetEntity().HasTag(GAME_TAG.HEALING_DOES_DAMAGE_HINT))
			{
				Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_IDLE);
				if (!(actorSpell == null) && actorSpell.IsActive())
				{
					actorSpell.ActivateState(SpellStateType.DEATH);
				}
			}
		}
	}

	public virtual void OnHealingDoesDamageEntityEnteredPlay()
	{
		foreach (Card card in m_cards)
		{
			if (card.CanPlayHealingDoesDamageHint())
			{
				Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
			}
		}
	}

	public virtual void OnLifestealDoesDamageEntityMousedOver()
	{
		if (TargetReticleManager.Get().IsActive())
		{
			return;
		}
		foreach (Card card in m_cards)
		{
			if (card.CanPlayLifestealDoesDamageHint())
			{
				Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
				Spell actorSpell2 = card.GetActorSpell(SpellType.LIFESTEAL_DOES_DAMAGE_HINT_IDLE);
				if (actorSpell2 != null)
				{
					actorSpell2.ActivateState(SpellStateType.BIRTH);
				}
			}
		}
	}

	public virtual void OnLifestealDoesDamageEntityMousedOut()
	{
		foreach (Card card in m_cards)
		{
			if (!card.GetEntity().HasTag(GAME_TAG.LIFESTEAL_DOES_DAMAGE_HINT))
			{
				Spell actorSpell = card.GetActorSpell(SpellType.LIFESTEAL_DOES_DAMAGE_HINT_IDLE);
				if (!(actorSpell == null) && actorSpell.IsActive())
				{
					actorSpell.ActivateState(SpellStateType.DEATH);
				}
			}
		}
	}

	public virtual void OnLifestealDoesDamageEntityEnteredPlay()
	{
		foreach (Card card in m_cards)
		{
			if (card.CanPlayLifestealDoesDamageHint())
			{
				Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
			}
		}
	}

	public virtual void OnSpellPowerEntityEnteredPlay(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
	}

	public virtual void OnSpellPowerEntityMousedOver(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
	}

	public virtual void OnSpellPowerEntityMousedOut(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
	}

	protected void UpdateInput()
	{
		bool flag = IsInputEnabled();
		foreach (Card card in m_cards)
		{
			Actor actor = card.GetActor();
			if (!(actor == null))
			{
				actor.ToggleForceIdle(!flag);
				actor.ToggleCollider(flag);
				card.UpdateActorState();
			}
		}
		Card mousedOverCard = InputManager.Get().GetMousedOverCard();
		if (flag && m_cards.Contains(mousedOverCard))
		{
			mousedOverCard.UpdateProposedManaUsage();
		}
	}

	protected void StartFinishLayoutTimer(float delaySec)
	{
		if (delaySec <= Mathf.Epsilon)
		{
			UpdateLayoutFinished();
			return;
		}
		if (m_cards.Find((Card card) => card.IsTransitioningZones()) == null)
		{
			UpdateLayoutFinished();
			return;
		}
		Hashtable args = iTween.Hash("time", delaySec, "oncomplete", "UpdateLayoutFinished", "oncompletetarget", base.gameObject);
		iTween.Timer(base.gameObject, args);
	}

	protected void UpdateLayoutFinished()
	{
		for (int i = 0; i < m_cards.Count; i++)
		{
			m_cards[i].EnableTransitioningZones(enable: false);
		}
		m_updatingLayout--;
		m_layoutDirty = false;
		FireUpdateLayoutCompleteCallbacks();
	}

	protected void FireUpdateLayoutCompleteCallbacks()
	{
		if (m_completeListeners.Count != 0)
		{
			UpdateLayoutCompleteListener[] array = m_completeListeners.ToArray();
			m_completeListeners.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(this);
			}
		}
	}
}
