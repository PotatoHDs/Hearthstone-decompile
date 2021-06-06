using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_38h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossMomentumSwing_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossMomentumSwing_01.prefab:5a3640ac1a8e6914abd67ef931c48e06");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerCleverDisguise_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerCleverDisguise_01.prefab:923bb9ccd09f6a4458c0f8209125ef40");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerDevourMind_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerDevourMind_01.prefab:4119495ef194bd745b8e4dfe43d28abd");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerMindControl_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerMindControl_01.prefab:c0a19e05293fcb24fb27ea792948309b");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerPlagueofMadness_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerPlagueofMadness_01.prefab:10e49746ada1c674cbaefdaa99d58aab");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerShadowMadness_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerShadowMadness_01.prefab:71c83982df42acf458656cb35ded1352");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSpreadingMadness_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSpreadingMadness_01.prefab:280983f4ac3d3bb4fa727890bbdc18a6");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSurrendertoMadness_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSurrendertoMadness_01.prefab:a04dd942a51b5004faf816171e9453ee");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerWastewanderSapper_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerWastewanderSapper_01.prefab:f116cf709c127f345982168a67031be9");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_DeathALT_01.prefab:7e86996565b80264fbd2162de15f17c6");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_01.prefab:a5441912fe34df14cbe6be982ca03cb6");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_02.prefab:35ec72459bd5f2640851721c75c3d5ab");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_03.prefab:aedeccf3d2f6dc147861ab1997c1a305");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_EmoteResponse_01.prefab:99cbf0bbdfc57194ba49029d5a9bdf3b");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_01.prefab:9fd94ec5ab2f83a46a607bf657f8bb53");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_02.prefab:b5be1d08ca34ee34eb489771e9ab517d");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_03.prefab:4b4984ed23a12c2488caed8c405d12e9");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_04 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_04.prefab:c294c76aa7b2ded489eabf5079f4a32d");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_01.prefab:e807765f9b0b56946b553e5e5e85da82");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_02.prefab:11f8fc2d8b51e254ca751ed84f401ff0");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_03.prefab:13beadc2b3065e24485a42af64bc08aa");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_04 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_04.prefab:384a4f55f17180249b1875cfc8ddee26");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_01.prefab:f1a2a97caf82cb246adff4ce8ebd071b");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_02.prefab:986e87a46b35f2b45bbba1900e529881");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_03.prefab:6a8da03a3b26f57448864644a009980d");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_04 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_04.prefab:a880df77698485f4ebafab5acc77c4f0");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_05 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_05.prefab:2d9aee7d48de9b24891441114297ecf1");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01.prefab:41979b81a51560346adee6bb3b3253ec");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02.prefab:d358a93940501e54db74f8e41c2112bd");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03.prefab:f5bf3e6094154be4dbac592b5e4c974e");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04.prefab:b563e333f27d5894584b95709904580c");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05.prefab:43b947bf763c0e6408edad722edd02ef");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IdleSpecial_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IdleSpecial_01.prefab:4e495d3ccc7e8794f940df95153a1695");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroFirstEncounter_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroFirstEncounter_01.prefab:6a380d69ceff5464783ba3d3149bf11d");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroGeneric_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroGeneric_01.prefab:5116372b2f600114aa4203b1240462f0");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1_01.prefab:000aea2a4cbe58f4a813682df959d712");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_01.prefab:1af50576ef843ac4c9beced8352a944f");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_02.prefab:83b4ff029ac96624db2fb876dc62e1a0");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_01.prefab:d019d84d20d774241a595a15d77d772d");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_02.prefab:e92d4ed0cd41b1b49b5202aa3e369d96");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_01.prefab:8f650589f3810a04594f72e57616c44f");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_02.prefab:5f9aa2f0ba4181b4db8b6103c3a7b9f5");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_03.prefab:7df11d6343016f943996e52018726c67");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialBrann_01.prefab:291ad5302e29fbb4e8ed46cf2a6b1b34");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialElise_01.prefab:47f6cea5ca369124babc1e1e2dfbf939");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialFinley_01.prefab:aa27b9060a0e0de45917e4fd3bd41664");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialReno_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialReno_01.prefab:d6bd5abdee2df76459b0216498e47bbf");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange1_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange1_01.prefab:b05667254eba01544ae2bdc8112a3c23");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange2_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange2_01.prefab:8b80efed8d6e46f4b87f3aebce1c1e6e");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerBlatantDecoy_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerBlatantDecoy_01.prefab:3927e0bafb038234ea1f2503ce924548");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMassHysteria_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMassHysteria_01.prefab:a02a2ca93246838488dc559a16b93903");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMindVision_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMindVision_01.prefab:66888c0ae26c2f04596b8e9697c36e9a");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMoguCultist_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMoguCultist_01.prefab:1f3d8c649412af94bb1f2df5dae07f0a");

	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerPlagueofMadness_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerPlagueofMadness_01.prefab:2a8fe3d79d4cc8046af64f8ca3c76dce");

	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Reno_Goes_Mad_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Reno_Goes_Mad_01.prefab:fcba06b37e7f52243990b6c23649d5fe");

	private static readonly AssetReference Tombs_of_Terror_Elise_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Elise_BrassRing_Quote.prefab:6e9e8faaae29f834183122d7ea9be68d");

	private List<string> m_BossPlagueOfMadness = new List<string> { VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerPlagueofMadness_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossMomentumSwing_01 };

	private List<string> m_HeroPower1Lines = new List<string> { VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_01, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_02, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_03, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_04 };

	private List<string> m_HeroPower2Lines = new List<string> { VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_01, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_02, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_03, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_04 };

	private List<string> m_HeroPower3Lines = new List<string> { VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_05 };

	private List<string> m_IdleLines = new List<string>
	{
		VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05,
		VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05, VO_ULDA_BOSS_38h_Male_PlagueLord_IdleSpecial_01
	};

	private List<string> m_IntroLines = new List<string> { VO_ULDA_BOSS_38h_Male_PlagueLord_IntroGeneric_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroFirstEncounter_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_38h_Male_PlagueLord_BossMomentumSwing_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerCleverDisguise_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerDevourMind_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerMindControl_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerPlagueofMadness_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerShadowMadness_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSpreadingMadness_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSurrendertoMadness_01, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerWastewanderSapper_01, VO_ULDA_BOSS_38h_Male_PlagueLord_DeathALT_01,
			VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_01, VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_02, VO_ULDA_BOSS_38h_Male_PlagueLord_DefeatPlayer_03, VO_ULDA_BOSS_38h_Male_PlagueLord_EmoteResponse_01, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_01, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_02, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_03, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower1_04, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_01, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_02,
			VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_03, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower2_04, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_38h_Male_PlagueLord_HeroPower3_05, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_01, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_02, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_03,
			VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_38h_Male_PlagueLord_Idle_05, VO_ULDA_BOSS_38h_Male_PlagueLord_IdleSpecial_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroFirstEncounter_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroGeneric_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_02, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_02,
			VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_02, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_03, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialBrann_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialElise_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialFinley_01, VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialReno_01, VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange1_01, VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange2_01, VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerBlatantDecoy_01,
			VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMassHysteria_01, VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMindVision_01, VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMoguCultist_01, VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerPlagueofMadness_01, VO_ULDA_Elise_Female_NightElf_STORY_Reno_Goes_Mad_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_ULDA_BOSS_38h_Male_PlagueLord_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_38h_Male_PlagueLord_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Gameplay.Get().StartCoroutine(PlayMultipleVOLinesForEmotes(emoteType, emoteSpell));
	}

	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string playerHeroCardID = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		int currentHealth = GameState.Get().GetOpposingSidePlayer().GetCurrentHealth();
		if (emoteType == EmoteType.START)
		{
			if (currentHealth == 300)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1_01);
			}
			if (currentHealth <= 299 && currentHealth >= 201)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase1Retry_02);
			}
			if (currentHealth <= 200 && currentHealth >= 101)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase2Retry_02);
			}
			if (currentHealth <= 100 && currentHealth >= 0)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_02);
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroPhase3Retry_03);
			}
			if (playerHeroCardID == "ULDA_Reno")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialReno_01);
			}
			if (playerHeroCardID == "ULDA_Brann")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialBrann_01);
			}
			if (playerHeroCardID == "ULDA_Elise")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialElise_01);
			}
			if (playerHeroCardID == "ULDA_Finley")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_38h_Male_PlagueLord_IntroSpecialFinley_01);
			}
			m_introLine = PopRandomLine(m_IntroLines);
			yield return PlayBossLine(enemyActor, m_introLine);
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (playerHeroCardID == "ULDA_Reno")
			{
				yield return PlayLineOnlyOnce(Tombs_of_Terror_Elise_BrassRing_Quote, VO_ULDA_Elise_Female_NightElf_STORY_Reno_Goes_Mad_01);
			}
			else
			{
				yield return PlayBossLine(enemyActor, m_standardEmoteResponseLine);
			}
			yield return null;
		}
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
		switch (missionEvent)
		{
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower1Lines);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower2Lines);
			break;
		case 105:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower3Lines);
			break;
		case 106:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_IdleSpecial_01);
			break;
		case 101:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange1_01);
			break;
		case 102:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_PhaseChange2_01);
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
			case "ULD_706":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerBlatantDecoy_01);
				break;
			case "TRL_258":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMassHysteria_01);
				break;
			case "CS2_003":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMindVision_01);
				break;
			case "ULD_705":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerMoguCultist_01);
				break;
			case "ULD_715":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_PlayerTriggerPlagueofMadness_01);
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
			case "ULD_328":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerCleverDisguise_01);
				break;
			case "ICC_207":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerDevourMind_01);
				break;
			case "CS1_113":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerMindControl_01);
				break;
			case "ULD_715":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossPlagueOfMadness);
				break;
			case "EX1_334":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerShadowMadness_01);
				break;
			case "OG_116":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSpreadingMadness_01);
				break;
			case "TRL_500":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerSurrendertoMadness_01);
				break;
			case "ULD_280":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_38h_Male_PlagueLord_BossTriggerWastewanderSapper_01);
				break;
			}
		}
	}
}
