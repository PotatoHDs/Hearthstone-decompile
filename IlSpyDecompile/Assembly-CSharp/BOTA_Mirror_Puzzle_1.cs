using System.Collections;
using System.Collections.Generic;

public class BOTA_Mirror_Puzzle_1 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_02.prefab:3950977a5bdbd8a4fad5e54b5d722938");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_03.prefab:a3ead03041b599d44b4b2c2a7d9832e9");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_04 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_04.prefab:e43d1c9b9fdcde34a8e7b651fadbbf51");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_05 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_05.prefab:fbcd038a159775d48a143ae95a9008c9");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_01.prefab:73b89b9ecbf31a54c9ab005e852638fc");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_02 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_02.prefab:e1d1839df3071c84b820a70d248dddc7");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_03 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_03.prefab:f99c9e8a36e76be4a83bdf3c3edfb5f8");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_04 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_04.prefab:cd4b8bc32b4c2ab49bfbf48426a12a13");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_05 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_05.prefab:2a778d1715e7cad40ba222bd2279b6be");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_06 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_06.prefab:4245ef9fe5ca2b54aaad2ba85396cf86");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_07 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_07.prefab:77650a4814e780748abb973190dc7199");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_08 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_08.prefab:22a4bea26d8c78748bed505cd5c3f8b1");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_09 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_09.prefab:c1c22d1fccb884a4b87675dd0f641bb2");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_10 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_10.prefab:ad8a6d3aebf10304b9d17657ad83b125");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_11 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_11.prefab:c06d4e405e015a240b5922fc11c0ba57");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Intro_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Intro_01.prefab:fe0916168512a9a4698604bfc862d80d");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_01.prefab:885a37a6420e391439cad845eba2043c");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_02.prefab:4c70dad20c01baf4ba562d458d4546d7");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_03.prefab:0b8bf2347077fda4188b5168600b848f");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_04.prefab:14314fde7b7ba6e43a5c0fcf5ef992ef");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_06 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_06.prefab:47fd5d0c6ce3cca42a1f4235f21bf0ee");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Restart_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Restart_01.prefab:7f5545a0109e71a46916ed16e1e8b761");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Restart_02 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Restart_02.prefab:906fd234d371127429c59eb8b6bd16dd");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Restart_03 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Restart_03.prefab:60ada232c003ced40a1d3c782c76d071");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Return_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Return_01.prefab:1ecb3fd857816aa42ba64138af86a04b");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Stuck_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Stuck_01.prefab:1f95f0e134ca79e43933c9e733a4666a");

	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Whoops_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Whoops_01.prefab:96437f5aedb57774aa57592c5e8cb12c");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_01.prefab:49656a209f243f140ada2061195d8d10");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_01.prefab:a28f9558decb3094fbdb7d652b4ba5bb");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_03.prefab:c6e7456d715d6c548b0313ea87685fdf");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_04 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_04.prefab:6c1747b914ceb3449803b2c9c364f304");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_05 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_05.prefab:fe218c0cb85e87041be94d34e90b7253");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_06 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_06.prefab:93a31166d4addc145881e9f323d64379");

	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Complete_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Complete_01.prefab:8ddfcecdb0be962429275b5524529d2a");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_02, VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_03, VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_04, VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_05, VO_BOTA_BOSS_01h_Male_Gnome_Idle_01, VO_BOTA_BOSS_01h_Male_Gnome_Idle_02, VO_BOTA_BOSS_01h_Male_Gnome_Idle_03, VO_BOTA_BOSS_01h_Male_Gnome_Idle_04, VO_BOTA_BOSS_01h_Male_Gnome_Idle_05, VO_BOTA_BOSS_01h_Male_Gnome_Idle_06,
			VO_BOTA_BOSS_01h_Male_Gnome_Idle_07, VO_BOTA_BOSS_01h_Male_Gnome_Idle_08, VO_BOTA_BOSS_01h_Male_Gnome_Idle_09, VO_BOTA_BOSS_01h_Male_Gnome_Idle_10, VO_BOTA_BOSS_01h_Male_Gnome_Idle_11, VO_BOTA_BOSS_01h_Male_Gnome_Intro_01, VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_01, VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_02, VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_03, VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_04,
			VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_06, VO_BOTA_BOSS_01h_Male_Gnome_Restart_01, VO_BOTA_BOSS_01h_Male_Gnome_Restart_02, VO_BOTA_BOSS_01h_Male_Gnome_Restart_03, VO_BOTA_BOSS_01h_Male_Gnome_Return_01, VO_BOTA_BOSS_01h_Male_Gnome_Stuck_01, VO_BOTA_BOSS_01h_Male_Gnome_Whoops_01, VO_BOTA_BOSS_05h_Male_Ethereal_Idle_01, VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_01, VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_03,
			VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_04, VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_05, VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_06, VO_BOTA_BOSS_05h_Male_Ethereal_Complete_01
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
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_01h_Male_Gnome_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_01h_Male_Gnome_Return_01;
		s_victoryLine_1 = null;
		s_victoryLine_2 = VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_01;
		s_victoryLine_3 = null;
		s_victoryLine_4 = null;
		s_victoryLine_5 = null;
		s_victoryLine_6 = VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_03;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_02, VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_03, VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_04, VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_05 };
		s_idleLines = new List<string>
		{
			VO_BOTA_BOSS_01h_Male_Gnome_Idle_01, VO_BOTA_BOSS_01h_Male_Gnome_Idle_02, VO_BOTA_BOSS_01h_Male_Gnome_Idle_03, VO_BOTA_BOSS_01h_Male_Gnome_Idle_04, VO_BOTA_BOSS_01h_Male_Gnome_Idle_05, VO_BOTA_BOSS_01h_Male_Gnome_Idle_06, VO_BOTA_BOSS_01h_Male_Gnome_Idle_07, VO_BOTA_BOSS_01h_Male_Gnome_Idle_08, VO_BOTA_BOSS_01h_Male_Gnome_Idle_09, VO_BOTA_BOSS_01h_Male_Gnome_Idle_10,
			VO_BOTA_BOSS_01h_Male_Gnome_Idle_11
		};
		s_restartLines = new List<string> { VO_BOTA_BOSS_01h_Male_Gnome_Restart_01, VO_BOTA_BOSS_01h_Male_Gnome_Restart_02, VO_BOTA_BOSS_01h_Male_Gnome_Restart_03 };
	}

	protected override IEnumerator RespondToPuzzleStartWithTiming()
	{
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		int currentMissionEvent = gameEntity.GetTag(GAME_TAG.MISSION_EVENT);
		if (currentMissionEvent == 10)
		{
			yield return PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_01, "VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_01");
		}
		if (currentMissionEvent == 60)
		{
			yield return PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_03, "VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_03");
		}
		if (currentMissionEvent == 30)
		{
			yield return PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_04, "VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_04");
		}
		if (currentMissionEvent == 50)
		{
			yield return PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_05, "VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_05");
		}
		if (currentMissionEvent == 40)
		{
			yield return PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_06, "VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_06");
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
			yield return PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", VO_BOTA_BOSS_05h_Male_Ethereal_Complete_01, "VO_BOTA_BOSS_05h_Male_Ethereal_Complete_01");
			GameState.Get().SetBusy(busy: false);
		}
	}
}
