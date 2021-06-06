using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000967 RID: 2407
public class RitualActivateSpell : SuperSpell
{
	// Token: 0x0600845C RID: 33884 RVA: 0x002AD4BE File Offset: 0x002AB6BE
	public void SetHasRitualTriggerSpell(bool hasSpell)
	{
		this.m_hasRitualTriggerSpell = hasSpell;
	}

	// Token: 0x0600845D RID: 33885 RVA: 0x002AD4C8 File Offset: 0x002AB6C8
	public override bool AddPowerTargets()
	{
		this.m_playSuperSpellVisuals = base.AddPowerTargets();
		Player controller = this.m_taskList.GetSourceEntity(true).GetController();
		if (!this.m_ritualSpellConfig.m_showRitualVisualsInPlay && this.m_ritualSpellConfig.IsRitualEntityInPlay(controller))
		{
			this.m_willShowRitualActorVisuals = false;
		}
		int tag = controller.GetTag(this.m_ritualSpellConfig.m_proxyRitualEntityTag);
		this.m_proxyRitualEntity = GameState.Get().GetEntity(tag);
		if (this.m_proxyRitualEntity == null)
		{
			Log.Spells.PrintError("RitualActivateSpell.AddPowerTargets(): Failed to get proxy ritual entity. Unable to display visuals. Proxy ritual entity ID: {0}, Proxy ritual entity tag: {1}", new object[]
			{
				tag,
				this.m_ritualSpellConfig.m_proxyRitualEntityTag
			});
			this.m_willShowRitualActorVisuals = false;
		}
		if (this.m_taskList.IsOrigin())
		{
			if (this.m_ritualSpellConfig.DoesTaskListContainRitualEntity(this.m_taskList, tag))
			{
				this.m_willShowRitualActorVisuals = false;
			}
			else if (this.m_ritualSpellConfig.DoesFutureTaskListContainsRitualEntity(GameState.Get().GetPowerProcessor().GetPowerQueue().GetList(), this.m_taskList, tag))
			{
				this.m_willShowRitualActorVisuals = false;
			}
		}
		return true;
	}

	// Token: 0x0600845E RID: 33886 RVA: 0x002AD5D4 File Offset: 0x002AB7D4
	protected override void OnAction(SpellStateType prevStateType)
	{
		if (this.m_ritualPortalSpellInstance == null && this.m_willShowRitualActorVisuals && this.InitPortalEffect())
		{
			this.m_isRitualPortalOpenForMinTime = false;
			base.StartCoroutine(this.DoPortalEffect());
		}
		if (this.m_playSuperSpellVisuals)
		{
			base.OnAction(prevStateType);
			return;
		}
		this.OnStateFinished();
	}

	// Token: 0x0600845F RID: 33887 RVA: 0x002AD629 File Offset: 0x002AB829
	public override bool CanPurge()
	{
		return (this.m_taskList == null || this.m_taskList.IsEndOfBlock()) && (!(this.m_ritualPortalSpellInstance != null) || !this.m_ritualPortalSpellInstance.IsActive());
	}

	// Token: 0x06008460 RID: 33888 RVA: 0x002AD660 File Offset: 0x002AB860
	public override void OnSpellFinished()
	{
		this.TryPortalClose();
		bool flag = this.m_ritualPortalSpellInstance != null || this.m_hasRitualTriggerSpell;
		bool flag2 = this.m_taskList != null && this.m_taskList.IsEndOfBlock();
		if (!flag || !flag2)
		{
			base.OnSpellFinished();
		}
	}

