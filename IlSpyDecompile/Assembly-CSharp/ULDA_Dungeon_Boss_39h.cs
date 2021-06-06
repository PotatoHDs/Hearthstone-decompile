using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_39h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossMomentumSwing_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossMomentumSwing_01.prefab:da9884341b1638b46971464ee387db5e");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocDeath_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocDeath_01.prefab:b15ee32cbbdb2e74a93e96a07037ff6b");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_01.prefab:3783a747e2f3fb04caa43b869108a256");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_02.prefab:71c06c76b13dd6e428b2e7d9f3e7e117");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerCrushingHand_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerCrushingHand_01.prefab:dcf4773ad1f532e49b50ef4ea760d1cf");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerFishflinger_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerFishflinger_01.prefab:6da9cddd8a5566e40ab02cc659ffab09");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerMurmy_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerMurmy_01.prefab:edd1e34b9ad4d1b4c8648ef04b9ba69a");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1CallintheFinishers_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1CallintheFinishers_01.prefab:bb69b6e6c0ed6e34f8cc6d55b2382589");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1EveryfinisAwesome_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1EveryfinisAwesome_01.prefab:e13b3999c6b45c34db84cea507f64d47");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1TiptheScales_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1TiptheScales_01.prefab:b8e22f75dbe04784b8f71f781c19b056");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_DeathALT_01.prefab:25d296dd91490a543bda494968932878");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_01.prefab:eca09c20f227f6c4ca46023c27764358");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_02.prefab:9595c421f29db7546892d613bc8dd428");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_03.prefab:36a111d57df822c4183603d7dc735d01");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_EmoteResponse_01.prefab:1573e6c973abae94da58885e8fa1064e");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_01.prefab:8914017a8dd19594bacfa1d6d36c56ea");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_02.prefab:abae716f707c8ff42beabd11387378ff");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_03.prefab:33aa503c2fcb7854d867dbc53df4004a");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_04 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_04.prefab:e5a7425c7e8c73f4d96c2a6dfa03ff2c");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_01.prefab:e644253664aab484bb4b2ac8446aa0a2");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_02.prefab:74ce6f0660d883748a55db776d32c379");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_03.prefab:689f6bac2a9d0b440a73e7d05999f18e");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_04 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_04.prefab:ab4b6ee54def71746bb2d6dff703dae0");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01.prefab:87ca2ec24b309ed49884e7693ff7c2bb");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02.prefab:6d583948cab9a594bb4a141138ca1467");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03.prefab:ed61fabaebfc2a14fb4edd6925733f8a");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04.prefab:66c34dbc4499f914193df40f3d1a9d54");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3Rare_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3Rare_01.prefab:896c87c3a1a12da45a09f94a82366eab");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01.prefab:09176d46c48be4947946551fe9ca7648");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02.prefab:af3f4f7d407051744bc2291d33368c7b");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03.prefab:3b8e0fb897c8f7b46b3b71716909ac98");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04.prefab:92a8a57e9faa56b4bb5eb3085434d18f");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05.prefab:afbab207bfcdc4948a6db5b06a6d7e9c");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IdleRare_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IdleRare_01.prefab:3e0b6de0af4327d4692a63b9809747c8");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IdleSpecialRare_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IdleSpecialRare_01.prefab:82b64ebace0bf254787027cca76578d1");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroFirstEncounter_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroFirstEncounter_01.prefab:4d9e92da5ada0254b8770372cf5f69d0");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroGeneric_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroGeneric_01.prefab:e33678a0d53b35847bf1650c6ec69898");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1_01.prefab:32e580df3f08b3a4894ef59e4acdc775");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_01.prefab:974bf1d807287ee4c925259c0c2332b3");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_02.prefab:089d1dd5b1a8d0e4897adfea38097949");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2_01.prefab:c4dd8b13696a0d846b320e7e0d58e4c9");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_01.prefab:aa4ae4ae89e5ed145b550ae73dca2151");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_02.prefab:92d94455f92a40441a699792d5e3a52a");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3_01.prefab:d5948a6c0fa76ea4da748faaebf8bff5");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_01.prefab:b6ea729de86c4c943b706f63ef80ee29");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_02.prefab:5ff7a1da9befa154686263fc29c772d4");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_03.prefab:cd683052bde0d0d4db377649312e90d1");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialBrann_01.prefab:28a33199a9cff654a9e616631f2ea6c0");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialElise_01.prefab:fcd5f7b9d7f251841975183f92fee39c");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialFinley_01.prefab:5ddea0f86c8cc844e968fc1e51873858");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialReno_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialReno_01.prefab:130593c1d635dba4e9ae248665a39fcb");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange1_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange1_01.prefab:920c844b64972c54692931172d2c44f1");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange2_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange2_01.prefab:b90b4009ed0a5aa49a2acadad945ebc9");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerHungryCrab_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerHungryCrab_01.prefab:e623dfe81ce0c8c4cb6ecdc26e60e2d7");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerMurloc_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerMurloc_01.prefab:82f0d3c9f1b00024db90aec9684d53f7");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlague_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlague_01.prefab:6391a384b794aae49b718ac9e08f6f90");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlagueofMurlocs_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlagueofMurlocs_01.prefab:fab6117247fc2d94aacd06d59f643ac5");

	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerZephrys_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerZephrys_01.prefab:e606d109e460e1448bcad2d2b2d51268");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_BossVeshPhase3HeroPower_01 = new AssetReference("VO_ULDA_Reno_Male_Human_BossVeshPhase3HeroPower_01.prefab:9d0aecc4a40adde4d8f4688fdd0bbbc8");

	private List<string> m_BossMurlocNoiseLines = new List<string> { VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_02 };

	private List<string> m_HeroPower1TriggerLines = new List<string> { VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_01, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_02, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_03, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_04 };

	private List<string> m_HeroPower2Lines = new List<string> { VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_01, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_02, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_03, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_04 };

	private List<string> m_HeroPower3Lines = new List<string>
	{
		VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02,
		VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3Rare_01
	};

	private List<string> m_IdleLines = new List<string>
	{
		VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05,
		VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05,
		VO_ULDA_BOSS_39h_Male_PlagueLord_IdleRare_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IdleRare_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IdleSpecialRare_01
	};

	private List<string> m_IntroLines = new List<string> { VO_ULDA_BOSS_39h_Male_PlagueLord_IntroGeneric_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroFirstEncounter_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_39h_Male_PlagueLord_BossMomentumSwing_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocDeath_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossMurlocNoise_02, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerCrushingHand_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerFishflinger_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerMurmy_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1CallintheFinishers_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1EveryfinisAwesome_01, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1TiptheScales_01,
			VO_ULDA_BOSS_39h_Male_PlagueLord_DeathALT_01, VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_01, VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_02, VO_ULDA_BOSS_39h_Male_PlagueLord_DefeatPlayer_03, VO_ULDA_BOSS_39h_Male_PlagueLord_EmoteResponse_01, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_01, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_02, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_03, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower1Trigger_04, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_01,
			VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_02, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_03, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower2_04, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_39h_Male_PlagueLord_HeroPower3Rare_01, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_01, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_02,
			VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_03, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_04, VO_ULDA_BOSS_39h_Male_PlagueLord_Idle_05, VO_ULDA_BOSS_39h_Male_PlagueLord_IdleRare_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IdleSpecialRare_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroFirstEncounter_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroGeneric_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_02,
			VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_02, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_02, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_03, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialBrann_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialElise_01, VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialFinley_01,
			VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialReno_01, VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange1_01, VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange2_01, VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerHungryCrab_01, VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerMurloc_01, VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlague_01, VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlagueofMurlocs_01, VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerZephrys_01, VO_ULDA_Reno_Male_Human_BossVeshPhase3HeroPower_01
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
		m_deathLine = VO_ULDA_BOSS_39h_Male_PlagueLord_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_39h_Male_PlagueLord_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		int currentHealth = GameState.Get().GetOpposingSidePlayer().GetCurrentHealth();
		if (emoteType == EmoteType.START)
		{
			if (currentHealth == 300)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1_01);
			}
			if (currentHealth <= 299 && currentHealth >= 201)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase1Retry_02);
			}
			if (currentHealth <= 200 && currentHealth >= 101)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2Retry_02);
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase2_01);
			}
			if (currentHealth <= 100 && currentHealth >= 0)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_02);
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3Retry_03);
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroPhase3_01);
			}
			if (cardId == "ULDA_Reno")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialReno_01);
			}
			if (cardId == "ULDA_Brann")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialBrann_01);
			}
			if (cardId == "ULDA_Elise")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialElise_01);
			}
			if (cardId == "ULDA_Finley")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_39h_Male_PlagueLord_IntroSpecialFinley_01);
			}
			m_introLine = PopRandomLine(m_IntroLines);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 110:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_BossMomentumSwing_01);
			break;
		case 111:
			yield return PlayRandomLineAlways(actor, m_BossMurlocNoiseLines);
			break;
		case 103:
			yield return PlayRandomLineAlways(actor, m_HeroPower1TriggerLines);
			break;
		case 104:
			yield return PlayRandomLineAlways(actor, m_HeroPower2Lines);
			break;
		case 105:
			yield return PlayRandomLineAlways(actor, m_HeroPower3Lines);
			break;
		case 101:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange1_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_PhaseChange2_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 109:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerMurloc_01);
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
			case "NEW1_017":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerHungryCrab_01);
				break;
			case "ULD_707":
			case "ULD_715":
			case "ULD_717":
			case "ULD_718":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlague_01);
				break;
			case "ULD_172":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerPlagueofMurlocs_01);
				break;
			case "ULD_003":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_PlayerTriggerZephrys_01);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "LOOT_060":
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerCrushingHand_01);
			break;
		case "ULD_289":
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerFishflinger_01);
			break;
		case "ULD_723":
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerMurmy_01);
			break;
		case "CFM_310":
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1CallintheFinishers_01);
			break;
		case "LOE_113":
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1EveryfinisAwesome_01);
			break;
		case "ULD_716":
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_39h_Male_PlagueLord_BossTriggerPhase1TiptheScales_01);
			break;
		case "ULDA_BOSS_39p3":
		case "ULDA_BOSS_39px3":
		{
			Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
				.GetActor();
			if (GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCardId() == "ULDA_Reno")
			{
				yield return PlayLineOnlyOnce(actor2, VO_ULDA_Reno_Male_Human_BossVeshPhase3HeroPower_01);
			}
			break;
		}
		}
	}
}
