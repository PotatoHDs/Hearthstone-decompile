using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007D0 RID: 2000
public class BouncingBlade : SuperSpell
{
	// Token: 0x06006E04 RID: 28164 RVA: 0x00237600 File Offset: 0x00235800
	protected override void Awake()
	{
		base.Awake();
		this.m_BladeRoot.SetActive(false);
		this.m_PreviousHitBone = this.m_HitBones[this.m_HitBones.Count - 1];
		this.m_OrgBladeScale = this.m_BladeRoot.transform.localScale;
		this.m_BladeRoot.transform.localScale = Vector3.zero;
	}

	// Token: 0x06006E05 RID: 28165 RVA: 0x00237668 File Offset: 0x00235868
	protected override void Start()
	{
		base.Start();
		this.SetupBounceLocations();
	}

	// Token: 0x06006E06 RID: 28166 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldReconnectIfStuck()
	{
		return false;
	}

	// Token: 0x06006E07 RID: 28167 RVA: 0x00237678 File Offset: 0x00235878
	protected override void OnAction(SpellStateType prevStateType)
	{
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		if (this.m_targets.Count == 0)
		{
			this.m_isDone = true;
			this.m_BladeRoot.SetActive(false);
			this.m_effectsPendingFinish--;
			base.FinishIfPossible();
			return;
		}
		if (!this.m_Running)
		{
			this.m_BladeRoot.SetActive(true);
			this.m_Blade.SetActive(false);
			this.m_Trail.SetActive(false);
			this.m_Running = true;
			base.StartCoroutine(this.BladeRunner());
		}
		this.m_BladeRoot.transform.localScale = this.m_OrgBladeScale;
		this.m_isDone = false;
		bool flag = base.IsHandlingLastTaskList();
		for (int i = 0; i < this.m_targets.Count; i++)
		{
			GameObject gameObject = this.m_targets[i];
			int metaDataIndexForTarget = base.GetMetaDataIndexForTarget(i);
			BouncingBlade.Target target = new BouncingBlade.Target();
			target.VisualTarget = gameObject;
			target.TargetPosition = gameObject.transform.position;
			target.MetaDataIdx = metaDataIndexForTarget;
			target.isMinion = true;
			if (i == this.m_targets.Count - 1)
			{
				target.LastTarget = true;
			}
			if (flag)
			{
				target.LastBlock = true;
			}
			this.m_TargetQueue.Add(target);
			if (!target.LastTarget)
			{
				BouncingBlade.Target target2 = new BouncingBlade.Target();
				target2.TargetPosition = this.AcquireRandomBoardTarget(out target2.Offscreen);
				target2.isMinion = false;
				target2.LastTarget = false;
				this.m_TargetQueue.Add(target2);
			}
		}
	}