	// Token: 0x06008461 RID: 33889 RVA: 0x002AD6AC File Offset: 0x002AB8AC
	private bool InitPortalEffect()
	{
		Spell ritualActivateSpell = this.m_ritualSpellConfig.GetRitualActivateSpell(this.m_proxyRitualEntity);
		if (ritualActivateSpell == null)
		{
			return false;
		}
		this.m_proxyRitualActor = this.m_ritualSpellConfig.LoadRitualActor(this.m_proxyRitualEntity);
		if (this.m_proxyRitualActor == null)
		{
			return false;
		}
		this.m_ritualSpellConfig.UpdateAndPositionActor(this.m_proxyRitualActor);
		this.m_ritualPortalSpellInstance = UnityEngine.Object.Instantiate<Spell>(ritualActivateSpell);
		SpellUtils.SetCustomSpellParent(this.m_ritualPortalSpellInstance, this);
		this.m_ritualPortalSpellInstance.AddSpellEventCallback(new Spell.SpellEventCallback(this.OnPortalSpellEvent));
		this.m_ritualPortalSpellInstance.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnPortalSpellStateFinished));
		TransformUtil.AttachAndPreserveLocalTransform(this.m_ritualPortalSpellInstance.transform, this.m_proxyRitualActor.transform);
		this.m_ritualSpellConfig.UpdateRitualActorComponents(this.m_proxyRitualActor);
		return true;
	}

	// Token: 0x06008462 RID: 33890 RVA: 0x002AD781 File Offset: 0x002AB981
	private IEnumerator DoPortalEffect()
	{
		this.m_ritualPortalSpellInstance.Activate();
		yield return new WaitForSeconds(this.m_minTimeRitualActivateSpellPlays);
		this.m_isRitualPortalOpenForMinTime = true;
		this.TryPortalClose();
		yield break;
	}

	// Token: 0x06008463 RID: 33891 RVA: 0x002AD790 File Offset: 0x002AB990
	private void OnPortalSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName != this.m_ritualSpellConfig.m_portalSpellEventName)
		{
			Log.Spells.PrintError("RitualActivateSpell received unexpected Spell Event {0}. Expected {1}", new object[]
			{
				eventName,
				this.m_ritualSpellConfig.m_portalSpellEventName
			});
			return;
		}
		if (this.m_ritualSpellConfig.m_hideRitualActor)
		{
			this.m_proxyRitualActor.Show();
		}
	}

	// Token: 0x06008464 RID: 33892 RVA: 0x002AD7F0 File Offset: 0x002AB9F0
	public void OnPortalSpellFinished()
	{
		base.OnSpellFinished();
	}

	// Token: 0x06008465 RID: 33893 RVA: 0x002AD7F8 File Offset: 0x002AB9F8
	private void OnPortalSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			UnityEngine.Object.Destroy(this.m_proxyRitualActor.gameObject);
			this.OnPortalSpellFinished();
		}
	}

	// Token: 0x06008466 RID: 33894 RVA: 0x002AD818 File Offset: 0x002ABA18
	private void TryPortalClose()
	{
		if (this.m_taskList != null && !this.m_taskList.IsEndOfBlock())
		{
			for (PowerTaskList powerTaskList = this.m_taskList; powerTaskList != null; powerTaskList = powerTaskList.GetNext())
			{
				if (powerTaskList.HasTasks())
				{
					return;
				}
			}
		}
		if (this.m_ritualPortalSpellInstance == null || this.m_ritualPortalSpellInstance.GetActiveState() == SpellStateType.DEATH || this.m_ritualPortalSpellInstance.GetActiveState() == SpellStateType.NONE)
		{
			return;
		}
		if (!this.m_isRitualPortalOpenForMinTime)
		{
			return;
		}
		this.m_ritualPortalSpellInstance.ActivateState(SpellStateType.DEATH);
	}

	// Token: 0x04006F69 RID: 28521
	public RitualSpellConfig m_ritualSpellConfig;

	// Token: 0x04006F6A RID: 28522
	public float m_minTimeRitualActivateSpellPlays = 2f;

	// Token: 0x04006F6B RID: 28523
	private bool m_playSuperSpellVisuals;

	// Token: 0x04006F6C RID: 28524
	private bool m_isRitualPortalOpenForMinTime = true;

	// Token: 0x04006F6D RID: 28525
	private bool m_willShowRitualActorVisuals = true;

	// Token: 0x04006F6E RID: 28526
	private bool m_hasRitualTriggerSpell;

	// Token: 0x04006F6F RID: 28527
	private Entity m_proxyRitualEntity;

	// Token: 0x04006F70 RID: 28528
	private Actor m_proxyRitualActor;

	// Token: 0x04006F71 RID: 28529
	private Spell m_ritualPortalSpellInstance;
}
