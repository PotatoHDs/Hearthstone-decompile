using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSeeds : SuperSpell
{
	public class MinionData
	{
		public GameObject gameObject;

		public Vector3 orgLocPos;

		public Quaternion orgLocRot;

		public Vector3 rotationDrift;

		public Card card;
	}

	private enum SpellTargetType
	{
		None,
		Death,
		Create
	}

	public Spell m_CustomSpawnSpell;

	public Spell m_CustomDeathSpell;

	public float m_StartDeathSpellAdjustment = 0.01f;

	public AnimationCurve m_HeightCurve;

	public float m_RotationDriftAmount;

	public AnimationCurve m_RotationDriftCurve;

	public ParticleSystem m_ImpactParticles;

	public ParticleSystem m_DustParticles;

	private SpellTargetType m_TargetType;

	private float m_HeightCurveLength;

	private float m_AnimTime;

	private AudioSource m_Sound;

	protected override void Awake()
	{
		m_Sound = GetComponent<AudioSource>();
		base.Awake();
	}

	public override bool AddPowerTargets()
	{
		m_visualToTargetIndexMap.Clear();
		m_targetToMetaDataMap.Clear();
		m_targets.Clear();
		List<PowerTask> taskList = m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			PowerTask task = taskList[i];
			Card targetCardFromPowerTask = GetTargetCardFromPowerTask(i, task);
			if (!(targetCardFromPowerTask == null))
			{
				m_targets.Add(targetCardFromPowerTask.gameObject);
			}
		}
		if (m_targets.Count > 0)
		{
			return true;
		}
		return false;
	}

	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		int num = 0;
		Network.PowerHistory power = task.GetPower();
		if (power.Type == Network.PowerType.FULL_ENTITY)
		{
			m_TargetType = SpellTargetType.Create;
			num = (power as Network.HistFullEntity).Entity.ID;
		}
		else
		{
			Network.HistTagChange histTagChange = power as Network.HistTagChange;
			if (histTagChange == null || histTagChange.Tag != 360 || histTagChange.Value <= 0)
			{
				return null;
			}
			m_TargetType = SpellTargetType.Death;
			num = histTagChange.Entity;
		}
		Entity entity = GameState.Get().GetEntity(num);
		if (entity == null)
		{
			Debug.LogWarning($"{this}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {num} but there is no entity with that id");
			return null;
		}
		return entity.GetCard();
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		if (m_TargetType == SpellTargetType.Death)
		{
			DeathEffect();
			return;
		}
		if (m_TargetType == SpellTargetType.Create)
		{
			StartCoroutine(CreateEffect());
			return;
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	private IEnumerator CreateEffect()
	{
		foreach (GameObject target in GetTargets())
		{
			if (target == null)
			{
				continue;
			}
			Card component = target.GetComponent<Card>();
			if (!(component == null))
			{
				component.OverrideCustomSpawnSpell(Object.Instantiate(m_CustomSpawnSpell));
				ZonePlay zonePlay = (ZonePlay)component.GetZone();
				if (!(zonePlay == null))
				{
					zonePlay.SetTransitionTime(0.01f);
				}
			}
		}
		m_effectsPendingFinish--;
		FinishIfPossible();
		ShakeCamera();
		yield return new WaitForSeconds(1f);
		foreach (GameObject target2 in GetTargets())
		{
			Card component2 = target2.GetComponent<Card>();
			if (!(component2 == null))
			{
				ZonePlay zonePlay2 = (ZonePlay)component2.GetZone();
				if (!(zonePlay2 == null))
				{
					zonePlay2.ResetTransitionTime();
				}
			}
		}
	}

	private void DeathEffect()
	{
		if (m_HeightCurve.length == 0)
		{
			Debug.LogWarning("PoisonSeeds Spell height animation curve in not defined");
			return;
		}
		if (m_RotationDriftCurve.length == 0)
		{
			Debug.LogWarning("PoisonSeeds Spell rotation drift animation curve in not defined");
			return;
		}
		if (m_CustomDeathSpell != null)
		{
			foreach (GameObject target in GetTargets())
			{
				if (!(target == null))
				{
					target.GetComponent<Card>().OverrideCustomDeathSpell(Object.Instantiate(m_CustomDeathSpell));
				}
			}
		}
		m_HeightCurveLength = m_HeightCurve.keys[m_HeightCurve.length - 1].time;
		List<MinionData> list = new List<MinionData>();
		foreach (GameObject target2 in GetTargets())
		{
			MinionData minionData = new MinionData();
			minionData.card = target2.GetComponent<Card>();
			minionData.gameObject = target2;
			minionData.orgLocPos = target2.transform.localPosition;
			minionData.orgLocRot = target2.transform.localRotation;
			float x = Mathf.Lerp(0f - m_RotationDriftAmount, m_RotationDriftAmount, Random.value);
			float y = Mathf.Lerp(0f - m_RotationDriftAmount, m_RotationDriftAmount, Random.value) * 0.1f;
			float z = Mathf.Lerp(0f - m_RotationDriftAmount, m_RotationDriftAmount, Random.value);
			minionData.rotationDrift = new Vector3(x, y, z);
			list.Add(minionData);
		}
		StartCoroutine(AnimateDeathEffect(list));
	}

	private IEnumerator AnimateDeathEffect(List<MinionData> minions)
	{
		if (m_Sound != null)
		{
			SoundManager.Get().Play(m_Sound);
		}
		List<ParticleSystem> impactParticles = new List<ParticleSystem>();
		foreach (MinionData minion in minions)
		{
			GameObject gameObject = Object.Instantiate(m_ImpactParticles.gameObject);
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = minion.gameObject.transform.position;
			impactParticles.Add(gameObject.GetComponentInChildren<ParticleSystem>());
			GameObject obj = Object.Instantiate(m_DustParticles.gameObject);
			obj.transform.parent = base.transform;
			obj.transform.position = minion.gameObject.transform.position;
			obj.GetComponent<ParticleSystem>().Play();
		}
		m_AnimTime = 0f;
		bool finished = false;
		while (m_AnimTime < m_HeightCurveLength)
		{
			m_AnimTime += Time.deltaTime;
			float num = m_HeightCurve.Evaluate(m_AnimTime);
			float num2 = m_RotationDriftCurve.Evaluate(m_AnimTime);
			foreach (MinionData minion2 in minions)
			{
				minion2.gameObject.transform.localPosition = new Vector3(minion2.orgLocPos.x, minion2.orgLocPos.y + num, minion2.orgLocPos.z);
				minion2.gameObject.transform.localRotation = minion2.orgLocRot;
				minion2.gameObject.transform.Rotate(minion2.rotationDrift * num2, Space.Self);
			}
			if (m_AnimTime > m_HeightCurveLength - m_StartDeathSpellAdjustment && !finished)
			{
				foreach (MinionData minion3 in minions)
				{
					if (minion3 != null && minion3.card != null && minion3.card.GetActor() != null)
					{
						minion3.card.GetActor().DoCardDeathVisuals();
					}
				}
				m_effectsPendingFinish--;
				FinishIfPossible();
				finished = true;
			}
			yield return null;
		}
		foreach (ParticleSystem item in impactParticles)
		{
			item.Play();
		}
		ShakeCamera();
	}

	private void ShakeCamera()
	{
		CameraShakeMgr.Shake(Camera.main, new Vector3(0.15f, 0.15f, 0.15f), 0.9f);
	}
}
