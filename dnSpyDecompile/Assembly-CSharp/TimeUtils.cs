using System;
using System.Text;
using System.Text.RegularExpressions;
using PegasusShared;
using UnityEngine;

// Token: 0x020009F8 RID: 2552
public class TimeUtils
{
	// Token: 0x06008A20 RID: 35360 RVA: 0x002C41E4 File Offset: 0x002C23E4
	public static long BinaryStamp()
	{
		return DateTime.UtcNow.ToBinary();
	}

	// Token: 0x06008A21 RID: 35361 RVA: 0x002C4200 File Offset: 0x002C2400
	public static DateTime ConvertEpochMicrosecToDateTime(long microsec)
	{
		return TimeUtils.EPOCH_TIME.AddMilliseconds((double)microsec / 1000.0);
	}

	// Token: 0x06008A22 RID: 35362 RVA: 0x002C4226 File Offset: 0x002C2426
	public static TimeSpan GetElapsedTimeSinceEpoch(DateTime? endDateTime = null)
	{
		return ((endDateTime != null) ? endDateTime.Value : DateTime.UtcNow) - TimeUtils.EPOCH_TIME;
	}

	// Token: 0x170007C0 RID: 1984
	// (get) Token: 0x06008A23 RID: 35363 RVA: 0x002C424C File Offset: 0x002C244C
	public static long UnixTimestampMilliseconds
	{
		get
		{
			return (long)TimeUtils.GetElapsedTimeSinceEpoch(null).TotalMilliseconds;
		}
	}

	// Token: 0x170007C1 RID: 1985
	// (get) Token: 0x06008A24 RID: 35364 RVA: 0x002C4270 File Offset: 0x002C2470
	public static long UnixTimestampSeconds
	{
		get
		{
			return (long)TimeUtils.GetElapsedTimeSinceEpoch(null).TotalSeconds;
		}
	}

	// Token: 0x06008A25 RID: 35365 RVA: 0x002C4294 File Offset: 0x002C2494
	public static string GetElapsedTimeStringFromEpochMicrosec(long microsec, TimeUtils.ElapsedStringSet stringSet)
	{
		DateTime d = TimeUtils.ConvertEpochMicrosecToDateTime(microsec);
		return TimeUtils.GetElapsedTimeString((int)(DateTime.UtcNow - d).TotalSeconds, stringSet, false);
	}

	// Token: 0x06008A26 RID: 35366 RVA: 0x002C42C4 File Offset: 0x002C24C4
	public static ulong DateTimeToUnixTimeStamp(DateTime time)
	{
		return (ulong)(time.ToUniversalTime() - TimeUtils.EPOCH_TIME).TotalSeconds;
	}

	// Token: 0x06008A27 RID: 35367 RVA: 0x002C42EC File Offset: 0x002C24EC
	public static ulong DateTimeToUnixTimeStampMilliseconds(DateTime time)
	{
		return (ulong)(time.ToUniversalTime() - TimeUtils.EPOCH_TIME).TotalMilliseconds;
	}

	// Token: 0x06008A28 RID: 35368 RVA: 0x002C4314 File Offset: 0x002C2514
	public static DateTime UnixTimeStampToDateTimeUtc(long secondsSinceEpoch)
	{
		return TimeUtils.EPOCH_TIME.AddSeconds((double)secondsSinceEpoch);
	}

	// Token: 0x06008A29 RID: 35369 RVA: 0x002C4330 File Offset: 0x002C2530
	public static DateTime UnixTimeStampMillisecondsToDateTimeUtc(long millisecondsSinceEpoch)
	{
		return TimeUtils.EPOCH_TIME.AddMilliseconds((double)millisecondsSinceEpoch);
	}

	// Token: 0x06008A2A RID: 35370 RVA: 0x002C434C File Offset: 0x002C254C
	public static DateTime UnixTimeStampToDateTimeLocal(long secondsSinceEpoch)
	{
		return TimeUtils.EPOCH_TIME.AddSeconds((double)secondsSinceEpoch).ToLocalTime();
	}

