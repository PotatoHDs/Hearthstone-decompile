using System.Collections;
using System.Collections.Generic;

public class BOM_01_Rokara_02 : BoM_01_Rokara_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2ExchangeD_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2ExchangeD_02.prefab:d31a95159c60fab4b936fc14eb97799d");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_01.prefab:0319689001b1ec44e84796681d7ba83e");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_03.prefab:bfbb391cf4b8f4e4fa6dbd5d526114a2");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeC_02.prefab:ee5e64a48e99ed94da8ce4177710d33a");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_01.prefab:4940ad74fbbd22d42bb9331fb92f64ca");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_03.prefab:fe383ad089fac5a40a10dc2f6bc7e0b7");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Death_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Death_01.prefab:221c8690e2666f745b0273e44ae735ad");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2EmoteResponse_01.prefab:1d7bdd581c9543543948e42bbe638099");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeA_02.prefab:4c4d9f428bafe5240a28a6c1e3433575");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeB_02.prefab:d12f5ef52ebdc5742b03ca513799b920");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeD_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeD_01.prefab:08a0cac0d571183449701ec0aada45ae");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_01.prefab:6b2b2cd4eb8509b45b6439e956d1915b");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_02.prefab:10e9e6734ac6b7848aa719c881446586");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_03.prefab:3624cc666661b0644a1916290096e4a2");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_01.prefab:5faa6215bc596564ab19f153b3434d35");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_02 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_02.prefab:9061673071052ba46bbf7d93afed7a29");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_03 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_03.prefab:95d11bff2f77afc41a00ebfce1ddd3e0");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Intro_02 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Intro_02.prefab:8e4ed929e024a1241b5c6d1280e66922");

	private static readonly AssetReference VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Loss_01 = new AssetReference("VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Loss_01.prefab:6606ec7237e9f6a4a90e9c3b99af4b04");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_01.prefab:f1e2fe2989b6b034aae777f2302b19bc");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_03.prefab:7da1b3ea252090e498b5f3dab9b62980");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeC_01.prefab:ee6ef62283f67d24fafe8f639631e492");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Intro_01.prefab:d6b4de7c0e6e15c49aa2847e1228096e");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Victory_02.prefab:36abe244d5d19014a9b084597fd6a926");

	private List<string> m_VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeALines = new List<string> { VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_03 };

	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string> { VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_03, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Victory_02 };

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_01, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_02, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_03 };

	private List<string> m_BossIdleLines2 = new List<string> { VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_01, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_02, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_03 };

	private List<string> m_IntroductionLines = new List<string> { VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Intro_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Intro_01 };

	private List<string> m_VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeBLines = new List<string> { VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2ExchangeD_02, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_03, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeC_02, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_03, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Death_01, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2EmoteResponse_01, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeA_02, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeB_02,
			VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeD_01, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_01, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_02, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2HeroPower_03, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_01, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_02, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Idle_03, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Intro_02, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Loss_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_01,
			VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_03, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeC_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Intro_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Victory_02
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		m_deathLine = VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Death_01;
		m_standardEmoteResponseLine = VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2EmoteResponse_01;
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
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Intro_02);
			break;
		case 506:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2Loss_01);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(Guff_BrassRing, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2Victory_02);
			yield return MissionPlayVO(Guff_BrassRing, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2Victory_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, m_standardEmoteResponseLine);
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
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 3:
			yield return MissionPlayVO("BOM_01_Guff_02t", Guff_BrassRing, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeA_02);
			yield return MissionPlayVO("BOM_01_Guff_02t", Guff_BrassRing, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeA_03);
			break;
		case 5:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeB_02);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeB_03);
			break;
		case 9:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission2ExchangeC_01);
			yield return MissionPlayVO("BOM_01_Guff_02t", Guff_BrassRing, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission2ExchangeC_02);
			break;
		case 13:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_HezrulBloodmark_Male_Centaur_Story_Rokara_Mission2ExchangeD_01);
			yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission2ExchangeD_02);
			break;
		}
	}
}
