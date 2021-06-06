using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

public class SherazinSeedMorphSpell : SuperSpell
{
	private Card m_sherazinCard;

	private int m_newSherazinChangeTaskIndex;

	public Spell m_CustomSpawnSpell;

	public override bool AddPowerTargets()
	{
		if (!CanAddPowerTargets())
		{
			return false;
		}
		if (m_taskList.GetBlockType() != HistoryBlock.Type.TRIGGER)
		{
			return false;
		}
		if (!FindSherazinChange())
		{
			return false;
		}
		m_sherazinCard = GetSourceCard();
		return true;
	}

	private bool FindSherazinChange()
	{
		List<PowerTask> taskList = m_taskList.GetTaskList();
		m_newSherazinChangeTaskIndex = -1;
		for (int i = 0; i < taskList.Count; i++)
		{
			if (taskList[i].GetPower() is Network.HistChangeEntity)
			{
				m_newSherazinChangeTaskIndex = i;
				return true;
			}
		}
		return false;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		AddSpellEventCallback(OnSpellEvent);
		StartCoroutine(FlipSeedIntoMinion());
	}

	public void OnSpellEvent(string eventName, object eventData, object userData)
	{
		StartCoroutine(FinishNewSherazinSpawn());
	}

	private IEnumerator FlipSeedIntoMinion()
	{
		bool complete = false;
		PowerTaskList.CompleteCallback callback = delegate
		{
			complete = true;
		};
		m_taskList.DoTasks(0, m_newSherazinChangeTaskIndex, callback);
		while (!complete)
		{
			yield return null;
		}
		Spell sherazinLeafSpell = m_sherazinCard.GetCustomKeywordSpell();
		while (sherazinLeafSpell != null && sherazinLeafSpell.GetActiveState() != 0)
		{
			yield return null;
		}
		GetComponent<PlayMakerFSM>().SendEvent("DoFlip");
	}

	private IEnumerator FinishNewSherazinSpawn()
	{
		bool complete = false;
		PowerTaskList.CompleteCallback callback = delegate
		{
			complete = true;
		};
		m_taskList.DoTasks(m_newSherazinChangeTaskIndex, m_taskList.GetTaskList().Count - m_newSherazinChangeTaskIndex, callback);
		while (!complete)
		{
			yield return null;
		}
		m_sherazinCard.GetActor().transform.localPosition = Vector3.zero;
		Spell spell = Object.Instantiate(m_CustomSpawnSpell);
		spell.SetSource(m_sherazinCard.gameObject);
		spell.RemoveAllTargets();
		spell.AddTarget(m_sherazinCard.gameObject);
		spell.AddStateFinishedCallback(OnCustomSummonSpellFinished);
		SpellUtils.SetCustomSpellParent(spell, m_sherazinCard.GetActor());
		spell.ActivateState(SpellStateType.ACTION);
		OnSpellFinished();
		OnStateFinished();
	}

	private void OnCustomSummonSpellFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(spell.gameObject);
		}
	}
}
