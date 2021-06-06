using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003EE RID: 1006
public class GIL_Dungeon_Boss_37h : GIL_Dungeon
{
	// Token: 0x0600380C RID: 14348 RVA: 0x0011B37C File Offset: 0x0011957C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_37h_Male_Murloc_Intro_01.prefab:95c9eaeeb3d8f5d45a72fc8d0c9cc53c",
			"VO_GILA_BOSS_37h_Male_Murloc_EmoteResponse_01.prefab:a906592af0362154199937ff2e902c89",
			"VO_GILA_BOSS_37h_Male_Murloc_Death_01.prefab:43009ccd78aa83f4b95e8f92b2805996",
			"VO_GILA_BOSS_37h_Male_Murloc_HeroPower_01.prefab:ef813888de24ebf48ba25732b8ef4c2a",
			"VO_GILA_BOSS_37h_Male_Murloc_HeroPower_02.prefab:84e8fda79b594044f811488ceef4fc0d",
			"VO_GILA_BOSS_37h_Male_Murloc_HeroPower_03.prefab:66928b1220f93204eb04efb008f868f7",
			"VO_GILA_BOSS_37h_Male_Murloc_HeroPower_04.prefab:5f2d5528bf215734db16f77a1e13f4e5",
			"VO_GILA_BOSS_37h_Male_Murloc_EventPlayPlague_01.prefab:fb811185b39e06d499a8039905debc9e",
			"VO_GILA_BOSS_37h_Male_Murloc_EventPlayPlague_02.prefab:e050966b8d5bb954faaff70e4eac1c6c",
			"VO_GILA_BOSS_37h_Male_Murloc_EventPlayPlague_07.prefab:83a85909e80d4b1458b2a40ce4d1863f",
			"VO_GILA_BOSS_37h_Male_Murloc_EventPlayMurlocHolmes_02.prefab:b1295ac84416a8042a38440d7a6c9427"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600380D RID: 14349 RVA: 0x0011B450 File Offset: 0x00119650
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_37h_Male_Murloc_Intro_01.prefab:95c9eaeeb3d8f5d45a72fc8d0c9cc53c", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_37h_Male_Murloc_EmoteResponse_01.prefab:a906592af0362154199937ff2e902c89", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600380E RID: 14350 RVA: 0x0011B4D7 File Offset: 0x001196D7
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_37h_Male_Murloc_Death_01.prefab:43009ccd78aa83f4b95e8f92b2805996";
	}

	// Token: 0x0600380F RID: 14351 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003810 RID: 14352 RVA: 0x0011B4DE File Offset: 0x001196DE
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "GILA_BOSS_37t")
		{
			string text = base.PopRandomLineWithChance(this.m_RandomLines);
			if (text != null)
			{
				yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x06003811 RID: 14353 RVA: 0x0011B4F4 File Offset: 0x001196F4
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_37h_Male_Murloc_HeroPower_01.prefab:ef813888de24ebf48ba25732b8ef4c2a",
			"VO_GILA_BOSS_37h_Male_Murloc_HeroPower_02.prefab:84e8fda79b594044f811488ceef4fc0d",
			"VO_GILA_BOSS_37h_Male_Murloc_HeroPower_03.prefab:66928b1220f93204eb04efb008f868f7",
			"VO_GILA_BOSS_37h_Male_Murloc_HeroPower_04.prefab:5f2d5528bf215734db16f77a1e13f4e5"
		};
	}

	// Token: 0x06003812 RID: 14354 RVA: 0x0011B527 File Offset: 0x00119727
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "GILA_827")
		{
			yield return base.PlayBossLine(actor, "VO_GILA_BOSS_37h_Male_Murloc_EventPlayMurlocHolmes_02.prefab:b1295ac84416a8042a38440d7a6c9427", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001D9F RID: 7583
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001DA0 RID: 7584
	private List<string> m_RandomLines = new List<string>
	{
		"VO_GILA_BOSS_37h_Male_Murloc_EventPlayPlague_01.prefab:fb811185b39e06d499a8039905debc9e",
		"VO_GILA_BOSS_37h_Male_Murloc_EventPlayPlague_02.prefab:e050966b8d5bb954faaff70e4eac1c6c",
		"VO_GILA_BOSS_37h_Male_Murloc_EventPlayPlague_07.prefab:83a85909e80d4b1458b2a40ce4d1863f"
	};
}
