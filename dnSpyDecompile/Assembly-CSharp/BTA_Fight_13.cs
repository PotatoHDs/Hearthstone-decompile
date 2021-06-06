using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F7 RID: 1271
public class BTA_Fight_13 : BTA_Dungeon
{
	// Token: 0x06004457 RID: 17495 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x06004458 RID: 17496 RVA: 0x001723F8 File Offset: 0x001705F8
	public BTA_Fight_13()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_13.s_booleanOptions);
	}

	// Token: 0x06004459 RID: 17497 RVA: 0x00172560 File Offset: 0x00170760
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_13.VO_BTA_01_Female_NightElf_Mission_Fight_13_PlayerStart_01,
			BTA_Fight_13.VO_BTA_08_Male_Orc_Mission_Fight_13_Misc_02,
			BTA_Fight_13.VO_BTA_08_Male_Orc_Mission_Fight_13_VictoryA_01,
			BTA_Fight_13.VO_BTA_10_Female_Naga_Mission_Fight_13_Misc_01,
			BTA_Fight_13.VO_BTA_10_Female_Naga_Mission_Fight_13_VictoryB_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_Attack_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedBasilisk_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedFungalGiant_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossDeath_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_02,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Emote_Response_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_FelSummoner_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_GlaiveboundAdeptTrigger_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_WrathspikeBrute_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Boss_EndlessLegion_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Hero_ImmolationAura_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleA_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleB_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleA_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleC_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_03,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01,
			BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600445A RID: 17498 RVA: 0x00172764 File Offset: 0x00170964
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_13h_IdleLines;
	}

	// Token: 0x0600445B RID: 17499 RVA: 0x0017276C File Offset: 0x0017096C
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_Lines;
	}

	// Token: 0x0600445C RID: 17500 RVA: 0x00172774 File Offset: 0x00170974
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossDeath_01;
	}

	// Token: 0x0600445D RID: 17501 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600445E RID: 17502 RVA: 0x0017278C File Offset: 0x0017098C
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_13.VO_BTA_01_Female_NightElf_Mission_Fight_13_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_02, this.m_OgreMechSpeechBubbleDirection, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600445F RID: 17503 RVA: 0x0017279C File Offset: 0x0017099C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004460 RID: 17504 RVA: 0x00172824 File Offset: 0x00170A24
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 500)
		{
			if (missionEvent == 101)
			{
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_08"), BTA_Dungeon.KarnukBrassRingDemonHunter, BTA_Fight_13.VO_BTA_08_Male_Orc_Mission_Fight_13_Misc_02, 2.5f);
				goto IL_369;
			}
			if (missionEvent == 500)
			{
				base.PlaySound(BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_Attack_01, 1f, true, false);
				goto IL_369;
			}
		}
		else
		{
			if (missionEvent == 501)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_08"), BTA_Dungeon.KarnukBrassRingDemonHunter, BTA_Fight_13.VO_BTA_08_Male_Orc_Mission_Fight_13_VictoryA_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_10"), BTA_Dungeon.ShaljaBrassRingDemonHunter, BTA_Fight_13.VO_BTA_10_Female_Naga_Mission_Fight_13_VictoryB_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_369;
			}
			if (missionEvent != 507)
			{
				if (missionEvent == 508)
				{
					int num = UnityEngine.Random.Range(1, 3);
					if (num == 1)
					{
						yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02, 2.5f);
						yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02, this.m_OgreMechSpeechBubbleDirection, 2.5f);
						goto IL_369;
					}
					if (num == 2)
					{
						yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_01, 2.5f);
						yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01, this.m_OgreMechSpeechBubbleDirection, 2.5f);
						goto IL_369;
					}
					goto IL_369;
				}
			}
			else
			{
				int num2 = UnityEngine.Random.Range(1, 3);
				if (num2 == 1)
				{
					yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02, 2.5f);
					goto IL_369;
				}
				if (num2 == 2)
				{
					yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_03, 2.5f);
					goto IL_369;
				}
				goto IL_369;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_369:
		yield break;
	}

	// Token: 0x06004461 RID: 17505 RVA: 0x0017283A File Offset: 0x00170A3A
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BT_495"))
		{
			if (!(cardId == "BT_509"))
			{
				if (!(cardId == "BT_510"))
				{
					if (!(cardId == "BT_514"))
					{
						if (!(cardId == "BTA_10"))
						{
							if (cardId == "BTA_08")
							{
								yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_08"), BTA_Dungeon.KarnukBrassRingDemonHunter, BTA_Fight_13.VO_BTA_08_Male_Orc_Mission_Fight_13_Misc_02, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_10"), BTA_Dungeon.ShaljaBrassRingDemonHunter, BTA_Fight_13.VO_BTA_10_Female_Naga_Mission_Fight_13_Misc_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineAlways(actor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Hero_ImmolationAura_01, this.m_OgreMechSpeechBubbleDirection, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_WrathspikeBrute_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_FelSummoner_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_GlaiveboundAdeptTrigger_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004462 RID: 17506 RVA: 0x00172850 File Offset: 0x00170A50
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BTA_14"))
		{
			if (!(cardId == "BTA_15"))
			{
				if (cardId == "BTA_16")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedFungalGiant_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Boss_EndlessLegion_01, this.m_OgreMechSpeechBubbleDirection, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedBasilisk_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004463 RID: 17507 RVA: 0x00172866 File Offset: 0x00170A66
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004464 RID: 17508 RVA: 0x00112BA9 File Offset: 0x00110DA9
	public override float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.5f;
	}

	// Token: 0x06004465 RID: 17509 RVA: 0x0017287C File Offset: 0x00170A7C
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
			GameEntity.Coroutines.StartCoroutine(this.PlayPairedBossIdleLines());
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

	// Token: 0x06004466 RID: 17510 RVA: 0x00172938 File Offset: 0x00170B38
	protected IEnumerator PlayPairedBossIdleLines()
	{
		int num = UnityEngine.Random.Range(1, 3);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (num == 1)
		{
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleA_01, this.m_OgreMechSpeechBubbleDirection, 2.5f);
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleB_01, this.m_OgreMechSpeechBubbleDirection, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleC_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040036E0 RID: 14048
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_13.InitBooleanOptions();

	// Token: 0x040036E1 RID: 14049
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_13_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_13_PlayerStart_01.prefab:bae570e3f5e92504dafd2ec014f95469");

	// Token: 0x040036E2 RID: 14050
	private static readonly AssetReference VO_BTA_08_Male_Orc_Mission_Fight_13_Misc_02 = new AssetReference("VO_BTA_08_Male_Orc_Mission_Fight_13_Misc_02.prefab:db1012ddaade0eb4c80841a7979488a9");

	// Token: 0x040036E3 RID: 14051
	private static readonly AssetReference VO_BTA_08_Male_Orc_Mission_Fight_13_VictoryA_01 = new AssetReference("VO_BTA_08_Male_Orc_Mission_Fight_13_VictoryA_01.prefab:3f7c607714785d14a90a5d6f9e9942df");

	// Token: 0x040036E4 RID: 14052
	private static readonly AssetReference VO_BTA_10_Female_Naga_Mission_Fight_13_Misc_01 = new AssetReference("VO_BTA_10_Female_Naga_Mission_Fight_13_Misc_01.prefab:04ab7d9bb0134fb43b507218e50093c9");

	// Token: 0x040036E5 RID: 14053
	private static readonly AssetReference VO_BTA_10_Female_Naga_Mission_Fight_13_VictoryB_01 = new AssetReference("VO_BTA_10_Female_Naga_Mission_Fight_13_VictoryB_01.prefab:832f26ddd691333499c84771674e9729");

	// Token: 0x040036E6 RID: 14054
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_Attack_01.prefab:1ea54479130e6e74b8486ceacca9e138");

	// Token: 0x040036E7 RID: 14055
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedBasilisk_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedBasilisk_01.prefab:45d04acd3518701488e6ecc12f075a01");

	// Token: 0x040036E8 RID: 14056
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedFungalGiant_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Boss_RustedFungalGiant_01.prefab:a4884c283694ef9439867c3d95f8fe0a");

	// Token: 0x040036E9 RID: 14057
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossDeath_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossDeath_01.prefab:5d7d05fa5f5e7f840a75b4997f715254");

	// Token: 0x040036EA RID: 14058
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_01.prefab:393f6dd3c7f07704b8c09513b8fede3b");

	// Token: 0x040036EB RID: 14059
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_02 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_02.prefab:6365b6327d264b940a47b433328f16d7");

	// Token: 0x040036EC RID: 14060
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Emote_Response_01.prefab:9b94ae21a9aba4446b87d1bf7997e806");

	// Token: 0x040036ED RID: 14061
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_FelSummoner_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_FelSummoner_01.prefab:300df50b2d1aa594e911513834cfd40e");

	// Token: 0x040036EE RID: 14062
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_GlaiveboundAdeptTrigger_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_GlaiveboundAdeptTrigger_01.prefab:dd4999b21305f404c994e01585b277bd");

	// Token: 0x040036EF RID: 14063
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_WrathspikeBrute_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_Hero_WrathspikeBrute_01.prefab:53c16de44f503914995c745164c42360");

	// Token: 0x040036F0 RID: 14064
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_01.prefab:f6bcaa55d4a8ddd46b51ed06755dacdc");

	// Token: 0x040036F1 RID: 14065
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02.prefab:06affe6dba6315a4994e4cffcbfbedf9");

	// Token: 0x040036F2 RID: 14066
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_03 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_03.prefab:3f006288048bea248b9116041f7600ff");

	// Token: 0x040036F3 RID: 14067
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleA_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleA_01.prefab:815d3d852c6f7484896ca5aa7ce0408a");

	// Token: 0x040036F4 RID: 14068
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleC_01 = new AssetReference("VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleC_01.prefab:01620e4baee2a4c4d81940a48586365b");

	// Token: 0x040036F5 RID: 14069
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Boss_EndlessLegion_01 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Boss_EndlessLegion_01.prefab:0903eb35b0e73794aad8171c1eb0c72b");

	// Token: 0x040036F6 RID: 14070
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Hero_ImmolationAura_01 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_Hero_ImmolationAura_01.prefab:bdc5f5b29b1db2d408bdd8ce273ba905");

	// Token: 0x040036F7 RID: 14071
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01.prefab:766c798064cdb204f8591f2698884b8e");

	// Token: 0x040036F8 RID: 14072
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02.prefab:f0f69149f5582114e818dfc7cb485f6f");

	// Token: 0x040036F9 RID: 14073
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleA_01 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleA_01.prefab:5a4a77be23df1e647b603812a049dbe8");

	// Token: 0x040036FA RID: 14074
	private static readonly AssetReference VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleB_01 = new AssetReference("VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleB_01.prefab:42ddb238f95e9c04e9c9615e3133008c");

	// Token: 0x040036FB RID: 14075
	private Notification.SpeechBubbleDirection m_OgreMechSpeechBubbleDirection = Notification.SpeechBubbleDirection.TopLeft;

	// Token: 0x040036FC RID: 14076
	private List<string> m_VO_BTA_BOSS_13h_IdleLines = new List<string>
	{
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleA_01,
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_IdleB_01,
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleA_01,
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_IdleC_01
	};

	// Token: 0x040036FD RID: 14077
	private List<string> m_missionEventTrigger501_Lines = new List<string>
	{
		BTA_Fight_13.VO_BTA_08_Male_Orc_Mission_Fight_13_VictoryA_01,
		BTA_Fight_13.VO_BTA_10_Female_Naga_Mission_Fight_13_VictoryB_01
	};

	// Token: 0x040036FE RID: 14078
	private List<string> m_VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_Lines = new List<string>
	{
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_01,
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_BossStart_02
	};

	// Token: 0x040036FF RID: 14079
	private List<string> m_VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_Lines = new List<string>
	{
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_01,
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_02,
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_Ogre_Mission_Fight_13_HeroPower_03
	};

	// Token: 0x04003700 RID: 14080
	private List<string> m_VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_Lines = new List<string>
	{
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01,
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02
	};

	// Token: 0x04003701 RID: 14081
	private List<string> m_VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_Lines_Copy = new List<string>
	{
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_01,
		BTA_Fight_13.VO_BTA_BOSS_13h_Male_OgreMech_Mission_Fight_13_HeroPower_02
	};

	// Token: 0x04003702 RID: 14082
	private HashSet<string> m_playedLines = new HashSet<string>();
}
