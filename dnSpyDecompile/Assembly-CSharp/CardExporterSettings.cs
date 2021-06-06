using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B45 RID: 2885
public class CardExporterSettings
{
	// Token: 0x170008A8 RID: 2216
	// (get) Token: 0x060098E1 RID: 39137 RVA: 0x00317892 File Offset: 0x00315A92
	// (set) Token: 0x060098E2 RID: 39138 RVA: 0x0031789A File Offset: 0x00315A9A
	public TAG_CARD_SET CardSet { get; set; }

	// Token: 0x170008A9 RID: 2217
	// (get) Token: 0x060098E3 RID: 39139 RVA: 0x003178A3 File Offset: 0x00315AA3
	// (set) Token: 0x060098E4 RID: 39140 RVA: 0x003178AB File Offset: 0x00315AAB
	public string ExportPath { get; set; }

	// Token: 0x170008AA RID: 2218
	// (get) Token: 0x060098E5 RID: 39141 RVA: 0x003178B4 File Offset: 0x00315AB4
	// (set) Token: 0x060098E6 RID: 39142 RVA: 0x003178BC File Offset: 0x00315ABC
	public int ResolutionX { get; set; } = 1024;

	// Token: 0x170008AB RID: 2219
	// (get) Token: 0x060098E7 RID: 39143 RVA: 0x003178C5 File Offset: 0x00315AC5
	// (set) Token: 0x060098E8 RID: 39144 RVA: 0x003178CD File Offset: 0x00315ACD
	public int ResolutionY { get; set; } = 1024;

	// Token: 0x170008AC RID: 2220
	// (get) Token: 0x060098E9 RID: 39145 RVA: 0x003178D6 File Offset: 0x00315AD6
	// (set) Token: 0x060098EA RID: 39146 RVA: 0x003178DE File Offset: 0x00315ADE
	public int GoldenTypeBitMask { get; set; } = 1;

	// Token: 0x170008AD RID: 2221
	// (get) Token: 0x060098EB RID: 39147 RVA: 0x003178E8 File Offset: 0x00315AE8
	public List<TAG_PREMIUM> GoldenTypesToExport
	{
		get
		{
			List<TAG_PREMIUM> list = new List<TAG_PREMIUM>();
			foreach (object obj in Enum.GetValues(typeof(TAG_PREMIUM)))
			{
				int num = (int)obj;
				if ((this.GoldenTypeBitMask & 1 << num) > 0)
				{
					list.Add((TAG_PREMIUM)num);
				}
			}
			return list;
		}
	}

	// Token: 0x170008AE RID: 2222
	// (get) Token: 0x060098EC RID: 39148 RVA: 0x00317964 File Offset: 0x00315B64
	// (set) Token: 0x060098ED RID: 39149 RVA: 0x0031796C File Offset: 0x00315B6C
	public int LocalesToExportBitMask { get; set; } = 1;

	// Token: 0x170008AF RID: 2223
	// (get) Token: 0x060098EE RID: 39150 RVA: 0x00317978 File Offset: 0x00315B78
	public List<Locale> LocalesToExport
	{
		get
		{
			List<Locale> list = new List<Locale>();
			foreach (object obj in Enum.GetValues(typeof(Locale)))
			{
				int num = (int)obj;
				if ((this.LocalesToExportBitMask & 1 << num) > 0)
				{
					list.Add((Locale)num);
				}
			}
			return list;
		}
	}

	// Token: 0x170008B0 RID: 2224
	// (get) Token: 0x060098EF RID: 39151 RVA: 0x003179F4 File Offset: 0x00315BF4
	// (set) Token: 0x060098F0 RID: 39152 RVA: 0x003179FC File Offset: 0x00315BFC
	public bool ExportCollectibleCardsOnly { get; set; } = true;

	// Token: 0x170008B1 RID: 2225
	// (get) Token: 0x060098F1 RID: 39153 RVA: 0x00317A05 File Offset: 0x00315C05
	// (set) Token: 0x060098F2 RID: 39154 RVA: 0x00317A0D File Offset: 0x00315C0D
	public bool UseNeutralPrefix { get; set; }

	// Token: 0x170008B2 RID: 2226
	// (get) Token: 0x060098F3 RID: 39155 RVA: 0x00317A16 File Offset: 0x00315C16
	// (set) Token: 0x060098F4 RID: 39156 RVA: 0x00317A1E File Offset: 0x00315C1E
	public bool ExportAsHeroPortrait { get; set; }

	// Token: 0x170008B3 RID: 2227
	// (get) Token: 0x060098F5 RID: 39157 RVA: 0x00317A27 File Offset: 0x00315C27
	// (set) Token: 0x060098F6 RID: 39158 RVA: 0x00317A2F File Offset: 0x00315C2F
	public bool UseShortExportName { get; set; }

