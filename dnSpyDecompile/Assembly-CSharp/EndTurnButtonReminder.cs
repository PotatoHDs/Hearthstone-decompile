using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000306 RID: 774
public class EndTurnButtonReminder : MonoBehaviour
{
	// Token: 0x060029B1 RID: 10673 RVA: 0x000D3DF8 File Offset: 0x000D1FF8
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
		List<Card> list = this.GenerateCardsToRemindList(gameState, zonePlay.GetCards());
		if (list.Count == 0)
		{
			return true;
		}
		this.PlayReminders(list);
		return true;
	}

	// Token: 0x060029B2 RID: 10674 RVA: 0x000D3E74 File Offset: 0x000D2074
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

	// Token: 0x060029B3 RID: 10675 RVA: 0x000D3EB8 File Offset: 0x000D20B8
	private void PlayReminders(List<Card> cards)
	{
		int num;
		Card item;
		do
		{
			num = UnityEngine.Random.Range(0, cards.Count);
			item = cards[num];
		}
		while (this.m_cardsWaitingToRemind.Contains(item));
		for (int i = 0; i < cards.Count; i++)
		{
			Card card = cards[i];
			Spell actorSpell = card.GetActorSpell(SpellType.WIGGLE, true);
			if (!(actorSpell == null) && actorSpell.GetActiveState() == SpellStateType.NONE && !this.m_cardsWaitingToRemind.Contains(card))
			{
				if (i == num)
				{
					actorSpell.Activate();
				}
				else
				{
					float num2 = UnityEngine.Random.Range(0f, this.m_MaxDelaySec);
					if (Mathf.Approximately(num2, 0f))
					{
						actorSpell.Activate();
					}
					else
					{
						this.m_cardsWaitingToRemind.Add(card);
						base.StartCoroutine(this.WaitAndPlayReminder(card, actorSpell, num2));
					}
				}
			}
		}
	}

	// Token: 0x060029B4 RID: 10676 RVA: 0x000D3F87 File Offset: 0x000D2187
	private IEnumerator WaitAndPlayReminder(Card card, Spell reminderSpell, float delay)
	{
		yield return new WaitForSeconds(delay);
		if (!GameState.Get().IsFriendlySidePlayerTurn())
		{
			yield break;
		}
		if (!(card.GetZone() is ZonePlay))
		{
			yield break;
		}
		reminderSpell.Activate();
		this.m_cardsWaitingToRemind.Remove(card);
		yield break;
	}

	// Token: 0x040017A4 RID: 6052
	public float m_MaxDelaySec = 0.3f;

	// Token: 0x040017A5 RID: 6053
	private List<Card> m_cardsWaitingToRemind = new List<Card>();
}
