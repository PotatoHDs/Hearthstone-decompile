using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using Assets;
using bgs;
using Blizzard.BlizzardErrorMobile;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using bnet.protocol;
using Hearthstone;
using Hearthstone.Attribution;
using Hearthstone.Core;
using Hearthstone.CRM;
using Hearthstone.DataModels;
using Hearthstone.Http;
using Hearthstone.InGameMessage;
using Hearthstone.InGameMessage.UI;
using Hearthstone.Login;
using Hearthstone.Progression;
using Hearthstone.Streaming;
using Hearthstone.UI;
using MiniJSON;
using PegasusGame;
using PegasusShared;
using PegasusUtil;
using SpectatorProto;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : IService
{
	private enum QuickLaunchAvailability
	{
		OK,
		FINDING_GAME,
		ACTIVE_GAME,
		SCENE_TRANSITION,
		COLLECTION_NOT_READY
	}

	private enum FriendListType
	{
		FRIEND,
		NEARBY,
		FSG
	}

	private class QuickLaunchState
	{
		public bool m_launching;

		public bool m_skipMulligan;

		public bool m_flipHeroes;

		public bool m_mirrorHeroes;

		public string m_opponentHeroCardId;
	}

	private struct NamedParam
	{
		public string Text { get; private set; }

		public int Number { get; private set; }

		public bool HasNumber => Number > 0;

		public NamedParam(string param)
		{
			Text = param;
			Number = 0;
			if (GeneralUtils.TryParseInt(param, out var val))
			{
				Number = val;
			}
		}
	}

	public delegate void LogFormatFunc(string format, params object[] args);

	private enum SetAdventureProgressMode
	{
		Victory,
		Defeat,
		Progress
	}

	private const int MAX_PLAYER_TAG_ATTRIBUTES = 20;

	private const int MAX_PLAYER_TAG_INT_VALUE = 999999;

	public const char CONFIG_CHEAT_PLAYER_TAG_DELIMITER = ',';

	public const char CONFIG_CHEAT_PLAYER_TAG_VALUE_DELIMITER = '=';

	public const string CONFIG_INSTANT_GAMEPLAY_KEY = "Cheats.InstantGameplay";

	public const string CONFIG_INSTANT_CHEAT_COMMANDS_KEY = "Cheats.InstantCheatCommands";

	public const char CONFIG_INSTANT_CHEAT_COMMANDS_DELIMITER = ',';

	public readonly Vector3 SPEECH_BUBBLE_HIDDEN_POSITION = new Vector3(15000f, 0f, 0f);

	private static Cheats s_instance;

	private bool m_isInGameplayScene;

	private int m_boardId;

	private string m_playerTags;

	private bool m_speechBubblesEnabled = true;

	private Map<Global.SoundCategory, bool> m_audioChannelEnabled = InitAudioChannelMap();

	private Queue<int> m_pvpdrTreasureIds = new Queue<int>();

	private Queue<int> m_pvpdrLootIds = new Queue<int>();

	private Map<string, List<Global.SoundCategory>> m_audioChannelGroups = new Map<string, List<Global.SoundCategory>>
	{
		{
			"VO",
			new List<Global.SoundCategory>
			{
				Global.SoundCategory.VO,
				Global.SoundCategory.SPECIAL_VO,
				Global.SoundCategory.BOSS_VO,
				Global.SoundCategory.TRIGGER_VO
			}
		},
		{
			"MUSIC",
			new List<Global.SoundCategory>
			{
				Global.SoundCategory.MUSIC,
				Global.SoundCategory.SPECIAL_MUSIC,
				Global.SoundCategory.HERO_MUSIC
			}
		},
		{
			"FX",
			new List<Global.SoundCategory>
			{
				Global.SoundCategory.FX,
				Global.SoundCategory.NONE,
				Global.SoundCategory.SPECIAL_CARD
			}
		},
		{
			"BACKGROUND",
			new List<Global.SoundCategory>
			{
				Global.SoundCategory.AMBIENCE,
				Global.SoundCategory.RESET_GAME
			}
		}
	};

	private bool m_loadingStoreChallengePrompt;

	private StoreChallengePrompt m_storeChallengePrompt;

	private bool m_isNewCardInPackOpeningEnabled;

	private AlertPopup m_alert;

	private static readonly Map<KeyCode, ScenarioDbId> s_quickPlayKeyMap = new Map<KeyCode, ScenarioDbId>
	{
		{
			KeyCode.F1,
			ScenarioDbId.PRACTICE_EXPERT_MAGE
		},
		{
			KeyCode.F2,
			ScenarioDbId.PRACTICE_EXPERT_HUNTER
		},
		{
			KeyCode.F3,
			ScenarioDbId.PRACTICE_EXPERT_WARRIOR
		},
		{
			KeyCode.F4,
			ScenarioDbId.PRACTICE_EXPERT_SHAMAN
		},
		{
			KeyCode.F5,
			ScenarioDbId.PRACTICE_EXPERT_DRUID
		},
		{
			KeyCode.F6,
			ScenarioDbId.PRACTICE_EXPERT_PRIEST
		},
		{
			KeyCode.F7,
			ScenarioDbId.PRACTICE_EXPERT_ROGUE
		},
		{
			KeyCode.F8,
			ScenarioDbId.PRACTICE_EXPERT_PALADIN
		},
		{
			KeyCode.F9,
			ScenarioDbId.PRACTICE_EXPERT_WARLOCK
		},
		{
			KeyCode.F10,
			ScenarioDbId.PRACTICE_EXPERT_DEMONHUNTER
		},
		{
			KeyCode.T,
			ScenarioDbId.TEST_BLANK_STATE
		}
	};

	private static readonly Map<KeyCode, string> s_opponentHeroKeyMap = new Map<KeyCode, string>
	{
		{
			KeyCode.F1,
			"HERO_08"
		},
		{
			KeyCode.F2,
			"HERO_05"
		},
		{
			KeyCode.F3,
			"HERO_01"
		},
		{
			KeyCode.F4,
			"HERO_02"
		},
		{
			KeyCode.F5,
			"HERO_06"
		},
		{
			KeyCode.F6,
			"HERO_09"
		},
		{
			KeyCode.F7,
			"HERO_03"
		},
		{
			KeyCode.F8,
			"HERO_04"
		},
		{
			KeyCode.F9,
			"HERO_07"
		},
		{
			KeyCode.F10,
			"HERO_10"
		},
		{
			KeyCode.T,
			"HERO_01"
		}
	};

	private const string IPSUM_PARAGRAPH = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ut rhoncus ante. Donec in pretium felis. Duis mollis purus a ante mollis luctus. Nulla hendrerit gravida nulla non convallis. Vivamus vel ligula a mi porta porta et at magna. Nulla euismod diam eget arcu pharetra scelerisque. In id sem a ipsum maximus cursus. In pulvinar fermentum dolor, at ultrices ipsum congue nec.";

	private const string IPSUM_TITLE = "Lorem Ipsum";

	private QuickLaunchState m_quickLaunchState = new QuickLaunchState();

	private bool m_skipSendingGetGameState;

	public static float VOChanceOverride = -1f;

	private float m_waitTime = 10f;

	private bool m_showedMessage;

	private List<WidgetInstance> s_createdWidgets = new List<WidgetInstance>();

	private static bool s_hasSubscribedToPartyEvents = false;

	private string[] m_lastUtilServerCmd;

	private IGameDownloadManager DownloadManager => GameDownloadManagerProvider.Get();

	public static bool ShowFakeBreakingNews => Vars.Key("Cheats.ShowFakeBreakingNews").GetBool(def: false);

	public static bool ShowFakeNerfedCards => Vars.Key("Cheats.ShowFakeNerfedCards").GetBool(def: false);

	public static bool ShowFakeAddedCards => Vars.Key("Cheats.ShowFakeAddedCards").GetBool(def: false);

	public static bool SimulateWebPaneLogin
	{
		get
		{
			if (HearthstoneApplication.IsPublic())
			{
				return false;
			}
			return Vars.Key("Cheats.SimulateWebPaneLogin").GetBool(def: false);
		}
	}

	public static event Action<string[]> PlayAudioByName;

	private static Map<Global.SoundCategory, bool> InitAudioChannelMap()
	{
		Map<Global.SoundCategory, bool> map = new Map<Global.SoundCategory, bool>();
		foreach (int value in Enum.GetValues(typeof(Global.SoundCategory)))
		{
			map.Add((Global.SoundCategory)value, value: true);
		}
		return map;
	}

	public static Cheats Get()
	{
		if (s_instance == null)
		{
			s_instance = HearthstoneServices.Get<Cheats>();
		}
		return s_instance;
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		CheatMgr cheatMgr = serviceLocator.Get<CheatMgr>();
		if (HearthstoneApplication.IsInternal())
		{
			cheatMgr.RegisterCategory("help");
			cheatMgr.RegisterCheatHandler("help", OnProcessCheat_help, "Get help for a specific command or list of commands", "<command name>", "");
			cheatMgr.RegisterCheatHandler("example", OnProcessCheat_example, "Run an example of this command if one exists", "<command name>");
			cheatMgr.RegisterCheatHandler("error", OnProcessCheat_error, "Make the client throw an arbitrary error.", "<warning | fatal | exception> <optional error message>", "warning This is an example warning message.");
			cheatMgr.RegisterCategory("bug");
			cheatMgr.RegisterCheatHandler("bug", On_ProcessCheat_bug);
			cheatMgr.RegisterCheatHandler("Bug", On_ProcessCheat_bug);
			cheatMgr.RegisterCheatHandler("crash", On_ProcessCheat_crash);
			cheatMgr.RegisterCheatHandler("anr", On_ProcessCheat_ANR);
			cheatMgr.RegisterCategory("general");
			cheatMgr.RegisterCheatHandler("cheat", OnProcessCheat_cheat, "Send a cheat command to the server", "<command> <arguments>");
			cheatMgr.RegisterCheatAlias("cheat", "c");
			cheatMgr.RegisterCheatHandler("timescale", OnProcessCheat_timescale, "Cheat to change the timescale", "<timescale>", "0.5");
			cheatMgr.RegisterCheatHandler("util", OnProcessCheat_utilservercmd, "Run a cheat on the UTIL server you're connected to.", "[subcommand] [subcommand args]", "help");
			cheatMgr.RegisterCheatHandler("game", OnProcessCheat_gameservercmd, "[NYI] Run a cheat on the GAME server you're connected to.", "[subcommand] [subcommand args]", "help");
			Network.Get().RegisterNetHandler(DebugCommandResponse.PacketID.ID, OnProcessCheat_utilservercmd_OnResponse);
			cheatMgr.RegisterCheatHandler("event", OnProcessCheat_EventTiming, "View event timings to see if they're active.", "[event=event_name]", "");
			cheatMgr.RegisterCheatHandler("audiochannel", OnProcessCheat_audioChannel, "Turn on/off an audio channel.", "[audio channel name] [on/off]", "fx off");
			cheatMgr.RegisterCheatHandler("audiochannelgroup", OnProcessCheat_audioChannelGroup, "Turn on/off a group of audio channels.", "[audio channel group name] [on/off]", "vo off");
			cheatMgr.RegisterCategory("igm");
			cheatMgr.RegisterCheatHandler("igm", OnProcessCheat_igm, "Register the content type and show it by using the debug UI", "<content_type>");
			cheatMgr.RegisterCheatHandler("msgui", OnProcessCheat_msgui, "Message popup ui", "<register|show> [<text|shop>]");
			cheatMgr.RegisterCategory("program");
			cheatMgr.RegisterCheatHandler("reset", OnProcessCheat_reset, "Reset the client");
			cheatMgr.RegisterCategory("gameplay");
			cheatMgr.RegisterCheatHandler("board", OnProcessCheat_board, "Set which board will be loaded on the next game", "<BRM|STW|GVG>", "BRM");
			cheatMgr.RegisterCheatHandler("playertags", OnProcessCheat_playerTags, "Set these tags on your player in the next game (limit 20)", "<TagId1=TagValue1,TagId2=TagValue2,...,TagIdN=TagValueN>", "427=10,419=1");
			cheatMgr.RegisterCheatHandler("togglespeechbubbles", OnProcessCheat_speechBubbles, "Toggle on/off speech bubbles.", "", "");
			cheatMgr.RegisterCheatHandler("disconnect", OnProcessCheat_disconnect, "Disconnects you from a game in progress (disconnects from game server only). If you want to disconnect from just battle.net, use 'disconnect bnet'.");
			cheatMgr.RegisterCheatHandler("restart", OnProcessCheat_restart, "Restarts any non-PvP game.");
			cheatMgr.RegisterCheatHandler("autohand", OnProcessCheat_autohand, "Set whether PhoneUI automatically hides your hand after playing a card", "<true/false>", "true");
			cheatMgr.RegisterCheatHandler("endturn", OnProcessCheat_endturn, "End your turn");
			cheatMgr.RegisterCheatHandler("scenario", OnProcessCheat_scenario, "Launch a scenario.", "<scenario_id> [<game_type_id>] [<deck_name>|<deck_id>] [<game_format>]");
			cheatMgr.RegisterCheatAlias("scenario", "mission");
			cheatMgr.RegisterCheatHandler("aigame", OnProcessCheat_aigame, "Launch a game vs an AI using specified deck code.", "<deck_code_string> [<game_format>]");
			cheatMgr.RegisterCheatHandler("loadsnapshot", OnProcessCheat_loadSnapshot, "Load a snapshot file from local disk.", "<replayfilename>");
			cheatMgr.RegisterCheatHandler("skipgetgamestate", OnProcessCheat_SkipSendingGetGameState, "Skip sending GetGameState packet in Gameplay.Start().");
			cheatMgr.RegisterCheatHandler("sendgetgamestate", OnProcessCheat_SendGetGameState, "Send GetGameState packet.");
			cheatMgr.RegisterCheatHandler("auto_exportgamestate", OnProcessCheat_autoexportgamestate, "Save JSON file serializing some of GameState");
			cheatMgr.RegisterCheatHandler("opponentname", OnProcessCheat_OpponentName, "Set the Opponent name", "", "The Innkeeper");
			cheatMgr.RegisterCheatHandler("history", OnProcessCheat_History, "disable/enable history", "", "true");
			cheatMgr.RegisterCheatHandler("settag", OnProcessCheat_settag, "Sets a tag on an entity to a value", "settag <tag_id> <entity_id> <tag_value>");
			cheatMgr.RegisterCheatHandler("thinkemotes", OnProcessCheat_playAllThinkEmotes, "Plays all of the think lines for the specified player's hero");
			cheatMgr.RegisterCheatHandler("playemote", OnProcessCheat_playEmote, "Play the emote for the specified player's hero", "playemote <emote_type> <player>");
			cheatMgr.RegisterCheatHandler("heropowervo", OnProcessCheat_playAllMissionHeroPowerLines, "Plays all the hero power lines associated with this mission");
			cheatMgr.RegisterCheatHandler("idlevo", OnProcessCheat_playAllMissionIdleLines, "Plays all idle lines associated with this mission");
			cheatMgr.RegisterCheatHandler("debugscript", OnProcessCheat_debugscript, "Toggles script debugging for a specific power", "debugscript <power_guid>");
			cheatMgr.RegisterCheatHandler("scriptdebug", OnProcessCheat_debugscript, "Toggles script debugging for a specific power", "scriptdebug <power_guid>");
			cheatMgr.RegisterCheatHandler("disablescriptdebug", OnProcessCheat_disablescriptdebug, "Disables all script debugging on the server", "disablescriptdebug");
			cheatMgr.RegisterCheatHandler("disabledebugscript", OnProcessCheat_disablescriptdebug, "Disables all script debugging on the server", "disabledebugscript");
			cheatMgr.RegisterCheatHandler("printpersistentlist", OnProcessCheat_printpersistentlist, "Prints all persistent lists for a particular entity. Call it with no entity to print ALL persistent lists on ALL entities", "printpersistentlist [entity_id]");
			cheatMgr.RegisterCheatHandler("printpersistentlists", OnProcessCheat_printpersistentlist, "Prints all persistent lists for a particular entity. Call it with no entity to print ALL persistent lists on ALL entities", "printpersistentlists [entity_id]");
			cheatMgr.RegisterCategory("collection");
			cheatMgr.RegisterCheatHandler("collectionfirstxp", OnProcessCheat_collectionfirstxp, "Set the number of page and cover flips to zero", "", "");
			cheatMgr.RegisterCheatHandler("resettips", OnProcessCheat_resettips, "Resets Innkeeper tips for collection manager", "", "");
			cheatMgr.RegisterCheatHandler("cardchangepopup", OnProcessCheat_cardchangepopup, "Show a Card Change popup.", "[showAddition] [useFakeData] [numFakeCards]", "false true 3");
			cheatMgr.RegisterCheatHandler("cardchangereset", OnProcessCheat_cardchangereset, "Reset the record of which changed cards have already been seen.");
			cheatMgr.RegisterCheatHandler("loginpopupsequence", OnProcessCheat_loginpopupsequence, "Show any active login popup sequences.");
			cheatMgr.RegisterCheatHandler("loginpopupreset", OnProcessCheat_loginpopupreset, "Reset game save data for login popup sequences.");
			cheatMgr.RegisterCheatHandler("onlygold", OnProcessCheat_onlygold, "In collection manager, do you want to see gold, nogold, or both?", "<command name>", "");
			cheatMgr.RegisterCheatHandler("exportcards", OnProcessCheat_exportcards, "Export images of cards");
			cheatMgr.RegisterCategory("cosmetics");
			cheatMgr.RegisterCheatHandler("defaultcardback", OnProcessCheat_favoritecardback, "Set your favorite cardback as if through the collection manager", "<cardback id>");
			cheatMgr.RegisterCheatHandler("favoritecardback", OnProcessCheat_favoritecardback, "Set your favorite cardback as if through the collection manager", "<cardback id>");
			cheatMgr.RegisterCheatHandler("favoritehero", OnProcessCheat_favoritehero, "Change your favorite hero for a class (only works from CollectionManager)", "<class_id> <hero_card_id> <hero_premium>");
			cheatMgr.RegisterCheatHandler("exportcardbacks", OnProcessCheat_exportcardbacks, "Export images of card backs");
			cheatMgr.RegisterCategory("legacy quests and rewards");
			cheatMgr.RegisterCheatHandler("questcompletepopup", OnProcessCheat_questcompletepopup, "Shows the quest complete achievement screen", "<quest_id>", "58");
			cheatMgr.RegisterCheatHandler("questprogresspopup", OnProcessCheat_questprogresspopup, "Pop up a quest progress toast", "<title> <description> <progress> <maxprogress>", "Hello World 3 10");
			cheatMgr.RegisterCheatHandler("questwelcome", OnProcessCheat_questwelcome, "Open list of daily quests", "<fromLogin>", "true");
			cheatMgr.RegisterCheatHandler("newquestvisual", OnProcessCheat_newquestvisual, "Shows a new quest tile, only usable while a quest popup is active");
			cheatMgr.RegisterCheatHandler("fixedrewardcomplete", OnProcessCheat_fixedrewardcomplete, "Shows the visual for a fixed reward", "<fixed_reward_map_id>");
			cheatMgr.RegisterCheatHandler("rewardboxes", OnProcessCheat_rewardboxes, "Open the reward box screen with example rewards", "<card|cardback|gold|dust|random> <num_boxes>", "");
			cheatMgr.RegisterCategory("shop");
			cheatMgr.RegisterCheatHandler("storepassword", OnProcessCheat_storepassword, "Show store challenge popup", "", "");
			cheatMgr.RegisterCheatHandler("testproduct", OnProcessCheat_testproduct, "Fill Shop with a product", "<pmt_product_id>");
			cheatMgr.RegisterCheatHandler("testadventurestore", OnProcessCheat_testadventurestore, "Open adventure store for a wing", "<wing_id> <is_full_adventure>");
			cheatMgr.RegisterCheatHandler("refreshcurrency", OnProcessCheat_refreshcurrency, "Refresh currency balance", "<runestones|arcane_orbs>");
			cheatMgr.RegisterCheatHandler("loadpersonalizedshop", OnProcessCheat_loadpersonalizedshop, "Load personalized shop", "<page_id>");
			cheatMgr.RegisterCategory("iks");
			cheatMgr.RegisterCheatHandler("iks", OnProcessCheat_iks, "Open InnKeepersSpecial with a custom url", "<url>");
			cheatMgr.RegisterCheatHandler("iksaction", OnProcessCheat_iksgameaction, "Execute a game action as if IKS was clicked.");
			cheatMgr.RegisterCheatHandler("iksseen", OnProcessCheat_iksseen, "Determine if an IKS message should be seen by its game action.");
			cheatMgr.RegisterCategory("rank");
			cheatMgr.RegisterCheatHandler("seasondialog", OnProcessCheat_seasondialog, "Open the season end dialog", "<rank> [standard|wild|classic]", "bronze5 wild");
			cheatMgr.RegisterCheatHandler("rankrefresh", OnProcessCheat_rankrefresh, "Request medalinfo from server and show rankchange twoscoop after receiving it");
			cheatMgr.RegisterCheatHandler("rankchange", OnProcessCheat_rankchange, "Show a fake rankchange twoscoop", "[rank] [up|down|win|loss] [wild] [winstreak] [chest]", "bronze5 up chest");
			cheatMgr.RegisterCheatHandler("rankreward", OnProcessCheat_rankreward, "Show a fake RankedRewardDisplay for rank (or all ranks up to a rank)", "<rank> [standard|wild|classic|all]", "bronze5 all");
			cheatMgr.RegisterCheatHandler("rankcardback", OnProcessCheat_rankcardback, "Show a fake RankedCardBackProgressDisplay", "<wins> [season_id]", "5 75");
			cheatMgr.RegisterCheatHandler("easyrank", OnProcessCheat_easyrank, "Easier cheat command to set your rank on the util server", "<rank>", "16");
			cheatMgr.RegisterCheatHandler("resetrotationtutorial", OnProcessCheat_resetrotationtutorial, "Cause the user to see the Set Rotation Tutorial again.", "<newbie|veteran>", "newbie|veteran");
			cheatMgr.RegisterCheatHandler("setrotationevent", OnProcessCheat_setrotationevent, $"Trigger the {SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021} event, causing some card sets to be rotated.", "<bypass intro> <seconds until event>", "true 3");
			cheatMgr.RegisterCheatHandler("rankdebug", OnProcessCheat_rankdebug, "Display debug information regarding rank / rating", "[show|off] <standard/wild>", "show standard");
			cheatMgr.RegisterCheatHandler("resetrankedintro", OnProcessCheat_resetrankedintro, "Reset game save data values for various tutorial elements for ranked play.");
			cheatMgr.RegisterCategory("sound/vo");
			cheatMgr.RegisterCheatHandler("playnullsound", OnProcessCheat_playnullsound, "Tell SoundManager to play a null sound.");
			cheatMgr.RegisterCheatHandler("playaudio", OnProcessCheat_playaudio, "Play an audio file by name");
			cheatMgr.RegisterCheatHandler("quote", OnProcessCheat_quote, "", "<character> <line> [sound]", "Innkeeper VO_INNKEEPER_FORGE_COMPLETE_22 VO_INNKEEPER_ARENA_COMPLETE");
			cheatMgr.RegisterCheatHandler("narrative", OnProcessCheat_narrative, "Show a narrative popup from an achievement");
			cheatMgr.RegisterCheatHandler("narrativedialog", OnProcessCheat_narrativedialog, "Show a narrative dialog sequence popup");
			cheatMgr.RegisterCategory("game modes");
			cheatMgr.RegisterCheatHandler("arena", OnProcessCheat_arena, "Runs various arena cheats.", "[subcommand] [subcommand args]", "help");
			cheatMgr.RegisterCheatHandler("retire", OnProcessCheat_retire, "Retires your draft deck", "", "");
			cheatMgr.RegisterCheatHandler("battlegrounds", OnProcessCheat_battlegrounds, "Queue for a game of Battlegrounds.");
			cheatMgr.RegisterCheatHandler("tb", OnProcessCheat_tavernbrawl, "Run a variety of Tavern Brawl related commands", "[subcommand] [subcommand args]", "view");
			cheatMgr.RegisterCheatHandler("resetTavernBrawlAdventure", OnProcessCheat_ResetTavernBrawlAdventure, "Reset the current Tavern Brawl Adventure progress");
			cheatMgr.RegisterCheatHandler("returningplayer", OnProcessCheat_returningplayer, "Set the Returning Player progress", "<0|1|2|3>", "1");
			cheatMgr.RegisterCheatHandler("duels", OnProcessCheat_duels, "Run a variety of Duels related commands", "[subcommand] [subcommand args]", "help");
			cheatMgr.RegisterCategory("ui");
			cheatMgr.RegisterCheatHandler("demotext", OnProcessCheat_demotext, "", "<line>", "HelloWorld!");
			cheatMgr.RegisterCheatHandler("popuptext", OnProcessCheat_popuptext, "show a popup notification", "<line>", "HelloWorld!");
			cheatMgr.RegisterCheatHandler("alerttext", OnProcessCheat_alerttext, "show a popup alert", "<line>", "HelloWorld!");
			cheatMgr.RegisterCheatHandler("logtext", OnProcessCheat_logtext, "log a line of text", "<level> <line>", "warning WatchOutWorld!");
			cheatMgr.RegisterCheatHandler("logenable", OnProcessCheat_logenable, "temporarily enables a logger", "<logger> <subtype> <enabled>", "Store file/screen/console true");
			cheatMgr.RegisterCheatHandler("loglevel", OnProcessCheat_loglevel, "temporarily sets the min level of a logger", "<logger> <level>", "Store debug");
			cheatMgr.RegisterCheatHandler("reloadgamestrings", OnProcessCheat_reloadgamestrings, "Reload all game strings from GLUE/GLOBAL/etc.");
			cheatMgr.RegisterCheatHandler("attn", OnProcessCheat_userattentionmanager, "Prints out what UserAttentionBlockers, if any, are currently active.");
			cheatMgr.RegisterCheatHandler("banner", OnProcessCheat_banner, "Shows the specified wooden banner (supply a banner_id). If none is supplied, it'll show the latest known banner. Use 'banner list' to view all known banners.", "<banner_id> | list", "33");
			cheatMgr.RegisterCheatHandler("browser", OnProcessCheat_browser, "Run In-Game Browser related commands", "[subcommand]", "show");
			cheatMgr.RegisterCheatHandler("notice", OnProcessCheat_notice, "Show a notice", "<gold|arcane_orbs|dust|booster|card|cardback|tavern_brawl_rewards|event|license> [data]");
			cheatMgr.RegisterCheatHandler("load_widget", OnProcessCheat_LoadWidget, "Show a widget given a specific guid. If `CHEATED_STATE` exists on a visual controller in the widget, it will be triggered and should be used to help get the widget into the proper location on the screen or any other special test only setup that is needed.");
			cheatMgr.RegisterCheatHandler("clear_widgets", OnProcessCheat_ClearWidgets, "Remove any widgets that were created via the load_widget cheat");
			cheatMgr.RegisterCheatHandler("serverlog", OnProcessCheat_ServerLog, "Log a ServerScript message");
			cheatMgr.RegisterCheatHandler("dialogevent", OnProcessCheat_dialogEvent, "Choose a category of dialog event, and force it to be run again.", "<event_type> or \"reset\"");
			cheatMgr.RegisterCategory("social");
			cheatMgr.RegisterCheatHandler("spectate", OnProcessCheat_spectate, "Connects to a game server to spectate", "<ip_address> <port> <game_handle> <spectator_password> [gameType] [missionId]");
			cheatMgr.RegisterCheatHandler("party", OnProcessCheat_party, "Run a variety of party related commands", "[sub command] [subcommand args]", "list");
			cheatMgr.RegisterCheatHandler("raf", OnProcessCheat_raf, "Run a RAF UI related commands", "[subcommand]", "showprogress");
			cheatMgr.RegisterCheatHandler("flist", OnProcessCheat_friendlist, "Run various friends list cheats.", "[subcommand] [subcommand args]", "add remove");
			cheatMgr.RegisterCheatHandler("fsg", OnProcessCheat_fsg, "Run a variety of Fireside Gathering related commands", "[subcommand] [subcommand args]", "view");
			cheatMgr.RegisterCheatHandler("gps", OnProcessCheat_GPS, "Modify GPS information in editor", "[subcommand] [subcommand args]", "view");
			cheatMgr.RegisterCheatHandler("wifi", OnProcessCheat_Wifi, "Modify WIFI information in editor", "[subcommand] [subcommand args]", "view");
			cheatMgr.RegisterCheatHandler("social", OnProcessCheat_social, "View information about the social list (friends, nearby players, FSG patrons, etc)", "[subcommand] [subcommand args]", "list");
			cheatMgr.RegisterCheatHandler("playstartemote", OnProcessCheat_playStartEmote, " the appropriate start, mirror start, or custom start emote on first the enemy hero, then the friendly hero");
			cheatMgr.RegisterCheatHandler("getbgdenylist", OnProcessCheat_getBattlegroundDenyList, "Get Battleground deny list");
			cheatMgr.RegisterCheatHandler("getbgminionpool", OnProcessCheat_getBattlegroundMinionPool, "Get Battleground minion pool");
			cheatMgr.RegisterCategory("device");
			cheatMgr.RegisterCheatHandler("lowmemorywarning", OnProcessCheat_lowmemorywarning, "Simulate a low memory warning from mobile.");
			cheatMgr.RegisterCheatHandler("mobile", OnProcessCheat_mobile, "Run Mobile related commands", "subcommand [subcommand args]", "subcommand:login|push|ngdp subcommand args:clear|register|logout");
			cheatMgr.RegisterCheatHandler("edittextdebug", OnProcessCheat_edittextdebug, "Toggle EditText debugging");
			cheatMgr.RegisterCategory("streaming");
			cheatMgr.RegisterCheatHandler("setupdateintention", OnProcessCheat_UpdateIntention, "Set the next \"goal\" for the runtime update manager", "[UpdateIntention]");
			cheatMgr.RegisterCheatHandler("updater", OnProcessCheat_Updater, "Modify the properties of Updater", "[subcommand] [subcommand args]", "speed");
			cheatMgr.RegisterCategory("assets");
			cheatMgr.RegisterCheatHandler("printassethandles", OnProcessCheat_Assets, "Prints outstanding AssetHandles", "[filter]");
			cheatMgr.RegisterCheatHandler("printassetbundles", OnProcessCheat_Assets, "Prints open AssetBundles", "[filter]");
			cheatMgr.RegisterCheatHandler("orphanasset", OnProcessCheat_Assets, "Orphans an AssetHandle");
			cheatMgr.RegisterCheatHandler("orphanprefab", OnProcessCheat_Assets, "Orphans a shared prefab");
			cheatMgr.RegisterCategory("account data");
			cheatMgr.RegisterCheatHandler("account", OnProcessCheat_account, "Account management cheat");
			cheatMgr.RegisterCheatHandler("cloud", OnProcessCheat_cloud, "Run Cloud Storage related commands", "[subcommand]", "set");
			cheatMgr.RegisterCheatHandler("tempaccount", OnProcessCheat_tempaccount, "Run Temporary Account related commands", "[subcommand]", "dialog");
			cheatMgr.RegisterCheatHandler("getgsd", OnProcessCheat_GetGameSaveData, "Request the value of a particular Game Save Data subkey.", "[key] [subkey]", "24 13");
			cheatMgr.RegisterCheatHandler("gsd", OnProcessCheat_GetGameSaveData, "Request the value of a particular Game Save Data subkey.", "[key] [subkey]", "24 13");
			cheatMgr.RegisterCheatHandler("setgsd", OnProcessCheat_SetGameSaveData, "Set the value(s) of a Game Save Data subkey. Can provide multiple values to set a list.", "[key] [subkey] [int_value]", "24 13 2");
			cheatMgr.RegisterCategory("adventure");
			cheatMgr.RegisterCheatHandler("adventureChallengeUnlock", OnProcessCheat_adventureChallengeUnlock, "Show adventure challenge unlock", "<wing number>");
			cheatMgr.RegisterCheatHandler("advevent", OnProcessCheat_advevent, "Trigger an AdventureWingEventTable event.", "<event name>", "PlateOpen");
			cheatMgr.RegisterCheatHandler("showadventureloadingpopup", OnProcessCheat_ShowAdventureLoadingPopup, "Show the popup for loading into the currently-set Adventure mission.");
			cheatMgr.RegisterCheatHandler("hidegametransitionpopup", OnProcessCheat_HideGameTransitionPopup, "Hide any currently shown game transition popup.");
			cheatMgr.RegisterCheatHandler("setallpuzzlesinprogress", OnProcessCheat_SetAllPuzzlesInProgress, "Set the sub-puzzle progress for each puzzle to be on the final puzzle.");
			cheatMgr.RegisterCheatHandler("unlockhagatha", OnProcessCheat_UnlockHagatha, "Set up the hagatha unlock flow. After running the cheat, complete a monster hunt to unlock.");
			cheatMgr.RegisterCheatHandler("setadventurecomingsoon", OnProcessCheat_SetAdventureComingSoon, "Set the Coming Soon state of an adventure.");
			cheatMgr.RegisterCheatHandler("resetsessionvo", OnProcessCheat_ResetSession_VO, "Reset the fact that you've seen once per session related VO, to be able to hear it again.");
			cheatMgr.RegisterCheatHandler("setvochance", OnProcessCheat_SetVOChance_VO, "Set an override on the chance to play a VO line in the adventure. This will only override the chance on VO that won't always play.", "<chance>", "0.1");
			cheatMgr.RegisterCategory("adventure:dungeon run");
			cheatMgr.RegisterCheatHandler("setdrprogress", OnProcessCheat_SetDungeonRunProgress, "Set how many bosses you've defeated during an active run in the provided Adventure.", "[adventure abbreviation] [num bosses] [next boss id (optional)]", "uld 7 46589");
			cheatMgr.RegisterCheatHandler("setdrvictory", OnProcessCheat_SetDungeonRunVictory, "Set victory in the provided Adventure.", "<adventure abbreviation>", "uld");
			cheatMgr.RegisterCheatHandler("setdrdefeat", OnProcessCheat_SetDungeonRunDefeat, "Set defeat and how many bosses you've defeated in the provided Adventure.", "[adventure abbreviation] [num bosses]", "uld 7");
			cheatMgr.RegisterCheatHandler("resetdradventure", OnProcessCheat_ResetDungeonRunAdventure, "Reset the current run for the provided Adventure.", "[adventure abbreviation]", "uld");
			cheatMgr.RegisterCheatAlias("resetdradventure", "resetdrrun");
			cheatMgr.RegisterCheatHandler("resetdrvo", OnProcessCheat_ResetDungeonRun_VO, "Reset the fact that you've seen all VO related to the provided Adventure, to be able to hear it again.", "[adventure abbreviation] [optional:value to set subkeys to]", "uld 1");
			cheatMgr.RegisterCheatHandler("unlockloadout", OnProcessCheat_UnlockLoadout, "Unlock all loadout options for the provided Adventure.", "[adventure abbreviation]", "uld");
			cheatMgr.RegisterCheatHandler("lockloadout", OnProcessCheat_LockLoadout, "Lock all loadout options for the provided Adventure.", "[adventure abbreviation]", "uld");
			cheatMgr.RegisterCategory("adventure:k&c");
			cheatMgr.RegisterCheatHandler("setkcprogress", OnProcessCheat_SetKCProgress, "Set how many bosses you've defeated during an active run in Kobolds & Catacombs.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setkcvictory", OnProcessCheat_SetKCVictory, "Set victory in Kobolds & Catacombs.");
			cheatMgr.RegisterCheatHandler("setkcdefeat", OnProcessCheat_SetKCDefeat, "Set defeat and how many bosses you've defeated in Kobolds & Catacombs.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resetkcvo", OnProcessCheat_ResetKC_VO, "Reset the fact that you've seen all K&C related VO, to be able to hear it again.");
			cheatMgr.RegisterCategory("adventure:witchwood");
			cheatMgr.RegisterCheatHandler("setgilprogress", OnProcessCheat_SetGILProgress, "Set how many bosses you've defeated during an active run in Witchwood.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setgilvictory", OnProcessCheat_SetGILVictory, "Set victory in Witchwood.");
			cheatMgr.RegisterCheatHandler("setgildefeat", OnProcessCheat_SetGILDefeat, "Set defeat and how many bosses you've defeated in Witchwood.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("setgilbonus", OnProcessCheat_SetGILBonus, "Set the Witchwood bonus challenge to be active.");
			cheatMgr.RegisterCheatHandler("resetGilAdventure", OnProcessCheat_ResetGILAdventure, "Reset the current Witchwood Adventure run.");
			cheatMgr.RegisterCheatHandler("resetgilvo", OnProcessCheat_ResetGIL_VO, "Reset the fact that you've seen all Witchwood related VO, to be able to hear it again.");
			cheatMgr.RegisterCategory("adventure:rastakhan");
			cheatMgr.RegisterCheatHandler("settrlprogress", OnProcessCheat_SetTRLProgress, "Set how many bosses you've defeated during an active run in Rastakhan.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("settrlvictory", OnProcessCheat_SetTRLVictory, "Set victory in Rastakhan.");
			cheatMgr.RegisterCheatHandler("settrldefeat", OnProcessCheat_SetTRLDefeat, "Set defeat and how many bosses you've defeated in Rastakhan.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resettrlvo", OnProcessCheat_ResetTRL_VO, "Reset the fact that you've seen all Rastakhan related VO, to be able to hear it again.");
			cheatMgr.RegisterCategory("adventure:dalaran");
			cheatMgr.RegisterCheatHandler("setdalprogress", OnProcessCheat_SetDALProgress, "Set how many bosses you've defeated during an active run in Dalaran.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setdalvictory", OnProcessCheat_SetDALVictory, "Set victory in Dalaran.");
			cheatMgr.RegisterCheatHandler("setdaldefeat", OnProcessCheat_SetDALDefeat, "Set defeat and how many bosses you've defeated in Dalaran.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resetDalaranAdventure", OnProcessCheat_ResetDalaranAdventure, "Reset the current Dalaran Adventure run, so you can start at the location selection again.");
			cheatMgr.RegisterCheatHandler("setdalheroicprogress", OnProcessCheat_SetDALHeroicProgress, "Set how many bosses you've defeated during an active run in Dalaran Heroic.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setdalheroicvictory", OnProcessCheat_SetDALHeroicVictory, "Set victory in Dalaran Heroic.");
			cheatMgr.RegisterCheatHandler("setdalheroicdefeat", OnProcessCheat_SetDALHeroicDefeat, "Set defeat and how many bosses you've defeated in Dalaran Heroic.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resetDalaranHeroicAdventure", OnProcessCheat_ResetDalaranHeroicAdventure, "Reset the current Dalaran Heroic Adventure run, so you can start at the location selection again.");
			cheatMgr.RegisterCheatHandler("resetdalvo", OnProcessCheat_ResetDAL_VO, "Reset the fact that you've seen all Dalaran related VO, to be able to hear it again.");
			cheatMgr.RegisterCategory("adventure:uldum");
			cheatMgr.RegisterCheatHandler("setuldprogress", OnProcessCheat_SetULDProgress, "Set how many bosses you've defeated during an active run in Uldum.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setuldvictory", OnProcessCheat_SetULDVictory, "Set victory in Uldum.");
			cheatMgr.RegisterCheatHandler("setulddefeat", OnProcessCheat_SetULDDefeat, "Set defeat and how many bosses you've defeated in Uldum.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resetuldrun", OnProcessCheat_ResetUldumAdventure, "Reset the current Uldum Adventure run, so you can start at the location selection again.");
			cheatMgr.RegisterCheatAlias("resetuldrun", "resetUldumAdventure");
			cheatMgr.RegisterCheatHandler("setuldheroicprogress", OnProcessCheat_SetULDHeroicProgress, "Set how many bosses you've defeated during an active run in Uldum Heroic.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setuldheroicvictory", OnProcessCheat_SetULDHeroicVictory, "Set victory in Uldum Heroic.");
			cheatMgr.RegisterCheatHandler("setuldheroicdefeat", OnProcessCheat_SetULDHeroicDefeat, "Set defeat and how many bosses you've defeated in Uldum Heroic.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resetuldheroicrun", OnProcessCheat_ResetUldumHeroicAdventure, "Reset the current Uldum Heroic Adventure run, so you can start at the location selection again.");
			cheatMgr.RegisterCheatAlias("resetuldheroicrun", "resetUldumHeroicAdventure");
			cheatMgr.RegisterCheatHandler("resetuldvo", OnProcessCheat_ResetULD_VO, "Reset the fact that you've seen all Uldum related VO, to be able to hear it again.");
			cheatMgr.DefaultCategory();
			cheatMgr.RegisterCheatHandler("brode", OnProcessCheat_brode, "Brode's personal cheat", "", "");
			cheatMgr.RegisterCheatHandler("freeyourmind", OnProcessCheat_freeyourmind, "And the rest will follow");
		}
		cheatMgr.RegisterCategory("config");
		cheatMgr.RegisterCheatHandler("has", OnProcessCheat_HasOption, "Query whether a Game Option exists.");
		cheatMgr.RegisterCheatHandler("get", OnProcessCheat_GetOption, "Get the value of a Game Option.");
		cheatMgr.RegisterCheatHandler("set", OnProcessCheat_SetOption, "Set the value of a Game Option.");
		cheatMgr.RegisterCheatHandler("getvar", OnProcessCheat_GetVar, "Get the value of a client.config var.");
		cheatMgr.RegisterCheatHandler("setvar", OnProcessCheat_SetVar, "Set the value of a client.config var.");
		cheatMgr.RegisterCheatHandler("delete", OnProcessCheat_DeleteOption, "Delete a Game Option; the absence of option may trigger default behavior");
		cheatMgr.RegisterCheatAlias("delete", "del");
		cheatMgr.RegisterCategory("ui");
		cheatMgr.RegisterCheatHandler("nav", OnProcessCheat_navigation, "Debug Navigation.GoBack");
		cheatMgr.RegisterCheatAlias("nav", "navigate");
		cheatMgr.RegisterCheatHandler("warning", OnProcessCheat_warning, "Show a warning message", "<message>", "Test You're a cheater and you've been warned!");
		cheatMgr.RegisterCheatHandler("fatal", OnProcessCheat_fatal, "Brings up the Fatal Error screen", "<error to display>", "Hearthstone cheated and failed!");
		cheatMgr.RegisterCheatHandler("alert", OnProcessCheat_alert, "Show a popup alert", "header=<string> text=<string> icon=<bool> response=<ok|confirm|cancel|confirm_cancel> oktext=<string> confirmtext=<string>", "header=header text=body text icon=true response=confirm");
		cheatMgr.RegisterCheatAlias("alert", "popup", "dialog");
		cheatMgr.RegisterCheatHandler("rankedintropopup", OnProcessCheat_rankedIntroPopup, "Show the Ranked Intro Popup");
		cheatMgr.RegisterCheatHandler("setrotationrotatedboosterspopup", OnProcessCheat_setRotationRotatedBoostersPopup, "Show the Set Rotation Tutorial Popup");
		cheatMgr.RegisterCategory("game modes");
		cheatMgr.RegisterCheatHandler("autodraft", OnProcessCheat_autodraft, "Sets Arena autodraft on/off.", "<on | off>", "on");
		cheatMgr.RegisterCategory("general");
		cheatMgr.RegisterCheatHandler("log", OnProcessCheat_log);
		cheatMgr.RegisterCheatHandler("ip", OnProcessCheat_IPAddress);
		cheatMgr.RegisterCategory("program");
		cheatMgr.RegisterCheatHandler("exit", OnProcessCheat_exit, "Exit the application", "", "");
		cheatMgr.RegisterCheatAlias("exit", "quit");
		cheatMgr.RegisterCheatHandler("pause", (CheatMgr.ProcessCheatCallback)delegate
		{
			HearthstoneApplication.Get().OnApplicationPause(pauseStatus: true);
			return true;
		}, (string)null, (string)null, (string)null);
		cheatMgr.RegisterCheatHandler("unpause", (CheatMgr.ProcessCheatCallback)delegate
		{
			HearthstoneApplication.Get().OnApplicationPause(pauseStatus: false);
			return true;
		}, (string)null, (string)null, (string)null);
		cheatMgr.RegisterCategory("account data");
		cheatMgr.RegisterCheatHandler("clearofflinelocalcache", (CheatMgr.ProcessCheatCallback)delegate
		{
			OfflineDataCache.ClearLocalCacheFile();
			return true;
		}, (string)null, (string)null, (string)null);
		cheatMgr.RegisterCheatHandler("herocount", OnProcessCheat_HeroCount, "Set the hero picker count and reload UI", "number of heroes to display 1-12", "12");
		cheatMgr.DefaultCategory();
		cheatMgr.RegisterCheatHandler("attribution", OnProcessCheat_Attribution);
		cheatMgr.RegisterCheatHandler("crm", OnProcessCheat_CRM);
		cheatMgr.RegisterCategory("progression");
		cheatMgr.RegisterCheatHandler("checkfornewquests", OnProcessCheat_checkfornewquests, "Trigger a check for next quests after n secs (default 0)", "[delaySecs]", "1");
		cheatMgr.RegisterCheatHandler("showachievementreward", OnProcessCheat_showachievementreward, "show a fake achievement reward scroll");
		cheatMgr.RegisterCheatHandler("showquestreward", OnProcessCheat_showquestreward, "show a fake quest reward scroll");
		cheatMgr.RegisterCheatHandler("showtrackreward", OnProcessCheat_showtrackreward, "show a fake track reward scroll");
		cheatMgr.RegisterCheatHandler("showquestprogresstoast", OnProcessCheat_showquestprogresstoast, "Pop up a quest progress toast widget", "<quest id>", "2");
		cheatMgr.RegisterCheatHandler("showquestnotification", OnProcessCheat_showquestnotification, "Shows the quest notification popup widget", "<daily|weekly>", "daily");
		cheatMgr.RegisterCheatHandler("showachievementtoast", OnProcessCheat_showachievementtoast, "Pop up a achievement complete toast widget", "<achieve id>", "2");
		cheatMgr.RegisterCheatHandler("showprogtileids", OnProcessCheat_showprogtileids, "Show the quest id or achievement id on quest and achievement tiles");
		cheatMgr.RegisterCheatHandler("simendofgamexp", OnProcessCheat_simendofgamexp, "Simulate different end of game situations and show end of game xp screen.", "<scenario id>", "1");
		cheatMgr.RegisterCheatHandler("showunclaimedtrackrewards", OnProcessCheat_showunclaimedtrackrewards, "Show the reward track's unclaimed rewards popup.");
		cheatMgr.RegisterCheatHandler("shownotavernpasswarning", OnProcessCheat_shownotavernpasswarning, "Shows the warning popup when no tavern pass is available");
		cheatMgr.RegisterCheatHandler("setlastrewardtrackseasonseen", OnProcessCheat_setlastrewardtrackseasonseen, "Sets the GSD value of Rewards Track: Season Last Seen");
		cheatMgr.RegisterCheatHandler("apprating", OnProcessCheat_ShowAppRatingPrompt, "Shows the app review popup (Android and iOS only).");
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(CheatMgr),
			typeof(Network)
		};
	}

	public void Shutdown()
	{
		s_instance = null;
	}

	public void OnCollectionManagerReady()
	{
		ConfigFile configFile = new ConfigFile();
		if (configFile.FullLoad(Vars.GetClientConfigPath()) && configFile.Get("Cheats.InstantGameplay", defaultVal: false))
		{
			configFile.Set("Cheats.InstantGameplay", val: false);
			configFile.Save();
			m_quickLaunchState = new QuickLaunchState();
			m_quickLaunchState.m_skipMulligan = true;
			LaunchQuickGame(260);
		}
	}

	public void OnMulliganEnded()
	{
		ConfigFile configFile = new ConfigFile();
		if (!configFile.FullLoad(Vars.GetClientConfigPath()))
		{
			return;
		}
		string text = configFile.Get("Cheats.InstantCheatCommands", null);
		if (!string.IsNullOrEmpty(text))
		{
			configFile.Set("Cheats.InstantCheatCommands", null);
			configFile.Save();
			string[] array = text.Split(',');
			foreach (string command in array)
			{
				Network.Get().SendDebugConsoleCommand(command);
			}
		}
	}

	public int GetBoardId()
	{
		return m_boardId;
	}

	public void ClearBoardId()
	{
		m_boardId = 0;
	}

	public bool HasCheatTreasureIds()
	{
		return m_pvpdrTreasureIds.Count > 0;
	}

	public void ClearCheatTreasures()
	{
		m_pvpdrTreasureIds.Clear();
	}

	public bool HasCheatLootIds()
	{
		return m_pvpdrLootIds.Count > 0;
	}

	public void ClearCheatLoot()
	{
		m_pvpdrLootIds.Clear();
	}

	public bool IsSpeechBubbleEnabled()
	{
		return m_speechBubblesEnabled;
	}

	public bool IsSoundCategoryEnabled(Global.SoundCategory sc)
	{
		if (m_audioChannelEnabled.ContainsKey(sc))
		{
			return m_audioChannelEnabled[sc];
		}
		return true;
	}

	public string GetPlayerTags()
	{
		return m_playerTags;
	}

	public void ClearAllPlayerTags()
	{
		m_playerTags = "";
	}

	public bool IsNewCardInPackOpeningEnabed()
	{
		return m_isNewCardInPackOpeningEnabled;
	}

	public bool IsLaunchingQuickGame()
	{
		return m_quickLaunchState.m_launching;
	}

	public bool ShouldSkipMulligan()
	{
		if (Options.Get().GetBool(Option.SKIP_ALL_MULLIGANS))
		{
			return true;
		}
		return m_quickLaunchState.m_skipMulligan;
	}

	public bool ShouldSkipSendingGetGameState()
	{
		return m_skipSendingGetGameState;
	}

	public bool QuickGameFlipHeroes()
	{
		return m_quickLaunchState.m_flipHeroes;
	}

	public bool QuickGameMirrorHeroes()
	{
		return m_quickLaunchState.m_mirrorHeroes;
	}

	public string QuickGameOpponentHeroCardId()
	{
		return m_quickLaunchState.m_opponentHeroCardId;
	}

	public bool HandleKeyboardInput()
	{
		if (HearthstoneApplication.IsInternal() && HandleQuickPlayInput())
		{
			return true;
		}
		return false;
	}

	public void SaveDuelsCheatTreasures(out List<int> addedTreasures)
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		addedTreasures = new List<int>();
		if (m_pvpdrTreasureIds.Count() <= 0 || selectedAdventureDataRecord == null)
		{
			return;
		}
		List<long> values = null;
		GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_OPTION, out values);
		if (values == null)
		{
			return;
		}
		int num = Math.Min(m_pvpdrTreasureIds.Count, values.Count);
		for (int i = 0; i < num; i++)
		{
			int num2 = m_pvpdrTreasureIds.Dequeue();
			if (num2 > 0)
			{
				values[i] = num2;
				addedTreasures.Add(num2);
			}
		}
		InvokeSetGameSaveDataCheat((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_OPTION, values.ToArray());
	}

	private bool AddCheatLootToBucket(AdventureDataDbfRecord dataRecord, GameSaveKeySubkeyId subkey, List<int> addedLoot)
	{
		List<long> values = null;
		GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)dataRecord.GameSaveDataServerKey, subkey, out values);
		if (values == null || values.Count < 4)
		{
			return false;
		}
		for (int i = 0; i < 3; i++)
		{
			if (m_pvpdrLootIds.Count == 0)
			{
				break;
			}
			int num = m_pvpdrLootIds.Dequeue();
			if (num > 0)
			{
				values[i + 1] = num;
				addedLoot.Add(num);
			}
		}
		InvokeSetGameSaveDataCheat((GameSaveKeyId)dataRecord.GameSaveDataServerKey, subkey, values.ToArray());
		return true;
	}

	public void SaveDuelsCheatLoot(out List<int> addedLootA, out List<int> addedLootB, out List<int> addedLootC)
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		addedLootA = new List<int>();
		addedLootB = new List<int>();
		addedLootC = new List<int>();
		if (m_pvpdrLootIds.Count > 0 && selectedAdventureDataRecord != null && AddCheatLootToBucket(selectedAdventureDataRecord, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_A, addedLootA) && m_pvpdrLootIds.Count > 0 && AddCheatLootToBucket(selectedAdventureDataRecord, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_B, addedLootB) && m_pvpdrLootIds.Count > 0)
		{
			AddCheatLootToBucket(selectedAdventureDataRecord, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_C, addedLootC);
		}
	}

	private void ParseErrorText(string[] args, string rawArgs, out string header, out string message)
	{
		header = ((args.Length == 0) ? "[PH] Header" : args[0]);
		if (args.Length <= 1)
		{
			message = "[PH] Message";
			return;
		}
		int startIndex = 0;
		bool flag = false;
		for (int i = 0; i < rawArgs.Length; i++)
		{
			if (char.IsWhiteSpace(rawArgs[i]))
			{
				if (flag)
				{
					startIndex = i;
					break;
				}
			}
			else
			{
				flag = true;
			}
		}
		message = rawArgs.Substring(startIndex).Trim();
	}

	private AlertPopup.PopupInfo GenerateAlertInfo(string rawArgs)
	{
		Map<string, string> map = ParseAlertArgs(rawArgs);
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo
		{
			m_showAlertIcon = false,
			m_headerText = "Header",
			m_text = "Message",
			m_responseDisplay = AlertPopup.ResponseDisplay.OK,
			m_okText = "OK",
			m_confirmText = "Confirm",
			m_cancelText = "Cancel"
		};
		foreach (KeyValuePair<string, string> item in map)
		{
			string key = item.Key;
			string value = item.Value;
			if (key.Equals("header"))
			{
				popupInfo.m_headerText = value;
			}
			else if (key.Equals("text"))
			{
				popupInfo.m_text = value;
			}
			else if (key.Equals("response"))
			{
				value = value.ToLowerInvariant();
				if (value.Equals("ok"))
				{
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				}
				else if (value.Equals("confirm"))
				{
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
				}
				else if (value.Equals("cancel"))
				{
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
				}
				else if (value.Equals("confirm_cancel") || value.Equals("cancel_confirm"))
				{
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
				}
			}
			else if (key.Equals("icon"))
			{
				popupInfo.m_showAlertIcon = GeneralUtils.ForceBool(value);
			}
			else if (key.Equals("oktext"))
			{
				popupInfo.m_okText = value;
			}
			else if (key.Equals("confirmtext"))
			{
				popupInfo.m_confirmText = value;
			}
			else if (key.Equals("canceltext"))
			{
				popupInfo.m_cancelText = value;
			}
			else if (key.Equals("offset"))
			{
				string[] array = value.Split();
				Vector3 offset = default(Vector3);
				if (array.Length % 2 == 0)
				{
					for (int i = 0; i < array.Length; i += 2)
					{
						string text = array[i].ToLowerInvariant();
						string str = array[i + 1];
						if (text.Equals("x"))
						{
							offset.x = GeneralUtils.ForceFloat(str);
						}
						else if (text.Equals("y"))
						{
							offset.y = GeneralUtils.ForceFloat(str);
						}
						else if (text.Equals("z"))
						{
							offset.z = GeneralUtils.ForceFloat(str);
						}
					}
				}
				popupInfo.m_offset = offset;
			}
			else if (key.Equals("padding"))
			{
				popupInfo.m_padding = GeneralUtils.ForceFloat(value);
			}
			else
			{
				if (!key.Equals("align"))
				{
					continue;
				}
				string[] array2 = value.Split('|');
				for (int j = 0; j < array2.Length; j++)
				{
					switch (array2[j].ToLower())
					{
					case "left":
						popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Left;
						break;
					case "center":
						popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
						break;
					case "right":
						popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Right;
						break;
					case "top":
						popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Upper;
						break;
					case "middle":
						popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
						break;
					case "bottom":
						popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Lower;
						break;
					}
				}
			}
		}
		return popupInfo;
	}

	private Map<string, string> ParseAlertArgs(string rawArgs)
	{
		Map<string, string> map = new Map<string, string>();
		int num = -1;
		int num2 = -1;
		string text = null;
		for (int i = 0; i < rawArgs.Length; i++)
		{
			if (rawArgs[i] != '=')
			{
				continue;
			}
			int num3 = -1;
			for (int num4 = i - 1; num4 >= 0; num4--)
			{
				char c = rawArgs[num4];
				char c2 = rawArgs[num4 + 1];
				if (!char.IsWhiteSpace(c))
				{
					num3 = num4;
				}
				if (char.IsWhiteSpace(c) && !char.IsWhiteSpace(c2))
				{
					break;
				}
			}
			if (num3 >= 0)
			{
				num2 = num3 - 2;
				if (text != null)
				{
					map[text] = rawArgs.Substring(num, num2 - num + 1);
				}
				num = i + 1;
				text = rawArgs.Substring(num3, i - num3).Trim().ToLowerInvariant()
					.Replace("\\n", "\n");
			}
		}
		num2 = rawArgs.Length - 1;
		if (text != null)
		{
			map[text] = rawArgs.Substring(num, num2 - num + 1).Replace("\\n", "\n");
		}
		return map;
	}

	private bool OnAlertProcessed(DialogBase dialog, object userData)
	{
		m_alert = (AlertPopup)dialog;
		return true;
	}

	private void HideAlert()
	{
		if (m_alert != null)
		{
			m_alert.Hide();
			m_alert = null;
		}
	}

	private bool HandleQuickPlayInput()
	{
		if (!HearthstoneServices.IsAvailable<SceneMgr>())
		{
			return false;
		}
		if (!InputCollection.GetKey(KeyCode.LeftShift) && !InputCollection.GetKey(KeyCode.RightShift))
		{
			return false;
		}
		if (InputCollection.GetKeyDown(KeyCode.F12))
		{
			PrintQuickPlayLegend();
			return false;
		}
		if (GetQuickLaunchAvailability() != 0)
		{
			return false;
		}
		ScenarioDbId scenarioDbId = ScenarioDbId.INVALID;
		string opponentHeroCardId = null;
		foreach (KeyValuePair<KeyCode, ScenarioDbId> item in s_quickPlayKeyMap)
		{
			KeyCode key = item.Key;
			ScenarioDbId value = item.Value;
			if (InputCollection.GetKeyDown(key))
			{
				scenarioDbId = value;
				opponentHeroCardId = s_opponentHeroKeyMap[key];
				break;
			}
		}
		if (scenarioDbId == ScenarioDbId.INVALID)
		{
			return false;
		}
		m_quickLaunchState.m_mirrorHeroes = false;
		m_quickLaunchState.m_flipHeroes = false;
		m_quickLaunchState.m_skipMulligan = true;
		m_quickLaunchState.m_opponentHeroCardId = opponentHeroCardId;
		if ((InputCollection.GetKey(KeyCode.RightAlt) || InputCollection.GetKey(KeyCode.LeftAlt)) && (InputCollection.GetKey(KeyCode.RightControl) || InputCollection.GetKey(KeyCode.LeftControl)))
		{
			m_quickLaunchState.m_mirrorHeroes = true;
			m_quickLaunchState.m_skipMulligan = false;
			m_quickLaunchState.m_flipHeroes = false;
		}
		else if (InputCollection.GetKey(KeyCode.RightControl) || InputCollection.GetKey(KeyCode.LeftControl))
		{
			m_quickLaunchState.m_flipHeroes = false;
			m_quickLaunchState.m_skipMulligan = false;
			m_quickLaunchState.m_mirrorHeroes = false;
		}
		else if (InputCollection.GetKey(KeyCode.RightAlt) || InputCollection.GetKey(KeyCode.LeftAlt))
		{
			m_quickLaunchState.m_flipHeroes = true;
			m_quickLaunchState.m_skipMulligan = false;
			m_quickLaunchState.m_mirrorHeroes = false;
		}
		LaunchQuickGame((int)scenarioDbId);
		return true;
	}

	private void PrintQuickPlayLegend()
	{
		string message = $"F1: {GetQuickPlayMissionName(KeyCode.F1)}\nF2: {GetQuickPlayMissionName(KeyCode.F2)}\nF3: {GetQuickPlayMissionName(KeyCode.F3)}\nF4: {GetQuickPlayMissionName(KeyCode.F4)}\nF5: {GetQuickPlayMissionName(KeyCode.F5)}\nF6: {GetQuickPlayMissionName(KeyCode.F6)}\nF7: {GetQuickPlayMissionName(KeyCode.F7)}\nF8: {GetQuickPlayMissionName(KeyCode.F8)}\nF9: {GetQuickPlayMissionName(KeyCode.F9)}\nF10: {GetQuickPlayMissionName(KeyCode.F10)}\n(CTRL and ALT will Show mulligan)\nSHIFT + CTRL = Hero on players side\nSHIFT + ALT = Hero on opponent side\nSHIFT + ALT + CTRL = Hero on both sides";
		if (UIStatus.Get() != null)
		{
			UIStatus.Get().AddInfo(message);
		}
		Debug.Log($"F1: {GetQuickPlayMissionShortName(KeyCode.F1)}  F2: {GetQuickPlayMissionShortName(KeyCode.F2)}  F3: {GetQuickPlayMissionShortName(KeyCode.F3)}  F4: {GetQuickPlayMissionShortName(KeyCode.F4)}  F5: {GetQuickPlayMissionShortName(KeyCode.F5)}  F6: {GetQuickPlayMissionShortName(KeyCode.F6)}  F7: {GetQuickPlayMissionShortName(KeyCode.F7)}  F8: {GetQuickPlayMissionShortName(KeyCode.F8)}  F9: {GetQuickPlayMissionShortName(KeyCode.F9)}\nF10: {GetQuickPlayMissionShortName(KeyCode.F10)}\n(CTRL and ALT will Show mulligan) -- SHIFT + CTRL = Hero on players side -- SHIFT + ALT = Hero on opponent side -- SHIFT + ALT + CTRL = Hero on both sides");
	}

	private string GetQuickPlayMissionName(KeyCode keyCode)
	{
		return GetQuickPlayMissionName((int)s_quickPlayKeyMap[keyCode]);
	}

	private string GetQuickPlayMissionShortName(KeyCode keyCode)
	{
		return GetQuickPlayMissionShortName((int)s_quickPlayKeyMap[keyCode]);
	}

	private string GetQuickPlayMissionName(int missionId)
	{
		return GetQuickPlayMissionNameImpl(missionId, "NAME");
	}

	private string GetQuickPlayMissionShortName(int missionId)
	{
		return GetQuickPlayMissionNameImpl(missionId, "SHORT_NAME");
	}

	private string GetQuickPlayMissionNameImpl(int missionId, string columnName)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record != null)
		{
			DbfLocValue dbfLocValue = (DbfLocValue)record.GetVar(columnName);
			if (dbfLocValue != null)
			{
				return dbfLocValue.GetString();
			}
		}
		string result = missionId.ToString();
		try
		{
			ScenarioDbId scenarioDbId = (ScenarioDbId)missionId;
			result = scenarioDbId.ToString();
			return result;
		}
		catch (Exception)
		{
			return result;
		}
	}

	private QuickLaunchAvailability GetQuickLaunchAvailability()
	{
		if (m_quickLaunchState.m_launching)
		{
			return QuickLaunchAvailability.ACTIVE_GAME;
		}
		if (SceneMgr.Get().IsInGame())
		{
			return QuickLaunchAvailability.ACTIVE_GAME;
		}
		if (GameMgr.Get().IsFindingGame())
		{
			return QuickLaunchAvailability.FINDING_GAME;
		}
		if (SceneMgr.Get().GetNextMode() != 0)
		{
			return QuickLaunchAvailability.SCENE_TRANSITION;
		}
		if (!SceneMgr.Get().IsSceneLoaded())
		{
			return QuickLaunchAvailability.SCENE_TRANSITION;
		}
		if (LoadingScreen.Get().IsTransitioning())
		{
			return QuickLaunchAvailability.ACTIVE_GAME;
		}
		if (CollectionManager.Get() == null || !CollectionManager.Get().IsFullyLoaded())
		{
			return QuickLaunchAvailability.COLLECTION_NOT_READY;
		}
		return QuickLaunchAvailability.OK;
	}

	private void LaunchQuickGame(int missionId, GameType gameType = GameType.GT_VS_AI, FormatType formatType = FormatType.FT_WILD, CollectionDeck deck = null, string aiDeck = null, GameType progFilterOverride = GameType.GT_UNKNOWN)
	{
		long num;
		string name;
		if (deck == null)
		{
			CollectionManager collectionManager = CollectionManager.Get();
			num = Options.Get().GetLong(Option.LAST_CUSTOM_DECK_CHOSEN);
			deck = collectionManager.GetDeck(num);
			if (deck == null)
			{
				TAG_CLASS defaultClass = TAG_CLASS.MAGE;
				List<CollectionDeck> decks = collectionManager.GetDecks(DeckType.NORMAL_DECK);
				deck = decks.Where((CollectionDeck x) => x.GetClass() == defaultClass).FirstOrDefault();
				if (deck == null)
				{
					deck = decks.FirstOrDefault();
					if (deck == null)
					{
						Debug.LogError("Could not launch quick game because the account has no decks. Please add at least one deck to your account");
						return;
					}
				}
				num = deck.ID;
				name = deck.Name;
			}
			else
			{
				name = deck.Name;
			}
		}
		else
		{
			num = deck.ID;
			name = deck.Name;
		}
		ReconnectMgr.Get().SetBypassReconnect(shouldBypass: true);
		m_quickLaunchState.m_launching = true;
		string quickPlayMissionName = GetQuickPlayMissionName(missionId);
		string message = $"Launching {quickPlayMissionName}\nDeck: {name}";
		UIStatus.Get().AddInfo(message);
		TimeScaleMgr.Get().PushTemporarySpeedIncrease(4f);
		SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
		GameMgr.Get().SetPendingAutoConcede(pendingAutoConcede: true);
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		GameMgr.Get().FindGame(gameType, formatType, missionId, 0, num, aiDeck, null, restoreSavedGameState: false, null, progFilterOverride);
	}

	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			HideAlert();
			m_isInGameplayScene = true;
		}
		if (m_isInGameplayScene && mode != SceneMgr.Mode.GAMEPLAY)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(OnSceneLoaded);
			m_quickLaunchState = new QuickLaunchState();
			m_isInGameplayScene = false;
		}
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if ((uint)(state - 2) <= 1u || (uint)(state - 7) <= 1u || state == FindGameState.SERVER_GAME_CANCELED)
		{
			GameMgr.Get().UnregisterFindGameEvent(OnFindGameEvent);
			m_quickLaunchState = new QuickLaunchState();
		}
		return false;
	}

	private JsonList GetCardlistJson(List<Card> list)
	{
		JsonList jsonList = new JsonList();
		for (int i = 0; i < list.Count; i++)
		{
			JsonNode cardJson = GetCardJson(list[i].GetEntity());
			jsonList.Add(cardJson);
		}
		return jsonList;
	}

	private JsonNode GetCardJson(Entity card)
	{
		if (card == null)
		{
			return null;
		}
		JsonNode jsonNode = new JsonNode();
		jsonNode["cardName"] = card.GetName();
		jsonNode["cardID"] = card.GetCardId();
		jsonNode["entityID"] = (long)card.GetEntityId();
		JsonList jsonList = new JsonList();
		if (card.GetTags() != null)
		{
			foreach (KeyValuePair<int, int> item in card.GetTags().GetMap())
			{
				JsonNode jsonNode2 = new JsonNode();
				string text = Enum.GetName(typeof(GAME_TAG), item.Key);
				if (text == null)
				{
					text = "NOTAG_" + item.Key;
				}
				jsonNode2[text] = (long)item.Value;
				jsonList.Add(jsonNode2);
			}
			jsonNode["tags"] = jsonList;
		}
		JsonList jsonList2 = new JsonList();
		List<Entity> enchantments = card.GetEnchantments();
		for (int i = 0; i < enchantments.Count(); i++)
		{
			JsonNode cardJson = GetCardJson(enchantments[i]);
			jsonList2.Add(cardJson);
		}
		jsonNode["enchantments"] = jsonList2;
		return jsonNode;
	}

	private bool OnProcessCheat_error(string func, string[] args, string rawArgs)
	{
		bool num = args.Length != 0 && (args[0] == "ex" || "except".Equals(args[0], StringComparison.InvariantCultureIgnoreCase) || "exception".Equals(args[0], StringComparison.InvariantCultureIgnoreCase));
		bool flag = args.Length != 0 && (args[0] == "f" || "fatal".Equals(args[0], StringComparison.InvariantCultureIgnoreCase));
		string text = ((args.Length <= 1) ? null : string.Join(" ", args.Skip(1).ToArray()));
		if (num)
		{
			if (text == null)
			{
				text = "This is a simulated Exception.";
			}
			throw new Exception(text);
		}
		if (flag)
		{
			if (text == null)
			{
				text = "This is a simulated Fatal Error.";
			}
			Error.AddFatal(FatalErrorReason.CHEAT, text);
		}
		else
		{
			if (text == null)
			{
				text = "This is a simulated Warning message.";
			}
			Error.AddWarning("Warning", text);
		}
		return true;
	}

	private bool ProcessAutofillParam(IEnumerable<string> values, string searchTerm, AutofillData autofillData)
	{
		values = values.OrderBy((string v) => v);
		string prefix = autofillData.m_lastAutofillParamPrefix ?? searchTerm ?? string.Empty;
		List<string> list = ((!string.IsNullOrEmpty(prefix.Trim())) ? values.Where((string v) => v.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase)).ToList() : values.ToList());
		int num = 0;
		if (autofillData.m_lastAutofillParamMatch != null)
		{
			num = list.IndexOf(autofillData.m_lastAutofillParamMatch);
			if (num >= 0)
			{
				num += ((!autofillData.m_isShiftTab) ? 1 : (-1));
				if (num >= list.Count)
				{
					num = 0;
				}
				else if (num < 0)
				{
					num = list.Count - 1;
				}
			}
		}
		if (num < 0)
		{
			num = 0;
		}
		else if (num >= list.Count)
		{
			autofillData.m_lastAutofillParamPrefix = null;
			autofillData.m_lastAutofillParamMatch = null;
			float num2 = 5f + Mathf.Max(0f, list.Count - 3);
			num2 *= Time.timeScale;
			string arg = string.Join("   ", values.ToArray());
			UIStatus.Get().AddError($"No match for '{searchTerm}'. Available params:\n{arg}", num2);
			return false;
		}
		autofillData.m_lastAutofillParamPrefix = prefix;
		autofillData.m_lastAutofillParamMatch = list[num];
		if (list.Count > 0)
		{
			float num3 = 5f + Mathf.Max(0f, list.Count - 3);
			num3 *= Time.timeScale;
			string text = string.Join("   ", list.ToArray());
			UIStatus.Get().AddInfoNoRichText("Available params:\n" + text, num3);
		}
		return true;
	}

	private bool OnProcessCheat_HasOption(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData != null)
		{
			if (args.Length != 1)
			{
				return false;
			}
			IEnumerable<string> values = from Option v in Enum.GetValues(typeof(Option))
				select EnumUtils.GetString(v);
			return ProcessAutofillParam(values, text, autofillData);
		}
		Option @enum;
		try
		{
			@enum = EnumUtils.GetEnum<Option>(text, StringComparison.OrdinalIgnoreCase);
		}
		catch (ArgumentException)
		{
			return false;
		}
		string message = $"HasOption: {EnumUtils.GetString(@enum)} = {Options.Get().HasOption(@enum)}";
		Debug.Log(message);
		UIStatus.Get().AddInfo(message);
		return true;
	}

	private bool OnProcessCheat_GetOption(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData != null)
		{
			if (args.Length != 1)
			{
				return false;
			}
			IEnumerable<string> values = from Option v in Enum.GetValues(typeof(Option))
				select EnumUtils.GetString(v);
			return ProcessAutofillParam(values, text, autofillData);
		}
		Option @enum;
		try
		{
			@enum = EnumUtils.GetEnum<Option>(text, StringComparison.OrdinalIgnoreCase);
		}
		catch (ArgumentException)
		{
			return false;
		}
		string message = $"GetOption: {EnumUtils.GetString(@enum)} = {Options.Get().GetOption(@enum)}";
		Debug.Log(message);
		UIStatus.Get().AddInfo(message);
		return true;
	}

	private bool OnProcessCheat_SetOption(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData != null)
		{
			if (args.Length != 1)
			{
				return false;
			}
			IEnumerable<string> values = from Option v in Enum.GetValues(typeof(Option))
				select EnumUtils.GetString(v);
			return ProcessAutofillParam(values, text, autofillData);
		}
		Option @enum;
		try
		{
			@enum = EnumUtils.GetEnum<Option>(text, StringComparison.OrdinalIgnoreCase);
		}
		catch (ArgumentException)
		{
			return false;
		}
		if (args.Length < 2)
		{
			return false;
		}
		string text2 = (Options.Get().HasOption(@enum) ? Options.Get().GetOption(@enum).ToString() : "<null>");
		string @string = EnumUtils.GetString(@enum);
		string text3 = args[1];
		Type optionType = Options.Get().GetOptionType(@enum);
		if (optionType == typeof(bool))
		{
			if (!GeneralUtils.TryParseBool(text3, out var boolVal))
			{
				return false;
			}
			Options.Get().SetBool(@enum, boolVal);
		}
		else if (optionType == typeof(int))
		{
			if (!GeneralUtils.TryParseInt(text3, out var val))
			{
				return false;
			}
			Options.Get().SetInt(@enum, val);
		}
		else if (optionType == typeof(long))
		{
			if (!GeneralUtils.TryParseLong(text3, out var val2))
			{
				return false;
			}
			Options.Get().SetLong(@enum, val2);
		}
		else if (optionType == typeof(float))
		{
			if (!GeneralUtils.TryParseFloat(text3, out var val3))
			{
				return false;
			}
			Options.Get().SetFloat(@enum, val3);
		}
		else
		{
			if (!(optionType == typeof(string)))
			{
				string message = $"SetOption: {@string} has unsupported underlying type {optionType}";
				UIStatus.Get().AddError(message);
				return true;
			}
			text3 = rawArgs.Remove(0, text.Length + 1);
			Options.Get().SetString(@enum, text3);
		}
		if (@enum == Option.CURSOR)
		{
			Cursor.visible = Options.Get().GetBool(Option.CURSOR);
		}
		string text4 = (Options.Get().HasOption(@enum) ? Options.Get().GetOption(@enum).ToString() : "<null>");
		string message2 = $"SetOption: {@string} to {text3}.\nPrevious value: {text2}\nNew GetOption: {text4}";
		Debug.Log(message2);
		NetCache.Get().DispatchClientOptionsToServer();
		UIStatus.Get().AddInfo(message2);
		return true;
	}

	private bool OnProcessCheat_GetVar(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData != null)
		{
			if (args.Length != 1)
			{
				return false;
			}
			IEnumerable<string> allKeys = Vars.AllKeys;
			return ProcessAutofillParam(allKeys, text, autofillData);
		}
		string message = string.Format("Var: {0} = {1}", text, Vars.Key(text).GetStr(null) ?? "(null)");
		Debug.Log(message);
		UIStatus.Get().AddInfo(message);
		return true;
	}

	private bool OnProcessCheat_SetVar(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData != null)
		{
			if (args.Length != 1)
			{
				return false;
			}
			IEnumerable<string> allKeys = Vars.AllKeys;
			return ProcessAutofillParam(allKeys, text, autofillData);
		}
		string text2 = ((args.Length < 2) ? null : args[1]);
		Vars.Key(text).Set(text2, permanent: false);
		string message = string.Format("Var: {0} = {1}", text, text2 ?? "(null)");
		Debug.Log(message);
		UIStatus.Get().AddInfo(message);
		if (text.Equals("Arena.AutoDraft", StringComparison.InvariantCultureIgnoreCase) && DraftDisplay.Get() != null)
		{
			DraftDisplay.Get().StartCoroutine(DraftDisplay.Get().RunAutoDraftCheat());
		}
		return true;
	}

	private bool OnProcessCheat_autodraft(string func, string[] args, string rawArgs)
	{
		string text = args[0];
		bool flag = string.IsNullOrEmpty(text) || GeneralUtils.ForceBool(text);
		Vars.Key("Arena.AutoDraft").Set(flag ? "true" : "false", permanent: false);
		if (flag && DraftDisplay.Get() != null)
		{
			TimeScaleMgr.Get().PushTemporarySpeedIncrease(4f);
			DraftDisplay.Get().StartCoroutine(DraftDisplay.Get().RunAutoDraftCheat());
		}
		else if (!flag)
		{
			TimeScaleMgr.Get().PopTemporarySpeedIncrease();
		}
		string message = string.Format("Arena autodraft turned {0}.", flag ? "on" : "off");
		Debug.Log(message);
		UIStatus.Get().AddInfo(message);
		return true;
	}

	private bool OnProcessCheat_HeroCount(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		try
		{
			int.TryParse(args[0], out var result);
			switch (SceneMgr.Get().GetMode())
			{
			case SceneMgr.Mode.ADVENTURE:
				GuestHeroPickerTrayDisplay.Get().CheatLoadHeroButtons(result);
				break;
			case SceneMgr.Mode.TAVERN_BRAWL:
				DeckPickerTrayDisplay.Get().CheatLoadHeroButtons(result);
				break;
			case SceneMgr.Mode.COLLECTIONMANAGER:
				HeroPickerDisplay.Get().CheatLoadHeroButtons(result);
				break;
			default:
				return false;
			}
		}
		catch (ArgumentException)
		{
			return false;
		}
		return true;
	}

	private bool OnProcessCheat_onlygold(string func, string[] args, string rawArgs)
	{
		string text = args[0].ToLowerInvariant();
		switch (text)
		{
		case "gold":
		case "normal":
		case "standard":
			Options.Get().SetString(Option.COLLECTION_PREMIUM_TYPE, text);
			break;
		case "both":
			Options.Get().DeleteOption(Option.COLLECTION_PREMIUM_TYPE);
			break;
		default:
			UIStatus.Get().AddError("Unknown cmd: " + (string.IsNullOrEmpty(text) ? "(blank)" : text) + "\nValid cmds: gold, standard, both");
			return false;
		}
		return true;
	}

	private bool OnProcessCheat_navigation(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
		{
			return true;
		}
		string[] array = new string[7] { "debug", "dump", "back", "pop", "stack", "history", "show" };
		string text = args[0].ToLowerInvariant();
		if (autofillData != null)
		{
			if (!HearthstoneApplication.IsInternal())
			{
				return false;
			}
			return ProcessAutofillParam(array, text, autofillData);
		}
		switch (text)
		{
		case "debug":
			Navigation.NAVIGATION_DEBUG = args.Length < 2 || GeneralUtils.ForceBool(args[1]);
			if (Navigation.NAVIGATION_DEBUG)
			{
				Navigation.DumpStack();
				UIStatus.Get().AddInfo("Navigation debugging turned on - see Console or output log for nav dump.");
			}
			else
			{
				UIStatus.Get().AddInfo("Navigation debugging turned off.");
			}
			break;
		case "dump":
			Navigation.DumpStack();
			UIStatus.Get().AddInfo("Navigation dumped, see Console or output log.");
			break;
		case "back":
		case "pop":
			if (!HearthstoneApplication.IsInternal())
			{
				return false;
			}
			if (!Navigation.CanGoBack)
			{
				string text3 = (Navigation.IsEmpty ? " Stack is empty." : string.Empty);
				UIStatus.Get().AddInfo("Cannot go back at this time." + text3);
				return true;
			}
			Navigation.GoBack();
			break;
		case "stack":
		case "history":
		case "show":
		{
			if (!HearthstoneApplication.IsInternal())
			{
				return false;
			}
			string stackDumpString = Navigation.StackDumpString;
			int num = stackDumpString.Count((char c) => c == '\n');
			float num2 = 5 + 3 * num;
			num2 *= Time.timeScale;
			UIStatus.Get().AddInfo(Navigation.IsEmpty ? "Stack is empty." : stackDumpString, num2);
			break;
		}
		default:
		{
			string text2 = "Unknown cmd: " + (string.IsNullOrEmpty(text) ? "(blank)" : text);
			if (HearthstoneApplication.IsInternal())
			{
				text2 = text2 + "\nValid cmds: " + string.Join(", ", array);
			}
			UIStatus.Get().AddError(text2);
			break;
		}
		}
		return true;
	}

	private bool OnProcessCheat_DeleteOption(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData != null)
		{
			if (args.Length != 1)
			{
				return false;
			}
			IEnumerable<string> values = from Option v in Enum.GetValues(typeof(Option))
				select EnumUtils.GetString(v);
			return ProcessAutofillParam(values, text, autofillData);
		}
		Option @enum;
		try
		{
			@enum = EnumUtils.GetEnum<Option>(text, StringComparison.OrdinalIgnoreCase);
		}
		catch (ArgumentException)
		{
			return false;
		}
		string arg = (Options.Get().HasOption(@enum) ? Options.Get().GetOption(@enum).ToString() : "<null>");
		Options.Get().DeleteOption(@enum);
		string arg2 = (Options.Get().HasOption(@enum) ? Options.Get().GetOption(@enum).ToString() : "<null>");
		string message = $"DeleteOption: {EnumUtils.GetString(@enum)}\nPrevious Value: {arg}\nNew Value: {arg2}";
		Debug.Log(message);
		UIStatus.Get().AddInfo(message);
		return true;
	}

	private bool OnProcessCheat_collectionfirstxp(string func, string[] args, string rawArgs)
	{
		Options.Get().SetInt(Option.COVER_MOUSE_OVERS, 0);
		Options.Get().SetInt(Option.PAGE_MOUSE_OVERS, 0);
		return true;
	}

	private bool OnProcessCheat_board(string func, string[] args, string rawArgs)
	{
		int result = 0;
		m_boardId = (int.TryParse(args[0], out result) ? result : 0);
		UIStatus.Get().AddInfo($"Board for next game set to id {m_boardId}.");
		return true;
	}

	private bool OnProcessCheat_playerTags(string func, string[] args, string rawArgs)
	{
		TryParsePlayerTags(args[0], out m_playerTags);
		return true;
	}

	private bool OnProcessCheat_arenaChoices(string func, string[] args, string rawArgs)
	{
		if (TryParseArenaChoices(args, out var output))
		{
			List<string> list = new List<string>();
			list.Add("arena");
			list.Add("choices");
			string[] array = output;
			foreach (string item in array)
			{
				list.Add(item);
			}
			OnProcessCheat_utilservercmd("util", list.ToArray(), rawArgs, null);
		}
		return true;
	}

	private bool OnProcessCheat_speechBubbles(string func, string[] args, string rawArgs)
	{
		m_speechBubblesEnabled = !m_speechBubblesEnabled;
		UIStatus.Get().AddInfo(string.Format("Speech bubbles {0}.", m_speechBubblesEnabled ? "enabled" : "disabled"));
		return true;
	}

	private bool OnProcessCheat_playAllThinkEmotes(string func, string[] args, string rawArgs)
	{
		if (args.Length != 1)
		{
			UIStatus.Get().AddError("Invalid params for " + func);
			Log.Gameplay.PrintError("Unrecognized number of arguments. Expected \"" + func + " <player>\"");
			return false;
		}
		int num;
		switch (args[0].ToLower())
		{
		case "1":
		case "friendly":
			num = 1;
			break;
		case "2":
		case "opponent":
			num = 2;
			break;
		default:
			UIStatus.Get().AddError("Invalid params for " + func);
			Log.Gameplay.PrintError("Unrecognized player: \"" + args[0] + "\". Expected \"1\", \"2\", \"friendly\", or \"opponent\"");
			return false;
		}
		Entity entity = GameState.Get()?.GetPlayer(num)?.GetHero();
		if (entity == null)
		{
			Log.Gameplay.PrintError($"Unable to find Hero for player {num}");
			return false;
		}
		Card card = entity.GetCard();
		Processor.RunCoroutine(PlayEmotesInOrder(card, EmoteType.THINK1, EmoteType.THINK2, EmoteType.THINK3));
		return true;
	}

	private IEnumerator PlayEmotesInOrder(Card heroCard, params EmoteType[] emoteTypes)
	{
		if (heroCard == null || emoteTypes == null)
		{
			yield break;
		}
		int i = 0;
		while (i < emoteTypes.Length)
		{
			if (heroCard.GetEmoteEntry(emoteTypes[i]) == null)
			{
				string text = $"Unable to locate {emoteTypes[i]} emote for {heroCard}";
				UIStatus.Get().AddError(text);
				Log.Gameplay.PrintError(text);
			}
			else
			{
				heroCard.PlayEmote(emoteTypes[i]);
				if (i < emoteTypes.Length - 1)
				{
					yield return new WaitForSeconds(5f);
				}
			}
			int num = i + 1;
			i = num;
		}
	}

	private bool OnProcessCheat_playEmote(string func, string[] args, string rawArgs)
	{
		if (args.Length != 1 && args.Length != 2)
		{
			UIStatus.Get().AddError("Provide 1 to 2 params for " + func + ".");
			Log.Gameplay.PrintError("Unrecognized number of arguments. Expected \"" + func + " <enum_type> <player>\"");
			return true;
		}
		EmoteType result = EmoteType.INVALID;
		Enum.TryParse<EmoteType>(args[0], ignoreCase: true, out result);
		if (!Enum.IsDefined(typeof(EmoteType), result) || result == EmoteType.INVALID)
		{
			Array names = Enum.GetNames(typeof(EmoteType));
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (string item in names)
			{
				if (num != 0)
				{
					stringBuilder.Append(num);
					stringBuilder.Append(" = ");
					stringBuilder.Append(item);
					stringBuilder.Append('\n');
				}
				num++;
			}
			string text = stringBuilder.ToString();
			UIStatus.Get().AddError("Invalid first param for " + func + ". See \"Messages\".");
			Log.Gameplay.PrintError("Unrecognized <enum_type>.\n" + $"Try a num [1-{names.Length - 1}] or a string:\n" + text);
			return true;
		}
		int id = 1;
		if (args.Length == 2)
		{
			switch (args[1].ToLower())
			{
			case "1":
			case "friendly":
				id = 1;
				break;
			case "2":
			case "opponent":
				id = 2;
				break;
			default:
				UIStatus.Get().AddError("Invalid second param for " + func + ". See \"Messages\".");
				Log.Gameplay.PrintError("Unrecognized player: \"" + args[1] + "\". Expected \"1\", \"2\", \"friendly\", or \"opponent\"");
				return true;
			}
		}
		Card card = GameState.Get()?.GetPlayer(id)?.GetHero()?.GetCard();
		if (card == null)
		{
			Log.Gameplay.PrintError("Unable to find Hero for current player");
			return false;
		}
		card.PlayEmote(result);
		return true;
	}

	private bool OnProcessCheat_playAllMissionHeroPowerLines(string func, string[] args, string rawArgs)
	{
		if (args.Length > 1 || args[0] != string.Empty)
		{
			UIStatus.Get().AddError("Invalid params for " + func);
			Log.Gameplay.PrintError("Unrecognized number of arguments. Expected 0 arguments.");
			return false;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity == null)
		{
			return false;
		}
		string name = "GetBossHeroPowerRandomLines";
		MethodInfo method = gameEntity.GetType().GetMethod(name);
		if (method == null)
		{
			Log.Gameplay.PrintError("This game mode lacks hero power lines.");
			return false;
		}
		List<string> list = method.Invoke(gameEntity, null) as List<string>;
		if (list == null)
		{
			return false;
		}
		Gameplay.Get().StartCoroutine(LoadAndPlayVO(list));
		return true;
	}

	private bool OnProcessCheat_playAllMissionIdleLines(string func, string[] args, string rawArgs)
	{
		if (args.Length > 1 || args[0] != string.Empty)
		{
			UIStatus.Get().AddError("Invalid params for " + func);
			Log.Gameplay.PrintError("Unrecognized number of arguments. Expected 0 arguments.");
			return false;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity == null)
		{
			return false;
		}
		string name = "GetIdleLines";
		MethodInfo method = gameEntity.GetType().GetMethod(name);
		if (method == null)
		{
			Log.Gameplay.PrintError("This game mode lacks idle lines.");
			return false;
		}
		List<string> list = method.Invoke(gameEntity, null) as List<string>;
		if (list == null)
		{
			return false;
		}
		Gameplay.Get().StartCoroutine(LoadAndPlayVO(list));
		return true;
	}

	private IEnumerator LoadAndPlayVO(List<string> assets)
	{
		if (assets == null || assets.Count == 0)
		{
			yield break;
		}
		foreach (string asset in assets)
		{
			if (SoundLoader.LoadSound(asset, OnVoLoaded))
			{
				if (asset != assets.Last())
				{
					yield return new WaitForSeconds(10f);
				}
			}
			else
			{
				string text = "Error loading asset " + asset.ToString();
				Log.Gameplay.PrintError(text);
				UIStatus.Get().AddError(text);
			}
		}
	}

	private void OnVoLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (!(go == null) && !string.IsNullOrEmpty(assetRef))
		{
			Debug.LogFormat("Now playing \"{0}\"", assetRef.ToString());
			AudioSource component = go.GetComponent<AudioSource>();
			SoundManager.Get().PlayPreloaded(component);
			string[] array = assetRef.ToString().Split(':');
			string key = array[0].Substring(0, array[0].Length - ".prefab".Length);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			NotificationManager notificationManager = NotificationManager.Get();
			Notification notification = notificationManager.CreateSpeechBubble(GameStrings.Get(key), Notification.SpeechBubbleDirection.TopRight, actor, bDestroyWhenNewCreated: false);
			notificationManager.DestroyNotification(notification, component.clip.length);
		}
	}

	private bool OnProcessCheat_audioChannel(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Global.SoundCategory value in Enum.GetValues(typeof(Global.SoundCategory)))
			{
				stringBuilder.Append(string.Format("\n{0}: {1}", value, (!m_audioChannelEnabled.ContainsKey(value) || m_audioChannelEnabled[value]) ? "enabled" : "disabled"));
			}
			UIStatus.Get().AddInfo($"Audio channels:{stringBuilder.ToString()}", 5f);
			return true;
		}
		if (args.Length > 2)
		{
			UIStatus.Get().AddError($"Argument format: [audio channel name] [on/off]");
			return true;
		}
		try
		{
			Global.SoundCategory soundCategory2 = (Global.SoundCategory)Enum.Parse(typeof(Global.SoundCategory), args[0], ignoreCase: true);
			if (args.Length == 1 || string.IsNullOrEmpty(args[1]))
			{
				UIStatus.Get().AddInfo(string.Format("Audio channel {0} is {1}", soundCategory2, m_audioChannelEnabled[soundCategory2] ? "on" : "off"));
				return true;
			}
			if (args[1].ToLower() != "on" && args[1].ToLower() != "off")
			{
				UIStatus.Get().AddError($"Second argument must be \"on\" or \"off\"");
				return true;
			}
			m_audioChannelEnabled[soundCategory2] = args[1].ToLower() == "on";
			SoundManager.Get().UpdateCategoryVolume(soundCategory2);
			UIStatus.Get().AddInfo(string.Format("Audio channel {0} has been {1}", soundCategory2, m_audioChannelEnabled[soundCategory2] ? "enabled" : "disabled"));
		}
		catch (ArgumentException)
		{
			UIStatus.Get().AddError($"{args[0]} is not an audio channel. Type audiochannel to see a list of channels.");
		}
		return true;
	}

	private bool OnProcessCheat_audioChannelGroup(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string key in m_audioChannelGroups.Keys)
			{
				stringBuilder.Append($"\n{key}");
			}
			UIStatus.Get().AddInfo($"Audio channel groups:{stringBuilder.ToString()}", 5f);
			return true;
		}
		if (args.Length != 2)
		{
			UIStatus.Get().AddError($"Argument format: [audio channel group name] [on/off]");
			return true;
		}
		if (!m_audioChannelGroups.ContainsKey(args[0].ToUpper()))
		{
			UIStatus.Get().AddError($"{args[0]} is not an audio channel group. Type audiochannelgroup to see a list of channel groups.");
			return true;
		}
		if (args[1].ToLower() != "on" && args[1].ToLower() != "off")
		{
			UIStatus.Get().AddError($"Second argument must be \"on\" or \"off\"");
			return true;
		}
		foreach (Global.SoundCategory item in m_audioChannelGroups[args[0].ToUpper()])
		{
			if (m_audioChannelEnabled.ContainsKey(item))
			{
				m_audioChannelEnabled[item] = args[1].ToLower() == "on";
				SoundManager.Get().UpdateCategoryVolume(item);
			}
		}
		UIStatus.Get().AddInfo(string.Format("Audio channel group {0} has been {1}", args[0], (args[1].ToLower() == "on") ? "enabled" : "disabled"));
		return true;
	}

	private bool TryParsePlayerTags(string input, out string output)
	{
		if (string.IsNullOrEmpty(input))
		{
			UIStatus.Get().AddInfo($"Player tags cleared.");
			output = input;
			return true;
		}
		string[] array = input.Split(',');
		if (array.Length > 20)
		{
			output = "";
			UIStatus.Get().AddError($"{array.Length} tag values found, but only {20} tag values can be passed.");
			return false;
		}
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			string[] array3 = text.Split('=');
			if (array3.Length != 2)
			{
				output = "";
				UIStatus.Get().AddError($"Invalid tag/value entry: \"{text}\". Format is \"TagId=Value\".");
				return false;
			}
			int result = 0;
			int result2 = 0;
			if (!int.TryParse(array3[0], out result))
			{
				output = "";
				UIStatus.Get().AddError($"Invalid tagId: \"{array3[0]}\". Must be an integer.");
				return false;
			}
			if (!int.TryParse(array3[1], out result2))
			{
				result2 = GameUtils.TranslateCardIdToDbId(array3[1], showWarning: true);
				if (result2 == 0)
				{
					output = "";
					UIStatus.Get().AddError($"Invalid tagValue: \"{array3[1]}\". Must be an integer.");
					return false;
				}
			}
			if (result > 999999)
			{
				output = "";
				UIStatus.Get().AddError($"Invalid tagId: \"{result}\". Must be < {999999}.");
				return false;
			}
			if (result <= 0)
			{
				output = "";
				UIStatus.Get().AddError($"Invalid tagId: \"{result}\". Must be > 0.");
				return false;
			}
			if (result2 > 999999)
			{
				output = "";
				UIStatus.Get().AddError($"Invalid tagValue: \"{result2}\". Must be < {999999}.");
				return false;
			}
		}
		UIStatus.Get().AddInfo($"Player tags set for next game.");
		output = input;
		return true;
	}

	private bool TryParseArenaChoices(string[] input, out string[] output)
	{
		List<string> list = new List<string>();
		bool result = input.Length != 0;
		for (int i = 0; i < input.Length; i++)
		{
			string text = input[i].Replace(",", "");
			int result2 = 0;
			if (!int.TryParse(text, out result2))
			{
				result2 = GameUtils.TranslateCardIdToDbId(text);
				if (result2 == 0)
				{
					UIStatus.Get().AddError($"Invalid tagValue: \"{text}\". Must be an integer or valid card Id.");
					result = false;
					break;
				}
				text = result2.ToString();
			}
			if (result2 > 999999)
			{
				UIStatus.Get().AddError($"Invalid card ID: \"{result2}\". Must be < {999999}.");
				result = false;
				break;
			}
			if (result2 <= 0)
			{
				UIStatus.Get().AddError($"Invalid card ID: \"{result2}\". Must be > 0.");
				result = false;
				break;
			}
			list.Add(text);
		}
		output = list.ToArray();
		return result;
	}

	private bool TryParseNamedArgs(string[] args, out Map<string, NamedParam> values)
	{
		values = new Map<string, NamedParam>();
		for (int i = 0; i < args.Length; i++)
		{
			string[] array = args[i].Trim().Split('=');
			if (array.Length > 1)
			{
				values.Add(array[0], new NamedParam(array[1]));
			}
		}
		return values.Count > 0;
	}

	private bool OnProcessCheat_resettips(string func, string[] args, string rawArgs)
	{
		Options.Get().SetBool(Option.HAS_SEEN_COLLECTIONMANAGER, val: false);
		return true;
	}

	private bool OnProcessCheat_brode(string func, string[] args, string rawArgs)
	{
		NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.ALL, new Vector3(133.1f, NotificationManager.DEPTH, 54.2f), GameStrings.Get("VO_INNKEEPER_FORGE_1WIN"), "VO_INNKEEPER_ARENA_1WIN.prefab:31bb13e800c74c0439ee1a7bfc1e3499");
		return true;
	}

	private bool On_ProcessCheat_bug(string func, string[] args, string rawArgs)
	{
		return true;
	}

	private bool On_ProcessCheat_ANR(string func, string[] args, string rawArgs)
	{
		if (!ExceptionReporter.Get().IsEnabledANRMonitor)
		{
			UIStatus.Get().AddInfo("ANR Monitor of ExceptionReporter is disabled");
			return true;
		}
		try
		{
			m_waitTime = float.Parse(args[0]);
		}
		catch
		{
		}
		m_showedMessage = false;
		Processor.RegisterUpdateDelegate(SimulatorPauseUpdate);
		return true;
	}

	private void SimulatorPauseUpdate()
	{
		UIStatus.Get().AddInfo("Wait for " + m_waitTime + " seconds");
		if (m_showedMessage)
		{
			Thread.Sleep((int)(m_waitTime * 1000f));
			Processor.UnregisterUpdateDelegate(SimulatorPauseUpdate);
		}
		m_showedMessage = true;
	}

	private bool OnProcessCheat_igm(string func, string[] args, string rawArgs)
	{
		return true;
	}

	private bool OnProcessCheat_msgui(string func, string[] args, string rawArgs)
	{
		string value = "show";
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
		{
			value = args[0];
		}
		if ("add".StartsWith(value))
		{
			AddMessagePopupForArgs(args);
		}
		else if ("help".StartsWith(value))
		{
			UIStatus.Get().AddInfo($"USAGE: msgui [add] [text|shop] [imageType|pid]");
		}
		return true;
	}

	private void AddMessagePopupForArgs(string[] args)
	{
		MessageUIData messageUIData = ConstructUIDataFromArgs(args);
		if (messageUIData == null)
		{
			Log.InGameMessage.PrintDebug("Failed to construct UI Data for test IGM");
			return;
		}
		MessagePopupDisplay messagePopupDisplay = HearthstoneServices.Get<MessagePopupDisplay>();
		if (messagePopupDisplay == null)
		{
			UIStatus.Get().AddError("Message Popup Display was not available to show a message");
		}
		else
		{
			messagePopupDisplay.QueueMessage(messageUIData);
		}
	}

	private static MessageUIData ConstructUIDataFromArgs(string[] args)
	{
		MessageContentType contentTypeIfAvailable = GetContentTypeIfAvailable(args);
		if (contentTypeIfAvailable == MessageContentType.INVALID)
		{
			return null;
		}
		MessageUIData messageUIData = new MessageUIData
		{
			ContentType = contentTypeIfAvailable,
			MessageData = ConstructContentDataForMessage(contentTypeIfAvailable, args)
		};
		if (messageUIData.MessageData == null)
		{
			return null;
		}
		return messageUIData;
	}

	private static object ConstructContentDataForMessage(MessageContentType contentType, string[] args)
	{
		switch (contentType)
		{
		case MessageContentType.TEXT:
			return ConstructTestTextMsg(args);
		case MessageContentType.SHOP:
			return ConstructTestShopMsg(args);
		default:
			UIStatus.Get().AddError($"Unsupported content type {contentType}");
			return null;
		}
	}

	private static TextMessageContent ConstructTestTextMsg(string[] args)
	{
		string imageType = "Logo";
		if (args.Length > 2 && !string.IsNullOrEmpty(args[2]))
		{
			imageType = args[2];
		}
		return new TextMessageContent
		{
			ImageType = imageType,
			Title = "Lorem Ipsum",
			TextBody = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ut rhoncus ante. Donec in pretium felis. Duis mollis purus a ante mollis luctus. Nulla hendrerit gravida nulla non convallis. Vivamus vel ligula a mi porta porta et at magna. Nulla euismod diam eget arcu pharetra scelerisque. In id sem a ipsum maximus cursus. In pulvinar fermentum dolor, at ultrices ipsum congue nec."
		};
	}

	private static ShopMessageContent ConstructTestShopMsg(string[] args)
	{
		long result = 10747L;
		if (args.Length > 2 && !string.IsNullOrEmpty(args[2]) && !long.TryParse(args[2], out result))
		{
			UIStatus.Get().AddError("Invalid product id for show igm: " + args[2]);
			return null;
		}
		return new ShopMessageContent
		{
			Title = "Lorem Ipsum",
			TextBody = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ut rhoncus ante. Donec in pretium felis. Duis mollis purus a ante mollis luctus. Nulla hendrerit gravida nulla non convallis. Vivamus vel ligula a mi porta porta et at magna. Nulla euismod diam eget arcu pharetra scelerisque. In id sem a ipsum maximus cursus. In pulvinar fermentum dolor, at ultrices ipsum congue nec.",
			ProductID = result
		};
	}

	private static MessageContentType GetContentTypeIfAvailable(string[] args)
	{
		MessageContentType result = MessageContentType.TEXT;
		if (args.Length > 1 && !string.IsNullOrEmpty(args[1]))
		{
			string text = args[1].ToLower();
			if (!(text == "text"))
			{
				if (text == "shop")
				{
					result = MessageContentType.SHOP;
				}
				else
				{
					result = MessageContentType.INVALID;
					UIStatus.Get().AddError("Invalid message type to show " + text);
				}
			}
			else
			{
				result = MessageContentType.TEXT;
			}
		}
		return result;
	}

	private bool On_ProcessCheat_crash(string func, string[] args, string rawArgs)
	{
		string[] value = new string[5] { "help", "cs", "plugin", "nativeinlib", "javainlib" };
		if (args.Length < 1 || string.IsNullOrEmpty(rawArgs))
		{
			throw new Exception("User requested exception");
		}
		string text = args[0].ToLower();
		string text2 = args.Skip(1).ToString();
		if (string.IsNullOrEmpty(text2))
		{
			text2 = "User requested exception";
		}
		if ("plugin".StartsWith(text))
		{
			if (PlatformSettings.IsMobileRuntimeOS)
			{
				MobileCallbackManager.CreateCrashPlugInLayer(text2);
			}
			else
			{
				UIStatus.Get().AddInfo("Plug-in crash is only for Android platform");
			}
		}
		else if ("javainlib".StartsWith(text))
		{
			if (PlatformSettings.RuntimeOS == OSCategory.Android)
			{
				MobileCallbackManager.CreateCrashInNativeLayer("java:" + text2);
			}
			else
			{
				UIStatus.Get().AddInfo("Java crash is only for Android platforms");
			}
		}
		else if ("nativeinlib".StartsWith(text))
		{
			if (PlatformSettings.IsMobileRuntimeOS)
			{
				MobileCallbackManager.CreateCrashInNativeLayer(text2);
			}
			else
			{
				UIStatus.Get().AddInfo("Native crash is only for mobile platforms");
			}
		}
		else
		{
			if ("cs".StartsWith(text))
			{
				throw new Exception(text2);
			}
			if (text == "help")
			{
				UIStatus.Get().AddInfo(string.Format("USAGE: crash [where] [exception title]\nWhere(substring): {0}", string.Join(" | ", value)));
			}
		}
		return true;
	}

	private bool OnProcessCheat_questcompletepopup(string func, string[] args, string rawArgs)
	{
		int result = 0;
		Achievement achievement = (int.TryParse(rawArgs, out result) ? AchieveManager.Get().GetAchievement(result) : null);
		if (achievement == null)
		{
			UIStatus.Get().AddError($"{func}: please specify a valid Quest ID");
			return true;
		}
		QuestToast.ShowQuestToast(UserAttentionBlocker.ALL, null, updateCacheValues: false, achievement);
		return true;
	}

	private bool OnProcessCheat_narrative(string func, string[] args, string rawArgs)
	{
		if (args.Length == 1 && args[0] == "clear")
		{
			List<Option> source = NarrativeManager.Get().Cheat_ClearAllSeen();
			string message = string.Format("Narrative seen options cleared:\n{0}", string.Join(", ", source.Select((Option o) => EnumUtils.GetString(o)).ToArray()));
			UIStatus.Get().AddInfo(message);
			return true;
		}
		int result = 0;
		if ((int.TryParse(rawArgs, out result) ? AchieveManager.Get().GetAchievement(result) : null) == null)
		{
			UIStatus.Get().AddError($"{func}: please specify a valid Quest ID");
			return true;
		}
		NarrativeManager.Get().OnQuestCompleteShown(result);
		NarrativeManager.Get().ShowOutstandingQuestDialogs();
		return true;
	}

	private bool OnProcessCheat_narrativedialog(string func, string[] args, string rawArgs)
	{
		int result = 0;
		CharacterDialogSequence characterDialogSequence = (int.TryParse(rawArgs, out result) ? new CharacterDialogSequence(result) : null);
		if (characterDialogSequence == null)
		{
			UIStatus.Get().AddError($"{func}: please specify a valid Dialog ID");
			return true;
		}
		NarrativeManager.Get().PushDialogSequence(characterDialogSequence);
		return true;
	}

	private bool OnProcessCheat_questwelcome(string func, string[] args, string rawArgs)
	{
		bool boolVal = true;
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
		{
			GeneralUtils.TryParseBool(args[0], out boolVal);
		}
		WelcomeQuests.Show(UserAttentionBlocker.ALL, boolVal);
		return true;
	}

	private bool OnProcessCheat_newquestvisual(string func, string[] args, string rawArgs)
	{
		if (WelcomeQuests.Get() == null)
		{
			UIStatus.Get().AddError("WelcomeQuests object is not active - try using 'questwelcome' cheat first.");
			return true;
		}
		int result = 0;
		Achievement achievement = (int.TryParse(rawArgs, out result) ? AchieveManager.Get().GetAchievement(result) : null);
		if (achievement == null)
		{
			UIStatus.Get().AddError($"{func}: please specify a valid Quest ID");
			return true;
		}
		WelcomeQuests.Get().GetFirstQuestTile().SetupTile(achievement, QuestTile.FsmEvent.QuestGranted);
		return true;
	}

	private bool OnProcessCheat_questprogresspopup(string func, string[] args, string rawArgs)
	{
		int result = 0;
		Achievement achievement = ((args.Length != 0 && int.TryParse(args[0], out result)) ? AchieveManager.Get().GetAchievement(result) : null);
		int result2 = 1;
		string questName;
		string questDescription;
		int result3;
		int result4;
		if (achievement == null)
		{
			if (result != 0)
			{
				UIStatus.Get().AddError("unknown Achieve with ID " + result);
				return true;
			}
			if (args.Length < 4)
			{
				UIStatus.Get().AddError("please specify an Achieve ID or the following params:\n<title> <description> <progress> <maxprogress>");
				return true;
			}
			questName = args[0];
			questDescription = args[1];
			int.TryParse(args[2], out result3);
			int.TryParse(args[3], out result4);
		}
		else
		{
			questName = achievement.Name;
			questDescription = achievement.Description;
			result3 = achievement.Progress;
			result4 = achievement.MaxProgress;
		}
		for (int i = 0; i < args.Length; i++)
		{
			string[] array = args[i].Split('=');
			if (array.Length >= 2)
			{
				string text = array[0];
				string text2 = array[1];
				if (text == "count" && !int.TryParse(text2, out result2))
				{
					UIStatus.Get().AddError($"Unable to parse parameter #{i + 1} as integer: {text2}");
					return true;
				}
			}
		}
		if (GameToastMgr.Get() != null)
		{
			if (result3 >= result4)
			{
				result3 = result4 - 1;
			}
			for (int j = 0; j < result2; j++)
			{
				GameToastMgr.Get().AddQuestProgressToast(result, questName, questDescription, result3, result4);
			}
			return true;
		}
		UIStatus.Get().AddError("GameToastMgr is null!");
		return true;
	}

	private bool OnProcessCheat_retire(string func, string[] args, string rawArgs)
	{
		if (DemoMgr.Get().GetMode() != DemoMode.BLIZZCON_2013)
		{
			return false;
		}
		DraftManager draftManager = DraftManager.Get();
		if (draftManager == null)
		{
			return false;
		}
		Network.Get().DraftRetire(draftManager.GetDraftDeck().ID, draftManager.GetSlot(), draftManager.CurrentSeasonId);
		return true;
	}

	private bool OnProcessCheat_storepassword(string func, string[] args, string rawArgs)
	{
		if (m_loadingStoreChallengePrompt)
		{
			return true;
		}
		if (m_storeChallengePrompt == null)
		{
			m_loadingStoreChallengePrompt = true;
			PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
			{
				Processor.RunCoroutine(StorePasswordCoroutine(assetRef, go, callbackData));
			};
			AssetLoader.Get().InstantiatePrefab("StoreChallengePrompt.prefab:43f02a51d311c214aa25232228ccefef", callback);
		}
		else if (m_storeChallengePrompt.IsShown())
		{
			m_storeChallengePrompt.Hide();
		}
		else
		{
			Processor.RunCoroutine(StorePasswordCoroutine(m_storeChallengePrompt.name, m_storeChallengePrompt.gameObject, null));
		}
		return true;
	}

	private bool OnProcessCheat_notice(string func, string[] args, string rawArgs)
	{
		if (args.Count() < 2)
		{
			UIStatus.Get().AddError("notice cheat requires 2 params: [string]type [int]data [OPTIONAL int]data2 [OPTIONAL bool]quest toast?");
			return true;
		}
		int result = -1;
		int.TryParse(args[1], out result);
		if (result < 0)
		{
			UIStatus.Get().AddError($"{result}: please specify a valid Notice Data Value");
			return true;
		}
		string text = null;
		if (args.Length > 2)
		{
			text = args[2];
		}
		bool flag = false;
		if (args.Length > 3)
		{
			flag = GeneralUtils.ForceBool(args[3]);
		}
		NetCache.ProfileNotice notice = null;
		Achievement achievement = new Achievement();
		List<RewardData> list = new List<RewardData>();
		switch (args[0])
		{
		case "gold":
			if (flag)
			{
				GoldRewardData goldRewardData = new GoldRewardData();
				goldRewardData.Amount = result;
				list.Add(goldRewardData);
			}
			else
			{
				notice = new NetCache.ProfileNoticeRewardCurrency
				{
					CurrencyType = PegasusShared.CurrencyType.CURRENCY_TYPE_GOLD,
					Amount = result
				};
			}
			break;
		case "arcane_orbs":
			if (flag)
			{
				list.Add(RewardUtils.CreateArcaneOrbRewardData(result));
				break;
			}
			notice = new NetCache.ProfileNoticeRewardCurrency
			{
				CurrencyType = PegasusShared.CurrencyType.CURRENCY_TYPE_ARCANE_ORBS,
				Amount = result
			};
			break;
		case "dust":
			if (flag)
			{
				ArcaneDustRewardData arcaneDustRewardData = new ArcaneDustRewardData();
				arcaneDustRewardData.Amount = result;
				list.Add(arcaneDustRewardData);
			}
			else
			{
				notice = new NetCache.ProfileNoticeRewardDust
				{
					Amount = result
				};
			}
			break;
		case "booster":
		{
			int result2 = 1;
			if (!string.IsNullOrEmpty(text))
			{
				int.TryParse(text, out result2);
			}
			if (GameDbf.Booster.GetRecord(result2) == null)
			{
				UIStatus.Get().AddError($"Booster ID is invalid: {result2}");
				return true;
			}
			if (flag)
			{
				BoosterPackRewardData boosterPackRewardData = new BoosterPackRewardData();
				boosterPackRewardData.Id = result2;
				boosterPackRewardData.Count = result;
				list.Add(boosterPackRewardData);
			}
			else
			{
				notice = new NetCache.ProfileNoticeRewardBooster
				{
					Count = result,
					Id = result2
				};
			}
			break;
		}
		case "card":
		{
			string text2 = "NEW1_040";
			if (!string.IsNullOrEmpty(text))
			{
				int result3 = -1;
				int.TryParse(text, out result3);
				text2 = ((result3 <= 0) ? text : GameUtils.TranslateDbIdToCardId(result3));
			}
			if (GameUtils.GetCardRecord(text2) == null)
			{
				UIStatus.Get().AddError($"Card ID is invalid: {text2}");
				return true;
			}
			if (flag)
			{
				CardRewardData cardRewardData = new CardRewardData();
				cardRewardData.CardID = text2;
				cardRewardData.Count = Mathf.Clamp(result, 1, 2);
				list.Add(cardRewardData);
			}
			else
			{
				notice = new NetCache.ProfileNoticeRewardCard
				{
					CardID = text2,
					Quantity = Mathf.Clamp(result, 1, 2)
				};
			}
			break;
		}
		case "cardback":
			if (GameDbf.CardBack.GetRecord(result) == null)
			{
				UIStatus.Get().AddError($"Cardback ID is invalid: {result}");
				return true;
			}
			if (flag)
			{
				CardBackRewardData cardBackRewardData = new CardBackRewardData();
				cardBackRewardData.CardBackID = result;
				list.Add(cardBackRewardData);
			}
			else
			{
				notice = new NetCache.ProfileNoticeRewardCardBack
				{
					CardBackID = result
				};
			}
			break;
		case "tavern_brawl_rewards":
		{
			NetCache.ProfileNoticeTavernBrawlRewards profileNoticeTavernBrawlRewards = new NetCache.ProfileNoticeTavernBrawlRewards();
			profileNoticeTavernBrawlRewards.Wins = result;
			profileNoticeTavernBrawlRewards.Chest = RewardUtils.GenerateTavernBrawlRewardChest_CHEAT(mode: profileNoticeTavernBrawlRewards.Mode = (text.Equals("heroic") ? TavernBrawlMode.TB_MODE_HEROIC : TavernBrawlMode.TB_MODE_NORMAL), wins: result);
			notice = profileNoticeTavernBrawlRewards;
			break;
		}
		case "event":
		{
			flag = true;
			EventRewardData eventRewardData = new EventRewardData();
			eventRewardData.EventType = result;
			list.Add(eventRewardData);
			break;
		}
		case "license":
		{
			flag = false;
			NetCache.NetCacheAccountLicenses netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
			NetCache.ProfileNoticeAcccountLicense profileNoticeAcccountLicense = new NetCache.ProfileNoticeAcccountLicense();
			profileNoticeAcccountLicense.License = result;
			profileNoticeAcccountLicense.Origin = NetCache.ProfileNotice.NoticeOrigin.ACCOUNT_LICENSE_FLAGS;
			profileNoticeAcccountLicense.OriginData = 1L;
			if (netObject.AccountLicenses.ContainsKey(profileNoticeAcccountLicense.License))
			{
				profileNoticeAcccountLicense.CasID = netObject.AccountLicenses[profileNoticeAcccountLicense.License].CasId + 1;
			}
			notice = profileNoticeAcccountLicense;
			break;
		}
		default:
			UIStatus.Get().AddError($"{args[0]}: please specify a valid Notice Type.\nValid Types are: 'gold','arcane_orbs','dust','booster','card','cardback','tavern_brawl_rewards','event','license'");
			return true;
		}
		if (flag)
		{
			achievement.SetDescription("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "");
			achievement.SetName("Title Text", "");
			QuestToast.ShowQuestToast(UserAttentionBlocker.ALL, null, updateCacheValues: false, achievement);
		}
		else
		{
			NetCache.Get().Cheat_AddNotice(notice);
		}
		return true;
	}

	private bool OnProcessCheat_LoadWidget(string func, string[] args, string rawArgs)
	{
		string text = args[0];
		if (string.IsNullOrEmpty(text))
		{
			UIStatus.Get().AddError("First parameter must be the GUID of a valid widget template.");
			return false;
		}
		WidgetInstance widgetInstance = WidgetInstance.Create(text);
		if (widgetInstance == null)
		{
			UIStatus.Get().AddError("First parameter must be the GUID of a valid widget template.");
			return false;
		}
		s_createdWidgets.Add(widgetInstance);
		widgetInstance.TriggerEvent("CHEATED_STATE");
		return true;
	}

	private bool OnProcessCheat_ClearWidgets(string func, string[] args, string rawArgs)
	{
		foreach (WidgetInstance s_createdWidget in s_createdWidgets)
		{
			UnityEngine.Object.Destroy(s_createdWidget.gameObject);
		}
		s_createdWidgets.Clear();
		return true;
	}

	private bool OnProcessCheat_ServerLog(string func, string[] args, string rawArgs)
	{
		ScriptLogMessage scriptLogMessage = new ScriptLogMessage();
		scriptLogMessage.Message = rawArgs;
		scriptLogMessage.Event = "Cheat";
		scriptLogMessage.Severity = 1;
		SceneDebugger.Get().AddServerScriptLogMessage(scriptLogMessage);
		return true;
	}

	private bool OnProcessCheat_dialogEvent(string func, string[] args, string rawArgs)
	{
		if (args.Length != 1)
		{
			UIStatus.Get().AddError("Provide 1 param for " + func + ".");
			return true;
		}
		NarrativeManager narrativeManager = NarrativeManager.Get();
		if (narrativeManager == null)
		{
			return false;
		}
		if (args[0] == "reset")
		{
			UIStatus.Get().AddInfo("All ScheduledCharacterDialogEvent's have been reset.");
			narrativeManager.ResetScheduledCharacterDialogEvent_Debug();
			return true;
		}
		ScheduledCharacterDialogEvent result = ScheduledCharacterDialogEvent.INVALID;
		Enum.TryParse<ScheduledCharacterDialogEvent>(args[0], ignoreCase: true, out result);
		if (!Enum.IsDefined(typeof(ScheduledCharacterDialogEvent), result) || result == ScheduledCharacterDialogEvent.INVALID)
		{
			Array names = Enum.GetNames(typeof(ScheduledCharacterDialogEvent));
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("reset -- this allows events to run again");
			stringBuilder.Append('\n');
			int num = 0;
			foreach (string item in names)
			{
				if (num != 0)
				{
					stringBuilder.Append(num);
					stringBuilder.Append(" = ");
					stringBuilder.Append(item);
					stringBuilder.Append('\n');
				}
				num++;
			}
			string text = stringBuilder.ToString();
			UIStatus.Get().AddError("Invalid param for " + func + ". See \"Messages\".");
			Log.Gameplay.PrintError("Unrecognized <event_type>.\n" + $"Try a num [1-{names.Length - 1}] or a string:\n" + text);
			return true;
		}
		narrativeManager.TriggerScheduledCharacterDialogEvent_Debug(result);
		return true;
	}

	private bool OnProcessCheat_account(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = "add, remove, set, skip, unlock";
		if (autofillData != null)
		{
			if ((rawArgs.EndsWith(" ") && args.Length == 0) || args.Length == 1)
			{
				string[] values = text.Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
				string searchTerm = ((args.Length == 0) ? string.Empty : args[0]);
				return ProcessAutofillParam(values, searchTerm, autofillData);
			}
			return false;
		}
		string message = "account cheat requires one of the following valid sub-commands: " + text;
		if (args.Length == 0)
		{
			UIStatus.Get().AddError(message);
			return true;
		}
		string text2 = args[0].ToLower();
		string[] args2 = args.Skip(1).ToArray();
		switch (text2)
		{
		case "add":
			HttpCheater.Get().RunAddResourceCommand(args2);
			break;
		case "remove":
			HttpCheater.Get().RunRemoveResourceCommand(args2);
			break;
		case "set":
			HttpCheater.Get().RunSetResourceCommand(args2);
			break;
		case "skip":
			HttpCheater.Get().RunSkipResourceCommand(args2);
			break;
		case "unlock":
			HttpCheater.Get().RunUnlockResourceCommand(args2);
			break;
		default:
			UIStatus.Get().AddError(message);
			break;
		}
		return true;
	}

	private bool OnProcessCheat_SkipSendingGetGameState(string func, string[] args, string rawArgs)
	{
		int result = 0;
		if (args.Length != 0 && int.TryParse(args[0], out result))
		{
			m_skipSendingGetGameState = result != 0;
			return true;
		}
		return false;
	}

	private bool OnProcessCheat_SendGetGameState(string func, string[] args, string rawArgs)
	{
		if (m_skipSendingGetGameState)
		{
			Network.Get().GetGameState();
			return true;
		}
		return false;
	}

	private string GetChallengeUrl(string type)
	{
		string text = $"https://login-qa-us.web.blizzard.net/login/admin/challenge/create/ct_{type.ToLower()}";
		string text2 = "joe_balance@zmail.blizzard.com";
		string text3 = "wtcg";
		string text4 = "*";
		string text5 = "none";
		string text6 = "";
		bool flag = false;
		bool flag2 = false;
		string text7 = "";
		string text8 = "";
		return $"{text}?email={text2}&programId={text3}&platformId={text4}&redirectUrl={text5}&messageKey={text6}&notifyRisk={flag}&chooseChallenge={flag2}&challengeType={text7}&riskTransId={text8}";
	}

	private IEnumerator StorePasswordCoroutine(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_loadingStoreChallengePrompt = false;
		m_storeChallengePrompt = go.GetComponent<StoreChallengePrompt>();
		m_storeChallengePrompt.Hide();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["Accept"] = "application/json;charset=UTF-8";
		dictionary["Accept-Language"] = Localization.GetBnetLocaleName();
		string challengeUrl = GetChallengeUrl("cvv");
		Debug.Log("creating challenge with url " + challengeUrl);
		IHttpRequest createChallenge = HttpRequestFactory.Get().CreateGetRequest(challengeUrl);
		createChallenge.SetRequestHeaders(dictionary);
		yield return createChallenge.SendRequest();
		Debug.Log("challenge response is " + createChallenge.ResponseAsString);
		string text = (string)(Json.Deserialize(createChallenge.ResponseAsString) as JsonNode)["challenge_url"];
		Debug.Log("challenge url is " + text);
		yield return m_storeChallengePrompt.StartCoroutine(m_storeChallengePrompt.Show(text));
	}

	private bool OnProcessCheat_favoritecardback(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0)
		{
			return false;
		}
		if (!int.TryParse(args[0].ToLowerInvariant(), out var result))
		{
			return false;
		}
		Network.Get().SetFavoriteCardBack(result);
		return true;
	}

	private bool OnProcessCheat_disconnect(string func, string[] args, string rawArgs)
	{
		if (args != null && args.Length >= 1 && args[0] == "bnet")
		{
			if (Network.BattleNetStatus() != Network.BnetLoginState.BATTLE_NET_LOGGED_IN)
			{
				UIStatus.Get().AddError("Not connected to Battle.net, status=" + Network.BattleNetStatus());
				return true;
			}
			BattleNet.RequestCloseAurora();
			UIStatus.Get().AddInfo("Disconnecting from Battle.net.");
			return true;
		}
		if (!Network.Get().IsConnectedToGameServer())
		{
			UIStatus.Get().AddError("Not connected to game server.");
			return true;
		}
		if (args != null && args.Length >= 1 && args[0] == "pong")
		{
			UIStatus.Get().AddInfo("Pong responses now being ignored.");
			Network.Get().SetShouldIgnorePong(value: true);
			return true;
		}
		bool num = args != null && args.Length >= 1 && args[0] == "internet";
		NetworkReachabilityManager networkReachabilityManager = HearthstoneServices.Get<NetworkReachabilityManager>();
		if (num)
		{
			networkReachabilityManager?.SetForceUnreachable(!networkReachabilityManager.GetForceUnreachable());
			UIStatus.Get().AddInfo(networkReachabilityManager.GetForceUnreachable() ? "Forcing unreachable network." : "Network reachable.");
			return true;
		}
		if (args != null && args.Length >= 2 && args[0] == "duration")
		{
			int num2 = int.Parse(args[1]);
			networkReachabilityManager?.SetForceUnreachable(value: true);
			Network.Get().SetSpoofDisconnected(value: true);
			Network.Get().OverrideKeepAliveSeconds(5u);
			UIStatus.Get().AddInfo($"All network disconnected for {num2} seconds");
			HearthstoneApplication.Get().StartCoroutine(EnableNetworkAfterDelay(num2));
			return true;
		}
		bool num3 = args == null || args.Length == 0 || args[0] != "force";
		Log.LoadingScreen.Print("Cheats.OnProcessCheat_disconnect() - reconnect=true");
		if (num3)
		{
			Network.Get().DisconnectFromGameServer();
		}
		else
		{
			Network.Get().SimulateUncleanDisconnectFromGameServer();
		}
		return true;
	}

	private IEnumerator EnableNetworkAfterDelay(int delay)
	{
		yield return new WaitForSeconds(delay);
		HearthstoneServices.Get<NetworkReachabilityManager>()?.SetForceUnreachable(value: false);
		Network.Get().SetSpoofDisconnected(value: false);
		Network.Get().OverrideKeepAliveSeconds(0u);
	}

	private bool OnProcessCheat_restart(string func, string[] args, string rawArgs)
	{
		if (!Network.Get().IsConnectedToGameServer())
		{
			UIStatus.Get().AddError("Not connected to game server.");
			return true;
		}
		if (!GameUtils.CanRestartCurrentMission(checkTutorial: false))
		{
			UIStatus.Get().AddError("This game cannot be restarted.");
			return true;
		}
		GameState.Get().Restart();
		return true;
	}

	private bool OnProcessCheat_warning(string func, string[] args, string rawArgs)
	{
		ParseErrorText(args, rawArgs, out var header, out var message);
		Error.AddWarning(header, message);
		return true;
	}

	private bool OnProcessCheat_fatal(string func, string[] args, string rawArgs)
	{
		Error.AddFatal(FatalErrorReason.CHEAT, rawArgs);
		return true;
	}

	private bool OnProcessCheat_exit(string func, string[] args, string rawArgs)
	{
		GeneralUtils.ExitApplication();
		return true;
	}

	private bool OnProcessCheat_log(string func, string[] args, string rawArgs)
	{
		string message = "unknown log command, please use 'log help'";
		float delay = 5f;
		string text = args[0].ToLowerInvariant();
		string text2 = ((args.Length >= 2) ? args[1] : string.Empty);
		switch (text)
		{
		case "help":
			message = "available log commands: load reload line";
			switch (text2)
			{
			case "load":
			case "reload":
				message = "reloads the log.config";
				break;
			case "line":
				message = "prints a simple long line to log, useful for debugging\nto visually differentiate between test results.\nyou can specify a parameter like\n'log warn' to call Debug.LogWarning. you can\nalso add a note/context to your line\nby adding words afterwards, like 'log test 2 start'\nor 'log error (test 3 starting)'.";
				delay = 10f;
				break;
			}
			break;
		case "load":
		case "reload":
			Log.Get().Load();
			break;
		case "line":
		{
			LogFormatFunc logFormatFunc = Debug.LogFormat;
			string empty = string.Empty;
			int num = 1;
			switch (text2)
			{
			case "warn":
			case "warning":
				logFormatFunc = Debug.LogWarningFormat;
				num++;
				break;
			case "err":
			case "error":
				logFormatFunc = Debug.LogErrorFormat;
				num++;
				break;
			}
			empty = string.Join(" ", args.Skip(num).ToArray());
			if (empty.Length > 0)
			{
				empty = $" {empty} ";
			}
			logFormatFunc("====={0}{1}", empty, new string('=', Mathf.Max(5, 75 - empty.Length)));
			message = "printed line to " + logFormatFunc.Method.Name;
			delay = 2f;
			break;
		}
		}
		UIStatus.Get().AddInfo(message, delay);
		return true;
	}

	private bool OnProcessCheat_alert(string func, string[] args, string rawArgs)
	{
		AlertPopup.PopupInfo info = GenerateAlertInfo(rawArgs);
		if (m_alert == null)
		{
			DialogManager.Get().ShowPopup(info, OnAlertProcessed);
		}
		else
		{
			m_alert.UpdateInfo(info);
		}
		return true;
	}

	private bool OnProcessCheat_rankedIntroPopup(string func, string[] args, string rawArgs)
	{
		DialogManager.Get().ShowRankedIntroPopUp(null);
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		DialogManager.Get().ShowBonusStarsPopup(localPlayerMedalInfo.CreateDataModel(FormatType.FT_STANDARD, RankedMedal.DisplayMode.Default), null);
		return true;
	}

	private bool OnProcessCheat_setRotationRotatedBoostersPopup(string func, string[] args, string rawArgs)
	{
		SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo info = new SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo();
		DialogManager.Get().ShowSetRotationTutorialPopup(UserAttentionBlocker.SET_ROTATION_INTRO, info);
		return true;
	}

	private bool OnProcessCheat_seasondialog(string func, string[] args, string rawArgs)
	{
		string cheatName = "bronze10";
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
		{
			cheatName = args[0];
		}
		LeagueRankDbfRecord leagueRankRecordByCheatName = RankMgr.Get().GetLeagueRankRecordByCheatName(cheatName);
		if (leagueRankRecordByCheatName == null)
		{
			return false;
		}
		FormatType formatType = FormatType.FT_STANDARD;
		if (args.Length >= 2)
		{
			switch (args[1].ToLower())
			{
			case "1":
			case "wild":
				formatType = FormatType.FT_WILD;
				break;
			case "2":
			case "standard":
				formatType = FormatType.FT_STANDARD;
				break;
			case "3":
			case "classic":
				formatType = FormatType.FT_CLASSIC;
				break;
			default:
				UIStatus.Get().AddInfo("please enter a valid value for 2nd parameter <format type>");
				return true;
			}
		}
		SeasonEndDialog.SeasonEndInfo seasonEndInfo = new SeasonEndDialog.SeasonEndInfo();
		seasonEndInfo.m_leagueId = leagueRankRecordByCheatName.LeagueId;
		seasonEndInfo.m_starLevelAtEndOfSeason = leagueRankRecordByCheatName.StarLevel;
		seasonEndInfo.m_bestStarLevelAtEndOfSeason = leagueRankRecordByCheatName.StarLevel;
		seasonEndInfo.m_formatType = formatType;
		MedalInfoTranslator medalInfoTranslator = MedalInfoTranslator.CreateMedalInfoForLeagueId(leagueRankRecordByCheatName.LeagueId, leagueRankRecordByCheatName.StarLevel, 0);
		medalInfoTranslator.GetPreviousMedal(formatType).starLevel = 1;
		medalInfoTranslator.GetCurrentMedal(formatType).bestStarLevel = leagueRankRecordByCheatName.StarLevel;
		seasonEndInfo.m_rankedRewards = new List<RewardData>();
		List<List<RewardData>> rewardsEarned = new List<List<RewardData>>();
		if (!medalInfoTranslator.GetRankedRewardsEarned(formatType, ref rewardsEarned))
		{
			return false;
		}
		foreach (List<RewardData> item in rewardsEarned)
		{
			seasonEndInfo.m_rankedRewards.AddRange(item);
		}
		for (int i = 0; i < seasonEndInfo.m_rankedRewards.Count; i++)
		{
			RandomCardRewardData randomCardRewardData = seasonEndInfo.m_rankedRewards[i] as RandomCardRewardData;
			if (randomCardRewardData != null)
			{
				string cardID = "GAME_005";
				switch (randomCardRewardData.Rarity)
				{
				case TAG_RARITY.COMMON:
					cardID = "EX1_096";
					break;
				case TAG_RARITY.RARE:
					cardID = "EX1_274";
					break;
				case TAG_RARITY.EPIC:
					cardID = "EX1_586";
					break;
				case TAG_RARITY.LEGENDARY:
					cardID = "EX1_562";
					break;
				}
				seasonEndInfo.m_rankedRewards[i] = new CardRewardData(cardID, randomCardRewardData.Premium, 1);
			}
		}
		NetCache.NetCacheRewardProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>();
		if (netObject != null)
		{
			seasonEndInfo.m_seasonID = netObject.Season;
		}
		DialogManager.DialogRequest dialogRequest = new DialogManager.DialogRequest();
		dialogRequest.m_type = DialogManager.DialogType.SEASON_END;
		dialogRequest.m_info = seasonEndInfo;
		dialogRequest.m_isFake = true;
		DialogManager.Get().AddToQueue(dialogRequest);
		return true;
	}

	private bool OnProcessCheat_playnullsound(string func, string[] args, string rawArgs)
	{
		SoundManager.Get().Play(null);
		return true;
	}

	private bool OnProcessCheat_playaudio(string func, string[] args, string rawArgs)
	{
		if (Cheats.PlayAudioByName != null)
		{
			Cheats.PlayAudioByName(args);
		}
		return true;
	}

	private bool OnProcessCheat_spectate(string func, string[] args, string rawArgs)
	{
		if (args.Length >= 1 && args[0] == "waiting")
		{
			SpectatorManager.Get().ShowWaitingForNextGameDialog();
			return true;
		}
		if (args.Length < 4 || args.Any((string a) => string.IsNullOrEmpty(a)))
		{
			Error.AddWarning("Spectate Cheat Error", "spectate cheat must have the following args:\n\nspectate ipaddress port game_handle spectator_password [gameType] [missionId]");
			return false;
		}
		JoinInfo joinInfo = new JoinInfo();
		joinInfo.ServerIpAddress = args[0];
		joinInfo.SecretKey = args[3];
		if (!uint.TryParse(args[1], out var result))
		{
			Error.AddWarning("Spectate Cheat Error", "error parsing the port # (uint) argument: " + args[1]);
			return false;
		}
		joinInfo.ServerPort = result;
		if (!int.TryParse(args[2], out var result2))
		{
			Error.AddWarning("Spectate Cheat Error", "error parsing the game_handle (int) argument: " + args[2]);
			return false;
		}
		joinInfo.GameHandle = result2;
		joinInfo.GameType = GameType.GT_UNKNOWN;
		joinInfo.MissionId = 2;
		if (args.Length >= 5 && int.TryParse(args[4], out result2))
		{
			joinInfo.GameType = (GameType)result2;
		}
		if (args.Length >= 6 && int.TryParse(args[5], out result2))
		{
			joinInfo.MissionId = result2;
		}
		GameMgr.Get().SpectateGame(joinInfo);
		return true;
	}

	private static void SubscribePartyEvents()
	{
		if (s_hasSubscribedToPartyEvents)
		{
			return;
		}
		BnetParty.OnError += delegate(PartyError error)
		{
			Log.Party.Print("{0} code={1} feature={2} party={3} str={4}", error.DebugContext, error.ErrorCode, error.FeatureEvent.ToString(), new PartyInfo(error.PartyId, error.PartyType), error.StringData);
		};
		BnetParty.OnJoined += delegate(OnlineEventType e, PartyInfo party, LeaveReason? reason)
		{
			Log.Party.Print("Party.OnJoined {0} party={1} reason={2}", e, party, reason.HasValue ? reason.Value.ToString() : "null");
		};
		BnetParty.OnPrivacyLevelChanged += delegate(PartyInfo party, PrivacyLevel privacy)
		{
			Log.Party.Print("Party.OnPrivacyLevelChanged party={0} privacy={1}", party, privacy);
		};
		BnetParty.OnMemberEvent += delegate(OnlineEventType e, PartyInfo party, BnetGameAccountId memberId, bool isRolesUpdate, LeaveReason? reason)
		{
			Log.Party.Print("Party.OnMemberEvent {0} party={1} memberId={2} isRolesUpdate={3} reason={4}", e, party, memberId, isRolesUpdate, reason.HasValue ? reason.Value.ToString() : "null");
		};
		BnetParty.OnReceivedInvite += delegate(OnlineEventType e, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, InviteRemoveReason? reason)
		{
			Log.Party.Print("Party.OnReceivedInvite {0} party={1} inviteId={2} reason={3}", e, party, inviteId, reason.HasValue ? reason.Value.ToString() : "null");
		};
		BnetParty.OnSentInvite += delegate(OnlineEventType e, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, bool senderIsMyself, InviteRemoveReason? reason)
		{
			PartyInvite sentInvite = BnetParty.GetSentInvite(party.Id, inviteId);
			Log.Party.Print("Party.OnSentInvite {0} party={1} inviteId={2} senderIsMyself={3} isRejoin={4} reason={5}", e, party, inviteId, senderIsMyself, (sentInvite == null) ? "null" : sentInvite.IsRejoin.ToString(), reason.HasValue ? reason.Value.ToString() : "null");
		};
		BnetParty.OnReceivedInviteRequest += delegate(OnlineEventType e, PartyInfo party, InviteRequest request, InviteRequestRemovedReason? reason)
		{
			Log.Party.Print("Party.OnReceivedInviteRequest {0} party={1} target={2} {3} requester={4} {5} reason={6}", e, party, request.TargetName, request.TargetId, request.RequesterName, request.RequesterId, reason.HasValue ? reason.Value.ToString() : "null");
		};
		BnetParty.OnChatMessage += delegate(PartyInfo party, BnetGameAccountId speakerId, string msg)
		{
			Log.Party.Print("Party.OnChatMessage party={0} speakerId={1} msg={2}", party, speakerId, msg);
		};
		BnetParty.OnPartyAttributeChanged += delegate(PartyInfo party, string key, Variant attrVal)
		{
			string text2 = "null";
			if (attrVal.HasIntValue)
			{
				text2 = "[long]" + attrVal.IntValue;
			}
			else if (attrVal.HasStringValue)
			{
				text2 = "[string]" + attrVal.StringValue;
			}
			else if (attrVal.HasBlobValue)
			{
				byte[] blobValue2 = attrVal.BlobValue;
				if (blobValue2 != null)
				{
					text2 = "blobLength=" + blobValue2.Length;
					try
					{
						string string2 = Encoding.UTF8.GetString(blobValue2);
						if (string2 != null)
						{
							text2 = text2 + " decodedUtf8=" + string2;
						}
					}
					catch (ArgumentException)
					{
					}
				}
			}
			Log.Party.Print("BnetParty.OnPartyAttributeChanged party={0} key={1} value={2}", party, key, text2);
		};
		BnetParty.OnMemberAttributeChanged += delegate(PartyInfo party, BnetGameAccountId partyMember, string key, Variant attrVal)
		{
			string text = "null";
			if (attrVal.HasIntValue)
			{
				text = "[long]" + attrVal.IntValue;
			}
			else if (attrVal.HasStringValue)
			{
				text = "[string]" + attrVal.StringValue;
			}
			else if (attrVal.HasBlobValue)
			{
				byte[] blobValue = attrVal.BlobValue;
				if (blobValue != null)
				{
					text = "blobLength=" + blobValue.Length;
					try
					{
						string @string = Encoding.UTF8.GetString(blobValue);
						if (@string != null)
						{
							text = text + " decodedUtf8=" + @string;
						}
					}
					catch (ArgumentException)
					{
					}
				}
			}
			Log.Party.Print("BnetParty.OnMemberAttributeChanged party={0} member={1} key={2} value={3}", party, partyMember, key, text);
		};
		s_hasSubscribedToPartyEvents = true;
	}

	private static PartyId ParsePartyId(string cmd, string arg, int argIndex, ref string errorMsg)
	{
		PartyId partyId = null;
		PartyType type;
		if (ulong.TryParse(arg, out var low))
		{
			PartyId[] joinedPartyIds = BnetParty.GetJoinedPartyIds();
			partyId = ((low < 0 || joinedPartyIds.Length == 0 || low >= (ulong)joinedPartyIds.LongLength) ? joinedPartyIds.FirstOrDefault((PartyId p) => p.Lo == low) : joinedPartyIds[low]);
			if (partyId == null)
			{
				errorMsg = "party " + cmd + ": couldn't find party at index, or with PartyId low bits: " + low;
			}
		}
		else if (!EnumUtils.TryGetEnum<PartyType>(arg, out type))
		{
			errorMsg = "party " + cmd + ": unable to parse party (index or LowBits or type)" + ((argIndex >= 0) ? (" at arg index=" + argIndex) : "") + " (" + arg + "), please specify the Low bits of a PartyId or a PartyType.";
		}
		else
		{
			partyId = (from info in BnetParty.GetJoinedParties()
				where info.Type == type
				select info.Id).FirstOrDefault();
			if (partyId == null)
			{
				errorMsg = "party " + cmd + ": no joined party with PartyType: " + arg;
			}
		}
		return partyId;
	}

	private bool OnProcessCheat_party(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1 || args.Any((string a) => string.IsNullOrEmpty(a)))
		{
			string message = "USAGE: party [cmd] [args]\nCommands: create | join | leave | dissolve | list | invite | accept | decline | revoke | requestinvite | ignorerequest | setleader | kick | chat | setprivacy | setlong | setstring | setblob | clearattr | subscribe | unsubscribe";
			Error.AddWarning("Party Cheat Error", message);
			return false;
		}
		string cmd = args[0];
		if (cmd == "unsubscribe")
		{
			BnetParty.RemoveFromAllEventHandlers(this);
			s_hasSubscribedToPartyEvents = false;
			Log.Party.Print("party {0}: unsubscribed.", cmd);
			return true;
		}
		bool result = true;
		string[] array = args.Skip(1).ToArray();
		string errorMsg = null;
		SubscribePartyEvents();
		switch (cmd)
		{
		case "create":
		{
			if (array.Length < 1)
			{
				errorMsg = "party create: requires a PartyType: " + string.Join(" | ", (from PartyType v in Enum.GetValues(typeof(PartyType))
					select string.Concat(v, " (", (int)v, ")")).ToArray());
				break;
			}
			PartyType outVal;
			if (int.TryParse(array[0], out var result5))
			{
				outVal = (PartyType)result5;
			}
			else if (!EnumUtils.TryGetEnum<PartyType>(array[0], out outVal))
			{
				errorMsg = "party create: unknown PartyType specified: " + array[0];
			}
			if (errorMsg == null)
			{
				byte[] creatorBlob = ProtobufUtil.ToByteArray(BnetUtils.CreatePegasusBnetId(BnetPresenceMgr.Get().GetMyGameAccountId()));
				BnetParty.CreateParty(outVal, PrivacyLevel.OPEN_INVITATION, creatorBlob, delegate(PartyType t, PartyId partyId)
				{
					Log.Party.Print("BnetParty.CreateSuccessCallback type={0} partyId={1}", t, partyId);
				});
			}
			break;
		}
		case "leave":
		case "dissolve":
		{
			bool flag7 = cmd == "dissolve";
			if (array.Length == 0)
			{
				Log.Party.Print("NOTE: party {0} without any arguments will {0} all joined parties.", cmd);
				PartyInfo[] joinedParties2 = BnetParty.GetJoinedParties();
				if (joinedParties2.Length == 0)
				{
					Log.Party.Print("No joined parties.");
				}
				PartyInfo[] array9 = joinedParties2;
				foreach (PartyInfo partyInfo in array9)
				{
					Log.Party.Print("party {0}: {1} party {2}", cmd, flag7 ? "dissolving" : "leaving", partyInfo);
					if (flag7)
					{
						BnetParty.DissolveParty(partyInfo.Id);
					}
					else
					{
						BnetParty.Leave(partyInfo.Id);
					}
				}
				break;
			}
			for (int num10 = 0; num10 < array.Length; num10++)
			{
				string arg2 = array[num10];
				string errorMsg2 = null;
				PartyId partyId11 = ParsePartyId(cmd, arg2, num10, ref errorMsg2);
				if (errorMsg2 != null)
				{
					Log.Party.Print(errorMsg2);
				}
				if (partyId11 != null)
				{
					Log.Party.Print("party {0}: {1} party {2}", cmd, flag7 ? "dissolving" : "leaving", BnetParty.GetJoinedParty(partyId11));
					if (flag7)
					{
						BnetParty.DissolveParty(partyId11);
					}
					else
					{
						BnetParty.Leave(partyId11);
					}
				}
			}
			break;
		}
		case "join":
		{
			if (array.Length < 1)
			{
				errorMsg = "party " + cmd + ": must specify an online friend index or a partyId (Hi-Lo format)";
				break;
			}
			PartyType partyType2 = PartyType.DEFAULT;
			string[] array4 = array;
			foreach (string text4 in array4)
			{
				int num7 = text4.IndexOf('-');
				int result7 = -1;
				PartyId partyId7 = null;
				if (num7 >= 0)
				{
					string s3 = text4.Substring(0, num7);
					string s4 = ((text4.Length > num7) ? text4.Substring(num7 + 1) : "");
					if (ulong.TryParse(s3, out var result8) && ulong.TryParse(s4, out var result9))
					{
						partyId7 = new PartyId(result8, result9);
					}
					else
					{
						errorMsg = "party " + cmd + ": unable to parse partyId (in format Hi-Lo).";
					}
				}
				else if (int.TryParse(text4, out result7))
				{
					BnetPlayer[] array7 = (from p in BnetFriendMgr.Get().GetFriends()
						where p.IsOnline() && p.GetHearthstoneGameAccount() != null
						select p).ToArray();
					errorMsg = ((result7 >= 0 && result7 < array7.Length) ? ("party " + cmd + ": Not-Yet-Implemented: find partyId from online friend's presence.") : ("party " + cmd + ": no online friend at index " + result7));
				}
				else
				{
					errorMsg = "party " + cmd + ": unable to parse online friend index.";
				}
				if (partyId7 != null)
				{
					BnetParty.JoinParty(partyId7, partyType2);
				}
			}
			break;
		}
		case "chat":
		{
			PartyId[] joinedPartyIds2 = BnetParty.GetJoinedPartyIds();
			if (array.Length < 1)
			{
				errorMsg = "party chat: must specify 1-2 arguments: party (index or LowBits or type) or a message to send.";
				break;
			}
			int count = 1;
			PartyId partyId4 = ParsePartyId(cmd, array[0], -1, ref errorMsg);
			if (partyId4 == null && joinedPartyIds2.Length != 0)
			{
				errorMsg = null;
				partyId4 = joinedPartyIds2[0];
				count = 0;
			}
			if (partyId4 != null)
			{
				BnetParty.SendChatMessage(partyId4, string.Join(" ", array.Skip(count).ToArray()));
			}
			break;
		}
		case "invite":
		{
			PartyId partyId9 = null;
			int count2 = 1;
			if (array.Length == 0)
			{
				PartyId[] joinedPartyIds5 = BnetParty.GetJoinedPartyIds();
				if (joinedPartyIds5.Length != 0)
				{
					partyId9 = joinedPartyIds5[0];
					count2 = 0;
				}
				else
				{
					errorMsg = "party invite: no joined parties to invite to.";
				}
			}
			else
			{
				partyId9 = ParsePartyId(cmd, array[0], -1, ref errorMsg);
			}
			if (!(partyId9 != null))
			{
				break;
			}
			string[] array8 = array.Skip(count2).ToArray();
			HashSet<BnetPlayer> hashSet = new HashSet<BnetPlayer>();
			IEnumerable<BnetPlayer> source = from p in BnetFriendMgr.Get().GetFriends()
				where p.IsOnline() && p.GetHearthstoneGameAccount() != null
				select p;
			if (array8.Length == 0)
			{
				Log.Party.Print("NOTE: party invite without any arguments will pick the first online friend.");
				BnetPlayer bnetPlayer = null;
				bnetPlayer = source.FirstOrDefault();
				if (bnetPlayer == null)
				{
					errorMsg = "party invite: no online Hearthstone friend found.";
				}
				else
				{
					hashSet.Add(bnetPlayer);
				}
			}
			else
			{
				for (int num8 = 0; num8 < array8.Length; num8++)
				{
					string arg = array8[num8];
					if (int.TryParse(arg, out var result11))
					{
						BnetPlayer bnetPlayer2 = source.ElementAtOrDefault(result11);
						if (bnetPlayer2 == null)
						{
							errorMsg = "party invite: no online Hearthstone friend index " + result11;
						}
						else
						{
							hashSet.Add(bnetPlayer2);
						}
						continue;
					}
					IEnumerable<BnetPlayer> enumerable3 = source.Where((BnetPlayer p) => p.GetBattleTag().ToString().Contains(arg, StringComparison.OrdinalIgnoreCase) || (p.GetFullName() != null && p.GetFullName().Contains(arg, StringComparison.OrdinalIgnoreCase)));
					if (!enumerable3.Any())
					{
						errorMsg = "party invite: no online Hearthstone friend matching name " + arg + " (arg index " + num8 + ")";
						continue;
					}
					foreach (BnetPlayer item in enumerable3)
					{
						if (!hashSet.Contains(item))
						{
							hashSet.Add(item);
							break;
						}
					}
				}
			}
			foreach (BnetPlayer item2 in hashSet)
			{
				BnetGameAccountId hearthstoneGameAccountId = item2.GetHearthstoneGameAccountId();
				if (BnetParty.IsMember(partyId9, hearthstoneGameAccountId))
				{
					Log.Party.Print("party invite: already a party member of {0}: {1}", item2, BnetParty.GetJoinedParty(partyId9));
				}
				else
				{
					Log.Party.Print("party invite: inviting {0} {1} to party {2}", hearthstoneGameAccountId, item2, BnetParty.GetJoinedParty(partyId9));
					BnetParty.SendInvite(partyId9, hearthstoneGameAccountId, isReservation: true);
				}
			}
			break;
		}
		case "accept":
		case "decline":
		{
			bool flag2 = cmd == "accept";
			PartyInvite[] receivedInvites2 = BnetParty.GetReceivedInvites();
			if (receivedInvites2.Length == 0)
			{
				errorMsg = "party " + cmd + ": no received party invites.";
				break;
			}
			if (array.Length == 0)
			{
				Log.Party.Print("NOTE: party {0} without any arguments will {0} all received invites.", cmd);
				PartyInvite[] array3 = receivedInvites2;
				foreach (PartyInvite partyInvite in array3)
				{
					Log.Party.Print("party {0}: {1} inviteId={2} from {3} for party {4}.", cmd, flag2 ? "accepting" : "declining", partyInvite.InviteId, partyInvite.InviterName, new PartyInfo(partyInvite.PartyId, partyInvite.PartyType));
					if (flag2)
					{
						BnetParty.AcceptReceivedInvite(partyInvite.InviteId);
					}
					else
					{
						BnetParty.DeclineReceivedInvite(partyInvite.InviteId);
					}
				}
				break;
			}
			for (int num4 = 0; num4 < array.Length; num4++)
			{
				if (ulong.TryParse(array[num4], out var indexOrId3))
				{
					PartyInvite partyInvite2 = null;
					if (indexOrId3 < (ulong)receivedInvites2.LongLength)
					{
						partyInvite2 = receivedInvites2[indexOrId3];
					}
					else
					{
						partyInvite2 = receivedInvites2.FirstOrDefault((PartyInvite inv) => inv.InviteId == indexOrId3);
						if (partyInvite2 == null)
						{
							Log.Party.Print("party {0}: unable to find received invite (id or index): {1}", cmd, array[num4]);
						}
					}
					if (partyInvite2 != null)
					{
						Log.Party.Print("party {0}: {1} inviteId={2} from {3} for party {4}.", cmd, flag2 ? "accepting" : "declining", partyInvite2.InviteId, partyInvite2.InviterName, new PartyInfo(partyInvite2.PartyId, partyInvite2.PartyType));
						if (flag2)
						{
							BnetParty.AcceptReceivedInvite(partyInvite2.InviteId);
						}
						else
						{
							BnetParty.DeclineReceivedInvite(partyInvite2.InviteId);
						}
					}
				}
				else
				{
					Log.Party.Print("party {0}: unable to parse invite (id or index): {1}", cmd, array[num4]);
				}
			}
			break;
		}
		case "revoke":
		{
			PartyId partyId14 = null;
			if (array.Length == 0)
			{
				Log.Party.Print("NOTE: party {0} without any arguments will {0} all sent invites for all parties.", cmd);
				PartyId[] joinedPartyIds8 = BnetParty.GetJoinedPartyIds();
				if (joinedPartyIds8.Length == 0)
				{
					Log.Party.Print("party {0}: no joined parties.", cmd);
				}
				PartyId[] array6 = joinedPartyIds8;
				foreach (PartyId partyId15 in array6)
				{
					PartyInvite[] array3 = BnetParty.GetSentInvites(partyId15);
					foreach (PartyInvite partyInvite3 in array3)
					{
						Log.Party.Print("party {0}: revoking inviteId={1} from {2} for party {3}.", cmd, partyInvite3.InviteId, partyInvite3.InviterName, BnetParty.GetJoinedParty(partyId15));
						BnetParty.RevokeSentInvite(partyId15, partyInvite3.InviteId);
					}
				}
			}
			else
			{
				partyId14 = ParsePartyId(cmd, array[0], -1, ref errorMsg);
			}
			if (!(partyId14 != null))
			{
				break;
			}
			PartyInfo joinedParty3 = BnetParty.GetJoinedParty(partyId14);
			PartyInvite[] sentInvites2 = BnetParty.GetSentInvites(partyId14);
			if (sentInvites2.Length == 0)
			{
				errorMsg = "party " + cmd + ": no sent invites for party " + joinedParty3;
				break;
			}
			string[] array11 = array.Skip(1).ToArray();
			if (array11.Length == 0)
			{
				Log.Party.Print("NOTE: party {0} without specifying InviteId (or index) will {0} all sent invites.", cmd);
				PartyInvite[] array3 = sentInvites2;
				foreach (PartyInvite partyInvite4 in array3)
				{
					Log.Party.Print("party {0}: revoking inviteId={1} from {2} for party {3}.", cmd, partyInvite4.InviteId, partyInvite4.InviterName, joinedParty3);
					BnetParty.RevokeSentInvite(partyId14, partyInvite4.InviteId);
				}
				break;
			}
			for (int num12 = 0; num12 < array11.Length; num12++)
			{
				if (ulong.TryParse(array11[num12], out var indexOrId))
				{
					PartyInvite partyInvite5 = null;
					if (indexOrId < (ulong)sentInvites2.LongLength)
					{
						partyInvite5 = sentInvites2[indexOrId];
					}
					else
					{
						partyInvite5 = sentInvites2.FirstOrDefault((PartyInvite inv) => inv.InviteId == indexOrId);
						if (partyInvite5 == null)
						{
							Log.Party.Print("party {0}: unable to find sent invite (id or index): {1} for party {2}", cmd, array11[num12], joinedParty3);
						}
					}
					if (partyInvite5 != null)
					{
						Log.Party.Print("party {0}: revoking inviteId={1} from {2} for party {3}.", cmd, partyInvite5.InviteId, partyInvite5.InviterName, joinedParty3);
						BnetParty.RevokeSentInvite(partyId14, partyInvite5.InviteId);
					}
				}
				else
				{
					Log.Party.Print("party {0}: unable to parse invite (id or index): {1}", cmd, array11[num12]);
				}
			}
			break;
		}
		case "requestinvite":
		{
			if (array.Length < 2)
			{
				errorMsg = "party " + cmd + ": must specify a partyId (Hi-Lo format) and an online friend index";
				break;
			}
			PartyType partyType = PartyType.DEFAULT;
			string[] array4 = array;
			foreach (string text3 in array4)
			{
				int num5 = text3.IndexOf('-');
				int result2 = -1;
				PartyId partyId3 = null;
				BnetGameAccountId bnetGameAccountId = null;
				if (num5 >= 0)
				{
					string s = text3.Substring(0, num5);
					string s2 = ((text3.Length > num5) ? text3.Substring(num5 + 1) : "");
					if (ulong.TryParse(s, out var result3) && ulong.TryParse(s2, out var result4))
					{
						partyId3 = new PartyId(result3, result4);
					}
					else
					{
						errorMsg = "party " + cmd + ": unable to parse partyId (in format Hi-Lo).";
					}
				}
				else if (int.TryParse(text3, out result2))
				{
					BnetPlayer[] array5 = (from p in BnetFriendMgr.Get().GetFriends()
						where p.IsOnline() && p.GetHearthstoneGameAccount() != null
						select p).ToArray();
					if (result2 < 0 || result2 >= array5.Length)
					{
						errorMsg = "party " + cmd + ": no online friend at index " + result2;
					}
					else
					{
						bnetGameAccountId = array5[result2].GetHearthstoneGameAccountId();
					}
				}
				else
				{
					errorMsg = "party " + cmd + ": unable to parse online friend index.";
				}
				if (partyId3 != null && bnetGameAccountId != null)
				{
					BnetParty.RequestInvite(partyId3, bnetGameAccountId, BnetPresenceMgr.Get().GetMyGameAccountId(), partyType);
				}
			}
			break;
		}
		case "ignorerequest":
		{
			PartyId[] joinedPartyIds4 = BnetParty.GetJoinedPartyIds();
			if (joinedPartyIds4.Length == 0)
			{
				Log.Party.Print("party {0}: no joined parties.", cmd);
				break;
			}
			PartyId[] array6 = joinedPartyIds4;
			foreach (PartyId partyId6 in array6)
			{
				InviteRequest[] inviteRequests = BnetParty.GetInviteRequests(partyId6);
				foreach (InviteRequest inviteRequest in inviteRequests)
				{
					Log.Party.Print("party {0}: ignoring request to invite {0} {1} from {2} {3}.", inviteRequest.TargetName, inviteRequest.TargetId, inviteRequest.RequesterName, inviteRequest.RequesterId);
					BnetParty.IgnoreInviteRequest(partyId6, inviteRequest.TargetId);
				}
			}
			break;
		}
		case "setleader":
		{
			IEnumerable<PartyId> enumerable2 = null;
			int result6 = -1;
			if (array.Length >= 2 && (!int.TryParse(array[1], out result6) || result6 < 0))
			{
				errorMsg = $"party {cmd}: invalid memberIndex={array[1]}";
			}
			if (array.Length == 0)
			{
				Log.Party.Print("NOTE: party {0} without any arguments will {0} to first member in all parties.", cmd);
				PartyId[] joinedPartyIds3 = BnetParty.GetJoinedPartyIds();
				if (joinedPartyIds3.Length == 0)
				{
					Log.Party.Print("party {0}: no joined parties.", cmd);
				}
				else
				{
					enumerable2 = joinedPartyIds3;
				}
			}
			else
			{
				PartyId partyId5 = ParsePartyId(cmd, array[0], -1, ref errorMsg);
				if (partyId5 != null)
				{
					enumerable2 = new PartyId[1] { partyId5 };
				}
			}
			if (enumerable2 == null)
			{
				break;
			}
			foreach (PartyId item3 in enumerable2)
			{
				PartyMember[] members2 = BnetParty.GetMembers(item3);
				if (result6 >= 0)
				{
					if (result6 >= members2.Length)
					{
						Log.Party.Print("party {0}: party={1} has no member at index={2}", cmd, BnetParty.GetJoinedParty(item3), result6);
					}
					else
					{
						PartyMember partyMember = members2[result6];
						BnetParty.SetLeader(item3, partyMember.GameAccountId);
					}
				}
				else if (members2.Any((PartyMember m) => m.GameAccountId != BnetPresenceMgr.Get().GetMyGameAccountId()))
				{
					BnetParty.SetLeader(item3, members2.First((PartyMember m) => m.GameAccountId != BnetPresenceMgr.Get().GetMyGameAccountId()).GameAccountId);
				}
				else
				{
					Log.Party.Print("party {0}: party={1} has no member not myself to set as leader.", cmd, BnetParty.GetJoinedParty(item3));
				}
			}
			break;
		}
		case "kick":
		{
			PartyId partyId12 = null;
			if (array.Length == 0)
			{
				Log.Party.Print("NOTE: party {0} without any arguments will {0} all members for all parties (other than self).", cmd);
				PartyId[] joinedPartyIds7 = BnetParty.GetJoinedPartyIds();
				if (joinedPartyIds7.Length == 0)
				{
					Log.Party.Print("party {0}: no joined parties.", cmd);
				}
				PartyId[] array6 = joinedPartyIds7;
				foreach (PartyId partyId13 in array6)
				{
					PartyMember[] members3 = BnetParty.GetMembers(partyId13);
					foreach (PartyMember partyMember2 in members3)
					{
						if (!(partyMember2.GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId()))
						{
							Log.Party.Print("party {0}: kicking memberId={1} from party {2}.", cmd, partyMember2.GameAccountId, BnetParty.GetJoinedParty(partyId13));
							BnetParty.KickMember(partyId13, partyMember2.GameAccountId);
						}
					}
				}
			}
			else
			{
				partyId12 = ParsePartyId(cmd, array[0], -1, ref errorMsg);
			}
			if (!(partyId12 != null))
			{
				break;
			}
			PartyInfo joinedParty2 = BnetParty.GetJoinedParty(partyId12);
			PartyMember[] members4 = BnetParty.GetMembers(partyId12);
			if (members4.Length == 1)
			{
				errorMsg = "party " + cmd + ": no members (other than self) for party " + joinedParty2;
				break;
			}
			string[] array10 = array.Skip(1).ToArray();
			if (array10.Length == 0)
			{
				Log.Party.Print("NOTE: party {0} without specifying member index will {0} all members (other than self).", cmd);
				PartyMember[] members3 = members4;
				foreach (PartyMember partyMember3 in members3)
				{
					if (!(partyMember3.GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId()))
					{
						Log.Party.Print("party {0}: kicking memberId={1} from party {2}.", cmd, partyMember3.GameAccountId, joinedParty2);
						BnetParty.KickMember(partyId12, partyMember3.GameAccountId);
					}
				}
				break;
			}
			for (int num11 = 0; num11 < array10.Length; num11++)
			{
				if (ulong.TryParse(array10[num11], out var indexOrId2))
				{
					PartyMember partyMember4 = null;
					if (indexOrId2 < (ulong)members4.LongLength)
					{
						partyMember4 = members4[indexOrId2];
					}
					else
					{
						partyMember4 = members4.FirstOrDefault((PartyMember m) => m.GameAccountId.GetLo() == indexOrId2);
						if (partyMember4 == null)
						{
							Log.Party.Print("party {0}: unable to find member (id or index): {1} for party {2}", cmd, array10[num11], joinedParty2);
						}
					}
					if (partyMember4 != null)
					{
						if (partyMember4.GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId())
						{
							Log.Party.Print("party {0}: cannot kick yourself (argIndex={1}); party={2}", cmd, num11, joinedParty2);
						}
						else
						{
							Log.Party.Print("party {0}: kicking memberId={1} from party {2}.", cmd, partyMember4.GameAccountId, joinedParty2);
							BnetParty.KickMember(partyId12, partyMember4.GameAccountId);
						}
					}
				}
				else
				{
					Log.Party.Print("party {0}: unable to parse member (id or index): {1}", cmd, array10[num11]);
				}
			}
			break;
		}
		case "setprivacy":
		{
			PartyId partyId8 = null;
			if (array.Length < 2)
			{
				errorMsg = "party setprivacy: must specify a party (index or LowBits or type) and a PrivacyLevel: " + string.Join(" | ", (from PrivacyLevel v in Enum.GetValues(typeof(PrivacyLevel))
					select string.Concat(v, " (", (int)v, ")")).ToArray());
			}
			else
			{
				partyId8 = ParsePartyId(cmd, array[0], -1, ref errorMsg);
			}
			if (partyId8 != null)
			{
				PrivacyLevel? privacyLevel = null;
				PrivacyLevel outVal2;
				if (int.TryParse(array[1], out var result10))
				{
					privacyLevel = (PrivacyLevel)result10;
				}
				else if (!EnumUtils.TryGetEnum<PrivacyLevel>(array[1], out outVal2))
				{
					errorMsg = "party setprivacy: unknown PrivacyLevel specified: " + array[1];
				}
				else
				{
					privacyLevel = outVal2;
				}
				if (privacyLevel.HasValue)
				{
					Log.Party.Print("party setprivacy: setting PrivacyLevel={0} for party {1}.", privacyLevel.Value, BnetParty.GetJoinedParty(partyId8));
					BnetParty.SetPrivacy(partyId8, privacyLevel.Value);
				}
			}
			break;
		}
		case "setlong":
		case "setstring":
		case "setblob":
		{
			bool flag3 = cmd == "setlong";
			bool flag4 = cmd == "setstring";
			bool flag5 = cmd == "setblob";
			int num9 = 1;
			PartyId partyId10 = null;
			if (array.Length < 2)
			{
				errorMsg = "party " + cmd + ": must specify attributeKey and a value.";
			}
			else
			{
				partyId10 = ParsePartyId(cmd, array[0], -1, ref errorMsg);
				if (partyId10 == null)
				{
					PartyId[] joinedPartyIds6 = BnetParty.GetJoinedPartyIds();
					if (joinedPartyIds6.Length != 0)
					{
						Log.Party.Print("party {0}: treating first argument as attributeKey (and not PartyId) - will use PartyId at index 0", cmd);
						errorMsg = null;
						partyId10 = joinedPartyIds6[0];
					}
				}
				else
				{
					Log.Party.Print("party {0}: treating first argument as PartyId (second argument will be attributeKey)", cmd);
				}
			}
			if (!(partyId10 != null))
			{
				break;
			}
			bool flag6 = false;
			string text5 = array[num9];
			string text6 = string.Join(" ", array.Skip(num9 + 1).ToArray());
			if (flag3)
			{
				if (long.TryParse(text6, out var result12))
				{
					BnetParty.SetPartyAttributeLong(partyId10, text5, result12);
					flag6 = true;
				}
			}
			else if (flag4)
			{
				BnetParty.SetPartyAttributeString(partyId10, text5, text6);
				flag6 = true;
			}
			else if (flag5)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(text6);
				BnetParty.SetPartyAttributeBlob(partyId10, text5, bytes);
				flag6 = true;
			}
			else
			{
				errorMsg = "party " + cmd + ": unhandled attribute type!";
			}
			if (flag6)
			{
				Log.Party.Print("party {0}: complete key={1} val={2} party={3}", cmd, text5, text6, BnetParty.GetJoinedParty(partyId10));
			}
			break;
		}
		case "clearattr":
		{
			PartyId partyId2 = null;
			if (array.Length < 2)
			{
				errorMsg = "party " + cmd + ": must specify attributeKey.";
			}
			else
			{
				partyId2 = ParsePartyId(cmd, array[0], -1, ref errorMsg);
				if (partyId2 == null)
				{
					PartyId[] joinedPartyIds = BnetParty.GetJoinedPartyIds();
					if (joinedPartyIds.Length != 0)
					{
						Log.Party.Print("party {0}: treating first argument as attributeKey (and not PartyId) - will use PartyId at index 0", cmd);
						errorMsg = null;
						partyId2 = joinedPartyIds[0];
					}
				}
				else
				{
					Log.Party.Print("party {0}: treating first argument as PartyId (second argument will be attributeKey)", cmd);
				}
			}
			if (partyId2 != null)
			{
				string text2 = array[1];
				BnetParty.ClearPartyAttribute(partyId2, text2);
				Log.Party.Print("party {0}: cleared key={1} party={2}", cmd, text2, BnetParty.GetJoinedParty(partyId2));
			}
			break;
		}
		case "subscribe":
		case "list":
		{
			IEnumerable<PartyId> enumerable = null;
			if (array.Length == 0)
			{
				PartyInfo[] joinedParties = BnetParty.GetJoinedParties();
				if (joinedParties.Length == 0)
				{
					Log.Party.Print("party list: no joined parties.");
				}
				else
				{
					Log.Party.Print("party list: listing all joined parties and the details of the party at index 0.");
					enumerable = new PartyId[1] { joinedParties[0].Id };
				}
				for (int j = 0; j < joinedParties.Length; j++)
				{
					Log.Party.Print("   {0}", GetPartySummary(joinedParties[j], j));
				}
			}
			else
			{
				enumerable = from p in array.Select(delegate(string a, int i)
					{
						string errorMsg3 = null;
						PartyId result13 = ParsePartyId(cmd, a, i, ref errorMsg3);
						if (errorMsg3 != null)
						{
							Log.Party.Print(errorMsg3);
						}
						return result13;
					})
					where p != null
					select p;
			}
			if (enumerable != null)
			{
				int num = -1;
				foreach (PartyId item4 in enumerable)
				{
					num++;
					PartyInfo joinedParty = BnetParty.GetJoinedParty(item4);
					Log.Party.Print("party {0}: {1}", cmd, GetPartySummary(BnetParty.GetJoinedParty(item4), num));
					PartyMember[] members = BnetParty.GetMembers(item4);
					if (members.Length == 0)
					{
						Log.Party.Print("   no members.");
					}
					else
					{
						Log.Party.Print("   members:");
					}
					for (int k = 0; k < members.Length; k++)
					{
						bool flag = members[k].GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId();
						Log.Party.Print("      [{0}] {1} isMyself={2} isLeader={3} roleIds={4}", k, members[k].GameAccountId, flag, members[k].IsLeader(joinedParty.Type), string.Join(",", members[k].RoleIds.Select((uint r) => r.ToString()).ToArray()));
					}
					PartyInvite[] sentInvites = BnetParty.GetSentInvites(item4);
					if (sentInvites.Length == 0)
					{
						Log.Party.Print("   no sent invites.");
					}
					else
					{
						Log.Party.Print("   sent invites:");
					}
					for (int l = 0; l < sentInvites.Length; l++)
					{
						PartyInvite invite = sentInvites[l];
						Log.Party.Print("      {0}", GetPartyInviteSummary(invite, l));
					}
					KeyValuePair<string, object>[] allPartyAttributes = BnetParty.GetAllPartyAttributes(item4);
					if (allPartyAttributes.Length == 0)
					{
						Log.Party.Print("   no party attributes.");
					}
					else
					{
						Log.Party.Print("   party attributes:");
					}
					for (int n = 0; n < allPartyAttributes.Length; n++)
					{
						KeyValuePair<string, object> keyValuePair = allPartyAttributes[n];
						string text = ((keyValuePair.Value == null) ? "<null>" : $"[{keyValuePair.Value.GetType().Name}]{keyValuePair.Value.ToString()}");
						if (keyValuePair.Value is byte[])
						{
							byte[] array2 = (byte[])keyValuePair.Value;
							text = "blobLength=" + array2.Length;
							try
							{
								string @string = Encoding.UTF8.GetString(array2);
								if (@string != null)
								{
									text = text + " decodedUtf8=" + @string;
								}
							}
							catch (ArgumentException)
							{
							}
						}
						Log.Party.Print("      {0}={1}", keyValuePair.Key ?? "<null>", text);
					}
				}
			}
			PartyInvite[] receivedInvites = BnetParty.GetReceivedInvites();
			if (receivedInvites.Length == 0)
			{
				Log.Party.Print("party list: no received party invites.");
			}
			else
			{
				Log.Party.Print("party list: received party invites:");
			}
			for (int num2 = 0; num2 < receivedInvites.Length; num2++)
			{
				PartyInvite invite2 = receivedInvites[num2];
				Log.Party.Print("   {0}", GetPartyInviteSummary(invite2, num2));
			}
			break;
		}
		default:
			errorMsg = "party: unknown party cmd: " + cmd;
			break;
		}
		if (errorMsg != null)
		{
			Log.Party.Print(errorMsg);
			Error.AddWarning("Party Cheat Error", errorMsg);
			result = false;
		}
		return result;
	}

	private static string GetPartyInviteSummary(PartyInvite invite, int index)
	{
		return string.Format("{0}: inviteId={1} sender={2} recipient={3} party={4}", (index >= 0) ? $"[{index}] " : "", invite.InviteId, string.Concat(invite.InviterId, " ", invite.InviterName), invite.InviteeId, new PartyInfo(invite.PartyId, invite.PartyType));
	}

	private static string GetPartySummary(PartyInfo info, int index)
	{
		PartyMember leader = BnetParty.GetLeader(info.Id);
		return string.Format("{0}{1}: members={2} invites={3} privacy={4} leader={5}", (index >= 0) ? $"[{index}] " : "", info, BnetParty.CountMembers(info.Id) + (BnetParty.IsPartyFull(info.Id) ? "(full)" : ""), BnetParty.GetSentInvites(info.Id).Length, BnetParty.GetPrivacyLevel(info.Id), (leader == null) ? "null" : leader.GameAccountId.ToString());
	}

	private bool OnProcessCheat_cheat(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = "spawncard, drawcard, loadcard, cyclehand, shuffle, addmana, readymana, maxmana, nocosts, healhero, healentity, nuke, damage, settag, ready, exhaust, freeze, move, undo, destroyhero, tiegame, getgsd, aiplaylastspawnedcard, forcestallingprevention, endturn, logrelay";
		if (autofillData != null)
		{
			string[] array = null;
			string[] array2 = new string[2] { "friendly", "opponent" };
			string[] array3 = new string[7] { "InPlay", "InDeck", "InHand", "InGraveyard", "InRemovedFromGame", "InSetAside", "InSecret" };
			Func<string[]> func2 = () => GameDbf.GetIndex().GetAllCardIds().ToArray();
			string searchTerm = autofillData.m_lastAutofillParamPrefix ?? ((args.Length == 0) ? string.Empty : args.Last());
			int num = args.Length;
			if (rawArgs.EndsWith(" "))
			{
				searchTerm = string.Empty;
				num++;
			}
			if (num > 1 && !string.IsNullOrEmpty(args[0]))
			{
				text = null;
				switch (args[0])
				{
				case "spawncard":
					switch (num)
					{
					case 4:
						array = new string[2] { "1", "0" };
						break;
					case 3:
						array = array3;
						break;
					case 2:
						array = func2();
						break;
					}
					break;
				case "loadcard":
					if (num == 2)
					{
						array = func2();
					}
					break;
				case "drawcard":
				case "cyclehand":
				case "shuffle":
				case "addmana":
				case "readymana":
				case "maxmana":
				case "healhero":
				case "nuke":
				case "destroyhero":
					if (num == 2)
					{
						array = array2;
					}
					break;
				case "move":
					if (num == 3)
					{
						array = array3;
					}
					break;
				case "getgsd":
					if (num == 2)
					{
						array = array2;
					}
					break;
				}
			}
			if (array == null)
			{
				if (text == null)
				{
					return false;
				}
				array = text.Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
			}
			return ProcessAutofillParam(array, searchTerm, autofillData);
		}
		if (!Network.Get().IsConnectedToGameServer())
		{
			UIStatus.Get().AddInfoNoRichText("Not connected to a game. Cannot send cheat command.");
			return true;
		}
		Network.Get().SendDebugConsoleCommand(rawArgs);
		return true;
	}

	private bool OnProcessCheat_autohand(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0)
		{
			return false;
		}
		if (!GeneralUtils.TryParseBool(args[0], out var boolVal))
		{
			return false;
		}
		if (InputManager.Get() == null)
		{
			return false;
		}
		string message = ((!boolVal) ? "auto hand hiding is off" : "auto hand hiding is on");
		Debug.Log(message);
		UIStatus.Get().AddInfo(message);
		InputManager.Get().SetHideHandAfterPlayingCard(boolVal);
		return true;
	}

	private bool OnProcessCheat_adventureChallengeUnlock(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		if (!int.TryParse(args[0].ToLowerInvariant(), out var result))
		{
			return false;
		}
		List<int> list = new List<int>();
		list.Add(result);
		AdventureMissionDisplay.Get().ShowClassChallengeUnlock(list);
		return true;
	}

	private bool OnProcessCheat_iks(string func, string[] args, string rawArgs)
	{
		InnKeepersSpecial.Get().InitializeJsonURL(args[0]);
		InnKeepersSpecial.Get().ResetAdUrl();
		Processor.RunCoroutine(TriggerWelcomeQuestShow());
		return true;
	}

	private IEnumerator TriggerWelcomeQuestShow()
	{
		yield return new WaitForSeconds(1f);
		while (InnKeepersSpecial.Get().ProcessingResponse)
		{
			yield return new WaitForSeconds(1f);
		}
		QuestManager questManager = QuestManager.Get();
		if (questManager.IsSystemEnabled)
		{
			questManager.SimulateQuestNotificationPopup(QuestPool.QuestPoolType.DAILY);
		}
		else
		{
			WelcomeQuests.Show(UserAttentionBlocker.ALL, fromLogin: true);
		}
	}

	private bool OnProcessCheat_iksgameaction(string func, string[] args, string rawArgs)
	{
		if (string.IsNullOrEmpty(rawArgs))
		{
			UIStatus.Get().AddError("Please specify a game action.");
			return true;
		}
		DeepLinkManager.ExecuteDeepLink(args, DeepLinkManager.DeepLinkSource.INNKEEPERS_SPECIAL, fromUnpause: false);
		return true;
	}

	private bool OnProcessCheat_iksseen(string func, string[] args, string rawArgs)
	{
		if (string.IsNullOrEmpty(rawArgs))
		{
			UIStatus.Get().AddError("Please specify a game action.");
			return true;
		}
		string gameAction = string.Join(" ", args);
		UIStatus.Get().AddInfo("Has Interacted With Product: " + InnKeepersSpecial.Get().HasInteractedWithAdvertisedProduct(gameAction));
		return true;
	}

	private bool OnProcessCheat_quote(string func, string[] args, string rawArgs)
	{
		string text = "innkeeper";
		string text2 = "VO_INNKEEPER_FIRST_100_GOLD";
		string soundPath = "VO_INNKEEPER_FIRST_100_GOLD.prefab:c6a50337099a454488acd96d2f37320f";
		if (args.Length < 1 || !(args[0] == "default"))
		{
			if (args.Length < 2)
			{
				UIStatus.Get().AddError("Please specify 2 arguments: CharacterPrefabAssetRef GameStringsKey [AudioAssetRef]\nExamples:\nquote default\nquote innkeeper VO_TUTORIAL_01_ANNOUNCER_05 VO_TUTORIAL_01_ANNOUNCER_05.prefab:635b33010e4704a42a87c7625b5b5ada\nquote Barnes_Quote.prefab:2e7e9f28b5bc37149a12b2e5feaa244a VO_Barnes_Male_Human_JulianneWin_01 VO_Barnes_Male_Human_JulianneWin_01.prefab:09d4c4aaf43ac634aaf325c2badc72a8", 5f * Time.timeScale);
				return true;
			}
			text = args[0];
			text2 = args[1];
			soundPath = text2;
			if (args.Length > 2)
			{
				soundPath = args[2];
			}
		}
		if (text.ToLowerInvariant().Contains("innkeeper"))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.ALL, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get(text2), soundPath);
		}
		else
		{
			NotificationManager.Get().CreateCharacterQuote(text, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get(text2), soundPath);
		}
		return true;
	}

	private bool OnProcessCheat_popuptext(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		string text = args[0];
		NotificationManager.Get().CreatePopupText(UserAttentionBlocker.ALL, Box.Get().m_LeftDoor.transform.position, TutorialEntity.GetTextScale(), text);
		return true;
	}

	private bool OnProcessCheat_demotext(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		string demoText = args[0];
		DemoMgr.Get().CreateDemoText(demoText);
		return true;
	}

	private bool OnProcessCheat_alerttext(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_text = rawArgs;
		popupInfo.m_showAlertIcon = true;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.NONE;
		DialogManager.Get().ShowPopup(popupInfo);
		return true;
	}

	private bool OnProcessCheat_logtext(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		if (args.Length > 1)
		{
			string format = rawArgs.Substring(rawArgs.IndexOf(' ') + 1);
			switch (args[0])
			{
			case "debug":
				Log.All.PrintDebug(format);
				return true;
			case "info":
				Log.All.PrintInfo(format);
				return true;
			case "warning":
				Log.All.PrintWarning(format);
				return true;
			case "error":
				Log.All.PrintError(format);
				return true;
			}
		}
		Log.All.Print(rawArgs);
		return true;
	}

	private bool OnProcessCheat_logenable(string func, string[] args, string rawArgs)
	{
		if (args.Count() < 3)
		{
			return false;
		}
		string name = args[0];
		LogInfo logInfo = Log.Get().GetLogInfo(name);
		if (logInfo == null)
		{
			return false;
		}
		string text = args[1];
		string text2 = args[2];
		bool flag = !text2.Equals("false", StringComparison.OrdinalIgnoreCase) && text2 != "0";
		switch (text)
		{
		case "file":
			logInfo.m_filePrinting = flag;
			break;
		case "screen":
			logInfo.m_screenPrinting = flag;
			break;
		case "console":
			logInfo.m_consolePrinting = flag;
			break;
		default:
			return false;
		}
		return true;
	}

	private bool OnProcessCheat_loglevel(string func, string[] args, string rawArgs)
	{
		if (args.Count() < 2)
		{
			return false;
		}
		string name = args.ElementAtOrDefault(0);
		if (!EnumUtils.TryGetEnum<Log.LogLevel>(args.ElementAtOrDefault(1), StringComparison.OrdinalIgnoreCase, out var result))
		{
			return false;
		}
		LogInfo logInfo = Log.Get().GetLogInfo(name);
		if (logInfo == null)
		{
			return false;
		}
		logInfo.m_minLevel = result;
		return true;
	}

	private bool OnProcessCheat_cardchangepopup(string func, string[] args, string rawArgs)
	{
		bool boolVal = false;
		if (args.Length >= 1 && args[0].Length > 0)
		{
			GeneralUtils.TryParseBool(args[0], out boolVal);
		}
		bool boolVal2 = true;
		if (args.Length >= 2)
		{
			GeneralUtils.TryParseBool(args[1], out boolVal2);
		}
		int val = 3;
		if (args.Length >= 3)
		{
			GeneralUtils.TryParseInt(args[2], out val);
		}
		List<CollectibleCard> cardsToShowOverride = null;
		if (boolVal2)
		{
			cardsToShowOverride = CollectionManager.Get().GetAllCards().FindAll((CollectibleCard card) => card.PremiumType == TAG_PREMIUM.NORMAL)
				.Take(val)
				.ToList();
		}
		if (boolVal)
		{
			PopupDisplayManager.Get().ShowAddedCards(null, UserAttentionBlocker.SET_ROTATION_INTRO, null, cardsToShowOverride);
		}
		else
		{
			PopupDisplayManager.Get().ShowChangedCards(null, UserAttentionBlocker.SET_ROTATION_INTRO, null, cardsToShowOverride);
		}
		return true;
	}

	private bool OnProcessCheat_cardchangereset(string func, string[] args, string rawArgs)
	{
		ChangedCardMgr.Get().ResetCardChangesSeen();
		return true;
	}

	private bool OnProcessCheat_loginpopupsequence(string func, string[] args, string rawArgs)
	{
		PopupDisplayManager.Get().ShowLoginPopupSequence();
		return true;
	}

	private bool OnProcessCheat_loginpopupreset(string func, string[] args, string rawArgs)
	{
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LOGIN_POPUP_SEQUENCE_SEEN_POPUPS, new long[0]));
		return true;
	}

	private bool OnProcessCheat_favoritehero(string func, string[] args, string rawArgs)
	{
		if (!(SceneMgr.Get().GetScene() is CollectionManagerScene))
		{
			Debug.LogWarning("OnProcessCheat_favoritehero must be used from the CollectionManagaer!");
			return false;
		}
		if (args.Length != 3)
		{
			return false;
		}
		if (!int.TryParse(args[0].ToLowerInvariant(), out var result))
		{
			return false;
		}
		if (!EnumUtils.TryCast<TAG_CLASS>(result, out var outVal))
		{
			return false;
		}
		string name = args[1];
		if (!int.TryParse(args[2].ToLowerInvariant(), out var result2))
		{
			return false;
		}
		if (!EnumUtils.TryCast<TAG_PREMIUM>(result2, out var outVal2))
		{
			return false;
		}
		NetCache.CardDefinition cardDefinition = new NetCache.CardDefinition
		{
			Name = name,
			Premium = outVal2
		};
		Log.All.Print("OnProcessCheat_favoritehero setting favorite hero to {0} for class {1}", cardDefinition, outVal);
		Network.Get().SetFavoriteHero(outVal, cardDefinition);
		return true;
	}

	private bool OnProcessCheat_settag(string func, string[] args, string rawArgs)
	{
		if (args.Length != 3)
		{
			return false;
		}
		int num = int.Parse(args[0]);
		if (num <= 0)
		{
			return false;
		}
		int num2 = int.Parse(args[2]);
		if (num2 < 0)
		{
			return false;
		}
		int result = 0;
		if (!int.TryParse(args[1], out result))
		{
			string entityIdentifier = args[1];
			Network.Get().SetTag(num, entityIdentifier, num2);
			return true;
		}
		Network.Get().SetTag(num, result, num2);
		return true;
	}

	private bool OnProcessCheat_debugscript(string func, string[] args, string rawArgs)
	{
		ScriptDebugDisplay.Get().ToggleDebugDisplay(shouldDisplay: true);
		if (args.Length != 1)
		{
			return false;
		}
		string powerGUID = args[0];
		Network.Get().DebugScript(powerGUID);
		return true;
	}

	private bool OnProcessCheat_disablescriptdebug(string func, string[] args, string rawArgs)
	{
		ScriptDebugDisplay.Get().ToggleDebugDisplay(shouldDisplay: false);
		Network.Get().DisableScriptDebug();
		return true;
	}

	private bool OnProcessCheat_printpersistentlist(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0 || args[0] == "")
		{
			Network.Get().PrintPersistentList(0);
			return true;
		}
		for (int i = 0; i < args.Length; i++)
		{
			int entityID = int.Parse(args[i]);
			Network.Get().PrintPersistentList(entityID);
		}
		return true;
	}

	private bool OnProcessCheat_help(string func, string[] args, string rawArgs)
	{
		StringBuilder stringBuilder = new StringBuilder();
		string text = null;
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
		{
			text = args[0];
		}
		List<string> list = new List<string>();
		if (text != null)
		{
			foreach (string cheatCommand in CheatMgr.Get().GetCheatCommands())
			{
				if (cheatCommand.Contains(text))
				{
					list.Add(cheatCommand);
				}
			}
		}
		else
		{
			foreach (string cheatCommand2 in CheatMgr.Get().GetCheatCommands())
			{
				list.Add(cheatCommand2);
			}
		}
		Debug.Log(string.Concat("found commands ", list, " ", list.Count));
		if (list.Count == 1)
		{
			text = list[0];
		}
		if (text == null || list.Count != 1)
		{
			if (text == null)
			{
				stringBuilder.Append("All available cheat commands:\n");
			}
			else
			{
				stringBuilder.Append("Cheat commands containing: \"" + text + "\"\n");
			}
			int num = 0;
			string text2 = "";
			foreach (string item in list)
			{
				text2 = text2 + item + ", ";
				num++;
				if (num > 4)
				{
					num = 0;
					stringBuilder.Append(text2);
					text2 = "";
				}
			}
			if (!string.IsNullOrEmpty(text2))
			{
				stringBuilder.Append(text2);
			}
			UIStatus.Get().AddInfo(stringBuilder.ToString(), 10f);
		}
		else
		{
			string value = "";
			CheatMgr.Get().cheatDesc.TryGetValue(text, out value);
			string value2 = "";
			CheatMgr.Get().cheatArgs.TryGetValue(text, out value2);
			stringBuilder.Append("Usage: ");
			stringBuilder.Append(text);
			if (!string.IsNullOrEmpty(value2))
			{
				stringBuilder.Append(" " + value2);
			}
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append("\n(" + value + ")");
			}
			UIStatus.Get().AddInfo(stringBuilder.ToString(), 10f);
		}
		return true;
	}

	private bool OnProcessCheat_fixedrewardcomplete(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
		{
			return false;
		}
		if (!GeneralUtils.TryParseInt(args[0], out var val))
		{
			return false;
		}
		return FixedRewardsMgr.Get().Cheat_ShowFixedReward(val, PositionLoginFixedReward);
	}

	private void PositionLoginFixedReward(Reward reward)
	{
		PegasusScene scene = SceneMgr.Get().GetScene();
		reward.transform.parent = scene.transform;
		reward.transform.localRotation = Quaternion.identity;
		reward.transform.localPosition = PopupDisplayManager.Get().GetRewardLocalPos();
	}

	private bool OnProcessCheat_example(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
		{
			return false;
		}
		string text = args[0];
		string value = "";
		if (!CheatMgr.Get().cheatExamples.TryGetValue(text, out value))
		{
			return false;
		}
		CheatMgr.Get().ProcessCheat(text + " " + value);
		return true;
	}

	private bool OnProcessCheat_tavernbrawl(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: tb [cmd] [args]\nCommands: view, get, set, refresh, scenario, reset";
		if (args.Length < 1 || args.Any((string a) => string.IsNullOrEmpty(a)))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		string text = args[0];
		string[] array = args.Skip(1).ToArray();
		string text2 = null;
		switch (text)
		{
		case "help":
			text2 = "usage";
			break;
		case "reset":
			if (array.Length == 0)
			{
				text2 = "Please specify what to reset: seen, toserver";
			}
			else if ("toserver".Equals(array[0], StringComparison.InvariantCultureIgnoreCase))
			{
				if (TavernBrawlManager.Get().IsCheated)
				{
					TavernBrawlManager.Get().Cheat_ResetToServerData();
					TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
					text2 = ((tavernBrawlMission != null) ? ("TB settings reset to server-specified Scenario ID " + tavernBrawlMission.missionId) : "TB settings reset to server-specified Scenario ID <null>");
				}
				else
				{
					text2 = "TB not locally cheated. Already using server-specified data.";
				}
			}
			else if ("seen".Equals(array[0], StringComparison.InvariantCultureIgnoreCase))
			{
				int result5 = 0;
				if (array.Length > 1 && !int.TryParse(array[1], out result5))
				{
					text2 = "Error parsing new seen value: " + array[1];
				}
				if (text2 == null)
				{
					TavernBrawlManager.Get().Cheat_ResetSeenStuff(result5);
					text2 = "all \"seentb*\" client-options reset to " + result5;
				}
			}
			else
			{
				text2 = "Unknown reset parameter: " + array[0];
			}
			break;
		case "refresh":
		{
			for (BrawlType brawlType2 = BrawlType.BRAWL_TYPE_TAVERN_BRAWL; brawlType2 < BrawlType.BRAWL_TYPE_COUNT; brawlType2++)
			{
				TavernBrawlManager.Get().RefreshServerData(brawlType2);
			}
			text2 = "TB refreshing";
			break;
		}
		case "fake_active_session":
		{
			int result7 = 0;
			int.TryParse(args[1], out result7);
			TavernBrawlManager.Get().Cheat_SetActiveSession(result7);
			text2 = "Fake Tavern Brawl Session set.";
			break;
		}
		case "do_rewards":
		{
			int result6 = 0;
			int.TryParse(array[0], out result6);
			TavernBrawlMode mode = TavernBrawlMode.TB_MODE_NORMAL;
			if (array.Length > 1)
			{
				mode = (array[1].Equals("heroic") ? TavernBrawlMode.TB_MODE_HEROIC : TavernBrawlMode.TB_MODE_NORMAL);
			}
			TavernBrawlManager.Get().Cheat_DoHeroicRewards(result6, mode);
			text2 = "Doing reward animation and ending fake session if one exists.";
			break;
		}
		case "get":
		case "set":
		{
			bool flag = text == "set";
			string text3 = array.FirstOrDefault();
			if (string.IsNullOrEmpty(text3))
			{
				text2 = $"Please specify a TB variable to {text}. Variables:RefreshTime";
				break;
			}
			string text4 = null;
			switch (text3.ToLower())
			{
			case "refreshtime":
				if (flag)
				{
					text2 = "cannot set RefreshTime";
				}
				else
				{
					text4 = TavernBrawlManager.Get().CurrentScheduledSecondsToRefresh + " secs";
				}
				break;
			case "wins":
			{
				int result4 = 0;
				int.TryParse(args[2], out result4);
				TavernBrawlManager.Get().Cheat_SetWins(result4);
				text2 = $"tb set wins {result4} successful";
				break;
			}
			case "losses":
			{
				int result3 = 0;
				int.TryParse(args[2], out result3);
				TavernBrawlManager.Get().Cheat_SetLosses(result3);
				text2 = $"tb set losses {result3} successful";
				break;
			}
			}
			if (flag)
			{
				text2 = string.Format("tb set {0} {1} successful.", text3, (array.Length >= 2) ? array[1] : "null");
			}
			else if (string.IsNullOrEmpty(text2))
			{
				text2 = string.Format("tb variable {0}: {1}", text3, text4 ?? "null");
			}
			break;
		}
		case "view":
		{
			TavernBrawlMission tavernBrawlMission2 = TavernBrawlManager.Get().CurrentMission();
			if (tavernBrawlMission2 == null)
			{
				text2 = "No active Tavern Brawl at this time.";
				break;
			}
			string arg = "";
			string arg2 = "";
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(tavernBrawlMission2.missionId);
			if (record != null)
			{
				arg = record.Name;
				arg2 = record.Description;
			}
			text2 = $"Active TB: [{tavernBrawlMission2.missionId}] {arg}\n{arg2}";
			break;
		}
		case "scen":
		case "scenario":
		{
			if (array.Length < 1)
			{
				text2 = "tb scenario: requires an ID parameter";
				break;
			}
			BrawlType brawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL;
			if (array.Length > 1)
			{
				int result = -1;
				if (int.TryParse(array[1], out result) && result >= 1 && result < 3)
				{
					brawlType = (BrawlType)result;
				}
			}
			if (!int.TryParse(array[0], out var result2))
			{
				text2 = "tb scenario: invalid non-integer Scenario ID " + array[0];
			}
			if (text2 == null)
			{
				TavernBrawlManager.Get().Cheat_SetScenario(result2, brawlType);
				text2 = "tb scenario: set on client to ID: " + result2 + " for type: " + brawlType;
			}
			break;
		}
		}
		if (text2 != null)
		{
			UIStatus.Get().AddInfo(text2, 5f);
		}
		return true;
	}

	private bool OnProcessCheat_duels(string func, string[] args, string rawArgs)
	{
		string text = "USAGE: duels [cmd] [args]\nCommands: nexttreasures nextloot";
		if (args.Length < 1 || args.Any((string a) => string.IsNullOrEmpty(a)))
		{
			UIStatus.Get().AddInfo(text, 10f);
			return true;
		}
		string text2 = args[0];
		string[] array = args.Skip(1).ToArray();
		string text3 = null;
		switch (text2)
		{
		case "help":
			text3 = text;
			break;
		case "nexttreasures":
		{
			if (array.Length < 1)
			{
				text3 = "duels nexttreasures: requires 1-3 card ids to add";
				break;
			}
			int num2 = 0;
			string[] array2 = array;
			foreach (string text5 in array2)
			{
				int result2 = 0;
				if (!int.TryParse(text5, out result2))
				{
					result2 = GameUtils.TranslateCardIdToDbId(text5);
					if (result2 == 0)
					{
						text3 = "invalid card id: " + text5;
						break;
					}
				}
				m_pvpdrTreasureIds.Enqueue(result2);
				num2++;
			}
			if (text3 == null)
			{
				text3 = "Added " + num2 + " cards to next treasures list";
			}
			break;
		}
		case "nextloot":
		{
			if (array.Length < 1)
			{
				text3 = "duels nextloot: requires at least 1 id to add";
				break;
			}
			int num = 0;
			string[] array2 = array;
			foreach (string text4 in array2)
			{
				int result = 0;
				if (!int.TryParse(text4, out result))
				{
					result = GameUtils.TranslateCardIdToDbId(text4);
					if (result == 0)
					{
						text3 = "invalid card id: " + text4;
						break;
					}
				}
				m_pvpdrLootIds.Enqueue(result);
				num++;
			}
			if (text3 == null)
			{
				text3 = "Added " + num + " cards to next loot list";
			}
			break;
		}
		}
		if (text3 != null)
		{
			UIStatus.Get().AddInfo(text3, 5f);
		}
		return true;
	}

	private bool OnProcessCheat_fsg(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = "checkin, checkout, fake_gatherings, no_fake_gatherings, fake_large_scale, nearby_notice, sign, view, gps_offset, gps_set, gps_reset, find, finalize, vars, player, refreshpatrons, returntooltip";
		string[] values = text.Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
		string text2 = "USAGE: fsg [cmd] [args]\nCommands: " + text;
		if (args.Length < 1 || args.Any((string a) => string.IsNullOrEmpty(a)))
		{
			if (autofillData != null)
			{
				return ProcessAutofillParam(values, string.Empty, autofillData);
			}
			UIStatus.Get().AddInfo(text2, 10f * Time.timeScale);
			return true;
		}
		if (autofillData != null)
		{
			if (args.Length == 1)
			{
				return ProcessAutofillParam(values, args[0], autofillData);
			}
			return false;
		}
		float delay = 5f * Time.timeScale;
		string text3 = args[0];
		string text4 = null;
		switch (text3)
		{
		case "help":
			text4 = text2;
			break;
		case "fake_gatherings":
		{
			int result = 2;
			bool boolVal = false;
			if (args.Length > 1)
			{
				int.TryParse(args[1], out result);
				if (result < 1)
				{
					result = 1;
				}
			}
			if (args.Length > 2)
			{
				GeneralUtils.TryParseBool(args[2], out boolVal);
			}
			FiresideGatheringManager.Get().Cheat_CreateFakeGatherings(result, boolVal);
			text4 = $"Created {result} fake gatherings";
			break;
		}
		case "no_fake_gatherings":
			FiresideGatheringManager.Get().Cheat_RemoveFakeGatherings();
			text4 = "Removed fake gatherings";
			break;
		case "fake_large_scale":
			if (!FiresideGatheringManager.Get().IsCheckedIn)
			{
				text4 = "Check into an FSG first, to toggle Large Scale FSG.";
				break;
			}
			FiresideGatheringManager.Get().Cheat_ToggleLargeScaleFSG();
			text4 = "Large Scale FSG toggled to " + FiresideGatheringManager.Get().CurrentFSG.IsLargeScaleFsg;
			break;
		case "checkin":
			FiresideGatheringManager.Get().Cheat_CheckInToFakeFSG();
			text4 = "Checked in to fake FSG";
			break;
		case "checkout":
			FiresideGatheringManager.Get().CheckOutOfFSG();
			text4 = "Checked out from FSG";
			break;
		case "nearby_notice":
			FiresideGatheringManager.Get().Cheat_NearbyFSGNotice();
			text4 = "Simulating nearby FSGs when checked out";
			break;
		case "debug_sign":
		{
			TavernSignData lastSign = FiresideGatheringManager.Get().LastSign;
			text4 = ((lastSign != null) ? $"Last FSG Sign:\nSign: {lastSign.Sign}\nBackground: {lastSign.Background}\nMajor: {lastSign.Major}\nMinor: {lastSign.Minor}" : "No Sign has been shown");
			break;
		}
		case "sign":
		{
			int result2 = UnityEngine.Random.Range(1, 8);
			int result3 = UnityEngine.Random.Range(1, 15);
			int result4 = UnityEngine.Random.Range(1, 85);
			int result5 = UnityEngine.Random.Range(1, 43);
			string text8 = "Cheat Sign";
			TavernSignType outVal = TavernSignType.TAVERN_SIGN_TYPE_CUSTOM;
			if (args.Length > 1)
			{
				string text9 = args[1];
				if (!EnumUtils.TryGetEnum<TavernSignType>(("TAVERN_SIGN_TYPE_" + text9).ToLower(), out outVal))
				{
					outVal = TavernSignType.TAVERN_SIGN_TYPE_CUSTOM;
				}
				int.TryParse(args[1], out result2);
				if (result2 < 1)
				{
					result2 = 1;
				}
			}
			if (args.Length > 2)
			{
				int.TryParse(args[2], out result3);
				if (result3 < 1)
				{
					result3 = 1;
				}
			}
			if (args.Length > 3)
			{
				int.TryParse(args[3], out result4);
				if (result4 < 1)
				{
					result4 = 1;
				}
			}
			if (args.Length > 4)
			{
				int.TryParse(args[4], out result5);
				if (result5 < 1)
				{
					result5 = 1;
				}
			}
			text8 = ((args.Length <= 5) ? $"fsg sign {result2} {result3} {result4} {result5}" : string.Join(" ", args.Slice(5)));
			FiresideGatheringManager.Get().Cheat_ShowSign(outVal, result2, result3, result4, result5, text8);
			break;
		}
		case "view":
		{
			FSGConfig currentFSG = FiresideGatheringManager.Get().CurrentFSG;
			text4 = ((currentFSG != null) ? string.Format("Checked into FSG: [{0}] {1}\nStart w/ Slush: {2}\nEnd w/ Slush: {3}", currentFSG.FsgId, currentFSG.TavernName, TimeUtils.UnixTimeStampToDateTimeUtc(currentFSG.UnixStartTimeWithSlush).ToLocalTime().ToString("R"), TimeUtils.UnixTimeStampToDateTimeUtc(currentFSG.UnixEndTimeWithSlush).ToLocalTime().ToString("R")) : "No FSG currently checked in to.");
			text4 += "\n";
			string text10 = "No Data";
			ClientLocationData bestLocationData = ClientLocationManager.Get().GetBestLocationData();
			if (bestLocationData != null)
			{
				text10 = bestLocationData.ToString();
			}
			FiresideGatheringManager.Get().Cheat_GetGPSCheats(out var isCheatingGPS, out var latitude2, out var longitude2, out var offset);
			if (isCheatingGPS || offset != 0.0)
			{
				if (isCheatingGPS)
				{
					text10 += $"GPS overridden w/ [{latitude2}, {longitude2}]";
				}
				if (offset != 0.0)
				{
					text10 += $" offset={offset}";
				}
			}
			text4 += string.Format("FSG: {0} GPS: {1} WIFI: {2}\nClient Location Data:\n{3}", FiresideGatheringManager.IsFSGFeatureEnabled ? "enabled" : "disabled", FiresideGatheringManager.IsGpsFeatureEnabled ? "enabled" : "disabled", FiresideGatheringManager.IsWifiFeatureEnabled ? "enabled" : "disabled", text10);
			if (FiresideGatheringManager.Get().HasFSGToInnkeeperSetup)
			{
				FSGConfig fSGToInnkeeperSetup = FiresideGatheringManager.Get().FSGToInnkeeperSetup;
				if (!fSGToInnkeeperSetup.IsSetupComplete)
				{
					text4 += string.Format("Innkeeper of FSG: [{0}] {1}\nStart w/ Slush: {2}\nEnd w/ Slush: {3}", fSGToInnkeeperSetup.FsgId, fSGToInnkeeperSetup.TavernName, TimeUtils.UnixTimeStampToDateTimeUtc(fSGToInnkeeperSetup.UnixStartTimeWithSlush).ToLocalTime().ToString("R"), TimeUtils.UnixTimeStampToDateTimeUtc(fSGToInnkeeperSetup.UnixEndTimeWithSlush).ToLocalTime().ToString("R"));
				}
			}
			delay = 20f * Time.timeScale;
			break;
		}
		case "gps_offset":
		{
			double result6 = 0.0;
			if (args.Length > 1)
			{
				double.TryParse(args[1], out result6);
			}
			FiresideGatheringManager.Get().Cheat_GPSOffset(result6);
			text4 = "Set GPS Offset to: " + result6;
			break;
		}
		case "gps_set":
		{
			double result7 = 0.0;
			double result8 = 0.0;
			if (args.Length > 1)
			{
				double.TryParse(args[1], out result7);
			}
			if (args.Length > 2)
			{
				double.TryParse(args[2], out result8);
			}
			FiresideGatheringManager.Get().Cheat_GPSSet(result7, result8);
			text4 = $"Set GPS Set to: [{result7}, {result8}]";
			if (args.Length >= 4 && "find".Equals(args[3], StringComparison.InvariantCultureIgnoreCase))
			{
				FiresideGatheringManager.Get().PlayerAccountShouldAutoCheckin.Set(newValue: true);
				FiresideGatheringManager.Get().RequestNearbyFSGs();
			}
			break;
		}
		case "gps_reset":
			FiresideGatheringManager.Get().Cheat_ResetGPSCheating();
			text4 = "GPS cheats have been reset.";
			break;
		case "find":
			if (!FiresideGatheringManager.CanRequestNearbyFSG)
			{
				UIStatus.Get().AddError("Cannot make request for NearbyFSGs either because FSG is disabled or the location features are disabled for this player's country.", delay);
				return true;
			}
			if (args.Length > 1)
			{
				bool flag = false;
				double latitude = 0.0;
				double longitude = 0.0;
				if ("irvine".Equals(args[1], StringComparison.InvariantCultureIgnoreCase))
				{
					flag = true;
					latitude = 33.6578341;
					longitude = -117.7674501;
				}
				if (flag)
				{
					FiresideGatheringManager.Get().Cheat_GPSSet(latitude, longitude);
				}
			}
			FiresideGatheringManager.Get().PlayerAccountShouldAutoCheckin.Set(newValue: true);
			FiresideGatheringManager.Get().RequestNearbyFSGs();
			text4 = "RequestNearbyFSGs sent to server.";
			break;
		case "finalize":
			if (!FiresideGatheringManager.Get().HasFSGToInnkeeperSetup)
			{
				UIStatus.Get().AddError("There is no FSG to call InnkeeperSetup on - make sure there is an FSG you've created on the website with this Battle.net account.", delay);
				return true;
			}
			FiresideGatheringManager.Get().InnkeeperSetupFSG(provideWifiForTavern: true);
			text4 = "InnkeeperSetupFSG sent to server for FSG ID: " + FiresideGatheringManager.Get().FSGToInnkeeperSetup.FsgId;
			break;
		case "vars":
		{
			StringBuilder builder = new StringBuilder();
			int lines = 0;
			Action<string, object> action = delegate(string displayName, object value)
			{
				if (lines != 0)
				{
					builder.Append("\n");
				}
				int num = displayName.LastIndexOf('.');
				if (num >= 0 && num < displayName.Length - 1)
				{
					displayName = displayName.Substring(num + 1);
				}
				builder.AppendFormat("{0}={1}", displayName, value);
				lines++;
			};
			string[] array = new string[3] { "Location.Latitude", "Location.Longitude", "Location.BSSID" };
			foreach (string text6 in array)
			{
				VarKey varKey = Vars.Key(text6);
				if (varKey.HasValue)
				{
					action(text6, varKey.GetStr("null"));
				}
			}
			NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
			action("FSGEnabled", netObject.FSGEnabled);
			action("AutoCheckin", FiresideGatheringManager.Get().AutoCheckInEnabled);
			action("LoginScan", netObject.FSGLoginScanEnabled);
			action("MaxPubscribedPatrons", netObject.FsgMaxPresencePubscribedPatronCount);
			action("PatronCountLimit", FiresideGatheringManager.Get().FriendListPatronCountLimit);
			action("FSG.PeriodicPrunePatronOldSubscriptionsSeconds", FiresideGatheringPresenceManager.PERIODIC_SUBSCRIBE_CHECK_SECONDS + "s");
			action("FSG.PatronOldSubscriptionThresholdSeconds", FiresideGatheringPresenceManager.PATRON_OLD_SUBSCRIPTION_THRESHOLD_SECONDS + "s");
			action("FSG.PresenceSubscriptionsVerboseLog", FiresideGatheringPresenceManager.IsVerboseLogging);
			action("ToScreen", FiresideGatheringPresenceManager.IsVerboseLoggingToScreen);
			string text7 = builder.ToString();
			Log.All.Print(text7);
			text4 = text7.Replace("\n", ", ") + "\n";
			delay = Mathf.Min(30f, 5f * (float)lines) * Time.timeScale;
			break;
		}
		case "player":
		{
			StringBuilder builder2 = new StringBuilder();
			int lines2 = 0;
			Action<string, object> obj = delegate(string displayName, object value)
			{
				if (lines2 != 0)
				{
					builder2.Append("\n");
				}
				builder2.AppendFormat("{0}={1}", displayName, value);
				lines2++;
			};
			obj(EnumUtils.GetString(Option.SHOULD_AUTO_CHECK_IN_TO_FIRESIDE_GATHERINGS), FiresideGatheringManager.Get().PlayerAccountShouldAutoCheckin);
			obj(EnumUtils.GetString(Option.HAS_INITIATED_FIRESIDE_GATHERING_SCAN), FiresideGatheringManager.Get().HasManuallyInitiatedFSGScanBefore);
			obj(EnumUtils.GetString(Option.LAST_TAVERN_JOINED), FiresideGatheringManager.Get().LastTavernID);
			string text5 = builder2.ToString();
			Log.All.Print(text5);
			text4 = text5.Replace("\n", ", ") + "\n";
			delay = Mathf.Min(30f, 5f * (float)lines2) * Time.timeScale;
			break;
		}
		case "refreshpatrons":
			Network.Get().RequestFSGPatronListUpdate();
			break;
		case "returntooltip":
			FiresideGatheringManager.Get().HasSeenReturnToFSGSceneTooltip = false;
			FiresideGatheringManager.Get().ShowReturnToFSGSceneTooltip();
			break;
		}
		if (text4 != null)
		{
			UIStatus.Get().AddInfo(text4, delay);
		}
		return true;
	}

	private bool OnProcessCheat_GPS(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: gps [cmd] [args]\nCommands: on/off";
		if (args.Length < 1 || args.Any((string a) => string.IsNullOrEmpty(a)))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		string text = args[0];
		string text2 = null;
		switch (text)
		{
		case "off":
		case "on":
			ClientLocationManager.Get().Cheat_SetGPSEnabled(text == "on");
			text2 = "GPS turned " + text;
			break;
		case "view":
		{
			double num = 0.0;
			double num2 = 0.0;
			GpsCoordinate location = ClientLocationManager.Get().GetBestLocationData().location;
			if (location != null)
			{
				num = location.Latitude;
				num2 = location.Longitude;
			}
			text2 = string.Format("GPS Services: {0}\nLatitude: {1}\nLongitude: {2}", ClientLocationManager.Get().GPSServicesEnabled ? "enabled" : "disabled", num, num2);
			break;
		}
		}
		if (text2 != null)
		{
			UIStatus.Get().AddInfo(text2, 5f);
		}
		return true;
	}

	private bool OnProcessCheat_Wifi(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: wifi [cmd] [args]\nCommands: on/off";
		if (args.Length < 1 || args.Any((string a) => string.IsNullOrEmpty(a)))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		string text = args[0];
		string text2 = null;
		switch (text)
		{
		case "off":
		case "on":
			ClientLocationManager.Get().Cheat_SetWifiEnabled(text == "on");
			text2 = "WIFI turned " + text;
			break;
		case "view":
			text2 = string.Format("WIFI Services: {0}", ClientLocationManager.Get().WifiEnabled ? "enabled" : "disabled");
			break;
		}
		if (text2 != null)
		{
			UIStatus.Get().AddInfo(text2, 5f);
		}
		return true;
	}

	private bool OnProcessCheat_utilservercmd(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string[] values = new string[15]
		{
			"help", "tb", "fsg", "arena", "ranked", "deck", "banner", "quest", "achieves", "prog",
			"setgsd", "returningplayer", "curl", "coin", "reward"
		};
		if (args.Length < 1 || string.IsNullOrEmpty(rawArgs))
		{
			if (autofillData != null)
			{
				return ProcessAutofillParam(values, string.Empty, autofillData);
			}
			UIStatus.Get().AddError("Must specify a sub-command.");
			return true;
		}
		string cmd = args[0].ToLower();
		string[] cmdArgs = args.Skip(1).ToArray();
		string text = ((cmdArgs.Length == 0) ? null : cmdArgs[0].ToLower());
		bool flag = cmd == "fsg" && text == "tb";
		if (autofillData != null && args.Length == 1 && !rawArgs.EndsWith(" "))
		{
			string searchTerm = cmd;
			return ProcessAutofillParam(values, searchTerm, autofillData);
		}
		bool requiresConfirm = true;
		switch (cmd)
		{
		case "help":
			requiresConfirm = false;
			break;
		case "tb":
		case "fsg":
		{
			int num2 = 1;
			if (flag)
			{
				text = ((cmdArgs.Length < 2) ? null : cmdArgs[1].ToLower());
				num2 = 2;
			}
			if (autofillData != null)
			{
				bool flag2 = rawArgs.EndsWith(" ") && cmdArgs.Length == (flag ? 1 : 0);
				if ((cmd == "tb" || flag) && (flag2 || cmdArgs.Length == ((!flag) ? 1 : 2)))
				{
					string[] values8 = "view, list, season, scenario, end_offset, start_offset, active, dormant, ticket, reset_ticket, reset, wins, losses, reward".Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
					return ProcessAutofillParam(values8, text, autofillData);
				}
				if (cmd == "fsg" && (flag2 || cmdArgs.Length == 1))
				{
					string[] values9 = "config, setconfig, tb, find, finalize, checkin, checkout, patrons".Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
					return ProcessAutofillParam(values9, text, autofillData);
				}
				return false;
			}
			switch (text)
			{
			case "help":
			case "view":
			case "list":
				requiresConfirm = false;
				break;
			case "reset":
				requiresConfirm = ((cmdArgs.Length < num2 + 1) ? null : cmdArgs[num2].ToLower()) != "help";
				break;
			}
			break;
		}
		case "arena":
			requiresConfirm = false;
			text = ((cmdArgs.Length < 2) ? null : cmdArgs[1].ToLower());
			if (autofillData != null)
			{
				if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
				{
					string[] values7 = "view_player, reward, ticket, set, view, list, season, scenario, end_offset, start_offset, active, dormant, choices".Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
					return ProcessAutofillParam(values7, text, autofillData);
				}
				return false;
			}
			if (text == "reward" && !cmdArgs.Any((string arg) => "justids".Equals(arg)))
			{
				List<string> list2 = cmdArgs.ToList();
				list2.Add("justids");
				cmdArgs = list2.ToArray();
			}
			break;
		case "ranked":
			requiresConfirm = false;
			if (autofillData != null)
			{
				if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
				{
					string[] values10 = "view, season, set, reward, medal, win, lose, games, seasonroll, seasonreset".Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
					return ProcessAutofillParam(values10, text, autofillData);
				}
				return false;
			}
			if (text == "seasonroll" || text == "seasonreset")
			{
				requiresConfirm = true;
			}
			break;
		case "deck":
			if (autofillData != null)
			{
				if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
				{
					string[] values5 = "view, test".Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
					return ProcessAutofillParam(values5, text, autofillData);
				}
				return false;
			}
			if (!(text == "view"))
			{
				if (text == "test")
				{
					requiresConfirm = false;
				}
				break;
			}
			requiresConfirm = false;
			if (!cmdArgs.Any((string arg) => arg.StartsWith("details=", StringComparison.InvariantCultureIgnoreCase)))
			{
				List<string> list = new List<string>(cmdArgs);
				list.Add("details=0");
				cmdArgs = list.ToArray();
			}
			break;
		case "banner":
			requiresConfirm = false;
			if (autofillData != null)
			{
				if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
				{
					string[] values4 = "list, reset".Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
					return ProcessAutofillParam(values4, text, autofillData);
				}
				return false;
			}
			if (string.IsNullOrEmpty(text) || text == "help")
			{
				UIStatus.Get().AddInfo("Usage: util banner <list | reset bannerId=#>\n\nClear seen banners (wooden signs at login) with IDs >= bannerId arg. If no parameters, clears out just latest known bannerId. If bannerId=0, all seen banners are cleared.", 5f);
				return true;
			}
			if (text == "list")
			{
				Cheat_ShowBannerList();
				return true;
			}
			break;
		case "prog":
		{
			bool autoFillResult = false;
			if (ProcessAutofillParam_util_prog(rawArgs, cmdArgs, autofillData, ref autoFillResult, ref requiresConfirm))
			{
				return autoFillResult;
			}
			break;
		}
		case "quest":
		case "achieves":
		{
			requiresConfirm = false;
			if (cmd == "quest")
			{
				cmd = "achieves";
			}
			if (autofillData != null)
			{
				if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
				{
					string[] values3 = "cancel, resetdaily, resetreroll, grant, complete, progress, seasonroll, seasonreset".Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
					return ProcessAutofillParam(values3, text, autofillData);
				}
				return false;
			}
			OnProcessCheat_util_achieves_ReplaceSlotWithAchieve(cmdArgs);
			int num = OnProcessCheat_util_achieves_GetAchievementFromArgs(cmdArgs);
			if (text == "grant")
			{
				Achievement achievement = AchieveManager.Get().GetAchievement(num);
				if (achievement != null && AchieveManager.Get().GetActiveQuests().Count >= 3 && achievement.CanShowInQuestLog)
				{
					UIStatus.Get().AddInfo($"{func} {cmd}: Quest log is full.", 5f);
					return true;
				}
			}
			switch (text)
			{
			case "cancel":
				OnProcessCheat_util_achieves_ShowQuestLog();
				break;
			case "grant":
			case "complete":
			case "progress":
				OnProcessCheat_util_achieves_ShowQuestPopupsWhenAchieveUpdated(num);
				break;
			default:
				UIStatus.Get().AddInfo("USAGE: quest [subcmd] [subcmd args]\nCommands: grant, complete, progress, cancel, resetdaily\n Subcommands: achieve=[achieveId] (required for grant), slot=[slot#] (Either achieveId or slot required for complete, progress, cancel), amount=[X] (for progress only- optional), offset=[X] (in hours from current time, for resetdaily and resetreroll", 10f);
				return true;
			case "resetdaily":
			case "resetreroll":
				break;
			}
			break;
		}
		case "curl":
		case "grant":
		case "getgsd":
		case "setgsd":
			requiresConfirm = false;
			break;
		case "returningplayer":
			requiresConfirm = false;
			if (autofillData != null)
			{
				if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
				{
					string[] values6 = "start, test_group, optout, complete, reset".Split(new char[2] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
					return ProcessAutofillParam(values6, text, autofillData);
				}
				return false;
			}
			break;
		case "hero":
			requiresConfirm = false;
			break;
		case "logrelay":
			if (string.IsNullOrEmpty(text))
			{
				UIStatus.Get().AddInfo("USAGE: logrelay [logName]", 10f);
				return true;
			}
			requiresConfirm = false;
			break;
		case "coin":
			requiresConfirm = false;
			if (text == "quickfavorite")
			{
				string text2 = args.FirstOrDefault((string x) => x.StartsWith("id="));
				int newFavoriteCoinID = 1;
				if (text2 != null)
				{
					newFavoriteCoinID = Convert.ToInt32(text2.Substring("id=".Length));
				}
				CoinManager.Get().RequestSetFavoriteCoin(newFavoriteCoinID);
				return true;
			}
			break;
		case "reward":
			requiresConfirm = false;
			if (autofillData != null)
			{
				if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
				{
					string[] values2 = new string[12]
					{
						"grantlist", "grantitem", "gold", "dust", "orbs", "booster", "card", "randomcard", "tavernticket", "cardback",
						"heroskin", "customcoin"
					};
					return ProcessAutofillParam(values2, text, autofillData);
				}
				return false;
			}
			break;
		}
		if (autofillData != null)
		{
			return false;
		}
		AlertPopup.ResponseCallback responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			if (response == AlertPopup.Response.CONFIRM || response == AlertPopup.Response.OK)
			{
				DebugCommandRequest debugCommandRequest = new DebugCommandRequest();
				debugCommandRequest.Command = cmd;
				debugCommandRequest.Args.AddRange(cmdArgs);
				Network.Get().SendDebugCommandRequest(debugCommandRequest);
			}
		};
		m_lastUtilServerCmd = args;
		if (requiresConfirm)
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = "Run UTIL server command?";
			popupInfo.m_text = "You are about to run a UTIL Server command - this may affect other players on this environment and possibly change configuration on this environment.\n\nPlease confirm you want to do this.";
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = responseCallback;
			DialogManager.Get().ShowPopup(popupInfo);
		}
		else
		{
			responseCallback(AlertPopup.Response.OK, null);
		}
		return true;
	}

	private void OnProcessCheat_utilservercmd_OnResponse()
	{
		DebugCommandResponse debugCommandResponse = Network.Get().GetDebugCommandResponse();
		bool flag = false;
		string text = "null response";
		string text2 = ((m_lastUtilServerCmd == null || m_lastUtilServerCmd.Length == 0) ? "" : m_lastUtilServerCmd[0]);
		string[] array = ((m_lastUtilServerCmd == null) ? new string[0] : m_lastUtilServerCmd.Skip(1).ToArray());
		string text3 = ((array.Length == 0) ? null : array[0]);
		string text4 = ((array.Length < 2) ? null : array[1].ToLower());
		m_lastUtilServerCmd = null;
		if (debugCommandResponse != null)
		{
			flag = debugCommandResponse.Success;
			text = string.Format("{0} {1}", debugCommandResponse.Success ? "" : "FAILED:", debugCommandResponse.HasResponse ? debugCommandResponse.Response : "reply=<blank>");
		}
		Log.LogLevel level = (flag ? Log.LogLevel.Info : Log.LogLevel.Error);
		Log.Net.Print(level, text);
		bool flag2 = true;
		float delay = 5f;
		if (flag)
		{
			bool flag3 = text2 == "fsg" && text3 == "tb";
			if (text2 == "tb" || flag3)
			{
				if (flag3)
				{
					text3 = text4;
					text4 = ((array.Length < 3) ? null : array[2].ToLower());
				}
				switch (text3)
				{
				case "reset":
					if (!(text4 != "help"))
					{
						break;
					}
					goto case "scenario";
				case "scenario":
				case "scen":
				case "season":
				case "end_offset":
				case "start_offset":
				case "wins":
				case "losses":
				case "ticket":
				{
					for (BrawlType brawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL; brawlType < BrawlType.BRAWL_TYPE_COUNT; brawlType++)
					{
						TavernBrawlManager.Get().RefreshServerData(brawlType);
					}
					break;
				}
				}
			}
			else
			{
				switch (text2)
				{
				case "ranked":
					if (text3 == "medal" || text3 == "seasonroll")
					{
						flag = flag && (!debugCommandResponse.HasResponse || !debugCommandResponse.Response.StartsWith("Error"));
						if (flag)
						{
							text = "Success";
							delay = 0.5f;
						}
						else if (debugCommandResponse.HasResponse)
						{
							text = debugCommandResponse.Response;
						}
					}
					switch (text3)
					{
					case "set":
					case "win":
					case "lose":
					case "games":
						NetCache.Get().RefreshNetObject<NetCache.NetCacheMedalInfo>();
						break;
					}
					break;
				case "hero":
					if (text3 == "addxp")
					{
						NetCache.Get().RefreshNetObject<NetCache.NetCacheHeroLevels>();
					}
					break;
				case "banner":
				{
					if (!(text3 == "reset"))
					{
						break;
					}
					NetCache.Get().ReloadNetObject<NetCache.NetCacheProfileProgress>();
					bool flag4 = false;
					int result = 0;
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string[] array3 = array2[i]?.Split('=');
						if (array3 != null && array3.Length >= 2 && (array3[0].Equals("banner", StringComparison.InvariantCultureIgnoreCase) || array3[0].Equals("bannerId", StringComparison.InvariantCultureIgnoreCase)))
						{
							flag4 = true;
							int.TryParse(array3[1], out result);
						}
					}
					if (flag4)
					{
						BannerManager.Get().Cheat_ClearSeenBannersNewerThan(result);
					}
					else
					{
						BannerManager.Get().Cheat_ClearSeenBanners();
					}
					break;
				}
				case "returningplayer":
					flag = flag && (!debugCommandResponse.HasResponse || !debugCommandResponse.Response.StartsWith("Error"));
					if (flag)
					{
						ReturningPlayerMgr.Get().Cheat_ResetReturningPlayer();
						if (true)
						{
							text += "\nYou may want to log out/in to take effect.";
						}
					}
					break;
				case "logrelay":
					if (text3 == "*")
					{
						flag2 = false;
					}
					break;
				}
			}
			if ((text2 == "ranked" || text2 == "arena") && text3 == "reward")
			{
				flag = flag && (!debugCommandResponse.HasResponse || !debugCommandResponse.Response.StartsWith("Error"));
				if (flag)
				{
					text = Cheat_ShowRewardBoxes(text);
					if (text == null)
					{
						delay = 0.5f;
						text = "Success";
					}
					else
					{
						flag = false;
					}
				}
			}
			if (text2 == "arena" && text3 == "season")
			{
				DraftManager.Get().RefreshCurrentSeasonFromServer();
			}
		}
		if (flag2)
		{
			if (flag)
			{
				UIStatus.Get().AddInfo(text, delay);
			}
			else
			{
				UIStatus.Get().AddError(text);
			}
		}
	}

	private int OnProcessCheat_util_achieves_GetAchievementFromArgs(string[] args)
	{
		string text = args.FirstOrDefault((string x) => x.StartsWith("achieve="));
		if (text != null)
		{
			return Convert.ToInt32(text.Substring("achieve=".Length));
		}
		return 0;
	}

	private int OnProcessCheat_util_achieves_GetAchieveFromSlotId(int slotId)
	{
		List<Achievement> activeQuests = AchieveManager.Get().GetActiveQuests();
		if (slotId > 0 && slotId <= activeQuests.Count)
		{
			return activeQuests[slotId - 1].ID;
		}
		return 0;
	}

	private void OnProcessCheat_util_achieves_ReplaceSlotWithAchieve(string[] args)
	{
		for (int i = 0; i < args.Length; i++)
		{
			if (args[i].StartsWith("slot=", ignoreCase: true, CultureInfo.CurrentCulture))
			{
				int slotId = Convert.ToInt32(args[i].Substring("slot=".Length));
				int num = OnProcessCheat_util_achieves_GetAchieveFromSlotId(slotId);
				args[i] = $"achieve={num}";
			}
		}
	}

	private void OnProcessCheat_util_achieves_ShowQuestPopupsWhenAchieveUpdated(int achieveId)
	{
		AchieveManager.AchievesUpdatedCallback action = null;
		AchieveManager.Get().RegisterAchievesUpdatedListener(action = delegate(List<Achievement> updatedAchieves, List<Achievement> completedAchieves, object userdata)
		{
			if (achieveId == 0 || updatedAchieves.Any((Achievement x) => x.ID == achieveId) || completedAchieves.Any((Achievement x) => x.ID == achieveId))
			{
				if (AchieveManager.Get().HasQuestsToShow(onlyNewlyActive: true))
				{
					WelcomeQuests.Show(UserAttentionBlocker.ALL, fromLogin: true);
				}
				else if (GameToastMgr.Get() != null)
				{
					GameToastMgr.Get().UpdateQuestProgressToasts();
				}
				AchieveManager.Get().RemoveAchievesUpdatedListener(action);
			}
		});
	}

	private void OnProcessCheat_util_achieves_ShowQuestLog()
	{
		if (QuestLog.Get() != null && !QuestLog.Get().IsShown())
		{
			QuestLog.Get().Show();
		}
	}

	private bool ProcessAutofillParam_util_prog(string rawArgs, string[] cmdArgs, AutofillData autofillData, ref bool autoFillResult, ref bool requiresConfirm)
	{
		requiresConfirm = false;
		if (autofillData == null)
		{
			return false;
		}
		if (cmdArgs.Length > 2)
		{
			return false;
		}
		string text = ((cmdArgs.Length < 1) ? null : cmdArgs[0].ToLower());
		string text2 = ((cmdArgs.Length < 2) ? null : cmdArgs[1].ToLower());
		bool flag = rawArgs.EndsWith(" ");
		if ((text == null && flag) || (text != null && text2 == null && !flag))
		{
			string[] values = new string[4] { "quest", "pool", "achieve", "track" };
			autoFillResult = ProcessAutofillParam(values, text, autofillData);
			return true;
		}
		if (text == null)
		{
			return false;
		}
		if (((text2 == null && flag) || (text2 != null && !flag)) && text == "quest")
		{
			string[] values2 = new string[7] { "help", "view", "grant", "ack", "advance", "complete", "reset" };
			autoFillResult = ProcessAutofillParam(values2, text2, autofillData);
			return true;
		}
		if (((text2 == null && flag) || (text2 != null && !flag)) && text == "pool")
		{
			string[] values3 = new string[10] { "help", "view", "grant", "login", "lastcheckdate", "reroll", "reset", "set", "testcalcnumquests", "testcalctimeuntil" };
			autoFillResult = ProcessAutofillParam(values3, text2, autofillData);
			return true;
		}
		if (((text2 == null && flag) || (text2 != null && !flag)) && text == "achieve")
		{
			string[] values4 = new string[8] { "help", "view", "score", "advance", "complete", "claim", "ack", "reset" };
			autoFillResult = ProcessAutofillParam(values4, text2, autofillData);
			return true;
		}
		if (((text2 == null && flag) || (text2 != null && !flag)) && text == "track")
		{
			string[] values5 = new string[9] { "help", "view", "set", "gamexp", "addxp", "levelup", "claim", "ack", "reset" };
			autoFillResult = ProcessAutofillParam(values5, text2, autofillData);
			return true;
		}
		return false;
	}

	private static string Cheat_ShowRewardBoxes(string parsableRewardBags)
	{
		if (SceneMgr.Get().IsInGame())
		{
			return "Cannot display reward boxes in gameplay.";
		}
		string[] array = parsableRewardBags.Trim().Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		if (array.Length < 2)
		{
			return "Error parsing reply, should start with 'Success:' then player_id: " + parsableRewardBags;
		}
		if (array.Length < 3)
		{
			return "No rewards returned by server: reply=" + parsableRewardBags;
		}
		List<NetCache.ProfileNotice> list = new List<NetCache.ProfileNotice>();
		array = array.Skip(1).ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			int result = 0;
			int num = i * 2;
			if (num >= array.Length)
			{
				break;
			}
			if (!int.TryParse(array[num], out result))
			{
				return "Reward at index " + num + " (" + array[num] + ") is not an int: reply=" + parsableRewardBags;
			}
			if (result != 0)
			{
				num++;
				if (num >= array.Length)
				{
					return "No reward bag data at index " + num + ": reply=" + parsableRewardBags;
				}
				long result2 = 0L;
				if (!long.TryParse(array[num], out result2))
				{
					return "Reward Data at index " + num + " (" + array[num] + ") is not a long int: reply=" + parsableRewardBags;
				}
				NetCache.ProfileNotice profileNotice = null;
				TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
				switch (result)
				{
				case 1:
				case 12:
				case 14:
				case 15:
				case 24:
					profileNotice = new NetCache.ProfileNoticeRewardBooster
					{
						Id = (int)result2,
						Count = 1
					};
					break;
				case 2:
					profileNotice = new NetCache.ProfileNoticeRewardCurrency
					{
						CurrencyType = PegasusShared.CurrencyType.CURRENCY_TYPE_GOLD,
						Amount = (int)result2
					};
					break;
				case 3:
					profileNotice = new NetCache.ProfileNoticeRewardDust
					{
						Amount = (int)result2
					};
					break;
				case 8:
				case 9:
				case 10:
				case 11:
					premium = TAG_PREMIUM.GOLDEN;
					goto case 4;
				case 4:
				case 5:
				case 6:
				case 7:
					profileNotice = new NetCache.ProfileNoticeRewardCard
					{
						CardID = GameUtils.TranslateDbIdToCardId((int)result2),
						Premium = premium
					};
					break;
				case 13:
					profileNotice = new NetCache.ProfileNoticeRewardCardBack
					{
						CardBackID = (int)result2
					};
					break;
				default:
					Debug.LogError("Unknown Reward Bag Type: " + result + " (data=" + result2 + ") at index " + num + ": reply=" + parsableRewardBags);
					break;
				}
				if (profileNotice != null)
				{
					list.Add(profileNotice);
				}
			}
		}
		RewardBoxesDisplay rewardBoxesDisplay = UnityEngine.Object.FindObjectOfType<RewardBoxesDisplay>();
		if (rewardBoxesDisplay != null)
		{
			float secondsToWait = 0f;
			if (rewardBoxesDisplay.IsClosing)
			{
				secondsToWait = 0.1f;
			}
			else
			{
				rewardBoxesDisplay.Close();
			}
			Processor.ScheduleCallback(secondsToWait, realTime: false, delegate
			{
				Cheat_ShowRewardBoxes(parsableRewardBags);
			});
			return null;
		}
		List<RewardData> rewards = RewardUtils.GetRewards(list);
		PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
		{
			RewardBoxesDisplay component = go.GetComponent<RewardBoxesDisplay>();
			component.SetRewards(callbackData as List<RewardData>);
			component.m_Root.transform.position = (UniversalInputManager.UsePhoneUI ? new Vector3(0f, 14.7f, 3f) : new Vector3(0f, 131.2f, -3.2f));
			if (Box.Get() != null && Box.Get().GetBoxCamera() != null && Box.Get().GetBoxCamera().GetState() == BoxCamera.State.OPENED)
			{
				component.m_Root.transform.position += new Vector3(-3f, 0f, 4.6f);
				if ((bool)UniversalInputManager.UsePhoneUI)
				{
					component.m_Root.transform.position += new Vector3(0f, 0f, -7f);
				}
				else
				{
					component.transform.localScale = Vector3.one * 0.6f;
				}
			}
			component.AnimateRewards();
		};
		AssetLoader.Get().InstantiatePrefab("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback, rewards);
		return null;
	}

	private bool OnProcessCheat_gameservercmd(string func, string[] args, string rawArgs)
	{
		return true;
	}

	private bool OnProcessCheat_rewardboxes(string func, string[] args, string rawArgs)
	{
		string.IsNullOrEmpty(args[0].ToLower());
		int val = 5;
		if (args.Length > 1)
		{
			GeneralUtils.TryParseInt(args[1], out val);
		}
		BoosterDbId boosterDbId = BoosterDbId.THE_GRAND_TOURNAMENT;
		BoosterDbId[] array = (from BoosterDbId i in Enum.GetValues(typeof(BoosterDbId))
			where i != BoosterDbId.INVALID
			select i).ToArray();
		boosterDbId = array[UnityEngine.Random.Range(0, array.Length)];
		string text = Cheat_ShowRewardBoxes(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(string.Concat("Success: 123456" + " " + 13, " ", UnityEngine.Random.Range(1, 34)), " ", 1), " ", (int)boosterDbId), " ", 3), " ", UnityEngine.Random.Range(1, 31) * 5), " ", 2), " ", UnityEngine.Random.Range(1, 31) * 5), " ", (UnityEngine.Random.Range(0, 2) == 0) ? 6 : 10), " ", GameUtils.TranslateCardIdToDbId("EX1_279")));
		if (text != null)
		{
			UIStatus.Get().AddError(text);
		}
		return true;
	}

	private bool OnProcessCheat_rankrefresh(string func, string[] args, string rawArgs)
	{
		NetCache.Get().RegisterScreenEndOfGame(OnNetCacheReady_CallRankChangeTwoScoopDebugShow);
		return true;
	}

	private void OnNetCacheReady_CallRankChangeTwoScoopDebugShow()
	{
		NetCache.Get().UnregisterNetCacheHandler(OnNetCacheReady_CallRankChangeTwoScoopDebugShow);
		RankChangeTwoScoop_NEW.DebugShowHelper(RankMgr.Get().GetLocalPlayerMedalInfo(), Options.GetFormatType());
	}

	private bool OnProcessCheat_rankchange(string func, string[] args, string rawArgs)
	{
		string cheatName = "bronze10";
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
		{
			cheatName = args[0];
		}
		LeagueRankDbfRecord leagueRankRecordByCheatName = RankMgr.Get().GetLeagueRankRecordByCheatName(cheatName);
		if (leagueRankRecordByCheatName == null)
		{
			return false;
		}
		FormatType formatType = FormatType.FT_STANDARD;
		bool isWinStreak = false;
		int stars = 0;
		int starsPerWin = 1;
		bool showWin = true;
		for (int i = 0; i < args.Length; i++)
		{
			string text = args[i].ToLower();
			switch (text)
			{
			case "winstreak":
			case "streak":
				isWinStreak = true;
				continue;
			case "win":
				showWin = true;
				continue;
			case "loss":
				showWin = false;
				continue;
			case "wild":
				formatType = FormatType.FT_WILD;
				continue;
			case "classic":
				formatType = FormatType.FT_CLASSIC;
				continue;
			}
			if (text.StartsWith("x") || text.EndsWith("x"))
			{
				text = text.Trim('x');
				starsPerWin = int.Parse(text);
			}
			else if (text.StartsWith("*") || text.EndsWith("*"))
			{
				text = text.Trim('*');
				stars = int.Parse(text);
			}
		}
		RankChangeTwoScoop_NEW.DebugShowFake(leagueRankRecordByCheatName.LeagueId, leagueRankRecordByCheatName.StarLevel, stars, starsPerWin, formatType, isWinStreak, showWin);
		return true;
	}

	private bool OnProcessCheat_rankreward(string func, string[] args, string rawArgs)
	{
		string cheatName = "bronze5";
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
		{
			cheatName = args[0];
		}
		LeagueRankDbfRecord leagueRankRecordByCheatName = RankMgr.Get().GetLeagueRankRecordByCheatName(cheatName);
		if (leagueRankRecordByCheatName == null)
		{
			return false;
		}
		FormatType formatType = FormatType.FT_STANDARD;
		bool flag = false;
		for (int i = 0; i < args.Length; i++)
		{
			switch (args[i].ToLower())
			{
			case "standard":
				formatType = FormatType.FT_STANDARD;
				break;
			case "wild":
				formatType = FormatType.FT_WILD;
				break;
			case "classic":
				formatType = FormatType.FT_CLASSIC;
				break;
			case "all":
				flag = true;
				break;
			}
		}
		MedalInfoTranslator medalInfoTranslator = MedalInfoTranslator.CreateMedalInfoForLeagueId(leagueRankRecordByCheatName.LeagueId, leagueRankRecordByCheatName.StarLevel, 1337);
		medalInfoTranslator.GetPreviousMedal(formatType).starLevel = (flag ? 1 : (leagueRankRecordByCheatName.StarLevel - 1));
		TranslatedMedalInfo currentMedal = medalInfoTranslator.GetCurrentMedal(formatType);
		currentMedal.bestStarLevel = leagueRankRecordByCheatName.StarLevel;
		NetCache.NetCacheRewardProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>();
		if (netObject != null)
		{
			currentMedal.seasonId = netObject.Season;
		}
		List<List<RewardData>> rewardsEarned = new List<List<RewardData>>();
		if (!medalInfoTranslator.GetRankedRewardsEarned(formatType, ref rewardsEarned) || rewardsEarned.Count == 0)
		{
			return false;
		}
		RankedRewardDisplay.DebugShowFake(leagueRankRecordByCheatName.LeagueId, leagueRankRecordByCheatName.StarLevel, formatType, rewardsEarned);
		return true;
	}

	private bool OnProcessCheat_rankcardback(string func, string[] args, string rawArgs)
	{
		string cheatName = "bronze10";
		LeagueRankDbfRecord leagueRankRecordByCheatName = RankMgr.Get().GetLeagueRankRecordByCheatName(cheatName);
		if (leagueRankRecordByCheatName == null)
		{
			return false;
		}
		int val = 0;
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]) && !GeneralUtils.TryParseInt(args[0], out val))
		{
			UIStatus.Get().AddInfo("please enter a valid int value for 1st parameter <wins>");
			return true;
		}
		int val2 = 0;
		if (args.Length >= 2 && !GeneralUtils.TryParseInt(args[1], out val2))
		{
			UIStatus.Get().AddInfo("please enter a valid int value for 2nd parameter <season_id>");
			return true;
		}
		if (val2 == 0)
		{
			NetCache.NetCacheRewardProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>();
			if (netObject != null)
			{
				val2 = netObject.Season;
			}
		}
		MedalInfoTranslator medalInfoTranslator = MedalInfoTranslator.CreateMedalInfoForLeagueId(leagueRankRecordByCheatName.LeagueId, leagueRankRecordByCheatName.StarLevel, 1337);
		TranslatedMedalInfo previousMedal = medalInfoTranslator.GetPreviousMedal(FormatType.FT_STANDARD);
		TranslatedMedalInfo currentMedal = medalInfoTranslator.GetCurrentMedal(FormatType.FT_STANDARD);
		previousMedal.seasonWins = Mathf.Max(0, val - 1);
		currentMedal.seasonWins = val;
		currentMedal.seasonId = val2;
		RankedCardBackProgressDisplay.DebugShowFake(medalInfoTranslator);
		return true;
	}

	private bool OnProcessCheat_easyrank(string func, string[] args, string rawArgs)
	{
		string arg = args[0].ToLower();
		CheatMgr.Get().ProcessCheat($"util ranked set rank={arg}");
		return true;
	}

	private bool OnProcessCheat_timescale(string func, string[] args, string rawArgs)
	{
		string text = args[0].ToLower();
		if (string.IsNullOrEmpty(text))
		{
			float timeScale = Time.timeScale;
			float devTimescaleMultiplier = SceneDebugger.GetDevTimescaleMultiplier();
			string message = ((timeScale != devTimescaleMultiplier) ? $"Current timeScale is: {timeScale}\nDev timescale: {devTimescaleMultiplier}\nGame timescale: {TimeScaleMgr.Get().GetGameTimeScale()}" : $"Current timeScale is: {timeScale}");
			UIStatus.Get().AddInfo(message, 3f * SceneDebugger.GetDevTimescaleMultiplier());
			return true;
		}
		float result = 1f;
		if (!float.TryParse(text, out result))
		{
			return false;
		}
		SceneDebugger.SetDevTimescaleMultiplier(result);
		UIStatus.Get().AddInfo($"Setting timescale to: {result}", 3f * result);
		return true;
	}

	private bool OnProcessCheat_reset(string func, string[] args, string rawArgs)
	{
		HearthstoneApplication.Get().Reset();
		return true;
	}

	private bool OnProcessCheat_endturn(string func, string[] args, string rawArgs)
	{
		UIStatus.Get().AddError("Deprecated. Use \"cheat endturn\" instead.");
		return true;
	}

	private bool OnProcessCheat_battlegrounds(string func, string[] args, string rawArgs)
	{
		if (SceneMgr.Get().IsInGame())
		{
			UIStatus.Get().AddError("Cannot queue for a battlegrounds game while in gameplay.");
			return true;
		}
		if (DialogManager.Get().ShowingDialog())
		{
			UIStatus.Get().AddError("Cannot queue for a battlegrounds game while a dialog is active.");
			return true;
		}
		GameMgr.Get().FindGame(GameType.GT_BATTLEGROUNDS, FormatType.FT_WILD, 3459, 0, 0L);
		return true;
	}

	private bool OnProcessCheat_scenario(string func, string[] args, string rawArgs)
	{
		string[] array = new string[5] { "id", "game_type", "deck_id", "format_type", "prog_override" };
		Map<string, NamedParam> values;
		bool flag = TryParseNamedArgs(args, out values);
		for (int i = 0; i < array.Length; i++)
		{
			string key = array[i];
			if (!values.TryGetValue(key, out var value))
			{
				if (!flag && args.Length > i)
				{
					values.Add(key, new NamedParam(args[i]));
					continue;
				}
				Map<string, NamedParam> map = values;
				value = default(NamedParam);
				map.Add(key, value);
			}
		}
		NamedParam namedParam = values["id"];
		int num = 260;
		if (namedParam.HasNumber)
		{
			num = namedParam.Number;
			if (GameDbf.Scenario.GetRecord(num) == null)
			{
				Error.AddWarning("scenario Cheat Error", "Error reading a scenario id from \"{0}\"", num);
				return false;
			}
		}
		NamedParam namedParam2 = values["game_type"];
		GameType gameType = GameType.GT_VS_AI;
		if (namedParam2.HasNumber)
		{
			gameType = (GameType)namedParam2.Number;
			if (gameType == GameType.GT_UNKNOWN)
			{
				Error.AddWarning("scenario Cheat Error", "Error reading a game type from \"{0}\"", gameType);
				return false;
			}
		}
		NamedParam deckParam = values["deck_id"];
		CollectionDeck collectionDeck = null;
		if (deckParam.HasNumber)
		{
			collectionDeck = CollectionManager.Get().GetDeck(deckParam.Number);
		}
		if (deckParam.HasNumber && collectionDeck == null)
		{
			collectionDeck = (from x in CollectionManager.Get().GetDecks()
				where x.Value.Name.Equals(deckParam.Text, StringComparison.CurrentCultureIgnoreCase)
				select x).FirstOrDefault().Value;
			if (collectionDeck == null)
			{
				Error.AddWarning("scenario Cheat Error", "Error reading a deck id from \"{0}\"", collectionDeck);
				return false;
			}
		}
		NamedParam namedParam3 = values["format_type"];
		FormatType formatType = FormatType.FT_WILD;
		if (namedParam3.HasNumber)
		{
			formatType = (FormatType)namedParam3.Number;
			if (formatType == FormatType.FT_UNKNOWN)
			{
				Error.AddWarning("scenario Cheat Error", "Error reading a format type from \"{0}\"", formatType);
				return false;
			}
		}
		NamedParam namedParam4 = values["prog_override"];
		GameType gameType2 = GameType.GT_UNKNOWN;
		if (namedParam4.HasNumber)
		{
			gameType2 = (GameType)namedParam4.Number;
			if (gameType2 == GameType.GT_UNKNOWN)
			{
				Error.AddWarning("scenario Cheat Error", "Error reading a prog override from \"{0}\"", gameType2);
				return false;
			}
		}
		QuickLaunchAvailability quickLaunchAvailability = GetQuickLaunchAvailability();
		switch (quickLaunchAvailability)
		{
		case QuickLaunchAvailability.FINDING_GAME:
			Error.AddDevWarning("scenario Cheat Error", "You are already finding a game.");
			goto IL_02f2;
		case QuickLaunchAvailability.ACTIVE_GAME:
			Error.AddDevWarning("scenario Cheat Error", "You are already in a game.");
			goto IL_02f2;
		case QuickLaunchAvailability.SCENE_TRANSITION:
			Error.AddDevWarning("scenario Cheat Error", "Can't start a game because a scene transition is active.");
			goto IL_02f2;
		case QuickLaunchAvailability.COLLECTION_NOT_READY:
			Error.AddDevWarning("scenario Cheat Error", "Can't start a game because your collection is not fully loaded.");
			goto IL_02f2;
		default:
			Error.AddDevWarning("scenario Cheat Error", "Can't start a game: {0}", quickLaunchAvailability);
			goto IL_02f2;
		case QuickLaunchAvailability.OK:
			{
				LaunchQuickGame(num, gameType, formatType, collectionDeck, null, gameType2);
				return true;
			}
			IL_02f2:
			return false;
		}
	}

	private bool OnProcessCheat_aigame(string func, string[] args, string rawArgs)
	{
		int missionId = 3680;
		GameType gameType = GameType.GT_VS_AI;
		string text = args[0];
		if (string.IsNullOrEmpty(text))
		{
			Error.AddWarning("aigame Cheat Error", "No deck string supplied");
			return false;
		}
		if (ShareableDeck.Deserialize(text) == null)
		{
			Error.AddWarning("aigame Cheat Error", "Invalid deck string supplied \"{0}\"", text);
			return false;
		}
		FormatType outVal = FormatType.FT_WILD;
		if (args.Length > 1)
		{
			string text2 = args[1];
			if (int.TryParse(text2, out var result))
			{
				outVal = (FormatType)result;
			}
			else if (!EnumUtils.TryGetEnum<FormatType>(text2, out outVal))
			{
				switch (text2.ToLower())
				{
				case "wild":
					outVal = FormatType.FT_WILD;
					break;
				case "standard":
				case "std":
					outVal = FormatType.FT_STANDARD;
					break;
				default:
					Error.AddWarning("scenario Cheat Error", "Error reading a parameter for FormatType \"{0}\", please use \"wild\" or \"standard\"", text2);
					return false;
				}
			}
		}
		QuickLaunchAvailability quickLaunchAvailability = GetQuickLaunchAvailability();
		switch (quickLaunchAvailability)
		{
		case QuickLaunchAvailability.FINDING_GAME:
			Error.AddDevWarning("scenario Cheat Error", "You are already finding a game.");
			goto IL_016f;
		case QuickLaunchAvailability.ACTIVE_GAME:
			Error.AddDevWarning("scenario Cheat Error", "You are already in a game.");
			goto IL_016f;
		case QuickLaunchAvailability.SCENE_TRANSITION:
			Error.AddDevWarning("scenario Cheat Error", "Can't start a game because a scene transition is active.");
			goto IL_016f;
		case QuickLaunchAvailability.COLLECTION_NOT_READY:
			Error.AddDevWarning("scenario Cheat Error", "Can't start a game because your collection is not fully loaded.");
			goto IL_016f;
		default:
			Error.AddDevWarning("scenario Cheat Error", "Can't start a game: {0}", quickLaunchAvailability);
			goto IL_016f;
		case QuickLaunchAvailability.OK:
			{
				LaunchQuickGame(missionId, gameType, outVal, null, text);
				return true;
			}
			IL_016f:
			return false;
		}
	}

	private bool OnProcessCheat_loadSnapshot(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		string text = args[0];
		if (!text.EndsWith(".replay"))
		{
			text += ".replay";
		}
		if (!File.Exists(text))
		{
			Error.AddDevWarning("loadsnapshot Cheat Error", $"Replay file {text}\nnot found!");
			return false;
		}
		byte[] array = File.ReadAllBytes(text);
		GameSnapshot gameSnapshot = new GameSnapshot();
		gameSnapshot.Deserialize(new MemoryStream(array));
		QuickLaunchAvailability quickLaunchAvailability = GetQuickLaunchAvailability();
		switch (quickLaunchAvailability)
		{
		case QuickLaunchAvailability.FINDING_GAME:
			Error.AddDevWarning("loadsnapshot Cheat Error", "You are already finding a game.");
			goto IL_00ff;
		case QuickLaunchAvailability.ACTIVE_GAME:
			Error.AddDevWarning("loadsnapshot Cheat Error", "You are already in a game.");
			goto IL_00ff;
		case QuickLaunchAvailability.SCENE_TRANSITION:
			Error.AddDevWarning("loadsnapshot Cheat Error", "Can't start a game because a scene transition is active.");
			goto IL_00ff;
		case QuickLaunchAvailability.COLLECTION_NOT_READY:
			Error.AddDevWarning("loadsnapshot Cheat Error", "Can't start a game because your collection is not fully loaded.");
			goto IL_00ff;
		default:
			Error.AddDevWarning("loadsnapshot Cheat Error", "Can't start a game: {0}", quickLaunchAvailability);
			goto IL_00ff;
		case QuickLaunchAvailability.OK:
			{
				GameType gameType = gameSnapshot.GameType;
				FormatType formatType = gameSnapshot.FormatType;
				int scenarioId = gameSnapshot.ScenarioId;
				m_quickLaunchState.m_launching = true;
				string message = $"Launching game from replay file\n{text}";
				UIStatus.Get().AddInfo(message);
				SceneMgr.Get().RegisterSceneLoadedEvent(OnSceneLoaded);
				GameMgr.Get().SetPendingAutoConcede(pendingAutoConcede: true);
				GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
				GameMgr.Get().FindGame(gameType, formatType, scenarioId, 0, 0L, null, null, restoreSavedGameState: false, array);
				return true;
			}
			IL_00ff:
			return false;
		}
	}

	private bool OnProcessCheat_exportcards(string func, string[] args, string rawArgs)
	{
		SceneManager.LoadScene("ExportCards");
		return true;
	}

	private bool OnProcessCheat_exportcardbacks(string func, string[] args, string rawArgs)
	{
		SceneManager.LoadScene("ExportCardBacks");
		return true;
	}

	private bool OnProcessCheat_freeyourmind(string func, string[] args, string rawArgs)
	{
		m_isNewCardInPackOpeningEnabled = true;
		return true;
	}

	private bool OnProcessCheat_reloadgamestrings(string func, string[] args, string rawArgs)
	{
		GameStrings.ReloadAll();
		return true;
	}

	private bool OnProcessCheat_userattentionmanager(string func, string[] args, string rawArgs)
	{
		string arg = UserAttentionManager.DumpUserAttentionBlockers("OnProcessCheat_userattentionmanager");
		UIStatus.Get().AddInfo($"Current UserAttentionBlockers: {arg}");
		return true;
	}

	private void Cheat_ShowBannerList()
	{
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = true;
		foreach (BannerDbfRecord item in from r in GameDbf.Banner.GetRecords()
			orderby r.ID descending
			select r)
		{
			if (!flag)
			{
				stringBuilder.Append("\n");
			}
			flag = false;
			stringBuilder.AppendFormat("{0}. {1}", item.ID, item.NoteDesc);
		}
		UIStatus.Get().AddInfo(stringBuilder.ToString(), 5f);
	}

	private bool OnProcessCheat_banner(string func, string[] args, string rawArgs)
	{
		int result = 0;
		if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
		{
			result = GameDbf.Banner.GetRecords().Max((BannerDbfRecord r) => r.ID);
		}
		else
		{
			if (!int.TryParse(args[0], out result))
			{
				if (args[0].Equals("list", StringComparison.InvariantCultureIgnoreCase))
				{
					Cheat_ShowBannerList();
					return true;
				}
				UIStatus.Get().AddInfo($"Unknown parameter: {args[0]}");
				return true;
			}
			if (GameDbf.Banner.GetRecord(result) == null)
			{
				UIStatus.Get().AddInfo($"Unknown bannerId: {result}");
				return true;
			}
		}
		BannerManager.Get().ShowBanner(result);
		return true;
	}

	private bool OnProcessCheat_raf(string func, string[] args, string rawArgs)
	{
		string a = args[0].ToLower();
		if (string.Equals(a, "showhero"))
		{
			RAFManager.Get().ShowRAFHeroFrame();
		}
		else if (string.Equals(a, "showprogress"))
		{
			RAFManager.Get().ShowRAFProgressFrame();
		}
		else if (string.Equals(a, "setprogress"))
		{
			if (args.Length > 1)
			{
				int rAFProgress = Convert.ToInt32(args[1]);
				RAFManager.Get().SetRAFProgress(rAFProgress);
			}
		}
		else if (string.Equals(a, "showglows"))
		{
			Options.Get().SetBool(Option.HAS_SEEN_RAF, val: false);
			Options.Get().SetBool(Option.HAS_SEEN_RAF_RECRUIT_URL, val: false);
			FriendListFrame friendListFrame = ChatMgr.Get().FriendListFrame;
			if (friendListFrame != null)
			{
				friendListFrame.UpdateRAFButtonGlow();
			}
			RAFFrame rAFFrame = RAFManager.Get().GetRAFFrame();
			if (rAFFrame != null)
			{
				rAFFrame.UpdateRecruitFriendsButtonGlow();
			}
			RAFManager.Get().ShowRAFProgressFrame();
		}
		return true;
	}

	private bool OnProcessCheat_returningplayer(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			Error.AddWarning("returningplayer Cheat Error", "No parameter provided.");
		}
		if (!GeneralUtils.TryParseInt(args[0], out var val))
		{
			Error.AddWarning("returningplayer Cheat Error", "Error reading an int from \"{0}\"", args[0]);
			return false;
		}
		ReturningPlayerMgr.Get().Cheat_SetReturningPlayerProgress(val);
		return true;
	}

	private bool OnProcessCheat_setrotationevent(string func, string[] args, string rawArgs)
	{
		int num = 0;
		int val = 1;
		bool boolVal = false;
		if (!string.IsNullOrEmpty(args[num]) && !GeneralUtils.TryParseBool(args[num], out boolVal))
		{
			UIStatus.Get().AddError("please enter a valid bool value for 1st parameter <bypass intro>");
			return true;
		}
		num++;
		if (args.Length > num && !string.IsNullOrEmpty(args[num]) && !GeneralUtils.TryParseInt(args[num], out val))
		{
			UIStatus.Get().AddError("please enter a valid int value for 2nd parameter <delay>");
			return true;
		}
		bool forceShowSetRotationIntro = !boolVal;
		Processor.RunCoroutine(TriggerSetRotationInSeconds(val, forceShowSetRotationIntro));
		return true;
	}

	private IEnumerator TriggerSetRotationInSeconds(int delay, bool forceShowSetRotationIntro)
	{
		SetRotationManager.Get().Cheat_OverrideSetRotationDate(DateTime.Now.AddSeconds(delay), forceShowSetRotationIntro);
		while (delay > 0)
		{
			UIStatus.Get().AddInfo($"Set Rotation occurs in {delay}...");
			delay--;
			yield return new WaitForSeconds(1f);
		}
		UIStatus.Get().AddInfo(string.Concat(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, " has occurred!"));
	}

	private bool OnProcessCheat_rankdebug(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
		{
			UIStatus.Get().AddError("rankdebug cheat must have the following args:\nshow [standard/wild]\noff");
			return true;
		}
		string a = args[0].ToLower();
		if (string.Equals(a, "show"))
		{
			if (args.Length > 1)
			{
				a = args[1].ToLower();
				if (string.Equals(a, "wild"))
				{
					Options.Get().SetEnum(Option.RANK_DEBUG, RankDebugOption.WILD);
					return true;
				}
				if (string.Equals(a, "classic"))
				{
					Options.Get().SetEnum(Option.RANK_DEBUG, RankDebugOption.CLASSIC);
					return true;
				}
				if (!string.Equals(a, "standard"))
				{
					UIStatus.Get().AddError("rankdebug error: Unknown league, please specify [standard/wild]");
					return true;
				}
			}
			Options.Get().SetEnum(Option.RANK_DEBUG, RankDebugOption.STANDARD);
		}
		else if (string.Equals(a, "off"))
		{
			Options.Get().SetEnum(Option.RANK_DEBUG, RankDebugOption.OFF);
		}
		else
		{
			UIStatus.Get().AddError("rankdebug error: Unknown argument");
		}
		return true;
	}

	private bool OnProcessCheat_resetrankedintro(string func, string[] args, string rawArgs)
	{
		List<GameSaveDataManager.SubkeySaveRequest> list = new List<GameSaveDataManager.SubkeySaveRequest>();
		list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_INTRO_SEEN_COUNT, default(long)));
		list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_SEASON_BONUS_STARS_POPUP_SEEN, default(long)));
		list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_BONUS_STARS_POPUP_SEEN_COUNT, default(long)));
		list.Add(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_REWARDS_VERSION_SEEN, default(long)));
		List<GameSaveDataManager.SubkeySaveRequest> requests = list;
		if (GameSaveDataManager.Get().SaveSubkeys(requests))
		{
			UIStatus.Get().AddInfo("Ranked intro game save data keys reset.");
			return true;
		}
		UIStatus.Get().AddInfo("Failed to reset ranked intro game save data keys!");
		return false;
	}

	private bool OnProcessCheat_advevent(string func, string[] args, string rawArgs)
	{
		if (AdventureScene.Get() == null || AdventureMissionDisplay.Get() == null || SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE)
		{
			UIStatus.Get().AddError("You must be viewing an Adventure to use this cheat!");
			return true;
		}
		if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
		{
			UIStatus.Get().AddError("You must provide an event from AdventureWingEventTable as a parameter!");
			return true;
		}
		if (AdventureMissionDisplay.Get().Cheat_AdventureEvent(args[0]))
		{
			UIStatus.Get().AddInfo($"Triggered event {args[0]} on each wing's AdventureWingEventTable.");
		}
		else
		{
			UIStatus.Get().AddInfo("Could not activate cheat 'advevent', perhaps 'advdev' has not been enabled yet?");
		}
		return true;
	}

	private bool OnProcessCheat_lowmemorywarning(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			MobileCallbackManager.Get().LowMemoryWarning("");
		}
		else
		{
			MobileCallbackManager.Get().LowMemoryWarning(args[0]);
		}
		return true;
	}

	private bool OnProcessCheat_mobile(string func, string[] args, string rawArgs)
	{
		string a = args[0].ToLower();
		if (string.Equals(a, "login"))
		{
			if (args.Length > 1 && string.Equals(args[1].ToLower(), "clear"))
			{
				ILoginService loginService = HearthstoneServices.Get<ILoginService>();
				loginService?.ClearAuthentication();
				loginService?.ClearAllSavedAccounts();
				UIStatus.Get().AddInfo("Mobile Login Cleared!");
			}
		}
		else if (string.Equals(a, "push"))
		{
			if (args.Length > 1)
			{
				string a2 = args[1].ToLower();
				if (string.Equals(a2, "register"))
				{
					MobileCallbackManager.RegisterPushNotifications();
					UIStatus.Get().AddInfo("Registered for Push!");
				}
				else if (string.Equals(a2, "logout"))
				{
					MobileCallbackManager.LogoutPushNotifications();
					UIStatus.Get().AddInfo("Logged Out for Push!");
				}
			}
		}
		else if (string.Equals(a, "ngdp") && args.Length > 1 && string.Equals(args[1].ToLower(), "clear"))
		{
			AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
			popupInfo.m_headerText = "GameDownloadManager";
			popupInfo.m_text = "Hearthstone can crash after clearing data. Do you still want to clear the data? Please re-launch Hearthstone after clearing data.";
			popupInfo.m_showAlertIcon = true;
			popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
			popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response != AlertPopup.Response.CANCEL && DownloadManager != null)
				{
					DownloadManager.DeleteDownloadedData();
				}
			};
			DialogManager.Get().ShowPopup(popupInfo);
		}
		return true;
	}

	private bool OnProcessCheat_edittextdebug(string func, string[] args, string rawArgs)
	{
		if (PlatformSettings.RuntimeOS == OSCategory.Android)
		{
			TextField.ToggleDebug();
		}
		else
		{
			UIStatus.Get().AddInfo("EditText debug is only for Android platforms");
		}
		return true;
	}

	private bool OnProcessCheat_resetrotationtutorial(string func, string[] args, string rawArgs)
	{
		bool flag = true;
		if (args.Length != 0)
		{
			string text = args[0].ToLower();
			if (string.Equals(text, "veteran"))
			{
				flag = false;
			}
			else if (!string.IsNullOrEmpty(text) && !string.Equals(text, "newbie"))
			{
				string message = $"resetrotationtutorial: {text} is not a valid parameter!";
				UIStatus.Get().AddError(message);
				return true;
			}
		}
		if (flag)
		{
			Options.Get().SetBool(Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, val: false);
			Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS, 0);
			Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS_NEW_PLAYER, 0);
			Options.Get().SetBool(Option.NEEDS_TO_MAKE_STANDARD_DECK, val: true);
		}
		else
		{
			Options.Get().SetBool(Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, val: true);
			Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS, 5);
			Options.Get().SetInt(Option.SET_ROTATION_INTRO_PROGRESS_NEW_PLAYER, 5);
		}
		Options.Get().SetBool(Option.DISABLE_SET_ROTATION_INTRO, val: false);
		string message2 = string.Format("Set Rotation tutorial progress reset as a {0}!\nReset disableSetRotationIntro to false. Restart client to trigger the flow.", flag ? "newbie" : "veteran");
		UIStatus.Get().AddInfo(message2);
		return true;
	}

	private bool OnProcessCheat_browser(string func, string[] args, string rawArgs)
	{
		string a = args[0].ToLower();
		if (string.Equals(a, "show"))
		{
			InGameBrowserManager.Get().Show();
		}
		else if (string.Equals(a, "hide"))
		{
			InGameBrowserManager.Get().Hide();
		}
		else if (string.Equals(a, "url") && args.Length > 1)
		{
			string url = args[1].ToLower();
			InGameBrowserManager.Get().SetUrl(url);
		}
		return true;
	}

	private bool OnProcessCheat_cloud(string func, string[] args, string rawArgs)
	{
		string a = args[0].ToLower();
		if (string.Equals(a, "set"))
		{
			if (args.Length > 2)
			{
				string text = args[1];
				string text2 = args[2];
				if (string.Equals(text2.ToLower(), "blank"))
				{
					text2 = "";
				}
				CloudStorageManager.Get().SetString(text, text2);
				UIStatus.Get().AddInfo("Cloud Storage Set: (" + text + ", " + text2 + ")");
			}
		}
		else if (string.Equals(a, "get"))
		{
			if (args.Length > 1)
			{
				string text3 = args[1];
				string @string = CloudStorageManager.Get().GetString(text3);
				UIStatus.Get().AddInfo("Cloud Storage Get: Value for " + text3 + " is " + ((@string == null) ? "NULL" : @string));
			}
		}
		else if (string.Equals(a, "reset"))
		{
			Options.Get().SetBool(Option.DISALLOWED_CLOUD_STORAGE, val: false);
			UIStatus.Get().AddInfo("Cloud Storage Disallow Reset!");
		}
		return true;
	}

	private bool OnProcessCheat_tempaccount(string func, string[] args, string rawArgs)
	{
		string a = args[0].ToLower();
		if (string.Equals(a, "dialog"))
		{
			if (args.Length > 1 && string.Equals(args[1].ToLower(), "skip"))
			{
				CreateSkipHelper.ShowCreateSkipDialog(null);
			}
			else
			{
				TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_03"), TemporaryAccountManager.HealUpReason.UNKNOWN, userTriggered: true, null);
			}
		}
		else if (string.Equals(a, "cheat"))
		{
			if (args.Length > 1)
			{
				string a2 = args[1].ToLower();
				if (string.Equals(a2, "on"))
				{
					Options.Get().SetBool(Option.IS_TEMPORARY_ACCOUNT_CHEAT, val: true);
					UIStatus.Get().AddInfo("Temporary Account CHEAT is now ON");
				}
				else if (string.Equals(a2, "off"))
				{
					Options.Get().SetBool(Option.IS_TEMPORARY_ACCOUNT_CHEAT, val: false);
					UIStatus.Get().AddInfo("Temporary Account CHEAT is now OFF");
				}
				else if (string.Equals(a2, "clear"))
				{
					Options.Get().DeleteOption(Option.IS_TEMPORARY_ACCOUNT_CHEAT);
					UIStatus.Get().AddInfo("Temporary Account CHEAT is now CLEARED");
				}
			}
		}
		else if (string.Equals(a, "status"))
		{
			string text = "Temporary Account status is " + (BattleNet.IsHeadlessAccount() ? "ON" : "OFF") + " Cheat is ";
			text = ((!Options.Get().HasOption(Option.IS_TEMPORARY_ACCOUNT_CHEAT)) ? (text + "CLEARED") : (text + (Options.Get().GetBool(Option.IS_TEMPORARY_ACCOUNT_CHEAT) ? "ON" : "OFF")));
			UIStatus.Get().AddInfo(text);
		}
		else if (string.Equals(a, "tutorial"))
		{
			if (args.Length > 1)
			{
				string a3 = args[1].ToLower();
				if (string.Equals(a3, "skip"))
				{
					Options.Get().SetBool(Option.CONNECT_TO_AURORA, val: true);
					Options.Get().SetEnum(Option.LOCAL_TUTORIAL_PROGRESS, TutorialProgress.ILLIDAN_COMPLETE);
					UIStatus.Get().AddInfo("Set to Skip No Account Tutorial");
				}
				else if (string.Equals(a3, "reset"))
				{
					Options.Get().SetBool(Option.CONNECT_TO_AURORA, val: false);
					Options.Get().SetEnum(Option.LOCAL_TUTORIAL_PROGRESS, TutorialProgress.NOTHING_COMPLETE);
					UIStatus.Get().AddInfo("Set to Reset No Account Tutorial");
				}
			}
		}
		else if (string.Equals(a, "id"))
		{
			string selectedTemporaryAccountId = TemporaryAccountManager.Get().GetSelectedTemporaryAccountId();
			UIStatus.Get().AddInfo("Selected Temporary Account ID is " + ((selectedTemporaryAccountId == null) ? "NULL" : selectedTemporaryAccountId));
		}
		else if (string.Equals(a, "healupcomplete"))
		{
			TemporaryAccountManager.Get().HealUpCompleteTest();
		}
		else if (string.Equals(a, "healupachievement"))
		{
			AchieveManager.Get().NotifyOfAccountCreation();
		}
		else if (string.Equals(a, "showswitchaccount"))
		{
			TemporaryAccountManager.Get().ShowSwitchAccountMenu(null, disableInputBlocker: false);
		}
		else if (string.Equals(a, "data"))
		{
			if (args.Length > 1)
			{
				string a4 = args[1].ToLower();
				if (string.Equals(a4, "print"))
				{
					TemporaryAccountManager.Get().PrintTemporaryAccountData();
					UIStatus.Get().AddInfo("Temporary Account Data Printed");
				}
				else if (string.Equals(a4, "clear"))
				{
					TemporaryAccountManager.Get().DeleteTemporaryAccountData();
					UIStatus.Get().AddInfo("Temporary Account Data Deleted");
				}
			}
		}
		else if (string.Equals(a, "nag"))
		{
			if (args.Length > 1)
			{
				string a5 = args[1].ToLower();
				if (string.Equals(a5, "time"))
				{
					string text2 = TemporaryAccountManager.Get().NagTimeDebugLog();
					Log.TemporaryAccount.Print(text2);
					UIStatus.Get().AddInfo(text2);
				}
				else if (string.Equals(a5, "clear"))
				{
					Options.Get().DeleteOption(Option.LAST_HEAL_UP_EVENT_DATE);
					UIStatus.Get().AddInfo("Last Heal Up Event Time Cleared!");
				}
			}
		}
		else if (string.Equals(a, "test"))
		{
			TemporaryAccountManager.Get().Test();
		}
		else if (string.Equals(a, "lazy"))
		{
			HearthstoneServices.Get<ILoginService>()?.ClearAuthentication();
			TemporaryAccountManager.Get().DeleteTemporaryAccountData();
			Options.Get().SetBool(Option.CONNECT_TO_AURORA, val: true);
			Options.Get().SetEnum(Option.LOCAL_TUTORIAL_PROGRESS, TutorialProgress.ILLIDAN_COMPLETE);
		}
		return true;
	}

	private bool OnProcessCheat_arena(string func, string[] args, string rawArgs)
	{
		string text = ((args.Length >= 1) ? args[0] : null);
		string text2 = ((args.Length >= 2) ? args[1] : null);
		string text3 = ((args.Length >= 3) ? args[2] : null);
		float delay = 5f * Time.timeScale;
		if (string.IsNullOrEmpty(text) || text == "help")
		{
			string message = ((text2 == "popup") ? "Valid arena popup args: clear, comingsoon [#days], endingsoon [#days]" : ((!(text2 == "refresh")) ? "Valid arena commands: popup refresh\n\nUse 'util arena' to execute cheats on server, e.g. 'util arena season x' to switch season to x." : "refreshes Arena season info from server"));
			UIStatus.Get().AddInfo(message, delay);
			return true;
		}
		string text4 = null;
		switch (text)
		{
		case "popup":
			switch (text2)
			{
			case null:
			case "help":
				UIStatus.Get().AddInfo("Valid arena popup args: clear, comingsoon [#days], endingsoon [#days]", delay);
				return true;
			case "clear":
			case "clearpopups":
			case "clearseen":
				if (text3 == "innkeeper")
				{
					DraftManager.Get().ClearAllInnkeeperPopups();
					text4 = "All arena innkeeper popups cleared.";
				}
				else
				{
					DraftManager.Get().ClearAllSeenPopups();
					text4 = "All arena popups cleared.";
				}
				NetCache.Get().DispatchClientOptionsToServer();
				break;
			case "1":
			case "comingsoon":
			{
				if (!double.TryParse(text3, out var result2))
				{
					result2 = 13.0;
				}
				DraftManager.Get().ShowArenaPopup_SeasonComingSoon((long)(result2 * 86400.0), null);
				text4 = string.Empty;
				break;
			}
			case "2":
			case "endingsoon":
			{
				if (!double.TryParse(text3, out var result))
				{
					result = 5.0;
				}
				DraftManager.Get().ShowArenaPopup_SeasonEndingSoon((long)(result * 86400.0), null);
				text4 = string.Empty;
				break;
			}
			}
			break;
		case "refresh":
			DraftManager.Get().RefreshCurrentSeasonFromServer();
			text4 = "Refreshing Arena season info from server.";
			break;
		case "season":
			text4 = $"Please use 'util arena {rawArgs}' instead.";
			break;
		case "choices":
		{
			List<string> list = new List<string>();
			for (int i = 1; i < args.Length; i++)
			{
				list.Add(args[i]);
			}
			if (TryParseArenaChoices(list.ToArray(), out var output))
			{
				List<string> list2 = new List<string>();
				list2.Add("arena");
				list2.Add("choices");
				string[] array = output;
				foreach (string item in array)
				{
					list2.Add(item);
				}
				OnProcessCheat_utilservercmd("util", list2.ToArray(), rawArgs, null);
			}
			text4 = string.Empty;
			break;
		}
		}
		NetCache.Get().DispatchClientOptionsToServer();
		if (text4 == null)
		{
			text4 = $"Unknown subcmd: {rawArgs}";
		}
		UIStatus.Get().AddInfo(text4, delay);
		return true;
	}

	private bool OnProcessCheat_EventTiming(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		args = args.Where((string a) => !string.IsNullOrEmpty(a.Trim())).ToArray();
		if (autofillData != null)
		{
			List<string> list = SpecialEventManager.Get().AllKnownEvents.Select((SpecialEventType e) => SpecialEventManager.Get().GetName(e)).ToList();
			if (args.Length <= 1)
			{
				list.InsertRange(0, new string[3] { "list", "listall", "help" });
			}
			return ProcessAutofillParam(list, (args.Length == 0) ? string.Empty : args.Last(), autofillData);
		}
		if (args.Length != 0 && args[0] == "help")
		{
			UIStatus.Get().AddInfoNoRichText("Lists events and whether or not they're Active.\nValid args: list | listall | [event names]\n", 5f * Time.timeScale);
			return true;
		}
		List<SpecialEventType> list2 = new List<SpecialEventType>();
		bool flag = false;
		bool flag2 = true;
		bool flag3 = false;
		string[] array = args;
		foreach (string text in array)
		{
			if (text == "list")
			{
				flag3 = true;
				continue;
			}
			if (text == "listall")
			{
				flag3 = true;
				list2.AddRange(SpecialEventManager.Get().AllKnownEvents);
				flag2 = false;
				continue;
			}
			string text2 = text;
			if (text.StartsWith("event=") && text.Length > 6)
			{
				text2 = text.Substring(6);
			}
			Func<string, string, bool> fnSubstringMatch = (string evtName, string userInput) => evtName.Contains(userInput, StringComparison.InvariantCultureIgnoreCase);
			Func<string, string, bool> fnStartsWithMatch = (string evtName, string userInput) => evtName.StartsWith(userInput, StringComparison.InvariantCultureIgnoreCase);
			Func<string, string, bool> fnEndsWithMatch = (string evtName, string userInput) => evtName.EndsWith(userInput, StringComparison.InvariantCultureIgnoreCase);
			Func<string, string, bool> fnExactMatch = (string evtName, string userInput) => evtName.Equals(userInput, StringComparison.InvariantCultureIgnoreCase);
			string[] names = text2.Split(',');
			Func<string, bool> fnIsMatch = (string evtName) => names.Any(delegate(string userInput)
			{
				Func<string, string, bool> func2 = fnSubstringMatch;
				bool flag8 = false;
				bool flag9 = false;
				if (userInput.StartsWith("^"))
				{
					userInput = userInput.Substring(1);
					flag8 = true;
				}
				if (userInput.EndsWith("$"))
				{
					userInput = userInput.Substring(0, userInput.Length - 1);
					flag9 = true;
				}
				if (userInput.Length == 0)
				{
					return false;
				}
				if (flag8 && flag9)
				{
					func2 = fnExactMatch;
				}
				else if (flag8)
				{
					func2 = fnStartsWithMatch;
				}
				else if (flag9)
				{
					func2 = fnEndsWithMatch;
				}
				return func2(evtName, userInput);
			});
			IEnumerable<SpecialEventType> collection = from evt in SpecialEventManager.Get().AllKnownEvents
				let evtName = SpecialEventManager.Get().GetName(evt)
				where fnIsMatch(evtName)
				select evt;
			list2.AddRange(collection);
			flag2 = false;
		}
		if (flag2)
		{
			list2 = SpecialEventManager.Get().AllKnownEvents.Where((SpecialEventType e) => SpecialEventManager.Get().IsEventActive(e, activeIfDoesNotExist: false)).ToList();
			flag = true;
		}
		DateTime utcNow = DateTime.UtcNow;
		if (flag)
		{
			list2.RemoveAll(delegate(SpecialEventType e)
			{
				DateTime? eventStartTimeUtc4 = SpecialEventManager.Get().GetEventStartTimeUtc(e);
				DateTime? eventEndTimeUtc4 = SpecialEventManager.Get().GetEventEndTimeUtc(e);
				TimeSpan timeSpan3 = ((!eventStartTimeUtc4.HasValue) ? TimeSpan.MaxValue : ((eventStartTimeUtc4.Value > utcNow) ? (eventStartTimeUtc4.Value - utcNow) : (utcNow - eventStartTimeUtc4.Value)));
				TimeSpan timeSpan4 = ((!eventEndTimeUtc4.HasValue) ? TimeSpan.MaxValue : ((eventEndTimeUtc4.Value > utcNow) ? (eventEndTimeUtc4.Value - utcNow) : (utcNow - eventEndTimeUtc4.Value)));
				bool num = timeSpan3.TotalDays <= 120.0;
				bool flag7 = timeSpan4.TotalDays <= 120.0;
				return !num && !flag7;
			});
		}
		if (list2.Count <= 0)
		{
			UIStatus.Get().AddInfoNoRichText("No events to show (check event names).");
			return true;
		}
		list2.Sort(delegate(SpecialEventType lhs, SpecialEventType rhs)
		{
			bool flag5 = SpecialEventManager.Get().IsEventActive(lhs, activeIfDoesNotExist: false);
			bool flag6 = SpecialEventManager.Get().IsEventActive(rhs, activeIfDoesNotExist: false);
			if (flag5 != flag6)
			{
				if (!flag5)
				{
					return 1;
				}
				return -1;
			}
			DateTime? eventStartTimeUtc2 = SpecialEventManager.Get().GetEventStartTimeUtc(lhs);
			DateTime? eventStartTimeUtc3 = SpecialEventManager.Get().GetEventStartTimeUtc(rhs);
			if (eventStartTimeUtc2 != eventStartTimeUtc3)
			{
				if (eventStartTimeUtc2.HasValue)
				{
					if (eventStartTimeUtc3.HasValue)
					{
						return eventStartTimeUtc2.Value.CompareTo(eventStartTimeUtc3.Value);
					}
					return 1;
				}
				return -1;
			}
			DateTime? eventEndTimeUtc2 = SpecialEventManager.Get().GetEventEndTimeUtc(lhs);
			DateTime? eventEndTimeUtc3 = SpecialEventManager.Get().GetEventEndTimeUtc(rhs);
			if (eventEndTimeUtc2 != eventEndTimeUtc3)
			{
				if (eventEndTimeUtc2.HasValue)
				{
					if (eventEndTimeUtc3.HasValue)
					{
						return eventEndTimeUtc2.Value.CompareTo(eventEndTimeUtc3.Value);
					}
					return -1;
				}
				return 1;
			}
			string name = SpecialEventManager.Get().GetName(lhs);
			string name2 = SpecialEventManager.Get().GetName(rhs);
			return name.CompareTo(name2);
		});
		StringBuilder stringBuilder = new StringBuilder();
		foreach (SpecialEventType item in list2)
		{
			if (flag3)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(SpecialEventManager.Get().GetName(item));
				continue;
			}
			bool flag4 = SpecialEventManager.Get().IsEventActive(item, activeIfDoesNotExist: false);
			DateTime? eventStartTimeUtc = SpecialEventManager.Get().GetEventStartTimeUtc(item);
			DateTime? eventEndTimeUtc = SpecialEventManager.Get().GetEventEndTimeUtc(item);
			DateTime? dateTime = eventStartTimeUtc;
			DateTime? dateTime2 = eventEndTimeUtc;
			if (dateTime.HasValue)
			{
				dateTime = dateTime.Value.AddSeconds(SpecialEventManager.Get().DevTimeOffsetSeconds).ToLocalTime();
			}
			if (dateTime2.HasValue)
			{
				dateTime2 = dateTime2.Value.AddSeconds(SpecialEventManager.Get().DevTimeOffsetSeconds).ToLocalTime();
			}
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Append("\n");
			}
			string text3 = (dateTime.HasValue ? dateTime.Value.ToString("yyyy/MM/dd") : "<always>");
			string text4 = (dateTime2.HasValue ? dateTime2.Value.ToString("yyyy/MM/dd") : "<forever>");
			stringBuilder.AppendFormat("{0} {1} {2}-{3}", SpecialEventManager.Get().GetName(item), flag4 ? "Active" : "Inactive", text3, text4);
			if (flag4)
			{
				TimeSpan? timeSpan = ((!eventEndTimeUtc.HasValue || eventEndTimeUtc.Value < utcNow) ? null : new TimeSpan?(eventEndTimeUtc.Value - utcNow));
				if (timeSpan.HasValue && timeSpan.Value.TotalDays < 3.0)
				{
					stringBuilder.AppendFormat(" ends in {0}", TimeUtils.GetElapsedTimeString((int)timeSpan.Value.TotalSeconds, TimeUtils.SPLASHSCREEN_DATETIME_STRINGSET, roundUp: true));
				}
			}
			else
			{
				TimeSpan? timeSpan2 = ((!eventStartTimeUtc.HasValue || eventStartTimeUtc.Value < utcNow) ? null : new TimeSpan?(eventStartTimeUtc.Value - utcNow));
				if (timeSpan2.HasValue && timeSpan2.Value.TotalDays < 3.0)
				{
					stringBuilder.AppendFormat(" starts in {0}", TimeUtils.GetElapsedTimeString((int)timeSpan2.Value.TotalSeconds, TimeUtils.SPLASHSCREEN_DATETIME_STRINGSET, roundUp: true));
				}
			}
		}
		stringBuilder.Append("\n");
		float delay = (float)Mathf.Max(5, 2 * Mathf.Min(20, list2.Count)) * Time.timeScale;
		string text5 = stringBuilder.ToString();
		Log.EventTiming.PrintInfo(text5);
		UIStatus.Get().AddInfoNoRichText(text5, delay);
		return true;
	}

	private bool OnProcessCheat_UpdateIntention(string func, string[] args, string rawArgs)
	{
		Options.Get().SetInt(Option.UPDATE_STATE, int.Parse(args[0]));
		return true;
	}

	private bool OnProcessCheat_autoexportgamestate(string func, string[] args, string rawArgs)
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			return false;
		}
		string arg = (string.IsNullOrEmpty(args[0]) ? "GameStateExportFile" : args[0]);
		JsonNode jsonNode = new JsonNode();
		foreach (KeyValuePair<int, Player> item in GameState.Get().GetPlayerMap())
		{
			string key = "Player" + item.Key;
			JsonNode jsonNode2 = new JsonNode();
			jsonNode.Add(key, jsonNode2);
			jsonNode2["Hero"] = GetCardJson(item.Value.GetHero());
			jsonNode2["HeroPower"] = GetCardJson(item.Value.GetHeroPower());
			if (item.Value.HasWeapon())
			{
				jsonNode2["Weapon"] = GetCardJson(item.Value.GetWeaponCard().GetEntity());
			}
			jsonNode2["CardsInBattlefield"] = GetCardlistJson(item.Value.GetBattlefieldZone().GetCards());
			if (item.Value.GetSide() == Player.Side.FRIENDLY)
			{
				jsonNode2["CardsInHand"] = GetCardlistJson(item.Value.GetHandZone().GetCards());
				jsonNode2["ActiveSecrets"] = GetCardlistJson(item.Value.GetSecretZone().GetCards());
			}
		}
		File.WriteAllText($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\{arg}.json", Json.Serialize(jsonNode));
		return true;
	}

	private bool OnProcessCheat_social(string func, string[] args, string rawArgs)
	{
		List<BnetPlayer> friends = BnetFriendMgr.Get().GetFriends();
		List<BnetPlayer> nearbyPlayers = BnetNearbyPlayerMgr.Get().GetNearbyPlayers();
		List<BnetPlayer> fullPatronList = FiresideGatheringManager.Get().FullPatronList;
		friends.Sort(FriendUtils.FriendSortCompare);
		nearbyPlayers.Sort(FriendUtils.FriendSortCompare);
		fullPatronList.Sort(FriendUtils.FriendSortCompare);
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool printFullPresence = false;
		string text = "USAGE: social [cmd] [args]\nCommands: help, list";
		float delay = 5f;
		string text2 = ((args == null || args.Length == 0) ? "list" : args[0]);
		string text3 = null;
		if (!(text2 == "help"))
		{
			if (text2 == "list")
			{
				if (args.Length >= 2 && args[1] == "help")
				{
					text3 = "Lists all players in the various social lists. Can specific specific lists: friend, nearby, fsg|patron";
				}
				else
				{
					for (int i = 1; i < args.Length; i++)
					{
						switch ((args[i] == null) ? "" : args[i].ToLower())
						{
						case "friend":
						case "friends":
							flag = true;
							break;
						case "nearby":
						case "nearbyplayer":
						case "nearbyplayers":
						case "subnet":
						case "local":
						case "localplayer":
						case "localplayers":
							flag2 = true;
							break;
						case "fsg":
						case "fireside":
						case "firesidegathering":
						case "patron":
						case "patrons":
						case "patronlist":
							flag3 = true;
							break;
						case "full":
						case "all":
						case "presence":
							printFullPresence = true;
							break;
						}
					}
					if (!flag && !flag2 && !flag3)
					{
						flag = (flag2 = (flag3 = true));
					}
					Log.Presence.PrintInfo("Cheat: print social list executed.");
					if (flag3)
					{
						FSGConfig currentFSG = FiresideGatheringManager.Get().CurrentFSG;
						if (currentFSG == null)
						{
							Log.Presence.PrintInfo("FSG patrons: not checked in.");
						}
						else
						{
							Log.Presence.PrintInfo("FSG {0}-{1} patrons: {2}", currentFSG.FsgId, currentFSG.TavernName, fullPatronList.Count);
						}
						foreach (BnetPlayer item in fullPatronList)
						{
							OnProcessCheat_social_PrintPlayer(printFullPresence, item);
						}
					}
					if (flag)
					{
						Log.Presence.PrintInfo("Friends: {0}", friends.Count);
						foreach (BnetPlayer item2 in friends)
						{
							OnProcessCheat_social_PrintPlayer(printFullPresence, item2);
						}
					}
					if (flag2)
					{
						Log.Presence.PrintInfo("Nearby Players: {0}", nearbyPlayers.Count);
						foreach (BnetPlayer item3 in nearbyPlayers)
						{
							OnProcessCheat_social_PrintPlayer(printFullPresence, item3);
						}
					}
					text3 = "Printed to Presence Log.";
				}
			}
		}
		else
		{
			text3 = text;
		}
		if (text3 != null)
		{
			UIStatus.Get().AddInfo(text3, delay);
		}
		return true;
	}

	private bool OnProcessCheat_playStartEmote(string func, string[] args, string rawArgs)
	{
		Gameplay gameplay = Gameplay.Get();
		if (gameplay == null)
		{
			return false;
		}
		gameplay.StartCoroutine(PlayStartingTaunts());
		return true;
	}

	private bool OnProcessCheat_getBattlegroundDenyList(string func, string[] args, string rawArgs)
	{
		Network.Get().UpdateBattlegroundInfo();
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		gameState.SetPrintBattlegroundDenyListOnUpdate(isPrinting: true);
		return true;
	}

	private bool OnProcessCheat_getBattlegroundMinionPool(string func, string[] args, string rawArgs)
	{
		Network.Get().UpdateBattlegroundInfo();
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		gameState.SetPrintBattlegroundMinionPoolOnUpdate(isPrinting: true);
		return true;
	}

	private IEnumerator PlayStartingTaunts()
	{
		Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		Card heroPowerCard = GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard();
		if (heroPowerCard != null)
		{
			while (!heroPowerCard.GetActor().IsShown())
			{
				yield return null;
			}
			GameState.Get().GetGameEntity().FadeInActor(heroPowerCard.GetActor(), 0.4f);
		}
		while (!heroCard.GetActor().IsShown())
		{
			yield return null;
		}
		GameState.Get().GetGameEntity().FadeInHeroActor(heroCard.GetActor());
		EmoteEntry emoteEntry = heroCard.GetEmoteEntry(EmoteType.START);
		bool flag = true;
		if (emoteEntry != null)
		{
			CardSoundSpell soundSpell = emoteEntry.GetSoundSpell();
			if (soundSpell != null && soundSpell.DetermineBestAudioSource() == null)
			{
				flag = false;
			}
		}
		CardSoundSpell emoteSpell2 = null;
		if (flag)
		{
			emoteSpell2 = heroCard.PlayEmote(EmoteType.START);
		}
		if (emoteSpell2 != null)
		{
			while (emoteSpell2.GetActiveState() != 0)
			{
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(MulliganManager.DEFAULT_STARTING_TAUNT_DURATION);
		}
		GameState.Get().GetGameEntity().FadeOutHeroActor(heroCard.GetActor());
		if (heroPowerCard != null)
		{
			GameState.Get().GetGameEntity().FadeOutActor(heroPowerCard.GetActor());
		}
		Card myHeroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		Card myHeroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
		if (myHeroPowerCard != null)
		{
			GameState.Get().GetGameEntity().FadeInActor(myHeroPowerCard.GetActor(), 0.4f);
		}
		EmoteType emoteToPlay = EmoteType.START;
		if (myHeroCard.GetEmoteEntry(EmoteType.MIRROR_START) != null && myHeroPowerCard.GetEntity().GetCardId() == heroPowerCard.GetEntity().GetCardId())
		{
			emoteToPlay = EmoteType.MIRROR_START;
		}
		while (!myHeroCard.GetActor().IsShown())
		{
			yield return null;
		}
		GameState.Get().GetGameEntity().FadeInHeroActor(myHeroCard.GetActor());
		emoteSpell2 = myHeroCard.PlayEmote(emoteToPlay, Notification.SpeechBubbleDirection.BottomRight);
		if (emoteSpell2 != null)
		{
			while (emoteSpell2.GetActiveState() != 0)
			{
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(MulliganManager.DEFAULT_STARTING_TAUNT_DURATION);
		}
		GameState.Get().GetGameEntity().FadeOutHeroActor(myHeroCard.GetActor());
		if (myHeroPowerCard != null)
		{
			GameState.Get().GetGameEntity().FadeOutActor(myHeroPowerCard.GetActor());
		}
	}

	private static void OnProcessCheat_social_PrintPlayer(bool printFullPresence, BnetPlayer player)
	{
		string text = ((player == null) ? "<null>" : (printFullPresence ? player.FullPresenceSummary : player.ShortSummary));
		SortedList<string, bool> sortedList = new SortedList<string, bool>();
		if (FiresideGatheringManager.Get().IsPlayerInMyFSG(player))
		{
			sortedList["fsg"] = true;
		}
		if (BnetNearbyPlayerMgr.Get().IsNearbyPlayer(player))
		{
			sortedList["nearby"] = true;
		}
		if (BnetFriendMgr.Get().IsFriend(player))
		{
			sortedList["friend"] = true;
		}
		string text2 = string.Join(", ", sortedList.Keys.ToArray());
		if (!string.IsNullOrEmpty(text2))
		{
			text2 = $"[{text2}]";
		}
		Log.Presence.PrintInfo("    {0} {1}", text, text2);
	}

	private bool OnProcessCheat_OpponentName(string func, string[] args, string rawArgs)
	{
		Gameplay gameplay = Gameplay.Get();
		if (gameplay == null)
		{
			return false;
		}
		NameBanner nameBannerForSide = gameplay.GetNameBannerForSide(Player.Side.OPPOSING);
		if (nameBannerForSide == null)
		{
			return false;
		}
		nameBannerForSide.m_playerName.Text = args[0];
		return true;
	}

	private bool OnProcessCheat_friendlist(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: flist [cmd] [args]\nCommands: fill, add, remove";
		if (args.Length < 1 || args.Any((string a) => string.IsNullOrEmpty(a)))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		float delay = 5f;
		string text = null;
		switch (args[0])
		{
		case "fill":
		{
			int season2 = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>().Season;
			int iD = RankMgr.Get().GetLeagueRecordForType(League.LeagueType.NORMAL, season2).ID;
			int maxStarLevel = RankMgr.Get().GetMaxStarLevel(iD);
			foreach (FriendListType value in Enum.GetValues(typeof(FriendListType)))
			{
				for (int k = 1; k < maxStarLevel; k++)
				{
					string name = $"{value} friend{k}";
					CreateCheatFriendlistItem(name, value, iD, k, BnetProgramId.HEARTHSTONE, isFriend: true, isOnline: true);
				}
			}
			text = $"Filled friend list";
			break;
		}
		case "add":
		{
			int result = 1;
			string text2 = "Player";
			FriendListType type = FriendListType.FRIEND;
			int season = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>().Season;
			int leagueId = RankMgr.Get().GetLeagueRecordForType(League.LeagueType.NORMAL, season).ID;
			int starLevel = 1;
			BnetProgramId programID = BnetProgramId.HEARTHSTONE;
			bool boolVal = true;
			bool boolVal2 = true;
			for (int i = 0; i < args.Length; i++)
			{
				string[] array = args[i]?.Split('=');
				if (array == null || array.Length < 2)
				{
					continue;
				}
				if (array[0].Equals("num", StringComparison.InvariantCultureIgnoreCase))
				{
					int.TryParse(array[1], out result);
					if (result < 1)
					{
						result = 1;
					}
				}
				else if (array[0].Equals("name", StringComparison.InvariantCultureIgnoreCase))
				{
					text2 = array[1];
				}
				else if (array[0].Equals("type", StringComparison.InvariantCultureIgnoreCase))
				{
					string text3 = array[1];
					if (!string.IsNullOrEmpty(text3))
					{
						type = EnumUtils.SafeParse(text3, FriendListType.FRIEND, ignoreCase: true);
					}
				}
				else if (array[0].Equals("rank", StringComparison.InvariantCultureIgnoreCase))
				{
					LeagueRankDbfRecord leagueRankRecordByCheatName = RankMgr.Get().GetLeagueRankRecordByCheatName(array[1]);
					if (leagueRankRecordByCheatName != null)
					{
						leagueId = leagueRankRecordByCheatName.LeagueId;
						starLevel = leagueRankRecordByCheatName.StarLevel;
					}
				}
				else if (array[0].Equals("program", StringComparison.InvariantCultureIgnoreCase))
				{
					string text4 = array[1];
					if (!string.IsNullOrEmpty(text4))
					{
						programID = new BnetProgramId(text4);
						leagueId = 0;
						starLevel = 0;
					}
				}
				else if (array[0].Equals("friend", StringComparison.InvariantCultureIgnoreCase))
				{
					GeneralUtils.TryParseBool(array[1], out boolVal);
				}
				else if (array[0].Equals("online", StringComparison.InvariantCultureIgnoreCase))
				{
					GeneralUtils.TryParseBool(array[1], out boolVal2);
				}
			}
			for (int j = 0; j < result; j++)
			{
				CreateCheatFriendlistItem(text2 + j, type, leagueId, starLevel, programID, boolVal, boolVal2);
			}
			text = $"Created {result} players";
			break;
		}
		case "remove":
			BnetNearbyPlayerMgr.Get().Cheat_RemoveCheatFriends();
			FiresideGatheringManager.Get().Cheat_RemoveCheatFriends();
			BnetFriendMgr.Get().Cheat_RemoveCheatFriends();
			text = $"Removed cheat friends";
			break;
		}
		BnetBarFriendButton.Get().UpdateOnlineCount();
		if (text != null)
		{
			UIStatus.Get().AddInfo(text, delay);
		}
		return true;
	}

	private bool OnProcessCheat_SetGameSaveData(string func, string[] args, string rawArgs)
	{
		GameSaveKeyId key = (GameSaveKeyId)0;
		GameSaveKeySubkeyId subkey = (GameSaveKeySubkeyId)0;
		if (!ValidateAndParseGameSaveDataKeyAndSubkey(args, out key, out subkey))
		{
			UIStatus.Get().AddError("You must provide valid key and subkeys!");
			return true;
		}
		long value = 0L;
		int i = 2;
		string text = string.Empty;
		List<long> list = new List<long>();
		for (; i < args.Count(); i++)
		{
			if (!ValidateAndParseLongAtIndex(i, args, out value))
			{
				value = GameUtils.TranslateCardIdToDbId(args[i], showWarning: true);
				if (value == 0L)
				{
					break;
				}
			}
			list.Add(value);
			text = text + value + ";";
		}
		args = new string[4]
		{
			"setgsd",
			"key=" + args[0],
			"subkey=" + args[1],
			"values=" + text
		};
		GameSaveDataManager.Get().Cheat_SaveSubkeyToLocalCache(key, subkey, list.ToArray());
		UIStatus.Get().AddInfo($"Set key {key} subkey {subkey} to {text}");
		return OnProcessCheat_utilservercmd("util", args, rawArgs, null);
	}

	private bool OnProcessCheat_SetDungeonRunProgress(string func, string[] args, string rawArgs)
	{
		return ParseAdventureThenSetProgress(args, SetAdventureProgressMode.Progress);
	}

	private bool OnProcessCheat_SetDungeonRunVictory(string func, string[] args, string rawArgs)
	{
		return ParseAdventureThenSetProgress(args, SetAdventureProgressMode.Victory);
	}

	private bool OnProcessCheat_SetDungeonRunDefeat(string func, string[] args, string rawArgs)
	{
		return ParseAdventureThenSetProgress(args, SetAdventureProgressMode.Defeat);
	}

	private bool OnProcessCheat_ResetDungeonRunAdventure(string func, string[] args, string rawArgs)
	{
		AdventureDbId adventureDbId = ParseAdventureDbIdFromArgs(args, 0);
		if (adventureDbId == AdventureDbId.INVALID)
		{
			return true;
		}
		return ResetDungeonRunAdventure(adventureDbId, AdventureModeDbId.DUNGEON_CRAWL);
	}

	private bool ResetDungeonRunAdventure(AdventureDbId adventure, AdventureModeDbId mode)
	{
		if (adventure == AdventureDbId.INVALID)
		{
			return true;
		}
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventure, (int)mode);
		if (adventureDataRecord == null)
		{
			UIStatus.Get().AddError($"No Adventure data found for Adventure {adventure} Mode {mode}");
			return true;
		}
		if (adventureDataRecord.GameSaveDataServerKey == 0)
		{
			UIStatus.Get().AddError($"No GameSaveDataServerKey for Adventure {adventure} Mode {mode}!");
			return true;
		}
		ResetAdventureRunCommon_Server(adventureDataRecord.GameSaveDataServerKey);
		if (adventureDataRecord.GameSaveDataClientKey != 0)
		{
			ResetAdventureRunCommon_Client(adventureDataRecord.GameSaveDataClientKey);
		}
		UIStatus.Get().AddInfo($"Reset current run for Adventure {adventure} Mode {mode}");
		return true;
	}

	private bool OnProcessCheat_ResetDungeonRun_VO(string func, string[] args, string rawArgs)
	{
		AdventureDbId adventureDbId = ParseAdventureDbIdFromArgs(args, 0);
		if (adventureDbId == AdventureDbId.INVALID)
		{
			return true;
		}
		long value = 0L;
		ValidateAndParseLongAtIndex(1, args, out value);
		return ResetDungeonRun_VO(adventureDbId, value);
	}

	private bool ResetDungeonRun_VO(AdventureDbId adventure, long subkeyValue)
	{
		AdventureDungeonCrawlDisplay.s_shouldShowWelcomeBanner = true;
		switch (adventure)
		{
		case AdventureDbId.LOOT:
			Options.Get().SetBool(Option.HAS_JUST_SEEN_LOOT_NO_TAKE_CANDLE_VO, val: false);
			break;
		case AdventureDbId.GIL:
			Options.Get().SetBool(Option.HAS_SEEN_PLAYED_TESS, val: false);
			Options.Get().SetBool(Option.HAS_SEEN_PLAYED_DARIUS, val: false);
			Options.Get().SetBool(Option.HAS_SEEN_PLAYED_SHAW, val: false);
			Options.Get().SetBool(Option.HAS_SEEN_PLAYED_TOKI, val: false);
			break;
		}
		AdventureModeDbId adventureModeDbId = AdventureModeDbId.DUNGEON_CRAWL;
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventure, (int)adventureModeDbId);
		if (adventureDataRecord == null)
		{
			UIStatus.Get().AddError($"No Adventure data found for Adventure {adventure} Mode {adventureModeDbId}");
			return true;
		}
		if (adventureDataRecord.GameSaveDataClientKey == 0)
		{
			UIStatus.Get().AddError($"No GameSaveDataClientKey for Adventure {adventure} Mode {adventureModeDbId}!");
			return true;
		}
		ResetVOSubkeysForAdventure((GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey, subkeyValue);
		if (adventureDataRecord.GameSaveDataServerKey != 0)
		{
			ResetVOSubkeysForAdventure((GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey, subkeyValue);
		}
		UIStatus.Get().AddInfo($"You can now see all {adventure} VO again.");
		return true;
	}

	private bool ParseAdventureThenSetProgress(string[] args, SetAdventureProgressMode progressMode)
	{
		AdventureDbId adventureDbId = ParseAdventureDbIdFromArgs(args, 0);
		if (adventureDbId == AdventureDbId.INVALID)
		{
			return true;
		}
		string[] array = new string[args.Length - 1];
		Array.Copy(args, 1, array, 0, args.Length - 1);
		if (SetAdventureProgressCommon(adventureDbId, AdventureModeDbId.DUNGEON_CRAWL, array, progressMode))
		{
			UIStatus.Get().AddInfo($"Set Dungeon Run {progressMode} for {adventureDbId}");
		}
		return true;
	}

	private bool OnProcessCheat_SetKCVictory(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.LOOT, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo($"Set KC victory");
		}
		return true;
	}

	private bool OnProcessCheat_SetKCProgress(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.LOOT, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo($"Set KC progress");
		}
		return true;
	}

	private bool OnProcessCheat_SetKCDefeat(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.LOOT, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo($"Set KC defeat");
		}
		return true;
	}

	private bool OnProcessCheat_SetGILVictory(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo($"Set Witchwood victory");
		}
		return true;
	}

	private bool OnProcessCheat_SetGILProgress(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo($"Set Witchwood progress");
		}
		return true;
	}

	private bool OnProcessCheat_SetGILDefeat(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo($"Set Witchwood defeat");
		}
		return true;
	}

	private bool OnProcessCheat_SetGILBonus(string func, string[] args, string rawArgs)
	{
		OnProcessCheat_utilservercmd("util", new string[4] { "quest", "progress", "achieve=1010", "amount=4" }, "util quest progress achieve=1010 amount=4", null);
		UIStatus.Get().AddInfo($"Set Witchwood Bonus Challenge Active");
		Options.Get().SetBool(Option.HAS_SEEN_GIL_BONUS_CHALLENGE, val: false);
		return true;
	}

	private bool OnProcessCheat_SetTRLVictory(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.TRL, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo($"Set Rastakhan's Rumble victory");
		}
		return true;
	}

	private bool OnProcessCheat_SetTRLProgress(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.TRL, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo($"Set Rastakhan's Rumble progress");
		}
		return true;
	}

	private bool OnProcessCheat_SetTRLDefeat(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.TRL, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo($"Set Rastakhan's Rumble defeat");
		}
		return true;
	}

	private bool OnProcessCheat_SetDALProgress(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo($"Set Dalaran progress");
		}
		return true;
	}

	private bool OnProcessCheat_SetDALVictory(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo($"Set Dalaran victory");
		}
		return true;
	}

	private bool OnProcessCheat_SetDALDefeat(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo($"Set Dalaran defeat");
		}
		return true;
	}

	private bool OnProcessCheat_ResetDalaranAdventure(string func, string[] args, string rawArgs)
	{
		return ResetDungeonRunAdventure(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL);
	}

	private bool OnProcessCheat_ResetTavernBrawlAdventure(string func, string[] args, string rawArgs)
	{
		if (TavernBrawlManager.Get() == null)
		{
			UIStatus.Get().AddError("TavernBrawlManager is not initialized!");
			return true;
		}
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		if (mission == null)
		{
			UIStatus.Get().AddError("No Tavern Brawl Mission found");
			return true;
		}
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(mission.missionId);
		if (record == null)
		{
			UIStatus.Get().AddError("Could not find scenario for current tavern brawl mission");
			return true;
		}
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord(record.AdventureId, record.ModeId);
		if (adventureDataRecord == null)
		{
			UIStatus.Get().AddError("Could not find adventure data for current tavern brawl mission");
			return true;
		}
		ResetAdventureRunCommon_Server(adventureDataRecord.GameSaveDataServerKey);
		ResetAdventureRunCommon_Client(adventureDataRecord.GameSaveDataClientKey);
		UIStatus.Get().AddInfo($"Reset Tavern Brawl Adventure Progress");
		return true;
	}

	private bool OnProcessCheat_SetDALHeroicProgress(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo($"Set Dalaran Heroic progress");
		}
		return true;
	}

	private bool OnProcessCheat_SetDALHeroicVictory(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo($"Set Dalaran Heroic victory");
		}
		return true;
	}

	private bool OnProcessCheat_SetDALHeroicDefeat(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo($"Set Dalaran Heroic defeat");
		}
		return true;
	}

	private bool OnProcessCheat_ResetDalaranHeroicAdventure(string func, string[] args, string rawArgs)
	{
		return ResetDungeonRunAdventure(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL_HEROIC);
	}

	private bool OnProcessCheat_SetULDProgress(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo($"Set Uldum progress");
		}
		return true;
	}

	private bool OnProcessCheat_SetULDVictory(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo($"Set Uldum victory");
		}
		return true;
	}

	private bool OnProcessCheat_SetULDDefeat(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL, args, SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo($"Set Uldum defeat");
		}
		return true;
	}

	private bool OnProcessCheat_ResetUldumAdventure(string func, string[] args, string rawArgs)
	{
		return ResetDungeonRunAdventure(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL);
	}

	private bool OnProcessCheat_SetULDHeroicProgress(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo($"Set Uldum Heroic progress");
		}
		return true;
	}

	private bool OnProcessCheat_SetULDHeroicVictory(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo($"Set Uldum Heroic victory");
		}
		return true;
	}

	private bool OnProcessCheat_SetULDHeroicDefeat(string func, string[] args, string rawArgs)
	{
		if (SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo($"Set Uldum Heroic defeat");
		}
		return true;
	}

	private bool OnProcessCheat_ResetUldumHeroicAdventure(string func, string[] args, string rawArgs)
	{
		return ResetDungeonRunAdventure(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL_HEROIC);
	}

	private bool OnProcessCheat_ResetGILAdventure(string func, string[] args, string rawArgs)
	{
		return ResetDungeonRunAdventure(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL);
	}

	private static AdventureDbId ParseAdventureDbIdFromArgs(string[] args, int index)
	{
		AdventureDbId result = AdventureDbId.INVALID;
		if (args.Length <= index || string.IsNullOrEmpty(args[index]))
		{
			UIStatus.Get().AddError("You must provide an Adventure to operate on!  Ex: 'uld'");
			return result;
		}
		result = GetAdventureDbIdFromString(args[index]);
		if (result == AdventureDbId.INVALID)
		{
			UIStatus.Get().AddError($"{args[index]} does not map to a valid Adventure!");
			return result;
		}
		return result;
	}

	private static AdventureDbId GetAdventureDbIdFromString(string adventureString)
	{
		if (string.IsNullOrEmpty(adventureString))
		{
			return AdventureDbId.INVALID;
		}
		AdventureDbId adventureDbId = AdventureDbId.INVALID;
		try
		{
			adventureDbId = (AdventureDbId)Enum.Parse(typeof(AdventureDbId), adventureString, ignoreCase: true);
		}
		catch (ArgumentException)
		{
		}
		if (adventureDbId != 0)
		{
			return adventureDbId;
		}
		switch (adventureString.ToLower())
		{
		case "nax":
		case "naxx":
			return AdventureDbId.NAXXRAMAS;
		case "league":
			return AdventureDbId.LOE;
		case "karazhan":
			return AdventureDbId.KARA;
		case "icecrown":
			return AdventureDbId.ICC;
		case "kc":
		case "k&c":
		case "knc":
			return AdventureDbId.LOOT;
		case "witchwood":
			return AdventureDbId.GIL;
		case "rastakhan":
			return AdventureDbId.TRL;
		case "dal":
			return AdventureDbId.DALARAN;
		case "uld":
		case "tot":
			return AdventureDbId.ULDUM;
		case "ga":
		case "drg":
			return AdventureDbId.DRAGONS;
		default:
			return AdventureDbId.INVALID;
		}
	}

	private bool OnProcessCheat_UnlockLoadout(string func, string[] args, string rawArgs)
	{
		return UpdateAdventureLoadoutOptionsLockStateFromArgs(args, shouldLock: false);
	}

	private bool OnProcessCheat_LockLoadout(string func, string[] args, string rawArgs)
	{
		return UpdateAdventureLoadoutOptionsLockStateFromArgs(args, shouldLock: true);
	}

	private bool OnProcessCheat_ShowAdventureLoadingPopup(string func, string[] args, string rawArgs)
	{
		GameMgr.Get().Cheat_ShowTransitionPopup(GameType.GT_VS_AI, FormatType.FT_WILD, (int)AdventureConfig.Get().GetMission());
		if (AdventureConfig.Get().GetMission() == ScenarioDbId.INVALID)
		{
			UIStatus.Get().AddInfo("Showing generic popup, navigate to an Adventure scenario to customize the popup");
		}
		else
		{
			UIStatus.Get().AddInfo($"Showing loading popup for scenario {(int)AdventureConfig.Get().GetMission()}");
		}
		return true;
	}

	private bool OnProcessCheat_HideGameTransitionPopup(string func, string[] args, string rawArgs)
	{
		GameMgr.Get().HideTransitionPopup();
		UIStatus.Get().AddInfo("Hiding Transition Popup");
		return true;
	}

	private static GameSaveKeyId GetGameSaveServerKeyForAdventure(AdventureDbId adventureDbId, AdventureModeDbId adventureMode)
	{
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)adventureDbId && r.ModeId == (int)adventureMode);
		if (record == null)
		{
			Debug.LogErrorFormat("No AdventureDataDbfRecord found for Adventure {0} Mode {1}, unable to unlock loadout options!", adventureDbId, adventureMode);
			return GameSaveKeyId.INVALID;
		}
		return (GameSaveKeyId)record.GameSaveDataServerKey;
	}

	private bool UpdateAdventureLoadoutOptionsLockStateFromArgs(string[] args, bool shouldLock)
	{
		AdventureDbId adventure = ParseAdventureDbIdFromArgs(args, 0);
		if (adventure == AdventureDbId.INVALID)
		{
			return true;
		}
		GameSaveKeyId normalServerKey = GetGameSaveServerKeyForAdventure(adventure, AdventureModeDbId.DUNGEON_CRAWL);
		if (normalServerKey == (GameSaveKeyId)0)
		{
			UIStatus.Get().AddError(string.Concat("No ServerKey found for Adventure ", adventure, " Mode ", AdventureModeDbId.DUNGEON_CRAWL, ", unable to unlock loadout options!"));
			return true;
		}
		List<GameSaveKeyId> list = new List<GameSaveKeyId> { normalServerKey };
		GameSaveKeyId heroicServerKey = GetGameSaveServerKeyForAdventure(adventure, AdventureModeDbId.DUNGEON_CRAWL_HEROIC);
		if (heroicServerKey != 0)
		{
			list.Add(heroicServerKey);
		}
		GameSaveDataManager.Get().Request(list, delegate(bool success)
		{
			UpdateAdventureLoadoutOptionsLockStateCommon(adventure, normalServerKey, shouldLock);
			if (heroicServerKey != 0)
			{
				UpdateAdventureLoadoutOptionsLockStateCommon(adventure, heroicServerKey, shouldLock);
			}
			if (!success)
			{
				UIStatus.Get().AddInfo(string.Concat("Failed to request ServerKeys for Adventure ", adventure, ", not all loadout options may be unlocked properly!"));
			}
			else
			{
				UIStatus.Get().AddInfo(string.Format("{0} Loadout {1}", shouldLock ? "Lock" : "Unlock", adventure));
			}
		});
		return true;
	}

	private void UpdateLockSubkey(GameSaveKeyId serverKey, GameSaveKeySubkeyId subkey, long unlockValue, bool shouldLock)
	{
		if (serverKey != 0 && subkey != 0)
		{
			GameSaveDataManager.Get().GetSubkeyValue(serverKey, subkey, out long value);
			if (shouldLock && value != 0L)
			{
				InvokeSetGameSaveDataCheat(serverKey, subkey, 0L);
			}
			else if (!shouldLock && value < unlockValue)
			{
				InvokeSetGameSaveDataCheat(serverKey, subkey, unlockValue);
			}
		}
	}

	private void UpdateAdventureLoadoutOptionsLockStateCommon(AdventureDbId adventureDbId, GameSaveKeyId serverKey, bool shouldLock)
	{
		foreach (AdventureHeroPowerDbfRecord record in GameDbf.AdventureHeroPower.GetRecords((AdventureHeroPowerDbfRecord r) => r.AdventureId == (int)adventureDbId))
		{
			UpdateLockSubkey(serverKey, (GameSaveKeySubkeyId)record.UnlockGameSaveSubkey, record.UnlockValue, shouldLock);
		}
		foreach (AdventureDeckDbfRecord record2 in GameDbf.AdventureDeck.GetRecords((AdventureDeckDbfRecord r) => r.AdventureId == (int)adventureDbId))
		{
			UpdateLockSubkey(serverKey, (GameSaveKeySubkeyId)record2.UnlockGameSaveSubkey, record2.UnlockValue, shouldLock);
		}
		foreach (AdventureLoadoutTreasuresDbfRecord record3 in GameDbf.AdventureLoadoutTreasures.GetRecords((AdventureLoadoutTreasuresDbfRecord r) => r.AdventureId == (int)adventureDbId))
		{
			UpdateLockSubkey(serverKey, (GameSaveKeySubkeyId)record3.UnlockGameSaveSubkey, record3.UnlockValue, shouldLock);
			UpdateLockSubkey(serverKey, (GameSaveKeySubkeyId)record3.UpgradeGameSaveSubkey, record3.UpgradeValue, shouldLock);
		}
	}

	private void ResetAdventureRunCommon_Server(int key)
	{
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_SCENARIO_ID, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_DECK, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_ANOMALY_MODE, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_ANOMALY_MODE, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_LOST_TO, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CLASS, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_FOUGHT_LIST, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_FIGHT, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_NEXT_BOSS_FIGHT_UNDEFEATED, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_A, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_B, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_C, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_OPTION, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_SHRINE_OPTIONS, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_HISTORY, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_TREASURE, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_SHRINE, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_HEALTH, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_HEALTH, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_EVENT_1, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_EVENT_2, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_OVERRIDE, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, new long[0]);
		InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUELS_DRAFT_HERO_CHOICES, new long[0]);
	}

	private void ResetAdventureRunCommon_Client(int key)
	{
	}

	private bool SetAdventureProgressCommon(AdventureDbId adventureDbId, AdventureModeDbId adventureMode, string[] args, SetAdventureProgressMode mode)
	{
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)adventureDbId && r.ModeId == (int)adventureMode);
		if (record == null)
		{
			UIStatus.Get().AddError(string.Concat("No AdventureDataDbfRecord found for Adventure ", adventureDbId, " Mode ", adventureMode, ", unable to set Adventure progress!"));
			return false;
		}
		long value = 0L;
		if (mode != 0 && !ValidateAndParseLongAtIndex(0, args, out value))
		{
			UIStatus.Get().AddError("You must provide a valid number of bosses defeated!");
			return false;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)record.GameSaveDataServerKey;
		long value2 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out value2);
		bool flag = value2 > 0;
		if (!flag)
		{
			long value3 = 0L;
			if (GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, out value3) && (int)value3 != 0)
			{
				InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CLASS, new long[1] { value3 });
			}
		}
		long deckClass = 0L;
		if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CLASS, out deckClass) || (int)deckClass == 0)
		{
			deckClass = 4L;
			HashSet<TAG_CLASS> hashSet = new HashSet<TAG_CLASS>(GameUtils.ORDERED_HERO_CLASSES);
			List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)adventureDbId);
			GuestHeroDbfRecord guestHeroDbfRecord = null;
			foreach (AdventureGuestHeroesDbfRecord item in records)
			{
				GuestHeroDbfRecord record2 = GameDbf.GuestHero.GetRecord(item.GuestHeroId);
				if (hashSet.Contains(GameUtils.GetTagClassFromCardDbId(record2.CardId)))
				{
					guestHeroDbfRecord = record2;
					break;
				}
			}
			if (guestHeroDbfRecord != null)
			{
				TAG_CLASS tagClassFromCardDbId = GameUtils.GetTagClassFromCardDbId(guestHeroDbfRecord.CardId);
				if (tagClassFromCardDbId != 0)
				{
					deckClass = (long)tagClassFromCardDbId;
				}
			}
			InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CLASS, new long[1] { deckClass });
		}
		long value4 = 0L;
		ScenarioDbId[] array;
		if (record != null && record.DungeonCrawlSelectChapter)
		{
			if (!flag)
			{
				value4 = (long)AdventureConfig.Get().GetMission();
				if (value4 <= 0)
				{
					GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_SCENARIO_ID, out value4);
				}
				if (value4 > 0)
				{
					InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, new long[1] { value4 });
				}
			}
		}
		else if (adventureDbId == AdventureDbId.BOH || adventureDbId == AdventureDbId.BOM)
		{
			if (adventureDbId == AdventureDbId.BOH)
			{
				long num = deckClass;
				long num2 = num - 2;
				if ((ulong)num2 <= 8uL)
				{
					switch (num2)
					{
					case 1L:
						goto IL_02cb;
					case 8L:
						goto IL_02e3;
					case 3L:
						goto IL_02fb;
					case 4L:
						goto IL_0310;
					case 5L:
						goto IL_0325;
					case 6L:
						goto IL_033a;
					case 0L:
						goto IL_034f;
					}
				}
				array = new ScenarioDbId[8]
				{
					ScenarioDbId.BOH_JAINA_01,
					ScenarioDbId.BOH_JAINA_02,
					ScenarioDbId.BOH_JAINA_03,
					ScenarioDbId.BOH_JAINA_04,
					ScenarioDbId.BOH_JAINA_05,
					ScenarioDbId.BOH_JAINA_06,
					ScenarioDbId.BOH_JAINA_07,
					ScenarioDbId.BOH_JAINA_08
				};
			}
			else
			{
				long num = deckClass;
				_ = 10;
				array = new ScenarioDbId[8]
				{
					ScenarioDbId.BOM_01_Rokara_01,
					ScenarioDbId.BOM_01_Rokara_02,
					ScenarioDbId.BOM_01_Rokara_03,
					ScenarioDbId.BOM_01_Rokara_04,
					ScenarioDbId.BOM_01_Rokara_05,
					ScenarioDbId.BOM_01_Rokara_06,
					ScenarioDbId.BOM_01_Rokara_07,
					ScenarioDbId.BOM_01_Rokara_08
				};
			}
			goto IL_0386;
		}
		goto IL_03b8;
		IL_0325:
		array = new ScenarioDbId[8]
		{
			ScenarioDbId.BOH_VALEERA_01,
			ScenarioDbId.BOH_VALEERA_02,
			ScenarioDbId.BOH_VALEERA_03,
			ScenarioDbId.BOH_VALEERA_04,
			ScenarioDbId.BOH_VALEERA_05,
			ScenarioDbId.BOH_VALEERA_06,
			ScenarioDbId.BOH_VALEERA_07,
			ScenarioDbId.BOH_VALEERA_08
		};
		goto IL_0386;
		IL_0310:
		array = new ScenarioDbId[8]
		{
			ScenarioDbId.BOH_ANDUIN_01,
			ScenarioDbId.BOH_ANDUIN_02,
			ScenarioDbId.BOH_ANDUIN_03,
			ScenarioDbId.BOH_ANDUIN_04,
			ScenarioDbId.BOH_ANDUIN_05,
			ScenarioDbId.BOH_ANDUIN_06,
			ScenarioDbId.BOH_ANDUIN_07,
			ScenarioDbId.BOH_ANDUIN_08
		};
		goto IL_0386;
		IL_02cb:
		array = new ScenarioDbId[8]
		{
			ScenarioDbId.BOH_REXXAR_01,
			ScenarioDbId.BOH_REXXAR_02,
			ScenarioDbId.BOH_REXXAR_03,
			ScenarioDbId.BOH_REXXAR_04,
			ScenarioDbId.BOH_REXXAR_05,
			ScenarioDbId.BOH_REXXAR_06,
			ScenarioDbId.BOH_REXXAR_07,
			ScenarioDbId.BOH_REXXAR_08
		};
		goto IL_0386;
		IL_0386:
		if (value >= 0 && value < array.Length)
		{
			value4 = (long)array[value];
		}
		if (value4 > 0)
		{
			InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, new long[1] { value4 });
		}
		goto IL_03b8;
		IL_02fb:
		array = new ScenarioDbId[8]
		{
			ScenarioDbId.BOH_UTHER_01,
			ScenarioDbId.BOH_UTHER_02,
			ScenarioDbId.BOH_UTHER_03,
			ScenarioDbId.BOH_UTHER_04,
			ScenarioDbId.BOH_UTHER_05,
			ScenarioDbId.BOH_UTHER_06,
			ScenarioDbId.BOH_UTHER_07,
			ScenarioDbId.BOH_UTHER_08
		};
		goto IL_0386;
		IL_033a:
		array = new ScenarioDbId[8]
		{
			ScenarioDbId.BOH_THRALL_01,
			ScenarioDbId.BOH_THRALL_02,
			ScenarioDbId.BOH_THRALL_03,
			ScenarioDbId.BOH_THRALL_04,
			ScenarioDbId.BOH_THRALL_05,
			ScenarioDbId.BOH_THRALL_06,
			ScenarioDbId.BOH_THRALL_07,
			ScenarioDbId.BOH_THRALL_08
		};
		goto IL_0386;
		IL_03b8:
		if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, out value4) || value4 <= 0)
		{
			ScenarioDbfRecord record3 = GameDbf.Scenario.GetRecord((ScenarioDbfRecord r) => r.AdventureId == (int)adventureDbId && r.ModeId == (int)adventureMode);
			if (record3 != null && record3.ID > 0)
			{
				value4 = record3.ID;
				InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, new long[1] { value4 });
			}
		}
		if (AdventureUtils.SelectableHeroPowersExistForAdventure(adventureDbId))
		{
			long value5 = 0L;
			if (!flag && GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, out value5) && value5 > 0)
			{
				InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER, new long[1] { value5 });
			}
			if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER, out value5) || value5 <= 0)
			{
				AdventureHeroPowerDbfRecord record4 = GameDbf.AdventureHeroPower.GetRecord((AdventureHeroPowerDbfRecord r) => r.AdventureId == (int)adventureDbId && r.ClassId == deckClass);
				if (record4 != null)
				{
					value5 = record4.CardId;
					InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER, new long[1] { value5 });
				}
			}
		}
		if (AdventureUtils.SelectableDecksExistForAdventure(adventureDbId))
		{
			long adventureDeckId = 0L;
			List<long> values = null;
			if (!flag && GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_DECK, out adventureDeckId) && adventureDeckId > 0)
			{
				values = ((IEnumerable<DeckCardDbfRecord>)GameDbf.DeckCard.GetRecords((DeckCardDbfRecord r) => r.DeckId == adventureDeckId)).Select((Func<DeckCardDbfRecord, long>)((DeckCardDbfRecord r) => r.CardId)).ToList();
				InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, values.ToArray());
			}
			if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, out values) || values == null || values.Count <= 0)
			{
				AdventureDeckDbfRecord record5 = GameDbf.AdventureDeck.GetRecord((AdventureDeckDbfRecord r) => r.AdventureId == (int)adventureDbId && r.ClassId == deckClass);
				if (record5 != null)
				{
					adventureDeckId = record5.DeckId;
					if (adventureDeckId > 0)
					{
						values = ((IEnumerable<DeckCardDbfRecord>)GameDbf.DeckCard.GetRecords((DeckCardDbfRecord r) => r.DeckId == adventureDeckId)).Select((Func<DeckCardDbfRecord, long>)((DeckCardDbfRecord r) => r.CardId)).ToList();
						InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, values.ToArray());
					}
				}
			}
		}
		if (!flag && AdventureUtils.SelectableLoadoutTreasuresExistForAdventure(adventureDbId))
		{
			long value6 = 0L;
			if (GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, out value6) && value6 > 0)
			{
				GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, out List<long> values2);
				if (values2 == null)
				{
					values2 = new List<long>();
				}
				values2.Add(value6);
				InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, values2.ToArray());
			}
		}
		long[] array2 = null;
		switch (adventureDbId)
		{
		case AdventureDbId.LOOT:
			array2 = new long[8] { 47316L, 46311L, 46915L, 46338L, 46371L, 47307L, 47001L, 47210L };
			break;
		case AdventureDbId.GIL:
			array2 = new long[8] { 47903L, 48311L, 48182L, 48151L, 48196L, 48600L, 48942L, 48315L };
			break;
		case AdventureDbId.TRL:
			array2 = new long[8] { 53222L, 53223L, 53224L, 53225L, 53226L, 53227L, 53228L, 53229L };
			break;
		case AdventureDbId.DALARAN:
			array2 = new long[12]
			{
				53750L, 53779L, 53667L, 53558L, 53572L, 53636L, 53607L, 53309L, 53562L, 53483L,
				53714L, 53783L
			};
			break;
		case AdventureDbId.BOH:
		{
			long num = deckClass;
			long num3 = num - 2;
			if ((ulong)num3 <= 8uL)
			{
				switch (num3)
				{
				case 1L:
					goto IL_078e;
				case 8L:
					goto IL_07a6;
				case 3L:
					goto IL_07be;
				case 4L:
					goto IL_07d6;
				case 5L:
					goto IL_07eb;
				case 6L:
					goto IL_0800;
				case 0L:
					goto IL_0815;
				}
			}
			array2 = new long[8] { 63199L, 63201L, 63204L, 63205L, 63206L, 63207L, 63208L, 61382L };
			break;
		}
		case AdventureDbId.BOM:
		{
			long num = deckClass;
			_ = 10;
			array2 = new long[8] { 67655L, 67656L, 67657L, 67658L, 67659L, 67660L, 67661L, 67662L };
			break;
		}
		default:
			{
				array2 = new long[8] { 57319L, 57378L, 57322L, 57397L, 57573L, 53810L, 57387L, 56176L };
				break;
			}
			IL_0815:
			array2 = new long[8] { 71857L, 71865L, 71866L, 71867L, 71868L, 71869L, 71870L, 71871L };
			break;
			IL_0800:
			array2 = new long[8] { 71187L, 71188L, 71189L, 71190L, 71191L, 71192L, 71193L, 71194L };
			break;
			IL_07eb:
			array2 = new long[8] { 68015L, 68016L, 68017L, 68018L, 68019L, 68020L, 68021L, 68022L };
			break;
			IL_07d6:
			array2 = new long[8] { 66904L, 66902L, 66903L, 66904L, 66905L, 66906L, 66908L, 66909L };
			break;
			IL_07be:
			array2 = new long[8] { 61388L, 65557L, 65558L, 65559L, 61389L, 65560L, 65561L, 65562L };
			break;
			IL_07a6:
			array2 = new long[8] { 61390L, 64757L, 64758L, 64759L, 64760L, 64761L, 64762L, 64763L };
			break;
			IL_078e:
			array2 = new long[8] { 63834L, 63835L, 63836L, 61384L, 63837L, 61385L, 63838L, 63839L };
			break;
		}
		switch (mode)
		{
		case SetAdventureProgressMode.Victory:
			value = array2.Length;
			break;
		case SetAdventureProgressMode.Progress:
			value = Math.Min(value, array2.Length - 1);
			break;
		}
		int adventureBossesInRun = AdventureConfig.GetAdventureBossesInRun(GameUtils.GetWingRecordFromMissionId((int)value4));
		if (adventureBossesInRun > 0)
		{
			value = Math.Min(value, adventureBossesInRun - 1);
		}
		long value7 = 0L;
		ValidateAndParseLongAtIndex(1, args, out value7);
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, out List<long> values3);
		if (values3 == null)
		{
			values3 = new List<long>();
		}
		if (values3.Count > value)
		{
			int num4 = values3.Count - (int)value;
			values3.RemoveRange(values3.Count - num4, num4);
		}
		else
		{
			while (values3.Count < value)
			{
				values3.Add(array2[values3.Count]);
			}
		}
		InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, values3.ToArray());
		InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, new long[1] { (mode == SetAdventureProgressMode.Progress) ? 1 : 0 });
		InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, new long[0]);
		if (mode == SetAdventureProgressMode.Victory || mode == SetAdventureProgressMode.Defeat)
		{
			InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_TREASURE, new long[0]);
			InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT, new long[0]);
		}
		switch (mode)
		{
		case SetAdventureProgressMode.Victory:
			InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_LOST_TO, new long[0]);
			InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_FIGHT, new long[0]);
			break;
		case SetAdventureProgressMode.Defeat:
			InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_LOST_TO, new long[1] { array2[values3.Count] });
			InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_FIGHT, new long[0]);
			break;
		default:
			if (value7 == 0L && values3.Count < array2.Length)
			{
				value7 = array2[values3.Count];
			}
			InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_FIGHT, new long[1] { value7 });
			break;
		}
		InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, new long[1]);
		return true;
		IL_02e3:
		array = new ScenarioDbId[8]
		{
			ScenarioDbId.BOH_GARROSH_01,
			ScenarioDbId.BOH_GARROSH_02,
			ScenarioDbId.BOH_GARROSH_03,
			ScenarioDbId.BOH_GARROSH_04,
			ScenarioDbId.BOH_GARROSH_05,
			ScenarioDbId.BOH_GARROSH_06,
			ScenarioDbId.BOH_GARROSH_07,
			ScenarioDbId.BOH_GARROSH_08
		};
		goto IL_0386;
		IL_034f:
		array = new ScenarioDbId[8]
		{
			ScenarioDbId.BOH_MALFURION_01,
			ScenarioDbId.BOH_MALFURION_02,
			ScenarioDbId.BOH_MALFURION_03,
			ScenarioDbId.BOH_MALFURION_04,
			ScenarioDbId.BOH_MALFURION_05,
			ScenarioDbId.BOH_MALFURION_06,
			ScenarioDbId.BOH_MALFURION_07,
			ScenarioDbId.BOH_MALFURION_08
		};
		goto IL_0386;
	}

	private bool OnProcessCheat_SetAllPuzzlesInProgress(string func, string[] args, string rawArgs)
	{
		int gameSaveDataServerKey = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == 429).GameSaveDataServerKey;
		foreach (ScenarioDbfRecord record in GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == 429))
		{
			int gameSaveDataProgressSubkey = record.GameSaveDataProgressSubkey;
			int gameSaveDataProgressMax = record.GameSaveDataProgressMax;
			InvokeSetGameSaveDataCheat((GameSaveKeyId)gameSaveDataServerKey, (GameSaveKeySubkeyId)gameSaveDataProgressSubkey, new long[1] { gameSaveDataProgressMax });
		}
		UIStatus.Get().AddInfo($"Set All Boomsday Puzzles To Their Last Sub-Puzzle");
		return true;
	}

	private void InvokeSetGameSaveDataCheat(GameSaveKeyId key, GameSaveKeySubkeyId subkey, long value)
	{
		InvokeSetGameSaveDataCheat(key, subkey, new long[1] { value });
	}

	private void InvokeSetGameSaveDataCheat(GameSaveKeyId key, GameSaveKeySubkeyId subkey, long[] values)
	{
		InvokeSetGameSaveDataCheat((int)key, subkey, values);
	}

	private void InvokeSetGameSaveDataCheat(int key, GameSaveKeySubkeyId subkey, long[] values)
	{
		List<string> obj = new List<string> { key.ToString() };
		int num = (int)subkey;
		obj.Add(num.ToString());
		List<string> list = obj;
		if (values != null)
		{
			foreach (long num2 in values)
			{
				list.Add(num2.ToString());
			}
		}
		OnProcessCheat_SetGameSaveData("setgsd", list.ToArray(), string.Join(" ", list.ToArray()));
	}

	private bool OnProcessCheat_GetGameSaveData(string func, string[] args, string rawArgs)
	{
		GameSaveKeyId key = (GameSaveKeyId)0;
		GameSaveKeySubkeyId subkey = (GameSaveKeySubkeyId)0;
		if (!ValidateAndParseGameSaveDataKeyAndSubkey(args, out key, out subkey))
		{
			UIStatus.Get().AddError("You must provide valid key and subkeys!");
			return true;
		}
		args = new string[3]
		{
			"getgsd",
			"key=" + args[0],
			"subkey=" + args[1]
		};
		return OnProcessCheat_utilservercmd("util", args, rawArgs, null);
	}

	private bool ValidateAndParseLongAtIndex(int index, string[] args, out long value)
	{
		value = 0L;
		long result = 0L;
		if (args.Length <= index || !long.TryParse(args[index], out result))
		{
			return false;
		}
		value = result;
		return true;
	}

	private bool ValidateAndParseGameSaveDataKeyAndSubkey(string[] args, out GameSaveKeyId key, out GameSaveKeySubkeyId subkey)
	{
		key = (GameSaveKeyId)0;
		subkey = (GameSaveKeySubkeyId)0;
		long result = 0L;
		if (args.Length < 1 || !long.TryParse(args[0], out result) || result == 0L)
		{
			UIStatus.Get().AddError("You must provide a valid non-zero id for the key!");
			return false;
		}
		key = (GameSaveKeyId)result;
		long result2 = 0L;
		if (args.Length < 2 || !long.TryParse(args[1], out result2) || result2 == 0L)
		{
			UIStatus.Get().AddError("You must provide a valid non-zero id for the key!");
			return false;
		}
		subkey = (GameSaveKeySubkeyId)result2;
		return true;
	}

	private bool OnProcessCheat_ResetKC_VO(string func, string[] args, string rawArgs)
	{
		ValidateAndParseLongAtIndex(0, args, out var value);
		ResetDungeonRun_VO(AdventureDbId.LOOT, value);
		return true;
	}

	private bool OnProcessCheat_ResetGIL_VO(string func, string[] args, string rawArgs)
	{
		ValidateAndParseLongAtIndex(0, args, out var value);
		ResetDungeonRun_VO(AdventureDbId.GIL, value);
		return true;
	}

	private bool OnProcessCheat_UnlockHagatha(string func, string[] args, string rawArgs)
	{
		InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_SERVER_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HUNTER_RUN_WINS, new long[1] { 1L });
		InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_SERVER_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_WARRIOR_RUN_WINS, new long[1] { 1L });
		InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_SERVER_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_ROGUE_RUN_WINS, new long[1] { 1L });
		OnProcessCheat_utilservercmd("util", new string[4] { "quest", "progress", "achieve=1010", "amount=3" }, "util quest progress achieve=1010 amount=3", null);
		SetAdventureProgressCommon(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL, new string[1] { "7" }, SetAdventureProgressMode.Progress);
		InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_CHARACTER_SELECT_VO, new long[1] { 1L });
		InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_1_VO, new long[1] { 1L });
		InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_2_VO, new long[1] { 1L });
		InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_3_VO, new long[1] { 1L });
		InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_1_VO, new long[1] { 1L });
		InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_1_VO, new long[1] { 1L });
		InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_2_VO, new long[1] { 1L });
		return true;
	}

	private bool OnProcessCheat_ResetTRL_VO(string func, string[] args, string rawArgs)
	{
		ValidateAndParseLongAtIndex(0, args, out var value);
		ResetDungeonRun_VO(AdventureDbId.TRL, value);
		return true;
	}

	private bool OnProcessCheat_ResetDAL_VO(string func, string[] args, string rawArgs)
	{
		ValidateAndParseLongAtIndex(0, args, out var value);
		ResetDungeonRun_VO(AdventureDbId.DALARAN, value);
		return true;
	}

	private bool OnProcessCheat_ResetULD_VO(string func, string[] args, string rawArgs)
	{
		ValidateAndParseLongAtIndex(0, args, out var value);
		ResetDungeonRun_VO(AdventureDbId.ULDUM, value);
		return true;
	}

	private void ResetVOSubkeysForAdventure(GameSaveKeyId adventureGameSaveKey, long subkeyValue = 0L)
	{
		List<GameSaveKeySubkeyId> obj = new List<GameSaveKeySubkeyId>
		{
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_WING_COMPLETE_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_CLASSES_VO
		};
		List<GameSaveKeySubkeyId> list = new List<GameSaveKeySubkeyId>
		{
			GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_HERO_POWER_TUTORIAL_PROGRESS,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_CHARACTER_SELECT_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_WELCOME_BANNER_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_1_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_2_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_3_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_4_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_5_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_1_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_2_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_3_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_4_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_HERO_POWER_1_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_DECK_1_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_1_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_2_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOOK_REVEAL_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOOK_REVEAL_HEROIC_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_WING_UNLOCK_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_WINGS_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_WINGS_HEROIC_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_ANOMALY_UNLOCK_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_REWARD_PAGE_REVEAL_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_FINAL_BOSS_LOSS_1_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_FINAL_BOSS_LOSS_2_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_LOSS_1_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_WIN_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_2_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_1_VO,
			GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_MULLIGAN_2_VO,
			GameSaveKeySubkeyId.ADVENTURE_HAS_SEEN_ADVENTURE,
			GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_SHRINE_TUTORIAL_1_VO,
			GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_SHRINE_TUTORIAL_2_VO,
			GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_ENEMY_SHRINE_DIES_TUTORIAL_VO,
			GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_ENEMY_SHRINE_REVIVES_TUTORIAL_VO,
			GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_DIES_TUTORIAL_VO,
			GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_TIMER_TICK_TUTORIAL_VO,
			GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_LOST_TUTORIAL_VO,
			GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_TRANSFORMED_TUTORIAL_VO,
			GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_BOUNCED_TUTORIAL_VO
		};
		foreach (GameSaveKeySubkeyId item in obj)
		{
			long[] values = null;
			InvokeSetGameSaveDataCheat(adventureGameSaveKey, item, values);
		}
		foreach (GameSaveKeySubkeyId item2 in list)
		{
			long[] values2 = null;
			if (subkeyValue != 0L)
			{
				values2 = new long[1] { subkeyValue };
			}
			InvokeSetGameSaveDataCheat(adventureGameSaveKey, item2, values2);
		}
	}

	private bool OnProcessCheat_SetAdventureComingSoon(string func, string[] args, string rawArgs)
	{
		if (args.Length < 2)
		{
			UIStatus.Get().AddInfo("Usage: setadventurecomingsoon [ADVENTURE] [TRUE/FALSE]\nExample: setadventurecomingsoon GIL true");
			return false;
		}
		AdventureDbId adventureDbId = ParseAdventureDbIdFromArgs(args, 0);
		if (adventureDbId == AdventureDbId.INVALID)
		{
			return false;
		}
		bool result = false;
		if (!bool.TryParse(args[1], out result))
		{
			UIStatus.Get().AddError($"Unable to parse \"{args[1]}\". Please enter True or False.");
			return false;
		}
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureDbId);
		record.SetVar("COMING_SOON_EVENT", result ? "always" : "never");
		GameDbf.Adventure.ReplaceRecordByRecordId(record);
		string message = ((AdventureScene.Get() == null) ? "Success!" : "Success!\nBack out and re-enter to see the change.");
		UIStatus.Get().AddInfo(message);
		return true;
	}

	private bool OnProcessCheat_ResetSession_VO(string func, string[] args, string rawArgs)
	{
		NotificationManager.Get().ResetSoundsPlayedThisSession();
		return true;
	}

	private bool OnProcessCheat_SetVOChance_VO(string func, string[] args, string rawArgs)
	{
		float result = -1f;
		if (args.Length != 0 && float.TryParse(args[0], out result) && result >= 0f)
		{
			result = Mathf.Clamp(result, 0f, 1f);
		}
		VOChanceOverride = result;
		return true;
	}

	private BnetPlayer CreateCheatFriendlistItem(string name, FriendListType type, int leagueId, int starLevel, BnetProgramId programID, bool isFriend, bool isOnline)
	{
		return type switch
		{
			FriendListType.FRIEND => BnetFriendMgr.Get().Cheat_CreateFriend(name, leagueId, starLevel, programID, isOnline), 
			FriendListType.NEARBY => BnetNearbyPlayerMgr.Get().Cheat_CreateNearbyPlayer(name, leagueId, starLevel, programID, isFriend, isOnline), 
			FriendListType.FSG => FiresideGatheringManager.Get().Cheat_CreateFSGPatron(name, leagueId, starLevel, programID, isFriend, isOnline), 
			_ => null, 
		};
	}

	private bool OnProcessCheat_History(string func, string[] args, string rawArgs)
	{
		HistoryManager historyManager = HistoryManager.Get();
		if (historyManager == null)
		{
			return false;
		}
		if (args[0].ToLower() == "true" || args[0].ToLower() == "on" || args[0] == "1")
		{
			historyManager.EnableHistory();
		}
		if (args[0].ToLower() == "false" || args[0].ToLower() == "off" || args[0] == "0")
		{
			historyManager.DisableHistory();
		}
		return true;
	}

	private bool OnProcessCheat_IPAddress(string func, string[] args, string rawArgs)
	{
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		if (hostEntry.AddressList.Length != 0)
		{
			string text = "";
			IPAddress[] addressList = hostEntry.AddressList;
			foreach (IPAddress iPAddress in addressList)
			{
				text = text + iPAddress.ToString() + "\n";
			}
			UIStatus.Get().AddInfo(text, 10f);
		}
		return true;
	}

	private bool OnProcessCheat_Attribution(string func, string[] args, string rawArgs)
	{
		BlizzardAttributionManager.Get().SendAllEventsForTest();
		return true;
	}

	private bool OnProcessCheat_CRM(string func, string[] args, string rawArgs)
	{
		BlizzardCRMManager.Get().SendAllEventsForTest();
		UIStatus.Get().AddInfo("Test CRM telemetry sent!");
		return true;
	}

	private bool OnProcessCheat_Updater(string func, string[] args, string rawArgs)
	{
		string text = "USAGE: updater [cmd] [args]\\nCommands: speed, gamespeed\\nNotice: Unit of speed is bytes per second.\\n\n\\t0 = unlimited, -1 = turn off game streaming\\n\\tStore the speed permanently: speed 0 store";
		if (args.Length < 1 || args.Any((string a) => string.IsNullOrEmpty(a)))
		{
			UIStatus.Get().AddInfo(text, 10f);
			return true;
		}
		if (DownloadManager == null)
		{
			UIStatus.Get().AddInfo("DownloadManager is not ready yet!");
			return true;
		}
		string text2 = args[0];
		bool flag = true;
		bool flag2 = false;
		int num = 0;
		if (args.Length > 1)
		{
			num = int.Parse(args[1]);
			flag2 = args.Length > 2 && args[2].Equals("store");
		}
		else
		{
			flag = false;
		}
		string text3 = null;
		switch (text2)
		{
		case "help":
			text3 = text;
			break;
		case "speed":
			if (num < 0)
			{
				text3 = "Error: Cannot use the negative value!";
				break;
			}
			if (flag)
			{
				DownloadManager.MaxDownloadSpeed = num;
				text3 = "Set the download speed to " + num;
			}
			else
			{
				text3 = "The current speed is " + DownloadManager.MaxDownloadSpeed;
			}
			if (flag2)
			{
				Options.Get().SetInt(Option.MAX_DOWNLOAD_SPEED, num);
			}
			break;
		case "gamespeed":
			if (flag)
			{
				if (num < 0)
				{
					DownloadManager.InGameStreamingDefaultSpeed = num;
					text3 = "Turned off in game streaming";
				}
				else
				{
					DownloadManager.DownloadSpeedInGame = num;
					text3 = "Set the download speed in game to " + num;
				}
			}
			else
			{
				text3 = "The current speed in game is " + DownloadManager.DownloadSpeedInGame;
			}
			if (flag2 && num >= 0)
			{
				Options.Get().SetInt(Option.STREAMING_SPEED_IN_GAME, num);
			}
			break;
		}
		if (text3 != null)
		{
			UIStatus.Get().AddInfo(text3, 5f);
		}
		return true;
	}

	private bool OnProcessCheat_Assets(string func, string[] args, string rawArgs)
	{
		string message = AssetLoaderDebug.HandleCheat(func, args, rawArgs);
		UIStatus.Get().AddInfo(message);
		return true;
	}

	private bool OnProcessCheat_testproduct(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: testproduct <pmt_product_id>";
		if (args.Length < 1 || !long.TryParse(args[0], out var result))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		string text = StoreManager.Get().Catalog.DebugFillShopWithProduct(result);
		if (text == null)
		{
			UIStatus.Get().AddInfo($"Shop filled with product {result}", 10f);
		}
		else
		{
			UIStatus.Get().AddInfo($"Error: {text}", 10f);
		}
		return true;
	}

	private bool OnProcessCheat_testadventurestore(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: testadventurestore <wing_id> <is_full_adventure>";
		if (args.Length < 1 || !int.TryParse(args[0], out var result))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		bool boolVal = false;
		if (args.Length >= 2 && !GeneralUtils.TryParseBool(args[1], out boolVal))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		WingDbfRecord record = GameDbf.Wing.GetRecord(result);
		if (record == null)
		{
			UIStatus.Get().AddInfo($"wing {result} not found", 10f);
			return true;
		}
		if (AdventureProgressMgr.Get() == null)
		{
			UIStatus.Get().AddInfo("AdventureProgressMgr not initialized", 10f);
			return true;
		}
		int adventureId = record.AdventureId;
		ProductType productType = ProductType.PRODUCT_TYPE_WING;
		ShopType shopType = ShopType.ADVENTURE_STORE;
		int numItemsRequired = 0;
		int num = 0;
		switch (adventureId)
		{
		case 0:
			UIStatus.Get().AddInfo($"wing {result} is not part of an adventure.", 10f);
			return true;
		case 1:
		case 2:
		case 402:
		case 423:
		case 432:
			UIStatus.Get().AddInfo($"wing {result} is part of a free adventure.", 10f);
			return true;
		case 3:
			productType = ProductType.PRODUCT_TYPE_NAXX;
			shopType = ShopType.ADVENTURE_STORE;
			numItemsRequired = 1;
			break;
		case 4:
			productType = ProductType.PRODUCT_TYPE_BRM;
			shopType = ShopType.ADVENTURE_STORE;
			numItemsRequired = 1;
			break;
		case 8:
			productType = ProductType.PRODUCT_TYPE_LOE;
			shopType = ShopType.ADVENTURE_STORE;
			numItemsRequired = 1;
			break;
		case 10:
			productType = ProductType.PRODUCT_TYPE_WING;
			shopType = ShopType.ADVENTURE_STORE;
			numItemsRequired = 1;
			break;
		default:
			productType = ProductType.PRODUCT_TYPE_WING;
			if (boolVal)
			{
				shopType = ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET;
				num = record.PmtProductIdForThisAndRestOfAdventure;
				if (num == 0)
				{
					UIStatus.Get().AddInfo($"wing {result} has no product id defined to complete the adventure", 10f);
					return true;
				}
			}
			else
			{
				shopType = ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET;
				num = record.PmtProductIdForSingleWingPurchase;
				if (num == 0)
				{
					UIStatus.Get().AddInfo($"wing {result} has no product id defined by the single wing", 10f);
					return true;
				}
			}
			break;
		}
		ItemOwnershipStatus productItemOwnershipStatus = StoreManager.GetProductItemOwnershipStatus(productType, record.ID);
		if (productItemOwnershipStatus == ItemOwnershipStatus.OWNED)
		{
			UIStatus.Get().AddInfo($"Cannot show store where wing ownership status is {productItemOwnershipStatus.ToString()}", 10f);
		}
		StoreManager.Get().StartAdventureTransaction(productType, record.ID, null, null, shopType, numItemsRequired, useOverlayUI: false, null, num);
		return true;
	}

	private bool OnProcessCheat_refreshcurrency(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1 || !Enum.TryParse<CurrencyType>(args[0], ignoreCase: true, out var result))
		{
			string message = "USAGE: refreshcurrency <runestones|arcane_orbs>";
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		StoreManager.Get().GetCurrencyCache(result).MarkDirty();
		return true;
	}

	private bool OnProcessCheat_loadpersonalizedshop(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			string message = "USAGE: loadpersonalizedshop <page_id>";
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		string text = args[0];
		if (text.Equals("false", StringComparison.CurrentCultureIgnoreCase) || text.Equals("null", StringComparison.CurrentCultureIgnoreCase))
		{
			StoreManager.Get().SetPersonalizedShopPageAndRefreshCatalog(null);
		}
		else
		{
			StoreManager.Get().SetPersonalizedShopPageAndRefreshCatalog(text);
		}
		return true;
	}

	private bool OnProcessCheat_checkfornewquests(string func, string[] args, string rawArgs)
	{
		float result = 0f;
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]) && !float.TryParse(args[0], out result))
		{
			UIStatus.Get().AddInfo("checkfornewquests [delaySeconds]");
			return true;
		}
		QuestManager.Get().DebugScheduleCheckForNewQuests(result);
		return true;
	}

	private bool OnProcessCheat_showquestnotification(string func, string[] args, string rawArgs)
	{
		QuestPool.QuestPoolType poolType = QuestPool.QuestPoolType.DAILY;
		if (args.Length != 0)
		{
			poolType = EnumUtils.SafeParse(args[0], QuestPool.QuestPoolType.DAILY, ignoreCase: true);
		}
		QuestManager.Get().SimulateQuestNotificationPopup(poolType);
		return true;
	}

	private bool OnProcessCheat_showquestprogresstoast(string func, string[] args, string rawArgs)
	{
		string message = "showquestprogresstoast <quest_id>";
		if (!int.TryParse(args[0], out var result))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		if (GameDbf.Quest.GetRecord(result) == null)
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		QuestManager.Get().SimulateQuestProgress(result);
		return true;
	}

	private bool OnProcessCheat_showachievementtoast(string func, string[] args, string rawArgs)
	{
		string message = "showachievementtoast <achieve_id>";
		if (!int.TryParse(args[0], out var result))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		AchievementDataModel achievementDataModel = AchievementManager.Get().Debug_GetAchievementDataModel(result);
		if (achievementDataModel == null)
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		AchievementToast.DebugShowFake(achievementDataModel);
		return true;
	}

	private bool OnProcessCheat_showachievementreward(string func, string[] args, string rawArgs)
	{
		string message = "showachievementeward <achievement_id>";
		if (!int.TryParse(args[0], out var result))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		RewardScrollDataModel rewardScrollDataModel = AchievementFactory.CreateRewardScrollDataModel(result);
		if (rewardScrollDataModel == null)
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		RewardScroll.DebugShowFake(rewardScrollDataModel);
		return true;
	}

	private bool OnProcessCheat_showquestreward(string func, string[] args, string rawArgs)
	{
		string message = "showquestreward <quest_id>";
		if (!int.TryParse(args[0], out var result))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		if (GameDbf.Quest.GetRecord(result) == null)
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		RewardScroll.DebugShowFake(QuestManager.Get().CreateRewardScrollDataModelByQuestId(result));
		return true;
	}

	private bool OnProcessCheat_showtrackreward(string func, string[] args, string rawArgs)
	{
		string message = "showtrackreward <level> <forPaidTrack>";
		if (!int.TryParse(args[0], out var level))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		bool result = false;
		if (args.Length > 1)
		{
			bool.TryParse(args[1], out result);
		}
		RewardTrackLevelDbfRecord rewardTrackLevelDbfRecord = RewardTrackManager.Get().RewardTrackAsset.Levels.Where((RewardTrackLevelDbfRecord r) => r.Level == level).FirstOrDefault();
		if (rewardTrackLevelDbfRecord == null)
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		int num = (result ? rewardTrackLevelDbfRecord.PaidRewardList : rewardTrackLevelDbfRecord.FreeRewardList);
		if (num <= 0)
		{
			if (result)
			{
				UIStatus.Get().AddInfo($"No paid rewards for level {level}.");
			}
			else
			{
				UIStatus.Get().AddInfo($"No free rewards for level {level}.");
			}
			return true;
		}
		RewardScroll.DebugShowFake(RewardTrackFactory.CreateRewardScrollDataModel(num, level));
		return true;
	}

	private bool OnProcessCheat_showprogtileids(string func, string[] args, string rawArgs)
	{
		ProgressUtils.ShowDebugIds = !ProgressUtils.ShowDebugIds;
		return true;
	}

	private bool OnProcessCheat_simendofgamexp(string func, string[] args, string rawArgs)
	{
		string message = "simendofgamexp <scenario_id>";
		if (args.Length != 1 || !int.TryParse(args[0], out var result))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		RewardXpNotificationManager.Get().DebugSimScenario(result);
		return true;
	}

	private bool OnProcessCheat_shownotavernpasswarning(string func, string[] args, string rawArgs)
	{
		Shop.OpenTavernPassErrorPopup();
		return true;
	}

	private bool OnProcessCheat_showunclaimedtrackrewards(string func, string[] args, string rawArgs)
	{
		RewardTrackSeasonRoll.DebugShowFakeForgotTrackRewards();
		return true;
	}

	private bool OnProcessCheat_setlastrewardtrackseasonseen(string func, string[] args, string rawArgs)
	{
		int result = 0;
		if (args.Length != 0 && !int.TryParse(args[0], out result))
		{
			UIStatus.Get().AddInfo("setlastrewardtrackseasonseen <season_number>");
			return true;
		}
		if (!RewardTrackManager.Get().SetRewardTrackSeasonLastSeen(result))
		{
			UIStatus.Get().AddInfo("setlastrewardtrackseasonseen failed to set GSD value");
			return true;
		}
		UIStatus.Get().AddInfo($"Last reward track season seen = {result}");
		return true;
	}

	private bool OnProcessCheat_ShowAppRatingPrompt(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: apprating [cmd] \nCommands: clear, show";
		if (args.Length < 1 || args.Any((string a) => string.IsNullOrEmpty(a)))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		string text = args[0];
		if (!(text == "clear"))
		{
			if (text == "show")
			{
				MobileCallbackManager.RequestAppReview(forcePopupToShow: true);
				UIStatus.Get().AddInfo("Requesting app rating prompt.");
			}
		}
		else
		{
			Options.Get().SetInt(Option.APP_RATING_POPUP_COUNT, 0);
			UIStatus.Get().AddInfo("Resetting app rating prompt count.");
		}
		return true;
	}
}
