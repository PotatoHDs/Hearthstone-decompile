using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x02000969 RID: 2409
public class RitualTriggerSpell : SuperSpell
{
	// Token: 0x06008474 RID: 33908 RVA: 0x002ADD34 File Offset: 0x002ABF34
	public override bool AddPowerTargets()
	{
		global::Player controller = this.m_taskList.GetSourceEntity(true).GetController();
		if (!this.m_ritualSpellConfig.m_showRitualVisualsInPlay && this.m_ritualSpellConfig.IsRitualEntityInPlay(controller))
		{
			return false;
		}
		int tag = controller.GetTag(this.m_ritualSpellConfig.m_proxyRitualEntityTag);
		this.m_proxyRitualEntity = GameState.Get().GetEntity(tag);
		if (this.m_proxyRitualEntity == null)
		{
			Log.Spells.PrintError("RitualTriggerSpell.AddPowerTargets(): Failed to get proxy ritual entity. Unable to display visuals. Proxy ritual entity ID: {0}, Proxy ritual entity tag: {1}", new object[]
			{
				tag,
				this.m_ritualSpellConfig.m_proxyRitualEntityTag
			});
			return false;
		}
		return this.m_ritualSpellConfig.DoesTaskListContainRitualEntity(this.m_taskList, tag) && base.AddPowerTargets();
	}

	// Token: 0x06008475 RID: 33909 RVA: 0x002ADDEA File Offset: 0x002ABFEA
	protected override void OnAction(SpellStateType prevStateType)
	{
		if (this.InitPortalEffect())
		{
			this.m_linkedSpellInstance = this.GetRitualActivateSpell();
			if (this.m_linkedSpellInstance != null)
			{
				this.m_linkedSpellInstance.SetHasRitualTriggerSpell(true);
			}
			base.StartCoroutine(this.DoPortalAndTransformEffect());
		}
	}

	// Token: 0x06008476 RID: 33910 RVA: 0x002ADE28 File Offset: 0x002AC028
	private RitualActivateSpell GetRitualActivateSpell()
	{
		for (PowerTaskList powerTaskList = this.m_taskList; powerTaskList != null; powerTaskList = powerTaskList.GetParent())
		{
			if (powerTaskList.GetBlockType() == HistoryBlock.Type.POWER)
			{
				CardEffect orCreateEffect = PowerSpellController.GetOrCreateEffect(powerTaskList.GetSourceEntity(true).GetCard(), powerTaskList);
				if (orCreateEffect != null)
				{
					RitualActivateSpell ritualActivateSpell = orCreateEffect.GetSpell(true) as RitualActivateSpell;
					if (ritualActivateSpell != null)
					{
						return ritualActivateSpell;
					}
				}
			}
		}
		return null;
	}

	// Token: 0x06008477 RID: 33911 RVA: 0x002ADE80 File Offset: 0x002AC080
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

	// Token: 0x06008478 RID: 33912 RVA: 0x002ADF55 File Offset: 0x002AC155
	private IEnumerator DoPortalAndTransformEffect()
	{
		this.m_ritualPortalSpellInstance.Activate();
		bool complete = false;
		PowerTaskList.CompleteCallback callback = delegate(PowerTaskList taskList, int startIndex, int count, object userData)
		{
			complete = true;
		};
		this.m_taskList.DoTasks(0, this.m_taskList.GetTaskList().Count, callback);
		yield return new WaitForSeconds(this.m_minTimeRitualTriggerSpellPlays);
		while (!complete)
		{
			yield return null;
		}
		Spell spell = this.ActivateTransformSpell();
		while (spell != null && !spell.IsFinished())
		{
			yield return null;
		}
		this.m_proxyRitualActor.SetEntity(this.m_proxyRitualEntity);
		this.m_proxyRitualActor.SetCardDefFromEntity(this.m_proxyRitualEntity);
		this.m_proxyRitualActor.UpdateAllComponents();
		this.OnSpellFinished();
		this.OnStateFinished();
		PowerTaskList targetTaskList = this.m_taskList;
		if (this.m_linkedSpellInstance != null)
		{
			targetTaskList = this.m_linkedSpellInstance.GetPowerTaskList();
		}
		while (!this.CanClosePortal(targetTaskList))
		{
			yield return null;
		}
		this.m_ritualPortalSpellInstance.ActivateState(SpellStateType.DEATH);
		yield break;
	}

