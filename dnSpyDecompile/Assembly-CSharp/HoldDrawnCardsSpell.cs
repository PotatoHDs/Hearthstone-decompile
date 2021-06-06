using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x020007FA RID: 2042
public class HoldDrawnCardsSpell : SuperSpell
{
	// Token: 0x06006EF7 RID: 28407 RVA: 0x0023C10B File Offset: 0x0023A30B
	public override bool AttachPowerTaskList(PowerTaskList taskList)
	{
		if (!base.AttachPowerTaskList(taskList))
		{
			return false;
		}
		this.FindHoldDrawnCardMetaDataTasks();
		return true;
	}

	// Token: 0x06006EF8 RID: 28408 RVA: 0x0023C120 File Offset: 0x0023A320
	private void FindHoldDrawnCardMetaDataTasks()
	{
		this.m_drawCardData.Clear();
		if (this.m_taskList == null)
		{
			return;
		}
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			Network.HistMetaData histMetaData = taskList[i].GetPower() as Network.HistMetaData;
			if (histMetaData != null && histMetaData.MetaType == HistoryMeta.Type.HOLD_DRAWN_CARD && histMetaData.Info.Count == 1)
			{
				global::Entity entity = GameState.Get().GetEntity(histMetaData.Info[0]);
				if (entity != null)
				{
					Card card = entity.GetCard();
					if (!(card == null))
					{
						this.m_drawCardData.Add(i, card);
					}
				}
			}
		}
	}

	// Token: 0x06006EF9 RID: 28409 RVA: 0x0023C1C5 File Offset: 0x0023A3C5
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		base.StartCoroutine(this.DrawCardsWithEffects());
	}

	// Token: 0x06006EFA RID: 28410 RVA: 0x0023C1E9 File Offset: 0x0023A3E9
	private IEnumerator DrawCardsWithEffects()
	{
		int num;
		for (int drawnCardIndex = 0; drawnCardIndex < this.m_drawCardData.Count; drawnCardIndex = num)
		{
			HoldDrawnCardsSpell.<>c__DisplayClass8_0 CS$<>8__locals1 = new HoldDrawnCardsSpell.<>c__DisplayClass8_0();
			int holdDrawMetaDataTaskIndex = this.m_drawCardData.Keys[drawnCardIndex];
			Card drawnCard = this.m_drawCardData.Values[drawnCardIndex];
			if (TurnStartManager.Get().IsCardDrawHandled(drawnCard))
			{
				TurnStartManager.Get().DrawCardImmediately(drawnCard);
			}
			CS$<>8__locals1.complete = false;
			this.m_taskList.DoTasks(0, holdDrawMetaDataTaskIndex + 1, delegate(PowerTaskList taskList, int startIndex, int count, object userData)
			{
				CS$<>8__locals1.complete = true;
			});
			while (!CS$<>8__locals1.complete)
			{
				yield return null;
			}
			this.m_taskList.GetTaskList()[holdDrawMetaDataTaskIndex].SetCompleted(false);
			while (!drawnCard.IsActorReady())
			{
				yield return null;
			}
			yield return new WaitForSeconds(this.m_PreEffectHoldTime);
			if (this.m_DrawnCardSpell != null)
			{
				Spell spell2 = UnityEngine.Object.Instantiate<Spell>(this.m_DrawnCardSpell);
				SpellUtils.SetCustomSpellParent(spell2, this);
				spell2.SetSource(base.GetSource());
				spell2.AddTarget(drawnCard.gameObject);
				spell2.Activate();
				this.m_drawnCardSpellInstances.Add(spell2);
			}
			int count2 = this.m_taskList.GetTaskList().Count;
			if (drawnCardIndex + 1 < this.m_drawCardData.Count)
			{
				count2 = this.m_drawCardData.Keys[drawnCardIndex + 1] - holdDrawMetaDataTaskIndex - 1;
			}
			this.m_taskList.DoTasks(holdDrawMetaDataTaskIndex + 1, count2);
			yield return new WaitForSeconds(this.m_PostEffectHoldTime);
			this.m_taskList.GetTaskList()[holdDrawMetaDataTaskIndex].SetCompleted(true);
			CS$<>8__locals1 = null;
			drawnCard = null;
			num = drawnCardIndex + 1;
		}
		foreach (Spell spell in this.m_drawnCardSpellInstances)
		{
			if (!(spell == null))
			{
				while (!spell.CanPurge())
				{
					yield return null;
				}
				SpellUtils.PurgeSpell(spell);
				spell = null;
			}
		}
		List<Spell>.Enumerator enumerator = default(List<Spell>.Enumerator);
		this.m_drawnCardSpellInstances.Clear();
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
		yield break;
		yield break;
	}

	// Token: 0x04005900 RID: 22784
	public float m_PreEffectHoldTime;

	// Token: 0x04005901 RID: 22785
	public float m_PostEffectHoldTime;

	// Token: 0x04005902 RID: 22786
	public Spell m_DrawnCardSpell;

	// Token: 0x04005903 RID: 22787
	private SortedList<int, Card> m_drawCardData = new SortedList<int, Card>();

	// Token: 0x04005904 RID: 22788
	private List<Spell> m_drawnCardSpellInstances = new List<Spell>();
}