	// Token: 0x060098F7 RID: 39159 RVA: 0x00317A38 File Offset: 0x00315C38
	public void LoadSettings()
	{
		if (PlayerPrefs.HasKey("EXPORT_CARDS_SET"))
		{
			this.CardSet = (TAG_CARD_SET)PlayerPrefs.GetInt("EXPORT_CARDS_SET");
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_EXPORT_PATH"))
		{
			this.ExportPath = PlayerPrefs.GetString("EXPORT_CARDS_EXPORT_PATH");
		}
		else
		{
			this.ExportPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_RESOLUTION_X"))
		{
			this.ResolutionX = PlayerPrefs.GetInt("EXPORT_CARDS_RESOLUTION_X");
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_RESOLUTION_Y"))
		{
			this.ResolutionY = PlayerPrefs.GetInt("EXPORT_CARDS_RESOLUTION_Y");
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_GOLDEN_OPTIONS"))
		{
			this.GoldenTypeBitMask = PlayerPrefs.GetInt("EXPORT_CARDS_GOLDEN_OPTIONS");
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_LOCALE_OPTIONS"))
		{
			this.LocalesToExportBitMask = PlayerPrefs.GetInt("EXPORT_CARDS_LOCALE_OPTIONS");
		}
		else if (PlayerPrefs.HasKey("EXPORT_CARDS_LOCALE"))
		{
			int @int = PlayerPrefs.GetInt("EXPORT_CARDS_LOCALE");
			this.LocalesToExportBitMask = 1 << @int;
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_COLLECTIBLE"))
		{
			this.ExportCollectibleCardsOnly = (PlayerPrefs.GetInt("EXPORT_CARDS_COLLECTIBLE") > 0);
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_NEUTRAL_PREFIX"))
		{
			this.UseNeutralPrefix = (PlayerPrefs.GetInt("EXPORT_CARDS_NEUTRAL_PREFIX") > 0);
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_EXPORT_HERO_PORTRAIT"))
		{
			this.ExportAsHeroPortrait = (PlayerPrefs.GetInt("EXPORT_CARDS_EXPORT_HERO_PORTRAIT") > 0);
		}
		if (PlayerPrefs.HasKey("EXPORT_CARDS_USE_SHORT_EXPORT_NAME"))
		{
			this.UseShortExportName = (PlayerPrefs.GetInt("EXPORT_CARDS_USE_SHORT_EXPORT_NAME") > 0);
		}
		this.SaveSettings();
	}

	// Token: 0x060098F8 RID: 39160 RVA: 0x00317BA4 File Offset: 0x00315DA4
	public void SaveSettings()
	{
		PlayerPrefs.SetString("EXPORT_CARDS_EXPORT_PATH", this.ExportPath);
		PlayerPrefs.SetInt("EXPORT_CARDS_SET", (int)this.CardSet);
		PlayerPrefs.SetInt("EXPORT_CARDS_RESOLUTION_X", this.ResolutionX);
		PlayerPrefs.SetInt("EXPORT_CARDS_RESOLUTION_Y", this.ResolutionY);
		PlayerPrefs.SetInt("EXPORT_CARDS_GOLDEN_OPTIONS", this.GoldenTypeBitMask);
		PlayerPrefs.SetInt("EXPORT_CARDS_LOCALE_OPTIONS", this.LocalesToExportBitMask);
		PlayerPrefs.SetInt("EXPORT_CARDS_COLLECTIBLE", this.ExportCollectibleCardsOnly ? 1 : 0);
		PlayerPrefs.SetInt("EXPORT_CARDS_NEUTRAL_PREFIX", this.UseNeutralPrefix ? 1 : 0);
		PlayerPrefs.SetInt("EXPORT_CARDS_EXPORT_HERO_PORTRAIT", this.ExportAsHeroPortrait ? 1 : 0);
		PlayerPrefs.SetInt("EXPORT_CARDS_USE_SHORT_EXPORT_NAME", this.UseShortExportName ? 1 : 0);
	}

	// Token: 0x04007FD9 RID: 32729
	private const string PREFS_KEY_SET = "EXPORT_CARDS_SET";

	// Token: 0x04007FDA RID: 32730
	private const string PREFS_KEY_EXPORT_PATH = "EXPORT_CARDS_EXPORT_PATH";

	// Token: 0x04007FDB RID: 32731
	private const string PREFS_KEY_RESOLUTION_X = "EXPORT_CARDS_RESOLUTION_X";

	// Token: 0x04007FDC RID: 32732
	private const string PREFS_KEY_RESOLUTION_Y = "EXPORT_CARDS_RESOLUTION_Y";

	// Token: 0x04007FDD RID: 32733
	private const string PREFS_KEY_GOLDEN = "EXPORT_CARDS_GOLDEN_OPTIONS";

	// Token: 0x04007FDE RID: 32734
	private const string PREFS_KEY_LOCALE = "EXPORT_CARDS_LOCALE_OPTIONS";

	// Token: 0x04007FDF RID: 32735
	private const string PREFS_KEY_COLLECTIBLE = "EXPORT_CARDS_COLLECTIBLE";

	// Token: 0x04007FE0 RID: 32736
	private const string PREFS_KEY_NEUTRAL_PREFIX = "EXPORT_CARDS_NEUTRAL_PREFIX";

	// Token: 0x04007FE1 RID: 32737
	private const string PREFS_KEY_EXPORT_HERO_PORTRAIT = "EXPORT_CARDS_EXPORT_HERO_PORTRAIT";

	// Token: 0x04007FE2 RID: 32738
	private const string PREFS_KEY_USE_SHORT_EXPORT_NAME = "EXPORT_CARDS_USE_SHORT_EXPORT_NAME";

	// Token: 0x04007FE3 RID: 32739
	private const string PREFS_KEY_LOCALE_DEPRECATED = "EXPORT_CARDS_LOCALE";
}
