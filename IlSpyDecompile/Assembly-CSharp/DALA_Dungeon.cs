using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DALA_Dungeon : DALA_MissionEntity
{
	private static readonly AssetReference VO_DALA_Chu_Male_Pandaren_TRIGGER_BRASS_KNUCKLES_01 = new AssetReference("VO_DALA_Chu_Male_Pandaren_TRIGGER_BRASS_KNUCKLES_01.prefab:2c4cbc9705397b44e9453659b62d06c2");

	private static readonly AssetReference VO_DALA_Chu_Male_Pandaren_TRIGGER_BRAWL_01 = new AssetReference("VO_DALA_Chu_Male_Pandaren_TRIGGER_BRAWL_01.prefab:43cf4ef5784d68d4ea9274db416df2a3");

	private static readonly AssetReference VO_DALA_Chu_Male_Pandaren_TRIGGER_BRING_IT_ON_01 = new AssetReference("VO_DALA_Chu_Male_Pandaren_TRIGGER_BRING_IT_ON_01.prefab:fdd06370873540b498ed7994e84986aa");

	private static readonly AssetReference VO_DALA_Chu_Male_Pandaren_TRIGGER_I_KNOW_A_GUY_01 = new AssetReference("VO_DALA_Chu_Male_Pandaren_TRIGGER_I_KNOW_A_GUY_01.prefab:8106846e7f54b84459cca5580a770631");

	private static readonly AssetReference VO_DALA_Chu_Male_Pandaren_TRIGGER_SLEEP_WITH_THE_FISHES_01 = new AssetReference("VO_DALA_Chu_Male_Pandaren_TRIGGER_SLEEP_WITH_THE_FISHES_01.prefab:5eb12ff2a7ce74a41ba58450ff9a0791");

	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_BLOODLUST_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_BLOODLUST_01.prefab:cd8cd403730328540a4390e923438ca9");

	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_HEALINGRAIN_02 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_HEALINGRAIN_02.prefab:bf9a4f3fd56c5114cb59ce3b86bccddd");

	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_LIGHTNINGSPELL_02 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_LIGHTNINGSPELL_02.prefab:668b1af03bc17f6439d95c15b4dfc96f");

	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_RAINOFTOADS_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_RAINOFTOADS_01.prefab:3cf010d85a7d1834aaf7acda6da2f6f6");

	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_01.prefab:b55e5dc8c5f5a9c46a04b83026eda601");

	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_STONECLAW_TOTEM_RARE_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_STONECLAW_TOTEM_RARE_01.prefab:93ef32ef616ba034ab9c57383f8943d9");

	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_TREASURE_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_TREASURE_01.prefab:236d088e880cd6f41a8a5a6a18772f66");

	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_TRAP_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_TRAP_01.prefab:0b0fc5a03d0380c4f87ae6fadec13e75");

	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_ASSASSINATE_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_ASSASSINATE_01.prefab:877041aad0f1e9747ad1d86470cd414c");

	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_CANNON_BARRAGE_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_CANNON_BARRAGE_01.prefab:090751b1c2cbe50479545d60dd46ab19");

	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_GUN_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_GUN_01.prefab:3cd70de36b3fee64f92804e2f26794d2");

	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_HEADCRACK_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_HEADCRACK_01.prefab:5ea88503051703e4c9ee2deb097c0cdf");

	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_HYPERBLASTER_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_HYPERBLASTER_01.prefab:6f98a3da7287667449f42e3c3ecfaee6");

	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_VANISH_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_VANISH_01.prefab:156cd6142e1d41a42a7c6401cec174c2");

	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_WANTED_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_WANTED_01.prefab:3b36218cb68744b40a2ca4b53929e059");

	private static readonly AssetReference VO_DALA_George_Male_Human_TRIGGER_AVENGE_01 = new AssetReference("VO_DALA_George_Male_Human_TRIGGER_AVENGE_01.prefab:313842461f19453458016944f7134199");

	private static readonly AssetReference VO_DALA_George_Male_Human_TRIGGER_EYE_FOR_AN_EYE_01 = new AssetReference("VO_DALA_George_Male_Human_TRIGGER_EYE_FOR_AN_EYE_01.prefab:73299c008d07e424683dffad6fd99fb3");

	private static readonly AssetReference VO_DALA_George_Male_Human_TRIGGER_LOST_IN_THE_JUNGLE_01 = new AssetReference("VO_DALA_George_Male_Human_TRIGGER_LOST_IN_THE_JUNGLE_01.prefab:fbd4cbef88e8e8e4f99caa2371be5a56");

	private static readonly AssetReference VO_DALA_George_Male_Human_TRIGGER_NOBLE_SACRIFICE_01 = new AssetReference("VO_DALA_George_Male_Human_TRIGGER_NOBLE_SACRIFICE_01.prefab:07da9e521919d6b4f9d86dc04b166488");

	private static readonly AssetReference VO_DALA_George_Male_Human_TRIGGER_VINE_CLEAVER_01 = new AssetReference("VO_DALA_George_Male_Human_TRIGGER_VINE_CLEAVER_01.prefab:4aff78a94764f8b478353d0a1dbb23ab");

	private static readonly AssetReference VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BLACKHOWLGUNSPIRE_01 = new AssetReference("VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BLACKHOWLGUNSPIRE_01.prefab:e6ec71b58fb7f80498149c9861e58e0d");

	private static readonly AssetReference VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BOMBTOSS_01 = new AssetReference("VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BOMBTOSS_01.prefab:2f74d357d42a3b040842cbeec69ae76a");

	private static readonly AssetReference VO_DALA_Barkeye_Male_Gnoll_TRIGGER_DEADEYE_01 = new AssetReference("VO_DALA_Barkeye_Male_Gnoll_TRIGGER_DEADEYE_01.prefab:d96787c9f81228d4aa7118c4cee87853");

	private static readonly AssetReference VO_DALA_Barkeye_Male_Gnoll_TRIGGER_HUNTERSMARK_01 = new AssetReference("VO_DALA_Barkeye_Male_Gnoll_TRIGGER_HUNTERSMARK_01.prefab:d17bf8714b62fb54fb7bfea3f74889dd");

	private static readonly AssetReference VO_DALA_Barkeye_Male_Gnoll_TRIGGER_LOCKANDLOAD_01 = new AssetReference("VO_DALA_Barkeye_Male_Gnoll_TRIGGER_LOCKANDLOAD_01.prefab:41720cb7042277845a21ad490273c136");

	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_ARMOR_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_ARMOR_01.prefab:a0dc154cad074904bb8481bdb4da3956");

	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_GOLDENCANDLE_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_GOLDENCANDLE_01.prefab:f47d9ff737531034b85ec8830e90b5e8");

	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_HEALINGSPELL_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_HEALINGSPELL_01.prefab:ebcf82f3f17f2434dbe790ef6fd07d36");

	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_MOONFIRE_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_MOONFIRE_01.prefab:57030794a70f1e94abb060f2aaba5ed1");

	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_POISONSEEDS_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_POISONSEEDS_01.prefab:774ffff7d3ae4d543a9c9ef58ca18dca");

	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_RAT_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_RAT_01.prefab:29d43d0fdb3b1f54e923bef3d056591c");

	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SOW_THE_SEEDS_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SOW_THE_SEEDS_01.prefab:7dfe985e52db89e479513db2f02e44c5");

	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SWIPE_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SWIPE_01.prefab:f23e0efeb3665e541a125f711d60e917");

	private static readonly AssetReference VO_DALA_Tekahn_Male_TolVir_TRIGGER_ASMALLROCK_01 = new AssetReference("VO_DALA_Tekahn_Male_TolVir_TRIGGER_ASMALLROCK_01.prefab:19345793940aa064e99df516b1a251aa");

	private static readonly AssetReference VO_DALA_Tekahn_Male_TolVir_TRIGGER_CATACLYSM_01 = new AssetReference("VO_DALA_Tekahn_Male_TolVir_TRIGGER_CATACLYSM_01.prefab:378a151f50f6e764096599c1fa0089d7");

	private static readonly AssetReference VO_DALA_Tekahn_Male_TolVir_TRIGGER_DARKPACT_01 = new AssetReference("VO_DALA_Tekahn_Male_TolVir_TRIGGER_DARKPACT_01.prefab:30870391b0a521b46a9d665f9c0fe687");

	private static readonly AssetReference VO_DALA_Tekahn_Male_TolVir_TRIGGER_RENOUNCEDARKNESS_01 = new AssetReference("VO_DALA_Tekahn_Male_TolVir_TRIGGER_RENOUNCEDARKNESS_01.prefab:ddf3dc76467c1de49bc100c32f4dde9c");

	private static readonly AssetReference VO_DALA_Tekahn_Male_TolVir_TRIGGER_UNWILLINGSACRIFICE_01 = new AssetReference("VO_DALA_Tekahn_Male_TolVir_TRIGGER_UNWILLINGSACRIFICE_01.prefab:c833be43965c0524199a2e64d9fda1ae");

	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_BLIZZARD_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_BLIZZARD_01.prefab:775f2886ed07122468873ca2eadb4945");

	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FIREBALL_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FIREBALL_01.prefab:d4c52635e1716144a828dddec7bf5825");

	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FORGOTTENTORCH_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FORGOTTENTORCH_01.prefab:52f77e5abbea9834287485cdb85f84a8");

	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_METEOR_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_METEOR_01.prefab:d8f2d1e9e1b7a6549acb33b1ef1249e2");

	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_THECANDLE_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_THECANDLE_01.prefab:e99721cf2171c9c49968f2a76acc1cbe");

	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_TIDALSURGE_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_TIDALSURGE_01.prefab:df469759b9204354da20c6f3f17510aa");

	private static readonly AssetReference VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_EXTRAARMS_01 = new AssetReference("VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_EXTRAARMS_01.prefab:915a9729a4a759b469a440946b9d6cb2");

	private static readonly AssetReference VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_FLYBY_01 = new AssetReference("VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_FLYBY_01.prefab:2e7f49708c744ca4ea1fa1ab2997cd97");

	private static readonly AssetReference VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWFORM_01 = new AssetReference("VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWFORM_01.prefab:1ddff86cd0dd2ca43967d64d283f1d2e");

	private static readonly AssetReference VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWWORDHORROR_01 = new AssetReference("VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWWORDHORROR_01.prefab:ef8e4e0e8e19d78428c1a7a2f933bd9e");

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

	public List<string> m_BossVOLines = new List<string>();

	public List<string> m_PlayerVOLines = new List<string>();

	public string m_introLine;

	public string m_deathLine;

	public string m_standardEmoteResponseLine;

	public List<string> m_BossIdleLines;

	public List<string> m_BossIdleLinesCopy;

	private int m_PlayPlayerVOLineIndex;

	private int m_PlayBossVOLineIndex;

	public static DALA_Dungeon InstantiateDALADungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		switch (opposingHeroCardID)
		{
		case "DALA_BOSS_01h":
			return new DALA_Dungeon_Boss_01h();
		case "DALA_BOSS_02h":
			return new DALA_Dungeon_Boss_02h();
		case "DALA_BOSS_03h":
			return new DALA_Dungeon_Boss_03h();
		case "DALA_BOSS_04h":
			return new DALA_Dungeon_Boss_04h();
		case "DALA_BOSS_05h":
			return new DALA_Dungeon_Boss_05h();
		case "DALA_BOSS_06h":
			return new DALA_Dungeon_Boss_06h();
		case "DALA_BOSS_07h":
			return new DALA_Dungeon_Boss_07h();
		case "DALA_BOSS_08h":
			return new DALA_Dungeon_Boss_08h();
		case "DALA_BOSS_09h":
			return new DALA_Dungeon_Boss_09h();
		case "DALA_BOSS_10h":
			return new DALA_Dungeon_Boss_10h();
		case "DALA_BOSS_11h":
			return new DALA_Dungeon_Boss_11h();
		case "DALA_BOSS_12h":
			return new DALA_Dungeon_Boss_12h();
		case "DALA_BOSS_13h":
			return new DALA_Dungeon_Boss_13h();
		case "DALA_BOSS_14h":
			return new DALA_Dungeon_Boss_14h();
		case "DALA_BOSS_15h":
			return new DALA_Dungeon_Boss_15h();
		case "DALA_BOSS_16h":
			return new DALA_Dungeon_Boss_16h();
		case "DALA_BOSS_17h":
			return new DALA_Dungeon_Boss_17h();
		case "DALA_BOSS_18h":
			return new DALA_Dungeon_Boss_18h();
		case "DALA_BOSS_19h":
			return new DALA_Dungeon_Boss_19h();
		case "DALA_BOSS_20h":
			return new DALA_Dungeon_Boss_20h();
		case "DALA_BOSS_21h":
			return new DALA_Dungeon_Boss_21h();
		case "DALA_BOSS_22h":
			return new DALA_Dungeon_Boss_22h();
		case "DALA_BOSS_23h":
			return new DALA_Dungeon_Boss_23h();
		case "DALA_BOSS_24h":
			return new DALA_Dungeon_Boss_24h();
		case "DALA_BOSS_25h":
			return new DALA_Dungeon_Boss_25h();
		case "DALA_BOSS_26h":
			return new DALA_Dungeon_Boss_26h();
		case "DALA_BOSS_27h":
			return new DALA_Dungeon_Boss_27h();
		case "DALA_BOSS_28h":
			return new DALA_Dungeon_Boss_28h();
		case "DALA_BOSS_29h":
			return new DALA_Dungeon_Boss_29h();
		case "DALA_BOSS_30h":
			return new DALA_Dungeon_Boss_30h();
		case "DALA_BOSS_31h":
			return new DALA_Dungeon_Boss_31h();
		case "DALA_BOSS_32h":
			return new DALA_Dungeon_Boss_32h();
		case "DALA_BOSS_33h":
			return new DALA_Dungeon_Boss_33h();
		case "DALA_BOSS_34h":
			return new DALA_Dungeon_Boss_34h();
		case "DALA_BOSS_35h":
			return new DALA_Dungeon_Boss_35h();
		case "DALA_BOSS_36h":
			return new DALA_Dungeon_Boss_36h();
		case "DALA_BOSS_37h":
			return new DALA_Dungeon_Boss_37h();
		case "DALA_BOSS_38h":
			return new DALA_Dungeon_Boss_38h();
		case "DALA_BOSS_39h":
			return new DALA_Dungeon_Boss_39h();
		case "DALA_BOSS_40h":
			return new DALA_Dungeon_Boss_40h();
		case "DALA_BOSS_41h":
			return new DALA_Dungeon_Boss_41h();
		case "DALA_BOSS_42h":
			return new DALA_Dungeon_Boss_42h();
		case "DALA_BOSS_43h":
			return new DALA_Dungeon_Boss_43h();
		case "DALA_BOSS_44h":
			return new DALA_Dungeon_Boss_44h();
		case "DALA_BOSS_45h":
			return new DALA_Dungeon_Boss_45h();
		case "DALA_BOSS_46h":
			return new DALA_Dungeon_Boss_46h();
		case "DALA_BOSS_47h":
			return new DALA_Dungeon_Boss_47h();
		case "DALA_BOSS_48h":
			return new DALA_Dungeon_Boss_48h();
		case "DALA_BOSS_49h":
			return new DALA_Dungeon_Boss_49h();
		case "DALA_BOSS_50h":
			return new DALA_Dungeon_Boss_50h();
		case "DALA_BOSS_51h":
			return new DALA_Dungeon_Boss_51h();
		case "DALA_BOSS_52h":
			return new DALA_Dungeon_Boss_52h();
		case "DALA_BOSS_53h":
			return new DALA_Dungeon_Boss_53h();
		case "DALA_BOSS_54h":
			return new DALA_Dungeon_Boss_54h();
		case "DALA_BOSS_55h":
			return new DALA_Dungeon_Boss_55h();
		case "DALA_BOSS_56h":
			return new DALA_Dungeon_Boss_56h();
		case "DALA_BOSS_57h":
			return new DALA_Dungeon_Boss_57h();
		case "DALA_BOSS_58h":
			return new DALA_Dungeon_Boss_58h();
		case "DALA_BOSS_59h":
			return new DALA_Dungeon_Boss_59h();
		case "DALA_BOSS_60h":
			return new DALA_Dungeon_Boss_60h();
		case "DALA_BOSS_61h":
			return new DALA_Dungeon_Boss_61h();
		case "DALA_BOSS_62h":
			return new DALA_Dungeon_Boss_62h();
		case "DALA_BOSS_63h":
			return new DALA_Dungeon_Boss_63h();
		case "DALA_BOSS_64h":
			return new DALA_Dungeon_Boss_64h();
		case "DALA_BOSS_65h":
			return new DALA_Dungeon_Boss_65h();
		case "DALA_BOSS_66h":
			return new DALA_Dungeon_Boss_66h();
		case "DALA_BOSS_67h":
			return new DALA_Dungeon_Boss_67h();
		case "DALA_BOSS_68h":
			return new DALA_Dungeon_Boss_68h();
		case "DALA_BOSS_69h":
			return new DALA_Dungeon_Boss_69h();
		case "DALA_BOSS_70h":
			return new DALA_Dungeon_Boss_70h();
		case "DALA_BOSS_71h":
			return new DALA_Dungeon_Boss_71h();
		case "DALA_BOSS_72h":
			return new DALA_Dungeon_Boss_72h();
		case "DALA_BOSS_73h":
			return new DALA_Dungeon_Boss_73h();
		case "DALA_BOSS_74h":
			return new DALA_Dungeon_Boss_74h();
		case "DALA_BOSS_75h":
			return new DALA_Dungeon_Boss_75h();
		default:
			Log.All.PrintError("DALA_Dungeon.InstantiateDALADungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", opposingHeroCardID);
			return new DALA_Dungeon();
		}
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_Chu_Male_Pandaren_TRIGGER_BRASS_KNUCKLES_01, VO_DALA_Chu_Male_Pandaren_TRIGGER_BRAWL_01, VO_DALA_Chu_Male_Pandaren_TRIGGER_BRING_IT_ON_01, VO_DALA_Chu_Male_Pandaren_TRIGGER_I_KNOW_A_GUY_01, VO_DALA_Chu_Male_Pandaren_TRIGGER_SLEEP_WITH_THE_FISHES_01, VO_DALA_Vessina_Female_Sethrak_TRIGGER_BLOODLUST_01, VO_DALA_Vessina_Female_Sethrak_TRIGGER_HEALINGRAIN_02, VO_DALA_Vessina_Female_Sethrak_TRIGGER_LIGHTNINGSPELL_02, VO_DALA_Vessina_Female_Sethrak_TRIGGER_RAINOFTOADS_01, VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_01,
			VO_DALA_Vessina_Female_Sethrak_TRIGGER_STONECLAW_TOTEM_RARE_01, VO_DALA_Vessina_Female_Sethrak_TRIGGER_TREASURE_01, VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_TRAP_01, VO_DALA_Eudora_Female_Vulpine_TRIGGER_ASSASSINATE_01, VO_DALA_Eudora_Female_Vulpine_TRIGGER_CANNON_BARRAGE_01, VO_DALA_Eudora_Female_Vulpine_TRIGGER_GUN_01, VO_DALA_Eudora_Female_Vulpine_TRIGGER_HEADCRACK_01, VO_DALA_Eudora_Female_Vulpine_TRIGGER_HYPERBLASTER_01, VO_DALA_Eudora_Female_Vulpine_TRIGGER_VANISH_01, VO_DALA_Eudora_Female_Vulpine_TRIGGER_WANTED_01,
			VO_DALA_George_Male_Human_TRIGGER_AVENGE_01, VO_DALA_George_Male_Human_TRIGGER_EYE_FOR_AN_EYE_01, VO_DALA_George_Male_Human_TRIGGER_LOST_IN_THE_JUNGLE_01, VO_DALA_George_Male_Human_TRIGGER_NOBLE_SACRIFICE_01, VO_DALA_George_Male_Human_TRIGGER_VINE_CLEAVER_01, VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BLACKHOWLGUNSPIRE_01, VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BOMBTOSS_01, VO_DALA_Barkeye_Male_Gnoll_TRIGGER_DEADEYE_01, VO_DALA_Barkeye_Male_Gnoll_TRIGGER_HUNTERSMARK_01, VO_DALA_Barkeye_Male_Gnoll_TRIGGER_LOCKANDLOAD_01,
			VO_DALA_Squeamlish_Female_Kobold_TRIGGER_ARMOR_01, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_GOLDENCANDLE_01, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_HEALINGSPELL_01, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_MOONFIRE_01, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_POISONSEEDS_01, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_RAT_01, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SOW_THE_SEEDS_01, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SWIPE_01, VO_DALA_Tekahn_Male_TolVir_TRIGGER_ASMALLROCK_01, VO_DALA_Tekahn_Male_TolVir_TRIGGER_CATACLYSM_01,
			VO_DALA_Tekahn_Male_TolVir_TRIGGER_DARKPACT_01, VO_DALA_Tekahn_Male_TolVir_TRIGGER_RENOUNCEDARKNESS_01, VO_DALA_Tekahn_Male_TolVir_TRIGGER_UNWILLINGSACRIFICE_01, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_BLIZZARD_01, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FIREBALL_01, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FORGOTTENTORCH_01, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_METEOR_01, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_THECANDLE_01, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_TIDALSURGE_01, VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_EXTRAARMS_01,
			VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_FLYBY_01, VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWFORM_01, VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWWORDHORROR_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01,
			VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01,
			VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01, VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01
		};
		m_PlayerVOLines = new List<string>(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public void SetBossVOLines(List<string> VOLines)
	{
		m_BossVOLines = new List<string>(VOLines);
	}

	protected virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 0.5f;
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
		yield return WaitForEntitySoundToFinish(entity);
		string cardID = entity.GetCardId();
		if (playerHeroCardID == "DALA_Chu")
		{
			switch (cardID)
			{
			case "CFM_631":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Chu_Male_Pandaren_TRIGGER_BRASS_KNUCKLES_01);
				break;
			case "EX1_407":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Chu_Male_Pandaren_TRIGGER_BRAWL_01);
				break;
			case "ICC_837":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Chu_Male_Pandaren_TRIGGER_BRING_IT_ON_01);
				break;
			case "CFM_940":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Chu_Male_Pandaren_TRIGGER_I_KNOW_A_GUY_01);
				break;
			case "CFM_716":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Chu_Male_Pandaren_TRIGGER_SLEEP_WITH_THE_FISHES_01);
				break;
			}
		}
		if (playerHeroCardID == "DALA_Vessina")
		{
			switch (cardID)
			{
			case "CS2_046":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Vessina_Female_Sethrak_TRIGGER_BLOODLUST_01);
				break;
			case "LOOT_373":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Vessina_Female_Sethrak_TRIGGER_HEALINGRAIN_02);
				break;
			case "EX1_259":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Vessina_Female_Sethrak_TRIGGER_LIGHTNINGSPELL_02);
				break;
			case "CS2_051":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Vessina_Female_Sethrak_TRIGGER_STONECLAW_TOTEM_RARE_01);
				break;
			case "DALA_711":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Vessina_Female_Sethrak_TRIGGER_TREASURE_01);
				break;
			}
		}
		if (playerHeroCardID == "DALA_Eudora")
		{
			switch (cardID)
			{
			case "CS2_076":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Eudora_Female_Vulpine_TRIGGER_ASSASSINATE_01);
				break;
			case "TRL_127":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Eudora_Female_Vulpine_TRIGGER_CANNON_BARRAGE_01);
				break;
			case "LOOT_542":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Eudora_Female_Vulpine_TRIGGER_GUN_01);
				break;
			case "EX1_137":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Eudora_Female_Vulpine_TRIGGER_HEADCRACK_01);
				break;
			case "DALA_723":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Eudora_Female_Vulpine_TRIGGER_HYPERBLASTER_01);
				break;
			case "NEW1_004":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Eudora_Female_Vulpine_TRIGGER_VANISH_01);
				break;
			case "GIL_687":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Eudora_Female_Vulpine_TRIGGER_WANTED_01);
				break;
			}
		}
		if (playerHeroCardID == "DALA_George")
		{
			string text = cardID;
			if (!(text == "UNG_960"))
			{
				if (text == "UNG_950")
				{
					yield return PlayLineOnlyOnce(playerActor, VO_DALA_George_Male_Human_TRIGGER_VINE_CLEAVER_01);
				}
			}
			else
			{
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_George_Male_Human_TRIGGER_LOST_IN_THE_JUNGLE_01);
			}
		}
		if (playerHeroCardID == "DALA_Barkeye")
		{
			switch (cardID)
			{
			case "GIL_152":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BLACKHOWLGUNSPIRE_01);
				break;
			case "BOT_033":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BOMBTOSS_01);
				break;
			case "CS2_084":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Barkeye_Male_Gnoll_TRIGGER_HUNTERSMARK_01);
				break;
			case "DALA_723":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Barkeye_Male_Gnoll_TRIGGER_DEADEYE_01);
				break;
			case "AT_061":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Barkeye_Male_Gnoll_TRIGGER_LOCKANDLOAD_01);
				break;
			}
		}
		if (playerHeroCardID == "DALA_Squeamlish")
		{
			switch (cardID)
			{
			case "GIL_637":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_ARMOR_01);
				break;
			case "DALA_709":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_GOLDENCANDLE_01);
				break;
			case "GVG_033":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_HEALINGSPELL_01);
				break;
			case "CS2_008":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_MOONFIRE_01);
				break;
			case "FP1_019":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_POISONSEEDS_01);
				break;
			case "LOOT_069":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_RAT_01);
				break;
			case "DALA_727":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SOW_THE_SEEDS_01);
				break;
			case "CS2_012":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SWIPE_01);
				break;
			}
		}
		if (playerHeroCardID == "DALA_Tekahn")
		{
			switch (cardID)
			{
			case "GILA_500p2t":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Tekahn_Male_TolVir_TRIGGER_ASMALLROCK_01);
				break;
			case "LOOT_417":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Tekahn_Male_TolVir_TRIGGER_CATACLYSM_01);
				break;
			case "LOOT_017":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Tekahn_Male_TolVir_TRIGGER_DARKPACT_01);
				break;
			case "OG_118":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Tekahn_Male_TolVir_TRIGGER_RENOUNCEDARKNESS_01);
				break;
			case "ICC_469":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Tekahn_Male_TolVir_TRIGGER_UNWILLINGSACRIFICE_01);
				break;
			}
		}
		if (playerHeroCardID == "DALA_Rakanishu")
		{
			switch (cardID)
			{
			case "CS2_028":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_BLIZZARD_01);
				break;
			case "CS2_029":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FIREBALL_01);
				break;
			case "LOE_002":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FORGOTTENTORCH_01);
				break;
			case "UNG_955":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_METEOR_01);
				break;
			case "LOOTA_843":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_THECANDLE_01);
				break;
			case "UNG_817":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Rakanishu_Male_Elemental_TRIGGER_TIDALSURGE_01);
				break;
			}
		}
		if (playerHeroCardID == "DALA_Kriziki")
		{
			switch (cardID)
			{
			case "BOT_219":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_EXTRAARMS_01);
				break;
			case "DALA_716":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_FLYBY_01);
				break;
			case "EX1_625":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWFORM_01);
				break;
			case "OG_100":
				yield return PlayLineOnlyOnce(playerActor, VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWWORDHORROR_01);
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
		switch (missionEvent)
		{
		case 501:
			yield return PlayLineOnlyOnce(actor, VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_TRAP_01);
			break;
		case 502:
			yield return PlayLineOnlyOnce(actor, VO_DALA_George_Male_Human_TRIGGER_AVENGE_01);
			break;
		case 503:
			yield return PlayLineOnlyOnce(actor, VO_DALA_George_Male_Human_TRIGGER_NOBLE_SACRIFICE_01);
			break;
		case 504:
			yield return PlayLineOnlyOnce(actor, VO_DALA_George_Male_Human_TRIGGER_EYE_FOR_AN_EYE_01);
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
			case 3005:
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_01");
			case 3188:
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_02");
			case 3189:
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_03");
			case 3190:
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_04");
			case 3191:
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_05");
			case 3328:
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_01_HEROIC");
			case 3329:
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_02_HEROIC");
			case 3330:
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_03_HEROIC");
			case 3331:
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_04_HEROIC");
			case 3332:
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_05_HEROIC");
			}
		}
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
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
}
