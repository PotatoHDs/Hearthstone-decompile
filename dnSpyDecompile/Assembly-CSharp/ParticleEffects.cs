using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A5C RID: 2652
[ExecuteAlways]
public class ParticleEffects : MonoBehaviour
{
	// Token: 0x06008EAD RID: 36525 RVA: 0x002E0CF0 File Offset: 0x002DEEF0
	private void Update()
	{
		if (this.m_ParticleSystems == null)
		{
			return;
		}
		if (this.m_ParticleSystems.Count == 0)
		{
			ParticleSystem component = base.GetComponent<ParticleSystem>();
			if (component == null)
			{
				base.enabled = false;
			}
			this.m_ParticleSystems.Add(component);
		}
		for (int i = 0; i < this.m_ParticleSystems.Count; i++)
		{
			ParticleSystem particleSystem = this.m_ParticleSystems[i];
			if (!(particleSystem == null))
			{
				int particleCount = particleSystem.particleCount;
				if (particleCount == 0)
				{
					return;
				}
				ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleCount];
				particleSystem.GetParticles(particles);
				if (this.m_ParticleAttractors != null)
				{
					this.ParticleAttractor(particleSystem, particles, particleCount);
				}
				if (this.m_ParticleRepulsers != null)
				{
					this.ParticleRepulser(particleSystem, particles, particleCount);
				}
				if (this.m_ParticleOrientation != null && this.m_ParticleOrientation.m_OrientToDirection)
				{
					this.OrientParticlesToDirection(particleSystem, particles, particleCount);
				}
				particleSystem.SetParticles(particles, particleCount);
			}
		}
	}

	// Token: 0x06008EAE RID: 36526 RVA: 0x002E0DD0 File Offset: 0x002DEFD0
	private void OnDrawGizmos()
	{
		if (this.m_ParticleAttractors != null)
		{
			foreach (ParticleEffectsAttractor particleEffectsAttractor in this.m_ParticleAttractors)
			{
				if (!(particleEffectsAttractor.m_Transform == null))
				{
					Gizmos.color = Color.green;
					float radius = particleEffectsAttractor.m_Radius * ((particleEffectsAttractor.m_Transform.lossyScale.x + particleEffectsAttractor.m_Transform.lossyScale.y + particleEffectsAttractor.m_Transform.lossyScale.z) * 0.333f);
					Gizmos.DrawWireSphere(particleEffectsAttractor.m_Transform.position, radius);
				}
			}
		}
		if (this.m_ParticleRepulsers != null)
		{
			foreach (ParticleEffectsRepulser particleEffectsRepulser in this.m_ParticleRepulsers)
			{
				if (!(particleEffectsRepulser.m_Transform == null))
				{
					Gizmos.color = Color.red;
					float radius2 = particleEffectsRepulser.m_Radius * ((particleEffectsRepulser.m_Transform.lossyScale.x + particleEffectsRepulser.m_Transform.lossyScale.y + particleEffectsRepulser.m_Transform.lossyScale.z) * 0.333f);
					Gizmos.DrawWireSphere(particleEffectsRepulser.m_Transform.position, radius2);
				}
			}
		}
	}

	// Token: 0x06008EAF RID: 36527 RVA: 0x002E0F4C File Offset: 0x002DF14C
	private void OrientParticlesToDirection(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		for (int i = 0; i < particleCount; i++)
		{
			particles[i].angularVelocity = 0f;
			Vector3 targetVector = particles[i].velocity;
			if (!this.m_WorldSpace)
			{
				targetVector = particleSystem.transform.TransformDirection(particles[i].velocity);
			}
			if (this.m_ParticleOrientation.m_UpVector == ParticleEffectsOrientUpVectors.Horizontal)
			{
				particles[i].rotation = ParticleEffects.VectorAngle(Vector3.forward, targetVector, Vector3.up);
			}
			else if (this.m_ParticleOrientation.m_UpVector == ParticleEffectsOrientUpVectors.Vertical)
			{
				particles[i].rotation = ParticleEffects.VectorAngle(Vector3.up, targetVector, Vector3.forward);
			}
		}
	}

	// Token: 0x06008EB0 RID: 36528 RVA: 0x002E1000 File Offset: 0x002DF200
	private void ParticleAttractor(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		for (int i = 0; i < particleCount; i++)
		{
			foreach (ParticleEffectsAttractor particleEffectsAttractor in this.m_ParticleAttractors)
			{
				if (!(particleEffectsAttractor.m_Transform == null) && particleEffectsAttractor.m_Radius > 0f && particleEffectsAttractor.m_Power > 0f)
				{
					Vector3 b = particles[i].position;
					if (!this.m_WorldSpace)
					{
						b = particleSystem.transform.TransformPoint(particles[i].position);
					}
					Vector3 a = particleEffectsAttractor.m_Transform.position - b;
					float num = particleEffectsAttractor.m_Radius * ((particleEffectsAttractor.m_Transform.lossyScale.x + particleEffectsAttractor.m_Transform.lossyScale.y + particleEffectsAttractor.m_Transform.lossyScale.z) * 0.333f);
					float num2 = (1f - a.magnitude / num) * particleEffectsAttractor.m_Power;
					Vector3 b2 = a * particles[i].velocity.magnitude;
					if (!this.m_WorldSpace)
					{
						b2 = particleSystem.transform.InverseTransformDirection(a * particles[i].velocity.magnitude);
					}
					Vector3 velocity = Vector3.Lerp(particles[i].velocity, b2, num2 * Time.deltaTime).normalized * particles[i].velocity.magnitude;
					particles[i].velocity = velocity;
				}
			}
		}
	}

	// Token: 0x06008EB1 RID: 36529 RVA: 0x002E11D8 File Offset: 0x002DF3D8
	private void ParticleRepulser(ParticleSystem particleSystem, ParticleSystem.Particle[] particles, int particleCount)
	{
		for (int i = 0; i < particleCount; i++)
		{
			foreach (ParticleEffectsRepulser particleEffectsRepulser in this.m_ParticleRepulsers)
			{
				if (!(particleEffectsRepulser.m_Transform == null) && particleEffectsRepulser.m_Radius > 0f && particleEffectsRepulser.m_Power > 0f)
				{
					Vector3 b = particles[i].position;
					if (!this.m_WorldSpace)
					{
						b = particleSystem.transform.TransformPoint(particles[i].position);
					}
					Vector3 a = particleEffectsRepulser.m_Transform.position - b;
					float num = particleEffectsRepulser.m_Radius * ((particleEffectsRepulser.m_Transform.lossyScale.x + particleEffectsRepulser.m_Transform.lossyScale.y + particleEffectsRepulser.m_Transform.lossyScale.z) * 0.333f);
					float num2 = (1f - a.magnitude / num) * particleEffectsRepulser.m_Power + particleEffectsRepulser.m_Power;
					Vector3 b2 = -a * particles[i].velocity.magnitude;
					if (!this.m_WorldSpace)
					{
						b2 = particleSystem.transform.InverseTransformDirection(-a * particles[i].velocity.magnitude);
					}
					Vector3 velocity = Vector3.Lerp(particles[i].velocity, b2, num2 * Time.deltaTime).normalized * particles[i].velocity.magnitude;
					particles[i].velocity = velocity;
				}
			}
		}
	}

	// Token: 0x06008EB2 RID: 36530 RVA: 0x002E13C4 File Offset: 0x002DF5C4
	private static float VectorAngle(Vector3 forwardVector, Vector3 targetVector, Vector3 upVector)
	{
		float num = Vector3.Angle(forwardVector, targetVector);
		if (Vector3.Dot(Vector3.Cross(forwardVector, targetVector), upVector) < 0f)
		{
			return 360f - num;
		}
		return num;
	}

	// Token: 0x04007705 RID: 30469
	public List<ParticleSystem> m_ParticleSystems;

	// Token: 0x04007706 RID: 30470
	public bool m_WorldSpace;

	// Token: 0x04007707 RID: 30471
	public ParticleEffectsOrientation m_ParticleOrientation;

	// Token: 0x04007708 RID: 30472
	public List<ParticleEffectsAttractor> m_ParticleAttractors;

	// Token: 0x04007709 RID: 30473
	public List<ParticleEffectsRepulser> m_ParticleRepulsers;
}
