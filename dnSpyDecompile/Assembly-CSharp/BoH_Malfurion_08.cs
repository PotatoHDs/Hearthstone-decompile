using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000533 RID: 1331
public class BoH_Malfurion_08 : BoH_Malfurion_Dungeon
{
	// Token: 0x06004895 RID: 18581 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004896 RID: 18582 RVA: 0x00184BF4 File Offset: 0x00182DF4
	public BoH_Malfurion_08()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Malfurion_08.s_booleanOptions);
	}

	// Token: 0x06004897 RID: 18583 RVA: 0x00184D2C File Offset: 0x00182F2C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Malfurion_08.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeB_02,
			BoH_Malfurion_08.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeC_02,
			BoH_Malfurion_08.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Intro_02,
			BoH_Malfurion_08.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_03,
			BoH_Malfurion_08.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_04,
			BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8_Victory_01,
			BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8EmoteResponse_01,
			BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_02,
			BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_04,
			BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeD_01,
			BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeE_01,
			BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_04,
			BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_05,
			BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Loss_01,
			BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8EmoteResponse_01,
			BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeA_01,
			BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeB_01,
			BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeC_04,
			BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_01,
			BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_02,
			BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_03,
			BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Intro_01,
			BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Loss_01,
			BoH_Malfurion_08.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8ExchangeD_02,
			BoH_Malfurion_08.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004898 RID: 18584 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004899 RID: 18585 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600489A RID: 18586 RVA: 0x00184F20 File Offset: 0x00183120
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Malfurion_08.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600489B RID: 18587 RVA: 0x00184F2F File Offset: 0x0018312F
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x0600489C RID: 18588 RVA: 0x00184F37 File Offset: 0x00183137
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DRGEVILBoss;
		base.OnCreateGame();
	}

	// Token: 0x0600489D RID: 18589 RVA: 0x00184F4A File Offset: 0x0018314A
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 504)
		{
			switch (missionEvent)
			{
			case 101:
				yield return base.PlayLineAlways(BoH_Malfurion_08.SaurfangBrassRing, BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_02, 2.5f);
				goto IL_4A4;
			case 102:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_08.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeC_02, 2.5f);
				yield return base.PlayLineAlways(BoH_Malfurion_08.SaurfangBrassRing, BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_04, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeC_04, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4A4;
			case 103:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeD_01, 2.5f);
				yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_Tyrande"), BoH_Malfurion_08.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8ExchangeD_02, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4A4;
			case 104:
				break;
			case 105:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4A4;
			default:
				if (missionEvent == 504)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8_Victory_01, 2.5f);
					yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_Tyrande"), BoH_Malfurion_08.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8Victory_02, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_08.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_03, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_08.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_04, 2.5f);
					GameState.Get().SetBusy(false);
					goto IL_4A4;
				}
				break;
			}
		}
		else
		{
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_4A4;
			}
			if (missionEvent == 515)
			{
				if (GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId() == "Story_08_Saurfang_008h2")
				{
					yield return base.MissionPlayVO(enemyActor, BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8EmoteResponse_01);
					goto IL_4A4;
				}
				yield return base.MissionPlayVO(enemyActor, BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8EmoteResponse_01);
				goto IL_4A4;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_4A4:
		yield break;
	}

	// Token: 0x0600489E RID: 18590 RVA: 0x00184F60 File Offset: 0x00183160
	public override IEnumerator OnPlayThinkEmoteWithTiming()
	{
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			yield break;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			yield break;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		float thinkEmoteBossIdleChancePercentage = this.GetThinkEmoteBossIdleChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkEmoteBossIdleChancePercentage > num || (!this.m_Mission_FriendlyPlayIdleLines && this.m_Mission_EnemyPlayIdleLines))
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			if (cardId == "Story_08_Saurfang_008h2")
			{
				string line = base.PopRandomLine(this.m_BossSaurfangIdleLinesCopy, true);
				if (this.m_BossSaurfangIdleLinesCopy.Count == 0)
				{
					this.m_BossSaurfangIdleLinesCopy = new List<string>(this.m_BossSaurfangIdleLines);
				}
				yield return base.MissionPlayVO(actor, line);
			}
			else if (cardId == "Story_08_Sylvanas_008hb")
			{
				string line2 = base.PopRandomLine(this.m_BossIdleLinesCopy, true);
				if (this.m_BossIdleLinesCopy.Count == 0)
				{
					this.m_BossIdleLinesCopy = new List<string>(this.m_BossIdleLines);
				}
				yield return base.MissionPlayVO(actor, line2);
			}
		}
		else
		{
			if (!this.m_Mission_FriendlyPlayIdleLines)
			{
				yield break;
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
			GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType).GetActiveAudioSource();
		}
		yield break;
	}

	// Token: 0x0600489F RID: 18591 RVA: 0x00184F6F File Offset: 0x0018316F
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x060048A0 RID: 18592 RVA: 0x00184F85 File Offset: 0x00183185
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
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x060048A1 RID: 18593 RVA: 0x00184F9B File Offset: 0x0018319B
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn == 3)
			{
				yield return base.PlayLineAlways(actor, BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_08.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeB_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003C70 RID: 15472
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Malfurion_08.InitBooleanOptions();

	// Token: 0x04003C71 RID: 15473
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeB_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeB_02.prefab:cef8fed293cffee4199974c22bf7f691");

	// Token: 0x04003C72 RID: 15474
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeC_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8ExchangeC_02.prefab:eb84b2a6db7752a4284a1642db4dc689");

	// Token: 0x04003C73 RID: 15475
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Intro_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Intro_02.prefab:5b82789dba0e9a742b50283d5de2aa15");

	// Token: 0x04003C74 RID: 15476
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_03.prefab:ca565754cb17bc8429ba0a970c5ba233");

	// Token: 0x04003C75 RID: 15477
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_04 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_04.prefab:19896f6b77a8dd945b570cd62534a4b8");

	// Token: 0x04003C76 RID: 15478
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_05 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_05.prefab:3aca5495e75012848b94079613816ca7");

	// Token: 0x04003C77 RID: 15479
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_06 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission8Victory_06.prefab:92435de6df5cab34481db3ba503520a9");

	// Token: 0x04003C78 RID: 15480
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8_Victory_01 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8_Victory_01.prefab:77fcd9344734e364493208a2aebd9e63");

	// Token: 0x04003C79 RID: 15481
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8EmoteResponse_01.prefab:9a8ca9200cd75064286c9fe4fb2a5c8f");

	// Token: 0x04003C7A RID: 15482
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_02 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_02.prefab:eaa8737fba71fa84dba3befade1ccc5b");

	// Token: 0x04003C7B RID: 15483
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_04 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeC_04.prefab:e5ec8a5ebad31554985870fab61ea596");

	// Token: 0x04003C7C RID: 15484
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeD_01.prefab:30ed87612411a9648b26b860d2312697");

	// Token: 0x04003C7D RID: 15485
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeE_01.prefab:094bf9c9302958c4980fb12795e35080");

	// Token: 0x04003C7E RID: 15486
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_04 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_04.prefab:b3bb664c9d96a6a4b822998b93104e1d");

	// Token: 0x04003C7F RID: 15487
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_05 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_05.prefab:daf576e796fe2ed4ca58e75cb5fce4c0");

	// Token: 0x04003C80 RID: 15488
	private static readonly AssetReference VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Loss_01.prefab:e7de37a0ba78ca2439b1c9549db75793");

	// Token: 0x04003C81 RID: 15489
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8EmoteResponse_01.prefab:39fffad8b3b710a4f8d67ca1b65aec01");

	// Token: 0x04003C82 RID: 15490
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeA_01.prefab:98ad199d5fcadd446b58190ffe9e6a7a");

	// Token: 0x04003C83 RID: 15491
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeB_01.prefab:f590e241f8b5b5d4f80b739b68395a94");

	// Token: 0x04003C84 RID: 15492
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeC_04 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8ExchangeC_04.prefab:e4b4160c0edd9c14e8be5aaa53cbc827");

	// Token: 0x04003C85 RID: 15493
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_01.prefab:d3772c8486312064da4b81f7eb893642");

	// Token: 0x04003C86 RID: 15494
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_02.prefab:fca33c26f99944e47ad463a737280368");

	// Token: 0x04003C87 RID: 15495
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_03.prefab:b0b842d84d15eba4ea085c74d63ab24d");

	// Token: 0x04003C88 RID: 15496
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Intro_01.prefab:b3f4ceb3cbf6e0541abd1aef57905d6f");

	// Token: 0x04003C89 RID: 15497
	private static readonly AssetReference VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Loss_01.prefab:b730811db48755f469e042bb01b55b89");

	// Token: 0x04003C8A RID: 15498
	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8ExchangeD_02 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8ExchangeD_02.prefab:6997b8e2512d4f24baa2fbcb8d697e0e");

	// Token: 0x04003C8B RID: 15499
	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8Victory_02 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission8Victory_02.prefab:bd3d776874b3ee247bfcb2b9bdee14dc");

	// Token: 0x04003C8C RID: 15500
	private static readonly AssetReference SaurfangBrassRing = new AssetReference("Saurfang_BrassRing_Quote.prefab:727d1e09f5a40f649afa7ed2f3e70564");

	// Token: 0x04003C8D RID: 15501
	private List<string> m_EmoteResponseLines = new List<string>
	{
		BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8EmoteResponse_01
	};

	// Token: 0x04003C8E RID: 15502
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_01,
		BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_02,
		BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_03
	};

	// Token: 0x04003C8F RID: 15503
	private new List<string> m_BossIdleLinesCopy = new List<string>
	{
		BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_01,
		BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_02,
		BoH_Malfurion_08.VO_Story_Hero_Sylvanas_Female_Undead_Story_Malfurion_Mission8Idle_03
	};

	// Token: 0x04003C90 RID: 15504
	private List<string> m_BossSaurfangIdleLines = new List<string>
	{
		BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeE_01,
		BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_04,
		BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_05
	};

	// Token: 0x04003C91 RID: 15505
	private List<string> m_BossSaurfangIdleLinesCopy = new List<string>
	{
		BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8ExchangeE_01,
		BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_04,
		BoH_Malfurion_08.VO_Story_Hero_Saurfang_Male_Orc_Story_Malfurion_Mission8Idle_05
	};

	// Token: 0x04003C92 RID: 15506
	private HashSet<string> m_playedLines = new HashSet<string>();
}
