using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButtonReminder : MonoBehaviour
{
	public float m_MaxDelaySec = 0.3f;

	private List<Card> m_cardsWaitingToRemind = new List<Card>();

	public bool ShowFriendlySidePlayerTurnReminder()
	{
		GameState gameState = GameState.Get();
		if (gameState.IsMulliganManagerActive())
		{
			return false;
		}
		Player friendlySidePlayer = gameState.GetFriendlySidePlayer();
		if (friendlySidePlayer == null)
		{
			return false;
		}
		if (!friendlySidePlayer.IsCurrentPlayer())
		{
			return false;
		}
		ZoneMgr zoneMgr = ZoneMgr.Get();
		if (zoneMgr == null)
		{
			return false;
		}
		ZonePlay zonePlay = zoneMgr.FindZoneOfType<ZonePlay>(Player.Side.FRIENDLY);
		if (zonePlay == null)
		{
			return false;
		}
		List<Card> list = GenerateCardsToRemindList(gameState, zonePlay.GetCards());
		if (list.Count == 0)
		{
			return true;
		}
		PlayReminders(list);
		return true;
	}

	private List<Card> GenerateCardsToRemindList(GameState state, List<Card> originalList)
	{
		List<Card> list = new List<Card>();
		for (int i = 0; i < originalList.Count; i++)
		{
			Card card = originalList[i];
			if (state.HasResponse(card.GetEntity()))
			{
				list.Add(card);
			}
		}
		return list;
	}

	private void PlayReminders(List<Card> cards)
	{
		int num;
		Card item;
		do
		{
			num = Random.Range(0, cards.Count);
			item = cards[num];
		}
		while (m_cardsWaitingToRemind.Contains(item));
		for (int i = 0; i < cards.Count; i++)
		{
			Card card = cards[i];
			Spell actorSpell = card.GetActorSpell(SpellType.WIGGLE);
			if (actorSpell == null || actorSpell.GetActiveState() != 0 || m_cardsWaitingToRemind.Contains(card))
			{
				continue;
			}
			if (i == num)
			{
				actorSpell.Activate();
				continue;
			}
			float num2 = Random.Range(0f, m_MaxDelaySec);
			if (Mathf.Approximately(num2, 0f))
			{
				actorSpell.Activate();
				continue;
			}
			m_cardsWaitingToRemind.Add(card);
			StartCoroutine(WaitAndPlayReminder(card, actorSpell, num2));
		}
	}

	private IEnumerator WaitAndPlayReminder(Card card, Spell reminderSpell, float delay)
	{
		yield return new WaitForSeconds(delay);
		if (GameState.Get().IsFriendlySidePlayerTurn() && card.GetZone() is ZonePlay)
		{
			reminderSpell.Activate();
			m_cardsWaitingToRemind.Remove(card);
		}
	}
}
