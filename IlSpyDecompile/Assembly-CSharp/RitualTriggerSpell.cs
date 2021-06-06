using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

public class RitualTriggerSpell : SuperSpell
{
	public RitualSpellConfig m_ritualSpellConfig;

	public float m_minTimeRitualTriggerSpellPlays = 2f;

	private Entity m_proxyRitualEntity;

	private Actor m_proxyRitualActor;

	private Spell m_ritualPortalSpellInstance;

	private RitualActivateSpell m_linkedSpellInstance;

	public override bool AddPowerTargets()
	{
		Player controller = m_taskList.GetSourceEntity().GetController();
		if (!m_ritualSpellConfig.m_showRitualVisualsInPlay && m_ritualSpellConfig.IsRitualEntityInPlay(controller))
		{
			return false;
		}
		int num = controller.GetTag(m_ritualSpellConfig.m_proxyRitualEntityTag);
		m_proxyRitualEntity = GameState.Get().GetEntity(num);
		if (m_proxyRitualEntity == null)
		{
			Log.Spells.PrintError("RitualTriggerSpell.AddPowerTargets(): Failed to get proxy ritual entity. Unable to display visuals. Proxy ritual entity ID: {0}, Proxy ritual entity tag: {1}", num, m_ritualSpellConfig.m_proxyRitualEntityTag);
			return false;
		}
		if (!m_ritualSpellConfig.DoesTaskListContainRitualEntity(m_taskList, num))
		{
			return false;
		}
		return base.AddPowerTargets();
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		if (InitPortalEffect())
		{
			m_linkedSpellInstance = GetRitualActivateSpell();
			if (m_linkedSpellInstance != null)
			{
				m_linkedSpellInstance.SetHasRitualTriggerSpell(hasSpell: true);
			}
			StartCoroutine(DoPortalAndTransformEffect());
		}
	}

	private RitualActivateSpell GetRitualActivateSpell()
	{
		for (PowerTaskList powerTaskList = m_taskList; powerTaskList != null; powerTaskList = powerTaskList.GetParent())
		{
			if (powerTaskList.GetBlockType() == HistoryBlock.Type.POWER)
			{
				CardEffect orCreateEffect = PowerSpellController.GetOrCreateEffect(powerTaskList.GetSourceEntity().GetCard(), powerTaskList);
				if (orCreateEffect != null)
				{
					RitualActivateSpell ritualActivateSpell = orCreateEffect.GetSpell() as RitualActivateSpell;
					if (ritualActivateSpell != null)
					{
						return ritualActivateSpell;
					}
				}
			}
		}
		return null;
	}

	private bool InitPortalEffect()
	{
		Spell ritualActivateSpell = m_ritualSpellConfig.GetRitualActivateSpell(m_proxyRitualEntity);
		if (ritualActivateSpell == null)
		{
			return false;
		}
		m_proxyRitualActor = m_ritualSpellConfig.LoadRitualActor(m_proxyRitualEntity);
		if (m_proxyRitualActor == null)
		{
			return false;
		}
		m_ritualSpellConfig.UpdateAndPositionActor(m_proxyRitualActor);
		m_ritualPortalSpellInstance = Object.Instantiate(ritualActivateSpell);
		SpellUtils.SetCustomSpellParent(m_ritualPortalSpellInstance, this);
		m_ritualPortalSpellInstance.AddSpellEventCallback(OnPortalSpellEvent);
		m_ritualPortalSpellInstance.AddStateFinishedCallback(OnPortalSpellStateFinished);
		TransformUtil.AttachAndPreserveLocalTransform(m_ritualPortalSpellInstance.transform, m_proxyRitualActor.transform);
		m_ritualSpellConfig.UpdateRitualActorComponents(m_proxyRitualActor);
		return true;
	}

