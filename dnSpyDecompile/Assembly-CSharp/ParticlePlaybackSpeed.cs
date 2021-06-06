using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A5D RID: 2653
[ExecuteAlways]
public class ParticlePlaybackSpeed : MonoBehaviour
{
	// Token: 0x06008EB4 RID: 36532 RVA: 0x002E13F6 File Offset: 0x002DF5F6
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06008EB5 RID: 36533 RVA: 0x002E1400 File Offset: 0x002DF600
	private void Update()
	{
		if (this.m_ParticlePlaybackSpeed == this.m_PreviousPlaybackSpeed)
		{
			return;
		}
		this.m_PreviousPlaybackSpeed = this.m_ParticlePlaybackSpeed;
		int i = 0;
		while (i < this.m_ParticleSystems.Count)
		{
			ParticleSystem particleSystem = this.m_ParticleSystems[i];
			if (particleSystem)
			{
				particleSystem.main.simulationSpeed = this.m_ParticlePlaybackSpeed;
				i++;
			}
			else
			{
				this.m_OrgPlaybackSpeed.Remove(particleSystem);
				this.m_ParticleSystems.RemoveAt(i);
			}
		}
	}

	// Token: 0x06008EB6 RID: 36534 RVA: 0x002E1484 File Offset: 0x002DF684
	private void OnDisable()
	{
		if (this.m_RestoreSpeedOnDisable)
		{
			foreach (KeyValuePair<ParticleSystem, float> keyValuePair in this.m_OrgPlaybackSpeed)
			{
				ParticleSystem key = keyValuePair.Key;
				float value = keyValuePair.Value;
				if (key)
				{
					key.main.simulationSpeed = value;
				}
			}
		}
		this.m_PreviousPlaybackSpeed = -10000000f;
		this.m_ParticleSystems.Clear();
		this.m_OrgPlaybackSpeed.Clear();
	}

	// Token: 0x06008EB7 RID: 36535 RVA: 0x002E13F6 File Offset: 0x002DF5F6
	private void OnEnable()
	{
		this.Init();
	}

	// Token: 0x06008EB8 RID: 36536 RVA: 0x002E1524 File Offset: 0x002DF724
	private void Init()
	{
		if (this.m_ParticleSystems == null)
		{
			this.m_ParticleSystems = new List<ParticleSystem>();
		}
		else
		{
			this.m_ParticleSystems.Clear();
		}
		if (this.m_OrgPlaybackSpeed == null)
		{
			this.m_OrgPlaybackSpeed = new Map<ParticleSystem, float>();
		}
		else
		{
			this.m_OrgPlaybackSpeed.Clear();
		}
		foreach (ParticleSystem particleSystem in base.gameObject.GetComponentsInChildren<ParticleSystem>())
		{
			this.m_OrgPlaybackSpeed.Add(particleSystem, particleSystem.main.simulationSpeed);
			this.m_ParticleSystems.Add(particleSystem);
		}
	}

	// Token: 0x0400770A RID: 30474
	public float m_ParticlePlaybackSpeed = 1f;

	// Token: 0x0400770B RID: 30475
	public bool m_RestoreSpeedOnDisable = true;

	// Token: 0x0400770C RID: 30476
	private float m_PreviousPlaybackSpeed = 1f;

	// Token: 0x0400770D RID: 30477
	private Map<ParticleSystem, float> m_OrgPlaybackSpeed;

	// Token: 0x0400770E RID: 30478
	private List<ParticleSystem> m_ParticleSystems;
}
