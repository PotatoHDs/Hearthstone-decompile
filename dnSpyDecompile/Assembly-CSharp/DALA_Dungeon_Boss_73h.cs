using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000476 RID: 1142
public class DALA_Dungeon_Boss_73h : DALA_Dungeon
{
	// Token: 0x06003DD5 RID: 15829 RVA: 0x0014524C File Offset: 0x0014344C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_BossWhirlwindTempest_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_Death_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_DefeatPlayer_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_EmoteResponse_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_HeroPower_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_HeroPower_02,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_HeroPower_03,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_HeroPower_04,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_Idle_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_Idle_02,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_Idle_04,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_Intro_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_PlayerShamanSpell_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_PlayerTotem_02,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_PlayerVoltron_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_PlayerWindfury_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_PlayerWindfury_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003DD6 RID: 15830 RVA: 0x001453C0 File Offset: 0x001435C0
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_HeroPower_01,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_HeroPower_02,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_HeroPower_03,
			DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_HeroPower_04
		};
	}

	// Token: 0x06003DD7 RID: 15831 RVA: 0x00145412 File Offset: 0x00143612
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_EmoteResponse_01;
	}

	// Token: 0x06003DD8 RID: 15832 RVA: 0x0014544A File Offset: 0x0014364A
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_73h.m_IdleLines;
	}

	// Token: 0x06003DD9 RID: 15833 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003DDA RID: 15834 RVA: 0x00145451 File Offset: 0x00143651
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
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_PlayerShamanSpell_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_PlayerTotem_02, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_PlayerVoltron_01, 2.5f);
			break;
		case 104:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_73h.m_PlayerWindfury);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003DDB RID: 15835 RVA: 0x00145467 File Offset: 0x00143667
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x06003DDC RID: 15836 RVA: 0x0014547D File Offset: 0x0014367D
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
		if (cardId == "DAL_742")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_BossWhirlwindTempest_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400293B RID: 10555
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_BossWhirlwindTempest_01 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_BossWhirlwindTempest_01.prefab:466d89ef2c68efd40a75dd29520d9e0d");

	// Token: 0x0400293C RID: 10556
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_Death_01 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_Death_01.prefab:6c3f7866c2830d14c9e0720765f7be14");

	// Token: 0x0400293D RID: 10557
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_DefeatPlayer_01.prefab:d290b7b568e2d274eb443c34c852da8e");

	// Token: 0x0400293E RID: 10558
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_EmoteResponse_01.prefab:fccbfb7f0a8fb6b40a5c1d097c8cc2c4");

	// Token: 0x0400293F RID: 10559
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_HeroPower_01 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_HeroPower_01.prefab:3bfeab29fd637fc4cb2a0b03b6823c3a");

	// Token: 0x04002940 RID: 10560
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_HeroPower_02 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_HeroPower_02.prefab:b725ba947f2d85a45a380b691c43b42a");

	// Token: 0x04002941 RID: 10561
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_HeroPower_03 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_HeroPower_03.prefab:70585c8072ade0046852bb9b0ce3ffdf");

	// Token: 0x04002942 RID: 10562
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_HeroPower_04 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_HeroPower_04.prefab:d7fc0ae02711bce41a544cabf5b4388c");

	// Token: 0x04002943 RID: 10563
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_Idle_01 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_Idle_01.prefab:6bfaf6737324986409471b17ebcbad50");

	// Token: 0x04002944 RID: 10564
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_Idle_02 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_Idle_02.prefab:b6b64a9016f72bd46977288750bf94ec");

	// Token: 0x04002945 RID: 10565
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_Idle_04 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_Idle_04.prefab:98ad12a474b125a4386edab1e2a24981");

	// Token: 0x04002946 RID: 10566
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_Intro_01 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_Intro_01.prefab:1b3bc54107269e043bc16c5dfa84d3f2");

	// Token: 0x04002947 RID: 10567
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_PlayerShamanSpell_01 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_PlayerShamanSpell_01.prefab:6898248dc3f25de4e9019b2d25c02395");

	// Token: 0x04002948 RID: 10568
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_PlayerTotem_02 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_PlayerTotem_02.prefab:697c531ba6483354da24c1d3e5d542d4");

	// Token: 0x04002949 RID: 10569
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_PlayerVoltron_01 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_PlayerVoltron_01.prefab:ced4fb774876fff4a89684447fbfa74c");

	// Token: 0x0400294A RID: 10570
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_PlayerWindfury_01 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_PlayerWindfury_01.prefab:46239d226057db84880ecce10f7232f1");

	// Token: 0x0400294B RID: 10571
	private static readonly AssetReference VO_DALA_BOSS_73h_Male_Tauren_PlayerWindfury_02 = new AssetReference("VO_DALA_BOSS_73h_Male_Tauren_PlayerWindfury_02.prefab:80b01e91fb23bfc419dc6921df59c9fe");

	// Token: 0x0400294C RID: 10572
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_Idle_01,
		DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_Idle_02,
		DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_Idle_04
	};

	// Token: 0x0400294D RID: 10573
	private static List<string> m_PlayerWindfury = new List<string>
	{
		DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_PlayerWindfury_01,
		DALA_Dungeon_Boss_73h.VO_DALA_BOSS_73h_Male_Tauren_PlayerWindfury_02
	};

	// Token: 0x0400294E RID: 10574
	private HashSet<string> m_playedLines = new HashSet<string>();
}
