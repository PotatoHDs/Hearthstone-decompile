using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarianWrynn : SuperSpell
{
	public string m_perMinionSound;

	public Spell m_varianSpellPrefab;

	public Spell m_deckSpellPrefab;

	public float m_spellLeadTime = 1f;

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		StartCoroutine(DoVariansCoolThing());
	}

	private IEnumerator DoVariansCoolThing()
	{
		Card card = m_taskList.GetSourceEntity().GetCard();
		List<GameObject> fxObjects = new List<GameObject>();
		if (m_varianSpellPrefab != null && m_taskList.IsOrigin())
		{
			Spell spell2 = Object.Instantiate(m_varianSpellPrefab);
			fxObjects.Add(spell2.gameObject);
			spell2.SetSource(card.gameObject);
			spell2.Activate();
		}
		List<PowerTask> tasks = m_taskList.GetTaskList();
		bool foundTarget = false;
		bool lastWasMinion = false;
		int i = 0;
		while (i < tasks.Count)
		{
			Network.PowerHistory power = tasks[i].GetPower();
			if (power.Type == Network.PowerType.SHOW_ENTITY)
			{
				Network.HistShowEntity showEntity = (Network.HistShowEntity)power;
				if (!foundTarget)
				{
					Card card2 = GameState.Get().GetEntity(showEntity.Entity.ID).GetCard();
					foundTarget = true;
					if (m_deckSpellPrefab != null && m_taskList.IsOrigin())
					{
						Spell spell = Object.Instantiate(m_deckSpellPrefab);
						fxObjects.Add(spell.gameObject);
						spell.SetSource(card2.gameObject);
						spell.Activate();
						while (!spell.IsFinished())
						{
							yield return null;
						}
					}
				}
				bool complete = false;
				PowerTaskList.CompleteCallback callback = delegate
				{
					complete = true;
				};
				m_taskList.DoTasks(0, i, callback);
				if (lastWasMinion)
				{
					yield return new WaitForSeconds(m_spellLeadTime);
				}
				lastWasMinion = IsMinion(showEntity);
				while (!complete)
				{
					yield return null;
				}
			}
			int num = i + 1;
			i = num;
		}
		foreach (GameObject item in fxObjects)
		{
			Object.Destroy(item);
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	private bool IsMinion(Network.HistShowEntity showEntity)
	{
		for (int i = 0; i < showEntity.Entity.Tags.Count; i++)
		{
			Network.Entity.Tag tag = showEntity.Entity.Tags[i];
			if (tag.Name == 202)
			{
				return tag.Value == 4;
			}
		}
		return false;
	}
}
