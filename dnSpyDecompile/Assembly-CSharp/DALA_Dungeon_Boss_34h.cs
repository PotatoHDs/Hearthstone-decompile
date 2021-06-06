using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200044F RID: 1103
public class DALA_Dungeon_Boss_34h : DALA_Dungeon
{
	// Token: 0x06003BF1 RID: 15345 RVA: 0x00137F70 File Offset: 0x00136170
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Death,
			DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Emote_01,
			DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Emote_02,
			DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Emote_03,
			DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Start,
			DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Taunt
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003BF2 RID: 15346 RVA: 0x00138034 File Offset: 0x00136234
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Start;
		this.m_deathLine = DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Death;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Taunt;
	}

	// Token: 0x06003BF3 RID: 15347 RVA: 0x0013806C File Offset: 0x0013626C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Eudora")
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

	// Token: 0x06003BF4 RID: 15348 RVA: 0x00138117 File Offset: 0x00136317
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Emote_01,
			DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Emote_02,
			DALA_Dungeon_Boss_34h.CarouselGryphon_DALA_Boss_34h_Emote_03
		};
	}

	// Token: 0x06003BF5 RID: 15349 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003BF6 RID: 15350 RVA: 0x0013814E File Offset: 0x0013634E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		yield break;
	}

	// Token: 0x06003BF7 RID: 15351 RVA: 0x00138164 File Offset: 0x00136364
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
		yield break;
	}

	// Token: 0x06003BF8 RID: 15352 RVA: 0x0013817A File Offset: 0x0013637A
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		yield break;
	}

	// Token: 0x04002517 RID: 9495
	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Death = new AssetReference("CarouselGryphon_DALA_Boss_34h_Death.prefab:9a5d763ff8219e64493f0c394518157a");

	// Token: 0x04002518 RID: 9496
	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Emote_01 = new AssetReference("CarouselGryphon_DALA_Boss_34h_Emote_01.prefab:6ab7a927d2c03c74ca44623c844c1e47");

	// Token: 0x04002519 RID: 9497
	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Emote_02 = new AssetReference("CarouselGryphon_DALA_Boss_34h_Emote_02.prefab:98c16c64b2f2d1e41bc8ce4e974f2f95");

	// Token: 0x0400251A RID: 9498
	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Emote_03 = new AssetReference("CarouselGryphon_DALA_Boss_34h_Emote_03.prefab:194e38fef6218364c962b73e7610ef17");

	// Token: 0x0400251B RID: 9499
	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Start = new AssetReference("CarouselGryphon_DALA_Boss_34h_Start.prefab:2373356c6e363214eb50c87492284296");

	// Token: 0x0400251C RID: 9500
	private static readonly AssetReference CarouselGryphon_DALA_Boss_34h_Taunt = new AssetReference("CarouselGryphon_DALA_Boss_34h_Taunt.prefab:392547bde2ed1cd45b457f1d0a6788a3");

	// Token: 0x0400251D RID: 9501
	private HashSet<string> m_playedLines = new HashSet<string>();
}
