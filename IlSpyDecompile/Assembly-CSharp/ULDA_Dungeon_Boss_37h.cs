using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_37h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossMomentumSwing_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossMomentumSwing_01.prefab:30efd0cbba230e34c8321e2c0b19c90a");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerDOOM_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerDOOM_01.prefab:767ddf9dc82dbee49a94936a7b4cfe64");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerForbiddenWords_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerForbiddenWords_01.prefab:4b08d65c6439d0341a503275d3efba4e");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPlagueofDeath_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPlagueofDeath_01.prefab:785e26c52f9e56945b9026d2838e9ce4");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPsychopomp_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPsychopomp_01.prefab:1f276a3f90f33584f95aaa4f63ad684d");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowofDeath_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowofDeath_01.prefab:5e05c5680cabe304bb57dbbc57edfd22");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordDeath_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordDeath_01.prefab:04aa2bb88a833fc42a8c4569da461fc4");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordPain_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordPain_01.prefab:44e81f1418cff0545aa0d3c7df362468");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossUniqueCard_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossUniqueCard_01.prefab:dafca73a199865348b910fca55d6a2c6");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_DeathALT_01.prefab:5c437d90222c27647ba633213d372ff9");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_01.prefab:cebb8db8ec54e3b4ca079e77ec30e3c9");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_02.prefab:f4e296a15892b8f4597cdbc4bed24d8d");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_03.prefab:ac4b796862c48494183327fa71a924e6");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_EmoteResponse_01.prefab:6bf90107695a95b41853415b8fd7ec65");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_01.prefab:21abf67e3c956eb489160b264fc4d580");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_02.prefab:652d00656c21a7b4b8e1a6512b682895");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_03.prefab:e4072d24bdbae9443ad745518ea734c5");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_04 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_04.prefab:c1397d7066f453f4abdb25656e0f0d94");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_05 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_05.prefab:de616f2d19949184e8676e1d858b9aa6");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_01.prefab:8c1d07c04936a72458c0a98a5f0f899d");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_02.prefab:36b4014a7434769459bbd1df13eca168");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_03.prefab:d2c3bace843c7ee44a9c6b66948b617b");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_04 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_04.prefab:26322c7248e1bd14f9cc5b57c01db698");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_01.prefab:2eb9651922e0fe94a8cb9f548b5243a2");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_02.prefab:b9b5455a77d065546b19f15c255f8830");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_03.prefab:81c52ebf0a96d9444ae250f7360cf01e");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_04 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_04.prefab:1b118d7ea3d7d7c439a648ba9f92342e");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_05 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_05.prefab:ccf16793723e95547bf374cee7adad3e");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01.prefab:43a0fbc639786274f9765fd8960943dc");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02.prefab:ed3d5c6013ccde54884341ff6c8133f4");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03.prefab:5c485dabe184bfe468dad99305563ea9");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04.prefab:d51ed2f37bc14644987bf9f1800fcc80");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IdleRare_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IdleRare_01.prefab:c85572f053c1d10459eea5b41816981b");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IdleSpecial_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IdleSpecial_01.prefab:01f95336e07ffd847a18acd0f864079a");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroFirstEncounter_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroFirstEncounter_01.prefab:964987766f9f20a478454836999740a0");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroGeneric_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroGeneric_01.prefab:33ac51b5728238c4d9253b6bd0c373d7");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1_01.prefab:f1a43c53d10349b4f8cb71d168686437");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_01.prefab:c9f47fb19f5238d4e8d97267fc602e70");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_02.prefab:3f8533571205a284aaf70d3111fc4bd2");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_01.prefab:3ead26d216897204889bf09275a44524");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_02.prefab:6e39b5702d29d4f428834bf83e6e6cfd");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_01.prefab:98e70f9dbc81f6140b2d5bfe6b898afa");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_02.prefab:e8af0dfb823da6340acf59b0507aadca");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_03.prefab:570fcef439125c84e8ae9a21fd89689e");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialBrann_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialBrann_01.prefab:4b3ff177c8e6af44aa0ce734369a53d4");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialElise_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialElise_01.prefab:5c6e9905abafe514cafdbd93d3c64e1e");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialFinley_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialFinley_01.prefab:c90573ecab2ec9c4ea76b1d9239cfdbf");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialReno_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialReno_01.prefab:368e6ec835b3d77408545636b752b429");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange1_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange1_01.prefab:c22a7aba538d01242add3f4fcc90d89f");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange2_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange2_01.prefab:d8de33988d71ce0419f67096fce01684");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerPlagueofDeath_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerPlagueofDeath_01.prefab:2d4b99bf7f4bf4f41a6e543ab73c1934");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerBurgle_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerBurgle_01.prefab:4722acb4877318b44adec789f6de3f57");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerLichKing_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerLichKing_01.prefab:4e568e594b94acc46b000ebf244d0c53");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerShadowWordDeath_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerShadowWordDeath_01.prefab:594d5109912d47f4188638e392f90136");

	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerSylvanas_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerSylvanas_01.prefab:ede776e90cabdf74596da56f1d9e41f4");

	private List<string> m_DefeatPlayerLines = new List<string> { VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_01, VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_02, VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_03 };

	private List<string> m_BossPlagueOfDeath = new List<string> { VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPlagueofDeath_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossMomentumSwing_01 };

	private List<string> m_IntroLines = new List<string> { VO_ULDA_BOSS_37h_Female_PlagueLord_IntroGeneric_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroFirstEncounter_01 };

	private List<string> m_HeroPower1Lines = new List<string> { VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_01, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_02, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_03, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_04, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_05 };

	private List<string> m_HeroPower2Lines = new List<string> { VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_01, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_02, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_03, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_04 };

	private List<string> m_HeroPower3Lines = new List<string> { VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_05 };

	private List<string> m_IdleLines = new List<string>
	{
		VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02,
		VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04, VO_ULDA_BOSS_37h_Female_PlagueLord_IdleRare_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IdleRare_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IdleSpecial_01
	};

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_37h_Female_PlagueLord_BossMomentumSwing_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerDOOM_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerForbiddenWords_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPlagueofDeath_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPsychopomp_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowofDeath_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordDeath_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordPain_01, VO_ULDA_BOSS_37h_Female_PlagueLord_BossUniqueCard_01, VO_ULDA_BOSS_37h_Female_PlagueLord_DeathALT_01,
			VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_01, VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_02, VO_ULDA_BOSS_37h_Female_PlagueLord_DefeatPlayer_03, VO_ULDA_BOSS_37h_Female_PlagueLord_EmoteResponse_01, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_01, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_02, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_03, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_04, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower1_05, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_01,
			VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_02, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_03, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower2_04, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_01, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_02, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_03, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_04, VO_ULDA_BOSS_37h_Female_PlagueLord_HeroPower3_05, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_01, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_02,
			VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_03, VO_ULDA_BOSS_37h_Female_PlagueLord_Idle_04, VO_ULDA_BOSS_37h_Female_PlagueLord_IdleRare_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IdleSpecial_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroFirstEncounter_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroGeneric_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_02, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_01,
			VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_02, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_02, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_03, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialBrann_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialElise_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialFinley_01, VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialReno_01, VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange1_01, VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange2_01,
			VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerPlagueofDeath_01, VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerBurgle_01, VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerLichKing_01, VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerShadowWordDeath_01, VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerSylvanas_01
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
		m_introLine = VO_ULDA_BOSS_37h_Female_PlagueLord_IntroGeneric_01;
		m_deathLine = VO_ULDA_BOSS_37h_Female_PlagueLord_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_37h_Female_PlagueLord_EmoteResponse_01;
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
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1_01);
			}
			if (currentHealth <= 299 && currentHealth >= 201)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase1Retry_02);
			}
			if (currentHealth <= 200 && currentHealth >= 101)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase2Retry_02);
			}
			if (currentHealth <= 100 && currentHealth >= 0)
			{
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_01);
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_02);
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroPhase3Retry_03);
			}
			if (cardId == "ULDA_Reno")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialReno_01);
			}
			if (cardId == "ULDA_Brann")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialBrann_01);
			}
			if (cardId == "ULDA_Elise")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialElise_01);
			}
			if (cardId == "ULDA_Finley")
			{
				m_IntroLines.Add(VO_ULDA_BOSS_37h_Female_PlagueLord_IntroSpecialFinley_01);
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
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_DefeatPlayerLines);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower1Lines);
			break;
		case 105:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower2Lines);
			break;
		case 106:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPower3Lines);
			break;
		case 101:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange1_01);
			break;
		case 102:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_PhaseChange2_01);
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
			case "ULD_718":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerPlagueofDeath_01);
				break;
			case "AT_033":
			case "EX1_182":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerBurgle_01);
				break;
			case "ICC_314":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerLichKing_01);
				break;
			case "EX1_622":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerShadowWordDeath_01);
				break;
			case "EX1_016":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_PlayerTriggerSylvanas_01);
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
			case "OG_239":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerDOOM_01);
				break;
			case "DAL_723":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerForbiddenWords_01);
				break;
			case "ULD_718":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossPlagueOfDeath);
				break;
			case "ULD_268":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerPsychopomp_01);
				break;
			case "ULD_286":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowofDeath_01);
				break;
			case "EX1_622":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordDeath_01);
				break;
			case "CS2_234":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_BossTriggerShadowWordPain_01);
				break;
			case "ULDA_BOSS_37t":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_37h_Female_PlagueLord_BossUniqueCard_01);
				break;
			}
		}
	}
}
