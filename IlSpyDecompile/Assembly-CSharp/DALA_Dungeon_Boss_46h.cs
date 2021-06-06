using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_46h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BossHealingRain_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BossHealingRain_01.prefab:7d073ff27c8508f45a833413a478be03");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BossTidalSurge_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BossTidalSurge_01.prefab:02c12b1253f9c6e4e89f61bae3fe0086");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BossWaterElemental_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BossWaterElemental_01.prefab:9d6a1f4c54be19a4f92cb74768c5514f");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BubblePop_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BubblePop_01.prefab:94196958f5cd37042b9d86e13f73ff0c");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BubblePop_02 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BubblePop_02.prefab:23ea3166544cd544cab6e50f64221455");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_BubblePop_03 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_BubblePop_03.prefab:08f6076197cb3fc41b816b708a8e327d");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Death_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Death_01.prefab:5f1465caf854da4469933eb35f1c9bb0");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_DefeatPlayer_01.prefab:451596d65fa5e6d4cb1ed0bce96a432a");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_EmoteResponse_01.prefab:7d364babbc8653441bc62d9d9707315c");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_HeroPower_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_HeroPower_01.prefab:0437664f676953f4d95361072710aa8d");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_HeroPower_02 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_HeroPower_02.prefab:a96246c1409d16243b57154d74133677");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_HeroPower_03 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_HeroPower_03.prefab:e4e937d03d308194d9ca90634c3f9882");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Idle_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Idle_01.prefab:73249301b84d43845a6edb921c17f003");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Idle_02 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Idle_02.prefab:f472d2ac36eb7c143869808c087bb22a");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Idle_03 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Idle_03.prefab:01c15d562f1753c4dbcd6720e56dc2d6");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Intro_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Intro_01.prefab:86e7f5cb0b765e147b8050594f6f788f");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_IntroRakanishu_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_IntroRakanishu_01.prefab:253ad7e35748ed34491af16e6c210e26");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Misc_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Misc_01.prefab:caafe958a1ae3b84c9496b0eeb3dd551");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Misc_02 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Misc_02.prefab:4c8e798d5133ca24091c7f8399ffe768");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_Misc_03 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_Misc_03.prefab:865c50de1960ffb46b18fb4f95cbf4b5");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_PlayerFireSpell_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_PlayerFireSpell_01.prefab:a20733a76b2e0874d86015e9f5050b4d");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_PlayerLightningSpell_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_PlayerLightningSpell_01.prefab:3526a390162c0a44b96f0605870d3e6c");

	private static readonly AssetReference VO_DALA_BOSS_46h_Female_Revenant_PlayerMech_01 = new AssetReference("VO_DALA_BOSS_46h_Female_Revenant_PlayerMech_01.prefab:afbd797449420d94e997a96537c93df2");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_46h_Female_Revenant_Idle_01, VO_DALA_BOSS_46h_Female_Revenant_Idle_02, VO_DALA_BOSS_46h_Female_Revenant_Idle_03 };

	private static List<string> m_BubblePop = new List<string> { VO_DALA_BOSS_46h_Female_Revenant_BubblePop_01, VO_DALA_BOSS_46h_Female_Revenant_BubblePop_02, VO_DALA_BOSS_46h_Female_Revenant_BubblePop_03 };

	private static List<string> m_HeroDamageGT5 = new List<string> { VO_DALA_BOSS_46h_Female_Revenant_Misc_01, VO_DALA_BOSS_46h_Female_Revenant_Misc_02, VO_DALA_BOSS_46h_Female_Revenant_Misc_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_46h_Female_Revenant_BossHealingRain_01, VO_DALA_BOSS_46h_Female_Revenant_BossTidalSurge_01, VO_DALA_BOSS_46h_Female_Revenant_BossWaterElemental_01, VO_DALA_BOSS_46h_Female_Revenant_BubblePop_01, VO_DALA_BOSS_46h_Female_Revenant_BubblePop_02, VO_DALA_BOSS_46h_Female_Revenant_BubblePop_03, VO_DALA_BOSS_46h_Female_Revenant_Death_01, VO_DALA_BOSS_46h_Female_Revenant_DefeatPlayer_01, VO_DALA_BOSS_46h_Female_Revenant_EmoteResponse_01, VO_DALA_BOSS_46h_Female_Revenant_HeroPower_01,
			VO_DALA_BOSS_46h_Female_Revenant_HeroPower_02, VO_DALA_BOSS_46h_Female_Revenant_HeroPower_03, VO_DALA_BOSS_46h_Female_Revenant_Idle_01, VO_DALA_BOSS_46h_Female_Revenant_Idle_02, VO_DALA_BOSS_46h_Female_Revenant_Idle_03, VO_DALA_BOSS_46h_Female_Revenant_Intro_01, VO_DALA_BOSS_46h_Female_Revenant_IntroRakanishu_01, VO_DALA_BOSS_46h_Female_Revenant_Misc_01, VO_DALA_BOSS_46h_Female_Revenant_Misc_02, VO_DALA_BOSS_46h_Female_Revenant_Misc_03,
			VO_DALA_BOSS_46h_Female_Revenant_PlayerFireSpell_01, VO_DALA_BOSS_46h_Female_Revenant_PlayerLightningSpell_01, VO_DALA_BOSS_46h_Female_Revenant_PlayerMech_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_46h_Female_Revenant_Intro_01;
		m_deathLine = VO_DALA_BOSS_46h_Female_Revenant_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_46h_Female_Revenant_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { VO_DALA_BOSS_46h_Female_Revenant_HeroPower_01, VO_DALA_BOSS_46h_Female_Revenant_HeroPower_02, VO_DALA_BOSS_46h_Female_Revenant_HeroPower_03 };
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Rakanishu")
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
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BubblePop);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroDamageGT5);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_46h_Female_Revenant_PlayerMech_01);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "CS2_029":
			case "GIL_147":
			case "LOOTA_827":
			case "CFM_094":
			case "CFM_621t16":
			case "CFM_621t2":
			case "CFM_621t25":
			case "CFM_662":
			case "CS2_032":
			case "KAR_076":
			case "LOOTA_BOSS_26p6":
			case "TRL_313":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_46h_Female_Revenant_PlayerFireSpell_01);
				break;
			case "GIL_600":
			case "BOT_246":
			case "BOTA_235":
			case "CFM_707":
			case "EX1_238":
			case "EX1_251":
			case "EX1_259":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_46h_Female_Revenant_PlayerLightningSpell_01);
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
			case "LOOT_373":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_46h_Female_Revenant_BossHealingRain_01);
				break;
			case "UNG_817":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_46h_Female_Revenant_BossTidalSurge_01);
				break;
			case "CS2_033":
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_46h_Female_Revenant_BossWaterElemental_01);
				break;
			}
		}
	}
}
