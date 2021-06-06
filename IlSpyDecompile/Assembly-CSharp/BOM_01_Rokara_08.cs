using System.Collections;
using System.Collections.Generic;

public class BOM_01_Rokara_08 : BoM_01_Rokara_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Death_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Death_01.prefab:d57f499dd401e4f4c839e841707c9605");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8EmoteResponse_01.prefab:6d311296a889b904c9a29cb79dc089c2");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeA_01.prefab:18e711394b1278546a1c7ced7f50dd09");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_01.prefab:b130b40a08b2b1944b2b64fb1db64a7c");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_03.prefab:40c9e53c2f1e50e4ebc51cd4375eebe2");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeC_01.prefab:49b39039325ad3c4887d55ebb094baff");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeD_01.prefab:57bfb32acf9ca8648a4cb862f01d19c7");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeE_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeE_02.prefab:eb076d1bfa8e79b4f96b5dd8e910d986");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_01.prefab:cbef19348ba154f4bbf2bba2704688a2");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_02.prefab:2b284754b8d9e864a80ccc2229cfe166");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_01.prefab:cb5ff3787df26514d80e731b04e6f16d");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_02.prefab:61d04c10c9329a64f8a694ebae888320");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_03.prefab:18360fe4696cd7b448cc10d2624c5556");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_01.prefab:1a8a670ce5f8bf3428a8f3e41fd44217");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_02.prefab:39e9056e2c47fc74e9de0afa5ca9481d");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_03.prefab:3833873a266f11b4c9389705a24beed1");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Intro_01.prefab:a144fec3f78382f44a3d06c2b69c9ecb");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Loss_01.prefab:c42ccb28859f6a24195679f062a5906f");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeA_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeA_02.prefab:a0d3e50809404814da5738a80f8c0e8a");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeB_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeB_02.prefab:4f4e12ba3fd558043b71cf14347ed9f8");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeC_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeC_02.prefab:6e8322ae52f3fe547ab4742e307c2468");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeD_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeD_02.prefab:93efbe7b4663c8a4b98db5de18bcb93b");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeE_01.prefab:31e86b085b455794a81a4827d9c48dbe");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8Intro_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8Intro_02.prefab:f55245828b083094da18790b620bab15");

	private List<string> m_VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeBLines = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_03 };

	private List<string> m_VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeFLines = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_02 };

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_03 };

	private List<string> m_BossIdleLines2 = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_03 };

	private List<string> m_IntroductionLines = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Intro_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8Intro_02 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Death_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8EmoteResponse_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeA_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_03, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeC_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeD_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeE_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_02,
			VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8HeroPower_03, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Idle_03, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Intro_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Loss_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeA_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeB_02,
			VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeC_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeD_02, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeE_01, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8Intro_02
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_TRLFinalBoss;
		m_deathLine = VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Death_01;
		m_standardEmoteResponseLine = VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8EmoteResponse_01;
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
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Intro_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8Intro_02);
			break;
		case 506:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8Loss_01);
			break;
		case 515:
			yield return MissionPlayVO(enemyActor, m_standardEmoteResponseLine);
			break;
		case 500:
			yield return MissionPlayVOOnce(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeC_01);
			yield return MissionPlayVOOnce(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeC_02);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeF_02);
			GameState.Get().SetBusy(busy: false);
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
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeA_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeB_02);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeB_03);
			break;
		case 15:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeD_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeD_02);
			break;
		case 19:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission8ExchangeE_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Rokara_Mission8ExchangeE_02);
			break;
		}
	}
}
