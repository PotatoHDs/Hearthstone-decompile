using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ParticlePlaybackSpeed : MonoBehaviour
{
	public float m_ParticlePlaybackSpeed = 1f;

	public bool m_RestoreSpeedOnDisable = true;

	private float m_PreviousPlaybackSpeed = 1f;

	private Map<ParticleSystem, float> m_OrgPlaybackSpeed;

	private List<ParticleSystem> m_ParticleSystems;

	private void Start()
	{
		Init();
	}

	private void Update()
	{
		if (m_ParticlePlaybackSpeed == m_PreviousPlaybackSpeed)
		{
			return;
		}
		m_PreviousPlaybackSpeed = m_ParticlePlaybackSpeed;
		int num = 0;
		while (num < m_ParticleSystems.Count)
		{
			ParticleSystem particleSystem = m_ParticleSystems[num];
			if ((bool)particleSystem)
			{
				ParticleSystem.MainModule main = particleSystem.main;
				main.simulationSpeed = m_ParticlePlaybackSpeed;
				num++;
			}
			else
			{
				m_OrgPlaybackSpeed.Remove(particleSystem);
				m_ParticleSystems.RemoveAt(num);
			}
		}
	}

	private void OnDisable()
	{
		if (m_RestoreSpeedOnDisable)
		{
			foreach (KeyValuePair<ParticleSystem, float> item in m_OrgPlaybackSpeed)
			{
				ParticleSystem key = item.Key;
				float value = item.Value;
				if ((bool)key)
				{
					ParticleSystem.MainModule main = key.main;
					main.simulationSpeed = value;
				}
			}
		}
		m_PreviousPlaybackSpeed = -1E+07f;
		m_ParticleSystems.Clear();
		m_OrgPlaybackSpeed.Clear();
	}

	private void OnEnable()
	{
		Init();
	}

	private void Init()
	{
		if (m_ParticleSystems == null)
		{
			m_ParticleSystems = new List<ParticleSystem>();
		}
		else
		{
			m_ParticleSystems.Clear();
		}
		if (m_OrgPlaybackSpeed == null)
		{
			m_OrgPlaybackSpeed = new Map<ParticleSystem, float>();
		}
		else
		{
			m_OrgPlaybackSpeed.Clear();
		}
		ParticleSystem[] componentsInChildren = base.gameObject.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem particleSystem in componentsInChildren)
		{
			m_OrgPlaybackSpeed.Add(particleSystem, particleSystem.main.simulationSpeed);
			m_ParticleSystems.Add(particleSystem);
		}
	}
}
