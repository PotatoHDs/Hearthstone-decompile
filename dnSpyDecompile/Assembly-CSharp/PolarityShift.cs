using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000817 RID: 2071
public class PolarityShift : SuperSpell
{
	// Token: 0x06006FBB RID: 28603 RVA: 0x00240D86 File Offset: 0x0023EF86
	protected override void Awake()
	{
		this.m_Sound = base.GetComponent<AudioSource>();
		base.Awake();
	}

	// Token: 0x06006FBC RID: 28604 RVA: 0x00240D9C File Offset: 0x0023EF9C
	protected override void OnAction(SpellStateType prevStateType)
	{
		if (this.m_HeightCurve.length == 0)
		{
			Debug.LogWarning("PolarityShift Spell height animation curve in not defined");
			base.OnAction(prevStateType);
			return;
		}
		if (this.m_RotationDriftCurve.length == 0)
		{
			Debug.LogWarning("PolarityShift Spell rotation drift animation curve in not defined");
			base.OnAction(prevStateType);
			return;
		}
		this.m_effectsPendingFinish++;
		base.OnAction(prevStateType);
		this.m_HeightCurveLength = this.m_HeightCurve.keys[this.m_HeightCurve.length - 1].time;
		this.m_ParticleEffects.m_ParticleSystems.Clear();
		List<PolarityShift.MinionData> list = new List<PolarityShift.MinionData>();
		foreach (GameObject gameObject in base.GetTargets())
		{
			PolarityShift.MinionData minionData = new PolarityShift.MinionData();
			minionData.gameObject = gameObject;
			minionData.orgLocPos = gameObject.transform.localPosition;
			minionData.orgLocRot = gameObject.transform.localRotation;
			float x = Mathf.Lerp(-this.m_RotationDriftAmount, this.m_RotationDriftAmount, UnityEngine.Random.value);
			float y = Mathf.Lerp(-this.m_RotationDriftAmount, this.m_RotationDriftAmount, UnityEngine.Random.value) * 0.1f;
			float z = Mathf.Lerp(-this.m_RotationDriftAmount, this.m_RotationDriftAmount, UnityEngine.Random.value);
			minionData.rotationDrift = new Vector3(x, y, z);
			minionData.glowParticle = UnityEngine.Object.Instantiate<ParticleSystem>(this.m_GlowParticle);
			minionData.glowParticle.transform.position = gameObject.transform.position;
			minionData.glowParticle.transform.Translate(0f, this.m_ParticleHeightOffset, 0f, Space.World);
			minionData.lightningParticle = UnityEngine.Object.Instantiate<ParticleSystem>(this.m_LightningParticle);
			minionData.lightningParticle.transform.position = gameObject.transform.position;
			minionData.lightningParticle.transform.Translate(0f, this.m_ParticleHeightOffset, 0f, Space.World);
			minionData.impactParticle = UnityEngine.Object.Instantiate<ParticleSystem>(this.m_ImpactParticle);
			minionData.impactParticle.transform.position = gameObject.transform.position;
			minionData.impactParticle.transform.Translate(0f, this.m_ParticleHeightOffset, 0f, Space.World);
			this.m_ParticleEffects.m_ParticleSystems.Add(minionData.lightningParticle);
			if (this.m_Sound != null)
			{
				SoundManager.Get().Play(this.m_Sound, null, null, null);
			}
			list.Add(minionData);
		}
		base.StartCoroutine(this.DoSpellFinished());
		base.StartCoroutine(this.MinionAnimation(list));
	}

	// Token: 0x06006FBD RID: 28605 RVA: 0x00241060 File Offset: 0x0023F260
	private IEnumerator DoSpellFinished()
	{
		yield return new WaitForSeconds(this.m_SpellFinishTime);
		this.m_effectsPendingFinish--;
		base.FinishIfPossible();
		yield break;
	}

