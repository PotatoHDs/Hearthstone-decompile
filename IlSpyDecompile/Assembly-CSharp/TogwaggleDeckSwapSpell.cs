using System.Collections;
using System.Collections.Generic;

public class TogwaggleDeckSwapSpell : SpawnToHandSpell
{
	protected override void OnAction(SpellStateType prevStateType)
	{
		StartCoroutine(DoActionWithTiming(prevStateType));
	}

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
		foreach (Zone item in SpellUtils.FindZonesFromTag(SpellZoneTag.DECK))
		{
			item.AddLayoutBlocker();
		}
		int num = -1;
		List<PowerTask> taskList2 = m_taskList.GetTaskList();
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
			bool complete = false;
			m_taskList.DoTasks(0, num + 1, delegate
			{
				complete = true;
			});
			while (!complete)
			{
				yield return null;
			}
		}
		base.OnBeforeActivateAreaEffectSpell = delegate(Spell spell)
		{
			spell.AddFinishedCallback(OnAEFinished);
			PlayMakerFSM component = spell.GetComponent<PlayMakerFSM>();
			if (component != null)
			{
				component.FsmVariables.GetFsmInt("FriendlyDeckSize").Value = friendlyDeckSize;
				component.FsmVariables.GetFsmInt("OpponentDeckSize").Value = opponentDeckSize;
			}
		};
		base.OnAction(prevStateType);
	}

	private void OnAEFinished(Spell spell, object userData)
	{
		if (spell != m_activeAreaEffectSpell)
		{
			return;
		}
		foreach (Zone item in SpellUtils.FindZonesFromTag(SpellZoneTag.DECK))
		{
			ZoneDeck zoneDeck = item as ZoneDeck;
			if (zoneDeck != null)
			{
				zoneDeck.RemoveLayoutBlocker();
				zoneDeck.SetSuppressEmotes(suppress: true);
				zoneDeck.SetVisibility(visible: true);
				zoneDeck.UpdateLayout();
				zoneDeck.SetSuppressEmotes(suppress: false);
			}
		}
	}
}
