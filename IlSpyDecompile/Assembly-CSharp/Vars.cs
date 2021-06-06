using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Vars
{
	public const string CONFIG_FILE_NAME = "client.config";

	public const string TOKEN_FILE_NAME = "token.config";

	public const string OPTIONS_FILE_NAME = "options.txt";

	public static string s_configNameOverride = "";

	public static string s_optionsNameOverride = "";

	public static IEnumerable<string> AllKeys => VarsInternal.Get().AllKeys;

	public static VarKey Key(string key)
	{
		return new VarKey(key);
	}

	public static void RefreshVars()
	{
		VarsInternal.RefreshVars(loadFile: true);
	}

	public static void ClearVars()
	{
		VarsInternal.RefreshVars(loadFile: false);
	}

	public static string GetOptionsFileName()
	{
		return "options.txt";
	}

	public static bool IsOptionsFileOverridden()
	{
		return GetOptionsFileName() != "options.txt";
	}

	public static string GetClientConfigPath()
	{
		return GetConfigPath("client.config", forceUserSaveFolder: false);
	}

	public static string GetTokenConfigPath()
	{
		return GetConfigPath("token.config", forceUserSaveFolder: false);
	}

	private static string GetConfigPath(string filename, bool forceUserSaveFolder)
	{
		string text;
		switch (1)
		{
		case 3:
			text = $"{Application.persistentDataPath}/{filename}";
			if (!forceUserSaveFolder && !File.Exists(text))
			{
				text = FileUtils.GetAssetPath(filename, useAssetBundleFolder: false);
			}
			break;
		case 4:
			text = $"{FileUtils.BaseExternalDataPath}/{filename}";
			if (!forceUserSaveFolder && !File.Exists(text))
			{
				text = $"{FileUtils.BasePersistentDataPath}/{filename}";
				if (!File.Exists(text))
				{
					text = FileUtils.GetAssetPath(filename, useAssetBundleFolder: false);
				}
			}
			break;
		default:
			text = filename;
			break;
		}
		Log.ConfigFile.Print("GetConfigPath() configPath={0}", text);
		return text;
	}

	public static bool SaveConfig()
	{
		return VarsInternal.Get().SaveConfig(GetConfigPath("client.config", forceUserSaveFolder: true));
	}

	public static string GenerateText()
	{
		return VarsInternal.Get().GenerateText();
	}
}
