using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class TriggerSpellController : SpellController
{
	public List<AuxiliaryTriggerSpellEntry> m_AuxiliaryTriggerSpells = new List<AuxiliaryTriggerSpellEntry>();

	private Map<int, Spell> m_triggerSpellByEntityId = new Map<int, Spell>();

	private List<CardSoundSpell> m_triggerSoundSpells = new List<CardSoundSpell>();

	private Map<int, Spell> m_actorTriggerSpellByEntityId = new Map<int, Spell>();

	private Map<int, Spell> m_auxiliaryTriggerSpellByEntityId = new Map<int, Spell>();

	private static readonly float BAUBLE_WAIT_TIME_SEC = 1f;

	private int m_cardEffectsBlockingFinish;

	private int m_cardEffectsBlockingTaskListFinish;

	private int m_actorEffectsBlockingFinish;

	private bool m_waitingForBauble;

	private bool m_baubleBlockedFinish;

	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!HasSourceCard(taskList))
		{
			return false;
		}
		List<Entity> sourceEntities = taskList.GetSourceEntities();
		List<Card> list = new List<Card>();
		foreach (Entity item in sourceEntities)
		{
			if (item != null)
			{
				list.Add(item.GetCard());
			}
		}
		foreach (Card item2 in list)
		{
			CardEffect cardEffect = InitEffect(item2);
			if (cardEffect != null)
			{
				InitTriggerSpell(cardEffect, item2);
				InitTriggerSounds(cardEffect, item2);
			}
			if (CanPlayActorTriggerSpell(item2.GetEntity()))
			{
				m_actorTriggerSpellByEntityId.Add(item2.GetEntity().GetEntityId(), GetActorTriggerSpell(item2.GetEntity()));
			}
			Card card = item2;
			if (item2.GetEntity().IsEnchantment())
			{
				Entity entity = GameState.Get().GetEntity(item2.GetEntity().GetAttached());
				if (entity != null)
				{
					card = entity.GetCard();
				}
			}
			if (!(card != null))
			{
				continue;
			}
			Spell auxiliaryTriggerSpell = GetAuxiliaryTriggerSpell();
			if (auxiliaryTriggerSpell != null)
			{
				m_auxiliaryTriggerSpellByEntityId.Add(item2.GetEntity().GetEntityId(), auxiliaryTriggerSpell);
				auxiliaryTriggerSpell.SetSource(card.gameObject);
				if (!auxiliaryTriggerSpell.AttachPowerTaskList(m_taskList))
				{
					Log.Power.Print("{0}.AddPowerSourceAndTargets() - FAILED to add targets to spell for {1}", this, m_auxiliaryTriggerSpellByEntityId);
					m_auxiliaryTriggerSpellByEntityId.Remove(item2.GetEntity().GetEntityId());
				}
			}
		}
		if (m_triggerSpellByEntityId.Count == 0 && m_triggerSoundSpells.Count == 0 && m_actorTriggerSpellByEntityId.Count == 0 && m_auxiliaryTriggerSpellByEntityId.Count == 0)
		{
			Reset();
			if (!TurnStartManager.Get().IsCardDrawHandled((list.Count > 0) ? list[0] : null))
			{
				return TurnStartManager.Get().IsCardDrawHandled(taskList.GetStartDrawMetaDataCard());
			}
			return true;
		}
		SetSource(list);
		return true;
	}

	protected override bool HasSourceCard(PowerTaskList taskList)
	{
		List<Entity> sourceEntities = taskList.GetSourceEntities();
		if (sourceEntities == null || sourceEntities.Count == 0)
		{
			return false;
		}
		if (GetCardsWithActorTrigger(taskList).Count == 0)
		{
			Card startDrawMetaDataCard = taskList.GetStartDrawMetaDataCard();
			if (startDrawMetaDataCard != null)
			{
				return TurnStartManager.Get().IsCardDrawHandled(startDrawMetaDataCard);
			}
			return false;
		}
		return true;
	}

	protected override void OnProcessTaskList()
	{
		StartCoroutine(OnProcessTaskListImpl());
	}

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
		if (!ActivateInitialSpell())
		{
			base.OnProcessTaskList();
		}
	}

	protected override void OnFinished()
	{
		if (m_processingTaskList)
		{
			m_pendingFinish = true;
		}
		else
		{
			StartCoroutine(WaitThenFinish());
		}
	}

	public override bool ShouldReconnectIfStuck()
	{
		if (m_triggerSpellByEntityId.Count > 0)
		{
			foreach (KeyValuePair<int, Spell> item in m_triggerSpellByEntityId)
			{
				if (item.Value != null && item.Value.ShouldReconnectIfStuck())
				{
					return true;
				}
			}
		}
		return base.ShouldReconnectIfStuck();
	}

	private void Reset()
	{
		foreach (KeyValuePair<int, Spell> item in m_triggerSpellByEntityId)
		{
			Spell value = item.Value;
			if (!(value == null) && value.GetPowerTaskList() != null && value.GetPowerTaskList().GetId() == m_taskListId)
			{
				SpellUtils.PurgeSpell(value);
			}
		}
		for (int i = 0; i < m_triggerSoundSpells.Count; i++)
		{
			CardSoundSpell cardSoundSpell = m_triggerSoundSpells[i];
			if (cardSoundSpell != null && cardSoundSpell.GetPowerTaskList().GetId() == m_taskListId)
			{
				SpellUtils.PurgeSpell(cardSoundSpell);
			}
		}
		foreach (KeyValuePair<int, Spell> item2 in m_auxiliaryTriggerSpellByEntityId)
		{
			Spell value2 = item2.Value;
			if (!(value2 == null) && value2.GetPowerTaskList() != null && value2.GetPowerTaskList().GetId() == m_taskListId)
			{
				SpellUtils.PurgeSpell(value2);
			}
		}
		foreach (KeyValuePair<int, Spell> item3 in m_actorTriggerSpellByEntityId)
		{
			Spell value3 = item3.Value;
			if (!(value3 == null) && value3.GetPowerTaskList() != null && value3.GetPowerTaskList().GetId() == m_taskListId)
			{
				SpellUtils.PurgeSpell(value3);
			}
		}
		m_triggerSpellByEntityId.Clear();
		m_auxiliaryTriggerSpellByEntityId.Clear();
		m_triggerSoundSpells.Clear();
		m_actorTriggerSpellByEntityId.Clear();
		m_cardEffectsBlockingFinish = 0;
		m_cardEffectsBlockingTaskListFinish = 0;
		m_actorEffectsBlockingFinish = 0;
	}

	private IEnumerator WaitThenFinish()
	{
		yield return new WaitForSeconds(10f);
		Reset();
		base.OnFinished();
	}

	private bool ActivateInitialSpell()
	{
		List<Entity> sourceEntities = m_taskList.GetSourceEntities();
		bool result = false;
		foreach (Entity item in sourceEntities)
		{
			if (ActivateActorTriggerSpell(item.GetEntityId()))
			{
				result = true;
				continue;
			}
			ActivateAuxiliaryTriggerSpell(item.GetEntityId());
			if (ActivateCardEffects(item.GetEntityId()))
			{
				result = true;
			}
		}
		return result;
	}

	private void ProcessCurrentTaskList()
	{
		if (m_taskList != null)
		{
			m_taskList.DoAllTasks();
		}
	}

	private List<Card> GetCardsWithActorTrigger(PowerTaskList taskList)
	{
		List<Entity> sourceEntities = taskList.GetSourceEntities();
		return GetCardsWithActorTrigger(sourceEntities);
	}

	private List<Card> GetCardsWithActorTrigger(List<Entity> entities)
	{
		List<Card> list = new List<Card>();
		if (entities == null || entities.Count == 0)
		{
			return list;
		}
		foreach (Entity entity in entities)
		{
			Card cardWithActorTrigger = GetCardWithActorTrigger(entity);
			if (cardWithActorTrigger != null)
			{
				list.Add(cardWithActorTrigger);
			}
		}
		return list;
	}

	private Card GetCardWithActorTrigger(Entity entity)
	{
		if (entity == null)
		{
			return null;
		}
		if (entity.IsEnchantment())
		{
			return GameState.Get().GetEntity(entity.GetAttached())?.GetCard();
		}
		return entity.GetCard();
	}

	private bool CanPlayActorTriggerSpell(Entity entity)
	{
		if (entity.GetController() != null && !entity.GetController().IsFriendlySide() && entity.IsObfuscated())
		{
			return false;
		}
		if (!m_taskList.IsOrigin())
		{
			return false;
		}
		Card cardWithActorTrigger = GetCardWithActorTrigger(entity);
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
		_ = m_triggerSpellByEntityId.Count;
		_ = 0;
		return true;
	}

	private Spell GetActorTriggerSpell(Entity entity)
	{
		int triggerKeyword = m_taskList.GetBlockStart().TriggerKeyword;
		SpellType actorTriggerSpellType = GetActorTriggerSpellType(triggerKeyword, entity);
		if (actorTriggerSpellType == SpellType.NONE)
		{
			return null;
		}
		return GetCardWithActorTrigger(entity).GetActorSpell(actorTriggerSpellType);
	}

	private SpellType GetActorTriggerSpellType(int triggerKeyword, Entity entity)
	{
		SpellType spellType = SpellType.NONE;
		switch (triggerKeyword)
		{
		case 363:
		case 1944:
			spellType = SpellType.POISONOUS;
			break;
		case 403:
			spellType = SpellType.INSPIRE;
			break;
		case 685:
		case 1675:
			spellType = SpellType.LIFESTEAL;
			break;
		case 923:
			spellType = SpellType.OVERKILL;
			break;
		case 1427:
			spellType = SpellType.SPELLBURST;
			break;
		case 1637:
			spellType = SpellType.FRENZY;
			break;
		case 32:
		case 424:
			spellType = SpellType.TRIGGER;
			break;
		}
		if (spellType == SpellType.TRIGGER && GameState.Get().IsUsingFastActorTriggers() && !entity.IsHeroPower())
		{
			spellType = SpellType.FAST_TRIGGER;
		}
		return spellType;
	}

	private bool ActivateActorTriggerSpell(int entityId)
	{
		if (!m_actorTriggerSpellByEntityId.ContainsKey(entityId))
		{
			return false;
		}
		Spell spell = m_actorTriggerSpellByEntityId[entityId];
		if (spell == null)
		{
			return false;
		}
		Entity entity = m_taskList.GetSourceEntities().Find((Entity e) => e.GetEntityId() == entityId);
		Card cardWithActorTrigger = GetCardWithActorTrigger(entity);
		if (cardWithActorTrigger == null)
		{
			return false;
		}
		if (cardWithActorTrigger.IsBaubleAnimating())
		{
			Log.Gameplay.PrintError("TriggerSpellController.ActivateTriggerSpell(): Clobbering bauble that is currently animating on Card {0}.", cardWithActorTrigger);
		}
		cardWithActorTrigger.DeactivateBaubles();
		cardWithActorTrigger.SetIsBaubleAnimating(isAnimating: true);
		m_actorEffectsBlockingFinish++;
		spell.AddStateFinishedCallback(OnActorTriggerSpellStateFinished, entity);
		spell.ActivateState(SpellStateType.ACTION);
		return true;
	}

	private void OnActorTriggerSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (prevStateType == SpellStateType.ACTION)
		{
			spell.RemoveStateFinishedCallback(OnActorTriggerSpellStateFinished, userData);
			StartCoroutine(FinishActorTriggerSpell(spell, prevStateType, userData));
		}
	}

	private IEnumerator FinishActorTriggerSpell(Spell spell, SpellStateType prevStateType, object userData)
	{
		Entity entity = (Entity)userData;
		m_baubleBlockedFinish = false;
		m_waitingForBauble = true;
		bool activatedCardEffects = ActivateCardEffects(entity.GetEntityId());
		if (!activatedCardEffects)
		{
			ProcessCurrentTaskList();
		}
		ActivateAuxiliaryTriggerSpell(entity.GetEntityId());
		switch (m_actorTriggerSpellByEntityId[entity.GetEntityId()].GetSpellType())
		{
		case SpellType.TRIGGER:
		case SpellType.POISONOUS:
		case SpellType.FAST_TRIGGER:
		case SpellType.INSPIRE:
		case SpellType.LIFESTEAL:
		case SpellType.DORMANT:
		case SpellType.OVERKILL:
			yield return null;
			break;
		default:
			yield return new WaitForSeconds(BAUBLE_WAIT_TIME_SEC);
			break;
		}
		Card cardWithActorTrigger = GetCardWithActorTrigger(entity);
		cardWithActorTrigger.SetIsBaubleAnimating(isAnimating: false);
		if (cardWithActorTrigger.CanShowActorVisuals())
		{
			cardWithActorTrigger.UpdateBauble();
		}
		m_waitingForBauble = false;
		m_actorEffectsBlockingFinish--;
		if (m_actorEffectsBlockingFinish <= 0)
		{
			if (!activatedCardEffects)
			{
				base.OnProcessTaskList();
			}
			else if (m_baubleBlockedFinish)
			{
				OnFinishedTaskList();
			}
		}
	}

	private CardEffect InitEffect(Card card)
	{
		if (card == null)
		{
			return null;
		}
		Network.HistBlockStart blockStart = m_taskList.GetBlockStart();
		int entityId = card.GetEntity().GetEntityId();
		string effectCardId = m_taskList.GetEffectCardId(entityId);
		int num = blockStart.EffectIndex;
		if (num < 0)
		{
			if (string.IsNullOrEmpty(effectCardId) || m_taskList.IsEffectCardIdClientCached(entityId))
			{
				return null;
			}
			num = 0;
		}
		CardEffect cardEffect = null;
		string text = card.GetEntity()?.GetCardId();
		if (string.IsNullOrEmpty(effectCardId) || text == effectCardId)
		{
			return card.GetTriggerEffect(num);
		}
		using DefLoader.DisposableCardDef disposableCardDef = DefLoader.Get().GetCardDef(effectCardId);
		if (disposableCardDef.CardDef.m_TriggerEffectDefs == null)
		{
			return null;
		}
		if (num >= disposableCardDef.CardDef.m_TriggerEffectDefs.Count)
		{
			return null;
		}
		return new CardEffect(disposableCardDef.CardDef.m_TriggerEffectDefs[num], card);
	}

	private bool ActivateCardEffects(int entityId)
	{
		bool num = ActivateTriggerSpell(entityId);
		bool flag = ActivateTriggerSounds();
		return num || flag;
	}

	private void OnCardSpellFinished(Spell spell, object userData)
	{
		CardSpellFinished();
	}

	private void OnCardSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			CardSpellNoneStateEntered();
		}
	}

	private void CardSpellFinished()
	{
		m_cardEffectsBlockingTaskListFinish--;
		if (m_cardEffectsBlockingTaskListFinish <= 0)
		{
			if (m_waitingForBauble)
			{
				m_baubleBlockedFinish = true;
				ProcessCurrentTaskList();
			}
			else
			{
				OnFinishedTaskList();
			}
		}
	}

	private void CardSpellNoneStateEntered()
	{
		m_cardEffectsBlockingFinish--;
		if (m_cardEffectsBlockingFinish <= 0)
		{
			OnFinished();
		}
	}

	private void InitTriggerSpell(CardEffect effect, Card card)
	{
		Spell spell = effect.GetSpell();
		if (!(spell == null))
		{
			if (!spell.AttachPowerTaskList(m_taskList))
			{
				Log.Power.Print("{0}.InitTriggerSpell() - FAILED to add targets to spell for {1}", this, card);
			}
			else
			{
				m_triggerSpellByEntityId.Add(card.GetEntity().GetEntityId(), spell);
				m_cardEffectsBlockingFinish++;
				m_cardEffectsBlockingTaskListFinish++;
			}
		}
	}

	private bool ActivateTriggerSpell(int entityId)
	{
		if (!m_triggerSpellByEntityId.ContainsKey(entityId))
		{
			return false;
		}
		Spell spell = m_triggerSpellByEntityId[entityId];
		if (spell == null)
		{
			return false;
		}
		spell.AddFinishedCallback(OnCardSpellFinished);
		spell.AddStateFinishedCallback(OnCardSpellStateFinished);
		spell.ActivateState(SpellStateType.ACTION);
		return true;
	}

	private bool InitTriggerSounds(CardEffect effect, Card card)
	{
		List<CardSoundSpell> soundSpells = effect.GetSoundSpells();
		if (soundSpells == null)
		{
			return false;
		}
		if (soundSpells.Count == 0)
		{
			return false;
		}
		foreach (CardSoundSpell item in soundSpells)
		{
			if ((bool)item)
			{
				if (!item.AttachPowerTaskList(m_taskList))
				{
					Log.Power.Print("{0}.InitTriggerSounds() - FAILED to attach task list to TriggerSoundSpell {1} for Card {2}", base.name, item, card);
				}
				else
				{
					m_triggerSoundSpells.Add(item);
				}
			}
		}
		if (m_triggerSoundSpells.Count == 0)
		{
			return false;
		}
		m_cardEffectsBlockingFinish++;
		m_cardEffectsBlockingTaskListFinish++;
		return true;
	}

	private bool ActivateTriggerSounds()
	{
		if (m_triggerSoundSpells.Count == 0)
		{
			return false;
		}
		Card source = GetSource();
		foreach (CardSoundSpell triggerSoundSpell in m_triggerSoundSpells)
		{
			if ((bool)triggerSoundSpell)
			{
				source.ActivateSoundSpell(triggerSoundSpell);
			}
		}
		CardSpellFinished();
		CardSpellNoneStateEntered();
		return true;
	}

	private Spell GetAuxiliaryTriggerSpell()
	{
		int triggerKeyword = m_taskList.GetBlockStart().TriggerKeyword;
		for (int i = 0; i < m_AuxiliaryTriggerSpells.Count; i++)
		{
			if (m_AuxiliaryTriggerSpells[i].m_TriggerKeyword == (GAME_TAG)triggerKeyword)
			{
				Spell spell = Object.Instantiate(m_AuxiliaryTriggerSpells[i].m_Spell);
				if (spell != null)
				{
					return spell;
				}
				Log.Gameplay.PrintError("{0}.GetAuxiliaryTriggerSpell(): keyword:{1}, spell:{2}", this, triggerKeyword, m_AuxiliaryTriggerSpells[i].m_Spell);
				return null;
			}
		}
		return null;
	}

	private void ActivateAuxiliaryTriggerSpell(int entityId)
	{
		if (m_auxiliaryTriggerSpellByEntityId.ContainsKey(entityId) && !(m_auxiliaryTriggerSpellByEntityId[entityId] == null))
		{
			m_auxiliaryTriggerSpellByEntityId[entityId].ActivateState(SpellStateType.ACTION);
		}
	}

	protected override float GetLostFrameTimeCatchUpSeconds()
	{
		return 0.2f;
	}
}