	// Token: 0x06008479 RID: 33913 RVA: 0x002ADF64 File Offset: 0x002AC164
	public bool CanClosePortal(PowerTaskList targetTaskList)
	{
		List<PowerTaskList> list = GameState.Get().GetPowerProcessor().GetPowerQueue().GetList();
		if (list.Count == 0)
		{
			return true;
		}
		PowerTaskList powerTaskList = list[0];
		return powerTaskList == null || !powerTaskList.IsDescendantOfBlock(targetTaskList);
	}

	// Token: 0x0600847A RID: 33914 RVA: 0x002ADFAC File Offset: 0x002AC1AC
	private void OnPortalSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName != this.m_ritualSpellConfig.m_portalSpellEventName)
		{
			Log.Spells.PrintError("RitualTriggerSpell received unexpected Spell Event {0}. Expected {1}", new object[]
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

	// Token: 0x0600847B RID: 33915 RVA: 0x002AE00C File Offset: 0x002AC20C
	private void OnPortalSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			UnityEngine.Object.Destroy(this.m_proxyRitualActor.gameObject);
			if (this.m_linkedSpellInstance != null)
			{
				this.m_linkedSpellInstance.SetHasRitualTriggerSpell(false);
				this.m_linkedSpellInstance.OnPortalSpellFinished();
			}
		}
	}

	// Token: 0x0600847C RID: 33916 RVA: 0x002AE04C File Offset: 0x002AC24C
	private Spell ActivateTransformSpell()
	{
		Spell ritualTriggerSpell = this.m_ritualSpellConfig.GetRitualTriggerSpell(this.m_proxyRitualEntity);
		if (ritualTriggerSpell == null)
		{
			return null;
		}
		Spell spell = UnityEngine.Object.Instantiate<Spell>(ritualTriggerSpell);
		spell.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnTransformSpellStateFinished));
		this.UpdateAndPositionTransformSpell(spell);
		SpellUtils.SetCustomSpellParent(spell, this.m_proxyRitualActor);
		TransformUtil.AttachAndPreserveLocalTransform(spell.transform, this.m_proxyRitualActor.transform);
		spell.ActivateState(SpellStateType.ACTION);
		return spell;
	}

	// Token: 0x0600847D RID: 33917 RVA: 0x002AE0C0 File Offset: 0x002AC2C0
	private void OnTransformSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			UnityEngine.Object.Destroy(spell);
		}
	}

	// Token: 0x0600847E RID: 33918 RVA: 0x002AE0D0 File Offset: 0x002AC2D0
	public override bool CanPurge()
	{
		return (this.m_taskList == null || this.m_taskList.IsEndOfBlock()) && (!(this.m_ritualPortalSpellInstance != null) || !this.m_ritualPortalSpellInstance.IsActive());
	}

	// Token: 0x0600847F RID: 33919 RVA: 0x002AE108 File Offset: 0x002AC308
	private void UpdateAndPositionActor(Actor actor)
	{
		if (actor == null)
		{
			return;
		}
		if (this.m_ritualSpellConfig.m_hideRitualActor)
		{
			actor.Hide();
		}
		Transform parent = Board.Get().FindBone(this.GetRitualBoneName());
		actor.transform.parent = parent;
		actor.transform.localPosition = Vector3.zero;
	}

	// Token: 0x06008480 RID: 33920 RVA: 0x002AE160 File Offset: 0x002AC360
	private void UpdateAndPositionTransformSpell(Spell spell)
	{
		if (spell == null)
		{
			return;
		}
		Transform parent = Board.Get().FindBone(this.GetRitualBoneName());
		spell.transform.parent = parent;
		spell.transform.localPosition = Vector3.zero;
	}

	// Token: 0x06008481 RID: 33921 RVA: 0x002AE1A4 File Offset: 0x002AC3A4
	private string GetRitualBoneName()
	{
		if (this.m_proxyRitualEntity == null)
		{
			return string.Empty;
		}
		if (this.m_proxyRitualEntity.GetControllerSide() != global::Player.Side.FRIENDLY)
		{
			return this.m_ritualSpellConfig.m_opponentBoneName;
		}
		return this.m_ritualSpellConfig.m_friendlyBoneName;
	}

	// Token: 0x04006F7E RID: 28542
	public RitualSpellConfig m_ritualSpellConfig;

	// Token: 0x04006F7F RID: 28543
	public float m_minTimeRitualTriggerSpellPlays = 2f;

	// Token: 0x04006F80 RID: 28544
	private global::Entity m_proxyRitualEntity;

	// Token: 0x04006F81 RID: 28545
	private Actor m_proxyRitualActor;

	// Token: 0x04006F82 RID: 28546
	private Spell m_ritualPortalSpellInstance;

	// Token: 0x04006F83 RID: 28547
	private RitualActivateSpell m_linkedSpellInstance;
}
