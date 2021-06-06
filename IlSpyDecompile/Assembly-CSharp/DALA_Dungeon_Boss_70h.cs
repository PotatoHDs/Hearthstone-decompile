using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_70h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOE_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOE_02.prefab:1731bb7643ecc6b458489fa31d238996");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOEFails_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOEFails_01.prefab:a26d844748379854b9646e306cff803f");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_BossCannonBarrage_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_BossCannonBarrage_01.prefab:4bc804a986342e147bebd557ba52281b");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_Death_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_Death_02.prefab:45b8dc245a322da4c91474d136e418f8");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_DefeatPlayer_02.prefab:27ad771ecb0a10043a32806d46fa2bbb");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_EmoteResponse_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_EmoteResponse_02.prefab:fa042e4963a6a76428643fa7bde36bdb");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPower_01.prefab:d56cfe445ec8038438debd15b5d6e93f");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPower_02.prefab:1e67d0ff0c0d80e438703a769cf95fea");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPower_03.prefab:aaf53c2f41f581b49b00432abcc48c1c");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPower_04.prefab:a47b195542e5bd24288db03e1c86d54c");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPower_05 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPower_05.prefab:05718bf4fbd395a41b2f00c16b4794e7");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_01.prefab:789e2c223b4c61141a38735de6d8af9b");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_02 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_02.prefab:5897826543b6e7843a01508fe6c4dafc");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_03 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_03.prefab:f3f9b7609ba28754b99575dbeec6c052");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_04 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_04.prefab:e0b6303a4c1c5264e8aab32421aebbe9");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_Idle_01.prefab:8ca5709e2c4daa946826958ed8d4ac9c");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_Idle_03.prefab:25f799d4e18a2e04a885700ec82d8031");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_Idle_04 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_Idle_04.prefab:909314d819475ff429cb3b13064add7b");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_Intro_01.prefab:9b04f5c16afac3942b67b61d605b2db1");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_IntroEudora_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_IntroEudora_01.prefab:9e0f04a3ae2d3ad41b8c6dd2c6154d8a");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_PlayerFlyBy_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_PlayerFlyBy_01.prefab:bcceb68d0f83e2f4db121f7ff2bdd734");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_PlayerPirate_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_PlayerPirate_01.prefab:fae4fd562e967f1459d41c8086d4e755");

	private static readonly AssetReference VO_DALA_BOSS_70h_Male_Human_PlayerShipCannon_01 = new AssetReference("VO_DALA_BOSS_70h_Male_Human_PlayerShipCannon_01.prefab:0d7551091576d0447b3bae2589bf567d");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_70h_Male_Human_Idle_01, VO_DALA_BOSS_70h_Male_Human_Idle_03, VO_DALA_BOSS_70h_Male_Human_Idle_04 };

	private static List<string> m_HeroPower = new List<string> { VO_DALA_BOSS_70h_Male_Human_HeroPower_01, VO_DALA_BOSS_70h_Male_Human_HeroPower_02, VO_DALA_BOSS_70h_Male_Human_HeroPower_03, VO_DALA_BOSS_70h_Male_Human_HeroPower_04, VO_DALA_BOSS_70h_Male_Human_HeroPower_05 };

	private static List<string> m_HeroPowerReloadingForOneTurn = new List<string> { VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_01, VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_03, VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_04 };

	private static List<string> m_HeroPowerReloadingForTwoTurns = new List<string> { VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_02, VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_03, VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOE_02, VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOEFails_01, VO_DALA_BOSS_70h_Male_Human_BossCannonBarrage_01, VO_DALA_BOSS_70h_Male_Human_Death_02, VO_DALA_BOSS_70h_Male_Human_DefeatPlayer_02, VO_DALA_BOSS_70h_Male_Human_EmoteResponse_02, VO_DALA_BOSS_70h_Male_Human_HeroPower_01, VO_DALA_BOSS_70h_Male_Human_HeroPower_02, VO_DALA_BOSS_70h_Male_Human_HeroPower_03, VO_DALA_BOSS_70h_Male_Human_HeroPower_04,
			VO_DALA_BOSS_70h_Male_Human_HeroPower_05, VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_01, VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_02, VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_03, VO_DALA_BOSS_70h_Male_Human_HeroPowerReloading_04, VO_DALA_BOSS_70h_Male_Human_Idle_01, VO_DALA_BOSS_70h_Male_Human_Idle_03, VO_DALA_BOSS_70h_Male_Human_Idle_04, VO_DALA_BOSS_70h_Male_Human_Intro_01, VO_DALA_BOSS_70h_Male_Human_IntroEudora_01,
			VO_DALA_BOSS_70h_Male_Human_PlayerFlyBy_01, VO_DALA_BOSS_70h_Male_Human_PlayerPirate_01, VO_DALA_BOSS_70h_Male_Human_PlayerShipCannon_01
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
		m_introLine = VO_DALA_BOSS_70h_Male_Human_Intro_01;
		m_deathLine = VO_DALA_BOSS_70h_Male_Human_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_70h_Male_Human_EmoteResponse_02;
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
			if (cardId != "DALA_Eudora")
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
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerReloadingForOneTurn);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerReloadingForTwoTurns);
			break;
		case 104:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_70h_Male_Human_PlayerPirate_01);
			break;
		case 105:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOE_02);
			break;
		case 106:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_70h_Male_Human_BossBombardmentAOEFails_01);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		if (!(cardId == "DALA_716"))
		{
			if (cardId == "GVG_075")
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_70h_Male_Human_PlayerShipCannon_01);
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_70h_Male_Human_PlayerFlyBy_01);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "TRL_127")
			{
				yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_70h_Male_Human_BossCannonBarrage_01);
			}
		}
	}
}
