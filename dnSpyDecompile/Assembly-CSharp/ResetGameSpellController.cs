using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006CA RID: 1738
public class ResetGameSpellController : SpellController
{
	// Token: 0x06006166 RID: 24934 RVA: 0x001FCBC4 File Offset: 0x001FADC4
	protected override bool AddPowerSourceAndTargets(PowerTaskList taskList)
	{
		ResetGameSpellController.s_hideScreenSpellInstance = null;
		Entity sourceEntity = taskList.GetSourceEntity(true);
		if (sourceEntity != null)
		{
			Card card = sourceEntity.GetCard();
			CardEffect cardEffect = this.InitEffect(card);
			if (cardEffect != null)
			{
				ResetGameSpellController.s_hideScreenSpellInstance = this.InitResetGameSpell(cardEffect, card);
			}
		}
		if (!taskList.IsStartOfBlock() || !taskList.IsEndOfBlock())
		{
			Log.Gameplay.PrintWarning(string.Format("{0}.AddPowerSourceAndTargets(): ResetGame power block was split into multiple tasklists.", this), Array.Empty<object>());
		}
		this.m_resetGameTaskIndex = -1;
		List<PowerTask> taskList2 = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			if (taskList2[i].GetPower() is Network.HistResetGame)
			{
				this.m_resetGameTaskIndex = i;
				break;
			}
		}
		if (this.m_clonedSourceEntity == null && taskList.GetSourceEntity(true) != null)
		{
			this.m_clonedSourceEntity = taskList.GetSourceEntity(true).CloneForZoneMgr();
		}
		return true;
	}

	// Token: 0x06006167 RID: 24935 RVA: 0x001FCC95 File Offset: 0x001FAE95
	protected override void OnProcessTaskList()
	{
		base.StartCoroutine(this.DoEffectsWithTiming());
	}

	// Token: 0x06006168 RID: 24936 RVA: 0x001FCCA4 File Offset: 0x001FAEA4
	private IEnumerator DoEffectsWithTiming()
	{
		if (this.m_taskList.IsStartOfBlock())
		{
			if (this.m_prevGameEntity == null)
			{
				this.m_prevGameEntity = GameState.Get().GetGameEntity();
			}
			GameState.Get().GetGameEntity().NotifyOfResetGameStarted();
		}
		if (this.m_resetGameTaskIndex != -1)
		{
			if (ResetGameSpellController.s_hideScreenSpellInstance == null)
			{
				ResetGameSpellController.s_hideScreenSpellInstance = UnityEngine.Object.Instantiate<Spell>(this.m_DefaultHideScreenSpell);
			}
			ResetGameSpellController.s_hideScreenSpellInstance.ActivateState(SpellStateType.BIRTH);
			while (ResetGameSpellController.s_hideScreenSpellInstance.GetActiveState() != SpellStateType.IDLE)
			{
				yield return null;
			}
			PowerTask resetGameTask = this.m_taskList.GetTaskList()[this.m_resetGameTaskIndex];
			this.m_taskList.DoTasks(0, this.m_resetGameTaskIndex + 1);
			while (!resetGameTask.IsCompleted())
			{
				yield return null;
			}
			resetGameTask = null;
		}
		List<Card> recreatedCards = new List<Card>();
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = this.m_resetGameTaskIndex; i < taskList.Count; i++)
		{
			Network.HistFullEntity histFullEntity = taskList[i].GetPower() as Network.HistFullEntity;
			if (histFullEntity != null)
			{
				Entity entity = GameState.Get().GetEntity(histFullEntity.Entity.ID);
				if (entity != null)
				{
					Card card = entity.GetCard();
					if (!(card == null))
					{
						card.SuppressPlaySounds(true);
						card.SetTransitionStyle(ZoneTransitionStyle.INSTANT);
						recreatedCards.Add(card);
					}
				}
			}
		}
		this.m_taskList.DoAllTasks();
		while (!this.m_taskList.IsComplete())
		{
			yield return null;
		}
		foreach (Card card2 in recreatedCards)
		{
			card2.SetTransitionStyle(ZoneTransitionStyle.NORMAL);
			card2.SuppressPlaySounds(false);
			Entity entity2 = card2.GetEntity();
			TAG_ZONE zone = entity2.GetZone();
			if (zone == TAG_ZONE.PLAY || zone == TAG_ZONE.SECRET)
			{
				card2.ShowExhaustedChange(entity2.IsExhausted());
			}
		}
		if (this.m_taskList.IsEndOfBlock())
		{
			EndTurnButton.Get().Reset();
			ResetGameSpellController.s_hideScreenSpellInstance.ActivateState(SpellStateType.DEATH);
			while (ResetGameSpellController.s_hideScreenSpellInstance.GetActiveState() != SpellStateType.NONE)
			{
				yield return null;
			}
			UnityEngine.Object.Destroy(ResetGameSpellController.s_hideScreenSpellInstance);
			ResetGameSpellController.s_hideScreenSpellInstance = null;
			GameState.Get().GetGameEntity().NotifyOfResetGameFinished(this.m_clonedSourceEntity, this.m_prevGameEntity);
			this.m_prevGameEntity = null;
		}
		this.OnFinishedTaskList();
		this.OnFinished();
		yield break;
	}

	// Token: 0x06006169 RID: 24937 RVA: 0x001FCCB4 File Offset: 0x001FAEB4
	private CardEffect InitEffect(Card card)
	{
		if (card == null)
		{
			return null;
		}
		int effectIndex = this.m_taskList.GetBlockStart().EffectIndex;
		if (effectIndex < 0)
		{
			return null;
		}
		return card.GetResetGameEffect(effectIndex);
	}

	// Token: 0x0600616A RID: 24938 RVA: 0x001FCCEC File Offset: 0x001FAEEC
	private Spell InitResetGameSpell(CardEffect effect, Card card)
	{
		Spell spell = effect.GetSpell(true);
		if (spell == null)
		{
			return null;
		}
		if (!spell.AttachPowerTaskList(this.m_taskList))
		{
			Log.Power.Print("{0}.InitResetGameSpell() - FAILED to add targets to spell for {1}", new object[]
			{
				this,
				card
			});
			return null;
		}
		return spell;
	}

	// Token: 0x04005136 RID: 20790
	public Spell m_DefaultHideScreenSpell;

	// Token: 0x04005137 RID: 20791
	private static Spell s_hideScreenSpellInstance;

	// Token: 0x04005138 RID: 20792
	private int m_resetGameTaskIndex;

	// Token: 0x04005139 RID: 20793
	private Entity m_clonedSourceEntity;

	// Token: 0x0400513A RID: 20794
	private Entity m_prevGameEntity;
}
