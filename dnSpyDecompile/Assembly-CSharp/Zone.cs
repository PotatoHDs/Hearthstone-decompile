using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000357 RID: 855
public class Zone : MonoBehaviour
{
	// Token: 0x06003186 RID: 12678 RVA: 0x000FDFE2 File Offset: 0x000FC1E2
	public override string ToString()
	{
		return string.Format("{1} {0}", this.m_ServerTag, this.m_Side);
	}

	// Token: 0x06003187 RID: 12679 RVA: 0x000FE004 File Offset: 0x000FC204
	public Player GetController()
	{
		return this.m_controller;
	}

	// Token: 0x06003188 RID: 12680 RVA: 0x000FE00C File Offset: 0x000FC20C
	public int GetControllerId()
	{
		if (this.m_controller != null)
		{
			return this.m_controller.GetPlayerId();
		}
		return 0;
	}

	// Token: 0x06003189 RID: 12681 RVA: 0x000FE023 File Offset: 0x000FC223
	public void SetController(Player controller)
	{
		this.m_controller = controller;
	}

	// Token: 0x0600318A RID: 12682 RVA: 0x000FE02C File Offset: 0x000FC22C
	public List<Card> GetCards()
	{
		return this.m_cards;
	}

	// Token: 0x0600318B RID: 12683 RVA: 0x000FE034 File Offset: 0x000FC234
	public int GetCardCount()
	{
		return this.m_cards.Count;
	}

	// Token: 0x0600318C RID: 12684 RVA: 0x000FE041 File Offset: 0x000FC241
	public Card GetFirstCard()
	{
		if (this.m_cards.Count <= 0)
		{
			return null;
		}
		return this.m_cards[0];
	}

	// Token: 0x0600318D RID: 12685 RVA: 0x000FE05F File Offset: 0x000FC25F
	public Card GetLastCard()
	{
		if (this.m_cards.Count <= 0)
		{
			return null;
		}
		return this.m_cards[this.m_cards.Count - 1];
	}

	// Token: 0x0600318E RID: 12686 RVA: 0x000FE089 File Offset: 0x000FC289
	public Card GetCardAtIndex(int index)
	{
		if (index < 0)
		{
			return null;
		}
		if (index >= this.m_cards.Count)
		{
			return null;
		}
		return this.m_cards[index];
	}

	// Token: 0x0600318F RID: 12687 RVA: 0x000FE0AD File Offset: 0x000FC2AD
	public Card GetCardAtPos(int pos)
	{
		return this.GetCardAtIndex(pos - 1);
	}

	// Token: 0x06003190 RID: 12688 RVA: 0x000FE0B8 File Offset: 0x000FC2B8
	public int GetLastPos()
	{
		return this.m_cards.Count + 1;
	}

	// Token: 0x06003191 RID: 12689 RVA: 0x000FE0C8 File Offset: 0x000FC2C8
	public int FindCardPos(Card card)
	{
		return 1 + this.m_cards.FindIndex((Card currCard) => currCard == card);
	}

	// Token: 0x06003192 RID: 12690 RVA: 0x000FE0FB File Offset: 0x000FC2FB
	public bool ContainsCard(Card card)
	{
		return this.FindCardPos(card) > 0;
	}

	// Token: 0x06003193 RID: 12691 RVA: 0x000FE107 File Offset: 0x000FC307
	public bool IsOnlyCard(Card card)
	{
		return this.m_cards.Count == 1 && this.m_cards[0] == card;
	}

	// Token: 0x06003194 RID: 12692 RVA: 0x000FE12B File Offset: 0x000FC32B
	public void DirtyLayout()
	{
		this.m_layoutDirty = true;
	}

	// Token: 0x06003195 RID: 12693 RVA: 0x000FE134 File Offset: 0x000FC334
	public bool IsLayoutDirty()
	{
		return this.m_layoutDirty;
	}

