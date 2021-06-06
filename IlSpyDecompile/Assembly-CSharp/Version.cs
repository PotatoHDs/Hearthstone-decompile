public abstract class Version
{
	public const int version = 84593;

	public const int clientChangelist = 2134675;

	public const string androidTextureCompression = "";

	public const int dataVersion = 20400;

	public const string cosmeticVersion = "20.4";

	private static int clientChangelist_;

	private static string serverChangelist_;

	private static string bobNetAddress_;

	private static string report_;

	public static string FullReport
	{
		get
		{
			if (string.IsNullOrEmpty(report_))
			{
				createReport();
			}
			return report_;
		}
	}

	public static string serverChangelist
	{
		get
		{
			return serverChangelist_ ?? string.Empty;
		}
		set
		{
			serverChangelist_ = $", server {value}";
			Reset();
		}
	}

	public static string bobNetAddress
	{
		get
		{
			return bobNetAddress_ ?? string.Empty;
		}
		set
		{
			bobNetAddress_ = $", Battle.net {value}";
			Reset();
		}
	}

	public static void Reset()
	{
		report_ = string.Empty;
	}

	private static void createReport()
	{
		report_ = $"Version {84593} (client {2134675}{serverChangelist}{bobNetAddress})";
	}
}
