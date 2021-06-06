using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000418 RID: 1048
public class BOTA_Mirror_Puzzle_1 : BOTA_MissionEntity
{
	// Token: 0x06003975 RID: 14709 RVA: 0x0012309C File Offset: 0x0012129C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_02,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_03,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_04,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_05,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_02,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_03,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_04,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_05,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_06,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_07,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_08,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_09,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_10,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_11,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Intro_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_02,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_03,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_04,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_06,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Restart_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Restart_02,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Restart_03,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Return_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Stuck_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Whoops_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Idle_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_03,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_04,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_05,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_06,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Complete_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003976 RID: 14710 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003977 RID: 14711 RVA: 0x00123314 File Offset: 0x00121514
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Return_01;
		this.s_victoryLine_1 = null;
		this.s_victoryLine_2 = BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_01;
		this.s_victoryLine_3 = null;
		this.s_victoryLine_4 = null;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_03;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_02,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_03,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_04,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_05
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_02,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_03,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_04,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_05,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_06,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_07,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_08,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_09,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_10,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Idle_11
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Restart_01,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Restart_02,
			BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_01h_Male_Gnome_Restart_03
		};
	}

	// Token: 0x06003978 RID: 14712 RVA: 0x001234D7 File Offset: 0x001216D7
	protected override IEnumerator RespondToPuzzleStartWithTiming()
	{
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		int currentMissionEvent = gameEntity.GetTag(GAME_TAG.MISSION_EVENT);
		if (currentMissionEvent == 10)
		{
			yield return base.PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_01, "VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_01", 3f, 1f, true, false);
		}
		if (currentMissionEvent == 60)
		{
			yield return base.PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_03, "VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_03", 3f, 1f, true, false);
		}
		if (currentMissionEvent == 30)
		{
			yield return base.PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_04, "VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_04", 3f, 1f, true, false);
		}
		if (currentMissionEvent == 50)
		{
			yield return base.PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_05, "VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_05", 3f, 1f, true, false);
		}
		if (currentMissionEvent == 40)
		{
			yield return base.PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_06, "VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_06", 3f, 1f, true, false);
		}
		yield break;
	}

	// Token: 0x06003979 RID: 14713 RVA: 0x001234E6 File Offset: 0x001216E6
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 99)
		{
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(true);
			yield return base.PlayBigCharacterQuoteAndWait("ZerekMasterCloner_BrassRing_Quote.prefab:0b848829997058a4ebfa59a8660bfbdf", BOTA_Mirror_Puzzle_1.VO_BOTA_BOSS_05h_Male_Ethereal_Complete_01, "VO_BOTA_BOSS_05h_Male_Ethereal_Complete_01", 3f, 1f, true, false);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04001F17 RID: 7959
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_02.prefab:3950977a5bdbd8a4fad5e54b5d722938");

	// Token: 0x04001F18 RID: 7960
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_03.prefab:a3ead03041b599d44b4b2c2a7d9832e9");

	// Token: 0x04001F19 RID: 7961
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_04 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_04.prefab:e43d1c9b9fdcde34a8e7b651fadbbf51");

	// Token: 0x04001F1A RID: 7962
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_05 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_EmoteResponse_05.prefab:fbcd038a159775d48a143ae95a9008c9");

	// Token: 0x04001F1B RID: 7963
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_01.prefab:73b89b9ecbf31a54c9ab005e852638fc");

	// Token: 0x04001F1C RID: 7964
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_02 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_02.prefab:e1d1839df3071c84b820a70d248dddc7");

	// Token: 0x04001F1D RID: 7965
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_03 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_03.prefab:f99c9e8a36e76be4a83bdf3c3edfb5f8");

	// Token: 0x04001F1E RID: 7966
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_04 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_04.prefab:cd4b8bc32b4c2ab49bfbf48426a12a13");

	// Token: 0x04001F1F RID: 7967
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_05 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_05.prefab:2a778d1715e7cad40ba222bd2279b6be");

	// Token: 0x04001F20 RID: 7968
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_06 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_06.prefab:4245ef9fe5ca2b54aaad2ba85396cf86");

	// Token: 0x04001F21 RID: 7969
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_07 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_07.prefab:77650a4814e780748abb973190dc7199");

	// Token: 0x04001F22 RID: 7970
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_08 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_08.prefab:22a4bea26d8c78748bed505cd5c3f8b1");

	// Token: 0x04001F23 RID: 7971
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_09 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_09.prefab:c1c22d1fccb884a4b87675dd0f641bb2");

	// Token: 0x04001F24 RID: 7972
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_10 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_10.prefab:ad8a6d3aebf10304b9d17657ad83b125");

	// Token: 0x04001F25 RID: 7973
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Idle_11 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Idle_11.prefab:c06d4e405e015a240b5922fc11c0ba57");

	// Token: 0x04001F26 RID: 7974
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Intro_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Intro_01.prefab:fe0916168512a9a4698604bfc862d80d");

	// Token: 0x04001F27 RID: 7975
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_01.prefab:885a37a6420e391439cad845eba2043c");

	// Token: 0x04001F28 RID: 7976
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_02.prefab:4c70dad20c01baf4ba562d458d4546d7");

	// Token: 0x04001F29 RID: 7977
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_03.prefab:0b8bf2347077fda4188b5168600b848f");

	// Token: 0x04001F2A RID: 7978
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_04.prefab:14314fde7b7ba6e43a5c0fcf5ef992ef");

	// Token: 0x04001F2B RID: 7979
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_06 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Puzzle_06.prefab:47fd5d0c6ce3cca42a1f4235f21bf0ee");

	// Token: 0x04001F2C RID: 7980
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Restart_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Restart_01.prefab:7f5545a0109e71a46916ed16e1e8b761");

	// Token: 0x04001F2D RID: 7981
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Restart_02 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Restart_02.prefab:906fd234d371127429c59eb8b6bd16dd");

	// Token: 0x04001F2E RID: 7982
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Restart_03 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Restart_03.prefab:60ada232c003ced40a1d3c782c76d071");

	// Token: 0x04001F2F RID: 7983
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Return_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Return_01.prefab:1ecb3fd857816aa42ba64138af86a04b");

	// Token: 0x04001F30 RID: 7984
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Stuck_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Stuck_01.prefab:1f95f0e134ca79e43933c9e733a4666a");

	// Token: 0x04001F31 RID: 7985
	private static readonly AssetReference VO_BOTA_BOSS_01h_Male_Gnome_Whoops_01 = new AssetReference("VO_BOTA_BOSS_01h_Male_Gnome_Whoops_01.prefab:96437f5aedb57774aa57592c5e8cb12c");

	// Token: 0x04001F32 RID: 7986
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Idle_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Idle_01.prefab:49656a209f243f140ada2061195d8d10");

	// Token: 0x04001F33 RID: 7987
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_01.prefab:a28f9558decb3094fbdb7d652b4ba5bb");

	// Token: 0x04001F34 RID: 7988
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_03 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_03.prefab:c6e7456d715d6c548b0313ea87685fdf");

	// Token: 0x04001F35 RID: 7989
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_04 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_04.prefab:6c1747b914ceb3449803b2c9c364f304");

	// Token: 0x04001F36 RID: 7990
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_05 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_05.prefab:fe218c0cb85e87041be94d34e90b7253");

	// Token: 0x04001F37 RID: 7991
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_06 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Tutorial_06.prefab:93a31166d4addc145881e9f323d64379");

	// Token: 0x04001F38 RID: 7992
	private static readonly AssetReference VO_BOTA_BOSS_05h_Male_Ethereal_Complete_01 = new AssetReference("VO_BOTA_BOSS_05h_Male_Ethereal_Complete_01.prefab:8ddfcecdb0be962429275b5524529d2a");
}