	// Token: 0x06003196 RID: 12694 RVA: 0x000FE13C File Offset: 0x000FC33C
	public bool IsUpdatingLayout()
	{
		return this.m_updatingLayout > 0;
	}

	// Token: 0x06003197 RID: 12695 RVA: 0x000FE147 File Offset: 0x000FC347
	public bool IsInputEnabled()
	{
		return this.m_inputBlockerCount <= 0;
	}

	// Token: 0x06003198 RID: 12696 RVA: 0x000FE155 File Offset: 0x000FC355
	public int GetInputBlockerCount()
	{
		return this.m_inputBlockerCount;
	}

	// Token: 0x06003199 RID: 12697 RVA: 0x000FE15D File Offset: 0x000FC35D
	public void AddInputBlocker()
	{
		this.AddInputBlocker(1);
	}

	// Token: 0x0600319A RID: 12698 RVA: 0x000FE166 File Offset: 0x000FC366
	public void RemoveInputBlocker()
	{
		this.AddInputBlocker(-1);
	}

	// Token: 0x0600319B RID: 12699 RVA: 0x000FE170 File Offset: 0x000FC370
	public void BlockInput(bool block)
	{
		int count = block ? 1 : -1;
		this.AddInputBlocker(count);
	}

	// Token: 0x0600319C RID: 12700 RVA: 0x000FE18C File Offset: 0x000FC38C
	public void AddInputBlocker(int count)
	{
		int inputBlockerCount = this.m_inputBlockerCount;
		this.m_inputBlockerCount += count;
		if (inputBlockerCount != this.m_inputBlockerCount && inputBlockerCount * this.m_inputBlockerCount == 0)
		{
			this.UpdateInput();
		}
	}

	// Token: 0x0600319D RID: 12701 RVA: 0x000FE1C7 File Offset: 0x000FC3C7
	public bool IsBlockingLayout()
	{
		return this.m_layoutBlockerCount > 0;
	}

	// Token: 0x0600319E RID: 12702 RVA: 0x000FE1D2 File Offset: 0x000FC3D2
	public int GetLayoutBlockerCount()
	{
		return this.m_layoutBlockerCount;
	}

	// Token: 0x0600319F RID: 12703 RVA: 0x000FE1DA File Offset: 0x000FC3DA
	public void AddLayoutBlocker()
	{
		this.m_layoutBlockerCount++;
	}

	// Token: 0x060031A0 RID: 12704 RVA: 0x000FE1EA File Offset: 0x000FC3EA
	public void RemoveLayoutBlocker()
	{
		this.m_layoutBlockerCount--;
	}

	// Token: 0x060031A1 RID: 12705 RVA: 0x000FE1FA File Offset: 0x000FC3FA
	public bool AddUpdateLayoutCompleteCallback(Zone.UpdateLayoutCompleteCallback callback)
	{
		return this.AddUpdateLayoutCompleteCallback(callback, null);
	}

	// Token: 0x060031A2 RID: 12706 RVA: 0x000FE204 File Offset: 0x000FC404
	public bool AddUpdateLayoutCompleteCallback(Zone.UpdateLayoutCompleteCallback callback, object userData)
	{
		Zone.UpdateLayoutCompleteListener updateLayoutCompleteListener = new Zone.UpdateLayoutCompleteListener();
		updateLayoutCompleteListener.SetCallback(callback);
		updateLayoutCompleteListener.SetUserData(userData);
		if (this.m_completeListeners.Contains(updateLayoutCompleteListener))
		{
			return false;
		}
		this.m_completeListeners.Add(updateLayoutCompleteListener);
		return true;
	}

	// Token: 0x060031A3 RID: 12707 RVA: 0x000FE242 File Offset: 0x000FC442
	public bool RemoveUpdateLayoutCompleteCallback(Zone.UpdateLayoutCompleteCallback callback)
	{
		return this.RemoveUpdateLayoutCompleteCallback(callback, null);
	}

