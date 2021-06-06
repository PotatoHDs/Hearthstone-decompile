using System;
using System.Collections;
using UnityEngine;

// Token: 0x020007FD RID: 2045
public class IdleSuperSpell : SuperSpell
{
	// Token: 0x06006F13 RID: 28435 RVA: 0x0023C721 File Offset: 0x0023A921
	public override bool AddPowerTargets()
	{
		this.m_playSuperSpellVisuals = base.AddPowerTargets();
		return true;
	}

	// Token: 0x06006F14 RID: 28436 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void DoActionPreTasks()
	{
	}

	// Token: 0x06006F15 RID: 28437 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void DoActionPostTasks()
	{
	}

	// Token: 0x06006F16 RID: 28438 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool HasPendingTasks()
	{
		return false;
	}

	// Token: 0x06006F17 RID: 28439 RVA: 0x0023C730 File Offset: 0x0023A930
	protected override void OnAction(SpellStateType prevStateType)
	{
		if (this.m_idleSpellInstance == null && (this.m_targets.Count > 0 || this.m_playIdleSpellWithoutTargets))
		{
			this.m_hasIdlePlayedForMinTime = false;
			base.StartCoroutine(this.DoIdleSpell(prevStateType));
			return;
		}
		this.DoActionPreTasks();
		if (this.m_playSuperSpellVisuals)
		{
			base.OnAction(prevStateType);
		}
		else
		{
			this.OnStateFinished();
		}
		this.DoActionPostTasks();
	}

	// Token: 0x06006F18 RID: 28440 RVA: 0x0023C79A File Offset: 0x0023A99A
	public override bool CanPurge()
	{
		return (this.m_taskList == null || this.m_taskList.IsEndOfBlock()) && (!(this.m_idleSpellInstance != null) || !this.m_idleSpellInstance.IsActive());
	}

	// Token: 0x06006F19 RID: 28441 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldReconnectIfStuck()
	{
		return false;
	}

	// Token: 0x06006F1A RID: 28442 RVA: 0x0023C7D1 File Offset: 0x0023A9D1
	private IEnumerator DoIdleSpell(SpellStateType prevStateType)
	{
		Actor actor = base.GetSourceCard().GetActor();
		this.m_idleSpellInstance = UnityEngine.Object.Instantiate<Spell>(this.m_idleSpell);
		SpellUtils.SetCustomSpellParent(this.m_idleSpellInstance, this);
		if (actor != null)
		{
			TransformUtil.AttachAndPreserveLocalTransform(this.m_idleSpellInstance.transform, actor.transform);
		}
		this.m_idleSpellInstance.SetSource(base.GetSource());
		this.m_idleSpellInstance.AddFinishedCallback(new Spell.FinishedCallback(this.OnIdleSpellFinished));
		this.m_idleSpellInstance.Activate();
		yield return new WaitForSeconds(this.m_waitTimeBeforeSuperSpellVisuals);
		this.DoActionPreTasks();
		if (this.m_playSuperSpellVisuals)
		{
			base.OnAction(prevStateType);
		}
		else
		{
			this.OnStateFinished();
		}
		this.DoActionPostTasks();
		yield return new WaitForSeconds(this.m_minTimeIdleIsPlaying);
		this.m_hasIdlePlayedForMinTime = true;
		while (!this.TryIdleFinish())
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x06006F1B RID: 28443 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnIdleSpellFinished(Spell spell, object userData)
	{
	}

	// Token: 0x06006F1C RID: 28444 RVA: 0x0023C7E7 File Offset: 0x0023A9E7
	public override void OnSpellFinished()
	{
		this.TryIdleFinish();
		base.OnSpellFinished();
	}

	// Token: 0x06006F1D RID: 28445 RVA: 0x0023C7F8 File Offset: 0x0023A9F8
	private bool TryIdleFinish()
	{
		if (this.m_taskList != null && !this.m_taskList.IsEndOfBlock())
		{
			for (PowerTaskList powerTaskList = this.m_taskList; powerTaskList != null; powerTaskList = powerTaskList.GetNext())
			{
				if (powerTaskList.HasTasks() && !powerTaskList.AreTasksComplete())
				{
					return false;
				}
			}
		}
		if (this.HasPendingTasks())
		{
			return false;
		}
		if (this.m_idleSpellInstance == null || this.m_idleSpellInstance.GetActiveState() == SpellStateType.DEATH || this.m_idleSpellInstance.GetActiveState() == SpellStateType.NONE)
		{
			return true;
		}
		if (!this.m_hasIdlePlayedForMinTime)
		{
			return false;
		}
		this.m_idleSpellInstance.ActivateState(SpellStateType.DEATH);
		return true;
	}

	// Token: 0x04005919 RID: 22809
	public Spell m_idleSpell;

	// Token: 0x0400591A RID: 22810
	public float m_waitTimeBeforeSuperSpellVisuals = 1.5f;

	// Token: 0x0400591B RID: 22811
	public float m_minTimeIdleIsPlaying = 1.5f;

	// Token: 0x0400591C RID: 22812
	public bool m_playIdleSpellWithoutTargets;

	// Token: 0x0400591D RID: 22813
	private Spell m_idleSpellInstance;

	// Token: 0x0400591E RID: 22814
	private bool m_playSuperSpellVisuals;

	// Token: 0x0400591F RID: 22815
	private bool m_hasIdlePlayedForMinTime = true;
}
