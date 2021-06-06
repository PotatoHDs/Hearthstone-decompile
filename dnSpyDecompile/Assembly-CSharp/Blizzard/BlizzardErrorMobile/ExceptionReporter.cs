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
	// Token: 0x02001214 RID: 4628
	public class ExceptionReporter
	{
		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x0600CF9A RID: 53146 RVA: 0x003DCB3C File Offset: 0x003DAD3C
		// (remove) Token: 0x0600CF9B RID: 53147 RVA: 0x003DCB74 File Offset: 0x003DAD74
		public event Action BeforeZipping;

		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x0600CF9C RID: 53148 RVA: 0x003DCBAC File Offset: 0x003DADAC
		// (remove) Token: 0x0600CF9D RID: 53149 RVA: 0x003DCBE4 File Offset: 0x003DADE4
		public event Action AfterZipping;

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x0600CF9E RID: 53150 RVA: 0x003DCC19 File Offset: 0x003DAE19
		// (set) Token: 0x0600CF9F RID: 53151 RVA: 0x003DCC21 File Offset: 0x003DAE21
		public bool Busy { get; private set; }

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x0600CFA0 RID: 53152 RVA: 0x003DCC2A File Offset: 0x003DAE2A
		// (set) Token: 0x0600CFA1 RID: 53153 RVA: 0x003DCC32 File Offset: 0x003DAE32
		public bool IsInDebugMode { get; set; }

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x0600CFA2 RID: 53154 RVA: 0x003DCC3B File Offset: 0x003DAE3B
		public bool IsEnabledANRMonitor
		{
			get
			{
				return this.m_monitorANR != null && !this.m_monitorANR.IsTerminated;
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x0600CFA3 RID: 53155 RVA: 0x003DCC55 File Offset: 0x003DAE55
		// (set) Token: 0x0600CFA4 RID: 53156 RVA: 0x003DCC5D File Offset: 0x003DAE5D
		public bool ReportOnTheFly { get; set; } = true;

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x0600CFA5 RID: 53157 RVA: 0x003DCC66 File Offset: 0x003DAE66
		// (set) Token: 0x0600CFA6 RID: 53158 RVA: 0x003DCC6E File Offset: 0x003DAE6E
		public bool SendExceptions { get; set; } = true;

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x0600CFA7 RID: 53159 RVA: 0x003DCC77 File Offset: 0x003DAE77
		// (set) Token: 0x0600CFA8 RID: 53160 RVA: 0x003DCC7F File Offset: 0x003DAE7F
		public bool SendAsserts { get; set; }

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x0600CFA9 RID: 53161 RVA: 0x003DCC88 File Offset: 0x003DAE88
		// (set) Token: 0x0600CFAA RID: 53162 RVA: 0x003DCC90 File Offset: 0x003DAE90
		public bool SendErrors { get; set; }

		// Token: 0x0600CFAB RID: 53163 RVA: 0x003DCC99 File Offset: 0x003DAE99
		public static ExceptionReporter Get()
		{
			if (ExceptionReporter.s_instance == null)
			{
				ExceptionReporter.s_instance = new ExceptionReporter();
			}
			return ExceptionReporter.s_instance;
		}

		// Token: 0x0600CFAC RID: 53164 RVA: 0x003DCCB1 File Offset: 0x003DAEB1
		public void Initialize(string installPath, MonoBehaviour monoBehaviour)
		{
			this.Initialize(installPath, null, monoBehaviour);
		}

		// Token: 0x0600CFAD RID: 53165 RVA: 0x003DCCBC File Offset: 0x003DAEBC
		public void Initialize(string installPath, IExceptionLogger logger, MonoBehaviour monoBehaviour)
		{
			ExceptionLogger.SetLogger(logger);
			this.m_monoBehaviour = monoBehaviour;
			ExceptionLogger.LogInfo("Version {0}", new object[]
			{
				VersionInfo.VERSION
			});
			ExceptionLogger.LogInfo("ScreenshotPath: {0}", new object[]
			{
				Screenshot.ScreenshotPath
			});
			Screenshot.RemoveScreenshot();
			this.m_exceptionDir = Path.Combine(installPath, "Exceptions").Replace("\\", "/");
			if (!Directory.Exists(this.m_exceptionDir))
			{
				try
				{
					Directory.CreateDirectory(this.m_exceptionDir);
				}
				catch (Exception ex)
				{
					ExceptionLogger.LogError("Failed to create the folder '{0}' for exceptions: {1}", new object[]
					{
						this.m_exceptionDir,
						ex.Message
					});
					this.UnregisterLogsCallback();
					return;
				}
			}
			this.DeserializeRecordedExceptions();
			CallbackManager.RegisterExceptionHandler();
			this.RegisterLogsCallback();
			ExceptionLogger.LogInfo("User UUID: {0}", new object[]
			{
				ExceptionReporter.s_userUUID
			});
			ReportBuilder.ApplicationUnityVersion = Application.unityVersion;
			ReportBuilder.ApplicationGenuine = Application.genuine;
			this.CreateCoroutine(this.TryReportPreviousExceptions());
		}

		// Token: 0x0600CFAE RID: 53166 RVA: 0x003DCDCC File Offset: 0x003DAFCC
		public bool SetSettings(ExceptionSettings settings)
		{
			try
			{
				ReportBuilder.Settings = settings;
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to set Settings: {0}", new object[]
				{
					ex.Message
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600CFAF RID: 53167 RVA: 0x003DCE14 File Offset: 0x003DB014
		public void ReportCaughtException(string message, string stackTrace)
		{
			this.RecordException("[Caught] " + message, stackTrace, false, ExceptionSettings.ReportType.EXCEPTION);
		}

		// Token: 0x0600CFB0 RID: 53168 RVA: 0x003DCE2A File Offset: 0x003DB02A
		public void RecordException(string message, string stackTrace)
		{
			this.RecordException(message, stackTrace, false, ExceptionSettings.ReportType.EXCEPTION);
		}

		// Token: 0x0600CFB1 RID: 53169 RVA: 0x003DCE36 File Offset: 0x003DB036
		public void RecordException(string message, string stackTrace, bool recordOnly)
		{
			this.RecordException(message, stackTrace, recordOnly, ExceptionSettings.ReportType.EXCEPTION);
		}

		// Token: 0x0600CFB2 RID: 53170 RVA: 0x003DCE44 File Offset: 0x003DB044
		public void RecordException(string message, string stackTrace, bool recordOnly, ExceptionSettings.ReportType reportType)
		{
			if (!this.SendExceptions)
			{
				ExceptionLogger.LogInfo("Exception has been reported but skipped because SendException is off.", Array.Empty<object>());
				return;
			}
			ReportBuilder reportBuilder;
			try
			{
				reportBuilder = ReportBuilder.BuildExceptionReport(message, stackTrace, reportType);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to create ExceptionReport: {0}", new object[]
				{
					ex.Message
				});
				return;
			}
			if (this.m_seenHashes.Contains(reportBuilder.Hash))
			{
				ExceptionLogger.LogDebug("Skipped same Exception...", Array.Empty<object>());
				return;
			}
			ExceptionLogger.LogInfo("Record an exception {0}-{1}\nAt:\n{2}", new object[]
			{
				message,
				reportBuilder.Hash,
				stackTrace
			});
			RecordedExceptions recordedExceptions = this.m_recordedExceptions;
			lock (recordedExceptions)
			{
				if (this.m_recordedExceptions.m_records.Count + this.m_recordedExceptions.m_backupRecords.Count >= 20)
				{
					ExceptionLogger.LogWarning("It reached the maximum records. Skipped.", Array.Empty<object>());
					return;
				}
			}
			this.m_seenHashes.Add(reportBuilder.Hash);
			Screenshot.RemoveScreenshot();
			if (!this.ReportOnTheFly || recordOnly)
			{
				this.MakeZipAndRecordInner(reportBuilder);
				return;
			}
			if (this.CreateCoroutine(this.MakeZipAndRecord(reportBuilder, this.ReportOnTheFly)) == null)
			{
				this.MakeZipAndRecordInner(reportBuilder);
			}
		}

		// Token: 0x0600CFB3 RID: 53171 RVA: 0x003DCF90 File Offset: 0x003DB190
		public void ClearExceptionHashes()
		{
			this.ReportExceptions();
			this.m_seenHashes.Clear();
		}

		// Token: 0x0600CFB4 RID: 53172 RVA: 0x003DCFA4 File Offset: 0x003DB1A4
		public bool ReportJiraBug(string summary, string enteredBy, string description, string id, string pw)
		{
			if (string.IsNullOrEmpty(summary))
			{
				ExceptionLogger.LogError("'summary' should have a value.", Array.Empty<object>());
				return false;
			}
			if (string.IsNullOrEmpty(enteredBy))
			{
				ExceptionLogger.LogError("'enteredBy' should have a value.", Array.Empty<object>());
				return false;
			}
			ReportBuilder builder;
			try
			{
				builder = ReportBuilder.BuildBugReport(summary, enteredBy, description);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to create IssueReport: {0}", new object[]
				{
					ex.Message
				});
				return false;
			}
			ExceptionLogger.LogInfo("Create a bug: {0} {1} {2}", new object[]
			{
				summary,
				enteredBy,
				description
			});
			this.CreateCoroutine(this.SendJiraBug(builder, id, pw));
			return true;
		}

		// Token: 0x0600CFB5 RID: 53173 RVA: 0x003DD050 File Offset: 0x003DB250
		public bool EnableANRMonitor(float waitLimitSeconds)
		{
			return this.EnableANRMonitor(waitLimitSeconds, 0.05f);
		}

		// Token: 0x0600CFB6 RID: 53174 RVA: 0x003DD060 File Offset: 0x003DB260
		public bool EnableANRMonitor(float waitLimitSeconds, float throttle)
		{
			if (this.m_monoBehaviour == null)
			{
				ExceptionLogger.LogError("EnableANRMonitor can be used after initialization only.", Array.Empty<object>());
				return false;
			}
			if (this.m_monitorANR == null)
			{
				this.m_monitorANR = new ANRMonitor(waitLimitSeconds, throttle, this.m_monoBehaviour);
				this.m_monitorANR.Detected += this.RecordANRException;
				this.m_monitorANR.FirstUpdateAfterANR += this.ReportANRException;
			}
			else
			{
				this.m_monitorANR.SetWaitSeconds(waitLimitSeconds, throttle);
			}
			return true;
		}

		// Token: 0x0600CFB7 RID: 53175 RVA: 0x003DD0E5 File Offset: 0x003DB2E5
		public void OnApplicationPause(bool pauseStatus)
		{
			ANRMonitor monitorANR = this.m_monitorANR;
			if (monitorANR == null)
			{
				return;
			}
			monitorANR.OnPause(pauseStatus);
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x0600CFB8 RID: 53176 RVA: 0x003DD0F8 File Offset: 0x003DB2F8
		private string SubmitURL
		{
			get
			{
				return ExceptionReporter.s_baseSubmitURL + ReportBuilder.Settings.m_projectID;
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x0600CFB9 RID: 53177 RVA: 0x003DD114 File Offset: 0x003DB314
		private string GetZipName
		{
			get
			{
				string exceptionDir = this.m_exceptionDir;
				string format = "exception-{0}.zip";
				int zipFileCount = this.m_zipFileCount;
				this.m_zipFileCount = zipFileCount + 1;
				return Path.Combine(exceptionDir, string.Format(format, zipFileCount)).Replace("\\", "/");
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x0600CFBA RID: 53178 RVA: 0x003DD15B File Offset: 0x003DB35B
		private string ExceptionReporterDataPath
		{
			get
			{
				return Path.Combine(this.m_exceptionDir, "ExceptionReporter.json").Replace("\\", "/");
			}
		}

		// Token: 0x0600CFBB RID: 53179 RVA: 0x003DD17C File Offset: 0x003DB37C
		private Coroutine CreateCoroutine(IEnumerator routine)
		{
			Coroutine result;
			try
			{
				result = this.m_monoBehaviour.StartCoroutine(routine);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to start coroutine: {0}", new object[]
				{
					ex.Message
				});
				result = null;
			}
			return result;
		}

		// Token: 0x0600CFBC RID: 53180 RVA: 0x003DD1C8 File Offset: 0x003DB3C8
		private void RegisterLogsCallback()
		{
			if (!this.m_started)
			{
				ExceptionLogger.LogDebug("Registering error reporter callback", Array.Empty<object>());
				Application.logMessageReceived += this.LogMessageCallback;
				this.m_started = true;
			}
		}

		// Token: 0x0600CFBD RID: 53181 RVA: 0x003DD1F9 File Offset: 0x003DB3F9
		private void UnregisterLogsCallback()
		{
			if (this.m_started)
			{
				ExceptionLogger.LogDebug("Unregistering error reporter callback", Array.Empty<object>());
				Application.logMessageReceived -= this.LogMessageCallback;
				this.m_started = false;
			}
		}

		// Token: 0x0600CFBE RID: 53182 RVA: 0x003DD22C File Offset: 0x003DB42C
		private void LogMessageCallback(string message, string stackTrace, LogType logType)
		{
			switch (logType)
			{
			case LogType.Error:
				if (this.SendErrors && !ExceptionLogger.IsExceptionLoggerError(message))
				{
					this.RecordException(message, stackTrace);
					return;
				}
				break;
			case LogType.Assert:
				if (this.SendAsserts)
				{
					this.RecordException(message, stackTrace);
					return;
				}
				break;
			case LogType.Warning:
			case LogType.Log:
				break;
			case LogType.Exception:
				if (this.SendExceptions)
				{
					this.RecordException(message, stackTrace);
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600CFBF RID: 53183 RVA: 0x003DD28E File Offset: 0x003DB48E
		private IEnumerator TryReportPreviousExceptions()
		{
			yield return new WaitUntil(() => ReportBuilder.Settings != null);
			this.ClearExceptionHashes();
			yield break;
		}

		// Token: 0x0600CFC0 RID: 53184 RVA: 0x003DD29D File Offset: 0x003DB49D
		private void RecordANRException()
		{
			this.RecordException(ExceptionReporter.s_titleOfANRException, "", true, ExceptionSettings.ReportType.ANR);
		}

		// Token: 0x0600CFC1 RID: 53185 RVA: 0x003DD2B1 File Offset: 0x003DB4B1
		private void ReportANRException()
		{
			this.m_monoBehaviour.StartCoroutine(this.AddScreenshotAndReport());
		}

		// Token: 0x0600CFC2 RID: 53186 RVA: 0x003DD2C5 File Offset: 0x003DB4C5
		private IEnumerator AddScreenshotAndReport()
		{
			if (!string.IsNullOrEmpty(this.m_zipFileForANR) && File.Exists(this.m_zipFileForANR))
			{
				ExceptionLogger.LogDebug("ANR occurred before", Array.Empty<object>());
				yield return new WaitForEndOfFrame();
				if (!Screenshot.CaptureScreenshot(ReportBuilder.Settings.m_maxScreenshotWidths[ExceptionSettings.ReportType.ANR]))
				{
					this.m_zipFileForANR = string.Empty;
					yield break;
				}
				ZipUtil.AddFileToZip(Screenshot.ScreenshotPath, this.m_zipFileForANR, ReportBuilder.Settings.m_maxZipSizeLimits[ExceptionSettings.ReportType.ANR]);
				this.m_zipFileForANR = string.Empty;
				Screenshot.RemoveScreenshot();
				this.ReportExceptions();
			}
			yield break;
		}

		// Token: 0x0600CFC3 RID: 53187 RVA: 0x003DD2D4 File Offset: 0x003DB4D4
		private void ReportExceptions()
		{
			RecordedExceptions recordedExceptions = this.m_recordedExceptions;
			lock (recordedExceptions)
			{
				this.ProcessPreviousExceptions();
				if (!this.m_started || this.m_recordedExceptions.m_records.Count + this.m_recordedExceptions.m_backupRecords.Count == 0)
				{
					return;
				}
			}
			this.m_monoBehaviour.StartCoroutine(this.SendInner());
		}

		// Token: 0x0600CFC4 RID: 53188 RVA: 0x003DD354 File Offset: 0x003DB554
		private void ProcessPreviousExceptions()
		{
			if (this.m_processedPreviousExceptions)
			{
				return;
			}
			ExceptionLogger.LogInfo("Checking the exceptions from previous launch.", Array.Empty<object>());
			this.m_recordedExceptions.m_lastReadTimeLog = CallbackManager.CatchCrashCaptureFromLog(this.m_recordedExceptions.m_lastReadTimeLog, Application.identifier);
			this.SerializeRecordedExceptions();
			this.m_processedPreviousExceptions = true;
		}

		// Token: 0x0600CFC5 RID: 53189 RVA: 0x003DD3A8 File Offset: 0x003DB5A8
		private void DeserializeRecordedExceptions()
		{
			try
			{
				if (File.Exists(this.ExceptionReporterDataPath))
				{
					RecordedExceptions recordedExceptions = this.m_recordedExceptions;
					lock (recordedExceptions)
					{
						string json = File.ReadAllText(this.ExceptionReporterDataPath);
						this.m_recordedExceptions = JsonUtility.FromJson<RecordedExceptions>(json);
						File.Delete(this.ExceptionReporterDataPath);
						this.MergeRecordedExceptions();
						ExceptionLogger.LogInfo("Loaded exception data from '{0}'", new object[]
						{
							this.ExceptionReporterDataPath
						});
					}
				}
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to read exception data: {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x0600CFC6 RID: 53190 RVA: 0x003DD45C File Offset: 0x003DB65C
		private void SerializeRecordedExceptions()
		{
			try
			{
				RecordedExceptions recordedExceptions = this.m_recordedExceptions;
				lock (recordedExceptions)
				{
					string contents = JsonUtility.ToJson(this.m_recordedExceptions);
					File.WriteAllText(this.ExceptionReporterDataPath, contents);
					ExceptionLogger.LogInfo("Saved exception data(Count: {0}) to '{1}'", new object[]
					{
						this.m_recordedExceptions.m_records.Count,
						this.ExceptionReporterDataPath
					});
				}
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to write exception data to '{0}': {1}", new object[]
				{
					this.ExceptionReporterDataPath,
					ex.Message
				});
			}
		}

		// Token: 0x0600CFC7 RID: 53191 RVA: 0x003DD514 File Offset: 0x003DB714
		private void MakeZipAndRecordInner(ReportBuilder builder)
		{
			string text = this.GetZipName;
			try
			{
				Action beforeZipping = this.BeforeZipping;
				if (beforeZipping != null)
				{
					beforeZipping();
				}
				byte[] bytes = ZipUtil.BuildZipArchive(builder);
				Action afterZipping = this.AfterZipping;
				if (afterZipping != null)
				{
					afterZipping();
				}
				File.WriteAllBytes(text, bytes);
			}
			catch (InsufficientMemoryException ex)
			{
				ExceptionLogger.LogError("Failed to zip because the file size is too big: {0}", new object[]
				{
					ex.Message
				});
				text = string.Empty;
			}
			catch (Exception ex2)
			{
				ExceptionLogger.LogError("Failed to zip: {0}", new object[]
				{
					ex2.Message
				});
			}
			RecordedExceptions recordedExceptions = this.m_recordedExceptions;
			lock (recordedExceptions)
			{
				if (!this.Busy)
				{
					this.m_recordedExceptions.m_records.Add(new ExceptionStruct(builder.Hash, builder.Summary, builder.StackTrace, builder.ReportUUID, text));
				}
				else
				{
					this.m_recordedExceptions.m_backupRecords.Add(new ExceptionStruct(builder.Hash, builder.Summary, builder.StackTrace, builder.ReportUUID, text));
				}
				if (builder.ReportType == ExceptionSettings.ReportType.ANR)
				{
					this.m_zipFileForANR = text;
				}
			}
			this.SerializeRecordedExceptions();
		}

		// Token: 0x0600CFC8 RID: 53192 RVA: 0x003DD660 File Offset: 0x003DB860
		private IEnumerator MakeZipAndRecord(ReportBuilder builder, bool reportNow)
		{
			yield return new WaitForEndOfFrame();
			if (builder.ReportType != ExceptionSettings.ReportType.ANR)
			{
				Screenshot.CaptureScreenshot(ReportBuilder.Settings.m_maxScreenshotWidths[builder.ReportType]);
			}
			this.MakeZipAndRecordInner(builder);
			if (reportNow)
			{
				while (this.Busy)
				{
					yield return new WaitForSeconds(0.5f);
				}
				this.ReportExceptions();
			}
			yield break;
		}

		// Token: 0x0600CFC9 RID: 53193 RVA: 0x003DD67D File Offset: 0x003DB87D
		private IEnumerator SendInner()
		{
			ExceptionLogger.LogDebug("SendInner started", Array.Empty<object>());
			while (this.Busy)
			{
				yield return new WaitForSeconds(0.5f);
			}
			this.MergeRecordedExceptions();
			RecordedExceptions obj = this.m_recordedExceptions;
			lock (obj)
			{
				this.Busy = true;
				foreach (ExceptionStruct item in this.m_recordedExceptions.m_records)
				{
					if (this.m_started && File.Exists(item.m_zipName))
					{
						if (this.IsInDebugMode)
						{
							continue;
						}
						ExceptionLogger.LogInfo("Reporting exception: ReportUUID {0}\nSummary: {1}\nStacktrace:\n{2}", new object[]
						{
							item.m_reportUUID,
							item.m_message,
							item.m_stackTrace
						});
						byte[] contents = File.ReadAllBytes(item.m_zipName);
						WWWForm wwwform = new WWWForm();
						wwwform.AddBinaryData("file", contents, "ReportedIssue.zip", "application/zip");
						UnityWebRequest unityRequest = UnityWebRequest.Post(this.SubmitURL, wwwform);
						yield return unityRequest.SendWebRequest();
						int num = (int)unityRequest.responseCode;
						ExceptionLogger.LogDebug("Response code: {0}, ", new object[]
						{
							num
						});
						if ((unityRequest.isHttpError && num != 404) || unityRequest.isNetworkError)
						{
							this.UnregisterLogsCallback();
							ExceptionLogger.LogError("Unable to send error report: {0}", new object[]
							{
								unityRequest.error
							});
						}
						unityRequest.Dispose();
						unityRequest = null;
					}
					this.SafeDeleteFile(item.m_zipName);
					item = null;
				}
				List<ExceptionStruct>.Enumerator enumerator = default(List<ExceptionStruct>.Enumerator);
				this.m_recordedExceptions.m_records.Clear();
				if (!this.IsInDebugMode)
				{
					this.SafeDeleteFile(this.ExceptionReporterDataPath);
					this.CleanSavedFiles();
				}
				this.Busy = false;
			}
			obj = null;
			yield break;
			yield break;
		}

		// Token: 0x0600CFCA RID: 53194 RVA: 0x003DD68C File Offset: 0x003DB88C
		private void MergeRecordedExceptions()
		{
			RecordedExceptions recordedExceptions = this.m_recordedExceptions;
			lock (recordedExceptions)
			{
				if (this.m_recordedExceptions.m_backupRecords.Count > 0)
				{
					this.m_recordedExceptions.m_records.AddRange(this.m_recordedExceptions.m_backupRecords);
					this.m_recordedExceptions.m_backupRecords.Clear();
				}
			}
		}

		// Token: 0x0600CFCB RID: 53195 RVA: 0x003DD704 File Offset: 0x003DB904
		private void CleanSavedFiles()
		{
			ExceptionLogger.LogInfo("Trying to clean up the old exception zip files.", Array.Empty<object>());
			string[] files;
			try
			{
				files = Directory.GetFiles(this.m_exceptionDir, "exception-*");
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to get the file list from {0}: {1}", new object[]
				{
					this.m_exceptionDir,
					ex.Message
				});
				return;
			}
			foreach (string text in files)
			{
				try
				{
					File.Delete(text);
				}
				catch (Exception ex2)
				{
					ExceptionLogger.LogError("Failed to delete the zip file '{0}': {1}", new object[]
					{
						text,
						ex2.Message
					});
				}
			}
		}

		// Token: 0x0600CFCC RID: 53196 RVA: 0x003DD7B8 File Offset: 0x003DB9B8
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
				ExceptionLogger.LogError("Failed to delete the file '{0}': {1}", new object[]
				{
					filepath,
					ex.Message
				});
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x0600CFCD RID: 53197 RVA: 0x003DD810 File Offset: 0x003DBA10
		// (set) Token: 0x0600CFCE RID: 53198 RVA: 0x003DD817 File Offset: 0x003DBA17
		private static string KAuthHeaderContents { get; set; }

		// Token: 0x0600CFCF RID: 53199 RVA: 0x003DD81F File Offset: 0x003DBA1F
		private IEnumerator SendJiraBug(ReportBuilder builder, string id, string pw)
		{
			bool skipReporterField = true;
			string reporterName = builder.EnteredBy;
			if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
			{
				ExceptionLogger.LogError("No Authentication for Jira access! Ignored.", Array.Empty<object>());
				yield break;
			}
			ExceptionReporter.KAuthHeaderContents = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(id + ":" + pw));
			using (UnityWebRequest getRequest = ExceptionReporter.CheckJiraUsername(builder.EnteredBy))
			{
				yield return getRequest.SendWebRequest();
				JsonList jsonList = Json.Deserialize(getRequest.downloadHandler.text) as JsonList;
				if (jsonList != null && jsonList.Count > 0)
				{
					JsonNode jsonNode = jsonList[0] as JsonNode;
					if (jsonNode != null)
					{
						string text = jsonNode["name"] as string;
						string text2 = jsonNode["key"] as string;
						string text3 = jsonNode["displayName"] as string;
						if (text.Equals(builder.EnteredBy, StringComparison.CurrentCultureIgnoreCase) || text2.Equals(builder.EnteredBy, StringComparison.CurrentCultureIgnoreCase) || text3.Equals(builder.EnteredBy, StringComparison.CurrentCultureIgnoreCase))
						{
							skipReporterField = false;
							reporterName = (jsonNode["name"] as string);
						}
					}
					else
					{
						ExceptionLogger.LogDebug("Jira returned null.", Array.Empty<object>());
					}
				}
				else
				{
					ExceptionLogger.LogError("Username search returned an error. Isnull?{0}\nRaw = <<{1}>>\n error = <<{2}>>, isHttpError={3} isNetworkError={4}", new object[]
					{
						jsonList == null,
						getRequest.downloadHandler.text,
						getRequest.error,
						getRequest.isHttpError,
						getRequest.isNetworkError
					});
				}
			}
			UnityWebRequest getRequest = null;
			string createJiraIssueJson = this.GetCreateJiraIssueJson(builder, reporterName, skipReporterField);
			string url = "https://api-jira.blizzard.com/rest/api/2/issue";
			using (UnityWebRequest getRequest = ExceptionReporter.CreateUnityWebPostRequest(url, createJiraIssueJson))
			{
				yield return getRequest.SendWebRequest();
				string text4 = getRequest.downloadHandler.text;
				ExceptionLogger.LogDebug("Create a bug response: {0}", new object[]
				{
					text4
				});
				if (getRequest.isNetworkError)
				{
					ExceptionLogger.LogWarning("Failed to create Jira issue - {0} - {1} - {2}", new object[]
					{
						getRequest.error,
						getRequest.responseCode,
						getRequest.downloadHandler.text
					});
				}
				if (getRequest.isHttpError)
				{
					ExceptionLogger.LogError("Jira web request encountered an error trying to file a bug. Response: {0}", new object[]
					{
						text4
					});
					yield break;
				}
				JsonNode jsonNode2 = Json.Deserialize(text4) as JsonNode;
				if (jsonNode2 == null || text4.Contains("errorMessages"))
				{
					ExceptionLogger.LogError("Failed to properly read JIRA response. Raw response: {0}", new object[]
					{
						text4
					});
					yield break;
				}
				string issueKey = (string)jsonNode2["key"];
				UnityWebRequest editRequest;
				if (skipReporterField && !string.IsNullOrEmpty(issueKey))
				{
					string url2 = string.Format("{0}/{1}", url, issueKey);
					string json = string.Format("{{\"update\": {{\"comment\": [{{\"add\": {{\"body\": \"{0}\"}}}}]}}}}", builder.EnteredBy);
					using (UnityWebRequest editRequest = ExceptionReporter.CreateUnityWebPutRequest(url2, json))
					{
						yield return editRequest.SendWebRequest();
					}
					editRequest = null;
				}
				yield return new WaitForEndOfFrame();
				Screenshot.CaptureScreenshot(ReportBuilder.Settings.m_maxScreenshotWidths[builder.ReportType]);
				List<IMultipartFormSection> list = new List<IMultipartFormSection>();
				try
				{
					Action beforeZipping = this.BeforeZipping;
					if (beforeZipping != null)
					{
						beforeZipping();
					}
					byte[] data = ZipUtil.BuildZipArchive(builder);
					Action afterZipping = this.AfterZipping;
					if (afterZipping != null)
					{
						afterZipping();
					}
					list.Add(new MultipartFormFileSection("file", data, "ReportedIssue.zip", "application/zip"));
				}
				catch (InsufficientMemoryException ex)
				{
					ExceptionLogger.LogError("Failed to zip because the file size is greater than {0}: {1}", new object[]
					{
						builder.SizeLimit,
						ex.Message
					});
				}
				ExceptionReporter.AddAttachableFiles(list, builder);
				string url3 = string.Format("{0}/{1}/attachments", url, issueKey);
				using (UnityWebRequest editRequest = ExceptionReporter.PostMultipart(url3, list))
				{
					editRequest.SetRequestHeader("Authorization", ExceptionReporter.KAuthHeaderContents);
					editRequest.SetRequestHeader("X-Atlassian-Token", "nocheck");
					yield return editRequest.SendWebRequest();
					text4 = editRequest.downloadHandler.text;
					ExceptionLogger.LogDebug("Attachment response: {0}", new object[]
					{
						text4
					});
					if (editRequest.isNetworkError)
					{
						ExceptionLogger.LogWarning("Failed to add attachment - {0} - {1} - {2}", new object[]
						{
							editRequest.error,
							editRequest.responseCode,
							editRequest.downloadHandler.text
						});
					}
					if (editRequest.isHttpError)
					{
						ExceptionLogger.LogError("Jira web request encountered an error adding attachments. Response: {0}", new object[]
						{
							text4
						});
						yield break;
					}
				}
				editRequest = null;
				issueKey = null;
			}
			getRequest = null;
			yield break;
			yield break;
		}

		// Token: 0x0600CFD0 RID: 53200 RVA: 0x003DD844 File Offset: 0x003DBA44
		private static void AddAttachableFiles(List<IMultipartFormSection> parts, ReportBuilder builder)
		{
			if (builder.AttachableFiles == null)
			{
				return;
			}
			string[] attachableFiles = builder.AttachableFiles;
			for (int i = 0; i < attachableFiles.Length; i++)
			{
				string[] array = attachableFiles[i].Split(new char[]
				{
					'|'
				});
				if (array.Length > 2 && !(array[2] == "0"))
				{
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
							byte[] array2;
							if (ReportBuilder.Settings.m_readFileMethodCallback != null)
							{
								array2 = ReportBuilder.Settings.m_readFileMethodCallback(text);
							}
							else
							{
								array2 = File.ReadAllBytes(text);
							}
							if (array2 != null)
							{
								parts.Add(new MultipartFormFileSection("file", array2, string.IsNullOrEmpty(text2) ? Path.GetFileName(text) : text2, string.IsNullOrEmpty(text3) ? "application/octet-stream" : text3));
								ExceptionLogger.LogInfo("Attached the loose file '{0}'", new object[]
								{
									text
								});
							}
							else
							{
								ExceptionLogger.LogInfo("Skipped with no data: {0}", new object[]
								{
									text
								});
							}
						}
					}
					catch (Exception ex)
					{
						ExceptionLogger.LogError("Failed to attach '{0}' for Jira with Error: {1}", new object[]
						{
							text,
							ex.Message
						});
					}
				}
			}
		}

		// Token: 0x0600CFD1 RID: 53201 RVA: 0x003DD9A0 File Offset: 0x003DBBA0
		public static UnityWebRequest CheckJiraUsername(string userName)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(string.Format("https://jira.blizzard.com/rest/api/latest/user/search?username={0}", userName), "GET");
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			unityWebRequest.SetRequestHeader("Authorization", ExceptionReporter.KAuthHeaderContents);
			unityWebRequest.useHttpContinue = false;
			ExceptionReporter.CustomCertHandler certificateHandler = new ExceptionReporter.CustomCertHandler();
			unityWebRequest.certificateHandler = certificateHandler;
			return unityWebRequest;
		}

		// Token: 0x0600CFD2 RID: 53202 RVA: 0x003DD9F1 File Offset: 0x003DBBF1
		public static UnityWebRequest CreateUnityWebPostRequest(string url, string json)
		{
			return ExceptionReporter.CreateUnityWebRequestHelper(url, json, "POST");
		}

		// Token: 0x0600CFD3 RID: 53203 RVA: 0x003DD9FF File Offset: 0x003DBBFF
		public static UnityWebRequest CreateUnityWebPutRequest(string url, string json)
		{
			return ExceptionReporter.CreateUnityWebRequestHelper(url, json, "PUT");
		}

		// Token: 0x0600CFD4 RID: 53204 RVA: 0x003DDA10 File Offset: 0x003DBC10
		private static UnityWebRequest CreateUnityWebRequestHelper(string url, string json, string method)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(url, method);
			unityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json))
			{
				contentType = "application/json"
			};
			unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
			unityWebRequest.SetRequestHeader("Authorization", ExceptionReporter.KAuthHeaderContents);
			return unityWebRequest;
		}

		// Token: 0x0600CFD5 RID: 53205 RVA: 0x003DDA60 File Offset: 0x003DBC60
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
					contentType = string.Format("multipart/form-data; boundary={0}", Encoding.UTF8.GetString(array, 0, array.Length))
				}
			};
		}

		// Token: 0x0600CFD6 RID: 53206 RVA: 0x003DDC8C File Offset: 0x003DBE8C
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
			ExceptionLogger.LogDebug("Creating json for bug\n{0}", new object[]
			{
				text
			});
			return text;
		}

		// Token: 0x0400A22B RID: 41515
		private const int DEFAULT_MAX_RECORDS = 20;

		// Token: 0x0400A22C RID: 41516
		private const float DEFAULT_ANR_THROTTLE = 0.05f;

		// Token: 0x0400A22D RID: 41517
		private static readonly string s_userUUID = SystemInfo.deviceUniqueIdentifier.ToString();

		// Token: 0x0400A22E RID: 41518
		private static readonly string s_baseSubmitURL = "http://iir.blizzard.com:3724/submit/";

		// Token: 0x0400A22F RID: 41519
		private static readonly string s_titleOfANRException = "ANR detected";

		// Token: 0x0400A232 RID: 41522
		private static ExceptionReporter s_instance;

		// Token: 0x0400A233 RID: 41523
		private bool m_started;

		// Token: 0x0400A234 RID: 41524
		private bool m_processedPreviousExceptions;

		// Token: 0x0400A235 RID: 41525
		private string m_zipFileForANR;

		// Token: 0x0400A236 RID: 41526
		private int m_zipFileCount;

		// Token: 0x0400A237 RID: 41527
		private string m_exceptionDir;

		// Token: 0x0400A238 RID: 41528
		private MonoBehaviour m_monoBehaviour;

		// Token: 0x0400A239 RID: 41529
		private RecordedExceptions m_recordedExceptions = new RecordedExceptions();

		// Token: 0x0400A23A RID: 41530
		private HashSet<string> m_seenHashes = new HashSet<string>();

		// Token: 0x0400A23B RID: 41531
		private ANRMonitor m_monitorANR;

		// Token: 0x02002954 RID: 10580
		public class CustomCertHandler : CertificateHandler
		{
			// Token: 0x06013E7F RID: 81535 RVA: 0x000052EC File Offset: 0x000034EC
			protected override bool ValidateCertificate(byte[] certificateData)
			{
				return true;
			}
		}
	}
}
