using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class UberShuriken : MonoBehaviour
{
	private const int VORTEX_NOISE_INVERVAL = 3;

	private const int FOLLOW_CURVE_INVERVAL = 3;

	private const int CURL_NOISE_INVERVAL = 3;

	public bool m_IncludeChildren;

	public UberCurve m_UberCurve;

	public bool m_FollowCurveDirection;

	public bool m_FollowCurvePosition;

	public float m_FollowCurvePositionAttraction = 0.5f;

	public float m_FollowCurvePositionIntensity = 1.7f;

	public AnimationCurve m_FollowCurvePositionOverLifetime = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

	public bool m_CurlNoise;

	public float m_CurlNoisePower = 1f;

	public AnimationCurve m_CurlNoiseOverLifetime = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

	public float m_CurlNoiseScale = 1f;

	public Vector3 m_CurlNoiseAnimation = Vector3.zero;

	public float m_CurlNoiseGizmoSize = 1f;

	public bool m_Twinkle;

	public float m_TwinkleRate = 1f;

	[Range(-1f, 1f)]
	public float m_TwinkleBias;

	public AnimationCurve m_TwinkleOverLifetime = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

	private List<ParticleSystem> m_particleSystems = new List<ParticleSystem>();

	private float m_time;

	private int m_followCurveIntervalIndex = 1;

	private int m_curlNoiseIntervalIndex = 2;

	private void Awake()
	{
		if (m_UberCurve == null)
		{
			m_UberCurve = GetComponent<UberCurve>();
		}
		UpdateParticleSystemList();
	}

	private void Update()
	{
		m_time = Time.time;
		UpdateParticles();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = Color.blue;
		if (m_CurlNoise && m_CurlNoiseGizmoSize > 0f)
		{
			int num = 10;
			float num2 = Mathf.Max(Mathf.Abs(m_CurlNoiseScale * 0.25f), 1f) * m_CurlNoiseGizmoSize;
			float num3 = 1f / ((float)num * 1.2f);
			float num4 = 1f;
			float num5 = 0f;
			float num6 = 0f;
			for (int i = 0; i < num; i++)
			{
				Gizmos.color = new Color(0f, 0f, 1f - num4, 1f);
				num4 -= num3;
				num5 = (float)i * 0.75f;
				Vector4[] array = GizmoCirclePoints(20 * Mathf.Max(Mathf.FloorToInt(Mathf.Abs(m_CurlNoiseScale)), 10), num5 * num2);
				Vector4 vector = array[array.Length - 1];
				for (int j = 0; j < array.Length; j++)
				{
					Gizmos.color = new Color(array[j].w * 0.5f, array[j].w, 1f, 1f);
					Gizmos.DrawLine(vector, array[j]);
					vector = array[j];
				}
				Vector4[] array2 = GizmoCircleLines(10, num6 * num2, num5 * num2);
				for (int k = 0; k < array2.Length; k += 2)
				{
					Gizmos.color = new Color(array2[k].w * 0.5f, array2[k].w, 1f, 1f);
					Gizmos.DrawLine(array2[k], array2[k + 1]);
				}
				num6 = num5;
			}
		}
		Gizmos.matrix = Matrix4x4.identity;
	}

	private Vector4[] GizmoCirclePoints(int numOfPoints, float radius)
	{
		Vector4[] array = new Vector4[numOfPoints];
		float num = 0f;
		float num2 = (float)Math.PI * 2f / (float)numOfPoints;
		for (int i = 0; i < numOfPoints; i++)
		{
			num += num2;
			array[i] = GizmoCurlNoisePoint(new Vector3(Mathf.Cos(num) * radius, Mathf.Sin(num) * radius, 0f));
		}
		return array;
	}

	private Vector4[] GizmoCircleLines(int numOfPoints, float previousRadius, float radius)
	{
		int num = numOfPoints * 2;
		Vector4[] array = new Vector4[num];
		float num2 = 0f;
		float num3 = 6.283f / (float)numOfPoints;
		for (int i = 0; i < num; i += 2)
		{
			num2 += num3;
			array[i] = GizmoCurlNoisePoint(new Vector3(Mathf.Cos(num2) * previousRadius, Mathf.Sin(num2) * previousRadius, 0f));
			array[i + 1] = GizmoCurlNoisePoint(new Vector3(Mathf.Cos(num2) * radius, Mathf.Sin(num2) * radius, 0f));
		}
		return array;
	}

	private Vector4 GizmoCurlNoisePoint(Vector3 point)
	{
		float time = m_time;
		float num = m_CurlNoiseAnimation.x * time;
		float num2 = m_CurlNoiseAnimation.y * time;
		float num3 = m_CurlNoiseAnimation.z * time;
		Vector3 vector = point * m_CurlNoiseScale * 0.1f;
		float num4 = UberMath.SimplexNoise(5f + vector.x + num, vector.y + num2, vector.z + num3) * m_CurlNoisePower;
		float num5 = UberMath.SimplexNoise(6f + vector.y + num, vector.z + num2, vector.x + num3) * m_CurlNoisePower;
		float num6 = UberMath.SimplexNoise(7f + vector.z + num, vector.x + num2, vector.y + num3) * m_CurlNoisePower;
		Vector3 vector2 = new Vector3(point.x + num4, point.y + num5, point.z + num6);
		float num7 = 1f;
		num7 = Mathf.Max(num4, Mathf.Max(num5, num6));
		return new Vector4(vector2.x, vector2.y, vector2.z, num7);
	}

	private void UpdateParticles()
	{
		m_followCurveIntervalIndex = ((m_followCurveIntervalIndex + 1 > 3) ? (m_followCurveIntervalIndex = 0) : (m_followCurveIntervalIndex + 1));
		m_curlNoiseIntervalIndex = ((m_curlNoiseIntervalIndex + 1 <= 3) ? (m_curlNoiseIntervalIndex + 1) : 0);
		foreach (ParticleSystem particleSystem in m_particleSystems)
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
				if (m_FollowCurveDirection || m_FollowCurvePosition)
				{
					FollowCurveOverLife(particleSystem, particles, particleCount);
				}
				if (m_CurlNoise)
				{
					ParticleCurlNoise(particleSystem, particles, particleCount);
				}
				if (m_Twinkle)
				{
					ParticleTwinkle(particleSystem, particles, particleCount);
				}
				particleSystem.SetParticles(particles, particleCount);
			}
		}
	}

	private void UpdateParticleSystemList()
	{
		m_particleSystems.Clear();
		if (m_IncludeChildren)
		{
			ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>();
			if (GetComponent<ParticleSystem>() == null || componentsInChildren.Length == 0)
			{
				Debug.LogError("Failed to find a ParticleSystem");
			}
			ParticleSystem[] array = componentsInChildren;
			foreach (ParticleSystem item in array)
			{
				m_particleSystems.Add(item);
			}
		}
		else
		{
			ParticleSystem component = GetComponent<ParticleSystem>();
			if (component == null)
			{
				Debug.LogError("Failed to find a ParticleSystem");
			}
			m_particleSystems.Add(component);
		}
	}

	private void FollowCurveOverLife(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		if (m_UberCurve == null)
		{
			CreateCurve();
		}
		for (int i = m_followCurveIntervalIndex; i < particleCount; i += 3)
		{
			float position = 1f - particles[i].remainingLifetime / particles[i].startLifetime;
			if (m_FollowCurvePosition)
			{
				Vector3 zero = Vector3.zero;
				zero = ((particleSystem.main.simulationSpace != ParticleSystemSimulationSpace.World) ? m_UberCurve.CatmullRomEvaluateLocalPosition(position) : m_UberCurve.CatmullRomEvaluateWorldPosition(position));
				Vector3 b = zero - particles[i].position;
				b = Vector3.Lerp(particles[i].velocity, b, m_FollowCurvePositionAttraction);
				particles[i].velocity = b * m_FollowCurvePositionIntensity;
			}
			if (m_FollowCurveDirection)
			{
				Vector3 velocity = m_UberCurve.CatmullRomEvaluateDirection(position).normalized * particles[i].velocity.magnitude;
				particles[i].velocity = velocity;
			}
		}
	}

	private void CreateCurve()
	{
		if (!(m_UberCurve != null))
		{
			m_UberCurve = GetComponent<UberCurve>();
			if (!(m_UberCurve != null))
			{
				m_UberCurve = base.gameObject.AddComponent<UberCurve>();
			}
		}
	}

	private void ParticleCurlNoise(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		float time = m_time;
		float num = m_CurlNoiseAnimation.x * time;
		float num2 = m_CurlNoiseAnimation.y * time;
		float num3 = m_CurlNoiseAnimation.z * time;
		for (int i = m_curlNoiseIntervalIndex; i < particleCount; i += 3)
		{
			float time2 = 1f - particles[i].remainingLifetime / particles[i].startLifetime;
			float num4 = m_CurlNoiseOverLifetime.Evaluate(time2) * m_CurlNoisePower;
			Vector3 velocity = particles[i].velocity;
			Vector3 vector = particles[i].position * m_CurlNoiseScale * 0.1f;
			velocity.x += UberMath.SimplexNoise(5f + vector.x + num, vector.y + num2, vector.z + num3) * num4;
			velocity.y += UberMath.SimplexNoise(6f + vector.y + num, vector.z + num2, vector.x + num3) * num4;
			velocity.z += UberMath.SimplexNoise(7f + vector.z + num, vector.x + num2, vector.y + num3) * num4;
			velocity = velocity.normalized * particles[i].velocity.magnitude;
			particles[i].velocity = velocity;
		}
	}

	private void ParticleTwinkle(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		for (int i = 0; i < particleCount; i++)
		{
			float num = particles[i].remainingLifetime / particles[i].startLifetime;
			Vector3 position = particles[i].position;
			Color color = particles[i].startColor;
			color.a = Mathf.Clamp01(UberMath.SimplexNoise((position.x + position.y + position.z - num - (float)i * 3.33f) * m_TwinkleRate, 0.5f) + m_TwinkleBias + num * m_TwinkleOverLifetime.Evaluate(num));
			particles[i].startColor = color;
		}
	}
}
