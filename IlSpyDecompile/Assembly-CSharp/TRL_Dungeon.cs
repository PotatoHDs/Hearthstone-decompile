using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRL_Dungeon : TRL_MissionEntity
{
	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Killed_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Killed_01.prefab:6310971ec24c8b04c855b61ab0de2ee7");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Killed_02 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Killed_02.prefab:69df2bf9c601e064ebe4b0fcc9b7bff0");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Killed_03 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Killed_03.prefab:80a489f9a60de644d94dd836da122249");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Killed_04 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Killed_04.prefab:8f1be59482206f046a5bc90d77e5a215");

	public static float f_shrineKillPlayRate = 1f;

	public static string s_deathLine = null;

	public static string s_responseLineGreeting = null;

	public static string s_responseLineWow = null;

	public static string s_responseLineThreaten = null;

	public static string s_responseLineWellPlayed = null;

	public static string s_responseLineThanks = null;

	public static string s_responseLineOops = null;

	public static string s_responseLineSorry = null;

	public static List<string> s_bossShrineDeathLines = new List<string>();

	public static List<string> s_genericShrineDeathLines = new List<string>();

	public static List<string> s_druidShrineDeathLines = new List<string>();

	public static List<string> s_hunterShrineDeathLines = new List<string>();

	public static List<string> s_mageShrineDeathLines = new List<string>();

	public static List<string> s_paladinShrineDeathLines = new List<string>();

	public static List<string> s_priestShrineDeathLines = new List<string>();

	public static List<string> s_rogueShrineDeathLines = new List<string>();

	public static List<string> s_shamanShrineDeathLines = new List<string>();

	public static List<string> s_warlockShrineDeathLines = new List<string>();

	public static List<string> s_warriorShrineDeathLines = new List<string>();

	protected static List<string> m_RikkarRandomLines = new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Killed_01, VO_TRLA_209h_Male_Troll_Shrine_Killed_02, VO_TRLA_209h_Male_Troll_Shrine_Killed_03, VO_TRLA_209h_Male_Troll_Shrine_Killed_04 };

	public static TRL_Dungeon InstantiateTRLDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		switch (opposingHeroCardID)
		{
		case "TRLA_200h":
			return new TRL_Dungeon_Boss_200h();
		case "TRLA_201h":
			return new TRL_Dungeon_Boss_201h();
		case "TRLA_202h":
			return new TRL_Dungeon_Boss_202h();
		case "TRLA_203h":
			return new TRL_Dungeon_Boss_203h();
		case "TRLA_204h":
			return new TRL_Dungeon_Boss_204h();
		case "TRLA_205h":
			return new TRL_Dungeon_Boss_205h();
		case "TRLA_206h":
			return new TRL_Dungeon_Boss_206h();
		case "TRLA_207h":
			return new TRL_Dungeon_Boss_207h();
		case "TRLA_208h":
			return new TRL_Dungeon_Boss_208h();
		default:
			Log.All.PrintError("TRL_Dungeon.InstantiateTRLDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", opposingHeroCardID);
			return new TRL_Dungeon();
		}
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Killed_01, VO_TRLA_209h_Male_Troll_Shrine_Killed_02, VO_TRLA_209h_Male_Troll_Shrine_Killed_03, VO_TRLA_209h_Male_Troll_Shrine_Killed_04 })
		{
			PreloadSound(item);
		}
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (!m_enemySpeaking && !string.IsNullOrEmpty(s_deathLine) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (GetShouldSupressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(s_deathLine, Notification.SpeechBubbleDirection.None, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(s_deathLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (emoteType)
		{
		case EmoteType.GREETINGS:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(s_responseLineGreeting, Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.WOW:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(s_responseLineWow, Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.THREATEN:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(s_responseLineThreaten, Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.WELL_PLAYED:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(s_responseLineWellPlayed, Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.THANKS:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(s_responseLineThanks, Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.OOPS:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(s_responseLineOops, Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case EmoteType.SORRY:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(s_responseLineSorry, Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		s_deathLine = null;
		s_responseLineGreeting = null;
		s_responseLineWow = null;
		s_responseLineThreaten = null;
		s_responseLineWellPlayed = null;
		s_responseLineThanks = null;
		s_responseLineOops = null;
		s_responseLineSorry = null;
		s_bossShrineDeathLines = new List<string>();
		s_genericShrineDeathLines = new List<string>();
		s_druidShrineDeathLines = new List<string>();
		s_hunterShrineDeathLines = new List<string>();
		s_mageShrineDeathLines = new List<string>();
		s_paladinShrineDeathLines = new List<string>();
		s_priestShrineDeathLines = new List<string>();
		s_rogueShrineDeathLines = new List<string>();
		s_shamanShrineDeathLines = new List<string>();
		s_warlockShrineDeathLines = new List<string>();
		s_warriorShrineDeathLines = new List<string>();
	}

	protected virtual bool GetShouldSupressDeathTextBubble()
	{
		return false;
	}

	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	private IEnumerator PlayRandomRikkarShrineDeathLine()
	{
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		if (!GameState.Get().IsFriendlySidePlayerTurn() && CanPlayVOLines(actor.GetEntity(), VOSpeaker.FRIENDLY_HERO))
		{
			string text = PopRandomLineWithChance(m_RikkarRandomLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}

	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = PopRandomLine(lines);
		if (text != null)
		{
			yield return PlayLineOnlyOnce(actor, text);
		}
	}

	protected string PopRandomLine(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		string text = lines[Random.Range(0, lines.Count)];
		lines.Remove(text);
		return text;
	}

	private IEnumerator PlayClassOrGenericShrineDeathLine(List<string> classDeathLines)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string text = PopRandomLineWithChance(classDeathLines);
		if (text != null)
		{
			yield return PlayLineOnlyOnce(actor, text);
			yield break;
		}
		text = PopRandomLineWithChance(s_genericShrineDeathLines);
		if (text != null)
		{
			yield return PlayLineOnlyOnce(actor, text);
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 150:
			yield return PlayClassOrGenericShrineDeathLine(s_genericShrineDeathLines);
			yield return PlayRandomRikkarShrineDeathLine();
			break;
		case 152:
			yield return PlayClassOrGenericShrineDeathLine(s_druidShrineDeathLines);
			yield return PlayRandomRikkarShrineDeathLine();
			break;
		case 153:
			yield return PlayClassOrGenericShrineDeathLine(s_hunterShrineDeathLines);
			yield return PlayRandomRikkarShrineDeathLine();
			break;
		case 154:
			yield return PlayClassOrGenericShrineDeathLine(s_mageShrineDeathLines);
			yield return PlayRandomRikkarShrineDeathLine();
			break;
		case 155:
			yield return PlayClassOrGenericShrineDeathLine(s_paladinShrineDeathLines);
			yield return PlayRandomRikkarShrineDeathLine();
			break;
		case 156:
			yield return PlayClassOrGenericShrineDeathLine(s_priestShrineDeathLines);
			yield return PlayRandomRikkarShrineDeathLine();
			break;
		case 157:
			yield return PlayClassOrGenericShrineDeathLine(s_rogueShrineDeathLines);
			yield return PlayRandomRikkarShrineDeathLine();
			break;
		case 158:
			yield return PlayClassOrGenericShrineDeathLine(s_shamanShrineDeathLines);
			yield return PlayRandomRikkarShrineDeathLine();
			break;
		case 159:
			yield return PlayClassOrGenericShrineDeathLine(s_warlockShrineDeathLines);
			yield return PlayRandomRikkarShrineDeathLine();
			break;
		case 160:
			yield return PlayClassOrGenericShrineDeathLine(s_warriorShrineDeathLines);
			yield return PlayRandomRikkarShrineDeathLine();
			break;
		case 202:
		{
			string text = PopRandomLineWithChance(s_bossShrineDeathLines);
			if (text != null)
			{
				yield return PlayBossLine(actor, text);
			}
			break;
		}
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}
}
