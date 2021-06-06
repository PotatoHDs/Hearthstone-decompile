using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200054C RID: 1356
public class BoH_Uther_03 : BoH_Uther_Dungeon
{
	// Token: 0x06004AA9 RID: 19113 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004AAA RID: 19114 RVA: 0x0018CC1C File Offset: 0x0018AE1C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Uther_03.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeA_01,
			BoH_Uther_03.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeB_01,
			BoH_Uther_03.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeC_01,
			BoH_Uther_03.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3Intro_01,
			BoH_Uther_03.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3Victory_01,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeA_01,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeB_01,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeC_01,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_01,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_03,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Loss_01,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Victory_01,
			BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004AAB RID: 19115 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004AAC RID: 19116 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004AAD RID: 19117 RVA: 0x0018CDA0 File Offset: 0x0018AFA0
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Uther_03.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004AAE RID: 19118 RVA: 0x0018CDAF File Offset: 0x0018AFAF
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004AAF RID: 19119 RVA: 0x0018CDB7 File Offset: 0x0018AFB7
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004AB0 RID: 19120 RVA: 0x0018CDBF File Offset: 0x0018AFBF
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01;
	}

	// Token: 0x06004AB1 RID: 19121 RVA: 0x0018CDD8 File Offset: 0x0018AFD8
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

	// Token: 0x06004AB2 RID: 19122 RVA: 0x0018CE61 File Offset: 0x0018B061
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 501)
		{
			if (missionEvent == 504)
			{
				yield return base.PlayLineAlways(actor, BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Loss_01, 2.5f);
			}
			else
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_03.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3Victory_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004AB3 RID: 19123 RVA: 0x0018CE77 File Offset: 0x0018B077
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

	// Token: 0x06004AB4 RID: 19124 RVA: 0x0018CE8D File Offset: 0x0018B08D
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

	// Token: 0x06004AB5 RID: 19125 RVA: 0x0018CEA3 File Offset: 0x0018B0A3
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 5)
		{
			if (turn != 7)
			{
				if (turn == 11)
				{
					yield return base.PlayLineAlways(enemyActor, BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Uther_03.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Uther_03.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_03.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004AB6 RID: 19126 RVA: 0x0017D1BC File Offset: 0x0017B3BC
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BRMAdventure);
	}

	// Token: 0x04003F09 RID: 16137
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Uther_03.InitBooleanOptions();

	// Token: 0x04003F0A RID: 16138
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeA_01.prefab:9a4fae4ac32fb0b46b16eba1b2ebdc86");

	// Token: 0x04003F0B RID: 16139
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeB_01.prefab:78c46583c71beb746b1e8e104c43f9eb");

	// Token: 0x04003F0C RID: 16140
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3ExchangeC_01.prefab:e01cb8c967c195548b8cf5e7bde23e5b");

	// Token: 0x04003F0D RID: 16141
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3Victory_01.prefab:1556c58b0e16e3a449c092853a1dddb6");

	// Token: 0x04003F0E RID: 16142
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3EmoteResponse_01.prefab:04487e78b0fbcbd48a6f47f4b10b2659");

	// Token: 0x04003F0F RID: 16143
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeA_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeA_01.prefab:93d30bf1ea7f890439e39667336e069a");

	// Token: 0x04003F10 RID: 16144
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeB_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeB_01.prefab:8e5a52dcbadfe334cb54ab8df1a4c314");

	// Token: 0x04003F11 RID: 16145
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeC_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3ExchangeC_01.prefab:d403b61cdae436340b0f899996472321");

	// Token: 0x04003F12 RID: 16146
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01.prefab:c3624cd3f84813d40a351159ae5afa67");

	// Token: 0x04003F13 RID: 16147
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02.prefab:36af4d8ed0b262f48a43e64f3adbe5ab");

	// Token: 0x04003F14 RID: 16148
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03.prefab:f29115de713965447b4e0faa02bd07dc");

	// Token: 0x04003F15 RID: 16149
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_01.prefab:b347788f2de75e1419ef935427fba50f");

	// Token: 0x04003F16 RID: 16150
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02.prefab:eee9893ee5ed9e04c906fd42bc42e5fb");

	// Token: 0x04003F17 RID: 16151
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_03 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_03.prefab:3004a634f1bf79045a32ec76ab637f7a");

	// Token: 0x04003F18 RID: 16152
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Loss_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Loss_01.prefab:5f6dde007919868458e07991e8515ad1");

	// Token: 0x04003F19 RID: 16153
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Victory_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Victory_01.prefab:bb8152842925e054bb6bb24f897595bf");

	// Token: 0x04003F1A RID: 16154
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission3Intro_01.prefab:e0cf704594324854f88cbc1707377ee5");

	// Token: 0x04003F1B RID: 16155
	private static readonly AssetReference VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Intro_01 = new AssetReference("VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Intro_01.prefab:d690374e721eff644acfc5b0d3b2360d");

	// Token: 0x04003F1C RID: 16156
	private List<string> m_HeroPowerLines = new List<string>
	{
		BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_01,
		BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_02,
		BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3HeroPower_03
	};

	// Token: 0x04003F1D RID: 16157
	private List<string> m_IdleLines = new List<string>
	{
		BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_01,
		BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_02,
		BoH_Uther_03.VO_Story_Minion_Varok_Male_Orc_Story_Uther_Mission3Idle_03
	};

	// Token: 0x04003F1E RID: 16158
	private HashSet<string> m_playedLines = new HashSet<string>();
}
