using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D1 RID: 1233
public class DRGA_Evil_Fight_03 : DRGA_Dungeon
{
	// Token: 0x0600420D RID: 16909 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600420E RID: 16910 RVA: 0x00161CA4 File Offset: 0x0015FEA4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigAOESpell_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigSpellFace_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_ALT_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_RogueSpell_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossAttack_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStart_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_EmoteResponse_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_01_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_02_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_03_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_01_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_02_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_03_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_04_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_05_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BigMech_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Bomb_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BrannMinion_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_EliseMinion_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_FinleyMinion_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_LoEMinion_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Pilfer_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoHero_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoMinion_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_01_Hero_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_02_Hero_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_03_Hero_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_04_Hero_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_PlayerStart_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01,
			DRGA_Evil_Fight_03.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600420F RID: 16911 RVA: 0x00161F58 File Offset: 0x00160158
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_IdleLines;
	}

	// Token: 0x06004210 RID: 16912 RVA: 0x0016133C File Offset: 0x0015F53C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x06004211 RID: 16913 RVA: 0x00161F60 File Offset: 0x00160160
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x06004212 RID: 16914 RVA: 0x00162031 File Offset: 0x00160231
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 100:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigAOESpell_01, 2.5f);
			goto IL_5D8;
		case 101:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigSpellFace_01, 2.5f);
			goto IL_5D8;
		case 102:
		case 107:
			break;
		case 103:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_RogueSpell_01, 2.5f);
			goto IL_5D8;
		case 104:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossAttack_01, 2.5f);
			goto IL_5D8;
		case 105:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_01_01, 2.5f);
				goto IL_5D8;
			}
			goto IL_5D8;
		case 106:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_02_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_03_01, 2.5f);
				goto IL_5D8;
			}
			goto IL_5D8;
		case 108:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_04_01, 2.5f);
				goto IL_5D8;
			}
			goto IL_5D8;
		case 109:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_05_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_01_Hero_01, 2.5f);
				goto IL_5D8;
			}
			goto IL_5D8;
		case 110:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BigMech_01, 2.5f);
				goto IL_5D8;
			}
			goto IL_5D8;
		case 111:
			yield return base.PlayLineOnlyOnce(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Bomb_01, 2.5f);
			goto IL_5D8;
		case 112:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_02_Hero_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_03_Hero_01, 2.5f);
				goto IL_5D8;
			}
			goto IL_5D8;
		case 113:
			if (!this.m_Heroic)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayCriticalLine(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01, 2.5f);
				yield return new WaitForSeconds(2f);
				yield return base.PlayCriticalLine(enemyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01, 2.5f);
				yield return base.PlayCriticalLine(friendlyActor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_5D8;
			}
			goto IL_5D8;
		case 114:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(DRGA_Dungeon.RenoBrassRing, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_01, 2.5f);
			GameState.Get().SetBusy(false);
			goto IL_5D8;
		default:
			if (missionEvent == 199)
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(enemyActor, this.m_missionEventHeroPowerTrigger);
				goto IL_5D8;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_5D8:
		yield break;
	}

	// Token: 0x06004213 RID: 16915 RVA: 0x00162047 File Offset: 0x00160247
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 884427981U)
		{
			if (num <= 666962294U)
			{
				if (num != 632763696U)
				{
					if (num != 666962294U)
					{
						goto IL_456;
					}
					if (!(cardId == "ULD_238"))
					{
						goto IL_456;
					}
				}
				else
				{
					if (!(cardId == "LOE_079"))
					{
						goto IL_456;
					}
					yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_EliseMinion_01, 2.5f);
					goto IL_456;
				}
			}
			else if (num != 829391416U)
			{
				if (num != 867650362U)
				{
					if (num != 884427981U)
					{
						goto IL_456;
					}
					if (!(cardId == "LOE_076"))
					{
						goto IL_456;
					}
					yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_FinleyMinion_01, 2.5f);
					goto IL_456;
				}
				else
				{
					if (!(cardId == "LOE_077"))
					{
						goto IL_456;
					}
					yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BrannMinion_01, 2.5f);
					goto IL_456;
				}
			}
			else if (!(cardId == "ULD_500"))
			{
				goto IL_456;
			}
		}
		else
		{
			if (num <= 3005377403U)
			{
				if (num != 2553838125U)
				{
					if (num != 3005377403U)
					{
						goto IL_456;
					}
					if (!(cardId == "DRGA_001"))
					{
						goto IL_456;
					}
				}
				else
				{
					if (!(cardId == "EX1_182"))
					{
						goto IL_456;
					}
					yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Pilfer_01, 2.5f);
					goto IL_456;
				}
			}
			else if (num != 3116145498U)
			{
				if (num != 3723532367U)
				{
					if (num != 3875516676U)
					{
						goto IL_456;
					}
					if (!(cardId == "ULD_139"))
					{
						goto IL_456;
					}
					goto IL_340;
				}
				else
				{
					if (!(cardId == "ULD_156"))
					{
						goto IL_456;
					}
					goto IL_340;
				}
			}
			else if (!(cardId == "LOE_011"))
			{
				goto IL_456;
			}
			if (this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoHero_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("LOE_011"), null, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoMinion_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetEnemyActorByCardId("LOE_011"), null, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoMinion_01, 2.5f);
				goto IL_456;
			}
			goto IL_456;
		}
		IL_340:
		if (this.m_Heroic)
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_LoEMinion_01, 2.5f);
		}
		IL_456:
		yield break;
	}

	// Token: 0x06004214 RID: 16916 RVA: 0x0016205D File Offset: 0x0016025D
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
		yield break;
	}

	// Token: 0x04003164 RID: 12644
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigAOESpell_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigAOESpell_01.prefab:7cd908c62e238d945a0d3880736e5421");

	// Token: 0x04003165 RID: 12645
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigSpellFace_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_BigSpellFace_01.prefab:a7ca4be725f2d3d42abf56dc852494e1");

	// Token: 0x04003166 RID: 12646
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_01.prefab:d390bce5be0368c4986ce095f709e840");

	// Token: 0x04003167 RID: 12647
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_ALT_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_Death_ALT_01.prefab:79be1f3e7cab0ce40905ddc2a846b886");

	// Token: 0x04003168 RID: 12648
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01.prefab:d8f1f38e062c3d3419d4932421a3f434");

	// Token: 0x04003169 RID: 12649
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01.prefab:620fb641515f72842b1c56912e35b4f2");

	// Token: 0x0400316A RID: 12650
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01.prefab:43898a028b442c049a67a6fd8cdc71e7");

	// Token: 0x0400316B RID: 12651
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_RogueSpell_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_RogueSpell_01.prefab:5e71aa682bec7304597e3bd7b619d3fd");

	// Token: 0x0400316C RID: 12652
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossAttack_01.prefab:02fea32c6fda97c4c952cb76e6387ff7");

	// Token: 0x0400316D RID: 12653
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStart_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStart_01.prefab:90a7d1f7d7e64eb4bb2eb7247686af35");

	// Token: 0x0400316E RID: 12654
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_BossStartHeroic_01.prefab:659d57c7389ab52448ac1a90cb4b47a6");

	// Token: 0x0400316F RID: 12655
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_EmoteResponse_01.prefab:ae4098952b0ba95479e77d10b81b2bca");

	// Token: 0x04003170 RID: 12656
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_01_01.prefab:8fa4c3834bbd3a744adf06e2f1b8f417");

	// Token: 0x04003171 RID: 12657
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_02_01.prefab:5f395a434ba858c4884c523c110160c9");

	// Token: 0x04003172 RID: 12658
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_03_01.prefab:302410326e7b25b4baaee34aeab9ffc6");

	// Token: 0x04003173 RID: 12659
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_01_01.prefab:7915ed9b45f767b40ae2bf314a46c752");

	// Token: 0x04003174 RID: 12660
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_02_01.prefab:94eb8d42e80b8b545825241962239e58");

	// Token: 0x04003175 RID: 12661
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_03_01.prefab:76dbdae839c0a944abee890120f6e41d");

	// Token: 0x04003176 RID: 12662
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_04_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_04_01.prefab:3a6a3380385797a43940cb82d2abfacb");

	// Token: 0x04003177 RID: 12663
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_05_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Misc_05_01.prefab:a14c4cae34d6fe64dbdff511465b0791");

	// Token: 0x04003178 RID: 12664
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BigMech_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BigMech_01.prefab:4701ca333ef721d45ae99aaa860e1ec7");

	// Token: 0x04003179 RID: 12665
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Bomb_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Bomb_01.prefab:dff47c58319837d479094f9233a6ebd6");

	// Token: 0x0400317A RID: 12666
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BrannMinion_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_BrannMinion_01.prefab:8df4b068b4329fe47b680f6ba836e890");

	// Token: 0x0400317B RID: 12667
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_EliseMinion_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_EliseMinion_01.prefab:2e5400dca2ca8bc479969fd2e4ae0f0a");

	// Token: 0x0400317C RID: 12668
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_FinleyMinion_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_FinleyMinion_01.prefab:af48615fd4ff6f84b89d7bddebe2e870");

	// Token: 0x0400317D RID: 12669
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_LoEMinion_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_LoEMinion_01.prefab:cd6ad095dd4b3174bacd848b2740216c");

	// Token: 0x0400317E RID: 12670
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Pilfer_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_Pilfer_01.prefab:d1edc6c04cc215540b3433a3daf46988");

	// Token: 0x0400317F RID: 12671
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoHero_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoHero_01.prefab:c499f8336b39f904fa00c3bbf78d3210");

	// Token: 0x04003180 RID: 12672
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoMinion_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Player_RenoMinion_01.prefab:5ec0f084ce43d3848a5dbab3c3b47ef7");

	// Token: 0x04003181 RID: 12673
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_01_Hero_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_01_Hero_01.prefab:573d202a24637ff418d0d823c691cccf");

	// Token: 0x04003182 RID: 12674
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_02_Hero_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_02_Hero_01.prefab:46df1344f2e44ab41af76ddc48d56420");

	// Token: 0x04003183 RID: 12675
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_03_Hero_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_03_Hero_01.prefab:7d5e73641e1f2c24e8fd10d04dfc8559");

	// Token: 0x04003184 RID: 12676
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_04_Hero_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_Misc_04_Hero_01.prefab:8b8c1754de37b88449785c075e344fe6");

	// Token: 0x04003185 RID: 12677
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Evil_Fight_03_PlayerStart_01.prefab:d7872f57f8ef4414fa2c5169fbcf737c");

	// Token: 0x04003186 RID: 12678
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_02_01.prefab:c615497291aad6d4e9c78ad885519bdd");

	// Token: 0x04003187 RID: 12679
	private static readonly AssetReference VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01 = new AssetReference("VO_DRGA_BOSS_05h_Male_Goblin_Good_Fight_01_RenoCaptured_01_01.prefab:40008b7fc1a38b8448aa4f562b342a57");

	// Token: 0x04003188 RID: 12680
	private static readonly AssetReference VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01 = new AssetReference("VO_DRGA_BOSS_01h_Male_Human_Good_Fight_01_RenoCaptured_03_01.prefab:661d71cae3c252d4cb2333e23c8016eb");

	// Token: 0x04003189 RID: 12681
	private List<string> m_missionEventHeroPowerTrigger = new List<string>
	{
		DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_01_01,
		DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_02_01,
		DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Boss_HeroPowerTrigger_03_01
	};

	// Token: 0x0400318A RID: 12682
	private List<string> m_VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_IdleLines = new List<string>
	{
		DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_01_01,
		DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_02_01,
		DRGA_Evil_Fight_03.VO_DRGA_BOSS_01h_Male_Human_Evil_Fight_03_Idle_03_01
	};

	// Token: 0x0400318B RID: 12683
	private HashSet<string> m_playedLines = new HashSet<string>();
}
