using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Hearthstone;
using UnityEngine;

// Token: 0x020009B9 RID: 2489
public class FileUtils
{
	// Token: 0x1700078F RID: 1935
	// (get) Token: 0x06008734 RID: 34612 RVA: 0x002BA33E File Offset: 0x002B853E
	private static string WorkingDir
	{
		get
		{
			if (FileUtils.s_workingDir == null)
			{
				FileUtils.s_workingDir = Directory.GetCurrentDirectory().Replace("\\", "/");
			}
			return FileUtils.s_workingDir;
		}
	}

	// Token: 0x06008735 RID: 34613 RVA: 0x002BA365 File Offset: 0x002B8565
	public static string MakeSourceAssetPath(DirectoryInfo folder)
	{
		return FileUtils.MakeSourceAssetPath(folder.FullName);
	}

	// Token: 0x06008736 RID: 34614 RVA: 0x002BA365 File Offset: 0x002B8565
	public static string MakeSourceAssetPath(FileInfo fileInfo)
	{
		return FileUtils.MakeSourceAssetPath(fileInfo.FullName);
	}

	// Token: 0x06008737 RID: 34615 RVA: 0x002BA374 File Offset: 0x002B8574
	public static string MakeSourceAssetPath(string path)
	{
		string text = path.Replace("\\", "/");
		int num = text.IndexOf("/Assets", StringComparison.OrdinalIgnoreCase);
		return text.Remove(0, num + 1);
	}

	// Token: 0x06008738 RID: 34616 RVA: 0x002BA3A7 File Offset: 0x002B85A7
	public static string MakeMetaPathFromSourcePath(string path)
	{
		return string.Format("{0}.meta", path);
	}

	// Token: 0x06008739 RID: 34617 RVA: 0x002BA3B4 File Offset: 0x002B85B4
	public static string MakeLowResPath(string originalPath)
	{
		if (originalPath == null)
		{
			return null;
		}
		if (originalPath.Contains(".low."))
		{
			return originalPath;
		}
		string text = Path.GetExtension(originalPath).Trim();
		if (string.IsNullOrEmpty(text))
		{
			return originalPath;
		}
		return originalPath.Replace(text, ".low" + text);
	}

	// Token: 0x0600873A RID: 34618 RVA: 0x002BA400 File Offset: 0x002B8600
	public static string GetDirectoryNameWithoutConv(string path)
	{
		int num = path.TrimEnd(FileUtils.FOLDER_SEPARATOR_CHARS).LastIndexOfAny(FileUtils.FOLDER_SEPARATOR_CHARS);
		if (num >= 0)
		{
			return path.Remove(num);
		}
		return "";
	}

	// Token: 0x0600873B RID: 34619 RVA: 0x002BA434 File Offset: 0x002B8634
	public static string MakeLocalizedPathFromSourcePath(Locale locale, string enUsPath)
	{
		if (string.IsNullOrEmpty(enUsPath))
		{
			return null;
		}
		string text = FileUtils.GetDirectoryNameWithoutConv(enUsPath);
		string fileName = Path.GetFileName(enUsPath);
		int num = text.LastIndexOf("/");
		if (num >= 0 && Localization.IsValidLocaleName(text.Substring(num + 1)))
		{
			text = text.Remove(num);
		}
		if (string.IsNullOrEmpty(text))
		{
			return string.Format("{0}/{1}", locale, fileName);
		}
		return string.Format("{0}/{1}/{2}", text, locale, fileName);
	}

	// Token: 0x0600873C RID: 34620 RVA: 0x002BA4B0 File Offset: 0x002B86B0
	public static string MakeEnglishPathFromLocalizedPath(string localizedPath, Locale? locale)
	{
		if (locale == null)
		{
			return localizedPath;
		}
		return string.Join("/", (from x in localizedPath.Split(new char[]
		{
			'/'
		})
		where x != locale.Value.ToString()
		select x).ToArray<string>());
	}

