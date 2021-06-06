using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Assets;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Hearthstone;
using PegasusShared;
using UnityEngine;

// Token: 0x020008CA RID: 2250
public class GameStrings
{
	// Token: 0x06007C45 RID: 31813 RVA: 0x00286690 File Offset: 0x00284890
	public static void LoadAll()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (object obj in Enum.GetValues(typeof(Global.GameStringCategory)))
		{
			Global.GameStringCategory gameStringCategory = (Global.GameStringCategory)obj;
			if (gameStringCategory != Global.GameStringCategory.INVALID)
			{
				GameStrings.LoadCategory(gameStringCategory, false);
			}
		}
		float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		Log.Performance.Print(string.Format("Loading All GameStrings took {0}s)", realtimeSinceStartup2 - realtimeSinceStartup), Array.Empty<object>());
	}

	// Token: 0x06007C46 RID: 31814 RVA: 0x00286728 File Offset: 0x00284928
	public static IEnumerator<IAsyncJobResult> Job_LoadAll()
	{
		JobResultCollection jobResultCollection = new JobResultCollection(Array.Empty<IAsyncJobResult>());
		foreach (object obj in Enum.GetValues(typeof(Global.GameStringCategory)))
		{
			Global.GameStringCategory gameStringCategory = (Global.GameStringCategory)obj;
			if (gameStringCategory != Global.GameStringCategory.INVALID)
			{
				jobResultCollection.Add(GameStrings.CreateLoadCategoryJob(gameStringCategory, false));
			}
		}
		yield return jobResultCollection;
		yield break;
	}

	// Token: 0x06007C47 RID: 31815 RVA: 0x00286730 File Offset: 0x00284930
	private static IAsyncJobResult CreateLoadCategoryJob(Global.GameStringCategory category, bool native)
	{
		return new JobDefinition(string.Format("GameStrings.LoadCategory[{0}]", category), GameStrings.Job_LoadCategory(category, native), Array.Empty<IJobDependency>());
	}

	// Token: 0x06007C48 RID: 31816 RVA: 0x00286753 File Offset: 0x00284953
	private static IEnumerator<IAsyncJobResult> Job_LoadCategory(Global.GameStringCategory category, bool native)
	{
		if (GameStrings.s_tables.ContainsKey(category))
		{
			GameStrings.UnloadCategory(category);
		}
		GameStrings.LoadCategory(category, native);
		yield break;
	}

	// Token: 0x06007C49 RID: 31817 RVA: 0x0028676C File Offset: 0x0028496C
	private static void ReloadAllInternal(bool native)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (object obj in Enum.GetValues(typeof(Global.GameStringCategory)))
		{
			Global.GameStringCategory gameStringCategory = (Global.GameStringCategory)obj;
			if (gameStringCategory != Global.GameStringCategory.INVALID && (!native || GameStrings.s_nativeGameStringCatetories.Contains(gameStringCategory)))
			{
				if (GameStrings.s_tables.ContainsKey(gameStringCategory))
				{
					GameStrings.UnloadCategory(gameStringCategory);
				}
				GameStrings.LoadCategory(gameStringCategory, native);
			}
		}
		float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		Log.Performance.Print(string.Format("Reloading {0} GameStrings took {1}s)", native ? "Native" : "All", realtimeSinceStartup2 - realtimeSinceStartup), Array.Empty<object>());
	}

	// Token: 0x06007C4A RID: 31818 RVA: 0x00286838 File Offset: 0x00284A38
	public static void ReloadAll()
	{
		GameStrings.ReloadAllInternal(false);
	}

	// Token: 0x06007C4B RID: 31819 RVA: 0x00286840 File Offset: 0x00284A40
	public static void LoadNative()
	{
		GameStrings.ReloadAllInternal(true);
	}

	// Token: 0x06007C4C RID: 31820 RVA: 0x00286848 File Offset: 0x00284A48
	public static string GetAssetPath(Locale locale, string fileName, bool native = false)
	{
		if (native)
		{
			return FileUtils.GetAssetPath(string.Format("{0}/{1}/{2}", "NativeStrings", locale, fileName), false);
		}
		return FileUtils.GetAssetPath(string.Format("{0}/{1}/{2}", "Strings", locale, fileName), true);
	}

	// Token: 0x06007C4D RID: 31821 RVA: 0x00286886 File Offset: 0x00284A86
	public static bool HasKey(string key)
	{
		return GameStrings.Find(key) != null;
	}

	// Token: 0x06007C4E RID: 31822 RVA: 0x00286894 File Offset: 0x00284A94
	public static bool TryGet(string key, out string localized)
	{
		localized = null;
		string text = GameStrings.Find(key);
		if (text == null)
		{
			return false;
		}
		localized = GameStrings.ParseLanguageRules(text);
		return true;
	}

	// Token: 0x06007C4F RID: 31823 RVA: 0x002868BC File Offset: 0x00284ABC
	public static string Get(string key)
	{
		string result;
		if (!GameStrings.TryGet(key, out result))
		{
			result = key;
		}
		return result;
	}

	// Token: 0x06007C50 RID: 31824 RVA: 0x002868D8 File Offset: 0x00284AD8
	public static string Format(string key, params object[] args)
	{
		string text = GameStrings.Find(key);
		if (text == null)
		{
			return key;
		}
		return GameStrings.FormatLocalizedString(text, args);
	}

	// Token: 0x06007C51 RID: 31825 RVA: 0x002868F8 File Offset: 0x00284AF8
	public static string FormatLocalizedString(string text, params object[] args)
	{
		text = string.Format(Localization.GetCultureInfo(), text, args);
		text = GameStrings.ParseLanguageRules(text);
		return text;
	}

	// Token: 0x06007C52 RID: 31826 RVA: 0x00286911 File Offset: 0x00284B11
	public static string FormatLocalizedStringWithPlurals(string text, GameStrings.PluralNumber[] pluralNumbers, params object[] args)
	{
		text = string.Format(Localization.GetCultureInfo(), text, args);
		text = GameStrings.ParseLanguageRules(text, pluralNumbers);
		return text;
	}

	// Token: 0x06007C53 RID: 31827 RVA: 0x0028692C File Offset: 0x00284B2C
	public static string FormatPlurals(string key, GameStrings.PluralNumber[] pluralNumbers, params object[] args)
	{
		string text = GameStrings.Find(key);
		if (text == null)
		{
			return key;
		}
		text = string.Format(Localization.GetCultureInfo(), text, args);
		return GameStrings.ParseLanguageRules(text, pluralNumbers);
	}

	// Token: 0x06007C54 RID: 31828 RVA: 0x0028695C File Offset: 0x00284B5C
	public static string FormatStringWithPlurals(List<LocalizedString> protoLocalized, string stringKey, params object[] optionalFormatArgs)
	{
		Locale[] loadOrder = Localization.GetLoadOrder(false);
		LocalizedString localizedString = protoLocalized.FirstOrDefault((LocalizedString s) => s.Key == stringKey);
		LocalizedStringValue localizedStringValue = null;
		int num = 0;
		while (localizedString != null && num < loadOrder.Length)
		{
			Locale locale = loadOrder[num];
			localizedStringValue = localizedString.Values.FirstOrDefault((LocalizedStringValue v) => v.Locale == (int)locale);
			if (localizedStringValue != null)
			{
				break;
			}
			num++;
		}
		if (localizedStringValue == null || localizedStringValue.Value == null)
		{
			return null;
		}
		return GameStrings.ParseLanguageRules(string.Format(localizedStringValue.Value, optionalFormatArgs));
	}

	// Token: 0x06007C55 RID: 31829 RVA: 0x002869F3 File Offset: 0x00284BF3
	public static string ParseLanguageRules(string str)
	{
		str = GameStrings.ParseLanguageRule1(str);
		str = GameStrings.ParseLanguageRule4(str, null);
		return str;
	}

	// Token: 0x06007C56 RID: 31830 RVA: 0x00286A07 File Offset: 0x00284C07
	public static string ParseLanguageRules(string str, GameStrings.PluralNumber[] pluralNumbers)
	{
		str = GameStrings.ParseLanguageRule1(str);
		str = GameStrings.ParseLanguageRule4(str, pluralNumbers);
		return str;
	}

	// Token: 0x06007C57 RID: 31831 RVA: 0x00286A1B File Offset: 0x00284C1B
	public static bool HasClassName(TAG_CLASS tag)
	{
		return GameStrings.s_classNames.ContainsKey(tag);
	}

	// Token: 0x06007C58 RID: 31832 RVA: 0x00286A28 File Offset: 0x00284C28
	public static string GetClassName(TAG_CLASS tag)
	{
		string key = null;
		if (!GameStrings.s_classNames.TryGetValue(tag, out key))
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C59 RID: 31833 RVA: 0x00286A54 File Offset: 0x00284C54
	public static string GetClassNameKey(TAG_CLASS tag)
	{
		string result = null;
		if (!GameStrings.s_classNames.TryGetValue(tag, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06007C5A RID: 31834 RVA: 0x00286A78 File Offset: 0x00284C78
	private static KeywordTextDbfRecord GetKeywordTextRecord(GAME_TAG tag)
	{
		return GameDbf.KeywordText.GetRecord((KeywordTextDbfRecord r) => r.Tag == (int)tag);
	}

	// Token: 0x06007C5B RID: 31835 RVA: 0x00286AA8 File Offset: 0x00284CA8
	public static bool HasKeywordName(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		return keywordTextRecord != null && !string.IsNullOrEmpty(keywordTextRecord.Name);
	}

	// Token: 0x06007C5C RID: 31836 RVA: 0x00286AD0 File Offset: 0x00284CD0
	public static string GetKeywordName(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.Name == null)
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(keywordTextRecord.Name);
	}

	// Token: 0x06007C5D RID: 31837 RVA: 0x00286B00 File Offset: 0x00284D00
	public static string GetKeywordNameKey(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.Name == null)
		{
			return "UNKNOWN";
		}
		return keywordTextRecord.Name;
	}

	// Token: 0x06007C5E RID: 31838 RVA: 0x00286B2C File Offset: 0x00284D2C
	public static bool HasKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		return keywordTextRecord != null && !string.IsNullOrEmpty(keywordTextRecord.Text);
	}

	// Token: 0x06007C5F RID: 31839 RVA: 0x00286B54 File Offset: 0x00284D54
	public static string GetKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.Text == null)
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(keywordTextRecord.Text);
	}

	// Token: 0x06007C60 RID: 31840 RVA: 0x00286B84 File Offset: 0x00284D84
	public static string GetKeywordTextKey(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.Text == null)
		{
			return "UNKNOWN";
		}
		return keywordTextRecord.Text;
	}

	// Token: 0x06007C61 RID: 31841 RVA: 0x00286BB0 File Offset: 0x00284DB0
	public static bool HasRefKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		return keywordTextRecord != null && !string.IsNullOrEmpty(keywordTextRecord.RefText);
	}

	// Token: 0x06007C62 RID: 31842 RVA: 0x00286BD8 File Offset: 0x00284DD8
	public static string GetRefKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.RefText == null)
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(keywordTextRecord.RefText);
	}

	// Token: 0x06007C63 RID: 31843 RVA: 0x00286C08 File Offset: 0x00284E08
	public static string GetRefKeywordTextKey(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.RefText == null)
		{
			return "UNKNOWN";
		}
		return keywordTextRecord.RefText;
	}

	// Token: 0x06007C64 RID: 31844 RVA: 0x00286C34 File Offset: 0x00284E34
	public static bool HasCollectionKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		return keywordTextRecord != null && !string.IsNullOrEmpty(keywordTextRecord.CollectionText);
	}

	// Token: 0x06007C65 RID: 31845 RVA: 0x00286C5C File Offset: 0x00284E5C
	public static string GetCollectionKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.CollectionText == null)
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(keywordTextRecord.CollectionText);
	}

	// Token: 0x06007C66 RID: 31846 RVA: 0x00286C8C File Offset: 0x00284E8C
	public static string GetCollectionKeywordTextKey(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GameStrings.GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.CollectionText == null)
		{
			return "UNKNOWN";
		}
		return keywordTextRecord.CollectionText;
	}

	// Token: 0x06007C67 RID: 31847 RVA: 0x00286CB7 File Offset: 0x00284EB7
	public static bool HasRarityText(TAG_RARITY tag)
	{
		return GameStrings.s_rarityNames.ContainsKey(tag);
	}

	// Token: 0x06007C68 RID: 31848 RVA: 0x00286CC4 File Offset: 0x00284EC4
	public static string GetRarityText(TAG_RARITY tag)
	{
		string key = null;
		if (!GameStrings.s_rarityNames.TryGetValue(tag, out key))
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C69 RID: 31849 RVA: 0x00286CF0 File Offset: 0x00284EF0
	public static string GetRarityTextKey(TAG_RARITY tag)
	{
		string result = null;
		if (!GameStrings.s_rarityNames.TryGetValue(tag, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06007C6A RID: 31850 RVA: 0x00286D11 File Offset: 0x00284F11
	public static bool HasRaceName(TAG_RACE tag)
	{
		return GameStrings.s_raceNames.ContainsKey(tag);
	}

	// Token: 0x06007C6B RID: 31851 RVA: 0x00286D20 File Offset: 0x00284F20
	public static string GetRaceName(TAG_RACE tag)
	{
		string key = null;
		if (!GameStrings.s_raceNames.TryGetValue(tag, out key))
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C6C RID: 31852 RVA: 0x00286D4C File Offset: 0x00284F4C
	public static string GetRaceNameKey(TAG_RACE tag)
	{
		string result = null;
		if (!GameStrings.s_raceNames.TryGetValue(tag, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06007C6D RID: 31853 RVA: 0x00286D6D File Offset: 0x00284F6D
	public static bool HasRaceNameBattlegrounds(TAG_RACE tag)
	{
		return GameStrings.s_raceNamesBattlegrounds.ContainsKey(tag);
	}

	// Token: 0x06007C6E RID: 31854 RVA: 0x00286D7C File Offset: 0x00284F7C
	public static string GetRaceNameBattlegrounds(TAG_RACE tag)
	{
		string key = null;
		if (!GameStrings.s_raceNamesBattlegrounds.TryGetValue(tag, out key))
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C6F RID: 31855 RVA: 0x00286DA8 File Offset: 0x00284FA8
	public static string GetRaceNameKeyBattlegrounds(TAG_RACE tag)
	{
		string result = null;
		if (!GameStrings.s_raceNamesBattlegrounds.TryGetValue(tag, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06007C70 RID: 31856 RVA: 0x00286DC9 File Offset: 0x00284FC9
	public static bool HasCardTypeName(TAG_CARDTYPE tag)
	{
		return GameStrings.s_cardTypeNames.ContainsKey(tag);
	}

	// Token: 0x06007C71 RID: 31857 RVA: 0x00286DD8 File Offset: 0x00284FD8
	public static string GetCardTypeName(TAG_CARDTYPE tag)
	{
		string key = null;
		if (!GameStrings.s_cardTypeNames.TryGetValue(tag, out key))
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C72 RID: 31858 RVA: 0x00286E04 File Offset: 0x00285004
	public static string GetCardTypeNameKey(TAG_CARDTYPE tag)
	{
		string result = null;
		if (!GameStrings.s_cardTypeNames.TryGetValue(tag, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06007C73 RID: 31859 RVA: 0x00286E25 File Offset: 0x00285025
	public static bool HasCardSetName(TAG_CARD_SET tag)
	{
		return GameStrings.s_cardSetNames.ContainsKey(tag);
	}

	// Token: 0x06007C74 RID: 31860 RVA: 0x00286E34 File Offset: 0x00285034
	public static string GetCardSetName(TAG_CARD_SET tag)
	{
		string key = null;
		if (!GameStrings.s_cardSetNames.TryGetValue(tag, out key))
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C75 RID: 31861 RVA: 0x00286E60 File Offset: 0x00285060
	public static string GetCardSetNameKey(TAG_CARD_SET tag)
	{
		string result = null;
		if (!GameStrings.s_cardSetNames.TryGetValue(tag, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06007C76 RID: 31862 RVA: 0x00286E81 File Offset: 0x00285081
	public static bool HasCardSetNameShortened(TAG_CARD_SET tag)
	{
		return GameStrings.s_cardSetNamesShortened.ContainsKey(tag);
	}

	// Token: 0x06007C77 RID: 31863 RVA: 0x00286E90 File Offset: 0x00285090
	public static string GetCardSetNameShortened(TAG_CARD_SET tag)
	{
		string key = null;
		if (GameStrings.s_cardSetNamesShortened.TryGetValue(tag, out key))
		{
			return GameStrings.Get(key);
		}
		Log.All.PrintWarning("GetCardSetNameShortened - Could not find a Card Set name for tag {0}; returning {1}", new object[]
		{
			tag,
			"UNKNOWN"
		});
		return "UNKNOWN";
	}

	// Token: 0x06007C78 RID: 31864 RVA: 0x00286EE0 File Offset: 0x002850E0
	public static string GetCardSetNameKeyShortened(TAG_CARD_SET tag)
	{
		string result = null;
		if (!GameStrings.s_cardSetNamesShortened.TryGetValue(tag, out result))
		{
			return null;
		}
		return result;
	}

	// Token: 0x06007C79 RID: 31865 RVA: 0x00286F01 File Offset: 0x00285101
	public static bool HasCardSetNameInitials(TAG_CARD_SET tag)
	{
		return GameStrings.s_cardSetNamesInitials.ContainsKey(tag);
	}

	// Token: 0x06007C7A RID: 31866 RVA: 0x00286F10 File Offset: 0x00285110
	public static string GetCardSetNameInitials(TAG_CARD_SET tag)
	{
		string key = null;
		if (!GameStrings.s_cardSetNamesInitials.TryGetValue(tag, out key))
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C7B RID: 31867 RVA: 0x00286F3A File Offset: 0x0028513A
	public static bool HasMiniSetName(TAG_CARD_SET tag)
	{
		return GameStrings.s_miniSetNames.ContainsKey(tag);
	}

	// Token: 0x06007C7C RID: 31868 RVA: 0x00286F48 File Offset: 0x00285148
	public static string GetMiniSetName(TAG_CARD_SET tag)
	{
		string key;
		if (!GameStrings.s_miniSetNames.TryGetValue(tag, out key))
		{
			return null;
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C7D RID: 31869 RVA: 0x00286F6C File Offset: 0x0028516C
	public static bool HasMultiClassGroupName(TAG_MULTI_CLASS_GROUP tag)
	{
		return GameStrings.s_multiClassGroupNames.ContainsKey(tag);
	}

	// Token: 0x06007C7E RID: 31870 RVA: 0x00286F7C File Offset: 0x0028517C
	public static string GetMultiClassGroupName(TAG_MULTI_CLASS_GROUP tag)
	{
		string key = null;
		if (!GameStrings.s_multiClassGroupNames.TryGetValue(tag, out key))
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C7F RID: 31871 RVA: 0x00286FA6 File Offset: 0x002851A6
	public static bool HasSpellSchoolName(TAG_SPELL_SCHOOL tag)
	{
		return GameStrings.s_spellSchoolNames.ContainsKey(tag);
	}

	// Token: 0x06007C80 RID: 31872 RVA: 0x00286FB4 File Offset: 0x002851B4
	public static string GetSpellSchoolName(TAG_SPELL_SCHOOL tag)
	{
		string key = null;
		if (!GameStrings.s_spellSchoolNames.TryGetValue(tag, out key))
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C81 RID: 31873 RVA: 0x00286FDE File Offset: 0x002851DE
	public static bool HasFormatName(FormatType format)
	{
		return GameStrings.s_formatNames.ContainsKey(format);
	}

	// Token: 0x06007C82 RID: 31874 RVA: 0x00286FEC File Offset: 0x002851EC
	public static string GetFormatName(FormatType format)
	{
		string key = null;
		if (!GameStrings.s_formatNames.TryGetValue(format, out key))
		{
			return "UNKNOWN";
		}
		return GameStrings.Get(key);
	}

	// Token: 0x06007C83 RID: 31875 RVA: 0x00287018 File Offset: 0x00285218
	public static string GetRandomTip(TipCategory tipCategory)
	{
		List<string> listOfTips = GameStrings.GetListOfTips(tipCategory);
		if (listOfTips.Count == 0)
		{
			Debug.LogError(string.Format("GameStrings.GetRandomTip() - no tips in category {0}", tipCategory));
			return "UNKNOWN";
		}
		int index = UnityEngine.Random.Range(0, listOfTips.Count);
		return listOfTips[index];
	}

	// Token: 0x06007C84 RID: 31876 RVA: 0x00287064 File Offset: 0x00285264
	public static string GetTip(TipCategory tipCategory, int progress, TipCategory randomTipCategory = TipCategory.DEFAULT)
	{
		List<string> listOfTips = GameStrings.GetListOfTips(tipCategory);
		if (progress < listOfTips.Count)
		{
			return listOfTips[progress];
		}
		return GameStrings.GetRandomTip(randomTipCategory);
	}

	// Token: 0x06007C85 RID: 31877 RVA: 0x00287090 File Offset: 0x00285290
	private static List<string> GetListOfTips(TipCategory tipCategory)
	{
		int num = 0;
		List<string> list = new List<string>();
		for (;;)
		{
			string text = string.Format("GLUE_TIP_{0}_{1}", tipCategory, num);
			string text2 = GameStrings.Get(text);
			if (text2.Equals(text))
			{
				break;
			}
			if (UniversalInputManager.Get().IsTouchMode())
			{
				string text3 = text + "_TOUCH";
				string text4 = GameStrings.Get(text3);
				if (!text4.Equals(text3))
				{
					text2 = text4;
				}
				if (UniversalInputManager.UsePhoneUI)
				{
					string text5 = text + "_PHONE";
					string text6 = GameStrings.Get(text5);
					if (!text6.Equals(text5))
					{
						text2 = text6;
					}
				}
			}
			if (!string.IsNullOrEmpty(text2))
			{
				list.Add(text2);
			}
			num++;
		}
		return list;
	}

	// Token: 0x06007C86 RID: 31878 RVA: 0x00287148 File Offset: 0x00285348
	public static string GetMonthFromDigits(int monthDigits)
	{
		Locale locale = Localization.GetLocale();
		if (locale != Locale.thTH)
		{
			return Localization.GetCultureInfo().DateTimeFormat.GetMonthName(monthDigits);
		}
		switch (monthDigits)
		{
		case 1:
			return "มกราคม";
		case 2:
			return "กุมภาพันธ์";
		case 3:
			return "มีนาคม";
		case 4:
			return "เมษายน";
		case 5:
			return "พฤษภาคม";
		case 6:
			return "มิถุนายน";
		case 7:
			return "กรกฎาคม";
		case 8:
			return "สิงหาคม";
		case 9:
			return "กันยายน";
		case 10:
			return "ตุลาคม";
		case 11:
			return "พฤศจิกายน";
		case 12:
			return "ธันวาคม";
		default:
			return string.Empty;
		}
	}

	// Token: 0x06007C87 RID: 31879 RVA: 0x002871FC File Offset: 0x002853FC
	public static string GetOrdinalNumber(int number)
	{
		string text = "ORDINAL_" + number;
		string text2 = GameStrings.Get(text);
		if (text2 == text)
		{
			Debug.LogError(string.Format("GameStrings.GetOrdinalNumber() - Unable to find ordinal string for number={0}", number));
			return number.ToString();
		}
		return text2;
	}

	// Token: 0x06007C88 RID: 31880 RVA: 0x00287248 File Offset: 0x00285448
	private static bool LoadCategory(Global.GameStringCategory cat, bool native)
	{
		if (GameStrings.s_tables.ContainsKey(cat))
		{
			Debug.LogWarning(string.Format("GameStrings.LoadCategory() - {0} is already loaded", cat));
			return false;
		}
		GameStringTable gameStringTable = new GameStringTable();
		if (!gameStringTable.Load(cat, native))
		{
			Debug.LogError(string.Format("GameStrings.LoadCategory() - {0} failed to load", cat));
			return false;
		}
		if (HearthstoneApplication.IsInternal())
		{
			GameStrings.CheckConflicts(gameStringTable);
		}
		GameStrings.s_tables.Add(cat, gameStringTable);
		return true;
	}

	// Token: 0x06007C89 RID: 31881 RVA: 0x002872BA File Offset: 0x002854BA
	private static bool UnloadCategory(Global.GameStringCategory cat)
	{
		if (!GameStrings.s_tables.Remove(cat))
		{
			Debug.LogWarning(string.Format("GameStrings.UnloadCategory() - {0} was never loaded", cat));
			return false;
		}
		return true;
	}

	// Token: 0x06007C8A RID: 31882 RVA: 0x002872E4 File Offset: 0x002854E4
	private static void CheckConflicts(GameStringTable table)
	{
		Map<string, string>.KeyCollection keys = table.GetAll().Keys;
		Global.GameStringCategory category = table.GetCategory();
		foreach (GameStringTable gameStringTable in GameStrings.s_tables.Values)
		{
			foreach (string text in keys)
			{
				if (gameStringTable.Get(text) != null)
				{
					string message = string.Format("GameStrings.CheckConflicts() - Tag {0} is used in {1} and {2}. All tags must be unique.", text, category, gameStringTable.GetCategory());
					Error.AddDevWarningNonRepeating("GameStrings Error", message, Array.Empty<object>());
				}
			}
		}
	}

	// Token: 0x06007C8B RID: 31883 RVA: 0x002873BC File Offset: 0x002855BC
	private static string Find(string key)
	{
		if (key == null)
		{
			return null;
		}
		foreach (GameStringTable gameStringTable in GameStrings.s_tables.Values)
		{
			string text = gameStringTable.Get(key);
			if (text != null)
			{
				return text;
			}
		}
		if (key.StartsWith("Assets/"))
		{
			Debug.LogErrorFormat("Asset path being used as GameString key={0}", new object[]
			{
				key
			});
		}
		return null;
	}

	// Token: 0x06007C8C RID: 31884 RVA: 0x00287444 File Offset: 0x00285644
	private static string[] ParseLanguageRuleArgs(string str, int ruleIndex, out int argStartIndex, out int argEndIndex)
	{
		argStartIndex = -1;
		argEndIndex = -1;
		argStartIndex = str.IndexOf('(', ruleIndex + 2);
		if (argStartIndex < 0)
		{
			Debug.LogWarning(string.Format("GameStrings.ParseLanguageRuleArgs() - failed to parse '(' for rule at index {0} in string {1}", ruleIndex, str));
			return null;
		}
		argEndIndex = str.IndexOf(')', argStartIndex + 1);
		if (argEndIndex < 0)
		{
			Debug.LogWarning(string.Format("GameStrings.ParseLanguageRuleArgs() - failed to parse ')' for rule at index {0} in string {1}", ruleIndex, str));
			return null;
		}
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(str, argStartIndex + 1, argEndIndex - argStartIndex - 1);
		string text = stringBuilder.ToString();
		MatchCollection matchCollection = Regex.Matches(text, "(?<!\\/)(?:[0-9]+,)*[0-9]+(?!\\/)");
		if (matchCollection.Count == 0)
		{
			matchCollection = Regex.Matches(text, "(?<!\\/)(?:[0-9]+,)*[0-9]+");
		}
		if (matchCollection.Count > 0)
		{
			stringBuilder.Remove(0, stringBuilder.Length);
			int num = 0;
			foreach (object obj in matchCollection)
			{
				Match match = (Match)obj;
				stringBuilder.Append(text, num, match.Index - num);
				stringBuilder.Append('0', match.Length);
				num = match.Index + match.Length;
			}
			stringBuilder.Append(text, num, text.Length - num);
			text = stringBuilder.ToString();
		}
		string[] array = text.Split(GameStrings.LANGUAGE_RULE_ARG_DELIMITERS);
		int num2 = 0;
		for (int i = 0; i < array.Length; i++)
		{
			string text2 = array[i];
			if (matchCollection.Count > 0)
			{
				stringBuilder.Remove(0, stringBuilder.Length);
				int num3 = 0;
				foreach (object obj2 in matchCollection)
				{
					Match match2 = (Match)obj2;
					if (match2.Index >= num2 && match2.Index < num2 + text2.Length)
					{
						int num4 = match2.Index - num2;
						stringBuilder.Append(text2, num3, num4 - num3);
						stringBuilder.Append(match2.Value);
						num3 = num4 + match2.Length;
					}
				}
				stringBuilder.Append(text2, num3, text2.Length - num3);
				text2 = stringBuilder.ToString();
				num2 += text2.Length + 1;
			}
			text2 = text2.Trim();
			array[i] = text2;
		}
		return array;
	}

	// Token: 0x06007C8D RID: 31885 RVA: 0x002876C0 File Offset: 0x002858C0
	private static string ParseLanguageRule1(string str)
	{
		int i = str.IndexOf("|1");
		if (i < 0)
		{
			return str;
		}
		StringBuilder stringBuilder = new StringBuilder();
		while (i >= 0)
		{
			string text = str.Substring(0, i);
			if (text.Length == 0)
			{
				Debug.LogWarningFormat("GameStrings.ParseLanguageRule1() - invalid preStr, str:{0}, ruleIndex:{1}", new object[]
				{
					str,
					i
				});
				break;
			}
			int num = str.IndexOf('(', i);
			if (num < 0)
			{
				Debug.LogWarningFormat("GameStrings.ParseLanguageRule1() - invalid openIndex, str:{0}, ruleIndex:{1}", new object[]
				{
					str,
					i
				});
				break;
			}
			int num2 = str.IndexOf(')', num);
			if (num2 < 0)
			{
				Debug.LogWarningFormat("GameStrings.ParseLanguageRule1() - invalid closeIndex, str:{0}, ruleIndex:{1}, openIndex:{2}", new object[]
				{
					str,
					i
				});
				break;
			}
			string text2 = str.Substring(num + 1, num2 - num - 1);
			string[] array = text2.Split(new char[]
			{
				','
			});
			if (array.Length != 2)
			{
				Debug.LogWarningFormat("GameStrings.ParseLanguageRule1() - invalid args, str:{0}, argStr:{1}", new object[]
				{
					str,
					text2
				});
				break;
			}
			char c = text[text.Length - 1];
			int num3;
			if (c >= '0' && c <= '9')
			{
				switch (c)
				{
				case '0':
				case '1':
				case '3':
				case '6':
				case '7':
				case '8':
					num3 = 0;
					goto IL_19A;
				}
				num3 = 1;
			}
			else
			{
				if (c < '가' || c > '힣')
				{
					Debug.LogWarningFormat("GameStrings.ParseLanguageRule1() - invalid precedingChar, str:{0}, precedingChar:{1}", new object[]
					{
						str,
						c
					});
					break;
				}
				num3 = (((c - '가') % '\u001c' != '\0') ? 0 : 1);
			}
			IL_19A:
			stringBuilder.Append(text);
			stringBuilder.Append(array[num3]);
			str = str.Substring(num2 + 1);
			i = str.IndexOf("|1");
		}
		stringBuilder.Append(str);
		return stringBuilder.ToString();
	}

	// Token: 0x06007C8E RID: 31886 RVA: 0x002878A8 File Offset: 0x00285AA8
	private static string ParseLanguageRule4(string str, GameStrings.PluralNumber[] pluralNumbers = null)
	{
		StringBuilder stringBuilder = null;
		int? num = null;
		int num2 = 0;
		int num3 = 0;
		for (int i = str.IndexOf("|4"); i >= 0; i = str.IndexOf("|4", i + 2))
		{
			num3++;
			int num4;
			int num5;
			string[] array = GameStrings.ParseLanguageRuleArgs(str, i, out num4, out num5);
			if (array != null)
			{
				int num6 = num2;
				int num7 = i - num2;
				string text = str.Substring(num6, num7);
				GameStrings.PluralNumber pluralNumber = null;
				if (pluralNumbers != null)
				{
					int pluralArgIndex = num3 - 1;
					pluralNumber = Array.Find<GameStrings.PluralNumber>(pluralNumbers, (GameStrings.PluralNumber currPluralNumber) => currPluralNumber.m_index == pluralArgIndex);
				}
				int value;
				if (pluralNumber != null)
				{
					num = new int?(pluralNumber.m_number);
				}
				else if (GameStrings.ParseLanguageRule4Number(array, text, out value))
				{
					num = new int?(value);
				}
				else if (num == null)
				{
					Debug.LogWarning(string.Format("GameStrings.ParseLanguageRule4() - failed to parse a number in substring \"{0}\" (indexes {1}-{2}) for rule {3} in string \"{4}\"", new object[]
					{
						text,
						num6,
						num7,
						num3,
						str
					}));
					goto IL_156;
				}
				int pluralIndex = GameStrings.GetPluralIndex(num.Value);
				if (pluralIndex >= array.Length)
				{
					Debug.LogWarning(string.Format("GameStrings.ParseLanguageRule4() - not enough arguments for rule {0} in string \"{1}\"", num3, str));
				}
				else
				{
					string value2 = array[pluralIndex];
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append(text);
					stringBuilder.Append(value2);
					num2 = num5 + 1;
				}
				if (pluralNumber != null && pluralNumber.m_useForOnlyThisIndex)
				{
					num = null;
				}
			}
			IL_156:;
		}
		if (stringBuilder == null)
		{
			return str;
		}
		stringBuilder.Append(str, num2, str.Length - num2);
		return stringBuilder.ToString();
	}

	// Token: 0x06007C8F RID: 31887 RVA: 0x00287A40 File Offset: 0x00285C40
	private static bool ParseLanguageRule4Number(string[] args, string betweenRulesStr, out int number)
	{
		if (GameStrings.ParseLanguageRule4Number_Foreward(args[0], out number))
		{
			return true;
		}
		if (GameStrings.ParseLanguageRule4Number_Backward(betweenRulesStr, out number))
		{
			return true;
		}
		number = 0;
		return false;
	}

	// Token: 0x06007C90 RID: 31888 RVA: 0x00287A60 File Offset: 0x00285C60
	private static bool ParseLanguageRule4Number_Foreward(string str, out int number)
	{
		number = 0;
		Match match = Regex.Match(str, "(?<!\\/)(?:[0-9]+,)*[0-9]+(?!\\/)");
		if (!match.Success)
		{
			match = Regex.Match(str, "(?<!\\/)(?:[0-9]+,)*[0-9]+");
		}
		return match.Success && GeneralUtils.TryParseInt(match.Value, out number);
	}

	// Token: 0x06007C91 RID: 31889 RVA: 0x00287AAC File Offset: 0x00285CAC
	private static bool ParseLanguageRule4Number_Backward(string str, out int number)
	{
		number = 0;
		MatchCollection matchCollection = Regex.Matches(str, "(?<!\\/)(?:[0-9]+,)*[0-9]+(?!\\/)");
		if (matchCollection.Count == 0)
		{
			matchCollection = Regex.Matches(str, "(?<!\\/)(?:[0-9]+,)*[0-9]+");
		}
		return matchCollection.Count != 0 && GeneralUtils.TryParseInt(matchCollection[matchCollection.Count - 1].Value, out number);
	}

	// Token: 0x06007C92 RID: 31890 RVA: 0x00287B04 File Offset: 0x00285D04
	private static int GetPluralIndex(int number)
	{
		switch (Localization.GetLocale())
		{
		case Locale.frFR:
		case Locale.koKR:
		case Locale.zhTW:
		case Locale.zhCN:
			if (number <= 1)
			{
				return 0;
			}
			return 1;
		case Locale.ruRU:
		{
			int num = number % 100;
			if (num - 11 <= 3)
			{
				return 2;
			}
			num = number % 10;
			if (num == 1)
			{
				return 0;
			}
			if (num - 2 > 2)
			{
				return 2;
			}
			return 1;
		}
		case Locale.plPL:
		{
			if (number == 1)
			{
				return 0;
			}
			if (number == 0)
			{
				return 2;
			}
			int num = number % 100;
			if (num - 11 <= 3)
			{
				return 2;
			}
			num = number % 10;
			if (num - 2 <= 2)
			{
				return 1;
			}
			return 2;
		}
		}
		if (number == 1)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x0400654F RID: 25935
	public const string s_UnknownName = "UNKNOWN";

	// Token: 0x04006550 RID: 25936
	private static Map<Global.GameStringCategory, GameStringTable> s_tables = new Map<Global.GameStringCategory, GameStringTable>();

	// Token: 0x04006551 RID: 25937
	private static readonly char[] LANGUAGE_RULE_ARG_DELIMITERS = new char[]
	{
		','
	};

	// Token: 0x04006552 RID: 25938
	private static List<Global.GameStringCategory> s_nativeGameStringCatetories = new List<Global.GameStringCategory>
	{
		Global.GameStringCategory.GLOBAL,
		Global.GameStringCategory.GLUE
	};

	// Token: 0x04006553 RID: 25939
	private const string NUMBER_PATTERN = "(?<!\\/)(?:[0-9]+,)*[0-9]+(?!\\/)";

	// Token: 0x04006554 RID: 25940
	private const string NUMBER_PATTERN_ALT = "(?<!\\/)(?:[0-9]+,)*[0-9]+";

	// Token: 0x04006555 RID: 25941
	public static Map<TAG_CLASS, string> s_classNames = new Map<TAG_CLASS, string>
	{
		{
			TAG_CLASS.DEATHKNIGHT,
			"GLOBAL_CLASS_DEATHKNIGHT"
		},
		{
			TAG_CLASS.DRUID,
			"GLOBAL_CLASS_DRUID"
		},
		{
			TAG_CLASS.HUNTER,
			"GLOBAL_CLASS_HUNTER"
		},
		{
			TAG_CLASS.MAGE,
			"GLOBAL_CLASS_MAGE"
		},
		{
			TAG_CLASS.PALADIN,
			"GLOBAL_CLASS_PALADIN"
		},
		{
			TAG_CLASS.PRIEST,
			"GLOBAL_CLASS_PRIEST"
		},
		{
			TAG_CLASS.ROGUE,
			"GLOBAL_CLASS_ROGUE"
		},
		{
			TAG_CLASS.SHAMAN,
			"GLOBAL_CLASS_SHAMAN"
		},
		{
			TAG_CLASS.WARLOCK,
			"GLOBAL_CLASS_WARLOCK"
		},
		{
			TAG_CLASS.WARRIOR,
			"GLOBAL_CLASS_WARRIOR"
		},
		{
			TAG_CLASS.DEMONHUNTER,
			"GLOBAL_CLASS_DEMONHUNTER"
		},
		{
			TAG_CLASS.NEUTRAL,
			"GLOBAL_CLASS_NEUTRAL"
		}
	};

	// Token: 0x04006556 RID: 25942
	public static Map<TAG_RACE, string> s_raceNames = new Map<TAG_RACE, string>
	{
		{
			TAG_RACE.BLOODELF,
			"GLOBAL_RACE_BLOODELF"
		},
		{
			TAG_RACE.DRAENEI,
			"GLOBAL_RACE_DRAENEI"
		},
		{
			TAG_RACE.DWARF,
			"GLOBAL_RACE_DWARF"
		},
		{
			TAG_RACE.GNOME,
			"GLOBAL_RACE_GNOME"
		},
		{
			TAG_RACE.GOBLIN,
			"GLOBAL_RACE_GOBLIN"
		},
		{
			TAG_RACE.HUMAN,
			"GLOBAL_RACE_HUMAN"
		},
		{
			TAG_RACE.NIGHTELF,
			"GLOBAL_RACE_NIGHTELF"
		},
		{
			TAG_RACE.ORC,
			"GLOBAL_RACE_ORC"
		},
		{
			TAG_RACE.TAUREN,
			"GLOBAL_RACE_TAUREN"
		},
		{
			TAG_RACE.TROLL,
			"GLOBAL_RACE_TROLL"
		},
		{
			TAG_RACE.UNDEAD,
			"GLOBAL_RACE_UNDEAD"
		},
		{
			TAG_RACE.WORGEN,
			"GLOBAL_RACE_WORGEN"
		},
		{
			TAG_RACE.MURLOC,
			"GLOBAL_RACE_MURLOC"
		},
		{
			TAG_RACE.DEMON,
			"GLOBAL_RACE_DEMON"
		},
		{
			TAG_RACE.SCOURGE,
			"GLOBAL_RACE_SCOURGE"
		},
		{
			TAG_RACE.MECHANICAL,
			"GLOBAL_RACE_MECHANICAL"
		},
		{
			TAG_RACE.ELEMENTAL,
			"GLOBAL_RACE_ELEMENTAL"
		},
		{
			TAG_RACE.OGRE,
			"GLOBAL_RACE_OGRE"
		},
		{
			TAG_RACE.PET,
			"GLOBAL_RACE_PET"
		},
		{
			TAG_RACE.TOTEM,
			"GLOBAL_RACE_TOTEM"
		},
		{
			TAG_RACE.NERUBIAN,
			"GLOBAL_RACE_NERUBIAN"
		},
		{
			TAG_RACE.PIRATE,
			"GLOBAL_RACE_PIRATE"
		},
		{
			TAG_RACE.DRAGON,
			"GLOBAL_RACE_DRAGON"
		},
		{
			TAG_RACE.ALL,
			"GLOBAL_RACE_ALL"
		},
		{
			TAG_RACE.EGG,
			"GLOBAL_RACE_EGG"
		},
		{
			TAG_RACE.QUILBOAR,
			"GLOBAL_RACE_QUILBOAR"
		}
	};

	// Token: 0x04006557 RID: 25943
	public static Map<TAG_RACE, string> s_raceNamesBattlegrounds = new Map<TAG_RACE, string>
	{
		{
			TAG_RACE.BLOODELF,
			"GLOBAL_RACE_BLOODELF_BATTLEGROUNDS"
		},
		{
			TAG_RACE.DRAENEI,
			"GLOBAL_RACE_DRAENEI_BATTLEGROUNDS"
		},
		{
			TAG_RACE.DWARF,
			"GLOBAL_RACE_DWARF_BATTLEGROUNDS"
		},
		{
			TAG_RACE.GNOME,
			"GLOBAL_RACE_GNOME_BATTLEGROUNDS"
		},
		{
			TAG_RACE.GOBLIN,
			"GLOBAL_RACE_GOBLIN_BATTLEGROUNDS"
		},
		{
			TAG_RACE.HUMAN,
			"GLOBAL_RACE_HUMAN_BATTLEGROUNDS"
		},
		{
			TAG_RACE.NIGHTELF,
			"GLOBAL_RACE_NIGHTELF_BATTLEGROUNDS"
		},
		{
			TAG_RACE.ORC,
			"GLOBAL_RACE_ORC_BATTLEGROUNDS"
		},
		{
			TAG_RACE.TAUREN,
			"GLOBAL_RACE_TAUREN_BATTLEGROUNDS"
		},
		{
			TAG_RACE.TROLL,
			"GLOBAL_RACE_TROLL_BATTLEGROUNDS"
		},
		{
			TAG_RACE.UNDEAD,
			"GLOBAL_RACE_UNDEAD_BATTLEGROUNDS"
		},
		{
			TAG_RACE.WORGEN,
			"GLOBAL_RACE_WORGEN_BATTLEGROUNDS"
		},
		{
			TAG_RACE.MURLOC,
			"GLOBAL_RACE_MURLOC_BATTLEGROUNDS"
		},
		{
			TAG_RACE.DEMON,
			"GLOBAL_RACE_DEMON_BATTLEGROUNDS"
		},
		{
			TAG_RACE.SCOURGE,
			"GLOBAL_RACE_SCOURGE_BATTLEGROUNDS"
		},
		{
			TAG_RACE.MECHANICAL,
			"GLOBAL_RACE_MECHANICAL_BATTLEGROUNDS"
		},
		{
			TAG_RACE.ELEMENTAL,
			"GLOBAL_RACE_ELEMENTAL_BATTLEGROUNDS"
		},
		{
			TAG_RACE.OGRE,
			"GLOBAL_RACE_OGRE_BATTLEGROUNDS"
		},
		{
			TAG_RACE.PET,
			"GLOBAL_RACE_PET_BATTLEGROUNDS"
		},
		{
			TAG_RACE.TOTEM,
			"GLOBAL_RACE_TOTEM_BATTLEGROUNDS"
		},
		{
			TAG_RACE.NERUBIAN,
			"GLOBAL_RACE_NERUBIAN_BATTLEGROUNDS"
		},
		{
			TAG_RACE.PIRATE,
			"GLOBAL_RACE_PIRATE_BATTLEGROUNDS"
		},
		{
			TAG_RACE.DRAGON,
			"GLOBAL_RACE_DRAGON_BATTLEGROUNDS"
		},
		{
			TAG_RACE.ALL,
			"GLOBAL_RACE_ALL_BATTLEGROUNDS"
		},
		{
			TAG_RACE.EGG,
			"GLOBAL_RACE_EGG_BATTLEGROUNDS"
		},
		{
			TAG_RACE.QUILBOAR,
			"GLOBAL_RACE_QUILBOARS_BATTLEGROUNDS"
		}
	};

	// Token: 0x04006558 RID: 25944
	public static Map<TAG_RARITY, string> s_rarityNames = new Map<TAG_RARITY, string>
	{
		{
			TAG_RARITY.COMMON,
			"GLOBAL_RARITY_COMMON"
		},
		{
			TAG_RARITY.EPIC,
			"GLOBAL_RARITY_EPIC"
		},
		{
			TAG_RARITY.LEGENDARY,
			"GLOBAL_RARITY_LEGENDARY"
		},
		{
			TAG_RARITY.RARE,
			"GLOBAL_RARITY_RARE"
		},
		{
			TAG_RARITY.FREE,
			"GLOBAL_RARITY_FREE"
		}
	};

	// Token: 0x04006559 RID: 25945
	public static Map<TAG_CARD_SET, string> s_cardSetNames = new Map<TAG_CARD_SET, string>
	{
		{
			TAG_CARD_SET.BASIC,
			"GLOBAL_CARD_SET_BASIC"
		},
		{
			TAG_CARD_SET.EXPERT1,
			"GLOBAL_CARD_SET_EXPERT1"
		},
		{
			TAG_CARD_SET.HOF,
			"GLOBAL_CARD_SET_HOF"
		},
		{
			TAG_CARD_SET.PROMO,
			"GLOBAL_CARD_SET_PROMO"
		},
		{
			TAG_CARD_SET.FP1,
			"GLOBAL_CARD_SET_NAXX"
		},
		{
			TAG_CARD_SET.PE1,
			"GLOBAL_CARD_SET_GVG"
		},
		{
			TAG_CARD_SET.BRM,
			"GLOBAL_CARD_SET_BRM"
		},
		{
			TAG_CARD_SET.TGT,
			"GLOBAL_CARD_SET_TGT"
		},
		{
			TAG_CARD_SET.LOE,
			"GLOBAL_CARD_SET_LOE"
		},
		{
			TAG_CARD_SET.OG,
			"GLOBAL_CARD_SET_OG"
		},
		{
			TAG_CARD_SET.OG_RESERVE,
			"GLOBAL_CARD_SET_OG_RESERVE"
		},
		{
			TAG_CARD_SET.SLUSH,
			"GLOBAL_CARD_SET_DEBUG"
		},
		{
			TAG_CARD_SET.KARA,
			"GLOBAL_CARD_SET_KARA"
		},
		{
			TAG_CARD_SET.KARA_RESERVE,
			"GLOBAL_CARD_SET_KARA_RESERVE"
		},
		{
			TAG_CARD_SET.GANGS,
			"GLOBAL_CARD_SET_GANGS"
		},
		{
			TAG_CARD_SET.GANGS_RESERVE,
			"GLOBAL_CARD_SET_GANGS_RESERVE"
		},
		{
			TAG_CARD_SET.UNGORO,
			"GLOBAL_CARD_SET_UNGORO"
		},
		{
			TAG_CARD_SET.ICECROWN,
			"GLOBAL_CARD_SET_ICECROWN"
		},
		{
			TAG_CARD_SET.LOOTAPALOOZA,
			"GLOBAL_CARD_SET_LOOTAPALOOZA"
		},
		{
			TAG_CARD_SET.GILNEAS,
			"GLOBAL_CARD_SET_GILNEAS"
		},
		{
			TAG_CARD_SET.BOOMSDAY,
			"GLOBAL_CARD_SET_BOOMSDAY"
		},
		{
			TAG_CARD_SET.TROLL,
			"GLOBAL_CARD_SET_TROLL"
		},
		{
			TAG_CARD_SET.DALARAN,
			"GLOBAL_CARD_SET_DALARAN"
		},
		{
			TAG_CARD_SET.ULDUM,
			"GLOBAL_CARD_SET_ULDUM"
		},
		{
			TAG_CARD_SET.WILD_EVENT,
			"GLOBAL_CARD_SET_WILD_EVENT"
		},
		{
			TAG_CARD_SET.DRAGONS,
			"GLOBAL_CARD_SET_DRG"
		},
		{
			TAG_CARD_SET.YEAR_OF_THE_DRAGON,
			"GLOBAL_CARD_SET_YOD"
		},
		{
			TAG_CARD_SET.BLACK_TEMPLE,
			"GLOBAL_CARD_SET_BT"
		},
		{
			TAG_CARD_SET.DEMON_HUNTER_INITIATE,
			"GLOBAL_CARD_SET_DHI"
		},
		{
			TAG_CARD_SET.SCHOLOMANCE,
			"GLOBAL_CARD_SET_SCH"
		},
		{
			TAG_CARD_SET.DARKMOON_FAIRE,
			"GLOBAL_CARD_SET_DMF"
		},
		{
			TAG_CARD_SET.THE_BARRENS,
			"GLOBAL_CARD_SET_BAR"
		},
		{
			TAG_CARD_SET.WAILING_CAVERNS,
			"GLOBAL_CARD_SET_WC"
		},
		{
			TAG_CARD_SET.LEGACY,
			"GLOBAL_CARD_SET_LEGACY"
		},
		{
			TAG_CARD_SET.CORE,
			"GLOBAL_CARD_SET_CORE"
		},
		{
			TAG_CARD_SET.VANILLA,
			"GLOBAL_CARD_SET_VANILLA"
		}
	};

	// Token: 0x0400655A RID: 25946
	public static Map<TAG_CARD_SET, string> s_cardSetNamesShortened = new Map<TAG_CARD_SET, string>
	{
		{
			TAG_CARD_SET.BASIC,
			"GLOBAL_CARD_SET_BASIC"
		},
		{
			TAG_CARD_SET.EXPERT1,
			"GLOBAL_CARD_SET_EXPERT1"
		},
		{
			TAG_CARD_SET.HOF,
			"GLOBAL_CARD_SET_HOF"
		},
		{
			TAG_CARD_SET.PROMO,
			"GLOBAL_CARD_SET_PROMO"
		},
		{
			TAG_CARD_SET.FP1,
			"GLOBAL_CARD_SET_NAXX"
		},
		{
			TAG_CARD_SET.PE1,
			"GLOBAL_CARD_SET_GVG"
		},
		{
			TAG_CARD_SET.BRM,
			"GLOBAL_CARD_SET_BRM"
		},
		{
			TAG_CARD_SET.TGT,
			"GLOBAL_CARD_SET_TGT_SHORT"
		},
		{
			TAG_CARD_SET.LOE,
			"GLOBAL_CARD_SET_LOE_SHORT"
		},
		{
			TAG_CARD_SET.OG,
			"GLOBAL_CARD_SET_OG_SHORT"
		},
		{
			TAG_CARD_SET.OG_RESERVE,
			"GLOBAL_CARD_SET_OG_RESERVE"
		},
		{
			TAG_CARD_SET.SLUSH,
			"GLOBAL_CARD_SET_DEBUG"
		},
		{
			TAG_CARD_SET.KARA,
			"GLOBAL_CARD_SET_KARA_SHORT"
		},
		{
			TAG_CARD_SET.KARA_RESERVE,
			"GLOBAL_CARD_SET_KARA_RESERVE"
		},
		{
			TAG_CARD_SET.GANGS,
			"GLOBAL_CARD_SET_GANGS_SHORT"
		},
		{
			TAG_CARD_SET.GANGS_RESERVE,
			"GLOBAL_CARD_SET_GANGS_RESERVE"
		},
		{
			TAG_CARD_SET.UNGORO,
			"GLOBAL_CARD_SET_UNGORO_SHORT"
		},
		{
			TAG_CARD_SET.ICECROWN,
			"GLOBAL_CARD_SET_ICECROWN_SHORT"
		},
		{
			TAG_CARD_SET.LOOTAPALOOZA,
			"GLOBAL_CARD_SET_LOOTAPALOOZA_SHORT"
		},
		{
			TAG_CARD_SET.GILNEAS,
			"GLOBAL_CARD_SET_GILNEAS_SHORT"
		},
		{
			TAG_CARD_SET.BOOMSDAY,
			"GLOBAL_CARD_SET_BOOMSDAY_SHORT"
		},
		{
			TAG_CARD_SET.TROLL,
			"GLOBAL_CARD_SET_TROLL_SHORT"
		},
		{
			TAG_CARD_SET.DALARAN,
			"GLOBAL_CARD_SET_DALARAN_SHORT"
		},
		{
			TAG_CARD_SET.ULDUM,
			"GLOBAL_CARD_SET_ULDUM_SHORT"
		},
		{
			TAG_CARD_SET.WILD_EVENT,
			"GLOBAL_CARD_SET_WILD_EVENT_SHORT"
		},
		{
			TAG_CARD_SET.DRAGONS,
			"GLOBAL_CARD_SET_DRG_SHORT"
		},
		{
			TAG_CARD_SET.YEAR_OF_THE_DRAGON,
			"GLOBAL_CARD_SET_YOD_SHORT"
		},
		{
			TAG_CARD_SET.BLACK_TEMPLE,
			"GLOBAL_CARD_SET_BT_SHORT"
		},
		{
			TAG_CARD_SET.DEMON_HUNTER_INITIATE,
			"GLOBAL_CARD_SET_DHI_SHORT"
		},
		{
			TAG_CARD_SET.SCHOLOMANCE,
			"GLOBAL_CARD_SET_SCH_SHORT"
		},
		{
			TAG_CARD_SET.DARKMOON_FAIRE,
			"GLOBAL_CARD_SET_DMF_SHORT"
		},
		{
			TAG_CARD_SET.THE_BARRENS,
			"GLOBAL_CARD_SET_BAR_SHORT"
		},
		{
			TAG_CARD_SET.WAILING_CAVERNS,
			"GLOBAL_CARD_SET_WC_SHORT"
		},
		{
			TAG_CARD_SET.LEGACY,
			"GLOBAL_CARD_SET_LEGACY_SHORT"
		},
		{
			TAG_CARD_SET.CORE,
			"GLOBAL_CARD_SET_CORE_SHORT"
		},
		{
			TAG_CARD_SET.VANILLA,
			"GLOBAL_CARD_SET_VANILLA_SHORT"
		}
	};

	// Token: 0x0400655B RID: 25947
	public static Map<TAG_CARD_SET, string> s_cardSetNamesInitials = new Map<TAG_CARD_SET, string>
	{
		{
			TAG_CARD_SET.FP1,
			"GLOBAL_CARD_SET_NAXX_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.PE1,
			"GLOBAL_CARD_SET_GVG_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.BRM,
			"GLOBAL_CARD_SET_BRM_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.TGT,
			"GLOBAL_CARD_SET_TGT_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.LOE,
			"GLOBAL_CARD_SET_LOE_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.OG,
			"GLOBAL_CARD_SET_OG_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.GANGS,
			"GLOBAL_CARD_SET_GANGS_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.LOOTAPALOOZA,
			"GLOBAL_CARD_SET_LOOTAPALOOZA_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.BOOMSDAY,
			"GLOBAL_CARD_SET_BOOMSDAY_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.TROLL,
			"GLOBAL_CARD_SET_TROLL_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.DALARAN,
			"GLOBAL_CARD_SET_DALARAN_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.ULDUM,
			"GLOBAL_CARD_SET_ULDUM_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.DRAGONS,
			"GLOBAL_CARD_SET_DRG_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.BLACK_TEMPLE,
			"GLOBAL_CARD_SET_BT_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.DEMON_HUNTER_INITIATE,
			"GLOBAL_CARD_SET_DHI_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.SCHOLOMANCE,
			"GLOBAL_CARD_SET_SCH_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.DARKMOON_FAIRE,
			"GLOBAL_CARD_SET_DMF_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.THE_BARRENS,
			"GLOBAL_CARD_SET_BAR_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.WAILING_CAVERNS,
			"GLOBAL_CARD_SET_WC_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.LEGACY,
			"GLOBAL_CARD_SET_LEGACY_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.CORE,
			"GLOBAL_CARD_SET_CORE_SEARCHABLE_SHORTHAND_NAMES"
		},
		{
			TAG_CARD_SET.VANILLA,
			"GLOBAL_CARD_SET_VANILLA_SEARCHABLE_SHORTHAND_NAMES"
		}
	};

	// Token: 0x0400655C RID: 25948
	public static Map<TAG_CARD_SET, string> s_miniSetNames = new Map<TAG_CARD_SET, string>
	{
		{
			TAG_CARD_SET.DARKMOON_FAIRE,
			"GLOBAL_MINI_SET_DMF"
		},
		{
			TAG_CARD_SET.THE_BARRENS,
			"GLOBAL_MINI_SET_BAR"
		}
	};

	// Token: 0x0400655D RID: 25949
	public static Map<TAG_CARDTYPE, string> s_cardTypeNames = new Map<TAG_CARDTYPE, string>
	{
		{
			TAG_CARDTYPE.HERO,
			"GLOBAL_CARDTYPE_HERO"
		},
		{
			TAG_CARDTYPE.MINION,
			"GLOBAL_CARDTYPE_MINION"
		},
		{
			TAG_CARDTYPE.SPELL,
			"GLOBAL_CARDTYPE_SPELL"
		},
		{
			TAG_CARDTYPE.ENCHANTMENT,
			"GLOBAL_CARDTYPE_ENCHANTMENT"
		},
		{
			TAG_CARDTYPE.WEAPON,
			"GLOBAL_CARDTYPE_WEAPON"
		},
		{
			TAG_CARDTYPE.ITEM,
			"GLOBAL_CARDTYPE_ITEM"
		},
		{
			TAG_CARDTYPE.TOKEN,
			"GLOBAL_CARDTYPE_TOKEN"
		},
		{
			TAG_CARDTYPE.HERO_POWER,
			"GLOBAL_CARDTYPE_HEROPOWER"
		}
	};

	// Token: 0x0400655E RID: 25950
	public static Map<TAG_MULTI_CLASS_GROUP, string> s_multiClassGroupNames = new Map<TAG_MULTI_CLASS_GROUP, string>
	{
		{
			TAG_MULTI_CLASS_GROUP.GRIMY_GOONS,
			"GLOBAL_KEYWORD_GRIMY_GOONS"
		},
		{
			TAG_MULTI_CLASS_GROUP.JADE_LOTUS,
			"GLOBAL_KEYWORD_JADE_LOTUS"
		},
		{
			TAG_MULTI_CLASS_GROUP.KABAL,
			"GLOBAL_KEYWORD_KABAL"
		}
	};

	// Token: 0x0400655F RID: 25951
	public static Map<TAG_SPELL_SCHOOL, string> s_spellSchoolNames = new Map<TAG_SPELL_SCHOOL, string>
	{
		{
			TAG_SPELL_SCHOOL.ARCANE,
			"GLOBAL_SPELL_SCHOOL_ARCANE"
		},
		{
			TAG_SPELL_SCHOOL.FIRE,
			"GLOBAL_SPELL_SCHOOL_FIRE"
		},
		{
			TAG_SPELL_SCHOOL.FROST,
			"GLOBAL_SPELL_SCHOOL_FROST"
		},
		{
			TAG_SPELL_SCHOOL.NATURE,
			"GLOBAL_SPELL_SCHOOL_NATURE"
		},
		{
			TAG_SPELL_SCHOOL.HOLY,
			"GLOBAL_SPELL_SCHOOL_HOLY"
		},
		{
			TAG_SPELL_SCHOOL.SHADOW,
			"GLOBAL_SPELL_SCHOOL_SHADOW"
		},
		{
			TAG_SPELL_SCHOOL.FEL,
			"GLOBAL_SPELL_SCHOOL_FEL"
		},
		{
			TAG_SPELL_SCHOOL.PHYSICAL_COMBAT,
			"GLOBAL_SPELL_SCHOOL_PHYSICAL_COMBAT"
		}
	};

	// Token: 0x04006560 RID: 25952
	public static Map<FormatType, string> s_formatNames = new Map<FormatType, string>
	{
		{
			FormatType.FT_STANDARD,
			"GLOBAL_STANDARD"
		},
		{
			FormatType.FT_WILD,
			"GLOBAL_WILD"
		},
		{
			FormatType.FT_CLASSIC,
			"GLOBAL_CLASSIC"
		}
	};

	// Token: 0x0200254A RID: 9546
	public class PluralNumber
	{
		// Token: 0x0400ED1C RID: 60700
		public int m_index;

		// Token: 0x0400ED1D RID: 60701
		public int m_number;

		// Token: 0x0400ED1E RID: 60702
		public bool m_useForOnlyThisIndex;
	}
}
