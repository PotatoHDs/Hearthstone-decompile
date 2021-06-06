using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000472 RID: 1138
public class DALA_Dungeon_Boss_69h : DALA_Dungeon
{
	// Token: 0x06003DA5 RID: 15781 RVA: 0x00143D10 File Offset: 0x00141F10
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_BossAzureDrake_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_BossSilence_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_BossSpellDamage_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Death_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_DefeatPlayer_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_EmoteResponse_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Exposition_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_02,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_03,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_04,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_05,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_06,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Idle_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Idle_02,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Idle_03,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Intro_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerAlextrasza_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_02,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerDeathwing_02,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerDKJaina_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerMageSpell_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerMalygos_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerNozdormu_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerYsera_01,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_TurnOne_02,
			DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_TurnTwo_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003DA6 RID: 15782 RVA: 0x00143F34 File Offset: 0x00142134
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_EmoteResponse_01;
	}

	// Token: 0x06003DA7 RID: 15783 RVA: 0x00143F6C File Offset: 0x0014216C
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_69h.m_IdleLines;
	}

	// Token: 0x06003DA8 RID: 15784 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003DA9 RID: 15785 RVA: 0x00143F73 File Offset: 0x00142173
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_BossSilence_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_BossSpellDamage_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_TurnOne_02, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Exposition_01, 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerMageSpell_01, 2.5f);
			break;
		case 106:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_69h.m_BossHeroPower);
			break;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_TurnTwo_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003DAA RID: 15786 RVA: 0x00143F89 File Offset: 0x00142189
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num > 1457382949U)
		{
			if (num <= 3898551556U)
			{
				if (num != 2560247920U)
				{
					if (num != 3064386877U)
					{
						if (num != 3898551556U)
						{
							goto IL_478;
						}
						if (!(cardId == "NEW1_030"))
						{
							goto IL_478;
						}
					}
					else
					{
						if (!(cardId == "CFM_623"))
						{
							goto IL_478;
						}
						goto IL_450;
					}
				}
				else
				{
					if (!(cardId == "DS1_185"))
					{
						goto IL_478;
					}
					goto IL_450;
				}
			}
			else if (num <= 4045846843U)
			{
				if (num != 4022523648U)
				{
					if (num != 4045846843U)
					{
						goto IL_478;
					}
					if (!(cardId == "CS2_023"))
					{
						goto IL_478;
					}
					goto IL_450;
				}
				else
				{
					if (!(cardId == "EX1_277"))
					{
						goto IL_478;
					}
					goto IL_450;
				}
			}
			else if (num != 4146512557U)
			{
				if (num != 4175990201U)
				{
					goto IL_478;
				}
				if (!(cardId == "OG_317"))
				{
					goto IL_478;
				}
			}
			else
			{
				if (!(cardId == "CS2_025"))
				{
					goto IL_478;
				}
				goto IL_450;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerDeathwing_02, 2.5f);
			goto IL_478;
		}
		if (num <= 1390125378U)
		{
			if (num != 471245355U)
			{
				if (num != 548604891U)
				{
					if (num != 1390125378U)
					{
						goto IL_478;
					}
					if (!(cardId == "EX1_572"))
					{
						goto IL_478;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerYsera_01, 2.5f);
					goto IL_478;
				}
				else
				{
					if (!(cardId == "ICC_833"))
					{
						goto IL_478;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerDKJaina_01, 2.5f);
					goto IL_478;
				}
			}
			else if (!(cardId == "AT_004"))
			{
				goto IL_478;
			}
		}
		else if (num != 1407050092U)
		{
			if (num != 1440605330U)
			{
				if (num != 1457382949U)
				{
					goto IL_478;
				}
				if (!(cardId == "EX1_560"))
				{
					goto IL_478;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerNozdormu_01, 2.5f);
				goto IL_478;
			}
			else
			{
				if (!(cardId == "EX1_561"))
				{
					goto IL_478;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerAlextrasza_01, 2.5f);
				goto IL_478;
			}
		}
		else
		{
			if (!(cardId == "EX1_563"))
			{
				goto IL_478;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerMalygos_01, 2.5f);
			goto IL_478;
		}
		IL_450:
		yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_69h.m_PlayerArcaneSpell);
		IL_478:
		yield break;
	}

	// Token: 0x06003DAB RID: 15787 RVA: 0x00143F9F File Offset: 0x0014219F
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "EX1_284")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_BossAzureDrake_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040028CC RID: 10444
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_BossAzureDrake_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_BossAzureDrake_01.prefab:6cc1a6ff063a97840b236aaffdb48aff");

	// Token: 0x040028CD RID: 10445
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_BossSilence_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_BossSilence_01.prefab:9a7e18b0ac4e4934ea4db8a25a4632d2");

	// Token: 0x040028CE RID: 10446
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_BossSpellDamage_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_BossSpellDamage_01.prefab:d02e380a7e4ed93469ce733e2b4b8fde");

	// Token: 0x040028CF RID: 10447
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Death_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Death_01.prefab:954bb36183fba9348ae6218a48553ef8");

	// Token: 0x040028D0 RID: 10448
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_DefeatPlayer_01.prefab:75d0908d4811de247b05a9a2bb3abae2");

	// Token: 0x040028D1 RID: 10449
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_EmoteResponse_01.prefab:1266b52465a8ee646857c58d6b460cf7");

	// Token: 0x040028D2 RID: 10450
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Exposition_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Exposition_01.prefab:8cfa53fd391a5224c9c6476adeb16df4");

	// Token: 0x040028D3 RID: 10451
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_01.prefab:852f081f1a21a904dabc5f38bcac0b8f");

	// Token: 0x040028D4 RID: 10452
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_02.prefab:c2573b16afd7a2644af0af154ce9ea27");

	// Token: 0x040028D5 RID: 10453
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_03.prefab:ff87304ea203cf04ca9bdfb088932ea3");

	// Token: 0x040028D6 RID: 10454
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_04.prefab:7aa587f1515026e41a3a1d1cb214a4c3");

	// Token: 0x040028D7 RID: 10455
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_05 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_05.prefab:b0d284f9b15bfdb4cba7e22af7533739");

	// Token: 0x040028D8 RID: 10456
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_HeroPower_06 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_HeroPower_06.prefab:d20d9293e7d205a4ab0e1fc7f5faad8e");

	// Token: 0x040028D9 RID: 10457
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Idle_01.prefab:729e04d54777ecd46b90828238bbdfef");

	// Token: 0x040028DA RID: 10458
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Idle_02.prefab:62795ac5a4e1a6643953bf30a4c4ae56");

	// Token: 0x040028DB RID: 10459
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Idle_03.prefab:e9b5267173d22d748a85763bbef413c5");

	// Token: 0x040028DC RID: 10460
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_Intro_01.prefab:237cac51b8815d34cb61af467e4862da");

	// Token: 0x040028DD RID: 10461
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerAlextrasza_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerAlextrasza_01.prefab:752a3f05d71d94f4abb7e2f26bec8c8d");

	// Token: 0x040028DE RID: 10462
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_01.prefab:65e9f802ca926de428d7f396cae61195");

	// Token: 0x040028DF RID: 10463
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_02 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_02.prefab:394d70654c373774fa5f2066f4d997d4");

	// Token: 0x040028E0 RID: 10464
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerDeathwing_02 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerDeathwing_02.prefab:08858ca81fb84364ba22a8593876a895");

	// Token: 0x040028E1 RID: 10465
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerDKJaina_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerDKJaina_01.prefab:191b63945f0a38d49bc3c5acab79f3e4");

	// Token: 0x040028E2 RID: 10466
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerMageSpell_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerMageSpell_01.prefab:f73d5bdca35c64b47be7d95f51916241");

	// Token: 0x040028E3 RID: 10467
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerMalygos_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerMalygos_01.prefab:b025960290d6efc419735c83e3a16199");

	// Token: 0x040028E4 RID: 10468
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerNozdormu_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerNozdormu_01.prefab:5bc309a5664e5464b9bdc02a836e60a5");

	// Token: 0x040028E5 RID: 10469
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_PlayerYsera_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_PlayerYsera_01.prefab:0aae3064d51890d45bc9592a6eee72a5");

	// Token: 0x040028E6 RID: 10470
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_TurnOne_02 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_TurnOne_02.prefab:731ddb75bfb5c924096fc46ccfc4d538");

	// Token: 0x040028E7 RID: 10471
	private static readonly AssetReference VO_DALA_BOSS_69h_Male_Human_TurnTwo_01 = new AssetReference("VO_DALA_BOSS_69h_Male_Human_TurnTwo_01.prefab:f9e00d6b3bb3f2941aa78f55d59c351b");

	// Token: 0x040028E8 RID: 10472
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x040028E9 RID: 10473
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Idle_01,
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Idle_02,
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_Idle_03
	};

	// Token: 0x040028EA RID: 10474
	private static List<string> m_PlayerArcaneSpell = new List<string>
	{
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_01,
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_PlayerArcaneSpell_02
	};

	// Token: 0x040028EB RID: 10475
	private static List<string> m_BossHeroPower = new List<string>
	{
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_01,
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_02,
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_03,
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_04,
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_05,
		DALA_Dungeon_Boss_69h.VO_DALA_BOSS_69h_Male_Human_HeroPower_06
	};
}
