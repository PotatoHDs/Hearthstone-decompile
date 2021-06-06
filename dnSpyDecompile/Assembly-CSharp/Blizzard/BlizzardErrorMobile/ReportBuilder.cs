using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x02001218 RID: 4632
	public class ReportBuilder
	{
		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x0600CFE8 RID: 53224 RVA: 0x003DE00E File Offset: 0x003DC20E
		// (set) Token: 0x0600CFE9 RID: 53225 RVA: 0x003DE015 File Offset: 0x003DC215
		public static string ApplicationUnityVersion { get; set; }

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x0600CFEA RID: 53226 RVA: 0x003DE01D File Offset: 0x003DC21D
		// (set) Token: 0x0600CFEB RID: 53227 RVA: 0x003DE024 File Offset: 0x003DC224
		public static bool ApplicationGenuine { get; set; }

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x0600CFEC RID: 53228 RVA: 0x003DE02C File Offset: 0x003DC22C
		// (set) Token: 0x0600CFED RID: 53229 RVA: 0x003DE034 File Offset: 0x003DC234
		public ExceptionSettings.ReportType ReportType { get; set; }

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x0600CFEE RID: 53230 RVA: 0x003DE03D File Offset: 0x003DC23D
		// (set) Token: 0x0600CFEF RID: 53231 RVA: 0x003DE045 File Offset: 0x003DC245
		public int ProjectID { get; set; }

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x0600CFF0 RID: 53232 RVA: 0x003DE04E File Offset: 0x003DC24E
		// (set) Token: 0x0600CFF1 RID: 53233 RVA: 0x003DE056 File Offset: 0x003DC256
		public string ModuleName { get; set; }

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x0600CFF2 RID: 53234 RVA: 0x003DE05F File Offset: 0x003DC25F
		// (set) Token: 0x0600CFF3 RID: 53235 RVA: 0x003DE067 File Offset: 0x003DC267
		public string Summary { get; set; }

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x0600CFF4 RID: 53236 RVA: 0x003DE070 File Offset: 0x003DC270
		// (set) Token: 0x0600CFF5 RID: 53237 RVA: 0x003DE078 File Offset: 0x003DC278
		public string StackTrace { get; protected set; }

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x0600CFF6 RID: 53238 RVA: 0x003DE081 File Offset: 0x003DC281
		// (set) Token: 0x0600CFF7 RID: 53239 RVA: 0x003DE089 File Offset: 0x003DC289
		public string Comment { get; protected set; }

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x0600CFF8 RID: 53240 RVA: 0x003DE092 File Offset: 0x003DC292
		// (set) Token: 0x0600CFF9 RID: 53241 RVA: 0x003DE09A File Offset: 0x003DC29A
		public string Hash { get; protected set; }

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x0600CFFA RID: 53242 RVA: 0x003DE0A3 File Offset: 0x003DC2A3
		// (set) Token: 0x0600CFFB RID: 53243 RVA: 0x003DE0AB File Offset: 0x003DC2AB
		public string Markup { get; protected set; }

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600CFFC RID: 53244 RVA: 0x003DE0B4 File Offset: 0x003DC2B4
		// (set) Token: 0x0600CFFD RID: 53245 RVA: 0x003DE0BC File Offset: 0x003DC2BC
		public string EnteredBy { get; set; }

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x0600CFFE RID: 53246 RVA: 0x003DE0C5 File Offset: 0x003DC2C5
		// (set) Token: 0x0600CFFF RID: 53247 RVA: 0x003DE0CD File Offset: 0x003DC2CD
		public int BuildNumber { get; set; }

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x0600D000 RID: 53248 RVA: 0x003DE0D6 File Offset: 0x003DC2D6
		// (set) Token: 0x0600D001 RID: 53249 RVA: 0x003DE0DE File Offset: 0x003DC2DE
		public string Branch { get; set; }

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x0600D002 RID: 53250 RVA: 0x003DE0E7 File Offset: 0x003DC2E7
		// (set) Token: 0x0600D003 RID: 53251 RVA: 0x003DE0EF File Offset: 0x003DC2EF
		public string Locale { get; set; }

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x0600D004 RID: 53252 RVA: 0x003DE0F8 File Offset: 0x003DC2F8
		// (set) Token: 0x0600D005 RID: 53253 RVA: 0x003DE100 File Offset: 0x003DC300
		public string ReportUUID { get; set; }

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x0600D006 RID: 53254 RVA: 0x003DE109 File Offset: 0x003DC309
		// (set) Token: 0x0600D007 RID: 53255 RVA: 0x003DE111 File Offset: 0x003DC311
		public string UserUUID { get; set; }

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x0600D008 RID: 53256 RVA: 0x003DE11A File Offset: 0x003DC31A
		// (set) Token: 0x0600D009 RID: 53257 RVA: 0x003DE122 File Offset: 0x003DC322
		public bool CaptureLogs { get; set; }

		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x0600D00A RID: 53258 RVA: 0x003DE12B File Offset: 0x003DC32B
		// (set) Token: 0x0600D00B RID: 53259 RVA: 0x003DE133 File Offset: 0x003DC333
		public bool CaptureConfig { get; set; }

		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x0600D00C RID: 53260 RVA: 0x003DE13C File Offset: 0x003DC33C
		// (set) Token: 0x0600D00D RID: 53261 RVA: 0x003DE144 File Offset: 0x003DC344
		public int SizeLimit { get; set; }

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x0600D00E RID: 53262 RVA: 0x003DE14D File Offset: 0x003DC34D
		// (set) Token: 0x0600D00F RID: 53263 RVA: 0x003DE155 File Offset: 0x003DC355
		public int LogLinesLimit { get; set; }

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x0600D010 RID: 53264 RVA: 0x003DE15E File Offset: 0x003DC35E
		// (set) Token: 0x0600D011 RID: 53265 RVA: 0x003DE166 File Offset: 0x003DC366
		public string[] LogPaths { get; set; }

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x0600D012 RID: 53266 RVA: 0x003DE16F File Offset: 0x003DC36F
		// (set) Token: 0x0600D013 RID: 53267 RVA: 0x003DE177 File Offset: 0x003DC377
		public string[] AttachableFiles { get; set; }

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x0600D014 RID: 53268 RVA: 0x003DE180 File Offset: 0x003DC380
		// (set) Token: 0x0600D015 RID: 53269 RVA: 0x003DE188 File Offset: 0x003DC388
		public Dictionary<string, string> AddtionalInfo { get; set; }

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x0600D016 RID: 53270 RVA: 0x003DE191 File Offset: 0x003DC391
		// (set) Token: 0x0600D017 RID: 53271 RVA: 0x003DE199 File Offset: 0x003DC399
		public string SerializedScene { get; set; }

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x0600D018 RID: 53272 RVA: 0x003DE1A2 File Offset: 0x003DC3A2
		// (set) Token: 0x0600D019 RID: 53273 RVA: 0x003DE1AC File Offset: 0x003DC3AC
		public static ExceptionSettings Settings
		{
			get
			{
				return ReportBuilder.s_settings;
			}
			set
			{
				if (string.IsNullOrEmpty(value.m_userUUID))
				{
					value.m_userUUID = SystemInfo.deviceUniqueIdentifier.ToString();
				}
				if (value.m_projectID == 2147483647)
				{
					throw new Exception("Setting is invalid! - no projectID");
				}
				if (value.m_buildNumber == 2147483647)
				{
					throw new Exception("Setting is invalid! - no buildNumber");
				}
				if (string.IsNullOrEmpty(value.m_version))
				{
					throw new Exception("Setting is invalid! - no version");
				}
				if (string.IsNullOrEmpty(value.m_locale))
				{
					throw new Exception("Setting is invalid! - no locale");
				}
				if (value.m_logLineLimits[ExceptionSettings.ReportType.EXCEPTION] == 100)
				{
					value.m_logLineLimits[ExceptionSettings.ReportType.EXCEPTION] = value.m_logLineLimit;
				}
				if (value.m_maxZipSizeLimit > 10485760)
				{
					ExceptionLogger.LogWarning(string.Format("Zip max size for Exception board cannot exceed {0} bytes.", 10485760), Array.Empty<object>());
					value.m_maxZipSizeLimit = 10485760;
				}
				if (value.m_maxZipSizeLimits[ExceptionSettings.ReportType.EXCEPTION] == 2097152)
				{
					value.m_maxZipSizeLimits[ExceptionSettings.ReportType.EXCEPTION] = value.m_maxZipSizeLimit;
				}
				if (value.m_jiraZipSizeLimit > 10485760)
				{
					ExceptionLogger.LogWarning(string.Format("Zip max size for JIRA system cannot exceed {0} bytes.", 10485760), Array.Empty<object>());
					value.m_jiraZipSizeLimit = 10485760;
				}
				if (value.m_maxZipSizeLimits[ExceptionSettings.ReportType.BUG] == 7340032)
				{
					value.m_maxZipSizeLimits[ExceptionSettings.ReportType.BUG] = value.m_maxZipSizeLimit;
				}
				ReportBuilder.s_settings = value;
			}
		}

		// Token: 0x0600D01A RID: 53274 RVA: 0x000052CE File Offset: 0x000034CE
		private ReportBuilder()
		{
		}

		// Token: 0x0600D01B RID: 53275 RVA: 0x003DE314 File Offset: 0x003DC514
		public static ReportBuilder BuildExceptionReport(string summary, string stackTrace, ExceptionSettings.ReportType reportType)
		{
			return ReportBuilder.BuildExceptionReport(summary, stackTrace, string.Empty, reportType);
		}

		// Token: 0x0600D01C RID: 53276 RVA: 0x003DE324 File Offset: 0x003DC524
		protected static ReportBuilder BuildExceptionReport(string summary, string stackTrace, string comment, ExceptionSettings.ReportType reportType)
		{
			if (ReportBuilder.s_settings == null)
			{
				throw new Exception("Setting should be set!");
			}
			ReportBuilder reportBuilder = new ReportBuilder();
			reportBuilder.Summary = summary;
			reportBuilder.StackTrace = stackTrace;
			reportBuilder.Comment = comment;
			reportBuilder.Hash = ReportBuilder.CreateHash(summary + stackTrace);
			reportBuilder.CaptureLogs = true;
			reportBuilder.CaptureConfig = true;
			reportBuilder.SizeLimit = ReportBuilder.Settings.m_maxZipSizeLimits[reportType];
			reportBuilder.LogLinesLimit = ReportBuilder.Settings.m_maxZipSizeLimits[reportType];
			reportBuilder.EnteredBy = "0";
			reportBuilder.ReportUUID = Guid.NewGuid().ToString().ToUpper();
			reportBuilder.UserUUID = ReportBuilder.Settings.m_userUUID;
			reportBuilder.ProjectID = ReportBuilder.Settings.m_projectID;
			reportBuilder.ModuleName = ReportBuilder.Settings.m_moduleName;
			reportBuilder.BuildNumber = ReportBuilder.Settings.m_buildNumber;
			reportBuilder.Branch = ReportBuilder.Settings.m_branchName;
			reportBuilder.Locale = ReportBuilder.Settings.m_locale;
			reportBuilder.ReportType = reportType;
			if (ReportBuilder.Settings.m_logPathsCallback != null)
			{
				reportBuilder.LogPaths = ReportBuilder.Settings.m_logPathsCallback(reportBuilder.ReportType);
			}
			if (ReportBuilder.Settings.m_attachableFilesCallback != null)
			{
				reportBuilder.AttachableFiles = ReportBuilder.Settings.m_attachableFilesCallback(reportBuilder.ReportType);
			}
			if (ReportBuilder.Settings.m_additionalInfoCallback != null)
			{
				reportBuilder.AddtionalInfo = ReportBuilder.Settings.m_additionalInfoCallback(reportBuilder.ReportType);
			}
			reportBuilder.BuildExceptionMarkup();
			return reportBuilder;
		}

		// Token: 0x0600D01D RID: 53277 RVA: 0x003DE4B8 File Offset: 0x003DC6B8
		private static string CreateHash(string blob)
		{
			string[] value = new string[]
			{
				"at <[0-9a-fA-F]{32}>:0",
				"\\bBuildId: ([0-9a-f]{32}|[0-9a-f]{40})\\b",
				"\\b0x[0-9a-f]{16}\\b",
				"/data/app/com\\.blizzard\\..*/",
				"_m[0-9a-fA-F]{40}\\b",
				"\\b[0-9a-f]{16}\\b"
			};
			string pattern = string.Join("|", value);
			return ReportBuilder.SHA1Calc(Regex.Replace(blob, pattern, delegate(Match m)
			{
				if (m.Value.StartsWith("at "))
				{
					return "at <0>:0";
				}
				if (m.Value.StartsWith("BuildId:"))
				{
					return "BuildId: 0";
				}
				if (m.Value.StartsWith("0x"))
				{
					return "0x0";
				}
				if (m.Value.StartsWith("/data/app/"))
				{
					return "/data/app/com.blizzard./";
				}
				if (m.Value.StartsWith("_m"))
				{
					return "_m0";
				}
				return "0";
			}));
		}

		// Token: 0x0600D01E RID: 53278 RVA: 0x003DE534 File Offset: 0x003DC734
		private static string SHA1Calc(string message)
		{
			SHA1 sha = SHA1.Create();
			byte[] array = sha.ComputeHash(Encoding.ASCII.GetBytes(message));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			sha.Dispose();
			return stringBuilder.ToString();
		}

		// Token: 0x0600D01F RID: 53279 RVA: 0x003DE594 File Offset: 0x003DC794
		private void BuildExceptionMarkup()
		{
			string text = ReportBuilder.CreateEscapedSGML(this.Summary);
			string text2 = ReportBuilder.CreateEscapedSGML(this.StackTrace);
			string text3 = "";
			if (this.AddtionalInfo != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.AddtionalInfo)
				{
					text3 = string.Concat(new string[]
					{
						text3,
						"\t\t<NameValuePair><Name>",
						ReportBuilder.CreateEscapedSGML(keyValuePair.Key),
						"</Name><Value>",
						ReportBuilder.CreateEscapedSGML(keyValuePair.Value),
						"</Value></NameValuePair>\n"
					});
				}
			}
			this.Markup = string.Concat(new object[]
			{
				"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<ReportedIssue xmlns=\"http://schemas.datacontract.org/2004/07/Inspector.Models\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">\n\t<Summary>",
				text,
				"</Summary>\n\t<Assertion>",
				text2,
				"</Assertion>\n\t<HashBlock>",
				this.Hash,
				"</HashBlock>\n\t<BuildNumber>",
				this.BuildNumber,
				"</BuildNumber>\n   <Comments>",
				this.Comment,
				"</Comments >\t<Module>",
				this.ModuleName,
				"</Module>\n\t<EnteredBy>",
				this.EnteredBy,
				"</EnteredBy>\n\t<IssueType>Exception</IssueType>\n\t<ProjectId>",
				this.ProjectID,
				"</ProjectId>\n\t<Metadata><NameValuePairs>\n\t\t<NameValuePair><Name>Build</Name><Value>",
				this.BuildNumber,
				"</Value></NameValuePair>\n\t\t<NameValuePair><Name>OS.Platform</Name><Value>",
				Application.platform,
				"</Value></NameValuePair>\n\t\t<NameValuePair><Name>Unity.Version</Name><Value>",
				ReportBuilder.ApplicationUnityVersion,
				"</Value></NameValuePair>\n\t\t<NameValuePair><Name>Unity.Genuine</Name><Value>",
				ReportBuilder.ApplicationGenuine.ToString(),
				"</Value></NameValuePair>\n\t\t<NameValuePair><Name>Locale</Name><Value>",
				this.Locale,
				"</Value></NameValuePair>\n\t\t<NameValuePair><Name>Branch</Name><Value>",
				this.Branch,
				"</Value></NameValuePair>\n\t\t<NameValuePair><Name>Report.UUID</Name><Value>",
				this.ReportUUID,
				"</Value></NameValuePair>\n\t\t<NameValuePair><Name>User.UUID</Name><Value>",
				this.UserUUID,
				"</Value></NameValuePair>\n",
				text3,
				"\t</NameValuePairs></Metadata>\n</ReportedIssue>\n"
			});
		}

		// Token: 0x0600D020 RID: 53280 RVA: 0x003DE7B0 File Offset: 0x003DC9B0
		private static string CreateEscapedSGML(string blob)
		{
			XmlElement xmlElement = new XmlDocument().CreateElement("root");
			xmlElement.InnerText = blob;
			return xmlElement.InnerXml;
		}

		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x0600D021 RID: 53281 RVA: 0x003DE7CD File Offset: 0x003DC9CD
		// (set) Token: 0x0600D022 RID: 53282 RVA: 0x003DE7D5 File Offset: 0x003DC9D5
		public string Description { get; set; }

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x0600D023 RID: 53283 RVA: 0x003DE7DE File Offset: 0x003DC9DE
		// (set) Token: 0x0600D024 RID: 53284 RVA: 0x003DE7E6 File Offset: 0x003DC9E6
		public string Version { get; set; }

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x0600D025 RID: 53285 RVA: 0x003DE7EF File Offset: 0x003DC9EF
		// (set) Token: 0x0600D026 RID: 53286 RVA: 0x003DE7F7 File Offset: 0x003DC9F7
		public string JiraProjectName { get; set; }

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x0600D027 RID: 53287 RVA: 0x003DE800 File Offset: 0x003DCA00
		// (set) Token: 0x0600D028 RID: 53288 RVA: 0x003DE808 File Offset: 0x003DCA08
		public string JiraComponentName { get; set; }

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x0600D029 RID: 53289 RVA: 0x003DE811 File Offset: 0x003DCA11
		// (set) Token: 0x0600D02A RID: 53290 RVA: 0x003DE819 File Offset: 0x003DCA19
		public string JiraVersion { get; set; }

		// Token: 0x0600D02B RID: 53291 RVA: 0x003DE824 File Offset: 0x003DCA24
		public static ReportBuilder BuildBugReport(string summary, string enteredBy, string description)
		{
			ReportBuilder reportBuilder = new ReportBuilder();
			reportBuilder.Summary = summary;
			reportBuilder.EnteredBy = enteredBy;
			reportBuilder.Description = description;
			reportBuilder.JiraProjectName = ReportBuilder.Settings.m_jiraProjectName;
			reportBuilder.JiraComponentName = ReportBuilder.Settings.m_jiraComponent;
			reportBuilder.JiraVersion = ReportBuilder.Settings.m_jiraVersion;
			reportBuilder.CaptureLogs = true;
			reportBuilder.CaptureConfig = true;
			reportBuilder.SizeLimit = ReportBuilder.Settings.m_maxZipSizeLimits[ExceptionSettings.ReportType.BUG];
			reportBuilder.LogLinesLimit = ReportBuilder.Settings.m_logLineLimits[ExceptionSettings.ReportType.BUG];
			reportBuilder.ProjectID = ReportBuilder.Settings.m_projectID;
			reportBuilder.ModuleName = ReportBuilder.Settings.m_moduleName;
			reportBuilder.Version = ReportBuilder.Settings.m_version;
			reportBuilder.Locale = ReportBuilder.Settings.m_locale;
			reportBuilder.ReportType = ExceptionSettings.ReportType.BUG;
			if (ReportBuilder.Settings.m_logPathsCallback != null)
			{
				reportBuilder.LogPaths = ReportBuilder.Settings.m_logPathsCallback(reportBuilder.ReportType);
			}
			if (ReportBuilder.Settings.m_attachableFilesCallback != null)
			{
				reportBuilder.AttachableFiles = ReportBuilder.Settings.m_attachableFilesCallback(reportBuilder.ReportType);
			}
			if (ReportBuilder.Settings.m_additionalInfoCallback != null)
			{
				reportBuilder.AddtionalInfo = ReportBuilder.Settings.m_additionalInfoCallback(reportBuilder.ReportType);
			}
			reportBuilder.FinalizeBugMarkup();
			return reportBuilder;
		}

		// Token: 0x0600D02C RID: 53292 RVA: 0x003DE97C File Offset: 0x003DCB7C
		public void FinalizeBugMarkup()
		{
			string text = ReportBuilder.CreateEscapedSGML(this.Summary);
			string text2 = "";
			foreach (KeyValuePair<string, string> keyValuePair in this.AddtionalInfo)
			{
				text2 = string.Concat(new string[]
				{
					text2,
					ReportBuilder.CreateEscapedSGML(keyValuePair.Key),
					": ",
					ReportBuilder.CreateEscapedSGML(keyValuePair.Value),
					"\n"
				});
			}
			this.Comment = string.Concat(new object[]
			{
				"Build: ",
				this.BuildNumber,
				"\nOS.Platform: ",
				Application.platform,
				"\nUnity.Version: ",
				Application.unityVersion,
				"\nUnity.Genuine: ",
				Application.genuine.ToString(),
				"\nLocale: ",
				this.Locale,
				"\nVersion: ",
				this.Version,
				"\nloadedLevelName: ",
				SceneManager.GetActiveScene().name,
				"\n",
				text2
			});
			this.Markup = string.Concat(new object[]
			{
				"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<ReportedIssue xmlns=\"http://schemas.datacontract.org/2004/07/Inspector.Models\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">\n\t<Summary>",
				text,
				"</Summary>\n\t<Assertion>Created intentionally through the \"bug\" command</Assertion>\n\t<Comments>\n",
				this.Comment,
				"</Comments>\n\t<BuildNumber>",
				this.BuildNumber,
				"</BuildNumber>\n\t<Module>",
				this.ModuleName,
				"</Module>\n\t<EnteredBy>",
				this.EnteredBy,
				"</EnteredBy>\n\t<IssueType>Bug</IssueType>\n\t<ProjectId>",
				this.ProjectID,
				"</ProjectId>\n\t<Description>",
				this.Description,
				"</Description>\n</ReportedIssue>\n"
			});
		}

		// Token: 0x0400A25D RID: 41565
		private static ExceptionSettings s_settings;
	}
}
