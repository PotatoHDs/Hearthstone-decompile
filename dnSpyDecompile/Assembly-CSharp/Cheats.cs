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

// Token: 0x02000886 RID: 2182
public class Cheats : IService
{
	// Token: 0x0600761C RID: 30236 RVA: 0x0025E8FC File Offset: 0x0025CAFC
	private static global::Map<Global.SoundCategory, bool> InitAudioChannelMap()
	{
		global::Map<Global.SoundCategory, bool> map = new global::Map<Global.SoundCategory, bool>();
		foreach (object obj in Enum.GetValues(typeof(Global.SoundCategory)))
		{
			int key = (int)obj;
			map.Add((Global.SoundCategory)key, true);
		}
		return map;
	}

	// Token: 0x170006F4 RID: 1780
	// (get) Token: 0x0600761D RID: 30237 RVA: 0x000274B4 File Offset: 0x000256B4
	private IGameDownloadManager DownloadManager
	{
		get
		{
			return GameDownloadManagerProvider.Get();
		}
	}

	// Token: 0x170006F5 RID: 1781
	// (get) Token: 0x0600761E RID: 30238 RVA: 0x0025E968 File Offset: 0x0025CB68
	public static bool ShowFakeBreakingNews
	{
		get
		{
			return Vars.Key("Cheats.ShowFakeBreakingNews").GetBool(false);
		}
	}

	// Token: 0x170006F6 RID: 1782
	// (get) Token: 0x0600761F RID: 30239 RVA: 0x0025E97A File Offset: 0x0025CB7A
	public static bool ShowFakeNerfedCards
	{
		get
		{
			return Vars.Key("Cheats.ShowFakeNerfedCards").GetBool(false);
		}
	}

	// Token: 0x170006F7 RID: 1783
	// (get) Token: 0x06007620 RID: 30240 RVA: 0x0025E98C File Offset: 0x0025CB8C
	public static bool ShowFakeAddedCards
	{
		get
		{
			return Vars.Key("Cheats.ShowFakeAddedCards").GetBool(false);
		}
	}

	// Token: 0x170006F8 RID: 1784
	// (get) Token: 0x06007621 RID: 30241 RVA: 0x0025E99E File Offset: 0x0025CB9E
	public static bool SimulateWebPaneLogin
	{
		get
		{
			return !HearthstoneApplication.IsPublic() && Vars.Key("Cheats.SimulateWebPaneLogin").GetBool(false);
		}
	}

	// Token: 0x06007622 RID: 30242 RVA: 0x0025E9B9 File Offset: 0x0025CBB9
	public static Cheats Get()
	{
		if (Cheats.s_instance == null)
		{
			Cheats.s_instance = HearthstoneServices.Get<Cheats>();
		}
		return Cheats.s_instance;
	}

	// Token: 0x06007623 RID: 30243 RVA: 0x0025E9D1 File Offset: 0x0025CBD1
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		CheatMgr cheatMgr = serviceLocator.Get<CheatMgr>();
		if (HearthstoneApplication.IsInternal())
		{
			cheatMgr.RegisterCategory("help");
			cheatMgr.RegisterCheatHandler("help", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_help), "Get help for a specific command or list of commands", "<command name>", "");
			cheatMgr.RegisterCheatHandler("example", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_example), "Run an example of this command if one exists", "<command name>", null);
			cheatMgr.RegisterCheatHandler("error", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_error), "Make the client throw an arbitrary error.", "<warning | fatal | exception> <optional error message>", "warning This is an example warning message.");
			cheatMgr.RegisterCategory("bug");
			cheatMgr.RegisterCheatHandler("bug", new CheatMgr.ProcessCheatCallback(this.On_ProcessCheat_bug), null, null, null);
			cheatMgr.RegisterCheatHandler("Bug", new CheatMgr.ProcessCheatCallback(this.On_ProcessCheat_bug), null, null, null);
			cheatMgr.RegisterCheatHandler("crash", new CheatMgr.ProcessCheatCallback(this.On_ProcessCheat_crash), null, null, null);
			cheatMgr.RegisterCheatHandler("anr", new CheatMgr.ProcessCheatCallback(this.On_ProcessCheat_ANR), null, null, null);
			cheatMgr.RegisterCategory("general");
			cheatMgr.RegisterCheatHandler("cheat", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_cheat), "Send a cheat command to the server", "<command> <arguments>", null);
			cheatMgr.RegisterCheatAlias("cheat", new string[]
			{
				"c"
			});
			cheatMgr.RegisterCheatHandler("timescale", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_timescale), "Cheat to change the timescale", "<timescale>", "0.5");
			cheatMgr.RegisterCheatHandler("util", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_utilservercmd), "Run a cheat on the UTIL server you're connected to.", "[subcommand] [subcommand args]", "help");
			cheatMgr.RegisterCheatHandler("game", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_gameservercmd), "[NYI] Run a cheat on the GAME server you're connected to.", "[subcommand] [subcommand args]", "help");
			Network.Get().RegisterNetHandler(DebugCommandResponse.PacketID.ID, new Network.NetHandler(this.OnProcessCheat_utilservercmd_OnResponse), null);
			cheatMgr.RegisterCheatHandler("event", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_EventTiming), "View event timings to see if they're active.", "[event=event_name]", "");
			cheatMgr.RegisterCheatHandler("audiochannel", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_audioChannel), "Turn on/off an audio channel.", "[audio channel name] [on/off]", "fx off");
			cheatMgr.RegisterCheatHandler("audiochannelgroup", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_audioChannelGroup), "Turn on/off a group of audio channels.", "[audio channel group name] [on/off]", "vo off");
			cheatMgr.RegisterCategory("igm");
			cheatMgr.RegisterCheatHandler("igm", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_igm), "Register the content type and show it by using the debug UI", "<content_type>", null);
			cheatMgr.RegisterCheatHandler("msgui", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_msgui), "Message popup ui", "<register|show> [<text|shop>]", null);
			cheatMgr.RegisterCategory("program");
			cheatMgr.RegisterCheatHandler("reset", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_reset), "Reset the client", null, null);
			cheatMgr.RegisterCategory("gameplay");
			cheatMgr.RegisterCheatHandler("board", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_board), "Set which board will be loaded on the next game", "<BRM|STW|GVG>", "BRM");
			cheatMgr.RegisterCheatHandler("playertags", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_playerTags), "Set these tags on your player in the next game (limit 20)", "<TagId1=TagValue1,TagId2=TagValue2,...,TagIdN=TagValueN>", "427=10,419=1");
			cheatMgr.RegisterCheatHandler("togglespeechbubbles", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_speechBubbles), "Toggle on/off speech bubbles.", "", "");
			cheatMgr.RegisterCheatHandler("disconnect", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_disconnect), "Disconnects you from a game in progress (disconnects from game server only). If you want to disconnect from just battle.net, use 'disconnect bnet'.", null, null);
			cheatMgr.RegisterCheatHandler("restart", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_restart), "Restarts any non-PvP game.", null, null);
			cheatMgr.RegisterCheatHandler("autohand", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_autohand), "Set whether PhoneUI automatically hides your hand after playing a card", "<true/false>", "true");
			cheatMgr.RegisterCheatHandler("endturn", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_endturn), "End your turn", null, null);
			cheatMgr.RegisterCheatHandler("scenario", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_scenario), "Launch a scenario.", "<scenario_id> [<game_type_id>] [<deck_name>|<deck_id>] [<game_format>]", null);
			cheatMgr.RegisterCheatAlias("scenario", new string[]
			{
				"mission"
			});
			cheatMgr.RegisterCheatHandler("aigame", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_aigame), "Launch a game vs an AI using specified deck code.", "<deck_code_string> [<game_format>]", null);
			cheatMgr.RegisterCheatHandler("loadsnapshot", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_loadSnapshot), "Load a snapshot file from local disk.", "<replayfilename>", null);
			cheatMgr.RegisterCheatHandler("skipgetgamestate", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SkipSendingGetGameState), "Skip sending GetGameState packet in Gameplay.Start().", null, null);
			cheatMgr.RegisterCheatHandler("sendgetgamestate", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SendGetGameState), "Send GetGameState packet.", null, null);
			cheatMgr.RegisterCheatHandler("auto_exportgamestate", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_autoexportgamestate), "Save JSON file serializing some of GameState", null, null);
			cheatMgr.RegisterCheatHandler("opponentname", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_OpponentName), "Set the Opponent name", "", "The Innkeeper");
			cheatMgr.RegisterCheatHandler("history", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_History), "disable/enable history", "", "true");
			cheatMgr.RegisterCheatHandler("settag", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_settag), "Sets a tag on an entity to a value", "settag <tag_id> <entity_id> <tag_value>", null);
			cheatMgr.RegisterCheatHandler("thinkemotes", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_playAllThinkEmotes), "Plays all of the think lines for the specified player's hero", null, null);
			cheatMgr.RegisterCheatHandler("playemote", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_playEmote), "Play the emote for the specified player's hero", "playemote <emote_type> <player>", null);
			cheatMgr.RegisterCheatHandler("heropowervo", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_playAllMissionHeroPowerLines), "Plays all the hero power lines associated with this mission", null, null);
			cheatMgr.RegisterCheatHandler("idlevo", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_playAllMissionIdleLines), "Plays all idle lines associated with this mission", null, null);
			cheatMgr.RegisterCheatHandler("debugscript", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_debugscript), "Toggles script debugging for a specific power", "debugscript <power_guid>", null);
			cheatMgr.RegisterCheatHandler("scriptdebug", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_debugscript), "Toggles script debugging for a specific power", "scriptdebug <power_guid>", null);
			cheatMgr.RegisterCheatHandler("disablescriptdebug", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_disablescriptdebug), "Disables all script debugging on the server", "disablescriptdebug", null);
			cheatMgr.RegisterCheatHandler("disabledebugscript", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_disablescriptdebug), "Disables all script debugging on the server", "disabledebugscript", null);
			cheatMgr.RegisterCheatHandler("printpersistentlist", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_printpersistentlist), "Prints all persistent lists for a particular entity. Call it with no entity to print ALL persistent lists on ALL entities", "printpersistentlist [entity_id]", null);
			cheatMgr.RegisterCheatHandler("printpersistentlists", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_printpersistentlist), "Prints all persistent lists for a particular entity. Call it with no entity to print ALL persistent lists on ALL entities", "printpersistentlists [entity_id]", null);
			cheatMgr.RegisterCategory("collection");
			cheatMgr.RegisterCheatHandler("collectionfirstxp", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_collectionfirstxp), "Set the number of page and cover flips to zero", "", "");
			cheatMgr.RegisterCheatHandler("resettips", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_resettips), "Resets Innkeeper tips for collection manager", "", "");
			cheatMgr.RegisterCheatHandler("cardchangepopup", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_cardchangepopup), "Show a Card Change popup.", "[showAddition] [useFakeData] [numFakeCards]", "false true 3");
			cheatMgr.RegisterCheatHandler("cardchangereset", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_cardchangereset), "Reset the record of which changed cards have already been seen.", null, null);
			cheatMgr.RegisterCheatHandler("loginpopupsequence", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_loginpopupsequence), "Show any active login popup sequences.", null, null);
			cheatMgr.RegisterCheatHandler("loginpopupreset", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_loginpopupreset), "Reset game save data for login popup sequences.", null, null);
			cheatMgr.RegisterCheatHandler("onlygold", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_onlygold), "In collection manager, do you want to see gold, nogold, or both?", "<command name>", "");
			cheatMgr.RegisterCheatHandler("exportcards", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_exportcards), "Export images of cards", null, null);
			cheatMgr.RegisterCategory("cosmetics");
			cheatMgr.RegisterCheatHandler("defaultcardback", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_favoritecardback), "Set your favorite cardback as if through the collection manager", "<cardback id>", null);
			cheatMgr.RegisterCheatHandler("favoritecardback", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_favoritecardback), "Set your favorite cardback as if through the collection manager", "<cardback id>", null);
			cheatMgr.RegisterCheatHandler("favoritehero", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_favoritehero), "Change your favorite hero for a class (only works from CollectionManager)", "<class_id> <hero_card_id> <hero_premium>", null);
			cheatMgr.RegisterCheatHandler("exportcardbacks", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_exportcardbacks), "Export images of card backs", null, null);
			cheatMgr.RegisterCategory("legacy quests and rewards");
			cheatMgr.RegisterCheatHandler("questcompletepopup", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_questcompletepopup), "Shows the quest complete achievement screen", "<quest_id>", "58");
			cheatMgr.RegisterCheatHandler("questprogresspopup", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_questprogresspopup), "Pop up a quest progress toast", "<title> <description> <progress> <maxprogress>", "Hello World 3 10");
			cheatMgr.RegisterCheatHandler("questwelcome", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_questwelcome), "Open list of daily quests", "<fromLogin>", "true");
			cheatMgr.RegisterCheatHandler("newquestvisual", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_newquestvisual), "Shows a new quest tile, only usable while a quest popup is active", null, null);
			cheatMgr.RegisterCheatHandler("fixedrewardcomplete", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_fixedrewardcomplete), "Shows the visual for a fixed reward", "<fixed_reward_map_id>", null);
			cheatMgr.RegisterCheatHandler("rewardboxes", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_rewardboxes), "Open the reward box screen with example rewards", "<card|cardback|gold|dust|random> <num_boxes>", "");
			cheatMgr.RegisterCategory("shop");
			cheatMgr.RegisterCheatHandler("storepassword", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_storepassword), "Show store challenge popup", "", "");
			cheatMgr.RegisterCheatHandler("testproduct", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_testproduct), "Fill Shop with a product", "<pmt_product_id>", null);
			cheatMgr.RegisterCheatHandler("testadventurestore", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_testadventurestore), "Open adventure store for a wing", "<wing_id> <is_full_adventure>", null);
			cheatMgr.RegisterCheatHandler("refreshcurrency", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_refreshcurrency), "Refresh currency balance", "<runestones|arcane_orbs>", null);
			cheatMgr.RegisterCheatHandler("loadpersonalizedshop", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_loadpersonalizedshop), "Load personalized shop", "<page_id>", null);
			cheatMgr.RegisterCategory("iks");
			cheatMgr.RegisterCheatHandler("iks", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_iks), "Open InnKeepersSpecial with a custom url", "<url>", null);
			cheatMgr.RegisterCheatHandler("iksaction", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_iksgameaction), "Execute a game action as if IKS was clicked.", null, null);
			cheatMgr.RegisterCheatHandler("iksseen", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_iksseen), "Determine if an IKS message should be seen by its game action.", null, null);
			cheatMgr.RegisterCategory("rank");
			cheatMgr.RegisterCheatHandler("seasondialog", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_seasondialog), "Open the season end dialog", "<rank> [standard|wild|classic]", "bronze5 wild");
			cheatMgr.RegisterCheatHandler("rankrefresh", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_rankrefresh), "Request medalinfo from server and show rankchange twoscoop after receiving it", null, null);
			cheatMgr.RegisterCheatHandler("rankchange", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_rankchange), "Show a fake rankchange twoscoop", "[rank] [up|down|win|loss] [wild] [winstreak] [chest]", "bronze5 up chest");
			cheatMgr.RegisterCheatHandler("rankreward", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_rankreward), "Show a fake RankedRewardDisplay for rank (or all ranks up to a rank)", "<rank> [standard|wild|classic|all]", "bronze5 all");
			cheatMgr.RegisterCheatHandler("rankcardback", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_rankcardback), "Show a fake RankedCardBackProgressDisplay", "<wins> [season_id]", "5 75");
			cheatMgr.RegisterCheatHandler("easyrank", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_easyrank), "Easier cheat command to set your rank on the util server", "<rank>", "16");
			cheatMgr.RegisterCheatHandler("resetrotationtutorial", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_resetrotationtutorial), "Cause the user to see the Set Rotation Tutorial again.", "<newbie|veteran>", "newbie|veteran");
			cheatMgr.RegisterCheatHandler("setrotationevent", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_setrotationevent), string.Format("Trigger the {0} event, causing some card sets to be rotated.", SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021), "<bypass intro> <seconds until event>", "true 3");
			cheatMgr.RegisterCheatHandler("rankdebug", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_rankdebug), "Display debug information regarding rank / rating", "[show|off] <standard/wild>", "show standard");
			cheatMgr.RegisterCheatHandler("resetrankedintro", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_resetrankedintro), "Reset game save data values for various tutorial elements for ranked play.", null, null);
			cheatMgr.RegisterCategory("sound/vo");
			cheatMgr.RegisterCheatHandler("playnullsound", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_playnullsound), "Tell SoundManager to play a null sound.", null, null);
			cheatMgr.RegisterCheatHandler("playaudio", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_playaudio), "Play an audio file by name", null, null);
			cheatMgr.RegisterCheatHandler("quote", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_quote), "", "<character> <line> [sound]", "Innkeeper VO_INNKEEPER_FORGE_COMPLETE_22 VO_INNKEEPER_ARENA_COMPLETE");
			cheatMgr.RegisterCheatHandler("narrative", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_narrative), "Show a narrative popup from an achievement", null, null);
			cheatMgr.RegisterCheatHandler("narrativedialog", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_narrativedialog), "Show a narrative dialog sequence popup", null, null);
			cheatMgr.RegisterCategory("game modes");
			cheatMgr.RegisterCheatHandler("arena", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_arena), "Runs various arena cheats.", "[subcommand] [subcommand args]", "help");
			cheatMgr.RegisterCheatHandler("retire", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_retire), "Retires your draft deck", "", "");
			cheatMgr.RegisterCheatHandler("battlegrounds", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_battlegrounds), "Queue for a game of Battlegrounds.", null, null);
			cheatMgr.RegisterCheatHandler("tb", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_tavernbrawl), "Run a variety of Tavern Brawl related commands", "[subcommand] [subcommand args]", "view");
			cheatMgr.RegisterCheatHandler("resetTavernBrawlAdventure", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetTavernBrawlAdventure), "Reset the current Tavern Brawl Adventure progress", null, null);
			cheatMgr.RegisterCheatHandler("returningplayer", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_returningplayer), "Set the Returning Player progress", "<0|1|2|3>", "1");
			cheatMgr.RegisterCheatHandler("duels", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_duels), "Run a variety of Duels related commands", "[subcommand] [subcommand args]", "help");
			cheatMgr.RegisterCategory("ui");
			cheatMgr.RegisterCheatHandler("demotext", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_demotext), "", "<line>", "HelloWorld!");
			cheatMgr.RegisterCheatHandler("popuptext", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_popuptext), "show a popup notification", "<line>", "HelloWorld!");
			cheatMgr.RegisterCheatHandler("alerttext", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_alerttext), "show a popup alert", "<line>", "HelloWorld!");
			cheatMgr.RegisterCheatHandler("logtext", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_logtext), "log a line of text", "<level> <line>", "warning WatchOutWorld!");
			cheatMgr.RegisterCheatHandler("logenable", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_logenable), "temporarily enables a logger", "<logger> <subtype> <enabled>", "Store file/screen/console true");
			cheatMgr.RegisterCheatHandler("loglevel", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_loglevel), "temporarily sets the min level of a logger", "<logger> <level>", "Store debug");
			cheatMgr.RegisterCheatHandler("reloadgamestrings", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_reloadgamestrings), "Reload all game strings from GLUE/GLOBAL/etc.", null, null);
			cheatMgr.RegisterCheatHandler("attn", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_userattentionmanager), "Prints out what UserAttentionBlockers, if any, are currently active.", null, null);
			cheatMgr.RegisterCheatHandler("banner", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_banner), "Shows the specified wooden banner (supply a banner_id). If none is supplied, it'll show the latest known banner. Use 'banner list' to view all known banners.", "<banner_id> | list", "33");
			cheatMgr.RegisterCheatHandler("browser", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_browser), "Run In-Game Browser related commands", "[subcommand]", "show");
			cheatMgr.RegisterCheatHandler("notice", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_notice), "Show a notice", "<gold|arcane_orbs|dust|booster|card|cardback|tavern_brawl_rewards|event|license> [data]", null);
			cheatMgr.RegisterCheatHandler("load_widget", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_LoadWidget), "Show a widget given a specific guid. If `CHEATED_STATE` exists on a visual controller in the widget, it will be triggered and should be used to help get the widget into the proper location on the screen or any other special test only setup that is needed.", null, null);
			cheatMgr.RegisterCheatHandler("clear_widgets", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ClearWidgets), "Remove any widgets that were created via the load_widget cheat", null, null);
			cheatMgr.RegisterCheatHandler("serverlog", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ServerLog), "Log a ServerScript message", null, null);
			cheatMgr.RegisterCheatHandler("dialogevent", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_dialogEvent), "Choose a category of dialog event, and force it to be run again.", "<event_type> or \"reset\"", null);
			cheatMgr.RegisterCategory("social");
			cheatMgr.RegisterCheatHandler("spectate", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_spectate), "Connects to a game server to spectate", "<ip_address> <port> <game_handle> <spectator_password> [gameType] [missionId]", null);
			cheatMgr.RegisterCheatHandler("party", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_party), "Run a variety of party related commands", "[sub command] [subcommand args]", "list");
			cheatMgr.RegisterCheatHandler("raf", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_raf), "Run a RAF UI related commands", "[subcommand]", "showprogress");
			cheatMgr.RegisterCheatHandler("flist", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_friendlist), "Run various friends list cheats.", "[subcommand] [subcommand args]", "add remove");
			cheatMgr.RegisterCheatHandler("fsg", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_fsg), "Run a variety of Fireside Gathering related commands", "[subcommand] [subcommand args]", "view");
			cheatMgr.RegisterCheatHandler("gps", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_GPS), "Modify GPS information in editor", "[subcommand] [subcommand args]", "view");
			cheatMgr.RegisterCheatHandler("wifi", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_Wifi), "Modify WIFI information in editor", "[subcommand] [subcommand args]", "view");
			cheatMgr.RegisterCheatHandler("social", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_social), "View information about the social list (friends, nearby players, FSG patrons, etc)", "[subcommand] [subcommand args]", "list");
			cheatMgr.RegisterCheatHandler("playstartemote", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_playStartEmote), " the appropriate start, mirror start, or custom start emote on first the enemy hero, then the friendly hero", null, null);
			cheatMgr.RegisterCheatHandler("getbgdenylist", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_getBattlegroundDenyList), "Get Battleground deny list", null, null);
			cheatMgr.RegisterCheatHandler("getbgminionpool", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_getBattlegroundMinionPool), "Get Battleground minion pool", null, null);
			cheatMgr.RegisterCategory("device");
			cheatMgr.RegisterCheatHandler("lowmemorywarning", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_lowmemorywarning), "Simulate a low memory warning from mobile.", null, null);
			cheatMgr.RegisterCheatHandler("mobile", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_mobile), "Run Mobile related commands", "subcommand [subcommand args]", "subcommand:login|push|ngdp subcommand args:clear|register|logout");
			cheatMgr.RegisterCheatHandler("edittextdebug", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_edittextdebug), "Toggle EditText debugging", null, null);
			cheatMgr.RegisterCategory("streaming");
			cheatMgr.RegisterCheatHandler("setupdateintention", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_UpdateIntention), "Set the next \"goal\" for the runtime update manager", "[UpdateIntention]", null);
			cheatMgr.RegisterCheatHandler("updater", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_Updater), "Modify the properties of Updater", "[subcommand] [subcommand args]", "speed");
			cheatMgr.RegisterCategory("assets");
			cheatMgr.RegisterCheatHandler("printassethandles", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_Assets), "Prints outstanding AssetHandles", "[filter]", null);
			cheatMgr.RegisterCheatHandler("printassetbundles", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_Assets), "Prints open AssetBundles", "[filter]", null);
			cheatMgr.RegisterCheatHandler("orphanasset", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_Assets), "Orphans an AssetHandle", null, null);
			cheatMgr.RegisterCheatHandler("orphanprefab", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_Assets), "Orphans a shared prefab", null, null);
			cheatMgr.RegisterCategory("account data");
			cheatMgr.RegisterCheatHandler("account", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_account), "Account management cheat", null, null);
			cheatMgr.RegisterCheatHandler("cloud", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_cloud), "Run Cloud Storage related commands", "[subcommand]", "set");
			cheatMgr.RegisterCheatHandler("tempaccount", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_tempaccount), "Run Temporary Account related commands", "[subcommand]", "dialog");
			cheatMgr.RegisterCheatHandler("getgsd", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_GetGameSaveData), "Request the value of a particular Game Save Data subkey.", "[key] [subkey]", "24 13");
			cheatMgr.RegisterCheatHandler("gsd", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_GetGameSaveData), "Request the value of a particular Game Save Data subkey.", "[key] [subkey]", "24 13");
			cheatMgr.RegisterCheatHandler("setgsd", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetGameSaveData), "Set the value(s) of a Game Save Data subkey. Can provide multiple values to set a list.", "[key] [subkey] [int_value]", "24 13 2");
			cheatMgr.RegisterCategory("adventure");
			cheatMgr.RegisterCheatHandler("adventureChallengeUnlock", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_adventureChallengeUnlock), "Show adventure challenge unlock", "<wing number>", null);
			cheatMgr.RegisterCheatHandler("advevent", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_advevent), "Trigger an AdventureWingEventTable event.", "<event name>", "PlateOpen");
			cheatMgr.RegisterCheatHandler("showadventureloadingpopup", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ShowAdventureLoadingPopup), "Show the popup for loading into the currently-set Adventure mission.", null, null);
			cheatMgr.RegisterCheatHandler("hidegametransitionpopup", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_HideGameTransitionPopup), "Hide any currently shown game transition popup.", null, null);
			cheatMgr.RegisterCheatHandler("setallpuzzlesinprogress", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetAllPuzzlesInProgress), "Set the sub-puzzle progress for each puzzle to be on the final puzzle.", null, null);
			cheatMgr.RegisterCheatHandler("unlockhagatha", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_UnlockHagatha), "Set up the hagatha unlock flow. After running the cheat, complete a monster hunt to unlock.", null, null);
			cheatMgr.RegisterCheatHandler("setadventurecomingsoon", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetAdventureComingSoon), "Set the Coming Soon state of an adventure.", null, null);
			cheatMgr.RegisterCheatHandler("resetsessionvo", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetSession_VO), "Reset the fact that you've seen once per session related VO, to be able to hear it again.", null, null);
			cheatMgr.RegisterCheatHandler("setvochance", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetVOChance_VO), "Set an override on the chance to play a VO line in the adventure. This will only override the chance on VO that won't always play.", "<chance>", "0.1");
			cheatMgr.RegisterCategory("adventure:dungeon run");
			cheatMgr.RegisterCheatHandler("setdrprogress", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetDungeonRunProgress), "Set how many bosses you've defeated during an active run in the provided Adventure.", "[adventure abbreviation] [num bosses] [next boss id (optional)]", "uld 7 46589");
			cheatMgr.RegisterCheatHandler("setdrvictory", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetDungeonRunVictory), "Set victory in the provided Adventure.", "<adventure abbreviation>", "uld");
			cheatMgr.RegisterCheatHandler("setdrdefeat", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetDungeonRunDefeat), "Set defeat and how many bosses you've defeated in the provided Adventure.", "[adventure abbreviation] [num bosses]", "uld 7");
			cheatMgr.RegisterCheatHandler("resetdradventure", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetDungeonRunAdventure), "Reset the current run for the provided Adventure.", "[adventure abbreviation]", "uld");
			cheatMgr.RegisterCheatAlias("resetdradventure", new string[]
			{
				"resetdrrun"
			});
			cheatMgr.RegisterCheatHandler("resetdrvo", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetDungeonRun_VO), "Reset the fact that you've seen all VO related to the provided Adventure, to be able to hear it again.", "[adventure abbreviation] [optional:value to set subkeys to]", "uld 1");
			cheatMgr.RegisterCheatHandler("unlockloadout", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_UnlockLoadout), "Unlock all loadout options for the provided Adventure.", "[adventure abbreviation]", "uld");
			cheatMgr.RegisterCheatHandler("lockloadout", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_LockLoadout), "Lock all loadout options for the provided Adventure.", "[adventure abbreviation]", "uld");
			cheatMgr.RegisterCategory("adventure:k&c");
			cheatMgr.RegisterCheatHandler("setkcprogress", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetKCProgress), "Set how many bosses you've defeated during an active run in Kobolds & Catacombs.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setkcvictory", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetKCVictory), "Set victory in Kobolds & Catacombs.", null, null);
			cheatMgr.RegisterCheatHandler("setkcdefeat", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetKCDefeat), "Set defeat and how many bosses you've defeated in Kobolds & Catacombs.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resetkcvo", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetKC_VO), "Reset the fact that you've seen all K&C related VO, to be able to hear it again.", null, null);
			cheatMgr.RegisterCategory("adventure:witchwood");
			cheatMgr.RegisterCheatHandler("setgilprogress", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetGILProgress), "Set how many bosses you've defeated during an active run in Witchwood.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setgilvictory", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetGILVictory), "Set victory in Witchwood.", null, null);
			cheatMgr.RegisterCheatHandler("setgildefeat", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetGILDefeat), "Set defeat and how many bosses you've defeated in Witchwood.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("setgilbonus", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetGILBonus), "Set the Witchwood bonus challenge to be active.", null, null);
			cheatMgr.RegisterCheatHandler("resetGilAdventure", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetGILAdventure), "Reset the current Witchwood Adventure run.", null, null);
			cheatMgr.RegisterCheatHandler("resetgilvo", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetGIL_VO), "Reset the fact that you've seen all Witchwood related VO, to be able to hear it again.", null, null);
			cheatMgr.RegisterCategory("adventure:rastakhan");
			cheatMgr.RegisterCheatHandler("settrlprogress", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetTRLProgress), "Set how many bosses you've defeated during an active run in Rastakhan.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("settrlvictory", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetTRLVictory), "Set victory in Rastakhan.", null, null);
			cheatMgr.RegisterCheatHandler("settrldefeat", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetTRLDefeat), "Set defeat and how many bosses you've defeated in Rastakhan.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resettrlvo", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetTRL_VO), "Reset the fact that you've seen all Rastakhan related VO, to be able to hear it again.", null, null);
			cheatMgr.RegisterCategory("adventure:dalaran");
			cheatMgr.RegisterCheatHandler("setdalprogress", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetDALProgress), "Set how many bosses you've defeated during an active run in Dalaran.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setdalvictory", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetDALVictory), "Set victory in Dalaran.", null, null);
			cheatMgr.RegisterCheatHandler("setdaldefeat", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetDALDefeat), "Set defeat and how many bosses you've defeated in Dalaran.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resetDalaranAdventure", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetDalaranAdventure), "Reset the current Dalaran Adventure run, so you can start at the location selection again.", null, null);
			cheatMgr.RegisterCheatHandler("setdalheroicprogress", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetDALHeroicProgress), "Set how many bosses you've defeated during an active run in Dalaran Heroic.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setdalheroicvictory", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetDALHeroicVictory), "Set victory in Dalaran Heroic.", null, null);
			cheatMgr.RegisterCheatHandler("setdalheroicdefeat", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetDALHeroicDefeat), "Set defeat and how many bosses you've defeated in Dalaran Heroic.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resetDalaranHeroicAdventure", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetDalaranHeroicAdventure), "Reset the current Dalaran Heroic Adventure run, so you can start at the location selection again.", null, null);
			cheatMgr.RegisterCheatHandler("resetdalvo", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetDAL_VO), "Reset the fact that you've seen all Dalaran related VO, to be able to hear it again.", null, null);
			cheatMgr.RegisterCategory("adventure:uldum");
			cheatMgr.RegisterCheatHandler("setuldprogress", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetULDProgress), "Set how many bosses you've defeated during an active run in Uldum.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setuldvictory", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetULDVictory), "Set victory in Uldum.", null, null);
			cheatMgr.RegisterCheatHandler("setulddefeat", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetULDDefeat), "Set defeat and how many bosses you've defeated in Uldum.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resetuldrun", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetUldumAdventure), "Reset the current Uldum Adventure run, so you can start at the location selection again.", null, null);
			cheatMgr.RegisterCheatAlias("resetuldrun", new string[]
			{
				"resetUldumAdventure"
			});
			cheatMgr.RegisterCheatHandler("setuldheroicprogress", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetULDHeroicProgress), "Set how many bosses you've defeated during an active run in Uldum Heroic.", "[num bosses] [next boss id (optional)]", "7 46589");
			cheatMgr.RegisterCheatHandler("setuldheroicvictory", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetULDHeroicVictory), "Set victory in Uldum Heroic.", null, null);
			cheatMgr.RegisterCheatHandler("setuldheroicdefeat", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_SetULDHeroicDefeat), "Set defeat and how many bosses you've defeated in Uldum Heroic.", "<num bosses>", "7");
			cheatMgr.RegisterCheatHandler("resetuldheroicrun", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetUldumHeroicAdventure), "Reset the current Uldum Heroic Adventure run, so you can start at the location selection again.", null, null);
			cheatMgr.RegisterCheatAlias("resetuldheroicrun", new string[]
			{
				"resetUldumHeroicAdventure"
			});
			cheatMgr.RegisterCheatHandler("resetuldvo", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ResetULD_VO), "Reset the fact that you've seen all Uldum related VO, to be able to hear it again.", null, null);
			cheatMgr.DefaultCategory();
			cheatMgr.RegisterCheatHandler("brode", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_brode), "Brode's personal cheat", "", "");
			cheatMgr.RegisterCheatHandler("freeyourmind", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_freeyourmind), "And the rest will follow", null, null);
		}
		cheatMgr.RegisterCategory("config");
		cheatMgr.RegisterCheatHandler("has", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_HasOption), "Query whether a Game Option exists.", null, null);
		cheatMgr.RegisterCheatHandler("get", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_GetOption), "Get the value of a Game Option.", null, null);
		cheatMgr.RegisterCheatHandler("set", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_SetOption), "Set the value of a Game Option.", null, null);
		cheatMgr.RegisterCheatHandler("getvar", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_GetVar), "Get the value of a client.config var.", null, null);
		cheatMgr.RegisterCheatHandler("setvar", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_SetVar), "Set the value of a client.config var.", null, null);
		cheatMgr.RegisterCheatHandler("delete", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_DeleteOption), "Delete a Game Option; the absence of option may trigger default behavior", null, null);
		cheatMgr.RegisterCheatAlias("delete", new string[]
		{
			"del"
		});
		cheatMgr.RegisterCategory("ui");
		cheatMgr.RegisterCheatHandler("nav", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_navigation), "Debug Navigation.GoBack", null, null);
		cheatMgr.RegisterCheatAlias("nav", new string[]
		{
			"navigate"
		});
		cheatMgr.RegisterCheatHandler("warning", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_warning), "Show a warning message", "<message>", "Test You're a cheater and you've been warned!");
		cheatMgr.RegisterCheatHandler("fatal", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_fatal), "Brings up the Fatal Error screen", "<error to display>", "Hearthstone cheated and failed!");
		cheatMgr.RegisterCheatHandler("alert", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_alert), "Show a popup alert", "header=<string> text=<string> icon=<bool> response=<ok|confirm|cancel|confirm_cancel> oktext=<string> confirmtext=<string>", "header=header text=body text icon=true response=confirm");
		cheatMgr.RegisterCheatAlias("alert", new string[]
		{
			"popup",
			"dialog"
		});
		cheatMgr.RegisterCheatHandler("rankedintropopup", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_rankedIntroPopup), "Show the Ranked Intro Popup", null, null);
		cheatMgr.RegisterCheatHandler("setrotationrotatedboosterspopup", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_setRotationRotatedBoostersPopup), "Show the Set Rotation Tutorial Popup", null, null);
		cheatMgr.RegisterCategory("game modes");
		cheatMgr.RegisterCheatHandler("autodraft", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_autodraft), "Sets Arena autodraft on/off.", "<on | off>", "on");
		cheatMgr.RegisterCategory("general");
		cheatMgr.RegisterCheatHandler("log", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_log), null, null, null);
		cheatMgr.RegisterCheatHandler("ip", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_IPAddress), null, null, null);
		cheatMgr.RegisterCategory("program");
		cheatMgr.RegisterCheatHandler("exit", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_exit), "Exit the application", "", "");
		cheatMgr.RegisterCheatAlias("exit", new string[]
		{
			"quit"
		});
		cheatMgr.RegisterCheatHandler("pause", delegate(string a, string[] b, string c)
		{
			HearthstoneApplication.Get().OnApplicationPause(true);
			return true;
		}, null, null, null);
		cheatMgr.RegisterCheatHandler("unpause", delegate(string a, string[] b, string c)
		{
			HearthstoneApplication.Get().OnApplicationPause(false);
			return true;
		}, null, null, null);
		cheatMgr.RegisterCategory("account data");
		cheatMgr.RegisterCheatHandler("clearofflinelocalcache", delegate(string a, string[] b, string c)
		{
			OfflineDataCache.ClearLocalCacheFile();
			return true;
		}, null, null, null);
		cheatMgr.RegisterCheatHandler("herocount", new CheatMgr.ProcessCheatAutofillCallback(this.OnProcessCheat_HeroCount), "Set the hero picker count and reload UI", "number of heroes to display 1-12", "12");
		cheatMgr.DefaultCategory();
		cheatMgr.RegisterCheatHandler("attribution", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_Attribution), null, null, null);
		cheatMgr.RegisterCheatHandler("crm", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_CRM), null, null, null);
		cheatMgr.RegisterCategory("progression");
		cheatMgr.RegisterCheatHandler("checkfornewquests", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_checkfornewquests), "Trigger a check for next quests after n secs (default 0)", "[delaySecs]", "1");
		cheatMgr.RegisterCheatHandler("showachievementreward", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_showachievementreward), "show a fake achievement reward scroll", null, null);
		cheatMgr.RegisterCheatHandler("showquestreward", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_showquestreward), "show a fake quest reward scroll", null, null);
		cheatMgr.RegisterCheatHandler("showtrackreward", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_showtrackreward), "show a fake track reward scroll", null, null);
		cheatMgr.RegisterCheatHandler("showquestprogresstoast", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_showquestprogresstoast), "Pop up a quest progress toast widget", "<quest id>", "2");
		cheatMgr.RegisterCheatHandler("showquestnotification", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_showquestnotification), "Shows the quest notification popup widget", "<daily|weekly>", "daily");
		cheatMgr.RegisterCheatHandler("showachievementtoast", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_showachievementtoast), "Pop up a achievement complete toast widget", "<achieve id>", "2");
		cheatMgr.RegisterCheatHandler("showprogtileids", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_showprogtileids), "Show the quest id or achievement id on quest and achievement tiles", null, null);
		cheatMgr.RegisterCheatHandler("simendofgamexp", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_simendofgamexp), "Simulate different end of game situations and show end of game xp screen.", "<scenario id>", "1");
		cheatMgr.RegisterCheatHandler("showunclaimedtrackrewards", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_showunclaimedtrackrewards), "Show the reward track's unclaimed rewards popup.", null, null);
		cheatMgr.RegisterCheatHandler("shownotavernpasswarning", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_shownotavernpasswarning), "Shows the warning popup when no tavern pass is available", null, null);
		cheatMgr.RegisterCheatHandler("setlastrewardtrackseasonseen", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_setlastrewardtrackseasonseen), "Sets the GSD value of Rewards Track: Season Last Seen", null, null);
		cheatMgr.RegisterCheatHandler("apprating", new CheatMgr.ProcessCheatCallback(this.OnProcessCheat_ShowAppRatingPrompt), "Shows the app review popup (Android and iOS only).", null, null);
		yield break;
	}

	// Token: 0x06007624 RID: 30244 RVA: 0x0025E9E7 File Offset: 0x0025CBE7
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(CheatMgr),
			typeof(Network)
		};
	}

	// Token: 0x06007625 RID: 30245 RVA: 0x0025EA09 File Offset: 0x0025CC09
	public void Shutdown()
	{
		Cheats.s_instance = null;
	}

	// Token: 0x06007626 RID: 30246 RVA: 0x0025EA14 File Offset: 0x0025CC14
	public void OnCollectionManagerReady()
	{
		ConfigFile configFile = new ConfigFile();
		if (!configFile.FullLoad(Vars.GetClientConfigPath()))
		{
			return;
		}
		if (!configFile.Get("Cheats.InstantGameplay", false))
		{
			return;
		}
		configFile.Set("Cheats.InstantGameplay", false);
		configFile.Save(null);
		this.m_quickLaunchState = new Cheats.QuickLaunchState();
		this.m_quickLaunchState.m_skipMulligan = true;
		this.LaunchQuickGame(260, GameType.GT_VS_AI, FormatType.FT_WILD, null, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x06007627 RID: 30247 RVA: 0x0025EA80 File Offset: 0x0025CC80
	public void OnMulliganEnded()
	{
		ConfigFile configFile = new ConfigFile();
		if (!configFile.FullLoad(Vars.GetClientConfigPath()))
		{
			return;
		}
		string text = configFile.Get("Cheats.InstantCheatCommands", null);
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		configFile.Set("Cheats.InstantCheatCommands", null);
		configFile.Save(null);
		foreach (string command in text.Split(new char[]
		{
			','
		}))
		{
			Network.Get().SendDebugConsoleCommand(command);
		}
	}

	// Token: 0x06007628 RID: 30248 RVA: 0x0025EAFE File Offset: 0x0025CCFE
	public int GetBoardId()
	{
		return this.m_boardId;
	}

	// Token: 0x06007629 RID: 30249 RVA: 0x0025EB06 File Offset: 0x0025CD06
	public void ClearBoardId()
	{
		this.m_boardId = 0;
	}

	// Token: 0x0600762A RID: 30250 RVA: 0x0025EB0F File Offset: 0x0025CD0F
	public bool HasCheatTreasureIds()
	{
		return this.m_pvpdrTreasureIds.Count > 0;
	}

	// Token: 0x0600762B RID: 30251 RVA: 0x0025EB1F File Offset: 0x0025CD1F
	public void ClearCheatTreasures()
	{
		this.m_pvpdrTreasureIds.Clear();
	}

	// Token: 0x0600762C RID: 30252 RVA: 0x0025EB2C File Offset: 0x0025CD2C
	public bool HasCheatLootIds()
	{
		return this.m_pvpdrLootIds.Count > 0;
	}

	// Token: 0x0600762D RID: 30253 RVA: 0x0025EB3C File Offset: 0x0025CD3C
	public void ClearCheatLoot()
	{
		this.m_pvpdrLootIds.Clear();
	}

	// Token: 0x0600762E RID: 30254 RVA: 0x0025EB49 File Offset: 0x0025CD49
	public bool IsSpeechBubbleEnabled()
	{
		return this.m_speechBubblesEnabled;
	}

	// Token: 0x0600762F RID: 30255 RVA: 0x0025EB51 File Offset: 0x0025CD51
	public bool IsSoundCategoryEnabled(Global.SoundCategory sc)
	{
		return !this.m_audioChannelEnabled.ContainsKey(sc) || this.m_audioChannelEnabled[sc];
	}

	// Token: 0x06007630 RID: 30256 RVA: 0x0025EB6F File Offset: 0x0025CD6F
	public string GetPlayerTags()
	{
		return this.m_playerTags;
	}

	// Token: 0x06007631 RID: 30257 RVA: 0x0025EB77 File Offset: 0x0025CD77
	public void ClearAllPlayerTags()
	{
		this.m_playerTags = "";
	}

	// Token: 0x06007632 RID: 30258 RVA: 0x0025EB84 File Offset: 0x0025CD84
	public bool IsNewCardInPackOpeningEnabed()
	{
		return this.m_isNewCardInPackOpeningEnabled;
	}

	// Token: 0x06007633 RID: 30259 RVA: 0x0025EB8C File Offset: 0x0025CD8C
	public bool IsLaunchingQuickGame()
	{
		return this.m_quickLaunchState.m_launching;
	}

	// Token: 0x06007634 RID: 30260 RVA: 0x0025EB99 File Offset: 0x0025CD99
	public bool ShouldSkipMulligan()
	{
		return Options.Get().GetBool(global::Option.SKIP_ALL_MULLIGANS) || this.m_quickLaunchState.m_skipMulligan;
	}

	// Token: 0x06007635 RID: 30261 RVA: 0x0025EBB6 File Offset: 0x0025CDB6
	public bool ShouldSkipSendingGetGameState()
	{
		return this.m_skipSendingGetGameState;
	}

	// Token: 0x06007636 RID: 30262 RVA: 0x0025EBBE File Offset: 0x0025CDBE
	public bool QuickGameFlipHeroes()
	{
		return this.m_quickLaunchState.m_flipHeroes;
	}

	// Token: 0x06007637 RID: 30263 RVA: 0x0025EBCB File Offset: 0x0025CDCB
	public bool QuickGameMirrorHeroes()
	{
		return this.m_quickLaunchState.m_mirrorHeroes;
	}

	// Token: 0x06007638 RID: 30264 RVA: 0x0025EBD8 File Offset: 0x0025CDD8
	public string QuickGameOpponentHeroCardId()
	{
		return this.m_quickLaunchState.m_opponentHeroCardId;
	}

	// Token: 0x06007639 RID: 30265 RVA: 0x0025EBE5 File Offset: 0x0025CDE5
	public bool HandleKeyboardInput()
	{
		return HearthstoneApplication.IsInternal() && this.HandleQuickPlayInput();
	}

	// Token: 0x0600763A RID: 30266 RVA: 0x0025EBFC File Offset: 0x0025CDFC
	public void SaveDuelsCheatTreasures(out List<int> addedTreasures)
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		addedTreasures = new List<int>();
		if (this.m_pvpdrTreasureIds.Count<int>() > 0 && selectedAdventureDataRecord != null)
		{
			List<long> list = null;
			GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_OPTION, out list);
			if (list == null)
			{
				return;
			}
			int num = Math.Min(this.m_pvpdrTreasureIds.Count, list.Count);
			for (int i = 0; i < num; i++)
			{
				int num2 = this.m_pvpdrTreasureIds.Dequeue();
				if (num2 > 0)
				{
					list[i] = (long)num2;
					addedTreasures.Add(num2);
				}
			}
			this.InvokeSetGameSaveDataCheat((GameSaveKeyId)selectedAdventureDataRecord.GameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_OPTION, list.ToArray());
		}
	}

	// Token: 0x0600763B RID: 30267 RVA: 0x0025ECA4 File Offset: 0x0025CEA4
	private bool AddCheatLootToBucket(AdventureDataDbfRecord dataRecord, GameSaveKeySubkeyId subkey, List<int> addedLoot)
	{
		List<long> list = null;
		GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)dataRecord.GameSaveDataServerKey, subkey, out list);
		if (list == null || list.Count < 4)
		{
			return false;
		}
		int num = 0;
		while (num < 3 && this.m_pvpdrLootIds.Count != 0)
		{
			int num2 = this.m_pvpdrLootIds.Dequeue();
			if (num2 > 0)
			{
				list[num + 1] = (long)num2;
				addedLoot.Add(num2);
			}
			num++;
		}
		this.InvokeSetGameSaveDataCheat((GameSaveKeyId)dataRecord.GameSaveDataServerKey, subkey, list.ToArray());
		return true;
	}

	// Token: 0x0600763C RID: 30268 RVA: 0x0025ED24 File Offset: 0x0025CF24
	public void SaveDuelsCheatLoot(out List<int> addedLootA, out List<int> addedLootB, out List<int> addedLootC)
	{
		AdventureDataDbfRecord selectedAdventureDataRecord = AdventureConfig.Get().GetSelectedAdventureDataRecord();
		addedLootA = new List<int>();
		addedLootB = new List<int>();
		addedLootC = new List<int>();
		if (this.m_pvpdrLootIds.Count > 0 && selectedAdventureDataRecord != null && this.AddCheatLootToBucket(selectedAdventureDataRecord, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_A, addedLootA) && this.m_pvpdrLootIds.Count > 0 && this.AddCheatLootToBucket(selectedAdventureDataRecord, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_B, addedLootB) && this.m_pvpdrLootIds.Count > 0)
		{
			this.AddCheatLootToBucket(selectedAdventureDataRecord, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_C, addedLootC);
		}
	}

	// Token: 0x0600763D RID: 30269 RVA: 0x0025EDA4 File Offset: 0x0025CFA4
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

	// Token: 0x0600763E RID: 30270 RVA: 0x0025EE0C File Offset: 0x0025D00C
	private AlertPopup.PopupInfo GenerateAlertInfo(string rawArgs)
	{
		global::Map<string, string> map = this.ParseAlertArgs(rawArgs);
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_headerText = "Header";
		popupInfo.m_text = "Message";
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		popupInfo.m_okText = "OK";
		popupInfo.m_confirmText = "Confirm";
		popupInfo.m_cancelText = "Cancel";
		foreach (KeyValuePair<string, string> keyValuePair in map)
		{
			string key = keyValuePair.Key;
			string text = keyValuePair.Value;
			if (key.Equals("header"))
			{
				popupInfo.m_headerText = text;
			}
			else if (key.Equals("text"))
			{
				popupInfo.m_text = text;
			}
			else if (key.Equals("response"))
			{
				text = text.ToLowerInvariant();
				if (text.Equals("ok"))
				{
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
				}
				else if (text.Equals("confirm"))
				{
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM;
				}
				else if (text.Equals("cancel"))
				{
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CANCEL;
				}
				else if (text.Equals("confirm_cancel") || text.Equals("cancel_confirm"))
				{
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL;
				}
			}
			else if (key.Equals("icon"))
			{
				popupInfo.m_showAlertIcon = GeneralUtils.ForceBool(text);
			}
			else if (key.Equals("oktext"))
			{
				popupInfo.m_okText = text;
			}
			else if (key.Equals("confirmtext"))
			{
				popupInfo.m_confirmText = text;
			}
			else if (key.Equals("canceltext"))
			{
				popupInfo.m_cancelText = text;
			}
			else if (key.Equals("offset"))
			{
				string[] array = text.Split(Array.Empty<char>());
				Vector3 offset = default(Vector3);
				if (array.Length % 2 == 0)
				{
					for (int i = 0; i < array.Length; i += 2)
					{
						string text2 = array[i].ToLowerInvariant();
						string str = array[i + 1];
						if (text2.Equals("x"))
						{
							offset.x = GeneralUtils.ForceFloat(str);
						}
						else if (text2.Equals("y"))
						{
							offset.y = GeneralUtils.ForceFloat(str);
						}
						else if (text2.Equals("z"))
						{
							offset.z = GeneralUtils.ForceFloat(str);
						}
					}
				}
				popupInfo.m_offset = offset;
			}
			else if (key.Equals("padding"))
			{
				popupInfo.m_padding = GeneralUtils.ForceFloat(text);
			}
			else if (key.Equals("align"))
			{
				string[] array2 = text.Split(new char[]
				{
					'|'
				});
				for (int j = 0; j < array2.Length; j++)
				{
					string a = array2[j].ToLower();
					if (!(a == "left"))
					{
						if (!(a == "center"))
						{
							if (!(a == "right"))
							{
								if (!(a == "top"))
								{
									if (!(a == "middle"))
									{
										if (a == "bottom")
										{
											popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Lower;
										}
									}
									else
									{
										popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Middle;
									}
								}
								else
								{
									popupInfo.m_alertTextAlignmentAnchor = UberText.AnchorOptions.Upper;
								}
							}
							else
							{
								popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Right;
							}
						}
						else
						{
							popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Center;
						}
					}
					else
					{
						popupInfo.m_alertTextAlignment = UberText.AlignmentOptions.Left;
					}
				}
			}
		}
		return popupInfo;
	}

	// Token: 0x0600763F RID: 30271 RVA: 0x0025F1AC File Offset: 0x0025D3AC
	private global::Map<string, string> ParseAlertArgs(string rawArgs)
	{
		global::Map<string, string> map = new global::Map<string, string>();
		int num = -1;
		string text = null;
		int num3;
		for (int i = 0; i < rawArgs.Length; i++)
		{
			if (rawArgs[i] == '=')
			{
				int num2 = -1;
				for (int j = i - 1; j >= 0; j--)
				{
					char c = rawArgs[j];
					char c2 = rawArgs[j + 1];
					if (!char.IsWhiteSpace(c))
					{
						num2 = j;
					}
					if (char.IsWhiteSpace(c) && !char.IsWhiteSpace(c2))
					{
						break;
					}
				}
				if (num2 >= 0)
				{
					num3 = num2 - 2;
					if (text != null)
					{
						map[text] = rawArgs.Substring(num, num3 - num + 1);
					}
					num = i + 1;
					text = rawArgs.Substring(num2, i - num2).Trim().ToLowerInvariant().Replace("\\n", "\n");
				}
			}
		}
		num3 = rawArgs.Length - 1;
		if (text != null)
		{
			map[text] = rawArgs.Substring(num, num3 - num + 1).Replace("\\n", "\n");
		}
		return map;
	}

	// Token: 0x06007640 RID: 30272 RVA: 0x0025F2B0 File Offset: 0x0025D4B0
	private bool OnAlertProcessed(DialogBase dialog, object userData)
	{
		this.m_alert = (AlertPopup)dialog;
		return true;
	}

	// Token: 0x06007641 RID: 30273 RVA: 0x0025F2BF File Offset: 0x0025D4BF
	private void HideAlert()
	{
		if (this.m_alert != null)
		{
			this.m_alert.Hide();
			this.m_alert = null;
		}
	}

	// Token: 0x06007642 RID: 30274 RVA: 0x0025F2E4 File Offset: 0x0025D4E4
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
			this.PrintQuickPlayLegend();
			return false;
		}
		if (this.GetQuickLaunchAvailability() != Cheats.QuickLaunchAvailability.OK)
		{
			return false;
		}
		ScenarioDbId scenarioDbId = ScenarioDbId.INVALID;
		string opponentHeroCardId = null;
		foreach (KeyValuePair<KeyCode, ScenarioDbId> keyValuePair in Cheats.s_quickPlayKeyMap)
		{
			KeyCode key = keyValuePair.Key;
			ScenarioDbId value = keyValuePair.Value;
			if (InputCollection.GetKeyDown(key))
			{
				scenarioDbId = value;
				opponentHeroCardId = Cheats.s_opponentHeroKeyMap[key];
				break;
			}
		}
		if (scenarioDbId == ScenarioDbId.INVALID)
		{
			return false;
		}
		this.m_quickLaunchState.m_mirrorHeroes = false;
		this.m_quickLaunchState.m_flipHeroes = false;
		this.m_quickLaunchState.m_skipMulligan = true;
		this.m_quickLaunchState.m_opponentHeroCardId = opponentHeroCardId;
		if ((InputCollection.GetKey(KeyCode.RightAlt) || InputCollection.GetKey(KeyCode.LeftAlt)) && (InputCollection.GetKey(KeyCode.RightControl) || InputCollection.GetKey(KeyCode.LeftControl)))
		{
			this.m_quickLaunchState.m_mirrorHeroes = true;
			this.m_quickLaunchState.m_skipMulligan = false;
			this.m_quickLaunchState.m_flipHeroes = false;
		}
		else if (InputCollection.GetKey(KeyCode.RightControl) || InputCollection.GetKey(KeyCode.LeftControl))
		{
			this.m_quickLaunchState.m_flipHeroes = false;
			this.m_quickLaunchState.m_skipMulligan = false;
			this.m_quickLaunchState.m_mirrorHeroes = false;
		}
		else if (InputCollection.GetKey(KeyCode.RightAlt) || InputCollection.GetKey(KeyCode.LeftAlt))
		{
			this.m_quickLaunchState.m_flipHeroes = true;
			this.m_quickLaunchState.m_skipMulligan = false;
			this.m_quickLaunchState.m_mirrorHeroes = false;
		}
		this.LaunchQuickGame((int)scenarioDbId, GameType.GT_VS_AI, FormatType.FT_WILD, null, null, GameType.GT_UNKNOWN);
		return true;
	}

	// Token: 0x06007643 RID: 30275 RVA: 0x0025F4B4 File Offset: 0x0025D6B4
	private void PrintQuickPlayLegend()
	{
		string message = string.Format("F1: {0}\nF2: {1}\nF3: {2}\nF4: {3}\nF5: {4}\nF6: {5}\nF7: {6}\nF8: {7}\nF9: {8}\nF10: {9}\n(CTRL and ALT will Show mulligan)\nSHIFT + CTRL = Hero on players side\nSHIFT + ALT = Hero on opponent side\nSHIFT + ALT + CTRL = Hero on both sides", new object[]
		{
			this.GetQuickPlayMissionName(KeyCode.F1),
			this.GetQuickPlayMissionName(KeyCode.F2),
			this.GetQuickPlayMissionName(KeyCode.F3),
			this.GetQuickPlayMissionName(KeyCode.F4),
			this.GetQuickPlayMissionName(KeyCode.F5),
			this.GetQuickPlayMissionName(KeyCode.F6),
			this.GetQuickPlayMissionName(KeyCode.F7),
			this.GetQuickPlayMissionName(KeyCode.F8),
			this.GetQuickPlayMissionName(KeyCode.F9),
			this.GetQuickPlayMissionName(KeyCode.F10)
		});
		if (UIStatus.Get() != null)
		{
			UIStatus.Get().AddInfo(message);
		}
		Debug.Log(string.Format("F1: {0}  F2: {1}  F3: {2}  F4: {3}  F5: {4}  F6: {5}  F7: {6}  F8: {7}  F9: {8}\nF10: {9}\n(CTRL and ALT will Show mulligan) -- SHIFT + CTRL = Hero on players side -- SHIFT + ALT = Hero on opponent side -- SHIFT + ALT + CTRL = Hero on both sides", new object[]
		{
			this.GetQuickPlayMissionShortName(KeyCode.F1),
			this.GetQuickPlayMissionShortName(KeyCode.F2),
			this.GetQuickPlayMissionShortName(KeyCode.F3),
			this.GetQuickPlayMissionShortName(KeyCode.F4),
			this.GetQuickPlayMissionShortName(KeyCode.F5),
			this.GetQuickPlayMissionShortName(KeyCode.F6),
			this.GetQuickPlayMissionShortName(KeyCode.F7),
			this.GetQuickPlayMissionShortName(KeyCode.F8),
			this.GetQuickPlayMissionShortName(KeyCode.F9),
			this.GetQuickPlayMissionShortName(KeyCode.F10)
		}));
	}

	// Token: 0x06007644 RID: 30276 RVA: 0x0025F61B File Offset: 0x0025D81B
	private string GetQuickPlayMissionName(KeyCode keyCode)
	{
		return this.GetQuickPlayMissionName((int)Cheats.s_quickPlayKeyMap[keyCode]);
	}

	// Token: 0x06007645 RID: 30277 RVA: 0x0025F62E File Offset: 0x0025D82E
	private string GetQuickPlayMissionShortName(KeyCode keyCode)
	{
		return this.GetQuickPlayMissionShortName((int)Cheats.s_quickPlayKeyMap[keyCode]);
	}

	// Token: 0x06007646 RID: 30278 RVA: 0x0025F641 File Offset: 0x0025D841
	private string GetQuickPlayMissionName(int missionId)
	{
		return this.GetQuickPlayMissionNameImpl(missionId, "NAME");
	}

	// Token: 0x06007647 RID: 30279 RVA: 0x0025F64F File Offset: 0x0025D84F
	private string GetQuickPlayMissionShortName(int missionId)
	{
		return this.GetQuickPlayMissionNameImpl(missionId, "SHORT_NAME");
	}

	// Token: 0x06007648 RID: 30280 RVA: 0x0025F660 File Offset: 0x0025D860
	private string GetQuickPlayMissionNameImpl(int missionId, string columnName)
	{
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(missionId);
		if (record != null)
		{
			DbfLocValue dbfLocValue = (DbfLocValue)record.GetVar(columnName);
			if (dbfLocValue != null)
			{
				return dbfLocValue.GetString(true);
			}
		}
		string result = missionId.ToString();
		try
		{
			ScenarioDbId scenarioDbId = (ScenarioDbId)missionId;
			result = scenarioDbId.ToString();
		}
		catch (Exception)
		{
		}
		return result;
	}

	// Token: 0x06007649 RID: 30281 RVA: 0x0025F6C4 File Offset: 0x0025D8C4
	private Cheats.QuickLaunchAvailability GetQuickLaunchAvailability()
	{
		if (this.m_quickLaunchState.m_launching)
		{
			return Cheats.QuickLaunchAvailability.ACTIVE_GAME;
		}
		if (SceneMgr.Get().IsInGame())
		{
			return Cheats.QuickLaunchAvailability.ACTIVE_GAME;
		}
		if (GameMgr.Get().IsFindingGame())
		{
			return Cheats.QuickLaunchAvailability.FINDING_GAME;
		}
		if (SceneMgr.Get().GetNextMode() != SceneMgr.Mode.INVALID)
		{
			return Cheats.QuickLaunchAvailability.SCENE_TRANSITION;
		}
		if (!SceneMgr.Get().IsSceneLoaded())
		{
			return Cheats.QuickLaunchAvailability.SCENE_TRANSITION;
		}
		if (LoadingScreen.Get().IsTransitioning())
		{
			return Cheats.QuickLaunchAvailability.ACTIVE_GAME;
		}
		if (CollectionManager.Get() == null || !CollectionManager.Get().IsFullyLoaded())
		{
			return Cheats.QuickLaunchAvailability.COLLECTION_NOT_READY;
		}
		return Cheats.QuickLaunchAvailability.OK;
	}

	// Token: 0x0600764A RID: 30282 RVA: 0x0025F73C File Offset: 0x0025D93C
	private void LaunchQuickGame(int missionId, GameType gameType = GameType.GT_VS_AI, FormatType formatType = FormatType.FT_WILD, CollectionDeck deck = null, string aiDeck = null, GameType progFilterOverride = GameType.GT_UNKNOWN)
	{
		long num;
		string name;
		if (deck == null)
		{
			CollectionManager collectionManager = CollectionManager.Get();
			num = Options.Get().GetLong(global::Option.LAST_CUSTOM_DECK_CHOSEN);
			deck = collectionManager.GetDeck(num);
			if (deck == null)
			{
				TAG_CLASS defaultClass = TAG_CLASS.MAGE;
				List<CollectionDeck> decks = collectionManager.GetDecks(DeckType.NORMAL_DECK);
				deck = (from x in decks
				where x.GetClass() == defaultClass
				select x).FirstOrDefault<CollectionDeck>();
				if (deck == null)
				{
					deck = decks.FirstOrDefault<CollectionDeck>();
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
		ReconnectMgr.Get().SetBypassReconnect(true);
		this.m_quickLaunchState.m_launching = true;
		string quickPlayMissionName = this.GetQuickPlayMissionName(missionId);
		string message = string.Format("Launching {0}\nDeck: {1}", quickPlayMissionName, name);
		UIStatus.Get().AddInfo(message);
		TimeScaleMgr.Get().PushTemporarySpeedIncrease(4f);
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		GameMgr.Get().SetPendingAutoConcede(true);
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		GameMgr.Get().FindGame(gameType, formatType, missionId, 0, num, aiDeck, null, false, null, progFilterOverride);
	}

	// Token: 0x0600764B RID: 30283 RVA: 0x0025F88C File Offset: 0x0025DA8C
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			this.HideAlert();
			this.m_isInGameplayScene = true;
		}
		if (this.m_isInGameplayScene && mode != SceneMgr.Mode.GAMEPLAY)
		{
			SceneMgr.Get().UnregisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
			this.m_quickLaunchState = new Cheats.QuickLaunchState();
			this.m_isInGameplayScene = false;
		}
	}

	// Token: 0x0600764C RID: 30284 RVA: 0x0025F8E0 File Offset: 0x0025DAE0
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if (state - FindGameState.CLIENT_CANCELED <= 1 || state - FindGameState.BNET_QUEUE_CANCELED <= 1 || state == FindGameState.SERVER_GAME_CANCELED)
		{
			GameMgr.Get().UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
			this.m_quickLaunchState = new Cheats.QuickLaunchState();
		}
		return false;
	}

	// Token: 0x0600764D RID: 30285 RVA: 0x0025F928 File Offset: 0x0025DB28
	private JsonList GetCardlistJson(List<global::Card> list)
	{
		JsonList jsonList = new JsonList();
		for (int i = 0; i < list.Count; i++)
		{
			JsonNode cardJson = this.GetCardJson(list[i].GetEntity());
			jsonList.Add(cardJson);
		}
		return jsonList;
	}

	// Token: 0x0600764E RID: 30286 RVA: 0x0025F968 File Offset: 0x0025DB68
	private JsonNode GetCardJson(global::Entity card)
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
			foreach (KeyValuePair<int, int> keyValuePair in card.GetTags().GetMap())
			{
				JsonNode jsonNode2 = new JsonNode();
				string text = Enum.GetName(typeof(GAME_TAG), keyValuePair.Key);
				if (text == null)
				{
					text = "NOTAG_" + keyValuePair.Key.ToString();
				}
				jsonNode2[text] = (long)keyValuePair.Value;
				jsonList.Add(jsonNode2);
			}
			jsonNode["tags"] = jsonList;
		}
		JsonList jsonList2 = new JsonList();
		List<global::Entity> enchantments = card.GetEnchantments();
		for (int i = 0; i < enchantments.Count<global::Entity>(); i++)
		{
			JsonNode cardJson = this.GetCardJson(enchantments[i]);
			jsonList2.Add(cardJson);
		}
		jsonNode["enchantments"] = jsonList2;
		return jsonNode;
	}

	// Token: 0x0600764F RID: 30287 RVA: 0x0025FAC8 File Offset: 0x0025DCC8
	private bool OnProcessCheat_error(string func, string[] args, string rawArgs)
	{
		bool flag = args.Length != 0 && (args[0] == "ex" || "except".Equals(args[0], StringComparison.InvariantCultureIgnoreCase) || "exception".Equals(args[0], StringComparison.InvariantCultureIgnoreCase));
		bool flag2 = args.Length != 0 && (args[0] == "f" || "fatal".Equals(args[0], StringComparison.InvariantCultureIgnoreCase));
		string text = (args.Length <= 1) ? null : string.Join(" ", args.Skip(1).ToArray<string>());
		if (flag)
		{
			if (text == null)
			{
				text = "This is a simulated Exception.";
			}
			throw new Exception(text);
		}
		if (flag2)
		{
			if (text == null)
			{
				text = "This is a simulated Fatal Error.";
			}
			global::Error.AddFatal(FatalErrorReason.CHEAT, text, Array.Empty<object>());
		}
		else
		{
			if (text == null)
			{
				text = "This is a simulated Warning message.";
			}
			global::Error.AddWarning("Warning", text, Array.Empty<object>());
		}
		return true;
	}

	// Token: 0x06007650 RID: 30288 RVA: 0x0025FB9C File Offset: 0x0025DD9C
	private bool ProcessAutofillParam(IEnumerable<string> values, string searchTerm, AutofillData autofillData)
	{
		values = from v in values
		orderby v
		select v;
		string prefix = autofillData.m_lastAutofillParamPrefix ?? (searchTerm ?? string.Empty);
		List<string> list;
		if (string.IsNullOrEmpty(prefix.Trim()))
		{
			list = values.ToList<string>();
		}
		else
		{
			list = (from v in values
			where v.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase)
			select v).ToList<string>();
		}
		int num = 0;
		if (autofillData.m_lastAutofillParamMatch != null)
		{
			num = list.IndexOf(autofillData.m_lastAutofillParamMatch);
			if (num >= 0)
			{
				num += (autofillData.m_isShiftTab ? -1 : 1);
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
			float num2 = 5f + Mathf.Max(0f, (float)(list.Count - 3));
			num2 *= Time.timeScale;
			string arg = string.Join("   ", values.ToArray<string>());
			UIStatus.Get().AddError(string.Format("No match for '{0}'. Available params:\n{1}", searchTerm, arg), num2);
			return false;
		}
		autofillData.m_lastAutofillParamPrefix = prefix;
		autofillData.m_lastAutofillParamMatch = list[num];
		if (list.Count > 0)
		{
			float num3 = 5f + Mathf.Max(0f, (float)(list.Count - 3));
			num3 *= Time.timeScale;
			string str = string.Join("   ", list.ToArray());
			UIStatus.Get().AddInfoNoRichText("Available params:\n" + str, num3);
		}
		return true;
	}

	// Token: 0x06007651 RID: 30289 RVA: 0x0025FD48 File Offset: 0x0025DF48
	private bool OnProcessCheat_HasOption(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData == null)
		{
			global::Option @enum;
			try
			{
				@enum = global::EnumUtils.GetEnum<global::Option>(text, StringComparison.OrdinalIgnoreCase);
			}
			catch (ArgumentException)
			{
				return false;
			}
			string message = string.Format("HasOption: {0} = {1}", global::EnumUtils.GetString<global::Option>(@enum), Options.Get().HasOption(@enum));
			Debug.Log(message);
			UIStatus.Get().AddInfo(message);
			return true;
		}
		if (args.Length != 1)
		{
			return false;
		}
		IEnumerable<string> values = from global::Option v in Enum.GetValues(typeof(global::Option))
		select global::EnumUtils.GetString<global::Option>(v);
		return this.ProcessAutofillParam(values, text, autofillData);
	}

	// Token: 0x06007652 RID: 30290 RVA: 0x0025FE00 File Offset: 0x0025E000
	private bool OnProcessCheat_GetOption(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData == null)
		{
			global::Option @enum;
			try
			{
				@enum = global::EnumUtils.GetEnum<global::Option>(text, StringComparison.OrdinalIgnoreCase);
			}
			catch (ArgumentException)
			{
				return false;
			}
			string message = string.Format("GetOption: {0} = {1}", global::EnumUtils.GetString<global::Option>(@enum), Options.Get().GetOption(@enum));
			Debug.Log(message);
			UIStatus.Get().AddInfo(message);
			return true;
		}
		if (args.Length != 1)
		{
			return false;
		}
		IEnumerable<string> values = from global::Option v in Enum.GetValues(typeof(global::Option))
		select global::EnumUtils.GetString<global::Option>(v);
		return this.ProcessAutofillParam(values, text, autofillData);
	}

	// Token: 0x06007653 RID: 30291 RVA: 0x0025FEB4 File Offset: 0x0025E0B4
	private bool OnProcessCheat_SetOption(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData == null)
		{
			global::Option @enum;
			try
			{
				@enum = global::EnumUtils.GetEnum<global::Option>(text, StringComparison.OrdinalIgnoreCase);
			}
			catch (ArgumentException)
			{
				return false;
			}
			if (args.Length < 2)
			{
				return false;
			}
			string text2 = Options.Get().HasOption(@enum) ? Options.Get().GetOption(@enum).ToString() : "<null>";
			string @string = global::EnumUtils.GetString<global::Option>(@enum);
			string text3 = args[1];
			Type optionType = Options.Get().GetOptionType(@enum);
			if (optionType == typeof(bool))
			{
				bool val;
				if (!GeneralUtils.TryParseBool(text3, out val))
				{
					return false;
				}
				Options.Get().SetBool(@enum, val);
			}
			else if (optionType == typeof(int))
			{
				int val2;
				if (!GeneralUtils.TryParseInt(text3, out val2))
				{
					return false;
				}
				Options.Get().SetInt(@enum, val2);
			}
			else if (optionType == typeof(long))
			{
				long val3;
				if (!GeneralUtils.TryParseLong(text3, out val3))
				{
					return false;
				}
				Options.Get().SetLong(@enum, val3);
			}
			else if (optionType == typeof(float))
			{
				float val4;
				if (!GeneralUtils.TryParseFloat(text3, out val4))
				{
					return false;
				}
				Options.Get().SetFloat(@enum, val4);
			}
			else
			{
				if (!(optionType == typeof(string)))
				{
					string message = string.Format("SetOption: {0} has unsupported underlying type {1}", @string, optionType);
					UIStatus.Get().AddError(message, -1f);
					return true;
				}
				text3 = rawArgs.Remove(0, text.Length + 1);
				Options.Get().SetString(@enum, text3);
			}
			if (@enum == global::Option.CURSOR)
			{
				Cursor.visible = Options.Get().GetBool(global::Option.CURSOR);
			}
			string text4 = Options.Get().HasOption(@enum) ? Options.Get().GetOption(@enum).ToString() : "<null>";
			string message2 = string.Format("SetOption: {0} to {1}.\nPrevious value: {2}\nNew GetOption: {3}", new object[]
			{
				@string,
				text3,
				text2,
				text4
			});
			Debug.Log(message2);
			NetCache.Get().DispatchClientOptionsToServer();
			UIStatus.Get().AddInfo(message2);
			return true;
		}
		if (args.Length != 1)
		{
			return false;
		}
		IEnumerable<string> values = from global::Option v in Enum.GetValues(typeof(global::Option))
		select global::EnumUtils.GetString<global::Option>(v);
		return this.ProcessAutofillParam(values, text, autofillData);
	}

	// Token: 0x06007654 RID: 30292 RVA: 0x0026011C File Offset: 0x0025E31C
	private bool OnProcessCheat_GetVar(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData == null)
		{
			string message = string.Format("Var: {0} = {1}", text, Vars.Key(text).GetStr(null) ?? "(null)");
			Debug.Log(message);
			UIStatus.Get().AddInfo(message);
			return true;
		}
		if (args.Length != 1)
		{
			return false;
		}
		IEnumerable<string> allKeys = Vars.AllKeys;
		return this.ProcessAutofillParam(allKeys, text, autofillData);
	}

	// Token: 0x06007655 RID: 30293 RVA: 0x00260180 File Offset: 0x0025E380
	private bool OnProcessCheat_SetVar(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData == null)
		{
			string text2 = (args.Length < 2) ? null : args[1];
			Vars.Key(text).Set(text2, false);
			string message = string.Format("Var: {0} = {1}", text, text2 ?? "(null)");
			Debug.Log(message);
			UIStatus.Get().AddInfo(message);
			if (text.Equals("Arena.AutoDraft", StringComparison.InvariantCultureIgnoreCase) && DraftDisplay.Get() != null)
			{
				DraftDisplay.Get().StartCoroutine(DraftDisplay.Get().RunAutoDraftCheat());
			}
			return true;
		}
		if (args.Length != 1)
		{
			return false;
		}
		IEnumerable<string> allKeys = Vars.AllKeys;
		return this.ProcessAutofillParam(allKeys, text, autofillData);
	}

	// Token: 0x06007656 RID: 30294 RVA: 0x00260220 File Offset: 0x0025E420
	private bool OnProcessCheat_autodraft(string func, string[] args, string rawArgs)
	{
		string text = args[0];
		bool flag = string.IsNullOrEmpty(text) || GeneralUtils.ForceBool(text);
		Vars.Key("Arena.AutoDraft").Set(flag ? "true" : "false", false);
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

	// Token: 0x06007657 RID: 30295 RVA: 0x002602D4 File Offset: 0x0025E4D4
	private bool OnProcessCheat_HeroCount(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		try
		{
			int num;
			int.TryParse(args[0], out num);
			SceneMgr.Mode mode = SceneMgr.Get().GetMode();
			if (mode != SceneMgr.Mode.COLLECTIONMANAGER)
			{
				if (mode != SceneMgr.Mode.ADVENTURE)
				{
					if (mode != SceneMgr.Mode.TAVERN_BRAWL)
					{
						return false;
					}
					DeckPickerTrayDisplay.Get().CheatLoadHeroButtons(num);
				}
				else
				{
					GuestHeroPickerTrayDisplay.Get().CheatLoadHeroButtons(num);
				}
			}
			else
			{
				HeroPickerDisplay.Get().CheatLoadHeroButtons(num);
			}
		}
		catch (ArgumentException)
		{
			return false;
		}
		return true;
	}

	// Token: 0x06007658 RID: 30296 RVA: 0x0026034C File Offset: 0x0025E54C
	private bool OnProcessCheat_onlygold(string func, string[] args, string rawArgs)
	{
		string text = args[0].ToLowerInvariant();
		if (!(text == "gold") && !(text == "normal") && !(text == "standard"))
		{
			if (!(text == "both"))
			{
				UIStatus.Get().AddError("Unknown cmd: " + (string.IsNullOrEmpty(text) ? "(blank)" : text) + "\nValid cmds: gold, standard, both", -1f);
				return false;
			}
			Options.Get().DeleteOption(global::Option.COLLECTION_PREMIUM_TYPE);
		}
		else
		{
			Options.Get().SetString(global::Option.COLLECTION_PREMIUM_TYPE, text);
		}
		return true;
	}

	// Token: 0x06007659 RID: 30297 RVA: 0x002603E8 File Offset: 0x0025E5E8
	private bool OnProcessCheat_navigation(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
		{
			return true;
		}
		string[] array = new string[]
		{
			"debug",
			"dump",
			"back",
			"pop",
			"stack",
			"history",
			"show"
		};
		string text = args[0].ToLowerInvariant();
		if (autofillData != null)
		{
			return HearthstoneApplication.IsInternal() && this.ProcessAutofillParam(array, text, autofillData);
		}
		uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
		if (num <= 1483009432U)
		{
			if (num != 1248952799U)
			{
				if (num != 1362321360U)
				{
					if (num != 1483009432U)
					{
						goto IL_27C;
					}
					if (!(text == "debug"))
					{
						goto IL_27C;
					}
					Navigation.NAVIGATION_DEBUG = (args.Length < 2 || GeneralUtils.ForceBool(args[1]));
					if (Navigation.NAVIGATION_DEBUG)
					{
						Navigation.DumpStack();
						UIStatus.Get().AddInfo("Navigation debugging turned on - see Console or output log for nav dump.");
						return true;
					}
					UIStatus.Get().AddInfo("Navigation debugging turned off.");
					return true;
				}
				else if (!(text == "pop"))
				{
					goto IL_27C;
				}
			}
			else
			{
				if (!(text == "history"))
				{
					goto IL_27C;
				}
				goto IL_213;
			}
		}
		else if (num <= 1649501183U)
		{
			if (num != 1538531746U)
			{
				if (num != 1649501183U)
				{
					goto IL_27C;
				}
				if (!(text == "stack"))
				{
					goto IL_27C;
				}
				goto IL_213;
			}
			else if (!(text == "back"))
			{
				goto IL_27C;
			}
		}
		else if (num != 2840060476U)
		{
			if (num != 3663001223U)
			{
				goto IL_27C;
			}
			if (!(text == "dump"))
			{
				goto IL_27C;
			}
			Navigation.DumpStack();
			UIStatus.Get().AddInfo("Navigation dumped, see Console or output log.");
			return true;
		}
		else
		{
			if (!(text == "show"))
			{
				goto IL_27C;
			}
			goto IL_213;
		}
		if (!HearthstoneApplication.IsInternal())
		{
			return false;
		}
		if (!Navigation.CanGoBack)
		{
			string str = Navigation.IsEmpty ? " Stack is empty." : string.Empty;
			UIStatus.Get().AddInfo("Cannot go back at this time." + str);
			return true;
		}
		Navigation.GoBack();
		return true;
		IL_213:
		if (!HearthstoneApplication.IsInternal())
		{
			return false;
		}
		string stackDumpString = Navigation.StackDumpString;
		int num2 = stackDumpString.Count((char c) => c == '\n');
		float num3 = (float)(5 + 3 * num2);
		num3 *= Time.timeScale;
		UIStatus.Get().AddInfo(Navigation.IsEmpty ? "Stack is empty." : stackDumpString, num3);
		return true;
		IL_27C:
		string text2 = "Unknown cmd: " + (string.IsNullOrEmpty(text) ? "(blank)" : text);
		if (HearthstoneApplication.IsInternal())
		{
			text2 = text2 + "\nValid cmds: " + string.Join(", ", array);
		}
		UIStatus.Get().AddError(text2, -1f);
		return true;
	}

	// Token: 0x0600765A RID: 30298 RVA: 0x002606C0 File Offset: 0x0025E8C0
	private bool OnProcessCheat_DeleteOption(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = args[0];
		if (autofillData == null)
		{
			global::Option @enum;
			try
			{
				@enum = global::EnumUtils.GetEnum<global::Option>(text, StringComparison.OrdinalIgnoreCase);
			}
			catch (ArgumentException)
			{
				return false;
			}
			string arg = Options.Get().HasOption(@enum) ? Options.Get().GetOption(@enum).ToString() : "<null>";
			Options.Get().DeleteOption(@enum);
			string arg2 = Options.Get().HasOption(@enum) ? Options.Get().GetOption(@enum).ToString() : "<null>";
			string message = string.Format("DeleteOption: {0}\nPrevious Value: {1}\nNew Value: {2}", global::EnumUtils.GetString<global::Option>(@enum), arg, arg2);
			Debug.Log(message);
			UIStatus.Get().AddInfo(message);
			return true;
		}
		if (args.Length != 1)
		{
			return false;
		}
		IEnumerable<string> values = from global::Option v in Enum.GetValues(typeof(global::Option))
		select global::EnumUtils.GetString<global::Option>(v);
		return this.ProcessAutofillParam(values, text, autofillData);
	}

	// Token: 0x0600765B RID: 30299 RVA: 0x002607C4 File Offset: 0x0025E9C4
	private bool OnProcessCheat_collectionfirstxp(string func, string[] args, string rawArgs)
	{
		Options.Get().SetInt(global::Option.COVER_MOUSE_OVERS, 0);
		Options.Get().SetInt(global::Option.PAGE_MOUSE_OVERS, 0);
		return true;
	}

	// Token: 0x0600765C RID: 30300 RVA: 0x002607E4 File Offset: 0x0025E9E4
	private bool OnProcessCheat_board(string func, string[] args, string rawArgs)
	{
		int num = 0;
		this.m_boardId = (int.TryParse(args[0], out num) ? num : 0);
		UIStatus.Get().AddInfo(string.Format("Board for next game set to id {0}.", this.m_boardId));
		return true;
	}

	// Token: 0x0600765D RID: 30301 RVA: 0x00260829 File Offset: 0x0025EA29
	private bool OnProcessCheat_playerTags(string func, string[] args, string rawArgs)
	{
		this.TryParsePlayerTags(args[0], out this.m_playerTags);
		return true;
	}

	// Token: 0x0600765E RID: 30302 RVA: 0x0026083C File Offset: 0x0025EA3C
	private bool OnProcessCheat_arenaChoices(string func, string[] args, string rawArgs)
	{
		string[] array;
		if (this.TryParseArenaChoices(args, out array))
		{
			List<string> list = new List<string>();
			list.Add("arena");
			list.Add("choices");
			foreach (string item in array)
			{
				list.Add(item);
			}
			this.OnProcessCheat_utilservercmd("util", list.ToArray(), rawArgs, null);
		}
		return true;
	}

	// Token: 0x0600765F RID: 30303 RVA: 0x002608A2 File Offset: 0x0025EAA2
	private bool OnProcessCheat_speechBubbles(string func, string[] args, string rawArgs)
	{
		this.m_speechBubblesEnabled = !this.m_speechBubblesEnabled;
		UIStatus.Get().AddInfo(string.Format("Speech bubbles {0}.", this.m_speechBubblesEnabled ? "enabled" : "disabled"));
		return true;
	}

	// Token: 0x06007660 RID: 30304 RVA: 0x002608DC File Offset: 0x0025EADC
	private bool OnProcessCheat_playAllThinkEmotes(string func, string[] args, string rawArgs)
	{
		if (args.Length != 1)
		{
			UIStatus.Get().AddError("Invalid params for " + func, -1f);
			global::Log.Gameplay.PrintError("Unrecognized number of arguments. Expected \"" + func + " <player>\"", Array.Empty<object>());
			return false;
		}
		string a = args[0].ToLower();
		int num;
		if (!(a == "1") && !(a == "friendly"))
		{
			if (!(a == "2") && !(a == "opponent"))
			{
				UIStatus.Get().AddError("Invalid params for " + func, -1f);
				global::Log.Gameplay.PrintError("Unrecognized player: \"" + args[0] + "\". Expected \"1\", \"2\", \"friendly\", or \"opponent\"", Array.Empty<object>());
				return false;
			}
			num = 2;
		}
		else
		{
			num = 1;
		}
		GameState gameState = GameState.Get();
		global::Entity entity;
		if (gameState == null)
		{
			entity = null;
		}
		else
		{
			global::Player player = gameState.GetPlayer(num);
			entity = ((player != null) ? player.GetHero() : null);
		}
		global::Entity entity2 = entity;
		if (entity2 == null)
		{
			global::Log.Gameplay.PrintError(string.Format("Unable to find Hero for player {0}", num), Array.Empty<object>());
			return false;
		}
		global::Card card = entity2.GetCard();
		Processor.RunCoroutine(this.PlayEmotesInOrder(card, new EmoteType[]
		{
			EmoteType.THINK1,
			EmoteType.THINK2,
			EmoteType.THINK3
		}), null);
		return true;
	}

	// Token: 0x06007661 RID: 30305 RVA: 0x00260A18 File Offset: 0x0025EC18
	private IEnumerator PlayEmotesInOrder(global::Card heroCard, params EmoteType[] emoteTypes)
	{
		if (heroCard == null || emoteTypes == null)
		{
			yield break;
		}
		int num;
		for (int i = 0; i < emoteTypes.Length; i = num)
		{
			if (heroCard.GetEmoteEntry(emoteTypes[i]) == null)
			{
				string text = string.Format("Unable to locate {0} emote for {1}", emoteTypes[i], heroCard);
				UIStatus.Get().AddError(text, -1f);
				global::Log.Gameplay.PrintError(text, Array.Empty<object>());
			}
			else
			{
				heroCard.PlayEmote(emoteTypes[i]);
				if (i < emoteTypes.Length - 1)
				{
					yield return new WaitForSeconds(5f);
				}
			}
			num = i + 1;
		}
		yield break;
	}

	// Token: 0x06007662 RID: 30306 RVA: 0x00260A30 File Offset: 0x0025EC30
	private bool OnProcessCheat_playEmote(string func, string[] args, string rawArgs)
	{
		if (args.Length != 1 && args.Length != 2)
		{
			UIStatus.Get().AddError("Provide 1 to 2 params for " + func + ".", -1f);
			global::Log.Gameplay.PrintError("Unrecognized number of arguments. Expected \"" + func + " <enum_type> <player>\"", Array.Empty<object>());
			return true;
		}
		EmoteType emoteType = EmoteType.INVALID;
		Enum.TryParse<EmoteType>(args[0], true, out emoteType);
		if (!Enum.IsDefined(typeof(EmoteType), emoteType) || emoteType == EmoteType.INVALID)
		{
			Array names = Enum.GetNames(typeof(EmoteType));
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			foreach (object obj in names)
			{
				string value = (string)obj;
				if (num != 0)
				{
					stringBuilder.Append(num);
					stringBuilder.Append(" = ");
					stringBuilder.Append(value);
					stringBuilder.Append('\n');
				}
				num++;
			}
			string str = stringBuilder.ToString();
			UIStatus.Get().AddError("Invalid first param for " + func + ". See \"Messages\".", -1f);
			global::Log.Gameplay.PrintError("Unrecognized <enum_type>.\n" + string.Format("Try a num [1-{0}] or a string:\n", names.Length - 1) + str, Array.Empty<object>());
			return true;
		}
		int id = 1;
		if (args.Length == 2)
		{
			string a = args[1].ToLower();
			if (!(a == "1") && !(a == "friendly"))
			{
				if (!(a == "2") && !(a == "opponent"))
				{
					UIStatus.Get().AddError("Invalid second param for " + func + ". See \"Messages\".", -1f);
					global::Log.Gameplay.PrintError("Unrecognized player: \"" + args[1] + "\". Expected \"1\", \"2\", \"friendly\", or \"opponent\"", Array.Empty<object>());
					return true;
				}
				id = 2;
			}
			else
			{
				id = 1;
			}
		}
		GameState gameState = GameState.Get();
		global::Card card;
		if (gameState == null)
		{
			card = null;
		}
		else
		{
			global::Player player = gameState.GetPlayer(id);
			if (player == null)
			{
				card = null;
			}
			else
			{
				global::Entity hero = player.GetHero();
				card = ((hero != null) ? hero.GetCard() : null);
			}
		}
		global::Card card2 = card;
		if (card2 == null)
		{
			global::Log.Gameplay.PrintError("Unable to find Hero for current player", Array.Empty<object>());
			return false;
		}
		card2.PlayEmote(emoteType);
		return true;
	}

	// Token: 0x06007663 RID: 30307 RVA: 0x00260C94 File Offset: 0x0025EE94
	private bool OnProcessCheat_playAllMissionHeroPowerLines(string func, string[] args, string rawArgs)
	{
		if (args.Length > 1 || args[0] != string.Empty)
		{
			UIStatus.Get().AddError("Invalid params for " + func, -1f);
			global::Log.Gameplay.PrintError("Unrecognized number of arguments. Expected 0 arguments.", Array.Empty<object>());
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
			global::Log.Gameplay.PrintError("This game mode lacks hero power lines.", Array.Empty<object>());
			return false;
		}
		List<string> list = method.Invoke(gameEntity, null) as List<string>;
		if (list == null)
		{
			return false;
		}
		Gameplay.Get().StartCoroutine(this.LoadAndPlayVO(list));
		return true;
	}

	// Token: 0x06007664 RID: 30308 RVA: 0x00260D50 File Offset: 0x0025EF50
	private bool OnProcessCheat_playAllMissionIdleLines(string func, string[] args, string rawArgs)
	{
		if (args.Length > 1 || args[0] != string.Empty)
		{
			UIStatus.Get().AddError("Invalid params for " + func, -1f);
			global::Log.Gameplay.PrintError("Unrecognized number of arguments. Expected 0 arguments.", Array.Empty<object>());
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
			global::Log.Gameplay.PrintError("This game mode lacks idle lines.", Array.Empty<object>());
			return false;
		}
		List<string> list = method.Invoke(gameEntity, null) as List<string>;
		if (list == null)
		{
			return false;
		}
		Gameplay.Get().StartCoroutine(this.LoadAndPlayVO(list));
		return true;
	}

	// Token: 0x06007665 RID: 30309 RVA: 0x00260E0A File Offset: 0x0025F00A
	private IEnumerator LoadAndPlayVO(List<string> assets)
	{
		if (assets == null || assets.Count == 0)
		{
			yield break;
		}
		foreach (string text in assets)
		{
			if (SoundLoader.LoadSound(text, new PrefabCallback<GameObject>(this.OnVoLoaded), null, null))
			{
				if (text != assets.Last<string>())
				{
					yield return new WaitForSeconds(10f);
				}
			}
			else
			{
				string text2 = "Error loading asset " + text.ToString();
				global::Log.Gameplay.PrintError(text2, Array.Empty<object>());
				UIStatus.Get().AddError(text2, -1f);
			}
		}
		List<string>.Enumerator enumerator = default(List<string>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06007666 RID: 30310 RVA: 0x00260E20 File Offset: 0x0025F020
	private void OnVoLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null || string.IsNullOrEmpty(assetRef))
		{
			return;
		}
		Debug.LogFormat("Now playing \"{0}\"", new object[]
		{
			assetRef.ToString()
		});
		AudioSource component = go.GetComponent<AudioSource>();
		SoundManager.Get().PlayPreloaded(component);
		string[] array = assetRef.ToString().Split(new char[]
		{
			':'
		});
		string key = array[0].Substring(0, array[0].Length - ".prefab".Length);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		NotificationManager notificationManager = NotificationManager.Get();
		Notification notification = notificationManager.CreateSpeechBubble(GameStrings.Get(key), Notification.SpeechBubbleDirection.TopRight, actor, false, true, 0f);
		notificationManager.DestroyNotification(notification, component.clip.length);
	}

	// Token: 0x06007667 RID: 30311 RVA: 0x00260EEC File Offset: 0x0025F0EC
	private bool OnProcessCheat_audioChannel(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in Enum.GetValues(typeof(Global.SoundCategory)))
			{
				Global.SoundCategory soundCategory = (Global.SoundCategory)obj;
				stringBuilder.Append(string.Format("\n{0}: {1}", soundCategory, (!this.m_audioChannelEnabled.ContainsKey(soundCategory) || this.m_audioChannelEnabled[soundCategory]) ? "enabled" : "disabled"));
			}
			UIStatus.Get().AddInfo(string.Format("Audio channels:{0}", stringBuilder.ToString()), 5f);
			return true;
		}
		if (args.Length > 2)
		{
			UIStatus.Get().AddError(string.Format("Argument format: [audio channel name] [on/off]", Array.Empty<object>()), -1f);
			return true;
		}
		try
		{
			Global.SoundCategory soundCategory2 = (Global.SoundCategory)Enum.Parse(typeof(Global.SoundCategory), args[0], true);
			if (args.Length == 1 || string.IsNullOrEmpty(args[1]))
			{
				UIStatus.Get().AddInfo(string.Format("Audio channel {0} is {1}", soundCategory2, this.m_audioChannelEnabled[soundCategory2] ? "on" : "off"));
				return true;
			}
			if (args[1].ToLower() != "on" && args[1].ToLower() != "off")
			{
				UIStatus.Get().AddError(string.Format("Second argument must be \"on\" or \"off\"", Array.Empty<object>()), -1f);
				return true;
			}
			this.m_audioChannelEnabled[soundCategory2] = (args[1].ToLower() == "on");
			SoundManager.Get().UpdateCategoryVolume(soundCategory2);
			UIStatus.Get().AddInfo(string.Format("Audio channel {0} has been {1}", soundCategory2, this.m_audioChannelEnabled[soundCategory2] ? "enabled" : "disabled"));
		}
		catch (ArgumentException)
		{
			UIStatus.Get().AddError(string.Format("{0} is not an audio channel. Type audiochannel to see a list of channels.", args[0]), -1f);
		}
		return true;
	}

	// Token: 0x06007668 RID: 30312 RVA: 0x00261148 File Offset: 0x0025F348
	private bool OnProcessCheat_audioChannelGroup(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string arg in this.m_audioChannelGroups.Keys)
			{
				stringBuilder.Append(string.Format("\n{0}", arg));
			}
			UIStatus.Get().AddInfo(string.Format("Audio channel groups:{0}", stringBuilder.ToString()), 5f);
			return true;
		}
		if (args.Length != 2)
		{
			UIStatus.Get().AddError(string.Format("Argument format: [audio channel group name] [on/off]", Array.Empty<object>()), -1f);
			return true;
		}
		if (!this.m_audioChannelGroups.ContainsKey(args[0].ToUpper()))
		{
			UIStatus.Get().AddError(string.Format("{0} is not an audio channel group. Type audiochannelgroup to see a list of channel groups.", args[0]), -1f);
			return true;
		}
		if (args[1].ToLower() != "on" && args[1].ToLower() != "off")
		{
			UIStatus.Get().AddError(string.Format("Second argument must be \"on\" or \"off\"", Array.Empty<object>()), -1f);
			return true;
		}
		foreach (Global.SoundCategory soundCategory in this.m_audioChannelGroups[args[0].ToUpper()])
		{
			if (this.m_audioChannelEnabled.ContainsKey(soundCategory))
			{
				this.m_audioChannelEnabled[soundCategory] = (args[1].ToLower() == "on");
				SoundManager.Get().UpdateCategoryVolume(soundCategory);
			}
		}
		UIStatus.Get().AddInfo(string.Format("Audio channel group {0} has been {1}", args[0], (args[1].ToLower() == "on") ? "enabled" : "disabled"));
		return true;
	}

	// Token: 0x06007669 RID: 30313 RVA: 0x0026133C File Offset: 0x0025F53C
	private bool TryParsePlayerTags(string input, out string output)
	{
		if (string.IsNullOrEmpty(input))
		{
			UIStatus.Get().AddInfo(string.Format("Player tags cleared.", Array.Empty<object>()));
			output = input;
			return true;
		}
		string[] array = input.Split(new char[]
		{
			','
		});
		if (array.Length > 20)
		{
			output = "";
			UIStatus.Get().AddError(string.Format("{0} tag values found, but only {1} tag values can be passed.", array.Length, 20), -1f);
			return false;
		}
		foreach (string text in array)
		{
			if (!string.IsNullOrEmpty(text))
			{
				string[] array3 = text.Split(new char[]
				{
					'='
				});
				if (array3.Length != 2)
				{
					output = "";
					UIStatus.Get().AddError(string.Format("Invalid tag/value entry: \"{0}\". Format is \"TagId=Value\".", text), -1f);
					return false;
				}
				int num = 0;
				int num2 = 0;
				if (!int.TryParse(array3[0], out num))
				{
					output = "";
					UIStatus.Get().AddError(string.Format("Invalid tagId: \"{0}\". Must be an integer.", array3[0]), -1f);
					return false;
				}
				if (!int.TryParse(array3[1], out num2))
				{
					num2 = GameUtils.TranslateCardIdToDbId(array3[1], true);
					if (num2 == 0)
					{
						output = "";
						UIStatus.Get().AddError(string.Format("Invalid tagValue: \"{0}\". Must be an integer.", array3[1]), -1f);
						return false;
					}
				}
				if (num > 999999)
				{
					output = "";
					UIStatus.Get().AddError(string.Format("Invalid tagId: \"{0}\". Must be < {1}.", num, 999999), -1f);
					return false;
				}
				if (num <= 0)
				{
					output = "";
					UIStatus.Get().AddError(string.Format("Invalid tagId: \"{0}\". Must be > 0.", num), -1f);
					return false;
				}
				if (num2 > 999999)
				{
					output = "";
					UIStatus.Get().AddError(string.Format("Invalid tagValue: \"{0}\". Must be < {1}.", num2, 999999), -1f);
					return false;
				}
			}
		}
		UIStatus.Get().AddInfo(string.Format("Player tags set for next game.", Array.Empty<object>()));
		output = input;
		return true;
	}

	// Token: 0x0600766A RID: 30314 RVA: 0x0026155C File Offset: 0x0025F75C
	private bool TryParseArenaChoices(string[] input, out string[] output)
	{
		List<string> list = new List<string>();
		bool result = input.Length != 0;
		for (int i = 0; i < input.Length; i++)
		{
			string text = input[i].Replace(",", "");
			int num = 0;
			if (!int.TryParse(text, out num))
			{
				num = GameUtils.TranslateCardIdToDbId(text, false);
				if (num == 0)
				{
					UIStatus.Get().AddError(string.Format("Invalid tagValue: \"{0}\". Must be an integer or valid card Id.", text), -1f);
					result = false;
					break;
				}
				text = num.ToString();
			}
			if (num > 999999)
			{
				UIStatus.Get().AddError(string.Format("Invalid card ID: \"{0}\". Must be < {1}.", num, 999999), -1f);
				result = false;
				break;
			}
			if (num <= 0)
			{
				UIStatus.Get().AddError(string.Format("Invalid card ID: \"{0}\". Must be > 0.", num), -1f);
				result = false;
				break;
			}
			list.Add(text);
		}
		output = list.ToArray();
		return result;
	}

	// Token: 0x0600766B RID: 30315 RVA: 0x00261654 File Offset: 0x0025F854
	private bool TryParseNamedArgs(string[] args, out global::Map<string, Cheats.NamedParam> values)
	{
		values = new global::Map<string, Cheats.NamedParam>();
		for (int i = 0; i < args.Length; i++)
		{
			string[] array = args[i].Trim().Split(new char[]
			{
				'='
			});
			if (array.Length > 1)
			{
				values.Add(array[0], new Cheats.NamedParam(array[1]));
			}
		}
		return values.Count > 0;
	}

	// Token: 0x0600766C RID: 30316 RVA: 0x002616B3 File Offset: 0x0025F8B3
	private bool OnProcessCheat_resettips(string func, string[] args, string rawArgs)
	{
		Options.Get().SetBool(global::Option.HAS_SEEN_COLLECTIONMANAGER, false);
		return true;
	}

	// Token: 0x0600766D RID: 30317 RVA: 0x002616C6 File Offset: 0x0025F8C6
	private bool OnProcessCheat_brode(string func, string[] args, string rawArgs)
	{
		NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.ALL, new Vector3(133.1f, NotificationManager.DEPTH, 54.2f), GameStrings.Get("VO_INNKEEPER_FORGE_1WIN"), "VO_INNKEEPER_ARENA_1WIN.prefab:31bb13e800c74c0439ee1a7bfc1e3499", 0f, null, false);
		return true;
	}

	// Token: 0x0600766E RID: 30318 RVA: 0x000052EC File Offset: 0x000034EC
	private bool On_ProcessCheat_bug(string func, string[] args, string rawArgs)
	{
		return true;
	}

	// Token: 0x0600766F RID: 30319 RVA: 0x00261700 File Offset: 0x0025F900
	private bool On_ProcessCheat_ANR(string func, string[] args, string rawArgs)
	{
		if (!ExceptionReporter.Get().IsEnabledANRMonitor)
		{
			UIStatus.Get().AddInfo("ANR Monitor of ExceptionReporter is disabled");
			return true;
		}
		try
		{
			this.m_waitTime = float.Parse(args[0]);
		}
		catch
		{
		}
		this.m_showedMessage = false;
		Processor.RegisterUpdateDelegate(new Action(this.SimulatorPauseUpdate));
		return true;
	}

	// Token: 0x06007670 RID: 30320 RVA: 0x00261768 File Offset: 0x0025F968
	private void SimulatorPauseUpdate()
	{
		UIStatus.Get().AddInfo("Wait for " + this.m_waitTime + " seconds");
		if (this.m_showedMessage)
		{
			Thread.Sleep((int)(this.m_waitTime * 1000f));
			Processor.UnregisterUpdateDelegate(new Action(this.SimulatorPauseUpdate));
		}
		this.m_showedMessage = true;
	}

	// Token: 0x06007671 RID: 30321 RVA: 0x000052EC File Offset: 0x000034EC
	private bool OnProcessCheat_igm(string func, string[] args, string rawArgs)
	{
		return true;
	}

	// Token: 0x06007672 RID: 30322 RVA: 0x002617CC File Offset: 0x0025F9CC
	private bool OnProcessCheat_msgui(string func, string[] args, string rawArgs)
	{
		string value = "show";
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
		{
			value = args[0];
		}
		if ("add".StartsWith(value))
		{
			this.AddMessagePopupForArgs(args);
		}
		else if ("help".StartsWith(value))
		{
			UIStatus.Get().AddInfo(string.Format("USAGE: msgui [add] [text|shop] [imageType|pid]", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007673 RID: 30323 RVA: 0x00261830 File Offset: 0x0025FA30
	private void AddMessagePopupForArgs(string[] args)
	{
		MessageUIData messageUIData = Cheats.ConstructUIDataFromArgs(args);
		if (messageUIData == null)
		{
			global::Log.InGameMessage.PrintDebug("Failed to construct UI Data for test IGM", Array.Empty<object>());
			return;
		}
		MessagePopupDisplay messagePopupDisplay = HearthstoneServices.Get<MessagePopupDisplay>();
		if (messagePopupDisplay == null)
		{
			UIStatus.Get().AddError("Message Popup Display was not available to show a message", -1f);
			return;
		}
		messagePopupDisplay.QueueMessage(messageUIData);
	}

	// Token: 0x06007674 RID: 30324 RVA: 0x00261884 File Offset: 0x0025FA84
	private static MessageUIData ConstructUIDataFromArgs(string[] args)
	{
		MessageContentType contentTypeIfAvailable = Cheats.GetContentTypeIfAvailable(args);
		if (contentTypeIfAvailable == MessageContentType.INVALID)
		{
			return null;
		}
		MessageUIData messageUIData = new MessageUIData
		{
			ContentType = contentTypeIfAvailable,
			MessageData = Cheats.ConstructContentDataForMessage(contentTypeIfAvailable, args)
		};
		if (messageUIData.MessageData == null)
		{
			return null;
		}
		return messageUIData;
	}

	// Token: 0x06007675 RID: 30325 RVA: 0x002618C2 File Offset: 0x0025FAC2
	private static object ConstructContentDataForMessage(MessageContentType contentType, string[] args)
	{
		if (contentType == MessageContentType.TEXT)
		{
			return Cheats.ConstructTestTextMsg(args);
		}
		if (contentType != MessageContentType.SHOP)
		{
			UIStatus.Get().AddError(string.Format("Unsupported content type {0}", contentType), -1f);
			return null;
		}
		return Cheats.ConstructTestShopMsg(args);
	}

	// Token: 0x06007676 RID: 30326 RVA: 0x002618FC File Offset: 0x0025FAFC
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

	// Token: 0x06007677 RID: 30327 RVA: 0x00261948 File Offset: 0x0025FB48
	private static ShopMessageContent ConstructTestShopMsg(string[] args)
	{
		long productID = 10747L;
		if (args.Length > 2 && !string.IsNullOrEmpty(args[2]) && !long.TryParse(args[2], out productID))
		{
			UIStatus.Get().AddError("Invalid product id for show igm: " + args[2], -1f);
			return null;
		}
		return new ShopMessageContent
		{
			Title = "Lorem Ipsum",
			TextBody = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ut rhoncus ante. Donec in pretium felis. Duis mollis purus a ante mollis luctus. Nulla hendrerit gravida nulla non convallis. Vivamus vel ligula a mi porta porta et at magna. Nulla euismod diam eget arcu pharetra scelerisque. In id sem a ipsum maximus cursus. In pulvinar fermentum dolor, at ultrices ipsum congue nec.",
			ProductID = productID
		};
	}

	// Token: 0x06007678 RID: 30328 RVA: 0x002619B8 File Offset: 0x0025FBB8
	private static MessageContentType GetContentTypeIfAvailable(string[] args)
	{
		MessageContentType result = MessageContentType.TEXT;
		if (args.Length > 1 && !string.IsNullOrEmpty(args[1]))
		{
			string text = args[1].ToLower();
			if (!(text == "text"))
			{
				if (!(text == "shop"))
				{
					result = MessageContentType.INVALID;
					UIStatus.Get().AddError("Invalid message type to show " + text, -1f);
				}
				else
				{
					result = MessageContentType.SHOP;
				}
			}
			else
			{
				result = MessageContentType.TEXT;
			}
		}
		return result;
	}

	// Token: 0x06007679 RID: 30329 RVA: 0x00261A24 File Offset: 0x0025FC24
	private bool On_ProcessCheat_crash(string func, string[] args, string rawArgs)
	{
		string[] value = new string[]
		{
			"help",
			"cs",
			"plugin",
			"nativeinlib",
			"javainlib"
		};
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

	// Token: 0x0600767A RID: 30330 RVA: 0x00261B78 File Offset: 0x0025FD78
	private bool OnProcessCheat_questcompletepopup(string func, string[] args, string rawArgs)
	{
		int achieveID = 0;
		global::Achievement achievement = int.TryParse(rawArgs, out achieveID) ? AchieveManager.Get().GetAchievement(achieveID) : null;
		if (achievement == null)
		{
			UIStatus.Get().AddError(string.Format("{0}: please specify a valid Quest ID", func), -1f);
			return true;
		}
		QuestToast.ShowQuestToast(UserAttentionBlocker.ALL, null, false, achievement);
		return true;
	}

	// Token: 0x0600767B RID: 30331 RVA: 0x00261BCC File Offset: 0x0025FDCC
	private bool OnProcessCheat_narrative(string func, string[] args, string rawArgs)
	{
		if (args.Length == 1 && args[0] == "clear")
		{
			List<global::Option> source = NarrativeManager.Get().Cheat_ClearAllSeen();
			string message = string.Format("Narrative seen options cleared:\n{0}", string.Join(", ", (from o in source
			select global::EnumUtils.GetString<global::Option>(o)).ToArray<string>()));
			UIStatus.Get().AddInfo(message);
			return true;
		}
		int num = 0;
		if ((int.TryParse(rawArgs, out num) ? AchieveManager.Get().GetAchievement(num) : null) == null)
		{
			UIStatus.Get().AddError(string.Format("{0}: please specify a valid Quest ID", func), -1f);
			return true;
		}
		NarrativeManager.Get().OnQuestCompleteShown(num);
		NarrativeManager.Get().ShowOutstandingQuestDialogs();
		return true;
	}

	// Token: 0x0600767C RID: 30332 RVA: 0x00261C94 File Offset: 0x0025FE94
	private bool OnProcessCheat_narrativedialog(string func, string[] args, string rawArgs)
	{
		int dialogSequenceId = 0;
		CharacterDialogSequence characterDialogSequence = int.TryParse(rawArgs, out dialogSequenceId) ? new CharacterDialogSequence(dialogSequenceId, CharacterDialogEventType.UNSPECIFIED) : null;
		if (characterDialogSequence == null)
		{
			UIStatus.Get().AddError(string.Format("{0}: please specify a valid Dialog ID", func), -1f);
			return true;
		}
		NarrativeManager.Get().PushDialogSequence(characterDialogSequence);
		return true;
	}

	// Token: 0x0600767D RID: 30333 RVA: 0x00261CE4 File Offset: 0x0025FEE4
	private bool OnProcessCheat_questwelcome(string func, string[] args, string rawArgs)
	{
		bool fromLogin = true;
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]))
		{
			GeneralUtils.TryParseBool(args[0], out fromLogin);
		}
		WelcomeQuests.Show(UserAttentionBlocker.ALL, fromLogin, null, false);
		return true;
	}

	// Token: 0x0600767E RID: 30334 RVA: 0x00261D18 File Offset: 0x0025FF18
	private bool OnProcessCheat_newquestvisual(string func, string[] args, string rawArgs)
	{
		if (WelcomeQuests.Get() == null)
		{
			UIStatus.Get().AddError("WelcomeQuests object is not active - try using 'questwelcome' cheat first.", -1f);
			return true;
		}
		int achieveID = 0;
		global::Achievement achievement = int.TryParse(rawArgs, out achieveID) ? AchieveManager.Get().GetAchievement(achieveID) : null;
		if (achievement == null)
		{
			UIStatus.Get().AddError(string.Format("{0}: please specify a valid Quest ID", func), -1f);
			return true;
		}
		WelcomeQuests.Get().GetFirstQuestTile().SetupTile(achievement, global::QuestTile.FsmEvent.QuestGranted);
		return true;
	}

	// Token: 0x0600767F RID: 30335 RVA: 0x00261D94 File Offset: 0x0025FF94
	private bool OnProcessCheat_questprogresspopup(string func, string[] args, string rawArgs)
	{
		int num = 0;
		global::Achievement achievement = (args.Length != 0 && int.TryParse(args[0], out num)) ? AchieveManager.Get().GetAchievement(num) : null;
		int num2 = 1;
		string questName;
		string questDescription;
		int num3;
		int maxProgress;
		if (achievement == null)
		{
			if (num != 0)
			{
				UIStatus.Get().AddError("unknown Achieve with ID " + num, -1f);
				return true;
			}
			if (args.Length < 4)
			{
				UIStatus.Get().AddError("please specify an Achieve ID or the following params:\n<title> <description> <progress> <maxprogress>", -1f);
				return true;
			}
			questName = args[0];
			questDescription = args[1];
			int.TryParse(args[2], out num3);
			int.TryParse(args[3], out maxProgress);
		}
		else
		{
			questName = achievement.Name;
			questDescription = achievement.Description;
			num3 = achievement.Progress;
			maxProgress = achievement.MaxProgress;
		}
		for (int i = 0; i < args.Length; i++)
		{
			string[] array = args[i].Split(new char[]
			{
				'='
			});
			if (array.Length >= 2)
			{
				string a = array[0];
				string text = array[1];
				if (a == "count" && !int.TryParse(text, out num2))
				{
					UIStatus.Get().AddError(string.Format("Unable to parse parameter #{0} as integer: {1}", i + 1, text), -1f);
					return true;
				}
			}
		}
		if (GameToastMgr.Get() != null)
		{
			if (num3 >= maxProgress)
			{
				num3 = maxProgress - 1;
			}
			for (int j = 0; j < num2; j++)
			{
				GameToastMgr.Get().AddQuestProgressToast(num, questName, questDescription, num3, maxProgress);
			}
			return true;
		}
		UIStatus.Get().AddError("GameToastMgr is null!", -1f);
		return true;
	}

	// Token: 0x06007680 RID: 30336 RVA: 0x00261F14 File Offset: 0x00260114
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

	// Token: 0x06007681 RID: 30337 RVA: 0x00261F60 File Offset: 0x00260160
	private bool OnProcessCheat_storepassword(string func, string[] args, string rawArgs)
	{
		if (this.m_loadingStoreChallengePrompt)
		{
			return true;
		}
		if (this.m_storeChallengePrompt == null)
		{
			this.m_loadingStoreChallengePrompt = true;
			PrefabCallback<GameObject> callback = delegate(AssetReference assetRef, GameObject go, object callbackData)
			{
				Processor.RunCoroutine(this.StorePasswordCoroutine(assetRef, go, callbackData), null);
			};
			AssetLoader.Get().InstantiatePrefab("StoreChallengePrompt.prefab:43f02a51d311c214aa25232228ccefef", callback, null, AssetLoadingOptions.None);
		}
		else if (this.m_storeChallengePrompt.IsShown())
		{
			this.m_storeChallengePrompt.Hide();
		}
		else
		{
			Processor.RunCoroutine(this.StorePasswordCoroutine(this.m_storeChallengePrompt.name, this.m_storeChallengePrompt.gameObject, null), null);
		}
		return true;
	}

	// Token: 0x06007682 RID: 30338 RVA: 0x00261FF8 File Offset: 0x002601F8
	private bool OnProcessCheat_notice(string func, string[] args, string rawArgs)
	{
		if (args.Count<string>() < 2)
		{
			UIStatus.Get().AddError("notice cheat requires 2 params: [string]type [int]data [OPTIONAL int]data2 [OPTIONAL bool]quest toast?", -1f);
			return true;
		}
		int num = -1;
		int.TryParse(args[1], out num);
		if (num < 0)
		{
			UIStatus.Get().AddError(string.Format("{0}: please specify a valid Notice Data Value", num), -1f);
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
		global::Achievement achievement = new global::Achievement();
		List<RewardData> list = new List<RewardData>();
		string text2 = args[0];
		uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text2);
		if (num2 <= 932599448U)
		{
			if (num2 <= 634254458U)
			{
				if (num2 != 138294084U)
				{
					if (num2 == 634254458U)
					{
						if (text2 == "arcane_orbs")
						{
							if (flag)
							{
								list.Add(RewardUtils.CreateArcaneOrbRewardData(num));
								goto IL_4EC;
							}
							notice = new NetCache.ProfileNoticeRewardCurrency
							{
								CurrencyType = PegasusShared.CurrencyType.CURRENCY_TYPE_ARCANE_ORBS,
								Amount = num
							};
							goto IL_4EC;
						}
					}
				}
				else if (text2 == "license")
				{
					flag = false;
					NetCache.NetCacheAccountLicenses netObject = NetCache.Get().GetNetObject<NetCache.NetCacheAccountLicenses>();
					NetCache.ProfileNoticeAcccountLicense profileNoticeAcccountLicense = new NetCache.ProfileNoticeAcccountLicense();
					profileNoticeAcccountLicense.License = (long)num;
					profileNoticeAcccountLicense.Origin = NetCache.ProfileNotice.NoticeOrigin.ACCOUNT_LICENSE_FLAGS;
					profileNoticeAcccountLicense.OriginData = 1L;
					if (netObject.AccountLicenses.ContainsKey(profileNoticeAcccountLicense.License))
					{
						profileNoticeAcccountLicense.CasID = netObject.AccountLicenses[profileNoticeAcccountLicense.License].CasId + 1L;
					}
					notice = profileNoticeAcccountLicense;
					goto IL_4EC;
				}
			}
			else if (num2 != 858780363U)
			{
				if (num2 == 932599448U)
				{
					if (text2 == "cardback")
					{
						if (GameDbf.CardBack.GetRecord(num) == null)
						{
							UIStatus.Get().AddError(string.Format("Cardback ID is invalid: {0}", num), -1f);
							return true;
						}
						if (flag)
						{
							list.Add(new CardBackRewardData
							{
								CardBackID = num
							});
							goto IL_4EC;
						}
						notice = new NetCache.ProfileNoticeRewardCardBack
						{
							CardBackID = num
						};
						goto IL_4EC;
					}
				}
			}
			else if (text2 == "tavern_brawl_rewards")
			{
				NetCache.ProfileNoticeTavernBrawlRewards profileNoticeTavernBrawlRewards = new NetCache.ProfileNoticeTavernBrawlRewards();
				profileNoticeTavernBrawlRewards.Wins = num;
				TavernBrawlMode mode = text.Equals("heroic") ? TavernBrawlMode.TB_MODE_HEROIC : TavernBrawlMode.TB_MODE_NORMAL;
				profileNoticeTavernBrawlRewards.Mode = mode;
				profileNoticeTavernBrawlRewards.Chest = RewardUtils.GenerateTavernBrawlRewardChest_CHEAT(num, mode);
				notice = profileNoticeTavernBrawlRewards;
				goto IL_4EC;
			}
		}
		else if (num2 <= 3389733797U)
		{
			if (num2 != 2284280159U)
			{
				if (num2 == 3389733797U)
				{
					if (text2 == "dust")
					{
						if (flag)
						{
							list.Add(new ArcaneDustRewardData
							{
								Amount = num
							});
							goto IL_4EC;
						}
						notice = new NetCache.ProfileNoticeRewardDust
						{
							Amount = num
						};
						goto IL_4EC;
					}
				}
			}
			else if (text2 == "card")
			{
				string text3 = "NEW1_040";
				if (!string.IsNullOrEmpty(text))
				{
					int num3 = -1;
					int.TryParse(text, out num3);
					if (num3 > 0)
					{
						text3 = GameUtils.TranslateDbIdToCardId(num3, false);
					}
					else
					{
						text3 = text;
					}
				}
				if (GameUtils.GetCardRecord(text3) == null)
				{
					UIStatus.Get().AddError(string.Format("Card ID is invalid: {0}", text3), -1f);
					return true;
				}
				if (flag)
				{
					list.Add(new CardRewardData
					{
						CardID = text3,
						Count = Mathf.Clamp(num, 1, 2)
					});
					goto IL_4EC;
				}
				notice = new NetCache.ProfileNoticeRewardCard
				{
					CardID = text3,
					Quantity = Mathf.Clamp(num, 1, 2)
				};
				goto IL_4EC;
			}
		}
		else if (num2 != 3966162835U)
		{
			if (num2 != 4264611999U)
			{
				if (num2 == 4286327515U)
				{
					if (text2 == "booster")
					{
						int num4 = 1;
						if (!string.IsNullOrEmpty(text))
						{
							int.TryParse(text, out num4);
						}
						if (GameDbf.Booster.GetRecord(num4) == null)
						{
							UIStatus.Get().AddError(string.Format("Booster ID is invalid: {0}", num4), -1f);
							return true;
						}
						if (flag)
						{
							list.Add(new BoosterPackRewardData
							{
								Id = num4,
								Count = num
							});
							goto IL_4EC;
						}
						notice = new NetCache.ProfileNoticeRewardBooster
						{
							Count = num,
							Id = num4
						};
						goto IL_4EC;
					}
				}
			}
			else if (text2 == "event")
			{
				flag = true;
				list.Add(new EventRewardData
				{
					EventType = num
				});
				goto IL_4EC;
			}
		}
		else if (text2 == "gold")
		{
			if (flag)
			{
				list.Add(new GoldRewardData
				{
					Amount = (long)num
				});
				goto IL_4EC;
			}
			notice = new NetCache.ProfileNoticeRewardCurrency
			{
				CurrencyType = PegasusShared.CurrencyType.CURRENCY_TYPE_GOLD,
				Amount = num
			};
			goto IL_4EC;
		}
		UIStatus.Get().AddError(string.Format("{0}: please specify a valid Notice Type.\nValid Types are: 'gold','arcane_orbs','dust','booster','card','cardback','tavern_brawl_rewards','event','license'", args[0]), -1f);
		return true;
		IL_4EC:
		if (flag)
		{
			achievement.SetDescription("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "");
			achievement.SetName("Title Text", "");
			QuestToast.ShowQuestToast(UserAttentionBlocker.ALL, null, false, achievement);
		}
		else
		{
			NetCache.Get().Cheat_AddNotice(notice);
		}
		return true;
	}

	// Token: 0x06007683 RID: 30339 RVA: 0x00262530 File Offset: 0x00260730
	private bool OnProcessCheat_LoadWidget(string func, string[] args, string rawArgs)
	{
		string text = args[0];
		if (string.IsNullOrEmpty(text))
		{
			UIStatus.Get().AddError("First parameter must be the GUID of a valid widget template.", -1f);
			return false;
		}
		WidgetInstance widgetInstance = WidgetInstance.Create(text, false);
		if (widgetInstance == null)
		{
			UIStatus.Get().AddError("First parameter must be the GUID of a valid widget template.", -1f);
			return false;
		}
		this.s_createdWidgets.Add(widgetInstance);
		widgetInstance.TriggerEvent("CHEATED_STATE", default(Widget.TriggerEventParameters));
		return true;
	}

	// Token: 0x06007684 RID: 30340 RVA: 0x002625A8 File Offset: 0x002607A8
	private bool OnProcessCheat_ClearWidgets(string func, string[] args, string rawArgs)
	{
		foreach (WidgetInstance widgetInstance in this.s_createdWidgets)
		{
			UnityEngine.Object.Destroy(widgetInstance.gameObject);
		}
		this.s_createdWidgets.Clear();
		return true;
	}

	// Token: 0x06007685 RID: 30341 RVA: 0x0026260C File Offset: 0x0026080C
	private bool OnProcessCheat_ServerLog(string func, string[] args, string rawArgs)
	{
		ScriptLogMessage scriptLogMessage = new ScriptLogMessage();
		scriptLogMessage.Message = rawArgs;
		scriptLogMessage.Event = "Cheat";
		scriptLogMessage.Severity = 1;
		SceneDebugger.Get().AddServerScriptLogMessage(scriptLogMessage);
		return true;
	}

	// Token: 0x06007686 RID: 30342 RVA: 0x00262644 File Offset: 0x00260844
	private bool OnProcessCheat_dialogEvent(string func, string[] args, string rawArgs)
	{
		if (args.Length != 1)
		{
			UIStatus.Get().AddError("Provide 1 param for " + func + ".", -1f);
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
		ScheduledCharacterDialogEvent scheduledCharacterDialogEvent = ScheduledCharacterDialogEvent.INVALID;
		Enum.TryParse<ScheduledCharacterDialogEvent>(args[0], true, out scheduledCharacterDialogEvent);
		if (!Enum.IsDefined(typeof(ScheduledCharacterDialogEvent), scheduledCharacterDialogEvent) || scheduledCharacterDialogEvent == ScheduledCharacterDialogEvent.INVALID)
		{
			Array names = Enum.GetNames(typeof(ScheduledCharacterDialogEvent));
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("reset -- this allows events to run again");
			stringBuilder.Append('\n');
			int num = 0;
			foreach (object obj in names)
			{
				string value = (string)obj;
				if (num != 0)
				{
					stringBuilder.Append(num);
					stringBuilder.Append(" = ");
					stringBuilder.Append(value);
					stringBuilder.Append('\n');
				}
				num++;
			}
			string str = stringBuilder.ToString();
			UIStatus.Get().AddError("Invalid param for " + func + ". See \"Messages\".", -1f);
			global::Log.Gameplay.PrintError("Unrecognized <event_type>.\n" + string.Format("Try a num [1-{0}] or a string:\n", names.Length - 1) + str, Array.Empty<object>());
			return true;
		}
		narrativeManager.TriggerScheduledCharacterDialogEvent_Debug(scheduledCharacterDialogEvent);
		return true;
	}

	// Token: 0x06007687 RID: 30343 RVA: 0x002627E8 File Offset: 0x002609E8
	private bool OnProcessCheat_account(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = "add, remove, set, skip, unlock";
		if (autofillData != null)
		{
			if ((rawArgs.EndsWith(" ") && args.Length == 0) || args.Length == 1)
			{
				string[] values = text.Split(new char[]
				{
					' ',
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				string searchTerm = (args.Length == 0) ? string.Empty : args[0];
				return this.ProcessAutofillParam(values, searchTerm, autofillData);
			}
			return false;
		}
		else
		{
			string message = "account cheat requires one of the following valid sub-commands: " + text;
			if (args.Length == 0)
			{
				UIStatus.Get().AddError(message, -1f);
				return true;
			}
			string a = args[0].ToLower();
			string[] args2 = args.Skip(1).ToArray<string>();
			if (!(a == "add"))
			{
				if (!(a == "remove"))
				{
					if (!(a == "set"))
					{
						if (!(a == "skip"))
						{
							if (!(a == "unlock"))
							{
								UIStatus.Get().AddError(message, -1f);
							}
							else
							{
								HttpCheater.Get().RunUnlockResourceCommand(args2);
							}
						}
						else
						{
							HttpCheater.Get().RunSkipResourceCommand(args2);
						}
					}
					else
					{
						HttpCheater.Get().RunSetResourceCommand(args2);
					}
				}
				else
				{
					HttpCheater.Get().RunRemoveResourceCommand(args2);
				}
			}
			else
			{
				HttpCheater.Get().RunAddResourceCommand(args2);
			}
			return true;
		}
	}

	// Token: 0x06007688 RID: 30344 RVA: 0x00262924 File Offset: 0x00260B24
	private bool OnProcessCheat_SkipSendingGetGameState(string func, string[] args, string rawArgs)
	{
		int num = 0;
		if (args.Length != 0 && int.TryParse(args[0], out num))
		{
			this.m_skipSendingGetGameState = (num != 0);
			return true;
		}
		return false;
	}

	// Token: 0x06007689 RID: 30345 RVA: 0x00262950 File Offset: 0x00260B50
	private bool OnProcessCheat_SendGetGameState(string func, string[] args, string rawArgs)
	{
		if (this.m_skipSendingGetGameState)
		{
			Network.Get().GetGameState();
			return true;
		}
		return false;
	}

	// Token: 0x0600768A RID: 30346 RVA: 0x00262968 File Offset: 0x00260B68
	private string GetChallengeUrl(string type)
	{
		string text = string.Format("https://login-qa-us.web.blizzard.net/login/admin/challenge/create/ct_{0}", type.ToLower());
		string format = "{0}?email={1}&programId={2}&platformId={3}&redirectUrl={4}&messageKey={5}&notifyRisk={6}&chooseChallenge={7}&challengeType={8}&riskTransId={9}";
		string text2 = "joe_balance@zmail.blizzard.com";
		string text3 = "wtcg";
		string text4 = "*";
		string text5 = "none";
		string text6 = "";
		bool flag = false;
		bool flag2 = false;
		string text7 = "";
		string text8 = "";
		return string.Format(format, new object[]
		{
			text,
			text2,
			text3,
			text4,
			text5,
			text6,
			flag,
			flag2,
			text7,
			text8
		});
	}

	// Token: 0x0600768B RID: 30347 RVA: 0x00262A04 File Offset: 0x00260C04
	private IEnumerator StorePasswordCoroutine(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_loadingStoreChallengePrompt = false;
		this.m_storeChallengePrompt = go.GetComponent<StoreChallengePrompt>();
		this.m_storeChallengePrompt.Hide();
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary["Accept"] = "application/json;charset=UTF-8";
		dictionary["Accept-Language"] = Localization.GetBnetLocaleName();
		string challengeUrl = this.GetChallengeUrl("cvv");
		Debug.Log("creating challenge with url " + challengeUrl);
		IHttpRequest createChallenge = HttpRequestFactory.Get().CreateGetRequest(challengeUrl);
		createChallenge.SetRequestHeaders(dictionary);
		yield return createChallenge.SendRequest();
		Debug.Log("challenge response is " + createChallenge.ResponseAsString);
		string text = (string)(Json.Deserialize(createChallenge.ResponseAsString) as JsonNode)["challenge_url"];
		Debug.Log("challenge url is " + text);
		yield return this.m_storeChallengePrompt.StartCoroutine(this.m_storeChallengePrompt.Show(text));
		yield break;
	}

	// Token: 0x0600768C RID: 30348 RVA: 0x00262A1C File Offset: 0x00260C1C
	private bool OnProcessCheat_favoritecardback(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0)
		{
			return false;
		}
		int favoriteCardBack;
		if (!int.TryParse(args[0].ToLowerInvariant(), out favoriteCardBack))
		{
			return false;
		}
		Network.Get().SetFavoriteCardBack(favoriteCardBack);
		return true;
	}

	// Token: 0x0600768D RID: 30349 RVA: 0x00262A50 File Offset: 0x00260C50
	private bool OnProcessCheat_disconnect(string func, string[] args, string rawArgs)
	{
		if (args != null && args.Length >= 1 && args[0] == "bnet")
		{
			if (Network.BattleNetStatus() != Network.BnetLoginState.BATTLE_NET_LOGGED_IN)
			{
				UIStatus.Get().AddError("Not connected to Battle.net, status=" + Network.BattleNetStatus(), -1f);
				return true;
			}
			BattleNet.RequestCloseAurora();
			UIStatus.Get().AddInfo("Disconnecting from Battle.net.");
			return true;
		}
		else
		{
			if (!Network.Get().IsConnectedToGameServer())
			{
				UIStatus.Get().AddError("Not connected to game server.", -1f);
				return true;
			}
			if (args != null && args.Length >= 1 && args[0] == "pong")
			{
				UIStatus.Get().AddInfo("Pong responses now being ignored.");
				Network.Get().SetShouldIgnorePong(true);
				return true;
			}
			bool flag = args != null && args.Length >= 1 && args[0] == "internet";
			NetworkReachabilityManager networkReachabilityManager = HearthstoneServices.Get<NetworkReachabilityManager>();
			if (flag)
			{
				if (networkReachabilityManager != null)
				{
					networkReachabilityManager.SetForceUnreachable(!networkReachabilityManager.GetForceUnreachable());
				}
				UIStatus.Get().AddInfo(networkReachabilityManager.GetForceUnreachable() ? "Forcing unreachable network." : "Network reachable.");
				return true;
			}
			if (args != null && args.Length >= 2 && args[0] == "duration")
			{
				int num = int.Parse(args[1]);
				if (networkReachabilityManager != null)
				{
					networkReachabilityManager.SetForceUnreachable(true);
				}
				Network.Get().SetSpoofDisconnected(true);
				Network.Get().OverrideKeepAliveSeconds(5U);
				UIStatus.Get().AddInfo(string.Format("All network disconnected for {0} seconds", num));
				HearthstoneApplication.Get().StartCoroutine(this.EnableNetworkAfterDelay(num));
				return true;
			}
			bool flag2 = args == null || args.Length == 0 || args[0] != "force";
			global::Log.LoadingScreen.Print("Cheats.OnProcessCheat_disconnect() - reconnect=true", Array.Empty<object>());
			if (flag2)
			{
				Network.Get().DisconnectFromGameServer();
			}
			else
			{
				Network.Get().SimulateUncleanDisconnectFromGameServer();
			}
			return true;
		}
	}

	// Token: 0x0600768E RID: 30350 RVA: 0x00262C1D File Offset: 0x00260E1D
	private IEnumerator EnableNetworkAfterDelay(int delay)
	{
		yield return new WaitForSeconds((float)delay);
		NetworkReachabilityManager networkReachabilityManager = HearthstoneServices.Get<NetworkReachabilityManager>();
		if (networkReachabilityManager != null)
		{
			networkReachabilityManager.SetForceUnreachable(false);
		}
		Network.Get().SetSpoofDisconnected(false);
		Network.Get().OverrideKeepAliveSeconds(0U);
		yield break;
	}

	// Token: 0x0600768F RID: 30351 RVA: 0x00262C2C File Offset: 0x00260E2C
	private bool OnProcessCheat_restart(string func, string[] args, string rawArgs)
	{
		if (!Network.Get().IsConnectedToGameServer())
		{
			UIStatus.Get().AddError("Not connected to game server.", -1f);
			return true;
		}
		if (!GameUtils.CanRestartCurrentMission(false))
		{
			UIStatus.Get().AddError("This game cannot be restarted.", -1f);
			return true;
		}
		GameState.Get().Restart();
		return true;
	}

	// Token: 0x06007690 RID: 30352 RVA: 0x00262C84 File Offset: 0x00260E84
	private bool OnProcessCheat_warning(string func, string[] args, string rawArgs)
	{
		string header;
		string message;
		this.ParseErrorText(args, rawArgs, out header, out message);
		global::Error.AddWarning(header, message, Array.Empty<object>());
		return true;
	}

	// Token: 0x06007691 RID: 30353 RVA: 0x00262CAA File Offset: 0x00260EAA
	private bool OnProcessCheat_fatal(string func, string[] args, string rawArgs)
	{
		global::Error.AddFatal(FatalErrorReason.CHEAT, rawArgs, Array.Empty<object>());
		return true;
	}

	// Token: 0x06007692 RID: 30354 RVA: 0x00262CBA File Offset: 0x00260EBA
	private bool OnProcessCheat_exit(string func, string[] args, string rawArgs)
	{
		GeneralUtils.ExitApplication();
		return true;
	}

	// Token: 0x06007693 RID: 30355 RVA: 0x00262CC4 File Offset: 0x00260EC4
	private bool OnProcessCheat_log(string func, string[] args, string rawArgs)
	{
		string message = "unknown log command, please use 'log help'";
		float delay = 5f;
		string a = args[0].ToLowerInvariant();
		string a2 = (args.Length >= 2) ? args[1] : string.Empty;
		if (a == "help")
		{
			message = "available log commands: load reload line";
			if (a2 == "load" || a2 == "reload")
			{
				message = "reloads the log.config";
			}
			else if (a2 == "line")
			{
				message = "prints a simple long line to log, useful for debugging\nto visually differentiate between test results.\nyou can specify a parameter like\n'log warn' to call Debug.LogWarning. you can\nalso add a note/context to your line\nby adding words afterwards, like 'log test 2 start'\nor 'log error (test 3 starting)'.";
				delay = 10f;
			}
		}
		else if (a == "load" || a == "reload")
		{
			global::Log.Get().Load();
		}
		else if (a == "line")
		{
			Cheats.LogFormatFunc logFormatFunc = new Cheats.LogFormatFunc(Debug.LogFormat);
			string text = string.Empty;
			int num = 1;
			if (a2 == "warn" || a2 == "warning")
			{
				logFormatFunc = new Cheats.LogFormatFunc(Debug.LogWarningFormat);
				num++;
			}
			else if (a2 == "err" || a2 == "error")
			{
				logFormatFunc = new Cheats.LogFormatFunc(Debug.LogErrorFormat);
				num++;
			}
			text = string.Join(" ", args.Skip(num).ToArray<string>());
			if (text.Length > 0)
			{
				text = string.Format(" {0} ", text);
			}
			logFormatFunc("====={0}{1}", new object[]
			{
				text,
				new string('=', Mathf.Max(5, 75 - text.Length))
			});
			message = "printed line to " + logFormatFunc.Method.Name;
			delay = 2f;
		}
		UIStatus.Get().AddInfo(message, delay);
		return true;
	}

	// Token: 0x06007694 RID: 30356 RVA: 0x00262E8C File Offset: 0x0026108C
	private bool OnProcessCheat_alert(string func, string[] args, string rawArgs)
	{
		AlertPopup.PopupInfo info = this.GenerateAlertInfo(rawArgs);
		if (this.m_alert == null)
		{
			DialogManager.Get().ShowPopup(info, new DialogManager.DialogProcessCallback(this.OnAlertProcessed));
		}
		else
		{
			this.m_alert.UpdateInfo(info);
		}
		return true;
	}

	// Token: 0x06007695 RID: 30357 RVA: 0x00262ED8 File Offset: 0x002610D8
	private bool OnProcessCheat_rankedIntroPopup(string func, string[] args, string rawArgs)
	{
		DialogManager.Get().ShowRankedIntroPopUp(null);
		MedalInfoTranslator localPlayerMedalInfo = RankMgr.Get().GetLocalPlayerMedalInfo();
		DialogManager.Get().ShowBonusStarsPopup(localPlayerMedalInfo.CreateDataModel(FormatType.FT_STANDARD, RankedMedal.DisplayMode.Default, false, false, null), null);
		return true;
	}

	// Token: 0x06007696 RID: 30358 RVA: 0x00262F14 File Offset: 0x00261114
	private bool OnProcessCheat_setRotationRotatedBoostersPopup(string func, string[] args, string rawArgs)
	{
		SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo info = new SetRotationRotatedBoostersPopup.SetRotationRotatedBoostersPopupInfo();
		DialogManager.Get().ShowSetRotationTutorialPopup(UserAttentionBlocker.SET_ROTATION_INTRO, info);
		return true;
	}

	// Token: 0x06007697 RID: 30359 RVA: 0x00262F34 File Offset: 0x00261134
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
			string a = args[1].ToLower();
			if (a == "1" || a == "wild")
			{
				formatType = FormatType.FT_WILD;
			}
			else if (a == "2" || a == "standard")
			{
				formatType = FormatType.FT_STANDARD;
			}
			else
			{
				if (!(a == "3") && !(a == "classic"))
				{
					UIStatus.Get().AddInfo("please enter a valid value for 2nd parameter <format type>");
					return true;
				}
				formatType = FormatType.FT_CLASSIC;
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
		List<List<RewardData>> list = new List<List<RewardData>>();
		if (!medalInfoTranslator.GetRankedRewardsEarned(formatType, ref list))
		{
			return false;
		}
		foreach (List<RewardData> collection in list)
		{
			seasonEndInfo.m_rankedRewards.AddRange(collection);
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

	// Token: 0x06007698 RID: 30360 RVA: 0x002631A8 File Offset: 0x002613A8
	private bool OnProcessCheat_playnullsound(string func, string[] args, string rawArgs)
	{
		SoundManager.Get().Play(null, null, null, null);
		return true;
	}

	// Token: 0x1400007A RID: 122
	// (add) Token: 0x06007699 RID: 30361 RVA: 0x002631BC File Offset: 0x002613BC
	// (remove) Token: 0x0600769A RID: 30362 RVA: 0x002631F0 File Offset: 0x002613F0
	public static event Action<string[]> PlayAudioByName;

	// Token: 0x0600769B RID: 30363 RVA: 0x00263223 File Offset: 0x00261423
	private bool OnProcessCheat_playaudio(string func, string[] args, string rawArgs)
	{
		if (Cheats.PlayAudioByName != null)
		{
			Cheats.PlayAudioByName(args);
		}
		return true;
	}

	// Token: 0x0600769C RID: 30364 RVA: 0x00263238 File Offset: 0x00261438
	private bool OnProcessCheat_spectate(string func, string[] args, string rawArgs)
	{
		if (args.Length >= 1 && args[0] == "waiting")
		{
			SpectatorManager.Get().ShowWaitingForNextGameDialog();
			return true;
		}
		if (args.Length >= 4)
		{
			if (!args.Any((string a) => string.IsNullOrEmpty(a)))
			{
				JoinInfo joinInfo = new JoinInfo();
				joinInfo.ServerIpAddress = args[0];
				joinInfo.SecretKey = args[3];
				uint serverPort;
				if (!uint.TryParse(args[1], out serverPort))
				{
					global::Error.AddWarning("Spectate Cheat Error", "error parsing the port # (uint) argument: " + args[1], Array.Empty<object>());
					return false;
				}
				joinInfo.ServerPort = serverPort;
				int num;
				if (!int.TryParse(args[2], out num))
				{
					global::Error.AddWarning("Spectate Cheat Error", "error parsing the game_handle (int) argument: " + args[2], Array.Empty<object>());
					return false;
				}
				joinInfo.GameHandle = num;
				joinInfo.GameType = GameType.GT_UNKNOWN;
				joinInfo.MissionId = 2;
				if (args.Length >= 5 && int.TryParse(args[4], out num))
				{
					joinInfo.GameType = (GameType)num;
				}
				if (args.Length >= 6 && int.TryParse(args[5], out num))
				{
					joinInfo.MissionId = num;
				}
				GameMgr.Get().SpectateGame(joinInfo);
				return true;
			}
		}
		global::Error.AddWarning("Spectate Cheat Error", "spectate cheat must have the following args:\n\nspectate ipaddress port game_handle spectator_password [gameType] [missionId]", Array.Empty<object>());
		return false;
	}

	// Token: 0x0600769D RID: 30365 RVA: 0x00263370 File Offset: 0x00261570
	private static void SubscribePartyEvents()
	{
		if (Cheats.s_hasSubscribedToPartyEvents)
		{
			return;
		}
		BnetParty.OnError += delegate(PartyError error)
		{
			global::Log.Party.Print("{0} code={1} feature={2} party={3} str={4}", new object[]
			{
				error.DebugContext,
				error.ErrorCode,
				error.FeatureEvent.ToString(),
				new PartyInfo(error.PartyId, error.PartyType),
				error.StringData
			});
		};
		BnetParty.OnJoined += delegate(OnlineEventType e, PartyInfo party, LeaveReason? reason)
		{
			global::Log.Party.Print("Party.OnJoined {0} party={1} reason={2}", new object[]
			{
				e,
				party,
				(reason != null) ? reason.Value.ToString() : "null"
			});
		};
		BnetParty.OnPrivacyLevelChanged += delegate(PartyInfo party, PrivacyLevel privacy)
		{
			global::Log.Party.Print("Party.OnPrivacyLevelChanged party={0} privacy={1}", new object[]
			{
				party,
				privacy
			});
		};
		BnetParty.OnMemberEvent += delegate(OnlineEventType e, PartyInfo party, BnetGameAccountId memberId, bool isRolesUpdate, LeaveReason? reason)
		{
			global::Log.Party.Print("Party.OnMemberEvent {0} party={1} memberId={2} isRolesUpdate={3} reason={4}", new object[]
			{
				e,
				party,
				memberId,
				isRolesUpdate,
				(reason != null) ? reason.Value.ToString() : "null"
			});
		};
		BnetParty.OnReceivedInvite += delegate(OnlineEventType e, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, InviteRemoveReason? reason)
		{
			global::Log.Party.Print("Party.OnReceivedInvite {0} party={1} inviteId={2} reason={3}", new object[]
			{
				e,
				party,
				inviteId,
				(reason != null) ? reason.Value.ToString() : "null"
			});
		};
		BnetParty.OnSentInvite += delegate(OnlineEventType e, PartyInfo party, ulong inviteId, BnetGameAccountId inviter, BnetGameAccountId invitee, bool senderIsMyself, InviteRemoveReason? reason)
		{
			PartyInvite sentInvite = BnetParty.GetSentInvite(party.Id, inviteId);
			global::Log.Party.Print("Party.OnSentInvite {0} party={1} inviteId={2} senderIsMyself={3} isRejoin={4} reason={5}", new object[]
			{
				e,
				party,
				inviteId,
				senderIsMyself,
				(sentInvite == null) ? "null" : sentInvite.IsRejoin.ToString(),
				(reason != null) ? reason.Value.ToString() : "null"
			});
		};
		BnetParty.OnReceivedInviteRequest += delegate(OnlineEventType e, PartyInfo party, InviteRequest request, InviteRequestRemovedReason? reason)
		{
			global::Log.Party.Print("Party.OnReceivedInviteRequest {0} party={1} target={2} {3} requester={4} {5} reason={6}", new object[]
			{
				e,
				party,
				request.TargetName,
				request.TargetId,
				request.RequesterName,
				request.RequesterId,
				(reason != null) ? reason.Value.ToString() : "null"
			});
		};
		BnetParty.OnChatMessage += delegate(PartyInfo party, BnetGameAccountId speakerId, string msg)
		{
			global::Log.Party.Print("Party.OnChatMessage party={0} speakerId={1} msg={2}", new object[]
			{
				party,
				speakerId,
				msg
			});
		};
		BnetParty.OnPartyAttributeChanged += delegate(PartyInfo party, string key, Variant attrVal)
		{
			string text = "null";
			if (attrVal.HasIntValue)
			{
				text = "[long]" + attrVal.IntValue.ToString();
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
			global::Log.Party.Print("BnetParty.OnPartyAttributeChanged party={0} key={1} value={2}", new object[]
			{
				party,
				key,
				text
			});
		};
		BnetParty.OnMemberAttributeChanged += delegate(PartyInfo party, BnetGameAccountId partyMember, string key, Variant attrVal)
		{
			string text = "null";
			if (attrVal.HasIntValue)
			{
				text = "[long]" + attrVal.IntValue.ToString();
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
			global::Log.Party.Print("BnetParty.OnMemberAttributeChanged party={0} member={1} key={2} value={3}", new object[]
			{
				party,
				partyMember,
				key,
				text
			});
		};
		Cheats.s_hasSubscribedToPartyEvents = true;
	}

	// Token: 0x0600769E RID: 30366 RVA: 0x002634F4 File Offset: 0x002616F4
	private static PartyId ParsePartyId(string cmd, string arg, int argIndex, ref string errorMsg)
	{
		PartyId partyId = null;
		ulong low;
		if (ulong.TryParse(arg, out low))
		{
			PartyId[] joinedPartyIds = BnetParty.GetJoinedPartyIds();
			if (low >= 0UL && joinedPartyIds.Length != 0 && low < (ulong)((long)joinedPartyIds.Length))
			{
				partyId = joinedPartyIds[(int)(checked((IntPtr)low))];
			}
			else
			{
				partyId = joinedPartyIds.FirstOrDefault((PartyId p) => p.Lo == low);
			}
			if (partyId == null)
			{
				errorMsg = string.Concat(new object[]
				{
					"party ",
					cmd,
					": couldn't find party at index, or with PartyId low bits: ",
					low
				});
			}
		}
		else
		{
			PartyType type;
			if (!global::EnumUtils.TryGetEnum<PartyType>(arg, out type))
			{
				errorMsg = string.Concat(new string[]
				{
					"party ",
					cmd,
					": unable to parse party (index or LowBits or type)",
					(argIndex >= 0) ? (" at arg index=" + argIndex) : "",
					" (",
					arg,
					"), please specify the Low bits of a PartyId or a PartyType."
				});
			}
			else
			{
				partyId = (from info in BnetParty.GetJoinedParties()
				where info.Type == type
				select info.Id).FirstOrDefault<PartyId>();
				if (partyId == null)
				{
					errorMsg = "party " + cmd + ": no joined party with PartyType: " + arg;
				}
			}
		}
		return partyId;
	}

	// Token: 0x0600769F RID: 30367 RVA: 0x0026365C File Offset: 0x0026185C
	private bool OnProcessCheat_party(string func, string[] args, string rawArgs)
	{
		if (args.Length >= 1)
		{
			if (!args.Any((string a) => string.IsNullOrEmpty(a)))
			{
				string cmd = args[0];
				if (cmd == "unsubscribe")
				{
					BnetParty.RemoveFromAllEventHandlers(this);
					Cheats.s_hasSubscribedToPartyEvents = false;
					global::Log.Party.Print("party {0}: unsubscribed.", new object[]
					{
						cmd
					});
					return true;
				}
				bool result = true;
				string[] array = args.Skip(1).ToArray<string>();
				string text = null;
				Cheats.SubscribePartyEvents();
				string cmd2 = cmd;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(cmd2);
				if (num <= 2631840913U)
				{
					if (num <= 392090781U)
					{
						if (num <= 136609321U)
						{
							if (num != 103796046U)
							{
								if (num != 136609321U)
								{
									goto IL_1F23;
								}
								if (!(cmd2 == "accept"))
								{
									goto IL_1F23;
								}
								goto IL_A89;
							}
							else
							{
								if (!(cmd2 == "setleader"))
								{
									goto IL_1F23;
								}
								IEnumerable<PartyId> enumerable = null;
								int num2 = -1;
								if (array.Length >= 2 && (!int.TryParse(array[1], out num2) || num2 < 0))
								{
									text = string.Format("party {0}: invalid memberIndex={1}", cmd, array[1]);
								}
								if (array.Length == 0)
								{
									global::Log.Party.Print("NOTE: party {0} without any arguments will {0} to first member in all parties.", new object[]
									{
										cmd
									});
									PartyId[] joinedPartyIds = BnetParty.GetJoinedPartyIds();
									if (joinedPartyIds.Length == 0)
									{
										global::Log.Party.Print("party {0}: no joined parties.", new object[]
										{
											cmd
										});
									}
									else
									{
										enumerable = joinedPartyIds;
									}
								}
								else
								{
									PartyId partyId17 = Cheats.ParsePartyId(cmd, array[0], -1, ref text);
									if (partyId17 != null)
									{
										enumerable = new PartyId[]
										{
											partyId17
										};
									}
								}
								if (enumerable != null)
								{
									using (IEnumerator<PartyId> enumerator = enumerable.GetEnumerator())
									{
										while (enumerator.MoveNext())
										{
											PartyId partyId2 = enumerator.Current;
											PartyMember[] members = BnetParty.GetMembers(partyId2);
											if (num2 >= 0)
											{
												if (num2 >= members.Length)
												{
													global::Log.Party.Print("party {0}: party={1} has no member at index={2}", new object[]
													{
														cmd,
														BnetParty.GetJoinedParty(partyId2),
														num2
													});
												}
												else
												{
													PartyMember partyMember = members[num2];
													BnetParty.SetLeader(partyId2, partyMember.GameAccountId);
												}
											}
											else if (members.Any((PartyMember m) => m.GameAccountId != BnetPresenceMgr.Get().GetMyGameAccountId()))
											{
												BnetParty.SetLeader(partyId2, members.First((PartyMember m) => m.GameAccountId != BnetPresenceMgr.Get().GetMyGameAccountId()).GameAccountId);
											}
											else
											{
												global::Log.Party.Print("party {0}: party={1} has no member not myself to set as leader.", new object[]
												{
													cmd,
													BnetParty.GetJoinedParty(partyId2)
												});
											}
										}
										goto IL_1F34;
									}
									goto IL_1407;
								}
								goto IL_1F34;
							}
						}
						else if (num != 217798785U)
						{
							if (num != 327549200U)
							{
								if (num != 392090781U)
								{
									goto IL_1F23;
								}
								if (!(cmd2 == "setlong"))
								{
									goto IL_1F23;
								}
								goto IL_186A;
							}
							else
							{
								if (!(cmd2 == "invite"))
								{
									goto IL_1F23;
								}
								PartyId partyId3 = null;
								int count = 1;
								if (array.Length == 0)
								{
									PartyId[] joinedPartyIds2 = BnetParty.GetJoinedPartyIds();
									if (joinedPartyIds2.Length != 0)
									{
										partyId3 = joinedPartyIds2[0];
										count = 0;
									}
									else
									{
										text = "party invite: no joined parties to invite to.";
									}
								}
								else
								{
									partyId3 = Cheats.ParsePartyId(cmd, array[0], -1, ref text);
								}
								if (partyId3 != null)
								{
									string[] array2 = array.Skip(count).ToArray<string>();
									HashSet<BnetPlayer> hashSet = new HashSet<BnetPlayer>();
									IEnumerable<BnetPlayer> source = from p in BnetFriendMgr.Get().GetFriends()
									where p.IsOnline() && p.GetHearthstoneGameAccount() != null
									select p;
									if (array2.Length == 0)
									{
										global::Log.Party.Print("NOTE: party invite without any arguments will pick the first online friend.", Array.Empty<object>());
										BnetPlayer bnetPlayer = source.FirstOrDefault<BnetPlayer>();
										if (bnetPlayer == null)
										{
											text = "party invite: no online Hearthstone friend found.";
										}
										else
										{
											hashSet.Add(bnetPlayer);
										}
									}
									else
									{
										for (int i2 = 0; i2 < array2.Length; i2++)
										{
											string arg = array2[i2];
											int num3;
											if (int.TryParse(arg, out num3))
											{
												BnetPlayer bnetPlayer2 = source.ElementAtOrDefault(num3);
												if (bnetPlayer2 == null)
												{
													text = "party invite: no online Hearthstone friend index " + num3;
												}
												else
												{
													hashSet.Add(bnetPlayer2);
												}
											}
											else
											{
												IEnumerable<BnetPlayer> enumerable2 = from p in source
												where p.GetBattleTag().ToString().Contains(arg, StringComparison.OrdinalIgnoreCase) || (p.GetFullName() != null && p.GetFullName().Contains(arg, StringComparison.OrdinalIgnoreCase))
												select p;
												if (!enumerable2.Any<BnetPlayer>())
												{
													text = string.Concat(new object[]
													{
														"party invite: no online Hearthstone friend matching name ",
														arg,
														" (arg index ",
														i2,
														")"
													});
												}
												else
												{
													foreach (BnetPlayer item in enumerable2)
													{
														if (!hashSet.Contains(item))
														{
															hashSet.Add(item);
															break;
														}
													}
												}
											}
										}
									}
									using (HashSet<BnetPlayer>.Enumerator enumerator3 = hashSet.GetEnumerator())
									{
										while (enumerator3.MoveNext())
										{
											BnetPlayer bnetPlayer3 = enumerator3.Current;
											BnetGameAccountId hearthstoneGameAccountId = bnetPlayer3.GetHearthstoneGameAccountId();
											if (BnetParty.IsMember(partyId3, hearthstoneGameAccountId))
											{
												global::Log.Party.Print("party invite: already a party member of {0}: {1}", new object[]
												{
													bnetPlayer3,
													BnetParty.GetJoinedParty(partyId3)
												});
											}
											else
											{
												global::Log.Party.Print("party invite: inviting {0} {1} to party {2}", new object[]
												{
													hearthstoneGameAccountId,
													bnetPlayer3,
													BnetParty.GetJoinedParty(partyId3)
												});
												BnetParty.SendInvite(partyId3, hearthstoneGameAccountId, true);
											}
										}
										goto IL_1F34;
									}
									goto IL_A89;
								}
								goto IL_1F34;
							}
						}
						else
						{
							if (!(cmd2 == "list"))
							{
								goto IL_1F23;
							}
							goto IL_1AEA;
						}
					}
					else if (num <= 649812317U)
					{
						if (num != 561175238U)
						{
							if (num != 649812317U)
							{
								goto IL_1F23;
							}
							if (!(cmd2 == "create"))
							{
								goto IL_1F23;
							}
							if (array.Length < 1)
							{
								text = "party create: requires a PartyType: " + string.Join(" | ", (from PartyType v in Enum.GetValues(typeof(PartyType))
								select string.Concat(new object[]
								{
									v,
									" (",
									(int)v,
									")"
								})).ToArray<string>());
								goto IL_1F34;
							}
							int num4;
							PartyType partyType;
							if (int.TryParse(array[0], out num4))
							{
								partyType = (PartyType)num4;
							}
							else if (!global::EnumUtils.TryGetEnum<PartyType>(array[0], out partyType))
							{
								text = "party create: unknown PartyType specified: " + array[0];
							}
							if (text == null)
							{
								byte[] creatorBlob = ProtobufUtil.ToByteArray(BnetUtils.CreatePegasusBnetId(BnetPresenceMgr.Get().GetMyGameAccountId()));
								BnetParty.CreateParty(partyType, PrivacyLevel.OPEN_INVITATION, creatorBlob, delegate(PartyType t, PartyId partyId)
								{
									global::Log.Party.Print("BnetParty.CreateSuccessCallback type={0} partyId={1}", new object[]
									{
										t,
										partyId
									});
								});
								goto IL_1F34;
							}
							goto IL_1F34;
						}
						else
						{
							if (!(cmd2 == "setstring"))
							{
								goto IL_1F23;
							}
							goto IL_186A;
						}
					}
					else if (num != 1169780829U)
					{
						if (num != 1416872128U)
						{
							if (num != 2631840913U)
							{
								goto IL_1F23;
							}
							if (!(cmd2 == "clearattr"))
							{
								goto IL_1F23;
							}
							PartyId partyId4 = null;
							if (array.Length < 2)
							{
								text = "party " + cmd + ": must specify attributeKey.";
							}
							else
							{
								partyId4 = Cheats.ParsePartyId(cmd, array[0], -1, ref text);
								if (partyId4 == null)
								{
									PartyId[] joinedPartyIds3 = BnetParty.GetJoinedPartyIds();
									if (joinedPartyIds3.Length != 0)
									{
										global::Log.Party.Print("party {0}: treating first argument as attributeKey (and not PartyId) - will use PartyId at index 0", new object[]
										{
											cmd
										});
										text = null;
										partyId4 = joinedPartyIds3[0];
									}
								}
								else
								{
									global::Log.Party.Print("party {0}: treating first argument as PartyId (second argument will be attributeKey)", new object[]
									{
										cmd
									});
								}
							}
							if (partyId4 != null)
							{
								string text2 = array[1];
								BnetParty.ClearPartyAttribute(partyId4, text2);
								global::Log.Party.Print("party {0}: cleared key={1} party={2}", new object[]
								{
									cmd,
									text2,
									BnetParty.GetJoinedParty(partyId4)
								});
								goto IL_1F34;
							}
							goto IL_1F34;
						}
						else if (!(cmd2 == "leave"))
						{
							goto IL_1F23;
						}
					}
					else
					{
						if (!(cmd2 == "setprivacy"))
						{
							goto IL_1F23;
						}
						PartyId partyId5 = null;
						if (array.Length < 2)
						{
							text = "party setprivacy: must specify a party (index or LowBits or type) and a PrivacyLevel: " + string.Join(" | ", (from PrivacyLevel v in Enum.GetValues(typeof(PrivacyLevel))
							select string.Concat(new object[]
							{
								v,
								" (",
								(int)v,
								")"
							})).ToArray<string>());
						}
						else
						{
							partyId5 = Cheats.ParsePartyId(cmd, array[0], -1, ref text);
						}
						if (!(partyId5 != null))
						{
							goto IL_1F34;
						}
						PrivacyLevel? privacyLevel = null;
						int value;
						PrivacyLevel value2;
						if (int.TryParse(array[1], out value))
						{
							privacyLevel = new PrivacyLevel?((PrivacyLevel)value);
						}
						else if (!global::EnumUtils.TryGetEnum<PrivacyLevel>(array[1], out value2))
						{
							text = "party setprivacy: unknown PrivacyLevel specified: " + array[1];
						}
						else
						{
							privacyLevel = new PrivacyLevel?(value2);
						}
						if (privacyLevel != null)
						{
							global::Log.Party.Print("party setprivacy: setting PrivacyLevel={0} for party {1}.", new object[]
							{
								privacyLevel.Value,
								BnetParty.GetJoinedParty(partyId5)
							});
							BnetParty.SetPrivacy(partyId5, privacyLevel.Value);
							goto IL_1F34;
						}
						goto IL_1F34;
					}
				}
				else if (num <= 3323728671U)
				{
					if (num <= 2722888107U)
					{
						if (num != 2719933859U)
						{
							if (num != 2722888107U)
							{
								goto IL_1F23;
							}
							if (!(cmd2 == "chat"))
							{
								goto IL_1F23;
							}
							PartyId[] joinedPartyIds4 = BnetParty.GetJoinedPartyIds();
							if (array.Length < 1)
							{
								text = "party chat: must specify 1-2 arguments: party (index or LowBits or type) or a message to send.";
								goto IL_1F34;
							}
							int count2 = 1;
							PartyId partyId6 = Cheats.ParsePartyId(cmd, array[0], -1, ref text);
							if (partyId6 == null && joinedPartyIds4.Length != 0)
							{
								text = null;
								partyId6 = joinedPartyIds4[0];
								count2 = 0;
							}
							if (partyId6 != null)
							{
								BnetParty.SendChatMessage(partyId6, string.Join(" ", array.Skip(count2).ToArray<string>()));
								goto IL_1F34;
							}
							goto IL_1F34;
						}
						else
						{
							if (!(cmd2 == "requestinvite"))
							{
								goto IL_1F23;
							}
							if (array.Length < 2)
							{
								text = "party " + cmd + ": must specify a partyId (Hi-Lo format) and an online friend index";
								goto IL_1F34;
							}
							PartyType partyType2 = PartyType.DEFAULT;
							foreach (string text3 in array)
							{
								int num5 = text3.IndexOf('-');
								int num6 = -1;
								PartyId partyId7 = null;
								BnetGameAccountId bnetGameAccountId = null;
								if (num5 >= 0)
								{
									string s = text3.Substring(0, num5);
									string s2 = (text3.Length > num5) ? text3.Substring(num5 + 1) : "";
									ulong highBits;
									ulong lowBits;
									if (ulong.TryParse(s, out highBits) && ulong.TryParse(s2, out lowBits))
									{
										partyId7 = new PartyId(highBits, lowBits);
									}
									else
									{
										text = "party " + cmd + ": unable to parse partyId (in format Hi-Lo).";
									}
								}
								else if (int.TryParse(text3, out num6))
								{
									BnetPlayer[] array4 = (from p in BnetFriendMgr.Get().GetFriends()
									where p.IsOnline() && p.GetHearthstoneGameAccount() != null
									select p).ToArray<BnetPlayer>();
									if (num6 < 0 || num6 >= array4.Length)
									{
										text = string.Concat(new object[]
										{
											"party ",
											cmd,
											": no online friend at index ",
											num6
										});
									}
									else
									{
										bnetGameAccountId = array4[num6].GetHearthstoneGameAccountId();
									}
								}
								else
								{
									text = "party " + cmd + ": unable to parse online friend index.";
								}
								if (partyId7 != null && bnetGameAccountId != null)
								{
									BnetParty.RequestInvite(partyId7, bnetGameAccountId, BnetPresenceMgr.Get().GetMyGameAccountId(), partyType2);
								}
							}
							goto IL_1F34;
						}
					}
					else if (num != 2760744108U)
					{
						if (num != 2946386435U)
						{
							if (num != 3323728671U)
							{
								goto IL_1F23;
							}
							if (!(cmd2 == "kick"))
							{
								goto IL_1F23;
							}
							goto IL_1407;
						}
						else
						{
							if (!(cmd2 == "subscribe"))
							{
								goto IL_1F23;
							}
							goto IL_1AEA;
						}
					}
					else
					{
						if (!(cmd2 == "setblob"))
						{
							goto IL_1F23;
						}
						goto IL_186A;
					}
				}
				else if (num <= 3462459964U)
				{
					if (num != 3374496889U)
					{
						if (num != 3462459964U)
						{
							goto IL_1F23;
						}
						if (!(cmd2 == "ignorerequest"))
						{
							goto IL_1F23;
						}
						PartyId[] joinedPartyIds5 = BnetParty.GetJoinedPartyIds();
						if (joinedPartyIds5.Length == 0)
						{
							global::Log.Party.Print("party {0}: no joined parties.", new object[]
							{
								cmd
							});
							goto IL_1F34;
						}
						foreach (PartyId partyId8 in joinedPartyIds5)
						{
							foreach (InviteRequest inviteRequest in BnetParty.GetInviteRequests(partyId8))
							{
								global::Log.Party.Print("party {0}: ignoring request to invite {0} {1} from {2} {3}.", new object[]
								{
									inviteRequest.TargetName,
									inviteRequest.TargetId,
									inviteRequest.RequesterName,
									inviteRequest.RequesterId
								});
								BnetParty.IgnoreInviteRequest(partyId8, inviteRequest.TargetId);
							}
						}
						goto IL_1F34;
					}
					else
					{
						if (!(cmd2 == "join"))
						{
							goto IL_1F23;
						}
						if (array.Length < 1)
						{
							text = "party " + cmd + ": must specify an online friend index or a partyId (Hi-Lo format)";
							goto IL_1F34;
						}
						PartyType partyType3 = PartyType.DEFAULT;
						foreach (string text4 in array)
						{
							int num7 = text4.IndexOf('-');
							int num8 = -1;
							PartyId partyId9 = null;
							if (num7 >= 0)
							{
								string s3 = text4.Substring(0, num7);
								string s4 = (text4.Length > num7) ? text4.Substring(num7 + 1) : "";
								ulong highBits2;
								ulong lowBits2;
								if (ulong.TryParse(s3, out highBits2) && ulong.TryParse(s4, out lowBits2))
								{
									partyId9 = new PartyId(highBits2, lowBits2);
								}
								else
								{
									text = "party " + cmd + ": unable to parse partyId (in format Hi-Lo).";
								}
							}
							else if (int.TryParse(text4, out num8))
							{
								BnetPlayer[] array6 = (from p in BnetFriendMgr.Get().GetFriends()
								where p.IsOnline() && p.GetHearthstoneGameAccount() != null
								select p).ToArray<BnetPlayer>();
								if (num8 < 0 || num8 >= array6.Length)
								{
									text = string.Concat(new object[]
									{
										"party ",
										cmd,
										": no online friend at index ",
										num8
									});
								}
								else
								{
									text = "party " + cmd + ": Not-Yet-Implemented: find partyId from online friend's presence.";
								}
							}
							else
							{
								text = "party " + cmd + ": unable to parse online friend index.";
							}
							if (partyId9 != null)
							{
								BnetParty.JoinParty(partyId9, partyType3);
							}
						}
						goto IL_1F34;
					}
				}
				else if (num != 3517145194U)
				{
					if (num != 3528071833U)
					{
						if (num != 3691399731U)
						{
							goto IL_1F23;
						}
						if (!(cmd2 == "revoke"))
						{
							goto IL_1F23;
						}
						PartyId partyId10 = null;
						if (array.Length == 0)
						{
							global::Log.Party.Print("NOTE: party {0} without any arguments will {0} all sent invites for all parties.", new object[]
							{
								cmd
							});
							PartyId[] joinedPartyIds6 = BnetParty.GetJoinedPartyIds();
							if (joinedPartyIds6.Length == 0)
							{
								global::Log.Party.Print("party {0}: no joined parties.", new object[]
								{
									cmd
								});
							}
							foreach (PartyId partyId11 in joinedPartyIds6)
							{
								foreach (PartyInvite partyInvite in BnetParty.GetSentInvites(partyId11))
								{
									global::Log.Party.Print("party {0}: revoking inviteId={1} from {2} for party {3}.", new object[]
									{
										cmd,
										partyInvite.InviteId,
										partyInvite.InviterName,
										BnetParty.GetJoinedParty(partyId11)
									});
									BnetParty.RevokeSentInvite(partyId11, partyInvite.InviteId);
								}
							}
						}
						else
						{
							partyId10 = Cheats.ParsePartyId(cmd, array[0], -1, ref text);
						}
						if (!(partyId10 != null))
						{
							goto IL_1F34;
						}
						PartyInfo joinedParty = BnetParty.GetJoinedParty(partyId10);
						PartyInvite[] sentInvites = BnetParty.GetSentInvites(partyId10);
						if (sentInvites.Length == 0)
						{
							text = string.Concat(new object[]
							{
								"party ",
								cmd,
								": no sent invites for party ",
								joinedParty
							});
							goto IL_1F34;
						}
						string[] array8 = array.Skip(1).ToArray<string>();
						if (array8.Length == 0)
						{
							global::Log.Party.Print("NOTE: party {0} without specifying InviteId (or index) will {0} all sent invites.", new object[]
							{
								cmd
							});
							foreach (PartyInvite partyInvite2 in sentInvites)
							{
								global::Log.Party.Print("party {0}: revoking inviteId={1} from {2} for party {3}.", new object[]
								{
									cmd,
									partyInvite2.InviteId,
									partyInvite2.InviterName,
									joinedParty
								});
								BnetParty.RevokeSentInvite(partyId10, partyInvite2.InviteId);
							}
							goto IL_1F34;
						}
						for (int l = 0; l < array8.Length; l++)
						{
							ulong indexOrId;
							if (ulong.TryParse(array8[l], out indexOrId))
							{
								PartyInvite partyInvite3;
								if (indexOrId < (ulong)((long)sentInvites.Length))
								{
									partyInvite3 = sentInvites[(int)(checked((IntPtr)indexOrId))];
								}
								else
								{
									partyInvite3 = sentInvites.FirstOrDefault((PartyInvite inv) => inv.InviteId == indexOrId);
									if (partyInvite3 == null)
									{
										global::Log.Party.Print("party {0}: unable to find sent invite (id or index): {1} for party {2}", new object[]
										{
											cmd,
											array8[l],
											joinedParty
										});
									}
								}
								if (partyInvite3 != null)
								{
									global::Log.Party.Print("party {0}: revoking inviteId={1} from {2} for party {3}.", new object[]
									{
										cmd,
										partyInvite3.InviteId,
										partyInvite3.InviterName,
										joinedParty
									});
									BnetParty.RevokeSentInvite(partyId10, partyInvite3.InviteId);
								}
							}
							else
							{
								global::Log.Party.Print("party {0}: unable to parse invite (id or index): {1}", new object[]
								{
									cmd,
									array8[l]
								});
							}
						}
						goto IL_1F34;
					}
					else
					{
						if (!(cmd2 == "decline"))
						{
							goto IL_1F23;
						}
						goto IL_A89;
					}
				}
				else if (!(cmd2 == "dissolve"))
				{
					goto IL_1F23;
				}
				bool flag = cmd == "dissolve";
				if (array.Length == 0)
				{
					global::Log.Party.Print("NOTE: party {0} without any arguments will {0} all joined parties.", new object[]
					{
						cmd
					});
					PartyInfo[] joinedParties = BnetParty.GetJoinedParties();
					if (joinedParties.Length == 0)
					{
						global::Log.Party.Print("No joined parties.", Array.Empty<object>());
					}
					foreach (PartyInfo partyInfo in joinedParties)
					{
						global::Log.Party.Print("party {0}: {1} party {2}", new object[]
						{
							cmd,
							flag ? "dissolving" : "leaving",
							partyInfo
						});
						if (flag)
						{
							BnetParty.DissolveParty(partyInfo.Id);
						}
						else
						{
							BnetParty.Leave(partyInfo.Id);
						}
					}
					goto IL_1F34;
				}
				for (int m2 = 0; m2 < array.Length; m2++)
				{
					string arg2 = array[m2];
					string text5 = null;
					PartyId partyId12 = Cheats.ParsePartyId(cmd, arg2, m2, ref text5);
					if (text5 != null)
					{
						global::Log.Party.Print(text5, Array.Empty<object>());
					}
					if (partyId12 != null)
					{
						global::Log.Party.Print("party {0}: {1} party {2}", new object[]
						{
							cmd,
							flag ? "dissolving" : "leaving",
							BnetParty.GetJoinedParty(partyId12)
						});
						if (flag)
						{
							BnetParty.DissolveParty(partyId12);
						}
						else
						{
							BnetParty.Leave(partyId12);
						}
					}
				}
				goto IL_1F34;
				IL_A89:
				bool flag2 = cmd == "accept";
				PartyInvite[] receivedInvites = BnetParty.GetReceivedInvites();
				if (receivedInvites.Length == 0)
				{
					text = "party " + cmd + ": no received party invites.";
					goto IL_1F34;
				}
				if (array.Length == 0)
				{
					global::Log.Party.Print("NOTE: party {0} without any arguments will {0} all received invites.", new object[]
					{
						cmd
					});
					foreach (PartyInvite partyInvite4 in receivedInvites)
					{
						global::Log.Party.Print("party {0}: {1} inviteId={2} from {3} for party {4}.", new object[]
						{
							cmd,
							flag2 ? "accepting" : "declining",
							partyInvite4.InviteId,
							partyInvite4.InviterName,
							new PartyInfo(partyInvite4.PartyId, partyInvite4.PartyType)
						});
						if (flag2)
						{
							BnetParty.AcceptReceivedInvite(partyInvite4.InviteId);
						}
						else
						{
							BnetParty.DeclineReceivedInvite(partyInvite4.InviteId);
						}
					}
					goto IL_1F34;
				}
				for (int n = 0; n < array.Length; n++)
				{
					ulong indexOrId;
					if (ulong.TryParse(array[n], out indexOrId))
					{
						PartyInvite partyInvite5;
						if (indexOrId < (ulong)((long)receivedInvites.Length))
						{
							partyInvite5 = receivedInvites[(int)(checked((IntPtr)indexOrId))];
						}
						else
						{
							partyInvite5 = receivedInvites.FirstOrDefault((PartyInvite inv) => inv.InviteId == indexOrId);
							if (partyInvite5 == null)
							{
								global::Log.Party.Print("party {0}: unable to find received invite (id or index): {1}", new object[]
								{
									cmd,
									array[n]
								});
							}
						}
						if (partyInvite5 != null)
						{
							global::Log.Party.Print("party {0}: {1} inviteId={2} from {3} for party {4}.", new object[]
							{
								cmd,
								flag2 ? "accepting" : "declining",
								partyInvite5.InviteId,
								partyInvite5.InviterName,
								new PartyInfo(partyInvite5.PartyId, partyInvite5.PartyType)
							});
							if (flag2)
							{
								BnetParty.AcceptReceivedInvite(partyInvite5.InviteId);
							}
							else
							{
								BnetParty.DeclineReceivedInvite(partyInvite5.InviteId);
							}
						}
					}
					else
					{
						global::Log.Party.Print("party {0}: unable to parse invite (id or index): {1}", new object[]
						{
							cmd,
							array[n]
						});
					}
				}
				goto IL_1F34;
				IL_1407:
				PartyId partyId13 = null;
				if (array.Length == 0)
				{
					global::Log.Party.Print("NOTE: party {0} without any arguments will {0} all members for all parties (other than self).", new object[]
					{
						cmd
					});
					PartyId[] joinedPartyIds7 = BnetParty.GetJoinedPartyIds();
					if (joinedPartyIds7.Length == 0)
					{
						global::Log.Party.Print("party {0}: no joined parties.", new object[]
						{
							cmd
						});
					}
					foreach (PartyId partyId14 in joinedPartyIds7)
					{
						foreach (PartyMember partyMember2 in BnetParty.GetMembers(partyId14))
						{
							if (!(partyMember2.GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId()))
							{
								global::Log.Party.Print("party {0}: kicking memberId={1} from party {2}.", new object[]
								{
									cmd,
									partyMember2.GameAccountId,
									BnetParty.GetJoinedParty(partyId14)
								});
								BnetParty.KickMember(partyId14, partyMember2.GameAccountId);
							}
						}
					}
				}
				else
				{
					partyId13 = Cheats.ParsePartyId(cmd, array[0], -1, ref text);
				}
				if (!(partyId13 != null))
				{
					goto IL_1F34;
				}
				PartyInfo joinedParty2 = BnetParty.GetJoinedParty(partyId13);
				PartyMember[] members2 = BnetParty.GetMembers(partyId13);
				if (members2.Length == 1)
				{
					text = string.Concat(new object[]
					{
						"party ",
						cmd,
						": no members (other than self) for party ",
						joinedParty2
					});
					goto IL_1F34;
				}
				string[] array11 = array.Skip(1).ToArray<string>();
				if (array11.Length == 0)
				{
					global::Log.Party.Print("NOTE: party {0} without specifying member index will {0} all members (other than self).", new object[]
					{
						cmd
					});
					foreach (PartyMember partyMember3 in members2)
					{
						if (!(partyMember3.GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId()))
						{
							global::Log.Party.Print("party {0}: kicking memberId={1} from party {2}.", new object[]
							{
								cmd,
								partyMember3.GameAccountId,
								joinedParty2
							});
							BnetParty.KickMember(partyId13, partyMember3.GameAccountId);
						}
					}
					goto IL_1F34;
				}
				for (int num9 = 0; num9 < array11.Length; num9++)
				{
					ulong indexOrId;
					if (ulong.TryParse(array11[num9], out indexOrId))
					{
						PartyMember partyMember4;
						if (indexOrId < (ulong)((long)members2.Length))
						{
							partyMember4 = members2[(int)(checked((IntPtr)indexOrId))];
						}
						else
						{
							partyMember4 = members2.FirstOrDefault((PartyMember m) => m.GameAccountId.GetLo() == indexOrId);
							if (partyMember4 == null)
							{
								global::Log.Party.Print("party {0}: unable to find member (id or index): {1} for party {2}", new object[]
								{
									cmd,
									array11[num9],
									joinedParty2
								});
							}
						}
						if (partyMember4 != null)
						{
							if (partyMember4.GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId())
							{
								global::Log.Party.Print("party {0}: cannot kick yourself (argIndex={1}); party={2}", new object[]
								{
									cmd,
									num9,
									joinedParty2
								});
							}
							else
							{
								global::Log.Party.Print("party {0}: kicking memberId={1} from party {2}.", new object[]
								{
									cmd,
									partyMember4.GameAccountId,
									joinedParty2
								});
								BnetParty.KickMember(partyId13, partyMember4.GameAccountId);
							}
						}
					}
					else
					{
						global::Log.Party.Print("party {0}: unable to parse member (id or index): {1}", new object[]
						{
							cmd,
							array11[num9]
						});
					}
				}
				goto IL_1F34;
				IL_186A:
				bool flag3 = cmd == "setlong";
				bool flag4 = cmd == "setstring";
				bool flag5 = cmd == "setblob";
				int num10 = 1;
				PartyId partyId15 = null;
				if (array.Length < 2)
				{
					text = "party " + cmd + ": must specify attributeKey and a value.";
				}
				else
				{
					partyId15 = Cheats.ParsePartyId(cmd, array[0], -1, ref text);
					if (partyId15 == null)
					{
						PartyId[] joinedPartyIds8 = BnetParty.GetJoinedPartyIds();
						if (joinedPartyIds8.Length != 0)
						{
							global::Log.Party.Print("party {0}: treating first argument as attributeKey (and not PartyId) - will use PartyId at index 0", new object[]
							{
								cmd
							});
							text = null;
							partyId15 = joinedPartyIds8[0];
						}
					}
					else
					{
						global::Log.Party.Print("party {0}: treating first argument as PartyId (second argument will be attributeKey)", new object[]
						{
							cmd
						});
					}
				}
				if (!(partyId15 != null))
				{
					goto IL_1F34;
				}
				bool flag6 = false;
				string text6 = array[num10];
				string text7 = string.Join(" ", array.Skip(num10 + 1).ToArray<string>());
				if (flag3)
				{
					long value3;
					if (long.TryParse(text7, out value3))
					{
						BnetParty.SetPartyAttributeLong(partyId15, text6, value3);
						flag6 = true;
					}
				}
				else if (flag4)
				{
					BnetParty.SetPartyAttributeString(partyId15, text6, text7);
					flag6 = true;
				}
				else if (flag5)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(text7);
					BnetParty.SetPartyAttributeBlob(partyId15, text6, bytes);
					flag6 = true;
				}
				else
				{
					text = "party " + cmd + ": unhandled attribute type!";
				}
				if (flag6)
				{
					global::Log.Party.Print("party {0}: complete key={1} val={2} party={3}", new object[]
					{
						cmd,
						text6,
						text7,
						BnetParty.GetJoinedParty(partyId15)
					});
					goto IL_1F34;
				}
				goto IL_1F34;
				IL_1AEA:
				IEnumerable<PartyId> enumerable3 = null;
				if (array.Length == 0)
				{
					PartyInfo[] joinedParties2 = BnetParty.GetJoinedParties();
					if (joinedParties2.Length == 0)
					{
						global::Log.Party.Print("party list: no joined parties.", Array.Empty<object>());
					}
					else
					{
						global::Log.Party.Print("party list: listing all joined parties and the details of the party at index 0.", Array.Empty<object>());
						enumerable3 = new PartyId[]
						{
							joinedParties2[0].Id
						};
					}
					for (int num11 = 0; num11 < joinedParties2.Length; num11++)
					{
						global::Log.Party.Print("   {0}", new object[]
						{
							Cheats.GetPartySummary(joinedParties2[num11], num11)
						});
					}
				}
				else
				{
					enumerable3 = from p in array.Select(delegate(string a, int i)
					{
						string text9 = null;
						PartyId result2 = Cheats.ParsePartyId(cmd, a, i, ref text9);
						if (text9 != null)
						{
							global::Log.Party.Print(text9, Array.Empty<object>());
						}
						return result2;
					})
					where p != null
					select p;
				}
				if (enumerable3 != null)
				{
					int num12 = -1;
					foreach (PartyId partyId16 in enumerable3)
					{
						num12++;
						PartyInfo joinedParty3 = BnetParty.GetJoinedParty(partyId16);
						global::Log.Party.Print("party {0}: {1}", new object[]
						{
							cmd,
							Cheats.GetPartySummary(BnetParty.GetJoinedParty(partyId16), num12)
						});
						PartyMember[] members3 = BnetParty.GetMembers(partyId16);
						if (members3.Length == 0)
						{
							global::Log.Party.Print("   no members.", Array.Empty<object>());
						}
						else
						{
							global::Log.Party.Print("   members:", Array.Empty<object>());
						}
						for (int num13 = 0; num13 < members3.Length; num13++)
						{
							bool flag7 = members3[num13].GameAccountId == BnetPresenceMgr.Get().GetMyGameAccountId();
							global::Logger party = global::Log.Party;
							string format = "      [{0}] {1} isMyself={2} isLeader={3} roleIds={4}";
							object[] array12 = new object[5];
							array12[0] = num13;
							array12[1] = members3[num13].GameAccountId;
							array12[2] = flag7;
							array12[3] = members3[num13].IsLeader(joinedParty3.Type);
							array12[4] = string.Join(",", (from r in members3[num13].RoleIds
							select r.ToString()).ToArray<string>());
							party.Print(format, array12);
						}
						PartyInvite[] sentInvites2 = BnetParty.GetSentInvites(partyId16);
						if (sentInvites2.Length == 0)
						{
							global::Log.Party.Print("   no sent invites.", Array.Empty<object>());
						}
						else
						{
							global::Log.Party.Print("   sent invites:", Array.Empty<object>());
						}
						for (int num14 = 0; num14 < sentInvites2.Length; num14++)
						{
							PartyInvite invite = sentInvites2[num14];
							global::Log.Party.Print("      {0}", new object[]
							{
								Cheats.GetPartyInviteSummary(invite, num14)
							});
						}
						KeyValuePair<string, object>[] allPartyAttributes = BnetParty.GetAllPartyAttributes(partyId16);
						if (allPartyAttributes.Length == 0)
						{
							global::Log.Party.Print("   no party attributes.", Array.Empty<object>());
						}
						else
						{
							global::Log.Party.Print("   party attributes:", Array.Empty<object>());
						}
						foreach (KeyValuePair<string, object> keyValuePair in allPartyAttributes)
						{
							string text8 = (keyValuePair.Value == null) ? "<null>" : string.Format("[{0}]{1}", keyValuePair.Value.GetType().Name, keyValuePair.Value.ToString());
							if (keyValuePair.Value is byte[])
							{
								byte[] array13 = (byte[])keyValuePair.Value;
								text8 = "blobLength=" + array13.Length;
								try
								{
									string @string = Encoding.UTF8.GetString(array13);
									if (@string != null)
									{
										text8 = text8 + " decodedUtf8=" + @string;
									}
								}
								catch (ArgumentException)
								{
								}
							}
							global::Log.Party.Print("      {0}={1}", new object[]
							{
								keyValuePair.Key ?? "<null>",
								text8
							});
						}
					}
				}
				PartyInvite[] receivedInvites2 = BnetParty.GetReceivedInvites();
				if (receivedInvites2.Length == 0)
				{
					global::Log.Party.Print("party list: no received party invites.", Array.Empty<object>());
				}
				else
				{
					global::Log.Party.Print("party list: received party invites:", Array.Empty<object>());
				}
				for (int num16 = 0; num16 < receivedInvites2.Length; num16++)
				{
					PartyInvite invite2 = receivedInvites2[num16];
					global::Log.Party.Print("   {0}", new object[]
					{
						Cheats.GetPartyInviteSummary(invite2, num16)
					});
				}
				goto IL_1F34;
				IL_1F23:
				text = "party: unknown party cmd: " + cmd;
				IL_1F34:
				if (text != null)
				{
					global::Log.Party.Print(text, Array.Empty<object>());
					global::Error.AddWarning("Party Cheat Error", text, Array.Empty<object>());
					result = false;
				}
				return result;
			}
		}
		string message = "USAGE: party [cmd] [args]\nCommands: create | join | leave | dissolve | list | invite | accept | decline | revoke | requestinvite | ignorerequest | setleader | kick | chat | setprivacy | setlong | setstring | setblob | clearattr | subscribe | unsubscribe";
		global::Error.AddWarning("Party Cheat Error", message, Array.Empty<object>());
		return false;
	}

	// Token: 0x060076A0 RID: 30368 RVA: 0x00265640 File Offset: 0x00263840
	private static string GetPartyInviteSummary(PartyInvite invite, int index)
	{
		return string.Format("{0}: inviteId={1} sender={2} recipient={3} party={4}", new object[]
		{
			(index >= 0) ? string.Format("[{0}] ", index) : "",
			invite.InviteId,
			invite.InviterId + " " + invite.InviterName,
			invite.InviteeId,
			new PartyInfo(invite.PartyId, invite.PartyType)
		});
	}

	// Token: 0x060076A1 RID: 30369 RVA: 0x002656C0 File Offset: 0x002638C0
	private static string GetPartySummary(PartyInfo info, int index)
	{
		PartyMember leader = BnetParty.GetLeader(info.Id);
		return string.Format("{0}{1}: members={2} invites={3} privacy={4} leader={5}", new object[]
		{
			(index >= 0) ? string.Format("[{0}] ", index) : "",
			info,
			BnetParty.CountMembers(info.Id) + (BnetParty.IsPartyFull(info.Id, true) ? "(full)" : ""),
			BnetParty.GetSentInvites(info.Id).Length,
			BnetParty.GetPrivacyLevel(info.Id),
			(leader == null) ? "null" : leader.GameAccountId.ToString()
		});
	}

	// Token: 0x060076A2 RID: 30370 RVA: 0x00265780 File Offset: 0x00263980
	private bool OnProcessCheat_cheat(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = "spawncard, drawcard, loadcard, cyclehand, shuffle, addmana, readymana, maxmana, nocosts, healhero, healentity, nuke, damage, settag, ready, exhaust, freeze, move, undo, destroyhero, tiegame, getgsd, aiplaylastspawnedcard, forcestallingprevention, endturn, logrelay";
		if (autofillData != null)
		{
			string[] array = null;
			string[] array2 = new string[]
			{
				"friendly",
				"opponent"
			};
			string[] array3 = new string[]
			{
				"InPlay",
				"InDeck",
				"InHand",
				"InGraveyard",
				"InRemovedFromGame",
				"InSetAside",
				"InSecret"
			};
			Func<string[]> func2 = () => GameDbf.GetIndex().GetAllCardIds().ToArray();
			string searchTerm = autofillData.m_lastAutofillParamPrefix ?? ((args.Length == 0) ? string.Empty : args.Last<string>());
			int num = args.Length;
			if (rawArgs.EndsWith(" "))
			{
				searchTerm = string.Empty;
				num++;
			}
			if (num > 1 && !string.IsNullOrEmpty(args[0]))
			{
				text = null;
				string text2 = args[0];
				uint num2 = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num2 <= 1880656320U)
				{
					if (num2 <= 489885591U)
					{
						if (num2 != 58507283U)
						{
							if (num2 != 407568404U)
							{
								if (num2 != 489885591U)
								{
									goto IL_326;
								}
								if (!(text2 == "healhero"))
								{
									goto IL_326;
								}
							}
							else
							{
								if (!(text2 == "move"))
								{
									goto IL_326;
								}
								if (num == 3)
								{
									array = array3;
									goto IL_326;
								}
								goto IL_326;
							}
						}
						else if (!(text2 == "readymana"))
						{
							goto IL_326;
						}
					}
					else if (num2 != 1041558059U)
					{
						if (num2 != 1077260709U)
						{
							if (num2 != 1880656320U)
							{
								goto IL_326;
							}
							if (!(text2 == "nuke"))
							{
								goto IL_326;
							}
						}
						else if (!(text2 == "drawcard"))
						{
							goto IL_326;
						}
					}
					else
					{
						if (!(text2 == "loadcard"))
						{
							goto IL_326;
						}
						if (num == 2)
						{
							array = func2();
							goto IL_326;
						}
						goto IL_326;
					}
				}
				else if (num2 <= 2223574791U)
				{
					if (num2 != 2030840142U)
					{
						if (num2 != 2145047390U)
						{
							if (num2 != 2223574791U)
							{
								goto IL_326;
							}
							if (!(text2 == "getgsd"))
							{
								goto IL_326;
							}
							if (num == 2)
							{
								array = array2;
								goto IL_326;
							}
							goto IL_326;
						}
						else if (!(text2 == "maxmana"))
						{
							goto IL_326;
						}
					}
					else if (!(text2 == "shuffle"))
					{
						goto IL_326;
					}
				}
				else if (num2 <= 2805348300U)
				{
					if (num2 != 2539301267U)
					{
						if (num2 != 2805348300U)
						{
							goto IL_326;
						}
						if (!(text2 == "cyclehand"))
						{
							goto IL_326;
						}
					}
					else if (!(text2 == "addmana"))
					{
						goto IL_326;
					}
				}
				else if (num2 != 2818391513U)
				{
					if (num2 != 3395887894U)
					{
						goto IL_326;
					}
					if (!(text2 == "spawncard"))
					{
						goto IL_326;
					}
					if (num == 4)
					{
						array = new string[]
						{
							"1",
							"0"
						};
						goto IL_326;
					}
					if (num == 3)
					{
						array = array3;
						goto IL_326;
					}
					if (num == 2)
					{
						array = func2();
						goto IL_326;
					}
					goto IL_326;
				}
				else if (!(text2 == "destroyhero"))
				{
					goto IL_326;
				}
				if (num == 2)
				{
					array = array2;
				}
			}
			IL_326:
			if (array == null)
			{
				if (text == null)
				{
					return false;
				}
				array = text.Split(new char[]
				{
					' ',
					','
				}, StringSplitOptions.RemoveEmptyEntries);
			}
			return this.ProcessAutofillParam(array, searchTerm, autofillData);
		}
		if (!Network.Get().IsConnectedToGameServer())
		{
			UIStatus.Get().AddInfoNoRichText("Not connected to a game. Cannot send cheat command.", -1f);
			return true;
		}
		Network.Get().SendDebugConsoleCommand(rawArgs);
		return true;
	}

	// Token: 0x060076A3 RID: 30371 RVA: 0x00265B10 File Offset: 0x00263D10
	private bool OnProcessCheat_autohand(string func, string[] args, string rawArgs)
	{
		if (args.Length == 0)
		{
			return false;
		}
		bool flag;
		if (!GeneralUtils.TryParseBool(args[0], out flag))
		{
			return false;
		}
		if (InputManager.Get() == null)
		{
			return false;
		}
		string message;
		if (flag)
		{
			message = "auto hand hiding is on";
		}
		else
		{
			message = "auto hand hiding is off";
		}
		Debug.Log(message);
		UIStatus.Get().AddInfo(message);
		InputManager.Get().SetHideHandAfterPlayingCard(flag);
		return true;
	}

	// Token: 0x060076A4 RID: 30372 RVA: 0x00265B70 File Offset: 0x00263D70
	private bool OnProcessCheat_adventureChallengeUnlock(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		int item;
		if (!int.TryParse(args[0].ToLowerInvariant(), out item))
		{
			return false;
		}
		List<int> list = new List<int>();
		list.Add(item);
		AdventureMissionDisplay.Get().ShowClassChallengeUnlock(list);
		return true;
	}

	// Token: 0x060076A5 RID: 30373 RVA: 0x00265BB1 File Offset: 0x00263DB1
	private bool OnProcessCheat_iks(string func, string[] args, string rawArgs)
	{
		InnKeepersSpecial.Get().InitializeJsonURL(args[0]);
		InnKeepersSpecial.Get().ResetAdUrl();
		Processor.RunCoroutine(this.TriggerWelcomeQuestShow(), null);
		return true;
	}

	// Token: 0x060076A6 RID: 30374 RVA: 0x00265BD8 File Offset: 0x00263DD8
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
			WelcomeQuests.Show(UserAttentionBlocker.ALL, true, null, false);
		}
		yield break;
	}

	// Token: 0x060076A7 RID: 30375 RVA: 0x00265BE0 File Offset: 0x00263DE0
	private bool OnProcessCheat_iksgameaction(string func, string[] args, string rawArgs)
	{
		if (string.IsNullOrEmpty(rawArgs))
		{
			UIStatus.Get().AddError("Please specify a game action.", -1f);
			return true;
		}
		DeepLinkManager.ExecuteDeepLink(args, DeepLinkManager.DeepLinkSource.INNKEEPERS_SPECIAL, false);
		return true;
	}

	// Token: 0x060076A8 RID: 30376 RVA: 0x00265C0C File Offset: 0x00263E0C
	private bool OnProcessCheat_iksseen(string func, string[] args, string rawArgs)
	{
		if (string.IsNullOrEmpty(rawArgs))
		{
			UIStatus.Get().AddError("Please specify a game action.", -1f);
			return true;
		}
		string gameAction = string.Join(" ", args);
		UIStatus.Get().AddInfo("Has Interacted With Product: " + InnKeepersSpecial.Get().HasInteractedWithAdvertisedProduct(gameAction).ToString());
		return true;
	}

	// Token: 0x060076A9 RID: 30377 RVA: 0x00265C6C File Offset: 0x00263E6C
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
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.ALL, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get(text2), soundPath, 0f, null, false);
		}
		else
		{
			NotificationManager.Get().CreateCharacterQuote(text, NotificationManager.DEFAULT_CHARACTER_POS, GameStrings.Get(text2), soundPath, true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
		}
		return true;
	}

	// Token: 0x060076AA RID: 30378 RVA: 0x00265D30 File Offset: 0x00263F30
	private bool OnProcessCheat_popuptext(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		string text = args[0];
		NotificationManager.Get().CreatePopupText(UserAttentionBlocker.ALL, Box.Get().m_LeftDoor.transform.position, TutorialEntity.GetTextScale(), text, true, NotificationManager.PopupTextType.BASIC);
		return true;
	}

	// Token: 0x060076AB RID: 30379 RVA: 0x00265D74 File Offset: 0x00263F74
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

	// Token: 0x060076AC RID: 30380 RVA: 0x00265D9C File Offset: 0x00263F9C
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

	// Token: 0x060076AD RID: 30381 RVA: 0x00265DD8 File Offset: 0x00263FD8
	private bool OnProcessCheat_logtext(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			return false;
		}
		if (args.Length > 1)
		{
			string format = rawArgs.Substring(rawArgs.IndexOf(' ') + 1);
			string a = args[0];
			if (a == "debug")
			{
				global::Log.All.PrintDebug(format, Array.Empty<object>());
				return true;
			}
			if (a == "info")
			{
				global::Log.All.PrintInfo(format, Array.Empty<object>());
				return true;
			}
			if (a == "warning")
			{
				global::Log.All.PrintWarning(format, Array.Empty<object>());
				return true;
			}
			if (a == "error")
			{
				global::Log.All.PrintError(format, Array.Empty<object>());
				return true;
			}
		}
		global::Log.All.Print(rawArgs, Array.Empty<object>());
		return true;
	}

	// Token: 0x060076AE RID: 30382 RVA: 0x00265E9C File Offset: 0x0026409C
	private bool OnProcessCheat_logenable(string func, string[] args, string rawArgs)
	{
		if (args.Count<string>() < 3)
		{
			return false;
		}
		string name = args[0];
		LogInfo logInfo = global::Log.Get().GetLogInfo(name);
		if (logInfo == null)
		{
			return false;
		}
		string a = args[1];
		string text = args[2];
		bool flag = !text.Equals("false", StringComparison.OrdinalIgnoreCase) && text != "0";
		if (!(a == "file"))
		{
			if (!(a == "screen"))
			{
				if (!(a == "console"))
				{
					return false;
				}
				logInfo.m_consolePrinting = flag;
			}
			else
			{
				logInfo.m_screenPrinting = flag;
			}
		}
		else
		{
			logInfo.m_filePrinting = flag;
		}
		return true;
	}

	// Token: 0x060076AF RID: 30383 RVA: 0x00265F3C File Offset: 0x0026413C
	private bool OnProcessCheat_loglevel(string func, string[] args, string rawArgs)
	{
		if (args.Count<string>() < 2)
		{
			return false;
		}
		string name = args.ElementAtOrDefault(0);
		global::Log.LogLevel minLevel;
		if (!global::EnumUtils.TryGetEnum<global::Log.LogLevel>(args.ElementAtOrDefault(1), StringComparison.OrdinalIgnoreCase, out minLevel))
		{
			return false;
		}
		LogInfo logInfo = global::Log.Get().GetLogInfo(name);
		if (logInfo == null)
		{
			return false;
		}
		logInfo.m_minLevel = minLevel;
		return true;
	}

	// Token: 0x060076B0 RID: 30384 RVA: 0x00265F88 File Offset: 0x00264188
	private bool OnProcessCheat_cardchangepopup(string func, string[] args, string rawArgs)
	{
		bool flag = false;
		if (args.Length >= 1 && args[0].Length > 0)
		{
			GeneralUtils.TryParseBool(args[0], out flag);
		}
		bool flag2 = true;
		if (args.Length >= 2)
		{
			GeneralUtils.TryParseBool(args[1], out flag2);
		}
		int count = 3;
		if (args.Length >= 3)
		{
			GeneralUtils.TryParseInt(args[2], out count);
		}
		List<CollectibleCard> cardsToShowOverride = null;
		if (flag2)
		{
			cardsToShowOverride = CollectionManager.Get().GetAllCards().FindAll((CollectibleCard card) => card.PremiumType == TAG_PREMIUM.NORMAL).Take(count).ToList<CollectibleCard>();
		}
		if (flag)
		{
			PopupDisplayManager.Get().ShowAddedCards(null, UserAttentionBlocker.SET_ROTATION_INTRO, null, cardsToShowOverride);
		}
		else
		{
			PopupDisplayManager.Get().ShowChangedCards(null, UserAttentionBlocker.SET_ROTATION_INTRO, null, cardsToShowOverride);
		}
		return true;
	}

	// Token: 0x060076B1 RID: 30385 RVA: 0x0026603C File Offset: 0x0026423C
	private bool OnProcessCheat_cardchangereset(string func, string[] args, string rawArgs)
	{
		ChangedCardMgr.Get().ResetCardChangesSeen();
		return true;
	}

	// Token: 0x060076B2 RID: 30386 RVA: 0x00266049 File Offset: 0x00264249
	private bool OnProcessCheat_loginpopupsequence(string func, string[] args, string rawArgs)
	{
		PopupDisplayManager.Get().ShowLoginPopupSequence();
		return true;
	}

	// Token: 0x060076B3 RID: 30387 RVA: 0x00266057 File Offset: 0x00264257
	private bool OnProcessCheat_loginpopupreset(string func, string[] args, string rawArgs)
	{
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.PLAYER_OPTIONS, GameSaveKeySubkeyId.LOGIN_POPUP_SEQUENCE_SEEN_POPUPS, new long[0]), null);
		return true;
	}

	// Token: 0x060076B4 RID: 30388 RVA: 0x0026607C File Offset: 0x0026427C
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
		int num;
		if (!int.TryParse(args[0].ToLowerInvariant(), out num))
		{
			return false;
		}
		TAG_CLASS tag_CLASS;
		if (!global::EnumUtils.TryCast<TAG_CLASS>(num, out tag_CLASS))
		{
			return false;
		}
		string name = args[1];
		int num2;
		if (!int.TryParse(args[2].ToLowerInvariant(), out num2))
		{
			return false;
		}
		TAG_PREMIUM premium;
		if (!global::EnumUtils.TryCast<TAG_PREMIUM>(num2, out premium))
		{
			return false;
		}
		NetCache.CardDefinition cardDefinition = new NetCache.CardDefinition
		{
			Name = name,
			Premium = premium
		};
		global::Log.All.Print("OnProcessCheat_favoritehero setting favorite hero to {0} for class {1}", new object[]
		{
			cardDefinition,
			tag_CLASS
		});
		Network.Get().SetFavoriteHero(tag_CLASS, cardDefinition);
		return true;
	}

	// Token: 0x060076B5 RID: 30389 RVA: 0x00266144 File Offset: 0x00264344
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
		int entityID = 0;
		if (!int.TryParse(args[1], out entityID))
		{
			string entityIdentifier = args[1];
			Network.Get().SetTag(num, entityIdentifier, num2);
			return true;
		}
		Network.Get().SetTag(num, entityID, num2);
		return true;
	}

	// Token: 0x060076B6 RID: 30390 RVA: 0x002661A8 File Offset: 0x002643A8
	private bool OnProcessCheat_debugscript(string func, string[] args, string rawArgs)
	{
		ScriptDebugDisplay.Get().ToggleDebugDisplay(true);
		if (args.Length != 1)
		{
			return false;
		}
		string powerGUID = args[0];
		Network.Get().DebugScript(powerGUID);
		return true;
	}

	// Token: 0x060076B7 RID: 30391 RVA: 0x002661D9 File Offset: 0x002643D9
	private bool OnProcessCheat_disablescriptdebug(string func, string[] args, string rawArgs)
	{
		ScriptDebugDisplay.Get().ToggleDebugDisplay(false);
		Network.Get().DisableScriptDebug();
		return true;
	}

	// Token: 0x060076B8 RID: 30392 RVA: 0x002661F4 File Offset: 0x002643F4
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

	// Token: 0x060076B9 RID: 30393 RVA: 0x00266244 File Offset: 0x00264444
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
			using (IEnumerator<string> enumerator = CheatMgr.Get().GetCheatCommands().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text2 = enumerator.Current;
					if (text2.Contains(text))
					{
						list.Add(text2);
					}
				}
				goto IL_99;
			}
		}
		foreach (string item in CheatMgr.Get().GetCheatCommands())
		{
			list.Add(item);
		}
		IL_99:
		Debug.Log(string.Concat(new object[]
		{
			"found commands ",
			list,
			" ",
			list.Count
		}));
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
			string text3 = "";
			foreach (string str in list)
			{
				text3 = text3 + str + ", ";
				num++;
				if (num > 4)
				{
					num = 0;
					stringBuilder.Append(text3);
					text3 = "";
				}
			}
			if (!string.IsNullOrEmpty(text3))
			{
				stringBuilder.Append(text3);
			}
			UIStatus.Get().AddInfo(stringBuilder.ToString(), 10f);
		}
		else
		{
			string text4 = "";
			CheatMgr.Get().cheatDesc.TryGetValue(text, out text4);
			string text5 = "";
			CheatMgr.Get().cheatArgs.TryGetValue(text, out text5);
			stringBuilder.Append("Usage: ");
			stringBuilder.Append(text);
			if (!string.IsNullOrEmpty(text5))
			{
				stringBuilder.Append(" " + text5);
			}
			if (!string.IsNullOrEmpty(text4))
			{
				stringBuilder.Append("\n(" + text4 + ")");
			}
			UIStatus.Get().AddInfo(stringBuilder.ToString(), 10f);
		}
		return true;
	}

	// Token: 0x060076BA RID: 30394 RVA: 0x002664B8 File Offset: 0x002646B8
	private bool OnProcessCheat_fixedrewardcomplete(string func, string[] args, string rawArgs)
	{
		int fixedRewardMapID;
		return args.Length >= 1 && !string.IsNullOrEmpty(args[0]) && GeneralUtils.TryParseInt(args[0], out fixedRewardMapID) && FixedRewardsMgr.Get().Cheat_ShowFixedReward(fixedRewardMapID, new FixedRewardsMgr.DelPositionNonToastReward(this.PositionLoginFixedReward));
	}

	// Token: 0x060076BB RID: 30395 RVA: 0x002664FC File Offset: 0x002646FC
	private void PositionLoginFixedReward(Reward reward)
	{
		PegasusScene scene = SceneMgr.Get().GetScene();
		reward.transform.parent = scene.transform;
		reward.transform.localRotation = Quaternion.identity;
		reward.transform.localPosition = PopupDisplayManager.Get().GetRewardLocalPos();
	}

	// Token: 0x060076BC RID: 30396 RVA: 0x0026654C File Offset: 0x0026474C
	private bool OnProcessCheat_example(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
		{
			return false;
		}
		string text = args[0];
		string str = "";
		if (!CheatMgr.Get().cheatExamples.TryGetValue(text, out str))
		{
			return false;
		}
		CheatMgr.Get().ProcessCheat(text + " " + str, false);
		return true;
	}

	// Token: 0x060076BD RID: 30397 RVA: 0x002665A4 File Offset: 0x002647A4
	private bool OnProcessCheat_tavernbrawl(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: tb [cmd] [args]\nCommands: view, get, set, refresh, scenario, reset";
		if (args.Length >= 1)
		{
			if (!args.Any((string a) => string.IsNullOrEmpty(a)))
			{
				string text = args[0];
				string[] array = args.Skip(1).ToArray<string>();
				string text2 = null;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 3324446467U)
				{
					if (num <= 1410115415U)
					{
						if (num != 946971642U)
						{
							if (num != 1410115415U)
							{
								goto IL_59B;
							}
							if (!(text == "get"))
							{
								goto IL_59B;
							}
						}
						else
						{
							if (!(text == "help"))
							{
								goto IL_59B;
							}
							text2 = "usage";
							goto IL_59B;
						}
					}
					else if (num != 1695364032U)
					{
						if (num != 3139823063U)
						{
							if (num != 3324446467U)
							{
								goto IL_59B;
							}
							if (!(text == "set"))
							{
								goto IL_59B;
							}
						}
						else
						{
							if (!(text == "do_rewards"))
							{
								goto IL_59B;
							}
							int wins = 0;
							int.TryParse(array[0], out wins);
							TavernBrawlMode mode = TavernBrawlMode.TB_MODE_NORMAL;
							if (array.Length > 1)
							{
								mode = (array[1].Equals("heroic") ? TavernBrawlMode.TB_MODE_HEROIC : TavernBrawlMode.TB_MODE_NORMAL);
							}
							TavernBrawlManager.Get().Cheat_DoHeroicRewards(wins, mode);
							text2 = "Doing reward animation and ending fake session if one exists.";
							goto IL_59B;
						}
					}
					else
					{
						if (!(text == "reset"))
						{
							goto IL_59B;
						}
						if (array.Length == 0)
						{
							text2 = "Please specify what to reset: seen, toserver";
							goto IL_59B;
						}
						if ("toserver".Equals(array[0], StringComparison.InvariantCultureIgnoreCase))
						{
							if (!TavernBrawlManager.Get().IsCheated)
							{
								text2 = "TB not locally cheated. Already using server-specified data.";
								goto IL_59B;
							}
							TavernBrawlManager.Get().Cheat_ResetToServerData();
							TavernBrawlMission tavernBrawlMission = TavernBrawlManager.Get().CurrentMission();
							if (tavernBrawlMission == null)
							{
								text2 = "TB settings reset to server-specified Scenario ID <null>";
								goto IL_59B;
							}
							text2 = "TB settings reset to server-specified Scenario ID " + tavernBrawlMission.missionId;
							goto IL_59B;
						}
						else
						{
							if (!"seen".Equals(array[0], StringComparison.InvariantCultureIgnoreCase))
							{
								text2 = "Unknown reset parameter: " + array[0];
								goto IL_59B;
							}
							int num2 = 0;
							if (array.Length > 1 && !int.TryParse(array[1], out num2))
							{
								text2 = "Error parsing new seen value: " + array[1];
							}
							if (text2 == null)
							{
								TavernBrawlManager.Get().Cheat_ResetSeenStuff(num2);
								text2 = "all \"seentb*\" client-options reset to " + num2;
								goto IL_59B;
							}
							goto IL_59B;
						}
					}
					bool flag = text == "set";
					string text3 = array.FirstOrDefault<string>();
					if (string.IsNullOrEmpty(text3))
					{
						text2 = string.Format("Please specify a TB variable to {0}. Variables:RefreshTime", text);
					}
					else
					{
						string text4 = null;
						string a2 = text3.ToLower();
						if (!(a2 == "refreshtime"))
						{
							if (!(a2 == "wins"))
							{
								if (a2 == "losses")
								{
									int num3 = 0;
									int.TryParse(args[2], out num3);
									TavernBrawlManager.Get().Cheat_SetLosses(num3);
									text2 = string.Format("tb set losses {0} successful", num3);
								}
							}
							else
							{
								int num4 = 0;
								int.TryParse(args[2], out num4);
								TavernBrawlManager.Get().Cheat_SetWins(num4);
								text2 = string.Format("tb set wins {0} successful", num4);
							}
						}
						else if (flag)
						{
							text2 = "cannot set RefreshTime";
						}
						else
						{
							text4 = TavernBrawlManager.Get().CurrentScheduledSecondsToRefresh + " secs";
						}
						if (flag)
						{
							text2 = string.Format("tb set {0} {1} successful.", text3, (array.Length >= 2) ? array[1] : "null");
						}
						else if (string.IsNullOrEmpty(text2))
						{
							text2 = string.Format("tb variable {0}: {1}", text3, text4 ?? "null");
						}
					}
				}
				else
				{
					if (num <= 3572655668U)
					{
						if (num != 3438346468U)
						{
							if (num != 3572655668U)
							{
								goto IL_59B;
							}
							if (!(text == "refresh"))
							{
								goto IL_59B;
							}
							for (BrawlType brawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL; brawlType < BrawlType.BRAWL_TYPE_COUNT; brawlType++)
							{
								TavernBrawlManager.Get().RefreshServerData(brawlType);
							}
							text2 = "TB refreshing";
							goto IL_59B;
						}
						else if (!(text == "scen"))
						{
							goto IL_59B;
						}
					}
					else if (num != 3660387664U)
					{
						if (num != 3685020920U)
						{
							if (num != 3987823151U)
							{
								goto IL_59B;
							}
							if (!(text == "scenario"))
							{
								goto IL_59B;
							}
						}
						else
						{
							if (!(text == "view"))
							{
								goto IL_59B;
							}
							TavernBrawlMission tavernBrawlMission2 = TavernBrawlManager.Get().CurrentMission();
							if (tavernBrawlMission2 == null)
							{
								text2 = "No active Tavern Brawl at this time.";
								goto IL_59B;
							}
							string arg = "";
							string arg2 = "";
							ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(tavernBrawlMission2.missionId);
							if (record != null)
							{
								arg = record.Name;
								arg2 = record.Description;
							}
							text2 = string.Format("Active TB: [{0}] {1}\n{2}", tavernBrawlMission2.missionId, arg, arg2);
							goto IL_59B;
						}
					}
					else
					{
						if (!(text == "fake_active_session"))
						{
							goto IL_59B;
						}
						int status = 0;
						int.TryParse(args[1], out status);
						TavernBrawlManager.Get().Cheat_SetActiveSession(status);
						text2 = "Fake Tavern Brawl Session set.";
						goto IL_59B;
					}
					if (array.Length < 1)
					{
						text2 = "tb scenario: requires an ID parameter";
					}
					else
					{
						BrawlType brawlType2 = BrawlType.BRAWL_TYPE_TAVERN_BRAWL;
						if (array.Length > 1)
						{
							int num5 = -1;
							if (int.TryParse(array[1], out num5) && num5 >= 1 && num5 < 3)
							{
								brawlType2 = (BrawlType)num5;
							}
						}
						int num6;
						if (!int.TryParse(array[0], out num6))
						{
							text2 = "tb scenario: invalid non-integer Scenario ID " + array[0];
						}
						if (text2 == null)
						{
							TavernBrawlManager.Get().Cheat_SetScenario(num6, brawlType2);
							text2 = string.Concat(new object[]
							{
								"tb scenario: set on client to ID: ",
								num6,
								" for type: ",
								brawlType2
							});
						}
					}
				}
				IL_59B:
				if (text2 != null)
				{
					UIStatus.Get().AddInfo(text2, 5f);
				}
				return true;
			}
		}
		UIStatus.Get().AddInfo(message, 10f);
		return true;
	}

	// Token: 0x060076BE RID: 30398 RVA: 0x00266B60 File Offset: 0x00264D60
	private bool OnProcessCheat_duels(string func, string[] args, string rawArgs)
	{
		string text = "USAGE: duels [cmd] [args]\nCommands: nexttreasures nextloot";
		if (args.Length >= 1)
		{
			if (!args.Any((string a) => string.IsNullOrEmpty(a)))
			{
				string a2 = args[0];
				string[] array = args.Skip(1).ToArray<string>();
				string text2 = null;
				if (!(a2 == "help"))
				{
					if (!(a2 == "nexttreasures"))
					{
						if (a2 == "nextloot")
						{
							if (array.Length < 1)
							{
								text2 = "duels nextloot: requires at least 1 id to add";
							}
							else
							{
								int num = 0;
								foreach (string text3 in array)
								{
									int num2 = 0;
									if (!int.TryParse(text3, out num2))
									{
										num2 = GameUtils.TranslateCardIdToDbId(text3, false);
										if (num2 == 0)
										{
											text2 = "invalid card id: " + text3;
											break;
										}
									}
									this.m_pvpdrLootIds.Enqueue(num2);
									num++;
								}
								if (text2 == null)
								{
									text2 = "Added " + num + " cards to next loot list";
								}
							}
						}
					}
					else if (array.Length < 1)
					{
						text2 = "duels nexttreasures: requires 1-3 card ids to add";
					}
					else
					{
						int num3 = 0;
						foreach (string text4 in array)
						{
							int num4 = 0;
							if (!int.TryParse(text4, out num4))
							{
								num4 = GameUtils.TranslateCardIdToDbId(text4, false);
								if (num4 == 0)
								{
									text2 = "invalid card id: " + text4;
									break;
								}
							}
							this.m_pvpdrTreasureIds.Enqueue(num4);
							num3++;
						}
						if (text2 == null)
						{
							text2 = "Added " + num3 + " cards to next treasures list";
						}
					}
				}
				else
				{
					text2 = text;
				}
				if (text2 != null)
				{
					UIStatus.Get().AddInfo(text2, 5f);
				}
				return true;
			}
		}
		UIStatus.Get().AddInfo(text, 10f);
		return true;
	}

	// Token: 0x060076BF RID: 30399 RVA: 0x00266D28 File Offset: 0x00264F28
	private bool OnProcessCheat_fsg(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string text = "checkin, checkout, fake_gatherings, no_fake_gatherings, fake_large_scale, nearby_notice, sign, view, gps_offset, gps_set, gps_reset, find, finalize, vars, player, refreshpatrons, returntooltip";
		string[] values = text.Split(new char[]
		{
			' ',
			','
		}, StringSplitOptions.RemoveEmptyEntries);
		string text2 = "USAGE: fsg [cmd] [args]\nCommands: " + text;
		if (args.Length >= 1)
		{
			if (!args.Any((string a) => string.IsNullOrEmpty(a)))
			{
				if (autofillData != null)
				{
					return args.Length == 1 && this.ProcessAutofillParam(values, args[0], autofillData);
				}
				float delay = 5f * Time.timeScale;
				string text3 = args[0];
				string text4 = null;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
				if (num <= 1185671095U)
				{
					if (num <= 671335251U)
					{
						if (num <= 213683108U)
						{
							if (num != 74980371U)
							{
								if (num == 213683108U)
								{
									if (text3 == "sign")
									{
										int num2 = UnityEngine.Random.Range(1, 8);
										int num3 = UnityEngine.Random.Range(1, 15);
										int num4 = UnityEngine.Random.Range(1, 85);
										int num5 = UnityEngine.Random.Range(1, 43);
										TavernSignType type = TavernSignType.TAVERN_SIGN_TYPE_CUSTOM;
										if (args.Length > 1)
										{
											string str = args[1];
											if (!global::EnumUtils.TryGetEnum<TavernSignType>(("TAVERN_SIGN_TYPE_" + str).ToLower(), out type))
											{
												type = TavernSignType.TAVERN_SIGN_TYPE_CUSTOM;
											}
											int.TryParse(args[1], out num2);
											if (num2 < 1)
											{
												num2 = 1;
											}
										}
										if (args.Length > 2)
										{
											int.TryParse(args[2], out num3);
											if (num3 < 1)
											{
												num3 = 1;
											}
										}
										if (args.Length > 3)
										{
											int.TryParse(args[3], out num4);
											if (num4 < 1)
											{
												num4 = 1;
											}
										}
										if (args.Length > 4)
										{
											int.TryParse(args[4], out num5);
											if (num5 < 1)
											{
												num5 = 1;
											}
										}
										string tavernName;
										if (args.Length > 5)
										{
											tavernName = string.Join(" ", args.Slice(5));
										}
										else
										{
											tavernName = string.Format("fsg sign {0} {1} {2} {3}", new object[]
											{
												num2,
												num3,
												num4,
												num5
											});
										}
										FiresideGatheringManager.Get().Cheat_ShowSign(type, num2, num3, num4, num5, tavernName);
									}
								}
							}
							else if (text3 == "fake_gatherings")
							{
								int num6 = 2;
								bool innkeeper = false;
								if (args.Length > 1)
								{
									int.TryParse(args[1], out num6);
									if (num6 < 1)
									{
										num6 = 1;
									}
								}
								if (args.Length > 2)
								{
									GeneralUtils.TryParseBool(args[2], out innkeeper);
								}
								FiresideGatheringManager.Get().Cheat_CreateFakeGatherings(num6, innkeeper);
								text4 = string.Format("Created {0} fake gatherings", num6);
							}
						}
						else if (num != 657316428U)
						{
							if (num == 671335251U)
							{
								if (text3 == "gps_reset")
								{
									FiresideGatheringManager.Get().Cheat_ResetGPSCheating();
									text4 = "GPS cheats have been reset.";
								}
							}
						}
						else if (text3 == "checkin")
						{
							FiresideGatheringManager.Get().Cheat_CheckInToFakeFSG();
							text4 = "Checked in to fake FSG";
						}
					}
					else if (num <= 748274432U)
					{
						if (num != 697234855U)
						{
							if (num == 748274432U)
							{
								if (text3 == "player")
								{
									StringBuilder builder = new StringBuilder();
									int lines = 0;
									Action<string, object> action = delegate(string displayName, object value)
									{
										int lines;
										if (lines != 0)
										{
											builder.Append("\n");
										}
										builder.AppendFormat("{0}={1}", displayName, value);
										lines++;
										lines = lines;
									};
									action(global::EnumUtils.GetString<global::Option>(global::Option.SHOULD_AUTO_CHECK_IN_TO_FIRESIDE_GATHERINGS), FiresideGatheringManager.Get().PlayerAccountShouldAutoCheckin);
									action(global::EnumUtils.GetString<global::Option>(global::Option.HAS_INITIATED_FIRESIDE_GATHERING_SCAN), FiresideGatheringManager.Get().HasManuallyInitiatedFSGScanBefore);
									action(global::EnumUtils.GetString<global::Option>(global::Option.LAST_TAVERN_JOINED), FiresideGatheringManager.Get().LastTavernID);
									string text5 = builder.ToString();
									global::Log.All.Print(text5, Array.Empty<object>());
									text4 = text5.Replace("\n", ", ") + "\n";
									delay = Mathf.Min(30f, 5f * (float)lines) * Time.timeScale;
								}
							}
						}
						else if (text3 == "nearby_notice")
						{
							FiresideGatheringManager.Get().Cheat_NearbyFSGNotice();
							text4 = "Simulating nearby FSGs when checked out";
						}
					}
					else if (num != 894411951U)
					{
						if (num != 946971642U)
						{
							if (num == 1185671095U)
							{
								if (text3 == "vars")
								{
									StringBuilder builder = new StringBuilder();
									int lines = 0;
									Action<string, object> action2 = delegate(string displayName, object value)
									{
										int lines;
										if (lines != 0)
										{
											builder.Append("\n");
										}
										int num13 = displayName.LastIndexOf('.');
										if (num13 >= 0 && num13 < displayName.Length - 1)
										{
											displayName = displayName.Substring(num13 + 1);
										}
										builder.AppendFormat("{0}={1}", displayName, value);
										lines++;
										lines = lines;
									};
									foreach (string text6 in new string[]
									{
										"Location.Latitude",
										"Location.Longitude",
										"Location.BSSID"
									})
									{
										VarKey varKey = Vars.Key(text6);
										if (varKey.HasValue)
										{
											action2(text6, varKey.GetStr("null"));
										}
									}
									NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
									action2("FSGEnabled", netObject.FSGEnabled);
									action2("AutoCheckin", FiresideGatheringManager.Get().AutoCheckInEnabled);
									action2("LoginScan", netObject.FSGLoginScanEnabled);
									action2("MaxPubscribedPatrons", netObject.FsgMaxPresencePubscribedPatronCount);
									action2("PatronCountLimit", FiresideGatheringManager.Get().FriendListPatronCountLimit);
									action2("FSG.PeriodicPrunePatronOldSubscriptionsSeconds", FiresideGatheringPresenceManager.PERIODIC_SUBSCRIBE_CHECK_SECONDS + "s");
									action2("FSG.PatronOldSubscriptionThresholdSeconds", FiresideGatheringPresenceManager.PATRON_OLD_SUBSCRIPTION_THRESHOLD_SECONDS + "s");
									action2("FSG.PresenceSubscriptionsVerboseLog", FiresideGatheringPresenceManager.IsVerboseLogging);
									action2("ToScreen", FiresideGatheringPresenceManager.IsVerboseLoggingToScreen);
									string text7 = builder.ToString();
									global::Log.All.Print(text7, Array.Empty<object>());
									text4 = text7.Replace("\n", ", ") + "\n";
									delay = Mathf.Min(30f, 5f * (float)lines) * Time.timeScale;
								}
							}
						}
						else if (text3 == "help")
						{
							text4 = text2;
						}
					}
					else if (text3 == "gps_offset")
					{
						double num7 = 0.0;
						if (args.Length > 1)
						{
							double.TryParse(args[1], out num7);
						}
						FiresideGatheringManager.Get().Cheat_GPSOffset(num7);
						text4 = "Set GPS Offset to: " + num7;
					}
				}
				else if (num <= 2596988452U)
				{
					if (num <= 1440200093U)
					{
						if (num != 1303992121U)
						{
							if (num == 1440200093U)
							{
								if (text3 == "refreshpatrons")
								{
									Network.Get().RequestFSGPatronListUpdate();
								}
							}
						}
						else if (text3 == "checkout")
						{
							FiresideGatheringManager.Get().CheckOutOfFSG(false);
							text4 = "Checked out from FSG";
						}
					}
					else if (num != 1529690686U)
					{
						if (num != 2468647292U)
						{
							if (num == 2596988452U)
							{
								if (text3 == "debug_sign")
								{
									TavernSignData lastSign = FiresideGatheringManager.Get().LastSign;
									if (lastSign == null)
									{
										text4 = "No Sign has been shown";
									}
									else
									{
										text4 = string.Format("Last FSG Sign:\nSign: {0}\nBackground: {1}\nMajor: {2}\nMinor: {3}", new object[]
										{
											lastSign.Sign,
											lastSign.Background,
											lastSign.Major,
											lastSign.Minor
										});
									}
								}
							}
						}
						else if (text3 == "gps_set")
						{
							double num8 = 0.0;
							double num9 = 0.0;
							if (args.Length > 1)
							{
								double.TryParse(args[1], out num8);
							}
							if (args.Length > 2)
							{
								double.TryParse(args[2], out num9);
							}
							FiresideGatheringManager.Get().Cheat_GPSSet(num8, num9);
							text4 = string.Format("Set GPS Set to: [{0}, {1}]", num8, num9);
							if (args.Length >= 4 && "find".Equals(args[3], StringComparison.InvariantCultureIgnoreCase))
							{
								FiresideGatheringManager.Get().PlayerAccountShouldAutoCheckin.Set(true);
								FiresideGatheringManager.Get().RequestNearbyFSGs(false);
							}
						}
					}
					else if (text3 == "returntooltip")
					{
						FiresideGatheringManager.Get().HasSeenReturnToFSGSceneTooltip = false;
						FiresideGatheringManager.Get().ShowReturnToFSGSceneTooltip();
					}
				}
				else if (num <= 2953526881U)
				{
					if (num != 2849937919U)
					{
						if (num == 2953526881U)
						{
							if (text3 == "no_fake_gatherings")
							{
								FiresideGatheringManager.Get().Cheat_RemoveFakeGatherings();
								text4 = "Removed fake gatherings";
							}
						}
					}
					else if (text3 == "finalize")
					{
						if (!FiresideGatheringManager.Get().HasFSGToInnkeeperSetup)
						{
							UIStatus.Get().AddError("There is no FSG to call InnkeeperSetup on - make sure there is an FSG you've created on the website with this Battle.net account.", delay);
							return true;
						}
						FiresideGatheringManager.Get().InnkeeperSetupFSG(true);
						text4 = "InnkeeperSetupFSG sent to server for FSG ID: " + FiresideGatheringManager.Get().FSGToInnkeeperSetup.FsgId;
					}
				}
				else if (num != 3186656602U)
				{
					if (num != 3685020920U)
					{
						if (num == 3792404953U)
						{
							if (text3 == "fake_large_scale")
							{
								if (!FiresideGatheringManager.Get().IsCheckedIn)
								{
									text4 = "Check into an FSG first, to toggle Large Scale FSG.";
								}
								else
								{
									FiresideGatheringManager.Get().Cheat_ToggleLargeScaleFSG();
									text4 = "Large Scale FSG toggled to " + FiresideGatheringManager.Get().CurrentFSG.IsLargeScaleFsg.ToString();
								}
							}
						}
					}
					else if (text3 == "view")
					{
						FSGConfig currentFSG = FiresideGatheringManager.Get().CurrentFSG;
						if (currentFSG == null)
						{
							text4 = "No FSG currently checked in to.";
						}
						else
						{
							text4 = string.Format("Checked into FSG: [{0}] {1}\nStart w/ Slush: {2}\nEnd w/ Slush: {3}", new object[]
							{
								currentFSG.FsgId,
								currentFSG.TavernName,
								global::TimeUtils.UnixTimeStampToDateTimeUtc(currentFSG.UnixStartTimeWithSlush).ToLocalTime().ToString("R"),
								global::TimeUtils.UnixTimeStampToDateTimeUtc(currentFSG.UnixEndTimeWithSlush).ToLocalTime().ToString("R")
							});
						}
						text4 += "\n";
						string text8 = "No Data";
						ClientLocationData bestLocationData = ClientLocationManager.Get().GetBestLocationData();
						if (bestLocationData != null)
						{
							text8 = bestLocationData.ToString();
						}
						bool flag;
						double num10;
						double num11;
						double num12;
						FiresideGatheringManager.Get().Cheat_GetGPSCheats(out flag, out num10, out num11, out num12);
						if (flag || num12 != 0.0)
						{
							if (flag)
							{
								text8 += string.Format("GPS overridden w/ [{0}, {1}]", num10, num11);
							}
							if (num12 != 0.0)
							{
								text8 += string.Format(" offset={0}", num12);
							}
						}
						text4 += string.Format("FSG: {0} GPS: {1} WIFI: {2}\nClient Location Data:\n{3}", new object[]
						{
							FiresideGatheringManager.IsFSGFeatureEnabled ? "enabled" : "disabled",
							FiresideGatheringManager.IsGpsFeatureEnabled ? "enabled" : "disabled",
							FiresideGatheringManager.IsWifiFeatureEnabled ? "enabled" : "disabled",
							text8
						});
						if (FiresideGatheringManager.Get().HasFSGToInnkeeperSetup)
						{
							FSGConfig fsgtoInnkeeperSetup = FiresideGatheringManager.Get().FSGToInnkeeperSetup;
							if (!fsgtoInnkeeperSetup.IsSetupComplete)
							{
								text4 += string.Format("Innkeeper of FSG: [{0}] {1}\nStart w/ Slush: {2}\nEnd w/ Slush: {3}", new object[]
								{
									fsgtoInnkeeperSetup.FsgId,
									fsgtoInnkeeperSetup.TavernName,
									global::TimeUtils.UnixTimeStampToDateTimeUtc(fsgtoInnkeeperSetup.UnixStartTimeWithSlush).ToLocalTime().ToString("R"),
									global::TimeUtils.UnixTimeStampToDateTimeUtc(fsgtoInnkeeperSetup.UnixEndTimeWithSlush).ToLocalTime().ToString("R")
								});
							}
						}
						delay = 20f * Time.timeScale;
					}
				}
				else if (text3 == "find")
				{
					if (!FiresideGatheringManager.CanRequestNearbyFSG)
					{
						UIStatus.Get().AddError("Cannot make request for NearbyFSGs either because FSG is disabled or the location features are disabled for this player's country.", delay);
						return true;
					}
					if (args.Length > 1)
					{
						bool flag2 = false;
						double latitude = 0.0;
						double longitude = 0.0;
						if ("irvine".Equals(args[1], StringComparison.InvariantCultureIgnoreCase))
						{
							flag2 = true;
							latitude = 33.6578341;
							longitude = -117.7674501;
						}
						if (flag2)
						{
							FiresideGatheringManager.Get().Cheat_GPSSet(latitude, longitude);
						}
					}
					FiresideGatheringManager.Get().PlayerAccountShouldAutoCheckin.Set(true);
					FiresideGatheringManager.Get().RequestNearbyFSGs(false);
					text4 = "RequestNearbyFSGs sent to server.";
				}
				if (text4 != null)
				{
					UIStatus.Get().AddInfo(text4, delay);
				}
				return true;
			}
		}
		if (autofillData != null)
		{
			return this.ProcessAutofillParam(values, string.Empty, autofillData);
		}
		UIStatus.Get().AddInfo(text2, 10f * Time.timeScale);
		return true;
	}

	// Token: 0x060076C0 RID: 30400 RVA: 0x00267A70 File Offset: 0x00265C70
	private bool OnProcessCheat_GPS(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: gps [cmd] [args]\nCommands: on/off";
		if (args.Length >= 1)
		{
			if (!args.Any((string a) => string.IsNullOrEmpty(a)))
			{
				string text = args[0];
				string text2 = null;
				if (!(text == "off") && !(text == "on"))
				{
					if (text == "view")
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
					}
				}
				else
				{
					ClientLocationManager.Get().Cheat_SetGPSEnabled(text == "on");
					text2 = "GPS turned " + text;
				}
				if (text2 != null)
				{
					UIStatus.Get().AddInfo(text2, 5f);
				}
				return true;
			}
		}
		UIStatus.Get().AddInfo(message, 10f);
		return true;
	}

	// Token: 0x060076C1 RID: 30401 RVA: 0x00267B98 File Offset: 0x00265D98
	private bool OnProcessCheat_Wifi(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: wifi [cmd] [args]\nCommands: on/off";
		if (args.Length >= 1)
		{
			if (!args.Any((string a) => string.IsNullOrEmpty(a)))
			{
				string text = args[0];
				string text2 = null;
				if (!(text == "off") && !(text == "on"))
				{
					if (text == "view")
					{
						text2 = string.Format("WIFI Services: {0}", ClientLocationManager.Get().WifiEnabled ? "enabled" : "disabled");
					}
				}
				else
				{
					ClientLocationManager.Get().Cheat_SetWifiEnabled(text == "on");
					text2 = "WIFI turned " + text;
				}
				if (text2 != null)
				{
					UIStatus.Get().AddInfo(text2, 5f);
				}
				return true;
			}
		}
		UIStatus.Get().AddInfo(message, 10f);
		return true;
	}

	// Token: 0x060076C2 RID: 30402 RVA: 0x00267C74 File Offset: 0x00265E74
	private bool OnProcessCheat_utilservercmd(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		string[] values = new string[]
		{
			"help",
			"tb",
			"fsg",
			"arena",
			"ranked",
			"deck",
			"banner",
			"quest",
			"achieves",
			"prog",
			"setgsd",
			"returningplayer",
			"curl",
			"coin",
			"reward"
		};
		if (args.Length < 1 || string.IsNullOrEmpty(rawArgs))
		{
			if (autofillData != null)
			{
				return this.ProcessAutofillParam(values, string.Empty, autofillData);
			}
			UIStatus.Get().AddError("Must specify a sub-command.", -1f);
			return true;
		}
		else
		{
			string cmd = args[0].ToLower();
			string[] cmdArgs = args.Skip(1).ToArray<string>();
			string text = (cmdArgs.Length == 0) ? null : cmdArgs[0].ToLower();
			bool flag = cmd == "fsg" && text == "tb";
			if (autofillData != null && args.Length == 1 && !rawArgs.EndsWith(" "))
			{
				string cmd3 = cmd;
				return this.ProcessAutofillParam(values, cmd3, autofillData);
			}
			bool flag2 = true;
			string cmd2 = cmd;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(cmd2);
			if (num <= 2223574791U)
			{
				if (num <= 946971642U)
				{
					if (num <= 456875097U)
					{
						if (num != 164679485U)
						{
							if (num != 456875097U)
							{
								goto IL_BB6;
							}
							if (!(cmd2 == "hero"))
							{
								goto IL_BB6;
							}
							flag2 = false;
							goto IL_BB6;
						}
						else
						{
							if (!(cmd2 == "quest"))
							{
								goto IL_BB6;
							}
							goto IL_8A1;
						}
					}
					else if (num != 927282899U)
					{
						if (num != 946971642U)
						{
							goto IL_BB6;
						}
						if (!(cmd2 == "help"))
						{
							goto IL_BB6;
						}
						flag2 = false;
						goto IL_BB6;
					}
					else if (!(cmd2 == "tb"))
					{
						goto IL_BB6;
					}
				}
				else if (num <= 1706796520U)
				{
					if (num != 1097580943U)
					{
						if (num != 1706796520U)
						{
							goto IL_BB6;
						}
						if (!(cmd2 == "arena"))
						{
							goto IL_BB6;
						}
						flag2 = false;
						text = ((cmdArgs.Length < 2) ? null : cmdArgs[1].ToLower());
						if (autofillData != null)
						{
							if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
							{
								string[] values2 = "view_player, reward, ticket, set, view, list, season, scenario, end_offset, start_offset, active, dormant, choices".Split(new char[]
								{
									' ',
									','
								}, StringSplitOptions.RemoveEmptyEntries);
								return this.ProcessAutofillParam(values2, text, autofillData);
							}
							return false;
						}
						else
						{
							if (!(text == "reward"))
							{
								goto IL_BB6;
							}
							if (!cmdArgs.Any((string arg) => "justids".Equals(arg)))
							{
								List<string> list = cmdArgs.ToList<string>();
								list.Add("justids");
								cmdArgs = list.ToArray();
								goto IL_BB6;
							}
							goto IL_BB6;
						}
					}
					else
					{
						if (!(cmd2 == "banner"))
						{
							goto IL_BB6;
						}
						flag2 = false;
						if (autofillData != null)
						{
							if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
							{
								string[] values3 = "list, reset".Split(new char[]
								{
									' ',
									','
								}, StringSplitOptions.RemoveEmptyEntries);
								return this.ProcessAutofillParam(values3, text, autofillData);
							}
							return false;
						}
						else
						{
							if (string.IsNullOrEmpty(text) || text == "help")
							{
								UIStatus.Get().AddInfo("Usage: util banner <list | reset bannerId=#>\n\nClear seen banners (wooden signs at login) with IDs >= bannerId arg. If no parameters, clears out just latest known bannerId. If bannerId=0, all seen banners are cleared.", 5f);
								return true;
							}
							if (text == "list")
							{
								this.Cheat_ShowBannerList();
								return true;
							}
							goto IL_BB6;
						}
					}
				}
				else if (num != 1875138358U)
				{
					if (num != 1939365082U)
					{
						if (num != 2223574791U)
						{
							goto IL_BB6;
						}
						if (!(cmd2 == "getgsd"))
						{
							goto IL_BB6;
						}
						goto IL_A12;
					}
					else
					{
						if (!(cmd2 == "reward"))
						{
							goto IL_BB6;
						}
						flag2 = false;
						if (autofillData == null)
						{
							goto IL_BB6;
						}
						if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
						{
							string[] values4 = new string[]
							{
								"grantlist",
								"grantitem",
								"gold",
								"dust",
								"orbs",
								"booster",
								"card",
								"randomcard",
								"tavernticket",
								"cardback",
								"heroskin",
								"customcoin"
							};
							return this.ProcessAutofillParam(values4, text, autofillData);
						}
						return false;
					}
				}
				else
				{
					if (!(cmd2 == "logrelay"))
					{
						goto IL_BB6;
					}
					if (string.IsNullOrEmpty(text))
					{
						UIStatus.Get().AddInfo("USAGE: logrelay [logName]", 10f);
						return true;
					}
					flag2 = false;
					goto IL_BB6;
				}
			}
			else if (num <= 2975164269U)
			{
				if (num <= 2579677094U)
				{
					if (num != 2495160603U)
					{
						if (num != 2579677094U)
						{
							goto IL_BB6;
						}
						if (!(cmd2 == "ranked"))
						{
							goto IL_BB6;
						}
						flag2 = false;
						if (autofillData != null)
						{
							if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
							{
								string[] values5 = "view, season, set, reward, medal, win, lose, games, seasonroll, seasonreset".Split(new char[]
								{
									' ',
									','
								}, StringSplitOptions.RemoveEmptyEntries);
								return this.ProcessAutofillParam(values5, text, autofillData);
							}
							return false;
						}
						else
						{
							if (text == "seasonroll" || text == "seasonreset")
							{
								flag2 = true;
								goto IL_BB6;
							}
							goto IL_BB6;
						}
					}
					else
					{
						if (!(cmd2 == "curl"))
						{
							goto IL_BB6;
						}
						goto IL_A12;
					}
				}
				else if (num != 2696088587U)
				{
					if (num != 2866453672U)
					{
						if (num != 2975164269U)
						{
							goto IL_BB6;
						}
						if (!(cmd2 == "grant"))
						{
							goto IL_BB6;
						}
						goto IL_A12;
					}
					else
					{
						if (!(cmd2 == "deck"))
						{
							goto IL_BB6;
						}
						if (autofillData != null)
						{
							if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
							{
								string[] values6 = "view, test".Split(new char[]
								{
									' ',
									','
								}, StringSplitOptions.RemoveEmptyEntries);
								return this.ProcessAutofillParam(values6, text, autofillData);
							}
							return false;
						}
						else if (!(text == "view"))
						{
							if (!(text == "test"))
							{
								goto IL_BB6;
							}
							flag2 = false;
							goto IL_BB6;
						}
						else
						{
							flag2 = false;
							if (!cmdArgs.Any((string arg) => arg.StartsWith("details=", StringComparison.InvariantCultureIgnoreCase)))
							{
								cmdArgs = new List<string>(cmdArgs)
								{
									"details=0"
								}.ToArray();
								goto IL_BB6;
							}
							goto IL_BB6;
						}
					}
				}
				else
				{
					if (!(cmd2 == "setgsd"))
					{
						goto IL_BB6;
					}
					goto IL_A12;
				}
			}
			else if (num <= 3337757733U)
			{
				if (num != 3205395652U)
				{
					if (num != 3337757733U)
					{
						goto IL_BB6;
					}
					if (!(cmd2 == "achieves"))
					{
						goto IL_BB6;
					}
					goto IL_8A1;
				}
				else
				{
					if (!(cmd2 == "returningplayer"))
					{
						goto IL_BB6;
					}
					flag2 = false;
					if (autofillData == null)
					{
						goto IL_BB6;
					}
					if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
					{
						string[] values7 = "start, test_group, optout, complete, reset".Split(new char[]
						{
							' ',
							','
						}, StringSplitOptions.RemoveEmptyEntries);
						return this.ProcessAutofillParam(values7, text, autofillData);
					}
					return false;
				}
			}
			else if (num != 3520440507U)
			{
				if (num != 3528263180U)
				{
					if (num != 4030608941U)
					{
						goto IL_BB6;
					}
					if (!(cmd2 == "prog"))
					{
						goto IL_BB6;
					}
					bool result = false;
					if (this.ProcessAutofillParam_util_prog(rawArgs, cmdArgs, autofillData, ref result, ref flag2))
					{
						return result;
					}
					goto IL_BB6;
				}
				else
				{
					if (!(cmd2 == "coin"))
					{
						goto IL_BB6;
					}
					flag2 = false;
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
					goto IL_BB6;
				}
			}
			else if (!(cmd2 == "fsg"))
			{
				goto IL_BB6;
			}
			int num2 = 1;
			if (flag)
			{
				text = ((cmdArgs.Length < 2) ? null : cmdArgs[1].ToLower());
				num2 = 2;
			}
			if (autofillData != null)
			{
				bool flag3 = rawArgs.EndsWith(" ") && cmdArgs.Length == (flag ? 1 : 0);
				if ((cmd == "tb" || flag) && (flag3 || cmdArgs.Length == (flag ? 2 : 1)))
				{
					string[] values8 = "view, list, season, scenario, end_offset, start_offset, active, dormant, ticket, reset_ticket, reset, wins, losses, reward".Split(new char[]
					{
						' ',
						','
					}, StringSplitOptions.RemoveEmptyEntries);
					return this.ProcessAutofillParam(values8, text, autofillData);
				}
				if (cmd == "fsg" && (flag3 || cmdArgs.Length == 1))
				{
					string[] values9 = "config, setconfig, tb, find, finalize, checkin, checkout, patrons".Split(new char[]
					{
						' ',
						','
					}, StringSplitOptions.RemoveEmptyEntries);
					return this.ProcessAutofillParam(values9, text, autofillData);
				}
				return false;
			}
			else
			{
				if (text == "help" || text == "view" || text == "list")
				{
					flag2 = false;
					goto IL_BB6;
				}
				if (!(text == "reset"))
				{
					goto IL_BB6;
				}
				flag2 = (((cmdArgs.Length < num2 + 1) ? null : cmdArgs[num2].ToLower()) != "help");
				goto IL_BB6;
			}
			IL_8A1:
			flag2 = false;
			if (cmd == "quest")
			{
				cmd = "achieves";
			}
			if (autofillData != null)
			{
				if ((rawArgs.EndsWith(" ") && cmdArgs.Length == 0) || cmdArgs.Length == 1)
				{
					string[] values10 = "cancel, resetdaily, resetreroll, grant, complete, progress, seasonroll, seasonreset".Split(new char[]
					{
						' ',
						','
					}, StringSplitOptions.RemoveEmptyEntries);
					return this.ProcessAutofillParam(values10, text, autofillData);
				}
				return false;
			}
			else
			{
				this.OnProcessCheat_util_achieves_ReplaceSlotWithAchieve(cmdArgs);
				int num3 = this.OnProcessCheat_util_achieves_GetAchievementFromArgs(cmdArgs);
				if (text == "grant")
				{
					global::Achievement achievement = AchieveManager.Get().GetAchievement(num3);
					if (achievement != null && AchieveManager.Get().GetActiveQuests(false).Count >= 3 && achievement.CanShowInQuestLog)
					{
						UIStatus.Get().AddInfo(string.Format("{0} {1}: Quest log is full.", func, cmd), 5f);
						return true;
					}
				}
				if (text == "cancel")
				{
					this.OnProcessCheat_util_achieves_ShowQuestLog();
					goto IL_BB6;
				}
				if (text == "resetdaily" || text == "resetreroll")
				{
					goto IL_BB6;
				}
				if (!(text == "grant") && !(text == "complete") && !(text == "progress"))
				{
					UIStatus.Get().AddInfo("USAGE: quest [subcmd] [subcmd args]\nCommands: grant, complete, progress, cancel, resetdaily\n Subcommands: achieve=[achieveId] (required for grant), slot=[slot#] (Either achieveId or slot required for complete, progress, cancel), amount=[X] (for progress only- optional), offset=[X] (in hours from current time, for resetdaily and resetreroll", 10f);
					return true;
				}
				this.OnProcessCheat_util_achieves_ShowQuestPopupsWhenAchieveUpdated(num3);
				goto IL_BB6;
			}
			IL_A12:
			flag2 = false;
			IL_BB6:
			if (autofillData != null)
			{
				return false;
			}
			AlertPopup.ResponseCallback responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response != AlertPopup.Response.CONFIRM && response != AlertPopup.Response.OK)
				{
					return;
				}
				DebugCommandRequest debugCommandRequest = new DebugCommandRequest();
				debugCommandRequest.Command = cmd;
				debugCommandRequest.Args.AddRange(cmdArgs);
				Network.Get().SendDebugCommandRequest(debugCommandRequest);
			};
			this.m_lastUtilServerCmd = args;
			if (flag2)
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
	}

	// Token: 0x060076C3 RID: 30403 RVA: 0x002688A0 File Offset: 0x00266AA0
	private void OnProcessCheat_utilservercmd_OnResponse()
	{
		DebugCommandResponse debugCommandResponse = Network.Get().GetDebugCommandResponse();
		bool flag = false;
		string text = "null response";
		string a = (this.m_lastUtilServerCmd == null || this.m_lastUtilServerCmd.Length == 0) ? "" : this.m_lastUtilServerCmd[0];
		string[] array = (this.m_lastUtilServerCmd == null) ? new string[0] : this.m_lastUtilServerCmd.Skip(1).ToArray<string>();
		string a2 = (array.Length == 0) ? null : array[0];
		string text2 = (array.Length < 2) ? null : array[1].ToLower();
		this.m_lastUtilServerCmd = null;
		if (debugCommandResponse != null)
		{
			flag = debugCommandResponse.Success;
			text = string.Format("{0} {1}", debugCommandResponse.Success ? "" : "FAILED:", debugCommandResponse.HasResponse ? debugCommandResponse.Response : "reply=<blank>");
		}
		global::Log.LogLevel level = flag ? global::Log.LogLevel.Info : global::Log.LogLevel.Error;
		global::Log.Net.Print(level, text, Array.Empty<object>());
		bool flag2 = true;
		float delay = 5f;
		if (flag)
		{
			bool flag3 = a == "fsg" && a2 == "tb";
			if (a == "tb" || flag3)
			{
				if (flag3)
				{
					a2 = text2;
					text2 = ((array.Length < 3) ? null : array[2].ToLower());
				}
				if (a2 == "scenario" || a2 == "scen" || a2 == "season" || a2 == "end_offset" || a2 == "start_offset" || a2 == "wins" || a2 == "losses" || a2 == "ticket" || (a2 == "reset" && text2 != "help"))
				{
					for (BrawlType brawlType = BrawlType.BRAWL_TYPE_TAVERN_BRAWL; brawlType < BrawlType.BRAWL_TYPE_COUNT; brawlType++)
					{
						TavernBrawlManager.Get().RefreshServerData(brawlType);
					}
				}
			}
			else if (a == "ranked")
			{
				if (a2 == "medal" || a2 == "seasonroll")
				{
					flag = (flag && (!debugCommandResponse.HasResponse || !debugCommandResponse.Response.StartsWith("Error")));
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
				if (a2 == "set" || a2 == "win" || a2 == "lose" || a2 == "games")
				{
					NetCache.Get().RefreshNetObject<NetCache.NetCacheMedalInfo>();
				}
			}
			else if (a == "hero")
			{
				if (a2 == "addxp")
				{
					NetCache.Get().RefreshNetObject<NetCache.NetCacheHeroLevels>();
				}
			}
			else if (a == "banner")
			{
				if (a2 == "reset")
				{
					NetCache.Get().ReloadNetObject<NetCache.NetCacheProfileProgress>();
					bool flag4 = false;
					int bannerId = 0;
					foreach (string text3 in array)
					{
						string[] array3 = (text3 == null) ? null : text3.Split(new char[]
						{
							'='
						});
						if (array3 != null && array3.Length >= 2 && (array3[0].Equals("banner", StringComparison.InvariantCultureIgnoreCase) || array3[0].Equals("bannerId", StringComparison.InvariantCultureIgnoreCase)))
						{
							flag4 = true;
							int.TryParse(array3[1], out bannerId);
						}
					}
					if (flag4)
					{
						BannerManager.Get().Cheat_ClearSeenBannersNewerThan(bannerId);
					}
					else
					{
						BannerManager.Get().Cheat_ClearSeenBanners();
					}
				}
			}
			else if (a == "returningplayer")
			{
				flag = (flag && (!debugCommandResponse.HasResponse || !debugCommandResponse.Response.StartsWith("Error")));
				if (flag)
				{
					ReturningPlayerMgr.Get().Cheat_ResetReturningPlayer();
					if (true)
					{
						text += "\nYou may want to log out/in to take effect.";
					}
				}
			}
			else if (a == "logrelay" && a2 == "*")
			{
				flag2 = false;
			}
			if ((a == "ranked" || a == "arena") && a2 == "reward")
			{
				flag = (flag && (!debugCommandResponse.HasResponse || !debugCommandResponse.Response.StartsWith("Error")));
				if (flag)
				{
					text = Cheats.Cheat_ShowRewardBoxes(text);
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
			if (a == "arena" && a2 == "season")
			{
				DraftManager.Get().RefreshCurrentSeasonFromServer();
			}
		}
		if (flag2)
		{
			if (flag)
			{
				UIStatus.Get().AddInfo(text, delay);
				return;
			}
			UIStatus.Get().AddError(text, -1f);
		}
	}

	// Token: 0x060076C4 RID: 30404 RVA: 0x00268D6C File Offset: 0x00266F6C
	private int OnProcessCheat_util_achieves_GetAchievementFromArgs(string[] args)
	{
		string text = args.FirstOrDefault((string x) => x.StartsWith("achieve="));
		if (text != null)
		{
			return Convert.ToInt32(text.Substring("achieve=".Length));
		}
		return 0;
	}

	// Token: 0x060076C5 RID: 30405 RVA: 0x00268DBC File Offset: 0x00266FBC
	private int OnProcessCheat_util_achieves_GetAchieveFromSlotId(int slotId)
	{
		List<global::Achievement> activeQuests = AchieveManager.Get().GetActiveQuests(false);
		if (slotId > 0 && slotId <= activeQuests.Count)
		{
			return activeQuests[slotId - 1].ID;
		}
		return 0;
	}

	// Token: 0x060076C6 RID: 30406 RVA: 0x00268DF4 File Offset: 0x00266FF4
	private void OnProcessCheat_util_achieves_ReplaceSlotWithAchieve(string[] args)
	{
		for (int i = 0; i < args.Length; i++)
		{
			if (args[i].StartsWith("slot=", true, CultureInfo.CurrentCulture))
			{
				int slotId = Convert.ToInt32(args[i].Substring("slot=".Length));
				int num = this.OnProcessCheat_util_achieves_GetAchieveFromSlotId(slotId);
				args[i] = string.Format("achieve={0}", num);
			}
		}
	}

	// Token: 0x060076C7 RID: 30407 RVA: 0x00268E58 File Offset: 0x00267058
	private void OnProcessCheat_util_achieves_ShowQuestPopupsWhenAchieveUpdated(int achieveId)
	{
		AchieveManager.AchievesUpdatedCallback action = null;
		Func<global::Achievement, bool> <>9__1;
		Func<global::Achievement, bool> <>9__2;
		AchieveManager.Get().RegisterAchievesUpdatedListener(action = delegate(List<global::Achievement> updatedAchieves, List<global::Achievement> completedAchieves, object userdata)
		{
			if (achieveId != 0)
			{
				Func<global::Achievement, bool> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = ((global::Achievement x) => x.ID == achieveId));
				}
				if (!updatedAchieves.Any(predicate))
				{
					Func<global::Achievement, bool> predicate2;
					if ((predicate2 = <>9__2) == null)
					{
						predicate2 = (<>9__2 = ((global::Achievement x) => x.ID == achieveId));
					}
					if (!completedAchieves.Any(predicate2))
					{
						return;
					}
				}
			}
			if (AchieveManager.Get().HasQuestsToShow(true))
			{
				WelcomeQuests.Show(UserAttentionBlocker.ALL, true, null, false);
			}
			else if (GameToastMgr.Get() != null)
			{
				GameToastMgr.Get().UpdateQuestProgressToasts();
			}
			AchieveManager.Get().RemoveAchievesUpdatedListener(action);
		}, null);
	}

	// Token: 0x060076C8 RID: 30408 RVA: 0x00268E9A File Offset: 0x0026709A
	private void OnProcessCheat_util_achieves_ShowQuestLog()
	{
		if (global::QuestLog.Get() != null && !global::QuestLog.Get().IsShown())
		{
			global::QuestLog.Get().Show();
		}
	}

	// Token: 0x060076C9 RID: 30409 RVA: 0x00268EC0 File Offset: 0x002670C0
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
		string text = (cmdArgs.Length < 1) ? null : cmdArgs[0].ToLower();
		string text2 = (cmdArgs.Length < 2) ? null : cmdArgs[1].ToLower();
		bool flag = rawArgs.EndsWith(" ");
		if ((text == null && flag) || (text != null && text2 == null && !flag))
		{
			string[] values = new string[]
			{
				"quest",
				"pool",
				"achieve",
				"track"
			};
			autoFillResult = this.ProcessAutofillParam(values, text, autofillData);
			return true;
		}
		if (text == null)
		{
			return false;
		}
		if (((text2 == null && flag) || (text2 != null && !flag)) && text == "quest")
		{
			string[] values2 = new string[]
			{
				"help",
				"view",
				"grant",
				"ack",
				"advance",
				"complete",
				"reset"
			};
			autoFillResult = this.ProcessAutofillParam(values2, text2, autofillData);
			return true;
		}
		if (((text2 == null && flag) || (text2 != null && !flag)) && text == "pool")
		{
			string[] values3 = new string[]
			{
				"help",
				"view",
				"grant",
				"login",
				"lastcheckdate",
				"reroll",
				"reset",
				"set",
				"testcalcnumquests",
				"testcalctimeuntil"
			};
			autoFillResult = this.ProcessAutofillParam(values3, text2, autofillData);
			return true;
		}
		if (((text2 == null && flag) || (text2 != null && !flag)) && text == "achieve")
		{
			string[] values4 = new string[]
			{
				"help",
				"view",
				"score",
				"advance",
				"complete",
				"claim",
				"ack",
				"reset"
			};
			autoFillResult = this.ProcessAutofillParam(values4, text2, autofillData);
			return true;
		}
		if (((text2 == null && flag) || (text2 != null && !flag)) && text == "track")
		{
			string[] values5 = new string[]
			{
				"help",
				"view",
				"set",
				"gamexp",
				"addxp",
				"levelup",
				"claim",
				"ack",
				"reset"
			};
			autoFillResult = this.ProcessAutofillParam(values5, text2, autofillData);
			return true;
		}
		return false;
	}

	// Token: 0x060076CA RID: 30410 RVA: 0x00269138 File Offset: 0x00267338
	private static string Cheat_ShowRewardBoxes(string parsableRewardBags)
	{
		if (SceneMgr.Get().IsInGame())
		{
			return "Cannot display reward boxes in gameplay.";
		}
		string[] array = parsableRewardBags.Trim().Split(new char[]
		{
			' '
		}, StringSplitOptions.RemoveEmptyEntries);
		if (array.Length < 2)
		{
			return "Error parsing reply, should start with 'Success:' then player_id: " + parsableRewardBags;
		}
		if (array.Length < 3)
		{
			return "No rewards returned by server: reply=" + parsableRewardBags;
		}
		List<NetCache.ProfileNotice> list = new List<NetCache.ProfileNotice>();
		array = array.Skip(1).ToArray<string>();
		for (int i = 0; i < array.Length; i++)
		{
			int num = 0;
			int num2 = i * 2;
			if (num2 >= array.Length)
			{
				break;
			}
			if (!int.TryParse(array[num2], out num))
			{
				return string.Concat(new object[]
				{
					"Reward at index ",
					num2,
					" (",
					array[num2],
					") is not an int: reply=",
					parsableRewardBags
				});
			}
			if (num != 0)
			{
				num2++;
				if (num2 >= array.Length)
				{
					return string.Concat(new object[]
					{
						"No reward bag data at index ",
						num2,
						": reply=",
						parsableRewardBags
					});
				}
				long num3 = 0L;
				if (!long.TryParse(array[num2], out num3))
				{
					return string.Concat(new object[]
					{
						"Reward Data at index ",
						num2,
						" (",
						array[num2],
						") is not a long int: reply=",
						parsableRewardBags
					});
				}
				NetCache.ProfileNotice profileNotice = null;
				TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
				switch (num)
				{
				case 1:
				case 12:
				case 14:
				case 15:
				case 24:
					profileNotice = new NetCache.ProfileNoticeRewardBooster
					{
						Id = (int)num3,
						Count = 1
					};
					break;
				case 2:
					profileNotice = new NetCache.ProfileNoticeRewardCurrency
					{
						CurrencyType = PegasusShared.CurrencyType.CURRENCY_TYPE_GOLD,
						Amount = (int)num3
					};
					break;
				case 3:
					profileNotice = new NetCache.ProfileNoticeRewardDust
					{
						Amount = (int)num3
					};
					break;
				case 4:
				case 5:
				case 6:
				case 7:
					goto IL_239;
				case 8:
				case 9:
				case 10:
				case 11:
					premium = TAG_PREMIUM.GOLDEN;
					goto IL_239;
				case 13:
					profileNotice = new NetCache.ProfileNoticeRewardCardBack
					{
						CardBackID = (int)num3
					};
					break;
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
				case 22:
				case 23:
					goto IL_26B;
				default:
					goto IL_26B;
				}
				IL_2C2:
				if (profileNotice != null)
				{
					list.Add(profileNotice);
					goto IL_2CE;
				}
				goto IL_2CE;
				IL_239:
				profileNotice = new NetCache.ProfileNoticeRewardCard
				{
					CardID = GameUtils.TranslateDbIdToCardId((int)num3, false),
					Premium = premium
				};
				goto IL_2C2;
				IL_26B:
				Debug.LogError(string.Concat(new object[]
				{
					"Unknown Reward Bag Type: ",
					num,
					" (data=",
					num3,
					") at index ",
					num2,
					": reply=",
					parsableRewardBags
				}));
				goto IL_2C2;
			}
			IL_2CE:;
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
			Processor.ScheduleCallback(secondsToWait, false, delegate(object userData)
			{
				Cheats.Cheat_ShowRewardBoxes(parsableRewardBags);
			}, null);
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
				if (UniversalInputManager.UsePhoneUI)
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
		AssetLoader.Get().InstantiatePrefab("RewardBoxes.prefab:f136fead3d6a148c6801f1e3bd2e8267", callback, rewards, AssetLoadingOptions.None);
		return null;
	}

	// Token: 0x060076CB RID: 30411 RVA: 0x000052EC File Offset: 0x000034EC
	private bool OnProcessCheat_gameservercmd(string func, string[] args, string rawArgs)
	{
		return true;
	}

	// Token: 0x060076CC RID: 30412 RVA: 0x002694AC File Offset: 0x002676AC
	private bool OnProcessCheat_rewardboxes(string func, string[] args, string rawArgs)
	{
		string.IsNullOrEmpty(args[0].ToLower());
		int num = 5;
		if (args.Length > 1)
		{
			GeneralUtils.TryParseInt(args[1], out num);
		}
		BoosterDbId[] array = (from BoosterDbId i in Enum.GetValues(typeof(BoosterDbId))
		where i > BoosterDbId.INVALID
		select i).ToArray<BoosterDbId>();
		BoosterDbId boosterDbId = array[UnityEngine.Random.Range(0, array.Length)];
		string text = Cheats.Cheat_ShowRewardBoxes("Success: 123456" + " " + 13 + " " + UnityEngine.Random.Range(1, 34) + " " + 1 + " " + (int)boosterDbId + " " + 3 + " " + UnityEngine.Random.Range(1, 31) * 5 + " " + 2 + " " + UnityEngine.Random.Range(1, 31) * 5 + " " + ((UnityEngine.Random.Range(0, 2) == 0) ? 6 : 10) + " " + GameUtils.TranslateCardIdToDbId("EX1_279", false));
		if (text != null)
		{
			UIStatus.Get().AddError(text, -1f);
		}
		return true;
	}

	// Token: 0x060076CD RID: 30413 RVA: 0x00269617 File Offset: 0x00267817
	private bool OnProcessCheat_rankrefresh(string func, string[] args, string rawArgs)
	{
		NetCache.Get().RegisterScreenEndOfGame(new NetCache.NetCacheCallback(this.OnNetCacheReady_CallRankChangeTwoScoopDebugShow));
		return true;
	}

	// Token: 0x060076CE RID: 30414 RVA: 0x00269630 File Offset: 0x00267830
	private void OnNetCacheReady_CallRankChangeTwoScoopDebugShow()
	{
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady_CallRankChangeTwoScoopDebugShow));
		RankChangeTwoScoop_NEW.DebugShowHelper(RankMgr.Get().GetLocalPlayerMedalInfo(), Options.GetFormatType());
	}

	// Token: 0x060076CF RID: 30415 RVA: 0x0026965C File Offset: 0x0026785C
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
			if (text == "winstreak" || text == "streak")
			{
				isWinStreak = true;
			}
			else if (text == "win")
			{
				showWin = true;
			}
			else if (text == "loss")
			{
				showWin = false;
			}
			else if (text == "wild")
			{
				formatType = FormatType.FT_WILD;
			}
			else if (text == "classic")
			{
				formatType = FormatType.FT_CLASSIC;
			}
			else if (text.StartsWith("x") || text.EndsWith("x"))
			{
				text = text.Trim(new char[]
				{
					'x'
				});
				starsPerWin = int.Parse(text);
			}
			else if (text.StartsWith("*") || text.EndsWith("*"))
			{
				text = text.Trim(new char[]
				{
					'*'
				});
				stars = int.Parse(text);
			}
		}
		RankChangeTwoScoop_NEW.DebugShowFake(leagueRankRecordByCheatName.LeagueId, leagueRankRecordByCheatName.StarLevel, stars, starsPerWin, formatType, isWinStreak, showWin);
		return true;
	}

	// Token: 0x060076D0 RID: 30416 RVA: 0x002697C8 File Offset: 0x002679C8
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
			string a = args[i].ToLower();
			if (a == "standard")
			{
				formatType = FormatType.FT_STANDARD;
			}
			else if (a == "wild")
			{
				formatType = FormatType.FT_WILD;
			}
			else if (a == "classic")
			{
				formatType = FormatType.FT_CLASSIC;
			}
			else if (a == "all")
			{
				flag = true;
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
		List<List<RewardData>> list = new List<List<RewardData>>();
		if (!medalInfoTranslator.GetRankedRewardsEarned(formatType, ref list) || list.Count == 0)
		{
			return false;
		}
		RankedRewardDisplay.DebugShowFake(leagueRankRecordByCheatName.LeagueId, leagueRankRecordByCheatName.StarLevel, formatType, list);
		return true;
	}

	// Token: 0x060076D1 RID: 30417 RVA: 0x002698FC File Offset: 0x00267AFC
	private bool OnProcessCheat_rankcardback(string func, string[] args, string rawArgs)
	{
		string cheatName = "bronze10";
		LeagueRankDbfRecord leagueRankRecordByCheatName = RankMgr.Get().GetLeagueRankRecordByCheatName(cheatName);
		if (leagueRankRecordByCheatName == null)
		{
			return false;
		}
		int num = 0;
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]) && !GeneralUtils.TryParseInt(args[0], out num))
		{
			UIStatus.Get().AddInfo("please enter a valid int value for 1st parameter <wins>");
			return true;
		}
		int num2 = 0;
		if (args.Length >= 2 && !GeneralUtils.TryParseInt(args[1], out num2))
		{
			UIStatus.Get().AddInfo("please enter a valid int value for 2nd parameter <season_id>");
			return true;
		}
		if (num2 == 0)
		{
			NetCache.NetCacheRewardProgress netObject = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>();
			if (netObject != null)
			{
				num2 = netObject.Season;
			}
		}
		MedalInfoTranslator medalInfoTranslator = MedalInfoTranslator.CreateMedalInfoForLeagueId(leagueRankRecordByCheatName.LeagueId, leagueRankRecordByCheatName.StarLevel, 1337);
		TranslatedMedalInfo previousMedal = medalInfoTranslator.GetPreviousMedal(FormatType.FT_STANDARD);
		TranslatedMedalInfo currentMedal = medalInfoTranslator.GetCurrentMedal(FormatType.FT_STANDARD);
		previousMedal.seasonWins = Mathf.Max(0, num - 1);
		currentMedal.seasonWins = num;
		currentMedal.seasonId = num2;
		RankedCardBackProgressDisplay.DebugShowFake(medalInfoTranslator);
		return true;
	}

	// Token: 0x060076D2 RID: 30418 RVA: 0x002699D8 File Offset: 0x00267BD8
	private bool OnProcessCheat_easyrank(string func, string[] args, string rawArgs)
	{
		string arg = args[0].ToLower();
		CheatMgr.Get().ProcessCheat(string.Format("util ranked set rank={0}", arg), false);
		return true;
	}

	// Token: 0x060076D3 RID: 30419 RVA: 0x00269A08 File Offset: 0x00267C08
	private bool OnProcessCheat_timescale(string func, string[] args, string rawArgs)
	{
		string text = args[0].ToLower();
		if (string.IsNullOrEmpty(text))
		{
			float timeScale = Time.timeScale;
			float devTimescaleMultiplier = SceneDebugger.GetDevTimescaleMultiplier();
			string message;
			if (timeScale == devTimescaleMultiplier)
			{
				message = string.Format("Current timeScale is: {0}", timeScale);
			}
			else
			{
				message = string.Format("Current timeScale is: {0}\nDev timescale: {1}\nGame timescale: {2}", timeScale, devTimescaleMultiplier, TimeScaleMgr.Get().GetGameTimeScale());
			}
			UIStatus.Get().AddInfo(message, 3f * SceneDebugger.GetDevTimescaleMultiplier());
			return true;
		}
		float num = 1f;
		if (!float.TryParse(text, out num))
		{
			return false;
		}
		SceneDebugger.SetDevTimescaleMultiplier(num);
		UIStatus.Get().AddInfo(string.Format("Setting timescale to: {0}", num), 3f * num);
		return true;
	}

	// Token: 0x060076D4 RID: 30420 RVA: 0x00269AC4 File Offset: 0x00267CC4
	private bool OnProcessCheat_reset(string func, string[] args, string rawArgs)
	{
		HearthstoneApplication.Get().Reset();
		return true;
	}

	// Token: 0x060076D5 RID: 30421 RVA: 0x00269AD1 File Offset: 0x00267CD1
	private bool OnProcessCheat_endturn(string func, string[] args, string rawArgs)
	{
		UIStatus.Get().AddError("Deprecated. Use \"cheat endturn\" instead.", -1f);
		return true;
	}

	// Token: 0x060076D6 RID: 30422 RVA: 0x00269AE8 File Offset: 0x00267CE8
	private bool OnProcessCheat_battlegrounds(string func, string[] args, string rawArgs)
	{
		if (SceneMgr.Get().IsInGame())
		{
			UIStatus.Get().AddError("Cannot queue for a battlegrounds game while in gameplay.", -1f);
			return true;
		}
		if (DialogManager.Get().ShowingDialog())
		{
			UIStatus.Get().AddError("Cannot queue for a battlegrounds game while a dialog is active.", -1f);
			return true;
		}
		GameMgr.Get().FindGame(GameType.GT_BATTLEGROUNDS, FormatType.FT_WILD, 3459, 0, 0L, null, null, false, null, GameType.GT_UNKNOWN);
		return true;
	}

	// Token: 0x060076D7 RID: 30423 RVA: 0x00269B5C File Offset: 0x00267D5C
	private bool OnProcessCheat_scenario(string func, string[] args, string rawArgs)
	{
		string[] array = new string[]
		{
			"id",
			"game_type",
			"deck_id",
			"format_type",
			"prog_override"
		};
		global::Map<string, Cheats.NamedParam> map;
		bool flag = this.TryParseNamedArgs(args, out map);
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			Cheats.NamedParam value;
			if (!map.TryGetValue(text, out value))
			{
				if (!flag && args.Length > i)
				{
					map.Add(text, new Cheats.NamedParam(args[i]));
				}
				else
				{
					global::Map<string, Cheats.NamedParam> map2 = map;
					string key = text;
					value = default(Cheats.NamedParam);
					map2.Add(key, value);
				}
			}
		}
		Cheats.NamedParam namedParam = map["id"];
		int num = 260;
		if (namedParam.HasNumber)
		{
			num = namedParam.Number;
			if (GameDbf.Scenario.GetRecord(num) == null)
			{
				global::Error.AddWarning("scenario Cheat Error", "Error reading a scenario id from \"{0}\"", new object[]
				{
					num
				});
				return false;
			}
		}
		Cheats.NamedParam namedParam2 = map["game_type"];
		GameType gameType = GameType.GT_VS_AI;
		if (namedParam2.HasNumber)
		{
			gameType = (GameType)namedParam2.Number;
			if (gameType == GameType.GT_UNKNOWN)
			{
				global::Error.AddWarning("scenario Cheat Error", "Error reading a game type from \"{0}\"", new object[]
				{
					gameType
				});
				return false;
			}
		}
		Cheats.NamedParam deckParam = map["deck_id"];
		CollectionDeck collectionDeck = null;
		if (deckParam.HasNumber)
		{
			collectionDeck = CollectionManager.Get().GetDeck((long)deckParam.Number);
		}
		if (deckParam.HasNumber && collectionDeck == null)
		{
			collectionDeck = (from x in CollectionManager.Get().GetDecks()
			where x.Value.Name.Equals(deckParam.Text, StringComparison.CurrentCultureIgnoreCase)
			select x).FirstOrDefault<KeyValuePair<long, CollectionDeck>>().Value;
			if (collectionDeck == null)
			{
				global::Error.AddWarning("scenario Cheat Error", "Error reading a deck id from \"{0}\"", new object[]
				{
					collectionDeck
				});
				return false;
			}
		}
		Cheats.NamedParam namedParam3 = map["format_type"];
		FormatType formatType = FormatType.FT_WILD;
		if (namedParam3.HasNumber)
		{
			formatType = (FormatType)namedParam3.Number;
			if (formatType == FormatType.FT_UNKNOWN)
			{
				global::Error.AddWarning("scenario Cheat Error", "Error reading a format type from \"{0}\"", new object[]
				{
					formatType
				});
				return false;
			}
		}
		Cheats.NamedParam namedParam4 = map["prog_override"];
		GameType gameType2 = GameType.GT_UNKNOWN;
		if (namedParam4.HasNumber)
		{
			gameType2 = (GameType)namedParam4.Number;
			if (gameType2 == GameType.GT_UNKNOWN)
			{
				global::Error.AddWarning("scenario Cheat Error", "Error reading a prog override from \"{0}\"", new object[]
				{
					gameType2
				});
				return false;
			}
		}
		Cheats.QuickLaunchAvailability quickLaunchAvailability = this.GetQuickLaunchAvailability();
		if (quickLaunchAvailability != Cheats.QuickLaunchAvailability.OK)
		{
			switch (quickLaunchAvailability)
			{
			case Cheats.QuickLaunchAvailability.FINDING_GAME:
				global::Error.AddDevWarning("scenario Cheat Error", "You are already finding a game.", Array.Empty<object>());
				break;
			case Cheats.QuickLaunchAvailability.ACTIVE_GAME:
				global::Error.AddDevWarning("scenario Cheat Error", "You are already in a game.", Array.Empty<object>());
				break;
			case Cheats.QuickLaunchAvailability.SCENE_TRANSITION:
				global::Error.AddDevWarning("scenario Cheat Error", "Can't start a game because a scene transition is active.", Array.Empty<object>());
				break;
			case Cheats.QuickLaunchAvailability.COLLECTION_NOT_READY:
				global::Error.AddDevWarning("scenario Cheat Error", "Can't start a game because your collection is not fully loaded.", Array.Empty<object>());
				break;
			default:
				global::Error.AddDevWarning("scenario Cheat Error", "Can't start a game: {0}", new object[]
				{
					quickLaunchAvailability
				});
				break;
			}
			return false;
		}
		this.LaunchQuickGame(num, gameType, formatType, collectionDeck, null, gameType2);
		return true;
	}

	// Token: 0x060076D8 RID: 30424 RVA: 0x00269E70 File Offset: 0x00268070
	private bool OnProcessCheat_aigame(string func, string[] args, string rawArgs)
	{
		int missionId = 3680;
		GameType gameType = GameType.GT_VS_AI;
		string text = args[0];
		if (string.IsNullOrEmpty(text))
		{
			global::Error.AddWarning("aigame Cheat Error", "No deck string supplied", Array.Empty<object>());
			return false;
		}
		if (ShareableDeck.Deserialize(text) == null)
		{
			global::Error.AddWarning("aigame Cheat Error", "Invalid deck string supplied \"{0}\"", new object[]
			{
				text
			});
			return false;
		}
		FormatType formatType = FormatType.FT_WILD;
		if (args.Length > 1)
		{
			string text2 = args[1];
			int num;
			if (int.TryParse(text2, out num))
			{
				formatType = (FormatType)num;
			}
			else if (!global::EnumUtils.TryGetEnum<FormatType>(text2, out formatType))
			{
				string a = text2.ToLower();
				if (!(a == "wild"))
				{
					if (!(a == "standard") && !(a == "std"))
					{
						global::Error.AddWarning("scenario Cheat Error", "Error reading a parameter for FormatType \"{0}\", please use \"wild\" or \"standard\"", new object[]
						{
							text2
						});
						return false;
					}
					formatType = FormatType.FT_STANDARD;
				}
				else
				{
					formatType = FormatType.FT_WILD;
				}
			}
		}
		Cheats.QuickLaunchAvailability quickLaunchAvailability = this.GetQuickLaunchAvailability();
		if (quickLaunchAvailability != Cheats.QuickLaunchAvailability.OK)
		{
			switch (quickLaunchAvailability)
			{
			case Cheats.QuickLaunchAvailability.FINDING_GAME:
				global::Error.AddDevWarning("scenario Cheat Error", "You are already finding a game.", Array.Empty<object>());
				break;
			case Cheats.QuickLaunchAvailability.ACTIVE_GAME:
				global::Error.AddDevWarning("scenario Cheat Error", "You are already in a game.", Array.Empty<object>());
				break;
			case Cheats.QuickLaunchAvailability.SCENE_TRANSITION:
				global::Error.AddDevWarning("scenario Cheat Error", "Can't start a game because a scene transition is active.", Array.Empty<object>());
				break;
			case Cheats.QuickLaunchAvailability.COLLECTION_NOT_READY:
				global::Error.AddDevWarning("scenario Cheat Error", "Can't start a game because your collection is not fully loaded.", Array.Empty<object>());
				break;
			default:
				global::Error.AddDevWarning("scenario Cheat Error", "Can't start a game: {0}", new object[]
				{
					quickLaunchAvailability
				});
				break;
			}
			return false;
		}
		this.LaunchQuickGame(missionId, gameType, formatType, null, text, GameType.GT_UNKNOWN);
		return true;
	}

	// Token: 0x060076D9 RID: 30425 RVA: 0x00269FFC File Offset: 0x002681FC
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
			global::Error.AddDevWarning("loadsnapshot Cheat Error", string.Format("Replay file {0}\nnot found!", text), Array.Empty<object>());
			return false;
		}
		byte[] array = File.ReadAllBytes(text);
		GameSnapshot gameSnapshot = new GameSnapshot();
		gameSnapshot.Deserialize(new MemoryStream(array));
		Cheats.QuickLaunchAvailability quickLaunchAvailability = this.GetQuickLaunchAvailability();
		if (quickLaunchAvailability != Cheats.QuickLaunchAvailability.OK)
		{
			switch (quickLaunchAvailability)
			{
			case Cheats.QuickLaunchAvailability.FINDING_GAME:
				global::Error.AddDevWarning("loadsnapshot Cheat Error", "You are already finding a game.", Array.Empty<object>());
				break;
			case Cheats.QuickLaunchAvailability.ACTIVE_GAME:
				global::Error.AddDevWarning("loadsnapshot Cheat Error", "You are already in a game.", Array.Empty<object>());
				break;
			case Cheats.QuickLaunchAvailability.SCENE_TRANSITION:
				global::Error.AddDevWarning("loadsnapshot Cheat Error", "Can't start a game because a scene transition is active.", Array.Empty<object>());
				break;
			case Cheats.QuickLaunchAvailability.COLLECTION_NOT_READY:
				global::Error.AddDevWarning("loadsnapshot Cheat Error", "Can't start a game because your collection is not fully loaded.", Array.Empty<object>());
				break;
			default:
				global::Error.AddDevWarning("loadsnapshot Cheat Error", "Can't start a game: {0}", new object[]
				{
					quickLaunchAvailability
				});
				break;
			}
			return false;
		}
		GameType gameType = gameSnapshot.GameType;
		FormatType formatType = gameSnapshot.FormatType;
		int scenarioId = gameSnapshot.ScenarioId;
		this.m_quickLaunchState.m_launching = true;
		string message = string.Format("Launching game from replay file\n{0}", text);
		UIStatus.Get().AddInfo(message);
		SceneMgr.Get().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		GameMgr.Get().SetPendingAutoConcede(true);
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		GameMgr.Get().FindGame(gameType, formatType, scenarioId, 0, 0L, null, null, false, array, GameType.GT_UNKNOWN);
		return true;
	}

	// Token: 0x060076DA RID: 30426 RVA: 0x0026A1A0 File Offset: 0x002683A0
	private bool OnProcessCheat_exportcards(string func, string[] args, string rawArgs)
	{
		SceneManager.LoadScene("ExportCards");
		return true;
	}

	// Token: 0x060076DB RID: 30427 RVA: 0x0026A1AD File Offset: 0x002683AD
	private bool OnProcessCheat_exportcardbacks(string func, string[] args, string rawArgs)
	{
		SceneManager.LoadScene("ExportCardBacks");
		return true;
	}

	// Token: 0x060076DC RID: 30428 RVA: 0x0026A1BA File Offset: 0x002683BA
	private bool OnProcessCheat_freeyourmind(string func, string[] args, string rawArgs)
	{
		this.m_isNewCardInPackOpeningEnabled = true;
		return true;
	}

	// Token: 0x060076DD RID: 30429 RVA: 0x0026A1C4 File Offset: 0x002683C4
	private bool OnProcessCheat_reloadgamestrings(string func, string[] args, string rawArgs)
	{
		GameStrings.ReloadAll();
		return true;
	}

	// Token: 0x060076DE RID: 30430 RVA: 0x0026A1CC File Offset: 0x002683CC
	private bool OnProcessCheat_userattentionmanager(string func, string[] args, string rawArgs)
	{
		string arg = UserAttentionManager.DumpUserAttentionBlockers("OnProcessCheat_userattentionmanager");
		UIStatus.Get().AddInfo(string.Format("Current UserAttentionBlockers: {0}", arg));
		return true;
	}

	// Token: 0x060076DF RID: 30431 RVA: 0x0026A1FC File Offset: 0x002683FC
	private void Cheat_ShowBannerList()
	{
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = true;
		foreach (BannerDbfRecord bannerDbfRecord in from r in GameDbf.Banner.GetRecords()
		orderby r.ID descending
		select r)
		{
			if (!flag)
			{
				stringBuilder.Append("\n");
			}
			flag = false;
			stringBuilder.AppendFormat("{0}. {1}", bannerDbfRecord.ID, bannerDbfRecord.NoteDesc);
		}
		UIStatus.Get().AddInfo(stringBuilder.ToString(), 5f);
	}

	// Token: 0x060076E0 RID: 30432 RVA: 0x0026A2B8 File Offset: 0x002684B8
	private bool OnProcessCheat_banner(string func, string[] args, string rawArgs)
	{
		int num = 0;
		if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
		{
			num = GameDbf.Banner.GetRecords().Max((BannerDbfRecord r) => r.ID);
		}
		else if (int.TryParse(args[0], out num))
		{
			if (GameDbf.Banner.GetRecord(num) == null)
			{
				UIStatus.Get().AddInfo(string.Format("Unknown bannerId: {0}", num));
				return true;
			}
		}
		else
		{
			if (args[0].Equals("list", StringComparison.InvariantCultureIgnoreCase))
			{
				this.Cheat_ShowBannerList();
				return true;
			}
			UIStatus.Get().AddInfo(string.Format("Unknown parameter: {0}", args[0]));
			return true;
		}
		BannerManager.Get().ShowBanner(num, null);
		return true;
	}

	// Token: 0x060076E1 RID: 30433 RVA: 0x0026A37C File Offset: 0x0026857C
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
				int rafprogress = Convert.ToInt32(args[1]);
				RAFManager.Get().SetRAFProgress(rafprogress);
			}
		}
		else if (string.Equals(a, "showglows"))
		{
			Options.Get().SetBool(global::Option.HAS_SEEN_RAF, false);
			Options.Get().SetBool(global::Option.HAS_SEEN_RAF_RECRUIT_URL, false);
			FriendListFrame friendListFrame = ChatMgr.Get().FriendListFrame;
			if (friendListFrame != null)
			{
				friendListFrame.UpdateRAFButtonGlow();
			}
			RAFFrame rafframe = RAFManager.Get().GetRAFFrame();
			if (rafframe != null)
			{
				rafframe.UpdateRecruitFriendsButtonGlow();
			}
			RAFManager.Get().ShowRAFProgressFrame();
		}
		return true;
	}

	// Token: 0x060076E2 RID: 30434 RVA: 0x0026A464 File Offset: 0x00268664
	private bool OnProcessCheat_returningplayer(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1)
		{
			global::Error.AddWarning("returningplayer Cheat Error", "No parameter provided.", Array.Empty<object>());
		}
		int progress;
		if (!GeneralUtils.TryParseInt(args[0], out progress))
		{
			global::Error.AddWarning("returningplayer Cheat Error", "Error reading an int from \"{0}\"", new object[]
			{
				args[0]
			});
			return false;
		}
		ReturningPlayerMgr.Get().Cheat_SetReturningPlayerProgress(progress);
		return true;
	}

	// Token: 0x060076E3 RID: 30435 RVA: 0x0026A4C0 File Offset: 0x002686C0
	private bool OnProcessCheat_setrotationevent(string func, string[] args, string rawArgs)
	{
		int num = 0;
		int delay = 1;
		bool flag = false;
		if (!string.IsNullOrEmpty(args[num]) && !GeneralUtils.TryParseBool(args[num], out flag))
		{
			UIStatus.Get().AddError("please enter a valid bool value for 1st parameter <bypass intro>", -1f);
			return true;
		}
		num++;
		if (args.Length > num && !string.IsNullOrEmpty(args[num]) && !GeneralUtils.TryParseInt(args[num], out delay))
		{
			UIStatus.Get().AddError("please enter a valid int value for 2nd parameter <delay>", -1f);
			return true;
		}
		bool forceShowSetRotationIntro = !flag;
		Processor.RunCoroutine(this.TriggerSetRotationInSeconds(delay, forceShowSetRotationIntro), null);
		return true;
	}

	// Token: 0x060076E4 RID: 30436 RVA: 0x0026A54A File Offset: 0x0026874A
	private IEnumerator TriggerSetRotationInSeconds(int delay, bool forceShowSetRotationIntro)
	{
		SetRotationManager.Get().Cheat_OverrideSetRotationDate(DateTime.Now.AddSeconds((double)delay), forceShowSetRotationIntro);
		while (delay > 0)
		{
			UIStatus.Get().AddInfo(string.Format("Set Rotation occurs in {0}...", delay));
			int num = delay;
			delay = num - 1;
			yield return new WaitForSeconds(1f);
		}
		UIStatus.Get().AddInfo(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021 + " has occurred!");
		yield break;
	}

	// Token: 0x060076E5 RID: 30437 RVA: 0x0026A560 File Offset: 0x00268760
	private bool OnProcessCheat_rankdebug(string func, string[] args, string rawArgs)
	{
		if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
		{
			UIStatus.Get().AddError("rankdebug cheat must have the following args:\nshow [standard/wild]\noff", -1f);
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
					Options.Get().SetEnum<RankDebugOption>(global::Option.RANK_DEBUG, RankDebugOption.WILD);
					return true;
				}
				if (string.Equals(a, "classic"))
				{
					Options.Get().SetEnum<RankDebugOption>(global::Option.RANK_DEBUG, RankDebugOption.CLASSIC);
					return true;
				}
				if (!string.Equals(a, "standard"))
				{
					UIStatus.Get().AddError("rankdebug error: Unknown league, please specify [standard/wild]", -1f);
					return true;
				}
			}
			Options.Get().SetEnum<RankDebugOption>(global::Option.RANK_DEBUG, RankDebugOption.STANDARD);
		}
		else if (string.Equals(a, "off"))
		{
			Options.Get().SetEnum<RankDebugOption>(global::Option.RANK_DEBUG, RankDebugOption.OFF);
		}
		else
		{
			UIStatus.Get().AddError("rankdebug error: Unknown argument", -1f);
		}
		return true;
	}

	// Token: 0x060076E6 RID: 30438 RVA: 0x0026A654 File Offset: 0x00268854
	private bool OnProcessCheat_resetrankedintro(string func, string[] args, string rawArgs)
	{
		List<GameSaveDataManager.SubkeySaveRequest> requests = new List<GameSaveDataManager.SubkeySaveRequest>
		{
			new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_INTRO_SEEN_COUNT, new long[1]),
			new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_SEASON_BONUS_STARS_POPUP_SEEN, new long[1]),
			new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_BONUS_STARS_POPUP_SEEN_COUNT, new long[1]),
			new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.RANKED_PLAY, GameSaveKeySubkeyId.RANKED_PLAY_LAST_REWARDS_VERSION_SEEN, new long[1])
		};
		if (GameSaveDataManager.Get().SaveSubkeys(requests, null))
		{
			UIStatus.Get().AddInfo("Ranked intro game save data keys reset.");
			return true;
		}
		UIStatus.Get().AddInfo("Failed to reset ranked intro game save data keys!");
		return false;
	}

	// Token: 0x060076E7 RID: 30439 RVA: 0x0026A704 File Offset: 0x00268904
	private bool OnProcessCheat_advevent(string func, string[] args, string rawArgs)
	{
		if (AdventureScene.Get() == null || AdventureMissionDisplay.Get() == null || SceneMgr.Get().GetMode() != SceneMgr.Mode.ADVENTURE)
		{
			UIStatus.Get().AddError("You must be viewing an Adventure to use this cheat!", -1f);
			return true;
		}
		if (args.Length < 1 || string.IsNullOrEmpty(args[0]))
		{
			UIStatus.Get().AddError("You must provide an event from AdventureWingEventTable as a parameter!", -1f);
			return true;
		}
		if (AdventureMissionDisplay.Get().Cheat_AdventureEvent(args[0]))
		{
			UIStatus.Get().AddInfo(string.Format("Triggered event {0} on each wing's AdventureWingEventTable.", args[0]));
		}
		else
		{
			UIStatus.Get().AddInfo("Could not activate cheat 'advevent', perhaps 'advdev' has not been enabled yet?");
		}
		return true;
	}

	// Token: 0x060076E8 RID: 30440 RVA: 0x0026A7AD File Offset: 0x002689AD
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

	// Token: 0x060076E9 RID: 30441 RVA: 0x0026A7D4 File Offset: 0x002689D4
	private bool OnProcessCheat_mobile(string func, string[] args, string rawArgs)
	{
		string a = args[0].ToLower();
		if (string.Equals(a, "login"))
		{
			if (args.Length > 1 && string.Equals(args[1].ToLower(), "clear"))
			{
				ILoginService loginService = HearthstoneServices.Get<ILoginService>();
				if (loginService != null)
				{
					loginService.ClearAuthentication();
				}
				if (loginService != null)
				{
					loginService.ClearAllSavedAccounts();
				}
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
				if (response != AlertPopup.Response.CANCEL && this.DownloadManager != null)
				{
					this.DownloadManager.DeleteDownloadedData();
				}
			};
			DialogManager.Get().ShowPopup(popupInfo);
		}
		return true;
	}

	// Token: 0x060076EA RID: 30442 RVA: 0x0026A921 File Offset: 0x00268B21
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

	// Token: 0x060076EB RID: 30443 RVA: 0x0026A944 File Offset: 0x00268B44
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
				string message = string.Format("resetrotationtutorial: {0} is not a valid parameter!", text);
				UIStatus.Get().AddError(message, -1f);
				return true;
			}
		}
		if (flag)
		{
			Options.Get().SetBool(global::Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, false);
			Options.Get().SetInt(global::Option.SET_ROTATION_INTRO_PROGRESS, 0);
			Options.Get().SetInt(global::Option.SET_ROTATION_INTRO_PROGRESS_NEW_PLAYER, 0);
			Options.Get().SetBool(global::Option.NEEDS_TO_MAKE_STANDARD_DECK, true);
		}
		else
		{
			Options.Get().SetBool(global::Option.HAS_SEEN_STANDARD_MODE_TUTORIAL, true);
			Options.Get().SetInt(global::Option.SET_ROTATION_INTRO_PROGRESS, 5);
			Options.Get().SetInt(global::Option.SET_ROTATION_INTRO_PROGRESS_NEW_PLAYER, 5);
		}
		Options.Get().SetBool(global::Option.DISABLE_SET_ROTATION_INTRO, false);
		string message2 = string.Format("Set Rotation tutorial progress reset as a {0}!\nReset disableSetRotationIntro to false. Restart client to trigger the flow.", flag ? "newbie" : "veteran");
		UIStatus.Get().AddInfo(message2);
		return true;
	}

	// Token: 0x060076EC RID: 30444 RVA: 0x0026AA4C File Offset: 0x00268C4C
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

	// Token: 0x060076ED RID: 30445 RVA: 0x0026AABC File Offset: 0x00268CBC
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
				UIStatus.Get().AddInfo(string.Concat(new string[]
				{
					"Cloud Storage Set: (",
					text,
					", ",
					text2,
					")"
				}));
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
			Options.Get().SetBool(global::Option.DISALLOWED_CLOUD_STORAGE, false);
			UIStatus.Get().AddInfo("Cloud Storage Disallow Reset!");
		}
		return true;
	}

	// Token: 0x060076EE RID: 30446 RVA: 0x0026ABC4 File Offset: 0x00268DC4
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
				TemporaryAccountManager.Get().ShowHealUpDialog(GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_HEADER_01"), GameStrings.Get("GLUE_TEMPORARY_ACCOUNT_DIALOG_BODY_03"), TemporaryAccountManager.HealUpReason.UNKNOWN, true, null);
			}
		}
		else if (string.Equals(a, "cheat"))
		{
			if (args.Length > 1)
			{
				string a2 = args[1].ToLower();
				if (string.Equals(a2, "on"))
				{
					Options.Get().SetBool(global::Option.IS_TEMPORARY_ACCOUNT_CHEAT, true);
					UIStatus.Get().AddInfo("Temporary Account CHEAT is now ON");
				}
				else if (string.Equals(a2, "off"))
				{
					Options.Get().SetBool(global::Option.IS_TEMPORARY_ACCOUNT_CHEAT, false);
					UIStatus.Get().AddInfo("Temporary Account CHEAT is now OFF");
				}
				else if (string.Equals(a2, "clear"))
				{
					Options.Get().DeleteOption(global::Option.IS_TEMPORARY_ACCOUNT_CHEAT);
					UIStatus.Get().AddInfo("Temporary Account CHEAT is now CLEARED");
				}
			}
		}
		else if (string.Equals(a, "status"))
		{
			string text = "Temporary Account status is " + (BattleNet.IsHeadlessAccount() ? "ON" : "OFF") + " Cheat is ";
			if (Options.Get().HasOption(global::Option.IS_TEMPORARY_ACCOUNT_CHEAT))
			{
				text += (Options.Get().GetBool(global::Option.IS_TEMPORARY_ACCOUNT_CHEAT) ? "ON" : "OFF");
			}
			else
			{
				text += "CLEARED";
			}
			UIStatus.Get().AddInfo(text);
		}
		else if (string.Equals(a, "tutorial"))
		{
			if (args.Length > 1)
			{
				string a3 = args[1].ToLower();
				if (string.Equals(a3, "skip"))
				{
					Options.Get().SetBool(global::Option.CONNECT_TO_AURORA, true);
					Options.Get().SetEnum<TutorialProgress>(global::Option.LOCAL_TUTORIAL_PROGRESS, TutorialProgress.ILLIDAN_COMPLETE);
					UIStatus.Get().AddInfo("Set to Skip No Account Tutorial");
				}
				else if (string.Equals(a3, "reset"))
				{
					Options.Get().SetBool(global::Option.CONNECT_TO_AURORA, false);
					Options.Get().SetEnum<TutorialProgress>(global::Option.LOCAL_TUTORIAL_PROGRESS, TutorialProgress.NOTHING_COMPLETE);
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
			TemporaryAccountManager.Get().ShowSwitchAccountMenu(null, false);
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
					global::Log.TemporaryAccount.Print(text2, Array.Empty<object>());
					UIStatus.Get().AddInfo(text2);
				}
				else if (string.Equals(a5, "clear"))
				{
					Options.Get().DeleteOption(global::Option.LAST_HEAL_UP_EVENT_DATE);
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
			ILoginService loginService = HearthstoneServices.Get<ILoginService>();
			if (loginService != null)
			{
				loginService.ClearAuthentication();
			}
			TemporaryAccountManager.Get().DeleteTemporaryAccountData();
			Options.Get().SetBool(global::Option.CONNECT_TO_AURORA, true);
			Options.Get().SetEnum<TutorialProgress>(global::Option.LOCAL_TUTORIAL_PROGRESS, TutorialProgress.ILLIDAN_COMPLETE);
		}
		return true;
	}

	// Token: 0x060076EF RID: 30447 RVA: 0x0026AFEC File Offset: 0x002691EC
	private bool OnProcessCheat_arena(string func, string[] args, string rawArgs)
	{
		string text = (args.Length >= 1) ? args[0] : null;
		string text2 = (args.Length >= 2) ? args[1] : null;
		string text3 = (args.Length >= 3) ? args[2] : null;
		float delay = 5f * Time.timeScale;
		if (string.IsNullOrEmpty(text) || text == "help")
		{
			string message;
			if (!(text2 == "popup"))
			{
				if (!(text2 == "refresh"))
				{
					message = "Valid arena commands: popup refresh\n\nUse 'util arena' to execute cheats on server, e.g. 'util arena season x' to switch season to x.";
				}
				else
				{
					message = "refreshes Arena season info from server";
				}
			}
			else
			{
				message = "Valid arena popup args: clear, comingsoon [#days], endingsoon [#days]";
			}
			UIStatus.Get().AddInfo(message, delay);
			return true;
		}
		string text4 = null;
		if (text == "popup")
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
			if (num <= 923577301U)
			{
				if (num <= 204816741U)
				{
					if (num != 0U)
					{
						if (num != 204816741U)
						{
							goto IL_38A;
						}
						if (!(text2 == "clearpopups"))
						{
							goto IL_38A;
						}
						goto IL_1ED;
					}
					else if (text2 != null)
					{
						goto IL_38A;
					}
				}
				else if (num != 873244444U)
				{
					if (num != 923577301U)
					{
						goto IL_38A;
					}
					if (!(text2 == "2"))
					{
						goto IL_38A;
					}
					goto IL_267;
				}
				else
				{
					if (!(text2 == "1"))
					{
						goto IL_38A;
					}
					goto IL_22D;
				}
			}
			else if (num <= 1550717474U)
			{
				if (num != 946971642U)
				{
					if (num != 1550717474U)
					{
						goto IL_38A;
					}
					if (!(text2 == "clear"))
					{
						goto IL_38A;
					}
					goto IL_1ED;
				}
				else if (!(text2 == "help"))
				{
					goto IL_38A;
				}
			}
			else if (num != 2447210329U)
			{
				if (num != 3891175137U)
				{
					if (num != 4093263549U)
					{
						goto IL_38A;
					}
					if (!(text2 == "comingsoon"))
					{
						goto IL_38A;
					}
					goto IL_22D;
				}
				else
				{
					if (!(text2 == "clearseen"))
					{
						goto IL_38A;
					}
					goto IL_1ED;
				}
			}
			else
			{
				if (!(text2 == "endingsoon"))
				{
					goto IL_38A;
				}
				goto IL_267;
			}
			UIStatus.Get().AddInfo("Valid arena popup args: clear, comingsoon [#days], endingsoon [#days]", delay);
			return true;
			IL_1ED:
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
			goto IL_38A;
			IL_22D:
			double num2;
			if (!double.TryParse(text3, out num2))
			{
				num2 = 13.0;
			}
			DraftManager.Get().ShowArenaPopup_SeasonComingSoon((long)(num2 * 86400.0), null);
			text4 = string.Empty;
			goto IL_38A;
			IL_267:
			double num3;
			if (!double.TryParse(text3, out num3))
			{
				num3 = 5.0;
			}
			DraftManager.Get().ShowArenaPopup_SeasonEndingSoon((long)(num3 * 86400.0), null);
			text4 = string.Empty;
		}
		else if (text == "refresh")
		{
			DraftManager.Get().RefreshCurrentSeasonFromServer();
			text4 = "Refreshing Arena season info from server.";
		}
		else if (text == "season")
		{
			text4 = string.Format("Please use 'util arena {0}' instead.", rawArgs);
		}
		else if (text == "choices")
		{
			List<string> list = new List<string>();
			for (int i = 1; i < args.Length; i++)
			{
				list.Add(args[i]);
			}
			string[] array;
			if (this.TryParseArenaChoices(list.ToArray(), out array))
			{
				List<string> list2 = new List<string>();
				list2.Add("arena");
				list2.Add("choices");
				foreach (string item in array)
				{
					list2.Add(item);
				}
				this.OnProcessCheat_utilservercmd("util", list2.ToArray(), rawArgs, null);
			}
			text4 = string.Empty;
		}
		IL_38A:
		NetCache.Get().DispatchClientOptionsToServer();
		if (text4 == null)
		{
			text4 = string.Format("Unknown subcmd: {0}", rawArgs);
		}
		UIStatus.Get().AddInfo(text4, delay);
		return true;
	}

	// Token: 0x060076F0 RID: 30448 RVA: 0x0026B3AC File Offset: 0x002695AC
	private bool OnProcessCheat_EventTiming(string func, string[] args, string rawArgs, AutofillData autofillData)
	{
		args = (from a in args
		where !string.IsNullOrEmpty(a.Trim())
		select a).ToArray<string>();
		if (autofillData != null)
		{
			List<string> list = (from e in SpecialEventManager.Get().AllKnownEvents
			select SpecialEventManager.Get().GetName(e)).ToList<string>();
			if (args.Length <= 1)
			{
				list.InsertRange(0, new string[]
				{
					"list",
					"listall",
					"help"
				});
			}
			return this.ProcessAutofillParam(list, (args.Length == 0) ? string.Empty : args.Last<string>(), autofillData);
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
		foreach (string text in args)
		{
			if (text == "list")
			{
				flag3 = true;
			}
			else if (text == "listall")
			{
				flag3 = true;
				list2.AddRange(SpecialEventManager.Get().AllKnownEvents);
				flag2 = false;
			}
			else
			{
				string text2 = text;
				if (text.StartsWith("event=") && text.Length > 6)
				{
					text2 = text.Substring(6);
				}
				Func<string, string, bool> fnSubstringMatch = (string evtName, string userInput) => evtName.Contains(userInput, StringComparison.InvariantCultureIgnoreCase);
				Func<string, string, bool> fnStartsWithMatch = (string evtName, string userInput) => evtName.StartsWith(userInput, StringComparison.InvariantCultureIgnoreCase);
				Func<string, string, bool> fnEndsWithMatch = (string evtName, string userInput) => evtName.EndsWith(userInput, StringComparison.InvariantCultureIgnoreCase);
				Func<string, string, bool> fnExactMatch = (string evtName, string userInput) => evtName.Equals(userInput, StringComparison.InvariantCultureIgnoreCase);
				string[] names = text2.Split(new char[]
				{
					','
				});
				Func<string, bool> fnIsMatch = (string evtName) => names.Any(delegate(string userInput)
				{
					Func<string, string, bool> func2 = fnSubstringMatch;
					bool flag5 = false;
					bool flag6 = false;
					if (userInput.StartsWith("^"))
					{
						userInput = userInput.Substring(1);
						flag5 = true;
					}
					if (userInput.EndsWith("$"))
					{
						userInput = userInput.Substring(0, userInput.Length - 1);
						flag6 = true;
					}
					if (userInput.Length == 0)
					{
						return false;
					}
					if (flag5 && flag6)
					{
						func2 = fnExactMatch;
					}
					else if (flag5)
					{
						func2 = fnStartsWithMatch;
					}
					else if (flag6)
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
		}
		if (flag2)
		{
			list2 = (from e in SpecialEventManager.Get().AllKnownEvents
			where SpecialEventManager.Get().IsEventActive(e, false)
			select e).ToList<SpecialEventType>();
			flag = true;
		}
		DateTime utcNow = DateTime.UtcNow;
		if (flag)
		{
			list2.RemoveAll(delegate(SpecialEventType e)
			{
				DateTime? eventStartTimeUtc2 = SpecialEventManager.Get().GetEventStartTimeUtc(e);
				DateTime? eventEndTimeUtc2 = SpecialEventManager.Get().GetEventEndTimeUtc(e);
				TimeSpan timeSpan3 = (eventStartTimeUtc2 != null) ? ((eventStartTimeUtc2.Value > utcNow) ? (eventStartTimeUtc2.Value - utcNow) : (utcNow - eventStartTimeUtc2.Value)) : TimeSpan.MaxValue;
				TimeSpan timeSpan4 = (eventEndTimeUtc2 != null) ? ((eventEndTimeUtc2.Value > utcNow) ? (eventEndTimeUtc2.Value - utcNow) : (utcNow - eventEndTimeUtc2.Value)) : TimeSpan.MaxValue;
				bool flag5 = timeSpan3.TotalDays <= 120.0;
				bool flag6 = timeSpan4.TotalDays <= 120.0;
				return !flag5 && !flag6;
			});
		}
		if (list2.Count <= 0)
		{
			UIStatus.Get().AddInfoNoRichText("No events to show (check event names).", -1f);
			return true;
		}
		list2.Sort(delegate(SpecialEventType lhs, SpecialEventType rhs)
		{
			bool flag5 = SpecialEventManager.Get().IsEventActive(lhs, false);
			bool flag6 = SpecialEventManager.Get().IsEventActive(rhs, false);
			if (flag5 != flag6)
			{
				if (!flag5)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				DateTime? eventStartTimeUtc2 = SpecialEventManager.Get().GetEventStartTimeUtc(lhs);
				DateTime? eventStartTimeUtc3 = SpecialEventManager.Get().GetEventStartTimeUtc(rhs);
				if (eventStartTimeUtc2 != eventStartTimeUtc3)
				{
					if (eventStartTimeUtc2 == null)
					{
						return -1;
					}
					if (eventStartTimeUtc3 != null)
					{
						return eventStartTimeUtc2.Value.CompareTo(eventStartTimeUtc3.Value);
					}
					return 1;
				}
				else
				{
					DateTime? eventEndTimeUtc2 = SpecialEventManager.Get().GetEventEndTimeUtc(lhs);
					DateTime? eventEndTimeUtc3 = SpecialEventManager.Get().GetEventEndTimeUtc(rhs);
					if (!(eventEndTimeUtc2 != eventEndTimeUtc3))
					{
						string name = SpecialEventManager.Get().GetName(lhs);
						string name2 = SpecialEventManager.Get().GetName(rhs);
						return name.CompareTo(name2);
					}
					if (eventEndTimeUtc2 == null)
					{
						return 1;
					}
					if (eventEndTimeUtc3 != null)
					{
						return eventEndTimeUtc2.Value.CompareTo(eventEndTimeUtc3.Value);
					}
					return -1;
				}
			}
		});
		StringBuilder stringBuilder = new StringBuilder();
		foreach (SpecialEventType eventType in list2)
		{
			if (flag3)
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(SpecialEventManager.Get().GetName(eventType));
			}
			else
			{
				bool flag4 = SpecialEventManager.Get().IsEventActive(eventType, false);
				DateTime? eventStartTimeUtc = SpecialEventManager.Get().GetEventStartTimeUtc(eventType);
				DateTime? eventEndTimeUtc = SpecialEventManager.Get().GetEventEndTimeUtc(eventType);
				DateTime? dateTime = eventStartTimeUtc;
				DateTime? dateTime2 = eventEndTimeUtc;
				if (dateTime != null)
				{
					dateTime = new DateTime?(dateTime.Value.AddSeconds((double)SpecialEventManager.Get().DevTimeOffsetSeconds).ToLocalTime());
				}
				if (dateTime2 != null)
				{
					dateTime2 = new DateTime?(dateTime2.Value.AddSeconds((double)SpecialEventManager.Get().DevTimeOffsetSeconds).ToLocalTime());
				}
				if (stringBuilder.Length != 0)
				{
					stringBuilder.Append("\n");
				}
				string text3 = (dateTime != null) ? dateTime.Value.ToString("yyyy/MM/dd") : "<always>";
				string text4 = (dateTime2 != null) ? dateTime2.Value.ToString("yyyy/MM/dd") : "<forever>";
				stringBuilder.AppendFormat("{0} {1} {2}-{3}", new object[]
				{
					SpecialEventManager.Get().GetName(eventType),
					flag4 ? "Active" : "Inactive",
					text3,
					text4
				});
				if (flag4)
				{
					TimeSpan? timeSpan = (eventEndTimeUtc == null || eventEndTimeUtc.Value < utcNow) ? null : new TimeSpan?(eventEndTimeUtc.Value - utcNow);
					if (timeSpan != null && timeSpan.Value.TotalDays < 3.0)
					{
						stringBuilder.AppendFormat(" ends in {0}", global::TimeUtils.GetElapsedTimeString((int)timeSpan.Value.TotalSeconds, global::TimeUtils.SPLASHSCREEN_DATETIME_STRINGSET, true));
					}
				}
				else
				{
					TimeSpan? timeSpan2 = (eventStartTimeUtc == null || eventStartTimeUtc.Value < utcNow) ? null : new TimeSpan?(eventStartTimeUtc.Value - utcNow);
					if (timeSpan2 != null && timeSpan2.Value.TotalDays < 3.0)
					{
						stringBuilder.AppendFormat(" starts in {0}", global::TimeUtils.GetElapsedTimeString((int)timeSpan2.Value.TotalSeconds, global::TimeUtils.SPLASHSCREEN_DATETIME_STRINGSET, true));
					}
				}
			}
		}
		stringBuilder.Append("\n");
		float delay = (float)Mathf.Max(5, 2 * Mathf.Min(20, list2.Count)) * Time.timeScale;
		string text5 = stringBuilder.ToString();
		global::Log.EventTiming.PrintInfo(text5, Array.Empty<object>());
		UIStatus.Get().AddInfoNoRichText(text5, delay);
		return true;
	}

	// Token: 0x060076F1 RID: 30449 RVA: 0x0026BA54 File Offset: 0x00269C54
	private bool OnProcessCheat_UpdateIntention(string func, string[] args, string rawArgs)
	{
		Options.Get().SetInt(global::Option.UPDATE_STATE, int.Parse(args[0]));
		return true;
	}

	// Token: 0x060076F2 RID: 30450 RVA: 0x0026BA6C File Offset: 0x00269C6C
	private bool OnProcessCheat_autoexportgamestate(string func, string[] args, string rawArgs)
	{
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			return false;
		}
		string arg = string.IsNullOrEmpty(args[0]) ? "GameStateExportFile" : args[0];
		JsonNode jsonNode = new JsonNode();
		foreach (KeyValuePair<int, global::Player> keyValuePair in GameState.Get().GetPlayerMap())
		{
			string key = "Player" + keyValuePair.Key;
			JsonNode jsonNode2 = new JsonNode();
			jsonNode.Add(key, jsonNode2);
			jsonNode2["Hero"] = this.GetCardJson(keyValuePair.Value.GetHero());
			jsonNode2["HeroPower"] = this.GetCardJson(keyValuePair.Value.GetHeroPower());
			if (keyValuePair.Value.HasWeapon())
			{
				jsonNode2["Weapon"] = this.GetCardJson(keyValuePair.Value.GetWeaponCard().GetEntity());
			}
			jsonNode2["CardsInBattlefield"] = this.GetCardlistJson(keyValuePair.Value.GetBattlefieldZone().GetCards());
			if (keyValuePair.Value.GetSide() == global::Player.Side.FRIENDLY)
			{
				jsonNode2["CardsInHand"] = this.GetCardlistJson(keyValuePair.Value.GetHandZone().GetCards());
				jsonNode2["ActiveSecrets"] = this.GetCardlistJson(keyValuePair.Value.GetSecretZone().GetCards());
			}
		}
		File.WriteAllText(string.Format("{0}\\{1}.json", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), arg), Json.Serialize(jsonNode));
		return true;
	}

	// Token: 0x060076F3 RID: 30451 RVA: 0x0026BC24 File Offset: 0x00269E24
	private bool OnProcessCheat_social(string func, string[] args, string rawArgs)
	{
		List<BnetPlayer> friends = BnetFriendMgr.Get().GetFriends();
		List<BnetPlayer> nearbyPlayers = BnetNearbyPlayerMgr.Get().GetNearbyPlayers();
		List<BnetPlayer> fullPatronList = FiresideGatheringManager.Get().FullPatronList;
		friends.Sort(new Comparison<BnetPlayer>(FriendUtils.FriendSortCompare));
		nearbyPlayers.Sort(new Comparison<BnetPlayer>(FriendUtils.FriendSortCompare));
		fullPatronList.Sort(new Comparison<BnetPlayer>(FriendUtils.FriendSortCompare));
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool printFullPresence = false;
		string text = "USAGE: social [cmd] [args]\nCommands: help, list";
		float delay = 5f;
		string a = (args == null || args.Length == 0) ? "list" : args[0];
		string text2 = null;
		if (!(a == "help"))
		{
			if (a == "list")
			{
				if (args.Length >= 2 && args[1] == "help")
				{
					text2 = "Lists all players in the various social lists. Can specific specific lists: friend, nearby, fsg|patron";
				}
				else
				{
					int i = 1;
					while (i < args.Length)
					{
						string text3 = (args[i] == null) ? "" : args[i].ToLower();
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
						if (num <= 2346441946U)
						{
							if (num <= 968225968U)
							{
								if (num <= 321211332U)
								{
									if (num != 55793322U)
									{
										if (num == 321211332U)
										{
											if (text3 == "all")
											{
												goto IL_3A3;
											}
										}
									}
									else if (text3 == "nearby")
									{
										goto IL_399;
									}
								}
								else if (num != 351961034U)
								{
									if (num == 968225968U)
									{
										if (text3 == "nearbyplayers")
										{
											goto IL_399;
										}
									}
								}
								else if (text3 == "localplayers")
								{
									goto IL_399;
								}
							}
							else if (num <= 1240809064U)
							{
								if (num != 1107537074U)
								{
									if (num == 1240809064U)
									{
										if (text3 == "subnet")
										{
											goto IL_399;
										}
									}
								}
								else if (text3 == "fireside")
								{
									goto IL_39E;
								}
							}
							else if (num != 1505396525U)
							{
								if (num != 2136870582U)
								{
									if (num == 2346441946U)
									{
										if (text3 == "friends")
										{
											goto IL_395;
										}
									}
								}
								else if (text3 == "patrons")
								{
									goto IL_39E;
								}
							}
							else if (text3 == "patronlist")
							{
								goto IL_39E;
							}
						}
						else if (num <= 3416301453U)
						{
							if (num <= 2621662984U)
							{
								if (num != 2362658692U)
								{
									if (num == 2621662984U)
									{
										if (text3 == "local")
										{
											goto IL_399;
										}
									}
								}
								else if (text3 == "presence")
								{
									goto IL_3A3;
								}
							}
							else if (num != 3200467009U)
							{
								if (num == 3416301453U)
								{
									if (text3 == "friend")
									{
										goto IL_395;
									}
								}
							}
							else if (text3 == "patron")
							{
								goto IL_39E;
							}
						}
						else if (num <= 3520440507U)
						{
							if (num != 3513378531U)
							{
								if (num == 3520440507U)
								{
									if (text3 == "fsg")
									{
										goto IL_39E;
									}
								}
							}
							else if (text3 == "nearbyplayer")
							{
								goto IL_399;
							}
						}
						else if (num != 3682206079U)
						{
							if (num != 4004841277U)
							{
								if (num == 4286165820U)
								{
									if (text3 == "full")
									{
										goto IL_3A3;
									}
								}
							}
							else if (text3 == "localplayer")
							{
								goto IL_399;
							}
						}
						else if (text3 == "firesidegathering")
						{
							goto IL_39E;
						}
						IL_3A6:
						i++;
						continue;
						IL_395:
						flag = true;
						goto IL_3A6;
						IL_399:
						flag2 = true;
						goto IL_3A6;
						IL_39E:
						flag3 = true;
						goto IL_3A6;
						IL_3A3:
						printFullPresence = true;
						goto IL_3A6;
					}
					if (!flag && !flag2 && !flag3)
					{
						flag2 = (flag = (flag3 = true));
					}
					global::Log.Presence.PrintInfo("Cheat: print social list executed.", Array.Empty<object>());
					if (flag3)
					{
						FSGConfig currentFSG = FiresideGatheringManager.Get().CurrentFSG;
						if (currentFSG == null)
						{
							global::Log.Presence.PrintInfo("FSG patrons: not checked in.", Array.Empty<object>());
						}
						else
						{
							global::Log.Presence.PrintInfo("FSG {0}-{1} patrons: {2}", new object[]
							{
								currentFSG.FsgId,
								currentFSG.TavernName,
								fullPatronList.Count
							});
						}
						foreach (BnetPlayer player in fullPatronList)
						{
							Cheats.OnProcessCheat_social_PrintPlayer(printFullPresence, player);
						}
					}
					if (flag)
					{
						global::Log.Presence.PrintInfo("Friends: {0}", new object[]
						{
							friends.Count
						});
						foreach (BnetPlayer player2 in friends)
						{
							Cheats.OnProcessCheat_social_PrintPlayer(printFullPresence, player2);
						}
					}
					if (flag2)
					{
						global::Log.Presence.PrintInfo("Nearby Players: {0}", new object[]
						{
							nearbyPlayers.Count
						});
						foreach (BnetPlayer player3 in nearbyPlayers)
						{
							Cheats.OnProcessCheat_social_PrintPlayer(printFullPresence, player3);
						}
					}
					text2 = "Printed to Presence Log.";
				}
			}
		}
		else
		{
			text2 = text;
		}
		if (text2 != null)
		{
			UIStatus.Get().AddInfo(text2, delay);
		}
		return true;
	}

	// Token: 0x060076F4 RID: 30452 RVA: 0x0026C1A8 File Offset: 0x0026A3A8
	private bool OnProcessCheat_playStartEmote(string func, string[] args, string rawArgs)
	{
		Gameplay gameplay = Gameplay.Get();
		if (gameplay == null)
		{
			return false;
		}
		gameplay.StartCoroutine(this.PlayStartingTaunts());
		return true;
	}

	// Token: 0x060076F5 RID: 30453 RVA: 0x0026C1D4 File Offset: 0x0026A3D4
	private bool OnProcessCheat_getBattlegroundDenyList(string func, string[] args, string rawArgs)
	{
		Network.Get().UpdateBattlegroundInfo();
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		gameState.SetPrintBattlegroundDenyListOnUpdate(true);
		return true;
	}

	// Token: 0x060076F6 RID: 30454 RVA: 0x0026C200 File Offset: 0x0026A400
	private bool OnProcessCheat_getBattlegroundMinionPool(string func, string[] args, string rawArgs)
	{
		Network.Get().UpdateBattlegroundInfo();
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return false;
		}
		gameState.SetPrintBattlegroundMinionPoolOnUpdate(true);
		return true;
	}

	// Token: 0x060076F7 RID: 30455 RVA: 0x0026C22A File Offset: 0x0026A42A
	private IEnumerator PlayStartingTaunts()
	{
		global::Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		global::Card heroPowerCard = GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard();
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
			CardSoundSpell soundSpell = emoteEntry.GetSoundSpell(true);
			if (soundSpell != null && soundSpell.DetermineBestAudioSource() == null)
			{
				flag = false;
			}
		}
		CardSoundSpell emoteSpell = null;
		if (flag)
		{
			emoteSpell = heroCard.PlayEmote(EmoteType.START);
		}
		if (emoteSpell != null)
		{
			while (emoteSpell.GetActiveState() != SpellStateType.NONE)
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
		global::Card myHeroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		global::Card myHeroPowerCard = GameState.Get().GetFriendlySidePlayer().GetHeroPowerCard();
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
		emoteSpell = myHeroCard.PlayEmote(emoteToPlay, Notification.SpeechBubbleDirection.BottomRight);
		if (emoteSpell != null)
		{
			while (emoteSpell.GetActiveState() != SpellStateType.NONE)
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
		yield break;
	}

	// Token: 0x060076F8 RID: 30456 RVA: 0x0026C234 File Offset: 0x0026A434
	private static void OnProcessCheat_social_PrintPlayer(bool printFullPresence, BnetPlayer player)
	{
		string text = (player == null) ? "<null>" : (printFullPresence ? player.FullPresenceSummary : player.ShortSummary);
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
		string text2 = string.Join(", ", sortedList.Keys.ToArray<string>());
		if (!string.IsNullOrEmpty(text2))
		{
			text2 = string.Format("[{0}]", text2);
		}
		global::Log.Presence.PrintInfo("    {0} {1}", new object[]
		{
			text,
			text2
		});
	}

	// Token: 0x060076F9 RID: 30457 RVA: 0x0026C2F8 File Offset: 0x0026A4F8
	private bool OnProcessCheat_OpponentName(string func, string[] args, string rawArgs)
	{
		Gameplay gameplay = Gameplay.Get();
		if (gameplay == null)
		{
			return false;
		}
		NameBanner nameBannerForSide = gameplay.GetNameBannerForSide(global::Player.Side.OPPOSING);
		if (nameBannerForSide == null)
		{
			return false;
		}
		nameBannerForSide.m_playerName.Text = args[0];
		return true;
	}

	// Token: 0x060076FA RID: 30458 RVA: 0x0026C338 File Offset: 0x0026A538
	private bool OnProcessCheat_friendlist(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: flist [cmd] [args]\nCommands: fill, add, remove";
		if (args.Length >= 1)
		{
			if (!args.Any((string a) => string.IsNullOrEmpty(a)))
			{
				float delay = 5f;
				string text = null;
				string a2 = args[0];
				if (!(a2 == "fill"))
				{
					if (!(a2 == "add"))
					{
						if (a2 == "remove")
						{
							BnetNearbyPlayerMgr.Get().Cheat_RemoveCheatFriends();
							FiresideGatheringManager.Get().Cheat_RemoveCheatFriends();
							BnetFriendMgr.Get().Cheat_RemoveCheatFriends();
							text = string.Format("Removed cheat friends", Array.Empty<object>());
						}
					}
					else
					{
						int num = 1;
						string arg = "Player";
						Cheats.FriendListType type = Cheats.FriendListType.FRIEND;
						int season = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>().Season;
						int leagueId = RankMgr.Get().GetLeagueRecordForType(League.LeagueType.NORMAL, season).ID;
						int starLevel = 1;
						BnetProgramId programID = BnetProgramId.HEARTHSTONE;
						bool isFriend = true;
						bool isOnline = true;
						foreach (string text2 in args)
						{
							string[] array = (text2 == null) ? null : text2.Split(new char[]
							{
								'='
							});
							if (array != null && array.Length >= 2)
							{
								if (array[0].Equals("num", StringComparison.InvariantCultureIgnoreCase))
								{
									int.TryParse(array[1], out num);
									if (num < 1)
									{
										num = 1;
									}
								}
								else if (array[0].Equals("name", StringComparison.InvariantCultureIgnoreCase))
								{
									arg = array[1];
								}
								else if (array[0].Equals("type", StringComparison.InvariantCultureIgnoreCase))
								{
									string text3 = array[1];
									if (!string.IsNullOrEmpty(text3))
									{
										type = global::EnumUtils.SafeParse<Cheats.FriendListType>(text3, Cheats.FriendListType.FRIEND, true);
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
									GeneralUtils.TryParseBool(array[1], out isFriend);
								}
								else if (array[0].Equals("online", StringComparison.InvariantCultureIgnoreCase))
								{
									GeneralUtils.TryParseBool(array[1], out isOnline);
								}
							}
						}
						for (int j = 0; j < num; j++)
						{
							this.CreateCheatFriendlistItem(arg + j, type, leagueId, starLevel, programID, isFriend, isOnline);
						}
						text = string.Format("Created {0} players", num);
					}
				}
				else
				{
					int season2 = NetCache.Get().GetNetObject<NetCache.NetCacheRewardProgress>().Season;
					int id = RankMgr.Get().GetLeagueRecordForType(League.LeagueType.NORMAL, season2).ID;
					int maxStarLevel = RankMgr.Get().GetMaxStarLevel(id);
					foreach (object obj in Enum.GetValues(typeof(Cheats.FriendListType)))
					{
						Cheats.FriendListType friendListType = (Cheats.FriendListType)obj;
						for (int k = 1; k < maxStarLevel; k++)
						{
							string name = string.Format("{0} friend{1}", friendListType, k);
							this.CreateCheatFriendlistItem(name, friendListType, id, k, BnetProgramId.HEARTHSTONE, true, true);
						}
					}
					text = string.Format("Filled friend list", Array.Empty<object>());
				}
				BnetBarFriendButton.Get().UpdateOnlineCount();
				if (text != null)
				{
					UIStatus.Get().AddInfo(text, delay);
				}
				return true;
			}
		}
		UIStatus.Get().AddInfo(message, 10f);
		return true;
	}

	// Token: 0x060076FB RID: 30459 RVA: 0x0026C6F0 File Offset: 0x0026A8F0
	private bool OnProcessCheat_SetGameSaveData(string func, string[] args, string rawArgs)
	{
		GameSaveKeyId gameSaveKeyId = (GameSaveKeyId)0;
		GameSaveKeySubkeyId gameSaveKeySubkeyId = (GameSaveKeySubkeyId)0;
		if (!this.ValidateAndParseGameSaveDataKeyAndSubkey(args, out gameSaveKeyId, out gameSaveKeySubkeyId))
		{
			UIStatus.Get().AddError("You must provide valid key and subkeys!", -1f);
			return true;
		}
		long num = 0L;
		int i = 2;
		string text = string.Empty;
		List<long> list = new List<long>();
		while (i < args.Count<string>())
		{
			if (!this.ValidateAndParseLongAtIndex(i, args, out num))
			{
				num = (long)GameUtils.TranslateCardIdToDbId(args[i], true);
				if (num == 0L)
				{
					break;
				}
			}
			list.Add(num);
			text = text + num + ";";
			i++;
		}
		args = new string[]
		{
			"setgsd",
			"key=" + args[0],
			"subkey=" + args[1],
			"values=" + text
		};
		GameSaveDataManager.Get().Cheat_SaveSubkeyToLocalCache(gameSaveKeyId, gameSaveKeySubkeyId, list.ToArray());
		UIStatus.Get().AddInfo(string.Format("Set key {0} subkey {1} to {2}", gameSaveKeyId, gameSaveKeySubkeyId, text));
		return this.OnProcessCheat_utilservercmd("util", args, rawArgs, null);
	}

	// Token: 0x060076FC RID: 30460 RVA: 0x0026C7FE File Offset: 0x0026A9FE
	private bool OnProcessCheat_SetDungeonRunProgress(string func, string[] args, string rawArgs)
	{
		return this.ParseAdventureThenSetProgress(args, Cheats.SetAdventureProgressMode.Progress);
	}

	// Token: 0x060076FD RID: 30461 RVA: 0x0026C808 File Offset: 0x0026AA08
	private bool OnProcessCheat_SetDungeonRunVictory(string func, string[] args, string rawArgs)
	{
		return this.ParseAdventureThenSetProgress(args, Cheats.SetAdventureProgressMode.Victory);
	}

	// Token: 0x060076FE RID: 30462 RVA: 0x0026C812 File Offset: 0x0026AA12
	private bool OnProcessCheat_SetDungeonRunDefeat(string func, string[] args, string rawArgs)
	{
		return this.ParseAdventureThenSetProgress(args, Cheats.SetAdventureProgressMode.Defeat);
	}

	// Token: 0x060076FF RID: 30463 RVA: 0x0026C81C File Offset: 0x0026AA1C
	private bool OnProcessCheat_ResetDungeonRunAdventure(string func, string[] args, string rawArgs)
	{
		AdventureDbId adventureDbId = Cheats.ParseAdventureDbIdFromArgs(args, 0);
		return adventureDbId == AdventureDbId.INVALID || this.ResetDungeonRunAdventure(adventureDbId, AdventureModeDbId.DUNGEON_CRAWL);
	}

	// Token: 0x06007700 RID: 30464 RVA: 0x0026C840 File Offset: 0x0026AA40
	private bool ResetDungeonRunAdventure(AdventureDbId adventure, AdventureModeDbId mode)
	{
		if (adventure == AdventureDbId.INVALID)
		{
			return true;
		}
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventure, (int)mode);
		if (adventureDataRecord == null)
		{
			UIStatus.Get().AddError(string.Format("No Adventure data found for Adventure {0} Mode {1}", adventure, mode), -1f);
			return true;
		}
		if (adventureDataRecord.GameSaveDataServerKey == 0)
		{
			UIStatus.Get().AddError(string.Format("No GameSaveDataServerKey for Adventure {0} Mode {1}!", adventure, mode), -1f);
			return true;
		}
		this.ResetAdventureRunCommon_Server(adventureDataRecord.GameSaveDataServerKey);
		if (adventureDataRecord.GameSaveDataClientKey != 0)
		{
			this.ResetAdventureRunCommon_Client(adventureDataRecord.GameSaveDataClientKey);
		}
		UIStatus.Get().AddInfo(string.Format("Reset current run for Adventure {0} Mode {1}", adventure, mode));
		return true;
	}

	// Token: 0x06007701 RID: 30465 RVA: 0x0026C8F4 File Offset: 0x0026AAF4
	private bool OnProcessCheat_ResetDungeonRun_VO(string func, string[] args, string rawArgs)
	{
		AdventureDbId adventureDbId = Cheats.ParseAdventureDbIdFromArgs(args, 0);
		if (adventureDbId == AdventureDbId.INVALID)
		{
			return true;
		}
		long subkeyValue = 0L;
		this.ValidateAndParseLongAtIndex(1, args, out subkeyValue);
		return this.ResetDungeonRun_VO(adventureDbId, subkeyValue);
	}

	// Token: 0x06007702 RID: 30466 RVA: 0x0026C924 File Offset: 0x0026AB24
	private bool ResetDungeonRun_VO(AdventureDbId adventure, long subkeyValue)
	{
		AdventureDungeonCrawlDisplay.s_shouldShowWelcomeBanner = true;
		if (adventure != AdventureDbId.LOOT)
		{
			if (adventure == AdventureDbId.GIL)
			{
				Options.Get().SetBool(global::Option.HAS_SEEN_PLAYED_TESS, false);
				Options.Get().SetBool(global::Option.HAS_SEEN_PLAYED_DARIUS, false);
				Options.Get().SetBool(global::Option.HAS_SEEN_PLAYED_SHAW, false);
				Options.Get().SetBool(global::Option.HAS_SEEN_PLAYED_TOKI, false);
			}
		}
		else
		{
			Options.Get().SetBool(global::Option.HAS_JUST_SEEN_LOOT_NO_TAKE_CANDLE_VO, false);
		}
		AdventureModeDbId adventureModeDbId = AdventureModeDbId.DUNGEON_CRAWL;
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)adventure, (int)adventureModeDbId);
		if (adventureDataRecord == null)
		{
			UIStatus.Get().AddError(string.Format("No Adventure data found for Adventure {0} Mode {1}", adventure, adventureModeDbId), -1f);
			return true;
		}
		if (adventureDataRecord.GameSaveDataClientKey == 0)
		{
			UIStatus.Get().AddError(string.Format("No GameSaveDataClientKey for Adventure {0} Mode {1}!", adventure, adventureModeDbId), -1f);
			return true;
		}
		this.ResetVOSubkeysForAdventure((GameSaveKeyId)adventureDataRecord.GameSaveDataClientKey, subkeyValue);
		if (adventureDataRecord.GameSaveDataServerKey != 0)
		{
			this.ResetVOSubkeysForAdventure((GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey, subkeyValue);
		}
		UIStatus.Get().AddInfo(string.Format("You can now see all {0} VO again.", adventure));
		return true;
	}

	// Token: 0x06007703 RID: 30467 RVA: 0x0026CA3C File Offset: 0x0026AC3C
	private bool ParseAdventureThenSetProgress(string[] args, Cheats.SetAdventureProgressMode progressMode)
	{
		AdventureDbId adventureDbId = Cheats.ParseAdventureDbIdFromArgs(args, 0);
		if (adventureDbId == AdventureDbId.INVALID)
		{
			return true;
		}
		string[] array = new string[args.Length - 1];
		Array.Copy(args, 1, array, 0, args.Length - 1);
		if (this.SetAdventureProgressCommon(adventureDbId, AdventureModeDbId.DUNGEON_CRAWL, array, progressMode))
		{
			UIStatus.Get().AddInfo(string.Format("Set Dungeon Run {0} for {1}", progressMode, adventureDbId));
		}
		return true;
	}

	// Token: 0x06007704 RID: 30468 RVA: 0x0026CA9C File Offset: 0x0026AC9C
	private bool OnProcessCheat_SetKCVictory(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.LOOT, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo(string.Format("Set KC victory", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007705 RID: 30469 RVA: 0x0026CAC8 File Offset: 0x0026ACC8
	private bool OnProcessCheat_SetKCProgress(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.LOOT, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo(string.Format("Set KC progress", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007706 RID: 30470 RVA: 0x0026CAF4 File Offset: 0x0026ACF4
	private bool OnProcessCheat_SetKCDefeat(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.LOOT, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo(string.Format("Set KC defeat", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007707 RID: 30471 RVA: 0x0026CB20 File Offset: 0x0026AD20
	private bool OnProcessCheat_SetGILVictory(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo(string.Format("Set Witchwood victory", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007708 RID: 30472 RVA: 0x0026CB4C File Offset: 0x0026AD4C
	private bool OnProcessCheat_SetGILProgress(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo(string.Format("Set Witchwood progress", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007709 RID: 30473 RVA: 0x0026CB78 File Offset: 0x0026AD78
	private bool OnProcessCheat_SetGILDefeat(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo(string.Format("Set Witchwood defeat", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x0600770A RID: 30474 RVA: 0x0026CBA4 File Offset: 0x0026ADA4
	private bool OnProcessCheat_SetGILBonus(string func, string[] args, string rawArgs)
	{
		this.OnProcessCheat_utilservercmd("util", new string[]
		{
			"quest",
			"progress",
			"achieve=1010",
			"amount=4"
		}, "util quest progress achieve=1010 amount=4", null);
		UIStatus.Get().AddInfo(string.Format("Set Witchwood Bonus Challenge Active", Array.Empty<object>()));
		Options.Get().SetBool(global::Option.HAS_SEEN_GIL_BONUS_CHALLENGE, false);
		return true;
	}

	// Token: 0x0600770B RID: 30475 RVA: 0x0026CC13 File Offset: 0x0026AE13
	private bool OnProcessCheat_SetTRLVictory(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.TRL, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo(string.Format("Set Rastakhan's Rumble victory", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x0600770C RID: 30476 RVA: 0x0026CC3F File Offset: 0x0026AE3F
	private bool OnProcessCheat_SetTRLProgress(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.TRL, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo(string.Format("Set Rastakhan's Rumble progress", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x0600770D RID: 30477 RVA: 0x0026CC6B File Offset: 0x0026AE6B
	private bool OnProcessCheat_SetTRLDefeat(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.TRL, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo(string.Format("Set Rastakhan's Rumble defeat", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x0600770E RID: 30478 RVA: 0x0026CC97 File Offset: 0x0026AE97
	private bool OnProcessCheat_SetDALProgress(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo(string.Format("Set Dalaran progress", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x0600770F RID: 30479 RVA: 0x0026CCC3 File Offset: 0x0026AEC3
	private bool OnProcessCheat_SetDALVictory(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo(string.Format("Set Dalaran victory", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007710 RID: 30480 RVA: 0x0026CCEF File Offset: 0x0026AEEF
	private bool OnProcessCheat_SetDALDefeat(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo(string.Format("Set Dalaran defeat", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007711 RID: 30481 RVA: 0x0026CD1B File Offset: 0x0026AF1B
	private bool OnProcessCheat_ResetDalaranAdventure(string func, string[] args, string rawArgs)
	{
		return this.ResetDungeonRunAdventure(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL);
	}

	// Token: 0x06007712 RID: 30482 RVA: 0x0026CD2C File Offset: 0x0026AF2C
	private bool OnProcessCheat_ResetTavernBrawlAdventure(string func, string[] args, string rawArgs)
	{
		if (TavernBrawlManager.Get() == null)
		{
			UIStatus.Get().AddError("TavernBrawlManager is not initialized!", -1f);
			return true;
		}
		TavernBrawlMission mission = TavernBrawlManager.Get().GetMission(BrawlType.BRAWL_TYPE_TAVERN_BRAWL);
		if (mission == null)
		{
			UIStatus.Get().AddError("No Tavern Brawl Mission found", -1f);
			return true;
		}
		ScenarioDbfRecord record = GameDbf.Scenario.GetRecord(mission.missionId);
		if (record == null)
		{
			UIStatus.Get().AddError("Could not find scenario for current tavern brawl mission", -1f);
			return true;
		}
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord(record.AdventureId, record.ModeId);
		if (adventureDataRecord == null)
		{
			UIStatus.Get().AddError("Could not find adventure data for current tavern brawl mission", -1f);
			return true;
		}
		this.ResetAdventureRunCommon_Server(adventureDataRecord.GameSaveDataServerKey);
		this.ResetAdventureRunCommon_Client(adventureDataRecord.GameSaveDataClientKey);
		UIStatus.Get().AddInfo(string.Format("Reset Tavern Brawl Adventure Progress", Array.Empty<object>()));
		return true;
	}

	// Token: 0x06007713 RID: 30483 RVA: 0x0026CE02 File Offset: 0x0026B002
	private bool OnProcessCheat_SetDALHeroicProgress(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, Cheats.SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo(string.Format("Set Dalaran Heroic progress", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007714 RID: 30484 RVA: 0x0026CE2E File Offset: 0x0026B02E
	private bool OnProcessCheat_SetDALHeroicVictory(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, Cheats.SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo(string.Format("Set Dalaran Heroic victory", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007715 RID: 30485 RVA: 0x0026CE5A File Offset: 0x0026B05A
	private bool OnProcessCheat_SetDALHeroicDefeat(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, Cheats.SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo(string.Format("Set Dalaran Heroic defeat", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007716 RID: 30486 RVA: 0x0026CE86 File Offset: 0x0026B086
	private bool OnProcessCheat_ResetDalaranHeroicAdventure(string func, string[] args, string rawArgs)
	{
		return this.ResetDungeonRunAdventure(AdventureDbId.DALARAN, AdventureModeDbId.DUNGEON_CRAWL_HEROIC);
	}

	// Token: 0x06007717 RID: 30487 RVA: 0x0026CE94 File Offset: 0x0026B094
	private bool OnProcessCheat_SetULDProgress(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo(string.Format("Set Uldum progress", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007718 RID: 30488 RVA: 0x0026CEC0 File Offset: 0x0026B0C0
	private bool OnProcessCheat_SetULDVictory(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo(string.Format("Set Uldum victory", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x06007719 RID: 30489 RVA: 0x0026CEEC File Offset: 0x0026B0EC
	private bool OnProcessCheat_SetULDDefeat(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL, args, Cheats.SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo(string.Format("Set Uldum defeat", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x0600771A RID: 30490 RVA: 0x0026CF18 File Offset: 0x0026B118
	private bool OnProcessCheat_ResetUldumAdventure(string func, string[] args, string rawArgs)
	{
		return this.ResetDungeonRunAdventure(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL);
	}

	// Token: 0x0600771B RID: 30491 RVA: 0x0026CF26 File Offset: 0x0026B126
	private bool OnProcessCheat_SetULDHeroicProgress(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, Cheats.SetAdventureProgressMode.Progress))
		{
			UIStatus.Get().AddInfo(string.Format("Set Uldum Heroic progress", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x0600771C RID: 30492 RVA: 0x0026CF52 File Offset: 0x0026B152
	private bool OnProcessCheat_SetULDHeroicVictory(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, Cheats.SetAdventureProgressMode.Victory))
		{
			UIStatus.Get().AddInfo(string.Format("Set Uldum Heroic victory", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x0600771D RID: 30493 RVA: 0x0026CF7E File Offset: 0x0026B17E
	private bool OnProcessCheat_SetULDHeroicDefeat(string func, string[] args, string rawArgs)
	{
		if (this.SetAdventureProgressCommon(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL_HEROIC, args, Cheats.SetAdventureProgressMode.Defeat))
		{
			UIStatus.Get().AddInfo(string.Format("Set Uldum Heroic defeat", Array.Empty<object>()));
		}
		return true;
	}

	// Token: 0x0600771E RID: 30494 RVA: 0x0026CFAA File Offset: 0x0026B1AA
	private bool OnProcessCheat_ResetUldumHeroicAdventure(string func, string[] args, string rawArgs)
	{
		return this.ResetDungeonRunAdventure(AdventureDbId.ULDUM, AdventureModeDbId.DUNGEON_CRAWL_HEROIC);
	}

	// Token: 0x0600771F RID: 30495 RVA: 0x0026CFB8 File Offset: 0x0026B1B8
	private bool OnProcessCheat_ResetGILAdventure(string func, string[] args, string rawArgs)
	{
		return this.ResetDungeonRunAdventure(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL);
	}

	// Token: 0x06007720 RID: 30496 RVA: 0x0026CFC8 File Offset: 0x0026B1C8
	private static AdventureDbId ParseAdventureDbIdFromArgs(string[] args, int index)
	{
		AdventureDbId adventureDbId = AdventureDbId.INVALID;
		if (args.Length <= index || string.IsNullOrEmpty(args[index]))
		{
			UIStatus.Get().AddError("You must provide an Adventure to operate on!  Ex: 'uld'", -1f);
			return adventureDbId;
		}
		adventureDbId = Cheats.GetAdventureDbIdFromString(args[index]);
		if (adventureDbId == AdventureDbId.INVALID)
		{
			UIStatus.Get().AddError(string.Format("{0} does not map to a valid Adventure!", args[index]), -1f);
			return adventureDbId;
		}
		return adventureDbId;
	}

	// Token: 0x06007721 RID: 30497 RVA: 0x0026D028 File Offset: 0x0026B228
	private static AdventureDbId GetAdventureDbIdFromString(string adventureString)
	{
		if (string.IsNullOrEmpty(adventureString))
		{
			return AdventureDbId.INVALID;
		}
		AdventureDbId adventureDbId = AdventureDbId.INVALID;
		try
		{
			adventureDbId = (AdventureDbId)Enum.Parse(typeof(AdventureDbId), adventureString, true);
		}
		catch (ArgumentException)
		{
		}
		if (adventureDbId != AdventureDbId.INVALID)
		{
			return adventureDbId;
		}
		string text = adventureString.ToLower();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
		if (num <= 1643767933U)
		{
			if (num > 1075868709U)
			{
				if (num <= 1188041456U)
				{
					if (num != 1178402731U)
					{
						if (num != 1188041456U)
						{
							return AdventureDbId.INVALID;
						}
						if (!(text == "uld"))
						{
							return AdventureDbId.INVALID;
						}
						return AdventureDbId.ULDUM;
					}
					else if (!(text == "kc"))
					{
						return AdventureDbId.INVALID;
					}
				}
				else if (num != 1387051717U)
				{
					if (num != 1643767933U)
					{
						return AdventureDbId.INVALID;
					}
					if (!(text == "knc"))
					{
						return AdventureDbId.INVALID;
					}
				}
				else if (!(text == "k&c"))
				{
					return AdventureDbId.INVALID;
				}
				return AdventureDbId.LOOT;
			}
			if (num != 408900432U)
			{
				if (num != 428798232U)
				{
					if (num != 1075868709U)
					{
						return AdventureDbId.INVALID;
					}
					if (!(text == "ga"))
					{
						return AdventureDbId.INVALID;
					}
					return AdventureDbId.DRAGONS;
				}
				else if (!(text == "nax"))
				{
					return AdventureDbId.INVALID;
				}
			}
			else
			{
				if (!(text == "league"))
				{
					return AdventureDbId.INVALID;
				}
				return AdventureDbId.LOE;
			}
		}
		else if (num <= 2617637408U)
		{
			if (num <= 1845823067U)
			{
				if (num != 1837638237U)
				{
					if (num != 1845823067U)
					{
						return AdventureDbId.INVALID;
					}
					if (!(text == "icecrown"))
					{
						return AdventureDbId.INVALID;
					}
					return AdventureDbId.ICC;
				}
				else
				{
					if (!(text == "karazhan"))
					{
						return AdventureDbId.INVALID;
					}
					return AdventureDbId.KARA;
				}
			}
			else if (num != 1935464710U)
			{
				if (num != 2617637408U)
				{
					return AdventureDbId.INVALID;
				}
				if (!(text == "naxx"))
				{
					return AdventureDbId.INVALID;
				}
			}
			else
			{
				if (!(text == "rastakhan"))
				{
					return AdventureDbId.INVALID;
				}
				return AdventureDbId.TRL;
			}
		}
		else if (num <= 2862703125U)
		{
			if (num != 2735789552U)
			{
				if (num != 2862703125U)
				{
					return AdventureDbId.INVALID;
				}
				if (!(text == "witchwood"))
				{
					return AdventureDbId.INVALID;
				}
				return AdventureDbId.GIL;
			}
			else
			{
				if (!(text == "tot"))
				{
					return AdventureDbId.INVALID;
				}
				return AdventureDbId.ULDUM;
			}
		}
		else if (num != 3861239580U)
		{
			if (num != 4014945102U)
			{
				return AdventureDbId.INVALID;
			}
			if (!(text == "dal"))
			{
				return AdventureDbId.INVALID;
			}
			return AdventureDbId.DALARAN;
		}
		else
		{
			if (!(text == "drg"))
			{
				return AdventureDbId.INVALID;
			}
			return AdventureDbId.DRAGONS;
		}
		return AdventureDbId.NAXXRAMAS;
	}

	// Token: 0x06007722 RID: 30498 RVA: 0x0026D2B8 File Offset: 0x0026B4B8
	private bool OnProcessCheat_UnlockLoadout(string func, string[] args, string rawArgs)
	{
		return this.UpdateAdventureLoadoutOptionsLockStateFromArgs(args, false);
	}

	// Token: 0x06007723 RID: 30499 RVA: 0x0026D2C2 File Offset: 0x0026B4C2
	private bool OnProcessCheat_LockLoadout(string func, string[] args, string rawArgs)
	{
		return this.UpdateAdventureLoadoutOptionsLockStateFromArgs(args, true);
	}

	// Token: 0x06007724 RID: 30500 RVA: 0x0026D2CC File Offset: 0x0026B4CC
	private bool OnProcessCheat_ShowAdventureLoadingPopup(string func, string[] args, string rawArgs)
	{
		GameMgr.Get().Cheat_ShowTransitionPopup(GameType.GT_VS_AI, FormatType.FT_WILD, (int)AdventureConfig.Get().GetMission());
		if (AdventureConfig.Get().GetMission() == ScenarioDbId.INVALID)
		{
			UIStatus.Get().AddInfo("Showing generic popup, navigate to an Adventure scenario to customize the popup");
		}
		else
		{
			UIStatus.Get().AddInfo(string.Format("Showing loading popup for scenario {0}", (int)AdventureConfig.Get().GetMission()));
		}
		return true;
	}

	// Token: 0x06007725 RID: 30501 RVA: 0x0026D330 File Offset: 0x0026B530
	private bool OnProcessCheat_HideGameTransitionPopup(string func, string[] args, string rawArgs)
	{
		GameMgr.Get().HideTransitionPopup();
		UIStatus.Get().AddInfo("Hiding Transition Popup");
		return true;
	}

	// Token: 0x06007726 RID: 30502 RVA: 0x0026D34C File Offset: 0x0026B54C
	private static GameSaveKeyId GetGameSaveServerKeyForAdventure(AdventureDbId adventureDbId, AdventureModeDbId adventureMode)
	{
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)adventureDbId && r.ModeId == (int)adventureMode);
		if (record == null)
		{
			Debug.LogErrorFormat("No AdventureDataDbfRecord found for Adventure {0} Mode {1}, unable to unlock loadout options!", new object[]
			{
				adventureDbId,
				adventureMode
			});
			return GameSaveKeyId.INVALID;
		}
		return (GameSaveKeyId)record.GameSaveDataServerKey;
	}

	// Token: 0x06007727 RID: 30503 RVA: 0x0026D3BC File Offset: 0x0026B5BC
	private bool UpdateAdventureLoadoutOptionsLockStateFromArgs(string[] args, bool shouldLock)
	{
		AdventureDbId adventure = Cheats.ParseAdventureDbIdFromArgs(args, 0);
		if (adventure == AdventureDbId.INVALID)
		{
			return true;
		}
		GameSaveKeyId normalServerKey = Cheats.GetGameSaveServerKeyForAdventure(adventure, AdventureModeDbId.DUNGEON_CRAWL);
		if (normalServerKey == (GameSaveKeyId)0)
		{
			UIStatus.Get().AddError(string.Concat(new object[]
			{
				"No ServerKey found for Adventure ",
				adventure,
				" Mode ",
				AdventureModeDbId.DUNGEON_CRAWL,
				", unable to unlock loadout options!"
			}), -1f);
			return true;
		}
		List<GameSaveKeyId> list = new List<GameSaveKeyId>
		{
			normalServerKey
		};
		GameSaveKeyId heroicServerKey = Cheats.GetGameSaveServerKeyForAdventure(adventure, AdventureModeDbId.DUNGEON_CRAWL_HEROIC);
		if (heroicServerKey != (GameSaveKeyId)0)
		{
			list.Add(heroicServerKey);
		}
		GameSaveDataManager.Get().Request(list, delegate(bool success)
		{
			this.UpdateAdventureLoadoutOptionsLockStateCommon(adventure, normalServerKey, shouldLock);
			if (heroicServerKey != (GameSaveKeyId)0)
			{
				this.UpdateAdventureLoadoutOptionsLockStateCommon(adventure, heroicServerKey, shouldLock);
			}
			if (!success)
			{
				UIStatus.Get().AddInfo("Failed to request ServerKeys for Adventure " + adventure + ", not all loadout options may be unlocked properly!");
				return;
			}
			UIStatus.Get().AddInfo(string.Format("{0} Loadout {1}", shouldLock ? "Lock" : "Unlock", adventure));
		});
		return true;
	}

	// Token: 0x06007728 RID: 30504 RVA: 0x0026D4AC File Offset: 0x0026B6AC
	private void UpdateLockSubkey(GameSaveKeyId serverKey, GameSaveKeySubkeyId subkey, long unlockValue, bool shouldLock)
	{
		if (serverKey == (GameSaveKeyId)0 || subkey == (GameSaveKeySubkeyId)0)
		{
			return;
		}
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(serverKey, subkey, out num);
		if (shouldLock && num != 0L)
		{
			this.InvokeSetGameSaveDataCheat(serverKey, subkey, 0L);
			return;
		}
		if (!shouldLock && num < unlockValue)
		{
			this.InvokeSetGameSaveDataCheat(serverKey, subkey, unlockValue);
		}
	}

	// Token: 0x06007729 RID: 30505 RVA: 0x0026D4F4 File Offset: 0x0026B6F4
	private void UpdateAdventureLoadoutOptionsLockStateCommon(AdventureDbId adventureDbId, GameSaveKeyId serverKey, bool shouldLock)
	{
		foreach (AdventureHeroPowerDbfRecord adventureHeroPowerDbfRecord in GameDbf.AdventureHeroPower.GetRecords((AdventureHeroPowerDbfRecord r) => r.AdventureId == (int)adventureDbId, -1))
		{
			this.UpdateLockSubkey(serverKey, (GameSaveKeySubkeyId)adventureHeroPowerDbfRecord.UnlockGameSaveSubkey, (long)adventureHeroPowerDbfRecord.UnlockValue, shouldLock);
		}
		foreach (AdventureDeckDbfRecord adventureDeckDbfRecord in GameDbf.AdventureDeck.GetRecords((AdventureDeckDbfRecord r) => r.AdventureId == (int)adventureDbId, -1))
		{
			this.UpdateLockSubkey(serverKey, (GameSaveKeySubkeyId)adventureDeckDbfRecord.UnlockGameSaveSubkey, (long)adventureDeckDbfRecord.UnlockValue, shouldLock);
		}
		foreach (AdventureLoadoutTreasuresDbfRecord adventureLoadoutTreasuresDbfRecord in GameDbf.AdventureLoadoutTreasures.GetRecords((AdventureLoadoutTreasuresDbfRecord r) => r.AdventureId == (int)adventureDbId, -1))
		{
			this.UpdateLockSubkey(serverKey, (GameSaveKeySubkeyId)adventureLoadoutTreasuresDbfRecord.UnlockGameSaveSubkey, (long)adventureLoadoutTreasuresDbfRecord.UnlockValue, shouldLock);
			this.UpdateLockSubkey(serverKey, (GameSaveKeySubkeyId)adventureLoadoutTreasuresDbfRecord.UpgradeGameSaveSubkey, (long)adventureLoadoutTreasuresDbfRecord.UpgradeValue, shouldLock);
		}
	}

	// Token: 0x0600772A RID: 30506 RVA: 0x0026D654 File Offset: 0x0026B854
	private void ResetAdventureRunCommon_Server(int key)
	{
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_SCENARIO_ID, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_DECK, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_ANOMALY_MODE, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_ANOMALY_MODE, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_LOST_TO, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CLASS, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_FOUGHT_LIST, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_FIGHT, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_NEXT_BOSS_FIGHT_UNDEFEATED, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_A, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_B, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_OPTION_C, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_TREASURE_OPTION, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_SHRINE_OPTIONS, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_LOOT_HISTORY, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_TREASURE, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_SHRINE, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_HEALTH, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_HEALTH, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_EVENT_1, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_EVENT_2, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_OVERRIDE, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, new long[0]);
		this.InvokeSetGameSaveDataCheat(key, GameSaveKeySubkeyId.DUELS_DRAFT_HERO_CHOICES, new long[0]);
	}

	// Token: 0x0600772B RID: 30507 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void ResetAdventureRunCommon_Client(int key)
	{
	}

	// Token: 0x0600772C RID: 30508 RVA: 0x0026D898 File Offset: 0x0026BA98
	private bool SetAdventureProgressCommon(AdventureDbId adventureDbId, AdventureModeDbId adventureMode, string[] args, Cheats.SetAdventureProgressMode mode)
	{
		AdventureDataDbfRecord record = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == (int)adventureDbId && r.ModeId == (int)adventureMode);
		if (record == null)
		{
			UIStatus.Get().AddError(string.Concat(new object[]
			{
				"No AdventureDataDbfRecord found for Adventure ",
				adventureDbId,
				" Mode ",
				adventureMode,
				", unable to set Adventure progress!"
			}), -1f);
			return false;
		}
		long num = 0L;
		if (mode != Cheats.SetAdventureProgressMode.Victory && !this.ValidateAndParseLongAtIndex(0, args, out num))
		{
			UIStatus.Get().AddError("You must provide a valid number of bosses defeated!", -1f);
			return false;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)record.GameSaveDataServerKey;
		long num2 = 0L;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num2);
		bool flag = num2 > 0L;
		if (!flag)
		{
			long num3 = 0L;
			if (GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_CLASS, out num3) && (int)num3 != 0)
			{
				this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CLASS, new long[]
				{
					num3
				});
			}
		}
		long deckClass = 0L;
		if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CLASS, out deckClass) || (int)deckClass == 0)
		{
			deckClass = 4L;
			HashSet<TAG_CLASS> hashSet = new HashSet<TAG_CLASS>(GameUtils.ORDERED_HERO_CLASSES);
			List<AdventureGuestHeroesDbfRecord> records = GameDbf.AdventureGuestHeroes.GetRecords((AdventureGuestHeroesDbfRecord r) => r.AdventureId == (int)adventureDbId, -1);
			GuestHeroDbfRecord guestHeroDbfRecord = null;
			foreach (AdventureGuestHeroesDbfRecord adventureGuestHeroesDbfRecord in records)
			{
				GuestHeroDbfRecord record2 = GameDbf.GuestHero.GetRecord(adventureGuestHeroesDbfRecord.GuestHeroId);
				if (hashSet.Contains(GameUtils.GetTagClassFromCardDbId(record2.CardId)))
				{
					guestHeroDbfRecord = record2;
					break;
				}
			}
			if (guestHeroDbfRecord != null)
			{
				TAG_CLASS tagClassFromCardDbId = GameUtils.GetTagClassFromCardDbId(guestHeroDbfRecord.CardId);
				if (tagClassFromCardDbId != TAG_CLASS.INVALID)
				{
					deckClass = (long)tagClassFromCardDbId;
				}
			}
			this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CLASS, new long[]
			{
				deckClass
			});
		}
		long num4 = 0L;
		if (record != null && record.DungeonCrawlSelectChapter)
		{
			if (!flag)
			{
				num4 = (long)AdventureConfig.Get().GetMission();
				if (num4 <= 0L)
				{
					GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_SCENARIO_ID, out num4);
				}
				if (num4 > 0L)
				{
					this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, new long[]
					{
						num4
					});
				}
			}
		}
		else if (adventureDbId == AdventureDbId.BOH || adventureDbId == AdventureDbId.BOM)
		{
			ScenarioDbId[] array;
			if (adventureDbId == AdventureDbId.BOH)
			{
				long deckClass2 = deckClass;
				long num5 = deckClass2 - 2L;
				if (num5 <= 8L)
				{
					switch ((uint)num5)
					{
					case 0U:
						array = new ScenarioDbId[]
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
						goto IL_386;
					case 1U:
						array = new ScenarioDbId[]
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
						goto IL_386;
					case 3U:
						array = new ScenarioDbId[]
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
						goto IL_386;
					case 4U:
						array = new ScenarioDbId[]
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
						goto IL_386;
					case 5U:
						array = new ScenarioDbId[]
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
						goto IL_386;
					case 6U:
						array = new ScenarioDbId[]
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
						goto IL_386;
					case 8U:
						array = new ScenarioDbId[]
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
						goto IL_386;
					}
				}
				array = new ScenarioDbId[]
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
				long deckClass2 = deckClass;
				array = new ScenarioDbId[]
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
			IL_386:
			if (num >= 0L && num < (long)array.Length)
			{
				num4 = (long)array[(int)(checked((IntPtr)num))];
			}
			if (num4 > 0L)
			{
				this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, new long[]
				{
					num4
				});
			}
		}
		if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, out num4) || num4 <= 0L)
		{
			ScenarioDbfRecord record3 = GameDbf.Scenario.GetRecord((ScenarioDbfRecord r) => r.AdventureId == (int)adventureDbId && r.ModeId == (int)adventureMode);
			if (record3 != null && record3.ID > 0)
			{
				num4 = (long)record3.ID;
				this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_SCENARIO_ID, new long[]
				{
					num4
				});
			}
		}
		if (AdventureUtils.SelectableHeroPowersExistForAdventure(adventureDbId))
		{
			long num6 = 0L;
			if (!flag && GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_HERO_POWER, out num6) && num6 > 0L)
			{
				this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER, new long[]
				{
					num6
				});
			}
			if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER, out num6) || num6 <= 0L)
			{
				AdventureHeroPowerDbfRecord record4 = GameDbf.AdventureHeroPower.GetRecord((AdventureHeroPowerDbfRecord r) => r.AdventureId == (int)adventureDbId && (long)r.ClassId == deckClass);
				if (record4 != null)
				{
					num6 = (long)record4.CardId;
					this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HERO_POWER, new long[]
					{
						num6
					});
				}
			}
		}
		if (AdventureUtils.SelectableDecksExistForAdventure(adventureDbId))
		{
			long adventureDeckId = 0L;
			List<long> list = null;
			if (!flag && GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_DECK, out adventureDeckId) && adventureDeckId > 0L)
			{
				list = (from r in GameDbf.DeckCard.GetRecords((DeckCardDbfRecord r) => (long)r.DeckId == adventureDeckId, -1)
				select (long)r.CardId).ToList<long>();
				this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, list.ToArray());
			}
			if (!GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, out list) || list == null || list.Count <= 0)
			{
				AdventureDeckDbfRecord record5 = GameDbf.AdventureDeck.GetRecord((AdventureDeckDbfRecord r) => r.AdventureId == (int)adventureDbId && (long)r.ClassId == deckClass);
				if (record5 != null)
				{
					adventureDeckId = (long)record5.DeckId;
					if (adventureDeckId > 0L)
					{
						list = (from r in GameDbf.DeckCard.GetRecords((DeckCardDbfRecord r) => (long)r.DeckId == adventureDeckId, -1)
						select (long)r.CardId).ToList<long>();
						this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, list.ToArray());
					}
				}
			}
		}
		if (!flag && AdventureUtils.SelectableLoadoutTreasuresExistForAdventure(adventureDbId))
		{
			long num7 = 0L;
			if (GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_SELECTED_LOADOUT_TREASURE_ID, out num7) && num7 > 0L)
			{
				List<long> list2;
				GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, out list2);
				if (list2 == null)
				{
					list2 = new List<long>();
				}
				list2.Add(num7);
				this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_DECK_CARD_LIST, list2.ToArray());
			}
		}
		AdventureDbId adventureDbId2 = adventureDbId;
		long[] array2;
		if (adventureDbId2 <= AdventureDbId.TRL)
		{
			if (adventureDbId2 == AdventureDbId.LOOT)
			{
				array2 = new long[]
				{
					47316L,
					46311L,
					46915L,
					46338L,
					46371L,
					47307L,
					47001L,
					47210L
				};
				goto IL_861;
			}
			if (adventureDbId2 == AdventureDbId.GIL)
			{
				array2 = new long[]
				{
					47903L,
					48311L,
					48182L,
					48151L,
					48196L,
					48600L,
					48942L,
					48315L
				};
				goto IL_861;
			}
			if (adventureDbId2 == AdventureDbId.TRL)
			{
				array2 = new long[]
				{
					53222L,
					53223L,
					53224L,
					53225L,
					53226L,
					53227L,
					53228L,
					53229L
				};
				goto IL_861;
			}
		}
		else if (adventureDbId2 <= AdventureDbId.ULDUM)
		{
			if (adventureDbId2 == AdventureDbId.DALARAN)
			{
				array2 = new long[]
				{
					53750L,
					53779L,
					53667L,
					53558L,
					53572L,
					53636L,
					53607L,
					53309L,
					53562L,
					53483L,
					53714L,
					53783L
				};
				goto IL_861;
			}
			if (adventureDbId2 != AdventureDbId.ULDUM)
			{
			}
		}
		else
		{
			if (adventureDbId2 == AdventureDbId.BOH)
			{
				long deckClass2 = deckClass;
				long num8 = deckClass2 - 2L;
				if (num8 <= 8L)
				{
					switch ((uint)num8)
					{
					case 0U:
						array2 = new long[]
						{
							71857L,
							71865L,
							71866L,
							71867L,
							71868L,
							71869L,
							71870L,
							71871L
						};
						goto IL_861;
					case 1U:
						array2 = new long[]
						{
							63834L,
							63835L,
							63836L,
							61384L,
							63837L,
							61385L,
							63838L,
							63839L
						};
						goto IL_861;
					case 3U:
						array2 = new long[]
						{
							61388L,
							65557L,
							65558L,
							65559L,
							61389L,
							65560L,
							65561L,
							65562L
						};
						goto IL_861;
					case 4U:
						array2 = new long[]
						{
							66904L,
							66902L,
							66903L,
							66904L,
							66905L,
							66906L,
							66908L,
							66909L
						};
						goto IL_861;
					case 5U:
						array2 = new long[]
						{
							68015L,
							68016L,
							68017L,
							68018L,
							68019L,
							68020L,
							68021L,
							68022L
						};
						goto IL_861;
					case 6U:
						array2 = new long[]
						{
							71187L,
							71188L,
							71189L,
							71190L,
							71191L,
							71192L,
							71193L,
							71194L
						};
						goto IL_861;
					case 8U:
						array2 = new long[]
						{
							61390L,
							64757L,
							64758L,
							64759L,
							64760L,
							64761L,
							64762L,
							64763L
						};
						goto IL_861;
					}
				}
				array2 = new long[]
				{
					63199L,
					63201L,
					63204L,
					63205L,
					63206L,
					63207L,
					63208L,
					61382L
				};
				goto IL_861;
			}
			if (adventureDbId2 == AdventureDbId.BOM)
			{
				long deckClass2 = deckClass;
				array2 = new long[]
				{
					67655L,
					67656L,
					67657L,
					67658L,
					67659L,
					67660L,
					67661L,
					67662L
				};
				goto IL_861;
			}
		}
		array2 = new long[]
		{
			57319L,
			57378L,
			57322L,
			57397L,
			57573L,
			53810L,
			57387L,
			56176L
		};
		IL_861:
		if (mode == Cheats.SetAdventureProgressMode.Victory)
		{
			num = (long)array2.Length;
		}
		else if (mode == Cheats.SetAdventureProgressMode.Progress)
		{
			num = Math.Min(num, (long)(array2.Length - 1));
		}
		int adventureBossesInRun = AdventureConfig.GetAdventureBossesInRun(GameUtils.GetWingRecordFromMissionId((int)num4));
		if (adventureBossesInRun > 0)
		{
			num = Math.Min(num, (long)(adventureBossesInRun - 1));
		}
		long num9 = 0L;
		this.ValidateAndParseLongAtIndex(1, args, out num9);
		List<long> list3;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, out list3);
		if (list3 == null)
		{
			list3 = new List<long>();
		}
		if ((long)list3.Count > num)
		{
			int num10 = list3.Count - (int)num;
			list3.RemoveRange(list3.Count - num10, num10);
		}
		else
		{
			while ((long)list3.Count < num)
			{
				list3.Add(array2[list3.Count]);
			}
		}
		this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSSES_DEFEATED, list3.ToArray());
		this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, new long[]
		{
			(mode == Cheats.SetAdventureProgressMode.Progress) ? 1L : 0L
		});
		this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_RETIRED, new long[0]);
		if (mode == Cheats.SetAdventureProgressMode.Victory || mode == Cheats.SetAdventureProgressMode.Defeat)
		{
			this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_TREASURE, new long[0]);
			this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_PLAYER_CHOSEN_LOOT, new long[0]);
		}
		if (mode == Cheats.SetAdventureProgressMode.Victory)
		{
			this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_LOST_TO, new long[0]);
			this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_FIGHT, new long[0]);
		}
		else if (mode == Cheats.SetAdventureProgressMode.Defeat)
		{
			this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_BOSS_LOST_TO, new long[]
			{
				array2[list3.Count]
			});
			this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_FIGHT, new long[0]);
		}
		else
		{
			if (num9 == 0L && list3.Count < array2.Length)
			{
				num9 = array2[list3.Count];
			}
			this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_NEXT_BOSS_FIGHT, new long[]
			{
				num9
			});
		}
		this.InvokeSetGameSaveDataCheat(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_LATEST_DUNGEON_RUN_COMPLETE, new long[1]);
		return true;
	}

	// Token: 0x0600772D RID: 30509 RVA: 0x0026E2C8 File Offset: 0x0026C4C8
	private bool OnProcessCheat_SetAllPuzzlesInProgress(string func, string[] args, string rawArgs)
	{
		int gameSaveDataServerKey = GameDbf.AdventureData.GetRecord((AdventureDataDbfRecord r) => r.AdventureId == 429).GameSaveDataServerKey;
		foreach (ScenarioDbfRecord scenarioDbfRecord in GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == 429, -1))
		{
			int gameSaveDataProgressSubkey = scenarioDbfRecord.GameSaveDataProgressSubkey;
			int gameSaveDataProgressMax = scenarioDbfRecord.GameSaveDataProgressMax;
			this.InvokeSetGameSaveDataCheat((GameSaveKeyId)gameSaveDataServerKey, (GameSaveKeySubkeyId)gameSaveDataProgressSubkey, new long[]
			{
				(long)gameSaveDataProgressMax
			});
		}
		UIStatus.Get().AddInfo(string.Format("Set All Boomsday Puzzles To Their Last Sub-Puzzle", Array.Empty<object>()));
		return true;
	}

	// Token: 0x0600772E RID: 30510 RVA: 0x0026E3A0 File Offset: 0x0026C5A0
	private void InvokeSetGameSaveDataCheat(GameSaveKeyId key, GameSaveKeySubkeyId subkey, long value)
	{
		this.InvokeSetGameSaveDataCheat(key, subkey, new long[]
		{
			value
		});
	}

	// Token: 0x0600772F RID: 30511 RVA: 0x0026E3B4 File Offset: 0x0026C5B4
	private void InvokeSetGameSaveDataCheat(GameSaveKeyId key, GameSaveKeySubkeyId subkey, long[] values)
	{
		this.InvokeSetGameSaveDataCheat((int)key, subkey, values);
	}

	// Token: 0x06007730 RID: 30512 RVA: 0x0026E3C0 File Offset: 0x0026C5C0
	private void InvokeSetGameSaveDataCheat(int key, GameSaveKeySubkeyId subkey, long[] values)
	{
		List<string> list = new List<string>();
		list.Add(key.ToString());
		int i = (int)subkey;
		list.Add(i.ToString());
		List<string> list2 = list;
		if (values != null)
		{
			foreach (long num in values)
			{
				list2.Add(num.ToString());
			}
		}
		this.OnProcessCheat_SetGameSaveData("setgsd", list2.ToArray(), string.Join(" ", list2.ToArray()));
	}

	// Token: 0x06007731 RID: 30513 RVA: 0x0026E438 File Offset: 0x0026C638
	private bool OnProcessCheat_GetGameSaveData(string func, string[] args, string rawArgs)
	{
		GameSaveKeyId gameSaveKeyId = (GameSaveKeyId)0;
		GameSaveKeySubkeyId gameSaveKeySubkeyId = (GameSaveKeySubkeyId)0;
		if (!this.ValidateAndParseGameSaveDataKeyAndSubkey(args, out gameSaveKeyId, out gameSaveKeySubkeyId))
		{
			UIStatus.Get().AddError("You must provide valid key and subkeys!", -1f);
			return true;
		}
		args = new string[]
		{
			"getgsd",
			"key=" + args[0],
			"subkey=" + args[1]
		};
		return this.OnProcessCheat_utilservercmd("util", args, rawArgs, null);
	}

	// Token: 0x06007732 RID: 30514 RVA: 0x0026E4AC File Offset: 0x0026C6AC
	private bool ValidateAndParseLongAtIndex(int index, string[] args, out long value)
	{
		value = 0L;
		long num = 0L;
		if (args.Length <= index || !long.TryParse(args[index], out num))
		{
			return false;
		}
		value = num;
		return true;
	}

	// Token: 0x06007733 RID: 30515 RVA: 0x0026E4D8 File Offset: 0x0026C6D8
	private bool ValidateAndParseGameSaveDataKeyAndSubkey(string[] args, out GameSaveKeyId key, out GameSaveKeySubkeyId subkey)
	{
		key = (GameSaveKeyId)0;
		subkey = (GameSaveKeySubkeyId)0;
		long num = 0L;
		if (args.Length < 1 || !long.TryParse(args[0], out num) || num == 0L)
		{
			UIStatus.Get().AddError("You must provide a valid non-zero id for the key!", -1f);
			return false;
		}
		key = (GameSaveKeyId)num;
		long num2 = 0L;
		if (args.Length < 2 || !long.TryParse(args[1], out num2) || num2 == 0L)
		{
			UIStatus.Get().AddError("You must provide a valid non-zero id for the key!", -1f);
			return false;
		}
		subkey = (GameSaveKeySubkeyId)num2;
		return true;
	}

	// Token: 0x06007734 RID: 30516 RVA: 0x0026E550 File Offset: 0x0026C750
	private bool OnProcessCheat_ResetKC_VO(string func, string[] args, string rawArgs)
	{
		long subkeyValue;
		this.ValidateAndParseLongAtIndex(0, args, out subkeyValue);
		this.ResetDungeonRun_VO(AdventureDbId.LOOT, subkeyValue);
		return true;
	}

	// Token: 0x06007735 RID: 30517 RVA: 0x0026E578 File Offset: 0x0026C778
	private bool OnProcessCheat_ResetGIL_VO(string func, string[] args, string rawArgs)
	{
		long subkeyValue;
		this.ValidateAndParseLongAtIndex(0, args, out subkeyValue);
		this.ResetDungeonRun_VO(AdventureDbId.GIL, subkeyValue);
		return true;
	}

	// Token: 0x06007736 RID: 30518 RVA: 0x0026E5A0 File Offset: 0x0026C7A0
	private bool OnProcessCheat_UnlockHagatha(string func, string[] args, string rawArgs)
	{
		this.InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_SERVER_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HUNTER_RUN_WINS, new long[]
		{
			1L
		});
		this.InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_SERVER_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_WARRIOR_RUN_WINS, new long[]
		{
			1L
		});
		this.InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_SERVER_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_ROGUE_RUN_WINS, new long[]
		{
			1L
		});
		this.OnProcessCheat_utilservercmd("util", new string[]
		{
			"quest",
			"progress",
			"achieve=1010",
			"amount=3"
		}, "util quest progress achieve=1010 amount=3", null);
		this.SetAdventureProgressCommon(AdventureDbId.GIL, AdventureModeDbId.DUNGEON_CRAWL, new string[]
		{
			"7"
		}, Cheats.SetAdventureProgressMode.Progress);
		this.InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_CHARACTER_SELECT_VO, new long[]
		{
			1L
		});
		this.InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_1_VO, new long[]
		{
			1L
		});
		this.InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_2_VO, new long[]
		{
			1L
		});
		this.InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_BOSS_FLIP_3_VO, new long[]
		{
			1L
		});
		this.InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_TREASURE_1_VO, new long[]
		{
			1L
		});
		this.InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_1_VO, new long[]
		{
			1L
		});
		this.InvokeSetGameSaveDataCheat(GameSaveKeyId.ADVENTURE_DATA_CLIENT_GIL, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_OFFER_LOOT_PACKS_2_VO, new long[]
		{
			1L
		});
		return true;
	}

	// Token: 0x06007737 RID: 30519 RVA: 0x0026E6EC File Offset: 0x0026C8EC
	private bool OnProcessCheat_ResetTRL_VO(string func, string[] args, string rawArgs)
	{
		long subkeyValue;
		this.ValidateAndParseLongAtIndex(0, args, out subkeyValue);
		this.ResetDungeonRun_VO(AdventureDbId.TRL, subkeyValue);
		return true;
	}

	// Token: 0x06007738 RID: 30520 RVA: 0x0026E714 File Offset: 0x0026C914
	private bool OnProcessCheat_ResetDAL_VO(string func, string[] args, string rawArgs)
	{
		long subkeyValue;
		this.ValidateAndParseLongAtIndex(0, args, out subkeyValue);
		this.ResetDungeonRun_VO(AdventureDbId.DALARAN, subkeyValue);
		return true;
	}

	// Token: 0x06007739 RID: 30521 RVA: 0x0026E73C File Offset: 0x0026C93C
	private bool OnProcessCheat_ResetULD_VO(string func, string[] args, string rawArgs)
	{
		long subkeyValue;
		this.ValidateAndParseLongAtIndex(0, args, out subkeyValue);
		this.ResetDungeonRun_VO(AdventureDbId.ULDUM, subkeyValue);
		return true;
	}

	// Token: 0x0600773A RID: 30522 RVA: 0x0026E764 File Offset: 0x0026C964
	private void ResetVOSubkeysForAdventure(GameSaveKeyId adventureGameSaveKey, long subkeyValue = 0L)
	{
		List<GameSaveKeySubkeyId> list = new List<GameSaveKeySubkeyId>();
		list.Add(GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_WING_COMPLETE_VO);
		list.Add(GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_COMPLETE_ALL_CLASSES_VO);
		List<GameSaveKeySubkeyId> list2 = new List<GameSaveKeySubkeyId>
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
		foreach (GameSaveKeySubkeyId subkey in list)
		{
			long[] values = null;
			this.InvokeSetGameSaveDataCheat(adventureGameSaveKey, subkey, values);
		}
		foreach (GameSaveKeySubkeyId subkey2 in list2)
		{
			long[] values2 = null;
			if (subkeyValue != 0L)
			{
				values2 = new long[]
				{
					subkeyValue
				};
			}
			this.InvokeSetGameSaveDataCheat(adventureGameSaveKey, subkey2, values2);
		}
	}

	// Token: 0x0600773B RID: 30523 RVA: 0x0026E9F0 File Offset: 0x0026CBF0
	private bool OnProcessCheat_SetAdventureComingSoon(string func, string[] args, string rawArgs)
	{
		if (args.Length < 2)
		{
			UIStatus.Get().AddInfo("Usage: setadventurecomingsoon [ADVENTURE] [TRUE/FALSE]\nExample: setadventurecomingsoon GIL true");
			return false;
		}
		AdventureDbId adventureDbId = Cheats.ParseAdventureDbIdFromArgs(args, 0);
		if (adventureDbId == AdventureDbId.INVALID)
		{
			return false;
		}
		bool flag = false;
		if (!bool.TryParse(args[1], out flag))
		{
			UIStatus.Get().AddError(string.Format("Unable to parse \"{0}\". Please enter True or False.", args[1]), -1f);
			return false;
		}
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)adventureDbId);
		record.SetVar("COMING_SOON_EVENT", flag ? "always" : "never");
		GameDbf.Adventure.ReplaceRecordByRecordId(record);
		string message = (AdventureScene.Get() == null) ? "Success!" : "Success!\nBack out and re-enter to see the change.";
		UIStatus.Get().AddInfo(message);
		return true;
	}

	// Token: 0x0600773C RID: 30524 RVA: 0x0026EAA4 File Offset: 0x0026CCA4
	private bool OnProcessCheat_ResetSession_VO(string func, string[] args, string rawArgs)
	{
		NotificationManager.Get().ResetSoundsPlayedThisSession();
		return true;
	}

	// Token: 0x0600773D RID: 30525 RVA: 0x0026EAB4 File Offset: 0x0026CCB4
	private bool OnProcessCheat_SetVOChance_VO(string func, string[] args, string rawArgs)
	{
		float num = -1f;
		if (args.Length != 0 && float.TryParse(args[0], out num) && num >= 0f)
		{
			num = Mathf.Clamp(num, 0f, 1f);
		}
		Cheats.VOChanceOverride = num;
		return true;
	}

	// Token: 0x0600773E RID: 30526 RVA: 0x0026EAF8 File Offset: 0x0026CCF8
	private BnetPlayer CreateCheatFriendlistItem(string name, Cheats.FriendListType type, int leagueId, int starLevel, BnetProgramId programID, bool isFriend, bool isOnline)
	{
		if (type == Cheats.FriendListType.FRIEND)
		{
			return BnetFriendMgr.Get().Cheat_CreateFriend(name, leagueId, starLevel, programID, isOnline);
		}
		if (type == Cheats.FriendListType.NEARBY)
		{
			return BnetNearbyPlayerMgr.Get().Cheat_CreateNearbyPlayer(name, leagueId, starLevel, programID, isFriend, isOnline);
		}
		if (type == Cheats.FriendListType.FSG)
		{
			return FiresideGatheringManager.Get().Cheat_CreateFSGPatron(name, leagueId, starLevel, programID, isFriend, isOnline);
		}
		return null;
	}

	// Token: 0x0600773F RID: 30527 RVA: 0x0026EB50 File Offset: 0x0026CD50
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

	// Token: 0x06007740 RID: 30528 RVA: 0x0026EBEC File Offset: 0x0026CDEC
	private bool OnProcessCheat_IPAddress(string func, string[] args, string rawArgs)
	{
		IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		if (hostEntry.AddressList.Length != 0)
		{
			string text = "";
			foreach (IPAddress ipaddress in hostEntry.AddressList)
			{
				text = text + ipaddress.ToString() + "\n";
			}
			UIStatus.Get().AddInfo(text, 10f);
		}
		return true;
	}

	// Token: 0x06007741 RID: 30529 RVA: 0x0026EC51 File Offset: 0x0026CE51
	private bool OnProcessCheat_Attribution(string func, string[] args, string rawArgs)
	{
		BlizzardAttributionManager.Get().SendAllEventsForTest();
		return true;
	}

	// Token: 0x06007742 RID: 30530 RVA: 0x0026EC5E File Offset: 0x0026CE5E
	private bool OnProcessCheat_CRM(string func, string[] args, string rawArgs)
	{
		BlizzardCRMManager.Get().SendAllEventsForTest();
		UIStatus.Get().AddInfo("Test CRM telemetry sent!");
		return true;
	}

	// Token: 0x06007743 RID: 30531 RVA: 0x0026EC7C File Offset: 0x0026CE7C
	private bool OnProcessCheat_Updater(string func, string[] args, string rawArgs)
	{
		string text = "USAGE: updater [cmd] [args]\\nCommands: speed, gamespeed\\nNotice: Unit of speed is bytes per second.\\n\n\\t0 = unlimited, -1 = turn off game streaming\\n\\tStore the speed permanently: speed 0 store";
		if (args.Length >= 1)
		{
			if (!args.Any((string a) => string.IsNullOrEmpty(a)))
			{
				if (this.DownloadManager == null)
				{
					UIStatus.Get().AddInfo("DownloadManager is not ready yet!");
					return true;
				}
				string a2 = args[0];
				bool flag = true;
				bool flag2 = false;
				int num = 0;
				if (args.Length > 1)
				{
					num = int.Parse(args[1]);
					flag2 = (args.Length > 2 && args[2].Equals("store"));
				}
				else
				{
					flag = false;
				}
				string text2 = null;
				if (!(a2 == "help"))
				{
					if (!(a2 == "speed"))
					{
						if (a2 == "gamespeed")
						{
							if (flag)
							{
								if (num < 0)
								{
									this.DownloadManager.InGameStreamingDefaultSpeed = num;
									text2 = "Turned off in game streaming";
								}
								else
								{
									this.DownloadManager.DownloadSpeedInGame = num;
									text2 = "Set the download speed in game to " + num;
								}
							}
							else
							{
								text2 = "The current speed in game is " + this.DownloadManager.DownloadSpeedInGame;
							}
							if (flag2 && num >= 0)
							{
								Options.Get().SetInt(global::Option.STREAMING_SPEED_IN_GAME, num);
							}
						}
					}
					else if (num < 0)
					{
						text2 = "Error: Cannot use the negative value!";
					}
					else
					{
						if (flag)
						{
							this.DownloadManager.MaxDownloadSpeed = num;
							text2 = "Set the download speed to " + num;
						}
						else
						{
							text2 = "The current speed is " + this.DownloadManager.MaxDownloadSpeed;
						}
						if (flag2)
						{
							Options.Get().SetInt(global::Option.MAX_DOWNLOAD_SPEED, num);
						}
					}
				}
				else
				{
					text2 = text;
				}
				if (text2 != null)
				{
					UIStatus.Get().AddInfo(text2, 5f);
				}
				return true;
			}
		}
		UIStatus.Get().AddInfo(text, 10f);
		return true;
	}

	// Token: 0x06007744 RID: 30532 RVA: 0x0026EE44 File Offset: 0x0026D044
	private bool OnProcessCheat_Assets(string func, string[] args, string rawArgs)
	{
		string message = AssetLoaderDebug.HandleCheat(func, args, rawArgs);
		UIStatus.Get().AddInfo(message);
		return true;
	}

	// Token: 0x06007745 RID: 30533 RVA: 0x0026EE68 File Offset: 0x0026D068
	private bool OnProcessCheat_testproduct(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: testproduct <pmt_product_id>";
		long num;
		if (args.Length < 1 || !long.TryParse(args[0], out num))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		string text = StoreManager.Get().Catalog.DebugFillShopWithProduct(num);
		if (text == null)
		{
			UIStatus.Get().AddInfo(string.Format("Shop filled with product {0}", num), 10f);
		}
		else
		{
			UIStatus.Get().AddInfo(string.Format("Error: {0}", text), 10f);
		}
		return true;
	}

	// Token: 0x06007746 RID: 30534 RVA: 0x0026EEF0 File Offset: 0x0026D0F0
	private bool OnProcessCheat_testadventurestore(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: testadventurestore <wing_id> <is_full_adventure>";
		int num;
		if (args.Length < 1 || !int.TryParse(args[0], out num))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		bool flag = false;
		if (args.Length >= 2 && !GeneralUtils.TryParseBool(args[1], out flag))
		{
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		WingDbfRecord record = GameDbf.Wing.GetRecord(num);
		if (record == null)
		{
			UIStatus.Get().AddInfo(string.Format("wing {0} not found", num), 10f);
			return true;
		}
		if (AdventureProgressMgr.Get() == null)
		{
			UIStatus.Get().AddInfo("AdventureProgressMgr not initialized", 10f);
			return true;
		}
		AdventureDbId adventureId = (AdventureDbId)record.AdventureId;
		int numItemsRequired = 0;
		int num2 = 0;
		AdventureDbId adventureDbId = adventureId;
		ProductType product;
		ShopType shopType;
		if (adventureDbId <= AdventureDbId.ICC)
		{
			switch (adventureDbId)
			{
			case AdventureDbId.INVALID:
				UIStatus.Get().AddInfo(string.Format("wing {0} is not part of an adventure.", num), 10f);
				return true;
			case AdventureDbId.TUTORIAL:
			case AdventureDbId.PRACTICE:
				break;
			case AdventureDbId.NAXXRAMAS:
				product = ProductType.PRODUCT_TYPE_NAXX;
				shopType = ShopType.ADVENTURE_STORE;
				numItemsRequired = 1;
				goto IL_1E6;
			case AdventureDbId.BRM:
				product = ProductType.PRODUCT_TYPE_BRM;
				shopType = ShopType.ADVENTURE_STORE;
				numItemsRequired = 1;
				goto IL_1E6;
			case (AdventureDbId)5:
			case (AdventureDbId)6:
			case AdventureDbId.TAVERN_BRAWL:
			case (AdventureDbId)9:
				goto IL_180;
			case AdventureDbId.LOE:
				product = ProductType.PRODUCT_TYPE_LOE;
				shopType = ShopType.ADVENTURE_STORE;
				numItemsRequired = 1;
				goto IL_1E6;
			case AdventureDbId.KARA:
				product = ProductType.PRODUCT_TYPE_WING;
				shopType = ShopType.ADVENTURE_STORE;
				numItemsRequired = 1;
				goto IL_1E6;
			default:
				if (adventureDbId != AdventureDbId.ICC)
				{
					goto IL_180;
				}
				break;
			}
		}
		else if (adventureDbId != AdventureDbId.GIL && adventureDbId != AdventureDbId.TRL)
		{
			goto IL_180;
		}
		UIStatus.Get().AddInfo(string.Format("wing {0} is part of a free adventure.", num), 10f);
		return true;
		IL_180:
		product = ProductType.PRODUCT_TYPE_WING;
		if (flag)
		{
			shopType = ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET;
			num2 = record.PmtProductIdForThisAndRestOfAdventure;
			if (num2 == 0)
			{
				UIStatus.Get().AddInfo(string.Format("wing {0} has no product id defined to complete the adventure", num), 10f);
				return true;
			}
		}
		else
		{
			shopType = ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET;
			num2 = record.PmtProductIdForSingleWingPurchase;
			if (num2 == 0)
			{
				UIStatus.Get().AddInfo(string.Format("wing {0} has no product id defined by the single wing", num), 10f);
				return true;
			}
		}
		IL_1E6:
		ItemOwnershipStatus productItemOwnershipStatus = StoreManager.GetProductItemOwnershipStatus(product, record.ID);
		if (productItemOwnershipStatus == ItemOwnershipStatus.OWNED)
		{
			UIStatus.Get().AddInfo(string.Format("Cannot show store where wing ownership status is {0}", productItemOwnershipStatus.ToString()), 10f);
		}
		StoreManager.Get().StartAdventureTransaction(product, record.ID, null, null, shopType, numItemsRequired, false, null, num2);
		return true;
	}

	// Token: 0x06007747 RID: 30535 RVA: 0x0026F13C File Offset: 0x0026D33C
	private bool OnProcessCheat_refreshcurrency(string func, string[] args, string rawArgs)
	{
		global::CurrencyType currencyType;
		if (args.Length < 1 || !Enum.TryParse<global::CurrencyType>(args[0], true, out currencyType))
		{
			string message = "USAGE: refreshcurrency <runestones|arcane_orbs>";
			UIStatus.Get().AddInfo(message, 10f);
			return true;
		}
		StoreManager.Get().GetCurrencyCache(currencyType).MarkDirty();
		return true;
	}

	// Token: 0x06007748 RID: 30536 RVA: 0x0026F188 File Offset: 0x0026D388
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

	// Token: 0x06007749 RID: 30537 RVA: 0x0026F1EC File Offset: 0x0026D3EC
	private bool OnProcessCheat_checkfornewquests(string func, string[] args, string rawArgs)
	{
		float delaySeconds = 0f;
		if (args.Length != 0 && !string.IsNullOrEmpty(args[0]) && !float.TryParse(args[0], out delaySeconds))
		{
			UIStatus.Get().AddInfo("checkfornewquests [delaySeconds]");
			return true;
		}
		QuestManager.Get().DebugScheduleCheckForNewQuests(delaySeconds);
		return true;
	}

	// Token: 0x0600774A RID: 30538 RVA: 0x0026F238 File Offset: 0x0026D438
	private bool OnProcessCheat_showquestnotification(string func, string[] args, string rawArgs)
	{
		QuestPool.QuestPoolType poolType = QuestPool.QuestPoolType.DAILY;
		if (args.Length != 0)
		{
			poolType = global::EnumUtils.SafeParse<QuestPool.QuestPoolType>(args[0], QuestPool.QuestPoolType.DAILY, true);
		}
		QuestManager.Get().SimulateQuestNotificationPopup(poolType);
		return true;
	}

	// Token: 0x0600774B RID: 30539 RVA: 0x0026F264 File Offset: 0x0026D464
	private bool OnProcessCheat_showquestprogresstoast(string func, string[] args, string rawArgs)
	{
		string message = "showquestprogresstoast <quest_id>";
		int num;
		if (!int.TryParse(args[0], out num))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		if (GameDbf.Quest.GetRecord(num) == null)
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		QuestManager.Get().SimulateQuestProgress(num);
		return true;
	}

	// Token: 0x0600774C RID: 30540 RVA: 0x0026F2B8 File Offset: 0x0026D4B8
	private bool OnProcessCheat_showachievementtoast(string func, string[] args, string rawArgs)
	{
		string message = "showachievementtoast <achieve_id>";
		int achievementId;
		if (!int.TryParse(args[0], out achievementId))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		AchievementDataModel achievementDataModel = AchievementManager.Get().Debug_GetAchievementDataModel(achievementId);
		if (achievementDataModel == null)
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		AchievementToast.DebugShowFake(achievementDataModel);
		return true;
	}

	// Token: 0x0600774D RID: 30541 RVA: 0x0026F308 File Offset: 0x0026D508
	private bool OnProcessCheat_showachievementreward(string func, string[] args, string rawArgs)
	{
		string message = "showachievementeward <achievement_id>";
		int achievementId;
		if (!int.TryParse(args[0], out achievementId))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		RewardScrollDataModel rewardScrollDataModel = AchievementFactory.CreateRewardScrollDataModel(achievementId, 0, null);
		if (rewardScrollDataModel == null)
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		RewardScroll.DebugShowFake(rewardScrollDataModel);
		return true;
	}

	// Token: 0x0600774E RID: 30542 RVA: 0x0026F354 File Offset: 0x0026D554
	private bool OnProcessCheat_showquestreward(string func, string[] args, string rawArgs)
	{
		string message = "showquestreward <quest_id>";
		int num;
		if (!int.TryParse(args[0], out num))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		if (GameDbf.Quest.GetRecord(num) == null)
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		RewardScroll.DebugShowFake(QuestManager.Get().CreateRewardScrollDataModelByQuestId(num, null));
		return true;
	}

	// Token: 0x0600774F RID: 30543 RVA: 0x0026F3AC File Offset: 0x0026D5AC
	private bool OnProcessCheat_showtrackreward(string func, string[] args, string rawArgs)
	{
		string message = "showtrackreward <level> <forPaidTrack>";
		int level;
		if (!int.TryParse(args[0], out level))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		bool flag = false;
		if (args.Length > 1)
		{
			bool.TryParse(args[1], out flag);
		}
		RewardTrackLevelDbfRecord rewardTrackLevelDbfRecord = (from r in RewardTrackManager.Get().RewardTrackAsset.Levels
		where r.Level == level
		select r).FirstOrDefault<RewardTrackLevelDbfRecord>();
		if (rewardTrackLevelDbfRecord == null)
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		int num = flag ? rewardTrackLevelDbfRecord.PaidRewardList : rewardTrackLevelDbfRecord.FreeRewardList;
		if (num <= 0)
		{
			if (flag)
			{
				UIStatus.Get().AddInfo(string.Format("No paid rewards for level {0}.", level));
			}
			else
			{
				UIStatus.Get().AddInfo(string.Format("No free rewards for level {0}.", level));
			}
			return true;
		}
		RewardScroll.DebugShowFake(RewardTrackFactory.CreateRewardScrollDataModel(num, level, 0, null));
		return true;
	}

	// Token: 0x06007750 RID: 30544 RVA: 0x0026F49D File Offset: 0x0026D69D
	private bool OnProcessCheat_showprogtileids(string func, string[] args, string rawArgs)
	{
		ProgressUtils.ShowDebugIds = !ProgressUtils.ShowDebugIds;
		return true;
	}

	// Token: 0x06007751 RID: 30545 RVA: 0x0026F4B0 File Offset: 0x0026D6B0
	private bool OnProcessCheat_simendofgamexp(string func, string[] args, string rawArgs)
	{
		string message = "simendofgamexp <scenario_id>";
		int scenarioId;
		if (args.Length != 1 || !int.TryParse(args[0], out scenarioId))
		{
			UIStatus.Get().AddInfo(message);
			return true;
		}
		RewardXpNotificationManager.Get().DebugSimScenario(scenarioId);
		return true;
	}

	// Token: 0x06007752 RID: 30546 RVA: 0x0026F4EE File Offset: 0x0026D6EE
	private bool OnProcessCheat_shownotavernpasswarning(string func, string[] args, string rawArgs)
	{
		Shop.OpenTavernPassErrorPopup();
		return true;
	}

	// Token: 0x06007753 RID: 30547 RVA: 0x0026F4F6 File Offset: 0x0026D6F6
	private bool OnProcessCheat_showunclaimedtrackrewards(string func, string[] args, string rawArgs)
	{
		RewardTrackSeasonRoll.DebugShowFakeForgotTrackRewards();
		return true;
	}

	// Token: 0x06007754 RID: 30548 RVA: 0x0026F500 File Offset: 0x0026D700
	private bool OnProcessCheat_setlastrewardtrackseasonseen(string func, string[] args, string rawArgs)
	{
		int num = 0;
		if (args.Length != 0 && !int.TryParse(args[0], out num))
		{
			UIStatus.Get().AddInfo("setlastrewardtrackseasonseen <season_number>");
			return true;
		}
		if (!RewardTrackManager.Get().SetRewardTrackSeasonLastSeen(num))
		{
			UIStatus.Get().AddInfo("setlastrewardtrackseasonseen failed to set GSD value");
			return true;
		}
		UIStatus.Get().AddInfo(string.Format("Last reward track season seen = {0}", num));
		return true;
	}

	// Token: 0x06007755 RID: 30549 RVA: 0x0026F56C File Offset: 0x0026D76C
	private bool OnProcessCheat_ShowAppRatingPrompt(string func, string[] args, string rawArgs)
	{
		string message = "USAGE: apprating [cmd] \nCommands: clear, show";
		if (args.Length >= 1)
		{
			if (!args.Any((string a) => string.IsNullOrEmpty(a)))
			{
				string a2 = args[0];
				if (!(a2 == "clear"))
				{
					if (a2 == "show")
					{
						MobileCallbackManager.RequestAppReview(true);
						UIStatus.Get().AddInfo("Requesting app rating prompt.");
					}
				}
				else
				{
					Options.Get().SetInt(global::Option.APP_RATING_POPUP_COUNT, 0);
					UIStatus.Get().AddInfo("Resetting app rating prompt count.");
				}
				return true;
			}
		}
		UIStatus.Get().AddInfo(message, 10f);
		return true;
	}

	// Token: 0x04005D60 RID: 23904
	private const int MAX_PLAYER_TAG_ATTRIBUTES = 20;

	// Token: 0x04005D61 RID: 23905
	private const int MAX_PLAYER_TAG_INT_VALUE = 999999;

	// Token: 0x04005D62 RID: 23906
	public const char CONFIG_CHEAT_PLAYER_TAG_DELIMITER = ',';

	// Token: 0x04005D63 RID: 23907
	public const char CONFIG_CHEAT_PLAYER_TAG_VALUE_DELIMITER = '=';

	// Token: 0x04005D64 RID: 23908
	public const string CONFIG_INSTANT_GAMEPLAY_KEY = "Cheats.InstantGameplay";

	// Token: 0x04005D65 RID: 23909
	public const string CONFIG_INSTANT_CHEAT_COMMANDS_KEY = "Cheats.InstantCheatCommands";

	// Token: 0x04005D66 RID: 23910
	public const char CONFIG_INSTANT_CHEAT_COMMANDS_DELIMITER = ',';

	// Token: 0x04005D67 RID: 23911
	public readonly Vector3 SPEECH_BUBBLE_HIDDEN_POSITION = new Vector3(15000f, 0f, 0f);

	// Token: 0x04005D68 RID: 23912
	private static Cheats s_instance;

	// Token: 0x04005D69 RID: 23913
	private bool m_isInGameplayScene;

	// Token: 0x04005D6A RID: 23914
	private int m_boardId;

	// Token: 0x04005D6B RID: 23915
	private string m_playerTags;

	// Token: 0x04005D6C RID: 23916
	private bool m_speechBubblesEnabled = true;

	// Token: 0x04005D6D RID: 23917
	private global::Map<Global.SoundCategory, bool> m_audioChannelEnabled = Cheats.InitAudioChannelMap();

	// Token: 0x04005D6E RID: 23918
	private Queue<int> m_pvpdrTreasureIds = new Queue<int>();

	// Token: 0x04005D6F RID: 23919
	private Queue<int> m_pvpdrLootIds = new Queue<int>();

	// Token: 0x04005D70 RID: 23920
	private global::Map<string, List<Global.SoundCategory>> m_audioChannelGroups = new global::Map<string, List<Global.SoundCategory>>
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

	// Token: 0x04005D71 RID: 23921
	private bool m_loadingStoreChallengePrompt;

	// Token: 0x04005D72 RID: 23922
	private StoreChallengePrompt m_storeChallengePrompt;

	// Token: 0x04005D73 RID: 23923
	private bool m_isNewCardInPackOpeningEnabled;

	// Token: 0x04005D74 RID: 23924
	private AlertPopup m_alert;

	// Token: 0x04005D75 RID: 23925
	private static readonly global::Map<KeyCode, ScenarioDbId> s_quickPlayKeyMap = new global::Map<KeyCode, ScenarioDbId>
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

	// Token: 0x04005D76 RID: 23926
	private static readonly global::Map<KeyCode, string> s_opponentHeroKeyMap = new global::Map<KeyCode, string>
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

	// Token: 0x04005D77 RID: 23927
	private const string IPSUM_PARAGRAPH = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec ut rhoncus ante. Donec in pretium felis. Duis mollis purus a ante mollis luctus. Nulla hendrerit gravida nulla non convallis. Vivamus vel ligula a mi porta porta et at magna. Nulla euismod diam eget arcu pharetra scelerisque. In id sem a ipsum maximus cursus. In pulvinar fermentum dolor, at ultrices ipsum congue nec.";

	// Token: 0x04005D78 RID: 23928
	private const string IPSUM_TITLE = "Lorem Ipsum";

	// Token: 0x04005D79 RID: 23929
	private Cheats.QuickLaunchState m_quickLaunchState = new Cheats.QuickLaunchState();

	// Token: 0x04005D7A RID: 23930
	private bool m_skipSendingGetGameState;

	// Token: 0x04005D7B RID: 23931
	public static float VOChanceOverride = -1f;

	// Token: 0x04005D7C RID: 23932
	private float m_waitTime = 10f;

	// Token: 0x04005D7D RID: 23933
	private bool m_showedMessage;

	// Token: 0x04005D7E RID: 23934
	private List<WidgetInstance> s_createdWidgets = new List<WidgetInstance>();

	// Token: 0x04005D80 RID: 23936
	private static bool s_hasSubscribedToPartyEvents = false;

	// Token: 0x04005D81 RID: 23937
	private string[] m_lastUtilServerCmd;

	// Token: 0x02002497 RID: 9367
	private enum QuickLaunchAvailability
	{
		// Token: 0x0400EAF9 RID: 60153
		OK,
		// Token: 0x0400EAFA RID: 60154
		FINDING_GAME,
		// Token: 0x0400EAFB RID: 60155
		ACTIVE_GAME,
		// Token: 0x0400EAFC RID: 60156
		SCENE_TRANSITION,
		// Token: 0x0400EAFD RID: 60157
		COLLECTION_NOT_READY
	}

	// Token: 0x02002498 RID: 9368
	private enum FriendListType
	{
		// Token: 0x0400EAFF RID: 60159
		FRIEND,
		// Token: 0x0400EB00 RID: 60160
		NEARBY,
		// Token: 0x0400EB01 RID: 60161
		FSG
	}

	// Token: 0x02002499 RID: 9369
	private class QuickLaunchState
	{
		// Token: 0x0400EB02 RID: 60162
		public bool m_launching;

		// Token: 0x0400EB03 RID: 60163
		public bool m_skipMulligan;

		// Token: 0x0400EB04 RID: 60164
		public bool m_flipHeroes;

		// Token: 0x0400EB05 RID: 60165
		public bool m_mirrorHeroes;

		// Token: 0x0400EB06 RID: 60166
		public string m_opponentHeroCardId;
	}

	// Token: 0x0200249A RID: 9370
	private struct NamedParam
	{
		// Token: 0x0601300C RID: 77836 RVA: 0x005243E4 File Offset: 0x005225E4
		public NamedParam(string param)
		{
			this.Text = param;
			this.Number = 0;
			int number;
			if (GeneralUtils.TryParseInt(param, out number))
			{
				this.Number = number;
			}
		}

		// Token: 0x17002B42 RID: 11074
		// (get) Token: 0x0601300D RID: 77837 RVA: 0x00524410 File Offset: 0x00522610
		// (set) Token: 0x0601300E RID: 77838 RVA: 0x00524418 File Offset: 0x00522618
		public string Text { get; private set; }

		// Token: 0x17002B43 RID: 11075
		// (get) Token: 0x0601300F RID: 77839 RVA: 0x00524421 File Offset: 0x00522621
		// (set) Token: 0x06013010 RID: 77840 RVA: 0x00524429 File Offset: 0x00522629
		public int Number { get; private set; }

		// Token: 0x17002B44 RID: 11076
		// (get) Token: 0x06013011 RID: 77841 RVA: 0x00524432 File Offset: 0x00522632
		public bool HasNumber
		{
			get
			{
				return this.Number > 0;
			}
		}
	}

	// Token: 0x0200249B RID: 9371
	// (Invoke) Token: 0x06013013 RID: 77843
	public delegate void LogFormatFunc(string format, params object[] args);

	// Token: 0x0200249C RID: 9372
	private enum SetAdventureProgressMode
	{
		// Token: 0x0400EB0A RID: 60170
		Victory,
		// Token: 0x0400EB0B RID: 60171
		Defeat,
		// Token: 0x0400EB0C RID: 60172
		Progress
	}
}
