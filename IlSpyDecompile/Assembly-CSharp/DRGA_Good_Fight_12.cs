using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGA_Good_Fight_12 : DRGA_Dungeon
{
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_DrawElise_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_DrawElise_02_01.prefab:3c2f60746e4c84e429d1e608a606107a");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_PlayerStart_01.prefab:39a35257a0ac6f8418dd100315960f57");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Drawcard_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Drawcard_01.prefab:fc90d2ee33a2a2b4ab809aa1c5c6fd46");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01a_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01a_01.prefab:5ac9f0f4046c7c842afeaf23cfba5ee3");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01b_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01b_01.prefab:4ea2f7e5f984e124cb274b112f2e38de");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_03_01.prefab:f93f7bf3762653649b5ebc71b4fa7321");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Transform_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Transform_01.prefab:5da54e0334c37f74ea6b614c2a33a22f");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonBrann_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonBrann_02_01.prefab:9acc290eb6f7adf44bcd496eeaa86d41");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_Gala_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_Gala_01.prefab:a7344630bb25eb0469113101a5c88d04");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_NoGala_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_NoGala_01.prefab:3abf06c829e34644f8cbcd32cedd3129");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonFinley_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonFinley_02_01.prefab:ba38ff15f9ef02e4987ed6b78badea97");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_DrawElise_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_DrawElise_01_01.prefab:b929a990e505a7d4e8963bafe8d49f56");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_EliseReact_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_EliseReact_01.prefab:d8c0ab1b6f4cb9148818e6e5f2eb6b19");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_02_01.prefab:c96499ffe468deb4982201ef8b8869ae");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_04b_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_04b_01.prefab:55446bcd85245c24b8c9b34f59967196");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_Gala_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_Gala_01.prefab:cf8f041483c58494dbebe2d168f8acb0");

	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_NoGala_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_NoGala_01.prefab:9ed0c0a7908fe494bbb1959203571498");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_DrawFinley_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_DrawFinley_01.prefab:276c23be1c85aec4db99aca53c052cce");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_RenoDragon_Misc_04a_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_RenoDragon_Misc_04a_01.prefab:5e6bedd3c5de0d0438b2a993ddd5c721");

	private static readonly AssetReference VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_SummonFinley_01_01 = new AssetReference("VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_SummonFinley_01_01.prefab:b8a15af7e3009e246b9f5eebf7f66906");

	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_DrawBrann_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_DrawBrann_01.prefab:9c6a0ba03296f8f4e9041372185aec69");

	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_RenoDragon_Misc_06_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_RenoDragon_Misc_06_01.prefab:b1975961e4f2b6e429b682ea9139318a");

	private static readonly AssetReference VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_SummonBrann_01_01 = new AssetReference("VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_SummonBrann_01_01.prefab:51f7943dfb3b8704897068e7f5cc0867");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Death_01.prefab:2e50bc22d063aec46952652e0e6ece7a");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01.prefab:eb9c22f23e619604b9de2e8cbf35486f");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01.prefab:f13d2d8751b90254bab7a94dd70d67db");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01.prefab:f9b97fd400ffca44db629c80efcb3895");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01.prefab:32a4b7c99f3aae145b2a414c984f355b");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01.prefab:3b0a5793ab112f54599e953587948acf");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01.prefab:ca6c35bf0c1d5d140aafc25c11c78cc7");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01.prefab:861be9550341a2b47b1383d91e916507");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01.prefab:4500792ba86ca0143b8d0040571c2d35");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStart_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStart_01.prefab:c8f6ae7cc5b081b48aad03997c35db79");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStartHeroic_01.prefab:f7b745602c378f7418804298f2eb7837");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_Gala_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_Gala_01.prefab:8908a649ff3bb784a9dcdeb6a0539604");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_NoGala_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_NoGala_01.prefab:048c458ecdb657d48a1048d779251599");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01.prefab:42615239b9fb58b4c8b518c764b63137");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01.prefab:d13f509fccbb2474bbf7bc5a276d7a4d");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01.prefab:a998f80b9b268df4fb86df39a37f992e");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01.prefab:a998749fb50c6264a9f178edb21d5b7f");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01.prefab:fef0bd9eb9481db43bc33e8639f61bc6");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01.prefab:ce67c7d371bd22d4b9e346aa1583df7f");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01.prefab:7ac14feda2477984aa143219f855979f");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01.prefab:194b03d8ed0c58f4b9c685c397dc6965");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01.prefab:b3b2591977dbf4f47a76584b055f4869");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_01_01.prefab:d02b7e2b1c022ea41863bdcfab331b79");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_02_01.prefab:f51fddd3bf8701a468de80c17972bf63");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01.prefab:9d551762e7ecc1845842217663425566");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_RafaamReact_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_RafaamReact_01.prefab:69d52f06ee6700d4dad52f6830adf9c3");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01.prefab:97d4176bf290fb64a9f7aba668c6d013");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01.prefab:d8f1f38e062c3d3419d4932421a3f434");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01.prefab:620fb641515f72842b1c56912e35b4f2");

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01.prefab:43898a028b442c049a67a6fd8cdc71e7");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_01_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_01_01.prefab:43e889f7e3762aa43b89a80d4a09fd59");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_02_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_02_01.prefab:675aa4e590ffda2459d2d314f9e6cf93");

	private static readonly AssetReference VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_03_01 = new AssetReference("VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_03_01.prefab:e8918e27e9c288e4eb6049c347f6e08e");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01.prefab:c1442324e17668d4abfbd46d6eed287a");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01.prefab:3ef89842ff6bafa44b6f5eee23690dfd");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01.prefab:e40782b698eb1fb49abc18fc5b1b4eca");

	private static readonly AssetReference VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01.prefab:0bb9093215198014fb9f69a097a6234a");

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

	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01.prefab:fbfe70bab9271e746adc673ebe4e8ab4");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Thinking_02 = new AssetReference("VO_ULDA_Reno_Male_Human_Thinking_02.prefab:f17889dac17a5554d9c34316c4323b35");

	private static readonly AssetReference VO_ULDA_Reno_Male_Human_Threaten_01 = new AssetReference("VO_ULDA_Reno_Male_Human_Threaten_01.prefab:aa2839ab336aa254faf86c33e8a63b0f");

	private static readonly AssetReference VO_DRG_610_Male_Dragon_Threaten_01 = new AssetReference("VO_DRG_610_Male_Dragon_Threaten_01.prefab:7c5e7f29cef55ea489b0393e9fb5a27d");

	private static readonly AssetReference VO_DRG_650_Male_Dragon_Threaten_01 = new AssetReference("VO_DRG_650_Male_Dragon_Threaten_01.prefab:ab3985b6f0a1b86488c9f959cd57f51c");

	private List<string> m_missionEventTrigger106Lines = new List<string> { VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01 };

	private List<string> m_VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_ExpositionLines = new List<string> { VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01 };

	private List<string> m_VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_IdleLines = new List<string> { VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_03_01 };

	private List<string> m_VO_PlayerPlaysGalakrond = new List<string> { VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_02_01 };

	private List<string> m_VO_BossHasGalakrond = new List<string> { VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01 };

	private List<string> m_missionEventHeroPowerTrigger = new List<string> { VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01 };

	private List<string> m_GalakrondHeroPowerTrigger = new List<string> { VO_DRG_610_Male_Dragon_Threaten_01, VO_DRG_650_Male_Dragon_Threaten_01 };

	private int m_InvokeTracker;

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_DrawElise_02_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_PlayerStart_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Drawcard_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01a_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01b_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_03_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Transform_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonBrann_02_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_Gala_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_NoGala_01,
			VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonFinley_02_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_DrawElise_01_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_EliseReact_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_02_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_04b_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_Gala_01, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_NoGala_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_DrawFinley_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_RenoDragon_Misc_04a_01, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_SummonFinley_01_01,
			VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_DrawBrann_01, VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_RenoDragon_Misc_06_01, VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_SummonBrann_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Death_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_03_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_04_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_05_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Invoke_06_01,
			VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStart_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStartHeroic_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_Gala_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_NoGala_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_03_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Idle_04_01,
			VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_01_Galakrond_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_02_Galakrond_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_03_Galakrond_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_04_Galakrond_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Misc_05_Galakrond_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Player_Galakrond_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_RafaamReact_01, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01,
			VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01, VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01, VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_01_01, VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_02_01, VO_DRGA_BOSS_07h_Male_Ethereal_Hero_Thinking_03_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_BossStart_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_01_01, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_03_01,
			VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01,
			VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01,
			VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01, VO_ULDA_Reno_Male_Human_Thinking_02, VO_ULDA_Reno_Male_Human_Threaten_01, VO_DRG_610_Male_Dragon_Threaten_01, VO_DRG_650_Male_Dragon_Threaten_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRGLOEBoss);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_Boss_Death_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (m_Heroic)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			if (m_Galakrond)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_Gala_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (!m_Galakrond)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_EmoteResponse_NoGala_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 100:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.BrannBrassRing, VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_DrawBrann_01);
			}
			break;
		case 101:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.BrannBrassRing, VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_SummonBrann_01_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonBrann_02_01);
			}
			break;
		case 102:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_DrawElise_01_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_DrawElise_02_01);
			}
			break;
		case 104:
			if (m_Galakrond && !m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_Gala_01);
			}
			if (!m_Galakrond && !m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_SummonElise_01_NoGala_01);
			}
			break;
		case 106:
			m_InvokeTracker++;
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineInOrderOnce(enemyActor, m_missionEventTrigger106Lines);
			if (!m_Heroic)
			{
				if (m_InvokeTracker == 2)
				{
					yield return PlayLineAlways(friendlyActor, VO_ULDA_Reno_Male_Human_Thinking_02);
				}
				else if (m_InvokeTracker == 4)
				{
					yield return PlayLineAlways(friendlyActor, VO_ULDA_Reno_Male_Human_Threaten_01);
				}
			}
			GameState.Get().SetBusy(busy: false);
			break;
		case 107:
			if (m_Galakrond)
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_Gala_01);
			}
			if (!m_Galakrond)
			{
				yield return PlayLineOnlyOnce(enemyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_BossAttack_NoGala_01);
			}
			break;
		case 110:
			if (m_Galakrond)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_VO_BossHasGalakrond);
			}
			break;
		case 115:
			yield return PlayLineInOrderOnce(enemyActor, m_VO_PlayerPlaysGalakrond);
			break;
		case 120:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_EliseReact_01);
			}
			break;
		case 121:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01a_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_01b_01);
			}
			break;
		case 122:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_02_01);
			}
			break;
		case 123:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Misc_03_01);
			}
			break;
		case 124:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.FinleyBrassRing, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_RenoDragon_Misc_04a_01);
				yield return PlayLineAlways(DRGA_Dungeon.EliseBrassRing, VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_12_RenoDragon_Misc_04b_01);
			}
			break;
		case 126:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_Misc_05_01);
			}
			break;
		case 127:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.BrannBrassRing, VO_DRGA_BOSS_04h_Male_Dwarf_Good_Fight_12_RenoDragon_Misc_06_01);
			}
			break;
		case 128:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Transform_01);
				yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_RenoDragon_RafaamReact_01);
			}
			break;
		case 130:
			if (!m_Heroic)
			{
				if (m_Galakrond)
				{
					yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_Gala_01);
				}
				if (!m_Galakrond)
				{
					yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonElise_02_NoGala_01);
				}
			}
			break;
		case 133:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.FinleyBrassRing, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_DrawFinley_01);
			}
			break;
		case 134:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(DRGA_Dungeon.FinleyBrassRing, VO_DRGA_BOSS_03h_Male_Murloc_Good_Fight_12_SummonFinley_01_01);
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_SummonFinley_02_01);
			}
			break;
		case 135:
			GameState.Get().SetBusy(busy: true);
			m_Galakrond = true;
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 136:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineInOrderOnce(enemyActor, m_VO_DRGA_BOSS_07h_Male_Ethereal_Good_Fight_12_ExpositionLines);
			GameState.Get().SetBusy(busy: false);
			break;
		case 137:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_GalakrondHeroPowerTrigger);
			break;
		case 140:
			GameState.Get().SetBusy(busy: true);
			m_Galakrond = true;
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_07h_Male_Ethereal_Evil_Fight_11_Galakrond_Born_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 141:
			GameState.Get().SetBusy(busy: true);
			m_Galakrond = true;
			yield return PlayLineAlways(enemyActor, VO_DRGA_BOSS_24h_Male_Dragon_Evil_Fight_12_Idle_02_01);
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_11_Misc_04_01);
			}
			GameState.Get().SetBusy(busy: false);
			break;
		case 145:
			if (!m_Heroic)
			{
				yield return PlayLineAlways(friendlyActor, VO_DRGA_BOSS_01h_Male_Human_Good_Fight_12_RenoDragon_Drawcard_01);
			}
			break;
		case 199:
			if (!m_Heroic)
			{
				yield return PlayAndRemoveRandomLineOnlyOnce(friendlyActor, m_missionEventHeroPowerTrigger);
			}
			break;
		case 505:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01);
			break;
		case 506:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01);
			break;
		case 507:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01);
			break;
		case 508:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01);
			break;
		case 509:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01);
			break;
		case 510:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01);
			break;
		case 511:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01);
			break;
		case 512:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01);
			break;
		case 513:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01);
			break;
		case 514:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01);
			break;
		case 515:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01);
			break;
		case 516:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01);
			break;
		case 517:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01);
			break;
		case 518:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01);
			break;
		case 519:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01);
			break;
		case 520:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01);
			break;
		case 521:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01);
			break;
		case 522:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01);
			break;
		case 523:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01);
			break;
		case 524:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01);
			break;
		case 525:
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01);
			break;
		case 555:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 556:
			yield return new WaitForSeconds(4f);
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 557:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(Wisdomball_Pop_up_BrassRing_Quote, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01);
			GameState.Get().SetBusy(busy: false);
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
		}
	}
}
