using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Blizzard.BlizzardErrorMobile.MiniJSON;
using UnityEngine;
using UnityEngine.Networking;

namespace Blizzard.BlizzardErrorMobile
{
	public class ExceptionReporter
	{
		public class CustomCertHandler : CertificateHandler
		{
			protected override bool ValidateCertificate(byte[] certificateData)
			{
				return true;
			}
		}

		private const int DEFAULT_MAX_RECORDS = 20;

		private const float DEFAULT_ANR_THROTTLE = 0.05f;

		private static readonly string s_userUUID = SystemInfo.deviceUniqueIdentifier.ToString();

		private static readonly string s_baseSubmitURL = "http://iir.blizzard.com:3724/submit/";

		private static readonly string s_titleOfANRException = "ANR detected";

		private static ExceptionReporter s_instance;

		private bool m_started;

		private bool m_processedPreviousExceptions;

		private string m_zipFileForANR;

		private int m_zipFileCount;

		private string m_exceptionDir;

		private MonoBehaviour m_monoBehaviour;

		private RecordedExceptions m_recordedExceptions = new RecordedExceptions();

		private HashSet<string> m_seenHashes = new HashSet<string>();

		private ANRMonitor m_monitorANR;

		public bool Busy { get; private set; }

		public bool IsInDebugMode { get; set; }

		public bool IsEnabledANRMonitor
		{
			get
			{
				if (m_monitorANR == null)
				{
					return false;
				}
				return !m_monitorANR.IsTerminated;
			}
		}

		public bool ReportOnTheFly { get; set; } = true;


		public bool SendExceptions { get; set; } = true;


		public bool SendAsserts { get; set; }

		public bool SendErrors { get; set; }

		private string SubmitURL => s_baseSubmitURL + ReportBuilder.Settings.m_projectID;

		private string GetZipName => Path.Combine(m_exceptionDir, $"exception-{m_zipFileCount++}.zip").Replace("\\", "/");

		private string ExceptionReporterDataPath => Path.Combine(m_exceptionDir, "ExceptionReporter.json").Replace("\\", "/");

		private static string KAuthHeaderContents { get; set; }

		public event Action BeforeZipping;

		public event Action AfterZipping;

		public static ExceptionReporter Get()
		{
			if (s_instance == null)
			{
				s_instance = new ExceptionReporter();
			}
			return s_instance;
		}

		public void Initialize(string installPath, MonoBehaviour monoBehaviour)
		{
			Initialize(installPath, null, monoBehaviour);
		}

		public void Initialize(string installPath, IExceptionLogger logger, MonoBehaviour monoBehaviour)
		{
			ExceptionLogger.SetLogger(logger);
			m_monoBehaviour = monoBehaviour;
			ExceptionLogger.LogInfo("Version {0}", VersionInfo.VERSION);
			ExceptionLogger.LogInfo("ScreenshotPath: {0}", Screenshot.ScreenshotPath);
			Screenshot.RemoveScreenshot();
			m_exceptionDir = Path.Combine(installPath, "Exceptions").Replace("\\", "/");
			if (!Directory.Exists(m_exceptionDir))
			{
				try
				{
					Directory.CreateDirectory(m_exceptionDir);
				}
				catch (Exception ex)
				{
					ExceptionLogger.LogError("Failed to create the folder '{0}' for exceptions: {1}", m_exceptionDir, ex.Message);
					UnregisterLogsCallback();
					return;
				}
			}
			DeserializeRecordedExceptions();
			CallbackManager.RegisterExceptionHandler();
			RegisterLogsCallback();
			ExceptionLogger.LogInfo("User UUID: {0}", s_userUUID);
			ReportBuilder.ApplicationUnityVersion = Application.unityVersion;
			ReportBuilder.ApplicationGenuine = Application.genuine;
			CreateCoroutine(TryReportPreviousExceptions());
		}

		public bool SetSettings(ExceptionSettings settings)
		{
			try
			{
				ReportBuilder.Settings = settings;
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to set Settings: {0}", ex.Message);
				return false;
			}
			return true;
		}

		public void ReportCaughtException(string message, string stackTrace)
		{
			RecordException("[Caught] " + message, stackTrace, recordOnly: false, ExceptionSettings.ReportType.EXCEPTION);
		}