	// Token: 0x06006FBE RID: 28606 RVA: 0x0024106F File Offset: 0x0023F26F
	private IEnumerator MinionAnimation(List<PolarityShift.MinionData> minions)
	{
		foreach (PolarityShift.MinionData minionData in minions)
		{
			minionData.glowParticle.Play();
		}
		this.m_AnimTime = 0f;
		while (this.m_AnimTime < this.m_HeightCurveLength)
		{
			this.m_AnimTime += Time.deltaTime;
			float num = this.m_HeightCurve.Evaluate(this.m_AnimTime);
			float d = this.m_RotationDriftCurve.Evaluate(this.m_AnimTime);
			foreach (PolarityShift.MinionData minionData2 in minions)
			{
				minionData2.gameObject.transform.localPosition = new Vector3(minionData2.orgLocPos.x, minionData2.orgLocPos.y + num, minionData2.orgLocPos.z);
				minionData2.gameObject.transform.localRotation = minionData2.orgLocRot;
				minionData2.gameObject.transform.Rotate(minionData2.rotationDrift * d, Space.Self);
			}
			yield return null;
		}
		foreach (PolarityShift.MinionData minionData3 in minions)
		{
			minionData3.impactParticle.Play();
			minionData3.lightningParticle.Play();
			MinionShake.ShakeObject(minionData3.gameObject, ShakeMinionType.RandomDirection, minionData3.gameObject.transform.position, ShakeMinionIntensity.MediumShake, 0f, 0f, 0f);
		}
		if (minions.Count > 0)
		{
			this.ShakeCamera();
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
			fsfx = null;
		}
		if (minions.Count > 0)
		{
			yield return new WaitForSeconds(this.m_CleanupTime);
			this.m_ParticleEffects.m_ParticleSystems.Clear();
			foreach (PolarityShift.MinionData minionData4 in minions)
			{
				UnityEngine.Object.Destroy(minionData4.glowParticle.gameObject);
				UnityEngine.Object.Destroy(minionData4.lightningParticle.gameObject);
				UnityEngine.Object.Destroy(minionData4.impactParticle.gameObject);
			}
		}
		this.OnStateFinished();
		yield break;
	}

	// Token: 0x06006FBF RID: 28607 RVA: 0x00241085 File Offset: 0x0023F285
	private void ShakeCamera()
	{
		CameraShakeMgr.Shake(Camera.main, new Vector3(0.1f, 0.1f, 0.1f), 0.75f);
	}

	// Token: 0x04005999 RID: 22937
	public AnimationCurve m_HeightCurve;

	// Token: 0x0400599A RID: 22938
	public float m_RotationDriftAmount;

	// Token: 0x0400599B RID: 22939
	public AnimationCurve m_RotationDriftCurve;

	// Token: 0x0400599C RID: 22940
	public float m_ParticleHeightOffset = 0.1f;

	// Token: 0x0400599D RID: 22941
	public ParticleSystem m_GlowParticle;

	// Token: 0x0400599E RID: 22942
	public ParticleSystem m_LightningParticle;

	// Token: 0x0400599F RID: 22943
	public ParticleSystem m_ImpactParticle;

	// Token: 0x040059A0 RID: 22944
	public ParticleEffects m_ParticleEffects;

	// Token: 0x040059A1 RID: 22945
	public float m_CleanupTime = 2f;

	// Token: 0x040059A2 RID: 22946
	public float m_SpellFinishTime = 2f;

	// Token: 0x040059A3 RID: 22947
	private float m_HeightCurveLength;

	// Token: 0x040059A4 RID: 22948
	private float m_AnimTime;

	// Token: 0x040059A5 RID: 22949
	private AudioSource m_Sound;

	// Token: 0x020023D8 RID: 9176
	public class MinionData
	{
		// Token: 0x0400E841 RID: 59457
		public GameObject gameObject;

		// Token: 0x0400E842 RID: 59458
		public Vector3 orgLocPos;

		// Token: 0x0400E843 RID: 59459
		public Quaternion orgLocRot;

		// Token: 0x0400E844 RID: 59460
		public Vector3 rotationDrift;

		// Token: 0x0400E845 RID: 59461
		public ParticleSystem glowParticle;

		// Token: 0x0400E846 RID: 59462
		public ParticleSystem lightningParticle;

		// Token: 0x0400E847 RID: 59463
		public ParticleSystem impactParticle;
	}
}
