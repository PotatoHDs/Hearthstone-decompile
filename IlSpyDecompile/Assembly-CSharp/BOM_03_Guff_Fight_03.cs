using System.Collections;
using System.Collections.Generic;

public class BOM_03_Guff_Fight_03 : BOM_03_Guff_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Death_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Death_01.prefab:cee9af5f93604f543ae52d62c22b7a25");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3EmoteResponse_01.prefab:32fb8d759269a53448e1f4c340114377");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeA_01.prefab:6f38b4201afb1564997c88ea4f83ce4a");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeC_01.prefab:10e399729caffd44a8de0e0abbd3b91b");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeD_01.prefab:a37d62b84f87f3b47a8dc75cd5fac52d");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeE_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeE_01.prefab:00acdc842b6f21245acd3f69b6dcb69a");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_01.prefab:d6db2227c9dcb7345a05de10aea7b262");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_02.prefab:568e12955b775ba48b69f4dee0bd1c53");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_03.prefab:708e426e1a5f7bc48b90d0dd6ba20a79");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_01.prefab:2966bd6c1b4df3e4da8af264c7fa9b3c");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_02.prefab:417d4a0429e188d43b084fcf6337def6");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_03.prefab:107f51e08e2990a4aacbcda196c57ed3");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Intro_01.prefab:8ce19f1faf9bcd4428b548c730ce9df7");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Loss_01.prefab:74097b99d6a97b34fb2aa5921fc44b9d");

	private static readonly AssetReference VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Victory_01.prefab:a3b75749d72463a4ab2e9a28e9ea3c90");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeB_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeB_Brukan_01.prefab:c8fb9c3f4ad13394c947128840ddcdff");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeE_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeE_Brukan_02.prefab:ab066736eb5110f4fb3dbca7a2978702");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeF_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeF_Brukan_01.prefab:8f5f8ab142f5af849a8cb3cf4c6de376");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeB_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeB_Dawngrasp_01.prefab:df598c66148541e438799120c962ee15");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeE_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeE_Dawngrasp_02.prefab:764961b1eea3dd742aaeb91350500d7b");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeF_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeF_Dawngrasp_01.prefab:e90b2b90f9760d743a5ad217a167d43c");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeA_02.prefab:29323b534b4df5d429b9fb8598d3ca4b");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeB_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeB_02.prefab:0a906a22de54fa04ea82aad5d263dad0");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeC_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeC_02.prefab:5e03a6199b31406439dafbff248532bc");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeD_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeD_02.prefab:a20adbfd29782e049bee7e3ef76be2b1");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeF_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeF_02.prefab:64a3d3b664750074ca89bc09a1920d4a");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Intro_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Intro_02.prefab:bad5f7eccc3015d4d890c6d5e5f38271");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_02.prefab:9068e16b9b4ef01458b4e8f3d7a373a9");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_03 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_03.prefab:5af3cccce52c3e3409f2400016fe2ece");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeB_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeB_Rokara_01.prefab:b733e70b33b50ef4bb0ac21c34816314");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeE_Rokara_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeE_Rokara_02.prefab:5281b55d11c7b554ab4d0b7511154c90");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeF_Rokara_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeF_Rokara_01.prefab:a3821aa1d808dec438c6b547c74ec47a");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeB_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeB_Tamsin_01.prefab:fd364d8bf64045e4799b889a1238502b");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeE_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeE_Tamsin_02.prefab:81f792a5b47374b46881492f8b316f46");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeF_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeF_Tamsin_01.prefab:3deb367ee4af2e242859e9dab2cc7e8a");

	private List<string> m_InGame_BossIdle = new List<string> { VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_02, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_03 };

	private List<string> m_InGame_BossUsesHeroPower = new List<string> { VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_02, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Death_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3EmoteResponse_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeA_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeC_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeD_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeE_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_02, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3HeroPower_03, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_01,
			VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_02, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Idle_03, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Intro_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Loss_01, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Victory_01, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeB_Brukan_01, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeE_Brukan_02, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeF_Brukan_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeB_Dawngrasp_01, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeE_Dawngrasp_02,
			VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeF_Dawngrasp_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeA_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeB_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeC_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeD_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeF_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Intro_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_03, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeB_Rokara_01,
			VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeE_Rokara_02, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeF_Rokara_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeB_Tamsin_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeE_Tamsin_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeF_Tamsin_01
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
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
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 514:
			yield return MissionPlayVO(actor, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Intro_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Intro_02);
			break;
		case 515:
			yield return MissionPlayVO(actor, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3EmoteResponse_01);
			break;
		case 516:
			yield return MissionPlaySound(actor, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Death_01);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(actor, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(actor, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_02);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3Victory_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 517:
			yield return MissionPlayVO(actor, m_InGame_BossIdle);
			break;
		case 510:
			yield return MissionPlayVO(actor, m_InGame_BossUsesHeroPower);
			break;
		case 100:
			GameState.Get().SetBusy(busy: true);
			if (HeroPowerBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeB_Brukan_01);
			}
			if (HeroPowerDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeB_Dawngrasp_01);
			}
			if (HeroPowerRokara)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeB_Rokara_01);
			}
			if (HeroPowerTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeB_Tamsin_01);
			}
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeB_02);
			GameState.Get().SetBusy(busy: false);
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
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
		case 5:
			yield return MissionPlayVO(actor, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeA_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO(actor, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeC_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeC_02);
			break;
		case 9:
			yield return MissionPlayVO(actor, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeD_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeD_02);
			break;
		case 11:
			yield return MissionPlayVO(actor, VO_Story_Hero_Barak_Male_Centaur_Story_Guff_Mission3ExchangeE_01);
			if (HeroPowerBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeE_Brukan_02);
			}
			if (HeroPowerDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeE_Dawngrasp_02);
			}
			if (HeroPowerRokara)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeE_Rokara_02);
			}
			if (HeroPowerTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeE_Tamsin_02);
			}
			break;
		case 15:
			if (HeroPowerBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission3ExchangeF_Brukan_01);
			}
			if (HeroPowerDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission3ExchangeF_Dawngrasp_01);
			}
			if (HeroPowerRokara)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission3ExchangeF_Rokara_01);
			}
			if (HeroPowerTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission3ExchangeF_Tamsin_01);
			}
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission3ExchangeF_02);
			break;
		}
	}
}