		public void RecordException(string message, string stackTrace)
		{
			RecordException(message, stackTrace, recordOnly: false, ExceptionSettings.ReportType.EXCEPTION);
		}

		public void RecordException(string message, string stackTrace, bool recordOnly)
		{
			RecordException(message, stackTrace, recordOnly, ExceptionSettings.ReportType.EXCEPTION);
		}

		public void RecordException(string message, string stackTrace, bool recordOnly, ExceptionSettings.ReportType reportType)
		{
			if (!SendExceptions)
			{
				ExceptionLogger.LogInfo("Exception has been reported but skipped because SendException is off.");
				return;
			}
			ReportBuilder reportBuilder;
			try
			{
				reportBuilder = ReportBuilder.BuildExceptionReport(message, stackTrace, reportType);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to create ExceptionReport: {0}", ex.Message);
				return;
			}
			if (m_seenHashes.Contains(reportBuilder.Hash))
			{
				ExceptionLogger.LogDebug("Skipped same Exception...");
				return;
			}
			ExceptionLogger.LogInfo("Record an exception {0}-{1}\nAt:\n{2}", message, reportBuilder.Hash, stackTrace);
			lock (m_recordedExceptions)
			{
				if (m_recordedExceptions.m_records.Count + m_recordedExceptions.m_backupRecords.Count >= 20)
				{
					ExceptionLogger.LogWarning("It reached the maximum records. Skipped.");
					return;
				}
			}
			m_seenHashes.Add(reportBuilder.Hash);
			Screenshot.RemoveScreenshot();
			if (!ReportOnTheFly || recordOnly)
			{
				MakeZipAndRecordInner(reportBuilder);
			}
			else if (CreateCoroutine(MakeZipAndRecord(reportBuilder, ReportOnTheFly)) == null)
			{
				MakeZipAndRecordInner(reportBuilder);
			}
		}

		public void ClearExceptionHashes()
		{
			ReportExceptions();
			m_seenHashes.Clear();
		}

		public bool ReportJiraBug(string summary, string enteredBy, string description, string id, string pw)
		{
			if (string.IsNullOrEmpty(summary))
			{
				ExceptionLogger.LogError("'summary' should have a value.");
				return false;
			}
			if (string.IsNullOrEmpty(enteredBy))
			{
				ExceptionLogger.LogError("'enteredBy' should have a value.");
				return false;
			}
			ReportBuilder builder;
			try
			{
				builder = ReportBuilder.BuildBugReport(summary, enteredBy, description);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to create IssueReport: {0}", ex.Message);
				return false;
			}
			ExceptionLogger.LogInfo("Create a bug: {0} {1} {2}", summary, enteredBy, description);
			CreateCoroutine(SendJiraBug(builder, id, pw));
			return true;
		}

		public bool EnableANRMonitor(float waitLimitSeconds)
		{
			return EnableANRMonitor(waitLimitSeconds, 0.05f);
		}

		public bool EnableANRMonitor(float waitLimitSeconds, float throttle)
		{
			if (m_monoBehaviour == null)
			{
				ExceptionLogger.LogError("EnableANRMonitor can be used after initialization only.");
				return false;
			}
			if (m_monitorANR == null)
			{
				m_monitorANR = new ANRMonitor(waitLimitSeconds, throttle, m_monoBehaviour);
				m_monitorANR.Detected += RecordANRException;
				m_monitorANR.FirstUpdateAfterANR += ReportANRException;
			}
			else
			{
				m_monitorANR.SetWaitSeconds(waitLimitSeconds, throttle);
			}
			return true;
		}

		public void OnApplicationPause(bool pauseStatus)
		{
			m_monitorANR?.OnPause(pauseStatus);
		}

		private Coroutine CreateCoroutine(IEnumerator routine)
		{
			try
			{
				return m_monoBehaviour.StartCoroutine(routine);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to start coroutine: {0}", ex.Message);
				return null;
			}
		}

		private void RegisterLogsCallback()
		{
			if (!m_started)
			{
				ExceptionLogger.LogDebug("Registering error reporter callback");
				Application.logMessageReceived += LogMessageCallback;
				m_started = true;
			}
		}

