using System;
using System.Collections.Generic;
using UnityEngine;

public class CardExporterSettings
{
	private const string PREFS_KEY_SET = "EXPORT_CARDS_SET";

	private const string PREFS_KEY_EXPORT_PATH = "EXPORT_CARDS_EXPORT_PATH";

	private const string PREFS_KEY_RESOLUTION_X = "EXPORT_CARDS_RESOLUTION_X";

	private const string PREFS_KEY_RESOLUTION_Y = "EXPORT_CARDS_RESOLUTION_Y";

	private const string PREFS_KEY_GOLDEN = "EXPORT_CARDS_GOLDEN_OPTIONS";

	private const string PREFS_KEY_LOCALE = "EXPORT_CARDS_LOCALE_OPTIONS";

	private const string PREFS_KEY_COLLECTIBLE = "EXPORT_CARDS_COLLECTIBLE";

	private const string PREFS_KEY_NEUTRAL_PREFIX = "EXPORT_CARDS_NEUTRAL_PREFIX";

	private const string PREFS_KEY_EXPORT_HERO_PORTRAIT = "EXPORT_CARDS_EXPORT_HERO_PORTRAIT";

	private const string PREFS_KEY_USE_SHORT_EXPORT_NAME = "EXPORT_CARDS_USE_SHORT_EXPORT_NAME";

	private const string PREFS_KEY_LOCALE_DEPRECATED = "EXPORT_CARDS_LOCALE";

	public TAG_CARD_SET CardSet { get; set; }

	public string ExportPath { get; set; }

	public int ResolutionX { get; set; } = 1024;


	public int ResolutionY { get; set; } = 1024;


	public int GoldenTypeBitMask { get; set; } = 1;


	public List<TAG_PREMIUM> GoldenTypesToExport
	{
		get
		{
			List<TAG_PREMIUM> list = new List<TAG_PREMIUM>();
			foreach (int value in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				if ((GoldenTypeBitMask & (1 << value)) > 0)
				{
					list.Add((TAG_PREMIUM)value);
				}
			}
			return list;
		}
	}

	public int LocalesToExportBitMask { get; set; } = 1;


	public List<Locale> LocalesToExport
	{
		get
		{
			List<Locale> list = new List<Locale>();
			foreach (int value in Enum.GetValues(typeof(Locale)))
			{
				if ((LocalesToExportBitMask & (1 << value)) > 0)
				{
					list.Add((Locale)value);
				}
			}
			return list;
		}
	}

	public bool ExportCollectibleCardsOnly { get; set; } = true;


	public bool UseNeutralPrefix { get; set; }

	public bool ExportAsHeroPortrait { get; set; }

	public bool UseShortExportName { get; set; }

	public void LoadSettings()
	{
		if (PlayerPrefs.HasKey("EXPORT_CARDS_SET"))
		{
			CardSet = (TAG_CARD_SET)PlayerPrefs.GetInt("EXPORT_CARDS_SET");
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_EXPORT_PATH"))
		{
			ExportPath = PlayerPrefs.GetString("EXPORT_CARDS_EXPORT_PATH");
		}
		else
		{
			ExportPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_RESOLUTION_X"))
		{
			ResolutionX = PlayerPrefs.GetInt("EXPORT_CARDS_RESOLUTION_X");
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_RESOLUTION_Y"))
		{
			ResolutionY = PlayerPrefs.GetInt("EXPORT_CARDS_RESOLUTION_Y");
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_GOLDEN_OPTIONS"))
		{
			GoldenTypeBitMask = PlayerPrefs.GetInt("EXPORT_CARDS_GOLDEN_OPTIONS");
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_LOCALE_OPTIONS"))
		{
			LocalesToExportBitMask = PlayerPrefs.GetInt("EXPORT_CARDS_LOCALE_OPTIONS");
		}
		else if (PlayerPrefs.HasKey("EXPORT_CARDS_LOCALE"))
		{
			int @int = PlayerPrefs.GetInt("EXPORT_CARDS_LOCALE");
			LocalesToExportBitMask = 1 << @int;
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_COLLECTIBLE"))
		{
			ExportCollectibleCardsOnly = PlayerPrefs.GetInt("EXPORT_CARDS_COLLECTIBLE") > 0;
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_NEUTRAL_PREFIX"))
		{
			UseNeutralPrefix = PlayerPrefs.GetInt("EXPORT_CARDS_NEUTRAL_PREFIX") > 0;
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_EXPORT_HERO_PORTRAIT"))
		{
			ExportAsHeroPortrait = PlayerPrefs.GetInt("EXPORT_CARDS_EXPORT_HERO_PORTRAIT") > 0;
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_USE_SHORT_EXPORT_NAME"))
		{
			UseShortExportName = PlayerPrefs.GetInt("EXPORT_CARDS_USE_SHORT_EXPORT_NAME") > 0;
		}
		SaveSettings();
	}

	public void SaveSettings()
	{
		PlayerPrefs.SetString("EXPORT_CARDS_EXPORT_PATH", ExportPath);
		PlayerPrefs.SetInt("EXPORT_CARDS_SET", (int)CardSet);
		PlayerPrefs.SetInt("EXPORT_CARDS_RESOLUTION_X", ResolutionX);
		PlayerPrefs.SetInt("EXPORT_CARDS_RESOLUTION_Y", ResolutionY);
		PlayerPrefs.SetInt("EXPORT_CARDS_GOLDEN_OPTIONS", GoldenTypeBitMask);
		PlayerPrefs.SetInt("EXPORT_CARDS_LOCALE_OPTIONS", LocalesToExportBitMask);
		PlayerPrefs.SetInt("EXPORT_CARDS_COLLECTIBLE", ExportCollectibleCardsOnly ? 1 : 0);
		PlayerPrefs.SetInt("EXPORT_CARDS_NEUTRAL_PREFIX", UseNeutralPrefix ? 1 : 0);
		PlayerPrefs.SetInt("EXPORT_CARDS_EXPORT_HERO_PORTRAIT", ExportAsHeroPortrait ? 1 : 0);
		PlayerPrefs.SetInt("EXPORT_CARDS_USE_SHORT_EXPORT_NAME", UseShortExportName ? 1 : 0);
	}
}
