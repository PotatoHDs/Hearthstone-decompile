using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPowerSwapSpell : Spell
{
	public Spell m_swapFX;

	public override bool AddPowerTargets()
	{
		if (!CanAddPowerTargets())
		{
			return false;
		}
		int entityId = GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetEntityId();
		int entityId2 = GameState.Get().GetOpposingSidePlayer().GetHeroPower()
			.GetEntityId();
		int num = -1;
		int num2 = -1;
		List<PowerTask> taskList = m_taskList.GetTaskList();
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
		if (num < 0)
		{
			return false;
		}
		if (num2 < 0)
		{
			return false;
		}
		return true;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		StartCoroutine(DoActionWithTiming(prevStateType));
	}

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
		if (m_swapFX == null)
		{
			OnSpellFinished();
			OnStateFinished();
			yield break;
		}
		Spell spell2 = Object.Instantiate(m_swapFX);
		SpellUtils.SetCustomSpellParent(spell2, this);
		spell2.SetSource(yourHeroPowerCard.gameObject);
		spell2.AddTarget(theirHeroPowerCard.gameObject);
		spell2.AddFinishedCallback(delegate
		{
			OnSpellFinished();
		});
		spell2.AddStateFinishedCallback(delegate
		{
			OnStateFinished();
		});
		spell2.Activate();
	}
}