	// Token: 0x060031A4 RID: 12708 RVA: 0x000FE24C File Offset: 0x000FC44C
	public bool RemoveUpdateLayoutCompleteCallback(Zone.UpdateLayoutCompleteCallback callback, object userData)
	{
		Zone.UpdateLayoutCompleteListener updateLayoutCompleteListener = new Zone.UpdateLayoutCompleteListener();
		updateLayoutCompleteListener.SetCallback(callback);
		updateLayoutCompleteListener.SetUserData(userData);
		return this.m_completeListeners.Remove(updateLayoutCompleteListener);
	}

	// Token: 0x060031A5 RID: 12709 RVA: 0x000FE279 File Offset: 0x000FC479
	public virtual bool CanAcceptTags(int controllerId, TAG_ZONE zoneTag, TAG_CARDTYPE cardType, Entity entity)
	{
		return this.m_ServerTag == zoneTag && (this.m_controller == null || this.m_controller.GetPlayerId() == controllerId) && cardType != TAG_CARDTYPE.ENCHANTMENT;
	}

	// Token: 0x060031A6 RID: 12710 RVA: 0x000FE2A5 File Offset: 0x000FC4A5
	public virtual bool AddCard(Card card)
	{
		this.m_cards.Add(card);
		this.DirtyLayout();
		return true;
	}

	// Token: 0x060031A7 RID: 12711 RVA: 0x000FE2BA File Offset: 0x000FC4BA
	public virtual bool InsertCard(int index, Card card)
	{
		this.m_cards.Insert(index, card);
		this.DirtyLayout();
		return true;
	}

