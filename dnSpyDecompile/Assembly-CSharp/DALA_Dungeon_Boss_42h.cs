using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000457 RID: 1111
public class DALA_Dungeon_Boss_42h : DALA_Dungeon
{
	// Token: 0x06003C58 RID: 15448 RVA: 0x0013AA00 File Offset: 0x00138C00
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_BossPogohopper_01,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_Death_02,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_DefeatPlayer_01,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_EmoteResponse_01,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_HeroPower_01,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_HeroPower_02,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_HeroPower_03,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_Idle_01,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_Idle_02,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_Intro_01,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_PlayerPogohopper_01,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_PlayerPogohopper2_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003C59 RID: 15449 RVA: 0x0013AB24 File Offset: 0x00138D24
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_HeroPower_01,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_HeroPower_02,
			DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_HeroPower_03
		};
	}

	// Token: 0x06003C5A RID: 15450 RVA: 0x0013AB5B File Offset: 0x00138D5B
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_42h.m_IdleLines;
	}

	// Token: 0x06003C5B RID: 15451 RVA: 0x0013AB62 File Offset: 0x00138D62
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_EmoteResponse_01;
	}

	// Token: 0x06003C5C RID: 15452 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003C5D RID: 15453 RVA: 0x0013AB9A File Offset: 0x00138D9A
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_PlayerPogohopper2_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_PlayerPogohopper_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003C5E RID: 15454 RVA: 0x0013ABB0 File Offset: 0x00138DB0
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

	// Token: 0x06003C5F RID: 15455 RVA: 0x0013ABC6 File Offset: 0x00138DC6
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
		if (cardId == "BOT_283")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_BossPogohopper_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040025DB RID: 9691
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_BossPogohopper_01 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_BossPogohopper_01.prefab:6c6e3e4336789c046b9e9535867cf85a");

	// Token: 0x040025DC RID: 9692
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_Death_02 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_Death_02.prefab:fdc9f12e07c9cfc44b3d4cb7d912c335");

	// Token: 0x040025DD RID: 9693
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_DefeatPlayer_01.prefab:a6d25ab360f39ad4e8b5c3530f7e13d2");

	// Token: 0x040025DE RID: 9694
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_EmoteResponse_01.prefab:c1e616b96cf8a7a4bb00a77f265a03a7");

	// Token: 0x040025DF RID: 9695
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_HeroPower_01 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_HeroPower_01.prefab:5fe596d383b8cae45951381d720b30e9");

	// Token: 0x040025E0 RID: 9696
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_HeroPower_02 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_HeroPower_02.prefab:49d62834f322c774798fd93c64bd6e38");

	// Token: 0x040025E1 RID: 9697
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_HeroPower_03 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_HeroPower_03.prefab:a76eea147c071324595a15a275518f47");

	// Token: 0x040025E2 RID: 9698
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_Idle_01 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_Idle_01.prefab:96f5e3391e6fc2640a9da7208a69dbb5");

	// Token: 0x040025E3 RID: 9699
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_Idle_02 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_Idle_02.prefab:47287fc18da1d0b41a072f8a619d3ad8");

	// Token: 0x040025E4 RID: 9700
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_Intro_01 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_Intro_01.prefab:8d01c93bbe719f9439e4437aefd8a4de");

	// Token: 0x040025E5 RID: 9701
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_PlayerPogohopper_01 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_PlayerPogohopper_01.prefab:3b32211ad7d75394aa45f1b72f7c3445");

	// Token: 0x040025E6 RID: 9702
	private static readonly AssetReference VO_DALA_BOSS_42h_Male_Mech_PlayerPogohopper2_01 = new AssetReference("VO_DALA_BOSS_42h_Male_Mech_PlayerPogohopper2_01.prefab:f210aca0e408f47479e8b816b2ba0ff1");

	// Token: 0x040025E7 RID: 9703
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_Idle_01,
		DALA_Dungeon_Boss_42h.VO_DALA_BOSS_42h_Male_Mech_Idle_02
	};

	// Token: 0x040025E8 RID: 9704
	private HashSet<string> m_playedLines = new HashSet<string>();
}
