using System;
using System.Text;
using System.Text.RegularExpressions;
using PegasusShared;
using UnityEngine;

public class TimeUtils
{
	public enum ElapsedTimeType
	{
		SECONDS,
		MINUTES,
		HOURS,
		YESTERDAY,
		DAYS,
		WEEKS,
		MONTH_AGO
	}

	public class ElapsedStringSet
	{
		public string m_seconds;

		public string m_minutes;

		public string m_hours;

		public string m_yesterday;

		public string m_days;

		public string m_weeks;

		public string m_monthAgo;
	}

	public const int SEC_PER_MINUTE = 60;

	public const int SEC_PER_HOUR = 3600;

	public const int SEC_PER_DAY = 86400;

	public const int SEC_PER_WEEK = 604800;

	public const int SEC_PER_MONTH = 2592000;

	public const int MS_PER_SEC = 1000;

	public const int MS_PER_MINUTE = 60000;

	public const int MS_PER_HOUR = 3600000;

	public static readonly DateTime EPOCH_TIME = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	public static readonly ElapsedStringSet SPLASHSCREEN_DATETIME_STRINGSET = new ElapsedStringSet
	{
		m_seconds = "GLOBAL_DATETIME_SPLASHSCREEN_SECONDS",
		m_minutes = "GLOBAL_DATETIME_SPLASHSCREEN_MINUTES",
		m_hours = "GLOBAL_DATETIME_SPLASHSCREEN_HOURS",
		m_yesterday = "GLOBAL_DATETIME_SPLASHSCREEN_DAY",
		m_days = "GLOBAL_DATETIME_SPLASHSCREEN_DAYS",
		m_weeks = "GLOBAL_DATETIME_SPLASHSCREEN_WEEKS",
		m_monthAgo = "GLOBAL_DATETIME_SPLASHSCREEN_MONTH"
	};

	public const string DEFAULT_TIME_UNITS_STR = "sec";

	public static long UnixTimestampMilliseconds => (long)GetElapsedTimeSinceEpoch().TotalMilliseconds;

	public static long UnixTimestampSeconds => (long)GetElapsedTimeSinceEpoch().TotalSeconds;

	public static long BinaryStamp()
	{
		return DateTime.UtcNow.ToBinary();
	}

	public static DateTime ConvertEpochMicrosecToDateTime(long microsec)
	{
		return EPOCH_TIME.AddMilliseconds((double)microsec / 1000.0);
	}

	public static TimeSpan GetElapsedTimeSinceEpoch(DateTime? endDateTime = null)
	{
		return (endDateTime.HasValue ? endDateTime.Value : DateTime.UtcNow) - EPOCH_TIME;
	}

	public static string GetElapsedTimeStringFromEpochMicrosec(long microsec, ElapsedStringSet stringSet)
	{
		DateTime dateTime = ConvertEpochMicrosecToDateTime(microsec);
		return GetElapsedTimeString((int)(DateTime.UtcNow - dateTime).TotalSeconds, stringSet);
	}

	public static ulong DateTimeToUnixTimeStamp(DateTime time)
	{
		return (ulong)(time.ToUniversalTime() - EPOCH_TIME).TotalSeconds;
	}

	public static ulong DateTimeToUnixTimeStampMilliseconds(DateTime time)
	{
		return (ulong)(time.ToUniversalTime() - EPOCH_TIME).TotalMilliseconds;
	}

	public static DateTime UnixTimeStampToDateTimeUtc(long secondsSinceEpoch)
	{
		return EPOCH_TIME.AddSeconds(secondsSinceEpoch);
	}

	public static DateTime UnixTimeStampMillisecondsToDateTimeUtc(long millisecondsSinceEpoch)
	{
		return EPOCH_TIME.AddMilliseconds(millisecondsSinceEpoch);
	}

	public static DateTime UnixTimeStampToDateTimeLocal(long secondsSinceEpoch)
	{
		return EPOCH_TIME.AddSeconds(secondsSinceEpoch).ToLocalTime();
	}

	public static string GetElapsedTimeString(long seconds, ElapsedStringSet stringSet, bool roundUp = false)
	{
		ElapsedTimeType timeType;
		long time;
		if (roundUp)
		{
			GetElapsedTimeRoundedUp(seconds, out timeType, out time);
		}
		else
		{
			GetElapsedTimeRoundedDown(seconds, out timeType, out time);
		}
		return GetElapsedTimeString(timeType, time, stringSet);
	}

	public static string GetElapsedTimeString(int seconds, ElapsedStringSet stringSet, bool roundUp = false)
	{
		return GetElapsedTimeString((long)seconds, stringSet, roundUp);
	}