	// Token: 0x06008A2B RID: 35371 RVA: 0x002C4370 File Offset: 0x002C2570
	public static string GetElapsedTimeString(long seconds, TimeUtils.ElapsedStringSet stringSet, bool roundUp = false)
	{
		TimeUtils.ElapsedTimeType timeType;
		long time;
		if (roundUp)
		{
			TimeUtils.GetElapsedTimeRoundedUp(seconds, out timeType, out time);
		}
		else
		{
			TimeUtils.GetElapsedTimeRoundedDown(seconds, out timeType, out time);
		}
		return TimeUtils.GetElapsedTimeString(timeType, time, stringSet);
	}

	// Token: 0x06008A2C RID: 35372 RVA: 0x002C439E File Offset: 0x002C259E
	public static string GetElapsedTimeString(int seconds, TimeUtils.ElapsedStringSet stringSet, bool roundUp = false)
	{
		return TimeUtils.GetElapsedTimeString((long)seconds, stringSet, roundUp);
	}

	// Token: 0x06008A2D RID: 35373 RVA: 0x002C43A9 File Offset: 0x002C25A9
	public static string GetElapsedTimeString(TimeUtils.ElapsedTimeType timeType, int time, TimeUtils.ElapsedStringSet stringSet)
	{
		return TimeUtils.GetElapsedTimeString(timeType, (long)time, stringSet);
	}

	// Token: 0x06008A2E RID: 35374 RVA: 0x002C43B4 File Offset: 0x002C25B4
	public static string GetElapsedTimeString(TimeUtils.ElapsedTimeType timeType, long time, TimeUtils.ElapsedStringSet stringSet)
	{
		switch (timeType)
		{
		case TimeUtils.ElapsedTimeType.SECONDS:
			if (stringSet.m_seconds != null)
			{
				return GameStrings.Format(stringSet.m_seconds, new object[]
				{
					time
				});
			}
			time = 1L;
			break;
		case TimeUtils.ElapsedTimeType.MINUTES:
			break;
		case TimeUtils.ElapsedTimeType.HOURS:
			goto IL_75;
		case TimeUtils.ElapsedTimeType.YESTERDAY:
			goto IL_9E;
		case TimeUtils.ElapsedTimeType.DAYS:
			goto IL_B8;
		case TimeUtils.ElapsedTimeType.WEEKS:
			goto IL_E1;
		default:
			goto IL_10A;
		}
		if (stringSet.m_minutes != null)
		{
			return GameStrings.Format(stringSet.m_minutes, new object[]
			{
				time
			});
		}
		time = 1L;
		IL_75:
		if (stringSet.m_hours != null)
		{
			return GameStrings.Format(stringSet.m_hours, new object[]
			{
				time
			});
		}
		time = 1L;
		IL_9E:
		if (stringSet.m_yesterday != null)
		{
			return GameStrings.Get(stringSet.m_yesterday);
		}
		time = 1L;
		IL_B8:
		if (stringSet.m_days != null)
		{
			return GameStrings.Format(stringSet.m_days, new object[]
			{
				time
			});
		}
		time = 1L;
		IL_E1:
		if (stringSet.m_weeks != null)
		{
			return GameStrings.Format(stringSet.m_weeks, new object[]
			{
				time
			});
		}
		time = 1L;
		IL_10A:
		return GameStrings.Format(stringSet.m_monthAgo, new object[]
		{
			time
		});
	}

	// Token: 0x06008A2F RID: 35375 RVA: 0x002C44E8 File Offset: 0x002C26E8
	public static void GetElapsedTime(long seconds, out TimeUtils.ElapsedTimeType timeType, out int time, bool roundUp = false)
	{
		long num;
		if (roundUp)
		{
			TimeUtils.GetElapsedTimeRoundedUp(seconds, out timeType, out num);
		}
		else
		{
			TimeUtils.GetElapsedTimeRoundedDown(seconds, out timeType, out num);
		}
		time = (int)num;
	}

