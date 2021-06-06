using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarityShift : SuperSpell
{
	public class MinionData
	{
		public GameObject gameObject;

		public Vector3 orgLocPos;

		public Quaternion orgLocRot;

		public Vector3 rotationDrift;

		public ParticleSystem glowParticle;

		public ParticleSystem lightningParticle;

		public ParticleSystem impactParticle;
	}

	public AnimationCurve m_HeightCurve;

	public float m_RotationDriftAmount;

	public AnimationCurve m_RotationDriftCurve;

	public float m_ParticleHeightOffset = 0.1f;

	public ParticleSystem m_GlowParticle;

	public ParticleSystem m_LightningParticle;

	public ParticleSystem m_ImpactParticle;

	public ParticleEffects m_ParticleEffects;

	public float m_CleanupTime = 2f;

	public float m_SpellFinishTime = 2f;

	private float m_HeightCurveLength;

	private float m_AnimTime;

	private AudioSource m_Sound;

	protected override void Awake()
	{
		m_Sound = GetComponent<AudioSource>();
		base.Awake();
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		if (m_HeightCurve.length == 0)
		{
			Debug.LogWarning("PolarityShift Spell height animation curve in not defined");
			base.OnAction(prevStateType);
			return;
		}
		if (m_RotationDriftCurve.length == 0)
		{
			Debug.LogWarning("PolarityShift Spell rotation drift animation curve in not defined");
			base.OnAction(prevStateType);
			return;
		}
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		m_HeightCurveLength = m_HeightCurve.keys[m_HeightCurve.length - 1].time;
		m_ParticleEffects.m_ParticleSystems.Clear();
		List<MinionData> list = new List<MinionData>();
		foreach (GameObject target in GetTargets())
		{
			MinionData minionData = new MinionData();
			minionData.gameObject = target;
			minionData.orgLocPos = target.transform.localPosition;
			minionData.orgLocRot = target.transform.localRotation;
			float x = Mathf.Lerp(0f - m_RotationDriftAmount, m_RotationDriftAmount, Random.value);
			float y = Mathf.Lerp(0f - m_RotationDriftAmount, m_RotationDriftAmount, Random.value) * 0.1f;
			float z = Mathf.Lerp(0f - m_RotationDriftAmount, m_RotationDriftAmount, Random.value);
			minionData.rotationDrift = new Vector3(x, y, z);
			minionData.glowParticle = Object.Instantiate(m_GlowParticle);
			minionData.glowParticle.transform.position = target.transform.position;
			minionData.glowParticle.transform.Translate(0f, m_ParticleHeightOffset, 0f, Space.World);
			minionData.lightningParticle = Object.Instantiate(m_LightningParticle);
			minionData.lightningParticle.transform.position = target.transform.position;
			minionData.lightningParticle.transform.Translate(0f, m_ParticleHeightOffset, 0f, Space.World);
			minionData.impactParticle = Object.Instantiate(m_ImpactParticle);
			minionData.impactParticle.transform.position = target.transform.position;
			minionData.impactParticle.transform.Translate(0f, m_ParticleHeightOffset, 0f, Space.World);
			m_ParticleEffects.m_ParticleSystems.Add(minionData.lightningParticle);
			if (m_Sound != null)
			{
				SoundManager.Get().Play(m_Sound);
			}
			list.Add(minionData);
		}
		StartCoroutine(DoSpellFinished());
		StartCoroutine(MinionAnimation(list));
	}

	private IEnumerator DoSpellFinished()
	{
		yield return new WaitForSeconds(m_SpellFinishTime);
		m_effectsPendingFinish--;
		FinishIfPossible();
	}

	private IEnumerator MinionAnimation(List<MinionData> minions)
	{
		foreach (MinionData minion in minions)
		{
			minion.glowParticle.Play();
		}
		m_AnimTime = 0f;
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
			yield return null;
		}
		foreach (MinionData minion3 in minions)
		{
			minion3.impactParticle.Play();
			minion3.lightningParticle.Play();
			MinionShake.ShakeObject(minion3.gameObject, ShakeMinionType.RandomDirection, minion3.gameObject.transform.position, ShakeMinionIntensity.MediumShake, 0f, 0f, 0f);
		}
		if (minions.Count > 0)
		{
			ShakeCamera();
			FullScreenEffects fsfx = FullScreenFXMgr.Get().ActiveCameraFullScreenEffects;
			if (fsfx != null && !fsfx.IsActive)
			{
				fsfx.BlendToColorEnable = true;
				fsfx.BlendToColorAmount = 1f;
				fsfx.BlendColor = Color.white;
				yield return null;
				fsfx.BlendToColorAmount = 0.67f;
				yield return null;
				fsfx.BlendToColorAmount = 0.33f;
				yield return null;
				fsfx.BlendToColorAmount = 0f;
				fsfx.BlendToColorEnable = false;
				fsfx.Disable();
			}
		}
		if (minions.Count > 0)
		{
			yield return new WaitForSeconds(m_CleanupTime);
			m_ParticleEffects.m_ParticleSystems.Clear();
			foreach (MinionData minion4 in minions)
			{
				Object.Destroy(minion4.glowParticle.gameObject);
				Object.Destroy(minion4.lightningParticle.gameObject);
				Object.Destroy(minion4.impactParticle.gameObject);
			}
		}
		OnStateFinished();
	}

	private void ShakeCamera()
	{
		CameraShakeMgr.Shake(Camera.main, new Vector3(0.1f, 0.1f, 0.1f), 0.75f);
	}
}