	// Token: 0x06006E08 RID: 28168 RVA: 0x00237806 File Offset: 0x00235A06
	private IEnumerator BladeRunner()
	{
		while (!this.m_isDone)
		{
			while (this.m_TargetQueue.Count > 0)
			{
				if (!this.m_Blade.activeSelf)
				{
					this.m_Blade.SetActive(true);
					if (this.m_BladeSpinning != null)
					{
						this.m_BladeSpinning.gameObject.SetActive(true);
						SoundManager.Get().Play(this.m_BladeSpinning, null, null, null);
					}
					if (this.m_BladeSpinningContinuous != null)
					{
						this.m_BladeSpinningContinuous.gameObject.SetActive(true);
						SoundManager.Get().Play(this.m_BladeSpinningContinuous, null, null, null);
					}
					if (this.m_StartSound != null)
					{
						SoundManager.Get().Play(this.m_StartSound, null, null, null);
					}
				}
				if (!this.m_Trail.activeSelf)
				{
					this.m_Trail.SetActive(true);
				}
				BouncingBlade.Target target = this.m_TargetQueue[0];
				if (target.isMinion)
				{
					int metaDataIdx = target.MetaDataIdx;
					yield return base.StartCoroutine(base.CompleteTasksUntilMetaData(metaDataIdx));
					this.AnimateToNextTarget(target);
					while (this.m_Animating)
					{
						yield return null;
					}
					if (metaDataIdx > 0)
					{
						yield return base.StartCoroutine(base.CompleteTasksFromMetaData(metaDataIdx, 0f));
					}
					if (target.LastBlock && target.LastTarget)
					{
						this.m_EndSparkParticles.Play();
						this.m_EndBigSparkParticles.Play();
						this.m_Blade.SetActive(false);
						if (this.m_BladeSpinning != null)
						{
							SoundManager.Get().Stop(this.m_BladeSpinning);
						}
						if (this.m_BladeSpinningContinuous != null)
						{
							SoundManager.Get().Stop(this.m_BladeSpinningContinuous);
						}
						if (this.m_EndSound != null)
						{
							SoundManager.Get().Play(this.m_EndSound, null, null, null);
						}
						yield return new WaitForSeconds(0.8f);
						this.m_effectsPendingFinish--;
						base.FinishIfPossible();
						this.m_BladeRoot.SetActive(true);
						this.m_Running = false;
						this.m_TargetQueue.Clear();
						yield break;
					}
					if (!target.LastBlock && target.LastTarget)
					{
						this.m_effectsPendingFinish--;
						base.FinishIfPossible();
					}
				}
				else
				{
					this.AnimateToNextTarget(target);
					while (this.m_Animating)
					{
						yield return null;
					}
				}
				this.m_TargetQueue.RemoveAt(0);
				yield return null;
				target = null;
			}
			BouncingBlade.Target target2 = new BouncingBlade.Target();
			target2.TargetPosition = this.AcquireRandomBoardTarget(out target2.Offscreen);
			target2.isMinion = false;
			target2.LastTarget = false;
			this.AnimateToNextTarget(target2);
			while (this.m_Animating)
			{
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x06006E09 RID: 28169 RVA: 0x00237818 File Offset: 0x00235A18
	private void SetupBounceLocations()
	{
		Vector3 position = Board.Get().FindBone("CenterPointBone").transform.position;
		Vector3 localPosition = this.m_HitBonesRoot.transform.localPosition;
		this.m_HitBonesRoot.transform.position = position;
		foreach (BouncingBlade.HitBonesType hitBonesType in this.m_HitBones)
		{
			hitBonesType.SetPosition(hitBonesType.Bone.transform.position);
		}
		this.m_HitBonesRoot.transform.localPosition = localPosition;
	}

	// Token: 0x06006E0A RID: 28170 RVA: 0x002378C8 File Offset: 0x00235AC8
	private void AnimateToNextTarget(BouncingBlade.Target target)
	{
		this.m_Animating = true;
		iTween.MoveTo(this.m_BladeRoot, iTween.Hash(new object[]
		{
			"position",
			target.TargetPosition,
			"speed",
			this.m_BladeAnimationSpeed,
			"orienttopath",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"AnimationComplete",
			"oncompleteparams",
			target
		}));
	}

	// Token: 0x06006E0B RID: 28171 RVA: 0x00237974 File Offset: 0x00235B74
	private void RampBladeVolume()
	{
		iTween.StopByName(this.m_BladeSpinning.gameObject, "BladeSpinningSound");
		SoundManager.Get().SetVolume(this.m_BladeSpinning, this.m_BladeSpinningMinVol);
		Action<object> action = delegate(object amount)
		{
			SoundManager.Get().SetVolume(this.m_BladeSpinning, (float)amount);
		};
		iTween.ValueTo(this.m_BladeSpinning.gameObject, iTween.Hash(new object[]
		{
			"name",
			"BladeSpinningSound",
			"from",
			this.m_BladeSpinningMinVol,
			"to",
			this.m_BladeSpinningMaxVol,
			"time",
			this.m_BladeSpinningRampTime,
			"easetype",
			iTween.EaseType.linear,
			"onupdate",
			action,
			"onupdatetarget",
			this.m_BladeSpinning.gameObject
		}));
	}

	// Token: 0x06006E0C RID: 28172 RVA: 0x00237A60 File Offset: 0x00235C60
	private void AnimationComplete(BouncingBlade.Target target)
	{
		this.m_Animating = false;
		this.AnimateSparks();
		if (!target.LastBlock && !target.LastTarget)
		{
			this.RampBladeVolume();
		}
		AudioSource audioSource;
		if (target.isMinion)
		{
			audioSource = this.m_BladeHitMinion;
		}
		else if (target.Offscreen)
		{
			audioSource = this.m_BladeHitOffScreen;
		}
		else
		{
			audioSource = this.m_BladeHitBoardCorner;
		}
		if (audioSource != null)
		{
			audioSource.gameObject.transform.position = target.TargetPosition;
			SoundManager.Get().Play(audioSource, null, null, null);
		}
	}

	// Token: 0x06006E0D RID: 28173 RVA: 0x00237AE8 File Offset: 0x00235CE8
	private void AnimateSparks()
	{
		foreach (ParticleSystem particleSystem in this.m_SparkParticles)
		{
			particleSystem.Play();
		}
	}

	// Token: 0x06006E0E RID: 28174 RVA: 0x00237B38 File Offset: 0x00235D38
	private Vector3 AcquireRandomBoardTarget(out bool offscreen)
	{
		offscreen = false;
		if (UnityEngine.Random.Range(1, 100) < 5)
		{
			offscreen = true;
		}
		List<BouncingBlade.HitBonesType> list = new List<BouncingBlade.HitBonesType>();
		if (offscreen)
		{
			using (List<BouncingBlade.HitBonesType>.Enumerator enumerator = this.m_HitBones.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BouncingBlade.HitBonesType hitBonesType = enumerator.Current;
					if (hitBonesType.Direction != BouncingBlade.HIT_DIRECTIONS.E && hitBonesType.Direction != BouncingBlade.HIT_DIRECTIONS.NE && hitBonesType.Direction != BouncingBlade.HIT_DIRECTIONS.NW && hitBonesType.Direction != BouncingBlade.HIT_DIRECTIONS.SE && hitBonesType.Direction != BouncingBlade.HIT_DIRECTIONS.SW && hitBonesType.Direction != this.m_PreviousHitBone.Direction)
					{
						list.Add(hitBonesType);
					}
				}
				goto IL_107;
			}
		}
		foreach (BouncingBlade.HitBonesType hitBonesType2 in this.m_HitBones)
		{
			if (hitBonesType2.Direction != BouncingBlade.HIT_DIRECTIONS.E_OFFSCREEN && hitBonesType2.Direction != BouncingBlade.HIT_DIRECTIONS.N_OFFSCREEN && hitBonesType2.Direction != BouncingBlade.HIT_DIRECTIONS.S_OFFSCREEN && hitBonesType2.Direction != BouncingBlade.HIT_DIRECTIONS.W_OFFSCREEN && hitBonesType2.Direction != this.m_PreviousHitBone.Direction)
			{
				list.Add(hitBonesType2);
			}
		}
		IL_107:
		int index = UnityEngine.Random.Range(0, list.Count);
		this.m_PreviousHitBone = list[index];
		return list[index].GetPosition();
	}

	// Token: 0x04005821 RID: 22561
	private const float DAMAGE_SPLAT_DELAY = 0f;

	// Token: 0x04005822 RID: 22562
	private const float BLADE_BIRTH_TIME = 0.3f;

	// Token: 0x04005823 RID: 22563
	private const int OFFSCREEN_HIT_PERCENT = 5;

	// Token: 0x04005824 RID: 22564
	public GameObject m_BladeRoot;

	// Token: 0x04005825 RID: 22565
	public GameObject m_Blade;

	// Token: 0x04005826 RID: 22566
	public GameObject m_Trail;

	// Token: 0x04005827 RID: 22567
	public GameObject m_HitBonesRoot;

	// Token: 0x04005828 RID: 22568
	public List<ParticleSystem> m_SparkParticles;

	// Token: 0x04005829 RID: 22569
	public ParticleSystem m_EndSparkParticles;

	// Token: 0x0400582A RID: 22570
	public ParticleSystem m_EndBigSparkParticles;

	// Token: 0x0400582B RID: 22571
	public List<BouncingBlade.HitBonesType> m_HitBones;

	// Token: 0x0400582C RID: 22572
	public AudioSource m_BladeSpinning;

	// Token: 0x0400582D RID: 22573
	public AudioSource m_BladeSpinningContinuous;

	// Token: 0x0400582E RID: 22574
	public AudioSource m_BladeHitMinion;

	// Token: 0x0400582F RID: 22575
	public AudioSource m_BladeHitBoardCorner;

	// Token: 0x04005830 RID: 22576
	public AudioSource m_BladeHitOffScreen;

	// Token: 0x04005831 RID: 22577
	public AudioSource m_StartSound;

	// Token: 0x04005832 RID: 22578
	public AudioSource m_EndSound;

	// Token: 0x04005833 RID: 22579
	public float m_BladeAnimationSpeed = 50f;

	// Token: 0x04005834 RID: 22580
	public float m_BladeSpinningMinVol;

	// Token: 0x04005835 RID: 22581
	public float m_BladeSpinningMaxVol = 1f;

	// Token: 0x04005836 RID: 22582
	public float m_BladeSpinningRampTime = 0.3f;

	// Token: 0x04005837 RID: 22583
	private bool m_Running;

	// Token: 0x04005838 RID: 22584
	private List<BouncingBlade.Target> m_TargetQueue = new List<BouncingBlade.Target>();

	// Token: 0x04005839 RID: 22585
	private Vector3? m_NextPosition;

	// Token: 0x0400583A RID: 22586
	private bool m_Animating;

	// Token: 0x0400583B RID: 22587
	private bool m_isDone;

	// Token: 0x0400583C RID: 22588
	private BouncingBlade.HitBonesType m_PreviousHitBone;

	// Token: 0x0400583D RID: 22589
	private Vector3 m_OrgBladeScale;

	// Token: 0x02002366 RID: 9062
	public enum HIT_DIRECTIONS
	{
		// Token: 0x0400E694 RID: 59028
		NW,
		// Token: 0x0400E695 RID: 59029
		NE,
		// Token: 0x0400E696 RID: 59030
		E,
		// Token: 0x0400E697 RID: 59031
		SW,
		// Token: 0x0400E698 RID: 59032
		SE,
		// Token: 0x0400E699 RID: 59033
		N_OFFSCREEN,
		// Token: 0x0400E69A RID: 59034
		E_OFFSCREEN,
		// Token: 0x0400E69B RID: 59035
		W_OFFSCREEN,
		// Token: 0x0400E69C RID: 59036
		S_OFFSCREEN
	}

	// Token: 0x02002367 RID: 9063
	[Serializable]
	public class HitBonesType
	{
		// Token: 0x06012AF9 RID: 76537 RVA: 0x0051362F File Offset: 0x0051182F
		public void SetPosition(Vector3 pos)
		{
			this.m_Position = pos;
		}

		// Token: 0x06012AFA RID: 76538 RVA: 0x00513638 File Offset: 0x00511838
		public Vector3 GetPosition()
		{
			return this.m_Position;
		}

		// Token: 0x0400E69D RID: 59037
		public BouncingBlade.HIT_DIRECTIONS Direction;

		// Token: 0x0400E69E RID: 59038
		public GameObject Bone;

		// Token: 0x0400E69F RID: 59039
		private Vector3 m_Position;
	}

	// Token: 0x02002368 RID: 9064
	[Serializable]
	public class Target
	{
		// Token: 0x0400E6A0 RID: 59040
		public GameObject VisualTarget;

		// Token: 0x0400E6A1 RID: 59041
		public Vector3 TargetPosition;

		// Token: 0x0400E6A2 RID: 59042
		public bool isMinion;

		// Token: 0x0400E6A3 RID: 59043
		public int MetaDataIdx;

		// Token: 0x0400E6A4 RID: 59044
		public bool LastTarget;

		// Token: 0x0400E6A5 RID: 59045
		public bool LastBlock;

		// Token: 0x0400E6A6 RID: 59046
		public bool Offscreen;
	}
}
