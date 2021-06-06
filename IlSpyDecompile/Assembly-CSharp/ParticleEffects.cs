using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ParticleEffects : MonoBehaviour
{
	public List<ParticleSystem> m_ParticleSystems;

	public bool m_WorldSpace;

	public ParticleEffectsOrientation m_ParticleOrientation;

	public List<ParticleEffectsAttractor> m_ParticleAttractors;

	public List<ParticleEffectsRepulser> m_ParticleRepulsers;

	private void Update()
	{
		if (m_ParticleSystems == null)
		{
			return;
		}
		if (m_ParticleSystems.Count == 0)
		{
			ParticleSystem component = GetComponent<ParticleSystem>();
			if (component == null)
			{
				base.enabled = false;
			}
			m_ParticleSystems.Add(component);
		}
		for (int i = 0; i < m_ParticleSystems.Count; i++)
		{
			ParticleSystem particleSystem = m_ParticleSystems[i];
			if (!(particleSystem == null))
			{
				int particleCount = particleSystem.particleCount;
				if (particleCount == 0)
				{
					break;
				}
				ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleCount];
				particleSystem.GetParticles(particles);
				if (m_ParticleAttractors != null)
				{
					ParticleAttractor(particleSystem, particles, particleCount);
				}
				if (m_ParticleRepulsers != null)
				{
					ParticleRepulser(particleSystem, particles, particleCount);
				}
				if (m_ParticleOrientation != null && m_ParticleOrientation.m_OrientToDirection)
				{
					OrientParticlesToDirection(particleSystem, particles, particleCount);
				}
				particleSystem.SetParticles(particles, particleCount);
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (m_ParticleAttractors != null)
		{
			foreach (ParticleEffectsAttractor particleAttractor in m_ParticleAttractors)
			{
				if (!(particleAttractor.m_Transform == null))
				{
					Gizmos.color = Color.green;
					float radius = particleAttractor.m_Radius * ((particleAttractor.m_Transform.lossyScale.x + particleAttractor.m_Transform.lossyScale.y + particleAttractor.m_Transform.lossyScale.z) * 0.333f);
					Gizmos.DrawWireSphere(particleAttractor.m_Transform.position, radius);
				}
			}
		}
		if (m_ParticleRepulsers == null)
		{
			return;
		}
		foreach (ParticleEffectsRepulser particleRepulser in m_ParticleRepulsers)
		{
			if (!(particleRepulser.m_Transform == null))
			{
				Gizmos.color = Color.red;
				float radius2 = particleRepulser.m_Radius * ((particleRepulser.m_Transform.lossyScale.x + particleRepulser.m_Transform.lossyScale.y + particleRepulser.m_Transform.lossyScale.z) * 0.333f);
				Gizmos.DrawWireSphere(particleRepulser.m_Transform.position, radius2);
			}
		}
	}

	private void OrientParticlesToDirection(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		for (int i = 0; i < particleCount; i++)
		{
			particles[i].angularVelocity = 0f;
			Vector3 targetVector = particles[i].velocity;
			if (!m_WorldSpace)
			{
				targetVector = particleSystem.transform.TransformDirection(particles[i].velocity);
			}
			if (m_ParticleOrientation.m_UpVector == ParticleEffectsOrientUpVectors.Horizontal)
			{
				particles[i].rotation = VectorAngle(Vector3.forward, targetVector, Vector3.up);
			}
			else if (m_ParticleOrientation.m_UpVector == ParticleEffectsOrientUpVectors.Vertical)
			{
				particles[i].rotation = VectorAngle(Vector3.up, targetVector, Vector3.forward);
			}
		}
	}

	private void ParticleAttractor(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		for (int i = 0; i < particleCount; i++)
		{
			foreach (ParticleEffectsAttractor particleAttractor in m_ParticleAttractors)
			{
				if (!(particleAttractor.m_Transform == null) && !(particleAttractor.m_Radius <= 0f) && !(particleAttractor.m_Power <= 0f))
				{
					Vector3 vector = particles[i].position;
					if (!m_WorldSpace)
					{
						vector = particleSystem.transform.TransformPoint(particles[i].position);
					}
					Vector3 vector2 = particleAttractor.m_Transform.position - vector;
					float num = particleAttractor.m_Radius * ((particleAttractor.m_Transform.lossyScale.x + particleAttractor.m_Transform.lossyScale.y + particleAttractor.m_Transform.lossyScale.z) * 0.333f);
					float num2 = (1f - vector2.magnitude / num) * particleAttractor.m_Power;
					Vector3 b = vector2 * particles[i].velocity.magnitude;
					if (!m_WorldSpace)
					{
						b = particleSystem.transform.InverseTransformDirection(vector2 * particles[i].velocity.magnitude);
					}
					Vector3 velocity = Vector3.Lerp(particles[i].velocity, b, num2 * Time.deltaTime).normalized * particles[i].velocity.magnitude;
					particles[i].velocity = velocity;
				}
			}
		}
	}

	private void ParticleRepulser(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		for (int i = 0; i < particleCount; i++)
		{
			foreach (ParticleEffectsRepulser particleRepulser in m_ParticleRepulsers)
			{
				if (!(particleRepulser.m_Transform == null) && !(particleRepulser.m_Radius <= 0f) && !(particleRepulser.m_Power <= 0f))
				{
					Vector3 vector = particles[i].position;
					if (!m_WorldSpace)
					{
						vector = particleSystem.transform.TransformPoint(particles[i].position);
					}
					Vector3 vector2 = particleRepulser.m_Transform.position - vector;
					float num = particleRepulser.m_Radius * ((particleRepulser.m_Transform.lossyScale.x + particleRepulser.m_Transform.lossyScale.y + particleRepulser.m_Transform.lossyScale.z) * 0.333f);
					float num2 = (1f - vector2.magnitude / num) * particleRepulser.m_Power + particleRepulser.m_Power;
					Vector3 b = -vector2 * particles[i].velocity.magnitude;
					if (!m_WorldSpace)
					{
						b = particleSystem.transform.InverseTransformDirection(-vector2 * particles[i].velocity.magnitude);
					}
					Vector3 velocity = Vector3.Lerp(particles[i].velocity, b, num2 * Time.deltaTime).normalized * particles[i].velocity.magnitude;
					particles[i].velocity = velocity;
				}
			}
		}
	}

	private static float VectorAngle(Vector3 forwardVector, Vector3 targetVector, Vector3 upVector)
	{
		float num = Vector3.Angle(forwardVector, targetVector);
		if (Vector3.Dot(Vector3.Cross(forwardVector, targetVector), upVector) < 0f)
		{
			return 360f - num;
		}
		return num;
	}
}
