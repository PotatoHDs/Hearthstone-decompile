using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistingNetherSpell : SuperSpell
{
	private enum VictimState
	{
		NONE,
		LIFTING,
		FLOATING,
		DRAINING,
		DEAD
	}

	private class Victim
	{
		public VictimState m_state;

		public Card m_card;
	}

	public float m_FinishTime;

	public TwistingNetherLiftInfo m_LiftInfo;

	public TwistingNetherFloatInfo m_FloatInfo;

	public TwistingNetherDrainInfo m_DrainInfo;

	public TwistingNetherSqueezeInfo m_SqueezeInfo;

	private static readonly Vector3 DEAD_SCALE = new Vector3(0.01f, 0.01f, 0.01f);

	private List<Victim> m_victims = new List<Victim>();

	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		if (power.Type != Network.PowerType.TAG_CHANGE)
		{
			return null;
		}
		Network.HistTagChange histTagChange = power as Network.HistTagChange;
		if (Gameplay.Get() != null)
		{
			if (histTagChange.Tag != 360)
			{
				return null;
			}
			if (histTagChange.Value != 1)
			{
				return null;
			}
		}
		Entity entity = GameState.Get().GetEntity(histTagChange.Entity);
		if (entity == null)
		{
			Debug.LogWarning($"{this}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {histTagChange.Entity} but there is no entity with that id");
			return null;
		}
		return entity.GetCard();
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		base.OnAction(prevStateType);
		if (!IsFinished())
		{
			Begin();
			if (CanFinish())
			{
				m_effectsPendingFinish--;
				FinishIfPossible();
			}
		}
	}

	protected override void CleanUp()
	{
		base.CleanUp();
		m_victims.Clear();
	}

	private void Begin()
	{
		m_effectsPendingFinish++;
		Action<object> action = delegate
		{
		};
		Hashtable args = iTween.Hash("from", 0f, "to", 1f, "time", m_FinishTime, "onupdate", action, "oncomplete", "OnFinishTimeFinished", "oncompletetarget", base.gameObject);
		iTween.ValueTo(base.gameObject, args);
		Setup();
		Lift();
	}

	private void Setup()
	{
		foreach (GameObject target in GetTargets())
		{
			Card component = target.GetComponent<Card>();
			component.SetDoNotSort(on: true);
			Victim victim = new Victim();
			victim.m_state = VictimState.NONE;
			victim.m_card = component;
			m_victims.Add(victim);
		}
	}

	private void Lift()
	{
		foreach (Victim victim in m_victims)
		{
			victim.m_state = VictimState.LIFTING;
			Vector3 vector = TransformUtil.RandomVector3(m_LiftInfo.m_OffsetMin, m_LiftInfo.m_OffsetMax);
			Vector3 vector2 = victim.m_card.transform.position + vector;
			float num = UnityEngine.Random.Range(m_LiftInfo.m_DelayMin, m_LiftInfo.m_DelayMax);
			float num2 = UnityEngine.Random.Range(m_LiftInfo.m_DurationMin, m_LiftInfo.m_DurationMax);
			Hashtable args = iTween.Hash("position", vector2, "delay", num, "time", num2, "easeType", m_LiftInfo.m_EaseType, "oncomplete", "OnLiftFinished", "oncompletetarget", base.gameObject, "oncompleteparams", victim);
			Vector3 vector3 = new Vector3(UnityEngine.Random.Range(m_LiftInfo.m_RotationMin, m_LiftInfo.m_RotationMax), UnityEngine.Random.Range(m_LiftInfo.m_RotationMin, m_LiftInfo.m_RotationMax), UnityEngine.Random.Range(m_LiftInfo.m_RotationMin, m_LiftInfo.m_RotationMax));
			float num3 = UnityEngine.Random.Range(m_LiftInfo.m_RotDelayMin, m_LiftInfo.m_RotDelayMax);
			float num4 = UnityEngine.Random.Range(m_LiftInfo.m_RotDurationMin, m_LiftInfo.m_RotDurationMax);
			Hashtable args2 = iTween.Hash("rotation", vector3, "delay", num3, "time", num4, "easeType", m_LiftInfo.m_EaseType);
			iTween.MoveTo(victim.m_card.gameObject, args);
			iTween.RotateTo(victim.m_card.gameObject, args2);
		}
	}

	private void OnLiftFinished(Victim victim)
	{
		Float(victim);
	}

	private void Float(Victim victim)
	{
		victim.m_state = VictimState.FLOATING;
		float num = UnityEngine.Random.Range(m_FloatInfo.m_DurationMin, m_FloatInfo.m_DurationMax);
		Action<object> action = delegate
		{
		};
		Hashtable args = iTween.Hash("from", 0f, "to", 1f, "time", num, "onupdate", action, "oncomplete", "OnFloatFinished", "oncompletetarget", base.gameObject, "oncompleteparams", victim);
		iTween.ValueTo(victim.m_card.gameObject, args);
	}

	private void OnFloatFinished(Victim victim)
	{
		Drain(victim);
	}

	private void Drain(Victim victim)
	{
		victim.m_state = VictimState.LIFTING;
		float num = UnityEngine.Random.Range(m_DrainInfo.m_DelayMin, m_DrainInfo.m_DelayMax);
		float num2 = UnityEngine.Random.Range(m_DrainInfo.m_DurationMin, m_DrainInfo.m_DurationMax);
		Hashtable args = iTween.Hash("position", base.transform.position, "delay", num, "time", num2, "easeType", m_DrainInfo.m_EaseType, "oncomplete", "OnDrainFinished", "oncompletetarget", base.gameObject, "oncompleteparams", victim);
		iTween.MoveTo(victim.m_card.gameObject, args);
		float num3 = UnityEngine.Random.Range(m_SqueezeInfo.m_DelayMin, m_SqueezeInfo.m_DelayMax);
		float num4 = UnityEngine.Random.Range(m_SqueezeInfo.m_DurationMin, m_SqueezeInfo.m_DurationMax);
		Hashtable args2 = iTween.Hash("scale", DEAD_SCALE, "delay", num3, "time", num4, "easeType", m_SqueezeInfo.m_EaseType);
		iTween.ScaleTo(victim.m_card.gameObject, args2);
	}

	private void OnDrainFinished(Victim victim)
	{
		CleanUpVictim(victim);
	}

	private void OnFinishTimeFinished()
	{
		foreach (Victim victim in m_victims)
		{
			CleanUpVictim(victim);
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	private void CleanUpVictim(Victim victim)
	{
		if (victim.m_state != VictimState.DEAD)
		{
			victim.m_state = VictimState.DEAD;
			victim.m_card.SetDoNotSort(on: false);
		}
	}

	private bool CanFinish()
	{
		foreach (Victim victim in m_victims)
		{
			if (victim.m_state != VictimState.DEAD)
			{
				return false;
			}
		}
		return true;
	}
}
