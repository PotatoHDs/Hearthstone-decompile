using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpellController : SpellController
{
	private Spell m_powerSpell;

	private List<CardSoundSpell> m_powerSoundSpells = new List<CardSoundSpell>();

	private int m_cardEffectsBlockingFinish;

	private int m_cardEffectsBlockingTaskListFinish;

	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		if (!HasSourceCard(taskList))
		{
			return false;
		}
		Entity sourceEntity = taskList.GetSourceEntity();
		Card card = sourceEntity.GetCard();
		CardEffect orCreateEffect = GetOrCreateEffect(card, m_taskList);
		if (orCreateEffect == null)
		{
			return false;
		}
		if (sourceEntity.IsMinion() || sourceEntity.IsHero())
		{
			if (!InitPowerSpell(orCreateEffect, card))
			{
				if (!SpellUtils.CanAddPowerTargets(taskList))
				{
					Reset();
					return false;
				}
				if (GetActorBattlecrySpell(card) == null)
				{
					Reset();
					return false;
				}
			}
		}
		else
		{
			InitPowerSpell(orCreateEffect, card);
			InitPowerSounds(orCreateEffect, card);
			if (m_powerSpell == null && m_powerSoundSpells.Count == 0)
			{
				Reset();
				return false;
			}
		}
		SetSource(card);
		return true;
	}

	protected override void OnProcessTaskList()
	{
		if (!ActivateActorBattlecrySpell() && !ActivateCardEffects())
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
		if (m_powerSpell != null)
		{
			return m_powerSpell.ShouldReconnectIfStuck();
		}
		return base.ShouldReconnectIfStuck();
	}

	private void Reset()
	{
		if (m_powerSpell != null && m_powerSpell.GetPowerTaskList().GetId() == m_taskListId)
		{
			SpellUtils.PurgeSpell(m_powerSpell);
		}
		if (m_powerSoundSpells != null)
		{
			for (int i = 0; i < m_powerSoundSpells.Count; i++)
			{
				CardSoundSpell cardSoundSpell = m_powerSoundSpells[i];
				if (cardSoundSpell != null && cardSoundSpell.GetPowerTaskList().GetId() == m_taskListId)
				{
					SpellUtils.PurgeSpell(cardSoundSpell);
				}
			}
		}
		m_powerSpell = null;
		m_powerSoundSpells.Clear();
		m_cardEffectsBlockingFinish = 0;
		m_cardEffectsBlockingTaskListFinish = 0;
	}

	private IEnumerator WaitThenFinish()
	{
		yield return new WaitForSeconds(10f);
		Reset();
		base.OnFinished();
	}

	private Spell GetActorBattlecrySpell(Card card)
	{
		Spell actorSpell = card.GetActorSpell(SpellType.BATTLECRY);
		if (actorSpell == null)
		{
			return null;
		}
		if (!actorSpell.HasUsableState(SpellStateType.ACTION))
		{
			return null;
		}
		return actorSpell;
	}

	private bool ActivateActorBattlecrySpell()
	{
		Card source = GetSource();
		Entity entity = source.GetEntity();
		if (!CanActivateActorBattlecrySpell(entity))
		{
			return false;
		}
		Spell actorBattlecrySpell = GetActorBattlecrySpell(source);
		if (actorBattlecrySpell == null)
		{
			return false;
		}
		m_taskList.SetActivateBattlecrySpellState();
		StartCoroutine(WaitThenActivateActorBattlecrySpell(actorBattlecrySpell));
		return true;
	}

	private bool CanActivateActorBattlecrySpell(Entity entity)
	{
		if (!m_taskList.ShouldActivateBattlecrySpell())
		{
			return false;
		}
		if (entity.GetZone() != TAG_ZONE.PLAY)
		{
			return false;
		}
		if (entity.HasTag(GAME_TAG.FAST_BATTLECRY))
		{
			return false;
		}
		if (entity.HasBattlecry())
		{
			return true;
		}
		if (entity.HasCombo() && entity.GetController().IsComboActive())
		{
			return true;
		}
		return false;
	}

	private IEnumerator WaitThenActivateActorBattlecrySpell(Spell actorBattlecrySpell)
	{
		yield return new WaitForSeconds(0.2f);
		actorBattlecrySpell.ActivateState(SpellStateType.ACTION);
		if (!ActivateCardEffects())
		{
			base.OnProcessTaskList();
		}
	}

	public static CardEffect GetOrCreateEffect(Card card, PowerTaskList taskList)
	{
		if (card == null)
		{
			return null;
		}
		CardEffect cardEffect = null;
		Network.HistBlockStart blockStart = taskList.GetBlockStart();
		string effectCardId = taskList.GetEffectCardId();
		int subOption = blockStart.SubOption;
		int effectIndex = blockStart.EffectIndex;
		string text = card.GetEntity()?.GetCardId();
		if (string.IsNullOrEmpty(effectCardId) || string.IsNullOrEmpty(text) || text == effectCardId)
		{
			if (subOption >= 0)
			{
				return card.GetSubOptionEffect(subOption, effectIndex);
			}
			return card.GetPlayEffect(effectIndex);
		}
		using DefLoader.DisposableCardDef disposableCardDef = DefLoader.Get().GetCardDef(effectCardId);
		CardEffectDef cardEffectDef = null;
		if (subOption >= 0)
		{
			if (effectIndex > 0)
			{
				if (disposableCardDef.CardDef.m_AdditionalSubOptionEffectDefs == null)
				{
					return null;
				}
				if (subOption >= disposableCardDef.CardDef.m_AdditionalSubOptionEffectDefs.Count)
				{
					return null;
				}
				List<CardEffectDef> list = disposableCardDef.CardDef.m_AdditionalSubOptionEffectDefs[subOption];
				effectIndex--;
				if (effectIndex >= list.Count)
				{
					return null;
				}
				cardEffectDef = list[effectIndex];
			}
			else
			{
				if (disposableCardDef.CardDef.m_SubOptionEffectDefs == null)
				{
					return null;
				}
				if (subOption >= disposableCardDef.CardDef.m_SubOptionEffectDefs.Count)
				{
					return null;
				}
				cardEffectDef = disposableCardDef.CardDef.m_SubOptionEffectDefs[subOption];
			}
		}
		else if (effectIndex > 0)
		{
			if (disposableCardDef.CardDef.m_AdditionalPlayEffectDefs == null)
			{
				return null;
			}
			effectIndex--;
			if (effectIndex >= disposableCardDef.CardDef.m_AdditionalPlayEffectDefs.Count)
			{
				return null;
			}
			cardEffectDef = disposableCardDef.CardDef.m_AdditionalPlayEffectDefs[effectIndex];
		}
		else
		{
			cardEffectDef = disposableCardDef.CardDef.m_PlayEffectDef;
		}
		return card.GetOrCreateProxyEffect(blockStart, cardEffectDef);
	}

	private bool ActivateCardEffects()
	{
		bool num = ActivatePowerSpell();
		bool flag = ActivatePowerSounds();
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
			OnFinishedTaskList();
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

	private bool InitPowerSpell(CardEffect effect, Card card)
	{
		Spell spell = effect.GetSpell();
		if (spell == null)
		{
			return false;
		}
		if (!spell.HasUsableState(SpellStateType.ACTION))
		{
			Log.Power.PrintWarning("{0}.InitPowerSpell() - spell {1} for Card {2} has no {3} state", base.name, spell, card, SpellStateType.ACTION);
			return false;
		}
		if (!spell.AttachPowerTaskList(m_taskList))
		{
			Log.Power.Print("{0}.InitPowerSpell() - FAILED to attach task list to spell {1} for Card {2}", base.name, spell, card);
			return false;
		}
		if (spell.GetActiveState() != 0)
		{
			spell.ActivateState(SpellStateType.NONE);
		}
		m_powerSpell = spell;
		m_cardEffectsBlockingFinish++;
		m_cardEffectsBlockingTaskListFinish++;
		return true;
	}

	private bool ActivatePowerSpell()
	{
		if (m_powerSpell == null)
		{
			return false;
		}
		m_powerSpell.AddFinishedCallback(OnCardSpellFinished);
		m_powerSpell.AddStateFinishedCallback(OnCardSpellStateFinished);
		m_powerSpell.ActivateState(SpellStateType.ACTION);
		return true;
	}

	private bool InitPowerSounds(CardEffect effect, Card card)
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
					Log.Power.Print("{0}.InitPowerSounds() - FAILED to attach task list to PowerSoundSpell {1} for Card {2}", base.name, item, card);
				}
				else
				{
					m_powerSoundSpells.Add(item);
				}
			}
		}
		if (m_powerSoundSpells.Count == 0)
		{
			return false;
		}
		m_cardEffectsBlockingFinish++;
		m_cardEffectsBlockingTaskListFinish++;
		return true;
	}

	private bool ActivatePowerSounds()
	{
		if (m_powerSoundSpells.Count == 0)
		{
			return false;
		}
		Card source = GetSource();
		foreach (CardSoundSpell powerSoundSpell in m_powerSoundSpells)
		{
			if ((bool)powerSoundSpell)
			{
				source.ActivateSoundSpell(powerSoundSpell);
			}
		}
		CardSpellFinished();
		CardSpellNoneStateEntered();
		return true;
	}
}
