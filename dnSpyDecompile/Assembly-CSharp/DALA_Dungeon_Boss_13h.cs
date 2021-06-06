using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200043A RID: 1082
public class DALA_Dungeon_Boss_13h : DALA_Dungeon
{
	// Token: 0x06003AED RID: 15085 RVA: 0x00131920 File Offset: 0x0012FB20
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_Death_01,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_DefeatPlayer_01,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_EmoteResponse_01,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPower_02,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPower_03,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPower_04,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPower_05,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPower_06,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPowerFriendly_01,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPowerFriendly_02,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_Idle_01,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_Idle_02,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_Idle_05,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_Intro_01
		};
		base.SetBossVOLines(list);
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003AEE RID: 15086 RVA: 0x00131A68 File Offset: 0x0012FC68
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_13h.m_IdleLines;
	}

	// Token: 0x06003AEF RID: 15087 RVA: 0x00131A70 File Offset: 0x0012FC70
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPower_02,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPower_03,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPower_04,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPower_05,
			DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPower_06
		};
	}

	// Token: 0x06003AF0 RID: 15088 RVA: 0x00131AD2 File Offset: 0x0012FCD2
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_EmoteResponse_01;
	}

	// Token: 0x06003AF1 RID: 15089 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003AF2 RID: 15090 RVA: 0x00131B0C File Offset: 0x0012FD0C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_Eudora" && cardId != "DALA_Vessina")
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

	// Token: 0x06003AF3 RID: 15091 RVA: 0x00131BC4 File Offset: 0x0012FDC4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 101)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_13h.HeroPowerFriendly);
		}
		else
		{
			yield return base.HandleMissionEventWithTiming(missionEvent);
		}
		yield break;
	}

	// Token: 0x06003AF4 RID: 15092 RVA: 0x00131BDA File Offset: 0x0012FDDA
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

	// Token: 0x06003AF5 RID: 15093 RVA: 0x00131BF0 File Offset: 0x0012FDF0
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
		yield break;
	}

	// Token: 0x04002339 RID: 9017
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_Death_01 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_Death_01.prefab:7d1fa6f34da72af4cb5819da98277e7e");

	// Token: 0x0400233A RID: 9018
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_DefeatPlayer_01.prefab:f120d0002717df74da0cf6d6983aa142");

	// Token: 0x0400233B RID: 9019
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_EmoteResponse_01.prefab:c1f9b9bc10545de4d9f6cd6c64317eaf");

	// Token: 0x0400233C RID: 9020
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_HeroPower_02 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_HeroPower_02.prefab:040ee66d9bc558943abc5e79149e92d4");

	// Token: 0x0400233D RID: 9021
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_HeroPower_03 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_HeroPower_03.prefab:c8dc93b06a686104b9f38b345bd08b3b");

	// Token: 0x0400233E RID: 9022
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_HeroPower_04 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_HeroPower_04.prefab:80514398e6f47cc4baccb2e8460f7b52");

	// Token: 0x0400233F RID: 9023
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_HeroPower_05 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_HeroPower_05.prefab:6de69f513fc739d41bbcbe5136148752");

	// Token: 0x04002340 RID: 9024
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_HeroPower_06 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_HeroPower_06.prefab:279101da6ff5a4a4f8991ead8e88593f");

	// Token: 0x04002341 RID: 9025
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_HeroPowerFriendly_01 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_HeroPowerFriendly_01.prefab:5bfc1fcf7822c4c4cb709e35e1901d7b");

	// Token: 0x04002342 RID: 9026
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_HeroPowerFriendly_02 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_HeroPowerFriendly_02.prefab:38417adf93a31884aba3ef47c27b4827");

	// Token: 0x04002343 RID: 9027
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_Idle_01 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_Idle_01.prefab:b8d28c02135ef2c4082a1eeb14729708");

	// Token: 0x04002344 RID: 9028
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_Idle_02 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_Idle_02.prefab:53644bdaa2e3d6e40877cb3f10f694c3");

	// Token: 0x04002345 RID: 9029
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_Idle_05 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_Idle_05.prefab:0bd4263c7a176d94ab6c90cdd1834796");

	// Token: 0x04002346 RID: 9030
	private static readonly AssetReference VO_DALA_BOSS_13h_Male_Worgen_Intro_01 = new AssetReference("VO_DALA_BOSS_13h_Male_Worgen_Intro_01.prefab:efd712918aa54504783f8fb8dca274f6");

	// Token: 0x04002347 RID: 9031
	private static List<string> HeroPowerFriendly = new List<string>
	{
		DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPowerFriendly_01,
		DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_HeroPowerFriendly_02
	};

	// Token: 0x04002348 RID: 9032
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_Idle_01,
		DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_Idle_02,
		DALA_Dungeon_Boss_13h.VO_DALA_BOSS_13h_Male_Worgen_Idle_05
	};

	// Token: 0x04002349 RID: 9033
	private HashSet<string> m_playedLines = new HashSet<string>();
}
