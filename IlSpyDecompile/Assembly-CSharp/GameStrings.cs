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

public class GameStrings
{
	public class PluralNumber
	{
		public int m_index;

		public int m_number;

		public bool m_useForOnlyThisIndex;
	}

	public const string s_UnknownName = "UNKNOWN";

	private static Map<Global.GameStringCategory, GameStringTable> s_tables = new Map<Global.GameStringCategory, GameStringTable>();

	private static readonly char[] LANGUAGE_RULE_ARG_DELIMITERS = new char[1] { ',' };

	private static List<Global.GameStringCategory> s_nativeGameStringCatetories = new List<Global.GameStringCategory>
	{
		Global.GameStringCategory.GLOBAL,
		Global.GameStringCategory.GLUE
	};

	private const string NUMBER_PATTERN = "(?<!\\/)(?:[0-9]+,)*[0-9]+(?!\\/)";

	private const string NUMBER_PATTERN_ALT = "(?<!\\/)(?:[0-9]+,)*[0-9]+";

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

	public static void LoadAll()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (Global.GameStringCategory value in Enum.GetValues(typeof(Global.GameStringCategory)))
		{
			if (value != 0)
			{
				LoadCategory(value, native: false);
			}
		}
		float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		Log.Performance.Print($"Loading All GameStrings took {realtimeSinceStartup2 - realtimeSinceStartup}s)");
	}

	public static IEnumerator<IAsyncJobResult> Job_LoadAll()
	{
		JobResultCollection jobResultCollection = new JobResultCollection();
		foreach (Global.GameStringCategory value in Enum.GetValues(typeof(Global.GameStringCategory)))
		{
			if (value != 0)
			{
				jobResultCollection.Add(CreateLoadCategoryJob(value, native: false));
			}
		}
		yield return jobResultCollection;
	}

	private static IAsyncJobResult CreateLoadCategoryJob(Global.GameStringCategory category, bool native)
	{
		return new JobDefinition($"GameStrings.LoadCategory[{category}]", Job_LoadCategory(category, native));
	}

	private static IEnumerator<IAsyncJobResult> Job_LoadCategory(Global.GameStringCategory category, bool native)
	{
		if (s_tables.ContainsKey(category))
		{
			UnloadCategory(category);
		}
		LoadCategory(category, native);
		yield break;
	}

	private static void ReloadAllInternal(bool native)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (Global.GameStringCategory value in Enum.GetValues(typeof(Global.GameStringCategory)))
		{
			if (value != 0 && (!native || s_nativeGameStringCatetories.Contains(value)))
			{
				if (s_tables.ContainsKey(value))
				{
					UnloadCategory(value);
				}
				LoadCategory(value, native);
			}
		}
		float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		Log.Performance.Print(string.Format("Reloading {0} GameStrings took {1}s)", native ? "Native" : "All", realtimeSinceStartup2 - realtimeSinceStartup));
	}

	public static void ReloadAll()
	{
		ReloadAllInternal(native: false);
	}

	public static void LoadNative()
	{
		ReloadAllInternal(native: true);
	}

	public static string GetAssetPath(Locale locale, string fileName, bool native = false)
	{
		if (native)
		{
			return FileUtils.GetAssetPath(string.Format("{0}/{1}/{2}", "NativeStrings", locale, fileName), useAssetBundleFolder: false);
		}
		return FileUtils.GetAssetPath(string.Format("{0}/{1}/{2}", "Strings", locale, fileName));
	}

	public static bool HasKey(string key)
	{
		return Find(key) != null;
	}

	public static bool TryGet(string key, out string localized)
	{
		localized = null;
		string text = Find(key);
		if (text == null)
		{
			return false;
		}
		localized = ParseLanguageRules(text);
		return true;
	}

	public static string Get(string key)
	{
		if (!TryGet(key, out var localized))
		{
			return key;
		}
		return localized;
	}

	public static string Format(string key, params object[] args)
	{
		string text = Find(key);
		if (text == null)
		{
			return key;
		}
		return FormatLocalizedString(text, args);
	}

	public static string FormatLocalizedString(string text, params object[] args)
	{
		text = string.Format(Localization.GetCultureInfo(), text, args);
		text = ParseLanguageRules(text);
		return text;
	}

	public static string FormatLocalizedStringWithPlurals(string text, PluralNumber[] pluralNumbers, params object[] args)
	{
		text = string.Format(Localization.GetCultureInfo(), text, args);
		text = ParseLanguageRules(text, pluralNumbers);
		return text;
	}

	public static string FormatPlurals(string key, PluralNumber[] pluralNumbers, params object[] args)
	{
		string text = Find(key);
		if (text == null)
		{
			return key;
		}
		text = string.Format(Localization.GetCultureInfo(), text, args);
		return ParseLanguageRules(text, pluralNumbers);
	}

	public static string FormatStringWithPlurals(List<LocalizedString> protoLocalized, string stringKey, params object[] optionalFormatArgs)
	{
		Locale[] loadOrder = Localization.GetLoadOrder();
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
		return ParseLanguageRules(string.Format(localizedStringValue.Value, optionalFormatArgs));
	}

	public static string ParseLanguageRules(string str)
	{
		str = ParseLanguageRule1(str);
		str = ParseLanguageRule4(str);
		return str;
	}

	public static string ParseLanguageRules(string str, PluralNumber[] pluralNumbers)
	{
		str = ParseLanguageRule1(str);
		str = ParseLanguageRule4(str, pluralNumbers);
		return str;
	}

	public static bool HasClassName(TAG_CLASS tag)
	{
		return s_classNames.ContainsKey(tag);
	}

	public static string GetClassName(TAG_CLASS tag)
	{
		string value = null;
		if (!s_classNames.TryGetValue(tag, out value))
		{
			return "UNKNOWN";
		}
		return Get(value);
	}

	public static string GetClassNameKey(TAG_CLASS tag)
	{
		string value = null;
		if (!s_classNames.TryGetValue(tag, out value))
		{
			return null;
		}
		return value;
	}

	private static KeywordTextDbfRecord GetKeywordTextRecord(GAME_TAG tag)
	{
		return GameDbf.KeywordText.GetRecord((KeywordTextDbfRecord r) => r.Tag == (int)tag);
	}

	public static bool HasKeywordName(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || string.IsNullOrEmpty(keywordTextRecord.Name))
		{
			return false;
		}
		return true;
	}

	public static string GetKeywordName(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.Name == null)
		{
			return "UNKNOWN";
		}
		return Get(keywordTextRecord.Name);
	}

	public static string GetKeywordNameKey(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.Name == null)
		{
			return "UNKNOWN";
		}
		return keywordTextRecord.Name;
	}

	public static bool HasKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || string.IsNullOrEmpty(keywordTextRecord.Text))
		{
			return false;
		}
		return true;
	}

	public static string GetKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.Text == null)
		{
			return "UNKNOWN";
		}
		return Get(keywordTextRecord.Text);
	}

	public static string GetKeywordTextKey(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.Text == null)
		{
			return "UNKNOWN";
		}
		return keywordTextRecord.Text;
	}

	public static bool HasRefKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || string.IsNullOrEmpty(keywordTextRecord.RefText))
		{
			return false;
		}
		return true;
	}

	public static string GetRefKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.RefText == null)
		{
			return "UNKNOWN";
		}
		return Get(keywordTextRecord.RefText);
	}

	public static string GetRefKeywordTextKey(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.RefText == null)
		{
			return "UNKNOWN";
		}
		return keywordTextRecord.RefText;
	}

	public static bool HasCollectionKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || string.IsNullOrEmpty(keywordTextRecord.CollectionText))
		{
			return false;
		}
		return true;
	}

	public static string GetCollectionKeywordText(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.CollectionText == null)
		{
			return "UNKNOWN";
		}
		return Get(keywordTextRecord.CollectionText);
	}

	public static string GetCollectionKeywordTextKey(GAME_TAG tag)
	{
		KeywordTextDbfRecord keywordTextRecord = GetKeywordTextRecord(tag);
		if (keywordTextRecord == null || keywordTextRecord.CollectionText == null)
		{
			return "UNKNOWN";
		}
		return keywordTextRecord.CollectionText;
	}

	public static bool HasRarityText(TAG_RARITY tag)
	{
		return s_rarityNames.ContainsKey(tag);
	}

	public static string GetRarityText(TAG_RARITY tag)
	{
		string value = null;
		if (!s_rarityNames.TryGetValue(tag, out value))
		{
			return "UNKNOWN";
		}
		return Get(value);
	}

	public static string GetRarityTextKey(TAG_RARITY tag)
	{
		string value = null;
		if (!s_rarityNames.TryGetValue(tag, out value))
		{
			return null;
		}
		return value;
	}

	public static bool HasRaceName(TAG_RACE tag)
	{
		return s_raceNames.ContainsKey(tag);
	}

	public static string GetRaceName(TAG_RACE tag)
	{
		string value = null;
		if (!s_raceNames.TryGetValue(tag, out value))
		{
			return "UNKNOWN";
		}
		return Get(value);
	}

	public static string GetRaceNameKey(TAG_RACE tag)
	{
		string value = null;
		if (!s_raceNames.TryGetValue(tag, out value))
		{
			return null;
		}
		return value;
	}

	public static bool HasRaceNameBattlegrounds(TAG_RACE tag)
	{
		return s_raceNamesBattlegrounds.ContainsKey(tag);
	}

	public static string GetRaceNameBattlegrounds(TAG_RACE tag)
	{
		string value = null;
		if (!s_raceNamesBattlegrounds.TryGetValue(tag, out value))
		{
			return "UNKNOWN";
		}
		return Get(value);
	}

	public static string GetRaceNameKeyBattlegrounds(TAG_RACE tag)
	{
		string value = null;
		if (!s_raceNamesBattlegrounds.TryGetValue(tag, out value))
		{
			return null;
		}
		return value;
	}

	public static bool HasCardTypeName(TAG_CARDTYPE tag)
	{
		return s_cardTypeNames.ContainsKey(tag);
	}

	public static string GetCardTypeName(TAG_CARDTYPE tag)
	{
		string value = null;
		if (!s_cardTypeNames.TryGetValue(tag, out value))
		{
			return "UNKNOWN";
		}
		return Get(value);
	}

	public static string GetCardTypeNameKey(TAG_CARDTYPE tag)
	{
		string value = null;
		if (!s_cardTypeNames.TryGetValue(tag, out value))
		{
			return null;
		}
		return value;
	}

	public static bool HasCardSetName(TAG_CARD_SET tag)
	{
		return s_cardSetNames.ContainsKey(tag);
	}

	public static string GetCardSetName(TAG_CARD_SET tag)
	{
		string value = null;
		if (!s_cardSetNames.TryGetValue(tag, out value))
		{
			return "UNKNOWN";
		}
		return Get(value);
	}

	public static string GetCardSetNameKey(TAG_CARD_SET tag)
	{
		string value = null;
		if (!s_cardSetNames.TryGetValue(tag, out value))
		{
			return null;
		}
		return value;
	}

	public static bool HasCardSetNameShortened(TAG_CARD_SET tag)
	{
		return s_cardSetNamesShortened.ContainsKey(tag);
	}

	public static string GetCardSetNameShortened(TAG_CARD_SET tag)
	{
		string value = null;
		if (s_cardSetNamesShortened.TryGetValue(tag, out value))
		{
			return Get(value);
		}
		Log.All.PrintWarning("GetCardSetNameShortened - Could not find a Card Set name for tag {0}; returning {1}", tag, "UNKNOWN");
		return "UNKNOWN";
	}

	public static string GetCardSetNameKeyShortened(TAG_CARD_SET tag)
	{
		string value = null;
		if (!s_cardSetNamesShortened.TryGetValue(tag, out value))
		{
			return null;
		}
		return value;
	}

	public static bool HasCardSetNameInitials(TAG_CARD_SET tag)
	{
		return s_cardSetNamesInitials.ContainsKey(tag);
	}

	public static string GetCardSetNameInitials(TAG_CARD_SET tag)
	{
		string value = null;
		if (!s_cardSetNamesInitials.TryGetValue(tag, out value))
		{
			return "UNKNOWN";
		}
		return Get(value);
	}

	public static bool HasMiniSetName(TAG_CARD_SET tag)
	{
		return s_miniSetNames.ContainsKey(tag);
	}

	public static string GetMiniSetName(TAG_CARD_SET tag)
	{
		if (!s_miniSetNames.TryGetValue(tag, out var value))
		{
			return null;
		}
		return Get(value);
	}

	public static bool HasMultiClassGroupName(TAG_MULTI_CLASS_GROUP tag)
	{
		return s_multiClassGroupNames.ContainsKey(tag);
	}

	public static string GetMultiClassGroupName(TAG_MULTI_CLASS_GROUP tag)
	{
		string value = null;
		if (!s_multiClassGroupNames.TryGetValue(tag, out value))
		{
			return "UNKNOWN";
		}
		return Get(value);
	}

	public static bool HasSpellSchoolName(TAG_SPELL_SCHOOL tag)
	{
		return s_spellSchoolNames.ContainsKey(tag);
	}

	public static string GetSpellSchoolName(TAG_SPELL_SCHOOL tag)
	{
		string value = null;
		if (!s_spellSchoolNames.TryGetValue(tag, out value))
		{
			return "UNKNOWN";
		}
		return Get(value);
	}

	public static bool HasFormatName(FormatType format)
	{
		return s_formatNames.ContainsKey(format);
	}

	public static string GetFormatName(FormatType format)
	{
		string value = null;
		if (!s_formatNames.TryGetValue(format, out value))
		{
			return "UNKNOWN";
		}
		return Get(value);
	}

	public static string GetRandomTip(TipCategory tipCategory)
	{
		List<string> listOfTips = GetListOfTips(tipCategory);
		if (listOfTips.Count == 0)
		{
			Debug.LogError($"GameStrings.GetRandomTip() - no tips in category {tipCategory}");
			return "UNKNOWN";
		}
		int index = UnityEngine.Random.Range(0, listOfTips.Count);
		return listOfTips[index];
	}

	public static string GetTip(TipCategory tipCategory, int progress, TipCategory randomTipCategory = TipCategory.DEFAULT)
	{
		List<string> listOfTips = GetListOfTips(tipCategory);
		if (progress < listOfTips.Count)
		{
			return listOfTips[progress];
		}
		return GetRandomTip(randomTipCategory);
	}

	private static List<string> GetListOfTips(TipCategory tipCategory)
	{
		int num = 0;
		List<string> list = new List<string>();
		while (true)
		{
			string text = $"GLUE_TIP_{tipCategory}_{num}";
			string text2 = Get(text);
			if (text2.Equals(text))
			{
				break;
			}
			if (UniversalInputManager.Get().IsTouchMode())
			{
				string text3 = text + "_TOUCH";
				string text4 = Get(text3);
				if (!text4.Equals(text3))
				{
					text2 = text4;
				}
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					string text5 = text + "_PHONE";
					string text6 = Get(text5);
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

	public static string GetMonthFromDigits(int monthDigits)
	{
		Locale locale = Localization.GetLocale();
		if (locale == Locale.thTH)
		{
			return monthDigits switch
			{
				1 => "มกราคม", 
				2 => "ก\u0e38มภาพ\u0e31นธ\u0e4c", 
				3 => "ม\u0e35นาคม", 
				4 => "เมษายน", 
				5 => "พฤษภาคม", 
				6 => "ม\u0e34ถ\u0e38นายน", 
				7 => "กรกฎาคม", 
				8 => "ส\u0e34งหาคม", 
				9 => "ก\u0e31นยายน", 
				10 => "ต\u0e38ลาคม", 
				11 => "พฤศจ\u0e34กายน", 
				12 => "ธ\u0e31นวาคม", 
				_ => string.Empty, 
			};
		}
		return Localization.GetCultureInfo().DateTimeFormat.GetMonthName(monthDigits);
	}

	public static string GetOrdinalNumber(int number)
	{
		string text = "ORDINAL_" + number;
		string text2 = Get(text);
		if (text2 == text)
		{
			Debug.LogError($"GameStrings.GetOrdinalNumber() - Unable to find ordinal string for number={number}");
			return number.ToString();
		}
		return text2;
	}

	private static bool LoadCategory(Global.GameStringCategory cat, bool native)
	{
		if (s_tables.ContainsKey(cat))
		{
			Debug.LogWarning($"GameStrings.LoadCategory() - {cat} is already loaded");
			return false;
		}
		GameStringTable gameStringTable = new GameStringTable();
		if (!gameStringTable.Load(cat, native))
		{
			Debug.LogError($"GameStrings.LoadCategory() - {cat} failed to load");
			return false;
		}
		if (HearthstoneApplication.IsInternal())
		{
			CheckConflicts(gameStringTable);
		}
		s_tables.Add(cat, gameStringTable);
		return true;
	}

	private static bool UnloadCategory(Global.GameStringCategory cat)
	{
		if (!s_tables.Remove(cat))
		{
			Debug.LogWarning($"GameStrings.UnloadCategory() - {cat} was never loaded");
			return false;
		}
		return true;
	}

	private static void CheckConflicts(GameStringTable table)
	{
		Map<string, string>.KeyCollection keys = table.GetAll().Keys;
		Global.GameStringCategory category = table.GetCategory();
		foreach (GameStringTable value in s_tables.Values)
		{
			foreach (string item in keys)
			{
				if (value.Get(item) != null)
				{
					string message = $"GameStrings.CheckConflicts() - Tag {item} is used in {category} and {value.GetCategory()}. All tags must be unique.";
					Error.AddDevWarningNonRepeating("GameStrings Error", message);
				}
			}
		}
	}

	private static string Find(string key)
	{
		if (key == null)
		{
			return null;
		}
		foreach (GameStringTable value in s_tables.Values)
		{
			string text = value.Get(key);
			if (text != null)
			{
				return text;
			}
		}
		if (key.StartsWith("Assets/"))
		{
			Debug.LogErrorFormat("Asset path being used as GameString key={0}", key);
		}
		return null;
	}

	private static string[] ParseLanguageRuleArgs(string str, int ruleIndex, out int argStartIndex, out int argEndIndex)
	{
		argStartIndex = -1;
		argEndIndex = -1;
		argStartIndex = str.IndexOf('(', ruleIndex + 2);
		if (argStartIndex < 0)
		{
			Debug.LogWarning($"GameStrings.ParseLanguageRuleArgs() - failed to parse '(' for rule at index {ruleIndex} in string {str}");
			return null;
		}
		argEndIndex = str.IndexOf(')', argStartIndex + 1);
		if (argEndIndex < 0)
		{
			Debug.LogWarning($"GameStrings.ParseLanguageRuleArgs() - failed to parse ')' for rule at index {ruleIndex} in string {str}");
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
			foreach (Match item in matchCollection)
			{
				stringBuilder.Append(text, num, item.Index - num);
				stringBuilder.Append('0', item.Length);
				num = item.Index + item.Length;
			}
			stringBuilder.Append(text, num, text.Length - num);
			text = stringBuilder.ToString();
		}
		string[] array = text.Split(LANGUAGE_RULE_ARG_DELIMITERS);
		int num2 = 0;
		for (int i = 0; i < array.Length; i++)
		{
			string text2 = array[i];
			if (matchCollection.Count > 0)
			{
				stringBuilder.Remove(0, stringBuilder.Length);
				int num3 = 0;
				foreach (Match item2 in matchCollection)
				{
					if (item2.Index >= num2 && item2.Index < num2 + text2.Length)
					{
						int num4 = item2.Index - num2;
						stringBuilder.Append(text2, num3, num4 - num3);
						stringBuilder.Append(item2.Value);
						num3 = num4 + item2.Length;
					}
				}
				stringBuilder.Append(text2, num3, text2.Length - num3);
				text2 = stringBuilder.ToString();
				num2 += text2.Length + 1;
			}
			text2 = (array[i] = text2.Trim());
		}
		return array;
	}

	private static string ParseLanguageRule1(string str)
	{
		int num = str.IndexOf("|1");
		if (num < 0)
		{
			return str;
		}
		StringBuilder stringBuilder;
		string text;
		int num3;
		string[] array;
		int num4;
		for (stringBuilder = new StringBuilder(); num >= 0; stringBuilder.Append(text), stringBuilder.Append(array[num4]), str = str.Substring(num3 + 1), num = str.IndexOf("|1"))
		{
			text = str.Substring(0, num);
			if (text.Length == 0)
			{
				Debug.LogWarningFormat("GameStrings.ParseLanguageRule1() - invalid preStr, str:{0}, ruleIndex:{1}", str, num);
				break;
			}
			int num2 = str.IndexOf('(', num);
			if (num2 < 0)
			{
				Debug.LogWarningFormat("GameStrings.ParseLanguageRule1() - invalid openIndex, str:{0}, ruleIndex:{1}", str, num);
				break;
			}
			num3 = str.IndexOf(')', num2);
			if (num3 < 0)
			{
				Debug.LogWarningFormat("GameStrings.ParseLanguageRule1() - invalid closeIndex, str:{0}, ruleIndex:{1}, openIndex:{2}", str, num);
				break;
			}
			string text2 = str.Substring(num2 + 1, num3 - num2 - 1);
			array = text2.Split(',');
			if (array.Length != 2)
			{
				Debug.LogWarningFormat("GameStrings.ParseLanguageRule1() - invalid args, str:{0}, argStr:{1}", str, text2);
				break;
			}
			char c = text[text.Length - 1];
			num4 = 0;
			switch (c)
			{
			case '0':
			case '1':
			case '3':
			case '6':
			case '7':
			case '8':
				num4 = 0;
				continue;
			case '2':
			case '4':
			case '5':
			case '9':
				num4 = 1;
				continue;
			default:
				if (c >= '가' && c <= '힣')
				{
					num4 = (((c - 44032) % 28 == 0) ? 1 : 0);
					continue;
				}
				break;
			}
			Debug.LogWarningFormat("GameStrings.ParseLanguageRule1() - invalid precedingChar, str:{0}, precedingChar:{1}", str, c);
			break;
		}
		stringBuilder.Append(str);
		return stringBuilder.ToString();
	}

	private static string ParseLanguageRule4(string str, PluralNumber[] pluralNumbers = null)
	{
		StringBuilder stringBuilder = null;
		int? num = null;
		int num2 = 0;
		int num3 = 0;
		for (int num4 = str.IndexOf("|4"); num4 >= 0; num4 = str.IndexOf("|4", num4 + 2))
		{
			num3++;
			int argStartIndex;
			int argEndIndex;
			string[] array = ParseLanguageRuleArgs(str, num4, out argStartIndex, out argEndIndex);
			if (array == null)
			{
				continue;
			}
			int num5 = num2;
			int num6 = num4 - num2;
			string text = str.Substring(num5, num6);
			PluralNumber pluralNumber = null;
			if (pluralNumbers != null)
			{
				int pluralArgIndex = num3 - 1;
				pluralNumber = Array.Find(pluralNumbers, (PluralNumber currPluralNumber) => currPluralNumber.m_index == pluralArgIndex);
			}
			int number;
			if (pluralNumber != null)
			{
				num = pluralNumber.m_number;
			}
			else if (ParseLanguageRule4Number(array, text, out number))
			{
				num = number;
			}
			else if (!num.HasValue)
			{
				Debug.LogWarning($"GameStrings.ParseLanguageRule4() - failed to parse a number in substring \"{text}\" (indexes {num5}-{num6}) for rule {num3} in string \"{str}\"");
				continue;
			}
			int pluralIndex = GetPluralIndex(num.Value);
			if (pluralIndex >= array.Length)
			{
				Debug.LogWarning($"GameStrings.ParseLanguageRule4() - not enough arguments for rule {num3} in string \"{str}\"");
			}
			else
			{
				string value = array[pluralIndex];
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder();
				}
				stringBuilder.Append(text);
				stringBuilder.Append(value);
				num2 = argEndIndex + 1;
			}
			if (pluralNumber != null && pluralNumber.m_useForOnlyThisIndex)
			{
				num = null;
			}
		}
		if (stringBuilder == null)
		{
			return str;
		}
		stringBuilder.Append(str, num2, str.Length - num2);
		return stringBuilder.ToString();
	}

	private static bool ParseLanguageRule4Number(string[] args, string betweenRulesStr, out int number)
	{
		if (ParseLanguageRule4Number_Foreward(args[0], out number))
		{
			return true;
		}
		if (ParseLanguageRule4Number_Backward(betweenRulesStr, out number))
		{
			return true;
		}
		number = 0;
		return false;
	}

	private static bool ParseLanguageRule4Number_Foreward(string str, out int number)
	{
		number = 0;
		Match match = Regex.Match(str, "(?<!\\/)(?:[0-9]+,)*[0-9]+(?!\\/)");
		if (!match.Success)
		{
			match = Regex.Match(str, "(?<!\\/)(?:[0-9]+,)*[0-9]+");
		}
		if (!match.Success)
		{
			return false;
		}
		if (!GeneralUtils.TryParseInt(match.Value, out number))
		{
			return false;
		}
		return true;
	}

	private static bool ParseLanguageRule4Number_Backward(string str, out int number)
	{
		number = 0;
		MatchCollection matchCollection = Regex.Matches(str, "(?<!\\/)(?:[0-9]+,)*[0-9]+(?!\\/)");
		if (matchCollection.Count == 0)
		{
			matchCollection = Regex.Matches(str, "(?<!\\/)(?:[0-9]+,)*[0-9]+");
		}
		if (matchCollection.Count == 0)
		{
			return false;
		}
		if (!GeneralUtils.TryParseInt(matchCollection[matchCollection.Count - 1].Value, out number))
		{
			return false;
		}
		return true;
	}

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
			if ((uint)(num - 11) <= 3u)
			{
				return 2;
			}
			switch (number % 10)
			{
			case 1:
				return 0;
			case 2:
			case 3:
			case 4:
				return 1;
			default:
				return 2;
			}
		}
		case Locale.plPL:
			switch (number)
			{
			case 1:
				return 0;
			case 0:
				return 2;
			default:
			{
				int num = number % 100;
				if ((uint)(num - 11) <= 3u)
				{
					return 2;
				}
				num = number % 10;
				if ((uint)(num - 2) <= 2u)
				{
					return 1;
				}
				return 2;
			}
			}
		default:
			if (number == 1)
			{
				return 0;
			}
			return 1;
		}
	}
}
