public class PreviousInstanceStatus
{
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

	public static void ReportAppStatus()
	{
		if (ClosedWithoutCrash)
		{
			CrashInARow = 0;
		}
		else
		{
			CrashInARow++;
			if (CrashCount < int.MaxValue)
			{
				CrashCount++;
			}
		}
		if (!string.IsNullOrEmpty(LastExceptionHash) && LastExceptionHash != "None" && ExceptionHash == LastExceptionHash)
		{
			SameExceptionCount++;
		}
		else
		{
			if (!string.IsNullOrEmpty(ExceptionHash) && ExceptionHash != "None")
			{
				LastExceptionHash = ExceptionHash;
			}
			SameExceptionCount = 0;
		}
		if (CrashCount > 0 || ExceptionCount > 0)
		{
			Log.All.PrintDebug("Report PreviousInstanceStatus - Crash {0}, Exception {1}", CrashCount, ExceptionCount);
			TelemetryManager.Client().SendPreviousInstanceStatus(CrashCount, ExceptionCount, LowMemoryCount, CrashInARow, SameExceptionCount, !ClosedWithoutCrash, ExceptionHash);
		}
		ClosedWithoutCrash = false;
		LowMemoryCount = 0;
		ExceptionHash = "None";
	}

	public void ClearStatusForNewVersion()
	{
		if (!UpdatedClientVersion.Equals("20.4"))
		{
			UpdatedClientVersion = "20.4";
			CrashCount = 0;
			ExceptionCount = 0;
			LowMemoryCount = 0;
			CrashInARow = 0;
			SameExceptionCount = 0;
			ExceptionHash = string.Empty;
			LastExceptionHash = string.Empty;
		}
	}
}
