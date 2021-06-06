using UnityEngine;

public class TimeDelete : MonoBehaviour
{
	public float m_SecondsToDelete = 10f;

	private float m_StartTime;

	private void Start()
	{
		m_StartTime = Time.time;
	}

	private void Update()
	{
		if (Time.time > m_StartTime + m_SecondsToDelete)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
