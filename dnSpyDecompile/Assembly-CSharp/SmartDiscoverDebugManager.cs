using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Hearthstone;
using UnityEngine;

// Token: 0x020002A2 RID: 674
public class SmartDiscoverDebugManager : MonoBehaviour
{
	// Token: 0x060021F4 RID: 8692 RVA: 0x000A75F0 File Offset: 0x000A57F0
	public static SmartDiscoverDebugManager Get()
	{
		if (SmartDiscoverDebugManager.s_instance == null)
		{
			GameObject gameObject = new GameObject();
			SmartDiscoverDebugManager.s_instance = gameObject.AddComponent<SmartDiscoverDebugManager>();
			gameObject.name = "SmartDiscoverDebugManager (Dynamically created)";
		}
		return SmartDiscoverDebugManager.s_instance;
	}

	// Token: 0x060021F5 RID: 8693 RVA: 0x000A761E File Offset: 0x000A581E
	public bool RequiresWaiting(string line)
	{
		return this.m_endRegex.Match(line).Success;
	}

	// Token: 0x060021F6 RID: 8694 RVA: 0x000A7631 File Offset: 0x000A5831
	public bool PreprocessCommand(string line)
	{
		if (this.m_endRegex.Match(line).Success)
		{
			Network.Get().SendDebugConsoleCommand("spawncard XXX_56633 friendly play 0");
			return true;
		}
		return false;
	}

