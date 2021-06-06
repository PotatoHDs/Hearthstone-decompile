using System;
using UnityEngine;

// Token: 0x02000A6D RID: 2669
public class RandomWeather : MonoBehaviour
{
	// Token: 0x06008F19 RID: 36633 RVA: 0x002E3F12 File Offset: 0x002E2112
	private void Start()
	{
		this.m_particleSystems = base.GetComponentsInChildren<ParticleSystem>();
		this.m_startTime = UnityEngine.Random.Range(Time.timeSinceLevelLoad + this.m_StartDelayMinMinutes * 60f, Time.timeSinceLevelLoad + this.m_StartDelayMaxMinutes * 60f);
	}

	// Token: 0x06008F1A RID: 36634 RVA: 0x002E3F50 File Offset: 0x002E2150
	private void Update()
	{
		if (this.m_active)
		{
			if (Time.timeSinceLevelLoad > this.m_runEndTime)
			{
				this.StopWeather();
				return;
			}
		}
		else if (Time.timeSinceLevelLoad > this.m_startTime)
		{
			this.StartWeather();
			this.m_startTime = UnityEngine.Random.Range(Time.timeSinceLevelLoad + this.m_StartDelayMinMinutes * 60f, Time.timeSinceLevelLoad + this.m_StartDelayMaxMinutes * 60f);
		}
	}

	// Token: 0x06008F1B RID: 36635 RVA: 0x002E3FBC File Offset: 0x002E21BC
	[ContextMenu("Start Weather")]
	private void StartWeather()
	{
		this.m_active = true;
		this.m_runEndTime = UnityEngine.Random.Range(Time.timeSinceLevelLoad + this.m_WeatherMinMinutes * 60f, Time.timeSinceLevelLoad + this.m_WeatherMaxMinutes * 60f);
		foreach (ParticleSystem particleSystem in this.m_particleSystems)
		{
			if (!(particleSystem == null))
			{
				particleSystem.Play();
			}
		}
	}

	// Token: 0x06008F1C RID: 36636 RVA: 0x002E4028 File Offset: 0x002E2228
	[ContextMenu("Stop Weather")]
	private void StopWeather()
	{
		this.m_active = false;
		foreach (ParticleSystem particleSystem in this.m_particleSystems)
		{
			if (!(particleSystem == null))
			{
				particleSystem.Stop();
			}
		}
	}

	// Token: 0x0400779E RID: 30622
	public float m_StartDelayMinMinutes = 1f;

	// Token: 0x0400779F RID: 30623
	public float m_StartDelayMaxMinutes = 10f;

	// Token: 0x040077A0 RID: 30624
	public float m_WeatherMinMinutes = 2f;

	// Token: 0x040077A1 RID: 30625
	public float m_WeatherMaxMinutes = 5f;

	// Token: 0x040077A2 RID: 30626
	private ParticleSystem[] m_particleSystems;

	// Token: 0x040077A3 RID: 30627
	private float m_startTime;

	// Token: 0x040077A4 RID: 30628
	private float m_runEndTime;

	// Token: 0x040077A5 RID: 30629
	private bool m_active;
}
