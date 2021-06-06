using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200042D RID: 1069
public class DALA_Dungeon : DALA_MissionEntity
{
	// Token: 0x06003A3D RID: 14909 RVA: 0x0012B664 File Offset: 0x00129864
	public static DALA_Dungeon InstantiateDALADungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(opposingHeroCardID);
		if (num <= 2042259098U)
		{
			if (num <= 842482271U)
			{
				if (num <= 286645119U)
				{
					if (num <= 117736096U)
					{
						if (num <= 53320291U)
						{
							if (num != 20500528U)
							{
								if (num == 53320291U)
								{
									if (opposingHeroCardID == "DALA_BOSS_32h")
									{
										return new DALA_Dungeon_Boss_32h();
									}
								}
							}
							else if (opposingHeroCardID == "DALA_BOSS_39h")
							{
								return new DALA_Dungeon_Boss_39h();
							}
						}
						else if (num != 87316814U)
						{
							if (num == 117736096U)
							{
								if (opposingHeroCardID == "DALA_BOSS_13h")
								{
									return new DALA_Dungeon_Boss_13h();
								}
							}
						}
						else if (opposingHeroCardID == "DALA_BOSS_37h")
						{
							return new DALA_Dungeon_Boss_37h();
						}
					}
					else if (num <= 150452691U)
					{
						if (num != 120033409U)
						{
							if (num == 150452691U)
							{
								if (opposingHeroCardID == "DALA_BOSS_18h")
								{
									return new DALA_Dungeon_Boss_18h();
								}
							}
						}
						else if (opposingHeroCardID == "DALA_BOSS_30h")
						{
							return new DALA_Dungeon_Boss_30h();
						}
					}
					else if (num != 151732619U)
					{
						if (num != 254945909U)
						{
							if (num == 286645119U)
							{
								if (opposingHeroCardID == "DALA_BOSS_14h")
								{
									return new DALA_Dungeon_Boss_14h();
								}
							}
						}
						else if (opposingHeroCardID == "DALA_BOSS_34h")
						{
							return new DALA_Dungeon_Boss_34h();
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_10h")
					{
						return new DALA_Dungeon_Boss_10h();
					}
				}
				else if (num <= 706393011U)
				{
					if (num <= 353358237U)
					{
						if (num != 319361714U)
						{
							if (num == 353358237U)
							{
								if (opposingHeroCardID == "DALA_BOSS_16h")
								{
									return new DALA_Dungeon_Boss_16h();
								}
							}
						}
						else if (opposingHeroCardID == "DALA_BOSS_11h")
						{
							return new DALA_Dungeon_Boss_11h();
						}
					}
					else if (num != 673573248U)
					{
						if (num == 706393011U)
						{
							if (opposingHeroCardID == "DALA_BOSS_69h")
							{
								return new DALA_Dungeon_Boss_69h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_62h")
					{
						return new DALA_Dungeon_Boss_62h();
					}
				}
				else if (num <= 722612950U)
				{
					if (num != 707672939U)
					{
						if (num == 722612950U)
						{
							if (opposingHeroCardID == "DALA_BOSS_15h")
							{
								return new DALA_Dungeon_Boss_15h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_61h")
					{
						return new DALA_Dungeon_Boss_61h();
					}
				}
				else if (num != 755432713U)
				{
					if (num != 789429236U)
					{
						if (num == 842482271U)
						{
							if (opposingHeroCardID == "DALA_BOSS_65h")
							{
								return new DALA_Dungeon_Boss_65h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_17h")
					{
						return new DALA_Dungeon_Boss_17h();
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_12h")
				{
					return new DALA_Dungeon_Boss_12h();
				}
			}
			else if (num <= 1404562481U)
			{
				if (num <= 1278553270U)
				{
					if (num <= 875302034U)
					{
						if (num != 854965594U)
						{
							if (num == 875302034U)
							{
								if (opposingHeroCardID == "DALA_BOSS_60h")
								{
									return new DALA_Dungeon_Boss_60h();
								}
							}
						}
						else if (opposingHeroCardID == "DALA_BOSS_19h")
						{
							return new DALA_Dungeon_Boss_19h();
						}
					}
					else if (num != 909298557U)
					{
						if (num == 1278553270U)
						{
							if (opposingHeroCardID == "DALA_BOSS_64h")
							{
								return new DALA_Dungeon_Boss_64h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_67h")
					{
						return new DALA_Dungeon_Boss_67h();
					}
				}
				else if (num <= 1337746195U)
				{
					if (num != 1311269865U)
					{
						if (num == 1337746195U)
						{
							if (opposingHeroCardID == "DALA_BOSS_47h")
							{
								return new DALA_Dungeon_Boss_47h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_63h")
					{
						return new DALA_Dungeon_Boss_63h();
					}
				}
				else if (num != 1345369556U)
				{
					if (num != 1371845886U)
					{
						if (num == 1404562481U)
						{
							if (opposingHeroCardID == "DALA_BOSS_45h")
							{
								return new DALA_Dungeon_Boss_45h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_42h")
					{
						return new DALA_Dungeon_Boss_42h();
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_66h")
				{
					return new DALA_Dungeon_Boss_66h();
				}
			}
			else if (num <= 1975442812U)
			{
				if (num <= 1539474981U)
				{
					if (num != 1410802746U)
					{
						if (num == 1539474981U)
						{
							if (opposingHeroCardID == "DALA_BOSS_41h")
							{
								return new DALA_Dungeon_Boss_41h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_68h")
					{
						return new DALA_Dungeon_Boss_68h();
					}
				}
				else if (num != 1540754909U)
				{
					if (num != 1840633480U)
					{
						if (num == 1975442812U)
						{
							if (opposingHeroCardID == "DALA_BOSS_40h")
							{
								return new DALA_Dungeon_Boss_40h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_44h")
					{
						return new DALA_Dungeon_Boss_44h();
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_49h")
				{
					return new DALA_Dungeon_Boss_49h();
				}
			}
			else if (num <= 2007142022U)
			{
				if (num != 1976825908U)
				{
					if (num == 2007142022U)
					{
						if (opposingHeroCardID == "DALA_BOSS_20h")
						{
							return new DALA_Dungeon_Boss_20h();
						}
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_48h")
				{
					return new DALA_Dungeon_Boss_48h();
				}
			}
			else if (num != 2009542503U)
			{
				if (num != 2039858617U)
				{
					if (num == 2042259098U)
					{
						if (opposingHeroCardID == "DALA_BOSS_46h")
						{
							return new DALA_Dungeon_Boss_46h();
						}
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_27h")
				{
					return new DALA_Dungeon_Boss_27h();
				}
			}
			else if (opposingHeroCardID == "DALA_BOSS_43h")
			{
				return new DALA_Dungeon_Boss_43h();
			}
		}
		else if (num <= 3194538694U)
		{
			if (num <= 2629898628U)
			{
				if (num <= 2510029307U)
				{
					if (num <= 2106674903U)
					{
						if (num != 2073958308U)
						{
							if (num == 2106674903U)
							{
								if (opposingHeroCardID == "DALA_BOSS_29h")
								{
									return new DALA_Dungeon_Boss_29h();
								}
							}
						}
						else if (opposingHeroCardID == "DALA_BOSS_22h")
						{
							return new DALA_Dungeon_Boss_22h();
						}
					}
					else if (num != 2475929616U)
					{
						if (num == 2510029307U)
						{
							if (opposingHeroCardID == "DALA_BOSS_25h")
							{
								return new DALA_Dungeon_Boss_25h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_26h")
					{
						return new DALA_Dungeon_Boss_26h();
					}
				}
				else if (num <= 2563082342U)
				{
					if (num != 2542745902U)
					{
						if (num == 2563082342U)
						{
							if (opposingHeroCardID == "DALA_BOSS_73h")
							{
								return new DALA_Dungeon_Boss_73h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_28h")
					{
						return new DALA_Dungeon_Boss_28h();
					}
				}
				else if (num != 2590010584U)
				{
					if (num != 2595798937U)
					{
						if (num == 2629898628U)
						{
							if (opposingHeroCardID == "DALA_BOSS_71h")
							{
								return new DALA_Dungeon_Boss_71h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_74h")
					{
						return new DALA_Dungeon_Boss_74h();
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_08h")
				{
					return new DALA_Dungeon_Boss_08h();
				}
			}
			else if (num <= 2711654925U)
			{
				if (num <= 2659386726U)
				{
					if (num != 2644838639U)
					{
						if (num == 2659386726U)
						{
							if (opposingHeroCardID == "DALA_BOSS_06h")
							{
								return new DALA_Dungeon_Boss_06h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_21h")
					{
						return new DALA_Dungeon_Boss_21h();
					}
				}
				else if (num != 2677555234U)
				{
					if (num != 2692103321U)
					{
						if (num == 2711654925U)
						{
							if (opposingHeroCardID == "DALA_BOSS_23h")
							{
								return new DALA_Dungeon_Boss_23h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_01h")
					{
						return new DALA_Dungeon_Boss_01h();
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_24h")
				{
					return new DALA_Dungeon_Boss_24h();
				}
			}
			else if (num <= 3031869936U)
			{
				if (num != 2726203012U)
				{
					if (num == 3031869936U)
					{
						if (opposingHeroCardID == "DALA_BOSS_75h")
						{
							return new DALA_Dungeon_Boss_75h();
						}
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_04h")
				{
					return new DALA_Dungeon_Boss_04h();
				}
			}
			else if (num != 3125162552U)
			{
				if (num != 3162170843U)
				{
					if (num == 3194538694U)
					{
						if (opposingHeroCardID == "DALA_BOSS_59h")
						{
							return new DALA_Dungeon_Boss_59h();
						}
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_03h")
				{
					return new DALA_Dungeon_Boss_03h();
				}
			}
			else if (opposingHeroCardID == "DALA_BOSS_57h")
			{
				return new DALA_Dungeon_Boss_57h();
			}
		}
		else if (num <= 3363899629U)
		{
			if (num <= 3267595245U)
			{
				if (num <= 3227707201U)
				{
					if (num != 3200778959U)
					{
						if (num == 3227707201U)
						{
							if (opposingHeroCardID == "DALA_BOSS_09h")
							{
								return new DALA_Dungeon_Boss_09h();
							}
						}
					}
					else if (opposingHeroCardID == "DALA_BOSS_72h")
					{
						return new DALA_Dungeon_Boss_72h();
					}
				}
				else if (num != 3259971884U)
				{
					if (num == 3267595245U)
					{
						if (opposingHeroCardID == "DALA_BOSS_70h")
						{
							return new DALA_Dungeon_Boss_70h();
						}
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_53h")
				{
					return new DALA_Dungeon_Boss_53h();
				}
			}
			else if (num <= 3297083343U)
			{
				if (num != 3294071575U)
				{
					if (num == 3297083343U)
					{
						if (opposingHeroCardID == "DALA_BOSS_07h")
						{
							return new DALA_Dungeon_Boss_07h();
						}
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_50h")
				{
					return new DALA_Dungeon_Boss_50h();
				}
			}
			else if (num != 3326788170U)
			{
				if (num != 3329799938U)
				{
					if (num == 3363899629U)
					{
						if (opposingHeroCardID == "DALA_BOSS_05h")
						{
							return new DALA_Dungeon_Boss_05h();
						}
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_02h")
				{
					return new DALA_Dungeon_Boss_02h();
				}
			}
			else if (opposingHeroCardID == "DALA_BOSS_55h")
			{
				return new DALA_Dungeon_Boss_55h();
			}
		}
		else if (num <= 3832235311U)
		{
			if (num <= 3730039406U)
			{
				if (num != 3696042883U)
				{
					if (num == 3730039406U)
					{
						if (opposingHeroCardID == "DALA_BOSS_51h")
						{
							return new DALA_Dungeon_Boss_51h();
						}
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_54h")
				{
					return new DALA_Dungeon_Boss_54h();
				}
			}
			else if (num != 3762859169U)
			{
				if (num != 3777304088U)
				{
					if (num == 3832235311U)
					{
						if (opposingHeroCardID == "DALA_BOSS_58h")
						{
							return new DALA_Dungeon_Boss_58h();
						}
					}
				}
				else if (opposingHeroCardID == "DALA_BOSS_31h")
				{
					return new DALA_Dungeon_Boss_31h();
				}
			}
			else if (opposingHeroCardID == "DALA_BOSS_56h")
			{
				return new DALA_Dungeon_Boss_56h();
			}
		}
		else if (num <= 3897668501U)
		{
			if (num != 3879499993U)
			{
				if (num == 3897668501U)
				{
					if (opposingHeroCardID == "DALA_BOSS_52h")
					{
						return new DALA_Dungeon_Boss_52h();
					}
				}
			}
			else if (opposingHeroCardID == "DALA_BOSS_38h")
			{
				return new DALA_Dungeon_Boss_38h();
			}
		}
		else if (num != 3912216588U)
		{
			if (num != 3946213111U)
			{
				if (num == 3979032874U)
				{
					if (opposingHeroCardID == "DALA_BOSS_33h")
					{
						return new DALA_Dungeon_Boss_33h();
					}
				}
			}
			else if (opposingHeroCardID == "DALA_BOSS_36h")
			{
				return new DALA_Dungeon_Boss_36h();
			}
		}
		else if (opposingHeroCardID == "DALA_BOSS_35h")
		{
			return new DALA_Dungeon_Boss_35h();
		}
		Log.All.PrintError("DALA_Dungeon.InstantiateDALADungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new DALA_Dungeon();
	}

	// Token: 0x06003A3E RID: 14910 RVA: 0x0012C370 File Offset: 0x0012A570
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon.VO_DALA_Chu_Male_Pandaren_TRIGGER_BRASS_KNUCKLES_01,
			DALA_Dungeon.VO_DALA_Chu_Male_Pandaren_TRIGGER_BRAWL_01,
			DALA_Dungeon.VO_DALA_Chu_Male_Pandaren_TRIGGER_BRING_IT_ON_01,
			DALA_Dungeon.VO_DALA_Chu_Male_Pandaren_TRIGGER_I_KNOW_A_GUY_01,
			DALA_Dungeon.VO_DALA_Chu_Male_Pandaren_TRIGGER_SLEEP_WITH_THE_FISHES_01,
			DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_BLOODLUST_01,
			DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_HEALINGRAIN_02,
			DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_LIGHTNINGSPELL_02,
			DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_RAINOFTOADS_01,
			DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_01,
			DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_STONECLAW_TOTEM_RARE_01,
			DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_TREASURE_01,
			DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_TRAP_01,
			DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_ASSASSINATE_01,
			DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_CANNON_BARRAGE_01,
			DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_GUN_01,
			DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_HEADCRACK_01,
			DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_HYPERBLASTER_01,
			DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_VANISH_01,
			DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_WANTED_01,
			DALA_Dungeon.VO_DALA_George_Male_Human_TRIGGER_AVENGE_01,
			DALA_Dungeon.VO_DALA_George_Male_Human_TRIGGER_EYE_FOR_AN_EYE_01,
			DALA_Dungeon.VO_DALA_George_Male_Human_TRIGGER_LOST_IN_THE_JUNGLE_01,
			DALA_Dungeon.VO_DALA_George_Male_Human_TRIGGER_NOBLE_SACRIFICE_01,
			DALA_Dungeon.VO_DALA_George_Male_Human_TRIGGER_VINE_CLEAVER_01,
			DALA_Dungeon.VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BLACKHOWLGUNSPIRE_01,
			DALA_Dungeon.VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BOMBTOSS_01,
			DALA_Dungeon.VO_DALA_Barkeye_Male_Gnoll_TRIGGER_DEADEYE_01,
			DALA_Dungeon.VO_DALA_Barkeye_Male_Gnoll_TRIGGER_HUNTERSMARK_01,
			DALA_Dungeon.VO_DALA_Barkeye_Male_Gnoll_TRIGGER_LOCKANDLOAD_01,
			DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_ARMOR_01,
			DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_GOLDENCANDLE_01,
			DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_HEALINGSPELL_01,
			DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_MOONFIRE_01,
			DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_POISONSEEDS_01,
			DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_RAT_01,
			DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SOW_THE_SEEDS_01,
			DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SWIPE_01,
			DALA_Dungeon.VO_DALA_Tekahn_Male_TolVir_TRIGGER_ASMALLROCK_01,
			DALA_Dungeon.VO_DALA_Tekahn_Male_TolVir_TRIGGER_CATACLYSM_01,
			DALA_Dungeon.VO_DALA_Tekahn_Male_TolVir_TRIGGER_DARKPACT_01,
			DALA_Dungeon.VO_DALA_Tekahn_Male_TolVir_TRIGGER_RENOUNCEDARKNESS_01,
			DALA_Dungeon.VO_DALA_Tekahn_Male_TolVir_TRIGGER_UNWILLINGSACRIFICE_01,
			DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_BLIZZARD_01,
			DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FIREBALL_01,
			DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FORGOTTENTORCH_01,
			DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_METEOR_01,
			DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_THECANDLE_01,
			DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_TIDALSURGE_01,
			DALA_Dungeon.VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_EXTRAARMS_01,
			DALA_Dungeon.VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_FLYBY_01,
			DALA_Dungeon.VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWFORM_01,
			DALA_Dungeon.VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWWORDHORROR_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01,
			DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01
		};
		this.m_PlayerVOLines = new List<string>(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003A3F RID: 14911 RVA: 0x0012C878 File Offset: 0x0012AA78
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06003A40 RID: 14912 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003A41 RID: 14913 RVA: 0x00112BA9 File Offset: 0x00110DA9
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 0.5f;
	}

	// Token: 0x06003A42 RID: 14914 RVA: 0x0012C888 File Offset: 0x0012AA88
	protected virtual void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = this.ChanceToPlayBossHeroPowerVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (this.m_enemySpeaking)
		{
			return;
		}
		if (num < num2)
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (actor == null)
		{
			return;
		}
		List<string> bossHeroPowerRandomLines = this.GetBossHeroPowerRandomLines();
		string text = "";
		while (bossHeroPowerRandomLines.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, bossHeroPowerRandomLines.Count);
			text = bossHeroPowerRandomLines[index];
			bossHeroPowerRandomLines.RemoveAt(index);
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
		}
		if (text == "")
		{
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce(text, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06003A43 RID: 14915 RVA: 0x0012C95C File Offset: 0x0012AB5C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		if (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield break;
		}
		if (entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		if (entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			this.OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	// Token: 0x06003A44 RID: 14916 RVA: 0x0012C972 File Offset: 0x0012AB72
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string playerHeroCardID = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardID = entity.GetCardId();
		if (playerHeroCardID == "DALA_Chu")
		{
			string text = cardID;
			if (!(text == "CFM_631"))
			{
				if (!(text == "EX1_407"))
				{
					if (!(text == "ICC_837"))
					{
						if (!(text == "CFM_940"))
						{
							if (text == "CFM_716")
							{
								yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Chu_Male_Pandaren_TRIGGER_SLEEP_WITH_THE_FISHES_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Chu_Male_Pandaren_TRIGGER_I_KNOW_A_GUY_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Chu_Male_Pandaren_TRIGGER_BRING_IT_ON_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Chu_Male_Pandaren_TRIGGER_BRAWL_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Chu_Male_Pandaren_TRIGGER_BRASS_KNUCKLES_01, 2.5f);
			}
		}
		if (playerHeroCardID == "DALA_Vessina")
		{
			string text = cardID;
			if (!(text == "CS2_046"))
			{
				if (!(text == "LOOT_373"))
				{
					if (!(text == "EX1_259"))
					{
						if (!(text == "CS2_051"))
						{
							if (text == "DALA_711")
							{
								yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_TREASURE_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_STONECLAW_TOTEM_RARE_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_LIGHTNINGSPELL_02, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_HEALINGRAIN_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_BLOODLUST_01, 2.5f);
			}
		}
		if (playerHeroCardID == "DALA_Eudora")
		{
			string text = cardID;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1313737616U)
			{
				if (num != 91272631U)
				{
					if (num != 1098742518U)
					{
						if (num == 1313737616U)
						{
							if (text == "DALA_723")
							{
								yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_HYPERBLASTER_01, 2.5f);
							}
						}
					}
					else if (text == "TRL_127")
					{
						yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_CANNON_BARRAGE_01, 2.5f);
					}
				}
				else if (text == "GIL_687")
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_WANTED_01, 2.5f);
				}
			}
			else if (num <= 1713316517U)
			{
				if (num != 1449563635U)
				{
					if (num == 1713316517U)
					{
						if (text == "CS2_076")
						{
							yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_ASSASSINATE_01, 2.5f);
						}
					}
				}
				else if (text == "NEW1_004")
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_VANISH_01, 2.5f);
				}
			}
			else if (num != 2468125649U)
			{
				if (num == 2797504017U)
				{
					if (text == "LOOT_542")
					{
						yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_GUN_01, 2.5f);
					}
				}
			}
			else if (text == "EX1_137")
			{
				yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Eudora_Female_Vulpine_TRIGGER_HEADCRACK_01, 2.5f);
			}
		}
		if (playerHeroCardID == "DALA_George")
		{
			string text = cardID;
			if (!(text == "UNG_960"))
			{
				if (text == "UNG_950")
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_George_Male_Human_TRIGGER_VINE_CLEAVER_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_George_Male_Human_TRIGGER_LOST_IN_THE_JUNGLE_01, 2.5f);
			}
		}
		if (playerHeroCardID == "DALA_Barkeye")
		{
			string text = cardID;
			if (!(text == "GIL_152"))
			{
				if (!(text == "BOT_033"))
				{
					if (!(text == "CS2_084"))
					{
						if (!(text == "DALA_723"))
						{
							if (text == "AT_061")
							{
								yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Barkeye_Male_Gnoll_TRIGGER_LOCKANDLOAD_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Barkeye_Male_Gnoll_TRIGGER_DEADEYE_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Barkeye_Male_Gnoll_TRIGGER_HUNTERSMARK_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BOMBTOSS_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BLACKHOWLGUNSPIRE_01, 2.5f);
			}
		}
		if (playerHeroCardID == "DALA_Squeamlish")
		{
			string text = cardID;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1780721183U)
			{
				if (num <= 1380848092U)
				{
					if (num != 89845608U)
					{
						if (num == 1380848092U)
						{
							if (text == "DALA_727")
							{
								yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SOW_THE_SEEDS_01, 2.5f);
							}
						}
					}
					else if (text == "GIL_637")
					{
						yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_ARMOR_01, 2.5f);
					}
				}
				else if (num != 1414697520U)
				{
					if (num == 1780721183U)
					{
						if (text == "CS2_012")
						{
							yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SWIPE_01, 2.5f);
						}
					}
				}
				else if (text == "DALA_709")
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_GOLDENCANDLE_01, 2.5f);
				}
			}
			else if (num <= 2146733097U)
			{
				if (num != 1847684564U)
				{
					if (num == 2146733097U)
					{
						if (text == "LOOT_069")
						{
							yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_RAT_01, 2.5f);
						}
					}
				}
				else if (text == "CS2_008")
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_MOONFIRE_01, 2.5f);
				}
			}
			else if (num != 3142034307U)
			{
				if (num == 3621261496U)
				{
					if (text == "GVG_033")
					{
						yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_HEALINGSPELL_01, 2.5f);
					}
				}
			}
			else if (text == "FP1_019")
			{
				yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Squeamlish_Female_Kobold_TRIGGER_POISONSEEDS_01, 2.5f);
			}
		}
		if (playerHeroCardID == "DALA_Tekahn")
		{
			string text = cardID;
			if (!(text == "GILA_500p2t"))
			{
				if (!(text == "LOOT_417"))
				{
					if (!(text == "LOOT_017"))
					{
						if (!(text == "OG_118"))
						{
							if (text == "ICC_469")
							{
								yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Tekahn_Male_TolVir_TRIGGER_UNWILLINGSACRIFICE_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Tekahn_Male_TolVir_TRIGGER_RENOUNCEDARKNESS_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Tekahn_Male_TolVir_TRIGGER_DARKPACT_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Tekahn_Male_TolVir_TRIGGER_CATACLYSM_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Tekahn_Male_TolVir_TRIGGER_ASMALLROCK_01, 2.5f);
			}
		}
		if (playerHeroCardID == "DALA_Rakanishu")
		{
			string text = cardID;
			if (!(text == "CS2_028"))
			{
				if (!(text == "CS2_029"))
				{
					if (!(text == "LOE_002"))
					{
						if (!(text == "UNG_955"))
						{
							if (!(text == "LOOTA_843"))
							{
								if (text == "UNG_817")
								{
									yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_TIDALSURGE_01, 2.5f);
								}
							}
							else
							{
								yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_THECANDLE_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_METEOR_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FORGOTTENTORCH_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FIREBALL_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Rakanishu_Male_Elemental_TRIGGER_BLIZZARD_01, 2.5f);
			}
		}
		if (playerHeroCardID == "DALA_Kriziki")
		{
			string text = cardID;
			if (!(text == "BOT_219"))
			{
				if (!(text == "DALA_716"))
				{
					if (!(text == "EX1_625"))
					{
						if (text == "OG_100")
						{
							yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWWORDHORROR_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWFORM_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_FLYBY_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(playerActor, DALA_Dungeon.VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_EXTRAARMS_01, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x06003A45 RID: 14917 RVA: 0x0012C988 File Offset: 0x0012AB88
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (!this.m_enemySpeaking && !string.IsNullOrEmpty(this.m_deathLine) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (this.GetShouldSuppressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_deathLine, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_deathLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x06003A46 RID: 14918 RVA: 0x0012CA28 File Offset: 0x0012AC28
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003A47 RID: 14919 RVA: 0x0012CAB4 File Offset: 0x0012ACB4
	protected string PopRandomLine(List<string> lines)
	{
		if (lines.Count == 0 || lines == null)
		{
			return null;
		}
		string text = lines[UnityEngine.Random.Range(0, lines.Count)];
		lines.Remove(text);
		return text;
	}

	// Token: 0x06003A48 RID: 14920 RVA: 0x0012CAEA File Offset: 0x0012ACEA
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
	}

	// Token: 0x06003A49 RID: 14921 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003A4A RID: 14922 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003A4B RID: 14923 RVA: 0x0012CB29 File Offset: 0x0012AD29
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003A4C RID: 14924 RVA: 0x0012CB46 File Offset: 0x0012AD46
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003A4D RID: 14925 RVA: 0x0012CB63 File Offset: 0x0012AD63
	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003A4E RID: 14926 RVA: 0x0012CB80 File Offset: 0x0012AD80
	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003A4F RID: 14927 RVA: 0x0012CB9D File Offset: 0x0012AD9D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		switch (missionEvent)
		{
		case 501:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon.VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_TRAP_01, 2.5f);
			break;
		case 502:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon.VO_DALA_George_Male_Human_TRIGGER_AVENGE_01, 2.5f);
			break;
		case 503:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon.VO_DALA_George_Male_Human_TRIGGER_NOBLE_SACRIFICE_01, 2.5f);
			break;
		case 504:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon.VO_DALA_George_Male_Human_TRIGGER_EYE_FOR_AN_EYE_01, 2.5f);
			break;
		case 505:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01, 2.5f);
			break;
		case 506:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01, 2.5f);
			break;
		case 507:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01, 2.5f);
			break;
		case 508:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01, 2.5f);
			break;
		case 509:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01, 2.5f);
			break;
		case 510:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01, 2.5f);
			break;
		case 511:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01, 2.5f);
			break;
		case 512:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01, 2.5f);
			break;
		case 513:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01, 2.5f);
			break;
		case 514:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01, 2.5f);
			break;
		case 515:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01, 2.5f);
			break;
		case 516:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01, 2.5f);
			break;
		case 517:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01, 2.5f);
			break;
		case 518:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01, 2.5f);
			break;
		case 519:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01, 2.5f);
			break;
		case 520:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01, 2.5f);
			break;
		case 521:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01, 2.5f);
			break;
		case 522:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01, 2.5f);
			break;
		case 523:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01, 2.5f);
			break;
		case 524:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01, 2.5f);
			break;
		case 525:
			yield return base.PlayBossLine(DALA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, DALA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01, 2.5f);
			break;
		default:
			switch (missionEvent)
			{
			case 1000:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				if (this.m_PlayPlayerVOLineIndex + 1 >= this.m_PlayerVOLines.Count)
				{
					this.m_PlayPlayerVOLineIndex = 0;
				}
				else
				{
					this.m_PlayPlayerVOLineIndex++;
				}
				SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
				yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
				break;
			case 1001:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
				yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
				break;
			case 1002:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				if (this.m_PlayBossVOLineIndex + 1 >= this.m_BossVOLines.Count)
				{
					this.m_PlayBossVOLineIndex = 0;
				}
				else
				{
					this.m_PlayBossVOLineIndex++;
				}
				SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
				yield return base.PlayBossLine(actor2, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
				break;
			case 1003:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
				yield return base.PlayBossLine(actor2, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
				break;
			default:
				yield return base.HandleMissionEventWithTiming(missionEvent);
				break;
			}
			break;
		}
		yield break;
	}

	// Token: 0x06003A50 RID: 14928 RVA: 0x0012CBB4 File Offset: 0x0012ADB4
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (playerSide == Player.Side.OPPOSING)
		{
			if (missionId == 3005)
			{
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_01");
			}
			if (missionId == 3188)
			{
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_02");
			}
			if (missionId == 3189)
			{
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_03");
			}
			if (missionId == 3190)
			{
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_04");
			}
			if (missionId == 3191)
			{
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_05");
			}
			if (missionId == 3328)
			{
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_01_HEROIC");
			}
			if (missionId == 3329)
			{
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_02_HEROIC");
			}
			if (missionId == 3330)
			{
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_03_HEROIC");
			}
			if (missionId == 3331)
			{
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_04_HEROIC");
			}
			if (missionId == 3332)
			{
				return GameStrings.Get("DALA_MISSION_CHAPTER_SUBTEXT_05_HEROIC");
			}
		}
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x06003A51 RID: 14929 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06003A52 RID: 14930 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06003A53 RID: 14931 RVA: 0x0012CCA0 File Offset: 0x0012AEA0
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		float thinkEmoteBossThinkChancePercentage = this.GetThinkEmoteBossThinkChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num && this.m_BossIdleLines != null && this.m_BossIdleLines.Count != 0)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			string line = this.PopRandomLine(this.m_BossIdleLinesCopy);
			if (this.m_BossIdleLinesCopy.Count == 0)
			{
				this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
			}
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
			return;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (UnityEngine.Random.Range(1, 4))
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
		GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
	}

	// Token: 0x040021AD RID: 8621
	private static readonly AssetReference VO_DALA_Chu_Male_Pandaren_TRIGGER_BRASS_KNUCKLES_01 = new AssetReference("VO_DALA_Chu_Male_Pandaren_TRIGGER_BRASS_KNUCKLES_01.prefab:2c4cbc9705397b44e9453659b62d06c2");

	// Token: 0x040021AE RID: 8622
	private static readonly AssetReference VO_DALA_Chu_Male_Pandaren_TRIGGER_BRAWL_01 = new AssetReference("VO_DALA_Chu_Male_Pandaren_TRIGGER_BRAWL_01.prefab:43cf4ef5784d68d4ea9274db416df2a3");

	// Token: 0x040021AF RID: 8623
	private static readonly AssetReference VO_DALA_Chu_Male_Pandaren_TRIGGER_BRING_IT_ON_01 = new AssetReference("VO_DALA_Chu_Male_Pandaren_TRIGGER_BRING_IT_ON_01.prefab:fdd06370873540b498ed7994e84986aa");

	// Token: 0x040021B0 RID: 8624
	private static readonly AssetReference VO_DALA_Chu_Male_Pandaren_TRIGGER_I_KNOW_A_GUY_01 = new AssetReference("VO_DALA_Chu_Male_Pandaren_TRIGGER_I_KNOW_A_GUY_01.prefab:8106846e7f54b84459cca5580a770631");

	// Token: 0x040021B1 RID: 8625
	private static readonly AssetReference VO_DALA_Chu_Male_Pandaren_TRIGGER_SLEEP_WITH_THE_FISHES_01 = new AssetReference("VO_DALA_Chu_Male_Pandaren_TRIGGER_SLEEP_WITH_THE_FISHES_01.prefab:5eb12ff2a7ce74a41ba58450ff9a0791");

	// Token: 0x040021B2 RID: 8626
	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_BLOODLUST_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_BLOODLUST_01.prefab:cd8cd403730328540a4390e923438ca9");

	// Token: 0x040021B3 RID: 8627
	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_HEALINGRAIN_02 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_HEALINGRAIN_02.prefab:bf9a4f3fd56c5114cb59ce3b86bccddd");

	// Token: 0x040021B4 RID: 8628
	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_LIGHTNINGSPELL_02 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_LIGHTNINGSPELL_02.prefab:668b1af03bc17f6439d95c15b4dfc96f");

	// Token: 0x040021B5 RID: 8629
	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_RAINOFTOADS_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_RAINOFTOADS_01.prefab:3cf010d85a7d1834aaf7acda6da2f6f6");

	// Token: 0x040021B6 RID: 8630
	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_01.prefab:b55e5dc8c5f5a9c46a04b83026eda601");

	// Token: 0x040021B7 RID: 8631
	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_STONECLAW_TOTEM_RARE_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_STONECLAW_TOTEM_RARE_01.prefab:93ef32ef616ba034ab9c57383f8943d9");

	// Token: 0x040021B8 RID: 8632
	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_TREASURE_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_TREASURE_01.prefab:236d088e880cd6f41a8a5a6a18772f66");

	// Token: 0x040021B9 RID: 8633
	private static readonly AssetReference VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_TRAP_01 = new AssetReference("VO_DALA_Vessina_Female_Sethrak_TRIGGER_SNAKE_TRAP_01.prefab:0b0fc5a03d0380c4f87ae6fadec13e75");

	// Token: 0x040021BA RID: 8634
	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_ASSASSINATE_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_ASSASSINATE_01.prefab:877041aad0f1e9747ad1d86470cd414c");

	// Token: 0x040021BB RID: 8635
	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_CANNON_BARRAGE_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_CANNON_BARRAGE_01.prefab:090751b1c2cbe50479545d60dd46ab19");

	// Token: 0x040021BC RID: 8636
	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_GUN_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_GUN_01.prefab:3cd70de36b3fee64f92804e2f26794d2");

	// Token: 0x040021BD RID: 8637
	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_HEADCRACK_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_HEADCRACK_01.prefab:5ea88503051703e4c9ee2deb097c0cdf");

	// Token: 0x040021BE RID: 8638
	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_HYPERBLASTER_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_HYPERBLASTER_01.prefab:6f98a3da7287667449f42e3c3ecfaee6");

	// Token: 0x040021BF RID: 8639
	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_VANISH_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_VANISH_01.prefab:156cd6142e1d41a42a7c6401cec174c2");

	// Token: 0x040021C0 RID: 8640
	private static readonly AssetReference VO_DALA_Eudora_Female_Vulpine_TRIGGER_WANTED_01 = new AssetReference("VO_DALA_Eudora_Female_Vulpine_TRIGGER_WANTED_01.prefab:3b36218cb68744b40a2ca4b53929e059");

	// Token: 0x040021C1 RID: 8641
	private static readonly AssetReference VO_DALA_George_Male_Human_TRIGGER_AVENGE_01 = new AssetReference("VO_DALA_George_Male_Human_TRIGGER_AVENGE_01.prefab:313842461f19453458016944f7134199");

	// Token: 0x040021C2 RID: 8642
	private static readonly AssetReference VO_DALA_George_Male_Human_TRIGGER_EYE_FOR_AN_EYE_01 = new AssetReference("VO_DALA_George_Male_Human_TRIGGER_EYE_FOR_AN_EYE_01.prefab:73299c008d07e424683dffad6fd99fb3");

	// Token: 0x040021C3 RID: 8643
	private static readonly AssetReference VO_DALA_George_Male_Human_TRIGGER_LOST_IN_THE_JUNGLE_01 = new AssetReference("VO_DALA_George_Male_Human_TRIGGER_LOST_IN_THE_JUNGLE_01.prefab:fbd4cbef88e8e8e4f99caa2371be5a56");

	// Token: 0x040021C4 RID: 8644
	private static readonly AssetReference VO_DALA_George_Male_Human_TRIGGER_NOBLE_SACRIFICE_01 = new AssetReference("VO_DALA_George_Male_Human_TRIGGER_NOBLE_SACRIFICE_01.prefab:07da9e521919d6b4f9d86dc04b166488");

	// Token: 0x040021C5 RID: 8645
	private static readonly AssetReference VO_DALA_George_Male_Human_TRIGGER_VINE_CLEAVER_01 = new AssetReference("VO_DALA_George_Male_Human_TRIGGER_VINE_CLEAVER_01.prefab:4aff78a94764f8b478353d0a1dbb23ab");

	// Token: 0x040021C6 RID: 8646
	private static readonly AssetReference VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BLACKHOWLGUNSPIRE_01 = new AssetReference("VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BLACKHOWLGUNSPIRE_01.prefab:e6ec71b58fb7f80498149c9861e58e0d");

	// Token: 0x040021C7 RID: 8647
	private static readonly AssetReference VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BOMBTOSS_01 = new AssetReference("VO_DALA_Barkeye_Male_Gnoll_TRIGGER_BOMBTOSS_01.prefab:2f74d357d42a3b040842cbeec69ae76a");

	// Token: 0x040021C8 RID: 8648
	private static readonly AssetReference VO_DALA_Barkeye_Male_Gnoll_TRIGGER_DEADEYE_01 = new AssetReference("VO_DALA_Barkeye_Male_Gnoll_TRIGGER_DEADEYE_01.prefab:d96787c9f81228d4aa7118c4cee87853");

	// Token: 0x040021C9 RID: 8649
	private static readonly AssetReference VO_DALA_Barkeye_Male_Gnoll_TRIGGER_HUNTERSMARK_01 = new AssetReference("VO_DALA_Barkeye_Male_Gnoll_TRIGGER_HUNTERSMARK_01.prefab:d17bf8714b62fb54fb7bfea3f74889dd");

	// Token: 0x040021CA RID: 8650
	private static readonly AssetReference VO_DALA_Barkeye_Male_Gnoll_TRIGGER_LOCKANDLOAD_01 = new AssetReference("VO_DALA_Barkeye_Male_Gnoll_TRIGGER_LOCKANDLOAD_01.prefab:41720cb7042277845a21ad490273c136");

	// Token: 0x040021CB RID: 8651
	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_ARMOR_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_ARMOR_01.prefab:a0dc154cad074904bb8481bdb4da3956");

	// Token: 0x040021CC RID: 8652
	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_GOLDENCANDLE_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_GOLDENCANDLE_01.prefab:f47d9ff737531034b85ec8830e90b5e8");

	// Token: 0x040021CD RID: 8653
	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_HEALINGSPELL_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_HEALINGSPELL_01.prefab:ebcf82f3f17f2434dbe790ef6fd07d36");

	// Token: 0x040021CE RID: 8654
	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_MOONFIRE_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_MOONFIRE_01.prefab:57030794a70f1e94abb060f2aaba5ed1");

	// Token: 0x040021CF RID: 8655
	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_POISONSEEDS_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_POISONSEEDS_01.prefab:774ffff7d3ae4d543a9c9ef58ca18dca");

	// Token: 0x040021D0 RID: 8656
	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_RAT_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_RAT_01.prefab:29d43d0fdb3b1f54e923bef3d056591c");

	// Token: 0x040021D1 RID: 8657
	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SOW_THE_SEEDS_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SOW_THE_SEEDS_01.prefab:7dfe985e52db89e479513db2f02e44c5");

	// Token: 0x040021D2 RID: 8658
	private static readonly AssetReference VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SWIPE_01 = new AssetReference("VO_DALA_Squeamlish_Female_Kobold_TRIGGER_SWIPE_01.prefab:f23e0efeb3665e541a125f711d60e917");

	// Token: 0x040021D3 RID: 8659
	private static readonly AssetReference VO_DALA_Tekahn_Male_TolVir_TRIGGER_ASMALLROCK_01 = new AssetReference("VO_DALA_Tekahn_Male_TolVir_TRIGGER_ASMALLROCK_01.prefab:19345793940aa064e99df516b1a251aa");

	// Token: 0x040021D4 RID: 8660
	private static readonly AssetReference VO_DALA_Tekahn_Male_TolVir_TRIGGER_CATACLYSM_01 = new AssetReference("VO_DALA_Tekahn_Male_TolVir_TRIGGER_CATACLYSM_01.prefab:378a151f50f6e764096599c1fa0089d7");

	// Token: 0x040021D5 RID: 8661
	private static readonly AssetReference VO_DALA_Tekahn_Male_TolVir_TRIGGER_DARKPACT_01 = new AssetReference("VO_DALA_Tekahn_Male_TolVir_TRIGGER_DARKPACT_01.prefab:30870391b0a521b46a9d665f9c0fe687");

	// Token: 0x040021D6 RID: 8662
	private static readonly AssetReference VO_DALA_Tekahn_Male_TolVir_TRIGGER_RENOUNCEDARKNESS_01 = new AssetReference("VO_DALA_Tekahn_Male_TolVir_TRIGGER_RENOUNCEDARKNESS_01.prefab:ddf3dc76467c1de49bc100c32f4dde9c");

	// Token: 0x040021D7 RID: 8663
	private static readonly AssetReference VO_DALA_Tekahn_Male_TolVir_TRIGGER_UNWILLINGSACRIFICE_01 = new AssetReference("VO_DALA_Tekahn_Male_TolVir_TRIGGER_UNWILLINGSACRIFICE_01.prefab:c833be43965c0524199a2e64d9fda1ae");

	// Token: 0x040021D8 RID: 8664
	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_BLIZZARD_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_BLIZZARD_01.prefab:775f2886ed07122468873ca2eadb4945");

	// Token: 0x040021D9 RID: 8665
	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FIREBALL_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FIREBALL_01.prefab:d4c52635e1716144a828dddec7bf5825");

	// Token: 0x040021DA RID: 8666
	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FORGOTTENTORCH_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_FORGOTTENTORCH_01.prefab:52f77e5abbea9834287485cdb85f84a8");

	// Token: 0x040021DB RID: 8667
	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_METEOR_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_METEOR_01.prefab:d8f2d1e9e1b7a6549acb33b1ef1249e2");

	// Token: 0x040021DC RID: 8668
	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_THECANDLE_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_THECANDLE_01.prefab:e99721cf2171c9c49968f2a76acc1cbe");

	// Token: 0x040021DD RID: 8669
	private static readonly AssetReference VO_DALA_Rakanishu_Male_Elemental_TRIGGER_TIDALSURGE_01 = new AssetReference("VO_DALA_Rakanishu_Male_Elemental_TRIGGER_TIDALSURGE_01.prefab:df469759b9204354da20c6f3f17510aa");

	// Token: 0x040021DE RID: 8670
	private static readonly AssetReference VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_EXTRAARMS_01 = new AssetReference("VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_EXTRAARMS_01.prefab:915a9729a4a759b469a440946b9d6cb2");

	// Token: 0x040021DF RID: 8671
	private static readonly AssetReference VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_FLYBY_01 = new AssetReference("VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_FLYBY_01.prefab:2e7f49708c744ca4ea1fa1ab2997cd97");

	// Token: 0x040021E0 RID: 8672
	private static readonly AssetReference VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWFORM_01 = new AssetReference("VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWFORM_01.prefab:1ddff86cd0dd2ca43967d64d283f1d2e");

	// Token: 0x040021E1 RID: 8673
	private static readonly AssetReference VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWWORDHORROR_01 = new AssetReference("VO_DALA_Kriziki_Female_Arakkoa_TRIGGER_SHADOWWORDHORROR_01.prefab:ef8e4e0e8e19d78428c1a7a2f933bd9e");

	// Token: 0x040021E2 RID: 8674
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01.prefab:bbb71f2dfac1c474da3209a15215f4ea");

	// Token: 0x040021E3 RID: 8675
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01.prefab:ad01bc4d23eab3e4f86c994d722cf247");

	// Token: 0x040021E4 RID: 8676
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01.prefab:688849bb5a4d2fd48a817159d1a224fa");

	// Token: 0x040021E5 RID: 8677
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01.prefab:001c634fac4ab874eb6e16d050554b1f");

	// Token: 0x040021E6 RID: 8678
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01.prefab:a5d093ea8ec90d64fb53834b44395720");

	// Token: 0x040021E7 RID: 8679
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01.prefab:97a9c24cb50609347964387b02a62b3c");

	// Token: 0x040021E8 RID: 8680
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01.prefab:feb7534214a13214bac9e2b8726a8cc8");

	// Token: 0x040021E9 RID: 8681
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01.prefab:3fd9e692fe70e924fa0fb5bfabcf17bd");

	// Token: 0x040021EA RID: 8682
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01.prefab:701ed0ac22ab9a84daade7ed23403317");

	// Token: 0x040021EB RID: 8683
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01.prefab:f2eeac3ed0b6f554dbfbc9deea60739f");

	// Token: 0x040021EC RID: 8684
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01.prefab:f9054e8df8774e44d869a0f96ac07efa");

	// Token: 0x040021ED RID: 8685
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01.prefab:d0a3f9b5c01e04d458178ca8c5069d66");

	// Token: 0x040021EE RID: 8686
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01.prefab:ab486ac19b475c74f84999cc9a80b7a6");

	// Token: 0x040021EF RID: 8687
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01.prefab:8789714bb9a92d143bb2024188b8ddd0");

	// Token: 0x040021F0 RID: 8688
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01.prefab:7fd61e2a38015f240bef703ee9f66e5c");

	// Token: 0x040021F1 RID: 8689
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01.prefab:9273a8457f705514f9755153f0c7abf6");

	// Token: 0x040021F2 RID: 8690
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01.prefab:cad9725371b04e943b07f43ecac56b32");

	// Token: 0x040021F3 RID: 8691
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01.prefab:ce7a5a15de006d041ad515427fc6f72f");

	// Token: 0x040021F4 RID: 8692
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01.prefab:92c7854b16b6919499ff3fe7e1e2a422");

	// Token: 0x040021F5 RID: 8693
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01.prefab:3edd1b61bd705b6439dd75542dd6b442");

	// Token: 0x040021F6 RID: 8694
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01.prefab:b9fae030ab3026a4bb17f592028c276d");

	// Token: 0x040021F7 RID: 8695
	private static readonly AssetReference Wisdomball_Pop_up_BrassRing_Quote = new AssetReference("Wisdomball_Pop-up_BrassRing_Quote.prefab:896ee20514caff74db639aa7055838f6");

	// Token: 0x040021F8 RID: 8696
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x040021F9 RID: 8697
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x040021FA RID: 8698
	public string m_introLine;

	// Token: 0x040021FB RID: 8699
	public string m_deathLine;

	// Token: 0x040021FC RID: 8700
	public string m_standardEmoteResponseLine;

	// Token: 0x040021FD RID: 8701
	public List<string> m_BossIdleLines;

	// Token: 0x040021FE RID: 8702
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x040021FF RID: 8703
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x04002200 RID: 8704
	private int m_PlayBossVOLineIndex;
}
