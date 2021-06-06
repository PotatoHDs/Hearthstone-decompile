using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOTA_Clear_Puzzle_4 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Complete_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Complete_01.prefab:6fe3e92b704b5f34b8bffc0563221018");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_01.prefab:f37406e97ea7dd345884bfd7334bfc21");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_02.prefab:14c7245670f0d9f43a9d645dfbc95481");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_03.prefab:5c1ec8319513deb449247f3e5dbaffba");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_01.prefab:58927d2670bc42b45833daa3356f0993");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_02 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_02.prefab:0e4c00a6c5b68cc409453d23683e2015");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_03 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_03.prefab:8dcf4f07ddd22b047938805a45b45bbb");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_04 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_04.prefab:86a553e0090fe2248b4e82541e5d9072");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_05 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_05.prefab:efca1c511606c744da4b1a60e9becf81");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_06 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_06.prefab:457770625d8a9c745a7f5e77726baa11");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_07 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_07.prefab:f9367d13bcef4c9409534e2413ef1b89");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Intro_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Intro_01.prefab:d51864f7c13cdf943ab16a629683040c");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_PlayDefile_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_PlayDefile_01.prefab:e6b564af96d315b45a0d958d973e6208");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_PlayTicking_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_PlayTicking_01.prefab:3dfecb4521329dd48bea94f9662a519b");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Restart_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Restart_01.prefab:c26d5c52553d3a7489a8733a0e181aee");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Restart_02 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Restart_02.prefab:9ec6e11b51bfd324f9c6f15d469c0ff9");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Restart_03 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Restart_03.prefab:a971edf9f35942145b4aa6d1efb1fa02");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Restart_04 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Restart_04.prefab:58f0d140708c8694283cf659051c54bc");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Restart_05 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Restart_05.prefab:1cda323a761a5314fa54d678f3d29a31");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Return_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Return_01.prefab:5338107286875594dbfd7d324903e628");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_01.prefab:b46498e17da2eca409226c20562a68c1");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_02.prefab:e3d2560e7085ad449bbb53fab0a39fac");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_03.prefab:9623b96f7131e4b4da7edbc55ecb99e4");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_04.prefab:a2772aa2ac0e3fe499d5d9c06ce7fdbd");

	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_05 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_05.prefab:06a7b84a92ab69644968abe855836eee");

	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing3_Complete_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing3_Complete_01.prefab:35eef2e00ddf24d4698bf4b2f52a458a");

	private HashSet<string> m_playedLines = new HashSet<string>();

	private string COMPLETE_LINE = VO_BOTA_BOSS_09h_Female_Banshee_Complete_01;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_09h_Female_Banshee_Complete_01, VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_01, VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_02, VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_03, VO_BOTA_BOSS_09h_Female_Banshee_Idle_01, VO_BOTA_BOSS_09h_Female_Banshee_Idle_02, VO_BOTA_BOSS_09h_Female_Banshee_Idle_03, VO_BOTA_BOSS_09h_Female_Banshee_Idle_04, VO_BOTA_BOSS_09h_Female_Banshee_Idle_05, VO_BOTA_BOSS_09h_Female_Banshee_Idle_06,
			VO_BOTA_BOSS_09h_Female_Banshee_Idle_07, VO_BOTA_BOSS_09h_Female_Banshee_Intro_01, VO_BOTA_BOSS_09h_Female_Banshee_PlayDefile_01, VO_BOTA_BOSS_09h_Female_Banshee_PlayTicking_01, VO_BOTA_BOSS_09h_Female_Banshee_Restart_01, VO_BOTA_BOSS_09h_Female_Banshee_Restart_02, VO_BOTA_BOSS_09h_Female_Banshee_Restart_03, VO_BOTA_BOSS_09h_Female_Banshee_Restart_04, VO_BOTA_BOSS_09h_Female_Banshee_Restart_05, VO_BOTA_BOSS_09h_Female_Banshee_Return_01,
			VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_01, VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_02, VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_03, VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_04, VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_05, VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing3_Complete_01
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
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_09h_Female_Banshee_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_09h_Female_Banshee_Return_01;
		s_victoryLine_1 = VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_01;
		s_victoryLine_2 = VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_03;
		s_victoryLine_3 = VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_02;
		s_victoryLine_4 = VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_04;
		s_victoryLine_5 = VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_05;
		s_victoryLine_6 = null;
		s_victoryLine_7 = null;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_01, VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_02, VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_03 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_09h_Female_Banshee_Idle_01, VO_BOTA_BOSS_09h_Female_Banshee_Idle_02, VO_BOTA_BOSS_09h_Female_Banshee_Idle_03, VO_BOTA_BOSS_09h_Female_Banshee_Idle_04, VO_BOTA_BOSS_09h_Female_Banshee_Idle_05, VO_BOTA_BOSS_09h_Female_Banshee_Idle_06, VO_BOTA_BOSS_09h_Female_Banshee_Idle_07 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_09h_Female_Banshee_Restart_01, VO_BOTA_BOSS_09h_Female_Banshee_Restart_02, VO_BOTA_BOSS_09h_Female_Banshee_Restart_03, VO_BOTA_BOSS_09h_Female_Banshee_Restart_04, VO_BOTA_BOSS_09h_Female_Banshee_Restart_05 };
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

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(2f);
			yield return PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing3_Complete_01);
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
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "ICC_041"))
		{
			if (cardId == "ICC_099")
			{
				yield return PlayEasterEggLine(actor, VO_BOTA_BOSS_09h_Female_Banshee_PlayTicking_01);
			}
		}
		else
		{
			yield return PlayEasterEggLine(actor, VO_BOTA_BOSS_09h_Female_Banshee_PlayDefile_01);
		}
	}
}
