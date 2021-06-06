using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace Blizzard.BlizzardErrorMobile
{
	internal static class ZipUtil
	{
		public static byte[] BuildZipArchive(ReportBuilder builder)
		{
			MemoryStream memoryStream = new MemoryStream();
			ZipConstants.DefaultCodePage = 0;
			ZipOutputStream zipOutputStream = new ZipOutputStream(memoryStream, 4096);
			zipOutputStream.SetLevel(6);
			byte[] array = new byte[0];
			if (!string.IsNullOrEmpty(builder.Markup))
			{
				array = Encoding.UTF8.GetBytes(builder.Markup);
				SafeAddToZip(zipOutputStream, array, "ReportedIssue.xml");
			}
			AddLogFiles(zipOutputStream, builder);
			AddAttachableFiles(zipOutputStream, builder);
			zipOutputStream.IsStreamOwner = false;
			zipOutputStream.Close();
			memoryStream.Flush();
			if (memoryStream.Length > builder.SizeLimit)
			{
				memoryStream.Close();
				throw new InsufficientMemoryException("The zip file is too large");
			}
			return memoryStream.ToArray();
		}

		public static void AddFileToZip(string filename, string zipFile, int sizeLimit)
		{
			MemoryStream memoryStream = new MemoryStream();
			using (FileStream baseInputStream = new FileStream(zipFile, FileMode.Open))
			{
				ZipInputStream zipInputStream = new ZipInputStream(baseInputStream);
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
				SafeAddToZip(zipOutputStream, File.ReadAllBytes(filename), new FileInfo(filename).Name);
				zipOutputStream.IsStreamOwner = false;
				zipOutputStream.Close();
				zipInputStream.Close();
				memoryStream.Flush();
				if (memoryStream.Length > sizeLimit)
				{
					memoryStream.Close();
					throw new InsufficientMemoryException("The zip file is too large");
				}
			}
			try
			{
				File.Delete(zipFile);
				File.WriteAllBytes(zipFile, memoryStream.ToArray());
				ExceptionLogger.LogDebug("Added {0} to {1}!", filename, zipFile);
			}
			catch (Exception ex)
			{
				ExceptionLogger.LogError("Failed to add {0} to {1}: {2}", filename, zipFile, ex.Message);
			}
		}

		private static void SafeAddFolderToZip(ZipOutputStream zipStream, string folder, string name)
		{
			if (string.IsNullOrEmpty(folder) || !Directory.Exists(folder))
			{
				return;
			}
			string[] array = Directory.GetFiles(folder).ToArray();
			foreach (string text in array)
			{
				try
				{
					SafeAddToZip(zipStream, text, $"{name}/{new FileInfo(text).Name}");
				}
				catch (Exception ex)
				{
					ExceptionLogger.LogError("Failed to add '" + text + "' from the folder '" + folder + "' to zip:\n" + ex.Message);
				}
			}
			array = Directory.GetDirectories(folder).ToArray();
			foreach (string text2 in array)
			{
				string name2 = new DirectoryInfo(text2).Name;
				SafeAddFolderToZip(zipStream, text2, name + "/" + name2);
			}
		}

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
				ExceptionLogger.LogError("Tried to add " + name + " to zip:\n" + ex.Message);
			}
		}

		private static void SafeAddToZip(ZipOutputStream zipStream, string filePath, string name)
		{
			if (ReportBuilder.Settings.m_readFileMethodCallback != null)
			{
				SafeAddToZip(zipStream, ReportBuilder.Settings.m_readFileMethodCallback(filePath), name);
			}
			else
			{
				SafeAddToZip(zipStream, File.ReadAllBytes(filePath), name);
			}
		}

		private static void AddLogFiles(ZipOutputStream zipStream, ReportBuilder builder)
		{
			if (builder.LogPaths == null)
			{
				return;
			}
			string[] logPaths = builder.LogPaths;
			foreach (string text in logPaths)
			{
				try
				{
					if (File.Exists(text))
					{
						string[] array = new string[0];
						if (ReportBuilder.Settings.m_readFileMethodCallback != null)
						{
							array = Encoding.UTF8.GetString(ReportBuilder.Settings.m_readFileMethodCallback(text)).Split(new string[3] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
						}
						if (builder.LogLinesLimit > 0)
						{
							array = array.Reverse().Take(builder.LogLinesLimit).Reverse()
								.ToArray();
						}
						SafeAddToZip(zipStream, Encoding.UTF8.GetBytes(string.Join("\r\n", array)), Path.GetFileName(text));
						ExceptionLogger.LogInfo("Attached the log file: {0}", text);
					}
				}
				catch (Exception ex)
				{
					ExceptionLogger.LogError("Failed to retrieve logs from '{0}' with Error: {1}", text, ex.Message);
				}
			}
		}

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

		private static void AddAttachableFiles(ZipOutputStream zipStream, ReportBuilder builder)
		{
			AddScreenshotFile(builder);
			if (builder.AttachableFiles == null)
			{
				return;
			}
			string[] attachableFiles = builder.AttachableFiles;
			for (int i = 0; i < attachableFiles.Length; i++)
			{
				string[] array = attachableFiles[i].Split('|');
				if (array.Count() > 2 && array[2] == "1")
				{
					continue;
				}
				string text = array[0];
				string text2 = null;
				if (array.Count() > 1)
				{
					text2 = array[1];
				}
				try
				{
					if (Directory.Exists(text))
					{
						SafeAddFolderToZip(zipStream, text, string.IsNullOrEmpty(text2) ? Path.GetFileName(Path.GetDirectoryName(text)) : text2);
						ExceptionLogger.LogInfo("Attached the folder '{0}'", text);
					}
					else if (File.Exists(text) || ReportBuilder.Settings.m_readFileMethodCallback != null)
					{
						SafeAddToZip(zipStream, text, string.IsNullOrEmpty(text2) ? Path.GetFileName(text) : text2);
						ExceptionLogger.LogInfo("Attached the file '{0}'", text);
					}
				}
				catch (Exception ex)
				{
					ExceptionLogger.LogError("Failed to attach '{0}' with Error: {1}", text, ex.Message);
				}
			}
		}
	}
}
