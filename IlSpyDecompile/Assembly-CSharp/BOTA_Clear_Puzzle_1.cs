using System.Collections;
using System.Collections.Generic;

public class BOTA_Clear_Puzzle_1 : BOTA_MissionEntity
{
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_01.prefab:46e7a21728fd64b4f953031d9d977732");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_02.prefab:18d3481a6092cf34e96f88f1142386d8");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_03.prefab:1eb726cce726f65419e12571297677ca");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_01.prefab:77150d342881aa54cbb7e79165328780");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_02 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_02.prefab:363554ecdf3b2e04ea0937af8a42656a");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_03 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_03.prefab:b0d825a2597656144bde988096af48df");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_04 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_04.prefab:dc44f71232290ea4280808bb5ae3df73");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_05 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_05.prefab:b3c08919f76a50a4ca716d80926a6544");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_06 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_06.prefab:d6485b62c515eae4dadf981a008a2506");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_07 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_07.prefab:1911b8a59ce445146a8501e953ae1f64");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Intro_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Intro_01.prefab:f0c4fac0aaafee0408f55786e085f697");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_PlaySap_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_PlaySap_01.prefab:1417f9f83bc647c4da3eaf28b2e06692");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_01.prefab:95ce41b863246c643b59d5aebcd80ab9");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_02.prefab:62e85efda01b7054e9543333724fa918");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Restart_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Restart_01.prefab:ba7e83c3d76e9044ea9d4eb81c37d573");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Restart_02 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Restart_02.prefab:d705a514b9eca334888fcc369162b96b");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Restart_03 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Restart_03.prefab:6b9328e67fed6a8479a976184892d4e8");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Restart_04 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Restart_04.prefab:e5ef3671705772249aafc88da4ec2a32");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Restart_05 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Restart_05.prefab:709f39f48ab3d534b9885258806d60be");

	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Return_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Return_01.prefab:9c21d2a1c04d1b140a8b2c7a419f7bf5");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Complete_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Complete_01.prefab:bc0994ae7d6cd784da984371e2e9b300");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_02.prefab:ccb8daed1960e8c4cb86fa30101bb2a9");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_03.prefab:dff0d5adb8926114ea74c77cbef8485d");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_06 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_06.prefab:77fe443edf907c14ea0a30a2468ca1fb");

	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_07 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_07.prefab:95c304fd114d39743b04007e803a6b9b");

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_01, VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_02, VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_03, VO_BOTA_BOSS_06h_Male_Goblin_Idle_01, VO_BOTA_BOSS_06h_Male_Goblin_Idle_02, VO_BOTA_BOSS_06h_Male_Goblin_Idle_03, VO_BOTA_BOSS_06h_Male_Goblin_Idle_04, VO_BOTA_BOSS_06h_Male_Goblin_Idle_05, VO_BOTA_BOSS_06h_Male_Goblin_Idle_06, VO_BOTA_BOSS_06h_Male_Goblin_Idle_07,
			VO_BOTA_BOSS_06h_Male_Goblin_Intro_01, VO_BOTA_BOSS_06h_Male_Goblin_PlaySap_01, VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_01, VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_02, VO_BOTA_BOSS_06h_Male_Goblin_Restart_01, VO_BOTA_BOSS_06h_Male_Goblin_Restart_02, VO_BOTA_BOSS_06h_Male_Goblin_Restart_03, VO_BOTA_BOSS_06h_Male_Goblin_Restart_04, VO_BOTA_BOSS_06h_Male_Goblin_Restart_05, VO_BOTA_BOSS_06h_Male_Goblin_Return_01,
			VO_BOTA_BOSS_07h_Male_Ooze_Complete_01, VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_02, VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_03, VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_06, VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_07
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
		BOTA_MissionEntity.s_introLine = VO_BOTA_BOSS_06h_Male_Goblin_Intro_01;
		BOTA_MissionEntity.s_returnLine = VO_BOTA_BOSS_06h_Male_Goblin_Return_01;
		s_victoryLine_1 = null;
		s_victoryLine_2 = null;
		s_victoryLine_3 = VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_01;
		s_victoryLine_4 = null;
		s_victoryLine_5 = null;
		s_victoryLine_6 = null;
		s_victoryLine_7 = VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_02;
		s_victoryLine_8 = null;
		s_victoryLine_9 = null;
		s_emoteLines = new List<string> { VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_01, VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_02, VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_03 };
		s_idleLines = new List<string> { VO_BOTA_BOSS_06h_Male_Goblin_Idle_01, VO_BOTA_BOSS_06h_Male_Goblin_Idle_02, VO_BOTA_BOSS_06h_Male_Goblin_Idle_03, VO_BOTA_BOSS_06h_Male_Goblin_Idle_04, VO_BOTA_BOSS_06h_Male_Goblin_Idle_05, VO_BOTA_BOSS_06h_Male_Goblin_Idle_06, VO_BOTA_BOSS_06h_Male_Goblin_Idle_07 };
		s_restartLines = new List<string> { VO_BOTA_BOSS_06h_Male_Goblin_Restart_01, VO_BOTA_BOSS_06h_Male_Goblin_Restart_02, VO_BOTA_BOSS_06h_Male_Goblin_Restart_03, VO_BOTA_BOSS_06h_Male_Goblin_Restart_04, VO_BOTA_BOSS_06h_Male_Goblin_Restart_05 };
	}

	protected override IEnumerator RespondToPuzzleStartWithTiming()
	{
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		int currentMissionEvent = gameEntity.GetTag(GAME_TAG.MISSION_EVENT);
		if (currentMissionEvent == 10)
		{
			yield return PlayBigCharacterQuoteAndWait("FlobbidinousFloop_BrassRing_Quote.prefab:bdd400552a67a6e4381cf9a680ebf952", VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_02, "VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_02");
		}
		if (currentMissionEvent == 20)
		{
			yield return PlayBigCharacterQuoteAndWait("FlobbidinousFloop_BrassRing_Quote.prefab:bdd400552a67a6e4381cf9a680ebf952", VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_03, "VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_03");
		}
		if (currentMissionEvent == 50)
		{
			yield return PlayBigCharacterQuoteAndWait("FlobbidinousFloop_BrassRing_Quote.prefab:bdd400552a67a6e4381cf9a680ebf952", VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_07, "VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_07");
		}
		if (currentMissionEvent == 80)
		{
			yield return PlayBigCharacterQuoteAndWait("FlobbidinousFloop_BrassRing_Quote.prefab:bdd400552a67a6e4381cf9a680ebf952", VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_06, "VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_06");
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
			yield return PlayBigCharacterQuoteAndWait("FlobbidinousFloop_BrassRing_Quote.prefab:bdd400552a67a6e4381cf9a680ebf952", VO_BOTA_BOSS_07h_Male_Ooze_Complete_01, "VO_BOTA_BOSS_07h_Male_Ooze_Complete_01");
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
			if (cardId == "EX1_581")
			{
				yield return PlayEasterEggLine(actor, VO_BOTA_BOSS_06h_Male_Goblin_PlaySap_01);
			}
		}
	}
}
