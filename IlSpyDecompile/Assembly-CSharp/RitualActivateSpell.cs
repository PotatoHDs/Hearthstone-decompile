using System.Collections;
using UnityEngine;

public class RitualActivateSpell : SuperSpell
{
	public RitualSpellConfig m_ritualSpellConfig;

	public float m_minTimeRitualActivateSpellPlays = 2f;

	private bool m_playSuperSpellVisuals;

	private bool m_isRitualPortalOpenForMinTime = true;

	private bool m_willShowRitualActorVisuals = true;

	private bool m_hasRitualTriggerSpell;

	private Entity m_proxyRitualEntity;

	private Actor m_proxyRitualActor;

	private Spell m_ritualPortalSpellInstance;

	public void SetHasRitualTriggerSpell(bool hasSpell)
	{
		m_hasRitualTriggerSpell = hasSpell;
	}

	public override bool AddPowerTargets()
	{
		m_playSuperSpellVisuals = base.AddPowerTargets();
		Player controller = m_taskList.GetSourceEntity().GetController();
		if (!m_ritualSpellConfig.m_showRitualVisualsInPlay && m_ritualSpellConfig.IsRitualEntityInPlay(controller))
		{
			m_willShowRitualActorVisuals = false;
		}
		int num = controller.GetTag(m_ritualSpellConfig.m_proxyRitualEntityTag);
		m_proxyRitualEntity = GameState.Get().GetEntity(num);
		if (m_proxyRitualEntity == null)
		{
			Log.Spells.PrintError("RitualActivateSpell.AddPowerTargets(): Failed to get proxy ritual entity. Unable to display visuals. Proxy ritual entity ID: {0}, Proxy ritual entity tag: {1}", num, m_ritualSpellConfig.m_proxyRitualEntityTag);
			m_willShowRitualActorVisuals = false;
		}
		if (m_taskList.IsOrigin())
		{
			if (m_ritualSpellConfig.DoesTaskListContainRitualEntity(m_taskList, num))
			{
				m_willShowRitualActorVisuals = false;
			}
			else if (m_ritualSpellConfig.DoesFutureTaskListContainsRitualEntity(GameState.Get().GetPowerProcessor().GetPowerQueue()
				.GetList(), m_taskList, num))
			{
				m_willShowRitualActorVisuals = false;
			}
		}
		return true;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		if (m_ritualPortalSpellInstance == null && m_willShowRitualActorVisuals && InitPortalEffect())
		{
			m_isRitualPortalOpenForMinTime = false;
			StartCoroutine(DoPortalEffect());
		}
		if (m_playSuperSpellVisuals)
		{
			base.OnAction(prevStateType);
		}
		else
		{
			OnStateFinished();
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

	public override void OnSpellFinished()
	{
		TryPortalClose();
		bool num = m_ritualPortalSpellInstance != null || m_hasRitualTriggerSpell;
		bool flag = m_taskList != null && m_taskList.IsEndOfBlock();
		if (!(num && flag))
		{
			base.OnSpellFinished();
		}
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

	private IEnumerator DoPortalEffect()
	{
		m_ritualPortalSpellInstance.Activate();
		yield return new WaitForSeconds(m_minTimeRitualActivateSpellPlays);
		m_isRitualPortalOpenForMinTime = true;
		TryPortalClose();
	}

	private void OnPortalSpellEvent(string eventName, object eventData, object userData)
	{
		if (eventName != m_ritualSpellConfig.m_portalSpellEventName)
		{
			Log.Spells.PrintError("RitualActivateSpell received unexpected Spell Event {0}. Expected {1}", eventName, m_ritualSpellConfig.m_portalSpellEventName);
		}
		else if (m_ritualSpellConfig.m_hideRitualActor)
		{
			m_proxyRitualActor.Show();
		}
	}

	public void OnPortalSpellFinished()
	{
		base.OnSpellFinished();
	}

	private void OnPortalSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (spell.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(m_proxyRitualActor.gameObject);
			OnPortalSpellFinished();
		}
	}

	private void TryPortalClose()
	{
		if (m_taskList != null && !m_taskList.IsEndOfBlock())
		{
			for (PowerTaskList powerTaskList = m_taskList; powerTaskList != null; powerTaskList = powerTaskList.GetNext())
			{
				if (powerTaskList.HasTasks())
				{
					return;
				}
			}
		}
		if (!(m_ritualPortalSpellInstance == null) && m_ritualPortalSpellInstance.GetActiveState() != SpellStateType.DEATH && m_ritualPortalSpellInstance.GetActiveState() != 0 && m_isRitualPortalOpenForMinTime)
		{
			m_ritualPortalSpellInstance.ActivateState(SpellStateType.DEATH);
		}
	}
}
