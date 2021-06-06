using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000474 RID: 1140
public class DALA_Dungeon_Boss_71h : DALA_Dungeon
{
	// Token: 0x06003DBC RID: 15804 RVA: 0x001447CC File Offset: 0x001429CC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_BossForceOfNature_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_BossSoulOfTheForest_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_BossTreeSpeaker_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Death_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_DefeatPlayer_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_EmoteResponse_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_02,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_03,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_04,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_05,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_06,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Idle_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Idle_02,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Idle_03,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Idle_04,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Intro_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_PlayerFlamestrike_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_PlayerNaturalize_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_PlayerSap_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_PlayerTreant_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003DBD RID: 15805 RVA: 0x00144980 File Offset: 0x00142B80
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_EmoteResponse_01;
	}

	// Token: 0x06003DBE RID: 15806 RVA: 0x001449B8 File Offset: 0x00142BB8
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_01,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_02,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_03,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_04,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_05,
			DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_HeroPower_06
		};
	}

	// Token: 0x06003DBF RID: 15807 RVA: 0x00144A2A File Offset: 0x00142C2A
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_71h.m_IdleLines;
	}

	// Token: 0x06003DC0 RID: 15808 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003DC1 RID: 15809 RVA: 0x00144A31 File Offset: 0x00142C31
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003DC2 RID: 15810 RVA: 0x00144A47 File Offset: 0x00142C47
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
		if (num <= 1945129914U)
		{
			if (num != 1095804670U)
			{
				if (num != 1791989495U)
				{
					if (num != 1945129914U)
					{
						goto IL_2EF;
					}
					if (!(cardId == "EX1_158t"))
					{
						goto IL_2EF;
					}
				}
				else if (!(cardId == "GIL_663t"))
				{
					goto IL_2EF;
				}
			}
			else if (!(cardId == "DAL_256t2"))
			{
				goto IL_2EF;
			}
		}
		else if (num <= 3368492501U)
		{
			if (num != 2165687222U)
			{
				if (num != 3368492501U)
				{
					goto IL_2EF;
				}
				if (!(cardId == "FP1_019t"))
				{
					goto IL_2EF;
				}
			}
			else
			{
				if (!(cardId == "EX1_161"))
				{
					goto IL_2EF;
				}
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_PlayerNaturalize_01, 2.5f);
				goto IL_2EF;
			}
		}
		else if (num != 3653573752U)
		{
			if (num != 4196992509U)
			{
				goto IL_2EF;
			}
			if (!(cardId == "CS2_032"))
			{
				goto IL_2EF;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_PlayerFlamestrike_01, 2.5f);
			goto IL_2EF;
		}
		else
		{
			if (!(cardId == "EX1_581"))
			{
				goto IL_2EF;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_PlayerSap_01, 2.5f);
			goto IL_2EF;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_PlayerTreant_01, 2.5f);
		IL_2EF:
		yield break;
	}

	// Token: 0x06003DC3 RID: 15811 RVA: 0x00144A5D File Offset: 0x00142C5D
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
		if (!(cardId == "EX1_571"))
		{
			if (!(cardId == "EX1_158"))
			{
				if (cardId == "TRL_341")
				{
					yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_BossTreeSpeaker_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_BossSoulOfTheForest_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_BossForceOfNature_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002908 RID: 10504
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_BossForceOfNature_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_BossForceOfNature_01.prefab:4645fa632d0b43c40ab2882239999533");

	// Token: 0x04002909 RID: 10505
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_BossSoulOfTheForest_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_BossSoulOfTheForest_01.prefab:d27c2b80ff3ccbe4189e803dce82b8f0");

	// Token: 0x0400290A RID: 10506
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_BossTreeSpeaker_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_BossTreeSpeaker_01.prefab:96abaac2bbbe6834cb59133bb86de706");

	// Token: 0x0400290B RID: 10507
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_Death_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_Death_01.prefab:f42a4ea6ddc7ee64d8ccdbced4b8db8c");

	// Token: 0x0400290C RID: 10508
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_DefeatPlayer_01.prefab:51f415d552af26640bfb9657a041a36e");

	// Token: 0x0400290D RID: 10509
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_EmoteResponse_01.prefab:ada70a577a9e23e41b4c7c39c6202064");

	// Token: 0x0400290E RID: 10510
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_HeroPower_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_HeroPower_01.prefab:7146c0cfa5880bb4aad1a829737b5f11");

	// Token: 0x0400290F RID: 10511
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_HeroPower_02 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_HeroPower_02.prefab:cdd29f02e83b0264c976bee1ed4c3bb2");

	// Token: 0x04002910 RID: 10512
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_HeroPower_03 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_HeroPower_03.prefab:58184868872afbb4daaa542409be1a49");

	// Token: 0x04002911 RID: 10513
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_HeroPower_04 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_HeroPower_04.prefab:beb32b4b3d5f44a4486f4f016f1df916");

	// Token: 0x04002912 RID: 10514
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_HeroPower_05 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_HeroPower_05.prefab:83f214dabce3b964abb15030fd7c8d2d");

	// Token: 0x04002913 RID: 10515
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_HeroPower_06 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_HeroPower_06.prefab:dc09a3e7b5e9b7f468a62042b46c08f5");

	// Token: 0x04002914 RID: 10516
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_Idle_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_Idle_01.prefab:eb135af0c5bcc8d469e6dee9ace22aa0");

	// Token: 0x04002915 RID: 10517
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_Idle_02 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_Idle_02.prefab:70bda963e55394c4797e1aa4218dc7b8");

	// Token: 0x04002916 RID: 10518
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_Idle_03 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_Idle_03.prefab:ca21eae7ed86c714699671118bcc2353");

	// Token: 0x04002917 RID: 10519
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_Idle_04 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_Idle_04.prefab:fba6fc265304d0441be7c834e220822c");

	// Token: 0x04002918 RID: 10520
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_Intro_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_Intro_01.prefab:aa39897d00f4d0b4382f9a0fa9beb94d");

	// Token: 0x04002919 RID: 10521
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_PlayerFlamestrike_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_PlayerFlamestrike_01.prefab:033b669252bb62344aae7536e8b70e9e");

	// Token: 0x0400291A RID: 10522
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_PlayerNaturalize_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_PlayerNaturalize_01.prefab:042aa2e8afb130c41af3f565e54a9aa8");

	// Token: 0x0400291B RID: 10523
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_PlayerSap_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_PlayerSap_01.prefab:defb484a05fd7f1448988c1e94e9d3d8");

	// Token: 0x0400291C RID: 10524
	private static readonly AssetReference VO_DALA_BOSS_71h_Female_NightElf_PlayerTreant_01 = new AssetReference("VO_DALA_BOSS_71h_Female_NightElf_PlayerTreant_01.prefab:ac454ad4346f53e41838ed237f9c22d8");

	// Token: 0x0400291D RID: 10525
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Idle_01,
		DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Idle_02,
		DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Idle_03,
		DALA_Dungeon_Boss_71h.VO_DALA_BOSS_71h_Female_NightElf_Idle_04
	};

	// Token: 0x0400291E RID: 10526
	private HashSet<string> m_playedLines = new HashSet<string>();
}
