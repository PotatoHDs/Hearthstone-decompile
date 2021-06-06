using UnityEngine;

public class ConnectionIndicator : MonoBehaviour
{
	public GameObject m_indicator;

	private static ConnectionIndicator s_instance;

	private bool m_active;

	private const float LATENCY_TOLERANCE = 3f;

	private void Awake()
	{
		s_instance = this;
		m_active = false;
		m_indicator.SetActive(value: false);
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static ConnectionIndicator Get()
	{
		return s_instance;
	}

	private void SetIndicator(bool val)
	{
		if (val != m_active)
		{
			m_active = val;
			m_indicator.SetActive(val);
			BnetBar.Get().UpdateLayout();
		}
	}

	public bool IsVisible()
	{
		return m_active;
	}

	private void Update()
	{
		SetIndicator(Network.Get().TimeSinceLastPong() > 3.0);
	}
}
