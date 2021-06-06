using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000409 RID: 1033
public class GIL_Dungeon_Boss_66h : GIL_Dungeon
{
	// Token: 0x06003908 RID: 14600 RVA: 0x0011ED80 File Offset: 0x0011CF80
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_66h_Female_Undead_Intro_01.prefab:a5215794eefe1114d9ffd4cc13cc7d1e",
			"VO_GILA_BOSS_66h_Female_Undead_EmoteResponse_02.prefab:d3150f2caf430a44ba2ff7eee0dc7a8a",
			"VO_GILA_BOSS_66h_Female_Undead_Death_02.prefab:1580a674aefbc4e459cbb59d1001fa95",
			"VO_GILA_BOSS_66h_Female_Undead_HeroPower_01.prefab:710a76b29da400648a62052df042614d",
			"VO_GILA_BOSS_66h_Female_Undead_HeroPower_02.prefab:03e8bf23eba1f7a49957b1dc5fba2e6f",
			"VO_GILA_BOSS_66h_Female_Undead_HeroPower_03.prefab:1442e3289ff370f4ca908faf4be06c50",
			"VO_GILA_BOSS_66h_Female_Undead_EventPlayHorn_02.prefab:8c24650aeffa2f142963239d8a560e7d"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003909 RID: 14601 RVA: 0x0011EE28 File Offset: 0x0011D028
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x0600390A RID: 14602 RVA: 0x0011EE3E File Offset: 0x0011D03E
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_66h_Female_Undead_HeroPower_01.prefab:710a76b29da400648a62052df042614d",
			"VO_GILA_BOSS_66h_Female_Undead_HeroPower_02.prefab:03e8bf23eba1f7a49957b1dc5fba2e6f",
			"VO_GILA_BOSS_66h_Female_Undead_HeroPower_03.prefab:1442e3289ff370f4ca908faf4be06c50"
		};
	}

	// Token: 0x0600390B RID: 14603 RVA: 0x0011EE66 File Offset: 0x0011D066
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_66h_Female_Undead_Death_02.prefab:1580a674aefbc4e459cbb59d1001fa95";
	}

	// Token: 0x0600390C RID: 14604 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600390D RID: 14605 RVA: 0x0011EE70 File Offset: 0x0011D070
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_66h_Female_Undead_Intro_01.prefab:a5215794eefe1114d9ffd4cc13cc7d1e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_66h_Female_Undead_EmoteResponse_02.prefab:d3150f2caf430a44ba2ff7eee0dc7a8a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600390E RID: 14606 RVA: 0x0011EEF7 File Offset: 0x0011D0F7
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
		if (cardId == "GILA_852a")
		{
			yield return base.PlayEasterEggLine(actor, "VO_GILA_BOSS_66h_Female_Undead_EventPlayHorn_02.prefab:8c24650aeffa2f142963239d8a560e7d", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001DE6 RID: 7654
	private HashSet<string> m_playedLines = new HashSet<string>();
}