	// Token: 0x0600873D RID: 34621 RVA: 0x002BA50C File Offset: 0x002B870C
	public static Locale? GetLocaleFromSourcePath(string path)
	{
		string directoryNameWithoutConv = FileUtils.GetDirectoryNameWithoutConv(path);
		int num = directoryNameWithoutConv.LastIndexOf("/");
		if (num < 0)
		{
			Locale? result = null;
			return result;
		}
		string str = directoryNameWithoutConv.Substring(num + 1);
		Locale value;
		try
		{
			value = EnumUtils.Parse<Locale>(str);
		}
		catch (Exception)
		{
			Locale? result = null;
			return result;
		}
		return new Locale?(value);
	}

	// Token: 0x0600873E RID: 34622 RVA: 0x002BA578 File Offset: 0x002B8778
	public static Locale? GetForeignLocaleFromSourcePath(string path)
	{
		Locale? localeFromSourcePath = FileUtils.GetLocaleFromSourcePath(path);
		if (localeFromSourcePath == null)
		{
			return null;
		}
		if (localeFromSourcePath.Value == Locale.enUS)
		{
			return null;
		}
		return localeFromSourcePath;
	}

	// Token: 0x0600873F RID: 34623 RVA: 0x002BA5B4 File Offset: 0x002B87B4
	public static bool IsForeignLocaleSourcePath(string path)
	{
		return FileUtils.GetForeignLocaleFromSourcePath(path) != null;
	}

	// Token: 0x06008740 RID: 34624 RVA: 0x002BA5D0 File Offset: 0x002B87D0
	public static string StripLocaleFromPath(string path)
	{
		string directoryNameWithoutConv = FileUtils.GetDirectoryNameWithoutConv(path);
		string fileName = Path.GetFileName(path);
		if (Localization.IsValidLocaleName(Path.GetFileName(directoryNameWithoutConv)))
		{
			return string.Format("{0}/{1}", FileUtils.GetDirectoryNameWithoutConv(directoryNameWithoutConv), fileName);
		}
		return path;
	}

	// Token: 0x06008741 RID: 34625 RVA: 0x002BA60C File Offset: 0x002B880C
	public static string GetAssetPath(string fileName, bool useAssetBundleFolder = true)
	{
		if (PlatformSettings.RuntimeOS == OSCategory.iOS)
		{
			string text = Application.persistentDataPath + "/" + fileName;
			if (new FileInfo(text).Exists)
			{
				return text;
			}
			string text2 = Application.dataPath + "/Raw/" + fileName;
			if (new FileInfo(text2).Exists)
			{
				return text2;
			}
			return text;
		}
		else
		{
			if (PlatformSettings.RuntimeOS != OSCategory.Android)
			{
				return fileName;
			}
			if (useAssetBundleFolder)
			{
				return AndroidDeviceSettings.Get().assetBundleFolder + "/" + fileName;
			}
			return AndroidDeviceSettings.Get().applicationStorageFolder + "/" + fileName;
		}
	}

