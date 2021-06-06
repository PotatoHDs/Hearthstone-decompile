using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000526 RID: 1318
public class BoH_Jaina_05 : BoH_Jaina_Dungeon
{
	// Token: 0x0600479C RID: 18332 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600479D RID: 18333 RVA: 0x00180914 File Offset: 0x0017EB14
	public BoH_Jaina_05()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Jaina_05.s_booleanOptions);
	}

	// Token: 0x0600479E RID: 18334 RVA: 0x001809B8 File Offset: 0x0017EBB8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01,
			BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01,
			BoH_Jaina_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01,
			BoH_Jaina_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01,
			BoH_Jaina_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01,
			BoH_Jaina_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01,
			BoH_Jaina_05.VO_Story_Hero_Malfurion_Male_NightElf_Story_Jaina_Mission5Malfurion_01,
			BoH_Jaina_05.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01,
			BoH_Jaina_05.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01,
			BoH_Jaina_05.VO_Story_Hero_Tyrande_Female_NightElf_Story_Jaina_Mission5Tyrande_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600479F RID: 18335 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060047A0 RID: 18336 RVA: 0x00180B6C File Offset: 0x0017ED6C
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060047A1 RID: 18337 RVA: 0x00180B7B File Offset: 0x0017ED7B
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5IdleLines;
	}

	// Token: 0x060047A2 RID: 18338 RVA: 0x00180B83 File Offset: 0x0017ED83
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPowerLines;
	}

	// Token: 0x060047A3 RID: 18339 RVA: 0x00180B8B File Offset: 0x0017ED8B
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01;
	}

	// Token: 0x060047A4 RID: 18340 RVA: 0x00180BA4 File Offset: 0x0017EDA4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060047A5 RID: 18341 RVA: 0x00180C2C File Offset: 0x0017EE2C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 501:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(BoH_Jaina_05.ThrallBrassRing, BoH_Jaina_05.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 502:
			yield return base.PlayLineAlways(BoH_Jaina_05.MalfurionBrassRing, BoH_Jaina_05.VO_Story_Hero_Malfurion_Male_NightElf_Story_Jaina_Mission5Malfurion_01, 2.5f);
			break;
		case 503:
			yield return base.PlayLineAlways(BoH_Jaina_05.TyrandeBrassRing, BoH_Jaina_05.VO_Story_Hero_Tyrande_Female_NightElf_Story_Jaina_Mission5Tyrande_01, 2.5f);
			break;
		case 504:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01, 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 505:
			yield return base.PlayLineAlways(BoH_Jaina_05.ThrallBrassRing, BoH_Jaina_05.VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01, 2.5f);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x060047A6 RID: 18342 RVA: 0x00180C42 File Offset: 0x0017EE42
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

	// Token: 0x060047A7 RID: 18343 RVA: 0x00180C58 File Offset: 0x0017EE58
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

	// Token: 0x060047A8 RID: 18344 RVA: 0x00180C6E File Offset: 0x0017EE6E
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn == 7)
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Jaina_05.VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060047A9 RID: 18345 RVA: 0x0016BFCC File Offset: 0x0016A1CC
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRGLOEBoss);
	}

	// Token: 0x04003B21 RID: 15137
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Jaina_05.InitBooleanOptions();

	// Token: 0x04003B22 RID: 15138
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Death_01.prefab:7a57a90fef864a748816495f7847a00f");

	// Token: 0x04003B23 RID: 15139
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5EmoteResponse_01.prefab:e1cda8373702d1049bfd320bea8d1eca");

	// Token: 0x04003B24 RID: 15140
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_01.prefab:220ba39a782dcac4a84ab439784f1be5");

	// Token: 0x04003B25 RID: 15141
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeA_02.prefab:ec7fc41e6a9dbc24587a38a6cf439ddd");

	// Token: 0x04003B26 RID: 15142
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5ExchangeB_01.prefab:730aae8456054c8428eee28a3d1cab85");

	// Token: 0x04003B27 RID: 15143
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01.prefab:2c1dc3f5ecc665a498e2afaa600e73d7");

	// Token: 0x04003B28 RID: 15144
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02.prefab:14668126189787347852cc18bce4ddbf");

	// Token: 0x04003B29 RID: 15145
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03.prefab:c1ba5dfdd9ade824ab49ae8066d2b87b");

	// Token: 0x04003B2A RID: 15146
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01.prefab:ab6fed133f9cb6b44b208a698d744ae5");

	// Token: 0x04003B2B RID: 15147
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02.prefab:3e124d842b73a534e9302947b6be8e1e");

	// Token: 0x04003B2C RID: 15148
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03.prefab:8826376ee2ecd424abd82d2f513dbfb3");

	// Token: 0x04003B2D RID: 15149
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Intro_01.prefab:c54a545aaa9585545a7d1b1d6a2cd579");

	// Token: 0x04003B2E RID: 15150
	private static readonly AssetReference VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Loss_01.prefab:7e05b1b514dbb834bae98367dc8c4589");

	// Token: 0x04003B2F RID: 15151
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeA_01.prefab:e044be60c9daf6b468d2eec0bc961587");

	// Token: 0x04003B30 RID: 15152
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5ExchangeB_01.prefab:4ec641caa9027ef478939dc16d7fda36");

	// Token: 0x04003B31 RID: 15153
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Intro_01.prefab:6b3d5621d1aadce49a35e96c2b63e831");

	// Token: 0x04003B32 RID: 15154
	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission5Victory_01.prefab:fb05d18c25c21c94caa6ac2b1fbb2762");

	// Token: 0x04003B33 RID: 15155
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Jaina_Mission5Malfurion_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Jaina_Mission5Malfurion_01.prefab:f3d7618d6d384f0f814fb40f04da4f6b");

	// Token: 0x04003B34 RID: 15156
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Thrall_01.prefab:29ede513ae5b45ae8bd2cfa59f32937b");

	// Token: 0x04003B35 RID: 15157
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Jaina_Mission5Victory_01.prefab:28dfa0839ba94acca781c893253d0510");

	// Token: 0x04003B36 RID: 15158
	private static readonly AssetReference VO_Story_Hero_Tyrande_Female_NightElf_Story_Jaina_Mission5Tyrande_01 = new AssetReference("VO_Story_Hero_Tyrande_Female_NightElf_Story_Jaina_Mission5Tyrande_01.prefab:21a832149b36441197d5d2a14101eea2");

	// Token: 0x04003B37 RID: 15159
	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	// Token: 0x04003B38 RID: 15160
	public static readonly AssetReference MalfurionBrassRing = new AssetReference("YoungMalfurion_Popup_BrassRing.prefab:5544ac85196277542a7fa0b1a9b578df");

	// Token: 0x04003B39 RID: 15161
	public static readonly AssetReference TyrandeBrassRing = new AssetReference("YoungTyrande_Popup_BrassRing.prefab:79f13833a3f5e97449ef744f460e9fbd");

	// Token: 0x04003B3A RID: 15162
	private List<string> m_VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPowerLines = new List<string>
	{
		BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_01,
		BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_02,
		BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5HeroPower_03
	};

	// Token: 0x04003B3B RID: 15163
	private List<string> m_VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5IdleLines = new List<string>
	{
		BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_01,
		BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_02,
		BoH_Jaina_05.VO_Story_Hero_Archimonde_Male_Demon_Story_Jaina_Mission5Idle_03
	};

	// Token: 0x04003B3C RID: 15164
	private HashSet<string> m_playedLines = new HashSet<string>();
}
