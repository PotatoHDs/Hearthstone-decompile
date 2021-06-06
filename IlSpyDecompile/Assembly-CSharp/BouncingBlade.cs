using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBlade : SuperSpell
{
	public enum HIT_DIRECTIONS
	{
		NW,
		NE,
		E,
		SW,
		SE,
		N_OFFSCREEN,
		E_OFFSCREEN,
		W_OFFSCREEN,
		S_OFFSCREEN
	}

	[Serializable]
	public class HitBonesType
	{
		public HIT_DIRECTIONS Direction;

		public GameObject Bone;

		private Vector3 m_Position;

		public void SetPosition(Vector3 pos)
		{
			m_Position = pos;
		}

		public Vector3 GetPosition()
		{
			return m_Position;
		}
	}

	[Serializable]
	public class Target
	{
		public GameObject VisualTarget;

		public Vector3 TargetPosition;

		public bool isMinion;

		public int MetaDataIdx;

		public bool LastTarget;

		public bool LastBlock;

		public bool Offscreen;
	}

	private const float DAMAGE_SPLAT_DELAY = 0f;

	private const float BLADE_BIRTH_TIME = 0.3f;

	private const int OFFSCREEN_HIT_PERCENT = 5;

	public GameObject m_BladeRoot;

	public GameObject m_Blade;

	public GameObject m_Trail;

	public GameObject m_HitBonesRoot;

	public List<ParticleSystem> m_SparkParticles;

	public ParticleSystem m_EndSparkParticles;

	public ParticleSystem m_EndBigSparkParticles;

	public List<HitBonesType> m_HitBones;

	public AudioSource m_BladeSpinning;

	public AudioSource m_BladeSpinningContinuous;

	public AudioSource m_BladeHitMinion;

	public AudioSource m_BladeHitBoardCorner;

	public AudioSource m_BladeHitOffScreen;

	public AudioSource m_StartSound;

	public AudioSource m_EndSound;

	public float m_BladeAnimationSpeed = 50f;

	public float m_BladeSpinningMinVol;

	public float m_BladeSpinningMaxVol = 1f;

	public float m_BladeSpinningRampTime = 0.3f;

	private bool m_Running;

	private List<Target> m_TargetQueue = new List<Target>();

	private Vector3? m_NextPosition;

	private bool m_Animating;

	private bool m_isDone;

	private HitBonesType m_PreviousHitBone;

	private Vector3 m_OrgBladeScale;

	protected override void Awake()
	{
		base.Awake();
		m_BladeRoot.SetActive(value: false);
		m_PreviousHitBone = m_HitBones[m_HitBones.Count - 1];
		m_OrgBladeScale = m_BladeRoot.transform.localScale;
		m_BladeRoot.transform.localScale = Vector3.zero;
	}

	protected override void Start()
	{
		base.Start();
		SetupBounceLocations();
	}

	public override bool ShouldReconnectIfStuck()
	{
		return false;
	}

	protected override void OnAction(SpellStateType prevStateType)
	{
		m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		if (m_targets.Count == 0)
		{
			m_isDone = true;
			m_BladeRoot.SetActive(value: false);
			m_effectsPendingFinish--;
			FinishIfPossible();
			return;
		}
		if (!m_Running)
		{
			m_BladeRoot.SetActive(value: true);
			m_Blade.SetActive(value: false);
			m_Trail.SetActive(value: false);
			m_Running = true;
			StartCoroutine(BladeRunner());
		}
		m_BladeRoot.transform.localScale = m_OrgBladeScale;
		m_isDone = false;
		bool flag = IsHandlingLastTaskList();
		for (int i = 0; i < m_targets.Count; i++)
		{
			GameObject gameObject = m_targets[i];
			int metaDataIndexForTarget = GetMetaDataIndexForTarget(i);
			Target target = new Target();
			target.VisualTarget = gameObject;
			target.TargetPosition = gameObject.transform.position;
			target.MetaDataIdx = metaDataIndexForTarget;
			target.isMinion = true;
			if (i == m_targets.Count - 1)
			{
				target.LastTarget = true;
			}
			if (flag)
			{
				target.LastBlock = true;
			}
			m_TargetQueue.Add(target);
			if (!target.LastTarget)
			{
				Target target2 = new Target();
				target2.TargetPosition = AcquireRandomBoardTarget(out target2.Offscreen);
				target2.isMinion = false;
				target2.LastTarget = false;
				m_TargetQueue.Add(target2);
			}
		}
	}

	private IEnumerator BladeRunner()
	{
		while (!m_isDone)
		{
			while (m_TargetQueue.Count > 0)
			{
				if (!m_Blade.activeSelf)
				{
					m_Blade.SetActive(value: true);
					if (m_BladeSpinning != null)
					{
						m_BladeSpinning.gameObject.SetActive(value: true);
						SoundManager.Get().Play(m_BladeSpinning);
					}
					if (m_BladeSpinningContinuous != null)
					{
						m_BladeSpinningContinuous.gameObject.SetActive(value: true);
						SoundManager.Get().Play(m_BladeSpinningContinuous);
					}
					if (m_StartSound != null)
					{
						SoundManager.Get().Play(m_StartSound);
					}
				}
				if (!m_Trail.activeSelf)
				{
					m_Trail.SetActive(value: true);
				}
				Target target = m_TargetQueue[0];
				if (target.isMinion)
				{
					int metaDataIdx = target.MetaDataIdx;
					yield return StartCoroutine(CompleteTasksUntilMetaData(metaDataIdx));
					AnimateToNextTarget(target);
					while (m_Animating)
					{
						yield return null;
					}
					if (metaDataIdx > 0)
					{
						yield return StartCoroutine(CompleteTasksFromMetaData(metaDataIdx, 0f));
					}
					if (target.LastBlock && target.LastTarget)
					{
						m_EndSparkParticles.Play();
						m_EndBigSparkParticles.Play();
						m_Blade.SetActive(value: false);
						if (m_BladeSpinning != null)
						{
							SoundManager.Get().Stop(m_BladeSpinning);
						}
						if (m_BladeSpinningContinuous != null)
						{
							SoundManager.Get().Stop(m_BladeSpinningContinuous);
						}
						if (m_EndSound != null)
						{
							SoundManager.Get().Play(m_EndSound);
						}
						yield return new WaitForSeconds(0.8f);
						m_effectsPendingFinish--;
						FinishIfPossible();
						m_BladeRoot.SetActive(value: true);
						m_Running = false;
						m_TargetQueue.Clear();
						yield break;
					}
					if (!target.LastBlock && target.LastTarget)
					{
						m_effectsPendingFinish--;
						FinishIfPossible();
					}
				}
				else
				{
					AnimateToNextTarget(target);
					while (m_Animating)
					{
						yield return null;
					}
				}
				m_TargetQueue.RemoveAt(0);
				yield return null;
			}
			Target target2 = new Target();
			target2.TargetPosition = AcquireRandomBoardTarget(out target2.Offscreen);
			target2.isMinion = false;
			target2.LastTarget = false;
			AnimateToNextTarget(target2);
			while (m_Animating)
			{
				yield return null;
			}
		}
	}

	private void SetupBounceLocations()
	{
		Vector3 position = Board.Get().FindBone("CenterPointBone").transform.position;
		Vector3 localPosition = m_HitBonesRoot.transform.localPosition;
		m_HitBonesRoot.transform.position = position;
		foreach (HitBonesType hitBone in m_HitBones)
		{
			hitBone.SetPosition(hitBone.Bone.transform.position);
		}
		m_HitBonesRoot.transform.localPosition = localPosition;
	}

	private void AnimateToNextTarget(Target target)
	{
		m_Animating = true;
		iTween.MoveTo(m_BladeRoot, iTween.Hash("position", target.TargetPosition, "speed", m_BladeAnimationSpeed, "orienttopath", true, "easetype", iTween.EaseType.linear, "oncompletetarget", base.gameObject, "oncomplete", "AnimationComplete", "oncompleteparams", target));
	}

	private void RampBladeVolume()
	{
		iTween.StopByName(m_BladeSpinning.gameObject, "BladeSpinningSound");
		SoundManager.Get().SetVolume(m_BladeSpinning, m_BladeSpinningMinVol);
		Action<object> action = delegate(object amount)
		{
			SoundManager.Get().SetVolume(m_BladeSpinning, (float)amount);
		};
		iTween.ValueTo(m_BladeSpinning.gameObject, iTween.Hash("name", "BladeSpinningSound", "from", m_BladeSpinningMinVol, "to", m_BladeSpinningMaxVol, "time", m_BladeSpinningRampTime, "easetype", iTween.EaseType.linear, "onupdate", action, "onupdatetarget", m_BladeSpinning.gameObject));
	}

	private void AnimationComplete(Target target)
	{
		m_Animating = false;
		AnimateSparks();
		if (!target.LastBlock && !target.LastTarget)
		{
			RampBladeVolume();
		}
		AudioSource audioSource = (target.isMinion ? m_BladeHitMinion : ((!target.Offscreen) ? m_BladeHitBoardCorner : m_BladeHitOffScreen));
		if (audioSource != null)
		{
			audioSource.gameObject.transform.position = target.TargetPosition;
			SoundManager.Get().Play(audioSource);
		}
	}

	private void AnimateSparks()
	{
		foreach (ParticleSystem sparkParticle in m_SparkParticles)
		{
			sparkParticle.Play();
		}
	}

	private Vector3 AcquireRandomBoardTarget(out bool offscreen)
	{
		offscreen = false;
		if (UnityEngine.Random.Range(1, 100) < 5)
		{
			offscreen = true;
		}
		List<HitBonesType> list = new List<HitBonesType>();
		if (offscreen)
		{
			foreach (HitBonesType hitBone in m_HitBones)
			{
				if (hitBone.Direction != HIT_DIRECTIONS.E && hitBone.Direction != HIT_DIRECTIONS.NE && hitBone.Direction != 0 && hitBone.Direction != HIT_DIRECTIONS.SE && hitBone.Direction != HIT_DIRECTIONS.SW && hitBone.Direction != m_PreviousHitBone.Direction)
				{
					list.Add(hitBone);
				}
			}
		}
		else
		{
			foreach (HitBonesType hitBone2 in m_HitBones)
			{
				if (hitBone2.Direction != HIT_DIRECTIONS.E_OFFSCREEN && hitBone2.Direction != HIT_DIRECTIONS.N_OFFSCREEN && hitBone2.Direction != HIT_DIRECTIONS.S_OFFSCREEN && hitBone2.Direction != HIT_DIRECTIONS.W_OFFSCREEN && hitBone2.Direction != m_PreviousHitBone.Direction)
				{
					list.Add(hitBone2);
				}
			}
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		m_PreviousHitBone = list[index];
		return list[index].GetPosition();
	}
}