	private IEnumerator DoPortalAndTransformEffect()
	{
		m_ritualPortalSpellInstance.Activate();
		bool complete = false;
		PowerTaskList.CompleteCallback callback = delegate
		{
			complete = true;
		};
		m_taskList.DoTasks(0, m_taskList.GetTaskList().Count, callback);
		yield return new WaitForSeconds(m_minTimeRitualTriggerSpellPlays);
		while (!complete)
		{
			yield return null;
		}
		Spell spell = ActivateTransformSpell();
		while (spell != null && !spell.IsFinished())
		{
			yield return null;
		}
		m_proxyRitualActor.SetEntity(m_proxyRitualEntity);
		m_proxyRitualActor.SetCardDefFromEntity(m_proxyRitualEntity);
		m_proxyRitualActor.UpdateAllComponents();
		OnSpellFinished();
		OnStateFinished();
		PowerTaskList targetTaskList = m_taskList;
		if (m_linkedSpellInstance != null)
		{
			targetTaskList = m_linkedSpellInstance.GetPowerTaskList();
		}
		while (!CanClosePortal(targetTaskList))
		{
			yield return null;
		}
		m_ritualPortalSpellInstance.ActivateState(SpellStateType.DEATH);
	}

	public bool CanClosePortal(PowerTaskList targetTaskList)
	{
		List<PowerTaskList> list = GameState.Get().GetPowerProcessor().GetPowerQueue()
			.GetList();
		if (list.Count == 0)
		{
			return true;
		}
		PowerTaskList powerTaskList = list[0];
		if (powerTaskList == null)
		{
			return true;
		}
		if (powerTaskList.IsDescendantOfBlock(targetTaskList))
		{
			return false;
		}
		return true;
	}

	private void OnPortalSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName != m_ritualSpellConfig.m_portalSpellEventName)
		{
			Log.Spells.PrintError("RitualTriggerSpell received unexpected Spell Event {0}. Expected {1}", eventName, m_ritualSpellConfig.m_portalSpellEventName);
		}
		else if (m_ritualSpellConfig.m_hideRitualActor)
		{
			m_proxyRitualActor.Show();
		}
	}

	private void OnPortalSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(m_proxyRitualActor.gameObject);
			if (m_linkedSpellInstance != null)
			{
				m_linkedSpellInstance.SetHasRitualTriggerSpell(hasSpell: false);
				m_linkedSpellInstance.OnPortalSpellFinished();
			}
		}
	}

	private Spell ActivateTransformSpell()
	{
		Spell ritualTriggerSpell = m_ritualSpellConfig.GetRitualTriggerSpell(m_proxyRitualEntity);
		if (ritualTriggerSpell == null)
		{
			return null;
		}
		Spell spell = Object.Instantiate(ritualTriggerSpell);
		spell.AddStateFinishedCallback(OnTransformSpellStateFinished);
		UpdateAndPositionTransformSpell(spell);
		SpellUtils.SetCustomSpellParent(spell, m_proxyRitualActor);
		TransformUtil.AttachAndPreserveLocalTransform(spell.transform, m_proxyRitualActor.transform);
		spell.ActivateState(SpellStateType.ACTION);
		return spell;
	}

	private void OnTransformSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(spell);
		}
	}

	public override bool CanPurge()
	{
		if (m_taskList != null && !m_taskList.IsEndOfBlock())
		{
			return false;
		}
		if (m_ritualPortalSpellInstance != null && m_ritualPortalSpellInstance.IsActive())
		{
			return false;
		}
		return true;
	}

	private void UpdateAndPositionActor(Actor actor)
	{
		if (!(actor == null))
		{
			if (m_ritualSpellConfig.m_hideRitualActor)
			{
				actor.Hide();
			}
			Transform parent = Board.Get().FindBone(GetRitualBoneName());
			actor.transform.parent = parent;
			actor.transform.localPosition = Vector3.zero;
		}
	}

	private void UpdateAndPositionTransformSpell(Spell spell)
	{
		if (!(spell == null))
		{
			Transform parent = Board.Get().FindBone(GetRitualBoneName());
			spell.transform.parent = parent;
			spell.transform.localPosition = Vector3.zero;
		}
	}

	private string GetRitualBoneName()
	{
		if (m_proxyRitualEntity != null)
		{
			if (m_proxyRitualEntity.GetControllerSide() != Player.Side.FRIENDLY)
			{
				return m_ritualSpellConfig.m_opponentBoneName;
			}
			return m_ritualSpellConfig.m_friendlyBoneName;
		}
		return string.Empty;
	}
}
