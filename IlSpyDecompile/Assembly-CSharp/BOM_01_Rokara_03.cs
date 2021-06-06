using System.Collections;
using System.Collections.Generic;

public class BOM_01_Rokara_03 : BoM_01_Rokara_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeB_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeB_Brukan_01.prefab:db17c264335d9814c813bb19a750b4c2");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeC_Brukan_01.prefab:ce169ec3e1701f64dbb9434bcbe6e512");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02.prefab:f4be4eeaeefddd14da2e1370d99ef7ba");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03.prefab:2536f28dc7d608042b46c91dfd86cacc");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeB_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeB_Guff_01.prefab:19cbc0432217f6547bfbf89ae89566ea");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeC_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeC_Guff_01.prefab:f19ea73d8f592344a8fc49c2453554ab");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Death_01 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Death_01.prefab:2d8727ce2f2d9bf49a9885d707a35435");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3EmoteResponse_01.prefab:fa9b394093dfdf548b3c4c9e0cc35699");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3ExchangeA_02.prefab:005ebdc69d2fb7d4c96029f54e6be81a");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_01.prefab:7dda034986b038f40bba160a167c4bea");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_02.prefab:9a7138582dedeee438aab61141d9d457");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_03.prefab:8b1a01542cdffd246a9c496d7b7871cd");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_01.prefab:574ee39549d2f32469ced4ee36e45727");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_02.prefab:a555e07ad916c634184491fd82335a1c");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_03.prefab:bfa752deee9e30142973234733d22f64");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Intro_02 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Intro_02.prefab:4ede0de7ace3c664f9ba6ac2ddd1c8df");

	private static readonly AssetReference VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Loss_01.prefab:739063e37010c0b488cbb98222f7f1c3");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeB_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeB_03.prefab:ff6ca8274ea77f145b020306a9a75545");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeC_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeC_03.prefab:5d65f0c3b04617a4e985f13f70965c36");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3Intro_01.prefab:c47d620247ce8b944b1259f04a3b4c55");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeA_01.prefab:980e891e7c9f28741973916504794534");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_01.prefab:7802f98054a13054cb549bb017204eaf");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_02.prefab:1dd9acdc656ee154998143ca65d1ffbd");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3Victory_01.prefab:375355c25d8988b4da3f87cbd83f0888");

	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string> { VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3Victory_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03 };

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_01, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_02, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_03 };

	private List<string> m_BossIdleLines2 = new List<string> { VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_01, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_02, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_03 };

	private List<string> m_IntroductionLines = new List<string> { VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3Intro_01, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Intro_02 };

	private List<string> m_VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeDLines = new List<string> { VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeB_Brukan_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeC_Brukan_01, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeB_Guff_01, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeC_Guff_01, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Death_01, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3EmoteResponse_01, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3ExchangeA_02, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_01,
			VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_02, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3HeroPower_03, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_01, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_02, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Idle_03, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Intro_02, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Loss_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeB_03, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeC_03, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3Intro_01,
			VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeA_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3Victory_01
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
		m_SupressEnemyDeathTextBubble = true;
		m_deathLine = VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Death_01;
		m_standardEmoteResponseLine = VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3EmoteResponse_01;
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 514:
			yield return MissionPlayVO(actor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3Intro_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Intro_02);
			break;
		case 506:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3Loss_01);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(Tamsin_BrassRing, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3Victory_01);
			if (HeroPowerIsBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02);
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03);
			}
			else
			{
				yield return MissionPlayVO(Brukan_BrassRing, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_02);
				yield return MissionPlayVO(Brukan_BrassRing, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3Victory_03);
			}
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
			yield return MissionPlayVO("BOM_01_Tamsin_03t", Tamsin_BrassRing, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeA_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Plaguemaw_Male_Quilboar_Story_Rokara_Mission3ExchangeA_02);
			break;
		case 5:
			if (HeroPowerIsBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeB_Brukan_01);
			}
			if (HeroPowerIsGuff)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeB_Guff_01);
			}
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeB_03);
			break;
		case 9:
			if (HeroPowerIsBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission3ExchangeC_Brukan_01);
			}
			if (HeroPowerIsGuff)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission3ExchangeC_Guff_01);
			}
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission3ExchangeC_03);
			break;
		case 11:
			yield return MissionPlayVO("BOM_01_Tamsin_03t", Tamsin_BrassRing, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_01);
			yield return MissionPlayVO("BOM_01_Tamsin_03t", Tamsin_BrassRing, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission3ExchangeD_02);
			break;
		}
	}
}
