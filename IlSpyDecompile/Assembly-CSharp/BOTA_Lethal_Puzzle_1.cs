using System.Collections;
using System.Collections.Generic;

public class BOTA_Lethal_Puzzle_1 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Complete_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Complete_01.prefab:474bd745b06e121468cb0904279eea8e");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_01.prefab:459b904bdbddd75479f408e70a3626a3");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_02.prefab:68f821585c7371d438d95096f491e6c0");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_03.prefab:761927665fddf1d4883751eca807a6e1");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_04 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_04.prefab:7dc1c80b7cc7f3e40b54a13f16660678");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_01.prefab:5bd2cf2e78907794b9ee5e38dd59d247");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_02 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_02.prefab:4f2af8591cfe0b642a62e098f9ed6565");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_03 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_03.prefab:689ea61945173364492ae403663c5da4");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_04 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_04.prefab:bcef4e246ca620b41825a55380183c9d");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_06 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_06.prefab:122a2ff9ba78a4143aad04121c4b0318");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_08 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_08.prefab:b45ee248f2155b94680f1527cb61c491");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_09 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_09.prefab:9e18cbad79fe5104288d45d46f5b83d2");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_10 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_10.prefab:4ce51faf6294994419d2c33266f31a15");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_11 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_11.prefab:6cf6094dee3147846b5a782ef8252f36");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Intro_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Intro_01.prefab:1d748a37e9e4bed46bbbc968e209b610");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_01.prefab:f099dc5102fc7da4b90ffcb2eabcdfe9");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_02.prefab:40e0c9996cb204f49bdfac9f2d4338eb");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_03.prefab:ada363fc1ee66a44e98a29f20260cfd1");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_01.prefab:82b6d09f67114b54f9cd653aa1b8009b");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_02 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_02.prefab:231b6a350abb8394680706c25d34dbb1");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_03 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_03.prefab:8f1f1384352765c489c3eb4a80b5cbed");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_04 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_04.prefab:ce2d6caca1a98c14f85343938c8f5b60");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_05 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_05.prefab:6fbf5c367961f4e4885612fe830a8ab1");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_06 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_06.prefab:93f4a9d7181a5cc4b90e8b8c515fd229");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Return_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Return_01.prefab:40b61fb1456bfdb43b462d9f899083b0");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Tutorial_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Tutorial_01.prefab:de797322cb0fad94c9d7a59f9f32b560");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Tutorial_04 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Tutorial_04.prefab:fc6faa26a2a28754981c1610ac7bf7a9");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Tutorial_05 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Tutorial_05.prefab:099452dc1c8d5f84bbb99898a309f564");

	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Tutorial_06 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Tutorial_06.prefab:17b9cde16ee448a49949b520bef1ee1f");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_01.prefab:658d5aed801b1d542959fac0ad624408");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_02 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_02.prefab:20a26f386c8134a4ca0e25617ed1ec02");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_03 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_03.prefab:2873d69f7652e084592591c5223cd00d");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_04 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_04.prefab:0e92dd70de6be834a90a1e24368b6510");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_06 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_06.prefab:6efaf12d23a1dc744a095a35c63296b5");

	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_08 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_08.prefab:8f67008fa28f1634bb19856744b8698c");

	private string COMPLETE_LINE = VO_BOTA_BOSS_10h_Male_NightElf_Complete_01;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_10h_Male_NightElf_Complete_01, VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_01, VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_02, VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_03, VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_04, VO_BOTA_BOSS_10h_Male_NightElf_Idle_01, VO_BOTA_BOSS_10h_Male_NightElf_Idle_02, VO_BOTA_BOSS_10h_Male_NightElf_Idle_03, VO_BOTA_BOSS_10h_Male_NightElf_Idle_04, VO_BOTA_BOSS_10h_Male_NightElf_Idle_06,
			VO_BOTA_BOSS_10h_Male_NightElf_Idle_08, VO_BOTA_BOSS_10h_Male_NightElf_Idle_09, VO_BOTA_BOSS_10h_Male_NightElf_Idle_10, VO_BOTA_BOSS_10h_Male_NightElf_Idle_11, VO_BOTA_BOSS_10h_Male_NightElf_Intro_01, VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_01, VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_02, VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_03, VO_BOTA_BOSS_10h_Male_NightElf_Restart_01, VO_BOTA_BOSS_10h_Male_NightElf_Restart_02,
			VO_BOTA_BOSS_10h_Male_NightElf_Restart_03, VO_BOTA_BOSS_10h_Male_NightElf_Restart_04, VO_BOTA_BOSS_10h_Male_NightElf_Restart_05, VO_BOTA_BOSS_10h_Male_NightElf_Restart_06, VO_BOTA_BOSS_14h_Female_Undead_Tutorial_01, VO_BOTA_BOSS_10h_Male_NightElf_Return_01, VO_BOTA_BOSS_14h_Female_Undead_Tutorial_04, VO_BOTA_BOSS_14h_Female_Undead_Tutorial_05, VO_BOTA_BOSS_14h_Female_Undead_Tutorial_06, VO_BOTA_BOSS_10h_Male_NightElf_Lethal_01,
			VO_BOTA_BOSS_10h_Male_NightElf_Lethal_02, VO_BOTA_BOSS_10h_Male_NightElf_Lethal_03, VO_BOTA_BOSS_10h_Male_NightElf_Lethal_04, VO_BOTA_BOSS_10h_Male_NightElf_Lethal_06, VO_BOTA_BOSS_10h_Male_NightElf_Lethal_08
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
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_10h_Male_NightElf_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_10h_Male_NightElf_Return_01;
		s_victoryLine_1 = VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_01;
		s_victoryLine_2 = null;
		s_victoryLine_3 = VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_02;
		s_victoryLine_4 = null;
		s_victoryLine_5 = VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_03;
		s_victoryLine_6 = null;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_01, VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_02, VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_03, VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_04 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_10h_Male_NightElf_Idle_01, VO_BOTA_BOSS_10h_Male_NightElf_Idle_02, VO_BOTA_BOSS_10h_Male_NightElf_Idle_03, VO_BOTA_BOSS_10h_Male_NightElf_Idle_04, VO_BOTA_BOSS_10h_Male_NightElf_Idle_06, VO_BOTA_BOSS_10h_Male_NightElf_Idle_08, VO_BOTA_BOSS_10h_Male_NightElf_Idle_09, VO_BOTA_BOSS_10h_Male_NightElf_Idle_10, VO_BOTA_BOSS_10h_Male_NightElf_Idle_11 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_10h_Male_NightElf_Restart_01, VO_BOTA_BOSS_10h_Male_NightElf_Restart_02, VO_BOTA_BOSS_10h_Male_NightElf_Restart_03, VO_BOTA_BOSS_10h_Male_NightElf_Restart_04, VO_BOTA_BOSS_10h_Male_NightElf_Restart_05, VO_BOTA_BOSS_10h_Male_NightElf_Restart_06 };
		s_lethalCompleteLines = new List<string> { VO_BOTA_BOSS_10h_Male_NightElf_Lethal_01, VO_BOTA_BOSS_10h_Male_NightElf_Lethal_02, VO_BOTA_BOSS_10h_Male_NightElf_Lethal_03, VO_BOTA_BOSS_10h_Male_NightElf_Lethal_04, VO_BOTA_BOSS_10h_Male_NightElf_Lethal_06, VO_BOTA_BOSS_10h_Male_NightElf_Lethal_08 };
	}

	protected override IEnumerator RespondToPuzzleStartWithTiming()
	{
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		int currentMissionEvent = gameEntity.GetTag(GAME_TAG.MISSION_EVENT);
		if (currentMissionEvent == 10)
		{
			yield return PlayBigCharacterQuoteAndWait("MyraRotspring_BrassRing_Quote.prefab:83be35e35b5e22b499ef296f55fcbd73", VO_BOTA_BOSS_14h_Female_Undead_Tutorial_01, "VO_BOTA_BOSS_14h_Female_Undead_Tutorial_01");
		}
		if (currentMissionEvent == 20)
		{
			yield return PlayBigCharacterQuoteAndWait("MyraRotspring_BrassRing_Quote.prefab:83be35e35b5e22b499ef296f55fcbd73", VO_BOTA_BOSS_14h_Female_Undead_Tutorial_04, "VO_BOTA_BOSS_14h_Female_Undead_Tutorial_04");
		}
		if (currentMissionEvent == 40)
		{
			yield return PlayBigCharacterQuoteAndWait("MyraRotspring_BrassRing_Quote.prefab:83be35e35b5e22b499ef296f55fcbd73", VO_BOTA_BOSS_14h_Female_Undead_Tutorial_05, "VO_BOTA_BOSS_14h_Female_Undead_Tutorial_05");
		}
		if (currentMissionEvent == 70)
		{
			yield return PlayBigCharacterQuoteAndWait("MyraRotspring_BrassRing_Quote.prefab:83be35e35b5e22b499ef296f55fcbd73", VO_BOTA_BOSS_14h_Female_Undead_Tutorial_06, "VO_BOTA_BOSS_14h_Female_Undead_Tutorial_06");
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 99:
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(enemyActor, COMPLETE_LINE);
			GameState.Get().SetBusy(busy: false);
			break;
		case 777:
		{
			string lethalCompleteLine = GetLethalCompleteLine();
			if (lethalCompleteLine != null)
			{
				yield return PlayBossLine(enemyActor, lethalCompleteLine);
			}
			break;
		}
		}
	}
}
