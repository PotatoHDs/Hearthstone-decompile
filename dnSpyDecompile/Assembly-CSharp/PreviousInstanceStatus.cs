using System;

// Token: 0x020008FF RID: 2303
public class PreviousInstanceStatus
{
	// Token: 0x1700074E RID: 1870
	// (get) Token: 0x06008029 RID: 32809 RVA: 0x0029AF28 File Offset: 0x00299128
	// (set) Token: 0x0600802A RID: 32810 RVA: 0x0029AF36 File Offset: 0x00299136
	public static bool ClosedWithoutCrash
	{
		get
		{
			return Options.Get().GetBool(Option.CLOSED_WITHOUT_CRASH);
		}
		set
		{
			Options.Get().SetBool(Option.CLOSED_WITHOUT_CRASH, value);
		}
	}

	// Token: 0x1700074F RID: 1871
	// (get) Token: 0x0600802B RID: 32811 RVA: 0x0029AF45 File Offset: 0x00299145
	// (set) Token: 0x0600802C RID: 32812 RVA: 0x0029AF53 File Offset: 0x00299153
	public static int CrashCount
	{
		get
		{
			return Options.Get().GetInt(Option.CRASH_COUNT);
		}
		set
		{
			Options.Get().SetInt(Option.CRASH_COUNT, value);
		}
	}

	// Token: 0x17000750 RID: 1872
	// (get) Token: 0x0600802D RID: 32813 RVA: 0x0029AF62 File Offset: 0x00299162
	// (set) Token: 0x0600802E RID: 32814 RVA: 0x0029AF70 File Offset: 0x00299170
	public static int ExceptionCount
	{
		get
		{
			return Options.Get().GetInt(Option.EXCEPTION_COUNT);
		}
		set
		{
			Options.Get().SetInt(Option.EXCEPTION_COUNT, value);
		}
	}

	// Token: 0x17000751 RID: 1873
	// (get) Token: 0x0600802F RID: 32815 RVA: 0x0029AF7F File Offset: 0x0029917F
	// (set) Token: 0x06008030 RID: 32816 RVA: 0x0029AF8D File Offset: 0x0029918D
	public static int LowMemoryCount
	{
		get
		{
			return Options.Get().GetInt(Option.LOW_MEMORY_COUNT);
		}
		set
		{
			Options.Get().SetInt(Option.LOW_MEMORY_COUNT, value);
		}
	}

	// Token: 0x17000752 RID: 1874
	// (get) Token: 0x06008031 RID: 32817 RVA: 0x0029AF9C File Offset: 0x0029919C
	// (set) Token: 0x06008032 RID: 32818 RVA: 0x0029AFAA File Offset: 0x002991AA
	public static int CrashInARow
	{
		get
		{
			return Options.Get().GetInt(Option.CRASH_IN_A_ROW_COUNT);
		}
		set
		{
			Options.Get().SetInt(Option.CRASH_IN_A_ROW_COUNT, value);
		}
	}

	// Token: 0x17000753 RID: 1875
	// (get) Token: 0x06008033 RID: 32819 RVA: 0x0029AFB9 File Offset: 0x002991B9
	// (set) Token: 0x06008034 RID: 32820 RVA: 0x0029AFC7 File Offset: 0x002991C7
	public static int SameExceptionCount
	{
		get
		{
			return Options.Get().GetInt(Option.SAME_EXCEPTION_COUNT);
		}
		set
		{
			Options.Get().SetInt(Option.SAME_EXCEPTION_COUNT, value);
		}
	}

	// Token: 0x17000754 RID: 1876
	// (get) Token: 0x06008035 RID: 32821 RVA: 0x0029AFD6 File Offset: 0x002991D6
	// (set) Token: 0x06008036 RID: 32822 RVA: 0x0029AFE4 File Offset: 0x002991E4
	public static string ExceptionHash
	{
		get
		{
			return Options.Get().GetString(Option.EXCEPTION_HASH);
		}
		set
		{
			Options.Get().SetString(Option.EXCEPTION_HASH, value);
		}
	}

