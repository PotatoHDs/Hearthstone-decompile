using System;
using System.Collections.Generic;

namespace Blizzard.BlizzardErrorMobile
{
	public class ExceptionSettings
	{
		public enum ReportType
		{
			EXCEPTION,
			BUG,
			ANR
		}

		public delegate string[] LogPathsHandler(ReportType type);

		public delegate string[] AttachableFilesHandler(ReportType type);

		public delegate Dictionary<string, string> AdditionalInfoHandler(ReportType type);

		public delegate byte[] ReadFileMethodHandler(string filePath);

		public const int DEFAULT_MAX_LOG_LINES = 100;

		public const int DEFAULT_EXCEPTIONBOARD_MAX_ZIP_SIZE = 2097152;

		public const int DEFAULT_JIRA_MAX_ZIP_SIZE = 7340032;

		public const int SYSTEM_MAX_ZIP_SIZE = 10485760;

		public const int UNDEFINED_ID = int.MaxValue;

		public int m_projectID = int.MaxValue;

		public int m_buildNumber = int.MaxValue;

		public string m_moduleName = "";

		public string m_branchName = "";

		public string m_version = "";

		public string m_locale = "";

		public string m_userUUID = "";

		public string m_jiraProjectName = "";

		public string m_jiraComponent = "";

		public string m_jiraVersion = "";

		[Obsolete("m_jiraZipSizeLimit is deprecated, please use m_maxZipSizeLimits instead.")]
		public int m_jiraZipSizeLimit = 7340032;

		[Obsolete("m_logLineLimit is deprecated, please use m_logLineLimits instead.")]
		public int m_logLineLimit = 100;

		[Obsolete("m_jiraZipSizeLimit is deprecated, please use m_maxZipSizeLimits instead.")]
		public int m_maxZipSizeLimit = 2097152;

		public Dictionary<ReportType, int> m_logLineLimits = new Dictionary<ReportType, int>
		{
			{
				ReportType.EXCEPTION,
				100
			},
			{
				ReportType.BUG,
				0
			},
			{
				ReportType.ANR,
				0
			}
		};

		public Dictionary<ReportType, int> m_maxZipSizeLimits = new Dictionary<ReportType, int>
		{
			{
				ReportType.EXCEPTION,
				2097152
			},
			{
				ReportType.BUG,
				7340032
			},
			{
				ReportType.ANR,
				2097152
			}
		};

		public Dictionary<ReportType, int> m_maxScreenshotWidths = new Dictionary<ReportType, int>
		{
			{
				ReportType.EXCEPTION,
				-1
			},
			{
				ReportType.BUG,
				-1
			},
			{
				ReportType.ANR,
				-1
			}
		};

		public LogPathsHandler m_logPathsCallback;

		public AttachableFilesHandler m_attachableFilesCallback;

		public AdditionalInfoHandler m_additionalInfoCallback;

		public ReadFileMethodHandler m_readFileMethodCallback;
	}
}