	public static string GetElapsedTimeString(ElapsedTimeType timeType, int time, ElapsedStringSet stringSet)
	{
		return GetElapsedTimeString(timeType, (long)time, stringSet);
	}

	public static string GetElapsedTimeString(ElapsedTimeType timeType, long time, ElapsedStringSet stringSet)
	{
		switch (timeType)
		{
		case ElapsedTimeType.SECONDS:
			if (stringSet.m_seconds == null)
			{
				time = 1L;
				goto case ElapsedTimeType.MINUTES;
			}
			return GameStrings.Format(stringSet.m_seconds, time);
		case ElapsedTimeType.MINUTES:
			if (stringSet.m_minutes == null)
			{
				time = 1L;
				goto case ElapsedTimeType.HOURS;
			}
			return GameStrings.Format(stringSet.m_minutes, time);
		case ElapsedTimeType.HOURS:
			if (stringSet.m_hours == null)
			{
				time = 1L;
				goto case ElapsedTimeType.YESTERDAY;
			}
			return GameStrings.Format(stringSet.m_hours, time);
		case ElapsedTimeType.YESTERDAY:
			if (stringSet.m_yesterday == null)
			{
				time = 1L;
				goto case ElapsedTimeType.DAYS;
			}
			return GameStrings.Get(stringSet.m_yesterday);
		case ElapsedTimeType.DAYS:
			if (stringSet.m_days == null)
			{
				time = 1L;
				goto case ElapsedTimeType.WEEKS;
			}
			return GameStrings.Format(stringSet.m_days, time);
		case ElapsedTimeType.WEEKS:
			if (stringSet.m_weeks == null)
			{
				time = 1L;
				break;
			}
			return GameStrings.Format(stringSet.m_weeks, time);
		}
		return GameStrings.Format(stringSet.m_monthAgo, time);
	}

	public static void GetElapsedTime(long seconds, out ElapsedTimeType timeType, out int time, bool roundUp = false)
	{
		long time2;
		if (roundUp)
		{
			GetElapsedTimeRoundedUp(seconds, out timeType, out time2);
		}
		else
		{
			GetElapsedTimeRoundedDown(seconds, out timeType, out time2);
		}
		time = (int)time2;
	}

	private static void GetElapsedTimeRoundedDown(long seconds, out ElapsedTimeType timeType, out long time)
	{
		time = 0L;
		if (seconds < 60)
		{
			timeType = ElapsedTimeType.SECONDS;
			time = seconds;
			return;
		}
		if (seconds < 3600)
		{
			timeType = ElapsedTimeType.MINUTES;
			time = seconds / 60;
			return;
		}
		long num = seconds / 86400;
		switch (num)
		{
		case 0L:
			timeType = ElapsedTimeType.HOURS;
			time = seconds / 3600;
			return;
		case 1L:
			timeType = ElapsedTimeType.YESTERDAY;
			return;
		}
		long num2 = seconds / 604800;
		if (num2 == 0L)
		{
			timeType = ElapsedTimeType.DAYS;
			time = num;
			return;
		}
		long num3 = seconds / 2592000;
		if (num3 == 0L)
		{
			timeType = ElapsedTimeType.WEEKS;
			time = num2;
		}
		else
		{
			timeType = ElapsedTimeType.MONTH_AGO;
			time = num3;
		}
	}

	private static void GetElapsedTimeRoundedUp(long seconds, out ElapsedTimeType timeType, out long time)
	{
		time = 0L;
		long num = seconds / 60;
		long num2 = seconds / 3600;
		long num3 = seconds / 86400;
		long num4 = seconds / 604800;
		long num5 = seconds / 2592000;
		if (num5 > 0)
		{
			timeType = ElapsedTimeType.MONTH_AGO;
			time = num5 + 1;
		}
		else if (num4 > 0)
		{
			timeType = ElapsedTimeType.WEEKS;
			time = num4 + 1;
		}
		else if (num3 > 0)
		{
			timeType = ElapsedTimeType.DAYS;
			time = num3 + 1;
		}
		else if (num2 > 0)
		{
			timeType = ElapsedTimeType.HOURS;
			time = num2 + 1;
		}
		else if (num > 0)
		{
			timeType = ElapsedTimeType.MINUTES;
			time = num + 1;
		}
		else
		{
			timeType = ElapsedTimeType.SECONDS;
			time = seconds;
		}
	}

	public static string GetDevElapsedTimeString(TimeSpan span)
	{
		return GetDevElapsedTimeString((long)span.TotalMilliseconds);
	}

