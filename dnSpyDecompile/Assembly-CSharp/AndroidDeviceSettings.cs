using System;
using System.Text;
using UnityEngine;

// Token: 0x020005E1 RID: 1505
public class AndroidDeviceSettings
{
	// Token: 0x06005277 RID: 21111 RVA: 0x001B1371 File Offset: 0x001AF571
	private AndroidDeviceSettings()
	{
		bool isEditor = Application.isEditor;
	}

	// Token: 0x17000512 RID: 1298
	// (get) Token: 0x06005278 RID: 21112 RVA: 0x001B13B0 File Offset: 0x001AF5B0
	// (set) Token: 0x06005279 RID: 21113 RVA: 0x001B1443 File Offset: 0x001AF643
	public string InstalledTexture
	{
		get
		{
			if (!string.IsNullOrEmpty(this.m_bestTexture))
			{
				return this.m_bestTexture;
			}
			this.m_bestTexture = Vars.Key("Mobile.Texture").GetStr("");
			if (!string.IsNullOrEmpty(this.m_bestTexture))
			{
				Log.Downloader.PrintInfo("m_bestTexture is already set to " + this.m_bestTexture, Array.Empty<object>());
				return this.m_bestTexture;
			}
			this.m_bestTexture = "etc1";
			if (SystemInfo.SupportsTextureFormat(TextureFormat.ASTC_RGBA_12x12))
			{
				this.m_bestTexture = "astc";
			}
			return this.m_bestTexture;
		}
		protected set
		{
			this.m_bestTexture = value;
		}
	}

	// Token: 0x0600527A RID: 21114 RVA: 0x001B144C File Offset: 0x001AF64C
	public void AskForSDCard()
	{
		this.m_determineSDCard = true;
	}

	// Token: 0x0600527B RID: 21115 RVA: 0x001B1458 File Offset: 0x001AF658
	public bool IsCurrentTextureFormatSupported()
	{
		bool result = SystemInfo.SupportsTextureFormat(new Map<string, TextureFormat>
		{
			{
				"",
				TextureFormat.ARGB32
			},
			{
				"etc1",
				TextureFormat.ETC_RGB4
			},
			{
				"etc2",
				TextureFormat.ETC2_RGBA8
			},
			{
				"astc",
				TextureFormat.ASTC_RGBA_10x10
			}
		}[this.InstalledTexture]);
		Debug.Log("Checking whether texture format of build (" + this.InstalledTexture + ") is supported? " + result.ToString());
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("All supported texture formats: ");
		foreach (object obj in Enum.GetValues(typeof(TextureFormat)))
		{
			TextureFormat textureFormat = (TextureFormat)obj;
			try
			{
				if (SystemInfo.SupportsTextureFormat(textureFormat))
				{
					stringBuilder.Append(textureFormat + ", ");
				}
			}
			catch (ArgumentException)
			{
			}
		}
		Log.Graphics.Print(stringBuilder.ToString(), Array.Empty<object>());
		return result;
	}

	// Token: 0x0600527C RID: 21116 RVA: 0x001B1578 File Offset: 0x001AF778
	public bool IsMusicPlaying()
	{
		if (Application.isEditor)
		{
			return false;
		}
		this.m_isMusicPlaying = false;
		return this.m_isMusicPlaying;
	}

	// Token: 0x0600527D RID: 21117 RVA: 0x001B1590 File Offset: 0x001AF790
	public string GetSimpleDeviceModel()
	{
		return this.m_deviceModel;
	}

	// Token: 0x0600527E RID: 21118 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public AndroidStore GetAndroidStore()
	{
		return AndroidStore.NONE;
	}

	// Token: 0x0600527F RID: 21119 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool IsNonStoreAppAllowed()
	{
		return false;
	}

	// Token: 0x06005280 RID: 21120 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool IsCNBuild()
	{
		return false;
	}

	// Token: 0x06005281 RID: 21121 RVA: 0x000D5239 File Offset: 0x000D3439
	public string GetPatchUrlFromArgument()
	{
		return "";
	}

	// Token: 0x06005282 RID: 21122 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public bool AllowUnknownApps()
	{
		return false;
	}

	// Token: 0x06005283 RID: 21123 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void TriggerUnknownSources(string responseFuncName)
	{
	}

	// Token: 0x06005284 RID: 21124 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void ProcessInstallAPK(string apkPath, string installAPKFuncName)
	{
	}

	// Token: 0x06005285 RID: 21125 RVA: 0x001B1598 File Offset: 0x001AF798
	public bool OpenAppStore()
	{
		UpdateUtils.OpenAppStore();
		return true;
	}

	// Token: 0x06005286 RID: 21126 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void DeleteOldNotificationChannels()
	{
	}

	// Token: 0x06005287 RID: 21127 RVA: 0x001B15A0 File Offset: 0x001AF7A0
	public static AndroidDeviceSettings Get()
	{
		if (AndroidDeviceSettings.s_instance == null)
		{
			AndroidDeviceSettings.s_instance = new AndroidDeviceSettings();
		}
		return AndroidDeviceSettings.s_instance;
	}

	// Token: 0x0400498A RID: 18826
	private static AndroidDeviceSettings s_instance;

	// Token: 0x0400498B RID: 18827
	private string m_bestTexture = "";

	// Token: 0x0400498C RID: 18828
	private bool m_isMusicPlaying;

	// Token: 0x0400498D RID: 18829
	public bool m_determineSDCard;

	// Token: 0x0400498E RID: 18830
	public string m_deviceModel = string.Empty;

	// Token: 0x0400498F RID: 18831
	public float heightPixels;

	// Token: 0x04004990 RID: 18832
	public float widthPixels;

	// Token: 0x04004991 RID: 18833
	public float xdpi;

	// Token: 0x04004992 RID: 18834
	public float ydpi;

	// Token: 0x04004993 RID: 18835
	public float widthInches;

	// Token: 0x04004994 RID: 18836
	public float heightInches;

	// Token: 0x04004995 RID: 18837
	public float diagonalInches;

	// Token: 0x04004996 RID: 18838
	public float aspectRatio;

	// Token: 0x04004997 RID: 18839
	public const int SCREENLAYOUT_SIZE_XLARGE = 4;

	// Token: 0x04004998 RID: 18840
	public int densityDpi = 300;

	// Token: 0x04004999 RID: 18841
	public bool isExtraLarge = true;

	// Token: 0x0400499A RID: 18842
	public bool isTablet = true;

	// Token: 0x0400499B RID: 18843
	public bool isOnTabletAllowlist;

	// Token: 0x0400499C RID: 18844
	public bool isOnPhoneAllowlist;

	// Token: 0x0400499D RID: 18845
	public bool isOnLowGraphicsList;

	// Token: 0x0400499E RID: 18846
	public string applicationStorageFolder;

	// Token: 0x0400499F RID: 18847
	public string assetBundleFolder;

	// Token: 0x040049A0 RID: 18848
	public string externalStorageFolder;

	// Token: 0x040049A1 RID: 18849
	public string m_HSStore;

	// Token: 0x040049A2 RID: 18850
	public int m_AndroidSDKVersion;

	// Token: 0x040049A3 RID: 18851
	public int screenLayout;
}
