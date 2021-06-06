using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200082E RID: 2094
public class TogwaggleDeckSwapSpell : SpawnToHandSpell
{
	// Token: 0x06007047 RID: 28743 RVA: 0x0024372B File Offset: 0x0024192B
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.StartCoroutine(this.DoActionWithTiming(prevStateType));
	}

	// Token: 0x06007048 RID: 28744 RVA: 0x0024373B File Offset: 0x0024193B
	private IEnumerator DoActionWithTiming(SpellStateType prevStateType)
	{
		int friendlyDeckSize = 0;
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		if (friendlySidePlayer != null)
		{
			ZoneDeck deckZone = friendlySidePlayer.GetDeckZone();
			if (deckZone != null)
			{
				friendlyDeckSize = deckZone.GetCardCount();
			}
		}
		int opponentDeckSize = 0;
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		if (opposingSidePlayer != null)
		{
			ZoneDeck deckZone2 = opposingSidePlayer.GetDeckZone();
			if (deckZone2 != null)
			{
				opponentDeckSize = deckZone2.GetCardCount();
			}
		}
		foreach (Zone zone in SpellUtils.FindZonesFromTag(SpellZoneTag.DECK))
		{
			zone.AddLayoutBlocker();
		}
		int num = -1;
		List<PowerTask> taskList2 = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList2.Count; i++)
		{
			Network.HistTagChange histTagChange = taskList2[i].GetPower() as Network.HistTagChange;
			if (histTagChange != null)
			{
				bool flag = false;
				if (histTagChange.Tag == 49 && histTagChange.Value == 2)
				{
					flag = true;
				}
				if (histTagChange.Tag == 50)
				{
					flag = true;
				}
				if (flag)
				{
					num = i;
				}
			}
		}
		if (num >= 0)
		{
			TogwaggleDeckSwapSpell.<>c__DisplayClass1_1 CS$<>8__locals2 = new TogwaggleDeckSwapSpell.<>c__DisplayClass1_1();
			CS$<>8__locals2.complete = false;
			this.m_taskList.DoTasks(0, num + 1, delegate(PowerTaskList taskList, int startIndex, int count, object userData)
			{
				CS$<>8__locals2.complete = true;
			});
			while (!CS$<>8__locals2.complete)
			{
				yield return null;
			}
			CS$<>8__locals2 = null;
		}
		base.OnBeforeActivateAreaEffectSpell = delegate(Spell spell)
		{
			spell.AddFinishedCallback(new Spell.FinishedCallback(this.OnAEFinished));
			PlayMakerFSM component = spell.GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.FsmVariables.GetFsmInt("FriendlyDeckSize").Value = friendlyDeckSize;
				component.FsmVariables.GetFsmInt("OpponentDeckSize").Value = opponentDeckSize;
			}
		};
		base.OnAction(prevStateType);
		yield break;
	}

	// Token: 0x06007049 RID: 28745 RVA: 0x00243754 File Offset: 0x00241954
	private void OnAEFinished(Spell spell, object userData)
	{
		if (spell != this.m_activeAreaEffectSpell)
		{
			return;
		}
		foreach (Zone zone in SpellUtils.FindZonesFromTag(SpellZoneTag.DECK))
		{
			ZoneDeck zoneDeck = zone as ZoneDeck;
			if (zoneDeck != null)
			{
				zoneDeck.RemoveLayoutBlocker();
				zoneDeck.SetSuppressEmotes(true);
				zoneDeck.SetVisibility(true);
				zoneDeck.UpdateLayout();
				zoneDeck.SetSuppressEmotes(false);
			}
		}
	}
}
