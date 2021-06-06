using UnityEngine;

public class RandomPlayAnimation : MonoBehaviour
{
	public float m_MinWaitTime;

	public float m_MaxWaitTime = 10f;

	private float m_waitTime = -1f;

	private float m_startTime;

	private Animation m_animation;

	private void Start()
	{
		m_animation = base.gameObject.GetComponent<Animation>();
	}

	private void Update()
	{
		if (m_animation == null)
		{
			base.enabled = false;
		}
		if (m_waitTime < 0f)
		{
			if (m_MinWaitTime < 0f)
			{
				m_MinWaitTime = 0f;
			}
			if (m_MaxWaitTime < 0f)
			{
				m_MaxWaitTime = 0f;
			}
			if (m_MaxWaitTime < m_MinWaitTime)
			{
				m_MaxWaitTime = m_MinWaitTime;
			}
			m_waitTime = Random.Range(m_MinWaitTime, m_MaxWaitTime);
			m_startTime = Time.time;
		}
		if (Time.time - m_startTime > m_waitTime)
		{
			m_waitTime = -1f;
			m_animation.Play();
		}
	}
}
