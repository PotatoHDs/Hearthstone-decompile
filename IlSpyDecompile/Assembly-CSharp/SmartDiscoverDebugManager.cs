using System.Collections.Generic;
using System.Text.RegularExpressions;
using Hearthstone;
using UnityEngine;

public class SmartDiscoverDebugManager : MonoBehaviour
{
	private static SmartDiscoverDebugManager s_instance;

	private Regex m_fileOpenRegex = new Regex("beginsmartdiscoverreport");

	private Regex m_beginRegex = new Regex("beginsmartdiscovertest (?<testName>.+)");

	private Regex m_descriptionRegex = new Regex("smartdiscovertestdescription (?<testString>.+)");

	private Regex m_testExpectsOneResultRegex = new Regex("smartdiscovertestexpectresult (?<cardId1>[^\\s]+)");

	private Regex m_testExpectsTwoResultsRegex = new Regex("smartdiscovertestexpectresult (?<cardId1>[^\\s]+) (?<cardId2>[^\\s]+)");

	private Regex m_testExpectsThreeResultsRegex = new Regex("smartdiscovertestexpectresult (?<cardId1>[^\\s]+) (?<cardId2>[^\\s]+) (?<cardId3>[^\\s]+)");

	private Regex m_endRegex = new Regex("endsmartdiscovertest");

	private List<string> m_expectedResults = new List<string>();

	private string m_currentTestName = "";

	public static SmartDiscoverDebugManager Get()
	{
		if (s_instance == null)
		{
			GameObject obj = new GameObject();
			s_instance = obj.AddComponent<SmartDiscoverDebugManager>();
			obj.name = "SmartDiscoverDebugManager (Dynamically created)";
		}
		return s_instance;
	}

	public bool RequiresWaiting(string line)
	{
		return m_endRegex.Match(line).Success;
	}

	public bool PreprocessCommand(string line)
	{
		if (m_endRegex.Match(line).Success)
		{
			Network.Get().SendDebugConsoleCommand("spawncard XXX_56633 friendly play 0");
			return true;
		}
		return false;
	}

