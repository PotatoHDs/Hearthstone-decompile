using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000550 RID: 1360
public class BoH_Uther_07 : BoH_Uther_Dungeon
{
	// Token: 0x06004AF4 RID: 19188 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004AF5 RID: 19189 RVA: 0x0018DDB4 File Offset: 0x0018BFB4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Uther_07.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_01,
			BoH_Uther_07.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_02,
			BoH_Uther_07.VO_Story_Hero_Jaina_Female_Human_Story_Uther_Mission7Victory_03,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7EmoteResponse_01,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeA_01,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeB_01,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeC_01,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_01,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_02,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_03,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_01,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_02,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_03,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_01,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_02,
			BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Loss_01,
			BoH_Uther_07.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeA_01,
			BoH_Uther_07.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeB_01,
			BoH_Uther_07.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_02,
			BoH_Uther_07.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_04,
			BoH_Uther_07.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Intro_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004AF6 RID: 19190 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004AF7 RID: 19191 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004AF8 RID: 19192 RVA: 0x0018DF68 File Offset: 0x0018C168
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(enemyActor, BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Uther_07.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_02, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004AF9 RID: 19193 RVA: 0x0018DF77 File Offset: 0x0018C177
	public override List<string> GetIdleLines()
	{
		return this.m_IdleLines;
	}

	// Token: 0x06004AFA RID: 19194 RVA: 0x0018DF7F File Offset: 0x0018C17F
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_HeroPowerLines;
	}

	// Token: 0x06004AFB RID: 19195 RVA: 0x0018DF87 File Offset: 0x0018C187
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7EmoteResponse_01;
	}

	// Token: 0x06004AFC RID: 19196 RVA: 0x0018DFA0 File Offset: 0x0018C1A0
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

	// Token: 0x06004AFD RID: 19197 RVA: 0x0018E029 File Offset: 0x0018C229
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
				yield return base.PlayLineAlways(actor, BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Loss_01, 2.5f);
			}
			else
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(BoH_Uther_07.ArthasBrassRing, BoH_Uther_07.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_07.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_02, 2.5f);
			yield return base.PlayLineAlways(BoH_Uther_07.ArthasBrassRing, BoH_Uther_07.VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_02, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_07.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_04, 2.5f);
			yield return base.PlayLineAlways(BoH_Uther_07.JainaBrassRing, BoH_Uther_07.VO_Story_Hero_Jaina_Female_Human_Story_Uther_Mission7Victory_03, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004AFE RID: 19198 RVA: 0x0018E03F File Offset: 0x0018C23F
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

	// Token: 0x06004AFF RID: 19199 RVA: 0x0018E055 File Offset: 0x0018C255
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

	// Token: 0x06004B00 RID: 19200 RVA: 0x0018E06B File Offset: 0x0018C26B
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
				if (turn == 12)
				{
					yield return base.PlayLineAlways(actor, BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Uther_07.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Uther_07.VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004B01 RID: 19201 RVA: 0x0016110D File Offset: 0x0015F30D
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}

	// Token: 0x04003F66 RID: 16230
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Uther_07.InitBooleanOptions();

	// Token: 0x04003F67 RID: 16231
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_01.prefab:338c563ff80f6ba43b049d188281b13c");

	// Token: 0x04003F68 RID: 16232
	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission7Victory_02.prefab:0ab659ff0a0232244ac63a1d3289ca18");

	// Token: 0x04003F69 RID: 16233
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Uther_Mission7Victory_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Uther_Mission7Victory_03.prefab:13b046e0cd1aad348b3540958ca141b3");

	// Token: 0x04003F6A RID: 16234
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7EmoteResponse_01.prefab:af9a7c1b9fbe4ee4b988cec78c45b164");

	// Token: 0x04003F6B RID: 16235
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeA_01.prefab:c9601bd05b1885b448f7316ed4644095");

	// Token: 0x04003F6C RID: 16236
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeB_01.prefab:e743c19f6a349bf47aa975a1558551bb");

	// Token: 0x04003F6D RID: 16237
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeC_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7ExchangeC_01.prefab:909ff51ac63194b4b8554bd8bd45d1e4");

	// Token: 0x04003F6E RID: 16238
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_01.prefab:ab2b1ab368a3ac744a6ced0488ffe589");

	// Token: 0x04003F6F RID: 16239
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_02.prefab:d7b20f10c964cd641a0fedb34b7a966e");

	// Token: 0x04003F70 RID: 16240
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_03.prefab:ebcffde9dfc93a448aed34359f97e770");

	// Token: 0x04003F71 RID: 16241
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_01.prefab:b594a43bba164e84a9cfb527412d6d73");

	// Token: 0x04003F72 RID: 16242
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_02 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_02.prefab:6228abd6176151b4880c880d86944086");

	// Token: 0x04003F73 RID: 16243
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_03 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_03.prefab:3603a30ac8ae0b1499e5fc393c786082");

	// Token: 0x04003F74 RID: 16244
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Loss_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Loss_01.prefab:5b959a6f3693532458355a428ad00a10");

	// Token: 0x04003F75 RID: 16245
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeA_01.prefab:6de5ab34a1457d641ad5a8c4c0d54446");

	// Token: 0x04003F76 RID: 16246
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7ExchangeB_01.prefab:5f58f0b33877f8c42956366461eee5ef");

	// Token: 0x04003F77 RID: 16247
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_02.prefab:e1ffbd73a32113943b73b413fe230366");

	// Token: 0x04003F78 RID: 16248
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_04 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Victory_04.prefab:608d3f5052fce9742bb46dcf0dc1f560");

	// Token: 0x04003F79 RID: 16249
	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission7Intro_01.prefab:817593da4dea5ce4ab755a16d4bf7a31");

	// Token: 0x04003F7A RID: 16250
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_01 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_01.prefab:96e13fc13d48aa147961633212885376");

	// Token: 0x04003F7B RID: 16251
	private static readonly AssetReference VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_02 = new AssetReference("VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Intro_02.prefab:4eebb5ccc0b19654ca84ea9e0bb7a58c");

	// Token: 0x04003F7C RID: 16252
	public static readonly AssetReference ArthasBrassRing = new AssetReference("Arthas_BrassRing_Quote.prefab:49bb0ac905ae3c54cbf3624451b62ab4");

	// Token: 0x04003F7D RID: 16253
	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	// Token: 0x04003F7E RID: 16254
	private List<string> m_HeroPowerLines = new List<string>
	{
		BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_01,
		BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_02,
		BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7HeroPower_03
	};

	// Token: 0x04003F7F RID: 16255
	private List<string> m_IdleLines = new List<string>
	{
		BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_01,
		BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_02,
		BoH_Uther_07.VO_Story_Hero_MalGanis_Male_Demon_Story_Uther_Mission7Idle_03
	};

	// Token: 0x04003F80 RID: 16256
	private HashSet<string> m_playedLines = new HashSet<string>();
}
