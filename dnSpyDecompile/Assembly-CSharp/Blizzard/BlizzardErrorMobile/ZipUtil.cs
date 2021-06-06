using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x0200121B RID: 4635
	internal static class ZipUtil
	{
		// Token: 0x0600D032 RID: 53298 RVA: 0x003DED60 File Offset: 0x003DCF60
		public static byte[] BuildZipArchive(ReportBuilder builder)
		{
			MemoryStream memoryStream = new MemoryStream();
			ZipConstants.DefaultCodePage = 0;
			ZipOutputStream zipOutputStream = new ZipOutputStream(memoryStream, 4096);
			zipOutputStream.SetLevel(6);
			byte[] bytes = new byte[0];
			if (!string.IsNullOrEmpty(builder.Markup))
			{
				bytes = Encoding.UTF8.GetBytes(builder.Markup);
				ZipUtil.SafeAddToZip(zipOutputStream, bytes, "ReportedIssue.xml");
			}
			ZipUtil.AddLogFiles(zipOutputStream, builder);
			ZipUtil.AddAttachableFiles(zipOutputStream, builder);
			zipOutputStream.IsStreamOwner = false;
			zipOutputStream.Close();
			memoryStream.Flush();
			if (memoryStream.Length > (long)builder.SizeLimit)
			{
				memoryStream.Close();
				throw new InsufficientMemoryException("The zip file is too large");
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600D033 RID: 53299 RVA: 0x003DEE04 File Offset: 0x003DD004
		public static void AddFileToZip(string filename, string zipFile, int sizeLimit)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (FileStream fileStream = new FileStream(zipFile, FileMode.Open))
			{
				ZipInputStream zipInputStream = new ZipInputStream(fileStream);
				ZipConstants.DefaultCodePage = 0;
				ZipOutputStream zipOutputStream = new ZipOutputStream(memoryStream, 4096);
				byte[] array = new byte[4096];
				ZipEntry nextEntry;
				while ((nextEntry = zipInputStream.GetNextEntry()) != null)
				{
					zipOutputStream.PutNextEntry(new ZipEntry(nextEntry.Name));
					int count;
					while ((count = zipInputStream.Read(array, 0, array.Length)) > 0)
					{
						zipOutputStream.Write(array, 0, count);
					}
					zipOutputStream.CloseEntry();
				}
				ZipUtil.SafeAddToZip(zipOutputStream, File.ReadAllBytes(filename), new FileInfo(filename).Name);
				zipOutputStream.IsStreamOwner = false;
				zipOutputStream.Close();
				zipInputStream.Close();
				memoryStream.Flush();
				if (memoryStream.Length > (long)sizeLimit)
				{
					memoryStream.Close();
					throw new InsufficientMemoryException("The zip file is too large");
				}
			}
			try
			{
				File.Delete(zipFile);
				File.WriteAllBytes(zipFile, memoryStream.ToArray());
				ExceptionLogger.LogDebug("Added {0} to {1}!", new object[]
				{
					filename,
					zipFile
				});
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to add {0} to {1}: {2}", new object[]
				{
					filename,
					zipFile,
					ex.Message
				});
			}
		}

		// Token: 0x0600D034 RID: 53300 RVA: 0x003DEF50 File Offset: 0x003DD150
		private static void SafeAddFolderToZip(ZipOutputStream zipStream, string folder, string name)
		{
			if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
			{
				return;
			}
			foreach (string text in Directory.GetFiles(folder).ToArray<string>())
			{
				try
				{
					ZipUtil.SafeAddToZip(zipStream, text, string.Format("{0}/{1}", name, new FileInfo(text).Name));
				}
				catch (Exception ex)
				{
					ExceptionLogger.LogError(string.Concat(new string[]
					{
						"Failed to add '",
						text,
						"' from the folder '",
						folder,
						"' to zip:\n",
						ex.Message
					}), Array.Empty<object>());
				}
			}
			foreach (string text2 in Directory.GetDirectories(folder).ToArray<string>())
			{
				string name2 = new DirectoryInfo(text2).Name;
				ZipUtil.SafeAddFolderToZip(zipStream, text2, name + "/" + name2);
			}
		}

		// Token: 0x0600D035 RID: 53301 RVA: 0x003DF040 File Offset: 0x003DD240
		private static void SafeAddToZip(ZipOutputStream zipStream, byte[] bytes, string name)
		{
			try
			{
				if (bytes != null && bytes.Length != 0)
				{
					zipStream.PutNextEntry(new ZipEntry(name));
					zipStream.Write(bytes, 0, bytes.Length);
					zipStream.CloseEntry();
				}
			}
			catch (NotSupportedException ex)
			{
				ExceptionLogger.LogError("Tried to add " + name + " to zip:\n" + ex.Message, Array.Empty<object>());
			}
		}

		// Token: 0x0600D036 RID: 53302 RVA: 0x003DF0A8 File Offset: 0x003DD2A8
		private static void SafeAddToZip(ZipOutputStream zipStream, string filePath, string name)
		{
			if (ReportBuilder.Settings.m_readFileMethodCallback != null)
			{
				ZipUtil.SafeAddToZip(zipStream, ReportBuilder.Settings.m_readFileMethodCallback(filePath), name);
				return;
			}
			ZipUtil.SafeAddToZip(zipStream, File.ReadAllBytes(filePath), name);
		}

		// Token: 0x0600D037 RID: 53303 RVA: 0x003DF0DC File Offset: 0x003DD2DC
		private static void AddLogFiles(ZipOutputStream zipStream, ReportBuilder builder)
		{
			if (builder.LogPaths == null)
			{
				return;
			}
			foreach (string text in builder.LogPaths)
			{
				try
				{
					if (File.Exists(text))
					{
						string[] array = new string[0];
						if (ReportBuilder.Settings.m_readFileMethodCallback != null)
						{
							array = Encoding.UTF8.GetString(ReportBuilder.Settings.m_readFileMethodCallback(text)).Split(new string[]
							{
								"\r\n",
								"\r",
								"\n"
							}, StringSplitOptions.None);
						}
						if (builder.LogLinesLimit > 0)
						{
							array = array.Reverse<string>().Take(builder.LogLinesLimit).Reverse<string>().ToArray<string>();
						}
						ZipUtil.SafeAddToZip(zipStream, Encoding.UTF8.GetBytes(string.Join("\r\n", array)), Path.GetFileName(text));
						ExceptionLogger.LogInfo("Attached the log file: {0}", new object[]
						{
							text
						});
					}
				}
				catch (Exception ex)
				{
					ExceptionLogger.LogError("Failed to retrieve logs from '{0}' with Error: {1}", new object[]
					{
						text,
						ex.Message
					});
				}
			}
		}

		// Token: 0x0600D038 RID: 53304 RVA: 0x003DF200 File Offset: 0x003DD400
		private static void AddScreenshotFile(ReportBuilder builder)
		{
			List<string> list = new List<string>();
			if (builder.AttachableFiles != null)
			{
				list.AddRange(builder.AttachableFiles);
			}
			if (File.Exists(Screenshot.ScreenshotPath))
			{
				list.Add(string.Format("{0}|{1}|{2}|image/png", Screenshot.ScreenshotPath, "screenshot.png", (builder.ReportType == ExceptionSettings.ReportType.BUG) ? "1" : "0"));
				builder.AttachableFiles = list.ToArray();
			}
		}

		// Token: 0x0600D039 RID: 53305 RVA: 0x003DF270 File Offset: 0x003DD470
		private static void AddAttachableFiles(ZipOutputStream zipStream, ReportBuilder builder)
		{
			ZipUtil.AddScreenshotFile(builder);
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
				if (array.Count<string>() <= 2 || !(array[2] == "1"))
				{
					string text = array[0];
					string text2 = null;
					if (array.Count<string>() > 1)
					{
						text2 = array[1];
					}
					try
					{
						if (Directory.Exists(text))
						{
							ZipUtil.SafeAddFolderToZip(zipStream, text, string.IsNullOrEmpty(text2) ? Path.GetFileName(Path.GetDirectoryName(text)) : text2);
							ExceptionLogger.LogInfo("Attached the folder '{0}'", new object[]
							{
								text
							});
						}
						else if (File.Exists(text) || ReportBuilder.Settings.m_readFileMethodCallback != null)
						{
							ZipUtil.SafeAddToZip(zipStream, text, string.IsNullOrEmpty(text2) ? Path.GetFileName(text) : text2);
							ExceptionLogger.LogInfo("Attached the file '{0}'", new object[]
							{
								text
							});
						}
					}
					catch (Exception ex)
					{
						ExceptionLogger.LogError("Failed to attach '{0}' with Error: {1}", new object[]
						{
							text,
							ex.Message
						});
					}
				}
			}
		}
	}
}
