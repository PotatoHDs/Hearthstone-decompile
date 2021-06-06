using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000544 RID: 1348
public class BoH_Thrall_05 : BoH_Thrall_Dungeon
{
	// Token: 0x060049F5 RID: 18933 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060049F6 RID: 18934 RVA: 0x0018A224 File Offset: 0x00188424
	public BoH_Thrall_05()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Thrall_05.s_booleanOptions);
	}

	// Token: 0x060049F7 RID: 18935 RVA: 0x0018A2C8 File Offset: 0x001884C8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01,
			BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01,
			BoH_Thrall_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01,
			BoH_Thrall_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01,
			BoH_Thrall_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01,
			BoH_Thrall_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01,
			BoH_Thrall_05.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01,
			BoH_Thrall_05.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01,
			BoH_Thrall_05.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01,
			BoH_Thrall_05.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5ExchangeG_01,
			BoH_Thrall_05.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5Intro_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060049F8 RID: 18936 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060049F9 RID: 18937 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060049FA RID: 18938 RVA: 0x0018A48C File Offset: 0x0018868C
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01);
		yield return base.MissionPlayVO(BoH_Thrall_05.JainaBrassRing, BoH_Thrall_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Thrall_05.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5Intro_03);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060049FB RID: 18939 RVA: 0x0018A49B File Offset: 0x0018869B
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x060049FC RID: 18940 RVA: 0x0018A4A3 File Offset: 0x001886A3
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x060049FD RID: 18941 RVA: 0x0018A4AB File Offset: 0x001886AB
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DRGLOEBoss;
		this.m_standardEmoteResponseLine = BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01;
	}

	// Token: 0x060049FE RID: 18942 RVA: 0x0018A4D0 File Offset: 0x001886D0
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

	// Token: 0x060049FF RID: 18943 RVA: 0x0018A554 File Offset: 0x00188754
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
			if (missionEvent != 504)
			{
				if (missionEvent == 507)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(actor, BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01, 2.5f);
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
				yield return base.PlayLineAlways(BoH_Thrall_05.JainaBrassRing, BoH_Thrall_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_05.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(friendlyActor, BoH_Thrall_05.VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5ExchangeG_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004A00 RID: 18944 RVA: 0x0018A56A File Offset: 0x0018876A
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

	// Token: 0x06004A01 RID: 18945 RVA: 0x0018A580 File Offset: 0x00188780
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

	// Token: 0x06004A02 RID: 18946 RVA: 0x0018A596 File Offset: 0x00188796
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 5)
			{
				if (turn == 8)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02, 2.5f);
				yield return base.PlayLineAlways(BoH_Thrall_05.JainaBrassRing, BoH_Thrall_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Thrall_05.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003E1E RID: 15902
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Thrall_05.InitBooleanOptions();

	// Token: 0x04003E1F RID: 15903
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01.prefab:c202f9e018853d34aa7cd3b170bbdbe8");

	// Token: 0x04003E20 RID: 15904
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01.prefab:4f16ea7a791dd2840b821e43c4b42fff");

	// Token: 0x04003E21 RID: 15905
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01.prefab:4a291c7eb5b14a746bbc1003174a6f21");

	// Token: 0x04003E22 RID: 15906
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02.prefab:f99c12dbfd44d0a4298953bdad0858f0");

	// Token: 0x04003E23 RID: 15907
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01.prefab:3f82a78d60a4f6a478151160c9f7c69d");

	// Token: 0x04003E24 RID: 15908
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01.prefab:9bafc7ca0daed364a924acf2c3a285c8");

	// Token: 0x04003E25 RID: 15909
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02.prefab:f14e857b02ace0a4c9e5e90079d98cf1");

	// Token: 0x04003E26 RID: 15910
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03.prefab:e66307477c710aa40b5282cee8c08daa");

	// Token: 0x04003E27 RID: 15911
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01.prefab:328671bb439191b4083ac5f1bd275d26");

	// Token: 0x04003E28 RID: 15912
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02.prefab:3e5cc1ae68fb70f45b480e616e9704a4");

	// Token: 0x04003E29 RID: 15913
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03.prefab:c2e738e549e1f964ab6e2d8167212a7c");

	// Token: 0x04003E2A RID: 15914
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01.prefab:cb62db362889df3439c0f8763137926c");

	// Token: 0x04003E2B RID: 15915
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01.prefab:1c2c7ae0fa332894ba11a77fcfa2d13c");

	// Token: 0x04003E2C RID: 15916
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01.prefab:88af02a07f66f23489dc599a3f77c402");

	// Token: 0x04003E2D RID: 15917
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01.prefab:db2b6786a83c30c4bab684c475433d2d");

	// Token: 0x04003E2E RID: 15918
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01.prefab:97ba31a31434a1c48be3e4572b5ae710");

	// Token: 0x04003E2F RID: 15919
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01.prefab:bc1fd3a3f7bc9e5498839203a0e16bcc");

	// Token: 0x04003E30 RID: 15920
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01.prefab:5a4f9acbc7645df4f901722085f27e05");

	// Token: 0x04003E31 RID: 15921
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01.prefab:2c88f209293c21243a61b4655f8a5176");

	// Token: 0x04003E32 RID: 15922
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5ExchangeG_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5ExchangeG_01.prefab:e5d6ea3ae3bd52649aaa5494bddde917");

	// Token: 0x04003E33 RID: 15923
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5Intro_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission5Intro_03.prefab:6d4d1d899cdc9364f88de7f99f77fa7c");

	// Token: 0x04003E34 RID: 15924
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission5ExchangeA_01.prefab:2f10ce18f13940249bec390be8571972");

	// Token: 0x04003E35 RID: 15925
	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	// Token: 0x04003E36 RID: 15926
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01,
		BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02,
		BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03
	};

	// Token: 0x04003E37 RID: 15927
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01,
		BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02,
		BoH_Thrall_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03
	};

	// Token: 0x04003E38 RID: 15928
	private HashSet<string> m_playedLines = new HashSet<string>();
}