		private void UnregisterLogsCallback()
		{
			if (m_started)
			{
				ExceptionLogger.LogDebug("Unregistering error reporter callback");
				Application.logMessageReceived -= LogMessageCallback;
				m_started = false;
			}
		}

		private void LogMessageCallback(string message, string stackTrace, LogType logType)
		{
			switch (logType)
			{
			case LogType.Assert:
				if (SendAsserts)
				{
					RecordException(message, stackTrace);
				}
				break;
			case LogType.Error:
				if (SendErrors && !ExceptionLogger.IsExceptionLoggerError(message))
				{
					RecordException(message, stackTrace);
				}
				break;
			case LogType.Exception:
				if (SendExceptions)
				{
					RecordException(message, stackTrace);
				}
				break;
			case LogType.Warning:
			case LogType.Log:
				break;
			}
		}

		private IEnumerator TryReportPreviousExceptions()
		{
			yield return new WaitUntil(() => ReportBuilder.Settings != null);
			ClearExceptionHashes();
		}

		private void RecordANRException()
		{
			RecordException(s_titleOfANRException, "", recordOnly: true, ExceptionSettings.ReportType.ANR);
		}

		private void ReportANRException()
		{
			m_monoBehaviour.StartCoroutine(AddScreenshotAndReport());
		}

		private IEnumerator AddScreenshotAndReport()
		{
			if (!string.IsNullOrEmpty(m_zipFileForANR) && File.Exists(m_zipFileForANR))
			{
				ExceptionLogger.LogDebug("ANR occurred before");
				yield return new WaitForEndOfFrame();
				if (!Screenshot.CaptureScreenshot(ReportBuilder.Settings.m_maxScreenshotWidths[ExceptionSettings.ReportType.ANR]))
				{
					m_zipFileForANR = string.Empty;
					yield break;
				}
				ZipUtil.AddFileToZip(Screenshot.ScreenshotPath, m_zipFileForANR, ReportBuilder.Settings.m_maxZipSizeLimits[ExceptionSettings.ReportType.ANR]);
				m_zipFileForANR = string.Empty;
				Screenshot.RemoveScreenshot();
				ReportExceptions();
			}
		}

		private void ReportExceptions()
		{
			lock (m_recordedExceptions)
			{
				ProcessPreviousExceptions();
				if (!m_started || m_recordedExceptions.m_records.Count + m_recordedExceptions.m_backupRecords.Count == 0)
				{
					return;
				}
			}
			m_monoBehaviour.StartCoroutine(SendInner());
		}

		private void ProcessPreviousExceptions()
		{
			if (!m_processedPreviousExceptions)
			{
				ExceptionLogger.LogInfo("Checking the exceptions from previous launch.");
				m_recordedExceptions.m_lastReadTimeLog = CallbackManager.CatchCrashCaptureFromLog(m_recordedExceptions.m_lastReadTimeLog, Application.identifier);
				SerializeRecordedExceptions();
				m_processedPreviousExceptions = true;
			}
		}

		private void DeserializeRecordedExceptions()
		{
			try
			{
				if (File.Exists(ExceptionReporterDataPath))
				{
					lock (m_recordedExceptions)
					{
						string json = File.ReadAllText(ExceptionReporterDataPath);
						m_recordedExceptions = JsonUtility.FromJson<RecordedExceptions>(json);
						File.Delete(ExceptionReporterDataPath);
						MergeRecordedExceptions();
						ExceptionLogger.LogInfo("Loaded exception data from '{0}'", ExceptionReporterDataPath);
					}
				}
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to read exception data: {0}", ex.Message);
			}
		}

		private void SerializeRecordedExceptions()
		{
			try
			{
				lock (m_recordedExceptions)
				{
					string contents = JsonUtility.ToJson(m_recordedExceptions);
					File.WriteAllText(ExceptionReporterDataPath, contents);
					ExceptionLogger.LogInfo("Saved exception data(Count: {0}) to '{1}'", m_recordedExceptions.m_records.Count, ExceptionReporterDataPath);
				}
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to write exception data to '{0}': {1}", ExceptionReporterDataPath, ex.Message);
			}
		}

