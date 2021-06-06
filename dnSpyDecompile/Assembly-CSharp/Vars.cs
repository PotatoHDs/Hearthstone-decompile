using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000A00 RID: 2560
public class Vars
{
	// Token: 0x06008AE8 RID: 35560 RVA: 0x002C73FF File Offset: 0x002C55FF
	public static VarKey Key(string key)
	{
		return new VarKey(key);
	}

	// Token: 0x170007C4 RID: 1988
	// (get) Token: 0x06008AE9 RID: 35561 RVA: 0x002C7407 File Offset: 0x002C5607
	public static IEnumerable<string> AllKeys
	{
		get
		{
			return VarsInternal.Get().AllKeys;
		}
	}

	// Token: 0x06008AEA RID: 35562 RVA: 0x002C7413 File Offset: 0x002C5613
	public static void RefreshVars()
	{
		VarsInternal.RefreshVars(true);
	}

	// Token: 0x06008AEB RID: 35563 RVA: 0x002C741B File Offset: 0x002C561B
	public static void ClearVars()
	{
		VarsInternal.RefreshVars(false);
	}

	// Token: 0x06008AEC RID: 35564 RVA: 0x002C7423 File Offset: 0x002C5623
	public static string GetOptionsFileName()
	{
		return "options.txt";
	}

	// Token: 0x06008AED RID: 35565 RVA: 0x002C742A File Offset: 0x002C562A
	public static bool IsOptionsFileOverridden()
	{
		return Vars.GetOptionsFileName() != "options.txt";
	}

	// Token: 0x06008AEE RID: 35566 RVA: 0x002C743B File Offset: 0x002C563B
	public static string GetClientConfigPath()
	{
		return Vars.GetConfigPath("client.config", false);
	}

	// Token: 0x06008AEF RID: 35567 RVA: 0x002C7448 File Offset: 0x002C5648
	public static string GetTokenConfigPath()
	{
		return Vars.GetConfigPath("token.config", false);
	}

	// Token: 0x06008AF0 RID: 35568 RVA: 0x002C7458 File Offset: 0x002C5658
	private static string GetConfigPath(string filename, bool forceUserSaveFolder)
	{
		OSCategory oscategory = OSCategory.PC;
		string text;
		if (oscategory == OSCategory.iOS)
		{
			text = string.Format("{0}/{1}", Application.persistentDataPath, filename);
			if (!forceUserSaveFolder && !File.Exists(text))
			{
				text = FileUtils.GetAssetPath(filename, false);
			}
		}
		else if (oscategory == OSCategory.Android)
		{
			text = string.Format("{0}/{1}", FileUtils.BaseExternalDataPath, filename);
			if (!forceUserSaveFolder && !File.Exists(text))
			{
				text = string.Format("{0}/{1}", FileUtils.BasePersistentDataPath, filename);
				if (!File.Exists(text))
				{
					text = FileUtils.GetAssetPath(filename, false);
				}
			}
		}
		else
		{
			text = filename;
		}
		Log.ConfigFile.Print("GetConfigPath() configPath={0}", new object[]
		{
			text
		});
		return text;
	}

	// Token: 0x06008AF1 RID: 35569 RVA: 0x002C74F0 File Offset: 0x002C56F0
	public static bool SaveConfig()
	{
		return VarsInternal.Get().SaveConfig(Vars.GetConfigPath("client.config", true));
	}

	// Token: 0x06008AF2 RID: 35570 RVA: 0x002C7507 File Offset: 0x002C5707
	public static string GenerateText()
	{
		return VarsInternal.Get().GenerateText();
	}

	// Token: 0x0400738B RID: 29579
	public const string CONFIG_FILE_NAME = "client.config";

	// Token: 0x0400738C RID: 29580
	public const string TOKEN_FILE_NAME = "token.config";

	// Token: 0x0400738D RID: 29581
	public const string OPTIONS_FILE_NAME = "options.txt";

	// Token: 0x0400738E RID: 29582
	public static string s_configNameOverride = "";

	// Token: 0x0400738F RID: 29583
	public static string s_optionsNameOverride = "";
}
