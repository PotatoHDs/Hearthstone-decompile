using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000543 RID: 1347
public class BoH_Thrall_04 : BoH_Thrall_Dungeon
{
	// Token: 0x060049E3 RID: 18915 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060049E4 RID: 18916 RVA: 0x00189DB4 File Offset: 0x00187FB4
	public BoH_Thrall_04()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Thrall_04.s_booleanOptions);
	}

	// Token: 0x060049E5 RID: 18917 RVA: 0x00189E58 File Offset: 0x00188058
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4EmoteResponse_01,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeA_01,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_01,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_02,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeC_01,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_01,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_02,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_03,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_01,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_02,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_03,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Intro_01,
			BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Loss_01,
			BoH_Thrall_04.VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01,
			BoH_Thrall_04.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4ExchangeC_02,
			BoH_Thrall_04.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Intro_02,
			BoH_Thrall_04.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Victory_01,
			BoH_Thrall_04.VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060049E6 RID: 18918 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060049E7 RID: 18919 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060049E8 RID: 18920 RVA: 0x00189FDC File Offset: 0x001881DC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Thrall_04.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060049E9 RID: 18921 RVA: 0x00189FEB File Offset: 0x001881EB
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x060049EA RID: 18922 RVA: 0x00189FF3 File Offset: 0x001881F3
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x060049EB RID: 18923 RVA: 0x00189FFB File Offset: 0x001881FB
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BRMAdventure;
		this.m_standardEmoteResponseLine = BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4EmoteResponse_01;
	}

	// Token: 0x060049EC RID: 18924 RVA: 0x0018A020 File Offset: 0x00188220
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

	// Token: 0x060049ED RID: 18925 RVA: 0x0018A0A4 File Offset: 0x001882A4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 504)
		{
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Loss_01, 2.5f);
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
			yield return base.PlayLineAlways(actor2, BoH_Thrall_04.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060049EE RID: 18926 RVA: 0x0018A0BA File Offset: 0x001882BA
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

	// Token: 0x060049EF RID: 18927 RVA: 0x0018A0D0 File Offset: 0x001882D0
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

	// Token: 0x060049F0 RID: 18928 RVA: 0x0018A0E6 File Offset: 0x001882E6
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
				if (turn == 7)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_04.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4ExchangeC_02, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_02, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003E08 RID: 15880
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Thrall_04.InitBooleanOptions();

	// Token: 0x04003E09 RID: 15881
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4EmoteResponse_01.prefab:4c0b63c39d320dc4b98e6c2534e6ae99");

	// Token: 0x04003E0A RID: 15882
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeA_01.prefab:2f36e1d15baa3494c99f76f99fff44aa");

	// Token: 0x04003E0B RID: 15883
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_01.prefab:239abdaddda43a44a94a635afadd2916");

	// Token: 0x04003E0C RID: 15884
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_02 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_02.prefab:a042711cd858b6847b2c4d8b14b2397b");

	// Token: 0x04003E0D RID: 15885
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeC_01.prefab:89a0644c39f524246bebb6071db02a15");

	// Token: 0x04003E0E RID: 15886
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_01.prefab:457bc30a75a8472f994825bfe91016a8");

	// Token: 0x04003E0F RID: 15887
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_02.prefab:e6fa581502262834b9cc4f70ecf089ce");

	// Token: 0x04003E10 RID: 15888
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_03 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_03.prefab:4acc9795798cb8845baae85e506e5380");

	// Token: 0x04003E11 RID: 15889
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_01.prefab:f9c85bf0404b33b4298054b731d0d348");

	// Token: 0x04003E12 RID: 15890
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_02 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_02.prefab:8044ee403a328e84fad151cf340ff56a");

	// Token: 0x04003E13 RID: 15891
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_03 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_03.prefab:36915d97e6c75b24ea3969c76b33e4de");

	// Token: 0x04003E14 RID: 15892
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Intro_01.prefab:ac1859af0ade3ef4a8e161e60be20c5e");

	// Token: 0x04003E15 RID: 15893
	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Loss_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Loss_01.prefab:ec89c713c17fa70498f3f574bc130f55");

	// Token: 0x04003E16 RID: 15894
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4ExchangeC_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4ExchangeC_02.prefab:501b6ffbf23dad942bbadc35b9619169");

	// Token: 0x04003E17 RID: 15895
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Intro_02.prefab:d0f98203b90be504385fba07ca2e3d1b");

	// Token: 0x04003E18 RID: 15896
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Victory_01.prefab:e047763d128221c44b4606752da2fa28");

	// Token: 0x04003E19 RID: 15897
	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01.prefab:a47ee3092fc3c9e4ea752540df8038ef");

	// Token: 0x04003E1A RID: 15898
	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_02.prefab:053e818ca35b1ed46aaaa876fe8eee53");

	// Token: 0x04003E1B RID: 15899
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_01,
		BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_02,
		BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_03
	};

	// Token: 0x04003E1C RID: 15900
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_01,
		BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_02,
		BoH_Thrall_04.VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_03
	};

	// Token: 0x04003E1D RID: 15901
	private HashSet<string> m_playedLines = new HashSet<string>();
}
