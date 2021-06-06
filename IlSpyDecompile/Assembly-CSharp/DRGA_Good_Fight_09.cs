using System.Collections;
using System.Collections.Generic;

public class DRGA_Good_Fight_09 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_15h_Male_Orc_Good_Fight_09_Kragg_EnterBattle_01 = new AssetReference("VO_DRGA_BOSS_15h_Male_Orc_Good_Fight_09_Kragg_EnterBattle_01.prefab:fffb0fc6a0990274d9dd7d7c1d4b20ab");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Attack_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Attack_01.prefab:b537043b22481b54bb4869e1fcc838df");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Concede_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Concede_01.prefab:0b5e7e72d9971ab4f9b3f5b16fc6f120");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Death_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Death_01.prefab:8b8fcd0b96015c94c881060522820b6a");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Greetings_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Greetings_01.prefab:82b4e2f8a4281a243b6f64359d6e7757");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Oops_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Oops_01.prefab:ac431971d8cf69f409d39bc1ddf7ea4a");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Thanks_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Thanks_01.prefab:1b2ddc06a2175044a82776fee162bf8a");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Threaten_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Threaten_01.prefab:d05926cebee474b449781e2dbfaafa28");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_WellPlayed_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_WellPlayed_01.prefab:45f531deb0246f24b878ff9740169ed5");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Wow_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Wow_01.prefab:189de760d67059e4fbb43f98c812f9c1");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_EnemyLowHealth_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_EnemyLowHealth_01.prefab:0ca0a87affc483f4393b8a6f6b7f9a1d");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Error_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Error_01.prefab:65320b93100d105448a88f7f1f29b65f");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_01_01.prefab:373a5792fae830747b1f713ff85e2ed0");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_02_01.prefab:d97d4dadbf61ea44d965351ae0d682bc");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_03_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_03_01.prefab:27075f1bd1ccc81478e2435a527d30b6");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_GreaseMonkey_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_GreaseMonkey_01.prefab:b25c61d94155d9a43ab65b15e8f3aee4");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01.prefab:23f2fb8953fe11e48a76432fc517c83a");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01.prefab:062631af973847c46b0c110530af2373");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01.prefab:773cc7e2beadfad4f9aa5ae0c2c5d212");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Kragg_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Kragg_01.prefab:c8aaec728e8c8ec44b08272061a5ec46");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Low_Cards_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Low_Cards_01.prefab:6889044179f5cae448ba12a43302b5ba");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_01_01.prefab:723ac94ba447ea840b17cf1def17e46d");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_02_01.prefab:1c6ddf6b9176f9e49a77dadbd9018651");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_MissilePod_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_MissilePod_01.prefab:81d40d931d7c96e44a41f79f16397de9");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Out_Of_Cards_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Out_Of_Cards_01.prefab:ee8a53887728b794fb589c2b317ecca5");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_01_01.prefab:4d4ca3da54d83b1439a914c13234a944");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_02_01.prefab:f100a8f2cd83edd4c956b277774150c9");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_03_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_03_01.prefab:cb0d4530ac229e347b55868b28b51191");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_01_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_01_01.prefab:c10978340f3c10e4cb1e4dbfefab42e0");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_02_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_02_01.prefab:2c831508d53e2084a939bd03978f4c33");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_03_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_03_01.prefab:8de6b414ab27129449f83f7b22dd3cf3");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Time_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Time_01.prefab:85f5f6b7eff5b574fa0f6c6691687685");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_UpgradedCannon_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_UpgradedCannon_01.prefab:01b82109fdd401f4fb9b6627dc1ba2bb");

	private static readonly AssetReference VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_PlayerStart_01.prefab:2db1f376091d2594c84a99002b627d09");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_BombFlinger_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_BombFlinger_01.prefab:d39d02036316ed94a853ff58e82148fb");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_Death_01.prefab:8dc20268b81bb7040b046f92ef541261");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_DeathRay_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_DeathRay_01.prefab:15203c1471c631047afe96a822d41cb7");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_EnemyLowHealth_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_EnemyLowHealth_01.prefab:ed335347636b8a2458caa731e7db0d86");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_01_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_01_01.prefab:40b7977c46ca8dd48b3a2f126777a68f");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_02_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_02_01.prefab:c8ab749bed9d80241b974b7ed04cab3e");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_03_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_03_01.prefab:caaff3a1747dd0144b61c2dc05ceaee1");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_01_01.prefab:62783d55b1b3aee4b9bfe8c93ec0310a");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_02_01.prefab:b9a4c7d6ea936cd428ef6f406d8f969c");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_03_01.prefab:3b4e817a2e8e01b46b092b28a5d8b03c");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_01_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_01_01.prefab:ef9bc59c0fa2caa45b5301bcd26391d9");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_02_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_02_01.prefab:658a78813f902224684d56b83f7d83ad");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_03_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_03_01.prefab:46a491a104d9cfa4cb94268218e3281c");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_01_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_01_01.prefab:b04c10bd2c1cfa045ad7d5511c876791");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_02_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_02_01.prefab:0b328fd528e774f489c905614f84b04b");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_03_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_03_01.prefab:31bb8ed9e031fee419f587d484d0ba67");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_RocketLauncher_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_RocketLauncher_01.prefab:dcdb925deb73472489ba55aa8bd55628");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_UpgradedCannon_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_UpgradedCannon_01.prefab:2eb50fc4cefd7ef44990f71da845e0d7");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossAttack_01.prefab:06e5246839b69464d99eeea355d114b8");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossStart_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossStart_01.prefab:c6e85f2163a8be244aadb4a7ba2d7193");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_EmoteResponse_01.prefab:6760369615a9b7a46833491567ddcb1d");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_01_01.prefab:d6dd416cddcef4844a28b816a8e59058");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_02_01.prefab:5e218b92c5c4bc84694bf1b741096f6f");

	private static readonly AssetReference VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_03_01.prefab:725829b038db8544f97c955fd7f771ec");

	private List<string> m_missionEventTrigger102Lines = new List<string> { VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_01_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_02_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_03_01 };

	private List<string> m_BossPlaysCannon = new List<string> { VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_01_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_02_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_03_01 };

	private List<string> m_PlayerPlaysCannon = new List<string> { VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_01_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_02_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_03_01 };

	private List<string> m_VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannonLines = new List<string> { VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_01_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_02_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_03_01 };

	private List<string> m_VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPowerLines = new List<string> { VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannonLines = new List<string> { VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_01_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_02_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_03_01 };

	private List<string> m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPowerLines = new List<string> { VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_01_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_02_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_03_01 };

	private List<string> m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_IdleLines = new List<string> { VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_01_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_02_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_03_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_15h_Male_Orc_Good_Fight_09_Kragg_EnterBattle_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Attack_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Concede_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Death_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Greetings_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Oops_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Thanks_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Threaten_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_WellPlayed_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Emote_Wow_01,
			VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_EnemyLowHealth_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Error_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_01_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_02_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannon_03_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_GreaseMonkey_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_01_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_02_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPower_03_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Kragg_01,
			VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Low_Cards_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_01_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_02_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_MissilePod_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Out_Of_Cards_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_01_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_02_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_PlayCannon_03_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_01_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_02_01,
			VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Thinking_03_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Time_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_UpgradedCannon_01, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_PlayerStart_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_BombFlinger_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_Death_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_DeathRay_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_EnemyLowHealth_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_01_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_02_01,
			VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannon_03_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_01_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_02_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPower_03_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_01_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_02_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_LowHealth_03_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_01_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_02_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_PlayCannon_03_01,
			VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_RocketLauncher_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_UpgradedCannon_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossAttack_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossStart_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_EmoteResponse_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_01_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_02_01, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Idle_03_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 102:
			yield return PlayLineInOrderOnce(enemyActor, m_missionEventTrigger102Lines);
			break;
		case 104:
			yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_BossAttack_01);
			break;
		case 105:
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("YOD_038"), null, VO_DRGA_BOSS_15h_Male_Orc_Good_Fight_09_Kragg_EnterBattle_01);
			break;
		case 106:
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_01_01);
			}
			break;
		case 107:
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_LowHealth_02_01);
				yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_EnemyLowHealth_01);
			}
			break;
		case 108:
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_HeroPowerLines);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "DRGA_BOSS_30t7":
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_FireCannonLines);
			}
			break;
		case "DRGA_BOSS_30t6":
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_GreaseMonkey_01);
			}
			break;
		case "AT_070":
		case "YOD_038":
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_Kragg_01);
			}
			break;
		case "DRGA_BOSS_30t3":
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_MissilePod_01);
			}
			break;
		case "DRGA_BOSS_30t5":
			if (!m_Heroic)
			{
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_30h_Male_Human_Good_Fight_09_Hero_UpgradedCannon_01);
			}
			break;
		case "DRGA_BOSS_30t":
		case "DRGA_BOSS_30t2":
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_PlayerPlaysCannon);
			}
			break;
		case "DRGA_BOSS_15t2":
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
			case "DRGA_BOSS_30t4":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_BombFlinger_01);
				break;
			case "DRGA_BOSS_31t":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_DeathRay_01);
				break;
			case "DRGA_BOSS_30t7":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_FireCannonLines);
				break;
			case "DRGA_BOSS_30t2":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_RocketLauncher_01);
				break;
			case "DRGA_BOSS_30t5":
				yield return PlayLineOnlyOnce(actor, VO_DRGA_BOSS_31h_Female_Vupera_Good_Fight_09_Boss_UpgradedCannon_01);
				break;
			case "DRGA_BOSS_30t":
				yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_BossPlaysCannon);
				break;
			}
		}
	}
}
