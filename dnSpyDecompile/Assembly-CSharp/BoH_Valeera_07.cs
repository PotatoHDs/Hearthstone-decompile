using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200055A RID: 1370
public class BoH_Valeera_07 : BoH_Valeera_Dungeon
{
	// Token: 0x06004BB6 RID: 19382 RVA: 0x00191008 File Offset: 0x0018F208
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Death_01,
			BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7EmoteResponse_01,
			BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeB_01,
			BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeE_02,
			BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_01,
			BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_02,
			BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_03,
			BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Intro_03,
			BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Loss_01,
			BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeC_01,
			BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeD_02,
			BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Intro_02,
			BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_01,
			BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_04,
			BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeB_02,
			BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeC_02,
			BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeD_01,
			BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Intro_01,
			BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Victory_03,
			BoH_Valeera_07.VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_01,
			BoH_Valeera_07.VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_03,
			BoH_Valeera_07.VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004BB7 RID: 19383 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004BB8 RID: 19384 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004BB9 RID: 19385 RVA: 0x001911CC File Offset: 0x0018F3CC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Intro_01);
		yield return base.MissionPlayVO(enemyActor, BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Intro_03);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004BBA RID: 19386 RVA: 0x001911DB File Offset: 0x0018F3DB
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004BBB RID: 19387 RVA: 0x001911E3 File Offset: 0x0018F3E3
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Death_01;
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DRG;
		this.m_standardEmoteResponseLine = BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7EmoteResponse_01;
	}

	// Token: 0x06004BBC RID: 19388 RVA: 0x00191218 File Offset: 0x0018F418
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

	// Token: 0x06004BBD RID: 19389 RVA: 0x0019129C File Offset: 0x0018F49C
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
		case 101:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(friendlyActor, BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeD_01);
			yield return base.MissionPlayVO(base.GetFriendlyActorByCardId("Story_06_Meryl"), BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeD_02);
			GameState.Get().SetBusy(false);
			break;
		case 102:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(friendlyActor, BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeD_01);
			yield return base.MissionPlayVO(base.GetFriendlyActorByCardId("Story_06_MerylDormant"), BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeD_02);
			GameState.Get().SetBusy(false);
			break;
		case 103:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(friendlyActor, BoH_Valeera_07.VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_01);
			yield return base.MissionPlayVO(enemyActor, BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeE_02);
			yield return base.MissionPlayVO(friendlyActor, BoH_Valeera_07.VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_03);
			GameState.Get().SetBusy(false);
			break;
		case 104:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_06_Meryl"), BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_07.VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7Victory_02, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 105:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_06_MerylDormant"), BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_07.VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7Victory_02, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 106:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Victory_03, 2.5f);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_06_Meryl"), BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_04, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 107:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Victory_03, 2.5f);
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_06_MerylDormant"), BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_04, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 108:
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_06_Meryl"), BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeC_02, 2.5f);
			break;
		case 109:
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_06_MerylDormant"), BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeC_02, 2.5f);
			break;
		default:
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
			else
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			break;
		}
		yield break;
	}

	// Token: 0x06004BBE RID: 19390 RVA: 0x001912B2 File Offset: 0x0018F4B2
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

	// Token: 0x06004BBF RID: 19391 RVA: 0x001912C8 File Offset: 0x0018F4C8
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

	// Token: 0x06004BC0 RID: 19392 RVA: 0x001912DE File Offset: 0x0018F4DE
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
			if (turn == 5)
			{
				yield return base.PlayLineAlways(actor, BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_07.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeB_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_06_Meryl"), BoH_Valeera_07.VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Intro_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x04004044 RID: 16452
	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Death_01 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Death_01.prefab:ca0a78c496b307f4d978e968148ee73a");

	// Token: 0x04004045 RID: 16453
	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7EmoteResponse_01.prefab:e6e6afd4839ebc64f9999af312fc5e07");

	// Token: 0x04004046 RID: 16454
	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeB_01.prefab:406c3e8392a4e4d40bf4a30d6dda954b");

	// Token: 0x04004047 RID: 16455
	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeE_02 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7ExchangeE_02.prefab:36d4f03c022f4ec4693955416caa1c57");

	// Token: 0x04004048 RID: 16456
	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_01 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_01.prefab:ad52fa28b2a8451459405f99e7892151");

	// Token: 0x04004049 RID: 16457
	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_02 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_02.prefab:09af7309e6ef6b94dad841fdd7019598");

	// Token: 0x0400404A RID: 16458
	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_03 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_03.prefab:3cdc9e3aee956d34784c11cf1460891b");

	// Token: 0x0400404B RID: 16459
	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Intro_03 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Intro_03.prefab:a24855c53cc35ff489f54bc6478723b3");

	// Token: 0x0400404C RID: 16460
	private static readonly AssetReference VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Loss_01 = new AssetReference("VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Loss_01.prefab:cc804cecd9372574194cbaaac6652de5");

	// Token: 0x0400404D RID: 16461
	private static readonly AssetReference VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeC_01 = new AssetReference("VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeC_01.prefab:7629175f09311124190bb4163c0084e4");

	// Token: 0x0400404E RID: 16462
	private static readonly AssetReference VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeD_02 = new AssetReference("VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7ExchangeD_02.prefab:85ecd6281705ded48b92e120122261ba");

	// Token: 0x0400404F RID: 16463
	private static readonly AssetReference VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Intro_02 = new AssetReference("VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Intro_02.prefab:91716e73c2c7a75428c8b1dae0c8b0ba");

	// Token: 0x04004050 RID: 16464
	private static readonly AssetReference VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_01 = new AssetReference("VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_01.prefab:b7d834ca9d05a1640a2d95f2f2fd967c");

	// Token: 0x04004051 RID: 16465
	private static readonly AssetReference VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_04 = new AssetReference("VO_Story_Minion_Meryl_Male_Undead_Story_Valeera_Mission7Victory_04.prefab:7c3b14fb3dd0f284481252da995310c9");

	// Token: 0x04004052 RID: 16466
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeB_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeB_02.prefab:fa404c845cad50945badfa8062f71af7");

	// Token: 0x04004053 RID: 16467
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeC_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeC_02.prefab:ff6a3372d722839469b7d8acbdf8c03b");

	// Token: 0x04004054 RID: 16468
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7ExchangeD_01.prefab:0f6f9ae519e612644ae5d51dadbfe6bb");

	// Token: 0x04004055 RID: 16469
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Intro_01.prefab:cc8606b2f3a208b4aa9c493bdcc0fa69");

	// Token: 0x04004056 RID: 16470
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Victory_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission7Victory_03.prefab:f684987f102468a44a31d7e5b657de34");

	// Token: 0x04004057 RID: 16471
	private static readonly AssetReference VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_01 = new AssetReference("VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_01.prefab:1365b33c2e210804f9e76f9e27a838c5");

	// Token: 0x04004058 RID: 16472
	private static readonly AssetReference VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_03 = new AssetReference("VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7ExchangeE_03.prefab:60cc81f8fef1ce64a8cc762754149b43");

	// Token: 0x04004059 RID: 16473
	private static readonly AssetReference VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7Victory_02 = new AssetReference("VO_Story_Hero_ValeeraDemon_Female_Demon_Story_Valeera_Mission7Victory_02.prefab:7a47b055f0d06ae4cb29e3f81989aff5");

	// Token: 0x0400405A RID: 16474
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_01,
		BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_02,
		BoH_Valeera_07.VO_Story_Hero_ChoGall_Male_Ogre_Story_Valeera_Mission7Idle_03
	};

	// Token: 0x0400405B RID: 16475
	private HashSet<string> m_playedLines = new HashSet<string>();
}
