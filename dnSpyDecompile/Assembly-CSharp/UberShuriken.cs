using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AA6 RID: 2726
[ExecuteAlways]
public class UberShuriken : MonoBehaviour
{
	// Token: 0x0600913B RID: 37179 RVA: 0x002F27EE File Offset: 0x002F09EE
	private void Awake()
	{
		if (this.m_UberCurve == null)
		{
			this.m_UberCurve = base.GetComponent<UberCurve>();
		}
		this.UpdateParticleSystemList();
	}

	// Token: 0x0600913C RID: 37180 RVA: 0x002F2810 File Offset: 0x002F0A10
	private void Update()
	{
		this.m_time = Time.time;
		this.UpdateParticles();
	}

	// Token: 0x0600913D RID: 37181 RVA: 0x002F2824 File Offset: 0x002F0A24
	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = Color.blue;
		if (this.m_CurlNoise && this.m_CurlNoiseGizmoSize > 0f)
		{
			int num = 10;
			float num2 = Mathf.Max(Mathf.Abs(this.m_CurlNoiseScale * 0.25f), 1f) * this.m_CurlNoiseGizmoSize;
			float num3 = 1f / ((float)num * 1.2f);
			float num4 = 1f;
			float num5 = 0f;
			for (int i = 0; i < num; i++)
			{
				Gizmos.color = new Color(0f, 0f, 1f - num4, 1f);
				num4 -= num3;
				float num6 = (float)i * 0.75f;
				Vector4[] array = this.GizmoCirclePoints(20 * Mathf.Max(Mathf.FloorToInt(Mathf.Abs(this.m_CurlNoiseScale)), 10), num6 * num2);
				Vector4 v = array[array.Length - 1];
				for (int j = 0; j < array.Length; j++)
				{
					Gizmos.color = new Color(array[j].w * 0.5f, array[j].w, 1f, 1f);
					Gizmos.DrawLine(v, array[j]);
					v = array[j];
				}
				Vector4[] array2 = this.GizmoCircleLines(10, num5 * num2, num6 * num2);
				for (int k = 0; k < array2.Length; k += 2)
				{
					Gizmos.color = new Color(array2[k].w * 0.5f, array2[k].w, 1f, 1f);
					Gizmos.DrawLine(array2[k], array2[k + 1]);
				}
				num5 = num6;
			}
		}
		Gizmos.matrix = Matrix4x4.identity;
	}

	// Token: 0x0600913E RID: 37182 RVA: 0x002F2A24 File Offset: 0x002F0C24
	private Vector4[] GizmoCirclePoints(int numOfPoints, float radius)
	{
		Vector4[] array = new Vector4[numOfPoints];
		float num = 0f;
		float num2 = 6.2831855f / (float)numOfPoints;
		for (int i = 0; i < numOfPoints; i++)
		{
			num += num2;
			array[i] = this.GizmoCurlNoisePoint(new Vector3(Mathf.Cos(num) * radius, Mathf.Sin(num) * radius, 0f));
		}
		return array;
	}

	// Token: 0x0600913F RID: 37183 RVA: 0x002F2A80 File Offset: 0x002F0C80
	private Vector4[] GizmoCircleLines(int numOfPoints, float previousRadius, float radius)
	{
		int num = numOfPoints * 2;
		Vector4[] array = new Vector4[num];
		float num2 = 0f;
		float num3 = 6.283f / (float)numOfPoints;
		for (int i = 0; i < num; i += 2)
		{
			num2 += num3;
			array[i] = this.GizmoCurlNoisePoint(new Vector3(Mathf.Cos(num2) * previousRadius, Mathf.Sin(num2) * previousRadius, 0f));
			array[i + 1] = this.GizmoCurlNoisePoint(new Vector3(Mathf.Cos(num2) * radius, Mathf.Sin(num2) * radius, 0f));
		}
		return array;
	}

	// Token: 0x06009140 RID: 37184 RVA: 0x002F2B10 File Offset: 0x002F0D10
	private Vector4 GizmoCurlNoisePoint(Vector3 point)
	{
		float time = this.m_time;
		float num = this.m_CurlNoiseAnimation.x * time;
		float num2 = this.m_CurlNoiseAnimation.y * time;
		float num3 = this.m_CurlNoiseAnimation.z * time;
		Vector3 vector = point * this.m_CurlNoiseScale * 0.1f;
		float num4 = UberMath.SimplexNoise(5f + vector.x + num, vector.y + num2, vector.z + num3) * this.m_CurlNoisePower;
		float num5 = UberMath.SimplexNoise(6f + vector.y + num, vector.z + num2, vector.x + num3) * this.m_CurlNoisePower;
		float num6 = UberMath.SimplexNoise(7f + vector.z + num, vector.x + num2, vector.y + num3) * this.m_CurlNoisePower;
		Vector3 vector2 = new Vector3(point.x + num4, point.y + num5, point.z + num6);
		float w = Mathf.Max(num4, Mathf.Max(num5, num6));
		return new Vector4(vector2.x, vector2.y, vector2.z, w);
	}

	// Token: 0x06009141 RID: 37185 RVA: 0x002F2C4C File Offset: 0x002F0E4C
	private void UpdateParticles()
	{
		this.m_followCurveIntervalIndex = ((this.m_followCurveIntervalIndex + 1 > 3) ? (this.m_followCurveIntervalIndex = 0) : (this.m_followCurveIntervalIndex + 1));
		this.m_curlNoiseIntervalIndex = ((this.m_curlNoiseIntervalIndex + 1 > 3) ? 0 : (this.m_curlNoiseIntervalIndex + 1));
		foreach (ParticleSystem particleSystem in this.m_particleSystems)
		{
			if (!(particleSystem == null))
			{
				int particleCount = particleSystem.particleCount;
				if (particleCount == 0)
				{
					break;
				}
				ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleCount];
				particleSystem.GetParticles(particles);
				if (this.m_FollowCurveDirection || this.m_FollowCurvePosition)
				{
					this.FollowCurveOverLife(particleSystem, particles, particleCount);
				}
				if (this.m_CurlNoise)
				{
					this.ParticleCurlNoise(particleSystem, particles, particleCount);
				}
				if (this.m_Twinkle)
				{
					this.ParticleTwinkle(particleSystem, particles, particleCount);
				}
				particleSystem.SetParticles(particles, particleCount);
			}
		}
	}

	// Token: 0x06009142 RID: 37186 RVA: 0x002F2D48 File Offset: 0x002F0F48
	private void UpdateParticleSystemList()
	{
		this.m_particleSystems.Clear();
		if (this.m_IncludeChildren)
		{
			ParticleSystem[] componentsInChildren = base.GetComponentsInChildren<ParticleSystem>();
			if (base.GetComponent<ParticleSystem>() == null || componentsInChildren.Length == 0)
			{
				Debug.LogError("Failed to find a ParticleSystem");
			}
			foreach (ParticleSystem item in componentsInChildren)
			{
				this.m_particleSystems.Add(item);
			}
			return;
		}
		ParticleSystem component = base.GetComponent<ParticleSystem>();
		if (component == null)
		{
			Debug.LogError("Failed to find a ParticleSystem");
		}
		this.m_particleSystems.Add(component);
	}

	// Token: 0x06009143 RID: 37187 RVA: 0x002F2DD8 File Offset: 0x002F0FD8
	private void FollowCurveOverLife(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		if (this.m_UberCurve == null)
		{
			this.CreateCurve();
		}
		for (int i = this.m_followCurveIntervalIndex; i < particleCount; i += 3)
		{
			float position = 1f - particles[i].remainingLifetime / particles[i].startLifetime;
			if (this.m_FollowCurvePosition)
			{
				Vector3 a = Vector3.zero;
				if (particleSystem.main.simulationSpace == ParticleSystemSimulationSpace.World)
				{
					a = this.m_UberCurve.CatmullRomEvaluateWorldPosition(position);
				}
				else
				{
					a = this.m_UberCurve.CatmullRomEvaluateLocalPosition(position);
				}
				Vector3 vector = a - particles[i].position;
				vector = Vector3.Lerp(particles[i].velocity, vector, this.m_FollowCurvePositionAttraction);
				particles[i].velocity = vector * this.m_FollowCurvePositionIntensity;
			}
			if (this.m_FollowCurveDirection)
			{
				Vector3 velocity = this.m_UberCurve.CatmullRomEvaluateDirection(position).normalized * particles[i].velocity.magnitude;
				particles[i].velocity = velocity;
			}
		}
	}

	// Token: 0x06009144 RID: 37188 RVA: 0x002F2EF7 File Offset: 0x002F10F7
	private void CreateCurve()
	{
		if (this.m_UberCurve != null)
		{
			return;
		}
		this.m_UberCurve = base.GetComponent<UberCurve>();
		if (this.m_UberCurve != null)
		{
			return;
		}
		this.m_UberCurve = base.gameObject.AddComponent<UberCurve>();
	}

	// Token: 0x06009145 RID: 37189 RVA: 0x002F2F34 File Offset: 0x002F1134
	private void ParticleCurlNoise(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		float time = this.m_time;
		float num = this.m_CurlNoiseAnimation.x * time;
		float num2 = this.m_CurlNoiseAnimation.y * time;
		float num3 = this.m_CurlNoiseAnimation.z * time;
		for (int i = this.m_curlNoiseIntervalIndex; i < particleCount; i += 3)
		{
			float time2 = 1f - particles[i].remainingLifetime / particles[i].startLifetime;
			float num4 = this.m_CurlNoiseOverLifetime.Evaluate(time2) * this.m_CurlNoisePower;
			Vector3 velocity = particles[i].velocity;
			Vector3 vector = particles[i].position * this.m_CurlNoiseScale * 0.1f;
			velocity.x += UberMath.SimplexNoise(5f + vector.x + num, vector.y + num2, vector.z + num3) * num4;
			velocity.y += UberMath.SimplexNoise(6f + vector.y + num, vector.z + num2, vector.x + num3) * num4;
			velocity.z += UberMath.SimplexNoise(7f + vector.z + num, vector.x + num2, vector.y + num3) * num4;
			velocity = velocity.normalized * particles[i].velocity.magnitude;
			particles[i].velocity = velocity;
		}
	}

	// Token: 0x06009146 RID: 37190 RVA: 0x002F30C8 File Offset: 0x002F12C8
	private void ParticleTwinkle(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		for (int i = 0; i < particleCount; i++)
		{
			float num = particles[i].remainingLifetime / particles[i].startLifetime;
			Vector3 position = particles[i].position;
			Color c = particles[i].startColor;
			c.a = Mathf.Clamp01(UberMath.SimplexNoise((position.x + position.y + position.z - num - (float)i * 3.33f) * this.m_TwinkleRate, 0.5f) + this.m_TwinkleBias + num * this.m_TwinkleOverLifetime.Evaluate(num));
			particles[i].startColor = c;
		}
	}

	// Token: 0x04007A15 RID: 31253
	private const int VORTEX_NOISE_INVERVAL = 3;

	// Token: 0x04007A16 RID: 31254
	private const int FOLLOW_CURVE_INVERVAL = 3;

	// Token: 0x04007A17 RID: 31255
	private const int CURL_NOISE_INVERVAL = 3;

	// Token: 0x04007A18 RID: 31256
	public bool m_IncludeChildren;

	// Token: 0x04007A19 RID: 31257
	public UberCurve m_UberCurve;

	// Token: 0x04007A1A RID: 31258
	public bool m_FollowCurveDirection;

	// Token: 0x04007A1B RID: 31259
	public bool m_FollowCurvePosition;

	// Token: 0x04007A1C RID: 31260
	public float m_FollowCurvePositionAttraction = 0.5f;

	// Token: 0x04007A1D RID: 31261
	public float m_FollowCurvePositionIntensity = 1.7f;

	// Token: 0x04007A1E RID: 31262
	public AnimationCurve m_FollowCurvePositionOverLifetime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 1f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04007A1F RID: 31263
	public bool m_CurlNoise;

	// Token: 0x04007A20 RID: 31264
	public float m_CurlNoisePower = 1f;

	// Token: 0x04007A21 RID: 31265
	public AnimationCurve m_CurlNoiseOverLifetime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04007A22 RID: 31266
	public float m_CurlNoiseScale = 1f;

	// Token: 0x04007A23 RID: 31267
	public Vector3 m_CurlNoiseAnimation = Vector3.zero;

	// Token: 0x04007A24 RID: 31268
	public float m_CurlNoiseGizmoSize = 1f;

	// Token: 0x04007A25 RID: 31269
	public bool m_Twinkle;

	// Token: 0x04007A26 RID: 31270
	public float m_TwinkleRate = 1f;

	// Token: 0x04007A27 RID: 31271
	[Range(-1f, 1f)]
	public float m_TwinkleBias;

	// Token: 0x04007A28 RID: 31272
	public AnimationCurve m_TwinkleOverLifetime = new AnimationCurve(new Keyframe[]
	{
		new Keyframe(0f, 0f),
		new Keyframe(1f, 1f)
	});

	// Token: 0x04007A29 RID: 31273
	private List<ParticleSystem> m_particleSystems = new List<ParticleSystem>();

	// Token: 0x04007A2A RID: 31274
	private float m_time;

	// Token: 0x04007A2B RID: 31275
	private int m_followCurveIntervalIndex = 1;

	// Token: 0x04007A2C RID: 31276
	private int m_curlNoiseIntervalIndex = 2;
}
