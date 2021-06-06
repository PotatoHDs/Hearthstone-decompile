using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200044E RID: 1102
public class DALA_Dungeon_Boss_33h : DALA_Dungeon
{
	// Token: 0x06003BE5 RID: 15333 RVA: 0x00137B78 File Offset: 0x00135D78
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_BossCoin_01,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Death_02,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_DefeatPlayer_01,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_EmoteResponse_01,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_HeroPower_01,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_HeroPower_02,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_HeroPower_03,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Idle_01,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Idle_02,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Idle_03,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Idle_04,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Intro_01,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_PlayerCoin_01,
			DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_PlayerGoldenIdol_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003BE6 RID: 15334 RVA: 0x00137CBC File Offset: 0x00135EBC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Death_02;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_EmoteResponse_01;
	}

	// Token: 0x06003BE7 RID: 15335 RVA: 0x00137CF4 File Offset: 0x00135EF4
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_33h.m_IdleLines;
	}

	// Token: 0x06003BE8 RID: 15336 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003BE9 RID: 15337 RVA: 0x00137CFC File Offset: 0x00135EFC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Eudora" && cardId != "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06003BEA RID: 15338 RVA: 0x00137DB4 File Offset: 0x00135FB4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_33h.m_HeroPower);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003BEB RID: 15339 RVA: 0x00137DCA File Offset: 0x00135FCA
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (!(cardId == "GAME_005") && !(cardId == "GVG_028t"))
		{
			if (cardId == "LOOT_998k" || cardId == "DALA_709" || cardId == "LOE_019t2")
			{
				yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_PlayerGoldenIdol_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_PlayerCoin_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003BEC RID: 15340 RVA: 0x00137DE0 File Offset: 0x00135FE0
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (cardId == "GAME_005" || cardId == "GVG_028t")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_BossCoin_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04002506 RID: 9478
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_BossCoin_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_BossCoin_01.prefab:641e0356fb933da43a0863ff3f4cb97b");

	// Token: 0x04002507 RID: 9479
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Death_02 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Death_02.prefab:3cefb8526164bc44db41c9b7e32506d8");

	// Token: 0x04002508 RID: 9480
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_DefeatPlayer_01.prefab:238d5ed4853c5b64c9abd46b0019a25a");

	// Token: 0x04002509 RID: 9481
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_EmoteResponse_01.prefab:cc15f3b773696994483b1e1e643cd50a");

	// Token: 0x0400250A RID: 9482
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_HeroPower_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_HeroPower_01.prefab:301fc70e924dc5e42bb93f221682c37a");

	// Token: 0x0400250B RID: 9483
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_HeroPower_02 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_HeroPower_02.prefab:e57975c4cda39ad4995879f2dc10fc91");

	// Token: 0x0400250C RID: 9484
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_HeroPower_03 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_HeroPower_03.prefab:f74b4be1f30a53c4ba84dd2259dc4747");

	// Token: 0x0400250D RID: 9485
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Idle_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Idle_01.prefab:0ddebf7b4b38c8b49b9157047a086582");

	// Token: 0x0400250E RID: 9486
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Idle_02 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Idle_02.prefab:af8af91debd524943aa16f3c20280887");

	// Token: 0x0400250F RID: 9487
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Idle_03 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Idle_03.prefab:a97b231d4e03e7a49abe64fef00435b9");

	// Token: 0x04002510 RID: 9488
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Idle_04 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Idle_04.prefab:c0552dd3a2757a440a8a0b0479d26b30");

	// Token: 0x04002511 RID: 9489
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_Intro_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_Intro_01.prefab:cedda85793eb1d74cab0e8ce5d3e3bef");

	// Token: 0x04002512 RID: 9490
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_PlayerCoin_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_PlayerCoin_01.prefab:ab526e51b7c1be7458bc211bb1fc5024");

	// Token: 0x04002513 RID: 9491
	private static readonly AssetReference VO_DALA_BOSS_33h_Male_Elemental_PlayerGoldenIdol_01 = new AssetReference("VO_DALA_BOSS_33h_Male_Elemental_PlayerGoldenIdol_01.prefab:765931e3128a256468ea3c0a4ef07246");

	// Token: 0x04002514 RID: 9492
	private static List<string> m_HeroPower = new List<string>
	{
		DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_HeroPower_01,
		DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_HeroPower_02,
		DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_HeroPower_03
	};

	// Token: 0x04002515 RID: 9493
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Idle_01,
		DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Idle_02,
		DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Idle_03,
		DALA_Dungeon_Boss_33h.VO_DALA_BOSS_33h_Male_Elemental_Idle_04
	};

	// Token: 0x04002516 RID: 9494
	private HashSet<string> m_playedLines = new HashSet<string>();
}
