using System.Collections;
using UnityEngine;

public class IdleSuperSpell : SuperSpell
{
	public Spell m_idleSpell;

	public float m_waitTimeBeforeSuperSpellVisuals = 1.5f;

	public float m_minTimeIdleIsPlaying = 1.5f;

	public bool m_playIdleSpellWithoutTargets;

	private Spell m_idleSpellInstance;

	private bool m_playSuperSpellVisuals;

	private bool m_hasIdlePlayedForMinTime = true;

	public override bool AddPowerTargets()
	{
		m_playSuperSpellVisuals = base.AddPowerTargets();
		return true;
	}

	protected virtual void DoActionPreTasks()
	{
	}

	protected virtual void DoActionPostTasks()
	{
	}

	protected virtual bool HasPendingTasks()
	{
		return false;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		if (m_idleSpellInstance == null && (m_targets.Count > 0 || m_playIdleSpellWithoutTargets))
		{
			m_hasIdlePlayedForMinTime = false;
			StartCoroutine(DoIdleSpell(prevStateType));
			return;
		}
		DoActionPreTasks();
		if (m_playSuperSpellVisuals)
		{
			base.OnAction(prevStateType);
		}
		else
		{
			OnStateFinished();
		}
		DoActionPostTasks();
	}

	public override bool CanPurge()
	{
		if (m_taskList != null && !m_taskList.IsEndOfBlock())
		{
			return false;
		}
		if (m_idleSpellInstance != null && m_idleSpellInstance.IsActive())
		{
			return false;
		}
		return true;
	}

	public override bool ShouldReconnectIfStuck()
	{
		return false;
	}

	private IEnumerator DoIdleSpell(SpellStateType prevStateType)
	{
		Actor actor = GetSourceCard().GetActor();
		m_idleSpellInstance = Object.Instantiate(m_idleSpell);
		SpellUtils.SetCustomSpellParent(m_idleSpellInstance, this);
		if (actor != null)
		{
			TransformUtil.AttachAndPreserveLocalTransform(m_idleSpellInstance.transform, actor.transform);
		}
		m_idleSpellInstance.SetSource(GetSource());
		m_idleSpellInstance.AddFinishedCallback(OnIdleSpellFinished);
		m_idleSpellInstance.Activate();
		yield return new WaitForSeconds(m_waitTimeBeforeSuperSpellVisuals);
		DoActionPreTasks();
		if (m_playSuperSpellVisuals)
		{
			base.OnAction(prevStateType);
		}
		else
		{
			OnStateFinished();
		}
		DoActionPostTasks();
		yield return new WaitForSeconds(m_minTimeIdleIsPlaying);
		m_hasIdlePlayedForMinTime = true;
		while (!TryIdleFinish())
		{
			yield return null;
		}
	}

	private void OnIdleSpellFinished(Spell spell, object userData)
	{
	}

	public override void OnSpellFinished()
	{
		TryIdleFinish();
		base.OnSpellFinished();
	}

	private bool TryIdleFinish()
	{
		if (m_taskList != null && !m_taskList.IsEndOfBlock())
		{
			for (PowerTaskList powerTaskList = m_taskList; powerTaskList != null; powerTaskList = powerTaskList.GetNext())
			{
				if (powerTaskList.HasTasks() && !powerTaskList.AreTasksComplete())
				{
					return false;
				}
			}
		}
		if (HasPendingTasks())
		{
			return false;
		}
		if (m_idleSpellInstance == null || m_idleSpellInstance.GetActiveState() == SpellStateType.DEATH || m_idleSpellInstance.GetActiveState() == SpellStateType.NONE)
		{
			return true;
		}
		if (!m_hasIdlePlayedForMinTime)
		{
			return false;
		}
		m_idleSpellInstance.ActivateState(SpellStateType.DEATH);
		return true;
	}
}
