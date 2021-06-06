using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGA_Good_Fight_03 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Backstory_01b_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Backstory_01b_01.prefab:177d1c2a2b5a99d4b845dec19ef50111");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_01_01.prefab:05ac18662fc0f5140884658ce8ee3731");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_02_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_02_01.prefab:5879f46a73bc6da439aa6a0000877afa");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_01_01.prefab:9f50fd1fba7d01940ab9692542a7d0d7");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_02_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_02_01.prefab:abe7dd8496d5f144c8f33b3ccbc2994f");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_03_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_03_01.prefab:9372aa947b8fd7e48bdebc145885f44e");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_01_01.prefab:4df11f96aa6e13e46a8c45f54d01a092");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_02_01.prefab:71a3e03bd89f663488da38d108913e94");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_PlayerStart_01.prefab:7e1c4a6c0605e394493541fdb3fecbc4");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Backstory_01a_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Backstory_01a_01.prefab:c650fcbd35a93b346b890d6235667040");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_Death_01.prefab:d558ce9226fb04f4c91cf5f7da64ff93");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_DeathAlt_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_DeathAlt_01.prefab:5950a1e738254cf4e92aa48776edc1a4");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_01_01.prefab:f38a7f2fcfae44441b695110b56cb795");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_02_01.prefab:c95a2decaf712fb4b8c3a2f3bcdf2bbb");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_03_01.prefab:85ceb28aa72931d4e8cc50748bb4c6e6");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossAttack_01.prefab:bb299237e7eb39b41a09694f25a415f5");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStart_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStart_01.prefab:72d1a63fa72c57d40b0559421371dbcf");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStartHeroic_01.prefab:8518d2aaea1de974eb4c7cd3fa705a05");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_EmoteResponse_01.prefab:fc8388762b51ddf409cbaae73aeddaa2");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_01_01.prefab:86be531afb5159847ba7073fcc60d367");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_02_01.prefab:afd16d20a4622194aafcdfef8c291163");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_03_01.prefab:db7c9b07e8037304a99fa339632c8bbf");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_BadLuckAlbatros_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_BadLuckAlbatros_01.prefab:836d4c0f59fbc8640a77a160bab1e046");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_01_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_01_01.prefab:7d824aa20de2e274489b31f4dd9b2ca5");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_02_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_02_01.prefab:376caa601a93c044a8f08848a60a8585");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DreadRaven_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DreadRaven_01.prefab:e140dca4135f0fd4fb4b046225f0d209");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerMadameLazul_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerMadameLazul_01.prefab:e645f863c7c4a334190c0a7a8a046020");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerVessina_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerVessina_01.prefab:cf47eb85e92cead4e8190a12883fc758");

	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_WingedGuardian_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_WingedGuardian_01.prefab:995645ebb500b5e4c956be2551a06b5a");

	private List<string> m_VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvilLines = new List<string> { VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_01_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_02_01 };

	private List<string> m_VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdeaLines = new List<string> { VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_01_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_02_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_03_01 };

	private List<string> m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_01_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_02_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_IdleLines = new List<string> { VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_01_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_02_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_03_01 };

	private List<string> m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWindsLines = new List<string> { VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_01_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_02_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Backstory_01b_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_01_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_02_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_01_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_02_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_03_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_01_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_02_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_PlayerStart_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Backstory_01a_01,
			VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_Death_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_DeathAlt_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_01_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_02_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_03_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossAttack_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStart_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStartHeroic_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_EmoteResponse_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_01_01,
			VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_02_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_03_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_BadLuckAlbatros_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_01_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_02_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DreadRaven_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerMadameLazul_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerVessina_01, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_WingedGuardian_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		if ((double)Random.Range(0f, 1f) < 0.5)
		{
			m_deathLine = VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_Death_01;
		}
		else
		{
			m_deathLine = VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_DeathAlt_01;
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Backstory_01a_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Backstory_01b_01);
			}
			break;
		case 102:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_01_01);
			}
			break;
		case 103:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_02_01);
			}
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossAttack_01);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "DAL_256"))
		{
			if (cardId == "DRGA_BOSS_02t" && !m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdeaLines);
			}
		}
		else if (!m_Heroic)
		{
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvilLines);
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
			case "DRG_071":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_BadLuckAlbatros_01);
				break;
			case "DRGA_BOSS_13t":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWindsLines);
				break;
			case "DRG_088":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DreadRaven_01);
				break;
			case "DAL_729":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerMadameLazul_01);
				break;
			case "ULD_173":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerVessina_01);
				break;
			case "YOD_003":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_WingedGuardian_01);
				break;
			}
		}
	}
}
