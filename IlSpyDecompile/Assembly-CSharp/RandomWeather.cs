using UnityEngine;

public class RandomWeather : MonoBehaviour
{
	public float m_StartDelayMinMinutes = 1f;

	public float m_StartDelayMaxMinutes = 10f;

	public float m_WeatherMinMinutes = 2f;

	public float m_WeatherMaxMinutes = 5f;

	private ParticleSystem[] m_particleSystems;

	private float m_startTime;

	private float m_runEndTime;

	private bool m_active;

	private void Start()
	{
		m_particleSystems = GetComponentsInChildren<ParticleSystem>();
		m_startTime = Random.Range(Time.timeSinceLevelLoad + m_StartDelayMinMinutes * 60f, Time.timeSinceLevelLoad + m_StartDelayMaxMinutes * 60f);
	}

	private void Update()
	{
		if (m_active)
		{
			if (Time.timeSinceLevelLoad > m_runEndTime)
			{
				StopWeather();
			}
		}
		else if (Time.timeSinceLevelLoad > m_startTime)
		{
			StartWeather();
			m_startTime = Random.Range(Time.timeSinceLevelLoad + m_StartDelayMinMinutes * 60f, Time.timeSinceLevelLoad + m_StartDelayMaxMinutes * 60f);
		}
	}

	[ContextMenu("Start Weather")]
	private void StartWeather()
	{
		m_active = true;
		m_runEndTime = Random.Range(Time.timeSinceLevelLoad + m_WeatherMinMinutes * 60f, Time.timeSinceLevelLoad + m_WeatherMaxMinutes * 60f);
		ParticleSystem[] particleSystems = m_particleSystems;
		foreach (ParticleSystem particleSystem in particleSystems)
		{
			if (!(particleSystem == null))
			{
				particleSystem.Play();
			}
		}
	}

	[ContextMenu("Stop Weather")]
	private void StopWeather()
	{
		m_active = false;
		ParticleSystem[] particleSystems = m_particleSystems;
		foreach (ParticleSystem particleSystem in particleSystems)
		{
			if (!(particleSystem == null))
			{
				particleSystem.Stop();
			}
		}
	}
}