	public bool ParseCheatCommand(string line)
	{
		Match match = m_fileOpenRegex.Match(line);
		if (match.Success)
		{
			Log.SmartDiscover.PurgeFile();
			return true;
		}
		match = m_beginRegex.Match(line);
		if (match.Success)
		{
			Network.Get().SendDebugConsoleCommand("settag 1324 1 0");
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.SMART_DISCOVER_DEBUG_TEST_COMPLETE, 0);
			string message = $"Begin Smart Discover Test: {match.Groups[1].Value}";
			Log.SmartDiscover.ForceFilePrint(Log.LogLevel.Info, message);
			m_currentTestName = match.Groups[1].Value;
			m_expectedResults.Clear();
			return true;
		}
		match = m_descriptionRegex.Match(line);
		if (match.Success)
		{
			Log.SmartDiscover.ForceFilePrint(Log.LogLevel.Info, match.Groups[1].Value);
			return true;
		}
		match = m_testExpectsThreeResultsRegex.Match(line);
		if (match.Success)
		{
			ParseExpectedResultsCommand(match, 3);
			return true;
		}
		match = m_testExpectsTwoResultsRegex.Match(line);
		if (match.Success)
		{
			ParseExpectedResultsCommand(match, 2);
			return true;
		}
		match = m_testExpectsOneResultRegex.Match(line);
		if (match.Success)
		{
			ParseExpectedResultsCommand(match, 1);
			return true;
		}
		match = m_endRegex.Match(line);
		if (match.Success)
		{
			ParseEndCommand(match);
			return true;
		}
		return false;
	}

	private void ParseExpectedResultsCommand(Match match, int expectedResultsCount)
	{
		m_expectedResults.Clear();
		List<string> list = new List<string>();
		string text = "Expected results:";
		for (int i = 1; i <= expectedResultsCount; i++)
		{
			string value = match.Groups[i].Value;
			m_expectedResults.Add(value);
			EntityDef entityDef = DefLoader.Get().GetEntityDef(value);
			if (entityDef != null)
			{
				list.Add(entityDef.GetName());
			}
			else
			{
				list.Add($"UNRECOGNIZED CARD ID: {value}");
			}
			text = $"{text} {list[list.Count - 1]}";
		}
		Log.SmartDiscover.ForceFilePrint(Log.LogLevel.Info, text);
	}

	private void ParseEndCommand(Match match)
	{
		bool flag = true;
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		string arg = "CHOICE_1_INVALID";
		string arg2 = "CHOICE_2_INVALID";
		string arg3 = "CHOICE_3_INVALID";
		EntityDef entityDef = DefLoader.Get().GetEntityDef(friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_1), displayError: false);
		if (entityDef != null)
		{
			arg = entityDef.GetName();
		}
		entityDef = DefLoader.Get().GetEntityDef(friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_2), displayError: false);
		if (entityDef != null)
		{
			arg2 = entityDef.GetName();
		}
		entityDef = DefLoader.Get().GetEntityDef(friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_3), displayError: false);
		if (entityDef != null)
		{
			arg3 = entityDef.GetName();
		}
		string message = $"Received results: {arg}, {arg2}, {arg3}";
		Log.SmartDiscover.ForceFilePrint(Log.LogLevel.Info, message);
		foreach (string expectedResult in m_expectedResults)
		{
			int num = GameUtils.TranslateCardIdToDbId(expectedResult);
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
		string message2 = string.Format("Test {0} {1}\n", m_currentTestName, flag ? "passed" : "FAILED");
		Log.SmartDiscover.ForceFilePrint(Log.LogLevel.Info, message2);
	}

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
		int num = friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_1);
		if (num != 0)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(num);
			string arg = "Unknown";
			if (entityDef != null)
			{
				arg = entityDef.GetName();
			}
			num = friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_2);
			entityDef = DefLoader.Get().GetEntityDef(num);
			string arg2 = "Unknown";
			if (entityDef != null)
			{
				arg2 = entityDef.GetName();
			}
			num = friendlySidePlayer.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_ENTITY_3);
			entityDef = DefLoader.Get().GetEntityDef(num);
			string arg3 = "Unknown";
			if (entityDef != null)
			{
				arg3 = entityDef.GetName();
			}
			string text = $"Results:\n1. {arg}\n2. {arg2}\n3. {arg3}";
			Vector3 position = new Vector3(Screen.width, Screen.height, 0f);
			DebugTextManager.Get().DrawDebugText(text, position, 0f, screenSpace: true);
		}
		string text2 = GetStringForPassiveResults(friendlySidePlayer);
		if (text2 == "")
		{
			text2 = GetStringForPassiveResults(gameState.GetOpposingSidePlayer());
		}
		else
		{
			string stringForPassiveResults = GetStringForPassiveResults(gameState.GetOpposingSidePlayer());
			if (stringForPassiveResults != "")
			{
				text2 = $"{text2}\n\n{stringForPassiveResults}";
			}
		}
		if (text2 != "")
		{
			Vector3 position2 = new Vector3(Screen.width, 0f, 0f);
			DebugTextManager.Get().DrawDebugText(text2, position2, 0f, screenSpace: true);
		}
	}

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
		int num = player.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_PASSIVE_EVAL_RESULT_1);
		if (num == 0)
		{
			return "";
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(num);
		string text = "Unknown";
		if (entityDef != null)
		{
			text = entityDef.GetName();
		}
		num = player.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_PASSIVE_EVAL_RESULT_2);
		entityDef = DefLoader.Get().GetEntityDef(num);
		string text2 = "Unknown";
		if (entityDef != null)
		{
			text2 = entityDef.GetName();
		}
		num = player.GetTag(GAME_TAG.SMART_DISCOVER_DEBUG_PASSIVE_EVAL_RESULT_3);
		entityDef = DefLoader.Get().GetEntityDef(num);
		string text3 = "Unknown";
		if (entityDef != null)
		{
			text3 = entityDef.GetName();
		}
		return $"Passive Results for {player.GetName()}:\n1. {text}\n2. {text2}\n3. {text3}";
	}
}