	public static string GetDevElapsedTimeString(long ms)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int unitCount = 0;
		if (ms >= 3600000)
		{
			AppendDevTimeUnitsString("{0}h", 3600000, stringBuilder, ref ms, ref unitCount);
		}
		if (ms >= 60000)
		{
			AppendDevTimeUnitsString("{0}m", 60000, stringBuilder, ref ms, ref unitCount);
		}
		if (ms >= 1000)
		{
			AppendDevTimeUnitsString("{0}s", 1000, stringBuilder, ref ms, ref unitCount);
		}
		if (unitCount <= 1)
		{
			if (unitCount > 0)
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.AppendFormat("{0}ms", ms);
		}
		return stringBuilder.ToString();
	}

	public static string GetDevElapsedTimeString(float sec)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int unitCount = 0;
		if (sec >= 3600f)
		{
			AppendDevTimeUnitsString("{0}h", 3600f, stringBuilder, ref sec, ref unitCount);
		}
		if (sec >= 60f)
		{
			AppendDevTimeUnitsString("{0}m", 60f, stringBuilder, ref sec, ref unitCount);
		}
		if (sec >= 1f)
		{
			AppendDevTimeUnitsString("{0}s", 1f, stringBuilder, ref sec, ref unitCount);
		}
		if (unitCount <= 1)
		{
			if (unitCount > 0)
			{
				stringBuilder.Append(' ');
			}
			float num = sec * 1000f;
			if (num > 0f)
			{
				stringBuilder.AppendFormat("{0:f0}ms", num);
			}
			else
			{
				stringBuilder.AppendFormat("{0}ms", num);
			}
		}
		return stringBuilder.ToString();
	}

	public static bool TryParseDevSecFromElapsedTimeString(string timeStr, out float sec)
	{
		sec = 0f;
		MatchCollection matchCollection = Regex.Matches(timeStr, "(?<number>(?:[0-9]+,)*[0-9]+)\\s*(?<units>[a-zA-Z]+)");
		if (matchCollection.Count == 0)
		{
			return false;
		}
		Match match = matchCollection[0];
		if (!match.Groups[0].Success)
		{
			return false;
		}
		Group group = match.Groups["number"];
		Group group2 = match.Groups["units"];
		if (!group.Success || !group2.Success)
		{
			return false;
		}
		string value = group.Value;
		string value2 = group2.Value;
		if (!float.TryParse(value, out sec))
		{
			return false;
		}
		value2 = ParseTimeUnitsStr(value2);
		if (value2 == "min")
		{
			sec *= 60f;
		}
		else if (value2 == "hour")
		{
			sec *= 3600f;
		}
		return true;
	}

	public static float ForceDevSecFromElapsedTimeString(string timeStr)
	{
		TryParseDevSecFromElapsedTimeString(timeStr, out var sec);
		return sec;
	}

	public static long PegDateToFileTimeUtc(Date date)
	{
		return new DateTime(date.Year, date.Month, date.Day, date.Hours, date.Min, date.Sec).ToFileTimeUtc();
	}

	public static Date FileTimeUtcToPegDate(long fileTimeUtc)
	{
		DateTime dateTime = DateTime.FromFileTimeUtc(fileTimeUtc);
		return new Date
		{
			Year = dateTime.Year,
			Month = dateTime.Month,
			Day = dateTime.Day,
			Hours = dateTime.Hour,
			Min = dateTime.Minute,
			Sec = dateTime.Second
		};
	}

	private static void AppendDevTimeUnitsString(string formatString, int msPerUnit, StringBuilder builder, ref long ms, ref int unitCount)
	{
		long num = ms / msPerUnit;
		if (num > 0)
		{
			if (unitCount > 0)
			{
				builder.Append(' ');
			}
			builder.AppendFormat(formatString, num);
			unitCount++;
		}
		ms -= num * msPerUnit;
	}

	private static void AppendDevTimeUnitsString(string formatString, float secPerUnit, StringBuilder builder, ref float sec, ref int unitCount)
	{
		float num = Mathf.Floor(sec / secPerUnit);
		if (num > 0f)
		{
			if (unitCount > 0)
			{
				builder.Append(' ');
			}
			builder.AppendFormat(formatString, num);
			unitCount++;
		}
		sec -= num * secPerUnit;
	}

	private static string ParseTimeUnitsStr(string unitsStr)
	{
		if (unitsStr == null)
		{
			return "sec";
		}
		unitsStr = unitsStr.ToLowerInvariant();
		switch (unitsStr)
		{
		case "s":
		case "sec":
		case "secs":
		case "second":
		case "seconds":
			return "sec";
		case "m":
		case "min":
		case "mins":
		case "minute":
		case "minutes":
			return "min";
		case "h":
		case "hour":
		case "hours":
			return "hour";
		default:
			return "sec";
		}
	}
}
