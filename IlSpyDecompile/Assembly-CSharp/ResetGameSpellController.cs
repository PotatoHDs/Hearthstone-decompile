using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameSpellController : SpellController
{
	public Spell m_DefaultHideScreenSpell;

	private static Spell s_hideScreenSpellInstance;

	private int m_resetGameTaskIndex;

	private Entity m_clonedSourceEntity;

	private Entity m_prevGameEntity;

	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		s_hideScreenSpellInstance = null;
		Entity sourceEntity = taskList.GetSourceEntity();
		if (sourceEntity != null)
		{
			Card card = sourceEntity.GetCard();
			CardEffect cardEffect = InitEffect(card);
			if (cardEffect != null)
			{
				s_hideScreenSpellInstance = InitResetGameSpell(cardEffect, card);
			}
		}
		if (!taskList.IsStartOfBlock() || !taskList.IsEndOfBlock())
		{
			Log.Gameplay.PrintWarning($"{this}.AddPowerSourceAndTargets(): ResetGame power block was split into multiple tasklists.");
		}
		m_resetGameTaskIndex = -1;
		List<PowerTask> taskList2 = m_taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			if (taskList2[i].GetPower() is Network.HistResetGame)
			{
				m_resetGameTaskIndex = i;
				break;
			}
		}
		if (m_clonedSourceEntity == null && taskList.GetSourceEntity() != null)
		{
			m_clonedSourceEntity = taskList.GetSourceEntity().CloneForZoneMgr();
		}
		return true;
	}

	protected override void OnProcessTaskList()
	{
		StartCoroutine(DoEffectsWithTiming());
	}

	private IEnumerator DoEffectsWithTiming()
	{
		if (m_taskList.IsStartOfBlock())
		{
			if (m_prevGameEntity == null)
			{
				m_prevGameEntity = GameState.Get().GetGameEntity();
			}
			GameState.Get().GetGameEntity().NotifyOfResetGameStarted();
		}
		if (m_resetGameTaskIndex != -1)
		{
			if (s_hideScreenSpellInstance == null)
			{
				s_hideScreenSpellInstance = Object.Instantiate(m_DefaultHideScreenSpell);
			}
			s_hideScreenSpellInstance.ActivateState(SpellStateType.BIRTH);
			while (s_hideScreenSpellInstance.GetActiveState() != SpellStateType.IDLE)
			{
				yield return null;
			}
			PowerTask resetGameTask = m_taskList.GetTaskList()[m_resetGameTaskIndex];
			m_taskList.DoTasks(0, m_resetGameTaskIndex + 1);
			while (!resetGameTask.IsCompleted())
			{
				yield return null;
			}
		}
		List<Card> recreatedCards = new List<Card>();
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = m_resetGameTaskIndex; i < taskList.Count; i++)
		{
			Network.HistFullEntity histFullEntity = taskList[i].GetPower() as Network.HistFullEntity;
			if (histFullEntity == null)
			{
				continue;
			}
			Entity entity = GameState.Get().GetEntity(histFullEntity.Entity.ID);
			if (entity != null)
			{
				Card card = entity.GetCard();
				if (!(card == null))
				{
					card.SuppressPlaySounds(suppress: true);
					card.SetTransitionStyle(ZoneTransitionStyle.INSTANT);
					recreatedCards.Add(card);
				}
			}
		}
		m_taskList.DoAllTasks();
		while (!m_taskList.IsComplete())
		{
			yield return null;
		}
		foreach (Card item in recreatedCards)
		{
			item.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
			item.SuppressPlaySounds(suppress: false);
			Entity entity2 = item.GetEntity();
			TAG_ZONE zone = entity2.GetZone();
			if (zone == TAG_ZONE.PLAY || zone == TAG_ZONE.SECRET)
			{
				item.ShowExhaustedChange(entity2.IsExhausted());
			}
		}
		if (m_taskList.IsEndOfBlock())
		{
			EndTurnButton.Get().Reset();
			s_hideScreenSpellInstance.ActivateState(SpellStateType.DEATH);
			while (s_hideScreenSpellInstance.GetActiveState() != 0)
			{
				yield return null;
			}
			Object.Destroy(s_hideScreenSpellInstance);
			s_hideScreenSpellInstance = null;
			GameState.Get().GetGameEntity().NotifyOfResetGameFinished(m_clonedSourceEntity, m_prevGameEntity);
			m_prevGameEntity = null;
		}
		OnFinishedTaskList();
		OnFinished();
	}

	private CardEffect InitEffect(Card card)
	{
		if (card == null)
		{
			return null;
		}
		int effectIndex = m_taskList.GetBlockStart().EffectIndex;
		if (effectIndex < 0)
		{
			return null;
		}
		return card.GetResetGameEffect(effectIndex);
	}

	private Spell InitResetGameSpell(CardEffect effect, Card card)
	{
		Spell spell = effect.GetSpell();
		if (spell == null)
		{
			return null;
		}
		if (!spell.AttachPowerTaskList(m_taskList))
		{
			Log.Power.Print("{0}.InitResetGameSpell() - FAILED to add targets to spell for {1}", this, card);
			return null;
		}
		return spell;
	}
}
