using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200052F RID: 1327
public class BoH_Malfurion_04 : BoH_Malfurion_Dungeon
{
	// Token: 0x0600484D RID: 18509 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600484E RID: 18510 RVA: 0x001836F8 File Offset: 0x001818F8
	public BoH_Malfurion_04()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Malfurion_04.s_booleanOptions);
	}

	// Token: 0x0600484F RID: 18511 RVA: 0x0018379C File Offset: 0x0018199C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01,
			BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01,
			BoH_Malfurion_04.VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_04,
			BoH_Malfurion_04.VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_06,
			BoH_Malfurion_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01,
			BoH_Malfurion_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01,
			BoH_Malfurion_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01,
			BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_02,
			BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_03,
			BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_01,
			BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_02,
			BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_01,
			BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_03,
			BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_02,
			BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_05,
			BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Victory_01,
			BoH_Malfurion_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01,
			BoH_Malfurion_04.VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_03,
			BoH_Malfurion_04.VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_07,
			BoH_Malfurion_04.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4Intro_01,
			BoH_Malfurion_04.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4ExchangeF_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004850 RID: 18512 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004851 RID: 18513 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004852 RID: 18514 RVA: 0x00183A00 File Offset: 0x00181C00
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(BoH_Malfurion_04.TyrandeBrassRing, BoH_Malfurion_04.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_02);
		yield return base.MissionPlayVO(BoH_Malfurion_04.ThrallBrassRing, BoH_Malfurion_04.VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_03);
		yield return base.MissionPlayVO(BoH_Malfurion_04.JainaBrassRing, BoH_Malfurion_04.VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_04);
		yield return base.MissionPlayVO(friendlyActor, BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_05);
		yield return base.MissionPlayVO(BoH_Malfurion_04.JainaBrassRing, BoH_Malfurion_04.VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_06);
		yield return base.MissionPlayVO(BoH_Malfurion_04.ThrallBrassRing, BoH_Malfurion_04.VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_07);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004853 RID: 18515 RVA: 0x00183A0F File Offset: 0x00181C0F
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004854 RID: 18516 RVA: 0x00183A17 File Offset: 0x00181C17
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004855 RID: 18517 RVA: 0x00183A1F File Offset: 0x00181C1F
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DHPrologueBoss;
		base.OnCreateGame();
		this.m_deathLine = BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01;
		this.m_standardEmoteResponseLine = BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01;
	}

	// Token: 0x06004856 RID: 18518 RVA: 0x00183A54 File Offset: 0x00181C54
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

	// Token: 0x06004857 RID: 18519 RVA: 0x00183AD8 File Offset: 0x00181CD8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 504)
		{
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01, 2.5f);
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
			yield return base.PlayLineAlways(BoH_Malfurion_04.JainaBrassRing, BoH_Malfurion_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01, 2.5f);
			yield return base.PlayLineAlways(BoH_Malfurion_04.ThrallBrassRing, BoH_Malfurion_04.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004858 RID: 18520 RVA: 0x00183AEE File Offset: 0x00181CEE
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

	// Token: 0x06004859 RID: 18521 RVA: 0x00183B04 File Offset: 0x00181D04
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

	// Token: 0x0600485A RID: 18522 RVA: 0x00183B1A File Offset: 0x00181D1A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 1:
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_02, 2.5f);
			break;
		case 2:
		case 4:
			break;
		case 3:
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_02, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_03, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02, 2.5f);
			yield return base.PlayLineAlways(BoH_Malfurion_04.JainaBrassRing, BoH_Malfurion_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01, 2.5f);
			break;
		default:
			if (turn != 9)
			{
				if (turn == 13)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01, 2.5f);
					yield return base.PlayLineAlways(BoH_Malfurion_04.JainaBrassRing, BoH_Malfurion_04.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Malfurion_04.TyrandeBrassRing, BoH_Malfurion_04.VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4ExchangeF_02, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Malfurion_04.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_03, 2.5f);
			}
			break;
		}
		yield break;
	}

	// Token: 0x04003BF7 RID: 15351
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Malfurion_04.InitBooleanOptions();

	// Token: 0x04003BF8 RID: 15352
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01.prefab:7c3b0168dcf6b1f4c98413fcf42948b1");

	// Token: 0x04003BF9 RID: 15353
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01.prefab:8fe4ef854d34eb642b887b2839e26477");

	// Token: 0x04003BFA RID: 15354
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01.prefab:004cc80b01319e949835075a088d4afd");

	// Token: 0x04003BFB RID: 15355
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02.prefab:bb49ea15214f8e942abd5a0acc34f0cd");

	// Token: 0x04003BFC RID: 15356
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01.prefab:1bbdd253bbb750744a650a2700c83477");

	// Token: 0x04003BFD RID: 15357
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01.prefab:eed6aad568b49994f9bf9586d892a5d7");

	// Token: 0x04003BFE RID: 15358
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02.prefab:c366fec51d4e1e84e90eefdeea718820");

	// Token: 0x04003BFF RID: 15359
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03.prefab:1f598960da1dc2c4ea8cebdd8212cb2b");

	// Token: 0x04003C00 RID: 15360
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01.prefab:5ee1e8614f9b8dd4cb13259a5bc6fe60");

	// Token: 0x04003C01 RID: 15361
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02.prefab:2ddce4251af42ba48af7da0a1053c204");

	// Token: 0x04003C02 RID: 15362
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03.prefab:b022e45ce90ad39459387cade279ecfa");

	// Token: 0x04003C03 RID: 15363
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01.prefab:e3952b6e2e24c3a49abff1b0fe787061");

	// Token: 0x04003C04 RID: 15364
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01.prefab:7c86c24452e96ad45b62b1986f9beed0");

	// Token: 0x04003C05 RID: 15365
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01.prefab:6e661728092d62948b857a0e3597052b");

	// Token: 0x04003C06 RID: 15366
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeB_01.prefab:026844931acc6da43820b3bdcce51d4e");

	// Token: 0x04003C07 RID: 15367
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_04 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_04.prefab:3e4381fb042bdbb4c942de4834d2c636");

	// Token: 0x04003C08 RID: 15368
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_06 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Malfurion_Mission4Intro_06.prefab:4f32d1fcbef966741944fb3af4d80482");

	// Token: 0x04003C09 RID: 15369
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01.prefab:d20577e581019f54e9fcf7158162c757");

	// Token: 0x04003C0A RID: 15370
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01.prefab:a328ce4f273c4f446813a583f27174bd");

	// Token: 0x04003C0B RID: 15371
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01.prefab:e7ed3771a3d428742939f8e3daf96b70");

	// Token: 0x04003C0C RID: 15372
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_02.prefab:2d952be3a6137ac44bbc077bda581e71");

	// Token: 0x04003C0D RID: 15373
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeA_03.prefab:89433a0909a0e064ab752c6f5d96f6d4");

	// Token: 0x04003C0E RID: 15374
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_01.prefab:7d1cce3d5d500854cb8f1a0eb86ff332");

	// Token: 0x04003C0F RID: 15375
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeE_02.prefab:9300638bb9d17294787e17582679829c");

	// Token: 0x04003C10 RID: 15376
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_01.prefab:9878a7f2bca65f649ad3f3e972fdfe6c");

	// Token: 0x04003C11 RID: 15377
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_03 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4ExchangeF_03.prefab:fad18f4e23144b041bcd7d455af62130");

	// Token: 0x04003C12 RID: 15378
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_02.prefab:3a8572329f45efc4fb1b21b7de229e9e");

	// Token: 0x04003C13 RID: 15379
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_05 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Intro_05.prefab:921ba69bde21815439f81d59336741bf");

	// Token: 0x04003C14 RID: 15380
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission4Victory_01.prefab:fc46b20014f83a74ebba38379a3916e2");

	// Token: 0x04003C15 RID: 15381
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01.prefab:bf1e78dfc4187b94b860298f1f0a46a9");

	// Token: 0x04003C16 RID: 15382
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01.prefab:c08db08b83d3282449ad5cd3fbc52acf");

	// Token: 0x04003C17 RID: 15383
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4ExchangeG_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4ExchangeG_01.prefab:4ae5bd8f271cdba4d899e5bda57fb444");

	// Token: 0x04003C18 RID: 15384
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_03.prefab:d4240388279d751409061fb0e1a986fe");

	// Token: 0x04003C19 RID: 15385
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_07 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Malfurion_Mission4Intro_07.prefab:a5c3a92c803a767438bd5d59859f000e");

	// Token: 0x04003C1A RID: 15386
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission4ThrallHelp_01.prefab:13d7b4dc4dd719f45992532dc0e29c75");

	// Token: 0x04003C1B RID: 15387
	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4ExchangeF_02 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4ExchangeF_02.prefab:328c0843ab357e64fbfaece1fecacbe0");

	// Token: 0x04003C1C RID: 15388
	private static readonly AssetReference VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4Intro_01 = new AssetReference("VO_Story_Minion_Tyrande_Female_NightElf_Story_Malfurion_Mission4Intro_01.prefab:5c9c0033ae0ab384ebb00ea25fc6edc7");

	// Token: 0x04003C1D RID: 15389
	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	// Token: 0x04003C1E RID: 15390
	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	// Token: 0x04003C1F RID: 15391
	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	// Token: 0x04003C20 RID: 15392
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01,
		BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02,
		BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03
	};

	// Token: 0x04003C21 RID: 15393
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01,
		BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02,
		BoH_Malfurion_04.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03
	};

	// Token: 0x04003C22 RID: 15394
	private HashSet<string> m_playedLines = new HashSet<string>();
}
