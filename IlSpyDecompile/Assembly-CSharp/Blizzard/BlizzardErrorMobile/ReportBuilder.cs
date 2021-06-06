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
	public class ReportBuilder
	{
		private static ExceptionSettings s_settings;

		public static string ApplicationUnityVersion { get; set; }

		public static bool ApplicationGenuine { get; set; }

		public ExceptionSettings.ReportType ReportType { get; set; }

		public int ProjectID { get; set; }

		public string ModuleName { get; set; }

		public string Summary { get; set; }

		public string StackTrace { get; protected set; }

		public string Comment { get; protected set; }

		public string Hash { get; protected set; }

		public string Markup { get; protected set; }

		public string EnteredBy { get; set; }

		public int BuildNumber { get; set; }

		public string Branch { get; set; }

		public string Locale { get; set; }

		public string ReportUUID { get; set; }

		public string UserUUID { get; set; }

		public bool CaptureLogs { get; set; }

		public bool CaptureConfig { get; set; }

		public int SizeLimit { get; set; }

		public int LogLinesLimit { get; set; }

		public string[] LogPaths { get; set; }

		public string[] AttachableFiles { get; set; }

		public Dictionary<string, string> AddtionalInfo { get; set; }

		public string SerializedScene { get; set; }

		public static ExceptionSettings Settings
		{
			get
			{
				return s_settings;
			}
			set
			{
				if (string.IsNullOrEmpty(value.m_userUUID))
				{
					value.m_userUUID = SystemInfo.deviceUniqueIdentifier.ToString();
				}
				if (value.m_projectID == int.MaxValue)
				{
					throw new Exception("Setting is invalid! - no projectID");
				}
				if (value.m_buildNumber == int.MaxValue)
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
					ExceptionLogger.LogWarning($"Zip max size for Exception board cannot exceed {10485760} bytes.");
					value.m_maxZipSizeLimit = 10485760;
				}
				if (value.m_maxZipSizeLimits[ExceptionSettings.ReportType.EXCEPTION] == 2097152)
				{
					value.m_maxZipSizeLimits[ExceptionSettings.ReportType.EXCEPTION] = value.m_maxZipSizeLimit;
				}
				if (value.m_jiraZipSizeLimit > 10485760)
				{
					ExceptionLogger.LogWarning($"Zip max size for JIRA system cannot exceed {10485760} bytes.");
					value.m_jiraZipSizeLimit = 10485760;
				}
				if (value.m_maxZipSizeLimits[ExceptionSettings.ReportType.BUG] == 7340032)
				{
					value.m_maxZipSizeLimits[ExceptionSettings.ReportType.BUG] = value.m_maxZipSizeLimit;
				}
				s_settings = value;
			}
		}

		public string Description { get; set; }

		public string Version { get; set; }

		public string JiraProjectName { get; set; }

		public string JiraComponentName { get; set; }

		public string JiraVersion { get; set; }

		private ReportBuilder()
		{
		}

		public static ReportBuilder BuildExceptionReport(string summary, string stackTrace, ExceptionSettings.ReportType reportType)
		{
			return BuildExceptionReport(summary, stackTrace, string.Empty, reportType);
		}

		protected static ReportBuilder BuildExceptionReport(string summary, string stackTrace, string comment, ExceptionSettings.ReportType reportType)
		{
			if (s_settings == null)
			{
				throw new Exception("Setting should be set!");
			}
			ReportBuilder reportBuilder = new ReportBuilder();
			reportBuilder.Summary = summary;
			reportBuilder.StackTrace = stackTrace;
			reportBuilder.Comment = comment;
			reportBuilder.Hash = CreateHash(summary + stackTrace);
			reportBuilder.CaptureLogs = true;
			reportBuilder.CaptureConfig = true;
			reportBuilder.SizeLimit = Settings.m_maxZipSizeLimits[reportType];
			reportBuilder.LogLinesLimit = Settings.m_maxZipSizeLimits[reportType];
			reportBuilder.EnteredBy = "0";
			reportBuilder.ReportUUID = Guid.NewGuid().ToString().ToUpper();
			reportBuilder.UserUUID = Settings.m_userUUID;
			reportBuilder.ProjectID = Settings.m_projectID;
			reportBuilder.ModuleName = Settings.m_moduleName;
			reportBuilder.BuildNumber = Settings.m_buildNumber;
			reportBuilder.Branch = Settings.m_branchName;
			reportBuilder.Locale = Settings.m_locale;
			reportBuilder.ReportType = reportType;
			if (Settings.m_logPathsCallback != null)
			{
				reportBuilder.LogPaths = Settings.m_logPathsCallback(reportBuilder.ReportType);
			}
			if (Settings.m_attachableFilesCallback != null)
			{
				reportBuilder.AttachableFiles = Settings.m_attachableFilesCallback(reportBuilder.ReportType);
			}
			if (Settings.m_additionalInfoCallback != null)
			{
				reportBuilder.AddtionalInfo = Settings.m_additionalInfoCallback(reportBuilder.ReportType);
			}
			reportBuilder.BuildExceptionMarkup();
			return reportBuilder;
		}

		private static string CreateHash(string blob)
		{
			string[] value = new string[6] { "at <[0-9a-fA-F]{32}>:0", "\\bBuildId: ([0-9a-f]{32}|[0-9a-f]{40})\\b", "\\b0x[0-9a-f]{16}\\b", "/data/app/com\\.blizzard\\..*/", "_m[0-9a-fA-F]{40}\\b", "\\b[0-9a-f]{16}\\b" };
			string pattern = string.Join("|", value);
			return SHA1Calc(Regex.Replace(blob, pattern, delegate(Match m)
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
				return m.Value.StartsWith("_m") ? "_m0" : "0";
			}));
		}

		private static string SHA1Calc(string message)
		{
			SHA1 sHA = SHA1.Create();
			byte[] array = sHA.ComputeHash(Encoding.ASCII.GetBytes(message));
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array2 = array;
			foreach (byte b in array2)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			sHA.Dispose();
			return stringBuilder.ToString();
		}

		private void BuildExceptionMarkup()
		{
			string text = CreateEscapedSGML(Summary);
			string text2 = CreateEscapedSGML(StackTrace);
			string text3 = "";
			if (AddtionalInfo != null)
			{
				foreach (KeyValuePair<string, string> item in AddtionalInfo)
				{
					text3 = text3 + "\t\t<NameValuePair><Name>" + CreateEscapedSGML(item.Key) + "</Name><Value>" + CreateEscapedSGML(item.Value) + "</Value></NameValuePair>\n";
				}
			}
			Markup = string.Concat("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<ReportedIssue xmlns=\"http://schemas.datacontract.org/2004/07/Inspector.Models\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">\n\t<Summary>", text, "</Summary>\n\t<Assertion>", text2, "</Assertion>\n\t<HashBlock>", Hash, "</HashBlock>\n\t<BuildNumber>", BuildNumber, "</BuildNumber>\n   <Comments>", Comment, "</Comments >\t<Module>", ModuleName, "</Module>\n\t<EnteredBy>", EnteredBy, "</EnteredBy>\n\t<IssueType>Exception</IssueType>\n\t<ProjectId>", ProjectID, "</ProjectId>\n\t<Metadata><NameValuePairs>\n\t\t<NameValuePair><Name>Build</Name><Value>", BuildNumber, "</Value></NameValuePair>\n\t\t<NameValuePair><Name>OS.Platform</Name><Value>", Application.platform, "</Value></NameValuePair>\n\t\t<NameValuePair><Name>Unity.Version</Name><Value>", ApplicationUnityVersion, "</Value></NameValuePair>\n\t\t<NameValuePair><Name>Unity.Genuine</Name><Value>", ApplicationGenuine.ToString(), "</Value></NameValuePair>\n\t\t<NameValuePair><Name>Locale</Name><Value>", Locale, "</Value></NameValuePair>\n\t\t<NameValuePair><Name>Branch</Name><Value>", Branch, "</Value></NameValuePair>\n\t\t<NameValuePair><Name>Report.UUID</Name><Value>", ReportUUID, "</Value></NameValuePair>\n\t\t<NameValuePair><Name>User.UUID</Name><Value>", UserUUID, "</Value></NameValuePair>\n", text3, "\t</NameValuePairs></Metadata>\n</ReportedIssue>\n");
		}

		private static string CreateEscapedSGML(string blob)
		{
			XmlElement xmlElement = new XmlDocument().CreateElement("root");
			xmlElement.InnerText = blob;
			return xmlElement.InnerXml;
		}

		public static ReportBuilder BuildBugReport(string summary, string enteredBy, string description)
		{
			ReportBuilder reportBuilder = new ReportBuilder();
			reportBuilder.Summary = summary;
			reportBuilder.EnteredBy = enteredBy;
			reportBuilder.Description = description;
			reportBuilder.JiraProjectName = Settings.m_jiraProjectName;
			reportBuilder.JiraComponentName = Settings.m_jiraComponent;
			reportBuilder.JiraVersion = Settings.m_jiraVersion;
			reportBuilder.CaptureLogs = true;
			reportBuilder.CaptureConfig = true;
			reportBuilder.SizeLimit = Settings.m_maxZipSizeLimits[ExceptionSettings.ReportType.BUG];
			reportBuilder.LogLinesLimit = Settings.m_logLineLimits[ExceptionSettings.ReportType.BUG];
			reportBuilder.ProjectID = Settings.m_projectID;
			reportBuilder.ModuleName = Settings.m_moduleName;
			reportBuilder.Version = Settings.m_version;
			reportBuilder.Locale = Settings.m_locale;
			reportBuilder.ReportType = ExceptionSettings.ReportType.BUG;
			if (Settings.m_logPathsCallback != null)
			{
				reportBuilder.LogPaths = Settings.m_logPathsCallback(reportBuilder.ReportType);
			}
			if (Settings.m_attachableFilesCallback != null)
			{
				reportBuilder.AttachableFiles = Settings.m_attachableFilesCallback(reportBuilder.ReportType);
			}
			if (Settings.m_additionalInfoCallback != null)
			{
				reportBuilder.AddtionalInfo = Settings.m_additionalInfoCallback(reportBuilder.ReportType);
			}
			reportBuilder.FinalizeBugMarkup();
			return reportBuilder;
		}

		public void FinalizeBugMarkup()
		{
			string text = CreateEscapedSGML(Summary);
			string text2 = "";
			foreach (KeyValuePair<string, string> item in AddtionalInfo)
			{
				text2 = text2 + CreateEscapedSGML(item.Key) + ": " + CreateEscapedSGML(item.Value) + "\n";
			}
			Comment = string.Concat("Build: ", BuildNumber, "\nOS.Platform: ", Application.platform, "\nUnity.Version: ", Application.unityVersion, "\nUnity.Genuine: ", Application.genuine.ToString(), "\nLocale: ", Locale, "\nVersion: ", Version, "\nloadedLevelName: ", SceneManager.GetActiveScene().name, "\n", text2);
			Markup = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<ReportedIssue xmlns=\"http://schemas.datacontract.org/2004/07/Inspector.Models\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">\n\t<Summary>" + text + "</Summary>\n\t<Assertion>Created intentionally through the \"bug\" command</Assertion>\n\t<Comments>\n" + Comment + "</Comments>\n\t<BuildNumber>" + BuildNumber + "</BuildNumber>\n\t<Module>" + ModuleName + "</Module>\n\t<EnteredBy>" + EnteredBy + "</EnteredBy>\n\t<IssueType>Bug</IssueType>\n\t<ProjectId>" + ProjectID + "</ProjectId>\n\t<Description>" + Description + "</Description>\n</ReportedIssue>\n";
		}
	}
}
