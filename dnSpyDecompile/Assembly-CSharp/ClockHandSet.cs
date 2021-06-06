using System;
using UnityEngine;

// Token: 0x02000A1C RID: 2588
public class ClockHandSet : MonoBehaviour
{
	// Token: 0x06008B8C RID: 35724 RVA: 0x002CA080 File Offset: 0x002C8280
	private void Update()
	{
		DateTime now = DateTime.Now;
		int minute = now.Minute;
		if (minute != this.m_prevMinute)
		{
			float num = this.ComputeMinuteRotation(minute);
			float num2 = this.ComputeMinuteRotation(this.m_prevMinute);
			float angle = num - num2;
			this.m_MinuteHand.transform.Rotate(Vector3.up, angle);
			this.m_prevMinute = minute;
		}
		int num3 = now.Hour % 12;
		if (num3 != this.m_prevHour)
		{
			float num4 = this.ComputeHourRotation(num3);
			float num5 = this.ComputeHourRotation(this.m_prevHour);
			float angle2 = num4 - num5;
			this.m_HourHand.transform.Rotate(Vector3.up, angle2);
			this.m_prevHour = num3;
		}
	}

	// Token: 0x06008B8D RID: 35725 RVA: 0x002CA126 File Offset: 0x002C8326
	private float ComputeMinuteRotation(int minute)
	{
		return (float)minute * 6f;
	}

	// Token: 0x06008B8E RID: 35726 RVA: 0x002CA130 File Offset: 0x002C8330
	private float ComputeHourRotation(int hour)
	{
		return (float)hour * 30f;
	}

	// Token: 0x04007417 RID: 29719
	public GameObject m_MinuteHand;

	// Token: 0x04007418 RID: 29720
	public GameObject m_HourHand;

	// Token: 0x04007419 RID: 29721
	private int m_prevMinute;

	// Token: 0x0400741A RID: 29722
	private int m_prevHour;
}
