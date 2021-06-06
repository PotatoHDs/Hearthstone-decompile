using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x0200081F RID: 2079
public class SherazinSeedMorphSpell : SuperSpell
{
	// Token: 0x06006FE7 RID: 28647 RVA: 0x002419E3 File Offset: 0x0023FBE3
	public override bool AddPowerTargets()
	{
		if (!base.CanAddPowerTargets())
		{
			return false;
		}
		if (this.m_taskList.GetBlockType() != HistoryBlock.Type.TRIGGER)
		{
			return false;
		}
		if (!this.FindSherazinChange())
		{
			return false;
		}
		this.m_sherazinCard = base.GetSourceCard();
		return true;
	}

	// Token: 0x06006FE8 RID: 28648 RVA: 0x00241A18 File Offset: 0x0023FC18
	private bool FindSherazinChange()
	{
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		this.m_newSherazinChangeTaskIndex = -1;
		for (int i = 0; i < taskList.Count; i++)
		{
			if (taskList[i].GetPower() is Network.HistChangeEntity)
			{
				this.m_newSherazinChangeTaskIndex = i;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006FE9 RID: 28649 RVA: 0x00241A66 File Offset: 0x0023FC66
	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		base.AddSpellEventCallback(new Spell.SpellEventCallback(this.OnSpellEvent));
		base.StartCoroutine(this.FlipSeedIntoMinion());
	}

	// Token: 0x06006FEA RID: 28650 RVA: 0x00241A8E File Offset: 0x0023FC8E
	public void OnSpellEvent(string eventName, object eventData, object userData)
	{
		base.StartCoroutine(this.FinishNewSherazinSpawn());
	}

	// Token: 0x06006FEB RID: 28651 RVA: 0x00241A9D File Offset: 0x0023FC9D
	private IEnumerator FlipSeedIntoMinion()
	{
		bool complete = false;
		PowerTaskList.CompleteCallback callback = delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			complete = true;
		};
		this.m_taskList.DoTasks(0, this.m_newSherazinChangeTaskIndex, callback);
		while (!complete)
		{
			yield return null;
		}
		Spell sherazinLeafSpell = this.m_sherazinCard.GetCustomKeywordSpell();
		while (sherazinLeafSpell != null && sherazinLeafSpell.GetActiveState() != SpellStateType.NONE)
		{
			yield return null;
		}
		base.GetComponent<PlayMakerFSM>().SendEvent("DoFlip");
		yield break;
	}

	// Token: 0x06006FEC RID: 28652 RVA: 0x00241AAC File Offset: 0x0023FCAC
	private IEnumerator FinishNewSherazinSpawn()
	{
		bool complete = false;
		PowerTaskList.CompleteCallback callback = delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			complete = true;
		};
		this.m_taskList.DoTasks(this.m_newSherazinChangeTaskIndex, this.m_taskList.GetTaskList().Count - this.m_newSherazinChangeTaskIndex, callback);
		while (!complete)
		{
			yield return null;
		}
		this.m_sherazinCard.GetActor().transform.localPosition = Vector3.zero;
		Spell spell = UnityEngine.Object.Instantiate<Spell>(this.m_CustomSpawnSpell);
		spell.SetSource(this.m_sherazinCard.gameObject);
		spell.RemoveAllTargets();
		spell.AddTarget(this.m_sherazinCard.gameObject);
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnCustomSummonSpellFinished));
		SpellUtils.SetCustomSpellParent(spell, this.m_sherazinCard.GetActor());
		spell.ActivateState(SpellStateType.ACTION);
		this.OnSpellFinished();
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x06006FED RID: 28653 RVA: 0x001FACD3 File Offset: 0x001F8ED3
	private void OnCustomSummonSpellFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(spell.gameObject);
	}

	// Token: 0x040059BD RID: 22973
	private Card m_sherazinCard;

	// Token: 0x040059BE RID: 22974
	private int m_newSherazinChangeTaskIndex;

	// Token: 0x040059BF RID: 22975
	public Spell m_CustomSpawnSpell;
}
