using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004DD RID: 1245
public class DRGA_Good_Fight_03 : DRGA_Dungeon
{
	// Token: 0x060042B5 RID: 17077 RVA: 0x0016768C File Offset: 0x0016588C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Backstory_01b_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_01_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_02_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_01_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_02_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_03_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_01_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_02_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_PlayerStart_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Backstory_01a_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_Death_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_DeathAlt_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_01_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_02_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_03_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossAttack_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStart_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStartHeroic_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_EmoteResponse_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_01_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_02_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_03_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_BadLuckAlbatros_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_01_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_02_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DreadRaven_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerMadameLazul_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerVessina_01,
			DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_WingedGuardian_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060042B6 RID: 17078 RVA: 0x001678C0 File Offset: 0x00165AC0
	public override List<string> GetIdleLines()
	{
		return this.m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_IdleLines;
	}

	// Token: 0x060042B7 RID: 17079 RVA: 0x001678C8 File Offset: 0x00165AC8
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPowerLines;
	}

	// Token: 0x060042B8 RID: 17080 RVA: 0x001678D0 File Offset: 0x00165AD0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		if ((double)UnityEngine.Random.Range(0f, 1f) < 0.5)
		{
			this.m_deathLine = DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_Death_01;
			return;
		}
		this.m_deathLine = DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_DeathAlt_01;
	}

	// Token: 0x060042B9 RID: 17081 RVA: 0x00167920 File Offset: 0x00165B20
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			if (!this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStart_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			if (this.m_Heroic)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStartHeroic_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
		else
		{
			if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				return;
			}
			return;
		}
	}

	// Token: 0x060042BA RID: 17082 RVA: 0x001679F1 File Offset: 0x00165BF1
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(actor, DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Backstory_01a_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Backstory_01b_01, 2.5f);
			}
			break;
		case 102:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_01_01, 2.5f);
			}
			break;
		case 103:
			if (!this.m_Heroic)
			{
				yield return base.PlayLineAlways(friendlyActor, DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_02_01, 2.5f);
			}
			break;
		case 104:
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossAttack_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x060042BB RID: 17083 RVA: 0x00167A07 File Offset: 0x00165C07
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
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "DAL_256"))
		{
			if (cardId == "DRGA_BOSS_02t")
			{
				if (!this.m_Heroic)
				{
					yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdeaLines);
				}
			}
		}
		else if (!this.m_Heroic)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvilLines);
		}
		yield break;
	}

	// Token: 0x060042BC RID: 17084 RVA: 0x00167A1D File Offset: 0x00165C1D
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
		if (!(cardId == "DRG_071"))
		{
			if (!(cardId == "DRGA_BOSS_13t"))
			{
				if (!(cardId == "DRG_088"))
				{
					if (!(cardId == "DAL_729"))
					{
						if (!(cardId == "ULD_173"))
						{
							if (cardId == "YOD_003")
							{
								yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_WingedGuardian_01, 2.5f);
							}
						}
						else
						{
							yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerVessina_01, 2.5f);
						}
					}
					else
					{
						yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerMadameLazul_01, 2.5f);
					}
				}
				else
				{
					yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DreadRaven_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, this.m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWindsLines);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(actor, DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_BadLuckAlbatros_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003358 RID: 13144
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Backstory_01b_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Backstory_01b_01.prefab:177d1c2a2b5a99d4b845dec19ef50111");

	// Token: 0x04003359 RID: 13145
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_01_01.prefab:05ac18662fc0f5140884658ce8ee3731");

	// Token: 0x0400335A RID: 13146
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_02_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_02_01.prefab:5879f46a73bc6da439aa6a0000877afa");

	// Token: 0x0400335B RID: 13147
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_01_01.prefab:9f50fd1fba7d01940ab9692542a7d0d7");

	// Token: 0x0400335C RID: 13148
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_02_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_02_01.prefab:abe7dd8496d5f144c8f33b3ccbc2994f");

	// Token: 0x0400335D RID: 13149
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_03_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_03_01.prefab:9372aa947b8fd7e48bdebc145885f44e");

	// Token: 0x0400335E RID: 13150
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_01_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_01_01.prefab:4df11f96aa6e13e46a8c45f54d01a092");

	// Token: 0x0400335F RID: 13151
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_02_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Misc_02_01.prefab:71a3e03bd89f663488da38d108913e94");

	// Token: 0x04003360 RID: 13152
	private static readonly AssetReference VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_PlayerStart_01 = new AssetReference("VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_PlayerStart_01.prefab:7e1c4a6c0605e394493541fdb3fecbc4");

	// Token: 0x04003361 RID: 13153
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Backstory_01a_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Backstory_01a_01.prefab:c650fcbd35a93b346b890d6235667040");

	// Token: 0x04003362 RID: 13154
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_Death_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_Death_01.prefab:d558ce9226fb04f4c91cf5f7da64ff93");

	// Token: 0x04003363 RID: 13155
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_DeathAlt_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_DeathAlt_01.prefab:5950a1e738254cf4e92aa48776edc1a4");

	// Token: 0x04003364 RID: 13156
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_01_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_01_01.prefab:f38a7f2fcfae44441b695110b56cb795");

	// Token: 0x04003365 RID: 13157
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_02_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_02_01.prefab:c95a2decaf712fb4b8c3a2f3bcdf2bbb");

	// Token: 0x04003366 RID: 13158
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_03_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_03_01.prefab:85ceb28aa72931d4e8cc50748bb4c6e6");

	// Token: 0x04003367 RID: 13159
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossAttack_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossAttack_01.prefab:bb299237e7eb39b41a09694f25a415f5");

	// Token: 0x04003368 RID: 13160
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStart_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStart_01.prefab:72d1a63fa72c57d40b0559421371dbcf");

	// Token: 0x04003369 RID: 13161
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStartHeroic_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_BossStartHeroic_01.prefab:8518d2aaea1de974eb4c7cd3fa705a05");

	// Token: 0x0400336A RID: 13162
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_EmoteResponse_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_EmoteResponse_01.prefab:fc8388762b51ddf409cbaae73aeddaa2");

	// Token: 0x0400336B RID: 13163
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_01_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_01_01.prefab:86be531afb5159847ba7073fcc60d367");

	// Token: 0x0400336C RID: 13164
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_02_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_02_01.prefab:afd16d20a4622194aafcdfef8c291163");

	// Token: 0x0400336D RID: 13165
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_03_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_03_01.prefab:db7c9b07e8037304a99fa339632c8bbf");

	// Token: 0x0400336E RID: 13166
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_BadLuckAlbatros_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_BadLuckAlbatros_01.prefab:836d4c0f59fbc8640a77a160bab1e046");

	// Token: 0x0400336F RID: 13167
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_01_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_01_01.prefab:7d824aa20de2e274489b31f4dd9b2ca5");

	// Token: 0x04003370 RID: 13168
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_02_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_02_01.prefab:376caa601a93c044a8f08848a60a8585");

	// Token: 0x04003371 RID: 13169
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DreadRaven_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DreadRaven_01.prefab:e140dca4135f0fd4fb4b046225f0d209");

	// Token: 0x04003372 RID: 13170
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerMadameLazul_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerMadameLazul_01.prefab:e645f863c7c4a334190c0a7a8a046020");

	// Token: 0x04003373 RID: 13171
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerVessina_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_PlayerVessina_01.prefab:cf47eb85e92cead4e8190a12883fc758");

	// Token: 0x04003374 RID: 13172
	private static readonly AssetReference VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_WingedGuardian_01 = new AssetReference("VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_WingedGuardian_01.prefab:995645ebb500b5e4c956be2551a06b5a");

	// Token: 0x04003375 RID: 13173
	private List<string> m_VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvilLines = new List<string>
	{
		DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_01_01,
		DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_StandAgainstEvil_02_01
	};

	// Token: 0x04003376 RID: 13174
	private List<string> m_VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdeaLines = new List<string>
	{
		DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_01_01,
		DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_02_01,
		DRGA_Good_Fight_03.VO_DRGA_BOSS_02h_Female_NightElf_Good_Fight_03_Elise_ThePerfectIdea_03_01
	};

	// Token: 0x04003377 RID: 13175
	private List<string> m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPowerLines = new List<string>
	{
		DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_01_01,
		DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_02_01,
		DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Boss_HeroPower_03_01
	};

	// Token: 0x04003378 RID: 13176
	private List<string> m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_IdleLines = new List<string>
	{
		DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_01_01,
		DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_02_01,
		DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Idle_03_01
	};

	// Token: 0x04003379 RID: 13177
	private List<string> m_VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWindsLines = new List<string>
	{
		DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_01_01,
		DRGA_Good_Fight_03.VO_DRGA_BOSS_13h_Female_Arakkoa_Good_Fight_03_Kriziki_DiamondWinds_02_01
	};

	// Token: 0x0400337A RID: 13178
	private HashSet<string> m_playedLines = new HashSet<string>();
}
