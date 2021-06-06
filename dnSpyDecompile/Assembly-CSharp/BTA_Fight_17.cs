using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004FB RID: 1275
public class BTA_Fight_17 : BTA_Dungeon
{
	// Token: 0x0600449C RID: 17564 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600449D RID: 17565 RVA: 0x00173AE2 File Offset: 0x00171CE2
	public BTA_Fight_17()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_17.s_booleanOptions);
	}

	// Token: 0x0600449E RID: 17566 RVA: 0x00173B08 File Offset: 0x00171D08
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_EpilogueA_01,
			BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_PlayerStart_01,
			BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse0B_01,
			BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse2_01,
			BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse4_01,
			BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse6_01,
			BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8A_01,
			BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8B_01,
			BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8E_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossDeath_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossStart_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Emote_Response_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_EpilogueB_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse0A_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse1_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse3_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse5_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse7_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8C_01,
			BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8D_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600449F RID: 17567 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060044A0 RID: 17568 RVA: 0x0016D56A File Offset: 0x0016B76A
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x060044A1 RID: 17569 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060044A2 RID: 17570 RVA: 0x00173CAC File Offset: 0x00171EAC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossStart_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_PlayerStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060044A3 RID: 17571 RVA: 0x00173CBC File Offset: 0x00171EBC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060044A4 RID: 17572 RVA: 0x00173D44 File Offset: 0x00171F44
	protected IEnumerator PlayFollowupQuote()
	{
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8B_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060044A5 RID: 17573 RVA: 0x00173D53 File Offset: 0x00171F53
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			this.PlayFollowupQuote();
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8A_01, 2.5f);
			GameState.Get().SetBusy(false);
			yield return new WaitForSeconds(0.1f);
			yield return this.PlayFollowupQuote();
			break;
		case 102:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8C_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8D_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 103:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8E_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 104:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8B_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		default:
			if (missionEvent != 501)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_EpilogueA_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_EpilogueB_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
			break;
		}
		yield break;
	}

	// Token: 0x060044A6 RID: 17574 RVA: 0x00173D69 File Offset: 0x00171F69
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

	// Token: 0x060044A7 RID: 17575 RVA: 0x00173D7F File Offset: 0x00171F7F
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

	// Token: 0x060044A8 RID: 17576 RVA: 0x00173D95 File Offset: 0x00171F95
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 1:
			yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse0A_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse0B_01, 2.5f);
			break;
		case 2:
		case 4:
		case 6:
		case 8:
		case 10:
		case 12:
		case 14:
			break;
		case 3:
			yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse1_01, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlways(friendlyActor, BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse2_01, 2.5f);
			break;
		case 7:
			yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse3_01, 2.5f);
			break;
		case 9:
			yield return base.PlayLineAlways(friendlyActor, BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse4_01, 2.5f);
			break;
		case 11:
			yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse5_01, 2.5f);
			break;
		case 13:
			yield return base.PlayLineAlways(friendlyActor, BTA_Fight_17.VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse6_01, 2.5f);
			break;
		case 15:
			yield return base.PlayLineAlways(actor, BTA_Fight_17.VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse7_01, 2.5f);
			break;
		default:
			if (turn == 18)
			{
				this.m_DisableIdle = true;
			}
			break;
		}
		yield break;
	}

	// Token: 0x060044A9 RID: 17577 RVA: 0x00173DAB File Offset: 0x00171FAB
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHPrologueBoss);
	}

	// Token: 0x04003759 RID: 14169
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_17.InitBooleanOptions();

	// Token: 0x0400375A RID: 14170
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_EpilogueA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_EpilogueA_01.prefab:b981744b125d02e41a2bacb71120550b");

	// Token: 0x0400375B RID: 14171
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_PlayerStart_01.prefab:dfbcd7e9af0181d4db070eaca0e0c3f0");

	// Token: 0x0400375C RID: 14172
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse0B_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse0B_01.prefab:f9182f0644b9773459269f5707b1242f");

	// Token: 0x0400375D RID: 14173
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse2_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse2_01.prefab:05fc5131c55cd7542b2991622a6f0e51");

	// Token: 0x0400375E RID: 14174
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse4_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse4_01.prefab:23b40f5f91306d6458a21956a1e3c8e5");

	// Token: 0x0400375F RID: 14175
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse6_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse6_01.prefab:af8cdb376b56c5c4c8b8f3732df6767e");

	// Token: 0x04003760 RID: 14176
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8A_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8A_01.prefab:6afb84689f8dc814e90db225dd10a511");

	// Token: 0x04003761 RID: 14177
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8B_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8B_01.prefab:1a74dd1f109b4b5498f4b08aa11e31d9");

	// Token: 0x04003762 RID: 14178
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8E_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_17_Verse8E_01.prefab:cebb08a64d2c1a24bbef8d8cefd223bf");

	// Token: 0x04003763 RID: 14179
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossDeath_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossDeath_01.prefab:c1f4a6474b7647c4e95b21a42b010610");

	// Token: 0x04003764 RID: 14180
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossStart_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_BossStart_01.prefab:5fcbef2241013ae458698f7632b124df");

	// Token: 0x04003765 RID: 14181
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Emote_Response_01.prefab:4af05b8c4a932dd4eb607d57c1710684");

	// Token: 0x04003766 RID: 14182
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_EpilogueB_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_EpilogueB_01.prefab:1102a41180e6a194f9a08d51f4595bc4");

	// Token: 0x04003767 RID: 14183
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse0A_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse0A_01.prefab:62ef80ef3008bca49a031afe15af2028");

	// Token: 0x04003768 RID: 14184
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse1_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse1_01.prefab:f5ac5cb19cb255947801a48f02cbc076");

	// Token: 0x04003769 RID: 14185
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse3_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse3_01.prefab:0b5455531d74bba4796fa251229623e2");

	// Token: 0x0400376A RID: 14186
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse5_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse5_01.prefab:3b5d55db59c3c71499c3f9d6073ec53d");

	// Token: 0x0400376B RID: 14187
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse7_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse7_01.prefab:3eed315e9279e004ea65d7f193380b44");

	// Token: 0x0400376C RID: 14188
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8C_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8C_01.prefab:42d200acac8d10643b0aa17c7b746024");

	// Token: 0x0400376D RID: 14189
	private static readonly AssetReference VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8D_01 = new AssetReference("VO_BTA_BOSS_17h_Male_NightElf_Mission_Fight_17_Verse8D_01.prefab:797919cc75f886747bbffa12f0f959d7");

	// Token: 0x0400376E RID: 14190
	private HashSet<string> m_playedLines = new HashSet<string>();
}
