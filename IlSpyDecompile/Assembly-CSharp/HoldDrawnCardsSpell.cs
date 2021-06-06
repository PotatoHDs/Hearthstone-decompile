using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

public class HoldDrawnCardsSpell : SuperSpell
{
	public float m_PreEffectHoldTime;

	public float m_PostEffectHoldTime;

	public Spell m_DrawnCardSpell;

	private SortedList<int, Card> m_drawCardData = new SortedList<int, Card>();

	private List<Spell> m_drawnCardSpellInstances = new List<Spell>();

	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		if (!base.AttachPowerTaskList(taskList))
		{
			return false;
		}
		FindHoldDrawnCardMetaDataTasks();
		return true;
	}

	private void FindHoldDrawnCardMetaDataTasks()
	{
		m_drawCardData.Clear();
		if (m_taskList == null)
		{
			return;
		}
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistMetaData histMetaData = taskList[i].GetPower() as Network.HistMetaData;
			if (histMetaData == null || histMetaData.MetaType != HistoryMeta.Type.HOLD_DRAWN_CARD || histMetaData.Info.Count != 1)
			{
				continue;
			}
			Entity entity = GameState.Get().GetEntity(histMetaData.Info[0]);
			if (entity != null)
			{
				Card card = entity.GetCard();
				if (!(card == null))
				{
					m_drawCardData.Add(i, card);
				}
			}
		}
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		StartCoroutine(DrawCardsWithEffects());
	}

	private IEnumerator DrawCardsWithEffects()
	{
		int drawnCardIndex = 0;
		while (drawnCardIndex < m_drawCardData.Count)
		{
			int holdDrawMetaDataTaskIndex = m_drawCardData.Keys[drawnCardIndex];
			Card drawnCard = m_drawCardData.Values[drawnCardIndex];
			if (TurnStartManager.Get().IsCardDrawHandled(drawnCard))
			{
				TurnStartManager.Get().DrawCardImmediately(drawnCard);
			}
			bool complete = false;
			m_taskList.DoTasks(0, holdDrawMetaDataTaskIndex + 1, delegate
			{
				complete = true;
			});
			while (!complete)
			{
				yield return null;
			}
			m_taskList.GetTaskList()[holdDrawMetaDataTaskIndex].SetCompleted(complete: false);
			while (!drawnCard.IsActorReady())
			{
				yield return null;
			}
			yield return new WaitForSeconds(m_PreEffectHoldTime);
			if (m_DrawnCardSpell != null)
			{
				Spell spell2 = Object.Instantiate(m_DrawnCardSpell);
				SpellUtils.SetCustomSpellParent(spell2, this);
				spell2.SetSource(GetSource());
				spell2.AddTarget(drawnCard.gameObject);
				spell2.Activate();
				m_drawnCardSpellInstances.Add(spell2);
			}
			int count2 = m_taskList.GetTaskList().Count;
			if (drawnCardIndex + 1 < m_drawCardData.Count)
			{
				count2 = m_drawCardData.Keys[drawnCardIndex + 1] - holdDrawMetaDataTaskIndex - 1;
			}
			m_taskList.DoTasks(holdDrawMetaDataTaskIndex + 1, count2);
			yield return new WaitForSeconds(m_PostEffectHoldTime);
			m_taskList.GetTaskList()[holdDrawMetaDataTaskIndex].SetCompleted(complete: true);
			int num = drawnCardIndex + 1;
			drawnCardIndex = num;
		}
		foreach (Spell spell in m_drawnCardSpellInstances)
		{
			if (!(spell == null))
			{
				while (!spell.CanPurge())
				{
					yield return null;
				}
				SpellUtils.PurgeSpell(spell);
			}
		}
		m_drawnCardSpellInstances.Clear();
		m_effectsPendingFinish--;
		FinishIfPossible();
	}
}
