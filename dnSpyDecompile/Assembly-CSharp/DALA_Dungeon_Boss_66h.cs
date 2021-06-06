using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200046F RID: 1135
public class DALA_Dungeon_Boss_66h : DALA_Dungeon
{
	// Token: 0x06003D82 RID: 15746 RVA: 0x001430E8 File Offset: 0x001412E8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_BossBigMinion_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_BossBurnCard_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_BossCurseofWeakness_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_Death_02,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_DefeatPlayer_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_EmoteResponse_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_02,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_03,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_04,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_05,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_06,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPowerEmpty_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_Idle_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_Idle_02,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_Idle_03,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_Intro_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_PlayerBurnCard_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_PlayerLeeroy_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_PlayerRushMinion_01,
			DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_TriggerBomb_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D83 RID: 15747 RVA: 0x0014329C File Offset: 0x0014149C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_EmoteResponse_01;
	}

	// Token: 0x06003D84 RID: 15748 RVA: 0x001432D4 File Offset: 0x001414D4
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_66h.m_IdleLines;
	}

	// Token: 0x06003D85 RID: 15749 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003D86 RID: 15750 RVA: 0x001432DB File Offset: 0x001414DB
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
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_66h.m_HeroPower);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPowerEmpty_01, 2.5f);
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_BossBigMinion_01, 2.5f);
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_BossBurnCard_01, 2.5f);
			break;
		case 105:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_PlayerBurnCard_01, 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_PlayerRushMinion_01, 2.5f);
			break;
		case 107:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_TriggerBomb_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003D87 RID: 15751 RVA: 0x001432F1 File Offset: 0x001414F1
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
		if (cardId == "EX1_116")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_PlayerLeeroy_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003D88 RID: 15752 RVA: 0x00143307 File Offset: 0x00141507
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
		if (cardId == "GIL_665")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_BossCurseofWeakness_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002889 RID: 10377
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_BossBigMinion_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_BossBigMinion_01.prefab:be8b725ef2070034f8c607950263517d");

	// Token: 0x0400288A RID: 10378
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_BossBurnCard_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_BossBurnCard_01.prefab:64a439b572181544fa3f55a7840743fe");

	// Token: 0x0400288B RID: 10379
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_BossCurseofWeakness_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_BossCurseofWeakness_01.prefab:4285b2cf2f0ccf74584a462585b7209a");

	// Token: 0x0400288C RID: 10380
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_Death_02 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_Death_02.prefab:1e3d237a2bc837b43b8d6498e7f11cd4");

	// Token: 0x0400288D RID: 10381
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_DefeatPlayer_01.prefab:d104b352b3b73de4db563c6d218be072");

	// Token: 0x0400288E RID: 10382
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_EmoteResponse_01.prefab:59d031ce9099fa64cb418e56a1d4ee47");

	// Token: 0x0400288F RID: 10383
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_HeroPower_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_HeroPower_01.prefab:b5f2b059ccd707843b60b5471aa23b1f");

	// Token: 0x04002890 RID: 10384
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_HeroPower_02 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_HeroPower_02.prefab:d0c6fd89c1792a84b90f88af6993b085");

	// Token: 0x04002891 RID: 10385
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_HeroPower_03 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_HeroPower_03.prefab:be9a6db46fb3e284a9ab70f7fd38364c");

	// Token: 0x04002892 RID: 10386
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_HeroPower_04 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_HeroPower_04.prefab:578b6b9e8f8f5064fbed39488147c45e");

	// Token: 0x04002893 RID: 10387
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_HeroPower_05 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_HeroPower_05.prefab:26d56c9852721d94ba6f11ce8f2bbbb0");

	// Token: 0x04002894 RID: 10388
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_HeroPower_06 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_HeroPower_06.prefab:6efed8044a876d74dad708480f3df43e");

	// Token: 0x04002895 RID: 10389
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_HeroPowerEmpty_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_HeroPowerEmpty_01.prefab:88671044a4728d447bb2e3a27fcf38b4");

	// Token: 0x04002896 RID: 10390
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_Idle_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_Idle_01.prefab:cda3f1b374d8cc449b3c9ff831574e75");

	// Token: 0x04002897 RID: 10391
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_Idle_02 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_Idle_02.prefab:7f728494ef4de8542af4df37a0da72d5");

	// Token: 0x04002898 RID: 10392
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_Idle_03 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_Idle_03.prefab:13a74c53478e80f4aa8504954cade4b9");

	// Token: 0x04002899 RID: 10393
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_Intro_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_Intro_01.prefab:73beceeb89a2af44691589133437ec11");

	// Token: 0x0400289A RID: 10394
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_PlayerBurnCard_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_PlayerBurnCard_01.prefab:823fbe07a5f04c94c88ba5ff6c9d2f71");

	// Token: 0x0400289B RID: 10395
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_PlayerLeeroy_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_PlayerLeeroy_01.prefab:f1f94b7de565f4a4c9a2bca9dd23cbd9");

	// Token: 0x0400289C RID: 10396
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_PlayerRushMinion_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_PlayerRushMinion_01.prefab:42bbc558956026c4b838fbecda71b155");

	// Token: 0x0400289D RID: 10397
	private static readonly AssetReference VO_DALA_BOSS_66h_Female_Undead_TriggerBomb_01 = new AssetReference("VO_DALA_BOSS_66h_Female_Undead_TriggerBomb_01.prefab:580c04fd2042d8649ba267cbac635356");

	// Token: 0x0400289E RID: 10398
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_Idle_01,
		DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_Idle_02,
		DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_Idle_03
	};

	// Token: 0x0400289F RID: 10399
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_01,
		DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_02,
		DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_03,
		DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_04,
		DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_05,
		DALA_Dungeon_Boss_66h.VO_DALA_BOSS_66h_Female_Undead_HeroPower_06
	};

	// Token: 0x040028A0 RID: 10400
	private HashSet<string> m_playedLines = new HashSet<string>();
}
