using UnityEngine;

public class MulliganTimer : MonoBehaviour
{
	public UberText m_timeText;

	private bool m_remainingTimeSet;

	private float m_endTimeStamp;

	private void Start()
	{
		if (!(MulliganManager.Get() == null))
		{
			base.transform.position = GetNewPosition();
		}
	}

	private void Update()
	{
		if (!m_remainingTimeSet)
		{
			return;
		}
		Vector3 newPosition = GetNewPosition();
		if (newPosition != base.transform.position)
		{
			base.transform.position = newPosition;
		}
		float num = ComputeCountdownRemainingSec();
		int num2 = Mathf.RoundToInt(num);
		if (num2 < 0)
		{
			num2 = 0;
		}
		m_timeText.Text = $":{num2:D2}";
		if (!(num > 0f))
		{
			if ((bool)MulliganManager.Get())
			{
				MulliganManager.Get().AutomaticContinueMulligan();
			}
			else
			{
				SelfDestruct();
			}
		}
	}

	private Vector3 GetNewPosition()
	{
		if (MulliganManager.Get() == null)
		{
			return new Vector3(100f, 0f, 0f);
		}
		Vector3 mulliganTimerPosition = MulliganManager.Get().GetMulliganTimerPosition();
		return (!UniversalInputManager.UsePhoneUI) ? new Vector3(mulliganTimerPosition.x, mulliganTimerPosition.y, mulliganTimerPosition.z - 1f) : new Vector3(mulliganTimerPosition.x + 1.8f, mulliganTimerPosition.y, mulliganTimerPosition.z);
	}

	private float ComputeCountdownRemainingSec()
	{
		float num = m_endTimeStamp - Time.realtimeSinceStartup;
		if (num < 0f)
		{
			return 0f;
		}
		return num;
	}

	public void SetEndTime(float endTimeStamp)
	{
		m_endTimeStamp = endTimeStamp;
		m_remainingTimeSet = true;
	}

	public void SelfDestruct()
	{
		Object.Destroy(base.gameObject);
	}
}