		private void MakeZipAndRecordInner(ReportBuilder builder)
		{
			string text = GetZipName;
			try
			{
				this.BeforeZipping?.Invoke();
				byte[] bytes = ZipUtil.BuildZipArchive(builder);
				this.AfterZipping?.Invoke();
				File.WriteAllBytes(text, bytes);
			}
			catch (InsufficientMemoryException ex)
			{
				ExceptionLogger.LogError("Failed to zip because the file size is too big: {0}", ex.Message);
				text = string.Empty;
			}
			catch (Exception ex2)
			{
				ExceptionLogger.LogError("Failed to zip: {0}", ex2.Message);
			}
			lock (m_recordedExceptions)
			{
				if (!Busy)
				{
					m_recordedExceptions.m_records.Add(new ExceptionStruct(builder.Hash, builder.Summary, builder.StackTrace, builder.ReportUUID, text));
				}
				else
				{
					m_recordedExceptions.m_backupRecords.Add(new ExceptionStruct(builder.Hash, builder.Summary, builder.StackTrace, builder.ReportUUID, text));
				}
				if (builder.ReportType == ExceptionSettings.ReportType.ANR)
				{
					m_zipFileForANR = text;
				}
			}
			SerializeRecordedExceptions();
		}

		private IEnumerator MakeZipAndRecord(ReportBuilder builder, bool reportNow)
		{
			yield return new WaitForEndOfFrame();
			if (builder.ReportType != ExceptionSettings.ReportType.ANR)
			{
				Screenshot.CaptureScreenshot(ReportBuilder.Settings.m_maxScreenshotWidths[builder.ReportType]);
			}
			MakeZipAndRecordInner(builder);
			if (reportNow)
			{
				while (Busy)
				{
					yield return new WaitForSeconds(0.5f);
				}
				ReportExceptions();
			}
		}

		private IEnumerator SendInner()
		{
			ExceptionLogger.LogDebug("SendInner started");
			while (Busy)
			{
				yield return new WaitForSeconds(0.5f);
			}
			MergeRecordedExceptions();
			lock (m_recordedExceptions)
			{
				Busy = true;
				foreach (ExceptionStruct item in m_recordedExceptions.m_records)
				{
					if (m_started && File.Exists(item.m_zipName))
					{
						if (IsInDebugMode)
						{
							continue;
						}
						ExceptionLogger.LogInfo("Reporting exception: ReportUUID {0}\nSummary: {1}\nStacktrace:\n{2}", item.m_reportUUID, item.m_message, item.m_stackTrace);
						byte[] contents = File.ReadAllBytes(item.m_zipName);
						WWWForm wWWForm = new WWWForm();
						wWWForm.AddBinaryData("file", contents, "ReportedIssue.zip", "application/zip");
						UnityWebRequest unityRequest = UnityWebRequest.Post(SubmitURL, wWWForm);
						yield return unityRequest.SendWebRequest();
						int num = (int)unityRequest.responseCode;
						ExceptionLogger.LogDebug("Response code: {0}, ", num);
						if ((unityRequest.isHttpError && num != 404) || unityRequest.isNetworkError)
						{
							UnregisterLogsCallback();
							ExceptionLogger.LogError("Unable to send error report: {0}", unityRequest.error);
						}
						unityRequest.Dispose();
					}
					SafeDeleteFile(item.m_zipName);
				}
				m_recordedExceptions.m_records.Clear();
				if (!IsInDebugMode)
				{
					SafeDeleteFile(ExceptionReporterDataPath);
					CleanSavedFiles();
				}
				Busy = false;
			}
		}

		private void MergeRecordedExceptions()
		{
			lock (m_recordedExceptions)
			{
				if (m_recordedExceptions.m_backupRecords.Count > 0)
				{
					m_recordedExceptions.m_records.AddRange(m_recordedExceptions.m_backupRecords);
					m_recordedExceptions.m_backupRecords.Clear();
				}
			}
		}

