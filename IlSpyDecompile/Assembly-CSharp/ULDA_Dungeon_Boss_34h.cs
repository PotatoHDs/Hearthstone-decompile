using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_34h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_BossClockworkGoblin_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_BossClockworkGoblin_01.prefab:91bc077876ba53a448a96242ae45328c");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_BossGoblinBomb_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_BossGoblinBomb_01.prefab:15404e1dcfec4624880bf09727297f4a");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_BossLackey_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_BossLackey_01.prefab:97b045826fe5d0c488dc8982e4253d51");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_DeathALT_01.prefab:9db63e2241b5b944cb5be6c487b06c58");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_DefeatPlayer_01.prefab:90b22ed56abb8334bb7302f1a239ee50");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_EmoteResponse_01.prefab:990eaba8ea6aa3747ac4822cbfd25385");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_01.prefab:c32ee29ec4d5a934ea42ac6d087538c2");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_02 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_02.prefab:dcdb14588146c43478ea87777ce174c0");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_03 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_03.prefab:2aab27e0292f9804c820d9071c3c331d");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_04 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_04.prefab:3033bc3518b12584dbac8eb01a012b79");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_05 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_05.prefab:3d6ae0b1c3976b545a4837aec2868980");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_Idle1_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_Idle1_01.prefab:c3cd3f19913c8e8439a52abf00409170");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_Idle2_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_Idle2_01.prefab:0cddde1666c4330408cdac1cf647965c");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_Idle3_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_Idle3_01.prefab:fcaf02acb0ef21f42af93bdba2c5c937");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_Intro_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_Intro_01.prefab:22094abad3abdcf40abf5576dcfa51da");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_IntroBrann_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_IntroBrann_01.prefab:d4eeb747a0cbbea4683fa855c2c1883b");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_PlayerBomb_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_PlayerBomb_01.prefab:3b734ce79b6f68c4cb9d16441f9f7726");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_PlayerJrExplorer_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_PlayerJrExplorer_01.prefab:4912da1a58558b64d85e6e68c85a962b");

	private static readonly AssetReference VO_ULDA_BOSS_34h_Male_Goblin_PlayerPlagueSpell_01 = new AssetReference("VO_ULDA_BOSS_34h_Male_Goblin_PlayerPlagueSpell_01.prefab:dd1e35917d4245a49b6241f4c4bb7e35");

	private List<string> m_HeroPowerLines = new List<string> { VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_01, VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_02, VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_03, VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_04, VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_05 };

	private List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_34h_Male_Goblin_Idle1_01, VO_ULDA_BOSS_34h_Male_Goblin_Idle2_01, VO_ULDA_BOSS_34h_Male_Goblin_Idle3_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_34h_Male_Goblin_BossClockworkGoblin_01, VO_ULDA_BOSS_34h_Male_Goblin_BossGoblinBomb_01, VO_ULDA_BOSS_34h_Male_Goblin_BossLackey_01, VO_ULDA_BOSS_34h_Male_Goblin_DeathALT_01, VO_ULDA_BOSS_34h_Male_Goblin_DefeatPlayer_01, VO_ULDA_BOSS_34h_Male_Goblin_EmoteResponse_01, VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_01, VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_02, VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_03, VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_04,
			VO_ULDA_BOSS_34h_Male_Goblin_HeroPower_05, VO_ULDA_BOSS_34h_Male_Goblin_Idle1_01, VO_ULDA_BOSS_34h_Male_Goblin_Idle2_01, VO_ULDA_BOSS_34h_Male_Goblin_Idle3_01, VO_ULDA_BOSS_34h_Male_Goblin_Intro_01, VO_ULDA_BOSS_34h_Male_Goblin_IntroBrann_01, VO_ULDA_BOSS_34h_Male_Goblin_PlayerBomb_01, VO_ULDA_BOSS_34h_Male_Goblin_PlayerJrExplorer_01, VO_ULDA_BOSS_34h_Male_Goblin_PlayerPlagueSpell_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_ULDA_BOSS_34h_Male_Goblin_Intro_01;
		m_deathLine = VO_ULDA_BOSS_34h_Male_Goblin_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_34h_Male_Goblin_EmoteResponse_01;
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer.IsFriendlySide() && !currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			if (m_IdleLines.Count != 0)
			{
				string line = m_IdleLines[0];
				m_IdleLines.RemoveAt(0);
				Gameplay.Get().StartCoroutine(PlayBossLine(actor, line));
			}
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Brann")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_34h_Male_Goblin_IntroBrann_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (missionEvent == 101)
		{
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_34h_Male_Goblin_PlayerBomb_01);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ULDA_015":
			case "ULDA_016":
			case "ULDA_017":
			case "ULDA_018":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_34h_Male_Goblin_PlayerJrExplorer_01);
				break;
			case "ULD_172":
			case "ULD_707":
			case "ULD_715":
			case "ULD_717":
			case "ULD_718":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_34h_Male_Goblin_PlayerPlagueSpell_01);
				break;
			}
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "DAL_060":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_34h_Male_Goblin_BossClockworkGoblin_01);
				break;
			case "BOT_031":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_34h_Male_Goblin_BossGoblinBomb_01);
				break;
			case "DAL_615":
			case "DAL_614":
			case "DAL_739":
			case "ULD_616":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_34h_Male_Goblin_BossLackey_01);
				break;
			}
		}
	}
}
