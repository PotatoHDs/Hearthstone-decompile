using System;
using System.Text;
using UnityEngine;

public class AndroidDeviceSettings
{
	private static AndroidDeviceSettings s_instance;

	private string m_bestTexture = "";

	private bool m_isMusicPlaying;

	public bool m_determineSDCard;

	public string m_deviceModel = string.Empty;

	public float heightPixels;

	public float widthPixels;

	public float xdpi;

	public float ydpi;

	public float widthInches;

	public float heightInches;

	public float diagonalInches;

	public float aspectRatio;

	public const int SCREENLAYOUT_SIZE_XLARGE = 4;

	public int densityDpi = 300;

	public bool isExtraLarge = true;

	public bool isTablet = true;

	public bool isOnTabletAllowlist;

	public bool isOnPhoneAllowlist;

	public bool isOnLowGraphicsList;

	public string applicationStorageFolder;

	public string assetBundleFolder;

	public string externalStorageFolder;

	public string m_HSStore;

	public int m_AndroidSDKVersion;

	public int screenLayout;

	public string InstalledTexture
	{
		get
		{
			if (!string.IsNullOrEmpty(m_bestTexture))
			{
				return m_bestTexture;
			}
			m_bestTexture = Vars.Key("Mobile.Texture").GetStr("");
			if (!string.IsNullOrEmpty(m_bestTexture))
			{
				Log.Downloader.PrintInfo("m_bestTexture is already set to " + m_bestTexture);
				return m_bestTexture;
			}
			m_bestTexture = "etc1";
			if (SystemInfo.SupportsTextureFormat(TextureFormat.ASTC_RGBA_12x12))
			{
				m_bestTexture = "astc";
			}
			return m_bestTexture;
		}
		protected set
		{
			m_bestTexture = value;
		}
	}

	private AndroidDeviceSettings()
	{
		_ = Application.isEditor;
	}

	public void AskForSDCard()
	{
		m_determineSDCard = true;
	}

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
		}[InstalledTexture]);
		Debug.Log("Checking whether texture format of build (" + InstalledTexture + ") is supported? " + result);
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("All supported texture formats: ");
		foreach (TextureFormat value in Enum.GetValues(typeof(TextureFormat)))
		{
			try
			{
				if (SystemInfo.SupportsTextureFormat(value))
				{
					stringBuilder.Append(string.Concat(value, ", "));
				}
			}
			catch (ArgumentException)
			{
			}
		}
		Log.Graphics.Print(stringBuilder.ToString());
		return result;
	}

	public bool IsMusicPlaying()
	{
		if (Application.isEditor)
		{
			return false;
		}
		m_isMusicPlaying = false;
		return m_isMusicPlaying;
	}

	public string GetSimpleDeviceModel()
	{
		return m_deviceModel;
	}

	public AndroidStore GetAndroidStore()
	{
		return AndroidStore.NONE;
	}

	public bool IsNonStoreAppAllowed()
	{
		return false;
	}

	public bool IsCNBuild()
	{
		return false;
	}

	public string GetPatchUrlFromArgument()
	{
		return "";
	}

	public bool AllowUnknownApps()
	{
		return false;
	}

	public void TriggerUnknownSources(string responseFuncName)
	{
	}

	public void ProcessInstallAPK(string apkPath, string installAPKFuncName)
	{
	}

	public bool OpenAppStore()
	{
		UpdateUtils.OpenAppStore();
		return true;
	}

	public void DeleteOldNotificationChannels()
	{
	}

	public static AndroidDeviceSettings Get()
	{
		if (s_instance == null)
		{
			s_instance = new AndroidDeviceSettings();
		}
		return s_instance;
	}
}
