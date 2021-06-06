using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_57h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_01.prefab:ced64f2c110cb67458879002cab82f29");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_02.prefab:bd0e7563bb4f242479d263ccdcd0a1bd");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Death_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Death_01.prefab:3d06a2f7602ace246b80d3ee714c85aa");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_DefeatPlayer_01.prefab:f52bec174e5c1634f9e985a503dada62");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_EmoteResponse_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_EmoteResponse_02.prefab:950c06f75131ddb41a5c6c37ed341fe6");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_01.prefab:43f575d764ffe6e48ae9a659ba97edcb");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_02.prefab:52a2046aa711f7c4ba1b2e2bbde6a7ca");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_03.prefab:6ad216df39a34f0449b0024dcb65828d");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_02.prefab:2da790703ec1d794bb2de1cd945e7c90");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_03.prefab:af328878f7ca4e64fbff865cbfac9f8b");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_04 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_04.prefab:c0e8d64f03b88384a9a632b6b5f2c774");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_05 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_05.prefab:4953e4b0b676ee1499607a8da172a09d");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_06 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_06.prefab:16e03d9ec5545534c94df0c785758b34");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPower_07 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPower_07.prefab:dfcf804334ac8124db554949a9e5fdb9");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_01.prefab:b40dfd799252546419423a095596fda3");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_02.prefab:88fbad2e65dbcf3469033620ec7615c6");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_03.prefab:eb4452cbcb5872145bb308d9253f7635");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Idle_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Idle_01.prefab:5c80ced9bd43b2f4da8241676f883e1d");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Idle_02.prefab:c221b3e6f8dcd9b45a4bd6d659253bf1");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Idle_04 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Idle_04.prefab:2a7995b044d4b034c9bdb6f6f6ca058e");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Idle_05 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Idle_05.prefab:71ee0407c84ffef4785aaea1d2be9c9d");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Intro_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Intro_01.prefab:a7d3855778023c24bacc5a87042b59c1");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_IntroChu_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_IntroChu_01.prefab:ca36347bf274b10419017d5941c61734");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_IntroEudora_01.prefab:ecfa97d881ff8ca40be5b18bc6c75e21");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_IntroVessina_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_IntroVessina_01.prefab:1e5fd968703990f43a270002a14dd038");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Misc_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Misc_02.prefab:2f5b3c750b527c94aa93edbc468b6647");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Misc_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Misc_03.prefab:8110c73b9f92d2244bc9937ac18badad");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_Misc_04 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_Misc_04.prefab:52b0f7d192ef6fe43a5fbbdd51e18ab7");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerBad_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerBad_01.prefab:be8a39fbb8e59394d8fec39444a44c85");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerBagOfCoins_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerBagOfCoins_01.prefab:890661ed9d9b69e4d955e1ff2830f022");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerBurgle_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerBurgle_01.prefab:11142355acb01b642a3d2805ac8f20c6");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_02.prefab:b703c2f718c7599489b7ee4eae340832");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_03.prefab:744b469db5ddb2841b4daf1988cf4ebb");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinFirst_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinFirst_01.prefab:ce694a72971033f4dbf419d87ed46f0d");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinIntoNothing_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinIntoNothing_01.prefab:a7bea1563689a7e42928e2446c735f6b");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerFlyBy_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerFlyBy_01.prefab:48f6ac39b4697264cbcdd8587704752d");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_01.prefab:32b7750008044614f8e1acf82d896dcd");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_02.prefab:4fabbdba6e96ec84cbe84fd8a53eafa8");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_03 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_03.prefab:d769bb53029cae849a8f186a88fd1fe3");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerPickPocket_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerPickPocket_01.prefab:804d4d9ab14b64945917af6615990466");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerTradePrince_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerTradePrince_01.prefab:60450c82f88c3494fbd1d65744f71ca2");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_01 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_01.prefab:a39ac99ecf7147040a72e283b3946d31");

	private static readonly AssetReference VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_02 = new AssetReference("VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_02.prefab:697f599064e2c5d4dbd8f0cf652e5844");

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_57h_Male_Goblin_HeroPower_02, VO_DALA_BOSS_57h_Male_Goblin_HeroPower_03, VO_DALA_BOSS_57h_Male_Goblin_HeroPower_04, VO_DALA_BOSS_57h_Male_Goblin_HeroPower_05, VO_DALA_BOSS_57h_Male_Goblin_HeroPower_06, VO_DALA_BOSS_57h_Male_Goblin_HeroPower_07 };

	private static List<string> m_HeroPowerFull = new List<string> { VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_01, VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_02, VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_03 };

	private static List<string> m_BossTreasure = new List<string> { VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_01, VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_02 };

	private static List<string> m_TurnStart = new List<string> { VO_DALA_BOSS_57h_Male_Goblin_Misc_02, VO_DALA_BOSS_57h_Male_Goblin_Misc_03, VO_DALA_BOSS_57h_Male_Goblin_Misc_04 };

	private static List<string> m_PlayerGallywixCoin = new List<string> { VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_01, VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_02, VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_03 };

	private static List<string> m_BossHeroPowerPlayerTreasure = new List<string> { VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_01, VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_02 };

	private static List<string> m_PlayerCoin = new List<string> { VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_02, VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_03 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_57h_Male_Goblin_Idle_01, VO_DALA_BOSS_57h_Male_Goblin_Idle_02, VO_DALA_BOSS_57h_Male_Goblin_Idle_04, VO_DALA_BOSS_57h_Male_Goblin_Idle_05 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_01, VO_DALA_BOSS_57h_Male_Goblin_BossTreasure_02, VO_DALA_BOSS_57h_Male_Goblin_Death_01, VO_DALA_BOSS_57h_Male_Goblin_DefeatPlayer_01, VO_DALA_BOSS_57h_Male_Goblin_EmoteResponse_02, VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_01, VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_02, VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_03, VO_DALA_BOSS_57h_Male_Goblin_HeroPower_02, VO_DALA_BOSS_57h_Male_Goblin_HeroPower_03,
			VO_DALA_BOSS_57h_Male_Goblin_HeroPower_04, VO_DALA_BOSS_57h_Male_Goblin_HeroPower_05, VO_DALA_BOSS_57h_Male_Goblin_HeroPower_06, VO_DALA_BOSS_57h_Male_Goblin_HeroPower_07, VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_01, VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_02, VO_DALA_BOSS_57h_Male_Goblin_HeroPowerFull_03, VO_DALA_BOSS_57h_Male_Goblin_Idle_01, VO_DALA_BOSS_57h_Male_Goblin_Idle_02, VO_DALA_BOSS_57h_Male_Goblin_Idle_04,
			VO_DALA_BOSS_57h_Male_Goblin_Idle_05, VO_DALA_BOSS_57h_Male_Goblin_Intro_01, VO_DALA_BOSS_57h_Male_Goblin_IntroChu_01, VO_DALA_BOSS_57h_Male_Goblin_IntroEudora_01, VO_DALA_BOSS_57h_Male_Goblin_IntroVessina_01, VO_DALA_BOSS_57h_Male_Goblin_Misc_02, VO_DALA_BOSS_57h_Male_Goblin_Misc_03, VO_DALA_BOSS_57h_Male_Goblin_Misc_04, VO_DALA_BOSS_57h_Male_Goblin_PlayerBad_01, VO_DALA_BOSS_57h_Male_Goblin_PlayerBagOfCoins_01,
			VO_DALA_BOSS_57h_Male_Goblin_PlayerBurgle_01, VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_02, VO_DALA_BOSS_57h_Male_Goblin_PlayerCoin_03, VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinFirst_01, VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinIntoNothing_01, VO_DALA_BOSS_57h_Male_Goblin_PlayerFlyBy_01, VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_01, VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_02, VO_DALA_BOSS_57h_Male_Goblin_PlayerGallywixCoin_03, VO_DALA_BOSS_57h_Male_Goblin_PlayerPickPocket_01,
			VO_DALA_BOSS_57h_Male_Goblin_PlayerTradePrince_01, VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_01, VO_DALA_BOSS_57h_Male_Goblin_PlayerTreasure_02
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
		m_introLine = VO_DALA_BOSS_57h_Male_Goblin_Intro_01;
		m_deathLine = VO_DALA_BOSS_57h_Male_Goblin_Death_01;
		m_standardEmoteResponseLine = VO_DALA_BOSS_57h_Male_Goblin_EmoteResponse_02;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
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
			if (cardId == "DALA_Chu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_57h_Male_Goblin_IntroChu_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_Vessina")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_57h_Male_Goblin_IntroVessina_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Eudora")
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_02);
			break;
		case 103:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_57h_Male_Goblin_Exposition_Story_03);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_TurnStart);
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_57h_Male_Goblin_PlayerBad_01);
			break;
		case 106:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_57h_Male_Goblin_PlayerBagOfCoins_01);
			break;
		case 107:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinFirst_01);
			break;
		case 108:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossTreasure);
			break;
		case 109:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 110:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerFull);
			break;
		case 111:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_57h_Male_Goblin_PlayerCoinIntoNothing_01);
			break;
		case 112:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossHeroPowerPlayerTreasure);
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
			case "GVG_028t":
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_PlayerGallywixCoin);
				break;
			case "GAME_005":
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_PlayerCoin);
				break;
			case "AT_033":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_57h_Male_Goblin_PlayerBurgle_01);
				break;
			case "DALA_716":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_57h_Male_Goblin_PlayerFlyBy_01);
				break;
			case "GIL_696":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_57h_Male_Goblin_PlayerPickPocket_01);
				break;
			case "GVG_028":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_57h_Male_Goblin_PlayerTradePrince_01);
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}
}
