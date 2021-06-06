using System;
using UnityEngine;

public class ClockHandSet : MonoBehaviour
{
	public GameObject m_MinuteHand;

	public GameObject m_HourHand;

	private int m_prevMinute;

	private int m_prevHour;

	private void Update()
	{
		DateTime now = DateTime.Now;
		int minute = now.Minute;
		if (minute != m_prevMinute)
		{
			float num = ComputeMinuteRotation(minute);
			float num2 = ComputeMinuteRotation(m_prevMinute);
			float angle = num - num2;
			m_MinuteHand.transform.Rotate(Vector3.up, angle);
			m_prevMinute = minute;
		}
		int num3 = now.Hour % 12;
		if (num3 != m_prevHour)
		{
			float num4 = ComputeHourRotation(num3);
			float num5 = ComputeHourRotation(m_prevHour);
			float angle2 = num4 - num5;
			m_HourHand.transform.Rotate(Vector3.up, angle2);
			m_prevHour = num3;
		}
	}

	private float ComputeMinuteRotation(int minute)
	{
		return (float)minute * 6f;
	}

	private float ComputeHourRotation(int hour)
	{
		return (float)hour * 30f;
	}
}
