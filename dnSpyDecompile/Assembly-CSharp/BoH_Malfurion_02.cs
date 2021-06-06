using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200052D RID: 1325
public class BoH_Malfurion_02 : BoH_Malfurion_Dungeon
{
	// Token: 0x06004829 RID: 18473 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600482A RID: 18474 RVA: 0x00182C08 File Offset: 0x00180E08
	public BoH_Malfurion_02()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Malfurion_02.s_booleanOptions);
	}

	// Token: 0x0600482B RID: 18475 RVA: 0x00182CAC File Offset: 0x00180EAC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01,
			BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01,
			BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01,
			BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01,
			BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01,
			BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeA_01,
			BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01,
			BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeA_02,
			BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeD_02,
			BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_02,
			BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_04,
			BoH_Malfurion_02.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeA_01,
			BoH_Malfurion_02.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeB_02,
			BoH_Malfurion_02.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeC_03,
			BoH_Malfurion_02.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeD_03,
			BoH_Malfurion_02.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2Intro_03,
			BoH_Malfurion_02.VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2ExchangeC_01,
			BoH_Malfurion_02.VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_01,
			BoH_Malfurion_02.VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_02,
			BoH_Malfurion_02.VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_03,
			BoH_Malfurion_02.VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Intro_01,
			BoH_Malfurion_02.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission2ExchangeE_01,
			BoH_Malfurion_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01,
			BoH_Malfurion_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01,
			BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_03,
			BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_04,
			BoH_Malfurion_02.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01,
			BoH_Malfurion_02.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01,
			BoH_Malfurion_02.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600482C RID: 18476 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600482D RID: 18477 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600482E RID: 18478 RVA: 0x00182EE0 File Offset: 0x001810E0
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Malfurion_02.VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Intro_01);
		yield return base.MissionPlayVO(base.GetFriendlyActorByCardId("Story_08_IllidanDormant"), BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_02);
		yield return base.MissionPlayVO(friendlyActor, BoH_Malfurion_02.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2Intro_03);
		yield return base.MissionPlayVO(base.GetFriendlyActorByCardId("Story_08_IllidanDormant"), BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_04);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600482F RID: 18479 RVA: 0x00182EEF File Offset: 0x001810EF
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004830 RID: 18480 RVA: 0x00182EF7 File Offset: 0x001810F7
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004831 RID: 18481 RVA: 0x00182EFF File Offset: 0x001810FF
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DHPrologue;
		base.OnCreateGame();
		this.m_deathLine = BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01;
		this.m_standardEmoteResponseLine = BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01;
	}

	// Token: 0x06004832 RID: 18482 RVA: 0x00182F34 File Offset: 0x00181134
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004833 RID: 18483 RVA: 0x00182FB8 File Offset: 0x001811B8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 105)
		{
			if (missionEvent != 504)
			{
				if (missionEvent == 507)
				{
					yield return base.PlayLineAlways(actor, BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01, 2.5f);
				}
				else
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_Illidan"), BoH_Malfurion_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_02.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01, 2.5f);
				yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_Illidan"), BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_03, 2.5f);
				yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_Illidan"), BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_04, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_02.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_Illidan"), BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeD_02, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_02.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeD_03, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004834 RID: 18484 RVA: 0x00182FCE File Offset: 0x001811CE
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

	// Token: 0x06004835 RID: 18485 RVA: 0x00182FE4 File Offset: 0x001811E4
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

	// Token: 0x06004836 RID: 18486 RVA: 0x00182FFA File Offset: 0x001811FA
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
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_02.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01, 2.5f);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_IllidanDormant"), BoH_Malfurion_02.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01, 2.5f);
			break;
		case 3:
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_02.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_IllidanDormant"), BoH_Malfurion_02.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeA_02, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_02.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeB_02, 2.5f);
			break;
		case 7:
			yield return base.PlayLineAlways(actor, BoH_Malfurion_02.VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_02.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeC_03, 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x04003BB8 RID: 15288
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Malfurion_02.InitBooleanOptions();

	// Token: 0x04003BB9 RID: 15289
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01.prefab:b8a47da9b0bd4a6448d223bef43d0220");

	// Token: 0x04003BBA RID: 15290
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_EmoteResponse_01.prefab:317e163701a77f84885c360265b7dde5");

	// Token: 0x04003BBB RID: 15291
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01.prefab:635024ae6a2db654e8c362c3872663d9");

	// Token: 0x04003BBC RID: 15292
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01.prefab:09d7a3dc443396146b14728f2a259a7f");

	// Token: 0x04003BBD RID: 15293
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01.prefab:caadce461e4d5f6449f0a22e3c7927e6");

	// Token: 0x04003BBE RID: 15294
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeA_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_ExchangeA_01.prefab:11b59196f3f88a14687333b92be8fecc");

	// Token: 0x04003BBF RID: 15295
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mission3_Loss_01.prefab:ee1e6da806ec47c2a9e815fb60456d1a");

	// Token: 0x04003BC0 RID: 15296
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeA_02.prefab:56b055f6adf889a40b58d21ebdae2830");

	// Token: 0x04003BC1 RID: 15297
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeD_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2ExchangeD_02.prefab:a6061c992cd5f8441affa79c42bccdc5");

	// Token: 0x04003BC2 RID: 15298
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_02.prefab:b0d7066942a6ed440925d9657ac5cfb7");

	// Token: 0x04003BC3 RID: 15299
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_04 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Intro_04.prefab:0c619a5fd68444f46858b47a7d3238c4");

	// Token: 0x04003BC4 RID: 15300
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeA_01.prefab:e283643f45dfd4646be4a091e3319229");

	// Token: 0x04003BC5 RID: 15301
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeB_02.prefab:2753323cad2b0944d988affc7ca63814");

	// Token: 0x04003BC6 RID: 15302
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeC_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeC_03.prefab:8430d6ada4b5b5c488f13ac2e3cc97e7");

	// Token: 0x04003BC7 RID: 15303
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeD_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2ExchangeD_03.prefab:a8b82e26b654faf45924c4af1fe44902");

	// Token: 0x04003BC8 RID: 15304
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2Intro_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission2Intro_03.prefab:8fe6ed1f5c3a0c54aa506b86fb53ff1a");

	// Token: 0x04003BC9 RID: 15305
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Turn1_01_01.prefab:99adf22deefe98f4bb271310cb564eb1");

	// Token: 0x04003BCA RID: 15306
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2ExchangeC_01.prefab:10b01c598d4dc6549a1bef8bfcff6614");

	// Token: 0x04003BCB RID: 15307
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_01.prefab:b43846f3a5ae7814f9b47a7138433885");

	// Token: 0x04003BCC RID: 15308
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_02.prefab:b5e8ea72dde49d34b9fb738134caf50a");

	// Token: 0x04003BCD RID: 15309
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_03.prefab:f0c1b712611c9064b9afa7bfdec2bf44");

	// Token: 0x04003BCE RID: 15310
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Intro_01.prefab:be70851298e525844ad9cb3ed9cb4bf2");

	// Token: 0x04003BCF RID: 15311
	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission2ExchangeE_01 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission2ExchangeE_01.prefab:f1bb3cf4be596f44e8c11b51599e9104");

	// Token: 0x04003BD0 RID: 15312
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Turn1_02_01.prefab:bb40d6ba961eac94aa12326f5241b821");

	// Token: 0x04003BD1 RID: 15313
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission3_Victory_01_01.prefab:0c0de402265457f488187c4400c4b07b");

	// Token: 0x04003BD2 RID: 15314
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_PreMission4_Intro_01_01.prefab:f06c936a8b4b0ea4398b87b4f5b50df9");

	// Token: 0x04003BD3 RID: 15315
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_03.prefab:f3d3696044b7b2047986c9de7d4413cb");

	// Token: 0x04003BD4 RID: 15316
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_04 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission2Victory_04.prefab:4b3b457b48d16f244959f6bd18901972");

	// Token: 0x04003BD5 RID: 15317
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission3_Victory_02_01.prefab:27a15072a8c732e46a10856d40ab1f86");

	// Token: 0x04003BD6 RID: 15318
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_PreMission4_Intro_02_01.prefab:883bced611fdb5c43bfed83b5949d416");

	// Token: 0x04003BD7 RID: 15319
	public static readonly AssetReference IllidanBrassRing = new AssetReference("DemonHunter_Illidan_Popup_BrassRing.prefab:8c007b8e8be417c4fbd9738960e6f7f0");

	// Token: 0x04003BD8 RID: 15320
	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	// Token: 0x04003BD9 RID: 15321
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_01_01,
		BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_02_01,
		BoH_Malfurion_02.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_HeroPower_03_01
	};

	// Token: 0x04003BDA RID: 15322
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Malfurion_02.VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_01,
		BoH_Malfurion_02.VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_02,
		BoH_Malfurion_02.VO_Story_Hero_Mannoroth_Male_Demon_Story_Malfurion_Mission2Idle_03
	};

	// Token: 0x04003BDB RID: 15323
	private HashSet<string> m_playedLines = new HashSet<string>();
}
