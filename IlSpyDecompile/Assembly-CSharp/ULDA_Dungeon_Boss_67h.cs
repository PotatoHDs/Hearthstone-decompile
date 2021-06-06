using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULDA_Dungeon_Boss_67h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossMomentumSwing_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossMomentumSwing_01.prefab:816084397af586144a486cf364344106");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerCataclysm_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerCataclysm_01.prefab:03d38ed167c4ebb4fb722b36e1a68c1d");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFireball_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFireball_01.prefab:2658eca0ec276dc468bb4b186acecee1");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFlamestrike_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFlamestrike_01.prefab:a56895a29e1925c45b7e4e1988355d47");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerForbiddenFlame_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerForbiddenFlame_01.prefab:b2881bc87774be2409d33711b93433c8");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerHellfire_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerHellfire_01.prefab:8d10a78d6f1fc744d908b5ebe715a3e6");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerObsidianDestroyer_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerObsidianDestroyer_01.prefab:25e08154b6fe0364e96a34e794e18241");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerPlagueofFlames_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerPlagueofFlames_01.prefab:66e0eeafede90164c8319781de9f8f42");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuDeath_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuDeath_01.prefab:a71752c90b1fc1f43bb75e48cd259687");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuSummon_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuSummon_01.prefab:787044eb33b05b44392fd8c598ee18a0");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerSoulfire_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerSoulfire_01.prefab:02992b091a683564f9524005f9918e6b");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_DeathALT_01.prefab:fb1218e8ca32acb4fbd9a4b33b480371");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_01.prefab:29c4248476d72844b9a8c208f9b540d1");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_02.prefab:c069a4ebba7700a44b9ba35c722b0ca9");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_03.prefab:82e4d0208d8d2ea4eb554d4f71e2c3f2");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_EmoteResponse_01.prefab:3810ea78be89b374e8d29f0a9247b13f");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_01.prefab:9d1ce84d8e2304b42885d1180616dcd2");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_02.prefab:85df586f1cf3cd84e95ce2ac9ad5faf7");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_03.prefab:98eac91c25a56a14cb1f51c27e98c06c");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_04 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_04.prefab:b2a253fae9296894e9fba0b19bff2d68");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_01.prefab:e99648f6a1a1d3343bb36b75cbd07210");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_02.prefab:6f3bff53ba7b7944eb9fa10ae5b8c1e6");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_03.prefab:4da8708434d359e408fc1afae7439875");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_04 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_04.prefab:ae51e12f422e7a84f9cc5999f1d5c536");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_01.prefab:194b003e852e6c74a9d82fdacb3db1d5");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_02.prefab:3117a2db6d484764692abaf7d1db4ccb");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_03.prefab:56b3ccc835f383d448db0c876c1cf48e");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_04 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_04.prefab:95da71fde629b9a44b7b69bdf6d4ccc4");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_05 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_05.prefab:bc4792e6296d61e40b14b59535b6186c");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_01.prefab:1f5c178cde1a63a47be9795646318fa2");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_02.prefab:c3032e38e784c3242ad7b3fd40203d11");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_03.prefab:4201e41ed92e1f14ea49f878a286f1da");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_04 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_04.prefab:d8106260a8672ce41bdcd91f83b7da36");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_05 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_05.prefab:d1e099f03d91a40449a787e205662a9f");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IdleSpecial_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IdleSpecial_01.prefab:ddd69f71273eac147a90d2a6708ef77f");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroFirstEncounter_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroFirstEncounter_01.prefab:a0413350949dbe944b8eadb05dcdbbe0");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroGeneric_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroGeneric_01.prefab:d1ac9639c5615bd41ae8f4ed0d68c7a4");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1_01.prefab:2982cc607bb12c9499b0a7b6bab2d288");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_01.prefab:5d408ca9b9ac03b4ba8da2f251a62b09");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_02 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_02.prefab:ad5ed4e61852d8e459588cb7c85bb38e");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase2Retry_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase2Retry_01.prefab:7cef55607aab3964d9a12c9bd961fd31");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_01.prefab:6719959d771a07f44975aed2abbbed34");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_03 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_03.prefab:5998b85acb10b5a4abe541a02eebaeb5");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01.prefab:df25e9123e451d247a441338d4c0d22e");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann2_01.prefab:f0c0c501e6296ec44be7d1d6d5f33a98");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01.prefab:f8b3005946314c646b2b27920aa27536");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise2_01.prefab:8efbfe3975972a2488ea91d9a8e095f1");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01.prefab:f19bb8621de37784796ae97019b38e41");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley2_01.prefab:cc63ac8b86153ca4ebb7c5109bae2402");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01.prefab:6d8f7a16336fb9a4f8eb82f10a095302");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno2_01.prefab:526fbd609cb27594bb2c32df0908ee6a");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange1_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange1_01.prefab:f03b223b3fc21d74d830d67a22394750");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange2_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange2_01.prefab:9097a7cd2af1cae418b487911169efe3");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerJrExplorer_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerJrExplorer_01.prefab:7362d25550003de418965dd3d066a581");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerKingPhaoris_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerKingPhaoris_01.prefab:640358e8a8aba9a42a59c8a2cc944f82");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSkilledPathfinder_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSkilledPathfinder_01.prefab:54d2867b71e59d24fb40210971a0b52f");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSunkeeperTarim_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSunkeeperTarim_01.prefab:f157ae6fac6770544843a72c2d7a9111");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerZephrystheGreat_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerZephrystheGreat_01.prefab:d0179e10bce9d88439f73053e6c1daa4");

	private static readonly AssetReference Tombs_of_Terror_Brann_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Brann_BrassRing_Quote.prefab:d521a1fe41518e24da6e4252b97fbeb7");

	private static readonly AssetReference Tombs_of_Terror_Elise_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Elise_BrassRing_Quote.prefab:6e9e8faaae29f834183122d7ea9be68d");

	private static readonly AssetReference Tombs_of_Terror_Finley_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Finley_BrassRing_Quote.prefab:547ebc970764ec64da6eb3de26ed4698");

	private static readonly AssetReference Tombs_of_Terror_Reno_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Reno_BrassRing_Quote.prefab:4c0b79d4f597c464baabf02e06cf8ae7");

	private static readonly AssetReference Rafaam_popup_BrassRing_Quote = new AssetReference("Rafaam_popup_BrassRing_Quote.prefab:187724fae6d64cf49acf11aa53ca2087");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_Elise_vs_Tekahn_b_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_Elise_vs_Tekahn_b_01.prefab:ee44a7efa3deff84e8c778c239a47cf4");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_TUT_Finale_Brann_Treasures_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_TUT_Finale_Brann_Treasures_01.prefab:75d3735ecc141cc409c426f0d519f149");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TUT_Finale_Elise_Treasures_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TUT_Finale_Elise_Treasures_01.prefab:e698826b7127cae429b490f8fa5e6b7f");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_TUT_Finale_Finley_Treasures_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_TUT_Finale_Finley_Treasures_01.prefab:a8c620ea58669a744b06dfd3b0b3ae94");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_TUT_Finale_Reno_Treasures_01 = new AssetReference("VO_ULDA_Reno_Male_Human_TUT_Finale_Reno_Treasures_01.prefab:1386554a1d31f2245b0f9e0a84c4968b");

	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_b_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_b_01.prefab:8d25fab9fad46394894ed0001a36501f");

	private static readonly AssetReference VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_c_01 = new AssetReference("VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_c_01.prefab:68c3a5a587ad7b64886ff1410b055d62");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_a_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_a_01.prefab:9c3335349390c3b4982a98bfbe3bb804");

	private static readonly AssetReference VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_d_01 = new AssetReference("VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_d_01.prefab:d3638361758c19d4793fc4a99414992b");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Start_vs_Plague_Fire_01 = new AssetReference("VO_ULDA_Reno_Male_Human_Start_vs_Plague_Fire_01.prefab:ce58d269d5d3e634eb6b1a6ea38ccea3");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_Start_vs_Plague_Fire_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_Start_vs_Plague_Fire_01.prefab:3fdc759c78353af41aedd9805ecad104");

	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_Start_vs_Plague_Fire_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_Start_vs_Plague_Fire_01.prefab:4b11912846eb2e04c95d83ddc3a4095a");

	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Start_vs_Plague_Fire_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Start_vs_Plague_Fire_01.prefab:eca71404cedfc4e438ae240932f82537");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TUT_Finale_Final_Defeat_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TUT_Finale_Final_Defeat_01.prefab:6df8872b16a0722449e24b1267325cb3");

	private List<string> m_DefeatHeroLines = new List<string> { VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_01, VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_02 };

	private List<string> m_HeroPower1Lines = new List<string> { VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_01, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_02, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_03, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_04 };

	private List<string> m_HeroPower2Lines = new List<string> { VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_01, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_02, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_03, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_04 };

	private List<string> m_HeroPower3Lines = new List<string> { VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_05 };

	private static List<string> m_IdleLines = new List<string> { VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_01, VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_02, VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_03, VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_05 };

	private List<string> m_IntroLines = new List<string> { VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_03, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase2Retry_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_02, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroFirstEncounter_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroGeneric_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1_01 };

	private List<string> m_IdleLinesCopy = new List<string>(m_IdleLines);

	private bool m_LinesPlaying;

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_67h_Male_PlagueLord_BossMomentumSwing_01, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerCataclysm_01, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFireball_01, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFlamestrike_01, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerForbiddenFlame_01, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerHellfire_01, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerObsidianDestroyer_01, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerPlagueofFlames_01, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuDeath_01, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuSummon_01,
			VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerSoulfire_01, VO_ULDA_BOSS_67h_Male_PlagueLord_DeathALT_01, VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_01, VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_02, VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_03, VO_ULDA_BOSS_67h_Male_PlagueLord_EmoteResponse_01, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_01, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_02, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_03, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower1_04,
			VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_01, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_02, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_03, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower2_04, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_67h_Male_PlagueLord_HeroPower3_05, VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_01,
			VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_02, VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_03, VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_67h_Male_PlagueLord_Idle_05, VO_ULDA_BOSS_67h_Male_PlagueLord_IdleSpecial_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroFirstEncounter_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroGeneric_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase1Retry_02,
			VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase2Retry_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroPhase3Retry_03, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann2_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise2_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley2_01, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01,
			VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno2_01, VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange1_01, VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange2_01, VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerJrExplorer_01, VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerKingPhaoris_01, VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSkilledPathfinder_01, VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSunkeeperTarim_01, VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerZephrystheGreat_01, VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_Elise_vs_Tekahn_b_01, VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_a_01,
			VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_d_01, VO_ULDA_Brann_Male_Dwarf_TUT_Finale_Brann_Treasures_01, VO_ULDA_Elise_Female_NightElf_TUT_Finale_Elise_Treasures_01, VO_ULDA_Finley_Male_Murloc_TUT_Finale_Finley_Treasures_01, VO_ULDA_Reno_Male_Human_TUT_Finale_Reno_Treasures_01, VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_b_01, VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_c_01, VO_ULDA_Reno_Male_Human_Start_vs_Plague_Fire_01, VO_ULDA_Elise_Female_NightElf_Start_vs_Plague_Fire_01, VO_ULDA_Brann_Male_Dwarf_Start_vs_Plague_Fire_01,
			VO_ULDA_Finley_Male_Murloc_Start_vs_Plague_Fire_01, VO_ULDA_Elise_Female_NightElf_TUT_Finale_Final_Defeat_01
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
		m_deathLine = VO_ULDA_BOSS_67h_Male_PlagueLord_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_67h_Male_PlagueLord_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Gameplay.Get().StartCoroutine(PlayMultipleVOLinesForEmotes(emoteType, emoteSpell));
	}

	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			yield return PlayBossLine(actor, m_standardEmoteResponseLine);
		}
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
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_HeroPower1Lines);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_HeroPower2Lines);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_HeroPower3Lines);
			break;
		case 104:
			yield return PlayLineOnlyOnce(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuSummon_01);
			break;
		case 105:
			yield return PlayLineOnlyOnce(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerRakanishuDeath_01);
			break;
		case 106:
			yield return PlayBossLine(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange1_01);
			break;
		case 107:
			yield return PlayBossLine(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_PhaseChange2_01);
			break;
		case 108:
			yield return PlayLineOnlyOnce(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_BossMomentumSwing_01);
			break;
		case 109:
			switch (cardId)
			{
			case "ULDA_Reno":
				yield return PlayLineOnlyOnce(Tombs_of_Terror_Reno_BrassRing_Quote, VO_ULDA_Reno_Male_Human_TUT_Finale_Reno_Treasures_01);
				break;
			case "ULDA_Finley":
				yield return PlayLineOnlyOnce(Tombs_of_Terror_Finley_BrassRing_Quote, VO_ULDA_Finley_Male_Murloc_TUT_Finale_Finley_Treasures_01);
				break;
			case "ULDA_Brann":
				yield return PlayLineOnlyOnce(Tombs_of_Terror_Brann_BrassRing_Quote, VO_ULDA_Brann_Male_Dwarf_TUT_Finale_Brann_Treasures_01);
				break;
			case "ULDA_Elise":
				yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_TUT_Finale_Elise_Treasures_01);
				break;
			}
			break;
		case 110:
			m_LinesPlaying = true;
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_a_01);
			yield return PlayLineOnlyOnce(Rafaam_popup_BrassRing_Quote, VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_b_01);
			yield return PlayLineOnlyOnce(Rafaam_popup_BrassRing_Quote, VO_DALA_Rafaam_Male_Ethereal_STORY_Finale_FinalPhase_c_01);
			yield return PlayLineOnlyOnce(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_FinalPhase_d_01);
			GameState.Get().SetBusy(busy: false);
			m_LinesPlaying = false;
			break;
		case 111:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_DefeatHeroLines);
			break;
		case 112:
			m_LinesPlaying = true;
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_DefeatPlayer_03);
			yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_TUT_Finale_Final_Defeat_01);
			GameState.Get().SetBusy(busy: false);
			m_LinesPlaying = false;
			break;
		case 113:
		{
			int randomNumber = Random.Range(1, 5);
			GameState.Get().SetBusy(busy: true);
			if (randomNumber == 1)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01);
			}
			if (randomNumber == 2)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01);
			}
			if (randomNumber == 3)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01);
			}
			if (randomNumber == 4)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01);
			}
			m_introLine = PopRandomLine(m_IntroLines);
			if (m_introLine == (string)VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01 || m_introLine == (string)VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01 || m_introLine == (string)VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01 || m_introLine == (string)VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01)
			{
				yield return PlayBossLine(enemyActor, m_introLine);
				if (randomNumber == 1)
				{
					yield return PlayBossLine(Tombs_of_Terror_Reno_BrassRing_Quote, VO_ULDA_Reno_Male_Human_Start_vs_Plague_Fire_01);
				}
				switch (randomNumber)
				{
				case 2:
					yield return PlayBossLine(Tombs_of_Terror_Brann_BrassRing_Quote, VO_ULDA_Brann_Male_Dwarf_Start_vs_Plague_Fire_01);
					break;
				case 3:
					yield return PlayBossLine(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_Start_vs_Plague_Fire_01);
					break;
				case 4:
					yield return PlayBossLine(Tombs_of_Terror_Finley_BrassRing_Quote, VO_ULDA_Finley_Male_Murloc_Start_vs_Plague_Fire_01);
					break;
				}
				if (m_introLine == (string)VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno_01)
				{
					yield return PlayBossLine(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialReno2_01);
				}
				else if (m_introLine == (string)VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann_01)
				{
					yield return PlayBossLine(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialBrann2_01);
				}
				else if (m_introLine == (string)VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise_01)
				{
					yield return PlayBossLine(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialElise2_01);
				}
				else if (m_introLine == (string)VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley_01)
				{
					yield return PlayBossLine(enemyActor, VO_ULDA_BOSS_67h_Male_PlagueLord_IntroSpecialFinley2_01);
				}
			}
			else
			{
				if (randomNumber == 1)
				{
					yield return PlayBossLine(Tombs_of_Terror_Reno_BrassRing_Quote, VO_ULDA_Reno_Male_Human_Start_vs_Plague_Fire_01);
				}
				switch (randomNumber)
				{
				case 2:
					yield return PlayBossLine(Tombs_of_Terror_Brann_BrassRing_Quote, VO_ULDA_Brann_Male_Dwarf_Start_vs_Plague_Fire_01);
					break;
				case 3:
					yield return PlayBossLine(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_Start_vs_Plague_Fire_01);
					break;
				case 4:
					yield return PlayBossLine(Tombs_of_Terror_Finley_BrassRing_Quote, VO_ULDA_Finley_Male_Murloc_Start_vs_Plague_Fire_01);
					break;
				}
				yield return PlayBossLine(enemyActor, m_introLine);
			}
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 10:
			if (GameState.Get().IsFriendlySidePlayerTurn())
			{
				TurnStartManager.Get().BeginListeningForTurnEvents();
			}
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ULDA_015":
			case "ULDA_016":
			case "ULDA_017":
			case "ULDA_018":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerJrExplorer_01);
				break;
			case "ULD_157":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSkilledPathfinder_01);
				break;
			case "ULD_003":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerZephrystheGreat_01);
				break;
			case "ULD_304":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerKingPhaoris_01);
				break;
			case "UNG_015":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_PlayerTriggerSunkeeperTarim_01);
				break;
			}
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ULDA_BOSS_67t":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_STORY_Finale_Elise_vs_Tekahn_b_01);
				break;
			case "EX1_308":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerSoulfire_01);
				break;
			case "CS2_062":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerHellfire_01);
				break;
			case "LOE_009":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerObsidianDestroyer_01);
				break;
			case "CS2_032":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFlamestrike_01);
				break;
			case "CS2_029":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerFireball_01);
				break;
			case "OG_086":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerForbiddenFlame_01);
				break;
			case "ULD_717":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_67h_Male_PlagueLord_BossTriggerPlagueofFlames_01);
				break;
			}
		}
	}

	public override void OnPlayThinkEmote()
	{
		if (m_enemySpeaking || m_LinesPlaying)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer.IsFriendlySide() && !currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			string line = PopRandomLine(m_IdleLinesCopy);
			if (m_IdleLinesCopy.Count == 0)
			{
				m_IdleLinesCopy = new List<string>(m_IdleLines);
			}
			Gameplay.Get().StartCoroutine(PlayBossLine(actor, line));
		}
	}
}
