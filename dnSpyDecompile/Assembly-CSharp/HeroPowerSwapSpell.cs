using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007F8 RID: 2040
public class HeroPowerSwapSpell : Spell
{
	// Token: 0x06006EE9 RID: 28393 RVA: 0x0023BD58 File Offset: 0x00239F58
	public override bool AddPowerTargets()
	{
		if (!base.CanAddPowerTargets())
		{
			return false;
		}
		int entityId = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetEntityId();
		int entityId2 = GameState.Get().GetOpposingSidePlayer().GetHeroPower().GetEntityId();
		int num = -1;
		int num2 = -1;
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistTagChange histTagChange = taskList[i].GetPower() as Network.HistTagChange;
			if (histTagChange != null && histTagChange.Tag == 50)
			{
				if (histTagChange.Entity == entityId)
				{
					num = i;
				}
				else if (histTagChange.Entity == entityId2)
				{
					num2 = i;
				}
			}
		}
		return num >= 0 && num2 >= 0;
	}

	// Token: 0x06006EEA RID: 28394 RVA: 0x0023BE0D File Offset: 0x0023A00D
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.DoActionWithTiming(prevStateType));
	}

	// Token: 0x06006EEB RID: 28395 RVA: 0x0023BE1D File Offset: 0x0023A01D
	private IEnumerator DoActionWithTiming(SpellStateType prevStateType)
	{
		Card yourHeroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
		Card theirHeroPowerCard = GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard();
		Animation yourAnim = yourHeroPowerCard.GetActor().GetComponent<Animation>();
		Animation theirAnim = theirHeroPowerCard.GetActor().GetComponent<Animation>();
		while (yourAnim.isPlaying || theirAnim.isPlaying)
		{
			yield return null;
		}
		if (this.m_swapFX == null)
		{
			this.OnSpellFinished();
			this.OnStateFinished();
			yield break;
		}
		Spell spell2 = UnityEngine.Object.Instantiate<Spell>(this.m_swapFX);
		SpellUtils.SetCustomSpellParent(spell2, this);
		spell2.SetSource(yourHeroPowerCard.gameObject);
		spell2.AddTarget(theirHeroPowerCard.gameObject);
		spell2.AddFinishedCallback(delegate(Spell spell, object userData)
		{
			this.OnSpellFinished();
		});
		spell2.AddStateFinishedCallback(delegate(Spell spell, SpellStateType previousStateType, object userData)
		{
			this.OnStateFinished();
		});
		spell2.Activate();
		yield break;
	}

	// Token: 0x040058F5 RID: 22773
	public Spell m_swapFX;
}
