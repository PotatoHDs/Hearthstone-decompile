using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULDA_Dungeon : ULDA_MissionEntity
{
	public List<string> m_BossVOLines = new List<string>();

	public List<string> m_PlayerVOLines = new List<string>();

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_TRIGGER_KillCommand_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_TRIGGER_KillCommand_01.prefab:dfe38ab5192b0bc42ba82da57ddef202");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_TRIGGER_SecretTrap_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_TRIGGER_SecretTrap_01.prefab:e8ef318fa0d43194f9dee431fee04f0f");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_TRIGGER_Whirlwind_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_TRIGGER_Whirlwind_01.prefab:b2976616ff1f27c44bf888deaca36dae");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TRIGGER_Innervate_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TRIGGER_Innervate_01.prefab:fb7a1039d7060364a8011adfa66263b5");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TRIGGER_Silence_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TRIGGER_Silence_01.prefab:197e2dd7a0921a44e892821dbb5879ec");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TRIGGER_SpreadingPlague_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TRIGGER_SpreadingPlague_01.prefab:5bdb3d0e3ade5de46ac595af579aaea5");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Trigger_Bloodlust_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Trigger_Bloodlust_01.prefab:5a56fe6de89b97e4aa89fee00c44ba77");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Trigger_Consecration_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Trigger_Consecration_01.prefab:0efe5eb06e9e9564c9db10797e5b62ce");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Trigger_Tip_the_Scales_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Trigger_Tip_the_Scales_01.prefab:de7dbdee4df8d4a40ac6ff1b660e2647");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_TRIGGER_Burgle_01 = new AssetReference("VO_ULDA_Reno_Male_Human_TRIGGER_Burgle_01.prefab:f40952ae237059f42bd284e2b3971148");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_TRIGGER_Fireball_01 = new AssetReference("VO_ULDA_Reno_Male_Human_TRIGGER_Fireball_01.prefab:20e37fa1f3277e549844bf23d30ad9ab");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_TRIGGER_Frostbolt_01 = new AssetReference("VO_ULDA_Reno_Male_Human_TRIGGER_Frostbolt_01.prefab:3ea42375f2b0a864d841c5dc133f408f");

	private static readonly AssetReference Tombs_of_Terror_Brann_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Brann_BrassRing_Quote.prefab:d521a1fe41518e24da6e4252b97fbeb7");

	private static readonly AssetReference Tombs_of_Terror_Elise_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Elise_BrassRing_Quote.prefab:6e9e8faaae29f834183122d7ea9be68d");

	private static readonly AssetReference Tombs_of_Terror_Finley_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Finley_BrassRing_Quote.prefab:547ebc970764ec64da6eb3de26ed4698");

	private static readonly AssetReference Tombs_of_Terror_Reno_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Reno_BrassRing_Quote.prefab:4c0b79d4f597c464baabf02e06cf8ae7");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_TUT_PlagueLord_Turn1_Brann_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_TUT_PlagueLord_Turn1_Brann_01.prefab:be40b9d9a54167e4a8b14692082d149b");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_Elise_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_Elise_01.prefab:8df3a42627590d74fa640975d7b7c9f6");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01.prefab:e6e2aa39fa139a841bb10f25d83ece17");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswerSelf_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswerSelf_01.prefab:409907c1b05359d4599957509fac0fe4");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_TUT_PlagueLord_Turn1_Finley_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_TUT_PlagueLord_Turn1_Finley_01.prefab:46fe80b948666044a9845cba1fca30da");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_TUT_PlagueLord_Turn1_Reno_01 = new AssetReference("VO_ULDA_Reno_Male_Human_TUT_PlagueLord_Turn1_Reno_01.prefab:30dfc1d3b64b77b43837aa231d97dd94");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter01a_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter01a_01.prefab:fa9132b4bd9a3a7419f2380e3d5cef7b");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter02a_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter02a_01.prefab:76c6b859ccc70c04e91ea67e5a30cf77");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter03a_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter03a_01.prefab:95f7ea61b24ecba4aa95ea2096aa80c9");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter04a_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter04a_01.prefab:4f37870210d71294492b4879e9931ace");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Elise_Chapter02b_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Elise_Chapter02b_01.prefab:44d19f9cad1c80649aa357af93284521");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter01b_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter01b_01.prefab:0ea5b0bb11d226442a2b7a0b64e4f1e1");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter03b_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter03b_01.prefab:24a814f2c65fa4d45b74c9997c0e8e98");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter04b_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter04b_01.prefab:4aedaf500c08a914db2f7718bb74935f");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Reno_Chapter01b_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Reno_Chapter01b_01.prefab:5b76058cf8d0a8846bf5ad6e979c26ad");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter01b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter01b_01.prefab:cd3dc1d586e5a1649ac352ff90c2a51d");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter02b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter02b_01.prefab:43e59afee8041c040b6d35da2195a45f");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter03b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter03b_01.prefab:e19bdd01cd1563d4baee974c91ae46d4");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter04b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter04b_01.prefab:13bb809e3da1eab44992a3b2643516d0");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter01a_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter01a_01.prefab:f257662ac9702374480737b02be9f57c");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter02a_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter02a_01.prefab:aee22a0a56434914c8884fb40af8958d");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter03a_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter03a_01.prefab:ba0b33d05ad749f48ad9434f9be304a4");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter04a_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter04a_01.prefab:b970b0a4ba3de294bb224fa1ac9f7260");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter02b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter02b_01.prefab:ecfbedb24716fb64da568a9b1eb47a61");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter04b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter04b_01.prefab:1899f68b7290e844998146fd3137af4e");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Elise_Chapter01b_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Elise_Chapter01b_01.prefab:6d2339aa3bea44146a07baa125a5e52b");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter01a_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter01a_01.prefab:8354ef70b8c286c4eb698359a1101023");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter02a_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter02a_01.prefab:0f77c06418fd9d14d818d174c73cd22c");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter03a_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter03a_01.prefab:bed3404ac2b9a9649b3afd825d61e889");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter04a_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter04a_01.prefab:189f9c91163d3ad48ab209d4233bfcea");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Reno_Chapter03b_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Reno_Chapter03b_01.prefab:a200d0b5bedab944b89fc3a15da18ea8");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter03b_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter03b_01.prefab:2845b25610ea74e429bbdd63cf218502");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter04b_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter04b_01.prefab:1443f0fdff63c4b469674b8f386cc26d");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Finley_Chapter02b_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Finley_Chapter02b_01.prefab:2259676fd99450f40840896733c18f58");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter01a_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter01a_01.prefab:44658c8766afae948b1359b8e575c77c");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter02a_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter02a_01.prefab:7241a38908c67be4cbb40f4556b6930b");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter03a_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter03a_01.prefab:0b9bc24d9adb2ba4a9a07b5bccbf0e30");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter04a_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter04a_01.prefab:9e8eba9e6e1f0f84988ea7bf0512b63f");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_01.prefab:477bfc3803adf1f4a8ce078927c3b47f");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_02.prefab:2105f5df08fc1254ba2fa09d2401557d");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_03.prefab:8fc03b74cd5e0104f821b177db07b5f2");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_01.prefab:491255b818cd83f4f997063f51c2eeac");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_02.prefab:d7d44a8920958874993b0e56487e9aaa");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_03.prefab:042f9732b05fa424da7f9d49c44815ae");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_01.prefab:e765c5abe81d1344a99f7111f3d41bd5");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_02.prefab:831b7acaf1433954084f3d632acbcbdd");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_03.prefab:566a70824da8fd546aa3ad0e9d1debae");

	private static readonly AssetReference Tombs_of_Terror_Vesh_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Vesh_BrassRing_Quote.prefab:99b4c68f3a6639743a2cbee4fb8d9e5b");

	private static readonly AssetReference Tombs_of_Terror_Icarax_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Icarax_BrassRing_Quote.prefab:f419bcdf120279a489a80794299832fb");

	private static readonly AssetReference Tombs_of_Terror_Kzrath_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Kzrath_BrassRing_Quote.prefab:66a43505bb8f47842bca007b4af3f7c7");

	private static readonly AssetReference Tombs_of_Terror_Xatma_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Xatma_BrassRing_Quote.prefab:641db52561e62fd49ab686d185e612c0");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_01.prefab:eb8b0170132143a4c9a78dc28c44a393");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_02.prefab:2aa8c6fddbe8e214db3913fb31574c2d");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_03.prefab:6eee41e7b3ff0e740b90304c2567b8d1");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureGatlingWand_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureGatlingWand_01.prefab:fa0f62a6dfa1fa945983bc327970c8d3");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureHat_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureHat_01.prefab:9aee9ea3bbc6ddb40bd09505440db1cb");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureJrExplorer_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureJrExplorer_01.prefab:8a0f42442e839c3469acd165e33435a2");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureLei_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureLei_01.prefab:6193d524c96eecf4384e47820a578c49");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureTitanRing_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureTitanRing_01.prefab:61f1fc26e68d7714c862fd481901f06d");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureWhip_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureWhip_01.prefab:1ca1a1e5a82c9404aa463eec9ef4337d");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseJrExplorer_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseJrExplorer_01.prefab:65d7b0a4b96431344858388063aab52f");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_01.prefab:da4f1971d0d7b654482b8140b3b209c8");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_02 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_02.prefab:bbffe4b428c1f404599f7f5ce2b9e0af");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseTreasureDruidStaff_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseTreasureDruidStaff_01.prefab:648cc33404dee9b4cbce8260921d59af");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseTreasureMachete_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseTreasureMachete_01.prefab:092a88caacb83a343a802433e5b21ce5");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseTreasureScarabStatue_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseTreasureScarabStatue_01.prefab:72e42a66ca0282f4a876cd5f935d481f");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannTreasureBlunderbuss_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannTreasureBlunderbuss_01.prefab:71441326544005944b593f2eaf77f740");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannTreasureFlo_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannTreasureFlo_01.prefab:f3946ff37880d694fb8a502607b15233");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannTreasureGiantEgg_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannTreasureGiantEgg_01.prefab:e2653cce6e405164f86786cb239245c0");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannTreasureJrExplorer_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannTreasureJrExplorer_01.prefab:a509641bdb84f6d46bd71d1fa0de452d");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannTreasureSaddle_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannTreasureSaddle_01.prefab:d11077cbb62b2494ba5ac379acd30669");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasureJrExplorer_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasureJrExplorer_01.prefab:39ad7ec65c173aa42a738a57a1c7ce17");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasureKarl_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasureKarl_01.prefab:6b1ff31d23eda8c458ca8e8554e1f6ba");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasureLance_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasureLance_01.prefab:9765963f754239f49baba405b0170338");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasureMurkyHorn_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasureMurkyHorn_01.prefab:04416db16fc68524d9c14a3b31973470");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasurePandarenTeaSet_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasurePandarenTeaSet_01.prefab:df3c626ff5d3fb8439c3776c08f834d1");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasureScarabMount_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasureScarabMount_01.prefab:acdf120f1aa827444900bfa24ebf9b86");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01.prefab:bbb71f2dfac1c474da3209a15215f4ea");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01.prefab:ad01bc4d23eab3e4f86c994d722cf247");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01.prefab:688849bb5a4d2fd48a817159d1a224fa");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01.prefab:001c634fac4ab874eb6e16d050554b1f");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01.prefab:a5d093ea8ec90d64fb53834b44395720");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01.prefab:97a9c24cb50609347964387b02a62b3c");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01.prefab:feb7534214a13214bac9e2b8726a8cc8");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01.prefab:3fd9e692fe70e924fa0fb5bfabcf17bd");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01.prefab:701ed0ac22ab9a84daade7ed23403317");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01.prefab:f2eeac3ed0b6f554dbfbc9deea60739f");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01.prefab:f9054e8df8774e44d869a0f96ac07efa");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01.prefab:d0a3f9b5c01e04d458178ca8c5069d66");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01.prefab:ab486ac19b475c74f84999cc9a80b7a6");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01.prefab:8789714bb9a92d143bb2024188b8ddd0");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01.prefab:7fd61e2a38015f240bef703ee9f66e5c");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01.prefab:9273a8457f705514f9755153f0c7abf6");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01.prefab:cad9725371b04e943b07f43ecac56b32");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01.prefab:ce7a5a15de006d041ad515427fc6f72f");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01.prefab:92c7854b16b6919499ff3fe7e1e2a422");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01.prefab:3edd1b61bd705b6439dd75542dd6b442");

	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01.prefab:b9fae030ab3026a4bb17f592028c276d");

	private static readonly AssetReference Wisdomball_Pop_up_BrassRing_Quote = new AssetReference("Wisdomball_Pop-up_BrassRing_Quote.prefab:896ee20514caff74db639aa7055838f6");

	private List<string> m_BossDeathPlotTwistTauntLines = new List<string> { VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_02, VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_03 };

	private List<string> m_BossMadnessPlotTwistTauntLines = new List<string> { VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_02, VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_03 };

	private List<string> m_BossMurlocPlotTwistTauntLines = new List<string> { VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_02, VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_03 };

	private List<string> m_BossWrathPlotTwistTauntLines = new List<string> { VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_01, VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_02, VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_03 };

	private List<string> m_EliseTreasureAddarah = new List<string> { VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_01, VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_02 };

	public string m_introLine;

	public string m_deathLine;

	public string m_standardEmoteResponseLine;

	public List<string> m_BossIdleLines;

	public List<string> m_BossIdleLinesCopy;

	private int m_PlayPlayerVOLineIndex;

	private int m_PlayBossVOLineIndex;

	public int TurnOfPlotTwistLastPlayed;

	public static ULDA_Dungeon InstantiateULDADungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		switch (opposingHeroCardID)
		{
		case "ULDA_BOSS_01h":
			return new ULDA_Dungeon_Boss_01h();
		case "ULDA_BOSS_02h":
			return new ULDA_Dungeon_Boss_02h();
		case "ULDA_BOSS_03h":
			return new ULDA_Dungeon_Boss_03h();
		case "ULDA_BOSS_04h":
			return new ULDA_Dungeon_Boss_04h();
		case "ULDA_BOSS_05h":
			return new ULDA_Dungeon_Boss_05h();
		case "ULDA_BOSS_06h":
			return new ULDA_Dungeon_Boss_06h();
		case "ULDA_BOSS_07h":
			return new ULDA_Dungeon_Boss_07h();
		case "ULDA_BOSS_08h":
			return new ULDA_Dungeon_Boss_08h();
		case "ULDA_BOSS_09h":
			return new ULDA_Dungeon_Boss_09h();
		case "ULDA_BOSS_10h":
			return new ULDA_Dungeon_Boss_10h();
		case "ULDA_BOSS_11h":
			return new ULDA_Dungeon_Boss_11h();
		case "ULDA_BOSS_12h":
			return new ULDA_Dungeon_Boss_12h();
		case "ULDA_BOSS_13h":
			return new ULDA_Dungeon_Boss_13h();
		case "ULDA_BOSS_14h":
			return new ULDA_Dungeon_Boss_14h();
		case "ULDA_BOSS_15h":
			return new ULDA_Dungeon_Boss_15h();
		case "ULDA_BOSS_16h":
			return new ULDA_Dungeon_Boss_16h();
		case "ULDA_BOSS_17h":
			return new ULDA_Dungeon_Boss_17h();
		case "ULDA_BOSS_18h":
			return new ULDA_Dungeon_Boss_18h();
		case "ULDA_BOSS_19h":
			return new ULDA_Dungeon_Boss_19h();
		case "ULDA_BOSS_20h":
			return new ULDA_Dungeon_Boss_20h();
		case "ULDA_BOSS_21h":
			return new ULDA_Dungeon_Boss_21h();
		case "ULDA_BOSS_22h":
			return new ULDA_Dungeon_Boss_22h();
		case "ULDA_BOSS_23h":
			return new ULDA_Dungeon_Boss_23h();
		case "ULDA_BOSS_24h":
			return new ULDA_Dungeon_Boss_24h();
		case "ULDA_BOSS_25h":
			return new ULDA_Dungeon_Boss_25h();
		case "ULDA_BOSS_26h":
			return new ULDA_Dungeon_Boss_26h();
		case "ULDA_BOSS_27h":
			return new ULDA_Dungeon_Boss_27h();
		case "ULDA_BOSS_28h":
			return new ULDA_Dungeon_Boss_28h();
		case "ULDA_BOSS_29h":
			return new ULDA_Dungeon_Boss_29h();
		case "ULDA_BOSS_30h":
			return new ULDA_Dungeon_Boss_30h();
		case "ULDA_BOSS_31h":
			return new ULDA_Dungeon_Boss_31h();
		case "ULDA_BOSS_32h":
			return new ULDA_Dungeon_Boss_32h();
		case "ULDA_BOSS_33h":
			return new ULDA_Dungeon_Boss_33h();
		case "ULDA_BOSS_34h":
			return new ULDA_Dungeon_Boss_34h();
		case "ULDA_BOSS_35h":
			return new ULDA_Dungeon_Boss_35h();
		case "ULDA_BOSS_36h":
			return new ULDA_Dungeon_Boss_36h();
		case "ULDA_BOSS_37h":
		case "ULDA_BOSS_37h2":
		case "ULDA_BOSS_37h3":
			return new ULDA_Dungeon_Boss_37h();
		case "ULDA_BOSS_38h":
		case "ULDA_BOSS_38h2":
		case "ULDA_BOSS_38h3":
			return new ULDA_Dungeon_Boss_38h();
		case "ULDA_BOSS_39h":
		case "ULDA_BOSS_39h2":
		case "ULDA_BOSS_39h3":
			return new ULDA_Dungeon_Boss_39h();
		case "ULDA_BOSS_40h":
		case "ULDA_BOSS_40h2":
		case "ULDA_BOSS_40h3":
			return new ULDA_Dungeon_Boss_40h();
		case "ULDA_BOSS_41h":
			return new ULDA_Dungeon_Boss_41h();
		case "ULDA_BOSS_42h":
			return new ULDA_Dungeon_Boss_42h();
		case "ULDA_BOSS_43h":
			return new ULDA_Dungeon_Boss_43h();
		case "ULDA_BOSS_44h":
			return new ULDA_Dungeon_Boss_44h();
		case "ULDA_BOSS_45h":
			return new ULDA_Dungeon_Boss_45h();
		case "ULDA_BOSS_46h":
			return new ULDA_Dungeon_Boss_46h();
		case "ULDA_BOSS_47h":
			return new ULDA_Dungeon_Boss_47h();
		case "ULDA_BOSS_48h":
			return new ULDA_Dungeon_Boss_48h();
		case "ULDA_BOSS_49h":
			return new ULDA_Dungeon_Boss_49h();
		case "ULDA_BOSS_50h":
			return new ULDA_Dungeon_Boss_50h();
		case "ULDA_BOSS_51h":
			return new ULDA_Dungeon_Boss_51h();
		case "ULDA_BOSS_52h":
			return new ULDA_Dungeon_Boss_52h();
		case "ULDA_BOSS_53h":
			return new ULDA_Dungeon_Boss_53h();
		case "ULDA_BOSS_54h":
			return new ULDA_Dungeon_Boss_54h();
		case "ULDA_BOSS_55h":
			return new ULDA_Dungeon_Boss_55h();
		case "ULDA_BOSS_56h":
			return new ULDA_Dungeon_Boss_56h();
		case "ULDA_BOSS_57h":
			return new ULDA_Dungeon_Boss_57h();
		case "ULDA_BOSS_58h":
			return new ULDA_Dungeon_Boss_58h();
		case "ULDA_BOSS_59h":
			return new ULDA_Dungeon_Boss_59h();
		case "ULDA_BOSS_60h":
			return new ULDA_Dungeon_Boss_60h();
		case "ULDA_BOSS_61h":
			return new ULDA_Dungeon_Boss_61h();
		case "ULDA_BOSS_62h":
			return new ULDA_Dungeon_Boss_62h();
		case "ULDA_BOSS_63h":
			return new ULDA_Dungeon_Boss_63h();
		case "ULDA_BOSS_65h":
			return new ULDA_Dungeon_Boss_65h();
		case "ULDA_BOSS_66h":
			return new ULDA_Dungeon_Boss_66h();
		case "ULDA_BOSS_67h":
			return new ULDA_Dungeon_Boss_67h();
		case "ULDA_BOSS_68h":
			return new ULDA_Dungeon_Boss_68h();
		case "ULDA_BOSS_69h":
			return new ULDA_Dungeon_Boss_69h();
		case "ULDA_BOSS_70h":
			return new ULDA_Dungeon_Boss_70h();
		case "ULDA_BOSS_71h":
			return new ULDA_Dungeon_Boss_71h();
		case "ULDA_BOSS_72h":
			return new ULDA_Dungeon_Boss_72h();
		case "ULDA_BOSS_73h":
			return new ULDA_Dungeon_Boss_73h();
		case "ULDA_BOSS_74h":
			return new ULDA_Dungeon_Boss_74h();
		case "ULDA_BOSS_75h":
			return new ULDA_Dungeon_Boss_75h();
		case "ULDA_BOSS_76h":
			return new ULDA_Dungeon_Boss_76h();
		case "ULDA_BOSS_77h":
			return new ULDA_Dungeon_Boss_77h();
		case "ULDA_BOSS_78h":
			return new ULDA_Dungeon_Boss_78h();
		case "ULDA_BOSS_79h":
			return new ULDA_Dungeon_Boss_79h();
		case "TB_TempleOutrun_Ichabod":
			return new ULDA_Dungeon_Boss_17h();
		case "TB_TempleOutrun_Pillager":
			return new ULDA_Dungeon_Boss_31h();
		case "TB_TempleOutrun_Ammunae":
			return new ULDA_Dungeon_Boss_71h();
		case "TB_TempleOutrun_Battrund":
			return new ULDA_Dungeon_Boss_31h();
		case "TB_TempleOutrun_Colossus":
			return new ULDA_Dungeon_Boss_22h();
		case "TB_TempleOutrun_Isiset":
			return new ULDA_Dungeon_Boss_69h();
		case "TB_TempleOutrun_Jar":
			return new ULDA_Dungeon_Boss_47h();
		case "TB_TempleOutrun_Jythiros":
			return new ULDA_Dungeon_Boss_58h();
		case "TB_TempleOutrun_Kasmut":
			return new ULDA_Dungeon_Boss_51h();
		case "TB_TempleOutrun_Kham":
			return new ULDA_Dungeon_Boss_20h();
		case "TB_TempleOutrun_LichBazhial":
			return new ULDA_Dungeon_Boss_52h();
		case "TB_TempleOutrun_Octosari":
			return new ULDA_Dungeon_Boss_78h();
		case "TB_TempleOutrun_Rajh":
			return new ULDA_Dungeon_Boss_70h();
		case "TB_TempleOutrun_Setesh":
			return new ULDA_Dungeon_Boss_72h();
		case "TB_TempleOutrun_Sothis":
			return new ULDA_Dungeon_Boss_19h();
		case "TB_TempleOutrun_Toomba":
			return new ULDA_Dungeon_Boss_27h();
		case "TB_TempleOutrun_TrapRoom":
			return new ULDA_Dungeon_Boss_44h();
		case "TB_TempleOutrun_Zafarr":
			return new ULDA_Dungeon_Boss_02h();
		case "TB_TempleOutrun_Zaraam":
			return new ULDA_Dungeon_Boss_52h();
		case "TB_TempleOutrunHHorsema":
			return new TB_TempleOutrun_Headless();
		default:
			Log.All.PrintError("ULDA_Dungeon.InstantiateULDADungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", opposingHeroCardID);
			return new ULDA_Dungeon();
		}
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_Brann_Male_Dwarf_TRIGGER_KillCommand_01, VO_ULDA_Brann_Male_Dwarf_TRIGGER_SecretTrap_01, VO_ULDA_Brann_Male_Dwarf_TRIGGER_Whirlwind_01, VO_ULDA_Elise_Female_NightElf_TRIGGER_Innervate_01, VO_ULDA_Elise_Female_NightElf_TRIGGER_Silence_01, VO_ULDA_Elise_Female_NightElf_TRIGGER_SpreadingPlague_01, VO_ULDA_Finley_Male_Murloc_Trigger_Bloodlust_01, VO_ULDA_Finley_Male_Murloc_Trigger_Consecration_01, VO_ULDA_Finley_Male_Murloc_Trigger_Tip_the_Scales_01, VO_ULDA_Reno_Male_Human_TRIGGER_Burgle_01,
			VO_ULDA_Reno_Male_Human_TRIGGER_Fireball_01, VO_ULDA_Reno_Male_Human_TRIGGER_Frostbolt_01, VO_ULDA_Brann_Male_Dwarf_TUT_PlagueLord_Turn1_Brann_01, VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_Elise_01, VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01, VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswerSelf_01, VO_ULDA_Finley_Male_Murloc_TUT_PlagueLord_Turn1_Finley_01, VO_ULDA_Reno_Male_Human_TUT_PlagueLord_Turn1_Reno_01, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter01a_01, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter02a_01,
			VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter03a_01, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter04a_01, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Elise_Chapter02b_01, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter01b_01, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter03b_01, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter04b_01, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Reno_Chapter01b_01, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter01b_01, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter02b_01, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter03b_01,
			VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter04b_01, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter01a_01, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter02a_01, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter03a_01, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter04a_01, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter02b_01, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter04b_01, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Elise_Chapter01b_01, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter01a_01, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter02a_01,
			VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter03a_01, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter04a_01, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Reno_Chapter03b_01, VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter03b_01, VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter04b_01, VO_ULDA_Reno_Male_Human_STORY_Banter_Finley_Chapter02b_01, VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter01a_01, VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter02a_01, VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter03a_01, VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter04a_01,
			VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_02, VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_03, VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_02, VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_03, VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_02, VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_03, VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_01,
			VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_02, VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_03, VO_ULDA_Reno_Male_Human_RenoTreasureGatlingWand_01, VO_ULDA_Reno_Male_Human_RenoTreasureHat_01, VO_ULDA_Reno_Male_Human_RenoTreasureJrExplorer_01, VO_ULDA_Reno_Male_Human_RenoTreasureLei_01, VO_ULDA_Reno_Male_Human_RenoTreasureTitanRing_01, VO_ULDA_Reno_Male_Human_RenoTreasureWhip_01, VO_ULDA_Elise_Female_NightElf_EliseJrExplorer_01, VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_01,
			VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_02, VO_ULDA_Elise_Female_NightElf_EliseTreasureDruidStaff_01, VO_ULDA_Elise_Female_NightElf_EliseTreasureMachete_01, VO_ULDA_Elise_Female_NightElf_EliseTreasureScarabStatue_01, VO_ULDA_Brann_Male_Dwarf_BrannTreasureBlunderbuss_01, VO_ULDA_Brann_Male_Dwarf_BrannTreasureFlo_01, VO_ULDA_Brann_Male_Dwarf_BrannTreasureGiantEgg_01, VO_ULDA_Brann_Male_Dwarf_BrannTreasureJrExplorer_01, VO_ULDA_Brann_Male_Dwarf_BrannTreasureSaddle_01, VO_ULDA_Finley_Male_Murloc_FinleyTreasureJrExplorer_01,
			VO_ULDA_Finley_Male_Murloc_FinleyTreasureKarl_01, VO_ULDA_Finley_Male_Murloc_FinleyTreasureLance_01, VO_ULDA_Finley_Male_Murloc_FinleyTreasureMurkyHorn_01, VO_ULDA_Finley_Male_Murloc_FinleyTreasurePandarenTeaSet_01, VO_ULDA_Finley_Male_Murloc_FinleyTreasureScarabMount_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01,
			VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01,
			VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01
		};
		m_PlayerVOLines = new List<string>(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	public void SetBossVOLines(List<string> VOLines)
	{
		m_BossVOLines = new List<string>(VOLines);
	}

	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	protected virtual void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = ChanceToPlayBossHeroPowerVOLine();
		float num2 = Random.Range(0f, 1f);
		if (m_enemySpeaking || num < num2)
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (actor == null)
		{
			return;
		}
		List<string> bossHeroPowerRandomLines = GetBossHeroPowerRandomLines();
		string text = "";
		while (bossHeroPowerRandomLines.Count > 0)
		{
			int index = Random.Range(0, bossHeroPowerRandomLines.Count);
			text = bossHeroPowerRandomLines[index];
			bossHeroPowerRandomLines.RemoveAt(index);
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
		}
		if (!(text == ""))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce(text, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (!m_enemySpeaking && entity.GetCardType() != 0 && entity.GetCardType() == TAG_CARDTYPE.HERO_POWER && entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		string playerHeroCardID = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardID = entity.GetCardId();
		if (playerHeroCardID == "ULDA_Reno")
		{
			switch (cardID)
			{
			case "AT_033":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_TRIGGER_Burgle_01);
				break;
			case "CS2_029":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_TRIGGER_Fireball_01);
				break;
			case "CS2_024":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_TRIGGER_Frostbolt_01);
				break;
			case "ULDA_207":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_RenoTreasureGatlingWand_01);
				break;
			case "ULDA_203":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_RenoTreasureHat_01);
				break;
			case "ULDA_016":
			case "ULDA_021":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_RenoTreasureJrExplorer_01);
				break;
			case "ULDA_206":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_RenoTreasureLei_01);
				break;
			case "ULDA_201":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_RenoTreasureWhip_01);
				break;
			}
		}
		if (playerHeroCardID == "ULDA_Finley")
		{
			switch (cardID)
			{
			case "CS2_046":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_Trigger_Bloodlust_01);
				break;
			case "CS2_093":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_Trigger_Consecration_01);
				break;
			case "ULD_716":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_Trigger_Tip_the_Scales_01);
				break;
			case "ULDA_501":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_FinleyTreasureScarabMount_01);
				break;
			case "ULDA_504":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_FinleyTreasurePandarenTeaSet_01);
				break;
			case "ULDA_010":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_FinleyTreasureMurkyHorn_01);
				break;
			case "ULDA_507":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_FinleyTreasureKarl_01);
				break;
			case "ULDA_015":
			case "ULDA_020":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_FinleyTreasureJrExplorer_01);
				break;
			case "ULDA_505":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_FinleyTreasureLance_01);
				break;
			}
		}
		if (playerHeroCardID == "ULDA_Elise")
		{
			switch (cardID)
			{
			case "EX1_169":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_TRIGGER_Innervate_01);
				break;
			case "EX1_332":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_TRIGGER_Silence_01);
				break;
			case "ICC_054":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_TRIGGER_SpreadingPlague_01);
				break;
			case "ULDA_003":
				yield return PlayAndRemoveRandomLineOnlyOnce(playerActor, m_EliseTreasureAddarah);
				break;
			case "ULDA_304":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_EliseTreasureDruidStaff_01);
				break;
			case "ULDA_305":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_EliseTreasureMachete_01);
				break;
			case "ULDA_302":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_EliseTreasureScarabStatue_01);
				break;
			case "ULDA_017":
			case "ULDA_022":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_EliseJrExplorer_01);
				break;
			}
		}
		if (playerHeroCardID == "ULDA_Brann")
		{
			switch (cardID)
			{
			case "EX1_539":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_TRIGGER_KillCommand_01);
				break;
			case "AT_060":
			case "EX1_554":
			case "EX1_610":
			case "EX1_611":
			case "GIL_577":
			case "ICC_200":
			case "LOE_021":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_TRIGGER_SecretTrap_01);
				break;
			case "EX1_400":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_TRIGGER_Whirlwind_01);
				break;
			case "ULDA_405":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_BrannTreasureGiantEgg_01);
				break;
			case "ULDA_002":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_BrannTreasureFlo_01);
				break;
			case "ULDA_018":
			case "ULDA_023":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_BrannTreasureJrExplorer_01);
				break;
			case "ULDA_401":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_BrannTreasureBlunderbuss_01);
				break;
			case "ULDA_402":
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_BrannTreasureSaddle_01);
				break;
			}
		}
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (!m_enemySpeaking && !string.IsNullOrEmpty(m_deathLine) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (GetShouldSuppressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_deathLine, Notification.SpeechBubbleDirection.None, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_deathLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected string PopRandomLine(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		string text = lines[Random.Range(0, lines.Count)];
		lines.Remove(text);
		return text;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = null;
		m_deathLine = null;
		m_standardEmoteResponseLine = null;
		m_BossIdleLines = new List<string>(GetIdleLines());
		m_BossIdleLinesCopy = new List<string>(GetIdleLines());
	}

	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	protected float ChanceToPlayMurlocPlotTwistVOLine()
	{
		return 0.1f;
	}

	protected float ChanceToPlayMadnessPlotTwistVOLine()
	{
		return 0.1f;
	}

	protected float ChanceToPlayDeathPlotTwistVOLine()
	{
		return 0.5f;
	}

	protected float ChanceToPlayWrathPlotTwistVOLine()
	{
		return 0.05f;
	}

	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = PopRandomLine(lines);
		if (text != null)
		{
			yield return PlayLineOnlyOnce(actor, text);
		}
	}

	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = PopRandomLine(lines);
		if (text != null)
		{
			yield return PlayLineOnlyOnce(actor, text);
		}
	}

	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = PopRandomLine(lines);
		if (text != null)
		{
			yield return PlayBossLine(actor, text);
		}
	}

	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = PopRandomLine(lines);
		if (text != null)
		{
			yield return PlayBossLine(actor, text);
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		float num = Random.Range(0f, 1f);
		bool flag = false;
		int num2 = GetTag(GAME_TAG.TURN) - GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
		switch (cardId)
		{
		case "ULDA_BOSS_37h":
		case "ULDA_BOSS_37h2":
		case "ULDA_BOSS_37h3":
		case "ULDA_BOSS_38h":
		case "ULDA_BOSS_38h2":
		case "ULDA_BOSS_38h3":
		case "ULDA_BOSS_39h":
		case "ULDA_BOSS_39h2":
		case "ULDA_BOSS_39h3":
		case "ULDA_BOSS_40h":
		case "ULDA_BOSS_40h2":
		case "ULDA_BOSS_40h3":
			flag = true;
			break;
		}
		switch (missionEvent)
		{
		case 801:
			if (!(ChanceToPlayDeathPlotTwistVOLine() < num) && TurnOfPlotTwistLastPlayed < num2)
			{
				TurnOfPlotTwistLastPlayed = num2;
				if (flag)
				{
					yield return PlayRandomLineAlways(actor2, m_BossDeathPlotTwistTauntLines);
					break;
				}
				TurnOfPlotTwistLastPlayed = num2;
				yield return PlayRandomLineAlways(Tombs_of_Terror_Xatma_BrassRing_Quote, m_BossDeathPlotTwistTauntLines);
			}
			break;
		case 802:
			if (!(ChanceToPlayMadnessPlotTwistVOLine() < num) && TurnOfPlotTwistLastPlayed < num2)
			{
				TurnOfPlotTwistLastPlayed = num2;
				if (flag)
				{
					yield return PlayRandomLineAlways(actor2, m_BossMadnessPlotTwistTauntLines);
				}
				else
				{
					yield return PlayRandomLineAlways(Tombs_of_Terror_Kzrath_BrassRing_Quote, m_BossMadnessPlotTwistTauntLines);
				}
			}
			break;
		case 803:
			if (!(ChanceToPlayMurlocPlotTwistVOLine() < num) && TurnOfPlotTwistLastPlayed < num2)
			{
				TurnOfPlotTwistLastPlayed = num2;
				if (flag)
				{
					yield return PlayRandomLineAlways(actor2, m_BossMurlocPlotTwistTauntLines);
				}
				else
				{
					yield return PlayRandomLineAlways(Tombs_of_Terror_Vesh_BrassRing_Quote, m_BossMurlocPlotTwistTauntLines);
				}
			}
			break;
		case 804:
			if (!(ChanceToPlayWrathPlotTwistVOLine() < num) && TurnOfPlotTwistLastPlayed < num2)
			{
				TurnOfPlotTwistLastPlayed = num2;
				if (flag)
				{
					yield return PlayRandomLineAlways(actor2, m_BossWrathPlotTwistTauntLines);
				}
				else
				{
					yield return PlayRandomLineAlways(Tombs_of_Terror_Icarax_BrassRing_Quote, m_BossWrathPlotTwistTauntLines);
				}
			}
			break;
		case 505:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01);
			break;
		case 506:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01);
			break;
		case 507:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01);
			break;
		case 508:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01);
			break;
		case 509:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01);
			break;
		case 510:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01);
			break;
		case 511:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01);
			break;
		case 512:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01);
			break;
		case 513:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01);
			break;
		case 514:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01);
			break;
		case 515:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01);
			break;
		case 516:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01);
			break;
		case 517:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01);
			break;
		case 518:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01);
			break;
		case 519:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01);
			break;
		case 520:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01);
			break;
		case 521:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01);
			break;
		case 522:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01);
			break;
		case 523:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01);
			break;
		case 524:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01);
			break;
		case 525:
			yield return PlayBossLine(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01);
			break;
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
			yield return PlayBossLine(actor2, m_BossVOLines[m_PlayBossVOLineIndex]);
			break;
		case 1003:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(m_BossVOLines[m_PlayBossVOLineIndex]);
			yield return PlayBossLine(actor2, m_BossVOLines[m_PlayBossVOLineIndex]);
			break;
		case 58023:
		{
			Network.Get().DisconnectFromGameServer();
			SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
			GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
			SceneMgr.Get().SetNextMode(postGameSceneMode);
			break;
		}
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (playerSide == Player.Side.OPPOSING)
		{
			switch (missionId)
			{
			case 3428:
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_01");
			case 3429:
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_02");
			case 3430:
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_03");
			case 3431:
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_04");
			case 3432:
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_05");
			case 3433:
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_01_HEROIC");
			case 3434:
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_02_HEROIC");
			case 3435:
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_03_HEROIC");
			case 3436:
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_04_HEROIC");
			case 3437:
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_05_HEROIC");
			}
		}
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide() || currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		float thinkEmoteBossThinkChancePercentage = GetThinkEmoteBossThinkChancePercentage();
		float num = Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num && m_BossIdleLines != null && m_BossIdleLines.Count != 0)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			string line = PopRandomLine(m_BossIdleLinesCopy);
			if (m_BossIdleLinesCopy.Count == 0)
			{
				m_BossIdleLinesCopy = new List<string>(GetIdleLines());
			}
			Gameplay.Get().StartCoroutine(PlayBossLine(actor, line));
			return;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (Random.Range(1, 4))
		{
		case 1:
			emoteType = EmoteType.THINK1;
			break;
		case 2:
			emoteType = EmoteType.THINK2;
			break;
		case 3:
			emoteType = EmoteType.THINK3;
			break;
		}
		GameState.Get().GetCurrentPlayer().GetHeroCard()
			.PlayEmote(emoteType);
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		ScenarioDbId missionId = (ScenarioDbId)GameMgr.Get().GetMissionId();
		string playerHeroCardID = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		bool isFirstBoss = ULDA_MissionEntity.GetIsFirstBoss();
		if (turn == 1 && isFirstBoss && GameState.Get() != null && GameState.Get().GetFriendlySidePlayer() != null && GameState.Get().GetFriendlySidePlayer().GetHero() != null)
		{
			switch (missionId)
			{
			case ScenarioDbId.ULDA_CITY:
			case ScenarioDbId.ULDA_CITY_HEROIC:
				switch (playerHeroCardID)
				{
				case "ULDA_Finley":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter01a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Brann_BrassRing_Quote, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter01b_01);
					break;
				case "ULDA_Brann":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter01a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter01b_01);
					break;
				case "ULDA_Elise":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter01a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Finley_BrassRing_Quote, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Elise_Chapter01b_01);
					break;
				}
				break;
			case ScenarioDbId.ULDA_DESERT:
			case ScenarioDbId.ULDA_DESERT_HEROIC:
				switch (playerHeroCardID)
				{
				case "ULDA_Reno":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter02a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter02b_01);
					break;
				case "ULDA_Finley":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter02a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Reno_BrassRing_Quote, VO_ULDA_Reno_Male_Human_STORY_Banter_Finley_Chapter02b_01);
					break;
				case "ULDA_Brann":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter02a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter02b_01);
					break;
				case "ULDA_Elise":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter02a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Brann_BrassRing_Quote, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Elise_Chapter02b_01);
					break;
				}
				break;
			case ScenarioDbId.ULDA_TOMB:
			case ScenarioDbId.ULDA_TOMB_HEROIC:
				switch (playerHeroCardID)
				{
				case "ULDA_Reno":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter03a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Finley_BrassRing_Quote, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Reno_Chapter03b_01);
					break;
				case "ULDA_Finley":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter03a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Brann_BrassRing_Quote, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter03b_01);
					break;
				case "ULDA_Brann":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter03a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter03b_01);
					break;
				case "ULDA_Elise":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter03a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Reno_BrassRing_Quote, VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter03b_01);
					break;
				}
				break;
			case ScenarioDbId.ULDA_HALLS:
			case ScenarioDbId.ULDA_HALLS_HEROIC:
				switch (playerHeroCardID)
				{
				case "ULDA_Reno":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter04a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter04b_01);
					break;
				case "ULDA_Finley":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter04a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Brann_BrassRing_Quote, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter04b_01);
					break;
				case "ULDA_Brann":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter04a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter04b_01);
					break;
				case "ULDA_Elise":
					yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter04a_01);
					yield return PlayLineOnlyOnce(Tombs_of_Terror_Reno_BrassRing_Quote, VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter04b_01);
					break;
				}
				break;
			}
		}
		bool num = GameUtils.GetDefeatedBossCount() == GetDefeatedBossCountForFinalBoss();
		int currentHealth = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCurrentHealth();
		if (num && GetDefeatedBossCountForFinalBoss() != 0 && currentHealth == 300)
		{
			switch (playerHeroCardID)
			{
			case "ULDA_Reno":
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Reno_Male_Human_TUT_PlagueLord_Turn1_Reno_01);
				yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01);
				GameState.Get().SetBusy(busy: false);
				break;
			case "ULDA_Finley":
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Finley_Male_Murloc_TUT_PlagueLord_Turn1_Finley_01);
				yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01);
				GameState.Get().SetBusy(busy: false);
				break;
			case "ULDA_Brann":
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Brann_Male_Dwarf_TUT_PlagueLord_Turn1_Brann_01);
				yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01);
				GameState.Get().SetBusy(busy: false);
				break;
			case "ULDA_Elise":
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_Elise_01);
				yield return PlayLineOnlyOnce(playerActor, VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswerSelf_01);
				GameState.Get().SetBusy(busy: false);
				break;
			}
		}
	}
}