		private void CleanSavedFiles()
		{
			ExceptionLogger.LogInfo("Trying to clean up the old exception zip files.");
			string[] files;
			try
			{
				files = Directory.GetFiles(m_exceptionDir, "exception-*");
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to get the file list from {0}: {1}", m_exceptionDir, ex.Message);
				return;
			}
			string[] array = files;
			foreach (string text in array)
			{
				try
				{
					File.Delete(text);
				}
				catch (Exception ex2)
				{
					ExceptionLogger.LogError("Failed to delete the zip file '{0}': {1}", text, ex2.Message);
				}
			}
		}

		private void SafeDeleteFile(string filepath)
		{
			if (string.IsNullOrEmpty(filepath))
			{
				return;
			}
			try
			{
				if (File.Exists(filepath))
				{
					File.Delete(filepath);
				}
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to delete the file '{0}': {1}", filepath, ex.Message);
			}
		}

		private IEnumerator SendJiraBug(ReportBuilder builder, string id, string pw)
		{
			bool skipReporterField = true;
			string reporterName = builder.EnteredBy;
			if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
			{
				ExceptionLogger.LogError("No Authentication for Jira access! Ignored.");
				yield break;
			}
			KAuthHeaderContents = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(id + ":" + pw));
			using (UnityWebRequest getRequest = CheckJiraUsername(builder.EnteredBy))
			{
				yield return getRequest.SendWebRequest();
				JsonList jsonList = Json.Deserialize(getRequest.downloadHandler.text) as JsonList;
				if (jsonList != null && jsonList.Count > 0)
				{
					JsonNode jsonNode = jsonList[0] as JsonNode;
					if (jsonNode != null)
					{
						string obj = jsonNode["name"] as string;
						string text = jsonNode["key"] as string;
						string text2 = jsonNode["displayName"] as string;
						if (obj.Equals(builder.EnteredBy, StringComparison.CurrentCultureIgnoreCase) || text.Equals(builder.EnteredBy, StringComparison.CurrentCultureIgnoreCase) || text2.Equals(builder.EnteredBy, StringComparison.CurrentCultureIgnoreCase))
						{
							skipReporterField = false;
							reporterName = jsonNode["name"] as string;
						}
					}
					else
					{
						ExceptionLogger.LogDebug("Jira returned null.");
					}
				}
				else
				{
					ExceptionLogger.LogError("Username search returned an error. Isnull?{0}\nRaw = <<{1}>>\n error = <<{2}>>, isHttpError={3} isNetworkError={4}", jsonList == null, getRequest.downloadHandler.text, getRequest.error, getRequest.isHttpError, getRequest.isNetworkError);
				}
			}
			string createJiraIssueJson = GetCreateJiraIssueJson(builder, reporterName, skipReporterField);
			string url = "https://api-jira.blizzard.com/rest/api/2/issue";
			using UnityWebRequest getRequest = CreateUnityWebPostRequest(url, createJiraIssueJson);
			yield return getRequest.SendWebRequest();
			string text3 = getRequest.downloadHandler.text;
			ExceptionLogger.LogDebug("Create a bug response: {0}", text3);
			if (getRequest.isNetworkError)
			{
				ExceptionLogger.LogWarning("Failed to create Jira issue - {0} - {1} - {2}", getRequest.error, getRequest.responseCode, getRequest.downloadHandler.text);
			}
			if (getRequest.isHttpError)
			{
				ExceptionLogger.LogError("Jira web request encountered an error trying to file a bug. Response: {0}", text3);
				yield break;
			}
			JsonNode jsonNode2 = Json.Deserialize(text3) as JsonNode;
			if (jsonNode2 == null || text3.Contains("errorMessages"))
			{
				ExceptionLogger.LogError("Failed to properly read JIRA response. Raw response: {0}", text3);
				yield break;
			}
			string issueKey = (string)jsonNode2["key"];
			if (skipReporterField && !string.IsNullOrEmpty(issueKey))
			{
				string url2 = $"{url}/{issueKey}";
				string json = $"{{\"update\": {{\"comment\": [{{\"add\": {{\"body\": \"{builder.EnteredBy}\"}}}}]}}}}";
				using UnityWebRequest unityWebRequest = CreateUnityWebPutRequest(url2, json);
				yield return unityWebRequest.SendWebRequest();
			}
			yield return new WaitForEndOfFrame();
			Screenshot.CaptureScreenshot(ReportBuilder.Settings.m_maxScreenshotWidths[builder.ReportType]);
			List<IMultipartFormSection> list = new List<IMultipartFormSection>();
			try
			{
				this.BeforeZipping?.Invoke();
				byte[] data = ZipUtil.BuildZipArchive(builder);
				this.AfterZipping?.Invoke();
				list.Add(new MultipartFormFileSection("file", data, "ReportedIssue.zip", "application/zip"));
			}
			catch (InsufficientMemoryException ex)
			{
				ExceptionLogger.LogError("Failed to zip because the file size is greater than {0}: {1}", builder.SizeLimit, ex.Message);
			}
			AddAttachableFiles(list, builder);
			string url3 = $"{url}/{issueKey}/attachments";
			using UnityWebRequest unityWebRequest = PostMultipart(url3, list);
			unityWebRequest.SetRequestHeader("Authorization", KAuthHeaderContents);
			unityWebRequest.SetRequestHeader("X-Atlassian-Token", "nocheck");
			yield return unityWebRequest.SendWebRequest();
			text3 = unityWebRequest.downloadHandler.text;
			ExceptionLogger.LogDebug("Attachment response: {0}", text3);
			if (unityWebRequest.isNetworkError)
			{
				ExceptionLogger.LogWarning("Failed to add attachment - {0} - {1} - {2}", unityWebRequest.error, unityWebRequest.responseCode, unityWebRequest.downloadHandler.text);
			}
			if (unityWebRequest.isHttpError)
			{
				ExceptionLogger.LogError("Jira web request encountered an error adding attachments. Response: {0}", text3);
				yield break;
			}
		}

