using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafaamStaffOfOriginationSpell : Spell
{
	public Spell m_CustomSpawnSpell;

	private int m_spawnTaskIndex;

	public override bool AddPowerTargets()
	{
		if (!m_taskList.DoesBlockHaveMetaDataTasks())
		{
			return false;
		}
		m_spawnTaskIndex = -1;
		bool flag = false;
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			Network.HistTagChange histTagChange = power as Network.HistTagChange;
			if (histTagChange != null && histTagChange.Tag == 420)
			{
				flag = true;
				continue;
			}
			Network.HistFullEntity histFullEntity = power as Network.HistFullEntity;
			if (histFullEntity != null && flag)
			{
				Card card = GameState.Get().GetEntity(histFullEntity.Entity.ID).GetCard();
				if (!(card == null))
				{
					m_targets.Add(card.gameObject);
					m_spawnTaskIndex = i;
					break;
				}
			}
		}
		if (m_spawnTaskIndex < 0)
		{
			return false;
		}
		return true;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		ApplyCustomSpawnOverride();
		DoTasksUntilSpawn();
	}

	private void ApplyCustomSpawnOverride()
	{
		foreach (GameObject target in m_targets)
		{
			Card component = target.GetComponent<Card>();
			Spell spell = Object.Instantiate(m_CustomSpawnSpell);
			component.OverrideCustomSpawnSpell(spell);
		}
	}

	private void DoTasksUntilSpawn()
	{
		PowerTaskList.CompleteCallback callback = delegate
		{
			StartCoroutine(WaitThenFinish());
		};
		m_taskList.DoTasks(0, m_spawnTaskIndex, callback);
	}

	private IEnumerator WaitThenFinish()
	{
		Network.HistFullEntity histFullEntity = (Network.HistFullEntity)m_taskList.GetTaskList()[m_spawnTaskIndex].GetPower();
		Card heroPowerCard = GameState.Get().GetEntity(histFullEntity.Entity.ID).GetHeroPowerCard();
		Spell electricSpell = heroPowerCard.GetActorSpell(SpellType.ELECTRIC_CHARGE_LEVEL_LARGE);
		while (!electricSpell.IsFinished())
		{
			yield return null;
		}
		OnStateFinished();
	}
}
