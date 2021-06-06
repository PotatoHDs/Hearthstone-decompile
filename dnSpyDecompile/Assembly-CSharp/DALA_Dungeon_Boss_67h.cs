using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000470 RID: 1136
public class DALA_Dungeon_Boss_67h : DALA_Dungeon
{
	// Token: 0x06003D8D RID: 15757 RVA: 0x0014351C File Offset: 0x0014171C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_BossAluneth_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_BossArcaneSpell_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_BossArcaneSpell_02,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_BossSpellDamageMinion_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_Death_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_DefeatPlayer_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_EmoteResponse_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_HeroPower_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_HeroPower_02,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_HeroPower_03,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_HeroPower_04,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_HeroPower_05,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_Idle_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_Idle_02,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_Idle_03,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_Intro_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_PlayerArcaneSpell_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_PlayerArchmage_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_PlayerKalecgos_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_PlayerMalygos_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D8E RID: 15758 RVA: 0x001436C0 File Offset: 0x001418C0
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_HeroPower_01,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_HeroPower_02,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_HeroPower_03,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_HeroPower_04,
			DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_HeroPower_05
		};
	}

	// Token: 0x06003D8F RID: 15759 RVA: 0x00143722 File Offset: 0x00141922
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_EmoteResponse_01;
	}

	// Token: 0x06003D90 RID: 15760 RVA: 0x0014375A File Offset: 0x0014195A
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_67h.m_IdleLines;
	}

	// Token: 0x06003D91 RID: 15761 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003D92 RID: 15762 RVA: 0x00143761 File Offset: 0x00141961
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003D93 RID: 15763 RVA: 0x00143777 File Offset: 0x00141977
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
		if (num <= 2272510196U)
		{
			if (num <= 1020708023U)
			{
				if (num != 471245355U)
				{
					if (num != 903264690U)
					{
						if (num != 1020708023U)
						{
							goto IL_398;
						}
						if (!(cardId == "DAL_558"))
						{
							goto IL_398;
						}
					}
					else if (!(cardId == "DAL_553"))
					{
						goto IL_398;
					}
				}
				else
				{
					if (!(cardId == "AT_004"))
					{
						goto IL_398;
					}
					goto IL_2CB;
				}
			}
			else if (num != 1407050092U)
			{
				if (num != 1440752425U)
				{
					if (num != 2272510196U)
					{
						goto IL_398;
					}
					if (!(cardId == "GIL_691"))
					{
						goto IL_398;
					}
				}
				else if (!(cardId == "EX1_559"))
				{
					goto IL_398;
				}
			}
			else
			{
				if (!(cardId == "EX1_563"))
				{
					goto IL_398;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_PlayerMalygos_01, 2.5f);
				goto IL_398;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_PlayerArchmage_01, 2.5f);
			goto IL_398;
		}
		if (num <= 3481427772U)
		{
			if (num != 2560247920U)
			{
				if (num != 3064386877U)
				{
					if (num != 3481427772U)
					{
						goto IL_398;
					}
					if (!(cardId == "DAL_609"))
					{
						goto IL_398;
					}
					yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_PlayerKalecgos_01, 2.5f);
					goto IL_398;
				}
				else if (!(cardId == "CFM_623"))
				{
					goto IL_398;
				}
			}
			else if (!(cardId == "DS1_185"))
			{
				goto IL_398;
			}
		}
		else if (num != 4022523648U)
		{
			if (num != 4045846843U)
			{
				if (num != 4146512557U)
				{
					goto IL_398;
				}
				if (!(cardId == "CS2_025"))
				{
					goto IL_398;
				}
			}
			else if (!(cardId == "CS2_023"))
			{
				goto IL_398;
			}
		}
		else if (!(cardId == "EX1_277"))
		{
			goto IL_398;
		}
		IL_2CB:
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_PlayerArcaneSpell_01, 2.5f);
		IL_398:
		yield break;
	}

	// Token: 0x06003D94 RID: 15764 RVA: 0x0014378D File Offset: 0x0014198D
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
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 2560247920U)
		{
			if (num <= 885464893U)
			{
				if (num != 471245355U)
				{
					if (num != 885464893U)
					{
						goto IL_314;
					}
					if (!(cardId == "BRM_002"))
					{
						goto IL_314;
					}
				}
				else
				{
					if (!(cardId == "AT_004"))
					{
						goto IL_314;
					}
					goto IL_2F2;
				}
			}
			else if (num != 1769836603U)
			{
				if (num != 2308101768U)
				{
					if (num != 2560247920U)
					{
						goto IL_314;
					}
					if (!(cardId == "DS1_185"))
					{
						goto IL_314;
					}
					goto IL_2F2;
				}
				else if (!(cardId == "DAL_182"))
				{
					goto IL_314;
				}
			}
			else
			{
				if (!(cardId == "LOOT_108"))
				{
					goto IL_314;
				}
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_BossAluneth_01, 2.5f);
				goto IL_314;
			}
		}
		else if (num <= 3999364365U)
		{
			if (num != 3064386877U)
			{
				if (num != 3999364365U)
				{
					goto IL_314;
				}
				if (!(cardId == "NEW1_020"))
				{
					goto IL_314;
				}
			}
			else
			{
				if (!(cardId == "CFM_623"))
				{
					goto IL_314;
				}
				goto IL_2F2;
			}
		}
		else if (num != 4022523648U)
		{
			if (num != 4045846843U)
			{
				if (num != 4146512557U)
				{
					goto IL_314;
				}
				if (!(cardId == "CS2_025"))
				{
					goto IL_314;
				}
				goto IL_2F2;
			}
			else
			{
				if (!(cardId == "CS2_023"))
				{
					goto IL_314;
				}
				goto IL_2F2;
			}
		}
		else
		{
			if (!(cardId == "EX1_277"))
			{
				goto IL_314;
			}
			goto IL_2F2;
		}
		yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_BossSpellDamageMinion_01, 2.5f);
		goto IL_314;
		IL_2F2:
		yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_67h.m_BossArcaneSpells);
		IL_314:
		yield break;
	}

	// Token: 0x040028A1 RID: 10401
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_BossAluneth_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_BossAluneth_01.prefab:926c214e7240da94e8279ad11ce74fca");

	// Token: 0x040028A2 RID: 10402
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_BossArcaneSpell_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_BossArcaneSpell_01.prefab:e86215487ae8d564ba7131e9b3f24d0a");

	// Token: 0x040028A3 RID: 10403
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_BossArcaneSpell_02 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_BossArcaneSpell_02.prefab:b33c2ead502fad44d9c499ef77cc8dd6");

	// Token: 0x040028A4 RID: 10404
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_BossSpellDamageMinion_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_BossSpellDamageMinion_01.prefab:aaaa69d2a47c22043b5335fa4bd6ab0c");

	// Token: 0x040028A5 RID: 10405
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_Death_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_Death_01.prefab:799a46bae9df7bf4e845b8267e664e91");

	// Token: 0x040028A6 RID: 10406
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_DefeatPlayer_01.prefab:4aeba524ab4f16f4d83ceec65d580673");

	// Token: 0x040028A7 RID: 10407
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_EmoteResponse_01.prefab:be2521311d8bd7a49bcdbe25e2edacb2");

	// Token: 0x040028A8 RID: 10408
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_HeroPower_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_HeroPower_01.prefab:3f475154a9755844eb8ab9cdcf78fbeb");

	// Token: 0x040028A9 RID: 10409
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_HeroPower_02 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_HeroPower_02.prefab:8c4cb6dee85d4684a8493cd0c54db053");

	// Token: 0x040028AA RID: 10410
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_HeroPower_03 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_HeroPower_03.prefab:9064cf19e63c3b947a3cf51e9049193f");

	// Token: 0x040028AB RID: 10411
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_HeroPower_04 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_HeroPower_04.prefab:14b33d653d4ed8040ae9d5ec9e6eff11");

	// Token: 0x040028AC RID: 10412
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_HeroPower_05 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_HeroPower_05.prefab:9618e072fc851b24db3f50ca10a47671");

	// Token: 0x040028AD RID: 10413
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_Idle_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_Idle_01.prefab:2f0be226ceba22e438d65b14aee01e3e");

	// Token: 0x040028AE RID: 10414
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_Idle_02 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_Idle_02.prefab:d8349d70b48c23046a62b69a3085eef1");

	// Token: 0x040028AF RID: 10415
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_Idle_03 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_Idle_03.prefab:dd03ba71892196b4082316004bad9ab7");

	// Token: 0x040028B0 RID: 10416
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_Intro_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_Intro_01.prefab:5291b993a47120146ba54cce9955a5c5");

	// Token: 0x040028B1 RID: 10417
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_PlayerArcaneSpell_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_PlayerArcaneSpell_01.prefab:12f65551fdb1698428a6b80bea806bbd");

	// Token: 0x040028B2 RID: 10418
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_PlayerArchmage_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_PlayerArchmage_01.prefab:018123b4f40d4924a8f74718b88258a5");

	// Token: 0x040028B3 RID: 10419
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_PlayerKalecgos_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_PlayerKalecgos_01.prefab:4b3d74dc8450a8b4da6bc61d95004ae4");

	// Token: 0x040028B4 RID: 10420
	private static readonly AssetReference VO_DALA_BOSS_67h_Male_Human_PlayerMalygos_01 = new AssetReference("VO_DALA_BOSS_67h_Male_Human_PlayerMalygos_01.prefab:4ec69bf3eb81e79409ac2f5a0427c824");

	// Token: 0x040028B5 RID: 10421
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x040028B6 RID: 10422
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_Idle_01,
		DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_Idle_02,
		DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_Idle_03
	};

	// Token: 0x040028B7 RID: 10423
	private static List<string> m_BossArcaneSpells = new List<string>
	{
		DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_BossArcaneSpell_01,
		DALA_Dungeon_Boss_67h.VO_DALA_BOSS_67h_Male_Human_BossArcaneSpell_02
	};
}
