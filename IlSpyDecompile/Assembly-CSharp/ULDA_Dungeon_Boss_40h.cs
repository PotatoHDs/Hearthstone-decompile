using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_40h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossMomentumSwing_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossMomentumSwing_01.prefab:22128e140844f7b4898e1ddf3d15e57a");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerArmagedillo_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerArmagedillo_01.prefab:0abf25da41474e446976c96f61dc2830");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerBattleRage_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerBattleRage_01.prefab:438b5d2c9ca70ee44b6c2aa2c743a55a");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerOctosari_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerOctosari_01.prefab:f7627e1971003f04198e0cbb0367efbb");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerPlagueofWrath_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerPlagueofWrath_01.prefab:c87066e231bf2744aa9838aebc7c16a6");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerReinforcements_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerReinforcements_01.prefab:356fa7feeaed8294599051eace96f4bb");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerRestlessMummy_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerRestlessMummy_01.prefab:cf1425e7633277a4ebad4133f160c3a1");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerSulthraze_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerSulthraze_01.prefab:118d6761f62926b49b0155900f483ef1");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerTempleBerserker_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerTempleBerserker_01.prefab:e2c1d8c39b6ff9c4fba71ddbfa8a1e7e");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerWoecleaver_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerWoecleaver_01.prefab:e70f55d669c095645ad071a69005ea05");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_DeathALT_01.prefab:ff39fbfbd2a24c04986e6674e129a828");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_01.prefab:20a1cbbc116156d40af93b77ddcb478e");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_02.prefab:12d9fd05419612144b1fe78e24ef5dab");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_03.prefab:f70fe24f458ce6244b3f03c868aae174");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_EmoteResponse_01.prefab:34532f81a53839846ab836fd38cb7170");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_01.prefab:eeb5086dca7304d478862186070c18a4");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_02.prefab:60aba71588857cb4a954b3034fe37a38");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_03.prefab:0a1406b4c5c14fd4c993d2b4d0ce78ee");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_04 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_04.prefab:40a3470f4f2fcc84f8593cd190c69c11");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_01.prefab:81da2850da581a24085c8c0c577125c3");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_02.prefab:fd202243a29bacd40a3903e5a20a715b");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_03.prefab:7fccb2e3c2322a341b54550ade006cc1");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_04 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_04.prefab:b9a0404a600e1354d8922e7f8e78ba07");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_01.prefab:507b8a73fa8ac2045b68cfa5f7121383");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_02.prefab:4f57c5bdfe859594ba9c3f76d3f12783");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_03.prefab:cf37a5340dd2e0c4eb08dbd8042ff8ec");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_04 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_04.prefab:ae97d640d7d438a40af5a8df8a1d31bd");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_05 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_05.prefab:9c584c31d039c524785420463d96e60b");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01.prefab:3fab3f3875411e942adde5152ba5b618");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02.prefab:7bc91430d6b6c06499099ed6f4ab5965");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03.prefab:e42cfd929f95e4942b99bb4efe22a57e");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04.prefab:defb38b8c3d352d439fc973ffb4e010b");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05.prefab:f75eae5bbc0d6974395ba00fb62eba6f");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IdleSpecial_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IdleSpecial_01.prefab:50ac2d697cbe24c44a5851f43b75cc57");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroFirstEncounter_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroFirstEncounter_01.prefab:ced3b5eb696c6f844a639ebe12e67ffc");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroGeneric_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroGeneric_01.prefab:9e5a3e1056dba874690cdf7655cf382c");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1_01.prefab:d987ba5f7d8820a41af367654ffcf79c");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_01.prefab:ff24d15a817472f4ba70f563c66c7ade");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_02.prefab:d2719de1288b8214f9eb6d89c8420f59");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_01.prefab:03b816d4e36310049b1939cdc34031ae");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_02.prefab:822aedac072bc2f4c89729eab8915ad7");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_01.prefab:fc1d8a10ff635ed4c9017c8b2776ab55");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_02.prefab:35030c812cdcbbb428d527b301f5f65a");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_03.prefab:01b3edbf3170dd64b8fa2ae50642efa7");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann_01.prefab:158f0876a1f259c499a9085a2bb53eaf");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann2_01.prefab:29b7a9a4f9fb3d24ead189c2edcf9d8c");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise_01.prefab:d4d903f4ce480aa4a83c26ea6b5dadc8");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise2_01.prefab:cbc2af44f2b1ed743957a9d4e14075b5");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley_01.prefab:eb614d24563e0be40b026713ef5bfdc9");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley2_01.prefab:6bfd6ad5574109f4b965aca3b1a33073");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno_01.prefab:fb862538bdaeaea42b33fe2341db1376");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno2_01.prefab:b7ac8cb7bd58eae4c84f6424fbaef9a2");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange1_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange1_01.prefab:178cf0a10a501db45a1e18ea8d197c8e");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange2_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange2_01.prefab:58b7ebc4459507243bdc82c5f65d01cc");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerBeamingSidekick_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerBeamingSidekick_01.prefab:4cd5d06d4a0c16c4bbcc9037de792737");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerDesertHare_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerDesertHare_01.prefab:8fb332eca775e3b4e8d28d88f26998ab");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInjuredTolvir_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInjuredTolvir_01.prefab:1960acc178408d74098494575f9fce5a");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInnerRage_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInnerRage_01.prefab:0c2974a64a69f944e9cdff9128e5ba7b");

	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerPlagueofWrath_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerPlagueofWrath_01.prefab:c7cf27f9cea924d4a954cbeb845937d6");

	private List<string> m_HeroPower1Lines = new List<string> { VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_01, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_02, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_03, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_04 };

	private List<string> m_HeroPower2Lines = new List<string> { VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_01, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_02, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_03, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_04 };

	private List<string> m_HeroPower3Lines = new List<string> { VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_05 };

	private List<string> m_IdleLines = new List<string>
	{
		VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05,
		VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05,
		VO_ULDA_BOSS_40h_Female_PlagueLord_IdleSpecial_01
	};

	private List<string> m_IntroLines = new List<string> { VO_ULDA_BOSS_40h_Female_PlagueLord_IntroGeneric_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroFirstEncounter_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_40h_Female_PlagueLord_BossMomentumSwing_01, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerArmagedillo_01, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerBattleRage_01, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerOctosari_01, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerPlagueofWrath_01, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerReinforcements_01, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerRestlessMummy_01, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerSulthraze_01, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerTempleBerserker_01, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerWoecleaver_01,
			VO_ULDA_BOSS_40h_Female_PlagueLord_DeathALT_01, VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_01, VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_02, VO_ULDA_BOSS_40h_Female_PlagueLord_DefeatPlayer_03, VO_ULDA_BOSS_40h_Female_PlagueLord_EmoteResponse_01, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_01, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_02, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_03, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower1_04, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_01,
			VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_02, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_03, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower2_04, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_40h_Female_PlagueLord_HeroPower3_05, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_01, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_02,
			VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_03, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_04, VO_ULDA_BOSS_40h_Female_PlagueLord_Idle_05, VO_ULDA_BOSS_40h_Female_PlagueLord_IdleSpecial_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroFirstEncounter_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroGeneric_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_02, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_01,
			VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_02, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_02, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_03, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann2_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise2_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley2_01,
			VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno_01, VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno2_01, VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange1_01, VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange2_01, VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerBeamingSidekick_01, VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerDesertHare_01, VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInjuredTolvir_01, VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInnerRage_01, VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerPlagueofWrath_01
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
		m_deathLine = VO_ULDA_BOSS_40h_Female_PlagueLord_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_40h_Female_PlagueLord_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Gameplay.Get().StartCoroutine(PlayMultipleVOLinesForEmotes(emoteType, emoteSpell));
	}

	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		int currentHealth = GameState.Get().GetOpposingSidePlayer().GetCurrentHealth();
		if (emoteType == EmoteType.START)
		{
			if (currentHealth == 300)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1_01);
			}
			if (currentHealth <= 299 && currentHealth >= 201)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase1Retry_02);
			}
			if (currentHealth <= 200 && currentHealth >= 101)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase2Retry_02);
			}
			if (currentHealth <= 100 && currentHealth >= 0)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_02);
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroPhase3Retry_03);
			}
			if (cardId == "ULDA_Reno")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno_01);
			}
			if (cardId == "ULDA_Brann")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann_01);
			}
			if (cardId == "ULDA_Elise")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise_01);
			}
			if (cardId == "ULDA_Finley")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley_01);
			}
			m_introLine = PopRandomLine(m_IntroLines);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			if (m_introLine == (string)VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno_01)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialReno2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			else if (m_introLine == (string)VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann_01)
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialBrann2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			else if (m_introLine == (string)VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise_01)
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialElise2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			else if (m_introLine == (string)VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley_01)
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_40h_Female_PlagueLord_IntroSpecialFinley2_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, enemyActor));
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
		case 107:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_BossMomentumSwing_01);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower1Lines);
			break;
		case 105:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower3Lines);
			break;
		case 106:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_IdleSpecial_01);
			break;
		case 101:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange1_01);
			break;
		case 102:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_PhaseChange2_01);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower2Lines);
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
			case "ULD_191":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerBeamingSidekick_01);
				break;
			case "ULD_719":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerDesertHare_01);
				break;
			case "ULD_271":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInjuredTolvir_01);
				break;
			case "EX1_607":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerInnerRage_01);
				break;
			case "ULD_707":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_PlayerTriggerPlagueofWrath_01);
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
			case "ULD_258":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerArmagedillo_01);
				break;
			case "EX1_392":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerBattleRage_01);
				break;
			case "ULD_177":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerOctosari_01);
				break;
			case "ULD_707":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerPlagueofWrath_01);
				break;
			case "ULD_256":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerReinforcements_01);
				break;
			case "ULD_206":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerRestlessMummy_01);
				break;
			case "TRL_325":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerSulthraze_01);
				break;
			case "ULD_185":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerTempleBerserker_01);
				break;
			case "LOOT_380":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_40h_Female_PlagueLord_BossTriggerWoecleaver_01);
				break;
			}
		}
	}
}
