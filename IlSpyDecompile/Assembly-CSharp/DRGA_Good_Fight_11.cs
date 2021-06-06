using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGA_Good_Fight_11 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01.prefab:fbfe70bab9271e746adc673ebe4e8ab4");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_01_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_01_01.prefab:f74d8edc07731114989fe9757034209b");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_02_01.prefab:0a1a07429fb26c54fb1c62ed40cc8b78");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_03_01.prefab:894edfd6a3537c74cb45f8556aeb0da1");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_02_01.prefab:206d3738080618243bc7f3c06db1b883");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_03_01.prefab:df986bea55a081e479adecedfe69a161");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_03_01.prefab:922d8f2382ebca54fbf72aafa8fae8da");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_05_01.prefab:7cdc013b0c4944a4c8e9528828c891b0");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Reno_Released_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Reno_Released_01_01.prefab:b70ceab4d9979874e9b8d851b9353c7f");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_01_01.prefab:6869eeb43168cd848889043266d7c25f");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_02_01.prefab:bba5ecfc9c1fde141b0e0dd5e4b1f834");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_01_01.prefab:220162e7b22893749a0b411c22b1934c");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_02_01.prefab:159461fc0fa70df4393eab97799e5210");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_03_01.prefab:4bc00c695fb8b484396fb9bb689f96ae");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_04_01.prefab:b3def71dc86798c409c650cf773d3139");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_05_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_05_01.prefab:79bd22a399d3ab44c8c4d4f393845add");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_01_01.prefab:3e47bcacdd236f74d965267e175a0afd");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_02_01.prefab:a8845bbc3c5959c4dacd0cef13fc4520");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossAttack_01.prefab:481791c50354af841a6de7acc5012b9d");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStart_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStart_01.prefab:e5a3aff30a058104fa104f92e5908e27");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStartHeroic_01.prefab:2d0847ccbc7c45845b7a889b958ff1f2");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_EmoteResponse_01.prefab:8beaf06a45daeca49a3cf2d33b2e2a67");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_01_01.prefab:b2d04decd9b813546a4dfabe16bbd089");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_02_01.prefab:5b880c8f7719f11448cbd1ace31e423f");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_03_01.prefab:542db798fb216774399849763b45f295");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_01_01.prefab:90a00acc56f54fe43b9215a22028e993");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_02_01.prefab:eaefd2d85494e5b458cdb91d03b75fa5");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Elise_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Elise_01.prefab:d065d4c7f99ed79489450bffd7c050c0");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_EVIL_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_EVIL_01.prefab:0d6bf612a8bf70f48a12ec2940abc737");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_01_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_01_01.prefab:cbb06829f46aa2140b3eecaca2dea947");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_02_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_02_01.prefab:d90f22b6ece2ded48baff8cb7040cd1d");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_03_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_03_01.prefab:cfdd3a9168280bd41834d8e01b228edb");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_04_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_04_01.prefab:796a06f5fbbf9b54d9346b3fcf6c0510");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Togwaggle_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Togwaggle_01.prefab:cd1554cd9b1cdfe4cb4ce7edc65c831d");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Waxadred_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Waxadred_01.prefab:e7e8732bf2daa15499d680d5797e7af8");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_WaxElemental_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_WaxElemental_01.prefab:1926bfd1e513e184891b03bf60017430");

	private static readonly AssetReference VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Reno_Released_04_01 = new AssetReference("VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Reno_Released_04_01.prefab:eb7b1bc3f68a81540ac20ed0bfee152c");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Concede_01 = new AssetReference("VO_ULDA_Reno_Male_Human_Concede_01.prefab:eaf3e0c057f25084284998291c6bb914");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Attack_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Attack_01.prefab:80a2b1d3517200846a4777a2733b5b60");

	private List<string> m_missionEventTrigger108Lines = new List<string> { VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_01_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_02_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_03_01 };

	private List<string> m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_01_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_02_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_03_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_04_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_05_01 };

	private List<string> m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLanceLines = new List<string> { VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_01_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_02_01 };

	private List<string> m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_IdleLines = new List<string> { VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_01_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_02_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_03_01 };

	private List<string> m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandleLines = new List<string> { VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_01_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_02_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_03_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_04_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_01_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_02_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Prison_03_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_02_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_03_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_03_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_05_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Reno_Released_01_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_01_01,
			VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_02_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_01_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_02_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_03_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_04_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPower_05_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_01_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLance_02_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossAttack_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStart_01,
			VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStartHeroic_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_EmoteResponse_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_01_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_02_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Idle_03_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_01_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_02_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Elise_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_EVIL_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_01_01,
			VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_02_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_03_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandle_04_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Togwaggle_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Waxadred_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_WaxElemental_01, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Reno_Released_04_01, VO_ULDA_Reno_Male_Human_Concede_01, VO_DRGA_BOSS_01h_Male_Human_Attack_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_Death_01_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
				return;
			}
			if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
				return;
			}
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 100:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_01_01);
			}
			break;
		case 101:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Misc_02_01);
			}
			break;
		case 104:
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("DRGA_001"), DRGA_Dungeon.RenoBrassRing, VO_ULDA_Reno_Male_Human_Concede_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_05_01);
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("DRGA_001"), DRGA_Dungeon.RenoBrassRing, VO_DRGA_BOSS_01h_Male_Human_Attack_01);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 106:
			yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_BossAttack_01);
			break;
		case 107:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Reno_Released_04_01);
			}
			break;
		case 108:
			if (!m_Heroic)
			{
				yield return PlayLineInOrderOnceWithBrassRing(GetEnemyActorByCardId("DRGA_001t"), DRGA_Dungeon.RenoBrassRing, m_missionEventTrigger108Lines);
			}
			break;
		case 109:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Reno_Released_01_01);
			}
			break;
		case 110:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.RenoBrassRing, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_02_01);
				yield return PlayLineAlways(DRGA_Dungeon.RenoBrassRing, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Reno_Released_03_01);
			}
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
			case "LOE_079":
			case "ULD_139":
			case "UNG_068":
			case "UNG_842":
			case "UNG_851":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Elise_01);
				break;
			case "DRGA_BOSS_08t2":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_LivingCandleLines);
				break;
			case "LOOT_541":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Togwaggle_01);
				break;
			case "DRG_036":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_Waxadred_01);
				break;
			case "LOOT_117":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_WaxElemental_01);
				break;
			case "DAL_064":
			case "GVG_110":
			case "DAL_431":
			case "DAL_729":
			case "DAL_417":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Player_EVIL_01);
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
			if (cardId == "DRGA_BOSS_08t")
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_08h_Male_Kobold_Good_Fight_11_Boss_WaxLanceLines);
			}
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_11_Misc_03_01);
		}
	}
}