	// Token: 0x17000755 RID: 1877
	// (get) Token: 0x06008037 RID: 32823 RVA: 0x0029AFF3 File Offset: 0x002991F3
	// (set) Token: 0x06008038 RID: 32824 RVA: 0x0029B001 File Offset: 0x00299201
	public static string LastExceptionHash
	{
		get
		{
			return Options.Get().GetString(Option.LAST_EXCEPTION_HASH);
		}
		set
		{
			Options.Get().SetString(Option.LAST_EXCEPTION_HASH, value);
		}
	}

	// Token: 0x17000756 RID: 1878
	// (get) Token: 0x06008039 RID: 32825 RVA: 0x0029B010 File Offset: 0x00299210
	// (set) Token: 0x0600803A RID: 32826 RVA: 0x0029B01E File Offset: 0x0029921E
	public static string UpdatedClientVersion
	{
		get
		{
			return Options.Get().GetString(Option.UPDATED_CLIENT_VERSION);
		}
		set
		{
			Options.Get().SetString(Option.UPDATED_CLIENT_VERSION, value);
		}
	}

	// Token: 0x0600803B RID: 32827 RVA: 0x0029B030 File Offset: 0x00299230
	public static void ReportAppStatus()
	{
		if (PreviousInstanceStatus.ClosedWithoutCrash)
		{
			PreviousInstanceStatus.CrashInARow = 0;
		}
		else
		{
			PreviousInstanceStatus.CrashInARow++;
			if (PreviousInstanceStatus.CrashCount < 2147483647)
			{
				PreviousInstanceStatus.CrashCount++;
			}
		}
		if (!string.IsNullOrEmpty(PreviousInstanceStatus.LastExceptionHash) && PreviousInstanceStatus.LastExceptionHash != "None" && PreviousInstanceStatus.ExceptionHash == PreviousInstanceStatus.LastExceptionHash)
		{
			PreviousInstanceStatus.SameExceptionCount++;
		}
		else
		{
			if (!string.IsNullOrEmpty(PreviousInstanceStatus.ExceptionHash) && PreviousInstanceStatus.ExceptionHash != "None")
			{
				PreviousInstanceStatus.LastExceptionHash = PreviousInstanceStatus.ExceptionHash;
			}
			PreviousInstanceStatus.SameExceptionCount = 0;
		}
		if (PreviousInstanceStatus.CrashCount > 0 || PreviousInstanceStatus.ExceptionCount > 0)
		{
			Log.All.PrintDebug("Report PreviousInstanceStatus - Crash {0}, Exception {1}", new object[]
			{
				PreviousInstanceStatus.CrashCount,
				PreviousInstanceStatus.ExceptionCount
			});
			TelemetryManager.Client().SendPreviousInstanceStatus(PreviousInstanceStatus.CrashCount, PreviousInstanceStatus.ExceptionCount, PreviousInstanceStatus.LowMemoryCount, PreviousInstanceStatus.CrashInARow, PreviousInstanceStatus.SameExceptionCount, !PreviousInstanceStatus.ClosedWithoutCrash, PreviousInstanceStatus.ExceptionHash);
		}
		PreviousInstanceStatus.ClosedWithoutCrash = false;
		PreviousInstanceStatus.LowMemoryCount = 0;
		PreviousInstanceStatus.ExceptionHash = "None";
	}

	// Token: 0x0600803C RID: 32828 RVA: 0x0029B160 File Offset: 0x00299360
	public void ClearStatusForNewVersion()
	{
		if (PreviousInstanceStatus.UpdatedClientVersion.Equals("20.4"))
		{
			return;
		}
		PreviousInstanceStatus.UpdatedClientVersion = "20.4";
		PreviousInstanceStatus.CrashCount = 0;
		PreviousInstanceStatus.ExceptionCount = 0;
		PreviousInstanceStatus.LowMemoryCount = 0;
		PreviousInstanceStatus.CrashInARow = 0;
		PreviousInstanceStatus.SameExceptionCount = 0;
		PreviousInstanceStatus.ExceptionHash = string.Empty;
		PreviousInstanceStatus.LastExceptionHash = string.Empty;
	}
}
