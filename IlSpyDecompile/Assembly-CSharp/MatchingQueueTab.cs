using UnityEngine;

public class MatchingQueueTab : MonoBehaviour
{
	public GameObject m_root;

	public UberText m_waitTime;

	public UberText m_queueTime;

	private TimeUtils.ElapsedStringSet m_timeStringSet;

	private float m_timeInQueue;

	private const string TIME_RANGE_STRING = "GLOBAL_APPROXIMATE_DATETIME_RANGE";

	private const string TIME_STRING = "GLOBAL_APPROXIMATE_DATETIME";

	private const int SUPPRESS_TIME = 30;

	private void Update()
	{
		InitTimeStringSet();
		m_timeInQueue += Time.deltaTime;
		m_waitTime.Text = TimeUtils.GetElapsedTimeString(Mathf.RoundToInt(m_timeInQueue), m_timeStringSet);
	}

	public void Show()
	{
		m_root.SetActive(value: true);
	}

	public void Hide()
	{
		m_root.SetActive(value: false);
	}

	public void ResetTimer()
	{
		m_timeInQueue = 0f;
		UpdateDisplay(0, 0);
	}

	public void UpdateDisplay(int minSeconds, int maxSeconds)
	{
		InitTimeStringSet();
		int num = Mathf.RoundToInt(m_timeInQueue);
		maxSeconds += num;
		if (maxSeconds <= 30)
		{
			Hide();
			return;
		}
		m_queueTime.Text = GetElapsedTimeString(minSeconds + num, maxSeconds);
		Show();
	}

	private void InitTimeStringSet()
	{
		if (m_timeStringSet == null)
		{
			m_timeStringSet = new TimeUtils.ElapsedStringSet
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

	private string GetElapsedTimeString(int minSeconds, int maxSeconds)
	{
		TimeUtils.GetElapsedTime(minSeconds, out var timeType, out var time);
		if (minSeconds == maxSeconds)
		{
			return GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME", TimeUtils.GetElapsedTimeString(minSeconds, m_timeStringSet));
		}
		TimeUtils.GetElapsedTime(maxSeconds, out var timeType2, out var time2);
		if (timeType == timeType2)
		{
			return timeType switch
			{
				TimeUtils.ElapsedTimeType.SECONDS => GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME_RANGE", time, GameStrings.Format(m_timeStringSet.m_seconds, time2)), 
				TimeUtils.ElapsedTimeType.MINUTES => GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME_RANGE", time, GameStrings.Format(m_timeStringSet.m_minutes, time2)), 
				TimeUtils.ElapsedTimeType.HOURS => GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME_RANGE", time, GameStrings.Format(m_timeStringSet.m_hours, time2)), 
				TimeUtils.ElapsedTimeType.YESTERDAY => GameStrings.Get(m_timeStringSet.m_yesterday), 
				TimeUtils.ElapsedTimeType.DAYS => GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME_RANGE", time, GameStrings.Format(m_timeStringSet.m_days, time2)), 
				TimeUtils.ElapsedTimeType.WEEKS => GameStrings.Format(m_timeStringSet.m_weeks, time, time2), 
				_ => GameStrings.Get(m_timeStringSet.m_monthAgo), 
			};
		}
		string elapsedTimeString = TimeUtils.GetElapsedTimeString(timeType, time, m_timeStringSet);
		string elapsedTimeString2 = TimeUtils.GetElapsedTimeString(timeType2, time2, m_timeStringSet);
		return GameStrings.Format("GLOBAL_APPROXIMATE_DATETIME_RANGE", elapsedTimeString, elapsedTimeString2);
	}
}