	// Token: 0x06008A30 RID: 35376 RVA: 0x002C4510 File Offset: 0x002C2710
	private static void GetElapsedTimeRoundedDown(long seconds, out TimeUtils.ElapsedTimeType timeType, out long time)
	{
		time = 0L;
		if (seconds < 60L)
		{
			timeType = TimeUtils.ElapsedTimeType.SECONDS;
			time = seconds;
			return;
		}
		if (seconds < 3600L)
		{
			timeType = TimeUtils.ElapsedTimeType.MINUTES;
			time = seconds / 60L;
			return;
		}
		long num = seconds / 86400L;
		if (num == 0L)
		{
			timeType = TimeUtils.ElapsedTimeType.HOURS;
			time = seconds / 3600L;
			return;
		}
		if (num == 1L)
		{
			timeType = TimeUtils.ElapsedTimeType.YESTERDAY;
			return;
		}
		long num2 = seconds / 604800L;
		if (num2 == 0L)
		{
			timeType = TimeUtils.ElapsedTimeType.DAYS;
			time = num;
			return;
		}
		long num3 = seconds / 2592000L;
		if (num3 == 0L)
		{
			timeType = TimeUtils.ElapsedTimeType.WEEKS;
			time = num2;
			return;
		}
		timeType = TimeUtils.ElapsedTimeType.MONTH_AGO;
		time = num3;
	}

	// Token: 0x06008A31 RID: 35377 RVA: 0x002C4594 File Offset: 0x002C2794
	private static void GetElapsedTimeRoundedUp(long seconds, out TimeUtils.ElapsedTimeType timeType, out long time)
	{
		time = 0L;
		long num = seconds / 60L;
		long num2 = seconds / 3600L;
		long num3 = seconds / 86400L;
		long num4 = seconds / 604800L;
		long num5 = seconds / 2592000L;
		if (num5 > 0L)
		{
			timeType = TimeUtils.ElapsedTimeType.MONTH_AGO;
			time = num5 + 1L;
			return;
		}
		if (num4 > 0L)
		{
			timeType = TimeUtils.ElapsedTimeType.WEEKS;
			time = num4 + 1L;
			return;
		}
		if (num3 > 0L)
		{
			timeType = TimeUtils.ElapsedTimeType.DAYS;
			time = num3 + 1L;
			return;
		}
		if (num2 > 0L)
		{
			timeType = TimeUtils.ElapsedTimeType.HOURS;
			time = num2 + 1L;
			return;
		}
		if (num > 0L)
		{
			timeType = TimeUtils.ElapsedTimeType.MINUTES;
			time = num + 1L;
			return;
		}
		timeType = TimeUtils.ElapsedTimeType.SECONDS;
		time = seconds;
	}

	// Token: 0x06008A32 RID: 35378 RVA: 0x002C4623 File Offset: 0x002C2823
	public static string GetDevElapsedTimeString(TimeSpan span)
	{
		return TimeUtils.GetDevElapsedTimeString((long)span.TotalMilliseconds);
	}

