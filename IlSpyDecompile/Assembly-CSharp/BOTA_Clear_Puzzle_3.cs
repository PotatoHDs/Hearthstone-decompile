using System.Collections;
using System.Collections.Generic;

public class BOTA_Clear_Puzzle_3 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Complete_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Complete_01.prefab:493eb06ad9050674fbb54c671609d583");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_01.prefab:f6e2731b7ce04aa4fbf7dc03f2c29ea8");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_02.prefab:1f451226373cccc4ca41fddcc3088dea");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_03.prefab:ba0dc202f1ee51541b98eb87f076f666");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_01.prefab:7ce8ca09c94329449b045f6f90b55fb9");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_02 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_02.prefab:45ddd6d3e34746f428d85e1295049a32");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_03 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_03.prefab:86978668814132b41acf8e6fa46ee789");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_04 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_04.prefab:531cd137ac9264449a69819a0e6b6d35");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_05 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_05.prefab:c192bd7436ca00945b783b8c3836f0c8");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_06 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_06.prefab:7ccca8ce2befcdd4a9e1cfe85b878002");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_07 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_07.prefab:2f553260a37daaf4ea4420ae372b649e");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_08 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_08.prefab:41c3b8e03c3183241b97dacb73109372");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_09 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_09.prefab:5d035c54f9974f749b050b23de557950");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Intro_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Intro_01.prefab:74bbe5cb61af0a945bb818deb8016df9");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_PlayAnima_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_PlayAnima_01.prefab:dc2000f07a54d7549931ee00d2bf23c6");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_PlayHorror_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_PlayHorror_01.prefab:f920687cb211c63448f3a435d72c18b2");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_PlayWeakness_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_PlayWeakness_01.prefab:f4f9eb9a779e9c847bb5aca86403ace2");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Restart_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Restart_01.prefab:6381f727ef3b3b547abd855ca5a36d4d");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Restart_02 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Restart_02.prefab:64dccfa006bc9e94c8bd8597d2c78cc2");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Restart_03 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Restart_03.prefab:c5aa8d563b1ef894fa1b3121444e3777");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Restart_04 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Restart_04.prefab:a8599707c926c634ca6b4c91a60bfafe");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Restart_05 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Restart_05.prefab:8b9a56ca3b748b643808a34bdf32ca38");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Return_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Return_01.prefab:97da9cd0542dd8d48b3a06679db4a26a");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_01.prefab:a007b141d68e0174b9d1178bdd209360");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_02.prefab:73c0b40b8b708704aa2a103f92776399");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_03.prefab:03e79595fd70fb6489e061cdb6e79c0c");

	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_PuzzleALT_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_PuzzleALT_01.prefab:458cf6cf8a59735429eb645451d58796");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private string COMPLETE_LINE = VO_BOTA_BOSS_08h_Female_Eredar_Complete_01;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_08h_Female_Eredar_Complete_01, VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_01, VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_02, VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_03, VO_BOTA_BOSS_08h_Female_Eredar_Idle_01, VO_BOTA_BOSS_08h_Female_Eredar_Idle_02, VO_BOTA_BOSS_08h_Female_Eredar_Idle_03, VO_BOTA_BOSS_08h_Female_Eredar_Idle_04, VO_BOTA_BOSS_08h_Female_Eredar_Idle_05, VO_BOTA_BOSS_08h_Female_Eredar_Idle_06,
			VO_BOTA_BOSS_08h_Female_Eredar_Idle_07, VO_BOTA_BOSS_08h_Female_Eredar_Idle_08, VO_BOTA_BOSS_08h_Female_Eredar_Idle_09, VO_BOTA_BOSS_08h_Female_Eredar_Intro_01, VO_BOTA_BOSS_08h_Female_Eredar_PlayAnima_01, VO_BOTA_BOSS_08h_Female_Eredar_PlayHorror_01, VO_BOTA_BOSS_08h_Female_Eredar_PlayWeakness_01, VO_BOTA_BOSS_08h_Female_Eredar_Restart_01, VO_BOTA_BOSS_08h_Female_Eredar_Restart_02, VO_BOTA_BOSS_08h_Female_Eredar_Restart_03,
			VO_BOTA_BOSS_08h_Female_Eredar_Restart_04, VO_BOTA_BOSS_08h_Female_Eredar_Restart_05, VO_BOTA_BOSS_08h_Female_Eredar_Return_01, VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_01, VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_02, VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_03, VO_BOTA_BOSS_08h_Female_Eredar_PuzzleALT_01
		})
		{
			PreloadSound(item);
		}
	}

	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_08h_Female_Eredar_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_08h_Female_Eredar_Return_01;
		s_victoryLine_1 = VO_BOTA_BOSS_08h_Female_Eredar_PuzzleALT_01;
		s_victoryLine_2 = VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_01;
		s_victoryLine_3 = null;
		s_victoryLine_4 = VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_02;
		s_victoryLine_5 = null;
		s_victoryLine_6 = VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_03;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_01, VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_02, VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_03 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_08h_Female_Eredar_Idle_01, VO_BOTA_BOSS_08h_Female_Eredar_Idle_02, VO_BOTA_BOSS_08h_Female_Eredar_Idle_03, VO_BOTA_BOSS_08h_Female_Eredar_Idle_04, VO_BOTA_BOSS_08h_Female_Eredar_Idle_05, VO_BOTA_BOSS_08h_Female_Eredar_Idle_06, VO_BOTA_BOSS_08h_Female_Eredar_Idle_07, VO_BOTA_BOSS_08h_Female_Eredar_Idle_08, VO_BOTA_BOSS_08h_Female_Eredar_Idle_09 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_08h_Female_Eredar_Restart_01, VO_BOTA_BOSS_08h_Female_Eredar_Restart_02, VO_BOTA_BOSS_08h_Female_Eredar_Restart_03, VO_BOTA_BOSS_08h_Female_Eredar_Restart_04, VO_BOTA_BOSS_08h_Female_Eredar_Restart_05 };
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (missionEvent == 99)
		{
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(enemyActor, COMPLETE_LINE);
			GameState.Get().SetBusy(busy: false);
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "OG_100":
				yield return PlayEasterEggLine(actor, VO_BOTA_BOSS_08h_Female_Eredar_PlayHorror_01);
				break;
			case "GIL_665":
				yield return PlayEasterEggLine(actor, VO_BOTA_BOSS_08h_Female_Eredar_PlayWeakness_01);
				break;
			case "GVG_077":
				yield return PlayEasterEggLine(actor, VO_BOTA_BOSS_08h_Female_Eredar_PlayAnima_01);
				break;
			}
		}
	}
}
