using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200054B RID: 1355
public class BoH_Uther_02 : BoH_Uther_Dungeon
{
	// Token: 0x06004A96 RID: 19094 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004A97 RID: 19095 RVA: 0x0018C6E8 File Offset: 0x0018A8E8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2EmoteResponse_01,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeC_01,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeF_01,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeF_02,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_01,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_02,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_03,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_01,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_02,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_03,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Intro_01,
			BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Loss_01,
			BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeA_01,
			BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeB_01,
			BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeC_01,
			BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeD_01,
			BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeE_01,
			BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeF_01,
			BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2Victory_01,
			BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2Intro_01,
			BoH_Uther_02.VO_Story_Minion_Terenas_Male_Human_Story_Uther_Mission2Victory_01,
			BoH_Uther_02.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2ExchangeA_01,
			BoH_Uther_02.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2ExchangeB_01,
			BoH_Uther_02.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004A98 RID: 19096 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004A99 RID: 19097 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004A9A RID: 19098 RVA: 0x0018C8CC File Offset: 0x0018AACC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004A9B RID: 19099 RVA: 0x0018C8DB File Offset: 0x0018AADB
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004A9C RID: 19100 RVA: 0x0018C8E3 File Offset: 0x0018AAE3
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004A9D RID: 19101 RVA: 0x0018C8EB File Offset: 0x0018AAEB
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2EmoteResponse_01;
	}

	// Token: 0x06004A9E RID: 19102 RVA: 0x0018C904 File Offset: 0x0018AB04
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

	// Token: 0x06004A9F RID: 19103 RVA: 0x0018C98D File Offset: 0x0018AB8D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 105)
		{
			switch (missionEvent)
			{
			case 501:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(BoH_Uther_02.TuralyonBrassRing, BoH_Uther_02.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2Victory_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Uther_02.TerenasBrassRing, BoH_Uther_02.VO_Story_Minion_Terenas_Male_Human_Story_Uther_Mission2Victory_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2Victory_01, 2.5f);
				GameState.Get().SetBusy(false);
				break;
			case 502:
				yield return base.PlayLineOnlyOnce(friendlyActor, BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeE_01, 2.5f);
				break;
			case 503:
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeF_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeF_02, 2.5f);
				GameState.Get().SetBusy(false);
				break;
			case 504:
				yield return base.PlayLineAlways(enemyActor, BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Loss_01, 2.5f);
				break;
			default:
				yield return base.HandleMissionEventWithTiming(missionEvent);
				break;
			}
		}
		else
		{
			yield return base.PlayLineAlways(BoH_Uther_02.TuralyonBrassRing, BoH_Uther_02.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeB_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004AA0 RID: 19104 RVA: 0x0018C9A3 File Offset: 0x0018ABA3
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

	// Token: 0x06004AA1 RID: 19105 RVA: 0x0018C9B9 File Offset: 0x0018ABB9
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

	// Token: 0x06004AA2 RID: 19106 RVA: 0x0018C9CF File Offset: 0x0018ABCF
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
			if (turn != 3)
			{
				if (turn == 9)
				{
					yield return base.PlayLineAlways(actor, BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(BoH_Uther_02.TuralyonBrassRing, BoH_Uther_02.VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2ExchangeA_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeA_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_02.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeD_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004AA3 RID: 19107 RVA: 0x0017D1BC File Offset: 0x0017B3BC
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BRMAdventure);
	}

	// Token: 0x04003EEB RID: 16107
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Uther_02.InitBooleanOptions();

	// Token: 0x04003EEC RID: 16108
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2EmoteResponse_01.prefab:e99fa9cff27558d47bae5396f280ec76");

	// Token: 0x04003EED RID: 16109
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeC_01.prefab:5f009be2a90c50246bf32055d555aad1");

	// Token: 0x04003EEE RID: 16110
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeF_01 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeF_01.prefab:6e75ca36e0f86bd438b50d62de39d241");

	// Token: 0x04003EEF RID: 16111
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeF_02 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2ExchangeF_02.prefab:cdf47d0e8f620ec4ca7d6d861e900754");

	// Token: 0x04003EF0 RID: 16112
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_01.prefab:a5ceb6e78bf885c4ab90a0f15c672e6c");

	// Token: 0x04003EF1 RID: 16113
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_02.prefab:198f953aeaaf2fd448958156bd462ebd");

	// Token: 0x04003EF2 RID: 16114
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_03.prefab:bcaf97cd0b101b84792ffa928b163ca5");

	// Token: 0x04003EF3 RID: 16115
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_01.prefab:29455d5e67785ef46962aa3e9ad5e20a");

	// Token: 0x04003EF4 RID: 16116
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_02.prefab:b813617484c062d428e14297c48a0be4");

	// Token: 0x04003EF5 RID: 16117
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_03.prefab:23608b80c9efffb4698596d449549941");

	// Token: 0x04003EF6 RID: 16118
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Loss_01.prefab:77305932981d4884cbd2c916322470ef");

	// Token: 0x04003EF7 RID: 16119
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeA_01.prefab:705945e31c5b2534cb667afe73bb96b7");

	// Token: 0x04003EF8 RID: 16120
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeB_01.prefab:206092eb6790cb5408b9c694176c2ed6");

	// Token: 0x04003EF9 RID: 16121
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeC_01.prefab:1c27797e5d3f0e7478b6dcb140f4c84c");

	// Token: 0x04003EFA RID: 16122
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeD_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeD_01.prefab:129cfddffac2b0b4cad6156153712af6");

	// Token: 0x04003EFB RID: 16123
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeE_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeE_01.prefab:7eadab07c7c728a45a1a538d0236cc78");

	// Token: 0x04003EFC RID: 16124
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeF_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2ExchangeF_01.prefab:f4e3ef05bdce73c438ec4a22465f7180");

	// Token: 0x04003EFD RID: 16125
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2Victory_01.prefab:f43a6f83dc1bdfd4e914b19b1f668d00");

	// Token: 0x04003EFE RID: 16126
	private static readonly AssetReference VO_Story_Minion_Terenas_Male_Human_Story_Uther_Mission2Victory_01 = new AssetReference("VO_Story_Minion_Terenas_Male_Human_Story_Uther_Mission2Victory_01.prefab:d8558cd406d23df4c9143c3b06ee5354");

	// Token: 0x04003EFF RID: 16127
	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2ExchangeA_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2ExchangeA_01.prefab:2e1c8b7265a9db244b0a71cefc95b28d");

	// Token: 0x04003F00 RID: 16128
	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2ExchangeB_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2ExchangeB_01.prefab:b623dc89eeff8ac478c6f36df301ef24");

	// Token: 0x04003F01 RID: 16129
	private static readonly AssetReference VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2Victory_01 = new AssetReference("VO_Story_Minion_Turalyon_Male_Human_Story_Uther_Mission2Victory_01.prefab:dce4d42c4936af84a9047c21e348e2ca");

	// Token: 0x04003F02 RID: 16130
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission2Intro_01.prefab:a027b0c170c35d9499f42984c238a427");

	// Token: 0x04003F03 RID: 16131
	private static readonly AssetReference VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Intro_01.prefab:0461a0b66c59be0458fc5397fb94a7b4");

	// Token: 0x04003F04 RID: 16132
	public static readonly AssetReference TerenasBrassRing = new AssetReference("Terenas_BrassRing_Quote.prefab:b640b1fdb81ce4942979cf91f8255eb1");

	// Token: 0x04003F05 RID: 16133
	public static readonly AssetReference TuralyonBrassRing = new AssetReference("Turalyon_BrassRing_Quote.prefab:40afbe0d5b4da0643baf2ebf5756548d");

	// Token: 0x04003F06 RID: 16134
	private List<string> m_HeroPowerLines = new List<string>
	{
		BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_01,
		BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_02,
		BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2HeroPower_03
	};

	// Token: 0x04003F07 RID: 16135
	private List<string> m_IdleLines = new List<string>
	{
		BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_01,
		BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_02,
		BoH_Uther_02.VO_Story_Hero_Orgrim_Male_Orc_Story_Uther_Mission2Idle_03
	};

	// Token: 0x04003F08 RID: 16136
	private HashSet<string> m_playedLines = new HashSet<string>();
}
