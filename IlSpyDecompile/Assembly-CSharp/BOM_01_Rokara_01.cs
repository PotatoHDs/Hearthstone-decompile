using System.Collections;
using System.Collections.Generic;

public class BOM_01_Rokara_01 : BoM_01_Rokara_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Death_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Death_01.prefab:558fd71a615ee7649baff1608da94a61");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1EmoteResponse_01.prefab:1e299a6d247708a488eb6d11f3d9a01d");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeA_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeA_02.prefab:8f3b42b3b2354eb4292a1abe39ab8e98");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeB_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeB_02.prefab:aab232a63fe10264a9e4bac58152a74c");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_01.prefab:25e6aa451cb3d374b9fcda4bed2d1609");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_03.prefab:39e2e513727070d4d98aa856d82f6fe7");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeE_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeE_02.prefab:edaa6063f7105b847ac2ebfdb5e2e94b");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_01.prefab:c0131a4f26cb87d44b4e547dac7dcadd");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_02.prefab:46d446e0cec712141a452109deb8bdcc");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_03.prefab:90cdff5762574f844aafbb195aa8c39d");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_01.prefab:faf2d4c922898584a88d1fd38d2263a5");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_02.prefab:182b3be79b0e0744c86f4773f671d72c");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_03.prefab:c9c2b13f47ce1f64bba15e914767f404");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_01.prefab:871c60506dc8f9b4683082b7629dc2eb");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_02.prefab:40de8524b612fc44e8a4156a4217001a");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_03.prefab:6b477cec73f4614468f2ef3366783b34");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Intro_02.prefab:0d71ed8d56164384eaa5d479ee364693");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Loss_01.prefab:b863a0ebc8636e24284d8ed96d542c80");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeA_01.prefab:5b3a56f5e6db0d44fb5b1e1a90668c41");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeC_02.prefab:5a9171a6b57489f41b4ffe153bf882f3");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_01.prefab:d11dfdfc84ff48d4993763b12cb45447");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_03.prefab:9f6b6e3cd4318524baab74d128f390d7");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeB_01.prefab:9a6eb3154953edf4fad2519c48cd5093");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeC_01.prefab:bd7647c719bc4624a877d55bc2a14163");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeD_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeD_02.prefab:2de8df243737b3c4fb00332062a04223");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeE_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeE_01.prefab:c8853146456ebd34f97697d3b051ff48");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Intro_01.prefab:3965fb11169706840862c690ff3f9157");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Victory_02.prefab:2f45585fa7c0e8e4088ee39c21f5c2c9");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_03 };

	private List<string> m_VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HitLines = new List<string> { VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_03 };

	private List<string> m_BossIdleLines2 = new List<string> { VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_03 };

	private List<string> m_IntroductionLines = new List<string> { VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Intro_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Intro_01 };

	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Victory_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public const int InGame_HitResponse = 58028;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Death_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1EmoteResponse_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeA_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeB_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_03, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeE_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HeroPower_03,
			VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Hit_03, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Idle_03, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Intro_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Loss_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeA_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeC_02,
			VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_03, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeB_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeC_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeD_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeE_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Intro_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Victory_02
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetBossIdleLines()
	{
		return m_BossIdleLines2;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_BossUsesHeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_OverrideMusicTrack = MusicPlaylistType.InGame_TRL;
		m_deathLine = VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Death_01;
		m_standardEmoteResponseLine = VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1EmoteResponse_01;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 514:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Intro_02);
			break;
		case 506:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1Loss_01);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(Garrosh_BrassRing, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1Victory_02);
			yield return MissionPlayVO(Garrosh_BrassRing, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1Victory_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, m_standardEmoteResponseLine);
			break;
		case 58028:
			yield return MissionPlayVOOnceInOrder(enemyActor, m_VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1HitLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
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
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 3:
			yield return MissionPlayVO(Garrosh_BrassRing, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeA_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeB_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeB_02);
			break;
		case 11:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeC_01);
			yield return MissionPlayVO(Garrosh_BrassRing, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission1ExchangeC_02);
			break;
		case 15:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeD_02);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeD_03);
			break;
		case 17:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission1ExchangeE_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission1ExchangeE_02);
			break;
		}
	}
}
