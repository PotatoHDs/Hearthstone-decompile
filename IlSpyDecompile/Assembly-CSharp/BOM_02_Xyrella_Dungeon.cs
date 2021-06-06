using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOM_02_Xyrella_Dungeon : BOM_02_Xyrella_MissionEntity
{
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_01.prefab:bdc7cdc8f439b7742b90d090b0a68ac6");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_02.prefab:60004d0fb7385e547a8224910590ae8e");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_03.prefab:5c82e945fe93bc241bd3e0e8e7a24dea");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_04 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_04.prefab:137db0ac137e1904db25fbedef06220a");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_05 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_05.prefab:7396e59074a787842b10d41833e31e0a");

	private List<string> m_Tavish_TriggerLines = new List<string> { VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_03, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_04, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_05 };

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_01.prefab:1987a509960661a49a9e90b9e184bf76");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_02.prefab:740f8883faa6d66498d13c7bc4538e99");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_03.prefab:f9e4dc77e90ef984caecf46604c7e981");

	private List<string> m_Tavish_DeathLines = new List<string> { VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_03 };

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_01.prefab:e72b6cdb10b7559408a53af9972b163e");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_02.prefab:385813dd835f47040963445651d6ecbf");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_03.prefab:99784d26fdf7a1f4bb30610f3adeccc1");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_04 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_04.prefab:74a7a948eb3a8c1458fefffce4ee6625");

	private List<string> m_Tavish_HealLines = new List<string> { VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_03, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_04 };

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_01.prefab:0faf2869c5fe23142bdc18cc709b209d");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_02.prefab:79cdd1ddc7fdb494ca990a33e07ba8f4");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_03.prefab:693af485ff9a431469f253a98277e58d");

	private List<string> m_Tavish_IsDeadLines = new List<string> { VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_03 };

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_01.prefab:a7368390318b5264a96defe9fa21da43");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_02.prefab:6cb1c9d46351b7043ab57b30330fad7e");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_03.prefab:80fb4434ee751714ab5dbfda2c3ac467");

	private List<string> m_Tavish_RezLines = new List<string> { VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_03 };

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_01.prefab:6505ae2ddb7e6c84cad08b7d665d27cc");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_02.prefab:ffaf1d03355a5584fbb58bf5d5fd4ff4");

	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_03.prefab:7ca800208e669934389c7ffc29b8cefc");

	private List<string> m_Tavish_AttackLines = new List<string> { VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_03 };

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_01.prefab:3931eef6f40244e46822ab4c9522297e");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_02.prefab:1192a9d9a7982594e979ccc257172026");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_03.prefab:74264db1dc70a194a9b801750beded5e");

	private List<string> m_Scabbs_RezLines = new List<string> { VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_03 };

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_01.prefab:359d4bbea22e8ef49bb81bb5224dda87");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_02.prefab:c28119a2c173320489d49fba69e7dbb0");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_03.prefab:1a305fda837bbfa4980242b29660e280");

	private List<string> m_Scabbs_DeathLines = new List<string> { VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_03 };

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_01.prefab:ba9ea9cc6632a5e44883797e594f9b66");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_02.prefab:42ba2603fb3d75b499fdcab3041574d8");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_03.prefab:da8b38b924a7a1347894c088d9cb4584");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_04 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_04.prefab:63a368c752c74d343ad61f4ec3b38642");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_05 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_05.prefab:32bb44d8c8f2726449c436effa0b439f");

	private List<string> m_Scabbs_TriggerLines = new List<string> { VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_03, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_04, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_05 };

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_01.prefab:21f7df62e12f70942a57b8fddebb0d11");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_02.prefab:7a0b9cf0bbf9eff4f89751b024585bdb");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_03.prefab:3113cf4b0a979434ba8bdbde4dee786e");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_04 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_04.prefab:4e6ef9170e7b8eb44b31e9d200fcc6a2");

	private List<string> m_Scabbs_HealLines = new List<string> { VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_03, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_04 };

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_01.prefab:62f5d5bc3c07e2843970a4d226583687");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_02.prefab:03a1158d578a0da43bee54ba64997f80");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_03.prefab:41325e4422e2a93478602fcdc42901db");

	private List<string> m_Scabbs_isDeadLines = new List<string> { VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_03 };

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_01.prefab:b13d79013fab8aa42887ea962872dbd6");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_02.prefab:9a7a93b7a3d3d1e42bfe32e1ba7288ca");

	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_03 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_03.prefab:84d0a22dbc608874c8360efb6aea6216");

	private List<string> m_Scabbs_AttackLines = new List<string> { VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_03, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_04 };

	public readonly AssetReference Tavish_BrassRing = new AssetReference("Tavish_BrassRing_Quote.prefab:ad6adae48f4bfba4da53b7138111c1e3");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_01.prefab:53f58e50aac9ccc41a764aff34c50340");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_02.prefab:e5614706798e2cf43ac1fca0e2581af8");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_03.prefab:e1577bcbb62807e45aa6c808714db2e7");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_04 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_04.prefab:97404acfc3770224fb1d352abadce4fa");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_05 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_05.prefab:ba5c601009dc72f4da982fa94abd7e7c");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_06 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_06.prefab:6246f9c7f3340b14f89b396dc3cc05fe");

	private List<string> m_Xyrella_HPHeal = new List<string> { VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_03, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_04, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_05, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_06 };

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_01.prefab:7f83089cafcb0ac4f8d06e61b1e7d50c");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_02.prefab:f726954dc0742054dbb14ab45690b46d");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_03.prefab:cd8028c5856c251449a90814bd67cbdf");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_04 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_04.prefab:11383f944ada27b4d95bf89b246a82cc");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_05 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_05.prefab:54e7707836df865498dd5fc41552d4c4");

	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_06 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_06.prefab:075634794bf6d3e4199382d60ef66067");

	private List<string> m_Xyrella_HPRez = new List<string> { VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_03, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_04, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_05, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_06 };

	public const int Tavish_TriggerLine = 58032;

	public const int Tavish_DeathLines = 58033;

	public const int Tavish_HealLines = 58034;

	public const int Tavish_IsDeadLines = 58035;

	public const int Tavish_RezLines = 58036;

	public const int Tavish_Attack = 58042;

	public const int Scabbs_RezLines = 58037;

	public const int Scabbs_DeathLines = 58038;

	public const int Scabbs_TriggerLines = 58039;

	public const int Scabbs_HealLines = 58040;

	public const int Scabbs_isDeadLines = 58041;

	public const int Scabbs_Attack = 58043;

	public bool m_Scabbs_isDead;

	public bool m_Tavish_isDead;

	public const int XyrellaCustomIdle = 58042;

	public const float m_Xyrella_HP_Speaking_Chance = 0.5f;

	public float m_Xyrella_HP_Seconds_Since_Action;

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = null;
		m_deathLine = null;
		m_standardEmoteResponseLine = null;
		m_BossIdleLines = new List<string>(GetBossIdleLines());
		m_BossIdleLinesCopy = new List<string>(GetBossIdleLines());
		m_OverrideMusicTrack = MusicPlaylistType.Invalid;
		m_OverrideMulliganMusicTrack = MusicPlaylistType.Invalid;
		m_Mission_EnemyHeroShouldExplodeOnDefeat = true;
		m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;
		m_OverrideBossSubtext = null;
		m_OverridePlayerSubtext = null;
		m_SupressEnemyDeathTextBubble = false;
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_03, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_04, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Trigger_05, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Death_03, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_02,
			VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_03, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Heal_04, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_IsDead_03, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_02, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Attack_03, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_01, VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_02,
			VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Rez_03, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Rez_03, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Death_03, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_03,
			VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_04, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Trigger_05, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_03, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Heal_04, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_01, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_IsDead_03, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_01,
			VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_02, VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Attack_03, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_02, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_03, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_04, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_05, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPRez_06, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_01, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_02,
			VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_03, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_04, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_05, VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_HPHeal_06
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public sealed override AdventureDbId GetAdventureID()
	{
		return AdventureDbId.BOM;
	}

	public static BOM_02_Xyrella_Dungeon InstantiateTemplate_SoloDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		Log.All.PrintError("BOM_02_Xyrella_Dungeon.InstantiateTemplate_SoloDungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", opposingHeroCardID);
		return new BOM_02_Xyrella_Dungeon();
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		yield return WaitForEntitySoundToFinish(entity);
		entity.GetCardId();
	}

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		MissionPause(pause: true);
		yield return HandleMissionEventWithTiming(514);
		MissionPause(pause: false);
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(busy: true);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: false);
			yield break;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroPower()
			.GetCard()
			.GetActor();
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		Random.Range(0f, 1f);
		GetTag(GAME_TAG.TURN);
		GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
		float num = Random.Range(0f, 1f);
		switch (missionEvent)
		{
		case 1000:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			if (m_PlayPlayerVOLineIndex + 1 >= m_PlayerVOLines.Count)
			{
				m_PlayPlayerVOLineIndex = 0;
			}
			else
			{
				m_PlayPlayerVOLineIndex++;
			}
			SceneDebugger.Get().AddMessage(m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			yield return PlayBossLine(actor, m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			break;
		case 1001:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			yield return PlayBossLine(actor, m_PlayerVOLines[m_PlayPlayerVOLineIndex]);
			break;
		case 1002:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			if (m_PlayBossVOLineIndex + 1 >= m_BossVOLines.Count)
			{
				m_PlayBossVOLineIndex = 0;
			}
			else
			{
				m_PlayBossVOLineIndex++;
			}
			SceneDebugger.Get().AddMessage(m_BossVOLines[m_PlayBossVOLineIndex]);
			yield return PlayBossLine(enemyActor, m_BossVOLines[m_PlayBossVOLineIndex]);
			break;
		case 1003:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(m_BossVOLines[m_PlayBossVOLineIndex]);
			yield return PlayBossLine(enemyActor, m_BossVOLines[m_PlayBossVOLineIndex]);
			break;
		case 1011:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string bossVOLine in m_BossVOLines)
			{
				SceneDebugger.Get().AddMessage(bossVOLine);
				yield return MissionPlayVO(enemyActor, bossVOLine);
			}
			foreach (string playerVOLine in m_PlayerVOLines)
			{
				SceneDebugger.Get().AddMessage(playerVOLine);
				yield return MissionPlayVO(enemyActor, playerVOLine);
			}
			break;
		case 1012:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string bossVOLine2 in m_BossVOLines)
			{
				SceneDebugger.Get().AddMessage(bossVOLine2);
				yield return MissionPlayVO(enemyActor, bossVOLine2);
			}
			break;
		case 1013:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string playerVOLine2 in m_PlayerVOLines)
			{
				SceneDebugger.Get().AddMessage(playerVOLine2);
				yield return MissionPlayVO(enemyActor, playerVOLine2);
			}
			break;
		case 1010:
			if (m_forceAlwaysPlayLine)
			{
				m_forceAlwaysPlayLine = false;
			}
			else
			{
				m_forceAlwaysPlayLine = true;
			}
			break;
		case 58023:
		{
			SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
			GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
			SceneMgr.Get().SetNextMode(postGameSceneMode);
			break;
		}
		case 600:
			m_Mission_EnemyHeroShouldExplodeOnDefeat = false;
			break;
		case 610:
			m_Mission_EnemyHeroShouldExplodeOnDefeat = true;
			break;
		case 601:
			m_Mission_FriendlyHeroShouldExplodeOnDefeat = false;
			break;
		case 611:
			m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;
			break;
		case 603:
			m_MissionDisableAutomaticVO = false;
			break;
		case 602:
			m_MissionDisableAutomaticVO = true;
			break;
		case 612:
			m_DoEmoteDrivenStart = true;
			break;
		case 516:
			if (m_SupressEnemyDeathTextBubble)
			{
				yield return MissionPlaySound(enemyActor, m_deathLine);
			}
			else
			{
				yield return MissionPlayVO(enemyActor, m_deathLine);
			}
			break;
		case 58032:
			yield return MissionPlayVO("BOM_02_Tavish_01t", m_Tavish_TriggerLines);
			break;
		case 58033:
			m_Tavish_isDead = true;
			yield return MissionPlaySound("BOM_02_Tavish_01t", m_Tavish_DeathLines);
			break;
		case 58034:
			if (shouldPlayBanterVO())
			{
				if (num > 0.5f)
				{
					yield return MissionPlayVO(actor, m_Xyrella_HPHeal);
				}
				else
				{
					yield return MissionPlayVO("BOM_02_Tavish_01t", m_Tavish_HealLines);
				}
			}
			break;
		case 58035:
			m_Tavish_isDead = true;
			yield return MissionPlayVO("BOM_02_Tavish_01t", m_Tavish_IsDeadLines);
			break;
		case 58042:
			if (shouldPlayBanterVO())
			{
				yield return MissionPlaySound("BOM_02_Tavish_01t", m_Tavish_AttackLines);
			}
			break;
		case 58036:
			m_Tavish_isDead = false;
			if (shouldPlayBanterVO())
			{
				if (num > 0.5f)
				{
					yield return MissionPlayVO(actor, m_Xyrella_HPRez);
				}
				else
				{
					yield return MissionPlayVO("BOM_02_Tavish_01t", m_Tavish_RezLines);
				}
			}
			break;
		case 58037:
			m_Scabbs_isDead = false;
			if (shouldPlayBanterVO())
			{
				if (num > 0.5f)
				{
					yield return MissionPlayVO(actor, m_Xyrella_HPRez);
				}
				else
				{
					yield return MissionPlayVO("BOM_02_Scabbs_06t", m_Scabbs_RezLines);
				}
			}
			break;
		case 58038:
			m_Tavish_isDead = true;
			yield return MissionPlaySound("BOM_02_Scabbs_06t", m_Scabbs_DeathLines);
			break;
		case 58039:
			yield return MissionPlayVO("BOM_02_Scabbs_06t", m_Scabbs_TriggerLines);
			break;
		case 58040:
			if (shouldPlayBanterVO())
			{
				if (num > 0.5f)
				{
					yield return MissionPlayVO(actor, m_Xyrella_HPHeal);
				}
				else
				{
					yield return MissionPlayVO("BOM_02_Scabbs_06t", m_Scabbs_HealLines);
				}
			}
			break;
		case 58041:
			yield return MissionPlayVO("BOM_02_Scabbs_06t", m_Scabbs_isDeadLines);
			break;
		case 58043:
			if (shouldPlayBanterVO())
			{
				yield return MissionPlaySound("BOM_02_Scabbs_06t", m_Scabbs_AttackLines);
			}
			break;
		case 518:
			if (shouldPlayBanterVO())
			{
				if (m_Scabbs_isDead && m_Tavish_isDead)
				{
					yield return MissionPlayVO("BOM_02_Scabbs_06t", m_Scabbs_isDeadLines);
					yield return MissionPlayVO("BOM_02_Tavish_01t", m_Tavish_IsDeadLines);
				}
				else if (m_Scabbs_isDead)
				{
					yield return MissionPlayVO("BOM_02_Scabbs_06t", m_Scabbs_isDeadLines);
				}
				else if (m_Tavish_isDead)
				{
					yield return MissionPlayVO("BOM_02_Tavish_01t", m_Tavish_IsDeadLines);
				}
				else
				{
					yield return MissionPlayThinkEmote(actor);
				}
			}
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}
}
