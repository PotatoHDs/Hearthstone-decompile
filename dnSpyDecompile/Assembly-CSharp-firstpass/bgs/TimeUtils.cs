using System;
using System.Text;
using System.Text.RegularExpressions;

namespace bgs
{
	// Token: 0x0200025C RID: 604
	public class TimeUtils
	{
		// Token: 0x06002511 RID: 9489 RVA: 0x000834A0 File Offset: 0x000816A0
		public static long BinaryStamp()
		{
			return DateTime.UtcNow.ToBinary();
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000834BC File Offset: 0x000816BC
		public static DateTime ConvertEpochMicrosecToDateTime(long microsec)
		{
			return TimeUtils.EPOCH_TIME.AddMilliseconds((double)microsec / 1000.0);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000834E2 File Offset: 0x000816E2
		public static TimeSpan GetElapsedTimeSinceEpoch(DateTime? endDateTime = null)
		{
			return ((endDateTime != null) ? endDateTime.Value : DateTime.UtcNow) - TimeUtils.EPOCH_TIME;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00083508 File Offset: 0x00081708
		public static void GetElapsedTime(int seconds, out TimeUtils.ElapsedTimeType timeType, out int time)
		{
			time = 0;
			if (seconds < 60)
			{
				timeType = TimeUtils.ElapsedTimeType.SECONDS;
				time = seconds;
				return;
			}
			if (seconds < 3600)
			{
				timeType = TimeUtils.ElapsedTimeType.MINUTES;
				time = seconds / 60;
				return;
			}
			int num = seconds / 86400;
			if (num == 0)
			{
				timeType = TimeUtils.ElapsedTimeType.HOURS;
				time = seconds / 3600;
				return;
			}
			if (num == 1)
			{
				timeType = TimeUtils.ElapsedTimeType.YESTERDAY;
				return;
			}
			int num2 = seconds / 604800;
			if (num2 == 0)
			{
				timeType = TimeUtils.ElapsedTimeType.DAYS;
				time = num;
				return;
			}
			if (num2 < 4)
			{
				timeType = TimeUtils.ElapsedTimeType.WEEKS;
				time = num2;
				return;
			}
			timeType = TimeUtils.ElapsedTimeType.MONTH_AGO;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00083576 File Offset: 0x00081776
		public static string GetDevElapsedTimeString(TimeSpan span)
		{
			return TimeUtils.GetDevElapsedTimeString((long)span.TotalMilliseconds);
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x00083588 File Offset: 0x00081788
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
				TimeUtils.AppendDevTimeUnitsString("{0}ms", 1, stringBuilder, ref ms, ref num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x00083610 File Offset: 0x00081810
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

		// Token: 0x06002518 RID: 9496 RVA: 0x000836E0 File Offset: 0x000818E0
		public static float ForceDevSecFromElapsedTimeString(string timeStr)
		{
			float result;
			TimeUtils.TryParseDevSecFromElapsedTimeString(timeStr, out result);
			return result;
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000836F8 File Offset: 0x000818F8
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

		// Token: 0x0600251A RID: 9498 RVA: 0x00083740 File Offset: 0x00081940
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

		// Token: 0x04000F66 RID: 3942
		public const int SEC_PER_MINUTE = 60;

		// Token: 0x04000F67 RID: 3943
		public const int SEC_PER_HOUR = 3600;

		// Token: 0x04000F68 RID: 3944
		public const int SEC_PER_DAY = 86400;

		// Token: 0x04000F69 RID: 3945
		public const int SEC_PER_WEEK = 604800;

		// Token: 0x04000F6A RID: 3946
		public const int MS_PER_SEC = 1000;

		// Token: 0x04000F6B RID: 3947
		public const int MS_PER_MINUTE = 60000;

		// Token: 0x04000F6C RID: 3948
		public const int MS_PER_HOUR = 3600000;

		// Token: 0x04000F6D RID: 3949
		public static readonly DateTime EPOCH_TIME = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04000F6E RID: 3950
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

		// Token: 0x04000F6F RID: 3951
		public const string DEFAULT_TIME_UNITS_STR = "sec";

		// Token: 0x020006E2 RID: 1762
		public enum ElapsedTimeType
		{
			// Token: 0x0400226E RID: 8814
			SECONDS,
			// Token: 0x0400226F RID: 8815
			MINUTES,
			// Token: 0x04002270 RID: 8816
			HOURS,
			// Token: 0x04002271 RID: 8817
			YESTERDAY,
			// Token: 0x04002272 RID: 8818
			DAYS,
			// Token: 0x04002273 RID: 8819
			WEEKS,
			// Token: 0x04002274 RID: 8820
			MONTH_AGO
		}

		// Token: 0x020006E3 RID: 1763
		public class ElapsedStringSet
		{
			// Token: 0x04002275 RID: 8821
			public string m_seconds;

			// Token: 0x04002276 RID: 8822
			public string m_minutes;

			// Token: 0x04002277 RID: 8823
			public string m_hours;

			// Token: 0x04002278 RID: 8824
			public string m_yesterday;

			// Token: 0x04002279 RID: 8825
			public string m_days;

			// Token: 0x0400227A RID: 8826
			public string m_weeks;

			// Token: 0x0400227B RID: 8827
			public string m_monthAgo;
		}
	}
}
