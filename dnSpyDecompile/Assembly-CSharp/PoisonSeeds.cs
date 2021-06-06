using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000816 RID: 2070
public class PoisonSeeds : SuperSpell
{
	// Token: 0x06006FB2 RID: 28594 RVA: 0x002409C1 File Offset: 0x0023EBC1
	protected override void Awake()
	{
		this.m_Sound = base.GetComponent<AudioSource>();
		base.Awake();
	}

	// Token: 0x06006FB3 RID: 28595 RVA: 0x002409D8 File Offset: 0x0023EBD8
	public override bool AddPowerTargets()
	{
		this.m_visualToTargetIndexMap.Clear();
		this.m_targetToMetaDataMap.Clear();
		this.m_targets.Clear();
		List<PowerTask> taskList = this.m_taskList.GetTaskList();
		for (int i = 0; i < taskList.Count; i++)
		{
			PowerTask task = taskList[i];
			Card targetCardFromPowerTask = this.GetTargetCardFromPowerTask(i, task);
			if (!(targetCardFromPowerTask == null))
			{
				this.m_targets.Add(targetCardFromPowerTask.gameObject);
			}
		}
		return this.m_targets.Count > 0;
	}

	// Token: 0x06006FB4 RID: 28596 RVA: 0x00240A60 File Offset: 0x0023EC60
	protected override Card GetTargetCardFromPowerTask(int index, PowerTask task)
	{
		Network.PowerHistory power = task.GetPower();
		int num;
		if (power.Type == Network.PowerType.FULL_ENTITY)
		{
			this.m_TargetType = PoisonSeeds.SpellTargetType.Create;
			num = (power as Network.HistFullEntity).Entity.ID;
		}
		else
		{
			Network.HistTagChange histTagChange = power as Network.HistTagChange;
			if (histTagChange == null || histTagChange.Tag != 360 || histTagChange.Value <= 0)
			{
				return null;
			}
			this.m_TargetType = PoisonSeeds.SpellTargetType.Death;
			num = histTagChange.Entity;
		}
		Entity entity = GameState.Get().GetEntity(num);
		if (entity == null)
		{
			Debug.LogWarning(string.Format("{0}.GetTargetCardFromPowerTask() - WARNING trying to target entity with id {1} but there is no entity with that id", this, num));
			return null;
		}
		return entity.GetCard();
	}

	// Token: 0x06006FB5 RID: 28597 RVA: 0x00240AF8 File Offset: 0x0023ECF8
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		if (this.m_TargetType == PoisonSeeds.SpellTargetType.Death)
		{
			this.DeathEffect();
			return;
		}
		if (this.m_TargetType == PoisonSeeds.SpellTargetType.Create)
		{
			base.StartCoroutine(this.CreateEffect());
			return;
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
	}

