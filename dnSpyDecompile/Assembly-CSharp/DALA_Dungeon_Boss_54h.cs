using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000463 RID: 1123
public class DALA_Dungeon_Boss_54h : DALA_Dungeon
{
	// Token: 0x06003CED RID: 15597 RVA: 0x0013E35C File Offset: 0x0013C55C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_BossBigSpell_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_02,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_02,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Death_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_DefeatPlayer_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_EmoteResponse_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Exposition_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_02,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_02,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_03,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Idle_02,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Idle_03,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Idle_04,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Intro_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerBigSpell_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerLackey_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerSafeguard_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_01,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_02,
			DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerWagglePick_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003CEE RID: 15598 RVA: 0x0013E540 File Offset: 0x0013C740
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_54h.m_IdleLines;
	}

	// Token: 0x06003CEF RID: 15599 RVA: 0x0013E547 File Offset: 0x0013C747
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_EmoteResponse_01;
	}

	// Token: 0x06003CF0 RID: 15600 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003CF1 RID: 15601 RVA: 0x0013E57F File Offset: 0x0013C77F
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Exposition_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerBigSpell_01, 2.5f);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_54h.m_PlayerUnlockMana);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_54h.m_PlayerHeroPowerTriggers);
			break;
		case 105:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_54h.m_BossHeroPowerTriggers);
			break;
		case 106:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_54h.m_BossOverloadPass);
			break;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_BossBigSpell_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003CF2 RID: 15602 RVA: 0x0013E595 File Offset: 0x0013C795
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
		if (num <= 3615501629U)
		{
			if (num != 3514835915U)
			{
				if (num != 3598724010U)
				{
					if (num != 3615501629U)
					{
						goto IL_2AF;
					}
					if (!(cardId == "DAL_615"))
					{
						goto IL_2AF;
					}
				}
				else if (!(cardId == "DAL_614"))
				{
					goto IL_2AF;
				}
			}
			else if (!(cardId == "DAL_613"))
			{
				goto IL_2AF;
			}
		}
		else if (num <= 3786761772U)
		{
			if (num != 3785628939U)
			{
				if (num != 3786761772U)
				{
					goto IL_2AF;
				}
				if (!(cardId == "DAL_739"))
				{
					goto IL_2AF;
				}
			}
			else if (!(cardId == "DAL_741"))
			{
				goto IL_2AF;
			}
		}
		else if (num != 3836947534U)
		{
			if (num != 4060285601U)
			{
				goto IL_2AF;
			}
			if (!(cardId == "DAL_088"))
			{
				goto IL_2AF;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerSafeguard_01, 2.5f);
			goto IL_2AF;
		}
		else
		{
			if (!(cardId == "DAL_720"))
			{
				goto IL_2AF;
			}
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerWagglePick_01, 2.5f);
			goto IL_2AF;
		}
		yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerLackey_01, 2.5f);
		IL_2AF:
		yield break;
	}

	// Token: 0x06003CF3 RID: 15603 RVA: 0x0013E5AB File Offset: 0x0013C7AB
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
		if (cardId == "BRM_011")
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_54h.m_BossLavaShock);
		}
		yield break;
	}

	// Token: 0x040026F3 RID: 9971
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_BossBigSpell_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_BossBigSpell_01.prefab:2ad92defe613eb044a3ff8783d7409fb");

	// Token: 0x040026F4 RID: 9972
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_01.prefab:b1b6892cd8914ba4489118cd65b62f33");

	// Token: 0x040026F5 RID: 9973
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_02.prefab:70f6d82bab328ae4797463d8a8bd64ad");

	// Token: 0x040026F6 RID: 9974
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_01.prefab:96b2d24132b421c4d83cd5dfaf8ee0ea");

	// Token: 0x040026F7 RID: 9975
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_02.prefab:ea9058ed4596a8946af2dde75df44432");

	// Token: 0x040026F8 RID: 9976
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Death_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Death_01.prefab:6405e6724f466b44ab720a2cefc7ca81");

	// Token: 0x040026F9 RID: 9977
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_DefeatPlayer_01.prefab:5ef8cae2dfe289144bba2d0bee96ddcf");

	// Token: 0x040026FA RID: 9978
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_EmoteResponse_01.prefab:469889c1cb4db2c4499b9bc5ac9688bb");

	// Token: 0x040026FB RID: 9979
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Exposition_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Exposition_01.prefab:64b51fbcb58d23047a25770de356e32a");

	// Token: 0x040026FC RID: 9980
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_01.prefab:86fcc403ff4a6234aaaa1940883ddbb6");

	// Token: 0x040026FD RID: 9981
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_02.prefab:64ea229c4e4e1b8498bc84abd219351c");

	// Token: 0x040026FE RID: 9982
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_01.prefab:b2e7e9e06e551b4479687281e49daa4f");

	// Token: 0x040026FF RID: 9983
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_02.prefab:191edf8126d85904bb1b9d561c6a01e9");

	// Token: 0x04002700 RID: 9984
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_03 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_03.prefab:3d773d9012ec5184b88e839c6b4217bb");

	// Token: 0x04002701 RID: 9985
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Idle_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Idle_02.prefab:d49961c4d1eddb0459e0f79d0fc04eb8");

	// Token: 0x04002702 RID: 9986
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Idle_03 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Idle_03.prefab:2bd18c7f92b00c44b8a6d86f8657a4d2");

	// Token: 0x04002703 RID: 9987
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Idle_04 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Idle_04.prefab:87463035dbf5d2745bd7965359f20edd");

	// Token: 0x04002704 RID: 9988
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_Intro_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_Intro_01.prefab:884a25bceb1f1db44bb516787dd7ef82");

	// Token: 0x04002705 RID: 9989
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerBigSpell_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerBigSpell_01.prefab:7d9970dedeb8112498bf49609af5eea5");

	// Token: 0x04002706 RID: 9990
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerLackey_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerLackey_01.prefab:de3b4da57d39de3469fb7c334f0b0132");

	// Token: 0x04002707 RID: 9991
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerSafeguard_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerSafeguard_01.prefab:1a32183f1da1ab347b299692e7550bd8");

	// Token: 0x04002708 RID: 9992
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_01.prefab:29be779eeea6c5e45962ceb0679e8a3b");

	// Token: 0x04002709 RID: 9993
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_02 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_02.prefab:d883d936d67bbc945b0cfa9992af98ec");

	// Token: 0x0400270A RID: 9994
	private static readonly AssetReference VO_DALA_BOSS_54h_Male_Goblin_PlayerWagglePick_01 = new AssetReference("VO_DALA_BOSS_54h_Male_Goblin_PlayerWagglePick_01.prefab:49b0c84d18cd3c24a848117799bd428b");

	// Token: 0x0400270B RID: 9995
	private static List<string> m_BossLavaShock = new List<string>
	{
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_01,
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_BossLavaShock_02
	};

	// Token: 0x0400270C RID: 9996
	private static List<string> m_BossOverloadPass = new List<string>
	{
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_01,
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_BossOverloadPass_02
	};

	// Token: 0x0400270D RID: 9997
	private static List<string> m_BossHeroPowerTriggers = new List<string>
	{
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_01,
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTrigger_02
	};

	// Token: 0x0400270E RID: 9998
	private static List<string> m_PlayerHeroPowerTriggers = new List<string>
	{
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_01,
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_02,
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_HeroPowerTriggerPlayer_03
	};

	// Token: 0x0400270F RID: 9999
	private static List<string> m_PlayerUnlockMana = new List<string>
	{
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_01,
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_PlayerUnlockMana_02
	};

	// Token: 0x04002710 RID: 10000
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Idle_02,
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Idle_03,
		DALA_Dungeon_Boss_54h.VO_DALA_BOSS_54h_Male_Goblin_Idle_04
	};

	// Token: 0x04002711 RID: 10001
	private HashSet<string> m_playedLines = new HashSet<string>();
}
