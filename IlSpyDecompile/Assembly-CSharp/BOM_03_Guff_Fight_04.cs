using System.Collections;
using System.Collections.Generic;

public class BOM_03_Guff_Fight_04 : BOM_03_Guff_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeB_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeB_Brukan_02.prefab:7d3cf9a21724aa948b62b48b572ba88b");

	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeC_Brukan_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeC_Brukan_02.prefab:def8d9f6f395e5e4d8a2c34843728a78");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeB_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeB_Dawngrasp_02.prefab:0da619274f0e2684891dd026063c7f79");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeC_Dawngrasp_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeC_Dawngrasp_02.prefab:3d72e40fa089ad6458aeb0ec5d531974");

	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4Victory_02.prefab:19307310b4515794ea4a7f13064263c8");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeA_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeA_02.prefab:aab793abb316d8848bcd997362016b27");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeC_01.prefab:91acd060b11441a44959f9c467a8bd07");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeD_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeD_01.prefab:ad7bbdd8a25df0a41b51163d32d0110b");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Intro_02.prefab:794ab97e567476d46be759acf63b8fd9");

	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Victory_01.prefab:e71541bc846b88f4581504649bca485f");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeB_Rokara_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeB_Rokara_02.prefab:d41c0ca211022c149a9b130f85e0faeb");

	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeC_Rokara_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeC_Rokara_02.prefab:ba0c583da1489e74f85941e05f564420");

	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Death_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Death_01.prefab:1ab153c50c04a3c45b081d3ad484820f");

	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4EmoteResponse_01.prefab:625ff46aa64cd6c4eb559220474b866a");

	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeA_01.prefab:b5b1e59ae67bfac49a6557588b548765");

	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeB_01.prefab:a82e0e7901fe42d4f8ffba8e3d07956c");

	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeD_02 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeD_02.prefab:270f9df0e34e0124cb2ba893b4ce46ad");

	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_01.prefab:a036a322a57967543b9cbcfc9f5028b7");

	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_02 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_02.prefab:f8bcaeb4c3f950c4eb709ff091e5934c");

	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_03 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_03.prefab:50f907e92a6bc9a4db8d62d1623f2a89");

	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Intro_01.prefab:4732afbaef78b8d48a718cdc9160daa0");

	private static readonly AssetReference VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Loss_01 = new AssetReference("VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Loss_01.prefab:b8bf472ed310b254e988e33cdb12ef79");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeB_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeB_Tamsin_02.prefab:ae112cacad1a3974cb02568c72fa0b43");

	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeC_Tamsin_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeC_Tamsin_02.prefab:60d6ffc7d09ef4b47be1342ac2db5fdd");

	private List<string> m_InGame_BossIdle = new List<string> { VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_01, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_02, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeB_Brukan_02, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeC_Brukan_02, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeB_Dawngrasp_02, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeC_Dawngrasp_02, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4Victory_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeA_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeC_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeD_01, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Intro_02, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Victory_01,
			VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeB_Rokara_02, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeC_Rokara_02, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Death_01, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4EmoteResponse_01, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeA_01, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeB_01, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeD_02, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_01, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_02, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Idle_03,
			VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Intro_01, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Loss_01, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeB_Tamsin_02, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeC_Tamsin_02
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
		switch (missionEvent)
		{
		case 514:
			yield return MissionPlayVO(actor, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Intro_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Intro_02);
			break;
		case 515:
			yield return MissionPlayVO(actor, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4EmoteResponse_01);
			break;
		case 516:
			yield return MissionPlaySound(actor, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Death_01);
			break;
		case 506:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(actor, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 505:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4Victory_01);
			yield return MissionPlayVO(Dawngrasp_BrassRing, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4Victory_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 517:
			yield return MissionPlayVO(actor, m_InGame_BossIdle);
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
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeA_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeA_02);
			break;
		case 7:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeB_01);
			if (HeroPowerBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeB_Brukan_02);
			}
			if (HeroPowerDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeB_Dawngrasp_02);
			}
			if (HeroPowerRokara)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeB_Rokara_02);
			}
			if (HeroPowerTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeB_Tamsin_02);
			}
			break;
		case 11:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeC_01);
			if (HeroPowerBrukan)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission4ExchangeC_Brukan_02);
			}
			if (HeroPowerDawngrasp)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Guff_Mission4ExchangeC_Dawngrasp_02);
			}
			if (HeroPowerRokara)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission4ExchangeC_Rokara_02);
			}
			if (HeroPowerTamsin)
			{
				yield return MissionPlayVO(friendlyHeroPowerActor, VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission4ExchangeC_Tamsin_02);
			}
			break;
		case 15:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission4ExchangeD_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Smythe_Male_Human_Story_Guff_Mission4ExchangeD_02);
			break;
		}
	}
}