	// Token: 0x06006FB6 RID: 28598 RVA: 0x00240B55 File Offset: 0x0023ED55
	private IEnumerator CreateEffect()
	{
		foreach (GameObject gameObject in base.GetTargets())
		{
			if (!(gameObject == null))
			{
				Card component = gameObject.GetComponent<Card>();
				if (!(component == null))
				{
					component.OverrideCustomSpawnSpell(UnityEngine.Object.Instantiate<Spell>(this.m_CustomSpawnSpell));
					ZonePlay zonePlay = (ZonePlay)component.GetZone();
					if (!(zonePlay == null))
					{
						zonePlay.SetTransitionTime(0.01f);
					}
				}
			}
		}
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
		this.ShakeCamera();
		yield return new WaitForSeconds(1f);
		using (List<GameObject>.Enumerator enumerator = base.GetTargets().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GameObject gameObject2 = enumerator.Current;
				Card component2 = gameObject2.GetComponent<Card>();
				if (!(component2 == null))
				{
					ZonePlay zonePlay2 = (ZonePlay)component2.GetZone();
					if (!(zonePlay2 == null))
					{
						zonePlay2.ResetTransitionTime();
					}
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06006FB7 RID: 28599 RVA: 0x00240B64 File Offset: 0x0023ED64
	private void DeathEffect()
	{
		if (this.m_HeightCurve.length == 0)
		{
			Debug.LogWarning("PoisonSeeds Spell height animation curve in not defined");
			return;
		}
		if (this.m_RotationDriftCurve.length == 0)
		{
			Debug.LogWarning("PoisonSeeds Spell rotation drift animation curve in not defined");
			return;
		}
		if (this.m_CustomDeathSpell != null)
		{
			foreach (GameObject gameObject in base.GetTargets())
			{
				if (!(gameObject == null))
				{
					gameObject.GetComponent<Card>().OverrideCustomDeathSpell(UnityEngine.Object.Instantiate<Spell>(this.m_CustomDeathSpell));
				}
			}
		}
		this.m_HeightCurveLength = this.m_HeightCurve.keys[this.m_HeightCurve.length - 1].time;
		List<PoisonSeeds.MinionData> list = new List<PoisonSeeds.MinionData>();
		foreach (GameObject gameObject2 in base.GetTargets())
		{
			PoisonSeeds.MinionData minionData = new PoisonSeeds.MinionData();
			minionData.card = gameObject2.GetComponent<Card>();
			minionData.gameObject = gameObject2;
			minionData.orgLocPos = gameObject2.transform.localPosition;
			minionData.orgLocRot = gameObject2.transform.localRotation;
			float x = Mathf.Lerp(-this.m_RotationDriftAmount, this.m_RotationDriftAmount, UnityEngine.Random.value);
			float y = Mathf.Lerp(-this.m_RotationDriftAmount, this.m_RotationDriftAmount, UnityEngine.Random.value) * 0.1f;
			float z = Mathf.Lerp(-this.m_RotationDriftAmount, this.m_RotationDriftAmount, UnityEngine.Random.value);
			minionData.rotationDrift = new Vector3(x, y, z);
			list.Add(minionData);
		}
		base.StartCoroutine(this.AnimateDeathEffect(list));
	}

	// Token: 0x06006FB8 RID: 28600 RVA: 0x00240D38 File Offset: 0x0023EF38
	private IEnumerator AnimateDeathEffect(List<PoisonSeeds.MinionData> minions)
	{
		if (this.m_Sound != null)
		{
			SoundManager.Get().Play(this.m_Sound, null, null, null);
		}
		List<ParticleSystem> impactParticles = new List<ParticleSystem>();
		foreach (PoisonSeeds.MinionData minionData in minions)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_ImpactParticles.gameObject);
			gameObject.transform.parent = base.transform;
			gameObject.transform.position = minionData.gameObject.transform.position;
			impactParticles.Add(gameObject.GetComponentInChildren<ParticleSystem>());
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.m_DustParticles.gameObject);
			gameObject2.transform.parent = base.transform;
			gameObject2.transform.position = minionData.gameObject.transform.position;
			gameObject2.GetComponent<ParticleSystem>().Play();
		}
		this.m_AnimTime = 0f;
		bool finished = false;
		while (this.m_AnimTime < this.m_HeightCurveLength)
		{
			this.m_AnimTime += Time.deltaTime;
			float num = this.m_HeightCurve.Evaluate(this.m_AnimTime);
			float d = this.m_RotationDriftCurve.Evaluate(this.m_AnimTime);
			foreach (PoisonSeeds.MinionData minionData2 in minions)
			{
				minionData2.gameObject.transform.localPosition = new Vector3(minionData2.orgLocPos.x, minionData2.orgLocPos.y + num, minionData2.orgLocPos.z);
				minionData2.gameObject.transform.localRotation = minionData2.orgLocRot;
				minionData2.gameObject.transform.Rotate(minionData2.rotationDrift * d, Space.Self);
			}
			if (this.m_AnimTime > this.m_HeightCurveLength - this.m_StartDeathSpellAdjustment && !finished)
			{
				foreach (PoisonSeeds.MinionData minionData3 in minions)
				{
					if (minionData3 != null && minionData3.card != null && minionData3.card.GetActor() != null)
					{
						minionData3.card.GetActor().DoCardDeathVisuals();
					}
				}
				this.m_effectsPendingFinish--;
				base.FinishIfPossible();
				finished = true;
			}
			yield return null;
		}
		foreach (ParticleSystem particleSystem in impactParticles)
		{
			particleSystem.Play();
		}
		this.ShakeCamera();
		yield break;
	}

	// Token: 0x06006FB9 RID: 28601 RVA: 0x00240D4E File Offset: 0x0023EF4E
	private void ShakeCamera()
	{
		CameraShakeMgr.Shake(Camera.main, new Vector3(0.15f, 0.15f, 0.15f), 0.9f);
	}

	// Token: 0x0400598D RID: 22925
	public Spell m_CustomSpawnSpell;

	// Token: 0x0400598E RID: 22926
	public Spell m_CustomDeathSpell;

	// Token: 0x0400598F RID: 22927
	public float m_StartDeathSpellAdjustment = 0.01f;

	// Token: 0x04005990 RID: 22928
	public AnimationCurve m_HeightCurve;

	// Token: 0x04005991 RID: 22929
	public float m_RotationDriftAmount;

	// Token: 0x04005992 RID: 22930
	public AnimationCurve m_RotationDriftCurve;

	// Token: 0x04005993 RID: 22931
	public ParticleSystem m_ImpactParticles;

	// Token: 0x04005994 RID: 22932
	public ParticleSystem m_DustParticles;

	// Token: 0x04005995 RID: 22933
	private PoisonSeeds.SpellTargetType m_TargetType;

	// Token: 0x04005996 RID: 22934
	private float m_HeightCurveLength;

	// Token: 0x04005997 RID: 22935
	private float m_AnimTime;

	// Token: 0x04005998 RID: 22936
	private AudioSource m_Sound;

	// Token: 0x020023D4 RID: 9172
	public class MinionData
	{
		// Token: 0x0400E82F RID: 59439
		public GameObject gameObject;

		// Token: 0x0400E830 RID: 59440
		public Vector3 orgLocPos;

		// Token: 0x0400E831 RID: 59441
		public Quaternion orgLocRot;

		// Token: 0x0400E832 RID: 59442
		public Vector3 rotationDrift;

		// Token: 0x0400E833 RID: 59443
		public Card card;
	}

	// Token: 0x020023D5 RID: 9173
	private enum SpellTargetType
	{
		// Token: 0x0400E835 RID: 59445
		None,
		// Token: 0x0400E836 RID: 59446
		Death,
		// Token: 0x0400E837 RID: 59447
		Create
	}
}