		private static void AddAttachableFiles(List<IMultipartFormSection> parts, ReportBuilder builder)
		{
			if (builder.AttachableFiles == null)
			{
				return;
			}
			string[] attachableFiles = builder.AttachableFiles;
			for (int i = 0; i < attachableFiles.Length; i++)
			{
				string[] array = attachableFiles[i].Split('|');
				if (array.Length <= 2 || array[2] == "0")
				{
					continue;
				}
				string text = array[0];
				string text2 = null;
				string text3 = null;
				if (array.Length > 1)
				{
					text2 = array[1];
				}
				if (array.Length > 3)
				{
					text3 = array[3];
				}
				try
				{
					if (File.Exists(text) || ReportBuilder.Settings.m_readFileMethodCallback != null)
					{
						byte[] array2 = ((ReportBuilder.Settings.m_readFileMethodCallback == null) ? File.ReadAllBytes(text) : ReportBuilder.Settings.m_readFileMethodCallback(text));
						if (array2 != null)
						{
							parts.Add(new MultipartFormFileSection("file", array2, string.IsNullOrEmpty(text2) ? Path.GetFileName(text) : text2, string.IsNullOrEmpty(text3) ? "application/octet-stream" : text3));
							ExceptionLogger.LogInfo("Attached the loose file '{0}'", text);
						}
						else
						{
							ExceptionLogger.LogInfo("Skipped with no data: {0}", text);
						}
					}
				}
				catch (Exception ex)
				{
					ExceptionLogger.LogError("Failed to attach '{0}' for Jira with Error: {1}", text, ex.Message);
				}
			}
		}

