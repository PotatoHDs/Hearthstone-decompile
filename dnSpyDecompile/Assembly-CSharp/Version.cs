using System;

// Token: 0x0200093C RID: 2364
public abstract class Version
{
	// Token: 0x060082BF RID: 33471 RVA: 0x002A7665 File Offset: 0x002A5865
	public static void Reset()
	{
		global::Version.report_ = string.Empty;
	}

	// Token: 0x1700076F RID: 1903
	// (get) Token: 0x060082C0 RID: 33472 RVA: 0x002A7671 File Offset: 0x002A5871
	public static string FullReport
	{
		get
		{
			if (string.IsNullOrEmpty(global::Version.report_))
			{
				global::Version.createReport();
			}
			return global::Version.report_;
		}
	}

	// Token: 0x060082C1 RID: 33473 RVA: 0x002A768C File Offset: 0x002A588C
	private static void createReport()
	{
		global::Version.report_ = string.Format("Version {0} (client {1}{2}{3})", new object[]
		{
			84593,
			2134675,
			global::Version.serverChangelist,
			global::Version.bobNetAddress
		});
	}

	// Token: 0x17000770 RID: 1904
	// (get) Token: 0x060082C2 RID: 33474 RVA: 0x002A76D8 File Offset: 0x002A58D8
	// (set) Token: 0x060082C3 RID: 33475 RVA: 0x002A76E8 File Offset: 0x002A58E8
	public static string serverChangelist
	{
		get
		{
			return global::Version.serverChangelist_ ?? string.Empty;
		}
		set
		{
			global::Version.serverChangelist_ = string.Format(", server {0}", value);
			global::Version.Reset();
		}
	}

	// Token: 0x17000771 RID: 1905
	// (get) Token: 0x060082C4 RID: 33476 RVA: 0x002A76FF File Offset: 0x002A58FF
	// (set) Token: 0x060082C5 RID: 33477 RVA: 0x002A770F File Offset: 0x002A590F
	public static string bobNetAddress
	{
		get
		{
			return global::Version.bobNetAddress_ ?? string.Empty;
		}
		set
		{
			global::Version.bobNetAddress_ = string.Format(", Battle.net {0}", value);
			global::Version.Reset();
		}
	}

	// Token: 0x04006D82 RID: 28034
	public const int version = 84593;

	// Token: 0x04006D83 RID: 28035
	public const int clientChangelist = 2134675;

	// Token: 0x04006D84 RID: 28036
	public const string androidTextureCompression = "";

	// Token: 0x04006D85 RID: 28037
	public const int dataVersion = 20400;

	// Token: 0x04006D86 RID: 28038
	public const string cosmeticVersion = "20.4";

	// Token: 0x04006D87 RID: 28039
	private static int clientChangelist_;

	// Token: 0x04006D88 RID: 28040
	private static string serverChangelist_;

	// Token: 0x04006D89 RID: 28041
	private static string bobNetAddress_;

	// Token: 0x04006D8A RID: 28042
	private static string report_;
}