	// Token: 0x060031A8 RID: 12712 RVA: 0x000FE2D0 File Offset: 0x000FC4D0
	public virtual int RemoveCard(Card card)
	{
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			if (this.m_cards[i] == card)
			{
				this.m_cards.RemoveAt(i);
				this.DirtyLayout();
				return i;
			}
		}
		if (!GameState.Get().EntityRemovedFromGame(card.GetEntity().GetEntityId()))
		{
			Debug.LogWarning(string.Format("{0}.RemoveCard() - FAILED: {1} tried to remove {2}", this, this.m_controller, card));
		}
		return -1;
	}

	// Token: 0x060031A9 RID: 12713 RVA: 0x000FE34A File Offset: 0x000FC54A
	public virtual void Reset()
	{
		this.m_cards.Clear();
		this.m_inputBlockerCount = 0;
		this.UpdateInput();
	}

	// Token: 0x060031AA RID: 12714 RVA: 0x0003678E File Offset: 0x0003498E
	public virtual Transform GetZoneTransformForCard(Card card)
	{
		return base.transform;
	}

	// Token: 0x060031AB RID: 12715 RVA: 0x000FE364 File Offset: 0x000FC564
	public virtual void UpdateLayout()
	{
		if (this.m_cards.Count == 0)
		{
			this.UpdateLayoutFinished();
			return;
		}
		if (GameState.Get().IsMulliganManagerActive())
		{
			this.UpdateLayoutFinished();
			return;
		}
		this.m_updatingLayout++;
		if (this.IsBlockingLayout())
		{
			this.UpdateLayoutFinished();
			return;
		}
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			Card card = this.m_cards[i];
			if (!card.IsDoNotSort())
			{
				card.ShowCard();
				card.EnableTransitioningZones(true);
				Transform zoneTransformForCard = this.GetZoneTransformForCard(card);
				iTween.MoveTo(card.gameObject, zoneTransformForCard.position, 1f);
				iTween.RotateTo(card.gameObject, zoneTransformForCard.localEulerAngles, 1f);
				iTween.ScaleTo(card.gameObject, zoneTransformForCard.localScale, 1f);
			}
		}
		this.StartFinishLayoutTimer(1f);
	}

	// Token: 0x060031AC RID: 12716 RVA: 0x000FE444 File Offset: 0x000FC644
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

	// Token: 0x060031AD RID: 12717 RVA: 0x000FE484 File Offset: 0x000FC684
	public virtual void OnHealingDoesDamageEntityMousedOver()
	{
		if (TargetReticleManager.Get().IsActive())
		{
			return;
		}
		foreach (Card card in this.m_cards)
		{
			if (card.CanPlayHealingDoesDamageHint())
			{
				Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST, true);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
				Spell actorSpell2 = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_IDLE, true);
				if (actorSpell2 != null)
				{
					actorSpell2.ActivateState(SpellStateType.BIRTH);
				}
			}
		}
	}

	// Token: 0x060031AE RID: 12718 RVA: 0x000FE520 File Offset: 0x000FC720
	public virtual void OnHealingDoesDamageEntityMousedOut()
	{
		foreach (Card card in this.m_cards)
		{
			if (!card.GetEntity().HasTag(GAME_TAG.HEALING_DOES_DAMAGE_HINT))
			{
				Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_IDLE, true);
				if (!(actorSpell == null) && actorSpell.IsActive())
				{
					actorSpell.ActivateState(SpellStateType.DEATH);
				}
			}
		}
	}

	// Token: 0x060031AF RID: 12719 RVA: 0x000FE5A4 File Offset: 0x000FC7A4
	public virtual void OnHealingDoesDamageEntityEnteredPlay()
	{
		foreach (Card card in this.m_cards)
		{
			if (card.CanPlayHealingDoesDamageHint())
			{
				Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST, true);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
			}
		}
	}

	// Token: 0x060031B0 RID: 12720 RVA: 0x000FE614 File Offset: 0x000FC814
	public virtual void OnLifestealDoesDamageEntityMousedOver()
	{
		if (TargetReticleManager.Get().IsActive())
		{
			return;
		}
		foreach (Card card in this.m_cards)
		{
			if (card.CanPlayLifestealDoesDamageHint())
			{
				Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST, true);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
				Spell actorSpell2 = card.GetActorSpell(SpellType.LIFESTEAL_DOES_DAMAGE_HINT_IDLE, true);
				if (actorSpell2 != null)
				{
					actorSpell2.ActivateState(SpellStateType.BIRTH);
				}
			}
		}
	}

	// Token: 0x060031B1 RID: 12721 RVA: 0x000FE6B0 File Offset: 0x000FC8B0
	public virtual void OnLifestealDoesDamageEntityMousedOut()
	{
		foreach (Card card in this.m_cards)
		{
			if (!card.GetEntity().HasTag(GAME_TAG.LIFESTEAL_DOES_DAMAGE_HINT))
			{
				Spell actorSpell = card.GetActorSpell(SpellType.LIFESTEAL_DOES_DAMAGE_HINT_IDLE, true);
				if (!(actorSpell == null) && actorSpell.IsActive())
				{
					actorSpell.ActivateState(SpellStateType.DEATH);
				}
			}
		}
	}

	// Token: 0x060031B2 RID: 12722 RVA: 0x000FE734 File Offset: 0x000FC934
	public virtual void OnLifestealDoesDamageEntityEnteredPlay()
	{
		foreach (Card card in this.m_cards)
		{
			if (card.CanPlayLifestealDoesDamageHint())
			{
				Spell actorSpell = card.GetActorSpell(SpellType.HEALING_DOES_DAMAGE_HINT_BURST, true);
				if (actorSpell != null)
				{
					actorSpell.Reactivate();
				}
			}
		}
	}

	// Token: 0x060031B3 RID: 12723 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnSpellPowerEntityEnteredPlay(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
	}

	// Token: 0x060031B4 RID: 12724 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnSpellPowerEntityMousedOver(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
	}

	// Token: 0x060031B5 RID: 12725 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public virtual void OnSpellPowerEntityMousedOut(TAG_SPELL_SCHOOL spellSchool = TAG_SPELL_SCHOOL.NONE)
	{
	}

	// Token: 0x060031B6 RID: 12726 RVA: 0x000FE7A4 File Offset: 0x000FC9A4
	protected void UpdateInput()
	{
		bool flag = this.IsInputEnabled();
		foreach (Card card in this.m_cards)
		{
			Actor actor = card.GetActor();
			if (!(actor == null))
			{
				actor.ToggleForceIdle(!flag);
				actor.ToggleCollider(flag);
				card.UpdateActorState(false);
			}
		}
		Card mousedOverCard = InputManager.Get().GetMousedOverCard();
		if (flag && this.m_cards.Contains(mousedOverCard))
		{
			mousedOverCard.UpdateProposedManaUsage();
		}
	}

	// Token: 0x060031B7 RID: 12727 RVA: 0x000FE848 File Offset: 0x000FCA48
	protected void StartFinishLayoutTimer(float delaySec)
	{
		if (delaySec <= Mathf.Epsilon)
		{
			this.UpdateLayoutFinished();
			return;
		}
		if (this.m_cards.Find((Card card) => card.IsTransitioningZones()) == null)
		{
			this.UpdateLayoutFinished();
			return;
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"time",
			delaySec,
			"oncomplete",
			"UpdateLayoutFinished",
			"oncompletetarget",
			base.gameObject
		});
		iTween.Timer(base.gameObject, args);
	}

	// Token: 0x060031B8 RID: 12728 RVA: 0x000FE8E8 File Offset: 0x000FCAE8
	protected void UpdateLayoutFinished()
	{
		for (int i = 0; i < this.m_cards.Count; i++)
		{
			this.m_cards[i].EnableTransitioningZones(false);
		}
		this.m_updatingLayout--;
		this.m_layoutDirty = false;
		this.FireUpdateLayoutCompleteCallbacks();
	}

	// Token: 0x060031B9 RID: 12729 RVA: 0x000FE938 File Offset: 0x000FCB38
	protected void FireUpdateLayoutCompleteCallbacks()
	{
		if (this.m_completeListeners.Count == 0)
		{
			return;
		}
		Zone.UpdateLayoutCompleteListener[] array = this.m_completeListeners.ToArray();
		this.m_completeListeners.Clear();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Fire(this);
		}
	}

	// Token: 0x04001B8D RID: 7053
	public TAG_ZONE m_ServerTag;

	// Token: 0x04001B8E RID: 7054
	public Player.Side m_Side;

	// Token: 0x04001B8F RID: 7055
	public const float TRANSITION_SEC = 1f;

	// Token: 0x04001B90 RID: 7056
	protected Player m_controller;

	// Token: 0x04001B91 RID: 7057
	protected List<Card> m_cards = new List<Card>();

	// Token: 0x04001B92 RID: 7058
	protected bool m_layoutDirty = true;

	// Token: 0x04001B93 RID: 7059
	protected int m_updatingLayout;

	// Token: 0x04001B94 RID: 7060
	protected List<Zone.UpdateLayoutCompleteListener> m_completeListeners = new List<Zone.UpdateLayoutCompleteListener>();

	// Token: 0x04001B95 RID: 7061
	protected int m_inputBlockerCount;

	// Token: 0x04001B96 RID: 7062
	protected int m_layoutBlockerCount;

	// Token: 0x020016FC RID: 5884
	// (Invoke) Token: 0x0600E66E RID: 58990
	public delegate void UpdateLayoutCompleteCallback(Zone zone, object userData);

	// Token: 0x020016FD RID: 5885
	protected class UpdateLayoutCompleteListener : EventListener<Zone.UpdateLayoutCompleteCallback>
	{
		// Token: 0x0600E671 RID: 58993 RVA: 0x0041163B File Offset: 0x0040F83B
		public void Fire(Zone zone)
		{
			this.m_callback(zone, this.m_userData);
		}
	}
}
