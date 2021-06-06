using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004F8 RID: 1272
public class BTA_Fight_14 : BTA_Dungeon
{
	// Token: 0x0600446B RID: 17515 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600446C RID: 17516 RVA: 0x00172AE5 File Offset: 0x00170CE5
	public BTA_Fight_14()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_14.s_booleanOptions);
	}

	// Token: 0x0600446D RID: 17517 RVA: 0x00172B08 File Offset: 0x00170D08
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_14.VO_BTA_01_Female_NightElf_Mission_Fight_14_BossStartReaverResponse_01,
			BTA_Fight_14.VO_BTA_01_Female_NightElf_Mission_Fight_14_PlayerStart_01,
			BTA_Fight_14.VO_BTA_01_Female_NightElf_Mission_Fight_14_Victory_01,
			BTA_Fight_14.VO_BTA_05_Male_Sporelok_Mission_Fight_14_BrassRing_01,
			BTA_Fight_14.VO_BTA_08_Male_Orc_Mission_Fight_14_Misc_01_01,
			BTA_Fight_14.VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_BossStartImp_01,
			BTA_Fight_14.VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_Turn3_01,
			BTA_Fight_14.BTA_BOSS_14h2_RustedFelReaver_Intro,
			BTA_Fight_14.BTA_BOSS_14h2_RustedFelReaver_Death,
			BTA_Fight_14.BTA_BOSS_14h2_RustedFelReaver_Defeat,
			BTA_Fight_14.BTA_BOSS_14h2_RustedFelReaver_Taunt
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600446E RID: 17518 RVA: 0x00172C1C File Offset: 0x00170E1C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_14.BTA_BOSS_14h2_RustedFelReaver_Death;
	}

	// Token: 0x0600446F RID: 17519 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004470 RID: 17520 RVA: 0x00172C34 File Offset: 0x00170E34
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_14.VO_BTA_01_Female_NightElf_Mission_Fight_14_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_14.VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_BossStartImp_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004471 RID: 17521 RVA: 0x00172C44 File Offset: 0x00170E44
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			return;
		}
		if (cardId == "BTA_BOSS_14h")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_14.VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_BossStartImp_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (cardId == "BTA_BOSS_14h2")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_14.BTA_BOSS_14h2_RustedFelReaver_Taunt, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x06004472 RID: 17522 RVA: 0x00172D15 File Offset: 0x00170F15
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				if (missionEvent != 501)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(actor, BTA_Fight_14.VO_BTA_01_Female_NightElf_Mission_Fight_14_Victory_01, 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName("");
				Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).UpdateHeroNameBanner();
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_06"), BTA_Dungeon.SklibbBrassRingDemonHunter, BTA_Fight_14.VO_BTA_05_Male_Sporelok_Mission_Fight_14_BrassRing_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			base.PlaySound(BTA_Fight_14.BTA_BOSS_14h2_RustedFelReaver_Intro, 1f, true, false);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004473 RID: 17523 RVA: 0x00172D2B File Offset: 0x00170F2B
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
		string cardId2 = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "BTA_08" && cardId2 == "BTA_BOSS_14h2")
		{
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_08"), BTA_Dungeon.KarnukBrassRingDemonHunter, BTA_Fight_14.VO_BTA_08_Male_Orc_Mission_Fight_14_Misc_01_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004474 RID: 17524 RVA: 0x00172D41 File Offset: 0x00170F41
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

	// Token: 0x06004475 RID: 17525 RVA: 0x00172D57 File Offset: 0x00170F57
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn == 3)
		{
			base.PlaySound(BTA_Fight_14.BTA_BOSS_14h2_RustedFelReaver_Intro, 1f, true, false);
			yield return new WaitForSeconds(2f);
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_14.VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_Turn3_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003703 RID: 14083
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_14.InitBooleanOptions();

	// Token: 0x04003704 RID: 14084
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_14_BossStartReaverResponse_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_14_BossStartReaverResponse_01.prefab:86f5af34c713a064c91e870a2bc1eabe");

	// Token: 0x04003705 RID: 14085
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_14_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_14_PlayerStart_01.prefab:d373fa6a9ce5df946a0cde85932538a6");

	// Token: 0x04003706 RID: 14086
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_14_Victory_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_14_Victory_01.prefab:89b7923056f049b4a939ab39efce5e8c");

	// Token: 0x04003707 RID: 14087
	private static readonly AssetReference VO_BTA_05_Male_Sporelok_Mission_Fight_14_BrassRing_01 = new AssetReference("VO_BTA_05_Male_Sporelok_Mission_Fight_14_BrassRing_01.prefab:25170d86adb671e4d8e4b8c8d3c91d96");

	// Token: 0x04003708 RID: 14088
	private static readonly AssetReference VO_BTA_08_Male_Orc_Mission_Fight_14_Misc_01_01 = new AssetReference("VO_BTA_08_Male_Orc_Mission_Fight_14_Misc_01_01.prefab:f6375b435acf09847954a6079d072f57");

	// Token: 0x04003709 RID: 14089
	private static readonly AssetReference VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_BossStartImp_01 = new AssetReference("VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_BossStartImp_01.prefab:b7fed89fc1054af4c94cc5159ed38678");

	// Token: 0x0400370A RID: 14090
	private static readonly AssetReference VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_Turn3_01 = new AssetReference("VO_BTA_BOSS_14h_Male_Demon_Mission_Fight_14_Turn3_01.prefab:3b6fb5f258ae57b46b780b6c4c9be4cd");

	// Token: 0x0400370B RID: 14091
	private static readonly AssetReference BTA_BOSS_14h2_RustedFelReaver_Death = new AssetReference("BTA_BOSS_14h2_RustedFelReaver_Death.prefab:e6a44b4fcf5c0774c859ebab4fa65461");

	// Token: 0x0400370C RID: 14092
	private static readonly AssetReference BTA_BOSS_14h2_RustedFelReaver_Defeat = new AssetReference("BTA_BOSS_14h2_RustedFelReaver_Defeat.prefab:246418e687442c84382afc90ea2e35c2");

	// Token: 0x0400370D RID: 14093
	private static readonly AssetReference BTA_BOSS_14h2_RustedFelReaver_Intro = new AssetReference("BTA_BOSS_14h2_RustedFelReaver_Intro.prefab:0dce97737b2e8464591b83dc06dfe47d");

	// Token: 0x0400370E RID: 14094
	private static readonly AssetReference BTA_BOSS_14h2_RustedFelReaver_Taunt = new AssetReference("BTA_BOSS_14h2_RustedFelReaver_Taunt.prefab:9329c646808cb0c41b4f065d80b2d40b");

	// Token: 0x0400370F RID: 14095
	private HashSet<string> m_playedLines = new HashSet<string>();
}