	// Token: 0x060021F7 RID: 8695 RVA: 0x000A765C File Offset: 0x000A585C
	public bool ParseCheatCommand(string line)
	{
		Match match = this.m_fileOpenRegex.Match(line);
		if (match.Success)
		{
			Log.SmartDiscover.PurgeFile();
			return true;
		}
		match = this.m_beginRegex.Match(line);
		if (match.Success)
		{
			Network.Get().SendDebugConsoleCommand("settag 1324 1 0");
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.SMART_DISCOVER_DEBUG_TEST_COMPLETE, 0);
			string message = string.Format("Begin Smart Discover Test: {0}", match.Groups[1].Value);
			Log.SmartDiscover.ForceFilePrint(Log.LogLevel.Info, message);
			this.m_currentTestName = match.Groups[1].Value;
			this.m_expectedResults.Clear();
			return true;
		}
		match = this.m_descriptionRegex.Match(line);
		if (match.Success)
		{
			Log.SmartDiscover.ForceFilePrint(Log.LogLevel.Info, match.Groups[1].Value);
			return true;
		}
		match = this.m_testExpectsThreeResultsRegex.Match(line);
		if (match.Success)
		{
			this.ParseExpectedResultsCommand(match, 3);
			return true;
		}
		match = this.m_testExpectsTwoResultsRegex.Match(line);
		if (match.Success)
		{
			this.ParseExpectedResultsCommand(match, 2);
			return true;
		}
		match = this.m_testExpectsOneResultRegex.Match(line);
		if (match.Success)
		{
			this.ParseExpectedResultsCommand(match, 1);
			return true;
		}
		match = this.m_endRegex.Match(line);
		if (match.Success)
		{
			this.ParseEndCommand(match);
			return true;
		}
		return false;
	}

	// Token: 0x060021F8 RID: 8696 RVA: 0x000A77C0 File Offset: 0x000A59C0
	private void ParseExpectedResultsCommand(Match match, int expectedResultsCount)
	{
		this.m_expectedResults.Clear();
		List<string> list = new List<string>();
		string text = "Expected results:";
		for (int i = 1; i <= expectedResultsCount; i++)
		{
			string value = match.Groups[i].Value;
			this.m_expectedResults.Add(value);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(value);
			if (entityDef != null)
			{
				list.Add(entityDef.GetName());
			}
			else
			{
				list.Add(string.Format("UNRECOGNIZED CARD ID: {0}", value));
			}
			text = string.Format("{0} {1}", text, list[list.Count - 1]);
		}
		Log.SmartDiscover.ForceFilePrint(Log.LogLevel.Info, text);
	}

	// Token: 0x060021F9 RID: 8697 RVA: 0x000A7868 File Offset: 0x000A5A68
	private void ParseEndCommand(Match match)
	{
		bool flag = true;
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		string arg = "CHOICE_1_INVALID";
		string arg2 = "CHOICE_2_INVALID";
		string arg3 = "CHOICE_3_INVALID";
		EntityDef entityDef = DefLoader.Get().GetEntityDef(friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_1), false);
		if (entityDef != null)
		{
			arg = entityDef.GetName();
		}
		entityDef = DefLoader.Get().GetEntityDef(friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_2), false);
		if (entityDef != null)
		{
			arg2 = entityDef.GetName();
		}
		entityDef = DefLoader.Get().GetEntityDef(friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_3), false);
		if (entityDef != null)
		{
			arg3 = entityDef.GetName();
		}
		string message = string.Format("Received results: {0}, {1}, {2}", arg, arg2, arg3);
		Log.SmartDiscover.ForceFilePrint(Log.LogLevel.Info, message);
		foreach (string cardId in this.m_expectedResults)
		{
			int num = GameUtils.TranslateCardIdToDbId(cardId, false);
			if (num == 0)
			{
				flag = false;
				break;
			}
			if (num != friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_1) && num != friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_2) && num != friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_3))
			{
				flag = false;
				break;
			}
		}
		string message2 = string.Format("Test {0} {1}\n", this.m_currentTestName, flag ? "passed" : "FAILED");
		Log.SmartDiscover.ForceFilePrint(Log.LogLevel.Info, message2);
	}

	// Token: 0x060021FA RID: 8698 RVA: 0x000A79D0 File Offset: 0x000A5BD0
	private void Update()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		GameState gameState = GameState.Get();
		if (gameState == null)
		{
			return;
		}
		Player friendlySidePlayer = gameState.GetFriendlySidePlayer();
		if (friendlySidePlayer == null)
		{
			return;
		}
		int tag = friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_1);
		if (tag != 0)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
			string arg = "Unknown";
			if (entityDef != null)
			{
				arg = entityDef.GetName();
			}
			tag = friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_2);
			entityDef = DefLoader.Get().GetEntityDef(tag, true);
			string arg2 = "Unknown";
			if (entityDef != null)
			{
				arg2 = entityDef.GetName();
			}
			tag = friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_3);
			entityDef = DefLoader.Get().GetEntityDef(tag, true);
			string arg3 = "Unknown";
			if (entityDef != null)
			{
				arg3 = entityDef.GetName();
			}
			string text = string.Format("Results:\n1. {0}\n2. {1}\n3. {2}", arg, arg2, arg3);
			Vector3 position = new Vector3((float)Screen.width, (float)Screen.height, 0f);
			DebugTextManager.Get().DrawDebugText(text, position, 0f, true, "", null);
		}
		string text2 = this.GetStringForPassiveResults(friendlySidePlayer);
		if (text2 == "")
		{
			text2 = this.GetStringForPassiveResults(gameState.GetOpposingSidePlayer());
		}
		else
		{
			string stringForPassiveResults = this.GetStringForPassiveResults(gameState.GetOpposingSidePlayer());
			if (stringForPassiveResults != "")
			{
				text2 = string.Format("{0}\n\n{1}", text2, stringForPassiveResults);
			}
		}
		if (text2 != "")
		{
			Vector3 position2 = new Vector3((float)Screen.width, 0f, 0f);
			DebugTextManager.Get().DrawDebugText(text2, position2, 0f, true, "", null);
		}
	}

	// Token: 0x060021FB RID: 8699 RVA: 0x000A7B5C File Offset: 0x000A5D5C
	private string GetStringForPassiveResults(Player player)
	{
		if (GameState.Get() == null)
		{
			return "";
		}
		if (player == null)
		{
			return "";
		}
		int tag = player.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_PASSIVE_EVAL_RESULT_1);
		if (tag == 0)
		{
			return "";
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(tag, true);
		string text = "Unknown";
		if (entityDef != null)
		{
			text = entityDef.GetName();
		}
		tag = player.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_PASSIVE_EVAL_RESULT_2);
		entityDef = DefLoader.Get().GetEntityDef(tag, true);
		string text2 = "Unknown";
		if (entityDef != null)
		{
			text2 = entityDef.GetName();
		}
		tag = player.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_PASSIVE_EVAL_RESULT_3);
		entityDef = DefLoader.Get().GetEntityDef(tag, true);
		string text3 = "Unknown";
		if (entityDef != null)
		{
			text3 = entityDef.GetName();
		}
		return string.Format("Passive Results for {0}:\n1. {1}\n2. {2}\n3. {3}", new object[]
		{
			player.GetName(),
			text,
			text2,
			text3
		});
	}

	// Token: 0x040012C0 RID: 4800
	private static SmartDiscoverDebugManager s_instance;

	// Token: 0x040012C1 RID: 4801
	private Regex m_fileOpenRegex = new Regex("beginsmartdiscoverreport");

	// Token: 0x040012C2 RID: 4802
	private Regex m_beginRegex = new Regex("beginsmartdiscovertest (?<testName>.+)");

	// Token: 0x040012C3 RID: 4803
	private Regex m_descriptionRegex = new Regex("smartdiscovertestdescription (?<testString>.+)");

	// Token: 0x040012C4 RID: 4804
	private Regex m_testExpectsOneResultRegex = new Regex("smartdiscovertestexpectresult (?<cardId1>[^\\s]+)");

	// Token: 0x040012C5 RID: 4805
	private Regex m_testExpectsTwoResultsRegex = new Regex("smartdiscovertestexpectresult (?<cardId1>[^\\s]+) (?<cardId2>[^\\s]+)");

	// Token: 0x040012C6 RID: 4806
	private Regex m_testExpectsThreeResultsRegex = new Regex("smartdiscovertestexpectresult (?<cardId1>[^\\s]+) (?<cardId2>[^\\s]+) (?<cardId3>[^\\s]+)");

	// Token: 0x040012C7 RID: 4807
	private Regex m_endRegex = new Regex("endsmartdiscovertest");

	// Token: 0x040012C8 RID: 4808
	private List<string> m_expectedResults = new List<string>();

	// Token: 0x040012C9 RID: 4809
	private string m_currentTestName = "";
}
