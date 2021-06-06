using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRL_MissionEntity : GenericDungeonMissionEntity
{
	public enum TRL_VOPoolType
	{
		INVALID = 0,
		KILL_SHRINE_GENERIC = 100,
		KILL_SHRINE_DRUID = 102,
		KILL_SHRINE_HUNTER = 103,
		KILL_SHRINE_MAGE = 104,
		KILL_SHRINE_PALADIN = 105,
		KILL_SHRINE_PRIEST = 106,
		KILL_SHRINE_ROGUE = 107,
		KILL_SHRINE_SHAMAN = 108,
		KILL_SHRINE_WARLOCK = 109,
		KILL_SHRINE_WARRIOR = 110,
		SHRINE_KILLED = 200,
		SHRINE_RETURNS = 300,
		SHRINE_FLAVOR_DRUID = 402,
		SHRINE_FLAVOR_HUNTER = 403,
		SHRINE_FLAVOR_MAGE = 404,
		SHRINE_FLAVOR_PALADIN = 405,
		SHRINE_FLAVOR_PRIEST = 406,
		SHRINE_FLAVOR_ROGUE = 407,
		SHRINE_FLAVOR_SHAMAN = 408,
		SHRINE_FLAVOR_WARLOCK = 409,
		SHRINE_FLAVOR_WARRIOR = 410,
		NEARING_WIN = 500,
		NEARING_LOSS = 600,
		TUTORIAL_SHRINE_1 = 700,
		TUTORIAL_SHRINE_2 = 701,
		TUTORIAL_ENEMY_SHRINE_DIES = 702,
		TUTORIAL_ENEMY_SHRINE_REVIVES = 703,
		TUTORIAL_PLAYER_SHRINE_DIES = 704,
		TUTORIAL_PLAYER_SHRINE_TIMER_TICK = 705,
		TUTORIAL_PLAYER_SHRINE_LOST = 706,
		TUTORIAL_PLAYER_SHRINE_TRANSFORMED = 707,
		TUTORIAL_PLAYER_SHRINE_BOUNCED = 708,
		TUTORIAL_PLAYER_FIRST_LOST = 709
	}

	private static readonly string Rastakhan_BrassRing_Quote = "Rastakhan_BrassRing_Quote.prefab:179bfad1464576448aeabfe5c3eff601";

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_Game1Begin_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_Game1Begin_01.prefab:afd9eaf8855bf874a826f364c38da31b");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_Game2Begin_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_Game2Begin_01.prefab:7bde3c2ed1d9a0b4798fd276e266afff");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_Defeat_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_Defeat_01.prefab:29800eb6bdd34bc4b9cdde83840bf51e");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_EnemyShrineDead_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_EnemyShrineDead_01.prefab:69cb28e5779dc554b84be3fa4e5a880e");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_EnemyShrineReturn_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_EnemyShrineReturn_01.prefab:ccecbea6798e0584fba482201ed9032c");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDead_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDead_01.prefab:cf30dbf19d496fd4d86ef16c68ba3e96");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDisabled_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDisabled_01.prefab:be54488096969d241a24ce2331c25843");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_ShrineDestroy_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_ShrineDestroy_01.prefab:992f9169c14340649bd658c32fa1d266");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_ShrineTransform_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_ShrineTransform_01.prefab:b36edf0591d4c554f8a37ab1eb33a8cd");

	private static readonly AssetReference VO_HERO_02b_Male_Troll_Troll_ShrineBounce_01 = new AssetReference("VO_HERO_02b_Male_Troll_Troll_ShrineBounce_01.prefab:f2213b57ae9a85642bf2a912602b3892");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Returns_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Returns_01.prefab:5b53352bae2cbee4e877af95624c6e23");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Returns_02 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Returns_02.prefab:f3f1211782e014d44be2266b48fe841a");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Returns_03 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Returns_03.prefab:d73d5740c7ae19d47af77a61ee4413cd");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Returns_04 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Returns_04.prefab:41039197a4433464c8d06ca949846885");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Returns_05 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Returns_05.prefab:8cf9bae702a69f84892ee05e1a929ddc");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Generic_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Generic_01.prefab:7b09cf3c50ff5ac4198d93a4d2de9fee");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Druid_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Druid_01.prefab:12dfbd67dd429fd4b856063118fe3f36");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Hunter_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Hunter_01.prefab:0b7d821dd44e7384f88412f506cee1a3");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Mage_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Mage_01.prefab:ebcc4b264896f7c4db4b844304302a50");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Paladin_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Paladin_01.prefab:6a2513f3d19f7bc42bdcb0b6699ac829");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Priest_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Priest_01.prefab:06f5ea33962b9d542a73dd7d4663e3a3");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Rogue_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Rogue_01.prefab:1b81524dcbb16494ea2f3564ac4354f7");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Shaman_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Shaman_01.prefab:8aa3a7365444fc345bf8f5e06af2c347");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Warlock_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Warlock_01.prefab:7d1f3535a0e597946bbd822ac13c3729");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Kill_Shrine_Warrior_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Kill_Shrine_Warrior_01.prefab:59e8110726205e749b266739ecb4209c");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Druid_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Druid_01.prefab:9e50e9b615d01b1409af733af9d1faf4");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Hunter_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Hunter_01.prefab:813de6f1b0767194b81009af23b277cd");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Mage_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Mage_01.prefab:0203c3c020550aa4f876e8d72a872786");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Paladin_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Paladin_01.prefab:52a41492e9ad12e48b5083345d1f4437");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Priest_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Priest_01.prefab:ac45a45b89023bb4e812f815dc59effb");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Rogue_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Rogue_01.prefab:525db2978fc2cb9408f5d6dee20e9b8a");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Shaman_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Shaman_01.prefab:61c3325aeb1e83a4686d9a91c407e2ba");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warlock_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warlock_01.prefab:5feb37f49c1eda44b817db7a4355a7ae");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warrior_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warrior_01.prefab:7f1010243156aca419753f8ca6cbf13f");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_01.prefab:f2b32d78a5bcf6448a5c43f5d944579b");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_02 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_02.prefab:72b5f01d510ff4d488c6179be0af9219");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_03 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_03.prefab:580a820958714974dad73b2fc29b310e");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_04 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_04.prefab:bfa4b0fbdea57214689961cda0257de3");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_05 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_05.prefab:93ecacd6d46db714b94b6b5de38c233e");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_01 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_01.prefab:7b02ae02563c9ee40b7d39ed2fbe17e9");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_02 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_02.prefab:f3619b48393189240a8c857a6a9f4ee7");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_03 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_03.prefab:399382b0101a43143bec43b136879a83");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_04 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_04.prefab:77f7baee64e324844b1b9306ed1a42ac");

	private static readonly AssetReference VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_05 = new AssetReference("VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_05.prefab:26bed8961b6b7344ca6208211cb2d1bb");

	public override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.TRL;
	}

	public override void PreloadAssets()
	{
		VOPool value = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Kill_Shrine_Generic_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(100, value);
		VOPool value2 = new VOPool(new List<string> { VO_HERO_02b_Male_Troll_Troll_Game1Begin_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_SHRINE_TUTORIAL_1_VO);
		m_VOPools.Add(700, value2);
		VOPool value3 = new VOPool(new List<string> { VO_HERO_02b_Male_Troll_Troll_Game2Begin_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_SHRINE_TUTORIAL_2_VO);
		m_VOPools.Add(701, value3);
		VOPool value4 = new VOPool(new List<string> { VO_HERO_02b_Male_Troll_Troll_Defeat_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.DUNGEON_CRAWL_HAS_SEEN_IN_GAME_LOSE_VO);
		m_VOPools.Add(709, value4);
		VOPool value5 = new VOPool(new List<string> { VO_HERO_02b_Male_Troll_Troll_EnemyShrineDead_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_ENEMY_SHRINE_DIES_TUTORIAL_VO);
		m_VOPools.Add(702, value5);
		VOPool value6 = new VOPool(new List<string> { VO_HERO_02b_Male_Troll_Troll_EnemyShrineReturn_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_ENEMY_SHRINE_REVIVES_TUTORIAL_VO);
		m_VOPools.Add(703, value6);
		VOPool value7 = new VOPool(new List<string> { VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDead_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_DIES_TUTORIAL_VO);
		m_VOPools.Add(704, value7);
		VOPool value8 = new VOPool(new List<string> { VO_HERO_02b_Male_Troll_Troll_FriendlyShrineDisabled_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_TIMER_TICK_TUTORIAL_VO);
		m_VOPools.Add(705, value8);
		VOPool value9 = new VOPool(new List<string> { VO_HERO_02b_Male_Troll_Troll_ShrineDestroy_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_LOST_TUTORIAL_VO);
		m_VOPools.Add(706, value9);
		VOPool value10 = new VOPool(new List<string> { VO_HERO_02b_Male_Troll_Troll_ShrineTransform_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_TRANSFORMED_TUTORIAL_VO);
		m_VOPools.Add(707, value10);
		VOPool value11 = new VOPool(new List<string> { VO_HERO_02b_Male_Troll_Troll_ShrineBounce_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.INVALID, Rastakhan_BrassRing_Quote, GameSaveKeySubkeyId.TRL_DUNGEON_HAS_SEEN_PLAYER_SHRINE_BOUNCED_TUTORIAL_VO);
		m_VOPools.Add(708, value11);
		VOPool value12 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Returns_01, VO_TRLA_209h_Male_Troll_Shrine_Returns_02, VO_TRLA_209h_Male_Troll_Shrine_Returns_03, VO_TRLA_209h_Male_Troll_Shrine_Returns_04, VO_TRLA_209h_Male_Troll_Shrine_Returns_05 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(300, value12);
		VOPool value13 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_01, VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_02, VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_03, VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_04, VO_TRLA_209h_Male_Troll_Trigger_Nearing_Win_05 }, 1f, ShouldPlayValue.Always, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(500, value13);
		VOPool value14 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_01, VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_02, VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_03, VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_04, VO_TRLA_209h_Male_Troll_Trigger_Nearing_Loss_05 }, 1f, ShouldPlayValue.Always, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(600, value14);
		VOPool value15 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Kill_Shrine_Druid_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(102, value15);
		VOPool value16 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Kill_Shrine_Hunter_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(103, value16);
		VOPool value17 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Kill_Shrine_Mage_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(104, value17);
		VOPool value18 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Kill_Shrine_Paladin_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(105, value18);
		VOPool value19 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Kill_Shrine_Priest_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(106, value19);
		VOPool value20 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Kill_Shrine_Rogue_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(107, value20);
		VOPool value21 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Kill_Shrine_Shaman_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(108, value21);
		VOPool value22 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Kill_Shrine_Warlock_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(109, value22);
		VOPool value23 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Kill_Shrine_Warrior_01 }, 1f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(110, value23);
		VOPool value24 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Flavor_Druid_01 }, 0.2f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(402, value24);
		VOPool value25 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Flavor_Hunter_01 }, 0.2f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(403, value25);
		VOPool value26 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Flavor_Mage_01 }, 0.2f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(404, value26);
		VOPool value27 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Flavor_Paladin_01 }, 0.2f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(405, value27);
		VOPool value28 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Flavor_Priest_01 }, 0.2f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(406, value28);
		VOPool value29 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Flavor_Rogue_01 }, 0.2f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(407, value29);
		VOPool value30 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Flavor_Shaman_01 }, 0.2f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(408, value30);
		VOPool value31 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warlock_01 }, 0.2f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(409, value31);
		VOPool value32 = new VOPool(new List<string> { VO_TRLA_209h_Male_Troll_Shrine_Flavor_Warrior_01 }, 0.2f, ShouldPlayValue.Once, VOSpeaker.FRIENDLY_HERO);
		m_VOPools.Add(410, value32);
		base.PreloadAssets();
	}

	protected override bool CanPlayVOLines(Entity speakerEntity, VOSpeaker speaker)
	{
		if (speaker == VOSpeaker.FRIENDLY_HERO)
		{
			return speakerEntity.GetCardId().Contains("TRLA_209h_");
		}
		return base.CanPlayVOLines(speakerEntity, speaker);
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(HandleMissionEventWithTiming(709));
		}
	}

	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_TRLMulligan);
		}
	}

	public override void StartGameplaySoundtracks()
	{
		bool flag = GameUtils.GetDefeatedBossCount() == 7;
		MusicManager.Get().StartPlaylist(flag ? MusicPlaylistType.InGame_TRLFinalBoss : MusicPlaylistType.InGame_TRLAdventure);
	}

	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}
}
