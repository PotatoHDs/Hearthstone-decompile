using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismaticLensPlaySpell : Spell
{
	public Spell m_CostSwapSpell;

	private Spell m_swapSpell;

	private const string SWAPPED_COST_ENCHANTMENT = "BOT_436e";

	protected override void OnAction(SpellStateType prevStateType)
	{
		SetInputEnabled(enabled: false);
		StartCoroutine(DoEffectWithTiming());
		base.OnAction(prevStateType);
	}

	public override void OnSpellFinished()
	{
		SetInputEnabled(enabled: true);
		base.OnSpellFinished();
	}

	private void SetInputEnabled(bool enabled)
	{
		foreach (GameObject target in m_targets)
		{
			target.GetComponent<Card>().SetInputEnabled(enabled);
		}
	}

	private IEnumerator DoEffectWithTiming()
	{
		int num = FindTaskCountToRunUntilDraw();
		if (num > 0)
		{
			yield return StartCoroutine(CompleteTasksUntilDraw(num));
			yield return StartCoroutine(WaitForDrawing());
			yield return StartCoroutine(PlayCostSwapSpell());
		}
		else
		{
			OnSpellFinished();
			Deactivate();
		}
	}

	private int FindTaskCountToRunUntilDraw()
	{
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.PowerHistory power = taskList[i].GetPower();
			if (power.Type == Network.PowerType.SHOW_ENTITY && !(((Network.HistShowEntity)power).Entity.CardID != "BOT_436e"))
			{
				return i;
			}
		}
		return -1;
	}

	private IEnumerator CompleteTasksUntilDraw(int taskCount)
	{
		bool complete = false;
		m_taskList.DoTasks(0, taskCount, delegate
		{
			complete = true;
		});
		while (!complete)
		{
			yield return null;
		}
	}

	private IEnumerator WaitForDrawing()
	{
		while (IsDrawing())
		{
			yield return null;
		}
	}

	private bool IsDrawing()
	{
		foreach (GameObject target in m_targets)
		{
			Card component = target.GetComponent<Card>();
			if (!(component.GetZone() is ZoneHand))
			{
				return true;
			}
			if (component.IsDoNotSort())
			{
				return true;
			}
			if (!component.CardStandInIsInteractive())
			{
				return true;
			}
		}
		return false;
	}

	private IEnumerator PlayCostSwapSpell()
	{
		m_swapSpell = Object.Instantiate(m_CostSwapSpell);
		m_swapSpell.AttachPowerTaskList(GetPowerTaskList());
		m_swapSpell.SetSource(GetSource());
		m_swapSpell.ActivateState(SpellStateType.ACTION);
		while (!m_swapSpell.IsFinished())
		{
			yield return null;
		}
		OnSpellFinished();
		while (m_swapSpell.GetActiveState() != 0)
		{
			yield return null;
		}
		Object.Destroy(m_swapSpell);
		m_swapSpell = null;
		Deactivate();
	}
}
