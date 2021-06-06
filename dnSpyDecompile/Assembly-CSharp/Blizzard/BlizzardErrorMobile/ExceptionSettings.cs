using System;
using System.Collections.Generic;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x02001213 RID: 4627
	public class ExceptionSettings
	{
		// Token: 0x0400A212 RID: 41490
		public const int DEFAULT_MAX_LOG_LINES = 100;

		// Token: 0x0400A213 RID: 41491
		public const int DEFAULT_EXCEPTIONBOARD_MAX_ZIP_SIZE = 2097152;

		// Token: 0x0400A214 RID: 41492
		public const int DEFAULT_JIRA_MAX_ZIP_SIZE = 7340032;

		// Token: 0x0400A215 RID: 41493
		public const int SYSTEM_MAX_ZIP_SIZE = 10485760;

		// Token: 0x0400A216 RID: 41494
		public const int UNDEFINED_ID = 2147483647;

		// Token: 0x0400A217 RID: 41495
		public int m_projectID = int.MaxValue;

		// Token: 0x0400A218 RID: 41496
		public int m_buildNumber = int.MaxValue;

		// Token: 0x0400A219 RID: 41497
		public string m_moduleName = "";

		// Token: 0x0400A21A RID: 41498
		public string m_branchName = "";

		// Token: 0x0400A21B RID: 41499
		public string m_version = "";

		// Token: 0x0400A21C RID: 41500
		public string m_locale = "";

		// Token: 0x0400A21D RID: 41501
		public string m_userUUID = "";

		// Token: 0x0400A21E RID: 41502
		public string m_jiraProjectName = "";

		// Token: 0x0400A21F RID: 41503
		public string m_jiraComponent = "";

		// Token: 0x0400A220 RID: 41504
		public string m_jiraVersion = "";

		// Token: 0x0400A221 RID: 41505
		[Obsolete("m_jiraZipSizeLimit is deprecated, please use m_maxZipSizeLimits instead.")]
		public int m_jiraZipSizeLimit = 7340032;

		// Token: 0x0400A222 RID: 41506
		[Obsolete("m_logLineLimit is deprecated, please use m_logLineLimits instead.")]
		public int m_logLineLimit = 100;

		// Token: 0x0400A223 RID: 41507
		[Obsolete("m_jiraZipSizeLimit is deprecated, please use m_maxZipSizeLimits instead.")]
		public int m_maxZipSizeLimit = 2097152;

		// Token: 0x0400A224 RID: 41508
		public Dictionary<ExceptionSettings.ReportType, int> m_logLineLimits = new Dictionary<ExceptionSettings.ReportType, int>
		{
			{
				ExceptionSettings.ReportType.EXCEPTION,
				100
			},
			{
				ExceptionSettings.ReportType.BUG,
				0
			},
			{
				ExceptionSettings.ReportType.ANR,
				0
			}
		};

		// Token: 0x0400A225 RID: 41509
		public Dictionary<ExceptionSettings.ReportType, int> m_maxZipSizeLimits = new Dictionary<ExceptionSettings.ReportType, int>
		{
			{
				ExceptionSettings.ReportType.EXCEPTION,
				2097152
			},
			{
				ExceptionSettings.ReportType.BUG,
				7340032
			},
			{
				ExceptionSettings.ReportType.ANR,
				2097152
			}
		};

		// Token: 0x0400A226 RID: 41510
		public Dictionary<ExceptionSettings.ReportType, int> m_maxScreenshotWidths = new Dictionary<ExceptionSettings.ReportType, int>
		{
			{
				ExceptionSettings.ReportType.EXCEPTION,
				-1
			},
			{
				ExceptionSettings.ReportType.BUG,
				-1
			},
			{
				ExceptionSettings.ReportType.ANR,
				-1
			}
		};

		// Token: 0x0400A227 RID: 41511
		public ExceptionSettings.LogPathsHandler m_logPathsCallback;

		// Token: 0x0400A228 RID: 41512
		public ExceptionSettings.AttachableFilesHandler m_attachableFilesCallback;

		// Token: 0x0400A229 RID: 41513
		public ExceptionSettings.AdditionalInfoHandler m_additionalInfoCallback;

		// Token: 0x0400A22A RID: 41514
		public ExceptionSettings.ReadFileMethodHandler m_readFileMethodCallback;

		// Token: 0x0200294F RID: 10575
		public enum ReportType
		{
			// Token: 0x0400FC96 RID: 64662
			EXCEPTION,
			// Token: 0x0400FC97 RID: 64663
			BUG,
			// Token: 0x0400FC98 RID: 64664
			ANR
		}

		// Token: 0x02002950 RID: 10576
		// (Invoke) Token: 0x06013E70 RID: 81520
		public delegate string[] LogPathsHandler(ExceptionSettings.ReportType type);

		// Token: 0x02002951 RID: 10577
		// (Invoke) Token: 0x06013E74 RID: 81524
		public delegate string[] AttachableFilesHandler(ExceptionSettings.ReportType type);

		// Token: 0x02002952 RID: 10578
		// (Invoke) Token: 0x06013E78 RID: 81528
		public delegate Dictionary<string, string> AdditionalInfoHandler(ExceptionSettings.ReportType type);

		// Token: 0x02002953 RID: 10579
		// (Invoke) Token: 0x06013E7C RID: 81532
		public delegate byte[] ReadFileMethodHandler(string filePath);
	}
}
