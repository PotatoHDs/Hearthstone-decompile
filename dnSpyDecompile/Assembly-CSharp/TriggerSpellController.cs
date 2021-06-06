using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006D5 RID: 1749
[CustomEditClass]
public class TriggerSpellController : SpellController
{
	// Token: 0x060061CD RID: 25037 RVA: 0x001FED60 File Offset: 0x001FCF60
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!this.HasSourceCard(taskList))
		{
			return false;
		}
		List<Entity> sourceEntities = taskList.GetSourceEntities(true);
		List<Card> list = new List<Card>();
		foreach (Entity entity in sourceEntities)
		{
			if (entity != null)
			{
				list.Add(entity.GetCard());
			}
		}
		foreach (Card card in list)
		{
			CardEffect cardEffect = this.InitEffect(card);
			if (cardEffect != null)
			{
				this.InitTriggerSpell(cardEffect, card);
				this.InitTriggerSounds(cardEffect, card);
			}
			if (this.CanPlayActorTriggerSpell(card.GetEntity()))
			{
				this.m_actorTriggerSpellByEntityId.Add(card.GetEntity().GetEntityId(), this.GetActorTriggerSpell(card.GetEntity()));
			}
			Card card2 = card;
			if (card.GetEntity().IsEnchantment())
			{
				Entity entity2 = GameState.Get().GetEntity(card.GetEntity().GetAttached());
				if (entity2 != null)
				{
					card2 = entity2.GetCard();
				}
			}
			if (card2 != null)
			{
				Spell auxiliaryTriggerSpell = this.GetAuxiliaryTriggerSpell();
				if (auxiliaryTriggerSpell != null)
				{
					this.m_auxiliaryTriggerSpellByEntityId.Add(card.GetEntity().GetEntityId(), auxiliaryTriggerSpell);
					auxiliaryTriggerSpell.SetSource(card2.gameObject);
					if (!auxiliaryTriggerSpell.AttachPowerTaskList(this.m_taskList))
					{
						Log.Power.Print("{0}.AddPowerSourceAndTargets() - FAILED to add targets to spell for {1}", new object[]
						{
							this,
							this.m_auxiliaryTriggerSpellByEntityId
						});
						this.m_auxiliaryTriggerSpellByEntityId.Remove(card.GetEntity().GetEntityId());
					}
				}
			}
		}
		if (this.m_triggerSpellByEntityId.Count == 0 && this.m_triggerSoundSpells.Count == 0 && this.m_actorTriggerSpellByEntityId.Count == 0 && this.m_auxiliaryTriggerSpellByEntityId.Count == 0)
		{
			this.Reset();
			return TurnStartManager.Get().IsCardDrawHandled((list.Count > 0) ? list[0] : null) || TurnStartManager.Get().IsCardDrawHandled(taskList.GetStartDrawMetaDataCard());
		}
		base.SetSource(list);
		return true;
	}

	// Token: 0x060061CE RID: 25038 RVA: 0x001FEFB4 File Offset: 0x001FD1B4
	protected override bool HasSourceCard(PowerTaskList taskList)
	{
		List<Entity> sourceEntities = taskList.GetSourceEntities(true);
		if (sourceEntities == null || sourceEntities.Count == 0)
		{
			return false;
		}
		if (this.GetCardsWithActorTrigger(taskList).Count == 0)
		{
			Card startDrawMetaDataCard = taskList.GetStartDrawMetaDataCard();
			return startDrawMetaDataCard != null && TurnStartManager.Get().IsCardDrawHandled(startDrawMetaDataCard);
		}
		return true;
	}

	// Token: 0x060061CF RID: 25039 RVA: 0x001FF003 File Offset: 0x001FD203
	protected override void OnProcessTaskList()
	{
		base.StartCoroutine(this.OnProcessTaskListImpl());
	}

	// Token: 0x060061D0 RID: 25040 RVA: 0x001FF012 File Offset: 0x001FD212
	private IEnumerator OnProcessTaskListImpl()
	{
		if (GameState.Get().IsTurnStartManagerActive())
		{
			TurnStartManager.Get().NotifyOfTriggerVisual();
			while (TurnStartManager.Get().IsTurnStartIndicatorShowing())
			{
				yield return null;
			}
		}
		if (!this.ActivateInitialSpell())
		{
			base.OnProcessTaskList();
			yield break;
		}
		yield break;
	}

	// Token: 0x060061D1 RID: 25041 RVA: 0x001FF021 File Offset: 0x001FD221
	protected override void OnFinished()
	{
		if (this.m_processingTaskList)
		{
			this.m_pendingFinish = true;
			return;
		}
		base.StartCoroutine(this.WaitThenFinish());
	}

	// Token: 0x060061D2 RID: 25042 RVA: 0x001FF040 File Offset: 0x001FD240
	public override bool ShouldReconnectIfStuck()
	{
		if (this.m_triggerSpellByEntityId.Count > 0)
		{
			foreach (KeyValuePair<int, Spell> keyValuePair in this.m_triggerSpellByEntityId)
			{
				if (keyValuePair.Value != null && keyValuePair.Value.ShouldReconnectIfStuck())
				{
					return true;
				}
			}
		}
		return base.ShouldReconnectIfStuck();
	}

	// Token: 0x060061D3 RID: 25043 RVA: 0x001FF0C4 File Offset: 0x001FD2C4
	private void Reset()
	{
		foreach (KeyValuePair<int, Spell> keyValuePair in this.m_triggerSpellByEntityId)
		{
			Spell value = keyValuePair.Value;
			if (!(value == null) && value.GetPowerTaskList() != null && value.GetPowerTaskList().GetId() == this.m_taskListId)
			{
				SpellUtils.PurgeSpell(value);
			}
		}
		for (int i = 0; i < this.m_triggerSoundSpells.Count; i++)
		{
			CardSoundSpell cardSoundSpell = this.m_triggerSoundSpells[i];
			if (cardSoundSpell != null && cardSoundSpell.GetPowerTaskList().GetId() == this.m_taskListId)
			{
				SpellUtils.PurgeSpell(cardSoundSpell);
			}
		}
		foreach (KeyValuePair<int, Spell> keyValuePair2 in this.m_auxiliaryTriggerSpellByEntityId)
		{
			Spell value2 = keyValuePair2.Value;
			if (!(value2 == null) && value2.GetPowerTaskList() != null && value2.GetPowerTaskList().GetId() == this.m_taskListId)
			{
				SpellUtils.PurgeSpell(value2);
			}
		}
		foreach (KeyValuePair<int, Spell> keyValuePair3 in this.m_actorTriggerSpellByEntityId)
		{
			Spell value3 = keyValuePair3.Value;
			if (!(value3 == null) && value3.GetPowerTaskList() != null && value3.GetPowerTaskList().GetId() == this.m_taskListId)
			{
				SpellUtils.PurgeSpell(value3);
			}
		}
		this.m_triggerSpellByEntityId.Clear();
		this.m_auxiliaryTriggerSpellByEntityId.Clear();
		this.m_triggerSoundSpells.Clear();
		this.m_actorTriggerSpellByEntityId.Clear();
		this.m_cardEffectsBlockingFinish = 0;
		this.m_cardEffectsBlockingTaskListFinish = 0;
		this.m_actorEffectsBlockingFinish = 0;
	}

	// Token: 0x060061D4 RID: 25044 RVA: 0x001FF2B4 File Offset: 0x001FD4B4
	private IEnumerator WaitThenFinish()
	{
		yield return new WaitForSeconds(10f);
		this.Reset();
		base.OnFinished();
		yield break;
	}

	// Token: 0x060061D5 RID: 25045 RVA: 0x001FF2C4 File Offset: 0x001FD4C4
	private bool ActivateInitialSpell()
	{
		List<Entity> sourceEntities = this.m_taskList.GetSourceEntities(true);
		bool result = false;
		foreach (Entity entity in sourceEntities)
		{
			if (this.ActivateActorTriggerSpell(entity.GetEntityId()))
			{
				result = true;
			}
			else
			{
				this.ActivateAuxiliaryTriggerSpell(entity.GetEntityId());
				if (this.ActivateCardEffects(entity.GetEntityId()))
				{
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x060061D6 RID: 25046 RVA: 0x001FF348 File Offset: 0x001FD548
	private void ProcessCurrentTaskList()
	{
		if (this.m_taskList != null)
		{
			this.m_taskList.DoAllTasks();
		}
	}

	// Token: 0x060061D7 RID: 25047 RVA: 0x001FF360 File Offset: 0x001FD560
	private List<Card> GetCardsWithActorTrigger(PowerTaskList taskList)
	{
		List<Entity> sourceEntities = taskList.GetSourceEntities(true);
		return this.GetCardsWithActorTrigger(sourceEntities);
	}

	// Token: 0x060061D8 RID: 25048 RVA: 0x001FF37C File Offset: 0x001FD57C
	private List<Card> GetCardsWithActorTrigger(List<Entity> entities)
	{
		List<Card> list = new List<Card>();
		if (entities == null || entities.Count == 0)
		{
			return list;
		}
		foreach (Entity entity in entities)
		{
			Card cardWithActorTrigger = this.GetCardWithActorTrigger(entity);
			if (cardWithActorTrigger != null)
			{
				list.Add(cardWithActorTrigger);
			}
		}
		return list;
	}

	// Token: 0x060061D9 RID: 25049 RVA: 0x001FF3F0 File Offset: 0x001FD5F0
	private Card GetCardWithActorTrigger(Entity entity)
	{
		if (entity == null)
		{
			return null;
		}
		Card card;
		if (entity.IsEnchantment())
		{
			Entity entity2 = GameState.Get().GetEntity(entity.GetAttached());
			if (entity2 == null)
			{
				return null;
			}
			card = entity2.GetCard();
		}
		else
		{
			card = entity.GetCard();
		}
		return card;
	}

	// Token: 0x060061DA RID: 25050 RVA: 0x001FF434 File Offset: 0x001FD634
	private bool CanPlayActorTriggerSpell(Entity entity)
	{
		if (entity.GetController() != null && !entity.GetController().IsFriendlySide() && entity.IsObfuscated())
		{
			return false;
		}
		if (!this.m_taskList.IsOrigin())
		{
			return false;
		}
		Card cardWithActorTrigger = this.GetCardWithActorTrigger(entity);
		if (cardWithActorTrigger == null)
		{
			return false;
		}
		if (cardWithActorTrigger.WillSuppressActorTriggerSpell())
		{
			return false;
		}
		if (!cardWithActorTrigger.CanShowActorVisuals())
		{
			return false;
		}
		int count = this.m_triggerSpellByEntityId.Count;
		return true;
	}

	// Token: 0x060061DB RID: 25051 RVA: 0x001FF4A8 File Offset: 0x001FD6A8
	private Spell GetActorTriggerSpell(Entity entity)
	{
		int triggerKeyword = this.m_taskList.GetBlockStart().TriggerKeyword;
		SpellType actorTriggerSpellType = this.GetActorTriggerSpellType(triggerKeyword, entity);
		if (actorTriggerSpellType == SpellType.NONE)
		{
			return null;
		}
		return this.GetCardWithActorTrigger(entity).GetActorSpell(actorTriggerSpellType, true);
	}

	// Token: 0x060061DC RID: 25052 RVA: 0x001FF4E4 File Offset: 0x001FD6E4
	private SpellType GetActorTriggerSpellType(int triggerKeyword, Entity entity)
	{
		SpellType spellType = SpellType.NONE;
		if (triggerKeyword <= 685)
		{
			if (triggerKeyword <= 363)
			{
				if (triggerKeyword != 32)
				{
					if (triggerKeyword != 363)
					{
						goto IL_96;
					}
					goto IL_6D;
				}
			}
			else
			{
				if (triggerKeyword == 403)
				{
					spellType = SpellType.INSPIRE;
					goto IL_96;
				}
				if (triggerKeyword != 424)
				{
					if (triggerKeyword != 685)
					{
						goto IL_96;
					}
					goto IL_77;
				}
			}
			spellType = SpellType.TRIGGER;
			goto IL_96;
		}
		if (triggerKeyword <= 1427)
		{
			if (triggerKeyword == 923)
			{
				spellType = SpellType.OVERKILL;
				goto IL_96;
			}
			if (triggerKeyword != 1427)
			{
				goto IL_96;
			}
			spellType = SpellType.SPELLBURST;
			goto IL_96;
		}
		else
		{
			if (triggerKeyword == 1637)
			{
				spellType = SpellType.FRENZY;
				goto IL_96;
			}
			if (triggerKeyword == 1675)
			{
				goto IL_77;
			}
			if (triggerKeyword != 1944)
			{
				goto IL_96;
			}
		}
		IL_6D:
		spellType = SpellType.POISONOUS;
		goto IL_96;
		IL_77:
		spellType = SpellType.LIFESTEAL;
		IL_96:
		if (spellType == SpellType.TRIGGER && GameState.Get().IsUsingFastActorTriggers() && !entity.IsHeroPower())
		{
			spellType = SpellType.FAST_TRIGGER;
		}
		return spellType;
	}

	// Token: 0x060061DD RID: 25053 RVA: 0x001FF5A4 File Offset: 0x001FD7A4
	private bool ActivateActorTriggerSpell(int entityId)
	{
		if (!this.m_actorTriggerSpellByEntityId.ContainsKey(entityId))
		{
			return false;
		}
		Spell spell = this.m_actorTriggerSpellByEntityId[entityId];
		if (spell == null)
		{
			return false;
		}
		Entity entity = this.m_taskList.GetSourceEntities(true).Find((Entity e) => e.GetEntityId() == entityId);
		Card cardWithActorTrigger = this.GetCardWithActorTrigger(entity);
		if (cardWithActorTrigger == null)
		{
			return false;
		}
		if (cardWithActorTrigger.IsBaubleAnimating())
		{
			Log.Gameplay.PrintError("TriggerSpellController.ActivateTriggerSpell(): Clobbering bauble that is currently animating on Card {0}.", new object[]
			{
				cardWithActorTrigger
			});
		}
		cardWithActorTrigger.DeactivateBaubles();
		cardWithActorTrigger.SetIsBaubleAnimating(true);
		this.m_actorEffectsBlockingFinish++;
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnActorTriggerSpellStateFinished), entity);
		spell.ActivateState(SpellStateType.ACTION);
		return true;
	}

	// Token: 0x060061DE RID: 25054 RVA: 0x001FF678 File Offset: 0x001FD878
	private void OnActorTriggerSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (prevStateType != SpellStateType.ACTION)
		{
			return;
		}
		spell.RemoveStateFinishedCallback(new Spell.StateFinishedCallback(this.OnActorTriggerSpellStateFinished), userData);
		base.StartCoroutine(this.FinishActorTriggerSpell(spell, prevStateType, userData));
	}

	// Token: 0x060061DF RID: 25055 RVA: 0x001FF6A3 File Offset: 0x001FD8A3
	private IEnumerator FinishActorTriggerSpell(Spell spell, SpellStateType prevStateType, object userData)
	{
		Entity entity = (Entity)userData;
		this.m_baubleBlockedFinish = false;
		this.m_waitingForBauble = true;
		bool activatedCardEffects = this.ActivateCardEffects(entity.GetEntityId());
		if (!activatedCardEffects)
		{
			this.ProcessCurrentTaskList();
		}
		this.ActivateAuxiliaryTriggerSpell(entity.GetEntityId());
		SpellType spellType = this.m_actorTriggerSpellByEntityId[entity.GetEntityId()].GetSpellType();
		if (spellType <= SpellType.FAST_TRIGGER)
		{
			if (spellType != SpellType.TRIGGER && spellType != SpellType.POISONOUS && spellType != SpellType.FAST_TRIGGER)
			{
				goto IL_DC;
			}
		}
		else if (spellType != SpellType.INSPIRE && spellType != SpellType.LIFESTEAL && spellType - SpellType.DORMANT > 1)
		{
			goto IL_DC;
		}
		yield return null;
		goto IL_FC;
		IL_DC:
		yield return new WaitForSeconds(TriggerSpellController.BAUBLE_WAIT_TIME_SEC);
		IL_FC:
		Card cardWithActorTrigger = this.GetCardWithActorTrigger(entity);
		cardWithActorTrigger.SetIsBaubleAnimating(false);
		if (cardWithActorTrigger.CanShowActorVisuals())
		{
			cardWithActorTrigger.UpdateBauble();
		}
		this.m_waitingForBauble = false;
		this.m_actorEffectsBlockingFinish--;
		if (this.m_actorEffectsBlockingFinish > 0)
		{
			yield break;
		}
		if (!activatedCardEffects)
		{
			base.OnProcessTaskList();
		}
		else if (this.m_baubleBlockedFinish)
		{
			this.OnFinishedTaskList();
		}
		yield break;
	}

	// Token: 0x060061E0 RID: 25056 RVA: 0x001FF6BC File Offset: 0x001FD8BC
	private CardEffect InitEffect(Card card)
	{
		if (card == null)
		{
			return null;
		}
		Network.HistBlockStart blockStart = this.m_taskList.GetBlockStart();
		int entityId = card.GetEntity().GetEntityId();
		string effectCardId = this.m_taskList.GetEffectCardId(entityId);
		int num = blockStart.EffectIndex;
		if (num < 0)
		{
			if (string.IsNullOrEmpty(effectCardId) || this.m_taskList.IsEffectCardIdClientCached(entityId))
			{
				return null;
			}
			num = 0;
		}
		CardEffect result = null;
		Entity entity = card.GetEntity();
		string a = (entity != null) ? entity.GetCardId() : null;
		if (!string.IsNullOrEmpty(effectCardId) && !(a == effectCardId))
		{
			using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(effectCardId, null))
			{
				if (cardDef.CardDef.m_TriggerEffectDefs == null)
				{
					return null;
				}
				if (num >= cardDef.CardDef.m_TriggerEffectDefs.Count)
				{
					return null;
				}
				result = new CardEffect(cardDef.CardDef.m_TriggerEffectDefs[num], card);
			}
			return result;
		}
		result = card.GetTriggerEffect(num);
		return result;
	}

	// Token: 0x060061E1 RID: 25057 RVA: 0x001FF7C8 File Offset: 0x001FD9C8
	private bool ActivateCardEffects(int entityId)
	{
		bool flag = this.ActivateTriggerSpell(entityId);
		bool flag2 = this.ActivateTriggerSounds();
		return flag || flag2;
	}

	// Token: 0x060061E2 RID: 25058 RVA: 0x001FF7E5 File Offset: 0x001FD9E5
	private void OnCardSpellFinished(Spell spell, object userData)
	{
		this.CardSpellFinished();
	}

	// Token: 0x060061E3 RID: 25059 RVA: 0x001FF7ED File Offset: 0x001FD9ED
	private void OnCardSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		this.CardSpellNoneStateEntered();
	}

	// Token: 0x060061E4 RID: 25060 RVA: 0x001FF7FE File Offset: 0x001FD9FE
	private void CardSpellFinished()
	{
		this.m_cardEffectsBlockingTaskListFinish--;
		if (this.m_cardEffectsBlockingTaskListFinish > 0)
		{
			return;
		}
		if (this.m_waitingForBauble)
		{
			this.m_baubleBlockedFinish = true;
			this.ProcessCurrentTaskList();
			return;
		}
		this.OnFinishedTaskList();
	}

	// Token: 0x060061E5 RID: 25061 RVA: 0x001FF834 File Offset: 0x001FDA34
	private void CardSpellNoneStateEntered()
	{
		this.m_cardEffectsBlockingFinish--;
		if (this.m_cardEffectsBlockingFinish > 0)
		{
			return;
		}
		this.OnFinished();
	}

	// Token: 0x060061E6 RID: 25062 RVA: 0x001FF854 File Offset: 0x001FDA54
	private void InitTriggerSpell(CardEffect effect, Card card)
	{
		Spell spell = effect.GetSpell(true);
		if (spell == null)
		{
			return;
		}
		if (!spell.AttachPowerTaskList(this.m_taskList))
		{
			Log.Power.Print("{0}.InitTriggerSpell() - FAILED to add targets to spell for {1}", new object[]
			{
				this,
				card
			});
			return;
		}
		this.m_triggerSpellByEntityId.Add(card.GetEntity().GetEntityId(), spell);
		this.m_cardEffectsBlockingFinish++;
		this.m_cardEffectsBlockingTaskListFinish++;
	}

	// Token: 0x060061E7 RID: 25063 RVA: 0x001FF8D4 File Offset: 0x001FDAD4
	private bool ActivateTriggerSpell(int entityId)
	{
		if (!this.m_triggerSpellByEntityId.ContainsKey(entityId))
		{
			return false;
		}
		Spell spell = this.m_triggerSpellByEntityId[entityId];
		if (spell == null)
		{
			return false;
		}
		spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnCardSpellFinished));
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnCardSpellStateFinished));
		spell.ActivateState(SpellStateType.ACTION);
		return true;
	}

	// Token: 0x060061E8 RID: 25064 RVA: 0x001FF938 File Offset: 0x001FDB38
	private bool InitTriggerSounds(CardEffect effect, Card card)
	{
		List<CardSoundSpell> soundSpells = effect.GetSoundSpells(true);
		if (soundSpells == null)
		{
			return false;
		}
		if (soundSpells.Count == 0)
		{
			return false;
		}
		foreach (CardSoundSpell cardSoundSpell in soundSpells)
		{
			if (cardSoundSpell)
			{
				if (!cardSoundSpell.AttachPowerTaskList(this.m_taskList))
				{
					Log.Power.Print("{0}.InitTriggerSounds() - FAILED to attach task list to TriggerSoundSpell {1} for Card {2}", new object[]
					{
						base.name,
						cardSoundSpell,
						card
					});
				}
				else
				{
					this.m_triggerSoundSpells.Add(cardSoundSpell);
				}
			}
		}
		if (this.m_triggerSoundSpells.Count == 0)
		{
			return false;
		}
		this.m_cardEffectsBlockingFinish++;
		this.m_cardEffectsBlockingTaskListFinish++;
		return true;
	}

	// Token: 0x060061E9 RID: 25065 RVA: 0x001FFA0C File Offset: 0x001FDC0C
	private bool ActivateTriggerSounds()
	{
		if (this.m_triggerSoundSpells.Count == 0)
		{
			return false;
		}
		Card source = base.GetSource();
		foreach (CardSoundSpell cardSoundSpell in this.m_triggerSoundSpells)
		{
			if (cardSoundSpell)
			{
				source.ActivateSoundSpell(cardSoundSpell);
			}
		}
		this.CardSpellFinished();
		this.CardSpellNoneStateEntered();
		return true;
	}

	// Token: 0x060061EA RID: 25066 RVA: 0x001FFA8C File Offset: 0x001FDC8C
	private Spell GetAuxiliaryTriggerSpell()
	{
		int triggerKeyword = this.m_taskList.GetBlockStart().TriggerKeyword;
		int i = 0;
		while (i < this.m_AuxiliaryTriggerSpells.Count)
		{
			if (this.m_AuxiliaryTriggerSpells[i].m_TriggerKeyword == (GAME_TAG)triggerKeyword)
			{
				Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_AuxiliaryTriggerSpells[i].m_Spell);
				if (spell != null)
				{
					return spell;
				}
				Log.Gameplay.PrintError("{0}.GetAuxiliaryTriggerSpell(): keyword:{1}, spell:{2}", new object[]
				{
					this,
					triggerKeyword,
					this.m_AuxiliaryTriggerSpells[i].m_Spell
				});
				return null;
			}
			else
			{
				i++;
			}
		}
		return null;
	}

	// Token: 0x060061EB RID: 25067 RVA: 0x001FFB2F File Offset: 0x001FDD2F
	private void ActivateAuxiliaryTriggerSpell(int entityId)
	{
		if (!this.m_auxiliaryTriggerSpellByEntityId.ContainsKey(entityId) || this.m_auxiliaryTriggerSpellByEntityId[entityId] == null)
		{
			return;
		}
		this.m_auxiliaryTriggerSpellByEntityId[entityId].ActivateState(SpellStateType.ACTION);
	}

	// Token: 0x060061EC RID: 25068 RVA: 0x001FFB66 File Offset: 0x001FDD66
	protected override float GetLostFrameTimeCatchUpSeconds()
	{
		return 0.2f;
	}

	// Token: 0x04005174 RID: 20852
	public List<AuxiliaryTriggerSpellEntry> m_AuxiliaryTriggerSpells = new List<AuxiliaryTriggerSpellEntry>();

	// Token: 0x04005175 RID: 20853
	private Map<int, Spell> m_triggerSpellByEntityId = new Map<int, Spell>();

	// Token: 0x04005176 RID: 20854
	private List<CardSoundSpell> m_triggerSoundSpells = new List<CardSoundSpell>();

	// Token: 0x04005177 RID: 20855
	private Map<int, Spell> m_actorTriggerSpellByEntityId = new Map<int, Spell>();

	// Token: 0x04005178 RID: 20856
	private Map<int, Spell> m_auxiliaryTriggerSpellByEntityId = new Map<int, Spell>();

	// Token: 0x04005179 RID: 20857
	private static readonly float BAUBLE_WAIT_TIME_SEC = 1f;

	// Token: 0x0400517A RID: 20858
	private int m_cardEffectsBlockingFinish;

	// Token: 0x0400517B RID: 20859
	private int m_cardEffectsBlockingTaskListFinish;

	// Token: 0x0400517C RID: 20860
	private int m_actorEffectsBlockingFinish;

	// Token: 0x0400517D RID: 20861
	private bool m_waitingForBauble;

	// Token: 0x0400517E RID: 20862
	private bool m_baubleBlockedFinish;
}
