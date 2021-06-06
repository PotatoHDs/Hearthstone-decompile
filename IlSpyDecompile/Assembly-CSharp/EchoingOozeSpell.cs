using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoingOozeSpell : Spell
{
	public Spell m_CustomSpawnSpell;

	public float m_PostSpawnDelayMin;

	public float m_PostSpawnDelayMax;

	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.HistFullEntity histFullEntity = task.GetPower() as Network.HistFullEntity;
		if (histFullEntity == null)
		{
			return null;
		}
		Network.Entity entity = histFullEntity.Entity;
		Entity entity2 = GameState.Get().GetEntity(entity.ID);
		if (entity2 == null)
		{
			Debug.LogWarning($"{this}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {entity.ID} but there is no entity with that id");
			return null;
		}
		return entity2.GetCard();
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		Card targetCard = GetTargetCard();
		if (targetCard == null)
		{
			OnStateFinished();
		}
		else
		{
			DoEffect(targetCard);
		}
	}

	private void DoEffect(Card targetCard)
	{
		Spell spell = Object.Instantiate(m_CustomSpawnSpell);
		targetCard.OverrideCustomSpawnSpell(spell);
		DoTasksUntilSpawn(targetCard);
		StartCoroutine(WaitThenFinish());
	}

	private void DoTasksUntilSpawn(Card targetCard)
	{
		int entityId = targetCard.GetEntity().GetEntityId();
		List<PowerTask> taskList = m_taskList.GetTaskList();
		int num = 0;
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistFullEntity histFullEntity = taskList[i].GetPower() as Network.HistFullEntity;
			if (histFullEntity != null && histFullEntity.Entity.ID == entityId)
			{
				num = i;
				break;
			}
		}
		m_taskList.DoTasks(0, num + 1);
	}

	private IEnumerator WaitThenFinish()
	{
		float num = Random.Range(m_PostSpawnDelayMin, m_PostSpawnDelayMax);
		if (!Mathf.Approximately(num, 0f))
		{
			yield return new WaitForSeconds(num);
		}
		OnStateFinished();
	}
}