		public static UnityWebRequest CheckJiraUsername(string userName)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest($"https://jira.blizzard.com/rest/api/latest/user/search?username={userName}", "GET");
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			unityWebRequest.SetRequestHeader("Authorization", KAuthHeaderContents);
			unityWebRequest.useHttpContinue = false;
			CustomCertHandler customCertHandler = (CustomCertHandler)(unityWebRequest.certificateHandler = new CustomCertHandler());
			return unityWebRequest;
		}

		public static UnityWebRequest CreateUnityWebPostRequest(string url, string json)
		{
			return CreateUnityWebRequestHelper(url, json, "POST");
		}

		public static UnityWebRequest CreateUnityWebPutRequest(string url, string json)
		{
			return CreateUnityWebRequestHelper(url, json, "PUT");
		}

		private static UnityWebRequest CreateUnityWebRequestHelper(string url, string json, string method)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(url, method);
			unityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json))
			{
				contentType = "application/json"
			};
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			unityWebRequest.SetRequestHeader("Authorization", KAuthHeaderContents);
			return unityWebRequest;
		}

		public static UnityWebRequest PostMultipart(string url, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] array = UnityWebRequest.GenerateBoundary();
			byte[] data = new byte[0];
			if (multipartFormSections != null && multipartFormSections.Count != 0)
			{
				byte[] bytes = Encoding.UTF8.GetBytes("\r\n");
				byte[] bytes2 = Encoding.UTF8.GetBytes("--");
				int num = 0;
				foreach (IMultipartFormSection multipartFormSection in multipartFormSections)
				{
					num += 128 + multipartFormSection.sectionData.Length;
				}
				List<byte> list = new List<byte>(num);
				StringBuilder stringBuilder = new StringBuilder();
				foreach (IMultipartFormSection multipartFormSection2 in multipartFormSections)
				{
					list.AddRange(bytes2);
					list.AddRange(array);
					list.AddRange(bytes);
					stringBuilder.Append("Content-Disposition: form-data");
					if (!string.IsNullOrEmpty(multipartFormSection2.sectionName))
					{
						stringBuilder.AppendFormat("; name=\"{0}\"", multipartFormSection2.sectionName);
					}
					if (!string.IsNullOrEmpty(multipartFormSection2.fileName))
					{
						stringBuilder.AppendFormat("; filename=\"{0}\"", multipartFormSection2.fileName);
					}
					stringBuilder.Append("\r\n");
					if (!string.IsNullOrEmpty(multipartFormSection2.contentType))
					{
						stringBuilder.AppendFormat("Content-Type: {0}\r\n", multipartFormSection2.contentType);
					}
					list.AddRange(Encoding.UTF8.GetBytes(stringBuilder.ToString()));
					stringBuilder = new StringBuilder();
					list.AddRange(bytes);
					list.AddRange(multipartFormSection2.sectionData);
					list.AddRange(bytes);
				}
				list.AddRange(bytes2);
				list.AddRange(array);
				list.AddRange(bytes2);
				data = list.ToArray();
				list.Clear();
			}
			return new UnityWebRequest(url, "POST")
			{
				downloadHandler = new DownloadHandlerBuffer(),
				uploadHandler = new UploadHandlerRaw(data)
				{
					contentType = $"multipart/form-data; boundary={Encoding.UTF8.GetString(array, 0, array.Length)}"
				}
			};
		}

		private string GetCreateJiraIssueJson(ReportBuilder builder, string reporterName, bool skipReporter = false)
		{
			JsonNode jsonNode = new JsonNode();
			JsonNode jsonNode2 = new JsonNode();
			JsonNode jsonNode3 = new JsonNode();
			JsonNode jsonNode4 = new JsonNode();
			JsonNode jsonNode5 = new JsonNode();
			JsonNode jsonNode6 = new JsonNode();
			JsonNode jsonNode7 = new JsonNode();
			JsonList jsonList = new JsonList();
			JsonList jsonList2 = new JsonList();
			jsonNode3.Add("key", builder.JiraProjectName);
			jsonNode4.Add("id", 1);
			jsonNode5.Add("name", builder.JiraComponentName);
			jsonList2.Add(jsonNode5);
			jsonNode6.Add("name", builder.JiraVersion);
			jsonList.Add(jsonNode6);
			jsonNode7.Add("name", reporterName);
			jsonNode2.Add("project", jsonNode3);
			jsonNode2.Add("summary", builder.Summary);
			jsonNode2.Add("description", builder.Comment + (string.IsNullOrEmpty(builder.Description) ? string.Empty : ("\n\n" + builder.Description)));
			jsonNode2.Add("issuetype", jsonNode4);
			jsonNode2.Add("components", jsonList2);
			jsonNode2.Add("versions", jsonList);
			if (!skipReporter)
			{
				jsonNode2.Add("reporter", jsonNode7);
			}
			jsonNode2.Add("customfield_10121", "To Be Filled in by Analyst");
			jsonNode.Add("fields", jsonNode2);
			string text = Json.Serialize(jsonNode);
			ExceptionLogger.LogDebug("Creating json for bug\n{0}", text);
			return text;
		}
	}
}
