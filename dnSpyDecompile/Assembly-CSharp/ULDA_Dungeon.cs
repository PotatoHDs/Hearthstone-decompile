using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200047D RID: 1149
public class ULDA_Dungeon : ULDA_MissionEntity
{
	// Token: 0x06003E2A RID: 15914 RVA: 0x00148078 File Offset: 0x00146278
	public static ULDA_Dungeon InstantiateULDADungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(opposingHeroCardID);
		if (num <= 1769281293U)
		{
			if (num <= 693405733U)
			{
				if (num <= 462188849U)
				{
					if (num <= 253497687U)
					{
						if (num <= 134981145U)
						{
							if (num != 61949381U)
							{
								if (num != 118203526U)
								{
									if (num != 134981145U)
									{
										goto IL_11CA;
									}
									if (!(opposingHeroCardID == "ULDA_BOSS_37h3"))
									{
										goto IL_11CA;
									}
								}
								else if (!(opposingHeroCardID == "ULDA_BOSS_37h2"))
								{
									goto IL_11CA;
								}
							}
							else
							{
								if (!(opposingHeroCardID == "ULDA_BOSS_43h"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_43h();
							}
						}
						else if (num != 176960692U)
						{
							if (num != 202989764U)
							{
								if (num != 253497687U)
								{
									goto IL_11CA;
								}
								if (!(opposingHeroCardID == "TB_TempleOutrun_Battrund"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_31h();
							}
							else
							{
								if (!(opposingHeroCardID == "TB_TempleOutrun_Colossus"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_22h();
							}
						}
						else
						{
							if (!(opposingHeroCardID == "TB_TempleOutrun_Isiset"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_69h();
						}
					}
					else if (num <= 395372563U)
					{
						if (num != 268630293U)
						{
							if (num != 363107880U)
							{
								if (num != 395372563U)
								{
									goto IL_11CA;
								}
								if (!(opposingHeroCardID == "ULDA_BOSS_16h"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_16h();
							}
							else
							{
								if (!(opposingHeroCardID == "ULDA_BOSS_46h"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_46h();
							}
						}
						else
						{
							if (!(opposingHeroCardID == "TB_TempleOutrun_Pillager"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_31h();
						}
					}
					else if (num <= 432484022U)
					{
						if (num != 429472254U)
						{
							if (num != 432484022U)
							{
								goto IL_11CA;
							}
							if (!(opposingHeroCardID == "ULDA_BOSS_48h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_48h();
						}
						else
						{
							if (!(opposingHeroCardID == "ULDA_BOSS_13h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_13h();
						}
					}
					else if (num != 458960352U)
					{
						if (num != 462188849U)
						{
							goto IL_11CA;
						}
						if (!(opposingHeroCardID == "ULDA_BOSS_14h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_14h();
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_68h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_68h();
					}
				}
				else if (num <= 558493233U)
				{
					if (num <= 506454752U)
					{
						if (num != 491676947U)
						{
							if (num != 498020380U)
							{
								if (num != 506454752U)
								{
									goto IL_11CA;
								}
								if (!(opposingHeroCardID == "TB_TempleOutrun_Kham"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_20h();
							}
							else
							{
								if (!(opposingHeroCardID == "ULDA_BOSS_42h"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_42h();
							}
						}
						else
						{
							if (!(opposingHeroCardID == "ULDA_BOSS_63h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_63h();
						}
					}
					else if (num != 525776638U)
					{
						if (num != 532016903U)
						{
							if (num != 558493233U)
							{
								goto IL_11CA;
							}
							if (!(opposingHeroCardID == "ULDA_BOSS_61h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_61h();
						}
						else
						{
							if (!(opposingHeroCardID == "ULDA_BOSS_41h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_41h();
						}
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_66h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_66h();
					}
				}
				else if (num <= 597101349U)
				{
					if (num != 564733498U)
					{
						if (num != 592220140U)
						{
							if (num != 597101349U)
							{
								goto IL_11CA;
							}
							if (!(opposingHeroCardID == "ULDA_BOSS_10h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_10h();
						}
						else
						{
							if (!(opposingHeroCardID == "ULDA_BOSS_38h3"))
							{
								goto IL_11CA;
							}
							goto IL_105C;
						}
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_44h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_44h();
					}
				}
				else if (num <= 608997759U)
				{
					if (num != 598381277U)
					{
						if (num != 608997759U)
						{
							goto IL_11CA;
						}
						if (!(opposingHeroCardID == "ULDA_BOSS_38h2"))
						{
							goto IL_11CA;
						}
						goto IL_105C;
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_18h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_18h();
					}
				}
				else if (num != 683192993U)
				{
					if (num != 693405733U)
					{
						goto IL_11CA;
					}
					if (!(opposingHeroCardID == "ULDA_BOSS_65h"))
					{
						goto IL_11CA;
					}
					return new ULDA_Dungeon_Boss_65h();
				}
				else
				{
					if (!(opposingHeroCardID == "TB_TempleOutrun_Toomba"))
					{
						goto IL_11CA;
					}
					return new ULDA_Dungeon_Boss_27h();
				}
			}
			else
			{
				if (num > 1164301271U)
				{
					if (num <= 1567655675U)
					{
						if (num <= 1196189850U)
						{
							if (num != 1174685204U)
							{
								if (num != 1191462823U)
								{
									if (num != 1196189850U)
									{
										goto IL_11CA;
									}
									if (!(opposingHeroCardID == "ULDA_BOSS_62h"))
									{
										goto IL_11CA;
									}
									return new ULDA_Dungeon_Boss_62h();
								}
								else if (!(opposingHeroCardID == "ULDA_BOSS_39h3"))
								{
									goto IL_11CA;
								}
							}
							else if (!(opposingHeroCardID == "ULDA_BOSS_39h2"))
							{
								goto IL_11CA;
							}
						}
						else if (num <= 1533555984U)
						{
							if (num != 1288043111U)
							{
								if (num != 1533555984U)
								{
									goto IL_11CA;
								}
								if (!(opposingHeroCardID == "ULDA_BOSS_37h"))
								{
									goto IL_11CA;
								}
								goto IL_1056;
							}
							else
							{
								if (!(opposingHeroCardID == "TB_TempleOutrun_Octosari"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_78h();
							}
						}
						else if (num != 1562624025U)
						{
							if (num != 1567655675U)
							{
								goto IL_11CA;
							}
							if (!(opposingHeroCardID == "ULDA_BOSS_34h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_34h();
						}
						else
						{
							if (!(opposingHeroCardID == "TB_TempleOutrun_TrapRoom"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_44h();
						}
					}
					else if (num <= 1702465007U)
					{
						if (num != 1600372270U)
						{
							if (num != 1647636952U)
							{
								if (num != 1702465007U)
								{
									goto IL_11CA;
								}
								if (!(opposingHeroCardID == "ULDA_BOSS_30h"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_30h();
							}
							else
							{
								if (!(opposingHeroCardID == "ULDA_BOSS_59h"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_59h();
							}
						}
						else if (!(opposingHeroCardID == "ULDA_BOSS_39h"))
						{
							goto IL_11CA;
						}
					}
					else if (num <= 1735181602U)
					{
						if (num != 1717013094U)
						{
							if (num != 1735181602U)
							{
								goto IL_11CA;
							}
							if (!(opposingHeroCardID == "ULDA_BOSS_35h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_35h();
						}
						else
						{
							if (!(opposingHeroCardID == "ULDA_BOSS_57h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_57h();
						}
					}
					else if (num != 1749729689U)
					{
						if (num != 1769281293U)
						{
							goto IL_11CA;
						}
						if (!(opposingHeroCardID == "ULDA_BOSS_32h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_32h();
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_50h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_50h();
					}
					return new ULDA_Dungeon_Boss_39h();
				}
				if (num <= 1067168871U)
				{
					if (num <= 1033069180U)
					{
						if (num != 898259848U)
						{
							if (num != 994564232U)
							{
								if (num != 1033069180U)
								{
									goto IL_11CA;
								}
								if (!(opposingHeroCardID == "ULDA_BOSS_11h"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_11h();
							}
							else
							{
								if (!(opposingHeroCardID == "ULDA_BOSS_60h"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_60h();
							}
						}
						else
						{
							if (!(opposingHeroCardID == "ULDA_BOSS_15h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_15h();
						}
					}
					else if (num != 1034452276U)
					{
						if (num != 1064768390U)
						{
							if (num != 1067168871U)
							{
								goto IL_11CA;
							}
							if (!(opposingHeroCardID == "ULDA_BOSS_12h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_12h();
						}
						else
						{
							if (!(opposingHeroCardID == "ULDA_BOSS_31h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_31h();
						}
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_19h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_19h();
					}
				}
				else if (num <= 1097484985U)
				{
					if (num != 1096341957U)
					{
						if (num != 1096656969U)
						{
							if (num != 1097484985U)
							{
								goto IL_11CA;
							}
							if (!(opposingHeroCardID == "ULDA_BOSS_36h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_36h();
						}
						else
						{
							if (!(opposingHeroCardID == "ULDA_BOSS_69h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_69h();
						}
					}
					else
					{
						if (!(opposingHeroCardID == "TB_TempleOutrun_Jythiros"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_58h();
					}
				}
				else if (num <= 1131584676U)
				{
					if (num != 1099885466U)
					{
						if (num != 1131584676U)
						{
							goto IL_11CA;
						}
						if (!(opposingHeroCardID == "ULDA_BOSS_33h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_33h();
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_17h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_17h();
					}
				}
				else if (num != 1163473255U)
				{
					if (num != 1164301271U)
					{
						goto IL_11CA;
					}
					if (!(opposingHeroCardID == "ULDA_BOSS_38h"))
					{
						goto IL_11CA;
					}
					goto IL_105C;
				}
				else
				{
					if (!(opposingHeroCardID == "ULDA_BOSS_67h"))
					{
						goto IL_11CA;
					}
					return new ULDA_Dungeon_Boss_67h();
				}
			}
			IL_1056:
			return new ULDA_Dungeon_Boss_37h();
			IL_105C:
			return new ULDA_Dungeon_Boss_38h();
		}
		if (num > 3098238591U)
		{
			if (num <= 3704671973U)
			{
				if (num <= 3470329760U)
				{
					if (num <= 3405913955U)
					{
						if (num != 3246435012U)
						{
							if (num != 3263212631U)
							{
								if (num != 3405913955U)
								{
									goto IL_11CA;
								}
								if (!(opposingHeroCardID == "ULDA_BOSS_01h"))
								{
									goto IL_11CA;
								}
								return new ULDA_Dungeon_Boss_01h();
							}
							else if (!(opposingHeroCardID == "ULDA_BOSS_40h3"))
							{
								goto IL_11CA;
							}
						}
						else if (!(opposingHeroCardID == "ULDA_BOSS_40h2"))
						{
							goto IL_11CA;
						}
					}
					else if (num != 3407193883U)
					{
						if (num != 3439910478U)
						{
							if (num != 3470329760U)
							{
								goto IL_11CA;
							}
							if (!(opposingHeroCardID == "ULDA_BOSS_24h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_24h();
						}
						else
						{
							if (!(opposingHeroCardID == "ULDA_BOSS_04h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_04h();
						}
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_09h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_09h();
					}
				}
				else if (num <= 3574822978U)
				{
					if (num != 3472627073U)
					{
						if (num != 3504326283U)
						{
							if (num != 3574822978U)
							{
								goto IL_11CA;
							}
							if (!(opposingHeroCardID == "ULDA_BOSS_08h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_08h();
						}
						else
						{
							if (!(opposingHeroCardID == "ULDA_BOSS_27h"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_27h();
						}
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_03h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_03h();
					}
				}
				else if (num <= 3639238783U)
				{
					if (num != 3607539573U)
					{
						if (num != 3639238783U)
						{
							goto IL_11CA;
						}
						if (!(opposingHeroCardID == "ULDA_BOSS_23h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_23h();
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_07h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_07h();
					}
				}
				else if (num != 3671955378U)
				{
					if (num != 3704671973U)
					{
						goto IL_11CA;
					}
					if (!(opposingHeroCardID == "ULDA_BOSS_29h"))
					{
						goto IL_11CA;
					}
					return new ULDA_Dungeon_Boss_29h();
				}
				else
				{
					if (!(opposingHeroCardID == "ULDA_BOSS_26h"))
					{
						goto IL_11CA;
					}
					return new ULDA_Dungeon_Boss_26h();
				}
			}
			else if (num <= 4140742972U)
			{
				if (num <= 3797039095U)
				{
					if (num != 3705951901U)
					{
						if (num != 3726913760U)
						{
							if (num != 3797039095U)
							{
								goto IL_11CA;
							}
							if (!(opposingHeroCardID == "TB_TempleOutrun_Zaraam"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_52h();
						}
						else
						{
							if (!(opposingHeroCardID == "TB_TempleOutrun_Kasmut"))
							{
								goto IL_11CA;
							}
							return new ULDA_Dungeon_Boss_51h();
						}
					}
					else
					{
						if (!(opposingHeroCardID == "ULDA_BOSS_21h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_21h();
					}
				}
				else if (num <= 4075206614U)
				{
					if (num != 4013650079U)
					{
						if (num != 4075206614U)
						{
							goto IL_11CA;
						}
						if (!(opposingHeroCardID == "ULDA_BOSS_22h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_22h();
					}
					else
					{
						if (!(opposingHeroCardID == "TB_TempleOutrun_Ammunae"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_71h();
					}
				}
				else if (num != 4108026377U)
				{
					if (num != 4140742972U)
					{
						goto IL_11CA;
					}
					if (!(opposingHeroCardID == "ULDA_BOSS_28h"))
					{
						goto IL_11CA;
					}
					return new ULDA_Dungeon_Boss_28h();
				}
				else
				{
					if (!(opposingHeroCardID == "ULDA_BOSS_25h"))
					{
						goto IL_11CA;
					}
					return new ULDA_Dungeon_Boss_25h();
				}
			}
			else if (num <= 4155291059U)
			{
				if (num != 4142022900U)
				{
					if (num != 4143863145U)
					{
						if (num != 4155291059U)
						{
							goto IL_11CA;
						}
						if (!(opposingHeroCardID == "ULDA_BOSS_45h"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_45h();
					}
					else
					{
						if (!(opposingHeroCardID == "TB_TempleOutrun_Setesh"))
						{
							goto IL_11CA;
						}
						return new ULDA_Dungeon_Boss_72h();
					}
				}
				else
				{
					if (!(opposingHeroCardID == "ULDA_BOSS_20h"))
					{
						goto IL_11CA;
					}
					return new ULDA_Dungeon_Boss_20h();
				}
			}
			else if (num <= 4189287582U)
			{
				if (num != 4187157834U)
				{
					if (num != 4189287582U)
					{
						goto IL_11CA;
					}
					if (!(opposingHeroCardID == "ULDA_BOSS_40h"))
					{
						goto IL_11CA;
					}
				}
				else
				{
					if (!(opposingHeroCardID == "TB_TempleOutrun_Jar"))
					{
						goto IL_11CA;
					}
					return new ULDA_Dungeon_Boss_47h();
				}
			}
			else if (num != 4222004177U)
			{
				if (num != 4291380319U)
				{
					goto IL_11CA;
				}
				if (!(opposingHeroCardID == "ULDA_BOSS_49h"))
				{
					goto IL_11CA;
				}
				return new ULDA_Dungeon_Boss_49h();
			}
			else
			{
				if (!(opposingHeroCardID == "ULDA_BOSS_47h"))
				{
					goto IL_11CA;
				}
				return new ULDA_Dungeon_Boss_47h();
			}
			return new ULDA_Dungeon_Boss_40h();
		}
		if (num <= 2421525997U)
		{
			if (num <= 2219797211U)
			{
				if (num <= 2001312973U)
				{
					if (num != 1783829380U)
					{
						if (num != 1831333848U)
						{
							if (num == 2001312973U)
							{
								if (opposingHeroCardID == "TB_TempleOutrun_Zafarr")
								{
									return new ULDA_Dungeon_Boss_02h();
								}
							}
						}
						else if (opposingHeroCardID == "TB_TempleOutrun_LichBazhial")
						{
							return new ULDA_Dungeon_Boss_52h();
						}
					}
					else if (opposingHeroCardID == "ULDA_BOSS_55h")
					{
						return new ULDA_Dungeon_Boss_55h();
					}
				}
				else if (num != 2185800688U)
				{
					if (num != 2214962579U)
					{
						if (num == 2219797211U)
						{
							if (opposingHeroCardID == "ULDA_BOSS_52h")
							{
								return new ULDA_Dungeon_Boss_52h();
							}
						}
					}
					else if (opposingHeroCardID == "TB_TempleOutrun_Ichabod")
					{
						return new ULDA_Dungeon_Boss_17h();
					}
				}
				else if (opposingHeroCardID == "ULDA_BOSS_51h")
				{
					return new ULDA_Dungeon_Boss_51h();
				}
			}
			else if (num <= 2354709711U)
			{
				if (num != 2279093304U)
				{
					if (num != 2285333569U)
					{
						if (num == 2354709711U)
						{
							if (opposingHeroCardID == "ULDA_BOSS_56h")
							{
								return new ULDA_Dungeon_Boss_56h();
							}
						}
					}
					else if (opposingHeroCardID == "ULDA_BOSS_58h")
					{
						return new ULDA_Dungeon_Boss_58h();
					}
				}
				else if (opposingHeroCardID == "ULDA_BOSS_73h")
				{
					return new ULDA_Dungeon_Boss_73h();
				}
			}
			else if (num <= 2387426306U)
			{
				if (num != 2363347926U)
				{
					if (num == 2387426306U)
					{
						if (opposingHeroCardID == "ULDA_BOSS_53h")
						{
							return new ULDA_Dungeon_Boss_53h();
						}
					}
				}
				else if (opposingHeroCardID == "TB_TempleOutrun_Rajh")
				{
					return new ULDA_Dungeon_Boss_70h();
				}
			}
			else if (num != 2413902636U)
			{
				if (num == 2421525997U)
				{
					if (opposingHeroCardID == "ULDA_BOSS_54h")
					{
						return new ULDA_Dungeon_Boss_54h();
					}
				}
			}
			else if (opposingHeroCardID == "ULDA_BOSS_77h")
			{
				return new ULDA_Dungeon_Boss_77h();
			}
		}
		else if (num <= 2851253563U)
		{
			if (num <= 2490248675U)
			{
				if (num != 2448002327U)
				{
					if (num != 2480718922U)
					{
						if (num == 2490248675U)
						{
							if (opposingHeroCardID == "TB_TempleOutrunHHorsema")
							{
								return new TB_TempleOutrun_Headless();
							}
						}
					}
					else if (opposingHeroCardID == "ULDA_BOSS_71h")
					{
						return new ULDA_Dungeon_Boss_71h();
					}
				}
				else if (opposingHeroCardID == "ULDA_BOSS_74h")
				{
					return new ULDA_Dungeon_Boss_74h();
				}
			}
			else if (num != 2834930456U)
			{
				if (num != 2849973635U)
				{
					if (num == 2851253563U)
					{
						if (opposingHeroCardID == "ULDA_BOSS_78h")
						{
							return new ULDA_Dungeon_Boss_78h();
						}
					}
				}
				else if (opposingHeroCardID == "ULDA_BOSS_70h")
				{
					return new ULDA_Dungeon_Boss_70h();
				}
			}
			else if (opposingHeroCardID == "ULDA_BOSS_02h")
			{
				return new ULDA_Dungeon_Boss_02h();
			}
		}
		else if (num <= 2969842956U)
		{
			if (num != 2883970158U)
			{
				if (num != 2916789921U)
				{
					if (num == 2969842956U)
					{
						if (opposingHeroCardID == "ULDA_BOSS_06h")
						{
							return new ULDA_Dungeon_Boss_06h();
						}
					}
				}
				else if (opposingHeroCardID == "ULDA_BOSS_72h")
				{
					return new ULDA_Dungeon_Boss_72h();
				}
			}
			else if (opposingHeroCardID == "ULDA_BOSS_75h")
			{
				return new ULDA_Dungeon_Boss_75h();
			}
		}
		else if (num <= 3018882658U)
		{
			if (num != 3003839479U)
			{
				if (num == 3018882658U)
				{
					if (opposingHeroCardID == "ULDA_BOSS_79h")
					{
						return new ULDA_Dungeon_Boss_79h();
					}
				}
			}
			else if (opposingHeroCardID == "ULDA_BOSS_05h")
			{
				return new ULDA_Dungeon_Boss_05h();
			}
		}
		else if (num != 3051599253U)
		{
			if (num == 3098238591U)
			{
				if (opposingHeroCardID == "TB_TempleOutrun_Sothis")
				{
					return new ULDA_Dungeon_Boss_19h();
				}
			}
		}
		else if (opposingHeroCardID == "ULDA_BOSS_76h")
		{
			return new ULDA_Dungeon_Boss_76h();
		}
		IL_11CA:
		Log.All.PrintError("ULDA_Dungeon.InstantiateULDADungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new ULDA_Dungeon();
	}

	// Token: 0x06003E2B RID: 15915 RVA: 0x00149270 File Offset: 0x00147470
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_TRIGGER_KillCommand_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_TRIGGER_SecretTrap_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_TRIGGER_Whirlwind_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TRIGGER_Innervate_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TRIGGER_Silence_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TRIGGER_SpreadingPlague_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_Trigger_Bloodlust_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_Trigger_Consecration_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_Trigger_Tip_the_Scales_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_TRIGGER_Burgle_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_TRIGGER_Fireball_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_TRIGGER_Frostbolt_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_TUT_PlagueLord_Turn1_Brann_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_Elise_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswerSelf_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_TUT_PlagueLord_Turn1_Finley_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_TUT_PlagueLord_Turn1_Reno_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter01a_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter02a_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter03a_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter04a_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Elise_Chapter02b_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter01b_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter03b_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter04b_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Reno_Chapter01b_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter01b_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter02b_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter03b_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter04b_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter01a_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter02a_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter03a_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter04a_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter02b_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter04b_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Elise_Chapter01b_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter01a_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter02a_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter03a_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter04a_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Reno_Chapter03b_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter03b_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter04b_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Finley_Chapter02b_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter01a_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter02a_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter03a_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter04a_01,
			ULDA_Dungeon.VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_01,
			ULDA_Dungeon.VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_02,
			ULDA_Dungeon.VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_03,
			ULDA_Dungeon.VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_01,
			ULDA_Dungeon.VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_02,
			ULDA_Dungeon.VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_03,
			ULDA_Dungeon.VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_01,
			ULDA_Dungeon.VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_02,
			ULDA_Dungeon.VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_03,
			ULDA_Dungeon.VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_01,
			ULDA_Dungeon.VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_02,
			ULDA_Dungeon.VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_03,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureGatlingWand_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureHat_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureJrExplorer_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureLei_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureTitanRing_01,
			ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureWhip_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseJrExplorer_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_02,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseTreasureDruidStaff_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseTreasureMachete_01,
			ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseTreasureScarabStatue_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_BrannTreasureBlunderbuss_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_BrannTreasureFlo_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_BrannTreasureGiantEgg_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_BrannTreasureJrExplorer_01,
			ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_BrannTreasureSaddle_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasureJrExplorer_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasureKarl_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasureLance_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasureMurkyHorn_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasurePandarenTeaSet_01,
			ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasureScarabMount_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01,
			ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01
		};
		this.m_PlayerVOLines = new List<string>(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003E2C RID: 15916 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06003E2D RID: 15917 RVA: 0x00149978 File Offset: 0x00147B78
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06003E2E RID: 15918 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003E2F RID: 15919 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06003E30 RID: 15920 RVA: 0x00149988 File Offset: 0x00147B88
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

	// Token: 0x06003E31 RID: 15921 RVA: 0x00149A5C File Offset: 0x00147C5C
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

	// Token: 0x06003E32 RID: 15922 RVA: 0x00149A72 File Offset: 0x00147C72
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string playerHeroCardID = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardID = entity.GetCardId();
		if (playerHeroCardID == "ULDA_Reno")
		{
			string text = cardID;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1370405219U)
			{
				if (num <= 286250261U)
				{
					if (num != 93744337U)
					{
						if (num != 286250261U)
						{
							goto IL_46A;
						}
						if (!(text == "AT_033"))
						{
							goto IL_46A;
						}
						yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_TRIGGER_Burgle_01, 2.5f);
						goto IL_46A;
					}
					else if (!(text == "ULDA_021"))
					{
						goto IL_46A;
					}
				}
				else if (num != 1353627600U)
				{
					if (num != 1370405219U)
					{
						goto IL_46A;
					}
					if (!(text == "ULDA_207"))
					{
						goto IL_46A;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureGatlingWand_01, 2.5f);
					goto IL_46A;
				}
				else
				{
					if (!(text == "ULDA_206"))
					{
						goto IL_46A;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureLei_01, 2.5f);
					goto IL_46A;
				}
			}
			else if (num <= 1471070933U)
			{
				if (num != 1437515695U)
				{
					if (num != 1471070933U)
					{
						goto IL_46A;
					}
					if (!(text == "ULDA_201"))
					{
						goto IL_46A;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureWhip_01, 2.5f);
					goto IL_46A;
				}
				else
				{
					if (!(text == "ULDA_203"))
					{
						goto IL_46A;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureHat_01, 2.5f);
					goto IL_46A;
				}
			}
			else if (num != 3945181129U)
			{
				if (num != 4129734938U)
				{
					if (num != 4170455491U)
					{
						goto IL_46A;
					}
					if (!(text == "ULDA_016"))
					{
						goto IL_46A;
					}
				}
				else
				{
					if (!(text == "CS2_024"))
					{
						goto IL_46A;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_TRIGGER_Frostbolt_01, 2.5f);
					goto IL_46A;
				}
			}
			else
			{
				if (!(text == "CS2_029"))
				{
					goto IL_46A;
				}
				yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_TRIGGER_Fireball_01, 2.5f);
				goto IL_46A;
			}
			yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_RenoTreasureJrExplorer_01, 2.5f);
		}
		IL_46A:
		if (playerHeroCardID == "ULDA_Finley")
		{
			string text = cardID;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1066276279U)
			{
				if (num <= 333288849U)
				{
					if (num != 76966718U)
					{
						if (num != 333288849U)
						{
							goto IL_7D4;
						}
						if (!(text == "ULD_716"))
						{
							goto IL_7D4;
						}
						yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_Trigger_Tip_the_Scales_01, 2.5f);
						goto IL_7D4;
					}
					else if (!(text == "ULDA_020"))
					{
						goto IL_7D4;
					}
				}
				else if (num != 982388184U)
				{
					if (num != 1049498660U)
					{
						if (num != 1066276279U)
						{
							goto IL_7D4;
						}
						if (!(text == "ULDA_504"))
						{
							goto IL_7D4;
						}
						yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasurePandarenTeaSet_01, 2.5f);
						goto IL_7D4;
					}
					else
					{
						if (!(text == "ULDA_505"))
						{
							goto IL_7D4;
						}
						yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasureLance_01, 2.5f);
						goto IL_7D4;
					}
				}
				else
				{
					if (!(text == "ULDA_501"))
					{
						goto IL_7D4;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasureScarabMount_01, 2.5f);
					goto IL_7D4;
				}
			}
			else if (num <= 2031105540U)
			{
				if (num != 1083053898U)
				{
					if (num != 2031105540U)
					{
						goto IL_7D4;
					}
					if (!(text == "CS2_093"))
					{
						goto IL_7D4;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_Trigger_Consecration_01, 2.5f);
					goto IL_7D4;
				}
				else
				{
					if (!(text == "ULDA_507"))
					{
						goto IL_7D4;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasureKarl_01, 2.5f);
					goto IL_7D4;
				}
			}
			else if (num != 4095193962U)
			{
				if (num != 4187233110U)
				{
					if (num != 4271121205U)
					{
						goto IL_7D4;
					}
					if (!(text == "ULDA_010"))
					{
						goto IL_7D4;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasureMurkyHorn_01, 2.5f);
					goto IL_7D4;
				}
				else if (!(text == "ULDA_015"))
				{
					goto IL_7D4;
				}
			}
			else
			{
				if (!(text == "CS2_046"))
				{
					goto IL_7D4;
				}
				yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_Trigger_Bloodlust_01, 2.5f);
				goto IL_7D4;
			}
			yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_FinleyTreasureJrExplorer_01, 2.5f);
		}
		IL_7D4:
		if (playerHeroCardID == "ULDA_Elise")
		{
			string text = cardID;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2299908174U)
			{
				if (num <= 717101676U)
				{
					if (num != 43411480U)
					{
						if (num != 717101676U)
						{
							goto IL_ADB;
						}
						if (!(text == "ICC_054"))
						{
							goto IL_ADB;
						}
						yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TRIGGER_SpreadingPlague_01, 2.5f);
						goto IL_ADB;
					}
					else if (!(text == "ULDA_022"))
					{
						goto IL_ADB;
					}
				}
				else if (num != 1754458488U)
				{
					if (num != 2299908174U)
					{
						goto IL_ADB;
					}
					if (!(text == "EX1_169"))
					{
						goto IL_ADB;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TRIGGER_Innervate_01, 2.5f);
					goto IL_ADB;
				}
				else
				{
					if (!(text == "EX1_332"))
					{
						goto IL_ADB;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TRIGGER_Silence_01, 2.5f);
					goto IL_ADB;
				}
			}
			else if (num <= 3511034145U)
			{
				if (num != 3494256526U)
				{
					if (num != 3511034145U)
					{
						goto IL_ADB;
					}
					if (!(text == "ULDA_304"))
					{
						goto IL_ADB;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseTreasureDruidStaff_01, 2.5f);
					goto IL_ADB;
				}
				else
				{
					if (!(text == "ULDA_305"))
					{
						goto IL_ADB;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseTreasureMachete_01, 2.5f);
					goto IL_ADB;
				}
			}
			else if (num != 3544589383U)
			{
				if (num != 4153677872U)
				{
					if (num != 4220641253U)
					{
						goto IL_ADB;
					}
					if (!(text == "ULDA_003"))
					{
						goto IL_ADB;
					}
					yield return this.PlayAndRemoveRandomLineOnlyOnce(playerActor, this.m_EliseTreasureAddarah);
					goto IL_ADB;
				}
				else if (!(text == "ULDA_017"))
				{
					goto IL_ADB;
				}
			}
			else
			{
				if (!(text == "ULDA_302"))
				{
					goto IL_ADB;
				}
				yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseTreasureScarabStatue_01, 2.5f);
				goto IL_ADB;
			}
			yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseJrExplorer_01, 2.5f);
		}
		IL_ADB:
		if (playerHeroCardID == "ULDA_Brann")
		{
			string text = cardID;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2563252929U)
			{
				if (num <= 1372656211U)
				{
					if (num != 60189099U)
					{
						if (num != 110374861U)
						{
							if (num != 1372656211U)
							{
								goto IL_ED5;
							}
							if (!(text == "EX1_539"))
							{
								goto IL_ED5;
							}
							yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_TRIGGER_KillCommand_01, 2.5f);
							goto IL_ED5;
						}
						else if (!(text == "ULDA_018"))
						{
							goto IL_ED5;
						}
					}
					else if (!(text == "ULDA_023"))
					{
						goto IL_ED5;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_BrannTreasureJrExplorer_01, 2.5f);
					goto IL_ED5;
				}
				if (num <= 1722638263U)
				{
					if (num != 1625306234U)
					{
						if (num != 1722638263U)
						{
							goto IL_ED5;
						}
						if (!(text == "ICC_200"))
						{
							goto IL_ED5;
						}
					}
					else if (!(text == "EX1_554"))
					{
						goto IL_ED5;
					}
				}
				else if (num != 1816221896U)
				{
					if (num != 2563252929U)
					{
						goto IL_ED5;
					}
					if (!(text == "GIL_577"))
					{
						goto IL_ED5;
					}
				}
				else
				{
					if (!(text == "EX1_400"))
					{
						goto IL_ED5;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_TRIGGER_Whirlwind_01, 2.5f);
					goto IL_ED5;
				}
			}
			else if (num <= 3203837030U)
			{
				if (num <= 2713629737U)
				{
					if (num != 2617794849U)
					{
						if (num != 2713629737U)
						{
							goto IL_ED5;
						}
						if (!(text == "LOE_021"))
						{
							goto IL_ED5;
						}
					}
					else if (!(text == "AT_060"))
					{
						goto IL_ED5;
					}
				}
				else if (num != 3187059411U)
				{
					if (num != 3203837030U)
					{
						goto IL_ED5;
					}
					if (!(text == "ULDA_402"))
					{
						goto IL_ED5;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_BrannTreasureSaddle_01, 2.5f);
					goto IL_ED5;
				}
				else
				{
					if (!(text == "ULDA_401"))
					{
						goto IL_ED5;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_BrannTreasureBlunderbuss_01, 2.5f);
					goto IL_ED5;
				}
			}
			else if (num <= 3463643828U)
			{
				if (num != 3254169887U)
				{
					if (num != 3463643828U)
					{
						goto IL_ED5;
					}
					if (!(text == "EX1_611"))
					{
						goto IL_ED5;
					}
				}
				else
				{
					if (!(text == "ULDA_405"))
					{
						goto IL_ED5;
					}
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_BrannTreasureGiantEgg_01, 2.5f);
					goto IL_ED5;
				}
			}
			else if (num != 3480421447U)
			{
				if (num != 4203863634U)
				{
					goto IL_ED5;
				}
				if (!(text == "ULDA_002"))
				{
					goto IL_ED5;
				}
				yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_BrannTreasureFlo_01, 2.5f);
				goto IL_ED5;
			}
			else if (!(text == "EX1_610"))
			{
				goto IL_ED5;
			}
			yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_TRIGGER_SecretTrap_01, 2.5f);
		}
		IL_ED5:
		yield break;
	}

	// Token: 0x06003E33 RID: 15923 RVA: 0x00149A88 File Offset: 0x00147C88
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

	// Token: 0x06003E34 RID: 15924 RVA: 0x00149B28 File Offset: 0x00147D28
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

	// Token: 0x06003E35 RID: 15925 RVA: 0x00149BB4 File Offset: 0x00147DB4
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

	// Token: 0x06003E36 RID: 15926 RVA: 0x00149BEA File Offset: 0x00147DEA
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
	}

	// Token: 0x06003E37 RID: 15927 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06003E38 RID: 15928 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003E39 RID: 15929 RVA: 0x00149C29 File Offset: 0x00147E29
	protected float ChanceToPlayMurlocPlotTwistVOLine()
	{
		return 0.1f;
	}

	// Token: 0x06003E3A RID: 15930 RVA: 0x00149C29 File Offset: 0x00147E29
	protected float ChanceToPlayMadnessPlotTwistVOLine()
	{
		return 0.1f;
	}

	// Token: 0x06003E3B RID: 15931 RVA: 0x00112BA9 File Offset: 0x00110DA9
	protected float ChanceToPlayDeathPlotTwistVOLine()
	{
		return 0.5f;
	}

	// Token: 0x06003E3C RID: 15932 RVA: 0x00149C30 File Offset: 0x00147E30
	protected float ChanceToPlayWrathPlotTwistVOLine()
	{
		return 0.05f;
	}

	// Token: 0x06003E3D RID: 15933 RVA: 0x00149C37 File Offset: 0x00147E37
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E3E RID: 15934 RVA: 0x00149C54 File Offset: 0x00147E54
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E3F RID: 15935 RVA: 0x00149C71 File Offset: 0x00147E71
	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E40 RID: 15936 RVA: 0x00149C8E File Offset: 0x00147E8E
	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = this.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003E41 RID: 15937 RVA: 0x00149CAB File Offset: 0x00147EAB
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		float num = UnityEngine.Random.Range(0f, 1f);
		bool flag = false;
		int num2 = base.GetTag(GAME_TAG.TURN) - GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
		if (cardId == "ULDA_BOSS_37h" || cardId == "ULDA_BOSS_37h2" || cardId == "ULDA_BOSS_37h3" || cardId == "ULDA_BOSS_38h" || cardId == "ULDA_BOSS_38h2" || cardId == "ULDA_BOSS_38h3" || cardId == "ULDA_BOSS_39h" || cardId == "ULDA_BOSS_39h2" || cardId == "ULDA_BOSS_39h3" || cardId == "ULDA_BOSS_40h" || cardId == "ULDA_BOSS_40h2" || cardId == "ULDA_BOSS_40h3")
		{
			flag = true;
		}
		if (missionEvent <= 804)
		{
			switch (missionEvent)
			{
			case 505:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01, 2.5f);
				goto IL_BD6;
			case 506:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01, 2.5f);
				goto IL_BD6;
			case 507:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01, 2.5f);
				goto IL_BD6;
			case 508:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01, 2.5f);
				goto IL_BD6;
			case 509:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01, 2.5f);
				goto IL_BD6;
			case 510:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01, 2.5f);
				goto IL_BD6;
			case 511:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01, 2.5f);
				goto IL_BD6;
			case 512:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01, 2.5f);
				goto IL_BD6;
			case 513:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01, 2.5f);
				goto IL_BD6;
			case 514:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01, 2.5f);
				goto IL_BD6;
			case 515:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01, 2.5f);
				goto IL_BD6;
			case 516:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01, 2.5f);
				goto IL_BD6;
			case 517:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01, 2.5f);
				goto IL_BD6;
			case 518:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01, 2.5f);
				goto IL_BD6;
			case 519:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01, 2.5f);
				goto IL_BD6;
			case 520:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01, 2.5f);
				goto IL_BD6;
			case 521:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01, 2.5f);
				goto IL_BD6;
			case 522:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01, 2.5f);
				goto IL_BD6;
			case 523:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01, 2.5f);
				goto IL_BD6;
			case 524:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01, 2.5f);
				goto IL_BD6;
			case 525:
				yield return base.PlayBossLine(ULDA_Dungeon.Wisdomball_Pop_up_BrassRing_Quote, ULDA_Dungeon.VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01, 2.5f);
				goto IL_BD6;
			default:
				switch (missionEvent)
				{
				case 801:
					if (this.ChanceToPlayDeathPlotTwistVOLine() < num || this.TurnOfPlotTwistLastPlayed >= num2)
					{
						goto IL_BD6;
					}
					this.TurnOfPlotTwistLastPlayed = num2;
					if (flag)
					{
						yield return this.PlayRandomLineAlways(actor2, this.m_BossDeathPlotTwistTauntLines);
						goto IL_BD6;
					}
					this.TurnOfPlotTwistLastPlayed = num2;
					yield return this.PlayRandomLineAlways(ULDA_Dungeon.Tombs_of_Terror_Xatma_BrassRing_Quote, this.m_BossDeathPlotTwistTauntLines);
					goto IL_BD6;
				case 802:
					if (this.ChanceToPlayMadnessPlotTwistVOLine() < num || this.TurnOfPlotTwistLastPlayed >= num2)
					{
						goto IL_BD6;
					}
					this.TurnOfPlotTwistLastPlayed = num2;
					if (flag)
					{
						yield return this.PlayRandomLineAlways(actor2, this.m_BossMadnessPlotTwistTauntLines);
						goto IL_BD6;
					}
					yield return this.PlayRandomLineAlways(ULDA_Dungeon.Tombs_of_Terror_Kzrath_BrassRing_Quote, this.m_BossMadnessPlotTwistTauntLines);
					goto IL_BD6;
				case 803:
					if (this.ChanceToPlayMurlocPlotTwistVOLine() < num || this.TurnOfPlotTwistLastPlayed >= num2)
					{
						goto IL_BD6;
					}
					this.TurnOfPlotTwistLastPlayed = num2;
					if (flag)
					{
						yield return this.PlayRandomLineAlways(actor2, this.m_BossMurlocPlotTwistTauntLines);
						goto IL_BD6;
					}
					yield return this.PlayRandomLineAlways(ULDA_Dungeon.Tombs_of_Terror_Vesh_BrassRing_Quote, this.m_BossMurlocPlotTwistTauntLines);
					goto IL_BD6;
				case 804:
					if (this.ChanceToPlayWrathPlotTwistVOLine() < num || this.TurnOfPlotTwistLastPlayed >= num2)
					{
						goto IL_BD6;
					}
					this.TurnOfPlotTwistLastPlayed = num2;
					if (flag)
					{
						yield return this.PlayRandomLineAlways(actor2, this.m_BossWrathPlotTwistTauntLines);
						goto IL_BD6;
					}
					yield return this.PlayRandomLineAlways(ULDA_Dungeon.Tombs_of_Terror_Icarax_BrassRing_Quote, this.m_BossWrathPlotTwistTauntLines);
					goto IL_BD6;
				}
				break;
			}
		}
		else
		{
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
				goto IL_BD6;
			case 1001:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
				yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
				goto IL_BD6;
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
				goto IL_BD6;
			case 1003:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
				yield return base.PlayBossLine(actor2, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
				goto IL_BD6;
			default:
				if (missionEvent == 58023)
				{
					Network.Get().DisconnectFromGameServer();
					SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
					GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
					SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
					goto IL_BD6;
				}
				break;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_BD6:
		yield break;
	}

	// Token: 0x06003E42 RID: 15938 RVA: 0x00149CC4 File Offset: 0x00147EC4
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		int missionId = GameMgr.Get().GetMissionId();
		if (playerSide == Player.Side.OPPOSING)
		{
			if (missionId == 3428)
			{
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_01");
			}
			if (missionId == 3429)
			{
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_02");
			}
			if (missionId == 3430)
			{
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_03");
			}
			if (missionId == 3431)
			{
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_04");
			}
			if (missionId == 3432)
			{
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_05");
			}
			if (missionId == 3433)
			{
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_01_HEROIC");
			}
			if (missionId == 3434)
			{
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_02_HEROIC");
			}
			if (missionId == 3435)
			{
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_03_HEROIC");
			}
			if (missionId == 3436)
			{
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_04_HEROIC");
			}
			if (missionId == 3437)
			{
				return GameStrings.Get("ULDA_MISSION_CHAPTER_SUBTEXT_05_HEROIC");
			}
		}
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x06003E43 RID: 15939 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06003E44 RID: 15940 RVA: 0x00149DA8 File Offset: 0x00147FA8
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

	// Token: 0x06003E45 RID: 15941 RVA: 0x00149EB2 File Offset: 0x001480B2
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		ScenarioDbId missionId = (ScenarioDbId)GameMgr.Get().GetMissionId();
		string playerHeroCardID = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		bool isFirstBoss = ULDA_MissionEntity.GetIsFirstBoss();
		if (turn == 1 && isFirstBoss && GameState.Get() != null && GameState.Get().GetFriendlySidePlayer() != null && GameState.Get().GetFriendlySidePlayer().GetHero() != null)
		{
			switch (missionId)
			{
			case ScenarioDbId.ULDA_CITY:
			case ScenarioDbId.ULDA_CITY_HEROIC:
				if (!(playerHeroCardID == "ULDA_Reno"))
				{
					if (playerHeroCardID == "ULDA_Finley")
					{
						yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter01a_01, 2.5f);
						yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Brann_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter01b_01, 2.5f);
					}
					else if (playerHeroCardID == "ULDA_Brann")
					{
						yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter01a_01, 2.5f);
						yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter01b_01, 2.5f);
					}
					else if (playerHeroCardID == "ULDA_Elise")
					{
						yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter01a_01, 2.5f);
						yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Finley_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Elise_Chapter01b_01, 2.5f);
					}
				}
				break;
			case ScenarioDbId.ULDA_DESERT:
			case ScenarioDbId.ULDA_DESERT_HEROIC:
				if (playerHeroCardID == "ULDA_Reno")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter02a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter02b_01, 2.5f);
				}
				else if (playerHeroCardID == "ULDA_Finley")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter02a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Reno_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Finley_Chapter02b_01, 2.5f);
				}
				else if (playerHeroCardID == "ULDA_Brann")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter02a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter02b_01, 2.5f);
				}
				else if (playerHeroCardID == "ULDA_Elise")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter02a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Brann_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Elise_Chapter02b_01, 2.5f);
				}
				break;
			case ScenarioDbId.ULDA_TOMB:
			case ScenarioDbId.ULDA_TOMB_HEROIC:
				if (playerHeroCardID == "ULDA_Reno")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter03a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Finley_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Reno_Chapter03b_01, 2.5f);
				}
				else if (playerHeroCardID == "ULDA_Finley")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter03a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Brann_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter03b_01, 2.5f);
				}
				else if (playerHeroCardID == "ULDA_Brann")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter03a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter03b_01, 2.5f);
				}
				else if (playerHeroCardID == "ULDA_Elise")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter03a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Reno_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter03b_01, 2.5f);
				}
				break;
			case ScenarioDbId.ULDA_HALLS:
			case ScenarioDbId.ULDA_HALLS_HEROIC:
				if (playerHeroCardID == "ULDA_Reno")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter04a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter04b_01, 2.5f);
				}
				else if (playerHeroCardID == "ULDA_Finley")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter04a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Brann_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter04b_01, 2.5f);
				}
				else if (playerHeroCardID == "ULDA_Brann")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter04a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter04b_01, 2.5f);
				}
				else if (playerHeroCardID == "ULDA_Elise")
				{
					yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter04a_01, 2.5f);
					yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Reno_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter04b_01, 2.5f);
				}
				break;
			}
		}
		bool flag = GameUtils.GetDefeatedBossCount() == base.GetDefeatedBossCountForFinalBoss();
		int currentHealth = GameState.Get().GetOpposingSidePlayer().GetHero().GetCurrentHealth();
		if (flag && base.GetDefeatedBossCountForFinalBoss() != 0 && currentHealth == 300)
		{
			if (playerHeroCardID == "ULDA_Reno")
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Reno_Male_Human_TUT_PlagueLord_Turn1_Reno_01, 2.5f);
				yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
			else if (playerHeroCardID == "ULDA_Finley")
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Finley_Male_Murloc_TUT_PlagueLord_Turn1_Finley_01, 2.5f);
				yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
			else if (playerHeroCardID == "ULDA_Brann")
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Brann_Male_Dwarf_TUT_PlagueLord_Turn1_Brann_01, 2.5f);
				yield return base.PlayLineOnlyOnce(ULDA_Dungeon.Tombs_of_Terror_Elise_BrassRing_Quote, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
			else if (playerHeroCardID == "ULDA_Elise")
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_Elise_01, 2.5f);
				yield return base.PlayLineOnlyOnce(playerActor, ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswerSelf_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		yield break;
	}

	// Token: 0x04002A0A RID: 10762
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x04002A0B RID: 10763
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x04002A0C RID: 10764
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_TRIGGER_KillCommand_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_TRIGGER_KillCommand_01.prefab:dfe38ab5192b0bc42ba82da57ddef202");

	// Token: 0x04002A0D RID: 10765
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_TRIGGER_SecretTrap_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_TRIGGER_SecretTrap_01.prefab:e8ef318fa0d43194f9dee431fee04f0f");

	// Token: 0x04002A0E RID: 10766
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_TRIGGER_Whirlwind_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_TRIGGER_Whirlwind_01.prefab:b2976616ff1f27c44bf888deaca36dae");

	// Token: 0x04002A0F RID: 10767
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TRIGGER_Innervate_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TRIGGER_Innervate_01.prefab:fb7a1039d7060364a8011adfa66263b5");

	// Token: 0x04002A10 RID: 10768
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TRIGGER_Silence_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TRIGGER_Silence_01.prefab:197e2dd7a0921a44e892821dbb5879ec");

	// Token: 0x04002A11 RID: 10769
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TRIGGER_SpreadingPlague_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TRIGGER_SpreadingPlague_01.prefab:5bdb3d0e3ade5de46ac595af579aaea5");

	// Token: 0x04002A12 RID: 10770
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Trigger_Bloodlust_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Trigger_Bloodlust_01.prefab:5a56fe6de89b97e4aa89fee00c44ba77");

	// Token: 0x04002A13 RID: 10771
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Trigger_Consecration_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Trigger_Consecration_01.prefab:0efe5eb06e9e9564c9db10797e5b62ce");

	// Token: 0x04002A14 RID: 10772
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_Trigger_Tip_the_Scales_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_Trigger_Tip_the_Scales_01.prefab:de7dbdee4df8d4a40ac6ff1b660e2647");

	// Token: 0x04002A15 RID: 10773
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_TRIGGER_Burgle_01 = new AssetReference("VO_ULDA_Reno_Male_Human_TRIGGER_Burgle_01.prefab:f40952ae237059f42bd284e2b3971148");

	// Token: 0x04002A16 RID: 10774
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_TRIGGER_Fireball_01 = new AssetReference("VO_ULDA_Reno_Male_Human_TRIGGER_Fireball_01.prefab:20e37fa1f3277e549844bf23d30ad9ab");

	// Token: 0x04002A17 RID: 10775
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_TRIGGER_Frostbolt_01 = new AssetReference("VO_ULDA_Reno_Male_Human_TRIGGER_Frostbolt_01.prefab:3ea42375f2b0a864d841c5dc133f408f");

	// Token: 0x04002A18 RID: 10776
	private static readonly AssetReference Tombs_of_Terror_Brann_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Brann_BrassRing_Quote.prefab:d521a1fe41518e24da6e4252b97fbeb7");

	// Token: 0x04002A19 RID: 10777
	private static readonly AssetReference Tombs_of_Terror_Elise_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Elise_BrassRing_Quote.prefab:6e9e8faaae29f834183122d7ea9be68d");

	// Token: 0x04002A1A RID: 10778
	private static readonly AssetReference Tombs_of_Terror_Finley_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Finley_BrassRing_Quote.prefab:547ebc970764ec64da6eb3de26ed4698");

	// Token: 0x04002A1B RID: 10779
	private static readonly AssetReference Tombs_of_Terror_Reno_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Reno_BrassRing_Quote.prefab:4c0b79d4f597c464baabf02e06cf8ae7");

	// Token: 0x04002A1C RID: 10780
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_TUT_PlagueLord_Turn1_Brann_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_TUT_PlagueLord_Turn1_Brann_01.prefab:be40b9d9a54167e4a8b14692082d149b");

	// Token: 0x04002A1D RID: 10781
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_Elise_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_Elise_01.prefab:8df3a42627590d74fa640975d7b7c9f6");

	// Token: 0x04002A1E RID: 10782
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswer_01.prefab:e6e2aa39fa139a841bb10f25d83ece17");

	// Token: 0x04002A1F RID: 10783
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswerSelf_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_TUT_PlagueLord_Turn1_EliseAnswerSelf_01.prefab:409907c1b05359d4599957509fac0fe4");

	// Token: 0x04002A20 RID: 10784
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_TUT_PlagueLord_Turn1_Finley_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_TUT_PlagueLord_Turn1_Finley_01.prefab:46fe80b948666044a9845cba1fca30da");

	// Token: 0x04002A21 RID: 10785
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_TUT_PlagueLord_Turn1_Reno_01 = new AssetReference("VO_ULDA_Reno_Male_Human_TUT_PlagueLord_Turn1_Reno_01.prefab:30dfc1d3b64b77b43837aa231d97dd94");

	// Token: 0x04002A22 RID: 10786
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter01a_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter01a_01.prefab:fa9132b4bd9a3a7419f2380e3d5cef7b");

	// Token: 0x04002A23 RID: 10787
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter02a_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter02a_01.prefab:76c6b859ccc70c04e91ea67e5a30cf77");

	// Token: 0x04002A24 RID: 10788
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter03a_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter03a_01.prefab:95f7ea61b24ecba4aa95ea2096aa80c9");

	// Token: 0x04002A25 RID: 10789
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter04a_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Brann_Chapter04a_01.prefab:4f37870210d71294492b4879e9931ace");

	// Token: 0x04002A26 RID: 10790
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Elise_Chapter02b_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Elise_Chapter02b_01.prefab:44d19f9cad1c80649aa357af93284521");

	// Token: 0x04002A27 RID: 10791
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter01b_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter01b_01.prefab:0ea5b0bb11d226442a2b7a0b64e4f1e1");

	// Token: 0x04002A28 RID: 10792
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter03b_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter03b_01.prefab:24a814f2c65fa4d45b74c9997c0e8e98");

	// Token: 0x04002A29 RID: 10793
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter04b_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Finley_Chapter04b_01.prefab:4aedaf500c08a914db2f7718bb74935f");

	// Token: 0x04002A2A RID: 10794
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Reno_Chapter01b_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_STORY_Banter_Reno_Chapter01b_01.prefab:5b76058cf8d0a8846bf5ad6e979c26ad");

	// Token: 0x04002A2B RID: 10795
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter01b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter01b_01.prefab:cd3dc1d586e5a1649ac352ff90c2a51d");

	// Token: 0x04002A2C RID: 10796
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter02b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter02b_01.prefab:43e59afee8041c040b6d35da2195a45f");

	// Token: 0x04002A2D RID: 10797
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter03b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter03b_01.prefab:e19bdd01cd1563d4baee974c91ae46d4");

	// Token: 0x04002A2E RID: 10798
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter04b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Brann_Chapter04b_01.prefab:13bb809e3da1eab44992a3b2643516d0");

	// Token: 0x04002A2F RID: 10799
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter01a_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter01a_01.prefab:f257662ac9702374480737b02be9f57c");

	// Token: 0x04002A30 RID: 10800
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter02a_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter02a_01.prefab:aee22a0a56434914c8884fb40af8958d");

	// Token: 0x04002A31 RID: 10801
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter03a_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter03a_01.prefab:ba0b33d05ad749f48ad9434f9be304a4");

	// Token: 0x04002A32 RID: 10802
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter04a_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Elise_Chapter04a_01.prefab:b970b0a4ba3de294bb224fa1ac9f7260");

	// Token: 0x04002A33 RID: 10803
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter02b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter02b_01.prefab:ecfbedb24716fb64da568a9b1eb47a61");

	// Token: 0x04002A34 RID: 10804
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter04b_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_STORY_Banter_Reno_Chapter04b_01.prefab:1899f68b7290e844998146fd3137af4e");

	// Token: 0x04002A35 RID: 10805
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Elise_Chapter01b_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Elise_Chapter01b_01.prefab:6d2339aa3bea44146a07baa125a5e52b");

	// Token: 0x04002A36 RID: 10806
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter01a_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter01a_01.prefab:8354ef70b8c286c4eb698359a1101023");

	// Token: 0x04002A37 RID: 10807
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter02a_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter02a_01.prefab:0f77c06418fd9d14d818d174c73cd22c");

	// Token: 0x04002A38 RID: 10808
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter03a_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter03a_01.prefab:bed3404ac2b9a9649b3afd825d61e889");

	// Token: 0x04002A39 RID: 10809
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter04a_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Finley_Chapter04a_01.prefab:189f9c91163d3ad48ab209d4233bfcea");

	// Token: 0x04002A3A RID: 10810
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_STORY_Banter_Reno_Chapter03b_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_STORY_Banter_Reno_Chapter03b_01.prefab:a200d0b5bedab944b89fc3a15da18ea8");

	// Token: 0x04002A3B RID: 10811
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter03b_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter03b_01.prefab:2845b25610ea74e429bbdd63cf218502");

	// Token: 0x04002A3C RID: 10812
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter04b_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Elise_Chapter04b_01.prefab:1443f0fdff63c4b469674b8f386cc26d");

	// Token: 0x04002A3D RID: 10813
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Finley_Chapter02b_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Finley_Chapter02b_01.prefab:2259676fd99450f40840896733c18f58");

	// Token: 0x04002A3E RID: 10814
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter01a_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter01a_01.prefab:44658c8766afae948b1359b8e575c77c");

	// Token: 0x04002A3F RID: 10815
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter02a_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter02a_01.prefab:7241a38908c67be4cbb40f4556b6930b");

	// Token: 0x04002A40 RID: 10816
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter03a_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter03a_01.prefab:0b9bc24d9adb2ba4a9a07b5bccbf0e30");

	// Token: 0x04002A41 RID: 10817
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter04a_01 = new AssetReference("VO_ULDA_Reno_Male_Human_STORY_Banter_Reno_Chapter04a_01.prefab:9e8eba9e6e1f0f84988ea7bf0512b63f");

	// Token: 0x04002A42 RID: 10818
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_01 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_01.prefab:477bfc3803adf1f4a8ce078927c3b47f");

	// Token: 0x04002A43 RID: 10819
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_02 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_02.prefab:2105f5df08fc1254ba2fa09d2401557d");

	// Token: 0x04002A44 RID: 10820
	private static readonly AssetReference VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_03 = new AssetReference("VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_03.prefab:8fc03b74cd5e0104f821b177db07b5f2");

	// Token: 0x04002A45 RID: 10821
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_01 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_01.prefab:491255b818cd83f4f997063f51c2eeac");

	// Token: 0x04002A46 RID: 10822
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_02 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_02.prefab:d7d44a8920958874993b0e56487e9aaa");

	// Token: 0x04002A47 RID: 10823
	private static readonly AssetReference VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_03 = new AssetReference("VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_03.prefab:042f9732b05fa424da7f9d49c44815ae");

	// Token: 0x04002A48 RID: 10824
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_01 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_01.prefab:e765c5abe81d1344a99f7111f3d41bd5");

	// Token: 0x04002A49 RID: 10825
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_02 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_02.prefab:831b7acaf1433954084f3d632acbcbdd");

	// Token: 0x04002A4A RID: 10826
	private static readonly AssetReference VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_03 = new AssetReference("VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_03.prefab:566a70824da8fd546aa3ad0e9d1debae");

	// Token: 0x04002A4B RID: 10827
	private static readonly AssetReference Tombs_of_Terror_Vesh_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Vesh_BrassRing_Quote.prefab:99b4c68f3a6639743a2cbee4fb8d9e5b");

	// Token: 0x04002A4C RID: 10828
	private static readonly AssetReference Tombs_of_Terror_Icarax_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Icarax_BrassRing_Quote.prefab:f419bcdf120279a489a80794299832fb");

	// Token: 0x04002A4D RID: 10829
	private static readonly AssetReference Tombs_of_Terror_Kzrath_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Kzrath_BrassRing_Quote.prefab:66a43505bb8f47842bca007b4af3f7c7");

	// Token: 0x04002A4E RID: 10830
	private static readonly AssetReference Tombs_of_Terror_Xatma_BrassRing_Quote = new AssetReference("Tombs_of_Terror_Xatma_BrassRing_Quote.prefab:641db52561e62fd49ab686d185e612c0");

	// Token: 0x04002A4F RID: 10831
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_01 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_01.prefab:eb8b0170132143a4c9a78dc28c44a393");

	// Token: 0x04002A50 RID: 10832
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_02 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_02.prefab:2aa8c6fddbe8e214db3913fb31574c2d");

	// Token: 0x04002A51 RID: 10833
	private static readonly AssetReference VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_03 = new AssetReference("VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_03.prefab:6eee41e7b3ff0e740b90304c2567b8d1");

	// Token: 0x04002A52 RID: 10834
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureGatlingWand_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureGatlingWand_01.prefab:fa0f62a6dfa1fa945983bc327970c8d3");

	// Token: 0x04002A53 RID: 10835
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureHat_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureHat_01.prefab:9aee9ea3bbc6ddb40bd09505440db1cb");

	// Token: 0x04002A54 RID: 10836
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureJrExplorer_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureJrExplorer_01.prefab:8a0f42442e839c3469acd165e33435a2");

	// Token: 0x04002A55 RID: 10837
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureLei_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureLei_01.prefab:6193d524c96eecf4384e47820a578c49");

	// Token: 0x04002A56 RID: 10838
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureTitanRing_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureTitanRing_01.prefab:61f1fc26e68d7714c862fd481901f06d");

	// Token: 0x04002A57 RID: 10839
	private static readonly AssetReference VO_ULDA_Reno_Male_Human_RenoTreasureWhip_01 = new AssetReference("VO_ULDA_Reno_Male_Human_RenoTreasureWhip_01.prefab:1ca1a1e5a82c9404aa463eec9ef4337d");

	// Token: 0x04002A58 RID: 10840
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseJrExplorer_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseJrExplorer_01.prefab:65d7b0a4b96431344858388063aab52f");

	// Token: 0x04002A59 RID: 10841
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_01.prefab:da4f1971d0d7b654482b8140b3b209c8");

	// Token: 0x04002A5A RID: 10842
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_02 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_02.prefab:bbffe4b428c1f404599f7f5ce2b9e0af");

	// Token: 0x04002A5B RID: 10843
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseTreasureDruidStaff_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseTreasureDruidStaff_01.prefab:648cc33404dee9b4cbce8260921d59af");

	// Token: 0x04002A5C RID: 10844
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseTreasureMachete_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseTreasureMachete_01.prefab:092a88caacb83a343a802433e5b21ce5");

	// Token: 0x04002A5D RID: 10845
	private static readonly AssetReference VO_ULDA_Elise_Female_NightElf_EliseTreasureScarabStatue_01 = new AssetReference("VO_ULDA_Elise_Female_NightElf_EliseTreasureScarabStatue_01.prefab:72e42a66ca0282f4a876cd5f935d481f");

	// Token: 0x04002A5E RID: 10846
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannTreasureBlunderbuss_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannTreasureBlunderbuss_01.prefab:71441326544005944b593f2eaf77f740");

	// Token: 0x04002A5F RID: 10847
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannTreasureFlo_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannTreasureFlo_01.prefab:f3946ff37880d694fb8a502607b15233");

	// Token: 0x04002A60 RID: 10848
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannTreasureGiantEgg_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannTreasureGiantEgg_01.prefab:e2653cce6e405164f86786cb239245c0");

	// Token: 0x04002A61 RID: 10849
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannTreasureJrExplorer_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannTreasureJrExplorer_01.prefab:a509641bdb84f6d46bd71d1fa0de452d");

	// Token: 0x04002A62 RID: 10850
	private static readonly AssetReference VO_ULDA_Brann_Male_Dwarf_BrannTreasureSaddle_01 = new AssetReference("VO_ULDA_Brann_Male_Dwarf_BrannTreasureSaddle_01.prefab:d11077cbb62b2494ba5ac379acd30669");

	// Token: 0x04002A63 RID: 10851
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasureJrExplorer_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasureJrExplorer_01.prefab:39ad7ec65c173aa42a738a57a1c7ce17");

	// Token: 0x04002A64 RID: 10852
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasureKarl_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasureKarl_01.prefab:6b1ff31d23eda8c458ca8e8554e1f6ba");

	// Token: 0x04002A65 RID: 10853
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasureLance_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasureLance_01.prefab:9765963f754239f49baba405b0170338");

	// Token: 0x04002A66 RID: 10854
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasureMurkyHorn_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasureMurkyHorn_01.prefab:04416db16fc68524d9c14a3b31973470");

	// Token: 0x04002A67 RID: 10855
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasurePandarenTeaSet_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasurePandarenTeaSet_01.prefab:df3c626ff5d3fb8439c3776c08f834d1");

	// Token: 0x04002A68 RID: 10856
	private static readonly AssetReference VO_ULDA_Finley_Male_Murloc_FinleyTreasureScarabMount_01 = new AssetReference("VO_ULDA_Finley_Male_Murloc_FinleyTreasureScarabMount_01.prefab:acdf120f1aa827444900bfa24ebf9b86");

	// Token: 0x04002A69 RID: 10857
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Copycard_01.prefab:bbb71f2dfac1c474da3209a15215f4ea");

	// Token: 0x04002A6A RID: 10858
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CopyCards_01.prefab:ad01bc4d23eab3e4f86c994d722cf247");

	// Token: 0x04002A6B RID: 10859
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_CostRandomizer_01.prefab:688849bb5a4d2fd48a817159d1a224fa");

	// Token: 0x04002A6C RID: 10860
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_DrawExtra_01.prefab:001c634fac4ab874eb6e16d050554b1f");

	// Token: 0x04002A6D RID: 10861
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Emptyhand_01.prefab:a5d093ea8ec90d64fb53834b44395720");

	// Token: 0x04002A6E RID: 10862
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraMana_01.prefab:97a9c24cb50609347964387b02a62b3c");

	// Token: 0x04002A6F RID: 10863
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_ExtraTurn_01.prefab:feb7534214a13214bac9e2b8726a8cc8");

	// Token: 0x04002A70 RID: 10864
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Fatigue_01.prefab:3fd9e692fe70e924fa0fb5bfabcf17bd");

	// Token: 0x04002A71 RID: 10865
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FatigueReset_01.prefab:701ed0ac22ab9a84daade7ed23403317");

	// Token: 0x04002A72 RID: 10866
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreeMinions_01.prefab:f2eeac3ed0b6f554dbfbc9deea60739f");

	// Token: 0x04002A73 RID: 10867
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_FreezeMinions_01.prefab:f9054e8df8774e44d869a0f96ac07efa");

	// Token: 0x04002A74 RID: 10868
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_HealPlayer_01.prefab:d0a3f9b5c01e04d458178ca8c5069d66");

	// Token: 0x04002A75 RID: 10869
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Minionwipe_01.prefab:ab486ac19b475c74f84999cc9a80b7a6");

	// Token: 0x04002A76 RID: 10870
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_MirrorImage_01.prefab:8789714bb9a92d143bb2024188b8ddd0");

	// Token: 0x04002A77 RID: 10871
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomCast_01.prefab:7fd61e2a38015f240bef703ee9f66e5c");

	// Token: 0x04002A78 RID: 10872
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_RandomLegendary_01.prefab:9273a8457f705514f9755153f0c7abf6");

	// Token: 0x04002A79 RID: 10873
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SaveLife_01.prefab:cad9725371b04e943b07f43ecac56b32");

	// Token: 0x04002A7A RID: 10874
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SelfDamage_01.prefab:ce7a5a15de006d041ad515427fc6f72f");

	// Token: 0x04002A7B RID: 10875
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_SpellCast_01.prefab:92c7854b16b6919499ff3fe7e1e2a422");

	// Token: 0x04002A7C RID: 10876
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Start_01.prefab:3edd1b61bd705b6439dd75542dd6b442");

	// Token: 0x04002A7D RID: 10877
	private static readonly AssetReference VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01 = new AssetReference("VO_DALA_BOSS_60h_Male_Human_FloatingHead_Trigger_Treasure_01.prefab:b9fae030ab3026a4bb17f592028c276d");

	// Token: 0x04002A7E RID: 10878
	private static readonly AssetReference Wisdomball_Pop_up_BrassRing_Quote = new AssetReference("Wisdomball_Pop-up_BrassRing_Quote.prefab:896ee20514caff74db639aa7055838f6");

	// Token: 0x04002A7F RID: 10879
	private List<string> m_BossDeathPlotTwistTauntLines = new List<string>
	{
		ULDA_Dungeon.VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_01,
		ULDA_Dungeon.VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_02,
		ULDA_Dungeon.VO_ULDA_BOSS_37h_Female_PlagueLord_BossPlotTwistTaunt_03
	};

	// Token: 0x04002A80 RID: 10880
	private List<string> m_BossMadnessPlotTwistTauntLines = new List<string>
	{
		ULDA_Dungeon.VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_01,
		ULDA_Dungeon.VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_02,
		ULDA_Dungeon.VO_ULDA_BOSS_38h_Male_PlagueLord_BossPlotTwistTaunt_03
	};

	// Token: 0x04002A81 RID: 10881
	private List<string> m_BossMurlocPlotTwistTauntLines = new List<string>
	{
		ULDA_Dungeon.VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_01,
		ULDA_Dungeon.VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_02,
		ULDA_Dungeon.VO_ULDA_BOSS_39h_Male_PlagueLord_BossPlotTwistTaunt_03
	};

	// Token: 0x04002A82 RID: 10882
	private List<string> m_BossWrathPlotTwistTauntLines = new List<string>
	{
		ULDA_Dungeon.VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_01,
		ULDA_Dungeon.VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_02,
		ULDA_Dungeon.VO_ULDA_BOSS_40h_Female_PlagueLord_BossPlotTwistTaunt_03
	};

	// Token: 0x04002A83 RID: 10883
	private List<string> m_EliseTreasureAddarah = new List<string>
	{
		ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_01,
		ULDA_Dungeon.VO_ULDA_Elise_Female_NightElf_EliseTreasureAddarah_02
	};

	// Token: 0x04002A84 RID: 10884
	public string m_introLine;

	// Token: 0x04002A85 RID: 10885
	public string m_deathLine;

	// Token: 0x04002A86 RID: 10886
	public string m_standardEmoteResponseLine;

	// Token: 0x04002A87 RID: 10887
	public List<string> m_BossIdleLines;

	// Token: 0x04002A88 RID: 10888
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x04002A89 RID: 10889
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x04002A8A RID: 10890
	private int m_PlayBossVOLineIndex;

	// Token: 0x04002A8B RID: 10891
	public int TurnOfPlotTwistLastPlayed;
}
