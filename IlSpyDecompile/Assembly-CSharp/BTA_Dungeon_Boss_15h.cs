using System.Collections;
using System.Collections.Generic;

public class BTA_Dungeon_Boss_15h : BTA_Dungeon
{
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Death_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Death_01.prefab:e5c8b619095374542bac028ed3654007");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_DefeatPlayer_02 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_DefeatPlayer_02.prefab:8ae8f6b7e7ae0a2479e671cc7129bd7a");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_EmoteResponse_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_EmoteResponse_01.prefab:777b3055ee92eae42b1b27826e242da7");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_EventBoombox_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_EventBoombox_01.prefab:d031062f6c4754b42a8a0975a1b221a1");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Intro_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Intro_01.prefab:c513358ed8344364dbf85ee7bb4f8eca");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_KillingBlowReady_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_KillingBlowReady_01.prefab:df8c2af5e5f7f4d4584ff6c6a87dfea6");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song1A_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song1A_01.prefab:9c6e2abda71d0ec49b5074792dfbf771");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song1B_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song1B_01.prefab:b8319bccf79c8b74d92c485cb6dd786c");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song1C_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song1C_01.prefab:5f1305268addceb4c9a673711114f60d");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song1D_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song1D_01.prefab:b541bc13aa2323348bfcd150bbd16fab");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song2A_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song2A_01.prefab:6d84e27d81f910c40bbd4e5ba577a581");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song2B_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song2B_01.prefab:8c675c5b762b25846be298354893ceca");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song2C_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song2C_01.prefab:633c3a18d8e1b3849a627c6058f5d4e3");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song2D_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song2D_01.prefab:df121bc515d9fe940853a5fc6de0aa80");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song3A_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song3A_01.prefab:7bd1910702d44ed4f8914cab4375f0d5");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song3B_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song3B_01.prefab:db88baed4964457498d39a08d7e852a9");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song3C_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song3C_01.prefab:b67a33acc2bab62468194ccd6700dffb");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song3D_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song3D_01.prefab:f0d2f6830d3ef74419a6fddfdb383faf");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_01.prefab:56af47117277b9e429bf3c7e59633d18");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_02 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_02.prefab:c824cad0fcdc132409358f46526f2610");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_03 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_03.prefab:b81beae0c979cdf4c8fcc651c5dd9dd3");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_StealDoomsayer_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_StealDoomsayer_01.prefab:44f418a3c657052439d81529f24d64ed");

	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_StealMinstrel_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_StealMinstrel_01.prefab:8cfb6f7ad88bd1443b6047f37bd70954");

	private static readonly AssetReference RussellTheBard_Short_Strum_Underlay = new AssetReference("RussellTheBard_Short_Strum_Underlay.prefab:09e52281d7049da458f12cb79ee395da");

	private static readonly AssetReference RussellTheBard_Song_Underlay_part1 = new AssetReference("RussellTheBard_Song_Underlay_part1.prefab:ea1958f63cb2e40469adc2c5803c061e");

	private static readonly AssetReference RussellTheBard_Song_Underlay_part2 = new AssetReference("RussellTheBard_Song_Underlay_part2.prefab:f3dd880fa456f6e4aa48b7af55ddd2c6");

	private static readonly AssetReference RussellTheBard_Song3A_01_Underlay = new AssetReference("RussellTheBard_Song3A_01_Underlay.prefab:ce68b6200ed0a7543acb8776ee1a78d7");

	private static readonly AssetReference RussellTheBard_Song3B_01_Underlay = new AssetReference("RussellTheBard_Song3B_01_Underlay.prefab:eb9516af5cca1cf4c95f3455ef3a7fc1");

	private static readonly AssetReference RussellTheBard_Song3C_01_Underlay = new AssetReference("RussellTheBard_Song3C_01_Underlay.prefab:a69e44df16d21b548becbed9fe9cfb03");

	private static readonly AssetReference RussellTheBard_Song3D_01_Underlay = new AssetReference("RussellTheBard_Song3D_01_Underlay.prefab:08d078248c7b20647aa0cbf0b2bf79e4");

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_LOOTA_BOSS_51h_Male_Dwarf_Death_01, VO_LOOTA_BOSS_51h_Male_Dwarf_DefeatPlayer_02, VO_LOOTA_BOSS_51h_Male_Dwarf_EmoteResponse_01, VO_LOOTA_BOSS_51h_Male_Dwarf_EventBoombox_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Intro_01, VO_LOOTA_BOSS_51h_Male_Dwarf_KillingBlowReady_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song1A_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song1B_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song1C_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song1D_01,
			VO_LOOTA_BOSS_51h_Male_Dwarf_Song2A_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song2B_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song2C_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song2D_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song3A_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song3B_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song3C_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Song3D_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_01, VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_02,
			VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_03, VO_LOOTA_BOSS_51h_Male_Dwarf_StealDoomsayer_01, VO_LOOTA_BOSS_51h_Male_Dwarf_StealMinstrel_01, RussellTheBard_Short_Strum_Underlay, RussellTheBard_Song_Underlay_part1, RussellTheBard_Song_Underlay_part2, RussellTheBard_Song3A_01_Underlay, RussellTheBard_Song3B_01_Underlay, RussellTheBard_Song3C_01_Underlay, RussellTheBard_Song3D_01_Underlay
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineWithUnderlay(enemyActor, VO_LOOTA_BOSS_51h_Male_Dwarf_Song3A_01, RussellTheBard_Song3A_01_Underlay);
			yield return PlayLineWithUnderlay(enemyActor, VO_LOOTA_BOSS_51h_Male_Dwarf_Song3B_01, RussellTheBard_Song3B_01_Underlay);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineWithUnderlay(playerActor, VO_LOOTA_BOSS_51h_Male_Dwarf_Song3C_01, RussellTheBard_Song3C_01_Underlay);
			yield return PlayLineWithUnderlay(playerActor, VO_LOOTA_BOSS_51h_Male_Dwarf_Song3D_01, RussellTheBard_Song3D_01_Underlay);
			GameState.Get().SetBusy(busy: false);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}
}
