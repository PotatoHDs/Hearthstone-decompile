using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

public class DefileSpell : SuperSpell
{
	public Spell m_SpellPrefab;

	public float m_TimeBetweenCasts = 1f;

	private List<GameObject> m_singleCastTargets;

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		StartCoroutine(DefileEffect());
	}

	private void FindTargetsForSingleCast(int index)
	{
		m_singleCastTargets.Clear();
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = index + 1; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			if (power.Type != Network.PowerType.META_DATA)
			{
				continue;
			}
			Network.HistMetaData histMetaData = (Network.HistMetaData)power;
			if (histMetaData.MetaType == HistoryMeta.Type.EFFECT_TIMING)
			{
				break;
			}
			if (histMetaData.MetaType != 0 || histMetaData.Info == null || histMetaData.Info.Count == 0)
			{
				continue;
			}
			for (int j = 0; j < histMetaData.Info.Count; j++)
			{
				Entity entity = GameState.Get().GetEntity(histMetaData.Info[j]);
				if (entity != null)
				{
					Card card = entity.GetCard();
					m_singleCastTargets.Add(card.gameObject);
				}
			}
		}
	}

	private IEnumerator DefileEffect()
	{
		m_singleCastTargets = new List<GameObject>();
		Card sourceCard = m_taskList.GetSourceEntity().GetCard();
		List<PowerTask> tasks = m_taskList.GetTaskList();
		int i = 0;
		while (i < tasks.Count)
		{
			Network.PowerHistory power = tasks[i].GetPower();
			if (power.Type == Network.PowerType.META_DATA && ((Network.HistMetaData)power).MetaType == HistoryMeta.Type.EFFECT_TIMING)
			{
				bool complete = false;
				PowerTaskList.CompleteCallback callback = delegate
				{
					complete = true;
				};
				FindTargetsForSingleCast(i);
				m_taskList.DoTasks(0, i, callback);
				while (!complete)
				{
					yield return null;
				}
				if (m_SpellPrefab != null)
				{
					m_effectsPendingFinish++;
					Spell spell = CloneSpell(m_SpellPrefab);
					spell.SetSource(sourceCard.gameObject);
					spell.AddTargets(m_singleCastTargets);
					spell.ActivateState(SpellStateType.ACTION);
					while (!spell.IsFinished())
					{
						yield return null;
					}
				}
				yield return new WaitForSeconds(m_TimeBetweenCasts);
			}
			int num = i + 1;
			i = num;
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
	}
}
