using System.Collections;
using System.Collections.Generic;

public class BOTA_Survival_Puzzle_1 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_01.prefab:7f4d4b4cb16f0dd43bec8e4673077968");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_02.prefab:fb4402a2149690f438e8d267435936c3");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_01.prefab:2c99a9fc4d8c9cc4e9c2b27516c5b167");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_02 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_02.prefab:c28de8a3027e9824bb179bc414ed9b05");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_03 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_03.prefab:4ad2b4258cfefaa428d3e865af153f7e");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_04 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_04.prefab:6453ec53e48b27a4e8cd791cb646629b");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_05 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_05.prefab:826405a6bc685dd4d963c3dfe0e882e8");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_06 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_06.prefab:994b99acbcdcbe94bb93b18f62a55b71");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Intro_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Intro_01.prefab:ef656224c4a3663418d29b6c2620dbfc");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_01.prefab:17e367c67c151f441bd1529fd2a4bb4d");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_03 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_03.prefab:f08d46fcaf7f22945a230eebdcfa4880");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_04 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_04.prefab:e3f8c577ca147e749bae6495d116d569");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_05 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_05.prefab:bf4e763d2070ac64a8e8f7eb0f128e35");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_06 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_06.prefab:62dffacf090ed5048b19090bcd672467");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_07 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_07.prefab:bb1bb6639fdbf6c40929bfde3f68bdf4");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Return_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Return_01.prefab:ec4f86fff154fed45b017f89fc9365ff");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_Night Elf_Puzzle_01.prefab:f6e3819d9b589114692b94607c3c87c3");

	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_15h_Female_Night Elf_Puzzle_03.prefab:e49106bf92dcb2e4ca2a835968c782dd");

	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_01.prefab:7a500b4d8aca2364db7e7307b829b42a");

	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_02 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_02.prefab:3fa57b8ac4ddbfb418ee8ddcd570d74d");

	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_04 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_04.prefab:5b13df8e68fd5204f84513246dc2900f");

	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_06 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_06.prefab:18881d3092348ed4b9b1e380705f91d4");

	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Complete_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Complete_01.prefab:d0d0a88812f1fce45bc4594c5f465873");

	private static readonly AssetReference BoommasterFlark_BrassRing_Quote = new AssetReference("BoommasterFlark_BrassRing_Quote.prefab:ac5bae8e6d4465c45b533aafa40053b2");

	private string COMPLETE_LINE = VO_BOTA_BOSS_16h_Male_Goblin_Complete_01;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_01, VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_02, VO_BOTA_BOSS_15h_Female_NightElf_Idle_01, VO_BOTA_BOSS_15h_Female_NightElf_Idle_02, VO_BOTA_BOSS_15h_Female_NightElf_Idle_03, VO_BOTA_BOSS_15h_Female_NightElf_Idle_04, VO_BOTA_BOSS_15h_Female_NightElf_Idle_05, VO_BOTA_BOSS_15h_Female_NightElf_Idle_06, VO_BOTA_BOSS_15h_Female_NightElf_Intro_01, VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_01,
			VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_03, VO_BOTA_BOSS_15h_Female_NightElf_Restart_01, VO_BOTA_BOSS_15h_Female_NightElf_Restart_03, VO_BOTA_BOSS_15h_Female_NightElf_Restart_04, VO_BOTA_BOSS_15h_Female_NightElf_Restart_05, VO_BOTA_BOSS_15h_Female_NightElf_Restart_06, VO_BOTA_BOSS_15h_Female_NightElf_Restart_07, VO_BOTA_BOSS_15h_Female_NightElf_Return_01, VO_BOTA_BOSS_16h_Male_Goblin_Complete_01, VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_01,
			VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_02, VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_04, VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_06
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
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_15h_Female_NightElf_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_15h_Female_NightElf_Return_01;
		s_victoryLine_1 = VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_01;
		s_victoryLine_2 = null;
		s_victoryLine_3 = null;
		s_victoryLine_4 = null;
		s_victoryLine_5 = null;
		s_victoryLine_6 = null;
		s_victoryLine_7 = VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_03;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_01, VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_02 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_15h_Female_NightElf_Idle_01, VO_BOTA_BOSS_15h_Female_NightElf_Idle_02, VO_BOTA_BOSS_15h_Female_NightElf_Idle_03, VO_BOTA_BOSS_15h_Female_NightElf_Idle_04, VO_BOTA_BOSS_15h_Female_NightElf_Idle_05, VO_BOTA_BOSS_15h_Female_NightElf_Idle_06 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_15h_Female_NightElf_Restart_01, VO_BOTA_BOSS_15h_Female_NightElf_Restart_03, VO_BOTA_BOSS_15h_Female_NightElf_Restart_04, VO_BOTA_BOSS_15h_Female_NightElf_Restart_05, VO_BOTA_BOSS_15h_Female_NightElf_Restart_06, VO_BOTA_BOSS_15h_Female_NightElf_Restart_07 };
	}

	protected override IEnumerator RespondToPuzzleStartWithTiming()
	{
		switch (GameState.Get().GetGameEntity().GetTag(GAME_TAG.MISSION_EVENT))
		{
		case 10:
			yield return PlayBigCharacterQuoteAndWait(BoommasterFlark_BrassRing_Quote, VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_01, "VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_01");
			break;
		case 20:
			yield return PlayBigCharacterQuoteAndWait(BoommasterFlark_BrassRing_Quote, VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_06, "VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_06");
			break;
		case 30:
			yield return PlayBigCharacterQuoteAndWait(BoommasterFlark_BrassRing_Quote, VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_04, "VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_04");
			break;
		case 40:
			yield return PlayBigCharacterQuoteAndWait(BoommasterFlark_BrassRing_Quote, VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_02, "VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_02");
			break;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 99)
		{
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(BoommasterFlark_BrassRing_Quote, COMPLETE_LINE);
			GameState.Get().SetBusy(busy: false);
		}
	}
}
