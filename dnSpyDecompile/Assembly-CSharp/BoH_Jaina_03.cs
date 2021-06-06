using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000524 RID: 1316
public class BoH_Jaina_03 : BoH_Jaina_Dungeon
{
	// Token: 0x06004778 RID: 18296 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004779 RID: 18297 RVA: 0x0017FFA4 File Offset: 0x0017E1A4
	public BoH_Jaina_03()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Jaina_03.s_booleanOptions);
	}

	// Token: 0x0600477A RID: 18298 RVA: 0x00180048 File Offset: 0x0017E248
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3EmoteResponse_01,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeA_01,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeB_01,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeD_01,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_01,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_02,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_03,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_01,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_02,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_03,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Intro_01,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Loss_01,
			BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Victory_01,
			BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeA_01,
			BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeB_01,
			BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeC_01,
			BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeD_01,
			BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Intro_01,
			BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600477B RID: 18299 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600477C RID: 18300 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600477D RID: 18301 RVA: 0x001801DC File Offset: 0x0017E3DC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600477E RID: 18302 RVA: 0x001801EB File Offset: 0x0017E3EB
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3IdleLines;
	}

	// Token: 0x0600477F RID: 18303 RVA: 0x001801F3 File Offset: 0x0017E3F3
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPowerLines;
	}

	// Token: 0x06004780 RID: 18304 RVA: 0x001801FC File Offset: 0x0017E3FC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004781 RID: 18305 RVA: 0x0018025A File Offset: 0x0017E45A
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
			if (missionEvent != 504)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004782 RID: 18306 RVA: 0x00180270 File Offset: 0x0017E470
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

	// Token: 0x06004783 RID: 18307 RVA: 0x00180286 File Offset: 0x0017E486
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

	// Token: 0x06004784 RID: 18308 RVA: 0x0018029C File Offset: 0x0017E49C
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
			switch (turn)
			{
			case 5:
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeB_01, 2.5f);
				break;
			case 7:
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeC_01, 2.5f);
				break;
			case 9:
				yield return base.PlayLineAlways(enemyActor, BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeD_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeD_01, 2.5f);
				break;
			}
		}
		else
		{
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_03.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004785 RID: 18309 RVA: 0x0011204E File Offset: 0x0011024E
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICC);
	}

	// Token: 0x04003AEE RID: 15086
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Jaina_03.InitBooleanOptions();

	// Token: 0x04003AEF RID: 15087
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3EmoteResponse_01.prefab:97f6c76ca47678c4eb8b5304572c75c2");

	// Token: 0x04003AF0 RID: 15088
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeA_01.prefab:c5399267f9d5bcc48a4647db9089d1a3");

	// Token: 0x04003AF1 RID: 15089
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeB_01.prefab:602d0ae13ff1c7941b71ef8ebd20c2b7");

	// Token: 0x04003AF2 RID: 15090
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3ExchangeD_01.prefab:54890be114137604996e1b2335a0f06c");

	// Token: 0x04003AF3 RID: 15091
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_01.prefab:c0202084e1d394f4d891f23137f7bfbf");

	// Token: 0x04003AF4 RID: 15092
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_02.prefab:649e928ba2cee174aa61cf99d3dede64");

	// Token: 0x04003AF5 RID: 15093
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_03.prefab:b40bbb664d4412c4482c040c91ef3093");

	// Token: 0x04003AF6 RID: 15094
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_01.prefab:794a6e637b82eb5428544728d95cca63");

	// Token: 0x04003AF7 RID: 15095
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_02.prefab:e9af642ef73b2994d83368f20783cbe9");

	// Token: 0x04003AF8 RID: 15096
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_03.prefab:f1974c03c2f627e46b9e83078418cb42");

	// Token: 0x04003AF9 RID: 15097
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Intro_01.prefab:c7b5889aed22f25418da2be0a749e04a");

	// Token: 0x04003AFA RID: 15098
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Loss_01.prefab:0c15b8bbeb2d178479bcdb854bc4a95c");

	// Token: 0x04003AFB RID: 15099
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Victory_01.prefab:5fbc1a1b366dd9946802bbabdc6b690e");

	// Token: 0x04003AFC RID: 15100
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeA_01.prefab:e4472d6be29dc0045b68d11327fae335");

	// Token: 0x04003AFD RID: 15101
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeB_01.prefab:23881a87aff08d84b867eaf06aea7cfd");

	// Token: 0x04003AFE RID: 15102
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeC_01.prefab:71f4e5216ff00034aad6ac5943ca10fb");

	// Token: 0x04003AFF RID: 15103
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3ExchangeD_01.prefab:8adbcfc3ff610bd4bb963d8eb0db62e7");

	// Token: 0x04003B00 RID: 15104
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Intro_01.prefab:9f9a7586ab9b74941aeeca903c05af39");

	// Token: 0x04003B01 RID: 15105
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission3Victory_01.prefab:629b5af6a65213b4a8f7794dcdd62a54");

	// Token: 0x04003B02 RID: 15106
	private List<string> m_VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPowerLines = new List<string>
	{
		BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_01,
		BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_02,
		BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3HeroPower_03
	};

	// Token: 0x04003B03 RID: 15107
	private List<string> m_VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3IdleLines = new List<string>
	{
		BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_01,
		BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_02,
		BoH_Jaina_03.VO_Story_Hero_Arthas_Male_Human_Story_Jaina_Mission3Idle_03
	};

	// Token: 0x04003B04 RID: 15108
	private HashSet<string> m_playedLines = new HashSet<string>();
}
