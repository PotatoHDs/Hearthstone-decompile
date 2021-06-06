using System;
using UnityEngine;

// Token: 0x020007CD RID: 1997
public class AncientCurseSpell : SuperSpell
{
	// Token: 0x06006DFC RID: 28156 RVA: 0x00237338 File Offset: 0x00235538
	public void DoHeroDamage()
	{
		PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
		if (currentTaskList == null)
		{
			Debug.LogWarning("AncientCurseSpell.DoHeroDamage() called when there was no current PowerTaskList!");
			return;
		}
		GameUtils.DoDamageTasks(currentTaskList, base.GetSourceCard(), this.GetVisualTargetCard());
	}
}
