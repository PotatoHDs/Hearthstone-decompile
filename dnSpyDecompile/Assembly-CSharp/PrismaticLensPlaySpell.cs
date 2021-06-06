using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000818 RID: 2072
public class PrismaticLensPlaySpell : Spell
{
	// Token: 0x06006FC1 RID: 28609 RVA: 0x002410D3 File Offset: 0x0023F2D3
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.SetInputEnabled(false);
		base.StartCoroutine(this.DoEffectWithTiming());
		base.OnAction(prevStateType);
	}

	// Token: 0x06006FC2 RID: 28610 RVA: 0x002410F0 File Offset: 0x0023F2F0
	public override void OnSpellFinished()
	{
		this.SetInputEnabled(true);
		base.OnSpellFinished();
	}

	// Token: 0x06006FC3 RID: 28611 RVA: 0x00241100 File Offset: 0x0023F300
	private void SetInputEnabled(bool enabled)
	{
		foreach (GameObject gameObject in this.m_targets)
		{
			gameObject.GetComponent<Card>().SetInputEnabled(enabled);
		}
	}

	// Token: 0x06006FC4 RID: 28612 RVA: 0x00241158 File Offset: 0x0023F358
	private IEnumerator DoEffectWithTiming()
	{
		int num = this.FindTaskCountToRunUntilDraw();
		if (num > 0)
		{
			yield return base.StartCoroutine(this.CompleteTasksUntilDraw(num));
			yield return base.StartCoroutine(this.WaitForDrawing());
			yield return base.StartCoroutine(this.PlayCostSwapSpell());
		}
		else
		{
			this.OnSpellFinished();
			base.Deactivate();
		}
		yield break;
	}

	// Token: 0x06006FC5 RID: 28613 RVA: 0x00241168 File Offset: 0x0023F368
	private int FindTaskCountToRunUntilDraw()
	{
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
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

	// Token: 0x06006FC6 RID: 28614 RVA: 0x002411C7 File Offset: 0x0023F3C7
	private IEnumerator CompleteTasksUntilDraw(int taskCount)
	{
		bool complete = false;
		this.m_taskList.DoTasks(0, taskCount, delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			complete = true;
		});
		while (!complete)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006FC7 RID: 28615 RVA: 0x002411DD File Offset: 0x0023F3DD
	private IEnumerator WaitForDrawing()
	{
		while (this.IsDrawing())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006FC8 RID: 28616 RVA: 0x002411EC File Offset: 0x0023F3EC
	private bool IsDrawing()
	{
		foreach (GameObject gameObject in this.m_targets)
		{
			Card component = gameObject.GetComponent<Card>();
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

	// Token: 0x06006FC9 RID: 28617 RVA: 0x0024126C File Offset: 0x0023F46C
	private IEnumerator PlayCostSwapSpell()
	{
		this.m_swapSpell = UnityEngine.Object.Instantiate<Spell>(this.m_CostSwapSpell);
		this.m_swapSpell.AttachPowerTaskList(base.GetPowerTaskList());
		this.m_swapSpell.SetSource(base.GetSource());
		this.m_swapSpell.ActivateState(SpellStateType.ACTION);
		while (!this.m_swapSpell.IsFinished())
		{
			yield return null;
		}
		this.OnSpellFinished();
		while (this.m_swapSpell.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		UnityEngine.Object.Destroy(this.m_swapSpell);
		this.m_swapSpell = null;
		base.Deactivate();
		yield break;
	}

	// Token: 0x040059A6 RID: 22950
	public Spell m_CostSwapSpell;

	// Token: 0x040059A7 RID: 22951
	private Spell m_swapSpell;

	// Token: 0x040059A8 RID: 22952
	private const string SWAPPED_COST_ENCHANTMENT = "BOT_436e";
}