	// Token: 0x06008A33 RID: 35379 RVA: 0x002C4634 File Offset: 0x002C2834
	public static string GetDevElapsedTimeString(long ms)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		if (ms >= 3600000L)
		{
			TimeUtils.AppendDevTimeUnitsString("{0}h", 3600000, stringBuilder, ref ms, ref num);
		}
		if (ms >= 60000L)
		{
			TimeUtils.AppendDevTimeUnitsString("{0}m", 60000, stringBuilder, ref ms, ref num);
		}
		if (ms >= 1000L)
		{
			TimeUtils.AppendDevTimeUnitsString("{0}s", 1000, stringBuilder, ref ms, ref num);
		}
		if (num <= 1)
		{
			if (num > 0)
			{
				stringBuilder.Append(' ');
			}
			stringBuilder.AppendFormat("{0}ms", ms);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06008A34 RID: 35380 RVA: 0x002C46CC File Offset: 0x002C28CC
	public static string GetDevElapsedTimeString(float sec)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		if (sec >= 3600f)
		{
			TimeUtils.AppendDevTimeUnitsString("{0}h", 3600f, stringBuilder, ref sec, ref num);
		}
		if (sec >= 60f)
		{
			TimeUtils.AppendDevTimeUnitsString("{0}m", 60f, stringBuilder, ref sec, ref num);
		}
		if (sec >= 1f)
		{
			TimeUtils.AppendDevTimeUnitsString("{0}s", 1f, stringBuilder, ref sec, ref num);
		}
		if (num <= 1)
		{
			if (num > 0)
			{
				stringBuilder.Append(' ');
			}
			float num2 = sec * 1000f;
			if (num2 > 0f)
			{
				stringBuilder.AppendFormat("{0:f0}ms", num2);
			}
			else
			{
				stringBuilder.AppendFormat("{0}ms", num2);
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06008A35 RID: 35381 RVA: 0x002C4784 File Offset: 0x002C2984
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
		string text = group2.Value;
		if (!float.TryParse(value, out sec))
		{
			return false;
		}
		text = TimeUtils.ParseTimeUnitsStr(text);
		if (text == "min")
		{
			sec *= 60f;
		}
		else if (text == "hour")
		{
			sec *= 3600f;
		}
		return true;
	}

	// Token: 0x06008A36 RID: 35382 RVA: 0x002C4854 File Offset: 0x002C2A54
	public static float ForceDevSecFromElapsedTimeString(string timeStr)
	{
		float result;
		TimeUtils.TryParseDevSecFromElapsedTimeString(timeStr, out result);
		return result;
	}

	// Token: 0x06008A37 RID: 35383 RVA: 0x002C486C File Offset: 0x002C2A6C
	public static long PegDateToFileTimeUtc(Date date)
	{
		return new DateTime(date.Year, date.Month, date.Day, date.Hours, date.Min, date.Sec).ToFileTimeUtc();
	}

	// Token: 0x06008A38 RID: 35384 RVA: 0x002C48AC File Offset: 0x002C2AAC
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

	// Token: 0x06008A39 RID: 35385 RVA: 0x002C4914 File Offset: 0x002C2B14
	private static void AppendDevTimeUnitsString(string formatString, int msPerUnit, StringBuilder builder, ref long ms, ref int unitCount)
	{
		long num = ms / (long)msPerUnit;
		if (num > 0L)
		{
			if (unitCount > 0)
			{
				builder.Append(' ');
			}
			builder.AppendFormat(formatString, num);
			unitCount++;
		}
		ms -= num * (long)msPerUnit;
	}

	// Token: 0x06008A3A RID: 35386 RVA: 0x002C495C File Offset: 0x002C2B5C
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

	// Token: 0x06008A3B RID: 35387 RVA: 0x002C49A8 File Offset: 0x002C2BA8
	private static string ParseTimeUnitsStr(string unitsStr)
	{
		if (unitsStr == null)
		{
			return "sec";
		}
		unitsStr = unitsStr.ToLowerInvariant();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(unitsStr);
		if (num <= 2885211357U)
		{
			if (num <= 1599397587U)
			{
				if (num != 50267956U)
				{
					if (num != 954666857U)
					{
						if (num != 1599397587U)
						{
							goto IL_1C8;
						}
						if (!(unitsStr == "secs"))
						{
							goto IL_1C8;
						}
					}
					else
					{
						if (!(unitsStr == "minute"))
						{
							goto IL_1C8;
						}
						goto IL_1BC;
					}
				}
				else
				{
					if (!(unitsStr == "hours"))
					{
						goto IL_1C8;
					}
					goto IL_1C2;
				}
			}
			else if (num != 1723256298U)
			{
				if (num != 1888081836U)
				{
					if (num != 2885211357U)
					{
						goto IL_1C8;
					}
					if (!(unitsStr == "second"))
					{
						goto IL_1C8;
					}
				}
				else
				{
					if (!(unitsStr == "mins"))
					{
						goto IL_1C8;
					}
					goto IL_1BC;
				}
			}
			else if (!(unitsStr == "seconds"))
			{
				goto IL_1C8;
			}
		}
		else if (num <= 3139892658U)
		{
			if (num != 2914829806U)
			{
				if (num != 3053661199U)
				{
					if (num != 3139892658U)
					{
						goto IL_1C8;
					}
					if (!(unitsStr == "sec"))
					{
						goto IL_1C8;
					}
				}
				else
				{
					if (!(unitsStr == "hour"))
					{
						goto IL_1C8;
					}
					goto IL_1C2;
				}
			}
			else
			{
				if (!(unitsStr == "minutes"))
				{
					goto IL_1C8;
				}
				goto IL_1BC;
			}
		}
		else if (num <= 3893112696U)
		{
			if (num != 3381609815U)
			{
				if (num != 3893112696U)
				{
					goto IL_1C8;
				}
				if (!(unitsStr == "m"))
				{
					goto IL_1C8;
				}
				goto IL_1BC;
			}
			else
			{
				if (!(unitsStr == "min"))
				{
					goto IL_1C8;
				}
				goto IL_1BC;
			}
		}
		else if (num != 3977000791U)
		{
			if (num != 4127999362U)
			{
				goto IL_1C8;
			}
			if (!(unitsStr == "s"))
			{
				goto IL_1C8;
			}
		}
		else
		{
			if (!(unitsStr == "h"))
			{
				goto IL_1C8;
			}
			goto IL_1C2;
		}
		return "sec";
		IL_1BC:
		return "min";
		IL_1C2:
		return "hour";
		IL_1C8:
		return "sec";
	}

	// Token: 0x0400735F RID: 29535
	public const int SEC_PER_MINUTE = 60;

	// Token: 0x04007360 RID: 29536
	public const int SEC_PER_HOUR = 3600;

	// Token: 0x04007361 RID: 29537
	public const int SEC_PER_DAY = 86400;

	// Token: 0x04007362 RID: 29538
	public const int SEC_PER_WEEK = 604800;

	// Token: 0x04007363 RID: 29539
	public const int SEC_PER_MONTH = 2592000;

	// Token: 0x04007364 RID: 29540
	public const int MS_PER_SEC = 1000;

	// Token: 0x04007365 RID: 29541
	public const int MS_PER_MINUTE = 60000;

	// Token: 0x04007366 RID: 29542
	public const int MS_PER_HOUR = 3600000;

	// Token: 0x04007367 RID: 29543
	public static readonly DateTime EPOCH_TIME = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	// Token: 0x04007368 RID: 29544
	public static readonly TimeUtils.ElapsedStringSet SPLASHSCREEN_DATETIME_STRINGSET = new TimeUtils.ElapsedStringSet
	{
		m_seconds = "GLOBAL_DATETIME_SPLASHSCREEN_SECONDS",
		m_minutes = "GLOBAL_DATETIME_SPLASHSCREEN_MINUTES",
		m_hours = "GLOBAL_DATETIME_SPLASHSCREEN_HOURS",
		m_yesterday = "GLOBAL_DATETIME_SPLASHSCREEN_DAY",
		m_days = "GLOBAL_DATETIME_SPLASHSCREEN_DAYS",
		m_weeks = "GLOBAL_DATETIME_SPLASHSCREEN_WEEKS",
		m_monthAgo = "GLOBAL_DATETIME_SPLASHSCREEN_MONTH"
	};

	// Token: 0x04007369 RID: 29545
	public const string DEFAULT_TIME_UNITS_STR = "sec";

	// Token: 0x02002685 RID: 9861
	public enum ElapsedTimeType
	{
		// Token: 0x0400F0DA RID: 61658
		SECONDS,
		// Token: 0x0400F0DB RID: 61659
		MINUTES,
		// Token: 0x0400F0DC RID: 61660
		HOURS,
		// Token: 0x0400F0DD RID: 61661
		YESTERDAY,
		// Token: 0x0400F0DE RID: 61662
		DAYS,
		// Token: 0x0400F0DF RID: 61663
		WEEKS,
		// Token: 0x0400F0E0 RID: 61664
		MONTH_AGO
	}

	// Token: 0x02002686 RID: 9862
	public class ElapsedStringSet
	{
		// Token: 0x0400F0E1 RID: 61665
		public string m_seconds;

		// Token: 0x0400F0E2 RID: 61666
		public string m_minutes;

		// Token: 0x0400F0E3 RID: 61667
		public string m_hours;

		// Token: 0x0400F0E4 RID: 61668
		public string m_yesterday;

		// Token: 0x0400F0E5 RID: 61669
		public string m_days;

		// Token: 0x0400F0E6 RID: 61670
		public string m_weeks;

		// Token: 0x0400F0E7 RID: 61671
		public string m_monthAgo;
	}
}
