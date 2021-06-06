using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGA_Evil_Fight_10 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_10_Turn_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_10_Turn_03_01.prefab:1a76bb04f0b5c90428f979c7a36fdb18");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Death_George_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Death_George_01.prefab:a62bfc220208c054c9818ca6a22f3f61");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_01_01.prefab:0bbb473ccf08c77469198f3a57007ac4");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_02_01.prefab:ec6ff6ef08206ca4f96a55098d744707");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_03_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_03_01.prefab:82c8845ca47801f438425b165aec30f4");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_George_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_George_01.prefab:d39765fbe184f8f43bc753f9f8508b01");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_George_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_George_01.prefab:4b3ed9dfe0ad6ee439815e780359f417");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Karl_Dies_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Karl_Dies_01.prefab:3d2c93b6491fa8f46bf191639739dc24");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossAttack_George_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossAttack_George_01.prefab:e88cdbc8792372d4199bd74014d332c3");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStart_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStart_01_01.prefab:dc04bd8bdd724cc43af01121352c92e4");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStartHeroic_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStartHeroic_01_01.prefab:cb084b7a87de7af49b6867944a212f88");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_EmoteResponse_George_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_EmoteResponse_George_01.prefab:0727589b0e2cdc04a934823cd0051b1b");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_01_01.prefab:57c964a0738e8b24b8be0f0f35ac36e1");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_02_01.prefab:d1fb32fbde218e94582369de527fa6ff");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_01_01.prefab:9ac8a7281e06b3649922677aaf147703");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_02_01.prefab:4eab5896ccea9324c9764b06c804a343");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_03_01.prefab:e966c427e58f5ae468b0286e20fcc0ae");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_04_01.prefab:3f7f113d6b46fda4ab610f5a66129eb5");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_05_01.prefab:e4fbfcf4b70c47743881de14690745ba");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_01_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_01_01.prefab:bc3da3275c25c5e439709a00565e34d6");

	private static readonly AssetReference VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_02_01 = new AssetReference("VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_02_01.prefab:8a8f215823a69f74cabf01fc48b6d9ab");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_Death_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_Death_Karl_01.prefab:2f5f773d469f39948a4919159132ca40");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_GeorgeDies_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_GeorgeDies_01.prefab:eefe3075acc27b642a89128615573812");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01.prefab:038cf01d36976394cb45c66da9ba7fa5");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01.prefab:77abd879f3c394643940e0227ffeadbd");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_01_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_01_01.prefab:e878544e8c9b15d41a4907885483b942");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_02_01.prefab:4c50321ebaeb2a24ba4c077dc6f86108");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_03_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_03_01.prefab:6ab4a3cf5711ebd4ba440b67559448d8");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossAttack_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossAttack_Karl_01.prefab:337ea46f1d0d63a4f9fe31ca7c3f427f");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStart_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStart_02_01.prefab:4087e874ae2d74c419b2b4a9ef0dc3a5");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStartHeroic_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStartHeroic_02_01.prefab:0f75923a791d4984eb2b4161a4915e11");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_EmoteResponse_Karl_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_EmoteResponse_Karl_01.prefab:70eb6fb0515207f4a8823036b8cde803");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_01_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_01_01.prefab:7c09562f58c6167418400bc5d8e0efb9");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_02_01.prefab:1198bf78270c25846951383068559b3d");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_LeagueOfExplorers_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_LeagueOfExplorers_01.prefab:1597c19f3860a4b4bb5bf4852af58270");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_01_01.prefab:3fecf4d3a6d75474dbda7a167aea1942");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_02_01.prefab:5e7161726b9dca449ae6264d2920ecc3");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_03_01.prefab:27f2ea84128c1804596941fae02c2eba");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_04_01.prefab:6389ee6b308b29944bc19f0da2062ece");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_05_01.prefab:49a2b1f6f32d81a45852e3119f7be628");

	private static readonly AssetReference VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01.prefab:a91332acb6b06194991aeb212229d743");

	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_01_01.prefab:9429f118ec9bad944863e8115b4438cb");

	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_02_01.prefab:42e12dde630a3c546ba23495fec5814f");

	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_EmoteResponseALT_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_EmoteResponseALT_01.prefab:385373b8b872ad64aa124582667abfb4");

	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_01_01.prefab:717a16cf3d92ba54fa9c29a13e476438");

	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_02_01.prefab:6337c3d0e2a1ccb4a899c038404c291b");

	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_03_01.prefab:95827e1c461408e418ee1d4212cf4494");

	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_04_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_04_01.prefab:e2a44f5e0bc0cf04b908bd03c7315820");

	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_05_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_05_01.prefab:6704a51ceedf5cc459e5e86fddb993a9");

	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_06_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_06_01.prefab:853ccc4d07a089944a3c2016a324e718");

	private static readonly AssetReference VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Transform_01 = new AssetReference("VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Transform_01.prefab:47062c53e13db26459bd48fb9b6bc44c");

	private List<string> m_missionEventHeroPowerGeorge = new List<string> { VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_George_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_George_01 };

	private List<string> m_KarlBossPlaysHighFive = new List<string> { VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_04_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_02_01 };

	private List<string> m_StartOfTurnKarlAfterCartBurned = new List<string> { VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_01_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_02_01 };

	private List<string> m_StartOfTurnGeorgeAfterCartBurned = new List<string> { VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_01_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_02_01 };

	private List<string> m_missionEventHeroPowerKarl = new List<string> { VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01 };

	private static List<string> m_VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdleLines = new List<string> { VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_01_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_02_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_03_01 };

	private List<string> m_GeorgeBossIdleLinesCopy = new List<string>(m_VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdleLines);

	private static List<string> m_VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdleLines = new List<string> { VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_01_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_02_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_03_01 };

	private List<string> m_KarlBossIdleLinesCopy = new List<string>(m_VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdleLines);

	private List<string> m_VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Elemental_HeroPowerLines = new List<string> { VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_01_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_02_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_DragonForm_HeroPowerLines = new List<string> { VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_04_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_05_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_06_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_10_Turn_03_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Death_George_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_01_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_02_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdle_03_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_George_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_George_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Karl_Dies_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossAttack_George_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStart_01_01,
			VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStartHeroic_01_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_EmoteResponse_George_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_01_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Carts_02_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_01_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_02_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_03_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_04_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_05_01, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_01_01,
			VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_02_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_Death_Karl_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_GeorgeDies_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_01_Karl_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_HeroPower_02_Karl_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_01_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_02_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdle_03_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossAttack_Karl_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStart_02_01,
			VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStartHeroic_02_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_EmoteResponse_Karl_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_01_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Carts_02_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_LeagueOfExplorers_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_01_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_02_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_03_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_04_01, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_05_01,
			VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_01_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_02_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_EmoteResponseALT_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_01_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_02_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_03_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_04_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_05_01, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_HeroPower_06_01,
			VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Transform_01
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
		m_deathLine = VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Death_George_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Gameplay.Get().StartCoroutine(PlayMultipleVOLinesForEmotes(emoteType, emoteSpell));
	}

	protected IEnumerator PlayMultipleVOLinesForEmotes(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStart_01_01);
				GameState.Get().SetBusy(busy: false);
			}
			if (m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossStartHeroic_01_01);
				GameState.Get().SetBusy(busy: false);
			}
			if (!m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStart_02_01);
				GameState.Get().SetBusy(busy: false);
			}
			if (m_Heroic)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossStartHeroic_02_01);
				GameState.Get().SetBusy(busy: false);
			}
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCardId() == "DRGA_BOSS_11h2")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_EmoteResponse_George_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_EmoteResponse_Karl_01, Notification.SpeechBubbleDirection.TopRight, enemyActor));
			}
		}
		yield return null;
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Death_George_01);
			m_deathLine = VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_Death_Karl_01;
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_Karl_Dies_01);
			break;
		case 103:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_BossAttack_George_01);
			break;
		case 104:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_StartOfTurnGeorgeAfterCartBurned);
			break;
		case 106:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_02_01);
			break;
		case 109:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_05_01);
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_05_01);
			break;
		case 110:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_01_01);
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_02_01);
			break;
		case 112:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_Death_Karl_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 113:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_GeorgeDies_01);
			break;
		case 114:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_BossAttack_Karl_01);
			break;
		case 115:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_StartOfTurnKarlAfterCartBurned);
			break;
		case 117:
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(DRGA_Dungeon.RafaamBrassRing, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_10_Turn_03_01);
			}
			break;
		case 118:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_01_01);
			}
			break;
		case 119:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Misc_02_01);
			}
			break;
		case 120:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(actor, VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Transform_01);
			}
			break;
		case 122:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_missionEventHeroPowerKarl);
			break;
		case 126:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_04_01);
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_04_01);
			break;
		case 128:
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01);
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_06_01);
			break;
		case 129:
			if (!m_Heroic)
			{
				yield return PlayLineInOrderOnce(actor, m_VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_Elemental_HeroPowerLines);
			}
			break;
		case 130:
			if (!m_Heroic)
			{
				yield return PlayLineInOrderOnce(actor, m_VO_DRGA_BOSS_16h_Male_Elemental_Evil_Fight_10_Rakanishu_DragonForm_HeroPowerLines);
			}
			break;
		case 151:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_missionEventHeroPowerGeorge);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "ULD_139":
		case "ULD_500":
		case "ULD_156":
		case "ULD_238":
			if (m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_LeagueOfExplorers_01);
			}
			break;
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		int num = Random.Range(1, 4);
		if (!(cardId == "DRGA_BOSS_11t"))
		{
			if (cardId == "DRGA_BOSS_12t")
			{
				if (num <= 2)
				{
					yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_KarlBossPlaysHighFive);
					yield break;
				}
				yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_01_01);
				yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_01_01);
			}
		}
		else if (GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId() == "DRGA_BOSS_16h2")
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_01_01);
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_PostTransform_02_01);
			GameState.Get().SetBusy(busy: false);
		}
		else
		{
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_George_Misc_01_01);
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Karl_Misc_03_01);
		}
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
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		float thinkEmoteBossThinkChancePercentage = GetThinkEmoteBossThinkChancePercentage();
		float num = Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			if (cardId == "DRGA_BOSS_11h2")
			{
				string line = PopRandomLine(m_GeorgeBossIdleLinesCopy);
				if (m_GeorgeBossIdleLinesCopy.Count == 0)
				{
					m_GeorgeBossIdleLinesCopy = new List<string>(m_VO_DRGA_BOSS_11h_Male_Human_Evil_Fight_10_Boss_GeorgeIdleLines);
				}
				Gameplay.Get().StartCoroutine(PlayBossLine(actor, line));
			}
			else
			{
				string line2 = PopRandomLine(m_KarlBossIdleLinesCopy);
				if (m_KarlBossIdleLinesCopy.Count == 0)
				{
					m_KarlBossIdleLinesCopy = new List<string>(m_VO_DRGA_BOSS_12h_Male_Human_Evil_Fight_10_Boss_KarlIdleLines);
				}
				Gameplay.Get().StartCoroutine(PlayBossLine(actor, line2));
			}
		}
		else
		{
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
	}
}
