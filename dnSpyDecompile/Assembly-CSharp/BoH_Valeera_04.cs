using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000557 RID: 1367
public class BoH_Valeera_04 : BoH_Valeera_Dungeon
{
	// Token: 0x06004B83 RID: 19331 RVA: 0x001900A0 File Offset: 0x0018E2A0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Valeera_04.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeB_02,
			BoH_Valeera_04.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeC_02,
			BoH_Valeera_04.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4Victory_04,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Death_01,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4EmoteResponse_01,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4ExchangeA_01,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_01,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_02,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_03,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_01,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_02,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_03,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Intro_01,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Loss_01,
			BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Victory_01,
			BoH_Valeera_04.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeB_01,
			BoH_Valeera_04.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeC_03,
			BoH_Valeera_04.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Intro_02,
			BoH_Valeera_04.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Victory_03,
			BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeA_02,
			BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_01,
			BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_03,
			BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Intro_03,
			BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_02,
			BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_05
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004B84 RID: 19332 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004B85 RID: 19333 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004B86 RID: 19334 RVA: 0x00190294 File Offset: 0x0018E494
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Intro_01);
		yield return base.MissionPlayVO(BoH_Valeera_04.BrollBrassRing, BoH_Valeera_04.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Intro_02);
		yield return base.MissionPlayVO(friendlyActor, BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Intro_03);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004B87 RID: 19335 RVA: 0x001902A3 File Offset: 0x0018E4A3
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004B88 RID: 19336 RVA: 0x001902AB File Offset: 0x0018E4AB
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004B89 RID: 19337 RVA: 0x001902B3 File Offset: 0x0018E4B3
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Death_01;
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BT;
		this.m_standardEmoteResponseLine = BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4EmoteResponse_01;
	}

	// Token: 0x06004B8A RID: 19338 RVA: 0x001902E8 File Offset: 0x0018E4E8
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

	// Token: 0x06004B8B RID: 19339 RVA: 0x0019036C File Offset: 0x0018E56C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 102)
		{
			if (missionEvent != 103)
			{
				if (missionEvent == 507)
				{
					yield return base.PlayLineAlways(actor, BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Loss_01, 2.5f);
				}
				else
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_02, 2.5f);
				yield return base.PlayLineAlways(BoH_Valeera_04.BrollBrassRing, BoH_Valeera_04.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Victory_03, 2.5f);
				yield return base.PlayLineAlways(BoH_Valeera_04.VarianBrassRing, BoH_Valeera_04.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4Victory_04, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_05, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004B8C RID: 19340 RVA: 0x00190382 File Offset: 0x0018E582
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

	// Token: 0x06004B8D RID: 19341 RVA: 0x00190398 File Offset: 0x0018E598
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

	// Token: 0x06004B8E RID: 19342 RVA: 0x001903AE File Offset: 0x0018E5AE
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
			if (turn != 7)
			{
				if (turn == 11)
				{
					yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(BoH_Valeera_04.VarianBrassRing, BoH_Valeera_04.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeC_02, 2.5f);
					yield return base.PlayLineAlways(BoH_Valeera_04.BrollBrassRing, BoH_Valeera_04.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeC_03, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(BoH_Valeera_04.BrollBrassRing, BoH_Valeera_04.VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Valeera_04.VarianBrassRing, BoH_Valeera_04.VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeB_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_04.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeA_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003FEF RID: 16367
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeB_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeB_02.prefab:dd2109fe438749c4599c34d7243532d2");

	// Token: 0x04003FF0 RID: 16368
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeC_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4ExchangeC_02.prefab:1ca6cc69fe12fe14b967ae296ebe4ea1");

	// Token: 0x04003FF1 RID: 16369
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4Victory_04 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Valeera_Mission4Victory_04.prefab:cc562277732f2f6498fd567898469d1b");

	// Token: 0x04003FF2 RID: 16370
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Death_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Death_01.prefab:f9e8f04ef7aae384bbe30e77df4b9fdf");

	// Token: 0x04003FF3 RID: 16371
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4EmoteResponse_01.prefab:748c18b7b52d09b49a1536bfcc30297b");

	// Token: 0x04003FF4 RID: 16372
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4ExchangeA_01.prefab:157cff44b3f14a64593d1f30a78c0393");

	// Token: 0x04003FF5 RID: 16373
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_01.prefab:41d0a1f9a407add43bcda581166286e5");

	// Token: 0x04003FF6 RID: 16374
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_02.prefab:792f6aa0ff77c85479a412042ae215f0");

	// Token: 0x04003FF7 RID: 16375
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_03 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_03.prefab:e5679e2be5dfd6b40a915e6a4d5e0379");

	// Token: 0x04003FF8 RID: 16376
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_01.prefab:fb1aca4772cf0064398153c0ea1e8bff");

	// Token: 0x04003FF9 RID: 16377
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_02 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_02.prefab:94c65fe0345b00d4faa1ed02a194a0de");

	// Token: 0x04003FFA RID: 16378
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_03 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_03.prefab:bf5d0de953abd3d4fa1cd5408a2d27e3");

	// Token: 0x04003FFB RID: 16379
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Intro_01.prefab:cf87ece7f8cf3234a8d1588c55ff8e08");

	// Token: 0x04003FFC RID: 16380
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Loss_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Loss_01.prefab:c163ec3c03658af4eb19f1036e4eef87");

	// Token: 0x04003FFD RID: 16381
	private static readonly AssetReference VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Victory_01.prefab:fe4234dfdd5be7b4a8f6568c843dee50");

	// Token: 0x04003FFE RID: 16382
	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeB_01 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeB_01.prefab:98bd91f390743bf4aba9425254c647ab");

	// Token: 0x04003FFF RID: 16383
	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeC_03 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4ExchangeC_03.prefab:8b53af1c3bbad7e4cb275990db51a086");

	// Token: 0x04004000 RID: 16384
	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Intro_02 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Intro_02.prefab:2bca6024fcf6c8a46856c26f432b718e");

	// Token: 0x04004001 RID: 16385
	private static readonly AssetReference VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Victory_03 = new AssetReference("VO_Story_Minion_Broll_Male_NightElf_Story_Valeera_Mission4Victory_03.prefab:654c0f5dddd28df4cbadbedad0d285a1");

	// Token: 0x04004002 RID: 16386
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeA_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeA_02.prefab:58ad4e7fe7f56ec40a130abc4cff96e2");

	// Token: 0x04004003 RID: 16387
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_01.prefab:1de707331d28620448c15efd9e20ce71");

	// Token: 0x04004004 RID: 16388
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4ExchangeC_03.prefab:c0e05ff08be572940a1893086ead4daf");

	// Token: 0x04004005 RID: 16389
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Intro_03 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Intro_03.prefab:7863afee8b3a15d4e9b2a606917b33e3");

	// Token: 0x04004006 RID: 16390
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_02.prefab:0ccb86e6efd0efd449633932bfe9baef");

	// Token: 0x04004007 RID: 16391
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_05 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission4Victory_05.prefab:94d1130ab18533145a0805b198a56fcb");

	// Token: 0x04004008 RID: 16392
	public static readonly AssetReference VarianBrassRing = new AssetReference("Varian_BrassRing_Quote.prefab:b192b80fcc22d1145bfa81b476cecc09");

	// Token: 0x04004009 RID: 16393
	public static readonly AssetReference BrollBrassRing = new AssetReference("Broll_BrassRing_Quote.prefab:1bfe5acde48846249b4b7716c3ff0d8c");

	// Token: 0x0400400A RID: 16394
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_01,
		BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_02,
		BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4HeroPower_03
	};

	// Token: 0x0400400B RID: 16395
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_01,
		BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_02,
		BoH_Valeera_04.VO_Story_Hero_Vendellin_Male_BloodElf_Story_Valeera_Mission4Idle_03
	};

	// Token: 0x0400400C RID: 16396
	private HashSet<string> m_playedLines = new HashSet<string>();
}
