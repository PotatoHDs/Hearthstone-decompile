using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Hearthstone;
using UnityEngine;

public class FileUtils
{
	private static string m_applicationPersistentDataPath = null;

	private static string m_ExternalDataPath = null;

	private static string s_workingDir;

	private static string s_persistentDataPath = Application.persistentDataPath;

	public static readonly char[] FOLDER_SEPARATOR_CHARS = new char[2] { '/', '\\' };

	private static string WorkingDir
	{
		get
		{
			if (s_workingDir == null)
			{
				s_workingDir = Directory.GetCurrentDirectory().Replace("\\", "/");
			}
			return s_workingDir;
		}
	}

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
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				folderPath = folderPath.Replace('\\', '/');
				return $"{folderPath}/Blizzard/Hearthstone";
			}
			if (PlatformSettings.RuntimeOS == OSCategory.iOS)
			{
				return s_persistentDataPath;
			}
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				if (m_applicationPersistentDataPath == null)
				{
					m_applicationPersistentDataPath = AndroidDeviceSettings.Get().applicationStorageFolder;
				}
				return m_applicationPersistentDataPath;
			}
			throw new NotImplementedException("Unknown persistent data path on this platform");
		}
	}

	public static string PublicPersistentDataPath => BasePersistentDataPath;

	public static string InternalPersistentDataPath => $"{BasePersistentDataPath}/Dev";

	public static string PersistentDataPath
	{
		get
		{
			string text = null;
			text = ((!HearthstoneApplication.IsInternal()) ? PublicPersistentDataPath : InternalPersistentDataPath);
			if (!Directory.Exists(text))
			{
				try
				{
					Directory.CreateDirectory(text);
					return text;
				}
				catch (Exception ex)
				{
					Debug.LogError($"FileUtils.PersistentDataPath - Error creating {text}. Exception={ex.Message}");
					Error.AddFatal(FatalErrorReason.CREATE_DATA_FOLDER, "GLOBAL_ERROR_ASSET_CREATE_PERSISTENT_DATA_PATH");
					return text;
				}
			}
			return text;
		}
	}

	public static string BaseExternalDataPath
	{
		get
		{
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				return AndroidDeviceSettings.Get().externalStorageFolder;
			}
			return BasePersistentDataPath;
		}
	}

	public static string ExternalDataPath
	{
		get
		{
			if (m_ExternalDataPath == null)
			{
				if (PlatformSettings.RuntimeOS == OSCategory.Android)
				{
					if (HearthstoneApplication.IsInternal())
					{
						m_ExternalDataPath = $"{BaseExternalDataPath}/Dev";
					}
					else
					{
						m_ExternalDataPath = BaseExternalDataPath;
					}
				}
				else
				{
					m_ExternalDataPath = PersistentDataPath;
				}
			}
			return m_ExternalDataPath;
		}
	}

	public static string CachePath
	{
		get
		{
			string text = $"{PersistentDataPath}/Cache";
			if (!Directory.Exists(text))
			{
				try
				{
					Directory.CreateDirectory(text);
					return text;
				}
				catch (Exception ex)
				{
					Debug.LogError($"FileUtils.CachePath - Error creating {text}. Exception={ex.Message}");
					return text;
				}
			}
			return text;
		}
	}

	public static string MakeSourceAssetPath(DirectoryInfo folder)
	{
		return MakeSourceAssetPath(folder.FullName);
	}

	public static string MakeSourceAssetPath(FileInfo fileInfo)
	{
		return MakeSourceAssetPath(fileInfo.FullName);
	}

	public static string MakeSourceAssetPath(string path)
	{
		string text = path.Replace("\\", "/");
		int num = text.IndexOf("/Assets", StringComparison.OrdinalIgnoreCase);
		return text.Remove(0, num + 1);
	}

	public static string MakeMetaPathFromSourcePath(string path)
	{
		return $"{path}.meta";
	}

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

	public static string GetDirectoryNameWithoutConv(string path)
	{
		int num = path.TrimEnd(FOLDER_SEPARATOR_CHARS).LastIndexOfAny(FOLDER_SEPARATOR_CHARS);
		if (num >= 0)
		{
			return path.Remove(num);
		}
		return "";
	}

	public static string MakeLocalizedPathFromSourcePath(Locale locale, string enUsPath)
	{
		if (string.IsNullOrEmpty(enUsPath))
		{
			return null;
		}
		string text = GetDirectoryNameWithoutConv(enUsPath);
		string fileName = Path.GetFileName(enUsPath);
		int num = text.LastIndexOf("/");
		if (num >= 0 && Localization.IsValidLocaleName(text.Substring(num + 1)))
		{
			text = text.Remove(num);
		}
		if (string.IsNullOrEmpty(text))
		{
			return $"{locale}/{fileName}";
		}
		return $"{text}/{locale}/{fileName}";
	}

	public static string MakeEnglishPathFromLocalizedPath(string localizedPath, Locale? locale)
	{
		if (!locale.HasValue)
		{
			return localizedPath;
		}
		return string.Join("/", (from x in localizedPath.Split('/')
			where x != locale.Value.ToString()
			select x).ToArray());
	}

	public static Locale? GetLocaleFromSourcePath(string path)
	{
		string directoryNameWithoutConv = GetDirectoryNameWithoutConv(path);
		int num = directoryNameWithoutConv.LastIndexOf("/");
		if (num < 0)
		{
			return null;
		}
		string str = directoryNameWithoutConv.Substring(num + 1);
		Locale value;
		try
		{
			value = EnumUtils.Parse<Locale>(str);
		}
		catch (Exception)
		{
			return null;
		}
		return value;
	}

	public static Locale? GetForeignLocaleFromSourcePath(string path)
	{
		Locale? localeFromSourcePath = GetLocaleFromSourcePath(path);
		if (!localeFromSourcePath.HasValue)
		{
			return null;
		}
		if (localeFromSourcePath.Value == Locale.enUS)
		{
			return null;
		}
		return localeFromSourcePath;
	}

	public static bool IsForeignLocaleSourcePath(string path)
	{
		return GetForeignLocaleFromSourcePath(path).HasValue;
	}

	public static string StripLocaleFromPath(string path)
	{
		string directoryNameWithoutConv = GetDirectoryNameWithoutConv(path);
		string fileName = Path.GetFileName(path);
		if (Localization.IsValidLocaleName(Path.GetFileName(directoryNameWithoutConv)))
		{
			return $"{GetDirectoryNameWithoutConv(directoryNameWithoutConv)}/{fileName}";
		}
		return path;
	}

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
		if (PlatformSettings.RuntimeOS == OSCategory.Android)
		{
			if (useAssetBundleFolder)
			{
				return AndroidDeviceSettings.Get().assetBundleFolder + "/" + fileName;
			}
			return AndroidDeviceSettings.Get().applicationStorageFolder + "/" + fileName;
		}
		return fileName;
	}

	public static string GetPluginPath(string fileName)
	{
		return $"Hearthstone_Data/Plugins/{fileName}";
	}

	public static IntPtr LoadPlugin(string fileName, bool handleError = true)
	{
		try
		{
			string pluginPath = GetPluginPath(fileName);
			IntPtr intPtr = DLLUtils.LoadLibrary(pluginPath);
			if (intPtr == IntPtr.Zero && handleError)
			{
				string text = $"{WorkingDir}/{pluginPath}";
				Error.AddDevFatal("Failed to load plugin from '{0}'", text);
				Error.AddFatal(FatalErrorReason.LOAD_PLUGIN, "GLOBAL_ERROR_ASSET_LOAD_FAILED", fileName);
			}
			return intPtr;
		}
		catch (Exception ex)
		{
			Error.AddDevFatal("FileUtils.LoadPlugin() - Exception occurred. message={0} stackTrace={1}", ex.Message, ex.StackTrace);
			return IntPtr.Zero;
		}
	}

	public static string CreateLocalFilePath(string relPath, bool useAssetBundleFolder = true)
	{
		return $"{WorkingDir}/{relPath}";
	}

	public static string GetOnDiskCapitalizationForFile(string filePath)
	{
		return GetOnDiskCapitalizationForFile(new FileInfo(filePath));
	}

	public static string GetOnDiskCapitalizationForDir(string dirPath)
	{
		return GetOnDiskCapitalizationForDir(new DirectoryInfo(dirPath));
	}

	public static string GetOnDiskCapitalizationForFile(FileInfo fileInfo)
	{
		DirectoryInfo directory = fileInfo.Directory;
		return Path.Combine(path2: directory.GetFiles(fileInfo.Name)[0].Name, path1: GetOnDiskCapitalizationForDir(directory));
	}

	public static string GetOnDiskCapitalizationForDir(DirectoryInfo dirInfo)
	{
		DirectoryInfo parent = dirInfo.Parent;
		if (parent == null)
		{
			return dirInfo.Name;
		}
		string name = parent.GetDirectories(dirInfo.Name)[0].Name;
		return Path.Combine(GetOnDiskCapitalizationForDir(parent), name);
	}

	public static bool GetLastFolderAndFileFromPath(string path, out string folderName, out string fileName)
	{
		folderName = null;
		fileName = null;
		if (string.IsNullOrEmpty(path))
		{
			return false;
		}
		int num = path.LastIndexOfAny(FOLDER_SEPARATOR_CHARS);
		if (num > 0)
		{
			int num2 = path.LastIndexOfAny(FOLDER_SEPARATOR_CHARS, num - 1);
			int num3 = ((num2 >= 0) ? (num2 + 1) : 0);
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
		if (folderName == null)
		{
			return fileName != null;
		}
		return true;
	}

	public static bool SetFolderWritableFlag(string dirPath, bool writable)
	{
		string[] files = Directory.GetFiles(dirPath);
		for (int i = 0; i < files.Length; i++)
		{
			SetFileWritableFlag(files[i], writable);
		}
		files = Directory.GetDirectories(dirPath);
		for (int i = 0; i < files.Length; i++)
		{
			SetFolderWritableFlag(files[i], writable);
		}
		return true;
	}

	public static bool SetFileWritableFlag(string path, bool setWritable)
	{
		if (!File.Exists(path))
		{
			return false;
		}
		try
		{
			FileAttributes attributes = File.GetAttributes(path);
			FileAttributes fileAttributes = (setWritable ? (attributes & ~FileAttributes.ReadOnly) : (attributes | FileAttributes.ReadOnly));
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

	public static string GetMD5(string fileName)
	{
		if (!File.Exists(fileName))
		{
			return string.Empty;
		}
		using FileStream inputStream = File.OpenRead(fileName);
		return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(inputStream)).Replace("-", string.Empty);
	}

	public static string GetMD5FromFoldersAndFiles(IEnumerable<string> folders, IEnumerable<string> files)
	{
		List<string> list = new List<string>();
		foreach (string folder in folders)
		{
			if (!Directory.Exists(folder))
			{
				return string.Empty;
			}
			list.AddRange(Directory.GetFiles(folder));
		}
		foreach (string file in files)
		{
			if (!File.Exists(file))
			{
				list.Add(file);
			}
		}
		return GetMD5FromFiles(list);
	}

	public static string GetMD5FromFiles(IEnumerable<string> files)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		List<byte> list = new List<byte>();
		MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
		int num = 0;
		foreach (string file in files)
		{
			using FileStream inputStream = File.OpenRead(file);
			list.AddRange(mD5CryptoServiceProvider.ComputeHash(inputStream));
			num++;
		}
		string text = BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(list.ToArray())).Replace("-", string.Empty);
		float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		Debug.LogFormat("Hashing {0} files took {1}s, HASH={1}", num, realtimeSinceStartup2 - realtimeSinceStartup, text);
		return text;
	}

	public static string GetMD5FromString(string buf)
	{
		return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(buf))).Replace("-", string.Empty);
	}

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
					text = Path.Combine(text.TrimEnd('/'), text2.TrimEnd('/'));
				}
			}
		}
		return text;
	}
}
