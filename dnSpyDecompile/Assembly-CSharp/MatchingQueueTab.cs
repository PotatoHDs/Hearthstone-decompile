using System;
using UnityEngine;

// Token: 0x02000374 RID: 884
public class MatchingQueueTab : MonoBehaviour
{
	// Token: 0x06003402 RID: 13314 RVA: 0x0010ACA7 File Offset: 0x00108EA7
	private void Update()
	{
		this.InitTimeStringSet();
		this.m_timeInQueue += Time.deltaTime;
		this.m_waitTime.Text = TimeUtils.GetElapsedTimeString(Mathf.RoundToInt(this.m_timeInQueue), this.m_timeStringSet, false);
	}

	// Token: 0x06003403 RID: 13315 RVA: 0x0010ACE3 File Offset: 0x00108EE3
	public void Show()
	{
		this.m_root.SetActive(true);
	}

	// Token: 0x06003404 RID: 13316 RVA: 0x0010ACF1 File Offset: 0x00108EF1
	public void Hide()
	{
		this.m_root.SetActive(false);
	}

	// Token: 0x06003405 RID: 13317 RVA: 0x0010ACFF File Offset: 0x00108EFF
	public void ResetTimer()
	{
		this.m_timeInQueue = 0f;
		this.UpdateDisplay(0, 0);
	}

	// Token: 0x06003406 RID: 13318 RVA: 0x0010AD14 File Offset: 0x00108F14
	public void UpdateDisplay(int minSeconds, int maxSeconds)
	{
		this.InitTimeStringSet();
		int num = Mathf.RoundToInt(this.m_timeInQueue);
		maxSeconds += num;
		if (maxSeconds <= 30)
		{
			this.Hide();
			return;
		}
		this.m_queueTime.Text = this.GetElapsedTimeString(minSeconds + num, maxSeconds);
		this.Show();
	}

	// Token: 0x06003407 RID: 13319 RVA: 0x0010AD60 File Offset: 0x00108F60
	private void InitTimeStringSet()
	{
		if (this.m_timeStringSet == null)
		{
			this.m_timeStringSet = new TimeUtils.ElapsedStringSet
			{
				m_seconds = "GLOBAL_DATETIME_SPINNER_SECONDS",
				m_minutes = "GLOBAL_DATETIME_SPINNER_MINUTES",
				m_hours = "GLOBAL_DATETIME_SPINNER_HOURS",
				m_yesterday = "GLOBAL_DATETIME_SPINNER_DAY",
				m_days = "GLOBAL_DATETIME_SPINNER_DAYS",
				m_weeks = "GLOBAL_DATETIME_SPINNER_WEEKS",
				m_monthAgo = "GLOBAL_DATETIME_SPINNER_MONTH"
			};
		}
	}

	// Token: 0x06003408 RID: 13320 RVA: 0x0010ADD0 File Offset: 0x00108FD0
	private string GetElapsedTimeString(int minSeconds, int maxSeconds)
	{
		TimeUtils.ElapsedTimeType elapsedTimeType;
		int num;
		TimeUtils.GetElapsedTime((long)minSeconds, out elapsedTimeType, out num, false);
		if (minSeconds == maxSeconds)
		{
			return GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME", new object[]
			{
				TimeUtils.GetElapsedTimeString(minSeconds, this.m_timeStringSet, false)
			});
		}
		TimeUtils.ElapsedTimeType elapsedTimeType2;
		int num2;
		TimeUtils.GetElapsedTime((long)maxSeconds, out elapsedTimeType2, out num2, false);
		if (elapsedTimeType != elapsedTimeType2)
		{
			string elapsedTimeString = TimeUtils.GetElapsedTimeString(elapsedTimeType, num, this.m_timeStringSet);
			string elapsedTimeString2 = TimeUtils.GetElapsedTimeString(elapsedTimeType2, num2, this.m_timeStringSet);
			return GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME_RANGE", new object[]
			{
				elapsedTimeString,
				elapsedTimeString2
			});
		}
		switch (elapsedTimeType)
		{
		case TimeUtils.ElapsedTimeType.SECONDS:
			return GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME_RANGE", new object[]
			{
				num,
				GameStrings.Format(this.m_timeStringSet.m_seconds, new object[]
				{
					num2
				})
			});
		case TimeUtils.ElapsedTimeType.MINUTES:
			return GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME_RANGE", new object[]
			{
				num,
				GameStrings.Format(this.m_timeStringSet.m_minutes, new object[]
				{
					num2
				})
			});
		case TimeUtils.ElapsedTimeType.HOURS:
			return GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME_RANGE", new object[]
			{
				num,
				GameStrings.Format(this.m_timeStringSet.m_hours, new object[]
				{
					num2
				})
			});
		case TimeUtils.ElapsedTimeType.YESTERDAY:
			return GameStrings.Get(this.m_timeStringSet.m_yesterday);
		case TimeUtils.ElapsedTimeType.DAYS:
			return GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME_RANGE", new object[]
			{
				num,
				GameStrings.Format(this.m_timeStringSet.m_days, new object[]
				{
					num2
				})
			});
		case TimeUtils.ElapsedTimeType.WEEKS:
			return GameStrings.Format(this.m_timeStringSet.m_weeks, new object[]
			{
				num,
				num2
			});
		default:
			return GameStrings.Get(this.m_timeStringSet.m_monthAgo);
		}
	}

	// Token: 0x04001C77 RID: 7287
	public GameObject m_root;

	// Token: 0x04001C78 RID: 7288
	public UberText m_waitTime;

	// Token: 0x04001C79 RID: 7289
	public UberText m_queueTime;

	// Token: 0x04001C7A RID: 7290
	private TimeUtils.ElapsedStringSet m_timeStringSet;

	// Token: 0x04001C7B RID: 7291
	private float m_timeInQueue;

	// Token: 0x04001C7C RID: 7292
	private const string TIME_RANGE_STRING = "GLOBAL_APPROXIMATE_DATETIME_RANGE";

	// Token: 0x04001C7D RID: 7293
	private const string TIME_STRING = "GLOBAL_APPROXIMATE_DATETIME";

	// Token: 0x04001C7E RID: 7294
	private const int SUPPRESS_TIME = 30;
}
