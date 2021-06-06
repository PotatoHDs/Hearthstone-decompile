using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000559 RID: 1369
public class BoH_Valeera_06 : BoH_Valeera_Dungeon
{
	// Token: 0x06004BA5 RID: 19365 RVA: 0x00190B38 File Offset: 0x0018ED38
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Death_01,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6EmoteResponse_01,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeA_02,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeB_01,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeC_01,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_01,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_02,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_03,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_01,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_02,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_03,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Intro_02,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Loss_01,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_01,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_04,
			BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_05,
			BoH_Valeera_06.VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission6Victory_03,
			BoH_Valeera_06.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6ExchangeA_01,
			BoH_Valeera_06.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6ExchangeC_02,
			BoH_Valeera_06.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6Intro_01,
			BoH_Valeera_06.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004BA6 RID: 19366 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004BA7 RID: 19367 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004BA8 RID: 19368 RVA: 0x00190CEC File Offset: 0x0018EEEC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Valeera_06.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6Intro_01);
		yield return base.MissionPlayVO(enemyActor, BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004BA9 RID: 19369 RVA: 0x00190CFB File Offset: 0x0018EEFB
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004BAA RID: 19370 RVA: 0x00190D03 File Offset: 0x0018EF03
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004BAB RID: 19371 RVA: 0x00190D0B File Offset: 0x0018EF0B
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Death_01;
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BRMAdventure;
		this.m_standardEmoteResponseLine = BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6EmoteResponse_01;
	}

	// Token: 0x06004BAC RID: 19372 RVA: 0x00190D40 File Offset: 0x0018EF40
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

	// Token: 0x06004BAD RID: 19373 RVA: 0x00190DC4 File Offset: 0x0018EFC4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 102)
		{
			if (missionEvent != 103)
			{
				if (missionEvent == 507)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(enemyActor, BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Loss_01, 2.5f);
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
				yield return base.PlayLineAlways(enemyActor, BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_05, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(enemyActor, BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_06.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6Victory_02, 2.5f);
			yield return base.PlayLineAlways(BoH_Valeera_06.JainaBrassRing, BoH_Valeera_06.VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission6Victory_03, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_04, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004BAE RID: 19374 RVA: 0x00190DDA File Offset: 0x0018EFDA
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

	// Token: 0x06004BAF RID: 19375 RVA: 0x00190DF0 File Offset: 0x0018EFF0
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

	// Token: 0x06004BB0 RID: 19376 RVA: 0x00190E06 File Offset: 0x0018F006
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 5)
			{
				if (turn == 11)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_06.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6ExchangeC_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Valeera_06.VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeA_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x0400402B RID: 16427
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Death_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Death_01.prefab:d4b75918e6afaf34b840b2a558b1a7b1");

	// Token: 0x0400402C RID: 16428
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6EmoteResponse_01.prefab:0110c821ef498d24da9e153e71af0729");

	// Token: 0x0400402D RID: 16429
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeA_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeA_02.prefab:096d086a4fcbfd04dbceaef4a96ffbec");

	// Token: 0x0400402E RID: 16430
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeB_01.prefab:f16c8c3351cf26a4f9ab0443b2a0d0af");

	// Token: 0x0400402F RID: 16431
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6ExchangeC_01.prefab:59b79fe7aa6881d4c88679fda7e23395");

	// Token: 0x04004030 RID: 16432
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_01.prefab:5dead36f717db5c4d99dbb049c83a219");

	// Token: 0x04004031 RID: 16433
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_02.prefab:8489c253666cfb94a9aa3690463761bf");

	// Token: 0x04004032 RID: 16434
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_03.prefab:0098145bff4bfdf439cab04fd5d7c4b4");

	// Token: 0x04004033 RID: 16435
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_01.prefab:42f49c3ee98210e42baa03ca50461338");

	// Token: 0x04004034 RID: 16436
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_02.prefab:eacecb3711cb11545b0564366eb24809");

	// Token: 0x04004035 RID: 16437
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_03.prefab:7c3ccab02fe20ed41ba881e7cc602f36");

	// Token: 0x04004036 RID: 16438
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Intro_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Intro_02.prefab:8f3539847cc3dff40a3926096d23a718");

	// Token: 0x04004037 RID: 16439
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Loss_01.prefab:55d721e9d7ec7f44c89516f9b9cd5c6a");

	// Token: 0x04004038 RID: 16440
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_01.prefab:e40326e699342794ab6942a04da88982");

	// Token: 0x04004039 RID: 16441
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_04 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_04.prefab:0a36d228fd5802f449b214dd3db44b00");

	// Token: 0x0400403A RID: 16442
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_05 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Victory_05.prefab:00e3a98fd67793e428abf80d2c813316");

	// Token: 0x0400403B RID: 16443
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission6Victory_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Valeera_Mission6Victory_03.prefab:0600016db0bd5fe4eabedabdb22c8c7e");

	// Token: 0x0400403C RID: 16444
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6ExchangeA_01.prefab:bac080dc857fe574fb22a771e025d06c");

	// Token: 0x0400403D RID: 16445
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6ExchangeC_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6ExchangeC_02.prefab:7b994f652fd682d4d9c2c2bb33ad18fa");

	// Token: 0x0400403E RID: 16446
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6Intro_01.prefab:ffcedc853085c5b4990c562e1b82a05e");

	// Token: 0x0400403F RID: 16447
	private static readonly AssetReference VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Valeera_Female_BloodElf_Story_Valeera_Mission6Victory_02.prefab:4d3b51d3cf1765d44a82ec0bfdc74f02");

	// Token: 0x04004040 RID: 16448
	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	// Token: 0x04004041 RID: 16449
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_01,
		BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_02,
		BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6HeroPower_03
	};

	// Token: 0x04004042 RID: 16450
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_01,
		BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_02,
		BoH_Valeera_06.VO_Story_Hero_Garona_Female_Orc_Story_Valeera_Mission6Idle_03
	};

	// Token: 0x04004043 RID: 16451
	private HashSet<string> m_playedLines = new HashSet<string>();
}
