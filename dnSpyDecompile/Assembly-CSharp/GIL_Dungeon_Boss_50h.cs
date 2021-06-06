using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003FB RID: 1019
public class GIL_Dungeon_Boss_50h : GIL_Dungeon
{
	// Token: 0x06003887 RID: 14471 RVA: 0x0011D118 File Offset: 0x0011B318
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_50h_Male_Spirit_Intro_01.prefab:55a97cc019cc32f4c90c7d7c8d520a0f",
			"VO_GILA_BOSS_50h_Male_Spirit_EmoteResponse_01.prefab:729f8a04024b7ce4faaf8d09d3dd7ae1",
			"VO_GILA_BOSS_50h_Male_Spirit_Death_01.prefab:f9aa74547facbdb458a4428ed822bd0b",
			"VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_01.prefab:934a9489c5a8b9f42a270add91ab4f82",
			"VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_02.prefab:3660ee1b841d85b43b4600560351529b",
			"VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_03.prefab:0b9ba141e22bf714ab0ab3263f55e796",
			"VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_04.prefab:ee8e8af8894c43c478c0a5c2841b8a0d"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003888 RID: 14472 RVA: 0x0011D1C0 File Offset: 0x0011B3C0
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003889 RID: 14473 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600388A RID: 14474 RVA: 0x0011D1D6 File Offset: 0x0011B3D6
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_50h_Male_Spirit_Death_01.prefab:f9aa74547facbdb458a4428ed822bd0b";
	}

	// Token: 0x0600388B RID: 14475 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600388C RID: 14476 RVA: 0x0011D1E0 File Offset: 0x0011B3E0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_50h_Male_Spirit_Intro_01.prefab:55a97cc019cc32f4c90c7d7c8d520a0f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_50h_Male_Spirit_EmoteResponse_01.prefab:729f8a04024b7ce4faaf8d09d3dd7ae1", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600388D RID: 14477 RVA: 0x0011D267 File Offset: 0x0011B467
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardID = entity.GetCardId();
		yield return base.WaitForEntitySoundToFinish(entity);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string a = cardID;
		if (a == "GILA_500p2t")
		{
			string text = base.PopRandomLineWithChance(this.m_RandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(enemyActor, text, 2.5f);
			}
		}
		if (entity.HasTag(GAME_TAG.ECHO))
		{
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_04.prefab:ee8e8af8894c43c478c0a5c2841b8a0d", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001DC6 RID: 7622
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DC7 RID: 7623
	private List<string> m_RandomLines = new List<string>
	{
		"VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_01.prefab:934a9489c5a8b9f42a270add91ab4f82",
		"VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_02.prefab:3660ee1b841d85b43b4600560351529b",
		"VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_03.prefab:0b9ba141e22bf714ab0ab3263f55e796"
	};
}
