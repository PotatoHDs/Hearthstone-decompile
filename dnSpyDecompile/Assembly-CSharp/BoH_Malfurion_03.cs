using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200052E RID: 1326
public class BoH_Malfurion_03 : BoH_Malfurion_Dungeon
{
	// Token: 0x0600483B RID: 18491 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600483C RID: 18492 RVA: 0x00183208 File Offset: 0x00181408
	public BoH_Malfurion_03()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Malfurion_03.s_booleanOptions);
	}

	// Token: 0x0600483D RID: 18493 RVA: 0x001832AC File Offset: 0x001814AC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Malfurion_03.VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01,
			BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3_Victory_01,
			BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3EmoteResponse_01,
			BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3ExchangeB_02,
			BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_01,
			BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_02,
			BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_03,
			BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_01,
			BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_02,
			BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_03,
			BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Loss_01,
			BoH_Malfurion_03.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeA_02,
			BoH_Malfurion_03.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeB_01,
			BoH_Malfurion_03.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3Victory_03,
			BoH_Malfurion_03.VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_02,
			BoH_Malfurion_03.VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_04,
			BoH_Malfurion_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01,
			BoH_Malfurion_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01,
			BoH_Malfurion_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01,
			BoH_Malfurion_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01,
			BoH_Malfurion_03.VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600483E RID: 18494 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600483F RID: 18495 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004840 RID: 18496 RVA: 0x00183460 File Offset: 0x00181660
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(BoH_Malfurion_03.CenariusBrassRing, BoH_Malfurion_03.VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01, 2.5f);
		yield return base.MissionPlayVO(friendlyActor, BoH_Malfurion_03.VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01);
		yield return base.MissionPlayVO(enemyActor, BoH_Malfurion_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004841 RID: 18497 RVA: 0x0018346F File Offset: 0x0018166F
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004842 RID: 18498 RVA: 0x00183477 File Offset: 0x00181677
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004843 RID: 18499 RVA: 0x0018347F File Offset: 0x0018167F
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DHPrologueBoss;
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3EmoteResponse_01;
	}

	// Token: 0x06004844 RID: 18500 RVA: 0x001834A4 File Offset: 0x001816A4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004845 RID: 18501 RVA: 0x0018352D File Offset: 0x0018172D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 504)
			{
				if (missionEvent == 507)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(actor, BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Loss_01, 2.5f);
					GameState.Get().SetBusy(false);
				}
				else
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3_Victory_01, 2.5f);
				yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_PriestessMaiev"), BoH_Malfurion_03.VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_02, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_03.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3Victory_03, 2.5f);
				yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_PriestessMaiev"), BoH_Malfurion_03.VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_04, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3_Victory_01, 2.5f);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_PriestessMaievFake"), BoH_Malfurion_03.VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_02, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_03.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3Victory_03, 2.5f);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_PriestessMaievFake"), BoH_Malfurion_03.VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_04, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004846 RID: 18502 RVA: 0x00183543 File Offset: 0x00181743
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

	// Token: 0x06004847 RID: 18503 RVA: 0x00183559 File Offset: 0x00181759
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

	// Token: 0x06004848 RID: 18504 RVA: 0x0018356F File Offset: 0x0018176F
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn != 5)
			{
				if (turn == 9)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_03.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeA_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_03.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3ExchangeB_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(BoH_Malfurion_03.TyrandeBrassRing, BoH_Malfurion_03.VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_03.VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003BDC RID: 15324
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Malfurion_03.InitBooleanOptions();

	// Token: 0x04003BDD RID: 15325
	private static readonly AssetReference VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01 = new AssetReference("VO_Prologue_Cenarius_Male_Demigod_Prologue_Mission4_Turn2_01_01.prefab:7bb08f07ba52f6648a829e02eb487881");

	// Token: 0x04003BDE RID: 15326
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3_Victory_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3_Victory_01.prefab:c61b231da2cef824fb0f3fa7e084c47c");

	// Token: 0x04003BDF RID: 15327
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3EmoteResponse_01.prefab:c185892269d345c4c9dbf7f21c9b6716");

	// Token: 0x04003BE0 RID: 15328
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3ExchangeB_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3ExchangeB_02.prefab:35a36132cfb8d4849925052626d573ae");

	// Token: 0x04003BE1 RID: 15329
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_01.prefab:aad833b61489dbf4685ebf2eeb0c3627");

	// Token: 0x04003BE2 RID: 15330
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_02.prefab:8f0d17e6d8c7c704da3e37e301bd8c60");

	// Token: 0x04003BE3 RID: 15331
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_03.prefab:876f1ba624edb9a458abf2d46b1b179f");

	// Token: 0x04003BE4 RID: 15332
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_01.prefab:1e77ec26a2325ac41bd55ebc19c670dc");

	// Token: 0x04003BE5 RID: 15333
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_02.prefab:3e2fed721a3834b4fbd36a16972c8089");

	// Token: 0x04003BE6 RID: 15334
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_03.prefab:26e8cf6f72bda7b47a3735ab4fe5c66f");

	// Token: 0x04003BE7 RID: 15335
	private static readonly AssetReference VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Loss_01.prefab:9405c00a59e48b341af329064c19bcde");

	// Token: 0x04003BE8 RID: 15336
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeA_02.prefab:580dd5b8073ebfe4bbe9c958fd546ba0");

	// Token: 0x04003BE9 RID: 15337
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3ExchangeB_01.prefab:88cec326552c7af4294ad484a1dc8366");

	// Token: 0x04003BEA RID: 15338
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3Victory_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission3Victory_03.prefab:b56d674b7a383d6458544f552cf32196");

	// Token: 0x04003BEB RID: 15339
	private static readonly AssetReference VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_02 = new AssetReference("VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_02.prefab:fc363b3ab82aa6546b5618fd880b7714");

	// Token: 0x04003BEC RID: 15340
	private static readonly AssetReference VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_04 = new AssetReference("VO_Story_Minion_Maiev_Female_NightElf_Story_Malfurion_Mission3_Victory_04.prefab:049de6375f4a59f43813de4ace958993");

	// Token: 0x04003BED RID: 15341
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeA_01.prefab:840bb7195ebb84a40b7904a3d736fe21");

	// Token: 0x04003BEE RID: 15342
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_ExchangeC_02_01.prefab:bd43645aeade43c4b9b16f5b15d8adb2");

	// Token: 0x04003BEF RID: 15343
	private static readonly AssetReference VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01 = new AssetReference("VO_TB_Hero_Illidan2_Male_NightElf_Prologue_Mission4_Turn1_02_01.prefab:22e60a23f6686734b9f429258774f9de");

	// Token: 0x04003BF0 RID: 15344
	private static readonly AssetReference VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01 = new AssetReference("VO_TB_Hero_Malfurion_Male_NightElf_Prologue_Mission4_Turn1_01_01.prefab:8254a4f7f73afec47ab6b25d383b535b");

	// Token: 0x04003BF1 RID: 15345
	private static readonly AssetReference VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01 = new AssetReference("VO_TB_Hero_Tyrande_Female_NightElf_Prologue_Mission4_ExchangeC_01_01.prefab:0325e29a9def4273954bfd4593b840d3");

	// Token: 0x04003BF2 RID: 15346
	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	// Token: 0x04003BF3 RID: 15347
	public static readonly AssetReference CenariusBrassRing = new AssetReference("Cenarius_BrassRing_Quote.prefab:9157110d07b5b004fa0c0f651c71ef81");

	// Token: 0x04003BF4 RID: 15348
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_01,
		BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_02,
		BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3HeroPower_03
	};

	// Token: 0x04003BF5 RID: 15349
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_01,
		BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_02,
		BoH_Malfurion_03.VO_Story_Hero_Illidan_Male_NightElf_Story_Malfurion_Mission3Idle_03
	};

	// Token: 0x04003BF6 RID: 15350
	private HashSet<string> m_playedLines = new HashSet<string>();
}