	// Token: 0x17000790 RID: 1936
	// (get) Token: 0x06008742 RID: 34626 RVA: 0x002BA69C File Offset: 0x002B889C
	public static string BasePersistentDataPath
	{
		get
		{
			if (PlatformSettings.RuntimeOS == OSCategory.Mac)
			{
				return string.Format("{0}/Library/Preferences/Blizzard/Hearthstone", Environment.GetEnvironmentVariable("HOME"));
			}
			if (PlatformSettings.RuntimeOS == OSCategory.PC)
			{
				string text = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				text = text.Replace('\\', '/');
				return string.Format("{0}/Blizzard/Hearthstone", text);
			}
			if (PlatformSettings.RuntimeOS == OSCategory.iOS)
			{
				return FileUtils.s_persistentDataPath;
			}
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				if (FileUtils.m_applicationPersistentDataPath == null)
				{
					FileUtils.m_applicationPersistentDataPath = AndroidDeviceSettings.Get().applicationStorageFolder;
				}
				return FileUtils.m_applicationPersistentDataPath;
			}
			throw new NotImplementedException("Unknown persistent data path on this platform");
		}
	}

	// Token: 0x17000791 RID: 1937
	// (get) Token: 0x06008743 RID: 34627 RVA: 0x002BA729 File Offset: 0x002B8929
	public static string PublicPersistentDataPath
	{
		get
		{
			return FileUtils.BasePersistentDataPath;
		}
	}

	// Token: 0x17000792 RID: 1938
	// (get) Token: 0x06008744 RID: 34628 RVA: 0x002BA730 File Offset: 0x002B8930
	public static string InternalPersistentDataPath
	{
		get
		{
			return string.Format("{0}/Dev", FileUtils.BasePersistentDataPath);
		}
	}

	// Token: 0x17000793 RID: 1939
	// (get) Token: 0x06008745 RID: 34629 RVA: 0x002BA744 File Offset: 0x002B8944
	public static string PersistentDataPath
	{
		get
		{
			string text = null;
			if (HearthstoneApplication.IsInternal())
			{
				text = FileUtils.InternalPersistentDataPath;
			}
			else
			{
				text = FileUtils.PublicPersistentDataPath;
			}
			if (!Directory.Exists(text))
			{
				try
				{
					Directory.CreateDirectory(text);
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("FileUtils.PersistentDataPath - Error creating {0}. Exception={1}", text, ex.Message));
					Error.AddFatal(FatalErrorReason.CREATE_DATA_FOLDER, "GLOBAL_ERROR_ASSET_CREATE_PERSISTENT_DATA_PATH", Array.Empty<object>());
				}
			}
			return text;
		}
	}

	// Token: 0x17000794 RID: 1940
	// (get) Token: 0x06008746 RID: 34630 RVA: 0x002BA7B4 File Offset: 0x002B89B4
	public static string BaseExternalDataPath
	{
		get
		{
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				return AndroidDeviceSettings.Get().externalStorageFolder;
			}
			return FileUtils.BasePersistentDataPath;
		}
	}

	// Token: 0x17000795 RID: 1941
	// (get) Token: 0x06008747 RID: 34631 RVA: 0x002BA7D0 File Offset: 0x002B89D0
	public static string ExternalDataPath
	{
		get
		{
			if (FileUtils.m_ExternalDataPath == null)
			{
				if (PlatformSettings.RuntimeOS == OSCategory.Android)
				{
					if (HearthstoneApplication.IsInternal())
					{
						FileUtils.m_ExternalDataPath = string.Format("{0}/Dev", FileUtils.BaseExternalDataPath);
					}
					else
					{
						FileUtils.m_ExternalDataPath = FileUtils.BaseExternalDataPath;
					}
				}
				else
				{
					FileUtils.m_ExternalDataPath = FileUtils.PersistentDataPath;
				}
			}
			return FileUtils.m_ExternalDataPath;
		}
	}

	// Token: 0x17000796 RID: 1942
	// (get) Token: 0x06008748 RID: 34632 RVA: 0x002BA824 File Offset: 0x002B8A24
	public static string CachePath
	{
		get
		{
			string text = string.Format("{0}/Cache", FileUtils.PersistentDataPath);
			if (!Directory.Exists(text))
			{
				try
				{
					Directory.CreateDirectory(text);
				}
				catch (Exception ex)
				{
					Debug.LogError(string.Format("FileUtils.CachePath - Error creating {0}. Exception={1}", text, ex.Message));
				}
			}
			return text;
		}
	}

	// Token: 0x06008749 RID: 34633 RVA: 0x002BA87C File Offset: 0x002B8A7C
	public static string GetPluginPath(string fileName)
	{
		return string.Format("Hearthstone_Data/Plugins/{0}", fileName);
	}

	// Token: 0x0600874A RID: 34634 RVA: 0x002BA88C File Offset: 0x002B8A8C
	public static IntPtr LoadPlugin(string fileName, bool handleError = true)
	{
		IntPtr result;
		try
		{
			string pluginPath = FileUtils.GetPluginPath(fileName);
			IntPtr intPtr = DLLUtils.LoadLibrary(pluginPath);
			if (intPtr == IntPtr.Zero && handleError)
			{
				string text = string.Format("{0}/{1}", FileUtils.WorkingDir, pluginPath);
				Error.AddDevFatal("Failed to load plugin from '{0}'", new object[]
				{
					text
				});
				Error.AddFatal(FatalErrorReason.LOAD_PLUGIN, "GLOBAL_ERROR_ASSET_LOAD_FAILED", new object[]
				{
					fileName
				});
			}
			result = intPtr;
		}
		catch (Exception ex)
		{
			Error.AddDevFatal("FileUtils.LoadPlugin() - Exception occurred. message={0} stackTrace={1}", new object[]
			{
				ex.Message,
				ex.StackTrace
			});
			result = IntPtr.Zero;
		}
		return result;
	}

	// Token: 0x0600874B RID: 34635 RVA: 0x002BA934 File Offset: 0x002B8B34
	public static string CreateLocalFilePath(string relPath, bool useAssetBundleFolder = true)
	{
		return string.Format("{0}/{1}", FileUtils.WorkingDir, relPath);
	}

	// Token: 0x0600874C RID: 34636 RVA: 0x002BA946 File Offset: 0x002B8B46
	public static string GetOnDiskCapitalizationForFile(string filePath)
	{
		return FileUtils.GetOnDiskCapitalizationForFile(new FileInfo(filePath));
	}

	// Token: 0x0600874D RID: 34637 RVA: 0x002BA953 File Offset: 0x002B8B53
	public static string GetOnDiskCapitalizationForDir(string dirPath)
	{
		return FileUtils.GetOnDiskCapitalizationForDir(new DirectoryInfo(dirPath));
	}

	// Token: 0x0600874E RID: 34638 RVA: 0x002BA960 File Offset: 0x002B8B60
	public static string GetOnDiskCapitalizationForFile(FileInfo fileInfo)
	{
		DirectoryInfo directory = fileInfo.Directory;
		string name = directory.GetFiles(fileInfo.Name)[0].Name;
		return Path.Combine(FileUtils.GetOnDiskCapitalizationForDir(directory), name);
	}

	// Token: 0x0600874F RID: 34639 RVA: 0x002BA994 File Offset: 0x002B8B94
	public static string GetOnDiskCapitalizationForDir(DirectoryInfo dirInfo)
	{
		DirectoryInfo parent = dirInfo.Parent;
		if (parent == null)
		{
			return dirInfo.Name;
		}
		string name = parent.GetDirectories(dirInfo.Name)[0].Name;
		return Path.Combine(FileUtils.GetOnDiskCapitalizationForDir(parent), name);
	}

	// Token: 0x06008750 RID: 34640 RVA: 0x002BA9D4 File Offset: 0x002B8BD4
	public static bool GetLastFolderAndFileFromPath(string path, out string folderName, out string fileName)
	{
		folderName = null;
		fileName = null;
		if (string.IsNullOrEmpty(path))
		{
			return false;
		}
		int num = path.LastIndexOfAny(FileUtils.FOLDER_SEPARATOR_CHARS);
		if (num > 0)
		{
			int num2 = path.LastIndexOfAny(FileUtils.FOLDER_SEPARATOR_CHARS, num - 1);
			int num3 = (num2 < 0) ? 0 : (num2 + 1);
			int length = num - num3;
			folderName = path.Substring(num3, length);
		}
		if (num < 0)
		{
			fileName = path;
		}
		else if (num < path.Length - 1)
		{
			fileName = path.Substring(num + 1);
		}
		return folderName != null || fileName != null;
	}

	// Token: 0x06008751 RID: 34641 RVA: 0x002BAA54 File Offset: 0x002B8C54
	public static bool SetFolderWritableFlag(string dirPath, bool writable)
	{
		string[] array = Directory.GetFiles(dirPath);
		for (int i = 0; i < array.Length; i++)
		{
			FileUtils.SetFileWritableFlag(array[i], writable);
		}
		array = Directory.GetDirectories(dirPath);
		for (int i = 0; i < array.Length; i++)
		{
			FileUtils.SetFolderWritableFlag(array[i], writable);
		}
		return true;
	}

	// Token: 0x06008752 RID: 34642 RVA: 0x002BAAA0 File Offset: 0x002B8CA0
	public static bool SetFileWritableFlag(string path, bool setWritable)
	{
		if (!File.Exists(path))
		{
			return false;
		}
		try
		{
			FileAttributes attributes = File.GetAttributes(path);
			FileAttributes fileAttributes = setWritable ? (attributes & ~FileAttributes.ReadOnly) : (attributes | FileAttributes.ReadOnly);
			if (setWritable && (Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX))
			{
				fileAttributes |= FileAttributes.Normal;
			}
			if (fileAttributes == attributes)
			{
				return true;
			}
			File.SetAttributes(path, fileAttributes);
			if (File.GetAttributes(path) != fileAttributes)
			{
				return false;
			}
			return true;
		}
		catch (DirectoryNotFoundException)
		{
		}
		catch (FileNotFoundException)
		{
		}
		catch (Exception)
		{
		}
		return false;
	}

	// Token: 0x06008753 RID: 34643 RVA: 0x002BAB48 File Offset: 0x002B8D48
	public static string GetMD5(string fileName)
	{
		if (!File.Exists(fileName))
		{
			return string.Empty;
		}
		string result;
		using (FileStream fileStream = File.OpenRead(fileName))
		{
			result = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(fileStream)).Replace("-", string.Empty);
		}
		return result;
	}

	// Token: 0x06008754 RID: 34644 RVA: 0x002BABA8 File Offset: 0x002B8DA8
	public static string GetMD5FromFoldersAndFiles(IEnumerable<string> folders, IEnumerable<string> files)
	{
		List<string> list = new List<string>();
		foreach (string path in folders)
		{
			if (!Directory.Exists(path))
			{
				return string.Empty;
			}
			list.AddRange(Directory.GetFiles(path));
		}
		foreach (string text in files)
		{
			if (!File.Exists(text))
			{
				list.Add(text);
			}
		}
		return FileUtils.GetMD5FromFiles(list);
	}

	// Token: 0x06008755 RID: 34645 RVA: 0x002BAC58 File Offset: 0x002B8E58
	public static string GetMD5FromFiles(IEnumerable<string> files)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		List<byte> list = new List<byte>();
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		int num = 0;
		foreach (string path in files)
		{
			using (FileStream fileStream = File.OpenRead(path))
			{
				list.AddRange(md5CryptoServiceProvider.ComputeHash(fileStream));
				num++;
			}
		}
		string text = BitConverter.ToString(md5CryptoServiceProvider.ComputeHash(list.ToArray())).Replace("-", string.Empty);
		float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		Debug.LogFormat("Hashing {0} files took {1}s, HASH={1}", new object[]
		{
			num,
			realtimeSinceStartup2 - realtimeSinceStartup,
			text
		});
		return text;
	}

	// Token: 0x06008756 RID: 34646 RVA: 0x002BAD3C File Offset: 0x002B8F3C
	public static string GetMD5FromString(string buf)
	{
		return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(buf))).Replace("-", string.Empty);
	}

	// Token: 0x06008757 RID: 34647 RVA: 0x002BAD68 File Offset: 0x002B8F68
	public static string CombinePath(params object[] args)
	{
		string text = string.Empty;
		foreach (object obj in args)
		{
			if (obj != null)
			{
				string text2 = obj.ToString();
				if (!string.IsNullOrEmpty(text2))
				{
					text = Path.Combine(text.TrimEnd(new char[]
					{
						'/'
					}), text2.TrimEnd(new char[]
					{
						'/'
					}));
				}
			}
		}
		return text;
	}

	// Token: 0x0400723F RID: 29247
	private static string m_applicationPersistentDataPath = null;

	// Token: 0x04007240 RID: 29248
	private static string m_ExternalDataPath = null;

	// Token: 0x04007241 RID: 29249
	private static string s_workingDir;

	// Token: 0x04007242 RID: 29250
	private static string s_persistentDataPath = Application.persistentDataPath;

	// Token: 0x04007243 RID: 29251
	public static readonly char[] FOLDER_SEPARATOR_CHARS = new char[]
	{
		'/',
		'\\'
	};
}
